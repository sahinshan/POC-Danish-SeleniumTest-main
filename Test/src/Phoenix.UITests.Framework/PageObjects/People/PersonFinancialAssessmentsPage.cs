using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonFinancialAssessmentsPage: CommonMethods
    {

        public PersonFinancialAssessmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By financialAssessmentFrame = By.Id("CWUrlPanel_IFrame");

        readonly By newButton = By.Id("TI_NewRecordButton");
        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Financial Assessments']");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

        public PersonFinancialAssessmentsPage WaitForPersonFinancialAssessmentsPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(financialAssessmentFrame);
            SwitchToIframe(financialAssessmentFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Financial Assessments");

            return this;
        }

        public FinancialAssessmentRecordPage OpenRecord(string RecordID)
        {
            WaitForElement(RecordIdentifier(RecordID));
            Click(RecordIdentifier(RecordID));

            return new FinancialAssessmentRecordPage(this.driver, this.Wait, this.appURL);
        }

    }
}
