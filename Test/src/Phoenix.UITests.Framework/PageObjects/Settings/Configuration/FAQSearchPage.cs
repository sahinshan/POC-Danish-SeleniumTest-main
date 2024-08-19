using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FAQSearchPage : CommonMethods
    {
        public FAQSearchPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By FAQSearchPageHeader = By.XPath("//*[@id='PageTitle']");


        #region Search Area

        readonly By text_FieldHeader = By.Id("TextLabel");
        readonly By category_FieldHeader = By.Id("CWCategoryIdLabel");

        readonly By text_Field = By.Id("CWField_Text");
        readonly By category_LinkField = By.Id("CWCategoryId_Link");
        readonly By category_RemoveButton = By.Id("CWClearLookup_CWCategoryId");
        readonly By category_LookupButton = By.Id("CWLookupBtn_CWCategoryId");

        readonly By searchButton = By.Id("CWSearchButton");
        readonly By clearButton = By.Id("CWClearFiltersButton");

        #endregion

        #region Results Area

        By recordTitle(string recordID) => By.XPath("//*[@id='Title_" + recordID + "']");
        By recordCategory(string recordID) => By.XPath("//*[@id='Category_" + recordID + "']");
        By recordExpandButton(string recordID) => By.XPath("//*[@id='BtnExpandCollapse_" + recordID + "']");
        By recordWasThisHelpfulMessage(string recordID) => By.XPath("//*[@id='li_" + recordID + "']/*/*/*/*/span[text()='Was this helpful?']");
        By recordThankYouMessage(string recordID) => By.XPath("//*[@id='li_" + recordID + "']/*/*/*/*/span[text()='Thank you for your feedback']");
        By recordYesButton(string recordID) => By.XPath("//*[@id='BtnUpvote_" + recordID + "']");
        By recordNoButton(string recordID) => By.XPath("//*[@id='BtnDownvote_" + recordID + "']");
        By recordContent(string recordID, string linenumber) => By.XPath("//*[@id='Content_" + recordID + "']/p[" + linenumber + "]");


        readonly By loadMoreRecordsButton = By.XPath("//button[text()='Load More Records']");

        readonly By noRecordsMessage = By.XPath("//*[@id='CWResultsList']/div/h2[text()='NO RECORDS']");
        readonly By noResultsFoundMessage = By.XPath("//*[@id='CWResultsList']/div/span[text()='No results were found for this screen.']");

        

        #endregion



        public FAQSearchPage WaitForFAQSearchPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(FAQSearchPageHeader);

            WaitForElement(text_FieldHeader);
            WaitForElement(category_FieldHeader);

            WaitForElement(searchButton);
            WaitForElement(clearButton);


            return this;
        }
        public FAQSearchPage InsertText(string ValueToInsert)
        {
            SendKeys(text_Field, ValueToInsert);

            return this;
        }
        public FAQSearchPage ClickCategoryLookupButton()
        {
            Click(category_LookupButton);

            return this;
        }
        public FAQSearchPage ClickCategoryRemoveButton()
        {
            Click(category_RemoveButton);

            return this;
        }



        public FAQSearchPage ClickSearchButton()
        {
            Click(searchButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public FAQSearchPage ClickClearButton()
        {
            Click(clearButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }



        public FAQSearchPage ValidateCategoryFieldText(string ExpectedText)
        {
            ValidateElementText(category_LinkField, ExpectedText);
            return this;
        }


        public FAQSearchPage ValidateRecordPresent(string recordid)
        {
            WaitForElement(recordTitle(recordid));

            return this;
        }

        public FAQSearchPage ValidateRecordNotPresent(string recordid)
        {
            WaitForElementRemoved(recordTitle(recordid));

            return this;
        }

        public FAQSearchPage ValidateRecordTitle(string recordid, string ExpectedText)
        {
            WaitForElement(recordTitle(recordid));
            ValidateElementText(recordTitle(recordid), ExpectedText);

            return this;
        }
        public FAQSearchPage ValidateRecordCategory(string recordid, string ExpectedText)
        {
            ValidateElementText(recordCategory(recordid), ExpectedText);

            return this;
        }
        public FAQSearchPage TapRecordExpandButton(string recordid)
        {
            Click(recordExpandButton(recordid));

            return this;
        }
        
        public FAQSearchPage ValidateWasThisHelpfulMessageVisible(string recordid)
        {
            WaitForElementVisible(recordWasThisHelpfulMessage(recordid));

            return this;
        }
        public FAQSearchPage ValidateWasThisHelpfulMessageNotVisible(string recordid)
        {
            WaitForElementNotVisible(recordWasThisHelpfulMessage(recordid), 10);

            return this;
        }
        
        public FAQSearchPage ValidateThankYouMessageVisible(string recordid)
        {
            WaitForElementVisible(recordThankYouMessage(recordid));

            return this;
        }
        public FAQSearchPage ValidateThankYouMessageNotVisible(string recordid)
        {
            WaitForElementNotVisible(recordThankYouMessage(recordid), 7);

            return this;
        }
        
        public FAQSearchPage ClickYesButton(string recordid)
        {
            Click(recordYesButton(recordid));

            return this;
        }
        public FAQSearchPage ValidateYesButtonVisible(string recordid)
        {
            WaitForElementVisible(recordYesButton(recordid));

            return this;
        }
        public FAQSearchPage ValidateYesButtonNotVisible(string recordid)
        {
            WaitForElementNotVisible(recordYesButton(recordid), 10);

            return this;
        }
        
        public FAQSearchPage ClickNoButton(string recordid)
        {
            Click(recordNoButton(recordid));

            return this;
        }
        public FAQSearchPage ValidateNoButtonVisible(string recordid)
        {
            WaitForElementVisible(recordNoButton(recordid));

            return this;
        }
        public FAQSearchPage ValidateNoButtonNotVisible(string recordid)
        {
            WaitForElementNotVisible(recordNoButton(recordid), 10);

            return this;
        }
        
        public FAQSearchPage ValidateRecordContent(string recordid, string linenumber, string ExpectedText)
        {
            ValidateElementText(recordContent(recordid, linenumber), ExpectedText);

            return this;
        }
        public FAQSearchPage ValidateRecordContentVisibility(string recordid, string linenumber, bool ExpectedVisibility)
        {
            var elementVisible = GetElementVisibility(recordContent(recordid, linenumber));

            if (elementVisible != ExpectedVisibility)
                throw new Exception("Expected: " + ExpectedVisibility + " Actual: " + elementVisible);

            return this;
        }

        
        
        public FAQSearchPage ValidateLoadMoreRecordsButtonVisibility(bool ExpectVisible)
        {
            bool elementVisible = GetElementVisibility(loadMoreRecordsButton);
            if (elementVisible != ExpectVisible)
                throw new Exception("Expected: " + ExpectVisible + " Actual: " + elementVisible );

            return this;
        }

        public FAQSearchPage ClickLoadMoreRecordsButton()
        {
            Click(loadMoreRecordsButton);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public FAQSearchPage ValidateNoRecordsMessageVisibility(bool ExpectVisible)
        {
            bool elementVisible1 = GetElementVisibility(noRecordsMessage);
            bool elementVisible2 = GetElementVisibility(noResultsFoundMessage);
            
            if (elementVisible1 != ExpectVisible )
                throw new Exception("Expected: " + ExpectVisible + " Actual: " + elementVisible1);

            if (elementVisible2 != ExpectVisible)
                throw new Exception("Expected: " + ExpectVisible + " Actual: " + elementVisible2);

            return this;
        }


    }
}
