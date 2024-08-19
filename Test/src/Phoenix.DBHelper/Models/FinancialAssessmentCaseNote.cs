using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinancialAssessmentCaseNote : BaseClass
    {

        public string TableName = "FinancialAssessmentCaseNote";
        public string PrimaryKeyName = "FinancialAssessmentCaseNoteId";


        public FinancialAssessmentCaseNote()
        {
            AuthenticateUser();
        }

        public FinancialAssessmentCaseNote(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public void CreateMultipleFinancialAssessmentCaseNoteRecords(int TotalRecordsToCreate, Guid ownerid, List<string> subjects, List<string> notesList,
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

        public void CreateMultipleFinancialAssessmentCaseNoteRecords(int TotalRecordsToCreate, Guid ownerid, List<string> subjects, List<string> notesList,
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

        public Guid CreateFinancialAssessmentCaseNote(Guid ownerid, string subject, string notes, Guid FinancialAssessmentId, Guid personid, DateTime casenotedate, Guid? responsibleuserid = null, int statusid = 1, bool inactive = false, bool InformationByThirdParty = false, bool IsSignificantEvent = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "subject", subject);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "notes", notes);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "FinancialAssessmentId", FinancialAssessmentId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "casenotedate", casenotedate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", InformationByThirdParty);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", IsSignificantEvent);


            return this.CreateRecord(buisinessDataObject);
        }


        public List<Guid> GetFinancialAssessmentCaseNoteByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid FinancialAssessmentCaseNoteId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, FinancialAssessmentCaseNoteId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonCaseNote(Guid PersonCaseNoteId)
        {
            this.DeleteRecord(TableName, PersonCaseNoteId);
        }



    }
}
