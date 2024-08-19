using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;
//using Microsoft.Kiota.Abstractions;
//using Microsoft.Graph.Models;

namespace CareDirectorApp.UITests.Cases
{
    /// <summary>
    /// This class contains all test methods for cases Tasks validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class CaseTasks_OfflineTabletModeTests : TestBase
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

        #region cases Tasks page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6430")]
        [Description("UI Test for Case Tasks (Offline Mode) - 0002 - " +
            "Navigate to the cases Tasks area (case contains Task records) - Validate the page content")]
        public void CaseTasks_OfflineTestMethod02()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid caseNoteID = new Guid("BAC2FBDA-639F-EA11-A2CD-005056926FE4");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();


            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            caseTasksPage
                .WaitForCaseTasksPageToLoad()
                .ValidateSubjectCellText("Task 001", caseNoteID.ToString())
                .ValidateDueCellText("20/05/2020 07:00", caseNoteID.ToString())
                .ValidateResponsibleTeamCellText("Mobile Team 1", caseNoteID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", caseNoteID.ToString())
                .ValidateCreatedOnCellText("26/05/2020 16:16", caseNoteID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", caseNoteID.ToString())
                .ValidateModifiedOnCellText("26/05/2020 16:16", caseNoteID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6431")]
        [Description("UI Test for Case Tasks (Offline Mode) - 0004 - " +
            "Navigate to the cases Tasks area - Open a cases Task record - Validate that the Task record page fields and titles are displayed")]
        public void CaseTasks_OfflineTestMethod04()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string TaskSubject = "Task 001";

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();


            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            caseTasksPage
                .WaitForCaseTasksPageToLoad()
                .TapOnRecord(TaskSubject);

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
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
        [Property("JiraIssueID", "CDV6-6432")]
        [Description("UI Test for Case Tasks (Offline Mode) - 0010 - " +
            "Navigate to the cases Tasks area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - Validate that the data is sync to the web platform")]
        public void CaseTasks_OfflineTestMethod10()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Task for the case
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByCaseID(caseID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();


            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

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
               .TapOnSaveAndCloseButton();

            caseTasksPage
                .WaitForCaseTasksPageToLoad()
                .TapOnRecord("Task 001");

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
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


            var caseNotes = this.PlatformServicesHelper.task.GetTaskByCaseID(caseID);
            Assert.AreEqual(1, caseNotes.Count);
            var fields = this.PlatformServicesHelper.task.GetTaskByID(caseNotes[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "personid", "duedate", "statusid", "regardingid", "regardingidname", "regardingidtablename");

            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
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
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(new DateTime(2020, 5, 20, 9, 0, 0), ((DateTime)fields["duedate"]).ToLocalTime());
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual(caseID, (Guid)fields["regardingid"]);
            Assert.AreEqual("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]", (string)fields["regardingidname"]);
            Assert.AreEqual("case", (string)fields["regardingidtablename"]);

        }


        #endregion

        #region Update Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6433")]
        [Description("UI Test for Case Tasks (Offline Mode) - 0015 - Create a new cases Task using the main APP web services" +
            "Navigate to the cases Tasks area - open the Task record - validate that all fields are correctly synced")]
        public void CaseTasks_OfflineTestMethod15()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176] 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal
            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);


            //remove any Task for the case
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByCaseID(caseID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            Guid caseTaskID = this.PlatformServicesHelper.task.CreateTask("Task 001", "Task 001 description", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, 
                activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date, caseID, "Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]", "case");

            
            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            caseTasksPage
                .WaitForCaseTasksPageToLoad()
                .TapOnRecord("Task 001");

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
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
        [Property("JiraIssueID", "CDV6-6434")]
        [Description("UI Test for Case Tasks (Offline Mode) - 0016 - Create a new cases Task using the main APP web services" +
            "Navigate to the cases Tasks area - open the Task record - clear all non mandatory fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void CaseTasks_OfflineTestMethod16()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176] 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid businessUnitID = new Guid("BA5B9FE6-419E-E911-A2C6-005056926FE4"); //Mobile Business Unit
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal
            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);

            var Expecteddatefield1 = date.ToString("dd'/'MM'/'yyyy HH:mm");

            //remove any Task for the case
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByCaseID(caseID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            Guid caseTaskID = this.PlatformServicesHelper.task.CreateTask("Task 001", "Task 001 description", mobileTeam1, mobile_test_user_1UserID, 
                activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date, caseID, "Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]", "case");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapTasksIcon_RelatedItems();

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


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            var caseNotes = this.PlatformServicesHelper.task.GetTaskByCaseID(caseID);
            Assert.AreEqual(1, caseNotes.Count);

            var fields = this.PlatformServicesHelper.task.GetTaskByID(caseNotes[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "personid", "duedate", "statusid", "regardingid", "regardingidname", "regardingidtablename");
            var datefield = this.PlatformServicesHelper.task.GetTaskByID(caseNotes[0], "duedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();
            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["duedate"]);
            string ActualDueDate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");
            DateTime ActualDate = new DateTime(2020, 5, 20, 9, 0, 0);

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
            Assert.AreEqual(Expecteddatefield1, ActualDueDate);
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual(caseID, (Guid)fields["regardingid"]);
            Assert.AreEqual("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]", (string)fields["regardingidname"]);
            Assert.AreEqual("case", (string)fields["regardingidtablename"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6435")]
        [Description("UI Test for Case Tasks (Offline Mode) - 0017 - Create a new cases Task using the main APP web services" +
            "Navigate to the cases Tasks area - open the Task record - update all fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void CaseTasks_OfflineTestMethod17()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid businessUnitID = new Guid("BA5B9FE6-419E-E911-A2C6-005056926FE4"); //Mobile Business Unit
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal
            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);

            var Expecteddatefield1 = date.ToString("dd'/'MM'/'yyyy HH:mm");

            //remove any Task for the case
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByCaseID(caseID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            Guid caseTaskID = this.PlatformServicesHelper.task.CreateTask("Task 001", "Task 001 description", mobileTeam1, mobile_test_user_1UserID, 
                activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date, caseID, "Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]", "case");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            caseTasksPage
                .WaitForCaseTasksPageToLoad()
                .TapOnRecord("Task 001");

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
                .InsertSubject("Task 001 updated")
                .InsertDescription("Task 001 description updated")
                .InsertDueDate("21/05/2020", "09:30")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("First").TapSearchButtonQuery().TapOnRecord("First Response");

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
                .TapPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("High");

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
                .TapOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Completed");

            caseTaskRecordPage
               .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
               .TapCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Assessment");

            caseTaskRecordPage
               .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
               .TapSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Health Assessment");

            caseTaskRecordPage
               .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
               .TapOnSaveAndCloseButton();

            caseTasksPage
                .WaitForCaseTasksPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            

            Guid updated_activitycategoryid = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"); //Assessment
            Guid updated_activitysubcategoryid = new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"); //Health Assessment
            Guid updated_activityoutcomeid = new Guid("4C2BEC1C-9E45-E911-A2C5-005056926FE4"); // Completed
            Guid updated_activityreasonid = new Guid("B9EC74E3-9C45-E911-A2C5-005056926FE4"); //First response
            Guid updated_activitypriorityid = new Guid("1E164C51-9D45-E911-A2C5-005056926FE4"); //High

            var caseNotes = this.PlatformServicesHelper.task.GetTaskByCaseID(caseID);
            Assert.AreEqual(1, caseNotes.Count);
            var fields = this.PlatformServicesHelper.task.GetTaskByID(caseNotes[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "personid", "duedate", "statusid", "regardingid", "regardingidname", "regardingidtablename");
            var datefield = this.PlatformServicesHelper.task.GetTaskByID(caseNotes[0], "duedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["duedate"]);
            string DueDate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");
            DateTime ExpectedDate = new DateTime(2020, 5, 21, 9, 30, 0);

            var Expecteddatefield = ExpectedDate.ToString("dd'/'MM'/'yyyy HH:mm");

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
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(Expecteddatefield, DueDate);
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual(caseID, (Guid)fields["regardingid"]);
            Assert.AreEqual("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]", (string)fields["regardingidname"]);
            Assert.AreEqual("case", (string)fields["regardingidtablename"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6436")]
        [Description("UI Test for Case Tasks (Offline Mode) - 0018 - Create a new cases Task using the main APP web services" +
            "Navigate to the cases Tasks area - open the Task record - Set the status to Completed - Save the record - Validate that the record gets deactivated")]
        public void CaseTasks_OfflineTestMethod18()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal
            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);


            //remove any Task for the case
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByCaseID(caseID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            Guid caseTaskID = this.PlatformServicesHelper.task.CreateTask("Task 001", "Task 001 description", mobileTeam1, mobile_test_user_1UserID, 
                activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date, caseID, "Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]", "case");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            caseTasksPage
                .WaitForCaseTasksPageToLoad()
                .TapOnRecord("Task 001");

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
                .TapStatusPicklist();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(1)
                .TapOKButton();

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
                .TapOnSaveAndCloseButton();

            caseTasksPage
               .WaitForCaseTasksPageToLoad()
               .ValidateNoRecordsMessageVisibility(false);


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            var fields = this.PlatformServicesHelper.task.GetTaskByID(caseTaskID,"inactive");
            Assert.AreEqual(true, (bool)fields["inactive"]);
        }



        #endregion

        #region Delete record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6437")]
        [Description("UI Test for Case Tasks (Offline Mode) - 0020 - Create a new cases Task using the main APP web services" +
            "Navigate to the cases Tasks area - open the Task record - Tap on the delete button - " +
            "Validate that the record is deleted from the web app database")]
        public void CaseTasks_OfflineTestMethod20()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal
            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);


            //remove any Task for the case
            foreach (Guid recordID in this.PlatformServicesHelper.task.GetTaskByCaseID(caseID))
                this.PlatformServicesHelper.task.DeleteTask(recordID);

            Guid caseTaskID = this.PlatformServicesHelper.task.CreateTask("Task 001", "Task 001 description", mobileTeam1, mobile_test_user_1UserID, 
                activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date, caseID, "Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]", "case");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapTasksIcon_RelatedItems();

            caseTasksPage
                .WaitForCaseTasksPageToLoad()
                .TapOnRecord("Task 001");

            caseTaskRecordPage
                .WaitForCaseTaskRecordPageToLoad("TASK: Task 001")
                .TapOnDeleteButton();

            warningPopup
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?")
                .TapOnYesButton();

            caseTasksPage
                .WaitForCaseTasksPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            var records = this.PlatformServicesHelper.task.GetTaskByCaseID(caseID);
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
