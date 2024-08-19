using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonAppointmentRecordPage : CommonMethods
    {

        public PersonAppointmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=appointment')]");



        readonly By messageArea = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        
        //contents rich textbox is inside his own iframe
        readonly By richTextBoxIframe = By.XPath("//iframe[@title='Rich Text Editor, CWField_notes']");



        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalIttemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By CompleteButton = By.Id("TI_CompleteButton");
        readonly By CancelButton = By.Id("TI_CancelButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");
        readonly By ActivateButton = By.Id("TI_ActivateButton");
        


        #region Field Title

        readonly By general_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div[1]/span[text()='General']");
        readonly By subject_FieldTitle = By.XPath("//*[@id='CWLabelHolder_subject']/label");
        readonly By Required_FieldTitle = By.XPath("//*[@id='CWLabelHolder_requiredattendees']/label");
        readonly By Optional_FieldTitle = By.XPath("//*[@id='CWLabelHolder_optionalattendees']/label");
        readonly By MeetingNotes_FieldTitle = By.XPath("//*[@id='CWLabelHolder_notes']/label");

        readonly By SchedulingInformation_SectionTitle = By.XPath("//*[@id='CWSection_SchedulingInformation']/fieldset/div[1]/span[text()='Scheduling Information']");
        readonly By StartDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By EndDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_enddate']/label");
        readonly By ShowTimeAs_FieldTitle = By.XPath("//*[@id='CWLabelHolder_showtimeasid']/label");
        readonly By StartTime_FieldTitle = By.XPath("//*[@id='CWLabelHolder_starttime']/label");
        readonly By EndTime_FieldTitle = By.XPath("//*[@id='CWLabelHolder_endtime']/label");
        readonly By AllowConcurrentAppointment_FieldTitle = By.XPath("//*[@id='CWLabelHolder_allowconcurrent']/label");

        readonly By details_SectionTitle = By.XPath("//*[@id='CWSection_Details']/fieldset/div[1]/span[text()='Details']");
        readonly By regarding_FieldTitle = By.XPath("//*[@id='CWLabelHolder_regardingid']/label");
        readonly By AppointmentType_FieldTitle = By.XPath("//*[@id='CWLabelHolder_appointmenttypeid']/label");
        readonly By Location_FieldTitle = By.XPath("//*[@id='CWLabelHolder_location']/label");
        readonly By priority_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activitypriorityid']/label");
        readonly By responsibleTeam_FieldTitle = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By responsibleUser_FieldTitle = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label");
        readonly By isCaseNote_FieldTitle = By.XPath("//*[@id='CWLabelHolder_iscasenote']/label");
        readonly By category_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activitycategoryid']/label");
        readonly By subCategory_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activitysubcategoryid']/label");
        readonly By reason_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activityreasonid']/label");
        readonly By outcome_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activityoutcomeid']/label");
        readonly By status_FieldTitle = By.XPath("//*[@id='CWLabelHolder_statusid']/label");
        readonly By containsInformationProvidedByThirdParty_FieldTitle = By.XPath("//*[@id='CWLabelHolder_informationbythirdparty']/label");
        
        readonly By significantEventDetails_SectionTitle = By.XPath("//*[@id='CWSection_SignificantEventDetails']/fieldset/div[1]/span[text()='Significant Event Details']");
        readonly By significantEvent_FieldTitle = By.XPath("//*[@id='CWLabelHolder_issignificantevent']/label");
        readonly By eventDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_significanteventdate']/label");
        readonly By eventCategory_FieldTitle = By.XPath("//*[@id='CWLabelHolder_significanteventcategoryid']/label");
        readonly By eventSubCategory_FieldTitle = By.XPath("//*[@id='CWLabelHolder_significanteventsubcategoryid']/label");

        #endregion


        #region Fields




        readonly By subject_Field = By.XPath("//*[@id='CWField_subject']");
        readonly By subject_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_subject']/label/span");
        readonly By Required_LookupButton = By.XPath("//*[@id='CWLookupBtn_requiredattendees']");
        By Required_Record(string RecordID) => By.XPath("//*[@id='MS_requiredattendees_" + RecordID + "']");
        By Required_RecordRemoveButton(string RecordID) => By.XPath("//*[@id='MS_requiredattendees_" + RecordID + "']/a");
        readonly By Required_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_requiredattendees']/label/span");
        readonly By Optional_LookupButton = By.XPath("//*[@id='CWLookupBtn_optionalattendees']");
        By Optional_Record(string RecordID) => By.XPath("//*[@id='MS_optionalattendees_" + RecordID + "']");
        By Optional_RecordRemoveButton(string RecordID) => By.XPath("//*[@id='MS_optionalattendees_" + RecordID + "']/a");
        readonly By MeetingNotes_TextAreaField = By.XPath("//*[@id='CWField_notes']");
        readonly string MeetingNotes_TextAreaFieldName = "CWField_notes";
        By MeetingNotes_Field(string LineNumber) => By.XPath("/html/body/p[" + LineNumber + "]");





        readonly By StartDate_Field = By.XPath("//*[@id='CWField_startdate']");
        readonly By StartDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_startdate']/label/span");
        readonly By EndDate_Field = By.XPath("//*[@id='CWField_enddate']");
        readonly By EndDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_enddate']/label/span");
        readonly By ShowTimeAs_Field = By.XPath("//*[@id='CWField_showtimeasid']");
        readonly By StartTime_Field = By.XPath("//*[@id='CWField_starttime']");
        readonly By StartTime_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_starttime']/label/span");
        readonly By EndTime_Field = By.XPath("//*[@id='CWField_endtime']");
        readonly By EndTime_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_endtime']/label/span");
        readonly By AllowConcurrentAppointment_YesRadioButton = By.XPath("//*[@id='CWField_allowconcurrent_1']");
        readonly By AllowConcurrentAppointment_NoRadioButton = By.XPath("//*[@id='CWField_allowconcurrent_0']");




        readonly By regarding_LinkField = By.XPath("//*[@id='CWField_regardingid_Link']");
        readonly By regarding_RemoveButton = By.XPath("//*[@id='CWClearLookup_regardingid']");
        readonly By regarding_LookupButton = By.XPath("//*[@id='CWLookupBtn_regardingid']");
        readonly By Regarding_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_regardingid']/label/span");

        readonly By AppointmentType_LinkField = By.XPath("//*[@id='CWField_appointmenttypeid_Link']");
        readonly By AppointmentType_RemoveButton = By.XPath("//*[@id='CWClearLookup_appointmenttypeid']");
        readonly By AppointmentType_LookupButton = By.XPath("//*[@id='CWLookupBtn_appointmenttypeid']");
        
        readonly By Location_Field = By.XPath("//*[@id='CWField_location']");

        readonly By priority_LinkField = By.XPath("//*[@id='CWField_activitypriorityid_Link']");
        readonly By priority_RemoveButton = By.XPath("//*[@id='CWClearLookup_activitypriorityid']");
        readonly By priority_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitypriorityid']");

        readonly By responsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By responsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");
        readonly By responsibleTeam_RemoveButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By responsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

        readonly By responsibleUser_LinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By responsibleUser_RemoveButton = By.XPath("//*[@id='CWClearLookup_responsibleuserid']");
        readonly By responsibleUser_LookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");

        readonly By isCaseNote_YesRadioButton = By.XPath("//*[@id='CWField_iscasenote_1']");
        readonly By isCaseNote_NoRadioButton = By.XPath("//*[@id='CWField_iscasenote_0']");

        readonly By category_LinkField = By.XPath("//*[@id='CWField_activitycategoryid_Link']");
        readonly By category_RemoveButton = By.XPath("//*[@id='CWClearLookup_activitycategoryid']");
        readonly By category_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitycategoryid']");

        readonly By subcategory_LinkField = By.XPath("//*[@id='CWField_activitysubcategoryid_Link']");
        readonly By subcategory_RemoveButton = By.XPath("//*[@id='CWClearLookup_activitysubcategoryid']");
        readonly By subcategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitysubcategoryid']");

        readonly By reason_LinkField = By.XPath("//*[@id='CWField_activityreasonid_Link']");
        readonly By reason_RemoveButton = By.XPath("//*[@id='CWClearLookup_activityreasonid']");
        readonly By reason_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityreasonid']");

        readonly By outcome_LinkField = By.XPath("//*[@id='CWField_activityoutcomeid_Link']");
        readonly By outcome_RemoveButton = By.XPath("//*[@id='CWClearLookup_activityoutcomeid']");
        readonly By outcome_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityoutcomeid']");

        readonly By status_Field = By.XPath("//*[@id='CWField_statusid']");
        readonly By status_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_statusid']/label/span");

        readonly By ContainsInformationProvidedByThirdParty_YesRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_1']");
        readonly By ContainsInformationProvidedByThirdParty_NoRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_0']");

        
        

        readonly By significantEvent_YesRadioButton = By.XPath("//*[@id='CWField_issignificantevent_1']");
        readonly By significantEvent_NoRadioButton = By.XPath("//*[@id='CWField_issignificantevent_0']");

        readonly By EventDate_Field = By.XPath("//*[@id='CWField_significanteventdate']");
        readonly By EventDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_significanteventdate']/label/span");

        readonly By eventCategory_LinkField = By.XPath("//*[@id='CWField_significanteventcategoryid_Link']");
        readonly By eventCategory_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_significanteventcategoryid']/label/span");
        readonly By eventCategory_RemoveButton = By.XPath("//*[@id='CWClearLookup_significanteventcategoryid']");
        readonly By eventCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_significanteventcategoryid']");

        readonly By eventSubCategory_LinkField = By.XPath("//*[@id='CWField_significanteventsubcategoryid_Link']");
        readonly By eventSubCategory_RemoveButton = By.XPath("//*[@id='CWClearLookup_significanteventsubcategoryid']");
        readonly By eventSubCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_significanteventsubcategoryid']");

        #endregion


        public PersonAppointmentRecordPage WaitForPersonAppointmentRecordPageToLoad(string RecordTitle)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(general_SectionTitle);
            this.WaitForElement(subject_FieldTitle);
            this.WaitForElement(Required_FieldTitle);
            this.WaitForElement(Optional_FieldTitle);
            this.WaitForElement(MeetingNotes_FieldTitle);

            this.WaitForElement(SchedulingInformation_SectionTitle);
            this.WaitForElement(StartDate_FieldTitle);
            this.WaitForElement(EndDate_FieldTitle);
            this.WaitForElement(ShowTimeAs_FieldTitle);
            this.WaitForElement(StartTime_FieldTitle);
            this.WaitForElement(EndTime_FieldTitle);
            this.WaitForElement(AllowConcurrentAppointment_FieldTitle);


            this.WaitForElement(details_SectionTitle);
            this.WaitForElement(regarding_FieldTitle);
            this.WaitForElement(AppointmentType_FieldTitle);
            this.WaitForElement(Location_FieldTitle);
            this.WaitForElement(priority_FieldTitle);
            this.WaitForElement(responsibleTeam_FieldTitle);
            this.WaitForElement(responsibleUser_FieldTitle);
            this.WaitForElement(isCaseNote_FieldTitle);
            this.WaitForElement(category_FieldTitle);
            this.WaitForElement(subCategory_FieldTitle);
            this.WaitForElement(reason_FieldTitle);
            this.WaitForElement(outcome_FieldTitle);
            this.WaitForElement(status_FieldTitle);
            this.WaitForElement(containsInformationProvidedByThirdParty_FieldTitle);
            
            this.WaitForElement(significantEventDetails_SectionTitle);
            this.WaitForElement(significantEvent_FieldTitle);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            ValidateElementText(pageHeader, "Appointment:\r\n" + RecordTitle);

            this.WaitForElementVisible(subject_Field);
            this.WaitForElementVisible(Required_LookupButton);


            return this;
        }
        public PersonAppointmentRecordPage WaitForDisabledPersonAppointmentRecordPageToLoad(string RecordTitle)
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);


            #region Wait For the elements to Load

            this.WaitForElement(pageHeader);

            this.WaitForElement(general_SectionTitle);
            this.WaitForElement(subject_FieldTitle);
            this.WaitForElement(Required_FieldTitle);
            this.WaitForElement(Optional_FieldTitle);
            this.WaitForElement(MeetingNotes_FieldTitle);

            this.WaitForElement(SchedulingInformation_SectionTitle);
            this.WaitForElement(StartDate_FieldTitle);
            this.WaitForElement(EndDate_FieldTitle);
            this.WaitForElement(ShowTimeAs_FieldTitle);
            this.WaitForElement(StartTime_FieldTitle);
            this.WaitForElement(EndTime_FieldTitle);
            this.WaitForElement(AllowConcurrentAppointment_FieldTitle);


            this.WaitForElement(details_SectionTitle);
            this.WaitForElement(regarding_FieldTitle);
            this.WaitForElement(AppointmentType_FieldTitle);
            this.WaitForElement(Location_FieldTitle);
            this.WaitForElement(priority_FieldTitle);
            this.WaitForElement(responsibleTeam_FieldTitle);
            this.WaitForElement(responsibleUser_FieldTitle);
            this.WaitForElement(isCaseNote_FieldTitle);
            this.WaitForElement(category_FieldTitle);
            this.WaitForElement(subCategory_FieldTitle);
            this.WaitForElement(reason_FieldTitle);
            this.WaitForElement(outcome_FieldTitle);
            this.WaitForElement(status_FieldTitle);
            this.WaitForElement(containsInformationProvidedByThirdParty_FieldTitle);

            this.WaitForElement(significantEventDetails_SectionTitle);
            this.WaitForElement(significantEvent_FieldTitle);


            this.WaitForElementRemoved(saveButton);
            this.WaitForElementRemoved(saveAndCloseButton);

            ValidateElementText(pageHeader, "Appointment:\r\n" + RecordTitle);

            this.WaitForElementVisible(subject_Field);
            this.WaitForElementVisible(Required_LookupButton);

            #endregion

            #region Validate that elements are disabled

            ValidateElementDisabled(subject_Field);
            ValidateElementDisabled(Required_LookupButton);
            ValidateElementDisabled(Optional_LookupButton);
            
            ValidateElementDisabled(StartDate_Field);
            ValidateElementDisabled(EndDate_Field);
            ValidateElementDisabled(ShowTimeAs_Field);
            ValidateElementDisabled(StartTime_Field);
            ValidateElementDisabled(EndTime_Field);
            ValidateElementDisabled(AllowConcurrentAppointment_YesRadioButton);
            ValidateElementDisabled(AllowConcurrentAppointment_NoRadioButton);
            
            ValidateElementDisabled(regarding_LookupButton);
            //ValidateElementDisabled(regarding_RemoveButton);
            ValidateElementDisabled(AppointmentType_LookupButton);
            //ValidateElementDisabled(AppointmentType_RemoveButton);
            ValidateElementDisabled(Location_Field);
            ValidateElementDisabled(priority_LookupButton);
            //ValidateElementDisabled(priority_RemoveButton);
            ValidateElementDisabled(responsibleTeam_LookupButton);
            //ValidateElementDisabled(responsibleTeam_RemoveButton);
            ValidateElementDisabled(responsibleUser_LookupButton);
            //ValidateElementDisabled(responsibleUser_RemoveButton);
            ValidateElementDisabled(isCaseNote_YesRadioButton);
            ValidateElementDisabled(isCaseNote_NoRadioButton);
            ValidateElementDisabled(category_LookupButton);
            //ValidateElementDisabled(category_RemoveButton);
            ValidateElementDisabled(subcategory_LookupButton);
            //ValidateElementDisabled(subcategory_RemoveButton);
            ValidateElementDisabled(reason_LookupButton);
            //ValidateElementDisabled(reason_RemoveButton);
            ValidateElementDisabled(outcome_LookupButton);
            //ValidateElementDisabled(outcome_RemoveButton);
            ValidateElementDisabled(status_Field);
            ValidateElementDisabled(ContainsInformationProvidedByThirdParty_YesRadioButton);
            ValidateElementDisabled(ContainsInformationProvidedByThirdParty_NoRadioButton);

            ValidateElementDisabled(significantEvent_YesRadioButton);
            ValidateElementDisabled(significantEvent_NoRadioButton);

            #endregion

            return this;
        }
        public PersonAppointmentRecordPage LoadMeetingNotesRichTextBox()
        {
            WaitForElement(richTextBoxIframe);
            SwitchToIframe(richTextBoxIframe);

            return this;
        }





        public PersonAppointmentRecordPage ValidateEventDateFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(eventDate_FieldTitle);
                WaitForElementVisible(EventDate_Field);
            }
            else
            {
                WaitForElementNotVisible(eventDate_FieldTitle, 3);
                WaitForElementNotVisible(EventDate_Field, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateEventCategoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(eventCategory_FieldTitle);
                WaitForElementVisible(eventCategory_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(eventCategory_FieldTitle, 3);
                WaitForElementNotVisible(eventCategory_LookupButton, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateEventSubCategoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(eventSubCategory_FieldTitle);
                WaitForElementVisible(eventSubCategory_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(eventSubCategory_FieldTitle, 3);
                WaitForElementNotVisible(eventSubCategory_LookupButton, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(messageArea);
            }
            else
            {
                WaitForElementNotVisible(messageArea, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateSubjectErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(subject_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(subject_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateRequiredErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Required_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Required_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateStartDateErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(StartDate_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(StartDate_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateEndDateErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(EndDate_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(EndDate_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateStartTimeErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(StartTime_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(StartTime_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateEndTimeErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(EndTime_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(EndTime_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateRegardingErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Regarding_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Regarding_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateResponsibleTeamErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(responsibleTeam_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(responsibleTeam_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateStatusErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(status_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(status_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateEventDateErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(EventDate_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(EventDate_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateEventCategoryErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(eventCategory_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(eventCategory_FieldErrorLabel, 3);
            }

            return this;
        }







        public PersonAppointmentRecordPage InsertSubject(string TextToInsert)
        {
            WaitForElementToBeClickable(subject_Field);
            SendKeys(subject_Field, TextToInsert);

            return this;
        }
        public PersonAppointmentRecordPage InsertMeetingNotes(string TextToInsert)
        {
            System.Threading.Thread.Sleep(1000);
            SetElementDisplayStyleToInline(MeetingNotes_TextAreaFieldName);
            SetElementVisibilityStyleToVisible(MeetingNotes_TextAreaFieldName);
            
            WaitForElementVisible(MeetingNotes_TextAreaField);
            SendKeys(MeetingNotes_TextAreaField, TextToInsert);

            return this;
        }
        public PersonAppointmentRecordPage InsertStartDate(string TextToInsert)
        {
            SendKeys(StartDate_Field, TextToInsert);

            return this;
        }
        public PersonAppointmentRecordPage InsertEndDate(string TextToInsert)
        {
            SendKeys(EndDate_Field, TextToInsert);

            return this;
        }
        public PersonAppointmentRecordPage InsertStartTime(string TextToInsert)
        {
            SendKeys(StartTime_Field, TextToInsert);

            return this;
        }
        public PersonAppointmentRecordPage InsertEndTime(string TextToInsert)
        {
            SendKeys(EndTime_Field, TextToInsert);

            return this;
        }
        public PersonAppointmentRecordPage InsertLocation(string TextToInsert)
        {
            SendKeys(Location_Field, TextToInsert);

            return this;
        }
        public PersonAppointmentRecordPage InsertEventDate(string DateToInsert)
        {
            SendKeys(EventDate_Field, DateToInsert);

            return this;
        }




        public PersonAppointmentRecordPage SelectShowTimeAs(string TextToSelect)
        {
            SelectPicklistElementByText(ShowTimeAs_Field, TextToSelect);

            return this;
        }
        public PersonAppointmentRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(status_Field, TextToSelect);

            return this;
        }



        public PersonAppointmentRecordPage ClickRequiredLookupButton()
        {
            Click(Required_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickRequiredRecordRemoveButton(string RecordID)
        {
            Click(Required_RecordRemoveButton(RecordID));

            return this;
        }
        public PersonAppointmentRecordPage ClickOptionalLookupButton()
        {
            Click(Optional_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickOptionalRecordRemoveButton(string RecordID)
        {
            Click(Optional_RecordRemoveButton(RecordID));

            return this;
        }
        public PersonAppointmentRecordPage ClickAllowConcurrentAppointmentYesRadioButton()
        {
            Click(AllowConcurrentAppointment_YesRadioButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickAllowConcurrentAppointmentNoRadioButton()
        {
            Click(AllowConcurrentAppointment_NoRadioButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickRegardingLookupButton()
        {
            Click(regarding_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickRegardingRemoveButton()
        {
            Click(regarding_RemoveButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickAppointmentTypeLookupButton()
        {
            Click(AppointmentType_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickAppointmentTypeRemoveButton()
        {
            Click(AppointmentType_RemoveButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickPriorityRemoveButton()
        {
            Click(priority_RemoveButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickPriorityLookupButton()
        {
            Click(priority_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickResponsibleTeamRemoveButton()
        {
            Click(responsibleTeam_RemoveButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(responsibleTeam_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickResponsibleUserRemoveButton()
        {
            Click(responsibleUser_RemoveButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickResponsibleUserLookupButton()
        {
            Click(responsibleUser_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickIsCaseNoteYesRadioButton()
        {
            Click(isCaseNote_YesRadioButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickIsCaseNoteNoRadioButton()
        {
            Click(isCaseNote_NoRadioButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickCategoryRemoveButton()
        {
            Click(category_RemoveButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickCategoryLookupButton()
        {
            Click(category_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickSubCategoryRemoveButton()
        {
            Click(subcategory_RemoveButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickSubCategoryLookupButton()
        {
            Click(subcategory_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickReasonRemoveButton()
        {
            Click(reason_RemoveButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickReasonLookupButton()
        {
            Click(reason_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickOutcomeRemoveButton()
        {
            Click(outcome_RemoveButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickOutcomeLookupButton()
        {
            Click(outcome_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickContainsInformationProvidedByThirdPartyYesRadioButton()
        {
            Click(ContainsInformationProvidedByThirdParty_YesRadioButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickContainsInformationProvidedByThirdPartyNoRadioButton()
        {
            Click(ContainsInformationProvidedByThirdParty_NoRadioButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickSignificantEventYesRadioButton()
        {
            Click(significantEvent_YesRadioButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickSignificantEventNoRadioButton()
        {
            Click(significantEvent_NoRadioButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickEventCategoryRemoveButton()
        {
            Click(eventCategory_RemoveButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickEventCategoryLookupButton()
        {
            Click(eventCategory_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickEventSubCategoryRemoveButton()
        {
            Click(eventSubCategory_RemoveButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickEventSubCategoryLookupButton()
        {
            Click(eventSubCategory_LookupButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }
        public PersonAppointmentRecordPage ClickCompleteButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(CompleteButton);
            Click(CompleteButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickCancelButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(CancelButton);
            Click(CancelButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public PersonAppointmentRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(DeleteButton);
            Click(DeleteButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickActivateButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(ActivateButton);
            Click(ActivateButton);

            return this;
        }
        public PersonAppointmentRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }




        public PersonAppointmentRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(messageArea, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateSubjectFieldText(string ExpectedText)
        {
            ValidateElementValue(subject_Field, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateRequiredRecordText(string RecordId, string ExpectedText)
        {
            ValidateElementTextContainsText(Required_Record(RecordId), ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateOptionalRecordText(string RecordId, string ExpectedText)
        {
            ValidateElementTextContainsText(Optional_Record(RecordId), ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateMeetingNotesFieldText(string LineNumber, string ExpectedText)
        {
            ValidateElementTextContainsText(MeetingNotes_Field(LineNumber), ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateStartDateFieldText(string ExpectedText)
        {
            ValidateElementValue(StartDate_Field, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateEndDateFieldText(string ExpectedText)
        {
            ValidateElementValue(EndDate_Field, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateShowTimeAsFieldText(string ExpectedText)
        {
            ValidatePicklistContainsElementByText(ShowTimeAs_Field, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateStartTimeFieldText(string ExpectedText)
        {
            ValidateElementValue(StartTime_Field, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateEndTimeFieldText(string ExpectedText)
        {
            ValidateElementValue(EndTime_Field, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateRegardingLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(regarding_LinkField, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateAppointmentTypeLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(AppointmentType_LinkField, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateLocationFieldText(string ExpectedText)
        {
            ValidateElementText(Location_Field, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidatePriorityLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(priority_LinkField, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(responsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateResponsibleUserLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(responsibleUser_LinkField, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(category_LinkField, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateSubCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(subcategory_LinkField, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateReasonLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(reason_LinkField, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateOutcomeLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(outcome_LinkField, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateSelectedStatus(string ExpectedText)
        {
            ValidatePicklistSelectedText(status_Field, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateEventDateText(string ExpectedText)
        {
            ValidateElementValue(EventDate_Field, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateEventCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(eventCategory_LinkField, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateEventSubCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementTextContainsText(eventSubCategory_LinkField, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateSubjectErrorLabelText(string ExpectedText)
        {
            ValidateElementText(subject_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateRequiredErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Required_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateStartDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(StartDate_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateEndDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EndDate_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateStartTimeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(StartTime_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateEndTimeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EndTime_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateRegardingErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Regarding_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateResponsibleTeamErrorLabelText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateStatusErrorLabelText(string ExpectedText)
        {
            ValidateElementText(status_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateEventDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EventDate_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAppointmentRecordPage ValidateEventCategoryErrorLabelText(string ExpectedText)
        {
            ValidateElementText(eventCategory_FieldErrorLabel, ExpectedText);

            return this;
        }


        public PersonAppointmentRecordPage ValidateAllowConcurrentAppointmentYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(AllowConcurrentAppointment_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(AllowConcurrentAppointment_YesRadioButton);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateAllowConcurrentAppointmentNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(AllowConcurrentAppointment_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(AllowConcurrentAppointment_NoRadioButton);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateIsCaseNoteYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(isCaseNote_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(isCaseNote_YesRadioButton);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateIsCaseNoteNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(isCaseNote_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(isCaseNote_NoRadioButton);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(ContainsInformationProvidedByThirdParty_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ContainsInformationProvidedByThirdParty_YesRadioButton);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(ContainsInformationProvidedByThirdParty_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ContainsInformationProvidedByThirdParty_NoRadioButton);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateSignificantEventYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(significantEvent_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(significantEvent_YesRadioButton);
            }

            return this;
        }
        public PersonAppointmentRecordPage ValidateSignificantEventNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(significantEvent_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(significantEvent_NoRadioButton);
            }

            return this;
        }

    }
}
