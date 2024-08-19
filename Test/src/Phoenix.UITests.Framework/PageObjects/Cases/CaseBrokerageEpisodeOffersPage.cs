using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class CaseBrokerageEpisodeOffersPage : CommonMethods
    {
        public CaseBrokerageEpisodeOffersPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=brokerageepisode&')]");
        readonly By CWNavItem_BrokerageEpisodeOffersFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/div/div/button[@title = 'Back']");
        //readonly By details_Button = By.XPath("/html/body/form/div[5]/nav/div/ul/li[2]/a[contains(@onclick,'LoadNavGroup')][text()='Details']");
        readonly By details_Button = By.XPath("//*[@id='CWNavGroup_EditForm']/a[text()='Details']");
        readonly By quickSearchTextBox = By.XPath("//*[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//*[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.Id("CWRefreshButton");


        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By ServiceProvisionLeftMenuLink = By.Id("CWNavItem_ServiceProvisions");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRow(string RecordID, int RecordPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");



        public CaseBrokerageEpisodeOffersPage WaitForCaseBrokerageEpisodeOffersPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWNavItem_BrokerageEpisodeOffersFrame);
            SwitchToIframe(CWNavItem_BrokerageEpisodeOffersFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Brokerage Offers");

            WaitForElement(exportToExcelButton);
            WaitForElement(deleteRecordButton);

            return this;
        }

        public CaseBrokerageEpisodeOffersPage WaitForCaseBrokerageEpisodeOffersDetailsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            return this;
        }


        public CaseBrokerageEpisodeOffersPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }
        public CaseBrokerageEpisodeOffersPage OpenRecord(string RecordId)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElement(recordRow(RecordId));
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }
        public CaseBrokerageEpisodeOffersPage SelectRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }
        public CaseBrokerageEpisodeOffersPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;

        }
        public CaseBrokerageEpisodeOffersPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }

        public CaseBrokerageEpisodeOffersPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }


        public CaseBrokerageEpisodeOffersPage ValidateNewRecordButtonVisibility(bool ExpectedVisible)
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

        public CaseBrokerageEpisodeOffersPage ValidateNoErrorMessageLabelVisibile(bool ExpectedText)
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

        public CaseBrokerageEpisodeOffersPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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

        public CaseBrokerageEpisodeOffersPage ValidateRecordPositionInList(string RecordId, int RecordPosition)
        {
            WaitForElement(recordRow(RecordId, RecordPosition));

            return this;
        }

        public CaseBrokerageEpisodeOffersPage ValidateRecordVisible(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public CaseBrokerageEpisodeOffersPage ValidateRecordNotVisible(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 5);

            return this;
        }

        public CaseBrokerageEpisodeOffersPage InsertQuickSearchQuery(string TextToInsert)
        {
            SendKeys(quickSearchTextBox, TextToInsert);

            return this;
        }

        public CaseBrokerageEpisodeOffersPage ClickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;

        }

        public CaseBrokerageEpisodeOffersPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public CaseBrokerageEpisodeOffersPage ClickBackButton()
        {

            WaitForElementVisible(back_Button);
            Click(back_Button);

            return this;
        }

        public CaseBrokerageEpisodeOffersPage NavigateToServiceProvisionSubPage()
        {
            WaitForElementVisible(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementVisible(ServiceProvisionLeftMenuLink);
            Click(ServiceProvisionLeftMenuLink);

            return this;
        }

        public CaseBrokerageEpisodeOffersPage ClickDetailsButton()
        {
            WaitForElementVisible(details_Button);
            WaitForElementToBeClickable(details_Button);
            Click(details_Button);

            return this;
        }

        public CaseBrokerageEpisodeOffersPage ClickRefreshButton()
        {
            MoveToElementInPage(refreshButton);
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;

        }

    }
}
