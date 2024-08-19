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
    public class HealthAppointmentCaseNote : BaseClass
    {

        public string TableName = "HealthAppointmentCaseNote";
        public string PrimaryKeyName = "HealthAppointmentCaseNoteId";
        

        public HealthAppointmentCaseNote(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreateHealthAppointmentCaseNote(Guid ownerid, Guid responsibleuserid, Guid caseid, Guid personid, Guid healthappointmentid, Guid communitycliniccareinterventionid, Guid activitycategoryid, Guid activitysubcategoryid, Guid activityoutcomeid, Guid activityreasonid, Guid activitypriorityid,
            DateTime casenotedate, string subject, int statusid, string notes, bool informationbythirdparty, bool issignificantevent, bool inactive)
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
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetCaseNoteByHealthAppointmentID(Guid HealthAppointmentId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "healthappointmentid", ConditionOperatorType.Equal, HealthAppointmentId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetHealthAppointmentCaseNoteByID(Guid HealthAppointmentCaseNoteId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, HealthAppointmentCaseNoteId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteHealthAppointmentCaseNote(Guid HealthAppointmentCaseNoteId)
        {
            this.DeleteRecord(TableName, HealthAppointmentCaseNoteId);
        }
    }
}
