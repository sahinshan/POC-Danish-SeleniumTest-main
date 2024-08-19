using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
    public class OptionsetValuesPage : CommonMethods
    {
        public OptionsetValuesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=optionset&')]");
        readonly By CWNavItem_Frame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        #region Display Order 

        By Outstanding_DisplayOrder(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[3][@title='1']");
        By Requested_DisplayOrder(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[3][@title='2']");
        By Completed_DisplayOrder(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[3][@title='3']");
        By NotProgressed_DisplayOrder(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[3][@title='4']");
        By Override_DisplayOrder(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[3][@title='5']");
        By Expired_DisplayOrder(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[3][@title='6']");

        By OptionSet_DisplayOrder(string recordID, int code) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[5][@title='" + code + "']");

        #endregion

        #region Available Option

        By Outstanding_AvailableOption(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[6][@title='Yes']");
        By Requested_AvailableOption(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[6][@title='Yes']");
        By Completed_AvailableOption(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[6][@title='Yes']");
        By NotProgressed_AvailableOption(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[6][@title='Yes']");
        By Override_AvailableOption(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[6][@title='Yes']");
        By Expired_AvailableOption(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[6][@title='Yes']");

        By OptionSet_AvailableOption(string recordID, string AvailableOption) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[6][@title='" + AvailableOption + "']");

        #endregion

        #region Customizable? Option

        By Outstanding_CustomizableOption(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[9][@title='Yes']");
        By Requested_CustomizableOption(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[9][@title='Yes']");
        By Completed_CustomizableOption(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[9][@title='Yes']");
        By NotProgressed_CustomizableOption(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[9][@title='Yes']");
        By Override_CustomizableOption(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[9][@title='Yes']");
        By Expired_CustomizableOption(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[9][@title='Yes']");

        By OptionSet_CustomizableOption(string recordID, string CustomizableOption) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[9][@title='" + CustomizableOption + "']");

        #endregion

        #region Display Option Name 

        By OptionSet_DisplayName(string recordID, string ExpectedTitle) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2][@title='" + ExpectedTitle + "']");

        #endregion

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        public OptionsetValuesPage WaitForOptionsetValuesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWNavItem_Frame);
            SwitchToIframe(CWNavItem_Frame);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Optionset Values");

            WaitForElement(quickSearchTextbox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public OptionsetValuesPage InsertQuickSearchText(string Text)
        {
            WaitForElementVisible(quickSearchTextbox);
            MoveToElementInPage(quickSearchTextbox);
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public OptionsetValuesPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public OptionsetValuesPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRow(RecordID));
            MoveToElementInPage(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }

        public OptionsetValuesPage ValidateOptionSetRecordIsAvailable(string RecordID, bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(recordRow(RecordID));
            }
            else
            {
                WaitForElementNotVisible(recordRow(RecordID), 3);
            }

            return this;
        }

        public OptionsetValuesPage ValidateOptionSetRecordIsAvailable(Guid RecordID, bool ExpectedDisplayed)
        {
            ValidateOptionSetRecordIsAvailable(RecordID.ToString(), ExpectedDisplayed);
            return this;
        }

        public OptionsetValuesPage ValidateDisplayOrder_AvailableOption_CustomizableOptionOfOutstandingOptionsetValue(string RecordId)
        {
            WaitForElementVisible(Outstanding_DisplayOrder(RecordId));
            WaitForElementVisible(Outstanding_AvailableOption(RecordId));
            WaitForElementVisible(Outstanding_CustomizableOption(RecordId));

            return this;
        }

        public OptionsetValuesPage ValidateDisplayOrder_AvailableOption_CustomizableOptionOfRequestedOptionsetValue(string RecordId)
        {
            WaitForElementVisible(Requested_DisplayOrder(RecordId));
            WaitForElementVisible(Requested_AvailableOption(RecordId));
            WaitForElementVisible(Requested_CustomizableOption(RecordId));

            return this;
        }

        public OptionsetValuesPage ValidateDisplayOrder_AvailableOption_CustomizableOptionOfCompletedOptionsetValue(string RecordId)
        {
            WaitForElementVisible(Completed_DisplayOrder(RecordId));
            WaitForElementVisible(Completed_AvailableOption(RecordId));
            WaitForElementVisible(Completed_CustomizableOption(RecordId));

            return this;
        }

        public OptionsetValuesPage ValidateDisplayOrder_AvailableOption_CustomizableOptionOfNotProgressedOptionsetValue(string RecordId)
        {
            WaitForElementVisible(NotProgressed_DisplayOrder(RecordId));
            WaitForElementVisible(NotProgressed_AvailableOption(RecordId));
            WaitForElementVisible(NotProgressed_CustomizableOption(RecordId));

            return this;
        }

        public OptionsetValuesPage ValidateDisplayOrder_AvailableOption_CustomizableOptionOfOverrideOptionsetValue(string RecordId)
        {
            WaitForElementVisible(Override_DisplayOrder(RecordId));
            WaitForElementVisible(Override_AvailableOption(RecordId));
            WaitForElementVisible(Override_CustomizableOption(RecordId));

            return this;
        }

        public OptionsetValuesPage ValidateDisplayOrder_AvailableOption_CustomizableOptionOfExpiredOptionsetValue(string RecordId)
        {
            WaitForElementVisible(Expired_DisplayOrder(RecordId));
            WaitForElementVisible(Expired_AvailableOption(RecordId));
            WaitForElementVisible(Expired_CustomizableOption(RecordId));

            return this;
        }

        public OptionsetValuesPage ValidateOptionSet_DisplayOrder(string RecordId, int Code)
        {
            WaitForElementVisible(OptionSet_DisplayOrder(RecordId, Code));

            return this;
        }

        public OptionsetValuesPage ValidateOptionSet_DisplayName(string RecordId, string OptionName)
        {
            WaitForElementVisible(OptionSet_DisplayName(RecordId, OptionName));

            return this;
        }

        public OptionsetValuesPage ValidateOptionSet_AvailableOption(string RecordId, string OptionName)
        {
            WaitForElementVisible(OptionSet_AvailableOption(RecordId, OptionName));

            return this;
        }

        public OptionsetValuesPage ValidateOptionSet_CustomizableOption(string RecordId, string OptionName)
        {
            WaitForElementVisible(OptionSet_CustomizableOption(RecordId, OptionName));

            return this;
        }

    }
}
