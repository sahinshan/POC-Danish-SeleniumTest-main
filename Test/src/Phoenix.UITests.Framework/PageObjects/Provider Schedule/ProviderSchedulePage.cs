using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ProviderSchedulePage : CommonMethods
    {
        public ProviderSchedulePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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
        readonly By FiltersTab = By.XPath("//li[@role='tab']/label/span[text()='Filters']");
        readonly By ViewTab = By.XPath("//li[@role='tab']/label/span[text()='View']");

        By picklistProviderRecord(string ProviderName) => By.XPath("//*[@class='cd-dropdown-option cd-selected cd-dropdown-with-icon cd-dropdown-option-favourite'][@title='" + ProviderName + "']");

        By picklistProviderRecord1(string ProviderName) => By.XPath("//span[text()='" + ProviderName + "']");

        By DiaryBooking(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']");

        readonly By ProviderPickList = By.XPath("//*[@id='id--home--provider']//div[@class='cd-dropdown-display']");

        readonly By ProviderSearchInput = By.XPath("//*[@id='id--home--provider--dropdown-filter']");

        By Provider_ProviderDiaryText(int Position, string Provider) => By.XPath("//*[@id='id--home--provider']//ul/li[" + Position + "]/button[contains(@title,'" + Provider + "')]");

        By Provider_Position(int Position) => By.XPath("//*[@id='id--home--provider']//ul/li[" + Position + "]/button");


        By ProviderText(string ProviderText) => By.XPath("//*[@id='id--home--provider']//li//*[text()='" + ProviderText + "']");
        
        By ProviderButton(string ProviderId) => By.XPath("//*[@id='id--home--provider']//li//*[@data-id='" + ProviderId + "']");

        By ScheduleBooking(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']");

        By ScheduleBookingSliderTitle(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']//span");

        #region Tooltip text
        readonly By Staff_Label = By.XPath("//div[@id = 'id--slider-tooltip--staff']");
        readonly By Provider_Label = By.XPath("//div[@id = 'id--slider-tooltip--provider']");
        readonly By Address_Label = By.XPath("//div[@id = 'id--slider-tooltip--provider-address']");
        readonly By BookingType_Label = By.XPath("//div[@id = 'id--slider-tooltip--booking-type']");
        readonly By Occurs_Label = By.XPath("//div[@id = 'id--slider-tooltip--occurs']");
        readonly By Time_Label = By.XPath("//div[@id = 'id--slider-tooltip--day-time']");
        readonly By DueToTakePlace_Label = By.XPath("//div[@id = 'id--slider-tooltip--next-due']");

        By Tooltip_Label(string LabelName) => By.XPath("//span[@class='cd-slider-tooltip-text']//div[@id = 'id--slider-tooltip--" + LabelName + "']");
        readonly By Booking_Status = By.XPath("//*[@class='mcc-alert mcc-alert--success cd-snackbar cd-snackbar-show']");

        By SliderLabel_BookingTypeName(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']//div[@class = 'cd-slider-content-labels']/span");
        By SliderLabel_NumberOfStaff(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']//div[@class = 'cd-slider-rhs']/span");

        By Slider_AllocatedBGColor(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']/div[@class = 'cd-slider-background'][contains(@style, 'background-color: rgba(69, 128, 69, 0.13);')]");
        By Slider_UnallocatedBGColor(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']/div[@class = 'cd-slider-background'][contains(@style, 'background-color: rgba(220, 53, 69, 0.13);')]");

        By Slider_Border_FrequencyGreaterThan1Week(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "'][@class = 'cd-slider cd-start-of-slot cd-end-of-slot cd-occurs-less-than-weekly']");
        By Slider_Border_FrequencyEquals1Week(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "'][@class = 'cd-slider cd-start-of-slot cd-end-of-slot']");

        #endregion

        #region Grid area

        readonly By GridColumn_Monday = By.XPath("//*[@class='cd-grid-column-sliders'][@data-id ='21dcf438-dc9c-4fca-8dc2-1339b5108357']");
        readonly By GridColumn_Tuesday = By.XPath("//*[@class='cd-grid-column-sliders'][@data-id ='d35c6aa6-0d55-4147-812c-857b45284752']");
        readonly By GridColumn_Wednesday = By.XPath("//*[@class='cd-grid-column-sliders'][@data-id ='c0fcaa7f-e7b6-4a34-a1d6-a97512f7a4b0']");
        readonly By GridColumn_Thursday = By.XPath("//*[@class='cd-grid-column-sliders'][@data-id ='ddd4d8ed-5f76-40e0-be5f-15ef0c06e320']");
        readonly By GridColumn_Friday = By.XPath("//*[@class='cd-grid-column-sliders'][@data-id ='b3080052-1774-4d34-8242-ee6bf82f848a']");
        readonly By GridColumn_Saturday = By.XPath("//*[@class='cd-grid-column-sliders'][@data-id ='af4301ba-41ce-415a-acc4-94915b7789f7']");
        readonly By GridColumn_Sunday = By.XPath("//*[@class='cd-grid-column-sliders'][@data-id ='f521f50b-13ca-4090-bb20-3688a64fb6fb']");

        readonly By RefreshPanel_Flex = By.XPath("//*[@id = 'CWRefreshPanel'][@style = 'display: flex;']");

        By ScheduleBooking_Friday(string Recordid) => By.XPath("//*[@data-id='b3080052-1774-4d34-8242-ee6bf82f848a']//div[@data-id='" + Recordid + "']");
        By ScheduleBooking_Saturday(string Recordid) => By.XPath("//*[@data-id='af4301ba-41ce-415a-acc4-94915b7789f7']//div[@data-id='" + Recordid + "']");
        By ScheduleBooking_Sunday(string Recordid) => By.XPath("//*[@data-id='f521f50b-13ca-4090-bb20-3688a64fb6fb']//div[@data-id='" + Recordid + "']");

        #endregion

        #region Context Menu

        readonly By contextMenu_OuterDiv = By.XPath("//div[@class='cd-context-menu']");

        readonly By contextMenu_UpdateOption = By.XPath("//*[@id='cd-context-menu-5']");
        readonly By contextMenu_SpecifyCareWorkerButton = By.XPath("//*[@id='cd-context-menu-5-1']");
        readonly By contextMenu_ChangeBookingTypeButton = By.XPath("//*[@id='cd-context-menu-5-2']");
        readonly By contextMenu_DeallocateButton = By.XPath("//*[@id='cd-context-menu-5-3']");
        readonly By contextMenu_ConvertToCyclicalBookingButton = By.XPath("//*[@id='cd-context-menu-5-6']");

        readonly By contextMenu_CopyOption = By.XPath("//*[@id='cd-context-menu-copy-copy-to']");
        readonly By contextMenu_CopyButton = By.XPath("//*[@id='cd-context-menu-copy-copy-to-copy']");
        readonly By contextMenu_CopyToButton = By.XPath("//*[@id='cd-context-menu-copy-copy-to-copy-to']");

        readonly By contextMenu_PastButton = By.XPath("//*[@id='cd-context-menu-paste']");

        readonly By contextMenu_DeleteButton = By.XPath("//*[@id='cd-context-menu-7']");

        #endregion

        public ProviderSchedulePage WaitForProviderSchedulePageToLoad()
        {
            System.Threading.Thread.Sleep(1000);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(AddBookingBtn);

            return this;
        }

        public ProviderSchedulePage WaitForRefreshPanelToDisappear()
        {
            SwitchToDefaultFrame();

            System.Threading.Thread.Sleep(1000);

            var isVisible = GetElementVisibility(RefreshPanel_Flex);
            var count = 0;

            while (count < 700 && !isVisible)
            {
                count++;
                System.Threading.Thread.Sleep(50);
                isVisible = GetElementVisibility(RefreshPanel_Flex);
            }

            Assert.IsTrue(isVisible);

            return this;
        }

        public ProviderSchedulePage SearchProviderRecord(string provider)
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

        public ProviderSchedulePage RightClickDiaryBooking(string Diarybookingid)
        {
            WaitForElementToBeClickable(DiaryBooking(Diarybookingid));
            RightClick(DiaryBooking(Diarybookingid));

            return this;
        }

        public ProviderSchedulePage RightClickDiaryBooking(Guid Diarybookingid)
        {
            return RightClickDiaryBooking(Diarybookingid.ToString());
        }


        public ProviderSchedulePage DragBookingToDayOfWeekArea(string Diarybookingid, DayOfWeek DayOfWeekToDragElementInto)
        {
            switch (DayOfWeekToDragElementInto)
            {
                case DayOfWeek.Sunday:
                    DragAndDropToTargetElement(DiaryBooking(Diarybookingid), GridColumn_Sunday);
                    break;
                case DayOfWeek.Monday:
                    DragAndDropToTargetElement(DiaryBooking(Diarybookingid), GridColumn_Monday);
                    break;
                case DayOfWeek.Tuesday:
                    DragAndDropToTargetElement(DiaryBooking(Diarybookingid), GridColumn_Tuesday);
                    break;
                case DayOfWeek.Wednesday:
                    DragAndDropToTargetElement(DiaryBooking(Diarybookingid), GridColumn_Wednesday);
                    break;
                case DayOfWeek.Thursday:
                    DragAndDropToTargetElement(DiaryBooking(Diarybookingid), GridColumn_Thursday);
                    break;
                case DayOfWeek.Friday:
                    DragAndDropToTargetElement(DiaryBooking(Diarybookingid), GridColumn_Friday);
                    break;
                case DayOfWeek.Saturday:
                    DragAndDropToTargetElement(DiaryBooking(Diarybookingid), GridColumn_Saturday);
                    break;
                default:
                    break;
            }

            return this;
        }

        public ProviderSchedulePage DragBookingToDayOfWeekArea(Guid Diarybookingid, DayOfWeek DayOfWeekToDragElementInto)
        {
            return DragBookingToDayOfWeekArea(Diarybookingid.ToString(), DayOfWeekToDragElementInto);
        }

        public ProviderSchedulePage ClickDiaryBooking(string Diarybookingid)
        {
            WaitForElementToBeClickable(DiaryBooking(Diarybookingid));
            Click(DiaryBooking(Diarybookingid));
            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public ProviderSchedulePage ClickDiaryBooking(Guid Diarybookingid)
        {
            return ClickDiaryBooking(Diarybookingid.ToString());
        }

        public ProviderSchedulePage ClickOnProviderWithoutSearching(Guid ProviderId)
        {
            WaitForElement(ProviderButton(ProviderId.ToString()));
            ScrollToElement(ProviderButton(ProviderId.ToString()));
            Click(ProviderButton(ProviderId.ToString()));

            return this;
        }

        public ProviderSchedulePage selectProvider(String ProviderDiaryText)
        {
            WaitForElementVisible(ProviderPickList);
            WaitForElementToBeClickable(ProviderPickList, true);
            Click(ProviderPickList);

            var elementVisible = WaitForElementVisibleWithoutException(ProviderSearchInput, 3);
            if (elementVisible)
            {
                SendKeys(ProviderSearchInput, ProviderDiaryText + Keys.Tab);
            }

            WaitForElementVisible(ProviderText(ProviderDiaryText));
            ScrollToElement(ProviderText(ProviderDiaryText));
            Click(ProviderText(ProviderDiaryText));

            return this;
        }

        public ProviderSchedulePage clickAddBooking()
        {
            WaitForElementToBeClickable(HomeTab);
            Click(HomeTab);

            WaitForElementToBeClickable(AddBookingBtn);
            ScrollToElement(AddBookingBtn);
            Click(AddBookingBtn);


            return this;
        }

        public ProviderSchedulePage MouseHoverDiaryBooking(String Schedulebookingid)
        {
            WaitForElementToBeClickable(ScheduleBooking(Schedulebookingid));
            MouseHover(ScheduleBooking(Schedulebookingid));



            return this;
        }

        public ProviderSchedulePage ValidateTimeLabelText(string ExpectedText)
        {
            WaitForElement(Time_Label);
            ValidateElementTextContainsText(Time_Label, ExpectedText);

            return this;
        }

        public ProviderSchedulePage ValidateOccursLabelText(string ExpectedText)
        {
            WaitForElement(Occurs_Label);
            ValidateElementTextContainsText(Occurs_Label, ExpectedText);

            return this;
        }

        public ProviderSchedulePage ValidateDueToTakePlaceLabelText(string ExpectedText)
        {
            ValidateElementTextContainsText(DueToTakePlace_Label, ExpectedText);

            return this;
        }

        public ProviderSchedulePage ValidateBookingTypeLabelText(string ExpectedText)
        {
            WaitForElement(BookingType_Label);
            ValidateElementTextContainsText(BookingType_Label, ExpectedText);

            return this;
        }

        public ProviderSchedulePage ValidateAddressLabelText(string ExpectedText)
        {
            WaitForElement(Address_Label);
            ValidateElementTextContainsText(Address_Label, ExpectedText);

            return this;
        }

        public ProviderSchedulePage ValidateProviderLabelText(string ExpectedText)
        {
            WaitForElement(Provider_Label);
            ValidateElementTextContainsText(Provider_Label, ExpectedText);

            return this;
        }


        public ProviderSchedulePage ValidateStaffLabelText(string ExpectedText)
        {
            WaitForElement(Staff_Label);
            ValidateElementTextContainsText(Staff_Label, ExpectedText);

            return this;
        }

        public ProviderSchedulePage ValidateSliderLabel_BookingTypeNameText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(SliderLabel_BookingTypeName(RecordId));
            ValidateElementText(SliderLabel_BookingTypeName(RecordId), ExpectedText);

            return this;
        }

        public ProviderSchedulePage ValidateSliderLabel_NumberOfStaffText(string RecordId, string ExpectedText)
        {
            WaitForElementVisible(SliderLabel_NumberOfStaff(RecordId));
            ValidateElementText(SliderLabel_NumberOfStaff(RecordId), ExpectedText);

            return this;
        }

        public ProviderSchedulePage ValidateSlider_BGColorAllocated(string RecordId)
        {
            WaitForElementVisible(Slider_AllocatedBGColor(RecordId));

            return this;
        }

        public ProviderSchedulePage ValidateSlider_BGColorUnallocated(string RecordId)
        {
            WaitForElementVisible(Slider_UnallocatedBGColor(RecordId));

            return this;
        }

        public ProviderSchedulePage ClickProviderDropdownList()
        {
            WaitForElementToBeClickable(ProviderPickList);
            Click(ProviderPickList);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public ProviderSchedulePage ValidateSortingListOfProvider(int Position, string ProviderName)
        {
            WaitForElement(Provider_Position(Position));
            var elementText = GetElementText(Provider_Position(Position));
            Assert.AreEqual(true, elementText.Contains(ProviderName));
            WaitForElement(Provider_ProviderDiaryText(Position, ProviderName));

            return this;
        }

        public ProviderSchedulePage ValidateProviderIsPresent(string ProviderName, bool IsPresent)
        {
            if (IsPresent)
                WaitForElementVisible(ProviderText(ProviderName));
            else
                WaitForElementNotVisible(ProviderText(ProviderName), 3);

            return this;
        }

        public ProviderSchedulePage ClickScheduleBooking(String Schedulebookingid)
        {
            WaitForElementToBeClickable(ScheduleBooking(Schedulebookingid));
            ScrollToElement(ScheduleBooking(Schedulebookingid));
            Click(ScheduleBooking(Schedulebookingid));

            return this;
        }

        public ProviderSchedulePage ValidateScheduleBookingSliderTitle(string Schedulebookingid, string ExpectedText)
        {
            WaitForElementVisible(ScheduleBooking(Schedulebookingid));
            ScrollToElement(ScheduleBooking(Schedulebookingid));
            var Title = GetElementText(ScheduleBookingSliderTitle(Schedulebookingid));
            Assert.AreEqual(ExpectedText, Title);

            return this;
        }

        public ProviderSchedulePage ValidateTooltipLabelText(string ToolTipLabelName, string ExpectedText)
        {
            ScrollToElement(Tooltip_Label(ToolTipLabelName));
            ValidateElementText(Tooltip_Label(ToolTipLabelName), ExpectedText);

            return this;
        }

        public ProviderSchedulePage ValidateBookingStatus(string ExpectedText)
        {
            WaitForElementVisible(Booking_Status);
            ValidateElementText(Booking_Status, ExpectedText);

            return this;
        }

        public ProviderSchedulePage ValidateScheduleBookingIsPresent(string Schedulebookingid, bool IsPresent)
        {
            if (IsPresent)
                WaitForElementVisible(ScheduleBooking(Schedulebookingid));
            else
                WaitForElementNotVisible(ScheduleBooking(Schedulebookingid), 3);

            return this;
        }

        public ProviderSchedulePage ValidateScheduleBookingForFridayIsPresent(string Schedulebookingid, bool IsPresent)
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

        public ProviderSchedulePage ValidateScheduleBookingForSaturdayIsPresent(string Schedulebookingid, bool IsPresent)
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

        public ProviderSchedulePage ValidateScheduleBookingForSundayIsPresent(string Schedulebookingid, bool IsPresent)
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
        public ProviderSchedulePage ValidateNumberOfBookingsForStaff(string Schedulebookingid, int ExpectedNumber)
        {
            var NumberOfBookings = GetWebElements(ScheduleBooking(Schedulebookingid));
            Assert.AreEqual(ExpectedNumber, NumberOfBookings.Count);

            return this;
        }

        //Method to click grid column based on the day
        public ProviderSchedulePage ClickGridColumn(string Day)
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

        public ProviderSchedulePage MoveToGridColumn(string Day)
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

        public ProviderSchedulePage ValidateBorderForFrequencyGreaterThan1Week(string Schedulebookingid)
        {
            WaitForElementVisible(ScheduleBooking(Schedulebookingid));
            ScrollToElement(ScheduleBooking(Schedulebookingid));
            bool IsVisible = GetElementVisibility(Slider_Border_FrequencyGreaterThan1Week(Schedulebookingid));
            Assert.AreEqual(true, IsVisible);

            return this;
        }

        public ProviderSchedulePage ValidateBorderForFrequencyEquals1Week(string Schedulebookingid)
        {
            WaitForElementVisible(ScheduleBooking(Schedulebookingid));
            ScrollToElement(ScheduleBooking(Schedulebookingid));
            bool IsVisible = GetElementVisibility(Slider_Border_FrequencyEquals1Week(Schedulebookingid));
            Assert.AreEqual(true, IsVisible);

            return this;
        }

        public ProviderSchedulePage RightClickGridArea(DayOfWeek DayOfWeek)
        {
            switch (DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    WaitForElementToBeClickable(GridColumn_Sunday);
                    RightClick(GridColumn_Sunday);
                    break;
                case DayOfWeek.Monday:
                    WaitForElementToBeClickable(GridColumn_Monday);
                    RightClick(GridColumn_Monday);
                    break;
                case DayOfWeek.Tuesday:
                    WaitForElementToBeClickable(GridColumn_Tuesday);
                    RightClick(GridColumn_Tuesday);
                    break;
                case DayOfWeek.Wednesday:
                    WaitForElementToBeClickable(GridColumn_Wednesday);
                    RightClick(GridColumn_Wednesday);
                    break;
                case DayOfWeek.Thursday:
                    WaitForElementToBeClickable(GridColumn_Thursday);
                    RightClick(GridColumn_Thursday);
                    break;
                case DayOfWeek.Friday:
                    WaitForElementToBeClickable(GridColumn_Friday);
                    RightClick(GridColumn_Friday);
                    break;
                case DayOfWeek.Saturday:
                    WaitForElementToBeClickable(GridColumn_Saturday);
                    RightClick(GridColumn_Saturday);
                    break;
                default:
                    break;
            }

            return this;
        }

        #region Context Menu

        public ProviderSchedulePage WaitForContextMenuToLoad()
        {
            WaitForElementVisible(contextMenu_OuterDiv);
            WaitForElementVisible(contextMenu_UpdateOption);
            WaitForElementVisible(contextMenu_CopyOption);
            WaitForElementVisible(contextMenu_PastButton);
            WaitForElementVisible(contextMenu_DeleteButton);

            return this;
        }

        public ProviderSchedulePage ClickCopyButton_ContextMenu()
        {
            WaitForElementToBeClickable(contextMenu_CopyOption);
            Click(contextMenu_CopyOption);

            WaitForElementToBeClickable(contextMenu_CopyButton);
            Click(contextMenu_CopyButton);

            return this;
        }

        public ProviderSchedulePage ClickCopyToButton_ContextMenu()
        {
            WaitForElementToBeClickable(contextMenu_CopyOption);
            Click(contextMenu_CopyOption);

            WaitForElementToBeClickable(contextMenu_CopyToButton);
            Click(contextMenu_CopyToButton);

            return this;
        }

        public ProviderSchedulePage ClickPastButton_ContextMenu()
        {
            WaitForElementToBeClickable(contextMenu_PastButton);
            Click(contextMenu_PastButton);

            return this;
        }

        public ProviderSchedulePage ClickSpecifyCareWorkerButton_ContextMenu()
        {
            WaitForElementToBeClickable(contextMenu_UpdateOption);
            Click(contextMenu_UpdateOption);

            WaitForElementToBeClickable(contextMenu_SpecifyCareWorkerButton);
            Click(contextMenu_SpecifyCareWorkerButton);

            return this;
        }

        #endregion

    }
}
