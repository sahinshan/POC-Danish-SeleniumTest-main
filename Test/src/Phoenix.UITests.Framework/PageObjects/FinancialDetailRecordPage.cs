using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinancialDetailRecordPage : CommonMethods
    {

        public FinancialDetailRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By incomesupportsetupIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=financialdetail&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");


        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");


        readonly By codeFiel = By.Id("CWField_code");
        readonly By govCodeField = By.Id("CWField_govcode");


        

        public FinancialDetailRecordPage WaitForFinancialDetailRecordPageToLoad(string IncomeSupportSetupName)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(incomesupportsetupIFrame);
            this.SwitchToIframe(incomesupportsetupIFrame);

            this.WaitForElement(pageHeader);

            this.WaitForElement(codeFiel);
            this.WaitForElement(govCodeField);

            if (driver.FindElement(pageHeader).Text != "Financial Detail: " + IncomeSupportSetupName)
                throw new Exception("Page title do not equals: Financial Detail: " + IncomeSupportSetupName);

            return this;
        }



        public FinancialDetailRecordPage InsertCode(string Code)
        {
            this.SendKeys(codeFiel, Code);

            return this;
        }

        public FinancialDetailRecordPage InsertGovCode(string GovCode)
        {
            this.SendKeys(govCodeField, GovCode);

            return this;
        }




        public FinancialDetailRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public FinancialDetailRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);


            return this;
        }

        public FinancialDetailRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }




    }
}
