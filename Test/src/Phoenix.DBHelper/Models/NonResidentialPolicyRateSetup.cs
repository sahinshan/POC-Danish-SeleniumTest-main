using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class NonResidentialPolicyRateSetup : BaseClass
    {
        public NonResidentialPolicyRateSetup()
        {
            AuthenticateUser();
        }

        public NonResidentialPolicyRateSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetNonResidentialPolicyRateSetupByID(Guid NonResidentialPolicyRateSetupId)
        {
            DataQuery query = this.GetDataQueryObject("NonResidentialPolicyRateSetup", false, "NonResidentialPolicyRateSetupId");
            this.BaseClassAddTableCondition(query, "NonResidentialPolicyRateSetupId", ConditionOperatorType.Equal, NonResidentialPolicyRateSetupId);
            this.AddReturnField(query, "NonResidentialPolicyRateSetup", "NonResidentialPolicyRateSetupid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "NonResidentialPolicyRateSetupId");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ChargingRuleID"></param>
        /// <param name="AuthorityID">1: IS ; 2: LA; 3: Both</param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public List<Guid> GetNonResidentialPolicyRateSetup(Guid ChargingRuleTypeId, DateTime StartDate, DateTime EndDate)
        {
            DataQuery query = this.GetDataQueryObject("NonResidentialPolicyRateSetup", false, "NonResidentialPolicyRateSetupId");

            this.BaseClassAddTableCondition(query, "ChargingRuleTypeId", ConditionOperatorType.Equal, ChargingRuleTypeId);
            this.BaseClassAddTableCondition(query, "StartDate", ConditionOperatorType.Equal, StartDate);
            this.BaseClassAddTableCondition(query, "EndDate", ConditionOperatorType.Equal, EndDate);

            this.AddReturnField(query, "NonResidentialPolicyRateSetup", "NonResidentialPolicyRateSetupid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "NonResidentialPolicyRateSetupId");
        }


        public void UpdateNonResidentialPolicyRateSetup(Guid NonResidentialPolicyRateSetupId, DateTime EndDate, int PercentIncreaseonISGCAmount, decimal MinimumDisabilityRelatedExpense, decimal MinimumWeeklyCharge, decimal MaximumWeeklyCharge, bool ExceedMaximumWeeklyChargeonFullCost)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("NonResidentialPolicyRateSetup", "NonResidentialPolicyRateSetupid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "NonResidentialPolicyRateSetupid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, NonResidentialPolicyRateSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, EndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PercentIncreaseonISGCAmount", DataType.Integer, BusinessObjectFieldType.Unknown, false, PercentIncreaseonISGCAmount);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "MinimumDisabilityRelatedExpense", DataType.Decimal, BusinessObjectFieldType.Unknown, false, MinimumDisabilityRelatedExpense);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "MinimumWeeklyCharge", DataType.Decimal, BusinessObjectFieldType.Unknown, false, MinimumWeeklyCharge);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "MaximumWeeklyCharge", DataType.Decimal, BusinessObjectFieldType.Unknown, false, MaximumWeeklyCharge);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ExceedMaximumWeeklyChargeonFullCost", DataType.Boolean, BusinessObjectFieldType.Unknown, false, ExceedMaximumWeeklyChargeonFullCost);

            this.UpdateRecord(buisinessDataObject);
        }


        public void DeleteNonResidentialPolicyRateSetup(Guid NonResidentialPolicyRateSetupID)
        {
            this.DeleteRecord("NonResidentialPolicyRateSetup", NonResidentialPolicyRateSetupID);
        }



    }
}
