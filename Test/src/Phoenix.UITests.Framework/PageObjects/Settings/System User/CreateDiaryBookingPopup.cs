using Microsoft.Office.Interop.Word;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.WebAppAPI.Entities.CareDirector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CreateDiaryBookingPopup : CommonMethods
    {
        public CreateDiaryBookingPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CreateDiaryBookingPopupHeader = By.XPath("//h2[text()='Create Booking']");
        readonly By dialogHeader_EditBooking = By.XPath("//h2[text()='Edit Booking']");

        readonly By RosteringTab = By.XPath("//*[@id='id--booking-drawer--tabs']//span[text()='Rostering']");
        readonly By AdminTab = By.XPath("//*[@id='id--booking-drawer--tabs']//span[text()='Admin']");
        readonly By staffTab = By.XPath("//*[@id='id--booking-drawer--tabs']//span[text()='Staff']");
        readonly By costsTab = By.XPath("//*[@id='id--booking-drawer--tabs']//span[text()='Costs']");
        readonly By chargesTab = By.XPath("//*[@id='id--booking-drawer--tabs']//span[text()='Charges']");

        readonly By closeButton = By.XPath("//div[@id='id--footer--closeButton']");
        readonly By deleteButton = By.XPath("//*[@id='id--footer--deleteButton']");
        readonly By editSelectedStaffButton = By.XPath("//button/span[text() = 'Manage Staff']");
        readonly By createBookingButton = By.XPath("//div[@id='id--footer--saveButton']");

        #region Rostering Tab

        readonly By locationProvider_LabelField = By.XPath("//*[@id='id--rostering--provider']/label[text()='Location / Provider']");
        readonly By locationProvider_MandatoryField = By.XPath("//*[@id='id--rostering--provider']/label/span[@class='mandatory']");
        readonly By locationProviderMainDiv = By.XPath("//*[@id='id--rostering--provider']//*[@class = 'mcc-dropdown']");
        readonly By locationProviderInput = By.XPath("//*[@id = 'id--rostering--provider--dropdown-filter']");
        By locationProviderResultEntry(string TextToSelect) => By.XPath("//div[@id = 'id--rostering--provider']//*[text()='" + TextToSelect + "']");
        readonly By Location_ProviderText = By.XPath("//div[@id = 'id--rostering--provider']//div[@class='cd-dropdown-display-text']");


        readonly By bookingType_LabelField = By.XPath("//*[@id='id--rostering--booking-type']/label[text()='Booking Type']");
        readonly By bookingType_MandatoryField = By.XPath("//*[@id='id--rostering--booking-type']/label/span[@class='mandatory']");
        readonly By bookingTypeMainDiv = By.XPath("//*[@id='id--rostering--booking-type']/details");
        readonly By BookingTypeDropDownText = By.XPath("//div[@id = 'id--rostering--booking-type']//div[@class='cd-dropdown-display-text']");
        By BookingTypePicklistOption(string RecordText) => By.XPath("//*[@id='id--rostering--booking-type']//*[@title='" + RecordText + "']");


        readonly By StartDate_CalendarIcon = By.XPath("//*[@id='id--rostering--start-date--anchor']");
        readonly By StartDate_Calendar_MonthPicklist = By.XPath("//*[@id='id--rostering--start-date']//select[@aria-label='Month']");
        readonly By StartDate_Calendar_YearInput = By.XPath("//*[@id='id--rostering--start-date']//input[@aria-label='Year']");
        By StartDate_Calendar_Date(string Date) => By.XPath("//*[@id='id--rostering--start-date']//span[@aria-label='" + Date + "']");

        readonly By EndDate_CalendarIcon = By.XPath("//*[@id='id--rostering--end-date--anchor']");
        readonly By EndDate_Calendar_MonthPicklist = By.XPath("//*[@id='id--rostering--end-date']//select[@aria-label='Month']");
        readonly By EndDate_Calendar_YearInput = By.XPath("//*[@id='id--rostering--end-date']//input[@aria-label='Year']");
        By EndDate_Calendar_Date(string Date) => By.XPath("//*[@id='id--rostering--end-date']//span[@aria-label='" + Date + "']");

        readonly By startDate_LabelField = By.XPath("//*[@id='id--rostering--start-date']/label[text()='Start Date']");
        readonly By startDate_MandatoryField = By.XPath("//*[@id='id--rostering--start-date']/label/span[@class='mandatory']");
        static string scheduleBookingStartDateInputId = "id--rostering--start-date--date-picker";
        readonly By startDate_Field = By.XPath("//input[@id='" + scheduleBookingStartDateInputId + "']");

        readonly By endDate_LabelField = By.XPath("//*[@id='id--rostering--end-date']/label[text()='End Date']");
        readonly By endDate_MandatoryField = By.XPath("//*[@id='id--rostering--end-date']/label/span[@class='mandatory']");
        static string scheduleBookingEndDateInputId = "id--rostering--end-date--date-picker";
        readonly By endDate_Field = By.XPath("//input[@id='" + scheduleBookingEndDateInputId + "']");

        readonly By startTime_LabelField = By.XPath("//*[@id='id--rostering--start-time']/label[text()='Start Time']");
        readonly By startTime_MandatoryField = By.XPath("//*[@id='id--rostering--start-time']/label/span[@class='mandatory']");
        static string scheduleBookingStartTimeInputId = "id--rostering--start-time--time-picker";
        readonly By startTimePicker = By.XPath("//input[@id='" + scheduleBookingStartTimeInputId + "']");
        readonly By startTimeHourInput = By.XPath("//*[@id='id--rostering--start-time']//input[@class='numInput flatpickr-hour']");
        readonly By startTimeMinuteInput = By.XPath("//*[@id='id--rostering--start-time']//input[@class='numInput flatpickr-minute']");

        readonly By endTime_LabelField = By.XPath("//*[@id='id--rostering--end-time']/label[text()='End Time']");
        readonly By endTime_MandatoryField = By.XPath("//*[@id='id--rostering--end-time']/label/span[@class='mandatory']");
        static string scheduleBookingEndTimeInputId = "id--rostering--end-time--time-picker";
        readonly By endTimePicker = By.XPath("//input[@id='" + scheduleBookingEndTimeInputId + "']");
        readonly By endTimeHourInput = By.XPath("//*[@id='id--rostering--end-time']//input[@class='numInput flatpickr-hour']");
        readonly By endTimeMinuteInput = By.XPath("//*[@id='id--rostering--end-time']//input[@class='numInput flatpickr-minute']");
        readonly By selectPeopleButton = By.XPath("//*[@id='id--rostering--people-layer--button-open']/button");
        By removePersonButton(string PersonContractId) => By.XPath("//*[@id='id--rostering--people-layer--chips']//span[@data-id='" + PersonContractId + "']//mcc-icon");

        readonly By notificationArea = By.XPath("//*[@id='cd-sub-drawer']");
        readonly By notificationArea_Title = By.XPath("//*[@id='cd-sub-drawer']//h2");
        readonly By notificationArea_Message = By.XPath("//*[@id='id--error-messages--list--0']");
        readonly By notificationArea_DismissButton = By.XPath("//*[@id='id--sub-drawer--dismiss-button']/button");

        readonly By comments_LabelField = By.XPath("//*[@id='id--rostering--comments']/label[text()='Comments']");
        readonly By comments_MandatoryField = By.XPath("//*[@id='id--rostering--comments']/label/span[@class='mandatory']");
        readonly By commentsTextArea = By.XPath("//textarea[@id = 'id--rostering--comments--textarea']");

        readonly By addUnassignedStaffButton = By.XPath("//button/span[text() = 'Add Unassigned']");

        By selectedStaffText(string RecordId, string StaffName) => By.XPath("//*[@id='id--rostering--0--selected']//span[@data-id='" + RecordId + "']/span[text() = '" + StaffName + "']");

        By removeStaffFromSelectedStaffField(string EmploymentContractId) => By.XPath("//*[@data-id='" + EmploymentContractId + "']/mcc-icon");

        readonly By employeeContractInfoText = By.XPath("//*[@id = 'id--rostering--employee-contract']");

        #endregion

        #region Staff Tab

        readonly By confirmAllStaffCheckbox_StaffTab = By.XPath("//*[@id='id--staff--confirm-all--checkbox']");
        readonly By topMessage_StaffTab = By.XPath("//*[@id='id--staff--rostering-summary']");
        By bookingCostInput_StaffTab(string EmploymnetContractId) => By.XPath("//*[@id='id--" + EmploymnetContractId + "--booking_cost--decimal']");
        By breakTimeInput_StaffTab(string EmploymnetContractId) => By.XPath("//*[@id='id--" + EmploymnetContractId + "--break_time--decimal']");
        By paidHoursInput_StaffTab(string EmploymnetContractId) => By.XPath("//*[@id='id--" + EmploymnetContractId + "--paid_hours--input']");
        By masterPayArrangementsInput_StaffTab(string EmploymnetContractId) => By.XPath("//*[@id='id--" + EmploymnetContractId + "--mpa--input']");
        By confirmStaffMemberCheckbox_StaffTab(string EmploymnetContractId) => By.XPath("//*[@id='id--" + EmploymnetContractId + "--confirm_staff--checkbox']");

        #endregion

        #region Charges Tab

        By personArea(string PersonContractId) => By.XPath("//*[@id='id--charges--charges--" + PersonContractId + "']");
        By bookingChargeTextArea(string PersonContractId) => By.XPath("//*[@id='id--" + PersonContractId + "--booking-charge--decimal']");
        By financeCodeTextArea(string PersonContractId) => By.XPath("//*[@id='id--" + PersonContractId + "--finance-code--input']");
        By contractServiceTextArea(string PersonContractId) => By.XPath("//*[@id='id--" + PersonContractId + "--contract-service--input']");

        #endregion

        #region admin Tab

        readonly By DateOfCancellationFld = By.XPath("//input[@id='id--admin--date-of-cancellation--date-picker']");
        readonly By TimeOfCancellationFld = By.XPath("//input[@id='id--admin--time-of-cancellation--time-picker']");
        readonly By ReasonForCancellation = By.XPath("//div[@id='id--cancelled--reason-for-cancellation']//div[@class='cd-dropdown-display-text']");
        readonly By cancellationNotes = By.XPath("//*[@id = 'id--admin--cancellation-note--textarea']");

        #endregion

        #region History Tab

        //xpaths for HistoryTab
        readonly By HistoryTab = By.XPath("//*[@id='id--booking-drawer--tabs']//span[text()='History']");

        readonly By TopLabel_HistoryTab = By.XPath("//*[@id='id--history--rostering-summary']");

        readonly By HistorySection = By.XPath("//*[@id='id--history--history']");

        By ExpandSection(int position) => By.XPath("//div[@id='id--history--history']/div[@class='cd-history'][" + position + "]/div[@class='cd-history-title-bar']");

        By ExpandField(string fieldName) => By.XPath("//div[@id='id--history--history']/div[@class='cd-history expanded']/div[@class='cd-history-field']//span[text()='" + fieldName + "']");

        By ExpandedField(string fieldName) => By.XPath("//div[@id='id--history--history']/div[@class='cd-history expanded']/div[@class='cd-history-field expanded']//span[text()='" + fieldName + "']");

        By CurrentValue(string fieldName) => By.XPath("//div[@id='id--history--history']//div[@class='cd-history-field expanded']//span[text()='" + fieldName + "']/parent::div/parent::div/span[1]");

        By PreviousValue(string fieldName) => By.XPath("//div[@id='id--history--history']//div[@class='cd-history-field expanded']//span[text()='" + fieldName + "']/parent::div/parent::div/span[2]");

        #endregion

        #region Delete Booking Dialogue

        readonly By header_DeleteBookingDialogue = By.XPath("//*[@id='cd-sub-drawer']//h2[text()='Delete Booking']");
        readonly By reasonForDeleteMandatoryIcon_DeleteBookingDialogue = By.XPath("//*[@id='id--deleteContent--delete-reasons']//span[@class='mandatory']");
        readonly By reasonForDeleteOutterDiv = By.XPath("//*[@id='id--deleteContent--delete-reasons']//details");
        readonly By reasonForDeleteDropDownText = By.XPath("//*[@id='id--deleteContent--delete-reasons']//*[@class='cd-dropdown-display-text']");

        By reasonForDeletePicklistOption(string RecordText) => By.XPath("//*[@id='id--deleteContent--delete-reasons']//*[@title='" + RecordText + "']");

        readonly By comments_DeleteBookingDialogue = By.XPath("//*[@id='id--deleteContent--delete-comment--textarea']");
        readonly By commentsMandatoryIcon_DeleteBookingDialogue = By.XPath("//div[@id='id--deleteContent--delete-comment']//span[@class='mandatory']");

        readonly By deleteAlertMessage = By.Id("id--deleteContent--delete-info");

        readonly By cancelButton_DeleteBookingDialogue = By.XPath("//*[@id='id--sub-drawer--footer-dismiss']/button");
        readonly By deleteButton_DeleteBookingDialogue = By.XPath("//*[@id='id--sub-drawer--footer-positive']/button");

        #endregion

        #region Dynamic Dialogue

        readonly By header_ErrorDialogue = By.XPath("//*[@id='cd-sub-drawer']//h2");
        readonly By closeButton_ErrorDialogue = By.XPath("//*[@id='cd-sub-drawer']/div/div/button");

        By message_ErrorDialogue(int messagePosition) => By.XPath("//*[@id='id--booking-drawer--list']/ul/li[" + messagePosition + "]");

        By issueListEntry_ErrorDialogue(int messagePosition) => By.XPath("//*[@id='id--issue--list--" + messagePosition + "']");
        readonly By dismissButton_ErrorDialogue = By.XPath("//*[@id='id--sub-drawer--footer-dismiss']/button");
        readonly By saveButton_ErrorDialogue = By.XPath("//*[@id='id--sub-drawer--footer-positive']/button");

        #endregion

        #region Rostering Tab

        public CreateDiaryBookingPopup WaitForCreateDiaryBookingPopupToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(CreateDiaryBookingPopupHeader);
            WaitForElementVisible(RosteringTab);

            return this;
        }

        public CreateDiaryBookingPopup WaitForEditDiaryBookingPopupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(dialogHeader_EditBooking);
            WaitForElement(closeButton);
            WaitForElement(deleteButton);
            WaitForElement(createBookingButton);

            return this;
        }

        public CreateDiaryBookingPopup ClickOnRosteringTab()
        {
            WaitForElementToBeClickable(RosteringTab);
            Click(RosteringTab);

            return this;
        }

        public CreateDiaryBookingPopup SelectLocationProvider(string TextToSelect)
        {
            WaitForElementToBeClickable(locationProviderMainDiv);
            Click(locationProviderMainDiv);

            //we have a problem here, the input element is only visible if we have lots of providers and the picklist needs scrolling
            System.Threading.Thread.Sleep(1000);
            var elementVisible = GetElementVisibility(locationProviderInput);
            if (elementVisible)
                SendKeys(locationProviderInput, TextToSelect);

            WaitForElementToBeClickable(locationProviderResultEntry(TextToSelect));
            Click(locationProviderResultEntry(TextToSelect));

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

        public CreateDiaryBookingPopup ClickSelectPeopleButton()
        {
            WaitForElementToBeClickable(selectPeopleButton);
            Click(selectPeopleButton);

            return this;
        }

        public CreateDiaryBookingPopup RemoveSelectedPerson(string PersonContractId)
        {
            WaitForElementToBeClickable(removePersonButton(PersonContractId));
            Click(removePersonButton(PersonContractId));

            return this;
        }

        public CreateDiaryBookingPopup RemoveSelectedPerson(Guid PersonContractId)
        {
            return RemoveSelectedPerson(PersonContractId.ToString());
        }

        public CreateDiaryBookingPopup SelectStartDate(string Year, string Month, string Day)
        {
            WaitForElementToBeClickable(StartDate_CalendarIcon);
            MoveToElementInPage(StartDate_CalendarIcon);
            Click(StartDate_CalendarIcon);

            WaitForElementToBeClickable(StartDate_Calendar_MonthPicklist);
            MoveToElementInPage(StartDate_Calendar_MonthPicklist);
            SelectPicklistElementByText(StartDate_Calendar_MonthPicklist, Month);

            WaitForElementToBeClickable(StartDate_Calendar_YearInput);
            MoveToElementInPage(StartDate_Calendar_YearInput);
            SendKeys(StartDate_Calendar_YearInput, Year);

            var date = string.Format("{0} {1}, {2}", Month, Day, Year);
            WaitForElementToBeClickable(StartDate_Calendar_Date(date));
            MoveToElementInPage(StartDate_Calendar_Date(date));
            Click(StartDate_Calendar_Date(date));

            return this;
        }

        public CreateDiaryBookingPopup SelectEndDate(string Year, string Month, string Day)
        {
            WaitForElementToBeClickable(EndDate_CalendarIcon);
            MoveToElementInPage(EndDate_CalendarIcon);
            Click(EndDate_CalendarIcon);

            WaitForElementToBeClickable(EndDate_Calendar_MonthPicklist);
            MoveToElementInPage(EndDate_Calendar_MonthPicklist);
            SelectPicklistElementByText(EndDate_Calendar_MonthPicklist, Month);

            WaitForElementToBeClickable(EndDate_Calendar_YearInput);
            MoveToElementInPage(EndDate_Calendar_YearInput);
            SendKeys(EndDate_Calendar_YearInput, Year);

            var date = string.Format("{0} {1}, {2}", Month, Day, Year);
            WaitForElementToBeClickable(EndDate_Calendar_Date(date));
            MoveToElementInPage(EndDate_Calendar_Date(date));
            Click(EndDate_Calendar_Date(date));

            return this;
        }

        public CreateDiaryBookingPopup InsertStartTime(string hours, string minutes)
        {
            WaitForElementToBeClickable(startTimePicker);
            Click(startTimePicker);

            WaitForElementToBeClickable(startTimeHourInput);
            SendKeys(startTimeHourInput, hours + Keys.Tab);

            WaitForElementToBeClickable(startTimeMinuteInput);
            SendKeys(startTimeMinuteInput, minutes + Keys.Tab);

            return this;
        }

        public CreateDiaryBookingPopup InsertEndTime(string hours, string minutes)
        {
            WaitForElementToBeClickable(endTimePicker);
            Click(endTimePicker);

            WaitForElementToBeClickable(endTimeHourInput);
            SendKeys(endTimeHourInput, hours + Keys.Tab);

            WaitForElementToBeClickable(endTimeMinuteInput);
            SendKeys(endTimeMinuteInput, minutes + Keys.Tab + Keys.Tab);

            return this;
        }


        public CreateDiaryBookingPopup WaitForNotificationAreaToBeVisible()
        {
            WaitForElementVisible(notificationArea);

            return this;
        }

        public CreateDiaryBookingPopup ValidateNotificationAreaMessage(string ExpectedTitle, string ExpectedMessage)
        {
            System.Threading.Thread.Sleep(500);
            WaitForElementVisible(notificationArea_Title);
            ValidateElementText(notificationArea_Title, ExpectedTitle);

            WaitForElementVisible(notificationArea_Title);
            ValidateElementText(notificationArea_Message, ExpectedMessage);

            return this;
        }

        public CreateDiaryBookingPopup CLickNotificationAreaDismissButton()
        {
            WaitForElementToBeClickable(notificationArea_DismissButton);
            Click(notificationArea_DismissButton);

            return this;
        }

        //method to insert text in comments text area
        public CreateDiaryBookingPopup InsertTextInCommentsTextArea(string TextToInsert)
        {
            WaitForElementToBeClickable(commentsTextArea);
            MoveToElementInPage(commentsTextArea);
            SendKeys(commentsTextArea, TextToInsert);
            return this;
        }

        //method to validate text in comments text area
        public CreateDiaryBookingPopup ValidateTextInCommentsTextArea(string ExpectedText)
        {
            MoveToElementInPage(commentsTextArea);
            WaitForElementVisible(commentsTextArea);
            ValidateElementValue(commentsTextArea, ExpectedText);
            return this;
        }

        //validate comments text area is visible
        public CreateDiaryBookingPopup ValidateCommentsTextAreaIsVisible(bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(commentsTextArea);
                MoveToElementInPage(commentsTextArea);
            }
            else
            {
                WaitForElementNotVisible(commentsTextArea, 3);
            }

            return this;
        }

        //method to validate location provider text
        public CreateDiaryBookingPopup ValidateLocationProviderText(string ExpectedText)
        {
            WaitForElementVisible(locationProviderMainDiv);
            ScrollToElement(Location_ProviderText);
            var elementText = GetElementText(Location_ProviderText);
            Assert.AreEqual(ExpectedText, elementText);
            return this;
        }

        public CreateDiaryBookingPopup ValidateBookingTypeDropDownText(string ExpectedText)
        {
            var elementText = GetElementTextByJavascript(BookingTypeDropDownText);
            Assert.AreEqual(ExpectedText, elementText);
            return this;
        }

        #endregion

        #region Staff Tab

        public CreateDiaryBookingPopup ClickOnStaffTab()
        {
            WaitForElementToBeClickable(staffTab);
            Click(staffTab);

            return this;
        }

        public CreateDiaryBookingPopup WaitForStaffTabToLoad()
        {
            WaitForElementVisible(topMessage_StaffTab);

            return this;
        }

        public CreateDiaryBookingPopup ValidateBookingCost_StaffTab(string EmploymentContractId, string ExpectedValue)
        {
            ValidateElementValue(bookingCostInput_StaffTab(EmploymentContractId), ExpectedValue);

            return this;
        }

        public CreateDiaryBookingPopup ValidateBookingCost_StaffTab(Guid EmploymentContractId, string ExpectedValue)
        {
            return ValidateBookingCost_StaffTab(EmploymentContractId.ToString(), ExpectedValue);
        }

        public CreateDiaryBookingPopup ValidateBreakTime_StaffTab(string EmploymentContractId, string ExpectedValue)
        {
            ValidateElementValue(breakTimeInput_StaffTab(EmploymentContractId), ExpectedValue);

            return this;
        }

        public CreateDiaryBookingPopup ValidateBreakTime_StaffTab(Guid EmploymentContractId, string ExpectedValue)
        {
            return ValidateBreakTime_StaffTab(EmploymentContractId.ToString(), ExpectedValue);
        }

        public CreateDiaryBookingPopup ValidatePaidHours_StaffTab(string EmploymentContractId, string ExpectedValue)
        {
            ValidateElementValue(paidHoursInput_StaffTab(EmploymentContractId), ExpectedValue);

            return this;
        }

        public CreateDiaryBookingPopup ValidatePaidHours_StaffTab(Guid EmploymentContractId, string ExpectedValue)
        {
            return ValidatePaidHours_StaffTab(EmploymentContractId.ToString(), ExpectedValue);
        }

        public CreateDiaryBookingPopup ValidateMasterPayArrangements_StaffTab(string EmploymentContractId, string ExpectedValue)
        {
            ValidateElementValue(masterPayArrangementsInput_StaffTab(EmploymentContractId), ExpectedValue);

            return this;
        }

        public CreateDiaryBookingPopup ValidateMasterPayArrangements_StaffTab(Guid EmploymentContractId, string ExpectedValue)
        {
            return ValidateMasterPayArrangements_StaffTab(EmploymentContractId.ToString(), ExpectedValue);
        }

        public CreateDiaryBookingPopup ClickConfirmStaffMemberCheckbox_StaffTab(string EmploymentContractId)
        {
            WaitForElementToBeClickable(confirmStaffMemberCheckbox_StaffTab(EmploymentContractId));
            Click(confirmStaffMemberCheckbox_StaffTab(EmploymentContractId));

            return this;
        }

        public CreateDiaryBookingPopup ClickConfirmStaffMemberCheckbox_StaffTab(Guid EmploymentContractId)
        {
            return ClickConfirmStaffMemberCheckbox_StaffTab(EmploymentContractId.ToString());
        }

        #endregion

        #region Charges Tab

        public CreateDiaryBookingPopup ClickOnChargesTab()
        {
            WaitForElementToBeClickable(chargesTab);
            Click(chargesTab);

            return this;
        }

        public CreateDiaryBookingPopup ClickPersonArea_ChargeTab(string PersonContractId)
        {
            WaitForElementToBeClickable(personArea(PersonContractId));
            Click(personArea(PersonContractId));

            return this;
        }

        public CreateDiaryBookingPopup ClickPersonArea_ChargeTab(Guid PersonContractId)
        {
            return ClickPersonArea_ChargeTab(PersonContractId.ToString());
        }

        public CreateDiaryBookingPopup ValidateBookingChargeValue_ChargeTab(string PersonContractId, string ExpectedValue)
        {
            WaitForElementVisible(bookingChargeTextArea(PersonContractId));
            ValidateElementValue(bookingChargeTextArea(PersonContractId), ExpectedValue);

            return this;
        }

        public CreateDiaryBookingPopup ValidateBookingChargeValue_ChargeTab(Guid PersonContractId, string ExpectedValue)
        {
            return ValidateBookingChargeValue_ChargeTab(PersonContractId.ToString(), ExpectedValue);
        }

        public CreateDiaryBookingPopup ValidateFinanceCodeValue_ChargeTab(string PersonContractId, string ExpectedValue)
        {
            WaitForElementVisible(financeCodeTextArea(PersonContractId));
            ValidateElementValue(financeCodeTextArea(PersonContractId), ExpectedValue);

            return this;
        }

        public CreateDiaryBookingPopup ValidateFinanceCodeValue_ChargeTab(Guid PersonContractId, string ExpectedValue)
        {
            return ValidateFinanceCodeValue_ChargeTab(PersonContractId.ToString(), ExpectedValue);
        }

        public CreateDiaryBookingPopup ValidateContractServiceValue_ChargeTab(string PersonContractId, string ExpectedValue)
        {
            WaitForElementVisible(contractServiceTextArea(PersonContractId));
            ValidateElementValue(contractServiceTextArea(PersonContractId), ExpectedValue);

            return this;
        }

        public CreateDiaryBookingPopup ValidateContractServiceValue_ChargeTab(Guid PersonContractId, string ExpectedValue)
        {
            return ValidateContractServiceValue_ChargeTab(PersonContractId.ToString(), ExpectedValue);
        }

        #endregion

        #region Warning Dialogue

        readonly By header_WarningDialogue = By.XPath("//*[@id='cd-sub-drawer']//h2");
        readonly By WarningBody = By.XPath("//*[@id='cd-sub-drawer']//div[@class='mcc-dialog-drawer__body']");
        readonly By dismissButton_WarningDialogue = By.XPath("//*[@id='id--sub-drawer--footer-dismiss']/button");
        readonly By saveButton_WarningDialogue = By.XPath("//*[@id='id--sub-drawer--footer-positive']/button");

        #endregion

        public CreateDiaryBookingPopup ClickOnCloseButton()
        {
            WaitForElement(closeButton);
            ScrollToElement(closeButton);
            Click(closeButton);
            return this;
        }

        public CreateDiaryBookingPopup ClickEditSelectedStaff()
        {
            WaitForElementToBeClickable(editSelectedStaffButton);
            MoveToElementInPage(editSelectedStaffButton);
            Click(editSelectedStaffButton);
            return this;
        }

        public CreateDiaryBookingPopup ClickCreateBooking()
        {
            WaitForElementToBeClickable(createBookingButton);
            Click(createBookingButton);
            return this;
        }

        public CreateDiaryBookingPopup ClickAdminTab()
        {
            WaitForElementToBeClickable(AdminTab);
            Click(AdminTab);
            return this;

        }

        public CreateDiaryBookingPopup ValidateDateofCancellation(string ExpectedText)
        {

            ValidateElementValueByJavascript("id--cancelled--date-of-cancellation--date-picker", ExpectedText);
            return this;
        }

        public CreateDiaryBookingPopup ValidateTimeofCancellation(string ExpectedText)
        {
            ValidateElementValueByJavascript("id--cancelled--time-of-cancellation--time-picker", ExpectedText);
            return this;
        }

        public CreateDiaryBookingPopup ValidateReasonofCancellation(string ExpectedText)
        {
            var elementText = GetElementText(ReasonForCancellation);
            Assert.AreEqual(ExpectedText, elementText);
            return this;
        }

        public CreateDiaryBookingPopup SelectBookingType(string TextToSelect)
        {
            WaitForElementToBeClickable(bookingTypeMainDiv);
            Click(bookingTypeMainDiv);

            WaitForElementToBeClickable(BookingTypePicklistOption(TextToSelect));
            Click(BookingTypePicklistOption(TextToSelect));

            return this;
        }

        public CreateDiaryBookingPopup ValidateLocation_ProviderMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(locationProvider_LabelField);
            MoveToElementInPage(locationProvider_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(locationProvider_MandatoryField);
            else
                WaitForElementNotVisible(locationProvider_MandatoryField, 3);

            return this;
        }

        public CreateDiaryBookingPopup ValidateBookingTypeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(bookingType_LabelField);
            MoveToElementInPage(bookingType_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(bookingType_MandatoryField);
            else
                WaitForElementNotVisible(bookingType_MandatoryField, 3);

            return this;
        }

        public CreateDiaryBookingPopup ValidateStartDateMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(startDate_LabelField);
            MoveToElementInPage(startDate_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(startDate_MandatoryField);
            else
                WaitForElementNotVisible(startDate_MandatoryField, 3);

            return this;
        }

        public CreateDiaryBookingPopup ValidateStartTimeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(startTime_LabelField);
            MoveToElementInPage(startTime_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(startTime_MandatoryField);
            else
                WaitForElementNotVisible(startTime_MandatoryField, 3);

            return this;
        }

        public CreateDiaryBookingPopup ValidateEndDateMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(endDate_LabelField);
            MoveToElementInPage(endDate_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(endDate_MandatoryField);
            else
                WaitForElementNotVisible(endDate_MandatoryField, 3);

            return this;
        }

        public CreateDiaryBookingPopup ValidateEndTimeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(endTime_LabelField);
            MoveToElementInPage(endTime_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(endTime_MandatoryField);
            else
                WaitForElementNotVisible(endTime_MandatoryField, 3);

            return this;
        }

        public CreateDiaryBookingPopup ValidateCommentsMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(comments_LabelField);
            MoveToElementInPage(comments_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(comments_MandatoryField);
            else
                WaitForElementNotVisible(comments_MandatoryField, 3);

            return this;
        }

        public CreateDiaryBookingPopup ValidateStartDate(string ExpectedStartDate)
        {
            WaitForElementToBeClickable(startDate_Field);
            MoveToElementInPage(startDate_Field);
            var startDate = GetElementValueByJavascript(scheduleBookingStartDateInputId);
            Assert.AreEqual(ExpectedStartDate, startDate);

            return this;
        }

        public CreateDiaryBookingPopup ValidateStartTime(string ExpectedStartTime)
        {
            WaitForElementToBeClickable(startTimePicker);
            MoveToElementInPage(startTimePicker);
            var startTime = GetElementValueByJavascript(scheduleBookingStartTimeInputId);
            Assert.AreEqual(ExpectedStartTime, startTime);

            return this;
        }

        public CreateDiaryBookingPopup ValidateEndDate(string ExpectedEndDate)
        {
            WaitForElementToBeClickable(endDate_Field);
            MoveToElementInPage(endDate_Field);
            var endDate = GetElementValueByJavascript(scheduleBookingEndDateInputId);
            Assert.AreEqual(ExpectedEndDate, endDate);

            return this;
        }

        public CreateDiaryBookingPopup ValidateEndTime(string ExpectedEndTime)
        {
            WaitForElementToBeClickable(endTimePicker);
            MoveToElementInPage(endTimePicker);
            var endTime = GetElementValueByJavascript(scheduleBookingEndTimeInputId);
            Assert.AreEqual(ExpectedEndTime, endTime);

            return this;
        }

        public CreateDiaryBookingPopup ClickAddUnassignedStaff()
        {
            WaitForElementToBeClickable(addUnassignedStaffButton);
            MoveToElementInPage(addUnassignedStaffButton);
            Click(addUnassignedStaffButton);

            return this;
        }

        public CreateDiaryBookingPopup ClickOnDeleteButton()
        {
            WaitForElement(deleteButton);
            ClickSpecial(deleteButton);

            return this;
        }

        public CreateDiaryBookingPopup WaitForCreateDiaryBookingPopupClosed()
        {
            WaitForElementNotVisible("CWRefreshPanel", 30);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementNotVisible(CreateDiaryBookingPopupHeader, 20);
            WaitForElementNotVisible(closeButton, 20);
            WaitForElementNotVisible(createBookingButton, 20);

            return this;
        }

        public CreateDiaryBookingPopup WaitForEditDiaryBookingPopupClosed()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible(dialogHeader_EditBooking, 7);
            WaitForElementNotVisible(closeButton, 7);
            WaitForElementNotVisible(deleteButton, 7);
            WaitForElementNotVisible(createBookingButton, 7);

            return this;
        }

        public CreateDiaryBookingPopup ValidateBookingTypeIsPresent(string BookingTypeName, bool IsPresent)
        {
            WaitForElementToBeClickable(bookingTypeMainDiv);
            Click(bookingTypeMainDiv);

            if (IsPresent)
                WaitForElementVisible(BookingTypePicklistOption(BookingTypeName));
            else
                WaitForElementNotVisible(BookingTypePicklistOption(BookingTypeName), 3);

            Click(bookingTypeMainDiv);

            return this;
        }


        #region History Tab

        public CreateDiaryBookingPopup ValidateHistoryTabIsVisible(bool ExpectedVisible = true)
        {
            if (ExpectedVisible)
                WaitForElementVisible(HistoryTab);
            else
                WaitForElementNotVisible(HistoryTab, 3);

            return this;
        }

        public CreateDiaryBookingPopup ClickHistoryTab()
        {
            WaitForElementToBeClickable(HistoryTab);
            MoveToElementInPage(HistoryTab);
            Click(HistoryTab);

            return this;
        }

        public CreateDiaryBookingPopup WaitForHistoryTabToLoad()
        {
            WaitForElementVisible(TopLabel_HistoryTab);
            WaitForElementVisible(HistorySection);

            WaitForElementVisible(closeButton);
            WaitForElementVisible(deleteButton);
            WaitForElementVisible(createBookingButton);

            return this;
        }

        public CreateDiaryBookingPopup ValidateHeaderOnHistoryTab(string ExpextedText)
        {
            WaitForElementVisible(TopLabel_HistoryTab);
            MoveToElementInPage(TopLabel_HistoryTab);
            ValidateElementTextContainsText(TopLabel_HistoryTab, ExpextedText);

            return this;
        }

        public CreateDiaryBookingPopup ExpandHistoryDetailSection(int position)
        {
            WaitForElementToBeClickable(ExpandSection(position));
            MoveToElementInPage(ExpandSection(position));
            Click(ExpandSection(position));

            return this;
        }

        public CreateDiaryBookingPopup ExpandSpecificField(string FieldName)
        {
            WaitForElementToBeClickable(ExpandField(FieldName));
            MoveToElementInPage(ExpandField(FieldName));
            Click(ExpandField(FieldName));

            return this;
        }

        public CreateDiaryBookingPopup ValidateExpandedFieldIsVisible(string FieldName, bool IsVisible)
        {
            if (IsVisible)
                WaitForElementVisible(ExpandedField(FieldName));
            else
                WaitForElementNotVisible(ExpandedField(FieldName), 5);

            return this;
        }

        public CreateDiaryBookingPopup ValidateCurrentAndPreviousValue(string FieldName, string CurrentText, string PreviousText)
        {
            WaitForElementVisible(CurrentValue(FieldName));
            MoveToElementInPage(CurrentValue(FieldName));
            ValidateElementText(CurrentValue(FieldName), "Current: " + CurrentText);

            WaitForElementVisible(PreviousValue(FieldName));
            MoveToElementInPage(PreviousValue(FieldName));
            if (PreviousText == "")
                ValidateElementText(PreviousValue(FieldName), "Previous:" + PreviousText);
            else
                ValidateElementText(PreviousValue(FieldName), "Previous: " + PreviousText);

            return this;
        }

        #endregion

        #region Delete Booking Dialogue

        public CreateDiaryBookingPopup WaitForDeleteBookingDynamicDialogueToLoad(bool IsReason = true)
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

        public CreateDiaryBookingPopup WaitForDeleteBookingDynamicDialoguePopupClosed(bool IsReason = true)
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

        public CreateDiaryBookingPopup ValidateReasonForDeleteMandatoryIconVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(reasonForDeleteMandatoryIcon_DeleteBookingDialogue);
            else
                WaitForElementNotVisible(reasonForDeleteMandatoryIcon_DeleteBookingDialogue, 3);

            return this;
        }

        public CreateDiaryBookingPopup ValidateCommentsMandatoryIconVisible_DeleteBookingDialouge(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(commentsMandatoryIcon_DeleteBookingDialogue);
            else
                WaitForElementNotVisible(commentsMandatoryIcon_DeleteBookingDialogue, 3);

            return this;
        }

        public CreateDiaryBookingPopup SelectReasonForDeletePicklistOption(string TextToSelect)
        {
            WaitForElementToBeClickable(reasonForDeleteOutterDiv);
            Click(reasonForDeleteOutterDiv);

            WaitForElementToBeClickable(reasonForDeletePicklistOption(TextToSelect));
            Click(reasonForDeletePicklistOption(TextToSelect));

            return this;
        }

        public CreateDiaryBookingPopup ValidateSelectedReasonForDeletePickListOption(string expectedText)
        {
            WaitForElementVisible(reasonForDeleteDropDownText);
            MoveToElementInPage(reasonForDeleteDropDownText);
            ValidateElementText(reasonForDeleteDropDownText, expectedText);

            return this;
        }

        public CreateDiaryBookingPopup InsertTextInComments_DeleteBookingDynamicDialogue(string TextToInsert)
        {
            WaitForElementToBeClickable(comments_DeleteBookingDialogue);
            MoveToElementInPage(comments_DeleteBookingDialogue);
            SendKeys(comments_DeleteBookingDialogue, TextToInsert);

            return this;
        }

        public CreateDiaryBookingPopup ClickCancelButton_DeleteBookingDynamicDialogue()
        {
            WaitForElementToBeClickable(cancelButton_DeleteBookingDialogue);
            Click(cancelButton_DeleteBookingDialogue);

            return this;
        }

        public CreateDiaryBookingPopup ClickDeleteButton_DeleteBookingDynamicDialogue()
        {
            WaitForElementToBeClickable(deleteButton_DeleteBookingDialogue);
            Click(deleteButton_DeleteBookingDialogue);

            return this;
        }

        public CreateDiaryBookingPopup ValidateDeleteAlertMessage(string expectedText)
        {
            WaitForElementVisible(deleteAlertMessage);
            MoveToElementInPage(deleteAlertMessage);
            ValidateElementText(deleteAlertMessage, expectedText);

            return this;
        }

        #endregion

        public CreateDiaryBookingPopup ValidateDeleteButtonIsPresent(bool IsVisible = true)
        {
            if (IsVisible)
                WaitForElementVisible(deleteButton);
            else
                WaitForElementNotVisible(deleteButton, 3);

            return this;
        }

        #region Dynamic Dialogue

        public CreateDiaryBookingPopup WaitForDynamicDialogueToLoad()
        {
            SwitchToDefaultFrame();

            System.Threading.Thread.Sleep(1500);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(header_ErrorDialogue);
            WaitForElementVisible(dismissButton_ErrorDialogue);

            return this;
        }

        public CreateDiaryBookingPopup ValidateMessage_DynamicDialogue(string ExpectedText, int MessagePosition = 1)
        {
            ValidateElementTextContainsText(message_ErrorDialogue(MessagePosition), ExpectedText);

            return this;
        }

        public CreateDiaryBookingPopup ValidateIssueMessage_DynamicDialogue(string ExpectedText, int MessagePosition = 1)
        {
            ValidateElementText(issueListEntry_ErrorDialogue(MessagePosition), ExpectedText);

            return this;
        }

        public CreateDiaryBookingPopup ClickDismissButton_DynamicDialogue()
        {
            WaitForElementToBeClickable(dismissButton_ErrorDialogue);
            Click(dismissButton_ErrorDialogue);

            return this;
        }

        public CreateDiaryBookingPopup ClickSaveButton_DynamicDialogue()
        {
            WaitForElementToBeClickable(saveButton_ErrorDialogue);
            Click(saveButton_ErrorDialogue);

            return this;
        }

        #endregion

        public CreateDiaryBookingPopup VerifySelectedStaffRecordInStaffForBookingIsDisplayed(string RecordID, string StaffName, bool IsDisplayed = true)
        {
            if (IsDisplayed)
                WaitForElement(selectedStaffText(RecordID.ToString(), StaffName));
            else
                WaitForElementNotVisible(selectedStaffText(RecordID.ToString(), StaffName), 3);

            return this;
        }

        public CreateDiaryBookingPopup RemoveStaffFromSelectedStaffField(string EmploymentContractId)
        {
            WaitForElement(removeStaffFromSelectedStaffField(EmploymentContractId));
            Click(removeStaffFromSelectedStaffField(EmploymentContractId));

            return this;
        }

        public CreateDiaryBookingPopup RemoveStaffFromSelectedStaffField(Guid EmploymentContractId)
        {
            return RemoveStaffFromSelectedStaffField(EmploymentContractId.ToString());
        }

        public CreateDiaryBookingPopup OpenDuplicateTab()
        {
            OpenNewTabViaJavascript();

            return this;
        }

        public CreateDiaryBookingPopup SwitchToNewTab()
        {
            string currentWindow = GetCurrentWindowIdentifier();
            string popupWindow = GetAllWindowIdentifier().Where(c => c != currentWindow).FirstOrDefault();

            SwitchToWindow(popupWindow);

            NavigateToBrowserURL(appURL + "/pages/default.aspx");

            return this;
        }

        public CreateDiaryBookingPopup SwitchToPreviousTab()
        {
            string currentWindow = GetCurrentWindowIdentifier();
            driver.Close();
            string popupWindow = GetAllWindowIdentifier().Where(c => c != currentWindow).FirstOrDefault();
            SwitchToWindow(popupWindow);

            return this;
        }

        public CreateDiaryBookingPopup ValidateEmployeeContractInfoText(string ExpectedText)
        {
            WaitForElementVisible(employeeContractInfoText);
            var elementText = GetElementText(employeeContractInfoText);
            Assert.IsTrue(elementText.Contains(ExpectedText));

            return this;
        }

        public CreateDiaryBookingPopup WaitForWarningDialogueToLoad()
        {
            WaitForElementVisible(header_WarningDialogue);

            WaitForElementVisible(WarningBody);
            WaitForElementVisible(dismissButton_WarningDialogue);
            WaitForElementVisible(saveButton_WarningDialogue);

            return this;
        }

        public CreateDiaryBookingPopup ValidateWarningAlertMessage(string expectedText)
        {
            WaitForElementVisible(WarningBody);
            MoveToElementInPage(WarningBody);
            ValidateElementText(WarningBody, expectedText);

            return this;
        }

        public CreateDiaryBookingPopup ClickDismissButton_WarningDialogue()
        {
            WaitForElementToBeClickable(dismissButton_WarningDialogue);
            Click(dismissButton_WarningDialogue);

            return this;
        }

        public CreateDiaryBookingPopup ClickConfirmButton_WarningDialogue()
        {
            WaitForElementToBeClickable(saveButton_WarningDialogue);
            Click(saveButton_WarningDialogue);

            return this;
        }

    }
}
