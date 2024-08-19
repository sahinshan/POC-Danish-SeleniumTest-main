using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FeesPage : CommonMethods
    {
        public FeesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=provider')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[contains(text(),'Fees')]");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By UpdateGLCodeButton = By.Id("TI_UpdateGLCode");

        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");



        public FeesPage WaitForFeesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);

            return this;
        }

        public FeesPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);


            return this;
        }

        public FeesPage OpenRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));
            return this;
        }

        public FeesPage OpenRecord(Guid RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId.ToString()));
            Click(recordRow(RecordId.ToString()));
            return this;
        }

        public FeesPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public FeesPage SelectRecord(Guid RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId.ToString()));
            Click(recordRowCheckBox(RecordId.ToString()));

            return this;
        }

        public FeesPage ClickUpdateGLCodeButton()
        {
            WaitForElementToBeClickable(UpdateGLCodeButton);
            Click(UpdateGLCodeButton);

            return this;
        }




    }
}
