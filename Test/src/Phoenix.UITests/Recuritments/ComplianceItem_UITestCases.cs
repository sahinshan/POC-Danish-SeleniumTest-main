using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// This class contains Automated UI test scripts for Compliance Item page.
    /// </summary>
    [TestClass]
    public class ComplianceItem_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _applicantId;
        private string _applicantFullName;
        private string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _userName = "Login_User_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _staffRecruitmentItemId;
        private string _staffRecruitmentItemName;
        private string applicantFirstName;
        private string applicantLastName;
        private Guid _outcomeId;
        private String CurrentDateValue = DateTime.Today.ToString("dd'/'MM'/'yyyy");

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Create default system user

                _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser(_userName, "Login_", "Automation_User", "Login Automation User", "Passw0rd_!", "Login_Automation_User@somemail.com", "Login_Automation_User@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true, 4);
                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

                #endregion

                #region Staff Recruitment Item

                _staffRecruitmentItemName = "CDV6_20840_Item_1_" + currentDateTime;
                _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-20840

        [TestProperty("JiraIssueID", "ACC-3489")]
        [Description("Test case in order to Capture the data for managing the Compliance Items for an Applicant User." +
            "Compliance Item should be created for the respective Applicant User" +
            "System should not allow user to save the record without entering data in Compliance Item field")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen", "Recruitment Documents")]
        public void ComplianceItems_UITestCases001()
        {
            #region Create Applicant 

            applicantFirstName = "CDV6_20840_" + currentDateTime;
            applicantLastName = "Applicant_01";
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_userName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .ValidateApplicantRecordIsPresent(_applicantId.ToString())
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #endregion

            #region Step 3

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.IsTrue(complianceItems.Count >= 0);

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRegardingApplicantName(_applicantFullName)
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateStatusSelectedText("Outstanding")
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .ValidateStatusSelectedText("Outstanding")
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateRegardingApplicantName(_applicantFullName);

            #endregion

            #region Step 4

            recruitmentDocumentsRecordPage
                .ClickBackButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickSaveButton()
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateComplianceItemFieldNotificationMessageText("Please fill out this field.");

            #endregion

            #region Step 5

            recruitmentDocumentsRecordPage
                .ClickRequestedDateCalendar()
                .ValidateSelectedDate(DateTime.Now.Day.ToString())
                .ValidateSelectedMonth(DateTime.Today.ToString("MMM"))
                .ValidateSelectedYear(DateTime.Today.ToString("yyyy"));

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3490")]
        [Description("Test case in order to Capture the data for managing the Compliance Items for an Applicant User." +
            "Requested By field should be set to Mandatory and gets enabled the moment user filled the Requested Date field" +
            "Alert dialog should be rendered when User selects the future Date in the Completed Date field")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen", "Recruitment Documents")]
        public void ComplianceItems_UITestCases002()
        {
            #region Step 6 and 7

            #region Create Applicant 

            applicantFirstName = "CDV6_20840_" + currentDateTime;
            applicantLastName = "Applicant_02";
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_userName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRegardingApplicantName(_applicantFullName)
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateStatusSelectedText("Outstanding")
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue(CurrentDateValue)
                .ValidateRequestedByMandatoryFieldVisibility(true)
                .ValidateRequestedByLinkText("Login_ Automation_User")
                .InsertCompletedDateValue(DateTime.Today.AddDays(1).ToString("dd'/'MM'/'yyyy"));

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Completed Date cannot be a future date")
                .TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3491")]
        [Description("Test case in order to Capture the data for managing the Compliance Items for an Applicant User." +
            "Requested By field should be set to Mandatory and gets enabled the moment user filled the Requested Date field" +
            "Completed By field should be set to Mandatory and gets enabled the moment user filled the Completed Date field ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen", "Recruitment Documents")]
        public void ComplianceItems_UITestCases003()
        {
            #region Step 8

            #region Create Applicant 

            applicantFirstName = "CDV6_20840_" + currentDateTime;
            applicantLastName = "Applicant_03";
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_userName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRegardingApplicantName(_applicantFullName)
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateStatusSelectedText("Outstanding")
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue(CurrentDateValue)
                .InsertCompletedDateValue(CurrentDateValue)
                .ValidateCompletedByMandatoryFieldVisibility(true)
                .ValidateCompletedByLinkText("Login_ Automation_User");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3492")]
        [Description("Test case in order to Capture the data for managing the Compliance Items for an Applicant User." +
            "Referee or Reference No should be mandatory and prompt the User with an Alert message" +
            "Alert dialog is rendered when User selects the Date < Requested Date in the Completed Date field" +
            "Requested By, Completed Date and Completed By field values should be cleared off when Requested Date field value is removed and focus out" +
            "Completed By should be set Mandatory and Enabled, Valid From Date and Valid To Date should be set in Enabled state when User filled the Completed Date field")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen", "Recruitment Documents")]
        public void ComplianceItems_UITestCases004()
        {
            #region Step 9 to Step 12

            #region Create Applicant 

            applicantFirstName = "CDV6_20840_" + currentDateTime;
            applicantLastName = "Applicant_04";
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_userName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRegardingApplicantName(_applicantFullName)
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateStatusSelectedText("Outstanding")
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue(CurrentDateValue)
                .InsertCompletedDateValue(CurrentDateValue)
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Reference Number or Referee Name is mandatory when Completed Date is populated. Please add Reference Number or Referee Name")
                .TapCloseButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertCompletedDateValue(DateTime.Today.AddDays(-1).ToString("dd'/'MM'/'yyyy"));

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Completed Date cannot be before Requested Date")
                .TapCloseButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue("")
                .ValidateRequestedByLinkText("")
                .ValidateCompletedDateField("")
                .ValidateCompletedDateFieldDisabled(true)
                .ValidateCompletedByLinkText("");

            recruitmentDocumentsRecordPage
                .InsertRequestedDateValue(CurrentDateValue)
                .ValidateValidFromDateFieldDisabled(true)
                .ValidateValidToDateFieldDisabled(true)
                .InsertCompletedDateValue(CurrentDateValue)
                .ValidateCompletedByMandatoryFieldVisibility(true)
                .ValidateValidFromDateFieldDisabled(false)
                .ValidateValidToDateFieldDisabled(false);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-20841

        [TestProperty("JiraIssueID", "ACC-3493")]
        [Description("Test case in order to Capture the data for managing the Compliance Items for an Applicant User." +
            "Create a Recruitment Document for an Applicant user and fill the Compliance Item from look up records, Reference Number as a single line text , Referee Name as a single line text , Referee phone as a number , Referee Email as an Email value , Referee Address as a multi-line text field and hit save Icon" +
            "A Recruitment Document with the compliance Items for the respective Applicant user should be created" +
            "Verify whether prompt Alert dialog is rendered when User entered Invalid Reference Number , Referee Name , Referee phone , Referee Email , Referee Address and hit save Icon")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen", "Recruitment Documents")]
        public void ComplianceItems_UITestCases005()
        {
            #region Create Applicant 

            applicantFirstName = "CDV6_20841_" + currentDateTime;
            applicantLastName = "Applicant_05";
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login(_userName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRegardingApplicantName(_applicantFullName)
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue(CurrentDateValue)
                .InsertCompletedDateValue(CurrentDateValue)
                .InsertReferenceNumber("RefNo 20841")
                .InsertRefereeName("Preethi")
                .InsertRefereePhone("9876544567")
                .InsertRefereeEmail("mail01@somemail.com")
                .InsertRefereeAddress("No 98, Street A, Ch-118")
                .ClickSaveButton()
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .WaitForRecordToBeSaved()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .ValidateStatusSelectedText("Completed")
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateRegardingApplicantName(_applicantFullName)
                .ValidateReferenceNumberFieldValue("RefNo 20841")
                .ValidateRefereeNameFieldValue("Preethi")
                .ValidateRefereePhoneFieldValue("9876544567")
                .ValidateRefereeEmailFieldValue("mail01@somemail.com")
                .ValidateRefereeAddressFieldValue("No 98, Street A, Ch-118");

            #endregion

            #region Step 14

            recruitmentDocumentsRecordPage
                .InsertRequestedDateValue(CurrentDateValue)
                .InsertCompletedDateValue(CurrentDateValue)
                .InsertReferenceNumber("33adsd^&***")
                .InsertRefereeName("Preethi$$%%")
                .InsertRefereePhone("12344/133adsd^&***")
                .InsertRefereeEmail("mail01@somemail")
                .InsertRefereeAddress("No:98, %%^&**")
                .ClickSaveButton()
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateRefereePhoneFieldNotificationMessageText("Please enter a valid phone number")
                .ValidateRefereeEmailFieldNotificationMessageText("Please enter a valid email");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3494")]
        [Description("Test case in order to Capture the data for managing the Compliance Items for an Applicant User." +
            "Document field value should be auto populated for the respective Applicant user compliance data available under the Compliance Management section" +
            "Chased  Date field should  highlight with the current Date in selected state" +
            "Chased By and Outcome fields should be set  Mandatory and Enabled state the moment user filled the Chased Date field" +
            "Compliance Management record should be created for the respective Applicant User with the field details")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Recruitment Documents Management")]
        public void ComplianceItems_UITestCases006()
        {
            #region Create Applicant 

            applicantFirstName = "CDV6_20841_" + currentDateTime;
            applicantLastName = "Applicant_06";
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            #region Requirement Last Chased Outcome
            _outcomeId = dbHelper.requirementLastChasedOutcome.CreateRequirementLastChasedOutcome("Outcome_20841" + currentDateTime, _careProviders_TeamId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));

            #endregion

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_userName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .WaitForApplicantsPageToLoad()
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRegardingApplicantName(_applicantFullName)
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .WaitForRecordToBeSaved()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .ValidateStatusSelectedText("Outstanding")
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateRegardingApplicantName(_applicantFullName);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsManagementAreaToLoad()
                .ClickCreateNewRecruitmentDocumentSubRecordButton();

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ValidateDocumentMandatoryFieldVisibility(true)
                .ValidateDocumentField(_staffRecruitmentItemName);

            #endregion

            #region Step 16

            recruitmentDocumentManagementRecordPage
                .ClickChasedDateCalendar()
                .ValidateSelectedDate(DateTime.Now.Day.ToString())
                .ValidateSelectedMonth(DateTime.Today.ToString("MMM"))
                .ValidateSelectedYear(DateTime.Today.ToString("yyyy"));

            #endregion

            #region Step 17

            recruitmentDocumentManagementRecordPage
                .ValidateChasedByMandatoryFieldVisibility(false)
                .ValidateOutcomeMandatoryFieldVisibility(false)
                .ValidateChasedByFieldLookupButtonDisabled(true)
                .ValidateOutcomeFieldLookupButtonDisabled(true)
                .InsertChasedDateValue(CurrentDateValue)
                .ValidateChasedByMandatoryFieldVisibility(true)
                .ValidateOutcomeMandatoryFieldVisibility(true)
                .ValidateChasedByFieldLookupButtonDisabled(false)
                .ValidateOutcomeFieldLookupButtonDisabled(false);

            #endregion

            #region Step 18

            recruitmentDocumentManagementRecordPage
                .ClickChasedByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_userName)
                .TapSearchButton()
                .ValidateResultElementPresent(_defaultLoginUserID.ToString())
                .SelectResultElement(_defaultLoginUserID.ToString());

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickOutcomeFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Outcome_20841")
                .TapSearchButton()
                .ValidateResultElementPresent(_outcomeId.ToString())
                .SelectResultElement(_outcomeId.ToString());

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .InsertNotesText("Test Notes")
                .ClickSaveButton()
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .WaitForRecruitmentDocumentManagementRecordToBeSaved()
                .ValidateDocumentField(_staffRecruitmentItemName)
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateChasedDateFieldValue(CurrentDateValue)
                .ValidateChasedByField("Login_ Automation_User")
                .ValidateOutcomeField("Outcome_20841" + currentDateTime);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3495")]
        [Description("Test case in order to Capture the data for managing the Compliance Items for an Applicant User." +
            "Verify whether Chased By, Outcome field values are cleared off when Chased Date field value is removed and focus out" +
            "Verify whether Alert dialog is rendered when user choose the Chased Date < Requested date." +
            "Verify whether Alert dialog is rendered when user enter the chased Date > completed date." +
            "Verify whether Alert dialog is rendered when user enter the future Date in Chased Date field" +
            "Verify whether status field is optional and disabled")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Recruitment Documents Management")]
        public void ComplianceItems_UITestCases007()
        {
            #region Step 19 to Step 23

            #region Create Applicant 

            applicantFirstName = "CDV6_20841_" + currentDateTime;
            applicantLastName = "Applicant_07";
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            #region Requirement Last Chased Outcome
            _outcomeId = dbHelper.requirementLastChasedOutcome.CreateRequirementLastChasedOutcome("Outcome_20841" + currentDateTime, _careProviders_TeamId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_userName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .WaitForApplicantsPageToLoad()
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRegardingApplicantName(_applicantFullName)
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue(DateTime.Today.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .InsertCompletedDateValue(DateTime.Today.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .InsertReferenceNumber("RefNo 20841")
                .ClickSaveButton()
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .WaitForRecordToBeSaved()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .ValidateStatusSelectedText("Completed")
                .ValidateRecruitmentStatus_Field_Disabled(true)
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateRegardingApplicantName(_applicantFullName);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsManagementAreaToLoad()
                .ClickCreateNewRecruitmentDocumentSubRecordButton();

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ValidateDocumentField(_staffRecruitmentItemName);

            recruitmentDocumentManagementRecordPage
                .InsertChasedDateValue(DateTime.Today.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickChasedByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_userName)
                .TapSearchButton()
                .ValidateResultElementPresent(_defaultLoginUserID.ToString())
                .SelectResultElement(_defaultLoginUserID.ToString());

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickOutcomeFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Outcome_20841" + currentDateTime)
                .TapSearchButton()
                .ValidateResultElementPresent(_outcomeId.ToString())
                .SelectResultElement(_outcomeId.ToString());

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .InsertNotesText("Test Notes")
                .ClickSaveButton()
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .WaitForRecruitmentDocumentManagementRecordToBeSaved();

            recruitmentDocumentManagementRecordPage
                .InsertChasedDateValue("")
                .ValidateChasedByField("")
                .ValidateOutcomeField("")
                .InsertChasedDateValue(DateTime.Today.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .ClickChasedByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_userName)
                .TapSearchButton()
                .ValidateResultElementPresent(_defaultLoginUserID.ToString())
                .SelectResultElement(_defaultLoginUserID.ToString());

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickOutcomeFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Outcome_20841" + currentDateTime)
                .TapSearchButton()
                .ValidateResultElementPresent(_outcomeId.ToString())
                .SelectResultElement(_outcomeId.ToString());

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .InsertNotesText("Test Notes")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Last Chased Date cannot be before Requested Date")
                .TapCloseButton();

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .InsertChasedDateValue(CurrentDateValue)
                .ClickChasedByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_userName)
                .TapSearchButton()
                .ValidateResultElementPresent(_defaultLoginUserID.ToString())
                .SelectResultElement(_defaultLoginUserID.ToString());

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickOutcomeFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Outcome_20841" + currentDateTime)
                .TapSearchButton()
                .ValidateResultElementPresent(_outcomeId.ToString())
                .SelectResultElement(_outcomeId.ToString());

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Last Chased Date cannot be after Completed Date")
                .TapCloseButton();

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .InsertChasedDateValue(DateTime.Today.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickChasedByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_userName)
                .TapSearchButton()
                .ValidateResultElementPresent(_defaultLoginUserID.ToString())
                .SelectResultElement(_defaultLoginUserID.ToString());

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickOutcomeFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Outcome_20841" + currentDateTime)
                .TapSearchButton()
                .ValidateResultElementPresent(_outcomeId.ToString())
                .SelectResultElement(_outcomeId.ToString());

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Last Chased Date cannot be a future date")
                .TapCloseButton();

            #endregion
        }

        #endregion

    }
}