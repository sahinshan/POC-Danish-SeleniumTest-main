using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ContactStatus : BaseClass
    {
        public string TableName { get { return "ContactStatus"; } }
        public string PrimaryKeyName { get { return "ContactStatusid"; } }


        public ContactStatus()
        {
            AuthenticateUser();
        }

        public ContactStatus(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateContactStatus(Guid ownerid, string Name, string Code, DateTime StartDate, int CategoryId, bool ValidForReopening)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(businessDataObject, "Code", Code);
            AddFieldToBusinessDataObject(businessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(businessDataObject, "CategoryId", CategoryId);
            AddFieldToBusinessDataObject(businessDataObject, "ValidForReopening", ValidForReopening);

            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "validforexport", false);

            return this.CreateRecord(businessDataObject);
        }

        public Guid CreateContactStatus(Guid ContactStatusid, Guid ownerid, string Name, string Code, DateTime StartDate, int CategoryId, bool ValidForReopening)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, PrimaryKeyName, ContactStatusid);
            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(businessDataObject, "Code", Code);
            AddFieldToBusinessDataObject(businessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(businessDataObject, "CategoryId", CategoryId);
            AddFieldToBusinessDataObject(businessDataObject, "ValidForReopening", ValidForReopening);

            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "validforexport", false);

            return this.CreateRecord(businessDataObject);
        }

        public List<Guid> GetByName(string Name)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ContactStatusId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ContactStatusId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteContactStatus(Guid ContactStatusid)
        {
            this.DeleteRecord(TableName, ContactStatusid);
        }
    }
}
