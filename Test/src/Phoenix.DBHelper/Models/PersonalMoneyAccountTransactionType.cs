using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonalMoneyAccountTransactionType : BaseClass
    {
        public string TableName = "PersonalMoneyAccountTransactionType";
        public string PrimaryKeyName = "PersonalMoneyAccountTransactionTypeId";

        public PersonalMoneyAccountTransactionType()
        {
            AuthenticateUser();
        }

        public PersonalMoneyAccountTransactionType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonalMoneyAccountTransactionTypeByID(Guid PersonalMoneyAccountTransactionTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonalMoneyAccountTransactionTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


    }
}
