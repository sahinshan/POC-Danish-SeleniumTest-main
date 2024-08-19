using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteLocalizedStringValue : BaseClass
    {

        private string tableName = "WebsiteLocalizedStringValue";
        private string primaryKeyName = "WebsiteLocalizedStringValueId";

        public WebsiteLocalizedStringValue()
        {
            AuthenticateUser();
        }

        public WebsiteLocalizedStringValue(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }




        public List<Guid> GetByWebsiteLocalizedStringID(Guid LocalizedStringId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "LocalizedStringId", ConditionOperatorType.Equal, LocalizedStringId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByWebsiteLocalizedStringIDAndLanguage(Guid localizedstringid, Guid languageid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "localizedstringid", ConditionOperatorType.Equal, localizedstringid);
            this.BaseClassAddTableCondition(query, "languageid", ConditionOperatorType.Contains, languageid);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid WebsiteLocalizedStringValueId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteLocalizedStringValueId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteLocalizedStringValue(Guid WebsiteLocalizedStringValueID)
        {
            this.DeleteRecord(tableName, WebsiteLocalizedStringValueID);
        }



    }
}
