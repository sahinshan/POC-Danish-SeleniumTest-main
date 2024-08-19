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
    public class TeamSecurityProfile : BaseClass
    {

        public string TableName = "TeamSecurityProfile";
        public string PrimaryKeyName = "TeamSecurityProfileId";
        

        public TeamSecurityProfile(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreateTeamSecurityProfile(Guid TeamId, Guid SecurityProfileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "TeamId", TeamId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SecurityProfileId", SecurityProfileId);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetTeamSecurityProfileByTeamID(Guid TeamID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "TeamID", ConditionOperatorType.Equal, TeamID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetTeamSecurityProfileByID(Guid TeamSecurityProfileId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, TeamSecurityProfileId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteTeamSecurityProfile(Guid TeamSecurityProfileId)
        {
            this.DeleteRecord(TableName, TeamSecurityProfileId);
        }
    }
}
