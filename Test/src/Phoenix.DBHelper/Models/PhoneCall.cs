using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class PhoneCall : BaseClass
    {

        private string tableName = "PhoneCall";
        private string primaryKeyName = "PhoneCallId";

        public PhoneCall()
        {
            AuthenticateUser();
        }

        public PhoneCall(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePhoneCallRecordForPerson(string Subject, string Description,
            Guid? CallerID, string CallerIdTableName, string CallerIDName,
            Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
            string PhoneNumber,
            Guid RegardingID, string RegardingIDName, Guid Ownerid, DateTime? PhoneCallDate, Guid? ResponsibleUser,
            Guid? ActivityCategoryID = null, bool InformationByThirdParty = false, int DirectionId = 1,
            bool IsSignificantEvent = false, DateTime? SignificantEventDate = null, Guid? SignificantEventCategoryId = null, bool IsCaseNote = false, Guid? significanteventsubcategoryid = null)
        {
            var businessDataObject = GetBusinessDataBaseObject("PhoneCall", "PhoneCallId");

            AddFieldToBusinessDataObject(businessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(businessDataObject, "Notes", Description);

            AddFieldToBusinessDataObject(businessDataObject, "CallerId", CallerID);
            AddFieldToBusinessDataObject(businessDataObject, "CallerIdTableName", CallerIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "CallerIdName", CallerIDName);

            AddFieldToBusinessDataObject(businessDataObject, "RecipientId", RecipientId);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdTableName", RecipientIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdName", RecipientIdName);

            AddFieldToBusinessDataObject(businessDataObject, "RegardingId", RegardingID);
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdTableName", "person");
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdName", RegardingIDName);

            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", Ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "ResponsibleUserId", ResponsibleUser);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "InformationByThirdParty", InformationByThirdParty);
            AddFieldToBusinessDataObject(businessDataObject, "IsSignificantEvent", IsSignificantEvent);
            AddFieldToBusinessDataObject(businessDataObject, "IsCaseNote", IsCaseNote);

            AddFieldToBusinessDataObject(businessDataObject, "PhoneNumber", PhoneNumber);

            AddFieldToBusinessDataObject(businessDataObject, "DirectionId", DirectionId);
            AddFieldToBusinessDataObject(businessDataObject, "StatusId", 1);

            AddFieldToBusinessDataObject(businessDataObject, "PersonID", RegardingID);
            AddFieldToBusinessDataObject(businessDataObject, "PersonID_cwname", RegardingIDName);

            if (PhoneCallDate.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", PhoneCallDate.Value);
            else
                AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", null);

            AddFieldToBusinessDataObject(businessDataObject, "ActivityCategoryId", ActivityCategoryID);

            if (SignificantEventDate.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "SignificantEventDate", SignificantEventDate.Value);
            else
                AddFieldToBusinessDataObject(businessDataObject, "SignificantEventDate", null);

            if (SignificantEventCategoryId.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "SignificantEventCategoryId", SignificantEventCategoryId.Value);

            if (SignificantEventCategoryId.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "significanteventsubcategoryid", significanteventsubcategoryid.Value);


            return CreateRecord(businessDataObject);
        }

        public Guid CreatePhoneCallRecordForPerson(string Subject, string Description,
            Guid CallerID, string CallerIdTableName, string CallerIDName,
            Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
            string PhoneNumber,
            Guid RegardingID, string RegardingIDName, Guid Ownerid, Guid responsibleuserid, DateTime? PhoneCallDate,
            Guid? activityreasonid, Guid? activitypriorityid, Guid? activitycategoryid, Guid? activitysubcategoryid, Guid? activityoutcomeid,
            string RegardingIdTableName = "person", bool? InformationByThirdParty = false, bool? IsCaseNote = false,
            bool? IsSignificantEvent = null, DateTime? SignificantEventDate = null, Guid? SignificantEventCategoryId = null, Guid? significanteventsubcategoryid = null)
        {
            var businessDataObject = GetBusinessDataBaseObject("PhoneCall", "PhoneCallId");

            AddFieldToBusinessDataObject(businessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(businessDataObject, "Notes", Description);

            AddFieldToBusinessDataObject(businessDataObject, "CallerId", CallerID);
            AddFieldToBusinessDataObject(businessDataObject, "CallerIdTableName", CallerIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "CallerIdName", CallerIDName);

            AddFieldToBusinessDataObject(businessDataObject, "RecipientId", RecipientId);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdTableName", RecipientIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdName", RecipientIdName);

            AddFieldToBusinessDataObject(businessDataObject, "RegardingId", RegardingID);
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdTableName", RegardingIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdName", RegardingIDName);

            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", Ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "responsibleuserid", responsibleuserid);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "InformationByThirdParty", InformationByThirdParty);
            AddFieldToBusinessDataObject(businessDataObject, "IsCaseNote", IsCaseNote);

            AddFieldToBusinessDataObject(businessDataObject, "PhoneNumber", PhoneNumber);

            AddFieldToBusinessDataObject(businessDataObject, "activityreasonid", activityreasonid);
            AddFieldToBusinessDataObject(businessDataObject, "activitypriorityid", activitypriorityid);
            AddFieldToBusinessDataObject(businessDataObject, "activitycategoryid", activitycategoryid);
            AddFieldToBusinessDataObject(businessDataObject, "activitysubcategoryid", activitysubcategoryid);
            AddFieldToBusinessDataObject(businessDataObject, "activityoutcomeid", activityoutcomeid);

            AddFieldToBusinessDataObject(businessDataObject, "IsSignificantEvent", IsSignificantEvent);
            AddFieldToBusinessDataObject(businessDataObject, "SignificantEventDate", SignificantEventDate);
            AddFieldToBusinessDataObject(businessDataObject, "SignificantEventCategoryId", SignificantEventCategoryId);
            AddFieldToBusinessDataObject(businessDataObject, "significanteventsubcategoryid", significanteventsubcategoryid);


            AddFieldToBusinessDataObject(businessDataObject, "DirectionId", 1);
            AddFieldToBusinessDataObject(businessDataObject, "StatusId", 1);

            if (PhoneCallDate.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", PhoneCallDate.Value);
            else
                AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", null);

            return CreateRecord(businessDataObject);
        }

        public Guid CreatePhoneCallRecordForPerson(string Subject, string Description,
            Guid CallerID, string CallerIdTableName, string CallerIDName,
            Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
            string PhoneNumber,
            Guid RegardingID, string RegardingIDName, Guid Ownerid, DateTime? PhoneCallDate, string RegardingIdTableName = "person")
        {
            var businessDataObject = GetBusinessDataBaseObject("PhoneCall", "PhoneCallId");

            AddFieldToBusinessDataObject(businessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(businessDataObject, "Notes", Description);

            AddFieldToBusinessDataObject(businessDataObject, "CallerId", CallerID);
            AddFieldToBusinessDataObject(businessDataObject, "CallerIdTableName", CallerIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "CallerIdName", CallerIDName);

            AddFieldToBusinessDataObject(businessDataObject, "RecipientId", RecipientId);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdTableName", RecipientIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdName", RecipientIdName);

            AddFieldToBusinessDataObject(businessDataObject, "RegardingId", RegardingID);
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdTableName", RegardingIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdName", RegardingIDName);

            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", Ownerid);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "InformationByThirdParty", false);
            AddFieldToBusinessDataObject(businessDataObject, "IsSignificantEvent", false);
            AddFieldToBusinessDataObject(businessDataObject, "IsCaseNote", false);

            AddFieldToBusinessDataObject(businessDataObject, "PhoneNumber", PhoneNumber);

            AddFieldToBusinessDataObject(businessDataObject, "DirectionId", 1);
            AddFieldToBusinessDataObject(businessDataObject, "StatusId", 1);

            if (PhoneCallDate.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", PhoneCallDate.Value);
            else
                AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", null);

            return CreateRecord(businessDataObject);
        }

        public Guid CreatePhoneCallRecordForPerson(string Subject, string Description,
            Guid CallerID, string CallerIdTableName, string CallerIDName,
            Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
            string PhoneNumber,
            Guid RegardingID, string RegardingIDName, Guid Ownerid, DateTime? PhoneCallDate, Guid? ResponsibleUser,
            Guid? ActivityCategoryID = null, bool InformationByThirdParty = false, int DirectionId = 1,
            bool IsSignificantEvent = false, DateTime? SignificantEventDate = null, Guid? SignificantEventCategoryId = null, bool IsCaseNote = false)
        {
            var businessDataObject = GetBusinessDataBaseObject("PhoneCall", "PhoneCallId");

            AddFieldToBusinessDataObject(businessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(businessDataObject, "Notes", Description);

            AddFieldToBusinessDataObject(businessDataObject, "CallerId", CallerID);
            AddFieldToBusinessDataObject(businessDataObject, "CallerIdTableName", CallerIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "CallerIdName", CallerIDName);

            AddFieldToBusinessDataObject(businessDataObject, "RecipientId", RecipientId);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdTableName", RecipientIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdName", RecipientIdName);

            AddFieldToBusinessDataObject(businessDataObject, "RegardingId", RegardingID);
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdTableName", "person");
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdName", RegardingIDName);

            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", Ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "ResponsibleUserId", ResponsibleUser);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "InformationByThirdParty", InformationByThirdParty);
            AddFieldToBusinessDataObject(businessDataObject, "IsSignificantEvent", IsSignificantEvent);
            AddFieldToBusinessDataObject(businessDataObject, "IsCaseNote", IsCaseNote);

            AddFieldToBusinessDataObject(businessDataObject, "PhoneNumber", PhoneNumber);

            AddFieldToBusinessDataObject(businessDataObject, "DirectionId", DirectionId);
            AddFieldToBusinessDataObject(businessDataObject, "StatusId", 1);

            AddFieldToBusinessDataObject(businessDataObject, "PersonID", RegardingID);
            AddFieldToBusinessDataObject(businessDataObject, "PersonID_cwname", RegardingIDName);

            if (PhoneCallDate.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", PhoneCallDate.Value);
            else
                AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", null);

            AddFieldToBusinessDataObject(businessDataObject, "ActivityCategoryId", ActivityCategoryID);

            if (SignificantEventDate.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "SignificantEventDate", SignificantEventDate.Value);
            else
                AddFieldToBusinessDataObject(businessDataObject, "SignificantEventDate", null);

            if (SignificantEventCategoryId.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "SignificantEventCategoryId", SignificantEventCategoryId.Value);

            return CreateRecord(businessDataObject);
        }

        public Guid CreatePhoneCallRecordForPerson(string Subject, string Description,
            Guid CallerID, string CallerIdTableName, string CallerIDName,
            Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
            string PhoneNumber,
            Guid RegardingID, string RegardingIDName, Guid Ownerid, string RegardingIdTableName = "person")
        {
            var currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            return CreatePhoneCallRecordForPerson(Subject, Description, CallerID, CallerIdTableName, CallerIDName, RecipientId, RecipientIdTableName, RecipientIdName, PhoneNumber, RegardingID, RegardingIDName, Ownerid, currentDate, RegardingIdTableName);
        }

        public Guid CreatePhoneCallRecordForCase(string Subject, string Description,
           Guid CallerID, string CallerIdTableName, string CallerIDName,
           Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
           string PhoneNumber,
           Guid RegardingID, string RegardingIDName, Guid PersonID, string PersonName, Guid Ownerid, string OwnerIDName,
           DateTime? PhoneCallDate, Guid? ResponsibleUser, string ResponsibleUserName)
        {
            var businessDataObject = GetBusinessDataBaseObject("PhoneCall", "PhoneCallId");

            AddFieldToBusinessDataObject(businessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(businessDataObject, "Notes", Description);

            AddFieldToBusinessDataObject(businessDataObject, "CallerId", CallerID);
            AddFieldToBusinessDataObject(businessDataObject, "CallerIdTableName", CallerIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "CallerIdName", CallerIDName);

            AddFieldToBusinessDataObject(businessDataObject, "RecipientId", RecipientId);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdTableName", RecipientIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdName", RecipientIdName);

            AddFieldToBusinessDataObject(businessDataObject, "RegardingId", RegardingID);
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdTableName", "case");
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdName", RegardingIDName);

            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", Ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "ownerid_cwname", OwnerIDName);
            AddFieldToBusinessDataObject(businessDataObject, "ResponsibleUserId", ResponsibleUser);
            AddFieldToBusinessDataObject(businessDataObject, "responsibleuserid_cwname", ResponsibleUserName);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "InformationByThirdParty", false);
            AddFieldToBusinessDataObject(businessDataObject, "IsSignificantEvent", false);
            AddFieldToBusinessDataObject(businessDataObject, "IsCaseNote", false);

            AddFieldToBusinessDataObject(businessDataObject, "PhoneNumber", PhoneNumber);

            AddFieldToBusinessDataObject(businessDataObject, "DirectionId", 1);
            AddFieldToBusinessDataObject(businessDataObject, "StatusId", 1);

            AddFieldToBusinessDataObject(businessDataObject, "CaseID", RegardingID);

            AddFieldToBusinessDataObject(businessDataObject, "PersonID", PersonID);
            AddFieldToBusinessDataObject(businessDataObject, "personid_cwname", PersonName);

            if (PhoneCallDate.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", PhoneCallDate.Value);
            else
                AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", null);

            return CreateRecord(businessDataObject);
        }

        public Guid CreatePhoneCallRecordForCase(Guid OwnerId, Guid ResponsibleUserId, string Subject, Guid RegardingID, string RegardingIDName, string RegardingIdTableName, string Notes,
          Guid? ActivityCategoryId, Guid? ActivitySubCategoryId, Guid? ActivityOutcomeId, Guid? ActivityReasonId, Guid? ActivityPriorityId,
          Guid RecipientId, string RecipientIdTableName, string RecipientIdName, int DirectionId, DateTime? PhoneCallDate, int StatusId, Guid PersonId, Guid CaseId)
        {
            var businessDataObject = GetBusinessDataBaseObject("PhoneCall", "PhoneCallId");

            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(businessDataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(businessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(businessDataObject, "RegardingID", RegardingID);

            AddFieldToBusinessDataObject(businessDataObject, "RegardingIDName", RegardingIDName);
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdTableName", RegardingIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "Notes", Notes);

            AddFieldToBusinessDataObject(businessDataObject, "ActivityCategoryId", ActivityCategoryId);
            AddFieldToBusinessDataObject(businessDataObject, "ActivitySubCategoryId", ActivitySubCategoryId);
            AddFieldToBusinessDataObject(businessDataObject, "ActivityOutcomeId", ActivityOutcomeId);

            AddFieldToBusinessDataObject(businessDataObject, "ActivityReasonId", ActivityReasonId);
            AddFieldToBusinessDataObject(businessDataObject, "ActivityPriorityId", ActivityPriorityId);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientId", RecipientId);

            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdTableName", RecipientIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdName", RecipientIdName);
            AddFieldToBusinessDataObject(businessDataObject, "DirectionId", DirectionId);
            AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", PhoneCallDate);
            AddFieldToBusinessDataObject(businessDataObject, "StatusId", StatusId);
            AddFieldToBusinessDataObject(businessDataObject, "PersonId", PersonId);

            AddFieldToBusinessDataObject(businessDataObject, "CaseId", CaseId);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "InformationByThirdParty", false);
            AddFieldToBusinessDataObject(businessDataObject, "IsSignificantEvent", false);
            AddFieldToBusinessDataObject(businessDataObject, "IsCaseNote", false);

            return CreateRecord(businessDataObject);
        }

        public List<Guid> CreateMultiplePersonPhoneCallRecords(int TotalRecordsToCreate, List<string> Subjects, List<string> Descriptions,
            List<Guid> PersonIds, string PhoneNumber,
            Guid Ownerid, List<Guid> responsibleusersids, DateTime? PhoneCallDate)
        {
            var allRecordsToCreate = new List<BusinessData>();
            var rnd = new Random();

            for (int i = 0; i < TotalRecordsToCreate; i++)
            {
                var Subject = Subjects[rnd.Next(0, Subjects.Count)];
                var Description = Descriptions[rnd.Next(0, Descriptions.Count)];
                var CallerID = PersonIds[rnd.Next(0, PersonIds.Count)];
                var RegardingID = PersonIds[rnd.Next(0, PersonIds.Count)];
                var RecipientId = responsibleusersids[rnd.Next(0, responsibleusersids.Count)];

                var businessDataObject = GetBusinessDataBaseObject("PhoneCall", "PhoneCallId");

                AddFieldToBusinessDataObject(businessDataObject, "Subject", Subject);
                AddFieldToBusinessDataObject(businessDataObject, "Notes", Description);

                AddFieldToBusinessDataObject(businessDataObject, "CallerId", CallerID);
                AddFieldToBusinessDataObject(businessDataObject, "CallerIdTableName", "person");
                AddFieldToBusinessDataObject(businessDataObject, "CallerIdName", "person name");

                AddFieldToBusinessDataObject(businessDataObject, "RecipientId", RecipientId);
                AddFieldToBusinessDataObject(businessDataObject, "RecipientIdTableName", "systemuser");
                AddFieldToBusinessDataObject(businessDataObject, "RecipientIdName", "user name");

                AddFieldToBusinessDataObject(businessDataObject, "RegardingId", RegardingID);
                AddFieldToBusinessDataObject(businessDataObject, "RegardingIdTableName", "person");
                AddFieldToBusinessDataObject(businessDataObject, "RegardingIdName", "person name");

                AddFieldToBusinessDataObject(businessDataObject, "OwnerId", Ownerid);

                AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);
                AddFieldToBusinessDataObject(businessDataObject, "InformationByThirdParty", false);
                AddFieldToBusinessDataObject(businessDataObject, "IsSignificantEvent", false);
                AddFieldToBusinessDataObject(businessDataObject, "IsCaseNote", false);

                AddFieldToBusinessDataObject(businessDataObject, "PhoneNumber", PhoneNumber);

                AddFieldToBusinessDataObject(businessDataObject, "DirectionId", 1);
                AddFieldToBusinessDataObject(businessDataObject, "StatusId", 1);

                if (PhoneCallDate.HasValue)
                    AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", PhoneCallDate.Value);
                else
                    AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", null);

                allRecordsToCreate.Add(businessDataObject);
            }

            return CreateMultipleRecords(allRecordsToCreate);
        }

        public void UpdateStatus(Guid PhoneCallId, int Statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PhoneCallId);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", Statusid);

            UpdateRecord(buisinessDataObject);
        }

        public void UpdateSubject(Guid PhoneCallId, string Subject)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PhoneCallId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Subject", Subject);

            UpdateRecord(buisinessDataObject);
        }

        public void UpdateSubject(Guid PhoneCallId, string Subject, Guid RegardingID, string RegardingIDName, string RegardingIdTableName)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PhoneCallId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Subject", Subject);

            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingID", RegardingID);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIDName", RegardingIDName);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdTableName", RegardingIdTableName);

            UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetPhoneCallByRegardingID(Guid RegardingId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "RegardingId", ConditionOperatorType.Equal, RegardingId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetPhoneCallByPersonID(Guid PersonId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetPhoneCallByPersonID(Guid PersonId, string Subject)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);
            this.BaseClassAddTableCondition(query, "Subject", ConditionOperatorType.Equal, Subject);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetPhoneCallByID(Guid PhoneCallId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PhoneCallId", ConditionOperatorType.Equal, PhoneCallId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid? GetDataRestrictionForPhoneCall(Guid PhoneCallID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.PhoneCalls.Where(c => c.PhoneCallId == PhoneCallID).Select(x => x.DataRestrictionId).FirstOrDefault();

            }
        }

        public void UpdatePhoneCallRecordDescription(Guid PhoneCallID, string notes)
        {
            var businessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, primaryKeyName, PhoneCallID);
            AddFieldToBusinessDataObject(businessDataObject, "notes", notes);

            UpdateRecord(businessDataObject);
        }

        public void UpdatePhoneCallDateField(Guid PhoneCallID, DateTime PhoneCallDate)
        {
            var businessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, primaryKeyName, PhoneCallID);
            AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", PhoneCallDate);

            UpdateRecord(businessDataObject);
        }

        public void UpdatePhoneCallSubject(Guid PhoneCallID, string subject)
        {
            var businessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, primaryKeyName, PhoneCallID);
            AddFieldToBusinessDataObject(businessDataObject, "subject", subject);

            UpdateRecord(businessDataObject);
        }

        public void DeletePhoneCall(Guid PhoneCallID)
        {
            this.DeleteRecord(tableName, PhoneCallID);
        }

        public Guid CreatePhoneCallRecordForPerson(string Subject, string Description,
            Guid CallerID, string CallerIdTableName, string CallerIDName,
            Guid RecipientId, string RecipientIdTableName, string RecipientIdName,
            string PhoneNumber,
            Guid RegardingID, string RegardingIDName, Guid Ownerid, DateTime? PhoneCallDate, Guid? ResponsibleUser, string ResponsibleUserName,
            Guid? ActivityCategoryID = null, bool InformationByThirdParty = false, int DirectionId = 1,
            bool IsSignificantEvent = false, DateTime? SignificantEventDate = null, Guid? SignificantEventCategoryId = null, bool IsCaseNote = false)
        {
            var businessDataObject = GetBusinessDataBaseObject("PhoneCall", "PhoneCallId");

            AddFieldToBusinessDataObject(businessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(businessDataObject, "Notes", Description);

            AddFieldToBusinessDataObject(businessDataObject, "CallerId", CallerID);
            AddFieldToBusinessDataObject(businessDataObject, "CallerIdTableName", CallerIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "CallerIdName", CallerIDName);

            AddFieldToBusinessDataObject(businessDataObject, "RecipientId", RecipientId);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdTableName", RecipientIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdName", RecipientIdName);

            AddFieldToBusinessDataObject(businessDataObject, "RegardingId", RegardingID);
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdTableName", "person");
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdName", RegardingIDName);

            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", Ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "ResponsibleUserId", ResponsibleUser);
            AddFieldToBusinessDataObject(businessDataObject, "ResponsibleUserId_cwname", ResponsibleUserName);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "InformationByThirdParty", InformationByThirdParty);
            AddFieldToBusinessDataObject(businessDataObject, "IsSignificantEvent", IsSignificantEvent);
            AddFieldToBusinessDataObject(businessDataObject, "IsCaseNote", IsCaseNote);

            AddFieldToBusinessDataObject(businessDataObject, "PhoneNumber", PhoneNumber);

            AddFieldToBusinessDataObject(businessDataObject, "DirectionId", DirectionId);
            AddFieldToBusinessDataObject(businessDataObject, "StatusId", 1);

            AddFieldToBusinessDataObject(businessDataObject, "PersonID", RegardingID);
            AddFieldToBusinessDataObject(businessDataObject, "PersonID_cwname", RegardingIDName);

            if (PhoneCallDate.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", PhoneCallDate.Value);
            else
                AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", null);

            AddFieldToBusinessDataObject(businessDataObject, "ActivityCategoryId", ActivityCategoryID);

            if (SignificantEventDate.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "SignificantEventDate", SignificantEventDate.Value);
            else
                AddFieldToBusinessDataObject(businessDataObject, "SignificantEventDate", null);

            if (SignificantEventCategoryId.HasValue)
                AddFieldToBusinessDataObject(businessDataObject, "SignificantEventCategoryId", SignificantEventCategoryId.Value);

            return CreateRecord(businessDataObject);
        }

        public Guid CreatePhoneCallRecord(Guid OwnerId, Guid ResponsibleUserId, string Subject, string Notes, Guid RegardingID, string RegardingIDName, string RegardingIdTableName,
          Guid? ActivityCategoryId, Guid? ActivitySubCategoryId, Guid? ActivityOutcomeId, Guid? ActivityReasonId, Guid? ActivityPriorityId,
          Guid RecipientId, string RecipientIdTableName, string RecipientIdName, int DirectionId, DateTime? PhoneCallDate, int StatusId, bool IsCaseNote = false)
        {
            var businessDataObject = GetBusinessDataBaseObject("PhoneCall", "PhoneCallId");

            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(businessDataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(businessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(businessDataObject, "RegardingID", RegardingID);

            AddFieldToBusinessDataObject(businessDataObject, "RegardingIDName", RegardingIDName);
            AddFieldToBusinessDataObject(businessDataObject, "RegardingIdTableName", RegardingIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "Notes", Notes);

            AddFieldToBusinessDataObject(businessDataObject, "ActivityCategoryId", ActivityCategoryId);
            AddFieldToBusinessDataObject(businessDataObject, "ActivitySubCategoryId", ActivitySubCategoryId);
            AddFieldToBusinessDataObject(businessDataObject, "ActivityOutcomeId", ActivityOutcomeId);

            AddFieldToBusinessDataObject(businessDataObject, "ActivityReasonId", ActivityReasonId);
            AddFieldToBusinessDataObject(businessDataObject, "ActivityPriorityId", ActivityPriorityId);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientId", RecipientId);

            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdTableName", RecipientIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "RecipientIdName", RecipientIdName);
            AddFieldToBusinessDataObject(businessDataObject, "DirectionId", DirectionId);
            AddFieldToBusinessDataObject(businessDataObject, "PhoneCallDate", PhoneCallDate);
            AddFieldToBusinessDataObject(businessDataObject, "StatusId", StatusId);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "InformationByThirdParty", false);
            AddFieldToBusinessDataObject(businessDataObject, "IsSignificantEvent", false);
            AddFieldToBusinessDataObject(businessDataObject, "IsCaseNote", IsCaseNote);

            return CreateRecord(businessDataObject);
        }
    }
}