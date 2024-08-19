using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonRecordsOfDNARPage : CommonMethods
    {
        public PersonRecordsOfDNARPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWNavItem_PersonDNARFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Records of DNAR']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        
        readonly By viewSelector = By.Id("CWViewSelector");



        By recordsAreaHeaderCell(int CellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + CellPosition + "]/a");

        By recordRowPosition(string RecordID, int ExpectedPosition) => By.XPath("//tr[" + ExpectedPosition + "][@id='" + RecordID + "']/td/input");
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        readonly By noRecorrdsMainMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noRecordsSubMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        readonly By ViewOptionSelected = By.Id("CWViewSelector");







        public PersonRecordsOfDNARPage WaitForPersonRecordsOfDNARPageToLoad()
        {
            this.SwitchToDefaultFrame();            

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonDNARFrame);
            SwitchToIframe(CWNavItem_PersonDNARFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(pageHeader);

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
            ValidateElementText(recordsAreaHeaderCell(2), "Person");
            //MoveToElementInPage(recordsAreaHeaderCell(3));
            //ValidateElementText(recordsAreaHeaderCell(3), "Responsible Team");
            //MoveToElementInPage(recordsAreaHeaderCell(4));
            //ValidateElementText(recordsAreaHeaderCell(4), "Date/Time of DNAR");
            //MoveToElementInPage(recordsAreaHeaderCell(5));
            //ValidateElementText(recordsAreaHeaderCell(5), "Review DNAR");
            //MoveToElementInPage(recordsAreaHeaderCell(6));
            //ValidateElementText(recordsAreaHeaderCell(6), "Review Date");
            //MoveToElementInPage(recordsAreaHeaderCell(7));
            //ValidateElementText(recordsAreaHeaderCell(7), "DNAR Order Completed By");
            //MoveToElementInPage(recordsAreaHeaderCell(8));
            //ValidateElementText(recordsAreaHeaderCell(8), "Senior Responsible Clinician with oversight");
            //MoveToElementInPage(recordsAreaHeaderCell(9));
            //ValidateElementText(recordsAreaHeaderCell(9), "Cancelled Decision");
            //MoveToElementInPage(recordsAreaHeaderCell(10));
            //ValidateElementText(recordsAreaHeaderCell(10), "Cancelled On");
            //MoveToElementInPage(recordsAreaHeaderCell(11));
            //ValidateElementText(recordsAreaHeaderCell(11), "Review/Additional Comments");

            return this;
        }
        public PersonRecordsOfDNARPage WaitForPersonRecordsOfDNARPageToLoadEmpty()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonDNARFrame);
            SwitchToIframe(CWNavItem_PersonDNARFrame);

            WaitForElement(pageHeader);

            WaitForElement(recordsAreaHeaderCell(2));
            WaitForElement(recordsAreaHeaderCell(3));
            WaitForElement(recordsAreaHeaderCell(4));
            WaitForElement(recordsAreaHeaderCell(5));
            WaitForElement(recordsAreaHeaderCell(6));

            ValidateElementText(recordsAreaHeaderCell(2), "Date/Time of DNAR".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(3), "Review DNAR".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(4), "Review Date".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(5), "DNAR Order Completed By".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(6), "Date/Time Completed".ToUpper());

            return this;
        }
        public PersonRecordsOfDNARPage WaitForPersonRecordsOfDNARPageToLoadAfterSearch()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonDNARFrame);
            SwitchToIframe(CWNavItem_PersonDNARFrame);

            WaitForElement(pageHeader);

            WaitForElement(recordsAreaHeaderCell(2));
            WaitForElement(recordsAreaHeaderCell(3));
            WaitForElement(recordsAreaHeaderCell(4));
            WaitForElement(recordsAreaHeaderCell(5));
            WaitForElement(recordsAreaHeaderCell(6));
            WaitForElement(recordsAreaHeaderCell(7));

            ValidateElementText(recordsAreaHeaderCell(2), "Date/Time of DNAR".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(3), "Review DNAR".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(4), "Review Date".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(5), "DNAR Order Completed By".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(6), "Date/Time Completed".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(7), "Senior Responsible Clinician with oversight".ToUpper());

            return this;
        }



        public PersonRecordsOfDNARPage ValidateRecordPositionVisible(string RecordID, int ExpectedPosition)
        {
            WaitForElementVisible(recordRowPosition(RecordID, ExpectedPosition));

            return this;
        }

        public PersonRecordsOfDNARPage ValidateRecordVisible(string RecordID)
        {
            WaitForElementVisible(recordRow(RecordID));

            return this;
        }
        public PersonRecordsOfDNARPage ValidateRecordNotVisible(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 3);

            return this;
        }
        public PersonRecordsOfDNARPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }
        public PersonRecordsOfDNARPage ValidateNoRecordsMessageVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
            {
                WaitForElementVisible(noRecorrdsMainMessage);
                WaitForElementVisible(noRecordsSubMessage);
            }
            else
            {
                WaitForElementNotVisible(noRecorrdsMainMessage, 3);
                WaitForElementNotVisible(noRecordsSubMessage, 3);
            }

            return this;
        }


        public PersonRecordsOfDNARPage SearchRecordsOfDNARRecord(string SearchQuery, string recordID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(recordID));

            return this;
        }
        public PersonRecordsOfDNARPage SearchRecordsOfDNARRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }


        public PersonRecordsOfDNARPage OpenRecordsOfDNARRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }



        public PersonRecordsOfDNARPage SelectRecordsOfDNARRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

       
        public PersonRecordsOfDNARPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }
        public PersonRecordsOfDNARPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public PersonRecordsOfDNARPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public PersonRecordsOfDNARPage SelectView(string ElementToSelect)
        {
            WaitForElementToBeClickable(viewSelector);
            SelectPicklistElementByText(viewSelector, ElementToSelect);

            return this;
        }

        public PersonRecordsOfDNARPage validateViewDropDown(String ExpectedText)
        {

            WaitForElementVisible(ViewOptionSelected);
            ValidatePicklistContainsElementByText(ViewOptionSelected, ExpectedText);

            return this;

        }
    }
}
