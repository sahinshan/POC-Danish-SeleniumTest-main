using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
    public class CPFinanceInvoiceBatchSetupsPage : CommonMethods
    {
        public CPFinanceInvoiceBatchSetupsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CWDialogIframe_FinanceInvoiceBatchSetup = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=careproviderfinanceinvoicebatchsetup&')]");
        readonly By CWUrlIframe = By.Id("CWUrlPanel_IFrame");        
        readonly By FinanceInvoiceBatchSetupPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By NewRecordButton = By.XPath("//*[@id='TI_NewRecordButton']");
        readonly By ExportToExcelButton = By.XPath("//*[@id='TI_ExportToExcelButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By BulkUpdateButton = By.XPath("//*[@id='TI_BulkEditButton']");
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

        public CPFinanceInvoiceBatchSetupsPage WaitForCPFinanceInvoiceBatchSetupsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWDialogIframe_FinanceInvoiceBatchSetup);
            SwitchToIframe(CWDialogIframe_FinanceInvoiceBatchSetup);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWUrlIframe);
            SwitchToIframe(CWUrlIframe);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(FinanceInvoiceBatchSetupPageHeader);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage WaitForPageToLoad()
        {

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(FinanceInvoiceBatchSetupPageHeader);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ValidateToolbarOptionsDisplayed()
        {
            
            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(BulkUpdateButton);
            WaitForElementVisible(DeleteRecordButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ClickAdditionalItemsButton()
        {
            WaitForElementToBeClickable(AdditionalItemsButton);
            Click(AdditionalItemsButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ClickBulkUpdateButton()
        {
            WaitForElementToBeClickable(BulkUpdateButton);
            Click(BulkUpdateButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ClickDeleteRecordButton()
        {

            if (GetElementVisibility(DeleteRecordButton).Equals(false))
            {
                if (GetElementVisibility(ToolBarMenu).Equals(true) && !GetElementByAttributeValue(ToolBarMenu, "class").Contains("show"))
                {
                    WaitForElementToBeClickable(ToolBarMenu);
                    Click(ToolBarMenu);
                }
            }
            else
            {
                WaitForElementToBeClickable(DeleteRecordButton);
                Click(DeleteRecordButton);

            }

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            ScrollToElement(viewSelector);
            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewSelector);
            ScrollToElement(viewSelector);
            ValidatePicklistSelectedText(viewSelector, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ValidateSystemViewOptionNotPresent(string optionNotPresent)
        {
            ScrollToElement(viewSelector);
            ValidatePicklistDoesNotContainsElementByText(viewSelector, optionNotPresent);
            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage InsertSearchQuery(string SearchQuery)
        {
            WaitForElementToBeClickable(searchTextBox);
            ScrollToElement(searchTextBox);
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ClickSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            ScrollToElement(searchButton);
            Click(searchButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            ScrollToElement(RefreshButton);
            Click(RefreshButton);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ValidateRecordVisible(Guid RecordID)
        {
            WaitForElementVisible(RecordCheckBox(RecordID.ToString()));

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ValidateRecordVisible(int RecordPosition, string RecordID, bool ExpectedVisible)
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

        public CPFinanceInvoiceBatchSetupsPage ValidateRecordNotVisible(Guid RecordID)
        {
            WaitForElementNotVisible(RecordCheckBox(RecordID.ToString()), 7);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage SelectRecord(Guid RecordID)
        {
            WaitForElementToBeClickable(RecordCheckBox(RecordID.ToString()));
            Click(RecordCheckBox(RecordID.ToString()));

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage OpenRecord(Guid RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID.ToString()));
            ScrollToElement(RecordIdentifier(RecordID.ToString()));
            Click(RecordIdentifier(RecordID.ToString()));

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage OpenRecord(int RecordPosition, Guid RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordPosition, RecordID.ToString()));
            Click(RecordIdentifier(RecordPosition, RecordID.ToString()));

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
        {
            ValidateElementText(RecordCell(RecordID.ToString(), CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ValidateHeaderCellSortIcon(int CellPosition, bool AscendingSort)
        {
            ScrollToElement(gridPageHeaderSortIcon(CellPosition));
            WaitForElementVisible(gridPageHeaderSortIcon(CellPosition));

            if(AscendingSort)
                ValidateElementAttribute(gridPageHeaderSortIcon(CellPosition), "class", "sortasc");
            else
                ValidateElementAttribute(gridPageHeaderSortIcon(CellPosition), "class", "sortdesc");

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(RecordCheckBox(RecordId));
            ScrollToElement(RecordCheckBox(RecordId));
            Click(RecordCheckBox(RecordId));

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ValidateHeaderCellText(string ExpectedText)
        {
            ScrollToElement(gridPageHeaderElement(ExpectedText));
            WaitForElementVisible(gridPageHeaderElement(ExpectedText));
            ValidateElementText(gridPageHeaderElement(ExpectedText), ExpectedText);
            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(RecordCell(RecordId, CellPosition));
            ValidateElementText(RecordCell(RecordId, CellPosition), ExpectedText);

            return this;
        }


        public CPFinanceInvoiceBatchSetupsPage ClickColumnHeader(int CellPosition)
        {
            ScrollToElement(gridPageHeaderElement(CellPosition));
            WaitForElementVisible(gridPageHeaderElement(CellPosition));
            Click(gridPageHeaderElement(CellPosition));

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ClickColumnHeader(string ColumnName)
        {
            ScrollToElement(gridPageHeaderElement(ColumnName));
            WaitForElementVisible(gridPageHeaderElement(ColumnName));
            Click(gridPageHeaderElement(ColumnName));

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ClickColumnHeaderSortDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            Click(gridPageHeaderElementSortAscendingOrder(CellPosition));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ClickColumnHeaderSortDescendingOrder(string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
            ScrollToElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
            WaitForElementToBeClickable(gridPageHeaderElementSortAscendingOrder(ColumnName));
            Click(gridPageHeaderElementSortAscendingOrder(ColumnName));

            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }


        public CPFinanceInvoiceBatchSetupsPage ClickColumnHeaderSortAscendingOrder(string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
            ScrollToElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
            WaitForElementToBeClickable(gridPageHeaderElementSortDescendingOrder(ColumnName));
            Click(gridPageHeaderElementSortDescendingOrder(ColumnName));

            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            ScrollToElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition, string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ClickColumnHeaderSortAscendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            Click(gridPageHeaderElementSortDescendingOrder(CellPosition));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");
            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
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

        public CPFinanceInvoiceBatchSetupsPage ValidateNoRecordsMessageVisible()
        {
            WaitForElementVisible(noRecordsMessage);
            WaitForElementVisible(noResultsMessage);

            return this;
        }

        public CPFinanceInvoiceBatchSetupsPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition, string ColumnName)
        {
            WaitForElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            ScrollToElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
            ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");
            return this;
        }

    }
}
