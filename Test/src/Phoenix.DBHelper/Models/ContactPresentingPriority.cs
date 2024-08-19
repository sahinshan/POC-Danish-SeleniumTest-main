using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ContactPresentingPriority : BaseClass
    {
        public string TableName { get { return "ContactPresentingPriority"; } }
        public string PrimaryKeyName { get { return "ContactPresentingPriorityid"; } }


        public ContactPresentingPriority()
        {
            AuthenticateUser();
        }

        public ContactPresentingPriority(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateContactPresentingPriority(Guid ownerid, string Name, string Code, string GovCode, DateTime StartDate)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(businessDataObject, "Code", Code);
            AddFieldToBusinessDataObject(businessDataObject, "GovCode", GovCode);
            AddFieldToBusinessDataObject(businessDataObject, "StartDate", StartDate);

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

        public Dictionary<string, object> GetByID(Guid ContactPresentingPriorityId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ContactPresentingPriorityId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteContactPresentingPriority(Guid ContactPresentingPriorityid)
        {
            this.DeleteRecord(TableName, ContactPresentingPriorityid);
        }
    }
}
