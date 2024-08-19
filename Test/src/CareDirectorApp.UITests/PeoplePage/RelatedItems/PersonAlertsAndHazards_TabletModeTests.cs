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
    [Category("Mobile_TabletMode_Online")]
    public class PersonAlertsAndHazards_TabletModeTests : TestBase
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

            //if the cases Alerts And Hazard injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the cases Alerts And Hazard review pop-up is open then close it 
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

        #region person Alerts And Hazards page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6948")]
        [Description("UI Test for Dashboards - 0001 - " +
            "Navigate to the Person Alerts And Hazards area (do not contains Alerts And Hazard records) - Validate the page content")]
        public void PersonAlertsAndHazards_TestMethod01()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 

            //remove any Alerts And Hazard for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewid in this.PlatformServicesHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByPersonAlertHazardID(recordID))
                    this.PlatformServicesHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewid);

                this.PlatformServicesHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAlertAndHazardIcon_RelatedItems();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6949")]
        [Description("UI Test for Dashboards - 0002 - " +
            "Navigate to the Person Alerts And Hazards area (person contains Alerts And Hazard records) - Validate the page content")]
        public void PersonAlertsAndHazards_TestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid alertAndHazardID = new Guid("2343fe63-33a0-ea11-a2cd-005056926fe4");

            peoplePage
                .WaitForPeoplePageToLoad()
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
        [Property("JiraIssueID", "CDV6-6950")]
        [Description("UI Test for Dashboards - 0003 - " +
            "Navigate to the Person Alerts And Hazards area - Open a person Alerts And Hazard record - Validate that the Alerts And Hazard record page is displayed")]
        public void PersonAlertsAndHazards_TestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string alertAndHazardStartDate = "01/05/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERT AND HAZARD: Mr Pavel MCNamara Represents an Alert/Hazard Dangerous Dog");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6951")]
        [Description("UI Test for Dashboards - 0004 - " +
            "Navigate to the Person Alerts And Hazards area - Open a person Alerts And Hazard record - Validate that the Alerts And Hazard record page field titles are displayed")]
        public void PersonAlertsAndHazards_TestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string alertAndHazardStartDate = "01/05/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .ValidateDetailsFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6952")]
        [Description("UI Test for Dashboards - 0005 - " +
            "Navigate to the Person Alerts And Hazards area - Open a person Alerts And Hazard record - Validate that the Alerts And Hazard record page fields are correctly displayed")]
        public void PersonAlertsAndHazards_TestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string alertAndHazardStartDate = "01/05/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
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

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6953")]
        [Description("UI Test for Dashboards - 0006 - " +
            "Navigate to the Person Alerts And Hazards area - Open a person Alerts And Hazard record (with only the mandatory information set) - Validate that the Alerts And Hazard record page fields are correctly displayed")]
        public void PersonAlertsAndHazards_TestMethod06()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string alertAndHazardStartDate = "10/05/2020";

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .ValidatePersonFieldText("Pavel MCNamara")
                .ValidateRoleFieldText("Represents an Alert/Hazard")
                .ValidateAlertHazardTypeFieldText("Dangerous Dog")
                .ValidateStartDateFieldText("10/05/2020")
                .ValidateEndDateFieldText("")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateReviewFrequencyFieldText(" ")
                .ValidateAlertHazardEndDetailsFieldText("")
                .ValidateDetailsFieldText("");
        }

        #endregion

        #region New Record page - Validate content

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6954")]
        [Description("UI Test for Dashboards - 0007 - " +
            "Navigate to the Person Alerts And Hazards area - Tap on the add button - Validate that the new record page is displayed and all field titles are visible ")]
        public void PersonAlertsAndHazards_TestMethod07()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAlertAndHazardIcon_RelatedItems();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad()
                .TapOnAddNewRecordButton();

            personAlertsAndHazardRecordPage
                .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERTS AND HAZARDS")
                .ValidatePersonFieldTitleVisible(true)
                .ValidateRoleFieldTitleVisible(true)
                .ValidateAlertHazardTypeFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateReviewFrequencyFieldTitleVisible(true)
                .ValidateAlertHazardEndReasonFieldTitleVisible(true)
                .ValidateDetailsFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6955")]
        [Description("UI Test for Dashboards - 0008 - " +
            "Navigate to the Person Alerts And Hazards area - Tap on the add button - Validate that the new record page is displayed but the delete button is not displayed")]
        public void PersonAlertsAndHazards_TestMethod08()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAlertAndHazardIcon_RelatedItems();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad()
                .TapOnAddNewRecordButton();

            personAlertsAndHazardRecordPage
                .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERTS AND HAZARDS")
                .WaitForDeleteButtonNotVisible();
        }

        #endregion

        #region Create New Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6956")]
        [Description("UI Test for Dashboards - 0009 - " +
            "Navigate to the Person Alerts And Hazards area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void PersonAlertsAndHazards_TestMethod09()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 


            //remove any Alerts And Hazard for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewid in this.PlatformServicesHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByPersonAlertHazardID(recordID))
                    this.PlatformServicesHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewid);

                this.PlatformServicesHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
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
               .TapOnSaveButton()
               .WaitForPersonAlertsAndHazardRecordPageToLoad("PERSON ALERT AND HAZARD: Maria Tsatsouline Represents an Alert/Hazard Dangerous Dog");


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
        [Property("JiraIssueID", "CDV6-6957")]
        [Description("UI Test for Dashboards - 0010 - " +
            "Navigate to the Person Alerts And Hazards area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - Validate that the data is sync to the web platform")]
        public void PersonAlertsAndHazards_TestMethod10()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 


            //remove any Alerts And Hazard for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewid in this.PlatformServicesHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByPersonAlertHazardID(recordID))
                    this.PlatformServicesHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewid);

                this.PlatformServicesHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .WaitForPersonAlertsAndHazardsPageToLoad();


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
        [Property("JiraIssueID", "CDV6-6958")]
        [Description("UI Test for Dashboards - 0011 - " +
            "Navigate to the Person Alerts And Hazards area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - " +
            "Re-Open the record - Validate that all fields are correctly set after saving the record")]
        public void PersonAlertsAndHazards_TestMethod11()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 


            //remove any Alerts And Hazard for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewid in this.PlatformServicesHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByPersonAlertHazardID(recordID))
                    this.PlatformServicesHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewid);

                this.PlatformServicesHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .WaitForPersonAlertsAndHazardsPageToLoad();

            personAlertsAndHazardsPage
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
                .ValidateDetailsFieldText("hazard details");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6959")]
        [Description("UI Test for Dashboards - 0012 - " +
            "Navigate to the Person Alerts And Hazards area - Tap on the add button - Set data only in mandatory fields - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void PersonAlertsAndHazards_TestMethod12()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 


            //remove any Alerts And Hazard for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewid in this.PlatformServicesHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByPersonAlertHazardID(recordID))
                    this.PlatformServicesHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewid);

                this.PlatformServicesHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .TapOnSaveAndCloseButton();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad();


            var records = this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID);

            Assert.AreEqual(1, records.Count);

            var fields = this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(records[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "owningbusinessunitid", "title", "inactive", "personid", "alertandhazardtypeid", "startdate", "enddate", "roleid", "details", "reviewfrequencytypeid", "alertandhazardendreasonid");

            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid businessUnitID = new Guid("BA5B9FE6-419E-E911-A2C6-005056926FE4"); //Mobile Business Unit
            Guid alertandhazardtypeid = new Guid("95499DDB-D139-E911-A2C5-005056926FE4"); //Dangerous Dog
            DateTime startDate = new DateTime(2020, 5, 20);
            int roleid = 1; //Represents an Alert/Hazard

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
        [Property("JiraIssueID", "CDV6-6960")]
        [Description("UI Test for Dashboards - 0013 - " +
            "Navigate to the Person Alerts And Hazards area - Tap on the add button - Set data only in mandatory fields except for Role - " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void PersonAlertsAndHazards_TestMethod13()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 


            //remove any Alerts And Hazard for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewid in this.PlatformServicesHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByPersonAlertHazardID(recordID))
                    this.PlatformServicesHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewid);

                this.PlatformServicesHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
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

            var records = this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID);
            Assert.AreEqual(0, records.Count);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6961")]
        [Description("UI Test for Dashboards - 0014 - " +
            "Navigate to the Person Alerts And Hazards area - Tap on the add button - Set data only in mandatory fields except for Alert/Hazard Type - " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void PersonAlertsAndHazards_TestMethod14()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 


            //remove any Alerts And Hazard for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewid in this.PlatformServicesHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByPersonAlertHazardID(recordID))
                    this.PlatformServicesHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewid);

                this.PlatformServicesHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .InsertStartDate("20/05/2020")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Alert/Hazard Type' is required")
                .TapOnOKButton();

            var records = this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID);
            Assert.AreEqual(0, records.Count);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6962")]
        [Description("UI Test for Dashboards - 0022 - " +
            "Navigate to the Person Alerts And Hazards area - Tap on the add button - Set data only in mandatory fields except for Start Date - " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void PersonAlertsAndHazards_TestMethod22()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 


            //remove any Alerts And Hazard for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewid in this.PlatformServicesHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByPersonAlertHazardID(recordID))
                    this.PlatformServicesHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewid);

                this.PlatformServicesHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordID);
            }

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Start Date' is required")
                .TapOnOKButton();

            var records = this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID);
            Assert.AreEqual(0, records.Count);

        }

        #endregion

        #region Update Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6963")]
        [Description("UI Test for Dashboards - 0015 - Create a new person Alerts And Hazard using the main APP web services" +
            "Navigate to the Person Alerts And Hazards area - open the Alerts And Hazard record - validate that all fields are correctly synced")]
        public void PersonAlertsAndHazards_TestMethod15()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 
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

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapAlertAndHazardIcon_RelatedItems();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad()
                .TapOnRecord("Dangerous Dog");

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
                .ValidateDetailsFieldText("hazard description");

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6964")]
        [Description("UI Test for Dashboards - 0016 - Create a new person Alerts And Hazard using the main APP web services" +
            "Navigate to the Person Alerts And Hazards area - open the Alerts And Hazard record - clear all non mandatory fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonAlertsAndHazards_TestMethod16()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 
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

            peoplePage
                .WaitForPeoplePageToLoad()
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
        [Property("JiraIssueID", "CDV6-6965")]
        [Description("UI Test for Dashboards - 0017 - Create a new person Alerts And Hazard using the main APP web services" +
            "Navigate to the Person Alerts And Hazards area - open the Alerts And Hazard record - update all fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonAlertsAndHazards_TestMethod17()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 
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


            peoplePage
                .WaitForPeoplePageToLoad()
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

            pickList.ScrollUpPicklist(1).TapOKButton();

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


           
            var fields = this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(PersonAlertsAndHazardID, "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "owningbusinessunitid", "title", "inactive", "personid", "alertandhazardtypeid", "startdate", "enddate", "roleid", "details", "reviewfrequencytypeid", "alertandhazardendreasonid");

           
            alertandhazardtypeid = new Guid("21A0E9EB-D139-E911-A2C5-005056926FE4"); //Lives in a dangerous environment
            startDate = new DateTime(2020, 5, 21);
            roleid = 2; //Exposed to Alert/Hazard
            reviewfrequencytypeid = 3; //Monthly
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

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6966")]
        [Description("UI Test for Dashboards - 0018 - Create a new person Alerts And Hazard using the main APP web services" +
            "Navigate to the Person Alerts And Hazards area - open the Alerts And Hazard record - Set an end date - Save and close the record - Validate that the record is no longer visible")]
        public void PersonAlertsAndHazards_TestMethod18()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 
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


            peoplePage
                .WaitForPeoplePageToLoad()
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
                .InsertEndDate("21/05/2020")
                .TapOnSaveAndCloseButton();

            personAlertsAndHazardsPage
                .WaitForPersonAlertsAndHazardsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);

            var fields = this.PlatformServicesHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(PersonAlertsAndHazardID, "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "owningbusinessunitid", "title", "inactive", "personid", "alertandhazardtypeid", "startdate", "enddate", "roleid", "details", "reviewfrequencytypeid", "alertandhazardendreasonid");

            DateTime endDate = new DateTime(2020, 5, 21);


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
            Assert.AreEqual(endDate, (DateTime)fields["enddate"]);
            Assert.AreEqual(roleid, (int)fields["roleid"]);
            Assert.AreEqual("hazard description", (string)fields["details"]);
            Assert.AreEqual(reviewfrequencytypeid, (int)fields["reviewfrequencytypeid"]);
            Assert.AreEqual(alertandhazardendreasonid, (Guid)fields["alertandhazardendreasonid"]);
        }



        #endregion

        #region Delete record



        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}
