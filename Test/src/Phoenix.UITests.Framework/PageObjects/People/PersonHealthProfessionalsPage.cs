using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonHealthProfessionalsPage : CommonMethods
    {
        public PersonHealthProfessionalsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); 
        readonly By HealthProfessionalsIFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Health Professionals']");
        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By RefreshButton = By.Id("CWRefreshButton");


        public PersonHealthProfessionalsPage WaitForPersonHealthProfessionalsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(HealthProfessionalsIFrame);
            SwitchToIframe(HealthProfessionalsIFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Person Health Professionals");

            WaitForElement(NewRecordButton);

            return this;
        }

        public PersonHealthProfessionalsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonHealthProfessionalsPage OpenPersonHealthProfesionalsRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

       

        public PersonHealthProfessionalsPage SelectPersonChronologyRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

      
        public PersonHealthProfessionalsPage SelectNewRecordButton()
        {
            
            Click(NewRecordButton);

            return this;
        }

        public PersonHealthProfessionalsPage SelectDeleteRecordButton()
        {
            Click(DeleteRecordButton);

            return this;
        }

        public PersonHealthProfessionalsPage SelectRefreshButton()
        {
            Click(RefreshButton);

            return this;
        }

        public PersonHealthProfessionalsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordLabel);

            }
            else
            {
                WaitForElementNotVisible(NoRecordLabel, 5);
            }
            return this;
        }


    }
}
