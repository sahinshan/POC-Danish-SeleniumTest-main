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
    public class CaseFormsPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _addNewRecordButton = e => e.Marked("caseform_NewRecordButton");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("FORMS (CASE)");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");


        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked(RecordText);
        Func<AppQuery, AppQuery> _toggleIcon(string RecordID) => e => e.Marked("caseform_Row_" + RecordID).Descendant().Marked("toggleIcon");


        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("caseform_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");


        #region Mobile mode labels

        Func<AppQuery, AppQuery> _FormTypeLabel(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("documentid_cwname_0_Label");
        Func<AppQuery, AppQuery> _StartDateLabel(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("startdate_1_Label");
        Func<AppQuery, AppQuery> _DueDateLabel(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("duedate_2_Label");
        Func<AppQuery, AppQuery> _StatusLabel(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("assessmentstatusid_cwname_3_Label");
        Func<AppQuery, AppQuery> _ResponsibleTeamLabel(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("responsibleteamid_cwname_4_Label");
        Func<AppQuery, AppQuery> _CompletedByLabel(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("completedbyid_cwname_5_Label");
        Func<AppQuery, AppQuery> _CompletionDateLabel(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("completiondate_6_Label");
        Func<AppQuery, AppQuery> _SignnedOffByLabel(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("signedoffbyid_cwname_7_Label");

        #endregion

        #region Mobile mode field values

        Func<AppQuery, AppQuery> _FormTypeValue(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("documentid_cwname_0_Value");
        Func<AppQuery, AppQuery> _StartDateValue(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("startdate_1_Value");
        Func<AppQuery, AppQuery> _DueDateValue(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("duedate_2_Value");
        Func<AppQuery, AppQuery> _StatusValue(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("assessmentstatusid_cwname_3_Value");
        Func<AppQuery, AppQuery> _ResponsibleTeamValue(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("responsibleteamid_cwname_4_Value");
        Func<AppQuery, AppQuery> _CompletedByValue(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("completedbyid_cwname_5_Value");
        Func<AppQuery, AppQuery> _CompletionDateValue(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("completiondate_6_Value");
        Func<AppQuery, AppQuery> _SignnedOffByValue(string RecordID) => e => e.Marked("caseform_PrimaryRow_" + RecordID).Descendant().Marked("signedoffbyid_cwname_7_Value");

        #endregion


        public CaseFormsPage(IApp app)
        {
            _app = app;
        }


        public CaseFormsPage WaitForCaseFormsPageToLoad(string ViewText = "Active Forms")
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_addNewRecordButton);
            WaitForElement(_peoplePageIconButton);
            WaitForElement(_pageTitle);

            WaitForElement(_viewPicker(ViewText));

            WaitForElement(_searchTextBox);
            WaitForElement(_searchButton);
            WaitForElement(_refreshButton);

            return this;
        }


        #region Methods used when displaying the APP in mobile mode

        public CaseFormsPage TapToogleButton(string RecordID)
        {
            ScrollToElement(_toggleIcon(RecordID));
            Tap(_toggleIcon(RecordID));

            return this;
        }

        public CaseFormsPage ValidateFormTypeFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_FormTypeValue(RecordID));
            string elementText = GetElementText(_FormTypeValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseFormsPage ValidateStartDateFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_StartDateValue(RecordID));
            string elementText = GetElementText(_StartDateValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseFormsPage ValidateStartDateFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_StartDateValue(RecordID));
            WaitForElementNotVisible(_StartDateValue(RecordID));

            return this;
        }

        public CaseFormsPage ValidateDueDateFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_DueDateValue(RecordID));
            string elementText = GetElementText(_DueDateValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseFormsPage ValidateStatusFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_StatusValue(RecordID));
            string elementText = GetElementText(_StatusValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseFormsPage ValidateResponsibleTeamFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_ResponsibleTeamValue(RecordID));
            string elementText = GetElementText(_ResponsibleTeamValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseFormsPage ValidateResponsibleTeamFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_ResponsibleTeamValue(RecordID));
            WaitForElementNotVisible(_ResponsibleTeamValue(RecordID));

            return this;
        }



        public CaseFormsPage ValidateCompletedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_CompletedByValue(RecordID));
            string elementText = GetElementText(_CompletedByValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseFormsPage ValidateCompletedByFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_CompletedByValue(RecordID));
            WaitForElementNotVisible(_CompletedByValue(RecordID));

            return this;
        }

        public CaseFormsPage ValidateCompletionDateFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_CompletionDateValue(RecordID));
            string elementText = GetElementText(_CompletionDateValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseFormsPage ValidateCompletionDateFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_CompletionDateValue(RecordID));
            WaitForElementNotVisible(_CompletionDateValue(RecordID));

            return this;
        }

        public CaseFormsPage ValidateSignnedOffByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_SignnedOffByValue(RecordID));
            string elementText = GetElementText(_SignnedOffByValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }



        #endregion

        #region Methods used when displaying the APP in tablet mode

        public CaseFormsPage ValidateFormTypeCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "documentid", ExpectedText));
            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseFormsPage ValidateStartDateCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "startdate", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseFormsPage ValidateDueDateCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "duedate", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseFormsPage ValidateStatusCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "assessmentstatusid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseFormsPage ValidateResponsibleTeamCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "ownerid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseFormsPage ValidateCompletedByCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "completedbyid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseFormsPage ValidateCompletedDateCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "completiondate", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseFormsPage ValidateSignedOffByCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "signedoffbyid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }




        #endregion


        public CaseFormsPage TapOnAddNewRecordButton()
        {
            this.Tap(_addNewRecordButton);

            return this;
        }

        public CaseFormsPage TapOnRecord(string recordText)
        {
            WaitForElement(_recordText(recordText));
            Tap(_recordText(recordText));

            return this;
        }

        

        public CaseFormsPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public CaseFormsPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
