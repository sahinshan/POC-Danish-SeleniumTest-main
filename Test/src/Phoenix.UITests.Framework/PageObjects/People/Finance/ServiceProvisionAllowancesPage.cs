using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServiceProvisionAllowancesPage : CommonMethods
    {
        public ServiceProvisionAllowancesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovision&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWNavItem_PersonCaseNoteFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[contains(text(),'Allowances')]");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By UpdateGLCodeButton = By.Id("TI_UpdateGLCode");

        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");



        public ServiceProvisionAllowancesPage WaitForServiceProvisionAllowancesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonCaseNoteFrame);
            SwitchToIframe(CWNavItem_PersonCaseNoteFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(NewRecordButton);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);

            return this;
        }

        public ServiceProvisionAllowancesPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);


            return this;
        }

        public ServiceProvisionAllowancesPage OpenRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));
            return this;
        }

        public ServiceProvisionAllowancesPage OpenRecord(Guid RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId.ToString()));
            Click(recordRow(RecordId.ToString()));
            return this;
        }

        public ServiceProvisionAllowancesPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ServiceProvisionAllowancesPage SelectRecord(Guid RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId.ToString()));
            Click(recordRowCheckBox(RecordId.ToString()));

            return this;
        }

        public ServiceProvisionAllowancesPage ClickUpdateGLCodeButton()
        {
            WaitForElementToBeClickable(AdditionalItemsButton);
            Click(AdditionalItemsButton);

            WaitForElementToBeClickable(UpdateGLCodeButton);
            Click(UpdateGLCodeButton);

            return this;
        }



    }
}
