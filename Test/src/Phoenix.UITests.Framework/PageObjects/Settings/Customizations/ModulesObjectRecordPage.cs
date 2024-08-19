using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Customizations
{
   public class ModulesObjectRecordPage :CommonMethods
    {
        public ModulesObjectRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=businessmodule&')]");

        readonly By menuButton = By.XPath("//*[@id='CWNavGroup_grpRelated']/a");

        readonly By pprcInactiveLabel = By.XPath("//*[@id='CWLabelHolder_inactive']/label[text()='Inactive']");

        readonly By pprcInactiveLabel_YesOption = By.XPath("//*[@id='CWField_inactive_1']");
        readonly By pprcInactiveLabel_NoOption = By.XPath("//*[@id='CWField_inactive_0']");

        readonly By activateButton = By.Id("TI_ActivateButton");
        readonly By saveAndClose_Button = By.Id("TI_SaveAndCloseButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");


        public ModulesObjectRecordPage WaitForModulesObjectRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(menuButton);

            return this;
        }
        public ModulesObjectRecordPage ValidateInactiveFieldVisible()
        {

            WaitForElement(pprcInactiveLabel);
            WaitForElement(pprcInactiveLabel_YesOption);
            WaitForElement(pprcInactiveLabel_NoOption);


            return this;
        }

        public ModulesObjectRecordPage ValidateInactiveYesRadioButton(bool InactiveYesOption)
        {
            if (InactiveYesOption)
            {
                WaitForElement(pprcInactiveLabel_YesOption);
                ValidateElementNotDisabled(pprcInactiveLabel_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(activateButton);
                Click(activateButton);
                driver.SwitchTo().Alert().Accept();
                Click(saveAndClose_Button);

            }
            return this;

        }

        public ModulesObjectRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            return this;
        }

        public ModulesObjectRecordPage ValidateStatusInactive(bool state)
        {
            WaitForElement(pprcInactiveLabel);
            WaitForElement(pprcInactiveLabel_YesOption);
            WaitForElement(pprcInactiveLabel_NoOption);

            if (state)
            {
                ValidateElementChecked(pprcInactiveLabel_YesOption);
                ValidateElementNotChecked(pprcInactiveLabel_NoOption);
            }
            else
            {
                ValidateElementNotChecked(pprcInactiveLabel_YesOption);
                ValidateElementChecked(pprcInactiveLabel_NoOption);
            }
            return this;

        }




    }
}
