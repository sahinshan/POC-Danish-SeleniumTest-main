using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
    public class LocalizedStringValuesPage : CommonMethods
    {
        public LocalizedStringValuesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=localizedstring&')]");
        readonly By CWNavItem_Frame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        public LocalizedStringValuesPage WaitForOptionsetValuesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWNavItem_Frame);
            SwitchToIframe(CWNavItem_Frame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Localized String Values");

            WaitForElement(quickSearchTextbox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        public LocalizedStringValuesPage InsertQuickSearchText(string Text)
        {
            MoveToElementInPage(quickSearchTextbox);
            WaitForElementVisible(quickSearchTextbox);
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public LocalizedStringValuesPage ClickQuickSearchButton()
        {
            MoveToElementInPage(quickSearchButton);
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public LocalizedStringValuesPage OpenRecord(string RecordID)
        {
            MoveToElementInPage(recordRow(RecordID));
            WaitForElementToBeClickable(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }

        public LocalizedStringValuesPage ClickCreateNewRecord()
        {
            MoveToElementInPage(NewRecordButton);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

    }
}
