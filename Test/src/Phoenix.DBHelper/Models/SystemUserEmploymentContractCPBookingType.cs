using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SystemUserEmploymentContractCPBookingType : BaseClass
    {

        public string TableName = "SystemUserEmploymentContractCPBookingType";
        public string PrimaryKeyName = "SystemUserEmploymentContractCPBookingTypeId";


        public SystemUserEmploymentContractCPBookingType()
        {
            AuthenticateUser();
        }

        public SystemUserEmploymentContractCPBookingType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public Dictionary<string, object> GetById(Guid SystemUserEmploymentContractCPBookingTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemUserEmploymentContractCPBookingTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateSystemUserEmploymentContractCPBookingType(Guid SystemUserEmploymentContractId, Guid CPBookingTypeId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "SystemUserEmploymentContractId", SystemUserEmploymentContractId);
            AddFieldToBusinessDataObject(dataObject, "CPBookingTypeId", CPBookingTypeId);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetBySystemUserEmploymentContractIdAndCPBookingTypeId(Guid SystemUserEmploymentContractId, Guid CPBookingTypeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserEmploymentContractId", ConditionOperatorType.Equal, SystemUserEmploymentContractId);
            this.BaseClassAddTableCondition(query, "CPBookingTypeId", ConditionOperatorType.Equal, CPBookingTypeId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteSystemUserEmploymentContractCPBookingType(Guid SystemUserEmploymentContractCPBookingTypeId)
        {
            this.DeleteRecord(TableName, SystemUserEmploymentContractCPBookingTypeId);
        }

    }
}
