using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SystemUserSuspension : BaseClass
    {

        public string TableName = "SystemUserSuspension";
        public string PrimaryKeyName = "SystemUserSuspensionId";


        public SystemUserSuspension()
        {
            AuthenticateUser();
        }

        public SystemUserSuspension(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetSystemUserSuspensionByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetSystemUserSuspensionByID(Guid SystemUserSuspensionId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemUserSuspensionId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateSystemUserSuspension(Guid systemuserid, DateTime startdate, Guid ownerid, Guid suspensionReasonId, List<Guid> contracts)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "SystemUserId", systemuserid);
            AddFieldToBusinessDataObject(dataObject, "SuspensionStartDate", startdate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", ownerid);
            AddFieldToBusinessDataObject(dataObject, "SuspensionReasonId", suspensionReasonId);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            dataObject.MultiSelectBusinessObjectFields["contracts"] = new MultiSelectBusinessObjectDataCollection();

            if (contracts != null && contracts.Count > 0)
            {
                foreach (Guid contractId in contracts)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = contractId,
                        ReferenceIdTableName = "SystemUserEmploymentContract",
                    };
                    dataObject.MultiSelectBusinessObjectFields["contracts"].Add(dataRecord);
                }
            }

            return this.CreateRecord(dataObject);
        }

        public void UpdateSuspensionEndDate(Guid SystemUserSuspensionId, DateTime? suspensionenddate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserSuspensionId);
            AddFieldToBusinessDataObject(buisinessDataObject, "suspensionenddate", suspensionenddate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateSuspensionEndDate(Guid SystemUserSuspensionId, DateTime? suspensionenddate, List<Guid> contracts)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserSuspensionId);
            AddFieldToBusinessDataObject(buisinessDataObject, "suspensionenddate", suspensionenddate);

            buisinessDataObject.MultiSelectBusinessObjectFields["contracts"] = new MultiSelectBusinessObjectDataCollection();

            if (contracts != null && contracts.Count > 0)
            {
                foreach (Guid contractId in contracts)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = contractId,
                        ReferenceIdTableName = "SystemUserEmploymentContract",
                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields["contracts"].Add(dataRecord);
                }
            }

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteSystemUserSuspension(Guid SystemUserSuspensionId)
        {
            this.DeleteRecord(TableName, SystemUserSuspensionId);
        }

        public List<Guid> GetSystemUserSuspensionBySystemUserId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
    }
}
