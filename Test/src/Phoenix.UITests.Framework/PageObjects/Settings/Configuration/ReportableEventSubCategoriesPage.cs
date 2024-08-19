using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReportableEventSubCategoriesPage : CommonMethods
    {
        public ReportableEventSubCategoriesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_reportableEventSubCategory= By.Id("iframe_careproviderreportableeventsubcategory");

        readonly By ReportableEventSubCategoryPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        
        readonly By selectAll_Checkbox = By.Id("cwgridheaderselector");

        readonly By createNewRecord_Button = By.XPath("//button[@id='TI_NewRecordButton']");
        readonly By NameHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/*");
        readonly By CodeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/a/*");
        readonly By GovCodeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/a/*");
        readonly By CategoryHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/a/*");
        readonly By StartDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]/a/*");
        readonly By EndDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[7]/a/*");
        readonly By ValidForExportHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[8]/a/*");
        readonly By CreatedByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[9]/a/*");
        readonly By CreatedOnHeader= By.XPath("//*[@id='CWGridHeaderRow']/th[10]/a/*");
        readonly By ModifiedByHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[11]/a/*");
        readonly By ModifiedOnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[12]/a/*");


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By recordRow_CreatedOnCell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        By recordPosition(int recordPosition, string recordId) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + recordPosition + "][@id='" + recordId + "']/ td[2]");

       
        public ReportableEventSubCategoriesPage WaitForReportableEventSubCategoriesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_reportableEventSubCategory);
            SwitchToIframe(iframe_reportableEventSubCategory);

            WaitForElement(ReportableEventSubCategoryPageHeader);

            WaitForElement(selectAll_Checkbox);

            return this;
        }

        public ReportableEventSubCategoriesPage SelectView(string TextToSelect)
        {
            SelectPicklistElementByText(viewsPicklist, TextToSelect);

            return this;
        }

        public ReportableEventSubCategoriesPage SearchReportableEventSubCategoriesRecord(string SearchQuery, string RecordID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow_CreatedOnCell(RecordID));

            return this;
        }

        public ReportableEventSubCategoriesPage SearchReportableEventSubCategoriesRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ReportableEventSubCategoriesPage OpenReportableEventSubCategoriesRecord(string RecordId)
        {
            WaitForElement(recordRow_CreatedOnCell(RecordId));
            Click(recordRow_CreatedOnCell(RecordId));

            return this;
        }

        public ReportableEventSubCategoriesPage SelectReportableEventSubcategoriesRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ReportableEventSubCategoriesPage ValidateRecordVisible(string recordID)
        {
            WaitForElementVisible(recordRowCheckBox(recordID));
            WaitForElementVisible(recordRow_CreatedOnCell(recordID));
            return this;
        }

        public ReportableEventSubCategoriesPage ClickCreateNewRecord()
        {
            WaitForElementToBeClickable(createNewRecord_Button);
            Click(createNewRecord_Button);
            return this;
        }

        public ReportableEventSubCategoriesPage ValidateReportableEventSubCategoriesNameHeader(string ExpectedText)
        {
            ValidateElementText(NameHeader, ExpectedText);

            return this;
        }


        public ReportableEventSubCategoriesPage ValidateReportableEvenSubCategoriesCodeHeader(string ExpectedText)
        {
            WaitForElementVisible(CodeHeader);
            ValidateElementText(CodeHeader, ExpectedText);

            return this;
        }

        public ReportableEventSubCategoriesPage ValidateReportableEvenSubCategoriesCategoryHeader(string ExpectedText)
        {
            WaitForElementVisible(CategoryHeader);
            ValidateElementText(CategoryHeader, ExpectedText);

            return this;
        }

        public ReportableEventSubCategoriesPage ValidateReportableEventSubCategoriesGovCodeHeader(string ExpectedText)
        {
            WaitForElementVisible(GovCodeHeader);
            ValidateElementText(GovCodeHeader, ExpectedText);

            return this;
        }

        public ReportableEventSubCategoriesPage ValidateReportableEventSubCategoriesStartDateHeader(string ExpectedText)
        {
            WaitForElementVisible(StartDateHeader);
            ValidateElementText(StartDateHeader, ExpectedText);

            return this;
        }

        public ReportableEventSubCategoriesPage ValidateReportableEventSubCategoriesEndDateHeader(string ExpectedText)
        {
            WaitForElementVisible(EndDateHeader);
            ValidateElementText(EndDateHeader, ExpectedText);

            return this;
        }

        public ReportableEventSubCategoriesPage ValidateReportableEventSubCategoriesValidForExportHeader(string ExpectedText)
        {
            WaitForElementVisible(ValidForExportHeader);
            ValidateElementText(ValidForExportHeader, ExpectedText);

            return this;
        }

        public ReportableEventSubCategoriesPage ValidateReportableEventSubCategoriesCreatedByHeader(string ExpectedText)
        {
            WaitForElementVisible(CreatedByHeader);
            ValidateElementText(CreatedByHeader, ExpectedText);

            return this;
        }

        public ReportableEventSubCategoriesPage ValidateReportableEventSubCategoriesCreatedOnHeader(string ExpectedText)
        {
            ScrollToElement(CreatedOnHeader);
            WaitForElementVisible(CreatedOnHeader);
            ValidateElementText(CreatedOnHeader, ExpectedText);

            return this;
        }

        public ReportableEventSubCategoriesPage ValidateReportableEventSubCategoriesModifiedByHeader(string ExpectedText)
        {
            ScrollToElement(ModifiedByHeader);
            WaitForElementVisible(ModifiedByHeader);
            ValidateElementText(ModifiedByHeader, ExpectedText);

            return this;
        }

        public ReportableEventSubCategoriesPage ValidateReportableEventSubCategoriesModifiedOnHeader(string ExpectedText)
        {
            ScrollToElement(ModifiedOnHeader);
            WaitForElementVisible(ModifiedOnHeader);
            ValidateElementText(ModifiedOnHeader, ExpectedText);

            return this;
        }

        public ReportableEventSubCategoriesPage ValidateRecordInPosition(int position, string recordID)
        {
            WaitForElementVisible(recordPosition(position, recordID));

            return this;
        }
    }
}
