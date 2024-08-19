using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteUserPasswordHistory : BaseClass
    {
        private string tableName = "WebsiteUserPasswordHistory";
        private string primaryKeyName = "WebsiteUserPasswordHistoryId";

        public WebsiteUserPasswordHistory()
        {
            AuthenticateUser();
        }

        public WebsiteUserPasswordHistory(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWebsiteUserPasswordHistory(Guid WebsiteUserId, string Password)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "WebsiteUserId", WebsiteUserId);
            AddFieldToBusinessDataObject(dataObject, "Password", Password);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }


        public List<Guid> GetByWebSiteUserID(Guid WebsiteUserId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteUserId", ConditionOperatorType.Equal, WebsiteUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid WebsiteUserPasswordHistoryId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteUserPasswordHistoryId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteUserPasswordHistory(Guid WebsiteUserPasswordHistoryID)
        {
            this.DeleteRecord(tableName, WebsiteUserPasswordHistoryID);
        }

    }
}
