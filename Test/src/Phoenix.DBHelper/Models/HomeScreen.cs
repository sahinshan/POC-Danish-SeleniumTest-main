using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class HomeScreen : BaseClass
    {
        public string TableName { get { return "HomeScreen"; } }
        public string PrimaryKeyName { get { return "HomeScreenid"; } }


        public HomeScreen()
        {
            AuthenticateUser();
        }

        public HomeScreen(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetHomeScreenByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetHomeScreenByID(Guid HomeScreenId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("HomeScreen", false, "HomeScreenId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "HomeScreenId", ConditionOperatorType.Equal, HomeScreenId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
