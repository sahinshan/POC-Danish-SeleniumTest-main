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
    public class PersonBodyMapsPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _addNewRecordButton = e => e.Marked("personbodymap_NewRecordButton");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("PERSON BODY MAPS");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");


        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked(RecordText);
        Func<AppQuery, AppQuery> _toggleIcon(string BodyMapRecordID) => e => e.Marked("personbodymap_Row_" + BodyMapRecordID).Descendant().Marked("toggleIcon");


        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("personbodymap_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");


        #region Mobile mode labels

        Func<AppQuery, AppQuery> _dateOfEventLabel(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID).Descendant().Marked("dateofevent_0_Label");
        Func<AppQuery, AppQuery> _viewTypeLabel(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID).Descendant().Marked("viewtypeid_cwname_1_Label");
        Func<AppQuery, AppQuery> _isReviewRequiredLabel(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID).Descendant().Marked("isreviewrequiredid_cwname_2_Label");
        Func<AppQuery, AppQuery> _reviewDateLabel(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID).Descendant().Marked("reviewdate_3_Label");
        Func<AppQuery, AppQuery> _modifiedOnLabel(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_4_Label");
        Func<AppQuery, AppQuery> _modifiedByLabel(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_5_Label");
        Func<AppQuery, AppQuery> _createdByLabel(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_6_Label");
        Func<AppQuery, AppQuery> _createdOnLabel(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID).Descendant().Marked("createdon_7_Label");

        #endregion

        #region Mobile mode field values

     

        Func<AppQuery, AppQuery> _dateOfEventValue(string RecordID) => e => e.Marked("personbodymap_Row_" + RecordID + "_Cell_dateofevent").Descendant().Class("LabelAppCompatRenderer");
        Func<AppQuery, AppQuery> _viewTypeValue(string RecordID) => e => e.Marked("personbodymap_Row_" + RecordID+"_Cell_viewtypeid");
        Func<AppQuery, AppQuery> _isReviewRequiredValue(string RecordID) => e => e.Marked("personbodymap_Row_" + RecordID+ "_Cell_isreviewrequiredid");
        Func<AppQuery, AppQuery> _reviewDateValue(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID+ "_Cell_reviewdate");
        Func<AppQuery, AppQuery> _modifiedOnValue(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_4_Value");
        Func<AppQuery, AppQuery> _modifiedByValue(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_5_Value");
        Func<AppQuery, AppQuery> _createdByValue(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_6_Value");
        Func<AppQuery, AppQuery> _createdOnValue(string RecordID) => e => e.Marked("personbodymap_PrimaryRow_" + RecordID).Descendant().Marked("createdon_7_Value");

        #endregion


        public PersonBodyMapsPage(IApp app)
        {
            _app = app;
        }


        public PersonBodyMapsPage WaitForPersonBodyMapsPageToLoad(string ViewText = "Body Maps")
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

        public PersonBodyMapsPage TapOnRecord(string recordText)
        {
            WaitForElement(_recordText(recordText));
            Tap(_recordText(recordText));

            return this;
        }

        public PersonBodyMapsPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }






        #region Methods used when displaying the APP in mobile mode

        public PersonBodyMapsPage TapToogleButton(string BodyMapRecordID)
        {
            ScrollToElement(_dateOfEventValue(BodyMapRecordID));
            Tap(_dateOfEventValue(BodyMapRecordID));

            return this;
        }
        public PersonBodyMapsPage ValidateDateOfEventFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_dateOfEventValue(RecordID));
            string elementText = GetElementText(_dateOfEventValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonBodyMapsPage ValidateViewTypeFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_viewTypeValue(RecordID));
            string elementText = GetElementText(_viewTypeValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonBodyMapsPage ValidateViewTypeFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_viewTypeValue(RecordID));
            WaitForElementNotVisible(_viewTypeValue(RecordID));

            return this;
        }

        public PersonBodyMapsPage ValidateIsReviewRequiredFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_isReviewRequiredValue(RecordID));
            string elementText = GetElementText(_isReviewRequiredValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonBodyMapsPage ValidateIsReviewRequiredFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_isReviewRequiredValue(RecordID));
            WaitForElementNotVisible(_isReviewRequiredValue(RecordID));

            return this;
        }

        public PersonBodyMapsPage ValidateReviewDateFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_reviewDateValue(RecordID));
            string elementText = GetElementText(_reviewDateValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonBodyMapsPage ValidateReviewDateFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_reviewDateValue(RecordID));
            WaitForElementNotVisible(_reviewDateValue(RecordID));

            return this;
        }

        public PersonBodyMapsPage ValidateModifiedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedOnValue(RecordID));
            string elementText = GetElementText(_modifiedOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonBodyMapsPage ValidateModifiedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedByValue(RecordID));
            string elementText = GetElementText(_modifiedByValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonBodyMapsPage ValidateCreatedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdByValue(RecordID));
            string elementText = GetElementText(_createdByValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonBodyMapsPage ValidateCreatedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_createdOnValue(RecordID));
            string elementText = GetElementText(_createdOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        #endregion

        #region Methods used when displaying the APP in tablet mode

        public PersonBodyMapsPage ValidateDateOfEventCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "dateofevent", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonBodyMapsPage ValidateViewTypeCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "viewtypeid", ExpectedText));

            if(string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonBodyMapsPage ValidateIsReviewRequiredCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "isreviewrequiredid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonBodyMapsPage ValidateReviewDateCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "reviewdate", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonBodyMapsPage ValidateModifiedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedon", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonBodyMapsPage ValidateModifiedByCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedby", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonBodyMapsPage ValidateCreatedByCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdby", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonBodyMapsPage ValidateCreatedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdon", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        #endregion


        public PersonBodyMapsPage TapOnAddNewRecordButton()
        {
            this.Tap(_addNewRecordButton);

            return this;
        }
    }
}
