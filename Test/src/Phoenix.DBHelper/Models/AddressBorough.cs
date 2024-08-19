using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class AddressBorough : BaseClass
    {
        public string TableName = "AddressBorough";
        public string PrimaryKeyName = "AddressBoroughId";

        public AddressBorough()
        {
            AuthenticateUser();
        }

        public AddressBorough(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateAddressBorough(string Name, DateTime StartDate, Guid OwnerId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetAddressBoroughByName1(string Name)
        {
            var query = new DataQuery(TableName, true, PrimaryKeyName);
            query.PrimaryKeyName = PrimaryKeyName;

            query.Filter.AddCondition(TableName, "Name", ConditionOperatorType.Equal, Name);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);
            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => Guid.Parse((c.FieldCollection["LanguageId"]).ToString())).ToList();
            else
                return new List<Guid>();
        }



        public List<Guid> GetAddressBoroughByName(string Name)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("AddressBorough", true, "AddressBoroughId");
            query.PrimaryKeyName = "AddressBoroughId";

            query.Filter.AddCondition("AddressBorough", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => Guid.Parse((c.FieldCollection["AddressBoroughId"]).ToString())).ToList();
            else
                return new List<Guid>();
        }

    }
}
