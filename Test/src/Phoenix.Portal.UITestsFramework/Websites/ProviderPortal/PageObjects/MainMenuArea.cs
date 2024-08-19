using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.ProviderPortal.PageObjects
{
    public class MainMenu : CommonMethods
    {
        public MainMenu(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        By styleSheetFile(string StileSheetFileName) => By.XPath("//link[@href='/portalwebsite/resources/" + StileSheetFileName + "']");

        readonly By HomePage_Link = By.XPath("//mosaic-navigation/mosaic-button/a[@href='home']");
        readonly By MemberHome_Link = By.XPath("//mosaic-navigation/mosaic-button/a[@href='member-home']");
        readonly By FrequentlyAskedQuestions_Link = By.XPath("//mosaic-navigation/mosaic-button/a[@href='faqs']");
        readonly By Announcements_Link = By.XPath("//mosaic-navigation/mosaic-button/a[@href='announcements']");
        readonly By ContactUs_Link = By.XPath("//mosaic-navigation/mosaic-button/a[@href='contact-us']");
        readonly By FinancialAssessment_Link = By.XPath("//mosaic-navigation/mosaic-button/a[@href='financial-assessment']");


        readonly By AboutMeUserNameButton = By.Id("CWAboutmeName");
        readonly By AboutMeChangePasswordButton = By.Id("CWAboutmeChangePassword");
        readonly By AboutMeEditDetailsButton = By.Id("CWAboutmeEditDetails");
        readonly By AboutMeDeactivateAccountButton = By.Id("CWAboutmeDeactivateAccount");
        readonly By AboutMeLogOutButton = By.Id("CWAboutmeLogOut");
        readonly By PersonFormsViewButton = By.XPath("//h4[text()='Person Forms']/parent::mosaic-grid-item/ancestor::mosaic-card-body/following-sibling::mosaic-card-footer/div/mosaic-button/a/div[text()='View']");



        By websiteLogo(string LogoName) => By.XPath("//mosaic-logo/a/img[@src='/resources/" + LogoName + "']");


        public MainMenu WaitForMainMenuToLoad()
        {
            WaitForElement(Announcements_Link);

            WaitForElement(AboutMeUserNameButton);

            return this;
        }

        public MainMenu WaitForAnonymousSitemapMainMenuToLoad()
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
        public MainMenu ValidateFrequentlyAskedQuestionsLinkVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(FrequentlyAskedQuestions_Link);
            else
                WaitForElementNotVisible(FrequentlyAskedQuestions_Link, 7);

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
        public MainMenu ValidateFinancialAssessmentVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(FinancialAssessment_Link);
            else
                WaitForElementNotVisible(FinancialAssessment_Link, 7);

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
        public MainMenu ClickFinancialAssessmentLink()
        {
            Click(FinancialAssessment_Link);

            return this;
        }
        public MainMenu ClickFrequentlyAskedQuestionsLink()
        {
            Click(FrequentlyAskedQuestions_Link);

            return this;
        }



        public MainMenu ClickAboutMeUserNameButton()
        {
            WaitForElementToBeClickable(AboutMeUserNameButton);

            Click(AboutMeUserNameButton);

            return this;
        }
        public MainMenu ClickChangePasswordButton()
        {
            WaitForElementToBeClickable(AboutMeChangePasswordButton);

            Click(AboutMeChangePasswordButton);

            return this;
        }
        public MainMenu ClickDeactivateAccountButton()
        {
            Click(AboutMeDeactivateAccountButton);

            return this;
        }
        public MainMenu ClickEditDetailsButton()
        {
            Click(AboutMeEditDetailsButton);

            return this;
        }

        public MainMenu ClickLogOutButton()
        {
            Click(AboutMeLogOutButton);

            return this;
        }

        public MainMenu ClickPersonFormsViewButton()
        {
            Click(PersonFormsViewButton);

            return this;
        }


        public MainMenu ValidateChangePasswordButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AboutMeChangePasswordButton);
            else
                WaitForElementNotVisible(AboutMeChangePasswordButton, 7);

            return this;
        }
        public MainMenu ValidateEditDetailsButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AboutMeEditDetailsButton);
            else
                WaitForElementNotVisible(AboutMeEditDetailsButton, 7);

            return this;
        }
        public MainMenu ValidateDeactivateAccounttButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AboutMeDeactivateAccountButton);
            else
                WaitForElementNotVisible(AboutMeDeactivateAccountButton, 7);

            return this;
        }
        public MainMenu ValidateLogOutButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AboutMeLogOutButton);
            else
                WaitForElementNotVisible(AboutMeLogOutButton, 7);

            return this;
        }

    }
    
}
