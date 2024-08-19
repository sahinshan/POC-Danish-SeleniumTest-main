using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ComplianceManagement : BaseClass
    {

        public string TableName = "ComplianceManagement";
        public string PrimaryKeyName = "ComplianceManagementId";


        public ComplianceManagement()
        {
            AuthenticateUser();
        }

        public ComplianceManagement(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateComplianceManagement(Guid complianceid, DateTime chaseddate, Guid requirementlastchasedoutcomeid, Guid chasedbyid, Guid ownerid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "complianceid", complianceid);
            AddFieldToBusinessDataObject(dataObject, "chaseddate", chaseddate);
            AddFieldToBusinessDataObject(dataObject, "requirementlastchasedoutcomeid", requirementlastchasedoutcomeid);
            AddFieldToBusinessDataObject(dataObject, "chasedbyid", chasedbyid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "validforexport", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByComplienceId(Guid complianceid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "complianceid", ConditionOperatorType.Equal, complianceid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid ComplianceManagementId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ComplianceManagementId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteComplianceManagement(Guid ComplianceManagementId)
        {
            this.DeleteRecord(TableName, ComplianceManagementId);
        }
    }
}
