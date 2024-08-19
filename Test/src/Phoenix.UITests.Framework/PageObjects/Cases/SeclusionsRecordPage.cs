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
    public class SeclusionsRecordPage : CommonMethods
    {
        public SeclusionsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By inpatientSeclusionIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=inpatientseclusion&')]");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");


        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By RelativeItems_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']");
      
        readonly By attachments_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_InpatientSeclusionAttachment']");
        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");
        readonly By seclusionReviews_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_InpatientSeclusionsReview']");





        #region Fields

        readonly By Inpatientcase_Field = By.Id("CWLookupBtn_caseid");


        readonly By seclusionDate_Field = By.Id("CWField_seclusiondatetime");
        readonly By seclusionsTime_Field = By.Id("CWField_seclusiondatetime_Time");
        readonly By seclusionsReason_LookUpButton = By.Id("CWLookupBtn_inpatientseclusionreasonid");
        readonly By approvedBy_LookUpButton = By.Id("CWLookupBtn_commencedapprovedbyid");
        readonly By rationaleForSeclusions_Field = By.Id("CWField_rationaleforseclusion");
        readonly By nokCarerNotified_Field = By.Id("CWField_nokcarernotifiedid");
        readonly By commencedBy_LookUpButton = By.Id("CWLookupBtn_commencedbyid");
        readonly By intialPlannedReviewDate_Field = By.Id("CWField_seclusionreviewplanneddatetime");
        readonly By intialPlannedReviewTime_Field = By.Id("CWField_seclusionreviewplanneddatetime_Time");

        readonly By endDate_Field = By.Id("CWField_enddatetime");
        readonly By endDateTime_Field = By.Id("CWField_enddatetime_Time");
        readonly By seclusionDiscontinuedBy_LookUpButton = By.Id("CWLookupBtn_seclusiondiscontinuedbyid");

        readonly By debriefProfessionals_LookUpButton = By.Id("CWLookupBtn_debriefprofessionals");

        #endregion


        public SeclusionsRecordPage WaitForSeclusionsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(inpatientSeclusionIFrame);
            SwitchToIframe(inpatientSeclusionIFrame);

            WaitForElement(pageHeader);

           
            return this;
        }

        public SeclusionsRecordPage WaitForSeclusionsRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDataFormDialog);
            this.SwitchToIframe(iframe_CWDataFormDialog);

            this.WaitForElement(inpatientSeclusionIFrame);
            this.SwitchToIframe(inpatientSeclusionIFrame);

            return this;
        }
        public SeclusionsRecordPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(backButton);

            return this;
        }

        public SeclusionsRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public SeclusionsRecordPage TapSaveButton()
        {
            WaitForElement(saveButton);
            driver.FindElement(saveButton).Click();

           
            return this;
        }

        public SeclusionsRecordPage TapSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);
            return this;
        }

        public SeclusionsRecordPage InsertSeclusionsDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(seclusionDate_Field);
            SendKeys(seclusionDate_Field, DateToInsert);
            SendKeysWithoutClearing(seclusionDate_Field, Keys.Tab);
            
            WaitForElement(seclusionsTime_Field);
            SendKeys(seclusionsTime_Field, TimeToInsert);


            return this;
        }

        public SeclusionsRecordPage InsertEndDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(endDate_Field);
            SendKeys(endDate_Field, DateToInsert);
            SendKeysWithoutClearing(endDate_Field, Keys.Tab);

            WaitForElement(endDateTime_Field);
            SendKeys(endDateTime_Field, TimeToInsert);


            return this;
        }

        public SeclusionsRecordPage InsertIntialPlannedReviewDateTime(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(intialPlannedReviewDate_Field);
            SendKeys(intialPlannedReviewDate_Field, DateToInsert);
            SendKeysWithoutClearing(seclusionDate_Field, Keys.Tab);

            WaitForElement(intialPlannedReviewTime_Field);
            SendKeys(intialPlannedReviewTime_Field, TimeToInsert);


            return this;
        }

       
        public SeclusionsRecordPage ClickSeclusionsReasonLookUpButton()
        {
            WaitForElement(seclusionsReason_LookUpButton);
            Click(seclusionsReason_LookUpButton);


            return this;
        }


        public SeclusionsRecordPage ClickInpatientCaseLookUpButton()
        {
            WaitForElement(Inpatientcase_Field);
            Click(Inpatientcase_Field);


            return this;
        }


        public SeclusionsRecordPage ClickSeclusionsDiscontinuedByLookUpButton()
        {
            WaitForElement(seclusionDiscontinuedBy_LookUpButton);
            Click(seclusionDiscontinuedBy_LookUpButton);


            return this;
        }

        public SeclusionsRecordPage ClickApprovedByLookUpButton()
        {
            WaitForElement(approvedBy_LookUpButton);
            Click(approvedBy_LookUpButton);


            return this;
        }

        public SeclusionsRecordPage ClickCommencedByLookUpButton()
        {
            WaitForElement(commencedBy_LookUpButton);
            Click(commencedBy_LookUpButton);


            return this;
        }

        public SeclusionsRecordPage ClickDebriefProfessionalsLookUpButton()
        {
            WaitForElement(debriefProfessionals_LookUpButton);
            Click(debriefProfessionals_LookUpButton);


            return this;
        }




        public SeclusionsRecordPage SelectNOKcarerNotified(string valueToSelect)
        {
            WaitForElement(nokCarerNotified_Field);
            SelectPicklistElementByText(nokCarerNotified_Field, valueToSelect);

            return this;
        }



        public SeclusionsRecordPage InsertRationaleForSeclusions(string TextToInsert)
        {
            WaitForElement(rationaleForSeclusions_Field);
            SendKeys(rationaleForSeclusions_Field, TextToInsert);
            return this;
        }

        public SeclusionsRecordPage NavigateToAttachmentArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RelativeItems_LeftMenu);
            Click(RelativeItems_LeftMenu);

            WaitForElementToBeClickable(attachments_MenuLeftSubMenu);
            Click(attachments_MenuLeftSubMenu);

            return this;
        }


        public SeclusionsRecordPage NavigateToSeclusionReviews()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RelativeItems_LeftMenu);
            Click(RelativeItems_LeftMenu);

            WaitForElementToBeClickable(seclusionReviews_MenuLeftSubMenu);
            Click(seclusionReviews_MenuLeftSubMenu);

            return this;
        }

        public SeclusionsRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RelativeItems_LeftMenu);
            Click(RelativeItems_LeftMenu);

            WaitForElementToBeClickable(audit_MenuLeftSubMenu);
            Click(audit_MenuLeftSubMenu);

            return this;
        }



    }
}