using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

   
    public class OrganisationalRisksPage : CommonMethods
    {
        public OrganisationalRisksPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region IFrame
        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        

        #endregion IFrame

        #region Fields and labels

        readonly By pageheader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Organisational Risks']");
        readonly By addNewRecordButton = By.Id("TI_NewRecordButton"); 
        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch"); 
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By refreshButton = By.Id("CWRefreshButton");


        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");



        readonly By deleteButton = By.Id("TI_DeleteRecordButton"); 
        readonly By selectAllRecord = By.Id("cwgridheaderselector");

        



        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        By recordCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");
       
        #endregion Fields and labels

        public OrganisationalRisksPage WaitForOrganisationalRisksPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(pageheader);

            WaitForElement(addNewRecordButton);

            WaitForElement(viewsPicklist);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

       
        public OrganisationalRisksPage SwitchToDynamicsDialogLevelIframe()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);


            return this;
        }

       
        public OrganisationalRisksPage SelectRecord(string RecordID)
        {
            this.Click(recordCheckBox(RecordID));

            return this;
        }

        public OrganisationalRisksPage ClickAddNewButton()
        {
            this.WaitForElement(addNewRecordButton);
            this.Click(addNewRecordButton);

            return new OrganisationalRisksPage(driver, Wait, appURL);
        }

        public OrganisationalRisksPage ClickSelectAllCheckBox()
        {
            this.WaitForElement(selectAllRecord);
            this.Click(selectAllRecord);

            return new OrganisationalRisksPage(driver, Wait, appURL);
        }



        public OrganisationalRisksPage OpenRecord(string RecordID)
        {
            this.Click(recordRow(RecordID));

            return this;
        }

    



    
        public OrganisationalRisksPage ClickExportToExcel()
        {
            Click(exportToExcelButton);

            return this;
        }

     



        public OrganisationalRisksPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public OrganisationalRisksPage ValidatePageTitle(string ExpectedText)
        {
            WaitForElementToContainText(pageheader, ExpectedText);
            ValidateElementText(pageheader, ExpectedText);

            return this;
        }

        public OrganisationalRisksPage InsertQuickSearchText(string TextToInsert)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, TextToInsert);

            return this;
        }

        public OrganisationalRisksPage ClickQuickSearchButton()
        {
            WaitForElement(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

      
        public OrganisationalRisksPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }
    }

}