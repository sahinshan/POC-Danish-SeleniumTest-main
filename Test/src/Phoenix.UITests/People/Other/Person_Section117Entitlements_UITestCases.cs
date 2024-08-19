using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UITests.Framework.PageObjects;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for Section 117 Entitlement Case Notes
    /// </summary>
    [TestClass]
    public class Person_Section117Entitlements_UITestCases : FunctionalTest
    {
        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _firstName;
        private string _lastName;
        private string _personFullname;
        private Guid _personId;
        private string _personNumber;
        private Guid _dataFormId;
        private Guid _contactReasonId;
        private Guid _caseStatusId;
        private Guid _activityPriorityId_Normal;
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityReasonId;
        private Guid _activityOutcomeId;
        private Guid _significantEventCategoryId;
        private Guid _significantEventSubCategoryId;

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Environment 

                _environmentName = ConfigurationManager.AppSettings["EnvironmentName"];

                #endregion

                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User PSection117MHAUser1

                _systemUsername = "PSection117MHAUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "PSection117MHA", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

                #endregion

                #region Activity Categories                

                _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Activity Sub Categories

                _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _teamId);

                #endregion

                #region Activity Reason

                _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Activity Priority

                _activityPriorityId_Normal = commonMethodsDB.CreateActivityPriority(new Guid("5246a13f-9d45-e911-a2c5-005056926fe4"), "Normal", new DateTime(2019, 1, 1), _teamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Significant Event Category

                _significantEventCategoryId = commonMethodsDB.CreateSignificantEventCategory("Category 1", new DateTime(2020, 1, 1), _teamId, null, null, null);

                #endregion

                #region Significant Event Sub Category

                _significantEventSubCategoryId = commonMethodsDB.CreateSignificantEventSubCategory(_teamId, "Sub Cat 1_1", _significantEventCategoryId, new DateTime(2020, 1, 1), null, null);

                #endregion

                #region Person

                _firstName = "John";
                _lastName = "LN_" + _currentDateString;
                _personFullname = _firstName + " " + _lastName;
                _personId = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId, new DateTime(2003, 1, 2));
                _personNumber = dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"].ToString();

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

        [TestProperty("JiraIssueID", "CDV6-25010")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a Section 117 Entitlement Case Note record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the record is properly cloned")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Section117EntitlementCaseNotes_Cloning_UITestMethod01()
        {
            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, _personId, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);

            #endregion

            #region Section 117 Entitlements
            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _teamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_teamId, _personId, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            Guid Section117EntitlementID = dbHelper.mhaAftercareEntitlement.CreateMHAAftercareEntitlement(_teamId, _personId,
                personMhaLegalStatusId, null, true, new DateTime(2021, 7, 7, 0, 0, 0), null, false);

            Guid Section117EntitlementCaseNoteID = dbHelper.mhaAftercareEntitlementCaseNote.CreateMHAAftercareEntitlementCaseNote(_teamId, _systemUserId,
                _personId, "Section 117 Entitlement Case Note 001", "Section 117 Entitlement Case Note Description", Section117EntitlementID,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId_Normal, true, _significantEventCategoryId, _significantEventSubCategoryId, new DateTime(2021, 7, 4), new DateTime(2021, 7, 5, 9, 25, 0), true);


            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToSection117EntitlementsPage();

            Section117EntitlementsPage
                .WaitForSection117EntitlementsPageToLoad()
                .OpenSection117EntitlementRecord(Section117EntitlementID.ToString());

            Section117EntitlementRecordPage
                .WaitForSection117EntitlementRecordPageToLoad()
                .NavigateToSection117EntitlementCaseNotesArea();

            Section117EntitlementCaseNotesPage
                .WaitForSection117EntitlementCaseNotesPageToLoad()
                .OpenSection117EntitlementCaseNoteRecord(Section117EntitlementCaseNoteID.ToString());

            Sectio117EntitlementCaseNoteRecordPage
                .WaitForSectio117EntitlementCaseNoteRecordPageToLoad()
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

            var records = dbHelper.caseCaseNote.GetByCaseID(_caseId);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var statusid = 1; //Open

            Assert.AreEqual("Section 117 Entitlement Case Note 001", fields["subject"]);
            Assert.AreEqual("Section 117 Entitlement Case Note Description", fields["notes"]);
            Assert.AreEqual(_personId.ToString(), fields["personid"].ToString());
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"]);
            Assert.AreEqual(_activityReasonId.ToString(), fields["activityreasonid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), fields["responsibleuserid"].ToString());
            Assert.AreEqual(_activityPriorityId_Normal.ToString(), fields["activitypriorityid"].ToString());
            Assert.AreEqual(_activityCategoryId.ToString(), fields["activitycategoryid"].ToString());
            Assert.AreEqual(new DateTime(2021, 7, 5, 9, 25, 0), ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(_activitySubCategoryId.ToString(), fields["activitysubcategoryid"].ToString());
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(_activityOutcomeId.ToString(), fields["activityoutcomeid"].ToString());
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(_significantEventCategoryId.ToString(), fields["significanteventcategoryid"].ToString());
            Assert.AreEqual(new DateTime(2021, 7, 4), (DateTime)fields["significanteventdate"]);
            Assert.AreEqual(_significantEventSubCategoryId.ToString(), fields["significanteventsubcategoryid"].ToString());
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(Section117EntitlementCaseNoteID.ToString(), fields["clonedfromid"].ToString());

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
