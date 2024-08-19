using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.QualityAndCompliance
{
    public class ApplicantRecordPage : CommonMethods
    {
        public ApplicantRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ApplicantRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'editpage.aspx?type=applicant&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By BackButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By RelatedItemMenu = By.XPath("//a[@class='nav-link dropdown-toggle']");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By shareRecordButton = By.Id("TI_ShareRecordButton");
        
        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");

        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        readonly By DetailsTab = By.Id("CWNavGroup_EditForm");
        readonly By RoleApplications_MenuItem = By.Id("CWNavItem_RoleApplicantions");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");

        #endregion

        #region Applicant General Fields

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");

        readonly By FirstName_Field = By.Id("CWField_firstname");
        readonly By LastName_Field = By.Id("CWField_lastname");
        readonly By AvailableFrom_Field = By.Id("CWField_availablefrom");
        readonly By ResponsibleTeam_LookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_Field = By.Id("CWField_ownerid_cwname");
        readonly By StatedGender_Field = By.Id("CWField_applicantgenderid");
        readonly By PhoneLandline_Field = By.Id("CWField_personalphonelandline");
        readonly By PhoneMobile_Field = By.Id("CWField_personalphonemobile");
        readonly By PersonalEmail_Field = By.Id("CWField_personalemail");
        readonly By DateOfBirth_Field = By.Id("CWField_dateofbirth");
        readonly By RecruitmentApplicantNotes_Field = By.Id("CWField_recruitmentapplicantnotes");

        readonly By FirstNameField_ErrorNotificationText = By.XPath("//*[@id = 'CWControlHolder_firstname']/label/span");
        readonly By LastNameField_ErrorNotificationText = By.XPath("//*[@id = 'CWControlHolder_lastname']/label/span");
        readonly By AvailableFromField_ErrorNotificationText = By.XPath("//*[@id = 'CWControlHolder_availablefrom']/label/span");
        readonly By StartDateField_ErrorNotificationText = By.XPath("//*[@id = 'CWControlHolder_startdate']/label/span");

        readonly By systemUser_LinkField = By.Id("CWField_systemuserid_Link");

        #endregion

        readonly By Availability_Tab = By.XPath("//*[@id='CWNavGroup_WorkScheduleAdvanced']/a");
        readonly By RecruitmentDocuments_Tab = By.XPath("//*[@id='CWNavGroup_Compliance']/a");
        readonly By ApplicantDashboard_Tab = By.XPath("//*[@id='CWNavGroup_RoleApplicationsDashboardWidget']/a");

        #region Applicant Language and Applicant Alias

        By ApplicantLanguageIFrame = By.Id("CWIFrame_Languages");

        By ApplicantAliasIFrame = By.Id("CWIFrame_Aliases");
        
        By NewRecordButton = By.Id("TI_NewRecordButton");
        
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        
        #endregion

        #region Address Fields
        readonly By TownOrCity_Field = By.Id("CWField_addressline4");
        readonly By Postcode_Field = By.Id("CWField_postcode");
        readonly By AddressType_Picklist = By.Id("CWField_addresstypeid");
        readonly By StartDate_Field = By.Id("CWField_startdate");

        readonly By PropertyName_Field = By.Id("CWField_propertyname");
        readonly By PropertyNo_Field = By.Id("CWField_addressline1");
        readonly By StreetEN_Field = By.Id("CWField_addressline2");
        readonly By VillageDistrict_Field = By.Id("CWField_addressline3");
        readonly By County_Field = By.Id("CWField_addressline5");
        readonly By Country_Field = By.Id("CWField_country");
        readonly By ClearAddress_Button = By.Id("CWFieldButton_ClearAddress");

        #endregion

        #region Additional Demographics Fields
        readonly By Pronouns_LookupButton = By.Id("CWLookupBtn_pronounsid");
        readonly By MaritalStatus_LookupButton = By.Id("CWLookupBtn_maritalstatusid");
        readonly By Religion_LookupButton = By.Id("CWLookupBtn_religionid");
        readonly By BritishCitizenship_Picklist = By.Id("CWField_britishcitizenshipid");
        readonly By CountryOfBirthNotKnown_YesOption = By.Id("CWField_countryofbirthnotknown_1");
        readonly By CountryOfBirthNotKnown_NoOption = By.Id("CWField_countryofbirthnotknown_0");
        readonly By NotBornInUKButCountryUnknown_YesOption = By.Id("CWField_notborninukbutcountryunknown_1");
        readonly By NotBornInUKButCountryUnknown_NoOption = By.Id("CWField_notborninukbutcountryunknown_0");
        readonly By Ethnicity_LookupButton = By.Id("CWLookupBtn_ethnicityid");
        readonly By Nationality_LookupButton = By.Id("CWLookupBtn_nationalityid");
        readonly By CountryOfBirth_LookupButton = By.Id("CWLookupBtn_countryofbirthid");
        readonly By YearOfEntryToUK_Field = By.Id("CWField_yearofentry");
        readonly By DisabilityStatus_Picklist = By.Id("CWField_disabilitystatusid");
        readonly By Pronouns_LinkField = By.Id("CWField_pronounsid_Link");
        readonly By MaritalStatus_LinkField = By.Id("CWField_maritalstatusid_Link");
        readonly By Religion_LinkField = By.Id("CWField_religionid_Link");
        readonly By Ethnicity_LinkField = By.Id("CWField_ethnicityid_Link");
        readonly By Nationality_LinkField = By.Id("CWField_nationalityid_Link");
        readonly By CountryOfBirth_LinkField = By.Id("CWField_countryofbirthid_Link");


        #endregion

        #region Employment Details Fields
        readonly By WorkingTimeDirectiveOptOut_PicklistField = By.Id("CWField_workingtimedirectiveoptoutid");
        readonly By allowUseGdprDataField_label = By.XPath("//*[@id = 'CWLabelHolder_allowusegdprdata']/label");
        readonly By allowUseGdprData_YesOption = By.Id("CWField_allowusegdprdata_1");
        readonly By allowUseGdprData_NoOption = By.Id("CWField_allowusegdprdata_0");

        #endregion

        #region Form Footer
        readonly By ActiveLabel = By.XPath("//*[@id = 'CWInputFormFooter']//label[text() = 'Active']/following-sibling::span");

        #endregion

        public ApplicantRecordPage WaitForApplicantRecordPagePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ApplicantRecordIFrame);
            SwitchToIframe(ApplicantRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

        public ApplicantRecordPage WaitForApplicantRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ApplicantRecordIFrame);
            SwitchToIframe(ApplicantRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }

        public ApplicantRecordPage WaitForApplicantLanguagesAreaToLoad()
        {
            WaitForElement(ApplicantLanguageIFrame);
            SwitchToIframe(ApplicantLanguageIFrame);
            WaitForElement(NewRecordButton);
            return this;

        }

        public ApplicantRecordPage WaitForApplicantAliasAreaToLoad()
        {
            WaitForElement(ApplicantAliasIFrame);
            SwitchToIframe(ApplicantAliasIFrame);
            WaitForElement(NewRecordButton);

            return this;

        }

        public ApplicantRecordPage WaitForApplicantRecordPageSubAreaToLoad(string sectionName)
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ApplicantRecordIFrame);
            SwitchToIframe(ApplicantRecordIFrame);

            switch (sectionName)
            {
                case "Applicant Languages":
                    WaitForElement(ApplicantLanguageIFrame);
                    SwitchToIframe(ApplicantLanguageIFrame);
                    break;
                case "Applicant Aliases":
                    WaitForElement(ApplicantAliasIFrame);
                    SwitchToIframe(ApplicantAliasIFrame);
                    WaitForElement(NewRecordButton);
                    break;
            }

            return this;

        }

        public ApplicantRecordPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }

        public ApplicantRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }

        public ApplicantRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);            
            Click(SaveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ApplicantRecordPage ClickBackButton()
        {
            WaitForElement(BackButton);
            Click(BackButton);

            return this;
        }

        public ApplicantRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            ScrollToElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ApplicantRecordPage NavigateToDetailsTab()
        {
            WaitForElementToBeClickable(DetailsTab);
            ScrollToElement(DetailsTab);
            Click(DetailsTab);

            return this;
        }

        public ApplicantRecordPage InsertFirstName(string Text)
        {
            WaitForElement(FirstName_Field);
            SendKeys(FirstName_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertLastName(string Text)
        {
            WaitForElement(LastName_Field);
            SendKeys(LastName_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertAvailableFromDateField(string Text)
        {
            WaitForElement(AvailableFrom_Field);
            ScrollToElement(AvailableFrom_Field);
            SendKeys(AvailableFrom_Field, Text);
            return this;
        }

        public ApplicantRecordPage ClickSystemUserFieldLinkText()
        {
            WaitForElement(systemUser_LinkField);
            ScrollToElement(systemUser_LinkField);
            WaitForElementToBeClickable(systemUser_LinkField);
            Click(systemUser_LinkField);
            return this;
        }

        public ApplicantRecordPage SelectStatedGender(string TextToSelect)
        {
            WaitForElement(StatedGender_Field);
            ScrollToElement(StatedGender_Field);
            SelectPicklistElementByText(StatedGender_Field, TextToSelect);
            return this;
        }

        public ApplicantRecordPage InsertTextInPersonalPhoneLandlineField(string Text)
        {
            WaitForElement(PhoneLandline_Field);
            ScrollToElement(PhoneLandline_Field);
            SendKeys(PhoneLandline_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertTextInPersonalPhoneMobileField(string Text)
        {
            WaitForElement(PhoneMobile_Field);
            ScrollToElement(PhoneMobile_Field);
            SendKeys(PhoneMobile_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertPersonalEmail(string Text)
        {
            WaitForElement(PersonalEmail_Field);
            ScrollToElement(PersonalEmail_Field);
            SendKeys(PersonalEmail_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertDateOfBirth(string Text)
        {
            WaitForElement(DateOfBirth_Field);
            ScrollToElement(DateOfBirth_Field);
            SendKeys(DateOfBirth_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertRecruitmentApplicantNotes(string Text)
        {
            WaitForElement(RecruitmentApplicantNotes_Field);
            ScrollToElement(RecruitmentApplicantNotes_Field);
            SendKeys(RecruitmentApplicantNotes_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertPropertyName(string Text)
        {
            WaitForElement(PropertyName_Field);
            ScrollToElement(PropertyName_Field);
            SendKeys(PropertyName_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertPropertyNo(string Text)
        {
            WaitForElement(PropertyNo_Field);
            ScrollToElement(PropertyNo_Field);
            SendKeys(PropertyNo_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertStreetEN(string Text)
        {
            WaitForElement(StreetEN_Field);
            ScrollToElement(StreetEN_Field);
            SendKeys(StreetEN_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertVillageOrDistrict(string Text)
        {
            WaitForElement(VillageDistrict_Field);
            ScrollToElement(VillageDistrict_Field);
            SendKeys(VillageDistrict_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertTownOrCity(string Text)
        {
            WaitForElement(TownOrCity_Field);
            ScrollToElement(TownOrCity_Field);
            SendKeys(TownOrCity_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertPostcode(string Text)
        {
            WaitForElement(Postcode_Field);
            ScrollToElement(Postcode_Field);
            SendKeys(Postcode_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertCounty(string Text)
        {
            WaitForElement(County_Field);
            ScrollToElement(County_Field);
            SendKeys(County_Field, Text);
            return this;
        }

        public ApplicantRecordPage InsertCountry(string Text)
        {
            WaitForElement(Country_Field);
            ScrollToElement(Country_Field);
            SendKeys(Country_Field, Text);
            return this;
        }

        public ApplicantRecordPage SelectAddressType(string TextToSelect)
        {
            WaitForElement(AddressType_Picklist);
            ScrollToElement(AddressType_Picklist);
            SelectPicklistElementByText(AddressType_Picklist, TextToSelect);
            return this;
        }

        public ApplicantRecordPage InsertStartDate(string Text)
        {
            WaitForElement(StartDate_Field);
            ScrollToElement(StartDate_Field);
            SendKeys(StartDate_Field, Text);
            return this;
        }

        public ApplicantRecordPage ClickClearAddressButton()
        {            
            ScrollToElement(ClearAddress_Button);
            WaitForElementToBeClickable(ClearAddress_Button);
            Click(ClearAddress_Button);
            return this;
        }

        public ApplicantRecordPage ClickPronounsLookupButton()
        {
            ScrollToElement(Pronouns_LookupButton);
            WaitForElementToBeClickable(Pronouns_LookupButton);
            Click(Pronouns_LookupButton);
            return this;
        }

        public ApplicantRecordPage ClickMaritalStatusLookupButton()
        {
            ScrollToElement(MaritalStatus_LookupButton);
            WaitForElementToBeClickable(MaritalStatus_LookupButton);
            Click(MaritalStatus_LookupButton);
            return this;
        }

        public ApplicantRecordPage ClickReligionLookupButton()
        {
            ScrollToElement(Religion_LookupButton);
            WaitForElementToBeClickable(Religion_LookupButton);
            Click(Religion_LookupButton);
            return this;
        }

        public ApplicantRecordPage SelectBritishCitizenship(string TextToSelect)
        {
            WaitForElement(BritishCitizenship_Picklist);
            ScrollToElement(BritishCitizenship_Picklist);
            SelectPicklistElementByText(BritishCitizenship_Picklist, TextToSelect);
            return this;
        }

        public ApplicantRecordPage ClickCountryOfBirthNotKnown_YesOption()
        {            
            ScrollToElement(CountryOfBirthNotKnown_YesOption);
            WaitForElementToBeClickable(CountryOfBirthNotKnown_YesOption);
            Click(CountryOfBirthNotKnown_YesOption);
            return this;
        }

        public ApplicantRecordPage ClickCountryOfBirthNotKnown_NoOption()
        {
            ScrollToElement(CountryOfBirthNotKnown_NoOption);
            WaitForElementToBeClickable(CountryOfBirthNotKnown_NoOption);
            Click(CountryOfBirthNotKnown_NoOption);
            return this;
        }

        public ApplicantRecordPage ClickNotBornInUKButCountryUnknown_YesOption()
        {
            ScrollToElement(NotBornInUKButCountryUnknown_YesOption);
            WaitForElementToBeClickable(NotBornInUKButCountryUnknown_YesOption);            
            Click(NotBornInUKButCountryUnknown_YesOption);
            return this;
        }

        public ApplicantRecordPage ClickNotBornInUKButCountryUnknown_NoOption()
        {
            ScrollToElement(NotBornInUKButCountryUnknown_NoOption);
            WaitForElementToBeClickable(NotBornInUKButCountryUnknown_NoOption);
            Click(NotBornInUKButCountryUnknown_NoOption);
            return this;
        }

        public ApplicantRecordPage ClickEthnicityLookupButton()
        {
            ScrollToElement(Ethnicity_LookupButton);
            WaitForElementToBeClickable(Ethnicity_LookupButton);
            Click(Ethnicity_LookupButton);
            return this;
        }

        public ApplicantRecordPage ClickNationalityLookupButton()
        {
            ScrollToElement(Nationality_LookupButton);
            WaitForElementToBeClickable(Nationality_LookupButton);
            Click(Nationality_LookupButton);
            return this;
        }

        public ApplicantRecordPage ClickCountryOfBirthLookupButton()
        {
            ScrollToElement(CountryOfBirth_LookupButton);
            WaitForElementToBeClickable(CountryOfBirth_LookupButton);
            Click(CountryOfBirth_LookupButton);
            return this;
        }

        public ApplicantRecordPage InsertYearOfEntryToUK(string Text)
        {
            ScrollToElement(YearOfEntryToUK_Field);
            WaitForElementToBeClickable(YearOfEntryToUK_Field);            
            SendKeys(YearOfEntryToUK_Field, Text);
            return this;
        }

        public ApplicantRecordPage SelectDisablityStatus(string TextToSelect)
        {
            ScrollToElement(DisabilityStatus_Picklist);
            WaitForElement(DisabilityStatus_Picklist);            
            SelectPicklistElementByText(DisabilityStatus_Picklist, TextToSelect);

            return this;
        }

        public ApplicantRecordPage SelectWorkingTimeDirectiveOptOutValue(string TextToSelect)
        {
            ScrollToElement(WorkingTimeDirectiveOptOut_PicklistField);
            WaitForElement(WorkingTimeDirectiveOptOut_PicklistField);
            SelectPicklistElementByText(WorkingTimeDirectiveOptOut_PicklistField, TextToSelect);

            return this;
        }

        public ApplicantRecordPage ClickAllowToUseGdpr_YesOption()
        {
            ScrollToElement(allowUseGdprData_YesOption);
            WaitForElementToBeClickable(allowUseGdprData_YesOption);
            Click(allowUseGdprData_YesOption);
            return this;
        }

        public ApplicantRecordPage ClickAllowToUseGdpr_NoOption()
        {
            ScrollToElement(allowUseGdprData_NoOption);
            WaitForElementToBeClickable(allowUseGdprData_NoOption);
            Click(allowUseGdprData_NoOption);
            return this;
        }

        public ApplicantRecordPage NavigateToAvailabilityTab()
        {
            WaitForElement(Availability_Tab);
            Click(Availability_Tab);
            return this;
        }

        public ApplicantRecordPage NavigateToRoleApplicationsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(RoleApplications_MenuItem);
            Click(RoleApplications_MenuItem);

            return this;
        }

        public ApplicantRecordPage NavigateToRecruitmentDocumentsTab()
        {
            WaitForElementToBeClickable(RecruitmentDocuments_Tab);
            ScrollToElement(RecruitmentDocuments_Tab);
            Click(RecruitmentDocuments_Tab);
            
            return this;
        }

        public ApplicantRecordPage NavigateToApplicantDashboardTab()
        {
            WaitForElementToBeClickable(ApplicantDashboard_Tab);
            Click(ApplicantDashboard_Tab);

            return this;
        }

        public ApplicantRecordPage OpenApplicantSubRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            ScrollToElement(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public ApplicantRecordPage ClickApplicantRecordPageSubArea_NewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            ScrollToElement(NewRecordButton);
            Click(NewRecordButton);
            return this;
        }

        public ApplicantRecordPage ValidateApplicantSubRecordIsPresent(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            WaitForElementToBeClickable(recordRow(RecordId));
            ScrollToElement(recordRow(RecordId));

            bool isRecordPresent = GetElementVisibility(recordRow(RecordId));
            Assert.IsTrue(isRecordPresent);

            return this;
        }

        public ApplicantRecordPage ValidateFirstName(string ExpectedText)
        {
            WaitForElementVisible(FirstName_Field);
            ScrollToElement(FirstName_Field);
            ValidateElementValue(FirstName_Field, ExpectedText);
            return this;
        }            

        public ApplicantRecordPage ValidatePageHeaderTitle(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            ValidateElementText(pageHeader, "Applicant:\r\n" + ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateLastName(string ExpectedText)
        {
            WaitForElementVisible(LastName_Field);
            ScrollToElement(LastName_Field);
            ValidateElementValue(LastName_Field, ExpectedText);

            return this;
        }

        public ApplicantRecordPage ValidateAvailableFrom(string ExpectedText)
        {
            WaitForElementVisible(AvailableFrom_Field);
            ScrollToElement(AvailableFrom_Field);
            ValidateElementValue(AvailableFrom_Field, ExpectedText);

            return this;
        }

        public ApplicantRecordPage ValidateSystemUserFieldLinkText(string ExpectedText)
        {
            WaitForElement(systemUser_LinkField);
            ScrollToElement(systemUser_LinkField);
            WaitForElementToBeClickable(systemUser_LinkField);
            ValidateElementByTitle(systemUser_LinkField, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateFirstNameFieldErrorMeessage(string ExpectedTimeText)
        {
            WaitForElementVisible(FirstNameField_ErrorNotificationText);
            ScrollToElement(FirstNameField_ErrorNotificationText);
            ValidateElementByTitle(FirstNameField_ErrorNotificationText, ExpectedTimeText);

            return this;
        }

        public ApplicantRecordPage ValidateLastNameFieldErrorMeessage(string ExpectedTimeText)
        {
            WaitForElementVisible(LastNameField_ErrorNotificationText);
            ScrollToElement(LastNameField_ErrorNotificationText);
            ValidateElementByTitle(LastNameField_ErrorNotificationText, ExpectedTimeText);

            return this;
        }

        public ApplicantRecordPage ValidateAvailableFromFieldErrorMeessage(string ExpectedTimeText)
        {
            WaitForElementVisible(AvailableFromField_ErrorNotificationText);
            ScrollToElement(AvailableFromField_ErrorNotificationText);
            ValidateElementByTitle(AvailableFromField_ErrorNotificationText, ExpectedTimeText);

            return this;
        }

        public ApplicantRecordPage ValidateStartDateFieldErrorMeessage(string ExpectedTimeText)
        {
            WaitForElementVisible(StartDateField_ErrorNotificationText);
            ScrollToElement(StartDateField_ErrorNotificationText);
            ValidateElementByTitle(StartDateField_ErrorNotificationText, ExpectedTimeText);

            return this;
        }

        public ApplicantRecordPage ValidateNotificationMessage(string ExpectedMessage)
        {
            WaitForElementVisible(notificationMessage);
            ScrollToElement(notificationMessage);
            ValidateElementText(notificationMessage, ExpectedMessage);

            return this;
        }

        public ApplicantRecordPage ValidateStatedGender(string ExpectedText)
        {
            WaitForElement(StatedGender_Field);
            ScrollToElement(StatedGender_Field);
            ValidatePicklistSelectedText(StatedGender_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidatePersonalPhoneLandlineField(string ExpectedText)
        {
            WaitForElement(PhoneLandline_Field);
            ScrollToElement(PhoneLandline_Field);
            ValidateElementValue(PhoneLandline_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidatePersonalPhoneMobileField(string ExpectedText)
        {
            WaitForElement(PhoneMobile_Field);
            ScrollToElement(PhoneMobile_Field);
            ValidateElementValue(PhoneMobile_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidatePersonalEmail(string ExpectedText)
        {
            WaitForElement(PersonalEmail_Field);
            ScrollToElement(PersonalEmail_Field);
            ValidateElementValue(PersonalEmail_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateDateOfBirth(string ExpectedText)
        {
            WaitForElement(DateOfBirth_Field);
            ScrollToElement(DateOfBirth_Field);
            ValidateElementValue(DateOfBirth_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateRecruitmentApplicantNotes(string ExpectedText)
        {
            WaitForElement(RecruitmentApplicantNotes_Field);
            ScrollToElement(RecruitmentApplicantNotes_Field);
            ValidateElementValue(RecruitmentApplicantNotes_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidatePropertyName(string ExpectedText)
        {
            WaitForElement(PropertyName_Field);
            ScrollToElement(PropertyName_Field);
            ValidateElementValue(PropertyName_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidatePropertyNo(string ExpectedText)
        {
            WaitForElement(PropertyNo_Field);
            ScrollToElement(PropertyNo_Field);
            ValidateElementValue(PropertyNo_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateStreetEN(string ExpectedText)
        {
            WaitForElement(StreetEN_Field);
            ScrollToElement(StreetEN_Field);
            SendKeys(StreetEN_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateVillageOrDistrict(string ExpectedText)
        {
            WaitForElement(VillageDistrict_Field);
            ScrollToElement(VillageDistrict_Field);
            ValidateElementValue(VillageDistrict_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateTownOrCity(string ExpectedText)
        {
            WaitForElement(TownOrCity_Field);
            ScrollToElement(TownOrCity_Field);
            ValidateElementValue(TownOrCity_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidatePostcode(string ExpectedText)
        {
            WaitForElement(Postcode_Field);
            ScrollToElement(Postcode_Field);
            ValidateElementValue(Postcode_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateCounty(string ExpectedText)
        {
            WaitForElement(County_Field);
            ScrollToElement(County_Field);
            ValidateElementValue(County_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateCountry(string ExpectedText)
        {
            WaitForElement(Country_Field);
            ScrollToElement(Country_Field);
            ValidateElementValue(Country_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateAddressType(string ExpectedText)
        {            
            ScrollToElement(AddressType_Picklist);
            WaitForElementVisible(AddressType_Picklist);
            ValidatePicklistSelectedText(AddressType_Picklist, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateStartDate(string ExpectedText)
        {            
            ScrollToElement(StartDate_Field);
            WaitForElementVisible(StartDate_Field);
            ValidateElementValue(StartDate_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidatePronounFieldLinkText(string ExpectedText)
        {
            ScrollToElement(Pronouns_LinkField);
            WaitForElementToBeClickable(Pronouns_LinkField);            
            ValidateElementByTitle(Pronouns_LinkField, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateMaritalStatusFieldLinkText(string ExpectedText)
        {
            ScrollToElement(MaritalStatus_LinkField);
            WaitForElementToBeClickable(MaritalStatus_LinkField);
            ValidateElementByTitle(MaritalStatus_LinkField, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateReligionFieldLinkText(string ExpectedText)
        {
            ScrollToElement(Religion_LinkField);
            WaitForElementToBeClickable(Religion_LinkField);
            ValidateElementByTitle(Religion_LinkField, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateBritishCitizenship(string ExpectedText)
        {
            WaitForElement(BritishCitizenship_Picklist);
            ScrollToElement(BritishCitizenship_Picklist);
            ValidatePicklistSelectedText(BritishCitizenship_Picklist, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateCountryOfBirthNotKnown_YesOptionSelected()
        {
            ScrollToElement(CountryOfBirthNotKnown_YesOption);
            WaitForElementToBeClickable(CountryOfBirthNotKnown_YesOption);
            ValidateElementChecked(CountryOfBirthNotKnown_YesOption);
            ValidateElementNotChecked(CountryOfBirthNotKnown_NoOption);
            return this;
        }

        public ApplicantRecordPage ValidateCountryOfBirthNotKnown_NoOptionSelected()
        {
            ScrollToElement(CountryOfBirthNotKnown_NoOption);
            WaitForElementToBeClickable(CountryOfBirthNotKnown_NoOption);
            ValidateElementChecked(CountryOfBirthNotKnown_NoOption);
            ValidateElementNotChecked(CountryOfBirthNotKnown_YesOption);
            return this;
        }

        public ApplicantRecordPage ValidateNotBornInUKButCountryUnknown_YesOptionSelected()
        {
            ScrollToElement(NotBornInUKButCountryUnknown_YesOption);
            WaitForElementToBeClickable(NotBornInUKButCountryUnknown_YesOption);
            ValidateElementChecked(NotBornInUKButCountryUnknown_YesOption);
            ValidateElementNotChecked(NotBornInUKButCountryUnknown_NoOption);
            return this;
        }

        public ApplicantRecordPage ValidateNotBornInUKButCountryUnknown_NoOptionSelected()
        {
            ScrollToElement(NotBornInUKButCountryUnknown_NoOption);
            WaitForElementToBeClickable(NotBornInUKButCountryUnknown_NoOption);
            ValidateElementChecked(NotBornInUKButCountryUnknown_NoOption);
            ValidateElementNotChecked(NotBornInUKButCountryUnknown_YesOption);
            return this;
        }

        public ApplicantRecordPage ValidateEthnicityFieldLinkText(string ExpectedText)
        {
            ScrollToElement(Ethnicity_LinkField);
            WaitForElementToBeClickable(Ethnicity_LinkField);
            ValidateElementByTitle(Ethnicity_LinkField, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateNationalityFieldLinkText(string ExpectedText)
        {
            ScrollToElement(Nationality_LinkField);
            WaitForElementToBeClickable(Nationality_LinkField);
            ValidateElementByTitle(Nationality_LinkField, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateCountryOfBirthFieldLinkText(string ExpectedText)
        {
            ScrollToElement(CountryOfBirth_LinkField);
            WaitForElementToBeClickable(CountryOfBirth_LinkField);
            ValidateElementByTitle(CountryOfBirth_LinkField, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateYearOfEntryToUK(string ExpectedText)
        {
            WaitForElement(YearOfEntryToUK_Field);
            ScrollToElement(YearOfEntryToUK_Field);
            ValidateElementValue(YearOfEntryToUK_Field, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateDisabilityStatus(string ExpectedText)
        {
            ScrollToElement(DisabilityStatus_Picklist);
            WaitForElement(DisabilityStatus_Picklist);            
            ValidatePicklistSelectedText(DisabilityStatus_Picklist, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateWorkingTimeDirectiveOptOutValue(string ExpectedText)
        {
            WaitForElement(WorkingTimeDirectiveOptOut_PicklistField);
            ScrollToElement(WorkingTimeDirectiveOptOut_PicklistField);
            ValidatePicklistSelectedText(WorkingTimeDirectiveOptOut_PicklistField, ExpectedText);
            return this;
        }

        public ApplicantRecordPage ValidateWorkingTimeDirectiveOptOutFieldOption(string TextToFind)
        {
            WaitForElement(WorkingTimeDirectiveOptOut_PicklistField);
            ScrollToElement(WorkingTimeDirectiveOptOut_PicklistField);
            ValidatePicklistContainsElementByText(WorkingTimeDirectiveOptOut_PicklistField, TextToFind);

            return this;
        }

        public ApplicantRecordPage ValidateAllowToUseGdprFieldIsVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(allowUseGdprDataField_label);
                WaitForElementVisible(allowUseGdprDataField_label);
                WaitForElementVisible(allowUseGdprData_YesOption);
                WaitForElementVisible(allowUseGdprData_NoOption);

            }
            else
            {
                WaitForElementNotVisible(allowUseGdprDataField_label, 2);
                WaitForElementNotVisible(allowUseGdprData_YesOption, 2);
                WaitForElementNotVisible(allowUseGdprData_NoOption, 2);
            }
            return this;
        }

        public ApplicantRecordPage ValidateAllowToUseGdprField_YesOptionSelected()
        {
            ScrollToElement(allowUseGdprDataField_label);
            ScrollToElement(allowUseGdprData_YesOption);
            ValidateElementChecked(allowUseGdprData_YesOption);
            ValidateElementNotChecked(allowUseGdprData_NoOption);
            return this;
        }

        public ApplicantRecordPage ValidateAllowToUseGdprField_NoOptionSelected()
        {
            ScrollToElement(allowUseGdprDataField_label);
            ScrollToElement(allowUseGdprData_NoOption);
            ValidateElementChecked(allowUseGdprData_NoOption);
            ValidateElementNotChecked(allowUseGdprData_YesOption);
            return this;
        }

        public ApplicantRecordPage ValidatePropertyNameFieldIsVisble(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(PropertyName_Field);
            }
            else
            {
                WaitForElementNotVisible(PropertyName_Field, 2);
            }
            bool ActualVisibility = GetElementVisibility(PropertyName_Field);
            Assert.AreEqual(ExpectedVisible, ActualVisibility);
            return this;
        }

        public ApplicantRecordPage ValidatePropertyNoFieldIsVisble(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(PropertyNo_Field);
            }
            else
            {
                WaitForElementNotVisible(PropertyNo_Field, 2);
            }
            bool ActualVisibility = GetElementVisibility(PropertyNo_Field);
            Assert.AreEqual(ExpectedVisible, ActualVisibility);
            return this;
        }

        public ApplicantRecordPage ValidateStreetENFieldIsVisble(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(StreetEN_Field);
            }
            else
            {
                WaitForElementNotVisible(StreetEN_Field, 2);
            }
            bool ActualVisibility = GetElementVisibility(StreetEN_Field);
            Assert.AreEqual(ExpectedVisible, ActualVisibility);
            return this;
        }

        public ApplicantRecordPage ValidateVillageDistrictFieldIsVisble(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(VillageDistrict_Field);
            }
            else
            {
                WaitForElementNotVisible(VillageDistrict_Field, 2);
            }
            bool ActualVisibility = GetElementVisibility(VillageDistrict_Field);
            Assert.AreEqual(ExpectedVisible, ActualVisibility);
            return this;
        }

        public ApplicantRecordPage ValidateTownCityFieldIsVisble(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(TownOrCity_Field);
            }
            else
            {
                WaitForElementNotVisible(TownOrCity_Field, 2);
            }
            bool ActualVisibility = GetElementVisibility(TownOrCity_Field);
            Assert.AreEqual(ExpectedVisible, ActualVisibility);
            return this;
        }

        public ApplicantRecordPage ValidatePostcodeFieldIsVisble(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(Postcode_Field);
            }
            else
            {
                WaitForElementNotVisible(Postcode_Field, 2);
            }
            bool ActualVisibility = GetElementVisibility(Postcode_Field);
            Assert.AreEqual(ExpectedVisible, ActualVisibility);
            return this;
        }

        public ApplicantRecordPage ValidateCountyFieldIsVisble(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(County_Field);
            }
            else
            {
                WaitForElementNotVisible(County_Field, 2);
            }
            bool ActualVisibility = GetElementVisibility(County_Field);
            Assert.AreEqual(ExpectedVisible, ActualVisibility);
            return this;
        }

        public ApplicantRecordPage ValidateCountryFieldIsVisble(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(Country_Field);
            }
            else
            {
                WaitForElementNotVisible(Country_Field, 2);
            }
            bool ActualVisibility = GetElementVisibility(Country_Field);
            Assert.AreEqual(ExpectedVisible, ActualVisibility);
            return this;
        }

        public ApplicantRecordPage ValidateAddressTypeFieldIsVisble(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(AddressType_Picklist);
            }
            else
            {
                WaitForElementNotVisible(AddressType_Picklist, 2);
            }
            bool ActualVisibility = GetElementVisibility(AddressType_Picklist);
            Assert.AreEqual(ExpectedVisible, ActualVisibility);
            return this;
        }

        public ApplicantRecordPage ValidateStartDateFieldIsVisble(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(StartDate_Field);
            }
            else
            {
                WaitForElementNotVisible(StartDate_Field, 2);
            }
            bool ActualVisibility = GetElementVisibility(StartDate_Field);
            Assert.AreEqual(ExpectedVisible, ActualVisibility);
            return this;
        }

        public ApplicantRecordPage ValidateClearAddressButtonIsVisble(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                ScrollToElement(ClearAddress_Button);
            }
            else
            {
                WaitForElementNotVisible(ClearAddress_Button, 2);
            }
            bool ActualVisibility = GetElementVisibility(ClearAddress_Button);
            Assert.AreEqual(ExpectedVisible, ActualVisibility);
            return this;
        }

        public ApplicantRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(shareRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public ApplicantRecordPage ValidateApplicantActiveLabelText(string ExpectedText)
        {
            WaitForElement(ActiveLabel);
            ScrollToElement(ActiveLabel);
            ValidateElementText(ActiveLabel, ExpectedText);
            return this;
        }
    }
}
