using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class UserSecurityProfile : BaseClass
    {

        public string TableName = "UserSecurityProfile";
        public string PrimaryKeyName = "UserSecurityProfileId";


        public UserSecurityProfile()
        {
            AuthenticateUser();
        }

        public UserSecurityProfile(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateUserSecurityProfile(Guid SystemUserId, Guid SecurityProfileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "SystemUserId", SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SecurityProfileId", SecurityProfileId);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }


        public List<Guid> CreateMultipleUserSecurityProfile(Guid SystemUserId, List<Guid> SecurityProfilesIds)
        {
            List<BusinessData> records = new List<BusinessData>();

            foreach (var SecurityProfileId in SecurityProfilesIds)
            {
                var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
                AddFieldToBusinessDataObject(buisinessDataObject, "SystemUserId", SystemUserId);
                AddFieldToBusinessDataObject(buisinessDataObject, "SecurityProfileId", SecurityProfileId);
                AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

                records.Add(buisinessDataObject);
            }

            return this.CreateMultipleRecords(records);
        }

        public List<Guid> GetUserSecurityProfileByUserID(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByUserIDAndProfileId(Guid SystemUserId, Guid SecurityProfileId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", ConditionOperatorType.Equal, SystemUserId);
            this.BaseClassAddTableCondition(query, "SecurityProfileId", ConditionOperatorType.Equal, SecurityProfileId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetUserSecurityProfileByID(Guid UserSecurityProfileId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, UserSecurityProfileId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateSecurityProfileId(Guid SystemUserId, Guid SecurityProfileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SecurityProfileId", SecurityProfileId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteUserSecurityProfile(Guid UserSecurityProfileId)
        {
            this.DeleteRecord(TableName, UserSecurityProfileId);
        }
    }
}
