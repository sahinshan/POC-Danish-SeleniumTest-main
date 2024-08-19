using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Security
{
    public class TeamsPage : CommonMethods
    {
        public TeamsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");

        readonly By contentSection = By.Id("CWContent");
        readonly By gridSection = By.Id("CWGridWrapper");

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        readonly By noRecorrdsMainMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noRecordsSubMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By TeamRowCheckBox(string TeamsID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + TeamsID + "']/td[1]/input");


        public TeamsPage WaitForTeamsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(contentSection);
            WaitForElement(gridSection);
           
            return this;
        }

        public TeamsPage SearchTeamsRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);          

            return this;
        }

        public TeamsPage ValidateTeamsRecord(string TeamsId, string ExpectedValue)
        {
            WaitForElementToBeClickable(TeamRowCheckBox(TeamsId));
            MoveToElementInPage(TeamRowCheckBox(TeamsId));
            ValidateElementValue(TeamRowCheckBox(TeamsId), ExpectedValue);

            return this;
        }

        public TeamsPage ValidateNoRecordsMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(noRecorrdsMainMessage);
                WaitForElementVisible(noRecordsSubMessage);
            }
            else
            {
                WaitForElementNotVisible(noRecorrdsMainMessage, 3);
                WaitForElementNotVisible(noRecordsSubMessage, 3);
            }

            return this;
        }

    }
}
