using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class BookingTypeRecordPage : CommonMethods
    {

        readonly By CWContent_Iframe = By.Id("CWContentIFrame");        
        readonly By availabilityTypes_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpbookingtype&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By DeleteButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
        readonly By AdditionalToolbarButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By Name = By.XPath("//*[@id='CWField_name']");
        readonly By UnallocatedDisplayColourId = By.XPath("//*[@id='CWField_unallocateddisplaycolourid']");
        readonly By CustomAllocatedDisplayColourId = By.XPath("//*[@id='CWField_customallocateddisplaycolourid']");
        readonly By DefaultStartTime = By.XPath("//*[@id='CWField_defaultstarttime']");
        readonly By DefaultStartTime_TimePicker = By.XPath("//*[@id='CWField_defaultstarttime_TimePicker']");
        readonly By Duration = By.XPath("//*[@id='CWField_duration']");
        readonly By BookingTypeClass = By.XPath("//*[@id='CWField_bookingtypeclassid']");
        readonly By UnallocatedColour = By.XPath("//*[@id='CWField_UnallocatedColour']");
        readonly By AllocatedColour = By.XPath("//*[@id='CWField_AllocatedColour']");
        readonly By DefaultEndTime = By.XPath("//*[@id='CWField_defaultendtime']");
        readonly By DefaultEndTime_TimePicker = By.XPath("//*[@id='CWField_defaultendtime_TimePicker']");
        readonly By BookingTypeShortCode = By.XPath("//*[@id='CWField_bookingtypeshortcode']");
        
        readonly By BookingChargeType = By.XPath("//*[@id='CWField_cpbookingchargetypeid']");
        readonly By Ispersonabsence_1 = By.XPath("//*[@id='CWField_ispersonabsence_1']");
        readonly By Ispersonabsence_0 = By.XPath("//*[@id='CWField_ispersonabsence_0']");
        readonly By ValidFromDate = By.XPath("//*[@id='CWField_validfromdate']");
        readonly By ValidFromDate_DatePicker = By.XPath("//*[@id='CWField_validfromdate_DatePicker']");
        readonly By ValidToDate = By.XPath("//*[@id='CWField_validtodate']");
        readonly By ValidToDate_DatePicker = By.XPath("//*[@id='CWField_validtodate_DatePicker']");

        readonly By WorkingContractedTime_FieldLabel = By.XPath("//*[@id='CWLabelHolder_bookingcountclassid']/label[text()='Working/Contracted Time']");
        readonly By WorkingContractedTime_Mandatory = By.XPath("//*[@id='CWLabelHolder_bookingcountclassid']/label/span[@class='mandatory']");
        readonly By WorkingContractedTime_PickList = By.Id("CWField_bookingcountclassid");

        readonly By DurationMinutes_FieldLabel = By.XPath("//*[@id='CWLabelHolder_capduration']/label[text()='Duration (minutes)']");
        readonly By DurationMinutes_Mandatory = By.XPath("//*[@id='CWLabelHolder_capduration']/label/span[@class='mandatory']");
        readonly By DurationMinutes_InputField = By.Id("CWField_capduration");
        readonly By DurationMinutes_FormError = By.XPath("//*[@id='CWControlHolder_capduration']/label/span");

        #region Staff - Non-Contact Time Section

        readonly By GeneralSection = By.XPath("//div[@id='CWTab_General']/div/div[1]//span[text()='General']");
        readonly By StaffSection = By.XPath("//div[@id='CWTab_General']/div/div[2]//span[text()='Staff']");

        readonly By StaffNonContactTimeSection = By.XPath("//*[@id = 'CWSection_NonContactTime']//span[text() = 'Staff - Non-Contact Time']");
        readonly By AssumeStaffAvailable_YesOption = By.XPath("//input[@id = 'CWField_assumestaffavailable_1']");
        readonly By AssumeStaffAvailable_NoOption = By.XPath("//input[@id = 'CWField_assumestaffavailable_0']");

        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");

        #endregion

        readonly By ClashActionTab = By.Id("CWNavGroup_CPBookingTypeClashAction"); 


        public BookingTypeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        public BookingTypeRecordPage WaitForBookingTypeRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElement(availabilityTypes_Iframe);
            SwitchToIframe(availabilityTypes_Iframe);            

            WaitForElementVisible(pageHeader);

            return this;
        }

        public BookingTypeRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public BookingTypeRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public BookingTypeRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public BookingTypeRecordPage ValidateNameText(string ExpectedText)
        {
            ValidateElementValue(Name, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage InsertTextOnName(string TextToInsert)
        {
            WaitForElementToBeClickable(Name);
            SendKeys(Name, TextToInsert);

            return this;
        }

        public BookingTypeRecordPage SelectUnallocatedDisplayColour(string TextToSelect)
        {
            WaitForElementToBeClickable(UnallocatedDisplayColourId);
            SelectPicklistElementByText(UnallocatedDisplayColourId, TextToSelect);

            return this;
        }

        public BookingTypeRecordPage ValidateUnallocatedDisplayColourSelectedText(string ExpectedText)
        {
            ValidateElementText(UnallocatedDisplayColourId, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage ValidateCustomAllocatedDisplayColourText(string ExpectedText)
        {
            ValidateElementValue(CustomAllocatedDisplayColourId, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage InsertTextOnCustomAllocatedDisplayColour(string TextToInsert)
        {
            WaitForElementToBeClickable(CustomAllocatedDisplayColourId);
            SendKeys(CustomAllocatedDisplayColourId, TextToInsert);

            return this;
        }

        public BookingTypeRecordPage ValidateDefaultStartTimeText(string ExpectedText)
        {
            ValidateElementValue(DefaultStartTime, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage InsertTextOnDefaultStartTime(string TextToInsert)
        {
            WaitForElementToBeClickable(DefaultStartTime);
            SendKeys(DefaultStartTime, TextToInsert);

            return this;
        }

        public BookingTypeRecordPage ClickDefaultStartTime_TimePicker()
        {
            WaitForElementToBeClickable(DefaultStartTime_TimePicker);
            Click(DefaultStartTime_TimePicker);

            return this;
        }

        public BookingTypeRecordPage ValidateDurationText(string ExpectedText)
        {
            ValidateElementValue(Duration, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage InsertTextOnDuration(string TextToInsert)
        {
            WaitForElementToBeClickable(Duration);
            SendKeys(Duration, TextToInsert);
            SendKeysWithoutClearing(Duration, Keys.Tab);

            return this;
        }

        public BookingTypeRecordPage SelectBookingTypeClass(string TextToSelect)
        {
            WaitForElementToBeClickable(BookingTypeClass);
            SelectPicklistElementByText(BookingTypeClass, TextToSelect);

            return this;
        }

        public BookingTypeRecordPage ValidateBookingTypeClassSelectedText(string ExpectedText)
        {
            WaitForElementVisible(BookingTypeClass);
            ValidatePicklistSelectedText(BookingTypeClass, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage ValidateUnallocatedColourText(string ExpectedText)
        {
            ValidateElementValue(UnallocatedColour, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage InsertTextOnUnallocatedColour(string TextToInsert)
        {
            WaitForElementToBeClickable(UnallocatedColour);
            SendKeys(UnallocatedColour, TextToInsert);

            return this;
        }

        public BookingTypeRecordPage ValidateAllocatedColourText(string ExpectedText)
        {
            ValidateElementValue(AllocatedColour, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage InsertTextOnAllocatedColour(string TextToInsert)
        {
            WaitForElementToBeClickable(AllocatedColour);
            SendKeys(AllocatedColour, TextToInsert);

            return this;
        }

        public BookingTypeRecordPage ValidateDefaultEndTimeText(string ExpectedText)
        {
            ValidateElementValue(DefaultEndTime, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage InsertTextOnDefaultendtime(string TextToInsert)
        {
            WaitForElementToBeClickable(DefaultEndTime);
            SendKeys(DefaultEndTime, TextToInsert);

            return this;
        }

        public BookingTypeRecordPage ClickDefaultEndTime_TimePicker()
        {
            WaitForElementToBeClickable(DefaultEndTime_TimePicker);
            Click(DefaultEndTime_TimePicker);

            return this;
        }

        public BookingTypeRecordPage ValidateBookingTypeShortCodeText(string ExpectedText)
        {
            ValidateElementValue(BookingTypeShortCode, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage InsertTextOnBookingTypeShortCode(string TextToInsert)
        {
            WaitForElementToBeClickable(BookingTypeShortCode);
            SendKeys(BookingTypeShortCode, TextToInsert);

            return this;
        }

        public BookingTypeRecordPage SelectWorkingContractedTime(string TextToSelect)
        {
            WaitForElementToBeClickable(WorkingContractedTime_PickList);
            ScrollToElement(WorkingContractedTime_PickList);
            SelectPicklistElementByText(WorkingContractedTime_PickList, TextToSelect);

            return this;
        }

        public BookingTypeRecordPage ValidateWorkingContractedTimeSelectedText(string ExpectedText)
        {
            WaitForElementVisible(WorkingContractedTime_PickList);
            ScrollToElement(WorkingContractedTime_PickList);
            ValidatePicklistSelectedText(WorkingContractedTime_PickList, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage SelectBookingChargeType(string TextToSelect)
        {
            WaitForElementToBeClickable(BookingChargeType);
            SelectPicklistElementByText(BookingChargeType, TextToSelect);

            return this;
        }

        public BookingTypeRecordPage ValidateBookingChargeTypeSelectedText(string ExpectedText)
        {
            ValidateElementText(BookingChargeType, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage ClickIspersonabsence_YesOption()
        {
            WaitForElementToBeClickable(Ispersonabsence_1);
            Click(Ispersonabsence_1);

            return this;
        }

        public BookingTypeRecordPage ValidateIspersonabsence_YesOptionChecked()
        {
            WaitForElement(Ispersonabsence_1);
            ValidateElementChecked(Ispersonabsence_1);

            return this;
        }

        public BookingTypeRecordPage ValidateIspersonabsence_YesOptionNotChecked()
        {
            WaitForElement(Ispersonabsence_1);
            ValidateElementNotChecked(Ispersonabsence_1);

            return this;
        }

        public BookingTypeRecordPage ClickIspersonabsence_NoOption()
        {
            WaitForElementToBeClickable(Ispersonabsence_0);
            Click(Ispersonabsence_0);

            return this;
        }

        public BookingTypeRecordPage ValidateIspersonabsence_NoOptionChecked()
        {
            WaitForElement(Ispersonabsence_0);
            ValidateElementChecked(Ispersonabsence_0);

            return this;
        }

        public BookingTypeRecordPage ValidateIspersonabsence_NoOptionNotChecked()
        {
            WaitForElement(Ispersonabsence_0);
            ValidateElementNotChecked(Ispersonabsence_0);

            return this;
        }

        public BookingTypeRecordPage ValidateValidFromDateText(string ExpectedText)
        {
            ValidateElementValue(ValidFromDate, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage InsertTextOnValidFromDate(string TextToInsert)
        {
            WaitForElementToBeClickable(ValidFromDate);
            SendKeys(ValidFromDate, TextToInsert);

            return this;
        }

        public BookingTypeRecordPage ClickValidFromDateDatePicker()
        {
            WaitForElementToBeClickable(ValidFromDate_DatePicker);
            Click(ValidFromDate_DatePicker);

            return this;
        }

        public BookingTypeRecordPage ValidateValidToDateText(string ExpectedText)
        {
            ValidateElementValue(ValidToDate, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage InsertTextOnValidToDate(string TextToInsert)
        {
            WaitForElementToBeClickable(ValidToDate);
            SendKeys(ValidToDate, TextToInsert);

            return this;
        }

        public BookingTypeRecordPage ClickValidToDateDatePicker()
        {
            WaitForElementToBeClickable(ValidToDate_DatePicker);
            Click(ValidToDate_DatePicker);

            return this;
        }

        public BookingTypeRecordPage ValidateStaffNonContactTimeSectionDisplayed(bool isDisplayed = true)
        {
            if (isDisplayed)
            {
                WaitForElement(StaffNonContactTimeSection);
                ScrollToElement(StaffNonContactTimeSection);
                GetElementVisibility(StaffNonContactTimeSection);
            }
            else
            {
                WaitForElementNotVisible(StaffNonContactTimeSection, 7);
            }

            return this;
        }

        //Validate Assume Staff Available Options Are Displayed
        public BookingTypeRecordPage ValidateAssumeStaffAvailable_OptionsAreDisplayed(bool IsDisplayed = true)
        {
            if(IsDisplayed)
            {
                WaitForElementVisible(StaffNonContactTimeSection);
                ScrollToElement(StaffNonContactTimeSection);
                WaitForElementVisible(AssumeStaffAvailable_YesOption);
                WaitForElementVisible(AssumeStaffAvailable_NoOption);
            }
            else
            {
                WaitForElementNotVisible(StaffNonContactTimeSection, 3);
                WaitForElementNotVisible(AssumeStaffAvailable_YesOption, 3);
                WaitForElementNotVisible(AssumeStaffAvailable_NoOption, 3);
            }


            return this;
        }

        public BookingTypeRecordPage ClickAssumeStaffAvailable_YesOption()
        {
            WaitForElementToBeClickable(AssumeStaffAvailable_YesOption);
            ScrollToElement(AssumeStaffAvailable_YesOption);
            Click(AssumeStaffAvailable_YesOption);

            return this;
        }

        public BookingTypeRecordPage ValidateAssumeStaffAvailable_YesOptionChecked()
        {
            WaitForElement(AssumeStaffAvailable_YesOption);
            ScrollToElement(AssumeStaffAvailable_YesOption);
            ValidateElementChecked(AssumeStaffAvailable_YesOption);

            return this;
        }

        public BookingTypeRecordPage ValidateAssumeStaffAvailable_YesOptionNotChecked()
        {
            WaitForElement(AssumeStaffAvailable_YesOption);
            ScrollToElement(AssumeStaffAvailable_YesOption);
            ValidateElementNotChecked(AssumeStaffAvailable_YesOption);

            return this;
        }

        public BookingTypeRecordPage ClickAssumeStaffAvailable_NoOption()
        {
            WaitForElementToBeClickable(AssumeStaffAvailable_NoOption);
            ScrollToElement(AssumeStaffAvailable_NoOption);
            Click(AssumeStaffAvailable_NoOption);

            return this;
        }

        public BookingTypeRecordPage ValidateAssumeStaffAvailable_NoOptionChecked()
        {
            WaitForElement(AssumeStaffAvailable_NoOption);
            ScrollToElement(AssumeStaffAvailable_NoOption);
            ValidateElementChecked(AssumeStaffAvailable_NoOption);

            return this;
        }

        public BookingTypeRecordPage ValidateAssumeStaffAvailable_NoOptionNotChecked()
        {
            WaitForElement(AssumeStaffAvailable_NoOption);
            ScrollToElement(AssumeStaffAvailable_NoOption);
            ValidateElementNotChecked(AssumeStaffAvailable_NoOption);

            return this;
        }

        public BookingTypeRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(DeleteButton);
            WaitForElementVisible(AdditionalToolbarButton);

            return this;
        }

        public BookingTypeRecordPage ValidateClashActionsTabVisibility(bool IsPresent)
        {
            if (IsPresent)
                WaitForElementVisible(ClashActionTab);
            else
                WaitForElementNotVisible(ClashActionTab, 3);

            return this;
        }

        public BookingTypeRecordPage NavigateToClashActionsTab()
        {
            WaitForElementToBeClickable(ClashActionTab);
            Click(ClashActionTab);

            return this;
        }

        public BookingTypeRecordPage ValidateWorkingContractedTimeFieldVisibility(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(WorkingContractedTime_FieldLabel);
                WaitForElementVisible(WorkingContractedTime_PickList);            }
            else
            {
                WaitForElementNotVisible(WorkingContractedTime_FieldLabel, 3);
                WaitForElementNotVisible(WorkingContractedTime_PickList, 3);
            }

            return this;
        }

        public BookingTypeRecordPage ValidateGeneralAndStaffSectionPosition()
        {
            WaitForElementVisible(GeneralSection);
            WaitForElementVisible(StaffSection);

            return this;
        }

        public BookingTypeRecordPage ValidateWorkingContractedTimeFieldIsMandatory(bool IsMandatory)
        {
            WaitForElementVisible(WorkingContractedTime_FieldLabel);
            ScrollToElement(WorkingContractedTime_FieldLabel);

            if (IsMandatory)
                WaitForElementVisible(WorkingContractedTime_Mandatory);
            else
                WaitForElementNotVisible(WorkingContractedTime_Mandatory, 5);

            return this;
        }

        public BookingTypeRecordPage ValidateWorkingContractedTimeDropDownText(string ExpectedText)
        {
            WaitForElementVisible(WorkingContractedTime_PickList);
            ScrollToElement(WorkingContractedTime_PickList);
            ValidatePicklistContainsElementByText(WorkingContractedTime_PickList, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage ValidateCapDurationMinutesFieldVisibility(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(DurationMinutes_FieldLabel);
                WaitForElementVisible(DurationMinutes_InputField);
            }
            else
            {
                WaitForElementNotVisible(DurationMinutes_FieldLabel, 3);
                WaitForElementNotVisible(DurationMinutes_InputField, 3);
            }

            return this;
        }

        public BookingTypeRecordPage ValidateCapDurationMinutesFieldIsMandatory(bool IsMandatory)
        {
            WaitForElementVisible(DurationMinutes_FieldLabel);
            ScrollToElement(DurationMinutes_FieldLabel);

            if (IsMandatory)
                WaitForElementVisible(DurationMinutes_Mandatory);
            else
                WaitForElementNotVisible(DurationMinutes_Mandatory, 5);

            return this;
        }

        public BookingTypeRecordPage InsertTextOnCapDurationMinutes(string TextToInsert)
        {
            WaitForElementToBeClickable(DurationMinutes_InputField);
            ScrollToElement(DurationMinutes_InputField);
            SendKeys(DurationMinutes_InputField, TextToInsert);
            SendKeysWithoutClearing(Duration, Keys.Tab);

            return this;
        }

        public BookingTypeRecordPage ValidateCapDurationMinutesText(string ExpectedText)
        {
            WaitForElementVisible(DurationMinutes_InputField);
            ScrollToElement(DurationMinutes_InputField);
            ValidateElementText(DurationMinutes_InputField, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage ValidateCapDurationMinutesFormErrorText(string ExpectedText)
        {
            WaitForElementVisible(DurationMinutes_FormError);
            ScrollToElement(DurationMinutes_FormError);
            ValidateElementText(DurationMinutes_FormError, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage ValidateNotificationMessageText(string ExpectedText)
        {
            WaitForElementVisible(NotificationMessage);
            ScrollToElement(NotificationMessage);
            ValidateElementText(NotificationMessage, ExpectedText);

            return this;
        }

        public BookingTypeRecordPage ValidatePageHeaderTitle(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            ValidateElementText(pageHeader, "Booking Type:\r\n" + ExpectedText);

            return this;
        }
    }
}
