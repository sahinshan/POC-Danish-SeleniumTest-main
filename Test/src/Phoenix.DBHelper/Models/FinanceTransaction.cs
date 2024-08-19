using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinanceTransaction : BaseClass
    {
        public FinanceTransaction()
        {
            AuthenticateUser();
        }

        public FinanceTransaction(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateFinanceTransaction(Guid ownerid, Guid personid, Guid vatcodeid, Guid serviceprovisionid, Guid providerid,
            Guid serviceelement1id, Guid rateunitid, Guid financeinvoicebatchsetupid, Guid serviceelement2id, Guid financeclientcategoryid,
            Guid paymenttypecodeid, Guid financeinvoiceid, Guid invoicestatusid, Guid financeinvoicebatchid, Guid financeextractid,
            DateTime startdate, DateTime enddate,
            decimal netamount, decimal vatamount, decimal grossamount, decimal totalunits,
            string glcode, string vatreference, string invoiceno,
            int personnumber)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("FinanceTransaction", "FinanceTransactionId");

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatcodeid", vatcodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionid", serviceprovisionid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeinvoicebatchsetupid", financeinvoicebatchsetupid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeclientcategoryid", financeclientcategoryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "paymenttypecodeid", paymenttypecodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeinvoiceid", financeinvoiceid);
            AddFieldToBusinessDataObject(buisinessDataObject, "invoicestatusid", invoicestatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeinvoicebatchid", financeinvoicebatchid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeextractid", financeextractid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "netamount", netamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatamount", vatamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "grossamount", grossamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalunits", totalunits);
            AddFieldToBusinessDataObject(buisinessDataObject, "glcode", glcode);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatreference", vatreference);
            AddFieldToBusinessDataObject(buisinessDataObject, "invoiceno", invoiceno);
            AddFieldToBusinessDataObject(buisinessDataObject, "personnumber", personnumber);
            AddFieldToBusinessDataObject(buisinessDataObject, "transactionclassid", 5);
            AddFieldToBusinessDataObject(buisinessDataObject, "transactiontypeid", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "whotopayid", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "contracttypeid", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "financemoduleid", 2);
            AddFieldToBusinessDataObject(buisinessDataObject, "hascontra", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "informationonly", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "updatedbyuser", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }


        public Guid CreateAdditionalTransaction(
            Guid OwnerId,
            Guid PersonId,
            Guid VatCodeId,
            Guid ServiceProvisionId,
            Guid ProviderId,
            Guid ServiceElement1Id,
            Guid FinanceInvoiceBatchSetupId,
            Guid ServiceElement2Id,
            Guid FinanceClientCategoryId,
            Guid PaymentTypeCodeId,
            Guid FinanceInvoiceId,
            Guid InvoiceStatusId,
            Guid FinanceInvoiceBatchId,
            decimal NetAmount,
            decimal VatAmount,
            decimal GrossAmount,
            DateTime StartDate,
            DateTime EndDate,
            int PersonNumber,
            string GlCode,
            string VatReference)
        {

            BusinessData record = GetBusinessDataBaseObject("FinanceTransaction", "FinanceTransactionId");

            this.AddFieldToBusinessDataObject(record, "OwnerId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, OwnerId);
            this.AddFieldToBusinessDataObject(record, "PersonId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, PersonId);
            this.AddFieldToBusinessDataObject(record, "VatCodeId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, VatCodeId);
            this.AddFieldToBusinessDataObject(record, "ServiceProvisionId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ServiceProvisionId);
            this.AddFieldToBusinessDataObject(record, "ProviderId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ProviderId);
            this.AddFieldToBusinessDataObject(record, "ServiceElement1Id", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ServiceElement1Id);
            this.AddFieldToBusinessDataObject(record, "FinanceInvoiceBatchSetupId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinanceInvoiceBatchSetupId);
            this.AddFieldToBusinessDataObject(record, "ServiceElement2Id", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ServiceElement2Id);
            this.AddFieldToBusinessDataObject(record, "FinanceClientCategoryId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinanceClientCategoryId);
            this.AddFieldToBusinessDataObject(record, "PaymentTypeCodeId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, PaymentTypeCodeId);
            this.AddFieldToBusinessDataObject(record, "FinanceInvoiceId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinanceInvoiceId);
            this.AddFieldToBusinessDataObject(record, "InvoiceStatusId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, InvoiceStatusId);
            this.AddFieldToBusinessDataObject(record, "FinanceInvoiceBatchId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinanceInvoiceBatchId);

            this.AddFieldToBusinessDataObject(record, "NetAmount", DataType.Decimal, BusinessObjectFieldType.Unknown, false, NetAmount);
            this.AddFieldToBusinessDataObject(record, "VatAmount", DataType.Decimal, BusinessObjectFieldType.Unknown, false, VatAmount);
            this.AddFieldToBusinessDataObject(record, "GrossAmount", DataType.Decimal, BusinessObjectFieldType.Unknown, false, GrossAmount);


            this.AddFieldToBusinessDataObject(record, "StartDate", DataType.Date, BusinessObjectFieldType.Unknown, false, StartDate);
            this.AddFieldToBusinessDataObject(record, "EndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, EndDate);


            this.AddFieldToBusinessDataObject(record, "PersonNumber", DataType.Integer, BusinessObjectFieldType.Unknown, false, PersonNumber);

            this.AddFieldToBusinessDataObject(record, "GlCode", DataType.Text, BusinessObjectFieldType.Unknown, false, GlCode);
            this.AddFieldToBusinessDataObject(record, "VatReference", DataType.Text, BusinessObjectFieldType.Unknown, false, VatReference);

            this.AddFieldToBusinessDataObject(record, "TransactionClassId", DataType.Integer, BusinessObjectFieldType.Unknown, false, 2);
            this.AddFieldToBusinessDataObject(record, "FinanceModuleId", DataType.Integer, BusinessObjectFieldType.Unknown, false, 2);
            this.AddFieldToBusinessDataObject(record, "TransactionTypeId", DataType.Integer, BusinessObjectFieldType.Unknown, false, 1);
            this.AddFieldToBusinessDataObject(record, "ContractTypeId", DataType.Integer, BusinessObjectFieldType.Unknown, false, 1);
            this.AddFieldToBusinessDataObject(record, "WhoToPayId", DataType.Integer, BusinessObjectFieldType.Unknown, false, 1);

            this.AddFieldToBusinessDataObject(record, "Inactive", DataType.Boolean, BusinessObjectFieldType.Unknown, false, 0);
            this.AddFieldToBusinessDataObject(record, "InformationOnly", DataType.Boolean, BusinessObjectFieldType.Unknown, false, 0);
            this.AddFieldToBusinessDataObject(record, "HasContra", DataType.Boolean, BusinessObjectFieldType.Unknown, false, 0);
            this.AddFieldToBusinessDataObject(record, "UpdatedByUser", DataType.Boolean, BusinessObjectFieldType.Unknown, false, 0);

            return this.CreateRecord(record);
        }


        public Dictionary<string, object> GetFinanceTransactionById(Guid FinanceTransactionId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");

            this.BaseClassAddTableCondition(query, "FinanceTransactionId", ConditionOperatorType.Equal, FinanceTransactionId);

            foreach (string field in Fields)
                this.AddReturnField(query, "FinanceTransaction", field);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public List<Guid> GetFinanceTransactionByServiceProvisionID(Guid ServiceProvisionID)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");
            this.BaseClassAddTableCondition(query, "ServiceProvisionId", ConditionOperatorType.Equal, ServiceProvisionID);
            this.AddReturnField(query, "FinanceTransaction", "FinanceTransactionid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceTransactionid");
        }

        public List<Guid> GetFinanceInvoiceIDForFinanceTransaction(Guid FinanceTransactionID)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");
            this.BaseClassAddTableCondition(query, "FinanceTransactionid", ConditionOperatorType.Equal, FinanceTransactionID);
            this.AddReturnField(query, "FinanceTransaction", "FinanceTransactionId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceInvoiceId");
        }

        public List<Guid> GetFinanceInvoiceBatchIDForFinanceTransaction(Guid FinanceTransactionID)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");
            this.BaseClassAddTableCondition(query, "FinanceTransactionid", ConditionOperatorType.Equal, FinanceTransactionID);
            this.AddReturnField(query, "FinanceTransaction", "FinanceInvoiceBatchId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceInvoiceBatchId");
        }

        public List<Guid> GetFinanceExtractIDForFinanceTransaction(Guid FinanceTransactionID)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");
            this.BaseClassAddTableCondition(query, "FinanceTransactionid", ConditionOperatorType.Equal, FinanceTransactionID);
            this.AddReturnField(query, "FinanceTransaction", "FinanceExtractId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceExtractId");
        }

        public List<Guid> GetFinanceTransactionByFinanceInvoiceIdAndTransactionClassId(Guid FinanceInvoiceId, int TransactionClassId)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");
            this.BaseClassAddTableCondition(query, "FinanceInvoiceId", ConditionOperatorType.Equal, FinanceInvoiceId);
            this.BaseClassAddTableCondition(query, "TransactionClassId", ConditionOperatorType.Equal, TransactionClassId);
            this.AddReturnField(query, "FinanceTransaction", "FinanceTransactionid");

            query.Orders.Add(new OrderBy("transactionnumber", SortOrder.Ascending, "FinanceTransaction"));

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceTransactionid");
        }

        public List<Guid> GetFinanceTransactionByInvoiceID(Guid FinanceInvoiceId)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");
            this.BaseClassAddTableCondition(query, "FinanceInvoiceId", ConditionOperatorType.Equal, FinanceInvoiceId);
            this.AddReturnField(query, "FinanceTransaction", "FinanceInvoiceId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceTransactionId");
        }

        public List<Guid> GetFinanceTransactionByServiceProvidedId(Guid ServiceProvidedId)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");
            this.BaseClassAddTableCondition(query, "serviceprovidedid", ConditionOperatorType.Equal, ServiceProvidedId);
            this.AddReturnField(query, "FinanceTransaction", "serviceprovidedid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceTransactionId");
        }

        public List<Guid> GetFinanceTransactionByServiceProvisionIdAndTransactionClassId(Guid ServiceProvisionId, int TransactionClassId)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");
            this.BaseClassAddTableCondition(query, "ServiceProvisionId", ConditionOperatorType.Equal, ServiceProvisionId);
            this.BaseClassAddTableCondition(query, "TransactionClassId", ConditionOperatorType.Equal, TransactionClassId);
            this.AddReturnField(query, "FinanceTransaction", "FinanceTransactionid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceTransactionid");
        }

        public List<Guid> GetFinanceTransactionByServiceProvisionIdAndStartDateAndEndDate(Guid ServiceProvisionId, DateTime startdate, DateTime enddate)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");
            this.BaseClassAddTableCondition(query, "ServiceProvisionId", ConditionOperatorType.Equal, ServiceProvisionId);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.Equal, startdate);
            this.BaseClassAddTableCondition(query, "enddate", ConditionOperatorType.Equal, enddate);
            this.AddReturnField(query, "FinanceTransaction", "FinanceTransactionid");

            query.Orders.Add(new OrderBy("transactionnumber", SortOrder.Descending, "FinanceTransaction"));

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceTransactionid");
        }

        public List<Guid> GetFinanceTransactionByFinanceInvoiceIdAndStartDateAndEndDate(Guid FinanceInvoiceId, DateTime startdate, DateTime enddate)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");
            this.BaseClassAddTableCondition(query, "FinanceInvoiceId", ConditionOperatorType.Equal, FinanceInvoiceId);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.Equal, startdate);
            this.BaseClassAddTableCondition(query, "enddate", ConditionOperatorType.Equal, enddate);
            this.AddReturnField(query, "FinanceTransaction", "FinanceTransactionid");

            query.Orders.Add(new OrderBy("transactionnumber", SortOrder.Descending, "FinanceTransaction"));

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceTransactionid");
        }

        public List<Guid> GetFinanceTransactionByServiceProvidedIdAndStartDateAndEndDate(Guid ServiceProvidedId, DateTime startdate, DateTime enddate)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");
            this.BaseClassAddTableCondition(query, "serviceprovidedid", ConditionOperatorType.Equal, ServiceProvidedId);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.Equal, startdate);
            this.BaseClassAddTableCondition(query, "enddate", ConditionOperatorType.Equal, enddate);
            this.AddReturnField(query, "FinanceTransaction", "FinanceTransactionid");

            query.Orders.Add(new OrderBy("transactionnumber", SortOrder.Descending, "FinanceTransaction"));

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceTransactionid");
        }

        public List<Guid> GetFinanceTransactionByServiceProvidedIdAndTransactionClassId(Guid ServiceProvidedId, int TransactionClassId)
        {
            DataQuery query = this.GetDataQueryObject("FinanceTransaction", false, "FinanceTransactionId");
            this.BaseClassAddTableCondition(query, "serviceprovidedid", ConditionOperatorType.Equal, ServiceProvidedId);
            this.BaseClassAddTableCondition(query, "TransactionClassId", ConditionOperatorType.Equal, TransactionClassId);
            this.AddReturnField(query, "FinanceTransaction", "FinanceTransactionid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceTransactionid");
        }

        public void DeleteFinanceTransaction(Guid FinanceTransactionID)
        {
            this.DeleteRecord("FinanceTransaction", FinanceTransactionID);
        }



    }
}
