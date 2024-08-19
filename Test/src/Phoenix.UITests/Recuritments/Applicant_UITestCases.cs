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
        private string _applicantFirstName;
        private string _applicantLastName;
        private Guid _careProviderStaffRoleTypeid;
        public Guid environmentid;
        private Guid _roleApplicant;
        private Guid _availabilityTypeId;
        private string _loginUsername;
        private Guid _recurrencePatternId;
        private Guid _recurrencePatternId_every2Weeks;
        private string _currentDayOfTheWeek;
        private Guid _availabilityTypeId1;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

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

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);
                }

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_1").FirstOrDefault();

                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];
                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                #endregion

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

                _applicantFirstName = "Testing_CDV6_14317_01";
                _applicantLastName = "User_01_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                _applicantId = dbHelper.applicant.CreateApplicant(_applicantFirstName, _applicantLastName, _careProviders_TeamId);

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

                _roleApplicant = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);

                #endregion

                #region Availability Types

                var availabilityTypeID = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Regular").Any();
                if (!availabilityTypeID)
                {
                    _availabilityTypeId = dbHelper.availabilityTypes.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

                }
                if (_availabilityTypeId == Guid.Empty)
                {
                    _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Regular")[0];
                }

                var availabilityTypeID1 = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").Any();
                if (!availabilityTypeID1)
                {
                    _availabilityTypeId1 = dbHelper.availabilityTypes.CreateAvailabilityType("Salaried/Contracted", 1, false, _careProviders_TeamId, 1, 1, true);

                }
                if (_availabilityTypeId1 == Guid.Empty)
                {
                    _availabilityTypeId1 = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted")[0];
                }

                var availabilityTypeID3 = dbHelper.availabilityTypes.GetAvailabilityTypeByName("OverTime").Any();
                if (!availabilityTypeID3)
                {
                    dbHelper.availabilityTypes.CreateAvailabilityType("OverTime", 4, false, _careProviders_TeamId, 1, 1, true);
                }


                #endregion

                #region Recurrence pattern

                _currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

                _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on " + _currentDayOfTheWeek.ToLower()).FirstOrDefault();
                _recurrencePatternId_every2Weeks = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 week(s) on " + _currentDayOfTheWeek.ToLower()).FirstOrDefault();
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

        #region https://advancedcsg.atlassian.net/browse/CDV6-14317

        [TestProperty("JiraIssueID", "ACC-3372")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has no Role Applications->  Click on Availability tab" +
            "Should not display Schedule Availability tab and should give appropriate error message to say the selected applicant doesn't have Role Applications")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases001()
        {
            var applicantFirstName = "Testing_CDV6_15832";
            var applicantLastName = "User_02_" + DateTime.Now.Date.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            System.Threading.Thread.Sleep(2000);

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ValidateScheduleAvailabiltyTabVisibility(false)
                .ValidateNoApplicationRecordPopUpVisibile(true);

        }

        [TestProperty("JiraIssueID", "ACC-3373")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has  Role Applications->  Click on Availability tab" +
            "Should display Schedule Availability tab and should give all existing Role Applications details to create Schedule Availability")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases002()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ValidateScheduleAvailabiltyTabVisibility(true)
                .ValidateNoApplicationRecordPopUpVisibile(false);
        }

        [TestProperty("JiraIssueID", "ACC-3374")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has  Role Applications->  Click on Availability tab" +
            "Should display Schedule Availability tab and should give all existing Role Applications details to create Schedule Availability-> Click on Create Option for any Role Application in any day" +
            "Should give option to select availability as below Standard,Over Time & Regular -> Select standard -> Click Save -> Record should be saved successfully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases003()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Salaried/Contracted");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton();
        }

        [TestProperty("JiraIssueID", "ACC-3375")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has  Role Applications->  Click on Availability tab" +
       "Should display Schedule Availability tab and should give all existing Role Applications details to create Schedule Availability-> Click on Create Option for any Role Application in any day" +
            "Should give option to select availability as below Standard,Over Time & Regular -> Over Time -> Click Save -> Record should be saved successfully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases004()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("OverTime");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton();
        }


        [TestProperty("JiraIssueID", "ACC-3376")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has  Role Applications->  Click on Availability tab" +
        "Should display Schedule Availability tab and should give all existing Role Applications details to create Schedule Availability-> Click on Create Option for any Role Application in any day" +
             "Should give option to select availability as below Standard,Over Time & Regular ->  Regular -> Click Save -> Record should be saved successfully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases005()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Regular");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton();
        }

        [TestProperty("JiraIssueID", "ACC-3377")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has  Role Applications->  Click on Availability tab" +
            "Should display Schedule Availability tab and should give all existing Role Applications details to create Schedule Availability-> Click on Create Option for any Role Application in any day" +
            "Should give option to select availability as below Standard,Over Time & Regular ->  Select any other availability  type other than its own availability  " +
            "Existing availability should be modified to newly selected availability type -> Click Save -> Record should be saved successfully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs"), TestCategory("Danish_Testing")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases006()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Salaried/Contracted");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton()
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickExsistScheduleAvailabilty();

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("OverTime");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton();
        }

        [TestProperty("JiraIssueID", "ACC-3378")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has  Role Applications->  Click on Availability tab->" +
         " Click on any existing Availability record-> Select the removal option -> Click on Save")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases007()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Salaried/Contracted");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickExsistScheduleAvailabilty();

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .ClickRemoveTimeSlotButton();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton();

        }

        [TestProperty("JiraIssueID", "ACC-3379")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has Role Applications ->  Click on Availability tab " +
        "-> Check the Role Applications in Schedule Availability Tab -> Should display Schedule Availability tab with all existing Role Applications details to create Schedule Availability and existing availabilities" +
        "Try to link the same availability type and same role application records by dragging the availability record to other. -> Both records should be combined together.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs"), TestCategory("Danish_Testing")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases_01()
        {
            Guid availability = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted")[0];

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Salaried/Contracted");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDateDuplicate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Salaried/Contracted");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .DragAvailabilityToLeft(commonMethodsHelper.GetCurrentDate(), 4, 2)
                .ClickOnSaveButton();

            Thread.Sleep(2000);

            var AvailabilitySchedules = dbHelper.userWorkSchedule.GetByApplicantID(_applicantId);
            Assert.AreEqual(1, AvailabilitySchedules.Count);

            var fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByApplicantID(AvailabilitySchedules[0],
              "title", "inactive", "startdate", "starttime", "enddate", "endtime", "recurrencepatternid", "availabilitytypesid", "weeknumber", "adhoc");

            Assert.AreEqual(DateTime.Now.Date, fields["startdate"]);
            Assert.AreEqual(false, fields.ContainsKey("enddate"));
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(23, 30, 0), fields["endtime"]);
            Assert.AreEqual("AutoGenerated", fields["title"]);
            Assert.AreEqual(false, fields["inactive"]);
            Assert.AreEqual(availability.ToString(), fields["availabilitytypesid"].ToString());
            Assert.AreEqual(1, fields["weeknumber"]);
            Assert.AreEqual(false, fields["adhoc"]);

        }

        [TestProperty("JiraIssueID", "ACC-3380")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has Role Applications ->  Click on Availability tab " +
        "-> Check the Role Applications in Schedule Availability Tab -> Should display Schedule Availability tab with all existing Role Applications details to create Schedule Availability and existing availabilities" +
        "Try to link the different availability type and same role application records by dragging the availability record to other.->Should not allow combine together as both records belongs to different availability types.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases_02()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Salaried/Contracted");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDateDuplicate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Regular");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .DragAvailabilityToLeft(commonMethodsHelper.GetCurrentDate(), 4, 2)
                .ClickOnSaveButton();

            Thread.Sleep(6000);

            var AvailabilitySchedules = dbHelper.userWorkSchedule.GetByApplicantID(_applicantId);
            Assert.AreEqual(2, AvailabilitySchedules.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3381")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has Role Applications ->  Click on Availability tab " +
        "-> Check the Role Applications in Schedule Availability Tab -> Should display Schedule Availability tab with all existing Role Applications details to create Schedule Availability and existing availabilities" +
        "Check the additional options available for last week in its dropdown -> Should give below options Remove Week, Duplicate Week and Move Week to Left")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases08()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Salaried/Contracted");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickAddWeek()
                .ClickWeekDoggleButton(1)                 //week1 
                .ValidateDorpDownElementvisible(true, 1, 1)  //Remove Week
                .ValidateDorpDownElementvisible(true, 1, 2)  //Duplicate Week
                .ValidateDorpDownElementvisible(true, 1, 3); //Move Week Right
        }

        [TestProperty("JiraIssueID", "ACC-3382")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has Role Applications ->  Click on Availability tab " +
            "-> Check the Role Applications in Schedule Availability Tab -> Should display Schedule Availability tab with all existing Role Applications details to create Schedule Availability and existing availabilities" +
            "Check the additional options available for last week in its dropdown -> Should give below options Remove Week, Duplicate Week and Move Week to Left")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases09()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Salaried/Contracted");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickAddWeek()
                .ClickWeekDoggleButton(2)                 //Week2
                .ValidateDorpDownElementvisible(true, 2, 1)  //Remove Week
                .ValidateDorpDownElementvisible(true, 2, 2)  //Duplicate Week
                .ValidateDorpDownElementvisible(true, 2, 3); //Move Week Left
        }

        [TestProperty("JiraIssueID", "ACC-3383")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has Role Applications ->  Click on Availability tab " +
           "-> Check the Role Applications in Schedule Availability Tab -> Should display Schedule Availability tab with all existing Role Applications details to create Schedule Availability and existing availabilities" +
           "Check the additional options available for Week 1 in its dropdown -> Should give below option Duplicate Week")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases010()
        {

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Salaried/Contracted");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickWeekDoggleButton(1)
                .ValidateElmentDoubleWeekOption(true); //Duplicate Week
        }

        [TestProperty("JiraIssueID", "ACC-3384")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has Role Applications ->  Click on Availability tab " +
          "-> Check the Role Applications in Schedule Availability Tab -> Should display Schedule Availability tab with all existing Role Applications details to create Schedule Availability and existing availabilities" +
         "Verify the Week 1 Cycle Date -> Should be editable only when there is more than one week in the grid, otherwise non editable")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases011()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
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

        [TestProperty("JiraIssueID", "ACC-3385")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant  who has Role Applications ->  Click on Availability tab " +
         "-> Check the Role Applications in Schedule Availability Tab -> Should display Schedule Availability tab with all existing Role Applications details to create Schedule Availability and existing availabilities" +
        "Look for Create Future Schedule option -> Should display option Create Future Schedule when there is a saved availability record for current schedule")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases012()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Salaried/Contracted");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickAddWeek()
                .ValidateCreateFeatureScheduleTap(true);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-14544

        [TestProperty("JiraIssueID", "ACC-3386")]
        [Description("Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Select existing applicant->  Click on Availability tab" +
            "New tab called Availability should be displayed .")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void VerifyAvailabiltyInApplicant_UITestCases001()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(_applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ValidateScheduleAvailabiltyTabVisibility(true);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3049

        [TestProperty("JiraIssueID", "ACC-3609")]
        [Description("1> & 2> Login CD as a Care Provider  -> Work Place  -> Recruitment -> Applicants -> Create New Applicant Record " +
            "3> Create New Applicant and Enter Same First Name & Last Name and click on Save and Validate Alert message." +
            "4> Click on Cancel button on alert message and validate record should not be saved." +
            "5> Enter Same First Name & Last Name and click on save. In the alert popup Click on OK button. Validate record should be saved with same name." +
            "6> Open Existing Applicant and Create New Aliases. Again Create new applicant with First Name and Last name same as Aliases and Validate Alert message should be display." +
            "7> Create New Applicant with First Name & Last Name same a System User and Validate Alert message should be display.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Aliases")]
        public void Applicant_Duplicate_UITestCases01()
        {
            var FirstName = "ACC_3609_" + _currentDateSuffix;
            var LastName = "Applicant_" + _currentDateSuffix;

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToApplicantsPage();

            applicantPage
               .WaitForApplicantsPageToLoad()
               .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName(FirstName)
                .InsertLastName(LastName)
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(1000);
            Guid ApplicantId = dbHelper.applicant.GetByFirstName(FirstName).FirstOrDefault();

            applicantPage
               .WaitForApplicantsPageToLoad()
               .SearchApplicantRecord(LastName)
               .ValidateApplicantRecordIsPresent(ApplicantId.ToString());

            #endregion

            #region Step 3 & 4

            applicantPage
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName(FirstName)
                .InsertLastName(LastName)
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Potential duplicates have been identified with First Name and Last Name. Please select ‘Ok’ to create a new Applicant.")
                .TapCancelButton();

            System.Threading.Thread.Sleep(1000);
            var Applicants = dbHelper.applicant.GetByFirstName(FirstName);
            Assert.AreEqual(1, Applicants.Count);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 5

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName(FirstName)
                .InsertLastName(LastName)
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Potential duplicates have been identified with First Name and Last Name. Please select ‘Ok’ to create a new Applicant.")
                .TapOKButton();

            System.Threading.Thread.Sleep(5000);
            Applicants = dbHelper.applicant.GetByFirstName(FirstName);
            Assert.AreEqual(2, Applicants.Count);

            #endregion

            #region Step 6

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidatePageHeaderTitle(FirstName + " " + LastName);

            applicantRecordPage
                .WaitForApplicantRecordPageSubAreaToLoad("Applicant Aliases")
                .ClickApplicantRecordPageSubArea_NewRecordButton();

            var Aliase_FirstName = "ACC_Aliase_" + _currentDateSuffix;
            var Aliase_LastName = "3609_" + _currentDateSuffix;

            applicantAliasRecordPage
                .WaitForApplicantAliasRecordPageToLoad()
                .InsertFirstName(Aliase_FirstName)
                .InsertLastName(Aliase_LastName)
                .ClickSaveAndCloseButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ClickBackButton();

            applicantPage
               .WaitForApplicantsPageToLoad()
               .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName(Aliase_FirstName)
                .InsertLastName(Aliase_LastName)
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Potential duplicates have been identified with First Name and Last Name. Please select ‘Ok’ to create a new Applicant.")
                .TapCancelButton();

            #endregion

            #region Step 7

            var SystemUser_FirstName = "ACC_3609_" + _currentDateSuffix;
            var SystemUser_LastName = "User_" + _currentDateSuffix;
            dbHelper.systemUser.CreateSystemUser(SystemUser_FirstName, SystemUser_FirstName, SystemUser_LastName, SystemUser_FirstName + " " + SystemUser_LastName, "Passw0rd_!", SystemUser_FirstName + "@somemail.com", SystemUser_FirstName + "@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName(SystemUser_FirstName)
                .InsertLastName(SystemUser_LastName)
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Potential duplicates have been identified with First Name and Last Name. Please select ‘Ok’ to create a new Applicant.")
                .TapCancelButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3610")]
        [Description("8> Navigate to Advanced search and check that Recruitment Applicant Business Object is present in list of Record Type. " +
                        "Select First Name contains data and get the result set. Click on + button and repeat steps 2 to 7 ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Advanced Search")]
        public void Applicant_Duplicate_UITestCases02()
        {
            var FirstName = "ACC_3610_" + _currentDateSuffix;
            var LastName = "Applicant_" + _currentDateSuffix;

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Applicants")
                .SelectFilter("1", "First Name")
                .SelectOperator("1", "Contains Data")
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Last Name")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", LastName)
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();

            #endregion

            #region Steps 2 to 7 from Advanced Search

            applicantRecordPage
                .WaitForApplicantRecordPageToLoadFromAdvancedSearch()
                .InsertFirstName(FirstName)
                .InsertLastName(LastName)
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(1000);

            Guid ApplicantId = dbHelper.applicant.GetByFirstName(FirstName).FirstOrDefault();

            advanceSearchPage
               .WaitForResultsPageToLoad()
               .ValidateSearchResultRecordPresent(ApplicantId.ToString())
               .ClickNewRecordButton_ResultsPage();

            applicantRecordPage
                .WaitForApplicantRecordPageToLoadFromAdvancedSearch()
                .InsertFirstName(FirstName)
                .InsertLastName(LastName)
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Potential duplicates have been identified with First Name and Last Name. Please select ‘Ok’ to create a new Applicant.")
                .TapCancelButton();

            System.Threading.Thread.Sleep(1000);

            var Applicants = dbHelper.applicant.GetByFirstName(FirstName);
            Assert.AreEqual(1, Applicants.Count);

            applicantRecordPage
                .WaitForApplicantRecordPageToLoadFromAdvancedSearch()
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Potential duplicates have been identified with First Name and Last Name. Please select ‘Ok’ to create a new Applicant.")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            Applicants = dbHelper.applicant.GetByFirstName(FirstName);
            Assert.AreEqual(2, Applicants.Count);

            applicantRecordPage
                .WaitForApplicantRecordPageToLoadFromAdvancedSearch()
                .ValidatePageHeaderTitle(FirstName + " " + LastName);

            applicantRecordPage
                .WaitForApplicantAliasAreaToLoad()
                .ClickApplicantRecordPageSubArea_NewRecordButton();

            var Aliase_FirstName = "ACC_Aliase_" + _currentDateSuffix;
            var Aliase_LastName = "3610_" + _currentDateSuffix;

            applicantAliasRecordPage
                .WaitForApplicantAliasRecordPageToLoad()
                .InsertFirstName(Aliase_FirstName)
                .InsertLastName(Aliase_LastName)
                .ClickSaveAndCloseButton();

            applicantRecordPage
                .WaitForApplicantRecordPageToLoadFromAdvancedSearch()
                .ClickBackButton();

            advanceSearchPage
               .WaitForResultsPageToLoad()
               .ClickNewRecordButton_ResultsPage();

            applicantRecordPage
                .WaitForApplicantRecordPageToLoadFromAdvancedSearch()
                .InsertFirstName(Aliase_FirstName)
                .InsertLastName(Aliase_LastName)
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Potential duplicates have been identified with First Name and Last Name. Please select ‘Ok’ to create a new Applicant.")
                .TapCancelButton();

            var SystemUser_FirstName = "ACC_3610_" + _currentDateSuffix;
            var SystemUser_LastName = "User_" + _currentDateSuffix;
            dbHelper.systemUser.CreateSystemUser(SystemUser_FirstName, SystemUser_FirstName, SystemUser_LastName, SystemUser_FirstName + " " + SystemUser_LastName, "Passw0rd_!", SystemUser_FirstName + "@somemail.com", SystemUser_FirstName + "@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true);

            applicantRecordPage
                .WaitForApplicantRecordPageToLoadFromAdvancedSearch()
                .InsertFirstName(SystemUser_FirstName)
                .InsertLastName(SystemUser_LastName)
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Potential duplicates have been identified with First Name and Last Name. Please select ‘Ok’ to create a new Applicant.")
                .TapCancelButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3656

        [TestProperty("JiraIssueID", "ACC-1164")]
        [Description("test steps for original test method ACC-1164")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Worker Contracts")]
        [TestProperty("Screen1", "Applicant Dashboard")]
        [TestProperty("Screen2", "System User Employment Contracts")]
        [TestProperty("Screen3", "Role Applications")]
        public void Applicant_ACC_1164_UITestCases01()
        {
            #region Applicant

            var applicantFirstName = "ACC_1164_" + _currentDateSuffix;
            var applicantLastName = "User_01_" + _currentDateSuffix;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);
            var _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            #region Employment Contract Type

            if (!dbHelper.employmentContractType.GetByName("Contracted").Any())
                dbHelper.employmentContractType.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            var _employmentContractTypeid = dbHelper.employmentContractType.GetByName("Contracted").FirstOrDefault();

            #endregion

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);
            var _roleApplicationName = (dbHelper.recruitmentRoleApplicant.GetRecruitmentRoleApplicantByID(_roleApplicationID, "name")["name"]).ToString();

            #endregion

            #region Complete All Requirements for the Applicant

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);

            foreach (Guid complianceId in complianceItems)
                dbHelper.compliance.UpdateCompliance(complianceId, 1593, "Test", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));

            #endregion

            #region Step 1 to 6

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToApplicantsPage();

            applicantPage
               .WaitForApplicantsPageToLoad()
               .SearchApplicantRecord(applicantFirstName)
               .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ClickProceedToInductionButton(1);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            var systemUserId = new Guid((dbHelper.applicant.GetApplicantByID(_applicantId, "systemuserid")["systemuserid"]).ToString());
            var systemUserEmploymentContracts = dbHelper.systemUserEmploymentContract.GetBySystemUserId(systemUserId);
            Assert.AreEqual(1, systemUserEmploymentContracts.Count);
            var systemUserEmploymentContractId = systemUserEmploymentContracts[0];

            systemUserEmploymentContractsPage
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateRecruitmentRoleApplicationLinkFieldText(_roleApplicationName)
                .ClickRecruitmentRoleApplicationLinkButton();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidatRoleApplicationRecordPageTitle(_roleApplicationName);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3657

        [TestProperty("JiraIssueID", "ACC-1175")]
        [Description("test steps for original test method ACC-1175")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Availability")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        [TestProperty("Screen3", "Applicant Dashboard")]
        public void Applicant_ACC_1175_UITestCases01()
        {
            var applicantFirstName = "ACC_1175_" + _currentDateSuffix;
            var applicantLastName = "User_01_" + _currentDateSuffix;
            var _applicantFullName = applicantFirstName + " " + applicantLastName;

            #region Employment Contract Type

            if (!dbHelper.employmentContractType.GetByName("Contracted").Any())
                dbHelper.employmentContractType.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            var _employmentContractTypeid = dbHelper.employmentContractType.GetByName("Contracted").FirstOrDefault();

            #endregion

            #region Steps 1 to 8

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToApplicantsPage();

            applicantPage
               .WaitForApplicantsPageToLoad()
               .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName(applicantFirstName)
                .InsertLastName(applicantLastName)
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            #region Role Application & Complete All Requirements for the Applicant

            _applicantId = dbHelper.applicant.GetByFirstName(applicantFirstName).FirstOrDefault();

            dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            foreach (Guid complianceId in complianceItems)
                dbHelper.compliance.UpdateCompliance(complianceId, 1593, "Test", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));

            #endregion

            applicantRecordPage
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateAttributeTitleAndValueText(5, "Availability collected", "No");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToAvailabilityTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Regular");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton();

            System.Threading.Thread.Sleep(2000);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateAttributeTitleAndValueText(5, "Availability collected", "Yes");

            #endregion
        }

        #endregion

    }
}



