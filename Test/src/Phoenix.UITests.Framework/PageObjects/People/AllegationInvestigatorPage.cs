using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class AllegationInvestigatorPage : CommonMethods
    {
        public AllegationInvestigatorPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personAllegationIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=allegation&')]");
        readonly By AllegationInvestigatorIFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Allegation Investigators']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

       
        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By search_Field = By.Id("CWQuickSearch");
        readonly By search_Button = By.Id("CWQuickSearchButton");
        readonly By AssignButton = By.Id("TI_AssignRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");

        readonly By startDateColumn = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/a/span[2]");

        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        
        By AllegationDate(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[' + RecordPosition + '][@id='" + RecordID + "']");
      
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
       

        public AllegationInvestigatorPage WaitForAllegationInvestigatorPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personAllegationIFrame);
            SwitchToIframe(personAllegationIFrame);

            WaitForElement(AllegationInvestigatorIFrame);
            SwitchToIframe(AllegationInvestigatorIFrame);

            return this;
        }

        public AllegationInvestigatorPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }

        public AllegationInvestigatorPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);
            
            return this;
        }

        public AllegationInvestigatorPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public AllegationInvestigatorPage SelectViewSelector(String TextToSelect)
        {
            SelectPicklistElementByText(viewSelector, TextToSelect);

            return this;
        }

        public AllegationInvestigatorPage SelectAllegationInvestigatorPageRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public AllegationInvestigatorPage OpenAllegationAbuserRecord(string RecordId)
        {
            WaitForElementToBeClickable(AllegationDate(RecordId));
            Click(AllegationDate(RecordId));

            return this;
        }

        public AllegationInvestigatorPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            ScrollToElement(recordPosition(ElementPosition, RecordID));
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public AllegationInvestigatorPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(search_Field);
            SendKeys(search_Field, SearchQuery);
            Click(search_Button);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public AllegationInvestigatorPage ClickAssignButton()
        {
            WaitForElementToBeClickable(AssignButton);
            Click(AssignButton);

            return this;
        }

        public AllegationInvestigatorPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }

    }
}