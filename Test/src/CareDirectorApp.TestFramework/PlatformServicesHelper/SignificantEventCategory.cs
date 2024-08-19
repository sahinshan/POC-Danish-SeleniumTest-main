using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareDirectorApp.TestFramework;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public class SignificantEventCategory : BaseClass
    {

        public string TableName = "SignificantEventCategory";
        public string PrimaryKeyName = "SignificantEventCategoryId";

        public SignificantEventCategory(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }


        
        public Guid CreateSignificantEventCategory(string Name, DateTime startdate, Guid ownerid, int? Code, string govcode, DateTime? enddate, bool useInChronology = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "govcode", govcode);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(dataObject, "ReferenceDataOwner", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }


        public Guid CreateSignificantEventCategory(string Name, DateTime StartDate, Guid OwnerId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "UseInChronology", true);

            return this.CreateRecord(buisinessDataObject);

        }


        public List<Guid> GetByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        
        public List<Guid> GetSignificantEvenetCategoryById(Guid SignificantEventCategoryId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "SignificantEventCategoryId", ConditionOperatorType.Equal, SignificantEventCategoryId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteSignificantEventCategory(Guid SignificantEventCategoryId)
        {
            this.DeleteRecord(TableName, SignificantEventCategoryId);
        }

        public Dictionary<string, object> GetSignificantEventCategoryByID(Guid SignificantEventCategoryId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SignificantEventCategoryId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }     

    }
}
