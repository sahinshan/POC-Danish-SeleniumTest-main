using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SystemUserEmploymentContractTeam : BaseClass
    {

        public string TableName = "SystemUserEmploymentContractTeam";
        public string PrimaryKeyName = "SystemUserEmploymentContractTeamId";


        public SystemUserEmploymentContractTeam()
        {
            AuthenticateUser();
        }

        public SystemUserEmploymentContractTeam(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> getBySystemUserEmploymentContractId(Guid SystemUserEmploymentContractId)

        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserEmploymentContractId", ConditionOperatorType.Equal, SystemUserEmploymentContractId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetById(Guid SystemUserEmploymentContractTeamId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemUserEmploymentContractTeamId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateSystemUserEmploymentContractTeam(Guid SystemUserEmploymentContractId, Guid TeamId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "SystemUserEmploymentContractId", SystemUserEmploymentContractId);
            AddFieldToBusinessDataObject(dataObject, "TeamId", TeamId);

            return this.CreateRecord(dataObject);
        }

        public void DeleteSystemUserEmploymentContractTeam(Guid SystemUserEmploymentContractTeamId)
        {
            this.DeleteRecord(TableName, SystemUserEmploymentContractTeamId);
        }

    }
}
