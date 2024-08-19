using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderFinanceInvoice : BaseClass
    {
        private string TableName = "careproviderfinanceinvoice";
        private string PrimaryKeyName = "careproviderfinanceinvoiceid";

        public CareProviderFinanceInvoice()
        {
            AuthenticateUser();
        }

        public CareProviderFinanceInvoice(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateCareProviderFinanceInvoice(Guid ownerid, Guid? careproviderfinanceinvoicebatchid, Guid? financeinvoicestatusid,
            Guid? careproviderfinanceextractbatchid, string DebtorReferenceNumber, Guid? careprovidercontractschemeid, Guid? careproviderbatchgroupingid,
            Guid? establishmentproviderid, Guid? funderproviderid, Guid? personid,
            DateTime chargesupto, Guid provideropersonid, string providerorpersonidtablename, string providerorpersonidname,
            decimal netamount, decimal vatamount, decimal grossamount, bool includeindebtoutstanding = true)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderfinanceinvoicebatchid", careproviderfinanceinvoicebatchid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeinvoicestatusid", financeinvoicestatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderfinanceextractbatchid", careproviderfinanceextractbatchid);
            AddFieldToBusinessDataObject(buisinessDataObject, "debtorreferencenumber", DebtorReferenceNumber);
            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractschemeid", careprovidercontractschemeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderbatchgroupingid", careproviderbatchgroupingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "establishmentproviderid", establishmentproviderid);
            AddFieldToBusinessDataObject(buisinessDataObject, "funderproviderid", funderproviderid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "chargesupto", chargesupto);
            AddFieldToBusinessDataObject(buisinessDataObject, "netamount", netamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatamount", vatamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "grossamount", grossamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "provideropersonid", provideropersonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerorpersonidtablename", providerorpersonidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerorpersonidname", providerorpersonidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "valuesverified", includeindebtoutstanding);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);



            return this.CreateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetCareProviderFinanceInvoiceById(Guid CareProviderFinanceInvoiceId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderFinanceInvoiceId);

            foreach (string field in Fields)
                this.AddReturnField(query, TableName, field);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetCareProviderFinanceInvoiceByInvoiceBatchId(Guid careproviderfinanceinvoicebatchid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careproviderfinanceinvoicebatchid", ConditionOperatorType.Equal, careproviderfinanceinvoicebatchid);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("invoicenumber", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCareProviderFinanceInvoiceByInvoiceStatusId(int FinanceInvoiceStatusId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "FinanceInvoiceStatusId", ConditionOperatorType.Equal, FinanceInvoiceStatusId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(Guid careproviderfinanceinvoicebatchid, int FinanceInvoiceStatusId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careproviderfinanceinvoicebatchid", ConditionOperatorType.Equal, careproviderfinanceinvoicebatchid);
            this.BaseClassAddTableCondition(query, "financeinvoicestatusid", ConditionOperatorType.Equal, FinanceInvoiceStatusId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("careproviderfinanceinvoicenumber", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCareProviderFinanceInvoiceByFinanceInvoiceStatusIdAndFinanceExtractBatchId(int financeinvoicestatusid, Guid CareProviderFinanceExtractBatchId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "financeinvoicestatusid", ConditionOperatorType.Equal, financeinvoicestatusid);
            this.BaseClassAddTableCondition(query, "CareProviderFinanceExtractBatchId", ConditionOperatorType.Equal, CareProviderFinanceExtractBatchId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCareProviderFinanceInvoiceByProviderOrPersonId(Guid ProviderOrPersonId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerorpersonid", ConditionOperatorType.Equal, ProviderOrPersonId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("careproviderfinanceinvoicenumber", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPayerAndFinanceInvoiceStatusId(Guid ProviderOrPersonId, int FinanceInvoiceStatusId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerorpersonid", ConditionOperatorType.Equal, ProviderOrPersonId);
            this.BaseClassAddTableCondition(query, "financeinvoicestatusid", ConditionOperatorType.Equal, FinanceInvoiceStatusId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("careproviderfinanceinvoicenumber", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void DeleteCareProviderFinanceInvoice(Guid CareProviderFinanceInvoiceID)
        {
            this.DeleteRecord(TableName, CareProviderFinanceInvoiceID);
        }

        public void UpdateInvoiceStatus(Guid CareProviderFinanceInvoiceId, int FinanceInvoiceStatusId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderFinanceInvoiceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "financeinvoicestatusid", FinanceInvoiceStatusId);

            this.UpdateRecord(buisinessDataObject);
        }

    }
}
