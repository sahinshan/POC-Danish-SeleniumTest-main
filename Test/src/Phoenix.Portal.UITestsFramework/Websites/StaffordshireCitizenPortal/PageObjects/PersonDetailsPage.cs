using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class PersonDetailsPage : CommonMethods
    {
        public PersonDetailsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        #region Field Titles

        readonly By personalDetailsTopMessage = By.XPath("//*[@id='CWSection_PersonalDetails']/mosaic-card-body/div/mosaic-alert/div[2]/p[text()='The details of the person requiring support, if you are filling this in on their behalf']");

        readonly By FirstNameFieldTitle = By.XPath("//*[@id='CWField_firstname']/label[text()='First Name']");
        readonly By LastNameFieldTitle = By.XPath("//*[@id='CWField_lastname']/label[text()='Last Name']");
        readonly By DateOfBirthFieldTitle = By.XPath("//*[@id='CWField_dateofbirth']/label[text()='Date of Birth']");
        readonly By GenderFieldTitle = By.XPath("//*[@id='CWField_genderid']/label[text()='Gender']");
        readonly By EthnicityFieldTitle = By.XPath("//*[@id='CWField_ethnicityid']/label[text()='Ethnicity']");
        readonly By NHSNoFieldTitle = By.XPath("//*[@id='CWField_nationalinsurancenumber']/label[text()='National Insurance Number']");
        readonly By NationalInsuranceNumberFieldTitle = By.XPath("//*[@id='CWField_nationalinsurancenumber']/label[text()='National Insurance Number']");

        readonly By contactDetailsTopMessage = By.XPath("//*[@id='CWSection_ContactDetails']/mosaic-card-body/div/mosaic-alert/div[2]/p[text()='The details of the person requiring support, if you are filling this in on their behalf']");

        readonly By PropertyNameFieldTitle = By.XPath("//*[@id='CWField_propertyname']/label[text()='Property Name']");
        readonly By PropertyNoFieldTitle = By.XPath("//*[@id='CWField_addressline1']/label[text()='Property No.']");
        readonly By StreetFieldTitle = By.XPath("//*[@id='CWField_addressline2']/label[text()='Street']");
        readonly By VillageDistrictFieldTitle = By.XPath("//*[@id='CWField_addressline3']/label[text()='Village/District']");
        readonly By TownCityFieldTitle = By.XPath("//*[@id='CWField_addressline4']/label[text()='Town/City']");
        readonly By CountyFieldTitle = By.XPath("//*[@id='CWField_addressline5']/label[text()='County']");
        readonly By PostcodeFieldTitle = By.XPath("//*[@id='CWField_postcode']/label[text()='Postcode']");
        readonly By HomePhoneFieldTitle = By.XPath("//*[@id='CWField_homephone']/label[text()='Home Phone Number']");
        readonly By MobilePhoneFieldTitle = By.XPath("//*[@id='CWField_mobilephone']/label[text()='Mobile Phone']");

        #endregion


        readonly By FirstNameMosaicField = By.Id("CWField_firstname");
        readonly By FirstNameField = By.Id("CWField_firstname-input");
        readonly By FirstNameFieldError = By.XPath("//*[@id='CWField_firstname']/div[2]/span");
        readonly By LastNameMosaicField = By.Id("CWField_lastname");
        readonly By LastNameField = By.Id("CWField_lastname-input");
        readonly By LastNameFieldError = By.XPath("//*[@id='CWField_lastname']/div[2]/span");
        readonly By DateOfBirthHiddenField = By.XPath("//*[@id='CWField_dateofbirth-input']");
        readonly By DateOfBirthVisibleField = By.XPath("//*[@id='CWField_dateofbirth']/div/input[@type='text']");
        readonly By DateOfBirthField = By.XPath("//*[@id='CWField_dateofbirth']/div/input[@class='mc-form__control form-control input']");
        readonly By DateOfBirthFieldError = By.XPath("//*[@id='CWField_dateofbirth']/div[3]/span");
        
        readonly By GenderSelectedValue = By.XPath("//*[@id='CWField_genderid']/div/div[@class='ss-single-selected']/span[@class='placeholder']");
        readonly By GenderPicklist = By.XPath("//*[@id='CWField_genderid-field']");
        readonly By GenderTopField = By.XPath("//*[@id='CWField_genderid']/div/div/span[1]");
        readonly By GenderSearchField = By.XPath("//*[@id='CWField_genderid']/div/div/div/input[@type='search']");
        By GenderResult(string Gender) => By.XPath("//*[@id='CWField_genderid']/div/div/div/div[text()='" + Gender + "']");
        readonly By GenderFieldError = By.XPath("//*[@id='CWField_genderid']/div[3]/span");

        readonly By EthnicitySelectedValue = By.XPath("//*[@id='CWField_ethnicityid']/div/div[@class='ss-single-selected']/span[@class='placeholder']");
        readonly By EthnicityPicklist = By.XPath("//*[@id='CWField_ethnicityid-field']");
        readonly By EthnicityTopField = By.XPath("//*[@id='CWField_ethnicityid']/div/div/span[1]");
        readonly By EthnicitySearchField = By.XPath("//*[@id='CWField_ethnicityid']/div/div/div/input[@type='search']");
        By EthnicityResult(string Ethnicity) => By.XPath("//*[@id='CWField_ethnicityid']/div/div/div/div[text()='" + Ethnicity + "']");
        readonly By EthnicityFieldError = By.XPath("//*[@id='CWField_ethnicityid']/div[3]/span");
        readonly By NHSNoMosaicField = By.Id("CWField_nationalinsurancenumber");
        readonly By NHSNoField = By.Id("CWField_nationalinsurancenumber-input");
        readonly By NationalInsuranceNumberMosaicField = By.Id("CWField_nationalinsurancenumber");
        readonly By NationalInsuranceNumberField = By.Id("CWField_nationalinsurancenumber-input");
        


        readonly By PropertyNameMosaicField = By.Id("CWField_propertyname");
        readonly By PropertyNameField = By.Id("CWField_propertyname-input");
        readonly By PropertyNoMosaicField = By.Id("CWField_addressline1");
        readonly By PropertyNoField = By.Id("CWField_addressline1-input");
        readonly By StreetMosaicField = By.Id("CWField_addressline2");
        readonly By StreetField = By.Id("CWField_addressline2-input");
        readonly By VillageDistrictMosaicField = By.Id("CWField_addressline3");
        readonly By VillageDistrictField = By.Id("CWField_addressline3-input");
        readonly By TownCityMosaicField = By.Id("CWField_addressline4");
        readonly By TownCityField = By.Id("CWField_addressline4-input");
        readonly By CountyMosaicField = By.Id("CWField_addressline5");
        readonly By CountyField = By.Id("CWField_addressline5-input");
        readonly By PostcodeMosaicField = By.Id("CWField_postcode");
        readonly By PostcodeField = By.Id("CWField_postcode-input");
        readonly By PostcodeFieldError = By.XPath("//*[@id='CWField_postcode']/div[2]/span");
        readonly By HomePhoneMosaicField = By.Id("CWField_homephone-input");
        readonly By HomePhoneField = By.Id("CWField_homephone-input");
        readonly By MobilePhoneMosaicField = By.Id("CWField_mobilephone-input");
        readonly By MobilePhoneField = By.Id("CWField_mobilephone-input");
        readonly By MobilePhoneFieldError = By.Id("//*[@id='CWField_mobilephone']/div[3]/span");


        readonly By SubmitButton = By.XPath("//*[@id='CWDataFormSubmitButton']");


        readonly By CalendarMonthSelect = By.XPath("//*[@id='CWField_dateofbirth']/div/div/div/div/select");
        readonly By CalendarYearInput = By.XPath("//*[@id='CWField_dateofbirth']/div/div/div/div/div/input[@aria-label='Year']");
        By CalendarDaySpan(string AriaLabel) => By.XPath("//*[@id='CWField_dateofbirth']/div/div/div/div/div/span[@aria-label='" + AriaLabel + "']");


        readonly By ToastMessage = By.XPath("//div[@class='toastify__message']");
        readonly By ToastMessageCloseButton = By.XPath("//span[@class='toast-close']");



        public PersonDetailsPage WaitForPersonDetailsPageToLoad()
        {
            this.driver.Navigate().Refresh();

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

            WaitForElement(SubmitButton);


            return this;
        }


        public PersonDetailsPage ValidateToastMessageVisible()
        {
            WaitForElementVisible(ToastMessage);
            WaitForElementVisible(ToastMessageCloseButton);

            return this;
        }
        public PersonDetailsPage ValidateToastMessageText(string ExpectedText)
        {
            ValidateElementText(ToastMessage, ExpectedText);

            return this;
        }



        public PersonDetailsPage WaitForCalendarToLoad()
        {
            WaitForElementToBeClickable(CalendarMonthSelect);
            WaitForElementToBeClickable(CalendarYearInput);

            return this;
        }
        public PersonDetailsPage WaitForCalendarNotVisible()
        {
            WaitForElementNotVisible(CalendarMonthSelect, 5);
            WaitForElementNotVisible(CalendarYearInput, 5);

            return this;
        }
        public PersonDetailsPage CalendarSelectMonth(string TextToSelect)
        {
            WaitForElementToBeClickable(CalendarMonthSelect);
            SelectPicklistElementByText(CalendarMonthSelect, TextToSelect);

            return this;
        }
        public PersonDetailsPage CalendarInsertYear(string TextToInsert)
        {
            WaitForElementToBeClickable(CalendarYearInput);
            SendKeys(CalendarYearInput, TextToInsert);

            return this;
        }
        public PersonDetailsPage CalendarClickOnDaySpan(string AriaLabel)
        {
            WaitForElementToBeClickable(CalendarDaySpan(AriaLabel));
            Click(CalendarDaySpan(AriaLabel));

            return this;
        }



       


        public PersonDetailsPage ClickOnGenderTopField()
        {
            Click(GenderTopField);

            return this;
        }
        public PersonDetailsPage ClickOnGenderOption(string Gender)
        {
            WaitForElement(GenderResult(Gender));
            Click(GenderResult(Gender));

            return this;
        }
        public PersonDetailsPage ClickOnEthnicityTopField()
        {
            Click(EthnicityTopField);

            return this;
        }
        public PersonDetailsPage ClickOnEthnicityOption(string Ethnicity)
        {
            Click(EthnicityResult(Ethnicity));

            return this;
        }
        public PersonDetailsPage ClickOnSubmitButton()
        {
            WaitForElementToBeClickable(SubmitButton);
            Click(SubmitButton);

            return this;
        }
        public PersonDetailsPage ClickDateOfBirthField()
        {
            Click(DateOfBirthField);

            return this;
        }
        





        public PersonDetailsPage InsertFirstName(string TextToInsert)
        {
            SendKeys(FirstNameField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertLastName(string TextToInsert)
        {
            SendKeys(LastNameField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertGenderSearchText(string TextToInsert)
        {
            WaitForElement(GenderSearchField);

            SendKeys(GenderSearchField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertEthnicitySearchText(string TextToInsert)
        {
            WaitForElement(EthnicitySearchField);

            SendKeys(EthnicitySearchField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertNHSNo(string TextToInsert)
        {
            SendKeys(NHSNoField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertNationalInsuranceNumber(string TextToInsert)
        {
            SendKeys(NationalInsuranceNumberField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertPropertyName(string TextToInsert)
        {
            SendKeys(PropertyNameField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertPropertyNo(string TextToInsert)
        {
            SendKeys(PropertyNoField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertStreet(string TextToInsert)
        {
            SendKeys(StreetField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertVillageDistrict(string TextToInsert)
        {
            SendKeys(VillageDistrictField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertTownCity(string TextToInsert)
        {
            SendKeys(TownCityField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertCounty(string TextToInsert)
        {
            SendKeys(CountyField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertPostcode(string TextToInsert)
        {
            SendKeys(PostcodeField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertHomePhone(string TextToInsert)
        {
            SendKeys(HomePhoneField, TextToInsert);

            return this;
        }
        public PersonDetailsPage InsertMobilePhone(string TextToInsert)
        {
            SendKeys(MobilePhoneField, TextToInsert);
            SendKeysWithoutClearing(MobilePhoneField, Keys.Tab);

            return this;
        }
        public PersonDetailsPage InsertDateOfBirth(string ValueToInsert)
        {
            SendKeys(DateOfBirthVisibleField, ValueToInsert);

            return this;
        }





        public PersonDetailsPage ValidateFirstName(string ExpectedText)
        {
            ValidateElementValue(FirstNameMosaicField, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateLastName(string ExpectedText)
        {
            ValidateElementValue(LastNameMosaicField, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateDateOfBirth(string ExpectedValue)
        {
            ValidateElementValue(DateOfBirthHiddenField, ExpectedValue);

            return this;
        }
        public PersonDetailsPage ValidateGenderSelectedText(string ExpectedText)
        {
            WaitForElementVisible(GenderSelectedValue);
            ValidateElementText(GenderSelectedValue, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateEthnicitySelectedText(string ExpectedText)
        {
            ValidateElementText(EthnicitySelectedValue, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateNHSNo(string ExpectedText)
        {
            ValidateElementValue(NHSNoMosaicField, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateNationalInsuranceNumber(string ExpectedText)
        {
            ValidateElementValue(NationalInsuranceNumberMosaicField, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidatePropertyName(string ExpectedText)
        {
            ValidateElementValue(PropertyNameMosaicField, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidatePropertyNo(string ExpectedText)
        {
            ValidateElementValue(PropertyNoMosaicField, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateStreet(string ExpectedText)
        {
            ValidateElementValue(StreetMosaicField, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateVillageDistrict(string ExpectedText)
        {
            ValidateElementValue(VillageDistrictMosaicField, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateTownCity(string ExpectedText)
        {
            ValidateElementValue(TownCityMosaicField, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateCounty(string ExpectedText)
        {
            ValidateElementValue(CountyMosaicField, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidatePostcode(string ExpectedText)
        {
            ValidateElementValue(PostcodeMosaicField, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateHomePhone(string ExpectedText)
        {
            ValidateElementValue(HomePhoneMosaicField, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateMobilePhone(string ExpectedText)
        {
            ValidateElementValue(MobilePhoneMosaicField, ExpectedText);

            return this;
        }






        public PersonDetailsPage ValidateFirstNameFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(FirstNameFieldError);
            else
                WaitForElementNotVisible(FirstNameFieldError, 7);

            return this;
        }
        public PersonDetailsPage ValidateLastNameFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LastNameFieldError);
            else
                WaitForElementNotVisible(LastNameFieldError, 7);

            return this;
        }
        public PersonDetailsPage ValidateDateOfBirthFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DateOfBirthFieldError);
            else
                WaitForElementNotVisible(DateOfBirthFieldError, 7);

            return this;
        }
        public PersonDetailsPage ValidateGenderFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(GenderFieldError);
            else
                WaitForElementNotVisible(GenderFieldError, 7);

            return this;
        }
        public PersonDetailsPage ValidateEthnicityFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EthnicityFieldError);
            else
                WaitForElementNotVisible(EthnicityFieldError, 7);

            return this;
        }
        public PersonDetailsPage ValidatePostcodeFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PostcodeFieldError);
            else
                WaitForElementNotVisible(PostcodeFieldError, 7);

            return this;
        }



        
        public PersonDetailsPage ValidateFirstNameFieldErrorText(string ExpectedText)
        {
            ValidateElementText(FirstNameFieldError, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateLastNameFieldErrorText(string ExpectedText)
        {
            ValidateElementText(LastNameFieldError, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateDateOfBirthFieldErrorText(string ExpectedText)
        {
            ValidateElementText(DateOfBirthFieldError, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateGenderFieldErrorText(string ExpectedText)
        {
            ValidateElementText(GenderFieldError, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateEthnicityFieldErrorText(string ExpectedText)
        {
            ValidateElementText(EthnicityFieldError, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidatePostcodeFieldErrorText(string ExpectedText)
        {
            ValidateElementText(PostcodeFieldError, ExpectedText);

            return this;
        }
        public PersonDetailsPage ValidateMobilePhoneFieldErrorVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MobilePhoneFieldError);
            else
                WaitForElementNotVisible(MobilePhoneFieldError, 7);

            return this;
        }
        public PersonDetailsPage ValidateMobilePhoneFieldErrorText(string ExpectedText)
        {
            ValidateElementText(MobilePhoneFieldError, ExpectedText);

            return this;
        }
    }
    
}
