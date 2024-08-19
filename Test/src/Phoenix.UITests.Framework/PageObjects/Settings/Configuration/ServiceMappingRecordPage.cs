using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.WebAppAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServiceMappingRecordPage : CommonMethods
    {
        public ServiceMappingRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=servicemapping')]");

        readonly By ServiceMappingRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        #region option toolbar

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By deactivateButton = By.Id("TI_DeactivateButton");
        readonly By activateButton = By.Id("TI_ActivateButton");

        #endregion

        #region Details area

        readonly By ServiceElement1_FieldLabel = By.XPath("//*[@id='CWLabelHolder_serviceelement1id']/label");
        readonly By ServiceElement1_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_serviceelement1id']");
        readonly By ServiceElement1_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_serviceelement1id']");
        readonly By ServiceElement1_LinkField = By.XPath("//*[@id='CWField_serviceelement1id_Link']");

        readonly By ServiceElement2_FieldLabel = By.XPath("//*[@id='CWLabelHolder_serviceelement2id']/label");
        readonly By ServiceElement2_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_serviceelement2id']");
        readonly By ServiceElement2_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_serviceelement2id']");
        readonly By ServiceElement2_LinkField = By.XPath("//*[@id='CWField_serviceelement2id_Link']");

        readonly By CareType_FieldLabel = By.XPath("//*[@id='CWLabelHolder_caretypeid']/label");
        readonly By CareType_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_caretypeid']");
        readonly By CareType_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_caretypeid']");
        readonly By CareType_LinkField = By.XPath("//*[@id='CWField_caretypeid_Link']");

        readonly By ResponsibleTeam_FieldLabel = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ResponsibleTeam_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");

        readonly By AvailableOnProviderPortal_FieldLabel = By.XPath("//*[@id='CWLabelHolder_availableonproviderportal']/label");
        readonly By AvailableOnProviderPortal_YesRadioButton = By.XPath("//*[@id='CWField_availableonproviderportal_1']");
        readonly By AvailableOnProviderPortal_NoRadioButton = By.XPath("//*[@id='CWField_availableonproviderportal_0']");


        readonly By Placement_FieldLabel = By.XPath("//*[@id='CWLabelHolder_lacplacement']/label");
        readonly By Placement_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_lacplacement']");
        By PlacementOption_Field(string Identifier) => By.XPath("//*[@id='MS_lacplacement_" + Identifier + "']");
        By PlacementOption_RemoveButton(string Identifier) => By.XPath("//*[@id='MS_lacplacement_" + Identifier + "']/a[text()='Remove']");


        readonly By PersonalBudgetType_FieldLabel = By.XPath("//*[@id='CWLabelHolder_personalbudgettype']/label");
        readonly By PersonalBudgetType_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_personalbudgettype']");
        By PersonalBudgetTypeOption_Field(string Identifier) => By.XPath("//*[@id='MS_personalbudgettype_" + Identifier + "']");
        By PersonalBudgetTypeOption_RemoveButton(string Identifier) => By.XPath("//*[@id='MS_personalbudgettype_" + Identifier + "']/a[text()='Remove']");


        readonly By Notes_FieldLabel = By.XPath("//*[@id='CWLabelHolder_notes']/label");
        readonly By Notes_Field = By.XPath("//*[@id='CWField_notes']");

        #endregion




        public ServiceMappingRecordPage WaitForServiceMappingRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(ServiceMappingRecordPageHeader);

            return this;
        }


        public ServiceMappingRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public ServiceMappingRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            return this;
        }
        public ServiceMappingRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public ServiceMappingRecordPage ClickDeactivateButton()
        {
            MoveToElementInPage(deactivateButton);
            WaitForElementToBeClickable(deactivateButton);            
            Click(deactivateButton);

            return this;
        }

        public ServiceMappingRecordPage ClickDeleteButton(bool Toolbar = false)
        {
            if (Toolbar)
            {
                WaitForElementToBeClickable(additionalToolbarElementsButton);
                Click(additionalToolbarElementsButton);
            }

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public ServiceMappingRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(deactivateButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }
        public ServiceMappingRecordPage ValidateNewRecordFieldsVisible()
        {
            WaitForElementVisible(ServiceElement1_FieldLabel);
            WaitForElementVisible(ServiceElement1_LookupButtonField);

            WaitForElementVisible(AvailableOnProviderPortal_FieldLabel);
            WaitForElementVisible(AvailableOnProviderPortal_YesRadioButton);
            WaitForElementVisible(AvailableOnProviderPortal_NoRadioButton);

            WaitForElementVisible(ResponsibleTeam_FieldLabel);
            WaitForElementVisible(ResponsibleTeam_LookupButtonField);

            WaitForElementVisible(Placement_FieldLabel);
            WaitForElementVisible(Placement_LookupButtonField);

            WaitForElementVisible(PersonalBudgetType_FieldLabel);
            WaitForElementVisible(PersonalBudgetType_LookupButtonField);

            WaitForElementVisible(Notes_FieldLabel);
            WaitForElementVisible(Notes_Field);

            return this;
        }


        public ServiceMappingRecordPage InsertNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Notes_Field);
            SendKeys(Notes_Field, TextToInsert);

            return this;
        }


        public ServiceMappingRecordPage ClickAvailableOnProviderPortalYesOption()
        {
            WaitForElementToBeClickable(AvailableOnProviderPortal_YesRadioButton);
            Click(AvailableOnProviderPortal_YesRadioButton);

            return this;
        }
        public ServiceMappingRecordPage ClickAvailableOnProviderPortalNoOption()
        {
            WaitForElementToBeClickable(AvailableOnProviderPortal_NoRadioButton);
            Click(AvailableOnProviderPortal_NoRadioButton);

            return this;
        }
       

        public ServiceMappingRecordPage ClickServiceElement1LookupButton()
        {
            WaitForElementToBeClickable(ServiceElement1_LookupButtonField);
            Click(ServiceElement1_LookupButtonField);

            return this;
        }
        public ServiceMappingRecordPage ClickServiceElement1RemoveButton()
        {
            WaitForElementToBeClickable(ServiceElement1_RemoveButtonField);
            Click(ServiceElement1_RemoveButtonField);

            return this;
        }
        public ServiceMappingRecordPage ClickServiceElement2LookupButton()
        {
            WaitForElementToBeClickable(ServiceElement2_LookupButtonField);
            Click(ServiceElement2_LookupButtonField);

            return this;
        }
        public ServiceMappingRecordPage ClickServiceElement2RemoveButton()
        {
            WaitForElementToBeClickable(ServiceElement2_RemoveButtonField);
            Click(ServiceElement2_RemoveButtonField);

            return this;
        }
        public ServiceMappingRecordPage ClickCareTypeLookupButton()
        {
            WaitForElementToBeClickable(CareType_LookupButtonField);
            Click(CareType_LookupButtonField);

            return this;
        }
        public ServiceMappingRecordPage ClickCareTypeRemoveButton()
        {
            WaitForElementToBeClickable(CareType_RemoveButtonField);
            Click(CareType_RemoveButtonField);

            return this;
        }
        public ServiceMappingRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButtonField);
            Click(ResponsibleTeam_LookupButtonField);

            return this;
        }
        public ServiceMappingRecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButtonField);
            Click(ResponsibleTeam_RemoveButtonField);

            return this;
        }
        public ServiceMappingRecordPage ClickPlacementLookupButton()
        {
            WaitForElementToBeClickable(Placement_LookupButtonField);
            Click(Placement_LookupButtonField);

            return this;
        }
        public ServiceMappingRecordPage ClickPersonalBudgetTypeLookupButton()
        {
            WaitForElementToBeClickable(PersonalBudgetType_LookupButtonField);
            Click(PersonalBudgetType_LookupButtonField);

            return this;
        }



        public ServiceMappingRecordPage ValidateServiceElement1LookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ServiceElement1_LookupButtonField);
            else
                WaitForElementNotVisible(ServiceElement1_LookupButtonField, 3);

            return this;
        }
        public ServiceMappingRecordPage ValidateServiceElement1LinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ServiceElement1_LinkField);
            else
                WaitForElementNotVisible(ServiceElement1_LinkField, 3);

            return this;
        }
        public ServiceMappingRecordPage ValidateServiceElement2LookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ServiceElement2_LookupButtonField);
            else
                WaitForElementNotVisible(ServiceElement2_LookupButtonField, 3);

            return this;
        }
        public ServiceMappingRecordPage ValidateServiceElement2LinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ServiceElement2_LinkField);
            else
                WaitForElementNotVisible(ServiceElement2_LinkField, 3);

            return this;
        }
        public ServiceMappingRecordPage ValidateCareTypeLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CareType_LookupButtonField);
            else
                WaitForElementNotVisible(CareType_LookupButtonField, 3);

            return this;
        }
        public ServiceMappingRecordPage ValidateCareTypeLinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CareType_LinkField);
            else
                WaitForElementNotVisible(CareType_LinkField, 3);

            return this;
        }
        public ServiceMappingRecordPage ValidateResponsibleTeamLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_LookupButtonField);
            else
                WaitForElementNotVisible(ResponsibleTeam_LookupButtonField, 3);

            return this;
        }
        public ServiceMappingRecordPage ValidateResponsibleTeamLinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_LinkField);
            else
                WaitForElementNotVisible(ResponsibleTeam_LinkField, 3);

            return this;
        }
        public ServiceMappingRecordPage ValidatePlacementLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                Assert.IsTrue(GetElementVisibility(Placement_LookupButtonField));
            }
            else
            {
                WaitForElementNotVisible(Placement_LookupButtonField, 5);
            }
            return this;
        }
        public ServiceMappingRecordPage ValidatePersonalBudgetTypeLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                Assert.IsTrue(GetElementVisibility(PersonalBudgetType_LookupButtonField));
            }
            else
            {
                WaitForElementNotVisible(PersonalBudgetType_LookupButtonField, 5);
            }
            return this;
        }
        public ServiceMappingRecordPage ValidateAvailableOnProviderPortalOptionsVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(AvailableOnProviderPortal_YesRadioButton);
                WaitForElementVisible(AvailableOnProviderPortal_NoRadioButton);
            }
            else
            {
                WaitForElementNotVisible(AvailableOnProviderPortal_YesRadioButton, 5);
                WaitForElementNotVisible(AvailableOnProviderPortal_NoRadioButton, 5);
            }

            return this;
        }
        public ServiceMappingRecordPage ValidateNotesFieldIsDisplayed(bool ExpectVisible)
        {
            if (ExpectVisible)
                Assert.IsTrue(GetElementVisibility(Notes_Field));
            else
                WaitForElementNotVisible(Notes_Field, 5);

            return this;
        }


        public ServiceMappingRecordPage ValidateServiceElement1LinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(ServiceElement1_LinkField);
            WaitForElementToBeClickable(ServiceElement1_LinkField);
            ValidateElementText(ServiceElement1_LinkField, ExpectedText);

            return this;
        }
        public ServiceMappingRecordPage ValidateServiceElement2LinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(ServiceElement2_LinkField);
            WaitForElementToBeClickable(ServiceElement2_LinkField);
            ValidateElementText(ServiceElement2_LinkField, ExpectedText);

            return this;
        }
        public ServiceMappingRecordPage ValidateCareTypeLinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(CareType_LinkField);
            WaitForElementToBeClickable(CareType_LinkField);
            ValidateElementText(CareType_LinkField, ExpectedText);

            return this;
        }
        public ServiceMappingRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(ResponsibleTeam_LinkField);
            WaitForElementToBeClickable(ResponsibleTeam_LinkField);
            ValidateElementTextContainsText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }

        
        public ServiceMappingRecordPage ValidateNotesFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(Notes_Field);
            WaitForElementToBeClickable(Notes_Field);
            ValidateElementValue(Notes_Field, ExpectedValue);

            return this;
        }


        public ServiceMappingRecordPage ValidateAvailableOnProviderPortalYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(AvailableOnProviderPortal_YesRadioButton);
                ValidateElementNotChecked(AvailableOnProviderPortal_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(AvailableOnProviderPortal_YesRadioButton);
                ValidateElementChecked(AvailableOnProviderPortal_NoRadioButton);
            }

            return this;
        }


        public ServiceMappingRecordPage ValidatePlacementOptionVisible(string OptionIdentifier, string ExpectedText)
        {
            WaitForElementVisible(PlacementOption_Field(OptionIdentifier));

            WaitForElementVisible(PlacementOption_RemoveButton(OptionIdentifier));

            ValidateElementText(PlacementOption_Field(OptionIdentifier), ExpectedText+ "\r\nRemove");


            return this;
        }
        public ServiceMappingRecordPage ValidatePersonalBudgetTypeOptionVisible(string OptionIdentifier, string ExpectedText)
        {
            WaitForElementVisible(PersonalBudgetTypeOption_Field(OptionIdentifier));

            WaitForElementVisible(PersonalBudgetTypeOption_RemoveButton(OptionIdentifier));

            ValidateElementText(PersonalBudgetTypeOption_Field(OptionIdentifier), ExpectedText+"\r\nRemove");


            return this;
        }


        public ServiceMappingRecordPage ClickPlacementOptionRemoveButton(string OptionIdentifier)
        {
            WaitForElementVisible(PlacementOption_RemoveButton(OptionIdentifier));
            Click(PlacementOption_RemoveButton(OptionIdentifier));

            return this;
        }
        public ServiceMappingRecordPage ClickPersonalBudgetTypeOptionRemoveButton(string OptionIdentifier)
        {
            WaitForElementVisible(PersonalBudgetTypeOption_RemoveButton(OptionIdentifier));
            Click(PersonalBudgetTypeOption_RemoveButton(OptionIdentifier));

            return this;
        }


        public ServiceMappingRecordPage ValidateAvailableOnProviderPortalOptionsAreDisabled()
        {
            ValidateElementDisabled(AvailableOnProviderPortal_YesRadioButton);
            ValidateElementDisabled(AvailableOnProviderPortal_NoRadioButton);

            return this;
        }


        public ServiceMappingRecordPage ValidateServiceElement1FieldIsDisabled()
        {
            MoveToElementInPage(ServiceElement1_LookupButtonField);
            ValidateElementDisabled(ServiceElement1_LookupButtonField);

            return this;
        }
        public ServiceMappingRecordPage ValidateServiceElement2FieldIsDisabled()
        {
            MoveToElementInPage(ServiceElement2_LookupButtonField);
            ValidateElementDisabled(ServiceElement2_LookupButtonField);

            return this;
        }
        public ServiceMappingRecordPage ValidateCareTypeFieldIsDisabled()
        {
            MoveToElementInPage(CareType_LookupButtonField);
            ValidateElementDisabled(CareType_LookupButtonField);

            return this;
        }
        public ServiceMappingRecordPage ValidateResponsibleTeamFieldIsDisabled()
        {
            WaitForElementVisible(ResponsibleTeam_LookupButtonField);
            MoveToElementInPage(ResponsibleTeam_LookupButtonField);
            ValidateElementDisabled(ResponsibleTeam_LookupButtonField);

            return this;
        }
        public ServiceMappingRecordPage ValidatePlacementLookupFieldButtonIsDisabled()
        {
            MoveToElementInPage(Placement_LookupButtonField);
            WaitForElementVisible(Placement_LookupButtonField);
            ValidateElementDisabled(Placement_LookupButtonField);

            return this;
        }
        public ServiceMappingRecordPage ValidatePersonalBudgetTypeLookupFieldButtonIsDisabled()
        {
            MoveToElementInPage(PersonalBudgetType_LookupButtonField);
            WaitForElementVisible(PersonalBudgetType_LookupButtonField);
            ValidateElementDisabled(PersonalBudgetType_LookupButtonField);

            return this;
        }
        public ServiceMappingRecordPage ValidateNotesFieldIsDisabled()
        {
            MoveToElementInPage(Notes_Field);
            WaitForElementVisible(Notes_Field);
            ValidateElementDisabled(Notes_Field);

            return this;
        }

        public ServiceMappingRecordPage ValidateRecordTitle(string PageTitle)
        {

            MoveToElementInPage(ServiceMappingRecordPageHeader);
            ValidateElementTextContainsText(ServiceMappingRecordPageHeader, PageTitle);

            return this;
        }

        public ServiceMappingRecordPage ClickActivateButton()
        {
            MoveToElementInPage(activateButton);
            WaitForElementToBeClickable(activateButton);
            Click(activateButton);

            return this;
        }

        public ServiceMappingRecordPage ValidateActiveButtonFieldIsDisplayed(bool ExpectVisible)
        {
            if (ExpectVisible)
                Assert.IsTrue(GetElementVisibility(activateButton));
            else
                WaitForElementNotVisible(activateButton, 5);

            return this;
        }

        public ServiceMappingRecordPage ValidateDeactiveButtonFieldIsDisplayed(bool ExpectVisible)
        {
            if (ExpectVisible)
                Assert.IsTrue(GetElementVisibility(deactivateButton));
            else
                WaitForElementNotVisible(deactivateButton, 5);

            return this;
        }
    }
}