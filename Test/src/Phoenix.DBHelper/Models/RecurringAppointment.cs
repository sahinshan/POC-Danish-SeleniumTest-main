using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RecurringAppointment : BaseClass
    {

        public string TableName = "RecurringAppointment";
        public string PrimaryKeyName = "RecurringAppointmentId";


        public RecurringAppointment()
        {
            AuthenticateUser();
        }

        public Guid CreateRecurringAppointment(Guid ownerid, Guid personid, Guid? activitycategoryid, Guid? activitysubcategoryid, Guid? activityoutcomeid, Guid? activityreasonid, Guid? activitypriorityid, Guid? responsibleuserid, Guid? RecurringAppointmenttypeid,
            string subject, string notes, string location,
            DateTime rangestartdate, TimeSpan starttime, DateTime rangeenddate, TimeSpan endtime,
            Guid regardingid, string regardingidtablename, string regardingidname,
            int statusid, int showtimeasid,
            bool issignificantevent, DateTime? significanteventdate, Guid? significanteventcategoryid, Guid? significanteventsubcategoryid,
            Guid recurrencepatternid, int endrangeid, DateTime firstappointmentdate, DateTime lastappointmentdate, Dictionary<Guid, string> RequiredAttendeedsSystemUsers,
            bool informationbythirdparty = false, bool syncedwithmailbox = false, bool iscasenote = false, bool allowconcurrent = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activitycategoryid", activitycategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activitysubcategoryid", activitysubcategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activityoutcomeid", activityoutcomeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activityreasonid", activityreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activitypriorityid", activitypriorityid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "RecurringAppointmenttypeid", RecurringAppointmenttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "subject", subject);
            AddFieldToBusinessDataObject(buisinessDataObject, "notes", notes);
            AddFieldToBusinessDataObject(buisinessDataObject, "location", location);
            AddFieldToBusinessDataObject(buisinessDataObject, "rangestartdate", rangestartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "rangeenddate", rangeenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "showtimeasid", showtimeasid);
            AddFieldToBusinessDataObject(buisinessDataObject, "issignificantevent", issignificantevent);
            AddFieldToBusinessDataObject(buisinessDataObject, "significanteventcategoryid", significanteventcategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "significanteventsubcategoryid", significanteventsubcategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "significanteventdate", significanteventdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "informationbythirdparty", informationbythirdparty);
            AddFieldToBusinessDataObject(buisinessDataObject, "syncedwithmailbox", syncedwithmailbox);
            AddFieldToBusinessDataObject(buisinessDataObject, "iscasenote", iscasenote);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowconcurrent", allowconcurrent);

            AddFieldToBusinessDataObject(buisinessDataObject, "recurrencepatternid", recurrencepatternid);
            AddFieldToBusinessDataObject(buisinessDataObject, "endrangeid", endrangeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "firstappointmentdate", firstappointmentdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "lastappointmentdate", lastappointmentdate);

            buisinessDataObject.MultiSelectMultiLookupFields = new MultiSelectBusinessObjectDataDictionary();
            buisinessDataObject.MultiSelectMultiLookupFields["requiredattendees"] = new MultiSelectBusinessObjectDataCollection();

            if (RequiredAttendeedsSystemUsers != null && RequiredAttendeedsSystemUsers.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> systemUserInfo in RequiredAttendeedsSystemUsers)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = systemUserInfo.Key,
                        ReferenceIdTableName = "systemuser",
                        ReferenceName = systemUserInfo.Value,
                        Id = Guid.NewGuid()
                    };
                    buisinessDataObject.MultiSelectMultiLookupFields["requiredattendees"].Add(dataRecord);
                }
            }

            buisinessDataObject.MultiSelectMultiLookupFields["optionalattendees"] = new MultiSelectBusinessObjectDataCollection();

            return this.CreateRecord(buisinessDataObject);
        }

        public RecurringAppointment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByRegardingID(Guid RegardingID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RegardingID", ConditionOperatorType.Equal, RegardingID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid RecurringAppointmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, RecurringAppointmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteRecurringAppointment(Guid RecurringAppointmentId)
        {
            this.DeleteRecord(TableName, RecurringAppointmentId);
        }
    }
}
