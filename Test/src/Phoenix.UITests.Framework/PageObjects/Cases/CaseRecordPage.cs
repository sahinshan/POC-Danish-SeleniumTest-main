using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CaseRecordPage : CommonMethods
    {
        public CaseRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By caseRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=case&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By TimelinePanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By pageTitle = By.XPath("//*[@id='CWToolbar']/div/h1/span");

        By cardTitleLink(string recordID) => By.XPath("//div[@class='card-title row']/div/a[contains(@onclick,'" + recordID + "')]");



        #region Top Menu

        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By additionalIItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By correctErrors_Button = By.Id("TI_ErrorCorrection");
        readonly By deleteRecord_Button = By.Id("TI_DeleteRecordButton");
        readonly By copyRecordLinkButton = By.Id("TI_CopyRecordLink");

        readonly By restrictAccessButton = By.Id("TI_RestrictAccessButton");
        readonly By removeRestrictionButton = By.Id("TI_RemoveRestrictionButton");
        readonly By activateButton = By.Id("TI_ActivateButton");


        #endregion

        #region Navigation Area

        readonly By detailsSection = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");
        readonly By timelineSection = By.XPath("//*[@id='CWNavGroup_Timeline']/a");

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");



        #region Left Sub Menu

        readonly By activitiesLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_Activities']/a");
        readonly By relatedItemsLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_RelatedItems']/a");
        readonly By HealthLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_Health']/a");
        readonly By otherInformationLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_OtherInformation']/a");




        readonly By CaseCaseNoteLeftSubMenuItem = By.XPath("//a[@id='CWNavItem_CaseCaseNote']");
        readonly By CaseTasksLeftSubMenuItem = By.XPath("//a[@id='CWNavItem_Task']");
        readonly By formsCaseLeftSubMenuItem = By.XPath("//a[@id='CWNavItem_CaseForm']");
        readonly By attachmentsLeftSubMenuItem = By.XPath("//a[@id='CWNavItem_Attachments']");
        readonly By caseInvolvementLeftSubMenuItem = By.XPath("//a[@id='CWNavItem_CaseInvolvement']");
        readonly By HealthAppointmentsLeftSubMenuItem = By.XPath("//a[@id='CWNavItem_HealthAppointment']");
        readonly By RecurringHealthAppointmentsSubMenuItem = By.XPath("//a[@id = 'CWNavItem_RecurringHealthAppointments']");
        readonly By InpatientLeaveAwolLeftSubMenuItem = By.XPath("//a[@id='CWNavItem_InpatientLeaveAwol']");
        readonly By BrokerageEpisodesLeftSubMenuItem = By.XPath("//a[@id='CWNavItem_BrokerageEpisode']");
        readonly By ConsultantEpisodesLeftSubMenuItem = By.XPath("//*[@id='CWNavItem_InpatientConsultantEpisodes']");
        readonly By RTTWaitTimeLeftSubMenuItem = By.XPath("//*[@id='CWNavItem_RTTWaitTime']");


        #endregion



        #endregion

        #region Fields

        readonly By caseNoField = By.XPath("//input[@id='CWField_casenumber']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndClose = By.Id("TI_SaveAndCloseButton");

        readonly By backButton = By.Id("BackButton");

        readonly By caseNumber_Field = By.Id("CWField_casenumber");
        readonly By contactRecievedBy_Field = By.Id("CWField_contactreceivedbyid_cwname");
        readonly By contactRecievedBy_LookUpButton = By.Id("CWLookupBtn_contactreceivedbyid");
        readonly By person_Field = By.Id("CWField_personid");
        readonly By personIDLink = By.Id("CWField_personid_Link");
        readonly By person_LookUpButton = By.Id("CWLookupBtn_personid");
        readonly By contactReason_Field = By.Id("CWField_contactreasonid_cwname");
        readonly By contactReason_LookUpButton = By.Id("CWLookupBtn_contactreasonid");
        readonly By caseDateAndTime_DateField = By.Id("CWField_startdatetime");
        readonly By admissionDateTime_DateField = By.Id("CWField_admissiondatetime");
        readonly By admissionDateTime_TimeField = By.Id("CWField_admissiondatetime_Time");
        readonly By caseDateAndTime_TimeField = By.Id("CWField_startdatetime_Time");
        readonly By presentingPriority_Field = By.Id("CWField_presentingpriorityid_cwname");
        readonly By presentingPriority_LookUpButton = By.Id("CWLookupBtn_presentingpriorityid");
        readonly By initialContact_Field = By.Id("CWField_initialcontactid_cwname");
        readonly By initialContact_LookUpButton = By.Id("CWLookupBtn_initialcontactid");
        readonly By CINcode_Field = By.Id("CWField_childinneedcodeid_cwname");
        readonly By CINcode_LookUpButton = By.Id("CWLookupBtn_childinneedcodeid");
        readonly By dateTimeContactReceived_DateField = By.Id("CWField_contactreceiveddatetime");
        readonly By dateTimeContactReceived_TimeField = By.Id("CWField_contactreceiveddatetime_Time");
        readonly By additionalInformation = By.Id("CWField_additionalinformation");

        readonly By dateRequestReceived_DateField = By.Id("CWField_requestreceiveddatetime");
        readonly By InpatientStatus_Picklist = By.Id("CWField_inpatientcasestatusid");


        readonly By contactMadeBy_Field = By.Id("CWField_contactmadebyid_cwname");
        readonly By contactMadeBy_LookUpButton = By.Id("CWLookupBtn_contactmadebyid");
        readonly By contactMadeByFreeText_Field = By.Id("CWField_contactmadebyname");
        readonly By caseOrigin_Field = By.Id("CWField_caseoriginid");
        readonly By contactMadeByName_Field = By.Id("CWField_contactmadebyname");
        readonly By contactSource_Field = By.Id("CWField_contactsourceid_cwname");
        readonly By contactSource_LookUpButton = By.Id("CWLookupBtn_contactsourceid");
        readonly By admissionSource_LookUpButton = By.Id("CWLookupBtn_inpatientadmissionsourceid");
        readonly By administrativeCategory_LookUpButton = By.Id("CWLookupBtn_administrativecategoryid");
        readonly By isThePersonAwareOfTheContact_Field = By.Id("CWField_personawareofcontactid");
        readonly By doesPersonAgreeSupportThisContact_Field = By.Id("CWField_personsupportcontactid");
        readonly By doesPersonAgreeSupportThisContact_MandatoryField = By.XPath("//*[@id='CWLabelHolder_personsupportcontactid']/label/span[@class = 'mandatory']");
        readonly By doesParentCarersAgreeSupportThisContact_Field = By.Id("CWField_carersupportcontactid");
        readonly By nokCarerAwareOfThisContact_Field = By.Id("CWField_nextofkinawareofcontactid");
        readonly By nokCarerAwareOfThisContact_MandatoryField = By.XPath("//*[@id='CWLabelHolder_nextofkinawareofcontactid']/label/span[@class='mandatory']");
        readonly By isParentCarerAwareOfThisContact_Field = By.Id("CWField_carerawareofcontactid");
        readonly By caseStatus_Field = By.Id("CWField_casestatusid_cwname");
        readonly By caseStatus_LinkField = By.Id("CWField_casestatusid_Link");
        readonly By caseStatus_LookUpButton = By.Id("CWLookupBtn_casestatusid");
        readonly By casePriority_Field = By.Id("CWField_casepriorityid_cwname");
        readonly By casePriority_LookUpButton = By.Id("CWLookupBtn_casepriorityid");
        readonly By reviewDate_Field = By.Id("CWField_reviewdate");
        readonly By communityClinicTeamRequired_LookUpButton = By.Id("CWLookupBtn_communityandclinicteamid");
        readonly By serviceTypeRequested_LookUpButton = By.Id("CWLookupBtn_servicetyperequestedid");

        //Presenting Situation
        readonly By OutlineNeedForAdmission_Field = By.Id("CWField_presentingneeddetails");
        readonly By AdmissionMethod_LookUpButton = By.Id("CWLookupBtn_inpatientadmissionmethodid");
        readonly By CurrentConsultant_LookUpButton = By.Id("CWLookupBtn_currentconsultantid");

        //Bed Occupancy
        readonly By Hospital_LookUpButton = By.Id("CWLookupBtn_providerid");
        readonly By Ward_LookUpButton = By.Id("CWLookupBtn_inpatientwardid");
        readonly By BayRoom_LookUpButton = By.Id("CWLookupBtn_inpatientbayid");
        readonly By Bed_LookUpButton = By.Id("CWLookupBtn_inpatientbedid");
        readonly By ResponsibleWard_LookUpButton = By.Id("CWLookupBtn_inpatientresponsiblewardid");

        //Referral to Treatment
        readonly By RTTReferral_Picklist = By.Id("CWField_rttreferralid");

        readonly By RTTTreatmentStatus_FieldLabel = By.XPath("//*[@id='CWLabelHolder_rtttreatmentstatusid']/label[text()='RTT Treatment Status']");
        readonly By RTTTreatmentStatus_LookUpButton = By.Id("CWLookupBtn_rtttreatmentstatusid");
        readonly By RTTTreatmentStatus_LinkText = By.Id("CWField_rtttreatmentstatusid_Link");
        readonly By RTTTreatmentStatus_MandatoryField = By.XPath("//*[@id='CWLabelHolder_rtttreatmentstatusid']/label/span[@class='mandatory']");

        readonly By RTTPathway_FieldLabel = By.XPath("//*[@id='CWLabelHolder_rttpathwayid']/label[text()='RTT Pathway']");
        readonly By RTTPathway_LookUpButton = By.Id("CWLookupBtn_rttpathwayid");
        readonly By RTTPathway_LinkText = By.Id("CWField_rttpathwayid_Link");
        readonly By RTTPathway_MandatoryField = By.XPath("//*[@id='CWLabelHolder_rttpathwayid']/label/span[@class='mandatory']");

        readonly By TransferredFromProvider_FieldLabel = By.XPath("//*[@id='CWLabelHolder_transferredfromproviderid']/label[text()='Transferred From Provider']");
        readonly By TransferredFromProvider_LookUpButton = By.Id("CWLookupBtn_transferredfromproviderid");
        readonly By TransferredFromProvider_LinkText = By.Id("CWField_transferredfromproviderid_Link");
        readonly By TransferredFromProvider_MandatoryField = By.XPath("//*[@id='CWLabelHolder_transferredfromproviderid']/label/span[@class='mandatory']");

        readonly By OriginalRTTReferralStartDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_rttreferraloriginalstartdate']/label[text()='Original RTT Referral Start Date']");
        readonly By OriginalRTTReferralStartDate_DateField = By.Id("CWField_rttreferraloriginalstartdate");
        readonly By OriginalRTTReferralStartDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_rttreferraloriginalstartdate']/label/span[@class='mandatory']");

        readonly By referringAgencyCaseId_Field = By.Id("CWField_referringagencycaseid");
        readonly By dateAndTimeOfContactWithTrainedStaff_DateField = By.Id("CWField_contactwithtrainedstaffdate");
        readonly By dateAndTimeOfContactWithTrainedStaff_TimeField = By.Id("CWField_contactwithtrainedstaffdate_Time");

        readonly By fosteringExperience_Field = By.Id("CWField_fosteringexperienceid_cwname");
        readonly By fosteringExperience_LookUpButton = By.Id("CWLookupBtn_fosteringexperienceid");
           
        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");
        readonly By contactRecievedBy_NotificationErrorLabel = By.XPath("//*[@id='CWControlHolder_contactreceivedbyid']/label/span");
        readonly By contactReason_NotificationErrorLabel = By.XPath("//*[@id='CWControlHolder_contactreasonid']/label/span");
        readonly By caseDateAndTime_DateNotificationErrorLabel = By.XPath("//*[@id='CWControlHolder_startdatetime']/div/div[1]/label/span");
        readonly By caseDateAndTime_TimeNotificationErrorLabel = By.XPath("//*[@id='CWControlHolder_startdatetime']/div/div[2]/label/span");
        readonly By dateTimeContactReceived_DateNotificationErrorLabel = By.XPath("//*[@id='CWControlHolder_contactreceiveddatetime']/div/div[1]/label/span");
        readonly By dateTimeContactReceived_TimeNotificationErrorLabel = By.XPath("//*[@id='CWControlHolder_contactreceiveddatetime']/div/div[2]/label/span");
        readonly By isThePersonAwareOfTheContact_NotificationErrorLabel = By.XPath("//*[@id='CWControlHolder_personawareofcontactid']/label/span");
        readonly By responsibleUser_LookupButton = By.Id("CWLookupBtn_responsibleuserid");
        readonly By responsibleUser_LinkField = By.Id("CWField_responsibleuserid_Link");
        readonly By responsibleTeam_LinkField = By.Id("CWField_ownerid_Link");


        readonly By policeNotified_YesOption = By.Id("CWField_policenotified_1");
        readonly By policaNotifiedDate_Field = By.Id("CWField_policenotifieddate");
        readonly By policeNotes_Filed = By.Id("CWField_policenotes");

        // Discharge/Closure

        readonly By dischargePerson_LabelField = By.Id("CWLabelHolder_dischargeperson");
        readonly By dischargePerson_YesOption = By.Id("CWField_dischargeperson_1");
        readonly By dischargePerson_NoOption = By.Id("CWField_dischargeperson_0");

        readonly By actualDischargeDateTime_LabelField = By.Id("CWLabelHolder_actualdischargedatetime");
        readonly By actualDischargeDateTime_DateField = By.Id("CWField_actualdischargedatetime");
        readonly By actualDischargeDateTime_TimeField = By.Id("CWField_actualdischargedatetime_Time");

        readonly By carerNOKNotified_LabelField = By.Id("CWLabelHolder_carernoknotified");
        readonly By carerNOKNotified_YesOption = By.Id("CWField_carernoknotified_1");
        readonly By carerNOKNotified_NoOption = By.Id("CWField_carernoknotified_0");

        readonly By dischargeMethod_LabelField = By.Id("CWLabelHolder_caseclosurereasonid");
        readonly By dischargeMethod_LookUpButton = By.Id("CWLookupBtn_caseclosurereasonid");
        readonly By dischargeMethod_LinkField = By.Id("CWField_caseclosurereasonid_Link");
        readonly By dischargeMethod_RemoveButton = By.Id("CWClearLookup_caseclosurereasonid");

        readonly By wishesContactFromAnIMHA_LabelField = By.Id("CWLabelHolder_wishescontactfromimhaid");
        readonly By wishesContactFromAnIMHA_PickList = By.Id("CWField_wishescontactfromimhaid");

        readonly By actualDischargeDestination_LabelField = By.Id("CWLabelHolder_actualdischargedestinationid");
        readonly By actualDischargeDestination_LookUpButton = By.Id("CWLookupBtn_actualdischargedestinationid");
        readonly By actualDischargeDestination_LinkField = By.Id("CWField_actualdischargedestinationid_Link");
        readonly By actualDischargeDestination_RemoveButton = By.Id("CWClearLookup_actualdischargedestinationid");

        readonly By dischargedToHomeAddress_LabelField = By.Id("CWLabelHolder_dischargedtohomeaddressid");
        readonly By dischargedToHomeAddress_PickList = By.Id("CWField_dischargedtohomeaddressid");

        readonly By reasonNotAccepted_LabelField = By.Id("CWLabelHolder_caserejectedreasonid");
        readonly By reasonNotAccepted_LookUpButton = By.Id("CWLookupBtn_caserejectedreasonid");
        readonly By reasonNotAccepted_LinkField = By.Id("CWField_caserejectedreasonid_Link");
        readonly By reasonNotAccepted_RemoveButton = By.Id("CWClearLookup_caserejectedreasonid");

        readonly By teamResponsibleForFollowUpAppointment_LabelField = By.Id("CWLabelHolder_followupteamid");
        readonly By teamResponsibleForFollowUpAppointment_LookUpButton = By.Id("CWLookupBtn_followupteamid");
        readonly By teamResponsibleForFollowUpAppointment_LinkField = By.Id("CWField_followupteamid_Link");
        readonly By teamResponsibleForFollowUpAppointment_RemoveButton = By.Id("CWClearLookup_followupteamid");

        readonly By dischargeInformation_LabelField = By.Id("CWLabelHolder_dischargeinformation");
        readonly By dischargeInformation_TextArea = By.Id("CWField_dischargeinformation");

        readonly By section117AftercareEntitlement_LabelField = By.Id("CWLabelHolder_section117aftercareentitlement");
        readonly By section117AftercareEntitlement_YesOption = By.Id("CWField_section117aftercareentitlement_1");
        readonly By section117AftercareEntitlement_NoOption = By.Id("CWField_section117aftercareentitlement_0");

        readonly By transferToProvider_LabelField = By.Id("CWLabelHolder_transferproviderid");
        readonly By transferToProvider_LookUpButton = By.Id("CWLookupBtn_transferproviderid");
        readonly By transferToProvider_LinkField = By.Id("CWField_transferproviderid_Link");
        readonly By transferToProvider_RemoveButton = By.Id("CWClearLookup_transferproviderid");

        readonly By dischargingProfessional_LabelField = By.Id("CWLabelHolder_dischargingprofessionalid");
        readonly By dischargingProfessional_LookUpButton = By.Id("CWLookupBtn_dischargingprofessionalid");
        readonly By dischargingProfessional_LinkField = By.Id("CWField_dischargingprofessionalid_Link");
        readonly By dischargingProfessional_RemoveButton = By.Id("CWClearLookup_dischargingprofessionalid");

        #endregion

        public CaseRecordPage OpenCaseRecordHyperlink(string CaseRecordHyperlink)
        {
            driver.Navigate().GoToUrl(CaseRecordHyperlink);

            return this;
        }

        public CaseRecordPage WaitForCaseRecordPageToLoad()
        {
           
            WaitForElement(caseRecordIFrame);
            SwitchToIframe(caseRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementToBeClickable(detailsSection);
            Click(detailsSection);

            WaitForElement(caseNoField);
            
            return this;
        }

        public CaseRecordPage WaitForCaseRecordPageToFullyLoadIframes()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseRecordIFrame);
            SwitchToIframe(caseRecordIFrame);

            WaitForElement(detailsSection);
           
            WaitForElement(caseNoField);

            return this;
        }

        public CaseRecordPage WaitForTimelinePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseRecordIFrame);
            SwitchToIframe(caseRecordIFrame);

            WaitForElement(TimelinePanelIFrame);
            SwitchToIframe(TimelinePanelIFrame);
            
            return this;
        }

        public CaseRecordPage WaitForCaseRecordPageToLoadFromHyperlink(string ExpectedTitle)
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(pageTitle);
            ValidateElementText(pageTitle, ExpectedTitle);

            WaitForElement(detailsSection);
            Click(detailsSection);

            WaitForElement(caseNoField);

            return this;
        }

        public CaseRecordPage SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseRecordIFrame);
            SwitchToIframe(caseRecordIFrame);

            WaitForElement(detailsSection);
            Click(detailsSection);

            WaitForElement(caseNoField);

            return this;
        }

        public CaseRecordPage WaitForPersonCaseRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementVisible(caseRecordIFrame);
            SwitchToIframe(caseRecordIFrame);

            WaitForElementVisible(detailsSection);
            WaitForElement(detailsSection);
            MoveToElementInPage(detailsSection);
            
            WaitForElementToBeClickable(detailsSection);
            Click(detailsSection);

            WaitForElementVisible(caseNoField);

            return this;
        }

        public CaseRecordPage WaitForCaseRecordToSaved()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementVisible(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementVisible(caseRecordIFrame);
            SwitchToIframe(caseRecordIFrame);

            WaitForElementVisible(timelineSection);
            MoveToElementInPage(timelineSection);

            WaitForElementToBeClickable(detailsSection);
            MoveToElementInPage(detailsSection);
            Click(detailsSection);

            WaitForElementVisible(caseNoField);

            return this;
        }

        public CaseRecordPage ValidateCaseRecordTitle(string CaseRecordTitle)
        {
            WaitForElement(pageTitle);
            MoveToElementInPage(pageTitle);
            ValidateElementText(pageTitle, CaseRecordTitle);

            return this;
        }

        public CaseRecordPage ValidateCaseNoFieldValue(string ExpectedText)
        {
            ValidateElementValue(caseNoField, ExpectedText);

            return this;
        }

        public CaseRecordPage ValidateRecordPresent(string RecordID)
        {
            WaitForElement(cardTitleLink(RecordID));

            return this;
        }

        public CaseRecordPage ValidateInpatientCaseDisabled()
        {
            ValidateElementDisabled(person_LookUpButton);
            ValidateElementDisabled(InpatientStatus_Picklist);
            ValidateElementDisabled(dateTimeContactReceived_DateField);
            ValidateElementDisabled(dateTimeContactReceived_TimeField);
            ValidateElementDisabled(contactSource_LookUpButton);
            ValidateElementDisabled(presentingPriority_LookUpButton);
            ValidateElementDisabled(contactMadeBy_LookUpButton);

            return this;
        }

        /// <summary>
        /// Tap on the "Details" link on the Case page
        /// </summary>
        public CaseRecordPage TapDetailsLink()
        {
            WaitForElementToBeClickable(detailsSection);
            Click(detailsSection);

            return this;
        }

        public CaseRecordPage ClickDetailsLink()
        {
            WaitForElementToBeClickable(detailsSection);
            Click(detailsSection);
          
            return this;
        }

        /// <summary>
        /// Tap on the "Assign" button
        /// </summary>
        public CaseRecordPage TapAssignButton()
        {
            WaitForElementToBeClickable(assignButton);
            Click(assignButton);
            
            return this;
        }

        public CaseCasesFormPage NavigateToCaseNotesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(activitiesLeftSubMenu);
            Click(activitiesLeftSubMenu);

            WaitForElementToBeClickable(CaseCaseNoteLeftSubMenuItem);
            Click(CaseCaseNoteLeftSubMenuItem);

            return new CaseCasesFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseRecordPage NavigateToTasksPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(activitiesLeftSubMenu);
            Click(activitiesLeftSubMenu);

            WaitForElementToBeClickable(CaseTasksLeftSubMenuItem);
            Click(CaseTasksLeftSubMenuItem);

            return this;
        }

        public CaseCasesFormPage NavigateToFormsCase()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(relatedItemsLeftSubMenu);
            Click(relatedItemsLeftSubMenu);

            WaitForElementToBeClickable(formsCaseLeftSubMenuItem);
            Click(formsCaseLeftSubMenuItem);
            
            return new CaseCasesFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseRecordPage NavigateToAttachments()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(relatedItemsLeftSubMenu);
            Click(relatedItemsLeftSubMenu);

            WaitForElementToBeClickable(attachmentsLeftSubMenuItem);
            Click(attachmentsLeftSubMenuItem);

            return this;
        }

        public CaseRecordPage NavigateToCaseInvolvement()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(relatedItemsLeftSubMenu);
            Click(relatedItemsLeftSubMenu);

            WaitForElementToBeClickable(caseInvolvementLeftSubMenuItem);
            Click(caseInvolvementLeftSubMenuItem);

            return this;
        }

        public CaseCasesFormPage NavigateToBrokerageEpisodes()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(relatedItemsLeftSubMenu);
            Click(relatedItemsLeftSubMenu);

            WaitForElementToBeClickable(BrokerageEpisodesLeftSubMenuItem);
            Click(BrokerageEpisodesLeftSubMenuItem);

            return new CaseCasesFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseCasesFormPage NavigateToHealthAppointmentsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(HealthLeftSubMenu);
            Click(HealthLeftSubMenu);

            WaitForElementToBeClickable(HealthAppointmentsLeftSubMenuItem);
            Click(HealthAppointmentsLeftSubMenuItem);

            return new CaseCasesFormPage(this.driver, this.Wait, this.appURL);
        }
        
        public CaseRecordPage NavigateToRecurringHealthAppointmentsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(HealthLeftSubMenu);
            Click(HealthLeftSubMenu);

            WaitForElementToBeClickable(RecurringHealthAppointmentsSubMenuItem);
            Click(RecurringHealthAppointmentsSubMenuItem);

            return this;
        }        

        public CaseCasesFormPage NavigateToRTTWaitTimePage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(HealthLeftSubMenu);
            Click(HealthLeftSubMenu);

            WaitForElementToBeClickable(RTTWaitTimeLeftSubMenuItem);
            Click(RTTWaitTimeLeftSubMenuItem);

            return new CaseCasesFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseCasesFormPage NavigateToConsultantEpisodesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(HealthLeftSubMenu);
            Click(HealthLeftSubMenu);

            WaitForElementToBeClickable(ConsultantEpisodesLeftSubMenuItem);
            Click(ConsultantEpisodesLeftSubMenuItem);

            return new CaseCasesFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseCasesFormPage NavigateToLeaveAWOLPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(HealthLeftSubMenu);
            Click(HealthLeftSubMenu);

            WaitForElementToBeClickable(InpatientLeaveAwolLeftSubMenuItem);
            Click(InpatientLeaveAwolLeftSubMenuItem);

            return new CaseCasesFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseRecordPage ClickCopyRecordLinkButton()
        {
            WaitForElementToBeClickable(additionalIItemsButton);
            Click(additionalIItemsButton);

            WaitForElementToBeClickable(copyRecordLinkButton);
            Click(copyRecordLinkButton);

            return this;
        }

        public CaseRecordPage ClickAdditionalIItemsButton()
        {
            WaitForElementToBeClickable(additionalIItemsButton);
            Click(additionalIItemsButton);

            return this;
        }

        public CaseRecordPage TapRestrictAccessButton()
        {
            WaitForElementToBeClickable(restrictAccessButton);
            Click(restrictAccessButton);

            return this;
        }

        public CaseRecordPage TapRemoveRestrictionButton()
        {
            WaitForElementToBeClickable(removeRestrictionButton);
            Click(removeRestrictionButton);

            return this;
        }

        public CaseRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            return this;
        }

        public CaseRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndClose);
            Click(saveAndClose);

            return this;
        }

        public CaseRecordPage ValidateNotificationMessage(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(notificationMessageArea);
            else
                WaitForElementNotVisible(notificationMessageArea, 5);

            return this;
        }

        public CaseRecordPage ValidateNotificationMessageText(String ExpectedText)
        {

            ValidateElementTextContainsText(notificationMessageArea, ExpectedText);
            return this;
        }

        public CaseRecordPage ValidateContactRecievedByNotificationMessage(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(contactRecievedBy_NotificationErrorLabel);
            else
                WaitForElementNotVisible(contactRecievedBy_NotificationErrorLabel, 5);

            return this;
        }

        public CaseRecordPage ValidateContactRecievedByNotificationMessageText(String ExpectedText)
        {
            ValidateElementTextContainsText(contactRecievedBy_NotificationErrorLabel, ExpectedText);
            
            return this;
        }

        public CaseRecordPage ValidateContactReasonNotificationMessage(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(contactReason_NotificationErrorLabel);
            else
                WaitForElementNotVisible(contactReason_NotificationErrorLabel, 5);

            return this;
        }

        public CaseRecordPage ValidateContactReasonNotificationMessageText(String ExpectedText)
        {
            ValidateElementTextContainsText(contactReason_NotificationErrorLabel, ExpectedText);
            
            return this;
        }

        public CaseRecordPage ValidateCaseDateTime_DateNotificationMessage(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(caseDateAndTime_DateNotificationErrorLabel);
            else
                WaitForElementNotVisible(caseDateAndTime_DateNotificationErrorLabel, 5);

            return this;
        }

        public CaseRecordPage ValidateCaseDateAndTime_DateNotificationMessageText(String ExpectedText)
        {
            ValidateElementTextContainsText(caseDateAndTime_DateNotificationErrorLabel, ExpectedText);
            
            return this;
        }

        public CaseRecordPage ValidateCaseDateTime_TimeNotificationMessage(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(caseDateAndTime_TimeNotificationErrorLabel);
            else
                WaitForElementNotVisible(caseDateAndTime_TimeNotificationErrorLabel, 5);

            return this;
        }

        public CaseRecordPage ValidateCaseDateAndTime_TimeNotificationMessageText(String ExpectedText)
        {
            ValidateElementTextContainsText(caseDateAndTime_TimeNotificationErrorLabel, ExpectedText);
            
            return this;
        }

        public CaseRecordPage ValidateDateTimeContactReceived_DateNotificationMessage(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(dateTimeContactReceived_DateNotificationErrorLabel);
            else
                WaitForElementNotVisible(dateTimeContactReceived_DateNotificationErrorLabel, 5);

            return this;
        }

        public CaseRecordPage ValidateDateTimeContactReceived_DateNotificationMessageText(String ExpectedText)
        {
            ValidateElementTextContainsText(dateTimeContactReceived_DateNotificationErrorLabel, ExpectedText);
            
            return this;
        }

        public CaseRecordPage ValidateDateTimeContactReceived_TimeNotificationMessage(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(dateTimeContactReceived_TimeNotificationErrorLabel);
            else
                WaitForElementNotVisible(dateTimeContactReceived_TimeNotificationErrorLabel, 5);

            return this;
        }

        public CaseRecordPage ValidateDateTimeContactReceived_TimeNotificationMessageText(String ExpectedText)
        {
            ValidateElementTextContainsText(dateTimeContactReceived_TimeNotificationErrorLabel, ExpectedText);
            
            return this;
        }

        public CaseRecordPage ValidateIsThePersonAwareOfTheContactNotificationMessage(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(isThePersonAwareOfTheContact_NotificationErrorLabel);
            else
                WaitForElementNotVisible(isThePersonAwareOfTheContact_NotificationErrorLabel, 5);

            return this;
        }

        public CaseRecordPage ValidateIsThePersonAwareOfTheContactTimeNotificationMessageText(String ExpectedText)
        {
            ValidateElementTextContainsText(isThePersonAwareOfTheContact_NotificationErrorLabel, ExpectedText);
            
            return this;
        }

        public CaseRecordPage ClickContactReceivedByLookUpButton()
        {
            WaitForElementToBeClickable(contactRecievedBy_LookUpButton);
            Click(contactRecievedBy_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickContactReasonLookUpButton()
        {
            WaitForElementToBeClickable(contactReason_LookUpButton);
            Click(contactReason_LookUpButton);

            return this;
        }

        public CaseRecordPage InsertCaseDate(string TextToInsert)
        {
            WaitForElementToBeClickable(caseDateAndTime_DateField);
            MoveToElementInPage(caseDateAndTime_DateField);
            SendKeys(caseDateAndTime_DateField, TextToInsert);

            return this;
        }

        public CaseRecordPage InsertAdmissionDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElementToBeClickable(admissionDateTime_DateField);
            SendKeys(admissionDateTime_DateField, DateToInsert);
            SendKeysWithoutClearing(admissionDateTime_DateField, Keys.Tab);
            SendKeys(admissionDateTime_TimeField, TimeToInsert);

            return this;
        }

        public CaseRecordPage InsertAdmissionTime(string TextToInsert)
        {
            WaitForElementToBeClickable(admissionDateTime_DateField);
            SendKeys(admissionDateTime_DateField, TextToInsert);
            SendKeysWithoutClearing(admissionDateTime_DateField, Keys.Tab);

            return this;
        }

        public CaseRecordPage InsertCaseTime(string TextToInsert)
        {
            WaitForElementToBeClickable(caseDateAndTime_TimeField);
            MoveToElementInPage(caseDateAndTime_TimeField);
            Click(caseDateAndTime_TimeField);
            SendKeys(caseDateAndTime_TimeField, TextToInsert);

            return this;
        }

        public CaseRecordPage ClickPresentingPriorityLookUpButton()
        {
            WaitForElementToBeClickable(presentingPriority_LookUpButton);
            Click(presentingPriority_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickInitialContactLookUpButton()
        {
            WaitForElementToBeClickable(initialContact_LookUpButton);
            Click(initialContact_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickCINcodeLookUpButton()
        {
            WaitForElementToBeClickable(CINcode_LookUpButton);
            Click(CINcode_LookUpButton);

            return this;
        }

        public CaseRecordPage InsertDateContactReceived(string TextToInsert)
        {
            WaitForElementToBeClickable(dateTimeContactReceived_DateField);
            MoveToElementInPage(dateTimeContactReceived_DateField);
            Click(dateTimeContactReceived_DateField);
            SendKeys(dateTimeContactReceived_DateField, TextToInsert);
            SendKeysWithoutClearing(dateTimeContactReceived_DateField, Keys.Tab);

            return this;
        }

        public CaseRecordPage InsertTimeContactReceived(string TextToInsert)
        {
            WaitForElementToBeClickable(dateTimeContactReceived_TimeField);
            MoveToElementInPage(dateTimeContactReceived_TimeField);
            Click(dateTimeContactReceived_TimeField);
            SendKeys(dateTimeContactReceived_TimeField, TextToInsert);

            return this;
        }

        public CaseRecordPage InsertAdditionalInformation(string TextToInsert)
        {
            SendKeys(additionalInformation, TextToInsert);

            return this;
        }

        public CaseRecordPage ClickContactMadeByLookUpButton()
        {
            WaitForElementToBeClickable(contactMadeBy_LookUpButton);
            Click(contactMadeBy_LookUpButton);

            return this;
        }

        public CaseRecordPage SelectInpatientStatus(string TextToSelect)
        {
            WaitForElementToBeClickable(InpatientStatus_Picklist);
            SelectPicklistElementByText(InpatientStatus_Picklist, TextToSelect);

            return this;
        }

        public CaseRecordPage SelectCaseOrigin(string TextToSelect)
        {
            SelectPicklistElementByText(caseOrigin_Field, TextToSelect);

            return this;
        }

        public CaseRecordPage InsertContactMadeByFreeText(string TextToInsert)
        {
            SendKeys(contactMadeByFreeText_Field, TextToInsert);

            return this;
        }

        public CaseRecordPage ClickContactSourceLookUpButton()
        {
            WaitForElementToBeClickable(contactSource_LookUpButton);
            Click(contactSource_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickAdmissionSourceLookUpButton()
        {
            WaitForElementToBeClickable(admissionSource_LookUpButton);
            MoveToElementInPage(admissionSource_LookUpButton);
            Click(admissionSource_LookUpButton);

            return this;
        }

        public CaseRecordPage SelectIsThePersonAwareOfTheContact(string TextToSelect)
        {
            WaitForElementVisible(isThePersonAwareOfTheContact_Field);
            MoveToElementInPage(isThePersonAwareOfTheContact_Field);
            SelectPicklistElementByText(isThePersonAwareOfTheContact_Field, TextToSelect);

            return this;
        }

        public CaseRecordPage SelectIsParentCarerAwareOfThisContact(string TextToSelect)
        {
            SelectPicklistElementByText(isParentCarerAwareOfThisContact_Field, TextToSelect);

            return this;
        }

        public CaseRecordPage SelectDoesPersonAgreeSupportThisContact(string TextToSelect)
        {
            SelectPicklistElementByText(doesPersonAgreeSupportThisContact_Field, TextToSelect);

            return this;
        }

        public CaseRecordPage SelectDoesParentCarersAgreeSupportThisContact(string TextToSelect)
        {
            SelectPicklistElementByText(doesParentCarersAgreeSupportThisContact_Field, TextToSelect);

            return this;
        }

        public CaseRecordPage SelectNOKCarerAwareOfThisContact_Field(string TextToSelect)
        {
            SelectPicklistElementByText(nokCarerAwareOfThisContact_Field, TextToSelect);

            return this;
        }

        public CaseRecordPage ClickCasePriorityLookUpButton()
        {
            WaitForElementToBeClickable(casePriority_LookUpButton);
            Click(casePriority_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickAdmissionMethodLookUpButton()
        {
            WaitForElementToBeClickable(AdmissionMethod_LookUpButton);
            MoveToElementInPage(AdmissionMethod_LookUpButton);
            Click(AdmissionMethod_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickCurrentConsultantLookUpButton()
        {
            WaitForElementToBeClickable(CurrentConsultant_LookUpButton);
            MoveToElementInPage(CurrentConsultant_LookUpButton);
            Click(CurrentConsultant_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickHospitalLookUpButton()
        {
            WaitForElementToBeClickable(Hospital_LookUpButton);
            MoveToElementInPage(Hospital_LookUpButton);
            Click(Hospital_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickWardLookUpButton()
        {
            WaitForElementToBeClickable(Ward_LookUpButton);
            MoveToElementInPage(Ward_LookUpButton);
            Click(Ward_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickBayRoomLookUpButton()
        {
            WaitForElementToBeClickable(BayRoom_LookUpButton);
            MoveToElementInPage(BayRoom_LookUpButton);
            Click(BayRoom_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickBedLookUpButton()
        {
            WaitForElementToBeClickable(Bed_LookUpButton);
            MoveToElementInPage(Bed_LookUpButton);
            Click(Bed_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickResponsibleWardLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleWard_LookUpButton);
            MoveToElementInPage(ResponsibleWard_LookUpButton);
            Click(ResponsibleWard_LookUpButton);

            return this;
        }

        public CaseRecordPage SelectRTTReferral(string TextToSelect)
        {
            WaitForElementToBeClickable(RTTReferral_Picklist);
            SelectPicklistElementByText(RTTReferral_Picklist, TextToSelect);

            return this;
        }

        public CaseRecordPage ClickRTTTreatmentStatusLookUpButton()
        {
            WaitForElementToBeClickable(RTTTreatmentStatus_LookUpButton);
            MoveToElementInPage(RTTTreatmentStatus_LookUpButton);
            Click(RTTTreatmentStatus_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickRTTPathwayLookUpButton()
        {
            WaitForElementToBeClickable(RTTPathway_LookUpButton);
            MoveToElementInPage(RTTPathway_LookUpButton);
            Click(RTTPathway_LookUpButton);

            return this;
        }

        public CaseRecordPage InsertOutlineNeedForAdmission(string TextToInsert)
        {
            WaitForElementVisible(OutlineNeedForAdmission_Field);
            MoveToElementInPage(OutlineNeedForAdmission_Field);
            SendKeys(OutlineNeedForAdmission_Field, TextToInsert);

            return this;
        }

        public CaseRecordPage InsertReviewDate(string TextToInsert)
        {
            WaitForElementToBeClickable(reviewDate_Field);
            MoveToElementInPage(reviewDate_Field);
            Click(reviewDate_Field);
            SendKeys(reviewDate_Field, TextToInsert);

            return this;
        }

        public CaseRecordPage InsertReferrringAgencyCaseId(string TextToInsert)
        {
            SendKeys(referringAgencyCaseId_Field, TextToInsert);

            return this;
        }

        public CaseRecordPage InsertDateAndTimeOfContactWithTrainedStaff_Date(string TextToInsert)
        {
            WaitForElementToBeClickable(dateAndTimeOfContactWithTrainedStaff_DateField);
            MoveToElementInPage(dateAndTimeOfContactWithTrainedStaff_DateField);
            Click(dateAndTimeOfContactWithTrainedStaff_DateField);
            SendKeys(dateAndTimeOfContactWithTrainedStaff_DateField, TextToInsert);

            return this;
        }

        public CaseRecordPage InsertDateAndTimeOfContactWithTrainedStaff_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(dateAndTimeOfContactWithTrainedStaff_TimeField);
            MoveToElementInPage(dateAndTimeOfContactWithTrainedStaff_TimeField);
            Click(dateAndTimeOfContactWithTrainedStaff_TimeField);
            SendKeys(dateAndTimeOfContactWithTrainedStaff_TimeField, TextToInsert);

            return this;
        }

        public CaseRecordPage ClickfosteringExperienceLookUpButton()
        {
            WaitForElementToBeClickable(fosteringExperience_LookUpButton);
            Click(fosteringExperience_LookUpButton);

            return this;
        }

        public CaseRecordPage ValidatePersonFieldValue(string ExpectedText)
        {
            ValidateElementValue(person_Field, ExpectedText);

            return this;
        }

        public CaseRecordPage ValidateCaseNumberAutoPopulated(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(caseNumber_Field);
            else
                WaitForElementNotVisible(caseNumber_Field, 5);

            return this;
        }

        public CaseRecordPage ValidateCaseNumber(String ExpectedText)
        {
            ValidateElementTextContainsText(caseNumber_Field, ExpectedText);
            
            return this;
        }

        public CaseRecordPage ValidateCaseDate(String ExpectedText)
        {
            ValidateElementTextContainsText(caseDateAndTime_DateField, ExpectedText);
            
            return this;
        }

        public CaseRecordPage ValidateCaseTime(String ExpectedText)
        {
            ValidateElementTextContainsText(caseDateAndTime_TimeField, ExpectedText);
            
            return this;
        }

        public CaseRecordPage ValidateDateContactReceivedFieldDisabled(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(dateTimeContactReceived_DateField);
                ValidateElementDisabled(dateTimeContactReceived_DateField);
            }
            else
            {
                WaitForElementNotVisible(dateTimeContactReceived_DateField, 5);
            }

            return this;
        }

        public CaseRecordPage ValidateTimeContactReceivedFieldDisabled(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(dateTimeContactReceived_TimeField);
                ValidateElementDisabled(dateTimeContactReceived_TimeField);
            }
            else
            {
                WaitForElementNotVisible(dateTimeContactReceived_TimeField, 5);
            }

            return this;
        }

        public CaseRecordPage ValidateDoesPersonAgreeSupportThisContact_MandatoryField(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(doesPersonAgreeSupportThisContact_MandatoryField);
                ValidateElementEnabled(doesPersonAgreeSupportThisContact_MandatoryField);
            }
            else
            {
                WaitForElementNotVisible(doesPersonAgreeSupportThisContact_MandatoryField, 5);
            }

            return this;
        }

        public CaseRecordPage ValidateNokCarerAwareOfThisContact_MandatoryField(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(nokCarerAwareOfThisContact_MandatoryField);
                ValidateElementEnabled(nokCarerAwareOfThisContact_MandatoryField);
            }
            else
            {
                WaitForElementNotVisible(nokCarerAwareOfThisContact_MandatoryField, 5);
            }

            return this;
        }

        public CaseRecordPage ValidateCaseStatusLinkAutopopulated(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(caseStatus_LinkField);
                ValidateElementEnabled(caseStatus_LinkField);
            }
            else
            {
                WaitForElementNotVisible(caseStatus_LinkField, 5);
            }

            return this;
        }

        public CaseRecordPage ValidateResponsibleTeamLinkAutopopulated(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(responsibleTeam_LinkField);
                ValidateElementEnabled(responsibleTeam_LinkField);
            }
            else
            {
                WaitForElementNotVisible(responsibleTeam_LinkField, 5);
            }

            return this;
        }

        public CaseRecordPage ClickResponsibleUserLookupButton()
        {
            WaitForElementToBeClickable(responsibleUser_LookupButton);
            MoveToElementInPage(responsibleUser_LookupButton);
            Click(responsibleUser_LookupButton);

            return this;
        }

        public CaseRecordPage ValidateResponsibleUserLinkFieldText(string ExpectedText)
        {

            WaitForElement(responsibleUser_LinkField);
            MoveToElementInPage(responsibleUser_LinkField);
            ValidateElementText(responsibleUser_LinkField, ExpectedText);


            return this;
        }

        public CaseRecordPage ValidateResponsibleUserAutopopulated(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(responsibleUser_LinkField);
                ValidateElementEnabled(responsibleUser_LinkField);
            }
            else
            {
                WaitForElementNotVisible(responsibleUser_LinkField, 5);
            }

            return this;
        }

        public CaseRecordPage ValidateResponsibleTeamAutopopulated(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(responsibleTeam_LinkField);
                ValidateElementEnabled(responsibleTeam_LinkField);
            }
            else
            {
                WaitForElementNotVisible(responsibleTeam_LinkField, 5);
            }

            return this;
        }

        public CaseRecordPage ClickPoliceNotified_YesOption()
        {
            WaitForElementToBeClickable(policeNotified_YesOption);
            Click(policeNotified_YesOption);

            return this;
        }

        public CaseRecordPage ValidatePoliceNotifiedDateField_Displayed(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(policaNotifiedDate_Field);
                ValidateElementEnabled(policaNotifiedDate_Field);
            }
            else
            {
                WaitForElementNotVisible(policaNotifiedDate_Field, 5);
            }

            return this;
        }

        public CaseRecordPage ValidatePoliceNotesField_Displayed(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(policeNotes_Filed);
                ValidateElementEnabled(policeNotes_Filed);
            }
            else
            {
                WaitForElementNotVisible(policeNotes_Filed, 5);
            }

            return this;
        }

        public CaseRecordPage ClickCorrectErrors()
        {
            WaitForElementToBeClickable(correctErrors_Button);
            Click(correctErrors_Button);

            return this;
        }

        public CaseRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteRecord_Button);
            Click(deleteRecord_Button);

            return this;
        }

        public CaseRecordPage ClickActivateButton()
        {
            WaitForElementToBeClickable(additionalIItemsButton);
            Click(additionalIItemsButton);

            WaitForElementToBeClickable(activateButton);
            Click(activateButton);

            return this;
        }

        public CaseRecordPage ClickDischargeMethodLookUpButton()
        {
            WaitForElementToBeClickable(dischargeMethod_LookUpButton);
            MoveToElementInPage(dischargeMethod_LookUpButton);
            Click(dischargeMethod_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public CaseRecordPage InsertActualDischargeDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElementToBeClickable(actualDischargeDateTime_DateField);
            SendKeys(actualDischargeDateTime_DateField, DateToInsert);
            SendKeysWithoutClearing(actualDischargeDateTime_DateField, Keys.Tab);
            SendKeys(actualDischargeDateTime_TimeField, TimeToInsert);

            return this;
        }

        public CaseRecordPage ClickActualDischargeDestinationLookUpButton()
        {
            WaitForElementToBeClickable(actualDischargeDestination_LookUpButton);
            MoveToElementInPage(actualDischargeDestination_LookUpButton);
            Click(actualDischargeDestination_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickTeamResponsibleForFollowUpAppointmentLookUpButton()
        {
            WaitForElementToBeClickable(teamResponsibleForFollowUpAppointment_LookUpButton);
            MoveToElementInPage(teamResponsibleForFollowUpAppointment_LookUpButton);
            Click(teamResponsibleForFollowUpAppointment_LookUpButton);

            return this;
        }

        public CaseRecordPage ValidateInpatientStatusSelectedText(string ExpectedValue)
        {
            WaitForElement(InpatientStatus_Picklist);
            ValidatePicklistSelectedText(InpatientStatus_Picklist, ExpectedValue);

            return this;
        }

        public CaseRecordPage ValidateDischargeMethodLookupButtonDisabled(bool ExpectVisible)
        {
            WaitForElementVisible(dischargeMethod_LookUpButton);
            MoveToElementInPage(dischargeMethod_LookUpButton);

            if (ExpectVisible)
                ValidateElementDisabled(dischargeMethod_LookUpButton);
            else
                ValidateElementNotDisabled(dischargeMethod_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickPersonLookUpButton()
        {
            WaitForElementToBeClickable(person_LookUpButton);
            MoveToElementInPage(person_LookUpButton);
            Click(person_LookUpButton);

            return this;
        }

        public CaseRecordPage ValidateRTTReferralFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(RTTReferral_Picklist);
            MoveToElementInPage(RTTReferral_Picklist);

            if (IsDisabled)
                ValidateElementDisabled(RTTReferral_Picklist);
            else
                ValidateElementNotDisabled(RTTReferral_Picklist);

            return this;
        }

        public CaseRecordPage ValidateRTTReferralPicklist_FieldOptionIsPresent(string OptionName)
        {
            WaitForElementVisible(RTTReferral_Picklist);
            ValidatePicklistContainsElementByText(RTTReferral_Picklist, OptionName);

            return this;
        }

        public CaseRecordPage ValidateRTTReferralSelectedText(string ExpectedValue)
        {
            WaitForElementVisible(RTTReferral_Picklist);
            ValidatePicklistSelectedText(RTTReferral_Picklist, ExpectedValue);

            return this;
        }

        public CaseRecordPage ValidateRTTTreatmentStatusFieldVisibility(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(RTTTreatmentStatus_FieldLabel);
                WaitForElementVisible(RTTTreatmentStatus_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(RTTTreatmentStatus_FieldLabel, 3);
                WaitForElementNotVisible(RTTTreatmentStatus_LookUpButton, 3);
            }

            return this;
        }

        public CaseRecordPage ValidateRTTPathwayFieldVisibility(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(RTTPathway_FieldLabel);
                WaitForElementVisible(RTTPathway_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(RTTPathway_FieldLabel, 3);
                WaitForElementNotVisible(RTTPathway_LookUpButton, 3);
            }

            return this;
        }

        public CaseRecordPage ValidateTransferredFromProviderFieldVisibility(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(TransferredFromProvider_FieldLabel);
                WaitForElementVisible(TransferredFromProvider_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(TransferredFromProvider_FieldLabel, 3);
                WaitForElementNotVisible(TransferredFromProvider_LookUpButton, 3);
            }

            return this;
        }

        public CaseRecordPage ValidateOriginalRTTReferralStartDateFieldVisibility(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(OriginalRTTReferralStartDate_FieldLabel);
                WaitForElementVisible(OriginalRTTReferralStartDate_DateField);
            }
            else
            {
                WaitForElementNotVisible(OriginalRTTReferralStartDate_FieldLabel, 3);
                WaitForElementNotVisible(OriginalRTTReferralStartDate_DateField, 3);
            }

            return this;
        }

        public CaseRecordPage ValidateRTTTreatmentStatusFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(RTTTreatmentStatus_FieldLabel);
            MoveToElementInPage(RTTTreatmentStatus_LookUpButton);

            if (IsDisabled)
                ValidateElementDisabled(RTTTreatmentStatus_LookUpButton);
            else
                ValidateElementNotDisabled(RTTTreatmentStatus_LookUpButton);

            return this;
        }

        public CaseRecordPage ValidateRTTPathwayFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(RTTPathway_FieldLabel);
            MoveToElementInPage(RTTPathway_LookUpButton);

            if (IsDisabled)
                ValidateElementDisabled(RTTPathway_LookUpButton);
            else
                ValidateElementNotDisabled(RTTPathway_LookUpButton);

            return this;
        }

        public CaseRecordPage ValidateRTTTreatmentStatusLinkText(string ExpectedText)
        {
            WaitForElementVisible(RTTTreatmentStatus_LinkText);
            MoveToElementInPage(RTTTreatmentStatus_LinkText);
            ValidateElementText(RTTTreatmentStatus_LinkText, ExpectedText);

            return this;
        }

        public CaseRecordPage ValidateRTTPathwayLinkText(string ExpectedText)
        {
            WaitForElementVisible(RTTPathway_LinkText);
            MoveToElementInPage(RTTPathway_LinkText);
            ValidateElementText(RTTPathway_LinkText, ExpectedText);

            return this;
        }

        public CaseRecordPage ValidateTransferredFromProviderLinkText(string ExpectedText)
        {
            WaitForElementVisible(TransferredFromProvider_LinkText);
            MoveToElementInPage(TransferredFromProvider_LinkText);
            ValidateElementText(TransferredFromProvider_LinkText, ExpectedText);

            return this;
        }

        public CaseRecordPage ValidateOriginalRTTReferralStartDateFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(OriginalRTTReferralStartDate_DateField);
            MoveToElementInPage(OriginalRTTReferralStartDate_DateField);
            ValidateElementValue(OriginalRTTReferralStartDate_DateField, ExpectedValue);

            return this;
        }

        public CaseRecordPage ValidateRTTTreatmentStatus_MandatoryField(bool ExpectedText)
        {
            WaitForElementVisible(RTTTreatmentStatus_FieldLabel);
            MoveToElementInPage(RTTTreatmentStatus_FieldLabel);

            if (ExpectedText)
            {
                WaitForElementVisible(RTTTreatmentStatus_MandatoryField);
                ValidateElementEnabled(RTTTreatmentStatus_MandatoryField);
            }
            else
                WaitForElementNotVisible(RTTTreatmentStatus_MandatoryField, 5);

            return this;
        }

        public CaseRecordPage ValidateRTTPathway_MandatoryField(bool ExpectedText)
        {
            WaitForElementVisible(RTTPathway_FieldLabel);
            MoveToElementInPage(RTTPathway_FieldLabel);

            if (ExpectedText)
            {
                WaitForElementVisible(RTTPathway_MandatoryField);
                ValidateElementEnabled(RTTPathway_MandatoryField);
            }
            else
                WaitForElementNotVisible(RTTPathway_MandatoryField, 5);

            return this;
        }

        public CaseRecordPage ValidateTransferredFromProvider_MandatoryField(bool ExpectedText)
        {
            WaitForElementVisible(TransferredFromProvider_FieldLabel);
            MoveToElementInPage(TransferredFromProvider_FieldLabel);

            if (ExpectedText)
            {
                WaitForElementVisible(TransferredFromProvider_MandatoryField);
                ValidateElementEnabled(TransferredFromProvider_MandatoryField);
            }
            else
                WaitForElementNotVisible(TransferredFromProvider_MandatoryField, 5);

            return this;
        }

        public CaseRecordPage ValidateOriginalRTTReferralStartDate_MandatoryField(bool ExpectedText)
        {
            WaitForElementVisible(OriginalRTTReferralStartDate_FieldLabel);
            MoveToElementInPage(OriginalRTTReferralStartDate_FieldLabel);

            if (ExpectedText)
            {
                WaitForElementVisible(OriginalRTTReferralStartDate_MandatoryField);
                ValidateElementEnabled(OriginalRTTReferralStartDate_MandatoryField);
            }
            else
                WaitForElementNotVisible(OriginalRTTReferralStartDate_MandatoryField, 5);

            return this;
        }

        public CaseRecordPage ValidateTransferredFromProviderLookupIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(TransferredFromProvider_FieldLabel);
            MoveToElementInPage(TransferredFromProvider_LookUpButton);

            if (IsDisabled)
                ValidateElementDisabled(TransferredFromProvider_LookUpButton);
            else
                ValidateElementNotDisabled(TransferredFromProvider_LookUpButton);

            return this;
        }

        public CaseRecordPage ValidateOriginalRTTReferralStartDateFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(OriginalRTTReferralStartDate_FieldLabel);
            MoveToElementInPage(OriginalRTTReferralStartDate_DateField);

            if (IsDisabled)
                ValidateElementDisabled(OriginalRTTReferralStartDate_DateField);
            else
                ValidateElementNotDisabled(OriginalRTTReferralStartDate_DateField);

            return this;
        }

        public CaseRecordPage ClickTransferredFromProviderLookUpButton()
        {
            WaitForElementToBeClickable(TransferredFromProvider_LookUpButton);
            MoveToElementInPage(TransferredFromProvider_LookUpButton);
            Click(TransferredFromProvider_LookUpButton);

            return this;
        }

        public CaseRecordPage InsertOriginalRTTReferralStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(OriginalRTTReferralStartDate_DateField);
            SendKeys(OriginalRTTReferralStartDate_DateField, TextToInsert);
            SendKeysWithoutClearing(OriginalRTTReferralStartDate_DateField, Keys.Tab);

            return this;
        }

        public CaseRecordPage InsertDateRequestReceived(string TextToInsert)
        {
            WaitForElementToBeClickable(dateRequestReceived_DateField);
            MoveToElementInPage(dateRequestReceived_DateField);
            Click(dateRequestReceived_DateField);
            SendKeys(dateRequestReceived_DateField, TextToInsert);
            SendKeysWithoutClearing(dateRequestReceived_DateField, Keys.Tab);

            return this;
        }

        public CaseRecordPage ClickAdministrativeCategoryLookUpButton()
        {
            WaitForElementToBeClickable(administrativeCategory_LookUpButton);
            MoveToElementInPage(administrativeCategory_LookUpButton);
            Click(administrativeCategory_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickCommunityClinicTeamRequiredLookUpButton()
        {
            WaitForElementToBeClickable(communityClinicTeamRequired_LookUpButton);
            MoveToElementInPage(communityClinicTeamRequired_LookUpButton);
            Click(communityClinicTeamRequired_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickServiceTypeRequestedLookUpButton()
        {
            WaitForElementToBeClickable(serviceTypeRequested_LookUpButton);
            MoveToElementInPage(serviceTypeRequested_LookUpButton);
            Click(serviceTypeRequested_LookUpButton);

            return this;
        }

        public CaseRecordPage ClickDischargePerson_YesOption()
        {
            WaitForElementToBeClickable(dischargePerson_YesOption);
            MoveToElementInPage(dischargePerson_YesOption);
            Click(dischargePerson_YesOption);

            return this;
        }

        public CaseRecordPage ClickDischargePerson_NoOption()
        {
            WaitForElementToBeClickable(dischargePerson_NoOption);
            MoveToElementInPage(dischargePerson_NoOption);
            Click(dischargePerson_NoOption);

            return this;
        }

        public CaseRecordPage ValidateDischargeMethodFieldVisibility(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(dischargeMethod_LabelField);
                WaitForElementVisible(dischargeMethod_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(dischargeMethod_LabelField, 3);
                WaitForElementNotVisible(dischargeMethod_LookUpButton, 3);
            }

            return this;
        }

        public CaseRecordPage ValidateTransferToProviderFieldVisibility(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(transferToProvider_LabelField);
                WaitForElementVisible(transferToProvider_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(transferToProvider_LabelField, 3);
                WaitForElementNotVisible(transferToProvider_LookUpButton, 3);
            }

            return this;
        }

        public CaseRecordPage ClickTransferToProviderLookUpButton()
        {
            WaitForElementToBeClickable(transferToProvider_LookUpButton);
            MoveToElementInPage(transferToProvider_LookUpButton);
            Click(transferToProvider_LookUpButton);

            return this;
        }

        public CaseRecordPage ValidateDischargingProfessionalFieldVisibility(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(dischargingProfessional_LabelField);
                WaitForElementVisible(dischargingProfessional_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(dischargingProfessional_LabelField, 3);
                WaitForElementNotVisible(dischargingProfessional_LookUpButton, 3);
            }

            return this;
        }

        public CaseRecordPage ClickDischargingProfessionalLookUpButton()
        {
            WaitForElementToBeClickable(dischargingProfessional_LookUpButton);
            MoveToElementInPage(dischargingProfessional_LookUpButton);
            Click(dischargingProfessional_LookUpButton);

            return this;
        }
    }
}
