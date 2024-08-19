using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
   public class ModulesObjectPage : CommonMethods
    {
        public ModulesObjectPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By recordCheckListItem(string recordID) => By.XPath("//div[@id = 'CWCheckList']/ul/li/*[@id='"+recordID+"'][1]");

        public ModulesObjectPage WaitForModulesObjectsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(pageHeader);

            this.ValidateElementTextContainsText(pageHeader, "Modules");

            return this;
        }

        public ModulesObjectPage InsertQuickSearchText(string Text)
        {
            this.SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public ModulesObjectPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ModulesObjectPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCheckListItem(RecordID));
            Click(recordCheckListItem(RecordID));

            return this;
        }

        public ModulesObjectPage ValidateBusinessModuleRecordActive(string RecordID)
        {
            MoveToElementInPage(recordCheckListItem(RecordID));
            ValidateElementChecked(recordCheckListItem(RecordID));
            return this;
        }

        public ModulesObjectPage ValidateBusinessModuleRecordInactive(string RecordID)
        {
            MoveToElementInPage(recordCheckListItem(RecordID));
            ValidateElementNotChecked(recordCheckListItem(RecordID));
            return this;
        }

    }
}
