using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the calendar time picker window next to a date field which is displayed to the user.
    /// </summary>
    public class CalendarTimePicker : CommonMethods
    {
        public CalendarTimePicker(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By timePickerTable = By.XPath("//*[@id= 'ui-timepicker-div']");
        readonly By timeHourPickerTitle = By.XPath("//*[@class = 'ui-timepicker-hours']/div");
        readonly By timeMinutePickerTitle = By.XPath("//*[@class = 'ui-timepicker-minutes']/div");
        By timePickerHourCell(string Hour) => By.XPath("//*[@class = 'ui-timepicker-hour-cell']/a[text() = '" + Hour + "']");
        By timePickerMinuteCell(string Minute) => By.XPath("//*[@class = 'ui-timepicker-minute-cell']/a[text() = '" + Minute + "']");

        By date(string date) => By.XPath("//td[@data-handler = 'selectDay']/a[@data-date = '" + date + "']");

        public CalendarTimePicker WaitForCalendarTimePickerPopupToLoad()
        {
            WaitForElement(timePickerTable);
            WaitForElement(timeHourPickerTitle);
            WaitForElement(timeMinutePickerTitle);

            return this;
        }

        public CalendarTimePicker ValidateDatePresent(DateTime ExpectedDate)
        {
            WaitForElementVisible(date(ExpectedDate.ToString("d")));

            return this;
        }

        public CalendarTimePicker SelectTime(DateTime ExpectedTime)
        {
            SelectHour(ExpectedTime.ToString("HH"));
            SelectMinute(ExpectedTime.ToString("mm"));

            return this;
        }

        public CalendarTimePicker SelectHour(String HourToSelect)
        {
            WaitForElement(timePickerHourCell(HourToSelect));
            ScrollToElement(timePickerHourCell(HourToSelect));
            Click(timePickerHourCell(HourToSelect));
            return this;
        }

        public CalendarTimePicker SelectMinute(String MinuteToSelect)
        {
            WaitForElement(timePickerMinuteCell(MinuteToSelect));
            ScrollToElement(timePickerMinuteCell(MinuteToSelect));
            Click(timePickerMinuteCell(MinuteToSelect));
            return this;
        }

    }
}
