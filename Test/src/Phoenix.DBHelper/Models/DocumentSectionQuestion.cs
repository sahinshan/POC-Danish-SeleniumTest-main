using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentSectionQuestion : BaseClass
    {

        private string tableName = "DocumentSectionQuestion";
        private string primaryKeyName = "DocumentSectionQuestionId";

        public DocumentSectionQuestion()
        {
            AuthenticateUser();
        }

        public DocumentSectionQuestion(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDocumentSectionQuestion(Guid QuestionCatalogueId, Guid SectionId)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "QuestionCatalogueId", QuestionCatalogueId);
            AddFieldToBusinessDataObject(dataObject, "SectionId", SectionId);

            AddFieldToBusinessDataObject(dataObject, "DisplayPosition", 1);
            AddFieldToBusinessDataObject(dataObject, "DefaultSDEEnabled", 0);
            AddFieldToBusinessDataObject(dataObject, "DisplayInnerBorder", 0);
            AddFieldToBusinessDataObject(dataObject, "DisplayOuterBorder", 0);
            AddFieldToBusinessDataObject(dataObject, "HideQuestionText", 0);
            AddFieldToBusinessDataObject(dataObject, "IncludeUserComments", 0);
            AddFieldToBusinessDataObject(dataObject, "PrintInstructions", 0);
            AddFieldToBusinessDataObject(dataObject, "PrintSelectedValueOnly", 0);
            AddFieldToBusinessDataObject(dataObject, "ShowQuestionTextInsideBox", 0);
            AddFieldToBusinessDataObject(dataObject, "ShowYesCheckboxOnly", 0);
            AddFieldToBusinessDataObject(dataObject, "PrintRowPosition", 1);
            AddFieldToBusinessDataObject(dataObject, "PrintColumnPositionId", 1);
            AddFieldToBusinessDataObject(dataObject, "ApplicableToSnapshot", 0);
            AddFieldToBusinessDataObject(dataObject, "AvailableInMobile", 0);
            AddFieldToBusinessDataObject(dataObject, "HideSubHeadingText", 0);
            AddFieldToBusinessDataObject(dataObject, "UserNotesDisplayExpanded", 0);
            AddFieldToBusinessDataObject(dataObject, "AvailabilityId", 1);
            AddFieldToBusinessDataObject(dataObject, "ApplyToAllGroupMembers", 0);
            AddFieldToBusinessDataObject(dataObject, "PrintQuestionText", 1);
            AddFieldToBusinessDataObject(dataObject, "PrintSubHeadingText", 1);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);


            return this.CreateRecord(dataObject);
        }


        public List<Guid> GetBySectionIdAndQuestionCatalogueId(Guid SectionId, Guid QuestionCatalogueId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "SectionId", ConditionOperatorType.Equal, SectionId);
            this.BaseClassAddTableCondition(query, "QuestionCatalogueId", ConditionOperatorType.Equal, QuestionCatalogueId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetBySectionId(Guid SectionId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "SectionId", ConditionOperatorType.Equal, SectionId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetDocumentSectionQuestionByID(Guid DocumentSectionQuestionId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentSectionQuestionId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateAvailability(Guid DocumentSectionQuestionID, int AvailabilityId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentSectionQuestionID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "AvailabilityId", AvailabilityId);

            this.UpdateRecord(buisinessDataObject);
        }


    }
}
