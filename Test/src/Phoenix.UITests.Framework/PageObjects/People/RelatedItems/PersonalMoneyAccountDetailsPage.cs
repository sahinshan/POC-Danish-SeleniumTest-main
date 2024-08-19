using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonalMoneyAccountDetailsPage : CommonMethods
    {
        public PersonalMoneyAccountDetailsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personAllegationIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersonalmoneyaccount&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Personal Money Account Details']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchField = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By cancelButton = By.Id("TI_Cancel");
        

        By GridHeaderCell(int cellPosition, string ExpectedText) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]//*[text()='" + ExpectedText + "']");


        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        public PersonalMoneyAccountDetailsPage WaitForPersonalMoneyAccountDetailsPageToLoad(bool ExpectNewRecordButton = true)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(personAllegationIFrame);
            SwitchToIframe(personAllegationIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(viewSelector);
            WaitForElementVisible(searchField);
            WaitForElementVisible(searchButton);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(exportToExcelButton);
            WaitForElementVisible(assignButton);
            WaitForElementVisible(cancelButton);

            if (ExpectNewRecordButton)
                WaitForElementVisible(newRecordButton);
            else
                WaitForElementNotVisible(newRecordButton, 3);

            return this;
        }

        public PersonalMoneyAccountDetailsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }

        public PersonalMoneyAccountDetailsPage ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);

            return this;
        }

        public PersonalMoneyAccountDetailsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonalMoneyAccountDetailsPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
        {
            return ValidateRecordCellText(RecordID.ToString(), CellPosition, ExpectedText);
        }

        public PersonalMoneyAccountDetailsPage SelectView(string TextToSelect)
        {
            SelectPicklistElementByText(viewSelector, TextToSelect);

            return this;
        }

        public PersonalMoneyAccountDetailsPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonalMoneyAccountDetailsPage SelectRecord(Guid RecordId)
        {
            return SelectRecord(RecordId.ToString());
        }

        public PersonalMoneyAccountDetailsPage ValidateRecordPresent(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(recordRowCheckBox(RecordId));
            else
                WaitForElementNotVisible(recordRowCheckBox(RecordId), 3);

            return this;
        }

        public PersonalMoneyAccountDetailsPage ValidateRecordPresent(Guid RecordId, bool ExpectVisible)
        {
            return ValidateRecordPresent(RecordId.ToString(), ExpectVisible);
        }

        public PersonalMoneyAccountDetailsPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public PersonalMoneyAccountDetailsPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(searchField);
            SendKeys(searchField, SearchQuery);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PersonalMoneyAccountDetailsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public PersonalMoneyAccountDetailsPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignButton);
            Click(assignButton);

            return this;
        }

        public PersonalMoneyAccountDetailsPage ClickCancelButton()
        {
            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);

            return this;
        }

        public PersonalMoneyAccountDetailsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCell(RecordID, 2));
            Click(recordCell(RecordID, 2));

            return this;
        }

        public PersonalMoneyAccountDetailsPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public PersonalMoneyAccountDetailsPage ValidateHeaderCellText(int cellPosition, string ExpectedText)
        {
            WaitForElement(GridHeaderCell(cellPosition, ExpectedText));
            
            return this;
        }


    }
}