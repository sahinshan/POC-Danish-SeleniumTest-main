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
    public class PersonLanguage : BaseClass
    {

        public PersonLanguage(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetPersonLanguageIdForPerson(Guid PersonID)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("PersonLanguage", true, "PersonLanguageId");
            query.PrimaryKeyName = "PersonLanguageId";

            query.Filter.AddCondition("PersonLanguage", "PersonId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonID);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["PersonLanguageId"]).ToList();
            else
                return new List<Guid>();
        }

        public void DeletePersonLanguage(Guid PersonLanguageID)
        {
            var response = DataProvider.Delete("PersonLanguage", PersonLanguageID);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

        }
    }
}
