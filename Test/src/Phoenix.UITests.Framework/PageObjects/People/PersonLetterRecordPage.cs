using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonLetterRecordPage : CommonMethods
    {

        public PersonLetterRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=letter&')]");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");

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
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By Assign_ResponsibleTeamLookUp = By.Id("CWOwnerLookupButton");
        readonly By cancelButton = By.Id("TI_CancelRecordButton");
      


        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");



        #region Headers

        
        readonly By Sender_FieldHeader = By.XPath("//*[@id='CWLabelHolder_senderid']/label[text()='Sender']");
        readonly By Sender_FieldMessageErrorLabel = By.XPath("//*[@id='CWControlHolder_senderid']/label/span");
        
        readonly By Address_FieldHeader = By.XPath("//*[@id='CWLabelHolder_address']/label[text()='Address']");
        readonly By Recipient_FieldHeader = By.XPath("//*[@id='CWLabelHolder_recipientid']/label[text()='Recipient']");
        readonly By recipient_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_recipientid']/label/span");
        readonly By Direction_FieldHeader = By.XPath("//*[@id='CWLabelHolder_directionid']/label[text()='Direction']");
        readonly By Direction_FieldOption = By.Id("CWField_directionid");
        readonly By Status_FieldHeader = By.XPath("//*[@id='CWLabelHolder_statusid']/label[text()='Status']");

        readonly By Subject_FieldHeader = By.XPath("//*[@id='CWLabelHolder_subject']/label[text()='Subject']");
        readonly By Subject_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_subject']/label/span");
        readonly By Description_FieldHeader = By.XPath("//*[@id='CWLabelHolder_notes']/label[text()='Description']");
        readonly By Regarding_FieldHeader = By.XPath("//*[@id='CWLabelHolder_regardingid']/label[text()='Regarding']");
        readonly By Reason_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activityreasonid']/label[text()='Reason']");
        readonly By Priority_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitypriorityid']/label[text()='Priority']");
        readonly By SentRecievedDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_letterdate']/label[text()='Sent/Received Date']");
        readonly By ContainsInformationProvidedByAThirdParty_FieldHeader = By.XPath("//*[@id='CWLabelHolder_informationbythirdparty']/label[text()='Contains Information Provided By A Third Party?']");
        readonly By ContainsInformationProvidedByAThirdParty_YesOption = By.XPath("//*[@id='CWField_informationbythirdparty_1']");
        readonly By ContainsInformationProvidedByAThirdParty_NoOption = By.XPath("//*[@id='CWField_informationbythirdparty_0']");
      
        readonly By IsCaseNote_FieldHeader = By.XPath("//*[@id='CWLabelHolder_iscasenote']/label[text()='Is Case Note?']");
        readonly By IsCaseNote_YesOption = By.Id("CWField_iscasenote_1");
        readonly By IsCaseNote_NoOption = By.Id("CWField_iscasenote_0");
        readonly By ResponsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By ResponsibleUser_FieldHeader = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label[text()='Responsible User']");
        readonly By Category_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitycategoryid']/label[text()='Category']");
        readonly By SubCategory_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activitysubcategoryid']/label[text()='Sub-Category']");
        readonly By Outcome_FieldHeader = By.XPath("//*[@id='CWLabelHolder_activityoutcomeid']/label[text()='Outcome']");

        readonly By SignificantEvent_FieldHeader = By.XPath("//*[@id='CWLabelHolder_issignificantevent']/label[text()='Significant Event?']");
        readonly By EventDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_significanteventdate']/label[text()='Event Date']");
        readonly By EventDate_Field = By.XPath("//*[@id='CWField_significanteventdate']");
        readonly By EventCategory_FieldHeader = By.XPath("//*[@id='CWLabelHolder_significanteventcategoryid']/label[text()='Event Category']");
        readonly By EventSubCategory_FieldHeader = By.XPath("//*[@id='CWLabelHolder_significanteventsubcategoryid']/label[text()='Event Sub Category']");

        readonly By SignificantEvent_YesOption = By.Id("CWField_issignificantevent_1");
        readonly By SignificantEvent_NoOption = By.Id("CWField_issignificantevent_0");


        readonly By File_Field = By.Id("CWField_fileid_Upload");
        readonly By File_Browse = By.Id("CWField_fileid");
        readonly By File_Upload = By.Id("CWField_fileid_UploadButton");
        readonly By File_FieldLink = By.Id("CWField_fileid_FileLink");
        
        #endregion

        #region 


        readonly By Sender_LinkField = By.XPath("//*[@id='CWField_senderid_Link']");
        readonly By Sender_Field = By.XPath("//*[@id='CWControlHolder_senderid']/div/div[2]");
        readonly By Sender_LookupButton = By.XPath("//*[@id='CWLookupBtn_senderid']");
        readonly By Sender_MandatoryField = By.XPath("//*[@id='CWLabelHolder_senderid']/label/span[@class ='mandatory']");
        readonly By Address_Field = By.XPath("//*[@id='CWField_address']");
        readonly By Recipient_Field = By.XPath("//*[@id='CWField_recipientid_cwname']"); 
        readonly By Recipient_FieldLookUp = By.Id("CWLookupBtn_recipientid");
        readonly By Direction_Field = By.XPath("//*[@id='CWField_directionid']");
        readonly By Status_Field = By.XPath("//*[@id='CWField_statusid']");
        readonly By Subject_Field = By.XPath("//*[@id='CWField_subject']");
        readonly By Description_Field = By.XPath("//*[@id='CWField_notes']");
        readonly By Description_LinkField = By.XPath("//*[@id='cke_1_contents']/iframe");

        readonly By Regarding_LinkField = By.XPath("//*[@id='CWField_regardingid_Link']");
        readonly By Regarding_LookupButton = By.XPath("//*[@id='CWLookupBtn_regardingid']");
        readonly By Reason_LinkField = By.XPath("//*[@id='CWField_activityreasonid_Link']");
        readonly By Reason_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityreasonid']"); 
        readonly By Priority_LinkField = By.XPath("//*[@id='CWField_activitypriorityid_Link']");
        readonly By Priority_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitypriorityid']");
        readonly By SentRecievedDate_DateField = By.XPath("//*[@id='CWField_letterdate']");
        readonly By ContainsInformationProvidedByAThirdParty_YesRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_1']");
        readonly By ContainsInformationProvidedByAThirdParty_NoRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_0']");
        readonly By IsCaseNote_YesRadioButton = By.XPath("//*[@id='CWField_iscasenote_1']");
        readonly By IsCaseNote_NoRadioButton = By.XPath("//*[@id='CWField_iscasenote_0']");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ResponsibleUser_LinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By ResponsibleUser_LookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
        readonly By Category_LinkField = By.XPath("//*[@id='CWField_activitycategoryid_Link']");
        readonly By Category_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitycategoryid']");
        readonly By SubCategory_LinkField = By.XPath("//*[@id='CWField_activitysubcategoryid_Link']");
        readonly By SubCategory_LookupButton = By.XPath("//*[@id='CWLookupBtn_activitysubcategoryid']");
        readonly By Outcome_LinkField = By.XPath("//*[@id='CWField_activityoutcomeid_Link']");
        readonly By Outcome_LookupButton = By.XPath("//*[@id='CWLookupBtn_activityoutcomeid']");


        readonly By SignificantEvent_YesRadioButton = By.XPath("//*[@id='CWField_issignificantevent_1']");
        readonly By SignificantEvent_NoRadioButton = By.XPath("//*[@id='CWField_issignificantevent_0']");
        readonly By SignificantEventCategory_LookUpButton = By.Id("CWLookupBtn_significanteventcategoryid");
        readonly By SignificantEventDate = By.Id("CWField_significanteventdate");
        readonly By SignificantEventSubCategory_LookUpButton = By.Id("CWLookupBtn_significanteventsubcategoryid");

        readonly By IsCloned_YesOption = By.Id("CWField_iscloned_1");
        readonly By IsCloned_NoOption = By.Id("CWField_iscloned_0");

        readonly By ClonedForm_LookUp = By.Id("CWLookupBtn_clonedfromid");

        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By AuditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        #endregion






        public PersonLetterRecordPage WaitForPersonLetterRecordPageToLoad(string LetterTitle)
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


            WaitForElement(SentRecievedDate_FieldHeader);
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


            if (driver.FindElement(pageHeader).Text != "Letter:\r\n" + LetterTitle)
                throw new Exception("Page title do not equals: Letter: " + LetterTitle);

            return this;
        }

        public PersonLetterRecordPage WaitForPersonLetterRecordPageToLoadFromAdvanceSearch(string LetterTitle)
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


            WaitForElement(SentRecievedDate_FieldHeader);
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


            if (driver.FindElement(pageHeader).Text != "Letter:\r\n" + LetterTitle)
                throw new Exception("Page title do not equals: Letter: " + LetterTitle);

            return this;
        }

        public PersonLetterRecordPage WaitForInactivePersonLetterRecordPageToLoad(string LetterTitle)
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


            WaitForElement(SentRecievedDate_FieldHeader);
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


            if (driver.FindElement(pageHeader).Text != "Letter:\r\n" + LetterTitle)
                throw new Exception("Page title do not equals: Letter: " + LetterTitle);




            ValidateElementDisabled(Sender_Field);
            ValidateElementDisabled(Recipient_FieldHeader);
            ValidateElementDisabled(Direction_Field);
            ValidateElementDisabled(Status_Field);
            ValidateElementDisabled(Subject_Field);
          
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



        public PersonLetterRecordPage ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NotificationMessage);
            else
                WaitForElementNotVisible(NotificationMessage, 3);

            return this;
        }
        public PersonLetterRecordPage ValidateNotificationMessageText(string ExpectText)
        {
            ValidateElementText(NotificationMessage, ExpectText);

            return this;
        }
        public PersonLetterRecordPage ValidateSenderMandatoryVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Sender_MandatoryField);
            else
                WaitForElementNotVisible(Sender_MandatoryField, 3);

            return this;
        }


        public PersonLetterRecordPage ValidateSubject(string ExpectedText)
        {
            ValidateElementValue(Subject_Field, ExpectedText);

            return this;
        }
        public PersonLetterRecordPage ValidateRecipient(string ExpectedText)
        {
            ValidateElementText(Recipient_Field, ExpectedText);

            return this;
        }
        public PersonLetterRecordPage ValidateDirection(string ExpectedText)
        {
            ValidatePicklistSelectedText(Direction_Field, ExpectedText);

            return this;
        }
        public PersonLetterRecordPage ValidateDescription(string ExpectedText)
        {
            SetElementDisplayStyleToInline("CWField_notes");
            SetElementVisibilityStyleToVisible("CWField_notes");

            ValidateElementValue(Description_Field, ExpectedText);

            return this;
        }


        public PersonLetterRecordPage ValidateRegardingFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Regarding_LinkField, ExpectedText);

            return this;
        }
        public PersonLetterRecordPage ValidateReasonFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Reason_LinkField, ExpectedText);

            return this;
        }
        public PersonLetterRecordPage ValidatePriorityFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Priority_LinkField, ExpectedText);

            return this;
        }
        public PersonLetterRecordPage ValidateSentRecievedDate(string ExpectedDate)
        {
            ValidateElementValue(SentRecievedDate_DateField, ExpectedDate);

            return this;
        }
        public PersonLetterRecordPage ValidateOutcomeFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Outcome_LinkField, ExpectedText);

            return this;
        }
        public PersonLetterRecordPage ValidateContainsInformationProvidedByAThirdPartyCheckedOption(bool YesOptionChecked)
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
        public PersonLetterRecordPage ValidateIsCaseNoteCheckedOption(bool YesOptionChecked)
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
        public PersonLetterRecordPage ValidateResponsibleTeamFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public PersonLetterRecordPage ValidateResponsibleUserFieldLinkText(string ExpectedText)
        {
            ValidateElementText(ResponsibleUser_LinkField, ExpectedText);

            return this;
        }
        public PersonLetterRecordPage ValidateCategoryFieldLinkText(string ExpectedText)
        {
            ValidateElementText(Category_LinkField, ExpectedText);

            return this;
        }
        public PersonLetterRecordPage ValidateSubCategoryFieldLinkText(string ExpectedText)
        {
            ValidateElementText(SubCategory_LinkField, ExpectedText);

            return this;
        }
        public PersonLetterRecordPage ValidateStatus(string ExpectedText)
        {
            ValidatePicklistSelectedText(Status_Field, ExpectedText);

            return this;
        }
        

        public PersonLetterRecordPage ValidateSignificantEventCheckedOption(bool YesOptionChecked)
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





        public PersonLetterRecordPage InsertSubject(string Subject)
        {
            SendKeys(Subject_Field, Subject);
            
            return this;
        }
        public PersonLetterRecordPage InsertDescription(string Subject)
        {
            SetElementDisplayStyleToInline("CWField_notes");
            SetElementVisibilityStyleToVisible("CWField_notes");

            SendKeys(Description_Field, Subject);

            return this;
        }
        public PersonLetterRecordPage InsertSentRecievedDate(string Date)
        {
            SendKeys(SentRecievedDate_DateField, Date);

            return this;
        }




        public PersonLetterRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(Status_Field, TextToSelect);

            return this;
        }


        public PersonLetterRecordPage ClickRegardingLookupButton()
        {
            Click(Regarding_LookupButton);

            return this;
        }
        public PersonLetterRecordPage ClickReasonLookupButton()
        {
            Click(Reason_LookupButton);

            return this;
        }
        public PersonLetterRecordPage ClickPriorityLookupButton()
        {
            Click(Priority_LookupButton);

            return this;
        }
        public PersonLetterRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(ResponsibleTeam_LookupButton);

            return this;
        }
        public PersonLetterRecordPage ClickResponsibleUserLookupButton()
        {
            Click(ResponsibleUser_LookupButton);

            return this;
        }
        public PersonLetterRecordPage ClickCategoryLookupButton()
        {
            Click(Category_LookupButton);

            return this;
        }
        public PersonLetterRecordPage ClickSubCategoryLookupButton()
        {
            Click(SubCategory_LookupButton);

            return this;
        }
        public PersonLetterRecordPage ClickOutcomeLookupButton()
        {
            Click(Outcome_LookupButton);

            return this;
        }


        public PersonLetterRecordPage ClickSenderLookupButton()
        {
            Click(Sender_LookupButton);

            return this;
        }



        public PersonLetterRecordPage ClickContainsInformationProvidedByAThirdParty_YesRadioButton()
        {
            Click(ContainsInformationProvidedByAThirdParty_YesRadioButton);

            return this;
        }
        public PersonLetterRecordPage ClickContainsInformationProvidedByAThirdParty_NoRadioButton()
        {
            Click(ContainsInformationProvidedByAThirdParty_NoRadioButton);

            return this;
        }
        public PersonLetterRecordPage ClickSignificantEvent_YesRadioButton()
        {
            Click(SignificantEvent_YesRadioButton);

            return this;
        }
        public PersonLetterRecordPage ClickSignificantEvent_NoRadioButton()
        {
            Click(SignificantEvent_NoRadioButton);

            return this;
        }



        public PersonLetterRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }
        public PersonLetterRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public PersonLetterRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            return this;
        }
        public PersonLetterRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }

        public PersonLetterRecordPage ClickDeleteButton()
        {
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public PersonLetterRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }

        public PersonLetterRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
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
        public PersonLetterRecordPage ValidateMessageAreaText(String ExpectedText)
        {
            ValidateElementText(NotificationMessage, ExpectedText);

            return this;
        }

        public PersonLetterRecordPage ValidateRecipientErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(recipient_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(recipient_FieldErrorLabel, 3);
            }

            return this;
        }

        public PersonLetterRecordPage ValidateRecipientMessageAreaText(String ExpectedText)
        {
            ValidateElementText(recipient_FieldErrorLabel, ExpectedText);

            return this;
        }

        public PersonLetterRecordPage ValidateSubjectErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Subject_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Subject_FieldErrorLabel, 3);
            }

            return this;
        }

        public PersonLetterRecordPage ValidateSubjectErrorLabelText(String ExpectedText)
        {
            ValidateElementText(Subject_FieldErrorLabel, ExpectedText);

            return this;
        }

        public PersonLetterRecordPage SelectDirection(String TextToSelect)
        {
            SelectPicklistElementByText(Direction_FieldOption, TextToSelect);

            return this;
        }
        public PersonLetterRecordPage ClickRecipientLookUp()
        {
            Click(Recipient_FieldLookUp);

            return this;
        }

       
            public PersonLetterRecordPage ValidateSubjectFieldText(String ExpectedText)
        {
            ValidateElementValue(Subject_Field, ExpectedText);
            return this;
        }

        public PersonLetterRecordPage ValidateRecipientLookUpText(String ExpectedText)
        {
            ValidateElementValue(Recipient_Field, ExpectedText);
            return this;
        }

        public PersonLetterRecordPage ValidateDirectionFieldTextValue(String ExpectedText)
        {
            ValidateElementValue(Direction_Field, ExpectedText);
            return this;
        }

        public PersonLetterRecordPage ValidateStatusFieldText(String ExpectedText)
        {
            SelectPicklistElementByText(Status_Field, ExpectedText);
            return this;
        }

        public PersonLetterRecordPage ValidateSenderMessageAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Sender_FieldMessageErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Sender_FieldMessageErrorLabel, 3);
            }

            return this;
        }
        public PersonLetterRecordPage ValidateSenderMessageAreaText(String ExpectedText)
        {
            ValidateElementText(Sender_FieldMessageErrorLabel, ExpectedText);
            return this;
        }

        public PersonLetterRecordPage InsertAddress(string Address)
        {
            SendKeys(Address_Field, Address);

            return this;
        }

        public PersonLetterRecordPage ClickSignificantEventCategoryLookupButton()
        {
            Click(SignificantEventCategory_LookUpButton);

            return this;
        }


        public PersonLetterRecordPage InsertSignificantEventDate(string Date)
        {
            SendKeys(SignificantEventDate, Date);

            return this;
        }


        public PersonLetterRecordPage ClickSignificantEventSubCategoryLookupButton()
        {
            Click(SignificantEventSubCategory_LookUpButton);

            return this;
        }

        public PersonLetterRecordPage IsEditableFields_Subject()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(Subject_Field);
            if (Subject_NonEditable_Fields.Enabled)
            {
                SendKeys(Subject_Field, "Editable");
            }
            
            return this;
        }

        public PersonLetterRecordPage IsEditableFields_Sender()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(Sender_LookupButton);
            if (Subject_NonEditable_Fields.Enabled)
            {
                SendKeys(Sender_LookupButton, "Editable");
            }
           
            return this;
        }

        public PersonLetterRecordPage IsEditableFields_Address()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(Address_Field);
            if (Subject_NonEditable_Fields.Enabled)
            {
                SendKeys(Address_Field, "Editable");
            }
            return this;
           
        }

        public PersonLetterRecordPage IsEditableFields_Recipient()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(Recipient_FieldLookUp);
            if (Subject_NonEditable_Fields.Enabled)
            {
                Click(Recipient_FieldLookUp);
            }
            return this;

        }
        public PersonLetterRecordPage IsEditableFields_Direction()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(Direction_Field);
            if (Subject_NonEditable_Fields.Enabled)
            {
                SendKeys(Direction_Field, "Editable");
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_Description(bool ExpectedDisable)
        {if(ExpectedDisable)
            {
                ValidateElementDisabled(Description_Field);
            }
            else
            {
                ValidateElementDisabled(Description_Field);
            }
          
            return this;
        }

        public PersonLetterRecordPage IsEditableFields_Regarding()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(Regarding_LookupButton);
            if (Subject_NonEditable_Fields.Enabled)
            {
               
                Click(Regarding_LookupButton);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_ResponsibleTeam()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(ResponsibleTeam_LookupButton);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(ResponsibleTeam_LookupButton);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_ResponsibleUser()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(ResponsibleUser_LookupButton);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(ResponsibleUser_LookupButton);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_Reason()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(Reason_LookupButton);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(Reason_LookupButton);
            }
            return this;

        }
        public PersonLetterRecordPage IsEditableFields_Priority()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(Priority_LookupButton);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(Priority_LookupButton);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_Category()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(Category_LookupButton);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(Category_LookupButton);
            }
            return this;

        }
        public PersonLetterRecordPage IsEditableFields_SubCategory()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(SubCategory_LookupButton);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(SubCategory_LookupButton);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_SentReceiveDate()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(SentRecievedDate_DateField);
            if (Subject_NonEditable_Fields.Enabled)
            {
                SendKeys(SentRecievedDate_DateField, "Editable");
            }
            return this;

        }
        public PersonLetterRecordPage IsEditableFields_Outcome()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(Outcome_LookupButton);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(Outcome_LookupButton);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_OutCome()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(Outcome_LookupButton);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(Outcome_LookupButton);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_ContainsInformationProvidedByAThirdParty_Yes()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(ContainsInformationProvidedByAThirdParty_YesOption);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(ContainsInformationProvidedByAThirdParty_YesOption);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_ContainsInformationProvidedByAThirdParty_No()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(ContainsInformationProvidedByAThirdParty_NoOption);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(ContainsInformationProvidedByAThirdParty_NoOption);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_IsCaseNote_Yes()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(IsCaseNote_YesOption);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(IsCaseNote_YesOption);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_IsCaseNote_No()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(IsCaseNote_NoOption);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(IsCaseNote_NoOption);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_SignificantEvent_Yes()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(SignificantEvent_YesOption);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(SignificantEvent_YesOption);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_SignificantEvent_No()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(SignificantEvent_NoOption);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(SignificantEvent_NoOption);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_IsCloned_Yes()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(IsCloned_YesOption);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(IsCloned_YesOption);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_IsCloned_No()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(IsCloned_NoOption);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(IsCloned_NoOption);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_ClonedForm()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(ClonedForm_LookUp);
            if (Subject_NonEditable_Fields.Enabled)
            {

                Click(ClonedForm_LookUp);
            }
            return this;

        }

        public PersonLetterRecordPage IsEditableFields_File()
        {
            IWebElement Subject_NonEditable_Fields = driver.FindElement(File_Field);
            if (Subject_NonEditable_Fields.Enabled)
            {

                WaitForElementToBeClickable(File_Field);
                
            }
            return this;

        }

        public PersonLetterRecordPage ClickActivateButton()
        {
            Click(additionalItemsButton);

            WaitForElementToBeClickable(activateButton);
            Click(activateButton);

            driver.SwitchTo().Alert().Accept();

           
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public PersonLetterRecordPage ClickFileIcon()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(File_Field);
            Click(File_Field);

            return this;
        }

      

      public PersonLetterRecordPage ClickFileUpload()
        {

            WaitForElement(File_Upload);
            Click(File_Upload);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        /*public PersonLetterRecordPage ClickFile1UploadDocument()
        {


            IWebElement upload_file = driver.FindElement(File_Browse);

            Click(File_Browse);
            upload_file.SendKeys("C:\\Temp\\DocToUpload.txt");

            return this;
        }*/

        public PersonLetterRecordPage ClickFile1UploadDocument(string FilePath)
        {
            SendKeys(File_Browse, FilePath);

           
            return this;
        }


      
        public PersonLetterRecordPage ValidateLatestFileLink(bool ExpectedFileLink)
        {
            if (ExpectedFileLink)
            {
                WaitForElementVisible(File_FieldLink);
            }
            else
            {
                WaitForElementNotVisible(File_FieldLink, 5);
            }
            return this;
        }

        public PersonLetterRecordPage ClickAssignButton()
        {
            Click(AssignRecordButton);

            return this;
        }

        public PersonLetterRecordPage ClickAssignResponsibleTeam()

        {
          
            Click(Assign_ResponsibleTeamLookUp);

            return this;
        }


        public PersonLetterRecordPage ValidateEventDateFieldVisibility(bool ExpectVisible)
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
        public PersonLetterRecordPage ValidateEventCategoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(EventCategory_FieldHeader);
                WaitForElementVisible(SignificantEventCategory_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(EventCategory_FieldHeader, 3);
                WaitForElementNotVisible(SignificantEventCategory_LookUpButton, 3);
            }

            return this;
        }
        public PersonLetterRecordPage ValidateEventSubCategoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(EventSubCategory_FieldHeader);
                WaitForElementVisible(SignificantEventSubCategory_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(EventSubCategory_FieldHeader, 3);
                WaitForElementNotVisible(SignificantEventSubCategory_LookUpButton, 3);
            }

            return this;
        }

        public PersonLetterRecordPage ClickCancelButton()
        {

            Click(additionalItemsButton);
           Click(cancelButton );

            return this;
        }


        public PersonLetterRecordPage NavigateToAuditSubPage()
        {
            WaitForElementToBeClickable(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementToBeClickable(AuditLink_LeftMenu);
            Click(AuditLink_LeftMenu);

            return this;
        }

        public PersonLetterRecordPage ValidateLatestFileLinkText(String ExpectedFileLinkText)
        {
            ValidateElementText(File_FieldLink, ExpectedFileLinkText);

            return this;
        }

    }



}
