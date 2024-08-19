using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings
{
    public class SystemUser_DiaryPage : CommonMethods
    {
        public SystemUser_DiaryPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialog_IFrame = By.XPath("//iframe[starts-with(@id, 'iframe_CWDialog_')][contains(@src, 'type=systemuser&')]");
        readonly By diaryIFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id = 'CWHeader']/div/h1");
        readonly By date_field = By.Id("CWUserDiaryDate");
        readonly By view_Picklist = By.Id("CWUserDiaryView");

       

        readonly By calendarArea = By.XPath("//div[@id = 'calendarcontainer']");
        By calendarBodyAppointmentRow(string calendarTime, string calendarDate, string recordID) => By.XPath("//div[@id = 'calbodycontainer']/table[@id = 'bodytable']/tbody/tr[@data-time = '" + calendarTime + "']/td[@data-calendardate='" + calendarDate + "']/a[@id = '" + recordID + "']");

        By calendarBodyAppointRowTitle(string calendarTime, string calendarDate, string recordID, string appointmentName) => By.XPath("//div[@id = 'calbodycontainer']/table[@id = 'bodytable']/tbody/tr[@data-time = '" + calendarTime + "']/td[@data-calendardate='" + calendarDate + "']/a[@id = '" + recordID + "']/div[@class = 'eventtitle' and @title = '" + appointmentName + "']");

        public SystemUser_DiaryPage WaitForDiaryPageToLoad()
        {        

            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(cwDialog_IFrame);
            SwitchToIframe(cwDialog_IFrame);

            WaitForElement(diaryIFrame);
            SwitchToIframe(diaryIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(pageHeader);

            WaitForElement(date_field);
            WaitForElement(view_Picklist);
            
            return this;
        }

        public SystemUser_DiaryPage WaitForCalendarToLoad()
        {            

            WaitForElement(calendarArea);

            WaitForElementNotVisible("CWRefreshPanel", 5);

            return this;
        }

        public SystemUser_DiaryPage InsertDate(string date)
        {
            WaitForElement(date_field);
            SendKeys(date_field, date);

            return this;
        }

        public SystemUser_DiaryPage ValidateUnscheduledHealthAppointmentNotDisplayed(string time, string date, string healthAppointmentID)
        {
            bool appointmentDisplayed = GetElementVisibility(calendarBodyAppointmentRow(time, date, healthAppointmentID));
            Assert.IsFalse(appointmentDisplayed);

            return this;
        }
        
        public SystemUser_DiaryPage ValidateSelectedViewPicklistOption(string optionSelected)
        {           
            WaitForElement(view_Picklist);
            ValidatePicklistSelectedText(view_Picklist, optionSelected);

            return this;
        }
    }
}
