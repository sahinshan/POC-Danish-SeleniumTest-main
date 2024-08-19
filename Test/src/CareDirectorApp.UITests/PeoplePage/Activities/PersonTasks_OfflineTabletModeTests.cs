using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.Activities
{
    /// <summary>
    /// This class contains all test methods for cases Tasks validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class PersonTasks_OfflineTabletModeTests : TestBase
    {
        static UIHelper uIHelper;

        [TestFixtureSetUp]
        public void ClassInitializationMethod()
        {
            if (this.IgnoreTestFixtureSetUp)
                return;

            //authenticate a user against the platform services
            this.PlatformServicesHelper = new PlatformServicesHelper("mobile_test_user_1", "Passw0rd_!");

            //start the APP
            uIHelper = new UIHelper();
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //set the default URL
            this.SetDefaultEndpointURL();

            //Login with test user account
            var changeUserButtonVisible = loginPage.WaitForBasicLoginPageToLoad().GetChangeUserButtonVisibility();
            if (changeUserButtonVisible)
            {
                //Login with test user account
                loginPage
                    .WaitForBasicLoginPageToLoad()
                    .TapChangeUserButton();

                warningPopup
                    .WaitForWarningPopupToLoad()
                    .TapOnYesButton();

                loginPage
                   .WaitForLoginPageToLoad()
                   .InsertUserName("Mobile_Test_User_1")
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

                //if the offline mode warning is displayed, then close it
                warningPopup.TapNoButtonIfPopupIsOpen();

                //wait for the homepage to load
                homePage
                    .WaitForHomePageToLoad();
            }
            else
            {
                //Login with test user account
                loginPage
                    .WaitForBasicLoginPageToLoad()
                    .InsertUserName("Mobile_Test_User_1")
                    .InsertPassword("Passw0rd_!")
                    .TapLoginButton();

                //Set the PIN Code
                pinPage
                    .WaitForPinPageToLoad()
                    .TapButton1()
                    .TapButton2()
                    .TapButton3()
                    .TapButton4()
                    .TapButtonOK()
                    .WaitForConfirmationPinPageToLoad()
                    .TapButton1()
                    .TapButton2()
                    .TapButton3()
                    .TapButton4()
                    .TapButtonOK();

                //wait for the homepage to load
                homePage
                    .WaitForHomePageToLoad();
            }
        }

        [SetUp]
        public void TestInitializationMethod()
        {
            if (this.IgnoreSetUp)
                return;

            //close the lookup popup if it is open
            lookupPopup.ClosePopupIfOpen();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

            //navigate to the settings page
            mainMenu.NavigateToSettingsPage();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

            //if the APP is in offline mode change it to online mode
            settingsPage.SetTheAppInOnlineMode();

        }

        #region person Tasks page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6699")]
        [Description("UI Test for person Tasks (Offline Mode) - 0002 - " +
            "Navigate to the person Tasks area (person contains Task records) - Validate the page content")]
        public void PersonTasks_Offlinemode_TestMethod2()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid caseNoteID = new Guid("8aa47fae-1ea0-ea11-a2cd-005056926fe4");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ValidateSubjectCellText("Task 001", caseNoteID.ToString())
                .ValidateDueCellText("20/05/2020 07:00", caseNoteID.ToString())
                .ValidateResponsibleTeamCellText("Mobile Team 1", caseNoteID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", caseNoteID.ToString())
                .ValidateCreatedOnCellText("27/05/2020 14:33", caseNoteID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", caseNoteID.ToString())
                .ValidateModifiedOnCellText("27/05/2020 14:33", caseNoteID.ToString());
        }

        #endregion

        #region Open existing records



        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6600")]
        [Description("UI Test for person Tasks (Offline Mode) - 0004 - " +
            "Navigate to the person Tasks area - Open a person Task record - Validate that the Task record page field titles and fields are displayed")]
        public void PersonTasks_Offlinemode_TestMethod4()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string TaskSubject = "Task 001";

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord(TaskSubject);

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
                .ValidateSubjectFieldTitleVisible(true)
                .ValidateDescriptionFieldTitleVisible(true)

                .ValidateReasonFieldTitleVisible(true)
                .ValidatePriorityFieldTitleVisible(true)
                .ValidateDueFieldTitleVisible(true)
                .ValidateOutcomeFieldTitleVisible(true)
                .ValidateStatusFieldTitleVisible(true)
                .ValidateCategoryFieldTitleVisible(true)
                .ValidateSubCategoryFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true)

                .ValidateSubjectFieldText(TaskSubject)
                .ValidateDescriptionRichFieldText("Task 001 description") //for this record the description is displayed as rich text

                .ValidateReasonFieldText("Other")
                .ValidatePriorityFieldText("Low")
                .ValidateDueFieldText("20/05/2020", "07:00")
                .ValidateOutcomeFieldText("More information needed")
                .ValidateStatusFieldText("Open")
                .ValidateCategoryFieldText("Advice")
                .ValidateSubCategoryFieldText("Home Support")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateResponsibleUserFieldText("Mobile Test User 1");
        }

        #endregion

        #region Create New Record



        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6601")]
        [Description("UI Test for person Tasks (Offline Mode) - 0010 - " +
            "Navigate to the person Tasks area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - Validate that the data is sync to the web platform")]
        public void PersonTasks_Offlinemode_TestMethod10()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any Task for the person
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByPersonID(personID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnAddNewRecordButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASKS")
                .InsertSubject("Task 001")
                .InsertDescription("Task 001 description")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Assessment");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASKS")
                .TapPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Normal");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASKS")
                .InsertDueDate("20/05/2020", "09:00")
                .TapOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("More information needed");

            personTaskRecordPage
               .WaitForPersonTaskRecordPageToLoad("TASKS")
               .TapCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Advice");

            personTaskRecordPage
               .WaitForPersonTaskRecordPageToLoad("TASKS")
               .TapSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Home Support");

            personTaskRecordPage
               .WaitForPersonTaskRecordPageToLoad("TASKS")
               .TapOnSaveAndCloseButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task 001");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
                .ValidateSubjectFieldText("Task 001")
                .ValidateDescriptionFieldText("Task 001 description")

                .ValidateReasonFieldText("Assessment")
                .ValidatePriorityFieldText("Normal")
                .ValidateDueFieldText("20/05/2020", "09:00")
                .ValidateOutcomeFieldText("More information needed")
                .ValidateStatusFieldText("Open")

                .ValidateCategoryFieldText("Advice")
                .ValidateSubCategoryFieldText("Home Support")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateResponsibleUserFieldText("Mobile Test User 1");


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            
            var tasks = this.PlatformServicesHelper.task.GetTaskByPersonID(personID);

            Assert.AreEqual(1, tasks.Count);

            var fields = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "personid", "duedate", "statusid", "regardingid", "regardingidname", "regardingidtablename");

            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid businessUnitID = new Guid("BA5B9FE6-419E-E911-A2C6-005056926FE4"); //Mobile Business Unit
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal


            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.AreEqual(businessUnitID, (Guid)fields["owningbusinessunitid"]);
            Assert.AreEqual("Task 001", (string)fields["subject"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual("<div>Task 001 description</div>", (string)fields["notes"]);
            Assert.AreEqual(activitycategoryid, (Guid)fields["activitycategoryid"]);
            Assert.AreEqual(activitysubcategoryid, (Guid)fields["activitysubcategoryid"]);
            Assert.AreEqual(activityoutcomeid, (Guid)fields["activityoutcomeid"]);
            Assert.AreEqual(activityreasonid, (Guid)fields["activityreasonid"]);
            Assert.AreEqual(activitypriorityid, (Guid)fields["activitypriorityid"]);
            Assert.AreEqual(false, (bool)fields["informationbythirdparty"]);
            Assert.AreEqual(false, (bool)fields["issignificantevent"]);
            Assert.IsFalse(fields.ContainsKey("caseid"));
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(new DateTime(2020, 5, 20, 9, 0, 0), ((DateTime)fields["duedate"]).ToLocalTime());
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual(personID, (Guid)fields["regardingid"]);
            Assert.AreEqual("Maria Tsatsouline", (string)fields["regardingidname"]);
            Assert.AreEqual("person", (string)fields["regardingidtablename"]);

        }

        #endregion

        #region Update Record



        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6602")]
        [Description("UI Test for person Tasks (Offline Mode) - 0017 - Create a new person Task using the main APP web services" +
            "Navigate to the person Tasks area - open the Task record - update all fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonTasks_Offlinemode_TestMethod17()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid businessUnitID = new Guid("BA5B9FE6-419E-E911-A2C6-005056926FE4"); //Mobile Business Unit
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal
            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);


            //remove any Task for the person
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByPersonID(personID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            Guid personTaskID = this.PlatformServicesHelper.task.CreateTask("Task 001", "Task 001 description", mobileTeam1, mobile_test_user_1UserID, 
                activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, null, personID, date, personID, "Maria Tsatsouline", "person");


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();


            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task 001");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
                .InsertSubject("Task 001 updated")
                .InsertDescription("Task 001 description updated")
                .InsertDueDate("21/05/2020", "09:30")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("First").TapSearchButtonQuery().TapOnRecord("First Response");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
                .TapPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("High");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
                .TapOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Completed");

            personTaskRecordPage
               .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
               .TapCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Assessment");

            personTaskRecordPage
               .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
               .TapSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Health Assessment");

            personTaskRecordPage
               .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
               .TapOnSaveAndCloseButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad();


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            

            var tasks = this.PlatformServicesHelper.task.GetTaskByPersonID(personID);
            Assert.AreEqual(1, tasks.Count);


            Guid updated_activitycategoryid = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"); //Assessment
            Guid updated_activitysubcategoryid = new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"); //Health Assessment
            Guid updated_activityoutcomeid = new Guid("4C2BEC1C-9E45-E911-A2C5-005056926FE4"); // Completed
            Guid updated_activityreasonid = new Guid("B9EC74E3-9C45-E911-A2C5-005056926FE4"); //First response
            Guid updated_activitypriorityid = new Guid("1E164C51-9D45-E911-A2C5-005056926FE4"); //High

            var fields = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "personid", "duedate", "statusid", "regardingid", "regardingidname", "regardingidtablename");
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.AreEqual(businessUnitID, (Guid)fields["owningbusinessunitid"]);
            Assert.AreEqual("Task 001 updated", (string)fields["subject"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual("<div>Task 001 description updated</div>", (string)fields["notes"]);
            Assert.AreEqual(updated_activitycategoryid, (Guid)fields["activitycategoryid"]);
            Assert.AreEqual(updated_activitysubcategoryid, (Guid)fields["activitysubcategoryid"]);
            Assert.AreEqual(updated_activityoutcomeid, (Guid)fields["activityoutcomeid"]);
            Assert.AreEqual(updated_activityreasonid, (Guid)fields["activityreasonid"]);
            Assert.AreEqual(updated_activitypriorityid, (Guid)fields["activitypriorityid"]);
            Assert.AreEqual(false, (bool)fields["informationbythirdparty"]);
            Assert.AreEqual(false, (bool)fields["issignificantevent"]);
            Assert.IsFalse(fields.ContainsKey("caseid"));
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(new DateTime(2020, 5, 21, 9, 30, 0), ((DateTime)fields["duedate"]).ToLocalTime());
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual(personID, (Guid)fields["regardingid"]);
            Assert.AreEqual("Maria Tsatsouline", (string)fields["regardingidname"]);
            Assert.AreEqual("person", (string)fields["regardingidtablename"]);
        }



        #endregion

        #region Delete record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6603")]
        [Description("UI Test for person Tasks (Offline Mode) - 0020 - Create a new person Task using the main APP web services" +
            "Navigate to the person Tasks area - open the Task record - Tap on the delete button - " +
            "Validate that the record is deleted from the web app database")]
        public void PersonTasks_Offlinemode_TestMethod20()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal
            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);


            //remove any Task for the person
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByPersonID(personID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            Guid personTaskID = this.PlatformServicesHelper.task.CreateTask("Task 001", "Task 001 description", mobileTeam1, mobile_test_user_1UserID, 
                activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, null, personID, date, personID, "Maria Tsatsouline", "person");



            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task 001");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
                .TapOnDeleteButton();

            warningPopup
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?")
                .TapOnYesButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad();


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            
            var records = this.PlatformServicesHelper.task.GetTaskByPersonID(personID);
            Assert.AreEqual(0, records.Count);
        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}
