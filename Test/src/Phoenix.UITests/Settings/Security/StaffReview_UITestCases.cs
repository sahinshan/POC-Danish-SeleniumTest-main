using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class StaffReview_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _languageId;
        private Guid _defaultLoginUserID;
        private Guid _systemUserID;
        private string _systemUsername;
        private Guid _newsystemUserID;

        private Guid _authenticationproviderid;
        private Guid _maritalStatusId;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid;
        private Guid _roleid;
        private Guid _questionCatalogueId;
        private Guid _documentId;
        private Guid _documentSectionId;
        private Guid _documentSectionQuestionId;
        private Guid _staffReviewSetupid;
        private Guid _staffReviewIdId;
        private Guid _documenttypeid;
        private Guid _documentsubtypeid;
        private Guid _reviewedBySystemUserId;
        private string _reviewedBySystemUsername;
        private Guid _reviewedBySystemUserId01;
        private string _reviewedBySystemUsername01;
        private Guid _recurrencePatternId;
        public Guid _userWorkSchedule;
        public Guid _userWorkSchedule1;
        public string EnvironmentName;
        public Guid environmentid;
        private string _loginUser;

        private string _systemUserName;
        private string _systemUserFirstName;
        private string _systemUserLastName;

        private Guid _provider_ReviewId;
        private string _bookingTypeName;
        private Guid _bookingTypeId;
        private string _documentName = "Staff Supervision 01_" + DateTime.Now.ToString("yyyyMMddHHmmss");

        private Guid _providerid;
        private string _providerName = "Staff Reviews Provider " + DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void SystemUserStaffReview_Setup()
        {
            try
            {
                #region Connecting Database : CareProvider

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);

                #endregion

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careDirectorQA_BusinessUnitId, null, "CareDirectorQA@careworkstempmail.com", "Default team for business unit", null);
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Marital Status

                var maritalStatusExist = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").Any();
                if (!maritalStatusExist)
                {
                    _maritalStatusId = dbHelper.maritalStatus.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);
                }
                if (_maritalStatusId == Guid.Empty)
                {
                    _maritalStatusId = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").FirstOrDefault();
                }
                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                {
                    _languageId = dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                }
                if (_languageId == Guid.Empty)
                {
                    _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").FirstOrDefault();
                }
                #endregion Lanuage

                #region Booking Type 1 -> "Booking (to location)" & "Count full booking length"

                if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 4").Any())
                    _bookingTypeId = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 4", 4, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, false, false, false, false, false);

                if (_bookingTypeId == Guid.Empty)
                    _bookingTypeId = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 4").First();

                #endregion

                #region Authentication Provider

                _providerid = dbHelper.provider.CreateProvider(_providerName, _careDirectorQA_TeamId, 3, true);

                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providerid, _bookingTypeId, true);

                #endregion

                #region Create default system user

                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_3").Any();
                if (!defaultLoginUserExists)
                {
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_3", "CW", "Admin_Test_User_3", "CW Admin Test User 3", "Passw0rd_!", "CW_Admin_Test_User_3@somemail.com", "CW_Admin_Test_User_3@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, true);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);
                }

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_3").FirstOrDefault();

                _loginUser = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];
                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);
                dbHelper.systemUser.UpdateEmployeeTypeId(_defaultLoginUserID, 4);

                #endregion  Create default system user

                #region Create SystemUser Record
                string systemUsername = DateTime.Now.ToString("yyyyMMddHHmmss_FFFFFF");

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_13797" + systemUsername).Any();
                if (!newSystemUser)
                    _systemUserID = dbHelper.systemUser.CreateSystemUser("CW_Test_User_CDV6_13797" + systemUsername, "CW", "Test_User_CDV6_13797" + systemUsername, "CW Test_User_CDV6_13797", "Passw0rd_!", "CW_Test_User_CDV6_13797@somemail.com", "CW_Test_User_CDV6_13797@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

                if (_systemUserID == Guid.Empty)
                    _systemUserID = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_13797" + systemUsername).FirstOrDefault();
                _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserID, "username")["username"];

                #endregion  Create SystemUser Record

                #region Recurrence Pattern

                var recPatternName = "Occurs every 1 week(s) on " + DateTime.Now.DayOfWeek.ToString().ToLower();

                if (!dbHelper.recurrencePattern.GetRecurrencePatternIdByName(recPatternName).Any())
                    _recurrencePatternId = dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

                if (_recurrencePatternId == Guid.Empty)
                    _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle(recPatternName).FirstOrDefault();

                #endregion

                #region Staff Reviewed by 

                var reviewedByExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_13797_User2").Any();
                if (!reviewedByExists)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.CreateSystemUser("CW_Test_User_CDV6_13797_User2", "CW", "Test_User_CDV6_13797", "CW Test_User_CDV6_13797_User2", "Passw0rd_!", "CW_Test_User_CDV6_13797_User2@somemail.com", "CW_Test_User_CDV6_13797_User2@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }

                if (_reviewedBySystemUserId == Guid.Empty)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_13797_User2").FirstOrDefault();
                    _reviewedBySystemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_reviewedBySystemUserId, "username")["username"];
                }
                #endregion

                #region Staff Reviewed by01

                var reviewedByExists01 = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_13797_User3").Any();
                if (!reviewedByExists01)
                {
                    _reviewedBySystemUserId01 = dbHelper.systemUser.CreateSystemUser("CW_Test_User_CDV6_13797_User3", "CW", "Test_User_CDV6_13797", "CW Test_User_CDV6_13797_User3", "Passw0rd_!", "CW_Test_User_CDV6_13797_User3@somemail.com", "CW_Test_User_CDV6_13797_User3@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }

                if (_reviewedBySystemUserId01 == Guid.Empty)
                {
                    _reviewedBySystemUserId01 = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_13797_User3").FirstOrDefault();
                    _reviewedBySystemUsername01 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_reviewedBySystemUserId01, "username")["username"];
                }
                #endregion

                #region Create New User WorkSchedule

                var newUserWorkSchedule = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID).Any();
                if (!newUserWorkSchedule)
                {
                    _userWorkSchedule = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Default Schedule", _systemUserID, _careDirectorQA_TeamId, _recurrencePatternId, DateTime.Now.Date, null, TimeSpan.FromHours(10), TimeSpan.FromHours(18));
                }
                if (_userWorkSchedule == Guid.Empty)
                {
                    _userWorkSchedule = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID).FirstOrDefault();
                }

                #endregion Create New User WorkSchedule

                #region Create New User WorkSchedule01

                var newUserWorkSchedule1 = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_reviewedBySystemUserId).Any();
                if (!newUserWorkSchedule1)
                {
                    _userWorkSchedule1 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Default Schedule", _reviewedBySystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, DateTime.Now.Date, null, TimeSpan.FromHours(10), TimeSpan.FromHours(18));
                }
                if (_userWorkSchedule1 == Guid.Empty)
                {
                    _userWorkSchedule1 = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_reviewedBySystemUserId).FirstOrDefault();
                }

                #endregion Create New User WorkSchedule01

                #region Care provider staff role type
                string CareProviderStaffRoleName = "Helper_" + DateTime.Now.ToString("yyyyMMddHHmmss_FFFFFF");
                var careProviderStaffRoleTypeExists = dbHelper.careProviderStaffRoleType.GetByName(CareProviderStaffRoleName).Any();
                if (!careProviderStaffRoleTypeExists)
                {
                    _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, CareProviderStaffRoleName, "2", null, new DateTime(2020, 1, 1), null);
                }
                if (_careProviderStaffRoleTypeid == Guid.Empty)
                {
                    _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.GetByName(CareProviderStaffRoleName).FirstOrDefault();
                }

                #endregion

                #region Employment Contract Type

                var employmentContractTypeExists = dbHelper.employmentContractType.GetByName("Full Time Employee Contract").Any();
                if (!employmentContractTypeExists)
                {
                    _employmentContractTypeid = dbHelper.employmentContractType.CreateEmploymentContractType(_careDirectorQA_TeamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));
                }
                if (_employmentContractTypeid == Guid.Empty)
                {
                    _employmentContractTypeid = dbHelper.employmentContractType.GetByName("Full Time Employee Contract").FirstOrDefault();
                }

                #endregion

                #region system User Employment Contract

                var roleid = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserID).Any();
                if (!roleid)
                {
                    _roleid = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date.AddDays(-10), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid);

                    dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_roleid, _bookingTypeId);


                }
                if (_roleid == Guid.Empty)
                {
                    _roleid = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserID).FirstOrDefault();
                }
                #endregion

                #region Question Catalogue

                var questionCatalogueExists = dbHelper.questionCatalogue.GetByQuestionName("Strengths").Any();
                if (!questionCatalogueExists)
                    dbHelper.questionCatalogue.CreateNumericQuestion("Strengths", "");
                _questionCatalogueId = dbHelper.questionCatalogue.GetByQuestionName("Strengths").First();

                #endregion

                #region Document

                var documentExists = dbHelper.document.GetDocumentByName(_documentName).Any();
                if (!documentExists)
                {
                    var documentCategoryId = dbHelper.documentCategory.GetByName("Staff Review Form")[0];
                    var documentTypeId = dbHelper.documentType.GetByName("Initial Assessment")[0];

                    _documentId = dbHelper.document.CreateDocument(_documentName, documentCategoryId, documentTypeId, _careDirectorQA_TeamId, 1);
                    _documentSectionId = dbHelper.documentSection.CreateDocumentSection("Section 1", _documentId);
                    _documentSectionQuestionId = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId, _documentSectionId);
                    dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published

                }
                if (_documentId == Guid.Empty)
                {
                    _documentId = dbHelper.document.GetDocumentByName(_documentName)[0];
                    _documentSectionId = dbHelper.documentSection.GetByDocumentIdAndName(_documentId, "Section 1")[0];
                    _documentSectionQuestionId = dbHelper.documentSectionQuestion.GetBySectionIdAndQuestionCatalogueId(_documentSectionId, _questionCatalogueId)[0];
                }

                #endregion

                #region Staff Review Setup Record

                var staffReviewSetupExists = dbHelper.staffReviewSetup.GetByName(_documentName).Any();
                if (!staffReviewSetupExists)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.CreateStaffReviewSetup(_documentName, _documentId, new DateTime(2021, 1, 1), "for automation", true, true, false, _bookingTypeId);
                }
                if (_staffReviewSetupid == Guid.Empty)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.GetByName(_documentName).FirstOrDefault();
                    dbHelper.staffReviewSetup.UpdateBookingTypeId(_staffReviewSetupid, _bookingTypeId);
                }

                #endregion

                #region Staff Review

                foreach (var staffReviewId in dbHelper.staffReview.GetBySystemUserId(_systemUserID))
                    dbHelper.staffReview.DeleteStaffReview(staffReviewId);

                #endregion Staff Review

                #region Attach Document Type

                var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
                if (!attachDocumentTypeExists)
                    dbHelper.attachDocumentType.CreateAttachDocumentType(_careDirectorQA_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
                _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

                #endregion

                #region Attach Document Sub Type

                var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
                if (!attachDocumentSubTypeExists)
                    dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careDirectorQA_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
                _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

                #endregion

                #region Create Provider

                var providerHospitalExists = dbHelper.provider.GetProviderByName("Automation_Provider_Review").Any();
                if (!providerHospitalExists)
                    dbHelper.provider.CreateProvider("Automation_Provider_Review", _careDirectorQA_TeamId);
                _provider_ReviewId = dbHelper.provider.GetProviderByName("Automation_Provider_Review")[0];

                #endregion Create Provider 

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        internal DateTime GetThisWeekFirstMonday()
        {
            DateTime dt = DateTime.Now;
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;

        }

        //#region https://advancedcsg.atlassian.net/browse/CDV6-13797

        //   [TestProperty("JiraIssueID", "CDV6-14551")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews -> Create New Record -> " +
        //       "Fill all mandatory details and review date, start time, end time, reviewed by as well and click Save->Staff Review record should be created successfully along with Appointment as per mentioned date," +
        //       " time and participants")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod001()
        //   {
        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText("CW_Test_User_CDV6_13797")
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .ClickCreateRecordButton();

        //       staffReviewRecordPage
        //           .WaitForStaffReviewNewRecordCreatePageToLoad()
        //           .ClickReviewTypeIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .TypeSearchQuery(_documentName)
        //           .TapSearchButton()
        //           .SelectResultElement(_staffReviewSetupid.ToString());

        //       staffReviewRecordPage
        //           .WaitForStaffReviewNewRecordCreatePageToLoad()
        //           .ClickReviewedByIdLookUp();

        //       lookupPopup
        //         .WaitForLookupPopupToLoad()
        //         .SelectViewByText("Lookup View")
        //         .TypeSearchQuery("CW_Test_User_CDV6_13797_User2")
        //         .TapSearchButton()
        //         .SelectResultElement(_reviewedBySystemUserId.ToString());

        //       staffReviewRecordPage
        //        .WaitForStaffReviewNewRecordCreatePageToLoad()
        //         .SelectStatusOption("Completed")
        //         .InsertNextReviewDate(DateTime.Now.AddMonths(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //         .InsertReviewStartTime("14:00")
        //         .InsertReviewEndTime("16:00")
        //         .ClickProviderLookUp();

        //       lookupPopup
        //         .WaitForLookupPopupToLoad()
        //         .SelectViewByText("Lookup View")
        //         .TypeSearchQuery("Automation_Provider_Review")
        //         .TapSearchButton()
        //         .SelectResultElement(_provider_ReviewId.ToString());

        //       staffReviewRecordPage
        //         .ClickSaveAndCloseButton();

        //       systemUserStaffReviewPage
        //         .WaitForSystemUserStaffReviewPageToLoad()
        //         .SelectSystemViewsOption("Completed");

        //       System.Threading.Thread.Sleep(3000);

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(1, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(staffReviews[0]);
        //       Assert.AreEqual(1, appointments.Count);

        //       var appointmentFields = dbHelper.appointment.GetAppointmentByID(appointments[0], "subject", "startdate", "enddate", "starttime", "endtime");
        //       Assert.AreEqual(_documentName, appointmentFields["subject"]);
        //       Assert.AreEqual(DateTime.Now.Date.AddMonths(1), appointmentFields["startdate"]);
        //       Assert.AreEqual(DateTime.Now.Date.AddMonths(1), appointmentFields["enddate"]);
        //       Assert.AreEqual(new TimeSpan(14, 0, 0), appointmentFields["starttime"]);
        //       Assert.AreEqual(new TimeSpan(16, 0, 0), appointmentFields["endtime"]);

        //       var appointmentFirstRequiredAttendee = dbHelper.appointmentRequiredAttendee.GetByAppointmentIDAndRegardingID(appointments[0], _systemUserID);
        //       Assert.AreEqual(1, appointmentFirstRequiredAttendee.Count);

        //       var appointmentSecondRequiredAttendee = dbHelper.appointmentRequiredAttendee.GetByAppointmentIDAndRegardingID(appointments[0], _reviewedBySystemUserId);
        //       Assert.AreEqual(1, appointmentSecondRequiredAttendee.Count);

        //   }

        //   [TestProperty("JiraIssueID", "CDV6-14552")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews -> Create New Record -> " +
        //       "Fill all mandatory and non mandatory fields except Review date(Next Review Date) and click Save-> Only Staff Review record should be created successfully not Appointment")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod002()
        //   {
        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText(_systemUsername)
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .ClickCreateRecordButton();

        //       staffReviewRecordPage
        //           .WaitForStaffReviewNewRecordCreatePageToLoad()
        //           .ClickReviewTypeIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .TypeSearchQuery(_documentName)
        //           .TapSearchButton()
        //           .SelectResultElement(_staffReviewSetupid.ToString());

        //       staffReviewRecordPage
        //         .WaitForStaffReviewNewRecordCreatePageToLoad()
        //         .ClickReviewedByIdLookUp();

        //       lookupPopup
        //         .WaitForLookupPopupToLoad()
        //         .SelectViewByText("Lookup View")
        //         .TypeSearchQuery(_reviewedBySystemUsername)
        //         .TapSearchButton()
        //         .SelectResultElement(_reviewedBySystemUserId.ToString());

        //       staffReviewRecordPage
        //         .WaitForStaffReviewNewRecordCreatePageToLoad()
        //         .SelectStatusOption("Completed")
        //         .InsertReviewStartTime("14:00")
        //         .InsertReviewEndTime("16:00")
        //         .ClickSaveAndCloseButton();

        //       systemUserStaffReviewPage
        //         .WaitForSystemUserStaffReviewPageToLoad()
        //         .SelectSystemViewsOption("Completed");

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(1, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(staffReviews[0]);
        //       Assert.AreEqual(0, appointments.Count);


        //   }

        //   [TestProperty("JiraIssueID", "CDV6-14553")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews -> Create New Record ->" +
        //       " Fill all mandatory and non mandatory fields except Start Time -> Only Staff Review record should be created successfully not Appointment")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod003()
        //   {
        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText("CW_Test_User_CDV6_13797")
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .ClickCreateRecordButton();

        //       staffReviewRecordPage
        //           .WaitForStaffReviewNewRecordCreatePageToLoad()
        //           .ClickReviewTypeIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .TypeSearchQuery("Staff Supervision")
        //           .TapSearchButton()
        //           .SelectResultElement(_staffReviewSetupid.ToString());

        //       staffReviewRecordPage
        //         .WaitForStaffReviewNewRecordCreatePageToLoad()
        //         .ClickReviewedByIdLookUp();

        //       lookupPopup
        //         .WaitForLookupPopupToLoad()
        //         .SelectViewByText("Lookup View")
        //         .TypeSearchQuery("CW_Test_User_CDV6_13797_User2")
        //         .TapSearchButton()
        //         .SelectResultElement(_reviewedBySystemUserId.ToString());

        //       staffReviewRecordPage
        //         .WaitForStaffReviewNewRecordCreatePageToLoad()
        //         .SelectStatusOption("Completed")
        //         .InsertNextReviewDate(DateTime.Now.AddMonths(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //         .InsertReviewEndTime("16:00")
        //         .ClickSaveAndCloseButton();

        //       systemUserStaffReviewPage
        //         .WaitForSystemUserStaffReviewPageToLoad()
        //         .SelectSystemViewsOption("Completed");

        //       System.Threading.Thread.Sleep(2000);

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(1, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(staffReviews[0]);
        //       Assert.AreEqual(0, appointments.Count);
        //   }

        //   [TestProperty("JiraIssueID", "CDV6-14554")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews -> Create New Record ->" +
        //       " Fill all mandatory and non mandatory fields except end time and click Save.-> Only Staff Review record should be created successfully not Appointment")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod004()
        //   {
        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText(_systemUsername)
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .ClickCreateRecordButton();

        //       staffReviewRecordPage
        //           .WaitForStaffReviewNewRecordCreatePageToLoad()
        //           .ClickReviewTypeIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .TypeSearchQuery(_documentName)
        //           .TapSearchButton()
        //           .SelectResultElement(_staffReviewSetupid.ToString());

        //       staffReviewRecordPage
        //         .WaitForStaffReviewNewRecordCreatePageToLoad()
        //         .ClickReviewedByIdLookUp();

        //       lookupPopup
        //         .WaitForLookupPopupToLoad()
        //         .SelectViewByText("Lookup View")
        //         .TypeSearchQuery(_reviewedBySystemUsername)
        //         .TapSearchButton()
        //         .SelectResultElement(_reviewedBySystemUserId.ToString());

        //       staffReviewRecordPage
        //         .WaitForStaffReviewNewRecordCreatePageToLoad()
        //         .SelectStatusOption("Completed")
        //         .InsertNextReviewDate(DateTime.Now.AddMonths(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //         .InsertReviewStartTime("14:00")
        //         .ClickSaveAndCloseButton();

        //       systemUserStaffReviewPage
        //         .WaitForSystemUserStaffReviewPageToLoad()
        //         .SelectSystemViewsOption("Completed");


        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(1, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(staffReviews[0]);
        //       Assert.AreEqual(0, appointments.Count);
        //   }

        //   [TestProperty("JiraIssueID", "CDV6-14555")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews -> Create New Record ->" +
        //       " Fill all mandatory and non mandatory fields except reviewed by and click Save.-> Only Staff Review record should be created successfully not Appointment")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod005()
        //   {
        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText("CW_Test_User_CDV6_13797")
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .ClickCreateRecordButton();

        //       staffReviewRecordPage
        //           .WaitForStaffReviewNewRecordCreatePageToLoad()
        //           .ClickReviewTypeIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .TypeSearchQuery("Staff Supervision")
        //           .TapSearchButton()
        //           .SelectResultElement(_staffReviewSetupid.ToString());

        //       staffReviewRecordPage
        //         .WaitForStaffReviewNewRecordCreatePageToLoad()
        //         .SelectStatusOption("Completed")
        //         .InsertNextReviewDate(DateTime.Now.AddMonths(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //         .InsertReviewStartTime("14:00")
        //         .InsertReviewEndTime("16:00")
        //         .ClickSaveAndCloseButton();

        //       systemUserStaffReviewPage
        //         .WaitForSystemUserStaffReviewPageToLoad()
        //         .SelectSystemViewsOption("Completed");

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(1, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(staffReviews[0]);
        //       Assert.AreEqual(0, appointments.Count);
        //   }

        //   [TestProperty("JiraIssueID", "CDV6-14557")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews -> " +
        //       "Open the Staff Review Record which is tagged to an appointment -> Make the Review Date (Next Review Date) as blank and click Save.->" +
        //       " Staff Review record should be saved successfully and respective Appointment tagged to this Staff Review Status should be changed as Cancelled.")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod006()
        //   {
        //       dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());
        //       dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

        //       var currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        //       _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, currentDate);
        //       dbHelper.staffReview.UpdateAppointmentStartAndEndTime(_staffReviewIdId, TimeSpan.FromHours(12), TimeSpan.FromHours(14));
        //       var appointmentId = dbHelper.appointment.CreateStaffReviewAppointment(_careDirectorQA_TeamId, null, null, null, null, null, null, null, "Staff Supervision", "", "", currentDate, TimeSpan.FromHours(12), currentDate, TimeSpan.FromHours(14), _staffReviewIdId, "staffreview", "Staff Supervision", 4, 1, false, null, null, null);


        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText("CW_Test_User_CDV6_13797")
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .SelectSystemViewsOption("Completed")
        //           .OpenRecord(_staffReviewIdId.ToString());

        //       staffReviewRecordPage
        //           .WaitForStaffReviewNewRecordCreatePageToLoad()
        //           .InsertNextReviewDate("")
        //           .InsertCompletedDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
        //           .ClickSaveAndCloseButton();

        //       systemUserStaffReviewPage
        //         .WaitForSystemUserStaffReviewPageToLoad()
        //         .OpenRecord(_staffReviewIdId.ToString());

        //       staffReviewRecordPage
        //           .WaitForStaffReviewNewRecordCreatePageToLoad()
        //           .ValidateNextReviewDate("");

        //       var appointmentFields = dbHelper.appointment.GetAppointmentByID(appointmentId, "statusid");
        //       Assert.AreEqual(3, appointmentFields["statusid"]); //statusid = 3 --> Cancelled appointment

        //   }

        //   [TestProperty("JiraIssueID", "CDV6-14558")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews -> " +
        //       "Open the Staff Review Record which is tagged to an appointment -> Delete the record-> Both Staff Review record and respective Appointment tagged to this Staff Review should get deleted")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod007()
        //   {
        //       _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId);

        //       dbHelper.appointment.CreateStaffReviewAppointment(_careDirectorQA_TeamId, null, null, null, null, null, null, null, "Staff Supervision", "", "",
        //           DateTime.Now.Date, TimeSpan.FromHours(12), DateTime.Now.Date, TimeSpan.FromHours(14), _staffReviewIdId,
        //           "staffreview", "Staff Supervision", 4, 1, false, null, null, null);

        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText("CW_Test_User_CDV6_13797")
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .SelectSystemViewsOption("Completed")
        //           .OpenRecord(_staffReviewIdId.ToString());

        //       staffReviewRecordPage
        //           .WaitForStaffReviewRecordPageToLoad()
        //           .ClickDeleteButton();

        //       alertPopup
        //           .WaitForAlertPopupToLoad()
        //           .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
        //           .TapOKButton();

        //       systemUserStaffReviewPage
        //         .WaitForSystemUserStaffReviewPageToLoad();

        //       System.Threading.Thread.Sleep(1000);

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(0, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(_staffReviewIdId);
        //       Assert.AreEqual(0, appointments.Count);
        //   }

        //   [TestProperty("JiraIssueID", "CDV6-14559")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews ->" +
        //       " Select the Staff Review Record which is tagged to an appointment and Delete the record-> Both Staff Review record and respective Appointment" +
        //       " tagged to this Staff Review should get deleted")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod008()
        //   {
        //       _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId);

        //       dbHelper.appointment.CreateStaffReviewAppointment(_careDirectorQA_TeamId, null, null, null, null, null, null, null, "Staff Supervision", "", "",
        //           DateTime.Now.Date, TimeSpan.FromHours(12), DateTime.Now.Date, TimeSpan.FromHours(14), _staffReviewIdId,
        //           "staffreview", "Staff Supervision", 4, 1, false, null, null, null);

        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText("CW_Test_User_CDV6_13797")
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .SelectSystemViewsOption("Completed")
        //           .SelectStaffReviewRecord(_staffReviewIdId.ToString())
        //           .ClickDeleteButton();

        //       alertPopup
        //           .WaitForAlertPopupToLoad()
        //           .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
        //           .TapOKButton();

        //       alertPopup
        //           .WaitForAlertPopupToLoad()
        //           .ValidateAlertText("1 item(s) deleted.")
        //           .TapOKButton();

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(0, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(_staffReviewIdId);
        //       Assert.AreEqual(0, appointments.Count);
        //   }

        //   [TestProperty("JiraIssueID", "CDV6-14560")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu ->" +
        //       " Staff Reviews -> Open the Staff Review Record which is tagged to an appointment -> Change the values of Review Date (Next Review Date) , " +
        //       "Start Time , End Time and click Save ->Staff Review record should be saved successfully and respective Appointment tagged to this Staff Review " +
        //       "should get updated as per newly entered date and time")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod009()
        //   {
        //       dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());
        //       dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());


        //       var currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        //       _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, currentDate);
        //       dbHelper.staffReview.UpdateAppointmentStartAndEndTime(_staffReviewIdId, TimeSpan.FromHours(12), TimeSpan.FromHours(14));

        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText(_systemUsername)
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .SelectSystemViewsOption("Completed")
        //           .OpenRecord(_staffReviewIdId.ToString());

        //       staffReviewRecordPage
        //           .WaitForStaffReviewRecordPageToLoad()
        //           .InsertNextReviewDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //           .InsertReviewEndTime("17:40")
        //           .InsertReviewStartTime("15:20")
        //           .InsertCompletedDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
        //           .ClickProviderLookUp();

        //       lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(_providerName).TapSearchButton().SelectResultElement(_providerid.ToString());

        //       staffReviewRecordPage
        //           .WaitForStaffReviewRecordPageToLoad()
        //           .ClickSaveAndCloseButton();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .ClickRefreshButton();

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(1, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(staffReviews[0]);
        //       Assert.AreEqual(1, appointments.Count);

        //       var appointmentFields = dbHelper.appointment.GetAppointmentByID(appointments[0], "startdate", "enddate", "starttime", "endtime");
        //       Assert.AreEqual(DateTime.Now.Date, ((DateTime)appointmentFields["startdate"]).ToLocalTime().Date);
        //       Assert.AreEqual(DateTime.Now.Date, appointmentFields["enddate"]);
        //       Assert.AreEqual(new TimeSpan(15, 20, 0), appointmentFields["starttime"]);
        //       Assert.AreEqual(new TimeSpan(17, 40, 0), appointmentFields["endtime"]);
        //   }

        //   [TestProperty("JiraIssueID", "CDV6-14561")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews ->" +
        //       " Open the Staff Review Record which is tagged to an appointment -> Make the Start Time , End Time as blank and click Save.->Staff Review record should" +
        //       " be saved successfully and respective Appointment tagged to this Staff Review should not get updated it should remain as it is")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod010()
        //   {
        //       dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());
        //       dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

        //       _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);
        //       dbHelper.staffReview.UpdateAppointmentStartAndEndTime(_staffReviewIdId, TimeSpan.FromHours(12), TimeSpan.FromHours(14));

        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText("CW_Test_User_CDV6_13797")
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .SelectSystemViewsOption("Completed")
        //           .OpenRecord(_staffReviewIdId.ToString());

        //       staffReviewRecordPage
        //           .WaitForStaffReviewRecordPageToLoad()
        //           .InsertReviewStartTime("")
        //           .InsertReviewEndTime("")
        //           .InsertCompletedDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
        //           .ClickSaveAndCloseButton();

        //       systemUserStaffReviewPage
        //          .WaitForSystemUserStaffReviewPageToLoad();

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(1, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(staffReviews[0]);
        //       Assert.AreEqual(1, appointments.Count);

        //       var appointmentFields = dbHelper.appointment.GetAppointmentByID(appointments[0], "starttime", "endtime");
        //       Assert.AreEqual(TimeSpan.FromHours(12), appointmentFields["starttime"]);
        //       Assert.AreEqual(TimeSpan.FromHours(14), appointmentFields["endtime"]);
        //   }

        //   [TestProperty("JiraIssueID", "CDV6-14634")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews ->" +
        //       " Create New Record -> Fill all mandatory details , reviewed by and give same  review date, start time, end time as per regarding user's  existing appointment and click Save ->" +
        //       "Should not create Staff review and appointment. And Should display proper error message of conflict")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod011()
        //   {
        //       DateTime nextReviewDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);
        //       _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, nextReviewDate);
        //       dbHelper.staffReview.UpdateAppointmentStartAndEndTime(_staffReviewIdId, TimeSpan.FromHours(12), TimeSpan.FromHours(14));

        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText("CW_Test_User_CDV6_13797")
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .ClickCreateRecordButton();

        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .ClickReviewTypeIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .TypeSearchQuery("Staff Supervision")
        //           .TapSearchButton()
        //           .SelectResultElement(_staffReviewSetupid.ToString());


        //       staffReviewRecordPage
        //           .WaitForStaffReviewNewRecordCreatePageToLoad()
        //           .ClickReviewedByIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .SelectViewByText("Lookup View")
        //           .TypeSearchQuery("CW_Test_User_CDV6_13797_User2")
        //           .TapSearchButton()
        //           .SelectResultElement(_reviewedBySystemUserId.ToString());

        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .SelectStatusOption("Completed")
        //          .InsertNextReviewDate(nextReviewDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //          .InsertReviewStartTime("12:00")
        //          .InsertReviewEndTime("14:00")
        //          .ClickProviderLookUp();

        //       lookupPopup
        //         .WaitForLookupPopupToLoad()
        //         .SelectViewByText("Lookup View")
        //         .TypeSearchQuery("Automation_Provider_Review")
        //         .TapSearchButton()
        //         .SelectResultElement(_provider_ReviewId.ToString());

        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()

        //          .ClickSaveAndCloseButton();

        //       dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Required User CW Test_User_CDV6_13797 has a conflicting Appointment at this time.").TapCloseButton();

        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad();

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(1, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(staffReviews[0]);
        //       Assert.AreEqual(1, appointments.Count);

        //   }


        //   [TestProperty("JiraIssueID", "CDV6-14635")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews ->" +
        // " Create New Record -> Fill all mandatory details , reviewed by and give same  review date, start time, end time as per regarding user's existing appointment and click Save ->" +
        // "Should not create Staff review and appointment. And Should display proper error message of conflict")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod012()
        //   {
        //       DateTime nextReviewDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);
        //       _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, nextReviewDate);
        //       dbHelper.staffReview.UpdateAppointmentStartAndEndTime(_staffReviewIdId, TimeSpan.FromHours(12), TimeSpan.FromHours(14));

        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText("CW_Test_User_CDV6_13797")
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .ClickCreateRecordButton();

        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .ClickReviewTypeIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .TypeSearchQuery("Staff Supervision")
        //           .TapSearchButton()
        //           .SelectResultElement(_staffReviewSetupid.ToString());


        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .ClickReviewedByIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .SelectViewByText("Lookup View")
        //           .TypeSearchQuery("CW_Test_User_CDV6_13797_User2")
        //           .TapSearchButton()
        //           .SelectResultElement(_reviewedBySystemUserId.ToString());

        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .SelectStatusOption("Completed")
        //          .InsertNextReviewDate(nextReviewDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //          .InsertReviewStartTime("12:00")
        //          .InsertReviewEndTime("14:00")
        //          .ClickProviderLookUp();

        //       lookupPopup
        //         .WaitForLookupPopupToLoad()
        //         .SelectViewByText("Lookup View")
        //         .TypeSearchQuery("Automation_Provider_Review")
        //         .TapSearchButton()
        //         .SelectResultElement(_provider_ReviewId.ToString());

        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .ClickSaveAndCloseButton()
        //          .ValidateAppointmentAlertMessage("Required User CW Test_User_CDV6_13797 has a conflicting Appointment at this time.");

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(1, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(staffReviews[0]);
        //       Assert.AreEqual(1, appointments.Count);
        //   }


        //   [TestProperty("JiraIssueID", "CDV6-14636")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews ->" +
        //" Create New Record -> Fill all mandatory details , reviewed by and give same  review date, start time, end time as per regarding user's  existing appointment and click Save ->" +
        //"Should not create Staff review and appointment. And Should display proper error message of conflict")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod013()
        //   {
        //       _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date.AddDays(1));
        //       dbHelper.staffReview.UpdateAppointmentStartAndEndTime(_staffReviewIdId, TimeSpan.FromHours(12), TimeSpan.FromHours(14));

        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText("CW_Test_User_CDV6_13797")
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .ClickCreateRecordButton();

        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .ClickReviewTypeIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .TypeSearchQuery("Staff Supervision")
        //           .TapSearchButton()
        //           .SelectResultElement(_staffReviewSetupid.ToString());


        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .ClickReviewedByIdLookUp();

        //       lookupPopup
        //          .WaitForLookupPopupToLoad()
        //          .SelectViewByText("Lookup View")
        //           .TypeSearchQuery("CW_Test_User_CDV6_13797_User2")
        //           .TapSearchButton()
        //           .SelectResultElement(_reviewedBySystemUserId.ToString());

        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .SelectStatusOption("Completed")
        //          .InsertNextReviewDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //          .InsertReviewStartTime("09:00")
        //          .InsertReviewEndTime("14:00")
        //          .ClickSaveAndCloseButton()
        //          .ValidateAppointmentAlertMessage("The Required Attendee, CW Test_User_CDV6_13797, does not contain a User Diary record for the Appointment time.");

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(1, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(staffReviews[0]);
        //       Assert.AreEqual(1, appointments.Count);
        //   }

        //   [TestProperty("JiraIssueID", "CDV6-14637")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews " +
        //       "-> Create New Record -> Fill all mandatory details , reviewed by and give any Next review date or start time or end time apart from Reviewed By user's work schedule and click Save ->" +
        //       "Should not create Staff review and appointment.Ans Should display proper error message of work schedule")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod014()
        //   {

        //       _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date.AddDays(1));
        //       dbHelper.staffReview.UpdateAppointmentStartAndEndTime(_staffReviewIdId, TimeSpan.FromHours(12), TimeSpan.FromHours(14));


        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText("CW_Test_User_CDV6_13797")
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .ClickCreateRecordButton();

        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .ClickReviewTypeIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .TypeSearchQuery("Staff Supervision")
        //           .TapSearchButton()
        //           .SelectResultElement(_staffReviewSetupid.ToString());


        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .ClickReviewedByIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .SelectViewByText("Lookup View")
        //           .TypeSearchQuery("CW_Test_User_CDV6_13797_User2")
        //           .TapSearchButton()
        //           .SelectResultElement(_reviewedBySystemUserId.ToString());

        //       staffReviewRecordPage
        //         .WaitForStaffReviewNewRecordCreatePageToLoad()
        //         .SelectStatusOption("Completed")
        //         .InsertNextReviewDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //         .InsertReviewStartTime("12:00")
        //         .InsertReviewEndTime("19:00")
        //         .ClickSaveAndCloseButton()
        //         .ValidateAppointmentAlertMessage("The Required Attendee, CW Test_User_CDV6_13797, does not contain a User Diary record for the Appointment time.");

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(1, staffReviews.Count);

        //       var appointments = dbHelper.appointment.GetAppointmentByRegardingID(staffReviews[0]);
        //       Assert.AreEqual(1, appointments.Count);


        //   }

        //   [TestProperty("JiraIssueID", "CDV6-14638")]
        //   [Description("Login CD ( Care Providers ) -> Settings -> Security -> System Users -> Select any existing User -> Menu -> Staff Reviews -> " +
        //   "Create New Record -> Fill all mandatory details , reviewed by ,Next review date or start time or end time and click Save ->Should not create Staff review and appointment. And" +
        //   "Should display proper error message of work schedule for both the user")]
        //   [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //   public void StaffReview_Appoinments_UITestMethod015()
        //   {

        //       dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());
        //       dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(_systemUserID, GetThisWeekFirstMonday());

        //       loginPage
        //           .GoToLoginPage()
        //           .Login(_loginUser, "Passw0rd_!", "Care Providers")
        //           .WaitFormHomePageToLoad(true, false, true);

        //       mainMenu
        //           .WaitForMainMenuToLoad()
        //           .NavigateToSystemUserSection();

        //       systemUsersPage
        //           .WaitForSystemUsersPageToLoad()
        //           .InsertQuickSearchText("CW_Test_User_CDV6_13797")
        //           .ClickQuickSearchButton()
        //           .WaitForResultsGridToLoad()
        //           .OpenRecord(_systemUserID.ToString());

        //       systemUserRecordPage
        //           .WaitForSystemUserRecordPageToLoad()
        //           .NavigateToStaffReviewSubPage();

        //       systemUserStaffReviewPage
        //           .WaitForSystemUserStaffReviewPageToLoad()
        //           .ClickCreateRecordButton();

        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .ClickReviewTypeIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .TypeSearchQuery("Staff Supervision")
        //           .TapSearchButton()
        //           .SelectResultElement(_staffReviewSetupid.ToString());


        //       staffReviewRecordPage
        //          .WaitForStaffReviewNewRecordCreatePageToLoad()
        //          .ClickReviewedByIdLookUp();

        //       lookupPopup
        //           .WaitForLookupPopupToLoad()
        //           .SelectViewByText("Lookup View")
        //           .TypeSearchQuery("CW_Test_User_CDV6_13797_User3")
        //           .TapSearchButton()
        //           .SelectResultElement(_reviewedBySystemUserId01.ToString());

        //       staffReviewRecordPage
        //         .WaitForStaffReviewNewRecordCreatePageToLoad()
        //         .ClickProviderLookUp();

        //       lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("Automation_Provider_Review").TapSearchButton().SelectResultElement(_provider_ReviewId.ToString());

        //       staffReviewRecordPage
        //         .WaitForStaffReviewNewRecordCreatePageToLoad()
        //         .SelectStatusOption("Completed")
        //         .InsertNextReviewDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        //         .InsertReviewStartTime("12:00")
        //         .InsertReviewEndTime("19:00")
        //         .ClickSaveAndCloseButton()
        //         .ValidateAppointmentAlertMessage("The Required Attendee, CW Test_User_CDV6_13797, does not contain a User Diary record for the Appointment time.");

        //       var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
        //       Assert.AreEqual(0, staffReviews.Count);
        //   }

        //#endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-16447

        [TestProperty("JiraIssueID", "ACC-3288")]
        [Description("Verify the Staff Review access for security Profile CW Staff Reviews(BU Edit) Pre Requisites :Logged in User should have the security profile CW Staff Reviews(BU Edit) Staff Review record exist for his default Business Unit")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_VerifyAcessForSecurityProfile_UITestMethod001()
        {
            _systemUserFirstName = "Test_User_CDV6_16944_";
            _systemUserLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "Test_User_CDV6_16944_" + _systemUserLastName;
            _newsystemUserID = dbHelper.systemUser.CreateSystemUser(_systemUserName, _systemUserFirstName, _systemUserLastName, _systemUserFirstName + _systemUserLastName, "Passw0rd_!", _systemUserName + "@somemail.com", _systemUserName + "@securemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_newsystemUserID, DateTime.Now.Date);

            foreach (var secProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_newsystemUserID))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfileId);

            var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_newsystemUserID, staffReviewsTeamEditSecurityProfileId);


            foreach (var _staffReviewIdId in dbHelper.staffReview.GetBySystemUserId(_newsystemUserID))
                dbHelper.staffReview.DeleteStaffReview(_staffReviewIdId);
            _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_newsystemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewIdId.ToString());

            staffReviewRecordPage
               .WaitForStaffReviewRecordPageToLoad()
               .ValidateAllFieldsEnableMode();
        }

        [TestProperty("JiraIssueID", "ACC-3289")]
        [Description("Verify the Staff Review access for security Profile CW Staff Reviews(BU View) Pre Requisites :Logged in User should have the security profile CW Staff Reviews(BU Edit) Staff Review record exist for his default Business Unit")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_VerifyAcessForSecurityProfile_UITestMethod002()
        {

            _systemUserFirstName = "Test_User_CDV6_16944_";
            _systemUserLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "Test_User_CDV6_16944_" + _systemUserLastName;
            _newsystemUserID = dbHelper.systemUser.CreateSystemUser(_systemUserName, _systemUserFirstName, _systemUserLastName, _systemUserFirstName + _systemUserLastName, "Passw0rd_!", _systemUserName + "@somemail.com", _systemUserName + "@securemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_newsystemUserID, DateTime.Now.Date);

            foreach (var secProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_newsystemUserID))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfileId);

            var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (View)").First();
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_newsystemUserID, staffReviewsTeamEditSecurityProfileId);


            foreach (var _staffReviewIdId in dbHelper.staffReview.GetBySystemUserId(_newsystemUserID))
                dbHelper.staffReview.DeleteStaffReview(_staffReviewIdId);
            _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_newsystemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                //.ClickRefreshButton()
                .OpenRecord(_staffReviewIdId.ToString());

            staffReviewRecordPage
               .WaitForStaffReviewRecordPageToLoad()
               .ValidateAllFieldsDisableMode();
        }

        [TestProperty("JiraIssueID", "ACC-3290")]
        [Description("Verify the Staff Review access for security Profile CW Staff Reviews (PCBU Edit) Pre Requisites :Logged in User should have the security profile CW Staff Reviews (PCBU Edit) Staff Review record exist for his default Business Unit")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_VerifyAcessForSecurityProfile_UITestMethod003()
        {
            _systemUserFirstName = "Test_User_CDV6_16944_";
            _systemUserLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "Test_User_CDV6_16944_" + _systemUserLastName;
            _newsystemUserID = dbHelper.systemUser.CreateSystemUser(_systemUserName, _systemUserFirstName, _systemUserLastName, _systemUserFirstName + _systemUserLastName, "Passw0rd_!", _systemUserName + "@somemail.com", _systemUserName + "@securemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_newsystemUserID, DateTime.Now.Date);

            foreach (var secProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_newsystemUserID))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfileId);

            var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_newsystemUserID, staffReviewsTeamEditSecurityProfileId);


            foreach (var _staffReviewIdId in dbHelper.staffReview.GetBySystemUserId(_newsystemUserID))
                dbHelper.staffReview.DeleteStaffReview(_staffReviewIdId);
            _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_newsystemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewIdId.ToString());

            staffReviewRecordPage
               .WaitForStaffReviewRecordPageToLoad()
               .ValidateAllFieldsEnableMode();
        }

        [TestProperty("JiraIssueID", "ACC-3291")]
        [Description("Verify the Staff Review access for security Profile CW Staff Reviews (PCBU View) Pre Requisites :Logged in User should have the security profile CW Staff Reviews (PCBU View) Staff Review record exist for his default Business Unit")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_VerifyAcessForSecurityProfile_UITestMethod004()
        {

            _systemUserFirstName = "Test_User_CDV6_16944_";
            _systemUserLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "Test_User_CDV6_16944_" + _systemUserLastName;
            _newsystemUserID = dbHelper.systemUser.CreateSystemUser(_systemUserName, _systemUserFirstName, _systemUserLastName, _systemUserFirstName + _systemUserLastName, "Passw0rd_!", _systemUserName + "@somemail.com", _systemUserName + "@securemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_newsystemUserID, DateTime.Now.Date);

            foreach (var secProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_newsystemUserID))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfileId);

            var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (View)").First();
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_newsystemUserID, staffReviewsTeamEditSecurityProfileId);


            foreach (var _staffReviewIdId in dbHelper.staffReview.GetBySystemUserId(_newsystemUserID))
                dbHelper.staffReview.DeleteStaffReview(_staffReviewIdId);
            _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_newsystemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewIdId.ToString());

            staffReviewRecordPage
               .WaitForStaffReviewRecordPageToLoad()
               .ValidateAllFieldsDisableMode();
        }

        [TestProperty("JiraIssueID", "ACC-3292")]
        [Description("Verify the Staff Review access for security Profile CW Staff Reviews (Org Edit) Pre Requisites :Logged in User should have the security profile CW Staff Reviews (Org Edit) Staff Review record exist for his default Business Unit")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_VerifyAcessForSecurityProfile_UITestMethod005()
        {

            _systemUserFirstName = "Test_User_CDV6_16944_";
            _systemUserLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "Test_User_CDV6_16944_" + _systemUserLastName;
            _newsystemUserID = dbHelper.systemUser.CreateSystemUser(_systemUserName, _systemUserFirstName, _systemUserLastName, _systemUserFirstName + _systemUserLastName, "Passw0rd_!", _systemUserName + "@somemail.com", _systemUserName + "@securemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_newsystemUserID, DateTime.Now.Date);

            foreach (var secProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_newsystemUserID))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfileId);

            var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_newsystemUserID, staffReviewsTeamEditSecurityProfileId);


            foreach (var _staffReviewIdId in dbHelper.staffReview.GetBySystemUserId(_systemUserID))
                dbHelper.staffReview.DeleteStaffReview(_staffReviewIdId);
            _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewIdId.ToString());

            staffReviewRecordPage
               .WaitForStaffReviewRecordPageToLoad()
               .ValidateAllFieldsEnableMode();
        }

        [TestProperty("JiraIssueID", "ACC-3293")]
        [Description("Verify the Staff Review access for security Profile CW Staff Reviews (Org View) Pre Requisites :Logged in User should have the security profile CW Staff Reviews (Org View) Staff Review record exist for his default Business Unit")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_VerifyAcessForSecurityProfile_UITestMethod006()
        {

            _systemUserFirstName = "Test_User_CDV6_16944_";
            _systemUserLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "Test_User_CDV6_16944_" + _systemUserLastName;
            _newsystemUserID = dbHelper.systemUser.CreateSystemUser(_systemUserName, _systemUserFirstName, _systemUserLastName, _systemUserFirstName + _systemUserLastName, "Passw0rd_!", _systemUserName + "@somemail.com", _systemUserName + "@securemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_newsystemUserID, DateTime.Now.Date);

            foreach (var secProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_newsystemUserID))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfileId);

            var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (View)").First();
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_newsystemUserID, staffReviewsTeamEditSecurityProfileId);


            foreach (var _staffReviewIdId in dbHelper.staffReview.GetBySystemUserId(_systemUserID))
                dbHelper.staffReview.DeleteStaffReview(_staffReviewIdId);
            _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewIdId.ToString());

            staffReviewRecordPage
               .WaitForStaffReviewRecordPageToLoad()
               .ValidateAllFieldsDisableMode();
        }

        [TestProperty("JiraIssueID", "ACC-3294")]
        [Description("Verify the Staff Review access for security Profile CW Staff Reviews (Team Edit) Pre Requisites :Logged in User should have the security profile CW Staff Reviews (Team Edit) Staff Review record exist for his default Business Unit")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_VerifyAcessForSecurityProfile_UITestMethod007()
        {

            _systemUserFirstName = "Test_User_CDV6_16944_";
            _systemUserLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "Test_User_CDV6_16944_" + _systemUserLastName;
            _newsystemUserID = dbHelper.systemUser.CreateSystemUser(_systemUserName, _systemUserFirstName, _systemUserLastName, _systemUserFirstName + _systemUserLastName, "Passw0rd_!", _systemUserName + "@somemail.com", _systemUserName + "@securemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_newsystemUserID, DateTime.Now.Date);

            dbHelper.systemUser.UpdateisManagerFld(_newsystemUserID);

            foreach (var secProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_newsystemUserID))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfileId);

            foreach (var secProfileId in dbHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(_careDirectorQA_TeamId))
                dbHelper.teamSecurityProfile.DeleteTeamSecurityProfile(secProfileId);

            var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();
            dbHelper.teamSecurityProfile.CreateTeamSecurityProfile(_careDirectorQA_TeamId, staffReviewsTeamEditSecurityProfileId);


            foreach (var _staffReviewIdId in dbHelper.staffReview.GetBySystemUserId(_newsystemUserID))
                dbHelper.staffReview.DeleteStaffReview(_staffReviewIdId);
            _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_newsystemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewIdId.ToString());

            staffReviewRecordPage
               .WaitForStaffReviewRecordPageToLoad()
               .ValidateAllFieldsEnableMode();
        }

        [TestProperty("JiraIssueID", "ACC-3295")]
        [Description("Verify the Staff Review access for security Profile CW Staff Reviews (Team View) Pre Requisites :Logged in User should have the security profile CW Staff Reviews (Team View) Staff Review record exist for his default Business Unit")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_VerifyAcessForSecurityProfile_UITestMethod008()
        {

            _systemUserFirstName = "Test_User_CDV6_16944_";
            _systemUserLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _systemUserName = "Test_User_CDV6_16944_" + _systemUserLastName;
            _newsystemUserID = dbHelper.systemUser.CreateSystemUser(_systemUserName, _systemUserFirstName, _systemUserLastName, _systemUserFirstName + _systemUserLastName, "Passw0rd_!", _systemUserName + "@somemail.com", _systemUserName + "@securemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_newsystemUserID, DateTime.Now.Date);

            dbHelper.systemUser.UpdateisManagerFld(_newsystemUserID);

            foreach (var secProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_newsystemUserID))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfileId);

            foreach (var secProfileId in dbHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(_careDirectorQA_TeamId))
                dbHelper.teamSecurityProfile.DeleteTeamSecurityProfile(secProfileId);

            var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (View)").First();
            dbHelper.teamSecurityProfile.CreateTeamSecurityProfile(_careDirectorQA_TeamId, staffReviewsTeamEditSecurityProfileId);


            foreach (var _staffReviewIdId in dbHelper.staffReview.GetBySystemUserId(_newsystemUserID))
                dbHelper.staffReview.DeleteStaffReview(_staffReviewIdId);
            _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_newsystemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewIdId.ToString());

            staffReviewRecordPage
               .WaitForStaffReviewRecordPageToLoad()
               .ValidateAllFieldsDisableMode();
        }

        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}
