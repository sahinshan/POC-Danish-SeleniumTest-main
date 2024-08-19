using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class FinancialAssessment : BaseClass
    {
        public string TableName { get { return "FinancialAssessment"; } }
        public string PrimaryKeyName { get { return "FinancialAssessmentid"; } }

        public FinancialAssessment()
        {
            AuthenticateUser();
        }

        public FinancialAssessment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateFinancialAssessment(
            Guid ownerid, Guid responsibleuserid, Guid personid, Guid financialassessmentstatusid,
            Guid chargingruleid, Guid? financialassessmentjointtypeid,
            Guid? partnerid, Guid incomesupporttypeid, Guid authorisedbyid, Guid financialassessmenttypeid, DateTime startdate, DateTime enddate, DateTime authorisationdate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "financialassessmentstatusid", financialassessmentstatusid);
            AddFieldToBusinessDataObject(dataObject, "chargingruleid", chargingruleid);
            AddFieldToBusinessDataObject(dataObject, "financialassessmentjointtypeid", financialassessmentjointtypeid);
            AddFieldToBusinessDataObject(dataObject, "partnerid", partnerid);
            AddFieldToBusinessDataObject(dataObject, "incomesupporttypeid", incomesupporttypeid);
            AddFieldToBusinessDataObject(dataObject, "authorisedbyid", authorisedbyid);
            AddFieldToBusinessDataObject(dataObject, "financialassessmenttypeid", financialassessmenttypeid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(dataObject, "authorisationdate", authorisationdate);
            AddFieldToBusinessDataObject(dataObject, "financialassessmentcategoryid", 2);
            AddFieldToBusinessDataObject(dataObject, "dayspropertydisregarded", 0);
            AddFieldToBusinessDataObject(dataObject, "incomesupportvalue", 253.75);
            AddFieldToBusinessDataObject(dataObject, "deferredpaymentscheme", false);
            AddFieldToBusinessDataObject(dataObject, "overridedefaultdeferredamount", false);
            AddFieldToBusinessDataObject(dataObject, "permitchargeupdatesbyfinancialassessment", true);
            AddFieldToBusinessDataObject(dataObject, "permitchargeupdatesbyrecalculation", true);
            AddFieldToBusinessDataObject(dataObject, "hasserviceprovisionassociated", true);
            AddFieldToBusinessDataObject(dataObject, "recordedinerror", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "calculationrequired", false);

            return CreateRecord(dataObject);
        }

        public Guid CreateFinancialAssessment(Guid ownerid, Guid responsibleuserid, Guid personid, Guid financialassessmentstatusid,
            Guid chargingruleid, Guid incomesupporttypeid, Guid authorisedbyid, Guid financialassessmenttypeid, DateTime startdate, DateTime enddate,
            DateTime authorisationdate, int financialassessmentcategoryid, decimal incomesupportvalue)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "financialassessmentstatusid", financialassessmentstatusid);
            AddFieldToBusinessDataObject(dataObject, "chargingruleid", chargingruleid);
            AddFieldToBusinessDataObject(dataObject, "incomesupporttypeid", incomesupporttypeid);
            AddFieldToBusinessDataObject(dataObject, "authorisedbyid", authorisedbyid);
            AddFieldToBusinessDataObject(dataObject, "financialassessmenttypeid", financialassessmenttypeid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(dataObject, "authorisationdate", authorisationdate);
            AddFieldToBusinessDataObject(dataObject, "financialassessmentcategoryid", financialassessmentcategoryid);
            AddFieldToBusinessDataObject(dataObject, "dayspropertydisregarded", 0);
            AddFieldToBusinessDataObject(dataObject, "incomesupportvalue", incomesupportvalue);
            AddFieldToBusinessDataObject(dataObject, "deferredpaymentscheme", false);
            AddFieldToBusinessDataObject(dataObject, "overridedefaultdeferredamount", false);
            AddFieldToBusinessDataObject(dataObject, "permitchargeupdatesbyfinancialassessment", true);
            AddFieldToBusinessDataObject(dataObject, "permitchargeupdatesbyrecalculation", true);
            AddFieldToBusinessDataObject(dataObject, "hasserviceprovisionassociated", true);
            AddFieldToBusinessDataObject(dataObject, "recordedinerror", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "calculationrequired", false);

            return CreateRecord(dataObject);
        }

        public Guid CreateFinancialAssessment(
            Guid personid, Guid financialassessmentstatusid, Guid responsibleuserid, Guid ownerid,
            DateTime startdate, DateTime enddate,
            Guid chargingruleid, Guid incomesupporttypeid, Guid financialassessmenttypeid, int dayspropertydisregarded, decimal incomesupportvalue)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            //General
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "financialassessmentstatusid", financialassessmentstatusid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);

            //Dates
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "enddate", enddate);

            //Type
            AddFieldToBusinessDataObject(dataObject, "chargingruleid", chargingruleid);
            AddFieldToBusinessDataObject(dataObject, "incomesupporttypeid", incomesupporttypeid);
            AddFieldToBusinessDataObject(dataObject, "financialassessmenttypeid", financialassessmenttypeid);
            AddFieldToBusinessDataObject(dataObject, "dayspropertydisregarded", dayspropertydisregarded);
            AddFieldToBusinessDataObject(dataObject, "incomesupportvalue", incomesupportvalue);

            //Deferred Payment Scheme
            AddFieldToBusinessDataObject(dataObject, "deferredpaymentscheme", false);

            //Activity
            AddFieldToBusinessDataObject(dataObject, "permitchargeupdatesbyfinancialassessment", true);
            AddFieldToBusinessDataObject(dataObject, "permitchargeupdatesbyrecalculation", true);

            //Authorisation Detail


            //Related Information




            AddFieldToBusinessDataObject(dataObject, "recordedinerror", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "calculationrequired", false);

            return CreateRecord(dataObject);
        }

        public void UpdateFinancialAssessmentStatus(Guid FinancialAssessmentID, Guid financialassessmentstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, FinancialAssessmentID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "financialassessmentstatusid", financialassessmentstatusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateFinancialAssessmentCalculationRequired(Guid FinancialAssessmentID, bool CalculationRequired)
        {

            var buisinessDataObject = GetBusinessDataBaseObject("FinancialAssessment", "FinancialAssessmentID");

            buisinessDataObject.FieldCollection.Add("FinancialAssessmentID", FinancialAssessmentID);
            buisinessDataObject.FieldCollection.Add("CalculationRequired", CalculationRequired);


            this.UpdateRecord(buisinessDataObject);

        }

        public List<Guid> GetFinancialAssessmentByPersonID(string PersonID)
        {
            DataQuery query = this.GetDataQueryObject("FinancialAssessment", false, "FinancialAssessmentId");

            BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, PersonID);

            AddReturnField(query, "FinancialAssessment", "FinancialAssessmentid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinancialAssessmentid");
        }


        public bool GetFinancialAssessmentCalculationRequiredFlag(Guid FinancialAssessmentID)
        {

            DataQuery query = new DataQuery("FinancialAssessment", false);
            query.PrimaryKeyName = "FinancialAssessmentId";

            query.AddThisTableCondition("FinancialAssessmentId", ConditionOperatorType.Equal, FinancialAssessmentID);

            query.AddField("FinancialAssessment", "CalculationRequired", "CalculationRequired");

            var response = ExecuteDataQuery(query);

            if (response.HasErrors) throw new Exception(response.Error);

            return response.BusinessDataCollection.Select(c => (bool)c.FieldCollection["CalculationRequired"]).FirstOrDefault();

        }


        public void WaitForFinancialAssessmentCalculationRequiredFlag(Guid FinancialAssessmentID, bool ExpectedCalculationRequired)
        {
            bool calculationRequired = GetFinancialAssessmentCalculationRequiredFlag(FinancialAssessmentID);

            int maximumRetries = 180;
            int count = 0;

            while (ExpectedCalculationRequired != calculationRequired)
            {
                count++;

                if (count > maximumRetries)
                    throw new Exception("unable to confirm the Calculation Required Flag for the financial assessment");

                System.Threading.Thread.Sleep(1000);

                calculationRequired = GetFinancialAssessmentCalculationRequiredFlag(FinancialAssessmentID);
            }

        }

        public Dictionary<string, object> GetByID(Guid FinancialAssessmentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, FinancialAssessmentId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteFinancialAssessment(Guid FinancialAssessmentID)
        {
            this.DeleteRecord(TableName, FinancialAssessmentID);
        }
    }
}
