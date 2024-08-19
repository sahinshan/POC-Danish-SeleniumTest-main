using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ChargingRuleSetupRecordPage : CommonMethods
    {

        public ChargingRuleSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By chargingRuleSetupIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=chargingrulesetup&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        

        
        readonly By chargingRuleLookupButton = By.Id("CWLookupBtn_chargingruletypeid");
        readonly By authorityPicklist = By.Id("CWField_authorityid");

        readonly By startDateField = By.Id("CWField_startdate");
        readonly By endDateField = By.Id("CWField_enddate");

        readonly By ageFromField = By.Id("CWField_agefrom");
        readonly By ageToField = By.Id("CWField_ageto");

        readonly By singleMinimumCapitalLimitField = By.Id("CWField_singleminimumcapitallimit");
        readonly By singleMaximumCapitalLimitField = By.Id("CWField_singlemaximumcapitallimit");
        readonly By jointMinimumCapitalLimitField= By.Id("CWField_jointminimumcapitallimit");
        readonly By jointMaximumCapitalLimitField = By.Id("CWField_jointmaximumcapitallimit");
        readonly By chargeRateField = By.Id("CWField_chargerate");
        readonly By forEachField = By.Id("CWField_foreach");

        //




        public ChargingRuleSetupRecordPage WaitForChargingRuleSetupRecordPageToLoad(string chargingRuleSetupName)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(chargingRuleSetupIFrame);
            this.SwitchToIframe(chargingRuleSetupIFrame);

            this.WaitForElement(pageHeader);

            this.WaitForElement(chargingRuleLookupButton);
            this.WaitForElement(authorityPicklist);
            this.WaitForElement(startDateField);
            this.WaitForElement(endDateField);

            if (driver.FindElement(pageHeader).Text != "Charging Rule Setup: " + chargingRuleSetupName)
                throw new Exception("Page title do not equals: Charging Rule Setup: " + chargingRuleSetupName);

            return this;
        }



        public ChargingRuleSetupRecordPage ClickChargingRuleLookupButton()
        {
            this.Click(chargingRuleLookupButton);

            return this;
        }

        public ChargingRuleSetupRecordPage SelectAuthority(string ValueTextToSelect)
        {
            this.SelectPicklistElementByText(authorityPicklist, ValueTextToSelect);

            return this;
        }

        public ChargingRuleSetupRecordPage InsertAgeFrom(string AgeFrom)
        {
            this.SendKeys(ageFromField, AgeFrom);

            return this;
        }

        public ChargingRuleSetupRecordPage InsertAgeTo(string AgeTo)
        {
            this.SendKeys(ageToField, AgeTo);

            return this;
        }

        public ChargingRuleSetupRecordPage InsertStartDate(string StartDate)
        {
            this.SendKeys(startDateField, StartDate);

            return this;
        }

        public ChargingRuleSetupRecordPage InsertEndDate(string EndDate)
        {
            this.SendKeys(endDateField, EndDate);

            return this;
        }

        public ChargingRuleSetupRecordPage InsertSingleMinimumCapitalLimit(string singleMinimumCapitalLimit)
        {
            this.SendKeys(singleMinimumCapitalLimitField, singleMinimumCapitalLimit);

            return this;
        }

        public ChargingRuleSetupRecordPage InsertSingleMaximumCapitalLimit(string singleMaximumCapitalLimit)
        {
            this.SendKeys(singleMaximumCapitalLimitField, singleMaximumCapitalLimit);

            return this;
        }

        public ChargingRuleSetupRecordPage InsertJointMinimumCapitalLimit(string jointMinimumCapitalLimit)
        {
            this.SendKeys(jointMinimumCapitalLimitField, jointMinimumCapitalLimit);

            return this;
        }

        public ChargingRuleSetupRecordPage InsertJointMaximumCapitalLimit(string jointMaximumCapitalLimit)
        {
            this.SendKeys(jointMaximumCapitalLimitField, jointMaximumCapitalLimit);

            return this;
        }

        public ChargingRuleSetupRecordPage InsertChargeRate(string ChargeRate)
        {
            this.SendKeys(chargeRateField, ChargeRate);

            return this;
        }

        public ChargingRuleSetupRecordPage InsertForEach(string ForEach)
        {
            this.SendKeys(forEachField, ForEach);

            return this;
        }



        public ChargingRuleSetupRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ChargingRuleSetupRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);


            return this;
        }

        public ChargingRuleSetupRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }




    }
}
