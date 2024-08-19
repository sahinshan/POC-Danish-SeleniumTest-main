using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonPhysicalObservationsPage : CommonMethods
    {
        public PersonPhysicalObservationsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']");
        readonly By activitiesLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_Activities']/a");
        readonly By caseNotestLeftSubMenuItem = By.Id("CWNavItem_PersonPhysicalObservationCaseNote");


        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWNavItem_PhysicalObservationFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Physical Observations']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By viewsPicklist = By.XPath("//*[@id='CWViewSelector']");
        readonly By searchField = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By GeneratePhysicalObservationChartButton = By.Id("TI_HealthChartButton");

        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");


        By GridHeaderCell(int cellPosition, string ExpectedText) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]//*[text()='" + ExpectedText + "']");
        By GridHeaderCell(string ExpectedText) => By.XPath("//*[@id='CWGridHeaderRow']/th//*[text()='" + ExpectedText + "']");


        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");



        public PersonPhysicalObservationsPage WaitForPersonPhysicalObservationsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PhysicalObservationFrame);
            SwitchToIframe(CWNavItem_PhysicalObservationFrame);

            WaitForElement(pageHeader);

            WaitForElement(NewRecordButton);

            return this;
        }


        public PersonPhysicalObservationsPage SelectView(string ElementTextToSelect)
        {
            WaitForElementToBeClickable(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, ElementTextToSelect);

            return this;
        }

        public PersonPhysicalObservationsPage OpenPersonPhysicalObservationRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public PersonPhysicalObservationsPage NavigateToPersonPhysicalObservationCaseNotesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(activitiesLeftSubMenu);
            Click(activitiesLeftSubMenu);

            WaitForElementToBeClickable(caseNotestLeftSubMenuItem);
            Click(caseNotestLeftSubMenuItem);

            return this;
        }

        public PersonPhysicalObservationsPage SelectPersonPhysicalObservationRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonPhysicalObservationsPage TapGeneratePhysicalObservationChartButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);
            Click(GeneratePhysicalObservationChartButton);

            return this;
        }

        public PersonPhysicalObservationsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;

        }

        public PersonPhysicalObservationsPage ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);

            return this;
        }

        public PersonPhysicalObservationsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonPhysicalObservationsPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
        {
            return ValidateRecordCellText(RecordID.ToString(), CellPosition, ExpectedText);
        }

        public PersonPhysicalObservationsPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonPhysicalObservationsPage ValidateRecordPresent(string RecordId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(recordRowCheckBox(RecordId));
            else
                WaitForElementNotVisible(recordRowCheckBox(RecordId), 3);

            return this;
        }

        public PersonPhysicalObservationsPage ValidateRecordPresent(Guid RecordId, bool ExpectVisible)
        {
            return ValidateRecordPresent(RecordId.ToString(), ExpectVisible);
        }

        public PersonPhysicalObservationsPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public PersonPhysicalObservationsPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(searchField);
            SendKeys(searchField, SearchQuery);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PersonPhysicalObservationsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public PersonPhysicalObservationsPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignButton);
            Click(assignButton);

            return this;
        }

        public PersonPhysicalObservationsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCell(RecordID, 2));
            Click(recordCell(RecordID, 2));

            return this;
        }

        public PersonPhysicalObservationsPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public PersonPhysicalObservationsPage ValidateHeaderCellText(int cellPosition, string ExpectedText)
        {
            WaitForElement(GridHeaderCell(cellPosition, ExpectedText));

            return this;
        }

        public PersonPhysicalObservationsPage ValidateHeaderCellIsDisplayed(string ExpectedText, bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
                WaitForElement(GridHeaderCell(ExpectedText));
            else
                WaitForElementNotVisible(GridHeaderCell(ExpectedText), 3);

            return this;
        }
    }
}
