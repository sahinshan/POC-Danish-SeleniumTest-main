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
    public class PersonBodyMapInjuryDescription : BaseClass
    {

        public string TableName = "PersonBodyMapInjuryDescription";
        public string PrimaryKeyName = "PersonBodyMapInjuryDescriptionId";


        public PersonBodyMapInjuryDescription(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreatePersonBodyMapInjuryDescription(Guid OwnerId, Guid PersonBodyMapId, Guid PersonId, Guid BodyMapSetupId,string Name, string Description)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonBodyMapId", PersonBodyMapId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "BodyMapSetupId", BodyMapSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Description", Description);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(Guid PersonBodyMapId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonBodyMapId", ConditionOperatorType.Equal, PersonBodyMapId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonBodyMapInjuryDescriptionByID(Guid PersonBodyMapInjuryDescriptionId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonBodyMapInjuryDescriptionId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonBodyMapInjuryDescription(Guid PersonBodyMapInjuryDescriptionId)
        {
            this.DeleteRecord(TableName, PersonBodyMapInjuryDescriptionId);
        }
    }
}
