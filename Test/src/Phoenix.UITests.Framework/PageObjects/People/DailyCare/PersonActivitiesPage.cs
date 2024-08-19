using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonActivitiesPage : CommonMethods
    {
        public PersonActivitiesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CWIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Person Activities']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchField = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");


        By GridHeaderCell(int cellPosition, string ExpectedText) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]//*[text()='" + ExpectedText + "']");


        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        public PersonActivitiesPage WaitForPageToLoad()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWIFrame);
            SwitchToIframe(CWIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(viewSelector);
            WaitForElementVisible(searchField);
            WaitForElementVisible(searchButton);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(newRecordButton);

            return this;
        }


        public PersonActivitiesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }

        public PersonActivitiesPage ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);

            return this;
        }

        public PersonActivitiesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonActivitiesPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
        {
            return ValidateRecordCellText(RecordID.ToString(), CellPosition, ExpectedText);
        }

        public PersonActivitiesPage SelectView(string TextToSelect)
        {
            SelectPicklistElementByText(viewSelector, TextToSelect);

            return this;
        }

        public PersonActivitiesPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonActivitiesPage ValidateRecordPresent(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(recordRowCheckBox(RecordId));
            else
                WaitForElementNotVisible(recordRowCheckBox(RecordId), 3);

            return this;
        }

        public PersonActivitiesPage ValidateRecordPresent(Guid RecordId, bool ExpectVisible)
        {
            return ValidateRecordPresent(RecordId.ToString(), ExpectVisible);
        }

        public PersonActivitiesPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public PersonActivitiesPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(searchField);
            SendKeys(searchField, SearchQuery);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PersonActivitiesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public PersonActivitiesPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignButton);
            Click(assignButton);

            return this;
        }

        public PersonActivitiesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCell(RecordID, 2));
            Click(recordCell(RecordID, 2));

            return this;
        }

        public PersonActivitiesPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public PersonActivitiesPage ValidateHeaderCellText(int cellPosition, string ExpectedText)
        {
            WaitForElement(GridHeaderCell(cellPosition, ExpectedText));
            
            return this;
        }


    }
}