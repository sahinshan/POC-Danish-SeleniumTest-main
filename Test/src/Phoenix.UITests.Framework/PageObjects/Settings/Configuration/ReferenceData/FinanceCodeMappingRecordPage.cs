using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinanceCodeMappingRecordPage : CommonMethods
    {
        public FinanceCodeMappingRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_CareProviderFinanceCodeMapping = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderfinancecodemapping')]");

        readonly By CWDataFormDialog = By.Id("iframe_CWDataFormDialog");

        readonly By FinanceCodeMappingRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        #region option toolbar

        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By AdditionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By BackButton = By.Id("BackButton");
        readonly By TRunOnDemandWorkflowButton = By.Id("TI_RunOnDemandWorkflow");

        #endregion

        #region General

        readonly By ContractScheme_LabelField = By.XPath("//*[@id='CWLabelHolder_careprovidercontractschemeid']/label[text()='Contract Scheme']");
        readonly By ContractScheme_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careprovidercontractschemeid']/label/span[@class='mandatory']");
        readonly By ContractScheme_LinkField = By.Id("CWField_careprovidercontractschemeid_Link");
        readonly By ContractScheme_LookupButton = By.Id("CWLookupBtn_careprovidercontractschemeid");
        readonly By ContractScheme_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_careprovidercontractschemeid']/label/span");
        readonly By ContractScheme_RemoveButton = By.Id("CWClearLookup_careprovidercontractschemeid");

        readonly By ResponsibleTeam_LabelField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By ResponsibleTeam_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span[@class='mandatory']");
        readonly By ResponsibleTeam_LinkField = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");
        readonly By ResponsibleTeam_RemoveButton = By.Id("CWClearLookup_ownerid");

        readonly By PositionNumber_LabelField = By.XPath("//*[@id='CWLabelHolder_positionnumber']/label[text()='Position Number']");
        readonly By PositionNumber_MandatoryField = By.XPath("//*[@id='CWLabelHolder_positionnumber']/label/span[@class='mandatory']");
        readonly By PositionNumber_TextField = By.Id("CWField_positionnumber");

        readonly By Inactive_LabelField = By.XPath("//*[@id='CWLabelHolder_inactive']/label[text()='Inactive']");
        readonly By Inactive_MandatoryField = By.XPath("//*[@id='CWLabelHolder_inactive']/label/span[@class='mandatory']");
        readonly By Inactive_YesOption = By.Id("CWField_inactive_1");
        readonly By Inactive_NoOption = By.Id("CWField_inactive_0");

        #endregion

        #region Level 1

        readonly By Level1_Label = By.XPath("//div[@id='CWSection_Level1Section']//div[@class='card-header']/span[text()='Level 1']");

        readonly By Level1Location_LabelField = By.XPath("//*[@id='CWLabelHolder_level1id']/label[text()='Level 1 Location']");
        readonly By Level1Location_MandatoryField = By.XPath("//*[@id='CWLabelHolder_level1id']/label/span[@class='mandatory']");
        readonly By Level1Location_LinkField = By.Id("CWField_level1id_Link");
        readonly By Level1Location_LookupButton = By.Id("CWLookupBtn_level1id");
        readonly By Level1Location_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_level1id']/label/span");
        readonly By Level1Location_RemoveButton = By.Id("CWClearLookup_level1id");

        readonly By Level1Constant_LabelField = By.XPath("//*[@id='CWLabelHolder_level1constant']/label[text()='Level 1 Constant']");
        readonly By Level1Constant_MandatoryField = By.XPath("//*[@id='CWLabelHolder_level1constant']/label/span[@class='mandatory']");
        readonly By Level1Constant_TextField = By.Id("CWField_level1constant");

        #endregion

        #region Level 2

        readonly By Level2_Label = By.XPath("//div[@id='CWSection_Level2Section']//div[@class='card-header']/span[text()='Level 2']");

        readonly By Level2Location_LabelField = By.XPath("//*[@id='CWLabelHolder_level2id']/label[text()='Level 2 Location']");
        readonly By Level2Location_MandatoryField = By.XPath("//*[@id='CWLabelHolder_level2id']/label/span[@class='mandatory']");
        readonly By Level2Location_LinkField = By.Id("CWField_level2id_Link");
        readonly By Level2Location_LookupButton = By.Id("CWLookupBtn_level2id");
        readonly By Level2Location_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_level2id']/label/span");
        readonly By Level2Location_RemoveButton = By.Id("CWClearLookup_level2id");

        readonly By Level2Constant_LabelField = By.XPath("//*[@id='CWLabelHolder_level2constant']/label[text()='Level 2 Constant']");
        readonly By Level2Constant_MandatoryField = By.XPath("//*[@id='CWLabelHolder_level2constant']/label/span[@class='mandatory']");
        readonly By Level2Constant_TextField = By.Id("CWField_level2constant");

        #endregion

        #region Level 3

        readonly By Level3_Label = By.XPath("//div[@id='CWSection_Level3Section']//div[@class='card-header']/span[text()='Level 3']");

        readonly By Level3Location_LabelField = By.XPath("//*[@id='CWLabelHolder_level3id']/label[text()='Level 3 Location']");
        readonly By Level3Location_MandatoryField = By.XPath("//*[@id='CWLabelHolder_level3id']/label/span[@class='mandatory']");
        readonly By Level3Location_LinkField = By.Id("CWField_level3id_Link");
        readonly By Level3Location_LookupButton = By.Id("CWLookupBtn_level3id");
        readonly By Level3Location_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_level3id']/label/span");
        readonly By Level3Location_RemoveButton = By.Id("CWClearLookup_level3id");

        readonly By Level3Constant_LabelField = By.XPath("//*[@id='CWLabelHolder_level3constant']/label[text()='Level 3 Constant']");
        readonly By Level3Constant_MandatoryField = By.XPath("//*[@id='CWLabelHolder_level3constant']/label/span[@class='mandatory']");
        readonly By Level3Constant_TextField = By.Id("CWField_level3constant");

        #endregion

        #region Level 4

        readonly By Level4_Label = By.XPath("//div[@id='CWSection_Level4Section']//div[@class='card-header']/span[text()='Level 4']");

        readonly By Level4Location_LabelField = By.XPath("//*[@id='CWLabelHolder_level4id']/label[text()='Level 4 Location']");
        readonly By Level4Location_MandatoryField = By.XPath("//*[@id='CWLabelHolder_level4id']/label/span[@class='mandatory']");
        readonly By Level4Location_LinkField = By.Id("CWField_level4id_Link");
        readonly By Level4Location_LookupButton = By.Id("CWLookupBtn_level4id");
        readonly By Level4Location_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_level4id']/label/span");
        readonly By Level4Location_RemoveButton = By.Id("CWClearLookup_level4id");

        readonly By Level4Constant_LabelField = By.XPath("//*[@id='CWLabelHolder_level4constant']/label[text()='Level 4 Constant']");
        readonly By Level4Constant_MandatoryField = By.XPath("//*[@id='CWLabelHolder_level4constant']/label/span[@class='mandatory']");
        readonly By Level4Constant_TextField = By.Id("CWField_level4constant");

        #endregion

        #region Warning notifications

        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");

        #endregion

        #region Menu Items

        readonly By MenuButton = By.Id("CWNavGroup_Menu");
        readonly By Audit_MenuItem = By.Id("CWNavItem_AuditHistory");

        #endregion

        public FinanceCodeMappingRecordPage WaitForFinanceCodeMappingRecordPageToLoad(bool AdvancedSearch = false)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            if (AdvancedSearch)
            {
                WaitForElement(CWDataFormDialog);
                SwitchToIframe(CWDataFormDialog);
            }

            WaitForElement(iframe_CWDialog_CareProviderFinanceCodeMapping);
            SwitchToIframe(iframe_CWDialog_CareProviderFinanceCodeMapping);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementVisible(FinanceCodeMappingRecordPageHeader);
            WaitForElementVisible(BackButton);
            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateAllFieldsVisible()
        {
            WaitForElementVisible(ContractScheme_LabelField);
            WaitForElementVisible(ContractScheme_LookupButton);

            WaitForElementVisible(ResponsibleTeam_LabelField);
            WaitForElementVisible(ResponsibleTeam_LookupButton);

            WaitForElementVisible(PositionNumber_LabelField);
            WaitForElementVisible(PositionNumber_TextField);

            WaitForElementVisible(Inactive_LabelField);
            WaitForElementVisible(Inactive_YesOption);
            WaitForElementVisible(Inactive_NoOption);

            WaitForElementVisible(Level1Location_LabelField);
            WaitForElementVisible(Level1Location_LookupButton);
            
            return this;
        }

        public FinanceCodeMappingRecordPage ValidateFinanceCodeMappingRecordTitle(string ExpectedText)
        {
            MoveToElementInPage(FinanceCodeMappingRecordPageHeader);
            WaitForElementVisible(FinanceCodeMappingRecordPageHeader);
            ValidateElementByTitle(FinanceCodeMappingRecordPageHeader, "Finance Code Mapping: " + ExpectedText);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            MoveToElementInPage(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            MoveToElementInPage(SaveButton);
            Click(SaveButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickDeleteButton(bool Toolbar = false)
        {
            if (Toolbar)
            {
                WaitForElementToBeClickable(AdditionalToolbarElementsButton);
                Click(AdditionalToolbarElementsButton);
            }

            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public FinanceCodeMappingRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(AdditionalToolbarElementsButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickContractSchemeLookupButton()
        {
            WaitForElementToBeClickable(ContractScheme_LookupButton);
            MoveToElementInPage(ContractScheme_LookupButton);
            Click(ContractScheme_LookupButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
            MoveToElementInPage(ResponsibleTeam_LookupButton);
            Click(ResponsibleTeam_LookupButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickInactive_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(Inactive_YesOption);
                MoveToElementInPage(Inactive_YesOption);
                Click(Inactive_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(Inactive_NoOption);
                MoveToElementInPage(Inactive_NoOption);
                Click(Inactive_NoOption);
            }

            return this;
        }

        public FinanceCodeMappingRecordPage ClickLevel1LocationLookupButton()
        {
            WaitForElementToBeClickable(Level1Location_LookupButton);
            MoveToElementInPage(Level1Location_LookupButton);
            Click(Level1Location_LookupButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickLevel2LocationLookupButton()
        {
            WaitForElementToBeClickable(Level2Location_LookupButton);
            MoveToElementInPage(Level2Location_LookupButton);
            Click(Level2Location_LookupButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickLevel3LocationLookupButton()
        {
            WaitForElementToBeClickable(Level3Location_LookupButton);
            MoveToElementInPage(Level3Location_LookupButton);
            Click(Level3Location_LookupButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickLevel4LocationLookupButton()
        {
            WaitForElementToBeClickable(Level4Location_LookupButton);
            MoveToElementInPage(Level4Location_LookupButton);
            Click(Level4Location_LookupButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickContractSchemeRemoveButton()
        {
            WaitForElementToBeClickable(ContractScheme_RemoveButton);
            MoveToElementInPage(ContractScheme_RemoveButton);
            Click(ContractScheme_RemoveButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButton);
            MoveToElementInPage(ResponsibleTeam_RemoveButton);
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickLevel1LocationRemoveButton()
        {
            WaitForElementToBeClickable(Level1Location_RemoveButton);
            MoveToElementInPage(Level1Location_RemoveButton);
            Click(Level1Location_RemoveButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickLevel2LocationRemoveButton()
        {
            WaitForElementToBeClickable(Level2Location_RemoveButton);
            MoveToElementInPage(Level2Location_RemoveButton);
            Click(Level2Location_RemoveButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickLevel3LocationRemoveButton()
        {
            WaitForElementToBeClickable(Level3Location_RemoveButton);
            MoveToElementInPage(Level3Location_RemoveButton);
            Click(Level3Location_RemoveButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickLevel4LocationRemoveButton()
        {
            WaitForElementToBeClickable(Level4Location_RemoveButton);
            MoveToElementInPage(Level4Location_RemoveButton);
            Click(Level4Location_RemoveButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateContractSchemeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ContractScheme_LinkField);
            MoveToElementInPage(ContractScheme_LinkField);
            ValidateElementText(ContractScheme_LinkField, ExpectedText);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeam_LinkField);
            MoveToElementInPage(ResponsibleTeam_LinkField);
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidatePositionNumberText(string ExpectedText)
        {
            WaitForElementVisible(PositionNumber_TextField);
            MoveToElementInPage(PositionNumber_TextField);
            ValidateElementValue(PositionNumber_TextField, ExpectedText);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateInactive_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(Inactive_YesOption);
            WaitForElementVisible(Inactive_NoOption);
            MoveToElementInPage(Inactive_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(Inactive_YesOption);
                ValidateElementNotChecked(Inactive_NoOption);
            }
            else
            {
                ValidateElementChecked(Inactive_NoOption);
                ValidateElementNotChecked(Inactive_YesOption);
            }

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel1LocationLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(Level1Location_LinkField);
            MoveToElementInPage(Level1Location_LinkField);
            ValidateElementText(Level1Location_LinkField, ExpectedText);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel2LocationLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(Level2Location_LinkField);
            MoveToElementInPage(Level2Location_LinkField);
            ValidateElementText(Level2Location_LinkField, ExpectedText);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel3LocationLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(Level3Location_LinkField);
            MoveToElementInPage(Level3Location_LinkField);
            ValidateElementText(Level3Location_LinkField, ExpectedText);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel4LocationLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(Level4Location_LinkField);
            MoveToElementInPage(Level4Location_LinkField);
            ValidateElementText(Level4Location_LinkField, ExpectedText);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateContractSchemeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ContractScheme_LabelField);
            MoveToElementInPage(ContractScheme_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ContractScheme_MandatoryField);
            else
                WaitForElementNotVisible(ContractScheme_MandatoryField, 3);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateResponsibleTeamMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ResponsibleTeam_LabelField);
            MoveToElementInPage(ResponsibleTeam_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_MandatoryField);
            else
                WaitForElementNotVisible(ResponsibleTeam_MandatoryField, 3);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidatePositionNumberMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(PositionNumber_LabelField);
            MoveToElementInPage(PositionNumber_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(PositionNumber_MandatoryField);
            else
                WaitForElementNotVisible(PositionNumber_MandatoryField, 3);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateInactiveMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Inactive_LabelField);
            MoveToElementInPage(Inactive_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Inactive_MandatoryField);
            else
                WaitForElementNotVisible(Inactive_MandatoryField, 3);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel1LocationMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Level1Location_LabelField);
            MoveToElementInPage(Level1Location_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Level1Location_MandatoryField);
            else
                WaitForElementNotVisible(Level1Location_MandatoryField, 3);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel2LocationMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Level2Location_LabelField);
            MoveToElementInPage(Level2Location_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Level2Location_MandatoryField);
            else
                WaitForElementNotVisible(Level2Location_MandatoryField, 3);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel3LocationMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Level3Location_LabelField);
            MoveToElementInPage(Level3Location_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Level3Location_MandatoryField);
            else
                WaitForElementNotVisible(Level3Location_MandatoryField, 3);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel4LocationMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Level4Location_LabelField);
            MoveToElementInPage(Level4Location_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Level4Location_MandatoryField);
            else
                WaitForElementNotVisible(Level4Location_MandatoryField, 3);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel1ConstantMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Level1Constant_LabelField);
            MoveToElementInPage(Level1Constant_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Level1Constant_MandatoryField);
            else
                WaitForElementNotVisible(Level1Constant_MandatoryField, 3);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel2ConstantMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Level2Constant_LabelField);
            MoveToElementInPage(Level2Constant_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Level2Constant_MandatoryField);
            else
                WaitForElementNotVisible(Level2Constant_MandatoryField, 3);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel3ConstantMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Level3Constant_LabelField);
            MoveToElementInPage(Level3Constant_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Level3Constant_MandatoryField);
            else
                WaitForElementNotVisible(Level3Constant_MandatoryField, 3);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel4ConstantMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Level4Constant_LabelField);
            MoveToElementInPage(Level4Constant_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Level4Constant_MandatoryField);
            else
                WaitForElementNotVisible(Level4Constant_MandatoryField, 3);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel1ConstantFieldMaximumLimitText(string expected)
        {
            WaitForElementVisible(Level1Constant_TextField);
            ValidateElementMaxLength(Level1Constant_TextField, expected);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel2ConstantFieldMaximumLimitText(string expected)
        {
            WaitForElementVisible(Level2Constant_TextField);
            ValidateElementMaxLength(Level2Constant_TextField, expected);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel3ConstantFieldMaximumLimitText(string expected)
        {
            WaitForElementVisible(Level3Constant_TextField);
            ValidateElementMaxLength(Level3Constant_TextField, expected);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel4ConstantFieldMaximumLimitText(string expected)
        {
            WaitForElementVisible(Level4Constant_TextField);
            ValidateElementMaxLength(Level4Constant_TextField, expected);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel1ConstantIsVisible(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(Level1Constant_LabelField);
                WaitForElementVisible(Level1Constant_TextField);
            }
            else
            {
                WaitForElementNotVisible(Level1Constant_LabelField, 3);
                WaitForElementNotVisible(Level1Constant_TextField, 3);
            }

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel2ConstantIsVisible(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(Level2Constant_LabelField);
                WaitForElementVisible(Level2Constant_TextField);
            }
            else
            {
                WaitForElementNotVisible(Level2Constant_LabelField, 3);
                WaitForElementNotVisible(Level2Constant_TextField, 3);
            }

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel3ConstantIsVisible(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(Level3Constant_LabelField);
                WaitForElementVisible(Level3Constant_TextField);
            }
            else
            {
                WaitForElementNotVisible(Level3Constant_LabelField, 3);
                WaitForElementNotVisible(Level3Constant_TextField, 3);
            }

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel4ConstantIsVisible(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(Level4Constant_LabelField);
                WaitForElementVisible(Level4Constant_TextField);
            }
            else
            {
                WaitForElementNotVisible(Level4Constant_LabelField, 3);
                WaitForElementNotVisible(Level4Constant_TextField, 3);
            }

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel1LocationIsVisible(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(Level1_Label);
                WaitForElementVisible(Level1Location_LabelField);
                WaitForElementVisible(Level1Location_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(Level1_Label, 3);
                WaitForElementNotVisible(Level1Location_LabelField, 3);
                WaitForElementNotVisible(Level1Location_LookupButton, 3);
            }

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel2LocationIsVisible(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(Level2_Label);
                WaitForElementVisible(Level2Location_LabelField);
                WaitForElementVisible(Level2Location_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(Level2_Label, 3);
                WaitForElementNotVisible(Level2Location_LabelField, 3);
                WaitForElementNotVisible(Level2Location_LookupButton, 3);
            }

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel3LocationIsVisible(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(Level3_Label);
                WaitForElementVisible(Level3Location_LabelField);
                WaitForElementVisible(Level3Location_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(Level3_Label, 3);
                WaitForElementNotVisible(Level3Location_LabelField, 3);
                WaitForElementNotVisible(Level3Location_LookupButton, 3);
            }

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel4LocationIsVisible(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(Level4_Label);
                WaitForElementVisible(Level4Location_LabelField);
                WaitForElementVisible(Level4Location_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(Level4_Label, 3);
                WaitForElementNotVisible(Level4Location_LabelField, 3);
                WaitForElementNotVisible(Level4Location_LookupButton, 3);
            }

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateFormErrorNotificationMessageIsDisplayed(string ExpectedText)
        {
            MoveToElementInPage(NotificationMessage);
            WaitForElementVisible(NotificationMessage);
            ValidateElementText(NotificationMessage, ExpectedText);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateContractSchemeFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(ContractScheme_FieldErrorLabel);
            MoveToElementInPage(ContractScheme_FieldErrorLabel);
            ValidateElementText(ContractScheme_FieldErrorLabel, ExpectedText);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateResponsibleTeamFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(ResponsibleTeam_FieldErrorLabel);
            MoveToElementInPage(ResponsibleTeam_FieldErrorLabel);
            ValidateElementText(ResponsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel1LocationFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(Level1Location_FieldErrorLabel);
            MoveToElementInPage(Level1Location_FieldErrorLabel);
            ValidateElementText(Level1Location_FieldErrorLabel, ExpectedText);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateContractSchemeLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ContractScheme_LookupButton);
            MoveToElementInPage(ContractScheme_LookupButton);
            if (IsDisabled)
                ValidateElementDisabled(ContractScheme_LookupButton);
            else
                ValidateElementNotDisabled(ContractScheme_LookupButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ResponsibleTeam_LookupButton);
            MoveToElementInPage(ResponsibleTeam_LookupButton);
            if (IsDisabled)
                ValidateElementDisabled(ResponsibleTeam_LookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeam_LookupButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidatePositionNumberFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(PositionNumber_TextField);
            MoveToElementInPage(PositionNumber_TextField);
            if (IsDisabled)
                ValidateElementDisabled(PositionNumber_TextField);
            else
                ValidateElementNotDisabled(PositionNumber_TextField);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateInactiveOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Inactive_YesOption);
            WaitForElementVisible(Inactive_NoOption);
            MoveToElementInPage(Inactive_NoOption);

            if (IsDisabled)
            {
                ValidateElementDisabled(Inactive_YesOption);
                ValidateElementDisabled(Inactive_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(Inactive_YesOption);
                ValidateElementNotDisabled(Inactive_NoOption);
            }

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel1ConstantFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Level1Constant_TextField);
            MoveToElementInPage(Level1Constant_TextField);
            if (IsDisabled)
                ValidateElementDisabled(Level1Constant_TextField);
            else
                ValidateElementNotDisabled(Level1Constant_TextField);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel2ConstantFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Level2Constant_TextField);
            MoveToElementInPage(Level2Constant_TextField);
            if (IsDisabled)
                ValidateElementDisabled(Level2Constant_TextField);
            else
                ValidateElementNotDisabled(Level2Constant_TextField);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel3ConstantFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Level3Constant_TextField);
            MoveToElementInPage(Level3Constant_TextField);
            if (IsDisabled)
                ValidateElementDisabled(Level3Constant_TextField);
            else
                ValidateElementNotDisabled(Level3Constant_TextField);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel4ConstantFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Level4Constant_TextField);
            MoveToElementInPage(Level4Constant_TextField);
            if (IsDisabled)
                ValidateElementDisabled(Level4Constant_TextField);
            else
                ValidateElementNotDisabled(Level4Constant_TextField);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel1LocationLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Level1Location_LookupButton);
            MoveToElementInPage(Level1Location_LookupButton);
            if (IsDisabled)
                ValidateElementDisabled(Level1Location_LookupButton);
            else
                ValidateElementNotDisabled(Level1Location_LookupButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel2LocationLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Level2Location_LookupButton);
            MoveToElementInPage(Level2Location_LookupButton);
            if (IsDisabled)
                ValidateElementDisabled(Level2Location_LookupButton);
            else
                ValidateElementNotDisabled(Level2Location_LookupButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel3LocationLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Level3Location_LookupButton);
            MoveToElementInPage(Level3Location_LookupButton);
            if (IsDisabled)
                ValidateElementDisabled(Level3Location_LookupButton);
            else
                ValidateElementNotDisabled(Level3Location_LookupButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ValidateLevel4LocationLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Level4Location_LookupButton);
            MoveToElementInPage(Level4Location_LookupButton);
            if (IsDisabled)
                ValidateElementDisabled(Level4Location_LookupButton);
            else
                ValidateElementNotDisabled(Level4Location_LookupButton);

            return this;
        }

        public FinanceCodeMappingRecordPage ClickMenuButton()
        {
            WaitForElementToBeClickable(MenuButton);
            MoveToElementInPage(MenuButton);
            Click(MenuButton);

            return this;
        }

        public FinanceCodeMappingRecordPage NavigateToAuditPage()
        {
            ClickMenuButton();

            WaitForElementToBeClickable(Audit_MenuItem);
            MoveToElementInPage(Audit_MenuItem);
            Click(Audit_MenuItem);

            return this;
        }

    }
}