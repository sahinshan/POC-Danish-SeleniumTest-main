using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PeopleSchedulePage : CommonMethods
    {
        public PeopleSchedulePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By AddBookingBtn = By.XPath("//*[@id='id--home--create-booking']/button");
        readonly By quickSearchArea = By.XPath("//div[@id='cd-select-provider']");
        readonly By quickSearchInput = By.XPath("//*[@id='cd-select-provider-cd-dropdown-search']");

        readonly By HomeTab = By.XPath("//li[@role='tab']/label/span[text()='Home']");

        By picklistProviderRecord(string ProviderName) => By.XPath("//*[@class='cd-dropdown-option cd-selected cd-dropdown-with-icon cd-dropdown-option-favourite'][@title='" + ProviderName + "']");

        By picklistProviderRecord1(string ProviderName) => By.XPath("//span[text()='" + ProviderName + "']");

        By DiaryBooking(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']");

        readonly By ProviderPickList = By.XPath("//*[@id='id--home--provider']//div[@class='cd-dropdown-display']");

        By Provider_ProviderDiaryText(int Position, string Provider) => By.XPath("//div[@id='cd-select-provider']//div/div/div/div[" + Position + "]/span[contains(text(),'" + Provider + "')]");

        By Provider_Position(int Position) => By.XPath("//div[@id='cd-select-provider']//div/div/div/div[" + Position + "]/span");


        readonly By ProviderInput = By.XPath("//*[@id='id--home--provider--dropdown-filter']");
        By picklistProviderRecordHighlighted(string ProviderName) => By.XPath("//*[@id='id--home--provider']//b[text()='" + ProviderName + "']");


        By Provider_ProviderDiaryText(string ProviderText) => By.XPath("//*[@id='id--home--provider']//div/span[text()='" + ProviderText + "']");
        By Provider_ProviderDiaryText(string ProviderName, Guid ProviderId) => By.XPath("//*[@id='id--home--provider']//li[@data-id = '" + ProviderId + "']//button[text()='" + ProviderName + "']");

        By ScheduleBooking(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']");
        By ScheduleBookingSliderTitle(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']//span");

        #region Tooltip text
        readonly By Staff_Label = By.XPath("//span[@class='cd-slider-tooltip-text']/div/span[text()='Staff:']/following-sibling::span");
        readonly By Provider_Label = By.XPath("//span[@class='cd-slider-tooltip-text']/div/span[text()='Provider:']/following-sibling::span");
        readonly By Address_Label = By.XPath("//span[@class='cd-slider-tooltip-text']/div/span[text()='Address:']/following-sibling::span");
        readonly By BookingType_Label = By.XPath("//span[@class='cd-slider-tooltip-text']/div/span[text()='Booking Type:']/following-sibling::span");
        readonly By Occurs_Label = By.XPath("//span[@class='cd-slider-tooltip-text']/div/span[text()='Occurs:']/following-sibling::span");
        readonly By Time_Label = By.XPath("//span[@class='cd-slider-tooltip-text']/div/span[text()='Time:']/following-sibling::span");
        readonly By StartEndDay_Label = By.XPath("//span[@class='cd-slider-tooltip-text']/div/span[text()='Start - End Day:']/following-sibling::span");
        readonly By DueToTakePlace_Label = By.XPath("//span[@class='cd-slider-tooltip-text']/div/span[text()='Due To Take Place:']/following-sibling::span");

        By Tooltip_Label(string LabelName) => By.XPath("//span[@class='cd-slider-tooltip-text']/div/span[text()='" + LabelName + ":']/following-sibling::span");

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

        By ScheduleBooking_Friday(string Recordid) => By.XPath("//*[@data-id = 'b3080052-1774-4d34-8242-ee6bf82f848a']//div[@data-id='" + Recordid + "']");
        By ScheduleBooking_Saturday(string Recordid) => By.XPath("//*[@data-id = 'af4301ba-41ce-415a-acc4-94915b7789f7']//div[@data-id='" + Recordid + "']");
        By ScheduleBooking_Sunday(string Recordid) => By.XPath("//*[@data-id = 'f521f50b-13ca-4090-bb20-3688a64fb6fb']//div[@data-id='" + Recordid + "']");

        #endregion

        public PeopleSchedulePage WaitForPeopleSchedulePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(AddBookingBtn);

            return this;
        }

        public PeopleSchedulePage WaitForRefreshPanelToDisappear(int RefreshDuration = 30)
        {

            SwitchToDefaultFrame();

            System.Threading.Thread.Sleep(RefreshDuration * 1000);
            System.Threading.Thread.Sleep(1000);

            var isVisible = GetElementVisibility(RefreshPanel_Flex);
            Assert.IsTrue(isVisible);

            return this;

        }

        public PeopleSchedulePage SearchProviderRecord(string provider)
        {
            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementToBeClickable(quickSearchArea);
            Click(quickSearchArea);

            WaitForElementToBeClickable(quickSearchInput);

            WaitForElementToBeClickable(picklistProviderRecord1(provider));
            Click(picklistProviderRecord1(provider));

            System.Threading.Thread.Sleep(500);


            return this;
        }

        public PeopleSchedulePage ClickDiaryBooking(string Diarybookingid)
        {
            WaitForElementToBeClickable(DiaryBooking(Diarybookingid));
            Click(DiaryBooking(Diarybookingid));
            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PeopleSchedulePage ClickDiaryBooking(Guid Diarybookingid)
        {
            return ClickDiaryBooking(Diarybookingid.ToString());
        }


        public PeopleSchedulePage selectProvider(String ProviderName, Guid ProviderId)
        {
            WaitForElementVisible(ProviderPickList);
            WaitForElementToBeClickable(ProviderPickList, true);
            Click(ProviderPickList);

            bool elementVisible = GetElementVisibility(ProviderInput);
            if (elementVisible)
            {
                SendKeys(ProviderInput, ProviderName);

                WaitForElement(picklistProviderRecordHighlighted(ProviderName));
                Click(picklistProviderRecordHighlighted(ProviderName));
            }
            else
            {
                WaitForElement(Provider_ProviderDiaryText(ProviderName, ProviderId));
                Click(Provider_ProviderDiaryText(ProviderName, ProviderId));
            }            

            return this;
        }

        public PeopleSchedulePage clickAddBooking()
        {
            WaitForElementToBeClickable(HomeTab);
            Click(HomeTab);

            WaitForElementToBeClickable(AddBookingBtn);
            ScrollToElement(AddBookingBtn);
            Click(AddBookingBtn);


            return this;
        }

        public PeopleSchedulePage MouseHoverDiaryBooking(String Schedulebookingid)
        {
            WaitForElementToBeClickable(ScheduleBooking(Schedulebookingid));
            MouseHover(ScheduleBooking(Schedulebookingid));



            return this;
        }

        public PeopleSchedulePage ValidateTimeLabelText(string ExpectedText)
        {
            WaitForElement(Time_Label);
            ValidateElementText(Time_Label, ExpectedText);

            return this;
        }

        public PeopleSchedulePage ValidateStartEndDayLabelText(string ExpectedText)
        {
            ValidateElementText(StartEndDay_Label, ExpectedText);

            return this;
        }

        public PeopleSchedulePage ValidateOccursLabelText(string ExpectedText)
        {
            ValidateElementText(Occurs_Label, ExpectedText);

            return this;
        }

        public PeopleSchedulePage ValidateDueToTakePlaceLabelText(string ExpectedText)
        {
            ValidateElementText(DueToTakePlace_Label, ExpectedText);

            return this;
        }

        public PeopleSchedulePage ValidateBookingTypeLabelText(string ExpectedText)
        {
            ValidateElementText(BookingType_Label, ExpectedText);

            return this;
        }

        public PeopleSchedulePage ValidateAddressLabelText(string ExpectedText)
        {
            ValidateElementText(Address_Label, ExpectedText);

            return this;
        }

        public PeopleSchedulePage ValidateProviderLabelText(string ExpectedText)
        {
            ValidateElementText(Provider_Label, ExpectedText);

            return this;
        }


        public PeopleSchedulePage ValidateStaffLabelText(string ExpectedText)
        {
            ValidateElementText(Staff_Label, ExpectedText);

            return this;
        }

        public PeopleSchedulePage ValidateSliderLabel_BookingTypeNameText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(SliderLabel_BookingTypeName(RecordId));
            ValidateElementText(SliderLabel_BookingTypeName(RecordId), ExpectedText);

            return this;
        }

        public PeopleSchedulePage ValidateSliderLabel_NumberOfStaffText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(SliderLabel_NumberOfStaff(RecordId));
            ValidateElementText(SliderLabel_NumberOfStaff(RecordId), ExpectedText);

            return this;
        }

        public PeopleSchedulePage ValidateSlider_BGColorAllocated(string RecordId)
        {
            WaitForElementVisible(Slider_AllocatedBGColor(RecordId));

            return this;
        }

        public PeopleSchedulePage ValidateSlider_BGColorUnallocated(string RecordId)
        {
            WaitForElementVisible(Slider_UnallocatedBGColor(RecordId));

            return this;
        }

        public PeopleSchedulePage ClickProviderDropdownList()
        {
            WaitForElementToBeClickable(ProviderPickList);
            Click(ProviderPickList);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PeopleSchedulePage ValidateSortingListOfProvider(int Position, string ProviderName)
        {
            WaitForElementVisible(Provider_Position(Position));
            var name = GetElementText(Provider_Position(Position));
            Assert.AreEqual(true, name.Contains(ProviderName));
            WaitForElementVisible(Provider_ProviderDiaryText(Position, ProviderName));

            return this;
        }

        public PeopleSchedulePage ValidateProviderIsPresent(string ProviderName, bool IsPresent)
        {
            if (IsPresent)
                WaitForElementVisible(Provider_ProviderDiaryText(ProviderName));
            else
                WaitForElementNotVisible(Provider_ProviderDiaryText(ProviderName), 3);

            return this;
        }

        public PeopleSchedulePage ClickScheduleBooking(String Schedulebookingid)
        {
            WaitForElementToBeClickable(ScheduleBooking(Schedulebookingid));
            ScrollToElement(ScheduleBooking(Schedulebookingid));
            Click(ScheduleBooking(Schedulebookingid));

            return this;
        }

        public PeopleSchedulePage ValidateScheduleBookingSliderTitle(string Schedulebookingid, string ExpectedText)
        {
            WaitForElementVisible(ScheduleBooking(Schedulebookingid));
            ScrollToElement(ScheduleBooking(Schedulebookingid));
            var Title = GetElementText(ScheduleBookingSliderTitle(Schedulebookingid));
            Assert.AreEqual(ExpectedText, Title);

            return this;
        }

        public PeopleSchedulePage ValidateTooltipLabelText(string ToolTipLabelName, string ExpectedText)
        {
            ScrollToElement(Tooltip_Label(ToolTipLabelName));
            ValidateElementText(Tooltip_Label(ToolTipLabelName), ExpectedText);

            return this;
        }

        public PeopleSchedulePage ValidateBookingStatus(string ExpectedText)
        {
            WaitForElementVisible(Booking_Status);
            ValidateElementText(Booking_Status, ExpectedText);

            return this;
        }

        public PeopleSchedulePage ValidateScheduleBookingIsPresent(string Schedulebookingid, bool IsPresent)
        {
            if (IsPresent)
                WaitForElementVisible(ScheduleBooking(Schedulebookingid));
            else
                WaitForElementNotVisible(ScheduleBooking(Schedulebookingid), 3);

            return this;
        }

        public PeopleSchedulePage ValidateScheduleBookingForFridayIsPresent(string Schedulebookingid, bool IsPresent)
        {
            if (IsPresent)
            {
                ScrollToElement(ScheduleBooking_Friday(Schedulebookingid));
                WaitForElementVisible(ScheduleBooking_Friday(Schedulebookingid));
            }
            else
                WaitForElementNotVisible(ScheduleBooking_Friday(Schedulebookingid), 3);

            return this;
        }

        public PeopleSchedulePage ValidateScheduleBookingForSaturdayIsPresent(string Schedulebookingid, bool IsPresent)
        {
            if (IsPresent)
            {
                ScrollToElement(ScheduleBooking_Saturday(Schedulebookingid));
                WaitForElementVisible(ScheduleBooking_Saturday(Schedulebookingid));
            }
            else
                WaitForElementNotVisible(ScheduleBooking_Saturday(Schedulebookingid), 3);

            return this;
        }

        public PeopleSchedulePage ValidateScheduleBookingForSundayIsPresent(string Schedulebookingid, bool IsPresent)
        {
            if (IsPresent)
            {
                ScrollToElement(ScheduleBooking_Sunday(Schedulebookingid));
                WaitForElementVisible(ScheduleBooking_Sunday(Schedulebookingid));
            }
            else
                WaitForElementNotVisible(ScheduleBooking_Sunday(Schedulebookingid), 3);

            return this;
        }

        //method to validate number of bookings in the grid
        public PeopleSchedulePage ValidateNumberOfBookingsForStaff(string Schedulebookingid, int ExpectedNumber)
        {
            var NumberOfBookings = GetWebElements(ScheduleBooking(Schedulebookingid));
            Assert.AreEqual(ExpectedNumber, NumberOfBookings.Count);

            return this;
        }

        //Method to click grid column based on the day
        public PeopleSchedulePage ClickGridColumn(string Day)
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

        public PeopleSchedulePage MoveToGridColumn(string Day)
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

        public PeopleSchedulePage ValidateBorderForFrequencyGreaterThan1Week(string Schedulebookingid)
        {
            WaitForElementVisible(ScheduleBooking(Schedulebookingid));
            ScrollToElement(ScheduleBooking(Schedulebookingid));
            bool IsVisible = GetElementVisibility(Slider_Border_FrequencyGreaterThan1Week(Schedulebookingid));
            Assert.AreEqual(true, IsVisible);

            return this;
        }

        public PeopleSchedulePage ValidateBorderForFrequencyEquals1Week(string Schedulebookingid)
        {
            WaitForElementVisible(ScheduleBooking(Schedulebookingid));
            ScrollToElement(ScheduleBooking(Schedulebookingid));
            bool IsVisible = GetElementVisibility(Slider_Border_FrequencyEquals1Week(Schedulebookingid));
            Assert.AreEqual(true, IsVisible);

            return this;
        }

    }
}
