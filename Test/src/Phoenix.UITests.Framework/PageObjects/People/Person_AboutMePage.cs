using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class Person_AboutMePage : CommonMethods
    {
        public Person_AboutMePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By personAboutMeIFrame = By.Id("CWUrlPanel_IFrame");

        #region About Me Page Table Grid Columns
        readonly By DateColumn = By.XPath("//*[@id = 'CWGridHeaderRow']/th[@field = 'date']");
        readonly By SupportedToWriteThisColumn = By.XPath("//*[@id = 'CWGridHeaderRow']/th[@field = 'supportedtowritethisbyidname']");
        readonly By CapacityEstablishedColumn = By.XPath("//*[@id = 'CWGridHeaderRow']/th[@field = 'capacityestablished']");
        readonly By ConsentGrantedColumn = By.XPath("//*[@id = 'CWGridHeaderRow']/th[@field = 'consentgranted']");
        //readonly By ResponsibleUserColumn = By.XPath("//*[@id = 'CWGridHeaderRow']/th[@field = 'responsibleuserid_cwname']");
        readonly By CreatedOnColumn = By.XPath("//*[@id = 'CWGridHeaderRow']/th[@field = 'createdon']");
        readonly By ModifiedOnColumn = By.XPath("//*[@id = 'CWGridHeaderRow']/th[@field = 'responsibleuserid_cwname']/following-sibling::th[@field = 'modifiedon']");

        readonly By SortByCreatedOn = By.XPath("//a[@title = 'Sort by Created On'][contains(@onclick,'PERSONABOUTME')]");
        #endregion

        #region Records grid
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        readonly By aboutMeRecordRow = By.XPath("//table[@id='CWGrid']/tbody/tr/td[2]");
        By aboutMeRecordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr/td[1]/input");

        #endregion


        #region About Me tab and options        
        readonly By personAboutMePageHeader = By.XPath("//h1[@class = 'page-title'][contains(text(),'Person About Me')]");
        readonly By LoadingImage = By.XPath("//*[@class = 'loader']");
        #endregion       

        #region Option toolbar
        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignButton = By.Id("TI_DeleteRecordButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        #endregion

        #region Search panel
        readonly By viewsPicklist = By.Id("CWViewSelector");

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div[@class = 'alert alert-info norecords']/h2");
        readonly By refreshButton = By.Id("CWRefreshButton");
        #endregion

        public Person_AboutMePage WaitForPerson_AboutMePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(personAboutMeIFrame);
            SwitchToIframe(personAboutMeIFrame);

            WaitForElement(personAboutMePageHeader);
            WaitForElement(viewsPicklist);
            WaitForElement(quickSearchButton);


            return this;
        }

        public Person_AboutMePage TypeSearchQuery(string Query)
        {
            SendKeys(quickSearchTextBox, Query);
            Click(quickSearchButton);
            return this;
        }
        public Person_AboutMePage ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            Click(refreshButton);

            return this;
        }
        public Person_AboutMePage SearchPersonAboutMeRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            WaitForElement(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }
        public Person_AboutMePage OpenPersonAboutMeRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId), 7);
            ScrollToElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public Person_AboutMePage OpenPersonAboutMeRecord()
        {
            WaitForElement(aboutMeRecordRow);
            MoveToElementInPage(aboutMeRecordRow);
            Click(aboutMeRecordRow);

            return this;
        }


        public Person_AboutMePage SelectAvailableViewByText(string PicklistText)
        {
            WaitForElement(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public Person_AboutMePage ValidateCreateNewRecordButtonAvailable(bool isAvailable)
        {            
            WaitForElementNotVisible("CWRefreshPanel", 7);
            if (isAvailable)
            {                
                Assert.IsTrue(GetElementVisibility(newRecordButton));
            }
            else
            {                
                Assert.IsFalse(GetElementVisibility(newRecordButton));
            }
             return this;            

        }

        public Person_AboutMePage ValidateSelectedPicklistOption(string selectedOption)
        {         
            WaitForElement(viewsPicklist, 3);
            MoveToElementInPage(viewsPicklist);
            ValidatePicklistSelectedText(viewsPicklist, selectedOption);

            return this;

        }

        public Person_AboutMePage ValidatePersonAboutMeGridColumns()
        {
            WaitForElementNotVisible("CWRefreshPanel", 30);            

            Assert.IsTrue(GetElementVisibility(DateColumn));
            Assert.IsTrue(GetElementVisibility(SupportedToWriteThisColumn));
            Assert.IsTrue(GetElementVisibility(CapacityEstablishedColumn));
            Assert.IsTrue(GetElementVisibility(ConsentGrantedColumn));            
            if (GetPicklistSelectedText(viewsPicklist).Equals("Related Records"))
                Assert.IsTrue(GetElementVisibility(CreatedOnColumn));
            if (GetPicklistSelectedText(viewsPicklist).Equals("Search Results"))
                Assert.IsTrue(GetElementVisibility(ModifiedOnColumn));

            return this;
        }

        public Person_AboutMePage ValidateNoRecordMessageVisible(bool ExpectedText)
        {
            bool displayStatus;
            if (ExpectedText)
            {
                WaitForElement(noRecordMessage);
                displayStatus = GetElementVisibility(noRecordMessage);
                Assert.IsTrue(displayStatus);

            }
            else
            {
                displayStatus = GetElementVisibility(noRecordMessage);
                Assert.IsFalse(displayStatus);
            }
            return this;
        }

        public Person_AboutMePage ValidateAboutMeSetupRecordAvailable(string aboutMeSetupId, string aboutMeSetup)
        {
            WaitForElement(recordRow(aboutMeSetupId));
            ValidateElementByTitle(recordRow(aboutMeSetupId), aboutMeSetup);

            return this;
        }

        public Person_AboutMePage ClickSortByCreatedOn()
        {
            WaitForElement(SortByCreatedOn, 3);
            Click(SortByCreatedOn);

            return this;
        }

        public Person_AboutMePage ValidateRelatedRecordsCount(int number)
        {
            WaitForElement(aboutMeRecordRow, 3);
            Assert.AreEqual(number, GetCountOfElements(aboutMeRecordRow));
            return this;
        }

        public Person_AboutMePage ValidatePersonAboutMeRecordPresent(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }
    }
}
