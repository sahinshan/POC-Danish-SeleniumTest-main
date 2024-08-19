using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class Letter : BaseClass
    {

        private string tableName = "Letter";
        private string primaryKeyName = "LetterId";

        public Letter()
        {
            AuthenticateUser();
        }

        public Letter(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetLetterByRegardingID(Guid RegardingId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "RegardingId", ConditionOperatorType.Equal, RegardingId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetLetterByPersonID(Guid PersonId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "regardingid", ConditionOperatorType.Equal, PersonId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetLetterByID(Guid LetterId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "LetterId", ConditionOperatorType.Equal, LetterId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid? GetDataRestrictionForLetter(Guid LetterID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.Letters.Where(c => c.LetterId == LetterID).Select(x => x.DataRestrictionId).FirstOrDefault();

            }
        }

        public void DeleteLetter(Guid LetterID)
        {
            this.DeleteRecord(tableName, LetterID);
        }

        public Guid CreateLetter(String SenderId, String SenderIdName, String SenderIDTableName, String Address, String RecipientId, String RecipientIdName, String RecipientIdTableName, Int16 DirectionId, String StatusId, string Subject, string Notes, Guid? CaseId,
            Guid OwnerId, Guid ResponsibleUserId, Guid? ActivityCategoryId, Guid? ActivitySubCategoryId, Guid? ActivityOutcomeId, Guid? ActivityReasonId,
            Guid? ActivityPriorityId, Guid PersonID, DateTime? LetterDate, Guid RegardingId, string RegardingIdName, string RegardingIdTableName,
            bool IsSignificantEvent, DateTime? significanteventdate, Guid? significanteventcategoryid, Guid? significanteventsubcategoryid, bool IsCaseNote = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "SenderId", SenderId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "SenderIdName", SenderIdName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "SenderIDTableName", SenderIDTableName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Address", Address);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecipientId", RecipientId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecipientIdName", RecipientIdName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecipientIdTableName", RecipientIdTableName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DirectionId", DirectionId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", StatusId);
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
            this.AddFieldToBusinessDataObject(buisinessDataObject, "LetterDate", LetterDate);

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
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsCaseNote", IsCaseNote);


            this.AddFieldToBusinessDataObject(buisinessDataObject, "Iscloned", false);
            // this.AddFieldToBusinessDataObject(buisinessDataObject, "ClonedfromId", ClonedfromId);

            return this.CreateRecord(buisinessDataObject);
        }
        //for chronology (issignificant Event true)
        public Guid CreateLetter(String SenderId, String SenderIdName, String SenderIDTableName, String Address, String RecipientId, String RecipientIdName, String RecipientIdTableName, Int16 DirectionId, String StatusId, string Subject, string Notes, Guid? CaseId,
            Guid OwnerId, Guid ResponsibleUserId, Guid ActivityCategoryId, Guid ActivitySubCategoryId, Guid ActivityOutcomeId, Guid ActivityReasonId,
            Guid ActivityPriorityId, Guid PersonID, DateTime LetterDate, Guid RegardingId, string RegardingIdName, string RegardingIdTableName,
            bool IsSignificantEvent, DateTime significanteventdate, Guid significanteventcategoryid, Guid significanteventsubcategoryid, bool IsCaseNote = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "SenderId", SenderId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "SenderIdName", SenderIdName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "SenderIDTableName", SenderIDTableName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Address", Address);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecipientId", RecipientId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecipientIdName", RecipientIdName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecipientIdTableName", RecipientIdTableName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DirectionId", DirectionId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", StatusId);
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
            this.AddFieldToBusinessDataObject(buisinessDataObject, "LetterDate", LetterDate);

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
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsCaseNote", IsCaseNote);


            this.AddFieldToBusinessDataObject(buisinessDataObject, "Iscloned", false);
            // this.AddFieldToBusinessDataObject(buisinessDataObject, "ClonedfromId", ClonedfromId);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreatePersonLetter(String RecipientId, Int16 DirectionId, String StatusId, String Subject, Guid OwnerId, Guid PersonID, Guid ResponsibleUserId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecipientId", RecipientId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DirectionId", DirectionId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", 1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Subject", Subject);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", ResponsibleUserId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "InformationByThirdParty", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsSignificantEvent", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateLetter(Guid? SenderId, String SenderIdName, String SenderIDTableName, String Address, Guid? RecipientId, String RecipientIdName, String RecipientIdTableName, Int16 DirectionId, String StatusId, string Subject, string Notes, Guid? CaseId,
            Guid OwnerId, Guid ResponsibleUserId, Guid? ActivityCategoryId, Guid? ActivitySubCategoryId, Guid? ActivityOutcomeId, Guid? ActivityReasonId,
            Guid? ActivityPriorityId, Guid PersonID, DateTime? LetterDate, Guid RegardingId, string RegardingIdName, string RegardingIdTableName,
            bool IsSignificantEvent, DateTime? significanteventdate, Guid? significanteventcategoryid, Guid? significanteventsubcategoryid, bool IsCaseNote = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "SenderId", SenderId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "SenderIdName", SenderIdName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "SenderIDTableName", SenderIDTableName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Address", Address);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecipientId", RecipientId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecipientIdName", RecipientIdName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecipientIdTableName", RecipientIdTableName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DirectionId", DirectionId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", StatusId);
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
            this.AddFieldToBusinessDataObject(buisinessDataObject, "LetterDate", LetterDate);

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
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsCaseNote", IsCaseNote);


            this.AddFieldToBusinessDataObject(buisinessDataObject, "Iscloned", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetLetterID(Guid personId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnFields(query, tableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, personId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }




    }
}
