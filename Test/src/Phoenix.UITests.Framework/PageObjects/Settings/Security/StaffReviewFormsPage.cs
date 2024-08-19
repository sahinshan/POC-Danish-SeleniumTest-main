using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class StaffReviewFormsPage : CommonMethods
    {
        public StaffReviewFormsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By staffReview_IFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=staffreview&')]");
        readonly By staffReviewForm_iframe1 = By.XPath("//iframe[@id='CWUrlPanel_IFrame']");

        readonly By systemViewsOption = By.XPath("//select[@id='CWViewSelector']");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
       
        readonly By quickSearch_Button = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By createNewRecord_Button = By.Id("TI_NewRecordButton");
        readonly By DeleteRecord_Button = By.Id("TI_DeleteRecordButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        By RecordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");
        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        public StaffReviewFormsPage WaitForStaffReviewFormsPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(staffReview_IFrame);
            SwitchToIframe(staffReview_IFrame);

            WaitForElement(staffReviewForm_iframe1);
            SwitchToIframe(staffReviewForm_iframe1);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(systemViewsOption);
            WaitForElementVisible(quickSearchTextBox);
            WaitForElementVisible(quickSearch_Button);

            return this;
        }
        public StaffReviewFormsPage InsertQuickSearchText(string userrecord)
        {
            WaitForElement(quickSearchTextBox);
            this.SendKeys(quickSearchTextBox, userrecord);
            return this;
        }
        public StaffReviewFormsPage ClickCreateRecordButton()
        {
            WaitForElementToBeClickable(createNewRecord_Button);
            Click(createNewRecord_Button);

            return this;
        }
        public StaffReviewFormsPage ClickDeleteRecordButton()
        {
            MoveToElementInPage(DeleteRecord_Button);
            WaitForElementToBeClickable(DeleteRecord_Button);
            Click(DeleteRecord_Button);

            return this;
        }
        public StaffReviewFormsPage ClickQuickSearchButton()
        {
            WaitForElement(quickSearch_Button);
            Click(quickSearch_Button);
            return this;
        }

        public StaffReviewFormsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            System.Threading.Thread.Sleep(2000);
            Click(refreshButton);
            return this;
        }
        public StaffReviewFormsPage ValidateCreateRecordButton(string ExpectedText)
        {
            WaitForElement(createNewRecord_Button);
            ValidateElementText(createNewRecord_Button, ExpectedText);

            return this;
        }
        public StaffReviewFormsPage ValidateDeleteRecordButton(string ExpectedText)
        {
            WaitForElement(DeleteRecord_Button);
            ValidateElementText(DeleteRecord_Button, ExpectedText);

            return this;
        }
        public StaffReviewFormsPage SelectRecord(string RecordID)
        {
            MoveToElementInPage(RecordRowCheckBox(RecordID));
            WaitForElementToBeClickable(RecordRowCheckBox(RecordID));
            Click(RecordRowCheckBox(RecordID));
            return this;
        }
        public StaffReviewFormsPage OpenRecord(string RecordID)
        {
            WaitForElement(recordRow(RecordID));
            WaitForElement(recordRow(RecordID));
            WaitForElement(recordRow(RecordID));
            WaitForElement(recordRow(RecordID));
            this.Click(recordRow(RecordID));
            return this;
        }
        public StaffReviewFormsPage ValidateCreateRecordButtonNotDisplay()
        {
            ValidateElementDoNotExist(createNewRecord_Button);

            return this;
        }
        public StaffReviewFormsPage ValidateDeleteRecordButtonNotDisplay()
        {
            ValidateElementDoNotExist(DeleteRecord_Button);

            return this;
        }
    }
}
