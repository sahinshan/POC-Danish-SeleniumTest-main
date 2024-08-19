using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderStaffRoleType : BaseClass
    {

        public string TableName = "CareProviderStaffRoleType";
        public string PrimaryKeyName = "CareProviderStaffRoleTypeId";


        public CareProviderStaffRoleType()
        {
            AuthenticateUser();
        }

        public CareProviderStaffRoleType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareProviderStaffRoleType(Guid ownerid, string name, string code, string govcode,
            DateTime StartDate, string description)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "code", code);
            AddFieldToBusinessDataObject(dataObject, "govcode", govcode);
            AddFieldToBusinessDataObject(dataObject, "startdate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "description", description);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
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

        public Dictionary<string, object> GetCareProviderStaffRoleTypeByID(Guid CareProviderStaffRoleTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderStaffRoleTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCareProviderStaffRoleType(Guid CareProviderStaffRoleTypeId)
        {
            this.DeleteRecord(TableName, CareProviderStaffRoleTypeId);
        }

        public void UpdateDeliversCare(Guid CareProviderStaffRoleTypeId, bool DeliversCare)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, CareProviderStaffRoleTypeId);
            AddFieldToBusinessDataObject(dataObject, "DeliversCare", DeliversCare);

            this.UpdateRecord(dataObject);
        }

        public void UpdateInactive(Guid CareProviderStaffRoleTypeId, bool Inactive)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, CareProviderStaffRoleTypeId);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            this.UpdateRecord(dataObject);
        }
    }
}
