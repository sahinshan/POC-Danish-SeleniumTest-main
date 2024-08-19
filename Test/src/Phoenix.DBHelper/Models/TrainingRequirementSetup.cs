using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class TrainingRequirementSetup : BaseClass
    {
        public string TableName = "TrainingRequirementSetup";
        public string PrimaryKeyName = "TrainingRequirementSetupId";


        public TrainingRequirementSetup()
        {
            AuthenticateUser();
        }

        public TrainingRequirementSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateTrainingRequirementSetup(string RecordName, Guid TrainingItemId, DateTime ValidFromDate, DateTime? ValidToDate, bool AllRoles,
            int StatusId, List<Guid> CareProviderStaffRoleTypeIds)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "RecordName", RecordName);
            AddFieldToBusinessDataObject(dataObject, "TrainingItemId", TrainingItemId);
            //AddFieldToBusinessDataObject(dataObject, "requireditemidtablename", requireditemidtablename);
            //AddFieldToBusinessDataObject(dataObject, "requireditemidname", requireditemidname);
            AddFieldToBusinessDataObject(dataObject, "ValidFromDate", ValidFromDate);
            AddFieldToBusinessDataObject(dataObject, "ValidToDate", ValidToDate);
            AddFieldToBusinessDataObject(dataObject, "AllRoles", AllRoles);
            AddFieldToBusinessDataObject(dataObject, "StatusId", StatusId);

            dataObject.MultiSelectBusinessObjectFields["selectedrolesid"] = new MultiSelectBusinessObjectDataCollection();

            if (CareProviderStaffRoleTypeIds != null && CareProviderStaffRoleTypeIds.Count > 0)
            {
                foreach (Guid _careProviderStaffRoleTypeId in CareProviderStaffRoleTypeIds)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = _careProviderStaffRoleTypeId,
                        ReferenceIdTableName = "CareProviderStaffRoleType"
                    };
                    dataObject.MultiSelectBusinessObjectFields["selectedrolesid"].Add(dataRecord);
                }
            }

            AddFieldToBusinessDataObject(dataObject, "AllBusinessUnits", true);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string TrainingRequirementSetupName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RecordName", ConditionOperatorType.Equal, TrainingRequirementSetupName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByTitle(string TrainingItemTypeName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, TrainingItemTypeName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetTrainingRequirementByID(Guid TrainingRequirementSetupId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, TrainingRequirementSetupId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteTrainingRequirement(Guid TrainingRequirementSetupId)
        {
            this.DeleteRecord(TableName, TrainingRequirementSetupId);
        }

        public List<Guid> GetByAllRoles(bool allroles)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "allroles", ConditionOperatorType.Equal, allroles);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateAllRoles(Guid RecruitmentRequirementid, bool allroles, List<Guid> selectedrolesid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, RecruitmentRequirementid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "allroles", allroles);

            buisinessDataObject.MultiSelectBusinessObjectFields["selectedrolesid"] = new MultiSelectBusinessObjectDataCollection();

            if (selectedrolesid != null && selectedrolesid.Count > 0)
            {
                foreach (Guid _careProviderStaffRoleTypeId in selectedrolesid)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = _careProviderStaffRoleTypeId,
                        ReferenceIdTableName = "CareProviderStaffRoleType"
                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields["selectedrolesid"].Add(dataRecord);
                }
            }

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
