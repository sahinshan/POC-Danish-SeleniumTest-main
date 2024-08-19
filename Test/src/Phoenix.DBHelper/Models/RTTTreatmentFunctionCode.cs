using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RTTTreatmentFunctionCode : BaseClass
    {
        public string TableName = "RTTTreatmentFunctionCode";
        public string PrimaryKeyName = "RTTTreatmentFunctionCodeId";

        public RTTTreatmentFunctionCode()
        {
            AuthenticateUser();
        }

        public RTTTreatmentFunctionCode(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid RTTTreatmentFunctionCodeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, RTTTreatmentFunctionCodeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateRTTTreatmentFunctionCode(Guid OwnerId, string Name, DateTime StartDate, int? GovCode = null, string RollupTreatmentFunctionCode = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "GovCode", GovCode);
            AddFieldToBusinessDataObject(dataObject, "RollupTreatmentFunctionCode", RollupTreatmentFunctionCode);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public void DeleteRTTTreatmentFunctionCodeRecord(Guid RTTTreatmentFunctionCodeId)
        {
            this.DeleteRecord(TableName, RTTTreatmentFunctionCodeId);
        }

    }
}
