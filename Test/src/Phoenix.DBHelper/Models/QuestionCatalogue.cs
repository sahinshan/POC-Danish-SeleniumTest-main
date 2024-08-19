using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class QuestionCatalogue : BaseClass
    {

        private string tableName = "QuestionCatalogue";
        private string primaryKeyName = "QuestionCatalogueId";

        public QuestionCatalogue()
        {
            AuthenticateUser();
        }

        public QuestionCatalogue(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateNumericQuestion(string Question, string SubHeading)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Question", Question);
            AddFieldToBusinessDataObject(dataObject, "SubHeading", SubHeading);

            AddFieldToBusinessDataObject(dataObject, "ReadOnly", false);
            AddFieldToBusinessDataObject(dataObject, "Locked", false);
            AddFieldToBusinessDataObject(dataObject, "IsDateTime", false);
            AddFieldToBusinessDataObject(dataObject, "CaptureMultipleViews", false);
            AddFieldToBusinessDataObject(dataObject, "QuestionTypeId", 8);
            AddFieldToBusinessDataObject(dataObject, "FlagTypeId", 1);
            AddFieldToBusinessDataObject(dataObject, "DisplayQuestionText", false);
            AddFieldToBusinessDataObject(dataObject, "DisplayHyperlink", false);
            AddFieldToBusinessDataObject(dataObject, "DisplayBorder", true);
            AddFieldToBusinessDataObject(dataObject, "ShowNewButton", false);
            AddFieldToBusinessDataObject(dataObject, "PreventFutureDates", false);
            AddFieldToBusinessDataObject(dataObject, "EnableDrillThrough", false);
            AddFieldToBusinessDataObject(dataObject, "allowrecordingneeds", false);
            AddFieldToBusinessDataObject(dataObject, "decimalprecision", 2);


            return this.CreateRecord(dataObject);
        }

        public Guid CreateQuestion(string Question, string SubHeading, int questionTypeId)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Question", Question);
            AddFieldToBusinessDataObject(dataObject, "SubHeading", SubHeading);

            AddFieldToBusinessDataObject(dataObject, "ReadOnly", 0);
            AddFieldToBusinessDataObject(dataObject, "Locked", 1);
            AddFieldToBusinessDataObject(dataObject, "IsDateTime", 0);
            AddFieldToBusinessDataObject(dataObject, "CaptureMultipleViews", 0);
            AddFieldToBusinessDataObject(dataObject, "AvailabilityId", 5);
            AddFieldToBusinessDataObject(dataObject, "QuestionTypeId", questionTypeId);
            AddFieldToBusinessDataObject(dataObject, "FlagTypeId", 1);
            AddFieldToBusinessDataObject(dataObject, "DisplayQuestionText", 0);
            AddFieldToBusinessDataObject(dataObject, "DisplayHyperlink", 0);
            AddFieldToBusinessDataObject(dataObject, "DisplayBorder", 1);
            AddFieldToBusinessDataObject(dataObject, "ShowNewButton", 0);
            AddFieldToBusinessDataObject(dataObject, "PreventFutureDates", 0);
            AddFieldToBusinessDataObject(dataObject, "EnableDrillThrough", 0);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByQuestionName(string Question)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Question", ConditionOperatorType.Equal, Question);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByQuestionName(string Question, int questionTypeId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Question", ConditionOperatorType.Equal, Question);
            this.BaseClassAddTableCondition(query, "QuestionTypeId", ConditionOperatorType.Equal, questionTypeId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetQuestionCatalogueByID(Guid QuestionCatalogueId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, QuestionCatalogueId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateAvailability(Guid QuestionCatalogueID, int AvailabilityId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, QuestionCatalogueID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "AvailabilityId", AvailabilityId);

            this.UpdateRecord(buisinessDataObject);
        }


    }
}
