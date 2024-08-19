using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonFormAppointmentRecordPage : CommonMethods
    {

        public PersonFormAppointmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=appointment&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By PersonTopBanner_SummaryItem_CWInfoLeft = By.XPath("//*[@id='CWBannerHolder']/ul/li[@id='CWSummaryItem']/div[@id='CWInfoLeft']");
        readonly By PersonTopBanner_SummaryItem_CWInfoRight = By.XPath("//*[@id='CWBannerHolder']/ul/li[@id='CWSummaryItem']/div[@id='CWInfoRight']");
        readonly By PersonTopBanner_SummaryItem_ExpandButton = By.XPath("//*[@id='CWBannerHolder']/ul/li[@id='CWSummaryItem']/div[@id='CWButton']");


        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");



        #region Headers

        readonly By Subject_FieldHeader = By.XPath("//*[@id='CWLabelHolder_subject']/label[text()='Subject']");
        readonly By Required_FieldHeader = By.XPath("//*[@id='CWLabelHolder_requiredattendees']/label[text()='Required']");
        readonly By Optional_FieldHeader = By.XPath("//*[@id='CWLabelHolder_optionalattendees']/label[text()='Optional']");
        readonly By MeetingNotes_FieldHeader = By.XPath("//*[@id='CWLabelHolder_notes']/label[text()='Meeting Notes']");

        readonly By StartDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_startdate']/label[text()='Start Date']");
        readonly By EndDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_enddate']/label[text()='End Date']");
        readonly By ShowTimeAs_FieldHeader = By.XPath("//*[@id='CWLabelHolder_showtimeasid']/label[text()='Show Time As']");
        readonly By StartTime_FieldHeader = By.XPath("//*[@id='CWLabelHolder_starttime']/label[text()='Start Time']");
        readonly By EndTime_FieldHeader = By.XPath("//*[@id='CWLabelHolder_endtime']/label[text()='End Time']");
        readonly By AllowConcurrentAppointment_FieldHeader = By.XPath("//*[@id='CWLabelHolder_allowconcurrent']/label[text()='Allow Concurrent Appointment']");

        readonly By Regarding_FieldHeader = By.XPath("//*[@id='CWLabelHolder_regardingid']/label[text()='Regarding']");
        readonly By AppointmentType_FieldHeader = By.XPath("//*[@id='CWLabelHolder_appointmenttypeid']/label[text()='Appointment Type']");
        readonly By Location_FieldHeader = By.XPath("//*[@id='CWLabelHolder_location']/label[text()='Location']");
        readonly By Priority_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitypriorityid']/label[text()='Priority']");
        readonly By ResponsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By ResponsibleUser_FieldHeader = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label[text()='Responsible User']");
        readonly By IsCaseNote_FieldHeader = By.XPath("//*[@id='CWLabelHolder_iscasenote']/label[text()='Is Case Note?']");
        readonly By Category_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitycategoryid']/label[text()='Category']");
        readonly By SubCategory_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitysubcategoryid']/label[text()='Sub-Category']");
        readonly By Reason_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activityreasonid']/label[text()='Reason']");
        readonly By Outcome_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activityoutcomeid']/label[text()='Outcome']");
        readonly By Status_FieldHeader = By.XPath("//*[@id='CWLabelHolder_statusid']/label[text()='Status']");
        readonly By ContainsInformationProvidedByAThirdParty_FieldHeader = By.XPath("//*[@id='CWLabelHolder_informationbythirdparty']/label[text()='Contains Information Provided By A Third Party?']");

        readonly By SignificantEvent_FieldHeader = By.XPath("//*[@id='CWLabelHolder_issignificantevent']/label[text()='Significant Event?']");

        #endregion

        #region Fields

        readonly By Subject_Field = By.XPath("//*[@id='CWField_subject']");
        By Required_Field(string attendeeID) => By.XPath("//*[@id='MS_requiredattendees_" + attendeeID + "']");
        By Optional_Field(string attendeeID) => By.XPath("//*[@id='MS_optionalattendees_" + attendeeID + "']");
        readonly By MeetingNotes_Field = By.XPath("//*[@id='CWField_notes']");

        readonly By StartDate_Field = By.XPath("//*[@id='CWField_startdate']");
        readonly By EndDate_Field = By.XPath("//*[@id='CWField_enddate']");
        readonly By ShowTimeAs_Field = By.XPath("//*[@id='CWField_showtimeasid']");
        readonly By StartTime_Field = By.XPath("//*[@id='CWField_starttime']");
        readonly By EndTime_Field = By.XPath("//*[@id='CWField_endtime']");
        readonly By AllowConcurrentAppointment_YesRadioButton = By.XPath("//*[@id='CWField_allowconcurrent_1']");
        readonly By AllowConcurrentAppointment_NoRadioButton = By.XPath("//*[@id='CWField_allowconcurrent_0']");

        readonly By Regarding_LinkField = By.XPath("//*[@id='CWField_regardingid_Link']");
        readonly By Regarding_LookupButton = By.XPath("//*[@id='CWLookupBtn_regardingid']");
        readonly By AppointmentType_LinkField = By.XPath("//*[@id='CWField_appointmenttypeid_Link']");
        readonly By AppointmentType_LookupButton = By.XPath("//*[@id='CWLookupBtn_appointmenttypeid']");
        readonly By Location_Field = By.XPath("//*[@id='CWField_location']");
        readonly By Priority_LinkField = By.XPath("//*[@id='CWField_activitypriorityid_Link']");
        readonly By Priority_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitypriorityid']");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ResponsibleUser_LinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By ResponsibleUser_LookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
        readonly By IsCaseNote_YesRadioButton = By.XPath("//*[@id='CWField_iscasenote_1']");
        readonly By IsCaseNote_NoRadioButton = By.XPath("//*[@id='CWField_iscasenote_0']");
        readonly By Category_LinkField = By.XPath("//*[@id='CWField_activitycategoryid_Link']");
        readonly By Category_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitycategoryid']");
        readonly By SubCategory_LinkField = By.XPath("//*[@id='CWField_activitysubcategoryid_Link']");
        readonly By SubCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitysubcategoryid']");
        readonly By Reason_LinkField = By.XPath("//*[@id='CWField_activityreasonid_Link']");
        readonly By Reason_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityreasonid']");
        readonly By Outcome_LinkField = By.XPath("//*[@id='CWField_activityoutcomeid_Link']");
        readonly By Outcome_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityoutcomeid']");
        readonly By Status_Field = By.XPath("//*[@id='CWField_statusid']");
        readonly By ContainsInformationProvidedByAThirdParty_YesRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_1']");
        readonly By ContainsInformationProvidedByAThirdParty_NoRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_0']");

        readonly By SignificantEvent_YesRadioButton = By.XPath("//*[@id='CWField_issignificantevent_1']");
        readonly By SignificantEvent_NoRadioButton = By.XPath("//*[@id='CWField_issignificantevent_0']");

        #endregion






        public PersonFormAppointmentRecordPage WaitForPersonFormAppointmentRecordPageToLoad(string AppointmentTitle)
        {
            SwitchToDefaultFrame();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(PersonTopBanner_SummaryItem_CWInfoLeft);
            WaitForElement(PersonTopBanner_SummaryItem_CWInfoRight);
            WaitForElement(PersonTopBanner_SummaryItem_ExpandButton);


            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(Subject_FieldHeader);
            WaitForElement(Required_FieldHeader);
            WaitForElement(Optional_FieldHeader);
            WaitForElement(MeetingNotes_FieldHeader);

            WaitForElement(StartDate_FieldHeader);
            WaitForElement(EndDate_FieldHeader);
            WaitForElement(ShowTimeAs_FieldHeader);
            WaitForElement(StartTime_FieldHeader);
            WaitForElement(EndTime_FieldHeader);
            WaitForElement(AllowConcurrentAppointment_FieldHeader);


            WaitForElement(Regarding_FieldHeader);
            WaitForElement(AppointmentType_FieldHeader);
            WaitForElement(Location_FieldHeader);
            WaitForElement(Priority_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ResponsibleUser_FieldHeader);
            WaitForElement(IsCaseNote_FieldHeader);
            WaitForElement(Category_FieldHeader);
            WaitForElement(SubCategory_FieldHeader);
            WaitForElement(Reason_FieldHeader);
            WaitForElement(Outcome_FieldHeader);
            WaitForElement(Status_FieldHeader);
            WaitForElement(ContainsInformationProvidedByAThirdParty_FieldHeader);

            WaitForElement(SignificantEvent_FieldHeader);


            if (driver.FindElement(pageHeader).Text != "Appointment:\r\n" + AppointmentTitle)
                throw new Exception("Page title do not equals: Appointment: " + AppointmentTitle);

            return this;
        }


        public PersonFormAppointmentRecordPage ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NotificationMessage);
            else
                WaitForElementNotVisible(NotificationMessage, 3);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateNotificationMessageText(string ExpectText)
        {
            ValidateElementText(NotificationMessage, ExpectText);

            return this;
        }



        public PersonFormAppointmentRecordPage ValidateSubject(string ExpectedText)
        {
            ValidateElementValue(Subject_Field, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateRequired(string attendeeID, string ExpectedText)
        {
            ValidateElementText(Required_Field(attendeeID), ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateOptional(string attendeeID, string ExpectedText)
        {
            ValidateElementText(Optional_Field(attendeeID), ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateMeetingNotes(string ExpectedText)
        {
            SetElementDisplayStyleToInline("CWField_notes");
            SetElementVisibilityStyleToVisible("CWField_notes");

            ValidateElementValue(MeetingNotes_Field, ExpectedText);

            return this;
        }


        public PersonFormAppointmentRecordPage ValidateStartDate(string ExpectedText)
        {
            ValidateElementValue(StartDate_Field, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateEndDate(string ExpectedText)
        {
            ValidateElementValue(EndDate_Field, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateShowTimeAs(string ExpectedText)
        {
            ValidatePicklistSelectedText(ShowTimeAs_Field, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateStartTime(string ExpectedText)
        {
            ValidateElementValue(StartTime_Field, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateEndTime(string ExpectedText)
        {
            ValidateElementValue(EndTime_Field, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateAllowConcurrentAppointmentCheckedOption(bool YesOptionChecked)
        {
            if (YesOptionChecked)
            {
                ValidateElementChecked(AllowConcurrentAppointment_YesRadioButton);
                ValidateElementNotChecked(AllowConcurrentAppointment_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(AllowConcurrentAppointment_YesRadioButton);
                ValidateElementChecked(AllowConcurrentAppointment_NoRadioButton);
            }

            return this;
        }


        public PersonFormAppointmentRecordPage ValidateRegardingFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Regarding_LinkField, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateAppointmentTypeFieldLinkText(string ExpectedText)
        {
            ValidateElementText(AppointmentType_LinkField, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateLocation(string ExpectedText)
        {
            ValidateElementText(Location_Field, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidatePriorityFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Priority_LinkField, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateResponsibleTeamFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateResponsibleUserFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleUser_LinkField, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateIsCaseNoteCheckedOption(bool YesOptionChecked)
        {
            if (YesOptionChecked)
            {
                ValidateElementChecked(IsCaseNote_YesRadioButton);
                ValidateElementNotChecked(IsCaseNote_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(IsCaseNote_YesRadioButton);
                ValidateElementChecked(IsCaseNote_NoRadioButton);
            }

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateCategoryFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Category_LinkField, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateSubCategoryFieldLinkText(string ExpectedText)
        {
            ValidateElementText(SubCategory_LinkField, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateReasonFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Reason_LinkField, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateOutcomeFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Outcome_LinkField, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateStatus(string ExpectedText)
        {
            ValidatePicklistSelectedText(Status_Field, ExpectedText);

            return this;
        }
        public PersonFormAppointmentRecordPage ValidateContainsInformationProvidedByAThirdPartyCheckedOption(bool YesOptionChecked)
        {
            if (YesOptionChecked)
            {
                ValidateElementChecked(ContainsInformationProvidedByAThirdParty_YesRadioButton);
                ValidateElementNotChecked(ContainsInformationProvidedByAThirdParty_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ContainsInformationProvidedByAThirdParty_YesRadioButton);
                ValidateElementChecked(ContainsInformationProvidedByAThirdParty_NoRadioButton);
            }

            return this;
        }

        public PersonFormAppointmentRecordPage ValidateSignificantEventCheckedOption(bool YesOptionChecked)
        {
            if (YesOptionChecked)
            {
                ValidateElementChecked(SignificantEvent_YesRadioButton);
                ValidateElementNotChecked(SignificantEvent_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(SignificantEvent_YesRadioButton);
                ValidateElementChecked(SignificantEvent_NoRadioButton);
            }

            return this;
        }





        public PersonFormAppointmentRecordPage InsertSubject(string Subject)
        {
            SendKeys(Subject_Field, Subject);
            
            return this;
        }
        public PersonFormAppointmentRecordPage InsertDescription(string Subject)
        {
            SetElementDisplayStyleToInline("CWField_notes");
            SetElementVisibilityStyleToVisible("CWField_notes");

            SendKeys(MeetingNotes_Field, Subject);

            return this;
        }
        public PersonFormAppointmentRecordPage InsertStartDate(string Date)
        {
            SendKeys(StartDate_Field, Date);

            return this;
        }




        public PersonFormAppointmentRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(Status_Field, TextToSelect);

            return this;
        }


        public PersonFormAppointmentRecordPage ClickRegardingLookupButton()
        {
            Click(Regarding_LookupButton);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickReasonLookupButton()
        {
            Click(Reason_LookupButton);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickPriorityLookupButton()
        {
            Click(Priority_LookupButton);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(ResponsibleTeam_LookupButton);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickResponsibleUserLookupButton()
        {
            Click(ResponsibleUser_LookupButton);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickCategoryLookupButton()
        {
            Click(Category_LookupButton);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickSubCategoryLookupButton()
        {
            Click(SubCategory_LookupButton);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickOutcomeLookupButton()
        {
            Click(Outcome_LookupButton);

            return this;
        }



        public PersonFormAppointmentRecordPage ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
        {
            Click(ContainsInformationProvidedByAThirdParty_YesRadioButton);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickContainsInformationProvidedByAThirdParty_NoRadioButton()
        {
            Click(ContainsInformationProvidedByAThirdParty_NoRadioButton);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickSignificantEvent_YesRadioButton()
        {
            Click(SignificantEvent_YesRadioButton);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickSignificantEvent_NoRadioButton()
        {
            Click(SignificantEvent_NoRadioButton);

            return this;
        }



        public PersonFormAppointmentRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            return this;
        }
        public PersonFormAppointmentRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }

        public PersonFormAppointmentRecordPage ClickDeleteButton()
        {
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }


    }
}
