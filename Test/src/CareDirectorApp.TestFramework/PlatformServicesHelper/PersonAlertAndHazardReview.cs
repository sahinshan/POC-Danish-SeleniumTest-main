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
    public class PersonAlertAndHazardReview : BaseClass
    {

        public string TableName = "PersonAlertAndHazardReview";
        public string PrimaryKeyName = "PersonAlertAndHazardReviewId";
        

        public PersonAlertAndHazardReview(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }


        public List<Guid> GetPersonAlertAndHazardReviewByPersonAlertHazardID(Guid PersonAlertAndHazardId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonAlertAndHazardId", ConditionOperatorType.Equal, PersonAlertAndHazardId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonAlertAndHazardReviewByID(Guid PersonAlertAndHazardReviewId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonAlertAndHazardReviewId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonAlertAndHazardReview(Guid PersonAlertAndHazardReviewId)
        {
            this.DeleteRecord(TableName, PersonAlertAndHazardReviewId);
        }
    }
}
