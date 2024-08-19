using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the dynamic dialog that is displayed with messages to the user.
    /// </summary>
    public class AppointmentDialogPopup : CommonMethods
    {
        public AppointmentDialogPopup(IWebDriver driver, WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.XPath("//*[@id='CWContentIFrame']");

        readonly By popupHeader = By.XPath("//*[@id='CWAppointmentDialogHeader']/h1");


        readonly By openButton = By.Id("CWAppointmentDialogOpen");
        readonly By rescheduleButton = By.Id("CWAppointmentDialogReschedule");
        readonly By closeButton = By.Id("CWCloseButton");




        public AppointmentDialogPopup WaitForAppointmentDialogPopupToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementWithSizeVisible(popupHeader);
            WaitForElementWithSizeVisible(openButton);
            WaitForElementWithSizeVisible(rescheduleButton);
            WaitForElementWithSizeVisible(closeButton);

            return this;
        }

        public AppointmentDialogPopup ClickOpenButton()
        {
            ClickOnElementWithSize(openButton);

            return this;
        }

        public AppointmentDialogPopup ClickRescheduleButton()
        {
            ClickOnElementWithSize(rescheduleButton);

            return this;
        }

        public AppointmentDialogPopup ClickCloseButton()
        {
            ClickOnElementWithSize(closeButton);

            return this;
        }

    }
}
