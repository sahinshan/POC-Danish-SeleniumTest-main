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
    public class Language : BaseClass
    {

        public Language(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetLanguageIdByName(string LanguageName)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Language", true, "LanguageId");
            query.PrimaryKeyName = "LanguageId";

            query.Filter.AddCondition("Language", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, LanguageName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["LanguageId"]).ToList();
            else
                return new List<Guid>();
        }
    }
}
