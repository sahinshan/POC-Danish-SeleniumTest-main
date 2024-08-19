using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class EmployeeDiaryPage : CommonMethods
    {
        public EmployeeDiaryPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id, 'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By AddBookingBtn = By.XPath("//*[@id='id--home--create-booking']/button");
        readonly By ChangeDate_Btn = By.XPath("//span[contains(text(),'Change Date')]");
        readonly By PreviousDateBtn = By.XPath("//span[text()='Previous Day']/parent::button");
        readonly By CalenderDatePicker = By.XPath("//*[@id='id--date-controls--change-date--anchor']");
        readonly By TodayDateBtn = By.XPath("//*[@id='id--date-controls--today']");

        By DiaryBooking(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']");
        By NextDayDiaryBooking(string date,string Recordid) => By.XPath("//div[@data-id='"+date+"']//div[@data-id='" + Recordid + "']");

        readonly By ShowAllBookingsCheckBox = By.XPath("//input[@id = 'id--home--show-all-bookings--checkbox']");
        readonly By EmploymentContractDropdown = By.XPath("//div[@id = 'id--home--employee-contract']");

        readonly By HomeTab = By.XPath("//li[@role='tab']/label/span[text()='Home']");
        readonly By FiltersTab = By.XPath("//li[@role='tab']/label/span[text()='Filters']");

        readonly By SelectedEmploymentContractText = By.XPath("//*[@id='id--home--employee-contract']//*[@class ='cd-dropdown-display-text']");
        By EmploymentContract(string EmploymentContractText) => By.XPath("//ul[@class='cd-dropdown-list']//button[@title = '" + EmploymentContractText + "']");

        #region Tooltip text
        readonly By Staff_Label = By.XPath("//div[@id = 'id--slider-tooltip--staff']");
        readonly By Provider_Label = By.XPath("//div[@id = 'id--slider-tooltip--provider']");
        readonly By Address_Label = By.XPath("//div[@id = 'id--slider-tooltip--provider-address']");
        readonly By BookingType_Label = By.XPath("//div[@id = 'id--slider-tooltip--booking-type']");
        readonly By Time_Label = By.XPath("//div[@id = 'id--slider-tooltip--planned-time']");
        readonly By ProviderDiary_StartEndDateLabel = By.XPath("//div[@id = 'id--slider-tooltip--booking-type']");

        By Tooltip_Label(string LabelName) => By.XPath("//span[@class='cd-slider-tooltip-text']//div[@id = 'id--slider-tooltip--" + LabelName + "']");


        #endregion

        public EmployeeDiaryPage WaitForEmployeeDiaryPageToLoad()
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

            WaitForElementVisible(AddBookingBtn);
            WaitForElementVisible(PreviousDateBtn);

            WaitForElementVisible(HomeTab);
            WaitForElementVisible(FiltersTab);

            WaitForElementVisible(CalenderDatePicker);

            return this;
        }

        public EmployeeDiaryPage ClickAddBookingButton()
        {
            WaitForElementToBeClickable(AddBookingBtn);
            Click(AddBookingBtn);

            return this;
        }

        public EmployeeDiaryPage ClickPreviousDateButton()
        {
            WaitForElementToBeClickable(PreviousDateBtn);
            Click(PreviousDateBtn);

            return this;
        }

        public EmployeeDiaryPage ClickTodayButton()
        {
            WaitForElementToBeClickable(TodayDateBtn);
            Click(TodayDateBtn);

            return this;
        }

        public EmployeeDiaryPage ClickChangeDate()
        {
            WaitForElementToBeClickable(ChangeDate_Btn);
            Click(ChangeDate_Btn);

            return this;


        }

        public EmployeeDiaryPage ClickDiaryBooking(string DiaryBookingid)
        {
            WaitForElement(DiaryBooking(DiaryBookingid));
            Click(DiaryBooking(DiaryBookingid));
            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public EmployeeDiaryPage ClickDiaryBooking(Guid DiaryBookingid)
        {
            return ClickDiaryBooking(DiaryBookingid.ToString());
        }

        public EmployeeDiaryPage ValidateDiaryBookingIsPresent(string Diarybookingid, bool IsPresent = true)
        {
            if (IsPresent)
                WaitForElementVisible(DiaryBooking(Diarybookingid));
            else
                WaitForElementNotVisible(DiaryBooking(Diarybookingid), 3);

            return this;
        }

        //method to validate showallbookings checkbox is visible
        public EmployeeDiaryPage ValidateShowAllBookingsCheckBoxIsVisible(bool isVisible = true)
        {
            var actualVisible = GetElementVisibility(ShowAllBookingsCheckBox);
            if (isVisible)
                Assert.AreEqual(true, actualVisible);
            else
                Assert.AreEqual(false, actualVisible);

            return this;
        }

        //method to click showallbookings checkbox
        public EmployeeDiaryPage ClickShowAllBookingsCheckBox()
        {
            WaitForElementToBeClickable(ShowAllBookingsCheckBox, true);
            Click(ShowAllBookingsCheckBox);

            return this;
        }

        //method to validate employment contract dropdown is visible
        public EmployeeDiaryPage ValidateEmploymentContractDropdownIsVisible(bool isVisible = true)
        {
            var actualVisible = GetElementVisibility(EmploymentContractDropdown);
            if (isVisible)
                Assert.AreEqual(true, actualVisible);
            else
                Assert.AreEqual(false, actualVisible);

            return this;
        }

        public EmployeeDiaryPage ValidateSelectedEmploymentContractText(string ExpectedText)
        {
            WaitForElementVisible(SelectedEmploymentContractText);
            ValidateElementText(SelectedEmploymentContractText, ExpectedText);

            return this;
        }

        //method to click employment contract dropdown
        public EmployeeDiaryPage ClickEmploymentContractDropdown()
        {
            WaitForElementVisible(EmploymentContractDropdown);
            WaitForElementToBeClickable(EmploymentContractDropdown, true);
            Click(EmploymentContractDropdown);

            return this;
        }

        public EmployeeDiaryPage SelectEmploymentContract(String EmploymentContractText)
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
        public EmployeeDiaryPage ValidateEmploymentContractTextStatus(string EmploymentContractText, bool IsPresent = true)
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

        public EmployeeDiaryPage MouseHoverDiaryBooking(String Diarybookingid)
        {
            WaitForElementToBeClickable(DiaryBooking(Diarybookingid));
            MouseHover(DiaryBooking(Diarybookingid));

            return this;
        }

        public EmployeeDiaryPage MouseHoverNextDayDiaryBooking(string Date,String Diarybookingid)
        {
            WaitForElementToBeClickable(NextDayDiaryBooking(Date, Diarybookingid));
            MouseHover(NextDayDiaryBooking(Date,Diarybookingid));

            return this;
        }

        public EmployeeDiaryPage ValidateTimeLabelText(string ExpectedText)
        {
            //ScrollToElement(Time_Label);
            //System.Threading.Thread.Sleep(1000);
            WaitForElement(Time_Label);
            ValidateElementTextContainsText(Time_Label, ExpectedText);

            return this;
        }

        public EmployeeDiaryPage ValidateStartEndDateLabelText(string ExpectedText)
        {
            ValidateElementText(ProviderDiary_StartEndDateLabel, ExpectedText);
            return this;
        }

        public EmployeeDiaryPage ValidateBookingTypeLabelText(string ExpectedText)
        {
            WaitForElement(BookingType_Label);
            ValidateElementTextContainsText(BookingType_Label, ExpectedText);

            return this;
        }

        public EmployeeDiaryPage ValidateStaffLabelText(string ExpectedText)
        {
            WaitForElement(Staff_Label);
            ValidateElementTextContainsText(Staff_Label, ExpectedText);

            return this;
        }


        public EmployeeDiaryPage ValidateAddressLabelText(string ExpectedText)
        {
            WaitForElement(Address_Label);
            ValidateElementTextContainsText(Address_Label, ExpectedText);

            return this;
        }

        public EmployeeDiaryPage ValidateProviderLabelText(string ExpectedText)
        {
            WaitForElement(Provider_Label);
            ValidateElementTextContainsText(Provider_Label, ExpectedText);

            return this;
        }

        public EmployeeDiaryPage ClickDatePicker()
        {
            WaitForElementToBeClickable(CalenderDatePicker);
            Click(CalenderDatePicker);

            return this;


        }


    }
}
