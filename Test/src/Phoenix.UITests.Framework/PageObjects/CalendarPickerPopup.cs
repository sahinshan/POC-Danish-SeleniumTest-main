using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the dynamic dialog that is displayed with messages to the user.
    /// </summary>
    public class CalendarPickerPopup : CommonMethods
    {
        public CalendarPickerPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Header

        readonly By popupHeader = By.XPath("//*[@class='mcc-dialog__header']/h2");
        readonly By cancelButton=By.XPath("//*[@class='mcc-button__label'][text()='Cancel']");
        readonly By doneButton = By.XPath("//*[@class='mcc-button__label'][text()='Done']");

        #endregion

        readonly By previousMonthButton = By.XPath("//*[contains(@class, 'flatpickr-calendar')]/div/span[contains(@class,'prev-month')]");
        readonly By monthPicklist = By.XPath("//*[contains(@class, 'flatpickr-calendar')]/div/div/div/select");
        readonly By yearTextbox = By.XPath("//*[contains(@class, 'flatpickr-calendar')]/div/div/div/div/input");
        readonly By nextMonthButton = By.XPath("//*[contains(@class, 'flatpickr-calendar')]/div/span[contains(@class,'next-month')]");
        readonly By DatePickerButton = By.XPath("//input[@id='id--fields--1--date-picker']");
        readonly By MonthDropDown= By.XPath("//select[@class='flatpickr-monthDropdown-months']");

        By daySpan(string dayAriaLabel) => By.XPath("//*[contains(@class, 'flatpickr-calendar')]/div/div/div/div[@class='dayContainer']/span[@aria-label='" + dayAriaLabel + "']");
        By daySpan1(string dayAriaLabel) => By.XPath("//div[@class='dayContainer']/span[@aria-label='" + dayAriaLabel + "']");


        public CalendarPickerPopup WaitForChangeDateDialogPopupToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(popupHeader);
            WaitForElement(cancelButton);
            WaitForElement(doneButton);
            WaitForElement(DatePickerButton);

            return this;
        }

        public CalendarPickerPopup WaitForCalendarPickerPopupToLoad()
        {
            WaitForElement(previousMonthButton);
            WaitForElement(monthPicklist);
            WaitForElement(yearTextbox);
            WaitForElement(nextMonthButton);

            return this;
        }

        public CalendarPickerPopup ValidateDatePresent(DateTime ExpectedDate)
        {
            var dateAriaLabelFormat = ExpectedDate.ToString("MMMM d, yyyy");
            WaitForElementVisible(daySpan(dateAriaLabelFormat));

            return this;
        }

        public CalendarPickerPopup ClickOnCalendarDate(DateTime ExpectedDate)
        {
            var dateAriaLabelFormat = ExpectedDate.ToString("MMMM d, yyyy");
            var expectedyear = ExpectedDate.ToString("yyyy");
            var currentyear = DateTime.Now.ToString("yyyy");
            
            if (expectedyear != currentyear)
            {
                WaitForElement(yearTextbox);
                MoveToElementInPage(yearTextbox);
                SendKeys(yearTextbox, expectedyear);
            }

            var expectedmonth = ExpectedDate.ToString("MMMM");
            var currentmonth = DateTime.Now.ToString("MMMM");
            if(expectedmonth== currentmonth)
            {
                WaitForElement(daySpan1(dateAriaLabelFormat));
                MoveToElementInPage(daySpan(dateAriaLabelFormat));
                ClickOnElementWithSize(daySpan(dateAriaLabelFormat));
            }            
            else
            {
                SelectPicklistElementByText(MonthDropDown, expectedmonth);
                WaitForElement(daySpan1(dateAriaLabelFormat));
                MoveToElementInPage(daySpan1(dateAriaLabelFormat));
                ClickOnElementWithSize(daySpan1(dateAriaLabelFormat));
            }

            return this;
        }
        
        public CalendarPickerPopup ClickOnDatePicker()
        {
            WaitForElement(DatePickerButton);
            System.Threading.Thread.Sleep(2000);
            ClickOnElementWithSize(DatePickerButton);

            return this;
        }

        public CalendarPickerPopup ClickOnDone()
        {
            WaitForElement(doneButton);
            ClickOnElementWithSize(doneButton);

            return this;
        }

        public CalendarPickerPopup ClickOnCancel()
        {
            WaitForElement(cancelButton);
            ClickOnElementWithSize(cancelButton);

            return this;
        }

    }
}
