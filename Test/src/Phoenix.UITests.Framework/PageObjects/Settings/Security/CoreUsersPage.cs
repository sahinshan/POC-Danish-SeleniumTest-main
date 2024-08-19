using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CoreUsersPage : CommonMethods
    {
        public CoreUsersPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");
        readonly By newRecord_Button = By.Id("TI_NewRecordButton");

        readonly By FirstName_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/*/*");
        readonly By LastName_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/*/*");
        readonly By BusinessUnit_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/*/*");
        readonly By DefaultTeam_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]/*/*");
        readonly By WorkEmail_FieldHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[7]/*/*");

        readonly By gridSection = By.Id("CWGridWrapper");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By SortByCreatedOn = By.XPath("//a/span[text()='Created On']");
        readonly By SortByCreatedOnAsc = By.XPath("//a/span[text()='Created On']/following-sibling::span[@class = 'sortasc']");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By recordFirstName_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[3]");
        By recordLastName_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[4]");
        By recordBusinessUnit_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[5]");
        By recordDefaultTeam_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[6]");
        By recordWorkEmail_cell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[7]");

        By noRecordsFoundText = By.XPath("//h2[text() = 'NO RECORDS']");


        public CoreUsersPage WaitForCoreUsersPageToLoad()
        {
            this.SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);
            this.WaitForElement(pageHeader);
            this.ValidateElementText(pageHeader, "Core Users");

            WaitForElementVisible(gridSection);

            return this;
        }

        public CoreUsersPage WaitForResultsGridToLoad()
        {
            this.WaitForElement(FirstName_FieldHeader);
            this.WaitForElement(LastName_FieldHeader);
            this.WaitForElement(BusinessUnit_FieldHeader);
            this.WaitForElement(DefaultTeam_FieldHeader);
            this.WaitForElement(WorkEmail_FieldHeader);

            return this;
        }

        public CoreUsersPage ValidateFirstNameCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordFirstName_cell(RecordID), ExpectedText);

            return this;
        }
        public CoreUsersPage ValidateLastNameCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordLastName_cell(RecordID), ExpectedText);

            return this;
        }

        public CoreUsersPage ValidateBusinessUnitCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordBusinessUnit_cell(RecordID), ExpectedText);

            return this;
        }
        
        public CoreUsersPage ValidateWorkEmailCell(string RecordID, string ExpectedText)
        {
            ValidateElementText(recordWorkEmail_cell(RecordID), ExpectedText);

            return this;
        }

        public CoreUsersPage ValidateRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 3);
            bool isRecordPresent = GetElementVisibility(recordRow(RecordID));
            Assert.IsFalse(isRecordPresent);


            return this;
        }

        public CoreUsersPage InsertQuickSearchText(string Text)
        {
            WaitForElementToBeClickable(quickSearchTextbox);
            this.SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public CoreUsersPage ClickQuickSearchButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public CoreUsersPage ClickRefreshButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);
            WaitForElement(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public CoreUsersPage ClickNewRecordButton()
        {

            WaitForElementNotVisible("CWRefreshPanel", 7);
            Click(newRecord_Button);

            return this;
        }

        public CoreUsersPage OpenRecord(string RecordID)
        {
            System.Threading.Thread.Sleep(500);

            this.WaitForElementToBeClickable(recordRow(RecordID));
            MoveToElementInPage(recordRow(RecordID));
            this.Click(recordRow(RecordID));

            System.Threading.Thread.Sleep(500);

            return this;
        }

        public CoreUsersPage ValidateRecordIsPresent(string RecordID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);
            WaitForElementVisible(recordRow(RecordID));
            bool isRecordPresent = GetElementVisibility(recordRow(RecordID));
            Assert.IsTrue(isRecordPresent);

            return this;
        }

        public CoreUsersPage ClickSortByCreatedOnDescendingOrder()
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

        public CoreUsersPage ValidateNoRecordsFoundText()
        {
            WaitForElementVisible(noRecordsFoundText);
            MoveToElementInPage(noRecordsFoundText);
            bool isNoRecordsPresentTextVisible = GetElementVisibility(noRecordsFoundText);
            Assert.IsTrue(isNoRecordsPresentTextVisible);

            return this;
        }


    }
}
