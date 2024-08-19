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
    public class Task : BaseClass
    {

        public string TableName = "Task";
        public string PrimaryKeyName = "TaskId";
        

        public Task(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreateTask(string Subject, string Notes, Guid OwnerId, Guid ResponsibleUserId, Guid ActivityCategoryId, 
            Guid ActivitySubCategoryId, Guid ActivityOutcomeId, Guid ActivityReasonId, Guid ActivityPriorityId, Guid? CaseId, 
            Guid PersonID, DateTime DueDate, Guid RegardingId, string RegardingIdName, string RegardingIdTableName)
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
            if(CaseId.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "CaseId", CaseId.Value);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DueDate", DueDate);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "RegardingId", RegardingId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdName", RegardingIdName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdTableName", RegardingIdTableName);


            this.AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", 1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsCaseNote", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetTaskByHealthAppointmentID(Guid HealthAppointmentID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "RegardingId", ConditionOperatorType.Equal, HealthAppointmentID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetTaskByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetTaskByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetTaskByID(Guid TaskId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, TaskId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateDataRestriction(Guid TaskID, Guid? DataRestrictionId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, TaskID);
            if(DataRestrictionId.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "DataRestrictionId", DataRestrictionId);
            else
                this.AddFieldToBusinessDataObject(buisinessDataObject, "DataRestrictionId", null);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteTask(Guid TaskId)
        {
            this.DeleteRecord(TableName, TaskId);
        }
    }
}
