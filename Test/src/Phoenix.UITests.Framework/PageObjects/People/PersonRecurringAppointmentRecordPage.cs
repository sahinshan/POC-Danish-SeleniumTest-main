using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonRecurringAppointmentRecordPage : CommonMethods
    {

        public PersonRecurringAppointmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=recurringappointment')]");



        readonly By messageArea = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        
        //contents rich textbox is inside his own iframe
        readonly By richTextBoxIframe = By.XPath("//iframe[@title='Rich Text Editor, CWField_notes']");



        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalIttemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By CancelButton = By.Id("TI_CancelButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");
        


        #region Field Title

        readonly By general_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div[1]/span[text()='General']");
        readonly By subject_FieldTitle = By.XPath("//*[@id='CWLabelHolder_subject']/label");
        readonly By Required_FieldTitle = By.XPath("//*[@id='CWLabelHolder_requiredattendees']/label");
        readonly By Optional_FieldTitle = By.XPath("//*[@id='CWLabelHolder_optionalattendees']/label");
        readonly By MeetingNotes_FieldTitle = By.XPath("//*[@id='CWLabelHolder_notes']/label");

        readonly By SchedulingInformation_SectionTitle = By.XPath("//*[@id='CWSection_SchedulingInformation']/fieldset/div[1]/span[text()='Scheduling Information']");
        readonly By StartTime_FieldTitle = By.XPath("//*[@id='CWLabelHolder_starttime']/label");
        readonly By EndTime_FieldTitle = By.XPath("//*[@id='CWLabelHolder_endtime']/label");
        readonly By RecurrencePattern_FieldTitle = By.XPath("//*[@id='CWLabelHolder_recurrencepatternid']/label");
        readonly By ShowTimeAs_FieldTitle = By.XPath("//*[@id='CWLabelHolder_showtimeasid']/label");

        readonly By RangeOfRecurrence_SectionTitle = By.XPath("//*[@id='CWSection_RangeOfRecurrence']/fieldset/div/span[text()='Range of Recurrence']");
        readonly By StartDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_rangestartdate']/label");
        readonly By EndDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_rangeenddate']/label");
        readonly By FirstAppointmentStartDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_firstappointmentdate']/label");
        readonly By EndRange_FieldTitle = By.XPath("//*[@id='CWLabelHolder_endrangeid']/label");
        readonly By NumberOfOccurrences_FieldTitle = By.XPath("//*[@id='CWLabelHolder_numoccurrences']/label");
        readonly By LastAppointmentEndDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_lastappointmentdate']/label");

        readonly By details_SectionTitle = By.XPath("//*[@id='CWSection_Details']/fieldset/div/span[text()='Details']");
        readonly By regarding_FieldTitle = By.XPath("//*[@id='CWLabelHolder_regardingid']/label");
        readonly By AppointmentType_FieldTitle = By.XPath("//*[@id='CWLabelHolder_appointmenttypeid']/label");
        readonly By Location_FieldTitle = By.XPath("//*[@id='CWLabelHolder_location']/label");
        readonly By priority_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activitypriorityid']/label");
        readonly By responsibleTeam_FieldTitle = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By responsibleUser_FieldTitle = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label");
        readonly By category_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activitycategoryid']/label");
        readonly By subCategory_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activitysubcategoryid']/label");
        readonly By reason_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activityreasonid']/label");
        readonly By outcome_FieldTitle = By.XPath("//*[@id='CWLabelHolder_activityoutcomeid']/label");
        readonly By status_FieldTitle = By.XPath("//*[@id='CWLabelHolder_statusid']/label");
        
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



        readonly By StartTime_Field = By.XPath("//*[@id='CWField_starttime']");
        readonly By StartTime_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_starttime']/label/span");
        readonly By EndTime_Field = By.XPath("//*[@id='CWField_endtime']");
        readonly By EndTime_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_endtime']/label/span");
        readonly By RecurrencePattern_LinkField = By.XPath("//*[@id='CWField_recurrencepatternid_Link']");
        readonly By RecurrencePattern_RemoveButton = By.XPath("//*[@id='CWClearLookup_recurrencepatternid']");
        readonly By RecurrencePattern_LookupButton = By.XPath("//*[@id='CWLookupBtn_recurrencepatternid']");
        readonly By RecurrencePattern_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_recurrencepatternid']/label/span");
        readonly By ShowTimeAs_Field = By.XPath("//*[@id='CWField_showtimeasid']");


        readonly By StartDate_Field = By.XPath("//*[@id='CWField_rangestartdate']");
        readonly By StartDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_rangestartdate']/label/span");
        readonly By EndDate_Field = By.XPath("//*[@id='CWField_rangeenddate']");
        readonly By EndDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_rangeenddate']/label/span");
        readonly By FirstAppointmentStartDate_Field = By.XPath("//*[@id='CWField_firstappointmentdate']");
        readonly By EndRange_Field = By.XPath("//*[@id='CWField_endrangeid']");
        readonly By EndRange_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_endrangeid']/label/span");
        readonly By NumberOfOccurrences_Field = By.XPath("//*[@id='CWField_numoccurrences']");
        readonly By LastAppointmentEndDate_Field = By.XPath("//*[@id='CWField_lastappointmentdate']");


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
        
        #endregion


        public PersonRecurringAppointmentRecordPage WaitForPersonRecurringAppointmentRecordPageToLoad(string RecordTitle)
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
            this.WaitForElement(StartTime_FieldTitle);
            this.WaitForElement(EndTime_FieldTitle);
            this.WaitForElement(RecurrencePattern_FieldTitle);
            this.WaitForElement(ShowTimeAs_FieldTitle);

            this.WaitForElement(RangeOfRecurrence_SectionTitle);
            this.WaitForElement(StartDate_FieldTitle);
            this.WaitForElement(EndDate_FieldTitle);
            this.WaitForElement(FirstAppointmentStartDate_FieldTitle);
            this.WaitForElement(EndRange_FieldTitle);
            this.WaitForElement(NumberOfOccurrences_Field);
            this.WaitForElement(LastAppointmentEndDate_FieldTitle);

            this.WaitForElement(details_SectionTitle);
            this.WaitForElement(regarding_FieldTitle);
            this.WaitForElement(AppointmentType_FieldTitle);
            this.WaitForElement(Location_FieldTitle);
            this.WaitForElement(priority_FieldTitle);
            this.WaitForElement(responsibleTeam_FieldTitle);
            this.WaitForElement(responsibleUser_FieldTitle);
            this.WaitForElement(category_FieldTitle);
            this.WaitForElement(subCategory_FieldTitle);
            this.WaitForElement(reason_FieldTitle);
            this.WaitForElement(outcome_FieldTitle);
            this.WaitForElement(status_FieldTitle);
            
            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            ValidateElementText(pageHeader, "Recurring Appointment:\r\n" + RecordTitle);

            this.WaitForElementVisible(subject_Field);
            this.WaitForElementVisible(Required_LookupButton);
            this.WaitForElementVisible(Optional_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage WaitForCancelledPersonRecurringAppointmentRecordPageToLoad(string RecordTitle)
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
            this.WaitForElement(StartTime_FieldTitle);
            this.WaitForElement(EndTime_FieldTitle);
            this.WaitForElement(RecurrencePattern_FieldTitle);
            this.WaitForElement(ShowTimeAs_FieldTitle);

            this.WaitForElement(RangeOfRecurrence_SectionTitle);
            this.WaitForElement(StartDate_FieldTitle);
            this.WaitForElement(EndDate_FieldTitle);
            this.WaitForElement(FirstAppointmentStartDate_FieldTitle);
            this.WaitForElement(EndRange_FieldTitle);
            this.WaitForElement(NumberOfOccurrences_Field);
            this.WaitForElement(LastAppointmentEndDate_FieldTitle);

            this.WaitForElement(details_SectionTitle);
            this.WaitForElement(regarding_FieldTitle);
            this.WaitForElement(AppointmentType_FieldTitle);
            this.WaitForElement(Location_FieldTitle);
            this.WaitForElement(priority_FieldTitle);
            this.WaitForElement(responsibleTeam_FieldTitle);
            this.WaitForElement(responsibleUser_FieldTitle);
            this.WaitForElement(category_FieldTitle);
            this.WaitForElement(subCategory_FieldTitle);
            this.WaitForElement(reason_FieldTitle);
            this.WaitForElement(outcome_FieldTitle);
            this.WaitForElement(status_FieldTitle);

            this.WaitForElementRemoved(saveButton);
            this.WaitForElementRemoved(saveAndCloseButton);

            ValidateElementText(pageHeader, "Recurring Appointment:\r\n" + RecordTitle);

            this.WaitForElementVisible(subject_Field);
            this.WaitForElementVisible(Required_LookupButton);

            #endregion

            #region Validate that elements are disabled

            this.ValidateElementDisabled(subject_Field);
            this.ValidateElementDisabled(Required_LookupButton);
            this.ValidateElementDisabled(Optional_LookupButton);

            this.ValidateElementDisabled(StartTime_Field);
            this.ValidateElementDisabled(EndTime_Field);
            this.ValidateElementDisabled(RecurrencePattern_LookupButton);
            this.ValidateElementDisabled(ShowTimeAs_Field);
            
            this.ValidateElementDisabled(StartDate_Field);
            this.ValidateElementDisabled(EndDate_Field);
            this.ValidateElementDisabled(FirstAppointmentStartDate_Field);
            this.ValidateElementDisabled(EndRange_Field);
            this.ValidateElementDisabled(NumberOfOccurrences_Field);
            this.ValidateElementDisabled(LastAppointmentEndDate_Field);
            
            this.ValidateElementDisabled(regarding_LookupButton);
            this.ValidateElementDisabled(AppointmentType_LookupButton);
            this.ValidateElementDisabled(Location_Field);
            this.ValidateElementDisabled(priority_LookupButton);
            this.ValidateElementDisabled(responsibleTeam_LookupButton);
            this.ValidateElementDisabled(responsibleUser_LookupButton);
            this.ValidateElementDisabled(category_LookupButton);
            this.ValidateElementDisabled(subcategory_LookupButton);
            this.ValidateElementDisabled(reason_LookupButton);
            this.ValidateElementDisabled(outcome_LookupButton);
            this.ValidateElementDisabled(status_Field);

            #endregion

            return this;
        }
        public PersonRecurringAppointmentRecordPage LoadMeetingNotesRichTextBox()
        {
            WaitForElement(richTextBoxIframe);
            SwitchToIframe(richTextBoxIframe);

            return this;
        }





        
        public PersonRecurringAppointmentRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
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
        public PersonRecurringAppointmentRecordPage ValidateSubjectErrorLabelVisible(bool ExpectVisible)
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
        public PersonRecurringAppointmentRecordPage ValidateRequiredErrorLabelVisible(bool ExpectVisible)
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
        public PersonRecurringAppointmentRecordPage ValidateStartTimeErrorLabelVisible(bool ExpectVisible)
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
        public PersonRecurringAppointmentRecordPage ValidateEndTimeErrorLabelVisible(bool ExpectVisible)
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
        public PersonRecurringAppointmentRecordPage ValidateRecurrencePatternErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RecurrencePattern_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(RecurrencePattern_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateStartDateErrorLabelVisible(bool ExpectVisible)
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
        public PersonRecurringAppointmentRecordPage ValidateEndDateErrorLabelVisible(bool ExpectVisible)
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
        public PersonRecurringAppointmentRecordPage ValidateEndRangeErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(EndRange_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(EndRange_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateRegardingErrorLabelVisible(bool ExpectVisible)
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
        public PersonRecurringAppointmentRecordPage ValidateResponsibleTeamErrorLabelVisible(bool ExpectVisible)
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







        public PersonRecurringAppointmentRecordPage InsertSubject(string TextToInsert)
        {
            SendKeys(subject_Field, TextToInsert);

            return this;
        }
        public PersonRecurringAppointmentRecordPage InsertMeetingNotes(string TextToInsert)
        {
            System.Threading.Thread.Sleep(1000);
            SetElementDisplayStyleToInline(MeetingNotes_TextAreaFieldName);
            SetElementVisibilityStyleToVisible(MeetingNotes_TextAreaFieldName);
            
            WaitForElementVisible(MeetingNotes_TextAreaField);
            SendKeys(MeetingNotes_TextAreaField, TextToInsert);

            return this;
        }
        public PersonRecurringAppointmentRecordPage InsertStartTime(string TextToInsert)
        {
            SendKeys(StartTime_Field, TextToInsert);

            return this;
        }
        public PersonRecurringAppointmentRecordPage InsertEndTime(string TextToInsert)
        {
            SendKeys(EndTime_Field, TextToInsert);

            return this;
        }
        public PersonRecurringAppointmentRecordPage InsertStartDate(string TextToInsert)
        {
            SendKeys(StartDate_Field, TextToInsert);

            return this;
        }
        public PersonRecurringAppointmentRecordPage InsertEndDate(string TextToInsert)
        {
            SendKeys(EndDate_Field, TextToInsert);

            return this;
        }
        public PersonRecurringAppointmentRecordPage InsertFirstAppointmentStartDate(string TextToInsert)
        {
            SendKeys(FirstAppointmentStartDate_Field, TextToInsert);

            return this;
        }
        public PersonRecurringAppointmentRecordPage InsertNumberOfOccurrences(string TextToInsert)
        {
            SendKeys(NumberOfOccurrences_Field, TextToInsert);

            return this;
        }
        public PersonRecurringAppointmentRecordPage InsertLastAppointmentEndDate(string TextToInsert)
        {
            SendKeys(LastAppointmentEndDate_Field, TextToInsert);

            return this;
        }
        public PersonRecurringAppointmentRecordPage InsertLocation(string TextToInsert)
        {
            SendKeys(Location_Field, TextToInsert);

            return this;
        }




        public PersonRecurringAppointmentRecordPage SelectShowTimeAs(string TextToSelect)
        {
            SelectPicklistElementByText(ShowTimeAs_Field, TextToSelect);

            return this;
        }
        public PersonRecurringAppointmentRecordPage SelectEndRange(string TextToSelect)
        {
            SelectPicklistElementByText(EndRange_Field, TextToSelect);

            return this;
        }



        public PersonRecurringAppointmentRecordPage ClickRequiredLookupButton()
        {
            Click(Required_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickRequiredRecordRemoveButton(string RecordID)
        {
            Click(Required_RecordRemoveButton(RecordID));

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickOptionalLookupButton()
        {
            Click(Optional_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickOptionalRecordRemoveButton(string RecordID)
        {
            Click(Optional_RecordRemoveButton(RecordID));

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickRecurrencePatternLookupButton()
        {
            Click(RecurrencePattern_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickRecurrencePatternRemoveButton()
        {
            Click(RecurrencePattern_RemoveButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickRegardingLookupButton()
        {
            Click(regarding_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickRegardingRemoveButton()
        {
            Click(regarding_RemoveButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickAppointmentTypeLookupButton()
        {
            Click(AppointmentType_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickAppointmentTypeRemoveButton()
        {
            Click(AppointmentType_RemoveButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickPriorityRemoveButton()
        {
            Click(priority_RemoveButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickPriorityLookupButton()
        {
            Click(priority_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickResponsibleTeamRemoveButton()
        {
            Click(responsibleTeam_RemoveButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(responsibleTeam_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickResponsibleUserRemoveButton()
        {
            Click(responsibleUser_RemoveButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickResponsibleUserLookupButton()
        {
            Click(responsibleUser_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickCategoryRemoveButton()
        {
            Click(category_RemoveButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickCategoryLookupButton()
        {
            Click(category_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickSubCategoryRemoveButton()
        {
            Click(subcategory_RemoveButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickSubCategoryLookupButton()
        {
            Click(subcategory_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickReasonRemoveButton()
        {
            Click(reason_RemoveButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickReasonLookupButton()
        {
            Click(reason_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickOutcomeRemoveButton()
        {
            Click(outcome_RemoveButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickOutcomeLookupButton()
        {
            Click(outcome_LookupButton);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);
            
            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickCancelButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(CancelButton);
            Click(CancelButton);


            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(DeleteButton);
            Click(DeleteButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }




        public PersonRecurringAppointmentRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(messageArea, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateSubjectFieldText(string ExpectedText)
        {
            ValidateElementValue(subject_Field, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateRequiredRecordText(string RecordId, string ExpectedText)
        {
            ValidateElementText(Required_Record(RecordId), ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateOptionalRecordText(string RecordId, string ExpectedText)
        {
            ValidateElementText(Optional_Record(RecordId), ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateMeetingNotesFieldText(string LineNumber, string ExpectedText)
        {
            ValidateElementText(MeetingNotes_Field(LineNumber), ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateStartTimeFieldText(string ExpectedText)
        {
            ValidateElementValue(StartTime_Field, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateEndTimeFieldText(string ExpectedText)
        {
            ValidateElementValue(EndTime_Field, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateRecurrencePattern(string ExpectedText)
        {
            ValidateElementText(RecurrencePattern_LinkField, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateShowTimeAsFieldText(string ExpectedText)
        {
            ValidatePicklistSelectedText(ShowTimeAs_Field, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateStartDateFieldText(string ExpectedText)
        {
            ValidateElementValue(StartDate_Field, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateEndDateFieldText(string ExpectedText)
        {
            ValidateElementValue(EndDate_Field, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateFirstAppointmentStartDateFieldText(string ExpectedText)
        {
            ValidateElementValue(FirstAppointmentStartDate_Field, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateEndRangeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(EndRange_Field, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateNumberOfOccurrencesFieldText(string ExpectedText)
        {
            ValidateElementValue(NumberOfOccurrences_Field, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateLastAppointmentEndDateFieldText(string ExpectedText)
        {
            ValidateElementValue(LastAppointmentEndDate_Field, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateRegardingLinkFieldText(string ExpectedText)
        {
            ValidateElementText(regarding_LinkField, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateAppointmentTypeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(AppointmentType_LinkField, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateLocationFieldText(string ExpectedText)
        {
            ValidateElementText(Location_Field, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidatePriorityLinkFieldText(string ExpectedText)
        {
            ValidateElementText(priority_LinkField, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateResponsibleUserLinkFieldText(string ExpectedText)
        {
            ValidateElementText(responsibleUser_LinkField, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementText(category_LinkField, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateSubCategoryLinkFieldText(string ExpectedText)
        {
            ValidateElementText(subcategory_LinkField, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateReasonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(reason_LinkField, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateOutcomeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(outcome_LinkField, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateSelectedStatus(string ExpectedText)
        {
            ValidatePicklistSelectedText(status_Field, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateSubjectErrorLabelText(string ExpectedText)
        {
            ValidateElementText(subject_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateRequiredErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Required_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateStartTimeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(StartTime_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateEndTimeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EndTime_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateRecurrencePatternErrorLabelText(string ExpectedText)
        {
            ValidateElementText(RecurrencePattern_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateStartDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(StartDate_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateEndDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EndDate_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateEndRangeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EndRange_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateRegardingErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Regarding_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonRecurringAppointmentRecordPage ValidateResponsibleTeamErrorLabelText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }


        

    }
}
