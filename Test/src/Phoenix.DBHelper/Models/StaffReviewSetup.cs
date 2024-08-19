using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class StaffReviewSetup : BaseClass
    {

        public string TableName = "StaffReviewSetup";
        public string PrimaryKeyName = "StaffReviewSetupId";


        public StaffReviewSetup()
        {
            AuthenticateUser();
        }

        public StaffReviewSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateStaffReviewSetup(string Name, Guid StaffReviewFormId, DateTime ValidFrom, string Description, bool AllBusinessUnits, bool AllRoles, bool UpdateStaffReviewItem)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            //AddFieldToBusinessDataObject(dataObject, "name", Name);
            AddFieldToBusinessDataObject(dataObject, "StaffReviewFormId", StaffReviewFormId);
            AddFieldToBusinessDataObject(dataObject, "AllBusinessUnits", AllBusinessUnits);
            AddFieldToBusinessDataObject(dataObject, "AllRoles", AllRoles);
            AddFieldToBusinessDataObject(dataObject, "UpdateStaffReviewItem", UpdateStaffReviewItem);
            AddFieldToBusinessDataObject(dataObject, "validfrom", ValidFrom);
            AddFieldToBusinessDataObject(dataObject, "description", Description);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateStaffReviewSetup(string Name, Guid StaffReviewFormId, DateTime ValidFrom, string Description, bool AllBusinessUnits, bool AllRoles, bool UpdateStaffReviewItem, Guid BookingTypeId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "name", Name);
            AddFieldToBusinessDataObject(dataObject, "StaffReviewFormId", StaffReviewFormId);
            AddFieldToBusinessDataObject(dataObject, "AllBusinessUnits", AllBusinessUnits);
            AddFieldToBusinessDataObject(dataObject, "AllRoles", AllRoles);
            AddFieldToBusinessDataObject(dataObject, "UpdateStaffReviewItem", UpdateStaffReviewItem);
            AddFieldToBusinessDataObject(dataObject, "validfrom", ValidFrom);
            AddFieldToBusinessDataObject(dataObject, "description", Description);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "BookingTypeId", BookingTypeId);

            return this.CreateRecord(dataObject);
        }

        public void UpdateAllFields(Guid StaffReviewSetupId, bool Inactive, DateTime ValidFrom, DateTime? ValidTo, bool AllBusinessUnits, bool AllRoles,
            Guid? ReviewTypeId, int? FirstOccurrenceId, int? RepeatOccurrenceId, bool UpdateStaffReviewItem)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, StaffReviewSetupId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", Inactive);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ValidFrom", ValidFrom);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ValidTo", ValidTo);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "AllBusinessUnits", AllBusinessUnits);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "AllRoles", AllRoles);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ReviewTypeId", ReviewTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "FirstOccurrenceId", FirstOccurrenceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RepeatOccurrenceId", RepeatOccurrenceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "UpdateStaffReviewItem", UpdateStaffReviewItem);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetBySystemUserId(Guid createdby)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "createdby", ConditionOperatorType.Equal, createdby);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetStaffReviewIdByUserIdandStaffReviewName(Guid createdby, string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "createdby", ConditionOperatorType.Equal, createdby);
            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);


        }

        public List<Guid> GetActiveRecordsByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);
            this.BaseClassAddTableCondition(query, "inactive", ConditionOperatorType.Equal, false);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetActiveRecordsByDocument(Guid StaffReviewFormId, bool AllBusinessUnits, bool AllRoles)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "StaffReviewFormId", ConditionOperatorType.Equal, StaffReviewFormId);
            this.BaseClassAddTableCondition(query, "AllBusinessUnits", ConditionOperatorType.Equal, AllBusinessUnits);
            this.BaseClassAddTableCondition(query, "AllRoles", ConditionOperatorType.Equal, AllRoles);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByFormId(Guid staffreviewformid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "staffreviewformid", ConditionOperatorType.Equal, staffreviewformid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetRecordsForAllRoles()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "allroles", ConditionOperatorType.Equal, true);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetStaffReviewFormByID(Guid staffreviewformid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, staffreviewformid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteStaffReviewSetup(Guid StaffReviewSetupId)
        {
            this.DeleteRecord(TableName, StaffReviewSetupId);
        }

        public void UpdateBookingTypeId(Guid StaffReviewSetupId, Guid BookingTypeId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, StaffReviewSetupId);
            AddFieldToBusinessDataObject(buisinessDataObject, "BookingTypeId", BookingTypeId);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
