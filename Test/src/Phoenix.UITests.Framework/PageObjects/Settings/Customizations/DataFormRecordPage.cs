using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DataFormRecordPage : CommonMethods
    {
        public DataFormRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=dataform&')]");

        readonly By BackButton = By.XPath("//button[@title='Back']");

        readonly By ShowInstructionsFieldLabel = By.XPath("//*[@id='CWLabelHolder_showinstructions']/label[text()='Show Instructions']");
        readonly By ShowInstructionsField_YesOption = By.XPath("//*[@id='CWField_showinstructions_1']");
        readonly By ShowInstructionsField_NoOption = By.XPath("//*[@id='CWField_showinstructions_0']");

        readonly By ShowLabelFieldLabel = By.XPath("//*[@id='CWLabelHolder_showlabel']/label[text()='Show Label']");
        readonly By ShowLabelField_YesOption = By.XPath("//*[@id='CWField_showlabel_1']");
        readonly By ShowLabelField_NoOption = By.XPath("//*[@id='CWField_showlabel_0']");

        #region General field
        readonly By NameField_Value = By.Id("CWField_name");
        readonly By LabelName_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_labelid']/label/span");
        readonly By LabelName_Field = By.XPath("//li[@id = 'CWLabelHolder_labelid']/label");
        readonly By LabelNameField_Value = By.XPath("//input[@id = 'CWField_labelid_cwname']");
        readonly By Type_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_formtype']/label/span");
        readonly By Type_Field = By.XPath("//li[@id = 'CWLabelHolder_formtype']/label");                
        readonly By TypeField_Value = By.XPath("//select[@id = 'CWField_formtype']");

        #endregion

        #region Toolbar options
        readonly By EditButton = By.Id("TI_CWEditForm");
        #endregion

        public DataFormRecordPage WaitForDataFormRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);


            return this;
        }

        public DataFormRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            MoveToElementInPage(BackButton);
            Click(BackButton);

            return this;
        }

        public DataFormRecordPage ClickEditButton()
        {
            WaitForElementToBeClickable(EditButton);
            MoveToElementInPage(EditButton);
            Click(EditButton);

            return this;
        }

        #region General Fields Record Page Field Validation Methods
        public DataFormRecordPage ValidateShowInstructionsFieldVisible()
        {

            WaitForElementVisible(ShowInstructionsFieldLabel);
            WaitForElementVisible(ShowInstructionsField_YesOption);
            WaitForElementVisible(ShowInstructionsField_NoOption);


            return this;
        }

        public DataFormRecordPage ValidateShowLabelFieldVisible()
        {

            WaitForElementVisible(ShowLabelFieldLabel);
            WaitForElementVisible(ShowLabelField_YesOption);
            WaitForElementVisible(ShowLabelField_NoOption);


            return this;
        }        

        public DataFormRecordPage ValidateLabelNameMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LabelName_MandatoryField);
            else
                WaitForElementNotVisible(LabelName_MandatoryField, 3);

            return this;
        }

        public DataFormRecordPage ValidateTypeMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Type_MandatoryField);
            else
                WaitForElementNotVisible(Type_MandatoryField, 3);

            return this;
        }

        public DataFormRecordPage ValidateLabelNameFieldValue(string expectedValue)
        {
            WaitForElementVisible(LabelName_Field);

            string actualValue = GetElementValue(LabelNameField_Value);
            Assert.AreEqual(expectedValue, actualValue);

            return this;
        }

        public DataFormRecordPage ValidateFieldTypeFieldValue(string expectedValue)
        {
            WaitForElementVisible(TypeField_Value);

            ValidatePicklistSelectedText(TypeField_Value, expectedValue);

            return this;
        }

        public DataFormRecordPage ValidateNameFieldValue(string expectedValue)
        {
            WaitForElementVisible(NameField_Value);
            string actualValue = GetElementValue(NameField_Value);
            Assert.AreEqual(expectedValue, actualValue);

            return this;
        }


        #endregion

    }
}
