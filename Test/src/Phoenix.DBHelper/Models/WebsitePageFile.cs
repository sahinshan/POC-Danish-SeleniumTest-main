using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsitePageFile : BaseClass
    {

        private string tableName = "WebsitePageFile";
        private string primaryKeyName = "WebsitePageFileId";

        public WebsitePageFile()
        {
            AuthenticateUser();
        }

        public WebsitePageFile(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public List<Guid> GetByWebsitePageId(Guid WebsitePageId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsitePageId", ConditionOperatorType.Equal, WebsitePageId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByWebsitePageId(Guid WebsitePageId, Guid fileid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsitePageId", ConditionOperatorType.Equal, WebsitePageId);
            this.BaseClassAddTableCondition(query, "fileid", ConditionOperatorType.Equal, fileid);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid WebsitePageFileId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsitePageFileId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsitePageFile(Guid WebsitePageFileID)
        {
            this.DeleteRecord(tableName, WebsitePageFileID);
        }



    }
}
