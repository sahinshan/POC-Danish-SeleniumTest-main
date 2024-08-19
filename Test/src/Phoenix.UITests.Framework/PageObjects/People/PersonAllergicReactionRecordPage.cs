using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonAllergicReactionRecordPage : CommonMethods
    {
        public PersonAllergicReactionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


  

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personallergicreaction')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By saveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By saveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");


        #region Fields

        readonly By Allergy_FieldHeader = By.Id("CWLabelHolder_personallergyid");
        readonly By Allergy_LinkField = By.XPath("//*[@id='CWField_personallergyid_Link']");
        readonly By Allergy_LookUpButton = By.Id("CWLookupBtn_personallergyid");
        readonly By Allergy_RemoveButton = By.Id("CWClearLookup_personallergyid");
        readonly By Allergy_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_personallergyid']/label/span");

        readonly By ResponsibleTeam_FieldHeader = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_RemoveButton = By.Id("CWClearLookup_ownerid");
        readonly By ResponsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");

        readonly By Reaction_FieldHeader = By.XPath("//*[@id='CWLabelHolder_allergicreactiontypeid']/label");
        readonly By Reaction_LinkField = By.XPath("//*[@id='CWField_allergicreactiontypeid_Link']");
        readonly By Reaction_LookUpButton = By.Id("CWLookupBtn_allergicreactiontypeid");
        readonly By Reaction_RemoveButton = By.Id("CWClearLookup_allergicreactiontypeid");
        readonly By Reaction_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_allergicreactiontypeid']/label/span");

        readonly By ReactionSNOMEDCT_FieldHeader = By.XPath("//*[@id='CWLabelHolder_snomedctid']/label");
        readonly By ReactionSNOMEDCT_LinkField = By.XPath("//*[@id='CWField_snomedctid_Link']");
        readonly By ReactionSNOMEDCT_LookUpButton = By.Id("CWLookupBtn_snomedctid");
        readonly By ReactionSNOMEDCT_RemoveButton = By.Id("CWClearLookup_snomedctid");
        readonly By ReactionSNOMEDCT_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_snomedctid']/label/span");

        readonly By Description_FieldHeader = By.XPath("//*[@id='CWLabelHolder_description']/label");
        readonly By Description_Field = By.Id("CWField_description");

        readonly By Primary_FieldHeader = By.XPath("//*[@id='CWLabelHolder_primary']/label");
        readonly By Primary_YesRadioButton = By.Id("CWField_primary_1");
        readonly By Primary_NoRadioButton = By.Id("CWField_primary_0");

        #endregion


        public PersonAllergicReactionRecordPage WaitForPersonAllergicReactionRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(Allergy_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(Reaction_FieldHeader);
            WaitForElement(Description_FieldHeader);
            WaitForElement(Primary_FieldHeader);

            WaitForElementNotVisible(ReactionSNOMEDCT_FieldHeader, 3);
            WaitForElementNotVisible(ReactionSNOMEDCT_LinkField, 3);
            WaitForElementNotVisible(ReactionSNOMEDCT_LookUpButton, 3);
            WaitForElementNotVisible(ReactionSNOMEDCT_RemoveButton, 3);
            WaitForElementNotVisible(ReactionSNOMEDCT_FieldErrorLabel, 3);

            return this;
        }

        public PersonAllergicReactionRecordPage WaitForPersonAllergicReactionRecordPageToLoad(string PageTitle)
        {
            SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Person Allergy: " + PageTitle);

            WaitForElement(Allergy_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(Reaction_FieldHeader);
            WaitForElement(Description_FieldHeader);
            WaitForElement(Primary_FieldHeader);

            WaitForElementNotVisible(ReactionSNOMEDCT_FieldHeader, 3);
            WaitForElementNotVisible(ReactionSNOMEDCT_LinkField, 3);
            WaitForElementNotVisible(ReactionSNOMEDCT_LookUpButton, 3);
            WaitForElementNotVisible(ReactionSNOMEDCT_RemoveButton, 3);
            WaitForElementNotVisible(ReactionSNOMEDCT_FieldErrorLabel, 3);


            return this;
        }

        public PersonAllergicReactionRecordPage WaitForSNOMEDPersonAllergicReactionRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(Allergy_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ReactionSNOMEDCT_FieldHeader);
            WaitForElement(Description_FieldHeader);
            WaitForElement(Primary_FieldHeader);

            WaitForElementNotVisible(Reaction_FieldHeader, 3);
            WaitForElementNotVisible(Reaction_LinkField, 3);
            WaitForElementNotVisible(Reaction_LookUpButton, 3);
            WaitForElementNotVisible(Reaction_RemoveButton, 3);
            WaitForElementNotVisible(Reaction_FieldErrorLabel, 3);

            return this;
        }

        public PersonAllergicReactionRecordPage WaitForSNOMEDPersonAllergicReactionRecordPageToLoad(string PageTitle)
        {
            SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Person Allergy: " + PageTitle);

            WaitForElement(Allergy_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ReactionSNOMEDCT_FieldHeader);
            WaitForElement(Description_FieldHeader);
            WaitForElement(Primary_FieldHeader);

            WaitForElementNotVisible(Reaction_FieldHeader, 3);
            WaitForElementNotVisible(Reaction_LinkField, 3);
            WaitForElementNotVisible(Reaction_LookUpButton, 3);
            WaitForElementNotVisible(Reaction_RemoveButton, 3);
            WaitForElementNotVisible(Reaction_FieldErrorLabel, 3);

            return this;
        }

        public PersonAllergicReactionRecordPage WaitForSNOMEDPersonAllergicReactionRecordPageToLoadFromAdvancedSearch(string PageTitle)
        {
            SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDataFormDialog);
            this.SwitchToIframe(iframe_CWDataFormDialog);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Person Allergy: " + PageTitle);

            WaitForElement(Allergy_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ReactionSNOMEDCT_FieldHeader);
            WaitForElement(Description_FieldHeader);
            WaitForElement(Primary_FieldHeader);

            WaitForElementNotVisible(Reaction_FieldHeader, 3);
            WaitForElementNotVisible(Reaction_LinkField, 3);
            WaitForElementNotVisible(Reaction_LookUpButton, 3);
            WaitForElementNotVisible(Reaction_RemoveButton, 3);
            WaitForElementNotVisible(Reaction_FieldErrorLabel, 3);

            return this;
        }




        public PersonAllergicReactionRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public PersonAllergicReactionRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }
        public PersonAllergicReactionRecordPage ClickDeleteButton()
        {
            WaitForElement(AdditionalItemsButton);
            Click(AdditionalItemsButton);
            WaitForElementVisible(DeleteButton);
            Click(DeleteButton);
            return this;
        }
        public PersonAllergicReactionRecordPage ClickBackButton()
        {

            WaitForElementVisible(Back_Button);
            Click(Back_Button);

            return this;
        }



        public PersonAllergicReactionRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
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
        public PersonAllergicReactionRecordPage ValidateAllergyFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Allergy_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Allergy_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAllergicReactionRecordPage ValidateResponsibleTeamFieldErrorLabelVisibility(bool ExpectVisible)
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
        public PersonAllergicReactionRecordPage ValidateReactionFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Reaction_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Reaction_FieldErrorLabel, 3);
            }

            return this;
        }
        public PersonAllergicReactionRecordPage ValidateReactionSNOMEDFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ReactionSNOMEDCT_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ReactionSNOMEDCT_FieldErrorLabel, 3);
            }

            return this;
        }
        

        



        public PersonAllergicReactionRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }
        public PersonAllergicReactionRecordPage ValidateAllergyFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Allergy_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAllergicReactionRecordPage ValidateResponsibleTeamFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAllergicReactionRecordPage ValidateReactionFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reaction_FieldErrorLabel, ExpectedText);

            return this;
        }
        public PersonAllergicReactionRecordPage ValidateReactionSNOMEDCTFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ReactionSNOMEDCT_FieldErrorLabel, ExpectedText);

            return this;
        }
      



        public PersonAllergicReactionRecordPage InsertDescription(string TextToInsert)
        {
            WaitForElement(Description_Field);
            SendKeys(Description_Field, TextToInsert);


            return this;
        }



        public PersonAllergicReactionRecordPage ClickAllergyLookUpButton()
        {
            WaitForElementToBeClickable(Allergy_LookUpButton);
            Click(Allergy_LookUpButton);

            return this;
        }
        public PersonAllergicReactionRecordPage ClickAllergyRemoveButton()
        {
            WaitForElementToBeClickable(Allergy_RemoveButton);
            Click(Allergy_RemoveButton);

            return this;
        }
        public PersonAllergicReactionRecordPage ClickResponsibleTeamLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookUpButton);
            Click(ResponsibleTeam_LookUpButton);

            return this;
        }
        public PersonAllergicReactionRecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButton);
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }
        public PersonAllergicReactionRecordPage ClickReactionLookUpButton()
        {
            WaitForElementToBeClickable(Reaction_LookUpButton);
            Click(Reaction_LookUpButton);

            return this;
        }
        public PersonAllergicReactionRecordPage ClickReactionRemoveButton()
        {
            WaitForElementToBeClickable(Reaction_RemoveButton);
            Click(Reaction_RemoveButton);

            return this;
        }
        public PersonAllergicReactionRecordPage ClickReactionSNOMEDCTLookUpButton()
        {
            WaitForElementToBeClickable(ReactionSNOMEDCT_LookUpButton);
            Click(ReactionSNOMEDCT_LookUpButton);

            return this;
        }
        public PersonAllergicReactionRecordPage ClickReactionSNOMEDCTRemoveButton()
        {
            WaitForElementToBeClickable(ReactionSNOMEDCT_RemoveButton);
            Click(ReactionSNOMEDCT_RemoveButton);

            return this;
        }
        public PersonAllergicReactionRecordPage ClickPrimaryYesRadioButton()
        {
            WaitForElementToBeClickable(Primary_YesRadioButton);
            Click(Primary_YesRadioButton);

            return this;
        }
        public PersonAllergicReactionRecordPage ClickPrimaryNoRadioButton()
        {
            WaitForElementToBeClickable(Primary_NoRadioButton);
            Click(Primary_NoRadioButton);

            return this;
        }



        public PersonAllergicReactionRecordPage ValidateAllergyLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Allergy_LinkField, ExpectedText);

            return this;
        }
        public PersonAllergicReactionRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public PersonAllergicReactionRecordPage ValidateReactionLinkFieldText(string ExpectedText)
        {
            ValidateElementText(Reaction_LinkField, ExpectedText);

            return this;
        }
        public PersonAllergicReactionRecordPage ValidateReactionSNOMEDCTLinkFieldText(string ExpectedText)
        {
            ValidateElementText(ReactionSNOMEDCT_LinkField, ExpectedText);

            return this;
        }
        public PersonAllergicReactionRecordPage ValidateDescriptionFieldText(string ExpectedText)
        {
            ValidateElementText(Description_Field, ExpectedText);

            return this;
        }



        public PersonAllergicReactionRecordPage ValidatePrimaryYesRadioButtonChecked(bool ExpectedChecked)
        {
            if(ExpectedChecked)
                ValidateElementChecked(Primary_YesRadioButton);
            else
                ValidateElementNotChecked(Primary_YesRadioButton);

            return this;
        }
        public PersonAllergicReactionRecordPage ValidatePrimaryNoRadioButtonChecked(bool ExpectedChecked)
        {
            if (ExpectedChecked)
                ValidateElementChecked(Primary_NoRadioButton);
            else
                ValidateElementNotChecked(Primary_NoRadioButton);

            return this;
        }

    }
}
