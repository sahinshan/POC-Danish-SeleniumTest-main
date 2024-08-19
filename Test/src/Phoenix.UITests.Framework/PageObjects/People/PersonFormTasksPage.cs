﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonFormTasksPage : CommonMethods
    {
        public PersonFormTasksPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personform&')]"); 
        readonly By CWNavItem_PersonTaskFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Tasks']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        readonly By SubjectHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Subject']");
        readonly By DueHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Due']");
        readonly By StatusHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Status']");
        readonly By RegardingHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Regarding']");
        readonly By ReasonHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Reason']");
        readonly By ResponsibleTeamHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Responsible Team']");
        readonly By ResponsibleUserHeader = By.XPath("//*[@id='CWGridHeaderRow']/th/*/*[text()='Responsible User']");


        By RecordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By SubjectCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By DueCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[3]");
        By StatusCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[4]");
        By RegardingCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[5]");
        By ReasonCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[6]");
        By ResponsibleTeamCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[7]");
        By ResponsibleUserCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[8]");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");



        public PersonFormTasksPage WaitForPersonFormTasksPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonTaskFrame);
            SwitchToIframe(CWNavItem_PersonTaskFrame);

            WaitForElement(pageHeader);
            
            WaitForElement(SubjectHeader);
            WaitForElement(DueHeader);
            WaitForElement(StatusHeader);
            WaitForElement(RegardingHeader);
            WaitForElement(ReasonHeader);
            WaitForElement(ResponsibleTeamHeader);
            WaitForElement(ResponsibleUserHeader);


            return this;
        }

        public PersonFormTasksPage SearchPersonFormTaskRecord(string SearchQuery, string personFormTaskID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(SubjectCell(personFormTaskID));

            return this;
        }
        public PersonFormTasksPage SearchPersonFormTaskRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonFormTasksPage OpenPersonFormTaskRecord(string RecordId)
        {
            WaitForElement(SubjectCell(RecordId));
            driver.FindElement(SubjectCell(RecordId)).Click();

            return this;
        }

        public PersonFormTasksPage SelectPersonFormTaskRecord(string RecordId)
        {
            WaitForElement(RecordRowCheckBox(RecordId));
            Click(RecordRowCheckBox(RecordId));

            return this;
        }

        public PersonFormTasksPage ClickNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }

        public PersonFormTasksPage ClickDeleteButton()
        {
            Click(DeleteRecordButton);

            return this;
        }


        public PersonFormTasksPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(RecordRowCheckBox(RecordId));

            return this;
        }
        public PersonFormTasksPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(RecordRowCheckBox(RecordId), 3);

            return this;
        }


        public PersonFormTasksPage ValidateSubjectCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(SubjectCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormTasksPage ValidateDueCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(DueCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormTasksPage ValidateStatusCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(StatusCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormTasksPage ValidateRegardingCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(RegardingCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormTasksPage ValidateReasonCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ReasonCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormTasksPage ValidateResponsibleTeamCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ResponsibleTeamCell(RecordId), ExpectedText);

            return this;
        }
        public PersonFormTasksPage ValidateResponsibleUserCellText(string RecordId, string ExpectedText)
        {
            ValidateElementText(ResponsibleUserCell(RecordId), ExpectedText);

            return this;
        }



    }
}
