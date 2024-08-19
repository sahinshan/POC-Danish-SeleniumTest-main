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
    public class CaseCaseNote : BaseClass
    {

        public string TableName = "CaseCaseNote";
        public string PrimaryKeyName = "CaseCaseNoteId";


        public CaseCaseNote(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreateCaseCaseNote(string Subject, string Notes, Guid OwnerId, Guid ResponsibleUserId, Guid ActivityCategoryId, Guid ActivitySubCategoryId, Guid ActivityOutcomeId, Guid ActivityReasonId, Guid ActivityPriorityId, Guid CaseId, Guid PersonID, DateTime CaseNoteDate)
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
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CaseId", CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CaseNoteDate", CaseNoteDate);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", 1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateCaseCaseNote(string Subject, string Notes, Guid OwnerId, Guid ResponsibleUserId, Guid ActivityCategoryId, Guid ActivitySubCategoryId, Guid ActivityOutcomeId, Guid ActivityReasonId, Guid ActivityPriorityId, Guid CaseId, Guid PersonID, DateTime CaseNoteDate,Guid Significanteventcategoryid,DateTime Significanteventdate)
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
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CaseId", CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CaseNoteDate", CaseNoteDate);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", 1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", true);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Significanteventcategoryid", Significanteventcategoryid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Significanteventdate", Significanteventdate);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public void UpdateSubject(Guid CaseNoteId, string Subject)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseNoteId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Subject", Subject);

            UpdateRecord(buisinessDataObject);
        }

        public void UpdateStatus(Guid CaseNoteId, int statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseNoteId);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);

            UpdateRecord(buisinessDataObject);
        }

        public void UpdateInactive(Guid CaseNoteId, bool Inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseNoteId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);

            UpdateRecord(buisinessDataObject);
        }

        public void UpdateLegacyId(Guid CaseNoteId, string legacyid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseNoteId);
            AddFieldToBusinessDataObject(buisinessDataObject, "legacyid", legacyid);

            UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetCaseNoteByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCaseCaseNoteByID(Guid CaseCaseNoteId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseCaseNoteId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCaseCaseNote(Guid CaseCaseNoteId)
        {
            this.DeleteRecord(TableName, CaseCaseNoteId);
        }
    }
}
