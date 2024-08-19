using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class HealthAppointmentCaseNote : BaseClass
    {

        public string TableName = "HealthAppointmentCaseNote";
        public string PrimaryKeyName = "HealthAppointmentCaseNoteId";
        public HealthAppointmentCaseNote()
        {
            AuthenticateUser();
        }

        public HealthAppointmentCaseNote(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateHealthAppointmentCaseNote(Guid ownerid, Guid responsibleuserid, Guid caseid, Guid personid, Guid healthappointmentid, Guid communitycliniccareinterventionid, Guid? activitycategoryid, Guid? activitysubcategoryid, Guid? activityoutcomeid, Guid? activityreasonid, Guid? activitypriorityid,
            DateTime casenotedate, string subject, int statusid, string notes, bool informationbythirdparty, bool issignificantevent, bool inactive,
            Guid? SignificantEventCategoryId, Guid? SignificantEventSubCategoryId, DateTime? SignificantEventDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmentid", healthappointmentid);
            AddFieldToBusinessDataObject(buisinessDataObject, "communitycliniccareinterventionid", communitycliniccareinterventionid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activitycategoryid", activitycategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activitysubcategoryid", activitysubcategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activityoutcomeid", activityoutcomeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activityreasonid", activityreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activitypriorityid", activitypriorityid);
            AddFieldToBusinessDataObject(buisinessDataObject, "casenotedate", casenotedate);
            AddFieldToBusinessDataObject(buisinessDataObject, "subject", subject);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "notes", notes);
            AddFieldToBusinessDataObject(buisinessDataObject, "informationbythirdparty", informationbythirdparty);
            AddFieldToBusinessDataObject(buisinessDataObject, "issignificantevent", issignificantevent);

            AddFieldToBusinessDataObject(buisinessDataObject, "SignificantEventCategoryId", SignificantEventCategoryId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SignificantEventSubCategoryId", SignificantEventSubCategoryId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SignificantEventDate", SignificantEventDate);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetCaseNoteByHealthAppointmentID(Guid HealthAppointmentId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "healthappointmentid", ConditionOperatorType.Equal, HealthAppointmentId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetHealthAppointmentCaseNoteByID(Guid HealthAppointmentCaseNoteId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, HealthAppointmentCaseNoteId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetHealthAppointmentCaseNoteByPersonId(Guid personid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteHealthAppointmentCaseNote(Guid HealthAppointmentCaseNoteId)
        {
            this.DeleteRecord(TableName, HealthAppointmentCaseNoteId);
        }
    }
}
