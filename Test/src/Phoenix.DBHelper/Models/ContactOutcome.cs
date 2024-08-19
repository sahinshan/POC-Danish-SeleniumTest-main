using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ContactOutcome : BaseClass
    {
        public string TableName { get { return "ContactOutcome"; } }
        public string PrimaryKeyName { get { return "ContactOutcomeid"; } }


        public ContactOutcome()
        {
            AuthenticateUser();
        }

        public ContactOutcome(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateContactOutcome(Guid ownerid, string Name, DateTime StartDate, bool ValidForSequelToContact, bool ValidForAdministrativeCategory, int OutcomeCategoryId)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(businessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(businessDataObject, "ValidForSequelToContact", ValidForSequelToContact);
            AddFieldToBusinessDataObject(businessDataObject, "ValidForAdministrativeCategory", ValidForAdministrativeCategory);
            AddFieldToBusinessDataObject(businessDataObject, "OutcomeCategoryId", OutcomeCategoryId);

            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "validforexport", false);

            return this.CreateRecord(businessDataObject);
        }

        public Guid CreateContactOutcome(Guid ContactOutcomeid, Guid ownerid, string Name, DateTime StartDate, bool ValidForSequelToContact, bool ValidForAdministrativeCategory, int OutcomeCategoryId)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ContactOutcomeid", ContactOutcomeid);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(businessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(businessDataObject, "ValidForSequelToContact", ValidForSequelToContact);
            AddFieldToBusinessDataObject(businessDataObject, "ValidForAdministrativeCategory", ValidForAdministrativeCategory);
            AddFieldToBusinessDataObject(businessDataObject, "OutcomeCategoryId", OutcomeCategoryId);

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

        public Dictionary<string, object> GetByID(Guid ContactOutcomeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ContactOutcomeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteContactOutcome(Guid ContactOutcomeid)
        {
            this.DeleteRecord(TableName, ContactOutcomeid);
        }
    }
}
