using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class StaffReviewRequirementsRecordPage : CommonMethods
    {
        public StaffReviewRequirementsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContent_Iframe = By.Id("CWContentIFrame");
        readonly By staffReviewSetup_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=staffreviewsetup&')]");

        readonly By staffReviewFormId_LookUP = By.Id("CWLookupBtn_staffreviewformid");
        readonly By businessUnits_LookUP = By.Id("CWLookupBtn_businessunits");
        readonly By roles_LookUP = By.Id("CWLookupBtn_role");
        readonly By bookingTypeField_Lookup = By.Id("CWLookupBtn_bookingtypeid");

        readonly By description_TextField = By.Id("CWField_description");
        readonly By bookingType_Field = By.Id("CWField_bookingtypeid_cwname");
        readonly By bookingType_FieldLinkText = By.Id("CWField_bookingtypeid_Link");

        readonly By validateForm_DateField = By.Id("CWField_validfrom");
        readonly By validateForm_Mandatory = By.XPath("//label[text()='Valid From']/span");
        readonly By validTo_DateField = By.Id("CWField_validto");
        readonly By validTo_Mandatory = By.Id("//label[text()='Valid To']/span");

        readonly By NameField_Label = By.XPath("//label[text()='Name']");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By createOnDemandWorkflowJobButton = By.Id("TI_RunOnDemandWorkflow");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By AllRoles_RadioButton = By.XPath("//*[@id='CWField_allroles_1']");

        public StaffReviewRequirementsRecordPage WaitForStaffReviewRequirementsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElement(staffReviewSetup_Iframe);
            SwitchToIframe(staffReviewSetup_Iframe);

            WaitForElement(staffReviewFormId_LookUP);

            WaitForElement(roles_LookUP);

            WaitForElement(description_TextField);

            WaitForElement(validateForm_DateField);

            return this;
        }

        public StaffReviewRequirementsRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(deleteButton);
            WaitForElementVisible(createOnDemandWorkflowJobButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public StaffReviewRequirementsRecordPage ClickStaffReviewFormIdLookUP()
        {
            WaitForElement(staffReviewFormId_LookUP);
            Click(staffReviewFormId_LookUP);

            return this;
        }
        public StaffReviewRequirementsRecordPage ClickBusinessUnitsLookUP()
        {
            WaitForElement(businessUnits_LookUP);
            Click(businessUnits_LookUP);

            return this;
        }
        public StaffReviewRequirementsRecordPage ClickRolesLookUP()
        {
            WaitForElement(roles_LookUP);
            Click(roles_LookUP);

            return this;
        }

        public StaffReviewRequirementsRecordPage ClickBookingTypeLookup()
        {
            WaitForElementToBeClickable(bookingTypeField_Lookup);
            ScrollToElement(bookingTypeField_Lookup);
            Click(bookingTypeField_Lookup);

            return this;
        }

        public StaffReviewRequirementsRecordPage InsertValidateForm(String Keys)
        {
            WaitForElement(validateForm_DateField);
            SendKeys(validateForm_DateField, Keys);

            return this;
        }
        public StaffReviewRequirementsRecordPage InsertValidTo(String Keys)
        {
            WaitForElement(validTo_DateField);
            SendKeys(validTo_DateField, Keys);

            return this;
        }
        public StaffReviewRequirementsRecordPage InsertDescriptionForm(String Keys)
        {
            WaitForElement(description_TextField);
            SendKeys(description_TextField, Keys);

            return this;
        }

        public StaffReviewRequirementsRecordPage ValidateNameFieldNotPresent()
        {
            Assert.IsFalse(GetElementVisibility(NameField_Label));

            return this;
        }
        public StaffReviewRequirementsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public StaffReviewRequirementsRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            return this;
        }

        public StaffReviewRequirementsRecordPage ClickAllRolesRadioButton()
        {
            WaitForElementToBeClickable(AllRoles_RadioButton);
            Click(AllRoles_RadioButton);

            return this;
        }
        public StaffReviewRequirementsRecordPage Validate_ValidateFormEditable()
        {
            WaitForElement(validateForm_DateField);
            ValidateElementEnabled(validateForm_DateField);

            return this;
        }
        public StaffReviewRequirementsRecordPage Validate_ValidateFormPreFilledTodayDate(string ExpectedText)
        {
            WaitForElement(validateForm_DateField);
            ValidateElementValue(validateForm_DateField, ExpectedText);

            return this;
        }
        public StaffReviewRequirementsRecordPage Validate_ValidateFormFieldMandatory(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(validateForm_Mandatory);
            }
            else
            {
                WaitForElementNotVisible(validateForm_Mandatory, 3);
            }

            return this;
        }
        public StaffReviewRequirementsRecordPage Validate_ValidToFieldNonMandatory(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(validTo_Mandatory);
            }
            else
            {
                WaitForElementNotVisible(validTo_Mandatory, 3);
            }

            return this;
        }

        public StaffReviewRequirementsRecordPage ValidateBookingTypeFieldLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(bookingType_FieldLinkText);
            ValidateElementByTitle(bookingType_FieldLinkText, ExpectedText);

            return this;
        }

        public StaffReviewRequirementsRecordPage ValidatStaffReviewRequirementsRecordPageTitle(string PageTitle)
        {
            ValidateElementTextContainsText(pageHeader, PageTitle);
            return this;
        }
    }
}
