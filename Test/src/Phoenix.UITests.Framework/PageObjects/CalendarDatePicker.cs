using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the calendar picker window next to a date field which is displayed to the user.
    /// </summary>
    public class CalendarDatePicker : CommonMethods
    {
        public CalendarDatePicker(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By calendarTable = By.XPath("//table[@class = 'ui-datepicker-calendar']");
        readonly By monthPicklist = By.XPath("//select[@class = 'ui-datepicker-month'][@aria-label = 'Select month']");
        readonly By yearPicklist = By.XPath("//select[@class = 'ui-datepicker-year'][@aria-label = 'Select year']");

        By date(string date) => By.XPath("//td[@data-handler = 'selectDay']/a[@data-date = '"+date+"']");
       
        public CalendarDatePicker WaitForCalendarPickerPopupToLoad()
        {
            WaitForElement(calendarTable);
            WaitForElement(monthPicklist);
            WaitForElement(yearPicklist);            

            return this;
        }

       
        public CalendarDatePicker ValidateDatePresent(DateTime ExpectedDate)
        {            
            WaitForElementVisible(date(ExpectedDate.ToString("d")));

            return this;
        }

        
        public CalendarDatePicker SelectCalendarDate(DateTime ExpectedDate)
        {            
            SelectCalenderYear(ExpectedDate.ToString("yyyy"));
            SelectCalenderMonth(ExpectedDate.ToString("MMM"));
            Click(date(ExpectedDate.Day.ToString()));

            return this;
        }

        public CalendarDatePicker SelectCalenderYear(String YearToSelect)
        {
            WaitForElementToBeClickable(yearPicklist);
            MoveToElementInPage(yearPicklist);
            SelectPicklistElementByValue(yearPicklist, YearToSelect);
            return this;
        }

        public CalendarDatePicker SelectCalenderMonth(String MonthToSelect)
        {
            WaitForElementToBeClickable(monthPicklist);
            MoveToElementInPage(monthPicklist);
            SelectPicklistElementByText(monthPicklist, MonthToSelect);
            return this;
        }

    }
}
