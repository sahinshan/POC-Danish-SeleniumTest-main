using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonFormOutcome : BaseClass
    {

        public string TableName = "PersonFormOutcome";
        public string PrimaryKeyName = "PersonFormOutcomeId";


        public PersonFormOutcome()
        {
            AuthenticateUser();
        }

        public PersonFormOutcome(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonFormId(Guid PersonFormId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonFormId", ConditionOperatorType.Equal, PersonFormId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid PersonFormOutcomeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonFormOutcomeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonFormOutcome(Guid PersonFormOutcomeId)
        {
            this.DeleteRecord(TableName, PersonFormOutcomeId);
        }
    }
}
