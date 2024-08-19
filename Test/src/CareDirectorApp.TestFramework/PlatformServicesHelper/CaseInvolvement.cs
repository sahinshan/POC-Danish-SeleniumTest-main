using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public class CaseInvolvement : BaseClass
    {

        public string TableName = "CaseInvolvement";
        public string PrimaryKeyName = "CaseInvolvementId";
        

        public CaseInvolvement(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreateCaseInvolvement(Guid ownerid, Guid involvementmemberid, string involvementmemberidtablename, string involvementmemberidname, 
            Guid involvementroleid, DateTime startdate, Guid caseid, Guid personid, bool islegitimaterelationship)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "involvementmemberid", involvementmemberid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "involvementmemberidtablename", involvementmemberidtablename);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "involvementmemberidname", involvementmemberidname);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "involvementroleid", involvementroleid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caseid", caseid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "islegitimaterelationship", islegitimaterelationship);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetCaseInvolvementsByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCaseInvolvementsByCaseAndUserID(Guid CaseID, Guid UserID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);
            this.AddTableCondition(query, "InvolvementMemberId", ConditionOperatorType.Equal, UserID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCaseInvolvementByID(Guid CaseInvolvementId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseInvolvementId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateEndDate(Guid CaseInvolvementId, DateTime? EndDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseInvolvementId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", EndDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteCaseInvolvement(Guid CaseInvolvementId)
        {
            this.DeleteRecord(TableName, CaseInvolvementId);
        }
    }
}
