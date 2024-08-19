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
    public class PersonDisabilityImpairmentsPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _pageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("DISABILITIES/IMPAIRMENTS");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");


        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked(RecordText);
        Func<AppQuery, AppQuery> _toggleIcon(string RecordID) => e => e.Marked("persondisabilityimpairments_Row_" + RecordID).Descendant().Marked("toggleIcon");


        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("persondisabilityimpairments_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");


        #region Mobile mode labels

        Func<AppQuery, AppQuery> _disabilitytypeLabel(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("disabilitytypeid_cwname_0_Label");
        Func<AppQuery, AppQuery> _impairmenttypeidLabel(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("startdate_1_Label");
        Func<AppQuery, AppQuery> _disabilityseverityidLabel(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("disabilityseverityid_cwname_2_Label");
        Func<AppQuery, AppQuery> _startdateLabel(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("startdate_3_Label");
        Func<AppQuery, AppQuery> _createdbyLabel(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_4_Label");
        Func<AppQuery, AppQuery> _createdOnLabel(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("createdon_5_Label");
        Func<AppQuery, AppQuery> _modifiedbyLabel(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_6_Label");
        Func<AppQuery, AppQuery> _modifiedOnLabel(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_7_Label");

        #endregion

        #region Mobile mode field values

        Func<AppQuery, AppQuery> _disabilitytypeValue(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("disabilitytypeid_cwname_0_Value");
        Func<AppQuery, AppQuery> _impairmenttypeidValue(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("startdate_1_Value");
        Func<AppQuery, AppQuery> _disabilityseverityidValue(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("disabilityseverityid_cwname_2_Value");
        Func<AppQuery, AppQuery> _startdateValue(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("startdate_3_Value");
        Func<AppQuery, AppQuery> _createdbyValue(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_4_Value");
        Func<AppQuery, AppQuery> _createdOnValue(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("createdon_cwname_5_Value");
        Func<AppQuery, AppQuery> _modifiedbyValue(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_6_Value");
        Func<AppQuery, AppQuery> _modifiedOnValue(string RecordID) => e => e.Marked("persondisabilityimpairments_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_7_Value");

        #endregion


        public PersonDisabilityImpairmentsPage(IApp app)
        {
            _app = app;
        }


        public PersonDisabilityImpairmentsPage WaitForPersonDisabilityImpairmentsPageToLoad(string ViewText = "Related Records")
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_pageIconButton);
            WaitForElement(_pageTitle);

            WaitForElement(_viewPicker(ViewText));

            WaitForElement(_searchTextBox);
            WaitForElement(_searchButton);
            WaitForElement(_refreshButton);

            return this;
        }


        #region Methods used when displaying the APP in mobile mode

        public PersonDisabilityImpairmentsPage TapToogleButton(string RecordID)
        {
            ScrollToElement(_toggleIcon(RecordID));
            Tap(_toggleIcon(RecordID));

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateDisabilityFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_disabilitytypeValue(RecordID));
            string elementText = GetElementText(_disabilitytypeValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateImpairmentFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_impairmenttypeidValue(RecordID));
            string elementText = GetElementText(_impairmenttypeidValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateImpairmentFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_impairmenttypeidValue(RecordID));
            WaitForElementNotVisible(_impairmenttypeidValue(RecordID));

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateSeverityFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_disabilityseverityidValue(RecordID));
            string elementText = GetElementText(_disabilityseverityidValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateSeverityFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_disabilityseverityidValue(RecordID));
            WaitForElementNotVisible(_disabilityseverityidValue(RecordID));

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateStartDateFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_startdateValue(RecordID));
            string elementText = GetElementText(_startdateValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateStartDateFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_startdateValue(RecordID));
            WaitForElementNotVisible(_startdateValue(RecordID));

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateCreatedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdbyValue(RecordID));
            string elementText = GetElementText(_createdbyValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateCreatedByFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_createdbyValue(RecordID));
            WaitForElementNotVisible(_createdbyValue(RecordID));

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateCreatedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdOnValue(RecordID));
            string elementText = GetElementText(_createdOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateCreatedOnFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_createdOnValue(RecordID));
            WaitForElementNotVisible(_createdOnValue(RecordID));

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateModifiedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedbyValue(RecordID));
            string elementText = GetElementText(_modifiedbyValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateModifiedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedOnValue(RecordID));
            string elementText = GetElementText(_modifiedOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        #endregion

        #region Methods used when displaying the APP in tablet mode

        public PersonDisabilityImpairmentsPage ValidateDisabilityCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "disabilitytypeid", ExpectedText));
            
            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateImpairmentCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "impairmenttypeid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateSeverityCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "disabilityseverityid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;

        }

        public PersonDisabilityImpairmentsPage ValidateStartDateCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "startdate", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;

        }

        public PersonDisabilityImpairmentsPage ValidateCreatedByCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdby", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateCreatedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdon", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateModifiedByText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedby", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateModifiedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedon", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }


        #endregion




        public PersonDisabilityImpairmentsPage TapOnRecord(string recordText)
        {
            WaitForElement(_recordText(recordText));
            Tap(_recordText(recordText));

            return this;
        }

        public PersonDisabilityImpairmentsPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public PersonDisabilityImpairmentsPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
