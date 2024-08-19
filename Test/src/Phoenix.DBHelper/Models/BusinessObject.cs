using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BusinessObject : BaseClass
    {
        public string TableName { get { return "BusinessObject"; } }
        public string PrimaryKeyName { get { return "BusinessObjectid"; } }


        public BusinessObject()
        {
            AuthenticateUser();
        }

        public BusinessObject(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetBusinessObjectByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetBusinessObjectByID(Guid BusinessObjectId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("BusinessObject", false, "BusinessObjectId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "BusinessObjectId", ConditionOperatorType.Equal, BusinessObjectId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
