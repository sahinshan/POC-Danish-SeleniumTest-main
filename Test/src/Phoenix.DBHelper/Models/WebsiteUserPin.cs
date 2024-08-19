using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteUserPin : BaseClass
    {

        private string tableName = "WebsiteUserPin";
        private string primaryKeyName = "WebsiteUserPinId";

        public WebsiteUserPin()
        {
            AuthenticateUser();
        }

        public WebsiteUserPin(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWebsiteUserPin(Guid WebsiteUserId, string pin, DateTime? expireon, DateTime? senton, string error)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "WebsiteUserId", WebsiteUserId);
            AddFieldToBusinessDataObject(dataObject, "pin", pin);
            AddFieldToBusinessDataObject(dataObject, "expireon", expireon);
            AddFieldToBusinessDataObject(dataObject, "senton", senton);
            AddFieldToBusinessDataObject(dataObject, "error", error);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "twofactorauthenticationtypeid", 1); //default to email
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public void UpdateWebsiteUserPin(Guid WebsiteUserPinId, DateTime expireon)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserPinId);

            AddFieldToBusinessDataObject(buisinessDataObject, "expireon", expireon);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByWebSiteUserID(Guid WebsiteUserId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteUserId", ConditionOperatorType.Equal, WebsiteUserId);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, this.tableName));

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid WebsiteUserPinId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteUserPinId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteUserPin(Guid WebsiteUserPinID)
        {
            this.DeleteRecord(tableName, WebsiteUserPinID);
        }



    }
}
