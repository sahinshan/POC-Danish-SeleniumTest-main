using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinanceInvoice : BaseClass
    {
        private string TableName = "FinanceInvoice";
        private string PrimaryKeyName = "FinanceInvoiceId";

        public FinanceInvoice()
        {
            AuthenticateUser();
        }

        public FinanceInvoice(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateFinanceInvoice(Guid ownerid, Guid invoicebatchid, Guid invoicestatusid, Guid serviceelement1id, Guid paymenttypeid, Guid financeextractid, Guid providerorpersonid,
            string providerorpersonidtablename, string providerorpersonidname, string creditorreferencenumber,
            DateTime invoicedate, DateTime transactionsupto,
            decimal netamount, decimal vatamount, decimal grossamount)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("FinanceInvoice", "FinanceInvoiceId");

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "invoicebatchid", invoicebatchid);
            AddFieldToBusinessDataObject(buisinessDataObject, "invoicestatusid", invoicestatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "paymenttypeid", paymenttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeextractid", financeextractid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerorpersonid", providerorpersonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerorpersonidtablename", providerorpersonidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerorpersonidname", providerorpersonidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "invoicedate", invoicedate);
            AddFieldToBusinessDataObject(buisinessDataObject, "transactionsupto", transactionsupto);
            AddFieldToBusinessDataObject(buisinessDataObject, "creditorreferencenumber", creditorreferencenumber);
            AddFieldToBusinessDataObject(buisinessDataObject, "netamount", netamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatamount", vatamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "grossamount", grossamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitstotalhours", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitstotaldays", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitstotalsessions", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitstotalmeals", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitstotaljourneys", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitstotalunits", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalunits", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "financemoduleid", 2);
            AddFieldToBusinessDataObject(buisinessDataObject, "valuesverified", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);



            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateFinanceInvoice(Guid ownerid, Guid invoicebatchid, Guid invoicestatusid, Guid serviceelement1id, Guid paymenttypeid, Guid? financeextractid, Guid providerorpersonid,
            string providerorpersonidtablename, string providerorpersonidname,
            DateTime invoicedate, DateTime transactionsupto,
            decimal netamount, decimal vatamount, decimal grossamount, bool valuesverified = true)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("FinanceInvoice", "FinanceInvoiceId");

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "invoicebatchid", invoicebatchid);
            AddFieldToBusinessDataObject(buisinessDataObject, "invoicestatusid", invoicestatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "paymenttypeid", paymenttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeextractid", financeextractid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerorpersonid", providerorpersonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerorpersonidtablename", providerorpersonidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerorpersonidname", providerorpersonidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "invoicedate", invoicedate);
            AddFieldToBusinessDataObject(buisinessDataObject, "transactionsupto", transactionsupto);
            AddFieldToBusinessDataObject(buisinessDataObject, "netamount", netamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatamount", vatamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "grossamount", grossamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitstotalhours", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitstotaldays", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitstotalsessions", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitstotalmeals", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitstotaljourneys", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "unitstotalunits", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalunits", 0.00);
            AddFieldToBusinessDataObject(buisinessDataObject, "financemoduleid", 2);
            AddFieldToBusinessDataObject(buisinessDataObject, "valuesverified", valuesverified);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);



            return this.CreateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetFinanceInvoiceById(Guid FinanceInvoiceId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject("FinanceInvoice", false, "FinanceInvoiceId");

            this.BaseClassAddTableCondition(query, "FinanceInvoiceId", ConditionOperatorType.Equal, FinanceInvoiceId);

            foreach (string field in Fields)
                this.AddReturnField(query, "FinanceInvoice", field);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetFinanceInvoiceByInvoiceBatchId(Guid InvoiceBatchId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "InvoiceBatchId", ConditionOperatorType.Equal, InvoiceBatchId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetFinanceInvoiceByInvoiceStatusId(Guid InvoiceStatusId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "InvoiceStatusId", ConditionOperatorType.Equal, InvoiceStatusId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetFinanceInvoiceByInvoiceStatusIdAndFinanceExtractId(Guid InvoiceStatusId, Guid FinanceExtractId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "InvoiceStatusId", ConditionOperatorType.Equal, InvoiceStatusId);
            this.BaseClassAddTableCondition(query, "FinanceExtractId", ConditionOperatorType.Equal, FinanceExtractId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetFinanceInvoiceByProviderOrPersonId(Guid ProviderOrPersonId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerorpersonid", ConditionOperatorType.Equal, ProviderOrPersonId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("invoicenumber", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void DeleteFinanceInvoice(Guid FinanceInvoiceID)
        {
            this.DeleteRecord("FinanceInvoice", FinanceInvoiceID);
        }

        public void UpdateProviderInvoiceNumber(Guid FinanceInvoiceId, string ProviderInvoiceNumber)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "financeinvoiceid", FinanceInvoiceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "providerinvoicenumber", ProviderInvoiceNumber);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateInvoiceReceivedDate(Guid FinanceInvoiceId, DateTime InvoiceReceivedDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "financeinvoiceid", FinanceInvoiceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "invoicereceiveddate", InvoiceReceivedDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void SetValuesVerifiedField(Guid FinanceInvoiceId, bool ValuesVerified)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "financeinvoiceid", FinanceInvoiceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "valuesverified", ValuesVerified);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateInvoiceStatus(Guid FinanceInvoiceId, Guid InvoiceStatusId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "financeinvoiceid", FinanceInvoiceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "invoicestatusid", InvoiceStatusId);

            this.UpdateRecord(buisinessDataObject);
        }

    }
}
