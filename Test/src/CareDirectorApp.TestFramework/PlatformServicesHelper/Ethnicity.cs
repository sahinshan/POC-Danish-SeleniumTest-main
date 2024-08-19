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
    public class Ethnicity : BaseClass
    {

        public Ethnicity(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }


        public List<Guid> GetEthnicityIdByName(string EthnicityName)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Ethnicity", true, "EthnicityId");
            query.PrimaryKeyName = "EthnicityId";

            query.Filter.AddCondition("Ethnicity", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, EthnicityName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["EthnicityId"]).ToList();
            else
                return new List<Guid>();
        }
    }
}
