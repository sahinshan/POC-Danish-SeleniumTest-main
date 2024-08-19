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
    public class PersonCaseNotesPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _addNewRecordButton = e => e.Marked("personcasenote_NewRecordButton");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("PERSON CASE NOTES");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");


        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked(RecordText);
        Func<AppQuery, AppQuery> _toggleIcon(string CaseNoteRecordID) => e => e.Marked("personcasenote_Row_" + CaseNoteRecordID).Descendant().Marked("toggleIcon");


        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("personcasenote_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");


        #region Mobile mode labels

        Func<AppQuery, AppQuery> _subjectLabel(string RecordID) => e => e.Marked("personcasenote_PrimaryRow_" + RecordID).Descendant().Marked("subject_0_Label");
        Func<AppQuery, AppQuery> _createdOnLabel(string RecordID) => e => e.Marked("personcasenote_PrimaryRow_" + RecordID).Descendant().Marked("createdon_cwname_1_Label");
        Func<AppQuery, AppQuery> _responsibleUserLabel(string RecordID) => e => e.Marked("personcasenote_PrimaryRow_" + RecordID).Descendant().Marked("responsibleuserid_cwname_2_Label");
        Func<AppQuery, AppQuery> _createdbyLabel(string RecordID) => e => e.Marked("personcasenote_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_3_Label");
        Func<AppQuery, AppQuery> _modifiedbyLabel(string RecordID) => e => e.Marked("personcasenote_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_4_Label");
        Func<AppQuery, AppQuery> _modifiedOnLabel(string RecordID) => e => e.Marked("personcasenote_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_5_Label");

        #endregion

        #region Mobile mode field values

        Func<AppQuery, AppQuery> _subjectValue(string RecordID) => e => e.Marked("personcasenote_PrimaryRow_" + RecordID).Descendant().Marked("subject_0_Value");
        Func<AppQuery, AppQuery> _createdOnValue(string RecordID) => e => e.Marked("personcasenote_PrimaryRow_" + RecordID).Descendant().Marked("createdon_cwname_1_Value");
        Func<AppQuery, AppQuery> _responsibleUserValue(string RecordID) => e => e.Marked("personcasenote_PrimaryRow_" + RecordID).Descendant().Marked("responsibleuserid_cwname_2_Value");
        Func<AppQuery, AppQuery> _createdbyValue(string RecordID) => e => e.Marked("personcasenote_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_3_Value");
        Func<AppQuery, AppQuery> _modifiedbyValue(string RecordID) => e => e.Marked("personcasenote_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_4_Value");
        Func<AppQuery, AppQuery> _modifiedOnValue(string RecordID) => e => e.Marked("personcasenote_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_5_Value");

        #endregion


        public PersonCaseNotesPage(IApp app)
        {
            _app = app;
        }


        public PersonCaseNotesPage WaitForPersonCaseNotesPageToLoad(string ViewText = "My Open Case Notes")
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_peoplePageIconButton);
            WaitForElement(_pageTitle);

            WaitForElement(_viewPicker(ViewText));

            WaitForElement(_searchTextBox);
            WaitForElement(_searchButton);
            WaitForElement(_refreshButton);

            return this;
        }


        #region Methods used when displaying the APP in mobile mode

        public PersonCaseNotesPage TapToogleButton(string CaseNoteRecordID)
        {
            ScrollToElement(_toggleIcon(CaseNoteRecordID));
            Tap(_toggleIcon(CaseNoteRecordID));

            return this;
        }

        public PersonCaseNotesPage ValidateSubjectFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_subjectValue(RecordID));
            string elementText = GetElementText(_subjectValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonCaseNotesPage ValidateCreatedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdOnValue(RecordID));
            string elementText = GetElementText(_createdOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonCaseNotesPage ValidateCreatedOnFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_createdOnValue(RecordID));
            WaitForElementNotVisible(_createdOnValue(RecordID));

            return this;
        }

        public PersonCaseNotesPage ValidateResponsibleUserFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_responsibleUserValue(RecordID));
            string elementText = GetElementText(_responsibleUserValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonCaseNotesPage ValidateResponsibleUserFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_responsibleUserValue(RecordID));
            WaitForElementNotVisible(_responsibleUserValue(RecordID));

            return this;
        }

        public PersonCaseNotesPage ValidateCreatedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdbyValue(RecordID));
            string elementText = GetElementText(_createdbyValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonCaseNotesPage ValidateCreatedByFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_createdbyValue(RecordID));
            WaitForElementNotVisible(_createdbyValue(RecordID));

            return this;
        }

        public PersonCaseNotesPage ValidateModifiedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedbyValue(RecordID));
            string elementText = GetElementText(_modifiedbyValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonCaseNotesPage ValidateModifiedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedOnValue(RecordID));
            string elementText = GetElementText(_modifiedOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        #endregion

        #region Methods used when displaying the APP in tablet mode

        public PersonCaseNotesPage ValidateSubjectCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "subject", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonCaseNotesPage ValidateCreatedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdon", ExpectedText));

            if(string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonCaseNotesPage ValidateResponsibleUserCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "responsibleuserid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonCaseNotesPage ValidateCreatedByCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdby", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonCaseNotesPage ValidateModifiedByText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedby", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonCaseNotesPage ValidateModifiedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedon", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }


        #endregion


        public PersonCaseNotesPage TapOnAddNewRecordButton()
        {
            this.Tap(_addNewRecordButton);

            return this;
        }

        public PersonCaseNotesPage TapOnRecord(string recordText)
        {
            WaitForElement(_recordText(recordText));
            Tap(_recordText(recordText));

            return this;
        }

        public PersonCaseNotesPage ValidateRecordPresent(string recordText)
        {
            WaitForElement(_recordText(recordText));

            return this;
        }

        public PersonCaseNotesPage ValidateRecordNotPresent(string recordText)
        {
            WaitForElementNotVisible(_recordText(recordText));

            return this;
        }

        public PersonCaseNotesPage ValidateAddNewRecordButtonVisible()
        {
            WaitForElement(_addNewRecordButton);

            return this;
        }

        public PersonCaseNotesPage ValidateAddNewRecordButtonNotVisible()
        {
            WaitForElementNotVisible(_addNewRecordButton);

            return this;
        }

        public PersonCaseNotesPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public PersonCaseNotesPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
