using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReportableEventInjuritySeveritiesPage : CommonMethods
    {
        public ReportableEventInjuritySeveritiesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_reportableEventInjurySeverity = By.Id("iframe_careproviderreportableeventinjuryseverity");

        readonly By ReportableEventSeverityPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        
        readonly By selectAll_Checkbox = By.Id("cwgridheaderselector");

        readonly By createNewRecord_Button = By.XPath("//button[@id='TI_NewRecordButton']");
        readonly By NameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/*");
        readonly By CodeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/a/*");
        readonly By GovCodeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/a/*");
        readonly By StartDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/a/*");
        readonly By EndDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]/a/*");
        readonly By ValidForExportHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[7]/a/*");
        readonly By CreatedByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[8]/a/*");
        readonly By CreatedOnHeader= By.XPath("//*[@id='CWGridHeaderRow']/th[9]/a/*");
        readonly By ModifiedByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[10]/a/*");
        readonly By ModifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[11]/a/*");


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By recordRow_CreatedOnCell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        By recordPosition(int recordPosition, string recordId) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + recordPosition + "][@id='" + recordId + "']/ td[2]");

       
        public ReportableEventInjuritySeveritiesPage WaitForReportableEventInjurySeverityPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_reportableEventInjurySeverity);
            SwitchToIframe(iframe_reportableEventInjurySeverity);

            WaitForElement(ReportableEventSeverityPageHeader);

            WaitForElement(selectAll_Checkbox);

            return this;
        }

        public ReportableEventInjuritySeveritiesPage SelectView(string TextToSelect)
        {
            SelectPicklistElementByText(viewsPicklist, TextToSelect);

            return this;
        }

        public ReportableEventInjuritySeveritiesPage SearchReportableEventInjurySeverityRecord(string SearchQuery, string RecordID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow_CreatedOnCell(RecordID));

            return this;
        }

        public ReportableEventInjuritySeveritiesPage SearchReportableEventInjurySeverityRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ReportableEventInjuritySeveritiesPage OpenReportableEventInjurySeverityRecord(string RecordId)
        {
            WaitForElement(recordRow_CreatedOnCell(RecordId));
            Click(recordRow_CreatedOnCell(RecordId));

            return this;
        }

        public ReportableEventInjuritySeveritiesPage SelectReportableEventInjurySeverityRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ReportableEventInjuritySeveritiesPage ValidateRecordVisible(string recordID)
        {
            WaitForElementVisible(recordRowCheckBox(recordID));
            WaitForElementVisible(recordRow_CreatedOnCell(recordID));
            return this;
        }

        public ReportableEventInjuritySeveritiesPage ClickCreateNewRecord()
        {
            WaitForElementToBeClickable(createNewRecord_Button);
            Click(createNewRecord_Button);
            return this;
        }

        public ReportableEventInjuritySeveritiesPage ValidateReportableEventInjuSevNameHeader(string ExpectedText)
        {
            ValidateElementText(NameHeader, ExpectedText);

            return this;
        }


        public ReportableEventInjuritySeveritiesPage ValidateReportableEventInjuSevCodeHeader(string ExpectedText)
        {
            WaitForElementVisible(CodeHeader);
            ValidateElementText(CodeHeader, ExpectedText);

            return this;
        }

        public ReportableEventInjuritySeveritiesPage ValidateReportableEventInjuSevGovCodeHeader(string ExpectedText)
        {
            WaitForElementVisible(GovCodeHeader);
            ValidateElementText(GovCodeHeader, ExpectedText);

            return this;
        }

        public ReportableEventInjuritySeveritiesPage ValidateReportableEventInjuSevStartDateHeader(string ExpectedText)
        {
            WaitForElementVisible(StartDateHeader);
            ValidateElementText(StartDateHeader, ExpectedText);

            return this;
        }

        public ReportableEventInjuritySeveritiesPage ValidateReportableEventInjuSevEndDateHeader(string ExpectedText)
        {
            WaitForElementVisible(EndDateHeader);
            ValidateElementText(EndDateHeader, ExpectedText);

            return this;
        }

        public ReportableEventInjuritySeveritiesPage ValidateReportableEventInjuSevValidForExportHeader(string ExpectedText)
        {
            WaitForElementVisible(ValidForExportHeader);
            ValidateElementText(ValidForExportHeader, ExpectedText);

            return this;
        }

        public ReportableEventInjuritySeveritiesPage ValidateReportableEventInjuSevCreatedByHeader(string ExpectedText)
        {
            WaitForElementVisible(CreatedByHeader);
            ValidateElementText(CreatedByHeader, ExpectedText);

            return this;
        }

        public ReportableEventInjuritySeveritiesPage ValidateReportableEventInjuSevCreatedOnHeader(string ExpectedText)
        {
            WaitForElementVisible(CreatedOnHeader);
            ValidateElementText(CreatedOnHeader, ExpectedText);

            return this;
        }

        public ReportableEventInjuritySeveritiesPage ValidateReportableEventInjuSevModifiedByHeader(string ExpectedText)
        {
            WaitForElementVisible(ModifiedByHeader);
            ValidateElementText(ModifiedByHeader, ExpectedText);

            return this;
        }

        public ReportableEventInjuritySeveritiesPage ValidateReportableEventInjuSevModifiedOnHeader(string ExpectedText)
        {
            ScrollToElement(ModifiedOnHeader);
            WaitForElementVisible(ModifiedOnHeader);
            ValidateElementText(ModifiedOnHeader, ExpectedText);

            return this;
        }

        public ReportableEventInjuritySeveritiesPage ValidateRecordInPosition(int position, string recordID)
        {
            WaitForElementVisible(recordPosition(position, recordID));

            return this;
        }
    }
}
