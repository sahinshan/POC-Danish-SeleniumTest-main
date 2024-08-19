using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RecruitmentRequirementStaffRole : BaseClass
    {

        public string TableName = "RecruitmentRequirementStaffRole";
        public string PrimaryKeyName = "RecruitmentRequirementStaffRoleid";


        public RecruitmentRequirementStaffRole()
        {
            AuthenticateUser();
        }

        public RecruitmentRequirementStaffRole(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateRecruitmentRequirementStaffRole(Guid RecruitmentRequirementId, Guid CareProviderStaffRoleTypeId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "RecruitmentRequirementId", RecruitmentRequirementId);
            AddFieldToBusinessDataObject(dataObject, "CareProviderStaffRoleTypeId", CareProviderStaffRoleTypeId);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByRecruitmentRequirementId(Guid RecruitmentRequirementId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RecruitmentRequirementId", ConditionOperatorType.Equal, RecruitmentRequirementId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteRecruitmentRequirementStaffRole(Guid RecruitmentRequirementStaffRoleId)
        {
            this.DeleteRecord(TableName, RecruitmentRequirementStaffRoleId);
        }
    }
}
