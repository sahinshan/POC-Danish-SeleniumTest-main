
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the Team Members sub page when accessed via a system user record
    /// Workplace - People - People Record - Address
    /// </summary>
    public class PersonAddressesPage : CommonMethods
    {
        public PersonAddressesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWNavItem_AddressesFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Person Addresses']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        readonly By startDate_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/span[2]");
        readonly By relatedRecords = By.XPath("//*[@id='SysView']/option[text()='Related Records']");
        readonly By startDate_ColumnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/span[text()='Start Date']");
        readonly By endDate_ColumnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/a/span[text()='End Date']");






        By recordIdentifier(string recordID) => By.XPath("//tr[@id='" + recordID + "']/td[2]");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By recordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[' + RecordPosition + '][@id='" + RecordID + "']");
        By tableHeaderCell(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By tableHeaderText(int HeaderCellPosition, string ColumnHeader) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[text()='" + ColumnHeader + "']");
        

        public PersonAddressesPage WaitForPersonAddressesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWNavItem_AddressesFrame);
            SwitchToIframe(CWNavItem_AddressesFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(pagehehader);

            WaitForElement(addNewRecordButton);

            WaitForElement(viewsPicklist);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        /// <summary>
        /// use this method to switch the focus to the Iframe that holds the Dynamic Dialog popup.
        /// In the case of System User Team Member sub page the Iframe path is CWContentIFrame -> iframe_CWDialog_
        /// </summary>
        /// <returns></returns>
        public PersonAddressesPage SwitchToDynamicsDialogLevelIframe()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            return this;
        }

        public PersonAddressesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public PersonAddressesPage ClickAddNewButton()
        {
            this.WaitForElement(addNewRecordButton);
            this.Click(addNewRecordButton);

            return this;
        }


        public PersonAddressesPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(NoRecordMessage, 5);
            }
            return this;
        }


        public PersonAddressesPage OpenRecord(string RecordID)
        {
            this.Click(recordRow(RecordID));

            return this;
        }

        public PersonAddressesPage ValidateRelatedRecords_Option(String ExpectedText)
        {
            ValidateElementText(relatedRecords, ExpectedText);


            return this;
        }

        public PersonAddressesPage ClickStartDateSort()
        {
            this.Click(startDate_sort);

            return this;
        }

        public PersonAddressesPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            ScrollToElement(recordPosition(ElementPosition, RecordID));
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public PersonAddressesPage ValidateStartDateColumnHeader(string ExpectedText)
        {


            WaitForElementVisible(startDate_ColumnHeader);
            ValidateElementText(startDate_ColumnHeader, ExpectedText);

            return this;
        }

        public PersonAddressesPage ValidateEndDateColumnHeader(string ExpectedText)
        {


            WaitForElementVisible(endDate_ColumnHeader);
            ValidateElementText(endDate_ColumnHeader, ExpectedText);

            return this;
        }

        public PersonAddressesPage ClickTableHeaderCell(int CellPosition)
        {
            Click(tableHeaderCell(CellPosition));

            return this;
        }

        public PersonAddressesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonAddressesPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
        {
            return ValidateRecordCellText(RecordID.ToString(), CellPosition, ExpectedText);
        }

        public PersonAddressesPage ValidateColumnHeader(int HeaderCellPosition,string ColumnHeader,string ExpectedText)
        {

            WaitForElement(tableHeaderText(HeaderCellPosition, ColumnHeader));
            ScrollToElement(tableHeaderText(HeaderCellPosition, ColumnHeader));
            WaitForElement(tableHeaderText(HeaderCellPosition, ColumnHeader));
            ValidateElementText(tableHeaderText(HeaderCellPosition, ColumnHeader), ExpectedText);

            return this;
        }

       
    }
}
