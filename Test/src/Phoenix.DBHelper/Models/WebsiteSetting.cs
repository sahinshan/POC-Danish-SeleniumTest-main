using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteSetting : BaseClass
    {

        private string tableName = "WebsiteSetting";
        private string primaryKeyName = "WebsiteSettingId";

        public WebsiteSetting()
        {
            AuthenticateUser();
        }

        public WebsiteSetting(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWebsiteSetting(string Name, string Description, Guid WebsiteId, bool IsEncrypted, string SettingValue, string EncryptedValue)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Description", Description);
            AddFieldToBusinessDataObject(dataObject, "WebsiteId", WebsiteId);
            AddFieldToBusinessDataObject(dataObject, "EncryptedValue", EncryptedValue);
            AddFieldToBusinessDataObject(dataObject, "SettingValue", SettingValue);
            AddFieldToBusinessDataObject(dataObject, "IsEncrypted", IsEncrypted);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }


        public void UpdateWebsiteSetting(Guid WebsiteSettingID, string Name, string Description, bool IsEncrypted, string SettingValue, string EncryptedValue)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteSettingID);

            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "Description", Description);
            AddFieldToBusinessDataObject(buisinessDataObject, "EncryptedValue", EncryptedValue);
            AddFieldToBusinessDataObject(buisinessDataObject, "SettingValue", SettingValue);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsEncrypted", IsEncrypted);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", 0);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateWebsiteSettingValue(Guid WebsiteSettingID, string SettingValue)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteSettingID);
            AddFieldToBusinessDataObject(buisinessDataObject, "SettingValue", SettingValue);

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


        public Dictionary<string, object> GetByID(Guid WebsiteSettingId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteSettingId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteSetting(Guid WebsiteSettingID)
        {
            this.DeleteRecord(tableName, WebsiteSettingID);
        }



    }
}
