using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SeclusionReviewsRecordPage : CommonMethods
    {
        public SeclusionReviewsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By inpatientSeclusionReviewIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=inpatientseclusionreview&')]");

        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By RelativeItems_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']");
        readonly By CWNavItem_InpatientLeaveAwolCaseNote = By.XPath("//*[@id='CWNavItem_InpatientLeaveAwolCaseNote']");



        readonly By appointment_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_Appointment']");
        readonly By caseNotes_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_InpatientLeaveAwolCaseNote']");
        readonly By emails_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_Email']");
        readonly By letters_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_Letter']");
        readonly By tasks_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_Task']");

        readonly By attachments_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_InpatientLeaveAwolAttachment']");
        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");






        #region Fields

       
        readonly By intialActualReviewDate_Field = By.Id("CWField_actualreviewdatetime");
        readonly By intialActualReviewTime_Field = By.Id("CWField_actualreviewdatetime_Time");
        readonly By willPersonContinueInSeclusion_Field = By.Id("CWField_willpersoncontinueinseclusionid");
        readonly By reviewCommentsAndActionPlan_Field = By.Id("CWField_reviewcommentsandactionplan");
        readonly By professionalsAtReview_LookUpButton = By.Id("CWLookupBtn_professionalsatreview");

        readonly By plannedReviewDate_Field = By.Id("CWField_plannedreviewdatetime");
        readonly By plannedReviewDateTime_Field = By.Id("CWField_plannedreviewdatetime_Time");
        #endregion


        public SeclusionReviewsRecordPage WaitForSeclusionReviewsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(inpatientSeclusionReviewIFrame);
            SwitchToIframe(inpatientSeclusionReviewIFrame);

            WaitForElement(pageHeader);

           
            return this;
        }

        public SeclusionReviewsRecordPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(backButton);

            return this;
        }

        public SeclusionReviewsRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public SeclusionReviewsRecordPage TapSaveButton()
        {
            WaitForElement(saveButton);
            driver.FindElement(saveButton).Click();

           
            return this;
        }

        public SeclusionReviewsRecordPage TapSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);
            return this;
        }

        
        public SeclusionReviewsRecordPage InsertActualReviewDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(intialActualReviewDate_Field);
            SendKeys(intialActualReviewDate_Field, DateToInsert);
            SendKeysWithoutClearing(intialActualReviewDate_Field, Keys.Tab);

            WaitForElement(intialActualReviewTime_Field);
            SendKeys(intialActualReviewTime_Field, TimeToInsert);


            return this;
        }

        public SeclusionReviewsRecordPage InsertPlannedReviewDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(plannedReviewDate_Field);
            SendKeys(plannedReviewDate_Field, DateToInsert);
            SendKeysWithoutClearing(plannedReviewDate_Field, Keys.Tab);

            WaitForElement(plannedReviewDateTime_Field);
            SendKeys(plannedReviewDateTime_Field, TimeToInsert);


            return this;
        }




        public SeclusionReviewsRecordPage ClickProfessionalsAtReviewLookUpButton()
        {
            WaitForElement(professionalsAtReview_LookUpButton);
            Click(professionalsAtReview_LookUpButton);


            return this;
        }


      

        public SeclusionReviewsRecordPage SelectWillPersonContinueInSeclusion(string valueToSelect)
        {
            WaitForElement(willPersonContinueInSeclusion_Field);
            SelectPicklistElementByText(willPersonContinueInSeclusion_Field, valueToSelect);

            return this;
        }



        public SeclusionReviewsRecordPage InsertReviewCommentsAndActionPlan(string TextToInsert)
        {
            WaitForElement(reviewCommentsAndActionPlan_Field);
            SendKeys(reviewCommentsAndActionPlan_Field, TextToInsert);
            return this;
        }

    }
}