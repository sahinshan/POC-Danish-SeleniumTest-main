using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinancialDetailDisregardChargingRuleType : BaseClass
    {
        public FinancialDetailDisregardChargingRuleType()
        {
            AuthenticateUser();
        }

        public FinancialDetailDisregardChargingRuleType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetFinancialDetailDisregardChargingRuleTypeByID(Guid FinancialDetailDisregardChargingRuleTypeId)
        {
            DataQuery query = this.GetDataQueryObject("FinancialDetailDisregardChargingRuleType", false, "FinancialDetailDisregardChargingRuleTypeId");
            this.BaseClassAddTableCondition(query, "FinancialDetailDisregardChargingRuleTypeId", ConditionOperatorType.Equal, FinancialDetailDisregardChargingRuleTypeId);
            this.AddReturnField(query, "FinancialDetailDisregardChargingRuleType", "FinancialDetailDisregardChargingRuleTypeid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinancialDetailDisregardChargingRuleTypeId");
        }


        public List<Guid> GetFinancialDetailDisregardChargingRuleType(Guid FinancialDetailDisregardID, Guid ChargingRuleTypeID)
        {
            DataQuery query = this.GetDataQueryObject("FinancialDetailDisregardChargingRuleType", false, "FinancialDetailDisregardChargingRuleTypeId");

            this.BaseClassAddTableCondition(query, "FinancialDetailDisregardId", ConditionOperatorType.Equal, FinancialDetailDisregardID);
            this.BaseClassAddTableCondition(query, "ChargingRuleTypeId", ConditionOperatorType.Equal, ChargingRuleTypeID);

            this.AddReturnField(query, "FinancialDetailDisregardChargingRuleType", "FinancialDetailDisregardChargingRuleTypeid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinancialDetailDisregardChargingRuleTypeId");
        }

        public List<Guid> GetFinancialDetailDisregardChargingRuleType(Guid FinancialDetailDisregardID)
        {
            DataQuery query = this.GetDataQueryObject("FinancialDetailDisregardChargingRuleType", false, "FinancialDetailDisregardChargingRuleTypeId");

            this.BaseClassAddTableCondition(query, "FinancialDetailDisregardId", ConditionOperatorType.Equal, FinancialDetailDisregardID);

            this.AddReturnField(query, "FinancialDetailDisregardChargingRuleType", "FinancialDetailDisregardChargingRuleTypeid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinancialDetailDisregardChargingRuleTypeId");
        }


        public void DeleteFinancialDetailDisregardChargingRuleType(Guid FinancialDetailDisregardChargingRuleTypeID)
        {
            this.DeleteRecord("FinancialDetailDisregardChargingRuleType", FinancialDetailDisregardChargingRuleTypeID);
        }



    }
}
