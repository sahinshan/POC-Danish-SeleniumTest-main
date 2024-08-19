using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_Tasks_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _defaultUsername;
        private Guid _defaultUserId;
        private string _defaultUserFullname;
        private string _systemUserName;
        private Guid _systemUserId;
        private string _systemUserFullName;
        private Guid _personId;
        private int personNumber;
        private string _person_fullName;
        private Guid _activityCategoryId_Assessment;
        private Guid _activityCategoryId_Advice;
        private Guid _activitySubCategoryId_HealthAssessment;
        private Guid _activitySubCategoryId_HomeSupport;
        private Guid _activityReasonId_Assessment;
        private Guid _activityPriorityId_Normal;
        private Guid _activityOutcomeId_Completed;
        private Guid _activityOutcomeId_MoreInformationNeeded;
        private Guid _significantEventCategoryId_Category1;
        private Guid _significantEventSubCategoryId_SubCat1_1;
        private Guid _significantEventSubCategoryId_SubCat1_2;
        private Guid _caseStatusId;
        private Guid _contactReasonId;
        private Guid _dataFormId_CommunityHealthCase;
        private Guid _contactSourceId;
        private Guid _providerId_Carer;
        private Guid _contactAdministrativeCategory;
        private Guid _caseServiceTypeRequestedid;
        private Guid _communityAndClinicTeamId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                _defaultUsername = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                _defaultUserId = dbHelper.systemUser.GetSystemUserByUserName(_defaultUsername).FirstOrDefault();
                _defaultUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultUserId, "fullname")["fullname"];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create SystemUser Record

                _systemUserName = "Person_Task_User_1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Task", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                _systemUserFullName = "Person Task User1";

                #endregion

                #region Person

                var firstName = "Automation";
                var lastName = _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
                personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _person_fullName = (string)dbHelper.person.GetPersonById(_personId, "fullname")["fullname"];

                #endregion

                #region Activity Categories                

                _activityCategoryId_Assessment = commonMethodsDB.CreateActivityCategory("Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);
                _activityCategoryId_Advice = commonMethodsDB.CreateActivityCategory("Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Sub Categories

                _activitySubCategoryId_HealthAssessment = commonMethodsDB.CreateActivitySubCategory("Health Assessment", new DateTime(2020, 1, 1), _activityCategoryId_Assessment, _careDirectorQA_TeamId);
                _activitySubCategoryId_HomeSupport = commonMethodsDB.CreateActivitySubCategory("Home Support", new DateTime(2020, 1, 1), _activityCategoryId_Advice, _careDirectorQA_TeamId);

                #endregion

                #region Activity Reason

                _activityReasonId_Assessment = commonMethodsDB.CreateActivityReason("Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Priority

                _activityPriorityId_Normal = commonMethodsDB.CreateActivityPriority("Normal", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId_Completed = commonMethodsDB.CreateActivityOutcome("Completed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);
                _activityOutcomeId_MoreInformationNeeded = commonMethodsDB.CreateActivityOutcome("More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Significant Event Category

                _significantEventCategoryId_Category1 = commonMethodsDB.CreateSignificantEventCategory("Category 1", DateTime.Now.Date, _careDirectorQA_TeamId, null, null, null);

                #endregion

                #region Significant Event Sub Category

                _significantEventSubCategoryId_SubCat1_2 = commonMethodsDB.CreateSignificantEventSubCategory(_careDirectorQA_TeamId, "Sub Cat 1_2", _significantEventCategoryId_Category1, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, null);
                _significantEventSubCategoryId_SubCat1_1 = commonMethodsDB.CreateSignificantEventSubCategory(_careDirectorQA_TeamId, "Sub Cat 1_1", _significantEventCategoryId_Category1, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, null);

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

                #endregion

                #region Data Form Community Health Case

                _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

                #endregion

                #region Contact Source

                _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Family", _careDirectorQA_TeamId);

                #endregion Contact Source

                #region Provider (Carer)

                _providerId_Carer = commonMethodsDB.CreateProvider("Ynys Mon - Local Health Board - Provider", _careDirectorQA_TeamId, 7);

                #endregion

                #region Contact Administrative Category

                _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

                #endregion

                #region Case Service Type Requested

                _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Medical Care", new DateTime(2020, 1, 1));

                #endregion

                #region Community and Clinic Team

                _communityAndClinicTeamId = commonMethodsDB.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Carer, _careDirectorQA_TeamId, "Ynys Mon - Local Health Board - Primary Team", "Created by Health Appointments");
                dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirectorQA_TeamId, _communityAndClinicTeamId, "Home Visit Data", new DateTime(2023, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-8418

        [TestProperty("JiraIssueID", "CDV6-11187")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a person Task record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the Task record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Task_Cloning_UITestMethod01()
        {
            #region Person Task

            var duedate = new DateTime(2021, 7, 5, 11, 15, 0, DateTimeKind.Utc);
            var significanteventdate = new DateTime(2021, 7, 4, 0, 0, 0, DateTimeKind.Utc);
            var statusid = 1; //Open
            bool IsSignificantEvent = true;
            bool InformationByThirdParty = true;

            Guid personTask1 = dbHelper.task.CreateTask("Task 001 All Fields Setup", "<p>Task 001 Description</p>", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Assessment, _activitySubCategoryId_HealthAssessment, _activityOutcomeId_Completed, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, duedate, _personId, _person_fullName, "person",
                IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_2, InformationByThirdParty);

            #endregion

            #region Case

            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personId, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-3).Date, DateTime.Now.AddDays(-2).Date, DateTime.Now.Date, "Unscheduled Appointments");

            string communityCaseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001 All Fields Setup")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(_caseId.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.task.GetTaskByCaseID(_caseId);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.task.GetTaskByID(records[0],
                "subject", "notes", "personid", "caseid", "ownerid", "activityreasonid", "regardingid", "regardingidname", "regardingidtablename", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "duedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            Assert.AreEqual("Task 001 All Fields Setup", fields["subject"]);
            Assert.AreEqual("<p>Task 001 Description</p>", fields["notes"]);
            Assert.AreEqual(_personId.ToString(), fields["personid"].ToString());
            Assert.AreEqual(_caseId.ToString(), fields["caseid"].ToString());
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual(_activityReasonId_Assessment.ToString(), fields["activityreasonid"].ToString());
            Assert.AreEqual(_caseId.ToString(), fields["regardingid"].ToString());
            Assert.AreEqual(communityCaseTitle.ToString(), fields["regardingidname"].ToString());
            Assert.AreEqual("case", fields["regardingidtablename"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityId_Normal.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryId_Assessment.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(duedate.ToLocalTime(), ((DateTime)fields["duedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId_HealthAssessment.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId_Completed.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId_Category1.ToString(), fields["significanteventcategoryid"].ToString());
            Assert.AreEqual(significanteventdate.ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(_significantEventSubCategoryId_SubCat1_2.ToString(), fields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(personTask1.ToString(), fields["clonedfromid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24901")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a person Task record (Task records should contain multiple custom fields that, for this record, have data set in them) - " +
            "Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that all custom fields are correctly cloned")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Task_Cloning_UITestMethod02()
        {
            var personID = new Guid("f4f051f0-de2f-4721-974b-0da92f5fedbc"); //Selma Ellis
            var personNumber = "109858";
            var caseID = new Guid("6de3f3dd-3540-e911-a2c5-005056926fe4");//CAS-3-297734
            var controlCaseID = new Guid("af2f7da3-e93a-e911-a2c5-005056926fe4"); //CAS-3-212576
            var personTaskID = new Guid("a18b048e-6fde-eb11-a325-005056926fe4"); //Task 01 All Fields Setup 

            //remove all cloned Tasks for the case record
            foreach (var recordid in dbHelper.task.GetTaskByCaseID(caseID))
                dbHelper.task.DeleteTask(recordid);

            //remove all cloned Tasks for the control case record
            foreach (var recordid in dbHelper.task.GetTaskByCaseID(controlCaseID))
                dbHelper.task.DeleteTask(recordid);


            loginPage
                .GoToLoginPage()
                .Login("PersonTasksUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTaskID.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001 All Fields Setup")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(caseID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.task.GetTaskByCaseID(caseID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.task.GetTaskByID(records[0],
                "qa_cloningtestmultilinetextbox", "qa_cloningtestboolfield", "qa_cloningtestweburl", "qa_cloningtestdate", "qa_cloningtestdateandtime", "qa_cloningtestdecimal", "qa_cloningtestemail",
                "qa_cloningtestfield", "qa_cloningtestmoney", "qa_cloningtestmultilinelargedatatextbox", "qa_cloningtestnumeric", "qa_cloningtestphone", "qa_cloningtestpicklistid", "qa_cloningtesttime");

            Assert.AreEqual("line 1\nline 2", fields["qa_cloningtestmultilinetextbox"]);
            Assert.AreEqual(true, fields["qa_cloningtestboolfield"]);
            Assert.AreEqual("https://www.google.com", fields["qa_cloningtestweburl"]);
            Assert.AreEqual(new DateTime(2021, 7, 8, 0, 0, 0, DateTimeKind.Utc).ToLocalTime(), ((DateTime)fields["qa_cloningtestdate"]).ToLocalTime());
            Assert.AreEqual(new DateTime(2021, 7, 7, 8, 25, 0, DateTimeKind.Utc).ToLocalTime(), ((DateTime)fields["qa_cloningtestdateandtime"]).ToLocalTime());
            Assert.AreEqual(43.21m, fields["qa_cloningtestdecimal"]);
            Assert.AreEqual("somemail@mail.com", fields["qa_cloningtestemail"]);
            Assert.AreEqual("Cloning Test Field Data single line", fields["qa_cloningtestfield"]);
            Assert.AreEqual(21.35m, fields["qa_cloningtestmoney"]);
            Assert.AreEqual("multi 1\nmulti 2", fields["qa_cloningtestmultilinelargedatatextbox"]);
            Assert.AreEqual(34, fields["qa_cloningtestnumeric"]);
            Assert.AreEqual("965478284", fields["qa_cloningtestphone"]);
            Assert.AreEqual(3, fields["qa_cloningtestpicklistid"]);
            Assert.AreEqual(new TimeSpan(13, 15, 0), fields["qa_cloningtesttime"]);
        }

        [TestProperty("JiraIssueID", "CDV6-24902")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a person Task record (with status set to cancelled) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Set Retain Status to Yes - Confirm the clone operation - Validate that the Task record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Task_Cloning_UITestMethod03()
        {
            #region Person Task

            Guid personTask1 = dbHelper.task.CreatePersonTask(_personId, _person_fullName, "Person Task 002", null, _careDirectorQA_TeamId);

            dbHelper.task.UpdateStatus(personTask1, 3);

            #endregion

            #region Case

            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personId, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-3).Date, DateTime.Now.AddDays(-2).Date, DateTime.Now.Date, "Person Task Cloning Check");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForInactivePersonTaskRecordPageToLoad("Person Task 002")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(_caseId.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.task.GetTaskByCaseID(_caseId);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.task.GetTaskByID(records[0], "subject", "statusid");
            var cancelledStatusid = 3; //Cancelled

            Assert.AreEqual("Person Task 002", fields["subject"]);
            Assert.AreEqual(cancelledStatusid.ToString(), fields["statusid"].ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24903")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a person Task record (with status set to cancelled) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Set Retain Status to No - Confirm the clone operation - Validate that the Task record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Task_Cloning_UITestMethod04()
        {
            #region Person Task

            Guid personTask1 = dbHelper.task.CreatePersonTask(_personId, _person_fullName, "Person Task 002", null, _careDirectorQA_TeamId);

            dbHelper.task.UpdateStatus(personTask1, 3);

            #endregion

            #region Case

            var _caseId = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, _personId, _systemUserId, _communityAndClinicTeamId, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, DateTime.Now.AddDays(-5).Date, DateTime.Now.AddDays(-3).Date, DateTime.Now.AddDays(-2).Date, DateTime.Now.Date, "Person Task Cloning Check");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForInactivePersonTaskRecordPageToLoad("Person Task 002")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("No")
                .SelectRecord(_caseId.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.task.GetTaskByCaseID(_caseId);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.task.GetTaskByID(records[0], "subject", "statusid");
            var openStatusid = 1; //Open

            Assert.AreEqual("Person Task 002", fields["subject"]);
            Assert.AreEqual(openStatusid.ToString(), fields["statusid"].ToString());
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-11544

        [TestProperty("JiraIssueID", "CDV6-11757")]
        [Description("Open a person record (person has no tasks linked to it) - Navigate to the Person Tasks screen - Validate that the No Records message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoadEmpty()
                .ValidateNoRecordsMessageVisibility(true);

        }

        [TestProperty("JiraIssueID", "CDV6-24904")]
        [Description("Open a person record (person has one task linked to it) - Navigate to the Person Tasks screen - Validate that the No Records message is NOT displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod02()
        {
            #region Person Task

            dbHelper.task.CreatePersonTask(_personId, _person_fullName, "Task 001", "notes....", _careDirectorQA_TeamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ValidateNoRecordsMessageVisibility(false);

        }

        [TestProperty("JiraIssueID", "CDV6-24905")]
        [Description("Open a person record (person has 1 task linked to it) - Navigate to the Person Tasks screen - Validate that the record is correctly displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod03()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = DateTime.Now.Date;

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "Task 001 description", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ValidateRecordVisible(personTask1.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24906")]
        [Description("Open a person record (person has 1 task linked to it with all fields set) - Navigate to the Person Tasks screen - Validate the content of each cell for the record row")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod04()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = DateTime.Now.Date;

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "Task 001 description", _careDirectorQA_TeamId, _systemUserId,
               _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
               "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ValidateRecordCellText(personTask1.ToString(), 2, "Task 001")
                .ValidateRecordCellText(personTask1.ToString(), 3, "20/05/2020 09:00:00")
                .ValidateRecordCellText(personTask1.ToString(), 4, "Open")
                .ValidateRecordCellText(personTask1.ToString(), 5, _person_fullName)
                .ValidateRecordCellText(personTask1.ToString(), 6, "Assessment")
                .ValidateRecordCellText(personTask1.ToString(), 7, "CareDirector QA")
                .ValidateRecordCellText(personTask1.ToString(), 8, _systemUserFullName)
                .ValidateRecordCellText(personTask1.ToString(), 9, _defaultUserFullname);

        }

        [TestProperty("JiraIssueID", "CDV6-24907")]
        [Description("Open a person record (person has 1 task linked to it with only the mandatory fields set) - Navigate to the Person Tasks screen - Validate the content of each cell for the record row")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod05()
        {
            #region Person Task

            Guid personTask1 = dbHelper.task.CreatePersonTask(_personId, _person_fullName, "Task 001", null, _careDirectorQA_TeamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ValidateRecordCellText(personTask1.ToString(), 2, "Task 001")
                .ValidateRecordCellText(personTask1.ToString(), 3, "")
                .ValidateRecordCellText(personTask1.ToString(), 4, "Open")
                .ValidateRecordCellText(personTask1.ToString(), 5, _person_fullName)
                .ValidateRecordCellText(personTask1.ToString(), 6, "")
                .ValidateRecordCellText(personTask1.ToString(), 7, "CareDirector QA")
                .ValidateRecordCellText(personTask1.ToString(), 8, "")
                .ValidateRecordCellText(personTask1.ToString(), 9, _defaultUserFullname);

        }

        [TestProperty("JiraIssueID", "CDV6-11772")]
        [Description("Open a person record (person has 3 task linked to it) - Navigate to the Person Tasks screen - Select 2 person task records - click on the delete button - " +
            "Confirm the delete operation - Validate that only the selected records are deleted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod06()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = DateTime.Now.Date;

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "Task 001 description", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            Guid personTask2 = dbHelper.task.CreateTask("Task 002", "Task 002 description", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            Guid personTask3 = dbHelper.task.CreateTask("Task 003", "Task 003 description", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .SelectPersonTaskRecord(personTask1.ToString())
                .SelectPersonTaskRecord(personTask2.ToString())
                .ClickDeletedButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("2 item(s) deleted.").TapOKButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ValidateRecordNotVisible(personTask1.ToString())
                .ValidateRecordNotVisible(personTask2.ToString())
                .ValidateRecordVisible(personTask3.ToString());

            var tasks = dbHelper.task.GetTaskByPersonID(_personId);
            Assert.AreEqual(1, tasks.Count);
            Assert.IsTrue(tasks.Contains(personTask3));

        }

        [TestProperty("JiraIssueID", "CDV6-24908")]
        [Description("Open a person record (person has 3 task linked to it) - Navigate to the Person Tasks screen - Search for a task record using the task subject text - " +
            "Validate that only the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod07()
        {
            #region Person Task

            Guid personTask1 = dbHelper.task.CreatePersonTask(_personId, _person_fullName, "Task 001", "notes....", _careDirectorQA_TeamId);
            Guid personTask2 = dbHelper.task.CreatePersonTask(_personId, _person_fullName, "Task 002", "notes....", _careDirectorQA_TeamId);
            Guid personTask3 = dbHelper.task.CreatePersonTask(_personId, _person_fullName, "Task 003", "notes....", _careDirectorQA_TeamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .SearchPersonTaskRecord("Task 002")

                .ValidateRecordVisible(personTask2.ToString())
                .ValidateRecordNotVisible(personTask1.ToString())
                .ValidateRecordNotVisible(personTask3.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24909")]
        [Description("Open a person record (person has 1 task linked to it) - Navigate to the Person Tasks screen - Click on the person task record - " +
            "Validate that the user is redirected to the task record page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod08()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = DateTime.Now.Date;

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "Task 001 description", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001");

        }

        [TestProperty("JiraIssueID", "CDV6-24910")]
        [Description("Open a person record (person has 1 task linked to it with data in all fields) - Navigate to the Person Tasks screen - Click on the person task record - " +
            "wait for the record page to load - Validate that all fields are correctly displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod09()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "<p>line 1</p>  <p>line 2</p>", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ValidateSubjectFieldText("Task 001")

                .LoadDescriptionRichTextBox()
                .ValidateDescriptionFieldText("1", "line 1")
                .ValidateDescriptionFieldText("2", "line 2")

                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ValidateRegardingLinkFieldText(_person_fullName)
                .ValidateReasonLinkFieldText("Assessment")
                .ValidatePriorityLinkFieldText("Normal")
                .ValidateDueDateText("20/05/2020", "09:00")
                .ValidateSelectedStatus("Open")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(false)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(true)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText(_systemUserFullName)
                .ValidateCategoryLinkFieldText("Advice")
                .ValidateSubCategoryLinkFieldText("Home Support")
                .ValidateOutcomeLinkFieldText("More information needed")
                .ValidateIsCaseNoteYesOptionChecked(false)
                .ValidateIsCaseNoteNoOptionChecked(true)

                .ValidateSignificantEventYesOptionChecked(true)
                .ValidateSignificantEventNoOptionChecked(false)
                .ValidateEventDateText(significanteventdate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEventCategoryLinkFieldText("Category 1")
                .ValidateEventSubCategoryLinkFieldText("Sub Cat 1_1")

                .ValidateIsClonedYesOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);

        }

        [TestProperty("JiraIssueID", "CDV6-24911")]
        [Description("Open a person record (person has 1 task linked to it with data only in the mandatory fields) - Navigate to the Person Tasks screen - " +
            "Click on the person task record - Wait for the record page to load - Validate that all fields are correctly displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod10()
        {
            #region Person Task

            Guid personTask1 = dbHelper.task.CreatePersonTask(_personId, _person_fullName, "Task 001", null, _careDirectorQA_TeamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ValidateSubjectFieldText("Task 001")

                .LoadDescriptionRichTextBox()
                .ValidateDescriptionFieldText("1", "")

                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ValidateRegardingLinkFieldText(_person_fullName)
                .ValidateReasonLinkFieldText("")
                .ValidatePriorityLinkFieldText("")
                .ValidateDueDateText("", "")
                .ValidateSelectedStatus("Open")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(false)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(true)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText("")
                .ValidateCategoryLinkFieldText("")
                .ValidateSubCategoryLinkFieldText("")
                .ValidateOutcomeLinkFieldText("")
                .ValidateIsCaseNoteYesOptionChecked(false)
                .ValidateIsCaseNoteNoOptionChecked(true)

                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)
                .ValidateEventDateFieldVisibility(false)
                .ValidateEventCategoryFieldVisibility(false)
                .ValidateEventSubCategoryFieldVisibility(false)

                .ValidateIsClonedYesOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);

        }

        [TestProperty("JiraIssueID", "CDV6-11773")]
        [Description("Open a person record (person has 1 task linked to it with data in all fields) - Navigate to the Person Tasks screen - Click on the person task record - " +
            "Wait for the record page to load - Remove the data from all non mandatory fields - Click on the save and close button - validate that the record is correctly saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod11()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "<p>line 1</p>  <p>line 2</p>", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .InsertDescription("")
                .ClickReasonRemoveButton()
                .ClickPriorityRemoveButton()
                .InsertDueDate("", "")
                .ClickResponsibleUserRemoveButton()
                .ClickCategoryRemoveButton()
                .ClickOutcomeRemoveButton()
                .ClickSaveAndCloseButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ValidateSubjectFieldText("Task 001")

                .LoadDescriptionRichTextBox()
                .ValidateDescriptionFieldText("1", "")

                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ValidateRegardingLinkFieldText(_person_fullName)
                .ValidateReasonLinkFieldText("")
                .ValidatePriorityLinkFieldText("")
                .ValidateDueDateText("", "")
                .ValidateSelectedStatus("Open")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(false)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(true)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText("")
                .ValidateCategoryLinkFieldText("")
                .ValidateSubCategoryLinkFieldText("")
                .ValidateOutcomeLinkFieldText("")
                .ValidateIsCaseNoteYesOptionChecked(false)
                .ValidateIsCaseNoteNoOptionChecked(true)

                .ValidateSignificantEventYesOptionChecked(true)
                .ValidateSignificantEventNoOptionChecked(false)
                .ValidateEventDateText(significanteventdate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEventCategoryLinkFieldText("Category 1")
                .ValidateEventSubCategoryLinkFieldText("Sub Cat 1_1")

                .ValidateIsClonedYesOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);

        }

        [TestProperty("JiraIssueID", "CDV6-11763")]
        [Description("Open a person record (person has 1 task linked to it with data only in the mandatory fields) - Navigate to the Person Tasks screen - Click on the person task record - " +
            "Wait for the record page to load - Update all editable fields - Click on the save button - Click on the Back button - Reopen the record - " +
            "Validate that the record is correctly Updated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod12()
        {
            #region Person Task

            Guid personTask1 = dbHelper.task.CreatePersonTask(_personId, _person_fullName, "Task 001", null, _careDirectorQA_TeamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .InsertSubject("Task 001 Updated")
                .InsertDescription("<p>line 1</p>  <p>line 2</p>")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonId_Assessment.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Normal").TapSearchButton().SelectResultElement(_activityPriorityId_Normal.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .InsertDueDate("08/07/2021", "07:55")
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId_Advice.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId_HomeSupport.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId_MoreInformationNeeded.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ClickContainsInformationProvidedByThirdPartyYesRadioButton()
                .ClickIsCaseNoteYesRadioButton()
                .ClickSignificantEventYesRadioButton()
                .InsertEventDate("13/07/2021")
                .ClickEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Category 1").TapSearchButton().SelectResultElement(_significantEventCategoryId_Category1.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ClickEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Sub Cat 1_1").TapSearchButton().SelectResultElement(_significantEventSubCategoryId_SubCat1_1.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ClickSaveButton()
                .ClickBackButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001 Updated")
                .ValidateSubjectFieldText("Task 001 Updated")

                .LoadDescriptionRichTextBox()
                .ValidateDescriptionFieldText("1", "line 1")
                .ValidateDescriptionFieldText("2", "line 2")

                .WaitForPersonTaskRecordPageToLoad("Task 001 Updated")
                .ValidateRegardingLinkFieldText(_person_fullName)
                .ValidateReasonLinkFieldText("Assessment")
                .ValidatePriorityLinkFieldText("Normal")
                .ValidateDueDateText("08/07/2021", "07:55")
                .ValidateSelectedStatus("Open")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(false)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText(_systemUserFullName)
                .ValidateCategoryLinkFieldText("Advice")
                .ValidateSubCategoryLinkFieldText("Home Support")
                .ValidateOutcomeLinkFieldText("More information needed")
                .ValidateIsCaseNoteYesOptionChecked(true)
                .ValidateIsCaseNoteNoOptionChecked(false)

                .ValidateSignificantEventYesOptionChecked(true)
                .ValidateSignificantEventNoOptionChecked(false)
                .ValidateEventDateText("13/07/2021")
                .ValidateEventCategoryLinkFieldText("Category 1")
                .ValidateEventSubCategoryLinkFieldText("Sub Cat 1_1")

                .ValidateIsClonedYesOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);

        }

        [TestProperty("JiraIssueID", "CDV6-24912")]
        [Description("Open a person record (person has 1 task linked to it with data in all fields) - Navigate to the Person Tasks screen - Click on the person task record - " +
            "wait for the record page to load - Remove the value from the subject field - click on the save and close button - Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod13()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "<p>line 1</p>  <p>line 2</p>", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, "Kristine Pollard",
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .InsertSubject("")
                .InsertEventDate("")
                .ClickEventCategoryRemoveButton()

                .ClickSaveAndCloseButton()

                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateSubjectErrorLabelVisible(true)
                .ValidateSubjectErrorLabelText("Please fill out this field.")
                .ValidateEventDateErrorLabelVisible(true)
                .ValidateEventDateErrorLabelText("Please fill out this field.")
                .ValidateEventCategoryErrorLabelVisible(true)
                .ValidateEventCategoryErrorLabelText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-24913")]
        [Description("Open a person record (person has 1 task linked to it with data in all fields) - Navigate to the Person Tasks screen - Click on the person task record - " +
            "wait for the record page to load - Update the value from the subject field - click on the back button - Validate that a warning is displayed to the user - " +
            "Confirm the back operation - Validate that the subject field was not updated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod14()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "<p>line 1</p>  <p>line 2</p>", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .InsertSubject("Task 001 Updated");

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnCloseIcon();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ValidateSubjectFieldText("Task 001");

        }

        [TestProperty("JiraIssueID", "CDV6-24914")]
        [Description("Open a person record (person has 1 task linked to it with data in all fields) - Navigate to the Person Tasks screen - Click on the person task record - " +
            "wait for the record page to load - Update the value from the subject field - click on the back button - Validate that a warning is displayed to the user - " +
            "click on the cancel button on the alert - validate that the alert is closed - click on the back button again - Validate that the alert is displayed again")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod15()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "<p>line 1</p>  <p>line 2</p>", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .InsertSubject("Task 001 Updated");

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnCloseIcon();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapCancelButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001");

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnCloseIcon();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ValidateSubjectFieldText("Task 001");

        }

        [TestProperty("JiraIssueID", "CDV6-11764")]
        [Description("Open a person record - Navigate to the Person Tasks screen - Click on the add new record button - " +
            "Wait for the new record page to load - Set data in all fields - Click on the save and close button - Validate that a new record is displayed - Open the record - " +
            "Validate that the record is correctly Saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod16()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .InsertSubject("Task 001")
                .InsertDescription("<p>line 1</p>  <p>line 2</p>")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonId_Assessment.ToString());


            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Normal").TapSearchButton().SelectResultElement(_activityPriorityId_Normal.ToString());


            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .InsertDueDate("08/07/2021", "07:55")
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_systemUserId.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId_Advice.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId_HomeSupport.ToString());


            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId_MoreInformationNeeded.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .ClickContainsInformationProvidedByThirdPartyYesRadioButton()
                .ClickIsCaseNoteYesRadioButton()
                .ClickSignificantEventYesRadioButton()
                .InsertEventDate("13/07/2021")
                .ClickEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Category 1").TapSearchButton().SelectResultElement(_significantEventCategoryId_Category1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .ClickEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Sub Cat 1_1").TapSearchButton().SelectResultElement(_significantEventSubCategoryId_SubCat1_1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .ClickSaveAndCloseButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad();

            var records = dbHelper.task.GetTaskByRegardingID(_personId);
            Assert.AreEqual(1, records.Count);
            var personTask1 = records.First();

            personTasksPage
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ValidateSubjectFieldText("Task 001")

                .LoadDescriptionRichTextBox()
                .ValidateDescriptionFieldText("1", "line 1")
                .ValidateDescriptionFieldText("2", "line 2")

                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ValidateRegardingLinkFieldText(_person_fullName)
                .ValidateReasonLinkFieldText("Assessment")
                .ValidatePriorityLinkFieldText("Normal")
                .ValidateDueDateText("08/07/2021", "07:55")
                .ValidateSelectedStatus("Open")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(false)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText(_systemUserFullName)
                .ValidateCategoryLinkFieldText("Advice")
                .ValidateSubCategoryLinkFieldText("Home Support")
                .ValidateOutcomeLinkFieldText("More information needed")
                .ValidateIsCaseNoteYesOptionChecked(true)
                .ValidateIsCaseNoteNoOptionChecked(false)

                .ValidateSignificantEventYesOptionChecked(true)
                .ValidateSignificantEventNoOptionChecked(false)
                .ValidateEventDateText("13/07/2021")
                .ValidateEventCategoryLinkFieldText("Category 1")
                .ValidateEventSubCategoryLinkFieldText("Sub Cat 1_1")

                .ValidateIsClonedYesOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);

            var significantEventRecords = dbHelper.personSignificantEvent.GetPersonSignificantEventByPersonID(_personId);
            Assert.AreEqual(1, significantEventRecords.Count);
            var newSignificantEventRecordId = significantEventRecords.FirstOrDefault();

            var fields = dbHelper.personSignificantEvent.GetPersonSignificantEventByID(newSignificantEventRecordId,
                "ownerid", "owningbusinessunitid", "title", "inactive", "eventdate", "eventdetails", "significanteventcategoryid", "significanteventsubcategoryid"
                , "sourceactivityid", "sourceactivityidtablename", "sourceactivityidname", "iscloned");


            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"].ToString());
            StringAssert.Contains((string)fields["title"], "Significant Event for " + _person_fullName + " created by " + _systemUserFullName + " on");
            Assert.AreEqual(false, fields["inactive"]);
            Assert.AreEqual(new DateTime(2021, 07, 13), fields["eventdate"]);
            Assert.AreEqual(false, fields.ContainsKey("eventdetails"));
            Assert.AreEqual(_significantEventCategoryId_Category1.ToString(), fields["significanteventcategoryid"].ToString());
            Assert.AreEqual(_significantEventSubCategoryId_SubCat1_1.ToString(), fields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(personTask1.ToString(), fields["sourceactivityid"].ToString());
            Assert.AreEqual("task", fields["sourceactivityidtablename"]);
            Assert.AreEqual("Task 001", fields["sourceactivityidname"]);
            Assert.AreEqual(false, fields["iscloned"]);

        }

        [TestProperty("JiraIssueID", "CDV6-11760")]
        [Description("Open a person record - Navigate to the Person Tasks screen - Click on the add new record button - " +
            "Wait for the new record page to load - Set data in the mandatory fields only - Click on the save and close button - Validate that a new record is displayed - Open the record - " +
            "Validate that the record is correctly Saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod17()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .InsertSubject("Task 001")
                .ClickResponsibleUserRemoveButton()
                .ClickSaveAndCloseButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad();

            var records = dbHelper.task.GetTaskByRegardingID(_personId);
            Assert.AreEqual(1, records.Count);
            var personTask1 = records.First();

            personTasksPage
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ValidateSubjectFieldText("Task 001")

                .LoadDescriptionRichTextBox()
                .ValidateDescriptionFieldText("1", "")

                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ValidateRegardingLinkFieldText(_person_fullName)
                .ValidateReasonLinkFieldText("")
                .ValidatePriorityLinkFieldText("")
                .ValidateDueDateText("", "")
                .ValidateSelectedStatus("Open")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(false)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(true)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText("")
                .ValidateCategoryLinkFieldText("")
                .ValidateSubCategoryLinkFieldText("")
                .ValidateOutcomeLinkFieldText("")
                .ValidateIsCaseNoteYesOptionChecked(false)
                .ValidateIsCaseNoteNoOptionChecked(true)

                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)
                .ValidateEventDateFieldVisibility(false)
                .ValidateEventCategoryFieldVisibility(false)
                .ValidateEventSubCategoryFieldVisibility(false)

                .ValidateIsClonedYesOptionChecked(false)
                .ValidateIsClonedNoOptionChecked(true);

        }

        [TestProperty("JiraIssueID", "CDV6-11758")]
        [Description("Open a person record - Navigate to the Person Tasks screen - Click on the add new record button - " +
            "Wait for the new record page to load - Set Significant Event to Yes - Leave the Status empty - Remove the Responsible Team - Do not set data in any of the mandatory fields - " +
            "Click on the save and close button - Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod18()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .SelectStatus("")
                .ClickResponsibleTeamRemoveButton()
                .ClickResponsibleUserRemoveButton()
                .ClickSignificantEventYesRadioButton()
                .ClickSaveAndCloseButton()

                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateSubjectErrorLabelVisible(true)
                .ValidateSubjectErrorLabelText("Please fill out this field.")
                .ValidateStatusErrorLabelVisible(true)
                .ValidateStatusErrorLabelText("Please fill out this field.")
                .ValidateResponsibleTeamErrorLabelVisible(true)
                .ValidateResponsibleTeamErrorLabelText("Please fill out this field.")
                .ValidateEventDateErrorLabelVisible(true)
                .ValidateEventDateErrorLabelText("Please fill out this field.")
                .ValidateEventCategoryErrorLabelVisible(true)
                .ValidateEventCategoryErrorLabelText("Please fill out this field.");

            var records = dbHelper.task.GetTaskByRegardingID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-24915")]
        [Description("Open a person record (person has 1 task linked to it) - Navigate to the Person Tasks screen - " +
            "Click on the person task record - Wait for the record page to load - Click on the delete button - Confirm the delete operation - Validate that the record is removed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod19()
        {
            #region Person Task

            Guid personTask1 = dbHelper.task.CreatePersonTask(_personId, _person_fullName, "Task 001", null, _careDirectorQA_TeamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ValidateRecordNotVisible(personTask1.ToString());

            var records = dbHelper.task.GetTaskByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-11759")]
        [Description("Open a person record - Navigate to the Person Tasks screen - Click on the add new record button - " +
            "Wait for the new record page to load - Remove the Status - Remove the Responsible Team - " +
            "Click on the save and close button - Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod20()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .SelectStatus("")
                .ClickResponsibleTeamRemoveButton()
                .ClickResponsibleUserRemoveButton()
                .ClickSignificantEventYesRadioButton()
                .ClickSaveAndCloseButton()

                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateSubjectErrorLabelVisible(true)
                .ValidateSubjectErrorLabelText("Please fill out this field.")
                .ValidateStatusErrorLabelVisible(true)
                .ValidateStatusErrorLabelText("Please fill out this field.")
                .ValidateResponsibleTeamErrorLabelVisible(true)
                .ValidateResponsibleTeamErrorLabelText("Please fill out this field.")
                .ValidateEventDateErrorLabelVisible(true)
                .ValidateEventCategoryErrorLabelVisible(true);


            var records = dbHelper.task.GetTaskByRegardingID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-11761")]
        [Description("Open a person record (person has 1 task linked to it with data in all fields) - Navigate to the Person Tasks screen - Click on the person task record - " +
            "Wait for the record page to load - Set the status to Completed - Click on the save button - validate that the record is correctly saved and gets disabled")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod21()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "<p>line 1</p>  <p>line 2</p>", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .SelectStatus("Completed")
                .ClickSaveButton()
                .WaitForInactivePersonTaskRecordPageToLoad("Task 001")
                .ValidateSelectedStatus("Completed");

        }

        [TestProperty("JiraIssueID", "CDV6-11762")]
        [Description("Open a person record (person has 1 task linked to it with data in all fields) - Navigate to the Person Tasks screen - Click on the person task record - " +
            "Wait for the record page to load - Set the status to Cancelled - Click on the save button - validate that the record is correctly saved and gets disabled")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod22()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "<p>line 1</p>  <p>line 2</p>", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .SelectStatus("Cancelled")
                .ClickSaveButton()
                .WaitForInactivePersonTaskRecordPageToLoad("Task 001")
                .ValidateSelectedStatus("Cancelled");

        }

        [TestProperty("JiraIssueID", "CDV6-11765")]
        [Description("Open a person record - Navigate to the Person Tasks screen - Click on the add new record button - " +
            "Wait for the new record page to load - Set data in the mandatory fields only - Click on the save and close button - Validate that a new record is displayed - Validate that no significant event record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod23()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .InsertSubject("Task 001")
                .ClickResponsibleUserRemoveButton()
                .ClickSaveAndCloseButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad();

            var records = dbHelper.task.GetTaskByRegardingID(_personId);
            Assert.AreEqual(1, records.Count);

            var significantEventRecords = dbHelper.personSignificantEvent.GetPersonSignificantEventByPersonID(_personId);
            Assert.AreEqual(0, significantEventRecords.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-11766")]
        [Description("Open a person record (person has 1 task linked to it with cancelled status) - Navigate to the Person Tasks screen - " +
            "Click on the person task record - Wait for the record page to load - CLick on the activate button - Validate that the record gets activated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod24()
        {
            #region Person Task

            Guid personTask1 = dbHelper.task.CreatePersonTask(_personId, _person_fullName, "Task 001", null, _careDirectorQA_TeamId);
            dbHelper.task.UpdateStatus(personTask1, 3);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForInactivePersonTaskRecordPageToLoad("Task 001")
                .ClickActivateButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.").TapOKButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .InsertSubject("Task 001 Updated");

        }

        [TestProperty("JiraIssueID", "CDV6-11767")]
        [Description("Open a person record - Navigate to the Person Tasks screen - Click on the add new record button - Wait for the new record page to load - " +
            "Set data in the mandatory fields only - Click on the save and close button - Navigate to the person Timeline tab - " +
            "Validate that the information for the newly created record is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod25()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("New")
                .InsertSubject("Task 001")
                .ClickResponsibleUserRemoveButton()
                .ClickSaveAndCloseButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad();

            var records = dbHelper.task.GetTaskByRegardingID(_personId);
            Assert.AreEqual(1, records.Count);
            var personTask1 = records.First();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ValidateRecordPresent(personTask1.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-11768")]
        [Description("Open a person record (person has 1 task linked to it with data in all fields) - Navigate to the Person Tasks screen - Click on the person task record - " +
            "Wait for the record page to load - Click on the Complete button - Validate that the record status is changed to Completed and the record gets disabled")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod26()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "<p>line 1</p>  <p>line 2</p>", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ClickCompleteButton()
                .WaitForInactivePersonTaskRecordPageToLoad("Task 001")
                .ValidateSelectedStatus("Completed");

        }

        [TestProperty("JiraIssueID", "CDV6-11770")]
        [Description("Open a person record (person has 1 task linked to it with data in all fields) - Navigate to the Person Tasks screen - Click on the person task record - " +
            "Wait for the record page to load - Click on the Cancel button - Validate that the record status is changed to Cancelled and the record gets disabled")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod27()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "<p>line 1</p>  <p>line 2</p>", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ClickCancelButton()
                .WaitForInactivePersonTaskRecordPageToLoad("Task 001")
                .ValidateSelectedStatus("Cancelled");

        }

        [TestProperty("JiraIssueID", "CDV6-11771")]
        [Description("Open a person record (person has 1 task linked to it with data in all fields) - Navigate to the Person Tasks screen - Click on the person task record - " +
            "Wait for the record page to load - Click on the Cancel button - Navigate to the Audit sub-page - Validate that the cancel operation gets audited")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void PersonTask_UITestMethod28()
        {
            #region Person Task

            DateTime date1 = new DateTime(2020, 5, 20, 9, 0, 0);
            bool IsSignificantEvent = true;
            DateTime significanteventdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid personTask1 = dbHelper.task.CreateTask("Task 001", "<p>line 1</p>  <p>line 2</p>", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId_Advice, _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, null, _personId, date1, _personId, _person_fullName,
                "person", IsSignificantEvent, significanteventdate, _significantEventCategoryId_Category1, _significantEventSubCategoryId_SubCat1_1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(personTask1.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task 001")
                .ClickCancelButton()
                .WaitForInactivePersonTaskRecordPageToLoad("Task 001")
                .NavigateToAuditSubPage();

            auditListPage
                .WaitForAuditListPageToLoad("task");

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = personTask1.ToString(),
                ParentTypeName = "task",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual(_systemUserFullName, auditResponseData.GridData[0].cols[1].Text);

        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}
