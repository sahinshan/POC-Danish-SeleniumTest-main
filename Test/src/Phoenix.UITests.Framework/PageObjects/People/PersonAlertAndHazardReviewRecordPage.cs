using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People


{
   public  class PersonAlertAndHazardReviewRecordPage : CommonMethods
    {
        public PersonAlertAndHazardReviewRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By personAlertAndHazardReviewRecordFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personalertandhazardreview&')]");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personalertandhazard')]");

        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");

        readonly By save_Button = By.Id("TI_SaveButton");
        readonly By saveAndClose_Button = By.Id("TI_SaveAndCloseButton");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/div/div/button[@onclick='; return false;']");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By alertHazard_FieldHeader = By.Id("CWLabelHolder_personalertandhazardid");
        readonly By alertHazard_FieldLookupButton = By.Id("CWLookupBtn_personalertandhazardid"); 
        readonly By responsibleTeam_FieldHeader = By.Id("CWLabelHolder_ownerid");
        readonly By responsibleTeam_FieldLookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By plannedReviewDate_FieldHeader = By.Id("CWLabelHolder_plannedreviewdate");
        readonly By plannedReviewDate_Field = By.XPath("//*[@id='CWField_plannedreviewdate']");
        readonly By plannedReviewDate_FieldMessageArea = By.XPath("//*[@id='CWControlHolder_plannedreviewdate']/label/span");
        readonly By reviewOutcome_FieldHeader = By.Id("CWLabelHolder_alertandhazardreviewoutcomeid");
        readonly By reviewOutcome_FieldLookupButton = By.Id("CWLookupBtn_alertandhazardreviewoutcomeid");
        readonly By reviewDate_FieldHeader = By.Id("CWLabelHolder_reviewdate");
        readonly By reviewDate_Field = By.Id("CWField_reviewdate");
        readonly By reviewCompletedBy_FieldHeader = By.Id("CWField_reviewcompletedbyid");
        readonly By reviewCompletedBy_FieldLookupButton = By.Id("CWLookupBtn_reviewcompletedbyid");
        readonly By reviewCompletedBy_FieldRemoveButton = By.Id("CWClearLookup_reviewcompletedbyid");
        readonly By reviewCompletedBy_FieldMessageArea = By.XPath("//*[@id='CWControlHolder_reviewcompletedbyid']/label/span");
        readonly By personView_FieldHeader = By.Id("CWLabelHolder_personview");
        readonly By personview_Field = By.Id("CWField_personview");
        readonly By professionalView_FieldHeader = By.Id("CWLabelHolder_professionalview");
        readonly By professionalView_Field = By.Id("CWField_professionalview");
        readonly By summary_FieldHeader = By.Id("CWLabelHolder_summary");
        readonly By summary_Field = By.Id("CWField_summary");

        readonly By leftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By auditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        readonly By optionMenuToolBar = By.Id("CWToolbarMenu");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        
        public PersonAlertAndHazardReviewRecordPage WaitForPersonAlertAndHazardReviewsPageToLoad(String ReviewTitle)
        {
            this.SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

          
            WaitForElement(personAlertAndHazardReviewRecordFrame);
            SwitchToIframe(personAlertAndHazardReviewRecordFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);


            WaitForElement(pageHeader);

            ValidateElementTextContainsText(pageHeader, "Person Alert And Hazard Review:\r\n" + ReviewTitle);


            return this;
        }

        public PersonAlertAndHazardReviewRecordPage WaitForPersonAlertAndHazardReviewsFromAdvancedSearchPageToLoad()
        {
            this.SwitchToDefaultFrame();

            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);


            this.WaitForElement(iframe_CWDataFormDialog);
            this.SwitchToIframe(iframe_CWDataFormDialog);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);
     
           
            return this;
        }
        public PersonAlertAndHazardReviewRecordPage ValidateReviewDateFieldValueVisible(String ExpectedValue)
        {
            ValidateElementValue(reviewDate_Field, ExpectedValue);
                return this;
        }
        public PersonAlertAndHazardReviewRecordPage InsertPlannedReviewDate(String PlannedReviewDate)
        {
            
            SendKeys(plannedReviewDate_Field, PlannedReviewDate);

            return this;
        }

        public PersonAlertAndHazardReviewRecordPage ClickReviewCompletedByLookUp()
        {
            WaitForElementVisible(reviewCompletedBy_FieldLookupButton);
            Click(reviewCompletedBy_FieldLookupButton);

            return this;
        }

        public PersonAlertAndHazardReviewRecordPage ClickSaveButton()
        {
            WaitForElementVisible(save_Button);
            Click(save_Button);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonAlertAndHazardReviewRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementVisible(saveAndClose_Button);
            Click(saveAndClose_Button);

            return this;
        }




        public PersonAlertAndHazardReviewRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(notificationMessage);
            }
            else
            {
                WaitForElementNotVisible(notificationMessage, 3);
            }

            return this;
        }
        public PersonAlertAndHazardReviewRecordPage ValidateMessageAreaText(String ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

        public PersonAlertAndHazardReviewRecordPage ValidatePlannedReviewDateMessageArea(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(plannedReviewDate_FieldMessageArea);
            }
            else
            {
                WaitForElementNotVisible(plannedReviewDate_FieldMessageArea, 3);
            }

            return this;
        }


        public PersonAlertAndHazardReviewRecordPage ValidatePlannedReviewDateMessageAreaText(String ExpectedText)
        {
            ValidateElementText(plannedReviewDate_FieldMessageArea, ExpectedText);

            return this;
        }
        public PersonAlertAndHazardReviewRecordPage ValidateReviewCompletedByMessageArea(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(reviewCompletedBy_FieldMessageArea);
            }
            else
            {
                WaitForElementNotVisible(reviewCompletedBy_FieldMessageArea, 3);
            }

            return this;
        }


        public PersonAlertAndHazardReviewRecordPage ValidateReviewCompletedByMessageAreaText(String ExpectedText)
        {
            ValidateElementText(reviewCompletedBy_FieldMessageArea, ExpectedText);

            return this;
        }

        public PersonAlertAndHazardReviewRecordPage InsertReviewDate(String ReviewDate)
        {

            SendKeys(reviewDate_Field, ReviewDate);

            return this;
        }

        public PersonAlertAndHazardReviewRecordPage InsertSummary(String TextToInsert)
        {
            SendKeys(summary_Field, TextToInsert);

            return this;
        }

        public PersonAlertAndHazardReviewRecordPage ClickDeleteButton()
        {
            WaitForElement(optionMenuToolBar);
            Click(optionMenuToolBar);
            WaitForElementVisible(deleteButton);
            Click(deleteButton);
            return this;
        }

        public PersonAlertAndHazardReviewRecordPage ClickPersonAlertAndHazardIDLookUp()
        {
            WaitForElementVisible(alertHazard_FieldLookupButton);
            Click(alertHazard_FieldLookupButton);

            return this;
        }

        public PersonAlertAndHazardReviewRecordPage InsertPersonView(String TextToInsert)
        {
            SendKeys(personview_Field, TextToInsert);

            return this;
        }

        public PersonAlertAndHazardReviewRecordPage InsertProfessionalView(String TextToInsert)
        {
            SendKeys(professionalView_Field, TextToInsert);

            return this;
        }

        public PersonAlertAndHazardReviewRecordPage ClickReviewOutcomeFieldLookUp()
        {
            WaitForElementVisible(reviewOutcome_FieldLookupButton);
            Click(reviewOutcome_FieldLookupButton);

            return this;
        }
        public PersonAlertAndHazardReviewRecordPage NavigateToAuditSubPage()
        {
            WaitForElementToBeClickable(leftMenuButton);
            Click(leftMenuButton);

            WaitForElementToBeClickable(auditLink_LeftMenu);
            Click(auditLink_LeftMenu);

            return this;
        }


    }
}
