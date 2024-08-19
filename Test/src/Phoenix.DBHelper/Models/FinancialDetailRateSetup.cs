
using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinancialDetailRateSetup : BaseClass
    {
        public FinancialDetailRateSetup()
        {
            AuthenticateUser();
        }

        public FinancialDetailRateSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetFinancialDetailRateSetupByID(Guid FinancialDetailRateSetupId)
        {
            DataQuery query = this.GetDataQueryObject("FinancialDetailRateSetup", false, "FinancialDetailRateSetupId");
            this.BaseClassAddTableCondition(query, "FinancialDetailRateSetupId", ConditionOperatorType.Equal, FinancialDetailRateSetupId);
            this.AddReturnField(query, "FinancialDetailRateSetup", "FinancialDetailRateSetupid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinancialDetailRateSetupId");
        }


        public List<Guid> GetFinancialDetailRateSetup(Guid FinacialDetailId, DateTime StartDate, DateTime EndDate)
        {
            DataQuery query = this.GetDataQueryObject("FinancialDetailRateSetup", false, "FinancialDetailRateSetupId");

            this.BaseClassAddTableCondition(query, "FinacialDetailId", ConditionOperatorType.Equal, FinacialDetailId);
            this.BaseClassAddTableCondition(query, "StartDate", ConditionOperatorType.Equal, StartDate);
            this.BaseClassAddTableCondition(query, "EndDate", ConditionOperatorType.Equal, EndDate);

            this.AddReturnField(query, "FinancialDetailRateSetup", "FinancialDetailRateSetupid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinancialDetailRateSetupId");
        }


        public void UpdateFinancialDetailRateSetup(Guid FinancialDetailRateSetupId, DateTime EndDate, decimal Amount)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("FinancialDetailRateSetup", "FinancialDetailRateSetupid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "FinancialDetailRateSetupid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinancialDetailRateSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, EndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Amount", DataType.Decimal, BusinessObjectFieldType.Unknown, false, Amount);

            this.UpdateRecord(buisinessDataObject);
        }


        public void DeleteFinancialDetailRateSetup(Guid FinancialDetailRateSetupID)
        {
            this.DeleteRecord("FinancialDetailRateSetup", FinancialDetailRateSetupID);
        }



    }
}
