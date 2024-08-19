using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonHealthDiagnosisRecordPage : CommonMethods
    {
        public PersonHealthDiagnosisRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=persondiagnosis')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By NotificationMessage_DataForm = By.Id("CWNotificationMessage_DataForm");
        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");



        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        #region General Section
        readonly By sourceofdiagnosisid_option = By.Id("CWField_sourceofdiagnosisid");
        readonly By consultantEpisode_LookupBt = By.Id("CWLookupBtn_inpatientconsultantepisodeid");
        readonly By consultantEpisode_LinkField = By.Id("CWField_inpatientconsultantepisodeid_Link");

        #endregion

        #region Recording Diagnosis Section

        readonly By diagnosisid_LookupBtn = By.Id("CWLookupBtn_diagnosisid");
        readonly By DiagnosisICD10_Field = By.Id("CWField_diagnosisid_cwname");
        readonly By SnomedCT_Field = By.Id("CWField_snomedctid_cwname");
        readonly By SnomedCT_LookupBtn = By.Id("CWLookupBtn_snomedctid");
        readonly By diagnosis_LinkField = By.Id("CWField_diagnosisid_Link");
        readonly By primaryorsecondaryid_option = By.Id("CWField_primaryorsecondaryid");
        readonly By asteriskordaggerid_option = By.Id("CWField_asteriskordaggerid");
        readonly By provisionalorconfirmedid_option = By.Id("CWField_provisionalorconfirmedid");
        readonly By confirmeddate_Field = By.Id("CWField_confirmeddate");
        readonly By dateOfDiasnosis_Field = By.Id("CWField_startdate");
        readonly By professionalcoderconfirmeddiagnosisid_LookupBtn = By.Id("CWLookupBtn_professionalcoderconfirmeddiagnosisid");
        readonly By ProfessionalCoderConfirmedDiagnosis_Field = By.Id("CWField_professionalcoderconfirmeddiagnosisid_cwname");
        readonly By isPersonAwerOfDiagnosis_option = By.Id("CWField_ispersonawareofdiagnosisid");
        readonly By codingSchema_option = By.Id("CWField_codingschemaid");

        #endregion

        #region Discontinuation of Diagnosis & Notes Section

        readonly By dateEnded_Field = By.Id("CWField_enddate");
        readonly By ProfessionalProvidedEndReason_LookupBtn = By.Id("CWLookupBtn_professionalprovidedendreasonid");
        readonly By ProfessionalProvidedEndReason_Field = By.Id("CWField_professionalprovidedendreasonid_cwname");
        readonly By ProfessionalProvidedEndReason_LinkField = By.Id("CWField_professionalprovidedendreasonid_Link");
        readonly By EndReason_LookupBtn = By.Id("CWLookupBtn_persondiagnosisendreasonid");
        readonly By EndReason_Field = By.Id("CWField_persondiagnosisendreasonid_cwname");
        readonly By EndReason_LinkField = By.Id("CWField_persondiagnosisendreasonid_Link");
        readonly By notes_Field = By.Id("CWField_notes");

        #endregion

        

        public PersonHealthDiagnosisRecordPage WaitForPersonHealthDiagnosisRecordPageToLoad(string TaskTitle)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            ValidateElementTextContainsText(pageHeader, "Person Diagnosis:\r\n" + TaskTitle);

           

            return this;
        }

        public PersonHealthDiagnosisRecordPage WaitForSavedPersonHealthDiagnosisRecordPageToLoad(string TaskTitle)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

          
            ValidateElementTextContainsText(pageHeader, "Person Diagnosis:\r\n" + TaskTitle);



            return this;
        }

        public PersonHealthDiagnosisRecordPage ClickConsultantEpisodeLookupButton()
        {
            WaitForElementToBeClickable(consultantEpisode_LookupBt);
            Click(consultantEpisode_LookupBt);

            return this;
        }
        public PersonHealthDiagnosisRecordPage SelectSourceOfDiagnosis(String OptionToSelect)
        {
            WaitForElementVisible(sourceofdiagnosisid_option);
            SelectPicklistElementByText(sourceofdiagnosisid_option, OptionToSelect);

            return this;
        }

        public PersonHealthDiagnosisRecordPage ClickDiagnosisLookupButton()
        {
            WaitForElementToBeClickable(diagnosisid_LookupBtn);
            Click(diagnosisid_LookupBtn);

            return this;
        }

        public PersonHealthDiagnosisRecordPage ClickEndReasonLookupButton()
        {
            WaitForElementToBeClickable(EndReason_LookupBtn);
            Click(EndReason_LookupBtn);

            return this;
        }

        public PersonHealthDiagnosisRecordPage ClickProfessionalProvidedEndReasonLookupButton()
        {
            WaitForElementToBeClickable(ProfessionalProvidedEndReason_LookupBtn);
            Click(ProfessionalProvidedEndReason_LookupBtn);

            return this;
        }

        public PersonHealthDiagnosisRecordPage ClickSomedCtLookupButton()
        {
            WaitForElementToBeClickable(SnomedCT_LookupBtn);
            Click(SnomedCT_LookupBtn);

            return this;
        }

        public PersonHealthDiagnosisRecordPage ClickProfessionalWhoConfirmedLookupButton()
        {
            WaitForElementToBeClickable(professionalcoderconfirmeddiagnosisid_LookupBtn);
            Click(professionalcoderconfirmeddiagnosisid_LookupBtn);

            return this;
        }

        public PersonHealthDiagnosisRecordPage SelectAsteriskorDaggerid(String OptionToSelect)
        {
            WaitForElementVisible(asteriskordaggerid_option);
            SelectPicklistElementByValue(asteriskordaggerid_option, OptionToSelect);

            return this;
        }

        public PersonHealthDiagnosisRecordPage SelectProvisionalOrConfirmedid(String OptionToSelect)
        {
            WaitForElementVisible(provisionalorconfirmedid_option);
            SelectPicklistElementByText(provisionalorconfirmedid_option, OptionToSelect);

            return this;
        }

        public PersonHealthDiagnosisRecordPage SelectCodingSchemaField(String OptionToSelect)
        {
            WaitForElementVisible(codingSchema_option);
            SelectPicklistElementByText(codingSchema_option, OptionToSelect);

            return this;
        }

        public PersonHealthDiagnosisRecordPage SelectisPersonAwerOfDiagnosis(String OptionToSelect)
        {
            WaitForElementVisible(isPersonAwerOfDiagnosis_option);
            SelectPicklistElementByText(isPersonAwerOfDiagnosis_option, OptionToSelect);

            return this;
        }

        public PersonHealthDiagnosisRecordPage SelectPrimaryOrSecondaryid(String OptionToSelect)
        {
            WaitForElementVisible(primaryorsecondaryid_option);
            SelectPicklistElementByText(primaryorsecondaryid_option, OptionToSelect);

            return this;
        }

        public PersonHealthDiagnosisRecordPage InsertDiagnosisDate(string datetoinsert)
        {
            WaitForElement(dateOfDiasnosis_Field);
            SendKeys(dateOfDiasnosis_Field, datetoinsert);

            return this;
        }

        public PersonHealthDiagnosisRecordPage InsertDateEnded(string datetoinsert)
        {
            WaitForElement(dateEnded_Field);
            SendKeys(dateEnded_Field, datetoinsert);

            return this;
        }

        public PersonHealthDiagnosisRecordPage InsertDateOfConfirmed(string datetoinsert)
        {
            WaitForElement(confirmeddate_Field);
            SendKeys(confirmeddate_Field, datetoinsert);

            return this;
        }

        public PersonHealthDiagnosisRecordPage InsertNotes(String TextToInsert)
        {
            WaitForElement(notes_Field);
            SendKeys(notes_Field, TextToInsert);
            return this;
        }


        public PersonHealthDiagnosisRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateDiagnosisDate(String ExpectedText)
        {
            WaitForElementVisible(dateOfDiasnosis_Field);
            ValidateElementValue(dateOfDiasnosis_Field, ExpectedText);
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateSourceOfDiagnosisField(String ExpectedText)
        {
            WaitForElementVisible(sourceofdiagnosisid_option);
            ValidateElementValue(sourceofdiagnosisid_option, ExpectedText);
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateConsultantEpisodeField(String ExpectedText)
        {
            WaitForElementVisible(consultantEpisode_LinkField);
            ValidateElementTextContainsText(consultantEpisode_LinkField, ExpectedText);
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateDiagnosisField(String ExpectedText)
        {
            WaitForElementVisible(diagnosis_LinkField);
            ValidateElementText(diagnosis_LinkField, ExpectedText);
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateProfessionalProvidedEndReasonField(String ExpectedText)
        {
            WaitForElementVisible(ProfessionalProvidedEndReason_LinkField);
            ValidateElementText(ProfessionalProvidedEndReason_LinkField, ExpectedText);
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateEndReasonField(String ExpectedText)
        {
            WaitForElementVisible(EndReason_LinkField);
            ValidateElementText(EndReason_LinkField, ExpectedText);
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateAsteriskOrDaggerField(String ExpectedText)
        {
            WaitForElementVisible(asteriskordaggerid_option);
            ValidateElementValue(asteriskordaggerid_option, ExpectedText);
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateProvisionalOrConfirmedField(String ExpectedText)
        {
            WaitForElementVisible(provisionalorconfirmedid_option);
            ValidateElementValue(provisionalorconfirmedid_option, ExpectedText);
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateisPersonAwerOfDiagnosisField(String ExpectedText)
        {
            WaitForElementVisible(isPersonAwerOfDiagnosis_option);
            ValidateElementValue(isPersonAwerOfDiagnosis_option, ExpectedText);
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidatePrimaryOrSecondaryField(String ExpectedText)
        {
            WaitForElementVisible(primaryorsecondaryid_option);
            ValidateElementValue(primaryorsecondaryid_option, ExpectedText);
            return this;
        }
        public PersonHealthDiagnosisRecordPage ValidateProfessionalCoderConfirmedDiagnosisFieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(professionalcoderconfirmeddiagnosisid_LookupBtn);
            }
            else
            {
                WaitForElementNotVisible(professionalcoderconfirmeddiagnosisid_LookupBtn, 5);
            }
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateProfessionalProvidedEndReasonFieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(ProfessionalProvidedEndReason_Field);
            }
            else
            {
                WaitForElementNotVisible(ProfessionalProvidedEndReason_Field, 5);
            }
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateEndReasonFieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(EndReason_Field);
            }
            else
            {
                WaitForElementNotVisible(EndReason_Field, 5);
            }
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateDiagnosisICD10FieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(DiagnosisICD10_Field);
            }
            else
            {
                WaitForElementNotVisible(DiagnosisICD10_Field, 5);
            }
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateSnomedCTFieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(SnomedCT_Field);
            }
            else
            {
                WaitForElementNotVisible(SnomedCT_Field, 5);
            }
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateProfessionalWhoConfirmedDiagnosisFieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(ProfessionalCoderConfirmedDiagnosis_Field);
            }
            else
            {
                WaitForElementNotVisible(ProfessionalCoderConfirmedDiagnosis_Field, 5);
            }
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateConfirmedDateFieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(confirmeddate_Field);
            }
            else
            {
                WaitForElementNotVisible(confirmeddate_Field, 5);
            }
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateProfessionalWhoConfirmedDiagnosisFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                WaitForElement(professionalcoderconfirmeddiagnosisid_LookupBtn);
                ValidateElementDisabled(professionalcoderconfirmeddiagnosisid_LookupBtn);
            }
            else
            {
                WaitForElement(professionalcoderconfirmeddiagnosisid_LookupBtn);
                ValidateElementEnabled(professionalcoderconfirmeddiagnosisid_LookupBtn);
            }
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateConfirmedDateFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(confirmeddate_Field);
            }
            else
            {
                ValidateElementEnabled(confirmeddate_Field);
            }
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateProvisonalConfirmedFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(provisionalorconfirmedid_option);
            }
            else
            {
                ValidateElementEnabled(provisionalorconfirmedid_option);
            }
            return this;
        }

        public PersonHealthDiagnosisRecordPage ValidateNotificationErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(NotificationMessage, ExpectedMessage);

            return this;
        }





    }
}
