using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Recruitment
{
    public class OptionsetValueRecordPage : CommonMethods
    {
        public OptionsetValueRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Frames

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=optionsetvalue&')]");

        #endregion

        #region Toolbar Section

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By additionalToolbarElementsButton = By.Id("CWToolbarMenu");

        #endregion

        #region Optionset Value Rercor Fields 

        readonly By OptionSet_Field = By.Id("CWField_optionsetid");
        readonly By Text_Field = By.Id("CWField_localizedtextid_cwname");
        readonly By DisplayOrder_Field = By.Id("CWField_displayorder");
        readonly By NumericCode_Field = By.Id("CWField_numericcode");
        readonly By Code_Field = By.Id("CWField_code");
        readonly By Weightage_Field = By.Id("CWField_weightage");
        readonly By StartDate_Field = By.Id("CWField_startdate");
        readonly By EndDate_Field = By.Id("CWField_enddate");
        readonly By BusinessModule_Field = By.Id("CWField_businessmoduleid");

        #endregion

        public OptionsetValueRecordPage WaitForOptionsetValueRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(pagehehader);
            WaitForElement(OptionSet_Field);
            WaitForElement(Text_Field);
            WaitForElement(DisplayOrder_Field);
            WaitForElement(StartDate_Field);

            return this;
        }

        public OptionsetValueRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public OptionsetValueRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            return this;
        }

        public OptionsetValueRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public OptionsetValueRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(deleteRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public OptionsetValueRecordPage ValidateOptionSet_Field_Disabled(bool ExpectVisible)
        {
            WaitForElementVisible(OptionSet_Field);

            if (ExpectVisible)
                ValidateElementDisabled(OptionSet_Field);
            else
                ValidateElementNotDisabled(OptionSet_Field);

            return this;
        }

        public OptionsetValueRecordPage ValidateText_Field_Disabled(bool ExpectVisible)
        {
            WaitForElementVisible(Text_Field);

            if (ExpectVisible)
                ValidateElementDisabled(Text_Field);
            else
                ValidateElementNotDisabled(Text_Field);

            return this;
        }

        public OptionsetValueRecordPage ValidateDisplayOrder_Field_Disabled(bool ExpectVisible)
        {
            WaitForElementVisible(DisplayOrder_Field);

            if (ExpectVisible)
                ValidateElementDisabled(DisplayOrder_Field);
            else
                ValidateElementNotDisabled(DisplayOrder_Field);

            return this;
        }

        public OptionsetValueRecordPage ValidateNumericCode_Field_Disabled(bool ExpectVisible)
        {
            WaitForElementVisible(NumericCode_Field);

            if (ExpectVisible)
                ValidateElementDisabled(NumericCode_Field);
            else
                ValidateElementNotDisabled(NumericCode_Field);

            return this;
        }

        public OptionsetValueRecordPage ValidateCode_Field_Disabled(bool ExpectVisible)
        {
            WaitForElementVisible(Code_Field);

            if (ExpectVisible)
                ValidateElementDisabled(Code_Field);
            else
                ValidateElementNotDisabled(Code_Field);

            return this;
        }

        public OptionsetValueRecordPage ValidateWeightage_Field_Disabled(bool ExpectVisible)
        {
            WaitForElementVisible(Weightage_Field);

            if (ExpectVisible)
                ValidateElementDisabled(Weightage_Field);
            else
                ValidateElementNotDisabled(Weightage_Field);

            return this;
        }

        public OptionsetValueRecordPage ValidateStartDate_Field_Disabled(bool ExpectVisible)
        {
            WaitForElementVisible(StartDate_Field);

            if (ExpectVisible)
                ValidateElementDisabled(StartDate_Field);
            else
                ValidateElementNotDisabled(StartDate_Field);

            return this;
        }

        public OptionsetValueRecordPage ValidateEndDate_Field_Disabled(bool ExpectVisible)
        {
            WaitForElementVisible(EndDate_Field);

            if (ExpectVisible)
                ValidateElementDisabled(EndDate_Field);
            else
                ValidateElementNotDisabled(EndDate_Field);

            return this;
        }

        public OptionsetValueRecordPage ValidateBusinessModule_Field_Disabled(bool ExpectVisible)
        {
            WaitForElementVisible(BusinessModule_Field);

            if (ExpectVisible)
                ValidateElementDisabled(BusinessModule_Field);
            else
                ValidateElementNotDisabled(BusinessModule_Field);

            return this;
        }

        public OptionsetValueRecordPage ValidateAll_Fields_Disabled(bool ExpectVisible)
        {
            ValidateOptionSet_Field_Disabled(ExpectVisible);
            ValidateText_Field_Disabled(ExpectVisible);
            ValidateDisplayOrder_Field_Disabled(ExpectVisible);
            ValidateNumericCode_Field_Disabled(ExpectVisible);
            ValidateCode_Field_Disabled(ExpectVisible);
            ValidateWeightage_Field_Disabled(ExpectVisible);
            ValidateStartDate_Field_Disabled(ExpectVisible);
            ValidateEndDate_Field_Disabled(ExpectVisible);
            ValidateBusinessModule_Field_Disabled(ExpectVisible);

            return this;

        }

        public OptionsetValueRecordPage ValidateOptionSetFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(OptionSet_Field);
            MoveToElementInPage(OptionSet_Field);
            ValidateElementValue(OptionSet_Field, ExpectedValue);

            return this;
        }

        public OptionsetValueRecordPage ValidateTextFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(Text_Field);
            MoveToElementInPage(Text_Field);
            ValidateElementValue(Text_Field, ExpectedValue);

            return this;
        }

        public OptionsetValueRecordPage ValidateDisplayOrderFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(DisplayOrder_Field);
            MoveToElementInPage(DisplayOrder_Field);
            ValidateElementValue(DisplayOrder_Field, ExpectedValue);

            return this;
        }

    }
}
