using OpenQA.Selenium;
using System;


namespace Phoenix.UITests.Framework.PageObjects
{
    public class CPMasterPayArrangementRecordPage : CommonMethods
    {

        public CPMasterPayArrangementRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careprovidermasterpayarrangement')]");

        readonly By PageHeader = By.XPath("//*[@id='CWToolbar']//h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
        readonly By RunOnDemandWorkflow = By.XPath("//*[@id='TI_RunOnDemandWorkflow']");

        readonly By Name = By.XPath("//*[@id='CWField_name']");
        readonly By Issystemuseremploymentcontractall_1 = By.XPath("//*[@id='CWField_issystemuseremploymentcontractall_1']");
        readonly By Issystemuseremploymentcontractall_0 = By.XPath("//*[@id='CWField_issystemuseremploymentcontractall_0']");
        By EmployeeContracts_SelectedOptionLink(string RecordId) => By.XPath("//*[@id='" + RecordId + "_Link']");
        readonly By EmployeeContractsidLookupButton = By.XPath("//*[@id='CWLookupBtn_systemuseremploymentcontractid']");
        readonly By Isproviderall_1 = By.XPath("//*[@id='CWField_isproviderall_1']");
        readonly By Isproviderall_0 = By.XPath("//*[@id='CWField_isproviderall_0']");
        By Providers_SelectedOptionLink(string RecordId) => By.XPath("//*[@id='" + RecordId + "_Link']");
        By Providers_SelectedOptionRemoveButton(string RecordId) => By.XPath("//*[@id='MS_providerid_" + RecordId + "']/a");
        readonly By ProvideridLookupButton = By.XPath("//*[@id='CWLookupBtn_providerid']");
        readonly By Iscontractschemeall_1 = By.XPath("//*[@id='CWField_iscontractschemeall_1']");
        readonly By Iscontractschemeall_0 = By.XPath("//*[@id='CWField_iscontractschemeall_0']");
        readonly By ContractschemeidLookupButton = By.XPath("//*[@id='CWLookupBtn_contractschemeid']");
        readonly By Iscareproviderstaffroletypeall_1 = By.XPath("//*[@id='CWField_iscareproviderstaffroletypeall_1']");
        readonly By Iscareproviderstaffroletypeall_0 = By.XPath("//*[@id='CWField_iscareproviderstaffroletypeall_0']");
        By Roles_SelectedOptionLink(string RecordId) => By.XPath("//*[@id='" + RecordId + "_Link']");
        readonly By CareproviderstaffroletypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderstaffroletypeid']");
        readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
        readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
        readonly By Isdraft_1 = By.XPath("//*[@id='CWField_isdraft_1']");
        readonly By Isdraft_0 = By.XPath("//*[@id='CWField_isdraft_0']");
        readonly By Allowforhybridrates_1 = By.XPath("//*[@id='CWField_allowforhybridrates_1']");
        readonly By Allowforhybridrates_0 = By.XPath("//*[@id='CWField_allowforhybridrates_0']");
        readonly By Ispayscheduledcareonactuals_1 = By.XPath("//*[@id='CWField_ispayscheduledcareonactuals_1']");
        readonly By Ispayscheduledcareonactuals_0 = By.XPath("//*[@id='CWField_ispayscheduledcareonactuals_0']");
        readonly By Defaultrate = By.XPath("//*[@id='CWField_defaultrate']");
        readonly By Defaultrate_ErrorLabel = By.XPath("//*[@for='CWField_defaultrate'][@class='formerror']//span");
        readonly By Isemploymentcontracttypeall_1 = By.XPath("//*[@id='CWField_isemploymentcontracttypeall_1']");
        readonly By Isemploymentcontracttypeall_0 = By.XPath("//*[@id='CWField_isemploymentcontracttypeall_0']");
        By ContractTypes_SelectedOptionLink(string RecordId) => By.XPath("//*[@id='" + RecordId + "_Link']");
        readonly By EmploymentcontracttypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_employmentcontracttypeid']");
        readonly By Iscpbookingtypeall_1 = By.XPath("//*[@id='CWField_iscpbookingtypeall_1']");
        readonly By Iscpbookingtypeall_0 = By.XPath("//*[@id='CWField_iscpbookingtypeall_0']");
        By BookingTypes_SelectedOptionLink(string RecordId) => By.XPath("//*[@id='" + RecordId + "_Link']");
        readonly By CpbookingtypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingtypeid']");
        readonly By Ispersoncontractall_1 = By.XPath("//*[@id='CWField_ispersoncontractall_1']");
        readonly By Ispersoncontractall_0 = By.XPath("//*[@id='CWField_ispersoncontractall_0']");
        readonly By PersoncontractidLookupButton = By.XPath("//*[@id='CWLookupBtn_personcontractid']");
        readonly By Iscptimebandsetall_1 = By.XPath("//*[@id='CWField_iscptimebandsetall_1']");
        readonly By Iscptimebandsetall_0 = By.XPath("//*[@id='CWField_iscptimebandsetall_0']");
        By TimebandSets_SelectedOptionLink(string RecordId) => By.XPath("//*[@id='" + RecordId + "_Link']");
        readonly By CptimebandsetidLookupButton = By.XPath("//*[@id='CWLookupBtn_cptimebandsetid']");
        readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");
        readonly By EnddateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
        readonly By Cppayrollunittypeid = By.XPath("//*[@id='CWField_cppayrollunittypeid']");
        readonly By Applyminbookinglength_1 = By.XPath("//*[@id='CWField_applyminbookinglength_1']");
        readonly By Applyminbookinglength_0 = By.XPath("//*[@id='CWField_applyminbookinglength_0']");
        readonly By Applymaxbookinglength_1 = By.XPath("//*[@id='CWField_applymaxbookinglength_1']");
        readonly By Applymaxbookinglength_0 = By.XPath("//*[@id='CWField_applymaxbookinglength_0']");
        readonly By DurationFromMinutes = By.XPath("//*[@id='CWField_minbookinglength']");
        readonly By DurationFromMinutes_ErrorLabel = By.XPath("//*[@for='CWField_minbookinglength'][@class='formerror']//span");
        readonly By DurationToMinutes = By.XPath("//*[@id='CWField_maxbookinglength']");
        readonly By DurationToMinutes_ErrorLabel = By.XPath("//*[@for='CWField_maxbookinglength'][@class='formerror']//span");



        public CPMasterPayArrangementRecordPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(PageHeader);
            WaitForElementVisible(BackButton);
            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);

            return this;
        }



        public CPMasterPayArrangementRecordPage ValidateDisplayedFieldsInCreateMode()
        {
            //Right side elements
            WaitForElementVisible(Name);
            WaitForElementVisible(Issystemuseremploymentcontractall_1);
            WaitForElementVisible(Issystemuseremploymentcontractall_0);
            WaitForElementVisible(EmployeeContractsidLookupButton);
            WaitForElementVisible(Isproviderall_1);
            WaitForElementVisible(Isproviderall_0);
            WaitForElementVisible(ProvideridLookupButton);
            WaitForElementVisible(Iscontractschemeall_1);
            WaitForElementVisible(Iscontractschemeall_0);
            WaitForElementVisible(Iscareproviderstaffroletypeall_1);
            WaitForElementVisible(Iscareproviderstaffroletypeall_0);
            WaitForElementVisible(Startdate);
            WaitForElementVisible(Isdraft_1);
            WaitForElementVisible(Isdraft_0);
            WaitForElementVisible(Allowforhybridrates_1);
            WaitForElementVisible(Allowforhybridrates_0);
            WaitForElementVisible(Ispayscheduledcareonactuals_1);
            WaitForElementVisible(Ispayscheduledcareonactuals_0);

            //Left side elements
            WaitForElementVisible(Defaultrate);
            WaitForElementVisible(Isemploymentcontracttypeall_1);
            WaitForElementVisible(Isemploymentcontracttypeall_0);
            WaitForElementVisible(Iscpbookingtypeall_1);
            WaitForElementVisible(Iscpbookingtypeall_0);
            WaitForElementVisible(CpbookingtypeidLookupButton);
            WaitForElementVisible(Ispersoncontractall_1);
            WaitForElementVisible(Ispersoncontractall_0);
            WaitForElementVisible(Iscptimebandsetall_1);
            WaitForElementVisible(Iscptimebandsetall_0);
            WaitForElementVisible(CptimebandsetidLookupButton);
            WaitForElementVisible(Enddate);
            WaitForElementVisible(Cppayrollunittypeid);
            WaitForElementVisible(Applyminbookinglength_1);
            WaitForElementVisible(Applyminbookinglength_0);
            WaitForElementVisible(Applymaxbookinglength_1);
            WaitForElementVisible(Applymaxbookinglength_0);

            return this;
        }



        public CPMasterPayArrangementRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickRunOnDemandWorkflow()
        {
            WaitForElementToBeClickable(RunOnDemandWorkflow);
            Click(RunOnDemandWorkflow);

            return this;
        }



        public CPMasterPayArrangementRecordPage ValidateNameText(string ExpectedText)
        {
            ValidateElementValue(Name, ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage InsertTextOnName(string TextToInsert)
        {
            WaitForElementToBeClickable(Name);
            SendKeys(Name, TextToInsert);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateNameFieldMaxLenght(string ExpectedAttributeValue)
        {
            ValidateElementAttribute(Name, "maxlength", ExpectedAttributeValue);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllEmployeeContracts_YesRadioButton()
        {
            WaitForElementToBeClickable(Issystemuseremploymentcontractall_1);
            Click(Issystemuseremploymentcontractall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllEmployeeContracts_YesRadioButtonChecked()
        {
            WaitForElement(Issystemuseremploymentcontractall_1);
            ValidateElementChecked(Issystemuseremploymentcontractall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllEmployeeContracts_YesRadioButtonNotChecked()
        {
            WaitForElement(Issystemuseremploymentcontractall_1);
            ValidateElementNotChecked(Issystemuseremploymentcontractall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllEmployeeContracts_NoRadioButton()
        {
            WaitForElementToBeClickable(Issystemuseremploymentcontractall_0);
            Click(Issystemuseremploymentcontractall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllEmployeeContracts_NoRadioButtonChecked()
        {
            WaitForElement(Issystemuseremploymentcontractall_0);
            ValidateElementChecked(Issystemuseremploymentcontractall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllEmployeeContracts_NoRadioButtonNotChecked()
        {
            WaitForElement(Issystemuseremploymentcontractall_0);
            ValidateElementNotChecked(Issystemuseremploymentcontractall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickEmployeeContracts_SelectedOptionLink(string RecordId)
        {
            WaitForElementToBeClickable(EmployeeContracts_SelectedOptionLink(RecordId));
            Click(EmployeeContracts_SelectedOptionLink(RecordId));

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateEmployeeContracts_SelectedOptionLinkText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(EmployeeContracts_SelectedOptionLink(RecordId));
            ValidateElementText(EmployeeContracts_SelectedOptionLink(RecordId), ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickEmployeeContractsLookupButton()
        {
            WaitForElementToBeClickable(EmployeeContractsidLookupButton);
            Click(EmployeeContractsidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateEmployeeContractsLookupButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(EmployeeContractsidLookupButton);
            else
                ValidateElementDisabled(EmployeeContractsidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateEmployeeContractsLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EmployeeContractsidLookupButton);
            else
                WaitForElementNotVisible(EmployeeContractsidLookupButton, 3);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllProviders_YesRadioButton()
        {
            WaitForElementToBeClickable(Isproviderall_1);
            Click(Isproviderall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllProviders_YesRadioButtonChecked()
        {
            WaitForElement(Isproviderall_1);
            ValidateElementChecked(Isproviderall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllProviders_YesRadioButtonNotChecked()
        {
            WaitForElement(Isproviderall_1);
            ValidateElementNotChecked(Isproviderall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllProviders_NoRadioButton()
        {
            WaitForElementToBeClickable(Isproviderall_0);
            Click(Isproviderall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllProviders_NoRadioButtonChecked()
        {
            WaitForElement(Isproviderall_0);
            ValidateElementChecked(Isproviderall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllProviders_NoRadioButtonNotChecked()
        {
            WaitForElement(Isproviderall_0);
            ValidateElementNotChecked(Isproviderall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickProviders_SelectedOptionLink(string RecordId)
        {
            WaitForElementToBeClickable(Providers_SelectedOptionLink(RecordId));
            Click(Providers_SelectedOptionLink(RecordId));

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateProviders_SelectedOptionLinkText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(Providers_SelectedOptionLink(RecordId));
            ValidateElementText(Providers_SelectedOptionLink(RecordId), ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickProviders_SelectedOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(Providers_SelectedOptionRemoveButton(RecordId));
            Click(Providers_SelectedOptionRemoveButton(RecordId));

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickProviders_SelectedOptionRemoveButton(Guid RecordId)
        {
            return ClickProviders_SelectedOptionRemoveButton(RecordId.ToString());
        }

        public CPMasterPayArrangementRecordPage ClickProvidersLookupButton()
        {
            WaitForElementToBeClickable(ProvideridLookupButton);
            Click(ProvideridLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateProvidersLookupButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(ProvideridLookupButton);
            else
                ValidateElementDisabled(ProvideridLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateProvidersLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ProvideridLookupButton);
            else
                WaitForElementNotVisible(ProvideridLookupButton, 3);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllContractSchemes_YesRadioButton()
        {
            WaitForElementToBeClickable(Iscontractschemeall_1);
            Click(Iscontractschemeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllContractSchemes_YesRadioButtonChecked()
        {
            WaitForElement(Iscontractschemeall_1);
            ValidateElementChecked(Iscontractschemeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllContractSchemes_YesRadioButtonNotChecked()
        {
            WaitForElement(Iscontractschemeall_1);
            ValidateElementNotChecked(Iscontractschemeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllContractSchemes_NoRadioButton()
        {
            WaitForElementToBeClickable(Iscontractschemeall_0);
            Click(Iscontractschemeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllContractSchemes_NoRadioButtonChecked()
        {
            WaitForElement(Iscontractschemeall_0);
            ValidateElementChecked(Iscontractschemeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllContractSchemes_NoRadioButtonNotChecked()
        {
            WaitForElement(Iscontractschemeall_0);
            ValidateElementNotChecked(Iscontractschemeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllContractSchemes_YesRadioButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(Iscontractschemeall_1);
            else
                ValidateElementDisabled(Iscontractschemeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllContractSchemes_NoRadioButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(Iscontractschemeall_0);
            else
                ValidateElementDisabled(Iscontractschemeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickContractSchemesLookupButton()
        {
            WaitForElementToBeClickable(ContractschemeidLookupButton);
            Click(ContractschemeidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateContractSchemesLookupButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(ContractschemeidLookupButton);
            else
                ValidateElementDisabled(ContractschemeidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateContractSchemesLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ContractschemeidLookupButton);
            else
                WaitForElementNotVisible(ContractschemeidLookupButton, 3);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllRoles_YesRadioButton()
        {
            WaitForElementToBeClickable(Iscareproviderstaffroletypeall_1);
            Click(Iscareproviderstaffroletypeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllRoles_YesRadioButtonChecked()
        {
            WaitForElement(Iscareproviderstaffroletypeall_1);
            ValidateElementChecked(Iscareproviderstaffroletypeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllRoles_YesRadioButtonNotChecked()
        {
            WaitForElement(Iscareproviderstaffroletypeall_1);
            ValidateElementNotChecked(Iscareproviderstaffroletypeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllRoles_NoRadioButton()
        {
            WaitForElementToBeClickable(Iscareproviderstaffroletypeall_0);
            Click(Iscareproviderstaffroletypeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllRoles_NoRadioButtonChecked()
        {
            WaitForElement(Iscareproviderstaffroletypeall_0);
            ValidateElementChecked(Iscareproviderstaffroletypeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllRoles_NoRadioButtonNotChecked()
        {
            WaitForElement(Iscareproviderstaffroletypeall_0);
            ValidateElementNotChecked(Iscareproviderstaffroletypeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickRoles_SelectedOptionLink(string RecordId)
        {
            WaitForElementToBeClickable(Roles_SelectedOptionLink(RecordId));
            Click(Roles_SelectedOptionLink(RecordId));

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateRoles_SelectedOptionLinkText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(Roles_SelectedOptionLink(RecordId));
            ValidateElementText(Roles_SelectedOptionLink(RecordId), ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickRolesLookupButton()
        {
            WaitForElementToBeClickable(CareproviderstaffroletypeidLookupButton);
            Click(CareproviderstaffroletypeidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateRolesLookupButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(CareproviderstaffroletypeidLookupButton);
            else
                ValidateElementDisabled(CareproviderstaffroletypeidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateRolesLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CareproviderstaffroletypeidLookupButton);
            else
                WaitForElementNotVisible(CareproviderstaffroletypeidLookupButton, 3);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateStartDateText(string ExpectedText)
        {
            ValidateElementValue(Startdate, ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage InsertTextOnStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Startdate);
            SendKeys(Startdate, TextToInsert);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickStartDateDatePicker()
        {
            WaitForElementToBeClickable(StartdateDatePicker);
            Click(StartdateDatePicker);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickSaveAsDraft_YesRadioButton()
        {
            WaitForElementToBeClickable(Isdraft_1);
            Click(Isdraft_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateSaveAsDraft_YesRadioButtonChecked()
        {
            WaitForElement(Isdraft_1);
            ValidateElementChecked(Isdraft_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateSaveAsDraft_YesRadioButtonNotChecked()
        {
            WaitForElement(Isdraft_1);
            ValidateElementNotChecked(Isdraft_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickSaveAsDraft_NoRadioButton()
        {
            WaitForElementToBeClickable(Isdraft_0);
            Click(Isdraft_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateSaveAsDraft_NoRadioButtonChecked()
        {
            WaitForElement(Isdraft_0);
            ValidateElementChecked(Isdraft_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateSaveAsDraft_NoRadioButtonNotChecked()
        {
            WaitForElement(Isdraft_0);
            ValidateElementNotChecked(Isdraft_0);
            return this;
        }

        public CPMasterPayArrangementRecordPage ClickAllowForHybridRates_YesRadioButton()
        {
            WaitForElementToBeClickable(Allowforhybridrates_1);
            Click(Allowforhybridrates_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateAllowForHybridRates_YesRadioButtonChecked()
        {
            WaitForElement(Allowforhybridrates_1);
            ValidateElementChecked(Allowforhybridrates_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateAllowForHybridRates_YesRadioButtonNotChecked()
        {
            WaitForElement(Allowforhybridrates_1);
            ValidateElementNotChecked(Allowforhybridrates_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickAllowForHybridRates_NoRadioButton()
        {
            WaitForElementToBeClickable(Allowforhybridrates_0);
            Click(Allowforhybridrates_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateAllowForHybridRates_NoRadioButtonChecked()
        {
            WaitForElement(Allowforhybridrates_0);
            ValidateElementChecked(Allowforhybridrates_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateAllowForHybridRates_NoRadioButtonNotChecked()
        {
            WaitForElement(Allowforhybridrates_0);
            ValidateElementNotChecked(Allowforhybridrates_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickPayScheduledCareOnActuals_YesRadioButton()
        {
            WaitForElementToBeClickable(Ispayscheduledcareonactuals_1);
            Click(Ispayscheduledcareonactuals_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidatePayScheduledCareOnActuals_YesRadioButtonChecked()
        {
            WaitForElement(Ispayscheduledcareonactuals_1);
            ValidateElementChecked(Ispayscheduledcareonactuals_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidatePayScheduledCareOnActuals_YesRadioButtonNotChecked()
        {
            WaitForElement(Ispayscheduledcareonactuals_1);
            ValidateElementNotChecked(Ispayscheduledcareonactuals_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickPayScheduledCareOnActuals_NoRadioButton()
        {
            WaitForElementToBeClickable(Ispayscheduledcareonactuals_0);
            Click(Ispayscheduledcareonactuals_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidatePayScheduledCareOnActuals_NoRadioButtonChecked()
        {
            WaitForElement(Ispayscheduledcareonactuals_0);
            ValidateElementChecked(Ispayscheduledcareonactuals_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidatePayScheduledCareOnActuals_NoRadioButtonNotChecked()
        {
            WaitForElement(Ispayscheduledcareonactuals_0);
            ValidateElementNotChecked(Ispayscheduledcareonactuals_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateRateText(string ExpectedText)
        {
            ValidateElementValue(Defaultrate, ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage InsertTextOnRate(string TextToInsert)
        {
            WaitForElementToBeClickable(Defaultrate);
            SendKeys(Defaultrate, TextToInsert + Keys.Tab);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateRateErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(Defaultrate_ErrorLabel);
            else
                WaitForElementNotVisible(Defaultrate_ErrorLabel, 3);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateRateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Defaultrate_ErrorLabel, ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllContractTypes_YesRadioButton()
        {
            WaitForElementToBeClickable(Isemploymentcontracttypeall_1);
            Click(Isemploymentcontracttypeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllContractTypes_YesRadioButtonChecked()
        {
            WaitForElement(Isemploymentcontracttypeall_1);
            ValidateElementChecked(Isemploymentcontracttypeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllContractTypes_YesRadioButtonNotChecked()
        {
            WaitForElement(Isemploymentcontracttypeall_1);
            ValidateElementNotChecked(Isemploymentcontracttypeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllContractTypes_NoRadioButton()
        {
            WaitForElementToBeClickable(Isemploymentcontracttypeall_0);
            Click(Isemploymentcontracttypeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllContractTypes_NoRadioButtonChecked()
        {
            WaitForElement(Isemploymentcontracttypeall_0);
            ValidateElementChecked(Isemploymentcontracttypeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllContractTypes_NoRadioButtonNotChecked()
        {
            WaitForElement(Isemploymentcontracttypeall_0);
            ValidateElementNotChecked(Isemploymentcontracttypeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickContractTypes_SelectedOptionLink(string RecordId)
        {
            WaitForElementToBeClickable(ContractTypes_SelectedOptionLink(RecordId));
            Click(ContractTypes_SelectedOptionLink(RecordId));

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateContractTypes_SelectedOptionLinkText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(ContractTypes_SelectedOptionLink(RecordId));
            ValidateElementText(ContractTypes_SelectedOptionLink(RecordId), ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickContractTypesLookupButton()
        {
            WaitForElementToBeClickable(EmploymentcontracttypeidLookupButton);
            Click(EmploymentcontracttypeidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateContractTypesLookupButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(EmploymentcontracttypeidLookupButton);
            else
                ValidateElementDisabled(EmploymentcontracttypeidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateContractTypesLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EmploymentcontracttypeidLookupButton);
            else
                WaitForElementNotVisible(EmploymentcontracttypeidLookupButton, 3);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllBookingTypes_YesRadioButton()
        {
            WaitForElementToBeClickable(Iscpbookingtypeall_1);
            Click(Iscpbookingtypeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllBookingTypes_YesRadioButtonChecked()
        {
            WaitForElement(Iscpbookingtypeall_1);
            ValidateElementChecked(Iscpbookingtypeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllBookingTypes_YesRadioButtonNotChecked()
        {
            WaitForElement(Iscpbookingtypeall_1);
            ValidateElementNotChecked(Iscpbookingtypeall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllBookingTypes_NoRadioButton()
        {
            WaitForElementToBeClickable(Iscpbookingtypeall_0);
            Click(Iscpbookingtypeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllBookingTypes_NoRadioButtonChecked()
        {
            WaitForElement(Iscpbookingtypeall_0);
            ValidateElementChecked(Iscpbookingtypeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllBookingTypes_NoRadioButtonNotChecked()
        {
            WaitForElement(Iscpbookingtypeall_0);
            ValidateElementNotChecked(Iscpbookingtypeall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickBookingTypes_SelectedOptionLink(string RecordId)
        {
            WaitForElementToBeClickable(BookingTypes_SelectedOptionLink(RecordId));
            Click(BookingTypes_SelectedOptionLink(RecordId));

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateBookingTypes_SelectedOptionLinkText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(BookingTypes_SelectedOptionLink(RecordId));
            ValidateElementText(BookingTypes_SelectedOptionLink(RecordId), ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickBookingTypesLookupButton()
        {
            WaitForElementToBeClickable(CpbookingtypeidLookupButton);
            Click(CpbookingtypeidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateBookingTypesLookupButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(CpbookingtypeidLookupButton);
            else
                ValidateElementDisabled(CpbookingtypeidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateBookingTypesLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CpbookingtypeidLookupButton);
            else
                WaitForElementNotVisible(CpbookingtypeidLookupButton, 3);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllPersonContracts_YesRadioButton()
        {
            WaitForElementToBeClickable(Ispersoncontractall_1);
            Click(Ispersoncontractall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllPersonContracts_YesRadioButtonChecked()
        {
            WaitForElement(Ispersoncontractall_1);
            ValidateElementChecked(Ispersoncontractall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllPersonContracts_YesRadioButtonNotChecked()
        {
            WaitForElement(Ispersoncontractall_1);
            ValidateElementNotChecked(Ispersoncontractall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllPersonContracts_NoRadioButton()
        {
            WaitForElementToBeClickable(Ispersoncontractall_0);
            Click(Ispersoncontractall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllPersonContracts_NoRadioButtonChecked()
        {
            WaitForElement(Ispersoncontractall_0);
            ValidateElementChecked(Ispersoncontractall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllPersonContracts_NoRadioButtonNotChecked()
        {
            WaitForElement(Ispersoncontractall_0);
            ValidateElementNotChecked(Ispersoncontractall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllPersonContracts_YesRadioButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(Ispersoncontractall_1);
            else
                ValidateElementDisabled(Ispersoncontractall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllPersonContracts_NoRadioButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(Ispersoncontractall_0);
            else
                ValidateElementDisabled(Ispersoncontractall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickPersonContractsLookupButton()
        {
            WaitForElementToBeClickable(PersoncontractidLookupButton);
            Click(PersoncontractidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidatePersonContractsLookupButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(PersoncontractidLookupButton);
            else
                ValidateElementDisabled(PersoncontractidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidatePersonContractsLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PersoncontractidLookupButton);
            else
                WaitForElementNotVisible(PersoncontractidLookupButton, 3);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllTimebandSets_YesRadioButton()
        {
            WaitForElementToBeClickable(Iscptimebandsetall_1);
            Click(Iscptimebandsetall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllTimebandSets_YesRadioButtonChecked()
        {
            WaitForElement(Iscptimebandsetall_1);
            ValidateElementChecked(Iscptimebandsetall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllTimebandSets_YesRadioButtonNotChecked()
        {
            WaitForElement(Iscptimebandsetall_1);
            ValidateElementNotChecked(Iscptimebandsetall_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyToAllTimebandSets_NoRadioButton()
        {
            WaitForElementToBeClickable(Iscptimebandsetall_0);
            Click(Iscptimebandsetall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllTimebandSets_NoRadioButtonChecked()
        {
            WaitForElement(Iscptimebandsetall_0);
            ValidateElementChecked(Iscptimebandsetall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyToAllTimebandSets_NoRadioButtonNotChecked()
        {
            WaitForElement(Iscptimebandsetall_0);
            ValidateElementNotChecked(Iscptimebandsetall_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickTimebandSets_SelectedOptionLink(string RecordId)
        {
            WaitForElementToBeClickable(TimebandSets_SelectedOptionLink(RecordId));
            Click(TimebandSets_SelectedOptionLink(RecordId));

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateTimebandSets_SelectedOptionLinkText(string RecordId, string ExpectedText)
        {
            WaitForElementToBeClickable(TimebandSets_SelectedOptionLink(RecordId));
            ValidateElementText(TimebandSets_SelectedOptionLink(RecordId), ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickTimebandSetsLookupButton()
        {
            WaitForElementToBeClickable(CptimebandsetidLookupButton);
            Click(CptimebandsetidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateTimebandSetsLookupButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
                ValidateElementNotDisabled(CptimebandsetidLookupButton);
            else
                ValidateElementDisabled(CptimebandsetidLookupButton);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateTimebandSetsLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CptimebandsetidLookupButton);
            else
                WaitForElementNotVisible(CptimebandsetidLookupButton, 3);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateEndDateText(string ExpectedText)
        {
            ValidateElementValue(Enddate, ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage InsertTextOnEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Enddate);
            SendKeys(Enddate, TextToInsert);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickEndDateDatePicker()
        {
            WaitForElementToBeClickable(EnddateDatePicker);
            Click(EnddateDatePicker);

            return this;
        }

        public CPMasterPayArrangementRecordPage SelectUnitType(string TextToSelect)
        {
            WaitForElementToBeClickable(Cppayrollunittypeid);
            SelectPicklistElementByText(Cppayrollunittypeid, TextToSelect);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateUnitTypeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Cppayrollunittypeid, ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyDurationFrom_YesRadioButton()
        {
            WaitForElementToBeClickable(Applyminbookinglength_1);
            Click(Applyminbookinglength_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyDurationFrom_YesRadioButtonChecked()
        {
            WaitForElement(Applyminbookinglength_1);
            ValidateElementChecked(Applyminbookinglength_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyDurationFrom_YesRadioButtonNotChecked()
        {
            WaitForElement(Applyminbookinglength_1);
            ValidateElementNotChecked(Applyminbookinglength_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyDurationFrom_NoRadioButton()
        {
            WaitForElementToBeClickable(Applyminbookinglength_0);
            Click(Applyminbookinglength_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyDurationFrom_NoRadioButtonChecked()
        {
            WaitForElement(Applyminbookinglength_0);
            ValidateElementChecked(Applyminbookinglength_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyDurationFrom_NoRadioButtonNotChecked()
        {
            WaitForElement(Applyminbookinglength_0);
            ValidateElementNotChecked(Applyminbookinglength_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyDurationTo_YesRadioButton()
        {
            WaitForElementToBeClickable(Applymaxbookinglength_1);
            Click(Applymaxbookinglength_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyDurationTo_YesRadioButtonChecked()
        {
            WaitForElement(Applymaxbookinglength_1);
            ValidateElementChecked(Applymaxbookinglength_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyDurationTo_YesRadioButtonNotChecked()
        {
            WaitForElement(Applymaxbookinglength_1);
            ValidateElementNotChecked(Applymaxbookinglength_1);

            return this;
        }

        public CPMasterPayArrangementRecordPage ClickApplyDurationTo_NoRadioButton()
        {
            WaitForElementToBeClickable(Applymaxbookinglength_0);
            Click(Applymaxbookinglength_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyDurationTo_NoRadioButtonChecked()
        {
            WaitForElement(Applymaxbookinglength_0);
            ValidateElementChecked(Applymaxbookinglength_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateApplyDurationTo_NoRadioButtonNotChecked()
        {
            WaitForElement(Applymaxbookinglength_0);
            ValidateElementNotChecked(Applymaxbookinglength_0);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateDurationFromMinutesText(string ExpectedText)
        {
            ValidateElementValue(DurationFromMinutes, ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage InsertTextOnDurationFromMinutes(string TextToInsert)
        {
            WaitForElementToBeClickable(DurationFromMinutes);
            SendKeys(DurationFromMinutes, TextToInsert + Keys.Tab);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateDurationFromMinutesErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(DurationFromMinutes_ErrorLabel);
            else
                WaitForElementNotVisible(DurationFromMinutes_ErrorLabel, 3);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateDurationFromMinutesErrorLabelText(string ExpectedText)
        {
            ValidateElementText(DurationFromMinutes_ErrorLabel, ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateDurationToMinutesText(string ExpectedText)
        {
            ValidateElementValue(DurationToMinutes, ExpectedText);

            return this;
        }

        public CPMasterPayArrangementRecordPage InsertTextOnDurationToMinutes(string TextToInsert)
        {
            WaitForElementToBeClickable(DurationToMinutes);
            SendKeys(DurationToMinutes, TextToInsert + Keys.Tab);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateDurationToMinutesErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(DurationToMinutes_ErrorLabel);
            else
                WaitForElementNotVisible(DurationToMinutes_ErrorLabel, 3);

            return this;
        }

        public CPMasterPayArrangementRecordPage ValidateDurationToMinutesErrorLabelText(string ExpectedText)
        {
            ValidateElementText(DurationToMinutes_ErrorLabel, ExpectedText);

            return this;
        }

    }
}
