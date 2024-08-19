
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserRecordPage : CommonMethods
    {
        public SystemUserRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");

        readonly By backButton = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By CopyRecordLinkButton = By.Id("TI_CopyRecordLink");

        readonly By systemUserTitle = By.XPath("//span[@class='record-title']");

        readonly By activate_Button = By.Id("TI_ActivateButton");
        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");
        readonly By recordNotAccessible_notificationMessage = By.Id("CWNotificationMessage_CWBase");
        readonly By unauthorizedAccess_notificationMessage = By.XPath("//div[@id = 'CWNotificationMessage_CWBase']/strong");
        readonly By notificationMessage_CloseButton = By.XPath("//button[text() = 'Close']");

        readonly By workSchedule = By.XPath("//*[@id='CWNavGroup_WorkSchedule']/a[text()='Work Schedule']");
        readonly By availability = By.XPath("//*[@id='CWNavGroup_WorkScheduleAdvanced']/a[text()='Availability']");
        readonly By teams = By.XPath("//*[@id='CWNavGroup_Teams']/a[text()='Teams']");
        readonly By securityProfiles = By.XPath("//*[@id='CWNavGroup_UserSecurityProfile']/a[text()='Security Profiles']");
        readonly By diary = By.XPath("//*[@id='CWNavGroup_UserDiary']/a[text()='Diary']");
        readonly By employeeSchedule = By.XPath("//*[@id='CWNavGroup_EmployeeSchedule']");
        readonly By employeeDiary = By.XPath("//*[@id='CWNavGroup_EmployeeDiary']");

        readonly By addressType_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])[1]");
        readonly By startDate_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])[2]");
        readonly By password_FieldErrorArea = By.XPath("//*[@id='CWControlHolder_password']/label/span");
        readonly By SecurityProfilesButton = By.Id("CWNavGroup_UserSecurityProfile");

        #region Section Headers 

        readonly By GeneralSection_Header = By.Id("CWSection_General");
        readonly By AddressSection_Header = By.Id("CWSection_Address");
        readonly By AdditionalDemographicsSection_Header = By.Id("CWSection_AdditionalDemographics");
        readonly By PhoneEmailSection_Header = By.Id("CWSection_PhoneEmail");
        readonly By AccountSection_Header = By.Id("CWSection_Account");
        readonly By EmploymentDetailsSection_Header = By.XPath("//*[@id='CWSection_AdditionalInformation']/fieldset/div/span");
        readonly By SettingSection_Header = By.Id("CWSection_Settings");
        readonly By LegacySystemDataSection_Header = By.Id("CWSection_LegacySystemData");

        #endregion

        #region General Fields

        readonly By id_Field_disabled = By.XPath("//*[@id='CWField_usernumber' and @disabled = 'disabled']");
        readonly By id_Field_Value = By.XPath("//*[@id='CWField_usernumber'and @value]");

        readonly By UserId_Field = By.Id("CWField_usernumber");

        readonly By EmployeeType_Field = By.Id("CWField_employeetypeid");
        readonly By FirstName_Field = By.Id("CWField_firstname");
        readonly By LastName_Field = By.Id("CWField_lastname");
        readonly By demographicstitleid_FieldLookup = By.Id("CWLookupBtn_demographicstitleid");
        readonly By middlename_Field = By.Id("CWField_middlename");
        readonly By dateofbirth_Field = By.Id("CWField_dateofbirth");
        readonly By persongenderid_Field = By.Id("CWField_persongenderid");
        readonly By browse_Button = By.Id("CWField_profilephotoid");
        readonly By canworkoffline_Field = By.Id("CWLabelHolder_canworkoffline");
        readonly By workinmultipleteams_Field = By.Id("CWLabelHolder_workinmultipleteams");
        readonly By ProfilePhoto_Field = By.Id("CWLabelHolder_profilephotoid");
        readonly By ProfilePhotoid_Field = By.Id("CWField_profilephotoid");
        readonly By pronounsField_LookupButton = By.Id("CWLookupBtn_pronounsid");
        readonly By pronouns_LinkField = By.Id("CWField_pronounsid_Link");

        #endregion

        #region Address Fields

        static string propertyname_FieldName = "CWField_propertyname";
        static string propertyno_FieldName = "CWField_addressline1";
        static string street_FieldName = "CWField_addressline2";
        static string villageDistrict_FieldName = "CWField_addressline3";
        static string townCity_FieldName = "CWField_addressline4";
        static string postcode_FieldName = "CWField_postcode";

        static string county_FieldName = "CWField_addressline5";
        static string country_FieldName = "CWField_country";
        static string addressType_FieldName = "CWField_addresstypeid";
        static string startDate_FieldName = "CWField_startdate";
        static string endDate_FieldName = "CWField_enddate";

        readonly By propertyname_Field = By.Id(propertyname_FieldName);
        readonly By propertyno_Field = By.Id(propertyno_FieldName);
        readonly By street_Field = By.Id(street_FieldName);
        readonly By villageDistrict_Field = By.Id(villageDistrict_FieldName);
        readonly By townCity_Field = By.Id(townCity_FieldName);
        readonly By postcode_Field = By.Id(postcode_FieldName);

        readonly By county_Field = By.Id(county_FieldName);
        readonly By country_Field = By.Id(country_FieldName);
        readonly By addressType_Field = By.Id(addressType_FieldName);
        readonly By startDate_Field = By.Id(startDate_FieldName);
        readonly By endDate_Field = By.Id(endDate_FieldName);

        readonly By addressSearch_Button = By.Id("CWFieldButton_AddressSearch");
        readonly By clearAddress_Button = By.Id("CWFieldButton_ClearAddress");

        readonly By addressType_MandatoryField = By.XPath("//*[@id='CWLabelHolder_addresstypeid']/label/span[@class='mandatory']");
        readonly By startDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_startdate']/label/span[@class ='mandatory']");

        #endregion

        #region Additional Demographics Fields

        readonly By maritalstatusid_FieldHeader = By.Id("CWLabelHolder_maritalstatusid");
        readonly By religionid_FieldHeader = By.Id("CWControlHolder_religionid");
        readonly By britishcitizenshipid_FieldHeader = By.Id("CWLabelHolder_britishcitizenshipid");
        readonly By ethnicityid_FieldHeader = By.Id("CWLabelHolder_ethnicityid");
        readonly By nationalityid_FieldHeader = By.Id("CWLabelHolder_nationalityid");
        readonly By countryofbirthid_FieldHeader = By.Id("CWLabelHolder_countryofbirthid");
        readonly By yearofentry_FieldHeader = By.Id("CWLabelHolder_yearofentry");
        readonly By disabilitystatusid_FieldHeader = By.Id("CWLabelHolder_disabilitystatusid");
        readonly By alwaysavailabletransport_FieldHeader = By.Id("CWField_alwaysavailabletransporttypeid_cwname");
        readonly By countryOfBirthNotKnown_FieldHeader = By.Id("CWLabelHolder_countryofbirthnotknown");
        readonly By notborninukbutcountryunknown_FieldHeader = By.Id("CWLabelHolder_notborninukbutcountryunknown");

        readonly By maritalstatusid_FieldLookup = By.Id("CWLookupBtn_maritalstatusid");
        readonly By religionid_FieldLookup = By.Id("CWLookupBtn_religionid");
        readonly By britishcitizenshipid_Field = By.Id("CWField_britishcitizenshipid");
        readonly By ethnicityid_FieldLookup = By.Id("CWLookupBtn_ethnicityid");
        readonly By nationalityid_FieldLookup = By.Id("CWLookupBtn_nationalityid");
        readonly By countryofbirthid_FieldLookup = By.Id("CWLookupBtn_countryofbirthid");
        readonly By yearofentry_Field = By.Id("CWField_yearofentry");
        readonly By disabilitystatusid_Field = By.Id("CWField_disabilitystatusid");
        readonly By alwaysavailabletransport_LookupButton = By.Id("CWLookupBtn_alwaysavailabletransporttypeid");
        readonly By alwaysavailabletransport_RemoveButton = By.Id("CWClearLookup_alwaysavailabletransporttypeid");
        readonly By alwaysavailabletransport_LinkField = By.Id("CWField_alwaysavailabletransporttypeid_Link");
        readonly By countryOfBirthNotKnown_Option = By.Id("CWLabelHolder_countryofbirthnotknown");
        readonly By countryofbirthnotknown_yesOption = By.Id("CWField_countryofbirthnotknown_1");
        readonly By countryofbirthnotknown_noOption = By.Id("CWField_countryofbirthnotknown_0");
        readonly By notborninukbutcountryunknown_Option = By.Id("CWLabelHolder_notborninukbutcountryunknown");
        readonly By notborninukbutcountryunknown_YesOption = By.Id("CWField_notborninukbutcountryunknown_1");
        readonly By notborninukbutcountryunknown_NoOption = By.Id("CWField_notborninukbutcountryunknown_0");
        readonly By yearOfEntry_FieldErrorArea = By.XPath("//span[@title='Please enter a value between 1000 and 9999.']");
        readonly By Pets_Picklist = By.Id("CWField_petstatusid");
        readonly By Smoker_Picklist = By.Id("CWField_smokerid");

        #endregion

        #region Phone&Email Fields

        readonly By workEmail_FieldHeader = By.Id("CWLabelHolder_workemail");
        readonly By secureemailaddress_FieldHeader = By.Id("CWLabelHolder_secureemailaddress");
        readonly By personalemail_FieldHeader = By.Id("CWLabelHolder_personalemail");
        readonly By workphonelandline_FieldHeader = By.Id("CWLabelHolder_workphonelandline");
        readonly By personalphonelandline_FieldHeader = By.Id("CWLabelHolder_personalphonelandline");
        readonly By workphonemobile_FieldHeader = By.Id("CWLabelHolder_workphonemobile");
        readonly By personalphonemobile_FieldHeader = By.Id("CWLabelHolder_personalphonemobile");

        readonly By workEmail_Field = By.Id("CWField_workemail");
        readonly By secureemailaddress_Field = By.Id("CWField_secureemailaddress");
        readonly By personalemail_Field = By.Id("CWField_personalemail");
        readonly By workphonelandline_Field = By.Id("CWField_workphonelandline");
        readonly By personalphonelandline_Field = By.Id("CWField_personalphonelandline");
        readonly By workphonemobile_Field = By.Id("CWField_workphonemobile");
        readonly By personalphonemobile_Field = By.Id("CWField_personalphonemobile");

        #endregion

        #region Account Fields 

        readonly By owningbusinessunitid_FieldHeader = By.Id("CWLabelHolder_owningbusinessunitid");
        readonly By defaultLookup_FieldHeader = By.Id("CWLabelHolder_defaultteamid");
        readonly By authenticationproviderid_FieldHeader = By.Id("CWLabelHolder_authenticationproviderid");
        readonly By username_FieldHeader = By.Id("CWLabelHolder_username");
        readonly By password_FieldHeader = By.Id("CWLabelHolder_password");
        readonly By loggingLevel_FieldHeader = By.Id("CWLabelHolder_tracelevelid");
        readonly By accountlockedoutdate_FieldHeader = By.Id("CWLabelHolder_accountlockedoutdate");
        readonly By accountlockedoutTime_FieldHeader = By.Id("CWLabelHolder_accountlockedoutdate_Time");
        readonly By failedpasswordattemptcount_FieldHeader = By.Id("CWLabelHolder_failedpasswordattemptcount");
        readonly By lastfailedpasswordattemptdate_FieldHeader = By.Id("CWLabelHolder_lastfailedpasswordattemptdate");
        readonly By lastfailedpasswordattemptTime_FieldHeader = By.Id("CWLabelHolder_lastfailedpasswordattemptdate_Time");
        readonly By isaccountlocked_FieldHeader = By.Id("CWLabelHolder_isaccountlocked");
        readonly By inactive_FieldHeader = By.Id("CWLabelHolder_inactive");
        readonly By inactive_Field_YesOption = By.Id("CWField_inactive_1");
        readonly By requiresLogin_YesOption = By.Id("CWField_canlogin_1");
        readonly By requiresLogin_NoOption = By.Id("CWField_canlogin_0");

        readonly By option = By.XPath("//*[@id=CWField_owningbusinessunitid]/option[2]");
        readonly By owningbusinessunitid_Field = By.XPath("//*[@id='CWField_owningbusinessunitid']");
        readonly By defaultLookup_Field = By.Id("CWLookupBtn_defaultteamid");
        readonly By authenticationproviderid_Field = By.Id("CWField_authenticationproviderid");
        readonly By username_Field = By.Id("CWField_username");
        readonly By password_Field = By.Id("CWField_password");
        readonly By changePassword_Link = By.XPath("//*[@id='CWField_password' and text()='Change Password']");
        readonly By loggingLevel_Field = By.Id("CWField_tracelevelid");
        readonly By accountlockedoutdate_Field = By.Id("CWField_accountlockedoutdate");
        readonly By accountlockedoutTime_Field = By.Id("CWField_accountlockedoutdate_Time");
        readonly By failedpasswordattemptcount_Field = By.Id("CWField_failedpasswordattemptcount");
        readonly By lastfailedpasswordattemptdate_Field = By.Id("CWField_lastfailedpasswordattemptdate");
        readonly By lastfailedpasswordattemptTime_Field = By.Id("CWField_lastfailedpasswordattemptdate_Time");
        readonly By isaccountlocked_YesOption = By.Id("CWField_isaccountlocked_1");
        readonly By isaccountlocked_NoOption = By.Id("CWField_isaccountlocked_0");

        #endregion

        #region Setting Fields
        readonly By recordsPerPage_FieldHeader = By.Id("CWLabelHolder_recordsperpageid");
        readonly By systemLanguage_FieldHeader = By.Id("CWLabelHolder_languageid");
        readonly By timeZone_FieldHeader = By.Id("CWLabelHolder_timezoneid");
        readonly By canAuthoriseForm_FieldHeader = By.Id("CWLabelHolder_canauthoriseform");
        readonly By canAuthorisedBehalfOfOthers_FieldHeader = By.Id("CWLabelHolder_canauthoriseonbehalfofothers");
        readonly By disableformautosave_FieldHeader = By.Id("CWLabelHolder_disableformautosave");
        readonly By cancompletednar_FieldHeader = By.Id("CWLabelHolder_cancompletednar");
        readonly By canauthorisednar_FieldHeader = By.Id("CWLabelHolder_canauthorisednar");
        readonly By ismanager_FieldHeader = By.Id("CWLabelHolder_ismanager");


        readonly By recordsPerPage_Field = By.Id("CWField_recordsperpageid");
        readonly By systemLanguage_Field = By.Id("CWField_languageid");
        readonly By timezoneid_Field = By.Id("CWField_timezoneid");
        readonly By comment_Field = By.Id("CWField_comment");
        readonly By comment_FiledWithoutSecurityProfile = By.XPath("//*[@id='CWControlHolder_comment']/span");
        readonly By comment_FieldHeader = By.Id("CWLabelHolder_comment");
        #endregion

        #region Employment Details Fields

        readonly By professiontypeid_FieldHeader = By.Id("CWLabelHolder_professiontypeid");
        readonly By professionalregistrationnumber_FieldHeader = By.Id("CWLabelHolder_professionalregistrationnumber");
        readonly By alternativeStaffNumber_FieldHeader = By.XPath("//*[@id='CWLabelHolder_alternativestaffnumber']/label");
        readonly By jobtitle_FieldHeader = By.Id("CWLabelHolder_jobtitle");
        readonly By NINumber_FieldHeader = By.Id("CWField_ninumber");
        readonly By EmploymentStatus_FieldHeader = By.XPath("//*[@id='CWLabelHolder_employmentstatusid']/label");

        readonly By employmentStatus_Field_Disabled = By.XPath("//*[@id='CWField_employmentstatusid' and @disabled = 'disabled']");

        readonly By professiontypeid_FieldLookup = By.Id("CWLookupBtn_professiontypeid");
        readonly By EmploymentStatus_Picklist = By.XPath("//*[@id='CWField_employmentstatusid']");
        readonly By professionalregistrationnumber_Field = By.Id("CWField_professionalregistrationnumber");
        readonly By alternativeStaffNumber_Field = By.Id("CWField_alternativestaffnumber");
        readonly By jobtitle_Field = By.Id("CWField_jobtitle");
        readonly By availableFrom_field = By.Id("CWField_availablefrom");
        readonly By optedOutOfWorkingTimeDirective_PicklistField = By.Id("CWField_optedoutofworkingtimedirectiveid");
        readonly By allowUseGdprDataField_label = By.XPath("//*[@id = 'CWLabelHolder_allowusegdprdata']/label");
        readonly By allowUseGdprData_YesOption = By.Id("CWField_allowusegdprdata_1");
        readonly By allowUseGdprData_NoOption = By.Id("CWField_allowusegdprdata_0");

        readonly By maximumworkinghours = By.Id("CWField_maximumworkinghours");
        readonly By maximumworkinghours_ErroLabel = By.XPath("//*[@id='CWControlHolder_maximumworkinghours']/label/span");

        #endregion

        #region Legacy System Data

        readonly By legacyComments_Fields_Disabled = By.XPath("//*[@id='CWField_comment' and @disabled = 'disabled']");

        #endregion

        #region Tabs

        readonly By detailsSection = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");
        readonly By teamsSection = By.XPath("//li[@id='CWNavGroup_Teams']/a[@title='Teams']");
        readonly By recruitmentDashboardSection = By.XPath("//*[@id='CWNavGroup_ApplicantDasboard']/a");

        #endregion

        #region Left Side Menu

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        #region Sections

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//details[@id='8931b5ac-fd14-e811-80d9-005056050630'][@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//*[@id='8931b5ac-fd14-e811-80d9-005056050630']/summary/div");

        readonly By employmentDetailsElementExpanded = By.XPath("//details[@id='741c7810-b464-ec11-a344-0050569231cf'][@open]");
        readonly By employmentLeftSubMenu = By.XPath("//*[@id='741c7810-b464-ec11-a344-0050569231cf']/summary/div");

        readonly By recruitmentDetailsElementExpanded = By.XPath("//details[@id='f0cc6f53-1eaf-ec11-a350-0050569231cf'][@open]");
        readonly By recruitmentLeftSubMenu = By.XPath("//*[@id='f0cc6f53-1eaf-ec11-a350-0050569231cf']/summary/div");

        #endregion

        #region Related Items

        readonly By accessAuditLeftSubMenuItem = By.Id("CWNavItem_UserAccessAudit");
        readonly By addressSubMenuItem = By.Id("CWNavItem_Addresses");
        readonly By aliasesSubMenuItem = By.Id("CWNavItem_Aliases");
        readonly By auditLeftSubMenuItem = By.Id("CWNavItem_AuditHistory");
        readonly By devicesLeftSubMenuItem = By.Id("CWNavItem_Devices");
        readonly By emergencyContactsSubMenuItem = By.Id("CWNavItem_EmergencyContacts");
        readonly By languagesLeftSubMenuItem = By.Id("CWNavItem_Languages");
        readonly By securityProfilesLeftSubMenu = By.Id("CWNavItem_UserSecurityProfile");
        readonly By applicationLeftSubMenuItem = By.Id("CWNavItem_UserApplication");
        readonly By teamsLeftSubMenuItem = By.Id("CWNavItem_Teams");

        #endregion

        #region Employment

        readonly By absencesLeftSubMenuItem = By.Id("CWNavItem_Absences");
        readonly By employmentContractsLeftSubMenuItem = By.Id("CWNavItem_UserEmploymentContracts");
        readonly By myStaffReviewsLeftSubMenuItems = By.Id("CWNavItem_SystemUserStaffReview");
        readonly By payArrangementsLeftSubMenuItems = By.Id("CWNavItem_EmploymentPayArrangements");
        readonly By SuspensionsLeftSubMenuItem = By.Id("CWNavItem_Suspensions");
        readonly By trainingLeftSubMenuItem = By.Id("CWNavItem_Training");
        readonly By workersIdsLeftSubMenuItems = By.Id("CWNavItem_WorkersIds");

        #endregion

        #region Recruitment

        readonly By recruitmentDocumentsSubMenuItem = By.Id("CWNavItem_Documents");
        readonly By rollApplicationsSubMenuItem = By.Id("CWNavItem_RoleApplications");

        #endregion

        /**CDV6 locators**/
        readonly By workScheduleLeftSubMenuItem = By.Id("CWNavItem_UserWorkSchedule");
        /**CDV6 locators**/

        #endregion




        public SystemUserRecordPage WaitForSystemUserRecordPageToLoad()
        {
            WaitForElementNotVisible("CWRefreshPanel", 45);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(MenuButton);
            WaitForElement(detailsSection);
            WaitForElement(teamsSection);


            return this;
        }

        public SystemUserRecordPage WaitForSystemUserNoTeamsRecordPageToLoad()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(MenuButton);
            WaitForElement(detailsSection);


            return this;
        }

        public SystemUserRecordPage WaitForSystemUserRecordCannotBeAccessedPageToLoad()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);


            return this;
        }

        public SystemUserRecordPage WaitForSystemUserRecordPageToLoad_ActivateRecord()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(MenuButton);
            WaitForElement(detailsSection);

            WaitForElement(teamsSection);



            return this;
        }

        public SystemUserRecordPage WaitForNewSystemUserRecordPageToLoad()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(clearAddress_Button);

            return this;
        }

        public SystemUserRecordPage WaitForSystemUserRecordPageToLoadFromAdvancedSearch()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

        public SystemUserRecordPage NavigateToTeamsSubPage()
        {
            WaitForElementToBeClickable(teamsSection);
            Click(teamsSection);

            return this;
        }

        public SystemUserRecordPage NavigateToRecruitmentDashboardSubPage()
        {
            WaitForElementToBeClickable(recruitmentDashboardSection);
            Click(recruitmentDashboardSection);

            return this;
        }

        public SystemUserRecordPage NavigateToAvailabilityPage()
        {
            WaitForElementToBeClickable(availability);
            Click(availability);

            return this; ;
        }

        public SystemUserRecordPage NavigateToEmployeeSchedulePage()
        {
            WaitForElementToBeClickable(employeeSchedule);
            Click(employeeSchedule);

            return this; ;
        }

        public SystemUserRecordPage NavigateToEmployeeDiaryPage()
        {
            WaitForElementToBeClickable(employeeDiary);
            Click(employeeDiary);

            return this; ;
        }

        public SystemUserRecordPage NavigateToLanguagesSubPage()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElement(languagesLeftSubMenuItem);
            Click(languagesLeftSubMenuItem);

            return this;
        }

        public SystemUserRecordPage NavigateToRelatedItemsSubPage_Address()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElement(addressSubMenuItem);
            Click(addressSubMenuItem);

            return this;
        }

        public SystemUserRecordPage NavigateToEmergencyContactsSubPage()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElement(emergencyContactsSubMenuItem);
            Click(emergencyContactsSubMenuItem);

            return this;
        }

        public SystemUserRecordPage NavigateToEmploymentContractsSubPage()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(employmentDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElement(employmentLeftSubMenu);
                Click(employmentLeftSubMenu);
            }

            WaitForElement(employmentContractsLeftSubMenuItem);
            Click(employmentContractsLeftSubMenuItem);

            return this;
        }

        public SystemUserRecordPage NavigateToTrainingSubPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(employmentDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElement(employmentLeftSubMenu);
                Click(employmentLeftSubMenu);
            }

            WaitForElementToBeClickable(trainingLeftSubMenuItem);
            Click(trainingLeftSubMenuItem);

            return this;
        }

        public SystemUserRecordPage NavigateToAbsencesSubPage()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(employmentDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElement(employmentLeftSubMenu);
                Click(employmentLeftSubMenu);
            }

            WaitForElement(absencesLeftSubMenuItem);
            Click(absencesLeftSubMenuItem);

            return this;
        }

        public SystemUserRecordPage NavigateToRecruitmentDocumentsSubPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(recruitmentDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(recruitmentLeftSubMenu);
                Click(recruitmentLeftSubMenu);
            }

            WaitForElementToBeClickable(recruitmentDocumentsSubMenuItem);
            Click(recruitmentDocumentsSubMenuItem);

            return this;
        }

        public SystemUserRecordPage NavigateToRollApplicationsSubPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(recruitmentDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(recruitmentLeftSubMenu);
                Click(recruitmentLeftSubMenu);
            }

            WaitForElementToBeClickable(rollApplicationsSubMenuItem);
            Click(rollApplicationsSubMenuItem);

            return this;
        }

        public SystemUserRecordPage ValidateRelatedItems()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementVisible(accessAuditLeftSubMenuItem);
            WaitForElementVisible(addressSubMenuItem);
            WaitForElementVisible(aliasesSubMenuItem);
            WaitForElementVisible(applicationLeftSubMenuItem);
            WaitForElementVisible(auditLeftSubMenuItem);
            WaitForElementVisible(emergencyContactsSubMenuItem);
            WaitForElementVisible(languagesLeftSubMenuItem);
            WaitForElementVisible(securityProfilesLeftSubMenu);
            WaitForElementVisible(teamsLeftSubMenuItem);



            return this;
        }

        public SystemUserRecordPage ValidateEmploymentSubMenuLink()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(employmentDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementVisible(employmentLeftSubMenu);
            }

            return this;
        }

        public SystemUserRecordPage ClickTrainingSubPageLink()
        {
            WaitForElementToBeClickable(trainingLeftSubMenuItem);
            Click(trainingLeftSubMenuItem);

            return this;
        }

        public SystemUserRecordPage ValidateEmploymentSubMenuItems()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(employmentDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElement(employmentLeftSubMenu);
                Click(employmentLeftSubMenu);
            }

            WaitForElementVisible(employmentContractsLeftSubMenuItem);
            WaitForElementVisible(myStaffReviewsLeftSubMenuItems);
            WaitForElementVisible(SuspensionsLeftSubMenuItem);
            WaitForElementVisible(trainingLeftSubMenuItem);


            return this;
        }

        public SystemUserRecordPage NavigateToRelatedItemsSubPage_Aliases()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElement(aliasesSubMenuItem);
            Click(aliasesSubMenuItem);

            return this;
        }

        public SystemUserRecordPage NavigateToStaffReviewSubPage()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(employmentDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElement(employmentLeftSubMenu);
                Click(employmentLeftSubMenu);
            }

            WaitForElement(myStaffReviewsLeftSubMenuItems);
            Click(myStaffReviewsLeftSubMenuItems);

            return this;

        }

        public SystemUserRecordPage ValidateToStaffReviewSubPage(string ExpectedText)
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(employmentDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElement(employmentLeftSubMenu);
                Click(employmentLeftSubMenu);
            }

            WaitForElement(myStaffReviewsLeftSubMenuItems);
            ValidateElementText(myStaffReviewsLeftSubMenuItems, ExpectedText);

            return this;

        }

        public SystemUserRecordPage NavigateToRelatedItemsSubPage_EmergencyContacts()
        {

            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElement(emergencyContactsSubMenuItem);
            Click(emergencyContactsSubMenuItem);

            return this;
        }

        public SystemUserRecordPage ValidateEmployeeTypeFieldValue(string ExpectedValue)
        {
            ValidateElementValue(EmployeeType_Field, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateFirstNameFieldValue(string ExpectedValue)
        {
            ValidateElementValue(FirstName_Field, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateEmploymentStatusSelectedText(string ExpectedValue)
        {
            WaitForElement(EmploymentStatus_Picklist);
            ValidatePicklistSelectedText(EmploymentStatus_Picklist, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateWorkSchedule_Navigation(string ExpectedValue)
        {
            ValidateElementTextContainsText(workSchedule, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateAvailability_Navigation(string ExpectedValue)
        {
            ValidateElementTextContainsText(availability, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateSecurityProfiles_Navigation(string ExpectedValue)
        {
            ValidateElementTextContainsText(securityProfiles, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateTeams_Navigation(string ExpectedValue)
        {
            ValidateElementTextContainsText(teams, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateDiary_Navigation(string ExpectedValue)
        {
            ValidateElementTextContainsText(diary, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateId_FieldValue(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(id_Field_Value);
                ValidateElementDisabled(id_Field_Value);
            }

            else
                WaitForElementNotVisible(id_Field_Value, 3);

            return this;
        }

        public SystemUserRecordPage ValidateLastNameFieldValue(string ExpectedValue)
        {
            ValidateElementValue(LastName_Field, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateUserNameFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(username_Field);
            ScrollToElement(username_Field);
            ValidateElementValue(username_Field, ExpectedValue);
            return this;
        }

        public SystemUserRecordPage ValidatePronounsLinkFieldText(string ExpectedValue)
        {
            ScrollToElement(pronouns_LinkField);
            WaitForElementVisible(pronouns_LinkField);
            ValidateElementByTitle(pronouns_LinkField, ExpectedValue);
            return this;
        }

        public SystemUserRecordPage ValidateLeftSideFields()
        {
            ValidateElementEnabled(propertyname_Field);
            ValidateElementEnabled(propertyno_Field);
            ValidateElementEnabled(street_Field);
            ValidateElementEnabled(villageDistrict_Field);
            ValidateElementEnabled(townCity_Field);
            ValidateElementEnabled(postcode_Field);
            //ValidateElementEnabled(addressSearch_Button);

            return this;
        }

        public SystemUserRecordPage ValidateRightSideFields()
        {
            ValidateElementEnabled(county_Field);
            ValidateElementEnabled(country_Field);
            ValidateElementEnabled(addressType_Field);
            ValidateElementEnabled(startDate_Field);
            ValidateElementEnabled(clearAddress_Button);

            return this;
        }

        public SystemUserRecordPage ValidateAddressTypeMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(addressType_MandatoryField);
            else
                WaitForElementNotVisible(addressType_MandatoryField, 3);

            return this;
        }

        public SystemUserRecordPage ValidateId_Field_Disabled(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(id_Field_disabled);
                ValidateElementDisabled(id_Field_disabled);
            }

            else
                WaitForElementNotVisible(id_Field_disabled, 3);

            return this;
        }

        public SystemUserRecordPage ValidateEmploymentStatus_Field_Disabled(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(employmentStatus_Field_Disabled);
                ValidateElementDisabled(employmentStatus_Field_Disabled);
            }

            else
                WaitForElementNotVisible(employmentStatus_Field_Disabled, 3);

            return this;
        }

        public SystemUserRecordPage ValidateLegacyComments_Field_Disabled(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(legacyComments_Fields_Disabled);
                ValidateElementDisabled(legacyComments_Fields_Disabled);
            }

            else
                WaitForElementNotVisible(legacyComments_Fields_Disabled, 3);

            return this;
        }

        public SystemUserRecordPage ValidateStartDateMandatoryFieldSignVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(startDate_MandatoryField);
            else
                WaitForElementNotVisible(startDate_MandatoryField, 3);

            return this;
        }

        public SystemUserRecordPage InsertPostCode(string TextToInsert)
        {
            WaitForElement(postcode_Field);
            SendKeys(postcode_Field, TextToInsert);

            return this;
        }

        public SystemUserRecordPage ClickActivateButton()
        {
            WaitForElement(activate_Button);
            Click(activate_Button);

            return this;
        }

        public SystemUserRecordPage InserttownCity(string TextToInsert)
        {
            WaitForElement(townCity_Field);
            SendKeys(townCity_Field, TextToInsert);

            return this;
        }

        public SystemUserRecordPage ValidateAddressType_Options(String TextToFind)
        {
            ValidatePicklistContainsElementByText(addressType_Field, "Home");
            ValidatePicklistContainsElementByText(addressType_Field, "Work");

            return this;
        }

        public SystemUserRecordPage ValidateTabForSearchAddress_Button()
        {
            ScrollToElement(postcode_Field);
            WaitForElementToBeClickable(postcode_Field);
            SendKeysWithoutClearing(postcode_Field, Keys.Tab);
            WaitForElementToBeClickable(addressSearch_Button);
            Click(addressSearch_Button);

            return this;
        }

        public SystemUserRecordPage ValidateTabForClearAddress_Button()
        {

            ScrollToElement(startDate_Field);
            WaitForElementToBeClickable(startDate_Field);
            SendKeys(startDate_Field, "20/12/2021");
            ScrollToElement(clearAddress_Button);
            WaitForElementToBeClickable(clearAddress_Button);
            Click(clearAddress_Button);

            return this;
        }

        public SystemUserRecordPage validateStartDate_Field(string Expected)
        {
            ValidateElementValue(startDate_Field, Expected);

            return this;
        }

        public SystemUserRecordPage ClickTitleLookupButton()
        {
            WaitForElement(demographicstitleid_FieldLookup);
            Click(demographicstitleid_FieldLookup);

            return this;
        }

        public SystemUserRecordPage ClickPronounsLookupButton()
        {
            ScrollToElement(pronounsField_LookupButton);
            WaitForElementToBeClickable(pronounsField_LookupButton);
            Click(pronounsField_LookupButton);

            return this;
        }

        public SystemUserRecordPage SelectEmployeeType(string ValueToSelect)
        {
            WaitForElementVisible(EmployeeType_Field);
            ScrollToElement(EmployeeType_Field);
            SelectPicklistElementByText(EmployeeType_Field, ValueToSelect);

            return this;
        }

        public SystemUserRecordPage InsertFirstName(string TextToInsert)
        {
            WaitForElement(FirstName_Field);
            SendKeys(FirstName_Field, TextToInsert);

            return this;
        }

        public SystemUserRecordPage InsertMiddleName(string TextToInsert)
        {
            WaitForElement(middlename_Field);
            SendKeys(middlename_Field, TextToInsert);

            return this;
        }

        public SystemUserRecordPage InsertLastName(string TextToInsert)
        {
            WaitForElement(LastName_Field);
            SendKeys(LastName_Field, TextToInsert);

            return this;
        }

        public SystemUserRecordPage InsertBirthDate(string DateToInsert)
        {
            WaitForElement(dateofbirth_Field);
            SendKeys(dateofbirth_Field, DateToInsert);

            return this;
        }

        public SystemUserRecordPage SelectGender_Options(string TextToSelect)
        {
            WaitForElement(persongenderid_Field);
            SelectPicklistElementByText(persongenderid_Field, TextToSelect);

            return this;
        }

        public SystemUserRecordPage ClickBrowsButton()
        {
            WaitForElement(browse_Button);
            Click(browse_Button);

            return this;
        }

        public SystemUserRecordPage InsertPropertyName(string StringToInsert)
        {
            WaitForElementToBeClickable(propertyname_Field);
            SendKeys(propertyname_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertPropertyNo(string StringToInsert)
        {
            WaitForElement(propertyno_Field);
            SendKeys(propertyno_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertStreetName(string StringToInsert)
        {
            WaitForElement(street_Field);
            SendKeys(street_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertVillageDistrict(string StringToInsert)
        {
            WaitForElement(villageDistrict_Field);
            SendKeys(villageDistrict_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertTownCity(string StringToInsert)
        {
            WaitForElement(townCity_Field);
            SendKeys(townCity_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertCounty(string StringToInsert)
        {
            WaitForElement(county_Field);
            SendKeys(county_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertCountry(string StringToInsert)
        {
            WaitForElement(country_Field);
            SendKeys(country_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertStartDate(string StringToInsert)
        {
            WaitForElement(startDate_Field);
            SendKeys(startDate_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage SelectAddressType_Options(string TextToSelect)
        {
            WaitForElement(addressType_Field);
            SelectPicklistElementByText(addressType_Field, TextToSelect);

            return this;
        }

        public SystemUserRecordPage ClickMaritalStatusLookupButton()
        {
            WaitForElement(maritalstatusid_FieldLookup);
            Click(maritalstatusid_FieldLookup);

            return this;
        }

        public SystemUserRecordPage ClickReligionLookupButton()
        {
            WaitForElement(religionid_FieldLookup);
            Click(religionid_FieldLookup);

            return this;
        }

        public SystemUserRecordPage ClickAlwaysAvailableTransportLookupButton()
        {
            WaitForElement(alwaysavailabletransport_LookupButton);
            Click(alwaysavailabletransport_LookupButton);

            return this;
        }

        public SystemUserRecordPage ClickAlwaysAvailableTransportRemoveButton()
        {
            WaitForElement(alwaysavailabletransport_RemoveButton);
            Click(alwaysavailabletransport_RemoveButton);

            return this;
        }

        public SystemUserRecordPage ValidateAlwaysAvailableTransportFieldIsEmpty()
        {
            WaitForElement(alwaysavailabletransport_LookupButton);
            ScrollToElement(alwaysavailabletransport_LookupButton);
            WaitForElementVisible(alwaysavailabletransport_LookupButton);
            WaitForElementNotVisible(alwaysavailabletransport_RemoveButton, 3);
            WaitForElementNotVisible(alwaysavailabletransport_LinkField, 3);

            return this;
        }

        public SystemUserRecordPage ValidateAlwaysAvailableTranspotLinkFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(alwaysavailabletransport_LinkField);
            ValidateElementText(alwaysavailabletransport_LinkField, ExpectedText);

            return this;
        }

        public SystemUserRecordPage ClickDefaultTeamLookupButton()
        {
            WaitForElement(defaultLookup_Field);
            Click(defaultLookup_Field);

            return this;
        }

        public SystemUserRecordPage ClickEthnicityLookupButton()
        {
            WaitForElement(ethnicityid_FieldLookup);
            Click(ethnicityid_FieldLookup);

            return this;
        }

        public SystemUserRecordPage ClickNationalityLookupButton()
        {
            WaitForElement(nationalityid_FieldLookup);
            Click(nationalityid_FieldLookup);

            return this;
        }

        public SystemUserRecordPage ClickCountryOfBirthLookupButton()
        {
            WaitForElement(countryofbirthid_FieldLookup);
            Click(countryofbirthid_FieldLookup);

            return this;
        }

        public SystemUserRecordPage SelectBrithishCitizenship_Options(string TextToSelect)
        {
            WaitForElement(britishcitizenshipid_Field);
            SelectPicklistElementByText(britishcitizenshipid_Field, TextToSelect);

            return this;
        }

        public SystemUserRecordPage SelectDisabilityStatus_Options(string TextToSelect)
        {
            WaitForElement(disabilitystatusid_Field);
            SelectPicklistElementByValue(disabilitystatusid_Field, TextToSelect);

            return this;
        }

        public SystemUserRecordPage ValidateToolTipTextForPropertyName(string ExpectedText)
        {

            ValidateElementToolTip(propertyname_Field, ExpectedText);

            return this;
        }

        public SystemUserRecordPage InsertYearOfEntryToUK(string yearToInsert)
        {
            WaitForElement(yearofentry_Field);
            SendKeys(yearofentry_Field, yearToInsert);

            return this;
        }

        public SystemUserRecordPage InsertWorkEmail(string StringToInsert)
        {
            WaitForElement(workEmail_Field);
            SendKeys(workEmail_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertSecureEmail(string StringToInsert)
        {
            WaitForElement(secureemailaddress_Field);
            SendKeys(secureemailaddress_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage ValidateToolTipTextForPropertyNo(string ExpectedText)
        {

            ValidateElementToolTip(propertyno_Field, ExpectedText);

            return this;
        }

        public SystemUserRecordPage InsertPersonalEmail(string StringToInsert)
        {
            WaitForElement(personalemail_Field);
            SendKeys(personalemail_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertWorkPhoneLandline(string StringToInsert)
        {
            WaitForElement(workphonelandline_Field);
            SendKeys(workphonelandline_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertWorkPhoneMobile(string StringToInsert)
        {
            WaitForElement(workphonemobile_Field);
            SendKeys(workphonemobile_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage ValidateToolTipTextForStreet(string ExpectedText)
        {

            ValidateElementToolTip(street_Field, ExpectedText);

            return this;
        }

        public SystemUserRecordPage InsertPersonalPhoneLandline(string StringToInsert)
        {
            WaitForElement(personalphonelandline_Field);
            SendKeys(personalphonelandline_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertPersonalPhoneMobile(string StringToInsert)
        {
            WaitForElement(personalphonemobile_Field);
            SendKeys(personalphonemobile_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage SelectBusinessUnitByValue(string ValueToSelect)
        {
            Thread.Sleep(2000);
            WaitForElement(owningbusinessunitid_Field);
            ScrollToElement(owningbusinessunitid_Field);
            SelectPicklistElementByValue(owningbusinessunitid_Field, ValueToSelect);


            return this;
        }

        public SystemUserRecordPage SelectBusinessUnitByText(string TextToSelect)
        {
            Thread.Sleep(2000);
            WaitForElement(owningbusinessunitid_Field);
            ScrollToElement(owningbusinessunitid_Field);
            SelectPicklistElementByText(owningbusinessunitid_Field, TextToSelect);


            return this;
        }

        public SystemUserRecordPage SelectAuthenticationProviderid_Options(string TextToSelect)
        {
            WaitForElement(authenticationproviderid_Field);
            SelectPicklistElementByText(authenticationproviderid_Field, TextToSelect);

            return this;
        }

        public SystemUserRecordPage SelectLoggingLevel_Options(string TextToSelect)
        {
            WaitForElement(loggingLevel_Field);
            SelectPicklistElementByText(loggingLevel_Field, TextToSelect);

            return this;
        }

        public SystemUserRecordPage ValidateToolTipTextForVillageDistrict(string ExpectedText)
        {

            ValidateElementToolTip(villageDistrict_Field, ExpectedText);

            return this;
        }

        public SystemUserRecordPage ValidateToolTipTextForTownCity(string ExpectedText)
        {

            ValidateElementToolTip(townCity_Field, ExpectedText);

            return this;
        }

        public SystemUserRecordPage InsertUserName(string StringToInsert)
        {
            WaitForElement(username_Field);
            SendKeys(username_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertAccountLookedOutDate(string StringToInsert)
        {
            WaitForElement(accountlockedoutdate_Field);
            SendKeys(accountlockedoutdate_Field, StringToInsert + Keys.Tab);

            return this;
        }

        public SystemUserRecordPage ValidateToolTipTextForPostCode(string ExpectedText)
        {

            ValidateElementToolTip(postcode_Field, ExpectedText);

            return this;
        }

        public SystemUserRecordPage InsertAccountLookedOutTime(string StringToInsert)
        {
            WaitForElement(accountlockedoutTime_Field);
            SendKeys(accountlockedoutTime_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertFailedPasswordAttemptCount(string StringToInsert)
        {
            WaitForElement(failedpasswordattemptcount_Field);
            SendKeys(failedpasswordattemptcount_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage ValidateToolTipTextForCounty(string ExpectedText)
        {

            ValidateElementToolTip(county_Field, ExpectedText);

            return this;
        }

        public SystemUserRecordPage InsertLastFailedPasswordAttemptDate(string StringToInsert)
        {
            WaitForElement(lastfailedpasswordattemptdate_Field);
            SendKeys(lastfailedpasswordattemptdate_Field, StringToInsert + Keys.Tab);

            return this;
        }

        public SystemUserRecordPage InsertLastFailedPasswordAttemptTime(string StringToInsert)
        {
            WaitForElement(lastfailedpasswordattemptTime_Field);
            SendKeys(lastfailedpasswordattemptTime_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage ValidateToolTipTextForCountry(string ExpectedText)
        {

            ValidateElementToolTip(country_Field, ExpectedText);

            return this;
        }

        public SystemUserRecordPage ClickProfessionTypeLookupButton()
        {
            WaitForElement(professiontypeid_FieldLookup);
            Click(professiontypeid_FieldLookup);

            return this;
        }

        public SystemUserRecordPage InsertProfessionalRegistrationNumber(string StringToInsert)
        {
            WaitForElement(professionalregistrationnumber_Field);
            SendKeys(professionalregistrationnumber_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertAlternativeStaffNumber(string StringToInsert)
        {
            WaitForElement(alternativeStaffNumber_Field);
            SendKeys(alternativeStaffNumber_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage ValidateAlternativeStaffNumber(string ExpectedText)
        {
            WaitForElement(alternativeStaffNumber_Field);
            ValidateElementValue(alternativeStaffNumber_Field, ExpectedText);

            return this;
        }

        public SystemUserRecordPage InsertJobTitle(string StringToInsert)
        {
            WaitForElement(jobtitle_Field);
            SendKeys(jobtitle_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertAvailableFromDateField(string stringToInsert)
        {
            WaitForElementToBeClickable(availableFrom_field);
            ScrollToElement(availableFrom_field);
            SendKeys(availableFrom_field, stringToInsert);

            return this;
        }

        public SystemUserRecordPage InsertMaximumWorkingHours(string valueToInsert)
        {
            WaitForElementToBeClickable(maximumworkinghours);
            SendKeys(maximumworkinghours, valueToInsert + Keys.Tab);

            return this;
        }

        public SystemUserRecordPage ValidateMaximumWorkingHours(string ExpectedValue)
        {
            WaitForElement(maximumworkinghours);
            ValidateElementValue(maximumworkinghours, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateMaximumWorkingHoursErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(maximumworkinghours_ErroLabel);
            else
                WaitForElementNotVisible(maximumworkinghours_ErroLabel, 3);

            return this;
        }

        public SystemUserRecordPage ValidateMaximumWorkingHoursErrorLabelText(string ExpectedText)
        {
            WaitForElement(maximumworkinghours_ErroLabel);
            ValidateElementText(maximumworkinghours_ErroLabel, ExpectedText);

            return this;
        }

        public SystemUserRecordPage ValidateToolTipTextForAddressType(string ExpectedText)
        {

            ValidateElementToolTip(addressType_Field, ExpectedText);


            return this;
        }

        public SystemUserRecordPage SelectRecordsPerPage_Options(string TextToSelect)
        {
            WaitForElement(recordsPerPage_Field);
            SelectPicklistElementByText(recordsPerPage_Field, TextToSelect);

            return this;
        }

        public SystemUserRecordPage SelectSystemLanguage_Options(string TextToSelect)
        {
            WaitForElement(systemLanguage_Field);
            SelectPicklistElementByText(systemLanguage_Field, TextToSelect);

            return this;
        }

        public SystemUserRecordPage SelectTimeZone_Options(string TextToSelect)
        {
            WaitForElement(timezoneid_Field);
            SelectPicklistElementByValue(timezoneid_Field, TextToSelect);


            return this;
        }

        public SystemUserRecordPage ValidateToolTipTextForStartDate(string ExpectedText)
        {

            ValidateElementToolTip(startDate_Field, ExpectedText);

            return this;
        }

        public SystemUserRecordPage InsertComment(string StringToInsert)
        {
            WaitForElement(comment_Field);
            SendKeys(comment_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            System.Threading.Thread.Sleep(1000);

            return this;

        }

        public SystemUserRecordPage CloseNotificationMessage()
        {
            WaitForElementToBeClickable(notificationMessage_CloseButton);
            ScrollToElement(notificationMessage_CloseButton);
            Click(notificationMessage_CloseButton);
            return this;
        }

        public SystemUserRecordPage ClickInactiveStatus_YesOption()
        {
            WaitForElementToBeClickable(inactive_Field_YesOption);
            ScrollToElement(inactive_Field_YesOption);
            Click(inactive_Field_YesOption);

            return this;
        }

        public SystemUserRecordPage ClickRequiresLogin_YesOption()
        {
            WaitForElementToBeClickable(requiresLogin_YesOption);
            ScrollToElement(requiresLogin_YesOption);
            Click(requiresLogin_YesOption);

            return this;
        }

        public SystemUserRecordPage ClickRequiresLogin_NoOption()
        {
            WaitForElementToBeClickable(requiresLogin_NoOption);
            ScrollToElement(requiresLogin_NoOption);
            Click(requiresLogin_NoOption);

            return this;
        }

        public SystemUserRecordPage ClickBackButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            System.Threading.Thread.Sleep(500);

            return this;

        }

        public SystemUserRecordPage ValidateToolTipTextForEndDate(string ExpectedText)
        {

            ValidateElementToolTip(endDate_Field, ExpectedText);


            return this;
        }

        public SystemUserRecordPage InsertPassword(string StringToInsert)
        {
            WaitForElement(password_Field);
            SendKeys(password_Field, StringToInsert);

            return this;
        }

        public SystemUserRecordPage ValidateAddressType_FieldText(String expectedtext)
        {
            WaitForElementVisible(addressType_Field);
            ScrollToElement(addressType_Field);
            ValidatePicklistSelectedText(addressType_Field, expectedtext);


            return this;
        }

        public SystemUserRecordPage ValidateBusinessType_FieldText(String expectedtext)
        {
            ValidateElementValue(owningbusinessunitid_Field, expectedtext);


            return this;
        }

        public SystemUserRecordPage ValidateRecordsPerPage_FieldValue(String expectedtext)
        {
            ValidateElementValue(recordsPerPage_Field, expectedtext);


            return this;
        }

        public SystemUserRecordPage ValidatePropertyNameMaximumLimitText(string expected)
        {
            ValidateElementMaxLength(propertyname_Field, expected);

            return this;
        }

        public SystemUserRecordPage ValidatePropertyNoMaximumLimitText(string expected)
        {
            ValidateElementMaxLength(propertyno_Field, expected);

            return this;
        }

        public SystemUserRecordPage ValidateStreetMaximumLimitText(string expected)
        {
            ValidateElementMaxLength(street_Field, expected);


            return this;
        }

        public SystemUserRecordPage ValidateVillageMaximumLimitText(string expected)
        {
            ValidateElementMaxLength(villageDistrict_Field, expected);

            return this;
        }

        public SystemUserRecordPage ValidateTownCityMaximumLimitText(string expected)
        {
            ValidateElementMaxLength(townCity_Field, expected);

            return this;
        }

        public SystemUserRecordPage ValidateCountyMaximumLimitText(string expected)
        {
            ValidateElementMaxLength(county_Field, expected);

            return this;
        }

        public SystemUserRecordPage ValidateCountryMaximumLimitText(string expected)
        {
            ValidateElementMaxLength(country_Field, expected);

            return this;
        }

        public SystemUserRecordPage ValidatePostcodeMaximumLimitText(string expected)
        {
            ValidateElementMaxLength(postcode_Field, expected);

            return this;
        }

        public SystemUserRecordPage ClickClearAddressButton()
        {
            WaitForElementToBeClickable(clearAddress_Button);
            Click(clearAddress_Button);

            return this;
        }

        public SystemUserRecordPage ValidateNotificationMessage(string ExpectedMessage)
        {
            WaitForElementVisible(notificationMessageArea);

            ValidateElementText(notificationMessageArea, ExpectedMessage);

            return this;
        }

        public SystemUserRecordPage ValidateAddressTypeFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(addressType_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public SystemUserRecordPage ValidatestartDateFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(startDate_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public SystemUserRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);
            return this;

        }

        public SystemUserRecordPage ValidateGeneralSectionFields()
        {
            ValidateElementEnabled(demographicstitleid_FieldLookup);
            ValidateElementEnabled(middlename_Field);
            ValidateElementEnabled(persongenderid_Field);
            ValidateElementEnabled(dateofbirth_Field);
            GetElementVisibility(UserId_Field);

            return this;
        }

        public SystemUserRecordPage ValidateAdditionalDemographicsSectionFields()
        {
            ValidateElementEnabled(maritalstatusid_FieldLookup);
            ValidateElementEnabled(religionid_FieldLookup);
            ValidateElementEnabled(britishcitizenshipid_Field);
            ValidateElementEnabled(ethnicityid_FieldLookup);
            ValidateElementEnabled(nationalityid_FieldLookup);
            ValidateElementEnabled(countryofbirthid_FieldLookup);
            ValidateElementEnabled(yearofentry_Field);
            ValidateElementEnabled(disabilitystatusid_Field);
            ValidateElementEnabled(alwaysavailabletransport_LookupButton);
            ValidateElementEnabled(countryOfBirthNotKnown_Option);
            ValidateElementEnabled(notborninukbutcountryunknown_Option);

            return this;
        }

        public SystemUserRecordPage ValidateCountryOfBirthNotKnownDefaultValue(string expected)
        {
            WaitForElement(countryOfBirthNotKnown_Option);
            ValidateElementValue(countryOfBirthNotKnown_Option, expected);

            return this;
        }

        public SystemUserRecordPage ValidateNotBornInUKButCountryUnknownDefaultValue(string expected)
        {
            WaitForElement(notborninukbutcountryunknown_Option);
            ValidateElementValue(notborninukbutcountryunknown_Option, expected);

            return this;
        }

        public SystemUserRecordPage ValidateBritishCitizenshipPickListValues(string expected)
        {
            WaitForElement(britishcitizenshipid_Field);
            ValidatePicklistContainsElementByText(britishcitizenshipid_Field, expected);

            return this;
        }

        public SystemUserRecordPage SelectCountryofBirthNotKnown_yes()
        {
            WaitForElement(countryofbirthnotknown_yesOption);
            ScrollToElement(countryofbirthnotknown_yesOption);
            Click(countryofbirthnotknown_yesOption);

            return this;
        }

        public SystemUserRecordPage ValidateNotBornInUKButCountryUnknownFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(notborninukbutcountryunknown_Option);
                ScrollToElement(notborninukbutcountryunknown_Option);
            }

            else
                WaitForElementNotVisible(notborninukbutcountryunknown_Option, 3);

            return this;
        }

        public SystemUserRecordPage ValidateCountryOfBirthFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(countryofbirthid_FieldLookup);
            else
                WaitForElementNotVisible(countryofbirthid_FieldLookup, 3);

            return this;
        }

        public SystemUserRecordPage SelectCountryofBirthNotKnown_no()
        {
            WaitForElement(countryofbirthnotknown_noOption);
            Click(countryofbirthnotknown_noOption);

            return this;
        }

        public SystemUserRecordPage SelectNotBornInUKButCountryUnknown_YesOption()
        {
            WaitForElement(notborninukbutcountryunknown_YesOption);
            Click(notborninukbutcountryunknown_YesOption);

            return this;
        }

        public SystemUserRecordPage SelectNotBornInUKButCountryUnknown_NoOption()
        {
            WaitForElement(notborninukbutcountryunknown_NoOption);
            Click(notborninukbutcountryunknown_NoOption);

            return this;
        }

        public SystemUserRecordPage ValidateYearOfEntryToUkFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(yearofentry_Field);
                ScrollToElement(yearofentry_Field);
            }

            else
                WaitForElementNotVisible(yearofentry_Field, 3);

            return this;
        }

        public SystemUserRecordPage ValidateYearOfEntryFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(yearOfEntry_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public SystemUserRecordPage ValidateYearOfEntryFieldValue(string ExpectedMessage)
        {
            WaitForElementVisible(yearofentry_Field);
            ScrollToElement(yearofentry_Field);
            ValidateElementValue(yearofentry_Field, ExpectedMessage);

            return this;
        }

        public SystemUserRecordPage ValidateDisabilityStatusPickListValues(string expected)
        {
            WaitForElement(disabilitystatusid_Field);
            ValidatePicklistContainsElementByText(disabilitystatusid_Field, expected);

            return this;
        }

        public SystemUserRecordPage ValidatePhoneAndEmailSectionFields()
        {
            WaitForElement(personalemail_Field);
            ValidateElementEnabled(personalemail_Field);
            WaitForElement(personalphonelandline_Field);
            ValidateElementEnabled(personalphonelandline_Field);
            WaitForElement(personalphonemobile_Field);
            ValidateElementEnabled(personalphonemobile_Field);

            return this;
        }

        public SystemUserRecordPage ClickAddressSearchButton(bool WaitForRefreshPanel = true)
        {
            WaitForElementToBeClickable(addressSearch_Button);
            Click(addressSearch_Button);

            if (WaitForRefreshPanel)
            {
                WaitForElementNotVisible("CWRefreshPanel", 7);
                System.Threading.Thread.Sleep(1000);
            }

            return this;

        }

        public SystemUserRecordPage ValidatePropertyNameFieldValue(string ExpectedText)
        {
            ValidateElementValueByJavascript(propertyname_FieldName, ExpectedText);

            return this;
        }

        public SystemUserRecordPage ValidatePropertyNoFieldValue(string ExpectedText)
        {
            ValidateElementValueByJavascript(propertyno_FieldName, ExpectedText);

            return this;
        }

        public SystemUserRecordPage ValidateStreetFieldValue(string ExpectedText)
        {
            ValidateElementValueByJavascript(street_FieldName, ExpectedText);

            return this;
        }

        public SystemUserRecordPage ValidateVillageDistrictFieldValue(string ExpectedText)
        {
            ValidateElementValueByJavascript(villageDistrict_FieldName, ExpectedText);

            return this;
        }

        public SystemUserRecordPage ValidateTownCityFieldValue(string ExpectedText)
        {
            ValidateElementValueByJavascript(townCity_FieldName, ExpectedText);

            return this;
        }

        public SystemUserRecordPage ValidatePostcodeFieldValue(string ExpectedText)
        {
            ValidateElementValueByJavascript(postcode_FieldName, ExpectedText);

            return this;
        }

        public SystemUserRecordPage ValidateCountyFieldValue(string ExpectedText)
        {
            ValidateElementValueByJavascript(county_FieldName, ExpectedText);

            return this;
        }

        public SystemUserRecordPage ValidateCountryFieldValue(string ExpectedText)
        {
            ValidateElementValueByJavascript(country_FieldName, ExpectedText);

            return this;
        }

        public SystemUserRecordPage ValidateSystemUserSectionHeaders(bool ExpectGeneralSectionVisible, bool ExpectedAddressSectionVisible, bool ExpectedAdditionalDemographicsSectionVisible, bool ExpectedPhoneEmailSectionVisible, bool ExpectedAccountSectionVisible, bool ExpectedEmploymentDetailsSectionVisible, bool ExpectedSettingSectionVisible, bool ExpectedLegacySystemDataSectionVisible)
        {
            bool generalSectionVisible = GetElementVisibility(GeneralSection_Header);
            bool addressSectionVisible = GetElementVisibility(AddressSection_Header);
            bool additionalDemographicsSectionVisible = GetElementVisibility(AdditionalDemographicsSection_Header);
            bool phoneEmailSectionVisible = GetElementVisibility(PhoneEmailSection_Header);
            bool accountSectionVisible = GetElementVisibility(AccountSection_Header);
            bool employmentDetailsSectionVisible = GetElementVisibility(EmploymentDetailsSection_Header);
            bool settingSectionVisible = GetElementVisibility(SettingSection_Header);
            bool legacySystemDataSectionVisible = GetElementVisibility(LegacySystemDataSection_Header);


            Assert.AreEqual(ExpectGeneralSectionVisible, generalSectionVisible);
            Assert.AreEqual(ExpectedAddressSectionVisible, addressSectionVisible);
            Assert.AreEqual(ExpectedAdditionalDemographicsSectionVisible, additionalDemographicsSectionVisible);
            Assert.AreEqual(ExpectedPhoneEmailSectionVisible, phoneEmailSectionVisible);
            Assert.AreEqual(ExpectedAccountSectionVisible, accountSectionVisible);
            Assert.AreEqual(ExpectedEmploymentDetailsSectionVisible, employmentDetailsSectionVisible);
            Assert.AreEqual(ExpectedSettingSectionVisible, settingSectionVisible);
            Assert.AreEqual(ExpectedLegacySystemDataSectionVisible, legacySystemDataSectionVisible);

            return this;
        }

        public SystemUserRecordPage ValidateGeneralSectionFieldHeaders(bool ExpectIdFieldVisible, bool ExpectedTitleFieldVisible, bool ExpectedFirstNameFieldVisible, bool ExpectedMiddleNameFieldVisible, bool ExpectedLastNameFieldVisible, bool ExpectedDateOfBirthFieldVisible, bool ExpectedStatedGenderFieldVisible, bool ExpectedProfilePhotoFieldVisible, bool ExpectedWorkInMultipleTeamsFieldVisible)
        {
            bool userNameFieldVisible = GetElementVisibility(username_Field);
            bool titleFieldVisible = GetElementVisibility(demographicstitleid_FieldLookup);
            bool firstNameFieldVisible = GetElementVisibility(FirstName_Field);
            bool middleNameFieldVisible = GetElementVisibility(middlename_Field);
            bool lastNameFieldVisible = GetElementVisibility(LastName_Field);
            bool dateOfBirthFieldVisible = GetElementVisibility(dateofbirth_Field);
            bool statedGenderFieldVisible = GetElementVisibility(persongenderid_Field);
            bool profilePhotoFieldVisible = GetElementVisibility(ProfilePhoto_Field);
            //bool canWorkOfflineFieldVisible = GetElementVisibility(canworkoffline_Field);
            bool workInMultipleTeamsFieldVisible = GetElementVisibility(workinmultipleteams_Field);


            Assert.AreEqual(ExpectIdFieldVisible, userNameFieldVisible);
            Assert.AreEqual(ExpectedTitleFieldVisible, titleFieldVisible);
            Assert.AreEqual(ExpectedFirstNameFieldVisible, firstNameFieldVisible);
            Assert.AreEqual(ExpectedMiddleNameFieldVisible, middleNameFieldVisible);
            Assert.AreEqual(ExpectedLastNameFieldVisible, lastNameFieldVisible);
            Assert.AreEqual(ExpectedDateOfBirthFieldVisible, dateOfBirthFieldVisible);
            Assert.AreEqual(ExpectedStatedGenderFieldVisible, statedGenderFieldVisible);
            Assert.AreEqual(ExpectedProfilePhotoFieldVisible, profilePhotoFieldVisible);
            //Assert.AreEqual(ExpectedCanWorkOfflineFieldVisible, canWorkOfflineFieldVisible);
            Assert.AreEqual(ExpectedWorkInMultipleTeamsFieldVisible, workInMultipleTeamsFieldVisible);

            return this;
        }

        public SystemUserRecordPage ValidateAddressSectionFieldHeaders(bool ExpectPropertyNoFieldVisible, bool ExpectedPropertyNameFieldVisible, bool ExpectedStreetFieldVisible, bool ExpectedVillagDistrictFieldVisible, bool ExpectedTownCityeFieldVisible, bool ExpectedPostcodeFieldVisible, bool ExpectedCountyFieldVisible, bool ExpectedCountryFieldVisible, bool ExpectedAddressTypeFieldVisible, bool ExpectedStartDateFieldVisible, bool AddressSearchbuttonVisible, bool ClearAddressbuttonVisible)
        {
            bool propertyNoFieldVisible = GetElementVisibility(propertyno_Field);
            bool propertyNameFieldVisible = GetElementVisibility(propertyname_Field);
            bool streetFieldVisible = GetElementVisibility(street_Field);
            bool VillageDistrictFieldVisible = GetElementVisibility(villageDistrict_Field);
            bool townCityFieldVisible = GetElementVisibility(townCity_Field);
            bool postCodeFieldVisible = GetElementVisibility(postcode_Field);
            bool countyFieldVisible = GetElementVisibility(county_Field);
            bool countryFieldVisible = GetElementVisibility(country_Field);
            bool addressTypeFieldVisible = GetElementVisibility(addressType_Field);
            bool startDateFieldVisible = GetElementVisibility(startDate_Field);
            bool addressSearchButtonVisible = GetElementVisibility(addressSearch_Button);
            bool clearAddressButtonVisible = GetElementVisibility(clearAddress_Button);



            Assert.AreEqual(ExpectPropertyNoFieldVisible, propertyNoFieldVisible);
            Assert.AreEqual(ExpectedPropertyNameFieldVisible, propertyNameFieldVisible);
            Assert.AreEqual(ExpectedStreetFieldVisible, streetFieldVisible);
            Assert.AreEqual(ExpectedVillagDistrictFieldVisible, VillageDistrictFieldVisible);
            Assert.AreEqual(ExpectedTownCityeFieldVisible, townCityFieldVisible);
            Assert.AreEqual(ExpectedPostcodeFieldVisible, postCodeFieldVisible);
            Assert.AreEqual(ExpectedCountyFieldVisible, countyFieldVisible);
            Assert.AreEqual(ExpectedCountryFieldVisible, countryFieldVisible);
            Assert.AreEqual(ExpectedAddressTypeFieldVisible, addressTypeFieldVisible);
            Assert.AreEqual(ExpectedStartDateFieldVisible, startDateFieldVisible);
            //Assert.AreEqual(AddressSearchbuttonVisible, addressSearchButtonVisible);
            Assert.AreEqual(ClearAddressbuttonVisible, clearAddressButtonVisible);

            return this;
        }

        public SystemUserRecordPage ValidateAdditionalDemographicsSectionFieldHeaders(bool ExpectedmaritalstatusFieldVisible, bool ExpectedreligionFieldVisible, bool ExpectedbrithishCitizenShipFieldVisible, bool ExpectedethinicityFieldVisible, bool ExpectednationalityFieldVisible, bool ExpectedcountryOfBirthFieldVisible,
            bool ExpectedyearOfEntryFieldVisible, bool ExpecteddisablityStatusFieldVisible, bool ExpectedtransportTypeFieldVisible, bool ExpectedcountryOfBirthNotKnownFieldVisible, bool ExpectednotBornInUKButCountryUnknownFieldVisible)
        {
            bool maritalstatusFieldVisible = GetElementVisibility(maritalstatusid_FieldHeader);
            bool religionFieldVisible = GetElementVisibility(religionid_FieldHeader);
            bool brithishCitizenShipFieldVisible = GetElementVisibility(britishcitizenshipid_FieldHeader);
            bool ethinicityFieldVisible = GetElementVisibility(ethnicityid_FieldHeader);
            bool nationalityFieldVisible = GetElementVisibility(nationalityid_FieldHeader);
            bool countryOfBirthFieldVisible = GetElementVisibility(countryofbirthid_FieldHeader);
            bool yearOfEntryFieldVisible = GetElementVisibility(yearofentry_FieldHeader);
            bool disablityStatusFieldVisible = GetElementVisibility(disabilitystatusid_FieldHeader);
            bool transportTypeFieldVisible = GetElementVisibility(alwaysavailabletransport_FieldHeader);
            bool countryOfBirthNotKnownFieldVisible = GetElementVisibility(countryOfBirthNotKnown_FieldHeader);
            bool notBornInUKButCountryUnknownFieldVisible = GetElementVisibility(notborninukbutcountryunknown_FieldHeader);



            Assert.AreEqual(ExpectedmaritalstatusFieldVisible, maritalstatusFieldVisible);
            Assert.AreEqual(ExpectedreligionFieldVisible, religionFieldVisible);
            Assert.AreEqual(ExpectedbrithishCitizenShipFieldVisible, brithishCitizenShipFieldVisible);
            Assert.AreEqual(ExpectedethinicityFieldVisible, ethinicityFieldVisible);
            Assert.AreEqual(ExpectednationalityFieldVisible, nationalityFieldVisible);
            Assert.AreEqual(ExpectedcountryOfBirthFieldVisible, countryOfBirthFieldVisible);
            Assert.AreEqual(ExpectedyearOfEntryFieldVisible, yearOfEntryFieldVisible);
            Assert.AreEqual(ExpecteddisablityStatusFieldVisible, disablityStatusFieldVisible);
            Assert.AreEqual(ExpectedtransportTypeFieldVisible, transportTypeFieldVisible);
            Assert.AreEqual(ExpectedcountryOfBirthNotKnownFieldVisible, countryOfBirthNotKnownFieldVisible);
            Assert.AreEqual(ExpectednotBornInUKButCountryUnknownFieldVisible, notBornInUKButCountryUnknownFieldVisible);

            return this;
        }

        public SystemUserRecordPage ValidatePhoneEmailSectionFieldHeaders(bool ExpectedworkEmailFieldVisible, bool ExpectedsecureemailaddressFieldVisible, bool ExpectedpersonalemailFieldVisible, bool ExpectedworkphonelandlineFieldVisible,
            bool ExpectedworkphonemobileFieldVisible, bool ExpectedpersonalphonelandlineFieldVisible, bool ExpectedpersonalphonemobileFieldVisible)
        {
            bool workEmailFieldVisible = GetElementVisibility(workEmail_FieldHeader);
            bool secureemailaddressFieldVisible = GetElementVisibility(secureemailaddress_FieldHeader);
            bool personalemailFieldVisible = GetElementVisibility(personalemail_FieldHeader);
            bool workphonelandlineFieldVisible = GetElementVisibility(workphonelandline_FieldHeader);
            bool workphonemobileFieldVisible = GetElementVisibility(workphonemobile_FieldHeader);
            bool personalphonelandlineFieldVisible = GetElementVisibility(personalphonelandline_FieldHeader);
            bool personalphonemobileFieldVisible = GetElementVisibility(personalphonemobile_FieldHeader);


            Assert.AreEqual(ExpectedworkEmailFieldVisible, workEmailFieldVisible);
            Assert.AreEqual(ExpectedsecureemailaddressFieldVisible, secureemailaddressFieldVisible);
            Assert.AreEqual(ExpectedpersonalemailFieldVisible, personalemailFieldVisible);
            Assert.AreEqual(ExpectedworkphonelandlineFieldVisible, workphonelandlineFieldVisible);
            Assert.AreEqual(ExpectedworkphonemobileFieldVisible, workphonemobileFieldVisible);
            Assert.AreEqual(ExpectedpersonalphonelandlineFieldVisible, personalphonelandlineFieldVisible);
            Assert.AreEqual(ExpectedpersonalphonemobileFieldVisible, personalphonemobileFieldVisible);

            return this;
        }

        public SystemUserRecordPage ValidateAccountSectionFieldHeaders(bool ExpectedowningBusinessUnitFieldVisible, bool ExpecteddefaultTeamFieldVisible, bool ExpectedauthenticationProviderFieldVisible,
            bool ExpecteduserNameFieldVisible, bool ExpectedloggingLevelFieldVisible, bool ExpectedisAccountLockedFieldVisible,
           bool ExpectedaccountLockedOutDateFieldVisible, bool ExpectedfailedPasswordAttementCountFieldVisible, bool ExpectedlastFailedPasswordAttemptDateFieldVisible, bool ExpectedinactiveFieldVisible)
        {
            bool owningBusinessUnitFieldVisible = GetElementVisibility(owningbusinessunitid_FieldHeader);
            bool defaultTeamFieldVisible = GetElementVisibility(defaultLookup_FieldHeader);
            bool authenticationProviderFieldVisible = GetElementVisibility(authenticationproviderid_FieldHeader);
            bool userNameFieldVisible = GetElementVisibility(username_FieldHeader);
            bool loggingLevelFieldVisible = GetElementVisibility(loggingLevel_FieldHeader);
            bool isAccountLockedFieldVisible = GetElementVisibility(isaccountlocked_FieldHeader);
            bool accountLockedOutDateFieldVisible = GetElementVisibility(accountlockedoutdate_FieldHeader);
            bool failedPasswordAttementCountFieldVisible = GetElementVisibility(failedpasswordattemptcount_FieldHeader);
            bool lastFailedPasswordAttemptDateFieldVisible = GetElementVisibility(lastfailedpasswordattemptdate_FieldHeader);
            bool inactiveFieldVisible = GetElementVisibility(inactive_FieldHeader);


            Assert.AreEqual(ExpectedowningBusinessUnitFieldVisible, owningBusinessUnitFieldVisible);
            Assert.AreEqual(ExpecteddefaultTeamFieldVisible, defaultTeamFieldVisible);
            Assert.AreEqual(ExpectedauthenticationProviderFieldVisible, authenticationProviderFieldVisible);
            Assert.AreEqual(ExpecteduserNameFieldVisible, userNameFieldVisible);
            Assert.AreEqual(ExpectedloggingLevelFieldVisible, loggingLevelFieldVisible);
            Assert.AreEqual(ExpectedisAccountLockedFieldVisible, isAccountLockedFieldVisible);
            Assert.AreEqual(ExpectedaccountLockedOutDateFieldVisible, accountLockedOutDateFieldVisible);
            Assert.AreEqual(ExpectedfailedPasswordAttementCountFieldVisible, failedPasswordAttementCountFieldVisible);
            Assert.AreEqual(ExpectedlastFailedPasswordAttemptDateFieldVisible, lastFailedPasswordAttemptDateFieldVisible);
            Assert.AreEqual(ExpectedinactiveFieldVisible, inactiveFieldVisible);

            return this;
        }

        public SystemUserRecordPage ValidateEmploymentDetailsSectionFieldHeaders(bool ExpectedprofessiontypeidFieldVisible, bool ExpectedEmploymentStatusFieldVisible, bool ExpectedprofessionalRegistrationNumberFieldVisible,
           bool ExpectedjobTitleFieldVisible, bool ExpectedNINumberFieldVisible)
        {
            bool professiontypeidFieldVisible = GetElementVisibility(professiontypeid_FieldHeader);
            bool EmploymentStatusFieldVisible = GetElementVisibility(EmploymentStatus_FieldHeader);
            bool professionalRegistrationNumberFieldVisible = GetElementVisibility(professionalregistrationnumber_FieldHeader);
            bool jobTitleFieldVisible = GetElementVisibility(jobtitle_FieldHeader);
            bool NINumberFieldVisible = GetElementVisibility(NINumber_FieldHeader);


            Assert.AreEqual(ExpectedprofessiontypeidFieldVisible, professiontypeidFieldVisible);
            Assert.AreEqual(ExpectedEmploymentStatusFieldVisible, EmploymentStatusFieldVisible);
            Assert.AreEqual(ExpectedprofessionalRegistrationNumberFieldVisible, professionalRegistrationNumberFieldVisible);
            Assert.AreEqual(ExpectedjobTitleFieldVisible, jobTitleFieldVisible);
            Assert.AreEqual(ExpectedNINumberFieldVisible, NINumberFieldVisible);


            return this;
        }

        public SystemUserRecordPage ValidateSettingsSectionFieldHeaders(bool ExpectedrecordsPerPageFieldVisible, bool ExpectedsystemLanguageFieldVisible, bool ExpectedtimeZoneFieldVisible,
            bool ExpectedcanAuthoriseFormFieldVisible, bool ExpectedcanAuthorisedBehalfOfOthersFieldVisible, bool ExpecteddisableformautosaveFieldVisible,
            bool ExpectedcancompletednarFieldVisible, bool ExpectedcanauthorisednarFieldVisible, bool ExpectedismanagerFieldVisible)
        {
            bool recordsPerPageFieldVisible = GetElementVisibility(recordsPerPage_FieldHeader);
            bool systemLanguageFieldVisible = GetElementVisibility(systemLanguage_FieldHeader);
            bool timeZoneFieldVisible = GetElementVisibility(timeZone_FieldHeader);
            bool canAuthoriseFormFieldVisible = GetElementVisibility(canAuthoriseForm_FieldHeader);
            bool canAuthorisedBehalfOfOthersFieldVisible = GetElementVisibility(canAuthorisedBehalfOfOthers_FieldHeader);
            bool disableformautosaveFieldVisible = GetElementVisibility(disableformautosave_FieldHeader);
            bool cancompletednarFieldVisible = GetElementVisibility(cancompletednar_FieldHeader);
            bool canauthorisednarFieldVisible = GetElementVisibility(canauthorisednar_FieldHeader);
            bool ismanagerFieldVisible = GetElementVisibility(ismanager_FieldHeader);


            Assert.AreEqual(ExpectedrecordsPerPageFieldVisible, recordsPerPageFieldVisible);
            Assert.AreEqual(ExpectedsystemLanguageFieldVisible, systemLanguageFieldVisible);
            Assert.AreEqual(ExpectedtimeZoneFieldVisible, timeZoneFieldVisible);
            Assert.AreEqual(ExpectedcanAuthoriseFormFieldVisible, canAuthoriseFormFieldVisible);
            Assert.AreEqual(ExpectedcanAuthorisedBehalfOfOthersFieldVisible, canAuthorisedBehalfOfOthersFieldVisible);
            Assert.AreEqual(ExpecteddisableformautosaveFieldVisible, disableformautosaveFieldVisible);
            Assert.AreEqual(ExpectedcancompletednarFieldVisible, cancompletednarFieldVisible);
            Assert.AreEqual(ExpectedcanauthorisednarFieldVisible, canauthorisednarFieldVisible);
            Assert.AreEqual(ExpectedismanagerFieldVisible, ismanagerFieldVisible);

            return this;
        }

        public SystemUserRecordPage ValidateLegacySystemDataSectionFieldHeaders(bool ExpectedcommentFieldVisible)
        {
            bool commentFieldVisible = GetElementVisibility(comment_FieldHeader);

            Assert.AreEqual(ExpectedcommentFieldVisible, commentFieldVisible);

            return this;
        }

        public SystemUserRecordPage ValidateEmploymentDetailsSectionTitleVisible(bool ExpectedVisible, string ExpectedSectionTitleText)
        {
            if (ExpectedVisible)
            {
                WaitForElement(EmploymentDetailsSection_Header);
                ValidateElementText(EmploymentDetailsSection_Header, ExpectedSectionTitleText);
            }
            else
            {
                WaitForElementNotVisible(EmploymentDetailsSection_Header, 7);
            }

            return this;
        }

        public SystemUserRecordPage ValidateProfessionTypeVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElement(professiontypeid_FieldLookup);
                ScrollToElement(professiontypeid_FieldLookup);
                WaitForElementVisible(professiontypeid_FieldLookup);
            }
            else
            {
                WaitForElementNotVisible(professiontypeid_FieldLookup, 7);
            }
            return this;
        }

        public SystemUserRecordPage ValidateEmploymentStatusVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElement(EmploymentStatus_Picklist);
                ScrollToElement(EmploymentStatus_Picklist);
                WaitForElementVisible(EmploymentStatus_Picklist);
            }
            else
            {
                WaitForElementNotVisible(EmploymentStatus_Picklist, 7);
            }
            return this;
        }

        public SystemUserRecordPage ValidateProfessionalRegistrationNumberVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElement(professionalregistrationnumber_Field);
                ScrollToElement(professionalregistrationnumber_Field);
                WaitForElementVisible(professionalregistrationnumber_Field);
            }
            else
            {
                WaitForElementNotVisible(professionalregistrationnumber_Field, 7);
            }
            return this;
        }

        public SystemUserRecordPage ValidateJobTitleVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElement(jobtitle_Field);
                ScrollToElement(jobtitle_Field);
                WaitForElementVisible(jobtitle_Field);
            }
            else
            {
                WaitForElementNotVisible(jobtitle_Field, 7);
            }
            return this;
        }

        public SystemUserRecordPage ValidateAvailableFromVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElement(availableFrom_field);
                ScrollToElement(availableFrom_field);
                WaitForElementVisible(availableFrom_field);
            }
            else
            {
                WaitForElementNotVisible(availableFrom_field, 7);
            }
            return this;
        }

        public SystemUserRecordPage ValidateCommentFieldDisabled(bool ExpectedDisable)
        {
            if (ExpectedDisable)
                ValidateElementDisabled(comment_Field);
            else
                ValidateElementEnabled(comment_Field);

            return this;
        }

        public SystemUserRecordPage ValidateNotifactionErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(notificationMessageArea, ExpectedMessage);

            return this;
        }

        public SystemUserRecordPage ValidateProfilePhotoFileType(string expected)
        {
            ValidateElementFileType(ProfilePhotoid_Field, expected);

            return this;
        }

        public SystemUserRecordPage ValidatePasswordFieldHeaderVisible(bool ExpectedPasswordFieldVisible)
        {
            bool PasswordFieldVisible = GetElementVisibility(password_FieldHeader);

            Assert.AreEqual(ExpectedPasswordFieldVisible, PasswordFieldVisible);

            return this;
        }

        public SystemUserRecordPage ValidatePasswordFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(password_FieldErrorArea, ExpectedMessage);

            return this;
        }

        public SystemUserRecordPage ValidateActionToolBarOptionsVisiblity(bool ExpectedSaveButtonVisible, bool ExpectedSaveAndCloseButtonVisible, bool ExpectedCopyRecordLinkButton)
        {
            bool saveButtonVisible = GetElementVisibility(saveButton);
            bool saveAndCloseButtonVisible = GetElementVisibility(saveAndCloseButton);
            bool copyRecordLinkButtonVisible = GetElementVisibility(CopyRecordLinkButton);

            Assert.AreEqual(ExpectedSaveButtonVisible, saveButtonVisible);
            Assert.AreEqual(ExpectedSaveAndCloseButtonVisible, saveAndCloseButtonVisible);
            Assert.AreEqual(ExpectedCopyRecordLinkButton, copyRecordLinkButtonVisible);

            return this;
        }

        public SystemUserRecordPage ValidateCommentFieldText(string ExpectedMessage)
        {


            WaitForElement(comment_Field);
            ValidateElementTextContainsText(comment_Field, ExpectedMessage);

            return this;
        }

        public SystemUserRecordPage NavigateToSecurityProfilePage()
        {

            WaitForElement(SecurityProfilesButton);
            Click(SecurityProfilesButton);

            return this;
        }

        public SystemUserRecordPage NavigateToDiaryPage()
        {

            WaitForElement(diary);
            Click(diary);

            return this;
        }

        public SystemUserRecordPage ValidateCommentFieldTextWithoutSecurityProfile(string ExpectedMessage)
        {


            WaitForElement(comment_FiledWithoutSecurityProfile);
            ValidateElementText(comment_FiledWithoutSecurityProfile, ExpectedMessage);

            return this;
        }

        public SystemUserRecordPage ValidateCommentFieldMaximumLimitText(string expected)
        {
            ValidateElementMaxLength(comment_Field, expected);

            return this;
        }

        public SystemUserRecordPage ValidateChangePasswordLink(string ExpectedText)
        {

            WaitForElementVisible(changePassword_Link);
            ValidateElementText(changePassword_Link, ExpectedText);


            return this;
        }

        public SystemUserRecordPage ClickChangePasswordLink()
        {

            WaitForElementVisible(changePassword_Link);
            Click(changePassword_Link);


            return this;
        }

        public SystemUserRecordPage NavigateToSuspensionsSubPage()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            WaitForElement(SuspensionsLeftSubMenuItem);
            Click(SuspensionsLeftSubMenuItem);

            return this;
        }

        public SystemUserRecordPage ValidateFailedPasswordAttemptCountFieldValue(string ExpectedValue)
        {
            ValidateElementValue(failedpasswordattemptcount_Field, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateLastFailedPasswordAttemptDateFieldValue(string ExpectedValue)
        {
            ValidateElementValue(lastfailedpasswordattemptdate_Field, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateAccountLockedOutDateFieldValue(string ExpectedValue)
        {
            ValidateElementValue(accountlockedoutdate_Field, ExpectedValue);

            return this;
        }

        public SystemUserRecordPage ValidateIsAccountLock_YesOptionChecked(bool ExpectedEnabled)
        {
            if (ExpectedEnabled)
                ValidateElementChecked(isaccountlocked_YesOption);
            else
                ValidateElementNotChecked(isaccountlocked_YesOption);

            return this;
        }

        public SystemUserRecordPage ClickIsAccountLocked_NoOption()
        {

            WaitForElementVisible(isaccountlocked_NoOption);
            Click(isaccountlocked_NoOption);


            return this;
        }

        public SystemUserRecordPage ClickAllowToUseGdpr_YesOption()
        {
            ScrollToElement(allowUseGdprData_YesOption);
            WaitForElementToBeClickable(allowUseGdprData_YesOption);
            Click(allowUseGdprData_YesOption);
            return this;
        }

        public SystemUserRecordPage ClickAllowToUseGdpr_NoOption()
        {
            ScrollToElement(allowUseGdprData_NoOption);
            WaitForElementToBeClickable(allowUseGdprData_NoOption);
            Click(allowUseGdprData_NoOption);
            return this;
        }

        public SystemUserRecordPage NavigateToWorkScheduleSubPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(workScheduleLeftSubMenuItem);
            Click(workScheduleLeftSubMenuItem);

            return this;
        }

        public SystemUserRecordPage NavigateToDetailsPage()
        {
            WaitForElement(detailsSection);
            Click(detailsSection);

            return this;
        }

        public SystemUserRecordPage SelectEmployeeTypes(string ValueToSelect)
        {
            WaitForElement(EmployeeType_Field);
            SelectPicklistElementByText(EmployeeType_Field, ValueToSelect);

            return this;
        }

        public SystemUserRecordPage ValidateRecordInaccessibleNotificationMessage(string ExpectedMessage)
        {
            WaitForElementVisible(recordNotAccessible_notificationMessage);

            ValidateElementText(recordNotAccessible_notificationMessage, ExpectedMessage);

            return this;
        }

        public SystemUserRecordPage ValidateUnauthorizedAccessNotificationMessage(string ExpectedMessage)
        {
            WaitForElementVisible(unauthorizedAccess_notificationMessage);

            ValidateElementText(unauthorizedAccess_notificationMessage, ExpectedMessage);

            return this;
        }

        public SystemUserRecordPage ValidateSystemUserRecordTitle(string username)
        {
            WaitForElementVisible(systemUserTitle);
            ValidateElementText(systemUserTitle, username);

            return this;
        }

        public SystemUserRecordPage ValidateSelectedEmployeeTypeValue(String expectedtext)
        {
            ValidatePicklistSelectedText(EmployeeType_Field, expectedtext);
            return this;
        }

        public SystemUserRecordPage ValidateOptedOutOfWorkingTimeDirective(string ExpectedText)
        {
            WaitForElement(optedOutOfWorkingTimeDirective_PicklistField);
            ScrollToElement(optedOutOfWorkingTimeDirective_PicklistField);
            ValidatePicklistSelectedText(optedOutOfWorkingTimeDirective_PicklistField, ExpectedText);
            return this;
        }

        public SystemUserRecordPage ValidateAllowToUseGdprFieldIsVisible(bool ExpectedVisible)
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

        public SystemUserRecordPage ValidateAllowToUseGdprField_YesOptionSelected()
        {
            ScrollToElement(allowUseGdprDataField_label);
            ScrollToElement(allowUseGdprData_YesOption);
            ValidateElementChecked(allowUseGdprData_YesOption);
            ValidateElementNotChecked(allowUseGdprData_NoOption);
            return this;
        }

        public SystemUserRecordPage ValidateAllowToUseGdprField_NoOptionSelected()
        {
            ScrollToElement(allowUseGdprDataField_label);
            ScrollToElement(allowUseGdprData_NoOption);
            ValidateElementChecked(allowUseGdprData_NoOption);
            ValidateElementNotChecked(allowUseGdprData_YesOption);
            return this;
        }

        public SystemUserRecordPage ValidateDateOfBirthFieldValue(string expectedText)
        {
            WaitForElementVisible(dateofbirth_Field);
            ScrollToElement(dateofbirth_Field);
            Assert.AreEqual(expectedText, GetElementValue(dateofbirth_Field));
            return this;
        }

        public SystemUserRecordPage ValidatePesonalEmailFieldValue(string expectedText)
        {
            WaitForElementVisible(personalemail_Field);
            ScrollToElement(personalemail_Field);
            Assert.AreEqual(expectedText, GetElementValue(personalemail_Field));
            return this;
        }

        public SystemUserRecordPage SelectPets_Option(string TextToSelect)
        {
            WaitForElement(Pets_Picklist);
            SelectPicklistElementByText(Pets_Picklist, TextToSelect);

            return this;
        }

        public SystemUserRecordPage SelectSmoker_Option(string TextToSelect)
        {
            WaitForElement(Smoker_Picklist);
            SelectPicklistElementByText(Smoker_Picklist, TextToSelect);

            return this;
        }

        public SystemUserRecordPage ValidatePronounsLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(pronounsField_LookupButton);
            else
                WaitForElementNotVisible(pronounsField_LookupButton, 3);

            return this;
        }


        public SystemUserRecordPage NavigateToEmploymentMenuAndValidateSubMenus()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(employmentDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElement(employmentLeftSubMenu);
                Click(employmentLeftSubMenu);
            }

            WaitForElementVisible(absencesLeftSubMenuItem);
            WaitForElementVisible(employmentContractsLeftSubMenuItem);
            WaitForElementVisible(myStaffReviewsLeftSubMenuItems);
            WaitForElementVisible(payArrangementsLeftSubMenuItems);
            WaitForElementVisible(SuspensionsLeftSubMenuItem);
            WaitForElementVisible(trainingLeftSubMenuItem);
            WaitForElementVisible(workersIdsLeftSubMenuItems);

            WaitForElement(MenuButton);
            Click(MenuButton);

            return this;
        }

        string currentWindow;
        public SystemUserRecordPage SwitchToNewTab(string WindowTitleText)
        {
            System.Threading.Thread.Sleep(1500);

            List<string> allWindows = GetAllWindowIdentifier();

            foreach (string winHandle in allWindows)
            {
                string currentWindowTitle = GetCurrentWindowTitle(winHandle);
                if (currentWindowTitle.Equals(WindowTitleText))
                {
                    break;
                }
                else
                {
                    SwitchToWindow(winHandle);
                }
            }

            return this;
        }

        public SystemUserRecordPage SwitchToPreviousTab()
        {
            currentWindow = GetCurrentWindowIdentifier();
            driver.Close();
            string popupWindow = GetAllWindowIdentifier().Where(c => c != currentWindow).FirstOrDefault();
            SwitchToWindow(popupWindow);

            return this;
        }
    }

}
