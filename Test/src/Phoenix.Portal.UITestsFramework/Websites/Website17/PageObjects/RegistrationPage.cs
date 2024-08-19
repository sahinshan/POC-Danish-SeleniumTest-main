using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.Website17.PageObjects
{
    public class RegistrationPage : CommonMethods
    {
        public RegistrationPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

       
        readonly By pageHeader = By.XPath("//mosaic-page-header/div/div/div/h1[text()='Register an account']");
        
        readonly By toastMessage = By.XPath("//*[@class='toastify__message']");


        #region Field Titles

        readonly By emailAddressFieldTitle = By.XPath("//*[@id='CWUsername']/label[text()='Email Address']");
        readonly By passwordFieldTitle = By.XPath("//*[@id='CWPassword']/label[text()='Password']");
        readonly By repeatPasswordFieldTitle = By.XPath("//*[@id='CWRepeatPassword']/label[text()='Repeat Password']");
        readonly By HowTorecievePinFieldTitle = By.XPath("//*[@id='CWTwoFactorAuthenticationType']/label[text()='How to Receive PIN']");

        readonly By personalDetailsTopMessage = By.XPath("//*[@id='CWSection_PersonalDetails']/mosaic-card-body/div/mosaic-alert/div[2]/p[text()='(of the person requiring support if you are filling this in on their behalf)']");

        readonly By FirstNameFieldTitle = By.XPath("//*[@id='CWField_firstname']/label[text()='First Name']");
        readonly By LastNameFieldTitle = By.XPath("//*[@id='CWField_lastname']/label[text()='Last Name']");
        readonly By DateOfBirthFieldTitle = By.XPath("//*[@id='CWField_dateofbirth']/label[text()='Date of Birth']");
        readonly By GenderFieldTitle = By.XPath("//*[@id='CWField_genderid']/label[text()='Gender']");
        readonly By EthnicityFieldTitle = By.XPath("//*[@id='CWField_ethnicityid']/label[text()='Ethnicity']");
        readonly By NHSNoFieldTitle = By.XPath("//*[@id='CWField_nhsnumber']/label[text()='NHS No']");
        readonly By NationalInsuranceNumberFieldTitle = By.XPath("//*[@id='CWField_nationalinsurancenumber']/label[text()='National Insurance Number']");

        readonly By contactDetailsTopMessage = By.XPath("//*[@id='CWSection_ContactDetails']/mosaic-card-body/div/mosaic-alert/div[2]/p[text()='(of the person requiring support if you are filling this in on their behalf)']");

        readonly By PropertyNameFieldTitle = By.XPath("//*[@id='CWField_propertyname']/label[text()='Property Name']");
        readonly By PropertyNoFieldTitle = By.XPath("//*[@id='CWField_addressline1']/label[text()='Property No']");
        readonly By StreetFieldTitle = By.XPath("//*[@id='CWField_addressline2']/label[text()='Street']");
        readonly By VillageDistrictFieldTitle = By.XPath("//*[@id='CWField_addressline3']/label[text()='Village/District']");
        readonly By TownCityFieldTitle = By.XPath("//*[@id='CWField_addressline4']/label[text()='Town/City']");
        readonly By CountyFieldTitle = By.XPath("//*[@id='CWField_addressline5']/label[text()='County']");
        readonly By PostcodeFieldTitle = By.XPath("//*[@id='CWField_postcode']/label[text()='Postcode']");
        readonly By HomePhoneFieldTitle = By.XPath("//*[@id='CWField_homephone']/label[text()='Home Phone']");
        readonly By MobilePhoneFieldTitle = By.XPath("//*[@id='CWField_mobilephone']/label[text()='Mobile Phone']");

        #endregion


        readonly By emailAddressField = By.Id("CWUsername-input");
        readonly By emailAddressFieldWarning = By.XPath("//*[@id='CWEmailMessage']/div[2]");
        readonly By emailAddressFieldError = By.XPath("//*[@id='CWUsername']/div[3]/span");
        readonly By emailInUserFieldMessage = By.XPath("//*[@id='CWEmailInUseMessage']/div[2]");
        readonly By emailInUserSigninLink = By.XPath("//*[@id='CWEmailInUseMessage']/div[2]/mosaic-link/a");
        readonly By passwordField = By.Id("CWPassword-input");
        readonly By passwordFieldError = By.XPath("//*[@id='CWPassword']/div[3]/span");
        readonly By repeatPasswordField = By.Id("CWRepeatPassword-input");
        readonly By repeatPasswordFieldError = By.XPath("//*[@id='CWRepeatPassword']/div[3]/span");
        readonly By HowToReceivePINField = By.Id("CWTwoFactorAuthenticationType-field");
        readonly By HowToReceivePINFieldError = By.XPath("//*[@id='CWTwoFactorAuthenticationType']/div[3]/span");




        readonly By FirstNameField = By.Id("CWField_firstname-input");
        readonly By FirstNameFieldError = By.XPath("//*[@id='CWField_firstname']/div[2]/span");
        readonly By LastNameField = By.Id("CWField_lastname-input");
        readonly By LastNameFieldError = By.XPath("//*[@id='CWField_lastname']/div[2]/span");
        readonly By DateOfBirthField = By.XPath("//*[@id='CWField_dateofbirth']/div/input[@class='mc-form__control form-control input']");
        readonly By DateOfBirthFieldError = By.XPath("//*[@id='CWField_dateofbirth']/div[3]/span");

        readonly By GenderTopField = By.XPath("//*[@id='CWField_genderid']/div/div/span[1]");
        readonly By GenderSearchField = By.XPath("//*[@id='CWField_genderid']/div/div/div/input[@type='search']");
        By GenderResult(string Gender) => By.XPath("//*[@id='CWField_genderid']/div/div/div/div[text()='" + Gender + "']");
        readonly By GenderFieldError = By.XPath("//*[@id='CWField_genderid']/div[3]/span");

        readonly By EthnicityTopField = By.XPath("//*[@id='CWField_ethnicityid']/div/div/span[1]");
        readonly By EthnicitySearchField = By.XPath("//*[@id='CWField_ethnicityid']/div/div/div/input[@type='search']");
        By EthnicityResult(string Ethnicity) => By.XPath("//*[@id='CWField_ethnicityid']/div/div/div/div[text()='" + Ethnicity + "']");
        readonly By EthnicityFieldError = By.XPath("//*[@id='CWField_ethnicityid']/div[3]/span");

        readonly By NHSNoField = By.Id("CWField_nhsnumber-input");
        readonly By NationalInsuranceNumberField = By.Id("CWField_nationalinsurancenumber-input");
        


        readonly By PropertyNameField = By.Id("CWField_propertyname-input");
        readonly By PropertyNoField = By.Id("CWField_addressline1-input");
        readonly By StreetField = By.Id("CWField_addressline2-input");
        readonly By VillageDistrictField = By.Id("CWField_addressline3-input");
        readonly By TownCityField = By.Id("CWField_addressline4-input");
        readonly By CountyField = By.Id("CWField_addressline5-input");
        readonly By PostcodeField = By.Id("CWField_postcode-input");
        readonly By PostcodeFieldError = By.XPath("//*[@id='CWField_postcode']/div[2]/span");
        readonly By HomePhoneField = By.Id("CWField_homephone-input");
        readonly By MobilePhoneField = By.Id("CWField_mobilephone-input");
        readonly By MobilePhoneFieldError = By.Id("//*[@id='CWField_mobilephone']/div[3]/span");




        readonly By PasswordPromptTitle = By.XPath("//*[@id='PasswordPromptTitle']");
        readonly By PasswordPromptMinUpper = By.XPath("//*[@id='PasswordPromptMinUpper']");
        readonly By PasswordPromptMinNum = By.XPath("//*[@id='PasswordPromptMinNum']");
        readonly By PasswordPromptMinSpecialChar = By.XPath("//*[@id='PasswordPromptMinSpecialChar']");
        readonly By PasswordPromptMinLength = By.XPath("//*[@id='PasswordPromptMinLength']");

        readonly By PasswordPromptMinUpperIcon = By.XPath("//*[@id='checkMinUpper']");
        readonly By PasswordPromptMinNumIcon = By.XPath("//*[@id='checkMinNum']");
        readonly By PasswordPromptMinSpecialCharIcon = By.XPath("//*[@id='checkMinSpecialChar']");
        readonly By PasswordPromptMinLengthIcon = By.XPath("//*[@id='checkMinLength']");


        readonly By registerButton = By.XPath("//*[@id='CWRegistrationButton']/button");

        readonly By termsAndConditionsMessage = By.XPath("//*[@id='CWRegistrationFooter']/p[text()='By clicking the Register button you agree to our ']");
        readonly By termsAndConditionsLink = By.XPath("//*[@id='CWRegistrationFooter']/p/a[text()='Terms and Conditions']");


        readonly By CalendarMonthSelect = By.XPath("//*[@id='CWField_dateofbirth']/div/div/div/div/select");
        readonly By CalendarYearInput = By.XPath("//*[@id='CWField_dateofbirth']/div/div/div/div/div/input[@aria-label='Year']");
        By CalendarDaySpan(string AriaLabel) => By.XPath("//*[@id='CWField_dateofbirth']/div/div/div/div/div/span[@aria-label='" + AriaLabel + "']");

        public RegistrationPage WaitForRegistrationPageToLoad()
        {
            WaitForElement(pageHeader);


            WaitForElement(emailAddressFieldTitle);
            WaitForElement(passwordFieldTitle);
            WaitForElement(repeatPasswordFieldTitle);
            WaitForElement(HowTorecievePinFieldTitle);

            WaitForElement(personalDetailsTopMessage);

            WaitForElement(FirstNameFieldTitle);
            WaitForElement(LastNameFieldTitle);
            WaitForElement(DateOfBirthFieldTitle);
            WaitForElement(GenderFieldTitle);
            WaitForElement(EthnicityFieldTitle);
            WaitForElement(NHSNoFieldTitle);
            WaitForElement(NationalInsuranceNumberFieldTitle);

            WaitForElement(contactDetailsTopMessage);

            WaitForElement(PropertyNameFieldTitle);
            WaitForElement(PropertyNoFieldTitle);
            WaitForElement(StreetFieldTitle);
            WaitForElement(VillageDistrictFieldTitle);
            WaitForElement(TownCityFieldTitle);
            WaitForElement(CountyFieldTitle);
            WaitForElement(PostcodeFieldTitle);
            WaitForElement(HomePhoneFieldTitle);
            WaitForElement(MobilePhoneFieldTitle);

            WaitForElement(registerButton);
            WaitForElement(termsAndConditionsMessage);
            WaitForElement(termsAndConditionsLink);


            return this;
        }



        public RegistrationPage WaitForToastMessageVisible()
        {
            WaitForElementVisible(toastMessage);

            return this;
        }
        public RegistrationPage ValidateForToastMessageText(string ExpectedText)
        {
            ValidateElementText(toastMessage, ExpectedText);

            return this;
        }


        public RegistrationPage SelectHowToreceivePIN(string TextToSelect)
        {
            SetElementDisplayStyleToInline("CWTwoFactorAuthenticationType-field");

            SelectPicklistElementByText(HowToReceivePINField, TextToSelect);

            return this;
        }


        public RegistrationPage WaitForCalendarToLoad()
        {
            WaitForElement(CalendarMonthSelect);
            WaitForElement(CalendarYearInput);

            return this;
        }
        public RegistrationPage WaitForCalendarNotVisible()
        {
            WaitForElementNotVisible(CalendarMonthSelect, 5);
            WaitForElementNotVisible(CalendarYearInput, 5);

            return this;
        }
        public RegistrationPage CalendarSelectMonth(string TextToSelect)
        {
            SelectPicklistElementByText(CalendarMonthSelect, TextToSelect);

            return this;
        }
        public RegistrationPage CalendarInsertYear(string TextToInsert)
        {
            SendKeys(CalendarYearInput, TextToInsert);

            return this;
        }
        public RegistrationPage CalendarClickOnDaySpan(string AriaLabel)
        {
            WaitForElement(CalendarDaySpan(AriaLabel));
            Click(CalendarDaySpan(AriaLabel));

            return this;
        }



        public RegistrationPage ClickOnGenderTopField()
        {
            Click(GenderTopField);

            return this;
        }
        public RegistrationPage ClickOnGenderOption(string Gender)
        {
            WaitForElement(GenderResult(Gender));
            Click(GenderResult(Gender));

            return this;
        }
        public RegistrationPage ClickOnEthnicityTopField()
        {
            Click(EthnicityTopField);

            return this;
        }
        public RegistrationPage ClickOnEthnicityOption(string Ethnicity)
        {
            Click(EthnicityResult(Ethnicity));

            return this;
        }
        public RegistrationPage ClickOnRegisterButton()
        {
            Click(registerButton);

            return this;
        }
        public RegistrationPage ClickOnPasswordField()
        {
            Click(passwordField);

            return this;
        }
        public RegistrationPage ClickDateOfBirthField()
        {
            Click(DateOfBirthField);

            return this;
        }
        public RegistrationPage ClickEmailInUserSigninLink()
        {
            Click(emailInUserSigninLink);

            return this;
        }



        public RegistrationPage InsertEmailAddress(string TextToInsert)
        {
            SendKeys(emailAddressField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertPassword(string TextToInsert)
        {
            SendKeys(passwordField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertRepeatPassword(string TextToInsert)
        {
            SendKeys(repeatPasswordField, TextToInsert);

            return this;
        }



        public RegistrationPage InsertFirstName(string TextToInsert)
        {
            SendKeys(FirstNameField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertLastName(string TextToInsert)
        {
            SendKeys(LastNameField, TextToInsert);

            return this;
        }
        
        public RegistrationPage InsertGenderSearchText(string TextToInsert)
        {
            WaitForElement(GenderSearchField);

            SendKeys(GenderSearchField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertEthnicitySearchText(string TextToInsert)
        {
            WaitForElement(EthnicitySearchField);

            SendKeys(EthnicitySearchField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertNHSNo(string TextToInsert)
        {
            SendKeys(NHSNoField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertNationalInsuranceNumber(string TextToInsert)
        {
            SendKeys(NationalInsuranceNumberField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertPropertyName(string TextToInsert)
        {
            SendKeys(PropertyNameField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertPropertyNo(string TextToInsert)
        {
            SendKeys(PropertyNoField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertStreet(string TextToInsert)
        {
            SendKeys(StreetField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertVillageDistrict(string TextToInsert)
        {
            SendKeys(VillageDistrictField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertTownCity(string TextToInsert)
        {
            SendKeys(TownCityField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertCounty(string TextToInsert)
        {
            SendKeys(CountyField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertPostcode(string TextToInsert)
        {
            SendKeys(PostcodeField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertHomePhone(string TextToInsert)
        {
            SendKeys(HomePhoneField, TextToInsert);

            return this;
        }
        public RegistrationPage InsertMobilePhone(string TextToInsert)
        {
            SendKeys(MobilePhoneField, TextToInsert);

            return this;
        }




        public RegistrationPage ValidatePasswordPromptTitleVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptTitle);
            else
                WaitForElementNotVisible(PasswordPromptTitle, 7);

            return this;
        }
        public RegistrationPage ValidatePasswordPromptMinUpperVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinUpper);
            else
                WaitForElementNotVisible(PasswordPromptMinUpper, 7);

            return this;
        }
        public RegistrationPage ValidatePasswordPromptMinNumVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinNum);
            else
                WaitForElementNotVisible(PasswordPromptMinNum, 7);

            return this;
        }
        public RegistrationPage ValidatePasswordPromptMinSpecialCharVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinSpecialChar);
            else
                WaitForElementNotVisible(PasswordPromptMinSpecialChar, 7);

            return this;
        }
        public RegistrationPage ValidatePasswordPromptMinLengthVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PasswordPromptMinLength);
            else
                WaitForElementNotVisible(PasswordPromptMinLength, 7);

            return this;
        }


        public RegistrationPage ValidatePasswordPromptTitleText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptTitle, ExpectedText);

            return this;
        }
        public RegistrationPage ValidatePasswordPromptMinUpperText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinUpper, ExpectedText);

            return this;
        }
        public RegistrationPage ValidatePasswordPromptMinNumText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinNum, ExpectedText);

            return this;
        }
        public RegistrationPage ValidatePasswordPromptMinSpecialCharText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinSpecialChar, ExpectedText);

            return this;
        }
        public RegistrationPage ValidatePasswordPromptMinLengthText(string ExpectedText)
        {
            ValidateElementText(PasswordPromptMinLength, ExpectedText);

            return this;
        }



        public RegistrationPage ValidatePasswordPromptMinUpperIconValid(bool ExpectValid)
        {
            if (ExpectValid)
                this.ValidateElementAttributeValue(PasswordPromptMinUpperIcon, "color", "success");
            else
                this.ValidateElementAttributeValue(PasswordPromptMinUpperIcon, "color", "danger");

            return this;
        }
        public RegistrationPage ValidatePasswordPromptMinNumIconValid(bool ExpectValid)
        {
            if (ExpectValid)
                this.ValidateElementAttributeValue(PasswordPromptMinNumIcon, "color", "success");
            else
                this.ValidateElementAttributeValue(PasswordPromptMinNumIcon, "color", "danger");

            return this;
        }
        public RegistrationPage ValidatePasswordPromptMinSpecialCharIconValid(bool ExpectValid)
        {
            if (ExpectValid)
                this.ValidateElementAttributeValue(PasswordPromptMinSpecialCharIcon, "color", "success");
            else
                this.ValidateElementAttributeValue(PasswordPromptMinSpecialCharIcon, "color", "danger");

            return this;
        }
        public RegistrationPage ValidatePasswordPromptMinLengthIconValid(bool ExpectValid)
        {
            if (ExpectValid)
                this.ValidateElementAttributeValue(PasswordPromptMinLengthIcon, "color", "success");
            else
                this.ValidateElementAttributeValue(PasswordPromptMinLengthIcon, "color", "danger");

            return this;
        }




        public RegistrationPage ValidateEmailAddressFieldWarningVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(emailAddressFieldWarning);
            else
                WaitForElementNotVisible(emailAddressFieldWarning, 7);

            return this;
        }
        public RegistrationPage ValidateEmailAddressFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(emailAddressFieldError);
            else
                WaitForElementNotVisible(emailAddressFieldError, 7);

            return this;
        }
        public RegistrationPage ValidateEmailInUserFieldMessageVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(emailInUserFieldMessage);
            else
                WaitForElementNotVisible(emailInUserFieldMessage, 7);

            return this;
        }
        public RegistrationPage ValidateEmailInUserSigninLinkVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(emailInUserSigninLink);
            else
                WaitForElementNotVisible(emailInUserSigninLink, 7);

            return this;
        }
        public RegistrationPage ValidatePasswordFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(passwordFieldError);
            else
                WaitForElementNotVisible(passwordFieldError, 7);

            return this;
        }
        public RegistrationPage ValidateRepeatPasswordFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(repeatPasswordFieldError);
            else
                WaitForElementNotVisible(repeatPasswordFieldError, 7);

            return this;
        }
        public RegistrationPage ValidateHowToReceivePINFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(HowToReceivePINFieldError);
            else
                WaitForElementNotVisible(HowToReceivePINFieldError, 7);

            return this;
        }
        public RegistrationPage ValidateFirstNameFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(FirstNameFieldError);
            else
                WaitForElementNotVisible(FirstNameFieldError, 7);

            return this;
        }
        public RegistrationPage ValidateLastNameFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LastNameFieldError);
            else
                WaitForElementNotVisible(LastNameFieldError, 7);

            return this;
        }
        public RegistrationPage ValidateDateOfBirthFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DateOfBirthFieldError);
            else
                WaitForElementNotVisible(DateOfBirthFieldError, 7);

            return this;
        }
        public RegistrationPage ValidateGenderFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(GenderFieldError);
            else
                WaitForElementNotVisible(GenderFieldError, 7);

            return this;
        }
        public RegistrationPage ValidateEthnicityFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EthnicityFieldError);
            else
                WaitForElementNotVisible(EthnicityFieldError, 7);

            return this;
        }
        public RegistrationPage ValidatePostcodeFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PostcodeFieldError);
            else
                WaitForElementNotVisible(PostcodeFieldError, 7);

            return this;
        }
        public RegistrationPage ValidateMobilePhoneFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MobilePhoneFieldError);
            else
                WaitForElementNotVisible(MobilePhoneFieldError, 7);

            return this;
        }


        public RegistrationPage ValidateEmailAddressFieldErrorText(string ExpectedText)
        {
            ValidateElementText(emailAddressFieldError, ExpectedText);

            return this;
        }
        public RegistrationPage ValidateEmailInUserFieldMessageText(string ExpectedText)
        {
            ValidateElementText(emailInUserFieldMessage, ExpectedText);

            return this;
        }
        public RegistrationPage ValidateEmailInUserSigninLinkText(string ExpectedText)
        {
            ValidateElementText(emailInUserSigninLink, ExpectedText);

            return this;
        }
        public RegistrationPage ValidatePasswordFieldErrorText(string ExpectedText)
        {
            ValidateElementText(passwordFieldError, ExpectedText);

            return this;
        }
        public RegistrationPage ValidateRepeatPasswordFieldErrorText(string ExpectedText)
        {
            ValidateElementText(repeatPasswordFieldError, ExpectedText);

            return this;
        }
        public RegistrationPage ValidateHowToReceivePINFieldErrorText(string ExpectedText)
        {
            ValidateElementText(HowToReceivePINFieldError, ExpectedText);

            return this;
        }
        public RegistrationPage ValidateFirstNameFieldErrorText(string ExpectedText)
        {
            ValidateElementText(FirstNameFieldError, ExpectedText);

            return this;
        }
        public RegistrationPage ValidateLastNameFieldErrorText(string ExpectedText)
        {
            ValidateElementText(LastNameFieldError, ExpectedText);

            return this;
        }
        public RegistrationPage ValidateDateOfBirthFieldErrorText(string ExpectedText)
        {
            ValidateElementText(DateOfBirthFieldError, ExpectedText);

            return this;
        }
        public RegistrationPage ValidateGenderFieldErrorText(string ExpectedText)
        {
            ValidateElementText(GenderFieldError, ExpectedText);

            return this;
        }
        public RegistrationPage ValidateEthnicityFieldErrorText(string ExpectedText)
        {
            ValidateElementText(EthnicityFieldError, ExpectedText);

            return this;
        }
        public RegistrationPage ValidatePostcodeFieldErrorText(string ExpectedText)
        {
            ValidateElementText(PostcodeFieldError, ExpectedText);

            return this;
        }
        public RegistrationPage ValidateMobilePhoneFieldErrorText(string ExpectedText)
        {
            ValidateElementText(MobilePhoneFieldError, ExpectedText);

            return this;
        }

    }
    
}
