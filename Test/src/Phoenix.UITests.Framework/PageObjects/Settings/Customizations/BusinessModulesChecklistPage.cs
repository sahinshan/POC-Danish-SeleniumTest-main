using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
    public class BusinessModulesChecklistPage : CommonMethods
    {
        public BusinessModulesChecklistPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        By moduleCheckbox(string BusinessModuleId) => By.XPath("//input[@id='" + BusinessModuleId + "']");

        By moduleLabel(string BusinessModuleId) => By.XPath("//label[@for='" + BusinessModuleId + "']");



        public BusinessModulesChecklistPage WaitForBusinessModulesChecklistPageToLoad()
        {
            this.SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(pageHeader);

            this.ValidateElementText(pageHeader, "Business Modules");

            return this;
        }

        public BusinessModulesChecklistPage ValidateModuleCheckBox(string BusinessModuleId, bool ExpectChecked)
        {
            MoveToElementInPage(moduleCheckbox(BusinessModuleId));
            if (ExpectChecked)
            {
                ValidateElementChecked(moduleCheckbox(BusinessModuleId));
            }
            else
            {
                ValidateElementNotChecked(moduleCheckbox(BusinessModuleId));
            }

            return this;
        }

        public BusinessModulesChecklistPage ValidateModuleLabel(string BusinessModuleId, string ExpectText)
        {
            MoveToElementInPage(moduleLabel(BusinessModuleId));
            ValidateElementText(moduleLabel(BusinessModuleId), ExpectText);

            return this;
        }


    }
}
