using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonAbsencesRecordPage : CommonMethods
    {
        public PersonAbsencesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personAbsenceIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonabsence&')]");


        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Absences']");
        readonly By BackButton = By.XPath("//*[@id='BackButton']");


        

        readonly By PlannedStartDate_Field = By.Id("CWField_plannedstartdatetime");
        readonly By PlannedEndDate_Field = By.Id("CWField_plannedenddateandtime");
        readonly By DurationDays_Field = By.Id("CWField_durationdays");
        readonly By DurationHours_Field = By.Id("CWField_durationhours");



        public PersonAbsencesRecordPage WaitForPersonAbsencesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personAbsenceIFrame);
            SwitchToIframe(personAbsenceIFrame);



            WaitForElementVisible(BackButton);

            return this;
        }

        public PersonAbsencesRecordPage ValidatePersonAbsence_PlannedStartDate(string expectedText)
        {
            WaitForElement(PlannedStartDate_Field);
            string startdate = GetElementValue(PlannedStartDate_Field);
            Assert.AreEqual(expectedText, startdate);

            return this;
        }

        public PersonAbsencesRecordPage ValidatePersonAbsence_PlannedEndDate(string expectedText)
        {
            WaitForElement(PlannedEndDate_Field);
            string enddate = GetElementValue(PlannedEndDate_Field);
            Assert.AreEqual(expectedText, enddate);

            return this;
        }

        public PersonAbsencesRecordPage ValidatePersonAbsence_DurationInDays(string expectedText)
        {
            WaitForElement(DurationDays_Field);
            ValidateElementDisabled(DurationDays_Field);
            string durationdays = GetElementValue(DurationDays_Field);
            Assert.AreEqual(expectedText, durationdays);

            return this;
        }

        public PersonAbsencesRecordPage ValidatePersonAbsence_DurationInHours(string expectedText)
        {
            WaitForElement(DurationHours_Field);
            ValidateElementDisabled(DurationHours_Field);
            string durationhours = GetElementValue(DurationHours_Field);
            Assert.AreEqual(expectedText, durationhours);

            return this;
        }



    }
}
