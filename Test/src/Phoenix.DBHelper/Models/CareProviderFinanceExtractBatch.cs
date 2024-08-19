using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderFinanceExtractBatch : BaseClass
    {
        public CareProviderFinanceExtractBatch()
        {
            AuthenticateUser();
        }

        public CareProviderFinanceExtractBatch(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }
        private string tableName = "CareProviderFinanceExtractBatch";
        private string primaryKeyName = "CareProviderFinanceExtractBatchId";

        public Guid CreateCareProviderFinanceExtractBatch(Guid ownerid, Guid extractnameid, Guid CareProviderFinanceExtractBatchsetupid,
            DateTime runon, DateTime completedon,
            string extractyear, decimal netbatchtotal, decimal grossbatchtotal, decimal vattotal, decimal totaldebits, decimal totalcredits,
            int numberofinvoicesextrated, int numberofuniquepayers, int batchstatusid, int extractmonth, int extractweek, bool isadhocbatch = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "extractnameid", extractnameid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderfinanceextractbatchsetupid", CareProviderFinanceExtractBatchsetupid);
            AddFieldToBusinessDataObject(buisinessDataObject, "runon", runon);
            AddFieldToBusinessDataObject(buisinessDataObject, "completedon", completedon);

            AddFieldToBusinessDataObject(buisinessDataObject, "extractyear", extractyear);
            AddFieldToBusinessDataObject(buisinessDataObject, "netbatchtotal", netbatchtotal);
            AddFieldToBusinessDataObject(buisinessDataObject, "grossbatchtotal", grossbatchtotal);
            AddFieldToBusinessDataObject(buisinessDataObject, "vattotal", vattotal);
            AddFieldToBusinessDataObject(buisinessDataObject, "totaldebits", totaldebits);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalcredits", totalcredits);
            AddFieldToBusinessDataObject(buisinessDataObject, "numberofinvoicesextrated", numberofinvoicesextrated);
            AddFieldToBusinessDataObject(buisinessDataObject, "numberofuniquepayers", numberofuniquepayers);
            AddFieldToBusinessDataObject(buisinessDataObject, "batchstatusid", batchstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "extractmonth", extractmonth);
            AddFieldToBusinessDataObject(buisinessDataObject, "extractweek", extractweek);
            AddFieldToBusinessDataObject(buisinessDataObject, "isadhocbatch", isadhocbatch);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);


            return this.CreateRecord(buisinessDataObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CareProviderFinanceExtractBatchSetupId"></param>
        /// <param name="BatchStatusId">1:New | 2:Completed</param>
        /// <returns></returns>
        public List<Guid> GetCareProviderFinanceExtractBatchByCareProviderFinanceExtractBatchSetupId(Guid CareProviderFinanceExtractBatchSetupId, int BatchStatusId = 1)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceExtractBatchSetupId", ConditionOperatorType.Equal, CareProviderFinanceExtractBatchSetupId);
            this.BaseClassAddTableCondition(query, "BatchStatusId", ConditionOperatorType.Equal, BatchStatusId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(Guid CareProviderFinanceExtractBatchSetupId, Guid extractnameid, int BatchStatusId = 1)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceExtractBatchSetupId", ConditionOperatorType.Equal, CareProviderFinanceExtractBatchSetupId);
            this.BaseClassAddTableCondition(query, "extractnameid", ConditionOperatorType.Equal, extractnameid);
            this.BaseClassAddTableCondition(query, "BatchStatusId", ConditionOperatorType.Equal, BatchStatusId);

            query.Orders.Add(new OrderBy("batchid", SortOrder.Descending, tableName));

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(Guid CareProviderFinanceExtractBatchSetupId, Guid extractnameid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceExtractBatchSetupId", ConditionOperatorType.Equal, CareProviderFinanceExtractBatchSetupId);
            this.BaseClassAddTableCondition(query, "extractnameid", ConditionOperatorType.Equal, extractnameid);

            query.Orders.Add(new OrderBy("batchid", SortOrder.Descending, tableName));

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractNameAndIsAdhocBatch(Guid CareProviderFinanceExtractBatchSetupId, Guid extractnameid, bool isadhocbatch, int BatchStatusId = 1)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderFinanceExtractBatchSetupId", ConditionOperatorType.Equal, CareProviderFinanceExtractBatchSetupId);
            this.BaseClassAddTableCondition(query, "extractnameid", ConditionOperatorType.Equal, extractnameid);
            this.BaseClassAddTableCondition(query, "isadhocbatch", ConditionOperatorType.Equal, isadhocbatch);
            this.BaseClassAddTableCondition(query, "BatchStatusId", ConditionOperatorType.Equal, BatchStatusId);

            query.Orders.Add(new OrderBy("batchid", SortOrder.Descending, tableName));

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public int GetCareProviderFinanceExtractBatchStatusId(Guid CareProviderFinanceExtractBatchId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CareProviderFinanceExtractBatchId);

            this.AddReturnField(query, tableName, "BatchStatusId");

            return this.ExecuteDataQueryAndExtractIntFields(query, "BatchStatusId").FirstOrDefault();
        }

        public Dictionary<string, object> GetCareProviderFinanceExtractBatchById(Guid CareProviderFinanceExtractBatchId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CareProviderFinanceExtractBatchId);

            foreach (string field in Fields)
                this.AddReturnField(query, tableName, field);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateCareProviderFinanceExtractBatchRunOnDate(Guid CareProviderFinanceExtractBatchId, DateTime RunOn)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderFinanceExtractBatchId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RunOn", DataType.DateTime, BusinessObjectFieldType.Unknown, false, RunOn);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDownloaded(Guid CareProviderFinanceExtractBatchId, bool isdownloaded)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderFinanceExtractBatchId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "isdownloaded", DataType.DateTime, BusinessObjectFieldType.Unknown, false, isdownloaded);

            this.UpdateRecord(buisinessDataObject);
        }



        public List<Guid> GetAllCareProviderFinanceExtractBatch()
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            query.Orders.Add(new OrderBy("batchid", SortOrder.Descending, tableName));

            this.AddReturnField(query, tableName, primaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetAllRecordsNeverDownloaded()
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "isdownloaded", ConditionOperatorType.Equal, false);
            this.BaseClassAddTableCondition(query, "extractcontent", ConditionOperatorType.NotNull);
            this.BaseClassAddTableCondition(query, "directdebitbatch", ConditionOperatorType.Equal, false);

            query.Orders.Add(new OrderBy("batchid", SortOrder.Descending, tableName));

            this.AddReturnField(query, tableName, primaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public void DeleteCareProviderFinanceExtractBatch(Guid CareProviderFinanceExtractBatchID)
        {
            this.DeleteRecord(tableName, CareProviderFinanceExtractBatchID);
        }



    }
}
