using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FAContribution : BaseClass
    {
        public string TableName { get { return "FAContribution"; } }
        public string PrimaryKeyName { get { return "FAContributionId"; } }

        public FAContribution()
        {
            AuthenticateUser();
        }

        public FAContribution(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetContributionsByFinancialAssessmentID(Guid FinancialAssessmentID)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "FinancialAssessmentId", ConditionOperatorType.Equal, FinancialAssessmentID);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateFAContribution(Guid FinancialAssessmentId, Guid? ServiceProvisionId, Guid PersonId, Guid OwnerId, Guid ContributionTypeId, Guid RecoveryMethodId, Guid DebtorBatchGroupingId, Guid PayeeId,
            string payeeidtablename, string payeeidname, DateTime StartDate, DateTime EndDate, string glcode = null)
        {

            var record = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            //General
            this.AddFieldToBusinessDataObject(record, "FinancialAssessmentId", FinancialAssessmentId);
            this.AddFieldToBusinessDataObject(record, "ServiceProvisionId", ServiceProvisionId);
            this.AddFieldToBusinessDataObject(record, "PersonId", PersonId);
            this.AddFieldToBusinessDataObject(record, "OwnerId", OwnerId);
            this.AddFieldToBusinessDataObject(record, "ContributionTypeId", ContributionTypeId);
            this.AddFieldToBusinessDataObject(record, "RecoveryMethodId", RecoveryMethodId);
            this.AddFieldToBusinessDataObject(record, "DebtorBatchGroupingId", DebtorBatchGroupingId);
            this.AddFieldToBusinessDataObject(record, "PayeeId", PayeeId);

            this.AddFieldToBusinessDataObject(record, "payeeidtablename", payeeidtablename);
            this.AddFieldToBusinessDataObject(record, "payeeidname", payeeidname);

            this.AddFieldToBusinessDataObject(record, "StartDate", StartDate);
            this.AddFieldToBusinessDataObject(record, "EndDate", EndDate);

            this.AddFieldToBusinessDataObject(record, "LinkStartDateToServiceProvision", false);
            this.AddFieldToBusinessDataObject(record, "LinkEndDateToServiceProvision", false);
            this.AddFieldToBusinessDataObject(record, "UpdateGLCode", false);
            this.AddFieldToBusinessDataObject(record, "Inactive", false);

            this.AddFieldToBusinessDataObject(record, "glcode", glcode);


            return this.CreateRecord(record);
        }

        public void ActivateFAContribution(Guid FAContributionID)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FAContributionID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", DataType.Boolean, BusinessObjectFieldType.Unknown, false, false);

            this.UpdateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetByID(Guid FAContributionId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, FAContributionId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
