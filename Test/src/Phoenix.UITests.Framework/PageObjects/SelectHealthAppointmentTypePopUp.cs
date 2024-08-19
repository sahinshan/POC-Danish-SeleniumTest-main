using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SelectHealthAppointmentTypePopUp : CommonMethods
    {


        public SelectHealthAppointmentTypePopUp(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By AppointmentTypeRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=healthappointment&')]");

        readonly By appointmentTypes = By.Id("dataFormList");
        readonly By popUpHeader = By.Id("CWDataFormListHeader");
        readonly By nextButton = By.Id("btnChooseDataForm");
        readonly By closeButton = By.Id("CWCloseButton");
        
        public SelectHealthAppointmentTypePopUp WaitForSelectHealthAppointmentTypePopUpToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(AppointmentTypeRecordIFrame);
            SwitchToIframe(AppointmentTypeRecordIFrame);

            WaitForElement(popUpHeader);

            return this;
        }
        public SelectHealthAppointmentTypePopUp WaitForSelectHealthAppointmentTypePopUpToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDataFormDialog);
            SwitchToIframe(iframe_CWDataFormDialog);

            WaitForElement(AppointmentTypeRecordIFrame);
            SwitchToIframe(AppointmentTypeRecordIFrame);

            WaitForElement(popUpHeader);

            return this;
        }



        public SelectHealthAppointmentTypePopUp TapNextButton()
        {
            Click(nextButton);
           
            return this;
        }

        private IAlert WaitForAlert()
        {
            int i = 0;
            while (i++ < 15)
            {
                try
                {
                    return driver.SwitchTo().Alert();
                }
                catch (NoAlertPresentException e)
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }

            throw new Exception("No Alert was displayed after 5 seconds");
        }

        public SelectHealthAppointmentTypePopUp SelectViewByText(string TextToSelect)
        {

            SelectPicklistElementByText(appointmentTypes, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }

        public SelectHealthAppointmentTypePopUp ValidateHealthAppointmentOption(string OptionText)
        {
            ValidatePicklistContainsElementByText(appointmentTypes, OptionText);

            return this;
        }

    }
}
