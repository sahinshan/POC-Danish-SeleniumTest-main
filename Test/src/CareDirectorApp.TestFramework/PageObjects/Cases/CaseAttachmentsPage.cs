using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CareDirectorApp.TestFramework.PageObjects
{
    public class CaseAttachmentsPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _addNewRecordButton = e => e.Marked("caseattachment_NewRecordButton");
        readonly Func<AppQuery, AppQuery> _casesPageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("ATTACHMENTS (FOR CASE)");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");


        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked(RecordText);
        Func<AppQuery, AppQuery> _toggleIcon(string CaseNoteRecordID) => e => e.Marked("caseattachment_Row_" + CaseNoteRecordID).Descendant().Marked("toggleIcon");


        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("caseattachment_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");


        #region Mobile mode labels

        Func<AppQuery, AppQuery> _titleLabel(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("title_0_Label");
        Func<AppQuery, AppQuery> _dateLabel(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("date_1_Label");
        Func<AppQuery, AppQuery> _documentTypeLabel(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("documenttypeid_cwname_2_Label");
        Func<AppQuery, AppQuery> _documentSubTypeLabel(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("documentsubtypeid_cwname_3_Label");
        Func<AppQuery, AppQuery> _createdbyLabel(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_4_Label");
        Func<AppQuery, AppQuery> _createdOnLabel(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("createdon_5_Label");
        Func<AppQuery, AppQuery> _modifiedByLabel(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_6_Label");

        #endregion

        #region Mobile mode field values

        Func<AppQuery, AppQuery> _titleValue(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("title_0_Value");
        Func<AppQuery, AppQuery> _dateValue(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("date_1_Value");
        Func<AppQuery, AppQuery> _documentTypeValue(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("documenttypeid_cwname_2_Value");
        Func<AppQuery, AppQuery> _documentSubTypeValue(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("documentsubtypeid_cwname_3_Value");
        Func<AppQuery, AppQuery> _createdbyValue(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_4_Value");
        Func<AppQuery, AppQuery> _createdOnValue(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("createdon_5_Value");
        Func<AppQuery, AppQuery> _modifiedByValue(string RecordID) => e => e.Marked("caseattachment_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_6_Value");

        #endregion


        public CaseAttachmentsPage(IApp app)
        {
            _app = app;
        }


        public CaseAttachmentsPage WaitForCaseAttachmentsPageToLoad(string ViewText = "Related Records")
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_addNewRecordButton);
            WaitForElement(_casesPageIconButton);
            WaitForElement(_pageTitle);

            WaitForElement(_viewPicker(ViewText));

            WaitForElement(_searchTextBox);
            WaitForElement(_searchButton);
            WaitForElement(_refreshButton);

            return this;
        }


        #region Methods used when displaying the APP in mobile mode

        public CaseAttachmentsPage TapToogleButton(string CaseNoteRecordID)
        {
            ScrollToElement(_toggleIcon(CaseNoteRecordID));
            Tap(_toggleIcon(CaseNoteRecordID));

            return this;
        }

        public CaseAttachmentsPage ValidateTitleFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_titleValue(RecordID));
            string elementText = GetElementText(_titleValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseAttachmentsPage ValidateDateFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_dateValue(RecordID));
            string elementText = GetElementText(_dateValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseAttachmentsPage ValidateDateFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_dateValue(RecordID));
            WaitForElementNotVisible(_dateValue(RecordID));

            return this;
        }

        public CaseAttachmentsPage ValidateDocumentTypeFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_documentTypeValue(RecordID));
            string elementText = GetElementText(_documentTypeValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseAttachmentsPage ValidateDocumentTypeFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_documentTypeValue(RecordID));
            WaitForElementNotVisible(_documentTypeValue(RecordID));

            return this;
        }

        public CaseAttachmentsPage ValidateDocumentSubTypeFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_documentSubTypeValue(RecordID));
            string elementText = GetElementText(_documentSubTypeValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseAttachmentsPage ValidateDocumentSubTypeFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_documentSubTypeValue(RecordID));
            WaitForElementNotVisible(_documentSubTypeValue(RecordID));

            return this;
        }

        public CaseAttachmentsPage ValidateCreatedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdbyValue(RecordID));
            string elementText = GetElementText(_createdbyValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseAttachmentsPage ValidateCreatedByFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_createdbyValue(RecordID));
            WaitForElementNotVisible(_createdbyValue(RecordID));

            return this;
        }

        public CaseAttachmentsPage ValidateCreatedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdOnValue(RecordID));
            string elementText = GetElementText(_createdOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseAttachmentsPage ValidateCreatedOnFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_createdOnValue(RecordID));
            WaitForElementNotVisible(_createdOnValue(RecordID));

            return this;
        }

        public CaseAttachmentsPage ValidateModifiedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedByValue(RecordID));
            string elementText = GetElementText(_modifiedByValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        #endregion

        #region Methods used when displaying the APP in tablet mode

        public CaseAttachmentsPage ValidateTitleCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "title", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "title", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseAttachmentsPage ValidateDateCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "date", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "date", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseAttachmentsPage ValidateDocumentTypeCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "documenttypeid", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "documenttypeid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseAttachmentsPage ValidateDocumentSubTypeCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "documentsubtypeid", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "documentsubtypeid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseAttachmentsPage ValidateCreatedByCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "createdby", ExpectedText));

            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdby", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseAttachmentsPage ValidateCreatedOnCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "createdon", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdon", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseAttachmentsPage ValidateModifiedByCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "modifiedby", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedby", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }


        #endregion


        public CaseAttachmentsPage TapOnAddNewRecordButton()
        {
            this.Tap(_addNewRecordButton);

            return this;
        }

        public CaseAttachmentsPage TapOnRecord(string recordText)
        {
            WaitForElement(_recordText(recordText));
            Tap(_recordText(recordText));

            return this;
        }

        public CaseAttachmentsPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public CaseAttachmentsPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
