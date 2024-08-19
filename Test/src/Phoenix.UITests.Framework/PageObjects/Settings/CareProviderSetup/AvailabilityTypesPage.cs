using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class AvailabilityTypesPage : CommonMethods
    {
        public AvailabilityTypesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Toolbar and Search View Panel
        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton"); 
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By viewSelector = By.Id("CWViewSelector");
        #endregion

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        public AvailabilityTypesPage WaitForAvailabilityTypesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 50);

            WaitForElement(pageHeader);

            WaitForElement(newRecordButton);
            WaitForElement(deleteRecordButton);
            WaitForElement(viewSelector);
            WaitForElement(quickSearchTextbox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        public AvailabilityTypesPage ClickNewRecordButton()
        {
            MoveToElementInPage(newRecordButton);
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public AvailabilityTypesPage InsertQuickSearchText(string Text)
        {
            MoveToElementInPage(quickSearchTextbox);
            WaitForElementToBeClickable(quickSearchTextbox);
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public AvailabilityTypesPage ClickQuickSearchButton()
        {
            MoveToElementInPage(quickSearchButton);
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public AvailabilityTypesPage ClickRefreshButton()
        {            
            MoveToElementInPage(refreshButton);
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public AvailabilityTypesPage ClickDeleteButton()
        {
            MoveToElementInPage(deleteRecordButton);
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }

        public AvailabilityTypesPage OpenRecord(string RecordID)
        {            
            WaitForElementToBeClickable(recordRow(RecordID));
            MoveToElementInPage(recordRow(RecordID));
            Click(recordRow(RecordID));
            return this;
        }

        public AvailabilityTypesPage ValidateAvailabilityTypeRecordIsPresent(string RecordID, bool IsPresent)
        {
            if (IsPresent)
            {
                WaitForElementToBeClickable(recordRow(RecordID));
                MoveToElementInPage(recordRow(RecordID));                
            }
            else
            {
                WaitForElementNotVisible(recordRow(RecordID), 3);
            }
            bool actualStatus = GetElementVisibility(recordRow(RecordID));
            Assert.AreEqual(IsPresent, actualStatus);

            return this;
        }
    }
}
