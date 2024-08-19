using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RecruitmentRequirement : BaseClass
    {

        public string TableName = "RecruitmentRequirement";
        public string PrimaryKeyName = "RecruitmentRequirementid";


        public RecruitmentRequirement()
        {
            AuthenticateUser();
        }

        public RecruitmentRequirement(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateRecruitmentRequirement(Guid ownerid, string RequirementName, Guid RequiredItemId, string requireditemidtablename, string requireditemidname, bool AllRoles,
            int InductionNumber, int? InductionStatusId, int AcceptanceNumber, int? AcceptanceStatusId, DateTime StartDate, DateTime? EndDate, List<Guid> CareProviderStaffRoleTypeIds)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "RequirementName", RequirementName);
            AddFieldToBusinessDataObject(dataObject, "RequiredItemId", RequiredItemId);
            AddFieldToBusinessDataObject(dataObject, "requireditemidtablename", requireditemidtablename);
            AddFieldToBusinessDataObject(dataObject, "requireditemidname", requireditemidname);
            AddFieldToBusinessDataObject(dataObject, "AllRoles", AllRoles);

            AddFieldToBusinessDataObject(dataObject, "InductionNumber", InductionNumber);
            AddFieldToBusinessDataObject(dataObject, "InductionStatusId", InductionStatusId);
            AddFieldToBusinessDataObject(dataObject, "AcceptanceNumber", AcceptanceNumber);
            AddFieldToBusinessDataObject(dataObject, "AcceptanceStatusId", AcceptanceStatusId);

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

            AddFieldToBusinessDataObject(dataObject, "startdate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "EndDate", EndDate);

            AddFieldToBusinessDataObject(dataObject, "AllBusinessUnits", true);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "AffectsNextDocumentCreationProcess", false);
            AddFieldToBusinessDataObject(dataObject, "AffectsNextApplicationIndicatorsUpdate", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string RequirementName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "requirementname", ConditionOperatorType.Equal, RequirementName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
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

        public Dictionary<string, object> GetRecruitmentRequirementByID(Guid RecruitmentRequirementId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, RecruitmentRequirementId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteRecruitmentRequirement(Guid RecruitmentRequirementId)
        {
            this.DeleteRecord(TableName, RecruitmentRequirementId);
        }

        public List<Guid> GetAll()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
