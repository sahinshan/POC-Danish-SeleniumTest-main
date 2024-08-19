
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class BusinessObjectFieldRecordPage : CommonMethods
    {
        public BusinessObjectFieldRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=businessobjectfield&')]");

        readonly By BackButton = By.XPath("//button[@title='Back']");

        readonly By IsBulkEditEnabledLabel = By.XPath("//*[@id='CWLabelHolder_isbulkeditenabled']/label[text()='Is Bulk Edit Enabled?']");

        readonly By IsBulkEditEnabledField_YesOption = By.XPath("//*[@id='CWField_isbulkeditenabled_1']");
        readonly By IsBulkEditEnabledField_NoOption = By.XPath("//*[@id='CWField_isbulkeditenabled_0']");

        #region Business Objects Fields page field
        readonly By LabelName_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_labelid']/label/span");
        readonly By LabelName_Field = By.XPath("//li[@id = 'CWLabelHolder_labelid']/label");
        readonly By ToolTip_Field = By.XPath("//li[@id = 'CWLabelHolder_tooltipid']/label");
        readonly By Required_Field = By.XPath("//li[@id = 'CWLabelHolder_required']/label");
        readonly By Type_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_fieldtype']/label/span");
        readonly By Type_Field = By.XPath("//li[@id = 'CWLabelHolder_fieldtype']/label");
        readonly By MaxLength_Field = By.XPath("//li[@id = 'CWLabelHolder_maxlength']/label");
        readonly By MaxLength_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_maxlength']/label/span");
        readonly By IsSearchable_Field = By.XPath("//li[@id = 'CWLabelHolder_issearchable']/label");
        readonly By DefaultValue_Field = By.XPath("//span[text() = 'Default Value']");
        

        readonly By LabelNameField_Value = By.XPath("//input[@id = 'CWField_labelid_cwname']");
        readonly By ToolTipField_Value = By.XPath("//input[@id = 'CWField_tooltipid_cwname']");
        By RequiredField_Value(string buttonValue) => By.XPath("//input[@id = 'CWField_required_"+buttonValue+"']"); //[@checked = 'checked'][@dirty = 'false']
        readonly By TypeField_Value = By.XPath("//select[@id = 'CWField_fieldtype']");
        readonly By MaxLengthField_Value = By.XPath("//input[@id = 'CWField_maxlength']");
        readonly By IsSearchableField_YesOption = By.XPath("//input[@id = 'CWField_issearchable_1'][@checked='checked']");
        readonly By IsSearchableField_NoOption = By.XPath("//input[@id = 'CWField_issearchable_0'][@checked='checked']");
        readonly By DefaultValueLookupField_Value = By.XPath("//input[@id = 'CWField_defaultguidvalue_cwname']/preceding-sibling::a");
        readonly By DefaultValueDateField_Value = By.XPath("//input[@id = 'CWField_defaulttotodaysdate_1']");
        readonly By DefaultValueField_NoOption = By.XPath("//input[@id = 'CWField_defaultbooleanvalue_0']");
        readonly By DefaultValueField_YesOption = By.XPath("//input[@id = 'CWField_defaultbooleanvalue_1']");
        readonly By DefaultValueSelectField_Value = By.XPath("//select[@id = 'CWField_defaultnumericvalue']");//select[@id = 'CWField_defaultnumericvalue']/option[@selected = 'selected']
        #endregion






        public BusinessObjectFieldRecordPage WaitForBusinessObjectFieldRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);


            return this;
        }

        public BusinessObjectFieldRecordPage ClickBackButton()
        {
            WaitForElement(BackButton, 5);
            ScrollToElement(BackButton);
            Click(BackButton);

            return this;
        }

        public BusinessObjectFieldRecordPage ValidateIsBulkEditEnabledFieldVisible()
        {

            WaitForElement(IsBulkEditEnabledLabel);
            WaitForElement(IsBulkEditEnabledField_YesOption);
            WaitForElement(IsBulkEditEnabledField_NoOption);


            return this;
        }

        
        #region Business Object Fields Record Page Field Validation Methods

        public BusinessObjectFieldRecordPage ValidateLabelNameMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LabelName_MandatoryField);
            else
                WaitForElementNotVisible(LabelName_MandatoryField, 3);

            return this;
        }

        public BusinessObjectFieldRecordPage ValidateTypeMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Type_MandatoryField);
            else
                WaitForElementNotVisible(Type_MandatoryField, 3);

            return this;
        }

        public BusinessObjectFieldRecordPage ValidateMaxLengthMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MaxLength_MandatoryField);
            else
                WaitForElementNotVisible(MaxLength_MandatoryField, 3);

            return this;
        }

        public BusinessObjectFieldRecordPage ValidateLabelNameFieldValue(string expectedValue)
        {
            WaitForElement(LabelName_Field);

            string actualValue = GetElementValue(LabelNameField_Value);
            Assert.AreEqual(expectedValue, actualValue);

            return this;
        }

        public BusinessObjectFieldRecordPage ValidateToolTipFieldValue(string expectedValue)
        {
            WaitForElement(ToolTipField_Value);

            string actualValue = GetElementValue(ToolTipField_Value);
            Assert.AreEqual(expectedValue, actualValue);

            return this;
        }

        public BusinessObjectFieldRecordPage ValidateRequiredFieldValue(string buttonValue)
        {
            WaitForElement(RequiredField_Value(buttonValue));
            ScrollToElement(RequiredField_Value(buttonValue));
            ValidateElementChecked(RequiredField_Value(buttonValue));            

            return this;
        }

        public BusinessObjectFieldRecordPage ValidateFieldTypeFieldValue(string expectedValue)
        {
            WaitForElement(TypeField_Value);

            ValidatePicklistSelectedText(TypeField_Value, expectedValue);

            return this;
        }

        public BusinessObjectFieldRecordPage ValidateMaxLengthFieldValue(string expectedValue)
        {
            WaitForElement(MaxLengthField_Value);

            string actualValue = GetElementValue(MaxLengthField_Value);
            Assert.AreEqual(expectedValue, actualValue);

            return this;
        }

        public BusinessObjectFieldRecordPage ValidateDefaultValueFieldValue(string expectedValue)
        {
            WaitForElement(DefaultValueField_NoOption);

            ValidateElementByTitle(DefaultValueField_NoOption, expectedValue);

            return this;
        }

        public BusinessObjectFieldRecordPage ValidateIsSearchableFieldVisible(bool ExpectedVisible)
        {
            bool flag = false;
            if (ExpectedVisible)
            {
                WaitForElementVisible(IsSearchable_Field);
                flag = true;
                //Assert.IsTrue(flag);

            }
            else
            {
                WaitForElementNotVisible(IsSearchable_Field, 5);
                //Assert.IsFalse(flag);
            }

            Assert.IsTrue(flag);
            return this;
        }

        public BusinessObjectFieldRecordPage ValidateDefaultValueFieldVisible()
        {
            WaitForElement(DefaultValue_Field);
            ScrollToElement(DefaultValue_Field);
            Assert.IsTrue(GetElementVisibility(DefaultValue_Field));
            return this;
        }


        public BusinessObjectFieldRecordPage ValidateBusinessObjectFieldsPageFields(bool labelNameVisible, bool maxLengthVisible, bool typeFieldVisible, string labelName, string toolTip, string optionVal, string fieldTypeVal, string maxLengthVal, bool searchableFieldExists, bool defaultValueFieldExists, string searchableFieldVal, string defaultValueFieldVal)
        {
            ValidateLabelNameMandatoryFieldSignVisible(labelNameVisible);
            ValidateMaxLengthMandatoryFieldSignVisible(maxLengthVisible);
            ValidateTypeMandatoryFieldSignVisible(typeFieldVisible);
            ValidateLabelNameFieldValue(labelName);
            ValidateToolTipFieldValue(toolTip);
            ValidateRequiredFieldValue(optionVal);
            ValidateFieldTypeFieldValue(fieldTypeVal);
            ValidateMaxLengthFieldValue(maxLengthVal);
            
            if (searchableFieldExists)
            {
                Assert.IsTrue(GetElementVisibility(IsSearchable_Field));
                Assert.IsTrue(GetElementVisibility(IsSearchableField_YesOption) || GetElementVisibility(IsSearchableField_YesOption));
            }

            if (defaultValueFieldExists)
            {
                Assert.IsTrue(GetElementVisibility(DefaultValue_Field));
                ValidateElementByTitle(DefaultValueField_NoOption, defaultValueFieldVal);
            }

            return this;
        }

        public BusinessObjectFieldRecordPage ValidateIsSearchableAndDefaultValueFields(string searchableFieldExists, string defaultValueFieldExists)
        {

            if (searchableFieldExists.Equals("yes"))
            {
                ScrollToElement(IsSearchable_Field);
                Assert.IsTrue(GetElementVisibility(IsSearchable_Field));
                Assert.IsTrue(GetElementVisibility(IsSearchableField_YesOption) || GetElementVisibility(IsSearchableField_NoOption));                
            }

            if (defaultValueFieldExists.Equals("yes"))
            {
                
                if (GetElementVisibility(DefaultValue_Field))
                {
                    ScrollToElement(DefaultValue_Field);
                    Assert.IsTrue(GetElementVisibility(DefaultValue_Field));                    
                } else
                    
                if (GetElementVisibility(DefaultValueDateField_Value))
                {
                    ScrollToElement(DefaultValueDateField_Value);
                    Assert.IsTrue(GetElementVisibility(DefaultValueDateField_Value));
                    ValidateElementChecked(DefaultValueDateField_Value);
                } else

                if (GetElementVisibility(DefaultValueField_NoOption))
                {
                    ScrollToElement(DefaultValueField_NoOption);
                    Assert.IsTrue(GetElementVisibility(DefaultValueField_NoOption));
                    ValidateElementChecked(DefaultValueField_NoOption);
                } else                

                if (GetElementVisibility(DefaultValueSelectField_Value))
                {
                    ScrollToElement(DefaultValueSelectField_Value);
                    Assert.IsTrue(GetElementVisibility(DefaultValueSelectField_Value));
                    ValidatePicklistSelectedText(DefaultValueSelectField_Value, "Draft");
                }

            }

            return this;
        }
        #endregion

    }
}
