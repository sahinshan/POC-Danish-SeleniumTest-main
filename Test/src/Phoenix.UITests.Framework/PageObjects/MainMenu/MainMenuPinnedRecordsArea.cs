using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class MainMenuPinnedRecordsArea : CommonMethods
    {
        public MainMenuPinnedRecordsArea(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By PinnedRecordsArea = By.XPath("//*[@id='CWPinnedRecords']");

        readonly By PinnedRecordsAreaHeader = By.XPath("//*[@id='CWPinnedRecords']//span[text()='Pinned Records']");

        By PinnedRecordsAreaLinkElement(string ElementId) => By.XPath("//*[@id='CWPinnedRecords']/div/a[contains(@onclick, '" + ElementId + "')]");

        By PinnedRecordsAreaLinkElement(string ElementId, string ElementText) => By.XPath("//*[@id='CWPinnedRecords']/div/a[contains(@onclick, '" + ElementId + "')][text()='" + ElementText + "']");



        public MainMenuPinnedRecordsArea WaitForPinnedRecordsAreaToLoad()
        {
            WaitForElementVisible(PinnedRecordsArea);
            WaitForElementVisible(PinnedRecordsAreaHeader);

            return this;
        }

        public MainMenuPinnedRecordsArea ValidatePinnedRecordsAreaLinkElementVisible(string ElementId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PinnedRecordsAreaLinkElement(ElementId));
            else
                WaitForElementNotVisible(PinnedRecordsAreaLinkElement(ElementId), 3);

            return this;
        }

        public MainMenuPinnedRecordsArea ValidatePinnedRecordsAreaLinkElementVisible(string ElementId, string ElementText, bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(PinnedRecordsAreaLinkElement(ElementId, ElementText));
            else
                WaitForElementNotVisible(PinnedRecordsAreaLinkElement(ElementId, ElementText), 3);

            return this;
        }

        public MainMenuPinnedRecordsArea OpenPinnedRecord(string RecordId)
        {
            WaitForElementToBeClickable(PinnedRecordsAreaLinkElement(RecordId));
            ScrollToElement(PinnedRecordsAreaLinkElement(RecordId));
            Click(PinnedRecordsAreaLinkElement(RecordId));

            return this;
        }

    }
}
