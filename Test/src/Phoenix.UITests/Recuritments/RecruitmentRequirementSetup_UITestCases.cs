using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// This class contains Automated UI test scripts for Recruitment Requirement Setup Record
    /// </summary>
    [TestClass]
    public class RecruitmentRequirementSetup_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _systemUserId;
        private string _systemUserName;
        private string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _staffRecruitmentItemId;
        private Guid _staffRecruitmentItem2Id;
        private Guid _staffRecruitmentItem3Id;
        private string _staffRecruitmentItemName;
        private string _staffRecruitmentItem2Name;
        private string _staffRecruitmentItem3Name;
        private Guid _careProviderStaffRoleType1Id;
        private string _careProviderStaffRoleType1Name;
        private Guid _careProviderStaffRoleType2Id;
        private string _careProviderStaffRoleType2Name;
        private Guid _careProviderStaffRoleType3Id;
        private string _careProviderStaffRoleType3Name;
        private Guid _applicantId;

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

                #region System User

                _systemUserName = "RecruitmentRequirementSetupUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "RecruitmentRequirementSetup", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _systemUserId);

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-23505

        [TestProperty("JiraIssueID", "ACC-3503")]
        [Description("Steps 1 to 6 from the original jira test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirementSetup_UITestCases001()
        {
            #region Staff Recruitment Item

            _staffRecruitmentItemName = "CDV6_23505_Item_" + currentDateTime;
            _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            _careProviderStaffRoleType1Name = "CDV6_23505_Role_1_" + currentDateTime;
            _careProviderStaffRoleType2Name = "CDV6_23505_Role_2_" + currentDateTime;

            _careProviderStaffRoleType1Id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType1Name, null, null, new DateTime(2020, 1, 1), null);
            _careProviderStaffRoleType2Id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType2Name, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Step 1

            String startDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10).ToString("dd'/'MM'/'yyyy");
            String endDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2).ToString("dd'/'MM'/'yyyy");

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickSaveButton();

            recruitmentRequirementRecordPage
                .ValidateRequirementNameFieldErrorLabelText("Please fill out this field.")
                .ValidatRequiredItemTypeFieldErrorLabelText("Please fill out this field.")
                .ValidatStatusForInductionFieldErrorLabelText("Please fill out this field.")
                .ValidatStatusForAcceptanceFieldErrorLabelText("Please fill out this field.");

            #endregion

            #region Step 2

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertStartDate(startDate)
                .InsertEndDate(startDate);

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateAllRolesYesRadionButtonChecked(true);

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .SelectStatusForInduction("Completed")
                .SelectStatusForAcceptance("Completed")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateRequirementNameValue(_staffRecruitmentItemName)
                .ValidateRequiredItemTypeLinkText(_staffRecruitmentItemName)
                .ValidateStartDateValue(startDate)
                .ValidateResponsibleTeamLinkText("CareProviders")
                .ValidateEndDateValue(startDate)
                .ValidateAllRolesYesRadionButtonChecked(true)
                .ValidateNoRequiredForInductionValue("1")
                .ValidateNoRequiredForAcceptanceValue("1")
                .ValidateStatusForInductionSelectedText("Completed")
                .ValidateStatusForAcceptanceSelectedText("Completed");

            #endregion

            #region Step 3

            recruitmentRequirementRecordPage
                .ClickBackButton();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertStartDate(startDate)
                .InsertEndDate(endDate);

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickAllRolesNoRadioButton()
                .ValidateRolesLookupButtonVisibility(true)
                .ClickRolesLookUpButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_careProviderStaffRoleType1Name)
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleType1Id.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .SelectStatusForInduction("Completed")
                .SelectStatusForAcceptance("Completed")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateRequirementNameValue(_staffRecruitmentItemName)
                .ValidateRequiredItemTypeLinkText(_staffRecruitmentItemName)
                .ValidateStartDateValue(startDate)
                .ValidateResponsibleTeamLinkText("CareProviders")
                .ValidateEndDateValue(endDate)
                .ValidateAllRolesYesRadionButtonChecked(false)
                .ValidateAllRolesNoRadionButtonChecked(true)
                .ValidateRolesMultiSelectElementText(_careProviderStaffRoleType1Id, _careProviderStaffRoleType1Name)
                .ValidateNoRequiredForInductionValue("1")
                .ValidateNoRequiredForAcceptanceValue("1")
                .ValidateStatusForInductionSelectedText("Completed")
                .ValidateStatusForAcceptanceSelectedText("Completed");

            #endregion

            #region Step 4 & 5

            //step 4 same as step 3, step 5 is not applicable, BU filed is not available

            #endregion

            #region Step 6

            recruitmentRequirementRecordPage
                .ClickBackButton();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertStartDate(startDate)
                .InsertEndDate(endDate);

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickAllRolesNoRadioButton()
                .ClickRolesLookUpButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_careProviderStaffRoleType2Name)
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleType2Id.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .SelectStatusForInduction("Completed")
                .SelectStatusForAcceptance("Completed")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateRequirementNameValue(_staffRecruitmentItemName)
                .ValidateRequiredItemTypeLinkText(_staffRecruitmentItemName)
                .ValidateStartDateValue(startDate)
                .ValidateResponsibleTeamLinkText("CareProviders")
                .ValidateEndDateValue(endDate)
                .ValidateAllRolesYesRadionButtonChecked(false)
                .ValidateAllRolesNoRadionButtonChecked(true)
                .ValidateRolesMultiSelectElementText(_careProviderStaffRoleType2Id, _careProviderStaffRoleType2Name)
                .ValidateNoRequiredForInductionValue("1")
                .ValidateNoRequiredForAcceptanceValue("1")
                .ValidateStatusForInductionSelectedText("Completed")
                .ValidateStatusForAcceptanceSelectedText("Completed");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3504")]
        [Description("Steps 7 to 12 from the original jira test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirementSetup_UITestCases002()
        {
            #region Staff Recruitment Item

            _staffRecruitmentItemName = "CDV6_23505_Item_" + currentDateTime;
            _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            _careProviderStaffRoleType1Name = "CDV6_23505_Role_1_" + currentDateTime;
            _careProviderStaffRoleType2Name = "CDV6_23505_Role_2_" + currentDateTime;

            _careProviderStaffRoleType1Id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType1Name, null, null, new DateTime(2020, 1, 1), null);
            _careProviderStaffRoleType2Id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType2Name, null, null, new DateTime(2020, 1, 1), null);

            #endregion


            #region Step 7 to Step 12

            String startDate = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("dd'/'MM'/'yyyy");
            String endDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(5).ToString("dd'/'MM'/'yyyy");

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertRequirementName("Requirement_" + currentDateTime)
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateAllRolesYesRadionButtonChecked(true);

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .SelectStatusForInduction("Completed")
                .SelectStatusForAcceptance("Completed")

                .InsertStartDate(endDate)
                .InsertEndDate(startDate);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("End Date should be greater than or equal to Start Date")
                .TapOKButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertStartDate(startDate)
                .InsertEndDate(endDate)
                .InsertNoRequiredForInduction("0")
                .InsertNoRequiredForAcceptance("0");

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("No. Required for Induction and No. Required for Acceptance can not be both set to 0")
                .TapOKButton();

            recruitmentRequirementRecordPage
                .InsertNoRequiredForInduction("0")
                .InsertNoRequiredForAcceptance("1")
                .SelectStatusForAcceptance("Completed")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateNoRequiredForInductionValue("0")
                .ValidateNoRequiredForAcceptanceValue("1")
                .ValidateStatusForAcceptanceSelectedText("Completed")
                .ValidateStatusForInductionFieldDisabled(true)
                .ValidateStatusForAcceptanceFieldDisabled(false);

            recruitmentRequirementRecordPage
                .InsertNoRequiredForInduction("1")
                .SelectStatusForInduction("Completed")
                .InsertNoRequiredForAcceptance("0")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateNoRequiredForInductionValue("1")
                .ValidateStatusForInductionSelectedText("Completed")
                .ValidateNoRequiredForAcceptanceValue("0")
                .ValidateStatusForAcceptanceFieldDisabled(true)
                .ValidateStatusForInductionFieldDisabled(false);

            recruitmentRequirementRecordPage
                .ClickBackButton();

            var RecruitmentRequirementId = dbHelper.recruitmentRequirement.GetByName("Requirement_" + currentDateTime)[0];
            var RecordId = (int)dbHelper.recruitmentRequirement.GetRecruitmentRequirementByID(RecruitmentRequirementId, "recordid")["recordid"];

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertRequirementName("Requirement_" + currentDateTime)
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateAllRolesYesRadionButtonChecked(true);

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .SelectStatusForInduction("Completed")
                .SelectStatusForAcceptance("Completed")

                .InsertStartDate(startDate)
                .InsertEndDate(endDate);

            recruitmentRequirementRecordPage
                .InsertNoRequiredForInduction("1")
                .SelectStatusForInduction("Completed")
                .InsertNoRequiredForAcceptance("0")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage(_staffRecruitmentItemName + " setup record cannot be created because it overlaps with another record for the Role(s) selected. Please resolve the overlap or contact your System Administrator. Overlapping Setup records:\r\n"
                + RecordId + " - " + "Requirement_" + currentDateTime)
                .TapCloseButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertRequirementName("Requirement_" + currentDateTime)
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertStartDate(commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(3).ToString("dd'/'MM'/'yyyy"));

            recruitmentRequirementRecordPage
                .InsertNoRequiredForInduction("1")
                .SelectStatusForInduction("Completed")
                .InsertNoRequiredForAcceptance("1")
                .SelectStatusForAcceptance("Completed")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage(_staffRecruitmentItemName + " setup record cannot be created because it overlaps with another record for the Role(s) selected. Please resolve the overlap or contact your System Administrator. Overlapping Setup records:\r\n"
                + RecordId + " - " + "Requirement_" + currentDateTime)
                .TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3505")]
        [Description("Steps 13 to 16 from the original jira test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirementSetup_UITestCases003()
        {
            #region Staff Recruitment Item

            _staffRecruitmentItemName = "CDV6_23505_Item_" + currentDateTime;
            _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            _careProviderStaffRoleType1Name = "CDV6_23505_Role_1_" + currentDateTime;
            _careProviderStaffRoleType2Name = "CDV6_23505_Role_2_" + currentDateTime;

            _careProviderStaffRoleType1Id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType1Name, null, null, new DateTime(2020, 1, 1), null);
            _careProviderStaffRoleType2Id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType2Name, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Step 13 to Step 16

            _staffRecruitmentItem2Name = "CDV6_23505_Item2_" + currentDateTime;
            _staffRecruitmentItem2Id = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItem2Name, new DateTime(2020, 1, 1));

            _careProviderStaffRoleType2Name = "CDV6_23505_Role_3_" + currentDateTime;
            _careProviderStaffRoleType2Id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType2Name, null, null, new DateTime(2020, 1, 1), null);

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleType2Id };
            //Recruitment Requirement Setup Date Later than Applicant Start Date - Applicant Start Date < Recruitment Requirement Setup Start Date
            var future_recruitmentRequirementId = dbHelper.recruitmentRequirement.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItemName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(2).Day), null, careProviderStaffRoleTypeIds);
            //Recruitment Requirement Setup Date Earlier than Applicant Start Date - Applicant Start Date > Recruitment Requirement Setup Start Date
            var current_recruitmentRequirementId = dbHelper.recruitmentRequirement.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItem2Name, _staffRecruitmentItem2Id, "staffrecruitmentitem", _staffRecruitmentItem2Name, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            var firstName = "Testing_CDV6_23505A";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            var _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];
            dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleType2Id, _systemUserId, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad();

            var compliance_recruitmentDocumentId = dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItem2Name);
            Assert.IsTrue(compliance_recruitmentDocumentId.Count >= 1);

            recruitmentDocumentsPage
                .TypeSearchQuery(_staffRecruitmentItem2Name)
                .ClickSearchButton()
                .ValidateRecruitmentDocumentsRecordIsPresent(compliance_recruitmentDocumentId[0].ToString());

            _staffRecruitmentItem3Name = "CDV6_23505_Item3_" + currentDateTime;
            _staffRecruitmentItem3Id = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItem3Name, new DateTime(2020, 1, 1));

            _careProviderStaffRoleType3Name = "CDV6_23505_Role_4_" + currentDateTime;
            _careProviderStaffRoleType3Id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType3Name, null, null, new DateTime(2020, 1, 1), null);

            careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleType3Id };
            var active_recruitmentRequirementId = dbHelper.recruitmentRequirement.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItem3Name, _staffRecruitmentItem3Id, "staffrecruitmentitem", _staffRecruitmentItem3Name, false, 1, 3, 1, 3, DateTime.Now,
               DateTime.Now.AddDays(3), careProviderStaffRoleTypeIds);

            var inactive_recruitmentRequirementId = dbHelper.recruitmentRequirement.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItem3Name, _staffRecruitmentItem3Id, "staffrecruitmentitem", _staffRecruitmentItem3Name, false, 1, 3, 1, 3, DateTime.Now.AddDays(-4), DateTime.Now.AddDays(-1), careProviderStaffRoleTypeIds);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .SelectView("Active Records")
                .ClickRecordIdHeaderLink()
                .ClickRecordIdHeaderLink()
                .ValidateRecordIsPresent(active_recruitmentRequirementId.ToString())
                .SelectView("Inactive Records")
                .ClickRecordIdHeaderLink()
                .ClickRecordIdHeaderLink()
                .ValidateRecordIsPresent(inactive_recruitmentRequirementId.ToString())
                .SelectView("All Records")
                .ClickRecordIdHeaderLink()
                .ClickRecordIdHeaderLink()
                .ValidateRecordIsPresent(active_recruitmentRequirementId.ToString())
                .ValidateRecordIsPresent(inactive_recruitmentRequirementId.ToString());

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3743

        [TestProperty("JiraIssueID", "ACC-3742")]
        [Description("Add ID number of Recruitment Setup record to enhance the identification of a duplicate record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirementSetup_UITestCases004()
        {
            #region Staff Recruitment Item

            _staffRecruitmentItemName = "CDV6_23505_Item_" + currentDateTime;
            _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            _careProviderStaffRoleType1Name = "CDV6_23505_Role_1_" + currentDateTime;
            _careProviderStaffRoleType2Name = "CDV6_23505_Role_2_" + currentDateTime;

            _careProviderStaffRoleType1Id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType1Name, null, null, new DateTime(2020, 1, 1), null);
            _careProviderStaffRoleType2Id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType2Name, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Step 1

            _staffRecruitmentItemName = "ACC3742_Item1" + currentDateTime;
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            _careProviderStaffRoleType1Name = "ACC3742_Role1_" + currentDateTime;
            _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType1Name, null, null, new DateTime(2020, 1, 1), "1 ACC 3742 ...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleType1Id };

            _careProviderStaffRoleType2Name = "ACC3742_Role2_" + currentDateTime;
            _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType2Name, null, null, new DateTime(2020, 1, 1), "2 ACC 3742 ...");
            List<Guid> careProviderStaffRoleType2Ids = new List<Guid>() { _careProviderStaffRoleType2Id };

            var _recruitmentRequirementName = "RecReq_3742" + currentDateTime;
            var recruitmentRequirementId = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);
            var recordIdValue1 = (int)dbHelper.recruitmentRequirement.GetRecruitmentRequirementByID(recruitmentRequirementId, "recordid")["recordid"];

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .ValidateRecruitmentRequirementSetupSubLinkVisible()
                .ClickRecruitmentRequirementSetupSubLink();

            #endregion

            #region Step 3

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickRecordIdHeaderLink()
                .ClickRecordIdHeaderLink()
                .ValidateRecordIsPresent(recruitmentRequirementId.ToString());

            #endregion

            #region Step 4, Step 8

            recruitmentRequirementsPage
                .OpenRecord(recruitmentRequirementId.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateRecordIdFieldVisibility(true)
                .ValidateRecordIdFieldValue(recordIdValue1.ToString())
                .ValidateRequirementNameMandatoryFieldVisibility(true)
                .ValidateRequiredItemTypeMandatoryFieldVisibility(true)
                .ValidateStartDateMandatoryFieldVisibility(true)
                .ValidateAllRolesMandatoryFieldVisibility(true)
                .ValidateRolesMandatoryFieldVisibility(true)
                .ValidateNoRequiredForInductionMandatoryFieldVisibility(true)
                .ValidateStatusForInductionMandatoryFieldVisibility(true)
                .ValidateNoRequiredForAcceptanceMandatoryFieldVisibility(true)
                .ValidateStatusForAcceptanceMandatoryFieldVisibility(true)
                .ValidateResponsibleTeamMandatoryFieldVisibility(true)
                .ClickBackButton();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateRecordIdFieldVisibility(true)
                .ValidateRequirementNameMandatoryFieldVisibility(true)
                .ValidateRequiredItemTypeMandatoryFieldVisibility(true)
                .ValidateStartDateMandatoryFieldVisibility(true)
                .ValidateAllRolesMandatoryFieldVisibility(true)
                .ClickAllRolesNoRadioButton()
                .ValidateRolesMandatoryFieldVisibility(true)
                .ValidateNoRequiredForInductionMandatoryFieldVisibility(true)
                .ValidateStatusForInductionMandatoryFieldVisibility(true)
                .ValidateNoRequiredForAcceptanceMandatoryFieldVisibility(true)
                .ValidateStatusForAcceptanceMandatoryFieldVisibility(true)
                .ValidateResponsibleTeamMandatoryFieldVisibility(true);

            #endregion

            #region Step 7

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertRequirementName(_staffRecruitmentItemName + "Dup")
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickAllRolesNoRadioButton()
                .ClickRolesLookUpButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_careProviderStaffRoleType1Name)
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleType1Id.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .SelectStatusForInduction("Completed")
                .SelectStatusForAcceptance("Completed")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage(_staffRecruitmentItemName + " setup record cannot be created because it overlaps with another record for the Role(s) selected. Please resolve the overlap or contact your System Administrator." +
                " Overlapping Setup records:\r\n" + recordIdValue1 + " - " + _recruitmentRequirementName)
                .TapCloseButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 11

            var _recruitmentRequirement2Name = "RecReq_3742b" + currentDateTime;
            var recruitmentRequirementId2 = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirement2Name, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleType2Ids);
            var recordIdValue2 = (int)dbHelper.recruitmentRequirement.GetRecruitmentRequirementByID(recruitmentRequirementId2, "recordid")["recordid"];

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordIsPresent(recruitmentRequirementId2.ToString())
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertRequirementName(_staffRecruitmentItemName + "Dup2")
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickAllRolesNoRadioButton()
                .ClickRolesLookUpButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_careProviderStaffRoleType1Name)
                .TapSearchButton()
                .AddElementToList(_careProviderStaffRoleType1Id.ToString())
                .TypeSearchQuery(_careProviderStaffRoleType2Name)
                .TapSearchButton()
                .AddElementToList(_careProviderStaffRoleType2Id.ToString())
                .TapOKButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .SelectStatusForInduction("Completed")
                .SelectStatusForAcceptance("Completed")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText(_staffRecruitmentItemName + " setup record cannot be created because it overlaps with another record for the Role(s) selected. Please resolve the overlap or contact your System Administrator." +
                " Overlapping Setup records:\r\n")
                .ValidateMessageContainsText(recordIdValue2 + " - " + _recruitmentRequirement2Name)
                .ValidateMessageContainsText(recordIdValue1 + " - " + _recruitmentRequirementName)
                .TapCloseButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3881

        [TestProperty("JiraIssueID", "ACC-3885")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirementSetup_UITestCases005()
        {
            #region Care provider staff role type

            var _defaultCareProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Default CPSRT", null, null, new DateTime(2021, 1, 1), "Default CPSRT ...");

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleName = "Rookie";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Rookie ...");

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "SRI_3885";
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_3885_A", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3885_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            var _recruitmentRequirementName = "RecReq_3885";
            var recruitmentRequirementId = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Step 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ValidateHeaderCellVisible("All Business Units?");

            #endregion

            #region Step 3

            recruitmentRequirementsPage
                .InsertQuickSearchText("*3885*")
                .ClickQuickSearchButton()
                .WaitForPageToLoadAfterSearch()
                .OpenRecord(recruitmentRequirementId.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateAllBusinessUnitsFieldVisible(false);

            #endregion

            #region Step 4

            //in this step we will not create a new record. in step 3 we alredy openned an existing record and validate that the field is not there

            recruitmentRequirementRecordPage
                .ClickBackButton();

            recruitmentRequirementsPage
                .WaitForPageToLoadAfterSearch()
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateAllBusinessUnitsFieldVisible(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3906")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirementSetup_UITestCases006()
        {
            #region Care provider staff role type

            var _defaultCareProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Default CPSRT", null, null, new DateTime(2021, 1, 1), "Default CPSRT ...");

            var _careProviderStaffRoleName = "Rookie";
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Rookie ...");

            var _careProviderStaffRole2Name = "Reviewer";
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRole2Name, null, null, new DateTime(2021, 1, 1), "Reviewer ...");

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "SRI_3885";
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_3885_A", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeId };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3885_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            var _recruitmentRequirementName = "RecReq_3885";
            var recruitmentRequirementId = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);
            var recruitmentRequirementRecordId = (dbHelper.recruitmentRequirement.GetRecruitmentRequirementByID(recruitmentRequirementId, "recordid")["recordid"]).ToString();

            var _recruitmentRequirement2Name = "RecReq_3885_AllRoles";
            var recruitmentRequirement2Id = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirement2Name, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, true, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, null);
            var recruitmentRequirement2RecordId = (dbHelper.recruitmentRequirement.GetRecruitmentRequirementByID(recruitmentRequirement2Id, "recordid")["recordid"]).ToString();

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateAllBusinessUnitsFieldVisible(false);

            recruitmentRequirementRecordPage
                .InsertRequirementName("RecReq 3885 All Roles")
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("SRI_3885", _staffRecruitmentItemId);

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickAllRolesYesRadioButton()
                .SelectStatusForInduction("Completed")
                .SelectStatusForAcceptance("Completed")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("SRI_3885 setup record cannot be created because it overlaps with another record for the Role(s) selected. Please resolve the overlap or contact your System Administrator. Overlapping Setup records:")
                .ValidateMessageContainsText(recruitmentRequirement2RecordId + " - RecReq_3885_AllRoles")
                .TapCloseButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad();

            #endregion

            #region Step 6

            recruitmentRequirementsPage
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateAllBusinessUnitsFieldVisible(false);

            recruitmentRequirementRecordPage
                .InsertRequirementName("RecReq 3885 All Roles")
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("SRI_3885", _staffRecruitmentItemId);

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickAllRolesNoRadioButton()
                .ClickRolesLookUpButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_careProviderStaffRoleName).TapSearchButton().AddElementToList(_careProviderStaffRoleTypeId)
                .TypeSearchQuery(_careProviderStaffRole2Name).TapSearchButton().AddElementToList(_careProviderStaffRoleType2Id)
                .TapOKButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .SelectStatusForInduction("Completed")
                .SelectStatusForAcceptance("Completed")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("SRI_3885 setup record cannot be created because it overlaps with another record for the Role(s) selected. Please resolve the overlap or contact your System Administrator. Overlapping Setup records:")
                .ValidateMessageContainsText(recruitmentRequirementRecordId + " - RecReq_3885")
                .TapCloseButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad();

            #endregion

            #region Step 7

            recruitmentRequirementsPage
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateAllBusinessUnitsFieldVisible(false);

            recruitmentRequirementRecordPage
                .InsertRequirementName("RecReq 3885 All Roles")
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("SRI_3885", _staffRecruitmentItemId);

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickAllRolesNoRadioButton()
                .ClickRolesLookUpButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_careProviderStaffRoleName).TapSearchButton().AddElementToList(_careProviderStaffRoleTypeId)
                .TapOKButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .SelectStatusForInduction("Completed")
                .SelectStatusForAcceptance("Completed")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("SRI_3885 setup record cannot be created because it overlaps with another record for the Role(s) selected. Please resolve the overlap or contact your System Administrator. Overlapping Setup records:")
                .ValidateMessageContainsText(recruitmentRequirementRecordId + " - RecReq_3885")
                .TapCloseButton();

            #endregion

            #region Step 8

            //Step 8 is covered as part of other automated tests

            #endregion

            #region Step 9

            //Step 9 is covered as part of other automated tests

            #endregion
        }

        #endregion
    }
}
