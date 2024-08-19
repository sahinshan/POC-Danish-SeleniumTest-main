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
    public class MaritalStatus : BaseClass
    {

        public MaritalStatus(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetMaritalStatusIdByName(string MaritalStatusName)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("MaritalStatus", true, "MaritalStatusId");
            query.PrimaryKeyName = "MaritalStatusId";

            query.Filter.AddCondition("MaritalStatus", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, MaritalStatusName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["MaritalStatusId"]).ToList();
            else
                return new List<Guid>();
        }
    }
}
