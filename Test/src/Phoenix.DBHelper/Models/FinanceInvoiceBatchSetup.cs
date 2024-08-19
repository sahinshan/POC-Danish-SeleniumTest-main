using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinanceInvoiceBatchSetup : BaseClass
    {
        private string TableName = "FinanceInvoiceBatchSetup";
        private string PrimaryKeyName = "financeinvoicebatchsetupid";

        public FinanceInvoiceBatchSetup()
        {
            AuthenticateUser();
        }

        public FinanceInvoiceBatchSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateFinanceInvoiceBatchSetup(Guid ownerid, Guid OwningBusinessUnitId, string Name, Guid? ExtractNameId, int FinanceModuleId,
            Guid? InvoiceById, Guid? InvoiceFrequencyId, Guid PaymentTypeId, Guid ProviderBatchGroupingId, int PayToDayId,
            Guid ServiceElement1Id, DateTime StartDate, bool CreateFinanceInvoiceBatch, bool? AuthoriseInvoicesLessThanZero, bool CreateFinanceTransactions, int? CreateBatchWithin = null, string StartTime = "01:00")
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "ExtractNameId", ExtractNameId);
            AddFieldToBusinessDataObject(buisinessDataObject, "FinanceModuleId", FinanceModuleId);
            AddFieldToBusinessDataObject(buisinessDataObject, "InvoiceById", InvoiceById);
            AddFieldToBusinessDataObject(buisinessDataObject, "InvoiceFrequencyId", InvoiceFrequencyId);
            AddFieldToBusinessDataObject(buisinessDataObject, "PaymentTypeId", PaymentTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ProviderBatchGroupingId", ProviderBatchGroupingId);
            AddFieldToBusinessDataObject(buisinessDataObject, "PayToDayId", PayToDayId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement1Id", ServiceElement1Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartTime", StartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "CreateFinanceInvoiceBatch", CreateFinanceInvoiceBatch);
            AddFieldToBusinessDataObject(buisinessDataObject, "AuthoriseInvoicesLessThanZero", AuthoriseInvoicesLessThanZero);
            AddFieldToBusinessDataObject(buisinessDataObject, "CreateFinanceTransactions", CreateFinanceTransactions);
            AddFieldToBusinessDataObject(buisinessDataObject, "CreateBatchWithin", CreateBatchWithin);
            AddFieldToBusinessDataObject(buisinessDataObject, "isadhocbatch", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }


        public Guid CreateFinanceInvoiceBatchSetup(Guid Ownerid, Guid OwningBusinessUnitId, int FinanceModuleId, Guid RuleTypeId,
            Guid ContributionTypeId, Guid RecoveryMethodId, Guid DebtorBatchGroupingId, DateTime StartDate, string StartTime,
            Guid InvoiceById, int? CreateBatchWithin, Guid InvoiceFrequencyId, int PayToDayId, Guid ExtractNameId,
            Guid DebtorHeaderTextId, Guid DebtorTransactionTextId, Guid DebtorRecoveryTextId,
            bool CreateFinanceInvoiceBatch, bool AuthoriseInvoicesLessThanZero, bool CreditorReferenceNumberRequired,
            bool CreateFinanceTransactions, bool DebtorReferenceNumberRequired)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "Ownerid", Ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);

            AddFieldToBusinessDataObject(buisinessDataObject, "FinanceModuleId", FinanceModuleId);
            AddFieldToBusinessDataObject(buisinessDataObject, "RuleTypeId", RuleTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ContributionTypeId", ContributionTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "RecoveryMethodId", RecoveryMethodId);
            AddFieldToBusinessDataObject(buisinessDataObject, "DebtorBatchGroupingId", DebtorBatchGroupingId);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartTime", StartTime);

            AddFieldToBusinessDataObject(buisinessDataObject, "InvoiceById", InvoiceById);
            AddFieldToBusinessDataObject(buisinessDataObject, "CreateBatchWithin", CreateBatchWithin);
            AddFieldToBusinessDataObject(buisinessDataObject, "InvoiceFrequencyId", InvoiceFrequencyId);
            AddFieldToBusinessDataObject(buisinessDataObject, "PayToDayId", PayToDayId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ExtractNameId", ExtractNameId);


            AddFieldToBusinessDataObject(buisinessDataObject, "DebtorHeaderTextId", DebtorHeaderTextId);
            AddFieldToBusinessDataObject(buisinessDataObject, "DebtorTransactionTextId", DebtorTransactionTextId);
            AddFieldToBusinessDataObject(buisinessDataObject, "DebtorRecoveryTextId", DebtorRecoveryTextId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CreateFinanceInvoiceBatch", CreateFinanceInvoiceBatch);
            AddFieldToBusinessDataObject(buisinessDataObject, "AuthoriseInvoicesLessThanZero", AuthoriseInvoicesLessThanZero);
            AddFieldToBusinessDataObject(buisinessDataObject, "CreditorReferenceNumberRequired", CreditorReferenceNumberRequired);

            AddFieldToBusinessDataObject(buisinessDataObject, "CreateFinanceTransactions", CreateFinanceTransactions);
            AddFieldToBusinessDataObject(buisinessDataObject, "DebtorReferenceNumberRequired", DebtorReferenceNumberRequired);

            AddFieldToBusinessDataObject(buisinessDataObject, "isadhocbatch", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }


        public List<Guid> GetByFinanceModule_ServiceElement1_PaymentType_ProviderBatchGrouping(int FinanceModuleId, Guid ServiceElement1Id, Guid PaymentTypeId, Guid ProviderBatchGroupingId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "FinanceModuleId", ConditionOperatorType.Equal, FinanceModuleId);
            this.BaseClassAddTableCondition(query, "ServiceElement1Id", ConditionOperatorType.Equal, ServiceElement1Id);
            this.BaseClassAddTableCondition(query, "PaymentTypeId", ConditionOperatorType.Equal, PaymentTypeId);
            this.BaseClassAddTableCondition(query, "ProviderBatchGroupingId", ConditionOperatorType.Equal, ProviderBatchGroupingId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Dictionary<string, object> GetByID(Guid FinanceInvoiceBatchSetupId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, FinanceInvoiceBatchSetupId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteFinanceInvoiceBatchSetupId(Guid FinanceInvoiceBatchSetupId)
        {
            this.DeleteRecord(TableName, FinanceInvoiceBatchSetupId);
        }

    }
}
