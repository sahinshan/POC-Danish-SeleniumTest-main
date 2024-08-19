
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
    /// Settings - Security - System Users - System User record - Team Member tabs
    /// </summary>
    public class SystemUserAbsencesPage : CommonMethods
    {
        public SystemUserAbsencesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By CWNavItem_AddressesFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Open-ended Absences']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        
        readonly By selectAllRecord = By.Id("cwgridheaderselector");




        By recordIdentifier(string recordID) => By.XPath("//tr[@id='" + recordID + "']/td[2]");

        By recordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
        
        By recordCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");



        By tableHeaderCell(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
        
        

      
        public SystemUserAbsencesPage WaitForSystemUserAbsencesPageToLoad()
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
            WaitForElementToBeClickable(quickSearchButton);
            WaitForElementToBeClickable(refreshButton);

            return this;
        }

        public SystemUserAbsencesPage ClickAddNewButton()
        {
            WaitForElementToBeClickable(addNewRecordButton);
            Click(addNewRecordButton);

            return this;
        }


        public SystemUserAbsencesPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public SystemUserAbsencesPage ClickSelectAllCheckBox()
        {
            WaitForElementToBeClickable(selectAllRecord);
            Click(selectAllRecord);

            return this;
        }

        public SystemUserAbsencesPage SelectRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCheckBox(RecordID));
            Click(recordCheckBox(RecordID));

            return this;
        }

        public SystemUserAbsencesPage ValidateRecordVisible(string RecordID)
        {
            WaitForElementVisible(recordCheckBox(RecordID));

            return this;
        }

        public SystemUserAbsencesPage ValidateRecordNotVisible(string RecordID)
        {
            WaitForElementNotVisible(recordCheckBox(RecordID), 7);

            return this;
        }

        public SystemUserAbsencesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordIdentifier(RecordID));
            Click(recordIdentifier(RecordID));

            return this;
        }

        public SystemUserAbsencesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }


        public SystemUserAbsencesPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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


        public SystemUserAbsencesPage InsertQuickSearchText(string Text)
        {
            SendKeys(quickSearchTextBox, Text);

            return this;
        }

        public SystemUserAbsencesPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            return this;
        }

        public SystemUserAbsencesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            return this;
        }

        public SystemUserAbsencesPage ValidateSelectedView(string ExpectedText)
        {
            ValidatePicklistSelectedText(viewsPicklist, ExpectedText);
            return this;
        }

        public SystemUserAbsencesPage SelectView(string TextToSelect)
        {
            WaitForElementToBeClickable(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }

        public SystemUserAbsencesPage ValidateTableHeaderCellText(int HeaderCellPosition, string ExpectedText)
        {
            ScrollToElement(tableHeaderCell(HeaderCellPosition));
            ValidateElementText(tableHeaderCell(HeaderCellPosition), ExpectedText);

            return this;
        }


    }
}
