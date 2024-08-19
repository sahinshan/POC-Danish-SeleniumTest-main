using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class CareProviderSchedulingSetupPage : CommonMethods
    {
        public CareProviderSchedulingSetupPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By ContentIFrame = By.Id("CWContentIFrame");
        readonly By OrganisationalRiskCategoriesPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Care Provider Scheduling Setup']");

        readonly By viewsPicklist = By.Id("CWViewSelector");

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");


        public CareProviderSchedulingSetupPage WaitForCareProviderSchedulingSetupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(ContentIFrame);
            SwitchToIframe(ContentIFrame);

            WaitForElement(OrganisationalRiskCategoriesPageHeader);

            WaitForElement(NewRecordButton);
            WaitForElement(DeleteRecordButton);
            WaitForElement(viewsPicklist);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        public CareProviderSchedulingSetupPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public CareProviderSchedulingSetupPage SearchCareProviderSchedulingSetupRecord(string SearchQuery)
        {
            WaitForElementVisible(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }
        
        public CareProviderSchedulingSetupPage OpenRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            MoveToElementInPage(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public CareProviderSchedulingSetupPage OpenRecord(Guid RecordId)
        {
            OpenRecord(RecordId.ToString());
            return this;
        }

        public CareProviderSchedulingSetupPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public CareProviderSchedulingSetupPage SelectAvailableViewByText(string PicklistText)
        {
            WaitForElementVisible(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

        public CareProviderSchedulingSetupPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            MoveToElementInPage(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public CareProviderSchedulingSetupPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(noRecordMessage);
            else
                WaitForElementNotVisible(noRecordMessage, 5);
            
            return this;
        }


    }
}
