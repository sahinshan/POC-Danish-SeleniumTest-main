using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonPhysicalObservationsGenerateChartPopup : CommonMethods
    {
        public PersonPhysicalObservationsGenerateChartPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By popupHeader = By.XPath("//*[@id='CW_PPOHeader']/h1/span[text()='Create Chart']");
        readonly By ChartType_Picklist = By.Id("CW_PPOOptionSet");
        
        readonly By FromDateField = By.Id("CW_PPOFromDate");
        readonly By FromTimeField = By.Id("CW_PPOFromTime");
        readonly By ToDateField = By.Id("CW_PPOToDate");
        readonly By ToTimeField = By.Id("CW_PPOToTime");

        readonly By GenerateButton = By.Id("CWHealthChartDialogGenerate");


        public PersonPhysicalObservationsGenerateChartPopup WaitForPersonPhysicalObservationsGenerateChartPopupToLoad()
        {
            WaitForElement(popupHeader);
            WaitForElement(ChartType_Picklist);
            WaitForElement(GenerateButton);

            return this;
        }

        public PersonPhysicalObservationsGenerateChartPopup SelectChartTypeByText(string TextToSelect)
        {
            SelectPicklistElementByText(ChartType_Picklist, TextToSelect);

            return this;
        }

        public PersonPhysicalObservationsGenerateChartPopup InsertFromDate(string TextToInsert)
        {
            WaitForElement(FromDateField);
            SendKeys(FromDateField, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsGenerateChartPopup InsertFromTime(string TextToInsert)
        {
            WaitForElement(FromTimeField);
            SendKeys(FromTimeField, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsGenerateChartPopup InsertToDate(string TextToInsert)
        {
            WaitForElement(ToDateField);
            SendKeys(ToDateField, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsGenerateChartPopup InsertToTime(string TextToInsert)
        {
            WaitForElement(ToTimeField);
            SendKeys(ToTimeField, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationsGenerateChartPopup TapGenerateButton()
        {
            WaitForElement(GenerateButton);
            Click(GenerateButton);

            WaitForElementNotVisible("CWRefreshPanel", 17);

            return this;
        }



    }
}
