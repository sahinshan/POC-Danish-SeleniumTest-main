using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class UserDashboardsPage : CommonMethods
    {
        public UserDashboardsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By UserDashboardsPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='User Dashboards']");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By UserDashboardRow(string UserDashboardID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + UserDashboardID + "']/td[2]");
        By UserDashboardRowCheckBox(string UserDashboardID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + UserDashboardID + "']/td[1]/input");







        public UserDashboardsPage WaitForUserDashboardsPageToLoad()
        {
            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(UserDashboardsPageHeader);
            WaitForElement(viewsPicklist);

            return this;
        }

        public UserDashboardsPage SearchUserDashboardRecord(string SearchQuery, string UserDashboardID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(UserDashboardRow(UserDashboardID));

            return this;
        }

        public UserDashboardsPage SearchUserDashboardRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public UserDashboardsPage OpenUserDashboardRecord(string UserDashboardId)
        {
            WaitForElement(UserDashboardRow(UserDashboardId));
            Click(UserDashboardRow(UserDashboardId));

            return this;
        }

        public UserDashboardsPage SelectUserDashboardRecord(string UserDashboardId)
        {
            WaitForElement(UserDashboardRowCheckBox(UserDashboardId));
            Click(UserDashboardRowCheckBox(UserDashboardId));

            return this;
        }

        
    }
}
