using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class RTTWaitTimesPage : CommonMethods
    {
        public RTTWaitTimesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=case&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");

        readonly By quickSearchTextBox = By.XPath("//*[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//*[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.Id("CWRefreshButton");


        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");
        readonly By HealthLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_Health']/a");
        readonly By ConsultantEpisodesLeftSubMenuItem = By.XPath("//*[@id='CWNavItem_InpatientConsultantEpisodes']");
        readonly By HealthAppointmentLeftSubMenuItem = By.XPath("//*[@id='CWNavItem_HealthAppointment']");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRow(string RecordID, int RecordPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        public RTTWaitTimesPage WaitForRTTWaitTimesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "RTT Wait Times");

            WaitForElement(exportToExcelButton);

            return this;
        }

        public RTTWaitTimesPage WaitForRTTWaitTimesMenusSectionToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            return this;
        }

        public RTTWaitTimesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }

        public RTTWaitTimesPage ClickRefreshButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

        public RTTWaitTimesPage OpenRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            MoveToElementInPage(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public RTTWaitTimesPage OpenRecord(Guid RecordId)
        {
            return OpenRecord(RecordId.ToString());
        }

        public RTTWaitTimesPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public RTTWaitTimesPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            MoveToElementInPage(deleteRecordButton);
            Click(deleteRecordButton);

            return this;

        }

        public RTTWaitTimesPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            MoveToElementInPage(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }

        public RTTWaitTimesPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            MoveToElementInPage(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }

        public RTTWaitTimesPage ValidateNewRecordButtonVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(newRecordButton);
            else
                WaitForElementNotVisible(newRecordButton, 5);
            
            return this;
        }

        public RTTWaitTimesPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordLabel);
                WaitForElementVisible(NoRecordMessage);
            }
            else
            {
                WaitForElementNotVisible(NoRecordLabel, 5);
                WaitForElementNotVisible(NoRecordMessage, 5);
            }
            
            return this;
        }

        public RTTWaitTimesPage ValidateRecordPositionInList(string RecordId, int RecordPosition)
        {
            WaitForElement(recordRow(RecordId, RecordPosition));

            return this;
        }

        public RTTWaitTimesPage ValidateRecordVisible(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public RTTWaitTimesPage ValidateRecordNotVisible(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 5);

            return this;
        }

        public RTTWaitTimesPage InsertQuickSearchQuery(string TextToInsert)
        {
            SendKeys(quickSearchTextBox, TextToInsert);

            return this;
        }

        public RTTWaitTimesPage ClickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;

        }

        public RTTWaitTimesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public RTTWaitTimesPage NavigateToConsultantEpisodesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(HealthLeftSubMenu);
            Click(HealthLeftSubMenu);

            WaitForElementToBeClickable(ConsultantEpisodesLeftSubMenuItem);
            Click(ConsultantEpisodesLeftSubMenuItem);

            return this;
        }

        public RTTWaitTimesPage NavigateToHealthAppointmentsContactsAndOffersPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(HealthLeftSubMenu);
            Click(HealthLeftSubMenu);

            WaitForElementToBeClickable(HealthAppointmentLeftSubMenuItem);
            Click(HealthAppointmentLeftSubMenuItem);

            return this;
        }

    }
}
