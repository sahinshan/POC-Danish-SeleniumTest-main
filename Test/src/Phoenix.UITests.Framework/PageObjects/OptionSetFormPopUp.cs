using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class OptionSetFormPopUp : CommonMethods
    {
        public OptionSetFormPopUp(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_CWOptionSetForm");

        readonly By popupHeader = By.XPath("//header[@id='CWHeader']/h1");

        readonly By businessObjectSelector = By.Id("CWBusinessObjectSelector");
        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");
        readonly By okButton = By.Id("CWOkButton");
        readonly By cancellButton = By.Id("CWCloseButton");
        readonly By lookIn_Field = By.Id("CWViewSelector");
        readonly By addSelectedButton = By.Id("CWAddSelectedButton");


        readonly By gridResultsArea = By.XPath("//table[@id='CWGrid']");

        By checkboxElement(string ElementID) => By.XPath("//tr[@id='" + ElementID + "']/td[1]/input[@id='CHK_" + ElementID + "']");

        By CheckboxElementToSelect(string SearchString) => By.XPath("//tr/td[@title ='" + SearchString + "']/preceding-sibling::td/input");
        By recordCheckbox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");
        By nameElement(string ElementName) => By.XPath("//table[@id='CWGrid']/tbody/tr/td[2][text()='" + ElementName + "']");


        public OptionSetFormPopUp WaitForOptionSetFormPopUpToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);

         
            return this;
        }

        public OptionSetFormPopUp WaitForOptionSetFormPopUpToLoad(int WaitTimeSeconds)
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);

            WaitForElement(viewSelector);
            WaitForElement(quickSearchTextbox);
            WaitForElement(searchButton);
            WaitForElement(refreshButton);
            WaitForElement(okButton);
            WaitForElement(cancellButton);

            WaitForElement(gridResultsArea, WaitTimeSeconds);

            return this;
        }

        public OptionSetFormPopUp WaitForOptionSetFormPopUpToLoad(string PopupTitle)
        {
            Wait.Until(c => c.FindElement(popupIframe));
            driver.SwitchTo().Frame(driver.FindElement(popupIframe));

            Wait.Until(c => c.FindElement(popupHeader));

            ValidateElementText(popupHeader, PopupTitle);

            Wait.Until(c => c.FindElement(viewSelector));
            Wait.Until(c => c.FindElement(quickSearchTextbox));
            Wait.Until(c => c.FindElement(searchButton));
            Wait.Until(c => c.FindElement(refreshButton));
            Wait.Until(c => c.FindElement(okButton));
            Wait.Until(c => c.FindElement(cancellButton));

            Wait.Until(c => c.FindElement(gridResultsArea));

            return this;
        }

        public OptionSetFormPopUp SelectBusinessObjectByText(string TextToSelect)
        {
            SelectPicklistElementByText(businessObjectSelector, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public OptionSetFormPopUp SelectViewByValue(string Value)
        {
            var element = driver.FindElement(viewSelector);
            SelectElement viewElement = new SelectElement(element);
            viewElement.SelectByValue(Value);

            return this;
        }

        public OptionSetFormPopUp SelectViewByText(string Text)
        {
            WaitForElementToBeClickable(viewSelector);
            SelectPicklistElementByText(viewSelector, Text);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public OptionSetFormPopUp SelectResult(string SearchString)
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementToBeClickable(CheckboxElementToSelect(SearchString));
            Click(CheckboxElementToSelect(SearchString));

            Click(okButton);

            driver.SwitchTo().ParentFrame();

            return this;
        }

        public OptionSetFormPopUp TypeSearchQuery(string Query)
        {
            SendKeys(quickSearchTextbox, Query);

            return this;
        }

        public OptionSetFormPopUp TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }       

        public OptionSetFormPopUp TapSearchButton(int WaitTime)
        {
            WaitForElementToBeClickable(searchButton);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", WaitTime);

            return this;
        }

        public OptionSetFormPopUp SelectResultElement(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(checkboxElement(ElementID));
            Click(checkboxElement(ElementID));

            Click(okButton);

            driver.SwitchTo().ParentFrame();

            return this;
        }


        public OptionSetFormPopUp ClickResultElementByText(string ElementText)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            driver.FindElement(nameElement(ElementText)).Click();

            return this;
        }

        public OptionSetFormPopUp ValidateResultElementPresent(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(checkboxElement(ElementID));

            return this;
        }

        public OptionSetFormPopUp ValidateResultElementNotPresent(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementNotVisible(checkboxElement(ElementID), 7);

            return this;
        }

        public OptionSetFormPopUp ValidateViewElementNotPresent(string ViewElementText)
        {
            ValidatePicklistDoesNotContainsElementByText(viewSelector, ViewElementText);

            return this;
        }

        public OptionSetFormPopUp SelectLookIn(String TextToSelect)
        {
            SelectPicklistElementByText(lookIn_Field, TextToSelect);

            return this;
        }

        public OptionSetFormPopUp ClickAddSelectedButton(String ElementID)
        {


            WaitForElementNotVisible("CWRefreshPanel", 7);

             WaitForElementToBeClickable(checkboxElement(ElementID));
            Click(checkboxElement(ElementID));

            // WaitForElementToBeClickable(recordCheckbox(ElementID));
            //Click(recordCheckbox(ElementID));


            WaitForElementToBeClickable(addSelectedButton);
            ScrollToElement(addSelectedButton);

            Click(addSelectedButton);

           Click(okButton);
            return this;



        }

       


    }

}

