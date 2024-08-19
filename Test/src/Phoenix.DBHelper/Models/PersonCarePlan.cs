using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;


namespace Phoenix.DBHelper.Models
{
    public class PersonCarePlan : BaseClass
    {

        public string TableName = "PersonCarePlan";
        public string PrimaryKeyName = "PersonCarePlanId";


        public PersonCarePlan()
        {
            AuthenticateUser();
        }

        public PersonCarePlan(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonCarePlan(Guid careplantypeid, Guid carecoordinatorid, Guid personid, Guid caseid, Guid responsibleuserid, DateTime startdate, int statusid, int? careplanfamilyinvolvedid, Guid ownerid)

        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "careplantypeid", careplantypeid);
            AddFieldToBusinessDataObject(dataObject, "carecoordinatorid", carecoordinatorid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "careplanfamilyinvolvedid", careplanfamilyinvolvedid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "planagreed", true);

            return this.CreateRecord(dataObject);
        }
        public Guid CreatePersonCarePlan(Guid careplantypeid, Guid personid, Guid responsibleuserid, DateTime startdate, DateTime reviewdate, int statusid, int careplanfamilyinvolvedid, Guid ownerid)

        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "careplantypeid", careplantypeid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "reviewdate", reviewdate);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "careplanfamilyinvolvedid", careplanfamilyinvolvedid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "planagreed", true);

            return this.CreateRecord(dataObject);
        }


        public Guid CreatePersonCarePlan(Guid careplantypeid, Guid personid, Guid responsibleuserid, Guid careplanneeddomainid, DateTime startdate, DateTime reviewdate, int careneedtypeid, int statusid, string currentsituation, string expectedoutcome, Guid ownerid)

        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "careplantypeid", careplantypeid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "careplanneeddomainid", careplanneeddomainid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "reviewdate", reviewdate);
            AddFieldToBusinessDataObject(dataObject, "careneedtypeid", careneedtypeid);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "currentsituation", currentsituation);
            AddFieldToBusinessDataObject(dataObject, "expectedoutcome", expectedoutcome);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "planagreed", true);

            return this.CreateRecord(dataObject);
        }


        public Guid CreatePersonCarePlan( Guid careplanneeddomainid, Guid ownerid, int careneedtypeid, Guid owningbusinessunitid, DateTime reviewdate, DateTime startdate, string expectedoutcome,Guid personid, string currentsituation, Guid responsibleuserid,  int statusid)

        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            
            AddFieldToBusinessDataObject(dataObject, "careplanneeddomainid", careplanneeddomainid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "careneedtypeid", careneedtypeid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "reviewdate", reviewdate);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "expectedoutcome", expectedoutcome);

            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "currentsituation", currentsituation);

            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "planagreed", true);
            AddFieldToBusinessDataObject(dataObject, "setregularreviewcycle", false);

            return this.CreateRecord(dataObject);
        }

        public void UpdateStatus(Guid PersonCarePlanId, int statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonCarePlanId);

            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateStatus(Guid PersonCarePlanId, int statusid,List<Guid> careplanagreedby,DateTime dateofagreement)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonCarePlanId);

            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);

            buisinessDataObject.MultiSelectBusinessObjectFields["careplanagreedbyid"] = new MultiSelectBusinessObjectDataCollection();

            if (careplanagreedby != null && careplanagreedby.Count > 0)
            {
                foreach (Guid careplanagreedbyid in careplanagreedby)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = careplanagreedbyid,
                        ReferenceIdTableName = "careplanagreedby",
                        ReferenceName = "Test"
                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields["careplanagreedbyid"].Add(dataRecord);
                }
            }
            AddFieldToBusinessDataObject(buisinessDataObject, "dateofagreement", dateofagreement);


            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCreatedOn(Guid PersonCarePlanId, DateTime CreatedOn)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonCarePlanId);

            AddFieldToBusinessDataObject(buisinessDataObject, "CreatedOn", CreatedOn);

            this.UpdateRecord(buisinessDataObject);
        }

        public Guid CreatePersonCarePlan
            (Guid ownerid, Guid owningbusinessunitid, Guid personid, DateTime dateofagreement, Dictionary<Guid, string> careplanagreedby,
            Guid careplanneeddomainid, DateTime startdate, bool setregularreviewcycle, DateTime reviewdate, 
            int careneedtypeid, int? milestone, string currentsituation, string expectedoutcome, string actions, 
            int statusid)

        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "dateofagreement", dateofagreement);
            
            dataObject.MultiSelectBusinessObjectFields["careplanagreedbyid"] = new MultiSelectBusinessObjectDataCollection();

            if (careplanagreedby != null && careplanagreedby.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> careplanagreedbyid in careplanagreedby)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = careplanagreedbyid.Key,
                        ReferenceIdTableName = "careplanagreedby",
                        ReferenceName = careplanagreedbyid.Value,
                    };
                    dataObject.MultiSelectBusinessObjectFields["careplanagreedbyid"].Add(dataRecord);
                }
            }

            AddFieldToBusinessDataObject(dataObject, "careplanneeddomainid", careplanneeddomainid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "setregularreviewcycle", setregularreviewcycle);
            AddFieldToBusinessDataObject(dataObject, "reviewdate", reviewdate);

            AddFieldToBusinessDataObject(dataObject, "careneedtypeid", careneedtypeid);
            if(milestone.HasValue)
                AddFieldToBusinessDataObject(dataObject, "milestone", milestone.Value);
            AddFieldToBusinessDataObject(dataObject, "currentsituation", currentsituation);
            AddFieldToBusinessDataObject(dataObject, "expectedoutcome", expectedoutcome);
            AddFieldToBusinessDataObject(dataObject, "actions", actions);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);


            return this.CreateRecord(dataObject);
        }

        public void UpdateAuthorisationInformation(Guid PersonCarePlanId, Guid? authorisedbyid, DateTime? authorisationdatetime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonCarePlanId);

            if(authorisedbyid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "authorisedbyid", authorisedbyid);
            if(authorisationdatetime.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "authorisationdatetime", authorisationdatetime);

            this.UpdateRecord(buisinessDataObject);
        }

        public void EndCarePlan(Guid PersonCarePlanId, DateTime enddate, Guid careplanendreasonid, int statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonCarePlanId);

            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "careplanendreasonid", careplanendreasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", true);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByCaseID(Guid CaseId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public List<Guid> GetByPersonID(Guid personid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid PersonCarePlanId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonCarePlanId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonCarePlan(Guid PersonCarePlanId)
        {
            this.DeleteRecord(TableName, PersonCarePlanId);
        }
    }
}
