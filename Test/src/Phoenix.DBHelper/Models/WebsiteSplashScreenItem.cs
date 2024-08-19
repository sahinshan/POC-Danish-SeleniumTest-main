using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteSplashScreenItem : BaseClass
    {

        private string tableName = "WebsiteSplashScreenItem";
        private string primaryKeyName = "WebsiteSplashScreenItemId";

        public WebsiteSplashScreenItem()
        {
            AuthenticateUser();
        }

        public WebsiteSplashScreenItem(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public List<Guid> GetByWebsiteSplashScreenId(Guid WebsiteSplashScreenId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteSplashScreenId", ConditionOperatorType.Equal, WebsiteSplashScreenId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByWebsiteSplashScreenIdAndPageId(Guid WebsiteSplashScreenId, Guid websitepageid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteSplashScreenId", ConditionOperatorType.Equal, WebsiteSplashScreenId);
            this.BaseClassAddTableCondition(query, "websitepageid", ConditionOperatorType.Equal, websitepageid);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid WebsiteSplashScreenItemId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteSplashScreenItemId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteSplashScreenItem(Guid WebsiteSplashScreenItemID)
        {
            this.DeleteRecord(tableName, WebsiteSplashScreenItemID);
        }



    }
}
