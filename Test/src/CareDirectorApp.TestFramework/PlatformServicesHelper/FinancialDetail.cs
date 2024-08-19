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
    public class FinancialDetail : BaseClass
    {

        public string TableName = "FinancialDetail";
        public string PrimaryKeyName = "FinancialDetailId";
        

        public FinancialDetail(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetFinancialDetailByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetFinancialDetailByID(Guid FinancialDetailId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, FinancialDetailId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteFinancialDetail(Guid FinancialDetailId)
        {
            this.DeleteRecord(TableName, FinancialDetailId);
        }
    }
}
