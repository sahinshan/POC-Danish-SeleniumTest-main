using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinanceInvoiceBatch : BaseClass
    {
        private string TableName = "FinanceInvoiceBatch";
        private string PrimaryKeyName = "FinanceInvoiceBatchId";

        public FinanceInvoiceBatch()
        {
            AuthenticateUser();
        }

        public FinanceInvoiceBatch(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateFinanceInvoiceBatch(Guid ownerid, Guid financeinvoicebatchsetupid, Guid serviceelement1id, Guid paymenttypeid, Guid providerbatchgroupingid,
            DateTime runontime, DateTime periodstartdate, DateTime periodenddate,
            int? financeinvoicebatchnumber, int batchstatusid, int financemoduleid, int? numberofinvoicescreated, int? numberofuniqueproviderspayees,
            decimal? netbatchtotal, decimal? grossbatchtotal, decimal? vattotal)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeinvoicebatchsetupid", financeinvoicebatchsetupid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            AddFieldToBusinessDataObject(buisinessDataObject, "paymenttypeid", paymenttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerbatchgroupingid", providerbatchgroupingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "runontime", runontime);
            AddFieldToBusinessDataObject(buisinessDataObject, "periodstartdate", periodstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "periodenddate", periodenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeinvoicebatchnumber", financeinvoicebatchnumber);
            AddFieldToBusinessDataObject(buisinessDataObject, "batchstatusid", batchstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financemoduleid", financemoduleid);
            AddFieldToBusinessDataObject(buisinessDataObject, "netbatchtotal", netbatchtotal);
            AddFieldToBusinessDataObject(buisinessDataObject, "grossbatchtotal", grossbatchtotal);
            AddFieldToBusinessDataObject(buisinessDataObject, "vattotal", vattotal);
            AddFieldToBusinessDataObject(buisinessDataObject, "numberofinvoicescreated", numberofinvoicescreated);
            AddFieldToBusinessDataObject(buisinessDataObject, "numberofuniqueproviderspayees", numberofuniqueproviderspayees);
            AddFieldToBusinessDataObject(buisinessDataObject, "isadhocbatch", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetActiveFinanceInvoiceBatchBySetupID(Guid FinanceInvoiceBatchSetupId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "FinanceInvoiceBatchSetupId", ConditionOperatorType.Equal, FinanceInvoiceBatchSetupId);
            this.BaseClassAddTableCondition(query, "BatchStatusId", ConditionOperatorType.Equal, 1);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetActiveFinanceInvoiceBatchBySetupIDAndStatusID(Guid FinanceInvoiceBatchSetupId, int batchstatusid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "FinanceInvoiceBatchSetupId", ConditionOperatorType.Equal, FinanceInvoiceBatchSetupId);
            this.BaseClassAddTableCondition(query, "BatchStatusId", ConditionOperatorType.Equal, batchstatusid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateFinanceInvoiceBatchDates(Guid FinanceInvoiceBatchId, DateTime? PeriodEndDate, DateTime RunOnTime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "FinanceInvoiceBatchid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinanceInvoiceBatchId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PeriodEndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, PeriodEndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RunOnTime", DataType.DateTime, BusinessObjectFieldType.Unknown, false, RunOnTime);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateFinanceInvoiceBatchDatesAndStatus(int BatchStatusId, Guid FinanceInvoiceBatchId, DateTime? PeriodEndDate, DateTime RunOnTime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "FinanceInvoiceBatchid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinanceInvoiceBatchId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PeriodEndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, PeriodEndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RunOnTime", DataType.DateTime, BusinessObjectFieldType.Unknown, false, RunOnTime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "batchstatusid", DataType.Integer, BusinessObjectFieldType.Unknown, false, BatchStatusId);

            this.UpdateRecord(buisinessDataObject);
        }


        public Dictionary<string, object> GetByFinanceInvoiceBatchId(Guid FinanceInvoiceBatchId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, FinanceInvoiceBatchId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }


    }
}
