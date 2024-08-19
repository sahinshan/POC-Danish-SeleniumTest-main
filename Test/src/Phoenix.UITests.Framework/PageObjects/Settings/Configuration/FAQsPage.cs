using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FAQsPage : CommonMethods
    {
        public FAQsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By FAQsPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");




        readonly By selectAll_Checkbox = By.Id("cwgridheaderselector");
        readonly By name_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[2]");
        readonly By category_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[3]");
        readonly By status_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[4]");
        readonly By upvotes_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[5]");
        readonly By downvotes_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[6]");
        readonly By createdby_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[7]");
        readonly By createdon_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[8]");
        readonly By modifiedby_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[9]");
        readonly By modifiedon_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[10]");




        By FAQRowCheckBox(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[1]/input");

        By FAQRow_NameCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[2]");
        By FAQRow_CategoryCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[3]");
        By FAQRow_StatusCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[4]");
        By FAQRow_UpvotesCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[5]");
        By FAQRow_DownvotesCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[6]");
        By FAQRow_CreatedByCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[7]");
        By FAQRow_CreatedOnCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[8]");
        By FAQRow_ModifiedByCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[9]");
        By FAQRow_ModifiedOnCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[10]");



        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");

        By recordRow(string recordId) => By.XPath("//table[@id = 'CWGrid']/tbody/tr[@id = '"+recordId+"']");





        public FAQsPage WaitForFAQsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(FAQsPageHeader);

            WaitForElement(name_Header);
            WaitForElement(category_Header);
            WaitForElement(status_Header);
            WaitForElement(upvotes_Header);
            WaitForElement(downvotes_Header);
            WaitForElement(createdby_Header);
            WaitForElement(createdon_Header);
            WaitForElement(modifiedby_Header);
            WaitForElement(modifiedon_Header);

            return this;
        }

        public FAQsPage SearchFAQRecord(string SearchQuery, string FAQID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(FAQRow_NameCell(FAQID));

            return this;
        }

        public FAQsPage SearchFAQRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public FAQsPage OpenFAQRecord(string FAQId)
        {
            WaitForElement(FAQRow_NameCell(FAQId));
            Click(FAQRow_NameCell(FAQId));

            return this;
        }

        public FAQsPage SelectFAQRecord(string FAQId)
        {
            WaitForElement(FAQRowCheckBox(FAQId));
            Click(FAQRowCheckBox(FAQId));

            return this;
        }

        public FAQsPage TapNewRecordButton()
        {
            WaitForElement(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }


        public FAQsPage ValidateFAQNameCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_NameCell(recordID), ExpectedText);
            return this;
        }
        public FAQsPage ValidateFAQCategoryCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_CategoryCell(recordID), ExpectedText);
            return this;
        }
        public FAQsPage ValidateFAQStatusCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_StatusCell(recordID), ExpectedText);
            return this;
        }
        public FAQsPage ValidateFAQUpVotesCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_UpvotesCell(recordID), ExpectedText);
            return this;
        }
        public FAQsPage ValidateFAQDownVotesCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_DownvotesCell(recordID), ExpectedText);
            return this;
        }
        public FAQsPage ValidateFAQCreatedByCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_CreatedByCell(recordID), ExpectedText);
            return this;
        }
        public FAQsPage ValidateFAQCreatedOnCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_CreatedOnCell(recordID), ExpectedText);
            return this;
        }
        public FAQsPage ValidateFAQModifiedByCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_ModifiedByCell(recordID), ExpectedText);
            return this;
        }
        public FAQsPage ValidateFAQModifiedOnCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_ModifiedOnCell(recordID), ExpectedText);
            return this;
        }

        public FAQsPage ValidateFAQRecordPresent(string recordID)
        {
            WaitForElementVisible(recordRow(recordID));
            MoveToElementInPage(recordRow(recordID));
            Assert.IsTrue(GetElementVisibility(recordRow(recordID)));
            return this;
        }

    }
}
