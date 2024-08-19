using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.QualityAndCompliance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class BookingDiaryStaffPage : CommonMethods
    {
        public BookingDiaryStaffPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame"); 
        readonly By BookingDiaryStaffPageRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpbookingdiarystaff&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By LatitudeField_Label = By.XPath("//label[@title='Latitude']");
        readonly By LatitudeField_Value = By.XPath("//input[@id='CWField_latitude']");
        readonly By LongitudeField_Label = By.XPath("//label[@title='Longitude']");
        readonly By LongitudeField_Value = By.XPath("//input[@id='CWField_longitude']");

        readonly By refreshButton = By.XPath("//button[@id='CWRefreshButton']");





        public BookingDiaryStaffPage WaitForBookingDiaryStaffPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(BookingDiaryStaffPageRecordIFrame);
            SwitchToIframe(BookingDiaryStaffPageRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public BookingDiaryStaffPage ValidateLatitudeFieldLabelText()
        {
           ScrollToElement(LatitudeField_Label);
            Assert.IsTrue(GetElementVisibility(LatitudeField_Label));

            return this;
        }

        public BookingDiaryStaffPage ValidateLatitudeFieldLabelValueEnabled()
        {
            ScrollToElement(LatitudeField_Value);
            WaitForElement(LatitudeField_Value);
            ValidateElementDisabled(LatitudeField_Value);
            return this;
        }


        public BookingDiaryStaffPage ValidateLongitudeFieldLabelText()
        {
            ScrollToElement(LongitudeField_Label);
            Assert.IsTrue(GetElementVisibility(LongitudeField_Label));

            return this;
        }

        public BookingDiaryStaffPage ValidateLongitudeFieldLabelValueEnabled()
        {
            ScrollToElement(LongitudeField_Value);
            WaitForElement(LongitudeField_Value);
            ValidateElementDisabled(LongitudeField_Value);
            return this;
        }


        public BookingDiaryStaffPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }


    }
}
