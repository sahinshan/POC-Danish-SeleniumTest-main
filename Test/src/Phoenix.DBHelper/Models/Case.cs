using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class Case : BaseClass
    {
        public string TableName { get { return "case"; } }
        public string PrimaryKeyName { get { return "caseid"; } }


        public Case()
        {
            AuthenticateUser();
        }

        public Case(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateSocialCareCaseRecord(Guid ownerid, Guid personid, Guid contactreceivedbyid, Guid responsibleuserid, Guid casestatusid,
            Guid contactreasonid, Guid dataformid, Guid? contactsourceid, DateTime contactreceiveddatetime, DateTime startdatetime, int personage)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            return this.CreateSocialCareCaseRecord(ownerid, personid, contactreceivedbyid, responsibleuserid, casestatusid,
                contactreasonid, dataformid, contactsourceid, contactreceiveddatetime, startdatetime, personage, "", "");
        }

        public Guid CreateSocialCareCaseRecord(Guid ownerid, Guid personid, Guid contactreceivedbyid, Guid responsibleuserid, Guid casestatusid,
            Guid contactreasonid, Guid dataformid, Guid? contactsourceid, DateTime contactreceiveddatetime, DateTime startdatetime, int personage,
            string additionalinformation, string contactmadebyname = "", Guid? presentingpriorityid = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "contactreceivedbyid", contactreceivedbyid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "casestatusid", casestatusid);
            AddFieldToBusinessDataObject(dataObject, "contactreasonid", contactreasonid);
            AddFieldToBusinessDataObject(dataObject, "dataformid", dataformid);
            AddFieldToBusinessDataObject(dataObject, "contactsourceid", contactsourceid);
            AddFieldToBusinessDataObject(dataObject, "contactreceiveddatetime", contactreceiveddatetime);
            AddFieldToBusinessDataObject(dataObject, "startdatetime", startdatetime);
            AddFieldToBusinessDataObject(dataObject, "personage", personage);
            AddFieldToBusinessDataObject(dataObject, "additionalinformation", additionalinformation);
            AddFieldToBusinessDataObject(dataObject, "contactmadebyname", contactmadebyname);
            AddFieldToBusinessDataObject(dataObject, "policenotified", false);
            AddFieldToBusinessDataObject(dataObject, "rereferral", false);
            AddFieldToBusinessDataObject(dataObject, "responsemadetocontact", false);
            AddFieldToBusinessDataObject(dataObject, "section117aftercareentitlement", false);
            AddFieldToBusinessDataObject(dataObject, "ispersononleave", false);
            AddFieldToBusinessDataObject(dataObject, "isswappinginpatient", false);
            AddFieldToBusinessDataObject(dataObject, "carernoknotified", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "dischargeperson", false);
            AddFieldToBusinessDataObject(dataObject, "personawareofcontactid", true);
            AddFieldToBusinessDataObject(dataObject, "personsupportcontactid", true);
            AddFieldToBusinessDataObject(dataObject, "presentingpriorityid", presentingpriorityid);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> CreateMultipleSocialCareCaseRecord(Guid ownerid, List<Guid> personids, List<Guid> contactreceivedbyUserId, List<Guid> responsibleusers, Guid casestatusid, Guid contactreasonid, Guid dataformid, Guid contactsourceid, DateTime contactreceiveddatetime, DateTime startdatetime, int personage)
        {
            var allRecordsToCreate = new List<BusinessData>();
            int totalcontactreceivedbyUserIds = contactreceivedbyUserId.Count;
            int totalresponsibleusers = responsibleusers.Count;
            Random r = new Random();

            foreach (var personid in personids)
            {
                var contactreceivedbyid = contactreceivedbyUserId[r.Next(0, totalcontactreceivedbyUserIds)];
                var responsibleuserid = responsibleusers[r.Next(0, totalresponsibleusers)];

                var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

                AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
                AddFieldToBusinessDataObject(dataObject, "personid", personid);
                AddFieldToBusinessDataObject(dataObject, "contactreceivedbyid", contactreceivedbyid);
                AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
                AddFieldToBusinessDataObject(dataObject, "casestatusid", casestatusid);
                AddFieldToBusinessDataObject(dataObject, "contactreasonid", contactreasonid);
                AddFieldToBusinessDataObject(dataObject, "dataformid", dataformid);
                AddFieldToBusinessDataObject(dataObject, "contactsourceid", contactsourceid);
                AddFieldToBusinessDataObject(dataObject, "contactreceiveddatetime", contactreceiveddatetime);
                AddFieldToBusinessDataObject(dataObject, "startdatetime", startdatetime);
                AddFieldToBusinessDataObject(dataObject, "personage", personage);
                AddFieldToBusinessDataObject(dataObject, "policenotified", false);
                AddFieldToBusinessDataObject(dataObject, "rereferral", false);
                AddFieldToBusinessDataObject(dataObject, "responsemadetocontact", false);
                AddFieldToBusinessDataObject(dataObject, "section117aftercareentitlement", false);
                AddFieldToBusinessDataObject(dataObject, "ispersononleave", false);
                AddFieldToBusinessDataObject(dataObject, "isswappinginpatient", false);
                AddFieldToBusinessDataObject(dataObject, "carernoknotified", false);
                AddFieldToBusinessDataObject(dataObject, "inactive", false);
                AddFieldToBusinessDataObject(dataObject, "dischargeperson", false);
                AddFieldToBusinessDataObject(dataObject, "personawareofcontactid", true);
                AddFieldToBusinessDataObject(dataObject, "personsupportcontactid", true);

                allRecordsToCreate.Add(dataObject);

            }

            return this.CreateMultipleRecords(allRecordsToCreate);
        }

        public Guid CreateCommunityHealthCaseRecord(Guid ownerid, Guid personid, Guid contactreceivedbyid,
            Guid communityandclinicteamid,
            Guid responsibleuserid, Guid casestatusid, Guid contactreasonid,
            Guid administrativecategoryid,
            Guid servicetyperequestedid,
            Guid dataformid, Guid contactsourceid, DateTime contactreceiveddatetime,
            DateTime requestreceiveddatetime,
            DateTime startdatetime,
            DateTime caseaccepteddatetime,
            string presentingneeddetails,
            Guid? CurrentConsultantId = null, DateTime? WaitingTimeStartDate = null, int? RTTReferralId = 2, Guid? RTTPathwayId = null, Guid? RTTTreatmentStatusId = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "contactreceivedbyid", contactreceivedbyid);
            AddFieldToBusinessDataObject(dataObject, "communityandclinicteamid", communityandclinicteamid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "casestatusid", casestatusid);
            AddFieldToBusinessDataObject(dataObject, "contactreasonid", contactreasonid);
            AddFieldToBusinessDataObject(dataObject, "administrativecategoryid", administrativecategoryid);
            AddFieldToBusinessDataObject(dataObject, "servicetyperequestedid", servicetyperequestedid);
            AddFieldToBusinessDataObject(dataObject, "dataformid", dataformid);
            AddFieldToBusinessDataObject(dataObject, "contactsourceid", contactsourceid);
            AddFieldToBusinessDataObject(dataObject, "contactreceiveddatetime", contactreceiveddatetime);
            AddFieldToBusinessDataObject(dataObject, "requestreceiveddatetime", requestreceiveddatetime);
            AddFieldToBusinessDataObject(dataObject, "startdatetime", startdatetime);
            AddFieldToBusinessDataObject(dataObject, "caseaccepteddatetime", caseaccepteddatetime);
            AddFieldToBusinessDataObject(dataObject, "presentingneeddetails", presentingneeddetails);

            AddFieldToBusinessDataObject(dataObject, "CurrentConsultantId", CurrentConsultantId);
            AddFieldToBusinessDataObject(dataObject, "WaitingTimeStartDate", WaitingTimeStartDate);
            AddFieldToBusinessDataObject(dataObject, "RTTReferralId", RTTReferralId);
            AddFieldToBusinessDataObject(dataObject, "RTTPathwayId", RTTPathwayId);
            AddFieldToBusinessDataObject(dataObject, "RTTTreatmentStatusId", RTTTreatmentStatusId);

            AddFieldToBusinessDataObject(dataObject, "personage", 21);
            AddFieldToBusinessDataObject(dataObject, "policenotified", 0);
            AddFieldToBusinessDataObject(dataObject, "rereferral", 0);
            AddFieldToBusinessDataObject(dataObject, "responsemadetocontact", 0);
            AddFieldToBusinessDataObject(dataObject, "section117aftercareentitlement", 0);
            AddFieldToBusinessDataObject(dataObject, "ispersononleave", 0);
            AddFieldToBusinessDataObject(dataObject, "isswappinginpatient", 0);
            AddFieldToBusinessDataObject(dataObject, "carernoknotified", 0);
            AddFieldToBusinessDataObject(dataObject, "personawareofcontactid", 2);
            AddFieldToBusinessDataObject(dataObject, "nextofkinawareofcontactid", 2);
            AddFieldToBusinessDataObject(dataObject, "caseacceptedid", 1);
            AddFieldToBusinessDataObject(dataObject, "cnacount", 0);
            AddFieldToBusinessDataObject(dataObject, "dnacount", 0);
            AddFieldToBusinessDataObject(dataObject, "dischargeperson", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetCasesByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetActiveCasesByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);
            this.BaseClassAddTableCondition(query, "Inactive", ConditionOperatorType.Equal, 0);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCaseById(Guid CaseId, params string[] fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, fields);
            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetInActiveCasesByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);
            this.BaseClassAddTableCondition(query, "Inactive", ConditionOperatorType.Equal, 1);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCaseByID(Guid CaseId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateCaseAcceptedIdField(Guid CaseId, int caseacceptedid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caseacceptedid", caseacceptedid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateReviewDate(Guid CaseId, DateTime? reviewdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "reviewdate", reviewdate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAdditionalInformation(Guid CaseId, string additionalinformation)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "additionalinformation", additionalinformation);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateRTTReferral(Guid CaseId, int? rttreferralid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "rttreferralid", rttreferralid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCaseOrigin(Guid CaseId, int caseoriginid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caseoriginid", caseoriginid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void AssignCaseRecord(Guid CaseRecordID, Guid AssignToTeamID, Guid ResponsibleUser)
        {
            var ar = new CareDirector.Sdk.ServiceRequest.AssignRequest
            {
                ActionType = AssignDataRestrictionActionType.Save,
                BusinessObjectId = new Guid("79F4EFC4-BFB1-E811-80DC-0050560502CC"),
                BusinessObjectName = "case",
                OwnerId = AssignToTeamID,
                //OwningBusinessUnitId = new Guid("052849A5-FB30-4959-9115-8EA1A766DC3B"),
                RecordId = CaseRecordID,
                ResponsibleUserId = ResponsibleUser,
            };

            Assign(ar);
        }

        public Guid? GetDataRestrictionForCase(Guid CaseID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.Cases.Where(c => c.CaseId == CaseID).Select(x => x.DataRestrictionId).FirstOrDefault();

            }
        }

        public Guid GetCaseResponsibleTeam(Guid CaseID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.Cases.Where(x => x.CaseId == CaseID).Select(x => x.OwnerId).FirstOrDefault();
            }
        }

        public Guid? GetCaseResponsibleUser(Guid CaseID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.Cases.Where(x => x.CaseId == CaseID).Select(x => x.ResponsibleUserId).FirstOrDefault();
            }
        }

        public void RemoveCaseRestrictionFromDB(Guid CaseID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                var _record = entity.Cases.Where(c => c.CaseId == CaseID).FirstOrDefault();
                _record.DataRestrictionId = null;
                entity.SaveChanges();
            }
        }

        public void UpdateCaseRecord(Guid CaseId, int inpatientcasestatusid, DateTime actualdischargedatetime, Guid caseclosurereasonid, Guid actualdischargedestinationid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inpatientcasestatusid", inpatientcasestatusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "actualdischargedatetime", actualdischargedatetime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caseclosurereasonid", caseclosurereasonid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "actualdischargedestinationid", actualdischargedestinationid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "carernoknotified", 0);


            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCaseRecordToDischarge(Guid CaseId, int inpatientcasestatusid, DateTime actualdischargedatetime, Guid caseclosurereasonid, Guid actualdischargedestinationid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inpatientcasestatusid", inpatientcasestatusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "actualdischargedatetime", actualdischargedatetime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caseclosurereasonid", caseclosurereasonid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "actualdischargedestinationid", actualdischargedestinationid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "carernoknotified", 0);


            this.UpdateRecord(buisinessDataObject);
        }



        public Guid CreateInpatientCaseRecord(Guid dataFormId, Guid ownerid, Guid personid, DateTime contactreceiveddatetime,
           Guid contactreceivedbyid,
           Guid responsibleuserid, Guid contactreasonid, Guid contactsourceid, string presentingneeddetails, int inpatientcasestatusid, Guid casestatusid,
           Guid currentconsultantid, DateTime waitingtimestartdate, DateTime decisiontoadmitagreeddatetime, DateTime intendedadmissiondate, DateTime startdatetime,
           Guid providerid,
           Guid inpatientresponsiblewardid, bool dischargeperson, bool policenotified, bool rereferral, bool responsemadetocontact, bool section117aftercareentitlement, bool ispersononleave, bool isswappinginpatient, bool carernoknotified, bool iscloned, bool inactive, int? rttreferralid = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            AddFieldToBusinessDataObject(dataObject, "dataFormId", dataFormId);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "casestatusid", casestatusid);
            AddFieldToBusinessDataObject(dataObject, "contactreceiveddatetime", contactreceiveddatetime);
            AddFieldToBusinessDataObject(dataObject, "contactreceivedbyid", contactreceivedbyid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "contactreasonid", contactreasonid);
            AddFieldToBusinessDataObject(dataObject, "inpatientcasestatusid", inpatientcasestatusid);
            AddFieldToBusinessDataObject(dataObject, "contactsourceid", contactsourceid);
            AddFieldToBusinessDataObject(dataObject, "presentingneeddetails", presentingneeddetails);
            AddFieldToBusinessDataObject(dataObject, "currentconsultantid", currentconsultantid);
            AddFieldToBusinessDataObject(dataObject, "waitingtimestartdate", waitingtimestartdate);
            AddFieldToBusinessDataObject(dataObject, "decisiontoadmitagreeddatetime", decisiontoadmitagreeddatetime);
            AddFieldToBusinessDataObject(dataObject, "intendedadmissiondate", intendedadmissiondate);
            AddFieldToBusinessDataObject(dataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(dataObject, "startdatetime", startdatetime);
            AddFieldToBusinessDataObject(dataObject, "inpatientresponsiblewardid", inpatientresponsiblewardid);
            AddFieldToBusinessDataObject(dataObject, "dischargeperson", dischargeperson);
            AddFieldToBusinessDataObject(dataObject, "policenotified", policenotified);
            AddFieldToBusinessDataObject(dataObject, "rereferral", rereferral);
            AddFieldToBusinessDataObject(dataObject, "responsemadetocontact", responsemadetocontact);
            AddFieldToBusinessDataObject(dataObject, "section117aftercareentitlement", section117aftercareentitlement);
            AddFieldToBusinessDataObject(dataObject, "ispersononleave", ispersononleave);
            AddFieldToBusinessDataObject(dataObject, "isswappinginpatient", isswappinginpatient);
            AddFieldToBusinessDataObject(dataObject, "carernoknotified", carernoknotified);
            AddFieldToBusinessDataObject(dataObject, "iscloned", iscloned);
            AddFieldToBusinessDataObject(dataObject, "inactive", inactive);
            AddFieldToBusinessDataObject(dataObject, "rttreferralid", rttreferralid);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateInpatientCaseRecordWithStatusAsAdmission(Guid ownerid, Guid personid, DateTime contactreceiveddatetime, Guid contactreceivedbyid, string presentingneeddetails, Guid responsibleuserid, Guid? CaseStatusId,
            Guid contactreasonid, DateTime startdatetime, Guid dataFormId, Guid contactsourceid, Guid InpatientWardId, Guid InpatientBayId, Guid InpatientBedId, Guid InpatientAdmissionSourceId, Guid InpatientAdmissionMethodId,
            Guid currentconsultantid, DateTime AdmissionDateTime, Guid providerid,
            Guid inpatientresponsiblewardid, int inpatientcasestatusid, DateTime waitingtimestartdate, bool dischargeperson, bool policenotified,
            bool rereferral, bool responsemadetocontact, bool section117aftercareentitlement, bool ispersononleave, bool isswappinginpatient, bool carernoknotified, bool iscloned, bool inactive, int? rttreferralid = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            AddFieldToBusinessDataObject(dataObject, "dataFormId", dataFormId);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "CaseStatusId", CaseStatusId);
            AddFieldToBusinessDataObject(dataObject, "InpatientWardId", InpatientWardId);
            AddFieldToBusinessDataObject(dataObject, "InpatientBayId", InpatientBayId);
            AddFieldToBusinessDataObject(dataObject, "InpatientBedId", InpatientBedId);
            AddFieldToBusinessDataObject(dataObject, "contactreceiveddatetime", contactreceiveddatetime);
            AddFieldToBusinessDataObject(dataObject, "contactreceivedbyid", contactreceivedbyid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "contactreasonid", contactreasonid);
            AddFieldToBusinessDataObject(dataObject, "inpatientcasestatusid", inpatientcasestatusid);
            AddFieldToBusinessDataObject(dataObject, "contactsourceid", contactsourceid);
            AddFieldToBusinessDataObject(dataObject, "presentingneeddetails", presentingneeddetails);
            AddFieldToBusinessDataObject(dataObject, "currentconsultantid", currentconsultantid);
            AddFieldToBusinessDataObject(dataObject, "waitingtimestartdate", waitingtimestartdate);
            AddFieldToBusinessDataObject(dataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(dataObject, "startdatetime", startdatetime);
            AddFieldToBusinessDataObject(dataObject, "AdmissionDateTime", AdmissionDateTime);
            AddFieldToBusinessDataObject(dataObject, "inpatientresponsiblewardid", inpatientresponsiblewardid);
            AddFieldToBusinessDataObject(dataObject, "dischargeperson", dischargeperson);
            AddFieldToBusinessDataObject(dataObject, "policenotified", policenotified);
            AddFieldToBusinessDataObject(dataObject, "rereferral", rereferral);
            AddFieldToBusinessDataObject(dataObject, "InpatientAdmissionMethodId", InpatientAdmissionMethodId);
            AddFieldToBusinessDataObject(dataObject, "InpatientAdmissionSourceId", InpatientAdmissionSourceId);
            AddFieldToBusinessDataObject(dataObject, "responsemadetocontact", responsemadetocontact);
            AddFieldToBusinessDataObject(dataObject, "section117aftercareentitlement", section117aftercareentitlement);
            AddFieldToBusinessDataObject(dataObject, "ispersononleave", ispersononleave);
            AddFieldToBusinessDataObject(dataObject, "isswappinginpatient", isswappinginpatient);
            AddFieldToBusinessDataObject(dataObject, "carernoknotified", carernoknotified);
            AddFieldToBusinessDataObject(dataObject, "iscloned", iscloned);
            AddFieldToBusinessDataObject(dataObject, "inactive", inactive);
            AddFieldToBusinessDataObject(dataObject, "rttreferralid", rttreferralid);


            return this.CreateRecord(dataObject);
        }

        public Guid CreateInpatientCaseRecordWithStatusAsAdmission(Guid ownerid, Guid personid, DateTime contactreceiveddatetime, Guid contactreceivedbyid, string presentingneeddetails, Guid responsibleuserid, Guid? CaseStatusId,
            Guid contactreasonid, DateTime startdatetime, Guid dataFormId, Guid contactsourceid, Guid InpatientWardId, Guid InpatientBayId, Guid InpatientBedId, Guid InpatientAdmissionSourceId, Guid InpatientAdmissionMethodId,
            Guid currentconsultantid, DateTime AdmissionDateTime, Guid providerid,
            Guid inpatientresponsiblewardid, int inpatientcasestatusid, DateTime waitingtimestartdate, bool dischargeperson, bool policenotified,
            bool rereferral, bool responsemadetocontact, bool section117aftercareentitlement, bool ispersononleave, bool isswappinginpatient, bool carernoknotified, bool iscloned, bool inactive,
            int? rttreferralid, Guid? rtttreatmentstatusid, Guid? rttpathwayid, Guid? transferredfromproviderid = null, DateTime? rttreferraloriginalstartdate = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            AddFieldToBusinessDataObject(dataObject, "dataFormId", dataFormId);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "CaseStatusId", CaseStatusId);
            AddFieldToBusinessDataObject(dataObject, "InpatientWardId", InpatientWardId);
            AddFieldToBusinessDataObject(dataObject, "InpatientBayId", InpatientBayId);
            AddFieldToBusinessDataObject(dataObject, "InpatientBedId", InpatientBedId);
            AddFieldToBusinessDataObject(dataObject, "contactreceiveddatetime", contactreceiveddatetime);
            AddFieldToBusinessDataObject(dataObject, "contactreceivedbyid", contactreceivedbyid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "contactreasonid", contactreasonid);
            AddFieldToBusinessDataObject(dataObject, "inpatientcasestatusid", inpatientcasestatusid);
            AddFieldToBusinessDataObject(dataObject, "contactsourceid", contactsourceid);
            AddFieldToBusinessDataObject(dataObject, "presentingneeddetails", presentingneeddetails);
            AddFieldToBusinessDataObject(dataObject, "currentconsultantid", currentconsultantid);
            AddFieldToBusinessDataObject(dataObject, "waitingtimestartdate", waitingtimestartdate);
            AddFieldToBusinessDataObject(dataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(dataObject, "startdatetime", startdatetime);
            AddFieldToBusinessDataObject(dataObject, "AdmissionDateTime", AdmissionDateTime);
            AddFieldToBusinessDataObject(dataObject, "inpatientresponsiblewardid", inpatientresponsiblewardid);
            AddFieldToBusinessDataObject(dataObject, "dischargeperson", dischargeperson);
            AddFieldToBusinessDataObject(dataObject, "policenotified", policenotified);
            AddFieldToBusinessDataObject(dataObject, "rereferral", rereferral);
            AddFieldToBusinessDataObject(dataObject, "InpatientAdmissionMethodId", InpatientAdmissionMethodId);
            AddFieldToBusinessDataObject(dataObject, "InpatientAdmissionSourceId", InpatientAdmissionSourceId);
            AddFieldToBusinessDataObject(dataObject, "responsemadetocontact", responsemadetocontact);
            AddFieldToBusinessDataObject(dataObject, "section117aftercareentitlement", section117aftercareentitlement);
            AddFieldToBusinessDataObject(dataObject, "ispersononleave", ispersononleave);
            AddFieldToBusinessDataObject(dataObject, "isswappinginpatient", isswappinginpatient);
            AddFieldToBusinessDataObject(dataObject, "carernoknotified", carernoknotified);
            AddFieldToBusinessDataObject(dataObject, "iscloned", iscloned);
            AddFieldToBusinessDataObject(dataObject, "inactive", inactive);
            AddFieldToBusinessDataObject(dataObject, "rttreferralid", rttreferralid);
            AddFieldToBusinessDataObject(dataObject, "rtttreatmentstatusid", rtttreatmentstatusid);
            AddFieldToBusinessDataObject(dataObject, "rttpathwayid", rttpathwayid);
            AddFieldToBusinessDataObject(dataObject, "transferredfromproviderid", transferredfromproviderid);
            AddFieldToBusinessDataObject(dataObject, "rttreferraloriginalstartdate", rttreferraloriginalstartdate);

            return this.CreateRecord(dataObject);
        }


        public Guid CreateSocialCareCaseRecord(Guid ownerid, Guid personid, Guid contactreceivedbyid, Guid responsibleuserid, Guid casestatusid,
            Guid contactreasonid, Guid dataformid, Guid? contactsourceid, DateTime contactreceiveddatetime, DateTime startdatetime, int personage,
            Guid clonedfromid, Guid ClonedFromCase, string clonedfromidname, string clonedfromidtablename = "case", bool iscloned = true)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "contactreceivedbyid", contactreceivedbyid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "casestatusid", casestatusid);
            AddFieldToBusinessDataObject(dataObject, "contactreasonid", contactreasonid);
            AddFieldToBusinessDataObject(dataObject, "dataformid", dataformid);
            AddFieldToBusinessDataObject(dataObject, "contactsourceid", contactsourceid);
            AddFieldToBusinessDataObject(dataObject, "contactreceiveddatetime", contactreceiveddatetime);
            AddFieldToBusinessDataObject(dataObject, "startdatetime", startdatetime);
            AddFieldToBusinessDataObject(dataObject, "personage", personage);
            AddFieldToBusinessDataObject(dataObject, "additionalinformation", "");
            AddFieldToBusinessDataObject(dataObject, "contactmadebyname", "");
            AddFieldToBusinessDataObject(dataObject, "policenotified", false);
            AddFieldToBusinessDataObject(dataObject, "rereferral", false);
            AddFieldToBusinessDataObject(dataObject, "responsemadetocontact", false);
            AddFieldToBusinessDataObject(dataObject, "section117aftercareentitlement", false);
            AddFieldToBusinessDataObject(dataObject, "ispersononleave", false);
            AddFieldToBusinessDataObject(dataObject, "isswappinginpatient", false);
            AddFieldToBusinessDataObject(dataObject, "carernoknotified", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "dischargeperson", false);
            AddFieldToBusinessDataObject(dataObject, "personawareofcontactid", true);
            AddFieldToBusinessDataObject(dataObject, "personsupportcontactid", true);

            AddFieldToBusinessDataObject(dataObject, "clonedfromid", clonedfromid);
            AddFieldToBusinessDataObject(dataObject, "ClonedFromCase", ClonedFromCase);
            AddFieldToBusinessDataObject(dataObject, "clonedfromidname", clonedfromidname);
            AddFieldToBusinessDataObject(dataObject, "clonedfromidtablename", clonedfromidtablename);
            AddFieldToBusinessDataObject(dataObject, "iscloned", iscloned);

            return this.CreateRecord(dataObject);
        }

        public void UpdateCaseRecordToClosed(Guid CaseId, Guid casestatusid, DateTime enddatetime, Guid caseclosurereasonid, Guid closureacceptedbyid, DateTime archivedate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "casestatusid", casestatusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddatetime", enddatetime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caseclosurereasonid", caseclosurereasonid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "closureacceptedbyid", closureacceptedbyid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "archivedate", archivedate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteCase(Guid Caseid)
        {
            this.DeleteRecord(TableName, Caseid);
        }

    }
}
