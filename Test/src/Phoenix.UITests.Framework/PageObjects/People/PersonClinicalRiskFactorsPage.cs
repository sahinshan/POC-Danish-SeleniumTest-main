using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonClinicalRiskFactorsPage : CommonMethods
    {
        public PersonClinicalRiskFactorsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWNavItem_PersonClinicalRiskFactorFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Clinical Risk Factors']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");



        By recordsAreaHeaderCell(int CellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + CellPosition + "]/a");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        readonly By noRecorrdsMainMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noRecordsSubMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        
        





        public PersonClinicalRiskFactorsPage WaitForPersonClinicalRiskFactorsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonClinicalRiskFactorFrame);
            SwitchToIframe(CWNavItem_PersonClinicalRiskFactorFrame);

            WaitForElementVisible(pageHeader);

            ValidateElementText(pageHeader, "Clinical Risk Factors");

            WaitForElementVisible(recordsAreaHeaderCell(2));
            WaitForElement(recordsAreaHeaderCell(3));
            WaitForElement(recordsAreaHeaderCell(4));
            WaitForElement(recordsAreaHeaderCell(5));
            WaitForElement(recordsAreaHeaderCell(6));
            WaitForElement(recordsAreaHeaderCell(7));
            WaitForElement(recordsAreaHeaderCell(8));
            WaitForElement(recordsAreaHeaderCell(9));
            WaitForElement(recordsAreaHeaderCell(10));
            WaitForElement(recordsAreaHeaderCell(11));
            WaitForElement(recordsAreaHeaderCell(12));

            ScrollToElement(recordsAreaHeaderCell(2));
            ValidateElementText(recordsAreaHeaderCell(2), "Risk Factor Type");
            ScrollToElement(recordsAreaHeaderCell(3));
            ValidateElementText(recordsAreaHeaderCell(3), "Risk Factor Sub-Type");
            ScrollToElement(recordsAreaHeaderCell(4));
            ValidateElementText(recordsAreaHeaderCell(4), "Risk Level");
            ScrollToElement(recordsAreaHeaderCell(5));
            ValidateElementText(recordsAreaHeaderCell(5), "Date Identified");
            ScrollToElement(recordsAreaHeaderCell(6));
            ValidateElementText(recordsAreaHeaderCell(6), "Sensitive Information");
            ScrollToElement(recordsAreaHeaderCell(7));
            ValidateElementText(recordsAreaHeaderCell(7), "End Date");
            ScrollToElement(recordsAreaHeaderCell(8));
            ValidateElementText(recordsAreaHeaderCell(8), "Responsible Team");
            ScrollToElement(recordsAreaHeaderCell(9));
            ValidateElementText(recordsAreaHeaderCell(9), "Modified On");
            ScrollToElement(recordsAreaHeaderCell(10));
            ValidateElementText(recordsAreaHeaderCell(10), "Modified By");
            ScrollToElement(recordsAreaHeaderCell(11));
            ValidateElementText(recordsAreaHeaderCell(11), "Created On");
            ScrollToElement(recordsAreaHeaderCell(12));
            ValidateElementText(recordsAreaHeaderCell(12), "Created By");

            return this;
        }
        public PersonClinicalRiskFactorsPage WaitForPersonClinicalRiskFactorsPageToLoadEmpty()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonClinicalRiskFactorFrame);
            SwitchToIframe(CWNavItem_PersonClinicalRiskFactorFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Clinical Risk Factors");

            WaitForElement(recordsAreaHeaderCell(2));
            WaitForElement(recordsAreaHeaderCell(3));
            WaitForElement(recordsAreaHeaderCell(4));
            WaitForElement(recordsAreaHeaderCell(5));
            WaitForElement(recordsAreaHeaderCell(6));
            WaitForElement(recordsAreaHeaderCell(7));
            WaitForElement(recordsAreaHeaderCell(8));
            WaitForElement(recordsAreaHeaderCell(9));
            WaitForElement(recordsAreaHeaderCell(10));
            WaitForElement(recordsAreaHeaderCell(11));
            WaitForElement(recordsAreaHeaderCell(12));

            ValidateElementText(recordsAreaHeaderCell(2), "Risk Factor Type".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(3), "Risk Factor Sub-Type".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(4), "Risk Level".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(5), "Date Identified".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(6), "Sensitive Information".ToUpper());

            return this;
        }
        public PersonClinicalRiskFactorsPage WaitForPersonClinicalRiskFactorsPageToLoadAfterSearch()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonClinicalRiskFactorFrame);
            SwitchToIframe(CWNavItem_PersonClinicalRiskFactorFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Clinical Risk Factors");

            WaitForElement(recordsAreaHeaderCell(2));
            WaitForElement(recordsAreaHeaderCell(3));
            WaitForElement(recordsAreaHeaderCell(4));
            WaitForElement(recordsAreaHeaderCell(5));
            WaitForElement(recordsAreaHeaderCell(6));

            ValidateElementText(recordsAreaHeaderCell(2), "Person".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(3), "Risk Factor Type".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(4), "Risk Factor Sub-Type".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(5), "Risk Level".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(6), "Date Identified".ToUpper());
            ValidateElementText(recordsAreaHeaderCell(7), "Sensitive Information".ToUpper());

            return this;
        }
        public PersonClinicalRiskFactorsPage WaitForBulkEditButtonVisible()
        {
            WaitForElementVisible(BulkEditButton);

            return this;
        }


        public PersonClinicalRiskFactorsPage ValidateRecordVisible(string RecordID)
        {
            WaitForElementVisible(recordRow(RecordID));

            return this;
        }
        public PersonClinicalRiskFactorsPage ValidateRecordNotVisible(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 3);

            return this;
        }
        public PersonClinicalRiskFactorsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorsPage ValidateNoRecordsMessageVisibility(bool ExpectVisible)
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


        public PersonClinicalRiskFactorsPage SearchPersonClinicalRiskFactorRecord(string SearchQuery, string recordID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(recordID));

            return this;
        }
        public PersonClinicalRiskFactorsPage SearchPersonClinicalRiskFactorRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }


        public PersonClinicalRiskFactorsPage OpenPersonClinicalRiskFactorRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }



        public PersonClinicalRiskFactorsPage SelectPersonClinicalRiskFactorRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        



        public PersonClinicalRiskFactorsPage ClickBulkEditButton()
        {
            WaitForElementToBeClickable(BulkEditButton);
            Click(BulkEditButton);

            return this;
        }
        public PersonClinicalRiskFactorsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }
        public PersonClinicalRiskFactorsPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }
    }
}
