using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;


namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// This class contains Automated UI test scripts for
    /// </summary>
    [TestClass]
    public class Applicant_UITestCases2 : FunctionalTest
    {

        #region Properties

        private string _environmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _environmentId;
        private Guid _applicantId;
        private Guid _roleApplication;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _recurrencePatternId;
        private Guid _recurrencePatternId_every2Weeks;
        private Guid _transportTypeId_Car;
        private Guid _transportTypeId_Walking;
        private string _currentDayOfTheWeek;
        private string _loginUsername;

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                _environmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion

                #region Default User

                var userid = dbHelper.systemUser.GetSystemUserByUserName("administrator").FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);

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

                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("test_user_CDV6_15656").Any();
                if (!defaultLoginUserExists)
                {
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser("test_user_CDV6_15656", "Test_User", "CDV6_15656", "test user CDV6 15656", "Passw0rd_!", "test_user_CDV6_15656@somemail.com", "test_user_CDV6_15656@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();
                    var staffReviewsMyRecordsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Org Edit)").First();

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsMyRecordsSecurityProfileId);
                }

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName("test_user_CDV6_15656").FirstOrDefault();
                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];
                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

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

                #region Recurrence pattern

                _currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

                _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on " + _currentDayOfTheWeek.ToLower()).FirstOrDefault();
                _recurrencePatternId_every2Weeks = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on " + _currentDayOfTheWeek.ToLower()).FirstOrDefault();
                #endregion

                #region TransportType

                var transportTypeExists = dbHelper.transportType.GetTransportTypeByName("Car").Any();
                if (!transportTypeExists)
                    dbHelper.transportType.CreateTransportType(_careProviders_TeamId, "Car", DateTime.Now.Date, 1, "50", 1);
                _transportTypeId_Car = dbHelper.transportType.GetTransportTypeByName("Car")[0];

                transportTypeExists = dbHelper.transportType.GetTransportTypeByName("Walking").Any();
                if (!transportTypeExists)
                    dbHelper.transportType.CreateTransportType(_careProviders_TeamId, "Walking", DateTime.Now.Date, 1, "6", 4);
                _transportTypeId_Walking = dbHelper.transportType.GetTransportTypeByName("Walking")[0];

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-15656

        [TestProperty("JiraIssueID", "ACC-3387")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases001()
        {
            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);


            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();


            System.Threading.Thread.Sleep(3000);

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickAddWeekButton();

            System.Threading.Thread.Sleep(3000);

            applicantScheduleTransportSubPage
                .ClickWeekExpandButton(1)
                .ValidateRemoveWeekButtonVisibility(1, true)
                .ValidateDuplicateWeekButtonVisibility(1, true)
                .ValidateMoveWeekRightButtonVisibility(1, true)
                .ValidateMoveWeekLeftButtonVisibility(1, false);

            applicantScheduleTransportSubPage
                .ClickWeekExpandButton(2)
                .ValidateRemoveWeekButtonVisibility(2, true)
                .ValidateDuplicateWeekButtonVisibility(2, true)
                .ValidateMoveWeekRightButtonVisibility(2, false)
                .ValidateMoveWeekLeftButtonVisibility(2, true);

        }

        [TestProperty("JiraIssueID", "ACC-3388")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases002()
        {
            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);


            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad();

            applicantScheduleTransportSubPage
                .ClickWeekExpandButton(1)

                .ValidateRemoveWeekButtonVisibility(1, false)
                .ValidateDuplicateWeekButtonVisibility(1, true)
                .ValidateMoveWeekRightButtonVisibility(1, false)
                .ValidateMoveWeekLeftButtonVisibility(1, false)

                .ClickDuplicateWeekButton(1);

            applicantScheduleTransportSubPage
                .ClickWeekExpandButton(2)
                .ValidateRemoveWeekButtonVisibility(2, true)
                .ValidateDuplicateWeekButtonVisibility(2, true)
                .ValidateMoveWeekRightButtonVisibility(2, false)
                .ValidateMoveWeekLeftButtonVisibility(2, true);

        }

        [TestProperty("JiraIssueID", "ACC-3389")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases003()
        {
            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);


            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()

                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()

                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()

                .ValidateAddWeekButtonVisibility(false);

        }

        [TestProperty("JiraIssueID", "ACC-3390")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases004()
        {
            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ValidateWeek1CycleDateReadonly(true);

        }

        [TestProperty("JiraIssueID", "ACC-3391")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases005()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            var _userTransportationScheduleid = dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), _recurrencePatternId, _transportTypeId_Car, 1);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();

            System.Threading.Thread.Sleep(2000);

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .DragTransportAvailabilityToLeft(currentDayOfTheWeek, 2)
                .ClickOnSaveButton();

            System.Threading.Thread.Sleep(6000);

            var fields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(_userTransportationScheduleid, "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(8, 45, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);
        }

        [TestProperty("JiraIssueID", "ACC-3392")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases006()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            var _userTransportationScheduleid = dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), _recurrencePatternId, _transportTypeId_Car, 1);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickWeekExpandButton(1)
                .ValidateRemoveWeekButtonVisibility(1, false)
                .ValidateDuplicateWeekButtonVisibility(1, true)
                .ValidateMoveWeekLeftButtonVisibility(1, false)
                .ValidateMoveWeekRightButtonVisibility(1, false);
        }

        [TestProperty("JiraIssueID", "ACC-3393")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases007()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);

            dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), _recurrencePatternId_every2Weeks, _transportTypeId_Car, 1);
            dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date.AddDays(7), new TimeSpan(9, 0, 0), new TimeSpan(17, 45, 0), _recurrencePatternId_every2Weeks, _transportTypeId_Car, 2);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            System.Threading.Thread.Sleep(3000);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            System.Threading.Thread.Sleep(3000);

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();

            System.Threading.Thread.Sleep(3000);

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()

                .ClickWeekExpandButton(2)
                .ValidateRemoveWeekButtonVisibility(2, true)
                .ValidateDuplicateWeekButtonVisibility(2, true)
                .ValidateMoveWeekLeftButtonVisibility(2, true)
                .ValidateMoveWeekRightButtonVisibility(2, false);

        }

        [TestProperty("JiraIssueID", "ACC-3394")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases008()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);

            dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), _recurrencePatternId_every2Weeks, _transportTypeId_Car, 1);
            dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date.AddDays(7), new TimeSpan(9, 0, 0), new TimeSpan(17, 45, 0), _recurrencePatternId_every2Weeks, _transportTypeId_Car, 2);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()

                .ClickWeekExpandButton(1)
                .ValidateRemoveWeekButtonVisibility(1, true)
                .ValidateDuplicateWeekButtonVisibility(1, true)
                .ValidateMoveWeekLeftButtonVisibility(1, false)
                .ValidateMoveWeekRightButtonVisibility(1, true);
        }

        [TestProperty("JiraIssueID", "ACC-3395")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases009()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);

            var _userTransportationScheduleid1 = dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), _recurrencePatternId, _transportTypeId_Car, 1);
            var _userTransportationScheduleid2 = dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date.AddDays(7), new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _recurrencePatternId, _transportTypeId_Walking, 1);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .DragTransportAvailabilityToLeft(_currentDayOfTheWeek, 4) //drag the "09:00 - 17:00" transport to the left
                .ClickOnSaveButton();

            availabilityErrorDialogPopup
                .WaitForAvailabilityErrorDialogPopupPageToLoad()
                .ValidateDialogText("Data sent allows no transportation schedules to be created or updated.")
                .ClickOnCloseButton();

            var fields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(_userTransportationScheduleid1, "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);

            fields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(_userTransportationScheduleid2, "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(6, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["endtime"]);

        }

        [TestProperty("JiraIssueID", "ACC-3396")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases010()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);

            var _userTransportationScheduleid1 = dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), _recurrencePatternId, _transportTypeId_Car, 1);
            var _userTransportationScheduleid2 = dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date.AddDays(7), new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _recurrencePatternId, _transportTypeId_Car, 1);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .DragTransportAvailabilityToLeft(_currentDayOfTheWeek, 4) //drag the "09:00 - 17:00" transport to the left
                .ClickOnSaveButton()
                .WaitForApplicantScheduleTransportSubPagePageToLoad();

            Thread.Sleep(4000);

            var transportationSchedules = dbHelper.userTransportationSchedule.GetByApplicantID(_applicantId);
            Assert.AreEqual(1, transportationSchedules.Count);

            var fields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(transportationSchedules[0], "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(6, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);

        }

        [TestProperty("JiraIssueID", "ACC-3397")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases011()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDayOfTheWeek, 1);

            applicantSelectScheduledTransportPopup
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickCarButton();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickOnSaveButton();

            Thread.Sleep(2000);

            var transportationSchedules = dbHelper.userTransportationSchedule.GetByApplicantID(_applicantId);
            Assert.AreEqual(1, transportationSchedules.Count);

            var fields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(transportationSchedules[0],
                "title", "inactive", "startdate", "starttime", "enddate", "endtime", "recurrencepatternid", "transporttypeid", "weeknumber", "adhoc");

            Assert.AreEqual(DateTime.Now.Date, fields["startdate"]);
            Assert.AreEqual(false, fields.ContainsKey("enddate"));
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);
            Assert.AreEqual("AutoGenerated", fields["title"]);
            Assert.AreEqual(false, fields["inactive"]);
            Assert.AreEqual(_recurrencePatternId.ToString(), fields["recurrencepatternid"].ToString());
            Assert.AreEqual(_transportTypeId_Car.ToString(), fields["transporttypeid"].ToString());
            Assert.AreEqual(1, fields["weeknumber"]);
            Assert.AreEqual(false, fields["adhoc"]);
        }

        [TestProperty("JiraIssueID", "ACC-3398")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases012()
        {
            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), _recurrencePatternId, _transportTypeId_Car, 1);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickTransportAvailabilitySlot(DateTime.Now.ToString("dd'/'MM'/'yyyy"), 2);

            applicantSelectScheduledTransportPopup
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickRemoveTimeSlotButton();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickOnSaveButton()
                .WaitForApplicantScheduleTransportSubPagePageToLoad();

            Thread.Sleep(3000);

            var transportationSchedules = dbHelper.userTransportationSchedule.GetByApplicantID(_applicantId);
            Assert.AreEqual(0, transportationSchedules.Count);
        }

        [TestProperty("JiraIssueID", "ACC-3399")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases013()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            var firstName = "Testing_CDV6_15171";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            var _userTransportationScheduleId = dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), _recurrencePatternId, _transportTypeId_Car, 1);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(lastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickScheduleTransportTab();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDayOfTheWeek, 2);

            System.Threading.Thread.Sleep(4000);

            applicantSelectScheduledTransportPopup
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickWalkingButton();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickOnSaveButton()
                .WaitForApplicantScheduleTransportSubPagePageToLoad();

            Thread.Sleep(4000);

            var fields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(_userTransportationScheduleId, "title", "inactive", "startdate", "starttime", "enddate", "endtime", "recurrencepatternid", "transporttypeid", "weeknumber", "adhoc");

            Assert.AreEqual(DateTime.Now.Date, fields["startdate"]);
            Assert.AreEqual(false, fields.ContainsKey("enddate"));
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);
            Assert.AreEqual("AutoGenerated", fields["title"]);
            Assert.AreEqual(false, fields["inactive"]);
            Assert.AreEqual(_recurrencePatternId.ToString(), fields["recurrencepatternid"].ToString());
            Assert.AreEqual(_transportTypeId_Walking.ToString(), fields["transporttypeid"].ToString());
            Assert.AreEqual(1, fields["weeknumber"]);
            Assert.AreEqual(false, fields["adhoc"]);
        }

        #endregion

    }
}



