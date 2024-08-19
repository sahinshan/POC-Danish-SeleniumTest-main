
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// Person Record - Care Plans Tab
    /// </summary>
    public class PersonCarePlansSubPage : CommonMethods
    {
        public PersonCarePlansSubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWSubTabsPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        #region Navigation Area

        readonly By CarePlansLink = By.XPath("//*[@id='CWTab_138da786-169c-e911-a2c5-0050569231cf']/a");
        readonly By CPAReviewLink = By.XPath("//li/a[@title='CPA review']");
        readonly By AssessmentsLink = By.XPath("//*[@id='CWTab_6cc1c1a4-0cca-ec11-a351-0050569231cf']/a");
        readonly By RegularCareLink = By.XPath("//*[@id='CWTab_7096ab04-c865-ed11-a354-0050569231cf']/a");
        readonly By CareDiaryLink = By.XPath("//*[@id='CWTab_bff1198b-90ea-ed11-a359-0050569231cf']/a");

        #endregion



        public PersonCarePlansSubPage WaitForPersonCarePlansSubPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWSubTabsPanel_IFrame);
            SwitchToIframe(CWSubTabsPanel_IFrame);

            WaitForElement(CarePlansLink);
            WaitForElement(RegularCareLink);

            return this;
        }

        //public PersonCarePlansSubPage ClickDashboardLink()
        //{
        //    Click(DashboardsLink);

        //    return this;
        //}

        public PersonCarePlansSubPage ClickCarePlansLink()
        {
            Click(CarePlansLink);

            return this;
        }

        public PersonCarePlansSubPage ClickAssessmentsLink()
        {
            Click(AssessmentsLink);

            return this;
        }

        public PersonCarePlansSubPage ClickCPAReviewLink()
        {
            Click(CPAReviewLink);

            return this;
        }

        public PersonCarePlansSubPage ClickRegularCareLink()
        {
            WaitForElementToBeClickable(RegularCareLink);
            Click(RegularCareLink);

            return this;
        }

        public PersonCarePlansSubPage ClickCareDiaryLink()
        {
            WaitForElementToBeClickable(CareDiaryLink);
            Click(CareDiaryLink);

            return this;
        }

    }
}
