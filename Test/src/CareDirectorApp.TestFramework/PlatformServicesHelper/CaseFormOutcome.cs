using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public class CaseFormOutcome : BaseClass
    {

        public string TableName = "CaseFormOutcome";
        public string PrimaryKeyName = "CaseFormOutcomeId";
        

        public CaseFormOutcome(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetByCaseFormID(Guid CaseFormId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "CaseFormId", ConditionOperatorType.Equal, CaseFormId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCaseFormOutcomeByID(Guid CaseFormOutcomeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseFormOutcomeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCaseFormOutcome(Guid CaseFormOutcomeId)
        {
            this.DeleteRecord(TableName, CaseFormOutcomeId);
        }
    }
}
