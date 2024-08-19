using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class EmployeeSchedulePage : CommonMethods
    {
        public EmployeeSchedulePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id, 'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By AddBookingBtn = By.XPath("//*[@id='id--home--create-booking']/button");
        readonly By quickSearchArea = By.XPath("//div[@id='cd-select-provider']");
        readonly By quickSearchInput = By.XPath("//*[@id='cd-select-provider-cd-dropdown-search']");

        readonly By HomeTab = By.XPath("//li[@role='tab']/label/span[text()='Home']");
        readonly By FiltersTab = By.XPath("//li[@role='tab']/label/span[text()='Filters']");
        readonly By ViewTab = By.XPath("//li[@role='tab']/label/span[text()='View']");

        By ScheduleBooking(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']");
        By ScheduleBookingSliderTitle(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']//span");

        #region Tooltip text
        readonly By Staff_Label = By.XPath("//div[@id = 'id--slider-tooltip--staff']");
        readonly By Provider_Label = By.XPath("//div[@id = 'id--slider-tooltip--provider']");
        readonly By Address_Label = By.XPath("//div[@id = 'id--slider-tooltip--provider-address']");
        readonly By BookingType_Label = By.XPath("//div[@id = 'id--slider-tooltip--booking-type']");
        readonly By Occurs_Label = By.XPath("//div[@id = 'id--slider-tooltip--occurs']");
        readonly By Time_Label = By.XPath("//div[@id = 'id--slider-tooltip--day-time']");
        readonly By StartEndDay_Label = By.XPath("//div[@id = 'id--slider-tooltip--booking-type']");
        readonly By DueToTakePlace_Label = By.XPath("//div[@id = 'id--slider-tooltip--next-due']");

        By Tooltip_Label(string LabelName) => By.XPath("//div[@id = 'id--slider-tooltip--"+LabelName+"']");

        readonly By Booking_Status = By.XPath("//*[@class='mcc-alert mcc-alert--success cd-snackbar cd-snackbar-show']");

        By SliderLabel_BookingTypeName(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']//div[@class = 'cd-slider-content-labels']/span");
        By SliderLabel_NumberOfStaff(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']//div[@class = 'cd-slider-rhs']/span");

        By Slider_AllocatedBGColor(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']/div[@class = 'cd-slider-background'][contains(@style, 'background-color: rgba(69, 128, 69, 0.13);')]");
        By Slider_UnallocatedBGColor(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']/div[@class = 'cd-slider-background'][contains(@style, 'background-color: rgba(220, 53, 69, 0.13);')]");

        By Slider_Border_FrequencyGreaterThan1Week(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "'][@class = 'cd-slider cd-start-of-slot cd-end-of-slot cd-occurs-less-than-weekly']");
        By Slider_Border_FrequencyEquals1Week(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "'][@class = 'cd-slider cd-start-of-slot cd-end-of-slot']");

        #endregion

        #region Grid area

        readonly By GridColumn_Monday = By.XPath("//*[@class = 'cd-grid-column-sliders'][@data-id = '21dcf438-dc9c-4fca-8dc2-1339b5108357']");
        readonly By GridColumn_Tuesday = By.XPath("//*[@class = 'cd-grid-column-sliders'][@data-id = 'd35c6aa6-0d55-4147-812c-857b45284752']");
        readonly By GridColumn_Wednesday = By.XPath("//*[@class = 'cd-grid-column-sliders'][@data-id = 'c0fcaa7f-e7b6-4a34-a1d6-a97512f7a4b0']");
        readonly By GridColumn_Thursday = By.XPath("//*[@class = 'cd-grid-column-sliders'][@data-id = 'ddd4d8ed-5f76-40e0-be5f-15ef0c06e320']");
        readonly By GridColumn_Friday = By.XPath("//*[@class = 'cd-grid-column-sliders'][@data-id = 'b3080052-1774-4d34-8242-ee6bf82f848a']");
        readonly By GridColumn_Saturday = By.XPath("//*[@class = 'cd-grid-column-sliders'][@data-id = 'af4301ba-41ce-415a-acc4-94915b7789f7']");
        readonly By GridColumn_Sunday = By.XPath("//*[@class = 'cd-grid-column-sliders'][@data-id = 'f521f50b-13ca-4090-bb20-3688a64fb6fb']");

        readonly By RefreshPanel_Flex = By.XPath("//*[@id = 'CWRefreshPanel'][@style = 'display: flex;']");

        #endregion

        readonly By ShowAllBookingsCheckBox = By.XPath("//input[@id = 'id--home--show-all-bookings--checkbox']");
        readonly By EmploymentContractDropdown = By.XPath("//div[@id = 'id--home--employee-contract']//div[@class = 'cd-dropdown-display']");
        readonly By SelectedEmploymentContractText = By.XPath("//div[@id = 'id--home--employee-contract']//div[@class = 'cd-dropdown-display-text']");
        By SelectedEmploymentContractTextByTitle(string contractTitle) => By.XPath("//div[@id = 'id--home--employee-contract']//div[@class = 'cd-dropdown-display-text'][text() = '" + contractTitle + "']");
        By EmploymentContract(string EmploymentContractText) => By.XPath("//ul[@class = 'cd-dropdown-list']/li/button[@title = '" + EmploymentContractText + "']");
        By ClickToAddNewContractButton = By.XPath("//span[text() = 'User has no contracts available!']/a[@title = 'Click to add new contract.']");

        public EmployeeSchedulePage WaitForEmployeeSchedulePageToLoad(bool IsAddBookingButtonVisible = true)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            if (IsAddBookingButtonVisible)
                WaitForElement(AddBookingBtn);

            return this;
        }

        public EmployeeSchedulePage ClickScheduleBooking(string Schedulebookingid)
        {
            WaitForElementToBeClickable(ScheduleBooking(Schedulebookingid));
            Click(ScheduleBooking(Schedulebookingid));
            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public EmployeeSchedulePage ClickScheduleBooking(Guid Schedulebookingid)
        {
            return ClickScheduleBooking(Schedulebookingid.ToString());
        }

        public EmployeeSchedulePage ClickAddBooking()
        {
            WaitForElementToBeClickable(AddBookingBtn);
            Click(AddBookingBtn);

            return this;
        }

        public EmployeeSchedulePage MouseHoverScheduleBooking(String Schedulebookingid)
        {
            WaitForElementToBeClickable(ScheduleBooking(Schedulebookingid));
            MouseHover(ScheduleBooking(Schedulebookingid));

            return this;
        }

        public EmployeeSchedulePage ValidateTimeLabelText(string ExpectedText)
        {
            WaitForElement(Time_Label);
            ValidateElementTextContainsText(Time_Label, ExpectedText);

            return this;
        }

        public EmployeeSchedulePage ValidateStartEndDayLabelText(string ExpectedText)
        {
            WaitForElement(StartEndDay_Label);
            ValidateElementTextContainsText(StartEndDay_Label, ExpectedText);

            return this;
        }

        public EmployeeSchedulePage ValidateOccursLabelText(string ExpectedText)
        {
            WaitForElement(Occurs_Label);
            ValidateElementTextContainsText(Occurs_Label, ExpectedText);

            return this;
        }

        public EmployeeSchedulePage ValidateDueToTakePlaceLabelText(string ExpectedText)
        {
            WaitForElement(DueToTakePlace_Label);
            ValidateElementTextContainsText(DueToTakePlace_Label, ExpectedText);

            return this;
        }

        public EmployeeSchedulePage ValidateBookingTypeLabelText(string ExpectedText)
        {
            WaitForElement(BookingType_Label);
            ValidateElementTextContainsText(BookingType_Label, ExpectedText);

            return this;
        }

        public EmployeeSchedulePage ValidateAddressLabelText(string ExpectedText)
        {
            WaitForElement(Address_Label);
            ValidateElementTextContainsText(Address_Label, ExpectedText);

            return this;
        }

        public EmployeeSchedulePage ValidateProviderLabelText(string ExpectedText)
        {
            WaitForElement(Provider_Label);
            ValidateElementTextContainsText(Provider_Label, ExpectedText);

            return this;
        }


        public EmployeeSchedulePage ValidateStaffLabelText(string ExpectedText)
        {
            WaitForElement(Staff_Label);
            ValidateElementTextContainsText(Staff_Label, ExpectedText);

            return this;
        }

        public EmployeeSchedulePage ValidateSliderLabel_BookingTypeNameText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(SliderLabel_BookingTypeName(RecordId));
            ValidateElementText(SliderLabel_BookingTypeName(RecordId), ExpectedText);

            return this;
        }

        public EmployeeSchedulePage ValidateSliderLabel_NumberOfStaffText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(SliderLabel_NumberOfStaff(RecordId));
            ValidateElementText(SliderLabel_NumberOfStaff(RecordId), ExpectedText);

            return this;
        }

        public EmployeeSchedulePage ValidateSlider_BGColorAllocated(string RecordId)
        {
            WaitForElementVisible(Slider_AllocatedBGColor(RecordId));

            return this;
        }

        public EmployeeSchedulePage ValidateSlider_BGColorUnallocated(string RecordId)
        {
            WaitForElementVisible(Slider_UnallocatedBGColor(RecordId));

            return this;
        }

        public EmployeeSchedulePage ValidateScheduleBookingSliderTitle(string Schedulebookingid, string ExpectedText)
        {
            WaitForElementVisible(ScheduleBooking(Schedulebookingid));
            ScrollToElement(ScheduleBooking(Schedulebookingid));
            var Title = GetElementText(ScheduleBookingSliderTitle(Schedulebookingid));
            Assert.AreEqual(ExpectedText, Title);

            return this;
        }

        public EmployeeSchedulePage ValidateTooltipLabelText(string ToolTipLabelName, string ExpectedText)
        {
            ScrollToElement(Tooltip_Label(ToolTipLabelName));
            ValidateElementText(Tooltip_Label(ToolTipLabelName), ExpectedText);

            return this;
        }

        public EmployeeSchedulePage ValidateBookingStatus(string ExpectedText)
        {
            WaitForElementVisible(Booking_Status);
            ValidateElementText(Booking_Status, ExpectedText);

            return this;
        }

        public EmployeeSchedulePage ValidateScheduleBookingIsPresent(string Schedulebookingid, bool IsPresent)
        {
            if (IsPresent)
                WaitForElementVisible(ScheduleBooking(Schedulebookingid));
            else
                WaitForElementNotVisible(ScheduleBooking(Schedulebookingid), 3);

            return this;
        }

        //method to validate number of bookings in the grid
        public EmployeeSchedulePage ValidateNumberOfBookingsForStaff(string Schedulebookingid, int ExpectedNumber)
        {
            var NumberOfBookings = GetWebElements(ScheduleBooking(Schedulebookingid));
            Assert.AreEqual(ExpectedNumber, NumberOfBookings.Count);

            return this;
        }

        //Method to click grid column based on the day
        public EmployeeSchedulePage ClickGridColumn(string Day)
        {
            switch (Day)
            {
                case "Monday":
                    WaitForElementToBeClickable(GridColumn_Monday);
                    Click(GridColumn_Monday);
                    break;
                case "Tuesday":
                    WaitForElementToBeClickable(GridColumn_Tuesday);
                    Click(GridColumn_Tuesday);
                    break;
                case "Wednesday":
                    WaitForElementToBeClickable(GridColumn_Wednesday);
                    Click(GridColumn_Wednesday);
                    break;
                case "Thursday":
                    WaitForElementToBeClickable(GridColumn_Thursday);
                    Click(GridColumn_Thursday);
                    break;
                case "Friday":
                    WaitForElementToBeClickable(GridColumn_Friday);
                    Click(GridColumn_Friday);
                    break;
                case "Saturday":
                    WaitForElementToBeClickable(GridColumn_Saturday);
                    Click(GridColumn_Saturday);
                    break;
                case "Sunday":
                    WaitForElementToBeClickable(GridColumn_Sunday);
                    Click(GridColumn_Sunday);
                    break;
            }

            return this;
        }

        public EmployeeSchedulePage MoveToGridColumn(string Day)
        {
            switch (Day)
            {
                case "Monday":
                    WaitForElementToBeClickable(GridColumn_Monday);
                    ScrollToElement(GridColumn_Monday);
                    break;
                case "Tuesday":
                    WaitForElementToBeClickable(GridColumn_Tuesday);
                    ScrollToElement(GridColumn_Tuesday);
                    break;
                case "Wednesday":
                    WaitForElementToBeClickable(GridColumn_Wednesday);
                    ScrollToElement(GridColumn_Wednesday);
                    break;
                case "Thursday":
                    WaitForElementToBeClickable(GridColumn_Thursday);
                    ScrollToElement(GridColumn_Thursday);
                    break;
                case "Friday":
                    WaitForElementToBeClickable(GridColumn_Friday);
                    ScrollToElement(GridColumn_Friday);
                    break;
                case "Saturday":
                    WaitForElementToBeClickable(GridColumn_Saturday);
                    ScrollToElement(GridColumn_Saturday);
                    break;
                case "Sunday":
                    WaitForElementToBeClickable(GridColumn_Sunday);
                    ScrollToElement(GridColumn_Sunday);
                    break;
            }

            return this;
        }

        public EmployeeSchedulePage ValidateBorderForFrequencyGreaterThan1Week(string Schedulebookingid)
        {
            WaitForElementVisible(ScheduleBooking(Schedulebookingid));
            ScrollToElement(ScheduleBooking(Schedulebookingid));
            bool IsVisible = GetElementVisibility(Slider_Border_FrequencyGreaterThan1Week(Schedulebookingid));
            Assert.AreEqual(true, IsVisible);

            return this;
        }

        public EmployeeSchedulePage ValidateBorderForFrequencyEquals1Week(string Schedulebookingid)
        {
            WaitForElementVisible(ScheduleBooking(Schedulebookingid));
            ScrollToElement(ScheduleBooking(Schedulebookingid));
            bool IsVisible = GetElementVisibility(Slider_Border_FrequencyEquals1Week(Schedulebookingid));
            Assert.AreEqual(true, IsVisible);

            return this;
        }

        //method to validate showallbookings checkbox is visible
        public EmployeeSchedulePage ValidateShowAllBookingsCheckBoxIsVisible(bool isVisible = true)
        {
            var actualVisible = GetElementVisibility(ShowAllBookingsCheckBox);
            if (isVisible)
                Assert.AreEqual(true, actualVisible);
            else
                Assert.AreEqual(false, actualVisible);

            return this;
        }

        //method to click showallbookings checkbox
        public EmployeeSchedulePage ClickShowAllBookingsCheckBox()
        {
            WaitForElementToBeClickable(ShowAllBookingsCheckBox, true);
            Click(ShowAllBookingsCheckBox);

            return this;
        }

        //method to validate employment contract dropdown is visible
        public EmployeeSchedulePage ValidateEmploymentContractDropdownIsVisible(bool isVisible = true)
        {
            if (isVisible)
                WaitForElementVisible(EmploymentContractDropdown);
            else
                WaitForElementNotVisible(EmploymentContractDropdown, 3);

            return this;
        }

        //method to validate selected employment contract text
        public EmployeeSchedulePage ValidateSelectedEmploymentContractText(string ExpectedText)
        {
            WaitForElementVisible(SelectedEmploymentContractText);
            ValidateElementText(SelectedEmploymentContractText, ExpectedText);

            return this;
        }

        //method to click employment contract dropdown
        public EmployeeSchedulePage ClickEmploymentContractDropdown()
        {
            WaitForElementVisible(EmploymentContractDropdown);
            WaitForElementToBeClickable(EmploymentContractDropdown, true);
            Click(EmploymentContractDropdown);

            return this;
        }

        public EmployeeSchedulePage SelectEmploymentContract(String EmploymentContractText)
        {
            WaitForElementVisible(EmploymentContractDropdown);
            WaitForElementToBeClickable(EmploymentContractDropdown, true);
            Click(EmploymentContractDropdown);

            WaitForElementVisible(EmploymentContract(EmploymentContractText));
            ScrollToElement(EmploymentContract(EmploymentContractText));
            Click(EmploymentContract(EmploymentContractText));

            return this;
        }

        //method to validate employment contract text
        public EmployeeSchedulePage ValidateEmploymentContractTextStatus(string EmploymentContractText, bool IsPresent = true)
        {
            string actualContractTitle;
            if (IsPresent)
            {
                WaitForElementVisible(EmploymentContract(EmploymentContractText));
                actualContractTitle = GetElementByAttributeValue(EmploymentContract(EmploymentContractText), "title");
                actualContractTitle.Contains(EmploymentContractText);
            }
            else
            {
                WaitForElementNotVisible(EmploymentContract(EmploymentContractText), 3);
            }

            return this;
        }

        public EmployeeSchedulePage ClickAddNewContractButton()
        {
            WaitForElementToBeClickable(ClickToAddNewContractButton, true);
            ScrollToElement(ClickToAddNewContractButton);
            Click(ClickToAddNewContractButton);

            return this;
        }

        //Add new contract button is visible
        public EmployeeSchedulePage ValidateClickToAddNewContractButtonIsVisible(bool isVisible = true)
        {
            WaitForElementVisible(ClickToAddNewContractButton);
            var actualVisible = GetElementVisibility(ClickToAddNewContractButton);
            if (isVisible)
                Assert.AreEqual(true, actualVisible);
            else
                Assert.AreEqual(false, actualVisible);

            return this;
        }

    }
}
