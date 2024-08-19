using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class StaffRecruitmentItem : BaseClass
    {

        public string TableName = "StaffRecruitmentItem";
        public string PrimaryKeyName = "StaffRecruitmentItemid";


        public StaffRecruitmentItem()
        {
            AuthenticateUser();
        }

        public StaffRecruitmentItem(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateStaffRecruitmentItem(Guid ownerid, string Name, DateTime StartDate, int? compliancerecurrenceid = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "startdate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "compliancerecurrenceid", compliancerecurrenceid);

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

        public Dictionary<string, object> GetStaffRecruitmentItemByID(Guid StaffRecruitmentItemId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, StaffRecruitmentItemId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteStaffRecruitmentItem(Guid StaffRecruitmentItemId)
        {
            this.DeleteRecord(TableName, StaffRecruitmentItemId);
        }
    }
}
