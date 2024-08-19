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
    public class PersonBodyMapReview : BaseClass
    {

        public string TableName = "PersonBodyMapReview";
        public string PrimaryKeyName = "PersonBodyMapReviewId";


        public PersonBodyMapReview(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreatePersonBodyMapReview(Guid OwnerId, Guid PersonBodyMapId, Guid PersonId, DateTime PlannedReviewDate, 
            DateTime? ActualReviewDate, int? IsReviewRequiredId, DateTime? NextReviewDate, string ProfessionalComments, string ClientComments, string AgreedOutcome)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonBodyMapId", PersonBodyMapId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PlannedReviewDate", PlannedReviewDate);

            if (ActualReviewDate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "ActualReviewDate", ActualReviewDate);
            
            if(IsReviewRequiredId.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "IsReviewRequiredId", IsReviewRequiredId.Value);
            
            if(NextReviewDate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "NextReviewDate", NextReviewDate);
            
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ProfessionalComments", ProfessionalComments);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClientComments", ClientComments);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "AgreedOutcome", AgreedOutcome);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonBodyMapReviewByPersonBodyMapIdID(Guid PersonBodyMapId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonBodyMapId", ConditionOperatorType.Equal, PersonBodyMapId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetPersonBodyMapReviewByPersonBodyMapIdID(Guid PersonBodyMapId, DateTime PlannedReviewDate)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonBodyMapId", ConditionOperatorType.Equal, PersonBodyMapId);
            this.AddTableCondition(query, "PlannedReviewDate", ConditionOperatorType.Equal, PlannedReviewDate);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonBodyMapReviewByID(Guid PersonBodyMapReviewId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonBodyMapReviewId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonBodyMapReview(Guid PersonBodyMapReviewId)
        {
            this.DeleteRecord(TableName, PersonBodyMapReviewId);
        }
    }
}
