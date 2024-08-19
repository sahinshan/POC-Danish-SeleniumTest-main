using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderFinanceTransaction : BaseClass
    {
        //rename all FinanceTransaction references to CareProviderFinanceTransaction
        public string TableName = "careproviderfinancetransaction";
        public string PrimaryKeyName = "careproviderfinancetransactionid";

        public CareProviderFinanceTransaction() { }

        public CareProviderFinanceTransaction(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Dictionary<string, object> GetFinanceTransactionById(Guid CareProviderFinanceTransactionId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderFinanceTransactionId);

            foreach (string field in Fields)
                this.AddReturnField(query, TableName, field);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public List<Guid> GetFinanceInvoiceIDForFinanceTransaction(Guid CareProviderFinanceTransactionId)
        {
            DataQuery query = this.GetDataQueryObject("CareProviderFinanceTransaction", false, "CareProviderFinanceTransactionId");
            this.BaseClassAddTableCondition(query, "CareProviderFinanceTransactionId", ConditionOperatorType.Equal, CareProviderFinanceTransactionId);
            this.AddReturnField(query, "CareProviderFinanceTransaction", "CareProviderFinanceInvoiceId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "CareProviderFinanceInvoiceId");
        }

        public List<Guid> GetFinanceInvoiceBatchIDForFinanceTransaction(Guid CareProviderFinanceTransactionId)
        {
            DataQuery query = this.GetDataQueryObject("CareProviderFinanceTransaction", false, "CareProviderFinanceTransactionId");
            this.BaseClassAddTableCondition(query, "CareProviderFinanceTransactionId", ConditionOperatorType.Equal, CareProviderFinanceTransactionId);
            this.AddReturnField(query, "CareProviderFinanceTransaction", "CareProviderFinanceInvoiceBatchId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "CareProviderFinanceInvoiceBatchId");
        }

        public List<Guid> GetFinanceExtractIDForFinanceTransaction(Guid CareProviderFinanceTransactionId)
        {
            DataQuery query = this.GetDataQueryObject("CareProviderFinanceTransaction", false, "CareProviderFinanceTransactionId");
            this.BaseClassAddTableCondition(query, "CareProviderFinanceTransactionId", ConditionOperatorType.Equal, CareProviderFinanceTransactionId);
            this.AddReturnField(query, "CareProviderFinanceTransaction", "CareProviderFinanceExtractId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "CareProviderFinanceExtractId");
        }

        public List<Guid> GetFinanceTransactionByInvoiceID(Guid CareProviderFinanceInvoiceId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoiceId", ConditionOperatorType.Equal, CareProviderFinanceInvoiceId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetFinanceTransactionByFinanceInvoiceIdAndStartDateAndEndDate(Guid CareProviderFinanceInvoiceId, DateTime startdate, DateTime enddate)
        {
            DataQuery query = this.GetDataQueryObject("CareProviderFinanceTransaction", false, "CareProviderFinanceTransactionId");
            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoiceId", ConditionOperatorType.Equal, CareProviderFinanceInvoiceId);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.Equal, startdate);
            this.BaseClassAddTableCondition(query, "enddate", ConditionOperatorType.Equal, enddate);
            this.AddReturnField(query, "CareProviderFinanceTransaction", "CareProviderFinanceTransactionid");

            query.Orders.Add(new OrderBy("careproviderfinancetransactionnumber", SortOrder.Descending, "CareProviderFinanceTransaction"));

            return this.ExecuteDataQueryAndExtractGuidFields(query, "CareProviderFinanceTransactionid");
        }

        public List<Guid> GetFinanceTransactionByFinanceExtractBatchIdAndStartDateAndEndDate(Guid CareProviderFinanceExtractBatchId, DateTime startdate, DateTime enddate)
        {
            DataQuery query = this.GetDataQueryObject("CareProviderFinanceTransaction", false, "CareProviderFinanceTransactionId");
            this.BaseClassAddTableCondition(query, "CareProviderFinanceExtractBatchId", ConditionOperatorType.Equal, CareProviderFinanceExtractBatchId);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.Equal, startdate);
            this.BaseClassAddTableCondition(query, "enddate", ConditionOperatorType.Equal, enddate);
            this.AddReturnField(query, "CareProviderFinanceTransaction", "CareProviderFinanceTransactionid");

            query.Orders.Add(new OrderBy("careproviderfinancetransactionnumber", SortOrder.Descending, "CareProviderFinanceTransaction"));

            return this.ExecuteDataQueryAndExtractGuidFields(query, "CareProviderFinanceTransactionid");
        }

        public List<Guid> GetFinanceTransactionByFinanceInvoiceIdAndFinanceExtractBatchIdAndStartDateAndEndDate(Guid CareProviderFinanceInvoiceId, Guid CareProviderFinanceExtractBatchId, DateTime startdate, DateTime enddate)
        {
            DataQuery query = this.GetDataQueryObject("CareProviderFinanceTransaction", false, "CareProviderFinanceTransactionId");
            this.BaseClassAddTableCondition(query, "careproviderfinanceinvoiceid", ConditionOperatorType.Equal, CareProviderFinanceInvoiceId);
            this.BaseClassAddTableCondition(query, "careproviderfinanceextractbatchid", ConditionOperatorType.Equal, CareProviderFinanceExtractBatchId);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.Equal, startdate);
            this.BaseClassAddTableCondition(query, "enddate", ConditionOperatorType.Equal, enddate);
            this.AddReturnField(query, "CareProviderFinanceTransaction", "CareProviderFinanceTransactionid");

            query.Orders.Add(new OrderBy("careproviderfinancetransactionnumber", SortOrder.Descending, "CareProviderFinanceTransaction"));

            return this.ExecuteDataQueryAndExtractGuidFields(query, "CareProviderFinanceTransactionid");
        }

        public List<Guid> GetFinanceTransactionByFinanceInvoiceIdAndFinanceExtractBatchId(Guid CareProviderFinanceInvoiceId, Guid CareProviderFinanceExtractBatchId)
        {
            DataQuery query = this.GetDataQueryObject("CareProviderFinanceTransaction", false, "CareProviderFinanceTransactionId");
            this.BaseClassAddTableCondition(query, "careproviderfinanceinvoiceid", ConditionOperatorType.Equal, CareProviderFinanceInvoiceId);
            this.BaseClassAddTableCondition(query, "careproviderfinanceextractbatchid", ConditionOperatorType.Equal, CareProviderFinanceExtractBatchId);
            this.AddReturnField(query, "CareProviderFinanceTransaction", "CareProviderFinanceTransactionid");

            query.Orders.Add(new OrderBy("careproviderfinancetransactionnumber", SortOrder.Descending, "CareProviderFinanceTransaction"));

            return this.ExecuteDataQueryAndExtractGuidFields(query, "CareProviderFinanceTransactionid");
        }

        public void DeleteFinanceTransaction(Guid CareProviderFinanceTransactionID)
        {
            this.DeleteRecord("CareProviderFinanceTransaction", CareProviderFinanceTransactionID);
        }

        public List<Guid> GetByFinanceInvoiceBatchId(Guid CareProviderFinanceInvoiceBatchId)
        {
            DataQuery query = this.GetDataQueryObject("CareProviderFinanceTransaction", false, "CareProviderFinanceTransactionId");
            this.BaseClassAddTableCondition(query, "careproviderfinanceinvoicebatchid", ConditionOperatorType.Equal, CareProviderFinanceInvoiceBatchId);
            this.AddReturnField(query, "CareProviderFinanceTransaction", "CareProviderFinanceTransactionid");

            query.Orders.Add(new OrderBy("careproviderfinancetransactionnumber", SortOrder.Descending, "CareProviderFinanceTransaction"));

            return this.ExecuteDataQueryAndExtractGuidFields(query, "CareProviderFinanceTransactionid");
        }

        public List<Guid> GetByPersonId(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("careproviderfinancetransactionnumber", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPersonContractService(Guid careproviderpersoncontractserviceid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careproviderpersoncontractserviceid", ConditionOperatorType.Equal, careproviderpersoncontractserviceid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("careproviderfinancetransactionnumber", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByCareProviderContractService(Guid careprovidercontractserviceid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careprovidercontractserviceid", ConditionOperatorType.Equal, careprovidercontractserviceid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("careproviderfinancetransactionnumber", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByInvoiceIDAndConfirmedValue(Guid CareProviderFinanceInvoiceId, bool confirmed)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoiceId", ConditionOperatorType.Equal, CareProviderFinanceInvoiceId);
            this.BaseClassAddTableCondition(query, "confirmed", ConditionOperatorType.Equal, confirmed);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPersonContractServiceAndStartDateAndEndDate(Guid careproviderpersoncontractserviceid, DateTime startdate, DateTime enddate)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careproviderpersoncontractserviceid", ConditionOperatorType.Equal, careproviderpersoncontractserviceid);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.Equal, startdate);
            this.BaseClassAddTableCondition(query, "enddate", ConditionOperatorType.Equal, enddate);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("careproviderfinancetransactionnumber", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPersonContractServiceAndTransactionClassId(Guid careproviderpersoncontractserviceid, int TransactionClassId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "careproviderpersoncontractserviceid", ConditionOperatorType.Equal, careproviderpersoncontractserviceid);
            this.BaseClassAddTableCondition(query, "TransactionClassId", ConditionOperatorType.Equal, TransactionClassId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("careproviderfinancetransactionnumber", SortOrder.Ascending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
