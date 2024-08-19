using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinancialDetailDisregardRecordPage : CommonMethods
    {

        public FinancialDetailDisregardRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By incomesupportsetupIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=financialdetaildisregard&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        
        readonly By financialDetailLookupButton = By.Id("CWLookupBtn_financialdetailid");
        readonly By linkedRuleTypeLookupButton = By.Id("CWLookupBtn_chargingruletype");

        readonly By assessmentCategoryPickList = By.Id("CWField_assessmentcategoryid");
        readonly By amountPercentageField = By.Id("CWField_amountpercentage");

        readonly By startDateField = By.Id("CWField_startdate");
        readonly By endDateField = By.Id("CWField_enddate");

        readonly By disregardTypePickList = By.Id("CWField_disregardtypeid");
        readonly By ãuthorityPickList = By.Id("CWField_authorityid");


        

        public FinancialDetailDisregardRecordPage WaitForFinancialDetailDisregardRecordPageToLoad(string IncomeSupportSetupName)
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

            if (driver.FindElement(pageHeader).Text != "Financial Detail Disregard: " + IncomeSupportSetupName)
                throw new Exception("Page title do not equals: Financial Detail Disregard: " + IncomeSupportSetupName);

            return this;
        }



        public FinancialDetailDisregardRecordPage ClickFinancialDetailLookupButton()
        {
            this.Click(financialDetailLookupButton);

            return this;
        }

        public FinancialDetailDisregardRecordPage ClickLinkedRuleTypeLookupButton()
        {
            this.Click(linkedRuleTypeLookupButton);

            return this;
        }

        public FinancialDetailDisregardRecordPage SelectAssessmentCategoryByText(string AssessmentCategoryText)
        {
            this.SelectPicklistElementByText(assessmentCategoryPickList, AssessmentCategoryText);

            return this;
        }

        public FinancialDetailDisregardRecordPage InsertAmountPercentage(string AmountPercentage)
        {
            this.SendKeys(amountPercentageField, AmountPercentage);

            return this;
        }

        public FinancialDetailDisregardRecordPage InsertStartDate(string StartDate)
        {
            this.SendKeys(startDateField, StartDate);

            return this;
        }

        public FinancialDetailDisregardRecordPage InsertEndDate(string EndDate)
        {
            this.SendKeys(endDateField, EndDate);

            return this;
        }

        public FinancialDetailDisregardRecordPage SelectDisregardTypeByText(string DisregardTypeText)
        {
            this.SelectPicklistElementByText(disregardTypePickList, DisregardTypeText);

            return this;
        }

        public FinancialDetailDisregardRecordPage SelectAuthorityByText(string AuthorityText)
        {
            this.SelectPicklistElementByText(ãuthorityPickList, AuthorityText);

            return this;
        }










        public FinancialDetailDisregardRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public FinancialDetailDisregardRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);


            return this;
        }

        public FinancialDetailDisregardRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }




    }
}
