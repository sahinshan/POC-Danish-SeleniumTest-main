using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class TeamSecurityProfile : BaseClass
    {

        public string TableName = "TeamSecurityProfile";
        public string PrimaryKeyName = "TeamSecurityProfileId";



        public TeamSecurityProfile()
        {
            AuthenticateUser();
        }

        public TeamSecurityProfile(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
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

            this.BaseClassAddTableCondition(query, "TeamID", ConditionOperatorType.Equal, TeamID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetTeamSecurityProfileByID(Guid TeamSecurityProfileId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, TeamSecurityProfileId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteTeamSecurityProfile(Guid TeamSecurityProfileId)
        {
            this.DeleteRecord(TableName, TeamSecurityProfileId);
        }
    }
}
