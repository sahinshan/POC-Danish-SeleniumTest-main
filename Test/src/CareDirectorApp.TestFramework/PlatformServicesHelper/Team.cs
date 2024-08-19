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
    public class Team : BaseClass
    {

        public Team(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetTeamIdByName(string TeamName)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Team", true, "TeamId");
            query.PrimaryKeyName = "TeamId";

            query.Filter.AddCondition("Team", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, TeamName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["TeamId"]).ToList();
            else
                return new List<Guid>();
        }
    }
}
