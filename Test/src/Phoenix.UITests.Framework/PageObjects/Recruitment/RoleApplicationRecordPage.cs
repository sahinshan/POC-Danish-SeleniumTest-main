using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Recruitment
{
    public class RoleApplicationRecordPage : CommonMethods
    {
        public RoleApplicationRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Frames

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_applicant = By.XPath("//iframe[contains(@id,'iframe_')][contains(@src,'type=applicant&')]"); 
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_')][contains(@src,'editpage.aspx?type=recruitmentroleapplicant')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=recruitmentroleapplicant'
        readonly By iframe_MessageSetRecruitmentStatus = By.XPath("//*[@id = 'CWExternalPage_MessageSetRecruitmentStatus']/iframe[@id = 'CWIFrame_MessageSetRecruitmentStatus']");

        #endregion

        #region Toolbar Section

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By CopyRecordButton = By.Id("TI_CopyRecordLink");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By additionalToolbarElementsButton = By.Id("CWToolbarMenu");

        #endregion

        #region Mandatory / Link Fields 

        readonly By Applicant_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_applicantid']//span[@class='mandatory']");
        readonly By Applicant_LinkField = By.Id("CWField_applicantid_Link");

        readonly By Role_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_careproviderstaffroletypeid']//span[@class='mandatory']");
        readonly By Role_LinkField = By.Id("CWField_careproviderstaffroletypeid_Link");
        readonly By Role_Field = By.Id("CWField_careproviderstaffroletypeid_cwname");

        readonly By ApplicationDate_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_applicationdate']//span[@class='mandatory']");

        readonly By RecruitmentStatus_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_recruitmentstatusid']//span[@class='mandatory']");
        readonly By RecruitmentStatus_Field = By.Id("CWField_recruitmentstatusid");
        readonly By RecruitmentStatus_AlertMessage = By.XPath("//*[@id = 'CWForm']//label[@id = 'messagesetrecruitmentstatuslabel']");

        readonly By ResponsibleTeam_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_ownerid']//span[@class='mandatory']");
        readonly By ResponsibleTeam_LinkField = By.Id("CWField_ownerid_Link");

        readonly By ResponsibleRecruiter_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']//span[@class='mandatory']");
        readonly By ResponsibleRecruiter_LinkField = By.Id("CWField_responsibleuserid_Link");

        readonly By TargetTeamField_Label = By.XPath("//label[text() = 'Target Team']");
        readonly By TargetBusinessUnitField_Label = By.XPath("//label[text() = 'Target Business Unit']");
        readonly By TargetTeam_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_targetteamid']//span[@class='mandatory']");
        readonly By TargetTeam_LinkField = By.Id("CWField_targetteamid_Link");
        readonly By TargetTeam_Field = By.Id("CWField_targetteamid_cwname");

        readonly By ApplicationSource_Field = By.Id("CWField_applicationsourceid_cwname");
        readonly By ApplicationSource_LinkField = By.Id("CWField_applicationsourceid_Link");

        readonly By RejectedReason_Mandatory_Field = By.XPath("//*[@id='CWLabelHolder_rejectedreasonid']//span[@class='mandatory']");
        readonly By RejectedReason_LinkField = By.Id("CWField_rejectedreasonid_Link");

        #endregion

        #region Fields / Lookup

        readonly By applicantLookupButton = By.XPath("//*[@id='CWLookupBtn_applicantid']");
        readonly By roleLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderstaffroletypeid']");
        readonly By responsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By responsibleRecruiterLookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
        readonly By targetTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_targetteamid']");
        readonly By applicationSourceLookupButton = By.XPath("//*[@id='CWLookupBtn_applicationsourceid']");
        readonly By contractTypeLookupButton = By.Id("CWLookupBtn_contracttypeid");
        readonly By rejectedReasonLookupButton = By.XPath("//*[@id='CWLookupBtn_rejectedreasonid']");

        readonly By applicationDateField = By.Id("CWField_applicationdate");
        readonly By additionalCommentField = By.Id("CWField_additonalcomments");

        readonly By progressTowardsInductionStatusField = By.Id("CWField_inductionprogress");
        readonly By progressTowardsFullyAcceptedField = By.Id("CWField_fullyacceptedprogress");
        readonly By daysToHireField = By.Id("CWField_daystohire");

        readonly By progressTowardsInductionStatusFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_inductionprogress']/label");
        readonly By progressTowardsFullyAcceptedFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_fullyacceptedprogress']/label");

        #endregion

        #region Field error messages
        readonly By targetTeamField_Error = By.XPath("//*[@id='CWControlHolder_targetteamid']/label/span");

        #endregion


        public RoleApplicationRecordPage WaitForRoleApplicationRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(pageHeader);

            WaitForElement(Applicant_LinkField);
            WaitForElement(ResponsibleRecruiter_LinkField);

            WaitForElement(responsibleRecruiterLookupButton);

            WaitForElement(applicationDateField);
            WaitForElement(targetTeamLookupButton);

            return this;
        }

        public RoleApplicationRecordPage WaitForRoleApplicationRecordToLoadFromApplicantDashboardPage()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog_applicant);
            SwitchToIframe(iframe_CWDialog_applicant);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(pageHeader);

            WaitForElement(Applicant_LinkField);
            WaitForElement(ResponsibleRecruiter_LinkField);

            WaitForElement(responsibleRecruiterLookupButton);

            WaitForElement(applicationDateField);
            WaitForElement(targetTeamLookupButton);

            return this;
        }

        public RoleApplicationRecordPage WaitForRoleApplicationRecordPageToLoadFromAdvancedSearch()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(pageHeader);

            WaitForElement(Applicant_LinkField);
            WaitForElement(ResponsibleRecruiter_LinkField);

            WaitForElement(responsibleRecruiterLookupButton);

            WaitForElement(applicationDateField);
            WaitForElement(targetTeamLookupButton);

            return this;
        }

        public RoleApplicationRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            ScrollToElement(backButton);
            Click(backButton);

            return this;
        }

        public RoleApplicationRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            ScrollToElement(saveButton);
            Click(saveButton);

            return this;
        }

        public RoleApplicationRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            ScrollToElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public RoleApplicationRecordPage ClickRoleLookupButton()
        {
            WaitForElementToBeClickable(roleLookupButton);
            ScrollToElement(roleLookupButton);
            Click(roleLookupButton);

            return this;
        }

        public RoleApplicationRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(responsibleTeamLookupButton);
            ScrollToElement(responsibleTeamLookupButton);
            Click(responsibleTeamLookupButton);

            return this;
        }

        public RoleApplicationRecordPage ClickTargetTeamLookupButton()
        {
            WaitForElementToBeClickable(targetTeamLookupButton);
            ScrollToElement(targetTeamLookupButton);
            Click(targetTeamLookupButton);

            return this;
        }

        public RoleApplicationRecordPage ClickApplicationSourceLookupButton()
        {
            WaitForElementToBeClickable(applicationSourceLookupButton);
            ScrollToElement(applicationSourceLookupButton);
            Click(applicationSourceLookupButton);

            return this;
        }

        public RoleApplicationRecordPage ClickContractTypeLookupButton()
        {
            WaitForElementToBeClickable(contractTypeLookupButton);
            ScrollToElement(contractTypeLookupButton);
            Click(contractTypeLookupButton);

            return this;
        }

        public RoleApplicationRecordPage ClickRejectedReasonLookupButton()
        {
            WaitForElementToBeClickable(rejectedReasonLookupButton);
            ScrollToElement(rejectedReasonLookupButton);
            Click(rejectedReasonLookupButton);

            return this;
        }

        public RoleApplicationRecordPage InsertApplicationDateValue(string Date)
        {
            WaitForElementVisible(applicationDateField);
            ScrollToElement(applicationDateField);
            SendKeys(applicationDateField, Date);

            return this;
        }

        public RoleApplicationRecordPage InsertRejctionAdditonalComments(string TextToInsert)
        {
            WaitForElementVisible(additionalCommentField);
            ScrollToElement(additionalCommentField);
            SendKeys(additionalCommentField, TextToInsert);

            return this;
        }

        public RoleApplicationRecordPage SelectRecruitmentStatus(string Select_RecruitmentStatus)
        {
            WaitForElementVisible(RecruitmentStatus_Field);
            ScrollToElement(RecruitmentStatus_Field);
            SelectPicklistElementByText(RecruitmentStatus_Field, Select_RecruitmentStatus);

            return this;
        }

        public RoleApplicationRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            //WaitForElementVisible(deleteRecordButton);

            return this;
        }

        public RoleApplicationRecordPage ValidateRecruitmentStatus_Field_Disabled(bool ExpectVisible)
        {
            WaitForElementVisible(RecruitmentStatus_Field);

            if (ExpectVisible)
                ValidateElementDisabled(RecruitmentStatus_Field);
            else
                ValidateElementNotDisabled(RecruitmentStatus_Field);

            return this;
        }

        public RoleApplicationRecordPage ValidateRecruitmentStatusFieldAlertMessage(string ExpectedMessage)
        {
            WaitForElement(iframe_MessageSetRecruitmentStatus);
            SwitchToIframe(iframe_MessageSetRecruitmentStatus);            
            WaitForElementVisible(RecruitmentStatus_AlertMessage);
            ScrollToElement(RecruitmentStatus_AlertMessage);
            string ActualMessage = GetElementText(RecruitmentStatus_AlertMessage);
            Assert.AreEqual(ExpectedMessage, ActualMessage);

            return this;
        }


        public RoleApplicationRecordPage ValidateApplicantName(string ExpectedText)
        {
            WaitForElementVisible(Applicant_LinkField);
            ScrollToElement(Applicant_LinkField);
            ValidateElementText(Applicant_LinkField, ExpectedText);

            return this;
        }

        public RoleApplicationRecordPage ValidateRoleName(string ExpectedText)
        {
            WaitForElementVisible(Role_LinkField);
            ScrollToElement(Role_LinkField);
            ValidateElementText(Role_LinkField, ExpectedText);

            return this;
        }

        public RoleApplicationRecordPage ValidateResponsibleTeamName(string ExpectedText)
        {
            WaitForElementVisible(ResponsibleTeam_LinkField);
            ScrollToElement(ResponsibleTeam_LinkField);
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public RoleApplicationRecordPage ValidateResponsibleRecruiterName(string ExpectedText)
        {
            WaitForElementVisible(ResponsibleRecruiter_LinkField);
            ScrollToElement(ResponsibleRecruiter_LinkField);
            ValidateElementText(ResponsibleRecruiter_LinkField, ExpectedText);

            return this;
        }

        public RoleApplicationRecordPage ValidateTargetTeamName(string ExpectedText)
        {
            WaitForElementVisible(TargetTeam_LinkField);
            ScrollToElement(TargetTeam_LinkField);
            ValidateElementText(TargetTeam_LinkField, ExpectedText);

            return this;
        }

        public RoleApplicationRecordPage ValidateTargetTeamFieldFormError(string ExpectedText)
        {
            WaitForElementVisible(targetTeamField_Error);
            ScrollToElement(targetTeamField_Error);
            ValidateElementByTitle(targetTeamField_Error, ExpectedText);
            return this;
        }

        public RoleApplicationRecordPage ValidateApplicationSourceName(string ExpectedText)
        {
            WaitForElementVisible(ApplicationSource_LinkField);
            ScrollToElement(ApplicationSource_LinkField);
            ValidateElementText(ApplicationSource_LinkField, ExpectedText);

            return this;
        }

        public RoleApplicationRecordPage ValidateRejectedReasonName(string ExpectedText)
        {
            WaitForElementVisible(RejectedReason_LinkField);
            ScrollToElement(RejectedReason_LinkField);
            ValidateElementText(RejectedReason_LinkField, ExpectedText);

            return this;
        }

        public RoleApplicationRecordPage ValidateRoleFieldBeBlank()
        {
            bool visibility = GetElementVisibility(Role_Field);
            Assert.IsTrue(visibility);
            return this;
        }

        public RoleApplicationRecordPage ValidateTargetTeamFieldBeBlank()
        {
            bool visibility = GetElementVisibility(TargetTeam_Field);
            Assert.IsTrue(visibility);
            return this;
        }

        public RoleApplicationRecordPage ValidateApplicationSourceFieldBeBlank()
        {
            bool visibility = GetElementVisibility(ApplicationSource_Field);
            Assert.IsTrue(visibility);
            return this;
        }

        public RoleApplicationRecordPage ValidateApplicant_Mandatory_Field()
        {
            ValidateElementEnabled(Applicant_Mandatory_Field);
            return this;
        }

        public RoleApplicationRecordPage ValidateRole_Mandatory_Field()
        {
            ValidateElementEnabled(Role_Mandatory_Field);
            return this;
        }

        public RoleApplicationRecordPage ValidateApplicationDate_Mandatory_Field()
        {
            ValidateElementEnabled(ApplicationDate_Mandatory_Field);
            return this;
        }

        public RoleApplicationRecordPage ValidateRecruitmentStatus_Mandatory_Field()
        {
            ValidateElementEnabled(RecruitmentStatus_Mandatory_Field);
            return this;
        }

        public RoleApplicationRecordPage ValidateResponsibleTeam_Mandatory_Field()
        {
            ValidateElementEnabled(ResponsibleTeam_Mandatory_Field);
            return this;
        }

        public RoleApplicationRecordPage ValidateResponsibleRecruiter_Mandatory_Field()
        {
            ValidateElementEnabled(ResponsibleRecruiter_Mandatory_Field);
            return this;
        }

        public RoleApplicationRecordPage ValidateTargetTeam_Mandatory_Field()
        {
            ValidateElementEnabled(TargetTeam_Mandatory_Field);
            return this;
        }

        public RoleApplicationRecordPage ValidateRejectedReason_Mandatory_Field()
        {
            ValidateElementEnabled(RejectedReason_Mandatory_Field);
            return this;
        }

        public RoleApplicationRecordPage ValidateAdditonalComments_Field()
        {
            ValidateElementEnabled(additionalCommentField);
            return this;
        }

        public RoleApplicationRecordPage ValidateMandatoryFields()
        {
            ValidateApplicant_Mandatory_Field();
            ValidateRole_Mandatory_Field();
            ValidateApplicationDate_Mandatory_Field();
            ValidateRecruitmentStatus_Mandatory_Field();
            ValidateResponsibleTeam_Mandatory_Field();
            ValidateResponsibleRecruiter_Mandatory_Field();
            ValidateTargetTeam_Mandatory_Field();

            return this;
        }

        public RoleApplicationRecordPage ValidateTargetBusinessUnitFieldIsNotDisplayed()
        {
            WaitForElementNotVisible(TargetBusinessUnitField_Label, 3);
            return this;
        }

        public RoleApplicationRecordPage ValidateTargetTeamFieldLabelIsVisible(bool Visible)
        {
            if (Visible)
                WaitForElementVisible(TargetTeamField_Label);
            else
                WaitForElementNotVisible(TargetTeamField_Label, 3);
            return this;
        }

        public RoleApplicationRecordPage ValidateTargetTeamFieldsAreVisible(bool Visible)
        {
            if (Visible)
            {
                WaitForElementVisible(TargetTeamField_Label);
                ScrollToElement(TargetTeamField_Label);
                WaitForElementVisible(targetTeamLookupButton);
            }
            else
            {
                WaitForElementNotVisible(TargetTeamField_Label, 2);
                WaitForElementNotVisible(targetTeamLookupButton, 2);
            }
            return this;
        }

        public RoleApplicationRecordPage ValidateTargetTeamLookupButtonIsEnabled(bool ExpectedEnabled)
        {
            WaitForElementVisible(targetTeamLookupButton);
            ScrollToElement(targetTeamLookupButton);
            if (ExpectedEnabled)
                ValidateElementEnabled(targetTeamLookupButton);
            else
                ValidateElementDisabled(targetTeamLookupButton);
            return this;
        }

        public RoleApplicationRecordPage ValidateBlankFields()
        {
            ValidateRoleFieldBeBlank();
            ValidateTargetTeamFieldBeBlank();
            ValidateApplicationSourceFieldBeBlank();

            return this;
        }

        public RoleApplicationRecordPage ValidateRecruitmentStatusSelectedText(string ExpectedText)
        {
            WaitForElementVisible(RecruitmentStatus_Field);
            ScrollToElement(RecruitmentStatus_Field);
            ValidatePicklistSelectedText(RecruitmentStatus_Field, ExpectedText);
            return this;
        }

        public RoleApplicationRecordPage ValidateRecruitmentStatus_Field_ReadOnly(bool ExpectVisible)
        {
            WaitForElementVisible(RecruitmentStatus_Field);

            if (ExpectVisible)
                ValidateElementReadonly(RecruitmentStatus_Field);
            else
                ValidateElementNotReadonly(RecruitmentStatus_Field);

            return this;
        }

        public RoleApplicationRecordPage ClickApplicantLookupButton()
        {
            WaitForElementToBeClickable(applicantLookupButton);
            ScrollToElement(applicantLookupButton);
            Click(applicantLookupButton);

            return this;
        }

        public RoleApplicationRecordPage ValidateProgressTowardsInductionStatusFieldValue(string ExpectedValue)
        {            
            ScrollToElement(progressTowardsInductionStatusField);
            ValidateElementValue(progressTowardsInductionStatusField, ExpectedValue);

            return this;
        }

        public RoleApplicationRecordPage ValidateProgressTowardsFullyAcceptedFieldValue(string ExpectedValue)
        {
            ScrollToElement(progressTowardsFullyAcceptedField);
            ValidateElementValue(progressTowardsFullyAcceptedField, ExpectedValue);

            return this;
        }

        public RoleApplicationRecordPage ValidateDaysToHireFieldValue(string ExpectedValue)
        {
            ScrollToElement(daysToHireField);
            ValidateElementValue(daysToHireField, ExpectedValue);

            return this;
        }

        public RoleApplicationRecordPage ValidateProgressTowardsInductionStatusFieldDisabled()
        {
            ScrollToElement(progressTowardsInductionStatusField);
            ValidateElementDisabled(progressTowardsInductionStatusField);

            return this;
        }

        public RoleApplicationRecordPage ValidateProgressTowardsFullyAcceptedFieldDisabled()
        {
            ScrollToElement(progressTowardsFullyAcceptedField);
            ValidateElementDisabled(progressTowardsFullyAcceptedField);

            return this;
        }

        public RoleApplicationRecordPage ValidateDaysToHireFieldDisabled()
        {
            ScrollToElement(daysToHireField);
            ValidateElementDisabled(daysToHireField);

            return this;
        }

        public RoleApplicationRecordPage ValidateProgressTowardsInductionStatusFieldIsDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(progressTowardsInductionStatusField);
            }
            else
            {
                WaitForElementNotVisible(progressTowardsInductionStatusField, 5);
            }
            Assert.AreEqual(progressTowardsInductionStatusField, GetElementVisibility(progressTowardsInductionStatusField));            

            return this;
        }

        public RoleApplicationRecordPage ValidateProgressTowardsFullyAcceptedFieldDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(progressTowardsFullyAcceptedField);
            }
            else
            {
                WaitForElementNotVisible(progressTowardsFullyAcceptedField, 5);
            }
            Assert.AreEqual(progressTowardsInductionStatusField, GetElementVisibility(progressTowardsFullyAcceptedField));

            return this;
        }

        public RoleApplicationRecordPage ValidateDaysToHireFieldDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                ScrollToElement(daysToHireField);
            }
            else
            {
                WaitForElementNotVisible(daysToHireField, 5);
            }
            Assert.AreEqual(daysToHireField, GetElementVisibility(progressTowardsFullyAcceptedField));

            return this;
        }

        public RoleApplicationRecordPage ValidateAdditonalComments_FieldValue(string ExpectedText)
        {
            ScrollToElement(additionalCommentField);
            string ActualText = GetElementText(additionalCommentField);
            Assert.AreEqual(ExpectedText, ActualText);
            return this;
        }

        public RoleApplicationRecordPage ValidatRoleApplicationRecordPageTitle(string PageTitle)
        {
            WaitForElementVisible(pageHeader);
            ScrollToElement(pageHeader);
            ValidateElementTextContainsText(pageHeader, PageTitle);

            return this;
        }

        public RoleApplicationRecordPage ValidateProgressTowardsInductionStatusLabel(string ExpectedText)
        {
            WaitForElement(progressTowardsInductionStatusFieldLabel);
            ScrollToElement(progressTowardsInductionStatusFieldLabel);
            ValidateElementText(progressTowardsInductionStatusFieldLabel, ExpectedText);
            return this;
        }

        public RoleApplicationRecordPage ValidateProgressTowardsFullyAcceptedLabel(string ExpectedText)
        {
            WaitForElement(progressTowardsFullyAcceptedFieldLabel);
            ScrollToElement(progressTowardsFullyAcceptedFieldLabel);
            ValidateElementText(progressTowardsFullyAcceptedFieldLabel, ExpectedText);
            return this;
        }

    }
}
