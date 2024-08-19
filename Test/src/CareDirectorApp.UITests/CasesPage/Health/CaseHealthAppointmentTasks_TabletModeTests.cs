using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.Cases.Health
{
    /// <summary>
    /// This class contains all test methods for health Appointment Tasks validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class CaseHealthAppointmentTasks : TestBase
    {
        static UIHelper uIHelper;

        #region properties

        string LayoutXml_NoWidgets = "";
        string LayoutXml_OneWidgetNotConfigured = "<Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidget_GridView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-Indigo</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidgetListView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_TwoWidgets_GridView_ListView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Task (For Case) - Active Records</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>ae1c53f3-3010-e911-80dc-0050560502cc</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>          <BusinessObjectName>casetask</BusinessObjectName>          <HeaderColour>bg-Green</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidget_SharedUserView = "  <Layout  >    <Widgets>      <widget>        <Title>Task - Tasks created in the last 30 days</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>65ffa115-4890-ea11-a2cd-005056926fe4</DataViewId>          <DataViewType>shared</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightBlue</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_AllWidgets = "  <Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>IFrame</Type>          <Url>www.google.com</Url>          <HeaderColour>bg-Pink</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Task - My Active Tasks</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightBlue</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>6</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>Chart</Type>          <BusinessObjectId>30f84b2d-b169-e411-bf00-005056c00008</BusinessObjectId>          <BusinessObjectName>case</BusinessObjectName>          <ChartId>5b540885-d816-ea11-a2c8-005056926fe4</ChartId>          <ChartGroup>System</ChartGroup>          <HeaderColour>bg-Yellow</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Task (For Case) - Active Records</Title>        <X>0</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>ae1c53f3-3010-e911-80dc-0050560502cc</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>          <BusinessObjectName>casetask</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>3</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>WidgetCatalogue</Type>          <HTMLWebResource>Html_ClinicAvailableSlotsSearch.html</HTMLWebResource>          <HeaderColour>bg-Red</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>6</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>Report</Type>          <HeaderColour>bg-Purple</HeaderColour>          <HideHeader>false</HideHeader>          <ReportId>dc3d3d9f-1765-ea11-a2cb-005056926fe4</ReportId>        </Settings>      </widget>    </Widgets>  </Layout>";


        #endregion

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

            //if the health Appointment Task injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the health Appointment Task review pop-up is open then close it 
            personBodyMapReviewPopup.ClosePopupIfOpen();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();



            //navigate to the Settings page
            mainMenu.NavigateToCasesPage();



            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();
        }

        #region Create New Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6515")]
        [Description("UI Test for Health Appointment Task records - 0009 - " +
            "Navigate to the health Appointment Tasks area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void healthAppointmentTasks_TestMethod01()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid healthAppointmentID = new Guid("ade9ebb8-1db6-ea11-a2cd-005056926fe4");
            string appointmentDate = "01/06/2020";


            //remove any Task for the case
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByHealthAppointmentID(healthAppointmentID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthAppointmentsIcon_RelatedItems();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad("Active Appointments")
                .TapOnRecord(appointmentDate);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Mr Pavel MCNamara, Assessment, Clients or patients home")
                .TapRelatedItemsButton()
                .TapTasksIcon();

            caseTasksPage
                .WaitForCaseTasksPageToLoad()
                .TapOnAddNewRecordButton();

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASKS")
                .InsertSubject("Task 001")
                .InsertDescription("Task 001 description")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Assessment");

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASKS")
                .TapPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Normal");

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASKS")
                .InsertDueDate("20/05/2020", "09:00")
                .TapOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("More information needed");

            caseTaskRecordPage
               .WaitForCaseTaskRecordPageToLoad("TASKS")
               .TapCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Advice");

            caseTaskRecordPage
               .WaitForCaseTaskRecordPageToLoad("TASKS")
               .TapSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Home Support");

            caseTaskRecordPage
               .WaitForCaseTaskRecordPageToLoad("TASKS")
               .TapOnSaveButton()
               .WaitForCaseTaskRecordPageToLoad("TASK: Task 001");


            var tasks = this.PlatformServicesHelper.task.GetTaskByHealthAppointmentID(healthAppointmentID);
            Assert.AreEqual(1, tasks.Count);
            var fields = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "personid", "duedate", "statusid", "regardingid", "regardingidname", "regardingidtablename");
            
            var dueDateField = this.PlatformServicesHelper.task.GetTaskByID(tasks[0], "duedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var DueDateConverted = usersettings.ConvertTimeFromUtc((DateTime)dueDateField["duedate"]);

            string DueDate = DueDateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");

            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
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
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(new DateTime(2020, 5, 20, 9, 0, 0).ToString("dd'/'MM'/'yyyy HH:mm"), DueDate);
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual(healthAppointmentID, (Guid)fields["regardingid"]);
            Assert.AreEqual("Mr Pavel MCNamara, Assessment, Clients or patients home", (string)fields["regardingidname"]);
            Assert.AreEqual("healthappointment", (string)fields["regardingidtablename"]);

        }

        #endregion

        #region Update Record



        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6516")]
        [Description("UI Test for Health Appointment Task records - 0016 - Create a new health Appointment Task using the main APP web services" +
            "Navigate to the health Appointment Tasks area - open the Task record - clear all non mandatory fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void healthAppointmentTasks_TestMethod02()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid healthAppointmentID = new Guid("ade9ebb8-1db6-ea11-a2cd-005056926fe4");
            string appointmentDate = "01/06/2020";
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


            //remove any Task for the case
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByHealthAppointmentID(healthAppointmentID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            Guid taskID = this.PlatformServicesHelper.task.CreateTask("Task 001", "Task 001 description", mobileTeam1, mobile_test_user_1UserID, 
                activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date, healthAppointmentID, "Mr Pavel MCNamara, Assessment, Clients or patients home", "healthappointment");

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthAppointmentsIcon_RelatedItems();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad("Active Appointments")
                .TapOnRecord(appointmentDate);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Mr Pavel MCNamara, Assessment, Clients or patients home")
                .TapRelatedItemsButton()
                .TapTasksIcon();

            caseTasksPage
                .WaitForCaseTasksPageToLoad()
                .TapOnRecord("Task 001");

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
                .InsertDescription("")
                .TapReasonRemoveButton()
                .TapPriorityRemoveButton()
                .ValidateDueFieldText("20/05/2020", "09:00")
                .TapOutcomeRemoveButton()
                .TapSubCategoryRemoveButton()
                .TapCategoryRemoveButton()
                .TapOnSaveAndCloseButton();

            caseTasksPage
              .WaitForCaseTasksPageToLoad();



            var fields = this.PlatformServicesHelper.task.GetTaskByID(taskID, "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "personid", "duedate", "statusid", "regardingid", "regardingidname", "regardingidtablename");

            var dueDateField = this.PlatformServicesHelper.task.GetTaskByID(taskID, "duedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var DueDateConverted = usersettings.ConvertTimeFromUtc((DateTime)dueDateField["duedate"]);

            string DueDate = DueDateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");




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
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(date.ToString("dd'/'MM'/'yyyy HH:mm"), DueDate);
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual(healthAppointmentID, (Guid)fields["regardingid"]);
            Assert.AreEqual("Mr Pavel MCNamara, Assessment, Clients or patients home", (string)fields["regardingidname"]);
            Assert.AreEqual("healthappointment", (string)fields["regardingidtablename"]);
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
