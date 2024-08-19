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
    public class PersonFinancialDetailAttachmentsPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _addNewRecordButton = e => e.Marked("personfinancialdetailattachment_NewRecordButton");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("ATTACHMENTS (FOR PERSON FINANCIAL DETAIL)");

        readonly Func<AppQuery, AppQuery> _personBanner = e => e.Marked("BannerStackLayout");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");



        #region Header Elements

        readonly Func<AppQuery, AppQuery> _TitleHeader = e => e.Marked("personfinancialdetailattachment_HeaderCell_title").Descendant().Marked("TITLE");
        readonly Func<AppQuery, AppQuery> _DateHeader = e => e.Marked("personfinancialdetailattachment_HeaderCell_date").Descendant().Marked("DATE");
        readonly Func<AppQuery, AppQuery> _DocumentTypeHeader = e => e.Marked("personfinancialdetailattachment_HeaderCell_documenttypeid").Descendant().Marked("DOCUMENT TYPE");
        readonly Func<AppQuery, AppQuery> _DocumentSubTypeHeader = e => e.Marked("personfinancialdetailattachment_HeaderCell_documentsubtypeid").Descendant().Marked("DOCUMENT SUB TYPE");
        readonly Func<AppQuery, AppQuery> _CreatedByHeader = e => e.Marked("personfinancialdetailattachment_HeaderCell_createdby").Descendant().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _CreatedOnHeader = e => e.Marked("personfinancialdetailattachment_HeaderCell_createdon").Descendant().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _ModifiedByHeader = e => e.Marked("personfinancialdetailattachment_HeaderCell_modifiedby").Descendant().Marked("MODIFIED BY");
        readonly Func<AppQuery, AppQuery> _ModifiedOnHeader = e => e.Marked("personfinancialdetailattachment_HeaderCell_modifiedon").Descendant().Marked("MODIFIED ON");

        #endregion

        #region Body Elements

        Func<AppQuery, AppQuery> _TitleCell(string recordid) => e => e.Marked("personfinancialdetailattachment_Row_" + recordid + "_Cell_title").Descendant().Class("FormsTextView");
        Func<AppQuery, AppQuery> _DateCell(string recordid) => e => e.Marked("personfinancialdetailattachment_Row_" + recordid + "_Cell_date").Descendant().Class("FormsTextView");
        Func<AppQuery, AppQuery> _DocumentTypeCell(string recordid) => e => e.Marked("personfinancialdetailattachment_Row_" + recordid + "_Cell_documenttypeid").Descendant().Class("FormsTextView");
        Func<AppQuery, AppQuery> _DocumentSubTypeCell(string recordid) => e => e.Marked("personfinancialdetailattachment_Row_" + recordid + "_Cell_documentsubtypeid").Descendant().Class("FormsTextView");
        Func<AppQuery, AppQuery> _CreatedByCell(string recordid) => e => e.Marked("personfinancialdetailattachment_Row_" + recordid + "_Cell_createdby").Descendant().Class("FormsTextView");
        Func<AppQuery, AppQuery> _CreatedOnCell(string recordid) => e => e.Marked("personfinancialdetailattachment_Row_" + recordid + "_Cell_createdon").Descendant().Class("FormsTextView");
        Func<AppQuery, AppQuery> _ModifiedByCell(string recordid) => e => e.Marked("personfinancialdetailattachment_Row_" + recordid + "_Cell_modifiedby").Descendant().Class("FormsTextView");
        Func<AppQuery, AppQuery> _ModifiedOnCell(string recordid) => e => e.Marked("personfinancialdetailattachment_Row_" + recordid + "_Cell_modifiedon").Descendant().Class("FormsTextView");

        #endregion



        public PersonFinancialDetailAttachmentsPage(IApp app)
        {
            _app = app;
        }


        public PersonFinancialDetailAttachmentsPage WaitForPersonFinancialDetailAttachmentsPageToLoad(string ViewText = "Related Records")
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_addNewRecordButton);
            WaitForElement(_peoplePageIconButton);
            WaitForElement(_pageTitle);
            
            WaitForElement(_personBanner);

            WaitForElement(_viewPicker(ViewText));

            WaitForElement(_searchTextBox);
            WaitForElement(_searchButton);
            WaitForElement(_refreshButton);

            WaitForElement(_TitleHeader);
            WaitForElement(_DateHeader);
            WaitForElement(_DocumentTypeHeader);
            WaitForElement(_DocumentSubTypeHeader);
            WaitForElement(_CreatedByHeader);


            return this;
        }

        #region Methods used when displaying the APP in tablet mode

        public PersonFinancialDetailAttachmentsPage ValidateTitleCellText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_TitleCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFinancialDetailAttachmentsPage ValidateDateCellText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_DateCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFinancialDetailAttachmentsPage ValidateDocumentTypeCellText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_DocumentTypeCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFinancialDetailAttachmentsPage ValidateDocumentSubTypeCellText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_DocumentSubTypeCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFinancialDetailAttachmentsPage ValidateCreatedByCellText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_CreatedByCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFinancialDetailAttachmentsPage ValidateCreatedOnCellText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_CreatedOnCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFinancialDetailAttachmentsPage ValidateModifiedByText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_ModifiedByCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public PersonFinancialDetailAttachmentsPage ValidateModifiedOnCellText(string ExpectedText, string RecordID)
        {
            string elementText = GetElementText(_ModifiedOnCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        #endregion


        public PersonFinancialDetailAttachmentsPage TapOnAddNewRecordButton()
        {
            this.Tap(_addNewRecordButton);

            return this;
        }

        public PersonFinancialDetailAttachmentsPage TapOnRecord(string RecordID)
        {
            WaitForElement(_TitleCell(RecordID));
            Tap(_TitleCell(RecordID));

            return this;
        }

        public PersonFinancialDetailAttachmentsPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public PersonFinancialDetailAttachmentsPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
