using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Security
{
    public class ScheduleAvailabilityPopup : CommonMethods
    {
        public ScheduleAvailabilityPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By problematicDiv = By.XPath("//*[@id='modal-dialog']/div");
        readonly By popupWindowHeader = By.XPath("//*[@class='mcc-dialog__header']/h2");
        readonly By popupBody = By.XPath("//*[@class='mcc-dialog__body']");
        readonly By popupFooter = By.XPath("//*[@class='mcc-dialog__footer']");

        readonly By popupHeader = By.XPath("//h1");

        readonly By topLabel = By.XPath("//*[@id='modal-dialog']//p");

        readonly By RemoveTimeSlot = By.XPath("//div/button[text()='Remove time slot']");

        By TransportationButton(string TransportName) => By.XPath("//*[@id='modal-dialog']//a[text()='" + TransportName + "']");

        By SelectType(string TypeName) => By.XPath("//*[@class='mcc-list-group']//button[text()='"+ TypeName + "']");

        readonly By CloseButton = By.XPath("//*[@id='SA-dialog-dismiss']");

        public ScheduleAvailabilityPopup WaitForScheduleAvailabilityPopupToLoad()
        {
            WaitForElement(problematicDiv);
            SetAttributeValue(problematicDiv, "class", "mcc-dialog");

            WaitForElementVisible(popupHeader);
            WaitForElementVisible(topLabel);

            WaitForElementVisible(CloseButton);

            return this;
        }

        public ScheduleAvailabilityPopup WaitForScheduleAvailabilityPopupWindowToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(popupWindowHeader);
            WaitForElement(popupBody);
            WaitForElement(popupFooter);
            WaitForElement(CloseButton);

            return this;
        }


        public ScheduleAvailabilityPopup SelectSheduleAvailabilityType(string AvailabilityText)
        {
            WaitForElementToBeClickable(TransportationButton(AvailabilityText));
            MoveToElementInPage(TransportationButton(AvailabilityText));
            Click(TransportationButton(AvailabilityText));

            return this;
        }


        public ScheduleAvailabilityPopup ClickCloseButton()
        {
            WaitForElementToBeClickable(CloseButton);
            Click(CloseButton);

            return this;
        }

        public ScheduleAvailabilityPopup ValidateTransportationModeVisble(string TransportName)
        {
            WaitForElementVisible(TransportationButton(TransportName));

            return this;
        }

        public ScheduleAvailabilityPopup SelectScheduleAvailabilityTypeText(string AvailabilityText)
        {
            ClickByJavascript(SelectType(AvailabilityText));

            return this;
        }

        public ScheduleAvailabilityPopup ClickRemoveTimeSlotButton()
        {
            MoveToElementInPage(RemoveTimeSlot);
            ClickByJavascript(RemoveTimeSlot);

            return this;
        }

    }
}
