
using Microsoft.Office.Interop.Word;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.WebAppAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonRecordEditPage : CommonMethods
    {
        public PersonRecordEditPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");

        readonly By iframe_CWNewPerson = By.Id("iframe_CWNewPerson");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");

        By personRecordIFrame(string PersonID) => By.Id("iframe_CWDialog_" + PersonID + "_Edit");


        #region Top Menu

        readonly By backButton = By.XPath("//*[@id='BackButton']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By printButton = By.Id("TI_PrintDetailsButton");
        readonly By shareButton = By.Id("TI_ShareRecordButton");
        readonly By additionalIItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By runOnDemandWorkflowButton = By.Id("TI_RunOnDemandWorkflow");
        readonly By copyRecordLinkButton = By.Id("TI_CopyRecordLink");
        readonly By deactivateButton = By.Id("TI_DeactivateButton");
        readonly By deceased_YesOption = By.Id("CWField_deceased_1");
        readonly By dateOfDeath_Field = By.Id("CWField_dateofdeath");
        readonly By UnPinFromMeButton = By.Id("TI_UnpinFromMeButton");
        readonly By PinToMeButton = By.Id("TI_PinToMeButton");

        #endregion

        readonly By PageTitle = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Person: ']/span");

        readonly By NotificationMessage = By.XPath("//*[@id='CWNotificationMessage_DataForm']");


        #region Fields

        #region Personal Details

        readonly By PersonType_Field = By.Id("CWField_persontypeid");
        readonly By firstName_Field = By.Id("CWField_firstname");
        readonly By middleName_Field = By.Id("CWField_middlename");
        readonly By lastName_Field = By.Id("CWField_lastname");
        readonly By statedGender_Field = By.Id("CWField_genderid");
        readonly By DOBAndAge_Field = By.Id("CWField_dobandagetypeid");
        readonly By dateOfBirth_Field = By.Id("CWField_dateofbirth");
        readonly By ethnicity_Field = By.Id("CWLookupBtn_ethnicityid");

        #endregion

        #region Address

        readonly By addressstartdate_Field = By.Id("CWField_addressstartdate");
        readonly By addressType_Field = By.Id("CWField_addresstypeid");
        readonly By postCode_Field = By.Id("CWField_postcode");
        readonly By LivesAlone_Field = By.Id("CWField_livesalonetypeid");

        #endregion

        #region Communication Preferences

        readonly By preferredInvoiceDeliveryMethod_Field = By.Id("CWField_personpreferreddocumentsdeliverymethodid");

        #endregion

        #region Access Information

        readonly By FloorLevel_Field = By.Id("CWField_floorlevel");
        readonly By FloorLevel_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_floorlevel']/label/span");
        readonly By PropertyKeySafe_SecureField = By.XPath("//*[@id='CWControlHolder_keysafe']/span[text()='****']");
        readonly By PropertyKeySafe_Field = By.Id("CWField_keysafe");
        readonly By AccessInstructions_SecureField = By.XPath("//*[@id='CWControlHolder_accessinstructions']/span[text()='****']");
        readonly By AccessInstructions_Field = By.Id("CWField_accessinstructions");
        readonly By HasLift_Field = By.Id("CWField_hasliftid");
        readonly By MedicalKeySafe_Field = By.Id("CWField_medicalkeysafe");


        #endregion

        #region Phone & Email

        readonly By homePhone_Field = By.Id("CWField_homephone");
        readonly By primaryEmail_Field = By.Id("CWField_primaryemail");
        readonly By BillingEmail_Field = By.Id("CWField_billingemail");
        readonly By primaryEmail_ErrorLabel = By.XPath("//*[@id='CWControlHolder_primaryemail']/label/span");

        #endregion

        #region Additional Information

        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By Smoker_Field = By.Id("CWField_smokerid");
        readonly By LivesInASmokingHousehold_Field = By.Id("CWField_smokinghouseholdid");
        readonly By Pets_Field = By.Id("CWField_petstatusid");
        readonly By ResponsibleStaffMember_LinkField = By.Id("CWField_responsiblestaffmemberid_Link");
        readonly By ResponsibleStaffMember_LookupButton = By.Id("CWLookupBtn_responsiblestaffmemberid");
        readonly By pronounsField_LookupButton = By.Id("CWLookupBtn_pronounsid");
        readonly By pronouns_LinkField = By.Id("CWField_pronounsid_Link");
        readonly By BandingField_LookupButton = By.XPath("//*[@id = 'CWLookupBtn_cpbandid']");
        By BandingField_LookupButton_ByPosition(int FieldPosition) => By.XPath("//li[" + FieldPosition + "]//*[@id = 'CWLookupBtn_cpbandid']");
        //deprecated as part of ACC-6375
        //By Notes_Field_ByPosition(int FieldPosition) => By.XPath("//li[" + FieldPosition + "]//*[@id = 'CWField_notes']");

        #endregion

        #region Finance Details

        readonly By Cpsuspenddebtorinvoices_1 = By.XPath("//*[@id='CWField_cpsuspenddebtorinvoices_1']");
        readonly By Cpsuspenddebtorinvoices_0 = By.XPath("//*[@id='CWField_cpsuspenddebtorinvoices_0']");
        readonly By CpsuspenddebtorinvoicesreasonidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpsuspenddebtorinvoicesreasonid']");
        readonly By Debtornumber1 = By.XPath("//*[@id='CWField_debtornumber1']");
        readonly By Anonymousforbilling_1 = By.XPath("//*[@id='CWField_anonymousforbilling_1']");
        readonly By Anonymousforbilling_0 = By.XPath("//*[@id='CWField_anonymousforbilling_0']");
        readonly By Paysbydirectdebit_SecureField = By.XPath("//*[@id='CWControlHolder_paysbydirectdebit']/span[text()='****']");
        readonly By Paysbydirectdebit_1 = By.XPath("//*[@id='CWField_paysbydirectdebit_1']");
        readonly By Paysbydirectdebit_0 = By.XPath("//*[@id='CWField_paysbydirectdebit_0']");
        readonly By Bankaccountnumber = By.XPath("//*[@id='CWField_bankaccountnumber']");
        readonly By Auddisref = By.XPath("//*[@id='CWField_auddisref']");
        readonly By Bankaccountsortcode = By.XPath("//*[@id='CWField_bankaccountsortcode']");
        readonly By Bankaccountname = By.XPath("//*[@id='CWField_bankaccountname']");
        readonly By TransactiontypeidLinkField = By.XPath("//*[@id='CWField_transactiontypeid_Link']");
        readonly By TransactiontypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_transactiontypeid']");
        readonly By Body = By.XPath("//body");

        #endregion

        #region External Identifiers

        //removed as part of ACC-6375
        //readonly By LASocialCareRef_Field = By.Id("CWField_socialservicesref");

        #endregion

        #region Error messages

        readonly By BankAccountNumberFormError = By.XPath("//*[@for = 'CWField_bankaccountnumber'][@class = 'formerror']/span");
        //xpath for bankaccountsortcode form error message
        readonly By BankAccountSortCodeFormError = By.XPath("//*[@for = 'CWField_bankaccountsortcode'][@class = 'formerror']/span");
        #endregion

        #region Labels
        By MandatoryField_Label(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']/span[@class = 'mandatory']");

        #endregion

        #region Edit Person Popup

        By PersonEdit_iframe_CWDialog(string RecordId) => By.XPath("//iframe[contains(@id,'mcc-iframe')][contains(@src,'id=" + RecordId + "')]");

        readonly By EditpopupHeader = By.XPath("//*[@class='mcc-drawer__header']//h2");
        readonly By closeIcon = By.XPath("//button[@class='mcc-button mcc-button--sm mcc-button--ghost mcc-button--icon-only btn-close-drawer']");

        readonly By DrwaerCloseButton = By.Id("CWCloseDrawerButton");

        #endregion

        #region Care Status

        readonly By NextCarePlanReviewDate = By.XPath("//*[@id = 'CWField_nextcareplanreviewdate']");

        #endregion

        #endregion


        public PersonRecordEditPage WaitForPersonRecordEditPageToLoad(string PersonID, string PersonName)
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame(PersonID));
            SwitchToIframe(personRecordIFrame(PersonID));

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(printButton);
            WaitForElement(shareButton);

            WaitForElement(PageTitle);
            ValidateElementText(PageTitle, PersonName);

            return this;
        }

        public PersonRecordEditPage WaitForEditPersonRecordPopupToLoad(string PersonID, string PersonName)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(PersonEdit_iframe_CWDialog(PersonID));
            SwitchToIframe(PersonEdit_iframe_CWDialog(PersonID));

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(EditpopupHeader);
            ValidateElementText(EditpopupHeader, "Person: " + PersonName);

            WaitForElementVisible(DrwaerCloseButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(saveButton);

            return this;
        }

        public PersonRecordEditPage WaitForNewPersonRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWNewPerson);
            SwitchToIframe(iframe_CWNewPerson);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(PageTitle);

            return this;
        }

        public PersonRecordEditPage ValidateNotificationMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NotificationMessage);
            else
                WaitForElementNotVisible(NotificationMessage, 3);

            return this;
        }

        public PersonRecordEditPage ValidateNotificationMessageText(string ExpectedText)
        {
            ValidateElementText(NotificationMessage, ExpectedText);

            return this;
        }

        public PersonRecordEditPage ValidateMandatoryFieldIsDisplayed(string FieldName, bool ExpectedMandatory = true)
        {
            if (ExpectedMandatory)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 2);
            bool ActualDisplayed = GetElementVisibility(MandatoryField_Label(FieldName));
            Assert.AreEqual(ExpectedMandatory, ActualDisplayed);

            return this;
        }

        public PersonRecordEditPage SelectPersonType(string TextToSelect)
        {
            WaitForElement(PersonType_Field);
            ScrollToElement(PersonType_Field);
            SelectPicklistElementByText(PersonType_Field, TextToSelect);

            return this;
        }

        //validate person type picklist selected text
        public PersonRecordEditPage ValidatePersonTypeSelectedText(string ExpectedText)
        {
            WaitForElement(PersonType_Field);
            ScrollToElement(PersonType_Field);
            ValidatePicklistSelectedText(PersonType_Field, ExpectedText);

            return this;
        }

        public PersonRecordEditPage InsertFirstName(string TextToInsert)
        {
            SendKeys(firstName_Field, TextToInsert);

            return this;
        }

        public PersonRecordEditPage InsertMiddleName(string TextToInsert)
        {
            SendKeys(middleName_Field, TextToInsert);

            return this;
        }


        public PersonRecordEditPage SelectPreferredInvoiceDeliveryMethod(string TextToSelect)
        {
            SelectPicklistElementByText(preferredInvoiceDeliveryMethod_Field, TextToSelect);

            return this;
        }
        public PersonRecordEditPage InsertLastName(string TextToInsert)
        {
            SendKeys(lastName_Field, TextToInsert);

            return this;
        }

        public PersonRecordEditPage SelectStatedGender(string TextToSelect)
        {
            SelectPicklistElementByText(statedGender_Field, TextToSelect);

            return this;
        }

        public PersonRecordEditPage SelectDOBAndAge(string TextToSelect)
        {
            SelectPicklistElementByText(DOBAndAge_Field, TextToSelect);

            return this;
        }

        public PersonRecordEditPage InsertDOB(string TextToInsert)
        {
            SendKeys(dateOfBirth_Field, TextToInsert);
            return this;
        }

        public PersonRecordEditPage InsertStartDateOfAddress(string TextToInsert)
        {
            WaitForElementToBeClickable(addressstartdate_Field);
            SendKeys(addressstartdate_Field, TextToInsert);

            return this;
        }

        public PersonRecordEditPage SelectAddressType(string TextToSelect)
        {
            SelectPicklistElementByText(addressType_Field, TextToSelect);

            return this;
        }

        public PersonRecordEditPage InsertPostCode(string TextToInsert)
        {
            SendKeys(postCode_Field, TextToInsert);

            return this;
        }

        public PersonRecordEditPage SelectLivesAlone(string TextToSelect)
        {
            WaitForElementToBeClickable(LivesAlone_Field);
            SelectPicklistElementByText(LivesAlone_Field, TextToSelect);

            return this;
        }

        public PersonRecordEditPage InsertHomePhone(string TextToInsert)
        {
            SendKeys(homePhone_Field, TextToInsert);

            return this;
        }

        public PersonRecordEditPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
            Click(ResponsibleTeam_LookupButton);

            return this;
        }

        public PersonRecordEditPage SelectSmoker(string TextToSelect)
        {
            WaitForElementToBeClickable(Smoker_Field);
            SelectPicklistElementByText(Smoker_Field, TextToSelect);

            return this;
        }

        public PersonRecordEditPage SelectLivesInASmokingHousehold(string TextToSelect)
        {
            WaitForElementToBeClickable(LivesInASmokingHousehold_Field);
            SelectPicklistElementByText(LivesInASmokingHousehold_Field, TextToSelect);

            return this;
        }

        public PersonRecordEditPage SelectPets(string TextToSelect)
        {
            WaitForElementToBeClickable(Pets_Field);
            SelectPicklistElementByText(Pets_Field, TextToSelect);

            return this;
        }

        //validate Pets_Field selected value
        public PersonRecordEditPage ValidatePetsFieldSelectedValue(string ExpectedValue)
        {
            ScrollToElement(Pets_Field);
            WaitForElementVisible(Pets_Field);
            ValidatePicklistSelectedText(Pets_Field, ExpectedValue);

            return this;
        }

        //method to validate LivesInASmokingHousehold_Field selected value
        public PersonRecordEditPage ValidateLivesInASmokingHouseholdFieldSelectedValue(string ExpectedValue)
        {
            ScrollToElement(LivesInASmokingHousehold_Field);
            WaitForElementVisible(LivesInASmokingHousehold_Field);
            ValidatePicklistSelectedText(LivesInASmokingHousehold_Field, ExpectedValue);

            return this;
        }

        public PersonRecordEditPage ClickResponsibleStaffMemberLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleStaffMember_LookupButton);
            Click(ResponsibleStaffMember_LookupButton);

            return this;
        }

        public PersonRecordEditPage ClickResponsibleStaffMemberLookupButton(string ExpectedText)
        {
            WaitForElement(ResponsibleStaffMember_LinkField);
            ValidateElementText(ResponsibleStaffMember_LinkField, ExpectedText);

            return this;
        }

        public PersonRecordEditPage ClickEthnicityLookupButton()
        {
            Click(ethnicity_Field);

            return this;
        }

        public PersonRecordEditPage InsertPrimaryEmail(string TextToInsert)
        {
            SendKeys(primaryEmail_Field, TextToInsert);

            return this;
        }

        public PersonRecordEditPage InsertBillingEmail(string TextToInsert)
        {
            WaitForElementToBeClickable(BillingEmail_Field);
            ScrollToElement(BillingEmail_Field);
            SendKeys(BillingEmail_Field, TextToInsert);

            return this;
        }

        public PersonRecordEditPage InsertFloorLevel(string TextToInsert)
        {
            WaitForElement(FloorLevel_Field);
            SendKeys(FloorLevel_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonRecordEditPage ValidateFloorLevelErrorLabelVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElement(FloorLevel_FieldErrorLabel);
                ScrollToElement(FloorLevel_FieldErrorLabel);
                WaitForElementVisible(FloorLevel_FieldErrorLabel);

            }
            else
            {
                WaitForElementNotVisible(FloorLevel_FieldErrorLabel, 3);
            }


            return this;
        }

        public PersonRecordEditPage ValidateFloorLevelErrorLabelText(string ExpectedText)
        {
            WaitForElement(FloorLevel_FieldErrorLabel);
            ValidateElementText(FloorLevel_FieldErrorLabel, ExpectedText);

            return this;
        }

        public PersonRecordEditPage ValidatePropertyKeySafeSecureFieldDisplayed()
        {
            WaitForElement(PropertyKeySafe_SecureField);
            ScrollToElement(PropertyKeySafe_SecureField);
            WaitForElementVisible(PropertyKeySafe_SecureField);

            return this;
        }

        public PersonRecordEditPage ValidatePropertyKeySafeFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(PropertyKeySafe_Field);
            }
            else
            {

                ValidateElementNotDisabled(PropertyKeySafe_Field);
            }
            return this;
        }

        public PersonRecordEditPage InsertPropertyKeySafe(string TextToInsert)
        {
            WaitForElement(PropertyKeySafe_Field);
            SendKeys(PropertyKeySafe_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonRecordEditPage ValidateAccessInstructionsSecureFieldDisplayed()
        {
            WaitForElement(AccessInstructions_SecureField);
            ScrollToElement(AccessInstructions_SecureField);
            WaitForElementVisible(AccessInstructions_SecureField);

            return this;
        }

        public PersonRecordEditPage ValidateAccessInstructionsFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(AccessInstructions_Field);
            }
            else
            {

                ValidateElementNotDisabled(AccessInstructions_Field);
            }
            return this;
        }

        public PersonRecordEditPage InsertAccessInstructions(string TextToInsert)
        {
            WaitForElement(AccessInstructions_Field);
            SendKeys(AccessInstructions_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonRecordEditPage SelectHasLift(string TextToSelect)
        {
            WaitForElement(HasLift_Field);
            SelectPicklistElementByText(HasLift_Field, TextToSelect);

            return this;
        }

        public PersonRecordEditPage InsertMedicalKeySafe(string TextToInsert)
        {
            WaitForElement(MedicalKeySafe_Field);
            SendKeys(MedicalKeySafe_Field, TextToInsert + Keys.Tab);

            return this;
        }

        //removed as part of ACC-6375
        //public PersonRecordEditPage InsertLASocialCareRef(string TextToInsert)
        //{
        //    WaitForElement(LASocialCareRef_Field);
        //    SendKeys(LASocialCareRef_Field, TextToInsert + Keys.Tab);

        //    return this;
        //}

        public PersonRecordEditPage ValidatePrimaryEmailErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(primaryEmail_ErrorLabel);
            else
                WaitForElementNotVisible(primaryEmail_ErrorLabel, 3);

            return this;
        }

        public PersonRecordEditPage ValidatePrimaryEmailErrorLabelText(string ExpectedText)
        {
            ValidateElementText(primaryEmail_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonRecordEditPage ClickBackButton()
        {
            Click(backButton);

            return this;
        }

        public PersonRecordEditPage ClickSaveButton()
        {
            Click(saveButton);

            return this;
        }

        public PersonRecordEditPage TapSaveAndCloseButton()
        {
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public PersonRecordEditPage ClickRunOnDemandWorkflowButton()
        {
            WaitForElement(additionalIItemsButton);
            Click(additionalIItemsButton);

            WaitForElement(runOnDemandWorkflowButton);
            Click(runOnDemandWorkflowButton);

            return this;
        }

        public PersonRecordEditPage ClickCopyRecordLinkButton()
        {
            WaitForElement(additionalIItemsButton);
            Click(additionalIItemsButton);

            WaitForElement(copyRecordLinkButton);
            Click(copyRecordLinkButton);

            return this;
        }

        public PersonRecordEditPage ClickPrintButton()
        {
            Click(printButton);

            return this;
        }

        public PersonRecordEditPage ClickDeactivateButton()
        {
            WaitForElement(additionalIItemsButton);
            Click(additionalIItemsButton);

            WaitForElement(deactivateButton);
            Click(deactivateButton);

            return this;
        }

        public PersonRecordEditPage ClickDeceasedYesRadioButton()
        {
            ScrollToElement(deceased_YesOption);
            Click(deceased_YesOption);

            return this;
        }

        public PersonRecordEditPage InsertDateOfDeath(string TextToInsert)
        {
            SendKeys(dateOfDeath_Field, TextToInsert);

            return this;
        }


        public PersonRecordEditPage ClickSuspendDebtorInvoices_YesRadioButton()
        {
            WaitForElementToBeClickable(Cpsuspenddebtorinvoices_1);
            Click(Cpsuspenddebtorinvoices_1);

            return this;
        }

        public PersonRecordEditPage ValidateSuspendDebtorInvoices_YesRadioButtonChecked()
        {
            WaitForElement(Cpsuspenddebtorinvoices_1);
            ValidateElementChecked(Cpsuspenddebtorinvoices_1);

            return this;
        }

        public PersonRecordEditPage ValidateSuspendDebtorInvoices_YesRadioButtonNotChecked()
        {
            WaitForElement(Cpsuspenddebtorinvoices_1);
            ValidateElementNotChecked(Cpsuspenddebtorinvoices_1);

            return this;
        }

        public PersonRecordEditPage ClickSuspendDebtorInvoices_NoRadioButton()
        {
            WaitForElementToBeClickable(Cpsuspenddebtorinvoices_0);
            Click(Cpsuspenddebtorinvoices_0);

            return this;
        }

        public PersonRecordEditPage ValidateSuspendDebtorInvoices_NoRadioButtonChecked()
        {
            WaitForElement(Cpsuspenddebtorinvoices_0);
            ValidateElementChecked(Cpsuspenddebtorinvoices_0);

            return this;
        }

        public PersonRecordEditPage ValidateSuspendDebtorInvoices_NoRadioButtonNotChecked()
        {
            WaitForElement(Cpsuspenddebtorinvoices_0);
            ValidateElementNotChecked(Cpsuspenddebtorinvoices_0);

            return this;
        }

        public PersonRecordEditPage ClickSuspendDebtorInvoicesReasonLookupButton()
        {
            WaitForElementToBeClickable(CpsuspenddebtorinvoicesreasonidLookupButton);
            Click(CpsuspenddebtorinvoicesreasonidLookupButton);

            return this;
        }

        public PersonRecordEditPage ValidateSuspendDebtorInvoicesReasonLookupButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(CpsuspenddebtorinvoicesreasonidLookupButton);
            }
            else
            {
                ValidateElementNotDisabled(CpsuspenddebtorinvoicesreasonidLookupButton);
            }

            return this;
        }

        public PersonRecordEditPage ValidateDebtorNumber1Text(string ExpectedText)
        {
            ValidateElementValue(Debtornumber1, ExpectedText);

            return this;
        }

        public PersonRecordEditPage InsertTextOnDebtorNumber1(string TextToInsert)
        {
            WaitForElementToBeClickable(Debtornumber1);
            SendKeys(Debtornumber1, TextToInsert);

            return this;
        }

        public PersonRecordEditPage ClickAnonymousForBilling_YesRadioButton()
        {
            WaitForElementToBeClickable(Anonymousforbilling_1);
            Click(Anonymousforbilling_1);

            return this;
        }

        public PersonRecordEditPage ValidateAnonymousForBilling_YesRadioButtonChecked()
        {
            WaitForElement(Anonymousforbilling_1);
            ValidateElementChecked(Anonymousforbilling_1);

            return this;
        }

        public PersonRecordEditPage ValidateAnonymousForBilling_YesRadioButtonNotChecked()
        {
            WaitForElement(Anonymousforbilling_1);
            ValidateElementNotChecked(Anonymousforbilling_1);

            return this;
        }

        public PersonRecordEditPage ClickAnonymousForBilling_NoRadioButton()
        {
            WaitForElementToBeClickable(Anonymousforbilling_0);
            Click(Anonymousforbilling_0);

            return this;
        }

        public PersonRecordEditPage ValidateAnonymousForBilling_NoRadioButtonChecked()
        {
            WaitForElement(Anonymousforbilling_0);
            ValidateElementChecked(Anonymousforbilling_0);

            return this;
        }

        public PersonRecordEditPage ValidateAnonymousForBilling_NoRadioButtonNotChecked()
        {
            WaitForElement(Anonymousforbilling_0);
            ValidateElementNotChecked(Anonymousforbilling_0);

            return this;
        }

        public PersonRecordEditPage ValidatePaysByDirectDebitSecureFieldDisplayed()
        {
            WaitForElement(Paysbydirectdebit_SecureField);
            ScrollToElement(Paysbydirectdebit_SecureField);
            WaitForElementVisible(Paysbydirectdebit_SecureField);
            return this;
        }

        public PersonRecordEditPage ValidatePaysByDirectDebitFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Paysbydirectdebit_0);
                ValidateElementDisabled(Paysbydirectdebit_1);
            }
            else
            {

                ValidateElementNotDisabled(Paysbydirectdebit_0);
                ValidateElementNotDisabled(Paysbydirectdebit_1);
            }
            return this;
        }

        public PersonRecordEditPage ClickPaysByDirectDebit_YesRadioButton()
        {
            WaitForElementToBeClickable(Paysbydirectdebit_1);
            Click(Paysbydirectdebit_1);

            return this;
        }

        public PersonRecordEditPage ValidatePaysByDirectDebit_YesRadioButtonChecked()
        {
            WaitForElement(Paysbydirectdebit_1);
            ValidateElementChecked(Paysbydirectdebit_1);

            return this;
        }

        public PersonRecordEditPage ValidatePaysByDirectDebit_YesRadioButtonNotChecked()
        {
            WaitForElement(Paysbydirectdebit_1);
            ValidateElementNotChecked(Paysbydirectdebit_1);

            return this;
        }

        public PersonRecordEditPage ClickPaysByDirectDebit_NoRadioButton()
        {
            WaitForElementToBeClickable(Paysbydirectdebit_0);
            Click(Paysbydirectdebit_0);

            return this;
        }

        public PersonRecordEditPage ValidatePaysByDirectDebit_NoRadioButtonChecked()
        {
            WaitForElement(Paysbydirectdebit_0);
            ValidateElementChecked(Paysbydirectdebit_0);

            return this;
        }

        public PersonRecordEditPage ValidatePaysByDirectDebit_NoRadioButtonNotChecked()
        {
            WaitForElement(Paysbydirectdebit_0);
            ValidateElementNotChecked(Paysbydirectdebit_0);

            return this;
        }

        public PersonRecordEditPage ValidateBankAccountNumberFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Bankaccountnumber);
            }
            else
            {

                ValidateElementNotDisabled(Bankaccountnumber);
            }
            return this;
        }

        public PersonRecordEditPage ValidateBankAccountNumberVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(Bankaccountnumber);
            else
                WaitForElementNotVisible(Bankaccountnumber, 3);

            return this;
        }

        public PersonRecordEditPage ValidateBankAccountNumberText(string ExpectedText)
        {
            ValidateElementValue(Bankaccountnumber, ExpectedText);

            return this;
        }

        public PersonRecordEditPage InsertTextOnBankAccountNumber(string TextToInsert)
        {
            WaitForElementToBeClickable(Bankaccountnumber);
            SendKeys(Bankaccountnumber, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonRecordEditPage ValidateBankAccountNumberFieldAttribute(string AttributeName, string ExpectedValue)
        {
            ValidateElementAttribute(Bankaccountnumber, AttributeName, ExpectedValue);

            return this;
        }

        //method to validate bankaccountnumber form error message
        public PersonRecordEditPage ValidateBankAccountNumberFormErrorText(string ExpectedText)
        {
            WaitForElementVisible(BankAccountNumberFormError);
            ScrollToElement(BankAccountNumberFormError);
            ValidateElementByTitle(BankAccountNumberFormError, ExpectedText);

            return this;
        }


        public PersonRecordEditPage ValidateAUDDISRefFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Auddisref);
            }
            else
            {

                ValidateElementNotDisabled(Auddisref);
            }
            return this;
        }

        public PersonRecordEditPage ValidateAUDDISRefVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(Auddisref);
            else
                WaitForElementNotVisible(Auddisref, 3);

            return this;
        }

        public PersonRecordEditPage ValidateAUDDISRefText(string ExpectedText)
        {
            ValidateElementValue(Auddisref, ExpectedText);

            return this;
        }

        public PersonRecordEditPage InsertTextOnAUDDISRef(string TextToInsert)
        {
            WaitForElementToBeClickable(Auddisref);
            ScrollToElement(Auddisref);
            SendKeys(Auddisref, TextToInsert);
            return this;
        }

        public PersonRecordEditPage ValidateBankAccountSortCodeFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Bankaccountsortcode);
            }
            else
            {

                ValidateElementNotDisabled(Bankaccountsortcode);
            }
            return this;
        }

        public PersonRecordEditPage ValidateBankAccountSortCodeVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(Bankaccountsortcode);
            else
                WaitForElementNotVisible(Bankaccountsortcode, 3);

            return this;
        }

        public PersonRecordEditPage ValidateBankAccountSortCodeFieldAttribute(string AttributeName, string ExpectedValue)
        {
            ValidateElementAttribute(Bankaccountsortcode, AttributeName, ExpectedValue);

            return this;
        }

        public PersonRecordEditPage ValidateBankAccountSortCodeText(string ExpectedText)
        {
            WaitForElementVisible(Bankaccountsortcode);
            ScrollToElement(Bankaccountsortcode);
            ValidateElementValueByJavascript("CWField_bankaccountsortcode", ExpectedText);

            return this;
        }

        public PersonRecordEditPage InsertTextOnBankAccountSortCode(string TextToInsert)
        {
            WaitForElementToBeClickable(Bankaccountsortcode);
            ScrollToElement(Bankaccountsortcode);
            Click(Bankaccountsortcode);
            //SendKeys(Bankaccountsortcode, TextToInsert + Keys.Tab);
            SendKeysViaJavascript("CWField_bankaccountsortcode", TextToInsert);
            System.Threading.Thread.Sleep(1000);

            return this;
        }

        public PersonRecordEditPage ClickBankAccountNameField()
        {
            WaitForElementToBeClickable(Bankaccountname);
            ScrollToElement(Bankaccountname);
            Click(Bankaccountname);

            return this;
        }

        public PersonRecordEditPage ClickBody()
        {
            WaitForElement(Body);
            Click(Body);

            return this;
        }

        public PersonRecordEditPage PressTabOnBankSortCodeField()
        {
            ScrollToElement(Bankaccountsortcode);
            Click(Bankaccountsortcode);
            SendKeys(Bankaccountsortcode, Keys.Tab);
            return this;
        }


        //method to validate bankaccountsortcode form error message
        public PersonRecordEditPage ValidateBankAccountSortCodeFormErrorText(string ExpectedText)
        {
            WaitForElementVisible(BankAccountSortCodeFormError);
            ScrollToElement(BankAccountSortCodeFormError);
            ValidateElementByTitle(BankAccountSortCodeFormError, ExpectedText);

            return this;
        }

        public PersonRecordEditPage ValidateBankAccountNameFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Bankaccountname);
            }
            else
            {

                ValidateElementNotDisabled(Bankaccountname);
            }
            return this;
        }

        public PersonRecordEditPage ValidateBankAccountNameVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(Bankaccountname);
            else
                WaitForElementNotVisible(Bankaccountname, 3);

            return this;
        }

        public PersonRecordEditPage ValidateBankAccountNameFieldAttribute(string AttributeName, string ExpectedValue)
        {
            ValidateElementAttribute(Bankaccountname, AttributeName, ExpectedValue);

            return this;
        }

        public PersonRecordEditPage ValidateBankAccountNameText(string ExpectedText)
        {
            ValidateElementValue(Bankaccountname, ExpectedText);

            return this;
        }

        public PersonRecordEditPage InsertTextOnBankAccountName(string TextToInsert)
        {
            WaitForElementToBeClickable(Bankaccountname);
            SendKeys(Bankaccountname, TextToInsert);

            return this;
        }

        public PersonRecordEditPage ValidateTransactionTypeFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(TransactiontypeidLookupButton);
            }
            else
            {

                ValidateElementNotDisabled(TransactiontypeidLookupButton);
            }
            return this;
        }

        public PersonRecordEditPage ValidateTransactionTypeVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(TransactiontypeidLookupButton);
            else
                WaitForElementNotVisible(TransactiontypeidLookupButton, 3);

            return this;
        }

        public PersonRecordEditPage ClickTransactionTypeLookupButton()
        {
            WaitForElementToBeClickable(TransactiontypeidLookupButton);
            Click(TransactiontypeidLookupButton);

            return this;
        }

        public PersonRecordEditPage ValidateTransactionTypeLinkFieldText(string ExpectedText)
        {
            WaitForElement(TransactiontypeidLinkField);
            ValidateElementText(TransactiontypeidLinkField, ExpectedText);

            return this;
        }
        public PersonRecordEditPage ClickUnpinFromMeLinkButton()
        {
            WaitForElementToBeClickable(additionalIItemsButton);
            Click(additionalIItemsButton);

            WaitForElementToBeClickable(UnPinFromMeButton);
            Click(UnPinFromMeButton);

            return this;
        }

        public PersonRecordEditPage ClickPinToMeLinkButton()
        {
            WaitForElementToBeClickable(additionalIItemsButton);
            Click(additionalIItemsButton);

            WaitForElementToBeClickable(PinToMeButton);
            Click(PinToMeButton);

            return this;
        }

        public PersonRecordEditPage ClickPronounsLookupButton()
        {
            ScrollToElement(pronounsField_LookupButton);
            WaitForElementToBeClickable(pronounsField_LookupButton);
            Click(pronounsField_LookupButton);

            return this;
        }

        public PersonRecordEditPage ValidatePronounsLinkFieldText(string ExpectedValue)
        {
            ScrollToElement(pronouns_LinkField);
            WaitForElementToBeClickable(pronouns_LinkField);
            ValidateElementByTitle(pronouns_LinkField, ExpectedValue);
            return this;
        }

        public PersonRecordEditPage ValidatePronounsLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(pronounsField_LookupButton);
            else
                WaitForElementNotVisible(pronounsField_LookupButton, 3);

            return this;
        }

        //wait for BandingField LookupButton and click
        public PersonRecordEditPage ClickBandingFieldLookupButton()
        {
            WaitForElementToBeClickable(BandingField_LookupButton);
            ScrollToElement(BandingField_LookupButton);
            Click(BandingField_LookupButton);

            return this;
        }

        //method to validate BandingField_LookupButton visible
        public PersonRecordEditPage ValidateBandingFieldLookupButtonIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(BandingField_LookupButton);
            else
                WaitForElementNotVisible(BandingField_LookupButton, 3);

            return this;
        }

        //method to validate BandingField_LookupButton By Position visible
        public PersonRecordEditPage ValidateBandingFieldLookupButtonByIsVisible(int FieldPosition, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(BandingField_LookupButton_ByPosition(FieldPosition));
            else
                WaitForElementNotVisible(BandingField_LookupButton_ByPosition(FieldPosition), 3);

            return this;
        }

        //deprecated as part of ACC-6375
        //method to validate notes field by position visible
        //public PersonRecordEditPage ValidateNotesFieldIsVisible(int FieldPosition, bool ExpectVisible = true)
        //{
        //    if (ExpectVisible)
        //        WaitForElementVisible(Notes_Field_ByPosition(FieldPosition));
        //    else
        //        WaitForElementNotVisible(Notes_Field_ByPosition(FieldPosition), 3);

        //    return this;
        //}

        public PersonRecordEditPage ClickCloseButton()
        {
            ScrollToElement(DrwaerCloseButton);
            WaitForElementToBeClickable(DrwaerCloseButton);
            Click(DrwaerCloseButton);

            return this;
        }

        //verify the NextCarePlanReviewDate field value
        public PersonRecordEditPage ValidateNextCarePlanReviewDate(string ExpectedValue)
        {
            WaitForElement(NextCarePlanReviewDate);
            ScrollToElement(NextCarePlanReviewDate);
            ValidateElementValue(NextCarePlanReviewDate, ExpectedValue);

            return this;
        }

        //verify the NextCarePlanReviewDate field is visible or not visible
        public PersonRecordEditPage ValidateNextCarePlanReviewDateFieldIsPresent(bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(NextCarePlanReviewDate);
            else
                WaitForElementNotVisible(NextCarePlanReviewDate, 3);

            return this;
        }

        //verify the NextCarePlanReviewDate field is disabled or not disabled
        public PersonRecordEditPage ValidateNextCarePlanReviewDateFieldIsDisabled(bool ExpectDisabled = true)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(NextCarePlanReviewDate);
            else
                ValidateElementNotDisabled(NextCarePlanReviewDate);

            return this;
        }
    }
}
