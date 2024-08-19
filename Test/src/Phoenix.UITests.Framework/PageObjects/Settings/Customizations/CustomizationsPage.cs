using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CustomizationsPage: CommonMethods
    {
        public CustomizationsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By pageHeader = By.XPath("//header[@id='CWHeader']/h1");

       
        readonly By BusinessObjectsButton = By.XPath("//h3[text()='Business Objects']");
        readonly By ModulesButton = By.XPath("//h3[text()='Modules']");
        readonly By OptionSets = By.XPath("//h3[text()='Option Sets']");
        readonly By LocalizedStrings = By.XPath("//h3[text()='Localized Strings']");

        public CustomizationsPage WaitForCustomizationsPageToLoad()
        {
            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(pageHeader);

            if (driver.FindElement(pageHeader).Text != "Customizations\r\nWHICH FEATURE WOULD YOU LIKE TO WORK WITH?")
                throw new Exception("Page title do not equals: \"Customizations\r\nWHICH FEATURE WOULD YOU LIKE TO WORK WITH?\" ");

            return this;
        }

        public CustomizationsPage ClickBusinessObjectsButton()
        {
            WaitForElementToBeClickable(BusinessObjectsButton);
            Click(BusinessObjectsButton);

            return this;
        }

        public CustomizationsPage ClickModulesButton()
        {
            WaitForElementToBeClickable(ModulesButton);
            Click(ModulesButton);
            
            return this;
        }

        public CustomizationsPage ClickOptionSetsButton()
        {
            WaitForElementToBeClickable(OptionSets);
            Click(OptionSets);

            return this;
        }

        public CustomizationsPage ClickLocalizedStringsButton()
        {
            WaitForElementToBeClickable(LocalizedStrings);
            Click(LocalizedStrings);

            return this;
        }

    }
}
