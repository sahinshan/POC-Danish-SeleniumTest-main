﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class TrainingCourseSetupPage : CommonMethods
    {
        public TrainingCourseSetupPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Search View Panel

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        readonly By newRecord_Button = By.XPath("//div/button[@title='Create new record']");
        readonly By quickSearchTextbox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By viewSelector = By.Id("CWViewSelector");

        By headerCell_Link(int cellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + cellPosition + "]/div");

        #endregion

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        public TrainingCourseSetupPage WaitForTrainingCourseSetupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElementVisible(pageHeader);

            WaitForElementVisible(newRecord_Button);

            ValidateElementText(pageHeader, "Training Courses");

            return this;
        }

        public TrainingCourseSetupPage ClickHeaderCell(int CellPosition)
        {
            ScrollToElement(headerCell_Link(CellPosition));
            Click(headerCell_Link(CellPosition));

            return this;
        }

        public TrainingCourseSetupPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecord_Button);
            Click(newRecord_Button);

            return this;
        }

        public TrainingCourseSetupPage InsertQuickSearchText(string Text)
        {
            WaitForElementToBeClickable(quickSearchTextbox);
            SendKeys(quickSearchTextbox, Text);

            return this;
        }

        public TrainingCourseSetupPage ClickQuickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public TrainingCourseSetupPage ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            ScrollToElement(refreshButton);
            Click(refreshButton);

            return this;
        }

        public TrainingCourseSetupPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordRow(RecordID));
            Click(recordRow(RecordID));
            return this;
        }

        public TrainingCourseSetupPage ValidateRecordIsPresent(string RecordID)
        {
            WaitForElementVisible(recordRow(RecordID));
            bool ExpectedVisible = GetElementVisibility(recordRow(RecordID));
            Assert.IsTrue(ExpectedVisible);

            return this;
        }

        public TrainingCourseSetupPage ValidateRecordIsNotPresent(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 7);
            bool ExpectedVisible = GetElementVisibility(recordRow(RecordID));
            Assert.IsFalse(ExpectedVisible);

            return this;
        }

        public TrainingCourseSetupPage SelectView(string ViewToSelect)
        {
            WaitForElementVisible(viewSelector);
            ScrollToElement(viewSelector);
            SelectPicklistElementByText(viewSelector, ViewToSelect);

            return this;
        }
    }
}
