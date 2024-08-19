using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonAllergicReaction : BaseClass
    {

        public string TableName = "PersonAllergicReaction";
        public string PrimaryKeyName = "PersonAllergicReactionId";


        public PersonAllergicReaction()
        {
            AuthenticateUser();
        }

        public PersonAllergicReaction(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonAllergyID(Guid PersonAllergyId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonAllergyId", ConditionOperatorType.Equal, PersonAllergyId);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonAllergicReactionByID(Guid PersonAllergicReactionId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonAllergicReactionId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonAllergicReaction(Guid PersonAllergicReactionId)
        {
            this.DeleteRecord(TableName, PersonAllergicReactionId);
        }
    }
}
