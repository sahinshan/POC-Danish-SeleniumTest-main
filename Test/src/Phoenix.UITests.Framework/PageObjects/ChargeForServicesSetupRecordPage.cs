using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ChargeForServicesSetupRecordPage : CommonMethods
    {

        public ChargeForServicesSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By incomesupportsetupIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=chargeforservicessetup&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

       

        
        readonly By chargingRuleLookupButton = By.Id("CWLookupBtn_chargingruletypeid");
        readonly By serviceElement1LookupButton = By.Id("CWLookupBtn_serviceelement1id");
        readonly By serviceElement2LookupButton = By.Id("CWLookupBtn_serviceelement2id");
        readonly By clientCategoryLookupButton = By.Id("CWLookupBtn_financeclientcategoryid");
        readonly By rateUnitLookupButton = By.Id("CWLookupBtn_rateunitid");

        readonly By startDateField = By.Id("CWField_startdate");
        readonly By endDateField = By.Id("CWField_enddate");

        readonly By chargeAtField = By.Id("CWField_chargeatid");
        readonly By percentageOfActualChargeAtRateField = By.Id("CWField_percentofactualchargeatrate");
        readonly By averageChargeAtRateField = By.Id("CWField_averagechargeatrate");
        //



        public ChargeForServicesSetupRecordPage WaitForChargeForServicesSetupRecordPageToLoad(string IncomeSupportSetupName)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(incomesupportsetupIFrame);
            this.SwitchToIframe(incomesupportsetupIFrame);

            this.WaitForElement(pageHeader);

            this.WaitForElement(chargingRuleLookupButton);

            this.WaitForElement(startDateField);
            this.WaitForElement(endDateField);

            if (driver.FindElement(pageHeader).Text != "Charge for Services Setup: " + IncomeSupportSetupName)
                throw new Exception("Page title do not equals: Charge for Services Setup: " + IncomeSupportSetupName);

            return this;
        }



        public ChargeForServicesSetupRecordPage ClickChargingRuleLookupButton()
        {
            this.Click(chargingRuleLookupButton);

            return this;
        }

        public ChargeForServicesSetupRecordPage ClickServiceElement1LookupButton()
        {
            this.Click(serviceElement1LookupButton);

            return this;
        }
        public ChargeForServicesSetupRecordPage ClickServiceElement2LookupButton()
        {
            this.Click(serviceElement2LookupButton);

            return this;
        }
        public ChargeForServicesSetupRecordPage clientCategoryLookupButtonClick()
        {
            this.Click(clientCategoryLookupButton);

            return this;
        }
        public ChargeForServicesSetupRecordPage ClickRateUnitLookupButton()
        {
            this.Click(rateUnitLookupButton);

            return this;
        }



        public ChargeForServicesSetupRecordPage InsertStartDate(string StartDate)
        {
            this.SendKeys(startDateField, StartDate);

            return this;
        }

        public ChargeForServicesSetupRecordPage InsertEndDate(string EndDate)
        {
            this.SendKeys(endDateField, EndDate);

            return this;
        }

        public ChargeForServicesSetupRecordPage SelectChargeAtByText(string ChargeAtTextToSelect)
        {
            this.SelectPicklistElementByText(chargeAtField, ChargeAtTextToSelect);

            return this;
        }

        public ChargeForServicesSetupRecordPage InsertPercentageOfActualChargeAtRateField(string PercentageOfActualChargeAtRate)
        {
            this.SendKeys(percentageOfActualChargeAtRateField, PercentageOfActualChargeAtRate);

            return this;
        }
        
        public ChargeForServicesSetupRecordPage InsertAverageChargeAtRate(string AverageChargeAtRate)
        {
            this.SendKeys(averageChargeAtRateField, AverageChargeAtRate);

            return this;
        }






        public ChargeForServicesSetupRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ChargeForServicesSetupRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);


            return this;
        }

        public ChargeForServicesSetupRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }




    }
}
