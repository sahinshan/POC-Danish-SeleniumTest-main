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
    public class PersonFormsPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _addNewRecordButton = e => e.Marked("personform_NewRecordButton");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("FORMS (PERSON)");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");


        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked(RecordText);
        Func<AppQuery, AppQuery> _toggleIcon(string RecordID) => e => e.Marked("personform_Row_" + RecordID).Descendant().Marked("toggleIcon");


        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("personform_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");


        #region Mobile mode labels

        Func<AppQuery, AppQuery> _FormTypeLabel(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("documentid_cwname_0_Label");
        Func<AppQuery, AppQuery> _ResponsibleUserLabel(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("responsibleuserid_cwname_1_Label");
        Func<AppQuery, AppQuery> _StartDateLabel(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("startdate_2_Label");
        Func<AppQuery, AppQuery> _CreatedByLabel(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_3_Label");
        Func<AppQuery, AppQuery> _CreatedOnLabel(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("createdon_cwname_3_Label");
        Func<AppQuery, AppQuery> _modifiedbyLabel(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_4_Label");
        Func<AppQuery, AppQuery> _modifiedOnLabel(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_5_Label");

        #endregion

        #region Mobile mode field values

        Func<AppQuery, AppQuery> _FormTypeValue(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("documentid_cwname_0_Value");
        Func<AppQuery, AppQuery> _ResponsibleUserValue(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("responsibleuserid_cwname_1_Value");
        Func<AppQuery, AppQuery> _StartDateValue(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("startdate_2_Value");
        Func<AppQuery, AppQuery> _CreatedByValue(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("createdby_cwname_3_Value");
        Func<AppQuery, AppQuery> _CreatedOnValue(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("createdon_cwname_3_Value");
        Func<AppQuery, AppQuery> _modifiedbyValue(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("modifiedby_cwname_4_Value");
        Func<AppQuery, AppQuery> _modifiedOnValue(string RecordID) => e => e.Marked("personform_PrimaryRow_" + RecordID).Descendant().Marked("modifiedon_5_Value");

        #endregion


        public PersonFormsPage(IApp app)
        {
            _app = app;
        }


        public PersonFormsPage WaitForPersonFormsPageToLoad(string ViewText = "Active Forms")
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

        public PersonFormsPage TapToogleButton(string RecordID)
        {
            ScrollToElement(_toggleIcon(RecordID));
            Tap(_toggleIcon(RecordID));

            return this;
        }
            
        public PersonFormsPage ValidateFormTypeFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_FormTypeValue(RecordID));
            string elementText = GetElementText(_FormTypeValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFormsPage ValidateResponsibleUserFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_ResponsibleUserValue(RecordID));
            string elementText = GetElementText(_ResponsibleUserValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFormsPage ValidateResponsibleUserFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_ResponsibleUserValue(RecordID));
            WaitForElementNotVisible(_ResponsibleUserValue(RecordID));

            return this;
        }

        public PersonFormsPage ValidateStartDateFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_StartDateValue(RecordID));
            string elementText = GetElementText(_StartDateValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFormsPage ValidateStartDateFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_StartDateValue(RecordID));
            WaitForElementNotVisible(_StartDateValue(RecordID));

            return this;
        }

        public PersonFormsPage ValidateCreatedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_CreatedByValue(RecordID));
            string elementText = GetElementText(_CreatedByValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFormsPage ValidateCreatedByFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_CreatedByValue(RecordID));
            WaitForElementNotVisible(_CreatedByValue(RecordID));

            return this;
        }

        public PersonFormsPage ValidateCreatedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_CreatedOnValue(RecordID));
            string elementText = GetElementText(_CreatedOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFormsPage ValidateCreatedOnFieldNotVisible(string RecordID)
        {
            TryScrollToElement(_CreatedOnValue(RecordID));
            WaitForElementNotVisible(_CreatedOnValue(RecordID));

            return this;
        }

        public PersonFormsPage ValidateModifiedByFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedbyValue(RecordID));
            string elementText = GetElementText(_modifiedbyValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFormsPage ValidateModifiedOnFieldValue(string ExpectedText, string RecordID)
        {
            ScrollToElement(_modifiedOnValue(RecordID));
            string elementText = GetElementText(_modifiedOnValue(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        #endregion

        #region Methods used when displaying the APP in tablet mode

        public PersonFormsPage ValidateFormTypeCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "documentid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonFormsPage ValidateResponsibleUserCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "responsibleuserid", ExpectedText));

            if(string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonFormsPage ValidateStartDateCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "startdate", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonFormsPage ValidateCreatedByCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdby", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonFormsPage ValidateCreatedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdon", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonFormsPage ValidateModifiedByText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedby", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonFormsPage ValidateModifiedOnCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedon", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }


        #endregion


        public PersonFormsPage TapOnAddNewRecordButton()
        {
            this.Tap(_addNewRecordButton);

            return this;
        }

        public PersonFormsPage TapOnRecord(string recordText)
        {
            WaitForElement(_recordText(recordText));
            Tap(_recordText(recordText));

            return this;
        }

        public PersonFormsPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public PersonFormsPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
