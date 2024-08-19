using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.Cases.RelatedItems
{
    /// <summary>
    /// This class contains all test methods for Case Involvements validations while the APP is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class CaseInvolvements_OfflineTabletModeTests : TestBase
    {
        static UIHelper uIHelper;

        #region properties

        string LayoutXml_NoWidgets = "";
        string LayoutXml_OneWidgetNotConfigured = "<Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidget_GridView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-Indigo</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidgetListView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_TwoWidgets_GridView_ListView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Case Attachment (For Case) - Active Records</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>ae1c53f3-3010-e911-80dc-0050560502cc</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>          <BusinessObjectName>casecasenote</BusinessObjectName>          <HeaderColour>bg-Green</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidget_SharedUserView = "  <Layout  >    <Widgets>      <widget>        <Title>Task - Tasks created in the last 30 days</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>65ffa115-4890-ea11-a2cd-005056926fe4</DataViewId>          <DataViewType>shared</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightBlue</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_AllWidgets = "  <Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>IFrame</Type>          <Url>www.google.com</Url>          <HeaderColour>bg-Pink</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Task - My Active Tasks</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightBlue</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>6</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>Chart</Type>          <BusinessObjectId>30f84b2d-b169-e411-bf00-005056c00008</BusinessObjectId>          <BusinessObjectName>case</BusinessObjectName>          <ChartId>5b540885-d816-ea11-a2c8-005056926fe4</ChartId>          <ChartGroup>System</ChartGroup>          <HeaderColour>bg-Yellow</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Case Attachment (For Case) - Active Records</Title>        <X>0</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>ae1c53f3-3010-e911-80dc-0050560502cc</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>          <BusinessObjectName>casecasenote</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>3</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>WidgetCatalogue</Type>          <HTMLWebResource>Html_ClinicAvailableSlotsSearch.html</HTMLWebResource>          <HeaderColour>bg-Red</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>6</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>Report</Type>          <HeaderColour>bg-Purple</HeaderColour>          <HideHeader>false</HideHeader>          <ReportId>dc3d3d9f-1765-ea11-a2cb-005056926fe4</ReportId>        </Settings>      </widget>    </Widgets>  </Layout>";


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

        #region Case Involvements page


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6569")]
        [Description("UI Test for Case Involvements - 0002 - " +
            "Navigate to the Case Involvements area (case contains case attachment records) - Validate the page content")]
        public void CaseInvolvements_OfflineTestMethod02()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid caseInvolvementID = new Guid("34992f0d-4791-ea11-a2cd-005056926fe4");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapCaseInvolvements_RelatedItems();

            caseInvolvementsPage
                .WaitForCaseInvolvementsPageToLoad()
                .ValidateResponsibleMemberCellText("Mobile Test User 1", caseInvolvementID.ToString())
                .ValidatRoleCellText("Responsible User", caseInvolvementID.ToString())
                .ValidateResponsibleTeamByText("Mobile Team 1", caseInvolvementID.ToString())
                .ValidateStartDateByText("02/05/2020", caseInvolvementID.ToString())
                .ValidatEndDateByText("", caseInvolvementID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", caseInvolvementID.ToString())
                .ValidateCreatedOnCellText("08/05/2020 17:14", caseInvolvementID.ToString())
                .ValidateModifiedByCellText("Mobile Test User 1", caseInvolvementID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6570")]
        [Description("UI Test for Case Involvements - 0003 - " +
            "Navigate to the Case Involvements area - Open a cases case attachment record - Validate that the case attachment record page is displayed")]
        public void CaseInvolvements_OfflineTestMethod03()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string involvementMember = "Mobile Test User 1";

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapCaseInvolvements_RelatedItems();

            caseInvolvementsPage
                .WaitForCaseInvolvementsPageToLoad()
                .TapOnRecord(involvementMember);

            caseInvolvementRecordPage
                .WaitForCaseInvolvementRecordPageToLoad("CASE INVOLVEMENT: Case Involvement within Case MCNamara, Pavel - (01/05/1960) [CAS-000004-6174] created by Mobile Test User 1 on 08/05/2020 17:14:55");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6571")]
        [Description("UI Test for Case Involvements - 0004 - " +
            "Navigate to the Case Involvements area - Open a cases case attachment record - Validate that the case attachment record page fields and titles are displayed")]
        public void CaseInvolvements_OfflineTestMethod04()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string involvementMember = "Mobile Test User 1";

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapCaseInvolvements_RelatedItems();

            caseInvolvementsPage
                .WaitForCaseInvolvementsPageToLoad()
                .TapOnRecord(involvementMember);

            caseInvolvementRecordPage
                .WaitForCaseInvolvementRecordPageToLoad("CASE INVOLVEMENT: Case Involvement within Case MCNamara, Pavel - (01/05/1960) [CAS-000004-6174] created by Mobile Test User 1 on 08/05/2020 17:14:55")
                
                .ValidateCaseFieldTitleVisible(true)
                .ValidateInvolvementMemberFieldTitleVisible(true)
                .ValidateRoleFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateInvolvementReasonFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateInvolvementPriorityFieldTitleVisible(true)
                .ValidateInvolvementStatusFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateDescriptionFieldTitleVisible(true)

                .ValidateCaseFieldText("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .ValidateInvolvementMemberFieldText("Mobile Test User 1")
                .ValidateRoleFieldText("Responsible User")
                .ValidateStartDateFieldText("02/05/2020")
                .ValidateinvolvementReasonFieldText("Care Coordinator")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateInvolvementPriorityFieldText("High")
                .ValidateInvolvementStatusFieldText("In Progress")
                .ValidateEndDateFieldText("")
                .ValidateInvolvementEndReasonFieldText("End Reason 1")
                .ValidateDescriptionFieldText("description info ...");

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
