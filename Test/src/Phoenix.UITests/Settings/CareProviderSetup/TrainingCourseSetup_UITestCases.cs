using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.CareProviderSetup
{
    [TestClass]
    public class TrainingCourseSetup_UITestCases : FunctionalTest
    {

        #region Private Properties
        private string _tenantName;
        private string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _careProviderQA_BusinessUnitId;
        private Guid _careProviderQA_TeamId;
        private Guid _systemUserId;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private Guid staffTrainingItemId;
        private string staffTrainingItemName;
        private string _username = "User_CDV6_22132";
        private string _providerName1;
        private Guid _providerId1;
        private string _providerName2;
        private Guid _providerId2;

        #endregion

        #region Private Methods

        private void CreateTrainingCourseSetup(string TrainingCourseName, Guid StaffTrainingItemId, string ValidFromDate, string ValidToDate, string Category)
        {
            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .ClickTrainingItemLookup();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(TrainingCourseName)
                .TapSearchButton()
                .SelectResultElement(StaffTrainingItemId.ToString());

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertValidFromDate_Field("")
                .InsertValidToDate_Field("")
                .InsertValidFromDate_Field(ValidFromDate)
                .InsertValidToDate_Field(ValidToDate)
                .SelectCategory(Category)
                .InsertCourseTitle(TrainingCourseName + " - " + Category)
                .InsertCourseDescription("...")
                .InsertLengthOfCourseMinutes("60")
                .ClickSaveButton();
        }

        #endregion

        [TestInitialize()]
        public void RecruitmentRequirements_SetupMethod()
        {
            try
            {
                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Business Unit

                _careProviderQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Team

                _careProviderQA_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviderQA_BusinessUnitId, null, "CareProviderQA@careworkstempmail.com", "Default team for business unit", null);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion

                #region Create SystemUser Record
                _username = _username + currentDateTime;
                _systemUserId = dbHelper.systemUser.CreateSystemUser(_username, "User", "CDV6_22132" + currentDateTime, "User_CDV6_22132", "Passw0rd_!", "User_CDV6_22132@somemail.com", "User_CDV6_22132@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviderQA_BusinessUnitId, _careProviderQA_TeamId, true, 4);

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemUserId, DateTime.Now.Date);

                #endregion

                #region Staff Training Item
                staffTrainingItemName = "Item_22132_" + currentDateTime;
                staffTrainingItemId = dbHelper.staffTrainingItem.CreateStaffTrainingItem(_careProviderQA_TeamId, staffTrainingItemName, new DateTime(2022, 1, 1));

                #endregion

                #region Create New Providers

                _providerName1 = "22132_Provider_1_" + currentDateTime;
                _providerName2 = "22132_Provider_2_" + currentDateTime;
                _providerId1 = commonMethodsDB.CreateProvider(_providerName1, _careProviderQA_TeamId, 12);
                _providerId2 = commonMethodsDB.CreateProvider(_providerName2, _careProviderQA_TeamId, 12);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22132

        [TestProperty("JiraIssueID", "ACC-3584")]
        [Description("Steps 1 to 6 from the original test CDV6-12953")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Training Courses")]
        public void TrainingCourseSetup_UITestMethod01()
        {

            #region Step 1
            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", _tenantName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToTrainingCourseSetupPage();

            trainingCourseSetupPage
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickNewRecordButton();

            CreateTrainingCourseSetup(staffTrainingItemName, staffTrainingItemId, DateTime.Today.ToString("dd'/'MM'/'yyyy"), DateTime.Today.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Internal");

            trainingCourseRecordPage
                .WaitForRecordToBeSaved()
                .WaitForTrainingCourseRecordPageToLoad()
                .ValidateTrainingCourseTitle(staffTrainingItemName + " - Internal")
                .ValidateTrainingItemLinkText(staffTrainingItemName)
                .ValidateResponsibleTeamLinkText("CareProviders")
                .ValidateValidFromDateValue(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
                .ValidateValidToDateValue(DateTime.Today.AddDays(5).ToString("dd'/'MM'/'yyyy"))
                .ValidateCategoryPicklistSelectedText("Internal");
            #endregion

            #region Step 2
            trainingCourseRecordPage
                .ClickBackButton();

            trainingCourseSetupPage
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickNewRecordButton();

            CreateTrainingCourseSetup(staffTrainingItemName, staffTrainingItemId, DateTime.Today.ToString("dd'/'MM'/'yyyy"), DateTime.Today.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Internal");

            dynamicDialogPopup
                 .WaitForCareCloudDynamicDialoguePopUpToLoad()
                 .ValidateMessage("Duplicate Training Course record found, please check and try again.")
                 .TapCloseButton();
            #endregion

            #region Step 3
            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertValidFromDate_Field("")
                .InsertValidToDate_Field("")
                .InsertValidFromDate_Field(DateTime.Today.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .InsertValidToDate_Field(DateTime.Today.AddDays(6).ToString("dd'/'MM'/'yyyy"))
                .SelectCategory("Internal")
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForCareCloudDynamicDialoguePopUpToLoad()
                 .ValidateMessage("Duplicate Training Course record found, please check and try again.")
                 .TapCloseButton();

            #endregion

            #region Step 4

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertValidFromDate_Field("")
                .InsertValidToDate_Field("")
                .InsertValidFromDate_Field(DateTime.Today.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .InsertValidToDate_Field(DateTime.Today.AddDays(16).ToString("dd'/'MM'/'yyyy"))
                .SelectCategory("Internal")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForTrainingCourseRecordPageToLoad()
                .ValidateTrainingCourseTitle(staffTrainingItemName + " - Internal")
                .ValidateTrainingItemLinkText(staffTrainingItemName)
                .ValidateResponsibleTeamLinkText("CareProviders")
                .ValidateValidFromDateValue(DateTime.Today.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ValidateValidToDateValue(DateTime.Today.AddDays(16).ToString("dd'/'MM'/'yyyy"))
                .ValidateCategoryPicklistSelectedText("Internal");
            #endregion

            #region Step 5
            //Step 5 is not valid. BU field is not available and applicable anymore.
            #endregion

            #region Step 6
            trainingCourseRecordPage
                .ClickBackButton();

            trainingCourseSetupPage
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickNewRecordButton();

            string staffTrainingItem2Name = "Item_22132_2" + currentDateTime;
            var StaffTrainingItem2Id = dbHelper.staffTrainingItem.CreateStaffTrainingItem(_careProviderQA_TeamId, staffTrainingItem2Name, new DateTime(2021, 1, 1));
            CreateTrainingCourseSetup(staffTrainingItem2Name, StaffTrainingItem2Id, DateTime.Today.AddDays(7).ToString("dd'/'MM'/'yyyy"), DateTime.Today.AddDays(16).ToString("dd'/'MM'/'yyyy"), "Internal");

            trainingCourseRecordPage
                .WaitForRecordToBeSaved()
                .WaitForTrainingCourseRecordPageToLoad()
                .ValidateTrainingCourseTitle(staffTrainingItem2Name + " - Internal")
                .ValidateTrainingItemLinkText(staffTrainingItem2Name)
                .ValidateResponsibleTeamLinkText("CareProviders")
                .ValidateValidFromDateValue(DateTime.Today.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ValidateValidToDateValue(DateTime.Today.AddDays(16).ToString("dd'/'MM'/'yyyy"))
                .ValidateCategoryPicklistSelectedText("Internal");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3585")]
        [Description("Steps 7 to 11 from the original test CDV6-12953")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Training Courses")]
        public void TrainingCourseSetup_UITestMethod02()
        {
            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", _tenantName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToTrainingCourseSetupPage();

            trainingCourseSetupPage
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickNewRecordButton();

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .ClickTrainingItemLookup();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(staffTrainingItemName)
               .TapSearchButton()
               .SelectResultElement(staffTrainingItemId.ToString());

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertValidFromDate_Field(DateTime.Now.AddDays(-7).ToString("dd'/'MM'/'yyyy"))
                .InsertValidToDate_Field(DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy"))
                .SelectCategory("External")
                .ValidateProviderFieldSection(true)
                .ClickProviderLookup();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_providerName1.ToString())
               .TapSearchButton()
               .SelectResultElement(_providerId1.ToString());

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertCourseTitle(staffTrainingItemName + " - " + "External")
                .InsertCourseDescription("...")
                .InsertLengthOfCourseMinutes("60")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateTrainingItemLinkText(staffTrainingItemName)
                .ValidateResponsibleTeamLinkText("CareProviders")
                .ValidateValidFromDateValue(DateTime.Now.AddDays(-7).ToString("dd'/'MM'/'yyyy"))
                .ValidateValidToDateValue(DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy"))
                .ValidateCategoryPicklistSelectedText("External")
                .ValidateProviderLinkText(_providerName1.ToString())
                .ClickBackButton();

            #endregion

            #region Step 8

            trainingCourseSetupPage
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickNewRecordButton();

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .ClickTrainingItemLookup();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(staffTrainingItemName)
               .TapSearchButton()
               .SelectResultElement(staffTrainingItemId.ToString());

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertValidFromDate_Field(DateTime.Now.AddDays(-7).ToString("dd'/'MM'/'yyyy"))
                .InsertValidToDate_Field(DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy"))
                .SelectCategory("External")
                .ValidateProviderFieldSection(true)
                .ClickProviderLookup();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_providerName2.ToString())
               .TapSearchButton()
               .SelectResultElement(_providerId2.ToString());

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertCourseTitle(staffTrainingItemName + " - " + "External")
                .InsertCourseDescription("...")
                .InsertLengthOfCourseMinutes("60")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateTrainingItemLinkText(staffTrainingItemName)
                .ValidateResponsibleTeamLinkText("CareProviders")
                .ValidateValidFromDateValue(DateTime.Now.AddDays(-7).ToString("dd'/'MM'/'yyyy"))
                .ValidateValidToDateValue(DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy"))
                .ValidateCategoryPicklistSelectedText("External")
                .ValidateProviderLinkText(_providerName2.ToString())
                .ClickBackButton();

            #endregion

            #region Step 9
            // Step 9 is not valid. BU field is not available and applicable anymore.
            #endregion

            #region Step 10

            trainingCourseSetupPage
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickNewRecordButton();

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .ClickTrainingItemLookup();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(staffTrainingItemName)
               .TapSearchButton()
               .SelectResultElement(staffTrainingItemId.ToString());

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertValidFromDate_Field(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .InsertValidToDate_Field(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .SelectCategory("External")
                .ClickProviderLookup();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_providerName2.ToString())
               .TapSearchButton()
               .SelectResultElement(_providerId2.ToString());

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertCourseTitle(staffTrainingItemName + " - " + "External")
                .InsertCourseDescription("...")
                .InsertLengthOfCourseMinutes("60")
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("Duplicate Training Course record found, please check and try again.")
                 .TapCloseButton();

            #endregion

            #region Step 11

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertValidFromDate_Field(DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateTrainingItemLinkText(staffTrainingItemName)
                .ValidateResponsibleTeamLinkText("CareProviders")
                .ValidateValidFromDateValue(DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy"))
                .ValidateValidToDateValue(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ValidateCategoryPicklistSelectedText("External")
                .ValidateProviderLinkText(_providerName2.ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3586")]
        [Description("Steps 12 & 13 from the original test CDV6-12953")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Training Courses")]
        public void TrainingCourseSetup_UITestMethod03()
        {
            #region Create Training Item & Training Courses

            string trainingCourseName_1 = "Item_22132_1_" + currentDateTime;
            string trainingCourseName_2 = "Item_22132_2_" + currentDateTime;
            string trainingCourseName_3 = "Item_22132_3_" + currentDateTime;

            Guid staffTrainingItem_1 = dbHelper.staffTrainingItem.CreateStaffTrainingItem(_careProviderQA_TeamId, trainingCourseName_1, new DateTime(2022, 1, 1));
            Guid staffTrainingItem_2 = dbHelper.staffTrainingItem.CreateStaffTrainingItem(_careProviderQA_TeamId, trainingCourseName_2, new DateTime(2022, 1, 1));
            Guid staffTrainingItem_3 = dbHelper.staffTrainingItem.CreateStaffTrainingItem(_careProviderQA_TeamId, trainingCourseName_3, new DateTime(2022, 1, 1));

            string training_title_1 = trainingCourseName_1 + " - Internal";
            string training_title_2 = trainingCourseName_2 + " - Internal";
            string training_title_3 = trainingCourseName_3 + " - Internal";

            Guid training_course_Id_1 = dbHelper.trainingRequirement.CreateTrainingRequirement(training_title_1, _careProviderQA_TeamId, staffTrainingItem_1, new DateTime(2022, 11, 11), null, null, 1);
            Guid training_course_Id_2 = dbHelper.trainingRequirement.CreateTrainingRequirement(training_title_2, _careProviderQA_TeamId, staffTrainingItem_2, new DateTime(2022, 11, 11), null, null, 1);
            Guid training_course_Id_3 = dbHelper.trainingRequirement.CreateTrainingRequirement(training_title_3, _careProviderQA_TeamId, staffTrainingItem_3, new DateTime(2022, 11, 11), null, null, 1);

            #endregion

            #region Step 12 & 13

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", _tenantName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToTrainingCourseSetupPage();

            trainingCourseSetupPage
                .WaitForTrainingCourseSetupPageToLoad()
                .SelectView("Active Records")
                .WaitForTrainingCourseSetupPageToLoad()
                .ValidateRecordIsPresent(training_course_Id_1.ToString())
                .ValidateRecordIsPresent(training_course_Id_2.ToString())
                .ValidateRecordIsPresent(training_course_Id_3.ToString());

            trainingCourseSetupPage
                .SelectView("Inactive Records")
                .WaitForTrainingCourseSetupPageToLoad()
                .ValidateRecordIsNotPresent(training_course_Id_1.ToString())
                .ValidateRecordIsNotPresent(training_course_Id_2.ToString())
                .ValidateRecordIsNotPresent(training_course_Id_3.ToString());

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);

            dbHelper.trainingRequirement.UpdateValidToDate(training_course_Id_1, new DateTime(2022, 11, 15));
            dbHelper.trainingRequirement.UpdateValidToDate(training_course_Id_2, new DateTime(2022, 11, 15));

            trainingCourseSetupPage
                .SelectView("Active Records")
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickHeaderCell(12)
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickHeaderCell(12)
                .WaitForTrainingCourseSetupPageToLoad()
                .ValidateRecordIsNotPresent(training_course_Id_1.ToString())
                .ValidateRecordIsNotPresent(training_course_Id_2.ToString())
                .ValidateRecordIsPresent(training_course_Id_3.ToString());

            trainingCourseSetupPage
                .SelectView("Inactive Records")
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickHeaderCell(12)
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickHeaderCell(12)
                .WaitForTrainingCourseSetupPageToLoad()
                .ValidateRecordIsPresent(training_course_Id_1.ToString())
                .ValidateRecordIsPresent(training_course_Id_2.ToString())
                .ValidateRecordIsNotPresent(training_course_Id_3.ToString());

            #endregion

            #region Step 14
            // Step 14 is not valid. BU field is not available and applicable anymore.
            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22350

        [TestProperty("JiraIssueID", "ACC-3587")]
        [Description("login into CD." +
            "Navigate to Settings > Care Provider Setup> Training Course Setup" +
            "Create Training Courses and Verify Date alert message & also verify other fields.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Training Courses")]
        public void TrainingCourseSetup_UITestMethod04()
        {
            #region Step 1, 2, 4 & 10

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", _tenantName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToTrainingCourseSetupPage();

            trainingCourseSetupPage
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickNewRecordButton();

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .ClickTrainingItemLookup();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(staffTrainingItemName)
               .TapSearchButton()
               .SelectResultElement(staffTrainingItemId.ToString());

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertValidFromDate_Field(DateTime.Now.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
                .InsertValidToDate_Field(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .InsertDurationInDays_Field("5")
                .InsertCourseCapacity_Field("2")
                .SelectRecurrence("Monthly")
                .SelectCategory("External")
                .ValidateProviderFieldSection(true)
                .ClickProviderLookup();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(_providerName1.ToString())
               .TapSearchButton()
               .SelectResultElement(_providerId1.ToString());

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertCourseTitle(staffTrainingItemName + " - " + "External")
                .InsertCourseDescription("...")
                .InsertLengthOfCourseMinutes("60")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateTrainingItemLinkText(staffTrainingItemName)
                .ValidateResponsibleTeamLinkText("CareProviders")
                .ValidateValidFromDateValue(DateTime.Now.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
                .ValidateValidToDateValue(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ValidateDurationInDaysValue("5")
                .ValidateCourseCapacityValue("2")
                .ValidateRecurrencePicklistSelectedText("Monthly")
                .ValidateCategoryPicklistSelectedText("External")
                .ValidateProviderLinkText(_providerName1.ToString())
                .ClickBackButton();

            #endregion

            #region Step 7 & 10

            trainingCourseSetupPage
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickNewRecordButton();

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .ClickTrainingItemLookup();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(staffTrainingItemName)
               .TapSearchButton()
               .SelectResultElement(staffTrainingItemId.ToString());

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertValidFromDate_Field(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .InsertValidToDate_Field(DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy"))
                .SelectCategory("Internal")
                .ValidateProviderFieldSection(false);

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .InsertCourseTitle(staffTrainingItemName + " - " + "External")
                .InsertCourseDescription("...")
                .InsertLengthOfCourseMinutes("60")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateTrainingItemLinkText(staffTrainingItemName)
                .ValidateResponsibleTeamLinkText("CareProviders")
                .ValidateValidFromDateValue(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ValidateValidToDateValue(DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy"))
                .ValidateCategoryPicklistSelectedText("Internal")
                .ClickBackButton();

            #endregion

            #region Step 9

            trainingCourseSetupPage
                .WaitForTrainingCourseSetupPageToLoad()
                .ClickNewRecordButton();

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .ClickTrainingItemLookup();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(staffTrainingItemName)
               .TapSearchButton()
               .SelectResultElement(staffTrainingItemId.ToString());

            trainingCourseRecordPage
                .WaitForTrainingCourseRecordPageToLoad()
                .SelectCategory("Internal")
                .InsertValidFromDate_Field(DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy"))
                .InsertValidToDate_Field(DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy"));

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Valid to date must be greater than valid from date.")
                .TapOKButton();

            #endregion

            #region Step 3, 5, 6, 8, 11, 12 & 13
            // Step 3, 5, 6, 8, 11, 12 & 13 covered in CDV6-22132[]
            #endregion

            #region Step 14
            // Step 14 is not valid. BU field is not available and applicable anymore.
            #endregion
        }

        #endregion

    }

}
