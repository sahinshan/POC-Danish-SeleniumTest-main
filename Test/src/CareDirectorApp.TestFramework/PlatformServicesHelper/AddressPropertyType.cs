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
    public class AddressPropertyType: BaseClass
    {
        public AddressPropertyType(string AccessToken) 
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetAddressPropertyTypeIdByName(string AddressPropertyTypeName)
        {
            var query = new CareDirector.Sdk.Query.DataQuery("AddressPropertyType", true, "AddressPropertyTypeId");
            query.PrimaryKeyName = "AddressPropertyTypeId";

            query.Filter.AddCondition("AddressPropertyType", "Name", ConditionOperatorType.Equal, AddressPropertyTypeName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = DataProvider.ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["AddressPropertyTypeId"]).ToList();
            else
                return new List<Guid>();
        }
    }
}
