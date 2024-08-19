using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class HealthSetupPage: CommonMethods
    {
        public HealthSetupPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By pageHeader = By.XPath("//header[@id='CWHeader']/h1");

       
        readonly By CommunityClinicTeams = By.XPath("//h3[text()='Community/Clinic Teams']");
        readonly By HospitalWards = By.XPath("//h3[text()='Hospital Wards']");
        readonly By RTTPathwaysSetup = By.XPath("//h3[text()='RTT Pathways Setup']");


        public HealthSetupPage WaitForHealthSetupPageToLoad()
        {
            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(pageHeader);

            if (driver.FindElement(pageHeader).Text != "Health Setup\r\nWHICH FEATURE WOULD YOU LIKE TO WORK WITH?")
                throw new Exception("Page title do not equals: \"Health Setup\r\nWHICH FEATURE WOULD YOU LIKE TO WORK WITH?\" ");

            return this;
        }

        public HealthSetupPage ClickCommunityClinicTeamsButton()
        {
            WaitForElementToBeClickable(CommunityClinicTeams);
            Click(CommunityClinicTeams);

            return this;
        }

        public HealthSetupPage ClickHospitalWardsButton()
        {
            WaitForElementToBeClickable(HospitalWards);
            Click(HospitalWards);

            return this;
        }

        public HealthSetupPage ClickRTTPathwaysSetupButton()
        {
            WaitForElementToBeClickable(RTTPathwaysSetup);
            MoveToElementInPage(RTTPathwaysSetup);
            Click(RTTPathwaysSetup);

            return this;
        }

    }
}
