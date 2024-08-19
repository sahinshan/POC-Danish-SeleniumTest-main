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
    public class Person_DiaryRecordPage : CommonMethods
    {
        public Person_DiaryRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By personAboutMeIFrame = By.Id("CWUrlPanel_IFrame");

        readonly By HomeTab = By.XPath("//li[@role='tab']/label/span[text()='Home']");
        readonly By FiltersTab = By.XPath("//li[@role='tab']/label/span[text()='Filters']");
        readonly By CalenderDatePicker = By.XPath("//*[@id='id--date-controls--change-date--anchor']");
        readonly By TodayDateBtn = By.XPath("//*[@id='id--date-controls--today']");

        By DiaryBooking(string Recordid) => By.XPath("//div[@data-id='" + Recordid + "']");
        By NextDayDiaryBooking(string date, string Recordid) => By.XPath("//div[@data-id='" + date + "']//div[@data-id='" + Recordid + "']");

        readonly By BookingType_Label = By.XPath("//div[@id = 'id--slider-tooltip--booking-type']");
        readonly By Time_Label = By.XPath("//div[@id = 'id--slider-tooltip--planned-time']");
        readonly By Provider_Label = By.XPath("//div[@id = 'id--slider-tooltip--provider']");
        
        
        public Person_DiaryRecordPage WaitForPersonDiaryPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(personAboutMeIFrame);
            SwitchToIframe(personAboutMeIFrame);

            WaitForElementVisible(HomeTab);
            WaitForElementVisible(FiltersTab);

            WaitForElementVisible(CalenderDatePicker);

            return this;
        }

        public Person_DiaryRecordPage ClickTodayButton()
        {
            WaitForElementToBeClickable(TodayDateBtn);
            Click(TodayDateBtn);

            return this;
        }

        public Person_DiaryRecordPage MouseHoverDiaryBooking(String Diarybookingid)
        {
            WaitForElementToBeClickable(DiaryBooking(Diarybookingid));
            MouseHover(DiaryBooking(Diarybookingid));

            return this;
        }

        public Person_DiaryRecordPage MouseHoverNextDayDiaryBooking(string Date, String Diarybookingid)
        {
            WaitForElementToBeClickable(NextDayDiaryBooking(Date, Diarybookingid));
            MouseHover(NextDayDiaryBooking(Date, Diarybookingid));

            return this;
        }

        public Person_DiaryRecordPage ValidateTimeLabelText(string ExpectedText)
        {
            
            WaitForElement(Time_Label);
            ValidateElementTextContainsText(Time_Label, ExpectedText);

            return this;
        }

        public Person_DiaryRecordPage ValidateBookingTypeLabelText(string ExpectedText)
        {
            WaitForElement(BookingType_Label);
            ValidateElementTextContainsText(BookingType_Label, ExpectedText);

            return this;
        }

        public Person_DiaryRecordPage ValidateProviderLabelText(string ExpectedText)
        {
            WaitForElement(Provider_Label);
            ValidateElementTextContainsText(Provider_Label, ExpectedText);

            return this;
        }

    }
}
