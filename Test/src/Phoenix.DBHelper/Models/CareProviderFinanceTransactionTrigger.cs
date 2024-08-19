using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderFinanceTransactionTrigger : BaseClass
    {

        public string TableName = "CareProviderFinanceTransactionTrigger";
        public string PrimaryKeyName = "CareProviderFinanceTransactionTriggerid";

        public CareProviderFinanceTransactionTrigger()
        {
            AuthenticateUser();
        }

        public CareProviderFinanceTransactionTrigger(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByRecordId(Guid recordid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "recordid", ConditionOperatorType.Equal, recordid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByRecordIdAndStatus(Guid recordid, int statusid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "recordid", ConditionOperatorType.Equal, recordid);
            this.BaseClassAddTableCondition(query, "statusid", ConditionOperatorType.Equal, statusid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CareProviderFinanceTransactionTriggerId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderFinanceTransactionTriggerId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetAll(bool inactive = true)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "inactive", ConditionOperatorType.Equal, inactive);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByRecordId(Guid recordid, string recordidtablename, string recordidname, int? statusid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "recordid", ConditionOperatorType.Equal, recordid);
            this.BaseClassAddTableCondition(query, "recordidtablename", ConditionOperatorType.Equal, recordidtablename);
            this.BaseClassAddTableCondition(query, "recordidname", ConditionOperatorType.Equal, recordidname);
            if (statusid.HasValue)
                this.BaseClassAddTableCondition(query, "statusid", ConditionOperatorType.Equal, statusid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
    }
}
