using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentSection : BaseClass
    {

        private string tableName = "DocumentSection";
        private string primaryKeyName = "DocumentSectionId";

        public DocumentSection()
        {
            AuthenticateUser();
        }

        public DocumentSection(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDocumentSection(string sectionname, Guid DocumentId)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "sectionname", sectionname);
            AddFieldToBusinessDataObject(dataObject, "DocumentId", DocumentId);

            AddFieldToBusinessDataObject(dataObject, "AddExtraPageForNotes", 0);
            AddFieldToBusinessDataObject(dataObject, "DisplayPosition", 1);
            AddFieldToBusinessDataObject(dataObject, "HideSectionOnPrint", 0);
            AddFieldToBusinessDataObject(dataObject, "PrintInstructions", 1);
            AddFieldToBusinessDataObject(dataObject, "PrintQuestionNumber", 0);
            AddFieldToBusinessDataObject(dataObject, "PrintSectionName", 1);
            AddFieldToBusinessDataObject(dataObject, "ShowSectionName", 1);
            AddFieldToBusinessDataObject(dataObject, "HierarchyLevel", 1);
            AddFieldToBusinessDataObject(dataObject, "SignoffRequired", 0);
            AddFieldToBusinessDataObject(dataObject, "ConditionalSignoffRequired", 0);
            AddFieldToBusinessDataObject(dataObject, "HeadingBackgroundColorId", 12);
            AddFieldToBusinessDataObject(dataObject, "HeadingBold", 0);
            AddFieldToBusinessDataObject(dataObject, "OverrideDocumentFormatting", 0);
            AddFieldToBusinessDataObject(dataObject, "HeadingFontColorId", 11);
            AddFieldToBusinessDataObject(dataObject, "HeadingFontSizeId", 14);
            AddFieldToBusinessDataObject(dataObject, "HeadingItalic", 0);
            AddFieldToBusinessDataObject(dataObject, "HeadingUnderline", 0);
            AddFieldToBusinessDataObject(dataObject, "PrintLayoutId", 1);
            AddFieldToBusinessDataObject(dataObject, "CanPrintThisIndividually", 1);
            AddFieldToBusinessDataObject(dataObject, "QuestionPositionId", 1);
            AddFieldToBusinessDataObject(dataObject, "AvailableInMobile", 0);
            AddFieldToBusinessDataObject(dataObject, "UserNotesDisplayExpanded", 0);
            AddFieldToBusinessDataObject(dataObject, "AvailabilityId", 1);
            AddFieldToBusinessDataObject(dataObject, "MDTSummarySection", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByDocumentIdAndName(Guid DocumentId, string sectionname)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DocumentId", ConditionOperatorType.Equal, DocumentId);
            this.BaseClassAddTableCondition(query, "sectionname", ConditionOperatorType.Equal, sectionname);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }
        public List<Guid> GetByDocumentId(Guid DocumentId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "DocumentId", ConditionOperatorType.Equal, DocumentId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetDocumentSectionByID(Guid DocumentSectionId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DocumentSectionId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateAvailability(Guid DocumentSectionID, int AvailabilityId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DocumentSectionID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "AvailabilityId", AvailabilityId);

            this.UpdateRecord(buisinessDataObject);
        }


    }
}
