using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonPhoneCallRecordPage : CommonMethods
    {

        public PersonPhoneCallRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=phonecall&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By PersonTopBanner_SummaryItem_CWInfoLeft = By.XPath("//*[@id='CWBannerHolder']/ul/li[@id='CWSummaryItem']/div[@id='CWInfoLeft']");
        readonly By PersonTopBanner_SummaryItem_CWInfoRight = By.XPath("//*[@id='CWBannerHolder']/ul/li[@id='CWSummaryItem']/div[@id='CWInfoRight']");
        readonly By PersonTopBanner_SummaryItem_ExpandButton = By.XPath("//*[@id='CWBannerHolder']/ul/li[@id='CWSummaryItem']/div[@id='CWButton']");


        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By cloneButton = By.Id("TI_CloneRecordButton");
        readonly By activateButton = By.Id("TI_ActivateButton");
        readonly By completeButton = By.Id("TI_CompleteRecordButton");
        readonly By cancelButton = By.Id("TI_CancelRecordButton");


        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");



        #region Headers


        readonly By Caller_FieldHeader = By.XPath("//*[@id='CWLabelHolder_callerid']/label[text()='Caller']");
        readonly By PhoneNumber_FieldHeader = By.XPath("//*[@id='CWLabelHolder_phonenumber']/label[text()='Phone Number']");
        readonly By Recipient_FieldHeader = By.XPath("//*[@id='CWLabelHolder_recipientid']/label[text()='Recipient']");
        readonly By Direction_FieldHeader = By.XPath("//*[@id='CWLabelHolder_directionid']/label[text()='Direction']");
        readonly By Status_FieldHeader = By.XPath("//*[@id='CWLabelHolder_statusid']/label[text()='Status']");
        readonly By Subject_FieldHeader = By.XPath("//*[@id='CWLabelHolder_subject']/label[text()='Subject']");
        readonly By Description_FieldHeader = By.XPath("//*[@id='CWLabelHolder_notes']/label[text()='Description']");

        readonly By Regarding_FieldHeader = By.XPath("//*[@id='CWLabelHolder_regardingid']/label[text()='Regarding']");
        readonly By Reason_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activityreasonid']/label[text()='Reason']");
        readonly By Priority_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitypriorityid']/label[text()='Priority']");
        readonly By PhoneCallDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_phonecalldate']/label[text()='Phone Call Date']");
        readonly By ContainsInformationProvidedByAThirdParty_FieldHeader = By.XPath("//*[@id='CWLabelHolder_informationbythirdparty']/label[text()='Contains Information Provided By A Third Party?']");
        readonly By IsCaseNote_FieldHeader = By.XPath("//*[@id='CWLabelHolder_iscasenote']/label[text()='Is Case Note?']");
        readonly By ResponsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By ResponsibleUser_FieldHeader = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label[text()='Responsible User']");
        readonly By Category_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitycategoryid']/label[text()='Category']");
        readonly By SubCategory_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitysubcategoryid']/label[text()='Sub-Category']");
        readonly By Outcome_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activityoutcomeid']/label[text()='Outcome']");

        readonly By SignificantEvent_FieldHeader = By.XPath("//*[@id='CWLabelHolder_issignificantevent']/label[text()='Significant Event?']");
        readonly By EventDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_significanteventdate']/label[text()='Event Date']");
        readonly By EventCategory_FieldHeader = By.XPath("//*[@id='CWLabelHolder_significanteventcategoryid']/label[text()='Event Category']");
        readonly By EventSubCategory_FieldHeader = By.XPath("//*[@id='CWLabelHolder_significanteventsubcategoryid']/label[text()='Event Sub Category']");

        #endregion

        #region Fields

        readonly By Caller_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_callerid']/label/span");
        readonly By Caller_LinkField = By.XPath("//*[@id='CWField_callerid_Link']");
        readonly By Caller_LookupButton = By.XPath("//*[@id='CWLookupBtn_callerid']");
        readonly By Caller_RemoveButton = By.XPath("//*[@id='CWClearLookup_callerid']");
        readonly By PhoneNumber_Field = By.XPath("//*[@id='CWField_phonenumber']");
        readonly By Recipient_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_recipientid']/label/span");
        readonly By Recipient_LinkField = By.XPath("//*[@id='CWField_recipientid_Link']");
        readonly By Recipient_LookupButton = By.XPath("//*[@id='CWLookupBtn_recipientid']");
        readonly By Recipient_RemoveButton = By.XPath("//*[@id='CWClearLookup_recipientid']");
        readonly By Direction_Field = By.XPath("//*[@id='CWField_directionid']");
        readonly By Direction_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_directionid']/label/span");
        readonly By Status_Field = By.XPath("//*[@id='CWField_statusid']");
        readonly By Status_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_statusid']/label/span");
        readonly By Subject_Field = By.XPath("//*[@id='CWField_subject']");
        readonly By Subject_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_subject']/label/span");
        readonly By Description_Field = By.XPath("//*[@id='CWField_notes']");

        readonly By Regarding_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_regardingid']/label/span");
        readonly By Regarding_LinkField = By.XPath("//*[@id='CWField_regardingid_Link']");
        readonly By Regarding_LookupButton = By.XPath("//*[@id='CWLookupBtn_regardingid']");
        readonly By Regarding_RemoveButton = By.XPath("//*[@id='CWClearLookup_regardingid']");
        readonly By Reason_LinkField = By.XPath("//*[@id='CWField_activityreasonid_Link']");
        readonly By Reason_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityreasonid']");
        readonly By Reason_RemoveButton = By.XPath("//*[@id='CWClearLookup_activityreasonid']");
        readonly By Priority_LinkField = By.XPath("//*[@id='CWField_activitypriorityid_Link']");
        readonly By Priority_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitypriorityid']");
        readonly By Priority_RemoveButton = By.XPath("//*[@id='CWClearLookup_activitypriorityid']");
        readonly By PhoneCallDate_DateField = By.XPath("//*[@id='CWField_phonecalldate']");
        readonly By PhoneCallDate_TimeField = By.XPath("//*[@id='CWField_phonecalldate_Time']");
        readonly By ContainsInformationProvidedByAThirdParty_YesRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_1']");
        readonly By ContainsInformationProvidedByAThirdParty_NoRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_0']");
        readonly By IsCaseNote_YesRadioButton = By.XPath("//*[@id='CWField_iscasenote_1']");
        readonly By IsCaseNote_NoRadioButton = By.XPath("//*[@id='CWField_iscasenote_0']");
        readonly By ResponsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ResponsibleTeam_RemoveButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By ResponsibleUser_LinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By ResponsibleUser_LookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
        readonly By ResponsibleUser_RemoveButton = By.XPath("//*[@id='CWClearLookup_responsibleuserid']");
        readonly By Category_LinkField = By.XPath("//*[@id='CWField_activitycategoryid_Link']");
        readonly By Category_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitycategoryid']");
        readonly By Category_RemoveButton = By.XPath("//*[@id='CWClearLookup_activitycategoryid']");
        readonly By SubCategory_LinkField = By.XPath("//*[@id='CWField_activitysubcategoryid_Link']");
        readonly By SubCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitysubcategoryid']");
        readonly By SubCategory_RemoveButton = By.XPath("//*[@id='CWClearLookup_activitysubcategoryid']");
        readonly By Outcome_LinkField = By.XPath("//*[@id='CWField_activityoutcomeid_Link']");
        readonly By Outcome_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityoutcomeid']");
        readonly By Outcome_RemoveButton = By.XPath("//*[@id='CWClearLookup_activityoutcomeid']");


        readonly By SignificantEvent_YesRadioButton = By.XPath("//*[@id='CWField_issignificantevent_1']");
        readonly By SignificantEvent_NoRadioButton = By.XPath("//*[@id='CWField_issignificantevent_0']");
        readonly By EventDate_Field = By.XPath("//*[@id='CWField_significanteventdate']");
        readonly By EventDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_significanteventdate']/label/span");
        readonly By EventCategory_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_significanteventcategoryid']/label/span");
        readonly By EventCategory_LinkField = By.XPath("//*[@id='CWField_significanteventcategoryid_Link']");
        readonly By EventCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_significanteventcategoryid']");
        readonly By EventCategory_RemoveButton = By.XPath("//*[@id='CWClearLookup_significanteventcategoryid']");
        readonly By EventSubCategory_LinkField = By.XPath("//*[@id='CWField_significanteventsubcategoryid_Link']");
        readonly By EventSubCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_significanteventsubcategoryid']");
        readonly By EventSubCategory_RemoveButton = By.XPath("//*[@id='CWClearLookup_significanteventsubcategoryid']");


        #endregion


        #region Menu

        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By AuditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");


        #endregion



        public PersonPhoneCallRecordPage WaitForPersonPhoneCallRecordPageToLoad(string PhoneCallTitle)
        {
            SwitchToDefaultFrame();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(PersonTopBanner_SummaryItem_CWInfoLeft);
            WaitForElement(PersonTopBanner_SummaryItem_CWInfoRight);
            WaitForElement(PersonTopBanner_SummaryItem_ExpandButton);


            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(Subject_FieldHeader);
            WaitForElement(Recipient_FieldHeader);
            WaitForElement(Direction_FieldHeader);
            WaitForElement(Description_FieldHeader);


            WaitForElement(PhoneCallDate_FieldHeader);
            WaitForElement(Regarding_FieldHeader);
            WaitForElement(Priority_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ResponsibleUser_FieldHeader);
            WaitForElement(IsCaseNote_FieldHeader);
            WaitForElement(Category_FieldHeader);
            WaitForElement(SubCategory_FieldHeader);
            WaitForElement(Reason_FieldHeader);
            WaitForElement(Outcome_FieldHeader);
            WaitForElement(Status_FieldHeader);
            WaitForElement(ContainsInformationProvidedByAThirdParty_FieldHeader);

            WaitForElement(SignificantEvent_FieldHeader);


            if (driver.FindElement(pageHeader).Text != "Phone Call:\r\n" + PhoneCallTitle)
                throw new Exception("Page title do not equals: Phone Call: " + PhoneCallTitle);

            return this;
        }

        public PersonPhoneCallRecordPage WaitForPersonPhoneCallRecordPageToLoadFromAdvanceSearch(string PhoneCallTitle)
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

            WaitForElement(Subject_FieldHeader);
            WaitForElement(Recipient_FieldHeader);
            WaitForElement(Direction_FieldHeader);
            WaitForElement(Description_FieldHeader);


            WaitForElement(PhoneCallDate_FieldHeader);
            WaitForElement(Regarding_FieldHeader);
            WaitForElement(Priority_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ResponsibleUser_FieldHeader);
            WaitForElement(IsCaseNote_FieldHeader);
            WaitForElement(Category_FieldHeader);
            WaitForElement(SubCategory_FieldHeader);
            WaitForElement(Reason_FieldHeader);
            WaitForElement(Outcome_FieldHeader);
            WaitForElement(Status_FieldHeader);
            WaitForElement(ContainsInformationProvidedByAThirdParty_FieldHeader);

            WaitForElement(SignificantEvent_FieldHeader);


            if (driver.FindElement(pageHeader).Text != "Phone Call:\r\n" + PhoneCallTitle)
                throw new Exception("Page title do not equals: Phone Call: " + PhoneCallTitle);

            return this;
        }

        public PersonPhoneCallRecordPage WaitForInactivePersonPhoneCallRecordPageToLoad(string PhoneCallTitle)
        {
            SwitchToDefaultFrame();


            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(PersonTopBanner_SummaryItem_CWInfoLeft);
            WaitForElement(PersonTopBanner_SummaryItem_CWInfoRight);
            WaitForElement(PersonTopBanner_SummaryItem_ExpandButton);


            WaitForElementNotVisible(saveButton, 3);
            WaitForElementNotVisible(saveAndCloseButton, 3);

            WaitForElement(Caller_FieldHeader);
            WaitForElement(PhoneNumber_FieldHeader);
            WaitForElement(Recipient_FieldHeader);
            WaitForElement(Direction_FieldHeader);
            WaitForElement(Status_FieldHeader);
            WaitForElement(Subject_FieldHeader);
            WaitForElement(Description_FieldHeader);

            WaitForElement(PhoneCallDate_FieldHeader);
            WaitForElement(Regarding_FieldHeader);
            WaitForElement(Priority_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ResponsibleUser_FieldHeader);
            WaitForElement(IsCaseNote_FieldHeader);
            WaitForElement(Category_FieldHeader);
            WaitForElement(SubCategory_FieldHeader);
            WaitForElement(Reason_FieldHeader);
            WaitForElement(Outcome_FieldHeader);
            WaitForElement(Status_FieldHeader);
            WaitForElement(ContainsInformationProvidedByAThirdParty_FieldHeader);

            WaitForElement(SignificantEvent_FieldHeader);


            if (driver.FindElement(pageHeader).Text != "Phone Call:\r\n" + PhoneCallTitle)
                throw new Exception("Page title do not equals: Phone Call: " + PhoneCallTitle);

            ValidateElementDisabled(Caller_LookupButton);
            ValidateElementDisabled(PhoneNumber_Field);
            ValidateElementDisabled(Recipient_LookupButton);
            ValidateElementDisabled(Direction_Field);
            ValidateElementDisabled(Status_Field);
            ValidateElementDisabled(Subject_Field);
            ValidateElementDisabled(PhoneCallDate_DateField);
            ValidateElementDisabled(PhoneCallDate_TimeField);
            ValidateElementDisabled(Regarding_LookupButton);
            ValidateElementDisabled(Priority_LookupButton);
            ValidateElementDisabled(ResponsibleTeam_LookupButton);
            ValidateElementDisabled(ResponsibleUser_LookupButton);
            ValidateElementDisabled(IsCaseNote_YesRadioButton);
            ValidateElementDisabled(IsCaseNote_NoRadioButton);
            ValidateElementDisabled(Category_LookupButton);
            ValidateElementDisabled(SubCategory_LookupButton);
            ValidateElementDisabled(Reason_LookupButton);
            ValidateElementDisabled(Outcome_LookupButton);
            ValidateElementDisabled(Status_Field);
            ValidateElementDisabled(ContainsInformationProvidedByAThirdParty_YesRadioButton);
            ValidateElementDisabled(ContainsInformationProvidedByAThirdParty_NoRadioButton);
            ValidateElementDisabled(SignificantEvent_YesRadioButton);
            ValidateElementDisabled(SignificantEvent_NoRadioButton);

            return this;
        }


        public PersonPhoneCallRecordPage ValidateEventDateFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(EventDate_FieldHeader);
                WaitForElementVisible(EventDate_Field);
            }
            else
            {
                WaitForElementNotVisible(EventDate_FieldHeader, 3);
                WaitForElementNotVisible(EventDate_Field, 3);
            }

            return this;
        }
        public PersonPhoneCallRecordPage ValidateEventCategoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(EventCategory_FieldHeader);
                WaitForElementVisible(EventCategory_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(EventCategory_FieldHeader, 3);
                WaitForElementNotVisible(EventCategory_LookupButton, 3);
            }

            return this;
        }
        public PersonPhoneCallRecordPage ValidateEventSubCategoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(EventSubCategory_FieldHeader);
                WaitForElementVisible(EventSubCategory_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(EventSubCategory_FieldHeader, 3);
                WaitForElementNotVisible(EventSubCategory_LookupButton, 3);
            }

            return this;
        }
        public PersonPhoneCallRecordPage ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NotificationMessage);
            else
                WaitForElementNotVisible(NotificationMessage, 3);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateCallerErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Caller_FieldErrorLabel);
            else
                WaitForElementNotVisible(Caller_FieldErrorLabel, 3);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateRecipientErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Recipient_FieldErrorLabel);
            else
                WaitForElementNotVisible(Recipient_FieldErrorLabel, 3);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateDirectionErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Direction_FieldErrorLabel);
            else
                WaitForElementNotVisible(Direction_FieldErrorLabel, 3);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateStatusErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Status_FieldErrorLabel);
            else
                WaitForElementNotVisible(Status_FieldErrorLabel, 3);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateSubjectErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Subject_FieldErrorLabel);
            else
                WaitForElementNotVisible(Subject_FieldErrorLabel, 3);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateRegardingErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Regarding_FieldErrorLabel);
            else
                WaitForElementNotVisible(Regarding_FieldErrorLabel, 3);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateResponsibleTeamErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_FieldErrorLabel);
            else
                WaitForElementNotVisible(ResponsibleTeam_FieldErrorLabel, 3);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateEventDateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EventDate_FieldErrorLabel);
            else
                WaitForElementNotVisible(EventDate_FieldErrorLabel, 3);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateEventCategoryErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EventCategory_FieldErrorLabel);
            else
                WaitForElementNotVisible(EventCategory_FieldErrorLabel, 3);

            return this;
        }



        public PersonPhoneCallRecordPage ValidateNotificationMessageText(string ExpectText)
        {
            ValidateElementText(NotificationMessage, ExpectText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateCallerFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Caller_LinkField, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidatePhoneNumber(string ExpectedText)
        {
            ValidateElementValue(PhoneNumber_Field, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateRecipientFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Recipient_LinkField, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateDirection(string ExpectedText)
        {
            ValidatePicklistSelectedText(Direction_Field, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateSubject(string ExpectedText)
        {
            ValidateElementValue(Subject_Field, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateDescription(string ExpectedText)
        {
            SetElementDisplayStyleToInline("CWField_notes");
            SetElementVisibilityStyleToVisible("CWField_notes");

            ValidateElementValue(Description_Field, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateCallerErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Caller_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateRecipientErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Recipient_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateDirectionErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Direction_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateStatusErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Status_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateSubjectErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Subject_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateRegardingErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Regarding_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateResponsibleTeamErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateEventDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EventDate_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateEventCategoryErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EventCategory_FieldErrorLabel, ExpectedText);

            return this;
        }



        public PersonPhoneCallRecordPage ValidateRegardingFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Regarding_LinkField, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateReasonFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Reason_LinkField, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidatePriorityFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Priority_LinkField, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidatePhoneCallDate(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(PhoneCallDate_DateField, ExpectedDate);
            ValidateElementValue(PhoneCallDate_TimeField, ExpectedTime);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateOutcomeFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Outcome_LinkField, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateContainsInformationProvidedByAThirdPartyCheckedOption(bool YesOptionChecked)
        {
            if (YesOptionChecked)
            {
                ValidateElementChecked(ContainsInformationProvidedByAThirdParty_YesRadioButton);
                ValidateElementNotChecked(ContainsInformationProvidedByAThirdParty_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ContainsInformationProvidedByAThirdParty_YesRadioButton);
                ValidateElementChecked(ContainsInformationProvidedByAThirdParty_NoRadioButton);
            }

            return this;
        }
        public PersonPhoneCallRecordPage ValidateIsCaseNoteCheckedOption(bool YesOptionChecked)
        {
            if (YesOptionChecked)
            {
                ValidateElementChecked(IsCaseNote_YesRadioButton);
                ValidateElementNotChecked(IsCaseNote_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(IsCaseNote_YesRadioButton);
                ValidateElementChecked(IsCaseNote_NoRadioButton);
            }

            return this;
        }
        public PersonPhoneCallRecordPage ValidateResponsibleTeamFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateResponsibleUserFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleUser_LinkField, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateCategoryFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Category_LinkField, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateSubCategoryFieldLinkText(string ExpectedText)
        {
            ValidateElementText(SubCategory_LinkField, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateStatus(string ExpectedText)
        {
            ValidatePicklistSelectedText(Status_Field, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateEventDate(string ExpectedDate)
        {
            ValidateElementValue(EventDate_Field, ExpectedDate);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateEventCategoryFieldLinkText(string ExpectedText)
        {
            ValidateElementText(EventCategory_LinkField, ExpectedText);

            return this;
        }
        public PersonPhoneCallRecordPage ValidateEventSubCategoryFieldLinkText(string ExpectedText)
        {
            ValidateElementText(EventSubCategory_LinkField, ExpectedText);

            return this;
        }


        public PersonPhoneCallRecordPage ValidateSignificantEventCheckedOption(bool YesOptionChecked)
        {
            if (YesOptionChecked)
            {
                ValidateElementChecked(SignificantEvent_YesRadioButton);
                ValidateElementNotChecked(SignificantEvent_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(SignificantEvent_YesRadioButton);
                ValidateElementChecked(SignificantEvent_NoRadioButton);
            }

            return this;
        }




        public PersonPhoneCallRecordPage InsertPhoneNumber(string Subject)
        {
            SendKeys(PhoneNumber_Field, Subject);

            return this;
        }
        public PersonPhoneCallRecordPage InsertSubject(string Subject)
        {
            SendKeys(Subject_Field, Subject);

            return this;
        }
        public PersonPhoneCallRecordPage InsertDescription(string Subject)
        {
            SetElementDisplayStyleToInline("CWField_notes");
            SetElementVisibilityStyleToVisible("CWField_notes");

            SendKeys(Description_Field, Subject);

            return this;
        }
        public PersonPhoneCallRecordPage InsertPhoneCallDate(string Date, string Time)
        {
            SendKeys(PhoneCallDate_DateField, Date);
            SendKeysWithoutClearing(PhoneCallDate_DateField, Keys.Tab);
            SendKeys(PhoneCallDate_TimeField, Time);

            return this;
        }
        public PersonPhoneCallRecordPage InsertEventDate(string Date)
        {
            SendKeys(EventDate_Field, Date);

            return this;
        }




        public PersonPhoneCallRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(Status_Field, TextToSelect);

            return this;
        }
        public PersonPhoneCallRecordPage SelectDirection(string TextToSelect)
        {
            SelectPicklistElementByText(Direction_Field, TextToSelect);

            return this;
        }


        public PersonPhoneCallRecordPage ClickCallerLookupButton()
        {
            Click(Caller_LookupButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickCallerRemoveButton()
        {
            Click(Caller_RemoveButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickRecipientLookupButton()
        {
            Click(Recipient_LookupButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickRecipientRemoveButton()
        {
            Click(Recipient_RemoveButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickRegardingLookupButton()
        {
            Click(Regarding_LookupButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickRegardingRemoveButton()
        {
            Click(Regarding_RemoveButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickReasonLookupButton()
        {
            Click(Reason_LookupButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickReasonRemoveButton()
        {
            Click(Reason_RemoveButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickPriorityLookupButton()
        {
            Click(Priority_LookupButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickPriorityRemoveButton()
        {
            Click(Priority_RemoveButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(ResponsibleTeam_LookupButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickResponsibleTeamRemoveButton()
        {
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickResponsibleUserLookupButton()
        {
            Click(ResponsibleUser_LookupButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickResponsibleUserRemoveButton()
        {
            Click(ResponsibleUser_RemoveButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickCategoryLookupButton()
        {
            Click(Category_LookupButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickCategoryRemoveButton()
        {
            Click(Category_RemoveButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickSubCategoryLookupButton()
        {
            Click(SubCategory_LookupButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickSubCategoryRemoveButton()
        {
            Click(SubCategory_RemoveButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickOutcomeLookupButton()
        {
            Click(Outcome_LookupButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickOutcomeRemoveButton()
        {
            Click(Outcome_RemoveButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickEventCategoryLookupButton()
        {
            Click(EventCategory_LookupButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickEventCategoryRemoveButton()
        {
            Click(EventCategory_RemoveButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickEventSubCategoryLookupButton()
        {
            Click(EventSubCategory_LookupButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickEventSubCategoryRemoveButton()
        {
            Click(EventSubCategory_RemoveButton);

            return this;
        }



        public PersonPhoneCallRecordPage ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
        {
            Click(ContainsInformationProvidedByAThirdParty_YesRadioButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickContainsInformationProvidedByAThirdParty_NoRadioButton()
        {
            Click(ContainsInformationProvidedByAThirdParty_NoRadioButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickIsCaseNote_YesRadioButton()
        {
            Click(IsCaseNote_YesRadioButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickIsCaseNote_NoRadioButton()
        {
            Click(IsCaseNote_NoRadioButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickSignificantEvent_YesRadioButton()
        {
            Click(SignificantEvent_YesRadioButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickSignificantEvent_NoRadioButton()
        {
            Click(SignificantEvent_NoRadioButton);

            return this;
        }



        public PersonPhoneCallRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public PersonPhoneCallRecordPage ClickSaveAndCloseButton()
        {
            Click(saveAndCloseButton);
            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }
        public PersonPhoneCallRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }

        public PersonPhoneCallRecordPage ClickDeleteButton()
        {
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public PersonPhoneCallRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickActivateButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(activateButton);
            Click(activateButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickCompleteButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(completeButton);
            Click(completeButton);

            return this;
        }
        public PersonPhoneCallRecordPage ClickCancelButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);

            return this;
        }

        public PersonPhoneCallRecordPage NavigateToAuditSubPage()
        {
            WaitForElementToBeClickable(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementToBeClickable(AuditLink_LeftMenu);
            Click(AuditLink_LeftMenu);

            return this;
        }
    }
}
