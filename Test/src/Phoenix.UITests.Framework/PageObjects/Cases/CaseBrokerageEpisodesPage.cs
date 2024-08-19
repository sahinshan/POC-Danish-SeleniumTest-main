using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class CaseBrokerageEpisodesPage : CommonMethods
    {
        public CaseBrokerageEpisodesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=case&')]");
        readonly By CWNavItem_BrokerageEpisodeFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");

        readonly By quickSearchTextBox = By.XPath("//*[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//*[@id='CWQuickSearchButton']");
        
        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRow(string RecordID, int RecordPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        public CaseBrokerageEpisodesPage WaitForCaseBrokerageEpisodesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWNavItem_BrokerageEpisodeFrame);
            SwitchToIframe(CWNavItem_BrokerageEpisodeFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(pageHeader);

            ValidateElementText(pageHeader, "Brokerage Episodes");

            WaitForElementVisible(exportToExcelButton);
            WaitForElementVisible(deleteRecordButton);

            return this;
        }
      

        public CaseBrokerageEpisodesPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }
        public CaseBrokerageEpisodesPage OpenRecord(string RecordId)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementVisible(recordRow(RecordId));
            WaitForElementToBeClickable(recordRow(RecordId));
            MoveToElementInPage(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }
        public CaseBrokerageEpisodesPage SelectRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }
        public CaseBrokerageEpisodesPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;

        }
        public CaseBrokerageEpisodesPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }

        public CaseBrokerageEpisodesPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }


        public CaseBrokerageEpisodesPage ValidateNewRecordButtonVisibility(bool ExpectedVisible)
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

        public CaseBrokerageEpisodesPage ValidateNoErrorMessageLabelVisibile(bool ExpectedText)
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

        public CaseBrokerageEpisodesPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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

        public CaseBrokerageEpisodesPage ValidateRecordPositionInList(string RecordId, int RecordPosition)
        {
            WaitForElement(recordRow(RecordId, RecordPosition));

            return this;
        }

        public CaseBrokerageEpisodesPage ValidateRecordVisible(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public CaseBrokerageEpisodesPage ValidateRecordNotVisible(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 5);

            return this;
        }

        public CaseBrokerageEpisodesPage InsertQuickSearchQuery(string TextToInsert)
        {
            SendKeys(quickSearchTextBox, TextToInsert);

            return this;
        }

        public CaseBrokerageEpisodesPage ClickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;

        }

        public CaseBrokerageEpisodesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

    }
}
