using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinanceTransactionTrigger : BaseClass
    {

        private string TableName = "FinanceTransactionTrigger";
        private string PrimaryKeyName = "FinanceTransactionTriggerId";

        public FinanceTransactionTrigger()
        {
            AuthenticateUser();
        }

        public FinanceTransactionTrigger(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByRecordidTableNameAndRecordidName(string recordidtablename, string recordidname)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "recordidtablename", ConditionOperatorType.Equal, recordidtablename);
            this.BaseClassAddTableCondition(query, "recordidname", ConditionOperatorType.Equal, recordidname);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByFinanceTransactionTriggerId(Guid FinanceTransactionTriggerId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, FinanceTransactionTriggerId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageEpisodeRejectionReasonId(Guid BrokerageEpisodeRejectionReasonId)
        {
            this.DeleteRecord(TableName, BrokerageEpisodeRejectionReasonId);
        }

    }
}
