using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Phoenix.UITests.Settings.DataManagement
{
    [TestClass]
    public class MergedRecords_UITestCases : FunctionalTest
    {

        private Guid MergeRecords_ScheduleJobID = new Guid("52736669-28cb-e911-a2c9-0050569231cf"); //Merge Records
        private Guid UnmergeRecords_ScheduleJobID = new Guid("99705b80-28cb-e911-a2c9-0050569231cf"); //Unmerge Records
        private Guid SyncRecordTitles_ScheduleJobID = new Guid("2171c169-21c1-ea11-9c21-1866da1e4209"); //Sync Record Titles

        #region issue https://advancedcsg.atlassian.net/browse/CDV6-1976

        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-1976 - Testing Merge records title change - " +
            "Login in the web app - navigate to Settings - Configuration - Data Management - Duplicate Detection - Merge records - Tap on the add new record button - " +
            "Insert a Title - Select a Master person record (with no Alerts and hazards) - Select a Subordinate person record (with alert and hazard records) - Insert a title and record type - " +
            "Tap on the save and close button - wait for the schedule jobs to run - Validate that the records are assigned to the master record and that the titles are updated accordingly")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24548")]
        public void MergedRecords_UITestMethod01()
        {

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            int representsAlertRoleid = 1; //Represents an Alert/Hazard
            int exposedAlertRoleid = 2; //Exposed to Alert/Hazard
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid alertandhazardtypeid = new Guid("95499DDB-D139-E911-A2C5-005056926FE4"); //Dangerous Dog
            DateTime startDate = DateTime.Now.Date;
            int reviewfrequencytypeid = 2; //weekly

            //Create alert and hazard record
            Guid alertAndHazardID1 = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(representsAlertRoleid, "hazard description", mobileTeam1, "Mobile Team 1", _personIDSubordinate, FirstName + " Subordinate", alertandhazardtypeid, "Dangerous Dog", null, null, startDate, null, reviewfrequencytypeid);
            Guid alertAndHazardID2 = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(exposedAlertRoleid, "hazard description", mobileTeam1, "Mobile Team 1", _personIDSubordinate, FirstName + " Subordinate", alertandhazardtypeid, "Dangerous Dog", null, null, startDate, null, reviewfrequencytypeid);



            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickMergedRecordsButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad()
                .ClickAddButton();

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .InsertTitle("Data Merge " + FirstName)
                .ClickMasterRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDMaster.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSubordinateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDSubordinate.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Person");


            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);



            //get the personid and title fields for the hazard record and assert they are correctly updated
            var fields = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(alertAndHazardID1, "personid", "title");
            Assert.AreEqual(_personIDMaster, (Guid)fields["personid"]);
            Assert.AreEqual(FirstName + " Master Represents an Alert/Hazard Dangerous Dog", (string)fields["title"]);

            //get the personid and title fields for the hazard record and assert they are correctly updated
            fields = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(alertAndHazardID2, "personid", "title");
            Assert.AreEqual(_personIDMaster, (Guid)fields["personid"]);
            Assert.AreEqual(FirstName + " Master Exposed to Alert/Hazard Dangerous Dog", (string)fields["title"]);

        }

        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-1976 - Testing Merge records title change - " +
            "Login in the web app - navigate to Settings - Configuration - Data Management - Duplicate Detection - Merge records - Tap on the add new record button - " +
            "Insert a Title - Select a Master person record (with no Allergies) - Select a Subordinate person record (with Allergies records) - Insert a title and record type - " +
            "Tap on the save and close button - wait for the schedule jobs to run - Validate that the records are assigned to the master record and that the titles are updated accordingly")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24549")]
        public void MergedRecords_UITestMethod02()
        {

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);


            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid allergyType = new Guid("77e05b21-c3cf-e911-a2c7-005056926fe4"); //Dust
            int allergyLevel = 3; //Hypersensitivity

            Guid allergy1 = dbHelper.personAllergy.CreatePersonAllergy(mobileTeam1, false, _personIDSubordinate, FirstName + " Subordinate", allergyType, "Dust", "allergen details", new DateTime(2020, 5, 1), null, "description information", allergyLevel);
            Guid allergy2 = dbHelper.personAllergy.CreatePersonAllergy(mobileTeam1, false, _personIDSubordinate, FirstName + " Subordinate", allergyType, "Dust", "allergen details", new DateTime(2020, 5, 10), null, "description information", allergyLevel);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickMergedRecordsButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad()
                .ClickAddButton();

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .InsertTitle("Data Merge " + FirstName)
                .ClickMasterRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDMaster.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSubordinateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDSubordinate.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Person");


            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);



            //get the personid and title fields for the Allergy record and assert they are correctly updated
            var fields = dbHelper.personAllergy.GetPersonAllergyByID(allergy1, "personid", "title");
            Assert.AreEqual(_personIDMaster, (Guid)fields["personid"]);
            Assert.AreEqual(FirstName + " Master, Dust, 01/05/2020", (string)fields["title"]);

            //get the personid and title fields for the Allergy record and assert they are correctly updated
            fields = dbHelper.personAllergy.GetPersonAllergyByID(allergy2, "personid", "title");
            Assert.AreEqual(_personIDMaster, (Guid)fields["personid"]);
            Assert.AreEqual(FirstName + " Master, Dust, 10/05/2020", (string)fields["title"]);

        }

        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-1976 - Testing Merge records title change - " +
            "Login in the web app - navigate to Settings - Configuration - Data Management - Duplicate Detection - Merge records - Tap on the add new record button - " +
            "Insert a Title - Select a Master person record (with no Case records) - Select a Subordinate person record (with Case records) - Insert a title and record type - " +
            "Tap on the save and close button - wait for the schedule jobs to run - Validate that the records are assigned to the master record and that the titles are updated accordingly")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24550")]
        public void MergedRecords_UITestMethod03()
        {

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);



            Guid ownerid = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("BC156AC3-BAFE-E811-80DC-0050560502CC"); //Allocate to Team
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid dataformid = new Guid("EDF4EFC4-BFB1-E811-80DC-0050560502CC"); //Social Care Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime startdatetime = new DateTime(2020, 7, 2);
            int personage = 21;


            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(ownerid, _personIDSubordinate, contactreceivedbyid, responsibleuserid, casestatusid, contactreasonid, dataformid, contactsourceid, contactreceiveddatetime, startdatetime, personage);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickMergedRecordsButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad()
                .ClickAddButton();

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .InsertTitle("Data Merge " + FirstName)
                .ClickMasterRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDMaster.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSubordinateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDSubordinate.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Person");


            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);



            //get the personid and title fields for the Case record and assert they are correctly updated
            var fields = dbHelper.Case.GetCaseByID(caseID, "personid", "title", "casenumber");
            Assert.AreEqual(_personIDMaster, (Guid)fields["personid"]);
            string caseNumber = (string)fields["casenumber"];
            Assert.AreEqual("Master, " + FirstName + " - (01/01/1999) [" + caseNumber + "]", (string)fields["title"]);
        }

        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-1976 - Testing Merge records title change - " +
            "Login in the web app - navigate to Settings - Configuration - Data Management - Duplicate Detection - Merge records - Tap on the add new record button - " +
            "Insert a Title - Select a Master person record (with no Adult Safeguarding records) - Select a Subordinate person record (with Adult Safeguarding records) - Insert a title and record type - " +
            "Tap on the save and close button - wait for the schedule jobs to run - Validate that the records are assigned to the master record and that the titles are updated accordingly")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24551")]
        public void MergedRecords_UITestMethod04()
        {

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);



            Guid ownerid = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("BC156AC3-BAFE-E811-80DC-0050560502CC"); //Allocate to Team
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid dataformid = new Guid("EDF4EFC4-BFB1-E811-80DC-0050560502CC"); //Social Care Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime startdatetime = new DateTime(2020, 7, 2);
            int personage = 21;

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(ownerid, _personIDSubordinate, contactreceivedbyid, responsibleuserid, casestatusid, contactreasonid, dataformid, contactsourceid, contactreceiveddatetime, startdatetime, personage);

            var casefields = dbHelper.Case.GetCaseByID(caseID, "title");
            string caseTitle = (string)casefields["title"];
            Guid adultsafeguardingcategoryofabuseid = new Guid("1f43e25d-bbb2-e911-a2c6-005056926fe4"); //Emotional
            Guid adultsafeguardingstatusid = new Guid("187b2938-2d86-ea11-a2cd-005056926fe4"); //Concern
            DateTime startdate = new DateTime(2020, 7, 1);

            Guid safeguardingID = dbHelper.AdultSafeguarding.CreateAdultSafeguarding(ownerid, responsibleuserid, caseID, caseTitle, _personIDSubordinate, adultsafeguardingcategoryofabuseid, adultsafeguardingstatusid, startdate);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickMergedRecordsButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad()
                .ClickAddButton();

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .InsertTitle("Data Merge " + FirstName)
                .ClickMasterRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDMaster.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSubordinateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDSubordinate.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Person");


            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job again (records related to case are updated on the second run) 
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);



            //get the personid and title fields for the Adult Safeguarding record and assert they are correctly updated
            var fields = dbHelper.AdultSafeguarding.GetAdultSafeguardingByID(safeguardingID, "caseid", "personid", "title");
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(_personIDMaster, (Guid)fields["personid"]);
            Assert.IsTrue(((string)fields["title"]).StartsWith("Adult Safeguarding within Case Master, " + FirstName + " - (01/01/1999)"));
        }

        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-1976 - Testing Merge records title change - " +
            "Login in the web app - navigate to Settings - Configuration - Data Management - Duplicate Detection - Merge records - Tap on the add new record button - " +
            "Insert a Title - Select a Master person record (with no Child Protection records) - Select a Subordinate person record (with Child Protection records) - Insert a title and record type - " +
            "Tap on the save and close button - wait for the schedule jobs to run - Validate that the records are assigned to the master record and that the titles are updated accordingly")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24552")]
        public void MergedRecords_UITestMethod05()
        {

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);



            Guid ownerid = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("BC156AC3-BAFE-E811-80DC-0050560502CC"); //Allocate to Team
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid dataformid = new Guid("EDF4EFC4-BFB1-E811-80DC-0050560502CC"); //Social Care Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime startdatetime = new DateTime(2020, 7, 2);
            int personage = 21;

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(ownerid, _personIDSubordinate, contactreceivedbyid, responsibleuserid, casestatusid, contactreasonid, dataformid, contactsourceid, contactreceiveddatetime, startdatetime, personage);


            var casefields = dbHelper.Case.GetCaseByID(caseID, "title");
            string caseTitle = (string)casefields["title"];
            Guid childprotectioncategoryofabuseid = new Guid("96588309-ac3e-ea11-a2c8-005056926fe4"); //Emotional abuse
            Guid childprotectionstatustypeid = new Guid("cb889fe7-8d3a-e911-a2c5-005056926fe4"); //Status_Default1
            DateTime startdate = new DateTime(2020, 7, 2);
            DateTime datestatuschanged = new DateTime(2020, 7, 2);

            Guid safeguardingID = dbHelper.ChildProtection.CreateChildProtection(ownerid, caseID, caseTitle, _personIDSubordinate, childprotectioncategoryofabuseid, childprotectionstatustypeid, startdate, datestatuschanged);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickMergedRecordsButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad()
                .ClickAddButton();

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .InsertTitle("Data Merge " + FirstName)
                .ClickMasterRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDMaster.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSubordinateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDSubordinate.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Person");


            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job again (records related to case are updated on the second run) 
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);



            //get the personid and title fields for the Child Protection record and assert they are correctly updated
            var fields = dbHelper.ChildProtection.GetChildProtectionByID(safeguardingID, "caseid", "personid", "title");
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(_personIDMaster, (Guid)fields["personid"]);
            Assert.IsTrue(((string)fields["title"]).StartsWith("Child Protection within Case Master, " + FirstName + " - (01/01/1999)"));
        }

        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-1976 - Testing Merge records title change - " +
            "Login in the web app - navigate to Settings - Configuration - Data Management - Duplicate Detection - Merge records - Tap on the add new record button - " +
            "Insert a Title - Select a Master person record (with no LAC Legal Status records) - Select a Subordinate person record (with LAC Legal Status records) - Insert a title and record type - " +
            "Tap on the save and close button - wait for the schedule jobs to run - Validate that the records are assigned to the master record and that the titles are updated accordingly")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24553")]
        public void MergedRecords_UITestMethod06()
        {

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);



            Guid ownerid = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("BC156AC3-BAFE-E811-80DC-0050560502CC"); //Allocate to Team
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid dataformid = new Guid("EDF4EFC4-BFB1-E811-80DC-0050560502CC"); //Social Care Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime startdatetime = new DateTime(2020, 7, 2);
            int personage = 21;

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(ownerid, _personIDSubordinate, contactreceivedbyid, responsibleuserid, casestatusid, contactreasonid, dataformid, contactsourceid, contactreceiveddatetime, startdatetime, personage);


            var casefields = dbHelper.Case.GetCaseByID(caseID, "title");
            string caseTitle = (string)casefields["title"];
            DateTime startdate = new DateTime(2020, 7, 2);

            Guid lacEpisodeID = dbHelper.LACEpisode.CreateLACEpisode(ownerid, caseID, caseTitle, _personIDSubordinate, startdate);


            Guid laclegalstatusreasonid = new Guid("25C63DF3-2D22-EA11-A2C8-005056926FE4"); //Status reason-S- Applicable to Started to Be Looked After
            Guid laclegalstatusid = new Guid("FAC4D2FC-1269-EA11-A2CB-005056926FE4"); //Accommodated under an agreed series of short-term breaks, when agreements are recorded (NOT individual episodes of care)
            Guid lacplacementid = new Guid("1485591D-B069-EA11-A2CB-005056926FE4"); //All Residential schools, except where dual-registered as a school and Children’s Home
            Guid laclocationcodeid = new Guid("3C067F2F-5F66-E911-A2C5-005056926FE4"); //Test LAC Location  
            Guid lacplacementproviderid = new Guid("4E049753-B669-EA11-A2CB-005056926FE4"); //Other local authority provision, including a regional adoption agency where another local authority is the host authority

            Guid lacLegalStatusID = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(OwnerID, caseID, caseTitle, _personIDSubordinate, lacEpisodeID, laclegalstatusreasonid, laclegalstatusid, lacplacementid, laclocationcodeid, lacplacementproviderid, startdate);



            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickMergedRecordsButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad()
                .ClickAddButton();

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .InsertTitle("Data Merge " + FirstName)
                .ClickMasterRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDMaster.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSubordinateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDSubordinate.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Person");


            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job again (records related to case are updated on the second run) 
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);



            //get the personid and title fields for the LAC Episode record and assert they are correctly updated
            var fields = dbHelper.LACEpisode.GetLACEpisodeByID(lacEpisodeID, "caseid", "personid", "title");
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(_personIDMaster, (Guid)fields["personid"]);
            Assert.IsTrue(((string)fields["title"]).StartsWith("LAC episode within Case Master, " + FirstName + " - (01/01/1999)"));


            //get the personid and title fields for the LAC Legal Status record and assert they are correctly updated
            fields = dbHelper.PersonLACLegalStatus.GetPersonLACLegalStatusByID(lacLegalStatusID, "caseid", "personid", "title");
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(_personIDMaster, (Guid)fields["personid"]);
            Assert.IsTrue(((string)fields["title"]).StartsWith("LAC Legal Status within Case Master, " + FirstName + " - (01/01/1999)"));
        }









        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-1976 - Testing Merge records title change - " +
            "Login in the web app - navigate to Settings - Configuration - Data Management - Duplicate Detection - Merge records - Tap on the add new record button - " +
            "Insert a Title - Select a Master person record (with no Alerts and hazards) - Select a Subordinate person record (with alert and hazard records) - Insert a title and record type - " +
            "Tap on the save and close button - wait for the schedule jobs to run - Validate that the records are assigned to the master record and that the titles are updated accordingly")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24554")]
        public void UnmergedRecords_UITestMethod01()
        {

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            int representsAlertRoleid = 1; //Represents an Alert/Hazard
            int exposedAlertRoleid = 2; //Exposed to Alert/Hazard
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid alertandhazardtypeid = new Guid("95499DDB-D139-E911-A2C5-005056926FE4"); //Dangerous Dog
            DateTime startDate = DateTime.Now.Date;
            int reviewfrequencytypeid = 2; //weekly

            //Create alert and hazard record
            Guid alertAndHazardID1 = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(representsAlertRoleid, "hazard description", mobileTeam1, "Mobile Team 1", _personIDSubordinate, FirstName + " Subordinate", alertandhazardtypeid, "Dangerous Dog", null, null, startDate, null, reviewfrequencytypeid);
            Guid alertAndHazardID2 = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(exposedAlertRoleid, "hazard description", mobileTeam1, "Mobile Team 1", _personIDSubordinate, FirstName + " Subordinate", alertandhazardtypeid, "Dangerous Dog", null, null, startDate, null, reviewfrequencytypeid);



            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickMergedRecordsButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad()
                .ClickAddButton();

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .InsertTitle("Data Merge " + FirstName)
                .ClickMasterRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDMaster.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSubordinateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDSubordinate.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Person");


            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);





            Guid masterRecordID = dbHelper.MergedRecord.GetMergedRecordByMasterRecordID(_personIDMaster)[0];

            mergedRecordsPage
                .SelectViewByName("Merged Records")
                .CliickOnRecord(masterRecordID.ToString());

            mergedRecordRecordPage
                .WaitForUnmergedRecordRecordPageToLoad("Data Merge " + FirstName)
                .ClickUnmergeButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to unmerge this record?").TapOKButton();

            mergedRecordRecordPage
                .ClickBackButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Unmerge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UnmergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UnmergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            System.Threading.Thread.Sleep(2000);

            //get the personid and title fields for the hazard record and assert they are correctly updated
            var fields = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(alertAndHazardID1, "personid", "title");
            Assert.AreEqual(_personIDSubordinate, (Guid)fields["personid"]);
            Assert.AreEqual(FirstName + " Subordinate Represents an Alert/Hazard Dangerous Dog", (string)fields["title"]);

            //get the personid and title fields for the hazard record and assert they are correctly updated
            fields = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(alertAndHazardID2, "personid", "title");
            Assert.AreEqual(_personIDSubordinate, (Guid)fields["personid"]);
            Assert.AreEqual(FirstName + " Subordinate Exposed to Alert/Hazard Dangerous Dog", (string)fields["title"]);

        }

        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-1976 - Testing Merge records title change - " +
            "Login in the web app - navigate to Settings - Configuration - Data Management - Duplicate Detection - Merge records - Tap on the add new record button - " +
            "Insert a Title - Select a Master person record (with no Allergies) - Select a Subordinate person record (with Allergies records) - Insert a title and record type - " +
            "Tap on the save and close button - wait for the schedule jobs to run - Validate that the records are assigned to the master record and that the titles are updated accordingly")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24555")]
        public void UnmergedRecords_UITestMethod02()
        {

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);


            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid allergyType = new Guid("77e05b21-c3cf-e911-a2c7-005056926fe4"); //Dust
            int allergyLevel = 3; //Hypersensitivity

            Guid allergy1 = dbHelper.personAllergy.CreatePersonAllergy(mobileTeam1, false, _personIDSubordinate, FirstName + " Subordinate", allergyType, "Dust", "allergen details", new DateTime(2020, 5, 1), null, "description information", allergyLevel);
            Guid allergy2 = dbHelper.personAllergy.CreatePersonAllergy(mobileTeam1, false, _personIDSubordinate, FirstName + " Subordinate", allergyType, "Dust", "allergen details", new DateTime(2020, 5, 10), null, "description information", allergyLevel);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickMergedRecordsButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad()
                .ClickAddButton();

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .InsertTitle("Data Merge " + FirstName)
                .ClickMasterRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDMaster.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSubordinateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDSubordinate.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Person");


            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);



            Guid masterRecordID = dbHelper.MergedRecord.GetMergedRecordByMasterRecordID(_personIDMaster)[0];

            mergedRecordsPage
                .SelectViewByName("Merged Records")
                .CliickOnRecord(masterRecordID.ToString());

            mergedRecordRecordPage
                .WaitForUnmergedRecordRecordPageToLoad("Data Merge " + FirstName)
                .ClickUnmergeButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to unmerge this record?").TapOKButton();

            mergedRecordRecordPage
                .ClickBackButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Unmerge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UnmergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UnmergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            System.Threading.Thread.Sleep(2000);

            //get the personid and title fields for the Allergy record and assert they are correctly updated
            var fields = dbHelper.personAllergy.GetPersonAllergyByID(allergy1, "personid", "title");
            Assert.AreEqual(_personIDSubordinate, (Guid)fields["personid"]);
            Assert.AreEqual(FirstName + " Subordinate, Dust, 01/05/2020", (string)fields["title"]);

            //get the personid and title fields for the Allergy record and assert they are correctly updated
            fields = dbHelper.personAllergy.GetPersonAllergyByID(allergy2, "personid", "title");
            Assert.AreEqual(_personIDSubordinate, (Guid)fields["personid"]);
            Assert.AreEqual(FirstName + " Subordinate, Dust, 10/05/2020", (string)fields["title"]);

        }

        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-1976 - Testing Merge records title change - " +
            "Login in the web app - navigate to Settings - Configuration - Data Management - Duplicate Detection - Merge records - Tap on the add new record button - " +
            "Insert a Title - Select a Master person record (with no Case records) - Select a Subordinate person record (with Case records) - Insert a title and record type - " +
            "Tap on the save and close button - wait for the schedule jobs to run - Validate that the records are assigned to the master record and that the titles are updated accordingly")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24556")]
        public void UnmergedRecords_UITestMethod03()
        {

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);



            Guid ownerid = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("BC156AC3-BAFE-E811-80DC-0050560502CC"); //Allocate to Team
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid dataformid = new Guid("EDF4EFC4-BFB1-E811-80DC-0050560502CC"); //Social Care Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime startdatetime = new DateTime(2020, 7, 2);
            int personage = 21;


            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(ownerid, _personIDSubordinate, contactreceivedbyid, responsibleuserid, casestatusid, contactreasonid, dataformid, contactsourceid, contactreceiveddatetime, startdatetime, personage);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickMergedRecordsButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad()
                .ClickAddButton();

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .InsertTitle("Data Merge " + FirstName)
                .ClickMasterRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDMaster.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSubordinateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDSubordinate.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Person");


            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);



            Guid masterRecordID = dbHelper.MergedRecord.GetMergedRecordByMasterRecordID(_personIDMaster)[0];

            mergedRecordsPage
                .SelectViewByName("Merged Records")
                .CliickOnRecord(masterRecordID.ToString());

            mergedRecordRecordPage
                .WaitForUnmergedRecordRecordPageToLoad("Data Merge " + FirstName)
                .ClickUnmergeButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to unmerge this record?").TapOKButton();

            mergedRecordRecordPage
                .ClickBackButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Unmerge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UnmergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UnmergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            System.Threading.Thread.Sleep(2000);



            //get the personid and title fields for the Case record and assert they are correctly updated
            var fields = dbHelper.Case.GetCaseByID(caseID, "personid", "title", "casenumber");
            Assert.AreEqual(_personIDSubordinate, (Guid)fields["personid"]);
            string caseNumber = (string)fields["casenumber"];
            Assert.AreEqual("Subordinate, " + FirstName + " - (01/01/1999) [" + caseNumber + "]", (string)fields["title"]);
        }

        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-1976 - Testing Merge records title change - " +
            "Login in the web app - navigate to Settings - Configuration - Data Management - Duplicate Detection - Merge records - Tap on the add new record button - " +
            "Insert a Title - Select a Master person record (with no Adult Safeguarding records) - Select a Subordinate person record (with Adult Safeguarding records) - Insert a title and record type - " +
            "Tap on the save and close button - wait for the schedule jobs to run - Validate that the records are assigned to the master record and that the titles are updated accordingly")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24557")]
        public void UnmergedRecords_UITestMethod04()
        {

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);



            Guid ownerid = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("BC156AC3-BAFE-E811-80DC-0050560502CC"); //Allocate to Team
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid dataformid = new Guid("EDF4EFC4-BFB1-E811-80DC-0050560502CC"); //Social Care Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime startdatetime = new DateTime(2020, 7, 2);
            int personage = 21;

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(ownerid, _personIDSubordinate, contactreceivedbyid, responsibleuserid, casestatusid, contactreasonid, dataformid, contactsourceid, contactreceiveddatetime, startdatetime, personage);

            var casefields = dbHelper.Case.GetCaseByID(caseID, "title");
            string caseTitle = (string)casefields["title"];
            Guid adultsafeguardingcategoryofabuseid = new Guid("1f43e25d-bbb2-e911-a2c6-005056926fe4"); //Emotional
            Guid adultsafeguardingstatusid = new Guid("187b2938-2d86-ea11-a2cd-005056926fe4"); //Concern
            DateTime startdate = new DateTime(2020, 7, 1);

            Guid safeguardingID = dbHelper.AdultSafeguarding.CreateAdultSafeguarding(ownerid, responsibleuserid, caseID, caseTitle, _personIDSubordinate, adultsafeguardingcategoryofabuseid, adultsafeguardingstatusid, startdate);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickMergedRecordsButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad()
                .ClickAddButton();

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .InsertTitle("Data Merge " + FirstName)
                .ClickMasterRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDMaster.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSubordinateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDSubordinate.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Person");


            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job again (records related to case are updated on the second run) 
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);


            Guid masterRecordID = dbHelper.MergedRecord.GetMergedRecordByMasterRecordID(_personIDMaster)[0];

            mergedRecordsPage
                .SelectViewByName("Merged Records")
                .CliickOnRecord(masterRecordID.ToString());

            mergedRecordRecordPage
                .WaitForUnmergedRecordRecordPageToLoad("Data Merge " + FirstName)
                .ClickUnmergeButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to unmerge this record?").TapOKButton();

            mergedRecordRecordPage
                .ClickBackButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Unmerge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UnmergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UnmergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job again (records related to case are updated on the second run) 
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            System.Threading.Thread.Sleep(2000);



            //get the personid and title fields for the Adult Safeguarding record and assert they are correctly updated
            var fields = dbHelper.AdultSafeguarding.GetAdultSafeguardingByID(safeguardingID, "caseid", "personid", "title");
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(_personIDSubordinate, (Guid)fields["personid"]);
            Assert.IsTrue(((string)fields["title"]).StartsWith("Adult Safeguarding within Case Subordinate, " + FirstName + " - (01/01/1999)"));
        }

        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-1976 - Testing Merge records title change - " +
            "Login in the web app - navigate to Settings - Configuration - Data Management - Duplicate Detection - Merge records - Tap on the add new record button - " +
            "Insert a Title - Select a Master person record (with no Child Protection records) - Select a Subordinate person record (with Child Protection records) - Insert a title and record type - " +
            "Tap on the save and close button - wait for the schedule jobs to run - Validate that the records are assigned to the master record and that the titles are updated accordingly")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24558")]
        public void UnmergedRecords_UITestMethod05()
        {

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);



            Guid ownerid = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("BC156AC3-BAFE-E811-80DC-0050560502CC"); //Allocate to Team
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid dataformid = new Guid("EDF4EFC4-BFB1-E811-80DC-0050560502CC"); //Social Care Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime startdatetime = new DateTime(2020, 7, 2);
            int personage = 21;

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(ownerid, _personIDSubordinate, contactreceivedbyid, responsibleuserid, casestatusid, contactreasonid, dataformid, contactsourceid, contactreceiveddatetime, startdatetime, personage);


            var casefields = dbHelper.Case.GetCaseByID(caseID, "title");
            string caseTitle = (string)casefields["title"];
            Guid childprotectioncategoryofabuseid = new Guid("96588309-ac3e-ea11-a2c8-005056926fe4"); //Emotional abuse
            Guid childprotectionstatustypeid = new Guid("cb889fe7-8d3a-e911-a2c5-005056926fe4"); //Status_Default1
            DateTime startdate = new DateTime(2020, 7, 2);
            DateTime datestatuschanged = new DateTime(2020, 7, 2);

            Guid safeguardingID = dbHelper.ChildProtection.CreateChildProtection(ownerid, caseID, caseTitle, _personIDSubordinate, childprotectioncategoryofabuseid, childprotectionstatustypeid, startdate, datestatuschanged);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickMergedRecordsButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad()
                .ClickAddButton();

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .InsertTitle("Data Merge " + FirstName)
                .ClickMasterRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDMaster.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSubordinateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDSubordinate.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Person");


            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job again (records related to case are updated on the second run) 
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);



            Guid masterRecordID = dbHelper.MergedRecord.GetMergedRecordByMasterRecordID(_personIDMaster)[0];

            mergedRecordsPage
                .SelectViewByName("Merged Records")
                .CliickOnRecord(masterRecordID.ToString());

            mergedRecordRecordPage
                .WaitForUnmergedRecordRecordPageToLoad("Data Merge " + FirstName)
                .ClickUnmergeButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to unmerge this record?").TapOKButton();

            mergedRecordRecordPage
                .ClickBackButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Unmerge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UnmergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UnmergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job again (records related to case are updated on the second run) 
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            System.Threading.Thread.Sleep(2000);


            //get the personid and title fields for the Child Protection record and assert they are correctly updated
            var fields = dbHelper.ChildProtection.GetChildProtectionByID(safeguardingID, "caseid", "personid", "title");
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(_personIDSubordinate, (Guid)fields["personid"]);
            Assert.IsTrue(((string)fields["title"]).StartsWith("Child Protection within Case Subordinate, " + FirstName + " - (01/01/1999)"));
        }

        [Description("Issue https://advancedcsg.atlassian.net/browse/CDV6-1976 - Testing Merge records title change - " +
            "Login in the web app - navigate to Settings - Configuration - Data Management - Duplicate Detection - Merge records - Tap on the add new record button - " +
            "Insert a Title - Select a Master person record (with no LAC Legal Status records) - Select a Subordinate person record (with LAC Legal Status records) - Insert a title and record type - " +
            "Tap on the save and close button - wait for the schedule jobs to run - Validate that the records are assigned to the master record and that the titles are updated accordingly")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24559")]
        public void UnmergedRecords_UITestMethod06()
        {

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId, AccommodationStatusId, LivesAloneTypeId, GenderId);



            Guid ownerid = new Guid("B6060DFA-7333-43B2-A662-3D9CADAB12E5"); //CareDirector QA
            Guid contactreceivedbyid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid responsibleuserid = new Guid("FDEABA2C-E8A6-E911-A2C6-005056926FE4"); //Security Test User Admin
            Guid casestatusid = new Guid("BC156AC3-BAFE-E811-80DC-0050560502CC"); //Allocate to Team
            Guid contactreasonid = new Guid("3784785B-9750-E911-A2C5-005056926FE4"); //Advice/Consultation
            Guid dataformid = new Guid("EDF4EFC4-BFB1-E811-80DC-0050560502CC"); //Social Care Case
            Guid contactsourceid = new Guid("31898DB2-AA3E-EA11-A2C8-005056926FE4"); //‘ANONYMOUS’
            DateTime contactreceiveddatetime = new DateTime(2020, 7, 1);
            DateTime startdatetime = new DateTime(2020, 7, 2);
            int personage = 21;

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(ownerid, _personIDSubordinate, contactreceivedbyid, responsibleuserid, casestatusid, contactreasonid, dataformid, contactsourceid, contactreceiveddatetime, startdatetime, personage);


            var casefields = dbHelper.Case.GetCaseByID(caseID, "title");
            string caseTitle = (string)casefields["title"];
            DateTime startdate = new DateTime(2020, 7, 2);

            Guid lacEpisodeID = dbHelper.LACEpisode.CreateLACEpisode(ownerid, caseID, caseTitle, _personIDSubordinate, startdate);


            Guid laclegalstatusreasonid = new Guid("25C63DF3-2D22-EA11-A2C8-005056926FE4"); //Status reason-S- Applicable to Started to Be Looked After
            Guid laclegalstatusid = new Guid("FAC4D2FC-1269-EA11-A2CB-005056926FE4"); //Accommodated under an agreed series of short-term breaks, when agreements are recorded (NOT individual episodes of care)
            Guid lacplacementid = new Guid("1485591D-B069-EA11-A2CB-005056926FE4"); //All Residential schools, except where dual-registered as a school and Children’s Home
            Guid laclocationcodeid = new Guid("3C067F2F-5F66-E911-A2C5-005056926FE4"); //Test LAC Location  
            Guid lacplacementproviderid = new Guid("4E049753-B669-EA11-A2CB-005056926FE4"); //Other local authority provision, including a regional adoption agency where another local authority is the host authority

            Guid lacLegalStatusID = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(OwnerID, caseID, caseTitle, _personIDSubordinate, lacEpisodeID, laclegalstatusreasonid, laclegalstatusid, lacplacementid, laclocationcodeid, lacplacementproviderid, startdate);



            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickMergedRecordsButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad()
                .ClickAddButton();

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .InsertTitle("Data Merge " + FirstName)
                .ClickMasterRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDMaster.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSubordinateRecordLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(FirstName).TapSearchButton().SelectResultElement(_personIDSubordinate.ToString());

            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickRecordTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().ClickResultElementByText("Person");


            mergedRecordRecordPage
                .WaitForMergedRecordRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();



            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job again (records related to case are updated on the second run) 
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);


            Guid masterRecordID = dbHelper.MergedRecord.GetMergedRecordByMasterRecordID(_personIDMaster)[0];

            mergedRecordsPage
                .SelectViewByName("Merged Records")
                .CliickOnRecord(masterRecordID.ToString());

            mergedRecordRecordPage
                .WaitForUnmergedRecordRecordPageToLoad("Data Merge " + FirstName)
                .ClickUnmergeButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to unmerge this record?").TapOKButton();

            mergedRecordRecordPage
                .ClickBackButton();

            mergedRecordsPage
                .WaitForMergedRecordsPageToLoad();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Unmerge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UnmergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UnmergeRecords_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Sync Record Titles" schedule job again (records related to case are updated on the second run) 
            this.WebAPIHelper.ScheduleJob.Execute(SyncRecordTitles_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(SyncRecordTitles_ScheduleJobID);

            System.Threading.Thread.Sleep(2000);




            //get the personid and title fields for the LAC Episode record and assert they are correctly updated
            var fields = dbHelper.LACEpisode.GetLACEpisodeByID(lacEpisodeID, "caseid", "personid", "title");
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(_personIDSubordinate, (Guid)fields["personid"]);
            Assert.IsTrue(((string)fields["title"]).StartsWith("LAC episode within Case Subordinate, " + FirstName + " - (01/01/1999)"));


            //get the personid and title fields for the LAC Legal Status record and assert they are correctly updated
            fields = dbHelper.PersonLACLegalStatus.GetPersonLACLegalStatusByID(lacLegalStatusID, "caseid", "personid", "title");
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(_personIDSubordinate, (Guid)fields["personid"]);
            Assert.IsTrue(((string)fields["title"]).StartsWith("LAC Legal Status within Case Subordinate, " + FirstName + " - (01/01/1999)"));
        }



        #endregion
    }


}
