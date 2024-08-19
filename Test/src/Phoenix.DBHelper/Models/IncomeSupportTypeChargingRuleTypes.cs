using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class IncomeSupportTypeChargingRuleTypes : BaseClass
    {
        public string TableName = "IncomeSupportTypeChargingRuleTypes";
        public string PrimaryKeyName = "IncomeSupportTypeChargingRuleTypesId";

        public IncomeSupportTypeChargingRuleTypes()
        {
            AuthenticateUser();
        }

        public IncomeSupportTypeChargingRuleTypes(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateIncomeSupportTypeChargingRuleTypes(Guid IncomeSupportTypeId, Guid ChargingRuleTypeId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "IncomeSupportTypeId", IncomeSupportTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ChargingRuleTypeId", ChargingRuleTypeId);

            return this.CreateRecord(buisinessDataObject);

        }

        public List<Guid> GetByIncomeSupportTypeId(Guid IncomeSupportTypeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "IncomeSupportTypeId", ConditionOperatorType.Equal, IncomeSupportTypeId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByChargingRuleTypeId(Guid ChargingRuleTypeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ChargingRuleTypeId", ConditionOperatorType.Equal, ChargingRuleTypeId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByIncomeSupportTypeAndChargingRule(Guid IncomeSupportTypeId, Guid ChargingRuleTypeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "IncomeSupportTypeId", ConditionOperatorType.Equal, IncomeSupportTypeId);
            this.BaseClassAddTableCondition(query, "ChargingRuleTypeId", ConditionOperatorType.Equal, ChargingRuleTypeId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public Dictionary<string, object> GetByID(Guid IncomeSupportTypeChargingRuleTypesId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, IncomeSupportTypeChargingRuleTypesId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteIncomeSupportTypeChargingRuleTypes(Guid IncomeSupportTypeChargingRuleTypesId)
        {
            this.DeleteRecord(TableName, IncomeSupportTypeChargingRuleTypesId);
        }

    }
}
