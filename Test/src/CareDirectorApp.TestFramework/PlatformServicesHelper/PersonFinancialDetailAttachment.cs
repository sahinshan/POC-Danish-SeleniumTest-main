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
    public class PersonFinancialDetailAttachment : BaseClass
    {

        public string TableName = "PersonFinancialDetailAttachment";
        public string PrimaryKeyName = "PersonFinancialDetailAttachmentId";
        

        public PersonFinancialDetailAttachment(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreatePersonFinancialDetailAttachment(string title, Guid ownerid, DateTime date, Guid documenttypeid, Guid documentsubtypeid, Guid personfinancialdetailid, Guid personid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "date", date);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "documenttypeid", documenttypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "documentsubtypeid", documentsubtypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personfinancialdetailid", personfinancialdetailid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonFinancialDetailId(Guid personfinancialdetailid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "personfinancialdetailid", ConditionOperatorType.Equal, personfinancialdetailid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid PersonFinancialDetailAttachmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonFinancialDetailAttachmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonFinancialDetailAttachment(Guid PersonFinancialDetailAttachmentId)
        {
            this.DeleteRecord(TableName, PersonFinancialDetailAttachmentId);
        }
    }
}
