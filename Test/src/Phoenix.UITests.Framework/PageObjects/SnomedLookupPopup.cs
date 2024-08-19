using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SnomedLookupPopup : CommonMethods
    {
        public SnomedLookupPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_CWSnomedBrowserForm");

        readonly By popupHeader = By.XPath("//header[@id='CWHeader']/h1");

        readonly By FilterSelector = By.Id("CWFilterSelector");
        readonly By TermTextbox = By.Id("CWTermSearch");
        readonly By TermSearchButton = By.Id("CWTermSearchButton");
        readonly By TermRemoveButton = By.Id("CWTermClearButton");


        readonly By gridResultsArea = By.XPath("//*[@id='CWResultsList']");

        By resultElement(string TermId) => By.XPath("//*[@id='CWResultsList']/li/div[@termid='" + TermId + "']");


        public SnomedLookupPopup WaitForSnomedLookupPopupToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(popupHeader);

            WaitForElement(TermTextbox);
            WaitForElement(TermTextbox);
            WaitForElement(TermSearchButton);
            WaitForElement(TermRemoveButton);

            WaitForElement(gridResultsArea);

            return this;
        }

        public SnomedLookupPopup WaitForSnomedLookupPopupToLoad(int WaitTimeSeconds)
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(popupHeader);

            WaitForElement(FilterSelector);
            WaitForElement(TermTextbox);
            WaitForElement(TermSearchButton);
            WaitForElement(TermRemoveButton);

            WaitForElement(gridResultsArea, WaitTimeSeconds);

            return this;
        }

        public SnomedLookupPopup WaitForSnomedLookupPopupToLoad(string PopupTitle)
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(popupHeader);

            ValidateElementText(popupHeader, PopupTitle);

            WaitForElement(FilterSelector);
            WaitForElement(TermTextbox);
            WaitForElement(TermSearchButton);
            WaitForElement(TermRemoveButton);

            WaitForElement(gridResultsArea);

            return this;
        }

        public SnomedLookupPopup SelectFilterByText(string TextToSelect)
        {
            SelectPicklistElementByText(FilterSelector, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }



        public SnomedLookupPopup InsertTerm(string TextToInsert)
        {
            SendKeys(TermTextbox, TextToInsert);

            return this;
        }

        public SnomedLookupPopup TapTermSearchButton()
        {
            WaitForElementToBeClickable(TermSearchButton);
            Click(TermSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }       

        public SnomedLookupPopup TapTermRemoveButton(int WaitTime)
        {
            WaitForElementToBeClickable(TermRemoveButton);
            Click(TermRemoveButton);

            WaitForElementNotVisible("CWRefreshPanel", WaitTime);

            return this;
        }

        public SnomedLookupPopup SelectResultElement(string TermId)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(resultElement(TermId));
            Click(resultElement(TermId));

            driver.SwitchTo().ParentFrame();

            return this;
        }



       


    }

}

