using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class LookUpRecordsPopUp : CommonMethods
    {
        public LookUpRecordsPopUp(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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
        readonly By closeButton = By.Id("CWCloseButton");
        readonly By lookIn_Field = By.Id("CWViewSelector");
        readonly By addSelectedButton = By.Id("CWAddSelectedButton");


        readonly By gridResultsArea = By.XPath("//table[@id='CWGrid']");
        By gridHeaderCell(int cellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]");



        By checkboxElement(string ElementID) => By.XPath("//tr[@id='" + ElementID + "']/td[1]/input[@id='CHK_" + ElementID + "']");

        By recordCheckbox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");
        By nameElement(string ElementName) => By.XPath("//table[@id='CWGrid']/tbody/tr/td[2][text()='" + ElementName + "']");

        By CheckboxElementToSelect(string SearchString) => By.XPath("//tr/td[@title ='" + SearchString + "'][contains(@id, '_Primary')]/preceding-sibling::td/input");

        public LookUpRecordsPopUp WaitForLookUpRecordsPopUpToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);

            
            WaitForElement(quickSearchTextbox);
            WaitForElement(searchButton);
            WaitForElement(refreshButton);
            WaitForElement(okButton);
            WaitForElement(closeButton);

            WaitForElement(gridResultsArea);

            return this;
        }

        public LookUpRecordsPopUp WaitForLookUpRecordsPopUpToLoad(int WaitTimeSeconds)
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

            WaitForElement(gridResultsArea, WaitTimeSeconds);

            return this;
        }

        public LookUpRecordsPopUp WaitForLookUpRecordsPopUpToLoad(string PopupTitle)
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
            Wait.Until(c => c.FindElement(closeButton));

            Wait.Until(c => c.FindElement(gridResultsArea));

            return this;
        }

        public LookUpRecordsPopUp SelectBusinessObjectByText(string TextToSelect)
        {
            SelectPicklistElementByText(businessObjectSelector, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public LookUpRecordsPopUp SelectViewByValue(string Value)
        {
            var element = driver.FindElement(viewSelector);
            SelectElement viewElement = new SelectElement(element);
            viewElement.SelectByValue(Value);

            return this;
        }

        public LookUpRecordsPopUp SelectViewByText(string Text)
        {
            WaitForElementToBeClickable(viewSelector);
            SelectPicklistElementByText(viewSelector, Text);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public LookUpRecordsPopUp TypeSearchQuery(string Query)
        {
            SendKeys(quickSearchTextbox, Query);
            return this;
        }

        public LookUpRecordsPopUp TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public LookUpRecordsPopUp TapSearchButton(int WaitTime)
        {
            WaitForElementToBeClickable(searchButton);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", WaitTime);

            return this;
        }

        public LookUpRecordsPopUp SelectResultElement(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementToBeClickable(checkboxElement(ElementID));
            Click(checkboxElement(ElementID));

            Click(okButton);

            driver.SwitchTo().ParentFrame();

            return this;
        }

        public LookUpRecordsPopUp ValidateGridHeaderCellText(int CellPosition, string ExpectedText)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            ValidateElementText(gridHeaderCell(CellPosition), ExpectedText);

            return this;
        }

        public LookUpRecordsPopUp ClickResultElementByText(string ElementText)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            driver.FindElement(nameElement(ElementText)).Click();

            return this;
        }

        public LookUpRecordsPopUp ValidateResultElementPresent(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(checkboxElement(ElementID));

            return this;
        }

        public LookUpRecordsPopUp ValidateResultElementNotPresent(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementNotVisible(checkboxElement(ElementID), 7);

            return this;
        }

        public LookUpRecordsPopUp ValidateViewElementNotPresent(string ViewElementText)
        {
            ValidatePicklistDoesNotContainsElementByText(viewSelector, ViewElementText);

            return this;
        }

        public LookUpRecordsPopUp SelectLookIn(String TextToSelect)
        {
            SelectPicklistElementByText(lookIn_Field, TextToSelect);

            return this;
        }

        public LookUpRecordsPopUp ClickAddSelectedButton(String ElementID)
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

        public LookUpRecordsPopUp ClickCloseButton()
        {
            WaitForElementToBeClickable(closeButton);
            Click(closeButton);

            return this;

        }

        public LookUpRecordsPopUp SelectResult(string SearchString)
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementToBeClickable(CheckboxElementToSelect(SearchString));
            Click(CheckboxElementToSelect(SearchString));

            Click(okButton);

            driver.SwitchTo().ParentFrame();

            return this;
        }


    }

}

