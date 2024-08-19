using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class NonResidentialPolicyRateSetupRecordPage : CommonMethods
    {

        public NonResidentialPolicyRateSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By incomesupportsetupIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=nonresidentialpolicyratesetup&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        

        
        readonly By chargingRuleTypeLookupButton = By.Id("CWLookupBtn_chargingruletypeid");

        readonly By startDateField = By.Id("CWField_startdate");
        readonly By endDateField = By.Id("CWField_enddate");

        readonly By ageFromField = By.Id("CWField_agefrom");
        readonly By ageToField = By.Id("CWField_ageto");

        readonly By percentIncreaseOnIsGcAmountField = By.Id("CWField_percentincreaseonisgcamount");
        readonly By minimumDisabilityRelatedExpenseField = By.Id("CWField_minimumdisabilityrelatedexpense");
        readonly By minimumWeeklyChargeField = By.Id("CWField_minimumweeklycharge");
        readonly By maximumWeeklyChargeField = By.Id("CWField_maximumweeklycharge");
        readonly By exceedMaximumWeeklyChargeOnFullCostYesButton = By.Id("CWField_exceedmaximumweeklychargeonfullcost_1");
        readonly By exceedMaximumWeeklyChargeOnFullCostNoButton = By.Id("CWField_exceedmaximumweeklychargeonfullcost_0");


        



        public NonResidentialPolicyRateSetupRecordPage WaitForNonResidentialPolicyRateSetupRecordPageToLoad(string IncomeSupportSetupName)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(incomesupportsetupIFrame);
            this.SwitchToIframe(incomesupportsetupIFrame);

            this.WaitForElement(pageHeader);

            this.WaitForElement(chargingRuleTypeLookupButton);

            this.WaitForElement(startDateField);
            this.WaitForElement(endDateField);

            if (driver.FindElement(pageHeader).Text != "Non-Residential Policy Rate Setup: " + IncomeSupportSetupName)
                throw new Exception("Page title do not equals: Non-Residential Policy Rate Setup: " + IncomeSupportSetupName);

            return this;
        }



        public NonResidentialPolicyRateSetupRecordPage ClickChargingRuleTypeLookupButton()
        {
            this.Click(chargingRuleTypeLookupButton);

            return this;
        }



        public NonResidentialPolicyRateSetupRecordPage InsertStartDate(string StartDate)
        {
            this.SendKeys(startDateField, StartDate);

            return this;
        }

        public NonResidentialPolicyRateSetupRecordPage InsertEndDate(string EndDate)
        {
            this.SendKeys(endDateField, EndDate);

            return this;
        }



        public NonResidentialPolicyRateSetupRecordPage InsertAgeFrom(string AgeFrom)
        {
            this.SendKeys(ageFromField, AgeFrom);

            return this;
        }

        public NonResidentialPolicyRateSetupRecordPage InsertAgeTo(string AgeTo)
        {
            this.SendKeys(ageToField, AgeTo);

            return this;
        }



        public NonResidentialPolicyRateSetupRecordPage InsertPercentageIncreaseOnISGCAmount(string PercentageIncreaseOnISGCAmount)
        {
            this.SendKeys(percentIncreaseOnIsGcAmountField, PercentageIncreaseOnISGCAmount);

            return this;
        }

        public NonResidentialPolicyRateSetupRecordPage InsertMinimumDisabilityRelatedExpense(string MinimumDisabilityRelatedExpense)
        {
            this.SendKeys(minimumDisabilityRelatedExpenseField, MinimumDisabilityRelatedExpense);

            return this;
        }

        public NonResidentialPolicyRateSetupRecordPage InsertMinimumWeeklyCharge(string MinimumWeeklyCharge)
        {
            this.SendKeys(minimumWeeklyChargeField, MinimumWeeklyCharge);

            return this;
        }

        public NonResidentialPolicyRateSetupRecordPage InsertMaximumWeeklyCharge(string MaximumWeeklyCharge)
        {
            this.SendKeys(maximumWeeklyChargeField, MaximumWeeklyCharge);

            return this;
        }

        public NonResidentialPolicyRateSetupRecordPage SelectExceedMaximumWeeklyChargeOnFullCost(bool ExceedMaximumWeeklyChargeOnFullCost)
        {
            this.WaitForElementToBeClickable(exceedMaximumWeeklyChargeOnFullCostYesButton);
            this.WaitForElementToBeClickable(exceedMaximumWeeklyChargeOnFullCostNoButton);

            if (ExceedMaximumWeeklyChargeOnFullCost)
                this.Click(exceedMaximumWeeklyChargeOnFullCostYesButton);
            else
                this.Click(exceedMaximumWeeklyChargeOnFullCostNoButton);

            return this;
        }



        public NonResidentialPolicyRateSetupRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public NonResidentialPolicyRateSetupRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);


            return this;
        }

        public NonResidentialPolicyRateSetupRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }




    }
}
