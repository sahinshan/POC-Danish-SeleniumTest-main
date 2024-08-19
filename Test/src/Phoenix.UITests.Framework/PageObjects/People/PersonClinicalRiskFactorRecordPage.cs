using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonClinicalRiskFactorRecordPage : CommonMethods
    {

        public PersonClinicalRiskFactorRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personclinicalriskfactor')]");


        readonly By messageArea = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalIttemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");



        #region Field Title

        readonly By general_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div[1]/span[text()='General']");
        
        readonly By Person_FieldTitle = By.XPath("//*[@id='CWLabelHolder_personid']/label");
        readonly By RelatedCase_FieldTitle = By.XPath("//*[@id='CWLabelHolder_caseid']/label");
        readonly By LinkedFrom_FieldTitle = By.XPath("//*[@id='CWLabelHolder_personformid']/label");
        readonly By RiskFactorType_FieldTitle = By.XPath("//*[@id='CWLabelHolder_clinicalriskfactortypeid']/label");
        readonly By RiskFactorSubType_FieldTitle = By.XPath("//*[@id='CWLabelHolder_clinicalriskfactorsubtypeid']/label");
        readonly By SensitiveInformation_FieldTitle = By.XPath("//*[@id='CWLabelHolder_sensitiveinformation']/label");
        readonly By Description_FieldTitle = By.XPath("//*[@id='CWLabelHolder_description']/label");
        readonly By Triggers_FieldTitle = By.XPath("//*[@id='CWLabelHolder_triggers']/label");
        readonly By RiskLevel_FieldTitle = By.XPath("//*[@id='CWLabelHolder_clinicalrisklevelid']/label");
        readonly By DateIdentified_FieldTitle = By.XPath("//*[@id='CWLabelHolder_dateidentified']/label");

        readonly By ResponsibleTeam_FieldTitle = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleUser_FieldTitle = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label");
        readonly By ResponsibleParty_FieldTitle = By.XPath("//*[@id='CWLabelHolder_responsibleparty']/label");
        readonly By RiskManagementMitigation_FieldTitle = By.XPath("//*[@id='CWLabelHolder_riskmitigation']/label");
        readonly By DateMitigationActioned_FieldTitle = By.XPath("//*[@id='CWLabelHolder_datemitigationactioned']/label");
        readonly By SignedOff_FieldTitle = By.XPath("//*[@id='CWLabelHolder_signedoff']/label");
        readonly By EndDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_enddate']/label");

        #endregion


        #region Fields

        readonly By Person_LinkField = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By Person_RemoveButton = By.XPath("//*[@id='CWClearLookup_personid']");
        readonly By Person_LookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
        readonly By Person_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_personid']/label/span");

        readonly By RelatedCase_LinkField = By.XPath("//*[@id='CWField_caseid_Link']");
        readonly By RelatedCase_RemoveButton = By.XPath("//*[@id='CWClearLookup_caseid']");
        readonly By RelatedCase_LookupButton = By.XPath("//*[@id='CWLookupBtn_caseid']");

        readonly By LinkedForm_LinkField = By.XPath("//*[@id='CWField_personformid_Link']");
        readonly By LinkedForm_RemoveButton = By.XPath("//*[@id='CWClearLookup_personformid']");
        readonly By LinkedForm_LookupButton = By.XPath("//*[@id='CWLookupBtn_personformid']");

        readonly By RiskFactorType_LinkField = By.XPath("//*[@id='CWField_clinicalriskfactortypeid_Link']");
        readonly By RiskFactorType_RemoveButton = By.XPath("//*[@id='CWClearLookup_clinicalriskfactortypeid']");
        readonly By RiskFactorType_LookupButton = By.XPath("//*[@id='CWLookupBtn_clinicalriskfactortypeid']");
        readonly By RiskFactorType_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_clinicalriskfactortypeid']/label/span");

        readonly By RiskFactorSubType_LinkField = By.XPath("//*[@id='CWField_clinicalriskfactorsubtypeid_Link']");
        readonly By RiskFactorSubType_RemoveButton = By.XPath("//*[@id='CWClearLookup_clinicalriskfactorsubtypeid']");
        readonly By RiskFactorSubType_LookupButton = By.XPath("//*[@id='CWLookupBtn_clinicalriskfactorsubtypeid']");

        readonly By SensitiveInformation_YesRadioButton = By.XPath("//*[@id='CWField_sensitiveinformation_1']");
        readonly By SensitiveInformation_NoRadioButton = By.XPath("//*[@id='CWField_sensitiveinformation_0']");

        readonly By Description_Field = By.XPath("//*[@id='CWField_description']");
        readonly By Description_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_description']/label/span");

        readonly By Triggers_Field = By.XPath("//*[@id='CWField_triggers']");

        readonly By RiskLevel_LinkField = By.XPath("//*[@id='CWField_clinicalrisklevelid_Link']");
        readonly By RiskLevel_RemoveButton = By.XPath("//*[@id='CWClearLookup_clinicalrisklevelid']");
        readonly By RiskLevel_LookupButton = By.XPath("//*[@id='CWLookupBtn_clinicalrisklevelid']");

        readonly By DateIdentified_DateField = By.XPath("//*[@id='CWField_dateidentified']");
        readonly By DateIdentified_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_dateidentified']/label/span");

        readonly By responsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");        
        readonly By responsibleTeam_RemoveButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By responsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By responsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");

        readonly By responsibleUser_LinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By responsibleUser_RemoveButton = By.XPath("//*[@id='CWClearLookup_responsibleuserid']");
        readonly By responsibleUser_LookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
        readonly By responsibleUser_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_responsibleuserid']/label/span");

        readonly By ResponsibleParty_Field = By.XPath("//*[@id='CWField_responsibleparty']");

        readonly By RiskManagementMitigation_Field = By.XPath("//*[@id='CWField_riskmitigation']");
        readonly By RiskManagementMitigation_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_riskmitigation']/label/span");

        readonly By DateMitigationActioned_DateField = By.XPath("//*[@id='CWField_datemitigationactioned']");

        readonly By SignedOff_YesRadioButton = By.XPath("//*[@id='CWField_signedoff_1']");
        readonly By SignedOff_NoRadioButton = By.XPath("//*[@id='CWField_signedoff_0']");

        readonly By EndDate_DateField = By.XPath("//*[@id='CWField_enddate']");

        readonly By ReasonEnded_LinkField = By.XPath("//*[@id='CWField_clinicalriskfactorendreasonid_Link']");
        readonly By ReasonEnded_RemoveButton = By.XPath("//*[@id='CWClearLookup_clinicalriskfactorendreasonid']");
        readonly By ReasonEnded_LookupButton = By.XPath("//*[@id='CWLookupBtn_clinicalriskfactorendreasonid']");
        readonly By ReasonEnded_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_clinicalriskfactorendreasonid']/label/span");

        readonly By EndedBy_LinkField = By.XPath("//*[@id='CWField_endedbyid_Link']");
        readonly By EndedBy_RemoveButton = By.XPath("//*[@id='CWClearLookup_endedbyid']");
        readonly By EndedBy_LookupButton = By.XPath("//*[@id='CWLookupBtn_endedbyid']");
        readonly By EndedBy_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_endedbyid']/label/span");

        #endregion

        #region Menu

        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By PersonClinicalRiskFactorHistory_LeftMenu = By.XPath("//*[@id='CWNavItem_PersonClinicalRiskFactorHistory']");

        #endregion


        public PersonClinicalRiskFactorRecordPage WaitForPersonClinicalRiskFactorRecordPageToLoad(string TaskTitle)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(general_SectionTitle);
            this.WaitForElement(Person_FieldTitle);
            this.WaitForElement(RelatedCase_FieldTitle);
            this.WaitForElement(LinkedFrom_FieldTitle);
            this.WaitForElement(RiskFactorType_FieldTitle);
            this.WaitForElement(RiskFactorSubType_FieldTitle);
            this.WaitForElement(SensitiveInformation_FieldTitle);
            this.WaitForElement(Description_FieldTitle);
            this.WaitForElement(Triggers_FieldTitle);
            this.WaitForElement(RiskLevel_FieldTitle);
            this.WaitForElement(DateIdentified_FieldTitle);
            this.WaitForElement(ResponsibleTeam_FieldTitle);
            this.WaitForElement(ResponsibleUser_FieldTitle);
            this.WaitForElement(ResponsibleParty_FieldTitle);
            this.WaitForElement(RiskManagementMitigation_FieldTitle);
            this.WaitForElement(DateMitigationActioned_FieldTitle);
            this.WaitForElement(SignedOff_FieldTitle);
            this.WaitForElement(EndDate_FieldTitle);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            ValidateElementTextContainsText(pageHeader, "Clinical Risk Factor:\r\n" + TaskTitle);

            this.WaitForElementVisible(Person_LinkField);

            return this;
        }

        public PersonClinicalRiskFactorRecordPage WaitForInactivePersonClinicalRiskFactorRecordPageToLoad(string TaskTitle)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            this.WaitForElement(general_SectionTitle);
            this.WaitForElement(Person_FieldTitle);
            this.WaitForElement(RelatedCase_FieldTitle);
            this.WaitForElement(LinkedFrom_FieldTitle);
            this.WaitForElement(RiskFactorType_FieldTitle);
            this.WaitForElement(RiskFactorSubType_FieldTitle);
            this.WaitForElement(SensitiveInformation_FieldTitle);
            this.WaitForElement(Description_FieldTitle);
            this.WaitForElement(Triggers_FieldTitle);
            this.WaitForElement(RiskLevel_FieldTitle);
            this.WaitForElement(DateIdentified_FieldTitle);
            this.WaitForElement(ResponsibleTeam_FieldTitle);
            this.WaitForElement(ResponsibleUser_FieldTitle);
            this.WaitForElement(ResponsibleParty_FieldTitle);
            this.WaitForElement(RiskManagementMitigation_FieldTitle);
            this.WaitForElement(DateMitigationActioned_FieldTitle);
            this.WaitForElement(SignedOff_FieldTitle);
            this.WaitForElement(EndDate_FieldTitle);


            WaitForElementNotVisible(saveButton, 5);
            WaitForElementNotVisible(saveAndCloseButton, 5);

            ValidateElementTextContainsText(pageHeader, "Clinical Risk Factor:\r\n" + TaskTitle);

            ValidateElementDisabled(Person_LookupButton);
            ValidateElementDisabled(RelatedCase_LookupButton);
            ValidateElementDisabled(LinkedForm_LookupButton);
            ValidateElementDisabled(RiskFactorType_LookupButton);
            ValidateElementDisabled(RiskFactorSubType_LookupButton);
            ValidateElementDisabled(SensitiveInformation_YesRadioButton);
            ValidateElementDisabled(SensitiveInformation_NoRadioButton);
            ValidateElementDisabled(Description_Field);
            ValidateElementDisabled(Triggers_Field);
            ValidateElementDisabled(RiskLevel_LookupButton);
            ValidateElementDisabled(DateIdentified_DateField);
            ValidateElementDisabled(responsibleTeam_LookupButton);
            ValidateElementDisabled(responsibleUser_LookupButton);
            ValidateElementDisabled(ResponsibleParty_Field);
            ValidateElementDisabled(RiskManagementMitigation_Field);
            ValidateElementDisabled(DateMitigationActioned_DateField);
            ValidateElementDisabled(SignedOff_YesRadioButton);
            ValidateElementDisabled(SignedOff_NoRadioButton);
            ValidateElementDisabled(EndDate_DateField);

            return this;
        }







        public PersonClinicalRiskFactorRecordPage InsertDescription(string TextToInsert)
        {
            SendKeys(Description_Field, TextToInsert);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage InsertTriggers(string TextToInsert)
        {
            SendKeys(Triggers_Field, TextToInsert);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage InsertDateIdentified(string DateToInsert)
        {
            SendKeys(DateIdentified_DateField, DateToInsert);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage InsertResponsibleParty(string TextToInsert)
        {
            SendKeys(ResponsibleParty_Field, TextToInsert);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage InsertRiskManagementMitigation(string TextToInsert)
        {
            SendKeys(RiskManagementMitigation_Field, TextToInsert);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage InsertDateMitigationActioned(string DateToInsert)
        {
            SendKeys(DateMitigationActioned_DateField, DateToInsert);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage InsertEndDate(string DateToInsert)
        {
            SendKeys(EndDate_DateField, DateToInsert);
            SendKeysWithoutClearing(EndDate_DateField, Keys.Tab);

            return this;
        }



        
        public PersonClinicalRiskFactorRecordPage ClickPersonRemoveButton()
        {
            Click(Person_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickPersonLookupButton()
        {
            Click(Person_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickRelatedCaseRemoveButton()
        {
            Click(RelatedCase_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickRelatedCaseLookupButton()
        {
            Click(RelatedCase_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickLinkedFormRemoveButton()
        {
            Click(LinkedForm_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickLinkedFormLookupButton()
        {
            Click(LinkedForm_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickRiskFactorTypeRemoveButton()
        {
            Click(RiskFactorType_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickRiskFactorTypeLookupButton()
        {
            Click(RiskFactorType_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickRiskFactorSubTypeRemoveButton()
        {
            Click(RiskFactorSubType_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickRiskFactorSubTypeLookupButton()
        {
            Click(RiskFactorSubType_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickSensitiveInformationYesRadioButton()
        {
            Click(SensitiveInformation_YesRadioButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickSensitiveInformationNoRadioButton()
        {
            Click(SensitiveInformation_NoRadioButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickRiskLevelRemoveButton()
        {
            Click(RiskLevel_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickRiskLevelLookupButton()
        {
            Click(RiskLevel_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickResponsibleTeamRemoveButton()
        {
            Click(responsibleTeam_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(responsibleTeam_LookupButton);
            
            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickResponsibleUserRemoveButton()
        {
            Click(responsibleUser_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickResponsibleUserLookupButton()
        {
            Click(responsibleUser_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickSignedOffYesRadioButton()
        {
            Click(SignedOff_YesRadioButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickSignedOffNoRadioButton()
        {
            Click(SignedOff_NoRadioButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickReasonEndedRemoveButton()
        {
            Click(ReasonEnded_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickReasonEndedLookupButton()
        {
            Click(ReasonEnded_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickEndedByRemoveButton()
        {
            Click(EndedBy_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickEndedByLookupButton()
        {
            Click(EndedBy_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }

        public PersonClinicalRiskFactorRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);            
            WaitForElementVisible(additionalIttemsButton);
            return this;
        }

        public PersonClinicalRiskFactorRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickDeleteButtonOnInactiveRecord()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }


        public PersonClinicalRiskFactorRecordPage NavigateToPersonClinicalRiskFactorHistory()
        {
            WaitForElementToBeClickable(LeftMenuButton);
            Click(LeftMenuButton);

            WaitForElementToBeClickable(PersonClinicalRiskFactorHistory_LeftMenu);
            Click(PersonClinicalRiskFactorHistory_LeftMenu);

            return this;
        }


        public PersonClinicalRiskFactorRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(messageArea, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidatePersonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Person_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateRelatedCaseLinkFieldText(string ExpectedText)
        {
            ValidateElementText(RelatedCase_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateLinkedFormLinkFieldText(string ExpectedText)
        {
            ValidateElementText(LinkedForm_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateRiskFactorTypeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(RiskFactorType_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateRiskFactorSubTypeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(RiskFactorSubType_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateDescription(string ExpectedText)
        {
            ValidateElementText(Description_Field, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateTriggers(string ExpectedText)
        {
            ValidateElementText(Triggers_Field, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateRiskLevelLinkFieldText(string ExpectedText)
        {
            ValidateElementText(RiskLevel_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateDateIdentifiedText(string ExpectedDate)
        {
            ValidateElementValue(DateIdentified_DateField, ExpectedDate);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateResponsibleUserLinkFieldText(string ExpectedText)
        {
            ValidateElementText(responsibleUser_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateReasonEndedLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ReasonEnded_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateEndedByLinkFieldText(string ExpectedText)
        {
            ValidateElementText(EndedBy_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateResponsibleParty(string ExpectedText)
        {
            ValidateElementValue(ResponsibleParty_Field, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateRiskManagementMitigation(string ExpectedText)
        {
            ValidateElementText(RiskManagementMitigation_Field, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateDateMitigationActionedText(string ExpectedDate)
        {
            ValidateElementValue(DateMitigationActioned_DateField, ExpectedDate);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateEndDate(string ExpectedDate)
        {
            ValidateElementValue(EndDate_DateField, ExpectedDate);

            return this;
        }


        public PersonClinicalRiskFactorRecordPage ValidatePersonErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Person_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateRiskFactorTypeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(RiskFactorType_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateDescriptionErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Description_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateDateIdentifiedErrorLabelText(string ExpectedText)
        {
            ValidateElementText(DateIdentified_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateResponsibleTeamErrorLabelText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateResponsibleUserErrorLabelText(string ExpectedText)
        {
            ValidateElementText(responsibleUser_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateRiskManagementMitigationErrorLabelText(string ExpectedText)
        {
            ValidateElementText(RiskManagementMitigation_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateReasonEndedErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ReasonEnded_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateEndedByErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EndedBy_FieldErrorLabel, ExpectedText);

            return this;
        }




        public PersonClinicalRiskFactorRecordPage ValidateSensitiveInformationYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(SensitiveInformation_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(SensitiveInformation_YesRadioButton);
            }

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateSensitiveInformationNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(SensitiveInformation_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(SensitiveInformation_NoRadioButton);
            }

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateSignedOffYesOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(SignedOff_YesRadioButton);
            }
            else
            {
                ValidateElementNotChecked(SignedOff_YesRadioButton);
            }

            return this;
        }
        public PersonClinicalRiskFactorRecordPage ValidateSignedOffNoOptionChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
            {
                ValidateElementChecked(SignedOff_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(SignedOff_NoRadioButton);
            }

            return this;
        }


    }
}
