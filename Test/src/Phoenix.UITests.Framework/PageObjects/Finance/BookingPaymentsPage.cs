using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinance
{
    public class BookingPaymentsPage : CommonMethods
    {
        public BookingPaymentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpayrollbatch&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By PageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By ExportToExcelButton = By.XPath("//*[@id='TI_ExportToExcelButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");
        By RecordIdentifier(int CellPosition, string RecordID) => By.XPath("//tr[" + CellPosition + "][@id='" + RecordID + "']");
        By gridPageHeaderLinkElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a");
        By gridPageHeaderSortDescIcon(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[@class='sortdesc']");
        By gridPageHeaderSortAscIcon(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[@class='sortasc']");
        By gridPageHeaderElement(int HeaderCellPosition, string HeaderCellText) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[text() = '" + HeaderCellText + "']");

        


        public BookingPaymentsPage WaitForPageToLoad()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(PageHeader);
            WaitForElementVisible(ExportToExcelButton);

            return this;
        }

        public BookingPaymentsPage WaitForPageToLoadFromPayrollBatch()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(PageHeader);
            WaitForElementVisible(ExportToExcelButton);

            return this;
        }



        public BookingPaymentsPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public BookingPaymentsPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }




        public BookingPaymentsPage SelectFinanceInvoiceRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            ScrollToElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public BookingPaymentsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ScrollToElement(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        public BookingPaymentsPage RecordsPageValidateHeaderLinkText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridPageHeaderLinkElement(CellPosition));
            WaitForElementVisible(gridPageHeaderLinkElement(CellPosition));
            ValidateElementText(gridPageHeaderLinkElement(CellPosition), ExpectedText);

            return this;
        }

        public BookingPaymentsPage RecordsPageValidateHeaderSortDescIconVisible(int CellPosition)
        {
            ScrollToElement(gridPageHeaderSortDescIcon(CellPosition));
            WaitForElementVisible(gridPageHeaderSortDescIcon(CellPosition)); ;

            return this;
        }

        public BookingPaymentsPage RecordsPageValidateHeaderSortAscIconVisible(int CellPosition)
        {
            ScrollToElement(gridPageHeaderSortAscIcon(CellPosition));
            WaitForElementVisible(gridPageHeaderSortAscIcon(CellPosition)); ;

            return this;
        }

        public BookingPaymentsPage ClickHeaderCellText(int CellPosition, string HeaderCellText)
        {
            WaitForElementToBeClickable(gridPageHeaderElement(CellPosition, HeaderCellText));
            ScrollToElement(gridPageHeaderElement(CellPosition, HeaderCellText));
            Click(gridPageHeaderElement(CellPosition, HeaderCellText));

            return this;
        }

        public BookingPaymentsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            ScrollToElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return this;
        }

        public BookingPaymentsPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public BookingPaymentsPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible = true)
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

        public BookingPaymentsPage ValidateRecordIsPresent(Guid RecordID, bool ExpectedVisible = true)
        {
            return ValidateRecordIsPresent(RecordID.ToString(), ExpectedVisible);

        }

        public BookingPaymentsPage ValidateRecordIsPresent(int CellPosition, string RecordID, bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(RecordIdentifier(CellPosition, RecordID));
                ScrollToElement(RecordIdentifier(CellPosition, RecordID));
            }
            else
                WaitForElementNotVisible(RecordIdentifier(CellPosition, RecordID), 3);

            Assert.AreEqual(ExpectedVisible, GetElementVisibility(RecordIdentifier(CellPosition, RecordID)));

            return this;
        }

        public BookingPaymentsPage ValidateRecordIsPresent(int CellPosition, Guid RecordID, bool ExpectedVisible = true)
        {
            return ValidateRecordIsPresent(CellPosition, RecordID.ToString(), ExpectedVisible);

        }
    }
}
