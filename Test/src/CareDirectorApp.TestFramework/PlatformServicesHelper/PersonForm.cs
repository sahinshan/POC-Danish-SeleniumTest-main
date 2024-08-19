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
    public class PersonForm : BaseClass
    {

        public string TableName = "PersonForm";
        public string PrimaryKeyName = "PersonFormId";
        

        public PersonForm(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreatePersonForm(Guid ownerid, Guid personid, string PersonName, Guid responsibleuserid, Guid? caseid, 
            Guid documentid, string documentidName, int assessmentstatusid, DateTime startdate, DateTime? duedate, DateTime? reviewdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid_cwname", PersonName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", responsibleuserid);

            if (caseid.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "caseid", caseid.Value);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "documentid", documentid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "documentid_cwname", documentidName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "assessmentstatusid", assessmentstatusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);

            if (duedate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "duedate", duedate.Value);
            
            if(reviewdate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "reviewdate", reviewdate.Value);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "sdeexecuted", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "answersinitialized", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonFormByCaseID(Guid CaseID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetPersonFormByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonFormByID(Guid PersonFormId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonFormId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonForm(Guid PersonFormId)
        {
            this.DeleteRecord(TableName, PersonFormId);
        }
    }
}
