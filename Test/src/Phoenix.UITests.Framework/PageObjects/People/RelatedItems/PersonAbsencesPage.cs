using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonAbsencesPage : CommonMethods
    {
        public PersonAbsencesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersoncontract&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");
        readonly By iframe_cppersoncontractcppersonabsence = By.Id("iframe_cppersoncontractcppersonabsence");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Person Absences']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By searchField = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");


        readonly By GridHeaderIdField = By.XPath("//*[@id='CWGridHeaderRow']//a[@title='Sort by Id']");


        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        
        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");
      
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
       

        public PersonAbsencesPage WaitForPersonAbsencesPageToLoadInsidePersonContractPage()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElement(iframe_cppersoncontractcppersonabsence);
            SwitchToIframe(iframe_cppersoncontractcppersonabsence);

            WaitForElementVisible(viewSelector);
            WaitForElementVisible(searchField);
            WaitForElementVisible(searchButton);

            WaitForElementVisible(pageHeader);

            WaitForElementNotVisible(newRecordButton, 3);

            return this;
        }

        public PersonAbsencesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }

        public PersonAbsencesPage ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);
            
            return this;
        }

        public PersonAbsencesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonAbsencesPage SelectViewSelector(string TextToSelect)
        {
            SelectPicklistElementByText(viewSelector, TextToSelect);

            return this;
        }

        public PersonAbsencesPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonAbsencesPage ValidateRecordPresent(string RecordId, bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(recordRowCheckBox(RecordId));
            else
                WaitForElementNotVisible(recordRowCheckBox(RecordId), 3);

            return this;
        }

        public PersonAbsencesPage ValidateRecordPresent(Guid RecordId, bool ExpectVisible)
        {
            return ValidateRecordPresent(RecordId.ToString(), ExpectVisible);
        }

        public PersonAbsencesPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            ScrollToElement(recordPosition(ElementPosition, RecordID));
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public PersonAbsencesPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(searchField);
            SendKeys(searchField, SearchQuery);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PersonAbsencesPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public PersonAbsencesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCell(RecordID, 2));
            Click(recordCell(RecordID, 2));

            return this;
        }

        public PersonAbsencesPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public PersonAbsencesPage GridHeaderIdFieldLink()
        {
            WaitForElementToBeClickable(GridHeaderIdField);
            Click(GridHeaderIdField);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

    }
}