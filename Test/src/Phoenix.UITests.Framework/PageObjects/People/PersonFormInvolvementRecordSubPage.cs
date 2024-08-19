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
    public class PersonFormInvolvementRecordSubPage : CommonMethods
    {
        public PersonFormInvolvementRecordSubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By newPersonFormInvolvementIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personforminvolvement&')]");
       

        #endregion



        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By editAssessmentButton = By.Id("TI_EditAssessmentButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By involvementMemberLookupBtn = By.Id("CWLookupBtn_involvementmemberid");
        readonly By involvementMemberRoleLookupBtn = By.Id("CWLookupBtn_involvementroleid");

        readonly By generalSectionTitle = By.XPath("//div[@id='CWSection_General']/fieldset/div/span[text()='General']");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By CaseNotesLink_LeftMenu = By.XPath("//*[@id='CWNavItem_PersonFormCaseNote']");
        readonly By AppointmentsLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Appointment']");
        readonly By EmailsLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Email']");
        readonly By LettersLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Letter']");
        readonly By PhoneCallsLink_LeftMenu = By.XPath("//*[@id='CWNavItem_PhoneCall']");
        readonly By TasksLink_LeftMenu = By.XPath("//*[@id='CWNavItem_Task']");

        readonly By relatedItemsLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_RelatedItems']/a");
        readonly By PersonFormInvolvementLeftSubMenuItem= By.XPath("//*[@id='CWNavItem_PersonFormInvolment']");

        #region Field Labels

        readonly By personFormFieldLabel = By.XPath("//li[@id='CWLabelHolder_personformid']/following-sibling::li");
        readonly By InvolvementMemberFieldLabel = By.XPath("//*[@id='CWField_involvementmemberid_Link']");
        readonly By CreatedOnPortal_YesRadioButton = By.XPath("//*[@id='CWField_createdonportal_1']");
        readonly By CreatedOnPortal_NoRadioButton = By.XPath("//*[@id='CWField_createdonportal_0']");


        #endregion

        #region Fields

        readonly By personField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_personid_Link']");
        readonly By personClearButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWClearLookup_personid']");
        readonly By personLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_personid']");

        readonly By formTypeField = By.XPath("//li[@id='CWControlHolder_documentid']/div/div/a[@id='CWField_documentid_Link']");
        readonly By formTypeClearButton = By.XPath("//li[@id='CWControlHolder_documentid']/div/div/button[@id='CWClearLookup_documentid']");
        readonly By formTypeLookupButton = By.XPath("//li[@id='CWControlHolder_documentid']/div/div/button[@id='CWLookupBtn_documentid']");

        readonly By statusPicklist = By.XPath("//select[@id='CWField_assessmentstatusid']");

        readonly By startDateField = By.XPath("//input[@id='CWField_startdate']");

        readonly By PrecedingFormLinkField = By.XPath("//li[@id='CWControlHolder_precedingformid']/div/div/a[@id='CWField_precedingformid_Link']");
        readonly By PrecedingFormClearButton = By.XPath("//li[@id='CWControlHolder_precedingformid']/div/div/button[@id='CWClearLookup_precedingformid']");
        readonly By PrecedingFormLookupButton = By.XPath("//li[@id='CWControlHolder_precedingformid']/div/div/button[@id='CWLookupBtn_precedingformid']");

        readonly By CreatedOnProviderPortal_YesRadioButton = By.XPath("//input[@id='CWField_createdonproviderportal_1']");
        readonly By CreatedOnProviderPortal_NoRadioButton = By.XPath("//input[@id='CWField_createdonproviderportal_0']");

        readonly By responsibleTeamField = By.XPath("//li[@id='CWControlHolder_ownerid']/div/div/a[@id='CWField_ownerid_Link']");
        readonly By responsibleTeamClearButton = By.XPath("//li[@id='CWControlHolder_ownerid']/div/div/button[@id='CWClearLookup_ownerid']");
        readonly By responsibleTeamLookupButton = By.XPath("//li[@id='CWControlHolder_ownerid']/div/div/button[@id='CWLookupBtn_ownerid']");

        readonly By responsibleUserField = By.XPath("//li[@id='CWControlHolder_responsibleuserid']/div/div/a[@id='CWField_responsibleuserid_Link']");
        readonly By responsibleUserClearButton = By.XPath("//li[@id='CWControlHolder_responsibleuserid']/div/div/button[@id='CWClearLookup_responsibleuserid']");
        readonly By responsibleUserLookupButton = By.XPath("//li[@id='CWControlHolder_responsibleuserid']/div/div/button[@id='CWLookupBtn_responsibleuserid']");

        readonly By dueDateField = By.XPath("//input[@id='CWField_duedate']");

        readonly By reviewDateField = By.XPath("//input[@id='CWField_reviewdate']");

        readonly By SourceCaseField = By.XPath("//li[@id='CWControlHolder_caseid']/div/div/a[@id='CWField_caseid_Link']");
        readonly By SourceCaseClearButton = By.XPath("//li[@id='CWControlHolder_caseid']/div/div/button[@id='CWClearLookup_caseid']");
        readonly By SourceCaseLookupButton = By.XPath("//li[@id='CWControlHolder_caseid']/div/div/button[@id='CWLookupBtn_caseid']");

        readonly By cancelledReasonField = By.XPath("//li[@id='CWControlHolder_formcancellationreasonid']/div/div/a[@id='CWField_formcancellationreasonid_Link']");
        readonly By cancelledReasonClearButton = By.XPath("//li[@id='CWControlHolder_formcancellationreasonid']/div/div/button[@id='CWClearLookup_formcancellationreasonid']");
        readonly By cancelledReasonLookupButton = By.XPath("//li[@id='CWControlHolder_formcancellationreasonid']/div/div/button[@id='CWLookupBtn_formcancellationreasonid']");

        readonly By CompletedByField = By.XPath("//li[@id='CWControlHolder_completedbyid']/div/div/a[@id='CWField_completedbyid_Link']");
        readonly By CompletedByClearButton = By.XPath("//li[@id='CWControlHolder_completedbyid']/div/div/button[@id='CWClearLookup_completedbyid']");
        readonly By CompletedByLookupButton = By.XPath("//li[@id='CWControlHolder_completedbyid']/div/div/button[@id='CWLookupBtn_completedbyid']");

        readonly By CompletionDateField = By.XPath("//input[@id='CWField_completiondate']");

        readonly By SignedOffByField = By.XPath("//li[@id='CWControlHolder_signedoffbyid']/div/div/a[@id='CWField_signedoffbyid_Link']");
        readonly By SignedOffByClearButton = By.XPath("//li[@id='CWControlHolder_signedoffbyid']/div/div/button[@id='CWClearLookup_signedoffbyid']");
        readonly By SignedOffByLookupButton = By.XPath("//li[@id='CWControlHolder_signedoffbyid']/div/div/button[@id='CWLookupBtn_signedoffbyid']");

        readonly By SignedOffDateField = By.XPath("//input[@id='CWField_signoffdate']");

        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        #endregion

        #region Notification and Error messages

        By WarningMainArea(string ExpactedText) => By.XPath("//div[@id='CWNotificationHolder_DataForm']/div[@id='CWNotificationMessage_DataForm'][text()='" + ExpactedText + "']");
        By personIDErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_personid']/label/span[text()='" + ExpactedText + "']");
        By FormTypeErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_documentid']/label/span[text()='" + ExpactedText + "']");
        By startDateErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_startdate']/label/span[text()='" + ExpactedText + "']");
        By responsibleTeamErrorArea(string ExpactedText) => By.XPath("//li[@id='CWControlHolder_ownerid']/label/span[text()='" + ExpactedText + "']");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");
        By RecordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        #endregion

        public PersonFormInvolvementRecordSubPage WaitForPersonFormInvolvementRecordSubPageToLoad(String Text)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(newPersonFormInvolvementIFrame);
            SwitchToIframe(newPersonFormInvolvementIFrame);

            WaitForElement(pageHeader);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);


            if (driver.FindElement(pageHeader).Text != "Person Form Involvement:\r\n" + Text)
                throw new Exception("Page title do not equals: Person Form Involvement: " + Text);

            return this;
        }

        public PersonFormInvolvementRecordSubPage WaitForPersonFormInvolvementRecordSubPageToLoadWithnoHeader()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(newPersonFormInvolvementIFrame);
            SwitchToIframe(newPersonFormInvolvementIFrame);

            WaitForElement(pageHeader);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);



            return this;
        }

        public PersonFormInvolvementRecordSubPage ValidatePersonFormField(string ExpectedText)
        {
            ValidateElementTextContainsText(personFormFieldLabel, ExpectedText);
            
            return this;
        }

        public PersonFormInvolvementRecordSubPage ValidateInvolvementMemberField(string ExpectedText)
        {
            ValidateElementText(InvolvementMemberFieldLabel, ExpectedText);

            return this;
        }

        public PersonFormInvolvementRecordSubPage ValidateCreatedOnPortalCheckedOption(bool NoOptionChecked)
        {
            if (NoOptionChecked)
            {
                ValidateElementChecked(CreatedOnPortal_NoRadioButton);
                ValidateElementNotChecked(CreatedOnPortal_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(CreatedOnPortal_NoRadioButton);
                ValidateElementChecked(CreatedOnPortal_YesRadioButton);
            }

            return this;
        }

        public PersonFormInvolvementRecordSubPage ClickInvolvementMemberLookupButton()
        {
            Click(involvementMemberLookupBtn);

            return this;
        }

        public PersonFormInvolvementRecordSubPage ClickInvolvementMemberRoleLookupButton()
        {
            Click(involvementMemberRoleLookupBtn);

            return this;
        }
        public PersonFormInvolvementRecordSubPage InsertStartDate(string StartDate)
        {
            WaitForElement(startDateField);
            SendKeys(startDateField, StartDate);

            return this;
        }

        public PersonFormInvolvementRecordSubPage TapSaveAndCloseButton()
        {
            driver.FindElement(saveAndCloseButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public PersonFormInvolvementRecordSubPage TapSaveButton()
        {
            driver.FindElement(saveButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }


        public PersonFormInvolvementRecordSubPage TapBackButton()
        {
            driver.FindElement(backButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public PersonFormInvolvementRecordSubPage ValidateTopAreaWarningMessage(string ExpectedMessage)
        {
            WaitForElementVisible(WarningMainArea(ExpectedMessage));

            return this;
        }

        public PersonFormInvolvementRecordSubPage TapDeleteRecordButton()
        {
            driver.FindElement(deleteRecordButton).Click();

        

            return this;
        }

        /// <summary>
        /// Wait for all toolbar buttons to be visible (after a user tapped on the Additional Toolbar Elements Button)
        /// </summary>
        /// <returns></returns>


    }
}
