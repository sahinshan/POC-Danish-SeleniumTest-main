using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SystemUserSuspensionContract : BaseClass
    {

        public string TableName = "SystemUserSuspensionContract";
        public string PrimaryKeyName = "SystemUserSuspensionContractId";


        public SystemUserSuspensionContract()
        {
            AuthenticateUser();
        }

        public SystemUserSuspensionContract(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetSystemUserSuspensionContractByName(string name)
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

        public Guid CreateSystemUserSuspensionContract(Guid? systemusersuspensionid, Guid? systemuseremploymentcontractid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "systemusersuspensionid", systemusersuspensionid);
            AddFieldToBusinessDataObject(dataObject, "systemuseremploymentcontractid", systemuseremploymentcontractid);

            return this.CreateRecord(dataObject);
        }

        public void DeleteSystemUserSuspensionContract(Guid SystemUserSuspensionContractId)
        {
            this.DeleteRecord(TableName, SystemUserSuspensionContractId);
        }

        public List<Guid> GetSystemUserSuspensionContractBySystemUserId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
    }
}
