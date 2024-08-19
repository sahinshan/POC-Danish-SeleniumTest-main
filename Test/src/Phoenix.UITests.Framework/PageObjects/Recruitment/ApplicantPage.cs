using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Recruitment.ApplicantPage
{
    public class ApplicantPage : CommonMethods
    {
        public ApplicantPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Action Plans Page Options Toolbar

        readonly By CWContentIFrame = By.Id("CWContentIFrame");

        readonly By ApplicantsPageHeader = By.XPath("//h1[text() ='Applicants']");
        readonly By AddButton = By.Id("TI_NewRecordButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By DetailsTab = By.XPath("//li[@id = 'CWNavGroup_EditForm']");
        readonly By noRecordsFoundMessage = By.XPath("//h2[text() = 'NO RECORDS']");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");

        #endregion

        #region Action Plans Search Grid Header
        readonly By viewsPicklist = By.Id("CWViewSelector");

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");

        #endregion

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");


        #region Recruitment Page

        public ApplicantPage WaitForApplicantsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(ApplicantsPageHeader);

            WaitForElement(AddButton);

            return this;
        }

        public ApplicantPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public ApplicantPage ClickAddButton()
        {
            WaitForElementToBeClickable(AddButton);
            MoveToElementInPage(AddButton);
            Click(AddButton);

            return this;
        }

        public ApplicantPage TypeSearchQuery(string Query)
        {
            WaitForElementVisible(quickSearchTextBox);
            MoveToElementInPage(quickSearchTextBox);
            SendKeys(quickSearchTextBox, Query);
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public ApplicantPage SearchApplicantRecord(string Query)
        {
            WaitForElementVisible(quickSearchTextBox);
            MoveToElementInPage(quickSearchTextBox);
            SendKeys(quickSearchTextBox, Query);
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public ApplicantPage SelectAvailableViewByText(string PicklistText)
        {
            WaitForElementToBeClickable(viewsPicklist);
            MoveToElementInPage(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            return this;
        }

        public ApplicantPage OpenApplicantRecord(string RecordId)
        {
            WaitForElementNotVisible("CWRefreshPanel", 30);
            WaitForElementToBeClickable(recordRow(RecordId));
            ScrollToElement(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public ApplicantPage ValidateApplicantRecordIsPresent(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));
            WaitForElementToBeClickable(recordRow(RecordId));
            MoveToElementInPage(recordRow(RecordId));

            bool isRecordPresent = GetElementVisibility(recordRow(RecordId));
            Assert.IsTrue(isRecordPresent);

            return this;
        }

        #endregion
    }
}
