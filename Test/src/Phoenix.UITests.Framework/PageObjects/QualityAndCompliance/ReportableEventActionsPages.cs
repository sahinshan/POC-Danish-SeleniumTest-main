using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReportableEventActionsPage : CommonMethods
    {
        public ReportableEventActionsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By CWContent_Iframe = By.XPath("//iframe[@id='CWContentIFrame']");
        readonly By careproviderReportableEvent_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableevent&')]");
        readonly By ReportableEventActionFrame_Iframe = By.Id("CWUrlPanel_IFrame");

        readonly By quickSearch_TextBox = By.XPath("//*[@id='CWQuickSearch']");

        readonly By recordsearch_Button = By.XPath("//*[@id='CWQuickSearchButton']");
        readonly By createNewRecord_Button = By.Id("TI_NewRecordButton");
        readonly By EventIdColumn = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/span");


        By RecordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By RecordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By tableHeaderRow(int position) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + position + "]/*/*");


        public ReportableEventActionsPage WaitForReportableEventActionsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElement(careproviderReportableEvent_Iframe);
            SwitchToIframe(careproviderReportableEvent_Iframe);

            WaitForElement(ReportableEventActionFrame_Iframe);
            SwitchToIframe(ReportableEventActionFrame_Iframe);

            System.Threading.Thread.Sleep(3000);

            return this;
        }
        public ReportableEventActionsPage ClickCreateNewRecord()
        {
            WaitForElement(createNewRecord_Button);
            Click(createNewRecord_Button);
            return this;
        }
        public ReportableEventActionsPage ValidateCreateNewRecordButton(string ExpectedText)
        {
            WaitForElement(createNewRecord_Button);
            ValidateElementText(createNewRecord_Button, ExpectedText);
            return this;
        }
        public ReportableEventActionsPage OpenRecord(string recordID)
        {
            WaitForElement(RecordRow(recordID));
            this.Click(RecordRow(recordID));
            return this;
        }
        public ReportableEventActionsPage InsertQuickSearchText(string userrecord)
        {
            WaitForElement(quickSearch_TextBox);
            this.SendKeys(quickSearch_TextBox, userrecord);

            return this;
        }
        public ReportableEventActionsPage ClickQuickSearchButton()
        {
            WaitForElement(recordsearch_Button);
            Click(recordsearch_Button);

            return this;
        }
        public ReportableEventActionsPage ValidateRecordData(string recordId, int cellPosition, string expectedText)
        {
            ValidateElementText(recordCell(recordId, cellPosition), expectedText);
            return this;
        }
        public ReportableEventActionsPage ValidateRecordCellText(int Position, string ExpectedText)
        {
            ScrollToElement(tableHeaderRow(Position));
            ValidateElementText(tableHeaderRow(Position), ExpectedText);

            return this;
        }
        public ReportableEventActionsPage ClickEventIdColumnToSort()
        {
            WaitForElementToBeClickable(EventIdColumn);
            Click(EventIdColumn);

            return this;

        }
    }
}

