using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SystemUserSuspensionReason : BaseClass
    {
        public string TableName = "SystemUserSuspensionReason";
        public string PrimaryKeyName = "SystemUserSuspensionReasonId";

        public SystemUserSuspensionReason()
        {
            AuthenticateUser();
        }

        public SystemUserSuspensionReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateSystemUserSuspensionReason(Guid ownerId, string name, DateTime startDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", ownerId);
            AddFieldToBusinessDataObject(dataObject, "Name", name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", startDate);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetSystemUserSuspensionReasonByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetSystemUserSuspensionContractByID(Guid SystemUserSuspensionId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemUserSuspensionId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


    }
}
