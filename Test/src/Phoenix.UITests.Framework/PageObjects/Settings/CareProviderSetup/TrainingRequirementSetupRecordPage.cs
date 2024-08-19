using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class TrainingRequirementSetupRecordPage : CommonMethods
    {

        public TrainingRequirementSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContent_Iframe = By.Id("CWContentIFrame");
        readonly By trainingRequirement_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=trainingrequirementsetup&')]");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']//*[@id='CWToolbarMenu']/button");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By RecordId_Field = By.XPath("//input[@id = 'CWField_recordid']");
        readonly By RequirementName_Field = By.Id("CWField_recordname");

        readonly By TrainingItemType_LookupButton = By.Id("CWLookupBtn_trainingitemid");
        readonly By TrainingItemType_FieldLinkText = By.Id("CWField_trainingitemid_Link");

        readonly By validFromDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_validfromdate']//span[@class='mandatory']");
        readonly By validFromDate_Field = By.Id("CWField_validfromdate");
        readonly By validFromDate_DatePicker_Field = By.Id("CWField_validfromdate_DatePicker");

        readonly By validToDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_validtodate']//span[@class='mandatory']");
        readonly By validToDate_Field = By.Id("CWField_validtodate");
        readonly By validToDate_DatePicker_Field = By.Id("CWField_validtodate_DatePicker");

        readonly By AllRoles_YesRadioButton = By.Id("CWField_allroles_1");
        readonly By AllRoles_NoRadioButton = By.Id("CWField_allroles_0");
        readonly By Roles_LookupButton = By.Id("CWLookupBtn_selectedrolesid");

        readonly By requirementStatus_Picklist = By.Id("CWField_statusid");

        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");
        readonly By requirementName_NotificationErrorLabel = By.XPath("//*[@id='CWControlHolder_recordname']/label/span");
        readonly By trainingItemType_NotificationErrorLabel = By.XPath("//*[@id='CWControlHolder_trainingitemid']/label/span");
        readonly By validFrom_NotificationErrorLabel = By.XPath("//*[@id='CWControlHolder_validfromdate']/label/span");


        public TrainingRequirementSetupRecordPage WaitForTrainingRequirementSetupRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElement(trainingRequirement_Iframe);
            SwitchToIframe(trainingRequirement_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 70);

            WaitForElementVisible(pageHeader);

            WaitForElementVisible(RequirementName_Field);

            WaitForElement(TrainingItemType_LookupButton);

            WaitForElement(validFromDate_Field);
            WaitForElement(validToDate_Field);

            return this;
        }

        public TrainingRequirementSetupRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 70);

            WaitForElementToBeClickable(saveButton);
            WaitForElementToBeClickable(saveAndCloseButton);
            WaitForElementToBeClickable(deleteButton);
            WaitForElementToBeClickable(additionalToolbarElementsButton);

            return this;
        }

        public TrainingRequirementSetupRecordPage ClickTrainingItemTypeLookupButton()
        {
            WaitForElementToBeClickable(TrainingItemType_LookupButton);
            Click(TrainingItemType_LookupButton);

            return this;
        }

        public TrainingRequirementSetupRecordPage ClickRolesLookupButton()
        {
            WaitForElementToBeClickable(Roles_LookupButton);
            ScrollToElement(Roles_LookupButton);
            Click(Roles_LookupButton);

            return this;
        }

        public TrainingRequirementSetupRecordPage InsertRequirementName(string TextToInsert)
        {
            WaitForElement(RequirementName_Field);
            SendKeys(RequirementName_Field, TextToInsert);
            SendKeysWithoutClearing(RequirementName_Field, Keys.Tab);

            return this;
        }

        public TrainingRequirementSetupRecordPage InsertValidFromDate(string TextToInsert)
        {
            WaitForElement(validFromDate_Field);
            SendKeys(validFromDate_Field, TextToInsert);
            SendKeysWithoutClearing(validFromDate_Field, Keys.Tab);

            return this;
        }

        public TrainingRequirementSetupRecordPage InsertValidToDate(string TextToInsert)
        {
            WaitForElement(validToDate_Field);
            SendKeys(validToDate_Field, TextToInsert);
            SendKeysWithoutClearing(validToDate_Field, Keys.Tab);

            return this;
        }

        public TrainingRequirementSetupRecordPage ClickAllRolesYesOption()
        {
            WaitForElementToBeClickable(AllRoles_YesRadioButton);
            Click(AllRoles_YesRadioButton);

            return this;
        }
    
        public TrainingRequirementSetupRecordPage ClickAllRolesNoOption()
        {
            WaitForElementToBeClickable(AllRoles_NoRadioButton);
            Click(AllRoles_NoRadioButton);

            return this;
        }

        public TrainingRequirementSetupRecordPage SelectRequirementStatusForFullAccepted(string TextToSelect)
        {
            WaitForElementToBeClickable(requirementStatus_Picklist);
            SelectPicklistElementByText(requirementStatus_Picklist, TextToSelect);

            return this;
        }

        public TrainingRequirementSetupRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            ScrollToElement(saveButton);
            Click(saveButton);

            return this;
        }

        public TrainingRequirementSetupRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            ScrollToElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public TrainingRequirementSetupRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            ScrollToElement(deleteButton);
            Click(deleteButton);

            return this;
        }

        public TrainingRequirementSetupRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            ScrollToElement(backButton);
            Click(backButton);

            return this;

        }

        public TrainingRequirementSetupRecordPage ValidateTrainingCourseTitle(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            ValidateElementText(pageHeader, ExpectedText);

            return this;
        }

        public TrainingRequirementSetupRecordPage ValidateTrainingItemTypeFieldText(string ExpectedText)
        {
            WaitForElementVisible(TrainingItemType_FieldLinkText);
            ValidateElementText(TrainingItemType_FieldLinkText, ExpectedText);

            return this;
        }

        public TrainingRequirementSetupRecordPage ValidateRequirementNameValue(string ExpectedValue)
        {
            WaitForElementVisible(validFromDate_Field);
            ScrollToElement(validFromDate_Field);
            ValidateElementValue(validFromDate_Field, ExpectedValue);

            return this;
        }

        public TrainingRequirementSetupRecordPage ValidateValidFromDateValue(string ExpectedValue)
        {
            WaitForElementVisible(validFromDate_Field);
            ScrollToElement(validFromDate_Field);
            ValidateElementValue(validFromDate_Field, ExpectedValue);

            return this;
        }

        public TrainingRequirementSetupRecordPage ValidateValidToDateValue(string ExpectedValue)
        {
            WaitForElementVisible(validToDate_Field);
            ScrollToElement(validToDate_Field);
            ValidateElementValue(validToDate_Field, ExpectedValue);

            return this;
        }

        public TrainingRequirementSetupRecordPage ValidateValidFromFieldNotificationMessageText(String ExpectedText)
        {
            WaitForElementVisible(validFrom_NotificationErrorLabel);
            ScrollToElement(validFrom_NotificationErrorLabel);
            ValidateElementByTitle(validFrom_NotificationErrorLabel, ExpectedText);

            return this;
        }

        public TrainingRequirementSetupRecordPage ValidateRecordIdFieldVisibility(bool ExpectVisible)
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

    }
}
