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
    public class Appointment : BaseClass
    {

        public string TableName = "Appointment";
        public string PrimaryKeyName = "AppointmentId";
        

        public Appointment(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreateAppointment(string subject, Guid ownerid, Guid? responsibleuserid, Guid? activitycategoryid, Guid? activitysubcategoryid, Guid? activityoutcomeid, Guid? activityreasonid, Guid? activitypriorityid, Guid personid, Guid? caseid, Guid? appointmenttypeid, 
            Guid regardingid, string regardingidname, string regardingidtablename, string notes, string location,
            DateTime startdate, TimeSpan starttime, DateTime enddate, TimeSpan endtime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "subject", subject);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            if (responsibleuserid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid.Value);
            
            if (activitycategoryid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "activitycategoryid", activitycategoryid.Value);

            if (activitysubcategoryid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "activitysubcategoryid", activitysubcategoryid.Value);

            if (activityoutcomeid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "activityoutcomeid", activityoutcomeid.Value);
            
            if (activityreasonid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "activityreasonid", activityreasonid.Value);

            if(activitypriorityid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "activitypriorityid", activitypriorityid.Value);

            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);

            if(caseid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "caseid", caseid.Value);

            if (appointmenttypeid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "appointmenttypeid", appointmenttypeid.Value);

            AddFieldToBusinessDataObject(buisinessDataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidtablename", regardingidtablename);

            AddFieldToBusinessDataObject(buisinessDataObject, "notes", notes);
            AddFieldToBusinessDataObject(buisinessDataObject, "location", location);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);

            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", 4);
            AddFieldToBusinessDataObject(buisinessDataObject, "showtimeasid", 5);
            AddFieldToBusinessDataObject(buisinessDataObject, "syncedwithmailbox", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "informationbythirdparty", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "issignificantevent", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowconcurrent", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetAppointmentByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAppointmentByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "caseid", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetAppointmentByID(Guid AppointmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AppointmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteAppointment(Guid AppointmentId)
        {
            this.DeleteRecord(TableName, AppointmentId);
        }
    }
}
