using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonEmailRecordPage : CommonMethods
    {

        public PersonEmailRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=email&')]");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By sendButton = By.Id("TI_SendButton");
        readonly By shareButton = By.Id("TI_ShareRecordButton");
        readonly By cloneButton = By.Id("TI_CloneRecordButton");

        readonly By additionalItemsMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By runOnDemandWorkflowButton = By.Id("TI_RunOnDemandWorkflow");


        readonly By attachmentsTab = By.XPath("//*[@id='CWNavGroup_Attachments']/a");

        readonly By fromId_Field = By.Id("CWLookupBtn_emailfromlookupid");
        readonly By fromIdField_Link = By.Id("CWField_emailfromlookupid_Link");
        readonly By fromId_Notification = By.XPath("//*[@id='CWControlHolder_emailfromlookupid']/label/span[text()='Please fill out this field.']");
        readonly By toId_Field = By.Id("CWLookupBtn_emailtoid");
        readonly By toId_Notificatiion = By.XPath("//*[@id='CWControlHolder_emailtoid']/label/span[text()='Please fill out this field.']");
        readonly By addRecords = By.Id("CWAddSelectedButton");
        readonly By ccField = By.Id("CWField_emailccid_List");
        readonly By ccField_LookUpButton = By.Id("CWLookupBtn_emailccid");

        readonly By subject_Field = By.XPath("//*[@id='CWField_subject']");
        readonly By subject_FieldHeader = By.XPath("//*[@id='CWLabelHolder_subject']/label[text()='Subject']");
        readonly By description_FieldHeader = By.XPath("//*[@id='CWLabelHolder_notes']/label[text()='Description']");
        readonly By regarding_FieldHeader = By.XPath("//*[@id='CWLabelHolder_regardingid']/label[text()='Regarding']");
        readonly By reason_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activityreasonid']/label[text()='Reason']");
        readonly By priority_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitypriorityid']/label[text()='Priority']");
        readonly By sentRecievedDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_letterdate']/label[text()='Sent/Received Date']");
        readonly By containsInformationProvidedByAThirdParty_FieldHeader = By.XPath("//*[@id='CWLabelHolder_informationbythirdparty']/label[text()='Contains Information Provided By A Third Party?']");
        readonly By responsibleTeam_FieldHeader = By.Id("CWControlHolder_ownerid");
        readonly By responsibleUser_FieldHeader = By.Id("CWLabelHolder_responsibleuserid");
        readonly By description_Field = By.Id("CWLabelHolder_notes");
        readonly By isCaseNote_FieldHeader = By.Id("CWLabelHolder_iscasenote");
        readonly By category_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitycategoryid']/label[text()='Category']");
        readonly By subCategory_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitysubcategoryid']/label[text()='Sub-Category']");
        readonly By outcome_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activityoutcomeid']/label[text()='Outcome']");
        readonly By significantEvent_FieldHeader = By.XPath("//*[@id='CWLabelHolder_issignificantevent']/label[text()='Significant Event?']");
        readonly By status_FieldHeader = By.Id("CWLabelHolder_statusid");
        readonly By careDirectorPopUp = By.XPath("//*[@id='CWDynamicDialog']/main/div[text()='CW Forms Test User 1 does not contain an email address.']");

        readonly By significantEvent_YesRadioButton = By.XPath("//*[@id='CWField_issignificantevent_1']");
        readonly By significantEvent_NoRadioButton = By.XPath("//*[@id='CWField_issignificantevent_0']");
        readonly By significantEventCategory_LookUpButton = By.Id("CWLookupBtn_significanteventcategoryid");
        readonly By significantEventDate = By.Id("CWField_significanteventdate");
        readonly By significantEventSubCategory_LookUpButton = By.Id("CWLookupBtn_significanteventsubcategoryid");
        readonly By regarding_LookupButton = By.XPath("//*[@id='CWLookupBtn_regardingid']");

        readonly By reason_LinkField = By.XPath("//*[@id='CWField_activityreasonid_Link']");
        readonly By reason_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityreasonid']");
        readonly By priority_LinkField = By.XPath("//*[@id='CWField_activitypriorityid_Link']");
        readonly By priority_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitypriorityid']");
        readonly By containsInformationProvidedByAThirdParty_YesRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_1']");
        readonly By containsInformationProvidedByAThirdParty_NoRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_0']");
        readonly By isCaseNote_YesRadioButton = By.XPath("//*[@id='CWField_iscasenote_1']");
        readonly By isCaseNote_NoRadioButton = By.XPath("//*[@id='CWField_iscasenote_0']");
        readonly By responsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By responsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By responsibleUser_LinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By responsibleUser_LookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
        readonly By category_LinkField = By.XPath("//*[@id='CWField_activitycategoryid_Link']");
        readonly By category_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitycategoryid']");
        readonly By subCategory_LinkField = By.XPath("//*[@id='CWField_activitysubcategoryid_Link']");
        readonly By subCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitysubcategoryid']");
        readonly By outcome_LinkField = By.XPath("//*[@id='CWField_activityoutcomeid_Link']");
        readonly By outcome_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityoutcomeid']");
        readonly By dueDate_Field = By.Id("CWField_duedate");
        readonly By protectiveMarkingScheme_Header = By.Id("CWLabelHolder_protectivemarkingschemeid");
        readonly By protectiveMarkingScheme_LookUp = By.Id("CWLookupBtn_protectivemarkingschemeid");

        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By AuditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");


        public PersonEmailRecordPage WaitForPersonEmailRecordPageToLoad(string EmailTitle)
        {
            driver.SwitchTo().DefaultContent();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);


            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            if (driver.FindElement(pageHeader).Text != "Email:\r\n" + EmailTitle)
                throw new Exception("Page title do not equals: Email: " + EmailTitle);

            return this;
        }

        public PersonEmailRecordPage WaitForPersonEmailRecordPageToLoadFromAdvanceSearch(string EmailTitle)
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

            WaitForElement(fromId_Field);
            WaitForElement(toId_Field);
            WaitForElement(ccField);
            WaitForElement(subject_FieldHeader);


            WaitForElement(description_FieldHeader);
            WaitForElement(regarding_FieldHeader);
            WaitForElement(priority_FieldHeader);
            WaitForElement(responsibleTeam_FieldHeader);
            WaitForElement(responsibleUser_FieldHeader);
            WaitForElement(isCaseNote_FieldHeader);
            WaitForElement(category_FieldHeader);
            WaitForElement(subCategory_FieldHeader);
            WaitForElement(reason_FieldHeader);
            WaitForElement(outcome_FieldHeader);
            WaitForElement(status_FieldHeader);
            WaitForElement(containsInformationProvidedByAThirdParty_FieldHeader);

            WaitForElement(significantEvent_FieldHeader);


            if (driver.FindElement(pageHeader).Text != "Email:\r\n" + EmailTitle)
                throw new Exception("Page title do not equals: Email: " + EmailTitle);

            return this;
        }


        public PersonEmailRecordPage NavigateToAttachmentsPage()
        {
            WaitForElementToBeClickable(attachmentsTab);
            Click(attachmentsTab);

            return this;
        }


        public PersonEmailRecordPage ValidateRunOnDemandWorkflowButtonVisible()
        {
            WaitForElementVisible(runOnDemandWorkflowButton);

            return this;
        }

        public PersonEmailRecordPage ValidateRunOnDemandWorkflowButtonNotVisible()
        {
            WaitForElementNotVisible(runOnDemandWorkflowButton, 7);

            return this;
        }


        public PersonEmailRecordPage ClickAdditionalItemsMenuButton()
        {
            if(GetElementVisibility(additionalItemsMenuButton))
                Click(additionalItemsMenuButton);

            return this;
        }

        public PersonEmailRecordPage ClickRunOnDemandWorkflowButton()
        {
            Click(runOnDemandWorkflowButton);

            return this;
        }

        public PersonEmailRecordPage ClickSaveButton()
        {
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonEmailRecordPage ClickSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            return this;
        }

        public PersonEmailRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }

        public PersonEmailRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }


        public PersonEmailRecordPage ClickFromIdLookUpButton()
        {
            WaitForElementToBeClickable(fromId_Field);
            Click(fromId_Field);

            return this;
        }

        public PersonEmailRecordPage ClickToIdLookUpButton()
        {
            WaitForElementToBeClickable(toId_Field);
            Click(toId_Field);

            return this;
        }

        public PersonEmailRecordPage ClickAddRecords()
        {
            Click(addRecords);

            return this;
        }

        public PersonEmailRecordPage InsertSubject(string Subject)
        {
            WaitForElement(subject_Field);
            SendKeys(subject_Field, Subject);

            return this;
        }

        public PersonEmailRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(NotificationMessage);
            }
            else
            {
                WaitForElementNotVisible(NotificationMessage, 3);
            }

            return this;
        }
        public PersonEmailRecordPage ValidateMessageAreaText(String ExpectedText)
        {
            ValidateElementText(NotificationMessage, ExpectedText);

            return this;
        }

        public PersonEmailRecordPage ValidateFromMessageAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(fromId_Notification);
            }
            else
            {
                WaitForElementNotVisible(fromId_Notification, 3);
            }

            return this;
        }
        public PersonEmailRecordPage ValidateFromMessageAreaText(String ExpectedText)
        {
            ValidateElementText(fromId_Notification, ExpectedText);

            return this;
        }

        public PersonEmailRecordPage ValidateToMessageAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(toId_Notificatiion);
            }
            else
            {
                WaitForElementNotVisible(toId_Notificatiion, 3);
            }

            return this;
        }
        public PersonEmailRecordPage ValidateToMessageAreaText(String ExpectedText)
        {
            ValidateElementText(toId_Notificatiion, ExpectedText);

            return this;
        }

        public PersonEmailRecordPage validateCareDirectorPopUpMessage(String ExpectedText)
        {
            ValidateElementText(careDirectorPopUp, ExpectedText);



            return this;
        }

        public PersonEmailRecordPage validateCareDirectorPopUp(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(careDirectorPopUp);
            }
            else
            {
                WaitForElementNotVisible(careDirectorPopUp, 3);
            }

            return this;
        }

        public PersonEmailRecordPage ValidateSubjectFieldText(String ExpectedText)
        {
            ValidateElementText(subject_Field, ExpectedText);

            return this;
        }

        public PersonEmailRecordPage ValidateFromFieldEmail(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(fromId_Field);
            }
            else
            {
                WaitForElementNotVisible(fromId_Field, 3);
            }

            return this;
        }

        public PersonEmailRecordPage ValidateFromFieldText(String ExpectedText)
        {
            WaitForElementVisible(fromIdField_Link);
            MoveToElementInPage(fromIdField_Link);
            ValidateElementText(fromIdField_Link, ExpectedText);

            return this;
        }

        public PersonEmailRecordPage ValidateCCAreaText(String ExpectedText)
        {
            ValidateElementText(ccField, ExpectedText);

            return this;
        }
        public PersonEmailRecordPage ClickCCLookUpButton()
        {
            WaitForElementToBeClickable(ccField_LookUpButton);
            Click(ccField_LookUpButton);

            return this;
        }

        public PersonEmailRecordPage ClickSignificantEvent_YesRadioButton()
        {
            WaitForElementToBeClickable(significantEvent_YesRadioButton);
            Click(significantEvent_YesRadioButton);

            return this;
        }

        public PersonEmailRecordPage ClickSignificantEvent_NoRadioButton()
        {
            WaitForElementToBeClickable(significantEvent_NoRadioButton);
            Click(significantEvent_NoRadioButton);

            return this;
        }

        public PersonEmailRecordPage ClickSignificantEventCategoryLookupButton()
        {
            WaitForElementToBeClickable(significantEventCategory_LookUpButton);
            Click(significantEventCategory_LookUpButton);

            return this;
        }
        public PersonEmailRecordPage InsertSignificantEventDate(string Date)
        {
            WaitForElement(significantEventDate);
            SendKeys(significantEventDate, Date);

            return this;
        }

        public PersonEmailRecordPage ClickSignificantEventSubCategoryLookupButton()
        {
            WaitForElementToBeClickable(significantEventSubCategory_LookUpButton);

            Click(significantEventSubCategory_LookUpButton);

            return this;
        }
        public PersonEmailRecordPage ClickReasonLookupButton()
        {
            WaitForElementToBeClickable(reason_LookupButton);
            Click(reason_LookupButton);

            return this;
        }

        public PersonEmailRecordPage ClickPriorityLookupButton()
        {
            WaitForElementToBeClickable(priority_LookupButton);
            Click(priority_LookupButton);

            return this;
        }
        public PersonEmailRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(responsibleTeam_LookupButton);
            Click(responsibleTeam_LookupButton);

            return this;
        }
        public PersonEmailRecordPage ClickResponsibleUserLookupButton()
        {
            WaitForElementToBeClickable(responsibleUser_LookupButton);
            Click(responsibleUser_LookupButton);

            return this;
        }
        public PersonEmailRecordPage ClickCategoryLookupButton()
        {
            WaitForElementToBeClickable(category_LookupButton);
            Click(category_LookupButton);

            return this;
        }
        public PersonEmailRecordPage ClickSubCategoryLookupButton()
        {
            WaitForElementToBeClickable(subCategory_LookupButton);
            Click(subCategory_LookupButton);

            return this;
        }
        public PersonEmailRecordPage ClickOutcomeLookupButton()
        {
            WaitForElementToBeClickable(outcome_LookupButton);
            Click(outcome_LookupButton);

            return this;
        }



        public PersonEmailRecordPage ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
        {
            WaitForElementToBeClickable(containsInformationProvidedByAThirdParty_YesRadioButton);
            Click(containsInformationProvidedByAThirdParty_YesRadioButton);

            return this;
        }
        public PersonEmailRecordPage ClickContainsInformationProvidedByAThirdParty_NoRadioButton()
        {
            WaitForElementToBeClickable(containsInformationProvidedByAThirdParty_NoRadioButton);
            Click(containsInformationProvidedByAThirdParty_NoRadioButton);

            return this;
        }

        public PersonEmailRecordPage InsertSentRecievedDate(string Date)
        {
            WaitForElementToBeClickable(dueDate_Field);
            SendKeys(dueDate_Field, Date);

            return this;
        }

        public PersonEmailRecordPage ClickRegardingLookupButton()
        {
            Click(regarding_LookupButton);

            return this;
        }

        public PersonEmailRecordPage NavigateToAuditSubPage()
        {
            WaitForElementToBeClickable(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementToBeClickable(AuditLink_LeftMenu);
            Click(AuditLink_LeftMenu);

            return this;
        }

        public PersonEmailRecordPage ClickSendEmailButton()
        {
            WaitForElementToBeClickable(sendButton);
            Click(sendButton);

            return this;
        }
        public PersonEmailRecordPage ClickProtectiveMarkingSchemeLookUp()
        {
            WaitForElementToBeClickable(protectiveMarkingScheme_LookUp);
            Click(protectiveMarkingScheme_LookUp);

            return this;
        }

        public PersonEmailRecordPage ValidateAdditionalItemsMenuButtonNotVisible()
        {
            WaitForElementNotVisible(additionalItemsMenuButton, 7);

            return this;
        }

    }
}
