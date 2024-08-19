using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
    public class LocalizedStringsPage : CommonMethods
    {
        public LocalizedStringsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By ExportTranslationsButton = By.Id("TI_ExportTranslationsButton");
        readonly By ImportTranslationsButton = By.Id("TI_ImportTranslationsButton");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        public LocalizedStringsPage WaitForLocalizedStringsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Localized Strings");

            WaitForElement(ExportTranslationsButton);
            WaitForElement(ImportTranslationsButton);

            WaitForElement(quickSearchTextbox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        public LocalizedStringsPage InsertQuickSearchText(string Text)
        {
            WaitForElementVisible(quickSearchTextbox);
            MoveToElementInPage(quickSearchTextbox);
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public LocalizedStringsPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public LocalizedStringsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRow(RecordID));
            MoveToElementInPage(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }


    }
}
