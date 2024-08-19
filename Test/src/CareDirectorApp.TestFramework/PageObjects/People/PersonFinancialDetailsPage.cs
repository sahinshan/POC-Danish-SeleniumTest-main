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
    public class PersonFinancialDetailsPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _addNewRecordButton = e => e.Marked("personfinancialdetail_NewRecordButton");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("PERSON FINANCIAL DETAILS");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");



        #region Header Elements

        readonly Func<AppQuery, AppQuery> _NameHeader = e => e.Marked("personfinancialdetail_HeaderCell_name").Descendant().Marked("NAME");
        readonly Func<AppQuery, AppQuery> _CreatedByHeader = e => e.Marked("personfinancialdetail_HeaderCell_createdby").Descendant().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _CreatedOnHeader = e => e.Marked("personfinancialdetail_HeaderCell_createdon").Descendant().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _ModifiedByHeader = e => e.Marked("personfinancialdetail_HeaderCell_modifiedby").Descendant().Marked("MODIFIED BY");
        readonly Func<AppQuery, AppQuery> _ModifiedOnHeader = e => e.Marked("personfinancialdetail_HeaderCell_modifiedon").Descendant().Marked("MODIFIED ON");


        #endregion

        #region Body Elements

        Func<AppQuery, AppQuery> _NameCell(string recordid) => e => e.Marked("personfinancialdetail_Row_" + recordid + "_Cell_name").Descendant().Class("FormsTextView");
        Func<AppQuery, AppQuery> _CreatedByCell(string recordid) => e => e.Marked("personfinancialdetail_Row_" + recordid + "_Cell_createdby").Descendant().Class("FormsTextView");
        Func<AppQuery, AppQuery> _CreatedOnCell(string recordid) => e => e.Marked("personfinancialdetail_Row_" + recordid + "_Cell_createdon").Descendant().Class("FormsTextView");
        Func<AppQuery, AppQuery> _ModifiedByCell(string recordid) => e => e.Marked("personfinancialdetail_Row_" + recordid + "_Cell_modifiedby").Descendant().Class("FormsTextView");
        Func<AppQuery, AppQuery> _ModifiedOnCell(string recordid) => e => e.Marked("personfinancialdetail_Row_" + recordid + "_Cell_modifiedon").Descendant().Class("FormsTextView");

        #endregion



        public PersonFinancialDetailsPage(IApp app)
        {
            _app = app;
        }


        public PersonFinancialDetailsPage WaitForPersonFinancialDetailsPageToLoad(string ViewText = "Related Records")
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

            WaitForElement(_NameHeader);
            WaitForElement(_CreatedByHeader);
            WaitForElement(_CreatedOnHeader);
            WaitForElement(_ModifiedByHeader);
            WaitForElement(_ModifiedOnHeader);


            return this;
        }

        #region Methods used when displaying the APP in tablet mode

        public PersonFinancialDetailsPage ValidateNameCellText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_NameCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFinancialDetailsPage ValidateCreatedByCellText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_CreatedByCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFinancialDetailsPage ValidateCreatedOnCellText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_CreatedOnCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFinancialDetailsPage ValidateModifiedByText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_ModifiedByCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFinancialDetailsPage ValidateModifiedOnCellText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_ModifiedOnCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        #endregion


        public PersonFinancialDetailsPage TapOnAddNewRecordButton()
        {
            this.Tap(_addNewRecordButton);

            return this;
        }

        public PersonFinancialDetailsPage TapOnRecord(string RecordID)
        {
            WaitForElement(_NameCell(RecordID));
            Tap(_NameCell(RecordID));

            return this;
        }

        public PersonFinancialDetailsPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public PersonFinancialDetailsPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
