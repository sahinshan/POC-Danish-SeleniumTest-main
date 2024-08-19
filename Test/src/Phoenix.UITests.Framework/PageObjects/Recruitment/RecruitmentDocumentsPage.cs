using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Recruitment.ApplicantPage
{
    public class RecruitmentDocumentsPage : CommonMethods
    {
        public RecruitmentDocumentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Action Plans Page Options Toolbar

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=applicant&')]");
        readonly By CWRelatedRecordPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By RecruitmentDocumentsPageHeader = By.XPath("//h1[text() ='Recruitment Documents']");
        readonly By AddButton = By.Id("TI_NewRecordButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        #endregion

        #region Action Plans Search Grid Header

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");

        #endregion

        By recordCheckbox(string RecordID) => By.XPath("//input[@id='CHK_" + RecordID + "']");
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");

        By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

        #region Recruitment Page

        public RecruitmentDocumentsPage WaitForRecruitmentDocumentsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWRelatedRecordPanel_IFrame);
            SwitchToIframe(CWRelatedRecordPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(RecruitmentDocumentsPageHeader);

            return this;
        }

        public RecruitmentDocumentsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            ScrollToElement(refreshButton);
            Click(refreshButton);

            return this;
        }

        public RecruitmentDocumentsPage ClickAddButton()
        {
            WaitForElementToBeClickable(AddButton);
            ScrollToElement(AddButton);
            Click(AddButton);

            return this;
        }

        public RecruitmentDocumentsPage SelectView(string ElementToSelect)
        {
            WaitForElementToBeClickable(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, ElementToSelect);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public RecruitmentDocumentsPage TypeSearchQuery(string Query)
        {
            WaitForElementToBeClickable(quickSearchTextBox);
            ScrollToElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, Query);

            return this;
        }

        public RecruitmentDocumentsPage ClickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            ScrollToElement(quickSearchButton);
            Click(quickSearchButton);
            return this;
        }

        public RecruitmentDocumentsPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            ScrollToElement(deleteRecordButton);
            Click(deleteRecordButton);
            return this;
        }

        public RecruitmentDocumentsPage OpenRecruitmentDocumentsRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            ScrollToElement(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public RecruitmentDocumentsPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordCheckbox(RecordId));
            ScrollToElement(recordCheckbox(RecordId));
            Click(recordCheckbox(RecordId));

            return this;
        }

        public RecruitmentDocumentsPage ValidateRecruitmentDocumentsRecordIsPresent(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            ScrollToElement(recordRow(RecordId));

            return this;
        }

        public RecruitmentDocumentsPage ValidateRecruitmentDocumentsRecordIsNotPresent(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 10);

            return this;
        }

        public RecruitmentDocumentsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
            ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

            return this;
        }

        #endregion
    }
}
