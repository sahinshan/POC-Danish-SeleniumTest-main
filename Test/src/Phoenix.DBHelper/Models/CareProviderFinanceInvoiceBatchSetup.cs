using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderFinanceInvoiceBatchSetup : BaseClass
    {
        private string TableName = "CareProviderFinanceInvoiceBatchSetup";
        private string PrimaryKeyName = "CareProviderFinanceInvoiceBatchSetupid";

        public CareProviderFinanceInvoiceBatchSetup()
        {
            AuthenticateUser();
        }

        public CareProviderFinanceInvoiceBatchSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareProviderFinanceInvoiceBatchSetup(bool issundry,
            Guid careprovidercontractschemeid, Guid careproviderbatchgroupingid,
            DateTime startdate, TimeSpan starttime,
            int invoicebyid, int careproviderinvoicefrequencyid, int? createbatchwithin, int chargetodayid, int whentobatchfinancetransactionsid, bool useenddatewhenbatchingfinancetransactions, DateTime financetransactionsupto, bool separateinvoices,
            Guid careproviderextractnameid, bool debtorreferencenumberrequired,
            Guid ownerid, bool isfinancecoderequired = false, bool isvatcoderequired = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "issundry", issundry);

            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractschemeid", careprovidercontractschemeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderbatchgroupingid", careproviderbatchgroupingid);

            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);

            AddFieldToBusinessDataObject(buisinessDataObject, "invoicebyid", invoicebyid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderinvoicefrequencyid", careproviderinvoicefrequencyid);
            AddFieldToBusinessDataObject(buisinessDataObject, "createbatchwithin", createbatchwithin);
            AddFieldToBusinessDataObject(buisinessDataObject, "chargetodayid", chargetodayid);
            AddFieldToBusinessDataObject(buisinessDataObject, "whentobatchfinancetransactionsid", whentobatchfinancetransactionsid);
            AddFieldToBusinessDataObject(buisinessDataObject, "useenddatewhenbatchingfinancetransactions", useenddatewhenbatchingfinancetransactions);
            AddFieldToBusinessDataObject(buisinessDataObject, "financetransactionsupto", financetransactionsupto);
            AddFieldToBusinessDataObject(buisinessDataObject, "separateinvoices", separateinvoices);

            AddFieldToBusinessDataObject(buisinessDataObject, "invoicetext", "Charge for services at {Establishment} up to {Charges Up To}");
            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextstandard", "Charge for {Person} for the period {Start Date} to {End Date}");
            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextcontra", "Cancellation of Charge for {Person} for the period {Start Date} to {End Date}");
            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextend", "Additional Charge for ending Service on {End Date}");
            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextadditional", "Additional Charge");
            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextnetincome", "Deduction of Charge for {Person} for the period {Start Date} to {End Date}");
            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextapportioned", "Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date}");
            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextapportionedfunder", "Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date}");
            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextapportionedprovider", "Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date}");
            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextexpense", "Ad Hoc Expense of {Expense} for {Person} on {Start Date}");

            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderextractnameid", careproviderextractnameid);
            AddFieldToBusinessDataObject(buisinessDataObject, "debtorreferencenumberrequired", debtorreferencenumberrequired);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "isfinancecoderequired", isfinancecoderequired);
            AddFieldToBusinessDataObject(buisinessDataObject, "isvatcoderequired", isvatcoderequired);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateCareProviderFinanceInvoiceBatchSetup(bool issundry,
            Guid careprovidercontractschemeid, Guid? careproviderbatchgroupingid,
            DateTime startdate, TimeSpan starttime,
            int invoicebyid, int careproviderinvoicefrequencyid, int? createbatchwithin, int chargetodayid, int whentobatchfinancetransactionsid, bool useenddatewhenbatchingfinancetransactions, DateTime? financetransactionsupto, bool separateinvoices,
            Guid careproviderextractnameid, bool debtorreferencenumberrequired,
            Guid ownerid, bool isfinancecoderequired = false, bool isvatcoderequired = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "issundry", issundry);

            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractschemeid", careprovidercontractschemeid);

            if (careproviderbatchgroupingid.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "careproviderbatchgroupingid", careproviderbatchgroupingid.Value);

            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);

            AddFieldToBusinessDataObject(buisinessDataObject, "invoicebyid", invoicebyid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderinvoicefrequencyid", careproviderinvoicefrequencyid);

            if (createbatchwithin.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "createbatchwithin", createbatchwithin);

            AddFieldToBusinessDataObject(buisinessDataObject, "chargetodayid", chargetodayid);
            AddFieldToBusinessDataObject(buisinessDataObject, "whentobatchfinancetransactionsid", whentobatchfinancetransactionsid);
            AddFieldToBusinessDataObject(buisinessDataObject, "useenddatewhenbatchingfinancetransactions", useenddatewhenbatchingfinancetransactions);

            if (financetransactionsupto.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "financetransactionsupto", financetransactionsupto);

            AddFieldToBusinessDataObject(buisinessDataObject, "separateinvoices", separateinvoices);

            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextcontra", "Cancellation of Charge for {Person} for the period {Start Date} to {End Date}");
            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextsundry", "Sundry Expense of {Expense} for {Person} on {Start Date}");
            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextreduction", "Reduction to full weekly charge of {Full Net Amount} being paid by other Third Parties");

            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderextractnameid", careproviderextractnameid);
            AddFieldToBusinessDataObject(buisinessDataObject, "debtorreferencenumberrequired", debtorreferencenumberrequired);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "isfinancecoderequired", isfinancecoderequired);
            AddFieldToBusinessDataObject(buisinessDataObject, "isvatcoderequired", isvatcoderequired);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public void UpdateEndDate(Guid PrimaryKeyId, DateTime? EndDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PrimaryKeyId);

            if (EndDate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", EndDate.Value);
            else
                this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", null);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateTransactionTextStandard(Guid PrimaryKeyId, string transactiontextstandard)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PrimaryKeyId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "transactiontextstandard", transactiontextstandard);

            this.UpdateRecord(buisinessDataObject);
        }


        public List<Guid> GetByContractScheme(Guid careprovidercontractschemeid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careprovidercontractschemeid", ConditionOperatorType.Equal, careprovidercontractschemeid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Dictionary<string, object> GetByID(Guid CareProviderFinanceInvoiceBatchSetupId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderFinanceInvoiceBatchSetupId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCareProviderFinanceInvoiceBatchSetupId(Guid CareProviderFinanceInvoiceBatchSetupId)
        {
            this.DeleteRecord(TableName, CareProviderFinanceInvoiceBatchSetupId);
        }
    }
}
