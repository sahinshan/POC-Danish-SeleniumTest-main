using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonSpecificTrainingRecordPage : CommonMethods
    {
        public PersonSpecificTrainingRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceuserspecifictraining')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        /*General*/
        readonly By ContractidLink = By.XPath("//*[@id='CWField_contractid_Link']");
        readonly By ContractidLookupButton = By.XPath("//*[@id='CWLookupBtn_contractid']");
        readonly By TrainingitemidLink = By.XPath("//*[@id='CWField_trainingitemid_Link']");
        readonly By TrainingitemidLookupButton = By.XPath("//*[@id='CWLookupBtn_trainingitemid']");
        readonly By Trainingtypeid = By.XPath("//*[@id='CWField_trainingtypeid']");
        readonly By Trainingcourse = By.XPath("//*[@id='CWField_trainingcourse']");
        readonly By Validfrom = By.XPath("//*[@id='CWField_validfrom']");
        readonly By ValidfromDatePicker = By.XPath("//*[@id='CWField_validfrom_DatePicker']");
        readonly By Validto = By.XPath("//*[@id='CWField_validto']");
        readonly By ValidtoDatePicker = By.XPath("//*[@id='CWField_validto_DatePicker']");
        readonly By Trainingcompletedid = By.XPath("//*[@id='CWField_trainingcompletedid']");

        readonly By trainingdescription = By.XPath("//*[@id='CWField_trainingdescription']");

        readonly By InstructoridLink = By.XPath("//*[@id='CWField_instructorid_Link']");
        readonly By InstructoridClearButton = By.XPath("//*[@id='CWClearLookup_instructorid']");
        readonly By InstructoridLookupButton = By.XPath("//*[@id='CWLookupBtn_instructorid']");
        readonly By Recurrenceid = By.XPath("//*[@id='CWField_recurrenceid']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ResponsibleuseridLink = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By ResponsibleuseridClearButton = By.XPath("//*[@id='CWClearLookup_responsibleuserid']");
        readonly By ResponsibleuseridLookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
        readonly By Lengthofcourse = By.XPath("//*[@id='CWField_lengthofcourse']");
        readonly By Schedulingpreventorwarnid = By.XPath("//*[@id='CWField_schedulingpreventorwarnid']");

        /*Booking*/
        readonly By BookingtypeidLink = By.XPath("//*[@id='CWField_bookingtypeid_Link']");
        readonly By BookingtypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_bookingtypeid']");
        readonly By Coursestartdate = By.XPath("//*[@id='CWField_coursestartdate']");
        readonly By CoursestartdateDatePicker = By.XPath("//*[@id='CWField_coursestartdate_DatePicker']");
        readonly By Courseenddate = By.XPath("//*[@id='CWField_courseenddate']");
        readonly By CourseenddateDatePicker = By.XPath("//*[@id='CWField_courseenddate_DatePicker']");
        readonly By ProvideridLink = By.XPath("//*[@id='CWField_providerid_Link']");
        readonly By ProvideridClearButton = By.XPath("//*[@id='CWClearLookup_providerid']");
        readonly By ProvideridLookupButton = By.XPath("//*[@id='CWLookupBtn_providerid']");
        readonly By Outcomeid = By.XPath("//*[@id='CWField_outcomeid']");
        readonly By Statusid = By.XPath("//*[@id='CWField_statusid']");
        readonly By Coursestarttime = By.XPath("//*[@id='CWField_coursestarttime']");
        readonly By Coursestarttime_TimePicker = By.XPath("//*[@id='CWField_coursestarttime_TimePicker']");
        readonly By Courseendtime = By.XPath("//*[@id='CWField_courseendtime']");
        readonly By Courseendtime_TimePicker = By.XPath("//*[@id='CWField_courseendtime_TimePicker']");
        By StaffContractRecordLink(string RecordId) => By.XPath("//*[@id='" + RecordId + "_Link']");
        By StaffContractRecordRemoveButton(string RecordId) => By.XPath("//*[@id='MS_staffcontractid_" + RecordId + "']/a[2]");
        readonly By StaffcontractidLookupButton = By.XPath("//*[@id='CWLookupBtn_staffcontractid']");
        readonly By Expirydate = By.XPath("//*[@id='CWField_expirydate']");
        readonly By ExpirydateDatePicker = By.XPath("//*[@id='CWField_expirydate_DatePicker']");


        public PersonSpecificTrainingRecordPage WaitForPersonContractRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(pageHeader);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);

            WaitForElement(BackButton);
            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);
            WaitForElement(ContractidLookupButton);

            return this;
        }


        public PersonSpecificTrainingRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }



        public PersonSpecificTrainingRecordPage ClickPersonContractLink()
        {
            WaitForElementToBeClickable(ContractidLink);
            Click(ContractidLink);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidatePersonContractLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ContractidLink);
            ValidateElementText(ContractidLink, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickPersonContractLookupButton()
        {
            WaitForElementToBeClickable(ContractidLookupButton);
            Click(ContractidLookupButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickTrainingItemLink()
        {
            WaitForElementToBeClickable(TrainingitemidLink);
            Click(TrainingitemidLink);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateTrainingItemLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(TrainingitemidLink);
            ValidateElementText(TrainingitemidLink, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickTrainingItemLookupButton()
        {
            WaitForElementToBeClickable(TrainingitemidLookupButton);
            Click(TrainingitemidLookupButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage SelectTrainingType(string TextToSelect)
        {
            WaitForElementToBeClickable(Trainingtypeid);
            SelectPicklistElementByText(Trainingtypeid, TextToSelect);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateTrainingTypeSelectedText(string ExpectedText)
        {
            ValidateElementText(Trainingtypeid, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateTrainingCourseText(string ExpectedText)
        {
            ValidateElementValue(Trainingcourse, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage InsertTextOnTrainingCourse(string TextToInsert)
        {
            WaitForElementToBeClickable(Trainingcourse);
            SendKeys(Trainingcourse, TextToInsert);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateValidFromText(string ExpectedText)
        {
            ValidateElementValue(Validfrom, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage InsertTextOnValidFrom(string TextToInsert)
        {
            WaitForElementToBeClickable(Validfrom);
            SendKeys(Validfrom, TextToInsert);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickValidFromDatePicker()
        {
            WaitForElementToBeClickable(ValidfromDatePicker);
            Click(ValidfromDatePicker);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateValidToText(string ExpectedText)
        {
            ValidateElementValue(Validto, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage InsertTextOnValidTo(string TextToInsert)
        {
            WaitForElementToBeClickable(Validto);
            SendKeys(Validto, TextToInsert);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickValidToDatePicker()
        {
            WaitForElementToBeClickable(ValidtoDatePicker);
            Click(ValidtoDatePicker);

            return this;
        }

        public PersonSpecificTrainingRecordPage SelectTrainingCompleted(string TextToSelect)
        {
            WaitForElementToBeClickable(Trainingcompletedid);
            SelectPicklistElementByText(Trainingcompletedid, TextToSelect);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateTrainingCompletedSelectedText(string ExpectedText)
        {
            ValidateElementText(Trainingcompletedid, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage InsertTextOnTrainingDescription(string TextToInsert)
        {
            SetElementDisplayStyleToInline("CWField_trainingdescription");
            SetElementVisibilityStyleToVisible("CWField_trainingdescription");

            SendKeys(trainingdescription, TextToInsert);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickInstructorLink()
        {
            WaitForElementToBeClickable(InstructoridLink);
            Click(InstructoridLink);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateInstructorLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(InstructoridLink);
            ValidateElementText(InstructoridLink, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickInstructorClearButton()
        {
            WaitForElementToBeClickable(InstructoridClearButton);
            Click(InstructoridClearButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickInstructorLookupButton()
        {
            WaitForElementToBeClickable(InstructoridLookupButton);
            Click(InstructoridLookupButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage SelectRecurrence(string TextToSelect)
        {
            WaitForElementToBeClickable(Recurrenceid);
            SelectPicklistElementByText(Recurrenceid, TextToSelect);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateRecurrenceSelectedText(string ExpectedText)
        {
            ValidateElementText(Recurrenceid, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickResponsibleUserLink()
        {
            WaitForElementToBeClickable(ResponsibleuseridLink);
            Click(ResponsibleuseridLink);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateResponsibleUserLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleuseridLink);
            ValidateElementText(ResponsibleuseridLink, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickResponsibleUserClearButton()
        {
            WaitForElementToBeClickable(ResponsibleuseridClearButton);
            Click(ResponsibleuseridClearButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickResponsibleUserLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleuseridLookupButton);
            Click(ResponsibleuseridLookupButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateLengthOfCourseText(string ExpectedText)
        {
            ValidateElementValue(Lengthofcourse, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage InsertTextOnLengthOfCourse(string TextToInsert)
        {
            WaitForElementToBeClickable(Lengthofcourse);
            SendKeys(Lengthofcourse, TextToInsert);

            return this;
        }

        public PersonSpecificTrainingRecordPage SelectSchedulingPreventOrWarn(string TextToSelect)
        {
            WaitForElementToBeClickable(Schedulingpreventorwarnid);
            SelectPicklistElementByText(Schedulingpreventorwarnid, TextToSelect);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateSchedulingPreventOrWarnSelectedText(string ExpectedText)
        {
            ValidateElementText(Schedulingpreventorwarnid, ExpectedText);

            return this;
        }



        public PersonSpecificTrainingRecordPage ClickBookingTypeLink()
        {
            WaitForElementToBeClickable(BookingtypeidLink);
            Click(BookingtypeidLink);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateBookingTypeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(BookingtypeidLink);
            ValidateElementText(BookingtypeidLink, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickBookingTypeLookupButton()
        {
            WaitForElementToBeClickable(BookingtypeidLookupButton);
            Click(BookingtypeidLookupButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateCourseStartDateText(string ExpectedText)
        {
            ValidateElementValue(Coursestartdate, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage InsertTextOnCourseStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Coursestartdate);
            SendKeys(Coursestartdate, TextToInsert);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickCourseStartDateDatePicker()
        {
            WaitForElementToBeClickable(CoursestartdateDatePicker);
            Click(CoursestartdateDatePicker);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateCourseEndDateText(string ExpectedText)
        {
            ValidateElementValue(Courseenddate, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage InsertTextOnCourseEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Courseenddate);
            SendKeys(Courseenddate, TextToInsert);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickCourseEndDateDatePicker()
        {
            WaitForElementToBeClickable(CourseenddateDatePicker);
            Click(CourseenddateDatePicker);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickProviderLink()
        {
            WaitForElementToBeClickable(ProvideridLink);
            Click(ProvideridLink);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateProviderLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ProvideridLink);
            ValidateElementText(ProvideridLink, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickProviderClearButton()
        {
            WaitForElementToBeClickable(ProvideridClearButton);
            Click(ProvideridClearButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickProviderLookupButton()
        {
            WaitForElementToBeClickable(ProvideridLookupButton);
            Click(ProvideridLookupButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage SelectOutcome(string TextToSelect)
        {
            WaitForElementToBeClickable(Outcomeid);
            SelectPicklistElementByText(Outcomeid, TextToSelect);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateOutcomeSelectedText(string ExpectedText)
        {
            ValidateElementText(Outcomeid, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage SelectStatus(string TextToSelect)
        {
            WaitForElementToBeClickable(Statusid);
            SelectPicklistElementByText(Statusid, TextToSelect);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateStatusSelectedText(string ExpectedText)
        {
            ValidateElementText(Statusid, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateCourseStartTimeText(string ExpectedText)
        {
            ValidateElementValue(Coursestarttime, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage InsertTextOnCourseStartTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Coursestarttime);
            SendKeys(Coursestarttime, TextToInsert);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickCourseStartTime_TimePicker()
        {
            WaitForElementToBeClickable(Coursestarttime_TimePicker);
            Click(Coursestarttime_TimePicker);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateCourseEndTimeText(string ExpectedText)
        {
            ValidateElementValue(Courseendtime, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage InsertTextOnCourseEndTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Courseendtime);
            SendKeys(Courseendtime, TextToInsert);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickCourseEndTime_TimePicker()
        {
            WaitForElementToBeClickable(Courseendtime_TimePicker);
            Click(Courseendtime_TimePicker);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickStaffContractRecordLink(string RecordId)
        {
            WaitForElementToBeClickable(StaffContractRecordLink(RecordId));
            Click(StaffContractRecordLink(RecordId));

            return this;
        }

        

        public PersonSpecificTrainingRecordPage ValidateStaffContractRecordLinkText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(StaffContractRecordLink(RecordId));
            ValidateElementText(StaffContractRecordLink(RecordId), ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickStaffContractRecordRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(StaffContractRecordRemoveButton(RecordId));
            Click(StaffContractRecordRemoveButton(RecordId));

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickStaffContractsLookupButton()
        {
            WaitForElementToBeClickable(StaffcontractidLookupButton);
            Click(StaffcontractidLookupButton);

            return this;
        }

        public PersonSpecificTrainingRecordPage ValidateExpiryDateText(string ExpectedText)
        {
            ValidateElementValue(Expirydate, ExpectedText);

            return this;
        }

        public PersonSpecificTrainingRecordPage InsertTextOnExpiryDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Expirydate);
            SendKeys(Expirydate, TextToInsert);

            return this;
        }

        public PersonSpecificTrainingRecordPage ClickExpiryDateDatePicker()
        {
            WaitForElementToBeClickable(ExpirydateDatePicker);
            Click(ExpirydateDatePicker);

            return this;
        }

    }
}
