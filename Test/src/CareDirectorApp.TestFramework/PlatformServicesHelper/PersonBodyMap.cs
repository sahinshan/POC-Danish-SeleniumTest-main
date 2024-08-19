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
    public class PersonBodyMap : BaseClass
    {

        public string TableName = "PersonBodyMap";
        public string PrimaryKeyName = "PersonBodyMapId";


        public PersonBodyMap(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreatePersonBodyMap(Guid OwnerId, Guid ResponsibleUserId, Guid PersonId, DateTime DateOfEvent, DateTime? ReviewDate, int? ViewTypeId, bool? IsReviewRequiredId, int PersonAge)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", ResponsibleUserId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DateOfEvent", DateOfEvent);
            
            if(ReviewDate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "ReviewDate", ReviewDate.Value);
            
            if(ViewTypeId.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "ViewTypeId", ViewTypeId.Value);

            if(IsReviewRequiredId.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "IsReviewRequiredId", IsReviewRequiredId.Value);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonAge", PersonAge);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonBodyMapByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonBodyMapByID(Guid PersonBodyMapId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonBodyMapId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonBodyMap(Guid PersonBodyMapId)
        {
            this.DeleteRecord(TableName, PersonBodyMapId);
        }
    }
}
