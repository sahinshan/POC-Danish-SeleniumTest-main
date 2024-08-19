
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// This class represents the "Person Financial Details" page when accessed via a "Financial Assessment" record (using the "Person Financial Details" tab)
    /// </summary>
    public class PersonFinancialDetailsSubPage : CommonMethods
    {
        public PersonFinancialDetailsSubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By financialAssessmentRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=financialassessment&')]");
        readonly By CWHTMLResourcePanel_IFrame = By.Id("CWHTMLResourcePanel_IFrame");
        readonly By iframe_fapersonfinancialdetails = By.Id("iframe_fapersonfinancialdetails");

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Person Financial Details']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");


        By recordIdentifier(string recordID) => By.XPath("//tr[@id='" + recordID + "']/td[2]");



        public PersonFinancialDetailsSubPage WaitForPersonFinancialDetailsSubPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(financialAssessmentRecordIFrame);
            SwitchToIframe(financialAssessmentRecordIFrame);

            WaitForElement(CWHTMLResourcePanel_IFrame);
            SwitchToIframe(CWHTMLResourcePanel_IFrame);

            WaitForElement(iframe_fapersonfinancialdetails);
            SwitchToIframe(iframe_fapersonfinancialdetails);

            WaitForElement(pagehehader);

            WaitForElement(addNewRecordButton);

            WaitForElement(viewsPicklist);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        public PersonFinancialDetailsSubPage OpenPersonFinancialDetailRecord(string RecordID)
        {
            Click(recordIdentifier(RecordID));

            return new PersonFinancialDetailsSubPage(driver, Wait, appURL);
        }

        public PersonFinancialDetailsSubPage TapAddNewButton()
        {
            this.WaitForElement(addNewRecordButton);
            this.Click(addNewRecordButton);

            return new PersonFinancialDetailsSubPage(driver, Wait, appURL);
        }

    }
}
