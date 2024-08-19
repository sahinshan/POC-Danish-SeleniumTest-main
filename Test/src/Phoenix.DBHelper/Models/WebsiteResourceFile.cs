using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteResourceFile : BaseClass
    {

        private string tableName = "WebsiteResourceFile";
        private string primaryKeyName = "WebsiteResourceFileId";

        public WebsiteResourceFile()
        {
            AuthenticateUser();
        }

        public WebsiteResourceFile(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public List<Guid> GetByWebSiteIDAndName(Guid WebsiteId, string Name)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteId", ConditionOperatorType.Equal, WebsiteId);
            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid WebsiteResourceFileId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteResourceFileId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteResourceFile(Guid WebsiteResourceFileID)
        {
            this.DeleteRecord(tableName, WebsiteResourceFileID);
        }



    }
}
