using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonGestationPeriodPage : CommonMethods
    {
        public PersonGestationPeriodPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWPersonGestationPeriodFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Gestation Periods']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

       
        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By search_Field = By.Id("CWQuickSearch");
        readonly By search_Button = By.Id("CWQuickSearchButton");
        readonly By AssignButton = By.Id("TI_AssignRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");

        readonly By startDateColumn = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/a/span[2]");

        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        
        By ChildCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[' + RecordPosition + '][@id='" + RecordID + "']");
        By recordRowPosition(string RecordID, int ExpectedPosition) => By.XPath("//tr[" + ExpectedPosition + "][@id='" + RecordID + "']/td/input");


        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By recordsAreaHeaderCell(int CellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + CellPosition + "]/a");
        public PersonGestationPeriodPage WaitForPersonGestationPeriodPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWPersonGestationPeriodFrame);
            SwitchToIframe(CWPersonGestationPeriodFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Gestation Periods");

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(recordsAreaHeaderCell(2));
            WaitForElement(recordsAreaHeaderCell(3));
            WaitForElement(recordsAreaHeaderCell(4));
            WaitForElement(recordsAreaHeaderCell(5));
            WaitForElement(recordsAreaHeaderCell(6));
            WaitForElement(recordsAreaHeaderCell(7));
            WaitForElement(recordsAreaHeaderCell(8));
            WaitForElement(recordsAreaHeaderCell(9));
            WaitForElement(recordsAreaHeaderCell(10));
           

            MoveToElementInPage(recordsAreaHeaderCell(2));
            ValidateElementText(recordsAreaHeaderCell(2), "Child");
            MoveToElementInPage(recordsAreaHeaderCell(3));
            ValidateElementText(recordsAreaHeaderCell(3), "Number");
            MoveToElementInPage(recordsAreaHeaderCell(4));
            ValidateElementText(recordsAreaHeaderCell(4), "Days/Weeks");
            MoveToElementInPage(recordsAreaHeaderCell(5));
            ValidateElementText(recordsAreaHeaderCell(5), "Start Date");
            MoveToElementInPage(recordsAreaHeaderCell(6));
            ValidateElementText(recordsAreaHeaderCell(6), "End Date");        
           
            return this;
        }


        public PersonGestationPeriodPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }

        public PersonGestationPeriodPage ClickStartDateColumnToSort()
        {
            WaitForElementToBeClickable(startDateColumn);
            Click(startDateColumn);

            return this;

        }


        public PersonGestationPeriodPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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
        public PersonGestationPeriodPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonGestationPeriodPage SelectViewSelector(String TextToSelect)
        {
            SelectPicklistElementByText(viewSelector, TextToSelect);

            return this;
        }

        public PersonGestationPeriodPage SelectPersonGestationPeriodPageRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;


        }
        public PersonGestationPeriodPage OpenGestationPeriodRecord(string RecordId)
        {
            WaitForElement(ChildCell(RecordId));
            Click(ChildCell(RecordId));

            return this;
        }

        public PersonGestationPeriodPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            ScrollToElement(recordPosition(ElementPosition, RecordID));
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public PersonGestationPeriodPage SearchRecord(string SearchQuery)
        {
            WaitForElement(search_Field);
            SendKeys(search_Field, SearchQuery);
            Click(search_Button);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonGestationPeriodPage ClickAssignButton()
        {
            WaitForElementToBeClickable(AssignButton);
            Click(AssignButton);

            return this;
        }

        public PersonGestationPeriodPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }
    }
    }