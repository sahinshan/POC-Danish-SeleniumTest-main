using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class AddressPropertyType : BaseClass
    {
        public AddressPropertyType()
        {
            AuthenticateUser();
        }

        public AddressPropertyType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetAddressPropertyTypeIdByName(string AddressPropertyTypeName)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("AddressPropertyType", true, "AddressPropertyTypeId");
            query.PrimaryKeyName = "AddressPropertyTypeId";

            query.Filter.AddCondition("AddressPropertyType", "Name", ConditionOperatorType.Equal, AddressPropertyTypeName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => (Guid.Parse(c.FieldCollection["AddressPropertyTypeId"].ToString()))).ToList();
            else
                return new List<Guid>();
        }

        public List<Guid> GetAddressPropertyTypeByName(string AddressPropertyTypeName)
        {
            DataQuery query = this.GetDataQueryObject("AddressPropertyType", false, "AddressPropertyTypeId");

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, AddressPropertyTypeName);

            this.AddReturnField(query, "AddressPropertyType", "AddressPropertyTypeId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "AddressPropertyTypeId");
        }
    }
}
