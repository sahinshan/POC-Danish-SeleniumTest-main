using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class CaseBrokerageOfferCommunicationsSubArea : CommonMethods
    {
        public CaseBrokerageOfferCommunicationsSubArea(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=brokerageoffer&')]");
        readonly By CWIFrame_CommunicationGrid = By.Id("CWIFrame_CommunicationGrid");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title = 'Back']");

        readonly By quickSearchTextBox = By.XPath("//*[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//*[@id='CWQuickSearchButton']");
        
        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRow(string RecordID, int RecordPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");



        public CaseBrokerageOfferCommunicationsSubArea WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWIFrame_CommunicationGrid);
            SwitchToIframe(CWIFrame_CommunicationGrid);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Brokerage Offer Communications");

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(exportToExcelButton);
            WaitForElement(deleteRecordButton);

            return this;
        }
      

        public CaseBrokerageOfferCommunicationsSubArea ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            MoveToElementInPage(newRecordButton);
            Click(newRecordButton);

            return this;

        }
        public CaseBrokerageOfferCommunicationsSubArea OpenRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }
        public CaseBrokerageOfferCommunicationsSubArea SelectRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }
        public CaseBrokerageOfferCommunicationsSubArea ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;

        }
        public CaseBrokerageOfferCommunicationsSubArea ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }

        public CaseBrokerageOfferCommunicationsSubArea ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }

        public CaseBrokerageOfferCommunicationsSubArea ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }


        public CaseBrokerageOfferCommunicationsSubArea ValidateNewRecordButtonVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(newRecordButton);
            }
            else
            {
                WaitForElementNotVisible(newRecordButton, 5);
            }
            return this;
        }

        public CaseBrokerageOfferCommunicationsSubArea ValidateNoErrorMessageLabelVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordLabel);
            }
            else
            {
                WaitForElementNotVisible(NoRecordLabel, 5);
            }
            return this;
        }

        public CaseBrokerageOfferCommunicationsSubArea ValidateNoRecordMessageVisibile(bool ExpectedText)
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

        public CaseBrokerageOfferCommunicationsSubArea ValidateRecordPositionInList(string RecordId, int RecordPosition)
        {
            WaitForElement(recordRow(RecordId, RecordPosition));

            return this;
        }

        public CaseBrokerageOfferCommunicationsSubArea ValidateRecordVisible(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public CaseBrokerageOfferCommunicationsSubArea ValidateRecordNotVisible(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 5);

            return this;
        }

        public CaseBrokerageOfferCommunicationsSubArea InsertQuickSearchQuery(string TextToInsert)
        {
            SendKeys(quickSearchTextBox, TextToInsert);

            return this;
        }

        public CaseBrokerageOfferCommunicationsSubArea ClickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;

        }

        public CaseBrokerageOfferCommunicationsSubArea ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

    }
}
