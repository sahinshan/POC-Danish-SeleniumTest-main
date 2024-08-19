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
    public class ServiceElement2RecordPage : CommonMethods
    {
        public ServiceElement2RecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_ServiceElement2Frame = By.Id("iframe_serviceelement2");
        readonly By cwDialog_ServiceElement2Frame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceelement2')]");

        #region option toolbar
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        #endregion

        readonly By ServiceElement2RecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        #region Top Menu

        readonly By TopLeftMenu = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");

        readonly By AuditSubMenuLink = By.XPath("//*[@id='CWNavItem_AuditHistory']");         
        readonly By ServiceMappingSubMenuLink = By.XPath("//*[@id='CWNavItem_ServiceMapping']");

        #endregion

        #region General area

        readonly By Name_FieldLabel = By.XPath("//*[@id='CWLabelHolder_name']/label");
        readonly By Name_Field = By.XPath("//*[@id='CWField_name']");

        readonly By Code_FieldLabel = By.XPath("//*[@id='CWLabelHolder_code']/label");
        readonly By Code_Field = By.XPath("//*[@id='CWField_code']");

        readonly By GovCode_FieldLabel = By.XPath("//*[@id='CWLabelHolder_govcode']/label");
        readonly By GovCode_Field = By.XPath("//*[@id='CWField_govcode']");

        readonly By Inactive_FieldLabel = By.XPath("//*[@id='CWLabelHolder_inactive']/label");
        readonly By Inactive_YesRadioButton = By.XPath("//*[@id='CWField_inactive_1']");
        readonly By Inactive_NoRadioButton = By.XPath("//*[@id='CWField_inactive_0']");

        readonly By HealthNursingContribution_FieldLabel = By.XPath("//*[@id='CWLabelHolder_healthnursingcontribution']/label");
        readonly By HealthNursingContribution_YesRadioButton = By.XPath("//*[@id='CWField_healthnursingcontribution_1']");
        readonly By HealthNursingContribution_NoRadioButton = By.XPath("//*[@id='CWField_healthnursingcontribution_0']");

        readonly By StartDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By StartDate_Field = By.XPath("//*[@id='CWField_startdate']");

        readonly By EndDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_enddate']/label");
        readonly By EndDate_Field = By.XPath("//*[@id='CWField_enddate']");

        readonly By ValidForExport_FieldLabel = By.XPath("//*[@id='CWLabelHolder_validforexport']/label");
        readonly By ValidForExport_YesRadioButton = By.XPath("//*[@id='CWField_validforexport_1']");
        readonly By ValidForExport_NoRadioButton = By.XPath("//*[@id='CWField_validforexport_0']");

        readonly By ResponsibleTeam_FieldLabel = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ResponsibleTeam_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");

        readonly By ThirdPartyContribution_FieldLabel = By.XPath("//*[@id='CWLabelHolder_thirdpartycontribution']/label");
        readonly By ThirdPartyContribution_YesRadioButton = By.XPath("//*[@id='CWField_thirdpartycontribution_1']");
        readonly By ThirdPartyContribution_NoRadioButton = By.XPath("//*[@id='CWField_thirdpartycontribution_0']");

        #endregion




        public ServiceElement2RecordPage WaitForServiceElement2RecordPageToLoad(bool serviceElementIFrame = true)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            if (serviceElementIFrame)
            {
                WaitForElement(iframe_ServiceElement2Frame);
                SwitchToIframe(iframe_ServiceElement2Frame);
            }

            WaitForElement(cwDialog_ServiceElement2Frame);
            SwitchToIframe(cwDialog_ServiceElement2Frame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(ServiceElement2RecordPageHeader);

            return this;
        }

        public ServiceElement2RecordPage ValidateNewRecordFieldsVisible()
        {
            WaitForElementVisible(Name_FieldLabel);
            WaitForElementVisible(Name_Field);
            WaitForElementVisible(Code_FieldLabel);
            WaitForElementVisible(Code_Field);
            WaitForElementVisible(GovCode_FieldLabel);
            WaitForElementVisible(GovCode_Field);
            WaitForElementVisible(Inactive_FieldLabel);
            WaitForElementVisible(Inactive_YesRadioButton);
            WaitForElementVisible(Inactive_NoRadioButton);
            WaitForElementVisible(HealthNursingContribution_FieldLabel);
            WaitForElementVisible(HealthNursingContribution_YesRadioButton);
            WaitForElementVisible(HealthNursingContribution_NoRadioButton);

            WaitForElementVisible(StartDate_FieldLabel);
            WaitForElementVisible(StartDate_Field);
            WaitForElementVisible(EndDate_FieldLabel);
            WaitForElementVisible(EndDate_Field);
            WaitForElementVisible(ValidForExport_FieldLabel);
            WaitForElementVisible(ValidForExport_YesRadioButton);
            WaitForElementVisible(ValidForExport_NoRadioButton);
            WaitForElementVisible(ResponsibleTeam_FieldLabel);
            WaitForElementVisible(ResponsibleTeam_LookupButtonField);
            WaitForElementVisible(ThirdPartyContribution_FieldLabel);
            WaitForElementVisible(ThirdPartyContribution_YesRadioButton);
            WaitForElementVisible(ThirdPartyContribution_NoRadioButton);

            return this;
        }

        public ServiceElement2RecordPage ValidateTopMenuLinksVisible()
        {
            WaitForElementToBeClickable(TopLeftMenu);
            Click(TopLeftMenu);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementVisible(AuditSubMenuLink);
            WaitForElementVisible(ServiceMappingSubMenuLink);            

            return this;
        }

        public ServiceElement2RecordPage ClickTopLeftMenu()
        {
            WaitForElementToBeClickable(TopLeftMenu);
            Click(TopLeftMenu);

            return this;
        }


        public ServiceElement2RecordPage InsertName(string TextToInsert)
        {
            WaitForElementToBeClickable(Name_Field);
            SendKeys(Name_Field, TextToInsert);

            return this;
        }

        public ServiceElement2RecordPage InsertCode(string TextToInsert)
        {
            WaitForElementToBeClickable(Code_Field);
            SendKeys(Code_Field, TextToInsert);

            return this;
        }

        public ServiceElement2RecordPage InsertGovCode(string TextToInsert)
        {
            WaitForElementToBeClickable(GovCode_Field);
            SendKeys(GovCode_Field, TextToInsert);

            return this;
        }

        public ServiceElement2RecordPage InsertStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(StartDate_Field);
            SendKeys(StartDate_Field, TextToInsert);

            return this;
        }

        public ServiceElement2RecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(EndDate_Field);
            SendKeys(EndDate_Field, TextToInsert);

            return this;
        }

        public ServiceElement2RecordPage ClickInactiveYesOption()
        {
            WaitForElementToBeClickable(Inactive_YesRadioButton);
            Click(Inactive_YesRadioButton);

            return this;
        }
        public ServiceElement2RecordPage ClickInactiveNoOption()
        {
            WaitForElementToBeClickable(Inactive_NoRadioButton);
            Click(Inactive_NoRadioButton);

            return this;
        }

        public ServiceElement2RecordPage ClickHealthNursingContributionYesOption()
        {
            MoveToElementInPage(HealthNursingContribution_YesRadioButton);
            WaitForElementToBeClickable(HealthNursingContribution_YesRadioButton);
            Click(HealthNursingContribution_YesRadioButton);

            return this;
        }

        public ServiceElement2RecordPage ClickHealthNursingContributionNoOption()
        {
            MoveToElementInPage(HealthNursingContribution_NoRadioButton);
            WaitForElementToBeClickable(HealthNursingContribution_NoRadioButton);
            Click(HealthNursingContribution_NoRadioButton);

            return this;
        }

        public ServiceElement2RecordPage ClickValidForExportYesOption()
        {
            WaitForElementToBeClickable(ValidForExport_YesRadioButton);
            Click(ValidForExport_YesRadioButton);

            return this;
        }

        public ServiceElement2RecordPage ClickValidForExportNoOption()
        {
            WaitForElementToBeClickable(ValidForExport_NoRadioButton);
            Click(ValidForExport_NoRadioButton);

            return this;
        }

        public ServiceElement2RecordPage ClickThirdPartyContributionYesOption()
        {
            MoveToElementInPage(ThirdPartyContribution_YesRadioButton);
            WaitForElementToBeClickable(ThirdPartyContribution_YesRadioButton);
            Click(ThirdPartyContribution_YesRadioButton);

            return this;
        }

        public ServiceElement2RecordPage ClickThirdPartyContributionNoOption()
        {
            MoveToElementInPage(ThirdPartyContribution_NoRadioButton);
            WaitForElementToBeClickable(ThirdPartyContribution_NoRadioButton);
            Click(ThirdPartyContribution_NoRadioButton);

            return this;
        }

        public ServiceElement2RecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButtonField);
            Click(ResponsibleTeam_LookupButtonField);

            return this;
        }

        public ServiceElement2RecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButtonField);
            Click(ResponsibleTeam_RemoveButtonField);

            return this;
        }

        public ServiceElement2RecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public ServiceElement2RecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            return this;
        }

        public ServiceElement2RecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(deleteButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public ServiceElement2RecordPage ValidateResponsibleTeamLinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_LinkField);
            else
                WaitForElementNotVisible(ResponsibleTeam_LinkField, 3);

            return this;
        }

        public ServiceElement2RecordPage ValidateHealthNursingContributionFieldIsDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                MoveToElementInPage(HealthNursingContribution_FieldLabel);
                Assert.IsTrue(GetElementVisibility(HealthNursingContribution_YesRadioButton));
                Assert.IsTrue(GetElementVisibility(HealthNursingContribution_NoRadioButton));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(HealthNursingContribution_YesRadioButton));
                Assert.IsFalse(GetElementVisibility(HealthNursingContribution_NoRadioButton));
            }
            return this;
        }

        public ServiceElement2RecordPage ValidateThirdPartyContributionFieldIsDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                MoveToElementInPage(ThirdPartyContribution_FieldLabel);
                Assert.IsTrue(GetElementVisibility(ThirdPartyContribution_YesRadioButton));
                Assert.IsTrue(GetElementVisibility(ThirdPartyContribution_NoRadioButton));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(ThirdPartyContribution_YesRadioButton));
                Assert.IsFalse(GetElementVisibility(ThirdPartyContribution_NoRadioButton));
            }
            return this;
        }

        public ServiceElement2RecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(ResponsibleTeam_LinkField);
            WaitForElementToBeClickable(ResponsibleTeam_LinkField);
            ValidateElementTextContainsText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public ServiceElement2RecordPage ValidateNameFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(Name_Field);
            WaitForElementToBeClickable(Name_Field);
            ValidateElementValue(Name_Field, ExpectedValue);

            return this;
        }
        public ServiceElement2RecordPage ValidateCodeFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(Code_Field);
            WaitForElementToBeClickable(Code_Field);
            ValidateElementValue(Code_Field, ExpectedValue);

            return this;
        }
        public ServiceElement2RecordPage ValidateGovCodeFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(GovCode_Field);
            WaitForElementToBeClickable(GovCode_Field);
            ValidateElementValue(GovCode_Field, ExpectedValue);

            return this;
        }
        public ServiceElement2RecordPage ValidateStartDateFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(StartDate_Field);
            WaitForElementToBeClickable(StartDate_Field);
            ValidateElementValue(StartDate_Field, ExpectedValue);

            return this;
        }
        public ServiceElement2RecordPage ValidateEndDateFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(EndDate_Field);
            WaitForElementToBeClickable(EndDate_Field);
            ValidateElementValue(EndDate_Field, ExpectedValue);

            return this;
        }

        public ServiceElement2RecordPage ValidateInactiveYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(Inactive_YesRadioButton);
                ValidateElementNotChecked(Inactive_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(Inactive_YesRadioButton);
                ValidateElementChecked(Inactive_NoRadioButton);
            }

            return this;
        }
        public ServiceElement2RecordPage ValidateHealthNursingContributionYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(HealthNursingContribution_YesRadioButton);
                ValidateElementNotChecked(HealthNursingContribution_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(HealthNursingContribution_YesRadioButton);
                ValidateElementChecked(HealthNursingContribution_NoRadioButton);
            }

            return this;
        }
        public ServiceElement2RecordPage ValidateValidForExportYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(ValidForExport_YesRadioButton);
                ValidateElementNotChecked(ValidForExport_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ValidForExport_YesRadioButton);
                ValidateElementChecked(ValidForExport_NoRadioButton);
            }

            return this;
        }
        public ServiceElement2RecordPage ValidateThirdPartyContributionYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(ThirdPartyContribution_YesRadioButton);
                ValidateElementNotChecked(ThirdPartyContribution_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ThirdPartyContribution_YesRadioButton);
                ValidateElementChecked(ThirdPartyContribution_NoRadioButton);
            }

            return this;
        }

        public ServiceElement2RecordPage ClickBackButton()
        {
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ServiceElement2RecordPage ValidateHealthNursingContributionOptionsIsDisabled()
        {
            ValidateElementDisabled(HealthNursingContribution_YesRadioButton);
            ValidateElementDisabled(HealthNursingContribution_NoRadioButton);

            return this;
        }

        public ServiceElement2RecordPage ValidateNameFieldIsDisabled()
        {
            WaitForElementVisible(Name_Field);
            MoveToElementInPage(Name_Field);
            ValidateElementDisabled(Name_Field);

            return this;
        }

        public ServiceElement2RecordPage ValidateCodeFieldIsDisabled()
        {
            WaitForElementVisible(Code_Field);
            MoveToElementInPage(Code_Field);
            ValidateElementDisabled(Code_Field);

            return this;
        }

        public ServiceElement2RecordPage ValidateGovCodeFieldIsDisabled()
        {
            WaitForElementVisible(GovCode_Field);
            MoveToElementInPage(GovCode_Field);
            ValidateElementDisabled(GovCode_Field);

            return this;
        }

        public ServiceElement2RecordPage ValidateStartDateFieldIsDisabled()
        {
            WaitForElementVisible(StartDate_Field);
            MoveToElementInPage(StartDate_Field);
            ValidateElementDisabled(StartDate_Field);

            return this;
        }

        public ServiceElement2RecordPage ValidateEndDateFieldIsDisabled()
        {
            WaitForElementVisible(EndDate_Field);
            MoveToElementInPage(EndDate_Field);
            ValidateElementDisabled(EndDate_Field);

            return this;
        }

        public ServiceElement2RecordPage ValidateInactiveFieldOptionsDisabled()
        {
            WaitForElementVisible(Inactive_YesRadioButton);
            MoveToElementInPage(Inactive_YesRadioButton);
            ValidateElementDisabled(Inactive_YesRadioButton);
            ValidateElementDisabled(Inactive_NoRadioButton);

            return this;
        }

        public ServiceElement2RecordPage ValidateResponsibleTeamFieldIsDisabled()
        {
            WaitForElementVisible(ResponsibleTeam_LookupButtonField);
            MoveToElementInPage(ResponsibleTeam_LookupButtonField);
            ValidateElementDisabled(ResponsibleTeam_LookupButtonField);

            return this;
        }

        public ServiceElement2RecordPage ValidateThirdPartyContribution_OptionsDisabled()
        {
            MoveToElementInPage(ThirdPartyContribution_YesRadioButton);
            ValidateElementDisabled(ThirdPartyContribution_YesRadioButton);
            ValidateElementDisabled(ThirdPartyContribution_NoRadioButton);

            return this;
        }

        public ServiceElement2RecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            MoveToElementInPage(deleteButton);
            Click(deleteButton);

            return this;
        }
    }
}