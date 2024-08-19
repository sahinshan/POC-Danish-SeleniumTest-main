using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AvailabilitySaveChangesDialogPopup : CommonMethods
    {
        public AvailabilitySaveChangesDialogPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By problematicDiv = By.XPath("//mcc-dialog/div");

        readonly By dialogHeader = By.XPath("//mcc-dialog//h2");
        readonly By dialogMessage = By.XPath("//mcc-dialog//div[@class='mcc-dialog__body']/div");
        
        readonly By closeButton = By.Id("SA-dialog-dismiss");
        readonly By reloadButton = By.XPath("//mcc-dialog//button[text() = 'Reload']");
        readonly By saveAndReloadButton = By.XPath("//mcc-dialog//button[text() = 'Save & Reload']");
        readonly By OKButton = By.XPath("//mcc-dialog//button[text() = 'OK']");

        public AvailabilitySaveChangesDialogPopup WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(problematicDiv);
            SetAttributeValue(problematicDiv, "class", "mcc-dialog");

            WaitForElementVisible(dialogHeader);
            WaitForElement(dialogMessage);
            WaitForElementVisible(closeButton);

            return this;
        }

        public AvailabilitySaveChangesDialogPopup ClickOnCloseButton()
        {
            WaitForElementToBeClickable(closeButton);
            Click(closeButton);

            return this;
        }

        public AvailabilitySaveChangesDialogPopup ClickOnReloadButton()
        {
            WaitForElementToBeClickable(reloadButton);
            Click(reloadButton);

            return this;
        }

        public AvailabilitySaveChangesDialogPopup ClickOnSaveAndReloadButton()
        {
            WaitForElementToBeClickable(saveAndReloadButton);
            Click(saveAndReloadButton);

            return this;
        }

        public AvailabilitySaveChangesDialogPopup ClickOnOKButton()
        {
            WaitForElementToBeClickable(OKButton);
            Click(OKButton);

            return this;
        }

        public AvailabilitySaveChangesDialogPopup ValidateDialogText(string ExpectedText)
        {
            ValidateElementText(dialogMessage, ExpectedText);

            return this;
        }

        public AvailabilitySaveChangesDialogPopup ValidateReloadButton()
        {
            WaitForElement(reloadButton);
            bool isDisplayed = GetElementVisibility(reloadButton);
            Assert.IsTrue(isDisplayed);

            return this;
        }

        public AvailabilitySaveChangesDialogPopup ValidateSaveAndReloadButton()
        {
            WaitForElement(saveAndReloadButton);
            bool isDisplayed = GetElementVisibility(saveAndReloadButton);
            Assert.IsTrue(isDisplayed);

            return this;
        }

        public AvailabilitySaveChangesDialogPopup ValidateCloseButton()
        {
            WaitForElement(closeButton);
            bool isDisplayed = GetElementVisibility(closeButton);
            Assert.IsTrue(isDisplayed);

            return this;
        }

        public AvailabilitySaveChangesDialogPopup ValidateOKButton()
        {
            WaitForElement(OKButton);
            bool isDisplayed = GetElementVisibility(OKButton);
            Assert.IsTrue(isDisplayed);

            return this;
        }
    }
}
