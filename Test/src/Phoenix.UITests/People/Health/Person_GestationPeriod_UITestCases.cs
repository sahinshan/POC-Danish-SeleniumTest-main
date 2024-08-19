using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Health
{
    [TestClass]
    public class Person_GestationPeriod_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private string _defaultUsername;
        private Guid _defaultUserId;
        private string _defaultUserFullname;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUserName;
        private Guid _personId;
        private Guid _childId;
        private int _personNumber;
        private int _childNumber;
        private string _childPersonFullName;
        private string _personFullName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _endReasonId;

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create SystemUser Record

                _systemUserName = "Person_Gestation_Period_User_1";
                commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Gestation Period", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Person

                var firstName = "Automation";
                var lastName = _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _personFullName = (string)dbHelper.person.GetPersonById(_personId, "fullname")["fullname"];

                #endregion

                #region Child Record (Person)

                _childId = commonMethodsDB.CreatePersonRecord("Child", "R_" + _currentDateSuffix, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
                _childNumber = (int)dbHelper.person.GetPersonById(_childId, "personnumber")["personnumber"];
                _childPersonFullName = "Child R_" + _currentDateSuffix;

                #endregion

                #region Gestation Periods End Reasons

                _endReasonId = commonMethodsDB.CreateGestationPeriodEndReason(_teamId, "Live Delivery", new DateTime(2020, 1, 1));

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-12507

        [TestProperty("JiraIssueID", "CDV6-12600")]
        [Description("Navigate to Workplace-People- Open the Mother Person Record" + "Navigate to Menu-Health-Gestation period" + "Click on Add New Button"
            + "Click on Save" + "Verify the Notification Messages for the Mandatory fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .ClickNewRecordButton();

            personGestationPeriodRecordPage
                .WaitForPersonGestationRecordPageToLoad("New")
                .ClickSaveButton()
                .ValidateNotificationMessage(true)
                .ValidateNumberFieldNotificationMessage(true)
                .ValidateDaysWeeksFieldNotificationMessage(true)
                .ValidateStartFieldNotificationMessage(true);

        }

        //Bug ID:CDV6-12647
        [TestProperty("JiraIssueID", "CDV6-12601")]
        [Description("Navigate to Workplace-People- Open the Mother Person Record" + "Navigate to Menu-Health-Gestation period" + "Click on Add New Button"
            + "Click on Save" + " Verify that End Date validation is displayed when user select the End Date as before Start Date.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases02()
        {
            #region Gestation Period

            var startDate = DateTime.Now.AddDays(-5);
            var endDate = DateTime.Now.AddDays(-25);

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .OpenGestationPeriodRecord(GestationPeriod.ToString());

            personGestationPeriodRecordPage
                .WaitForPersonGestationPeriodRecordPageToLoad("Person Gestation Period for " + _personFullName + " created by " + _defaultUserFullname + " on")
                .InsertEndDate(endDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("End Date can not be less than Start Date");

        }

        [TestProperty("JiraIssueID", "CDV6-12602")]
        [Description("Navigate to Workplace-People- Open the Mother Person Record" + "Navigate to Menu-Health-Gestation period" + "Click on Add New Button"
            + "Click on Save" + " Verify that End Date validation is displayed when user select the End Date as future date.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases03()
        {
            #region Gestation Period

            var startDate = DateTime.Now.AddDays(-5);
            var endDate = DateTime.Now.AddDays(25);

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .OpenGestationPeriodRecord(GestationPeriod.ToString());

            personGestationPeriodRecordPage
                .WaitForPersonGestationPeriodRecordPageToLoad("Person Gestation Period for " + _personFullName + " created by " + _defaultUserFullname + " on")
                .InsertEndDate(endDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("End Date can not be in Future");

        }

        [TestProperty("JiraIssueID", "CDV6-12603")]
        [Description(" Verify that user is able to create Gestation Period record by entering appropriate values in all required fields and user is returned to the Gestation Periods default view page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases04()
        {
            #region Gestation Period

            var startDate = DateTime.Now.AddDays(-5);
            var endDate = DateTime.Now.Date;

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .OpenGestationPeriodRecord(GestationPeriod.ToString());

            personGestationPeriodRecordPage
                .WaitForPersonGestationPeriodRecordPageToLoad("Person Gestation Period for " + _personFullName + " created by " + _defaultUserFullname + " on")
                .InsertEndDate(endDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad();

            var records = dbHelper.personGestationPeriod.GetPersonGestationPeriodIdByPersonId(_personId);
            Assert.AreEqual(1, records.Count);

            var gestationPeriodRecords = dbHelper.personGestationPeriod.GetPersonGestationPeriodByID(records[0], "ownerid", "ChildId", "startDate", "totaldaysorweeks", "gestationperiodtypeid", "gestationendreasonid", "notes");

            Assert.AreEqual(_teamId.ToString(), gestationPeriodRecords["ownerid"].ToString());
            Assert.AreEqual(_childId.ToString(), gestationPeriodRecords["childid"].ToString());
            Assert.AreEqual(startDate.Date, gestationPeriodRecords["startdate"]);
            Assert.AreEqual(785, gestationPeriodRecords["totaldaysorweeks"]);
            Assert.AreEqual(1, gestationPeriodRecords["gestationperiodtypeid"]);
            Assert.AreEqual(_endReasonId.ToString(), gestationPeriodRecords["gestationendreasonid"].ToString());
            Assert.AreEqual("Gestation Details:", gestationPeriodRecords["notes"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12604")]
        [Description(" Verify System should be auto created the Personal Relationships record for both mother and child records ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases05()
        {
            #region Gestation Period / Relationship

            var startDate = DateTime.Now.AddDays(-5);
            dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate, _endReasonId, "Gestation Details:");

            var RelationshipRecord = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad()
                .ValidateRecordCellText(RelationshipRecord[0].ToString(), 4, "Yes")
                .ValidateRecordCellText(RelationshipRecord[0].ToString(), 5, "Yes");

        }

        [TestProperty("JiraIssueID", "CDV6-12616")]
        [Description("Navigate to Menu->Health->Gestation Periods->Create a new record for the same child which is already having relationship with mother(Family member = Yes)" +
          "Open the Mother and Child records given in the Gestation Period->Menu->Care Network->Relationships->" +
           "Verify that Person Relationship record is not auto created and updated between mother and child when child is not equal to null in the Gestation period")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases06()
        {
            #region Gestation Period / Relationship

            var startDate = DateTime.Now.AddDays(-5);
            dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate, _endReasonId, "Gestation Details:");
            var RelationshipRecord = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId);

            //Create 2nd Gestation Record for the Same Mother and Child
            dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate, _endReasonId, " 2nd Gestation Record Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad()
                .ValidateRecordCellText(RelationshipRecord[0].ToString(), 4, "Yes")
                .ValidateRecordCellText(RelationshipRecord[0].ToString(), 5, "Yes");

            var RelationshipsRecords = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId);
            Assert.AreEqual(1, RelationshipsRecords.Count);

            System.Threading.Thread.Sleep(2000);

            var GestationPeriodRecords = dbHelper.personGestationPeriod.GetPersonGestationPeriodIdByPersonId(_personId);
            Assert.AreEqual(2, GestationPeriodRecords.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12617")]
        [Description("Create a Gestation period record , Relationship Record should be auto created" +
            "Open the relavent Relationship record and change the family member status to No" + "Create a new Gestation record for the same child again" +
            "Verify that Personal Relationship record get updated between mother and child when child ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases07()
        {
            #region Gestation Period / Relationship

            var startDate = DateTime.Now.AddDays(-5);
            dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate, _endReasonId, "Gestation Details:");

            var RelationshipRecord = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId);
            var RelationshipChildRecord = RelationshipRecord.FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad()
                .OpenPersonRelationshipRecord(RelationshipChildRecord.ToString());

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad()
                .SelectFamilyMembers("2")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            personRelationshipPage
             .WaitForPersonRelationshipPageToLoad();

            var RelationshipsRecords = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId);
            Assert.AreEqual(1, RelationshipsRecords.Count);

            Guid GestationPeriod1 = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate, _endReasonId, "Gestation Details:");

            var RelationshipsUpdatedRecord = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId);
            Assert.AreEqual(1, RelationshipsUpdatedRecord.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad()
                .ValidateRecordCellText(RelationshipRecord[0].ToString(), 4, "Yes")
                .ValidateRecordCellText(RelationshipRecord[0].ToString(), 5, "Yes");

        }

        [TestProperty("JiraIssueID", "CDV6-12622")]
        [Description("Navigate to Menu -> Health -> Gestation Periods -> Create a new record when child is equal to null -> Navigate to Menu -> Care Network -> Relationships ->  " +
         "Verify that system is not auto created the personal relationship record between mother and child when child is equal to null in the Gestation period.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases08()
        {
            #region Gestation Period

            var startDate = DateTime.Now.AddDays(-5);
            dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, null, 785, 1, startDate, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad();

            var RelationshipsRecords = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId);
            Assert.AreEqual(0, RelationshipsRecords.Count);

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad();

            var GestationPeriodRecords = dbHelper.personGestationPeriod.GetPersonGestationPeriodIdByPersonId(_personId);
            Assert.AreEqual(1, GestationPeriodRecords.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12623")]
        [Description("Navigate to Menu -> Health -> Gestation Periods -> Open the existing active Gestation period which is having child -> " +
            "Select some other child from lookup -> Click on Save and Return to Previous Page -> Open the Mother and Child records given in the Gestation Period -> Menu -> Care Network -> Relationships -> " +
            "Verify that system is not auto created the personal relationship record between mother and child when child record is set after initial save and Personal Relationship record not updated between mother and child when user updated the gestation period record. ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases09()
        {
            var startDate = DateTime.Now.AddDays(-5);

            #region Child Person 2

            var _childId2 = commonMethodsDB.CreatePersonRecord("Sree", "R_" + _currentDateSuffix, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            int _child2PersonNumber = (int)dbHelper.person.GetPersonById(_childId2, "personnumber")["personnumber"];
            string _child2FullName = "Sree R_" + _currentDateSuffix;

            #endregion

            #region Gestation Period

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .OpenGestationPeriodRecord(GestationPeriod.ToString());

            personGestationPeriodRecordPage
                .WaitForPersonGestationPeriodRecordPageToLoad("Person Gestation Period for " + _personFullName + " created by " + _defaultUserFullname + " on")
                .ClickChildLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_child2FullName)
                .TapSearchButton()
                .SelectResultElement(_childId2.ToString());

            personGestationPeriodRecordPage
               .WaitForPersonGestationPeriodRecordPageToLoad("Person Gestation Period for " + _personFullName + " created by " + _defaultUserFullname + " on")
               .ClickSaveAndCloseButton();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad();

            var GestationPeriodRecords = dbHelper.personGestationPeriod.GetPersonGestationPeriodIdByPersonId(_personId);
            Assert.AreEqual(1, GestationPeriodRecords.Count);

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .ValidateRecordCellText(GestationPeriodRecords[0].ToString(), 2, _child2FullName);

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad();

            //Verify that Intially saved child  record to the mother is not updated.
            var RelationshipsRecords = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId);
            Assert.AreEqual(1, RelationshipsRecords.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad()
                .ValidateRecordCellText(RelationshipsRecords[0].ToString(), 2, _childPersonFullName);

            //To Verify that the Personal relationship record for the Updated child is not Auto-populaed.

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_child2PersonNumber.ToString(), _childId2.ToString())
                .OpenPersonRecord(_childId2.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            //To Verify that the Personal relationship record for the Updated child is not Auto-populaed in Data base.
            var UpdatedChildRelationshipsRecords = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_childId2);
            Assert.AreEqual(0, UpdatedChildRelationshipsRecords.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12624")]
        [Description(" Verify that Gestation period record is created against the new mother and Personal Relationship record auto created " +
            "against the new mother when change the mother name in the gestation period record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases10()
        {
            var startDate = DateTime.Now.Date.AddDays(-5);

            #region Child Record (Person)

            var childId2 = commonMethodsDB.CreatePersonRecord("Sree", "R_" + _currentDateSuffix, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            string _child2FullName = "Sree R_" + _currentDateSuffix;

            #endregion

            #region Mother Record (Person)

            var _motherId = commonMethodsDB.CreatePersonRecord("Mother", "R_" + _currentDateSuffix, _ethnicityId, _teamId, new DateTime(1980, 1, 2));
            var _motherNumber = (int)dbHelper.person.GetPersonById(_motherId, "personnumber")["personnumber"];
            string _motherFullName = "Mother R_" + _currentDateSuffix;

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .ClickNewRecordButton();

            personGestationPeriodRecordPage
                .WaitForPersonGestationRecordPageToLoad("New")
                .ClickMotherLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_motherFullName)
                .TapSearchButton()
                .SelectResultElement(_motherId.ToString());

            personGestationPeriodRecordPage
                .WaitForPersonGestationRecordPageToLoad("New")
                .ClickChildLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_child2FullName)
                .TapSearchButton()
                .SelectResultElement(childId2.ToString());

            personGestationPeriodRecordPage
                .WaitForPersonGestationRecordPageToLoad("New")
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertNumber("765")
                .SelectDaysWeeks("1")
                .ClickSaveAndCloseButton();


            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad();

            var GestationPeriodRecords = dbHelper.personGestationPeriod.GetPersonGestationPeriodIdByPersonId(_motherId);
            Assert.AreEqual(1, GestationPeriodRecords.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecord(_motherNumber.ToString(), _motherId.ToString())
                .OpenPersonRecord(_motherId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .ValidateRecordCellText(GestationPeriodRecords[0].ToString(), 2, _child2FullName);

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad();

            var RelationshipsRecords = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_motherId);
            Assert.AreEqual(1, RelationshipsRecords.Count);

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecord(_motherNumber.ToString(), _motherId.ToString())
                .OpenPersonRecord(_motherId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad()
                .ValidateRecordCellText(RelationshipsRecords[0].ToString(), 2, _child2FullName);

        }

        [TestProperty("JiraIssueID", "CDV6-12626")]
        [Description("Verify that user is able to update the auto created personal relationship record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases11()
        {
            #region Gestation Period / Relationship

            var startDate = DateTime.Now.AddDays(-5);
            dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate, _endReasonId, "Gestation Details:");

            var RelationshipRecord = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId);
            var RelationshipChildRecord = RelationshipRecord.FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad()
                .OpenPersonRelationshipRecord(RelationshipChildRecord.ToString());

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad()
                .SelectFamilyMembers("2")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad();

            var RelationshipsRecords = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_personId);
            Assert.AreEqual(1, RelationshipsRecords.Count);

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad()
                .ValidateRecordCellText(RelationshipRecord[0].ToString(), 4, "Yes")
                .ValidateRecordCellText(RelationshipRecord[0].ToString(), 5, "No");

        }

        //Validate IsbirtParent Clarification Needed 
        //Bug ID:CDV6-12646
        [TestProperty("JiraIssueID", "CDV6-12627")]
        [Description("Navigate to People -> Open the (Mother) Person record with Gender = Female -> Menu -> Health -> Gestation Periods -> Create a new record ->" +
        "Click on Save and Return to Previous Page->Open the Mother and Child records given in the Gestation Period->Menu->Care Network->Relationships->Verify that Mother / Daughter relation is created in the personal relationship record when mother and child has stated Gender = Female" +
            "Verify the Mother and Daughter relationship" + "Verify the Is Birth parent field is autopopulated to Yes")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases12()
        {
            var startDate = DateTime.Now.AddDays(-5);

            #region Gestation Record / Relationship

            dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate, _endReasonId, "Gestation Details:");

            var RelationshipRecord = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_childId);
            var RelationshipChildRecord = RelationshipRecord.FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_childNumber.ToString(), _childId.ToString())
                .OpenPersonRecord(_childId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
                .WaitForPersonRelationshipPageToLoad()
                .OpenPersonRelationshipRecord(RelationshipChildRecord.ToString());

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad()
                .ValidatePersonRelationshipType("Daughter")
                .ValidateRelatedPersonRelationshipType("Mother")
                .ValidateIsBirthParent("Yes");

        }

        //Validate IsbirtParent Clarification Needed Bug ID:CDV6-12646
        [TestProperty("JiraIssueID", "CDV6-12629")]
        [Description("Navigate to People -> Open the (Mother) Person record with Gender = Female -> Menu -> Health -> Gestation Periods -> Create a new record ->" +
        "Click on Save and Return to Previous Page->Open the Mother and Child records given in the Gestation Period->Menu->Care Network->Relationships->Verify that Mother / Son relation is created in the personal relationship record when mother and child has stated Gender = Male" +
            "Verify the Mother and Son relationship" + "Verify the Is Birth parent field is autopopulated to Yes")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases13()
        {
            var startDate = DateTime.Now.AddDays(-5);

            #region Child Record (Person)

            _childId = dbHelper.person.CreatePersonRecord("", "Child", "", "R_" + _currentDateSuffix, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 1);
            _childNumber = (int)dbHelper.person.GetPersonById(_childId, "personnumber")["personnumber"];
            _childPersonFullName = "Child R_" + _currentDateSuffix;

            #endregion

            #region Gestation Record / Relationship

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate, _endReasonId, "Gestation Details:");

            var RelationshipRecord = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_childId);
            var RelationshipChildRecord = RelationshipRecord.FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_childNumber.ToString(), _childId.ToString())
                .OpenPersonRecord(_childId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
               .WaitForPersonRelationshipPageToLoad()
               .OpenPersonRelationshipRecord(RelationshipChildRecord.ToString());

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad()
                .ValidatePersonRelationshipType("Son")
                .ValidateRelatedPersonRelationshipType("Mother")
                .ValidateIsBirthParent("Yes");

        }

        [TestProperty("JiraIssueID", "CDV6-12635")]
        [Description(" Verify that user is able to create multiple Gestation Period records against the same Person")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases14()
        {
            var startDate1 = DateTime.Now.AddDays(-5);
            var startDate2 = DateTime.Now.AddDays(-10);

            #region Child Record 2 (Person)

            var _childId2 = commonMethodsDB.CreatePersonRecord("Tami", "R_" + _currentDateSuffix, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            string _child2FullName = "Tami R_" + _currentDateSuffix;

            #endregion

            #region Gestation Record

            dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate1, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .ClickNewRecordButton();

            personGestationPeriodRecordPage
                .WaitForPersonGestationRecordPageToLoad("New")
                .ClickChildLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_child2FullName)
                .TapSearchButton()
                .SelectResultElement(_childId2.ToString());

            personGestationPeriodRecordPage
                .WaitForPersonGestationRecordPageToLoad("New")
                .InsertStartDate(startDate2.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertNumber("765")
                .SelectDaysWeeks("1")
                .InsertNotes("UI Gestation Record")
                .ClickEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Live Delivery")
                .SelectResultElement(_endReasonId.ToString());

            personGestationPeriodRecordPage
                .WaitForPersonGestationRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad();

            var records = dbHelper.personGestationPeriod.GetPersonGestationPeriodIdByPersonId(_personId);
            Assert.AreEqual(2, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12637")]
        [Description(" Navigate to Gestation Periods -> Verify that all Gestation Periods records are sorted default by 'Start Date' column in descending order in the Gestation Period view.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases15()
        {
            var startDate1 = DateTime.Now.AddDays(-5);
            var startDate2 = DateTime.Now.AddDays(-10);

            #region Child Record 2 (Person)

            var _childId2 = commonMethodsDB.CreatePersonRecord("Tami", "R_" + _currentDateSuffix, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            string _child2FullName = "Tami R_" + _currentDateSuffix;

            #endregion

            #region Gestation Record

            dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate1, _endReasonId, "Gestation Details:");
            dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId2, 785, 1, startDate2, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            var PersonRecords = dbHelper.personGestationPeriod.GetPersonGestationPeriodIdByPersonId(_personId);
            Assert.AreEqual(2, PersonRecords.Count);

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .ValidateRecordPosition(1, PersonRecords[0].ToString())
                .ValidateRecordPosition(2, PersonRecords[1].ToString())
                .ValidateRecordCellText(PersonRecords[0].ToString(), 5, startDate1.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateRecordCellText(PersonRecords[1].ToString(), 5, startDate2.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

        }

        [TestProperty("JiraIssueID", "CDV6-12650")]
        [Description(" Navigate to Gestation Periods -> Verify that all Gestation Periods records are sorted default by 'Start Date' column in descending order in the Gestation Period view." +
            " Verify that user is able to sort the 'Start Date' column as ascending order in Gestation Period view")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases16()
        {
            var startDate1 = DateTime.Now.AddDays(-5);
            var startDate2 = DateTime.Now.AddDays(-10);

            #region Child Record 2 (Person)

            var _childId2 = commonMethodsDB.CreatePersonRecord("Tami", "R_" + _currentDateSuffix, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            string _child2FullName = "Tami R_" + _currentDateSuffix;

            #endregion

            #region Gestation Record

            dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate1, _endReasonId, "Gestation Details:");
            dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId2, 785, 1, startDate2, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();


            var PersonRecords = dbHelper.personGestationPeriod.GetPersonGestationPeriodIdByPersonId(_personId);
            Assert.AreEqual(2, PersonRecords.Count);

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .ValidateRecordPosition(1, PersonRecords[0].ToString())
                .ValidateRecordPosition(2, PersonRecords[1].ToString())
                .ValidateRecordCellText(PersonRecords[0].ToString(), 5, startDate1.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateRecordCellText(PersonRecords[1].ToString(), 5, startDate2.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickStartDateColumnToSort()
                .ValidateRecordPosition(1, PersonRecords[1].ToString())
                .ValidateRecordPosition(2, PersonRecords[0].ToString())
                .ValidateRecordCellText(PersonRecords[1].ToString(), 5, startDate2.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateRecordCellText(PersonRecords[0].ToString(), 5, startDate1.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture));

        }

        //Bug Id : CDV6-12687
        [TestProperty("JiraIssueID", "CDV6-12673")]
        [Description(" Navigate to Gestation Periods -> Enter any Child/Number/StartDate/EndDate/Days/Weeks field data in the Search for records box -> Click on Search icon ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases17()
        {
            var startDate1 = DateTime.Now.AddDays(-5);
            var startDate2 = DateTime.Now.AddDays(-15);
            var endDate = DateTime.Now.AddDays(-2);
            var endDate2 = DateTime.Now.AddDays(-1);
            var startDate3 = DateTime.Now.AddMonths(-3);

            #region Child Record 2 (Person)

            var _childId2 = dbHelper.person.CreatePersonRecord("", "Tami", "", "R_" + _currentDateSuffix, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 1);
            string _child2FullName = "Tami R_" + _currentDateSuffix;

            #endregion

            #region Gestation Records

            Guid GestationPeriod1 = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate1, _endReasonId, "Gestation Details:");
            Guid GestationPeriod2 = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId2, 742, 1, startDate2, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
               .WaitForPersonGestationPeriodPageToLoad()
               .OpenGestationPeriodRecord(GestationPeriod2.ToString());

            personGestationPeriodRecordPage
                .WaitForPersonGestationPeriodRecordPageToLoad("Person Gestation Period for " + _personFullName + " created by " + _defaultUserFullname + " on")
                .InsertEndDate(endDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .SearchRecord("785")
                .ValidateNoRecordMessageVisibile(false)
                .SearchRecord("234")
                .ValidateNoRecordMessageVisibile(true)
                .SearchRecord(_child2FullName)
                .ValidateNoRecordMessageVisibile(false)
                .SearchRecord(_childPersonFullName)
                .ValidateNoRecordMessageVisibile(false)
                .SearchRecord("Wesley Mooney")
                .ValidateNoRecordMessageVisibile(true)
                .SearchRecord(startDate2.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateNoRecordMessageVisibile(false)
                .SearchRecord(startDate1.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateNoRecordMessageVisibile(false)
                .SearchRecord(startDate3.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateNoRecordMessageVisibile(true)
                .SearchRecord(endDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateNoRecordMessageVisibile(false)
                .SearchRecord(endDate2.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateNoRecordMessageVisibile(true)
                .SearchRecord("Days")
                .ValidateNoRecordMessageVisibile(false);

        }

        [TestProperty("JiraIssueID", "CDV6-12674")]
        [Description("Navigate to Gestation Periods -> Open the existing record -> Click on 3 dots on top left corner -> Click on Delete -> " +
        "Click OK in delete record popup->Verify that user is able to delete the Gestation Period record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases18()
        {
            var startDate1 = DateTime.Now.AddDays(-5);

            #region Gestation Record

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate1, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .OpenGestationPeriodRecord(GestationPeriod.ToString());

            personGestationPeriodRecordPage
                .WaitForPersonGestationPeriodRecordPageToLoad("Person Gestation Period for " + _personFullName + " created by " + _defaultUserFullname + " on")
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            var PersonRecords = dbHelper.personGestationPeriod.GetPersonGestationPeriodIdByPersonId(_personId);
            Assert.AreEqual(0, PersonRecords.Count);


        }

        [TestProperty("JiraIssueID", "CDV6-12676")]
        [Description("Navigate to Gestation Periods -> Open the existing record -> Click on Assign this record to another team icon on top left corner -> Select another Responsible Team -> Click OK ->" +
            " Verify that Gestation Period record Responsible Team is changed to another Responsible Team")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases19()
        {
            var startDate1 = DateTime.Now.AddDays(-5);

            #region Team Manager (System User)

            var _teamManagerUserName = "alex.smith";
            var _teamManagerId = commonMethodsDB.CreateSystemUserRecord(_teamManagerUserName, "Alex", "Smith (Advanced)", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Team

            var _advancedTeamId = commonMethodsDB.CreateTeam("Advanced", _teamManagerId, _businessUnitId, "", "oneadvanced@randommail.com", "Advanced", "");

            #endregion

            #region  Gestation Record

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate1, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .SelectPersonGestationPeriodPageRecord(GestationPeriod.ToString())
                .ClickAssignButton();

            assignRecordPopup.WaitForAssignRecordAlertAndHazardPopupToLoad().ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("Advanced")
                .TapSearchButton().SelectResultElement(_advancedTeamId.ToString());

            assignRecordPopup.TapOkButton();

        }

        [TestProperty("JiraIssueID", "CDV6-12678")]
        [Description("Verify that system download the csv file and exported records are same as in the Gestation Periods page records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases20()
        {
            var startDate1 = DateTime.Now.AddDays(-5);

            #region Gestation Record

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate1, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .SelectPersonGestationPeriodPageRecord(GestationPeriod.ToString())
                .ClickExportToExcelButton();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Selected Records")
                .SelectExportFormat("Csv (comma separated with quotes)")
                .ClickExportButton();

            System.Threading.Thread.Sleep(5000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "GestationPeriods.csv");
            Assert.IsTrue(fileExists);

        }


        //Validate IsbirtParent Clarification Needed Bug ID:CDV6-12646
        [TestProperty("JiraIssueID", "CDV6-12705")]
        [Description("Navigate to People->Open the(Mother) Person record with Gender = Female->Menu->Health->Gestation Periods->Create a new record ->" +
        "Click on Save and Return to Previous Page->Open the Mother and Child records given in the Gestation Period->Menu->Care Network->Relationships->Verify that Mother / Child relation is created in the personal relationship record when mother and child has stated Gender = Not Known " +
            "Verify the Mother and child relationship" + "Verify the Is Birth parent field is autopopulated to Yes")]

        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]

        public void Person_GestationPeriod_UITestCases21()
        {
            var startDate = DateTime.Now.AddDays(-5);

            #region Child Record (Not Known)

            var _childId2 = dbHelper.person.CreatePersonRecord("", "Child", "", "R_" + _currentDateSuffix, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 3);
            var _childNumber2 = (int)dbHelper.person.GetPersonById(_childId2, "personnumber")["personnumber"];

            #endregion

            #region Gestation Record / Relationship

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId2, 785, 1, startDate, _endReasonId, "Gestation Details:");

            var RelationshipRecord = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_childId2);
            var RelationshipChildRecord = RelationshipRecord.FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_childNumber2.ToString(), _childId2.ToString())
                .OpenPersonRecord(_childId2.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
               .WaitForPersonRelationshipPageToLoad()
               .OpenPersonRelationshipRecord(RelationshipChildRecord.ToString());

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad()
                .ValidatePersonRelationshipType("Child")
                .ValidateRelatedPersonRelationshipType("Mother")
                .ValidateIsBirthParent("Yes");

        }


        //Validate IsbirtParent Clarification Needed Bug ID:CDV6-12646,CDV6-12714
        [TestProperty("JiraIssueID", "CDV6-12706")]
        [Description("Navigate to People->Open the(Mother) Person record with Gender = Female->Menu->Health->Gestation Periods->Create a new record ->" +
        "Click on Save and Return to Previous Page->Open the Mother and Child records given in the Gestation Period->Menu->Care Network->Relationships->Verify that Mother / Child relation is created in the personal relationship record when mother and child has stated Gender = Unborn/Intermediate" +
            "Verify the Mother and child relationship" + "Verify the Is Birth parent field is autopopulated to Yes")]

        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]

        public void Person_GestationPeriod_UITestCases22()
        {
            DateTime startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            DateTime endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

            #region Child Record (Indeterminate)

            var _childId2 = dbHelper.person.CreatePersonRecord("", "Child", "", "R_" + _currentDateSuffix, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 4);
            var _childNumber2 = (int)dbHelper.person.GetPersonById(_childId2, "personnumber")["personnumber"];

            #endregion

            #region Gestation Record / Relationship

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecordEndDate(_personId, _teamId, _childId2, 785, 1, startDate, endDate, _endReasonId, "Gestation Details:");

            var RelationshipRecord = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_childId2);
            var RelationshipChildRecord = RelationshipRecord.FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_childNumber2.ToString(), _childId2.ToString())
                .OpenPersonRecord(_childId2.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
               .WaitForPersonRelationshipPageToLoad()
               .OpenPersonRelationshipRecord(RelationshipChildRecord.ToString());

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad()
                .ValidatePersonRelationshipType("Child")
                .ValidateRelatedPersonRelationshipType("Mother")
                .ValidateRelatedPersonRelationshipEndDate(endDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateIsBirthParent("Yes");

        }

        //Validate IsbirtParent Clarification Needed Bug ID:CDV6-12646
        [TestProperty("JiraIssueID", "CDV6-12708")]
        [Description("Navigate to People->Open the(Mother) Person record with Gender = Unknown->Menu->Health->Gestation Periods->Create a new record ->" +
        "Click on Save and Return to Previous Page->Open the Mother and Child records given in the Gestation Period->Menu->Care Network->Relationships->Verify that Mother / Child relation is created in the personal relationship record when mother and child has stated Gender = Female" +
            "Verify the Parent and Daughter relationship" + "Verify the Is Birth parent field is autopopulated to Yes")]

        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]

        public void Person_GestationPeriod_UITestCases23()
        {
            var startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            var endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

            #region Person 2

            var _personId2 = dbHelper.person.CreatePersonRecord("", "Person", "", "R_" + _currentDateSuffix, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 3);

            #endregion

            #region Gestation Record / Ralationship

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecordEndDate(_personId2, _teamId, _childId, 785, 1, startDate, endDate, _endReasonId, "Gestation Details:");

            var RelationshipRecord = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_childId);
            var RelationshipChildRecord = RelationshipRecord.FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_childNumber.ToString(), _childId.ToString())
                .OpenPersonRecord(_childId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
               .WaitForPersonRelationshipPageToLoad()
               .OpenPersonRelationshipRecord(RelationshipChildRecord.ToString());

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad()
                .ValidatePersonRelationshipType("Daughter")
                .ValidateRelatedPersonRelationshipType("Parent")
                .ValidateIsBirthParent("Yes");

        }

        //Validate IsbirtParent Clarification Needed Bug ID:CDV6-12646
        [TestProperty("JiraIssueID", "CDV6-12709")]
        [Description("Navigate to People->Open the(Mother) Person record with Gender = Male->Menu->Health->Gestation Periods->Create a new record ->" +
        "Click on Save and Return to Previous Page->Open the Mother and Child records given in the Gestation Period->Menu->Care Network->Relationships->Verify that Mother / Child relation is created in the personal relationship record when mother and child has stated Gender = Unknown" +
            "Verify the Parent and Child relationship" + "Verify the Is Birth parent field is autopopulated to Yes")]

        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]

        public void Person_GestationPeriod_UITestCases24()
        {
            var startDate = DateTime.Now.Date;
            var endDate = DateTime.Now.Date;

            #region Person 2 (Male)

            var _personId2 = dbHelper.person.CreatePersonRecord("", "Person", "", "R_" + _currentDateSuffix, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 1);

            #endregion

            #region Child Record (Not Known)

            var _childId2 = dbHelper.person.CreatePersonRecord("", "Child", "", "R_" + _currentDateSuffix, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 3);
            var _childNumber2 = (int)dbHelper.person.GetPersonById(_childId2, "personnumber")["personnumber"];

            #endregion

            #region Gestation Record / Relationship

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecordEndDate(_personId2, _teamId, _childId2, 785, 1, startDate, endDate, _endReasonId, "Gestation Details:");

            var RelationshipRecord = dbHelper.personRelationship.GetPersonRelationshipByPersonID(_childId2);
            var RelationshipChildRecord = RelationshipRecord.FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_childNumber2.ToString(), _childId2.ToString())
                .OpenPersonRecord(_childId2.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRelationshipsPage();

            personRelationshipPage
               .WaitForPersonRelationshipPageToLoad()
               .OpenPersonRelationshipRecord(RelationshipChildRecord.ToString());

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad()
                .ValidatePersonRelationshipType("Child")
                .ValidateRelatedPersonRelationshipType("Parent")
                .ValidateIsBirthParent("Yes");

        }

        [TestProperty("JiraIssueID", "CDV6-12711")]
        [Description("Verify that system download the Excel file and exported records are same as in the Gestation Periods page records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_GestationPeriod_UITestCases25()
        {
            var startDate1 = DateTime.Now.AddDays(-5);

            #region Gestation Record

            Guid GestationPeriod = dbHelper.personGestationPeriod.CreatePersonGestationPeriodRecord(_personId, _teamId, _childId, 785, 1, startDate1, _endReasonId, "Gestation Details:");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToGestationPeriodPage();

            personGestationPeriodPage
                .WaitForPersonGestationPeriodPageToLoad()
                .SelectPersonGestationPeriodPageRecord(GestationPeriod.ToString())
                .ClickExportToExcelButton();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Selected Records")
                .SelectExportFormat("Excel")
                .ClickExportButton();

            System.Threading.Thread.Sleep(5000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "GestationPeriods.xlsx");
            Assert.IsTrue(fileExists);

        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }

    #endregion
}

