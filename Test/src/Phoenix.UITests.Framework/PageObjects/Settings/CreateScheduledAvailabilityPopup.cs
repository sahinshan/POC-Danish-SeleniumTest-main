using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings
{
    public class CreateScheduledAvailabilityPopup : CommonMethods
    {
        public CreateScheduledAvailabilityPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By problematicDiv = By.XPath("//mcc-dialog/div");
        readonly By popupHeader = By.XPath("//mcc-dialog//h2");
        readonly By topLabel = By.XPath("//mcc-dialog//p");
        
        readonly By overtimeAvailabilityButton = By.XPath("//mcc-dialog//button[text() = 'OverTime']");
        By availabilityType(string expectedText) => By.XPath("//mcc-dialog//button[text() = '" + expectedText + "']");


        readonly By RemoveTimeSlotButton = By.XPath("//mcc-dialog//button[text()='Remove time slot']");
        readonly By CloseButton = By.XPath("//*[@id='SA-dialog-dismiss']");



        public CreateScheduledAvailabilityPopup WaitForCreateScheduledAvailabilityPopupToLoad(bool CreateMode)
        {
            SwitchToDefaultFrame();

            WaitForElement(problematicDiv);
            SetAttributeValue(problematicDiv, "class", "mcc-dialog");

            WaitForElementVisible(popupHeader);
            WaitForElementVisible(topLabel);

            if (CreateMode)
                ValidateElementTextContainsText(topLabel, "Create ");
            else
                ValidateElementTextContainsText(topLabel, "Edit ");

            WaitForElementVisible(CloseButton);

            return this;
        }


        public CreateScheduledAvailabilityPopup ClickOvertimeButton()
        {
            WaitForElement(overtimeAvailabilityButton);
            Click(overtimeAvailabilityButton);

            return this;
        }

        public CreateScheduledAvailabilityPopup ClickRemoveTimeSlotButton()
        {
            WaitForElement(RemoveTimeSlotButton);
            MoveToElementInPage(RemoveTimeSlotButton);
            WaitForElementToBeClickable(RemoveTimeSlotButton);
            Click(RemoveTimeSlotButton);

            return this;
        }
        public CreateScheduledAvailabilityPopup ClickCloseButton()
        {
            ScrollToElement(CloseButton);
            Click(CloseButton);

            return this;
        }

        public CreateScheduledAvailabilityPopup ValidateAvailabilityTypeDisplayed(string expectedText)
        {
            bool isAvailabilityTypeDisplayed = GetElementVisibility(availabilityType(expectedText));
            Assert.IsTrue(isAvailabilityTypeDisplayed);

            return this;
        }

        public CreateScheduledAvailabilityPopup ClickAvailabilityButton(string name)
        {
            WaitForElementToBeClickable(availabilityType(name));
            MoveToElementInPage(availabilityType(name));
            Click(availabilityType(name));

            return this;
        }

    }
}
