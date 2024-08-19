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
    public class HealthAppointment : BaseClass
    {

        public string TableName = "HealthAppointment";
        public string PrimaryKeyName = "HealthAppointmentId";
        

        public HealthAppointment(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreateHealthAppointment(
            Guid ownerid, Guid personid, string personname, Guid dataformid, Guid contacttypeid, Guid healthappointmentreasonid, string healthappointmentreasonname, Guid caseid, Guid responsibleuserid, 
            Guid communityandclinicteamid, Guid healthappointmentlocationtypeid, string healthappointmentlocationtypename, Guid healthprofessionalid,
            string appointmentinformation, DateTime startdate, TimeSpan starttime, TimeSpan endtime, DateTime enddate, 
            bool cancelappointment, Guid? wholedtheappointmentid, Guid? healthappointmentoutcometypeid,
            int? cancellationreasontypeid, int? nonattendancetypeid, Guid? WhoCancelledTheAppointmentId, string WhoCancelledTheAppointmentIdName, string WhoCancelledTheAppointmentIdTableName, string whocancelledtheappointmentfreetext, DateTime? dateunavailablefrom, DateTime? dateavailablefrom, Guid? healthappointmentabsencereasonid, DateTime? cnanotificationdate,
            bool additionalprofessionalrequired,bool addtraveltimetoappointment, bool returntobaseafterappointment)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid_cwname", personname);
            AddFieldToBusinessDataObject(buisinessDataObject, "dataformid", dataformid);
            AddFieldToBusinessDataObject(buisinessDataObject, "contacttypeid", contacttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmentreasonid", healthappointmentreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmentreasonid_cwname", healthappointmentreasonname);
            AddFieldToBusinessDataObject(buisinessDataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "communityandclinicteamid", communityandclinicteamid);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmentlocationtypeid", healthappointmentlocationtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmentlocationtypeid_cwname", healthappointmentlocationtypename);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthprofessionalid", healthprofessionalid);
            AddFieldToBusinessDataObject(buisinessDataObject, "appointmentinformation", appointmentinformation);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "cancelappointment", cancelappointment);

            if(wholedtheappointmentid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "wholedtheappointmentid", wholedtheappointmentid.Value);
            if (healthappointmentoutcometypeid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmentoutcometypeid", healthappointmentoutcometypeid.Value);

            if (cancellationreasontypeid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "cancellationreasontypeid", cancellationreasontypeid.Value);
            if (nonattendancetypeid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "nonattendancetypeid", nonattendancetypeid.Value);
            if (WhoCancelledTheAppointmentId.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "WhoCancelledTheAppointmentId", WhoCancelledTheAppointmentId.Value);
            if (!string.IsNullOrEmpty(WhoCancelledTheAppointmentIdName))
                AddFieldToBusinessDataObject(buisinessDataObject, "WhoCancelledTheAppointmentIdName", WhoCancelledTheAppointmentIdName);
            if (!string.IsNullOrEmpty(WhoCancelledTheAppointmentIdTableName))
                AddFieldToBusinessDataObject(buisinessDataObject, "WhoCancelledTheAppointmentIdTableName", WhoCancelledTheAppointmentIdTableName);
            if (!string.IsNullOrEmpty(whocancelledtheappointmentfreetext))
                AddFieldToBusinessDataObject(buisinessDataObject, "whocancelledtheappointmentfreetext", whocancelledtheappointmentfreetext);
            if (dateunavailablefrom.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "dateunavailablefrom", dateunavailablefrom.Value);
            if (dateavailablefrom.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "dateavailablefrom", dateavailablefrom.Value);
            if (healthappointmentabsencereasonid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmentabsencereasonid", healthappointmentabsencereasonid.Value);
            if (cnanotificationdate.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "cnanotificationdate", cnanotificationdate.Value);


            AddFieldToBusinessDataObject(buisinessDataObject, "allowconcurrentappointment", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "bypassrestrictions", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "homevisit", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "outofhours", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "clientsusualplaceofresidence", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "groupbooking", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ambulancerequired", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "additionalprofessionalrequired", additionalprofessionalrequired);
            AddFieldToBusinessDataObject(buisinessDataObject, "addtraveltimetoappointment", addtraveltimetoappointment);
            AddFieldToBusinessDataObject(buisinessDataObject, "returntobaseafterappointment", returntobaseafterappointment);
            AddFieldToBusinessDataObject(buisinessDataObject, "confirmallgroupbookingshavebeenoutcomed", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmenttypeid", 4);
            AddFieldToBusinessDataObject(buisinessDataObject, "syncedwithmailbox", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetHealthAppointmentByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetHealthAppointmentByID(Guid HealthAppointmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, HealthAppointmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteHealthAppointment(Guid HealthAppointmentId)
        {
            this.DeleteRecord(TableName, HealthAppointmentId);
        }
    }
}
