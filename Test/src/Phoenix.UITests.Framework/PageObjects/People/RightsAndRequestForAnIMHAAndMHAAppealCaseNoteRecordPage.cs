using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage : CommonMethods
    {

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=mharightsandrequestscasenote')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");


        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By cloneButton = By.Id("TI_CloneRecordButton");



        #region Field Title

        readonly By ContainsInformationProvidedByThirdParty_FieldTitle = By.XPath("//*[@id='CWLabelHolder_informationbythirdparty']/label");


        #endregion



        #region Fields

        readonly By subject_Field = By.XPath("//*[@id='CWField_subject']");

        readonly By spellCheckButton_NotesField = By.XPath("//*[@id='cke_63']/span[1]");

        readonly By responsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By responsibleTeam_RemoveButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By responsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

        readonly By responsibleUser_LinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By responsibleUser_RemoveButton = By.XPath("//*[@id='CWClearLookup_responsibleuserid']");
        readonly By responsibleUser_LookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");

        readonly By ContainsInformationProvidedByThirdParty_YesRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_1']");
        readonly By ContainsInformationProvidedByThirdParty_NoRadioButton = By.XPath("//*[@id='CWField_informationbythirdparty_0']");


        #endregion

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage WaitForRightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            return this;
        }


        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage ValidateContainsInformationProvidedByThirdPartyFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ContainsInformationProvidedByThirdParty_FieldTitle);
                WaitForElementVisible(ContainsInformationProvidedByThirdParty_YesRadioButton);
                WaitForElementVisible(ContainsInformationProvidedByThirdParty_NoRadioButton);
            }
            else
            {
                WaitForElementNotVisible(ContainsInformationProvidedByThirdParty_FieldTitle, 3);
                WaitForElementNotVisible(ContainsInformationProvidedByThirdParty_YesRadioButton, 3);
                WaitForElementNotVisible(ContainsInformationProvidedByThirdParty_NoRadioButton, 3);
            }

            return this;
        }


        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage ClickNotesFieldSpellCheckButton()
        {
            WaitForElement(spellCheckButton_NotesField);
            Click(spellCheckButton_NotesField);

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage InsertSubject(string TextToInsert)
        {
            SendKeys(subject_Field, TextToInsert);

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage ClickResponsibleTeamRemoveButton()
        {
            Click(responsibleTeam_RemoveButton);

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(responsibleTeam_LookupButton);

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage ClickResponsibleUserRemoveButton()
        {
            Click(responsibleUser_RemoveButton);

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage ClickResponsibleUserLookupButton()
        {
            Click(responsibleUser_LookupButton);

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(cloneButton);
            Click(cloneButton);

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            return this;
        }

        public RightsAndRequestForAnIMHAAndMHAAppealCaseNoteRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_LinkField, ExpectedText);

            return this;
        }




    }
}
