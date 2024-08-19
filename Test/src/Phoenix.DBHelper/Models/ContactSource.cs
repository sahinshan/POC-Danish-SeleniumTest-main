using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class ContactSource : BaseClass
    {

        public string TableName = "ContactSource".ToLower();
        public string PrimaryKeyName = "ContactSourceId".ToLower();

        public ContactSource()
        {
            AuthenticateUser();
        }

        public ContactSource(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateContactSource(Guid OwnerId, string Name, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateContactSource(Guid ContactSourceId, Guid OwnerId, string Name, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ContactSourceId", ContactSourceId);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateContactSource(Guid OwnerId, string Name, DateTime StartDate, string Code, string GovCode)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "GovCode", GovCode);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string ContactSourceName)
        {
            var query = new DataQuery(TableName, true, PrimaryKeyName);
            query.PrimaryKeyName = PrimaryKeyName;

            query.Filter.AddCondition(TableName, "name", ConditionOperatorType.Equal, ContactSourceName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => Guid.Parse(c.FieldCollection[PrimaryKeyName].ToString())).ToList();
            else
                return new List<Guid>();
        }
    }
}
