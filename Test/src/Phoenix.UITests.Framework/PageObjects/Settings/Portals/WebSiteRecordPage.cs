using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteRecordPage : CommonMethods
    {

        public WebSiteRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=website&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By cloneWebsiteButton = By.Id("TI_CloneWebsiteButton");



        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By WebsitePages_Link = By.Id("CWNavItem_WebsitePages");
        readonly By WebsiteSettings_Link = By.Id("CWNavItem_WebsiteSetting");
        readonly By WebsiteAnnouncement_Link = By.Id("CWNavItem_WebsiteAnnouncement");
        readonly By WebsiteUsers_Link = By.Id("CWNavItem_WebsiteUser");
        readonly By WebsiteUserAccessAudit_Link = By.Id("CWNavItem_WebsiteUserAccessAudit");
        readonly By WebsiteSitemap_Link = By.Id("CWNavItem_WebsiteSitemap");
        readonly By WebsitePointsOfContact_Link = By.Id("CWNavItem_WebsitePointsOfContact");
        readonly By WebsiteFeedback_Link = By.Id("CWNavItem_WebsiteFeedback");
        readonly By WebsiteContact_Link = By.Id("CWNavItem_WebsiteContact");
        readonly By WebsitePortalTasks_Link = By.Id("CWNavItem_PortalTasks");


        #region Field Labels

        #region General

        readonly By Name_FieldName = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
        readonly By HomePage_FieldName = By.XPath("//*[@id='CWLabelHolder_homepage']/label[text()='Home Page']");
        readonly By MemberHomePage_FieldName = By.XPath("//*[@id='CWLabelHolder_memberhomepage']/label[text()='Member Home Page']");
        readonly By Logo_FieldName = By.XPath("//*[@id='CWLabelHolder_logo']/label[text()='Logo']");
        readonly By Application_FieldName = By.XPath("//*[@id='CWLabelHolder_applicationid']/label[text()='Application']");
        readonly By UserRecordType_FieldName = By.XPath("//*[@id='CWLabelHolder_userrecordtypeid']/label[text()='User Record Type']");

        #endregion

        #region Administration

        readonly By WebSiteURL_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteurl']/label[text()='Website Url']");
        readonly By EmailVerificationRequired_FieldName = By.XPath("//*[@id='CWLabelHolder_emailverificationrequired']/label[text()='Email Verification Required']");
        readonly By UserApprovalRequired_FieldName = By.XPath("//*[@id='CWLabelHolder_userapprovalrequired']/label[text()='User Approval Required']");

        #endregion

        #region Password Complexity

        readonly By MinimumPasswordLength_FieldName = By.XPath("//*[@id='CWLabelHolder_minimumpasswordlength']/label[text()='Minimum Password Length']");
        readonly By MinimumNumericCharacters_FieldName = By.XPath("//*[@id='CWLabelHolder_minimumnumericcharacters']/label[text()='Minimum Numeric Characters']");
        readonly By MinimumSpecialCharacters_FieldName = By.XPath("//*[@id='CWLabelHolder_minimumspecialcharacters']/label[text()='Minimum Special Characters']");
        readonly By MinimumUppercaseLetters_FieldName = By.XPath("//*[@id='CWLabelHolder_minimumuppercaseletters']/label[text()='Minimum Uppercase Letters']");

        #endregion

        #region Password Policy

        readonly By MaximumPasswordAgeDays_FieldName = By.XPath("//*[@id='CWLabelHolder_maximumpasswordage']/label[text()='Maximum Password Age (Days)']");
        readonly By MinimumpasswordAgeDays_FieldName = By.XPath("//*[@id='CWLabelHolder_minimumpasswordage']/label[text()='Minimum password age (Days)']");
        readonly By EnforcePasswordHistory_FieldName = By.XPath("//*[@id='CWLabelHolder_enforcepasswordhistory']/label[text()='Enforce Password History']");

        #endregion

        #region Account Lockout Policy

        readonly By EnableAccountLocking_FieldName = By.XPath("//*[@id='CWLabelHolder_enableaccountlocking']/label[text()='Enable Account Locking']");
        readonly By AccountLockoutDuration_FieldName = By.XPath("//*[@id='CWLabelHolder_accountlockoutduration']/label[text()='Account Lockout Duration']");
        readonly By AccountLockoutThreshold_FieldName = By.XPath("//*[@id='CWLabelHolder_accountlockoutthreshold']/label[text()='Account Lockout Threshold']");
        readonly By ResetAccountLockoutCounterAfter_FieldName = By.XPath("//*[@id='CWLabelHolder_resetaccountlockoutcounterafter']/label[text()='Reset Account Lockout Counter After']");

        #endregion

        #region Two Factor Authentication

        readonly By EnableTwoFactorAuthentication_FieldName = By.XPath("//*[@id='CWLabelHolder_enabletwofactorauthentication']/label[text()='Enable Two Factor Authentication']");
        readonly By DefaultPINReceivingMethod_FieldName = By.XPath("//*[@id='CWLabelHolder_defaultpinreceivingmethodid']/label[text()='Default PIN Receiving Method']");
        readonly By PINExpireInMinutes_FieldName = By.XPath("//*[@id='CWLabelHolder_pinexpirein']/label[text()='PIN Expire In (Minutes)']");
        readonly By NumberOfPINDigits_FieldName = By.XPath("//*[@id='CWLabelHolder_numberofpindigits']/label[text()='Number of PIN Digits']");

        #endregion

        #endregion

        #region Fields


        #region General

        readonly By Name_Field = By.XPath("//*[@id='CWField_name']");
        readonly By Name_ErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");

        readonly By DisplayName_Field = By.XPath("//*[@id='CWField_displayname']");
        readonly By DisplayName_ErrorLabel = By.XPath("//*[@id='CWControlHolder_displayname']/label/span");

        readonly By HomePage_Field = By.XPath("//*[@id='CWField_homepage']");
        readonly By MemberHomePage_Field = By.XPath("//*[@id='CWField_memberhomepage']");
        readonly By Footer_Field = By.XPath("//*[@id='CWField_footer']");

        readonly By Logo_EditButton = By.XPath("//*[@id='CWField_logo_Upload']");
        readonly By Logo_Browse = By.XPath("//*[@id='CWField_logo']");
        readonly By Logo_UploadButton = By.XPath("//*[@id='CWField_logo_UploadButton']");

        readonly By Application_FieldLink = By.Id("CWField_applicationid_Link");
        readonly By Application_RemoveButton = By.Id("CWClearLookup_applicationid");
        readonly By Application_LookupButton = By.Id("CWLookupBtn_applicationid");
        readonly By Application_ErrorLabel = By.XPath("//*[@id='CWControlHolder_applicationid']/label/span");

        readonly By UserRecordType_FieldLink = By.Id("CWField_userrecordtypeid_Link");
        readonly By UserRecordType_RemoveButton = By.Id("CWClearLookup_userrecordtypeid");
        readonly By UserRecordType_LookupButton = By.Id("CWLookupBtn_userrecordtypeid");
        readonly By UserRecordType_ErrorLabel = By.XPath("//*[@id='CWControlHolder_userrecordtypeid']/label/span");

        readonly By RegistrationFormName_Field = By.XPath("//*[@id='CWField_registrationdataformname']");
        readonly By RecordsPerPage_Field = By.XPath("//*[@id='CWField_recordsperpage']");
        readonly By SessionTimeoutInMinutes_Field = By.XPath("//*[@id='CWField_sessiontimeout']");

        readonly By Version_Field = By.XPath("//*[@id='CWField_version']");
        readonly By Version_ErrorLabel = By.XPath("//*[@id='CWControlHolder_version']/label/span");

        readonly By Type_Picklist = By.XPath("//*[@id='CWField_portaltypeid']");

        #endregion

        #region Administration

        readonly By WebSiteURL_Field = By.Id("CWField_websiteurl");
        readonly By WebSiteURL_ErrorLabel = By.XPath("//*[@id='CWControlHolder_websiteurl']/div/div/label/span");
        readonly By WebSiteURL_Link = By.Id("CWField_websiteurl_Link");
        readonly By EmailVerificationRequiredYesOption_RadioButton = By.Id("CWField_emailverificationrequired_1");
        readonly By EmailVerificationRequiredNoOption_RadioButton = By.Id("CWField_emailverificationrequired_0");
        readonly By UserApprovalRequiredYesOption_RadioButton = By.Id("CWField_userapprovalrequired_1");
        readonly By UserApprovalRequiredNoOption_RadioButton = By.Id("CWField_userapprovalrequired_0");
        readonly By AuthenticationType_Picklist = By.Id("CWField_authenticationtypeid");

        #endregion

        #region Password Complexity

        readonly By MinimumPasswordLength_Field = By.Id("CWField_minimumpasswordlength");
        readonly By MinimumPasswordLength_ErrorLabel = By.XPath("//*[@id='CWControlHolder_minimumpasswordlength']/label/span");
        readonly By MinimumNumericCharacters_Field = By.Id("CWField_minimumnumericcharacters");
        readonly By MinimumSpecialCharacters_Field = By.Id("CWField_minimumspecialcharacters");
        readonly By MinimumUppercaseLetters_Field = By.Id("CWField_minimumuppercaseletters");

        #endregion

        #region Password Policy

        readonly By MaximumPasswordAgeDays_Field = By.Id("CWField_maximumpasswordage");
        readonly By MinimumpasswordAgeDays_Field = By.Id("CWField_minimumpasswordage");
        readonly By MinimumpasswordAgeDays_ErrorLabel = By.XPath("//*[@id='CWControlHolder_minimumpasswordage']/label/span");
        readonly By EnforcePasswordHistory_Field = By.Id("CWField_enforcepasswordhistory");
        readonly By EnforcePasswordHistory_ErrorLabel = By.XPath("//*[@id='CWControlHolder_enforcepasswordhistory']/label/span");
        readonly By PasswordResetExpireInMinutes_Field = By.Id("CWField_passwordresetexpirein");

        #endregion

        #region Account Lockout Policy

        readonly By EnableAccountLockingYesOption_RadioButton = By.Id("CWField_enableaccountlocking_1");
        readonly By EnableAccountLockingNoOption_RadioButton = By.Id("CWField_enableaccountlocking_0");
        readonly By AccountLockoutDuration_Field = By.Id("CWField_accountlockoutduration");
        readonly By AccountLockoutThreshold_Field = By.Id("CWField_accountlockoutthreshold");
        readonly By AccountLockoutThreshold_ErrorLabel = By.XPath("//*[@id='CWControlHolder_accountlockoutthreshold']/label/span");
        readonly By ResetAccountLockoutCounterAfter_Field = By.Id("CWField_resetaccountlockoutcounterafter");

        #endregion

        #region Two Factor Authentication

        readonly By EnableTwoFactorAuthenticationYesOption_RadioButton = By.Id("CWField_enabletwofactorauthentication_1");
        readonly By EnableTwoFactorAuthenticationNoOption_RadioButton = By.Id("CWField_enabletwofactorauthentication_0");
        readonly By DefaultPINReceivingMethod_Picklist = By.Id("CWField_defaultpinreceivingmethodid");
        readonly By PINExpireInMinutes_Field = By.Id("CWField_pinexpirein");
        readonly By NumberOfPINDigits_Field = By.Id("CWField_numberofpindigits");
        readonly By NumberOfPINDigits_ErrorLabel = By.XPath("//*[@id='CWControlHolder_numberofpindigits']/label/span");
        readonly By MaxInvalidPinAttemptAllowed_Field = By.Id("CWField_maxinvalidpinattemptallowed");

        #endregion


        #endregion



        By HomePage_AutoCompleteDiv(string ElementText) => By.XPath("//*[@id='ui-id-3']/li/div[text()='" + ElementText + "']");
        By MemberHomePage_AutoCompleteDiv(string ElementText) => By.XPath("//*[@id='ui-id-4']/li/div[text()='" + ElementText + "']");
        By Header_AutoCompleteDiv(string ElementText) => By.XPath("//*[@id='ui-id-5']/li/div[text()='" + ElementText + "']");
        By Footer_AutoCompleteDiv(string ElementText) => By.XPath("//*[@id='ui-id-5']/li/div[text()='" + ElementText + "']");




        public WebSiteRecordPage WaitForWebSiteRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            this.WaitForElement(Name_FieldName);
            this.WaitForElement(HomePage_FieldName);
            this.WaitForElement(MemberHomePage_FieldName);
            this.WaitForElement(Logo_FieldName);
            this.WaitForElement(Application_FieldName);
            this.WaitForElement(UserRecordType_FieldName);
            this.WaitForElement(WebSiteURL_FieldName);
            this.WaitForElement(EmailVerificationRequired_FieldName);
            this.WaitForElement(UserApprovalRequired_FieldName);
            this.WaitForElement(MinimumPasswordLength_FieldName);
            this.WaitForElement(MinimumNumericCharacters_FieldName);
            this.WaitForElement(MinimumSpecialCharacters_FieldName);
            this.WaitForElement(MinimumUppercaseLetters_FieldName);
            this.WaitForElement(MaximumPasswordAgeDays_FieldName);
            this.WaitForElement(MinimumpasswordAgeDays_FieldName);
            this.WaitForElement(EnforcePasswordHistory_FieldName);
            this.WaitForElement(PasswordResetExpireInMinutes_Field);
            this.WaitForElement(EnableAccountLocking_FieldName);
            this.WaitForElement(AccountLockoutDuration_FieldName);
            this.WaitForElement(AccountLockoutThreshold_FieldName);
            this.WaitForElement(ResetAccountLockoutCounterAfter_FieldName);
            this.WaitForElement(EnableTwoFactorAuthentication_FieldName);
            this.WaitForElement(DefaultPINReceivingMethod_FieldName);
            this.WaitForElement(PINExpireInMinutes_FieldName);
            this.WaitForElement(NumberOfPINDigits_FieldName);

            return this;
        }


        public WebSiteRecordPage ValidateNameFieldText(string ExpectedText)
        {
            ValidateElementValue(Name_Field, ExpectedText);

            return this;
        }

        public WebSiteRecordPage ValidateDisplayNameFieldText(string ExpectedText)
        {
            WaitForElement(DisplayName_Field);
            MoveToElementInPage(DisplayName_Field);
            ValidateElementValue(DisplayName_Field, ExpectedText);

            return this;
        }

        public WebSiteRecordPage ValidateHomePageFieldText(string ExpectedText)
        {
            ValidateElementValue(HomePage_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateMemberHomePageFieldText(string ExpectedText)
        {
            ValidateElementValue(MemberHomePage_Field, ExpectedText);

            return this;
        }

        //public WebSiteRecordPage ValidateFooterFieldText(string ExpectedText)
        //{
        //    ValidateElementValue(Footer_Field, ExpectedText);

        //    return this;
        //}
        public WebSiteRecordPage ValidateApplicationFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Application_FieldLink, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateUserRecordTypeFieldLinkText(string ExpectedText)
        {
            ValidateElementText(UserRecordType_FieldLink, ExpectedText);

            return this;
        }



        public WebSiteRecordPage ValidateWebSiteURL(string ExpectedText)
        {
            ValidateElementValue(WebSiteURL_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateWebSiteURLLink(string ExpectedText)
        {
            ValidateElementText(WebSiteURL_Link, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateMinimumPasswordLength(string ExpectedText)
        {
            ValidateElementValue(MinimumPasswordLength_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateMinimumNumericCharacters(string ExpectedText)
        {
            ValidateElementValue(MinimumNumericCharacters_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateMinimumSpecialCharacters(string ExpectedText)
        {
            ValidateElementValue(MinimumSpecialCharacters_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateMinimumUppercaseLetters(string ExpectedText)
        {
            ValidateElementValue(MinimumUppercaseLetters_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateMaximumPasswordAgeDays(string ExpectedText)
        {
            ValidateElementValue(MaximumPasswordAgeDays_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateMinimumpasswordAgeDays(string ExpectedText)
        {
            ValidateElementValue(MinimumpasswordAgeDays_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateEnforcePasswordHistory(string ExpectedText)
        {
            ValidateElementValue(EnforcePasswordHistory_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidatePasswordResetExpireInMinutes(string ExpectedText)
        {
            ValidateElementValue(PasswordResetExpireInMinutes_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateAccountLockoutDuration(string ExpectedText)
        {
            ValidateElementValue(AccountLockoutDuration_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateAccountLockoutThreshold(string ExpectedText)
        {
            ValidateElementValue(AccountLockoutThreshold_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateResetAccountLockoutCounterAfter(string ExpectedText)
        {
            ValidateElementValue(ResetAccountLockoutCounterAfter_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateDefaultPINReceivingMethod(string ExpectedText)
        {
            ValidatePicklistSelectedText(DefaultPINReceivingMethod_Picklist, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidatePINExpireInMinutes(string ExpectedText)
        {
            ValidateElementValue(PINExpireInMinutes_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateNumberOfPINDigits(string ExpectedText)
        {
            ValidateElementValue(NumberOfPINDigits_Field, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateMaxInvalidPinAttemptAllowed(string ExpectedText)
        {
            ValidateElementValue(MaxInvalidPinAttemptAllowed_Field, ExpectedText);

            return this;
        }




        public WebSiteRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Name_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateApplicationFieldErrorLabel(string ExpectedText)
        {
            ValidateElementText(Application_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateUserRecordTypeFieldErrorLabel(string ExpectedText)
        {
            ValidateElementText(UserRecordType_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateWebSiteURLFieldErrorLabel(string ExpectedText)
        {
            ValidateElementText(WebSiteURL_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateMinimumPasswordLengthFieldErrorLabel(string ExpectedText)
        {
            ValidateElementText(MinimumPasswordLength_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateMinimumPasswordAgeDaysFieldErrorLabel(string ExpectedText)
        {
            ValidateElementText(MinimumpasswordAgeDays_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateEnforcePasswordHistoryFieldErrorLabel(string ExpectedText)
        {
            ValidateElementText(EnforcePasswordHistory_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateNumberOfPINDigitsFieldErrorLabel(string ExpectedText)
        {
            ValidateElementText(NumberOfPINDigits_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteRecordPage ValidateAccountLockoutThresholdFieldErrorLabel(string ExpectedText)
        {
            ValidateElementText(AccountLockoutThreshold_ErrorLabel, ExpectedText);

            return this;
        }




        public WebSiteRecordPage ValidateEmailVerificationRequiredYesOptionChecked()
        {
            ValidateElementChecked(EmailVerificationRequiredYesOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ValidateEmailVerificationRequiredNoOptionChecked()
        {
            ValidateElementChecked(EmailVerificationRequiredNoOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ValidateUserApprovalRequiredYesOptionChecked()
        {
            ValidateElementChecked(UserApprovalRequiredYesOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ValidateUserApprovalRequiredNoOptionChecked()
        {
            ValidateElementChecked(UserApprovalRequiredNoOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ValidateEnableAccountLockingYesOptionChecked()
        {
            ValidateElementChecked(EnableAccountLockingYesOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ValidateEnableAccountLockingNoOptionChecked()
        {
            ValidateElementChecked(EnableAccountLockingNoOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ValidateEnableTwoFactorAuthenticationYesOptionChecked()
        {
            ValidateElementChecked(EnableTwoFactorAuthenticationYesOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ValidateEnableTwoFactorAuthenticationNoOptionChecked()
        {
            ValidateElementChecked(EnableTwoFactorAuthenticationNoOption_RadioButton);

            return this;
        }




        public WebSiteRecordPage InsertWebSiteURL(string TextToInsert)
        {
            SendKeys(WebSiteURL_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertMinimumPasswordLength(string TextToInsert)
        {
            SendKeys(MinimumPasswordLength_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertMinimumNumericCharacters(string TextToInsert)
        {
            SendKeys(MinimumNumericCharacters_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertMinimumSpecialCharacters(string TextToInsert)
        {
            SendKeys(MinimumSpecialCharacters_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertMinimumUppercaseLetters(string TextToInsert)
        {
            SendKeys(MinimumUppercaseLetters_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertMaximumPasswordAgeDays(string TextToInsert)
        {
            SendKeys(MaximumPasswordAgeDays_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertMinimumpasswordAgeDays(string TextToInsert)
        {
            SendKeys(MinimumpasswordAgeDays_Field, TextToInsert);
            SendKeysWithoutClearing(MinimumpasswordAgeDays_Field, Keys.Tab);

            return this;
        }
        public WebSiteRecordPage InsertEnforcePasswordHistory(string TextToInsert)
        {
            SendKeys(EnforcePasswordHistory_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertPasswordResetExpireInMinutes(string TextToInsert)
        {
            SendKeys(PasswordResetExpireInMinutes_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertAccountLockoutDuration(string TextToInsert)
        {
            SendKeys(AccountLockoutDuration_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertAccountLockoutThreshold(string TextToInsert)
        {
            SendKeys(AccountLockoutThreshold_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertResetAccountLockoutCounterAfter(string TextToInsert)
        {
            SendKeys(ResetAccountLockoutCounterAfter_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertDefaultPINReceivingMethod(string TextToSelect)
        {
            SelectPicklistElementByText(DefaultPINReceivingMethod_Picklist, TextToSelect);

            return this;
        }
        public WebSiteRecordPage InsertPINExpireInMinutes(string TextToInsert)
        {
            SendKeys(PINExpireInMinutes_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertNumberOfPINDigits(string TextToInsert)
        {
            SendKeys(NumberOfPINDigits_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertMaxInvalidPinAttemptAllowed(string TextToInsert)
        {
            SendKeys(MaxInvalidPinAttemptAllowed_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage SelectAuthenticationType(string TextToSelect)
        {
            SelectPicklistElementByText(AuthenticationType_Picklist, TextToSelect);

            return this;
        }
        public WebSiteRecordPage SelectType(string TextToSelect)
        {
            SelectPicklistElementByText(Type_Picklist, TextToSelect);

            return this;
        }



        public WebSiteRecordPage ClickEmailVerificationRequiredYesOptionChecked()
        {
            Click(EmailVerificationRequiredYesOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ClickEmailVerificationRequiredNoOptionChecked()
        {
            Click(EmailVerificationRequiredNoOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ClickUserApprovalRequiredYesOptionChecked()
        {
            Click(UserApprovalRequiredYesOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ClickUserApprovalRequiredNoOptionChecked()
        {
            Click(UserApprovalRequiredNoOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ClickEnableAccountLockingYesOptionChecked()
        {
            Click(EnableAccountLockingYesOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ClickEnableAccountLockingNoOptionChecked()
        {
            Click(EnableAccountLockingNoOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ClickEnableTwoFactorAuthenticationYesOptionChecked()
        {
            Click(EnableTwoFactorAuthenticationYesOption_RadioButton);

            return this;
        }
        public WebSiteRecordPage ClickEnableTwoFactorAuthenticationNoOptionChecked()
        {
            Click(EnableTwoFactorAuthenticationNoOption_RadioButton);

            return this;
        }





        public WebSiteRecordPage InsertName(string TextToInsert)
        {
            SendKeys(Name_Field, TextToInsert);

            return this;
        }

        public WebSiteRecordPage InsertDisplayName(string TextToInsert)
        {
            WaitForElement(DisplayName_Field);
            MoveToElementInPage(DisplayName_Field);
            SendKeys(DisplayName_Field, TextToInsert);

            return this;
        }

        public WebSiteRecordPage InsertRegistrationFormName(string TextToInsert)
        {
            SendKeys(RegistrationFormName_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertRecordsPerPage(string TextToInsert)
        {
            SendKeys(RecordsPerPage_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertSessionTimeoutInMinutes(string TextToInsert)
        {
            SendKeys(SessionTimeoutInMinutes_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertHomePage(string TextToInsert)
        {
            SendKeys(HomePage_Field, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertMemberHomePage(string TextToInsert)
        {
            SendKeys(MemberHomePage_Field, TextToInsert);

            return this;
        }
        //public WebSiteRecordPage InsertFooter(string TextToInsert)
        //{
        //    SendKeys(Footer_Field, TextToInsert);

        //    return this;
        //}
        public WebSiteRecordPage InsertLogoPath(string TextToInsert)
        {
            SendKeys(Logo_Browse, TextToInsert);

            return this;
        }
        public WebSiteRecordPage InsertVersion(string TextToInsert)
        {
            SendKeys(Version_Field, TextToInsert);

            return this;
        }



        public WebSiteRecordPage TapLogoEditButton()
        {
            Click(Logo_EditButton);

            return this;
        }
        public WebSiteRecordPage TapLogoUploadButton()
        {
            Click(Logo_UploadButton);

            return this;
        }



        public WebSiteRecordPage TapApplicationLookupButton()
        {
            WaitForElement(Application_LookupButton);
            Click(Application_LookupButton);

            return this;
        }
        public WebSiteRecordPage TapApplicationRemoveButton()
        {
            Click(Application_RemoveButton);

            return this;
        }
        public WebSiteRecordPage TapUserRecordTypeLookupButton()
        {
            Click(UserRecordType_LookupButton);

            return this;
        }
        public WebSiteRecordPage TapUserRecordTypeRemoveButton()
        {
            Click(UserRecordType_RemoveButton);

            return this;
        }





        public WebSiteRecordPage ValidateApplicationLookupButtonEnabled()
        {
            ValidateElementEnabled(Application_LookupButton);

            return this;
        }
        public WebSiteRecordPage ValidateApplicationLookupButtonDisabled()
        {
            ValidateElementDisabled(Application_LookupButton);

            return this;
        }
        public WebSiteRecordPage ValidateUserRecordTypeLookupButtonDisabled()
        {
            ValidateElementDisabled(UserRecordType_LookupButton);

            return this;
        }





        public WebSiteRecordPage ClickOnHomePageAutoCompleteOption(string ElementText)
        {
            WaitForElement(HomePage_AutoCompleteDiv(ElementText));
            Click(HomePage_AutoCompleteDiv(ElementText));

            return this;
        }
        public WebSiteRecordPage ClickOnMemberHomePageAutoCompleteOption(string ElementText)
        {
            WaitForElement(MemberHomePage_AutoCompleteDiv(ElementText));
            Click(MemberHomePage_AutoCompleteDiv(ElementText));

            return this;
        }
        public WebSiteRecordPage ClickOnHeaderAutoCompleteOption(string ElementText)
        {
            WaitForElement(Header_AutoCompleteDiv(ElementText));
            Click(Header_AutoCompleteDiv(ElementText));

            return this;
        }
        //public WebSiteRecordPage ClickOnFooterAutoCompleteOption(string ElementText)
        //{
        //    WaitForElement(Footer_AutoCompleteDiv(ElementText));
        //    Click(Footer_AutoCompleteDiv(ElementText));

        //    return this;
        //}



        public WebSiteRecordPage ValidateHomePageAutoCompleteOptionVisibility(string ElementText, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(HomePage_AutoCompleteDiv(ElementText));
            else
                WaitForElementNotVisible(HomePage_AutoCompleteDiv(ElementText), 3);

            return this;
        }
        public WebSiteRecordPage ValidateMemberHomePageAutoCompleteOptionVisibility(string ElementText, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MemberHomePage_AutoCompleteDiv(ElementText));
            else
                WaitForElementNotVisible(MemberHomePage_AutoCompleteDiv(ElementText), 3);

            return this;
        }
        public WebSiteRecordPage ValidateHeaderAutoCompleteOptionVisibility(string ElementText, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Header_AutoCompleteDiv(ElementText));
            else
                WaitForElementNotVisible(Header_AutoCompleteDiv(ElementText), 3);

            return this;
        }
        public WebSiteRecordPage ValidateFooterAutoCompleteOptionVisibility(string ElementText, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Footer_AutoCompleteDiv(ElementText));
            else
                WaitForElementNotVisible(Footer_AutoCompleteDiv(ElementText), 3);

            return this;
        }
        public WebSiteRecordPage ValidateApplicationLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Application_LookupButton);
            else
                WaitForElementNotVisible(Application_LookupButton, 3);

            return this;
        }
        public WebSiteRecordPage ValidateApplicationRemoveButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Application_RemoveButton);
            else
                WaitForElementNotVisible(Application_RemoveButton, 3);

            return this;
        }
        public WebSiteRecordPage ValidateUserRecordTypeLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(UserRecordType_LookupButton);
            else
                WaitForElementNotVisible(UserRecordType_LookupButton, 3);

            return this;
        }
        public WebSiteRecordPage ValidateUserRecordTypeRemoveButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(UserRecordType_RemoveButton);
            else
                WaitForElementNotVisible(UserRecordType_RemoveButton, 3);

            return this;
        }
        public WebSiteRecordPage ValidateNameFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Name_ErrorLabel);
            else
                WaitForElementNotVisible(Name_ErrorLabel, 3);

            return this;
        }
        public WebSiteRecordPage ValidateApplicationFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Application_ErrorLabel);
            else
                WaitForElementNotVisible(Application_ErrorLabel, 3);

            return this;
        }
        public WebSiteRecordPage ValidateUserRecordTypeFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(UserRecordType_ErrorLabel);
            else
                WaitForElementNotVisible(UserRecordType_ErrorLabel, 3);

            return this;
        }
        public WebSiteRecordPage ValidateWebSiteURLFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(WebSiteURL_ErrorLabel);
            else
                WaitForElementNotVisible(WebSiteURL_ErrorLabel, 3);

            return this;
        }
        public WebSiteRecordPage ValidateMinimumPasswordLengthFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MinimumPasswordLength_ErrorLabel);
            else
                WaitForElementNotVisible(MinimumPasswordLength_ErrorLabel, 3);

            return this;
        }
        public WebSiteRecordPage ValidateMinimumPasswordAgeDaysFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MinimumpasswordAgeDays_ErrorLabel);
            else
                WaitForElementNotVisible(MinimumpasswordAgeDays_ErrorLabel, 3);

            return this;
        }
        public WebSiteRecordPage ValidateEnforcePasswordHistoryFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EnforcePasswordHistory_ErrorLabel);
            else
                WaitForElementNotVisible(EnforcePasswordHistory_ErrorLabel, 3);

            return this;
        }
        public WebSiteRecordPage ValidateNumberOfPINDigitsFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NumberOfPINDigits_ErrorLabel);
            else
                WaitForElementNotVisible(NumberOfPINDigits_ErrorLabel, 3);

            return this;
        }
        public WebSiteRecordPage ValidateAccountLockoutThresholdFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AccountLockoutThreshold_ErrorLabel);
            else
                WaitForElementNotVisible(AccountLockoutThreshold_ErrorLabel, 3);

            return this;
        }


        public WebSiteRecordPage NavigateToWebsitePages()
        {
            Click(MenuButton);

            WaitForElement(WebsitePages_Link);
            Click(WebsitePages_Link);

            return this;
        }

        public WebSiteRecordPage NavigateToWebsiteSettings()
        {
            Click(MenuButton);

            WaitForElement(WebsiteSettings_Link);
            Click(WebsiteSettings_Link);

            return this;
        }

        public WebSiteRecordPage NavigateToWebsiteAnnouncements()
        {
            Click(MenuButton);

            WaitForElement(WebsiteAnnouncement_Link);
            Click(WebsiteAnnouncement_Link);

            return this;
        }

        public WebSiteRecordPage NavigateToWebsiteUsers()
        {
            Click(MenuButton);

            WaitForElement(WebsiteUsers_Link);
            Click(WebsiteUsers_Link);

            return this;
        }

        public WebSiteRecordPage NavigateToWebsiteUserAccessAudit()
        {
            Click(MenuButton);

            WaitForElement(WebsiteUserAccessAudit_Link);
            Click(WebsiteUserAccessAudit_Link);

            return this;
        }

        public WebSiteRecordPage NavigateToWebsiteSitemaps()
        {
            Click(MenuButton);

            WaitForElement(WebsiteSitemap_Link);
            Click(WebsiteSitemap_Link);

            return this;
        }

        public WebSiteRecordPage NavigateToWebsitePointsOfContact()
        {
            Click(MenuButton);

            WaitForElement(WebsitePointsOfContact_Link);
            Click(WebsitePointsOfContact_Link);

            return this;
        }

        public WebSiteRecordPage NavigateToWebsiteContact()
        {
            Click(MenuButton);

            WaitForElement(WebsiteContact_Link);
            Click(WebsiteContact_Link);

            return this;
        }
        public WebSiteRecordPage NavigateToPortalTasks()
        {
            Click(MenuButton);

            WaitForElement(WebsitePortalTasks_Link);
            Click(WebsitePortalTasks_Link);

            return this;
        }

        public WebSiteRecordPage NavigateToWebsiteFeedback()
        {
            Click(MenuButton);

            WaitForElement(WebsiteFeedback_Link);
            Click(WebsiteFeedback_Link);

            return this;
        }

        public WebSiteRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);

            return this;
        }
        public WebSiteRecordPage ClickCloneButton()
        {
            Click(cloneWebsiteButton);

            return this;
        }

    }
}
