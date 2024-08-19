using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class RecruitmentRequirementRecordPage : CommonMethods
    {
        public RecruitmentRequirementRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContent_Iframe = By.Id("CWContentIFrame");
        readonly By recruitmentrequirement_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=recruitmentrequirement&')]");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");

        readonly By recordId_Field = By.Id("CWField_recordid");

        readonly string RequirementName_FieldId = "CWField_requirementname";
        readonly By RequirementName_Field = By.Id("CWField_requirementname");
        readonly By RequirementName_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_requirementname']/label/span");

        readonly By requiredItemType_LookUP = By.Id("CWLookupBtn_requireditemid");
        readonly By requiredItemType_FieldLinkText = By.Id("CWField_requireditemid_Link");
        readonly By requiredItemType_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_requireditemid']/label/span");

        readonly By startDate_Field = By.Id("CWField_startdate");

        readonly By responsibleTeam_LookUP = By.Id("CWLookupBtn_ownerid");
        readonly By responsibleTeam_FieldLinkText = By.Id("CWField_ownerid_Link");

        readonly By endDate_Field = By.Id("CWField_enddate");

        readonly By roles_LookUP = By.Id("CWLookupBtn_selectedrolesid");
        By roles_SelectedRecordLinkText(string RecordId) => By.XPath("//ul[@id='CWField_selectedrolesid_List']/*/*[@id='" + RecordId + "_Link']");

        readonly By AllRoles_YesRadioButtonOption = By.XPath("//*[@id='CWField_allroles_1']");
        readonly By AllRoles_NoRadioButtonOption = By.XPath("//*[@id='CWField_allroles_0']");

        readonly By Allbusinessunits_YesRadioButtonOption = By.XPath("//*[@id='CWField_allbusinessunits_1']");
        readonly By Allbusinessunits_NoRadioButtonOption = By.XPath("//*[@id='CWField_allbusinessunits_0']");

        readonly By NoRequiredForInduction_Field = By.Id("CWField_inductionnumber");
        readonly By NoRequiredForInduction_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_inductionnumber']/label/span");
        readonly By NoRequiredForAcceptance_Field = By.Id("CWField_acceptancenumber");
        readonly By NoRequiredForAcceptance_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_acceptancenumber']/label/span");
        readonly By StatusForInduction_Picklist = By.Id("CWField_inductionstatusid");
        readonly By StatusForInduction_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_inductionstatusid']/label/span");
        readonly By StatusForAcceptance_Picklist = By.Id("CWField_acceptancestatusid");
        readonly By StatusForAcceptance_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_acceptancestatusid']/label/span");

        readonly By RecordId_Field = By.XPath("//input[@id = 'CWField_recordid']");
        readonly By RequirementNameMandatoryField = By.XPath("//*[@id = 'CWLabelHolder_requirementname']/label/span[@class = 'mandatory']");

        readonly By RequiredItemTypeMandatoryField = By.XPath("//*[@id = 'CWLabelHolder_requireditemid']/label/span[@class = 'mandatory']");
        readonly By StartDateMandatoryField = By.XPath("//*[@id = 'CWLabelHolder_startdate']/label/span[@class = 'mandatory']");
        readonly By AllRolesMandatoryField = By.XPath("//*[@id = 'CWLabelHolder_allroles']/label/span[@class = 'mandatory']");
        readonly By RolesMandatoryField = By.XPath("//*[@id = 'CWLabelHolder_selectedrolesid']/label/span[@class = 'mandatory']");
        readonly By NumberRequiredForInductionMandatoryField = By.XPath("//*[@id = 'CWLabelHolder_inductionnumber']/label/span[@class = 'mandatory']");
        readonly By StatusForInductionMandatoryField = By.XPath("//*[@id = 'CWLabelHolder_inductionstatusid']/label/span[@class = 'mandatory']");
        readonly By NumberRequiredForAcceptanceMandatoryField = By.XPath("//*[@id = 'CWLabelHolder_acceptancenumber']/label/span[@class = 'mandatory']");
        readonly By StatusForAcceptanceMandatoryField = By.XPath("//*[@id = 'CWLabelHolder_acceptancestatusid']/label/span[@class = 'mandatory']");
        readonly By ResponsibleTeamMandatoryField = By.XPath("//*[@id = 'CWLabelHolder_ownerid']/label/span[@class = 'mandatory']");


        public RecruitmentRequirementRecordPage WaitForRecruitmentRequirementRecordPageToLoad()
        {
            SwitchToDefaultFrame();


            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(recruitmentrequirement_Iframe);
            SwitchToIframe(recruitmentrequirement_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(requiredItemType_LookUP);
            WaitForElement(RequirementName_Field);
            WaitForElement(startDate_Field);

            return this;
        }


        public RecruitmentRequirementRecordPage InsertStartDate(string TextToInsert)
        {
            WaitForElement(startDate_Field);
            SendKeys(startDate_Field, TextToInsert);
            SendKeysWithoutClearing(NoRequiredForInduction_Field, Keys.Tab);

            return this;
        }

        public RecruitmentRequirementRecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElement(endDate_Field);
            SendKeys(endDate_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public RecruitmentRequirementRecordPage InsertRequirementName(string TextToInsert)
        {
            WaitForElement(RequirementName_Field);
            SendKeys(RequirementName_Field, TextToInsert);

            return this;
        }

        public RecruitmentRequirementRecordPage InsertNoRequiredForInduction(string TextToInsert)
        {
            WaitForElementToBeClickable(NoRequiredForInduction_Field);
            SendKeys(NoRequiredForInduction_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public RecruitmentRequirementRecordPage InsertNoRequiredForAcceptance(string TextToInsert)
        {
            WaitForElementToBeClickable(NoRequiredForAcceptance_Field);
            SendKeys(NoRequiredForAcceptance_Field, TextToInsert + Keys.Tab);

            return this;
        }


        public RecruitmentRequirementRecordPage SelectStatusForInduction(string TextToSelect)
        {
            WaitForElement(StatusForInduction_Picklist);
            SelectPicklistElementByText(StatusForInduction_Picklist, TextToSelect);

            return this;
        }

        public RecruitmentRequirementRecordPage SelectStatusForAcceptance(string TextToSelect)
        {
            WaitForElement(StatusForAcceptance_Picklist);
            SelectPicklistElementByText(StatusForAcceptance_Picklist, TextToSelect);

            return this;
        }



        public RecruitmentRequirementRecordPage ClickRequiredItemTypeLookUpButton()
        {
            WaitForElement(requiredItemType_LookUP);
            Click(requiredItemType_LookUP);

            return this;
        }

        public RecruitmentRequirementRecordPage ClickRolesLookUpButton()
        {
            WaitForElement(roles_LookUP);
            Click(roles_LookUP);

            return this;
        }

        public RecruitmentRequirementRecordPage ClickAllRolesYesRadioButton()
        {
            WaitForElementToBeClickable(AllRoles_YesRadioButtonOption);
            Click(AllRoles_YesRadioButtonOption);

            return this;
        }

        public RecruitmentRequirementRecordPage ClickAllRolesNoRadioButton()
        {
            WaitForElementToBeClickable(AllRoles_NoRadioButtonOption);
            Click(AllRoles_NoRadioButtonOption);

            return this;
        }

        public RecruitmentRequirementRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public RecruitmentRequirementRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            return this;
        }

        public RecruitmentRequirementRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(deleteButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public RecruitmentRequirementRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public RecruitmentRequirementRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRecordIdFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RecordId_Field);
            }
            else
            {
                WaitForElementNotVisible(RecordId_Field, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRequirementNameMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RequirementNameMandatoryField);
            }
            else
            {
                WaitForElementNotVisible(RequirementNameMandatoryField, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRequiredItemTypeMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RequiredItemTypeMandatoryField);
            }
            else
            {
                WaitForElementNotVisible(RequiredItemTypeMandatoryField, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateStartDateMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(StartDateMandatoryField);
            }
            else
            {
                WaitForElementNotVisible(StartDateMandatoryField, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateAllRolesMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(AllRolesMandatoryField);
            }
            else
            {
                WaitForElementNotVisible(AllRolesMandatoryField, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRolesMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RolesMandatoryField);
            }
            else
            {
                WaitForElementNotVisible(RolesMandatoryField, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateNoRequiredForInductionMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(NumberRequiredForInductionMandatoryField);
            }
            else
            {
                WaitForElementNotVisible(NumberRequiredForInductionMandatoryField, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateStatusForInductionMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(StatusForInductionMandatoryField);
            }
            else
            {
                WaitForElementNotVisible(StatusForInductionMandatoryField, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateNoRequiredForAcceptanceMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(NumberRequiredForAcceptanceMandatoryField);
            }
            else
            {
                WaitForElementNotVisible(NumberRequiredForAcceptanceMandatoryField, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateStatusForAcceptanceMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(StatusForAcceptanceMandatoryField);
            }
            else
            {
                WaitForElementNotVisible(StatusForAcceptanceMandatoryField, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateResponsibleTeamMandatoryFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ResponsibleTeamMandatoryField);
            }
            else
            {
                WaitForElementNotVisible(ResponsibleTeamMandatoryField, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRequirementNameFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
            {
                WaitForElementVisible(RequirementName_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(RequirementName_FieldErrorLabel, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRequiredItemTypeFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(requiredItemType_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(requiredItemType_FieldErrorLabel, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateStatusForInductionFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(StatusForInduction_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(StatusForInduction_FieldErrorLabel, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateStatusForAcceptanceFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(StatusForAcceptance_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(StatusForAcceptance_FieldErrorLabel, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRolesLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(roles_LookUP);
            }
            else
            {
                WaitForElementNotVisible(roles_LookUP, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateNoRequiredForInductionFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(NoRequiredForInduction_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(NoRequiredForInduction_FieldErrorLabel, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateNoRequiredForAcceptanceFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(NoRequiredForAcceptance_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(NoRequiredForAcceptance_FieldErrorLabel, 3);
            }

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRecordIdFieldValue(string ExpectedString)
        {
            WaitForElementVisible(RecordId_Field);
            ScrollToElement(RecordId_Field);
            ValidateElementValue(RecordId_Field, ExpectedString);
            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRequirementNameFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(RequirementName_FieldErrorLabel);
            ValidateElementText(RequirementName_FieldErrorLabel, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidatRequiredItemTypeFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(requiredItemType_FieldErrorLabel);
            ValidateElementText(requiredItemType_FieldErrorLabel, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidatStatusForInductionFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(StatusForInduction_FieldErrorLabel);
            ValidateElementText(StatusForInduction_FieldErrorLabel, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidatStatusForAcceptanceFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(StatusForAcceptance_FieldErrorLabel);
            ValidateElementText(StatusForAcceptance_FieldErrorLabel, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateNoRequiredForInductionFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(NoRequiredForInduction_FieldErrorLabel);
            ValidateElementText(NoRequiredForInduction_FieldErrorLabel, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateNoRequiredForAcceptanceFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(NoRequiredForAcceptance_FieldErrorLabel);
            ValidateElementText(NoRequiredForAcceptance_FieldErrorLabel, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRecordIdValue(string ExpectedText)
        {
            WaitForElement(recordId_Field);
            ValidateElementValue(recordId_Field, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRequirementNameValue(string ExpectedText)
        {
            WaitForElement(RequirementName_Field);
            ValidateElementValue(RequirementName_Field, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRequirementNameValueViaJavascript(string ExpectedText)
        {
            WaitForElement(RequirementName_Field);
            ValidateElementValueByJavascript(RequirementName_FieldId, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateStartDateValue(string ExpectedText)
        {
            WaitForElement(startDate_Field);
            ValidateElementValue(startDate_Field, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateEndDateValue(string ExpectedText)
        {
            WaitForElement(endDate_Field);
            ValidateElementValue(endDate_Field, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRequiredItemTypeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(requiredItemType_FieldLinkText);
            ValidateElementByTitle(requiredItemType_FieldLinkText, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(responsibleTeam_FieldLinkText);
            ValidateElementByTitle(responsibleTeam_FieldLinkText, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRolesMultiSelectElementText(Guid RecordId, string ExpectedText)
        {
            WaitForElement(roles_SelectedRecordLinkText(RecordId.ToString()));
            ValidateElementText(roles_SelectedRecordLinkText(RecordId.ToString()), ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateNoRequiredForInductionValue(string ExpectedText)
        {
            WaitForElement(NoRequiredForInduction_Field);
            ValidateElementValue(NoRequiredForInduction_Field, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateNoRequiredForAcceptanceValue(string ExpectedText)
        {
            WaitForElement(NoRequiredForAcceptance_Field);
            ValidateElementValue(NoRequiredForAcceptance_Field, ExpectedText);

            return this;
        }





        public RecruitmentRequirementRecordPage ValidateStatusForInductionSelectedText(string ExpectedText)
        {
            WaitForElement(StatusForInduction_Picklist);
            ValidatePicklistSelectedText(StatusForInduction_Picklist, ExpectedText);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateStatusForAcceptanceSelectedText(string ExpectedText)
        {
            WaitForElement(StatusForAcceptance_Picklist);
            ValidatePicklistSelectedText(StatusForAcceptance_Picklist, ExpectedText);

            return this;
        }






        public RecruitmentRequirementRecordPage ValidateAllRolesYesRadionButtonChecked(bool ExpecteChecked)
        {
            if(ExpecteChecked)
                ValidateElementChecked(AllRoles_YesRadioButtonOption);
            else
                ValidateElementNotChecked(AllRoles_YesRadioButtonOption);

            return this;
        }
        public RecruitmentRequirementRecordPage ValidateAllRolesNoRadionButtonChecked(bool ExpecteChecked)
        {
            if (ExpecteChecked)
                ValidateElementChecked(AllRoles_NoRadioButtonOption);
            else
                ValidateElementNotChecked(AllRoles_NoRadioButtonOption);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateStatusForInductionFieldDisabled(bool ExpectedChecked)
        {
            if (ExpectedChecked)
                ValidateElementDisabled(StatusForInduction_Picklist);
            else
                ValidateElementNotDisabled(StatusForInduction_Picklist);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateStatusForAcceptanceFieldDisabled(bool ExpectedChecked)
        {
            if (ExpectedChecked)
                ValidateElementDisabled(StatusForAcceptance_Picklist);
            else
                ValidateElementNotDisabled(StatusForAcceptance_Picklist);

            return this;
        }

        public RecruitmentRequirementRecordPage ValidateRecordIdFieldDisabled(bool ExpectedChecked)
        {
            if (ExpectedChecked)
            {
                ValidateElementDisabled(RecordId_Field);
            }
            else
            {
                ValidateElementNotDisabled(RecordId_Field);
            }

            return this;
        }




        public RecruitmentRequirementRecordPage ValidateAllBusinessUnitsFieldVisible(bool ExpecteVisible)
        {
            if (ExpecteVisible) 
            {
                WaitForElementVisible(Allbusinessunits_YesRadioButtonOption);
                WaitForElementVisible(Allbusinessunits_NoRadioButtonOption);
            }
            else
            {
                WaitForElementNotVisible(Allbusinessunits_YesRadioButtonOption, 7);
                WaitForElementNotVisible(Allbusinessunits_NoRadioButtonOption, 7);
            }

            return this;
        }
    }
}
