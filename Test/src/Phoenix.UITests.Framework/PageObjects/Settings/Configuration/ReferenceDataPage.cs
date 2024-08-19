using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReferenceDataPage : CommonMethods
    {
        public ReferenceDataPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//header[@id='CWHeader']/h1");

        readonly By searchTextBox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");

        By referenceDataMainHeader(string HeaderName) => By.XPath("//*[@class='card-header']/h2/button[text() = '" + HeaderName + "']");
        
        By referenceDataTitle(string ReferenceDataName) => By.XPath("//div[@class='card-deck']/div/div/a/h3[@class='card-title'][text()='" + ReferenceDataName + "']");
        
        By referenceDataSubTitle(string SubTitle) => By.XPath("//div[@class='card-deck ']/div/div/a/p[@class='cardtext'][text()='" + SubTitle + "']");

        
        public ReferenceDataPage WaitForReferenceDataPageToLoad()
        {
            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(pageHeader);
            WaitForElement(searchTextBox);
            WaitForElement(searchButton);

            if (driver.FindElement(pageHeader).Text != "Reference Data")
                throw new Exception("Reference Data page title do not equals: \"Reference Data\" ");

            return this;
        }

        public ReferenceDataPage ValidateReferenceDataMainHeaderVisibility(string HeaderName, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(referenceDataMainHeader(HeaderName));
            else
                WaitForElementNotVisible(referenceDataMainHeader(HeaderName), 3);

            return this;
        }
        
        public ReferenceDataPage ValidateReferenceDataElementVisibility(string ReferenceDataName, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(referenceDataTitle(ReferenceDataName));
            else
                WaitForElementNotVisible(referenceDataTitle(ReferenceDataName), 3);

            return this;
        }
        
        public ReferenceDataPage ValidateReferenceDataElementSubTitleVisibility(string SubTitle, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(referenceDataSubTitle(SubTitle));
            else
                WaitForElementNotVisible(referenceDataSubTitle(SubTitle), 3);

            return this;
        }

        public ReferenceDataPage ClickReferenceDataMainHeader(string HeaderName)
        {
            WaitForElementToBeClickable(referenceDataMainHeader(HeaderName));
            Click(referenceDataMainHeader(HeaderName));

            return this;
        }
        
        public ReferenceDataPage ClickReferenceDataElement(string ReferenceDataName)
        {
            WaitForElementToBeClickable(referenceDataTitle(ReferenceDataName));
            Click(referenceDataTitle(ReferenceDataName));

            return this;
        }

        public ReferenceDataPage InsertSearchQuery(string SearchQuery)
        {
            SendKeys(searchTextBox, SearchQuery);

            return this;
        }

        public ReferenceDataPage TapSearchButton()
        {
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}
