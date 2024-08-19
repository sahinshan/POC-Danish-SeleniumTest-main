using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class FinanceExtract : BaseClass
    {
        public FinanceExtract()
        {
            AuthenticateUser();
        }

        public FinanceExtract(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }
        private string tableName = "FinanceExtract";
        private string primaryKeyName = "FinanceExtractId";

        public Guid CreateFinanceExtract(Guid ownerid, Guid extractnameid, Guid financeextractsetupid,
            DateTime runon, DateTime completedon, DateTime originalrunon,
            string extractyear, decimal netamount, decimal grossamount, decimal totalcredits, decimal vatamount, decimal totaldebits,
            int numberofinvoicesextrated, int numberofuniqueproviderspayees, int financemoduleid, int batchstatusid, int extractmonth, int extractweek, int batchid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "extractnameid", extractnameid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financeextractsetupid", financeextractsetupid);
            AddFieldToBusinessDataObject(buisinessDataObject, "runon", runon);
            AddFieldToBusinessDataObject(buisinessDataObject, "completedon", completedon);
            AddFieldToBusinessDataObject(buisinessDataObject, "originalrunon", originalrunon);
            AddFieldToBusinessDataObject(buisinessDataObject, "extractyear", extractyear);
            AddFieldToBusinessDataObject(buisinessDataObject, "netamount", netamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "grossamount", grossamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "totalcredits", totalcredits);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatamount", vatamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "totaldebits", totaldebits);
            AddFieldToBusinessDataObject(buisinessDataObject, "numberofinvoicesextrated", numberofinvoicesextrated);
            AddFieldToBusinessDataObject(buisinessDataObject, "numberofuniqueproviderspayees", numberofuniqueproviderspayees);
            AddFieldToBusinessDataObject(buisinessDataObject, "financemoduleid", financemoduleid);
            AddFieldToBusinessDataObject(buisinessDataObject, "batchstatusid", batchstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "extractmonth", extractmonth);
            AddFieldToBusinessDataObject(buisinessDataObject, "extractweek", extractweek);
            AddFieldToBusinessDataObject(buisinessDataObject, "batchid", batchid);
            AddFieldToBusinessDataObject(buisinessDataObject, "adhoc", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);


            return this.CreateRecord(buisinessDataObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FinanceExtractSetupId"></param>
        /// <param name="BatchStatusId">1:New | 2:Completed</param>
        /// <returns></returns>
        public List<Guid> GetFinanceExtractByFinanceExtractSetupId(Guid FinanceExtractSetupId, int BatchStatusId = 1)
        {
            DataQuery query = this.GetDataQueryObject("FinanceExtract", false, "FinanceExtractId");

            this.BaseClassAddTableCondition(query, "FinanceExtractSetupId", ConditionOperatorType.Equal, FinanceExtractSetupId);
            this.BaseClassAddTableCondition(query, "BatchStatusId", ConditionOperatorType.Equal, BatchStatusId);

            this.AddReturnField(query, "FinanceExtract", "FinanceExtractid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinanceExtractId");
        }


        public int GetFinanceExtractStatusId(Guid FinanceExtractId)
        {
            DataQuery query = this.GetDataQueryObject("FinanceExtract", false, "FinanceExtractId");

            this.BaseClassAddTableCondition(query, "FinanceExtractId", ConditionOperatorType.Equal, FinanceExtractId);

            this.AddReturnField(query, "FinanceExtract", "BatchStatusId");

            return this.ExecuteDataQueryAndExtractIntFields(query, "BatchStatusId").FirstOrDefault();
        }

        public Dictionary<string, object> GetFinanceExtractById(Guid FinanceExtractId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject("FinanceExtract", false, "FinanceExtractId");

            this.BaseClassAddTableCondition(query, "FinanceExtractId", ConditionOperatorType.Equal, FinanceExtractId);

            foreach (string field in Fields)
                this.AddReturnField(query, "FinanceExtract", field);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateFinanceExtractRunOnDate(Guid FinanceExtractId, DateTime RunOnDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("FinanceExtract", "FinanceExtractid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "FinanceExtractid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinanceExtractId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RunOn", DataType.DateTime, BusinessObjectFieldType.Unknown, false, RunOnDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OriginalRunOn", DataType.DateTime, BusinessObjectFieldType.Unknown, false, RunOnDate);

            this.UpdateRecord(buisinessDataObject);
        }


        public void DeleteFinanceExtract(Guid FinanceExtractID)
        {
            this.DeleteRecord("FinanceExtract", FinanceExtractID);
        }



    }
}
