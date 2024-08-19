//Create a CPContractServicesPage page object class
//This class contains all the elements and methods for the CPContractServicesPage page.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
    public class CPContractServicesPage : CommonMethods
    {
        public CPContractServicesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By PageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By NewRecordButton = By.XPath("//*[@id='TI_NewRecordButton']");
        readonly By ExportToExcelButton = By.XPath("//*[@id='TI_ExportToExcelButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By ToolBarMenu = By.Id("CWToolbarMenu");
        readonly By viewSelector = By.XPath("//*[@id='CWViewSelector']");
        readonly By searchTextBox = By.XPath("//*[@id='CWQuickSearch']");
        readonly By searchButton = By.XPath("//*[@id='CWQuickSearchButton']");
        readonly By RefreshButton = By.XPath("//*[@id='CWRefreshButton']");

        By RecordCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By RecordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        By RecordIdentifier(int RecordPosition, string RecordID) => By.XPath("//tr[" + RecordPosition + "][@id='" + RecordID + "']/td[2]");

        By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
        By gridPageHeaderSortIcon(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2]");
        By gridPageHeaderElement(string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a/span[text() = '" + ColumnName + "']");

        By gridPageHeaderElementSortOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2]");
        By gridPageHeaderElementSortOrder(int HeaderCellPosition, string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[text() = '" + ColumnName + "']");


        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2][@class = 'sortasc']");
        By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[2][@class = 'sortdesc']");
        By gridPageHeaderElementSortAscendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortasc']");
        By gridPageHeaderElementSortDescendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        #region Advanced Search Filter Panel

        readonly By DoNotUseViewFilterCheckbox = By.Id("CWIgnoreViewFilter");
        readonly By EstablishmentLookupButton = By.Id("CWLookupBtn_careprovidercontractservice_establishmentproviderid");
        readonly By ContractSchemeLookupButton = By.Id("CWLookupBtn_careprovidercontractservice_careprovidercontractschemeid");
        readonly By FunderLookupButton = By.Id("CWLookupBtn_careprovidercontractservice_funderproviderid");
        readonly By SearchButton = By.Id("CWSearchButton");
        readonly By ClearFiltersButton = By.Id("CWClearFiltersButton");

        #endregion



        public CPContractServicesPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(PageHeader);

            return this;
        }

        public CPContractServicesPage ValidateToolbarOptionsDisplayed()
        {

            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }

        public CPContractServicesPage ClickAdditionalItemsButton()
        {
            WaitForElementToBeClickable(AdditionalItemsButton);
            Click(AdditionalItemsButton);

            return this;
        }

        public CPContractServicesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public CPContractServicesPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public CPContractServicesPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public CPContractServicesPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            ScrollToElement(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public CPContractServicesPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            ScrollToElement(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public CPContractServicesPage ValidateSystemViewOptionNotPresent(string optionNotPresent)
        {
            ScrollToElement(viewSelector);
            ValidatePicklistDoesNotContainsElementByText(viewSelector, optionNotPresent);
            return this;
        }

        public CPContractServicesPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            ScrollToElement(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public CPContractServicesPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            ScrollToElement(searchButton);
            Click(searchButton);

            return this;
        }

        public CPContractServicesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            ScrollToElement(RefreshButton);
            Click(RefreshButton);

            return this;
        }

        public CPContractServicesPage ValidateRecordVisible(Guid RecordID)
        {
            WaitForElementVisible(RecordCheckBox(RecordID.ToString()));

            return this;
        }

        public CPContractServicesPage ValidateRecordVisible(int RecordPosition, string RecordID, bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(RecordIdentifier(RecordPosition, RecordID));
                ScrollToElement(RecordIdentifier(RecordPosition, RecordID));
            }
            else
                WaitForElementNotVisible(RecordIdentifier(RecordPosition, RecordID), 3);

            return this;
        }

        public CPContractServicesPage ValidateRecordNotVisible(Guid RecordID)
        {
            WaitForElementNotVisible(RecordCheckBox(RecordID.ToString()), 7);

            return this;
        }

        public CPContractServicesPage SelectRecord(Guid RecordID)
        {
            WaitForElementToBeClickable(RecordCheckBox(RecordID.ToString()));
            Click(RecordCheckBox(RecordID.ToString()));

            return this;
        }

        public CPContractServicesPage OpenRecord(Guid RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID.ToString()));
            ScrollToElement(RecordIdentifier(RecordID.ToString()));
            Click(RecordIdentifier(RecordID.ToString()));

            return this;
        }

        public CPContractServicesPage OpenRecord(int RecordPosition, Guid RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordPosition, RecordID.ToString()));
            Click(RecordIdentifier(RecordPosition, RecordID.ToString()));

            return this;
        }

        public CPContractServicesPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
        {
            ValidateElementText(RecordCell(RecordID.ToString(), CellPosition), ExpectedText);

            return this;
        }

        public CPContractServicesPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public CPContractServicesPage ValidateHeaderCellSortIcon(int CellPosition, bool AscendingSort)
        {
            ScrollToElement(gridPageHeaderSortIcon(CellPosition));
            WaitForElementVisible(gridPageHeaderSortIcon(CellPosition));

            if (AscendingSort)
                ValidateElementAttribute(gridPageHeaderSortIcon(CellPosition), "class", "sortasc");
            else
                ValidateElementAttribute(gridPageHeaderSortIcon(CellPosition), "class", "sortdesc");

            return this;
        }

        public CPContractServicesPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(RecordCheckBox(RecordId));
            ScrollToElement(RecordCheckBox(RecordId));
            Click(RecordCheckBox(RecordId));

            return this;
        }

        public CPContractServicesPage ValidateHeaderCellText(string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElement(ExpectedText));
            WaitForElementVisible(gridPageHeaderElement(ExpectedText));
            ValidateElementText(gridPageHeaderElement(ExpectedText), ExpectedText);
            return this;
        }

        public CPContractServicesPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(RecordCell(RecordId, CellPosition));
            ValidateElementText(RecordCell(RecordId, CellPosition), ExpectedText);

            return this;
        }


        public CPContractServicesPage ClickColumnHeader(int CellPosition)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            Click(gridPageHeaderElement(CellPosition));

            return this;
        }

        public CPContractServicesPage ClickColumnHeader(string ColumnName)
        {
            ScrollToElement(gridPageHeaderElement(ColumnName));
            WaitForElementVisible(gridPageHeaderElement(ColumnName));
            Click(gridPageHeaderElement(ColumnName));

            return this;
        }

        public CPContractServicesPage ClickColumnHeaderSortDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            Click(gridPageHeaderElementSortAscendingOrder(CellPosition));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPContractServicesPage ClickColumnHeaderSortDescendingOrder(string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
            WaitForElementToBeClickable(gridPageHeaderElementSortAscendingOrder(ColumnName));
            Click(gridPageHeaderElementSortAscendingOrder(ColumnName));

            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }


        public CPContractServicesPage ClickColumnHeaderSortAscendingOrder(string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
            ScrollToElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
            WaitForElementToBeClickable(gridPageHeaderElementSortDescendingOrder(ColumnName));
            Click(gridPageHeaderElementSortDescendingOrder(ColumnName));

            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }

        public CPContractServicesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            ScrollToElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public CPContractServicesPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

            return this;
        }

        public CPContractServicesPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition, string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

            return this;
        }

        public CPContractServicesPage ClickColumnHeaderSortAscendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            Click(gridPageHeaderElementSortDescendingOrder(CellPosition));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPContractServicesPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");
            return this;
        }

        public CPContractServicesPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(RecordIdentifier(RecordID));
                ScrollToElement(RecordIdentifier(RecordID));
            }
            else
                WaitForElementNotVisible(RecordIdentifier(RecordID), 3);

            Assert.AreEqual(ExpectedVisible, GetElementVisibility(RecordIdentifier(RecordID)));

            return this;

        }

        public CPContractServicesPage ValidateNoRecordsMessageVisible()
        {
            WaitForElementVisible(noRecordsMessage);
            WaitForElementVisible(noResultsMessage);

            return this;
        }

        public CPContractServicesPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition, string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");
            return this;
        }

        public CPContractServicesPage ClickDoNotUseViewFilterCheckbox()
        {
            WaitForElementToBeClickable(DoNotUseViewFilterCheckbox);
            string elementChecked = GetElementByAttributeValue(DoNotUseViewFilterCheckbox, "checked");
            if (elementChecked != "true")
                Click(DoNotUseViewFilterCheckbox);

            return this;
        }

        //Click Establishment Lookup Button
        public CPContractServicesPage ClickEstablishmentLookupButton()
        {
            WaitForElement(EstablishmentLookupButton);
            ScrollToElement(EstablishmentLookupButton);
            Click(EstablishmentLookupButton);

            return this;
        }

        //Click Contract Scheme Lookup Button
        public CPContractServicesPage ClickContractSchemeLookupButton()
        {
            WaitForElement(ContractSchemeLookupButton);
            ScrollToElement(ContractSchemeLookupButton);
            Click(ContractSchemeLookupButton);

            return this;
        }

        //Click Funder Lookup Button
        public CPContractServicesPage ClickFunderLookupButton()
        {
            WaitForElement(FunderLookupButton);
            ScrollToElement(FunderLookupButton);
            Click(FunderLookupButton);

            return this;
        }

        //Click Search Button
        public CPContractServicesPage ClickSearchButton()
        {
            WaitForElement(SearchButton);
            ScrollToElement(SearchButton);
            Click(SearchButton);

            return this;
        }

        //Click Clear Filters Button
        public CPContractServicesPage ClickClearFiltersButton()
        {
            WaitForElement(ClearFiltersButton);
            ScrollToElement(ClearFiltersButton);
            Click(ClearFiltersButton);

            return this;
        }

    }
}
