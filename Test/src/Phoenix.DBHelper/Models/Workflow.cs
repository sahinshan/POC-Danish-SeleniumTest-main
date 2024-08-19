using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class Workflow : BaseClass
    {
        public string TableName { get { return "Workflow"; } }
        public string PrimaryKeyName { get { return "Workflowid"; } }


        public Workflow()
        {
            AuthenticateUser();
        }

        public Workflow(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWorkflowRecord(string Name, string Description, Guid OwnerId, Guid BusinessObjectId, string WorkflowXml,
            int ScopeId, int TypeId, bool RecordIsCreated, bool RecordStatusChanges, bool RecordIsAssigned, bool RecordFieldsChange,
            bool RecordIsDeleted, bool IsChildProcess, bool IsOnDemandProcess, bool AutoDeleteCompletedJobs, bool Published)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Description", Description);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "BusinessObjectId", BusinessObjectId);
            AddFieldToBusinessDataObject(dataObject, "WorkflowXml", WorkflowXml);
            AddFieldToBusinessDataObject(dataObject, "ScopeId", ScopeId);
            AddFieldToBusinessDataObject(dataObject, "TypeId", TypeId);
            AddFieldToBusinessDataObject(dataObject, "RecordIsCreated", RecordIsCreated);
            AddFieldToBusinessDataObject(dataObject, "RecordStatusChanges", RecordStatusChanges);
            AddFieldToBusinessDataObject(dataObject, "RecordIsAssigned", RecordIsAssigned);
            AddFieldToBusinessDataObject(dataObject, "RecordFieldsChange", RecordFieldsChange);
            AddFieldToBusinessDataObject(dataObject, "RecordIsDeleted", RecordIsDeleted);
            AddFieldToBusinessDataObject(dataObject, "IsChildProcess", IsChildProcess);
            AddFieldToBusinessDataObject(dataObject, "IsOnDemandProcess", IsOnDemandProcess);
            AddFieldToBusinessDataObject(dataObject, "AutoDeleteCompletedJobs", AutoDeleteCompletedJobs);
            AddFieldToBusinessDataObject(dataObject, "Published", Published);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreatePostSyncWorkflowRecord(string Name, string Description, Guid OwnerId, Guid BusinessObjectId, string WorkflowXml)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Description", Description);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "BusinessObjectId", BusinessObjectId);
            AddFieldToBusinessDataObject(dataObject, "WorkflowXml", WorkflowXml);

            AddFieldToBusinessDataObject(dataObject, "Published", 1);
            AddFieldToBusinessDataObject(dataObject, "ScopeId", 4);
            AddFieldToBusinessDataObject(dataObject, "TypeId", 2);
            AddFieldToBusinessDataObject(dataObject, "RecordIsCreated", 1);
            AddFieldToBusinessDataObject(dataObject, "RecordStatusChanges", 0);
            AddFieldToBusinessDataObject(dataObject, "RecordIsAssigned", 0);
            AddFieldToBusinessDataObject(dataObject, "RecordFieldsChange", 0);
            AddFieldToBusinessDataObject(dataObject, "RecordIsDeleted", 0);
            AddFieldToBusinessDataObject(dataObject, "IsChildProcess", 0);
            AddFieldToBusinessDataObject(dataObject, "IsOnDemandProcess", 0);
            AddFieldToBusinessDataObject(dataObject, "AutoDeleteCompletedJobs", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }


        public Guid CreateASyncWorkflowRecord(string Name, string Description, Guid OwnerId, Guid BusinessObjectId, string WorkflowXml)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Description", Description);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "BusinessObjectId", BusinessObjectId);
            AddFieldToBusinessDataObject(dataObject, "WorkflowXml", WorkflowXml);

            AddFieldToBusinessDataObject(dataObject, "Published", 1);
            AddFieldToBusinessDataObject(dataObject, "ScopeId", 1);
            AddFieldToBusinessDataObject(dataObject, "TypeId", 1);
            AddFieldToBusinessDataObject(dataObject, "RecordIsCreated", 1);
            AddFieldToBusinessDataObject(dataObject, "RecordStatusChanges", 0);
            AddFieldToBusinessDataObject(dataObject, "RecordIsAssigned", 0);
            AddFieldToBusinessDataObject(dataObject, "RecordFieldsChange", 0);
            AddFieldToBusinessDataObject(dataObject, "RecordIsDeleted", 0);
            AddFieldToBusinessDataObject(dataObject, "IsChildProcess", 0);
            AddFieldToBusinessDataObject(dataObject, "IsOnDemandProcess", 0);
            AddFieldToBusinessDataObject(dataObject, "AutoDeleteCompletedJobs", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateASyncWorkflowRecord(string Name, string Description, Guid OwnerId, Guid BusinessObjectId, string WorkflowXml, bool recordIsCreated, bool recordFieldsChange, string updateFields)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Description", Description);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "BusinessObjectId", BusinessObjectId);
            AddFieldToBusinessDataObject(dataObject, "WorkflowXml", WorkflowXml);

            AddFieldToBusinessDataObject(dataObject, "Published", 1);
            AddFieldToBusinessDataObject(dataObject, "ScopeId", 1);
            AddFieldToBusinessDataObject(dataObject, "TypeId", 1);
            AddFieldToBusinessDataObject(dataObject, "RecordIsCreated", recordIsCreated);
            AddFieldToBusinessDataObject(dataObject, "RecordStatusChanges", 0);
            AddFieldToBusinessDataObject(dataObject, "RecordIsAssigned", 0);
            AddFieldToBusinessDataObject(dataObject, "RecordFieldsChange", recordFieldsChange);
            AddFieldToBusinessDataObject(dataObject, "UpdateFields", updateFields);
            AddFieldToBusinessDataObject(dataObject, "RecordIsDeleted", 0);
            AddFieldToBusinessDataObject(dataObject, "IsChildProcess", 0);
            AddFieldToBusinessDataObject(dataObject, "IsOnDemandProcess", 0);
            AddFieldToBusinessDataObject(dataObject, "AutoDeleteCompletedJobs", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetWorkflowByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetWorkflowByBusinessObjectId(Guid BusinessObjectId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "BusinessObjectId", ConditionOperatorType.Equal, BusinessObjectId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetWorkflowByID(Guid WorkflowId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, WorkflowId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateWorkflowXmlField(Guid WorkflowId, string WorkflowXml)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, WorkflowId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "WorkflowXml", WorkflowXml);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePublishedField(Guid WorkflowId, bool Published)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, WorkflowId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "published", Published);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateOwner(Guid WorkflowId, Guid ownerid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, WorkflowId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateWaitFields(Guid WorkflowId, int? beforendays, int? afterndays, Guid? datefieldid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, WorkflowId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "beforendays", beforendays);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "afterndays", afterndays);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "datefieldid", datefieldid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteWorkflow(Guid WorkflowId)
        {
            this.DeleteRecord(TableName, WorkflowId);
        }

        public void ImportWorkflowUsingPlatformAPI(byte[] Document, string FileName)
        {
            base.ImportDocument(Document, FileName);
        }
    }
}
