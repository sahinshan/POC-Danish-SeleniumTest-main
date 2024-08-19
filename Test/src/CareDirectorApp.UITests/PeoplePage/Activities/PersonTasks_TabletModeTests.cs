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
    [Category("Mobile_TabletMode_Online")]
    public class PersonTasks_TabletModeTests : TestBase
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

            //if the cases Task injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the cases Task review pop-up is open then close it 
            personBodyMapReviewPopup.ClosePopupIfOpen();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();



            //navigate to the Settings page
            mainMenu.NavigateToPeoplePage();



            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();
        }

        #region person Tasks page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6704")]
        [Description("UI Test for Dashboards - 0001 - " +
            "Navigate to the person Tasks area (do not contains Task records) - Validate the page content")]
        public void PersonTasks_TestMethod1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any Task for the person
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByPersonID(personID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6705")]
        [Description("UI Test for Dashboards - 0002 - " +
            "Navigate to the person Tasks area (person contains Task records) - Validate the page content")]
        public void PersonTasks_TestMethod2()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid caseNoteID = new Guid("8aa47fae-1ea0-ea11-a2cd-005056926fe4");

            peoplePage
                .WaitForPeoplePageToLoad()
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
        [Property("JiraIssueID", "CDV6-6706")]
        [Description("UI Test for Dashboards - 0003 - " +
            "Navigate to the person Tasks area - Open a person Task record - Validate that the Task record page is displayed")]
        public void PersonTasks_TestMethod3()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string TaskSubject = "Task 001";

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6707")]
        [Description("UI Test for Dashboards - 0004 - " +
            "Navigate to the person Tasks area - Open a person Task record - Validate that the Task record page field titles are displayed")]
        public void PersonTasks_TestMethod4()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string TaskSubject = "Task 001";

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .ValidateResponsibleUserFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6708")]
        [Description("UI Test for Dashboards - 0005 - " +
            "Navigate to the person Tasks area - Open a person Task record - Validate that the Task record page fields are correctly displayed")]
        public void PersonTasks_TestMethod5()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string TaskSubject = "Task 001";

            peoplePage
                .WaitForPeoplePageToLoad()
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

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6709")]
        [Description("UI Test for Dashboards - 0006 - " +
            "Navigate to the person Tasks area - Open a person Task record (with only the mandatory information set) - Validate that the Task record page fields are correctly displayed")]
        public void PersonTasks_TestMethod6()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string TaskSubject = "Task 002";

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 002")
                .ValidateSubjectFieldText(TaskSubject)
                .ValidateDescriptionFieldText("")

                .ValidateReasonFieldText("")
                .ValidatePriorityFieldText("")
                .ValidateDueFieldText("", "")
                .ValidateOutcomeFieldText("")
                .ValidateStatusFieldText("Open")

                .ValidateCategoryFieldText("")
                .ValidateSubCategoryFieldText("")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateResponsibleUserFieldText("");
        }

        #endregion

        #region New Record page - Validate content

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6710")]
        [Description("UI Test for Dashboards - 0007 - " +
            "Navigate to the person Tasks area - Tap on the add button - Validate that the new record page is displayed and all field titles are visible ")]
        public void PersonTasks_TestMethod7()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnAddNewRecordButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASKS")
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
                .ValidateResponsibleUserFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6711")]
        [Description("UI Test for Dashboards - 0008 - " +
            "Navigate to the person Tasks area - Tap on the add button - Validate that the new record page is displayed but the delete button is not displayed")]
        public void PersonTasks_TestMethod8()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnAddNewRecordButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASKS")
                .WaitForDeleteButtonNotVisible();
        }

        #endregion

        #region Create New Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6712")]
        [Description("UI Test for Dashboards - 0009 - " +
            "Navigate to the person Tasks area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void PersonTasks_TestMethod9()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Task for the person
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByPersonID(personID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
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
               .TapOnSaveButton()
               .WaitForPersonTaskRecordPageToLoad("TASK: Task 001");


            var tasks = this.PlatformServicesHelper.task.GetTaskByPersonID(personID);

            Assert.AreEqual(1, tasks.Count);

            var fields = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "personid", "duedate", "statusid", "regardingid", "regardingidname", "regardingidtablename");

            var datefield = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "duedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["duedate"]);
            string DueDate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");


            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
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
            Assert.AreEqual(new DateTime(2020, 5, 20, 9, 0, 0).ToString("dd'/'MM'/'yyyy HH:mm"), DueDate);
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual(personID, (Guid)fields["regardingid"]);
            Assert.AreEqual("Maria Tsatsouline", (string)fields["regardingidname"]);
            Assert.AreEqual("person", (string)fields["regardingidtablename"]);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6713")]
        [Description("UI Test for Dashboards - 0010 - " +
            "Navigate to the person Tasks area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - Validate that the data is sync to the web platform")]
        public void PersonTasks_TestMethod10()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Task for the person
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByPersonID(personID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
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
               .TapResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile Team").TapSearchButtonQuery().TapOnRecord("Mobile Team 2");

            personTaskRecordPage
               .WaitForPersonTaskRecordPageToLoad("TASKS")
               .TapOnSaveAndCloseButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad();


            var tasks = this.PlatformServicesHelper.task.GetTaskByPersonID(personID);

            Assert.AreEqual(1, tasks.Count);

            var fields = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "personid", "duedate", "statusid", "regardingid", "regardingidname", "regardingidtablename");


            var datefield = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "duedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["duedate"]);
            string DueDate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");


            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam2 = new Guid("932692CD-429E-E911-A2C6-005056926FE4"); //Mobile Team 2
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
            Assert.AreEqual(mobileTeam2, (Guid)fields["ownerid"]);
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
            Assert.AreEqual(new DateTime(2020, 5, 20, 9, 0, 0).ToString("dd'/'MM'/'yyyy HH:mm"), DueDate);
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual(personID, (Guid)fields["regardingid"]);
            Assert.AreEqual("Maria Tsatsouline", (string)fields["regardingidname"]);
            Assert.AreEqual("person", (string)fields["regardingidtablename"]);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6714")]
        [Description("UI Test for Dashboards - 0011 - " +
            "Navigate to the person Tasks area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - " +
            "Re-Open the record - Validate that all fields are correctly set after saving the record")]
        public void PersonTasks_TestMethod11()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Task for the person
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByPersonID(personID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .WaitForPersonTasksPageToLoad();


            var tasks = this.PlatformServicesHelper.task.GetTaskByPersonID(personID);
            Assert.AreEqual(1, tasks.Count);

            personTasksPage
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
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6715")]
        [Description("UI Test for Dashboards - 0012 - " +
            "Navigate to the person Tasks area - Tap on the add button - Set data only in mandatory fields - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void PersonTasks_TestMethod12()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Task for the person
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByPersonID(personID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .TapOnSaveButton()
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001");


            var tasks = this.PlatformServicesHelper.task.GetTaskByPersonID(personID);
            Assert.AreEqual(1, tasks.Count);

            var fields = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "personid", "duedate", "statusid", "regardingid", "regardingidname", "regardingidtablename");

            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid businessUnitID = new Guid("BA5B9FE6-419E-E911-A2C6-005056926FE4"); //Mobile Business Unit


            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.AreEqual(businessUnitID, (Guid)fields["owningbusinessunitid"]);
            Assert.AreEqual("Task 001", (string)fields["subject"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual(false, fields.ContainsKey("notes"));
            Assert.IsFalse(fields.ContainsKey("activitycategoryid"));
            Assert.IsFalse(fields.ContainsKey("activitysubcategoryid"));
            Assert.IsFalse(fields.ContainsKey("activityoutcomeid"));
            Assert.IsFalse(fields.ContainsKey("activityreasonid"));
            Assert.IsFalse(fields.ContainsKey("activitypriorityid"));
            Assert.AreEqual(false, (bool)fields["informationbythirdparty"]);
            Assert.AreEqual(false, (bool)fields["issignificantevent"]);
            Assert.IsFalse(fields.ContainsKey("caseid"));
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.IsFalse(fields.ContainsKey("duedate"));
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual(personID, (Guid)fields["regardingid"]);
            Assert.AreEqual("Maria Tsatsouline", (string)fields["regardingidname"]);
            Assert.AreEqual("person", (string)fields["regardingidtablename"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6716")]
        [Description("UI Test for Dashboards - 0013 - " +
            "Navigate to the person Tasks area - Tap on the add button - Set data only in mandatory fields except for Subject- " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void PersonTasks_TestMethod13()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Task for the person
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByPersonID(personID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
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
                //.InsertSubject("Task 001")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Subject' is required")
                .TapOnOKButton();

            var tasks = this.PlatformServicesHelper.task.GetTaskByPersonID(personID);
            Assert.AreEqual(0, tasks.Count);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6717")]
        [Description("UI Test for Dashboards - 0014 - " +
            "Navigate to the person Tasks area - Tap on the add button - Set data only in mandatory fields except for Responsible Team - " +
            "Tap on the Save button - Validate that the record is saved and the Responsible Team field is set with the user team")]
        public void PersonTasks_TestMethod14()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Task for the person
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByPersonID(personID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .TapResponsibleTeamRemoveButton()
                .TapOnSaveAndCloseButton();

            personTasksPage
               .WaitForPersonTasksPageToLoad();

            var tasks = this.PlatformServicesHelper.task.GetTaskByPersonID(personID);
            Assert.AreEqual(1, tasks.Count);

            var fields = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "ownerid");

            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);

        }

        #endregion

        #region Update Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6718")]
        [Description("UI Test for Dashboards - 0015 - Create a new person Task using the main APP web services" +
            "Navigate to the person Tasks area - open the Task record - validate that all fields are correctly synced")]
        public void PersonTasks_TestMethod15()
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

            Guid personTaskID = this.PlatformServicesHelper.task.CreateTask("Task 001", "Task 001 description", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, 
                activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, null, personID, date, personID, "Maria Tsatsouline", "person");

            peoplePage
                .WaitForPeoplePageToLoad()
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

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6719")]
        [Description("UI Test for Dashboards - 0016 - Create a new person Task using the main APP web services" +
            "Navigate to the person Tasks area - open the Task record - clear all non mandatory fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonTasks_TestMethod16()
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

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapTasksIcon_RelatedItems();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .TapOnRecord("Task 001");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
                .InsertDescription("")
                .TapReasonRemoveButton()
                .TapPriorityRemoveButton()
                .ValidateDueFieldText("20/05/2020", "09:00")
                .TapOutcomeRemoveButton()
                .TapSubCategoryRemoveButton()
                .TapCategoryRemoveButton()
                .TapOnSaveAndCloseButton();

            personTasksPage
              .WaitForPersonTasksPageToLoad();


            var tasks = this.PlatformServicesHelper.task.GetTaskByPersonID(personID);
            Assert.AreEqual(1, tasks.Count);

            var fields = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "personid", "duedate", "statusid", "regardingid", "regardingidname", "regardingidtablename");

            var datefield = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "duedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["duedate"]);
            string DueDate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");




            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.AreEqual(businessUnitID, (Guid)fields["owningbusinessunitid"]);
            Assert.AreEqual("Task 001", (string)fields["subject"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual("<div></div>", fields["notes"]);
            Assert.IsFalse(fields.ContainsKey("activitycategoryid"));
            Assert.IsFalse(fields.ContainsKey("activitysubcategoryid"));
            Assert.IsFalse(fields.ContainsKey("activityoutcomeid"));
            Assert.IsFalse(fields.ContainsKey("activityreasonid"));
            Assert.IsFalse(fields.ContainsKey("activitypriorityid"));
            Assert.AreEqual(false, (bool)fields["informationbythirdparty"]);
            Assert.AreEqual(false, (bool)fields["issignificantevent"]);
            Assert.IsFalse(fields.ContainsKey("caseid"));
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(date.ToString("dd'/'MM'/'yyyy HH:mm"), DueDate);
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual(personID, (Guid)fields["regardingid"]);
            Assert.AreEqual("Maria Tsatsouline", (string)fields["regardingidname"]);
            Assert.AreEqual("person", (string)fields["regardingidtablename"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6720")]
        [Description("UI Test for Dashboards - 0017 - Create a new person Task using the main APP web services" +
            "Navigate to the person Tasks area - open the Task record - update all fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonTasks_TestMethod17()
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

            peoplePage
                .WaitForPeoplePageToLoad()
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


            var tasks = this.PlatformServicesHelper.task.GetTaskByPersonID(personID);
            Assert.AreEqual(1, tasks.Count);


            Guid updated_activitycategoryid = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"); //Assessment
            Guid updated_activitysubcategoryid = new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"); //Health Assessment
            Guid updated_activityoutcomeid = new Guid("4C2BEC1C-9E45-E911-A2C5-005056926FE4"); // Completed
            Guid updated_activityreasonid = new Guid("B9EC74E3-9C45-E911-A2C5-005056926FE4"); //First response
            Guid updated_activitypriorityid = new Guid("1E164C51-9D45-E911-A2C5-005056926FE4"); //High

            var fields = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "personid", "duedate", "statusid", "regardingid", "regardingidname", "regardingidtablename");

            var datefield = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "duedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["duedate"]);
            string DueDate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");


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
            Assert.AreEqual(new DateTime(2020, 5, 21, 9, 30, 0).ToString("dd'/'MM'/'yyyy HH:mm"), DueDate);
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual(personID, (Guid)fields["regardingid"]);
            Assert.AreEqual("Maria Tsatsouline", (string)fields["regardingidname"]);
            Assert.AreEqual("person", (string)fields["regardingidtablename"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6721")]
        [Description("UI Test for Dashboards - 0018 - Create a new person Task using the main APP web services" +
            "Navigate to the person Tasks area - open the Task record - Set the status to Completed - Save the record - Validate that the record gets deactivated")]
        public void PersonTasks_TestMethod18()
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

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .TapStatusPicklist();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(1)
                .TapOKButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
                .TapOnSaveAndCloseButton();

            personTasksPage
               .WaitForPersonTasksPageToLoad()
               .ValidateNoRecordsMessageVisibility(true);


            var fields = this.PlatformServicesHelper.task.GetTaskByID(personTaskID,"inactive");
            Assert.AreEqual(true, (bool)fields["inactive"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6722")]
        [Description("UI Test for Dashboards - 0019 - Create a new person Task using the main APP web services" +
            "Navigate to the person Tasks area - open the Task record - Set the status to Canceled - Save the record - Validate that the record gets deactivated")]
        public void PersonTasks_TestMethod19()
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

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .TapStatusPicklist();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(2)
                .TapOKButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
                .TapOnSaveAndCloseButton();

            personTasksPage
               .WaitForPersonTasksPageToLoad()
               .ValidateNoRecordsMessageVisibility(true);


            var fields = this.PlatformServicesHelper.task.GetTaskByID(personTaskID, "inactive");
            Assert.AreEqual(true, (bool)fields["inactive"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6723")]
        [Description("UI Test for Dashboards - 0021 - Create a new person Task using the main APP web services" +
            "Navigate to the person Tasks area - open the Task record - Set the status to Canceled - Save the record - Validate that the record is no longer editable")]
        public void PersonTasks_TestMethod21()
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

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .TapStatusPicklist();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(2)
                .TapOKButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
                .TapOnSaveAndCloseButton();

            personTasksPage
               .WaitForPersonTasksPageToLoad()
               .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            personTasksPage
               .WaitForPersonTasksPageToLoad("Inactive Tasks")
               .TapOnRecord("Task 001");

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("TASK: Task 001")
                .ValidateReasonRemoveButtonVisible(false)
                .ValidatePriorityRemoveButtonVisible(false)
                .ValidateOutcomeRemoveButtonVisible(false)
                .ValidateCategoryRemoveButtonVisible(false)
                .ValidateSubCategoryRemoveButtonVisible(false)
                .ValidateResponsibleTeamRemoveButtonVisible(false)
                .ValidateReasonLookupButtonVisible(false)
                .ValidatePriorityLookupButtonVisible(false)
                .ValidateOutcomeLookupButtonVisible(false)
                .ValidateCategoryLookupButtonVisible(false)
                .ValidateSubCategoryLookupButtonVisible(false)
                .ValidateResponsibleTeamLookupButtonVisible(false);

        }

        #endregion

        #region Delete record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6724")]
        [Description("UI Test for Dashboards - 0020 - Create a new person Task using the main APP web services" +
            "Navigate to the person Tasks area - open the Task record - Tap on the delete button - " +
            "Validate that the record is deleted from the web app database")]
        public void PersonTasks_TestMethod20()
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

            peoplePage
                .WaitForPeoplePageToLoad()
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
