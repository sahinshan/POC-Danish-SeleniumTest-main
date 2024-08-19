using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class IncomeSupportSetupRecordPage : CommonMethods
    {

        public IncomeSupportSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By incomesupportsetupIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=incomesupportsetup&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        

        
        readonly By incomeSupportTypeLookupButton = By.Id("CWLookupBtn_incomesupporttypeid");

        readonly By amountField = By.Id("CWField_amount");
        readonly By jointamountField = By.Id("CWField_jointamount");
        readonly By minimumGuaranteeAmountField = By.Id("CWField_minimumguaranteeamount");
        readonly By jointMinimumGuaranteeAmountField = By.Id("CWField_jointminimumguaranteeamount");

        readonly By startDateField = By.Id("CWField_startdate");
        readonly By endDateField = By.Id("CWField_enddate");

        readonly By ageFromField = By.Id("CWField_agefrom");
        readonly By ageToField = By.Id("CWField_ageto");



        public IncomeSupportSetupRecordPage WaitForIncomeSupportSetupRecordPageToLoad(string IncomeSupportSetupName)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(incomesupportsetupIFrame);
            this.SwitchToIframe(incomesupportsetupIFrame);

            this.WaitForElement(pageHeader);

            this.WaitForElement(incomeSupportTypeLookupButton);
            this.WaitForElement(amountField);
            this.WaitForElement(startDateField);
            this.WaitForElement(endDateField);

            if (driver.FindElement(pageHeader).Text != "Income Support Setup: " + IncomeSupportSetupName)
                throw new Exception("Page title do not equals: Income Support Setup: " + IncomeSupportSetupName);

            return this;
        }



        public IncomeSupportSetupRecordPage ClickIncomeSupportTypeLookupButton()
        {
            this.Click(incomeSupportTypeLookupButton);

            return this;
        }

        public IncomeSupportSetupRecordPage InsertStartDate(string StartDate)
        {
            this.SendKeys(startDateField, StartDate);

            return this;
        }

        public IncomeSupportSetupRecordPage InsertEndDate(string EndDate)
        {
            this.SendKeys(endDateField, EndDate);

            return this;
        }

        public IncomeSupportSetupRecordPage InsertAmount(string Amount)
        {
            this.SendKeys(amountField, Amount);

            return this;
        }

        public IncomeSupportSetupRecordPage InsertJointAmount(string JointAmount)
        {
            this.SendKeys(jointamountField, JointAmount);

            return this;
        }

        public IncomeSupportSetupRecordPage InsertMinimumGuaranteeAmount(string MinimumGuaranteeAmount)
        {
            this.SendKeys(minimumGuaranteeAmountField, MinimumGuaranteeAmount);

            return this;
        }

        public IncomeSupportSetupRecordPage InsertJointMinimumGuaranteeAmount(string JointMinimumGuaranteeAmount)
        {
            this.SendKeys(jointMinimumGuaranteeAmountField, JointMinimumGuaranteeAmount);

            return this;
        }

        public IncomeSupportSetupRecordPage InsertAgeFrom(string AgeFrom)
        {
            this.SendKeys(ageFromField, AgeFrom);

            return this;
        }

        public IncomeSupportSetupRecordPage InsertAgeTo(string AgeTo)
        {
            this.SendKeys(ageToField, AgeTo);

            return this;
        }

        

        



        public IncomeSupportSetupRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public IncomeSupportSetupRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);


            return this;
        }

        public IncomeSupportSetupRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }




    }
}
