using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ProviderDiaryPage : CommonMethods
    {
        public ProviderDiaryPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        
        
        #region Locators

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By AddBookingBtn = By.XPath("//*[@id='id--home--create-booking']/button");
        readonly By ChangeDate_Btn = By.XPath("//*[@id='id--date-controls--change-date--anchor']");
        readonly By calendarYearInput = By.XPath("//input[@aria-label='Year']");
        readonly By calendarMonthPicklist = By.XPath("//select[@aria-label='Month']");
        By calendarDayButton(string DateToSelect) => By.XPath("//span[@aria-label='" + DateToSelect + "']");

        readonly By HomeTab = By.XPath("//li[@role='tab']/label/span[text()='Home']");
        readonly By ViewTab = By.XPath("//li[@role='tab']/label/span[text()='View']");

        readonly By PreviousDateBtn = By.XPath("//button//*[@name='left_arrow']");
        readonly By NextDateBtn = By.XPath("//button//*[@name='right_arrow']");

        readonly By ProviderDiary_CareTaskAssignedText = By.XPath("//div[@id='id--care-tasks--care-tasks-list']//li");
        readonly By MinusBtn = By.XPath("//*[@id='id--view--zoom-out']/button");
        readonly By ViewWeekBtn = By.XPath("//div[@id ='id--view--view-week']/button");
        readonly By GridDatePicker = By.XPath("//input[@id = 'id--date-controls--change-date--grid-date-picker']");

        readonly By providerPicklistTopDiv = By.XPath("//*[@id='id--home--provider']");
        readonly By providerSearchInput = By.XPath("//*[@id='id--home--provider--dropdown-filter']");
        By picklistProviderRecord(string ProviderName) => By.XPath("//*[@id='id--home--provider']//button[@title='" + ProviderName + "']");
        By picklistProviderRecord(string ProviderName, Guid ProviderId) => By.XPath("//*[@id='id--home--provider']//li[@data-id = '" + ProviderId + "']//button[text()='" + ProviderName + "']");
        By picklistProviderRecordHighlighted(string ProviderName) => By.XPath("//*[@id='id--home--provider']//b[text()='" + ProviderName + "']");


        By DiaryBooking(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']");
        By UnassignedDiaryBooking(string Recordid) => By.XPath("//div[@data-id='00000000-0000-0000-0000-000000000000']//div[@data-id='" + Recordid + "']");
        By DiaryBooking(string SystemUserEmploymentContractId, string Recordid) => By.XPath("//div[@data-id='" + SystemUserEmploymentContractId + "']//div[@data-id='" + Recordid + "']");
        readonly By ProviderDiary_BookingTypeLabel = By.XPath("//div[@id = 'id--slider-tooltip--booking-type']");

        //this is the locator that represents a staff contract horizontal area in the screen
        By staffContractArea(string EmploymentContractId) => By.XPath("//div[@data-id='" + EmploymentContractId + "'][@class='cd-grid-column-sliders']");


        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.XPath("//button[@id='CWRefreshButton']");
        readonly By ProviderDiary_TimeLabel = By.XPath("//div[@id='id--slider-tooltip--planned-time']");

        readonly By ProviderPickList = By.XPath("//*[@id='id--home--provider']//div[@class='cd-dropdown-display']");
        By Provider_ProviderDiaryText(string Provider) => By.XPath("//*[@id='id--home--provider']//*[@title='" + Provider + "']");

        readonly By ProviderDiary_StaffLabel = By.XPath("//div[@id = 'id--slider-tooltip--staff']");

        readonly By Booking_Status = By.XPath("//*[@class='mcc-alert mcc-alert--success cd-snackbar cd-snackbar-show']");



        #region Context Menu

        readonly By contextMenu_OuterDiv = By.XPath("//div[@class='cd-context-menu']");

        readonly By contextMenu_ConfirmOption = By.XPath("//*[@id='cd-context-menu-3']");
        readonly By contextMenu_UpdateOption = By.XPath("//*[@id='cd-context-menu-5']");

        readonly By contextMenu_ConfirmAllButton = By.XPath("//*[@id='cd-context-menu-3-confirm-all']");

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


        #endregion



        public ProviderDiaryPage WaitForProviderDiaryPageToLoad()
        {
            System.Threading.Thread.Sleep(1500);

            WaitForElementNotVisible("CWRefreshPanel", 40);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 40);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 40);

            WaitForElementVisible(HomeTab);
            WaitForElementVisible(ViewTab);

            return this;
        }

        public ProviderDiaryPage SearchProviderRecord(string ProviderName)
        {
            WaitForElementToBeClickable(HomeTab);
            ScrollToElement(HomeTab);
            Click(HomeTab);

            WaitForElementToBeClickable(providerPicklistTopDiv);
            Click(providerPicklistTopDiv);

            bool elementVisible = GetElementVisibility(providerSearchInput);
            System.Threading.Thread.Sleep(2000);
            if (elementVisible)
            {
                SendKeys(providerSearchInput, ProviderName);
                System.Threading.Thread.Sleep(2000);
                WaitForElement(picklistProviderRecordHighlighted(ProviderName));
                Click(picklistProviderRecordHighlighted(ProviderName));
            }
            else
            {
                WaitForElement(picklistProviderRecord(ProviderName));
                System.Threading.Thread.Sleep(2000);
                Click(picklistProviderRecord(ProviderName));
            }

            return this;
        }

        public ProviderDiaryPage SearchProviderRecord(string ProviderName, Guid ProviderId)
        {
            WaitForElementToBeClickable(HomeTab);
            ScrollToElement(HomeTab);
            Click(HomeTab);

            WaitForElementToBeClickable(providerPicklistTopDiv);
            Click(providerPicklistTopDiv);

            bool elementVisible = GetElementVisibility(providerSearchInput);
            if (elementVisible)
            {
                SendKeys(providerSearchInput, ProviderName);

                WaitForElement(picklistProviderRecordHighlighted(ProviderName));
                Click(picklistProviderRecordHighlighted(ProviderName));
            }
            else
            {
                WaitForElement(picklistProviderRecord(ProviderName, ProviderId));
                Click(picklistProviderRecord(ProviderName, ProviderId));
            }

            return this;
        }

        public ProviderDiaryPage ClickPreviousDateButton()
        {
            WaitForElementToBeClickable(ViewTab);
            ScrollToElement(ViewTab);
            Click(ViewTab);

            WaitForElementToBeClickable(PreviousDateBtn);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            Click(PreviousDateBtn);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public ProviderDiaryPage ClickNextDateButton()
        {
            WaitForElementToBeClickable(ViewTab);
            ScrollToElement(ViewTab);
            Click(ViewTab);

            WaitForElementToBeClickable(NextDateBtn);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            Click(NextDateBtn);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public ProviderDiaryPage ClickDiaryBooking(string Diarybookingid)
        {
            WaitForElementToBeClickable(DiaryBooking(Diarybookingid), true);
            Click(DiaryBooking(Diarybookingid));

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ProviderDiaryPage ClickDiaryBooking(Guid Diarybookingid)
        {
            return ClickDiaryBooking(Diarybookingid.ToString());
        }

        public ProviderDiaryPage RightClickDiaryBooking(string Diarybookingid)
        {
            WaitForElementToBeClickable(DiaryBooking(Diarybookingid));
            RightClick(DiaryBooking(Diarybookingid));

            return this;
        }

        public ProviderDiaryPage RightClickDiaryBooking(Guid Diarybookingid)
        {
            return RightClickDiaryBooking(Diarybookingid.ToString());
        }


        public ProviderDiaryPage ValidateUnassignedDiaryBookingIsPresent(string DiaryBookingId)
        {
            WaitForElementVisible(UnassignedDiaryBooking(DiaryBookingId));

            return this;
        }

        public ProviderDiaryPage ValidateUnassignedDiaryBookingIsNotPresent(string DiaryBookingId)
        {
            WaitForElementNotVisible(UnassignedDiaryBooking(DiaryBookingId), 3);

            return this;
        }


        public ProviderDiaryPage ValidateCareTasksAssigned(string ExpectedText)
        {
            ValidateElementText(ProviderDiary_CareTaskAssignedText, ExpectedText);

            return this;
        }

        public ProviderDiaryPage ClickChangeDate(string Year, string Month, string Day)
        {
            WaitForElementToBeClickable(ChangeDate_Btn);
            ScrollToElement(ChangeDate_Btn);
            Click(ChangeDate_Btn);

            WaitForElement(calendarYearInput);
            SendKeys(calendarYearInput, Year);

            WaitForElement(calendarMonthPicklist);
            SelectPicklistElementByText(calendarMonthPicklist, Month);

            WaitForElement(calendarDayButton(Month + " " + Day + ", " + Year));
            Click(calendarDayButton(Month + " " + Day + ", " + Year));

            return this;
        }

        public ProviderDiaryPage ClickMinusButton()
        {
            WaitForElementToBeClickable(ViewTab);
            ScrollToElement(ViewTab);
            Click(ViewTab);

            WaitForElementToBeClickable(MinusBtn);
            Click(MinusBtn);
            WaitForElementNotVisible("CWRefreshPanel", 80);
            return this;
        }

        public ProviderDiaryPage ClickViewWeekButton()
        {
            ClickOnViewTab();

            WaitForElementToBeClickable(ViewWeekBtn);
            Click(ViewWeekBtn);
            WaitForElementNotVisible("CWRefreshPanel", 80);
            return this;
        }

        public ProviderDiaryPage ClickDatePicker()
        {
            WaitForElementToBeClickable(GridDatePicker);
            Click(GridDatePicker);

            return this;
        }

        public ProviderDiaryPage MouseHoverDiaryBooking(String Diarybookingid)
        {

            WaitForElementToBeClickable(DiaryBooking(Diarybookingid));
            MouseHover(DiaryBooking(Diarybookingid));
            WaitForElementNotVisible("CWRefreshPanel", 30);
            return this;
        }

        public ProviderDiaryPage ValidateTimeLabelText(string ExpectedText)
        {
            var elementInnerText = GetElementTextByJavascript("id--slider-tooltip--planned-time");
            Assert.AreEqual(ExpectedText, elementInnerText);
            return this;
        }

        public ProviderDiaryPage ValidateBookingTypeLabelText(string ExpectedText)
        {
            ValidateElementTextContainsText(ProviderDiary_BookingTypeLabel, ExpectedText);

            return this;
        }

        public ProviderDiaryPage ValidateStaffLabelText(string ExpectedText)
        {
            ValidateElementTextContainsText(ProviderDiary_StaffLabel, ExpectedText);

            return this;
        }

        public ProviderDiaryPage selectProvider(String ProviderDiaryText)
        {
            SearchProviderRecord(ProviderDiaryText);

            return this;
        }

        public ProviderDiaryPage selectProvider(String ProviderDiaryText, Guid ProviderId)
        {
            SearchProviderRecord(ProviderDiaryText, ProviderId);

            return this;
        }

        public ProviderDiaryPage clickAddBooking()
        {
            WaitForElementToBeClickable(HomeTab);
            Click(HomeTab);

            WaitForElementToBeClickable(AddBookingBtn);
            Click(AddBookingBtn);

            return this;
        }

        public ProviderDiaryPage ValidateDiaryBookingIsPresent(string Schedulebookingid, bool IsPresent = true)
        {
            if (IsPresent)
                WaitForElementVisible(DiaryBooking(Schedulebookingid));
            else
                WaitForElementNotVisible(DiaryBooking(Schedulebookingid), 3);

            return this;
        }

        public ProviderDiaryPage ValidateDiaryBookingIsPresent(string systemuseremploymentcontractid, string Schedulebookingid, bool IsPresent = true)
        {
            if (IsPresent)
                WaitForElementVisible(DiaryBooking(systemuseremploymentcontractid, Schedulebookingid));
            else
                WaitForElementNotVisible(DiaryBooking(systemuseremploymentcontractid, Schedulebookingid), 3);

            return this;
        }

        public ProviderDiaryPage ValidateBookingStatus(string ExpectedText)
        {
            WaitForElementVisible(Booking_Status);
            ValidateElementText(Booking_Status, ExpectedText);

            return this;
        }

        public ProviderDiaryPage ClickOnHomeTab()
        {
            WaitForElementToBeClickable(HomeTab);
            Click(HomeTab);

            return this;
        }

        public ProviderDiaryPage ClickOnViewTab()
        {
            WaitForElementToBeClickable(ViewTab);
            Click(ViewTab);

            return this;
        }

        public ProviderDiaryPage ValidateProviderIsPresent(String ProviderDiaryText, bool IsPresent = true)
        {
            if (IsPresent)
            {
                WaitForElement(Provider_ProviderDiaryText(ProviderDiaryText));
            }
            else
            {
                WaitForElementRemoved(Provider_ProviderDiaryText(ProviderDiaryText));
            }
            
            return this;
        }

        public ProviderDiaryPage RightClickGridArea(string StaffEmploymentContractId)
        {
            WaitForElement(staffContractArea(StaffEmploymentContractId));
            RightClick(staffContractArea(StaffEmploymentContractId));

            return this;
        }

        public ProviderDiaryPage DragBookingToAnotherStaffMember(string DiaryBookingId, string TargetEmploymentContractId)
        {
            WaitForElementToBeClickable(DiaryBooking(DiaryBookingId));
            DragAndDropToTargetElement(DiaryBooking(DiaryBookingId), staffContractArea(TargetEmploymentContractId));

            return this;
        }

        public ProviderDiaryPage DragBookingToAnotherStaffMember(Guid DiaryBookingId, Guid TargetEmploymentContractId)
        {
            return DragBookingToAnotherStaffMember(DiaryBookingId.ToString(), TargetEmploymentContractId.ToString());
        }

        public ProviderDiaryPage RightClickGridArea(Guid StaffEmploymentContractId)
        {
            return RightClickGridArea(StaffEmploymentContractId.ToString());
        }

        #region Context Menu

        public ProviderDiaryPage WaitForContextMenuToLoad()
        {
            WaitForElementVisible(contextMenu_OuterDiv);
            WaitForElementVisible(contextMenu_UpdateOption);
            WaitForElementVisible(contextMenu_CopyOption);
            WaitForElementVisible(contextMenu_PastButton);
            WaitForElementVisible(contextMenu_DeleteButton);

            return this;
        }

        public ProviderDiaryPage ClickConfirmAllButton_ContextMenu()
        {
            WaitForElementToBeClickable(contextMenu_ConfirmOption);
            Click(contextMenu_ConfirmOption);

            WaitForElementToBeClickable(contextMenu_ConfirmAllButton);
            Click(contextMenu_ConfirmAllButton);

            return this;
        }

        public ProviderDiaryPage ClickCopyButton_ContextMenu()
        {
            WaitForElementToBeClickable(contextMenu_CopyOption);
            Click(contextMenu_CopyOption);

            WaitForElementToBeClickable(contextMenu_CopyButton);
            Click(contextMenu_CopyButton);

            return this;
        }

        public ProviderDiaryPage ClickCopyToButton_ContextMenu()
        {
            WaitForElementToBeClickable(contextMenu_CopyOption);
            Click(contextMenu_CopyOption);

            WaitForElementToBeClickable(contextMenu_CopyToButton);
            Click(contextMenu_CopyToButton);

            return this;
        }

        public ProviderDiaryPage ClickPastButton_ContextMenu()
        {
            WaitForElementToBeClickable(contextMenu_PastButton);
            Click(contextMenu_PastButton);

            return this;
        }

        public ProviderDiaryPage ClickSpecifyCareWorkerButton_ContextMenu()
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
