using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ChangeDefaultTeamPopup : CommonMethods
    {
        public ChangeDefaultTeamPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_ChangeDefaultTeamDialog");

        readonly By popupHeader = By.XPath("//*[@id='CWHeaderText'][text()='Change default team']");

        readonly By TeamSubLabel = By.XPath("//*[@id='TeamSubLabel'][text()='Select a team']");
        readonly By TeamLookupButton = By.XPath("//*[@id='TeamButton']");
        readonly By SaveButton = By.XPath("//*[@id='SaveButton']");



        public ChangeDefaultTeamPopup WaitForChangeDefaultTeamPopupToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);

            WaitForElement(TeamSubLabel);
            WaitForElement(TeamLookupButton);
            WaitForElement(SaveButton);

            return this;
        }

        public ChangeDefaultTeamPopup WaitForChangeDefaultTeamPopupToReLoad()
        {
            WaitForElement(popupHeader);

            WaitForElement(TeamSubLabel);
            WaitForElement(TeamLookupButton);
            WaitForElement(SaveButton);

            return this;
        }


        public ChangeDefaultTeamPopup ClickTeamLookupButton()
        {
            Click(TeamLookupButton);

            return this;
        }
        public ChangeDefaultTeamPopup ClickSaveButton()
        {
            Click(SaveButton);

            return this;
        }

    }
}
