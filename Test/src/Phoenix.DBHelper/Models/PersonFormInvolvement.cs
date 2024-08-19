using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonFormInvolvement : BaseClass
    {

        public string TableName = "PersonFormInvolvement";
        public string PrimaryKeyName = "PersonFormInvolvementId";


        public PersonFormInvolvement()
        {
            AuthenticateUser();
        }

        public PersonFormInvolvement(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonFormInvolement(Guid OwnerId, Guid personid, Guid PersonformId, Guid involvementmemberid, Guid involvementroleid, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "PersonformId", PersonformId);
            AddFieldToBusinessDataObject(dataObject, "involvementmemberid", involvementmemberid);
            AddFieldToBusinessDataObject(dataObject, "involvementroleid", involvementroleid);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);


            return this.CreateRecord(dataObject);
        }
        public List<Guid> GetByPersonFormInvolvementId(Guid PersonFormId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonFormId", ConditionOperatorType.Equal, PersonFormId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid PersonFormInvolvementId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonFormInvolvementId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonFormInvolement(Guid PersonFormInvolvementId)
        {
            this.DeleteRecord(TableName, PersonFormInvolvementId);
        }
    }
}
