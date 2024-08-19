using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonFinancialDetailRecordPage : CommonMethods
    {

        public PersonFinancialDetailRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personfinancialdetail&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By financialDetailTypePickList = By.Id("CWField_financialdetailtypeid");
        readonly By frequencyOfReceiptLookupButton = By.Id("CWLookupBtn_frequencyofreceiptid");

        readonly By financialDetailLookupButton = By.Id("CWLookupBtn_financialdetailid");
        readonly By financialDetailLink = By.Id("CWField_financialdetailid_Link");

        readonly By startDateField = By.Id("CWField_startdate");
        readonly By endDateField = By.Id("CWField_enddate");

        readonly By amountField = By.Id("CWField_amount");
        readonly By applicationDateField = By.Id("CWField_applicationdate");

        readonly By addressField = By.Id("CWField_address");
        readonly By grossValueField = By.Id("CWField_grossvalue");
        readonly By outstandingLoanField = By.Id("CWField_outstandingloan");
        readonly By percentageOwnershiptField = By.Id("CWField_percentageownership");

        readonly By propertyDisregardTypeLookupButton = By.Id("CWLookupBtn_propertydisregardtypeid");

        readonly By excludeFromDWPCalculationYesRadioButton = By.Id("CWField_excludefromdwpcalculation_1");
        readonly By excludeFromDWPCalculationNoRadioButton = By.Id("CWField_excludefromdwpcalculation_0");

        readonly By inactiveYesRadioButton = By.Id("CWField_inactive_1");
        readonly By inactiveNoRadioButton = By.Id("CWField_inactive_0");

        readonly By Impairment_Icon = By.XPath("//*[@id='CWBannerHolder']/ul/li/div/img[@title='Impairments']");



        public PersonFinancialDetailRecordPage WaitForPersonFinancialDetailRecordPageToLoad(string PersonFinancialDetailName)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            //this.WaitForElement(financialDetailLookupButton);

            this.WaitForElement(startDateField);
            this.WaitForElement(endDateField);

            if (driver.FindElement(pageHeader).Text != "Person Financial Detail: " + PersonFinancialDetailName)
                throw new Exception("Page title do not equals: Person Financial Detail: " + PersonFinancialDetailName);

            return this;
        }




        public PersonFinancialDetailRecordPage SelectfinancialDetailTypeByText(string financialDetailTypeText)
        {
            this.SelectPicklistElementByText(financialDetailTypePickList, financialDetailTypeText);

            return this;
        }

        public PersonFinancialDetailRecordPage ClickFrequencyOfReceiptLookupButton()
        {
            this.Click(frequencyOfReceiptLookupButton);

            return this;
        }

        public PersonFinancialDetailRecordPage InsertStartDate(string StartDate)
        {
            this.SendKeys(startDateField, StartDate);

            return this;
        }

        public PersonFinancialDetailRecordPage InsertEndDate(string EndDate)
        {
            this.SendKeys(endDateField, EndDate);

            return this;
        }

        public PersonFinancialDetailRecordPage ClickFinancialDetailLookupButton()
        {
            this.Click(financialDetailLookupButton);

            return this;
        }

        public PersonFinancialDetailRecordPage ClickFinancialDetailLink()
        {
            this.Click(financialDetailLink);

            return this;
        }

        public PersonFinancialDetailRecordPage InsertAmount(string Amount)
        {
            this.SendKeys(amountField, Amount);

            return this;
        }

        public PersonFinancialDetailRecordPage InsertAddress(string Address)
        {
            this.SendKeys(addressField, Address);

            return this;
        }

        public PersonFinancialDetailRecordPage InsertGrossValue(string GrossValue)
        {
            this.SendKeys(grossValueField, GrossValue);

            return this;
        }

        public PersonFinancialDetailRecordPage InsertOutstandingLoan(string OutstandingLoan)
        {
            this.SendKeys(outstandingLoanField, OutstandingLoan);

            return this;
        }

        public PersonFinancialDetailRecordPage InsertPercentageOwnership(string PercentageOwnership)
        {
            this.SendKeys(percentageOwnershiptField, PercentageOwnership);

            return this;
        }

        public PersonFinancialDetailRecordPage ClickPropertyDisregardType()
        {
            this.Click(propertyDisregardTypeLookupButton);

            return this;
        }

        public PersonFinancialDetailRecordPage SelectExcludeFromDWPCalculation(bool ExcludeFromDWPCalculation)
        {
            if(ExcludeFromDWPCalculation)
                this.Click(excludeFromDWPCalculationYesRadioButton);
            else
                this.Click(excludeFromDWPCalculationNoRadioButton);


            return this;
        }

        public PersonFinancialDetailRecordPage InsertApplicationDate(string ApplicationDate)
        {
            this.SendKeys(applicationDateField, ApplicationDate);

            return this;
        }

        public PersonFinancialDetailRecordPage SelectInactive(bool Inactive)
        {
            if (Inactive)
                this.Click(inactiveYesRadioButton);
            else
                this.Click(inactiveNoRadioButton);


            return this;
        }



        public PersonFinancialDetailRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonFinancialDetailRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);


            return this;
        }

        public PersonFinancialDetailRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);


            return this;
        }

        public PersonFinancialDetailRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }

        public PersonFinancialDetailRecordPage ValidatePersonImpariment_Icon(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(Impairment_Icon);
            }
            else
            {
                WaitForElementNotVisible(Impairment_Icon, 5);
            }
            return this;
        }




    }
}
