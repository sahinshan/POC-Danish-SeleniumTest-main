using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ProvidersPage : CommonMethods
    {
        public ProvidersPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By ProvidersPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Providers']");

        readonly By ProviderIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=provider&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");
        readonly By Childproviders_IFrame = By.Id("iframe_childproviders");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.XPath("//button[@id='CWRefreshButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        
        By ProviderRow(string ProviderID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ProviderID + "']/td[2]");
        By ProviderRowCheckBox(string ProviderID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ProviderID + "']/td[1]/input");

        By ProviderRecordRow(string ProviderID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ProviderID + "']");


        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By mailMergeButton = By.Id("TI_MailMergeButton");
        readonly By bulkEditButton = By.Id("TI_BulkEditButton");




        public ProvidersPage WaitForProvidersPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(ProvidersPageHeader);
            WaitForElement(NewRecordButton);

            return this;
        }

        public ProvidersPage WaitForChildProvidersPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(ProviderIFrame);
            SwitchToIframe(ProviderIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(Childproviders_IFrame);
            SwitchToIframe(Childproviders_IFrame);

            WaitForElement(ProvidersPageHeader);
            WaitForElement(NewRecordButton);

            return this;
        }

        public ProvidersPage SearchProviderRecord(string SearchQuery, string ProviderID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);
            WaitForElementToBeClickable(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(ProviderRow(ProviderID));

            return this;
        }

        public ProvidersPage SearchProviderRecord(string SearchQuery, Guid ProviderID)
        {
            return SearchProviderRecord(SearchQuery, ProviderID.ToString());
        }


        public ProvidersPage SearchProviderRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public ProvidersPage OpenHospitalRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public ProvidersPage OpenProviderRecord(string ProviderId)
        {
            WaitForElementToBeClickable(ProviderRow(ProviderId));
            Click(ProviderRow(ProviderId));

            return this;
        }

        public ProvidersPage OpenProviderRecord(Guid ProviderId)
        {
            return OpenProviderRecord(ProviderId.ToString());
        }

        public ProvidersPage SelectProviderRecord(string ProviderId)
        {
            WaitForElement(ProviderRowCheckBox(ProviderId));
            Click(ProviderRowCheckBox(ProviderId));

            return this;
        }

        public ProvidersPage SelectProviderRecord(Guid ProviderId)
        {
            return SelectProviderRecord(ProviderId.ToString());
        }

        public ProvidersPage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ProvidersPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public ProvidersPage ClickMailMergeButton()
        {
            WaitForElementToBeClickable(mailMergeButton);
            Click(mailMergeButton);

            return this;
        }

        public ProvidersPage ClickBulkEditButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(bulkEditButton);
            Click(bulkEditButton);

            return this;
        }

        public ProvidersPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            ScrollToElement(refreshButton);
            Click(refreshButton);

            return this;
        }

        public ProvidersPage ValidateProviderRecordIsPresent(string ProviderId, bool IsPresent)
        {
            if (IsPresent)
                WaitForElementVisible(ProviderRecordRow(ProviderId));
            else
                WaitForElementNotVisible(ProviderRecordRow(ProviderId), 3);

            return this;
        }

    }
}
