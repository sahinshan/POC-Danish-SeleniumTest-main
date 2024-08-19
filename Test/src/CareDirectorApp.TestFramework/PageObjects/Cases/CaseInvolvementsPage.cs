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
    public class CaseInvolvementsPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _casesPageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("CASE INVOLVEMENTS");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");


        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked(RecordText);
        Func<AppQuery, AppQuery> _toggleIcon(string CaseNoteRecordID) => e => e.Marked("caseinvolvement_Row_" + CaseNoteRecordID).Descendant().Marked("toggleIcon");


        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("caseinvolvement_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");


        #region Mobile mode labels

        Func<AppQuery, AppQuery> _involvementmemberidLabel(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("involvementmemberid_cwname_0_Label");
        Func<AppQuery, AppQuery> _involvementroleidLabel(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("involvementroleid_cwname_1_Label");
        Func<AppQuery, AppQuery> _owneridLabel(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("ownerid_cwname_2_Label");
        Func<AppQuery, AppQuery> _startdateLabel(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("startdate_3_Label");
        Func<AppQuery, AppQuery> _enddateLabel(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("enddate_4_Label");
        Func<AppQuery, AppQuery> _createdbyLabel(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_5_Label");
        Func<AppQuery, AppQuery> _createdOnLabel(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("createdon_6_Label");
        Func<AppQuery, AppQuery> _modifiedByLabel(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_7_Label");
        Func<AppQuery, AppQuery> _modifiedOnLabel(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_cwname_8_Label");

        #endregion

        #region Mobile mode field values

        Func<AppQuery, AppQuery> _involvementmemberidValue(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("involvementmemberid_cwname_0_Value");
        Func<AppQuery, AppQuery> _involvementroleidValue(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("involvementroleid_cwname_1_Value");
        Func<AppQuery, AppQuery> _owneridValue(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("ownerid_cwname_2_Value");
        Func<AppQuery, AppQuery> _startdateValue(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("startdate_3_Value");
        Func<AppQuery, AppQuery> _enddateValue(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("enddate_4_Value");
        Func<AppQuery, AppQuery> _createdbyValue(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_5_Value");
        Func<AppQuery, AppQuery> _createdOnValue(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("createdon_6_Value");
        Func<AppQuery, AppQuery> _modifiedByValue(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_7_Value");
        Func<AppQuery, AppQuery> _modifiedOnValue(string RecordID) => e => e.Marked("caseinvolvement_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_cwname_8_Value");

        #endregion


        public CaseInvolvementsPage(IApp app)
        {
            _app = app;
        }


        public CaseInvolvementsPage WaitForCaseInvolvementsPageToLoad(string ViewText = "Related Records")
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_casesPageIconButton);
            WaitForElement(_pageTitle);

            WaitForElement(_viewPicker(ViewText));

            WaitForElement(_searchTextBox);
            WaitForElement(_searchButton);
            WaitForElement(_refreshButton);

            return this;
        }


        #region Methods used when displaying the APP in mobile mode

        public CaseInvolvementsPage TapToogleButton(string CaseNoteRecordID)
        {
            ScrollToElement(_toggleIcon(CaseNoteRecordID));
            Tap(_toggleIcon(CaseNoteRecordID));

            return this;
        }

        public CaseInvolvementsPage ValidateInvolvementMemberFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_involvementmemberidValue(RecordID));
            string elementText = GetElementText(_involvementmemberidValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseInvolvementsPage ValidateRoleFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_involvementroleidValue(RecordID));
            string elementText = GetElementText(_involvementroleidValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseInvolvementsPage ValidateRoleFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_involvementroleidValue(RecordID));
            WaitForElementNotVisible(_involvementroleidValue(RecordID));

            return this;
        }

        public CaseInvolvementsPage ValidateResponsibleTeamFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_owneridValue(RecordID));
            string elementText = GetElementText(_owneridValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseInvolvementsPage ValidateResponsibleTeamFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_owneridValue(RecordID));
            WaitForElementNotVisible(_owneridValue(RecordID));

            return this;
        }

        public CaseInvolvementsPage ValidateStartDateFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_startdateValue(RecordID));
            string elementText = GetElementText(_startdateValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseInvolvementsPage ValidateStartDateFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_startdateValue(RecordID));
            WaitForElementNotVisible(_startdateValue(RecordID));

            return this;
        }

        public CaseInvolvementsPage ValidateEndDateFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_enddateValue(RecordID));
            string elementText = GetElementText(_enddateValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseInvolvementsPage ValidateEndDateFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_enddateValue(RecordID));
            WaitForElementNotVisible(_enddateValue(RecordID));

            return this;
        }

        public CaseInvolvementsPage ValidateCreatedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdbyValue(RecordID));
            string elementText = GetElementText(_createdbyValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseInvolvementsPage ValidateCreatedByFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_createdbyValue(RecordID));
            WaitForElementNotVisible(_createdbyValue(RecordID));

            return this;
        }

        public CaseInvolvementsPage ValidateCreatedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdOnValue(RecordID));
            string elementText = GetElementText(_createdOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseInvolvementsPage ValidateCreatedOnFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_createdOnValue(RecordID));
            WaitForElementNotVisible(_createdOnValue(RecordID));

            return this;
        }

        public CaseInvolvementsPage ValidateModifiedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedByValue(RecordID));
            string elementText = GetElementText(_modifiedByValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CaseInvolvementsPage ValidateModifiedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedOnValue(RecordID));
            string elementText = GetElementText(_modifiedOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        #endregion

        #region Methods used when displaying the APP in tablet mode

        public CaseInvolvementsPage ValidateResponsibleMemberCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "involvementmemberid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseInvolvementsPage ValidatRoleCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "involvementroleid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseInvolvementsPage ValidateResponsibleTeamByText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "ownerid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseInvolvementsPage ValidateStartDateByText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "startdate", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseInvolvementsPage ValidatEndDateByText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "enddate", ExpectedText));
            
            if(string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);


            return this;
        }

        public CaseInvolvementsPage ValidateCreatedByCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdby", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseInvolvementsPage ValidateCreatedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdon", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseInvolvementsPage ValidateModifiedByCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedby", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseInvolvementsPage ValidateModifiedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedon", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }


        #endregion



        public CaseInvolvementsPage TapOnRecord(string recordText)
        {
            WaitForElement(_recordText(recordText));
            Tap(_recordText(recordText));

            return this;
        }

        public CaseInvolvementsPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public CaseInvolvementsPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
