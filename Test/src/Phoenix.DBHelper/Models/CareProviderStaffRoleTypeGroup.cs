using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderStaffRoleTypeGroup : BaseClass
    {

        public string TableName = "CareProviderStaffRoleTypeGroup";
        public string PrimaryKeyName = "CareProviderStaffRoleTypeGroupId";


        public CareProviderStaffRoleTypeGroup()
        {
            AuthenticateUser();
        }

        public CareProviderStaffRoleTypeGroup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareProviderStaffRoleTypeGroup(Guid ownerid, string name, string code, string govcode, DateTime StartDate, string description, bool inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "code", code);
            AddFieldToBusinessDataObject(dataObject, "govcode", govcode);
            AddFieldToBusinessDataObject(dataObject, "startdate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "description", description);
            AddFieldToBusinessDataObject(dataObject, "inactive", inactive);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCareProviderStaffRoleTypeGroupByID(Guid CareProviderStaffRoleTypeGroupId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderStaffRoleTypeGroupId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCareProviderStaffRoleTypeGroup(Guid CareProviderStaffRoleTypeGroupId)
        {
            this.DeleteRecord(TableName, CareProviderStaffRoleTypeGroupId);
        }
    }
}
