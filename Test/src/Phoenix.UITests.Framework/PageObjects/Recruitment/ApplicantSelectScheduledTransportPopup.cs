using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ApplicantSelectScheduledTransportPopup : CommonMethods
    {
        public ApplicantSelectScheduledTransportPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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

        readonly By RemoveTimeSlotButton = By.XPath("//mcc-dialog//button[text()='Remove time slot']");
        readonly By CloseButton = By.XPath("//*[@id='SA-dialog-dismiss']");


        public ApplicantSelectScheduledTransportPopup WaitForApplicantSelectScheduledTransportPopupToLoad(bool CreateMode)
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

        public ApplicantSelectScheduledTransportPopup WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(bool CreateMode)
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


        public ApplicantSelectScheduledTransportPopup ClickCarButton()
        {
            Click(CarButton);

            return this;
        }
        public ApplicantSelectScheduledTransportPopup ClickWalkingButton()
        {
            Click(WalkingButton);

            return this;
        }
        public ApplicantSelectScheduledTransportPopup ClickBicycleButton()
        {
            Click(BicycleButton);

            return this;
        }
        public ApplicantSelectScheduledTransportPopup ClickMotorbikeButton()
        {
            Click(MotorbikeButton);

            return this;
        }
        public ApplicantSelectScheduledTransportPopup ClickRemoveTimeSlotButton()
        {
            WaitForElementToBeClickable(RemoveTimeSlotButton);
            ScrollToElement(RemoveTimeSlotButton);
            Click(RemoveTimeSlotButton);

            return this;
        }
        public ApplicantSelectScheduledTransportPopup ClickCloseButton()
        {
            Click(CloseButton);

            return this;
        }
        

    }
}
