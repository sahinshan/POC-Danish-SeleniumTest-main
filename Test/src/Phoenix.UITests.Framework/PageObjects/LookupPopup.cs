using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Phoenix.UITests.Framework.PageObjects
{
    public class LookupPopup : CommonMethods
    {
        public LookupPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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
        readonly By okButton = By.Id("CWOkButton");
        readonly By cancellButton = By.Id("CWCloseButton");
        readonly By lookIn_Field = By.Id("CWViewSelector");
        readonly By addSelectedButton = By.Id("CWAddSelectedButton");
        readonly By lookFor_Field = By.Id("CWBusinessObjectSelector");
        readonly By Assign_OkButton = By.Id("CWSave");
        readonly By multiSelectContainer = By.Id("CWMultiselectContainer");

        readonly By gridResultsArea = By.XPath("//table[@id='CWGrid']");


        By gridHeaderCell(int cellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]//a");

        By checkboxElement(string ElementID) => By.XPath("//tr[@id='" + ElementID + "']/td[1]/input[@id='CHK_" + ElementID + "']");
        
        By gridRecordCell(string ElementID, int cellPosition) => By.XPath("//tr[@id='" + ElementID + "']/td[" + cellPosition + "]");

        By nameElement(string ElementName) => By.XPath("//table[@id='CWGrid']/tbody/tr/td[2][text()='" + ElementName + "']");

        By CheckboxElementToSelect(string SearchString) => By.XPath("//tr/td[@title ='" + SearchString + "']/preceding-sibling::td/input");


        public LookupPopup WaitForLookupPopupToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(popupHeader);

            WaitForElement(viewSelector);
            WaitForElement(quickSearchTextbox);
            WaitForElement(searchButton);
            WaitForElement(refreshButton);
            WaitForElementVisible(okButton);
            WaitForElementVisible(cancellButton);

            WaitForElement(gridResultsArea);

            return this;
        }

        public LookupPopup WaitForLookupPopupToLoad(int WaitTimeSeconds)
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

        public LookupPopup WaitForLookupPopupToLoad(string PopupTitle)
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElementVisible(popupHeader);

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

        public LookupPopup WaitForOptionSetLookupPopupToLoad()
        {
            WaitForElement(iframe_CWOptionSetForm);
            SwitchToIframe(iframe_CWOptionSetForm);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(popupHeader);

            WaitForElement(quickSearchTextbox);
            WaitForElement(searchButton);
            WaitForElement(refreshButton);
            WaitForElementVisible(okButton);
            WaitForElementVisible(cancellButton);

            WaitForElement(gridResultsArea);

            return this;
        }

        public LookupPopup SelectBusinessObjectByText(string TextToSelect)
        {
            SelectPicklistElementByText(businessObjectSelector, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public LookupPopup SelectViewByValue(string Value)
        {
            var element = driver.FindElement(viewSelector);
            SelectElement viewElement = new SelectElement(element);
            viewElement.SelectByValue(Value);

            return this;
        }

        public LookupPopup SelectViewByText(string Text)
        {
            WaitForElementToBeClickable(viewSelector);
            SelectPicklistElementByText(viewSelector, Text);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public LookupPopup TypeSearchQuery(string Query)
        {
            WaitForElementVisible(quickSearchTextbox);
            ScrollToElement(quickSearchTextbox);
            SendKeys(quickSearchTextbox, Query);

            return this;
        }

        public LookupPopup TapSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            ScrollToElement(searchButton);
            Click(searchButton);           

            return this;
        }

        public LookupPopup SearchAndSelectRecord(string Query, Guid RecordToSelect)
        {
            TypeSearchQuery(Query);

            TapSearchButton();

            SelectResultElement(RecordToSelect);

            return this;
        }

        public LookupPopup TapSearchButton(int WaitTime)
        {
            WaitForElementToBeClickable(searchButton);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", WaitTime);

            return this;
        }

        public LookupPopup SelectResultElement(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementToBeClickable(checkboxElement(ElementID));
            Click(checkboxElement(ElementID));

            Click(okButton);

            driver.SwitchTo().ParentFrame();

            return this;
        }

        public LookupPopup SelectResultElement(Guid ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementToBeClickable(checkboxElement(ElementID.ToString()));
            Click(checkboxElement(ElementID.ToString()));

            Click(okButton);

            driver.SwitchTo().ParentFrame();

            return this;
        }

        public LookupPopup ValidateGridHeaderCellText(int CellPosition, string ExpectedText)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            ScrollToElement(gridHeaderCell(CellPosition));
            ValidateElementText(gridHeaderCell(CellPosition), ExpectedText);

            return this;
        }

        public LookupPopup ClickResultElementByText(string ElementText)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            driver.FindElement(nameElement(ElementText)).Click();

            return this;
        }

        public LookupPopup ValidateResultElementPresent(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);
            WaitForElementVisible(checkboxElement(ElementID));

            return this;
        }

        public LookupPopup ValidateResultElementPresent(Guid ElementID)
        {
            return ValidateResultElementPresent(ElementID.ToString());
        }

        public LookupPopup ValidateResultElementNotPresent(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementNotVisible(checkboxElement(ElementID), 7);

            return this;
        }

        public LookupPopup ValidateResultElementNotPresent(Guid ElementID)
        {
            return ValidateResultElementNotPresent(ElementID.ToString());
        }

        public LookupPopup ValidateBusinessObjectNotPresent(string BusinessObjectName)
        {
            ValidatePicklistDoesNotContainsElementByText(businessObjectSelector, BusinessObjectName);

            return this;
        }

        public LookupPopup ValidateBusinessObjectPresent(string BusinessObjectName)
        {
            ValidatePicklistContainsElementByText(businessObjectSelector, BusinessObjectName);

            return this;
        }

        public LookupPopup ValidateViewElementNotPresent(string ViewElementText)
        {
            ValidatePicklistDoesNotContainsElementByText(viewSelector, ViewElementText);

            return this;
        }
        
        public LookupPopup ValidateViewElementPresent(string ViewElementText)
        {
            ValidatePicklistContainsElementByText(viewSelector, ViewElementText);

            return this;
        }

        public LookupPopup SelectLookIn(String TextToSelect)
        {
            WaitForElementToBeClickable(lookIn_Field);
            SelectPicklistElementByText(lookIn_Field, TextToSelect);

            return this;
        }

        public LookupPopup SelectLookFor(String TextToSelect)
        {
            SelectPicklistElementByText(lookFor_Field, TextToSelect);

            return this;
        }

        public LookupPopup ClickAddSelectedButton(String ElementID)
        {


            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(checkboxElement(ElementID));
            Click(checkboxElement(ElementID));

            // WaitForElementToBeClickable(recordCheckbox(ElementID));
            //Click(recordCheckbox(ElementID));


            WaitForElementToBeClickable(addSelectedButton);
            ScrollToElement(addSelectedButton);

            Click(addSelectedButton);

            WaitForElementToBeClickable(okButton);
            Click(okButton);
            return this;

        }

        public LookupPopup ClickCloseButton()
        {
            WaitForElementToBeClickable(cancellButton);
            Click(cancellButton);

            return this;

        }

        public LookupPopup SelectResult(string SearchString)
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementToBeClickable(CheckboxElementToSelect(SearchString));
            Click(CheckboxElementToSelect(SearchString));

            Click(okButton);

            driver.SwitchTo().ParentFrame();

            return this;
        }

        public LookupPopup ClickAssignOkButton()
        {
            WaitForElementToBeClickable(Assign_OkButton);
            ScrollToElement(Assign_OkButton);
            Click(Assign_OkButton);

            return this;

        }

        public LookupPopup ValidateGridRecordCellText(Guid ElementId, int CellPosition, string ExpectedText)
        {
            ScrollToElement(gridRecordCell(ElementId.ToString(), CellPosition));
            ValidateElementText(gridRecordCell(ElementId.ToString(), CellPosition), ExpectedText);

            return this;
        }

        public LookupPopup ValidateMultiSelectLookupSection()
        {
            WaitForElementVisible(multiSelectContainer);
            WaitForElementVisible(addSelectedButton);

            return this;
        }

        public LookupPopup SearchAndValidateRecordPresentOrNot(string Query, Guid RecordToValidate, bool IsPresent)
        {
            TypeSearchQuery(Query);

            TapSearchButton();

            if (IsPresent)
                ValidateResultElementPresent(RecordToValidate.ToString());
            else
                ValidateResultElementNotPresent(RecordToValidate.ToString());

            return this;
        }

        public LookupPopup SelectElementAndClickAddRecordsButton(string Query, string ElementID)
        {
            TypeSearchQuery(Query);

            TapSearchButton();

            ValidateResultElementPresent(ElementID.ToString());
            WaitForElementToBeClickable(checkboxElement(ElementID));
            Click(checkboxElement(ElementID));

            WaitForElementToBeClickable(addSelectedButton);
            ScrollToElement(addSelectedButton);
            Click(addSelectedButton);

            return this;
        }

        public LookupPopup ClickOkButton()
        {
            WaitForElementToBeClickable(okButton);
            ScrollToElement(okButton);
            Click(okButton);

            return this;

        }

        public LookupPopup ValidateLookForPickListExpectedTextIsPresent(string ExpectedText, bool IsPresent)
        {
            WaitForElementVisible(lookFor_Field);
            ScrollToElement(lookFor_Field);

            if (IsPresent)
                ValidatePicklistContainsElementByText(lookFor_Field, ExpectedText);
            else
                ValidatePicklistDoesNotContainsElementByText(lookFor_Field, ExpectedText);

            return this;
        }

        public LookupPopup ValidateLookInPickListExpectedTextIsPresent(string ExpectedText, bool IsPresent)
        {
            WaitForElementVisible(lookIn_Field);
            ScrollToElement(lookIn_Field);

            if (IsPresent)
                ValidatePicklistContainsElementByText(lookIn_Field, ExpectedText);
            else
                ValidatePicklistDoesNotContainsElementByText(lookIn_Field, ExpectedText);

            return this;
        }

        //Validate lookFor_Field Selected Value
        public LookupPopup ValidateLookForSelectedValue(string ExpectedValue)
        {
            WaitForElementVisible(lookFor_Field);
            ScrollToElement(lookFor_Field);

            ValidatePicklistSelectedText(lookFor_Field, ExpectedValue);

            return this;
        }

        //Validate lookIn_Field Selected Value
        public LookupPopup ValidateLookInSelectedValue(string ExpectedValue)
        {
            WaitForElementVisible(lookIn_Field);
            ScrollToElement(lookIn_Field);

            ValidatePicklistSelectedText(lookIn_Field, ExpectedValue);

            return this;
        }

        //Validate lookIn_Field is disabled or not disabled
        public LookupPopup ValidateLookInFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(lookIn_Field);
            ScrollToElement(lookIn_Field);

            if(IsDisabled)
                ValidateElementDisabled(lookIn_Field);
            else
                ValidateElementNotDisabled(lookIn_Field);

            return this;
        }

    }

}

