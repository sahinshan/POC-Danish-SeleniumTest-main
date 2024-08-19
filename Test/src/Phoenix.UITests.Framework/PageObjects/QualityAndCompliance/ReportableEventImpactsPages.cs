using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReportableEventImpactsPage : CommonMethods
    {
        public ReportableEventImpactsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
       
        readonly By RecordPanel_Iframe = By.XPath("//iframe[@id='CWUrlPanel_IFrame']");
        readonly By careproviderReportableEvent_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableevent&')]");
       // readonly By ReportableEventImpactFrame_Iframe = By.Id("CWNavGroup_CareProviderReportableEventImpacts");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By quickSearch_TextBox = By.XPath("//*[@id='CWQuickSearch']");

        readonly By recordsearch_Button = By.XPath("//*[@id='CWQuickSearchButton']");
        readonly By createNewRecord_Button = By.XPath("//div[@id='CWWrapper']//button[@id='TI_NewRecordButton']");
        readonly By EventIdColumn = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/span");

        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By RelatedItemMenu = By.XPath("//a[@class='nav-link dropdown-toggle']");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ReportableEventRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableevent&')]");
        readonly By InternalPersonOrganisationHeader = By.XPath("//*[@id='CWGridHeader']/thead/tr/th[2]//a/span[text()='Internal Person/Organisation']");
        readonly By ExternalPersonOrganisationHeader = By.XPath("//*[@id='CWGridHeader']/thead/tr/th[3]//a/span[text()='External Person/Organisation Name']");
        readonly By RoleInEventHeader = By.XPath("//*[@id='CWGridHeader']/thead/tr/th[4]//a/span[text()='Role in Event']");
        readonly By ImpactTypeHeader = By.XPath("//*[@id='CWGridHeader']/thead/tr/th[5]//a/span[text()='Impact Type']");
        
        By recordPosition(int recordPosition, string recordId) => By.XPath("//tr[" + recordPosition + "][@id='" + recordId + "']/ td[2]");
        

        By RecordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By RecordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By tableHeaderRow(int position) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + position + "]//a/span[1]");


        public ReportableEventImpactsPage WaitForReportableEventImpactsPageToLoad()
        {
            

            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ReportableEventRecordIFrame);
            SwitchToIframe(ReportableEventRecordIFrame);

            WaitForElement(RecordPanel_Iframe);
            SwitchToIframe(RecordPanel_Iframe);

            WaitForElementVisible(pageHeader);
            //System.Threading.Thread.Sleep(3000);

            return this;
        }
        public ReportableEventImpactsPage ClickCreateNewRecord()
        {
            WaitForElementToBeClickable(createNewRecord_Button);
            Click(createNewRecord_Button);
            return this;
        }
        public ReportableEventImpactsPage ValidateCreateNewRecordButton(string ExpectedText)
        {
            WaitForElement(createNewRecord_Button);
            ValidateElementText(createNewRecord_Button, ExpectedText);
            return this;
        }
        public ReportableEventImpactsPage OpenRecord(string recordID)
        {
            WaitForElement(RecordRow(recordID));
            this.Click(RecordRow(recordID));
            return this;
        }

        public ReportableEventImpactsPage ValidateRecordInPosition(int position, string recordID)
        {
            WaitForElementVisible(recordPosition(position, recordID));
            
            return this;
        }

        public ReportableEventImpactsPage InsertQuickSearchText(string userrecord)
        {
            WaitForElement(quickSearch_TextBox);
            this.SendKeys(quickSearch_TextBox, userrecord);

            return this;
        }
        public ReportableEventImpactsPage ClickQuickSearchButton()
        {
            WaitForElement(recordsearch_Button);
            Click(recordsearch_Button);

            return this;
        }

       
        public ReportableEventImpactsPage ValidateRecordData(string recordId, int cellPosition, string expectedText)
        {
            ValidateElementText(recordCell(recordId, cellPosition), expectedText);
            return this;
        }
        public ReportableEventImpactsPage ValidateRecordCellText(int Position, string ExpectedText)
        {
            ScrollToElement(tableHeaderRow(Position));
            ValidateElementText(tableHeaderRow(Position), ExpectedText);

            return this;
        }

        public ReportableEventImpactsPage ValidateInternalPersonOrganisationHeaderText(string ExpectedText)
        {
            WaitForElementVisible(InternalPersonOrganisationHeader);
            ValidateElementText(InternalPersonOrganisationHeader, ExpectedText);

            return this;
        }

        public ReportableEventImpactsPage ValidateExternalPersonOrganisationHeaderText(string ExpectedText)
        {
            WaitForElementVisible(ExternalPersonOrganisationHeader);
            ValidateElementText(ExternalPersonOrganisationHeader, ExpectedText);

            return this;
        }

        public ReportableEventImpactsPage ValidateRoleInEventHeaderText(string ExpectedText)
        {
            WaitForElementVisible(RoleInEventHeader);
            ValidateElementText(RoleInEventHeader, ExpectedText);

            return this;
        }

        public ReportableEventImpactsPage ValidateImpactTypeHeaderText(string ExpectedText)
        {
            WaitForElementVisible(ImpactTypeHeader);
            ValidateElementText(ImpactTypeHeader, ExpectedText);

            return this;
        }

    }
}

