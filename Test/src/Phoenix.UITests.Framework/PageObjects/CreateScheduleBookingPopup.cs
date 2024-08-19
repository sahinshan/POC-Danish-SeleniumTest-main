using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CreateScheduleBookingPopup : CommonMethods
    {
        public CreateScheduleBookingPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By dialogHeader_CreateBooking = By.XPath("//h2[text()='Create Booking']");
        readonly By dialogHeader_EditBooking = By.XPath("//h2[text()='Edit Booking']");

        #region Rostering Tab

        readonly By RosteringTab = By.XPath("//*[@id='id--booking-drawer--tabs']//span[text()='Rostering']");

        readonly By Location_ProviderTextOuterDiv = By.XPath("//div[@id ='id--rostering--provider']//*[@class='cd-dropdown-display']");
        readonly By Location_ProviderText = By.XPath("//div[@id = 'id--rostering--provider']//div[@class='cd-dropdown-display-text']");
        By Location_ProviderPicklistOption(string OptionToSelect) => By.XPath("//*[@id='id--rostering--provider']//*[@title = '" + OptionToSelect + "']");

        readonly By BookingTypeOutterDiv = By.XPath("//*[@id='id--rostering--booking-type']/details");
        readonly By BookingTypeDropDownText = By.XPath("//div[@id = 'id--rostering--booking-type']//div[@class='cd-dropdown-display-text']");
        By BookingTypePicklistOption(string RecordText) => By.XPath("//*[@id='id--rostering--booking-type']//*[@title='" + RecordText + "']");


        readonly By scheduleBookingEndTime = By.XPath("//input[@id='id--rostering--end-time--time-picker']");
        readonly By startDayTopDiv = By.XPath("//*[@id='id--rostering--start-day']//div[@class='cd-dropdown-display']");
        readonly By startDaySelectedOption = By.XPath("//*[@id='id--rostering--start-day']//div[@class='cd-dropdown-display-text']");
        By startDayOption(string OptionText) => By.XPath("//*[@id='id--rostering--start-day']//*[@title='" + OptionText + "']");


        readonly By endDayTopDiv = By.XPath("//*[@id='id--rostering--end-day']//div[@class='cd-dropdown-display']");
        readonly By endDaySelectedOption = By.XPath("//*[@id='id--rostering--end-day']//div[@class='cd-dropdown-display-text']");
        By endDayOption(string OptionText) => By.XPath("//*[@id='id--rostering--end-day']//*[@title='" + OptionText + "']");


        static string scheduleBookingStartTimeInputId = "id--rostering--start-time--time-picker";
        readonly By scheduleBookingStartTimeInput = By.XPath("//input[@id='" + scheduleBookingStartTimeInputId + "']");
        readonly By scheduleBookingStartTimePicker_Hours = By.XPath("//*[@id='id--rostering--start-time']//input[@class='numInput flatpickr-hour']");
        readonly By scheduleBookingStartTimePicker_Minutes = By.XPath("//*[@id='id--rostering--start-time']//input[@class='numInput flatpickr-minute']");


        static string scheduleBookingEndTimeInputId = "id--rostering--end-time--time-picker";
        readonly By scheduleBookingEndTimeInput = By.XPath("//input[@id='" + scheduleBookingEndTimeInputId + "']");
        readonly By scheduleBookingEndTimePicker_Hours = By.XPath("//*[@id='id--rostering--end-time']//input[@class='numInput flatpickr-hour']");
        readonly By scheduleBookingEndTimePicker_Minutes = By.XPath("//*[@id='id--rostering--end-time']//input[@class='numInput flatpickr-minute']");

        readonly By scheduleBookingPeopleButton = By.XPath("//div[@id='id--rostering--people-layer--button-open']");
        readonly By genderPreferenceBookingDropdownText = By.XPath("//*[@id='id--rostering--gender-preference']//*[@class='cd-dropdown-display-text']");
        readonly By genderPreferenceBookingDropdownOuterDiv = By.XPath("//*[@id='id--rostering--gender-preference']//*[@class='mcc-dropdown']");
        By genderPreferenceBookingPicklistOptions(string OptionToSelectByText) => By.XPath("//*[@id ='id--rostering--gender-preference']//*[@title = '" + OptionToSelectByText + "']");
        readonly By addUnassignedStaffButton = By.XPath("//button/span[text() = 'Add Unassigned']");
        readonly By editSelectedStaffButton = By.XPath("//button/span[text() = 'Manage Staff']");
        readonly By manageStaffButton = By.XPath("//*[@id='id--rostering--0--button']/button");

        By selectedStaffField(string EmploymentContractId) => By.XPath("//*[@data-id='" + EmploymentContractId + "']/span");
        By selectedStaffField(string EmploymentContractId, int DataPosition) => By.XPath("//*[" + DataPosition + "][@data-id='" + EmploymentContractId + "']/span");
        By selectedStaffField(string EmploymentContractId, string title) => By.XPath("//*[@data-id='" + EmploymentContractId + "']/span[text() = '" + title + "']");
        By removeStaffFromSelectedStaffField(string EmploymentContractId) => By.XPath("//*[@data-id='" + EmploymentContractId + "']/mcc-icon");
        readonly By commentsTextArea = By.XPath("//textarea[@id='id--rostering--comments--textarea']");

        readonly By selectedStaffSection = By.XPath("//div[@id='id--rostering--0--selected']/div[@class='cd-box-of-chips']/span/span[text()='Unassigned']/parent::span");

        readonly By bookingDurationText = By.XPath("//*[@id = 'id--rostering--booking-duration']/span[@class = 'booking_duration']");

        readonly By employeeContractInfoText = By.XPath("//*[@id = 'id--rostering--employee-contract']");

        #endregion

        #region Occurrence Tab

        readonly By OccurrenceTab = By.XPath("//*[@id='id--booking-drawer--tabs']//span[text()='Occurrence']");

        readonly By TopLabel_OccurrenceTab = By.XPath("//*[@id='id--occurrence--rostering-summary']");

        readonly By WeeksTopDiv_OccurrenceTab = By.XPath("//*[@id='id--occurrence--takes-place-every']//div[@class = 'cd-dropdown-display']");
        readonly By WeeksSelectedOption_OccurrenceTab = By.XPath("//*[@id='id--occurrence--takes-place-every']//div[@class = 'cd-dropdown-display-text']");
        readonly By WeeksSearchInput_OccurrenceTab = By.XPath("//*[@id='id--occurrence--takes-place-every--dropdown-filter']");
        By WeeksOption_OccurrenceTab(string TextToSelect) => By.XPath("//div[@id = 'id--occurrence--takes-place-every']//li/button[@title = '" + TextToSelect + "']");

        readonly By NextDueToTakePlaceOuterElement_OccurrenceTab = By.XPath("//*[@id='id--occurrence--next-due']");
        readonly By NextDueToTakePlaceMandatoryIcon_OccurrenceTab = By.XPath("//*[@id='id--occurrence--next-due']//span[@class='mandatory']");
        internal static string NextDueToTakePlaceInputId_OccurrenceTab = "id--occurrence--next-due--date-picker";
        readonly By NextDueToTakePlaceInput_OccurrenceTab = By.XPath("//*[@id='" + NextDueToTakePlaceInputId_OccurrenceTab + "']");
        readonly By NextDueToTakePlaceTopDiv_OccurrenceTab = By.XPath("//*[@id='id--occurrence--next-due--anchor']");
        readonly By NextDueToTakePlace_Calendar_MonthPicklist = By.XPath("//*[@id='id--occurrence--next-due']//select[@aria-label='Month']");
        readonly By NextDueToTakePlace_Calendar_YearInput = By.XPath("//*[@id='id--occurrence--next-due']//input[@aria-label='Year']");
        By NextDueToTakePlace_Calendar_Date(string Date) => By.XPath("//*[@id='id--occurrence--next-due']//span[@aria-label='" + Date + "']");


        readonly By OnAPublicHolidayTopDiv_OccurrenceTab = By.XPath("//*[@id='id--occurrence--public-holiday']");

        readonly By FirstOccurrenceTopDiv_OccurrenceTab = By.XPath("//*[@id='id--occurrence--first-occurrence--anchor']");
        internal static string FirstOccurrenceInputId_OccurrenceTab = "id--occurrence--first-occurrence--date-picker";
        readonly By FirstOccurrenceInput_OccurrenceTab = By.XPath("//*[@id='" + FirstOccurrenceInputId_OccurrenceTab + "']");
        readonly By FirstOccurrence_Calendar_MonthPicklist = By.XPath("//*[@id='id--occurrence--first-occurrence']//select[@aria-label='Month']");
        readonly By FirstOccurrence_Calendar_YearInput = By.XPath("//*[@id='id--occurrence--first-occurrence']//input[@aria-label='Year']");
        By FirstOccurrence_Calendar_Date(string Date) => By.XPath("//*[@id='id--occurrence--first-occurrence']//span[@aria-label='" + Date + "']");



        readonly By LastOccurrenceTopDiv_OccurrenceTab = By.XPath("//*[@id='id--occurrence--last-occurrence--anchor']");
        internal static string LastOccurrenceInputId_OccurrenceTab = "id--occurrence--last-occurrence--date-picker";
        readonly By LastOccurrenceInput_OccurrenceTab = By.XPath("//*[@id='" + LastOccurrenceInputId_OccurrenceTab + "']");
        readonly By LastOccurrence_Calendar_MonthPicklist = By.XPath("//*[@id='id--occurrence--last-occurrence']//select[@aria-label='Month']");
        readonly By LastOccurrence_Calendar_YearInput = By.XPath("//*[@id='id--occurrence--last-occurrence']//input[@aria-label='Year']");
        By LastOccurrence_Calendar_Date(string Date) => By.XPath("//*[@id='id--occurrence--last-occurrence']//span[@aria-label='" + Date + "']");



        readonly By ResetChangesButton_OccurrenceTab = By.XPath("//*[@id='id--append-body--resetButton']/button/span");
        readonly By CreateAnotherBookingCheckbox_OccurrenceTab = By.XPath("//*[@id='id--append-body--create-another-booking--checkbox']");

        readonly By OnAPublicHolidayLabel_OccurrenceTab = By.XPath("//*[@id='id--occurrence--public-holiday']/label[text()='On a Public Holiday']");
        readonly By OnAPublicHoliday_MandatoryField_OccurrenceTab = By.XPath("//*[@id='id--occurrence--public-holiday']/label/span[@class='mandatory']");
        readonly By BookingTakesPlaceEveryLabel_OccurrenceTab = By.XPath("//*[@id='id--occurrence--takes-place-every']/label[text() = 'Booking Takes Place Every']");
        readonly By BookingTakesPlaceEvery_MandatoryField_OccurenceTab = By.XPath("//*[@id='id--occurrence--takes-place-every']/label/span[@class='mandatory']");
        readonly By NextDueToTakePlaceLabel_OccurrenceTab = By.XPath("//*[@id='id--occurrence--next-due']/label[text() = 'Next Due To Take Place']");
        readonly By OnAPublicHolidayDropDownText = By.XPath("//*[@id='id--occurrence--public-holiday']//*[@class='cd-dropdown-display-text']");
        readonly By OnAPublicHolidayDropdown_TopDiv = By.XPath("//div[@id='id--occurrence--public-holiday']//details[contains(@class, 'mcc-dropdown')]");
        //readonly By OnAPublicHolidayDropdown_TopDiv = By.XPath("//div[@id='id--occurrence--public-holiday']//div[@class = 'cd-dropdown-display-text']/following-sibling::*[@name = 'expand_arrow']");
        readonly By BookingTakesPlaceEveryDropdownText = By.XPath("//*[@id='id--occurrence--takes-place-every']//*[@class='cd-dropdown-display-text']");
        By OnAPublicHolidayPicklistOption(string RecordText) => By.XPath("//div[@id='id--occurrence--public-holiday']//*[@class = 'cd-dropdown-list']/li/button[@title='" + RecordText + "']");
        By BookingTakesPlaceEveryPicklistOption(string RecordText) => By.XPath("//div[@id = 'id--occurrence--takes-place-every']//ul[@class = 'cd-dropdown-list']//button[@title='" + RecordText + "']");
        readonly By BookingTakesPlaceEverySearchInput = By.XPath("//input[@id = 'id--occurrence--takes-place-every--dropdown-filter']");
        readonly By BookingTakesEveryDropdown = By.XPath("//*[@id = 'id--occurrence--takes-place-every']//div[@class = 'cd-dropdown-display']");
        #endregion

        #region Dynamic Dialogue

        readonly By header_ErrorDialogue = By.XPath("//*[@id='cd-sub-drawer']//h2");
        readonly By closeButton_ErrorDialogue = By.XPath("//*[@id='cd-sub-drawer']/div/div/button");

        By message_ErrorDialogue(int messagePosition) => By.XPath("//*[@class='cd-booking-issue']/li[" + messagePosition + "]");
        By issueListEntry_ErrorDialogue(int messagePosition) => By.XPath("//*[@id='id--issue--list--" + messagePosition + "']");
        readonly By dismissButton_ErrorDialogue = By.XPath("//*[@id='id--sub-drawer--footer-dismiss']/button");
        readonly By saveButton_ErrorDialogue = By.XPath("//*[@id='id--sub-drawer--footer-positive']/button");

        #endregion

        readonly By closeButton = By.XPath("//*[@id='id--footer--closeButton']");
        readonly By deleteButton = By.XPath("//*[@id='id--footer--deleteButton']");
        readonly By createBookingButton = By.XPath("//*[@class='mcc-drawer__footer']//div[@id='id--footer--saveButton']");
        readonly By saveButon_WarningPopup = By.XPath("//div[@id = 'id--sub-drawer--positive-button']/button");
        readonly By saveChangeBookingButton = By.XPath("//*[@class='mcc-drawer__footer']//div[@id='id--footer--saveButton']");

        #region History Tab

        //xpaths for HistoryTab
        readonly By HistoryTab = By.XPath("//*[@id='id--booking-drawer--tabs']//span[text()='History']");

        readonly By TopLabel_HistoryTab = By.XPath("//*[@id='id--history--rostering-summary']");

        readonly By HistorySection = By.XPath("//*[@id='id--history--history']");

        By HistoryDetailText(int position) => By.XPath("//div[@id='id--history--history']/div[@class='cd-history'][" + position + "]//span");

        By ExpandSection(int position) => By.XPath("//div[@id='id--history--history']/div[@class='cd-history'][" + position + "]/div[@class='cd-history-title-bar']");

        By ExpandField(string fieldName) => By.XPath("//div[@id='id--history--history']/div[@class='cd-history expanded']/div[@class='cd-history-field']//span[text()='" + fieldName + "']");

        By ExpandedField(string fieldName) => By.XPath("//div[@id='id--history--history']/div[@class='cd-history expanded']/div[@class='cd-history-field expanded']//span[text()='" + fieldName + "']");


        By CurrentValue(string fieldName) => By.XPath("//div[@id='id--history--history']//div[@class='cd-history-field expanded']//span[text()='" + fieldName + "']/parent::div/parent::div/span[1]");
        By PreviousValue(string fieldName) => By.XPath("//div[@id='id--history--history']//div[@class='cd-history-field expanded']//span[text()='" + fieldName + "']/parent::div/parent::div/span[2]");

        #endregion

        #region Mandatory Field Label

        By MandatoryField_Label(string FieldName) => By.XPath("//label[text()='" + FieldName + "']/span[@class='mandatory']");

        #endregion

        #region Delete Booking Dialogue

        readonly By header_DeleteBookingDialogue = By.XPath("//*[@id='cd-sub-drawer']//h2[text()='Delete Booking']");
        readonly By reasonForDeleteMandatoryIcon_DeleteBookingDialogue = By.XPath("//div[@id='id--deleteContent--delete-reasons']//span[@class='mandatory']");
        readonly By reasonForDeleteOutterDiv = By.XPath("//*[@id='id--deleteContent--delete-reasons']//div[@class = 'cd-dropdown-display']");
        readonly By reasonForDeleteDropDownText = By.XPath("//*[@id='id--deleteContent--delete-reasons']//*[@class='cd-dropdown-display-text']");

        By reasonForDeletePicklistOption(string RecordText) => By.XPath("//*[@id='id--deleteContent--delete-reasons']//*[@title='" + RecordText + "']");

        readonly By comments_DeleteBookingDialogue = By.XPath("//*[@id='id--deleteContent--delete-comment--textarea']");
        readonly By commentsMandatoryIcon_DeleteBookingDialogue = By.XPath("//div[@id='id--delete-content--2']//span[@class='mandatory']");

        readonly By deleteAlertMessage = By.Id("id--deleteContent--delete-info");

        readonly By cancelButton_DeleteBookingDialogue = By.XPath("//*[@id='id--sub-drawer--footer-dismiss']/button");
        readonly By deleteButton_DeleteBookingDialogue = By.XPath("//*[@id='id--sub-drawer--footer-positive']/button");

        #endregion

        #region Warning Dialogue

        readonly By header_WarningDialogue = By.XPath("//*[@id='cd-sub-drawer']//h2");
        readonly By WarningBody = By.XPath("//*[@id='cd-sub-drawer']//div[@class='mcc-dialog-drawer__body']");
        By WarningBodyMessageLine(int LineNumber) => By.XPath("//*[@id='cd-sub-drawer']//div[@class='mcc-dialog-drawer__body']//li[" + LineNumber + "]");
        readonly By dismissButton_WarningDialogue = By.XPath("//*[@id='id--sub-drawer--footer-dismiss']/button");
        readonly By saveButton_WarningDialogue = By.XPath("//*[@id='id--sub-drawer--footer-positive']/button");

        #endregion


        public CreateScheduleBookingPopup WaitForCreateScheduleBookingPopupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(dialogHeader_CreateBooking);
            WaitForElement(closeButton);
            WaitForElement(createBookingButton);

            return this;
        }

        public CreateScheduleBookingPopup WaitForEditScheduleBookingPopupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(dialogHeader_EditBooking);
            WaitForElement(closeButton);
            WaitForElement(deleteButton);
            WaitForElement(createBookingButton);

            return this;
        }

        public CreateScheduleBookingPopup WaitForCreateScheduleBookingPopupClosed()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementNotVisible(dialogHeader_CreateBooking, 15);
            WaitForElementNotVisible(closeButton, 15);
            WaitForElementNotVisible(createBookingButton, 15);

            return this;
        }

        public CreateScheduleBookingPopup WaitForEditScheduleBookingPopupClosed()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementNotVisible(dialogHeader_EditBooking, 15);
            WaitForElementNotVisible(closeButton, 15);
            WaitForElementNotVisible(deleteButton, 15);
            WaitForElementNotVisible(createBookingButton, 15);

            return this;
        }

        #region Rostering Tab

        public CreateScheduleBookingPopup ClickRosteringTab()
        {
            ScrollToElement(RosteringTab);
            WaitForElementToBeClickable(RosteringTab);
            Click(RosteringTab);
            return this;
        }

        //method to validate if the RosteringTab is visible
        public CreateScheduleBookingPopup ValidateRosteringTabIsVisible(bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(RosteringTab);
            }
            else
            {
                WaitForElementNotVisible(RosteringTab, 3);
            }

            return this;
        }

        //method to validate location provider text
        public CreateScheduleBookingPopup ValidateLocationProviderText(string ExpectedText)
        {
            WaitForElementVisible(Location_ProviderTextOuterDiv);
            ScrollToElement(Location_ProviderText);
            var elementText = GetElementText(Location_ProviderText);
            Assert.AreEqual(ExpectedText, elementText);
            return this;
        }

        //method to select location provider
        public CreateScheduleBookingPopup SelectLocationProvider(string TextToSelect)
        {
            WaitForElementToBeClickable(Location_ProviderTextOuterDiv);
            Click(Location_ProviderTextOuterDiv);

            WaitForElementToBeClickable(Location_ProviderPicklistOption(TextToSelect));
            Click(Location_ProviderPicklistOption(TextToSelect));

            return this;
        }

        public CreateScheduleBookingPopup ValidateBookingTypeDropDownText(string ExpectedText)
        {
            WaitForElement(BookingTypeDropDownText);
            var elementText = GetElementTextByJavascript(BookingTypeDropDownText);
            Assert.AreEqual(ExpectedText, elementText);
            return this;
        }

        public CreateScheduleBookingPopup SelectBookingType(string TextToSelect)
        {
            WaitForElementToBeClickable(BookingTypeOutterDiv);
            Click(BookingTypeOutterDiv);

            WaitForElementToBeClickable(BookingTypePicklistOption(TextToSelect));
            Click(BookingTypePicklistOption(TextToSelect));

            return this;
        }

        public CreateScheduleBookingPopup SetStartDay(string DayToSelect)
        {
            WaitForElementToBeClickable(startDayTopDiv);
            ScrollToElement(startDayTopDiv);
            Click(startDayTopDiv);

            WaitForElementToBeClickable(startDayOption(DayToSelect));
            ScrollToElement(startDayOption(DayToSelect));
            Click(startDayOption(DayToSelect));

            return this;
        }

        public CreateScheduleBookingPopup SetEndDay(string DayToSelect)
        {
            WaitForElementToBeClickable(endDayTopDiv);
            Click(endDayTopDiv);

            WaitForElementToBeClickable(endDayOption(DayToSelect));
            Click(endDayOption(DayToSelect));

            return this;
        }

        public CreateScheduleBookingPopup SetStartTime(string Hours, string Minutes)
        {
            WaitForElementToBeClickable(scheduleBookingStartTimeInput);
            Click(scheduleBookingStartTimeInput);

            WaitForElementToBeClickable(scheduleBookingStartTimePicker_Hours);
            SendKeys(scheduleBookingStartTimePicker_Hours, Hours + Keys.Tab);

            WaitForElementToBeClickable(scheduleBookingStartTimePicker_Minutes);
            SendKeys(scheduleBookingStartTimePicker_Minutes, Minutes + Keys.Tab + Keys.Tab);

            return this;
        }

        public string GetStartTime()
        {
            ScrollToElement(scheduleBookingStartTimeInput);
            var startTime = GetElementValueByJavascript(scheduleBookingStartTimeInputId);
            return startTime.ToString();
        }

        public CreateScheduleBookingPopup ValidateStartTime(string ExpectedStartTime)
        {
            WaitForElementToBeClickable(scheduleBookingStartTimeInput);
            ScrollToElement(scheduleBookingStartTimeInput);
            var startTime = GetElementValueByJavascript(scheduleBookingStartTimeInputId);
            Assert.AreEqual(ExpectedStartTime, startTime);

            return this;
        }

        public CreateScheduleBookingPopup SetEndTime(string Hours, string Minutes)
        {
            WaitForElementToBeClickable(scheduleBookingEndTimeInput);
            Click(scheduleBookingEndTimeInput);

            WaitForElementToBeClickable(scheduleBookingEndTimePicker_Hours);
            SendKeysWithoutClearing(scheduleBookingEndTimePicker_Hours, Hours + Keys.Tab);

            WaitForElementToBeClickable(scheduleBookingEndTimePicker_Minutes);
            SendKeys(scheduleBookingEndTimePicker_Minutes, Minutes + Keys.Tab + Keys.Tab);

            return this;
        }

        public string GetEndTime()
        {
            ScrollToElement(scheduleBookingEndTimeInput);
            var endTime = GetElementValueByJavascript(scheduleBookingEndTimeInputId);
            return endTime.ToString();
        }

        public CreateScheduleBookingPopup ValidateEndTime(string ExpectedEndTime)
        {
            ScrollToElement(scheduleBookingEndTimeInput);
            var endTime = GetElementValueByJavascript(scheduleBookingEndTimeInputId);
            Assert.AreEqual(ExpectedEndTime, endTime);

            return this;

        }

        public CreateScheduleBookingPopup ClickSelectPeople()
        {
            WaitForElementToBeClickable(scheduleBookingPeopleButton);
            Click(scheduleBookingPeopleButton);
            return this;
        }

        public CreateScheduleBookingPopup ClickEditSelectedStaff()
        {
            WaitForElementToBeClickable(editSelectedStaffButton);
            ScrollToElement(editSelectedStaffButton);
            Click(editSelectedStaffButton);
            return this;
        }

        public CreateScheduleBookingPopup ClickManageStaffButton()
        {
            WaitForElementToBeClickable(manageStaffButton);
            ScrollToElement(manageStaffButton);
            Click(manageStaffButton);

            return this;
        }

        //method to validate if the manageStaffButton is disabled
        public CreateScheduleBookingPopup ValidateManageStaffButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
                ValidateElementDisabled(manageStaffButton);
            else
                ValidateElementNotDisabled(manageStaffButton);

            return this;
        }


        //validate selected staff field values by position
        public CreateScheduleBookingPopup ValidateSelectedStaffFieldValues(string EmploymentContractId, string ExpectedValue)
        {
            //ScrollToElement(selectedStaffField(EmploymentContractId));
            var elementText = GetElementText(selectedStaffField(EmploymentContractId));
            Assert.AreEqual(ExpectedValue, elementText);
            return this;
        }

        public CreateScheduleBookingPopup GetAndValidateUnassignedStaffFieldValue(string ExpectedValue)
        {
            var dataValue = GetElementByAttributeValue(selectedStaffSection, "data-id");

            ScrollToElement(selectedStaffField(dataValue));
            var elementText = GetElementText(selectedStaffField(dataValue));
            Assert.AreEqual(ExpectedValue, elementText);

            return this;
        }

        public CreateScheduleBookingPopup ValidateSelectedStaffFieldValues(string EmploymentContractId, string ExpectedValue, int DataPosition)
        {
            WaitForElementToBeClickable(selectedStaffField(EmploymentContractId, DataPosition));
            ScrollToElement(selectedStaffField(EmploymentContractId, DataPosition));
            var elementText = GetElementText(selectedStaffField(EmploymentContractId, DataPosition));
            Assert.AreEqual(ExpectedValue, elementText);
            return this;
        }


        //method to remove value from selected staff field by data-id
        public CreateScheduleBookingPopup ValidateSelectedStaffFieldValues(Guid EmploymentContractId, string ExpectedValue)
        {
            return ValidateSelectedStaffFieldValues(EmploymentContractId.ToString(), ExpectedValue);
        }

        public CreateScheduleBookingPopup ValidateSelectedStaffFieldValues(string EmploymentContractId, string Title, bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(selectedStaffField(EmploymentContractId, Title));
            }
            else
            {
                WaitForElementNotVisible(selectedStaffField(EmploymentContractId, Title), 3);
            }
            return this;
        }

        //method to remove value from selected staff field by position and display text
        public CreateScheduleBookingPopup RemoveStaffFromSelectedStaffField(string EmploymentContractId)
        {
            WaitForElement(removeStaffFromSelectedStaffField(EmploymentContractId));
            Click(removeStaffFromSelectedStaffField(EmploymentContractId));
            return this;
        }

        public CreateScheduleBookingPopup RemoveStaffFromSelectedStaffField(Guid EmploymentContractId)
        {
            return RemoveStaffFromSelectedStaffField(EmploymentContractId.ToString());
        }

        //method to add unassigned staff
        public CreateScheduleBookingPopup ClickAddUnassignedStaff()
        {
            WaitForElementToBeClickable(addUnassignedStaffButton);
            ScrollToElement(addUnassignedStaffButton);
            Click(addUnassignedStaffButton);
            return this;
        }

        #endregion

        #region Occurence Tab

        public CreateScheduleBookingPopup WaitForOccurrenceTabToLoad()
        {
            WaitForElementVisible(TopLabel_OccurrenceTab);

            WaitForElementVisible(WeeksTopDiv_OccurrenceTab);

            WaitForElementVisible(NextDueToTakePlaceTopDiv_OccurrenceTab);
            WaitForElementVisible(OnAPublicHolidayTopDiv_OccurrenceTab);
            WaitForElementVisible(FirstOccurrenceTopDiv_OccurrenceTab);
            WaitForElementVisible(LastOccurrenceTopDiv_OccurrenceTab);

            WaitForElementVisible(ResetChangesButton_OccurrenceTab);

            if (GetElementVisibility(dialogHeader_CreateBooking).Equals(true))
                WaitForElementVisible(CreateAnotherBookingCheckbox_OccurrenceTab);

            return this;
        }

        public CreateScheduleBookingPopup ValidateOccurrenceTabTopLabelContainsText(string TextToSearch)
        {
            WaitForElementVisible(TopLabel_OccurrenceTab);
            ValidateElementTextContainsText(TopLabel_OccurrenceTab, TextToSearch);

            return this;
        }

        public CreateScheduleBookingPopup ClickOccurrenceTab()
        {
            WaitForElementToBeClickable(OccurrenceTab);
            ScrollToElement(OccurrenceTab);
            Click(OccurrenceTab);
            return this;
        }

        public CreateScheduleBookingPopup ValidateOccurrenceTabIsVisible(bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(OccurrenceTab);
            }
            else
            {
                WaitForElementNotVisible(OccurrenceTab, 3);
            }

            return this;
        }

        public CreateScheduleBookingPopup SelectWeeksOption(string WeeksOptionToSelect)
        {
            WaitForElementToBeClickable(WeeksTopDiv_OccurrenceTab);
            Click(WeeksTopDiv_OccurrenceTab);

            WaitForElementVisible(WeeksSearchInput_OccurrenceTab);
            SendKeys(WeeksSearchInput_OccurrenceTab, WeeksOptionToSelect + Keys.Tab);

            WaitForElementToBeClickable(WeeksOption_OccurrenceTab(WeeksOptionToSelect));
            Click(WeeksOption_OccurrenceTab(WeeksOptionToSelect));

            return this;
        }

        public CreateScheduleBookingPopup ValidateSelectedWeeks(string WeeksOptionSelected)
        {
            ValidateElementText(WeeksSelectedOption_OccurrenceTab, WeeksOptionSelected);
            return this;
        }

        public CreateScheduleBookingPopup ValidateNextDueToTakePlaceFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(NextDueToTakePlaceOuterElement_OccurrenceTab);
            else
                ValidateElementNotDisabled(NextDueToTakePlaceOuterElement_OccurrenceTab);

            return this;
        }

        public CreateScheduleBookingPopup ValidateNextDueToTakePlaceMandatoryIconVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NextDueToTakePlaceMandatoryIcon_OccurrenceTab);
            else
                WaitForElementNotVisible(NextDueToTakePlaceMandatoryIcon_OccurrenceTab, 3);

            return this;
        }


        public CreateScheduleBookingPopup ValidateNextDueToTakePlaceDate(string ExpectedText)
        {
            ValidateElementValueByJavascript(NextDueToTakePlaceInputId_OccurrenceTab, ExpectedText);

            return this;
        }

        public CreateScheduleBookingPopup SelectNextDueToTakePlaceDate(string Year, string Month, string Day)
        {
            WaitForElementToBeClickable(NextDueToTakePlaceInput_OccurrenceTab);
            ScrollToElement(NextDueToTakePlaceInput_OccurrenceTab);
            Click(NextDueToTakePlaceInput_OccurrenceTab);

            if (GetElementByAttributeValue(NextDueToTakePlace_Calendar_YearInput, "disabled") != "true")
            {
                WaitForElementToBeClickable(NextDueToTakePlace_Calendar_YearInput);
                ScrollToElement(NextDueToTakePlace_Calendar_YearInput);
                SendKeys(NextDueToTakePlace_Calendar_YearInput, Year + Keys.Tab);
            }

            WaitForElementToBeClickable(NextDueToTakePlace_Calendar_MonthPicklist);
            ScrollToElement(NextDueToTakePlace_Calendar_MonthPicklist);
            SelectPicklistElementByText(NextDueToTakePlace_Calendar_MonthPicklist, Month);

            var date = string.Format("{0} {1}, {2}", Month, Day, Year);
            WaitForElementToBeClickable(NextDueToTakePlace_Calendar_Date(date));
            ScrollToElement(NextDueToTakePlace_Calendar_Date(date));
            Click(NextDueToTakePlace_Calendar_Date(date));

            return this;
        }

        public CreateScheduleBookingPopup ValidateNextDueToTakePlaceDateNotSelectable(string Year, string Month, string Day)
        {
            WaitForElementToBeClickable(NextDueToTakePlaceInput_OccurrenceTab);
            ScrollToElement(NextDueToTakePlaceInput_OccurrenceTab);
            Click(NextDueToTakePlaceInput_OccurrenceTab);

            WaitForElementToBeClickable(NextDueToTakePlace_Calendar_MonthPicklist);
            SelectPicklistElementByText(NextDueToTakePlace_Calendar_MonthPicklist, Month);

            var date = string.Format("{0} {1}, {2}", Month, Day, Year);
            WaitForElementVisible(NextDueToTakePlace_Calendar_Date(date));
            ScrollToElement(NextDueToTakePlace_Calendar_Date(date));
            var attributeValue = GetElementByAttributeValue(NextDueToTakePlace_Calendar_Date(date), "class");
            StringAssert.Contains(attributeValue, "disabled");

            return this;
        }

        public CreateScheduleBookingPopup ValidateNextDueToTakePlaceDateSelectable(string Year, string Month, string Day)
        {
            WaitForElementToBeClickable(NextDueToTakePlaceInput_OccurrenceTab);
            Click(NextDueToTakePlaceInput_OccurrenceTab);

            if (GetElementByAttributeValue(NextDueToTakePlace_Calendar_YearInput, "disabled") != "true")
            {
                WaitForElementToBeClickable(NextDueToTakePlace_Calendar_YearInput);
                SendKeys(NextDueToTakePlace_Calendar_YearInput, Year + Keys.Tab);
            }

            WaitForElementToBeClickable(NextDueToTakePlace_Calendar_MonthPicklist);
            SelectPicklistElementByText(NextDueToTakePlace_Calendar_MonthPicklist, Month);

            var date = string.Format("{0} {1}, {2}", Month, Day, Year);
            WaitForElementVisible(NextDueToTakePlace_Calendar_Date(date));
            var attributeValue = GetElementByAttributeValue(NextDueToTakePlace_Calendar_Date(date), "class");
            Assert.IsFalse(attributeValue.Contains("disabled"));

            return this;
        }

        //validate NextDueToTakePlaceInput_OccurrenceTab field is disabled
        public CreateScheduleBookingPopup ValidateNextDueToTakePlaceInputDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(NextDueToTakePlaceInput_OccurrenceTab);
            else
                ValidateElementNotDisabled(NextDueToTakePlaceInput_OccurrenceTab);

            return this;
        }

        public CreateScheduleBookingPopup ValidateCurrentDateSelectedInFirstOccurenceCalendar(string Year, string Month, string Day)
        {
            WaitForElementToBeClickable(FirstOccurrenceInput_OccurrenceTab);
            ScrollToElement(FirstOccurrenceInput_OccurrenceTab);
            Click(FirstOccurrenceInput_OccurrenceTab);

            WaitForElementToBeClickable(FirstOccurrence_Calendar_MonthPicklist);
            ScrollToElement(FirstOccurrence_Calendar_MonthPicklist);
            SelectPicklistElementByText(FirstOccurrence_Calendar_MonthPicklist, Month);

            var date = string.Format("{0} {1}, {2}", Month, Day, Year);
            WaitForElementVisible(FirstOccurrence_Calendar_Date(date));
            var classAttributeValue = GetElementByAttributeValue(FirstOccurrence_Calendar_Date(date), "class");
            Assert.IsTrue(classAttributeValue.Contains("today"));

            var ariaCurrentAttributeValue = GetElementByAttributeValue(FirstOccurrence_Calendar_Date(date), "aria-current");
            Assert.IsTrue(ariaCurrentAttributeValue.Contains("date"));

            return this;
        }

        public CreateScheduleBookingPopup ValidateFirstOccurrenceValue(string ExpectedValue)
        {
            ValidateElementValueByJavascript(FirstOccurrenceInputId_OccurrenceTab, ExpectedValue);

            return this;
        }

        public CreateScheduleBookingPopup SelectFirstOccurrenceDate(string Year, string Month, string Day)
        {
            WaitForElementToBeClickable(FirstOccurrenceInput_OccurrenceTab);
            ScrollToElement(FirstOccurrenceInput_OccurrenceTab);
            Click(FirstOccurrenceInput_OccurrenceTab);

            WaitForElementToBeClickable(FirstOccurrence_Calendar_MonthPicklist);
            ScrollToElement(FirstOccurrence_Calendar_MonthPicklist);
            SelectPicklistElementByText(FirstOccurrence_Calendar_MonthPicklist, Month);

            WaitForElementToBeClickable(FirstOccurrence_Calendar_YearInput);
            ScrollToElement(FirstOccurrence_Calendar_YearInput);
            SendKeys(FirstOccurrence_Calendar_YearInput, Year);

            var date = string.Format("{0} {1}, {2}", Month, Day, Year);
            WaitForElementToBeClickable(FirstOccurrence_Calendar_Date(date));
            ScrollToElement(FirstOccurrence_Calendar_Date(date));
            Click(FirstOccurrence_Calendar_Date(date));

            return this;
        }


        public CreateScheduleBookingPopup SelectLastOccurrenceDate(string Year, string Month, string Day)
        {
            WaitForElementToBeClickable(LastOccurrenceInput_OccurrenceTab);
            ScrollToElement(LastOccurrenceInput_OccurrenceTab);
            Click(LastOccurrenceInput_OccurrenceTab);

            WaitForElementToBeClickable(LastOccurrence_Calendar_YearInput);
            ScrollToElement(LastOccurrence_Calendar_YearInput);
            SendKeys(LastOccurrence_Calendar_YearInput, Year);

            WaitForElementToBeClickable(LastOccurrence_Calendar_MonthPicklist);
            ScrollToElement(LastOccurrence_Calendar_MonthPicklist);
            SelectPicklistElementByText(LastOccurrence_Calendar_MonthPicklist, Month);

            var date = string.Format("{0} {1}, {2}", Month, Day, Year);
            WaitForElementToBeClickable(LastOccurrence_Calendar_Date(date));
            ScrollToElement(LastOccurrence_Calendar_Date(date));
            Click(LastOccurrence_Calendar_Date(date));

            return this;
        }

        public CreateScheduleBookingPopup ValidateLastOccurrenceDateNotSelectable(string Year, string Month, string Day)
        {
            WaitForElementToBeClickable(LastOccurrenceInput_OccurrenceTab);
            ScrollToElement(LastOccurrenceInput_OccurrenceTab);
            Click(LastOccurrenceInput_OccurrenceTab);

            WaitForElementToBeClickable(LastOccurrence_Calendar_MonthPicklist);
            try
            {
                SelectPicklistElementByText(LastOccurrence_Calendar_MonthPicklist, Month);
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                return this;//if the month is not even selectale, then simply exit the method
            }

            WaitForElementToBeClickable(LastOccurrence_Calendar_YearInput);
            ScrollToElement(LastOccurrence_Calendar_YearInput);
            SendKeys(LastOccurrence_Calendar_YearInput, Year);

            var date = string.Format("{0} {1}, {2}", Month, Day, Year);
            WaitForElementVisible(LastOccurrence_Calendar_Date(date));
            ScrollToElement(LastOccurrence_Calendar_Date(date));
            var attributeValue = GetElementByAttributeValue(LastOccurrence_Calendar_Date(date), "class");
            StringAssert.Contains(attributeValue, "disabled");

            return this;
        }

        public CreateScheduleBookingPopup ValidateLastOccurrenceValue(string ExpectedValue)
        {
            ValidateElementValueByJavascript(LastOccurrenceInputId_OccurrenceTab, ExpectedValue);

            return this;
        }


        public CreateScheduleBookingPopup ClickCreateAnotherBookingPicklist()
        {
            WaitForElementToBeClickable(CreateAnotherBookingCheckbox_OccurrenceTab);
            Click(CreateAnotherBookingCheckbox_OccurrenceTab);
            return this;
        }

        public CreateScheduleBookingPopup ValidateCreateAnotherBookingCheckboxIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CreateAnotherBookingCheckbox_OccurrenceTab);
            else
                WaitForElementNotVisible(CreateAnotherBookingCheckbox_OccurrenceTab, 3);

            return this;
        }

        //method to validate create another booking checkbox is checked
        public CreateScheduleBookingPopup ValidateCreateAnotherBookingCheckboxIsChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(CreateAnotherBookingCheckbox_OccurrenceTab);
            else
                ValidateElementNotChecked(CreateAnotherBookingCheckbox_OccurrenceTab);

            return this;
        }

        public CreateScheduleBookingPopup ClickResetChangesButton()
        {
            WaitForElementToBeClickable(ResetChangesButton_OccurrenceTab);
            ScrollToElement(ResetChangesButton_OccurrenceTab);
            Click(ResetChangesButton_OccurrenceTab);
            ScrollToElement(ResetChangesButton_OccurrenceTab);
            Click(ResetChangesButton_OccurrenceTab);
            return this;
        }

        //validate reset changes button is visible
        public CreateScheduleBookingPopup ValidateResetChangesButtonIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResetChangesButton_OccurrenceTab);
            else
                WaitForElementNotVisible(ResetChangesButton_OccurrenceTab, 3);

            return this;
        }

        public CreateScheduleBookingPopup ValidateOnAPublicHolidayMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(OnAPublicHolidayLabel_OccurrenceTab);
            ScrollToElement(OnAPublicHolidayLabel_OccurrenceTab);

            if (ExpectVisible)
                WaitForElementVisible(OnAPublicHoliday_MandatoryField_OccurrenceTab);
            else
                WaitForElementNotVisible(OnAPublicHoliday_MandatoryField_OccurrenceTab, 3);

            return this;
        }

        public CreateScheduleBookingPopup SelectBookingTakesPlaceEvery(string TextToSelect)
        {
            WaitForElementToBeClickable(BookingTakesEveryDropdown);
            Click(BookingTakesEveryDropdown);

            WaitForElementToBeClickable(BookingTakesPlaceEverySearchInput);
            ScrollToElement(BookingTakesPlaceEverySearchInput);
            Click(BookingTakesPlaceEverySearchInput);

            WaitForElementToBeClickable(BookingTakesPlaceEveryPicklistOption(TextToSelect));
            ScrollToElement(BookingTakesPlaceEveryPicklistOption(TextToSelect));
            Click(BookingTakesPlaceEveryPicklistOption(TextToSelect));

            return this;
        }

        //Method to Validate Booking Takes Place Every Mandatory Field Visibility
        public CreateScheduleBookingPopup ValidateBookingTakesPlaceEveryMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(BookingTakesPlaceEveryLabel_OccurrenceTab);
            ScrollToElement(BookingTakesPlaceEveryLabel_OccurrenceTab);

            if (ExpectVisible)
                WaitForElementVisible(BookingTakesPlaceEvery_MandatoryField_OccurenceTab);
            else
                WaitForElementNotVisible(BookingTakesPlaceEvery_MandatoryField_OccurenceTab, 3);

            return this;
        }

        //method to validate next due to take place mandatory field visibility
        public CreateScheduleBookingPopup ValidateNextDueToTakePlaceMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(NextDueToTakePlaceLabel_OccurrenceTab);
            ScrollToElement(NextDueToTakePlaceLabel_OccurrenceTab);

            if (ExpectVisible)
                WaitForElementVisible(NextDueToTakePlaceMandatoryIcon_OccurrenceTab);
            else
                WaitForElementNotVisible(NextDueToTakePlaceMandatoryIcon_OccurrenceTab, 3);

            return this;
        }

        public CreateScheduleBookingPopup SelectOnAPublicHoliday(string TextToSelect)
        {
            WaitForElementToBeClickable(OnAPublicHolidayDropdown_TopDiv);
            ScrollToElement(OnAPublicHolidayDropdown_TopDiv);
            Click(OnAPublicHolidayDropdown_TopDiv);

            if (IsElementAttributePresent(OnAPublicHolidayDropdown_TopDiv, "open").Equals(false))
                Click(OnAPublicHolidayDropdown_TopDiv);

            WaitForElementVisible(OnAPublicHolidayPicklistOption(TextToSelect));
            WaitForElementToBeClickable(OnAPublicHolidayPicklistOption(TextToSelect));
            ScrollToElement(OnAPublicHolidayPicklistOption(TextToSelect));
            Click(OnAPublicHolidayPicklistOption(TextToSelect));

            return this;
        }

        //validate on a public holiday picklist option is available
        public CreateScheduleBookingPopup ValidateOnAPublicHolidayPicklistOptionIsAvailable(string OptionToValidate, bool ExpectAvailable)
        {
            WaitForElementToBeClickable(OnAPublicHolidayDropdown_TopDiv);
            Click(OnAPublicHolidayDropdown_TopDiv);
            if (IsElementAttributePresent(OnAPublicHolidayDropdown_TopDiv, "open").Equals(false))
                Click(OnAPublicHolidayDropdown_TopDiv);

            if (ExpectAvailable)
                WaitForElementVisible(OnAPublicHolidayPicklistOption(OptionToValidate));
            else
                WaitForElementNotVisible(OnAPublicHolidayPicklistOption(OptionToValidate), 3);

            return this;
        }

        public CreateScheduleBookingPopup ValidateOnAPublicHolidayDropDownText(string ExpectedText)
        {
            var elementText = GetElementTextByJavascript(OnAPublicHolidayDropDownText);
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CreateScheduleBookingPopup ValidateBookingTakesPlaceEveryDropDownText(string ExpectedText)
        {
            WaitForElementVisible(BookingTakesPlaceEveryDropdownText);
            ScrollToElement(BookingTakesPlaceEveryDropdownText);
            var elementText = GetElementText(BookingTakesPlaceEveryDropdownText);
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CreateScheduleBookingPopup ValidateNextDueToTakePlaceFieldVisibility()
        {
            WaitForElementVisible(NextDueToTakePlaceOuterElement_OccurrenceTab);
            ScrollToElement(NextDueToTakePlaceOuterElement_OccurrenceTab);
            WaitForElementVisible(NextDueToTakePlaceLabel_OccurrenceTab);
            WaitForElementVisible(NextDueToTakePlaceInput_OccurrenceTab);

            return this;
        }

        #endregion

        #region Dynamic Dialogue

        public CreateScheduleBookingPopup WaitForDynamicDialogueToLoad()
        {
            SwitchToDefaultFrame();

            System.Threading.Thread.Sleep(1500);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElementVisible(header_ErrorDialogue);

            return this;
        }

        public CreateScheduleBookingPopup ValidateMessage_DynamicDialogue(string ExpectedText, int MessagePosition = 1)
        {
            ValidateElementText(message_ErrorDialogue(MessagePosition), ExpectedText);

            return this;
        }

        public CreateScheduleBookingPopup ValidateMessageContainsText_DynamicDialogue(string ExpectedText, int MessagePosition = 1)
        {
            ValidateElementTextContainsText(message_ErrorDialogue(MessagePosition), ExpectedText);

            return this;
        }

        public CreateScheduleBookingPopup ValidateIssueMessage_DynamicDialogue(string ExpectedText, int MessagePosition = 1)
        {
            ValidateElementText(issueListEntry_ErrorDialogue(MessagePosition), ExpectedText);

            return this;
        }

        public CreateScheduleBookingPopup ClickDismissButton_DynamicDialogue()
        {
            WaitForElementToBeClickable(dismissButton_ErrorDialogue);
            Click(dismissButton_ErrorDialogue);

            return this;
        }

        public CreateScheduleBookingPopup ClickSaveButton_DynamicDialogue()
        {
            WaitForElementToBeClickable(saveButton_ErrorDialogue);
            Click(saveButton_ErrorDialogue);

            return this;
        }

        #endregion

        public CreateScheduleBookingPopup ClickCreateBooking()
        {
            WaitForElementToBeClickable(createBookingButton);

            Click(createBookingButton);
            return this;
        }

        public CreateScheduleBookingPopup ClickOnCloseButton()
        {
            WaitForElement(closeButton);
            ClickSpecial(closeButton);
            return this;
        }

        //method to ValidateMandatoryFieldIsDisplayed
        public CreateScheduleBookingPopup ValidateMandatoryFieldIsDisplayed(string FieldName, bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(MandatoryField_Label(FieldName));
                ScrollToElement(MandatoryField_Label(FieldName));
            }
            else
            {
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 3);
            }

            return this;
        }

        //method to insert text in comments text area
        public CreateScheduleBookingPopup InsertTextInCommentsTextArea(string TextToInsert)
        {
            WaitForElementToBeClickable(commentsTextArea);
            ScrollToElement(commentsTextArea);
            SendKeys(commentsTextArea, TextToInsert);
            return this;
        }

        //method to validate text in comments text area
        public CreateScheduleBookingPopup ValidateTextInCommentsTextArea(string ExpectedText)
        {
            WaitForElement(commentsTextArea);
            ValidateElementValue(commentsTextArea, ExpectedText);

            return this;
        }

        //validate comments text area is visible
        public CreateScheduleBookingPopup ValidateCommentsTextAreaIsVisible(bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(commentsTextArea);
                ScrollToElement(commentsTextArea);
            }
            else
            {
                WaitForElementNotVisible(commentsTextArea, 3);
            }

            return this;
        }

        //method to select genderPreferenceBookingDropdownText
        public CreateScheduleBookingPopup SelectGenderPreferenceBookingDropdownText(string OptionToSelectByText)
        {
            WaitForElementToBeClickable(genderPreferenceBookingDropdownOuterDiv);
            ScrollToElement(genderPreferenceBookingDropdownOuterDiv);
            Click(genderPreferenceBookingDropdownOuterDiv);

            WaitForElementToBeClickable(genderPreferenceBookingPicklistOptions(OptionToSelectByText));
            ScrollToElement(genderPreferenceBookingPicklistOptions(OptionToSelectByText));
            Click(genderPreferenceBookingPicklistOptions(OptionToSelectByText));

            return this;
        }

        //method to validate gender preference booking dropdown text
        public CreateScheduleBookingPopup ValidateGenderPreferenceBookingDropdownText(string ExpectedText)
        {
            ScrollToElement(genderPreferenceBookingDropdownText);
            var elementText = GetElementText(genderPreferenceBookingDropdownText);
            Assert.AreEqual(ExpectedText, elementText);
            return this;
        }

        public CreateScheduleBookingPopup ValidateStartDayText(string ExpectedText)
        {
            WaitForElementVisibleWithoutException(startDaySelectedOption, 5);
            ScrollToElement(startDaySelectedOption);
            var elementText = GetElementText(startDaySelectedOption);
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public CreateScheduleBookingPopup ValidateEndDayText(string ExpectedText)
        {
            WaitForElementVisibleWithoutException(endDaySelectedOption, 5);
            ScrollToElement(endDaySelectedOption);
            var elementText = GetElementText(endDaySelectedOption);

            Assert.AreEqual(ExpectedText, elementText);
            return this;
        }

        //click warning save button
        public CreateScheduleBookingPopup ClickSaveButton_WarningPopup()
        {
            WaitForElementToBeClickable(saveButon_WarningPopup);
            ScrollToElement(saveButon_WarningPopup);
            Click(saveButon_WarningPopup);

            return this;
        }

        public CreateScheduleBookingPopup OpenDuplicateTab()
        {
            OpenNewTabViaJavascript();

            return this;
        }

        string currentWindow;
        public CreateScheduleBookingPopup SwitchToNewTab()
        {
            currentWindow = GetCurrentWindowIdentifier();
            string popupWindow = GetAllWindowIdentifier().Where(c => c != currentWindow).FirstOrDefault();

            SwitchToWindow(popupWindow);

            NavigateToBrowserURL(appURL + "/pages/default.aspx");

            return this;
        }

        public CreateScheduleBookingPopup SwitchToPreviousTab()
        {
            driver.Close();
            SwitchToWindow(currentWindow);

            return this;
        }

        public CreateScheduleBookingPopup ClickOnDeleteButton()
        {
            WaitForElement(deleteButton);
            ClickSpecial(deleteButton);
            return this;
        }

        public CreateScheduleBookingPopup ValidateDeleteButtonIsPresent(bool IsVisible = true)
        {
            if (IsVisible)
                WaitForElementVisible(deleteButton);
            else
                WaitForElementNotVisible(deleteButton, 3);

            return this;
        }

        #region History Tab

        public CreateScheduleBookingPopup ValidateHistoryTabIsVisible(bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
                WaitForElementVisible(HistoryTab);
            else
                WaitForElementNotVisible(HistoryTab, 3);

            return this;
        }

        public CreateScheduleBookingPopup ClickHistoryTab()
        {
            WaitForElementToBeClickable(HistoryTab);
            ScrollToElement(HistoryTab);
            Click(HistoryTab);

            return this;
        }

        public CreateScheduleBookingPopup WaitForHistoryTabToLoad()
        {
            WaitForElementVisible(TopLabel_HistoryTab);
            WaitForElementVisible(HistorySection);

            WaitForElementVisible(closeButton);
            WaitForElementVisible(deleteButton);
            WaitForElementVisible(createBookingButton);

            return this;
        }

        public CreateScheduleBookingPopup ValidateDetailsOnHistoryTab(int position, string ExpextedText)
        {
            WaitForElementVisible(HistoryDetailText(position));
            ValidateElementTextContainsText(HistoryDetailText(position), ExpextedText);

            return this;
        }

        public CreateScheduleBookingPopup ExpandHistoryDetailSection(int position)
        {
            WaitForElementToBeClickable(ExpandSection(position));
            ScrollToElement(ExpandSection(position));
            Click(ExpandSection(position));

            return this;
        }

        public CreateScheduleBookingPopup ExpandSpecificField(string FieldName)
        {
            WaitForElementToBeClickable(ExpandField(FieldName));
            ScrollToElement(ExpandField(FieldName));
            Click(ExpandField(FieldName));

            return this;
        }

        public CreateScheduleBookingPopup ValidateExpandedFieldIsVisible(string FieldName, bool IsVisible)
        {
            if (IsVisible)
                WaitForElementVisible(ExpandedField(FieldName));
            else
                WaitForElementNotVisible(ExpandedField(FieldName), 5);

            return this;
        }

        public CreateScheduleBookingPopup ValidateCurrentAndPreviousValue(string FieldName, string CurrentText, string PreviousText)
        {
            WaitForElementVisible(CurrentValue(FieldName));
            ScrollToElement(CurrentValue(FieldName));
            ValidateElementText(CurrentValue(FieldName), "Current: " + CurrentText);

            WaitForElementVisible(PreviousValue(FieldName));
            ScrollToElement(PreviousValue(FieldName));
            if (PreviousText == "")
                ValidateElementText(PreviousValue(FieldName), "Previous:" + PreviousText);
            else
                ValidateElementText(PreviousValue(FieldName), "Previous: " + PreviousText);

            return this;
        }

        #endregion



        #region Delete Booking Dialogue

        public CreateScheduleBookingPopup WaitForDeleteBookingDynamicDialogueToLoad(bool IsReason = true)
        {
            WaitForElementVisible(header_DeleteBookingDialogue);

            if (IsReason)
            {
                WaitForElementVisible(reasonForDeleteOutterDiv);
                WaitForElementVisible(comments_DeleteBookingDialogue);
            }
            else
                WaitForElementVisible(deleteAlertMessage);

            WaitForElementVisible(cancelButton_DeleteBookingDialogue);
            WaitForElementVisible(deleteButton_DeleteBookingDialogue);

            return this;
        }

        public CreateScheduleBookingPopup WaitForDeleteBookingDynamicDialoguePopupClosed(bool IsReason = true)
        {
            WaitForElementNotVisible(header_DeleteBookingDialogue, 3);
            if (IsReason)
            {
                WaitForElementNotVisible(reasonForDeleteOutterDiv, 3);
                WaitForElementNotVisible(comments_DeleteBookingDialogue, 3);
            }
            else
                WaitForElementNotVisible(deleteAlertMessage, 3);

            return this;
        }

        public CreateScheduleBookingPopup ValidateReasonForDeleteMandatoryIconVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(reasonForDeleteMandatoryIcon_DeleteBookingDialogue);
            else
                WaitForElementNotVisible(reasonForDeleteMandatoryIcon_DeleteBookingDialogue, 3);

            return this;
        }

        public CreateScheduleBookingPopup ValidateCommentsMandatoryIconVisible_DeleteBookingDialouge(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(commentsMandatoryIcon_DeleteBookingDialogue);
            else
                WaitForElementNotVisible(commentsMandatoryIcon_DeleteBookingDialogue, 3);

            return this;
        }

        public CreateScheduleBookingPopup SelectReasonForDeletePicklistOption(string TextToSelect)
        {
            WaitForElementToBeClickable(reasonForDeleteOutterDiv);
            Click(reasonForDeleteOutterDiv);

            WaitForElementToBeClickable(reasonForDeletePicklistOption(TextToSelect));
            Click(reasonForDeletePicklistOption(TextToSelect));

            return this;
        }

        public CreateScheduleBookingPopup ValidateSelectedReasonForDeletePickListOption(string expectedText)
        {
            WaitForElementVisible(reasonForDeleteDropDownText);
            ScrollToElement(reasonForDeleteDropDownText);
            ValidateElementText(reasonForDeleteDropDownText, expectedText);

            return this;
        }

        public CreateScheduleBookingPopup InsertTextInComments_DeleteBookingDynamicDialogue(string TextToInsert)
        {
            WaitForElementToBeClickable(comments_DeleteBookingDialogue);
            ScrollToElement(comments_DeleteBookingDialogue);
            SendKeys(comments_DeleteBookingDialogue, TextToInsert);

            return this;
        }

        public CreateScheduleBookingPopup ClickCancelButton_DeleteBookingDynamicDialogue()
        {
            WaitForElementToBeClickable(cancelButton_DeleteBookingDialogue);
            Click(cancelButton_DeleteBookingDialogue);

            return this;
        }

        public CreateScheduleBookingPopup ClickDeleteButton_DeleteBookingDynamicDialogue()
        {
            WaitForElementToBeClickable(deleteButton_DeleteBookingDialogue);
            Click(deleteButton_DeleteBookingDialogue);

            return this;
        }

        public CreateScheduleBookingPopup ValidateDeleteAlertMessage(string expectedText)
        {
            WaitForElementVisible(deleteAlertMessage);
            ScrollToElement(deleteAlertMessage);
            ValidateElementText(deleteAlertMessage, expectedText);

            return this;
        }

        #endregion

        #region Delete Booking Dialogue

        public CreateScheduleBookingPopup WaitForWarningDialogueToLoad()
        {
            WaitForElementVisible(header_WarningDialogue);

            WaitForElementVisible(WarningBody);
            WaitForElementVisible(dismissButton_WarningDialogue);
            WaitForElementVisible(saveButton_WarningDialogue);

            return this;
        }

        public CreateScheduleBookingPopup ValidateWarningAlertMessage(string expectedText)
        {
            WaitForElementVisible(WarningBody);
            ScrollToElement(WarningBody);
            ValidateElementText(WarningBody, expectedText);

            return this;
        }

        public CreateScheduleBookingPopup ValidateWarningAlertMessageLine(string expectedText, int LineNumber)
        {
            ValidateElementText(WarningBodyMessageLine(LineNumber), expectedText);

            return this;
        }

        public CreateScheduleBookingPopup ClickDismissButton_WarningDialogue()
        {
            WaitForElementToBeClickable(dismissButton_WarningDialogue);
            Click(dismissButton_WarningDialogue);

            return this;
        }

        public CreateScheduleBookingPopup ClickConfirmButton_WarningDialogue()
        {
            WaitForElementToBeClickable(saveButton_WarningDialogue);
            Click(saveButton_WarningDialogue);

            return this;
        }

        public CreateScheduleBookingPopup ValidateWarningDialogueNotVisibile()
        {
            WaitForElementNotVisible(header_WarningDialogue, 3);

            return this;
        }

        #endregion

        public CreateScheduleBookingPopup ClickSaveChangesBookingButton()
        {
            WaitForElementToBeClickable(saveChangeBookingButton);
            Click(saveChangeBookingButton);

            return this;
        }

        public CreateScheduleBookingPopup ValidateBookingDurationText(string ExpectedText)
        {
            ScrollToElement(bookingDurationText);
            var elementText = GetElementText(bookingDurationText);
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        //validate employee contract info text
        public CreateScheduleBookingPopup ValidateEmployeeContractInfoText(string ExpectedText)
        {
            WaitForElementVisible(employeeContractInfoText);
            var elementText = GetElementText(employeeContractInfoText);
            Assert.IsTrue(elementText.Contains(ExpectedText));

            return this;
        }
    }
}
