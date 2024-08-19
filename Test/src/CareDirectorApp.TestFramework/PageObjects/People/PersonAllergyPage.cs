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
    public class PersonAllergyPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _pageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("PERSON ALLERGIES");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");


        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked(RecordText);
        Func<AppQuery, AppQuery> _toggleIcon(string RecordID) => e => e.Marked("personallergy_Row_" + RecordID).Descendant().Marked("toggleIcon");


        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("personallergy_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");


        #region Mobile mode labels

        Func<AppQuery, AppQuery> _allergytypeLabel(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("allergytypeid_cwname_0_Label");
        Func<AppQuery, AppQuery> _startdateLabel(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("startdate_1_Label");
        Func<AppQuery, AppQuery> _allergicreactionlevelLabel(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("allergicreactionlevelid_cwname_2_Label");
        Func<AppQuery, AppQuery> _createdbyLabel(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_3_Label");
        Func<AppQuery, AppQuery> _createdOnLabel(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("createdon_4_Label");
        Func<AppQuery, AppQuery> _modifiedbyLabel(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_5_Label");
        Func<AppQuery, AppQuery> _modifiedOnLabel(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_6_Label");

        #endregion

        #region Mobile mode field values

        Func<AppQuery, AppQuery> _allergytypeValue(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("allergytypeid_cwname_0_Value");
        Func<AppQuery, AppQuery> _startdateValue(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("startdate_1_Value");
        Func<AppQuery, AppQuery> _allergicreactionlevelValue(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("allergicreactionlevelid_cwname_2_Value");
        Func<AppQuery, AppQuery> _createdbyValue(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_3_Value");
        Func<AppQuery, AppQuery> _createdOnValue(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("createdon_cwname_4_Value");
        Func<AppQuery, AppQuery> _modifiedbyValue(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_5_Value");
        Func<AppQuery, AppQuery> _modifiedOnValue(string RecordID) => e => e.Marked("personallergy_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_6_Value");

        #endregion


        public PersonAllergyPage(IApp app)
        {
            _app = app;
        }


        public PersonAllergyPage WaitForPersonAllergyPageToLoad(string ViewText = "Related Records")
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

        public PersonAllergyPage TapToogleButton(string RecordID)
        {
            ScrollToElement(_toggleIcon(RecordID));
            Tap(_toggleIcon(RecordID));

            return this;
        }

        public PersonAllergyPage ValidateAllergyTypeFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_allergytypeValue(RecordID));
            string elementText = GetElementText(_allergytypeValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonAllergyPage ValidateStartDateFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_startdateValue(RecordID));
            string elementText = GetElementText(_startdateValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonAllergyPage ValidateStartDateFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_startdateValue(RecordID));
            WaitForElementNotVisible(_startdateValue(RecordID));

            return this;
        }

        public PersonAllergyPage ValidateLevelFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_allergicreactionlevelValue(RecordID));
            string elementText = GetElementText(_allergicreactionlevelValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonAllergyPage ValidateLevelFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_allergicreactionlevelValue(RecordID));
            WaitForElementNotVisible(_allergicreactionlevelValue(RecordID));

            return this;
        }

        public PersonAllergyPage ValidateCreatedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdbyValue(RecordID));
            string elementText = GetElementText(_createdbyValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonAllergyPage ValidateCreatedByFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_createdbyValue(RecordID));
            WaitForElementNotVisible(_createdbyValue(RecordID));

            return this;
        }

        public PersonAllergyPage ValidateCreatedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdOnValue(RecordID));
            string elementText = GetElementText(_createdOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonAllergyPage ValidateCreatedOnFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_createdOnValue(RecordID));
            WaitForElementNotVisible(_createdOnValue(RecordID));

            return this;
        }

        public PersonAllergyPage ValidateModifiedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedbyValue(RecordID));
            string elementText = GetElementText(_modifiedbyValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonAllergyPage ValidateModifiedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedOnValue(RecordID));
            string elementText = GetElementText(_modifiedOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        #endregion

        #region Methods used when displaying the APP in tablet mode

        public PersonAllergyPage ValidateAllergyTypeCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "allergytypeid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAllergyPage ValidateStartDateCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "startdate", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAllergyPage ValidateLevelCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "allergicreactionlevelid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAllergyPage ValidateCreatedByCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdby", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAllergyPage ValidateCreatedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdon", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAllergyPage ValidateModifiedByText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedby", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAllergyPage ValidateModifiedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedon", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }


        #endregion




        public PersonAllergyPage TapOnRecord(string recordText)
        {
            WaitForElement(_recordText(recordText));
            Tap(_recordText(recordText));

            return this;
        }

        public PersonAllergyPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public PersonAllergyPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
