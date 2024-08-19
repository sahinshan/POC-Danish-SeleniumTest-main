using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class LookupMultiSelectPopup : CommonMethods
    {
        public LookupMultiSelectPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_CWLookupForm");

        readonly By iframe_CWOptionSetForm = By.Id("iframe_CWOptionSetForm");

        readonly By popupHeader = By.XPath("//header[@id='CWHeader']/h1");

        readonly By businessObjectSelector = By.Id("CWBusinessObjectSelector");
        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");
        readonly By addRecordsButton = By.Id("CWAddSelectedButton");
        readonly By okButton = By.Id("CWOkButton");
        readonly By closeButton = By.Id("CWCloseButton");


        readonly By gridResultsArea = By.XPath("//table[@id='CWGrid']");

        By checkboxElement(string ElementID) => By.XPath("//tr[@id='" + ElementID + "']/td[1]/input[@id='CHK_" + ElementID + "']");
        By nameElement(string ElementName) => By.XPath("//table[@id='CWGrid']/tbody/tr/td[2][text()='" + ElementName + "']");


        public LookupMultiSelectPopup WaitForLookupMultiSelectPopupToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);

            WaitForElement(viewSelector);
            WaitForElement(quickSearchTextbox);
            WaitForElement(searchButton);
            WaitForElement(refreshButton);
            WaitForElement(okButton);
            WaitForElement(closeButton);

            WaitForElement(gridResultsArea);

            return this;
        }

        public LookupMultiSelectPopup WaitForOptionSetLookupMultiSelectPopupToLoad()
        {
            WaitForElement(iframe_CWOptionSetForm);
            SwitchToIframe(iframe_CWOptionSetForm);

            WaitForElement(popupHeader);

            WaitForElement(quickSearchTextbox);
            WaitForElement(searchButton);
            WaitForElement(refreshButton);
            WaitForElement(okButton);
            WaitForElement(closeButton);

            WaitForElement(gridResultsArea);

            return this;
        }

        public LookupMultiSelectPopup SelectBusinessObjectByText(string TextToSelect)
        {
            SelectPicklistElementByText(businessObjectSelector, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public LookupMultiSelectPopup SelectViewByValue(string Value)
        {
            var element = driver.FindElement(viewSelector);
            SelectElement viewElement = new SelectElement(element);
            viewElement.SelectByValue(Value);

            return this;
        }

        public LookupMultiSelectPopup SelectViewByText(string Text)
        {
            SelectPicklistElementByText(viewSelector, Text);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public LookupMultiSelectPopup TypeSearchQuery(string Query)
        {
            SendKeys(quickSearchTextbox, Query);

            return this;
        }

        public LookupMultiSelectPopup TapSearchButton()
        {
            driver.FindElement(searchButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public LookupMultiSelectPopup SelectResultElement(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            this.Click(checkboxElement(ElementID));

            this.Click(addRecordsButton);

            this.Click(okButton);

            return this;
        }

        public LookupMultiSelectPopup SelectResultElement(Guid ElementID)
        {
            return SelectResultElement(ElementID.ToString());
        }

        public LookupMultiSelectPopup TapOKButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            this.Click(okButton);

            return this;
        }

        public LookupMultiSelectPopup TapCloseButton()
        {
            Click(closeButton);
            return this;
        }

        public LookupMultiSelectPopup AddElementToList(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            this.Click(checkboxElement(ElementID));

            this.Click(addRecordsButton);

            return this;
        }

        public LookupMultiSelectPopup AddElementToList(Guid ElementID)
        {
            return AddElementToList(ElementID.ToString());
        }

        public LookupMultiSelectPopup ClickResultElementByText(string ElementText)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            driver.FindElement(nameElement(ElementText)).Click();

            return this;
        }

        public LookupMultiSelectPopup ValidateResultElementPresent(string ElementID)
        {
            WaitForElementVisible(checkboxElement(ElementID));

            return this;
        }

        public LookupMultiSelectPopup ValidateResultElementPresent(Guid ElementID)
        {
            return ValidateResultElementPresent(ElementID.ToString());
        }

        public LookupMultiSelectPopup ValidateResultElementNotPresent(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementNotVisible(checkboxElement(ElementID), 7);

            return this;
        }

        public LookupMultiSelectPopup ValidateResultElementNotPresent(Guid ElementID)
        {
            return ValidateResultElementNotPresent(ElementID.ToString());
        }

       
    }
}
