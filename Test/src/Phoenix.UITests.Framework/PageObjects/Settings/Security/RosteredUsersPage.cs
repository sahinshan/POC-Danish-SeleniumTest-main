using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Security
{
    public class RosteredUsersPage : CommonMethods
    {
        public RosteredUsersPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");
        readonly By newRecord_Button = By.Id("TI_NewRecordButton");

        readonly By Id_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/*/*");
        readonly By FirstName_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/*/*");
        readonly By LastName_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/*/*");        
        readonly By BusinessUnit_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/*/*");
        readonly By DefaultTeam_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]/*/*");
        readonly By WorkEmail_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[7]/*/*");



        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By SortByCreatedOn = By.XPath("//a/span[text()='Created On']");
        readonly By SortByCreatedOnAsc = By.XPath("//a/span[text()='Created On']/following-sibling::span[@class = 'sortasc']");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By recordFirstName_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[3]");
        By recordLastName_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[4]");
        By recordName_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[5]");
        By recordBusinessUnit_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[6]");
        By recordUserName_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[7]");
        By recordWorkEmail_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[8]");



        public RosteredUsersPage WaitForRosteredUsersPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);
            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Rostered Users");            

            return this;
        }

        public RosteredUsersPage WaitForResultsGridToLoad()
        {
            WaitForElement(Id_FieldHeader);
            WaitForElement(FirstName_FieldHeader);
            WaitForElement(LastName_FieldHeader);            
            WaitForElement(BusinessUnit_FieldHeader);
            WaitForElement(DefaultTeam_FieldHeader);
            WaitForElement(WorkEmail_FieldHeader);

            return this;
        }


        public RosteredUsersPage ValidateFirstNameCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordFirstName_cell(RecordID), ExpectedText);

            return this;
        }
        public RosteredUsersPage ValidateLastNameCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordLastName_cell(RecordID), ExpectedText);

            return this;
        }
        public RosteredUsersPage ValidateNameCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordName_cell(RecordID), ExpectedText);

            return this;
        }
        public RosteredUsersPage ValidateBusinessUnitCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordBusinessUnit_cell(RecordID), ExpectedText);

            return this;
        }
        public RosteredUsersPage ValidateUserNameCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordUserName_cell(RecordID), ExpectedText);

            return this;
        }
        public RosteredUsersPage ValidateWorkEmailCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordWorkEmail_cell(RecordID), ExpectedText);

            return this;
        }
        public RosteredUsersPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 3);
            bool isRecordPresent = GetElementVisibility(recordRow(RecordID));
            Assert.IsFalse(isRecordPresent);


            return this;
        }
        public RosteredUsersPage InsertQuickSearchText(string Text)
        {
            WaitForElementToBeClickable(quickSearchTextbox);
            this.SendKeys(quickSearchTextbox, Text);

            return this;
        }
        public RosteredUsersPage ClickQuickSearchButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);            

            return this;
        }

        public RosteredUsersPage ClickRefreshButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);
            WaitForElement(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public RosteredUsersPage ClickNewRecordButton()
        {

            WaitForElementNotVisible("CWRefreshPanel", 7);
            Click(newRecord_Button);

            return this;
        }

        public RosteredUsersPage OpenRecord(string RecordID)
        {
            System.Threading.Thread.Sleep(500);

            this.WaitForElementToBeClickable(recordRow(RecordID));
            MoveToElementInPage(recordRow(RecordID));
            this.Click(recordRow(RecordID));

            System.Threading.Thread.Sleep(500);

            return this;
        }


        public RosteredUsersPage SelectSystemViewsRecord(string ExpectedTextToSelect)
        {
            System.Threading.Thread.Sleep(500);

            this.WaitForElementToBeClickable(viewSelector);
            this.Click(viewSelector);

            WaitForElement(viewSelector);

            SelectPicklistElementByText(viewSelector, ExpectedTextToSelect);

            System.Threading.Thread.Sleep(500);

            return this;
        }

        public RosteredUsersPage ValidateRecordIsPresent(string RecordID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);
            WaitForElementVisible(recordRow(RecordID));
            bool isRecordPresent = GetElementVisibility(recordRow(RecordID));
            Assert.IsTrue(isRecordPresent);

            return this;
        }

        public RosteredUsersPage ClickSortByCreatedOnDescendingOrder()
        {
            WaitForElement(SortByCreatedOn);
            MoveToElementInPage(SortByCreatedOn);
            WaitForElementToBeClickable(SortByCreatedOn);
            Click(SortByCreatedOn);
            WaitForElement(SortByCreatedOnAsc);
            MoveToElementInPage(SortByCreatedOnAsc);
            WaitForElementToBeClickable(SortByCreatedOnAsc);
            Click(SortByCreatedOnAsc);            
            return this;
        }


    }
}
