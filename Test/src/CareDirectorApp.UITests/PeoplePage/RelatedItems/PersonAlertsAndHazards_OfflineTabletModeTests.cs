using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.RelatedItems
{
    /// <summary>
    /// This class contains all test methods for cases Person Alerts And Hazards validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class PersonAlertsAndHazards_OfflineTabletModeTests : TestBase
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

        #region person Alerts And Hazards page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6942")]
        [Description("UI Test for Dashboards - 0002 - " +
            "Navigate to the Person Alerts And Hazards area (person contains Alerts And Hazard records) - Validate the page content")]
        public void PersonAlertsAndHazards_OfflineTestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid alertAndHazardID = new Guid("2343fe63-33a0-ea11-a2cd-005056926fe4");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAlertAndHazardIcon_RelatedItems();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad()
                .ValidateAlertHazardTypeCellText("Dangerous Dog", alertAndHazardID.ToString())
                .ValidateRoleCellText("Represents an Alert/Hazard", alertAndHazardID.ToString())
                .ValidateStartDateCellText("01/05/2020", alertAndHazardID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", alertAndHazardID.ToString())
                .ValidateCreatedOnCellText("27/05/2020 17:02", alertAndHazardID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", alertAndHazardID.ToString())
                .ValidateModifiedOnCellText("27/05/2020 17:03", alertAndHazardID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6943")]
        [Description("UI Test for Dashboards - 0004 - " +
            "Navigate to the Person Alerts And Hazards area - Open a person Alerts And Hazard record - Validate that the Alerts And Hazard record page fields and titles are displayed")]
        public void PersonAlertsAndHazards_OfflineTestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string alertAndHazardStartDate = "01/05/2020";

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAlertAndHazardIcon_RelatedItems();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad()
                .TapOnRecord(alertAndHazardStartDate);

            personAlertsAndHazardRecordPage
                .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERT AND HAZARD: Mr Pavel MCNamara Represents an Alert/Hazard Dangerous Dog")
                .ValidatePersonFieldTitleVisible(true)
                .ValidateRoleFieldTitleVisible(true)
                .ValidateAlertHazardTypeFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateReviewFrequencyFieldTitleVisible(true)
                .ValidateAlertHazardEndReasonFieldTitleVisible(true)
                .ValidateDetailsFieldTitleVisible(true)

                .ValidatePersonFieldText("Pavel MCNamara")
                .ValidateRoleFieldText("Represents an Alert/Hazard")
                .ValidateAlertHazardTypeFieldText("Dangerous Dog")
                .ValidateStartDateFieldText("01/05/2020")
                .ValidateEndDateFieldText("")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateReviewFrequencyFieldText("Monthly")
                .ValidateAlertHazardEndDetailsFieldText("End Reason_1")
                .ValidateDetailsFieldText("hazzard details");
        }


        #endregion

        #region Create New Record


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6944")]
        [Description("UI Test for Dashboards - 0010 - " +
            "Navigate to the Person Alerts And Hazards area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - Validate that the data is sync to the web platform")]
        public void PersonAlertsAndHazards_OfflineTestMethod10()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any Alerts And Hazard for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewid in this.PlatformServicesHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByPersonAlertHazardID(recordID))
                    this.PlatformServicesHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewid);

                this.PlatformServicesHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordID);
            }

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAlertAndHazardIcon_RelatedItems();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad()
                .TapOnAddNewRecordButton();

            personAlertsAndHazardRecordPage
                .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERTS AND HAZARDS")
                .TapRoleField();

            pickList.ScrollUpPicklist(1).TapOKButton();

            personAlertsAndHazardRecordPage
               .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERTS AND HAZARDS")
               .TapAlertHazardTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Dangerous Dog");

            personAlertsAndHazardRecordPage
              .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERTS AND HAZARDS")
              .InsertStartDate("20/05/2020")
              .TapReviewFrequencyField();

            pickList.ScrollUpPicklist(2).TapOKButton();

            personAlertsAndHazardRecordPage
            .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERTS AND HAZARDS")
            .TapAlertHazardEndReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("End Reason_1");

            personAlertsAndHazardRecordPage
               .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERTS AND HAZARDS")
               .InsertDatails("hazard details")
               .TapOnSaveAndCloseButton();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad()
                .TapOnRecord("20/05/2020");

            personAlertsAndHazardRecordPage
                .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERT AND HAZARD: Maria Tsatsouline Represents an Alert/Hazard Dangerous Dog")
                .ValidatePersonFieldText("Maria Tsatsouline")
                .ValidateRoleFieldText("Represents an Alert/Hazard")
                .ValidateAlertHazardTypeFieldText("Dangerous Dog")
                .ValidateStartDateFieldText("20/05/2020")
                .ValidateEndDateFieldText("")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateReviewFrequencyFieldText("Weekly")
                .ValidateAlertHazardEndDetailsFieldText("End Reason_1")
                .ValidateDetailsFieldText("hazard details"); ;

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            var records = this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID);
            Assert.AreEqual(1, records.Count);
            var fields = this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(records[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "owningbusinessunitid", "title", "inactive", "personid", "alertandhazardtypeid", "startdate", "enddate", "roleid", "details", "reviewfrequencytypeid", "alertandhazardendreasonid");

            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid businessUnitID = new Guid("BA5B9FE6-419E-E911-A2C6-005056926FE4"); //Mobile Business Unit
            Guid alertandhazardtypeid = new Guid("95499DDB-D139-E911-A2C5-005056926FE4"); //Dangerous Dog
            DateTime startDate = new DateTime(2020, 5, 20);
            int roleid = 1; //Represents an Alert/Hazard
            int reviewfrequencytypeid = 2; //weekly
            Guid alertandhazardendreasonid = new Guid("2F979679-5097-E911-A2C6-005056926FE4"); //End Reason_1


            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(businessUnitID, (Guid)fields["owningbusinessunitid"]);
            Assert.AreEqual("Maria Tsatsouline Represents an Alert/Hazard Dangerous Dog", (string)fields["title"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(alertandhazardtypeid, (Guid)fields["alertandhazardtypeid"]);
            Assert.AreEqual(startDate, (DateTime)fields["startdate"]);
            Assert.IsFalse(fields.ContainsKey("enddate"));
            Assert.AreEqual(roleid, (int)fields["roleid"]);
            Assert.AreEqual("hazard details", (string)fields["details"]);
            Assert.AreEqual(reviewfrequencytypeid, (int)fields["reviewfrequencytypeid"]);
            Assert.AreEqual(alertandhazardendreasonid, (Guid)fields["alertandhazardendreasonid"]);

        }

        

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6945")]
        [Description("UI Test for Dashboards - 0013 - " +
            "Navigate to the Person Alerts And Hazards area - Tap on the add button - Set data only in mandatory fields except for Role - " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void PersonAlertsAndHazards_OfflineTestMethod13()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Alerts And Hazard for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewid in this.PlatformServicesHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByPersonAlertHazardID(recordID))
                    this.PlatformServicesHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewid);

                this.PlatformServicesHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordID);
            }

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAlertAndHazardIcon_RelatedItems();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad()
                .TapOnAddNewRecordButton();

            personAlertsAndHazardRecordPage
                .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERTS AND HAZARDS")
                .TapAlertHazardTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Dangerous Dog");

            personAlertsAndHazardRecordPage
                .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERTS AND HAZARDS")
                .InsertStartDate("20/05/2020")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Role' is required")
                .TapOnOKButton();

            

        }


        #endregion

        #region Update Record


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6946")]
        [Description("UI Test for Dashboards - 0016 - Create a new person Alerts And Hazard using the main APP web services" +
            "Navigate to the Person Alerts And Hazards area - open the Alerts And Hazard record - clear all non mandatory fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonAlertsAndHazards_OfflineTestMethod16()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid businessUnitID = new Guid("BA5B9FE6-419E-E911-A2C6-005056926FE4"); //Mobile Business Unit
            Guid alertandhazardtypeid = new Guid("95499DDB-D139-E911-A2C5-005056926FE4"); //Dangerous Dog
            DateTime startDate = new DateTime(2020, 5, 20);
            int roleid = 1; //Represents an Alert/Hazard
            int reviewfrequencytypeid = 2; //weekly
            Guid alertandhazardendreasonid = new Guid("2F979679-5097-E911-A2C6-005056926FE4"); //End Reason_1


            //remove any Alerts And Hazard for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewid in this.PlatformServicesHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByPersonAlertHazardID(recordID))
                    this.PlatformServicesHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewid);

                this.PlatformServicesHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordID);
            }

            Guid PersonAlertsAndHazardID = this.PlatformServicesHelper.personAlertAndHazard.CreatePersonAlertAndHazard(roleid, "hazard description", mobileTeam1, "Mobile Team 1", personID, "Maria Tsatsouline", alertandhazardtypeid, "Dangerous Dog", alertandhazardendreasonid, "End Reason_1", startDate, null, reviewfrequencytypeid);



            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAlertAndHazardIcon_RelatedItems();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad()
                .TapOnRecord("20/05/2020");

            personAlertsAndHazardRecordPage
                .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERT AND HAZARD: Maria Tsatsouline Represents an Alert/Hazard Dangerous Dog")
                .TapReviewFrequencyField();

            pickList.ScrollDownPicklist(2).TapOKButton();

            personAlertsAndHazardRecordPage
            .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERT AND HAZARD: Maria Tsatsouline Represents an Alert/Hazard Dangerous Dog")
                .TapAlertHazardEndReasonRemoveButton()
                .InsertDatails("")
                .TapOnSaveAndCloseButton();

            personAlertsAndHazardsPage
              .WaitForPersonAlertsAndHazardsPageToLoad();


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            var fields = this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(PersonAlertsAndHazardID, "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "owningbusinessunitid", "title", "inactive", "personid", "alertandhazardtypeid", "startdate", "enddate", "roleid", "details", "reviewfrequencytypeid", "alertandhazardendreasonid");

            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(businessUnitID, (Guid)fields["owningbusinessunitid"]);
            Assert.AreEqual("Maria Tsatsouline Represents an Alert/Hazard Dangerous Dog", (string)fields["title"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(alertandhazardtypeid, (Guid)fields["alertandhazardtypeid"]);
            Assert.AreEqual(startDate, (DateTime)fields["startdate"]);
            Assert.IsFalse(fields.ContainsKey("enddate"));
            Assert.AreEqual(roleid, (int)fields["roleid"]);
            Assert.IsFalse(fields.ContainsKey("details"));
            Assert.IsFalse(fields.ContainsKey("reviewfrequencytypeid"));
            Assert.IsFalse(fields.ContainsKey("alertandhazardendreasonid"));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6947")]
        [Description("UI Test for Dashboards - 0017 - Create a new person Alerts And Hazard using the main APP web services" +
            "Navigate to the Person Alerts And Hazards area - open the Alerts And Hazard record - update all fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonAlertsAndHazards_OfflineTestMethod17()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid businessUnitID = new Guid("BA5B9FE6-419E-E911-A2C6-005056926FE4"); //Mobile Business Unit
            Guid alertandhazardtypeid = new Guid("95499DDB-D139-E911-A2C5-005056926FE4"); //Dangerous Dog
            DateTime startDate = new DateTime(2020, 5, 20);
            int roleid = 1; //Represents an Alert/Hazard
            int reviewfrequencytypeid = 2; //weekly
            Guid alertandhazardendreasonid = new Guid("2F979679-5097-E911-A2C6-005056926FE4"); //End Reason_1


            //remove any Alerts And Hazard for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewid in this.PlatformServicesHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByPersonAlertHazardID(recordID))
                    this.PlatformServicesHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewid);

                this.PlatformServicesHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordID);
            }

            Guid PersonAlertsAndHazardID = this.PlatformServicesHelper.personAlertAndHazard.CreatePersonAlertAndHazard(roleid, "hazard description", mobileTeam1, "Mobile Team 1", personID, "Maria Tsatsouline", alertandhazardtypeid, "Dangerous Dog", alertandhazardendreasonid, "End Reason_1", startDate, null, reviewfrequencytypeid);



            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAlertAndHazardIcon_RelatedItems();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad()
                .TapOnRecord("20/05/2020");

            personAlertsAndHazardRecordPage
                .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERT AND HAZARD: Maria Tsatsouline Represents an Alert/Hazard Dangerous Dog")
                .TapRoleField();

            pickList.ScrollUpPicklist(1).TapOKButton();

            personAlertsAndHazardRecordPage
               .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERT AND HAZARD: Maria Tsatsouline Represents an Alert/Hazard Dangerous Dog")
               .TapAlertHazardTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Lives in a dangerous environment");

            personAlertsAndHazardRecordPage
              .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERT AND HAZARD: Maria Tsatsouline Represents an Alert/Hazard Dangerous Dog")
              .InsertStartDate("21/05/2020")
              .TapReviewFrequencyField();

            pickList.ScrollUpPicklist(2).TapOKButton();

            personAlertsAndHazardRecordPage
            .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERT AND HAZARD: Maria Tsatsouline Represents an Alert/Hazard Dangerous Dog")
            .TapAlertHazardEndReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("End Reason_2");

            personAlertsAndHazardRecordPage
               .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERT AND HAZARD: Maria Tsatsouline Represents an Alert/Hazard Dangerous Dog")
               .InsertDatails("hazard details updated")
               .TapOnSaveAndCloseButton();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad();


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            var fields = this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(PersonAlertsAndHazardID, "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "owningbusinessunitid", "title", "inactive", "personid", "alertandhazardtypeid", "startdate", "enddate", "roleid", "details", "reviewfrequencytypeid", "alertandhazardendreasonid");
            alertandhazardtypeid = new Guid("21A0E9EB-D139-E911-A2C5-005056926FE4"); //Lives in a dangerous environment
            startDate = new DateTime(2020, 5, 21);
            roleid = 2; //Exposed to Alert/Hazard
            reviewfrequencytypeid = 4; //Quarterly
            alertandhazardendreasonid = new Guid("F316B480-5097-E911-A2C6-005056926FE4"); //End Reason_2

            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(businessUnitID, (Guid)fields["owningbusinessunitid"]);
            Assert.AreEqual("Maria Tsatsouline Exposed to Alert/Hazard Lives in a dangerous environment", (string)fields["title"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(alertandhazardtypeid, (Guid)fields["alertandhazardtypeid"]);
            Assert.AreEqual(startDate, (DateTime)fields["startdate"]);
            Assert.IsFalse(fields.ContainsKey("enddate"));
            Assert.AreEqual(roleid, (int)fields["roleid"]);
            Assert.AreEqual("hazard details updated", (string)fields["details"]);
            Assert.AreEqual(reviewfrequencytypeid, (int)fields["reviewfrequencytypeid"]);
            Assert.AreEqual(alertandhazardendreasonid, (Guid)fields["alertandhazardendreasonid"]);
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
