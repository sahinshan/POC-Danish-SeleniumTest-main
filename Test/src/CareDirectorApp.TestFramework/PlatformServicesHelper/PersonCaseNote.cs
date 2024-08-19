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
    public class PersonCaseNote : BaseClass
    {

        public string TableName = "PersonCaseNote";
        public string PrimaryKeyName = "PersonCaseNoteId";
        

        public PersonCaseNote(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreatePersonCaseNote(string Subject, string Notes, Guid OwnerId, Guid ResponsibleUserId, Guid ActivityCategoryId, Guid ActivitySubCategoryId, Guid ActivityOutcomeId, Guid ActivityReasonId, Guid ActivityPriorityId, Guid PersonId, DateTime CaseNoteDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "Subject", Subject);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Notes", Notes);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", ResponsibleUserId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ActivityCategoryId", ActivityCategoryId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ActivitySubCategoryId", ActivitySubCategoryId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ActivityOutcomeId", ActivityOutcomeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ActivityReasonId", ActivityReasonId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ActivityPriorityId", ActivityPriorityId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CaseNoteDate", CaseNoteDate);
            
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", 1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonCaseNoteByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonCaseNoteByID(Guid PersonCaseNoteId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonCaseNoteId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonCaseNote(Guid PersonCaseNoteId)
        {
            this.DeleteRecord(TableName, PersonCaseNoteId);
        }
    }
}
