using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class TrainingRequirement : BaseClass
    {
        public string TableName = "TrainingRequirement";
        public string PrimaryKeyName = "TrainingRequirementId";

        public TrainingRequirement()
        {
            AuthenticateUser();
        }

        public TrainingRequirement(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateTrainingRequirement(string title, Guid ownerid, Guid courseTitleId, DateTime ValidFromDate, DateTime? ValidToDate, int? trainingRecurrenceId, int categoryId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "title", title);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "coursetitleid", courseTitleId);
            AddFieldToBusinessDataObject(dataObject, "ValidFromDate", ValidFromDate);
            AddFieldToBusinessDataObject(dataObject, "ValidToDate", ValidToDate);
            AddFieldToBusinessDataObject(dataObject, "trainingrecurrenceid", trainingRecurrenceId);
            AddFieldToBusinessDataObject(dataObject, "categoryid", categoryId);

            AddFieldToBusinessDataObject(dataObject, "AllBusinessUnits", true);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateTrainingRequirement(Guid ownerid, Guid courseTitleId, DateTime ValidFromDate, DateTime? ValidToDate, int? trainingDuration, int? trainingCourseCapacity, int? trainingRecurrenceId, int? categoryId, Guid? trainingProviderId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "coursetitleid", courseTitleId);
            AddFieldToBusinessDataObject(dataObject, "ValidFromDate", ValidFromDate);
            AddFieldToBusinessDataObject(dataObject, "ValidToDate", ValidToDate);
            AddFieldToBusinessDataObject(dataObject, "trainingrecurrenceid", trainingRecurrenceId);
            AddFieldToBusinessDataObject(dataObject, "categoryid", categoryId);
            AddFieldToBusinessDataObject(dataObject, "trainindduration", trainingDuration);
            AddFieldToBusinessDataObject(dataObject, "trainingcoursecapacity", trainingCourseCapacity);
            AddFieldToBusinessDataObject(dataObject, "trainindduration", trainingDuration);
            AddFieldToBusinessDataObject(dataObject, "trainingproviderid", trainingProviderId);

            AddFieldToBusinessDataObject(dataObject, "AllBusinessUnits", true);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string TrainingRequirementName, int categoryId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, TrainingRequirementName);
            this.BaseClassAddTableCondition(query, "categoryid", ConditionOperatorType.Equal, categoryId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetTrainingRequirementByID(Guid TrainingRequirementId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, TrainingRequirementId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteTrainingRequirement(Guid TrainingRequirementId)
        {
            this.DeleteRecord(TableName, TrainingRequirementId);
        }

        public List<Guid> GetTrainingRequirementByTrainingItem(Guid CourseTitleId, int? categoryid = null)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "coursetitleid", ConditionOperatorType.Equal, CourseTitleId);
            if (categoryid.HasValue)
                this.BaseClassAddTableCondition(query, "categoryid", ConditionOperatorType.Equal, categoryid.Value);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateValidToDate(Guid TrainingRequirementId, DateTime ValidToDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, TrainingRequirementId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ValidToDate", ValidToDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateInactive(Guid TrainingRequirementId, bool inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, TrainingRequirementId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateRecurrence(Guid TrainingRequirementId, int trainingRecurrenceId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, TrainingRequirementId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "trainingrecurrenceid", trainingRecurrenceId);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
