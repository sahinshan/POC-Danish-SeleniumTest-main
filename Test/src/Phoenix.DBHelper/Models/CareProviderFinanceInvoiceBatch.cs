using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderFinanceInvoiceBatch : BaseClass
    {
        private string TableName = "CareProviderFinanceInvoiceBatch";
        private string PrimaryKeyName = "CareProviderFinanceInvoiceBatchId";

        public CareProviderFinanceInvoiceBatch()
        {
            AuthenticateUser();
        }

        public CareProviderFinanceInvoiceBatch(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareProviderFinanceInvoiceBatch(Guid ownerid, Guid careproviderfinanceinvoicebatchsetupid, int batchstatusid,
            Guid careprovidercontractschemeid, bool isadhocbatch, Guid careproviderbatchgroupingid,
            DateTime runontime, DateTime periodstartdate, DateTime periodenddate, int whentobatchfinancetransactionsid,
            int? numberofinvoicescreated, int? numberofinvoicescancelled,
            decimal? netbatchtotal, decimal? grossbatchtotal, decimal? vattotal)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderfinanceinvoicebatchsetupid", careproviderfinanceinvoicebatchsetupid);
            AddFieldToBusinessDataObject(buisinessDataObject, "batchstatusid", batchstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractschemeid", careprovidercontractschemeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "isadhocbatch", isadhocbatch);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderbatchgroupingid", careproviderbatchgroupingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "runontime", runontime);
            AddFieldToBusinessDataObject(buisinessDataObject, "periodstartdate", periodstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "periodenddate", periodenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "whentobatchfinancetransactionsid", whentobatchfinancetransactionsid);

            AddFieldToBusinessDataObject(buisinessDataObject, "netbatchtotal", netbatchtotal);
            AddFieldToBusinessDataObject(buisinessDataObject, "grossbatchtotal", grossbatchtotal);
            AddFieldToBusinessDataObject(buisinessDataObject, "vattotal", vattotal);
            AddFieldToBusinessDataObject(buisinessDataObject, "numberofinvoicescreated", numberofinvoicescreated);
            AddFieldToBusinessDataObject(buisinessDataObject, "numberofinvoicescancelled", numberofinvoicescancelled);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetActiveFinanceInvoiceBatchBySetupID(Guid CareProviderFinanceInvoiceBatchSetupId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoiceBatchSetupId", ConditionOperatorType.Equal, CareProviderFinanceInvoiceBatchSetupId);
            this.BaseClassAddTableCondition(query, "BatchStatusId", ConditionOperatorType.Equal, 1);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetFinanceInvoiceBatchBySetupIDAndStatusID(Guid CareProviderFinanceInvoiceBatchSetupId, int batchstatusid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoiceBatchSetupId", ConditionOperatorType.Equal, CareProviderFinanceInvoiceBatchSetupId);
            this.BaseClassAddTableCondition(query, "BatchStatusId", ConditionOperatorType.Equal, batchstatusid);

            this.AddReturnField(query, TableName, PrimaryKeyName);
            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateFinanceInvoiceBatchDates(Guid CareProviderFinanceInvoiceBatchid, DateTime RunOn, DateTime? PeriodStartDate, DateTime? PeriodEndDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "RunOn", DataType.DateTime, BusinessObjectFieldType.Unknown, false, RunOn);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderFinanceInvoiceBatchid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderFinanceInvoiceBatchid);

            if (PeriodStartDate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "PeriodStartDate", DataType.Date, BusinessObjectFieldType.Unknown, false, PeriodStartDate);

            if (PeriodEndDate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "PeriodEndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, PeriodEndDate);


            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateRunOnDate(Guid CareProviderFinanceInvoiceBatchid, DateTime RunOn)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "RunOn", DataType.DateTime, BusinessObjectFieldType.Unknown, false, RunOn);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderFinanceInvoiceBatchid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderFinanceInvoiceBatchid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateRunOnTime(Guid CareProviderFinanceInvoiceBatchid, DateTime RunOnTime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "RunOnTime", DataType.DateTime, BusinessObjectFieldType.Unknown, false, RunOnTime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderFinanceInvoiceBatchid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderFinanceInvoiceBatchid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateFinanceInvoiceBatchDatesAndStatus(int BatchStatusId, Guid CareProviderFinanceInvoiceBatchid, DateTime RunOnTime, DateTime? PeriodStartDate, DateTime? PeriodEndDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderFinanceInvoiceBatchid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderFinanceInvoiceBatchid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RunOnTime", DataType.DateTime, BusinessObjectFieldType.Unknown, false, RunOnTime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PeriodStartDate", DataType.Date, BusinessObjectFieldType.Unknown, false, PeriodStartDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PeriodEndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, PeriodEndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "batchstatusid", DataType.Integer, BusinessObjectFieldType.Unknown, false, BatchStatusId);

            this.UpdateRecord(buisinessDataObject);
        }


        public Dictionary<string, object> GetByFinanceInvoiceBatchId(Guid CareProviderFinanceInvoiceBatchid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderFinanceInvoiceBatchid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public int GetHighestBatchID()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);
            this.AddReturnField(query, TableName, "careproviderfinanceinvoicebatchnumber");

            query.Orders.Add(new OrderBy("careproviderfinanceinvoicebatchnumber", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractIntFields(query, "careproviderfinanceinvoicebatchnumber").FirstOrDefault();
        }

        public List<Guid> GetByFinanceInvoiceBatchSetupID(Guid CareProviderFinanceInvoiceBatchSetupId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoiceBatchSetupId", ConditionOperatorType.Equal, CareProviderFinanceInvoiceBatchSetupId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByFinanceInvoiceBatchSetupIDSortByBatchId(Guid CareProviderFinanceInvoiceBatchSetupId, string sortOrder)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoiceBatchSetupId", ConditionOperatorType.Equal, CareProviderFinanceInvoiceBatchSetupId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            if (sortOrder.Equals("increasing") || sortOrder.Equals("ascending"))
                query.Orders.Add(new OrderBy("careproviderfinanceinvoicebatchnumber", SortOrder.Ascending, TableName));
            else
                query.Orders.Add(new OrderBy("careproviderfinanceinvoicebatchnumber", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        //Get By Finance invoice batch setup id and isadhocbatch
        public List<Guid> GetByFinanceInvoiceBatchSetupIDAndIsAdHocBatch(Guid CareProviderFinanceInvoiceBatchSetupId, bool IsAdHocBatch)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoiceBatchSetupId", ConditionOperatorType.Equal, CareProviderFinanceInvoiceBatchSetupId);
            this.BaseClassAddTableCondition(query, "IsAdHocBatch", ConditionOperatorType.Equal, IsAdHocBatch);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByFinanceInvoiceBatchSetupIDSortedByRunOn(Guid CareProviderFinanceInvoiceBatchSetupId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceInvoiceBatchSetupId", ConditionOperatorType.Equal, CareProviderFinanceInvoiceBatchSetupId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("runon", SortOrder.Ascending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public List<Guid> GetCompletedBatchedByFinanceInvoiceBatchSetupIDSortedByRunOn()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "BatchStatusId", ConditionOperatorType.Equal, 2);

            this.AddReturnField(query, TableName, PrimaryKeyName);
            query.Orders.Add(new OrderBy("runon", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        //update whentobatchfinancetransactionsid field
        public void UpdateWhenToBatchFinanceTransactions(Guid CareProviderFinanceInvoiceBatchid, int WhenToBatchFinanceTransactionsID)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderFinanceInvoiceBatchid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderFinanceInvoiceBatchid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "whentobatchfinancetransactionsid", DataType.Integer, BusinessObjectFieldType.Unknown, false, WhenToBatchFinanceTransactionsID);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetAll()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAllByAscendingRunOn(bool Inactive)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Inactive", ConditionOperatorType.Equal, Inactive);

            query.Orders.Add(new OrderBy("runon", SortOrder.Ascending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAllByDescendingRunOn(bool Inactive)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Inactive", ConditionOperatorType.Equal, Inactive);

            query.Orders.Add(new OrderBy("runon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
    }
}
