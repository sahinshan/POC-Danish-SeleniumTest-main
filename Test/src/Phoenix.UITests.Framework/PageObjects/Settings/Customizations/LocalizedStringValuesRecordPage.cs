using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Recruitment
{
    public class LocalizedStringValuesRecordPage : CommonMethods
    {
        public LocalizedStringValuesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Frames

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=localizedstringvalue&')]");

        #endregion

        #region Toolbar Section

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");

        #endregion

        #region 

        readonly By LocalizedString_Field = By.Id("CWField_localizedstringid");
        readonly By Productlanguage_Field = By.Id("CWField_productlanguageid");
        readonly By Plaintext_Field = By.Id("CWField_plaintext");

        #endregion

        public LocalizedStringValuesRecordPage WaitForLocalizedStringValuesRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(pagehehader);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(LocalizedString_Field);
            WaitForElement(Productlanguage_Field);
            WaitForElement(Plaintext_Field);

            return this;
        }

        public LocalizedStringValuesRecordPage SelectLanguage(string LanguageValue)
        {
            MoveToElementInPage(Productlanguage_Field);
            SelectPicklistElementByValue(Productlanguage_Field, LanguageValue);

            return this;
        }

        public LocalizedStringValuesRecordPage InsertPlainText(string TextToInsert)
        {
            MoveToElementInPage(backButton);
            SendKeys(Plaintext_Field, TextToInsert);

            return this;
        }

        public LocalizedStringValuesRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public LocalizedStringValuesRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            return this;
        }

        public LocalizedStringValuesRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public LocalizedStringValuesRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(deleteRecordButton);

            return this;
        }

        public LocalizedStringValuesRecordPage ValidateOptionSet_Field_Disabled(bool ExpectVisible)
        {
            WaitForElementVisible(LocalizedString_Field);

            if (ExpectVisible)
                ValidateElementDisabled(LocalizedString_Field);
            else
                ValidateElementNotDisabled(LocalizedString_Field);

            return this;
        }

        public LocalizedStringValuesRecordPage ValidateText_Field_Disabled(bool ExpectVisible)
        {
            WaitForElementVisible(Productlanguage_Field);

            if (ExpectVisible)
                ValidateElementDisabled(Productlanguage_Field);
            else
                ValidateElementNotDisabled(Productlanguage_Field);

            return this;
        }

    }
}
