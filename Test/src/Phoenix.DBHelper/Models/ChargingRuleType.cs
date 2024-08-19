using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ChargingRuleType : BaseClass
    {
        public string TableName = "ChargingRuleType";
        public string PrimaryKeyName = "ChargingRuleTypeId";

        public ChargingRuleType()
        {
            AuthenticateUser();
        }

        public ChargingRuleType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateChargingRuleType(string name, Guid ownerid, DateTime StartDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            return this.CreateRecord(buisinessDataObject);

        }

        public List<Guid> GetChargingRuleTypeByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetChargingRuleTypeByID(Guid ChargingRuleTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ChargingRuleTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteChargingRuleType(Guid ChargingRuleTypeId)
        {
            this.DeleteRecord(TableName, ChargingRuleTypeId);
        }

    }
}
