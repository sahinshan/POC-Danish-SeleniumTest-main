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
    public class CaseAttachment : BaseClass
    {

        public string TableName = "CaseAttachment";
        public string PrimaryKeyName = "CaseAttachmentId";
        

        public CaseAttachment(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreateCaseAttachment(string title, Guid ownerid, DateTime date, Guid documenttypeid, Guid documentsubtypeid, Guid caseid, Guid personid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "date", date);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "documenttypeid", documenttypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "documentsubtypeid", documentsubtypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caseid", caseid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetCaseAttachmentsByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCaseAttachmentByID(Guid CaseAttachmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseAttachmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCaseAttachment(Guid CaseAttachmentId)
        {
            this.DeleteRecord(TableName, CaseAttachmentId);
        }
    }
}
