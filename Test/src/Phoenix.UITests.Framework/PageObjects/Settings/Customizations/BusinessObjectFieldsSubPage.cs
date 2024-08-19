
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the Team Members sub page when accessed via a system user record
    /// Settings - Security - System Users - System User record - Team Member tabs
    /// </summary>
    public class BusinessObjectFieldsSubPage : CommonMethods
    {
        public BusinessObjectFieldsSubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=businessobject&')]");
        readonly By CWRelatedRecordPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Business Object Fields']");

        #region Options Toolbar
        readonly By BackButton = By.XPath("//button[@title='Back']");
        #endregion

        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordIdentifier(string recordID) => By.XPath("//tr[@id='" + recordID + "']/td[2]");
        By recordRow(string businessObjectFieldName) => By.XPath("//td[@title = '"+businessObjectFieldName+"']");



        public BusinessObjectFieldsSubPage WaitForBusinessObjectFieldsSubPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWRelatedRecordPanel_IFrame);
            SwitchToIframe(CWRelatedRecordPanel_IFrame);

            WaitForElement(pagehehader);

            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        public BusinessObjectFieldsSubPage InsertQuickSearchText(string Text)
        {
            this.SendKeys(quickSearchTextBox, Text);

            return this;
        }

        public BusinessObjectFieldsSubPage ClickQuickSearchButton()
        {
            this.Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public BusinessObjectFieldsSubPage OpenRecord(string RecordID)
        {
            this.Click(recordIdentifier(RecordID));

            return this;
        }

        public BusinessObjectFieldsSubPage SearchAndOpenRecord(string Text)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            if (GetElementVisibility(recordRow(Text)))
            {                
                ScrollToElement(recordRow(Text));
                Click(recordRow(Text));
            }
            else
            {
                SendKeys(quickSearchTextBox, Text);
                Click(quickSearchButton);

                WaitForElement(recordRow(Text));
                ScrollToElement(recordRow(Text));
                Click(recordRow(Text));
            }

            return this;
        }
    }
}
