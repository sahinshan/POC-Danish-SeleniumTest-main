using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonHealthDisabilityImpairmentsPage : CommonMethods
    {
        public PersonHealthDisabilityImpairmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWPersonDisabilityImpairmentsIFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Disabilities/Impairments']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By Disability_Icon = By.XPath("//*[@id='CWBannerHolder']/ul/li/div/img[@title='Disability']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By recordsAreaHeaderCell(int CellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + CellPosition + "]/a");

        public PersonHealthDisabilityImpairmentsPage WaitForPersonHealthDisabilityImpairmentsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWPersonDisabilityImpairmentsIFrame);
            SwitchToIframe(CWPersonDisabilityImpairmentsIFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Disabilities/Impairments");

           

            return this;
        }

       
        public PersonHealthDisabilityImpairmentsPage SelectPersonDisabilityRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

       

        public PersonHealthDisabilityImpairmentsPage SelectNewRecordButton()
        {

            Click(newRecordButton);

            return this;
        }

        public PersonHealthDisabilityImpairmentsPage SelectDeleteRecordButton()
        {

            Click(DeleteRecordButton);

            return this;
        }

        

        public PersonHealthDisabilityImpairmentsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
          //  ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }
        public PersonHealthDisabilityImpairmentsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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
       


    }
}