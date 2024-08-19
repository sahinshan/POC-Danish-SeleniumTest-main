using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteMessage : BaseClass
    {

        private string tableName = "WebsiteMessage";
        private string primaryKeyName = "WebsiteMessageId";

        public WebsiteMessage()
        {
            AuthenticateUser();
        }

        public WebsiteMessage(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWebsiteMessage(Guid OwnerId,
            Guid FromId, string fromidtablename, string fromidname,
            Guid ToId, string toidtablename, string toidname,
            Guid RegardingId, string regardingidtablename, string regardingidname,
            string Message, bool Read)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "FromId", FromId);
            AddFieldToBusinessDataObject(dataObject, "fromidtablename", fromidtablename);
            AddFieldToBusinessDataObject(dataObject, "fromidname", fromidname);

            AddFieldToBusinessDataObject(dataObject, "ToId", ToId);
            AddFieldToBusinessDataObject(dataObject, "toidtablename", toidtablename);
            AddFieldToBusinessDataObject(dataObject, "toidname", toidname);

            AddFieldToBusinessDataObject(dataObject, "RegardingId", RegardingId);
            AddFieldToBusinessDataObject(dataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(dataObject, "regardingidname", regardingidname);

            AddFieldToBusinessDataObject(dataObject, "Message", Message);

            AddFieldToBusinessDataObject(dataObject, "Read", Read);

            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }


        public List<Guid> GetByToIdAndRegardingId(Guid ToId, Guid RegardingId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "ToId", ConditionOperatorType.Equal, ToId);
            this.BaseClassAddTableCondition(query, "RegardingId", ConditionOperatorType.Equal, RegardingId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByFromIdAndRegardingId(Guid FromId, Guid RegardingId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "FromId", ConditionOperatorType.Equal, FromId);
            this.BaseClassAddTableCondition(query, "RegardingId", ConditionOperatorType.Equal, RegardingId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid WebsiteMessageId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteMessageId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteMessage(Guid WebsiteMessageID)
        {
            this.DeleteRecord(tableName, WebsiteMessageID);
        }



    }
}
