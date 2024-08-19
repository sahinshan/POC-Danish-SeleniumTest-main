using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CaseHealthAppointmentRecordPage : CommonMethods
    {

        public CaseHealthAppointmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=healthappointment')]");
        readonly By iframe_CWCaseNotes = By.Id("CWIFrame_CaseNotes");
        readonly By iframe_AdditionalProfessionals = By.Id("CWIFrame_AdditionalProfessionals");
        readonly By iframe_CWNewAppointmentDialog = By.Id("iframe_CWNewAppointmentDialog");
        readonly By iframe_CWDialog_CWEditAppointmentDialog = By.Id("iframe_CWDialog_CWEditAppointmentDialog");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");
        readonly By backButton = By.Id("BackButton");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By shareRecordButton = By.Id("TI_ShareRecordButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By errorCorrection = By.Id("TI_ErrorCorrection");
        readonly By restrictAccessButton = By.Id("TI_RestrictAccessButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By CaseNotesLink_LeftMenu = By.XPath("//*[@id='CWNavItem_CaseNotes']");

        #region Field Title

        readonly By AppointmentReason_FieldTitle = By.XPath("//*[@id='CWLabelHolder_healthappointmentreasonid']/label");

        #endregion

        #region Fields

        readonly By groupBooking_YesRadioButton = By.Id("CWField_groupbooking_1");
        readonly By groupBooking_NoRadioButton = By.Id("CWField_groupbooking_0");

        readonly By communityClinicTeam_LookupButton = By.Id("CWLookupBtn_communityandclinicteamid");
        readonly By communityClinicTeam_Field = By.Id("CWField_communityandclinicteamid_Link");
        readonly By appointmentReason_LinkField = By.XPath("//*[@id='CWField_healthappointmentreasonid_Link']");
        readonly By appointmentReason_RemoveButton = By.XPath("//*[@id='CWClearLookup_healthappointmentreasonid']");

        readonly By responsibleUserId_LookUpButton = By.Id("CWLookupBtn_responsibleuserid");
        readonly By contactTypeId_LookUpButton = By.Id("CWLookupBtn_contacttypeid");
        readonly By contactTypeId_LinkField = By.Id("CWField_contacttypeid_Link");
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By startTime_Field = By.Id("CWField_starttime");
        readonly By endDate_Field = By.Id("CWField_enddate");
        readonly By endTime_Field = By.Id("CWField_endtime");
        readonly By additionalProfessionalRequiredYesOption_radioButton = By.Id("CWField_additionalprofessionalrequired_1");
        readonly By advocateInAttendence_Field = By.Id("CWField_advocatesinattendance_cwname");
        readonly By advocateAttendee_LookUpButton = By.Id("CWLookupBtn_advocatesinattendance");
        readonly By locationTypes_LookUpButton = By.Id("CWLookupBtn_healthappointmentlocationtypeid");
        readonly By locationTypes_LinkField = By.Id("CWField_healthappointmentlocationtypeid_Link");
        readonly By leadProfessional_LookUpButton = By.Id("CWLookupBtn_healthprofessionalid");
        readonly By leadProfessional_LinkField = By.Id("CWField_healthprofessionalid_Link");
        readonly By healthAppointmentReason_LookUpButton = By.Id("CWLookupBtn_healthappointmentreasonid");
        readonly By relatedCase_LookUpButton = By.Id("CWLookupBtn_caseid");
        readonly By relatedCase_Field = By.Id("CWField_caseid_Link");


        readonly By outcome_SectionTitle = By.XPath("//*[@id='CWLabelHolder_healthappointmentoutcometypeid']/label");
        readonly By outcome_Field = By.Id("CWField_healthappointmentoutcometypeid_cwname");
        readonly By outcomeField_LookUpButton = By.Id("CWLookupBtn_healthappointmentoutcometypeid");
        readonly By careintervention_Field = By.Id("CWField_communitycliniccareinterventionid_cwname");
        readonly By careintervention_LookUpButton = By.Id("CWLookupBtn_communitycliniccareinterventionid");
        readonly By location_LookUpButton = By.Id("CWLookupBtn_providerid");
        readonly By location_LinkField = By.Id("CWField_providerid_Link");
        readonly By room_LookUpButton = By.Id("CWLookupBtn_providerroomid");

        readonly By additionalMenuOption_ToolBar = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By menu_Delete_Option = By.XPath("//*[@id='TI_DeleteRecordButton']");
        readonly By menu_RunWorkFlow_Option = By.Id("TI_RunOnDemandWorkflow");
        readonly By menu_CopyRecordLink_Options = By.Id("TI_CopyRecordLink");
        readonly By menu_Reschedule_Option = By.Id("TI_RescheduleButton");


        readonly By responsibleTeam_Field = By.Id("CWField_ownerid_Link");
        readonly By responsibleUser_Field = By.Id("CWField_responsibleuserid_Link");
        readonly By person_Field = By.Id("CWField_personid_Link");

        readonly By homeVisit_Section = By.Id("CWLabelHolder_homevisit");
        readonly By homeVisit_YesRadioButton = By.Id("CWField_homevisit_1");
        readonly By homeVisit_NoRadioButton = By.Id("CWField_homevisit_0");
        readonly By travelTimeForHealthProfessional = By.Id("CWSection_HPTravelTimeSection");
        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By healthAppointmentReason_NottificationText = By.XPath("//*[@id='CWControlHolder_healthappointmentreasonid']/label/span[text()='Please fill out this field.']");
        readonly By contactType_NottificationText = By.XPath("//*[@id='CWControlHolder_contacttypeid']/label/span[text()='Please fill out this field.']");
        readonly By startDate_NottificationText = By.XPath("//*[@id='CWControlHolder_startdate']/label/span[text() = 'Please fill out this field.']");
        readonly By startTime_NottificationText = By.XPath("//*[@id='CWControlHolder_starttime']/label/span[text() = 'Please fill out this field.']");
        readonly By endDate_NottificationText = By.XPath("//*[@id='CWControlHolder_enddate']/label/span[text() = 'Please fill out this field.']");
        readonly By endTime_NottificationText = By.XPath("//*[@id='CWControlHolder_endtime']/label/span[text() = 'Please fill out this field.']");
        readonly By location_NotificationText = By.XPath("//*[@id='CWControlHolder_healthappointmentlocationtypeid']/label/span[text()='Please fill out this field.']");

        readonly By selectAvailableSlots_Button = By.Id("CWFieldButton_SelectAvailableSlotsButton");

        readonly By bypassRestrictions_YesOption = By.Id("CWField_bypassrestrictions_1");
        readonly By bypassRestrictions_NoOption = By.Id("CWField_bypassrestrictions_0");

        #endregion

        #region Additional Professional

        readonly By additionalProfessionalRequiredYes_Button = By.Id("CWField_additionalprofessionalrequired_1");
        readonly By additionalProfessionalRequiredNo_Button = By.Id("CWField_additionalprofessionalrequired_0");
        readonly By addtionalProfessionalRequiredArea = By.Id("CWSection_AdditionalProfessionalsRequiredSection");
        readonly By additionalProfessionalRequiredAreaFrame = By.Id("CWIFrame_AdditionalProfessionals");
        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");
        
        By additionalProfessionalRecord(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id = '" + recordID + "']/td[2]");
        
        #endregion


        public CaseHealthAppointmentRecordPage ValidateCommunityClinicTeamLinkFieldText(string ExpectedText)
        {
            WaitForElementVisible(communityClinicTeam_Field);
            ValidateElementText(communityClinicTeam_Field, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateAppointmentReasonLinkFieldText(string ExpectedText)
        {
            WaitForElementVisible(appointmentReason_LinkField);
            ValidateElementText(appointmentReason_LinkField, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            WaitForElementVisible(responsibleTeam_Field);
            ValidateElementText(responsibleTeam_Field, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateResponsibleUserLinkFieldText(string ExpectedText)
        {
            WaitForElementVisible(responsibleUser_Field);
            ValidateElementText(responsibleUser_Field, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateContactTypeLinkFieldText(string ExpectedText)
        {
            WaitForElementVisible(contactTypeId_LinkField);
            ValidateElementText(contactTypeId_LinkField, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateStartDateFieldText(string ExpectedText)
        {
            WaitForElementVisible(startDate_Field);
            ValidateElementValue(startDate_Field, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateEndDateFieldText(string ExpectedText)
        {
            WaitForElementVisible(endDate_Field);
            ValidateElementValue(endDate_Field, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateStartTimeFieldText(string ExpectedText)
        {
            WaitForElementVisible(startTime_Field);
            ValidateElementValue(startTime_Field, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateEndTimeFieldText(string ExpectedText)
        {
            WaitForElementVisible(endTime_Field);
            ValidateElementValue(endTime_Field, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateLocationTypeLinkFieldText(string ExpectedText)
        {
            WaitForElementVisible(locationTypes_LinkField);
            ValidateElementText(locationTypes_LinkField, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateLocationLinkFieldText(string ExpectedText)
        {
            WaitForElementVisible(location_LinkField);
            ValidateElementText(location_LinkField, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateLeadProfessionalLinkFieldText(string ExpectedText)
        {
            WaitForElementVisible(leadProfessional_LinkField);
            ValidateElementText(leadProfessional_LinkField, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage WaitForCaseHealthAppointmentRecordPageToLoad()
        {
            SwitchToDefaultFrame();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(AppointmentReason_FieldTitle);

            return this;
        }

        public CaseHealthAppointmentRecordPage WaitForCaseHealthAppointmentRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDataFormDialog);
            SwitchToIframe(iframe_CWDataFormDialog);
            
            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(AppointmentReason_FieldTitle);

            return this;
        }

        public CaseHealthAppointmentRecordPage WaitForCaseHealthAppointmentRecordPage()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            return this;
        }

        public CaseHealthAppointmentRecordPage WaitForHealthAppointmentRecordPageToLoadFromHealthDiary(bool NewAppointmentRecord = true)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            if (NewAppointmentRecord)
            {
                WaitForElement(iframe_CWNewAppointmentDialog);
                SwitchToIframe(iframe_CWNewAppointmentDialog);
            }
            else
            {
                WaitForElement(iframe_CWDialog_CWEditAppointmentDialog);
                SwitchToIframe(iframe_CWDialog_CWEditAppointmentDialog);
            }

            WaitForElement(pageHeader);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(AppointmentReason_FieldTitle);

            return this;
        }

        public CaseHealthAppointmentRecordPage WaitForCaseHealthAppointmentRecordPageWithCaseNotes()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(iframe_CWCaseNotes);
            SwitchToIframe(iframe_CWCaseNotes);

            return this;
        }

        public CaseHealthAppointmentRecordPage WaitForCaseHealthAppointmentRecordPageWithAdditionalProfessionalRequiredSection()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(iframe_AdditionalProfessionals);
            SwitchToIframe(iframe_AdditionalProfessionals);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateAllAutoPopulatedFields(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(responsibleTeam_Field);
                WaitForElementVisible(responsibleUser_Field);
                WaitForElementVisible(communityClinicTeam_Field);
                WaitForElementVisible(relatedCase_Field);
                WaitForElementVisible(person_Field);
            }
            else
            {
                WaitForElementNotVisible(responsibleTeam_Field, 5);
                WaitForElementNotVisible(responsibleUser_Field, 5);
                WaitForElementNotVisible(communityClinicTeam_Field, 5);
                WaitForElementNotVisible(relatedCase_Field, 5);
                WaitForElementNotVisible(person_Field, 5);
            }

            return this;
        }

        public CaseHealthAppointmentRecordPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(backButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(noRecordMessage);
            else
                WaitForElementNotVisible(noRecordMessage, 5);
            
            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateSelectAvailableSlotButtonVisibile(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(selectAvailableSlots_Button);
            else
                WaitForElementNotVisible(selectAvailableSlots_Button, 5);
            
            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateMessageAreaTextVisibileForFields(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(healthAppointmentReason_NottificationText);
                WaitForElementVisible(contactType_NottificationText);
                WaitForElementVisible(startDate_NottificationText);
                WaitForElementVisible(startTime_NottificationText);
                WaitForElementVisible(endDate_NottificationText);
                WaitForElementVisible(endTime_NottificationText);
                WaitForElementVisible(location_NotificationText);

            }
            else
            {
                WaitForElementNotVisible(healthAppointmentReason_NottificationText, 5);
                WaitForElementNotVisible(contactType_NottificationText, 5);
                WaitForElementNotVisible(startDate_NottificationText, 5);
                WaitForElementNotVisible(startTime_NottificationText, 5);
                WaitForElementNotVisible(endDate_NottificationText, 5);
                WaitForElementNotVisible(endTime_NottificationText, 5);
                WaitForElementNotVisible(location_NotificationText, 5);
            }
            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateGroupBookingSelectedOption(bool YesOptionSelected)
        {
            if (YesOptionSelected)
            {
                ValidateElementChecked(groupBooking_YesRadioButton);
                ValidateElementNotChecked(groupBooking_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(groupBooking_YesRadioButton);
                ValidateElementChecked(groupBooking_NoRadioButton);
            }

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateHomeVisitSectionVisibile(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(homeVisit_Section);
            else
                WaitForElementNotVisible(homeVisit_Section, 5);
            
            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateHomeVisitSelectedOption(bool HomeVisit)
        {
            if (HomeVisit)
            {
                ValidateElementChecked(homeVisit_YesRadioButton);
                ValidateElementNotChecked(homeVisit_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(homeVisit_YesRadioButton);
                ValidateElementChecked(homeVisit_NoRadioButton);
            }

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateTravelSectionForHPVisibile(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(travelTimeForHealthProfessional);
            else
                WaitForElementNotVisible(travelTimeForHealthProfessional, 5);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateMenuOptionsForUnscheduledAppointments_ToolBarButton()
        {
            WaitForElementToBeClickable(additionalMenuOption_ToolBar);
            Click(additionalMenuOption_ToolBar);

            WaitForElementVisible(menu_Delete_Option);
            WaitForElementVisible(menu_RunWorkFlow_Option);
            WaitForElementVisible(menu_CopyRecordLink_Options);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateMenuOptionsForAppointments_ToolBarButton()
        {
            WaitForElementToBeClickable(additionalMenuOption_ToolBar);
            MoveToElementInPage(additionalMenuOption_ToolBar);
            Click(additionalMenuOption_ToolBar);

            WaitForElementVisible(menu_Reschedule_Option);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickAdditionalProfessionalRequired_RadioButton()
        {
            WaitForElementVisible(additionalProfessionalRequiredYesOption_radioButton);
            WaitForElementToBeClickable(additionalProfessionalRequiredYesOption_radioButton);
            MoveToElementInPage(additionalProfessionalRequiredYesOption_radioButton);
            Click(additionalProfessionalRequiredYesOption_radioButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(additionalMenuOption_ToolBar);
            Click(additionalMenuOption_ToolBar);

            WaitForElementVisible(menu_Delete_Option);
            Click(menu_Delete_Option);

            return this;
        }

        public CaseHealthAppointmentRecordPage TapSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage TapSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage NavigateToCaseHHealthAppointmentCaseNotesArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(CaseNotesLink_LeftMenu);
            Click(CaseNotesLink_LeftMenu);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickCommunityClinicTeamLookupButton()
        {
            WaitForElementToBeClickable(communityClinicTeam_LookupButton);
            MoveToElementInPage(communityClinicTeam_LookupButton);
            Click(communityClinicTeam_LookupButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickResponsibleUserLookupButton()
        {
            WaitForElementToBeClickable(responsibleUserId_LookUpButton);
            MoveToElementInPage(responsibleUserId_LookUpButton);
            Click(responsibleUserId_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickContactTypeLookUpButton()
        {
            WaitForElementToBeClickable(contactTypeId_LookUpButton);
            MoveToElementInPage(contactTypeId_LookUpButton);
            Click(contactTypeId_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickGroupBookingYesRadioButton()
        {
            WaitForElementToBeClickable(groupBooking_YesRadioButton);
            MoveToElementInPage(groupBooking_YesRadioButton);
            Click(groupBooking_YesRadioButton);


            return this;
        }

        public CaseHealthAppointmentRecordPage ClickGroupBookingNoRadioButton()
        {
            WaitForElementToBeClickable(groupBooking_NoRadioButton);
            MoveToElementInPage(groupBooking_NoRadioButton);
            Click(groupBooking_NoRadioButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickHomeVisitYesRadioButton()
        {
            WaitForElementToBeClickable(homeVisit_YesRadioButton);
            MoveToElementInPage(homeVisit_YesRadioButton);
            Click(homeVisit_YesRadioButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickHomeVisitNoRadioButton()
        {
            WaitForElementToBeClickable(homeVisit_NoRadioButton);
            MoveToElementInPage(homeVisit_NoRadioButton);
            Click(homeVisit_NoRadioButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickRoomLookUpButton()
        {
            WaitForElementToBeClickable(room_LookUpButton);
            MoveToElementInPage(room_LookUpButton);
            Click(room_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickLocationLookUpButton()
        {
            WaitForElementToBeClickable(location_LookUpButton);
            MoveToElementInPage(location_LookUpButton);
            Click(location_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickOutcomeLookUpButton()
        {
            WaitForElementToBeClickable(outcomeField_LookUpButton);
            MoveToElementInPage(outcomeField_LookUpButton);
            Click(outcomeField_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickCareInterventionLookUpButton()
        {
            WaitForElementToBeClickable(careintervention_LookUpButton);
            MoveToElementInPage(careintervention_LookUpButton);
            Click(careintervention_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickhealthAppointmentReasonLookUpButton()
        {
            WaitForElementToBeClickable(healthAppointmentReason_LookUpButton);
            MoveToElementInPage(healthAppointmentReason_LookUpButton);
            Click(healthAppointmentReason_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickRelatedCaseLookUpButton()
        {
            WaitForElementToBeClickable(relatedCase_LookUpButton);
            MoveToElementInPage(relatedCase_LookUpButton);
            Click(relatedCase_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage InsertStartDate(string TextToInsert)
        {
            WaitForElementVisible(startDate_Field);
            MoveToElementInPage(startDate_Field);
            SendKeys(startDate_Field, TextToInsert);

            return this;
        }

        public CaseHealthAppointmentRecordPage InsertStartTime(string TimeToInsert)
        {
            WaitForElementVisible(startTime_Field);
            SendKeys(startTime_Field, TimeToInsert);

            return this;
        }

        public CaseHealthAppointmentRecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElementVisible(endDate_Field);
            SendKeys(endDate_Field, TextToInsert);

            return this;
        }

        public CaseHealthAppointmentRecordPage InsertEndTime(string TextToInsert)
        {
            WaitForElementVisible(endTime_Field);
            SendKeys(endTime_Field, TextToInsert);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickAdvocateInAttendenceLookUpButton()
        {
            WaitForElementToBeClickable(advocateAttendee_LookUpButton);
            MoveToElementInPage(advocateAttendee_LookUpButton);
            Click(advocateAttendee_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickLocationTypesLookUpButton()
        {
            WaitForElementToBeClickable(locationTypes_LookUpButton);
            MoveToElementInPage(locationTypes_LookUpButton);
            Click(locationTypes_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickLeadProfessionalLookUpButton()
        {
            WaitForElementToBeClickable(leadProfessional_LookUpButton);
            MoveToElementInPage(leadProfessional_LookUpButton);
            Click(leadProfessional_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateOutcomeSection()
        {
            WaitForElementVisible(outcome_SectionTitle);
            WaitForElementVisible(outcome_Field);
            WaitForElementVisible(outcomeField_LookUpButton);
            WaitForElementVisible(careintervention_Field);
            WaitForElementVisible(careintervention_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage SelectAdditionalProfessionalRequired(bool status)
        {
            WaitForElementVisible(additionalProfessionalRequiredYes_Button);
            WaitForElementVisible(additionalProfessionalRequiredNo_Button);

            if (status)
                Click(additionalProfessionalRequiredYes_Button);
            else
                Click(additionalProfessionalRequiredNo_Button);

            return this;
        }

        public CaseHealthAppointmentRecordPage WaitForAdditionalProfessionalRequiredAreaToLoad()
        {
            WaitForElement(addtionalProfessionalRequiredArea);
            WaitForElement(additionalProfessionalRequiredAreaFrame);
            SwitchToIframe(additionalProfessionalRequiredAreaFrame);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickAddNewRecordButton()
        {
            WaitForElementToBeClickable(addNewRecordButton);
            ScrollToElement(addNewRecordButton);
            Click(addNewRecordButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage OpenCaseHealthAppointmentAdditionalProfessionalRecord(string RecordId)
        {
            WaitForElementToBeClickable(additionalProfessionalRecord(RecordId));
            MoveToElementInPage(additionalProfessionalRecord(RecordId));
            Click(additionalProfessionalRecord(RecordId));

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickBypassRestrictionsYesOption()
        {
            WaitForElementToBeClickable(bypassRestrictions_YesOption);
            MoveToElementInPage(bypassRestrictions_YesOption);
            Click(bypassRestrictions_YesOption);

            return this;
        }

        public CaseHealthAppointmentRecordPage ClickBypassRestrictionsNoOption()
        {
            WaitForElementToBeClickable(bypassRestrictions_NoOption);
            MoveToElementInPage(bypassRestrictions_NoOption);
            Click(bypassRestrictions_NoOption);

            return this;
        }

        public CaseHealthAppointmentRecordPage ValidateOutcomeLookupButtonDisabled(bool isDisabled)
        {
            WaitForElementVisible(outcomeField_LookUpButton);
            MoveToElementInPage(outcomeField_LookUpButton);

            if (isDisabled)
                ValidateElementDisabled(outcomeField_LookUpButton);
            else
                ValidateElementNotDisabled(outcomeField_LookUpButton);

            return this;
        }

        public CaseHealthAppointmentRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementVisible(shareRecordButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(errorCorrection);
            WaitForElementVisible(restrictAccessButton);

            return this;
        }
    }
}
