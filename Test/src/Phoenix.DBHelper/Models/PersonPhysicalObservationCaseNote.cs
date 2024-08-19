using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class PersonPhysicalObservationCaseNote : BaseClass
    {

        public string TableName = "PersonPhysicalObservationCaseNote";
        public string PrimaryKeyName = "PersonPhysicalObservationCaseNoteId";


        public PersonPhysicalObservationCaseNote()
        {
            AuthenticateUser();
        }

        public PersonPhysicalObservationCaseNote(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        /// <summary>
        /// This method will create multiple person case notes records
        /// </summary>
        /// <param name="InitializationSeed"></param>
        /// <param name="Titles"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="DateOfBirth"></param>
        /// <param name="Ethnicity"></param>
        /// <param name="OwnerID"></param>
        /// <param name="AddressTypeId"></param>
        /// <param name="GenderId"></param>
        /// <returns></returns>
        public void CreateMultiplePhysicalObservationCaseNoteRecords(int TotalRecordsToCreate, Guid ownerid, List<string> subjects, List<string> notesList,
            Guid personid, DateTime MinCaseNoteDate, DateTime MaxCaseNoteDate, Guid responsibleuserid)
        {
            var allRecordsToCreate = new List<BusinessData>();

            //we cannot insert more than 1000 records at once
            if (TotalRecordsToCreate > 1000)
                TotalRecordsToCreate = 1000;

            var rnd = new Random();
            var totalSubjects = subjects.Count;
            var totalNotes = notesList.Count;
            var datesDifferenceInDays = (MaxCaseNoteDate - MinCaseNoteDate).Days;

            for (int i = 0; i < TotalRecordsToCreate; i++)
            {
                var subject = subjects[rnd.Next(0, totalSubjects)];
                var notes = notesList[rnd.Next(0, totalNotes)];
                var casenotedate = MinCaseNoteDate.AddDays(rnd.Next(0, datesDifferenceInDays));

                var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

                this.AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
                this.AddFieldToBusinessDataObject(dataObject, "subject", subject);
                this.AddFieldToBusinessDataObject(dataObject, "notes", notes);
                this.AddFieldToBusinessDataObject(dataObject, "personid", personid);
                this.AddFieldToBusinessDataObject(dataObject, "casenotedate", casenotedate);
                this.AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);

                this.AddFieldToBusinessDataObject(dataObject, "statusid", 1);
                this.AddFieldToBusinessDataObject(dataObject, "inactive", false);
                this.AddFieldToBusinessDataObject(dataObject, "InformationByThirdParty", false);
                this.AddFieldToBusinessDataObject(dataObject, "IsSignificantEvent", false);

                allRecordsToCreate.Add(dataObject);
            }


            this.CreateMultipleRecords(allRecordsToCreate);
        }

        public void CreateMultiplePhysicalObservationCaseNoteRecords(int TotalRecordsToCreate, Guid ownerid, List<string> subjects, List<string> notesList,
            List<Guid> personids, DateTime MinCaseNoteDate, DateTime MaxCaseNoteDate, List<Guid> responsibleuserids)
        {
            var allRecordsToCreate = new List<BusinessData>();

            //we cannot insert more than 1000 records at once
            if (TotalRecordsToCreate > 500)
                TotalRecordsToCreate = 500;

            var rnd = new Random();
            var datesDifferenceInDays = (MaxCaseNoteDate - MinCaseNoteDate).Days;

            for (int i = 0; i < TotalRecordsToCreate; i++)
            {
                var subject = subjects[rnd.Next(0, subjects.Count)];
                var notes = notesList[rnd.Next(0, notesList.Count)];
                var casenotedate = MinCaseNoteDate.AddDays(rnd.Next(0, datesDifferenceInDays));
                var responsibleuserid = responsibleuserids[rnd.Next(0, responsibleuserids.Count)];
                var personid = personids[rnd.Next(0, personids.Count)];

                var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

                this.AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
                this.AddFieldToBusinessDataObject(dataObject, "subject", subject);
                this.AddFieldToBusinessDataObject(dataObject, "notes", notes);
                this.AddFieldToBusinessDataObject(dataObject, "personid", personid);
                this.AddFieldToBusinessDataObject(dataObject, "casenotedate", casenotedate);
                this.AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);

                this.AddFieldToBusinessDataObject(dataObject, "statusid", 1);
                this.AddFieldToBusinessDataObject(dataObject, "inactive", false);
                this.AddFieldToBusinessDataObject(dataObject, "InformationByThirdParty", false);
                this.AddFieldToBusinessDataObject(dataObject, "IsSignificantEvent", false);

                allRecordsToCreate.Add(dataObject);
            }


            this.CreateMultipleRecords(allRecordsToCreate);
        }

        public Guid CreatePersonPhysicalObservationCaseNote(Guid ownerid, string subject, string notes, Guid PersonPhysicalObservationId, DateTime casenotedate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "subject", subject);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "notes", notes);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonPhysicalObservationId", PersonPhysicalObservationId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "casenotedate", casenotedate);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", 1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreatePersonCaseNote(Guid ownerid, string subject, string notes, Guid personid, DateTime casenotedate, Guid responsibleuserid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "subject", subject);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "notes", notes);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "casenotedate", casenotedate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", 1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonCaseNoteByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonCaseNoteByID(Guid PersonCaseNoteId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonCaseNoteId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid? GetDataRestrictionForPersonCaseNote(Guid PersonCaseNoteID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.PersonCaseNotes.Where(c => c.PersonCaseNoteId == PersonCaseNoteID).Select(x => x.DataRestrictionId).FirstOrDefault();

            }
        }

        public void DeletePersonCaseNote(Guid PersonCaseNoteId)
        {
            this.DeleteRecord(TableName, PersonCaseNoteId);
        }


        public Guid CreatePersonCaseNote(string Subject, string Notes, Guid OwnerId, Guid ResponsibleUserId, Guid ActivityCategoryId,
            Guid ActivitySubCategoryId, Guid ActivityOutcomeId, Guid ActivityReasonId, Guid ActivityPriorityId, Guid? CaseId,
            Guid PersonID, DateTime DueDate, Guid RegardingId, string RegardingIdName, string RegardingIdTableName,
            bool IsSignificantEvent, DateTime significanteventdate, Guid significanteventcategoryid, Guid significanteventsubcategoryid)
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


            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsCaseNote", false);

            return this.CreateRecord(buisinessDataObject);
        }
    }
}
