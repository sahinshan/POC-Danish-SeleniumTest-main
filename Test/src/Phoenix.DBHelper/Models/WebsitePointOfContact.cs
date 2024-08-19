using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsitePointOfContact : BaseClass
    {

        private string tableName = "WebsitePointOfContact";
        private string primaryKeyName = "WebsitePointOfContactId";

        public WebsitePointOfContact()
        {
            AuthenticateUser();
        }

        public WebsitePointOfContact(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWebsitePointOfContact(Guid OwnerId, string Name, Guid WebsiteId, int StatusId, string Address, string Email, string Phone)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "WebsiteId", WebsiteId);
            AddFieldToBusinessDataObject(dataObject, "StatusId", StatusId);
            AddFieldToBusinessDataObject(dataObject, "Address", Address);
            AddFieldToBusinessDataObject(dataObject, "Email", Email);
            AddFieldToBusinessDataObject(dataObject, "Phone", Phone);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }


        public void UpdateWebsitePointOfContact(Guid WebsitePointOfContactID, string Name, int StatusId, string Address, string Email, string Phone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsitePointOfContactID);

            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", StatusId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Address", Address);
            AddFieldToBusinessDataObject(buisinessDataObject, "Email", Email);
            AddFieldToBusinessDataObject(buisinessDataObject, "Phone", Phone);

            this.UpdateRecord(buisinessDataObject);
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
            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Contains, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid WebsitePointOfContactId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsitePointOfContactId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsitePointOfContact(Guid WebsitePointOfContactID)
        {
            this.DeleteRecord(tableName, WebsitePointOfContactID);
        }



    }
}
