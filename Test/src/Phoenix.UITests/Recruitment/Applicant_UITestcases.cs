using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Phoenix.UITests.Settings.FormsManagement
{
    /// <summary>
    /// This class contains Automated UI test scripts for
    /// </summary>
    [TestClass]
    public class Applicant_UITestCases : FunctionalTest
    {

        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _systemUserID;
        private Guid _environmentId;
        private Guid _applicantId;
        private Guid _careProviderStaffRoleTypeid;
        public Guid environmentid;
        private Guid _roleApplicant;

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {

            #region Connecting Database : CareProvider

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["CareDirectorQA_CDEntities"].ConnectionString = connectionStringsSection.ConnectionStrings["CareDirectorQA_CDEntities"].ConnectionString.Replace("&quot;", "\"").Replace("CareDirectorQA_CD", "CareProviders_CD");
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

            #endregion Connecting Database : CareProvider

            #region Environment Name
            EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
            _environmentId = Guid.Parse(ConfigurationManager.AppSettings["CareProvidersEnvironmentID"]);

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_environmentId);

            #endregion

            #region Business Unit
            var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
            _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];


            #endregion

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
            _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

            #endregion

            #region Create default system user

            var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_1").Any();
            if (!defaultLoginUserExists)
            {
                _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_1", "CW", "Admin_Test_User_1", "CW Admin Test User 1", "Passw0rd_!", "CW_Admin_Test_User_1@somemail.com", "CW_Admin_Test_User_1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true);

                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("CW Systemuser - Secure Fields (Edit)").First();
                var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("CW Staff Reviews (Team Edit)").First();
                var staffReviewsMyRecordsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("CW Staff Reviews (My Records)").First();

                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsMyRecordsSecurityProfileId);
            }

            if (Guid.Empty == _defaultLoginUserID)
                _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_3").FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

            #endregion  Create default system user

            //  dbHelper = new DBHelper.DatabaseHelper("CW_Admin_Test_User_3", "Passw0rd_!", environmentid);

            #region Team Manager

            dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

            #endregion

            #region Create SystemUser Record

            var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_13797").Any();
            if (!newSystemUser)
                _systemUserID = dbHelper.systemUser.CreateSystemUser("CW_Test_User_CDV6_13797", "CW", "Test_User_CDV6_13797", "CW Test_User_CDV6_13797", "Passw0rd_!", "CW_Test_User_CDV6_13797@somemail.com", "CW_Test_User_CDV6_13797@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

            if (_systemUserID == Guid.Empty)
                _systemUserID = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_13797").FirstOrDefault();

            #endregion  Create SystemUser Record

            #region Applicant

            var newApplicant = dbHelper.applicant.GetByFirstName("Testing_CDV6_14317_01").Any();
            if (!newApplicant)
            {
                _applicantId = dbHelper.applicant.CreateApplicant("Testing_CDV6_14317_01", "User_01", _careProviders_TeamId);
            }
            if (_applicantId == Guid.Empty)
            {
                _applicantId = dbHelper.applicant.GetByFirstName("Testing_CDV6_14317_01")[0];
            }

            #endregion

            #region Care provider staff role type

            var careProviderStaffRoleTypeExists = dbHelper.careProviderStaffRoleType.GetByName("Helper").Any();
            if (!careProviderStaffRoleTypeExists)
            {
                _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            }
            if (_careProviderStaffRoleTypeid == Guid.Empty)
            {
                _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.GetByName("Helper").FirstOrDefault();
            }

            #endregion


            #region Role Applicant

            //delete existing record

            foreach (var roleApplicantId in dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId))
            {
                foreach (var workSheduleId in dbHelper.userWorkSchedule.GetUserRoleApplicantID(_applicantId))
                    dbHelper.userWorkSchedule.DeleteUserWorkSchedule(workSheduleId);

                dbHelper.recruitmentRoleApplicant.DeleteRecruitmentRoleApplicant(roleApplicantId);
            }

            var roleApplicantExists = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId).Any();
            if (!roleApplicantExists)
            {
                _roleApplicant = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            }
            if (_roleApplicant == Guid.Empty)
            {
                _roleApplicant = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId).FirstOrDefault();
            }

            #endregion

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-14317

        [TestProperty("JiraIssueID", "CDV6-15072")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has no Role Applications->  Click on Availability tab" +
            "Should not display Schedule Availability tab and should give appropriate error message to say the selected applicant doesn't have Role Applications")]
        [TestMethod, TestCategory("AWSReady"), TestCategory("UITest"), TestCategory("UK Care Providers Combined")]

        public void Applicant_UITestCases001()
        {
            _applicantId = dbHelper.applicant.CreateApplicant("Testing_CDV6_15832", "User_02", _careProviders_TeamId);

            loginPage
              .GoToLoginPage()
              .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
              .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("Testing_CDV6_15832")
                .OpenApplicantRecord(_applicantId.ToString());
            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ValidateScheduleApplicantTabVisibility(false)
                .ValidateNoApplicationRecordPopUpVisibile(true);

        }

        [TestProperty("JiraIssueID", "CDV6-15073")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has  Role Applications->  Click on Availability tab" +
         "Should display Schedule Availability tab and should give all existing Role Applications details to create Schedule Availability")]
        [TestMethod, TestCategory("AWSReady"), TestCategory("UITest"), TestCategory("UK Care Providers Combined")]
        public void Applicant_UITestCases002()
        {
            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("Testing_CDV6_14317_01")
                .OpenApplicantRecord(_applicantId.ToString());
            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ValidateScheduleApplicantTabVisibility(true)
                .ValidateNoApplicationRecordPopUpVisibile(false);
        }

        [TestProperty("JiraIssueID", "CDV6-15074")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has  Role Applications->  Click on Availability tab" +
       "Should display Schedule Availability tab and should give all existing Role Applications details to create Schedule Availability-> Click on Create Option for any Role Application in any day" +
            "Should give option to select availability as below Standard,Over Time & Regular -> Select standard -> Click Save -> Record should be saved successfully")]
        [TestMethod, TestCategory("AWSReady"), TestCategory("UITest"), TestCategory("UK Care Providers Combined")]
        public void Applicant_UITestCases003()
        {

            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("Testing_CDV6_14317_01")
                .OpenApplicantRecord(_applicantId.ToString());
            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();
            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectSheduleAvailabilityType(1) //standard
                .ClickOnSaveButton();
        }

        [TestProperty("JiraIssueID", "CDV6-15075")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has  Role Applications->  Click on Availability tab" +
       "Should display Schedule Availability tab and should give all existing Role Applications details to create Schedule Availability-> Click on Create Option for any Role Application in any day" +
            "Should give option to select availability as below Standard,Over Time & Regular -> Over Time -> Click Save -> Record should be saved successfully")]
        [TestMethod, TestCategory("AWSReady"), TestCategory("UITest"), TestCategory("UK Care Providers Combined")]
        public void Applicant_UITestCases004()
        {

            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("Testing_CDV6_14317_01")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectSheduleAvailabilityType(2)   //over time
                .ClickOnSaveButton();
        }
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has  Role Applications->  Click on Availability tab" +
        "Should display Schedule Availability tab and should give all existing Role Applications details to create Schedule Availability-> Click on Create Option for any Role Application in any day" +
             "Should give option to select availability as below Standard,Over Time & Regular ->  Regular -> Click Save -> Record should be saved successfully")]
        [TestMethod, TestCategory("AWSReady"), TestCategory("UITest"), TestCategory("UK Care Providers Combined")]
        public void Applicant_UITestCases005()
        {

            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("Testing_CDV6_14317_01")
                .OpenApplicantRecord(_applicantId.ToString());
            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();
            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectSheduleAvailabilityType(3)        //regular
                .ClickOnSaveButton();
        }
        [TestProperty("JiraIssueID", "CDV6-15078")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has  Role Applications->  Click on Availability tab" +
        "Should display Schedule Availability tab and should give all existing Role Applications details to create Schedule Availability-> Click on Create Option for any Role Application in any day" +
             "Should give option to select availability as below Standard,Over Time & Regular ->  Select any other availability  type other than its own availability  " +
            "Existing availability should be modified to newly selected availability type -> Click Save -> Record should be saved successfully")]

        [TestMethod, TestCategory("AWSReady"), TestCategory("UITest"), TestCategory("UK Care Providers Combined")]
        public void Applicant_UITestCases006()
        {

            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("Testing_CDV6_14317_01")
                .OpenApplicantRecord(_applicantId.ToString());
            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();
            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectSheduleAvailabilityType(1)
                .ClickOnSaveButton()
                .ClickExsistScheduleAvailabilty()
                .SelectSheduleAvailabilityType(2)
                .ClickOnSaveButton();
        }
        [TestProperty("JiraIssueID", "CDV6-15080")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has  Role Applications->  Click on Availability tab->" +
         " Click on any existing Availability record-> Select the removal option -> Click on Save")]
        [TestMethod, TestCategory("AWSReady"), TestCategory("UITest"), TestCategory("UK Care Providers Combined")]
        public void Applicant_UITestCases007()
        {

            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("Testing_CDV6_14317_01")
                .OpenApplicantRecord(_applicantId.ToString());
            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();
            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectSheduleAvailabilityType(1)
                .ClickOnSaveButton()
                .ClickExsistScheduleAvailabilty()
                .ClickOnRemoveTimeList()
                .ClickOnSaveButton();
        }
        [TestProperty("JiraIssueID", "CDV6-15112")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has Role Applications ->  Click on Availability tab " +
        "-> Check the Role Applications in Schedule Availability Tab -> Should display Schedule Availability tab with all existing Role Applications details to create Schedule Availability and existing availabilities" +
        "Check the additional options available for last week in its dropdown -> Should give below options Remove Week, Duplicate Week and Move Week to Left")]
        [TestMethod, TestCategory("AWSReady"), TestCategory("UITest"), TestCategory("UK Care Providers Combined")]
        public void Applicant_UITestCases08()
        {
            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("Testing_CDV6_14317_01")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectSheduleAvailabilityType(1)
                .ClickAddWeek()
                .ClickWeekDoggleButton(1)                 //week1 
                .ValidateDorpDownElementvisible(true, 1, 1)  //Remove Week
                .ValidateDorpDownElementvisible(true, 1, 2)  //Duplicate Week
                .ValidateDorpDownElementvisible(true, 1, 3); //Move Week Right
        }
        [TestProperty("JiraIssueID", "CDV6-15114")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has Role Applications ->  Click on Availability tab " +
            "-> Check the Role Applications in Schedule Availability Tab -> Should display Schedule Availability tab with all existing Role Applications details to create Schedule Availability and existing availabilities" +
            "Check the additional options available for last week in its dropdown -> Should give below options Remove Week, Duplicate Week and Move Week to Left")]
        [TestMethod, TestCategory("AWSReady"), TestCategory("UITest"), TestCategory("UK Care Providers Combined")]
        public void Applicant_UITestCases09()
        {

            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("Testing_CDV6_14317_01")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectSheduleAvailabilityType(1)
                .ClickAddWeek()
                .ClickWeekDoggleButton(2)                 //Week2
                .ValidateDorpDownElementvisible(true, 2, 1)  //Remove Week
                .ValidateDorpDownElementvisible(true, 2, 2)  //Duplicate Week
                .ValidateDorpDownElementvisible(true, 2, 3); //Move Week Left
        }
        [TestProperty("JiraIssueID", "CDV6-15115")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has Role Applications ->  Click on Availability tab " +
           "-> Check the Role Applications in Schedule Availability Tab -> Should display Schedule Availability tab with all existing Role Applications details to create Schedule Availability and existing availabilities" +
           "Check the additional options available for Week 1 in its dropdown -> Should give below option Duplicate Week")]
        [TestMethod, TestCategory("AWSReady"), TestCategory("UITest"), TestCategory("UK Care Providers Combined")]
        public void Applicant_UITestCases010()
        {

            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("Testing_CDV6_14317_01")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
               .WaitForApplicantSheduleAvailabilityPageToLoad()
               .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .SelectSheduleAvailabilityType(1)
               .ClickWeekDoggleButton(1)
               .ValidateElmentDoubleWeekOption(true); //Duplicate Week
        }
        [TestProperty("JiraIssueID", "CDV6-15116")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has Role Applications ->  Click on Availability tab " +
         "-> Check the Role Applications in Schedule Availability Tab -> Should display Schedule Availability tab with all existing Role Applications details to create Schedule Availability and existing availabilities" +
         "Verify the Week 1 Cycle Date -> Should be editable only when there is more than one week in the grid, otherwise non editable")]
        [TestMethod, TestCategory("AWSReady"), TestCategory("UITest"), TestCategory("UK Care Providers Combined")]
        public void Applicant_UITestCases011()
        {
            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("Testing_CDV6_14317_01")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ValidateWeek1CycleDateNonEditable()  //when there is only one week
                .ClickAddWeek()
                .ValidateWeek1CycleDateEditable(); //when there is more than one week
        }

        [TestProperty("JiraIssueID", "CDV6-15227")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has Role Applications ->  Click on Availability tab " +
        "-> Check the Role Applications in Schedule Availability Tab -> Should display Schedule Availability tab with all existing Role Applications details to create Schedule Availability and existing availabilities" +
        "Look for Create Future Schedule option -> Should display option Create Future Schedule when there is a saved availability record for current schedule")]
        [TestMethod, TestCategory("AWSReady"), TestCategory("UITest"), TestCategory("UK Care Providers Combined")]
        public void Applicant_UITestCases012()
        {
            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
               .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("Testing_CDV6_14317_01")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectSheduleAvailabilityType(1)
                .ClickAddWeek()
                .ValidateCreateFeatureScheduleTap(true);
        }

        #endregion
    }
}



