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
    public class PersonRelationshipsPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _pageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("PERSON RELATIONSHIP");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");


        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked(RecordText);
        Func<AppQuery, AppQuery> _toggleIcon(string RecordID) => e => e.Marked("personrelationship_Row_" + RecordID).Descendant().Marked("toggleIcon");


        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("personrelationship_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");


        #region Mobile mode labels

        Func<AppQuery, AppQuery> _relatedPersonLabel(string RecordID) => e => e.Marked("personrelationship_PrimaryRow_" + RecordID).Descendant().Marked("relatedpersonidid_cwname_0_Label");
        Func<AppQuery, AppQuery> _relationshipLabel(string RecordID) => e => e.Marked("personrelationship_PrimaryRow_" + RecordID).Descendant().Marked("personrelationshiptypeid_cwname_1_Label");
        Func<AppQuery, AppQuery> _createdbyLabel(string RecordID) => e => e.Marked("personrelationship_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_3_Label");
        Func<AppQuery, AppQuery> _createdOnLabel(string RecordID) => e => e.Marked("personrelationship_PrimaryRow_" + RecordID).Descendant().Marked("createdon_4_Label");
        Func<AppQuery, AppQuery> _modifiedbyLabel(string RecordID) => e => e.Marked("personrelationship_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_5_Label");
        Func<AppQuery, AppQuery> _modifiedOnLabel(string RecordID) => e => e.Marked("personrelationship_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_6_Label");

        #endregion

        #region Mobile mode field values

        Func<AppQuery, AppQuery> _relatedPersonValue(string RecordID) => e => e.Marked("personrelationship_PrimaryRow_" + RecordID).Descendant().Marked("relatedpersonidid_cwname_0_Value");
        Func<AppQuery, AppQuery> _relationshipValue(string RecordID) => e => e.Marked("personrelationship_PrimaryRow_" + RecordID).Descendant().Marked("personrelationshiptypeid_cwname_1_Value");
        Func<AppQuery, AppQuery> _createdbyValue(string RecordID) => e => e.Marked("personrelationship_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_3_Value");
        Func<AppQuery, AppQuery> _createdOnValue(string RecordID) => e => e.Marked("personrelationship_PrimaryRow_" + RecordID).Descendant().Marked("createdon_cwname_4_Value");
        Func<AppQuery, AppQuery> _modifiedbyValue(string RecordID) => e => e.Marked("personrelationship_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_5_Value");
        Func<AppQuery, AppQuery> _modifiedOnValue(string RecordID) => e => e.Marked("personrelationship_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_6_Value");

        #endregion


        public PersonRelationshipsPage(IApp app)
        {
            _app = app;
        }


        public PersonRelationshipsPage WaitForPersonRelationshipsPageToLoad(string ViewText = "Relationships")
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

        public PersonRelationshipsPage TapToogleButton(string RecordID)
        {
            ScrollToElement(_toggleIcon(RecordID));
            Tap(_toggleIcon(RecordID));

            return this;
        }

        public PersonRelationshipsPage ValidateRelatedPersonFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_relatedPersonValue(RecordID));
            string elementText = GetElementText(_relatedPersonValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonRelationshipsPage ValidateRelationshipFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_relationshipValue(RecordID));
            string elementText = GetElementText(_relationshipValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonRelationshipsPage ValidateRelationshipFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_relationshipValue(RecordID));
            WaitForElementNotVisible(_relationshipValue(RecordID));

            return this;
        }



        public PersonRelationshipsPage ValidateCreatedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdbyValue(RecordID));
            string elementText = GetElementText(_createdbyValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonRelationshipsPage ValidateCreatedByFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_createdbyValue(RecordID));
            WaitForElementNotVisible(_createdbyValue(RecordID));

            return this;
        }

        public PersonRelationshipsPage ValidateCreatedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdOnValue(RecordID));
            string elementText = GetElementText(_createdOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonRelationshipsPage ValidateCreatedOnFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_createdOnValue(RecordID));
            WaitForElementNotVisible(_createdOnValue(RecordID));

            return this;
        }

        public PersonRelationshipsPage ValidateModifiedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedbyValue(RecordID));
            string elementText = GetElementText(_modifiedbyValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonRelationshipsPage ValidateModifiedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedOnValue(RecordID));
            string elementText = GetElementText(_modifiedOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        #endregion

        #region Methods used when displaying the APP in tablet mode

        public PersonRelationshipsPage ValidateRelatedPersonCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "relatedpersonid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonRelationshipsPage ValidateRelationshipCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "personrelationshiptypeid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }


        public PersonRelationshipsPage ValidateCreatedByCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdby", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonRelationshipsPage ValidateCreatedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdon", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonRelationshipsPage ValidateModifiedByText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedby", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonRelationshipsPage ValidateModifiedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedon", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }


        #endregion




        public PersonRelationshipsPage TapOnRecord(string recordText)
        {
            WaitForElement(_recordText(recordText));
            Tap(_recordText(recordText));

            return this;
        }

        public PersonRelationshipsPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public PersonRelationshipsPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
