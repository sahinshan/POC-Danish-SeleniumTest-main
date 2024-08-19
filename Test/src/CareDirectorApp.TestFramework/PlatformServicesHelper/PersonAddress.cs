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
    public class PersonAddress : BaseClass
    {

        public PersonAddress(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetPersonAddressIdForPerson(Guid PersonID)
        {
            var query = new CareDirector.Sdk.Query.DataQuery("PersonAddress", true, "PersonAddressId");
            query.PrimaryKeyName = "PersonAddressId";

            query.Filter.AddCondition("PersonAddress", "PersonId", ConditionOperatorType.Equal, PersonID);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["PersonAddressId"]).ToList();
            else
                return new List<Guid>();
        }

        public void DeletePersonAddress(Guid PersonAddressID)
        {
            var response = DataProvider.Delete("PersonAddress", PersonAddressID);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

        }
    }
}
