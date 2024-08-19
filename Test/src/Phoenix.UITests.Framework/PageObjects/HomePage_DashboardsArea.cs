using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.UITests.Framework.PageObjects
{
    public class HomePage_DashboardsArea : CommonMethods
    {
        public HomePage_DashboardsArea(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By SystemDashboardRecordIFrame = By.XPath("//iframe[contains(@id,'CWFrame')][contains(@src,'dashboardpage.aspx')]");
        readonly By widgetIframe = By.XPath("//iframe[@onload='CW.DashboardControl.ResizeIFrame(this)']");

        readonly By refreshButton = By.Id("btnRefresh");
        
        
        
        readonly By automationTestingSystemDashboard_NewRecordButton = By.Id("TI_NewRecordButton");
        By automationTestingSystemDashboard_record(string RecordID) => By.XPath("//*[@id='" + RecordID + "_Primary']");

        //



        public HomePage_DashboardsArea WaitForHomePage_DashboardsAreaToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(SystemDashboardRecordIFrame);
            SwitchToIframe(SystemDashboardRecordIFrame);

            WaitForElement(refreshButton);

            return this;
        }

        public HomePage_DashboardsArea WaitForHomePage_AutomationTestingSystemDashboardToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(SystemDashboardRecordIFrame);
            SwitchToIframe(SystemDashboardRecordIFrame);

            WaitForElement(widgetIframe);
            SwitchToIframe(widgetIframe);

            WaitForElement(automationTestingSystemDashboard_NewRecordButton);

            return this;
        }

        public HomePage_DashboardsArea ValidateAutomationTestingSystemDashboardRecordPresent(string RecordID)
        {
            WaitForElement(automationTestingSystemDashboard_record(RecordID));

            return this;
        }

        public HomePage_DashboardsArea ValidateAutomationTestingSystemDashboardRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(automationTestingSystemDashboard_record(RecordID), 7);

            return this;
        }
    }
    
}
