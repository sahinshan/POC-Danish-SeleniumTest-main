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
    public class Applicant_UITestCases3 : FunctionalTest
    {

        #region Properties

        private string _environmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _LoginUserIDwithnoEmploymentcontract;
        private Guid _environmentId;
        private Guid _applicantId;
        private Guid _roleApplication;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _recurrencePatternId;
        private Guid _recurrencePatternId_every2Weeks;
        private Guid _transportTypeId_Car;
        private Guid _transportTypeId_Walking;
        private string _currentDayOfTheWeek;
        private Guid _availabilityTypeId;
        private Guid _availabilityTypeId_Regular;
        private Guid _availabilityTypeId_overtime;
        private Guid _UserScheduleIdTypeId;
        private Guid _UserDiaryId;
        private Guid _employmentContractId;
        private Guid _employmentContractTypeid;
        private Guid _staffreviewrequirementsids;
        private string _loginUsername;
        private string _loginUsernamewithnoEmploymnetContract;

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

                _loginUsername = "test_user_CDV6_15656";
                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("test_user_CDV6_15656").Any();
                if (!defaultLoginUserExists)
                {
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser("test_user_CDV6_15656", "Test_User", "CDV6_15656", "test user CDV6 15656", "Passw0rd_!", "test_user_CDV6_15656@somemail.com", "test_user_CDV6_15656@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();
                    var staffReviewsMyRecordsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Org Edit)").First();

                    //foreach (var userSecProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_defaultLoginUserID))
                    //    dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecProfileId);

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsMyRecordsSecurityProfileId);
                }

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName("test_user_CDV6_15656").FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                #endregion

                #region user with no employment contract

                _loginUsernamewithnoEmploymnetContract = "test_user_CDV6_208471";
                var defaultLoginUserExists1 = dbHelper.systemUser.GetSystemUserByUserName("test_user_CDV6_208471").Any();
                if (!defaultLoginUserExists1)
                {
                    _LoginUserIDwithnoEmploymentcontract = dbHelper.systemUser.CreateSystemUser("test_user_CDV6_208471", "Test_User", "CDV6_208471", "test user CDV6 208471", "Passw0rd_!", "test_user_CDV6_20847@somemail.com", "test_user_CDV6_208471@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();
                    var staffReviewsMyRecordsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Org Edit)").First();

                    //foreach (var userSecProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_LoginUserIDwithnoEmploymentcontract))
                    //    dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecProfileId);

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_LoginUserIDwithnoEmploymentcontract, staffReviewsTeamEditSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_LoginUserIDwithnoEmploymentcontract, staffReviewsMyRecordsSecurityProfileId);
                }

                if (Guid.Empty == _LoginUserIDwithnoEmploymentcontract)
                    _LoginUserIDwithnoEmploymentcontract = dbHelper.systemUser.GetSystemUserByUserName("test_user_CDV6_208471").FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_LoginUserIDwithnoEmploymentcontract, DateTime.Now.Date);

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

                #region Availability Types

                var availabilityTypeID = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").Any();
                if (!availabilityTypeID)
                {
                    _availabilityTypeId = dbHelper.availabilityTypes.CreateAvailabilityType("Salaried/Contracted", 1, false, _careProviders_TeamId, 1, 1, true);

                }
                if (_availabilityTypeId == Guid.Empty)
                {
                    _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted")[0];
                }

                var _availabilityTypeId1 = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Regular").Any();
                if (!_availabilityTypeId1)
                {
                    _availabilityTypeId_Regular = dbHelper.availabilityTypes.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

                }
                if (_availabilityTypeId_Regular == Guid.Empty)
                {
                    _availabilityTypeId_Regular = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Regular")[0];
                }

                var availabilityTypeID3 = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Hourly/Overtime").Any();
                if (!availabilityTypeID3)
                {
                    _availabilityTypeId_overtime = dbHelper.availabilityTypes.CreateAvailabilityType("Hourly/Overtime", 52, false, _careProviders_TeamId, 1, 1, true);

                }
                if (_availabilityTypeId_overtime == Guid.Empty)
                {
                    _availabilityTypeId_overtime = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Hourly/Overtime")[0];
                }




                #endregion

                #region Care provider staff review type

                var careProviderStaffReviewRequirementTypeExists = dbHelper.staffReviewRequirement.GetByName("Staff Appraisal").Any();
                if (!careProviderStaffReviewRequirementTypeExists)
                {
                    //  _staffreviewrequirementsids = dbHelper.staffReviewRequirement.CreateStaffReviewRequirement(_careProviders_TeamId, "Staff Appraisal",  DateTime.Now);
                }
                if (_staffreviewrequirementsids == Guid.Empty)
                {
                    _staffreviewrequirementsids = dbHelper.staffReviewRequirement.GetByName("Staff Appraisal").FirstOrDefault();
                }
                #endregion

                #region Employment Contract Type

                var employmentContractTypeExists = dbHelper.employmentContractType.GetByName("Employment Contract Type 13622").Any();
                if (!employmentContractTypeExists)
                {
                    _employmentContractTypeid = dbHelper.employmentContractType.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 13622", "2", null, new DateTime(2020, 1, 1));
                }
                if (_employmentContractTypeid == Guid.Empty)
                {
                    _employmentContractTypeid = dbHelper.employmentContractType.GetByName("Employment Contract Type 13622").FirstOrDefault();
                }
                #endregion

                #region Employment Contract 

                _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_defaultLoginUserID,
                        new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, null, _staffreviewrequirementsids);
                if (_employmentContractId == Guid.Empty)
                {
                    _employmentContractId = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_defaultLoginUserID).FirstOrDefault();

                }
                #endregion
            }


            catch
            {
                if (driver != null)
                    driver.Close();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-16260

        [TestProperty("JiraIssueID", "ACC-3400")]
        [Description("Verify creation of  Schedule Availability in View Diary / Manage Ad Hoc Pre Requisites:There is a existing applicant with an existing Role Applications.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases001()
        {
            var firstName = "Testing_CDV6_15810";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", firstName + " " + lastName, _employmentContractTypeid);

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
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .ClickCreateAdhocScheduleAvailabilityButton(DateTime.Now.ToString("dd'/'MM'/'yyyy"), 1);

            System.Threading.Thread.Sleep(3000);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ValidateAvailabilityOptionText("Salaried/Contracted")
                .ValidateAvailabilityOptionText("Hourly/Overtime")
                .ValidateAvailabilityOptionText("Regular")
                .ClickAvailabilityButton();

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .ValidateCreatedAdhocAvailabilitySlotIsVisible(DateTime.Now.ToString("dd'/'MM'/'yyyy"), firstName + " " + lastName + ", Helper, , " + DateTime.Now.ToString("dd'/'MM'/'yyyy"), true);

        }

        [TestProperty("JiraIssueID", "ACC-3401")]
        [Description("Verify modification of  existing Availability in View Diary / Manage Ad Hoc Pre Requisites: There is a existing applicant with an existing Role Applications and existing availability")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases002()
        {
            var currentDatePlusOneWeek = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");

            var firstName = "Testing_CDV6_15812";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("CDV6_15812", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, DateTime.Now, null, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), _roleApplication, _applicantId);
            _UserDiaryId = dbHelper.userDairy.createUserDairy(_defaultLoginUserID, _careProviders_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, DateTime.Now, DateTime.Now, 60, 1380);


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
                .ClickViewDiaryOrManageAdhocTab();

            Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .InsertGoToDate(currentDatePlusOneWeek)
                .ClickCreateOptionBasedOnDate(currentDatePlusOneWeek);

            Thread.Sleep(3000);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();

            Thread.Sleep(3000);

            var availabiltytypeid_regular = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Regular").FirstOrDefault();
            var fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(_UserScheduleIdTypeId, "title", "inactive", "startdate", "starttime", "enddate", "endtime", "recurrencepatternid", "availabilitytypesid");

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad();

            Assert.AreEqual("CDV6_15812", fields["title"]);
            Assert.AreEqual(false, fields.ContainsKey("enddate"));

        }

        [TestProperty("JiraIssueID", "ACC-3402")]
        [Description("Verify deletion of  existing Availability in View Diary Manage Ad Hoc Pre Requisites .There is a existing applicant with an existing Role Applications and existing availability")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases003()
        {
            var currentDatePlusOneWeek = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");

            var firstName = "Testing_CDV6_15813";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("CDV6_15813_Laks_2", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, DateTime.Now, null, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), _roleApplication, _applicantId);
            _UserDiaryId = dbHelper.userDairy.createUserDairy(_defaultLoginUserID, _careProviders_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, DateTime.Now, DateTime.Now, 60, 1380);


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
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .InsertGoToDate(currentDatePlusOneWeek)
                .ClickCreateOptionBasedOnDate(currentDatePlusOneWeek);

            System.Threading.Thread.Sleep(3000);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickRemoveTimeSlotButton();

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad();

            Thread.Sleep(2000);

            var fields = dbHelper.userDairy.GetUserDairybyWorkScheduleID(_UserScheduleIdTypeId,
                "deleted");
            Assert.AreEqual(false, fields.ContainsKey("deleted"));

        }

        [TestProperty("JiraIssueID", "ACC-3403")]
        [Description("Verify combining the availability records of same availability types in View Diary / Manage Ad Hoc.PreRequisite:There is a existing applicant with an existing Role Applications and two existing availability record of same availability type and same role application in a same day.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases004()
        {
            var currentDatePlusOneWeek = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");

            var firstName = "Testing_CDV6_15814";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", firstName + " " + lastName);

            _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", null, _careProviders_TeamId, _recurrencePatternId, null, _availabilityTypeId,
                DateTime.Now.Date, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);

            var userscheduleid2 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", null, _careProviders_TeamId, _recurrencePatternId, null, _availabilityTypeId,
                DateTime.Now.Date, null, new TimeSpan(9, 15, 0), new TimeSpan(17, 0, 0), _roleApplication, _applicantId, 1);

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
               .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .InsertGoToDate(currentDatePlusOneWeek)
                .DragAvailabilityRecordLeftSide_ToLeft(currentDatePlusOneWeek, 2); //drag the "09:15 - 17:00" transport to the left

            System.Threading.Thread.Sleep(3000);

            var userworkschedules = dbHelper.userWorkSchedule.GetUserRoleApplicantID(_applicantId);
            Assert.AreEqual(3, userworkschedules.Count);

            var fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(userworkschedules[0], "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(6, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);

        }

        [TestProperty("JiraIssueID", "ACC-3404")]
        [Description("Verify combining the availability records of different availability in types View Diary / Manage Ad Hoc.PreRequisite:There is a existing applicant with an existing Role Applications and two existing availability record of different availability type and same role application in a same day.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases005()
        {
            var currentDatePlusOneWeek = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");

            var firstName = "Testing_CDV6_15815";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("CDV6_15815", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, DateTime.Now.Date, null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), _roleApplication, _applicantId);
            var userscheduleid2 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("CDV6_15815", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId_Regular, DateTime.Now.Date.AddDays(7), null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId);

            var _UserDiaryId1 = dbHelper.userDairy.createUserDairy(_defaultLoginUserID, _careProviders_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, DateTime.Now, DateTime.Now, 60, 1380);
            var _UserDiaryId2 = dbHelper.userDairy.createUserDairy(_defaultLoginUserID, _careProviders_TeamId, userscheduleid2, _applicantId, _roleApplication, DateTime.Now, DateTime.Now, 60, 1380);


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
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .InsertGoToDate(currentDatePlusOneWeek)
                .DragExpandOptionBasedOnDate_ToLeft(currentDatePlusOneWeek); //drag the "09:00 - 17:00" transport to the left

            var fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(_UserScheduleIdTypeId, "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);

            fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(userscheduleid2, "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(6, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["endtime"]);

        }


        [TestProperty("JiraIssueID", "ACC-3405")]
        [Description("Verify the Time slot adjustment of availability records in View Diary / Manage Ad Hoc.Pre Requisites :There is a existing applicant with an existing Role Applications and existing availability record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases006()
        {
            var currentDayOfTheWeek = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentDatePlusOneWeek = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");


            var firstName = "Testing_CDV6_15816";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("CDV6_15816", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, DateTime.Now.Date, null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), _roleApplication, _applicantId);
            var _UserDiaryId1 = dbHelper.userDairy.createUserDairy(_defaultLoginUserID, _careProviders_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, DateTime.Now, DateTime.Now, 60, 1380);

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
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .InsertGoToDate(currentDayOfTheWeek)
                .DragExpandOptionBasedOnDate_ToLeft(currentDayOfTheWeek); //drag the "09:00 - 17:00" transport to the left

            System.Threading.Thread.Sleep(2000);

            var fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(_UserScheduleIdTypeId, "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);

        }


        [TestProperty("JiraIssueID", "ACC-3406")]
        [Description("Verify creation of  Transport Availability.PreRequisite:There is a existing applicant.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases007()
        {
            var currentDayOfTheWeek = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentDatePlusOneWeek = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");

            var firstName = "Testing_CDV6_15817";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);


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
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoadWhenNoRoleIsAdded()
                .ClickCreateTransportOptionBasedOnDate(currentDayOfTheWeek); //drag the "09:00 - 17:00" transport to the left

            System.Threading.Thread.Sleep(2000);

            applicantSelectScheduledTransportPopup
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(true)
                .ClickCarButton();

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoadWhenNoRoleIsAdded();

            var transportavailability = dbHelper.userTransportationSchedule.GetByApplicantID(_applicantId);
            Assert.AreEqual(1, transportavailability.Count);

        }


        [TestProperty("JiraIssueID", "ACC-3407")]
        [Description("Verify deletion of  existing Transport Schedule.PreRequisite:There is a existing applicant with an existing Transport Schedule")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases008()
        {
            var currentDayOfTheWeek = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentDatePlusOneWeek = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");


            var firstName = "Testing_CDV6_15818";
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
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .ClickCreateTransportOptionBasedOnDate(currentDayOfTheWeek); //drag the "09:00 - 17:00" transport to the left

            System.Threading.Thread.Sleep(2000);

            applicantSelectScheduledTransportPopup
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(false)
                .ClickRemoveTimeSlotButton();

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var transportationSchedules = dbHelper.userTransportationSchedule.GetByApplicantID(_applicantId);
            Assert.AreEqual(2, transportationSchedules.Count); //Since there are 2 more records in the recurrence created.Hence not 0,it is 2

        }



        [TestProperty("JiraIssueID", "ACC-3408")]
        [Description("Verify modification of  existing Transport Schedule.PreRequisite:There is a existing applicant with an existing Transport Schedule")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases009()
        {
            var currentDayOfTheWeek = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentDatePlusOneWeek = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");

            var firstName = "Testing_CDV6_15819";
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
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .ClickCreateTransportOptionBasedOnDate(currentDayOfTheWeek); //drag the "09:00 - 17:00" transport to the left

            System.Threading.Thread.Sleep(2000);
            applicantSelectScheduledTransportPopup
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(false)
                .ClickWalkingButton();

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad();

            Thread.Sleep(2000);

            var TransportId = dbHelper.userTransportationSchedule.GetUserTransportScheduleIdByApplicantIDAndTransportTypeId(_applicantId, _transportTypeId_Walking).FirstOrDefault();

            var fields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(TransportId,
                 "inactive", "startdate", "starttime", "enddate", "endtime", "recurrencepatternid", "transporttypeid", "weeknumber", "adhoc");

            Assert.AreEqual(DateTime.Now.Date, fields["startdate"]);
            Assert.AreEqual(true, fields.ContainsKey("enddate"));
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);
            Assert.AreEqual(false, fields["inactive"]);
            Assert.AreEqual(_transportTypeId_Walking.ToString(), fields["transporttypeid"].ToString());
            Assert.AreEqual(true, fields["adhoc"]);

        }


        [TestProperty("JiraIssueID", "ACC-3409")]
        [Description("Verify combining the Transport Schedule records of different Transport types.PreRequisite:There is a existing applicant with an existing two existing Transport Schedule record of different Transport type in a same day.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases0010()
        {
            var currentDayOfTheWeek = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentDatePlusOneWeek = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");

            var firstName = "Testing_CDV6_15820";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            var _userTransportationScheduleId1 = dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), _recurrencePatternId, _transportTypeId_Car, 1);
            var _userTransportationScheduleId2 = dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), _recurrencePatternId, _transportTypeId_Walking, 1);


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
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .DragTransportExpandOptionBasedOnDate_ToLeft(currentDayOfTheWeek); //drag the "09:00 - 17:00" transport to the left

            System.Threading.Thread.Sleep(2000);
            var fields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(_userTransportationScheduleId1, "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);

            fields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(_userTransportationScheduleId2, "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);

        }


        [TestProperty("JiraIssueID", "ACC-3410")]
        [Description("Verify combining the Transport Schedule records of same transport types.PreRequisite:There is a existing applicant with two existing Transport Schedule record of same transport type in a same day.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases0011()
        {
            var currentDayOfTheWeek = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentDatePlusOneWeek = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");

            var firstName = "Testing_CDV6_15821";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            var _userTransportationScheduleId1 = dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _recurrencePatternId, _transportTypeId_Car, 1);
            var _userTransportationScheduleId2 = dbHelper.userTransportationSchedule.CreateUserTransportationSchedule(_careProviders_TeamId, _applicantId, DateTime.Now.Date, new TimeSpan(9, 15, 0), new TimeSpan(17, 0, 0), _recurrencePatternId, _transportTypeId_Car, 1);


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
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .InsertGoToDate(currentDatePlusOneWeek)
                .DragTransportRecordLeftSide_ToLeft(currentDatePlusOneWeek, 2); //drag the "09:00 - 17:00" transport to the left

            System.Threading.Thread.Sleep(2000);
            var transportationSchedules = dbHelper.userTransportationSchedule.GetUserTransportScheduleIdByApplicantIDAndTransportTypeId(_applicantId, _transportTypeId_Car);
            Assert.AreEqual(3, transportationSchedules.Count);

            var fields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(transportationSchedules[0], "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(6, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);

        }

        [TestProperty("JiraIssueID", "ACC-3411")]
        [Description("Verify the Time slot adjustment of Transport Schedule records.PreRequisite:There is a existing applicant with an existing Transport Schedule record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases0012()
        {
            var currentDayOfTheWeek = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentDatePlusOneWeek = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");

            var firstName = "Testing_CDV6_15822";
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
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .InsertGoToDate(currentDatePlusOneWeek)
                .DragTransportExpandOptionBasedOnDate_ToLeft(currentDatePlusOneWeek); //drag the "09:00 - 17:00" transport to the left

            System.Threading.Thread.Sleep(2000);

            var fields = dbHelper.userTransportationSchedule.GetUserTransportationScheduleByID(_userTransportationScheduleId, "starttime", "endtime");
            Assert.AreEqual(new TimeSpan(9, 0, 0), fields["starttime"]);
            Assert.AreEqual(new TimeSpan(17, 0, 0), fields["endtime"]);

        }

        [TestProperty("JiraIssueID", "ACC-3412")]
        [Description("Verify creation of  Transport Availability and check View Diary/Ad hoc Availability ( Without any recent change ) for Applicant.PreRequisite:There is a existing applicant.There is no recent change for the any of the same day in Dairy / Ad hoc")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen", "Schedule Transport")]
        public void Applicant_UITestCases013()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            var firstName = "Testing_CDV6_15846";
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

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickViewDiaryOrManageAdhocTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

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

        [TestProperty("JiraIssueID", "ACC-3413")]
        [Description("Verify creation of  Transport Availability and check View Diary/Ad hoc Availability ( With recent change ) for Applicant.PreRequisite:There is a existing applicant.There is recent change for any of the same day in Dairy / Ad hoc")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Transport")]
        public void Applicant_UITestCases014()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy");

            var firstName = "Testing_CDV6_15847";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
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
                .ClickViewDiaryOrManageAdhocTab();

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoadWhenNoRoleIsAdded()
                .ClickModifyTransportOptionBasedOnDate(currentdate);

            applicantSelectScheduledTransportPopup
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(false)
                .ClickWalkingButton();

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoadWhenNoRoleIsAdded()
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

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            var transportationSchedules = dbHelper.userTransportationSchedule.GetByApplicantID(_applicantId);
            Assert.AreEqual(3, transportationSchedules.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3414")]
        [Description("Verify modification of  existing Transport Schedule and check View Diary/Ad hoc Availability ( Without any recent change ) for Applicant.PreRequisite:There is a existing applicant with an existing Transport Schedule.There is no recent change for the any of the same day in Dairy / Ad hoc")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases015()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            var firstName = "Testing_CDV6_15848";
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
                .ClickCreateTransportAvailabilityButton(currentDayOfTheWeek, 2);

            applicantSelectScheduledTransportPopup
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickWalkingButton();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickOnSaveButton();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

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
            Assert.AreEqual(_transportTypeId_Walking.ToString(), fields["transporttypeid"].ToString());
            Assert.AreEqual(1, fields["weeknumber"]);
            Assert.AreEqual(false, fields["adhoc"]);

        }

        [TestProperty("JiraIssueID", "ACC-3415")]
        [Description("Verify modification of  existing Transport Schedule and check View Diary/Ad hoc Availability ( Without any recent change ) for Applicant.PreRequisite:There is a existing applicant with an existing Transport Schedule.There is no recent change for the any of the same day in Dairy / Ad hoc")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases016()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            var firstName = "Testing_CDV6_15849";
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
                .ClickCreateTransportAvailabilityButton(currentDayOfTheWeek, 2);

            applicantSelectScheduledTransportPopup
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickWalkingButton();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickOnSaveButton();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

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
            Assert.AreEqual(_transportTypeId_Walking.ToString(), fields["transporttypeid"].ToString());
            Assert.AreEqual(1, fields["weeknumber"]);
            Assert.AreEqual(false, fields["adhoc"]);

        }

        [TestProperty("JiraIssueID", "ACC-3416")]
        [Description("Verify deletion of  existing Transport Schedule and check View Diary/Ad hoc Availability ( Without any recent change ) for Applicant.PreRequisite:There is a existing applicant with an existing Transport Schedule.There is no recent change for the any of the same day in Dairy / Ad hoc")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Transport")]
        public void Applicant_UITestCases017()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            var firstName = "Testing_CDV6_15850";
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
                .ClickCreateTransportAvailabilityButton(currentDayOfTheWeek, 2);

            applicantSelectScheduledTransportPopup
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickRemoveTimeSlotButton();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickOnSaveButton();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            Thread.Sleep(2000);

            var transportationSchedules = dbHelper.userTransportationSchedule.GetByApplicantID(_applicantId);
            Assert.AreEqual(0, transportationSchedules.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3417")]
        [Description("Verify deletion of  existing Transport Schedule and check View Diary/Ad hoc Availability ( With recent change ) for Applicant.PreRequisite:There is a existing applicant with an existing Transport Schedule")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Transport")]
        public void Applicant_UITestCases018()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var firstName = "Testing_CDV6_15851";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
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
                .ClickViewDiaryOrManageAdhocTab();

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoadWhenNoRoleIsAdded()
                .ClickModifyTransportOptionBasedOnDate(currentdate);

            applicantSelectScheduledTransportPopup
                .WaitForApplicantSelectScheduledTransportPopupToLoadFromViewDiarAdhoc(false)
                .ClickWalkingButton();

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoadWhenNoRoleIsAdded()
                .ClickScheduleTransportTab();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickCreateTransportAvailabilityButton(currentDayOfTheWeek, 2);

            applicantSelectScheduledTransportPopup
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickRemoveTimeSlotButton();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickOnSaveButton();

            applicantScheduleTransportSubPage
                .WaitForApplicantScheduleTransportSubPagePageToLoad()
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            Thread.Sleep(2000);

            var transportationSchedules = dbHelper.userTransportationSchedule.GetByApplicantID(_applicantId);
            Assert.AreEqual(1, transportationSchedules.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3418")]
        [Description("Verify deletion of  existing Availability Schedule and check View Diary/Ad hoc Availability ( With recent change ) for Applicant.PreRequisite:There is a existing applicant with an existing Availability Schedule")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases019()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var firstName = "Testing_CDV6_15854";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, DateTime.Now, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);

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

            System.Threading.Thread.Sleep(2000);

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .ClickModifyScheduleAvailabiltyDate(currentdate);

            System.Threading.Thread.Sleep(3000);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .ClickScheduleAvailabiltyTab();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickExsistScheduleAvailabilty();

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickRemoveTimeSlotButton();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton()
                .WaitForApplicantSheduleAvailabilityPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var UserWorkSchedulecount = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(_UserScheduleIdTypeId);
            Assert.AreEqual(0, UserWorkSchedulecount.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3419")]
        [Description("Verify deletion of  existing Availability Schedule and check View Diary/Ad hoc Availability ( Without any recent change ) for Applicant.PreRequisite:There is a existing applicant with an existing Availability Schedule")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases020()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var firstName = "Testing_CDV6_15855";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, DateTime.Now, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);

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

            System.Threading.Thread.Sleep(2000);

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickExsistScheduleAvailabilty();

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .ClickRemoveTimeSlotButton();

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton();

            System.Threading.Thread.Sleep(2000);

            var UserWorkSchedulecount = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(_UserScheduleIdTypeId);
            Assert.AreEqual(0, UserWorkSchedulecount.Count);

        }



        [TestProperty("JiraIssueID", "ACC-3420")]
        [Description("Verify modification of  existing Availability Schedule and check View Diary/Ad hoc Availability ( With recent change ) for Applicant.PreRequisite:There is a existing applicant with an existing Availability Schedule.There is recent change for any of the same day in Dairy / Ad hoc")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        public void Applicant_UITestCases021()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var firstName = "Testing_CDV6_15856";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, DateTime.Now, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);

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

            System.Threading.Thread.Sleep(2000);

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .ClickModifyScheduleAvailabiltyDate(currentdate);

            System.Threading.Thread.Sleep(3000);
            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .ClickScheduleAvailabiltyTab();

            System.Threading.Thread.Sleep(2000);

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickExsistScheduleAvailabilty();

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton();

            System.Threading.Thread.Sleep(4000);

            var userschedules = dbHelper.userWorkSchedule.GetUserRoleApplicantID(_applicantId);
            Assert.AreEqual(2, userschedules.Count);

            var fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(userschedules[0],
            "availabilitytypesid", "weeknumber", "AdHoc");

            Assert.AreEqual(_availabilityTypeId_Regular.ToString(), fields["availabilitytypesid"].ToString());

        }

        [TestProperty("JiraIssueID", "ACC-3421")]
        [Description("Verify modification of  existing Availability Schedule and check View Diary/Ad hoc Availability ( Without any recent change ) for Applicant.PreRequisite:There is a existing applicant with an existing Availability Schedule.There is no recent change for the any of the same day in Dairy / Ad hoc")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases022()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var firstName = "Testing_CDV6_15857";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, DateTime.Now, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);

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
                .ClickExsistScheduleAvailabilty();

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Regular");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton();

            System.Threading.Thread.Sleep(4000);

            var UserWorkSchedulecount = dbHelper.userWorkSchedule.GetUserRoleApplicantID(_applicantId);
            Assert.AreEqual(1, UserWorkSchedulecount.Count);

            var fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(UserWorkSchedulecount[0],
           "availabilitytypesid", "weeknumber", "AdHoc");

            Assert.AreEqual(_availabilityTypeId_Regular.ToString(), fields["availabilitytypesid"].ToString());
        }

        [TestProperty("JiraIssueID", "ACC-3422")]
        [Description("Verify creation of  Availability schedule and check View Diary/Ad hoc Availability ( With recent change ) for Applicant.PreRequisite:There is a existing applicant.There is a existing role applications.There is recent change for any of the same day in Dairy / Ad hoc")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "View Diary Manage Ad Hoc")]
        [TestProperty("Screen3", "Schedule Availability")]
        public void Applicant_UITestCases023()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var firstName = "Testing_CDV6_15858";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, DateTime.Now, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId);

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

            System.Threading.Thread.Sleep(2000);

            applicantSheduleAvailability
               .WaitForApplicantSheduleAvailabilityPageToLoad()
               .ClickViewDiaryOrManageAdhocTab();

            System.Threading.Thread.Sleep(3000);

            applicantViewDiaryOrManageAdhoc
                .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
                .ClickModifyScheduleAvailabiltyDate(currentdate);

            System.Threading.Thread.Sleep(3000);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            applicantViewDiaryOrManageAdhoc
               .WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
               .ClickScheduleAvailabiltyTab();

            System.Threading.Thread.Sleep(2000);

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .CreateScheduleAvailabiltyDate(currentdate);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Regular");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton();

            System.Threading.Thread.Sleep(3000);

            var UserWorkSchedulecount = dbHelper.userWorkSchedule.GetUserRoleApplicantID(_applicantId);
            Assert.AreEqual(2, UserWorkSchedulecount.Count);

            var fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(UserWorkSchedulecount[0], "availabilitytypesid", "weeknumber", "AdHoc");

            Assert.AreEqual(_availabilityTypeId_Regular.ToString(), fields["availabilitytypesid"].ToString());
        }

        [TestProperty("JiraIssueID", "ACC-3423")]
        [Description("Verify creation of  Availability schedule and check View Diary/Ad hoc Availability ( Without any recent change ) for Applicant.PreRequisite:There is a existing applicant.There is a existing role applications.There is recent change for any of the same day in Dairy / Ad hoc")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Applicant_UITestCases024()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var firstName = "Testing_CDV6_15859";
            var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);
            _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1);
            _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _defaultLoginUserID, _careProviders_TeamId, _recurrencePatternId, _employmentContractId, _availabilityTypeId, DateTime.Now, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId);

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
                .CreateScheduleAvailabiltyDate(currentdate);

            scheduleAvailabilityPopup
                .WaitForScheduleAvailabilityPopupWindowToLoad()
                .SelectScheduleAvailabilityTypeText("Salaried/Contracted");

            applicantSheduleAvailability
                .WaitForApplicantSheduleAvailabilityPageToLoad()
                .ClickOnSaveButton();

            System.Threading.Thread.Sleep(2000);
            var UserWorkSchedulecount = dbHelper.userWorkSchedule.GetUserRoleApplicantID(_applicantId);
            Assert.AreEqual(1, UserWorkSchedulecount.Count);

            var fields = dbHelper.userWorkSchedule.GetUserWorkScheduleByID(UserWorkSchedulecount[0],
           "availabilitytypesid", "weeknumber", "AdHoc");

            Assert.AreEqual(_availabilityTypeId.ToString(), fields["availabilitytypesid"].ToString());

        }

        #endregion

        //#region https://advancedcsg.atlassian.net/browse/CDV6-14545

        //[TestProperty("JiraIssueID", "CDV6-15222")]
        //[Description("As a Recruiting Manager When displaying / adding / updating availability for an applicant schedule and for transport I want the View Diary / Manage Ad hoc tab to be hidden from view So that this is not visible and cannot be clicked on or used ")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //public void Applicant_ViewScheduleAdhoc_UITestCases001()
        //{
        //    var firstName = "Testing_CDV6_15859";
        //    var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        //    _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careProviders_TeamId);

        //    loginPage
        //    .GoToLoginPage()
        //    .Login(_loginUsernamewithnoEmploymnetContract, "Passw0rd_!", _environmentName);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToApplicantsPage();

        //    applicantPage
        //        .WaitForApplicantsPageToLoad()
        //        .TypeSearchQuery(lastName)
        //        .OpenApplicantRecord(_applicantId.ToString());

        //    applicantRecordPage
        //        .WaitForApplicantRecordPagePageToLoad()
        //        .NavigateToAvailabilityTab();

        //    System.Threading.Thread.Sleep(2000);


        //    applicantSheduleAvailability
        //        .WaitForApplicantSheduleAvailabilityPageToLoad()
        //        .ValidateViewDiaryOrManageAdhocTabVisibility(false);
        //}

        //#endregion

    }
}



