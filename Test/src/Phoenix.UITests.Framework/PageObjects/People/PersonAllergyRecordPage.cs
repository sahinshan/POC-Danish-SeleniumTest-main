using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonAllergyRecordPage : CommonMethods
    {
        public PersonAllergyRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


  

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personallergy')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By saveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By saveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");


        #region Fields

        readonly By person_FieldHeader = By.Id("CWLabelHolder_personid");
        readonly By person_LinkField = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By person_LookUpButton = By.Id("CWLookupBtn_personid");
        readonly By person_RemoveButton = By.Id("CWClearLookup_personid");
        readonly By person_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_personid']/label/span");

        readonly By ResponsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_RemoveButton = By.Id("CWClearLookup_ownerid");
        readonly By ResponsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");

        readonly By AllergyType_FieldHeader = By.XPath("//*[@id='CWLabelHolder_allergytypeid']/label");
        readonly By AllergyType_LinkField = By.XPath("//*[@id='CWField_allergytypeid_Link']");
        readonly By AllergyType_LookUpButton = By.Id("CWLookupBtn_allergytypeid");
        readonly By AllergyType_RemoveButton = By.Id("CWClearLookup_allergytypeid");
        readonly By AllergyType_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_allergytypeid']/label/span");

        readonly By AllergenSNOMEDCT_FieldHeader = By.XPath("//*[@id='CWLabelHolder_snomedctid']/label");
        readonly By AllergenSNOMEDCT_LinkField = By.XPath("//*[@id='CWField_snomedctid_Link']");
        readonly By AllergenSNOMEDCT_LookUpButton = By.Id("CWLookupBtn_snomedctid");
        readonly By AllergenSNOMEDCT_RemoveButton = By.Id("CWClearLookup_snomedctid");
        readonly By AllergenSNOMEDCT_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_snomedctid']/label/span");

        readonly By AllergenWhatSubstanceCausedTheReaction_FieldHeader = By.XPath("//*[@id='CWLabelHolder_allergendetails']/label");
        readonly By AllergenWhatSubstanceCausedTheReaction_Field = By.XPath("//*[@id='CWField_allergendetails']");
        readonly By AllergenWhatSubstanceCausedTheReaction_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_allergendetails']/label/span");

        readonly By Level_FieldHeader = By.XPath("//*[@id='CWLabelHolder_allergicreactionlevelid']/label");
        readonly By Level_Field = By.Id("CWField_allergicreactionlevelid");
        readonly By Level_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_allergicreactionlevelid']/label/span");

        readonly By Description_FieldHeader = By.XPath("//*[@id='CWLabelHolder_description']/label");
        readonly By Description_Field = By.Id("CWField_description");

        readonly By StartDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By StartDate_Field = By.Id("CWField_startdate");
        readonly By StartDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_startdate']/label/span");

        readonly By EndDate_FieldHeader = By.XPath("//*[@id='CWLabelHolder_enddate']/label");
        readonly By EndDate_Field = By.Id("CWField_enddate");

        readonly By InformedBy_FieldHeader = By.XPath("//*[@id='CWLabelHolder_allergyinformerid']/label");
        readonly By InformedBy_LinkField = By.XPath("//*[@id='CWField_allergyinformerid_Link']");
        readonly By InformedBy_LookUpButton = By.Id("CWLookupBtn_allergyinformerid");
        readonly By InformedBy_RemoveButton = By.Id("CWClearLookup_allergyinformerid");

        readonly By ObservedBy_FieldHeader = By.XPath("//*[@id='CWLabelHolder_observedbyid']/label");
        readonly By ObservedBy_LinkField = By.XPath("//*[@id='CWField_observedbyid_Link']");
        readonly By ObservedBy_LookUpButton = By.Id("CWLookupBtn_observedbyid");
        readonly By ObservedBy_RemoveButton = By.Id("CWClearLookup_observedbyid");

        #endregion


        public PersonAllergyRecordPage WaitForSNOMEDPersonAllergyRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(person_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(AllergyType_FieldHeader);
            WaitForElement(AllergenSNOMEDCT_FieldHeader);
            WaitForElement(Level_FieldHeader);
            WaitForElement(Description_FieldHeader);
            WaitForElement(StartDate_FieldHeader);
            WaitForElement(EndDate_FieldHeader);
            WaitForElement(InformedBy_FieldHeader);
            WaitForElement(ObservedBy_FieldHeader);

            return this;
        }

        public PersonAllergyRecordPage WaitForPersonAllergyRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(person_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(AllergyType_FieldHeader);
            WaitForElementNotVisible(AllergenSNOMEDCT_FieldHeader, 3);
            WaitForElementNotVisible(AllergenSNOMEDCT_LookUpButton, 3);
            WaitForElementNotVisible(AllergenSNOMEDCT_RemoveButton, 3);
            WaitForElementNotVisible(AllergenSNOMEDCT_LinkField, 3);
            WaitForElementVisible(AllergenWhatSubstanceCausedTheReaction_FieldHeader);
            WaitForElement(Level_FieldHeader);
            WaitForElement(Description_FieldHeader);
            WaitForElement(StartDate_FieldHeader);
            WaitForElement(EndDate_FieldHeader);
            WaitForElement(InformedBy_FieldHeader);
            WaitForElement(ObservedBy_FieldHeader);

            return this;
        }
        public PersonAllergyRecordPage WaitForPersonAllergyRecordPageToLoad(string PageTitle)
        {
            WaitForPersonAllergyRecordPageToLoad();

            ValidateElementText(pageHeader, "Person Allergy:\r\n" + PageTitle);

            return this;
        }

        public PersonAllergyRecordPage WaitForSNOMEDPersonAllergyRecordPageToLoad(string PageTitle)
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Person Allergy:\r\n" + PageTitle);

            WaitForElement(person_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader );
            WaitForElement(AllergyType_FieldHeader);
            WaitForElement(AllergenSNOMEDCT_FieldHeader);
            WaitForElement(Level_FieldHeader);
            WaitForElement(Description_FieldHeader);
            WaitForElement(StartDate_FieldHeader);
            WaitForElement(EndDate_FieldHeader);
            WaitForElement(InformedBy_FieldHeader);
            WaitForElement(ObservedBy_FieldHeader);
            

            return this;
        }
        public PersonAllergyRecordPage WaitForSNOMEDPersonAllergyRecordPageToLoadFromAdvancedSearch(string PageTitle)
        {
            SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDataFormDialog);
            this.SwitchToIframe(iframe_CWDataFormDialog);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Person Allergy:\r\n" + PageTitle);

            WaitForElement(person_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(AllergyType_FieldHeader);
            WaitForElement(AllergenSNOMEDCT_FieldHeader);
            WaitForElement(Level_FieldHeader);
            WaitForElement(Description_FieldHeader);
            WaitForElement(StartDate_FieldHeader);
            WaitForElement(EndDate_FieldHeader);
            WaitForElement(InformedBy_FieldHeader);
            WaitForElement(ObservedBy_FieldHeader);


            return this;
        }

        public PersonAllergyRecordPage WaitForNoneKnownPersonAllergyRecordPageToLoad(string PageTitle)
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Person Allergy:\r\n" + PageTitle);

            WaitForElement(person_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(AllergyType_FieldHeader);
            WaitForElementNotVisible(Level_FieldHeader, 3);
            WaitForElementNotVisible(Description_FieldHeader, 3);
            WaitForElement(StartDate_FieldHeader);
            WaitForElement(EndDate_FieldHeader);
            WaitForElement(InformedBy_FieldHeader);
            WaitForElement(ObservedBy_FieldHeader);


            return this;
        }



        public PersonAllergyRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public PersonAllergyRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }
        public PersonAllergyRecordPage ClickDeleteButton()
        {
            WaitForElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            WaitForElementVisible(DeleteButton);
            Click(DeleteButton);
            return this;
        }
        public PersonAllergyRecordPage ClickBackButton()
        {

            WaitForElementVisible(Back_Button);
            Click(Back_Button);

            return this;
        }



        public PersonAllergyRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
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
        public PersonAllergyRecordPage ValidatePersonFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(person_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(person_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAllergyRecordPage ValidateResponsibleTeamFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ResponsibleTeam_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ResponsibleTeam_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAllergyRecordPage ValidateAllergyTypeFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(AllergyType_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(StartDate_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAllergyRecordPage ValidateAllergenSNOMEDCTFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(AllergenSNOMEDCT_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(AllergenSNOMEDCT_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAllergyRecordPage ValidateAllergenWhatSubstanceCausedTheReactionFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(AllergenWhatSubstanceCausedTheReaction_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(AllergenWhatSubstanceCausedTheReaction_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAllergyRecordPage ValidateLevelFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Level_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Level_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAllergyRecordPage ValidateStartDateFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(StartDate_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(StartDate_FieldErrorLabel, 3);
            }

            return this;
        }
        



        public PersonAllergyRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidatePersonFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(person_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateResponsibleTeamFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateAllergyTypeFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AllergyType_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateAllergenSNOMEDCTFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AllergenSNOMEDCT_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateAllergenWhatSubstanceCausedTheReactionFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AllergenWhatSubstanceCausedTheReaction_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateLevelFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Level_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateStartDateFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(StartDate_FieldErrorLabel, ExpectedText);

            return this;
        }




        public PersonAllergyRecordPage SelectLevel(string TextToSelect)
        {
            WaitForElement(Level_Field);
            SelectPicklistElementByText(Level_Field, TextToSelect);


            return this;
        }

        public PersonAllergyRecordPage InsertAllergenWhatSubstanceCausedTheReaction(string TextToInsert)
        {
            WaitForElement(AllergenWhatSubstanceCausedTheReaction_Field);
            SendKeys(AllergenWhatSubstanceCausedTheReaction_Field, TextToInsert);


            return this;
        }
        public PersonAllergyRecordPage InsertDescription(string TextToInsert)
        {
            WaitForElement(Description_Field);
            SendKeys(Description_Field, TextToInsert);


            return this;
        }
        public PersonAllergyRecordPage InsertStartDate(string TextToInsert)
        {
            WaitForElement(StartDate_Field);
            SendKeys(StartDate_Field, TextToInsert);


            return this;
        }
        public PersonAllergyRecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElement(EndDate_Field);
            SendKeys(EndDate_Field, TextToInsert);


            return this;
        }



        public PersonAllergyRecordPage ClickPersonLookUpButton()
        {
            WaitForElementToBeClickable(person_LookUpButton);
            Click(person_LookUpButton);

            return this;
        }
        public PersonAllergyRecordPage ClickPersonRemoveButton()
        {
            WaitForElementToBeClickable(person_RemoveButton);
            Click(person_RemoveButton);

            return this;
        }
        public PersonAllergyRecordPage ClickResponsibleTeamLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookUpButton);
            Click(ResponsibleTeam_LookUpButton);

            return this;
        }
        public PersonAllergyRecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButton);
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }
        public PersonAllergyRecordPage ClickAllergyTypeLookUpButton()
        {
            WaitForElementToBeClickable(AllergyType_LookUpButton);
            Click(AllergyType_LookUpButton);

            return this;
        }
        public PersonAllergyRecordPage ClickAllergyTypeRemoveButton()
        {
            WaitForElementToBeClickable(AllergyType_RemoveButton);
            Click(AllergyType_RemoveButton);

            return this;
        }
        public PersonAllergyRecordPage ClickAllergenSNOMEDCTLookUpButton()
        {
            WaitForElementToBeClickable(AllergenSNOMEDCT_LookUpButton);
            Click(AllergenSNOMEDCT_LookUpButton);

            return this;
        }
        public PersonAllergyRecordPage ClickAllergenSNOMEDCTRemoveButton()
        {
            WaitForElementToBeClickable(AllergenSNOMEDCT_RemoveButton);
            Click(AllergenSNOMEDCT_RemoveButton);

            return this;
        }
        public PersonAllergyRecordPage ClickInformedByLookUpButton()
        {
            WaitForElementToBeClickable(InformedBy_LookUpButton);
            Click(InformedBy_LookUpButton);

            return this;
        }
        public PersonAllergyRecordPage ClickInformedByRemoveButton()
        {
            WaitForElementToBeClickable(InformedBy_RemoveButton);
            Click(InformedBy_RemoveButton);

            return this;
        }
        public PersonAllergyRecordPage ClickObservedByLookUpButton()
        {
            WaitForElementToBeClickable(ObservedBy_LookUpButton);
            Click(ObservedBy_LookUpButton);

            return this;
        }
        public PersonAllergyRecordPage ClickObservedByRemoveButton()
        {
            WaitForElementToBeClickable(ObservedBy_RemoveButton);
            Click(ObservedBy_RemoveButton);

            return this;
        }





        public PersonAllergyRecordPage ValidatePersonLinkFieldText(string ExpectedText)
        {
            ValidateElementText(person_LinkField, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateAllergyTypeLinkFieldText(string ExpectedText)
        {
            ValidateElementText(AllergyType_LinkField, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateAllergenSNOMEDCTLinkFieldText(string ExpectedText)
        {
            ValidateElementText(AllergenSNOMEDCT_LinkField, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateLevelSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Level_Field, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateAllergenWhatSubstanceCausedTheReaction(string ExpectedText)
        {
            ValidateElementText(AllergenWhatSubstanceCausedTheReaction_Field, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateDescriptionFieldText(string ExpectedText)
        {
            ValidateElementText(Description_Field, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateStartDate(string ExpectedText)
        {
            ValidateElementValue(StartDate_Field, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateEndDate(string ExpectedText)
        {
            ValidateElementValue(EndDate_Field, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateInformedByLinkFieldText(string ExpectedText)
        {
            ValidateElementText(InformedBy_LinkField, ExpectedText);

            return this;
        }
        public PersonAllergyRecordPage ValidateObservedByLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ObservedBy_LinkField, ExpectedText);

            return this;
        }

    }
}
