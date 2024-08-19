using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.Website17.PageObjects
{
    public class MainMenu : CommonMethods
    {
        public MainMenu(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By HomePage_Link = By.XPath("//mosaic-navigation/mosaic-button/a[@href='home_page']");
        readonly By MemberHome_Link = By.XPath("//mosaic-navigation/mosaic-button/a[@href='member-home']");
        readonly By Announcements_Link = By.XPath("//mosaic-navigation/mosaic-button/a[@href='announcements']");
        readonly By ContactUs_Link = By.XPath("//mosaic-navigation/mosaic-button/a[@href='contact-us']");



        By websiteLogo(string LogoName) => By.XPath("//mosaic-logo/a/img[@src='resources/" + LogoName + "']");


        public MainMenu WaitForMainMenuToLoad()
        {
            WaitForElement(Announcements_Link);
           
            return this;
        }

        public MainMenu ValidateWebsiteLogoPresent(string LogoName)
        {
            WaitForElement(websiteLogo(LogoName));

            return this;
        }



        public MainMenu ValidateHomeLinkVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(HomePage_Link);
            else
                WaitForElementNotVisible(HomePage_Link, 7);

            return this;
        }
        public MainMenu ValidateMemberHomeLinkVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MemberHome_Link);
            else
                WaitForElementNotVisible(MemberHome_Link, 7);

            return this;
        }
        public MainMenu ValidateAnnouncementsLinkVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Announcements_Link);
            else
                WaitForElementNotVisible(Announcements_Link, 7);

            return this;
        }
        public MainMenu ValidateContactUsVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ContactUs_Link);
            else
                WaitForElementNotVisible(ContactUs_Link, 7);

            return this;
        }


        public MainMenu ClickHomeLink()
        {
            Click(HomePage_Link);

            return this;
        }
        public MainMenu ClickMemberHomeLink()
        {
            Click(MemberHome_Link);

            return this;
        }
        public MainMenu ClickAnnouncementsLink()
        {
            Click(Announcements_Link);

            return this;
        }
        public MainMenu ClickContactUsLink()
        {
            Click(ContactUs_Link);

            return this;
        }


    }
    
}
