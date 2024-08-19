using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;
using System.Linq;
using System.Threading;

namespace CareDirectorApp.UITests.Cases
{
    /// <summary>
    /// This class contains all test methods for cases case notes validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class CaseCaseNotes_BusinessRules_TabletModeTests : TestBase
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

            //if the cases case note injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the cases case note review pop-up is open then close it 
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

        #region https://advancedcsg.atlassian.net/browse/CDV6-11975


        #region 1 - Action Testing Business Rules (1)

        [Test]
        [Property("JiraIssueID", "CDV6-12003")]
        [Description("Testing Hide Section action and Show Section action")]
        public void CaseCaseNotes_BusinessRules_TestMethod001()
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

            PlatformServicesHelper.caseCaseNote.CreateCaseCaseNote("Testing CDV6-11975 - Hide Section", "description...", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date);

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
                .TapOnRecord("Testing CDV6-11975 - Hide Section");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Hide Section")

                //validate General section titles visible
                .ValidateGeneralSectionTitleVisible(true)
                .ValidateSubjectFieldTitleVisible(true)
                .ValidateDescriptionFieldTitleVisible(true)

                //validate Details section titles NOT visible
                .ValidateDetailsSectionTitleVisible(false)
                .ValidateReasonFieldTitleVisible(false)
                .ValidatePriorityFieldTitleVisible(false)
                .ValidateDateFieldTitleVisible(false)
                .ValidateOutcomeFieldTitleVisible(false)
                .ValidateStatusFieldTitleVisible(false)
                .ValidateCategoryFieldTitleVisible(false)
                .ValidateSubCategoryFieldTitleVisible(false)
                .ValidateResponsibleTeamFieldTitleVisible(false)
                .ValidateResponsibleUserFieldTitleVisible(false)

                //validate General section fields visible
                .ValidateSubjectFieldVisible(true)
                .ValidateDescriptionFieldVisible(true)

                //validate Details section fields NOT visible
                .ValidateReasonFieldVisible(false)
                .ValidatePriorityFieldVisible(false)
                .ValidateDateFieldVisible(false)
                .ValidateOutcomeFieldVisible(false)
                .ValidateStatusFieldVisible(false)
                .ValidateCategoryFieldVisible(false)
                .ValidateSubCategoryFieldVisible(false)
                .ValidateResponsibleTeamFieldVisible(false)
                .ValidateResponsibleUserFieldVisible(false)

                .InsertSubject("Testing CDV6-11975 - Show Section")
                .TapOnSaveButton()
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Show Section")

                //validate General section titles visible
                .ValidateGeneralSectionTitleVisible(true)
                .ValidateSubjectFieldTitleVisible(true)
                .ValidateDescriptionFieldTitleVisible(true)

                //validate Details section titles NOT visible
                .ValidateDetailsSectionTitleVisible(true)
                .ValidateReasonFieldTitleVisible(true)
                .ValidatePriorityFieldTitleVisible(true)
                .ValidateDateFieldTitleVisible(true)
                .ValidateOutcomeFieldTitleVisible(true)
                .ValidateStatusFieldTitleVisible(true)
                .ValidateCategoryFieldTitleVisible(true)
                .ValidateSubCategoryFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true)

                //validate General section fields visible
                .ValidateSubjectFieldVisible(true)
                .ValidateDescriptionFieldVisible(true)

                //validate Details section fields NOT visible
                .ValidateReasonFieldVisible(true)
                .ValidatePriorityFieldVisible(true)
                .ValidateDateFieldVisible(true)
                .ValidateOutcomeFieldVisible(true)
                .ValidateStatusFieldVisible(true)
                .ValidateCategoryFieldVisible(true)
                .ValidateSubCategoryFieldVisible(true)
                .ValidateResponsibleTeamFieldVisible(true)
                .ValidateResponsibleUserFieldVisible(true);

        }

        [Test]
        [Property("JiraIssueID", "CDV6-12004")]
        [Description("Testing Hide Field action and Show Field action")]
        public void CaseCaseNotes_BusinessRules_TestMethod002()
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

            PlatformServicesHelper.caseCaseNote.CreateCaseCaseNote("Testing CDV6-11975 - Hide Field", "description...", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date);

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
                .TapOnRecord("Testing CDV6-11975 - Hide Field");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Hide Field")

                //validate General section titles visible
                .ValidateGeneralSectionTitleVisible(true)
                .ValidateSubjectFieldTitleVisible(true)
                .ValidateDescriptionFieldTitleVisible(false);

            //validate Details section titles NOT visible
            caseCaseNoteRecordPage
                .ValidateDetailsSectionTitleVisible(true)
                .ValidateReasonFieldTitleVisible(false)
                .ValidatePriorityFieldTitleVisible(true)
                .ValidateDateFieldTitleVisible(false)
                .ValidateOutcomeFieldTitleVisible(true)
                .ValidateStatusFieldTitleVisible(false)
                .ValidateCategoryFieldTitleVisible(true)
                .ValidateSubCategoryFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true);

            //validate General section fields visible
            caseCaseNoteRecordPage
                .ValidateSubjectFieldVisible(true)
                .ValidateDescriptionFieldVisible(false);

            //validate Details section fields NOT visible
            caseCaseNoteRecordPage
                .ValidateReasonFieldVisible(false)
                .ValidatePriorityFieldVisible(true)
                .ValidateDateFieldVisible(false)
                .ValidateOutcomeFieldVisible(true)
                .ValidateStatusFieldVisible(false)
                .ValidateCategoryFieldVisible(true)
                .ValidateSubCategoryFieldVisible(true)
                .ValidateResponsibleTeamFieldVisible(true)
                .ValidateResponsibleUserFieldVisible(true)

                .InsertSubject("Testing CDV6-11975 - Show Field")
                .TapOnSaveButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Show Field")

                //validate General section titles visible
                .ValidateGeneralSectionTitleVisible(true)
                .ValidateSubjectFieldTitleVisible(true)
                .ValidateDescriptionFieldTitleVisible(true);

            //validate Details section titles NOT visible
            caseCaseNoteRecordPage
                .ValidateDetailsSectionTitleVisible(true)
                .ValidateReasonFieldTitleVisible(true)
                .ValidatePriorityFieldTitleVisible(true)
                .ValidateDateFieldTitleVisible(true)
                .ValidateOutcomeFieldTitleVisible(true)
                .ValidateStatusFieldTitleVisible(true)
                .ValidateCategoryFieldTitleVisible(true)
                .ValidateSubCategoryFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true);

            //validate General section fields visible
            caseCaseNoteRecordPage
                .ValidateSubjectFieldVisible(true)
                .ValidateDescriptionFieldVisible(true);

            //validate Details section fields NOT visible
            caseCaseNoteRecordPage
                .ValidateReasonFieldVisible(true)
                .ValidatePriorityFieldVisible(true)
                .ValidateDateFieldVisible(true)
                .ValidateOutcomeFieldVisible(true)
                .ValidateStatusFieldVisible(true)
                .ValidateCategoryFieldVisible(true)
                .ValidateSubCategoryFieldVisible(true)
                .ValidateResponsibleTeamFieldVisible(true)
                .ValidateResponsibleUserFieldVisible(true);


        }

        [Test]
        [Property("JiraIssueID", "CDV6-12006")]
        [Description("Testing Set Field Mandatory action and Set Field Option action")]
        public void CaseCaseNotes_BusinessRules_TestMethod003()
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

            var caseNoteId = PlatformServicesHelper.caseCaseNote.CreateCaseCaseNote("Testing CDV6-11975 - Set Field Mandatory", null, mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date);

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
                .TapOnRecord("Testing CDV6-11975 - Set Field Mandatory");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Set Field Mandatory")
                .InserDate("11/08/2021", "")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Description' is required").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Set Field Mandatory")
                .TapOnBackButton();

            warningPopup.WaitForWarningPopupToLoad().TapOnYesButton();

            PlatformServicesHelper.caseCaseNote.UpdateSubject(caseNoteId, "Testing CDV6-11975 - Set Field Optional");

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapSearchButton()
                .TapOnRecord("Testing CDV6-11975 - Set Field Optional");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Set Field Optional")
                .InserDate("11/08/2021", "")
                .TapOnSaveButton() //this time the description field should be optional
                .TapOnBackButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-12007")]
        [Description("Testing Set Field Readonly action and Set Field Writable action")]
        public void CaseCaseNotes_BusinessRules_TestMethod004()
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

            var caseNoteId = PlatformServicesHelper.caseCaseNote.CreateCaseCaseNote("Testing CDV6-11975", null, mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date);

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
                .TapOnRecord("Testing CDV6-11975");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975");
                Thread.Sleep(1000);

            caseCaseNoteRecordPage
                .ValidateDescriptionFieldEditable(true)
                .ValidateReasonFieldEditable(true)
                .ValidateDateFieldEditable(true)
                .ValidateStatusFieldEditable(true);

            caseCaseNoteRecordPage
                .InsertSubject("Testing CDV6-11975 - Set Field Read only")
                .TapOnSaveButton()
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Set Field Read only");

            caseCaseNoteRecordPage
                .ValidateDescriptionFieldEditable(false)
                .ValidateReasonFieldEditable(false)
                .ValidateDateFieldEditable(false)
                .ValidateStatusFieldEditable(false);

            caseCaseNoteRecordPage
                .InsertSubject("Testing CDV6-11975 - Set Field Writable")
                .TapOnSaveButton()
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Set Field Writable");

            caseCaseNoteRecordPage
                .ValidateDescriptionFieldEditable(true)
                .ValidateReasonFieldEditable(true)
                .ValidateDateFieldEditable(true)
                .ValidateStatusFieldEditable(true);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-12008")]
        [Description("Testing Empty Field action and Set Field Value action")]
        public void CaseCaseNotes_BusinessRules_TestMethod005()
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

            var caseNoteId = PlatformServicesHelper.caseCaseNote.CreateCaseCaseNote("Testing CDV6-11975 - Empty Field", "Description", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date);

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
                .TapOnRecord("Testing CDV6-11975 - Empty Field");
                 Thread.Sleep(1000);

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Empty Field")
                .ValidateSubjectFieldText("")
                .ValidateDescriptionFieldText("")
                .ValidateReasonFieldText("")
                .ValidateDateFieldText("", "")
                .ValidateStatusFieldText(" ")

                .InsertSubject("Testing CDV6-11975 - Set Field Values Directly")
                .InserDate("05/11/2021", "")
                .TapStatusPicklist();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Empty Field")
                .TapOnSaveButton()
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Set Field Values Directly")

                .ValidateSubjectFieldText("CDV6-11975 - Set Field Rule Activated")
                .ValidateDescriptionFieldText("Line 1\nLine 2")
                .ValidateReasonFieldText("Undertake Diagnostic Tests")
                .ValidateDateFieldText("02/08/2021", "04:30")
                .ValidateStatusFieldText("Cancelled");
        }

        [Test]
        [Property("JiraIssueID", "")]
        [Description("Testing Set Field Value action (copying from another field)")]
        public void CaseCaseNotes_BusinessRules_TestMethod006()
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

            var caseNoteId = PlatformServicesHelper.caseCaseNote.CreateCaseCaseNote("Testing CDV6-11975 - Set Field to Field", "Description", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date);
            PlatformServicesHelper.caseCaseNote.UpdateLegacyId(caseNoteId, "casenoteid1234567");

            var fields = PlatformServicesHelper.caseCaseNote.GetCaseCaseNoteByID(caseNoteId, "createdon");
            var createdOnDate = (DateTime)fields["createdon"];

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
                .TapOnRecord("Testing CDV6-11975 - Set Field to Field");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Set Field to Field")
                .ValidateSubjectFieldText("casenoteid1234567")
                .ValidateDateFieldText(createdOnDate.ToLocalTime().ToString("dd/MM/yyyy"), createdOnDate.ToLocalTime().ToString("HH:mm"));
        }

        [Test]
        [Property("JiraIssueID", "CDV6-12011")]
        [Description("Testing the Stop Rule action")]
        public void CaseCaseNotes_BusinessRules_TestMethod007()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InsertSubject("Testing CDV6-11975 - Stop Rule")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Undertake*").TapSearchButtonQuery().TapOnRecord("Undertake Diagnostic Tests");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("09/08/2021", "")
                .TapResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("mobile*").TapSearchButtonQuery().TapOnRecord("Mobile Test User 2");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .TapOnSaveButton()
                .TapOnBackButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();
        }

        #endregion

        #region 2 - Action Testing Business Rules (2)

        [Test]
        [Property("JiraIssueID", "CDV6-12014")]
        [Description("Testing the Cancel Save action")]
        public void CaseCaseNotes_BusinessRules_TestMethod008()
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

            var caseNoteId = PlatformServicesHelper.caseCaseNote.CreateCaseCaseNote("Testing CDV6-11975 - Cancel Save", "Description", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date);

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
                .TapOnRecord("Testing CDV6-11975 - Cancel Save");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Cancel Save")
                .InserDate("11/08/2021", "")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Testing CDV6-11975 - Cancel Save activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Cancel Save")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Testing CDV6-11975 - Cancel Save activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-11975 - Cancel Save");
        }

        #endregion

        #region 3 - Condition Testing Business Rules (If Else Structures)

        [Test]
        [Property("JiraIssueID", "CDV6-12015")]
        [Description("Testing if else structures - scenario 1")]
        public void CaseCaseNotes_BusinessRules_TestMethod009()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InsertSubject("Conditions Testing CDV6-11975 - Scenario 1")
                .InserDate("04/08/2021", "")
                .TapStatusPicklist();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Testing CDV6-11975 - Scenario 1 activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .TapOnBackButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-12016")]
        [Description("Testing if else structures - scenario 2")]
        public void CaseCaseNotes_BusinessRules_TestMethod010()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InsertSubject("Conditions Testing CDV6-11975 - Scenario 2 (1)")
                .InserDate("03/08/2021", "")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Testing CDV6-11975 - Scenario 2 (1) activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Conditions Testing CDV6-11975 - Scenario 2 (1)")
                .InsertSubject("Conditions Testing CDV6-11975 - Scenario 2 (2)")
                .InserDate("06/08/2021", "")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Testing CDV6-11975 - Scenario 2 (2) activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .TapOnBackButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-12017")]
        [Description("Testing if else structures - scenario 3")]
        public void CaseCaseNotes_BusinessRules_TestMethod011()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InsertSubject("Conditions Testing CDV6-11975 - Scenario 3")
                .InsertDescription("desc ...")
                .InserDate("05/11/2021", "")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Testing CDV6-11975 - Scenario 3 - description contains data").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Conditions Testing CDV6-11975 - Scenario 3")
                .InsertDescription("")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Testing CDV6-11975 - Scenario 3 - description does not contains data").TapOnOKButton();

            caseCaseNoteRecordPage
                .TapOnBackButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-12100")]
        [Description("Testing if else structures - scenario 5")]
        public void CaseCaseNotes_BusinessRules_Scenario5_TestMethod012()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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

            caseCaseNoteRecordPage.WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")

               .InsertSubject("Conditions Popup Testing CDV6-11975 - Scenario 5").TapOnSaveButton();
            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 5 activated").TapOnOKButton();

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("05/11/2021", "").TapOnSaveButton()
               .InsertSubject("Conditions Popup Testing CDV6-11975 - Scenario 5").TapOnSaveButton();
            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 5 activated").TapOnOKButton();
            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 5 activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Conditions Popup Testing CDV6-11975 - Scenario 5")
                .TapOnBackButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-12052")]
        [Description("Testing if else structures - scenario 6")]
        public void CaseCaseNotes_BusinessRules_Scenario6_TestMethod012()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
                                                                              // Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
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

            var significanteventcategoryid = PlatformServicesHelper.SignificantEventCategory.GetByName("Category 1").FirstOrDefault();

            var caseNoteId = PlatformServicesHelper.caseCaseNote.CreateCaseCaseNote("Testing CDV6-12052 - Set Field Mandatory", null, mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, caseID, personID, date, significanteventcategoryid, date);


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
                .TapOnRecord("Testing CDV6-12052 - Set Field Mandatory");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Testing CDV6-12052 - Set Field Mandatory")
                .InsertSubject("Conditions Popup Testing CDV6-11975 - Scenario 6")

            .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 6 activated").TapOnOKButton();


        }

        [Test]
        [Property("JiraIssueID", "CDV6-12039")]
        [Description("Testing if else structures - Condition _ Operators _ Picklist")]
        public void CaseCaseNotes_BusinessRules_PickList_TestMethod010()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InserDate("03/08/2021", "")
                .TapStatusPicklist();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(0).TapOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 3 (1)")
                .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 3 (1) acticated").TapOnOKButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
                 .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                 .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 3 (2)")
                 .InserDate("03/08/2021", "")
                 .TapStatusPicklist();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 3 (2) acticated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .TapOnSaveAndCloseButton();


            caseCaseNotesPage
                  .WaitForCaseCaseNotesPageToLoad()
                  .TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
                 .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                 .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 3 (3)")
                 .InserDate("03/08/2021", "")
                 .TapStatusPicklist();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 3 (3) acticated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .TapOnSaveAndCloseButton();

            caseCaseNotesPage
                 .WaitForCaseCaseNotesPageToLoad()
                 .TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
                 .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                 .InserDate("03/08/2021", "")
                 .TapStatusPicklist();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(0).TapOKButton();

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
               .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 3 (4)")
               .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 3 (4) acticated").TapOnOKButton();


            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 3 (5)")
                .InserDate("03/08/2021", "")
                .TapStatusPicklist();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 3 (5) acticated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .TapOnSaveAndCloseButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                 .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                 .InserDate("03/08/2021", "")
                 .TapStatusPicklist();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(0).TapOKButton();

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
               .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 3 (6)")
               .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 3 (6) acticated").TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-12038")]
        [Description("Testing if else structures - Condition _ Operators _ Lookup")]
        public void CaseCaseNotes_BusinessRules_Lookup_TestMethod010()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 2 (1)")
                .InserDate("03/08/2021", "")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Assess*").TapSearchButtonQuery().TapOnRecord("Assessment");


            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 2 (1) activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .TapOnSaveAndCloseButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
               .InserDate("03/08/2021", "")
               .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 2 (2)")
               .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 2 (2) activated").TapOnOKButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 2 (2) activated").TapOnOKButton();

            caseCaseNoteRecordPage
             .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Condition Operators Testing CDV6-11975 - Scenario 2 (2)")
             .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("7 day*").TapSearchButtonQuery().TapOnRecord("7 day follow up");

            caseCaseNoteRecordPage
             .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Condition Operators Testing CDV6-11975 - Scenario 2 (2)")
             .TapOnSaveAndCloseButton();



            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
             .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
             .InserDate("03/08/2021", "")
             .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 2 (3)")
             .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 2 (3) activated").TapOnOKButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 2 (3) activated").TapOnOKButton();

            caseCaseNoteRecordPage
             .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Condition Operators Testing CDV6-11975 - Scenario 2 (3)")
             .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("7 day*").TapSearchButtonQuery().TapOnRecord("7 day follow up");
            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 2 (3) activated").TapOnOKButton();


            caseCaseNoteRecordPage
             .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Condition Operators Testing CDV6-11975 - Scenario 2 (3)")
             .TapOnSaveAndCloseButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
               .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 2 (4)")
               .InserDate("03/08/2021", "")
               .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Assess*").TapSearchButtonQuery().TapOnRecord("Assessment");

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 2 (4) activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .TapOnSaveAndCloseButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
               .InserDate("03/08/2021", "")
               .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 2 (5)")
               .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 2 (5) activated").TapOnOKButton();
            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 2 (5) activated").TapOnOKButton();

            caseCaseNoteRecordPage
             .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Condition Operators Testing CDV6-11975 - Scenario 2 (5)")
             .TapOnSaveAndCloseButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
               .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 2 (7)")
               .InserDate("03/08/2021", "")
               .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Assess*").TapSearchButtonQuery().TapOnRecord("Assessment");

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 2 (7) activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .TapOnSaveAndCloseButton();

            caseCaseNotesPage
               .WaitForCaseCaseNotesPageToLoad()
               .TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
               .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
               .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 2 (8)")
               .InserDate("03/08/2021", "")
               .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Assess*").TapSearchButtonQuery().TapOnRecord("Assessment");

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 2 (8) activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .TapOnSaveAndCloseButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-12037")]
        [Description("Testing if else structures - Condition _ Operators _ Lookup")]
        public void CaseCaseNotes_BusinessRules_ConditionOperatorsMultiLine_TestMethod010()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InserDate("03/08/2021", "")
                .InsertDescription("Description1")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 1")
                .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 1 - Contains Data").TapOnOKButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("03/08/2021", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 1")
                .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Condition Operators Testing CDV6-11975 - Scenario 1 - Does not contains data").TapOnOKButton();


        }

        [Test]
        [Property("JiraIssueID", "CDV6-12042")]
        [Description("Testing if else structures - Condition _ Operators _ Lookup")]
        public void CaseCaseNotes_BusinessRules_ConditionOperatorsSingleLine_TestMethod010()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline


            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


            casesPage.WaitForCasesPageToLoad().TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());


            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("11/08/1979", "")
                .InsertSubject("Subject1")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 4 (2) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("12/08/1979", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 4 (3)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 4 (3) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario")
                .InserDate("13/08/1979", "")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 4 (4) activated"
                )
                .TapOnOKButton();


            caseCaseNoteRecordPage.WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)");


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 4 (4) activated"
                )
                .TapOnOKButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .TapOnSaveAndCloseButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("14/08/1979", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 4 (5)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 4 (5) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 4 (5)")
                .InserDate("15/08/1979", "");


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 4 (5) activated"
                )
                .TapOnOKButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 4 (5) activated"
                )
                .TapOnOKButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                // .InserDate("15/08/1979", "")
                // .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 4 (5)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 4 (6) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("16/08/1979", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 4 (7)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 4 (7) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("17/08/1979", "")
                .InsertSubject("Description")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 4 (8) activated"
                )
                .TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-12043")]
        [Description("Testing if else structures - Condition _ Operators _ DateTime1")]
        public void CaseCaseNotes_BusinessRules_ConditionOperatorsDate_TestMethod010()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline


            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


            casesPage.WaitForCasesPageToLoad().TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());


            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("11/08/1979", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (2)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 5 (2) activated"
                )
                .TapOnOKButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 4 (2) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("09/08/2021", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (3)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 5 (3) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("10/08/2021", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (4)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 5 (4) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("02/08/2021", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (5)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 5 (5) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("01/08/2021", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (6)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 5 (6) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("06/08/2021", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (7)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 5 (7) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("06/08/2021", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (8)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 5 (8) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("01/08/2021", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (9)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 5 (9) activated"
                )
                .TapOnOKButton();


            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();


            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InserDate("02/08/2021", "")
                .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (10)")
                .TapOnSaveAndCloseButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage(
                    "Message",
                    "Condition Operators Testing CDV6-11975 - Scenario 5 (10) activated"
                )
                .TapOnOKButton();


        }

        [Test]
        [Property("JiraIssueID", "CDV6-12045")]
        [Description("Testing if else structures - Data Form Event _ Change Field")]
        public void CaseCaseNotes_BusinessRules_ConditionOperatorsDate_TestMethod011()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);

            casesPage.WaitForCasesPageToLoad().TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
              .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
              .TapRelatedItemsButton()
              .TapActivitiesArea_RelatedItems()
              .TapCaseNotesIcon_RelatedItems();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate("01/08/2021", "")
              .InsertSubject("Data Form Events Testing CDV6-11975 - Scenario 1")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Data Form Events Testing CDV6-11975 - Scenario 1 activated"
              )
              .TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-12054")]
        [Description("Testing if else structures - Condition _ Operators _ Date Time (2)")]
        public void CaseCaseNotes_BusinessRules_ConditionOperatorsDate_TestMethod012()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);

            casesPage.WaitForCasesPageToLoad().TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
              .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
              .TapRelatedItemsButton()
              .TapActivitiesArea_RelatedItems()
              .TapCaseNotesIcon_RelatedItems();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate("01/08/2030", "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (11)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (11) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate("01/08/2010", "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (12)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (12) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate("01/08/2040", "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (13)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (13) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate("09/04/2023", "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (15)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (15) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate("11/02/2023", "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (16)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (16) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate("14/04/2021", "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (17)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (17) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate("12/04/2023", "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (18)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (18) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate("12/04/2023", "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (19)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (19) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate("09/04/2025", "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (20)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (20) activated"
              )
              .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-12071")]
        [Description("Testing if else structures - Condition _ Operators _ Date Time (3)")]
        public void CaseCaseNotes_BusinessRules_ConditionOperatorsDate_TestMethod013()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);

            casesPage.WaitForCasesPageToLoad().TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
              .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
              .TapRelatedItemsButton()
              .TapActivitiesArea_RelatedItems()
              .TapCaseNotesIcon_RelatedItems();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate(DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (21)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (21) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate(DateTime.Now.AddMonths(-3).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (22)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (22) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate(DateTime.Now.AddYears(-3).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (23)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (23) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (24)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (24) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate(DateTime.Now.AddDays(3).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (25)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (25) activated"
              )
              .TapOnOKButton();

            caseCaseNotesPage.WaitForCaseCaseNotesPageToLoad().TapOnAddNewRecordButton();

            caseCaseNoteRecordPage
              .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
              .InserDate("09/08/2021", "")
              .InsertSubject("Condition Operators Testing CDV6-11975 - Scenario 5 (26)")
              .TapOnSaveAndCloseButton();

            errorPopup
              .WaitForErrorPopupToLoad()
              .ValidateErrorMessageTitleAndMessage(
                "Message",
                "Condition Operators Testing CDV6-11975 - Scenario 5 (26) activated"
              )
              .TapOnOKButton();
        }
        #endregion

        #region 4 - Business Rule Condition Builder popup testing

        [Test]
        [Property("JiraIssueID", "CDV6-12029")]
        [Description("Testing the Condition Builder - Several 'And' Conditions ")]
        public void CaseCaseNotes_BusinessRules_TestMethod012()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InsertSubject("Conditions Popup Testing CDV6-11975 - Scenario 1")
                .InsertDescription("desc ...")
                .InserDate("02/08/2021", "")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Assess*").TapSearchButtonQuery().TapOnRecord("Assessment");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 1 Activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .TapOnBackButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-12030")]
        [Description("Testing the Condition Builder - Several 'Or' Conditions ")]
        public void CaseCaseNotes_BusinessRules_TestMethod013()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InsertSubject("Conditions Popup Testing CDV6-11975 - Scenario 2")
                .InserDate("02/08/2021", "")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 2 activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Conditions Popup Testing CDV6-11975 - Scenario 2")
                .InserDate("01/08/2021", "") //set a different date
                .TapReasonLookupButton(); //set a matching Reason

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("7 day*").TapSearchButtonQuery().TapOnRecord("7 day follow up");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Conditions Popup Testing CDV6-11975 - Scenario 2")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 2 activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Conditions Popup Testing CDV6-11975 - Scenario 2")
                .TapReasonRemoveButton() //remove the Reason value
                .TapStatusPicklist();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Conditions Popup Testing CDV6-11975 - Scenario 2")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 2 activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Conditions Popup Testing CDV6-11975 - Scenario 2")
                .TapOnBackButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();

            var caseNoteId = PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID).FirstOrDefault();
            PlatformServicesHelper.caseCaseNote.UpdateStatus(caseNoteId, 1);
            PlatformServicesHelper.caseCaseNote.UpdateInactive(caseNoteId, false);

            caseCaseNotesPage
                .TapOnRecord("Conditions Popup Testing CDV6-11975 - Scenario 2");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Conditions Popup Testing CDV6-11975 - Scenario 2")
                .TapOnBackButton(); //at this point no alert should be displayed

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-12048")]
        [Description("Testing the Condition Builder - Form Mode = Create")]
        public void CaseCaseNotes_BusinessRules_TestMethod014()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InserDate("24/11/2021", "")
                .TapStatusPicklist();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTES (FOR CASE)")
                .InsertSubject("Conditions Popup Testing CDV6-11975 - Scenario 3")
                .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 3 - Create Mode").TapOnOKButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnRecord("Conditions Popup Testing CDV6-11975 - Scenario 3");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Conditions Popup Testing CDV6-11975 - Scenario 3")
                .TapOnBackButton(); //at this point no alert should be displayed as we are not in create mode

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-12049")]
        [Description("Testing the Condition Builder - Form Mode = Update")]
        public void CaseCaseNotes_BusinessRules_TestMethod015()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InserDate("12/08/2021", "")
                .InsertSubject("Conditions Popup Testing CDV6-11975 - Scenario 3")
                .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 3 - Create Mode").TapOnOKButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnRecord("Conditions Popup Testing CDV6-11975 - Scenario 3");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Conditions Popup Testing CDV6-11975 - Scenario 3");

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 3 - Update Mode").TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-12035")]
        [Description("Testing the Condition Builder - User Default Team Business Unit")]
        public void CaseCaseNotes_BusinessRules_TestMethod016()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InsertSubject("Conditions Popup Testing CDV6-11975 - Scenario 4")
                .InserDate("02/08/2021", "")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 4 activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .TapOnBackButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();
        }

        public void CaseCaseNotes_BusinessRules_TestMethod0150()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InserDate("12/08/2021", "")
                .InsertSubject("Conditions Popup Testing CDV6-11975 - Scenario 3")
                .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 3 - Create Mode").TapOnOKButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad()
                .TapOnRecord("Conditions Popup Testing CDV6-11975 - Scenario 3");

            caseCaseNoteRecordPage
                .WaitForCaseCaseNoteRecordPageToLoad("CASE NOTE (FOR CASE): Conditions Popup Testing CDV6-11975 - Scenario 3");

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 3 - Update Mode").TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-12100")]
        [Description("Testing the Condition _ Pop up _ Parent Relationship")]
        public void CaseCaseNotes_BusinessRules_TestMethod017()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.caseCaseNote.GetCaseNoteByCaseID(caseID))
                this.PlatformServicesHelper.caseCaseNote.DeleteCaseCaseNote(caseNoteID);


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
                .InsertSubject("Conditions Popup Testing CDV6-11975 - Scenario 4")
                .InserDate("02/08/2021", "")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Message", "Conditions Popup Testing CDV6-11975 - Scenario 4 activated").TapOnOKButton();

            caseCaseNoteRecordPage
                .TapOnBackButton();

            caseCaseNotesPage
                .WaitForCaseCaseNotesPageToLoad();
        }

        #endregion

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}
