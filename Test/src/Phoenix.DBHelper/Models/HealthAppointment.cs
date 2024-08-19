using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class HealthAppointment : BaseClass
    {

        public string TableName = "HealthAppointment";
        public string PrimaryKeyName = "HealthAppointmentId";

        public HealthAppointment()
        {
            AuthenticateUser();
        }

        public HealthAppointment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateHealthAppointment(
            Guid ownerid, Guid personid, string personname, Guid dataformid, Guid contacttypeid, Guid healthappointmentreasonid, string healthappointmentreasonname, Guid caseid, Guid responsibleuserid,
            Guid communityandclinicteamid, Guid healthappointmentlocationtypeid, string healthappointmentlocationtypename, Guid healthprofessionalid,
            string appointmentinformation, DateTime startdate, TimeSpan starttime, TimeSpan endtime, DateTime enddate,
            bool cancelappointment, Guid? wholedtheappointmentid, Guid? healthappointmentoutcometypeid,
            int? cancellationreasontypeid, int? nonattendancetypeid, Guid? WhoCancelledTheAppointmentId, string WhoCancelledTheAppointmentIdName, string WhoCancelledTheAppointmentIdTableName, string whocancelledtheappointmentfreetext, DateTime? dateunavailablefrom, DateTime? dateavailablefrom, Guid? healthappointmentabsencereasonid, DateTime? cnanotificationdate,
            bool additionalprofessionalrequired, bool addtraveltimetoappointment, bool returntobaseafterappointment,
            bool allowconcurrentappointment = false, bool bypassrestrictions = false)
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

            if (wholedtheappointmentid.HasValue)
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


            AddFieldToBusinessDataObject(buisinessDataObject, "allowconcurrentappointment", allowconcurrentappointment);
            AddFieldToBusinessDataObject(buisinessDataObject, "bypassrestrictions", bypassrestrictions);
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
            AddFieldToBusinessDataObject(buisinessDataObject, "accommodationstatusreviewed", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "employeestatusreviewed", true);
            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateHealthAppointment(
            Guid ownerid, Guid personid, string personname, Guid dataformid, Guid contacttypeid, Guid healthappointmentreasonid, string healthappointmentreasonname, Guid caseid, Guid responsibleuserid,
            Guid communityandclinicteamid, Guid healthappointmentlocationtypeid, string healthappointmentlocationtypename, Guid healthprofessionalid, Guid? providerid,
            string appointmentinformation, DateTime startdate, TimeSpan starttime, TimeSpan endtime, DateTime enddate,
            bool cancelappointment, Guid? wholedtheappointmentid, Guid? healthappointmentoutcometypeid,
            int? cancellationreasontypeid, int? nonattendancetypeid, Guid? WhoCancelledTheAppointmentId, string WhoCancelledTheAppointmentIdName, string WhoCancelledTheAppointmentIdTableName, string whocancelledtheappointmentfreetext, DateTime? dateunavailablefrom, DateTime? dateavailablefrom, Guid? healthappointmentabsencereasonid, DateTime? cnanotificationdate,
            bool additionalprofessionalrequired, bool addtraveltimetoappointment, bool returntobaseafterappointment, bool bypassrestrictions = false)
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
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "appointmentinformation", appointmentinformation);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "cancelappointment", cancelappointment);

            if (wholedtheappointmentid.HasValue)
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
            AddFieldToBusinessDataObject(buisinessDataObject, "bypassrestrictions", bypassrestrictions);
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

        public Guid CreateHealthAppointmentForHomeVisitIsNo(
            Guid ownerid, Guid personid, string personname, Guid dataformid, Guid contacttypeid, Guid healthappointmentreasonid, string healthappointmentreasonname, Guid caseid, Guid responsibleuserid,
            Guid communityandclinicteamid, Guid healthappointmentlocationtypeid, string healthappointmentlocationtypename, Guid healthprofessionalid, Guid providerid, Guid providerroomid,
            string appointmentinformation, DateTime startdate, TimeSpan starttime, TimeSpan endtime, DateTime enddate,
            bool additionalprofessionalrequired, bool bypassrestrictions = false)
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
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerroomid", providerroomid);
            AddFieldToBusinessDataObject(buisinessDataObject, "appointmentinformation", appointmentinformation);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);

            AddFieldToBusinessDataObject(buisinessDataObject, "groupbooking", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowconcurrentappointment", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "bypassrestrictions", bypassrestrictions);
            AddFieldToBusinessDataObject(buisinessDataObject, "homevisit", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "outofhours", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ambulancerequired", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "additionalprofessionalrequired", additionalprofessionalrequired);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateHealthAppointmentForUnscheduled(Guid ownerid, Guid personid, Guid dataformid, Guid contacttypeid, Guid healthappointmentreasonid, Guid Caseid, DateTime startdate, TimeSpan starttime,
                                                        DateTime enddate, TimeSpan endtime, Guid ResponsibleUserId, Guid HealthAppointmentOutcomeTypeId, Guid CommunityAndClinicTeamId, Guid HealthAppointmentLocationTypeId,
                                                        int HealthAppointmentTypeId, Guid HealthProfessionalId, Guid CommunityClinicCareInterventionId, string Title)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "dataformid", dataformid);
            AddFieldToBusinessDataObject(dataObject, "contacttypeid", contacttypeid);
            AddFieldToBusinessDataObject(dataObject, "healthappointmentreasonid", healthappointmentreasonid);
            AddFieldToBusinessDataObject(dataObject, "Caseid", Caseid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(dataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(dataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(dataObject, "dataformid", dataformid);
            AddFieldToBusinessDataObject(dataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(dataObject, "HealthAppointmentOutcomeTypeId", HealthAppointmentOutcomeTypeId);
            AddFieldToBusinessDataObject(dataObject, "CommunityAndClinicTeamId", CommunityAndClinicTeamId);
            AddFieldToBusinessDataObject(dataObject, "HealthAppointmentLocationTypeId", HealthAppointmentLocationTypeId);
            AddFieldToBusinessDataObject(dataObject, "HealthAppointmentTypeId", HealthAppointmentTypeId);
            AddFieldToBusinessDataObject(dataObject, "HealthProfessionalId", HealthProfessionalId);
            AddFieldToBusinessDataObject(dataObject, "CommunityClinicCareInterventionId", CommunityClinicCareInterventionId);
            AddFieldToBusinessDataObject(dataObject, "Title", Title);
            AddFieldToBusinessDataObject(dataObject, "rereferral", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 1);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateHealthAppointmentForUnscheduled(Guid ownerid, Guid personid, Guid dataformid, Guid contacttypeid, Guid healthappointmentreasonid, Guid Caseid, DateTime startdate, TimeSpan starttime,
                                                        DateTime enddate, TimeSpan endtime, Guid ResponsibleUserId, Guid CommunityAndClinicTeamId, Guid HealthAppointmentLocationTypeId,
                                                        int HealthAppointmentTypeId, Guid HealthProfessionalId, string Title, bool inactive)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "dataformid", dataformid);
            AddFieldToBusinessDataObject(dataObject, "contacttypeid", contacttypeid);
            AddFieldToBusinessDataObject(dataObject, "healthappointmentreasonid", healthappointmentreasonid);
            AddFieldToBusinessDataObject(dataObject, "Caseid", Caseid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(dataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(dataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(dataObject, "dataformid", dataformid);
            AddFieldToBusinessDataObject(dataObject, "ResponsibleUserId", ResponsibleUserId);
            //AddFieldToBusinessDataObject(dataObject, "HealthAppointmentOutcomeTypeId", HealthAppointmentOutcomeTypeId);
            AddFieldToBusinessDataObject(dataObject, "CommunityAndClinicTeamId", CommunityAndClinicTeamId);
            AddFieldToBusinessDataObject(dataObject, "HealthAppointmentLocationTypeId", HealthAppointmentLocationTypeId);
            AddFieldToBusinessDataObject(dataObject, "HealthAppointmentTypeId", HealthAppointmentTypeId);
            AddFieldToBusinessDataObject(dataObject, "HealthProfessionalId", HealthProfessionalId);
            //AddFieldToBusinessDataObject(dataObject, "CommunityClinicCareInterventionId", CommunityClinicCareInterventionId);
            AddFieldToBusinessDataObject(dataObject, "Title", Title);
            AddFieldToBusinessDataObject(dataObject, "Inactive", inactive);

            return this.CreateRecord(dataObject);
        }

        public void CancelAppointment(Guid HealthAppointmentId, int cancellationreasontypeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, HealthAppointmentId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "cancelappointment", true);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "cancellationreasontypeid", cancellationreasontypeid);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetHealthAppointmentByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetGroupBookingsByProviderID(Guid providerid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerid", ConditionOperatorType.Equal, providerid);
            this.BaseClassAddTableCondition(query, "groupbooking", ConditionOperatorType.Equal, true);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetHealthAppointmentByHealthProfessionalID(Guid healthprofessionalid, DateTime StartDate)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "healthprofessionalid", ConditionOperatorType.Equal, healthprofessionalid);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.GreaterEqual, StartDate);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetHealthAppointmentByID(Guid HealthAppointmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, HealthAppointmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public void DeleteHealthAppointment(Guid HealthAppointmentId)
        {
            this.DeleteRecord(TableName, HealthAppointmentId);
        }


        public Guid CreateHealthAppointment1(
            Guid ownerid, Guid personid, Guid dataformid, Guid contacttypeid, Guid healthappointmentreasonid, Guid caseid,
            DateTime startdate, TimeSpan starttime, TimeSpan endtime, DateTime enddate, Guid responsibleuserid,
            Guid communityandclinicteamid, Guid healthappointmentlocationtypeid, Guid healthprofessionalid, int AppointmentScheduledById = 1)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "dataformid", dataformid);
            AddFieldToBusinessDataObject(buisinessDataObject, "contacttypeid", contacttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmentreasonid", healthappointmentreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "communityandclinicteamid", communityandclinicteamid);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmentlocationtypeid", healthappointmentlocationtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthprofessionalid", healthprofessionalid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);

            AddFieldToBusinessDataObject(buisinessDataObject, "AppointmentScheduledById", AppointmentScheduledById);

            AddFieldToBusinessDataObject(buisinessDataObject, "cancelappointment", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowconcurrentappointment", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "bypassrestrictions", false);

            AddFieldToBusinessDataObject(buisinessDataObject, "homevisit", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "outofhours", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "clientsusualplaceofresidence", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "groupbooking", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ambulancerequired", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "additionalprofessionalrequired", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "addtraveltimetoappointment", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "returntobaseafterappointment", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "confirmallgroupbookingshavebeenoutcomed", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmenttypeid", 4);
            AddFieldToBusinessDataObject(buisinessDataObject, "syncedwithmailbox", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }


        public void UpdateHealthAppointmentOutcomeType(Guid HealthAppointmentId, Guid healthappointmentoutcometypeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, HealthAppointmentId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmentoutcometypeid", healthappointmentoutcometypeid);

            this.UpdateRecord(buisinessDataObject);
        }

    }
}
