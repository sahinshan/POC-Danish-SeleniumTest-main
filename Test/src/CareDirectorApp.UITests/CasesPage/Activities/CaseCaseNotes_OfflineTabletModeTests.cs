using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.Cases
{
    /// <summary>
    /// This class contains all test methods for cases case notes validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class CaseCaseNotes_OfflineTabletModeTests : TestBase
    {
        static UIHelper uIHelper;

        #region properties

        string LayoutXml_NoWidgets = "";
        string LayoutXml_OneWidgetNotConfigured = "<Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidget_GridView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-Indigo</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidgetListView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_TwoWidgets_GridView_ListView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Case Note (For Case) - Active Records</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>ae1c53f3-3010-e911-80dc-0050560502cc</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>          <BusinessObjectName>casecasenote</BusinessObjectName>          <HeaderColour>bg-Green</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidget_SharedUserView = "  <Layout  >    <Widgets>      <widget>        <Title>Task - Tasks created in the last 30 days</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>65ffa115-4890-ea11-a2cd-005056926fe4</DataViewId>          <DataViewType>shared</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightBlue</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_AllWidgets = "  <Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>IFrame</Type>          <Url>www.google.com</Url>          <HeaderColour>bg-Pink</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Task - My Active Tasks</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightBlue</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>6</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>Chart</Type>          <BusinessObjectId>30f84b2d-b169-e411-bf00-005056c00008</BusinessObjectId>          <BusinessObjectName>case</BusinessObjectName>          <ChartId>5b540885-d816-ea11-a2c8-005056926fe4</ChartId>          <ChartGroup>System</ChartGroup>          <HeaderColour>bg-Yellow</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Case Note (For Case) - Active Records</Title>        <X>0</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>ae1c53f3-3010-e911-80dc-0050560502cc</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>          <BusinessObjectName>casecasenote</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>3</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>WidgetCatalogue</Type>          <HTMLWebResource>Html_ClinicAvailableSlotsSearch.html</HTMLWebResource>          <HeaderColour>bg-Red</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>6</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>Report</Type>          <HeaderColour>bg-Purple</HeaderColour>          <HideHeader>false</HideHeader>          <ReportId>dc3d3d9f-1765-ea11-a2cb-005056926fe4</ReportId>        </Settings>      </widget>    </Widgets>  </Layout>";


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

        #region cases Case Notes page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6401")]
        [Description("UI Test for Case case notes (offline mode) - 0002 - " +
            "Navigate to the cases Case Notes area (case contains case note records) - Validate the page content")]
        public void CaseCaseNotes_OfflineTestMethod02()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid caseNoteID = new Guid("cac16b15-419a-ec11-a334-005056926fe4");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .ValidateSubjectCellText("Case Note 001", caseNoteID.ToString())
                .ValidateCreatedOnCellText("02/03/2022 15:54", caseNoteID.ToString())
                .ValidateResponsibleUserCellText("Mobile Test User 1", caseNoteID.ToString())
                .ValidateCreatedByCellText("CW Forms Test User 1", caseNoteID.ToString())
                .ValidateModifiedByText("CW Forms Test User 1", caseNoteID.ToString())
                .ValidateModifiedOnCellText("02/03/2022 16:22", caseNoteID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6402")]
        [Description("UI Test for Case case notes (offline mode) - 0004 - " +
            "Navigate to the cases Case Notes area - Open a cases case note record - Validate that the case note record page fields and titles are displayed")]
        public void CaseCaseNotes_OfflineTestMethod04()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string CaseNoteSubject = "Case Note 001";

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnRecord(CaseNoteSubject);

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
                .ValidateSubjectFieldTitleVisible(true)
                .ValidateDescriptionFieldTitleVisible(true)
                .ValidateReasonFieldTitleVisible(true)
                .ValidatePriorityFieldTitleVisible(true)
                .ValidateDateFieldTitleVisible(true)
                .ValidateOutcomeFieldTitleVisible(true)
                .ValidateStatusFieldTitleVisible(true)
                .ValidateCategoryFieldTitleVisible(true)
                .ValidateSubCategoryFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true)

                .ValidateSubjectFieldText(CaseNoteSubject)
                .ValidateDescriptionRichFieldText("Case Note 001 description") //for this record the description is displayed as rich text
                .ValidateReasonFieldText("Other")
                .ValidatePriorityFieldText("Low")
                .ValidateDateFieldText("20/05/2020", "07:00")
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
        [Property("JiraIssueID", "CDV6-6403")]
        [Description("UI Test for Case case notes (offline mode) - 0010 - " +
            "Navigate to the cases Case Notes area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - Validate that the data is sync to the web platform")]
        public void CaseCaseNotes_OfflineTestMethod10()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InsertSubject("Case Note 001")
                .InsertDescription("Case Note 001 description")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Assessment");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .TapPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Normal");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("20/05/2020", "")
                .TapOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("More information needed");

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
               .TapCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Advice");

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
               .TapSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Home Support");

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
               .TapResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Mobile Test User 1");

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
               .TapOnSaveAndCloseButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnRecord("Case Note 001");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
                .ValidateSubjectFieldText("Case Note 001")
                .ValidateDescriptionFieldText("Case Note 001 description")
                .ValidateReasonFieldText("Assessment")
                .ValidatePriorityFieldText("Normal")
                .ValidateDateFieldText("20/05/2020", "00:00")
                .ValidateOutcomeFieldText("More information needed")
                .ValidateStatusFieldText("Open")
                .ValidateCategoryFieldText("Advice")
                .ValidateSubCategoryFieldText("Home Support")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateResponsibleUserFieldText("Mobile Test User 1");


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            var caseNotes = this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID);
            Assert.AreEqual(1, caseNotes.Count);
            var fields = this.PlatformServicesHelper.caseCaseNote.GetCaseCaseNoteByID(caseNotes[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "casenotedate", "statusid");

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
            Assert.AreEqual("Case Note 001", (string)fields["subject"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual("<div>Case Note 001 description</div>", (string)fields["notes"]);
            Assert.AreEqual(activitycategoryid, (Guid)fields["activitycategoryid"]);
            Assert.AreEqual(activitysubcategoryid, (Guid)fields["activitysubcategoryid"]);
            Assert.AreEqual(activityoutcomeid, (Guid)fields["activityoutcomeid"]);
            Assert.AreEqual(activityreasonid, (Guid)fields["activityreasonid"]);
            Assert.AreEqual(activitypriorityid, (Guid)fields["activitypriorityid"]);
            Assert.AreEqual(false, (bool)fields["informationbythirdparty"]);
            Assert.AreEqual(false, (bool)fields["issignificantevent"]);
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(new DateTime(2020, 5, 20, 0, 0, 0), ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(1, (int)fields["statusid"]);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6404")]
        [Description("UI Test for Case case notes (offline mode) - 0013 - " +
            "Navigate to the cases Case Notes area - Tap on the add button - Set data only in mandatory fields except for Subject- " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void CaseCaseNotes_OfflineTestMethod13()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                //.InsertSubject("Case Note 001")
                .InserDate("20/05/2020", "09:00")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Subject' is required")
                .TapOnOKButton();

            mainMenu.NavigateToSettingsPage();

            warningPopup.WaitForWarningPopupToLoad().TapOnYesButton();

            settingsPage.SetTheAppInOnlineMode();

            var caseNotes = this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID);
            Assert.AreEqual(0, caseNotes.Count);

        }


        #endregion

        #region Update Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6405")]
        [Description("UI Test for Case case notes (offline mode) - 0017 - Create a new cases case note using the main APP web services" +
            "Navigate to the cases Case Notes area - open the case note record - update all fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void CaseCaseNotes_OfflineTestMethod17()
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

            var datefield1 = date.ToString("dd'/'MM'/'yyyy HH:mm");

           
            // string CancelledDate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");
            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);

            Guid caseCaseNoteID = this.PlatformServicesHelper.caseCaseNote.CreateCaseCaseNote("Case Note 001", "Case Note 001 description", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnRecord("Case Note 001");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
                .InsertSubject("Case Note 001 updated")
                .InsertDescription("Case Note 001 description updated")
                .InserDate("21/05/2020", "")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("First").TapSearchButtonQuery().TapOnRecord("First Response");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
                .TapPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("High");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
                .TapOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Completed");

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
               .TapCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Assessment");

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
               .TapSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Health Assessment");

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
               .TapResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Mobile Test User 2");

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
               .TapOnSaveAndCloseButton();
            
            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            
            Guid mobile_test_user_2UserID = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4"); //mobile_test_user_2
            Guid updated_activitycategoryid = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"); //Assessment
            Guid updated_activitysubcategoryid = new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"); //Health Assessment
            Guid updated_activityoutcomeid = new Guid("4C2BEC1C-9E45-E911-A2C5-005056926FE4"); // Completed
            Guid updated_activityreasonid = new Guid("B9EC74E3-9C45-E911-A2C5-005056926FE4"); //First response
            Guid updated_activitypriorityid = new Guid("1E164C51-9D45-E911-A2C5-005056926FE4"); //High

            var caseNotes = this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID);
            Assert.AreEqual(1, caseNotes.Count);
          
            var fields = this.PlatformServicesHelper.caseCaseNote.GetCaseCaseNoteByID(caseNotes[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "caseID", "casenotedate", "statusid");
           
            var datefield = this.PlatformServicesHelper.caseCaseNote.GetCaseCaseNoteByID(caseNotes[0], "casenotedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["casenotedate"]);
            string casenotedate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");

            DateTime date1 = new DateTime(2020, 5, 21, 0, 0, 0);

            var Expecteddatefield1 = date1.ToString("dd'/'MM'/'yyyy HH:mm");

            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(mobile_test_user_2UserID, (Guid)fields["responsibleuserid"]);
            Assert.AreEqual(businessUnitID, (Guid)fields["owningbusinessunitid"]);
            Assert.AreEqual("Case Note 001 updated", (string)fields["subject"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual("<div>Case Note 001 description updated</div>", (string)fields["notes"]);
            Assert.AreEqual(updated_activitycategoryid, (Guid)fields["activitycategoryid"]);
            Assert.AreEqual(updated_activitysubcategoryid, (Guid)fields["activitysubcategoryid"]);
            Assert.AreEqual(updated_activityoutcomeid, (Guid)fields["activityoutcomeid"]);
            Assert.AreEqual(updated_activityreasonid, (Guid)fields["activityreasonid"]);
            Assert.AreEqual(updated_activitypriorityid, (Guid)fields["activitypriorityid"]);
            Assert.AreEqual(false, (bool)fields["informationbythirdparty"]);
            Assert.AreEqual(false, (bool)fields["issignificantevent"]);
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            // Assert.AreEqual(new DateTime(2020, 5, 21, 0, 0, 0), casenotedate);
             Assert.AreEqual(Expecteddatefield1, casenotedate);

            Assert.AreEqual(1, (int)fields["statusid"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6406")]
        [Description("UI Test for Case case notes (offline mode) - 0018 - Create a new cases case note using the main APP web services" +
            "Navigate to the cases Case Notes area - open the case note record - Set the status to Completed - Save the record - Validate that the record gets deactivated")]
        public void CaseCaseNotes_OfflineTestMethod18()
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


            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);

            Guid caseCaseNoteID = this.PlatformServicesHelper.caseCaseNote.CreateCaseCaseNote("Case Note 001", "Case Note 001 description", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnRecord("Case Note 001");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
                .TapStatusPicklist();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(1)
                .TapOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
                .TapOnSaveAndCloseButton();

            caseCaseNotesPage
               .WaitForCaseCaseNotesPageToLoad()
               .ValidateNoRecordsMessageVisibility(false);

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            var fields = this.PlatformServicesHelper.caseCaseNote.GetCaseCaseNoteByID(caseCaseNoteID,"inactive");
            Assert.AreEqual(true, (bool)fields["inactive"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6407")]
        [Description("UI Test for Case case notes (offline mode) - 0021 - Create a new cases case note using the main APP web services" +
            "Navigate to the cases Case Notes area - open the case note record - Set the status to Canceled - Save the record - Validate that the record is still editable (only after going online the record get deactivated)")]
        public void CaseCaseNotes_OfflineTestMethod21()
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


            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);

            Guid caseCaseNoteID = this.PlatformServicesHelper.caseCaseNote.CreateCaseCaseNote("Case Note 001", "Case Note 001 description", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnRecord("Case Note 001");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
                .TapStatusPicklist();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(2)
                .TapOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
                .TapOnSaveAndCloseButton();

            caseCaseNotesPage
               .WaitForCaseCaseNotesPageToLoad()
               .TapOnRecord("Case Note 001");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
                .ValidateReasonRemoveButtonVisible(true)
                .ValidatePriorityRemoveButtonVisible(true)
                .ValidateOutcomeRemoveButtonVisible(true)
                .ValidateCategoryRemoveButtonVisible(true)
                .ValidateSubCategoryRemoveButtonVisible(true)
                .ValidateResponsibleUserRemoveButtonVisible(true)
                .ValidateReasonLookupButtonVisible(true)
                .ValidatePriorityLookupButtonVisible(true)
                .ValidateOutcomeLookupButtonVisible(true)
                .ValidateCategoryLookupButtonVisible(true)
                .ValidateSubCategoryLookupButtonVisible(true)
                .ValidateResponsibleUserLookupButtonVisible(true);

        }

        #endregion

        #region Delete record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6408")]
        [Description("UI Test for Case case notes (offline mode) - 0020 - Create a new cases case note using the main APP web services" +
            "Navigate to the cases Case Notes area - open the case note record - Tap on the delete button - " +
            "Validate that the record is deleted from the web app database")]
        public void CaseCaseNotes_OfflineTestMethod20()
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


            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);

            Guid caseCaseNoteID = this.PlatformServicesHelper.caseCaseNote.CreateCaseCaseNote("Case Note 001", "Case Note 001 description", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnRecord("Case Note 001");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Case Note 001")
                .TapOnDeleteButton();

            warningPopup
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?")
                .TapOnYesButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            var records = this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID);
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
