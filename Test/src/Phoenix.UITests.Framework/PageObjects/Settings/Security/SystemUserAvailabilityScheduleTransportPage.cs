using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Security
{
    public class SystemUserAvailabilityScheduleTransportPage : CommonMethods
    {
        public SystemUserAvailabilityScheduleTransportPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By problematicDiv = By.XPath("//mcc-dialog/div");

        readonly By popupHeader = By.XPath("//mcc-dialog//h2");
        readonly By topLabel = By.XPath("//mcc-dialog//p");

        readonly By CarButton = By.XPath("//mcc-dialog//span[text()='Car']");
        readonly By WalkingButton = By.XPath("//mcc-dialog//span[text()='Walking']");
        readonly By BicycleButton = By.XPath("//mcc-dialog//span[text()='Bicycle']");
        readonly By MotorbikeButton = By.XPath("//mcc-dialog//span[text()='Motorbike']");
        readonly By TrainButton = By.XPath("//mcc-dialog//span[text()='Train']");
        readonly By FlightButton = By.XPath("//mcc-dialog//span[text()='Flight']");

        By TransportationButton(string TransportName) => By.XPath("//mcc-dialog//span[text()='" + TransportName + "']");

        By TransportationTypeIcon(string TransportName) => By.XPath("//mcc-dialog//span[text()='" + TransportName + "']/parent::button/mcc-icon[@class='mcc-icon']");

        readonly By RemoveTimeSlotButton = By.XPath("//mcc-dialog//button[text()='Remove time slot']");
        readonly By CloseButton = By.XPath("//*[@id='SA-dialog-dismiss']");

        public SystemUserAvailabilityScheduleTransportPage WaitForApplicantSelectScheduledTransportPopupToLoad(bool CreateMode)
        {
            SwitchToDefaultFrame();

            WaitForElement(problematicDiv);
            SetAttributeValue(problematicDiv, "class", "mcc-dialog");

            WaitForElementVisible(popupHeader);
            WaitForElementVisible(topLabel);

            if (CreateMode)
                ValidateElementText(topLabel, "Create scheduled transport availability");
            else
                ValidateElementText(topLabel, "Edit scheduled transport availability");

            WaitForElement(CarButton);
            WaitForElementVisible(WalkingButton);
            WaitForElementVisible(BicycleButton);
            WaitForElementVisible(MotorbikeButton);

            WaitForElementVisible(CloseButton);

            return this;
        }

        public SystemUserAvailabilityScheduleTransportPage WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(bool CreateMode)
        {
            SwitchToDefaultFrame();

            WaitForElement(problematicDiv);
            SetAttributeValue(problematicDiv, "class", "mcc-dialog");

            WaitForElementVisible(popupHeader);
            WaitForElementVisible(topLabel);

            if (CreateMode)
                ValidateElementText(topLabel, "Create ad hoc transport availability");
            else
                ValidateElementText(topLabel, "Edit ad hoc transport availability");

            WaitForElement(CarButton);
            WaitForElementVisible(WalkingButton);
            WaitForElementVisible(BicycleButton);
            WaitForElementVisible(MotorbikeButton);

            WaitForElementVisible(CloseButton);

            return this;
        }


        public SystemUserAvailabilityScheduleTransportPage ClickCarButton()
        {
            WaitForElementToBeClickable(CarButton);
            Click(CarButton);

            return this;
        }

        public SystemUserAvailabilityScheduleTransportPage ClickWalkingButton()
        {
            WaitForElementToBeClickable(WalkingButton);
            Click(WalkingButton);

            return this;
        }

        public SystemUserAvailabilityScheduleTransportPage ClickBicycleButton()
        {
            WaitForElementToBeClickable(BicycleButton);
            Click(BicycleButton);

            return this;
        }

        public SystemUserAvailabilityScheduleTransportPage ClickMotorbikeButton()
        {
            WaitForElementToBeClickable(MotorbikeButton);
            Click(MotorbikeButton);

            return this;
        }

        public SystemUserAvailabilityScheduleTransportPage ClickTrainButton()
        {
            WaitForElementToBeClickable(TrainButton);
            Click(TrainButton);

            return this;
        }

        public SystemUserAvailabilityScheduleTransportPage ClickFlightButton()
        {
            WaitForElementToBeClickable(FlightButton);
            Click(FlightButton);

            return this;
        }

        public SystemUserAvailabilityScheduleTransportPage ClickRemoveTimeSlotButton()
        {
            WaitForElementToBeClickable(RemoveTimeSlotButton);
            ScrollToElement(RemoveTimeSlotButton);
            Click(RemoveTimeSlotButton);

            return this;
        }

        public SystemUserAvailabilityScheduleTransportPage ClickCloseButton()
        {
            WaitForElementToBeClickable(CloseButton);
            Click(CloseButton);

            return this;
        }

        public SystemUserAvailabilityScheduleTransportPage ValidateTransportationModeIsVisible(string TransportName)
        {
            WaitForElementVisible(TransportationButton(TransportName));

            return this;
        }

        public SystemUserAvailabilityScheduleTransportPage ValidateTransportationTypeIconIsVisible(string TransportName)
        {
            WaitForElementVisible(TransportationTypeIcon(TransportName));

            return this;
        }

        public SystemUserAvailabilityScheduleTransportPage ClickTransportTypeButton(string TransportName)
        {
            WaitForElementToBeClickable(TransportationButton(TransportName));
            ScrollToElement(TransportationButton(TransportName));
            Click(TransportationButton(TransportName));

            return this;
        }

    }
}
