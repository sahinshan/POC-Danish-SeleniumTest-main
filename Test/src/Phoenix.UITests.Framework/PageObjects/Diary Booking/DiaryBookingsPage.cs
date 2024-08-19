using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DiaryBookingsPage : CommonMethods
    {
        public DiaryBookingsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By BookingdiaryIFrame=By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpbookingdiary')]");
        readonly By ProviderDiaryPageHeader = By.XPath("//div[@id='cd-main']/div/h1[text()='Provider Diary']");
        readonly By ProviderDiaryEditBookingPageHeader = By.XPath("//div/h2[text()='Edit Diary Booking']");

        readonly By AddBookingBtn = By.Id("cd-new-booking");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By PreviousDateBtn = By.XPath("//span[text()='Previous Day']/parent::button");
        readonly By CloseButton = By.XPath("//span[text()='Close']/parent::button");
        readonly By SaveChangesButton = By.XPath("//span[text()='Save Changes']/parent::button");
        readonly By CareTasksButon = By.XPath("//span[text()='Care Tasks']/parent::label");
        readonly By ProviderDiary_CareTaskAssignedText= By.XPath("//div[@id='id--care-tasks--care-tasks-list']//li");
        readonly By DiaryBookingPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Diary Bookings']");
        readonly By PlannedStartDate_Field = By.XPath("//input[@id='CWField_plannedstartdate']");
        readonly By PlannedStartTime_Field = By.XPath("//input[@id='CWField_plannedstarttime']");

        readonly By PlannedEndDate_Field = By.XPath("//input[@id='CWField_plannedenddate']");
        readonly By PlannedEndTime_Field = By.XPath("//input[@id='CWField_plannedendtime']");

        readonly By ResponsibleTeam_Field = By.Id("CWField_ownerid_Link");
        readonly By LocationProvider_Field = By.Id("CWField_locationid_Link");
        By DiaryBooking(string Recordid) => By.XPath("//tr[@id='"+Recordid+"']/td[2]");

        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.XPath("//button[@id='CWRefreshButton']");

     



        public DiaryBookingsPage WaitForDiaryBookingPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(DiaryBookingPageHeader);

            ValidateElementText(DiaryBookingPageHeader, "Diary Bookings");

            return this;

           
        }


        public DiaryBookingsPage WaitForCPDiaryBookingPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(BookingdiaryIFrame);
            SwitchToIframe(BookingdiaryIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            

            return this;


        }


        public DiaryBookingsPage ClickDiaryBooking(String Diarybookingid)
        {

            WaitForElementToBeClickable(DiaryBooking(Diarybookingid));
            Click(DiaryBooking(Diarybookingid));

            WaitForElementNotVisible("CWRefreshPanel", 80);


            return this;
        }




        public DiaryBookingsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public DiaryBookingsPage ValidateCareTasksAssigned(string ExpectedText)
        {
            ValidateElementText(ProviderDiary_CareTaskAssignedText, ExpectedText);

            return this;
        }

        public DiaryBookingsPage ValidatePersonAbsence_PlannedStartDate(string expectedText)
        {
            WaitForElement(PlannedStartDate_Field);
            ValidateElementDisabled(PlannedStartDate_Field);
            ValidateElementValue(PlannedStartDate_Field, expectedText);

            return this;
        }

        public DiaryBookingsPage ValidatePersonAbsence_PlannedStartTime(string expectedText)
        {
            WaitForElement(PlannedStartTime_Field);
            ValidateElementDisabled(PlannedStartTime_Field);
            ValidateElementValue(PlannedStartTime_Field, expectedText);


            return this;
        }

        public DiaryBookingsPage ValidatePersonAbsence_PlannedEndDate(string expectedText)
        {
            WaitForElement(PlannedEndDate_Field);
            ValidateElementDisabled(PlannedEndDate_Field);
            ValidateElementValue(PlannedEndDate_Field, expectedText);

            return this;
        }

        public DiaryBookingsPage ValidatePersonAbsence_PlannedEndTime(string expectedText)
        {
            WaitForElement(PlannedEndTime_Field);
            ValidateElementDisabled(PlannedEndTime_Field);
            ValidateElementValue(PlannedEndTime_Field, expectedText);

            return this;
        }

        public DiaryBookingsPage ValidatePersonAbsence_ResponsibleTeam(string expectedText)
        {
            WaitForElement(ResponsibleTeam_Field);
            ValidateElementText(ResponsibleTeam_Field,expectedText);

            return this;
        }

        public DiaryBookingsPage ValidatePersonAbsence_ProviderField(string expectedText)
        {
            WaitForElement(LocationProvider_Field);
            ValidateElementText(LocationProvider_Field, expectedText);


            return this;
        }
    }
}
