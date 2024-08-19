using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AvailabilityDinamicDialogPopup : CommonMethods
    {
        public AvailabilityDinamicDialogPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By problematicDiv = By.XPath("//*[@id='modal-dialog']/div");

        readonly By popupWindowHeader = By.XPath("//*[@class='mcc-dialog__header']/h2");
        readonly By popupBody = By.XPath("//*[@class='mcc-dialog__body']");
        readonly By popupFooter = By.XPath("//*[@class='mcc-dialog__footer']");

        readonly By dialogHeader = By.XPath("//*[@id='modal-dialog']//h1");
        readonly By dialogMessage = By.XPath("//*[@id='modal-dialog']//p");
        readonly By dialogMessage_Standard = By.XPath("//*[@class='mcc-list-group']//button[text()='Salaried/Contracted']");
        readonly By dialogMessage_OverTime = By.XPath("//*[@class='mcc-list-group']//button[text()='Hourly/Overtime']");
        readonly By dialogMessage_Regular = By.XPath("//*[@class='mcc-list-group']//button[text()='Regular']");

        By dialogMessageByText(string Name) => By.XPath("//*[@class='mcc-list-group']//button[text()='" + Name + "']");

        readonly By closeButton = By.Id("SA-dialog-dismiss");

        readonly By RemoveTimeSlotButton = By.XPath("//div/button[text()='Remove time slot']");

        public AvailabilityDinamicDialogPopup WaitForAvailabilityDinamicDialogPopupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(popupWindowHeader);
            WaitForElement(popupBody);
            WaitForElement(popupFooter);
            WaitForElement(closeButton);

            return this;
        }

        public AvailabilityDinamicDialogPopup ClickOnCloseButton()
        {
            WaitForElementToBeClickable(closeButton);
            Click(closeButton);

            return this;
        }

        public AvailabilityDinamicDialogPopup ValidateDialogText(string ExpectedText)
        {
            ValidateElementText(dialogMessage, ExpectedText);

            return this;
        }

        public AvailabilityDinamicDialogPopup ValidateStandardOption()
        {
            WaitForElement(dialogMessage_Standard);
            MoveToElementInPage(dialogMessage);
            bool isDisplayed = GetElementVisibility(dialogMessage_Standard);
            Assert.IsTrue(isDisplayed);

            return this;
        }

        public AvailabilityDinamicDialogPopup ValidateOverTimeOption()
        {
            WaitForElement(dialogMessage_OverTime);
            MoveToElementInPage(dialogMessage_OverTime);
            bool isDisplayed = GetElementVisibility(dialogMessage_OverTime);
            Assert.IsTrue(isDisplayed);

            return this;
        }

        public AvailabilityDinamicDialogPopup ValidateRegularOption()
        {
            WaitForElement(dialogMessage_Regular);
            MoveToElementInPage(dialogMessage_Regular);
            bool isDisplayed = GetElementVisibility(dialogMessage_Regular);
            Assert.IsTrue(isDisplayed);

            return this;
        }

        public AvailabilityDinamicDialogPopup ClickAvailabilityButton(string TextToSelect)
        {
            MoveToElementInPage(dialogMessageByText(TextToSelect));
            ClickByJavascript(dialogMessageByText(TextToSelect));

            return this;
        }

        public AvailabilityDinamicDialogPopup ValidateAvailabilityOptionText(string ExpectedText)
        {
            MoveToElementInPage(dialogMessageByText(ExpectedText));
            string textValue = GetElementTextByJavascript(dialogMessageByText(ExpectedText));
            Assert.AreEqual(textValue, ExpectedText);

            return this;
        }

        public AvailabilityDinamicDialogPopup ClickAvailabilityButton()
        {
            MoveToElementInPage(dialogMessage_Standard);
            ClickByJavascript(dialogMessage_Standard);

            return this;
        }

        public AvailabilityDinamicDialogPopup ClickAvailabilityRegularButton()
        {
            MoveToElementInPage(dialogMessage_Regular);
            ClickByJavascript(dialogMessage_Regular);

            return this;
        }

        public AvailabilityDinamicDialogPopup ClickAvailabilityOvertimeButton()
        {
            MoveToElementInPage(dialogMessage_OverTime);
            ClickByJavascript(dialogMessage_OverTime);

            return this;
        }

        public AvailabilityDinamicDialogPopup ClickRemoveTimeSlotButton()
        {
            MoveToElementInPage(RemoveTimeSlotButton);
            ClickByJavascript(RemoveTimeSlotButton);

            return this;
        }

    }
}
