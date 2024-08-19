using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public class UserSecurityProfile : BaseClass
    {

        public string TableName = "UserSecurityProfile";
        public string PrimaryKeyName = "UserSecurityProfileId";
        

        public UserSecurityProfile(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreateUserSecurityProfile(Guid SystemUserId, Guid SecurityProfileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "SystemUserId", SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SecurityProfileId", SecurityProfileId);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetUserSecurityProfileByUserID(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "SystemUserId", ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetUserSecurityProfileByID(Guid UserSecurityProfileId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, UserSecurityProfileId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteUserSecurityProfile(Guid UserSecurityProfileId)
        {
            this.DeleteRecord(TableName, UserSecurityProfileId);
        }
    }
}
