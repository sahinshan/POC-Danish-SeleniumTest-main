using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class CaseBrokerageEpisodeServiceProvisionsPage : CommonMethods
    {
        public CaseBrokerageEpisodeServiceProvisionsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=brokerageepisode&')]");
        readonly By CWNavItem_BrokerageEpisodeServiceProvisionFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/div/div/button[@title = 'Back']");

        readonly By quickSearchTextBox = By.XPath("//*[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//*[@id='CWQuickSearchButton']");
        
        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By record = By.XPath("//*[@id='CWGrid']/tbody/tr[6]");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRow(string RecordID, int RecordPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        By recordCheckbox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");
        

        public CaseBrokerageEpisodeServiceProvisionsPage WaitForCaseBrokerageEpisodeServiceProvisionsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWNavItem_BrokerageEpisodeServiceProvisionFrame);
            SwitchToIframe(CWNavItem_BrokerageEpisodeServiceProvisionFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Service Provisions");

            WaitForElement(exportToExcelButton);
            WaitForElement(deleteRecordButton);

            return this;
        }
      

        public CaseBrokerageEpisodeServiceProvisionsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }
        public CaseBrokerageEpisodeServiceProvisionsPage OpenRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            Click(recordRow(RecordId));
            return this;
            
        }
        public CaseBrokerageEpisodeServiceProvisionsPage SelectRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }
        public CaseBrokerageEpisodeServiceProvisionsPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;

        }
        public CaseBrokerageEpisodeServiceProvisionsPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }

        public CaseBrokerageEpisodeServiceProvisionsPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }


        public CaseBrokerageEpisodeServiceProvisionsPage ValidateNewRecordButtonVisibility(bool ExpectedVisible)
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

        public CaseBrokerageEpisodeServiceProvisionsPage ValidateNoErrorMessageLabelVisibile(bool ExpectedText)
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

        public CaseBrokerageEpisodeServiceProvisionsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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

        public CaseBrokerageEpisodeServiceProvisionsPage ValidateRecordPositionInList(string RecordId, int RecordPosition)
        {
            WaitForElement(recordRow(RecordId, RecordPosition));

            return this;
        }

        public CaseBrokerageEpisodeServiceProvisionsPage ValidateRecordVisible(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public CaseBrokerageEpisodeServiceProvisionsPage ValidateRecordNotVisible(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 5);

            return this;
        }

        public CaseBrokerageEpisodeServiceProvisionsPage InsertQuickSearchQuery(string TextToInsert)
        {
            SendKeys(quickSearchTextBox, TextToInsert);

            return this;
        }

        public CaseBrokerageEpisodeServiceProvisionsPage ClickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;

        }

        public CaseBrokerageEpisodeServiceProvisionsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public CaseBrokerageEpisodeServiceProvisionsPage ClickBackButton()
        {

            WaitForElementVisible(back_Button);
            Click(back_Button);

            return this;
        }

    }
}
