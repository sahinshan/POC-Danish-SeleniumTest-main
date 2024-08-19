using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.Health
{
    /// <summary>
    /// This class  containsall test methods for person body maps validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PersonBodyMaps_TabletModeTests : TestBase
    {
        static UIHelper uIHelper;

        #region properties

        string LayoutXml_NoWidgets = "";
        string LayoutXml_OneWidgetNotConfigured = "<Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidget_GridView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-Indigo</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidgetListView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_TwoWidgets_GridView_ListView = "<Layout  >    <Widgets>      <widget>        <Title>Task - My Active Tasks</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Case Note (For Case) - Active Records</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>ae1c53f3-3010-e911-80dc-0050560502cc</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>          <BusinessObjectName>casecasenote</BusinessObjectName>          <HeaderColour>bg-Green</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_OneWidget_SharedUserView = "  <Layout  >    <Widgets>      <widget>        <Title>Task - Tasks created in the last 30 days</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>65ffa115-4890-ea11-a2cd-005056926fe4</DataViewId>          <DataViewType>shared</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightBlue</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>    </Widgets>  </Layout>";
        string LayoutXml_AllWidgets = "  <Layout  >    <Widgets>      <widget>        <Title>New widget</Title>        <X>0</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>IFrame</Type>          <Url>www.google.com</Url>          <HeaderColour>bg-Pink</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Task - My Active Tasks</Title>        <X>3</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>GridView</Type>          <DataViewId>bfdef053-6cc5-e811-9c01-1866da1e4209</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>04f4874b-199d-e611-9bd2-1866da1e4209</BusinessObjectId>          <BusinessObjectName>task</BusinessObjectName>          <HeaderColour>bg-LightBlue</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>6</X>        <Y>0</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>Chart</Type>          <BusinessObjectId>30f84b2d-b169-e411-bf00-005056c00008</BusinessObjectId>          <BusinessObjectName>person</BusinessObjectName>          <ChartId>5b540885-d816-ea11-a2c8-005056926fe4</ChartId>          <ChartGroup>System</ChartGroup>          <HeaderColour>bg-Yellow</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>Case Note (For Case) - Active Records</Title>        <X>0</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>ListView</Type>          <DataViewId>ae1c53f3-3010-e911-80dc-0050560502cc</DataViewId>          <DataViewType>system</DataViewType>          <BusinessObjectId>b41b53f3-3010-e911-80dc-0050560502cc</BusinessObjectId>          <BusinessObjectName>casecasenote</BusinessObjectName>          <HeaderColour>bg-LightGreen</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>3</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>WidgetCatalogue</Type>          <HTMLWebResource>Html_ClinicAvailableSlotsSearch.html</HTMLWebResource>          <HeaderColour>bg-Red</HeaderColour>          <HideHeader>false</HideHeader>        </Settings>      </widget>      <widget>        <Title>New widget</Title>        <X>6</X>        <Y>3</Y>        <Width>3</Width>        <Height>3</Height>        <Settings>          <Type>Report</Type>          <HeaderColour>bg-Purple</HeaderColour>          <HideHeader>false</HideHeader>          <ReportId>dc3d3d9f-1765-ea11-a2cb-005056926fe4</ReportId>        </Settings>      </widget>    </Widgets>  </Layout>";


        #endregion

        [TestFixtureSetUp]
        public void ClassInitializationMethod()
        {
            if (this.IgnoreTestFixtureSetUp)
                return;

            //authenticate a user against the platform services
            this.PlatformServicesHelper.AuthenticateUser("SecurityTestUserAdmin", "Passw0rd_!");

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

            //if the person body map injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the person body map review pop-up is open then close it 
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


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6892")]
        [Description("UI Test for Dashboards - 0001 - " +
            "Navigate to the person body maps area (person do not contains person body map records) - Validate the page content")]
        public void PersonBodyMaps_TestMethod01()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            this.PlatformServicesHelper = new PlatformServicesHelper();
            //remove any person body map for the person
            foreach (Guid bodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(bodyMapID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6893")]
        [Description("UI Test for Dashboards - 0002 - " +
            "Navigate to the person body maps area (person contains person body map record with empty view type field) - " +
            "Validate the page content")]
        public void PersonBodyMaps_TestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            Guid bodyMapRecordID = new Guid("e1031d3a-4791-ea11-a2cd-005056926fe4"); //01/05/2020 08:00:00

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateDateOfEventCellText("01/05/2020 08:00", bodyMapRecordID.ToString())
                .ValidateViewTypeCellText("", bodyMapRecordID.ToString())
                .ValidateIsReviewRequiredCellText("", bodyMapRecordID.ToString())
                .ValidateReviewDateCellText("", bodyMapRecordID.ToString())
                .ValidateModifiedOnCellText("08/05/2020 17:16", bodyMapRecordID.ToString())
                .ValidateModifiedByCellText("Mobile Test User 1", bodyMapRecordID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", bodyMapRecordID.ToString())
                .ValidateCreatedOnCellText("08/05/2020 17:16", bodyMapRecordID.ToString())
                ;
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6894")]
        [Description("UI Test for Dashboards - 0003 - " +
            "Navigate to the person body maps area (person contains person body map record with all fields set) - " +
            "Validate the page content")]
        public void PersonBodyMaps_TestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            Guid bodyMapRecordID = new Guid("946da179-4791-ea11-a2cd-005056926fe4"); //04/05/2020 13:00:00

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateDateOfEventCellText("04/05/2020 13:00", bodyMapRecordID.ToString())
                .ValidateViewTypeCellText("Full Body", bodyMapRecordID.ToString())
                .ValidateIsReviewRequiredCellText("No", bodyMapRecordID.ToString())
                .ValidateReviewDateCellText("05/05/2020 07:00", bodyMapRecordID.ToString())
                .ValidateModifiedOnCellText("08/05/2020 17:20", bodyMapRecordID.ToString())
                .ValidateModifiedByCellText("Mobile Test User 1", bodyMapRecordID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", bodyMapRecordID.ToString())
                .ValidateCreatedOnCellText("08/05/2020 17:18", bodyMapRecordID.ToString());
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6895")]
        [Description("UI Test for Dashboards - 0004 - " +
            "Navigate to the person body maps area - Open a person body map record - Validate that the user is redirected to the person body map page")]
        public void PersonBodyMaps_TestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            Guid bodyMapRecordID = new Guid("946da179-4791-ea11-a2cd-005056926fe4"); //04/05/2020 13:00:00

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("04/05/2020 13:00");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Mr Pavel MCNamara on 04/05/2020 13:00:00");
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6896")]
        [Description("UI Test for Dashboards - 0005 - " +
            "Navigate to the person body maps area - Open a person body map record (record with data in all fields) - Validate that all field titles are visible")]
        public void PersonBodyMaps_TestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            Guid bodyMapRecordID = new Guid("946da179-4791-ea11-a2cd-005056926fe4"); //04/05/2020 13:00:00

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("04/05/2020 13:00");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Mr Pavel MCNamara on 04/05/2020 13:00:00")
                .ValidateDateOfEventFieldTitleVisible(true)
                .ValidateViewTypeFieldTitleVisible(true)
                .ValidateInjuryDescriptionFieldTitleVisible(true)
                .ValidateIsReviewRequiredFieldTitleVisible(true)
                .ValidateReviewsTitleVisible(true);

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6897")]
        [Description("UI Test for Dashboards - 0006 - " +
            "Navigate to the person body maps area - Open a person body map record (record with data in all fields) - " +
            "Validate that all field values correctly displayed")]
        public void PersonBodyMaps_TestMethod06()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            Guid bodyMapRecordID = new Guid("946da179-4791-ea11-a2cd-005056926fe4"); //04/05/2020 13:00:00

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("04/05/2020 13:00");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Mr Pavel MCNamara on 04/05/2020 13:00:00")
                .ValidateDateOfEventFieldText("04/05/2020 13:00")
                .ValidateViewTypeFieldText("Full Body")
                .ValidateIsReviewRequiriedFieldText("No");

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6898")]
        [Description("UI Test for Dashboards - 0007 - " +
            "Navigate to the person body maps area - Open a person body map record (record with multiple injury descriptions) - " +
            "Validate that the injury descriptions are displayed with the description and name information")]
        public void PersonBodyMaps_TestMethod07()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            Guid bodyMapRecordID = new Guid("946da179-4791-ea11-a2cd-005056926fe4"); //04/05/2020 13:00:00

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("04/05/2020 13:00");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Mr Pavel MCNamara on 04/05/2020 13:00:00")
                .ValidateInjuryDescriptionRecordTitle("Broken Rib")
                .ValidateInjuryDescriptionRecordDescription("Torso Front")
                .ValidateInjuryDescriptionRecordTitle("Broken Arm")
                .ValidateInjuryDescriptionRecordDescription("Left Arm Back");

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6899")]
        [Description("UI Test for Dashboards - 0008 - " +
            "Navigate to the person body maps area - Open a person body map record (record with multiple Review records) - " +
            "Validate that the review records are displayed with correct titles and sub titles")]
        public void PersonBodyMaps_TestMethod08()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            Guid bodyMapRecordID = new Guid("946da179-4791-ea11-a2cd-005056926fe4"); //04/05/2020 13:00:00

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("04/05/2020 13:00");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Mr Pavel MCNamara on 04/05/2020 13:00:00")
                .ValidateReviewRecordTitle("Planned Review: 06/05/2020 08:00")
                .ValidateReviewRecordSubTitle("Actual Review: 06/05/2020 02:05")
                .ValidateReviewRecordTitle("Planned Review: 05/05/2020 07:00")
                .ValidateReviewRecordSubTitle("Actual Review: 05/05/2020 07:00")
                ;

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6900")]
        [Description("UI Test for Dashboards - 0009 - " +
            "Navigate to the person body maps area - Open a person body map record (record with multiple injury descriptions) - " +
            "Tap on a injury description record - Validate that the injury description pop-up is displayed and all fields titles are correctly displayed")]
        public void PersonBodyMaps_TestMethod09()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            Guid bodyMapRecordID = new Guid("946da179-4791-ea11-a2cd-005056926fe4"); //04/05/2020 13:00:00

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("04/05/2020 13:00");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Mr Pavel MCNamara on 04/05/2020 13:00:00")
                .TapOnInjuryDescriptionRecord("Broken Rib");

            personBodyInjuryDescriptionPopup
                .WaitForPersonBodyInjuryDescriptionPopupToLoad("BODY MAP INJURY DESCRIPTION: Torso-Front")
                .ValidateNameFieldTitleVisible(true)
                .ValidatePersonBodymapFieldTitleVisible(true)
                .ValidateDescriptionFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateBodyMapSetupFieldTitleVisible(true)
                .ValidateCreatedByTitleVisible(true)
                .ValidateCreatedOnTitleVisible(true)
                .ValidateModifieddByTitleVisible(true)
                .ValidateModifieddOnTitleVisible(true);

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6901")]
        [Description("UI Test for Dashboards - 0010 - " +
            "Navigate to the person body maps area - Open a person body map record (record with multiple injury descriptions) - " +
            "Tap on a injury description record - Validate that the injury description pop-up is displayed and all fields values are correctly displayed")]
        public void PersonBodyMaps_TestMethod10()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            Guid bodyMapRecordID = new Guid("946da179-4791-ea11-a2cd-005056926fe4"); //04/05/2020 13:00:00

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("04/05/2020 13:00");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Mr Pavel MCNamara on 04/05/2020 13:00:00")
                .TapOnInjuryDescriptionRecord("Broken Rib");

            personBodyInjuryDescriptionPopup
                .WaitForPersonBodyInjuryDescriptionPopupToLoad("BODY MAP INJURY DESCRIPTION: Torso-Front")
                .ValidateNameFieldValue("Torso-Front")
                .ValidatePersonBodyMapFieldValue("Body Map for Mr Pavel MCNamara on 04/05/2020 13:00:00")
                .ValidateDescriptionFieldValue("Broken Rib")
                .ValidateResponsibleTeamFieldValue("Mobile Team 1")
                .ValidateBodyMapSetupFieldValue("Torso Front")
                .ValidateCreatedByFieldText("Mobile Test User 1")
                .ValidateCreatedOnFieldText("08/05/2020 17:18")
                .ValidateModifieddByFieldText("Mobile Test User 1")
                .ValidateModifieddOnFieldText("08/05/2020 17:20");

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6902")]
        [Description("UI Test for Dashboards - 0011 - " +
            "Navigate to the person body maps area - Open a person body map record (record with multiple Reviews) - " +
            "Tap on a review record (record has Is review Required? set to No) - " +
            "Validate that the review pop-up is displayed and all fields titles are correctly displayed")]
        public void PersonBodyMaps_TestMethod11()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            Guid bodyMapRecordID = new Guid("946da179-4791-ea11-a2cd-005056926fe4"); //04/05/2020 13:00:00

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("04/05/2020 13:00");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Mr Pavel MCNamara on 04/05/2020 13:00:00")
                .TapOnReviewRecord("Planned Review: 06/05/2020 08:00");


            personBodyMapReviewPopup
                .WaitForPersonBodyMapReviewPopupToLoad("PERSON BODY MAP REVIEW: Body Map Review for Mr Pavel MCNamara created on 08/05/2020 17:19:50")
                .ValidateDateTimeOfPlannedReviewFieldTitleVisible(true)
                .ValidateDateTimeOfActualReviewFieldTitleVisible(true)
                .ValidateIsReviewRequiredFieldTitleVisible(true)
                .ValidateDateTimeOfNextReviewFieldTitleVisible(false)
                .ValidateProfessionalCommentsFieldTitleVisible(true)
                .ValidateClientCommentsFieldTitleVisible(true)
                .ValidateAgreedOutcomeFieldTitleVisible(true)
                .ValidateCreatedByTitleVisible(true)
                .ValidateCreatedOnTitleVisible(true)
                .ValidateModifieddByTitleVisible(true)
                .ValidateModifieddOnTitleVisible(true)
                ;
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6903")]
        [Description("UI Test for Dashboards - 0012 - " +
            "Navigate to the person body maps area - Open a person body map record (record with multiple Reviews) - " +
            "Tap on a review record (record has Is review Required? set to Yes) - " +
            "Validate that the review pop-up is displayed and all fields titles are correctly displayed")]
        public void PersonBodyMaps_TestMethod12()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara
            Guid bodyMapRecordID = new Guid("946da179-4791-ea11-a2cd-005056926fe4"); //04/05/2020 13:00:00

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("04/05/2020 13:00");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Mr Pavel MCNamara on 04/05/2020 13:00:00")
                .TapOnReviewRecord("Planned Review: 05/05/2020 07:00");


            personBodyMapReviewPopup
                .WaitForPersonBodyMapReviewPopupToLoad("PERSON BODY MAP REVIEW: Body Map Review for Mr Pavel MCNamara created on 08/05/2020 17:18:50")
                .ValidateDateTimeOfPlannedReviewFieldValue("05/05/2020 07:00")
                .ValidateDateTimeOfActualReviewFieldValue("05/05/2020 07:00")
                .ValidateIsReviewRequiredFieldValue("Yes")
                .ValidateDateTimeOfNextReviewFieldValue("06/05/2020 08:00")
                .ValidateProfessionalCommentsFieldValue("Comment 1\nComment 2")
                .ValidateClientCommentsFieldValue("Comment 3\nComment 4")
                .ValidateAgreedOutcomeFieldValue("Comment 5\nComment 6")
                .ValidateCreatedByFieldValue("Mobile Test User 1")
                .ValidateCreatedOnFieldValue("08/05/2020 17:18")
                .ValidateModifieddByFieldValue("Mobile Test User 1")
                .ValidateModifieddOnFieldValue("08/05/2020 17:20")

                ;
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6904")]
        [Description("UI Test for Dashboards - 0013 - " +
            "Navigate to the person body maps area - Open a person body map record (record with no review information) - " +
            "Validate that the review are is not visible")]
        public void PersonBodyMaps_TestMethod13()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("03/05/2020 08:30");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Mr Pavel MCNamara on 03/05/2020 08:30:00")
                .ValidateReviewsTitleVisible(false);



        }

        [Test]
        [Property("JiraIssueID", "CDV6-6905")]
        [Description("UI Test for Dashboards - 0014 - " +
            "Navigate to the person body maps area - Open an active person body map record (with injury information and no review information ) - " +
            "Validate that all field values are correctly displayed")]
        public void PersonBodyMaps_TestMethod14()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("02/05/2020 09:05");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Mr Pavel MCNamara on 02/05/2020 09:05:00")
                .ValidateDateOfEventFieldText("02/05/2020 09:05")
                .ValidateViewTypeFieldText("Head")
                .ValidateBodyAreaLookupButtonVisible(true)
                .ValidateEmptyDescriptionOfIssueField("")
                .ValidateInjuryDescriptionFieldTitleVisible(true)
                .ValidateInjuryDescriptionRecordTitle("hematoma ")
                .ValidateInjuryDescriptionRecordDescription("Head Right Side - Ear")
                .ValidateInjuryDescriptionRecordTitle("Large Cut")
                .ValidateInjuryDescriptionRecordDescription("Head Left Side - Chin")
                .ValidateIsReviewRequiredFieldTitleVisible(true);

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6906")]
        [Description("UI Test for Dashboards - 0015 - " +
            "Navigate to the person body maps area - Tap on the add button - Wait for the Person Body Map new record page to load - Tap on the save button (do not set any info)" +
            "Validate that the user is prevented from saving the record")]
        public void PersonBodyMaps_TestMethod15()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();

            //remove any person body map for the person
            foreach (Guid personBodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(personBodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(personBodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(personBodyMapID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnAddNewRecordButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAPS")
                .InsertDateOfEvent("", "")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Date Of Event' is required")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6907")]
        [Description("UI Test for Dashboards - 0016 - " +
            "Navigate to the person body maps area - Tap on the add button - Wait for the Person Body Map new record page to load - " +
            "Set a Date of Event - Tap on the save button - " +
            "Validate that the record is saved only with the information set by the user")]
        public void PersonBodyMaps_TestMethod16()
        {
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1

            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();

            //remove any person body map for the person
            foreach (Guid personBodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(personBodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(personBodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(personBodyMapID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnAddNewRecordButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAPS")
                .InsertDateOfEvent("12/05/2020", "09:30")
                .TapOnSaveAndCloseButton();

            personBodyMapsPage
               .WaitForPersonBodyMapsPageToLoad();


            var personBodyMapRecords = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID);
            Assert.AreEqual(1, personBodyMapRecords.Count);

            var fields = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByID(personBodyMapRecords[0],
                "OwnerId", "ResponsibleUserId", "PersonId", "DateOfEvent", "ReviewDate", "ViewTypeId", "IsReviewRequiredId", "PersonAge");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            Assert.AreEqual(mobileTeam1TeamID, (Guid)fields["OwnerId".ToLower()]);
            Assert.AreEqual(mobileTestUser1UserID, (Guid)fields["ResponsibleUserId".ToLower()]);
            Assert.AreEqual(personID, (Guid)fields["PersonId".ToLower()]);
            Assert.AreEqual(new DateTime(2020, 5, 12, 9, 30, 0).ToString("dd'/'MM'/'yyyy HH:mm"), usersettings.ConvertTimeFromUtc((DateTime)fields["dateofevent"]).Value.ToString("dd'/'MM'/'yyyy HH:mm"));
            Assert.IsFalse(fields.ContainsKey("ReviewDate".ToLower()));
            Assert.IsFalse(fields.ContainsKey("ViewTypeId".ToLower()));
            Assert.IsFalse(fields.ContainsKey("IsReviewRequiredId".ToLower()));
            Assert.AreEqual(63, (int)fields["PersonAge".ToLower()]);

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6908")]
        [Description("UI Test for Dashboards - 0017 - " +
            "Navigate to the person body maps area - Tap on the add button - Wait for the Person Body Map new record page to load - " +
            "Set a Date of Event - Tap on the save button - " +
            "Validate that the record is saved and the APP displays the remaining fields")]
        public void PersonBodyMaps_TestMethod17()
        {
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1

            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();

            //remove any person body map for the person
            foreach (Guid personBodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(personBodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(personBodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(personBodyMapID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnAddNewRecordButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAPS")
                .InsertDateOfEvent("12/05/2020", "09:30")
                .TapOnSaveButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoadAfterFirstSave("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00");

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6909")]
        [Description("UI Test for Dashboards - 0018 - " +
            "Navigate to the person body maps area - Tap on the add button - Wait for the Person Body Map new record page to load - " +
            "Set a Date of Event - Tap on the save button - Wait for the record to be saved and the page to reload - Select a body area - Insert a description - tap on the save button" +
            "Validate that the record is saved and sync to the Web APP")]
        public void PersonBodyMaps_TestMethod18()
        {
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1

            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();

            //remove any person body map for the person
            foreach (Guid personBodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(personBodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(personBodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(personBodyMapID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnAddNewRecordButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAPS")
                .InsertDateOfEvent("12/05/2020", "09:30")
                .TapOnSaveButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoadAfterFirstSave("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapBodyAreaLookupButton();

            bodyAreaLookupPopup
                .WaitForBodyAreaLookupPopupToLoad()
                .TapOnRecord("Head Back");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoadAfterFirstSave("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .InsertDescriptionOfIssueInjurySymptom("Large Cut")
                .TapOnSaveButton()
                .WaitForPersonBodyMapRecordPageToLoadAfterFirstSave("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00");


            var personBodyMapRecords = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID);
            var fields = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByID(personBodyMapRecords[0], "OwnerId", "ResponsibleUserId", "PersonId", "DateOfEvent", "ReviewDate", "ViewTypeId", "IsReviewRequiredId", "PersonAge");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            Assert.AreEqual(mobileTeam1TeamID, (Guid)fields["OwnerId".ToLower()]);
            Assert.AreEqual(mobileTestUser1UserID, (Guid)fields["ResponsibleUserId".ToLower()]);
            Assert.AreEqual(personID, (Guid)fields["PersonId".ToLower()]);
            Assert.AreEqual(new DateTime(2020, 5, 12, 9, 30, 0).ToString("dd'/'MM'/'yyyy HH:mm"), usersettings.ConvertTimeFromUtc((DateTime)fields["dateofevent"]).Value.ToString("dd'/'MM'/'yyyy HH:mm"));
            Assert.IsFalse(fields.ContainsKey("ReviewDate".ToLower()));
            Assert.AreEqual(1, (int)fields["ViewTypeId".ToLower()]);
            Assert.IsFalse(fields.ContainsKey("IsReviewRequiredId".ToLower()));
            Assert.AreEqual(63, (int)fields["PersonAge".ToLower()]);


            Guid bodyMapSetupID = new Guid("E5C7F9CE-E7A9-E811-80DC-005056050630"); //Head-Back
            var injuries = PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(personBodyMapRecords[0]);
            Assert.AreEqual(1, injuries.Count);
            var injuryRecordFields = PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByID(injuries[0], "name", "bodymapsetupid", "description", "personid");

            Assert.AreEqual("Head Back", (string)injuryRecordFields["name".ToLower()]);
            Assert.AreEqual(bodyMapSetupID, (Guid)injuryRecordFields["bodymapsetupid".ToLower()]);
            Assert.AreEqual("Large Cut", (string)injuryRecordFields["description".ToLower()]);
            Assert.AreEqual(personID, (Guid)injuryRecordFields["personid".ToLower()]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6910")]
        [Description("UI Test for Dashboards - 0019 - " +
            "Navigate to the person body maps area - Open a person body map record - " +
            "Set Is Review Required to No - insert date/time of review - Tap on the save button - " +
            "Validate that the record is saved and set to Inactive and validate that the associated review record is created")]
        public void PersonBodyMaps_TestMethod19()
        {
            Guid bodyMapSetupID = new Guid("E5C7F9CE-E7A9-E811-80DC-005056050630"); //Head-Back
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1
            Guid headBackBodyAreaID = new Guid("1e84431d-e7d2-4ec7-9792-ae487564bf94"); //Head-Back body area

            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();

            //remove any person body map for the person
            foreach (Guid bodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(bodyMapID);
            }

            Guid personBodyMapID = PlatformServicesHelper.personBodyMap.CreatePersonBodyMap(mobileTeam1TeamID, mobileTestUser1UserID, personID, new DateTime(2020, 5, 12, 9, 30, 0), null, 1, null, 60);
            Guid personBodyMapInjuryID = PlatformServicesHelper.personBodyMapInjuryDescription.CreatePersonBodyMapInjuryDescription(mobileTeam1TeamID, personBodyMapID, personID, bodyMapSetupID, "Head-Back", "Large Cut");


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("12/05/2020 09:30");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapIsReviewrequiredField();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(2)
                .TapOKButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnSaveButton()
                .WaitForSaveButtonNotVisible()
                .WaitOnSaveAndCloseButtonNotVisible();


            var personBodyMapRecords = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID);
            var fields = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByID(personBodyMapRecords[0], "isreviewrequiredid", "inactive");

            Assert.AreEqual(2, fields["isreviewrequiredid"]);
            Assert.AreEqual(true, fields["inactive"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6911")]
        [Description("UI Test for Dashboards - 0020 - " +
            "Navigate to the person body maps area - Open a person body map record - " +
            "Set Is Review Required to Yes - Tap on the save button - Validate that the record is saved and is not set to Inactive")]
        public void PersonBodyMaps_TestMethod20()
        {
            Guid bodyMapSetupID = new Guid("E5C7F9CE-E7A9-E811-80DC-005056050630"); //Head-Back
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1
            Guid headBackBodyAreaID = new Guid("1e84431d-e7d2-4ec7-9792-ae487564bf94"); //Head-Back body area

            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();

            //remove any person body map for the person
            foreach (Guid bodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(bodyMapID);
            }

            Guid personBodyMapID = PlatformServicesHelper.personBodyMap.CreatePersonBodyMap(mobileTeam1TeamID, mobileTestUser1UserID, personID, new DateTime(2020, 5, 12, 9, 30, 0), null, 1, null, 60);
            Guid personBodyMapInjuryID = PlatformServicesHelper.personBodyMapInjuryDescription.CreatePersonBodyMapInjuryDescription(mobileTeam1TeamID, personBodyMapID, personID, bodyMapSetupID, "Head-Back", "Large Cut");


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("12/05/2020 09:30");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapIsReviewrequiredField();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(1)
                .TapOKButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .InsertDateTimeOfReview("13/05/2020", "10:30")
                .TapOnSaveAndCloseButton();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad();


            var personBodyMapRecords = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID);
            var fields = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByID(personBodyMapRecords[0], "isreviewrequiredid", "inactive");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            Assert.AreEqual(1, fields["isreviewrequiredid"]);
            Assert.AreEqual(false, fields["inactive"]);

            var reviewRecords = this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(personBodyMapRecords[0]);
            Assert.AreEqual(1, reviewRecords.Count);

            var reviewRecordFields = PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByID(reviewRecords[0], "inactive", "PlannedReviewDate", "ActualReviewDate", "IsReviewRequiredId", "NextReviewDate", "ProfessionalComments", "ClientComments", "AgreedOutcome", "PersonId");
            Assert.AreEqual(false, (bool)reviewRecordFields["inactive".ToLower()]);
            Assert.AreEqual(new DateTime(2020, 5, 13, 10, 30, 0).ToString("dd'/'MM'/'yyyy HH:mm"), usersettings.ConvertTimeFromUtc((DateTime)reviewRecordFields["plannedreviewdate"]).Value.ToString("dd'/'MM'/'yyyy HH:mm"));
            Assert.IsFalse(reviewRecordFields.ContainsKey("ActualReviewDate".ToLower()));
            Assert.IsFalse(reviewRecordFields.ContainsKey("IsReviewRequiredId".ToLower()));
            Assert.IsFalse(reviewRecordFields.ContainsKey("NextReviewDate".ToLower()));
            Assert.IsFalse(reviewRecordFields.ContainsKey("ProfessionalComments".ToLower()));
            Assert.IsFalse(reviewRecordFields.ContainsKey("ClientComments".ToLower()));
            Assert.IsFalse(reviewRecordFields.ContainsKey("AgreedOutcome".ToLower()));
            Assert.AreEqual(personID, reviewRecordFields["PersonId".ToLower()]);



        }

        [Test]
        [Property("JiraIssueID", "CDV6-6912")]
        [Description("UI Test for Dashboards - 0021 - " +
            "Navigate to the person body maps area - Open a person body map record (with a injury record) - " +
            "Open the injury record - Update the Description field - Save the change - validate that the data is sync to the web database")]
        public void PersonBodyMaps_TestMethod21()
        {
            Guid bodyMapSetupID = new Guid("E5C7F9CE-E7A9-E811-80DC-005056050630"); //Head-Back
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1
            Guid headBackBodyAreaID = new Guid("1e84431d-e7d2-4ec7-9792-ae487564bf94"); //Head-Back body area

            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();

            //remove any person body map for the person
            foreach (Guid bodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(bodyMapID);
            }

            Guid personBodyMapID = PlatformServicesHelper.personBodyMap.CreatePersonBodyMap(mobileTeam1TeamID, mobileTestUser1UserID, personID, new DateTime(2020, 5, 12, 9, 30, 0), null, 1, null, 60);
            Guid personBodyMapInjuryID = PlatformServicesHelper.personBodyMapInjuryDescription.CreatePersonBodyMapInjuryDescription(mobileTeam1TeamID, personBodyMapID, personID, bodyMapSetupID, "Head-Back", "Large Cut");


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("12/05/2020 09:30");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnInjuryDescriptionRecord("Large Cut");

            personBodyInjuryDescriptionPopup
                .WaitForPersonBodyInjuryDescriptionPopupToLoad("BODY MAP INJURY DESCRIPTION: Head-Back")
                .InsertDescriptionReview("Large Cut - Update")
                .TapSaveButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnSaveAndCloseButton();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad();



            var personBodyMapRecords = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID);
            var injuries = this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(personBodyMapRecords[0]);
            Assert.AreEqual(1, injuries.Count);

            var reviewRecordFields = PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByID(injuries[0], "description");
            Assert.AreEqual("Large Cut - Update", (string)reviewRecordFields["description".ToLower()]);



        }

        [Test]
        [Property("JiraIssueID", "CDV6-6913")]
        [Description("UI Test for Dashboards - 0022 - " +
            "Navigate to the person body maps area - Open a person body map record (with a injury record) - " +
            "Open the injury record - Tap on the delete button - Save the change - validate that injury  record is deleted from the web database")]
        public void PersonBodyMaps_TestMethod22()
        {
            Guid bodyMapSetupID = new Guid("E5C7F9CE-E7A9-E811-80DC-005056050630"); //Head-Back
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1
            Guid headBackBodyAreaID = new Guid("1e84431d-e7d2-4ec7-9792-ae487564bf94"); //Head-Back body area

            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();

            //Remove any person body map for the person
            foreach (Guid bodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(bodyMapID);
            }

            Guid personBodyMapID = PlatformServicesHelper.personBodyMap.CreatePersonBodyMap(mobileTeam1TeamID, mobileTestUser1UserID, personID, new DateTime(2020, 5, 12, 9, 30, 0), null, 1, null, 60);
            Guid personBodyMapInjuryID = PlatformServicesHelper.personBodyMapInjuryDescription.CreatePersonBodyMapInjuryDescription(mobileTeam1TeamID, personBodyMapID, personID, bodyMapSetupID, "Head-Back", "Large Cut");


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("12/05/2020 09:30");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnInjuryDescriptionRecord("Large Cut");

            personBodyInjuryDescriptionPopup
                .WaitForPersonBodyInjuryDescriptionPopupToLoad("BODY MAP INJURY DESCRIPTION: Head-Back")
                .TapDeleteButton();

            warningPopup
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?")
                .TapOnYesButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnSaveAndCloseButton();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad();



            var personBodyMapRecords = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID);
            var injuries = this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(personBodyMapRecords[0]);
            Assert.AreEqual(0, injuries.Count);

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6914")]
        [Description("UI Test for Dashboards - 0023 - " +
            "Navigate to the person body maps area - Open a person body map record (with a review records) - " +
            "Open the review record - Set Date/Time of Actual review, Is Review Required? = No and all comments fields - Save the record - " +
            "Validate that the review info is sync to the web database and that the body map record is disabled")]
        public void PersonBodyMaps_TestMethod23()
        {
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            //Remove any person body map for the person
            foreach (Guid bodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(bodyMapID);
            }

            Guid personBodyMapID = PlatformServicesHelper.personBodyMap.CreatePersonBodyMap(mobileTeam1TeamID, mobileTestUser1UserID, personID, new DateTime(2020, 5, 12, 9, 30, 0), null, 1, null, 60);
            Guid personBodyMapReview = PlatformServicesHelper.personBodyMapReview.CreatePersonBodyMapReview(mobileTeam1TeamID, personBodyMapID, personID, new DateTime(2020, 5, 13, 9, 0, 0), null, null, null, null, null, null);
            DateTime personBodyMapReviewCreatedOnDate = ((DateTime)PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByID(personBodyMapReview, "createdon")["createdon"]);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("12/05/2020 09:30");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnReviewRecord("Planned Review: 13/05/2020 09:00");

            personBodyMapReviewPopup
                .WaitForPersonBodyMapReviewPopupToLoad("PERSON BODY MAP REVIEW: Body Map Review for Maria Tsatsouline created on " + usersettings.ConvertTimeFromUtc(personBodyMapReviewCreatedOnDate).Value.ToString("dd'/'MM'/'yyyy HH:mm:ss"))
                .InsertDateTimeOfActualReview("13/05/2020", "10:00")
                .InsertProfessionalComments("professional comments ...")
                .InsertClientComments("client comments ...")
                .InsertAgreedOutcome("outcome ...")
                .TapIsReviewRequiredField();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(2)
                .TapOKButton();

            personBodyMapReviewPopup
                .WaitForPersonBodyMapReviewPopupToLoad("PERSON BODY MAP REVIEW: Body Map Review for Maria Tsatsouline created on " + usersettings.ConvertTimeFromUtc(personBodyMapReviewCreatedOnDate).Value.ToString("dd'/'MM'/'yyyy HH:mm:ss"))
                .TapSaveButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnSaveAndCloseButton();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad();


            var bodyMapFields = PlatformServicesHelper.personBodyMap.GetPersonBodyMapByID(personBodyMapID, "inactive");
            Assert.AreEqual(true, (bool)bodyMapFields["inactive"]);

            var reviewRecords = PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(personBodyMapID);
            Assert.AreEqual(1, reviewRecords.Count);

            var reviewFields = PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByID(reviewRecords[0], "plannedreviewdate", "actualreviewdate", "isreviewrequiredid", "nextreviewdate", "professionalcomments", "clientcomments", "agreedoutcome", "personid");
            Assert.AreEqual(new DateTime(2020, 5, 13, 9, 0, 0).ToString("dd'/'MM'/'yyyy HH:mm"), usersettings.ConvertTimeFromUtc((DateTime)reviewFields["plannedreviewdate"]).Value.ToString("dd'/'MM'/'yyyy HH:mm"));
            Assert.AreEqual(new DateTime(2020, 5, 13, 10, 0, 0).ToString("dd'/'MM'/'yyyy HH:mm"), usersettings.ConvertTimeFromUtc((DateTime)reviewFields["actualreviewdate"]).Value.ToString("dd'/'MM'/'yyyy HH:mm"));
            Assert.AreEqual(2, reviewFields["isreviewrequiredid"]);
            Assert.AreEqual(false, reviewFields.ContainsKey("nextreviewdate"));
            Assert.AreEqual("professional comments ...", (string)reviewFields["professionalcomments"]);
            Assert.AreEqual("client comments ...", (string)reviewFields["clientcomments"]);
            Assert.AreEqual("outcome ...", (string)reviewFields["agreedoutcome"]);
            Assert.AreEqual(personID, (Guid)reviewFields["personid"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6915")]
        [Description("UI Test for Dashboards - 0024 - " +
            "Navigate to the person body maps area - Open a person body map record (with a review records) - " +
            "Open the review record - Set Date/Time of Actual review, Is Review Required? = No and all comments fields - Save the record - " +
            "Validate that the review info is sync to the web database and that the body map record is disabled")]
        public void PersonBodyMaps_TestMethod24()
        {
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            //Remove any person body map for the person
            foreach (Guid bodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(bodyMapID);
            }

            Guid personBodyMapID = PlatformServicesHelper.personBodyMap.CreatePersonBodyMap(mobileTeam1TeamID, mobileTestUser1UserID, personID, new DateTime(2020, 5, 12, 9, 30, 0), null, 1, null, 60);
            Guid personBodyMapReview = PlatformServicesHelper.personBodyMapReview.CreatePersonBodyMapReview(mobileTeam1TeamID, personBodyMapID, personID, new DateTime(2020, 5, 13, 9, 0, 0), null, null, null, null, null, null);
            DateTime personBodyMapReviewCreatedOnDate = ((DateTime)PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByID(personBodyMapReview, "createdon")["createdon"]);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("12/05/2020 09:30");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnReviewRecord("Planned Review: 13/05/2020 09:00");

            personBodyMapReviewPopup
                .WaitForPersonBodyMapReviewPopupToLoad("PERSON BODY MAP REVIEW: Body Map Review for Maria Tsatsouline created on " + usersettings.ConvertTimeFromUtc(personBodyMapReviewCreatedOnDate).Value.ToString("dd'/'MM'/'yyyy HH:mm:ss"))
                .InsertDateTimeOfActualReview("13/05/2020", "10:00")
                .InsertProfessionalComments("professional comments ...")
                .InsertClientComments("client comments ...")
                .InsertAgreedOutcome("outcome ...")
                .TapIsReviewRequiredField();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(2)
                .TapOKButton();

            personBodyMapReviewPopup
                .WaitForPersonBodyMapReviewPopupToLoad("PERSON BODY MAP REVIEW: Body Map Review for Maria Tsatsouline created on " + usersettings.ConvertTimeFromUtc(personBodyMapReviewCreatedOnDate).Value.ToString("dd'/'MM'/'yyyy HH:mm:ss"))
                .TapSaveButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnSaveAndCloseButton();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad();


            var bodyMapFields = PlatformServicesHelper.personBodyMap.GetPersonBodyMapByID(personBodyMapID, "inactive");
            Assert.AreEqual(true, (bool)bodyMapFields["inactive"]);

            var reviewRecords = PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(personBodyMapID);
            Assert.AreEqual(1, reviewRecords.Count);

            var reviewFields = PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByID(reviewRecords[0], "plannedreviewdate", "actualreviewdate", "isreviewrequiredid", "nextreviewdate", "professionalcomments", "clientcomments", "agreedoutcome", "personid");


            Assert.AreEqual(new DateTime(2020, 5, 13, 9, 0, 0).ToString("dd'/'MM'/'yyyy HH:mm"), usersettings.ConvertTimeFromUtc((DateTime)reviewFields["plannedreviewdate"]).Value.ToString("dd'/'MM'/'yyyy HH:mm"));
            Assert.AreEqual(new DateTime(2020, 5, 13, 10, 0, 0).ToString("dd'/'MM'/'yyyy HH:mm"), usersettings.ConvertTimeFromUtc((DateTime)reviewFields["actualreviewdate"]).Value.ToString("dd'/'MM'/'yyyy HH:mm"));
            Assert.AreEqual(2, reviewFields["isreviewrequiredid"]);
            Assert.AreEqual(false, reviewFields.ContainsKey("nextreviewdate"));
            Assert.AreEqual("professional comments ...", (string)reviewFields["professionalcomments"]);
            Assert.AreEqual("client comments ...", (string)reviewFields["clientcomments"]);
            Assert.AreEqual("outcome ...", (string)reviewFields["agreedoutcome"]);
            Assert.AreEqual(personID, (Guid)reviewFields["personid"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6916")]
        [Description("UI Test for Dashboards - 0025 - " +
            "Navigate to the person body maps area - Open a person body map record (with a review records) - " +
            "Open the review record - Set Date/Time of Actual review, Is Review Required? = Yes,  Date/Time of Next Review and all comments fields - Save the record - " +
            "Validate that the review info is sync to the web database, the body map record is not disabled and a new review record is created")]
        public void PersonBodyMaps_TestMethod25()
        {
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            //Remove any person body map for the person
            foreach (Guid bodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(bodyMapID);
            }

            DateTime plannedReviewDate = new DateTime(2020, 5, 13, 9, 0, 0);
            Guid personBodyMapID = PlatformServicesHelper.personBodyMap.CreatePersonBodyMap(mobileTeam1TeamID, mobileTestUser1UserID, personID, new DateTime(2020, 5, 12, 9, 30, 0), null, 1, null, 60);
            Guid personBodyMapReview = PlatformServicesHelper.personBodyMapReview.CreatePersonBodyMapReview(mobileTeam1TeamID, personBodyMapID, personID, plannedReviewDate, null, null, null, null, null, null);
            DateTime personBodyMapReviewCreatedOnDate = ((DateTime)PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByID(personBodyMapReview, "createdon")["createdon"]);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("12/05/2020 09:30");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnReviewRecord("Planned Review: 13/05/2020 09:00");

            personBodyMapReviewPopup
                .WaitForPersonBodyMapReviewPopupToLoad("PERSON BODY MAP REVIEW: Body Map Review for Maria Tsatsouline created on " + usersettings.ConvertTimeFromUtc(personBodyMapReviewCreatedOnDate).Value.ToString("dd'/'MM'/'yyyy HH:mm:ss"))
                .InsertDateTimeOfActualReview("13/05/2020", "10:00")
                
                .InsertProfessionalComments("professional comments ...")
                .InsertClientComments("client comments ...")
                .InsertAgreedOutcome("outcome ...")
                .TapIsReviewRequiredField();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(1)
                .TapOKButton();

            personBodyMapReviewPopup
                .WaitForPersonBodyMapReviewPopupToLoad("PERSON BODY MAP REVIEW: Body Map Review for Maria Tsatsouline created on " + usersettings.ConvertTimeFromUtc(personBodyMapReviewCreatedOnDate).Value.ToString("dd'/'MM'/'yyyy HH:mm:ss"))
                .InsertDateTimeOfNextReview("14/05/2020", "10:00")
                .TapSaveButton();

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnSaveAndCloseButton();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad();


            var bodyMapFields = PlatformServicesHelper.personBodyMap.GetPersonBodyMapByID(personBodyMapID, "inactive");
            Assert.AreEqual(false, (bool)bodyMapFields["inactive"]);

            
            var reviewRecords = PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(personBodyMapID);
            Assert.AreEqual(2, reviewRecords.Count);


            var firstReviewRecord = PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(personBodyMapID, plannedReviewDate);
            Assert.AreEqual(1, firstReviewRecord.Count);

            var reviewFields = PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByID(firstReviewRecord[0], "plannedreviewdate", "actualreviewdate", "isreviewrequiredid", "nextreviewdate", "professionalcomments", "clientcomments", "agreedoutcome", "personid");


            
            Assert.AreEqual(plannedReviewDate.ToString("dd'/'MM'/'yyyy HH:mm"), usersettings.ConvertTimeFromUtc((DateTime)reviewFields["plannedreviewdate"]).Value.ToString("dd'/'MM'/'yyyy HH:mm"));
            Assert.AreEqual(new DateTime(2020, 5, 13, 10, 0, 0).ToString("dd'/'MM'/'yyyy HH:mm"), usersettings.ConvertTimeFromUtc((DateTime)reviewFields["actualreviewdate"]).Value.ToString("dd'/'MM'/'yyyy HH:mm"));
            Assert.AreEqual(1, reviewFields["isreviewrequiredid"]);
            Assert.AreEqual(new DateTime(2020, 5, 14, 10, 0, 0).ToString("dd'/'MM'/'yyyy HH:mm"), usersettings.ConvertTimeFromUtc((DateTime)reviewFields["nextreviewdate"]).Value.ToString("dd'/'MM'/'yyyy HH:mm"));
            Assert.AreEqual("professional comments ...", (string)reviewFields["professionalcomments"]);
            Assert.AreEqual("client comments ...", (string)reviewFields["clientcomments"]);
            Assert.AreEqual("outcome ...", (string)reviewFields["agreedoutcome"]);
            Assert.AreEqual(personID, (Guid)reviewFields["personid"]);



            var secondReviewRecord = PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(personBodyMapID, new DateTime(2020, 5, 14, 10, 0, 0));
            Assert.AreEqual(1, secondReviewRecord.Count);

            reviewFields = PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByID(secondReviewRecord[0], "plannedreviewdate", "actualreviewdate", "isreviewrequiredid", "nextreviewdate", "professionalcomments", "clientcomments", "agreedoutcome", "personid");
            Assert.AreEqual(new DateTime(2020, 5, 14, 10, 0, 0).ToString("dd'/'MM'/'yyyy HH:mm"), usersettings.ConvertTimeFromUtc((DateTime)reviewFields["plannedreviewdate"]).Value.ToString("dd'/'MM'/'yyyy HH:mm"));
            Assert.AreEqual(false, reviewFields.ContainsKey("actualreviewdate"));
            Assert.AreEqual(false, reviewFields.ContainsKey("isreviewrequiredid"));
            Assert.AreEqual(false, reviewFields.ContainsKey("nextreviewdate"));
            Assert.AreEqual(false, reviewFields.ContainsKey("professionalcomments"));
            Assert.AreEqual(false, reviewFields.ContainsKey("clientcomments"));
            Assert.AreEqual(false, reviewFields.ContainsKey("agreedoutcome"));
            Assert.AreEqual(personID, (Guid)reviewFields["personid"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6917")]
        [Description("UI Test for Dashboards - 0026 - " +
            "Navigate to the person body maps area - Open a person body map record (no review or injury records records) - " +
            "Tap in the delete button - Confirm the delete operation - Validate that body map record is deleted from the web database")]
        public void PersonBodyMaps_TestMethod26()
        {
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();

            //Remove any person body map for the person
            foreach (Guid bodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(bodyMapID);
            }

            Guid personBodyMapID = PlatformServicesHelper.personBodyMap.CreatePersonBodyMap(mobileTeam1TeamID, mobileTestUser1UserID, personID, new DateTime(2020, 5, 12, 9, 30, 0), null, 1, null, 60);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("12/05/2020 09:30");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnDeleteButton();

            warningPopup
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?")
                .TapOnYesButton();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad();


            var bodyMapRecords = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID);
            Assert.AreEqual(0, bodyMapRecords.Count);

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6918")]
        [Description("UI Test for Dashboards - 0027 - " +
            "Navigate to the person body maps area - Open a person body map record (One review record exists for the body map) - " +
            "Tap in the delete button - Confirm the delete operation - Validate that the user is prevented from saving the record")]
        public void PersonBodyMaps_TestMethod27()
        {
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();

            //Remove any person body map for the person
            foreach (Guid bodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(bodyMapID);
            }

            DateTime plannedReviewDate = new DateTime(2020, 5, 13, 9, 0, 0);
            Guid personBodyMapID = PlatformServicesHelper.personBodyMap.CreatePersonBodyMap(mobileTeam1TeamID, mobileTestUser1UserID, personID, new DateTime(2020, 5, 12, 9, 30, 0), null, 1, null, 60);
            PlatformServicesHelper.personBodyMapReview.CreatePersonBodyMapReview(mobileTeam1TeamID, personBodyMapID, personID, plannedReviewDate, null, null, null, null, null, null);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("12/05/2020 09:30");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnDeleteButton();

            warningPopup
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?")
                .TapOnYesButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Related record exists in Person Body Map Review. Please delete related records before deleting record in Person Body Map.")
                .TapOnOKButton();


            var bodyMapRecords = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID);
            Assert.AreEqual(1, bodyMapRecords.Count);

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6919")]
        [Description("UI Test for Dashboards - 0028 - " +
            "Navigate to the person body maps area - Open a person body map record (One injury record exists for the body map) - " +
            "Tap in the delete button - Confirm the delete operation - Validate that the user is prevented from saving the record")]
        public void PersonBodyMaps_TestMethod28()
        {
            Guid bodyMapSetupID = new Guid("E5C7F9CE-E7A9-E811-80DC-005056050630"); //Head-Back
            Guid mobileTeam1TeamID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid mobileTestUser1UserID = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //Mobile Test User 1
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline
            this.PlatformServicesHelper = new PlatformServicesHelper();

            //Remove any person body map for the person
            foreach (Guid bodyMapID in this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID))
            {
                foreach (Guid descriptionID in this.PlatformServicesHelper.personBodyMapInjuryDescription.GetPersonBodyMapInjuryDescriptionByPersonBodyMapID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapInjuryDescription.DeletePersonBodyMapInjuryDescription(descriptionID);

                foreach (Guid reviewid in this.PlatformServicesHelper.personBodyMapReview.GetPersonBodyMapReviewByPersonBodyMapIdID(bodyMapID))
                    this.PlatformServicesHelper.personBodyMapReview.DeletePersonBodyMapReview(reviewid);

                this.PlatformServicesHelper.personBodyMap.DeletePersonBodyMap(bodyMapID);
            }

            DateTime plannedReviewDate = new DateTime(2020, 5, 13, 9, 0, 0);
            Guid personBodyMapID = PlatformServicesHelper.personBodyMap.CreatePersonBodyMap(mobileTeam1TeamID, mobileTestUser1UserID, personID, new DateTime(2020, 5, 12, 9, 30, 0), null, 1, null, 60);
            PlatformServicesHelper.personBodyMapInjuryDescription.CreatePersonBodyMapInjuryDescription(mobileTeam1TeamID, personBodyMapID, personID, bodyMapSetupID, "Head-Back", "large cut");

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapBodyMapsIcon_RelatedItems();

            personBodyMapsPage
                .WaitForPersonBodyMapsPageToLoad()
                .TapOnRecord("12/05/2020 09:30");

            personBodyMapRecordPage
                .WaitForPersonBodyMapRecordPageToLoad("PERSON BODY MAP: Body Map for Maria Tsatsouline on 12/05/2020 09:30:00")
                .TapOnDeleteButton();

            warningPopup
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?")
                .TapOnYesButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "Related record exists in Body Map Injury Description. Please delete related records before deleting record in Person Body Map.")
                .TapOnOKButton();


            var bodyMapRecords = this.PlatformServicesHelper.personBodyMap.GetPersonBodyMapByPersonID(personID);
            Assert.AreEqual(1, bodyMapRecords.Count);

        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}
