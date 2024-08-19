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
    public class FrequencyOfReceipt : BaseClass
    {

        public string TableName = "FrequencyOfReceipt";
        public string PrimaryKeyName = "FrequencyOfReceiptId";
        

        public FrequencyOfReceipt(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetFrequencyOfReceiptByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetFrequencyOfReceiptByID(Guid FrequencyOfReceiptId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, FrequencyOfReceiptId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteFrequencyOfReceipt(Guid FrequencyOfReceiptId)
        {
            this.DeleteRecord(TableName, FrequencyOfReceiptId);
        }
    }
}
