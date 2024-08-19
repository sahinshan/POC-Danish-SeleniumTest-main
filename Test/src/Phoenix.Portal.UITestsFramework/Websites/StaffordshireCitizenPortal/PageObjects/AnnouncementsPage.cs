using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class AnnouncementsPage : CommonMethods
    {
        public AnnouncementsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

       
        readonly By pageHeader = By.XPath("//*[@id='CWContent']/mosaic-page-header/div/div/div/div/h1[text()='Announcements']");
        
        By announcementHeader(string ExpectedText) => By.XPath("//mosaic-card-body/div/h2[text()='" + ExpectedText + "']");
        By announcementPublishedDate(string ExpectedText) => By.XPath("//mosaic-card-body/div/span/small[text()='" + ExpectedText + "']");
        By announcementContentLine(string ExpectedText) => By.XPath("//mosaic-card-body/div/div/p[text()='" + ExpectedText + "']");


        
        


        public AnnouncementsPage WaitForAnnouncementsPageToLoad()
        {
            WaitForElement(pageHeader);


            return this;
        }

        public AnnouncementsPage ValidateAnnouncementHeader(string ExpectedText)
        {
            WaitForElement(announcementHeader(ExpectedText));

            return this;
        }

        public AnnouncementsPage ValidateAnnouncementPublishedDate(string ExpectedText)
        {
            WaitForElement(announcementPublishedDate(ExpectedText));

            return this;
        }

        public AnnouncementsPage ValidateAnnouncementContentLine(string ExpectedText)
        {
            WaitForElement(announcementContentLine(ExpectedText));

            return this;
        }


    }
    
}
