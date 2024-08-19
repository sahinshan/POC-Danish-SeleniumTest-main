using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class WorkflowJob : BaseClass
    {
        public string TableName { get { return "WorkflowJob"; } }
        public string PrimaryKeyName { get { return "WorkflowJobid"; } }


        public WorkflowJob()
        {
            AuthenticateUser();
        }

        public WorkflowJob(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWorkflowJobRecord(Guid WorkflowId, Guid RegardingId, string RegardingIdTableName, string RegardingIdName, string BusinessData, string ResultXml, DateTime StartedOn, DateTime? CompletedOn, int StatusId, int RunOrdered)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "WorkflowId", WorkflowId);
            AddFieldToBusinessDataObject(dataObject, "RegardingId", RegardingId);
            AddFieldToBusinessDataObject(dataObject, "RegardingIdTableName", RegardingIdTableName);
            AddFieldToBusinessDataObject(dataObject, "RegardingIdName", RegardingIdName);
            AddFieldToBusinessDataObject(dataObject, "BusinessData", BusinessData);
            AddFieldToBusinessDataObject(dataObject, "ResultXml", ResultXml);
            AddFieldToBusinessDataObject(dataObject, "StartedOn", StartedOn);
            if (CompletedOn.HasValue)
                AddFieldToBusinessDataObject(dataObject, "CompletedOn", CompletedOn.Value);
            AddFieldToBusinessDataObject(dataObject, "StatusId", StatusId);
            AddFieldToBusinessDataObject(dataObject, "RunOrdered", RunOrdered);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);


            return this.CreateRecord(dataObject);
        }

        public void WaitForWorkflowJobFinishedExecuting(Guid WorkflowJobID)
        {
            int count = 0;
            int notStartedStatusID = 1;
            int inProgressStatusID = 2;
            int statusID = GetWorkflowJobStatusByWorkflowJobID(WorkflowJobID);

            while (statusID == notStartedStatusID || statusID == inProgressStatusID)
            {
                count++;
                if (count > 120)
                    throw new Exception("Workflow Job did not finish executing after 120 seconds");

                System.Threading.Thread.Sleep(1000); //wait for 1 second
                statusID = GetWorkflowJobStatusByWorkflowJobID(WorkflowJobID); //get the job status again
            }
        }

        public int GetWorkflowJobStatusByWorkflowJobID(Guid WorkflowJobID)
        {
            DataQuery query = this.GetDataQueryObject("WorkflowJob", false, "WorkflowJobId");
            this.BaseClassAddTableCondition(query, "WorkflowJobId", ConditionOperatorType.Equal, WorkflowJobID);
            this.AddReturnField(query, "WorkflowJob", "StatusId");

            return this.ExecuteDataQueryAndExtractIntFields(query, "StatusId").FirstOrDefault();
        }

        public List<Guid> GetWorkflowJobByWorkflowId(Guid WorkflowId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            BaseClassAddTableCondition(query, "WorkflowId", ConditionOperatorType.Equal, WorkflowId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetWorkflowJobByWorkflowId(Guid WorkflowId, int statusid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            BaseClassAddTableCondition(query, "WorkflowId", ConditionOperatorType.Equal, WorkflowId);
            BaseClassAddTableCondition(query, "statusid", ConditionOperatorType.Equal, statusid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByWorkflowIdAndRegardingId(Guid WorkflowId, Guid RegardingId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            BaseClassAddTableCondition(query, "WorkflowId", ConditionOperatorType.Equal, WorkflowId);
            BaseClassAddTableCondition(query, "RegardingId", ConditionOperatorType.Equal, RegardingId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetWorkflowJobByID(Guid WorkflowJobId, params string[] FieldsToReturn)
        {
            var query = base.GetDataQueryObject(TableName, false, PrimaryKeyName);

            base.AddReturnFields(query, TableName, FieldsToReturn);

            base.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, WorkflowJobId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateWorkflowJobXmlField(Guid WorkflowJobId, string WorkflowJobXml)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, WorkflowJobId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "WorkflowJobXml", WorkflowJobXml);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCompletedOnField(Guid WorkflowJobId, DateTime CompletedOn)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, WorkflowJobId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CompletedOn", CompletedOn);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteWorkflowJob(Guid WorkflowJobId)
        {
            this.DeleteRecord(TableName, WorkflowJobId);
        }
    }
}
