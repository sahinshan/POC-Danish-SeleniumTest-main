using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteUserRecordPage : CommonMethods
    {

        public WebSiteUserRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websiteuser&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By unlockButton = By.Id("TI_UnlockAccount");
        readonly By resetPasswordButton = By.Id("TI_ResetPassword");




        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By WebSiteUserPINLink = By.Id("CWNavItem_WebsiteUserPin");
        readonly By WebsiteUserPasswordResetLink = By.Id("CWNavItem_WebsiteUserPasswordReset");
        readonly By WebsiteUserPasswordHistoryLink = By.Id("CWNavItem_WebsiteUserPasswordHistory");
        
        readonly By Details_Tab = By.XPath("//*[@id='CWNavGroup_EditForm']/a");
        readonly By AccessSessions_Tab = By.XPath("//*[@id='CWNavGroup_AccessSessions']/a");



        readonly By Website_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteid']/label[text()='Website']");
        readonly By UserName_FieldName = By.XPath("//*[@id='CWLabelHolder_username']/label[text()='Username']");
        readonly By Password_FieldName = By.XPath("//*[@id='CWLabelHolder_password']/label[text()='Password']");
        readonly By Profile_FieldName = By.XPath("//*[@id='CWLabelHolder_profileid']/label[text()='Profile']");
        readonly By Status_FieldName = By.XPath("//*[@id='CWLabelHolder_statusid']/label[text()='Status']");
        readonly By SecurityProfile_FieldName = By.XPath("//*[@id='CWLabelHolder_securityprofileid']/label[text()='Security Profile']");
        readonly By EmailVerified_FieldName = By.XPath("//*[@id='CWLabelHolder_emailverified']/label[text()='Email Verified']");

        readonly By LastLoginDate_FieldName = By.XPath("//*[@id='CWLabelHolder_lastlogindate']/label[text()='Last Login Date']");
        readonly By LastPasswordChangedDate_FieldName = By.XPath("//*[@id='CWLabelHolder_lastpasswordchangeddate']/label[text()='Last Password Changed Date']");
        readonly By IsAccountLocked_FieldName = By.XPath("//*[@id='CWLabelHolder_isaccountlocked']/label[text()='Is Account Locked']");
        readonly By LockedOutDate_FieldName = By.XPath("//*[@id='CWLabelHolder_lockedoutdate']/label[text()='Locked Out Date']");
        readonly By FailedPasswordAttemptCount_FieldName = By.XPath("//*[@id='CWLabelHolder_failedpasswordattemptcount']/label[text()='Failed Password Attempt Count']");
        readonly By LastFailedPasswordAttemptDate_FieldName = By.XPath("//*[@id='CWLabelHolder_lastfailedpasswordattemptdate']/label[text()='Last Failed Password Attempt Date']");
        readonly By FailedPINAttemptCount_FieldName = By.XPath("//*[@id='CWLabelHolder_failedpinattemptcount']/label[text()='Failed PIN Attempt Count']");
        
        readonly By TwoFactorAuthenticationType_FieldName = By.XPath("//*[@id='CWLabelHolder_twofactorauthenticationtypeid']/label[text()='Two Factor Authentication Type']");





        readonly By Website_FieldLink = By.Id("CWField_websiteid_Link");
        readonly By Website_RemoveButton = By.Id("CWClearLookup_websiteid");
        readonly By Website_LookupButton = By.Id("CWLookupBtn_websiteid");

        readonly By UserName_Field = By.XPath("//*[@id='CWField_username']");
        readonly By UserName_ErrorLabel = By.XPath("//*[@id='CWControlHolder_username']/label/span");

        readonly By Password_Field = By.XPath("//input[@id='CWField_password']");
        readonly By ChangePassword_Link = By.XPath("//a[@id='CWField_password']");

        readonly By Profile_FieldLink = By.Id("CWField_profileid_Link");
        readonly By Profile_RemoveButton = By.Id("CWClearLookup_profileid");
        readonly By Profile_LookupButton = By.Id("CWLookupBtn_profileid");
        readonly By Profile_ErrorLabel = By.XPath("//*[@id='CWControlHolder_profileid']/label/span");

        readonly By Status_Picklist = By.XPath("//*[@id='CWField_statusid']");
        readonly By Status_ErrorLabel = By.XPath("//*[@id='CWControlHolder_statusid']/label/span");
        
        readonly By SecurityProfile_FieldLink = By.Id("CWField_securityprofileid_Link");
        readonly By SecurityProfile_RemoveButton = By.Id("CWClearLookup_securityprofileid");
        readonly By SecurityProfile_LookupButton = By.Id("CWLookupBtn_securityprofileid");

        readonly By EmailVerifiedYesOption_RadioButton = By.Id("CWField_emailverified_1");
        readonly By EmailVerifiedNoOption_RadioButton = By.Id("CWField_emailverified_0");


        readonly By LastLoginDate_DateField = By.Id("CWField_lastlogindate");
        readonly By LastLoginDate_TimeField = By.Id("CWField_lastlogindate_Time");
        readonly By LastPasswordChangedDate_DateField = By.Id("CWField_lastpasswordchangeddate");
        readonly By LastPasswordChangedDate_TimeField = By.Id("CWField_lastpasswordchangeddate_Time");
        readonly By IsAccountLockedYesOption_RadioButton = By.Id("CWField_isaccountlocked_1");
        readonly By IsAccountLockedNoOption_RadioButton = By.Id("CWField_isaccountlocked_0");
        readonly By LockedOutDate_DateField = By.Id("CWField_lockedoutdate");
        readonly By LockedOutDate_TimeField = By.Id("CWField_lockedoutdate_Time");
        readonly By FailedPasswordAttemptCount_Field = By.Id("CWField_failedpasswordattemptcount");
        readonly By LastFailedPasswordAttemptDate_DateField = By.Id("CWField_lastfailedpasswordattemptdate");
        readonly By LastFailedPasswordAttemptDate_TimeField = By.Id("CWField_lastfailedpasswordattemptdate_Time");
        readonly By FailedPINAttemptCount_Field = By.Id("CWField_failedpinattemptcount");

        readonly By TwoFactorAuthenticationType_Field = By.Id("CWField_twofactorauthenticationtypeid");




        public WebSiteUserRecordPage WaitForWebSiteUserRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            this.WaitForElement(Website_FieldName);
            this.WaitForElement(UserName_FieldName);
            this.WaitForElement(Password_FieldName);
            this.WaitForElement(Profile_FieldName);
            this.WaitForElement(Status_FieldName);
            this.WaitForElement(SecurityProfile_FieldName);
            this.WaitForElement(EmailVerified_FieldName);

            this.WaitForElement(LastLoginDate_FieldName);
            this.WaitForElement(LastPasswordChangedDate_FieldName);
            this.WaitForElement(IsAccountLocked_FieldName);
            this.WaitForElement(LockedOutDate_FieldName);
            this.WaitForElement(FailedPasswordAttemptCount_FieldName);
            this.WaitForElement(LastFailedPasswordAttemptDate_FieldName);
            this.WaitForElement(FailedPINAttemptCount_FieldName);

            this.WaitForElement(TwoFactorAuthenticationType_FieldName);

            return this;
        }



        public WebSiteUserRecordPage ValidateWebSiteFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Website_FieldLink, ExpectedText);

            return this;
        }
        public WebSiteUserRecordPage ValidateUserNameFieldText(string ExpectedText)
        {
            ValidateElementValue(UserName_Field, ExpectedText);

            return this;
        }
        public WebSiteUserRecordPage ValidateProfileFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Profile_FieldLink, ExpectedText);

            return this;
        }
        public WebSiteUserRecordPage ValidateStatusFieldText( string ExpectedText)
        {
            ValidatePicklistSelectedText(Status_Picklist, ExpectedText);

            return this;
        }
        public WebSiteUserRecordPage ValidateSecurityProfileFieldLinkText(string ExpectedText)
        {
            ValidateElementText(SecurityProfile_FieldLink, ExpectedText);

            return this;
        }

        public WebSiteUserRecordPage ValidateEmailVerifiedYesRadioButtonChecked()
        {
            ValidateElementChecked(EmailVerifiedYesOption_RadioButton);

            return this;
        }
        public WebSiteUserRecordPage ValidateEmailVerifiedNoRadioButtonChecked()
        {
            ValidateElementChecked(EmailVerifiedNoOption_RadioButton);

            return this;
        }

        public WebSiteUserRecordPage ValidateLastLoginDateFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(LastLoginDate_DateField, ExpectedDate);
            ValidateElementValue(LastLoginDate_TimeField, ExpectedTime);

            return this;
        }
        public WebSiteUserRecordPage ValidateLastPasswordChangedDateFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(LastPasswordChangedDate_DateField, ExpectedDate);
            ValidateElementValue(LastPasswordChangedDate_TimeField, ExpectedTime);

            return this;
        }

        public WebSiteUserRecordPage ValidateIsAccountLockedYesRadioButtonChecked()
        {
            ValidateElementChecked(IsAccountLockedYesOption_RadioButton);

            return this;
        }
        public WebSiteUserRecordPage ValidateIsAccountLockedNoRadioButtonChecked()
        {
            ValidateElementChecked(IsAccountLockedNoOption_RadioButton);

            return this;
        }

        public WebSiteUserRecordPage ValidateLockedOutDateFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(LockedOutDate_DateField, ExpectedDate);
            ValidateElementValue(LockedOutDate_TimeField, ExpectedTime);

            return this;
        }
        public WebSiteUserRecordPage ValidateFailedPasswordAttemptCountFieldText(string ExpectedText)
        {
            ValidateElementValue(FailedPasswordAttemptCount_Field, ExpectedText);

            return this;
        }
        public WebSiteUserRecordPage ValidateLastFailedPasswordAttemptDateFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(LastFailedPasswordAttemptDate_DateField, ExpectedDate);
            ValidateElementValue(LastFailedPasswordAttemptDate_TimeField, ExpectedTime);

            return this;
        }
        public WebSiteUserRecordPage ValidateFailedPINAttemptCountFieldText(string ExpectedText)
        {
            ValidateElementValue(FailedPINAttemptCount_Field, ExpectedText);

            return this;
        }

        public WebSiteUserRecordPage ValidateTwoFactorAuthenticationTypeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(TwoFactorAuthenticationType_Field, ExpectedText);

            return this;
        }



        public WebSiteUserRecordPage ValidateUserNameFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(UserName_ErrorLabel);
            else
                WaitForElementNotVisible(UserName_ErrorLabel, 3);

            return this;
        }
        public WebSiteUserRecordPage ValidateProfileFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Profile_ErrorLabel);
            else
                WaitForElementNotVisible(Profile_ErrorLabel, 3);

            return this;
        }
        public WebSiteUserRecordPage ValidateStatusFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Status_ErrorLabel);
            else
                WaitForElementNotVisible(Status_ErrorLabel, 3);

            return this;
        }



        public WebSiteUserRecordPage ValidateUserNameFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(UserName_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteUserRecordPage ValidateProfileFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Profile_ErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteUserRecordPage ValidateStatusFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Status_ErrorLabel, ExpectedText);

            return this;
        }




        public WebSiteUserRecordPage InsertUserName(string TextToInsert)
        {
            SendKeys(UserName_Field, TextToInsert);

            return this;
        }
        public WebSiteUserRecordPage InsertPassword(string TextToInsert)
        {
            SendKeys(Password_Field, TextToInsert);

            return this;
        }
        public WebSiteUserRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(Status_Picklist, TextToSelect);

            return this;
        }
        public WebSiteUserRecordPage SelectTwoFactorAuthenticationType(string TextToSelect)
        {
            SelectPicklistElementByText(TwoFactorAuthenticationType_Field, TextToSelect);

            return this;
        }





        public WebSiteUserRecordPage ClickWebsiteLookupButton()
        {
            Click(Website_LookupButton);

            return this;
        }
        public WebSiteUserRecordPage ClickWebsiteRemoveButton()
        {
            Click(Website_RemoveButton);

            return this;
        }
        public WebSiteUserRecordPage ClickProfileLookupButton()
        {
            Click(Profile_LookupButton);

            return this;
        }
        public WebSiteUserRecordPage ClickProfileRemoveButton()
        {
            Click(Profile_RemoveButton);

            return this;
        }
        public WebSiteUserRecordPage ClickSecurityProfileLookupButton()
        {
            Click(SecurityProfile_LookupButton);

            return this;
        }
        public WebSiteUserRecordPage ClickSecurityProfileRemoveButton()
        {
            Click(SecurityProfile_RemoveButton);

            return this;
        }
        public WebSiteUserRecordPage ClickChangePasswordLink()
        {
            Click(ChangePassword_Link);

            return this;
        }


        public WebSiteUserRecordPage ClickEmailVerifiedYesOption()
        {
            Click(EmailVerifiedYesOption_RadioButton);

            return this;
        }
        public WebSiteUserRecordPage ClickEmailVerifiedNoOption()
        {
            Click(EmailVerifiedNoOption_RadioButton);

            return this;
        }





        public WebSiteUserRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteUserRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public WebSiteUserRecordPage ClickDeleteButton()
        {
            Click(additionalItemsMenuButton);
            
            WaitForElement(deleteButton);
            Click(deleteButton);

            return this;
        }
        public WebSiteUserRecordPage ClickUnlockButton()
        {
            Click(unlockButton);

            return this;
        }
        public WebSiteUserRecordPage ClickResetPasswordButton()
        {
            Click(resetPasswordButton);

            return this;
        }




        public WebSiteUserRecordPage NavigateToWebsiteUserPIN()
        {
            Click(MenuButton);

            WaitForElement(WebSiteUserPINLink);
            Click(WebSiteUserPINLink);

            return this;
        }
        public WebSiteUserRecordPage NavigateToWebsiteUserPasswordReset()
        {
            Click(MenuButton);

            WaitForElement(WebsiteUserPasswordResetLink);
            Click(WebsiteUserPasswordResetLink);

            return this;
        }
        public WebSiteUserRecordPage NavigateToWebsiteUserPasswordHistory()
        {
            Click(MenuButton);

            WaitForElement(WebsiteUserPasswordHistoryLink);
            Click(WebsiteUserPasswordHistoryLink);

            return this;
        }



        public WebSiteUserRecordPage NavigateToWebsiteUserAccessAuditTab()
        {
            WaitForElement(AccessSessions_Tab);
            Click(AccessSessions_Tab);

            return this;
        }

    }
}
