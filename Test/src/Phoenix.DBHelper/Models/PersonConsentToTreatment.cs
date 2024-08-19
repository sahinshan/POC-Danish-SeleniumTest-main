using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonConsentToTreatment : BaseClass
    {

        public string TableName = "PersonConsentToTreatment";
        public string PrimaryKeyName = "PersonConsentToTreatmentId";


        public PersonConsentToTreatment()
        {
            AuthenticateUser();
        }

        public PersonConsentToTreatment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateConsentToTreatmentForm2(Guid ownerid, Guid personid, Guid caseid, Guid inpatientproceduretypeid, Guid heathprofessionalid,
            DateTime consentgivendate, DateTime completiondatetime, DateTime consentvaliduntil,
            string personlackscapacity, string procedureinformation)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("PersonConsentToTreatment", "PersonConsentToTreatmentid");

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(buisinessDataObject, "inpatientproceduretypeid", inpatientproceduretypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "heathprofessionalid", heathprofessionalid);
            AddFieldToBusinessDataObject(buisinessDataObject, "consentgivendate", consentgivendate);
            AddFieldToBusinessDataObject(buisinessDataObject, "completiondatetime", completiondatetime);
            AddFieldToBusinessDataObject(buisinessDataObject, "consentvaliduntil", consentvaliduntil);
            AddFieldToBusinessDataObject(buisinessDataObject, "personlackscapacity", personlackscapacity);
            AddFieldToBusinessDataObject(buisinessDataObject, "procedureinformation", procedureinformation);
            AddFieldToBusinessDataObject(buisinessDataObject, "consenttotreatmentrequiredtypeid", 2);
            AddFieldToBusinessDataObject(buisinessDataObject, "interpreterstatement", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "discussedprocedure", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "discussedparticularconcerns", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "discussedalternativetreatments", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "informationmediaprovided", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "professionalhasknowledge", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "additionalproceduresdiscussed", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "noadditionalproceduresperson", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "hasadvocate", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "withdrawconsent", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "canstudentsbepresentid", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "advancedecisionid", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "noadditionalproceduresparental", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateConsentToTreatmentForm1(Guid ownerid, Guid personid, Guid caseid, Guid inpatientproceduretypeid, Guid heathprofessionalid,
            DateTime consentgivendate, DateTime consentvaliduntil, DateTime completiondatetime,
            string procedureinformation)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("PersonConsentToTreatment", "PersonConsentToTreatmentid");

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(buisinessDataObject, "inpatientproceduretypeid", inpatientproceduretypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "heathprofessionalid", heathprofessionalid);
            AddFieldToBusinessDataObject(buisinessDataObject, "consentgivendate", consentgivendate);
            AddFieldToBusinessDataObject(buisinessDataObject, "consentvaliduntil", consentvaliduntil);
            AddFieldToBusinessDataObject(buisinessDataObject, "completiondatetime", completiondatetime);
            AddFieldToBusinessDataObject(buisinessDataObject, "procedureinformation", procedureinformation);
            AddFieldToBusinessDataObject(buisinessDataObject, "consenttotreatmentrequiredtypeid", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "interpreterstatement", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "canstudentsbepresentid", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "advancedecisionid", 2);
            AddFieldToBusinessDataObject(buisinessDataObject, "capacitytoconsentid", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "discussedprocedure", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "discussedparticularconcerns", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "discussedalternativetreatments", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "informationmediaprovided", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "professionalhasknowledge", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "additionalproceduresdiscussed", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "noadditionalproceduresperson", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "hasadvocate", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "withdrawconsent", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "noadditionalproceduresparental", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", 0);


            return this.CreateRecord(buisinessDataObject);
        }

        public void UpdateCompletionDatetime(Guid PersonConsentToTreatmentId, DateTime completiondatetime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonConsentToTreatmentId);

            AddFieldToBusinessDataObject(buisinessDataObject, "completiondatetime", completiondatetime);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonConsentToTreatmentByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "caseid", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetPersonConsentToTreatmentByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonConsentToTreatmentByID(Guid PersonConsentToTreatmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonConsentToTreatmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonConsentToTreatment(Guid PersonConsentToTreatmentId)
        {
            this.DeleteRecord(TableName, PersonConsentToTreatmentId);
        }
    }
}
