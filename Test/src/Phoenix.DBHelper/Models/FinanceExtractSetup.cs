using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinanceExtractSetup : BaseClass
    {
        private string TableName = "FinanceExtractSetup";
        private string PrimaryKeyName = "FinanceExtractSetupId";

        public FinanceExtractSetup()
        {
            AuthenticateUser();
        }

        public FinanceExtractSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateFinanceExtractSetup(Guid ownerid, Guid owningbusinessunitid, int financemoduleid,
            DateTime startdate, TimeSpan starttime, Guid extractnameid, int extractfrequencyid,
            bool monday, bool tuesday, bool wednesday, bool thursday, bool friday, bool saturday, bool sunday,
            int fileformatid = 1, bool excludezerotransactions = true, bool separatecreditextract = false, bool extractcreditinvoices = true,
            string vatglcode = "", string extractreference = "")
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(businessDataObject, "financemoduleid", financemoduleid);
            AddFieldToBusinessDataObject(businessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(businessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(businessDataObject, "extractnameid", extractnameid);
            AddFieldToBusinessDataObject(businessDataObject, "extractfrequencyid", extractfrequencyid);

            AddFieldToBusinessDataObject(businessDataObject, "monday", monday);
            AddFieldToBusinessDataObject(businessDataObject, "tuesday", tuesday);
            AddFieldToBusinessDataObject(businessDataObject, "wednesday", wednesday);
            AddFieldToBusinessDataObject(businessDataObject, "thursday", thursday);
            AddFieldToBusinessDataObject(businessDataObject, "friday", friday);
            AddFieldToBusinessDataObject(businessDataObject, "saturday", saturday);
            AddFieldToBusinessDataObject(businessDataObject, "sunday", sunday);

            AddFieldToBusinessDataObject(businessDataObject, "fileformatid", fileformatid);

            AddFieldToBusinessDataObject(businessDataObject, "excludezerotransactions", excludezerotransactions);
            AddFieldToBusinessDataObject(businessDataObject, "separatecreditextract", separatecreditextract);
            AddFieldToBusinessDataObject(businessDataObject, "extractcreditinvoices", extractcreditinvoices);
            AddFieldToBusinessDataObject(businessDataObject, "vatglcode", vatglcode);
            AddFieldToBusinessDataObject(businessDataObject, "extractreference", extractreference);

            AddFieldToBusinessDataObject(businessDataObject, "validforexport", false);
            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);

            return this.CreateRecord(businessDataObject);
        }

        public void UpdateFinanceExtractSetupExcludeZeroTransactions(Guid FinanceExtractSetupId, bool ExcludeZeroTransactions)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("FinanceExtractSetup", "FinanceExtractSetupid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "FinanceExtractSetupid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinanceExtractSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ExcludeZeroTransactions", DataType.Boolean, BusinessObjectFieldType.Unknown, false, ExcludeZeroTransactions);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateFinanceExtractSetupExtractCreditInvoices(Guid FinanceExtractSetupId, bool ExtractCreditInvoices)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("FinanceExtractSetup", "FinanceExtractSetupid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "FinanceExtractSetupid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinanceExtractSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ExtractCreditInvoices", DataType.Boolean, BusinessObjectFieldType.Unknown, false, ExtractCreditInvoices);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateFinanceExtractSetupSeparateCreditExtract(Guid FinanceExtractSetupId, bool SeparateCreditExtract)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("FinanceExtractSetup", "FinanceExtractSetupid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "FinanceExtractSetupid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinanceExtractSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "SeparateCreditExtract", DataType.Boolean, BusinessObjectFieldType.Unknown, false, SeparateCreditExtract);

            this.UpdateRecord(buisinessDataObject);
        }


        public List<Guid> GetByBusinessUnitIdAndFinanceModuleId(Guid OwnerId, Guid OwningBusinessUnitId, int FinanceModuleId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "OwnerId", ConditionOperatorType.Equal, OwnerId);
            this.BaseClassAddTableCondition(query, "OwningBusinessUnitId", ConditionOperatorType.Equal, OwningBusinessUnitId);
            this.BaseClassAddTableCondition(query, "FinanceModuleId", ConditionOperatorType.Equal, FinanceModuleId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetFinanceExtractSetupByFinanceModuleId(int FinanceModuleId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "FinanceModuleId", ConditionOperatorType.Equal, FinanceModuleId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
