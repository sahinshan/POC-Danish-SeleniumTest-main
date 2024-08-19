using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ContractSchemeRecordPage : CommonMethods
    {
        public ContractSchemeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CareProviderContractScheme = By.Id("iframe_careprovidercontractscheme");
        readonly By cwDialog_CareProviderContractScheme = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careprovidercontractscheme')]");

        readonly By ContactReasonRecordPageHeader = By.XPath("//*[@id='CWToolbar']//h1");
        readonly By PageTitle = By.XPath("//*[@id='CWToolbar']/div/h1/span");

        readonly By BackButton = By.Id("BackButton");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By AdditionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']//div[@id='CWToolbarMenu']/button");
        readonly By ActivateButton = By.Id("TI_ActivateButton");

        readonly By FinanceCodeMappingTab = By.Id("CWNavGroup_CareProviderFinanceCodeMapping");
        readonly By FinanceTransactionsTab = By.Id("CWNavGroup_FinanceTransactions");

        #region General Fields

        readonly By Name_LabelField = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
        readonly By Name_MandatoryField = By.XPath("//*[@id='CWLabelHolder_name']/label/span[@class='mandatory']");
        readonly By Name_Field = By.Id("CWField_name");
        readonly By Name_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");

        readonly By Code_LabelField = By.XPath("//*[@id='CWLabelHolder_code']/label[text()='Code']");
        readonly By Code_MandatoryField = By.XPath("//*[@id='CWLabelHolder_code']/label/span[@class='mandatory']");
        readonly By Code_Field = By.Id("CWField_code");
        readonly By Code_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_code']/label/span");

        readonly By GovCode_LabelField = By.XPath("//*[@id='CWLabelHolder_govcode']/label[text()='Gov Code']");
        readonly By GovCode_MandatoryField = By.XPath("//*[@id='CWLabelHolder_code']/label/span[@class='mandatory']");
        readonly By GovCode_Field = By.Id("CWField_govcode");
        readonly By GovCode_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_code']/label/span");

        readonly By Inactive_LabelField = By.XPath("//*[@id='CWLabelHolder_inactive']/label[text()='Inactive']");
        readonly By Inactive_YesOption = By.Id("CWField_inactive_1");
        readonly By Inactive_NoOption = By.Id("CWField_inactive_0");

        readonly By StartDate_LabelField = By.XPath("//*[@id='CWLabelHolder_startdate']/label[text()='Start Date']");
        readonly By StartDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_startdate']/label/span[@class='mandatory']");
        readonly By StartDate_Field = By.Id("CWField_startdate");
        readonly By StartDate_DatePicker = By.Id("CWField_startdate_DatePicker");

        readonly By EndDate_LabelField = By.XPath("//*[@id='CWLabelHolder_enddate']/label[text()='End Date']");
        readonly By EndDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_enddate']/label/span[@class='mandatory']");
        readonly By EndDate_Field = By.Id("CWField_enddate");
        readonly By EndDate_DatePicker = By.Id("CWField_enddate_DatePicker");

        readonly By ValidForExport_LabelField = By.XPath("//*[@id='CWLabelHolder_validforexport']/label[text()='Valid For Export']");
        readonly By ValidForExport_YesOption = By.Id("CWField_validforexport_1");
        readonly By ValidForExport_NoOption = By.Id("CWField_validforexport_0");

        readonly By ResponsibleTeam_LabelField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By ResponsibleTeam_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span[@class='mandatory']");
        readonly By ResponsibleTeamLink = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

        readonly By ResponsibleUser_LabelField = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label[text()='Responsible User']");
        readonly By ResponsibleUser_MandatoryField = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label/span[@class='mandatory']");
        readonly By ResponsibleUserLink = By.Id("CWField_responsibleuserid_Link");
        readonly By ResponsibleUser_LookupButton = By.Id("CWLookupBtn_responsibleuserid");
        readonly By ResponsibleUser_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_responsibleuserid']/label/span");

        #endregion

        #region Scheme

        readonly By Establishment_LabelField = By.XPath("//*[@id='CWLabelHolder_establishmentid']/label[text()='Establishment']");
        readonly By Establishment_MandatoryField = By.XPath("//*[@id='CWLabelHolder_establishmentid']/label/span[@class='mandatory']");
        readonly By EstablishmentLink = By.Id("CWField_establishmentid_Link");
        readonly By Establishment_LookupButton = By.Id("CWLookupBtn_establishmentid");
        readonly By Establishment_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_establishmentid']/label/span");

        readonly By Funder_LabelField = By.XPath("//*[@id='CWLabelHolder_funderid']/label[text()='Funder']");
        readonly By Funder_MandatoryField = By.XPath("//*[@id='CWLabelHolder_funderid']/label/span[@class='mandatory']");
        readonly By FunderLink = By.Id("CWField_funderid_Link");
        readonly By Funder_LookupButton = By.Id("CWLookupBtn_funderid");
        readonly By Funder_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_funderid']/label/span");

        readonly By FinanceCode_LabelField = By.XPath("//*[@id='CWLabelHolder_financecodeid']/label[text()='Finance Code']");
        readonly By FinanceCode_MandatoryField = By.XPath("//*[@id='CWLabelHolder_financecodeid']/label/span[@class='mandatory']");
        readonly By FinanceCodeLink = By.Id("CWField_financecodeid_Link");
        readonly By FinanceCode_LookupButton = By.Id("CWLookupBtn_financecodeid");

        readonly By ChargeScheduledCareOnActuals_LabelField = By.XPath("//*[@id='CWLabelHolder_chargescheduledcareonactuals']/label[text()='Charge Scheduled Care On Actuals?']");
        readonly By ChargeScheduledCareOnActuals_YesOption = By.Id("CWField_chargescheduledcareonactuals_1");
        readonly By ChargeScheduledCareOnActuals_NoOption = By.Id("CWField_chargescheduledcareonactuals_0");

        readonly By SundriesApply_LabelField = By.XPath("//*[@id='CWLabelHolder_sundriesapply']/label[text()='Sundries Apply?']");
        readonly By SundriesApply_YesOption = By.Id("CWField_sundriesapply_1");
        readonly By SundriesApply_NoOption = By.Id("CWField_sundriesapply_0");

        readonly By UpdateableOnPersonContractService_LabelField = By.XPath("//*[@id='CWLabelHolder_isupdateableonpersoncontractservice']/label[text()='Finance Code Updateable on Person Contract Service?']");
        readonly By UpdateableOnPersonContractService_MandatoryField = By.XPath("//*[@id='CWLabelHolder_isupdateableonpersoncontractservice']/label/span[@class='mandatory']");
        readonly By UpdateableOnPersonContractService_YesOption = By.Id("CWField_isupdateableonpersoncontractservice_1");
        readonly By UpdateableOnPersonContractService_NoOption = By.Id("CWField_isupdateableonpersoncontractservice_0");

        readonly By DefaultAllPersonContractsEnabledForScheduledBookings_LabelField = By.XPath("//*[@id='CWLabelHolder_defaultallpcenabledforscheduledbookings']/label[text()='Default All Person Contracts Enabled For Scheduled Bookings?']");
        readonly By DefaultAllPersonContractsEnabledForScheduledBookings_YesOption = By.Id("CWField_defaultallpcenabledforscheduledbookings_1");
        readonly By DefaultAllPersonContractsEnabledForScheduledBookings_NoOption = By.Id("CWField_defaultallpcenabledforscheduledbookings_0");

        readonly By AccountCodeApplies_LabelField = By.XPath("//*[@id='CWLabelHolder_accountcodeapplies']/label[text()='Account Code Applies?']");
        readonly By AccountCodeApplies_YesOption = By.Id("CWField_accountcodeapplies_1");
        readonly By AccountCodeApplies_NoOption = By.Id("CWField_accountcodeapplies_0");

        #endregion

        readonly By NoteText_LabelField = By.XPath("//*[@id='CWLabelHolder_notetext']/label[text()='Note Text']");
        readonly By NoteText_Field = By.Id("CWField_notetext");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");

        readonly By ActiveLabel = By.XPath("//*[@id ='CWInputFormFooter']//label[text()='Active']/following-sibling::span");

        #region Menu Items

        readonly By MenuButton = By.Id("CWNavGroup_Menu");
        readonly By Audit_MenuItem = By.Id("CWNavItem_AuditHistory");

        #endregion


        public ContractSchemeRecordPage WaitForContractSchemeRecordPageToLoad(bool DetailsTab = true, bool MiddleFrameVisible = true)
        {
            SwitchToDefaultFrame();

            WaitForElementVisible(contentIFrame);
            SwitchToIframe(contentIFrame);

            if (MiddleFrameVisible)
            {
                WaitForElementVisible(iframe_CareProviderContractScheme);
                SwitchToIframe(iframe_CareProviderContractScheme);
            }

            WaitForElementVisible(cwDialog_CareProviderContractScheme);
            SwitchToIframe(cwDialog_CareProviderContractScheme);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementVisible(ContactReasonRecordPageHeader);
            if (DetailsTab)
            {
                WaitForElementVisible(SaveButton);
                WaitForElementVisible(SaveAndCloseButton);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateAllFieldsVisible()
        {
            WaitForElementVisible(Name_LabelField);
            WaitForElementVisible(Name_Field);

            WaitForElementVisible(Code_LabelField);
            WaitForElementVisible(Code_Field);

            WaitForElementVisible(GovCode_LabelField);
            WaitForElementVisible(GovCode_Field);

            WaitForElementVisible(Inactive_LabelField);
            WaitForElementVisible(Inactive_YesOption);
            WaitForElementVisible(Inactive_NoOption);

            WaitForElementVisible(StartDate_LabelField);
            WaitForElementVisible(StartDate_Field);

            WaitForElementVisible(EndDate_LabelField);
            WaitForElementVisible(EndDate_Field);

            WaitForElementVisible(ValidForExport_LabelField);
            WaitForElementVisible(ValidForExport_YesOption);
            WaitForElementVisible(ValidForExport_NoOption);

            WaitForElementVisible(ResponsibleTeam_LabelField);
            WaitForElementVisible(ResponsibleTeam_LookupButton);

            WaitForElementVisible(ResponsibleUser_LabelField);
            WaitForElementVisible(ResponsibleUser_LookupButton);

            WaitForElementVisible(Establishment_LabelField);
            WaitForElementVisible(Establishment_LookupButton);

            WaitForElementVisible(Funder_LabelField);
            WaitForElementVisible(Funder_LookupButton);

            WaitForElementVisible(FinanceCode_LabelField);
            WaitForElementVisible(FinanceCode_LookupButton);

            WaitForElementVisible(ChargeScheduledCareOnActuals_LabelField);
            WaitForElementVisible(ChargeScheduledCareOnActuals_YesOption);
            WaitForElementVisible(ChargeScheduledCareOnActuals_NoOption);

            WaitForElementVisible(SundriesApply_LabelField);
            WaitForElementVisible(SundriesApply_YesOption);
            WaitForElementVisible(SundriesApply_NoOption);

            WaitForElementVisible(UpdateableOnPersonContractService_LabelField);
            WaitForElementVisible(UpdateableOnPersonContractService_YesOption);
            WaitForElementVisible(UpdateableOnPersonContractService_NoOption);

            WaitForElementVisible(DefaultAllPersonContractsEnabledForScheduledBookings_LabelField);
            WaitForElementVisible(DefaultAllPersonContractsEnabledForScheduledBookings_YesOption);
            WaitForElementVisible(DefaultAllPersonContractsEnabledForScheduledBookings_NoOption);

            WaitForElementVisible(AccountCodeApplies_LabelField);
            WaitForElementVisible(AccountCodeApplies_YesOption);
            WaitForElementVisible(AccountCodeApplies_NoOption);

            WaitForElementVisible(NoteText_LabelField);
            WaitForElementVisible(NoteText_Field);

            return this;
        }

        public ContractSchemeRecordPage ValidateContractSchemeRecordTitle(string ContractSchemeTitle)
        {
            WaitForElementVisible(PageTitle);
            MoveToElementInPage(PageTitle);
            ValidateElementText(PageTitle, ContractSchemeTitle);

            return this;
        }

        public ContractSchemeRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            MoveToElementInPage(BackButton);
            Click(BackButton);

            return this;
        }

        public ContractSchemeRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            MoveToElementInPage(SaveButton);
            Click(SaveButton);

            return this;
        }

        public ContractSchemeRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            MoveToElementInPage(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ContractSchemeRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            MoveToElementInPage(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public ContractSchemeRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            MoveToElementInPage(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public ContractSchemeRecordPage ValidateNameText(string ExpectedText)
        {
            WaitForElementVisible(Name_Field);
            MoveToElementInPage(Name_Field);
            ValidateElementValue(Name_Field, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage InsertName(string TextToInsert)
        {
            WaitForElementToBeClickable(Name_Field);
            MoveToElementInPage(Name_Field);
            SendKeys(Name_Field, TextToInsert);

            return this;
        }

        public ContractSchemeRecordPage ValidateCodeText(string ExpectedText)
        {
            WaitForElementVisible(Code_Field);
            MoveToElementInPage(Code_Field);
            ValidateElementValue(Code_Field, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage InsertCode(string TextToInsert)
        {
            WaitForElementToBeClickable(Code_Field);
            MoveToElementInPage(Code_Field);
            SendKeys(Code_Field, TextToInsert);
            SendKeysWithoutClearing(Code_Field, Keys.Tab);

            return this;
        }

        public ContractSchemeRecordPage ValidateGovCodeText(string ExpectedText)
        {
            WaitForElementVisible(GovCode_Field);
            MoveToElementInPage(GovCode_Field);
            ValidateElementValue(GovCode_Field, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage InsertGovCode(string TextToInsert)
        {
            WaitForElementToBeClickable(GovCode_Field);
            MoveToElementInPage(GovCode_Field);
            SendKeys(GovCode_Field, TextToInsert);
            SendKeysWithoutClearing(GovCode_Field, Keys.Tab);

            return this;
        }

        public ContractSchemeRecordPage ValidateStartDateText(string ExpectedText)
        {
            WaitForElementVisible(StartDate_Field);
            MoveToElementInPage(StartDate_Field);
            ValidateElementValue(StartDate_Field, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage InsertStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(StartDate_Field);
            MoveToElementInPage(StartDate_Field);
            SendKeys(StartDate_Field, TextToInsert);

            return this;
        }

        public ContractSchemeRecordPage ClickStartDateDatePicker()
        {
            WaitForElementToBeClickable(StartDate_DatePicker);
            MoveToElementInPage(StartDate_DatePicker);
            Click(StartDate_DatePicker);

            return this;
        }

        public ContractSchemeRecordPage ValidateEndDateText(string ExpectedText)
        {
            WaitForElementVisible(EndDate_Field);
            MoveToElementInPage(EndDate_Field);
            ValidateElementValue(EndDate_Field, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(EndDate_Field);
            MoveToElementInPage(EndDate_Field);
            SendKeys(EndDate_Field, TextToInsert);

            return this;
        }

        public ContractSchemeRecordPage ClickEndDateDatePicker()
        {
            WaitForElementToBeClickable(EndDate_DatePicker);
            MoveToElementInPage(EndDate_DatePicker);
            Click(EndDate_DatePicker);

            return this;
        }

        public ContractSchemeRecordPage ClickInactive_Option(bool YesOption)
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

        public ContractSchemeRecordPage ValidateInactive_OptionIsCheckedOrNot(bool YesOptionIsChecked)
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

        public ContractSchemeRecordPage ClickValidForExport_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(ValidForExport_YesOption);
                MoveToElementInPage(ValidForExport_YesOption);
                Click(ValidForExport_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(ValidForExport_NoOption);
                MoveToElementInPage(ValidForExport_NoOption);
                Click(ValidForExport_NoOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateValidForExport_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(ValidForExport_YesOption);
            WaitForElementVisible(ValidForExport_NoOption);
            MoveToElementInPage(ValidForExport_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(ValidForExport_YesOption);
                ValidateElementNotChecked(ValidForExport_NoOption);
            }
            else
            {
                ValidateElementChecked(ValidForExport_NoOption);
                ValidateElementNotChecked(ValidForExport_YesOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateChargeScheduledCareOnActuals_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(ChargeScheduledCareOnActuals_YesOption);
            WaitForElementVisible(ChargeScheduledCareOnActuals_NoOption);
            MoveToElementInPage(ChargeScheduledCareOnActuals_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(ChargeScheduledCareOnActuals_YesOption);
                ValidateElementNotChecked(ChargeScheduledCareOnActuals_NoOption);
            }
            else
            {
                ValidateElementChecked(ChargeScheduledCareOnActuals_NoOption);
                ValidateElementNotChecked(ChargeScheduledCareOnActuals_YesOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateSundriesApply_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(SundriesApply_YesOption);
            WaitForElementVisible(SundriesApply_NoOption);
            MoveToElementInPage(SundriesApply_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(SundriesApply_YesOption);
                ValidateElementNotChecked(SundriesApply_NoOption);
            }
            else
            {
                ValidateElementChecked(SundriesApply_NoOption);
                ValidateElementNotChecked(SundriesApply_YesOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateUpdateableOnPersonContractService_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(UpdateableOnPersonContractService_YesOption);
            WaitForElementVisible(UpdateableOnPersonContractService_NoOption);
            MoveToElementInPage(UpdateableOnPersonContractService_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(UpdateableOnPersonContractService_YesOption);
                ValidateElementNotChecked(UpdateableOnPersonContractService_NoOption);
            }
            else
            {
                ValidateElementChecked(UpdateableOnPersonContractService_NoOption);
                ValidateElementNotChecked(UpdateableOnPersonContractService_YesOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateDefaultAllPersonContractsEnabledForScheduledBookings_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(DefaultAllPersonContractsEnabledForScheduledBookings_YesOption);
            WaitForElementVisible(DefaultAllPersonContractsEnabledForScheduledBookings_NoOption);
            MoveToElementInPage(DefaultAllPersonContractsEnabledForScheduledBookings_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(DefaultAllPersonContractsEnabledForScheduledBookings_YesOption);
                ValidateElementNotChecked(DefaultAllPersonContractsEnabledForScheduledBookings_NoOption);
            }
            else
            {
                ValidateElementChecked(DefaultAllPersonContractsEnabledForScheduledBookings_NoOption);
                ValidateElementNotChecked(DefaultAllPersonContractsEnabledForScheduledBookings_YesOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateAccountCodeApplies_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(AccountCodeApplies_YesOption);
            WaitForElementVisible(AccountCodeApplies_NoOption);
            MoveToElementInPage(AccountCodeApplies_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(AccountCodeApplies_YesOption);
                ValidateElementNotChecked(AccountCodeApplies_NoOption);
            }
            else
            {
                ValidateElementChecked(AccountCodeApplies_NoOption);
                ValidateElementNotChecked(AccountCodeApplies_YesOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            MoveToElementInPage(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public ContractSchemeRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
            MoveToElementInPage(ResponsibleTeam_LookupButton);
            Click(ResponsibleTeam_LookupButton);

            return this;
        }

        public ContractSchemeRecordPage ClickResponsibleUserLink()
        {
            WaitForElementToBeClickable(ResponsibleUserLink);
            MoveToElementInPage(ResponsibleUserLink);
            Click(ResponsibleUserLink);

            return this;
        }

        public ContractSchemeRecordPage ValidateResponsibleUserLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleUserLink);
            ValidateElementText(ResponsibleUserLink, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage ClickResponsibleUserLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleUser_LookupButton);
            MoveToElementInPage(ResponsibleUser_LookupButton);
            Click(ResponsibleUser_LookupButton);

            return this;
        }

        public ContractSchemeRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(Name_FieldErrorLabel);
            MoveToElementInPage(Name_FieldErrorLabel);
            ValidateElementText(Name_FieldErrorLabel, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage ValidateCodeFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(Code_FieldErrorLabel);
            MoveToElementInPage(Code_FieldErrorLabel);
            ValidateElementText(Code_FieldErrorLabel, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage ValidateResponsibleUserFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(ResponsibleUser_FieldErrorLabel);
            MoveToElementInPage(ResponsibleUser_FieldErrorLabel);
            ValidateElementText(ResponsibleUser_FieldErrorLabel, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage ValidateEstablishmentFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(Establishment_FieldErrorLabel);
            MoveToElementInPage(Establishment_FieldErrorLabel);
            ValidateElementText(Establishment_FieldErrorLabel, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage ValidateFunderFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(Funder_FieldErrorLabel);
            MoveToElementInPage(Funder_FieldErrorLabel);
            ValidateElementText(Funder_FieldErrorLabel, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage ValidateNotificationAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage ValidateNameMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Name_LabelField);
            MoveToElementInPage(Name_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Name_MandatoryField);
            else
                WaitForElementNotVisible(Name_MandatoryField, 3);

            return this;
        }

        public ContractSchemeRecordPage ValidateCodeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Code_LabelField);
            MoveToElementInPage(Code_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Code_MandatoryField);
            else
                WaitForElementNotVisible(Code_MandatoryField, 3);

            return this;
        }

        public ContractSchemeRecordPage ValidateStartDateMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(StartDate_LabelField);
            MoveToElementInPage(StartDate_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(StartDate_MandatoryField);
            else
                WaitForElementNotVisible(StartDate_MandatoryField, 3);

            return this;
        }

        public ContractSchemeRecordPage ValidateEndDateMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(EndDate_LabelField);
            MoveToElementInPage(EndDate_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(EndDate_MandatoryField);
            else
                WaitForElementNotVisible(EndDate_MandatoryField, 3);

            return this;
        }

        public ContractSchemeRecordPage ValidateResponsibleTeamMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ResponsibleTeam_LabelField);
            MoveToElementInPage(ResponsibleTeam_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_MandatoryField);
            else
                WaitForElementNotVisible(ResponsibleTeam_MandatoryField, 3);

            return this;
        }

        public ContractSchemeRecordPage ValidateResponsibleUserMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ResponsibleUser_LabelField);
            MoveToElementInPage(ResponsibleUser_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ResponsibleUser_MandatoryField);
            else
                WaitForElementNotVisible(ResponsibleUser_MandatoryField, 3);

            return this;
        }

        public ContractSchemeRecordPage ValidateEstablishmentMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Establishment_LabelField);
            MoveToElementInPage(Establishment_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Establishment_MandatoryField);
            else
                WaitForElementNotVisible(Establishment_MandatoryField, 3);

            return this;
        }

        public ContractSchemeRecordPage ValidateFunderMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Funder_LabelField);
            MoveToElementInPage(Funder_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Funder_MandatoryField);
            else
                WaitForElementNotVisible(Funder_MandatoryField, 3);

            return this;
        }

        public ContractSchemeRecordPage ValidateFinanceCodeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(FinanceCode_LabelField);
            MoveToElementInPage(FinanceCode_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(FinanceCode_MandatoryField);
            else
                WaitForElementNotVisible(FinanceCode_MandatoryField, 3);

            return this;
        }

        public ContractSchemeRecordPage ValidateUpdateableOnPersonContractServiceMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(UpdateableOnPersonContractService_LabelField);
            MoveToElementInPage(UpdateableOnPersonContractService_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(UpdateableOnPersonContractService_MandatoryField);
            else
                WaitForElementNotVisible(UpdateableOnPersonContractService_MandatoryField, 3);

            return this;
        }

        public ContractSchemeRecordPage ValidateNameFieldMaximumLimitText(string expected)
        {
            WaitForElementVisible(Name_Field);
            ValidateElementMaxLength(Name_Field, expected);

            return this;
        }

        public ContractSchemeRecordPage ValidateCodeFieldMaximumLimitText(string expected)
        {
            WaitForElementVisible(Code_Field);
            ValidateElementMaxLength(Code_Field, expected);

            return this;
        }

        public ContractSchemeRecordPage ValidateGovCodeFieldMaximumLimitText(string expected)
        {
            WaitForElementVisible(GovCode_Field);
            ValidateElementMaxLength(GovCode_Field, expected);

            return this;
        }

        public ContractSchemeRecordPage ValidateNoteTextFieldMaximumLimitText(string expected)
        {
            WaitForElementVisible(NoteText_Field);
            ValidateElementMaxLength(NoteText_Field, expected);

            return this;
        }

        public ContractSchemeRecordPage ValidateCodeFieldIsNumeric()
        {
            WaitForElementVisible(Code_Field);
            ValidateElementAttribute(Code_Field, "validatenumeric", "ValidateNumeric");

            return this;
        }

        public ContractSchemeRecordPage ValidateStartDateDatePickerIsVisible()
        {
            WaitForElementVisible(StartDate_DatePicker);

            return this;
        }

        public ContractSchemeRecordPage ValidateEndDateDatePickerIsVisible()
        {
            WaitForElementVisible(EndDate_DatePicker);

            return this;
        }

        public ContractSchemeRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementVisible(DeleteRecordButton);
            WaitForElementVisible(AdditionalToolbarElementsButton);

            return this;
        }

        public ContractSchemeRecordPage ClickEstablishmentLookupButton()
        {
            WaitForElementToBeClickable(Establishment_LookupButton);
            MoveToElementInPage(Establishment_LookupButton);
            Click(Establishment_LookupButton);

            return this;
        }

        public ContractSchemeRecordPage ClickFunderLookupButton()
        {
            WaitForElementToBeClickable(Funder_LookupButton);
            MoveToElementInPage(Funder_LookupButton);
            Click(Funder_LookupButton);

            return this;
        }

        public ContractSchemeRecordPage ClickFinanceCodeLookupButton()
        {
            WaitForElementToBeClickable(FinanceCode_LookupButton);
            MoveToElementInPage(FinanceCode_LookupButton);
            Click(FinanceCode_LookupButton);

            return this;
        }

        public ContractSchemeRecordPage ValidateFinanceCodeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(FinanceCodeLink);
            MoveToElementInPage(FinanceCodeLink);
            ValidateElementText(FinanceCodeLink, ExpectedText);

            return this;
        }

        public ContractSchemeRecordPage ValidateNameFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Name_Field);
            MoveToElementInPage(Name_Field);
            if (IsDisabled)
                ValidateElementDisabled(Name_Field);
            else
                ValidateElementNotDisabled(Name_Field);

            return this;
        }

        public ContractSchemeRecordPage ValidateCodeFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Code_Field);
            MoveToElementInPage(Code_Field);
            if (IsDisabled)
                ValidateElementDisabled(Code_Field);
            else
                ValidateElementNotDisabled(Code_Field);

            return this;
        }

        public ContractSchemeRecordPage ValidateGovCodeFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(GovCode_Field);
            MoveToElementInPage(GovCode_Field);
            if (IsDisabled)
                ValidateElementDisabled(GovCode_Field);
            else
                ValidateElementNotDisabled(GovCode_Field);

            return this;
        }

        public ContractSchemeRecordPage ValidateStartDateFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(StartDate_Field);
            MoveToElementInPage(StartDate_Field);
            if (IsDisabled)
                ValidateElementDisabled(StartDate_Field);
            else
                ValidateElementNotDisabled(StartDate_Field);

            return this;
        }

        public ContractSchemeRecordPage ValidateEndDateFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(EndDate_Field);
            MoveToElementInPage(EndDate_Field);
            if (IsDisabled)
                ValidateElementDisabled(EndDate_Field);
            else
                ValidateElementNotDisabled(EndDate_Field);

            return this;
        }

        public ContractSchemeRecordPage ValidateNoteTextFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(NoteText_Field);
            MoveToElementInPage(NoteText_Field);
            if (IsDisabled)
                ValidateElementDisabled(NoteText_Field);
            else
                ValidateElementNotDisabled(NoteText_Field);

            return this;
        }

        public ContractSchemeRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ResponsibleTeam_LookupButton);
            MoveToElementInPage(ResponsibleTeam_LookupButton);
            if (IsDisabled)
                ValidateElementDisabled(ResponsibleTeam_LookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeam_LookupButton);

            return this;
        }

        public ContractSchemeRecordPage ValidateResponsibleUserLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ResponsibleUser_LookupButton);
            MoveToElementInPage(ResponsibleUser_LookupButton);
            if (IsDisabled)
                ValidateElementDisabled(ResponsibleUser_LookupButton);
            else
                ValidateElementNotDisabled(ResponsibleUser_LookupButton);

            return this;
        }

        public ContractSchemeRecordPage ValidateEstablishmentLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Establishment_LookupButton);
            MoveToElementInPage(Establishment_LookupButton);
            if (IsDisabled)
                ValidateElementDisabled(Establishment_LookupButton);
            else
                ValidateElementNotDisabled(Establishment_LookupButton);

            return this;
        }

        public ContractSchemeRecordPage ValidateFunderLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Funder_LookupButton);
            MoveToElementInPage(Funder_LookupButton);
            if (IsDisabled)
                ValidateElementDisabled(Funder_LookupButton);
            else
                ValidateElementNotDisabled(Funder_LookupButton);

            return this;
        }

        public ContractSchemeRecordPage ValidateFinanceCodeLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(FinanceCode_LookupButton);
            MoveToElementInPage(FinanceCode_LookupButton);
            if (IsDisabled)
                ValidateElementDisabled(FinanceCode_LookupButton);
            else
                ValidateElementNotDisabled(FinanceCode_LookupButton);

            return this;
        }

        public ContractSchemeRecordPage ValidateInactiveOptionsIsDisabled(bool IsDisabled)
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

        public ContractSchemeRecordPage ValidateValidForExportOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ValidForExport_YesOption);
            WaitForElementVisible(ValidForExport_NoOption);
            MoveToElementInPage(ValidForExport_NoOption);

            if (IsDisabled)
            {
                ValidateElementDisabled(ValidForExport_YesOption);
                ValidateElementDisabled(ValidForExport_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(ValidForExport_YesOption);
                ValidateElementNotDisabled(ValidForExport_NoOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateChargeScheduledCareOnActualsOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ChargeScheduledCareOnActuals_YesOption);
            WaitForElementVisible(ChargeScheduledCareOnActuals_NoOption);
            MoveToElementInPage(ChargeScheduledCareOnActuals_NoOption);

            if (IsDisabled)
            {
                ValidateElementDisabled(ChargeScheduledCareOnActuals_YesOption);
                ValidateElementDisabled(ChargeScheduledCareOnActuals_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(ChargeScheduledCareOnActuals_YesOption);
                ValidateElementNotDisabled(ChargeScheduledCareOnActuals_NoOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateSundriesApplyOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(SundriesApply_YesOption);
            WaitForElementVisible(SundriesApply_NoOption);
            MoveToElementInPage(SundriesApply_NoOption);

            if (IsDisabled)
            {
                ValidateElementDisabled(SundriesApply_YesOption);
                ValidateElementDisabled(SundriesApply_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(SundriesApply_YesOption);
                ValidateElementNotDisabled(SundriesApply_NoOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateUpdateableOnPersonContractServiceOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(UpdateableOnPersonContractService_YesOption);
            WaitForElementVisible(UpdateableOnPersonContractService_NoOption);
            MoveToElementInPage(UpdateableOnPersonContractService_NoOption);

            if (IsDisabled)
            {
                ValidateElementDisabled(UpdateableOnPersonContractService_YesOption);
                ValidateElementDisabled(UpdateableOnPersonContractService_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(UpdateableOnPersonContractService_YesOption);
                ValidateElementNotDisabled(UpdateableOnPersonContractService_NoOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateDefaultAllPersonContractsEnabledForScheduledBookingsOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(DefaultAllPersonContractsEnabledForScheduledBookings_YesOption);
            WaitForElementVisible(DefaultAllPersonContractsEnabledForScheduledBookings_NoOption);
            MoveToElementInPage(DefaultAllPersonContractsEnabledForScheduledBookings_NoOption);

            if (IsDisabled)
            {
                ValidateElementDisabled(DefaultAllPersonContractsEnabledForScheduledBookings_YesOption);
                ValidateElementDisabled(DefaultAllPersonContractsEnabledForScheduledBookings_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(DefaultAllPersonContractsEnabledForScheduledBookings_YesOption);
                ValidateElementNotDisabled(DefaultAllPersonContractsEnabledForScheduledBookings_NoOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateAccountCodeAppliesOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(AccountCodeApplies_YesOption);
            WaitForElementVisible(AccountCodeApplies_NoOption);
            MoveToElementInPage(AccountCodeApplies_NoOption);

            if (IsDisabled)
            {
                ValidateElementDisabled(AccountCodeApplies_YesOption);
                ValidateElementDisabled(AccountCodeApplies_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(AccountCodeApplies_YesOption);
                ValidateElementNotDisabled(AccountCodeApplies_NoOption);
            }

            return this;
        }

        public ContractSchemeRecordPage ValidateActivateButtonIsVisible(bool IsVisible)
        {
            if (IsVisible)
                WaitForElementVisible(ActivateButton);
            else
                WaitForElementNotVisible(ActivateButton, 3);

            return this;
        }

        public ContractSchemeRecordPage ValidateContractSchemeActiveLabelText(string ExpectedText)
        {
            WaitForElement(ActiveLabel);
            MoveToElementInPage(ActiveLabel);
            ValidateElementText(ActiveLabel, ExpectedText);
            return this;
        }

        public ContractSchemeRecordPage ClickActivateButton()
        {
            WaitForElementToBeClickable(ActivateButton);
            MoveToElementInPage(ActivateButton);
            Click(ActivateButton);

            return this;
        }

        public ContractSchemeRecordPage ClickMenuButton()
        {
            WaitForElementToBeClickable(MenuButton);
            MoveToElementInPage(MenuButton);
            Click(MenuButton);

            return this;
        }

        public ContractSchemeRecordPage NavigateToAuditPage()
        {
            ClickMenuButton();

            WaitForElementToBeClickable(Audit_MenuItem);
            MoveToElementInPage(Audit_MenuItem);
            Click(Audit_MenuItem);

            return this;
        }

        public ContractSchemeRecordPage NavigateToFinanceCodeMappingsTab()
        {
            WaitForElementToBeClickable(FinanceCodeMappingTab);
            MoveToElementInPage(FinanceCodeMappingTab);
            Click(FinanceCodeMappingTab);

            return this;
        }

        public ContractSchemeRecordPage NavigateToFinanceTransactionsTab()
        {
            WaitForElementToBeClickable(FinanceTransactionsTab);
            MoveToElementInPage(FinanceTransactionsTab);
            Click(FinanceTransactionsTab);

            return this;
        }

    }
}
