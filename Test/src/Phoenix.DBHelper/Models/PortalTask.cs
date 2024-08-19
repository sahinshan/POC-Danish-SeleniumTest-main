using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PortalTask : BaseClass
    {

        private string tableName = "PortalTask";
        private string primaryKeyName = "PortalTaskId";

        public PortalTask()
        {
            AuthenticateUser();
        }

        public PortalTask(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePortalTask(Guid ownerid, DateTime duedate, int portaltaskactionid, int portaltaskstatusid, string title, Guid websiteid,
            Guid targetuserid, string targetuseridtablename, string targetuseridname,
            Guid? recordid, string recordidtablename, string recordidname,
            Guid? targetpageid, Guid? workflowid)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "duedate", duedate);
            AddFieldToBusinessDataObject(dataObject, "portaltaskactionid", portaltaskactionid);
            AddFieldToBusinessDataObject(dataObject, "portaltaskstatusid", portaltaskstatusid);
            AddFieldToBusinessDataObject(dataObject, "name", title);
            AddFieldToBusinessDataObject(dataObject, "websiteid", websiteid);
            AddFieldToBusinessDataObject(dataObject, "targetuserid", targetuserid);
            AddFieldToBusinessDataObject(dataObject, "targetuseridtablename", targetuseridtablename);
            AddFieldToBusinessDataObject(dataObject, "targetuseridname", targetuseridname);
            AddFieldToBusinessDataObject(dataObject, "recordid", recordid);
            AddFieldToBusinessDataObject(dataObject, "recordidtablename", recordidtablename);
            AddFieldToBusinessDataObject(dataObject, "recordidname", recordidname);
            AddFieldToBusinessDataObject(dataObject, "targetpageid", targetpageid);
            AddFieldToBusinessDataObject(dataObject, "workflowid", workflowid);


            return this.CreateRecord(dataObject);
        }

        public void UpdateDueDate(Guid PortalTaskId, DateTime DueDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PortalTaskId);

            AddFieldToBusinessDataObject(buisinessDataObject, "DueDate", DueDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateStatus(Guid PortalTaskId, int portaltaskstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PortalTaskId);

            AddFieldToBusinessDataObject(buisinessDataObject, "portaltaskstatusid", portaltaskstatusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByTargetUserId(Guid TargetUserId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "TargetUserId", ConditionOperatorType.Equal, TargetUserId);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, this.tableName));

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByRecordId(Guid recordid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "recordid", ConditionOperatorType.Equal, recordid);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, this.tableName));

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByTargetUserIdAndTitle(Guid TargetUserId, string title)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "TargetUserId", ConditionOperatorType.Equal, TargetUserId);
            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, title);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, this.tableName));

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByTargetUserIdAndWorkflowId(Guid TargetUserId, Guid workflowid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "TargetUserId", ConditionOperatorType.Equal, TargetUserId);
            this.BaseClassAddTableCondition(query, "workflowid", ConditionOperatorType.Equal, workflowid);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, this.tableName));

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid PortalTaskId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, PortalTaskId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePortalTask(Guid PortalTaskID)
        {
            this.DeleteRecord(tableName, PortalTaskID);
        }



    }
}
