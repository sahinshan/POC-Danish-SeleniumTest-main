using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserStaffReviewPage : CommonMethods
    {
        public SystemUserStaffReviewPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By staffReview_iframe = By.XPath("//iframe[@id='CWNavItem_SystemUserStaffReviewFrame']");
        readonly By staffReview_iframe1 = By.XPath("//iframe[@id='CWUrlPanel_IFrame']");


        readonly By systemViewsOption = By.XPath("//select[@id='CWViewSelector']");
        //readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");

        readonly By workSchedule_Link = By.XPath("//li[@id='CWNavGroup_WorkSchedule']/a");
        readonly By name_Link = By.XPath("//a[@title='Name']");

        //readonly By quickSearch_Button = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By createNewRecord_Button = By.Id("TI_NewRecordButton");
        readonly By DeleteRecord_Button = By.Id("TI_DeleteRecordButton");
        readonly By searchButton = By.XPath("//*[@id = 'CWSearchButton']");
        readonly By StaffReviewForm_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[2]//a");
        
        readonly By StaffReviewType_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[3]//a/*");

        By RecordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");
        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        public SystemUserStaffReviewPage WaitForSystemUserStaffReviewPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(staffReview_iframe1);
            SwitchToIframe(staffReview_iframe1);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(createNewRecord_Button);
            WaitForElement(systemViewsOption);
            //WaitForElement(quickSearchTextBox);
            //WaitForElement(quickSearch_Button);


            return this;
        }
        public SystemUserStaffReviewPage WaitForSystemUserAvailabilityMenuClick()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(staffReview_iframe);
            SwitchToIframe(staffReview_iframe);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(workSchedule_Link);
            return this;
        }

        //public SystemUserStaffReviewPage InsertQuickSearchText(string userrecord)
        //{
        //    WaitForElement(quickSearchTextBox);
        //    this.SendKeys(quickSearchTextBox, userrecord);
        //    return this;
        //}

        public SystemUserStaffReviewPage ClickCreateRecordButton()
        {
            WaitForElement(createNewRecord_Button);
            Click(createNewRecord_Button);

            return this;
        }
        public SystemUserStaffReviewPage SelectSystemViewsOption(string selectedText)
        {
            System.Threading.Thread.Sleep(1500);
            WaitForElement(systemViewsOption);
            SelectPicklistElementByText(systemViewsOption, selectedText);

            return this;
        }
        public SystemUserStaffReviewPage SelectStaffReviewRecord(string recordID)
        {
            WaitForElement(RecordRowCheckBox(recordID));
            this.Click(RecordRowCheckBox(recordID));
            return this;
        }
        public SystemUserStaffReviewPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteRecord_Button);
            Click(DeleteRecord_Button);

            return this;
        }

        //public SystemUserStaffReviewPage ClickQuickSearchButton()
        //{
        //    WaitForElement(quickSearch_Button);
        //    Click(quickSearch_Button);
        //    return this;
        //}

        public SystemUserStaffReviewPage ClickSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            MoveToElementInPage(searchButton);
            Click(searchButton);
            return this;
        }

        public SystemUserStaffReviewPage ClickWorkScheduleLink()
        {
            WaitForElement(workSchedule_Link);
            Click(workSchedule_Link);

            return this;
        }

        public SystemUserStaffReviewPage OpenRecord(string RecordID)
        {
            WaitForElement(recordRow(RecordID));
            WaitForElement(recordRow(RecordID));
            WaitForElement(recordRow(RecordID));
            WaitForElement(recordRow(RecordID));
            MoveToElementInPage(recordRow(RecordID));
            Click(recordRow(RecordID));
            return this;
        }
        public SystemUserStaffReviewPage ValidateShouldnotdisplaycolumnName()
        {
            ValidateElementDoNotExist(name_Link);
                   return this;
        }

        public SystemUserStaffReviewPage ValidateStaffReview()
        {
            ValidateElementDoNotExist(name_Link);

            return this;
        }

        public SystemUserStaffReviewPage ValidateStaffReviewForm_Header(String ExpectedText)
        {
            ValidateElementByTitle(StaffReviewForm_Header, ExpectedText);

            return this;
           
        }

        public SystemUserStaffReviewPage ValidateStaffReviewType_Header(String ExpectedText)
        {
            ValidateElementText(StaffReviewType_Header, ExpectedText);
            
            return this;
        }



    }
}
