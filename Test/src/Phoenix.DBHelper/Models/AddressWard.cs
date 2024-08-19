using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class AddressWard : BaseClass
    {
        public string TableName = "AddressWard";
        public string PrimaryKeyName = "AddressWardId";

        public AddressWard()
        {
            AuthenticateUser();
        }

        public AddressWard(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateAddressWard(string Name, DateTime StartDate, Guid OwnerId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetAddressWardByName1(string Name)
        {
            var query = new DataQuery(TableName, true, PrimaryKeyName);
            query.PrimaryKeyName = PrimaryKeyName;

            query.Filter.AddCondition(TableName, "Name", ConditionOperatorType.Equal, Name);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection[PrimaryKeyName]).ToList();
            else
                return new List<Guid>();
        }


        public List<Guid> GetAddressWardByName(string AddressWardName)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("AddressWard", true, "AddressWardId");
            query.PrimaryKeyName = "AddressWardId";

            query.Filter.AddCondition("AddressWard", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, AddressWardName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => Guid.Parse((c.FieldCollection["AddressWardId"]).ToString())).ToList();
            else
                return new List<Guid>();
        }
    }
}
