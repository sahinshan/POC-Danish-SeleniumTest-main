using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Recruitment
{
    public class RecruitmentDocumentAttachmentPage : CommonMethods
    {
        public RecruitmentDocumentAttachmentPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Recruitment Document Attachment Page Options Toolbar

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By RecruitmentDocumentAttachmentsIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=compliance&')]");
        readonly By iframe_AttachmentsFrame = By.Id("CWUrlPanel_IFrame");


        readonly By RecruitmentDocumentAttachmentPageHeader = By.XPath("//h1[text() ='Recruitment Document Attachments']");
        readonly By AddButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By assignAnotherTeamButton = By.Id("TI_AssignRecordButton");

        readonly By DetailsTab = By.XPath("//li[@id = 'CWNavGroup_EditForm']");

        #endregion

        #region Recruitment Document Attachment Page Search Grid Header

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.Id("CWRefreshButton");

        #endregion

        #region Recruitment Document Attachment Page Data Grid

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");

        By recordCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        readonly By noRecordsFoundMessage = By.XPath("//h2[text() = 'NO RECORDS']");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");

        By recruitmentDocumentAttachmentCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");

        #endregion

        #region Recruitment Document Attachment Page

        public RecruitmentDocumentAttachmentPage WaitForRecruitmentDocumentAttachmentPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(RecruitmentDocumentAttachmentsIFrame);
            SwitchToIframe(RecruitmentDocumentAttachmentsIFrame);

            WaitForElement(iframe_AttachmentsFrame);
            SwitchToIframe(iframe_AttachmentsFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(RecruitmentDocumentAttachmentPageHeader);
            WaitForElement(viewsPicklist);

            WaitForElement(AddButton);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);
            WaitForElement(exportToExcelButton);
            WaitForElement(deleteButton);

            return this;
        }

        public RecruitmentDocumentAttachmentPage ClickCreateNewRecordButton()
        {
            WaitForElementToBeClickable(AddButton);
            ScrollToElement(AddButton);
            Click(AddButton);

            return this;
        }

        public RecruitmentDocumentAttachmentPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            ScrollToElement(refreshButton);
            Click(refreshButton);

            return this;
        }

        public RecruitmentDocumentAttachmentPage TypeSearchQuery(string Query)
        {
            WaitForElementVisible(quickSearchTextBox);
            ScrollToElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, Query);
            return this;
        }

        public RecruitmentDocumentAttachmentPage ClickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            ScrollToElement(quickSearchButton);
            Click(quickSearchButton);
            return this;
        }

        public RecruitmentDocumentAttachmentPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            ScrollToElement(deleteButton);
            Click(deleteButton);

            return this;
        }

        public RecruitmentDocumentAttachmentPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            ScrollToElement(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }

        public RecruitmentDocumentAttachmentPage ClickAssignToAnotherTeamButton()
        {
            WaitForElementToBeClickable(assignAnotherTeamButton);
            ScrollToElement(assignAnotherTeamButton);
            Click(assignAnotherTeamButton);

            return this;
        }

        public RecruitmentDocumentAttachmentPage OpenRecruitmentDocumentAttachmentsRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            ScrollToElement(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public RecruitmentDocumentAttachmentPage SelectRecruitmentDocumentAttachmentsRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordCheckBox(RecordId));
            ScrollToElement(recordCheckBox(RecordId));
            Click(recordCheckBox(RecordId));

            return this;
        }

        public RecruitmentDocumentAttachmentPage ViewDetailsTab()
        {
            SwitchToDefaultFrame();
            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);
            WaitForElement(DetailsTab);
            WaitForElementToBeClickable(DetailsTab);
            Click(DetailsTab);

            return this;
        }

        public RecruitmentDocumentAttachmentPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(noRecordsFoundMessage);

            }
            else
            {
                WaitForElementNotVisible(noRecordsFoundMessage, 5);
            }
            return this;
        }

        public RecruitmentDocumentAttachmentPage ValidateRecruitmentDocumentAttachmentRecordIsPresent(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));
            ScrollToElement(recordRow(RecordId));

            bool isRecordPresent = GetElementVisibility(recordRow(RecordId));
            Assert.IsTrue(isRecordPresent);

            return this;
        }

        public RecruitmentDocumentAttachmentPage ValidateRecruitmentDocumentAttachmentRecordIsNotPresent(string RecordId)
        {

            bool isRecordPresent = GetElementVisibility(recordRow(RecordId));
            Assert.IsFalse(isRecordPresent);

            return this;
        }

        public RecruitmentDocumentAttachmentPage ValidateRecruitmentDocumentPageSubRecordIsPresent(string RecordId)
        {
            WaitForElement(recruitmentDocumentAttachmentCell(RecordId));
            ScrollToElement(recruitmentDocumentAttachmentCell(RecordId));

            bool isRecordPresent = GetElementVisibility(recruitmentDocumentAttachmentCell(RecordId));
            Assert.IsTrue(isRecordPresent);

            return this;
        }

        #endregion
    }
}
