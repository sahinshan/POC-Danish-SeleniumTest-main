using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class Task : BaseClass
    {

        public string TableName = "Task";
        public string PrimaryKeyName = "TaskId";


        public Task()
        {
            AuthenticateUser();
        }

        public Task(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateProfessionalTask(Guid ProfessionalID, string ProfessionalName, string Subject, string Notes, Guid OwnerId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingId", ProfessionalID);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdTableName", "professional");
            AddFieldToBusinessDataObject(buisinessDataObject, "Notes", Notes);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdName", ProfessionalName);
            AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", 1);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreatePersonTask(Guid PersonID, string PersonName, string Subject, string Notes, Guid OwnerId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingId", PersonID);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdTableName", "person");
            AddFieldToBusinessDataObject(buisinessDataObject, "Notes", Notes);
            AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonID);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdName", PersonName);
            AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", 1);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreatePersonTask(Guid PersonID, string PersonName, string Subject, string Notes, Guid OwnerId, DateTime? duedate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingId", PersonID);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdTableName", "person");
            AddFieldToBusinessDataObject(buisinessDataObject, "Notes", Notes);
            AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonID);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdName", PersonName);
            AddFieldToBusinessDataObject(buisinessDataObject, "duedate", duedate);
            AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", 1);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreatePersonTask(Guid PersonID, string PersonName, string Subject, string Notes, Guid OwnerId, Guid ResponsibleUserId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingId", PersonID);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdTableName", "person");
            AddFieldToBusinessDataObject(buisinessDataObject, "Notes", Notes);
            AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonID);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdName", PersonName);
            AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", 1);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> CreateMultiplePersonTasks(int TotalRecordsToCreate, List<Guid> PersonIDs, List<string> subjects, List<string> notesList, Guid OwnerId, DateTime? duedate, List<Guid> responsibleusersids)
        {
            var allRecordsToCreate = new List<BusinessData>();
            var rnd = new Random();

            for (int i = 0; i < TotalRecordsToCreate; i++)
            {
                var Subject = subjects[rnd.Next(0, subjects.Count)];
                var Notes = notesList[rnd.Next(0, notesList.Count)];
                var responsibleuserid = responsibleusersids[rnd.Next(0, responsibleusersids.Count)];
                var personID = PersonIDs[rnd.Next(0, PersonIDs.Count)];

                var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
                AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
                AddFieldToBusinessDataObject(buisinessDataObject, "Subject", Subject);
                AddFieldToBusinessDataObject(buisinessDataObject, "RegardingId", personID);
                AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdTableName", "person");
                AddFieldToBusinessDataObject(buisinessDataObject, "Notes", Notes);
                AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", personID);
                AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdName", "person name");
                AddFieldToBusinessDataObject(buisinessDataObject, "duedate", duedate);
                AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
                AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", 1);

                allRecordsToCreate.Add(buisinessDataObject);
            }


            return CreateMultipleRecords(allRecordsToCreate);
        }

        public Guid CreateTask(string Subject, string Notes, Guid OwnerId, Guid? ResponsibleUserId, Guid? ActivityCategoryId,
            Guid? ActivitySubCategoryId, Guid? ActivityOutcomeId, Guid? ActivityReasonId, Guid? ActivityPriorityId, Guid? CaseId,
            Guid PersonID, DateTime? DueDate, Guid RegardingId, string RegardingIdName, string RegardingIdTableName)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(buisinessDataObject, "Notes", Notes);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ActivityCategoryId", ActivityCategoryId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ActivitySubCategoryId", ActivitySubCategoryId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ActivityOutcomeId", ActivityOutcomeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ActivityReasonId", ActivityReasonId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ActivityPriorityId", ActivityPriorityId);

            if (CaseId.HasValue)
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

        public Guid CreateTask(string Subject, string Notes, Guid OwnerId, Guid ResponsibleUserId, Guid? ActivityCategoryId,
            Guid? ActivitySubCategoryId, Guid? ActivityOutcomeId, Guid? ActivityReasonId, Guid? ActivityPriorityId, Guid? CaseId,
            Guid PersonID, DateTime DueDate, Guid RegardingId, string RegardingIdName, string RegardingIdTableName,
            bool IsSignificantEvent, DateTime significanteventdate, Guid significanteventcategoryid, Guid significanteventsubcategoryid,
            bool InformationByThirdParty = false)
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

            if (CaseId.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "CaseId", CaseId.Value);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DueDate", DueDate);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "RegardingId", RegardingId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdName", RegardingIdName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdTableName", RegardingIdTableName);


            this.AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", 1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", IsSignificantEvent);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "significanteventdate", significanteventdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "significanteventcategoryid", significanteventcategoryid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "significanteventsubcategoryid", significanteventsubcategoryid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", InformationByThirdParty);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsCaseNote", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public void UpdateStatus(Guid TaskId, int statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, TaskId);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateInactive(Guid TaskId, bool Inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, TaskId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetTaskByHealthAppointmentID(Guid HealthAppointmentID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RegardingId", ConditionOperatorType.Equal, HealthAppointmentID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetTaskByRegardingID(Guid RegardingId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RegardingId", ConditionOperatorType.Equal, RegardingId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetTaskByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetTaskByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetTasksForPersonRecord(Guid PersonID, string TaskSubject)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.Tasks.Where(x => x.PersonId == PersonID && x.Subject == TaskSubject).Select(x => x.TaskId).ToList();
            }
        }

        public Dictionary<string, object> GetTaskByID(Guid TaskId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, TaskId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid? GetDataRestrictionForTask(Guid TaskID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.Tasks.Where(c => c.TaskId == TaskID).Select(x => x.DataRestrictionId).FirstOrDefault();

            }
        }

        public void RemoveTaskRestrictionFromDB(Guid TaskID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                var _record = entity.Tasks.Where(c => c.TaskId == TaskID).FirstOrDefault();
                _record.DataRestrictionId = null;
                entity.SaveChanges();
            }
        }

        public void DeleteTask(Guid TaskId)
        {
            this.DeleteRecord(TableName, TaskId);
        }

        public void UpdateResponsibleTeamOwner(Guid TaskId, Guid ownerid, Guid responsibleUserId, Guid RegardingId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, TaskId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", responsibleUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingId", RegardingId);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonIdAndSubject(Guid PersonID, string Subject)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ownerid", ConditionOperatorType.Equal, PersonID);
            this.BaseClassAddTableCondition(query, "subject", ConditionOperatorType.Equal, Subject);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetBySubject(string Subject)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "subject", ConditionOperatorType.Equal, Subject);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
