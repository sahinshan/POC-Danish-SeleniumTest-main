using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]
    public class Case_Tasks_UITestCases : FunctionalTest
    {

        #region Properties

        private string _tenantName;
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUserName;
        private Guid _contactReasonId;
        private Guid _systemUserId;
        private Guid _personID;
        private Guid caseid;
        private string caseNumber;
        private Guid _caseStatusId;
        private Guid _dataFormId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityReasonId;
        private Guid _activityPriorityId;
        private Guid _activityOutcomeId;
        private Guid _significantEventCategoryId;
        private Guid _significantEventSubCategoryId;
        private string firstName;
        private string lastName;

        #endregion

        [TestInitialize()]
        public void Case_CaseTask_SetupTest()
        {
            try
            {
                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion                

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region System User "CaseTaskUser1"

                _systemUserName = "CaseTaskUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseTask", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

                #endregion

                #region Activity Categories                

                _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Sub Categories

                _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

                #endregion

                #region Activity Reason

                _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Priority

                _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion                

                #region Significant Event Category
                _significantEventCategoryId = commonMethodsDB.CreateSignificantEventCategory("Default11404", DateTime.Now.Date, _careDirectorQA_TeamId, null, null, null);

                #endregion

                #region Significant Event Sub Category

                if (!dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("SubCategory11404").Any())
                {
                    dbHelper.significantEventSubCategory.CreateSignificantEventSubCategory(_careDirectorQA_TeamId, "SubCategory11404", _significantEventCategoryId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, null);
                }
                _significantEventSubCategoryId = dbHelper.significantEventSubCategory.SignificantEventSubCategoryByName("SubCategory11404").FirstOrDefault();

                #endregion

                #region Person

                firstName = "CaseTaskCloneAutomation";
                lastName = _currentDateSuffix;
                _personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2003, 1, 2));

                #endregion

                #region Case

                var startDate = new DateTime(2017, 03, 10);
                caseid = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, startDate, startDate, 20, "Case Case Note Information");
                caseNumber = (string)dbHelper.Case.GetCaseByID(caseid, "casenumber")["casenumber"];

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

        [TestProperty("JiraIssueID", "CDV6-11406")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a Task (For Case) record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Select the Person record (linked to the Case) as the destination record - Confirm the clone operation - Validate that the task record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseTasks_Cloning_UITestMethod01()
        {
            string caseTitle = (string)dbHelper.Case.GetCaseByID(caseid, "title")["title"];
            var CaseTaskID = dbHelper.task.CreateTask("Case Task 001", "Case Task Description", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, caseid, _personID,
                new DateTime(2017, 03, 10), caseid, caseTitle, "case", true, new DateTime(2017, 03, 10), _significantEventCategoryId, _significantEventSubCategoryId);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToTasksPage();

            caseTasksPage
                .WaitForCaseTasksPageToLoad()
                .OpenCaseTaskRecord(CaseTaskID.ToString());

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("Case Task 001")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Person")
                .SelectRetainStatus("Yes")
                .SelectRecord(_personID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.task.GetTaskByRegardingID(_personID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.task.GetTaskByID(records[0],
                "subject", "notes", "personid", "caseid", "ownerid", "activityreasonid", "regardingid", "regardingidname", "regardingidtablename", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "duedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var statusid = 1; //Open

            Assert.AreEqual("Case Task 001", fields["subject"]);
            Assert.AreEqual("Case Task Description", fields["notes"]);
            Assert.AreEqual(_personID.ToString(), fields["personid"]);
            Assert.AreEqual(false, fields.ContainsKey("caseid"));
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"]);
            Assert.AreEqual(_personID.ToString(), fields["regardingid"]);
            Assert.AreEqual(firstName + " " + lastName, fields["regardingidname"]);
            Assert.AreEqual("person", fields["regardingidtablename"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"]);
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"]);
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"]);
            Assert.AreEqual(new DateTime(2017, 03, 10).ToLocalTime(), ((DateTime)fields["duedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"]);
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"]);
            Assert.AreEqual(false, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), fields["significanteventcategoryid"]);
            Assert.AreEqual(new DateTime(2017, 03, 10).ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), fields["significanteventsubcategoryid"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(CaseTaskID.ToString(), fields["clonedfromid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-11407")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a Task (For Case) record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Select a Person record (with a relationship to the person linked to the case) as the destination record - Confirm the clone operation - " +
            "Validate that the task record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseTasks_Cloning_UITestMethod02()
        {
            #region Person Relationship
            var relatedPersonfirstName = "CaseTaskRelatedPerson";
            var relatedPersonlastName = _currentDateSuffix;
            var relatedPersonID = commonMethodsDB.CreatePersonRecord(relatedPersonfirstName, relatedPersonlastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2003, 1, 2));

            var brotherRelationshipTyeId = dbHelper.personRelationshipType.GetByName("Brother").FirstOrDefault();
            var sisterRelationshipTyeId = dbHelper.personRelationshipType.GetByName("Sister").FirstOrDefault();

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personID, firstName + " " + lastName, sisterRelationshipTyeId, "Sister", relatedPersonID,
                relatedPersonfirstName + " " + relatedPersonlastName, brotherRelationshipTyeId, "Brother",
               new DateTime(2017, 03, 10), "Relationship", 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 1, false);

            string caseTitle = (string)dbHelper.Case.GetCaseByID(caseid, "title")["title"];
            var CaseTaskID = dbHelper.task.CreateTask("Case Task 001", "Case Task Description", _careDirectorQA_TeamId, _systemUserId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, caseid, _personID,
                new DateTime(2017, 03, 10), caseid, caseTitle, "case", true, new DateTime(2017, 03, 10), _significantEventCategoryId, _significantEventSubCategoryId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseid.ToString())
                .OpenCaseRecord(caseid.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToTasksPage();

            caseTasksPage
                .WaitForCaseTasksPageToLoad()
                .OpenCaseTaskRecord(CaseTaskID.ToString());

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("Case Task 001")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Person")
                .SelectRetainStatus("Yes")
                .SelectRecord(relatedPersonID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            var records = dbHelper.task.GetTaskByRegardingID(relatedPersonID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.task.GetTaskByID(records[0],
                "subject", "notes", "personid", "caseid", "ownerid", "activityreasonid", "regardingid", "regardingidname", "regardingidtablename", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "duedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var statusid = 1; //Open

            Assert.AreEqual("Case Task 001", fields["subject"]);
            Assert.AreEqual("Case Task Description", fields["notes"]);
            Assert.AreEqual(relatedPersonID.ToString(), fields["personid"]);
            Assert.AreEqual(false, fields.ContainsKey("caseid"));
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"]);
            Assert.AreEqual(relatedPersonID.ToString(), fields["regardingid"]);
            Assert.AreEqual(relatedPersonfirstName + " " + relatedPersonlastName, fields["regardingidname"]);
            Assert.AreEqual("person", fields["regardingidtablename"]);
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"]);
            Assert.AreEqual(_activityPriorityId.ToString(), fields["activitypriorityid"]);
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"]);
            Assert.AreEqual(new DateTime(2017, 03, 10).ToLocalTime(), ((DateTime)fields["duedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"]);
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"]);
            Assert.AreEqual(false, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), fields["significanteventcategoryid"]);
            Assert.AreEqual(new DateTime(2017, 03, 10).ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), fields["significanteventsubcategoryid"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(CaseTaskID.ToString(), fields["clonedfromid"]);
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
