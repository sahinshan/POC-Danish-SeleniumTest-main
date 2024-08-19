using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SystemSetting : BaseClass
    {
        string TableName = "SystemSetting";
        string PrimaryKeyName = "SystemSettingId";

        public SystemSetting()
        {
            AuthenticateUser();
        }

        public SystemSetting(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateSystemSetting(string Name, string SettingValue, string Description, bool IsEncrypted, string EncryptedValue)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "SettingValue", SettingValue);
            AddFieldToBusinessDataObject(buisinessDataObject, "Description", Description);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsEncrypted", IsEncrypted);
            AddFieldToBusinessDataObject(buisinessDataObject, "EncryptedValue", EncryptedValue);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetSystemSettingIdByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateSystemSettingValue(Guid SystemSettingID, string Value)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("SystemSetting", "SystemSettingId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "SystemSettingID", SystemSettingID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "settingvalue", Value);

            this.UpdateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetByID(Guid SystemSettingId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            AddReturnFields(query, TableName, FieldsToReturn);

            BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemSettingId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
