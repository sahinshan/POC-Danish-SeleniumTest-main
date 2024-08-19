using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonContributionExceptionsPage: CommonMethods
    {

        public PersonContributionExceptionsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=facontribution&')]");
        readonly By CWNavItem_ContributionExceptionFrame = By.Id("CWNavItem_ContributionExceptionFrame");

        readonly By newButton = By.Id("TI_NewRecordButton");
        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Contribution Exceptions']");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        public PersonContributionExceptionsPage WaitForPersonContributionExceptionsPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWNavItem_ContributionExceptionFrame);
            SwitchToIframe(CWNavItem_ContributionExceptionFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Contribution Exceptions");

            return this;
        }

        public FinancialAssessmentRecordPage OpenRecord(string RecordID)
        {
            WaitForElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return new FinancialAssessmentRecordPage(this.driver, this.Wait, this.appURL);
        }

        public FinancialAssessmentRecordPage ClickAddNewRecordButton()
        {
            WaitForElement(newButton);
            Click(newButton);

            return new FinancialAssessmentRecordPage(this.driver, this.Wait, this.appURL);
        }

    }
}
