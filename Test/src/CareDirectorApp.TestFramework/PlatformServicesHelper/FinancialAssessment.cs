using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public class FinancialAssessment : BaseClass
    {

        public string TableName = "FinancialAssessment";
        public string PrimaryKeyName = "FinancialAssessmentId";
        

        public FinancialAssessment(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreateFinancialAssessment(Guid PersonId, Guid OwnerId, Guid ResponsibleUserId, Guid FinancialAssessmentStatusId, 
            Guid ChargingRuleId, Guid IncomeSupportTypeId, Guid FinancialAssessmentTypeId, 
            DateTime StartDate, DateTime EndDate, DateTime CommencementDate,
            decimal IncomeSupportValue, string Notes,
            int FinancialAssessmentCategoryId, int DaysPropertyDisregarded, 
            bool CalculationRequired, 
            bool DeferredPaymentScheme, bool OverrideDefaultDeferredAmount, bool CalculateInterest,
            bool RecordedInError, bool HasServiceProvisionAssociated, bool PermitChargeUpdatesByFinancialAssessment, 
            bool PermitChargeUpdatesByRecalculation)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "FinancialAssessmentStatusId", FinancialAssessmentStatusId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ChargingRuleId", ChargingRuleId);
            AddFieldToBusinessDataObject(buisinessDataObject, "IncomeSupportTypeId", IncomeSupportTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "FinancialAssessmentTypeId", FinancialAssessmentTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", EndDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "CommencementDate", CommencementDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "IncomeSupportValue", IncomeSupportValue);
            AddFieldToBusinessDataObject(buisinessDataObject, "Notes", Notes);
            AddFieldToBusinessDataObject(buisinessDataObject, "FinancialAssessmentCategoryId", FinancialAssessmentCategoryId);
            AddFieldToBusinessDataObject(buisinessDataObject, "DaysPropertyDisregarded", DaysPropertyDisregarded);
            AddFieldToBusinessDataObject(buisinessDataObject, "CalculationRequired", CalculationRequired);
            AddFieldToBusinessDataObject(buisinessDataObject, "DeferredPaymentScheme", DeferredPaymentScheme);
            AddFieldToBusinessDataObject(buisinessDataObject, "OverrideDefaultDeferredAmount", OverrideDefaultDeferredAmount);
            AddFieldToBusinessDataObject(buisinessDataObject, "CalculateInterest", CalculateInterest);
            AddFieldToBusinessDataObject(buisinessDataObject, "RecordedInError", RecordedInError);
            AddFieldToBusinessDataObject(buisinessDataObject, "HasServiceProvisionAssociated", HasServiceProvisionAssociated);
            AddFieldToBusinessDataObject(buisinessDataObject, "PermitChargeUpdatesByFinancialAssessment", PermitChargeUpdatesByFinancialAssessment);
            AddFieldToBusinessDataObject(buisinessDataObject, "PermitChargeUpdatesByRecalculation", PermitChargeUpdatesByRecalculation);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetFinancialAssessmentByPersonID(Guid PersonId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetFinancialAssessmentByID(Guid FinancialAssessmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, FinancialAssessmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteFinancialAssessment(Guid FinancialAssessmentId)
        {
            this.DeleteRecord(TableName, FinancialAssessmentId);
        }
    }
}
