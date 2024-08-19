using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ComplianceItemAttachment : BaseClass
    {

        public string TableName = "ComplianceItemAttachment";
        public string PrimaryKeyName = "complianceitemattachmentid";

        public ComplianceItemAttachment()
        {
            AuthenticateUser();
        }

        public ComplianceItemAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetComplianceItemAttachmentById(Guid complianceid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "complianceitemattachmentid", ConditionOperatorType.Equal, complianceid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetComplianceItemAttachmentIdByTitle(string title)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, title);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByComplianceItemAttachmentID(Guid complianceid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, complianceid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetComplianceItemAttachmentByComplianceItemId(Guid ComplianceItemId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ComplianceItemId", ConditionOperatorType.Equal, ComplianceItemId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAll()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteComplianceItemAttachment(Guid complianceid)
        {
            this.DeleteRecord(TableName, complianceid);
        }

    }
}
