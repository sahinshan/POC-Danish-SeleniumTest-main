using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteContact : BaseClass
    {

        private string tableName = "WebsiteContact";
        private string primaryKeyName = "WebsiteContactId";

        public WebsiteContact()
        {
            AuthenticateUser();
        }

        public WebsiteContact(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWebsiteContact(Guid ownerid, string Name, string Subject, string Message, Guid WebsitePointOfContactId, string Email, Guid WebsiteId)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(dataObject, "Message", Message);
            AddFieldToBusinessDataObject(dataObject, "WebsitePointOfContactId", WebsitePointOfContactId);
            AddFieldToBusinessDataObject(dataObject, "Email", Email);
            AddFieldToBusinessDataObject(dataObject, "WebsiteId", WebsiteId);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public void UpdateWebsiteContact(Guid WebsiteContactId, string Name, string Subject, string Message, Guid WebsitePointOfContactId, string Email)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteContactId);

            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "Subject", Subject);
            AddFieldToBusinessDataObject(buisinessDataObject, "Message", Message);
            AddFieldToBusinessDataObject(buisinessDataObject, "WebsitePointOfContactId", WebsitePointOfContactId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Email", Email);

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

        public List<Guid> GetByWebSiteIDAndSubject(Guid WebsiteId, string Subject)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteId", ConditionOperatorType.Equal, WebsiteId);
            this.BaseClassAddTableCondition(query, "Subject", ConditionOperatorType.Contains, Subject);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid WebsiteContactId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteContactId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteContact(Guid WebsiteContactID)
        {
            this.DeleteRecord(tableName, WebsiteContactID);
        }



    }
}
