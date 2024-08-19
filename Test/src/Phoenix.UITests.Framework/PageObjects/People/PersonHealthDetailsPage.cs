using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonHealthDetailsPage : CommonMethods
    {
        public PersonHealthDetailsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWPersonHealthDetailFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Health Details']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By refreshButton = By.Id("CWRefreshButton");
       
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By HealthIssueCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By recordsAreaHeaderCell(int CellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + CellPosition + "]/a");
        public PersonHealthDetailsPage WaitForPersonHealthDetailsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWPersonHealthDetailFrame);
            SwitchToIframe(CWPersonHealthDetailFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Person Health Details");

            WaitForElement(recordsAreaHeaderCell(2));
            WaitForElement(recordsAreaHeaderCell(3));
            WaitForElement(recordsAreaHeaderCell(4));
            WaitForElement(recordsAreaHeaderCell(5));
            WaitForElement(recordsAreaHeaderCell(6));
            WaitForElement(recordsAreaHeaderCell(7));
            WaitForElement(recordsAreaHeaderCell(8));
            WaitForElement(recordsAreaHeaderCell(9));
            WaitForElement(recordsAreaHeaderCell(10));
           

            ScrollToElement(recordsAreaHeaderCell(2));
            ValidateElementText(recordsAreaHeaderCell(2), "Health Issue");
            ScrollToElement(recordsAreaHeaderCell(3));
            ValidateElementText(recordsAreaHeaderCell(3), "Start Date");
            ScrollToElement(recordsAreaHeaderCell(4));
            ValidateElementText(recordsAreaHeaderCell(4), "End Date");
            ScrollToElement(recordsAreaHeaderCell(5));
            ValidateElementText(recordsAreaHeaderCell(5), "Diagnosed");
            ScrollToElement(recordsAreaHeaderCell(6));
            ValidateElementText(recordsAreaHeaderCell(6), "Diagnosed Date");
            ScrollToElement(recordsAreaHeaderCell(7));
            ValidateElementText(recordsAreaHeaderCell(7), "Responsible Team");
            ScrollToElement(recordsAreaHeaderCell(8));
            ValidateElementText(recordsAreaHeaderCell(8), "Modified By");
            ScrollToElement(recordsAreaHeaderCell(9));
            ValidateElementText(recordsAreaHeaderCell(9), "Modified On");
            ScrollToElement(recordsAreaHeaderCell(10));
            ValidateElementText(recordsAreaHeaderCell(10), "Created By");
            ScrollToElement(recordsAreaHeaderCell(11));
            ValidateElementText(recordsAreaHeaderCell(11), "Created On");
         

            return this;
        }


        public PersonHealthDetailsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }

        public PersonHealthDetailsPage ClickRefreshPage()
        {            
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            return this;
        }
      
        public PersonHealthDetailsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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
        public PersonHealthDetailsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonHealthDetailsPage SelectViewSelector(String TextToSelect)
        {
            SelectPicklistElementByText(viewSelector, TextToSelect);

            return this;
        }

        public PersonHealthDetailsPage SelectPersonHealthDetailsPageRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }
        public PersonHealthDetailsPage OpenPersonHealthDetailRecord(string RecordId)
        {
            WaitForElement(HealthIssueCell(RecordId));
            Click(HealthIssueCell(RecordId));

            return this;
        }

    }
}