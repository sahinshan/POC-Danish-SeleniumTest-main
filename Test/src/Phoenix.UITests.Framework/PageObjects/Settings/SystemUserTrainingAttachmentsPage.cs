
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
    public class SystemUserTrainingAttachmentsPage : CommonMethods
    {
        public SystemUserTrainingAttachmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemusertraining&')]");
        readonly By CWIFrame_SystemUserTrainingAttachmentItems = By.Id("CWIFrame_SystemUserTrainingAttachmentItems");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Training Attachments']");

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
        
        

      
        public SystemUserTrainingAttachmentsPage WaitForSystemUserTrainingAttachmentsSubgridPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWIFrame_SystemUserTrainingAttachmentItems);
            SwitchToIframe(CWIFrame_SystemUserTrainingAttachmentItems);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(pagehehader);

            WaitForElementToBeClickable(addNewRecordButton);
            WaitForElementToBeClickable(deleteButton);

            return this;
        }

        public SystemUserTrainingAttachmentsPage WaitForSystemUserTrainingAttachmentsPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(pagehehader);

            WaitForElementToBeClickable(addNewRecordButton);
            WaitForElementToBeClickable(deleteButton);
            
            WaitForElement(viewsPicklist);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        public SystemUserTrainingAttachmentsPage ClickAddNewButton()
        {
            this.WaitForElementToBeClickable(addNewRecordButton);
            this.Click(addNewRecordButton);

            return new SystemUserTrainingAttachmentsPage(driver, Wait, appURL);
        }


        public SystemUserTrainingAttachmentsPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public SystemUserTrainingAttachmentsPage ClickSelectAllCheckBox()
        {
            this.WaitForElementToBeClickable(selectAllRecord);
            this.Click(selectAllRecord);

            return new SystemUserTrainingAttachmentsPage(driver, Wait, appURL);
        }

        public SystemUserTrainingAttachmentsPage SelectRecord(string RecordID)
        {
            this.WaitForElementToBeClickable(recordCheckBox(RecordID));
            this.Click(recordCheckBox(RecordID));

            return this;
        }

        public SystemUserTrainingAttachmentsPage ValidateRecordVisible(string RecordID)
        {
            WaitForElementVisible(recordCheckBox(RecordID));

            return this;
        }

        public SystemUserTrainingAttachmentsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordIdentifier(RecordID));
            Click(recordIdentifier(RecordID));

            return this;
        }

        public SystemUserTrainingAttachmentsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }


        public SystemUserTrainingAttachmentsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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


        public SystemUserTrainingAttachmentsPage InsertQuickSearchText(string Text)
        {
            this.SendKeys(quickSearchTextBox, Text);

            return this;
        }

        public SystemUserTrainingAttachmentsPage ClickQuickSearchButton()
        {
            this.Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            return this;
        }

        public SystemUserTrainingAttachmentsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            return this;
        }

        public SystemUserTrainingAttachmentsPage ValidateSelectedView(string ExpectedText)
        {
            ValidatePicklistSelectedText(viewsPicklist, ExpectedText);
            return this;
        }

        public SystemUserTrainingAttachmentsPage SelectView(string TextToSelect)
        {
            WaitForElementToBeClickable(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }

        public SystemUserTrainingAttachmentsPage ValidateTableHeaderCellText(int HeaderCellPosition, string ExpectedText)
        {
            ValidateElementText(tableHeaderCell(HeaderCellPosition), ExpectedText);

            return this;
        }


    }
}
