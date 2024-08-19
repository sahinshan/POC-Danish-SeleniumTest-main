using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteRecordType : BaseClass
    {

        private string tableName = "WebsiteRecordType";
        private string primaryKeyName = "WebsiteRecordTypeId";

        public WebsiteRecordType()
        {
            AuthenticateUser();
        }

        public WebsiteRecordType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public List<Guid> GetByWebSiteIdAndRecordTypeId(Guid WebsiteId, Guid recordtypeid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteId", ConditionOperatorType.Equal, WebsiteId);
            this.BaseClassAddTableCondition(query, "recordtypeid", ConditionOperatorType.Equal, recordtypeid);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid WebsiteRecordTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteRecordTypeId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteRecordType(Guid WebsiteRecordTypeID)
        {
            this.DeleteRecord(tableName, WebsiteRecordTypeID);
        }



    }
}
