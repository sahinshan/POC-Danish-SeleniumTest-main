using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonContractServiceRecordPage : CommonMethods
    {
        public PersonContractServiceRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersoncontractservice')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By PinToMeButton = By.Id("TI_PinToMeButton");
        readonly By UnpinFromMeButton = By.Id("TI_UnpinFromMeButton");
        readonly By CloneRecordButton = By.Id("TI_CloneRecordButton");
        readonly By ToolbarMenuButton = By.Id("CWToolbarMenu");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By auditSubMenuLink = By.XPath("//*[@id='CWNavItem_AuditHistory']");
        readonly By financeTransactionTriggersSubMenuLink = By.XPath("//*[@id='CWNavItem_FinanceTransactionTriggers']");


        readonly By DetailsTab = By.XPath("//*[@id='CWNavGroup_EditForm']/a");
        readonly By RatesTab = By.XPath("//*[@id='CWNavGroup_Rates']/a");
        readonly By FinanceTransactionsTab = By.Id("CWNavGroup_FinanceTransactions");
        readonly By ChargeApportionmentTab = By.Id("CWNavGroup_ChargeApportionment");

        readonly By NotificationArea = By.XPath("//*[@id='CWNotificationHolder_DataForm']");

        #region Genereal

        readonly By GeneralSectionTitle = By.XPath("//*[@id='CWSection_General']//span[text()='General']");

        readonly By Id_LabelField = By.XPath("//*[@id='CWLabelHolder_careproviderpersoncontractservicenumber']/label[text()='Id']");
        readonly By Id_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careproviderpersoncontractservicenumber']/label/span[@class='mandatory']");
        readonly By Id_Field = By.Id("CWField_careproviderpersoncontractservicenumber");

        readonly By ResponsibleTeam_LabelField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By ResponsibleTeam_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span[@class='mandatory']");
        readonly By ResponsibleTeam_LinkText = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

        readonly By PersonContract_LabelField = By.XPath("//*[@id='CWLabelHolder_careproviderpersoncontractid']/label[text()='Person Contract']");
        readonly By PersonContract_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careproviderpersoncontractid']/label/span[@class='mandatory']");
        readonly By PersonContract_LinkText = By.Id("CWField_careproviderpersoncontractid_Link");
        readonly By PersonContract_LookupButton = By.Id("CWLookupBtn_careproviderpersoncontractid");
        readonly By PersonContract_RemoveButton = By.Id("CWClearLookup_careproviderpersoncontractid");

        #endregion

        #region Service Request

        readonly By ServiceRequestSectionTitle = By.XPath("//*[@id='CWSection_ServiceRequest']//span[text()='Service Request']");

        readonly By ContractScheme_LabelField = By.XPath("//*[@id='CWLabelHolder_careprovidercontractschemeid']/label[text()='Contract Scheme']");
        readonly By ContractScheme_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careprovidercontractschemeid']/label/span[@class='mandatory']");
        readonly By ContractScheme_LinkText = By.Id("CWField_careprovidercontractschemeid_Link");
        readonly By ContractScheme_LookupButton = By.Id("CWLookupBtn_careprovidercontractschemeid");

        readonly By ServiceDetail_LabelField = By.XPath("//*[@id='CWLabelHolder_careproviderservicedetailid']/label[text()='Service Detail']");
        readonly By ServiceDetail_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careproviderservicedetailid']/label/span[@class='mandatory']");
        readonly By ServiceDetail_LinkText = By.Id("CWField_careproviderservicedetailid_Link");
        readonly By ServiceDetail_LookupButton = By.Id("CWLookupBtn_careproviderservicedetailid");
        readonly By ServiceDetail_RemoveButton = By.Id("CWClearLookup_careproviderservicedetailid");

        readonly By UpdateFinanceCode_LabelField = By.XPath("//*[@id='CWLabelHolder_isupdatefinancecode']/label[text()='Update Finance Code?']");
        readonly By UpdateFinanceCode_MandatoryField = By.XPath("//*[@id='CWLabelHolder_isupdatefinancecode']/label/span[@class='mandatory']");
        readonly By UpdateFinanceCode_YesOption = By.Id("CWField_isupdatefinancecode_1");
        readonly By UpdateFinanceCode_NoOption = By.Id("CWField_isupdatefinancecode_0");

        readonly By AccountCode_LabelField = By.XPath("//*[@id='CWLabelHolder_accountcode']/label[text()='Account Code']");
        readonly By AccountCode_MandatoryField = By.XPath("//*[@id='CWLabelHolder_accountcode']/label/span[@class='mandatory']");
        readonly By AccountCode_Field = By.Id("CWField_accountcode");
        readonly By AccountCode_FieldErrorMessage = By.XPath("//*[@id='CWControlHolder_accountcode']/label/span");

        readonly By Service_LabelField = By.XPath("//*[@id='CWLabelHolder_careproviderserviceid']/label[text()='Service']");
        readonly By Service_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careproviderserviceid']/label/span[@class='mandatory']");
        readonly By Service_LinkText = By.Id("CWField_careproviderserviceid_Link");
        readonly By Service_LookupButton = By.Id("CWLookupBtn_careproviderserviceid");
        readonly By Service_RemoveButton = By.Id("CWClearLookup_careproviderserviceid");
        readonly By Service_FieldErrorMessage = By.XPath("//*[@id='CWControlHolder_careproviderserviceid']/label/span");

        readonly By ContractService_LabelField = By.XPath("//*[@id='CWLabelHolder_careprovidercontractserviceid']/label[text()='Contract Service']");
        readonly By ContractService_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careprovidercontractserviceid']/label/span[@class='mandatory']");
        readonly By ContractService_LinkText = By.Id("CWField_careprovidercontractserviceid_Link");
        readonly By ContractService_LookupButton = By.Id("CWLookupBtn_careprovidercontractserviceid");
        readonly By ContractService_RemoveButton = By.Id("CWClearLookup_careprovidercontractserviceid");

        readonly By FinanceCode_LabelField = By.XPath("//*[@id='CWLabelHolder_financecode']/label[text()='Finance Code']");
        readonly By FinanceCode_MandatoryField = By.XPath("//*[@id='CWLabelHolder_financecode']/label/span[@class='mandatory']");
        readonly By FinanceCode_Field = By.Id("CWField_financecode");

        readonly By FinanceCode_PencliIcon = By.Id("CWFieldButton_financecode");

        #endregion

        #region Contract Request

        readonly By ContractRequestSectionTitle = By.XPath("//*[@id='CWSection_ContractRequest']//span[text()='Contract Request']");

        readonly By StartDateTime_LabelField = By.XPath("//*[@id='CWLabelHolder_startdatetime']/label[text()='Start Date/Time']");
        readonly By StartDateTime_MandatoryField = By.XPath("//*[@id='CWLabelHolder_startdatetime']/label/span[@class='mandatory']");
        readonly By StartDate_Field = By.Id("CWField_startdatetime");
        readonly By StartDate_DatePicker = By.Id("CWField_startdatetime_DatePicker");
        readonly By StartTime_Field = By.Id("CWField_startdatetime_Time");
        readonly By StartTime_TimePicker = By.Id("CWField_startdatetime_Time_TimePicker");
        readonly By StartDate_FieldErrorMessage = By.XPath("//*[@id='CWControlHolder_startdatetime']//label[@for='CWField_startdatetime']/span");
        readonly By StartTime_FieldErrorMessage = By.XPath("//*[@id='CWControlHolder_startdatetime']//label[@for='CWField_startdatetime_Time']/span");

        readonly By EndDateTime_LabelField = By.XPath("//*[@id='CWLabelHolder_enddatetime']/label[text()='End Date/Time']");
        readonly By EndDateTime_MandatoryField = By.XPath("//*[@id='CWLabelHolder_enddatetime']/label/span[@class='mandatory']");
        readonly By EndDate_Field = By.Id("CWField_enddatetime");
        readonly By EndDate_DatePicker = By.Id("CWField_enddatetime_DatePicker");
        readonly By EndTime_Field = By.Id("CWField_enddatetime_Time");
        readonly By EndTime_TimePicker = By.Id("CWField_enddatetime_Time_TimePicker");

        readonly By ExpectedEndDateTime_LabelField = By.XPath("//*[@id='CWLabelHolder_expectedenddatetime']/label[text()='Expected End Date/Time']");
        readonly By ExpectedEndDateTime_MandatoryField = By.XPath("//*[@id='CWLabelHolder_expectedenddatetime']/label/span[@class='mandatory']");
        readonly By ExpectedEndDate_Field = By.Id("CWField_expectedenddatetime");
        readonly By ExpectedEndDate_DatePicker = By.Id("CWField_expectedenddatetime_DatePicker");
        readonly By ExpectedEndTime_Field = By.Id("CWField_expectedenddatetime_Time");
        readonly By ExpectedEndTime_TimePicker = By.Id("CWField_expectedenddatetime_Time_TimePicker");

        readonly By Status_LabelField = By.XPath("//*[@id='CWLabelHolder_personcontractservicestatusid']/label[text()='Status']");
        readonly By Status_MandatoryField = By.XPath("//*[@id='CWLabelHolder_personcontractservicestatusid']/label/span[@class='mandatory']");
        readonly By Status_PickList = By.Id("CWField_personcontractservicestatusid");

        readonly By EndReason_LabelField = By.XPath("//*[@id='CWLabelHolder_careproviderpersoncontractserviceendreasonid']/label[text()='End Reason']");
        readonly By EndReason_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careproviderpersoncontractserviceendreasonid']/label/span[@class='mandatory']");
        readonly By EndReason_LinkText = By.Id("CWField_careproviderpersoncontractserviceendreasonid_Link");
        readonly By EndReason_LookupButton = By.Id("CWLookupBtn_careproviderpersoncontractserviceendreasonid");
        readonly By EndReason_RemoveButton = By.Id("CWClearLookup_careproviderpersoncontractserviceendreasonid");

        readonly By ChargePerWeek_LabelField = By.XPath("//*[@id='CWLabelHolder_chargeperweek']/label[text()='Charge Per Week']");
        readonly By ChargePerWeek_MandatoryField = By.XPath("//*[@id='CWLabelHolder_chargeperweek']/label/span[@class='mandatory']");
        readonly By ChargePerWeek_Field = By.Id("CWField_chargeperweek");

        #endregion

        #region Level Of Service

        readonly By LevelOfServiceSectionTitle = By.XPath("//*[@id='CWSection_LevelOfService']//span[text()='Level of Service']");

        readonly By RateUnit_LabelField = By.XPath("//*[@id='CWLabelHolder_careproviderrateunitid']/label[text()='Rate Unit']");
        readonly By RateUnit_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careproviderrateunitid']/label/span[@class='mandatory']");
        readonly By RateUnit_LinkText = By.Id("CWField_careproviderrateunitid_Link");
        readonly By RateUnit_LookupButton = By.Id("CWLookupBtn_careproviderrateunitid");
        readonly By RateUnit_RemoveButton = By.Id("CWClearLookup_careproviderrateunitid");
        readonly By RateUnit_FieldErrorMessage = By.XPath("//*[@id='CWControlHolder_careproviderrateunitid']/label/span");

        readonly By FrequencyInWeeks_LabelField = By.XPath("//*[@id='CWLabelHolder_frequencyinweeks']/label[text()='Frequency (in weeks)']");
        readonly By FrequencyInWeeks_MandatoryField = By.XPath("//*[@id='CWLabelHolder_frequencyinweeks']/label/span[@class='mandatory']");
        readonly By FrequencyInWeeks_Field = By.Id("CWField_frequencyinweeks");

        readonly By RateRequired_LabelField = By.XPath("//*[@id='CWLabelHolder_israterequired']/label[text()='Rate Required?']");
        readonly By RateRequired_MandatoryField = By.XPath("//*[@id='CWLabelHolder_israterequired']/label/span[@class='mandatory']");
        readonly By RateRequired_YesOption = By.Id("CWField_israterequired_1");
        readonly By RateRequired_NoOption = By.Id("CWField_israterequired_0");

        readonly By OverrideRate_LabelField = By.XPath("//*[@id='CWLabelHolder_isoverriderate']/label[text()='Override Rate?']");
        readonly By OverrideRate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_isoverriderate']/label/span[@class='mandatory']");
        readonly By OverrideRate_YesOption = By.Id("CWField_isoverriderate_1");
        readonly By OverrideRate_NoOption = By.Id("CWField_isoverriderate_0");

        readonly By SuspendDebtorInvoices_LabelField = By.XPath("//*[@id='CWLabelHolder_cpsuspenddebtorinvoices']/label[text()='Suspend Debtor Invoices?']");
        readonly By SuspendDebtorInvoices_MandatoryField = By.XPath("//*[@id='CWLabelHolder_cpsuspenddebtorinvoices']/label/span[@class='mandatory']");
        readonly By SuspendDebtorInvoices_YesOption = By.Id("CWField_cpsuspenddebtorinvoices_1");
        readonly By SuspendDebtorInvoices_NoOption = By.Id("CWField_cpsuspenddebtorinvoices_0");

        readonly By RateVerified_LabelField = By.XPath("//*[@id='CWLabelHolder_israteverified']/label[text()='Rate Verified?']");
        readonly By RateVerified_MandatoryField = By.XPath("//*[@id='CWLabelHolder_israteverified']/label/span[@class='mandatory']");
        readonly By RateVerified_YesOption = By.Id("CWField_israteverified_1");
        readonly By RateVerified_NoOption = By.Id("CWField_israteverified_0");

        readonly By SuspendDebtorInvoicesReason_LabelField = By.XPath("//*[@id='CWLabelHolder_cpsuspenddebtorinvoicesreasonid']/label[text()='Suspend Debtor Invoices Reason']");
        readonly By SuspendDebtorInvoicesReason_MandatoryField = By.XPath("//*[@id='CWLabelHolder_cpsuspenddebtorinvoicesreasonid']/label/span[@class='mandatory']");
        readonly By SuspendDebtorInvoicesReason_LinkText = By.Id("CWField_cpsuspenddebtorinvoicesreasonid_Link");
        readonly By SuspendDebtorInvoicesReason_LookupButton = By.Id("CWLookupBtn_cpsuspenddebtorinvoicesreasonid");
        readonly By SuspendDebtorInvoicesReason_RemoveButton = By.Id("CWClearLookup_cpsuspenddebtorinvoicesreasonid");

        #endregion

        #region Authorisation Details

        readonly By AuthorisationDetailsSectionTitle = By.XPath("//*[@id='CWSection_AuthorizationDetail']//span[text()='Authorisation Details']");

        readonly By CompletedBy_LabelField = By.XPath("//*[@id='CWLabelHolder_completedbysystemuserid']/label[text()='Completed By']");
        readonly By CompletedBy_MandatoryField = By.XPath("//*[@id='CWLabelHolder_completedbysystemuserid']/label/span[@class='mandatory']");
        readonly By CompletedBy_LinkText = By.Id("CWField_completedbysystemuserid_Link");
        readonly By CompletedBy_LookupButton = By.Id("CWLookupBtn_completedbysystemuserid");
        readonly By CompletedBy_RemoveButton = By.Id("CWClearLookup_completedbysystemuserid");

        readonly By CompletedDate_LabelField = By.XPath("//*[@id='CWLabelHolder_completeddate']/label[text()='Completed Date']");
        readonly By CompletedDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_completeddate']/label/span[@class='mandatory']");
        readonly By CompletedDate_Field = By.Id("CWField_completeddate");
        readonly By CompletedDate_DatePickerIcon = By.Id("CWField_completeddate_DatePicker");

        #endregion

        #region Notes

        readonly By NotesSectionTitle = By.XPath("//*[@id='CWSection_Notes']//span[text()='Notes']");
        readonly By NoteText_LabelField = By.XPath("//*[@id='CWLabelHolder_notetext']/label[text()='Note Text']");
        readonly By NoteText_MandatoryField = By.XPath("//*[@id='CWLabelHolder_notetext']/label/span[@class='mandatory']");
        readonly By NoteText_Field = By.Id("CWField_notetext");

        #endregion


        public PersonContractServiceRecordPage WaitForPersonContractServiceRecordPageToLoad(bool NavigateFromPeople = true)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            if (NavigateFromPeople)
            {
                WaitForElement(iframe_CWDialog);
                SwitchToIframe(iframe_CWDialog);
            }

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElement(pageHeader);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);

            return this;
        }

        public PersonContractServiceRecordPage WaitForPersonContractServiceRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidateAllFieldsOfGeneralSection()
        {
            WaitForElementVisible(GeneralSectionTitle);
            ScrollToElement(GeneralSectionTitle);

            WaitForElementVisible(Id_LabelField);
            WaitForElementVisible(Id_Field);
            ScrollToElement(Id_Field);

            WaitForElementVisible(ResponsibleTeam_LabelField);
            WaitForElementVisible(ResponsibleTeam_LinkText);
            WaitForElementVisible(ResponsibleTeam_LookupButton);
            ScrollToElement(ResponsibleTeam_LookupButton);

            WaitForElementVisible(PersonContract_LabelField);
            WaitForElementVisible(PersonContract_LinkText);
            WaitForElementVisible(PersonContract_LookupButton);
            ScrollToElement(PersonContract_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidateAllFieldsOfServiceRequestSection()
        {
            WaitForElementVisible(ServiceRequestSectionTitle);
            ScrollToElement(ServiceRequestSectionTitle);

            WaitForElementVisible(ContractScheme_LabelField);
            WaitForElementVisible(ContractScheme_LinkText);
            WaitForElementVisible(ContractScheme_LookupButton);
            ScrollToElement(ContractScheme_LookupButton);

            WaitForElementVisible(ServiceDetail_LabelField);
            WaitForElementVisible(ServiceDetail_LookupButton);
            ScrollToElement(ServiceDetail_LookupButton);

            WaitForElementVisible(UpdateFinanceCode_LabelField);
            WaitForElementVisible(UpdateFinanceCode_YesOption);
            WaitForElementVisible(UpdateFinanceCode_NoOption);
            ScrollToElement(UpdateFinanceCode_NoOption);

            WaitForElementVisible(AccountCode_LabelField);
            WaitForElementVisible(AccountCode_Field);
            ScrollToElement(AccountCode_Field);

            WaitForElementVisible(Service_LabelField);
            WaitForElementVisible(Service_LookupButton);
            ScrollToElement(Service_LookupButton);

            WaitForElementVisible(ContractService_LabelField);
            WaitForElementVisible(ContractService_LookupButton);
            ScrollToElement(ContractService_LookupButton);

            WaitForElementVisible(FinanceCode_LabelField);
            WaitForElementVisible(FinanceCode_Field);
            ScrollToElement(FinanceCode_Field);

            return this;
        }

        public PersonContractServiceRecordPage ValidateAllFieldsOfContractRequestSection()
        {
            WaitForElementVisible(ContractRequestSectionTitle);
            ScrollToElement(ContractRequestSectionTitle);

            WaitForElementVisible(StartDateTime_LabelField);
            WaitForElementVisible(StartDate_Field);
            ScrollToElement(StartDate_Field);
            WaitForElementVisible(StartDate_DatePicker);
            WaitForElementVisible(StartTime_Field);
            WaitForElementVisible(StartTime_TimePicker);

            WaitForElementVisible(ExpectedEndDateTime_LabelField);
            WaitForElementVisible(ExpectedEndDate_Field);
            ScrollToElement(ExpectedEndDate_Field);
            WaitForElementVisible(ExpectedEndDate_DatePicker);
            WaitForElementVisible(ExpectedEndTime_Field);
            WaitForElementVisible(ExpectedEndTime_TimePicker);

            WaitForElementVisible(Status_LabelField);
            WaitForElementVisible(Status_PickList);
            ScrollToElement(Status_PickList);

            WaitForElementVisible(EndDateTime_LabelField);
            WaitForElementVisible(EndDate_Field);
            ScrollToElement(EndDate_Field);
            WaitForElementVisible(EndDate_DatePicker);
            WaitForElementVisible(EndTime_Field);
            WaitForElementVisible(EndTime_TimePicker);

            WaitForElementVisible(ChargePerWeek_LabelField);
            WaitForElementVisible(ChargePerWeek_Field);
            ScrollToElement(ChargePerWeek_Field);

            return this;
        }

        public PersonContractServiceRecordPage ValidateAllFieldsOfLevelOfServiceSection()
        {
            WaitForElementVisible(LevelOfServiceSectionTitle);
            ScrollToElement(LevelOfServiceSectionTitle);

            WaitForElementVisible(RateUnit_LabelField);
            WaitForElementVisible(RateUnit_LookupButton);
            ScrollToElement(RateUnit_LookupButton);

            WaitForElementVisible(RateRequired_LabelField);
            ScrollToElement(RateRequired_LabelField);
            WaitForElementVisible(RateRequired_YesOption);
            WaitForElementVisible(RateRequired_NoOption);

            WaitForElementVisible(OverrideRate_LabelField);
            ScrollToElement(OverrideRate_LabelField);
            WaitForElementVisible(OverrideRate_YesOption);
            WaitForElementVisible(OverrideRate_NoOption);

            WaitForElementVisible(SuspendDebtorInvoices_LabelField);
            ScrollToElement(SuspendDebtorInvoices_LabelField);
            WaitForElementVisible(SuspendDebtorInvoices_YesOption);
            WaitForElementVisible(SuspendDebtorInvoices_NoOption);

            WaitForElementVisible(FrequencyInWeeks_LabelField);
            WaitForElementVisible(FrequencyInWeeks_Field);
            ScrollToElement(FrequencyInWeeks_Field);

            WaitForElementVisible(RateVerified_LabelField);
            ScrollToElement(RateVerified_LabelField);
            WaitForElementVisible(RateVerified_YesOption);
            WaitForElementVisible(RateVerified_NoOption);

            WaitForElementVisible(SuspendDebtorInvoicesReason_LabelField);
            WaitForElementVisible(SuspendDebtorInvoicesReason_LookupButton);
            ScrollToElement(SuspendDebtorInvoicesReason_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidateAllFieldsOfAuthorisationDetailsSection()
        {
            WaitForElementVisible(AuthorisationDetailsSectionTitle);
            ScrollToElement(AuthorisationDetailsSectionTitle);

            WaitForElementVisible(CompletedBy_LabelField);
            WaitForElementVisible(CompletedBy_LookupButton);
            ScrollToElement(CompletedBy_LookupButton);

            WaitForElementVisible(CompletedDate_LabelField);
            WaitForElementVisible(CompletedDate_Field);
            ScrollToElement(CompletedDate_Field);

            return this;
        }

        public PersonContractServiceRecordPage ValidateNoteTextSection()
        {
            WaitForElementVisible(NotesSectionTitle);
            ScrollToElement(NotesSectionTitle);

            WaitForElementVisible(NoteText_LabelField);
            WaitForElementVisible(NoteText_Field);
            ScrollToElement(NoteText_Field);

            return this;
        }

        public PersonContractServiceRecordPage ValidatePageHeaderText(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            ValidateElementText(pageHeader, "Person Contract Service:\r\n" + ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(auditSubMenuLink);
            Click(auditSubMenuLink);

            return this;
        }

        public PersonContractServiceRecordPage NavigateToFinanceTransactionTriggersPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(financeTransactionTriggersSubMenuLink);
            Click(financeTransactionTriggersSubMenuLink);

            return this;
        }

        public PersonContractServiceRecordPage ValidateRatesTabIsVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(RatesTab);
            else
                WaitForElementNotVisible(RatesTab, 5);

            return this;
        }

        public PersonContractServiceRecordPage NavigateToRatesTab()
        {
            WaitForElementToBeClickable(RatesTab);
            ScrollToElement(RatesTab);
            Click(RatesTab);

            return this;
        }

        public PersonContractServiceRecordPage ClickDetailsTab()
        {
            WaitForElementToBeClickable(DetailsTab);
            Click(DetailsTab);

            return this;
        }

        public PersonContractServiceRecordPage NavigateToFinanceTransactionsTab()
        {
            WaitForElementToBeClickable(FinanceTransactionsTab);
            ScrollToElement(FinanceTransactionsTab);
            Click(FinanceTransactionsTab);

            return this;
        }

        public PersonContractServiceRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public PersonContractServiceRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonContractServiceRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonContractServiceRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public PersonContractServiceRecordPage ClickPinToMeButton()
        {
            WaitForElementToBeClickable(PinToMeButton);
            ScrollToElement(PinToMeButton);
            Click(PinToMeButton);

            return this;
        }

        public PersonContractServiceRecordPage ClickUnpinFromMeButton()
        {
            if (GetElementVisibility(UnpinFromMeButton))
            {
                ScrollToElement(UnpinFromMeButton);
                Click(UnpinFromMeButton);
            }
            else
            {
                WaitForElementToBeClickable(ToolbarMenuButton);
                ScrollToElement(ToolbarMenuButton);
                Click(ToolbarMenuButton);

                WaitForElementToBeClickable(UnpinFromMeButton);
                ScrollToElement(UnpinFromMeButton);
                Click(UnpinFromMeButton);
            }

            return this;
        }

        public PersonContractServiceRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(ToolbarMenuButton);
            ScrollToElement(ToolbarMenuButton);
            Click(ToolbarMenuButton);

            WaitForElementToBeClickable(DeleteRecordButton);
            ScrollToElement(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public PersonContractServiceRecordPage ClickPersonContractLookupButton()
        {
            WaitForElementToBeClickable(PersonContract_LookupButton);
            ScrollToElement(PersonContract_LookupButton);
            Click(PersonContract_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ClickServiceDetailLookupButton()
        {
            WaitForElementToBeClickable(ServiceDetail_LookupButton);
            ScrollToElement(ServiceDetail_LookupButton);
            Click(ServiceDetail_LookupButton);

            return this;
        }

        //Click ServiceDetail_RemoveButton button
        public PersonContractServiceRecordPage ClickServiceDetailClearLookupButton()
        {
            WaitForElementToBeClickable(ServiceDetail_RemoveButton);
            ScrollToElement(ServiceDetail_RemoveButton);
            Click(ServiceDetail_RemoveButton);

            return this;
        }

        public PersonContractServiceRecordPage ClickServiceLookupButton()
        {
            WaitForElementToBeClickable(Service_LookupButton);
            ScrollToElement(Service_LookupButton);
            Click(Service_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ClickContractServiceLookupButton()
        {
            WaitForElementToBeClickable(ContractService_LookupButton);
            ScrollToElement(ContractService_LookupButton);
            Click(ContractService_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ClickEndReasonLookupButton()
        {
            WaitForElementToBeClickable(EndReason_LookupButton);
            ScrollToElement(EndReason_LookupButton);
            Click(EndReason_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ClickRateUnitLookupButton()
        {
            WaitForElementToBeClickable(RateUnit_LookupButton);
            ScrollToElement(RateUnit_LookupButton);
            Click(RateUnit_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(CloneRecordButton);
            WaitForElementVisible(PinToMeButton);

            return this;
        }

        public PersonContractServiceRecordPage InsertTextOnAccountCode(string TextToInsert)
        {
            WaitForElementToBeClickable(AccountCode_Field);
            SendKeys(AccountCode_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractServiceRecordPage InsertTextOnFinanceCode(string TextToInsert)
        {
            WaitForElementToBeClickable(FinanceCode_Field);
            SendKeys(FinanceCode_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractServiceRecordPage InsertStartDate(string StartDate)
        {
            WaitForElementToBeClickable(StartDate_Field);
            SendKeys(StartDate_Field, StartDate + Keys.Tab);

            return this;
        }

        public PersonContractServiceRecordPage InsertStartTime(string StartTime)
        {
            WaitForElementToBeClickable(StartTime_Field);
            SendKeys(StartTime_Field, StartTime + Keys.Tab);

            return this;
        }

        public PersonContractServiceRecordPage InsertEndDate(string EndDate)
        {
            WaitForElementToBeClickable(EndDate_Field);
            SendKeys(EndDate_Field, EndDate + Keys.Tab);

            return this;
        }

        public PersonContractServiceRecordPage InsertEndTime(string EndTime)
        {
            WaitForElementToBeClickable(EndTime_Field);
            SendKeys(EndTime_Field, EndTime + Keys.Tab);

            return this;
        }

        public PersonContractServiceRecordPage InsertExpectedEndDate(string ExpectedEndDate)
        {
            WaitForElementToBeClickable(ExpectedEndDate_Field);
            SendKeys(ExpectedEndDate_Field, ExpectedEndDate + Keys.Tab);

            return this;
        }

        public PersonContractServiceRecordPage InsertExpectedEndTime(string ExpectedEndTime)
        {
            WaitForElementToBeClickable(ExpectedEndTime_Field);
            SendKeys(ExpectedEndTime_Field, ExpectedEndTime + Keys.Tab);

            return this;
        }

        public PersonContractServiceRecordPage InsertValueInFrequencyInWeeks(string ValueToInsert)
        {
            WaitForElementToBeClickable(FrequencyInWeeks_Field);
            SendKeys(FrequencyInWeeks_Field, ValueToInsert + Keys.Tab);

            return this;
        }

        public PersonContractServiceRecordPage InsertTextOnNoteText(string TextToInsert)
        {
            WaitForElementToBeClickable(NoteText_Field);
            SendKeys(NoteText_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractServiceRecordPage ValidateNotificationAreaVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NotificationArea);
            else
                WaitForElementNotVisible(NotificationArea, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateNotificationAreaText(string ExpectedText)
        {
            ScrollToElement(NotificationArea);
            WaitForElementVisible(NotificationArea);
            ValidateElementText(NotificationArea, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateIdFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(Id_Field);
            ScrollToElement(Id_Field);
            ValidateElementValue(Id_Field, ExpectedValue);

            return this;
        }

        public PersonContractServiceRecordPage ValidateIdFieldIsBlank(bool IsBlank)
        {
            WaitForElementVisible(Id_Field);
            ScrollToElement(Id_Field);

            string actualFieldValue = GetElementByAttributeValue(Id_Field, "value");

            if (IsBlank)
                Assert.AreEqual("", actualFieldValue);
            else
                Assert.AreNotEqual("", actualFieldValue);

            return this;
        }

        public PersonContractServiceRecordPage GetIdFieldValueAndValidateTitle(string contractName)
        {
            WaitForElementVisible(Id_Field);
            ScrollToElement(Id_Field);

            string actualFieldValue = GetElementByAttributeValue(Id_Field, "value");

            WaitForElementVisible(pageHeader);
            ValidateElementText(pageHeader, "Person Contract Service:\r\n" + contractName + " \\ " + actualFieldValue);

            return this;
        }

        public PersonContractServiceRecordPage ValidatePersonContractLinkText(string ExpectedText)
        {
            if (ExpectedText != "")
            {
                WaitForElementToBeClickable(PersonContract_LinkText);
                ScrollToElement(PersonContract_LinkText);
            }

            ValidateElementText(PersonContract_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeam_LinkText);
            ScrollToElement(ResponsibleTeam_LinkText);
            ValidateElementText(ResponsibleTeam_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateContractSchemeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ContractScheme_LinkText);
            ScrollToElement(ContractScheme_LinkText);
            ValidateElementText(ContractScheme_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateServiceDetailLinkText(string ExpectedText)
        {
            if (ExpectedText != "")
            {
                WaitForElementToBeClickable(ServiceDetail_LinkText);
                ScrollToElement(ServiceDetail_LinkText);
            }

            ValidateElementText(ServiceDetail_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateAccountCodeFieldText(string ExpectedText)
        {
            ScrollToElement(AccountCode_Field);
            ValidateElementValue(AccountCode_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateMaximumLimitOfAccountCodeField(string expected)
        {
            WaitForElementVisible(AccountCode_Field);
            ValidateElementMaxLength(AccountCode_Field, expected);

            return this;
        }

        public PersonContractServiceRecordPage ValidateServiceLinkText(string ExpectedText)
        {
            if (ExpectedText != "")
            {
                WaitForElementToBeClickable(Service_LinkText);
                ScrollToElement(Service_LinkText);
            }

            ValidateElementText(Service_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateContractServiceLinkText(string ExpectedText)
        {
            if (ExpectedText != "")
            {
                WaitForElementToBeClickable(ContractService_LinkText);
                ScrollToElement(ContractService_LinkText);
            }

            ValidateElementText(ContractService_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateFinanceCodeFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(FinanceCode_Field);
            ValidateElementValue(FinanceCode_Field, ExpectedValue);

            return this;
        }

        public PersonContractServiceRecordPage ValidateStartDateFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(StartDate_Field);
            ScrollToElement(StartDate_Field);
            ValidateElementText(StartDate_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateStartTimeFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(StartTime_Field);
            ScrollToElement(StartTime_Field);
            ValidateElementText(StartTime_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateEndDateFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(EndDate_Field);
            ScrollToElement(EndDate_Field);
            ValidateElementText(EndDate_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateEndTimeFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(EndTime_Field);
            ScrollToElement(EndTime_Field);
            ValidateElementText(EndTime_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateExpectedEndDateFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(ExpectedEndDate_Field);
            ScrollToElement(ExpectedEndDate_Field);
            ValidateElementText(ExpectedEndDate_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateEndReasonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(EndReason_LinkText);
            ScrollToElement(EndReason_LinkText);
            ValidateElementText(EndReason_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateExpectedEndTimeFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(ExpectedEndTime_Field);
            ScrollToElement(ExpectedEndTime_Field);
            ValidateElementText(ExpectedEndTime_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateChargePerWeekFieldText(string ExpectedText)
        {
            ScrollToElement(ChargePerWeek_Field);
            ValidateElementValue(ChargePerWeek_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateRateUnitLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(RateUnit_LinkText);
            ScrollToElement(RateUnit_LinkText);
            ValidateElementText(RateUnit_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateFrequencyInWeeksFieldValue(string ExpectedValue)
        {
            WaitForElementVisible(FrequencyInWeeks_Field);
            ScrollToElement(FrequencyInWeeks_Field);
            ValidateElementValue(FrequencyInWeeks_Field, ExpectedValue);

            return this;
        }

        public PersonContractServiceRecordPage ValidateSuspendDebtorInvoicesReasonLinkText(string ExpectedText)
        {
            if (ExpectedText != "")
            {
                WaitForElementToBeClickable(SuspendDebtorInvoicesReason_LinkText);
                ScrollToElement(SuspendDebtorInvoicesReason_LinkText);
            }

            ValidateElementText(SuspendDebtorInvoicesReason_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateCompletedByLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CompletedBy_LinkText);
            ScrollToElement(CompletedBy_LinkText);
            ValidateElementText(CompletedBy_LinkText, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateCompletedDateFieldText(string ExpectedText)
        {
            WaitForElementToBeClickable(CompletedDate_Field);
            ScrollToElement(CompletedDate_Field);
            ValidateElementText(CompletedDate_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateNoteTextText(string ExpectedText)
        {
            ValidateElementText(NoteText_Field, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateIdMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Id_LabelField);
            ScrollToElement(Id_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Id_MandatoryField);
            else
                WaitForElementNotVisible(Id_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidatePersonContractMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(PersonContract_LabelField);
            ScrollToElement(PersonContract_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(PersonContract_MandatoryField);
            else
                WaitForElementNotVisible(PersonContract_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateResponsibleTeamMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ResponsibleTeam_LabelField);
            ScrollToElement(ResponsibleTeam_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_MandatoryField);
            else
                WaitForElementNotVisible(ResponsibleTeam_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateContractSchemeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ContractScheme_LabelField);
            ScrollToElement(ContractScheme_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ContractScheme_MandatoryField);
            else
                WaitForElementNotVisible(ContractScheme_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateServiceDetailMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ServiceDetail_LabelField);
            ScrollToElement(ServiceDetail_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ServiceDetail_MandatoryField);
            else
                WaitForElementNotVisible(ServiceDetail_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateUpdateFinanceCodeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(UpdateFinanceCode_LabelField);
            ScrollToElement(UpdateFinanceCode_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(UpdateFinanceCode_MandatoryField);
            else
                WaitForElementNotVisible(UpdateFinanceCode_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateAccountCodeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(AccountCode_LabelField);
            ScrollToElement(AccountCode_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(AccountCode_MandatoryField);
            else
                WaitForElementNotVisible(AccountCode_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateServiceMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Service_LabelField);
            ScrollToElement(Service_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Service_MandatoryField);
            else
                WaitForElementNotVisible(Service_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateContractServiceMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ContractService_LabelField);
            ScrollToElement(ContractService_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ContractService_MandatoryField);
            else
                WaitForElementNotVisible(ContractService_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateFinanceCodeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(FinanceCode_LabelField);
            ScrollToElement(FinanceCode_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(FinanceCode_MandatoryField);
            else
                WaitForElementNotVisible(FinanceCode_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateStartDateTimeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(StartDateTime_LabelField);
            ScrollToElement(StartDateTime_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(StartDateTime_MandatoryField);
            else
                WaitForElementNotVisible(StartDateTime_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateExpectedEndDateTimeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ExpectedEndDateTime_LabelField);
            ScrollToElement(ExpectedEndDateTime_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ExpectedEndDateTime_MandatoryField);
            else
                WaitForElementNotVisible(ExpectedEndDateTime_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateStatusMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Status_LabelField);
            ScrollToElement(Status_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Status_MandatoryField);
            else
                WaitForElementNotVisible(Status_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateEndDateTimeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(EndDateTime_LabelField);
            ScrollToElement(EndDateTime_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(EndDateTime_MandatoryField);
            else
                WaitForElementNotVisible(EndDateTime_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateChargePerWeekMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ChargePerWeek_LabelField);
            ScrollToElement(ChargePerWeek_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ChargePerWeek_MandatoryField);
            else
                WaitForElementNotVisible(ChargePerWeek_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateRateUnitMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(RateUnit_LabelField);
            ScrollToElement(RateUnit_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(RateUnit_MandatoryField);
            else
                WaitForElementNotVisible(RateUnit_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateRateRequiredMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(RateRequired_LabelField);
            ScrollToElement(RateRequired_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(RateRequired_MandatoryField);
            else
                WaitForElementNotVisible(RateRequired_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateOverrideRateMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(OverrideRate_LabelField);
            ScrollToElement(OverrideRate_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(OverrideRate_MandatoryField);
            else
                WaitForElementNotVisible(OverrideRate_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateSuspendDebtorInvoicesMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(SuspendDebtorInvoices_LabelField);
            ScrollToElement(SuspendDebtorInvoices_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(SuspendDebtorInvoices_MandatoryField);
            else
                WaitForElementNotVisible(SuspendDebtorInvoices_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateFrequencyInWeeksMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(FrequencyInWeeks_LabelField);
            ScrollToElement(FrequencyInWeeks_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(FrequencyInWeeks_MandatoryField);
            else
                WaitForElementNotVisible(FrequencyInWeeks_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateRateVerifiedMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(RateVerified_LabelField);
            ScrollToElement(RateVerified_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(RateVerified_MandatoryField);
            else
                WaitForElementNotVisible(RateVerified_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateSuspendDebtorInvoicesReasonMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(SuspendDebtorInvoicesReason_LabelField);
            ScrollToElement(SuspendDebtorInvoicesReason_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(SuspendDebtorInvoicesReason_MandatoryField);
            else
                WaitForElementNotVisible(SuspendDebtorInvoicesReason_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateCompletedByMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(CompletedBy_LabelField);
            ScrollToElement(CompletedBy_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(CompletedBy_MandatoryField);
            else
                WaitForElementNotVisible(CompletedBy_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateCompletedDateMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(CompletedDate_LabelField);
            ScrollToElement(CompletedDate_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(CompletedDate_MandatoryField);
            else
                WaitForElementNotVisible(CompletedDate_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateNoteTextMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(NoteText_LabelField);
            ScrollToElement(NoteText_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(NoteText_MandatoryField);
            else
                WaitForElementNotVisible(NoteText_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateEndReasonMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(EndReason_LabelField);
            ScrollToElement(EndReason_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(EndReason_MandatoryField);
            else
                WaitForElementNotVisible(EndReason_MandatoryField, 3);

            return this;
        }

        public PersonContractServiceRecordPage ValidateEndReasonFieldIsVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(EndReason_LabelField);
                WaitForElementVisible(EndReason_LookupButton);
                ScrollToElement(EndReason_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(EndReason_LabelField, 3);
                WaitForElementNotVisible(EndReason_LookupButton, 3);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateDeleteRecordButtonVisibility(bool ExpectVisible)
        {
            WaitForElementToBeClickable(ToolbarMenuButton);
            ScrollToElement(ToolbarMenuButton);
            Click(ToolbarMenuButton);

            if (ExpectVisible)
                WaitForElementVisible(DeleteRecordButton);
            else
                WaitForElementNotVisible(DeleteRecordButton, 3);

            Click(ToolbarMenuButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidateAllMandatoryFieldVisibility()
        {
            ValidateIdMandatoryFieldVisibility(true);
            ValidatePersonContractMandatoryFieldVisibility(true);
            ValidateResponsibleTeamMandatoryFieldVisibility(true);

            ValidateContractSchemeMandatoryFieldVisibility(true);
            ValidateServiceMandatoryFieldVisibility(true);

            ValidateStartDateTimeMandatoryFieldVisibility(true);
            ValidateStatusMandatoryFieldVisibility(true);

            ValidateRateUnitMandatoryFieldVisibility(true);
            ValidateRateRequiredMandatoryFieldVisibility(true);
            ValidateSuspendDebtorInvoicesMandatoryFieldVisibility(true);
            ValidateFrequencyInWeeksMandatoryFieldVisibility(true);

            return this;
        }

        public PersonContractServiceRecordPage ValidateAllNonMandatoryFieldVisibility()
        {
            ValidateServiceDetailMandatoryFieldVisibility(false);
            ValidateAccountCodeMandatoryFieldVisibility(false);
            ValidateContractServiceMandatoryFieldVisibility(false);
            ValidateFinanceCodeMandatoryFieldVisibility(false);
            ValidateUpdateFinanceCodeMandatoryFieldVisibility(false);

            ValidateExpectedEndDateTimeMandatoryFieldVisibility(false);
            ValidateEndDateTimeMandatoryFieldVisibility(false);
            ValidateChargePerWeekMandatoryFieldVisibility(false);

            ValidateOverrideRateMandatoryFieldVisibility(false);
            ValidateRateVerifiedMandatoryFieldVisibility(false);
            ValidateSuspendDebtorInvoicesReasonMandatoryFieldVisibility(false);

            ValidateCompletedByMandatoryFieldVisibility(false);
            ValidateCompletedDateMandatoryFieldVisibility(false);

            ValidateNoteTextMandatoryFieldVisibility(false);

            return this;
        }

        public PersonContractServiceRecordPage ValidateIdFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Id_Field);
            ScrollToElement(Id_Field);
            if (IsDisabled)
                ValidateElementDisabled(Id_Field);
            else
                ValidateElementNotDisabled(Id_Field);

            return this;
        }

        public PersonContractServiceRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ResponsibleTeam_LookupButton);
            ScrollToElement(ResponsibleTeam_LookupButton);

            if (IsDisabled)
                ValidateElementDisabled(ResponsibleTeam_LookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeam_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidatePersonContractLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(PersonContract_LookupButton);
            ScrollToElement(PersonContract_LookupButton);

            if (IsDisabled)
                ValidateElementDisabled(PersonContract_LookupButton);
            else
                ValidateElementNotDisabled(PersonContract_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidateContractSchemeLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ContractScheme_LookupButton);
            ScrollToElement(ContractScheme_LookupButton);

            if (IsDisabled)
                ValidateElementDisabled(ContractScheme_LookupButton);
            else
                ValidateElementNotDisabled(ContractScheme_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidateUpdateFinanceCodeOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(UpdateFinanceCode_YesOption);
            WaitForElementVisible(UpdateFinanceCode_NoOption);
            ScrollToElement(UpdateFinanceCode_NoOption);

            if (IsDisabled)
            {
                ValidateElementDisabled(UpdateFinanceCode_YesOption);
                ValidateElementDisabled(UpdateFinanceCode_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(UpdateFinanceCode_YesOption);
                ValidateElementNotDisabled(UpdateFinanceCode_NoOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateServiceLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Service_LookupButton);
            ScrollToElement(Service_LookupButton);

            if (IsDisabled)
                ValidateElementDisabled(Service_LookupButton);
            else
                ValidateElementNotDisabled(Service_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidateServiceDetailLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ServiceDetail_LookupButton);
            ScrollToElement(ServiceDetail_LookupButton);

            if (IsDisabled)
                ValidateElementDisabled(ServiceDetail_LookupButton);
            else
                ValidateElementNotDisabled(ServiceDetail_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidateContractServiceLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ContractService_LookupButton);
            ScrollToElement(ContractService_LookupButton);

            if (IsDisabled)
                ValidateElementDisabled(ContractService_LookupButton);
            else
                ValidateElementNotDisabled(ContractService_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidateRateUnitLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(RateUnit_LookupButton);
            ScrollToElement(RateUnit_LookupButton);

            if (IsDisabled)
                ValidateElementDisabled(RateUnit_LookupButton);
            else
                ValidateElementNotDisabled(RateUnit_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidateFinanceCodeFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(FinanceCode_Field);
            ScrollToElement(FinanceCode_Field);

            if (IsDisabled)
                ValidateElementDisabled(FinanceCode_Field);
            else
                ValidateElementNotDisabled(FinanceCode_Field);

            return this;
        }

        public PersonContractServiceRecordPage ValidateAccountCodeFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(AccountCode_Field);
            ScrollToElement(AccountCode_Field);

            if (IsDisabled)
                ValidateElementDisabled(AccountCode_Field);
            else
                ValidateElementNotDisabled(AccountCode_Field);

            return this;
        }

        public PersonContractServiceRecordPage ValidateStartDateTimeFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(StartDate_Field);
            WaitForElementVisible(StartTime_Field);
            ScrollToElement(StartDate_Field);

            if (IsDisabled)
            {
                ValidateElementDisabled(StartDate_Field);
                ValidateElementDisabled(StartTime_Field);
            }
            else
            {
                ValidateElementNotDisabled(StartDate_Field);
                ValidateElementNotDisabled(StartTime_Field);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateEndDateTimeFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(EndDate_Field);
            WaitForElementVisible(EndTime_Field);
            ScrollToElement(EndDate_Field);

            if (IsDisabled)
            {
                ValidateElementDisabled(EndDate_Field);
                ValidateElementDisabled(EndTime_Field);
            }
            else
            {
                ValidateElementNotDisabled(EndDate_Field);
                ValidateElementNotDisabled(EndTime_Field);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateExpectedEndDateTimeFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ExpectedEndDate_Field);
            WaitForElementVisible(ExpectedEndTime_Field);
            ScrollToElement(ExpectedEndDate_Field);

            if (IsDisabled)
            {
                ValidateElementDisabled(ExpectedEndDate_Field);
                ValidateElementDisabled(ExpectedEndTime_Field);
            }
            else
            {
                ValidateElementNotDisabled(ExpectedEndDate_Field);
                ValidateElementNotDisabled(ExpectedEndTime_Field);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateStatusPicklistIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(Status_PickList);
            ScrollToElement(Status_PickList);

            if (IsDisabled)
                ValidateElementDisabled(Status_PickList);
            else
                ValidateElementNotDisabled(Status_PickList);

            return this;
        }

        public PersonContractServiceRecordPage ValidateChargePerWeekFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(ChargePerWeek_Field);
            ScrollToElement(ChargePerWeek_Field);

            if (IsDisabled)
                ValidateElementDisabled(ChargePerWeek_Field);
            else
                ValidateElementNotDisabled(ChargePerWeek_Field);

            return this;
        }

        public PersonContractServiceRecordPage ValidateFrequencyInWeeksFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(FrequencyInWeeks_Field);
            ScrollToElement(FrequencyInWeeks_Field);

            if (IsDisabled)
                ValidateElementDisabled(FrequencyInWeeks_Field);
            else
                ValidateElementNotDisabled(FrequencyInWeeks_Field);

            return this;
        }

        public PersonContractServiceRecordPage ValidateRateRequiredOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(RateRequired_YesOption);
            WaitForElementVisible(RateRequired_NoOption);
            ScrollToElement(RateRequired_NoOption);

            if (IsDisabled)
            {
                ValidateElementDisabled(RateRequired_YesOption);
                ValidateElementDisabled(RateRequired_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(RateRequired_YesOption);
                ValidateElementNotDisabled(RateRequired_NoOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateOverrideRateOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(OverrideRate_YesOption);
            WaitForElementVisible(OverrideRate_NoOption);
            ScrollToElement(OverrideRate_NoOption);

            if (IsDisabled)
            {
                ValidateElementDisabled(OverrideRate_YesOption);
                ValidateElementDisabled(OverrideRate_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(OverrideRate_YesOption);
                ValidateElementNotDisabled(OverrideRate_NoOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateRateVerifiedOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(RateVerified_YesOption);
            WaitForElementVisible(RateVerified_NoOption);
            ScrollToElement(RateVerified_NoOption);

            if (IsDisabled)
            {
                ValidateElementDisabled(RateVerified_YesOption);
                ValidateElementDisabled(RateVerified_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(RateVerified_YesOption);
                ValidateElementNotDisabled(RateVerified_NoOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateSuspendDebtorInvoicesOptionsIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(SuspendDebtorInvoices_YesOption);
            WaitForElementVisible(SuspendDebtorInvoices_NoOption);
            ScrollToElement(SuspendDebtorInvoices_NoOption);

            if (IsDisabled)
            {
                ValidateElementDisabled(SuspendDebtorInvoices_YesOption);
                ValidateElementDisabled(SuspendDebtorInvoices_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(SuspendDebtorInvoices_YesOption);
                ValidateElementNotDisabled(SuspendDebtorInvoices_NoOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateSuspendDebtorInvoicesReasonLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(SuspendDebtorInvoicesReason_LookupButton);
            ScrollToElement(SuspendDebtorInvoicesReason_LookupButton);

            if (IsDisabled)
                ValidateElementDisabled(SuspendDebtorInvoicesReason_LookupButton);
            else
                ValidateElementNotDisabled(SuspendDebtorInvoicesReason_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidateCompletedByLookupButtonIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(CompletedBy_LookupButton);
            ScrollToElement(CompletedBy_LookupButton);

            if (IsDisabled)
                ValidateElementDisabled(CompletedBy_LookupButton);
            else
                ValidateElementNotDisabled(CompletedBy_LookupButton);

            return this;
        }

        public PersonContractServiceRecordPage ValidateCompletedDateFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(CompletedDate_Field);
            WaitForElementVisible(CompletedDate_DatePickerIcon);
            ScrollToElement(CompletedDate_Field);

            if (IsDisabled)
            {
                ValidateElementDisabled(CompletedDate_Field);
                ValidateElementDisabled(CompletedDate_DatePickerIcon);
            }
            else
            {
                ValidateElementNotDisabled(CompletedDate_Field);
                ValidateElementNotDisabled(CompletedDate_DatePickerIcon);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateNoteTextFieldIsDisabled(bool IsDisabled)
        {
            WaitForElementVisible(NoteText_Field);
            ScrollToElement(NoteText_Field);

            if (IsDisabled)
                ValidateElementDisabled(NoteText_Field);
            else
                ValidateElementNotDisabled(NoteText_Field);

            return this;
        }

        public PersonContractServiceRecordPage ValidateMaximumLimitOfNoteText(string expected)
        {
            WaitForElementVisible(NoteText_Field);
            ValidateElementMaxLength(NoteText_Field, expected);

            return this;
        }

        public PersonContractServiceRecordPage ValidateRangeOfFrequencyInWeeks(string expectedRange)
        {
            WaitForElementVisible(FrequencyInWeeks_Field);
            ValidateElementAttribute(FrequencyInWeeks_Field, "range", expectedRange);

            return this;
        }

        public PersonContractServiceRecordPage ValidateFrequencyInWeeksFieldIsNumeric()
        {
            WaitForElementVisible(FrequencyInWeeks_Field);
            ValidateElementAttribute(FrequencyInWeeks_Field, "validatenumeric", "ValidateNumeric");

            return this;
        }

        public PersonContractServiceRecordPage ValidateUpdateFinanceCode_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(UpdateFinanceCode_YesOption);
            WaitForElementVisible(UpdateFinanceCode_NoOption);
            ScrollToElement(UpdateFinanceCode_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(UpdateFinanceCode_YesOption);
                ValidateElementNotChecked(UpdateFinanceCode_NoOption);
            }
            else
            {
                ValidateElementChecked(UpdateFinanceCode_NoOption);
                ValidateElementNotChecked(UpdateFinanceCode_YesOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateRateRequired_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(RateRequired_YesOption);
            WaitForElementVisible(RateRequired_NoOption);
            ScrollToElement(RateRequired_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(RateRequired_YesOption);
                ValidateElementNotChecked(RateRequired_NoOption);
            }
            else
            {
                ValidateElementChecked(RateRequired_NoOption);
                ValidateElementNotChecked(RateRequired_YesOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateOverrideRate_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(OverrideRate_YesOption);
            WaitForElementVisible(OverrideRate_NoOption);
            ScrollToElement(OverrideRate_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(OverrideRate_YesOption);
                ValidateElementNotChecked(OverrideRate_NoOption);
            }
            else
            {
                ValidateElementChecked(OverrideRate_NoOption);
                ValidateElementNotChecked(OverrideRate_YesOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateSuspendDebtorInvoices_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(SuspendDebtorInvoices_YesOption);
            WaitForElementVisible(SuspendDebtorInvoices_NoOption);
            ScrollToElement(SuspendDebtorInvoices_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(SuspendDebtorInvoices_YesOption);
                ValidateElementNotChecked(SuspendDebtorInvoices_NoOption);
            }
            else
            {
                ValidateElementChecked(SuspendDebtorInvoices_NoOption);
                ValidateElementNotChecked(SuspendDebtorInvoices_YesOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ValidateRateVerified_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(RateVerified_YesOption);
            WaitForElementVisible(RateVerified_NoOption);
            ScrollToElement(RateVerified_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(RateVerified_YesOption);
                ValidateElementNotChecked(RateVerified_NoOption);
            }
            else
            {
                ValidateElementChecked(RateVerified_NoOption);
                ValidateElementNotChecked(RateVerified_YesOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ClickUpdateFinanceCode_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(UpdateFinanceCode_YesOption);
                ScrollToElement(UpdateFinanceCode_YesOption);
                Click(UpdateFinanceCode_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(UpdateFinanceCode_NoOption);
                ScrollToElement(UpdateFinanceCode_NoOption);
                Click(UpdateFinanceCode_NoOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ClickRateRequired_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(RateRequired_YesOption);
                ScrollToElement(RateRequired_YesOption);
                Click(RateRequired_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(RateRequired_NoOption);
                ScrollToElement(RateRequired_NoOption);
                Click(RateRequired_NoOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ClickOverrideRate_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(OverrideRate_YesOption);
                //ScrollToElement(OverrideRate_YesOption);
                Click(OverrideRate_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(OverrideRate_NoOption);
                //ScrollToElement(OverrideRate_NoOption);
                Click(OverrideRate_NoOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ClickSuspendDebtorInvoices_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(SuspendDebtorInvoices_YesOption);
                ScrollToElement(SuspendDebtorInvoices_YesOption);
                Click(SuspendDebtorInvoices_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(SuspendDebtorInvoices_NoOption);
                ScrollToElement(SuspendDebtorInvoices_NoOption);
                Click(SuspendDebtorInvoices_NoOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage ClickRateVerified_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(RateVerified_YesOption);
                ScrollToElement(RateVerified_YesOption);
                Click(RateVerified_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(RateVerified_NoOption);
                ScrollToElement(RateVerified_NoOption);
                Click(RateVerified_NoOption);
            }

            return this;
        }

        public PersonContractServiceRecordPage SelectStatus(string TextToSelect)
        {
            WaitForElementToBeClickable(Status_PickList);
            ScrollToElement(Status_PickList);
            SelectPicklistElementByText(Status_PickList, TextToSelect);

            return this;
        }

        public PersonContractServiceRecordPage ValidateStatusPickListValues(string expectedText)
        {
            WaitForElementVisible(Status_PickList);
            ScrollToElement(Status_PickList);
            ValidatePicklistContainsElementByText(Status_PickList, expectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateStatusPicklist_FieldOptionIsPresent(string OptionName)
        {
            WaitForElementVisible(Status_PickList);
            ValidatePicklistContainsElementByText(Status_PickList, OptionName);

            return this;
        }

        public PersonContractServiceRecordPage ValidateStatusPickList_OptionDisabled(string OptionText, bool ExpectDisabled)
        {
            WaitForElementVisible(Status_PickList);
            ValidatePicklistOptionDisabled(Status_PickList, OptionText, ExpectDisabled);

            return this;
        }

        public PersonContractServiceRecordPage ValidateSelectedStatusPickListValue(string expectedText)
        {
            WaitForElementVisible(Status_PickList);
            ScrollToElement(Status_PickList);
            ValidatePicklistSelectedText(Status_PickList, expectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateExpectedEndDateTimeFieldLabelToolTip(string ExpectedTooltip)
        {
            WaitForElementVisible(ExpectedEndDateTime_LabelField);
            ScrollToElement(ExpectedEndDateTime_LabelField);
            MouseHover(ExpectedEndDateTime_LabelField);
            ValidateElementAttribute(ExpectedEndDateTime_LabelField, "title", ExpectedTooltip);

            return this;
        }

        public PersonContractServiceRecordPage ClickServiceRemoveButton()
        {
            WaitForElementToBeClickable(Service_RemoveButton);
            ScrollToElement(Service_RemoveButton);
            Click(Service_RemoveButton);

            return this;
        }

        public PersonContractServiceRecordPage ClearEndDate()
        {
            Click(EndDate_Field);
            ClearText(EndDate_Field);

            SendKeysWithoutClearing(EndDate_Field, Keys.Tab);

            return this;
        }

        public PersonContractServiceRecordPage ValidateServiceFieldErrorMessageText(string ExpectedText)
        {
            WaitForElement(Service_FieldErrorMessage);
            ScrollToElement(Service_FieldErrorMessage);
            ValidateElementText(Service_FieldErrorMessage, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateStartDateFieldErrorMessageText(string ExpectedText)
        {
            WaitForElement(StartDate_FieldErrorMessage);
            ScrollToElement(StartDate_FieldErrorMessage);
            ValidateElementText(StartDate_FieldErrorMessage, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateStartTimeFieldErrorMessageText(string ExpectedText)
        {
            WaitForElement(StartTime_FieldErrorMessage);
            ScrollToElement(StartTime_FieldErrorMessage);
            ValidateElementText(StartTime_FieldErrorMessage, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateRateUnitFieldErrorMessageText(string ExpectedText)
        {
            WaitForElement(RateUnit_FieldErrorMessage);
            ScrollToElement(RateUnit_FieldErrorMessage);
            ValidateElementText(RateUnit_FieldErrorMessage, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidateAccountCodeErrorMessageText(string ExpectedText)
        {
            WaitForElement(AccountCode_FieldErrorMessage);
            ScrollToElement(AccountCode_FieldErrorMessage);
            ValidateElementText(AccountCode_FieldErrorMessage, ExpectedText);

            return this;
        }

        public PersonContractServiceRecordPage ValidatePersonContractRemoveButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PersonContract_RemoveButton);
            else
                WaitForElementNotVisible(PersonContract_RemoveButton, 3);

            return this;
        }

        public PersonContractServiceRecordPage ClickFinanceCodePencilIcon()
        {
            WaitForElementToBeClickable(FinanceCode_PencliIcon);
            Click(FinanceCode_PencliIcon);

            return this;
        }

        public PersonContractServiceRecordPage NavigateToChargeApportionmentsTab()
        {
            WaitForElementToBeClickable(ChargeApportionmentTab);
            ScrollToElement(ChargeApportionmentTab);
            Click(ChargeApportionmentTab);

            return this;
        }

        public PersonContractServiceRecordPage ValidateChargeApportionmentsTabIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ChargeApportionmentTab);
            else
                WaitForElementNotVisible(ChargeApportionmentTab, 3);

            return this;
        }

    }
}
