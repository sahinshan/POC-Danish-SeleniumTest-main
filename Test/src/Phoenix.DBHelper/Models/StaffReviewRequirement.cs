using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class StaffReviewRequirement : BaseClass
    {

        public string TableName = "StaffReview";
        public string PrimaryKeyName = "staffreviewid";


        public StaffReviewRequirement()
        {
            AuthenticateUser();
        }

        public StaffReviewRequirement(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateStaffReviewRequirement(Guid ownerid, string staffreviewformid_cwname, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "staffreviewformid_cwname", staffreviewformid_cwname);
            AddFieldToBusinessDataObject(dataObject, "startdate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }
        public Guid CreateStaffReviewRequirementId(Guid staffreviewformid_cwname, string description, bool AllBusinessUnits, bool AllRoles, DateTime ValidFrom,
          int FirstOccurrenceId, int RepeatOccurrenceId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "staffreviewformid_cwname", staffreviewformid_cwname);
            AddFieldToBusinessDataObject(dataObject, "AllBusinessUnits", AllBusinessUnits);
            AddFieldToBusinessDataObject(dataObject, "description", description);
            AddFieldToBusinessDataObject(dataObject, "AllRoles", AllRoles);
            AddFieldToBusinessDataObject(dataObject, "startdate", ValidFrom);
            // AddFieldToBusinessDataObject(dataObject, "ReviewTypeId", ReviewTypeId);
            AddFieldToBusinessDataObject(dataObject, "FirstOccurrenceId", FirstOccurrenceId);
            AddFieldToBusinessDataObject(dataObject, "RepeatOccurrenceId", RepeatOccurrenceId);

            return this.CreateRecord(dataObject);
        }
        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public List<Guid> GetStaffReviewFormId(Guid StaffReviewForm)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "StaffReviewForm", ConditionOperatorType.Equal, StaffReviewForm);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetStaffReviewRequirementByID(Guid StaffReviewRequirementId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, StaffReviewRequirementId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteStaffReviewRequirement(Guid StaffReviewRequirementId)
        {
            this.DeleteRecord(TableName, StaffReviewRequirementId);
        }
    }
}
