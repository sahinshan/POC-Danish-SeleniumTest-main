using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CurrentRanking : BaseClass
    {
        public string TableName = "CurrentRanking";
        public string PrimaryKeyName = "CurrentRankingId";

        public CurrentRanking()
        {
            AuthenticateUser();
        }

        public CurrentRanking(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetCurrentRankingByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCurrentRankingByID(Guid CurrentRankingId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CurrentRankingId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCurrentRanking(Guid CurrentRankingId)
        {
            this.DeleteRecord(TableName, CurrentRankingId);
        }

    }
}
