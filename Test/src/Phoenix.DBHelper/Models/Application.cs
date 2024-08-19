using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class Application : BaseClass
    {
        public string TableName { get { return "Application"; } }
        public string PrimaryKeyName { get { return "Applicationid"; } }


        public Application()
        {
            AuthenticateUser();
        }

        public Application(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }



        public List<Guid> GetByName(string DisplayName)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "DisplayName", ConditionOperatorType.Equal, DisplayName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetApplicationByID(Guid ApplicationId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("Application", false, "ApplicationId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "ApplicationId", ConditionOperatorType.Equal, ApplicationId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
