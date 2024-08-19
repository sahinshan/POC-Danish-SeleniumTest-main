using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.QualityAndCompliance
{
    public class ReportableEventBehaviourPage : CommonMethods
    {
        public ReportableEventBehaviourPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By ContentIFrame = By.Id("CWContentIFrame");
        readonly By ReportableEventBehaviourIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By ReportableEventPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Reportable Events']");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By careproviderReportableEvent_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableevent&')]");

        readonly By viewsPicklist = By.Id("CWViewSelector");

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By recordPosition(int recordPosition, string recordId) => By.XPath("//tr[@id='" + recordId + "']/ td[2]");
        By tableHeaderRow(int position) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + position + "]");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        By recordCell(int RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By ReportableEventRow(string RiskID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RiskID + "']/td[2]");
        By ReportableEventRowCheckBox(string RiskID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RiskID + "']/td[1]/input");

        By ReportableEventIdRow(int RecordID, int RowPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr["+ RowPosition + "][@id='"+ RecordID + "']");

        By ReportableEventsInactiveRecordsRowCount=> By.XPath("//table[@id='CWGrid']/tbody/tr");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");


        readonly By EventIdHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/*");
        readonly By ResponsibleUserHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/a/*");
        readonly By ResponsibleTeamHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/a/*");
        readonly By StartDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/a/*");
        readonly By EventtypeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]/a/*");
        readonly By GeneralseverityHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[7]/a/*");
        readonly By EventstatusHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[8]/a/*");
        readonly By EndDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[10]/a/*");
        readonly By StatuschangedHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[11]/a/*");
        readonly By StatuschangedbyHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[12]/a/*");
        readonly By CreatedbyHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[13]/a/*");
        readonly By CreatedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[14]/a/*");
        readonly By ModifiedByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[15]/a/*");
        readonly By ModifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[16]/a/*");
        readonly By EventIdSortBtn = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a");
        By SectionLink(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "-tab']");

        public ReportableEventBehaviourPage WaitForReportableEventBehaviourPageToLoad()
        {
            SwitchToDefaultFrame();


            this.WaitForElement(ContentIFrame);
            this.SwitchToIframe(ContentIFrame);

            this.WaitForElement(careproviderReportableEvent_Iframe);
            this.SwitchToIframe(careproviderReportableEvent_Iframe);

            this.WaitForElement(ReportableEventBehaviourIFrame);
            this.SwitchToIframe(ReportableEventBehaviourIFrame);
            WaitForElement(NewRecordButton);

            return this;
        }

        public ReportableEventBehaviourPage WaitForReportableEventPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            this.WaitForElement(ContentIFrame);
            this.SwitchToIframe(ContentIFrame);

            this.WaitForElement(careproviderReportableEvent_Iframe);
            this.SwitchToIframe(careproviderReportableEvent_Iframe);

            this.WaitForElement(ReportableEventBehaviourIFrame);
            this.SwitchToIframe(ReportableEventBehaviourIFrame);


            return this;
        }
        public ReportableEventBehaviourPage SearchReportableEventsRecord(string SearchQuery, string ReportableEventID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(ReportableEventRow(ReportableEventID));

            return this;
        }
        public ReportableEventBehaviourPage ValidateRecordCellText(int Position, string ExpectedText)
        {
            ScrollToElement(tableHeaderRow(Position));
            ValidateElementText(tableHeaderRow(Position), ExpectedText);

            return this;
        }
        public ReportableEventBehaviourPage TypeSearchQuery(string Query)
        {
            WaitForElement(quickSearchButton);
            SendKeys(quickSearchTextBox, Query);
            Click(quickSearchButton);
            return this;
        }
        public ReportableEventBehaviourPage ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            Click(refreshButton);

            return this;
        }

        public ReportableEventBehaviourPage ClickNewRecordButton()
        {
            WaitForElement(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public ReportableEventBehaviourPage ClickEventIdButton()
        {
            WaitForElement(EventIdSortBtn);
            Click(EventIdSortBtn);

            return this;
        }

        public ReportableEventBehaviourPage SearchReportableEventsRecord(string SearchQuery)
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);
            WaitForElementToBeClickable(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ReportableEventBehaviourPage SearchReportableEventsRecordusingId(string ReportableEventID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, ReportableEventID);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ReportableEventBehaviourPage OpenReportableEventRecordUsingID(string ReportableEventID)
        {
            WaitForElement(ReportableEventRow(ReportableEventID));
            driver.FindElement(ReportableEventRow(ReportableEventID)).Click();



            return this;
        }
        public ReportableEventBehaviourPage OpenReportableEventRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public ReportableEventBehaviourPage SelectReportableEventRecord(string ReportableEventID)
        {
            WaitForElement(ReportableEventRowCheckBox(ReportableEventID));
            Click(ReportableEventRowCheckBox(ReportableEventID));

            return this;
        }

        public ReportableEventBehaviourPage SelectReportableEventRecordUsindRecordId(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }
       
    }
}
