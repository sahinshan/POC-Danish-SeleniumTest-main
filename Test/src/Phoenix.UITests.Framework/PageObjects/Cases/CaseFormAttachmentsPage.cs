using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CaseFormAttachmentsPage : CommonMethods
    {
        public CaseFormAttachmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By caseFormRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=caseform&')]");
        readonly By CWNavItem_AttachmentsFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Attachments (Case Form)']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By BulkCreateButton = By.Id("TI_UploadMultipleButton");






        public CaseFormAttachmentsPage WaitForCaseFormAttachmentsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseFormRecordIFrame);
            SwitchToIframe(caseFormRecordIFrame);

            WaitForElement(CWNavItem_AttachmentsFrame);
            SwitchToIframe(CWNavItem_AttachmentsFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public CaseFormAttachmentsPage SearchCaseFormAttachmentsRecord(string SearchQuery, string CaseFormAttachmentID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(CaseFormAttachmentID));

            return this;
        }

        public CaseFormAttachmentsPage SearchCaseFormAttachmentsRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public CaseFormAttachmentsPage OpenCaseFormAttachmentsRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public CaseFormAttachmentsPage SelectCaseFormAttachmentsRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }


        public CaseFormAttachmentsPage WaitForBulkEditButtonVisible()
        {
            WaitForElementVisible(BulkEditButton);

            return this;
        }

        public CaseFormAttachmentsPage ClickBulkEditButton()
        {
            Click(BulkEditButton);

            return this;
        }

        public CaseFormAttachmentsPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public CaseFormAttachmentsPage ClickBulkCreateButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(BulkCreateButton);
            Click(BulkCreateButton);

            return this;
        }

        public CaseFormAttachmentsPage ClickQuickSearchButton()
        {
            Click(quickSearchButton);

            return this;
        }
    }
}
