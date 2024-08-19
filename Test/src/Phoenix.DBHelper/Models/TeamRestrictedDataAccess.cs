using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class TeamRestrictedDataAccess : BaseClass
    {

        public string TableName = "TeamRestrictedDataAccess";
        public string PrimaryKeyName = "TeamRestrictedDataAccessId";



        public TeamRestrictedDataAccess()
        {
            AuthenticateUser();
        }

        public TeamRestrictedDataAccess(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateTeamRestrictedDataAccess(Guid DataRestrictionID, Guid TeamID, DateTime StartDate, DateTime? EndDate, Guid OwnerId)
        {
            var record = GetBusinessDataBaseObject("teamrestricteddataaccess", "teamrestricteddataaccessid");

            AddFieldToBusinessDataObject(record, "DataRestrictionReferenceId", DataRestrictionID);
            AddFieldToBusinessDataObject(record, "TeamId", TeamID);
            AddFieldToBusinessDataObject(record, "StartDate", StartDate);
            if (EndDate.HasValue)
                AddFieldToBusinessDataObject(record, "EndDate", EndDate.Value);
            else
                AddFieldToBusinessDataObject(record, "EndDate", null);
            AddFieldToBusinessDataObject(record, "OwnerId", OwnerId);


            return this.CreateRecord(record);
        }

        public List<Guid> GetTeamRestrictedDataAccessByTeamID(Guid TeamID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "TeamID", ConditionOperatorType.Equal, TeamID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetTeamRestrictedDataAccessByTeamID(Guid TeamID, Guid DataRestrictionId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "TeamID", ConditionOperatorType.Equal, TeamID);
            this.BaseClassAddTableCondition(query, "DataRestrictionReferenceId", ConditionOperatorType.Equal, DataRestrictionId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetTeamRestrictedDataAccessByID(Guid TeamRestrictedDataAccessId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, TeamRestrictedDataAccessId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteTeamRestrictedDataAccess(Guid DataRestrictionID, Guid TeamID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                List<Phoenix.DBHelper.TeamRestrictedDataAccess> dataAccesses = (from ent in entity.TeamRestrictedDataAccesses
                                                                                where ent.DataRestrictionReferenceId == DataRestrictionID
                                                                                && ent.TeamId == TeamID
                                                                                select ent).ToList();
                entity.TeamRestrictedDataAccesses.RemoveRange(dataAccesses);
                entity.SaveChanges();
            }
        }



    }
}
