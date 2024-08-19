using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ContractEndReason : BaseClass
    {

        public string TableName = "ContractEndReason";
        public string PrimaryKeyName = "ContractEndReasonId";


        public ContractEndReason()
        {
            AuthenticateUser();
        }

        public ContractEndReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }



        public List<Guid> GetByName(string name)
        {
            var query = GetDataQueryObject(TableName, false, PrimaryKeyName);
            AddReturnField(query, TableName, PrimaryKeyName);

            BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetContractEndReasonByID(Guid ContractEndReasonId, params string[] FieldsToReturn)
        {
            var query = GetDataQueryObject(TableName, false, PrimaryKeyName);
            AddReturnFields(query, TableName, FieldsToReturn);

            BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ContractEndReasonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
