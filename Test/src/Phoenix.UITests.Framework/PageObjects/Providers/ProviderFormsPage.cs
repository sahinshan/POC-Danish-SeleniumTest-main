using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ProviderFormsPage : CommonMethods
    {
        public ProviderFormsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By providerRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=provider&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWNavItem_ProviderFormFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Forms (For Provider)']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//*[@id='" + RecordID + "']/td[" + CellPosition + "]");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");



        public ProviderFormsPage WaitForProviderFormsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(providerRecordIFrame);
            SwitchToIframe(providerRecordIFrame);

            WaitForElement(CWNavItem_ProviderFormFrame);
            SwitchToIframe(CWNavItem_ProviderFormFrame);

            WaitForElement(pageHeader);

            WaitForElement(NewRecordButton);
            WaitForElement(ExportToExcelButton);
            WaitForElement(AssignRecordButton);
            WaitForElement(DeleteRecordButton);

            return this;
        }

        public ProviderFormsPage SearchProviderFormRecord(string SearchQuery, string ProviderID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(ProviderID));

            return this;
        }

        public ProviderFormsPage SearchProviderFormRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ProviderFormsPage OpenProviderFormRecord(string RecordId)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public ProviderFormsPage ValidateProviderFormRecordPresent(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            

            return this;
        }

        public ProviderFormsPage ValidateRecordCellText(string RecordId, int CellNumber, string ExpectedText)
        {
            ValidateElementText(recordCell(RecordId, CellNumber), ExpectedText);


            return this;
        }

        public ProviderFormsPage SelectProviderFormRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ProviderFormsPage ClickAddNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }

    }
}
