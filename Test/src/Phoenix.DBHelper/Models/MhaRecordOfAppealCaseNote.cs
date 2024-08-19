using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MhaRecordOfAppealCaseNote : BaseClass
    {
        public string TableName = "MhaRecordOfAppealCaseNote";
        public string PrimaryKeyName = "MhaRecordOfAppealCaseNoteId";


        public MhaRecordOfAppealCaseNote()
        {
            AuthenticateUser();
        }

        public MhaRecordOfAppealCaseNote(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateMhaRecordOfAppealCaseNote(Guid ownerid, Guid? responsibleuserid,
            string subject, string notes, Guid mharecordofappealid,
            Guid? activitycategoryid, Guid? activitysubcategoryid, Guid? activityoutcomeid, Guid? activityreasonid, Guid? activitypriorityid,
            bool issignificantevent, Guid? significanteventcategoryid, Guid? significanteventsubcategoryid,
            DateTime? significanteventdate, DateTime casenotedate, bool informationbythirdparty = false, int statusid = 1, bool iscloned = false, bool inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "subject", subject);
            AddFieldToBusinessDataObject(dataObject, "notes", notes);
            AddFieldToBusinessDataObject(dataObject, "mharecordofappealid", mharecordofappealid);
            AddFieldToBusinessDataObject(dataObject, "activitycategoryid", activitycategoryid);
            AddFieldToBusinessDataObject(dataObject, "activitysubcategoryid", activitysubcategoryid);
            AddFieldToBusinessDataObject(dataObject, "activityoutcomeid", activityoutcomeid);
            AddFieldToBusinessDataObject(dataObject, "activityreasonid", activityreasonid);
            AddFieldToBusinessDataObject(dataObject, "activitypriorityid", activitypriorityid);
            AddFieldToBusinessDataObject(dataObject, "issignificantevent", issignificantevent);
            AddFieldToBusinessDataObject(dataObject, "significanteventcategoryid", significanteventcategoryid);
            AddFieldToBusinessDataObject(dataObject, "significanteventsubcategoryid", significanteventsubcategoryid);
            AddFieldToBusinessDataObject(dataObject, "significanteventdate", significanteventdate);
            AddFieldToBusinessDataObject(dataObject, "casenotedate", casenotedate);
            AddFieldToBusinessDataObject(dataObject, "informationbythirdparty", informationbythirdparty);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "iscloned", iscloned);
            AddFieldToBusinessDataObject(dataObject, "inactive", inactive);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByMhaRecordOfAppealCaseNoteId(Guid MhaRecordOfAppealCaseNoteId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "MhaRecordOfAppealCaseNoteId", ConditionOperatorType.Equal, MhaRecordOfAppealCaseNoteId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid MhaRecordOfAppealCaseNoteId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, MhaRecordOfAppealCaseNoteId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteMhaRecordOfAppealCaseNote(Guid MhaRecordOfAppealCaseNoteId)
        {
            this.DeleteRecord(TableName, MhaRecordOfAppealCaseNoteId);
        }
    }
}
