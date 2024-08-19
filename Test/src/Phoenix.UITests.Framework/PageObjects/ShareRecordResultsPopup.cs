using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ShareRecordResultsPopup : CommonMethods
    {
        public ShareRecordResultsPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By ShareWithResultsMainArea = By.XPath("//div[@id='CWShareWithResult']");

        readonly By noResultsMessage = By.XPath("//ul[@id='CWShareWithResultHolder']/li/ul/li[text()='No results found. Please change your search criteria.']");

        readonly By closeButton = By.XPath("//div[@id='CWShareWithResult']/div/a[contains(@onclick, 'CW.RecordShare.CloseSearchDialog()')]");

        #region User Results

        By userFullName(string UserID, string FullName) => By.XPath("//li[@id='" + UserID + "']/ul/li/h4[text()='" + FullName + "']");
        By userTeam(string UserID, string TeamName) => By.XPath("//li[@id='" + UserID + "']/ul/li/p[@title='" + TeamName + "']");
        By userName(string UserID, string UserName) => By.XPath("//li[@id='" + UserID + "']/ul/li/p[@title='" + UserName + "']");

        By addUserButton(string UserID) => By.XPath("//li[@id='" + UserID + "']/ul/li/a[@title='Click to add']");
        By userAlreadyAddedButton(string UserID) => By.XPath("//li[@id='" + UserID + "']/ul/li/span[@title='Object yet added']");

        #endregion

        #region Team Results

        By teamName(string TeamID, string FullName) => By.XPath("//li[@id='" + TeamID + "']/ul/li/h4[text()='" + FullName + "']");
        By addTeamButton(string TeamID) => By.XPath("//li[@id='" + TeamID + "']/ul/li/a[contains(@onclick, 'CW.RecordShare.Add')]");
        By teamAlreadyAddedButton(string TeamID) => By.XPath("//li[@id='" + TeamID + "']/ul/li/span[@title='Object yet added']");
        
        #endregion



        public ShareRecordResultsPopup WaitForShareRecordResultsPopupToLoad()
        {
            Wait.Until(c => c.FindElement(ShareWithResultsMainArea));
            
            return this;
        }

        public ShareRecordResultsPopup ValidateNoResultsMessage(bool ExpectNoResultsMessage)
        {
            bool noResultsMessageVisible = GetElementVisibility(noResultsMessage);
            Assert.AreEqual(ExpectNoResultsMessage, noResultsMessageVisible);

            return this;
        }

        public ShareRecordResultsPopup ValidateUserRecordPresent(string UserID, string UserFullName, string UserTeamName, string UserName, bool UserAlreadyAdded)
        {
            Wait.Until(c => c.FindElement(userFullName(UserID, UserFullName)));
            Wait.Until(c => c.FindElement(userTeam(UserID, UserTeamName)));
            Wait.Until(c => c.FindElement(userName(UserID, UserName)));

            if (UserAlreadyAdded)
            {
                Wait.Until(c => c.FindElement(userAlreadyAddedButton(UserID)));
                bool addButtonVisible = GetElementVisibility(addUserButton(UserID));
                Assert.IsFalse(addButtonVisible);
            }
            else
            {
                Wait.Until(c => c.FindElement(addUserButton(UserID)));
                bool alreadyAddedButtonVisible = GetElementVisibility(userAlreadyAddedButton(UserID));
                Assert.IsFalse(alreadyAddedButtonVisible);
            }

            return this;
        }

        public ShareRecordResultsPopup ValidateTeamRecordPresent(string TeamID, string TeamFullName, bool TeamAlreadyAdded)
        {
            Wait.Until(c => c.FindElement(teamName(TeamID, TeamFullName)));

            if (TeamAlreadyAdded)
            {
                Wait.Until(c => c.FindElement(teamAlreadyAddedButton(TeamID)));
                bool addButtonVisible = GetElementVisibility(addTeamButton(TeamID));
                Assert.IsFalse(addButtonVisible);
            }
            else
            {
                Wait.Until(c => c.FindElement(addTeamButton(TeamID)));
                bool alreadyAddedButtonVisible = GetElementVisibility(teamAlreadyAddedButton(TeamID));
                Assert.IsFalse(alreadyAddedButtonVisible);
            }

            return this;
        }


        public ShareRecordResultsPopup CloseResultsWindow()
        {
            driver.FindElement(closeButton).Click();

            return this;
        }

        public ShareRecordResultsPopup TapAddUserButton(string UserID)
        {
            driver.FindElement(addUserButton(UserID)).Click();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ShareRecordResultsPopup TapAddTeamButton(string TeamID)
        {
            driver.FindElement(addTeamButton(TeamID)).Click();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}
