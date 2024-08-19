using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonContributionExceptionRecordPage : CommonMethods
    {

        public PersonContributionExceptionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By incomesupportsetupIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=facontributionexception&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By exceptionReasonLookupButton = By.Id("CWLookupBtn_exceptionreasonid");
        readonly By recoveryMethodLookupButton = By.Id("CWLookupBtn_recoverymethodid");
        readonly By debtorBatchGroupingLookupButton = By.Id("CWLookupBtn_debtorbatchgroupingid");


        

        public PersonContributionExceptionRecordPage WaitForPersonContributionExceptionRecordPageToLoad(string ContributionExceptionName)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(incomesupportsetupIFrame);
            this.SwitchToIframe(incomesupportsetupIFrame);

            this.WaitForElement(pageHeader);

            this.WaitForElement(exceptionReasonLookupButton);
            this.WaitForElement(recoveryMethodLookupButton);
            this.WaitForElement(debtorBatchGroupingLookupButton);

            if (driver.FindElement(pageHeader).Text != "Contribution Exception: " + ContributionExceptionName)
                throw new Exception("Page title do not equals: Contribution Exception: " + ContributionExceptionName);

            return this;
        }



        public PersonContributionExceptionRecordPage ClickExceptionReasonLookupButton()
        {
            this.Click(exceptionReasonLookupButton);

            return this;
        }

        public PersonContributionExceptionRecordPage ClickRecoveryMethodLookupButton()
        {
            this.Click(recoveryMethodLookupButton);

            return this;
        }

        public PersonContributionExceptionRecordPage ClickDebtorBatchGroupingLookupButton()
        {
            this.Click(debtorBatchGroupingLookupButton);

            return this;
        }   




        public PersonContributionExceptionRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonContributionExceptionRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);


            return this;
        }

        public PersonContributionExceptionRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }




    }
}
