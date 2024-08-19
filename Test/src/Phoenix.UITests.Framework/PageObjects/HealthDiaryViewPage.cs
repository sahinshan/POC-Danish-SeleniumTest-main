using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class HealthDiaryViewPage : CommonMethods
    {

        public HealthDiaryViewPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By pageHeader = By.XPath("//div[@id = 'CWHeader']/div/h1");
        readonly By date_field = By.Id("CWClinicDate");
        readonly By communityClinicTeam_LookupButton = By.Id("CWLookupBtn_CWClinicTypeId");
        readonly By location_LookupButton = By.Id("CWLookupBtn_CWProviderId");
        By clinicRoom_Checkbox(string roomID) => By.XPath("//input[@id = 'CWResource1'][@value = '"+roomID+"']");
        readonly By homeVisit_YesOption = By.Id("CWHomeVisitId_1");
        readonly By homeVisit_NoOption = By.Id("CWHomeVisitId_0");

        readonly By calendarArea = By.XPath("//div[@id = 'calendarcontainer']");
        readonly By calendarHeader_CurrentDay = By.XPath("//th[@class = 'calheader currentday']");
        readonly By calendarBodyArea = By.XPath("//div[@id = 'calbodycontainer']/table[@id = 'bodytable']/tbody");
        
        By calendarBodyRow(string calendarTime) => By.XPath("//div[@id = 'calbodycontainer']/table[@id = 'bodytable']/tbody/tr[@data-time = '"+ calendarTime + "']");
        
        By calendarBodyDateCell(string calendarTime, string calendarDate) => By.XPath("//div[@id = 'calbodycontainer']/table[@id = 'bodytable']/tbody/tr[@data-time = '"+ calendarTime+"']/td[@data-calendardate='"+calendarDate+"']");
        
        By calendarBodyAppointmentRow(string calendarTime, string calendarDate, string recordID) => By.XPath("//div[@id = 'calbodycontainer']/table[@id = 'bodytable']/tbody/tr[@data-time = '"+ calendarTime+"']/td[@data-calendardate='"+calendarDate+"']/a[@id = '"+recordID+"']");

        By calendarBodyAppointRowTitle(string calendarTime, string calendarDate, string recordID, string appointmentName) => By.XPath("//div[@id = 'calbodycontainer']/table[@id = 'bodytable']/tbody/tr[@data-time = '" + calendarTime + "']/td[@data-calendardate='" + calendarDate + "']/a[@id = '" + recordID + "']/div[@class = 'eventtitle' and @title = '"+appointmentName+"']");

        public HealthDiaryViewPage WaitForHealthDiaryViewPageToLoad()
        {
            SwitchToDefaultFrame();


            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(pageHeader);

            WaitForElementToBeClickable(date_field);
            WaitForElementToBeClickable(communityClinicTeam_LookupButton);
            WaitForElementToBeClickable(homeVisit_YesOption);
            WaitForElementToBeClickable(homeVisit_NoOption);

            return this;
        }

        public HealthDiaryViewPage WaitForCalendarToLoad()
        {
            WaitForElement(calendarArea);

            return this;
        }

        public HealthDiaryViewPage InsertDate(string date)
        {
            WaitForElement(date_field);
            SendKeys(date_field, date);

            return this;
        }

        public HealthDiaryViewPage ClickCommunityClinicTeamLookupButton()
        {
            WaitForElement(communityClinicTeam_LookupButton);
            Click(communityClinicTeam_LookupButton);

            return this;
        }

        public HealthDiaryViewPage ClickLocationLookupButton()
        {
            WaitForElement(location_LookupButton);
            Click(location_LookupButton);

            return this;
        }

        public HealthDiaryViewPage SelectClinicRoom(string roomID)
        {
            WaitForElement(clinicRoom_Checkbox(roomID));
            if(!driver.FindElement(clinicRoom_Checkbox(roomID)).GetAttribute("checked").Equals("checked"))
                Click(clinicRoom_Checkbox(roomID));

            return this;
        }

        public HealthDiaryViewPage ClickHomeVisitYesNoOption(bool optionToSelect)
        {
            WaitForElement(homeVisit_YesOption);
            WaitForElement(homeVisit_NoOption);
            if (optionToSelect)
            {
                ScrollToElement(homeVisit_YesOption);
                Click(homeVisit_YesOption);
            }
            else
            {
                ScrollToElement(homeVisit_NoOption);
                Click(homeVisit_NoOption);
            }

            return this;
        }

        public HealthDiaryViewPage ValidateUnscheduledHealthAppointmentNotDisplayed(string time, string date, string healthAppointmentID)
        {            
            bool appointmentDisplayed = GetElementVisibility(calendarBodyAppointmentRow(time, date, healthAppointmentID));
            Assert.IsFalse(appointmentDisplayed);

            return this;
        }

        public HealthDiaryViewPage ValidateHealthAppointmentDisplayed(string time, string date, string healthAppointmentID)
        {
            WaitForElementVisible(calendarBodyAppointmentRow(time, date, "Event_" + healthAppointmentID));

            return this;
        }

        public HealthDiaryViewPage ClickHealthAppointment(string time, string date, string healthAppointmentID)
        {
            WaitForElementToBeClickable(calendarBodyAppointmentRow(time, date, "Event_" + healthAppointmentID));
            Click(calendarBodyAppointmentRow(time, date, "Event_" + healthAppointmentID));

            return this;
        }

        public HealthDiaryViewPage ClickHealthDiaryDateCell(string time, string date)
        {
            WaitForElementToBeClickable(calendarBodyDateCell(time, date));
            ScrollToElement(calendarBodyDateCell(time, date));
            Click(calendarBodyDateCell(time, date));
            
            return this;
        }

    }
}
