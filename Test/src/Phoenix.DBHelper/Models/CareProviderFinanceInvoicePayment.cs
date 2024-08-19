using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderFinanceInvoicePayment : BaseClass
    {
        private string TableName = "CareProviderFinanceInvoicePayment";
        private string PrimaryKeyName = "CareProviderFinanceInvoicePaymentId";

        public CareProviderFinanceInvoicePayment()
        {
            AuthenticateUser();
        }

        public CareProviderFinanceInvoicePayment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateCareProviderFinanceInvoicePayment(Guid ownerid, Guid? CareProviderFinanceInvoiceId, Guid? establishmentId,
            Guid? providerorpersonid, string providerorpersonidtablename, string providerorpersonidname, string alternativeinvoicenumber,
            DateTime? paymentdate, DateTime? posteddate, decimal? paymentamount, decimal? debtwrittenoff,
            Guid? paymentmethodid, string paidby, bool totalrecord, bool allocated, string reference, Guid? allocatedfinanceinvoicepaymentid, Guid? careproviderfinanceinvoicepaymentreporttypeid = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            if (CareProviderFinanceInvoiceId.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderFinanceInvoiceId", CareProviderFinanceInvoiceId);
            if (establishmentId.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "establishmentId", establishmentId);
            if (providerorpersonid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "providerorpersonid", providerorpersonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerorpersonidtablename", providerorpersonidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerorpersonidname", providerorpersonidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "alternativeinvoicenumber", alternativeinvoicenumber);
            AddFieldToBusinessDataObject(buisinessDataObject, "paymentdate", paymentdate);
            if (posteddate.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "posteddate", posteddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "paymentamount", paymentamount);
            if (debtwrittenoff.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "debtwrittenoff", debtwrittenoff);
            AddFieldToBusinessDataObject(buisinessDataObject, "paymentmethodid", paymentmethodid);
            AddFieldToBusinessDataObject(buisinessDataObject, "paidby", paidby);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalrecord", totalrecord);
            AddFieldToBusinessDataObject(buisinessDataObject, "allocated", allocated);
            AddFieldToBusinessDataObject(buisinessDataObject, "reference", reference);
            if (allocatedfinanceinvoicepaymentid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "allocatedfinanceinvoicepaymentid", allocatedfinanceinvoicepaymentid);
            if(careproviderfinanceinvoicepaymentreporttypeid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "careproviderfinanceinvoicepaymentreporttypeid", careproviderfinanceinvoicepaymentreporttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetCareProviderFinanceInvoicePaymentById(Guid CareProviderFinanceInvoicePaymentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject("CareProviderFinanceInvoicePayment", false, "CareProviderFinanceInvoicePaymentId");

            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoicePaymentId", ConditionOperatorType.Equal, CareProviderFinanceInvoicePaymentId);

            foreach (string field in Fields)
                this.AddReturnField(query, "CareProviderFinanceInvoicePayment", field);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByInvoiceBatchId(Guid CareProviderFinanceInvoiceBatchId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoiceBatchId", ConditionOperatorType.Equal, CareProviderFinanceInvoiceBatchId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByInvoiceId(Guid CareProviderFinanceInvoiceId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoiceId", ConditionOperatorType.Equal, CareProviderFinanceInvoiceId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByInvoiceIdAndAlternativeInvoiceNumber(Guid CareProviderFinanceInvoiceId, string alternativeinvoicenumber)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoiceId", ConditionOperatorType.Equal, CareProviderFinanceInvoiceId);
            this.BaseClassAddTableCondition(query, "alternativeinvoicenumber", ConditionOperatorType.Equal, alternativeinvoicenumber);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCareProviderFinanceInvoicePaymentByProviderOrPersonId(Guid ProviderOrPersonId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerorpersonid", ConditionOperatorType.Equal, ProviderOrPersonId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByEstablishmentId(Guid establishmentid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "establishmentid", ConditionOperatorType.Equal, establishmentid);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void DeleteCareProviderFinanceInvoicePayment(Guid CareProviderFinanceInvoicePaymentID)
        {
            this.DeleteRecord("CareProviderFinanceInvoicePayment", CareProviderFinanceInvoicePaymentID);
        }

        //Get All Finance Invoice Payments sorted by Payment Date
        public List<Guid> GetAll()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("paymentdate", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public List<Guid> GetByAccountingPeriodId(Guid careprovideraccountingperiodid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careprovideraccountingperiodid", ConditionOperatorType.Equal, careprovideraccountingperiodid);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByTotalRecord_Allocated_FinanceInvoicePaymentAllocatedTo(bool totalrecord, bool allocated, Guid allocatedfinanceinvoicepaymentid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "totalrecord", ConditionOperatorType.Equal, totalrecord);
            this.BaseClassAddTableCondition(query, "allocated", ConditionOperatorType.Equal, allocated);
            this.BaseClassAddTableCondition(query, "allocatedfinanceinvoicepaymentid", ConditionOperatorType.Equal, allocatedfinanceinvoicepaymentid);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
