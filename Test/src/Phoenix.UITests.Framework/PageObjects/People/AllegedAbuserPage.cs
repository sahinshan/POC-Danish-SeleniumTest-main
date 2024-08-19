using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class AllegedAbuserPage : CommonMethods
    {
        public AllegedAbuserPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By AllegedAbuserFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Allegations']");
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
        
        By recordsAreaHeaderCell(int CellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + CellPosition + "]/a");


        public AllegedAbuserPage WaitForAllegedAbuserPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(AllegedAbuserFrame);
            SwitchToIframe(AllegedAbuserFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Allegations");

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
            ValidateElementText(recordsAreaHeaderCell(2), "Allegation Date");
            ScrollToElement(recordsAreaHeaderCell(3));
            ValidateElementText(recordsAreaHeaderCell(3), "Alleged Victim");
            ScrollToElement(recordsAreaHeaderCell(4));
            ValidateElementText(recordsAreaHeaderCell(4), "Alleged Abuser");
            ScrollToElement(recordsAreaHeaderCell(5));
            ValidateElementText(recordsAreaHeaderCell(5), "Primary Category of Abuse");
            ScrollToElement(recordsAreaHeaderCell(6));
            ValidateElementText(recordsAreaHeaderCell(6), "Police have been Notified?");
            ScrollToElement(recordsAreaHeaderCell(7));
            ValidateElementText(recordsAreaHeaderCell(7), "Date Police Notified");
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

        public AllegedAbuserPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }

        public AllegedAbuserPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(NoRecordMessage);
            else
                WaitForElementNotVisible(NoRecordMessage, 5);
            
            return this;
        }
        
        public AllegedAbuserPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public AllegedAbuserPage SelectViewSelector(String TextToSelect)
        {
            SelectPicklistElementByText(viewSelector, TextToSelect);

            return this;
        }

        public AllegedAbuserPage SelectAllegedAbuserPageRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;


        }
        
        public AllegedAbuserPage OpenAllegationAbuserRecord(string RecordId)
        {
            WaitForElementToBeClickable(AllegationDate(RecordId));
            Click(AllegationDate(RecordId));

            return this;
        }

        public AllegedAbuserPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            ScrollToElement(recordPosition(ElementPosition, RecordID));
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }

        public AllegedAbuserPage SearchRecord(string SearchQuery)
        {
            WaitForElement(search_Field);
            SendKeys(search_Field, SearchQuery);
            Click(search_Button);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public AllegedAbuserPage ClickAssignButton()
        {
            WaitForElementToBeClickable(AssignButton);
            Click(AssignButton);

            return this;
        }

        public AllegedAbuserPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        } 

    }
}