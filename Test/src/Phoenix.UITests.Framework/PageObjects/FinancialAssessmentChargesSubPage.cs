
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// This class represents the "Financial Assessment Charges" page when accessed via the "Financial Assessment" record (using the "Charges" tab)
    /// </summary>
    public class FinancialAssessmentChargesSubPage : CommonMethods
    {
        public FinancialAssessmentChargesSubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By financialAssessmentRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=financialassessment&')]");
        readonly By CWRelatedRecordPanel_IFrame = By.Id("CWRelatedRecordPanel_IFrame");

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Financial Assessment Charges']");

        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By mailMergeButton = By.Id("TI_MailMergeButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By viewChargeScheduleButton = By.Id("TI_ViewChargeSchedule");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");


        By recordIdentifier(string recordID) => By.XPath("//tr[@id='" + recordID + "']/td[2]");



        public FinancialAssessmentChargesSubPage WaitForFinancialAssessmentRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(financialAssessmentRecordIFrame);
            SwitchToIframe(financialAssessmentRecordIFrame);

            WaitForElement(CWRelatedRecordPanel_IFrame);
            SwitchToIframe(CWRelatedRecordPanel_IFrame);

            WaitForElement(pagehehader);

            WaitForElement(exportToExcelButton);
            WaitForElement(mailMergeButton);
            WaitForElement(assignButton);
            WaitForElement(viewChargeScheduleButton);

            WaitForElement(viewsPicklist);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        public FinancialAssessmentChargePage OpenFinancialAssessmentChargeRecord(string RecordID)
        {
            Click(recordIdentifier(RecordID));

            return new FinancialAssessmentChargePage(driver, Wait, appURL);
        }

    }
}
