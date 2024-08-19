using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonClinicalRiskFactorHistoryRecordPage : CommonMethods
    {

        public PersonClinicalRiskFactorHistoryRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personclinicalriskfactorhistory')]");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");


        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");



        #region Field Title

        readonly By general_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div[1]/span[text()='General']");
        
        readonly By Person_FieldTitle = By.XPath("//*[@id='CWLabelHolder_personid']/label");
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
        readonly By RiskManagementMitigation_FieldTitle = By.XPath("//*[@id='CWLabelHolder_riskmanagementmitigation']/label");
        readonly By DateMitigationActioned_FieldTitle = By.XPath("//*[@id='CWLabelHolder_datemitigationactioned']/label");
        readonly By SignedOff_FieldTitle = By.XPath("//*[@id='CWLabelHolder_signedoff']/label");
        readonly By EndDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_enddate']/label");

        #endregion


        #region Fields

        readonly By Person_LinkField = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By Person_RemoveButton = By.XPath("//*[@id='CWClearLookup_personid']");
        readonly By Person_LookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");

        readonly By LinkedForm_LinkField = By.XPath("//*[@id='CWField_personformid_Link']");
        readonly By LinkedForm_RemoveButton = By.XPath("//*[@id='CWClearLookup_personformid']");
        readonly By LinkedForm_LookupButton = By.XPath("//*[@id='CWLookupBtn_personformid']");

        readonly By RiskFactorType_LinkField = By.XPath("//*[@id='CWField_clinicalriskfactortypeid_Link']");
        readonly By RiskFactorType_RemoveButton = By.XPath("//*[@id='CWClearLookup_clinicalriskfactortypeid']");
        readonly By RiskFactorType_LookupButton = By.XPath("//*[@id='CWLookupBtn_clinicalriskfactortypeid']");

        readonly By RiskFactorSubType_LinkField = By.XPath("//*[@id='CWField_clinicalriskfactorsubtypeid_Link']");
        readonly By RiskFactorSubType_RemoveButton = By.XPath("//*[@id='CWClearLookup_clinicalriskfactorsubtypeid']");
        readonly By RiskFactorSubType_LookupButton = By.XPath("//*[@id='CWLookupBtn_clinicalriskfactorsubtypeid']");

        readonly By SensitiveInformation_YesRadioButton = By.XPath("//*[@id='CWField_sensitiveinformation_1']");
        readonly By SensitiveInformation_NoRadioButton = By.XPath("//*[@id='CWField_sensitiveinformation_0']");

        readonly By Description_Field = By.XPath("//*[@id='CWField_description']");

        readonly By Triggers_Field = By.XPath("//*[@id='CWField_triggers']");

        readonly By RiskLevel_LinkField = By.XPath("//*[@id='CWField_clinicalrisklevelid_Link']");
        readonly By RiskLevel_RemoveButton = By.XPath("//*[@id='CWClearLookup_clinicalrisklevelid']");
        readonly By RiskLevel_LookupButton = By.XPath("//*[@id='CWLookupBtn_clinicalrisklevelid']");

        readonly By DateIdentified_DateField = By.XPath("//*[@id='CWField_dateidentified']");

        readonly By responsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");        
        readonly By responsibleTeam_RemoveButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By responsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

        readonly By responsibleUser_LinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By responsibleUser_RemoveButton = By.XPath("//*[@id='CWClearLookup_responsibleuserid']");
        readonly By responsibleUser_LookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");

        readonly By ResponsibleParty_Field = By.XPath("//*[@id='CWField_responsibleparty']");

        readonly By RiskManagementMitigation_Field = By.XPath("//*[@id='CWField_riskmanagementmitigation']");

        readonly By DateMitigationActioned_DateField = By.XPath("//*[@id='CWField_datemitigationactioned']");

        readonly By SignedOff_YesRadioButton = By.XPath("//*[@id='CWField_signedoff_1']");
        readonly By SignedOff_NoRadioButton = By.XPath("//*[@id='CWField_signedoff_0']");

        readonly By EndDate_DateField = By.XPath("//*[@id='CWField_enddate']");

        readonly By ReasonEnded_LinkField = By.XPath("//*[@id='CWField_clinicalriskfactorendreasonid_Link']");
        readonly By ReasonEnded_RemoveButton = By.XPath("//*[@id='CWClearLookup_clinicalriskfactorendreasonid']");
        readonly By ReasonEnded_LookupButton = By.XPath("//*[@id='CWLookupBtn_clinicalriskfactorendreasonid']");

        readonly By EndedBy_LinkField = By.XPath("//*[@id='CWField_endedbyid_Link']");
        readonly By EndedBy_RemoveButton = By.XPath("//*[@id='CWClearLookup_endedbyid']");
        readonly By EndedBy_LookupButton = By.XPath("//*[@id='CWLookupBtn_endedbyid']");

        #endregion


        public PersonClinicalRiskFactorHistoryRecordPage WaitForInactivePersonClinicalRiskFactorHistoryRecordPageToLoad(string TaskTitle)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            this.WaitForElement(general_SectionTitle);
            this.WaitForElement(Person_FieldTitle);
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

            ValidateElementTextContainsText(pageHeader, "Clinical Risk Factor History:\r\n" + TaskTitle);

            ValidateElementDisabled(Person_LookupButton);
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







        public PersonClinicalRiskFactorHistoryRecordPage InsertDescription(string TextToInsert)
        {
            SendKeys(Description_Field, TextToInsert);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage InsertTriggers(string TextToInsert)
        {
            SendKeys(Triggers_Field, TextToInsert);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage InsertDateIdentified(string DateToInsert)
        {
            SendKeys(DateIdentified_DateField, DateToInsert);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage InsertResponsibleParty(string TextToInsert)
        {
            SendKeys(ResponsibleParty_Field, TextToInsert);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage InsertRiskManagementMitigation(string TextToInsert)
        {
            SendKeys(RiskManagementMitigation_Field, TextToInsert);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage InsertDateMitigationActioned(string DateToInsert)
        {
            SendKeys(DateMitigationActioned_DateField, DateToInsert);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage InsertEndDate(string DateToInsert)
        {
            SendKeys(EndDate_DateField, DateToInsert);
            SendKeysWithoutClearing(EndDate_DateField, Keys.Tab);

            return this;
        }



        
        public PersonClinicalRiskFactorHistoryRecordPage ClickPersonRemoveButton()
        {
            Click(Person_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickPersonLookupButton()
        {
            Click(Person_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickLinkedFormRemoveButton()
        {
            Click(LinkedForm_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickLinkedFormLookupButton()
        {
            Click(LinkedForm_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickRiskFactorTypeRemoveButton()
        {
            Click(RiskFactorType_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickRiskFactorTypeLookupButton()
        {
            Click(RiskFactorType_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickRiskFactorSubTypeRemoveButton()
        {
            Click(RiskFactorSubType_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickRiskFactorSubTypeLookupButton()
        {
            Click(RiskFactorSubType_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickSensitiveInformationYesRadioButton()
        {
            Click(SensitiveInformation_YesRadioButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickSensitiveInformationNoRadioButton()
        {
            Click(SensitiveInformation_NoRadioButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickRiskLevelRemoveButton()
        {
            Click(RiskLevel_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickRiskLevelLookupButton()
        {
            Click(RiskLevel_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickResponsibleTeamRemoveButton()
        {
            Click(responsibleTeam_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickResponsibleTeamLookupButton()
        {
            Click(responsibleTeam_LookupButton);
            
            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickResponsibleUserRemoveButton()
        {
            Click(responsibleUser_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickResponsibleUserLookupButton()
        {
            Click(responsibleUser_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickSignedOffYesRadioButton()
        {
            Click(SignedOff_YesRadioButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickSignedOffNoRadioButton()
        {
            Click(SignedOff_NoRadioButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickReasonEndedRemoveButton()
        {
            Click(ReasonEnded_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickReasonEndedLookupButton()
        {
            Click(ReasonEnded_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickEndedByRemoveButton()
        {
            Click(EndedBy_RemoveButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickEndedByLookupButton()
        {
            Click(EndedBy_LookupButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickDeleteButtonOnInactiveRecord()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }





        public PersonClinicalRiskFactorHistoryRecordPage ValidatePersonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Person_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateLinkedFormLinkFieldText(string ExpectedText)
        {
            ValidateElementText(LinkedForm_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateRiskFactorTypeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(RiskFactorType_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateRiskFactorSubTypeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(RiskFactorSubType_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateDescription(string ExpectedText)
        {
            ValidateElementText(Description_Field, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateTriggers(string ExpectedText)
        {
            ValidateElementText(Triggers_Field, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateRiskLevelLinkFieldText(string ExpectedText)
        {
            ValidateElementText(RiskLevel_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateDateIdentifiedText(string ExpectedDate)
        {
            ValidateElementValue(DateIdentified_DateField, ExpectedDate);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateResponsibleUserLinkFieldText(string ExpectedText)
        {
            ValidateElementText(responsibleUser_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateReasonEndedLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ReasonEnded_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateEndedByLinkFieldText(string ExpectedText)
        {
            ValidateElementText(EndedBy_LinkField, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateResponsibleParty(string ExpectedText)
        {
            ValidateElementValue(ResponsibleParty_Field, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateRiskManagementMitigation(string ExpectedText)
        {
            ValidateElementText(RiskManagementMitigation_Field, ExpectedText);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateDateMitigationActionedText(string ExpectedDate)
        {
            ValidateElementValue(DateMitigationActioned_DateField, ExpectedDate);

            return this;
        }
        public PersonClinicalRiskFactorHistoryRecordPage ValidateEndDate(string ExpectedDate)
        {
            ValidateElementValue(EndDate_DateField, ExpectedDate);

            return this;
        }






        public PersonClinicalRiskFactorHistoryRecordPage ValidateSensitiveInformationYesOptionChecked(bool ExpectChecked)
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
        public PersonClinicalRiskFactorHistoryRecordPage ValidateSensitiveInformationNoOptionChecked(bool ExpectChecked)
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
        public PersonClinicalRiskFactorHistoryRecordPage ValidateSignedOffYesOptionChecked(bool ExpectChecked)
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
        public PersonClinicalRiskFactorHistoryRecordPage ValidateSignedOffNoOptionChecked(bool ExpectChecked)
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
