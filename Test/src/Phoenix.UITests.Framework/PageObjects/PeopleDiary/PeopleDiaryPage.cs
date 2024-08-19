using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PeopleDiaryPage : CommonMethods
    {
        public PeopleDiaryPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By PeopleDiaryPageHeader = By.XPath("//div[@id='cd-header']//h1[text()='People Diary']");
        readonly By PeopleDiaryEditBookingPageHeader = By.XPath("//div/h2[text()='Edit Diary Booking']");

        readonly By homeTab = By.XPath("//*[@id='id--ribbon--tabs']//span[text()='Home']");
        readonly By viewTab = By.XPath("//*[@id='id--ribbon--tabs']//span[text()='View']");

        readonly By AddBookingBtn = By.XPath("//*[@id='id--home--create-booking']/button");

        readonly By HomeTab = By.XPath("//li[@role='tab']/label/span[text()='Home']");
        readonly By FiltersTab = By.XPath("//li[@role='tab']/label/span[text()='Filters']");
        readonly By ViewTab = By.XPath("//li[@role='tab']/label/span[text()='View']");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By PreviousDateBtn = By.XPath("//button//*[@name='left_arrow']");
        readonly By CloseButton = By.XPath("//span[text()='Close']/parent::button");
        readonly By SaveChangesButton = By.XPath("//span[text()='Save Changes']/parent::button");
        readonly By CareTasksButon = By.XPath("//span[text()='Care Tasks']/parent::label");
        readonly By ProviderDiary_CareTaskAssignedText = By.XPath("//div[@id='id--care-tasks--care-tasks-list']//li");
        readonly By ProviderDiary = By.XPath("//*[@id='id--home--provider']//div[@class='cd-dropdown-display']");
        readonly By ProviderSearchInput = By.XPath("//*[@id='id--home--provider--dropdown-filter']");

        readonly By MinusBtn = By.XPath("//button//*[@name='subtract']");
        readonly By PlusBtn = By.XPath("//button//*[@name = 'plus_math']");
        readonly By PeopleDiary_TimeLabel = By.XPath("//div[@id='id--slider-tooltip--planned-time']");
        readonly By PeopleDiary_StartEndDateLabel = By.XPath("//span[@class='cd-slider-tooltip-text']/div/span[text()='Start - End Date:']/following-sibling::span");
        readonly By PeopleDiary_BookingTypeLabel = By.XPath("//div[@id='id--slider-tooltip--booking-type']");
        By PeopleDiary_ProviderPicklistElement(string ProviderName) => By.XPath("//*[@id='id--home--provider']//button[@title='" + ProviderName + "']");




        By quickSearchTextBox(string providerName) => By.XPath("//div[@id='cd-select-provider']/div/div/div/div[text()='" + providerName + "']");
        By DiaryBooking(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']");

        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.XPath("//button[@id='CWRefreshButton']");
        By DiaryBookingByDate(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']/div[3]//div[@class='cd-slider-title']/span");
        readonly By ChangeDate_Btn = By.XPath("//span[contains(text(),'Change Date')]");

        readonly By GridDatePicker = By.XPath("//*[@id='id--date-controls--change-date']");
        By NextDateBtn = By.XPath("//*[@id='id--date-controls--next-day']");



        public PeopleDiaryPage WaitForPeopleDiaryPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(AddBookingBtn);

            return this;
        }

        public PeopleDiaryPage WaitForProviderDiaryEditPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(PeopleDiaryEditBookingPageHeader);
            WaitForElement(CloseButton);
            WaitForElement(SaveChangesButton);

            return this;
        }

        public PeopleDiaryPage SearchProviderRecord(string provider)
        {
            ClickHomeTab();

            WaitForElementToBeClickable(quickSearchTextBox(provider));
            Click(quickSearchTextBox(provider));

            return this;
        }

        public PeopleDiaryPage ClickPreviousDateButton()
        {
            ClickHomeTab();

            WaitForElementToBeClickable(PreviousDateBtn);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            Click(PreviousDateBtn);

            return this;
        }

        public PeopleDiaryPage ClickNextDateButton()
        {
            ClickHomeTab();

            WaitForElementToBeClickable(NextDateBtn);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            Click(NextDateBtn);

            return this;
        }

        public PeopleDiaryPage ClickChangeDate()
        {
            WaitForElementToBeClickable(ChangeDate_Btn);
            ScrollToElement(ChangeDate_Btn);
            Click(ChangeDate_Btn);
            WaitForElementNotVisible("CWRefreshPanel", 40);
            return this;


        }

        public PeopleDiaryPage ClickDatePicker()
        {
            WaitForElementToBeClickable(homeTab);
            Click(homeTab);

            WaitForElementToBeClickable(GridDatePicker);
            Click(GridDatePicker);

            return this;
        }

        public PeopleDiaryPage ValidateDiaryBooking(String Diarybookingid)
        {

            ValidateElementDoNotExist(DiaryBookingByDate(Diarybookingid));

            return this;
        }

        public PeopleDiaryPage ValidateDiaryBookingIsPresent(String Diarybookingid, bool isPresent = true)
        {

            if (isPresent)
            {
                WaitForElementVisible(DiaryBooking(Diarybookingid));
            }
            else
            {
                ValidateElementDoNotExist(DiaryBookingByDate(Diarybookingid));
            }


            return this;
        }

        public PeopleDiaryPage ClickHomeTab()
        {
            WaitForElementToBeClickable(ViewTab);
            Click(ViewTab);

            return this;
        }

        public PeopleDiaryPage ClickViewTab()
        {
            WaitForElementToBeClickable(ViewTab);
            Click(ViewTab);

            return this;
        }

        public PeopleDiaryPage ClickMinusButton()
        {
            ClickViewTab();

            WaitForElementToBeClickable(MinusBtn);
            Click(MinusBtn);
            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PeopleDiaryPage selectProvider(String ProviderDiaryText)
        {
            WaitForElementVisible(ProviderDiary);
            WaitForElementToBeClickable(ProviderDiary, true);
            Click(ProviderDiary);

            var elementVisible = WaitForElementVisibleWithoutException(ProviderSearchInput, 3);
            if (elementVisible)
            {
                SendKeys(ProviderSearchInput, ProviderDiaryText + Keys.Tab);
            }

            WaitForElementVisible(PeopleDiary_ProviderPicklistElement(ProviderDiaryText));
            ScrollToElement(PeopleDiary_ProviderPicklistElement(ProviderDiaryText));
            Click(PeopleDiary_ProviderPicklistElement(ProviderDiaryText));

            return this;
        }

        public PeopleDiaryPage clickAddBooking()
        {
            ClickHomeTab();

            WaitForElementToBeClickable(HomeTab);
            Click(HomeTab);

            WaitForElementToBeClickable(AddBookingBtn);
            Click(AddBookingBtn);

            return this;
        }

        public PeopleDiaryPage MouseHoverDiaryBooking(String Diarybookingid)
        {

            WaitForElementToBeClickable(DiaryBooking(Diarybookingid));
            MouseHover(DiaryBooking(Diarybookingid));

            WaitForElementNotVisible("CWRefreshPanel", 80);


            return this;
        }

        public PeopleDiaryPage ValidateTimeLabelText(string ExpectedText)
        {
            var actual=GetElementTextByJavascript(PeopleDiary_TimeLabel);
            if (!actual.Contains(ExpectedText))
            {
                throw new Exception("Failed to validate the element text. Expected: " + ExpectedText + " || Actual: " + actual + "");
            }

            return this;
        }

        public PeopleDiaryPage ValidateStartEndDateLabelText(string ExpectedText)
        {
            ValidateElementText(PeopleDiary_StartEndDateLabel, ExpectedText);

            return this;
        }

        public PeopleDiaryPage ValidateBookingTypeLabelText(string ExpectedText)
        {
            var actual = GetElementTextByJavascript(PeopleDiary_BookingTypeLabel);
            if (!actual.Contains(ExpectedText))
            {
                throw new Exception("Failed to validate the element text. Expected: " + ExpectedText + " || Actual: " + actual + "");
            }


            return this;
        }


        public PeopleDiaryPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public PeopleDiaryPage clickDiaryBooking(String Diarybookingid)
        {

            WaitForElementToBeClickable(DiaryBooking(Diarybookingid));
            Click(DiaryBooking(Diarybookingid));


            return this;
        }

    }
}
