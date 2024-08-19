using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinancialDetailRateSetupRecordPage : CommonMethods
    {

        public FinancialDetailRateSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By incomesupportsetupIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=financialdetailratesetup&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        
        readonly By financialDetailLookupButton = By.Id("CWLookupBtn_finacialdetailid");
        readonly By frequencyOfReceiptLookupButton = By.Id("CWLookupBtn_frequencyofreceiptid");

        readonly By startDateField = By.Id("CWField_startdate");
        readonly By endDateField = By.Id("CWField_enddate");
        readonly By ageFromField = By.Id("CWField_agefrom");
        readonly By ageToField = By.Id("CWField_ageto");
        readonly By amountField = By.Id("CWField_amount");
        

        

        public FinancialDetailRateSetupRecordPage WaitForFinancialDetailRateSetupRecordPageToLoad(string IncomeSupportSetupName)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(incomesupportsetupIFrame);
            this.SwitchToIframe(incomesupportsetupIFrame);

            this.WaitForElement(pageHeader);

            this.WaitForElement(financialDetailLookupButton);

            this.WaitForElement(startDateField);
            this.WaitForElement(endDateField);

            if (driver.FindElement(pageHeader).Text != "Financial Detail Rate Setup: " + IncomeSupportSetupName)
                throw new Exception("Page title do not equals: Financial Detail Rate Setup: " + IncomeSupportSetupName);

            return this;
        }



        public FinancialDetailRateSetupRecordPage ClickFinancialDetailLookupButton()
        {
            this.Click(financialDetailLookupButton);

            return this;
        }

        public FinancialDetailRateSetupRecordPage ClickFrequencyOfReceiptLookupButton()
        {
            this.Click(frequencyOfReceiptLookupButton);

            return this;
        }

        public FinancialDetailRateSetupRecordPage InsertStartDate(string StartDate)
        {
            this.SendKeys(startDateField, StartDate);

            return this;
        }

        public FinancialDetailRateSetupRecordPage InsertEndDate(string EndDate)
        {
            this.SendKeys(endDateField, EndDate);

            return this;
        }

        public FinancialDetailRateSetupRecordPage InsertAgeFrom(string AgeFrom)
        {
            this.SendKeys(ageFromField, AgeFrom);

            return this;
        }

        public FinancialDetailRateSetupRecordPage InsertAgeTo(string AgeTo)
        {
            this.SendKeys(ageToField, AgeTo);

            return this;
        }

        public FinancialDetailRateSetupRecordPage InsertAmount(string Amount)
        {
            this.SendKeys(amountField, Amount);

            return this;
        }








        public FinancialDetailRateSetupRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public FinancialDetailRateSetupRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);


            return this;
        }

        public FinancialDetailRateSetupRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }




    }
}
