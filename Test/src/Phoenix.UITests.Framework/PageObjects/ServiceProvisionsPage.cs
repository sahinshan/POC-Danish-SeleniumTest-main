
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServiceProvisionsPage : CommonMethods
    {
        public ServiceProvisionsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        #region Top Menu

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CWDialogIFrame_Provider = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=provider&')]");
        By CWDialogIFrame(string parentRecordIdSuffix) => By.XPath("//iframe[contains(@id, 'iframe_CWDialog_" + parentRecordIdSuffix + "')]");
        readonly By RelatedRecordsPanelIFrame = By.Id("CWUrlPanel_IFrame");

        readonly By ViewsPicklist = By.Id("CWViewSelector");
        readonly By QuickSearchTextBox = By.Id("CWQuickSearch");
        readonly By QuickSearchButton = By.Id("CWQuickSearchButton");
        readonly By RefreshButton = By.Id("CWRefreshButton");

        readonly By BackButton = By.XPath("//button[@title = 'Back']");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");

        #endregion

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Service Provisions']");

        readonly By NoRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By ServiceProvisionRow(string serviceProvisionId) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + serviceProvisionId + "']/td[2]");

        public ServiceProvisionsPage WaitForServiceProvisionsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(pageHeader);

            WaitForElementVisible(ViewsPicklist);
            WaitForElementVisible(QuickSearchTextBox);
            WaitForElementVisible(QuickSearchButton);
            WaitForElementVisible(RefreshButton);

            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);

            return this;
        }

        public ServiceProvisionsPage WaitForServiceProvisionsPageToLoad(string parentRecordIdSuffix)
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(CWDialogIFrame(parentRecordIdSuffix));
            SwitchToIframe(CWDialogIFrame(parentRecordIdSuffix));

            WaitForElement(RelatedRecordsPanelIFrame);
            SwitchToIframe(RelatedRecordsPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(pageHeader);

            WaitForElementVisible(ViewsPicklist);
            WaitForElementVisible(QuickSearchTextBox);
            WaitForElementVisible(QuickSearchButton);
            WaitForElementVisible(RefreshButton);

            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);

            return this;
        }

        public ServiceProvisionsPage SearchServiceProvisionRecord(string SearchQuery, string serviceProvisionId)
        {

            WaitForElement(QuickSearchTextBox);
            SendKeys(QuickSearchTextBox, SearchQuery);

            WaitForElementToBeClickable(QuickSearchButton);
            MoveToElementInPage(QuickSearchButton);
            Click(QuickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);
            WaitForElement(ServiceProvisionRow(serviceProvisionId));

            return this;
        }

        public ServiceProvisionsPage OpenServiceProvisionRecord(string serviceProvisionId)
        {
            WaitForElementToBeClickable(ServiceProvisionRow(serviceProvisionId));
            MoveToElementInPage(ServiceProvisionRow(serviceProvisionId));
            Click(ServiceProvisionRow(serviceProvisionId));

            return new ServiceProvisionsPage(this.driver, this.Wait, this.appURL);
        }

        public ServiceProvisionsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            MoveToElementInPage(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

    }
}
