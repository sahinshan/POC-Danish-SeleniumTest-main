using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AssessmentPrintRecord : BaseClass
    {

        private string tableName = "AssessmentPrintRecord";
        private string primaryKeyName = "AssessmentPrintRecordId";

        public AssessmentPrintRecord()
        {
            AuthenticateUser();
        }

        public AssessmentPrintRecord(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetAssessmentPrintRecordForCaseForm(Guid AssessmentId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "AssessmentId", ConditionOperatorType.Equal, AssessmentId);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, tableName));
            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetAssessmentPrintRecordForCaseForm(Guid CaseFormID, bool CheckIfContainsFile, params string[] fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "AssessmentId", ConditionOperatorType.Equal, CaseFormID);

            if (CheckIfContainsFile)
                this.BaseClassAddTableCondition(query, "FileId", ConditionOperatorType.NotNull);

            this.AddReturnField(query, tableName, primaryKeyName);

            var results = this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);

            if (results.Count > 0)
                return GetAssessmentPrintRecordByID(results[0], fields);
            else
                return new Dictionary<string, object>();
        }

        public Dictionary<string, object> GetAssessmentPrintRecordByID(Guid AssessmentPrintRecordId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, AssessmentPrintRecordId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public void DeleteAssessmentPrintRecord(Guid AssessmentPrintRecordID)
        {
            this.DeleteRecord(tableName, AssessmentPrintRecordID);
        }

        public Guid CreateAssessmentPrintRecord(Guid ownerid, Guid OwningBusinessUnitId, string Title, string Comments, string PrintTemplateName, Guid? AssessmentSectionId, Guid AssessmentId, string AssessmentIdTableName, int PrintAuditOnly)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Title", Title);
            AddFieldToBusinessDataObject(buisinessDataObject, "Comments", Comments);
            AddFieldToBusinessDataObject(buisinessDataObject, "PrintTemplateName", PrintTemplateName);
            AddFieldToBusinessDataObject(buisinessDataObject, "AssessmentSectionId", AssessmentSectionId);
            AddFieldToBusinessDataObject(buisinessDataObject, "AssessmentId", AssessmentId);
            AddFieldToBusinessDataObject(buisinessDataObject, "AssessmentIdTableName", AssessmentIdTableName);
            AddFieldToBusinessDataObject(buisinessDataObject, "PrintAuditOnly", PrintAuditOnly);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

    }
}
