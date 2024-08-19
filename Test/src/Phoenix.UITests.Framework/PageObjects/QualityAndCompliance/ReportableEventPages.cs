using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.QualityAndCompliance
{
    public class ReportableEventPage : CommonMethods
    {
        public ReportableEventPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By ContentIFrame = By.Id("CWContentIFrame");
        readonly By ReportableEventPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Reportable Events']");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");

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


        readonly By EventIdHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]//a/*");
        readonly By ResponsibleUserHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]//a/*");
        readonly By ResponsibleTeamHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[4]//a/*");
        readonly By StartDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]//a/*");
        readonly By EventtypeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]//a/*");
        readonly By GeneralseverityHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[7]//a/*");
        readonly By EventstatusHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[8]//a/*");
        readonly By EndDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[10]//a/*");
        readonly By StatuschangedHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[11]//a/*");
        readonly By StatuschangedbyHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[12]//a/*");
        readonly By CreatedbyHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[13]//a/*");
        readonly By CreatedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[14]//a/*");
        readonly By ModifiedByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[15]//a/*");
        readonly By ModifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[16]//a/*");
        readonly By EventIdSortBtn = By.XPath("//*[@id='CWGridHeaderRow']/th[2]//a");
        By SectionLink(string SectionIdentifier) => By.XPath("//*[@id='CWS_" + SectionIdentifier + "-tab']");

        public ReportableEventPage WaitForReportableEventPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(ContentIFrame);
            SwitchToIframe(ContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(ReportableEventPageHeader);

            WaitForElement(NewRecordButton);

            return this;
        }

        public ReportableEventPage WaitForReportableEventPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            this.WaitForElement(ContentIFrame);
            this.SwitchToIframe(ContentIFrame);

            this.WaitForElement(iframe_CWDataFormDialog);
            this.SwitchToIframe(iframe_CWDataFormDialog);


            return this;
        }
        public ReportableEventPage SearchReportableEventsRecord(string SearchQuery, string ReportableEventID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(ReportableEventRow(ReportableEventID));

            return this;
        }
        public ReportableEventPage ValidateRecordCellText(int Position, string ExpectedText)
        {
            ScrollToElement(tableHeaderRow(Position));
            ValidateElementText(tableHeaderRow(Position), ExpectedText);

            return this;
        }
        public ReportableEventPage TypeSearchQuery(string Query)
        {
            WaitForElement(quickSearchButton);
            SendKeys(quickSearchTextBox, Query);
            Click(quickSearchButton);
            return this;
        }
        public ReportableEventPage ClickRefreshButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public ReportableEventPage ClickEventIdButton()
        {
            WaitForElement(EventIdSortBtn);
            Click(EventIdSortBtn);

            return this;
        }

        public ReportableEventPage SearchReportableEventsRecord(string SearchQuery)
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);
            WaitForElementToBeClickable(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ReportableEventPage SearchReportableEventsRecordusingId(string ReportableEventID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, ReportableEventID);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ReportableEventPage OpenReportableEventRecordUsingID(string ReportableEventID)
        {
            WaitForElement(ReportableEventRow(ReportableEventID));
            driver.FindElement(ReportableEventRow(ReportableEventID)).Click();



            return this;
        }
        public ReportableEventPage OpenReportableEventRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public ReportableEventPage SelectReportableEventRecord(string ReportableEventID)
        {
            WaitForElement(ReportableEventRowCheckBox(ReportableEventID));
            Click(ReportableEventRowCheckBox(ReportableEventID));

            return this;
        }

        public ReportableEventPage SelectReportableEventRecordUsindRecordId(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }
        public ReportableEventPage SelectAvailableViewByText(string PicklistText)
        {
            WaitForElementToBeClickable(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public ReportableEventPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public ReportableEventPage ValidateNewRecordIcon(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NewRecordButton);
            else
                WaitForElementNotVisible(NewRecordButton, 5);

            return this;
        }
        public ReportableEventPage ClickExportToExcelButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }

        public ReportableEventPage ClickDeleteButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }


        public ReportableEventPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(noRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(noRecordMessage, 5);
            }
            return this;
        }

        public ReportableEventPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(recordRowCheckBox(RecordId));

            return this;
        }

        public ReportableEventPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(recordRowCheckBox(RecordId), 7);

            return this;
        }

        public ReportableEventPage ValidateRecordData(int eventId, int cellPosition, string expectedText)
        {
            ValidateElementText(recordCell(eventId, cellPosition), expectedText);
            return this;
        }


        public ReportableEventPage ValidateReportableEventIdOrder(string element)
        {
           
             //   WaitForElement(orderedListText);
              //  int actualElementCount = GetCountOfElements(orderedListText);
               // Assert.AreEqual(elementCount, actualElementCount);
            
                return this;
            


        }
        public ReportableEventPage ValidateEventIdHeaderText(string ExpectedText)
        {
            ValidateElementText(EventIdHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ClickSectionLink(string SectionIdentifier)
        {
            WaitForElementToBeClickable(SectionLink(SectionIdentifier));
            ScrollToElement(SectionLink(SectionIdentifier));
            Click(SectionLink(SectionIdentifier));

            return this;
        }

        public ReportableEventPage ValidateResponsibleUserHeaderText(string ExpectedText)
        {
            ValidateElementText(ResponsibleUserHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidateResponsibleTeamHeaderText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeamHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidateStartDateHeaderText(string ExpectedText)
        {
            ValidateElementText(StartDateHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidateEventTypeHeaderText(string ExpectedText)
        {
            ValidateElementText(EventtypeHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidateGeneralSeverityHeaderText(string ExpectedText)
        {
            ValidateElementText(GeneralseverityHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidateEventStatusHeaderText(string ExpectedText)
        {
            ScrollToElement(EventstatusHeader);
            ValidateElementText(EventstatusHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidateEndDateHeaderText(string ExpectedText)
        {
            ScrollToElement(EndDateHeader);
            ValidateElementText(EndDateHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidateStatusChangedHeaderText(string ExpectedText)
        {
            ScrollToElement(StatuschangedHeader);
            ValidateElementText(StatuschangedHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidateStatusChangedByHeaderText(string ExpectedText)
        {
            ScrollToElement(StatuschangedbyHeader);
            ValidateElementText(StatuschangedbyHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidatecreatedByHeaderText(string ExpectedText)
        {
            ScrollToElement(CreatedbyHeader);
            ValidateElementText(CreatedbyHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidatecreatedOnHeaderText(string ExpectedText)
        {
            ScrollToElement(CreatedOnHeader);
            ValidateElementText(CreatedOnHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidateModifiedByHeaderText(string ExpectedText)
        {
            ScrollToElement(ModifiedByHeader);
            ValidateElementText(ModifiedByHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidateModifiedOnHeaderText(string ExpectedText)
        {
            ScrollToElement(ModifiedOnHeader);
            ValidateElementText(ModifiedOnHeader, ExpectedText);

            return this;
        }

        public ReportableEventPage ValidateRecordInPosition(int position, string recordID)
        {
            WaitForElementVisible(recordPosition(position, recordID));

            return this;
        }
    }
}
