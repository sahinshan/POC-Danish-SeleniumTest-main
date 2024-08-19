using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserLanguagesPage : CommonMethods
    {
        public SystemUserLanguagesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By CWNavItem_LanguagesFrame = By.Id("CWUrlPanel_IFrame");


        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        
        By tableHeaderCell(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By recordCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");
        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By recordRowPosition(string recordID, int PositionInList) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + PositionInList + "][@id='" + recordID + "']/td[2]");
        By recordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");



        public SystemUserLanguagesPage WaitForSystemUserLanguagesPageToLoad()
        {
            SwitchToDefaultFrame();

            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            this.WaitForElement(personRecordIFrame);
            this.SwitchToIframe(personRecordIFrame);

            this.WaitForElement(CWNavItem_LanguagesFrame);
            this.SwitchToIframe(CWNavItem_LanguagesFrame);

            this.WaitForElement(newRecordButton);
            this.WaitForElement(pageHeader);

            this.ValidateElementText(pageHeader, "System User Languages");

            return this;
        }


        public SystemUserLanguagesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }
        public SystemUserLanguagesPage ValidateRecordPresent(string RecordID)
        {
            WaitForElementVisible(recordRow(RecordID));

            return this;
        }

        public SystemUserLanguagesPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 3);

            return this;
        }

        public SystemUserLanguagesPage ValidateRecordPositionInList(string RecordID, int ExpectedPositionInList)
        {
            WaitForElementVisible(recordRowPosition(RecordID, ExpectedPositionInList));

            return this;
        }

        public SystemUserLanguagesPage InsertQuickSearchText(string Text)
        {
            this.SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public SystemUserLanguagesPage ClickQuickSearchButton()
        {
            this.Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SystemUserLanguagesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public SystemUserLanguagesPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public SystemUserLanguagesPage OpenRecord(string RecordID)
        {
            this.Click(recordRow(RecordID));

            return this;
        }

        public SystemUserLanguagesPage SelectRecord(string RecordID)
        {
            this.Click(recordCheckBox(RecordID));

            return this;
        }

        public SystemUserLanguagesPage ClickTableHeaderCell(int CellPosition)
        {
            Click(tableHeaderCell(CellPosition));

            return this;
        }


    }
}
