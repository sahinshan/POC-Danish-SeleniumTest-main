using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the Business Object Record > Data Forms grid page    
    /// </summary>
    public class DataFormsPage : CommonMethods
    {
        public DataFormsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=businessobject&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1");

        #region Options Toolbar
        readonly By BackButton = By.XPath("//button[@title='Back']");
        #endregion

        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordIdentifier(string recordID) => By.XPath("//tr[@id='" + recordID + "']/td[2]");
        By recordRow(string businessObjectFieldName) => By.XPath("//td[@title = '"+businessObjectFieldName+"']");



        public DataFormsPage WaitForDataFormsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElement(pagehehader);

            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        public DataFormsPage InsertQuickSearchText(string Text)
        {
            SendKeys(quickSearchTextBox, Text);

            return this;
        }

        public DataFormsPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public DataFormsPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordIdentifier(RecordID));
            Click(recordIdentifier(RecordID));

            return this;
        }

        public DataFormsPage SearchAndOpenRecord(string Text)
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

        public DataFormsPage ClickBackButton()
        {
            MoveToElementInPage(BackButton);            
            MoveToElementInPage(BackButton);
            Click(BackButton);
            return this;
        }

        public DataFormsPage ValidateRecordIsPresent(string RecordID)
        {
            WaitForElementVisible(recordIdentifier(RecordID));            

            return this;
        }

        public DataFormsPage ValidateRecordIsNotPresent(string RecordID)
        {
            WaitForElementNotVisible(recordIdentifier(RecordID), 3);

            return this;
        }
    }
}
