using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonAlertandHazardsRecordPage : CommonMethods
    {
        public PersonAlertandHazardsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personalertandhazard')]");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By saveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By saveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By restrictAccessButton = By.Id("TI_RestrictAccessButton");
        readonly By back_Button = By.XPath("//button[@title = 'Back']");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");

        readonly By relatedPersonAlertAndHazards_Icon = By.XPath("//*[@id='CWBannerHolder']/ul/li/div/img[@title='Related Person Alerts/Hazards']");
        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");

        readonly By detailsTab = By.Id("CWNavGroup_EditForm");

        #region Fields

        readonly By person_FieldHeader = By.Id("CWLabelHolder_personid");
        readonly By person_FieldLookUpButton = By.Id("CWLookupBtn_personid");
        readonly By person_FieldRemoveButton = By.Id("CWClearLookup_personid");
        readonly By responsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid'']/label");
        readonly By responsibleTeam_Field = By.Id("CWField_ownerid_cwname");
        readonly By responsibleTeam_FieldLookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By responsibleTeam_FieldRemoveButton = By.Id("CWClearLookup_ownerid");
        readonly By role_FieldHeader = By.Id("//*[@id='CWLabelHolder_roleid']/label");
        readonly By role_Field = By.Id("CWField_roleid");
        readonly By role_FieldMessageArea = By.XPath("//*[@id='CWControlHolder_roleid']/label/span");
        readonly By reviewFrequency_FieldHeader = By.XPath("//*[@id='CWLabelHolder_reviewfrequencytypeid']/label");
        readonly By reviewFrequency_Field = By.Id("CWField_reviewfrequencytypeid");

        readonly By alertHazardsType_FieldHeader = By.XPath("//*[@id='CWLabelHolder_alertandhazardtypeid']/label");
        readonly By alertHazardsType_FieldLookUp = By.Id("CWLookupBtn_alertandhazardtypeid");
        readonly By alertHazardType_FieldMessageArea = By.XPath("//*[@id='CWControlHolder_alertandhazardtypeid']/label/span");

        readonly By alertHazardsEndReason_FieldHeader = By.XPath("//*[@id='CWLabelHolder_alertandhazardendreasonid']/label");
        readonly By alertHazardsEndReason_FieldLookUpButton = By.Id("CWLookupBtn_alertandhazardendreasonid");
        readonly By alertHazardsEndReason_FieldMessageArea = By.XPath("//*[@id='CWControlHolder_alertandhazardendreasonid']/label/span");

        readonly By endDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_enddate']/label");
        readonly By endDate_Field = By.Id("CWField_enddate");
        

        readonly By startDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By startDate_FieldMessageArea = By.XPath("//*[@id='CWControlHolder_startdate']/label/span");

        readonly By details_FieldHeader = By.XPath("//*[@id='CWLabelHolder_details']/label");
        readonly By details_Field = By.Id("CWField_details");

        readonly By reviewDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_reviewdate']/label");
        readonly By reviewDate_Field = By.Id("CWField_reviewdate");


        readonly By leftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By auditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");
        readonly By alertAndHazardReview = By.Id("CWNavItem_AlertAndHazardReview");
        readonly By detail_Field = By.Id("CWField_details");

        readonly By optionMenuToolBar = By.Id("CWToolbarMenu");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
       
        readonly By dynamicDialog = By.Id("CWDynamicDialog");
        readonly By alertsHazardsRecorded_Button = By.XPath("//*[@id='CWBannerHolder']/ul/li[2]/div[2]/img[@title='Alerts/Hazards recorded']");
       
        #endregion

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");

        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
       
        By record(String RecordID) => By.XPath("//*[@id='CHK_" + RecordID + "']");


        public PersonAlertandHazardsRecordPage WaitForPersonAlertandHazardsRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            return this;
        }

        public PersonAlertandHazardsRecordPage WaitForPersonAlertAndHazardRecordPageToLoadFromAdvanceSearch(string AlertAndHazardsTitle)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDataFormDialog);
            SwitchToIframe(iframe_CWDataFormDialog);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            if (driver.FindElement(pageHeader).Text != "Person Alert And Hazard:\r\n" + AlertAndHazardsTitle)
                throw new Exception("Page title do not equals:Person Alert And Hazard:\r\n" + AlertAndHazardsTitle);

            return this;
        }

        public PersonAlertandHazardsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public PersonAlertandHazardsRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(notificationMessage);
            else
                WaitForElementNotVisible(notificationMessage, 3);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateMessageAreaText(String ExpectedText)
        {
            WaitForElementVisible(notificationMessage);
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateRoleMessageAreaText(String ExpectedText)
        {
            WaitForElementVisible(notificationMessage);
            ValidateElementText(role_FieldMessageArea, ExpectedText);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateRoleMessageArea(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(role_FieldMessageArea);
            else
                WaitForElementNotVisible(role_FieldMessageArea, 3);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateAlertHazardTypeMessageAreaText(String ExpectedText)
        {
            WaitForElementVisible(alertHazardType_FieldMessageArea);
            ValidateElementText(alertHazardType_FieldMessageArea, ExpectedText);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateAlertHazardTypeMessageArea(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(alertHazardType_FieldMessageArea);
            else
                WaitForElementNotVisible(alertHazardType_FieldMessageArea, 3);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateStartDateMessageAreaText(String ExpectedText)
        {
            WaitForElementVisible(startDate_FieldMessageArea);
            ValidateElementText(startDate_FieldMessageArea, ExpectedText);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateStartDateMessageArea(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(startDate_FieldMessageArea);
            else
                WaitForElementNotVisible(startDate_FieldMessageArea, 3);

            return this;
        }

        public PersonAlertandHazardsRecordPage InsertFutureEndDate(string TextToInsert)
        {
            WaitForElement(endDate_Field);
            SendKeys(endDate_Field, TextToInsert);

            return this;
        }

        public PersonAlertandHazardsRecordPage InsertStartDate(string SearchQuery)
        {
            WaitForElement(startDate_Field);
            SendKeys(startDate_Field, SearchQuery);

            return this;
        }

        public PersonAlertandHazardsRecordPage SelectRole(String OptionToSelect)
        {
            WaitForElementVisible(role_Field);
            SelectPicklistElementByValue(role_Field, OptionToSelect);

            return this;
        }
        
        public PersonAlertandHazardsRecordPage ClickAlertAndHazardsLookUp()
        {
            WaitForElementToBeClickable(alertHazardsType_FieldLookUp);
            MoveToElementInPage(alertHazardsType_FieldLookUp);
            Click(alertHazardsType_FieldLookUp);

            return this;
        }

        public PersonAlertandHazardsRecordPage ClickRestrictAccessRecord()
        {
            WaitForElementToBeClickable(restrictAccessButton);
            MoveToElementInPage(restrictAccessButton);
            Click(restrictAccessButton);

            return this;
        }

        public PersonAlertandHazardsRecordPage NavigateToAuditSubPage()
        {
            WaitForElementToBeClickable(leftMenuButton);
            Click(leftMenuButton);

            WaitForElementToBeClickable(auditLink_LeftMenu);
            Click(auditLink_LeftMenu);

            return this;
        }

        public PersonAlertandHazardsRecordPage NavigateToAlertAndHazardReviewSubpage()
        {
            WaitForElementToBeClickable(leftMenuButton);
            Click(leftMenuButton);

            WaitForElementToBeClickable(alertAndHazardReview);
            Click(alertAndHazardReview);

            return this;
        }

        public PersonAlertandHazardsRecordPage ClickDetailsTab()
        {
            WaitForElementToBeClickable(detailsTab);
            MoveToElementInPage(detailsTab);
            Click(detailsTab);

            return this;
        }

        public PersonAlertandHazardsRecordPage InsertReviewDate(String Texttoinsert)
        {
            WaitForElement(reviewDate_Field);
            MoveToElementInPage(reviewDate_Field);
            SendKeys(reviewDate_Field, Texttoinsert);

            return this;
        }

        public PersonAlertandHazardsRecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElement(endDate_Field);
            SendKeys(endDate_Field, TextToInsert);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateAlertHazardEndReasonArea(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(alertHazardsEndReason_FieldMessageArea);
            else
                WaitForElementNotVisible(alertHazardsEndReason_FieldMessageArea, 3);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateAlertHazardEndReasonAreaText(String ExpectedText)
        {
            WaitForElementVisible(alertHazardsEndReason_FieldMessageArea);
            ValidateElementText(alertHazardsEndReason_FieldMessageArea, ExpectedText);

            return this;
        }

        public PersonAlertandHazardsRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(optionMenuToolBar);
            MoveToElementInPage(optionMenuToolBar);
            Click(optionMenuToolBar);

            WaitForElementToBeClickable(deleteButton);
            MoveToElementInPage(deleteButton);
            Click(deleteButton);

            return this;
        }
        
        public PersonAlertandHazardsRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(back_Button);
            MoveToElementInPage(back_Button);
            Click(back_Button);

            return this;
        }

        public PersonAlertandHazardsRecordPage SelectPersonAlertAndHazardRecord(string RecordId)
        {
            WaitForElementToBeClickable(record(RecordId));
            MoveToElementInPage(record(RecordId));
            Click(record(RecordId));

            return this;
        }

        public PersonAlertandHazardsRecordPage OpenPersonAlertAndHazardsRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            MoveToElementInPage(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public PersonAlertandHazardsRecordPage InsertDetails(String TextToInsert)
        {
            WaitForElementToBeClickable(detail_Field);
            MoveToElementInPage(detail_Field);
            SendKeys(detail_Field, TextToInsert);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateAlertHazardRecordedButton(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(alertsHazardsRecorded_Button);
            else
                WaitForElementNotVisible(alertsHazardsRecorded_Button, 3);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateRelatedPersonAlertHazardButton(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(alertsHazardsRecorded_Button);
            else
                WaitForElementNotVisible(alertsHazardsRecorded_Button, 3);

            return this;
        }

        public PersonAlertandHazardsRecordPage ClickPersonLookUpButton()
        {
            WaitForElementToBeClickable(person_FieldLookUpButton);
            MoveToElementInPage(person_FieldLookUpButton);
            Click(person_FieldLookUpButton);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidatePersonRelatedAlertsAndHazards_Icon(bool ExpectedText)
        {
            if (ExpectedText)
                WaitForElementVisible(relatedPersonAlertAndHazards_Icon);
            else
                WaitForElementNotVisible(relatedPersonAlertAndHazards_Icon, 5);

            return this;
        }

        public PersonAlertandHazardsRecordPage ValidateEndDate(String ExpectedText)
        {
            WaitForElementVisible(endDate_Field);
            ValidateElementText(endDate_Field, ExpectedText);

            return this;
        }

        public PersonAlertandHazardsRecordPage SelectReviewFrequency(String OptionToSelect)
        {
            WaitForElementVisible(reviewFrequency_Field);
            SelectPicklistElementByValue(reviewFrequency_Field, OptionToSelect);

            return this;
        }

        public PersonAlertandHazardsRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 50);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(restrictAccessButton);
            WaitForElementVisible(assignRecordButton);

            return this;
        }

    }
}
