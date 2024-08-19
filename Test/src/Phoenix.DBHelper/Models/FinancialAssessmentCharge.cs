using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class FinancialAssessmentCharge : BaseClass
    {
        public string TableName { get { return "FinancialAssessmentCharge"; } }
        public string PrimaryKeyName { get { return "FinancialAssessmentChargeid"; } }

        public FinancialAssessmentCharge()
        {
            AuthenticateUser();
        }

        public FinancialAssessmentCharge(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateFinancialAssessmentCharge(Guid ownerid, Guid financialassessmentid, Guid scheduletypeid, DateTime startdate, DateTime enddate, decimal initialcharge, decimal finalcharge, decimal chargetopaynow)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "financialassessmentid", financialassessmentid);
            AddFieldToBusinessDataObject(dataObject, "scheduletypeid", scheduletypeid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(dataObject, "initialcharge", initialcharge);
            AddFieldToBusinessDataObject(dataObject, "finalcharge", finalcharge);
            AddFieldToBusinessDataObject(dataObject, "chargetopaynow", chargetopaynow);
            AddFieldToBusinessDataObject(dataObject, "statusid", 1);
            AddFieldToBusinessDataObject(dataObject, "belowminimum", false);
            AddFieldToBusinessDataObject(dataObject, "isdeferred", false);
            AddFieldToBusinessDataObject(dataObject, "ismanualdeferred", false);
            AddFieldToBusinessDataObject(dataObject, "limittomaximum", false);
            AddFieldToBusinessDataObject(dataObject, "rounddown", false);
            AddFieldToBusinessDataObject(dataObject, "isfullcost", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);


            return CreateRecord(dataObject);
        }


        public List<Guid> GetFinancialAssessmentChargeForFinancialAssessment(Guid FinancialAssessmentID)
        {
            DataQuery query = this.GetDataQueryObject("FinancialAssessmentCharge", false, "FinancialAssessmentChargeId");

            BaseClassAddTableCondition(query, "financialassessmentid", ConditionOperatorType.Equal, FinancialAssessmentID);

            AddReturnField(query, "FinancialAssessmentCharge", "FinancialAssessmentChargeid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinancialAssessmentChargeid");
        }

        public List<Guid> GetFinancialAssessmentChargeForFinancialAssessment(Guid FinancialAssessmentID, DateTime ChargeStartDate)
        {
            DataQuery query = new DataQuery("FinancialAssessmentCharge", false);
            query.PrimaryKeyName = "FinancialAssessmentChargeId";

            query.AddThisTableCondition("FinancialAssessmentId", ConditionOperatorType.Equal, FinancialAssessmentID);
            query.AddThisTableCondition("StartDate", ConditionOperatorType.Equal, ChargeStartDate);

            query.AddField("FinancialAssessmentCharge", "financialassessmentchargeid", "financialassessmentchargeid");

            var response = ExecuteDataQuery(query);

            if (response.HasErrors) throw new Exception(response.Error);

            return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["FinancialAssessmentChargeId"]).ToList();
        }


        public void DeleteFinancialAssessmentCharge(Guid FinancialAssessmentChargeID)
        {
            var financialAssessmentChargeTotal = new FinancialAssessmentChargeTotal();
            var financialAssessmentChargeDetail = new FinancialAssessmentChargeDetail();

            foreach (Guid chargeTotalID in financialAssessmentChargeTotal.GetFinancialAssessmentChargeTotalForFinancialAssessmentCharge(FinancialAssessmentChargeID))
                financialAssessmentChargeTotal.DeleteFinancialAssessmentChargeTotal(chargeTotalID);

            foreach (Guid chargeDetailID in financialAssessmentChargeDetail.GetFinancialAssessmentChargeDetailForFinancialAssessmentCharge(FinancialAssessmentChargeID, null, null, null))
                financialAssessmentChargeDetail.DeleteFinancialAssessmentChargeDetail(chargeDetailID);

            DeleteRecord("FinancialAssessmentCharge", FinancialAssessmentChargeID);
        }
    }
}
