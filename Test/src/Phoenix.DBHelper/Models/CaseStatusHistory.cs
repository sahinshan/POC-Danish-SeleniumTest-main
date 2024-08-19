using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CaseStatusHistory : BaseClass
    {
        public string TableName { get { return "CaseStatusHistory"; } }
        public string PrimaryKeyName { get { return "CaseStatusHistoryid"; } }


        public CaseStatusHistory()
        {
            AuthenticateUser();
        }

        public CaseStatusHistory(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByCaseID(Guid CaseId)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPersonID(Guid PersonID)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personId", ConditionOperatorType.Equal, PersonID);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CaseStatusHistoryId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseStatusHistoryId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCaseStatusHistory(Guid CaseStatusHistoryid)
        {
            this.DeleteRecord(TableName, CaseStatusHistoryid);
        }
    }
}
