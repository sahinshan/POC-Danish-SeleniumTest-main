using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FACalculationTrigger : BaseClass
    {
        public FACalculationTrigger()
        {
            AuthenticateUser();
        }

        public FACalculationTrigger(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetAllTriggersForFinancialAssessment(Guid FinancialAsessmentId)
        {
            DataQuery query = this.GetDataQueryObject("FACalculationTrigger", false, "FACalculationTriggerId");

            this.BaseClassAddTableCondition(query, "FinancialAsessmentId", ConditionOperatorType.Equal, FinancialAsessmentId);

            this.AddReturnField(query, "FACalculationTrigger", "FACalculationTriggerId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FACalculationTriggerId");
        }

        public List<Guid> GetAllWaitingTriggersByFinancialAssessmentID(Guid FinancialAsessmentId, Guid RegardingID)
        {
            DataQuery query = this.GetDataQueryObject("FACalculationTrigger", false, "FACalculationTriggerId");

            this.BaseClassAddTableCondition(query, "FinancialAsessmentId", ConditionOperatorType.Equal, FinancialAsessmentId);
            this.BaseClassAddTableCondition(query, "RegardingId", ConditionOperatorType.Equal, RegardingID);
            this.BaseClassAddTableCondition(query, "StatusId", ConditionOperatorType.Equal, 4);

            this.AddReturnField(query, "FACalculationTrigger", "FACalculationTriggerId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FACalculationTriggerId");
        }

        public void DeleteFACalculationTrigger(Guid facalculationtriggerid)
        {
            this.DeleteRecord("FACalculationTrigger", facalculationtriggerid);
        }

    }
}
