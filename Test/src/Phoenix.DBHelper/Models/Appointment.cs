using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class Appointment : BaseClass
    {

        public string TableName = "Appointment";
        public string PrimaryKeyName = "AppointmentId";


        public Appointment()
        {
            AuthenticateUser();
        }

        public Guid CreateAppointment(Guid ownerid, Guid personid, Guid? activitycategoryid, Guid? activitysubcategoryid, Guid? activityoutcomeid, Guid? activityreasonid, Guid? activitypriorityid, Guid? responsibleuserid, Guid? appointmenttypeid,
            string subject, string notes, string location,
            DateTime startdate, TimeSpan starttime, DateTime enddate, TimeSpan endtime,
            Guid regardingid, string regardingidtablename, string regardingidname,
            int statusid, int? showtimeasid,
            bool issignificantevent, DateTime? significanteventdate, Guid? significanteventcategoryid, Guid? significanteventsubcategoryid,
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
            AddFieldToBusinessDataObject(buisinessDataObject, "appointmenttypeid", appointmenttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "subject", subject);
            AddFieldToBusinessDataObject(buisinessDataObject, "notes", notes);
            AddFieldToBusinessDataObject(buisinessDataObject, "location", location);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
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

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateStaffReviewAppointment(Guid ownerid, Guid? activitycategoryid, Guid? activitysubcategoryid, Guid? activityoutcomeid, Guid? activityreasonid, Guid? activitypriorityid, Guid? responsibleuserid, Guid? appointmenttypeid,
            string subject, string notes, string location,
            DateTime startdate, TimeSpan starttime, DateTime enddate, TimeSpan endtime,
            Guid regardingid, string regardingidtablename, string regardingidname,
            int statusid, int showtimeasid,
            bool issignificantevent, DateTime? significanteventdate, Guid? significanteventcategoryid, Guid? significanteventsubcategoryid,
            bool informationbythirdparty = false, bool syncedwithmailbox = false, bool iscasenote = false, bool allowconcurrent = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activitycategoryid", activitycategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activitysubcategoryid", activitysubcategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activityoutcomeid", activityoutcomeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activityreasonid", activityreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "activitypriorityid", activitypriorityid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "appointmenttypeid", appointmenttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "subject", subject);
            AddFieldToBusinessDataObject(buisinessDataObject, "notes", notes);
            AddFieldToBusinessDataObject(buisinessDataObject, "location", location);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
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

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> CreateMultiplePersonAppointments(int TotalRecordsToCreate, Guid ownerid, List<Guid> personids, List<Guid> responsibleusersids,
            List<string> subjects, List<string> notesList, List<string> locations,
            DateTime startdate, TimeSpan starttime, DateTime enddate, TimeSpan endtime,
            int statusid, int showtimeasid)
        {
            var allRecordsToCreate = new List<BusinessData>();
            var rnd = new Random();

            for (int i = 0; i < TotalRecordsToCreate; i++)
            {
                var subject = subjects[rnd.Next(0, subjects.Count)];
                var notes = notesList[rnd.Next(0, notesList.Count)];
                var location = locations[rnd.Next(0, locations.Count)];
                var personid = personids[rnd.Next(0, personids.Count)];
                var responsibleuserid = responsibleusersids[rnd.Next(0, responsibleusersids.Count)];

                var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

                AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
                AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
                AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
                AddFieldToBusinessDataObject(buisinessDataObject, "subject", subject);
                AddFieldToBusinessDataObject(buisinessDataObject, "notes", notes);
                AddFieldToBusinessDataObject(buisinessDataObject, "location", location);
                AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
                AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
                AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
                AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
                AddFieldToBusinessDataObject(buisinessDataObject, "regardingid", personid);
                AddFieldToBusinessDataObject(buisinessDataObject, "regardingidtablename", "person");
                AddFieldToBusinessDataObject(buisinessDataObject, "regardingidname", "person name");
                AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
                AddFieldToBusinessDataObject(buisinessDataObject, "showtimeasid", showtimeasid);
                AddFieldToBusinessDataObject(buisinessDataObject, "issignificantevent", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "informationbythirdparty", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "syncedwithmailbox", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "iscasenote", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "allowconcurrent", true);

                allRecordsToCreate.Add(buisinessDataObject);
            }

            return this.CreateMultipleRecords(allRecordsToCreate);


        }

        public Appointment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByRecurringAppointmentId(Guid RecurringAppointmentId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RecurringAppointmentId", ConditionOperatorType.Equal, RecurringAppointmentId);

            query.Orders.Add(new OrderBy("StartDate", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAppointmentByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAppointmentByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAppointmentByRegardingID(Guid RegardingID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RegardingID", ConditionOperatorType.Equal, RegardingID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetAppointmentByID(Guid AppointmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AppointmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid? GetDataRestrictionForAppointment(Guid AppointmentID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.Appointments.Where(c => c.AppointmentId == AppointmentID).Select(x => x.DataRestrictionId).FirstOrDefault();

            }
        }

        public List<Guid> GetAppointmentsForPersonRecord(Guid PersonID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.Appointments.Where(x => x.RegardingId == PersonID && x.RegardingIdTableName == "person").OrderByDescending(c => c.CreatedOn).Select(x => x.AppointmentId).ToList();
            }
        }

        public void UpdateStatus(Guid AppointmentID, int statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, AppointmentID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteAppointment(Guid AppointmentId)
        {
            this.DeleteRecord(TableName, AppointmentId);
        }
    }
}
