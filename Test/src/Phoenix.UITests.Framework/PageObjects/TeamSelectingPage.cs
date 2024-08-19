using OpenQA.Selenium;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class TeamSelectingPage : CommonMethods
    {
        public TeamSelectingPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By pageTitle = By.XPath("//*[@id='TeamSubLabel'][text()='Confirm your team selection for the session']");
        readonly By team_Picklist = By.Id("CWTeam");
        readonly By continueButton = By.Id("CWContinueButton");
        


        public TeamSelectingPage WaitForTeamSelectingPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(pageTitle);
            WaitForElement(team_Picklist);
            WaitForElement(continueButton);

            return this;
        }

        public TeamSelectingPage SelectDefaultTeam(string TeamName)
        {
            SelectPicklistElementByText(team_Picklist, TeamName);

            return this;
        }

        public TeamSelectingPage ValidateTeamPicklistContainsElement(string TeamName)
        {
            ValidatePicklistContainsElementByText(team_Picklist, TeamName);

            return this;
        }
        public TeamSelectingPage ValidateTeamPicklistDoesNotContainsElement(string TeamName)
        {
            ValidatePicklistDoesNotContainsElementByText(team_Picklist, TeamName);

            return this;
        }

        public TeamSelectingPage ClickContinueButton()
        {
            Click(continueButton);

            return this;
        }

    }
}
