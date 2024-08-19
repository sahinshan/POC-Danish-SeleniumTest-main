using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteSplashScreen : BaseClass
    {

        private string tableName = "WebsiteSplashScreen";
        private string primaryKeyName = "WebsiteSplashScreenId";

        public WebsiteSplashScreen()
        {
            AuthenticateUser();
        }

        public WebsiteSplashScreen(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }




        public List<Guid> GetByWebSiteID(Guid WebsiteId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteId", ConditionOperatorType.Equal, WebsiteId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByWebSiteIDAndName(Guid WebsiteId, string uniquename)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteId", ConditionOperatorType.Equal, WebsiteId);
            this.BaseClassAddTableCondition(query, "uniquename", ConditionOperatorType.Equal, uniquename);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid WebsiteSplashScreenId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteSplashScreenId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteSplashScreen(Guid WebsiteSplashScreenID)
        {
            this.DeleteRecord(tableName, WebsiteSplashScreenID);
        }



    }
}
