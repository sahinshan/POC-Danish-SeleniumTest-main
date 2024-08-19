using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;
using System.Collections.Generic;

namespace CareDirectorApp.UITests.People.Finance
{
    /// <summary>
    /// https://advancedcsg.atlassian.net/browse/CDV6-5538
    /// 
    /// Tests for the activation and deactivation of the finance business module 
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class PersonFinancialAssessments_OfflineTabletModeTests : TestBase
    {
        static UIHelper uIHelper;

        internal Guid CareDirectorApp_ApplicationID { get; set; }
        internal Guid FinancialAssessment_BusinessModuleID { get; set; }
        internal Guid ApplicationLinkedBusinessModule { get; set; }

        internal Guid Mobile_test_user_1_userid { get; set; }
        internal List<Guid> UserDevices { get; set; }


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

            //this._app.Repl();

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

            ///close the lookup popup if it is open
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

            //if the APP is in off-line mode change it to on-line mode
            settingsPage.SetTheAppInOnlineMode();

        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-5537

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5537" +
            "Open a person record - navigate to the person Financial Assessments page - Validate that the page is displayed")]
        [Property("JiraIssueID", "CDV6-6726")]
        public void PersonFinancialAssessments_OfflineTestMethod01()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad();
        }


        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5537" +
            "Open a person record (person has no person Financial Assessments records) - Navigate to the person Financial Assessments page - " +
            "Validate that the no records message is present")]
        [Property("JiraIssueID", "CDV6-6727")]
        public void PersonFinancialAssessments_OfflineTestMethod02()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(pfdID);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5537" +
            "Open a person record (person has Person Financial Assessments record) - Navigate to the person Financial Assessments page - " +
            "Validate that the record is displayed")]
        [Property("JiraIssueID", "CDV6-6728")]
        public void PersonFinancialAssessments_OfflineTestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid financialAssessment1 = new Guid("b6493f29-872d-eb11-a2ce-005056926fe4"); //204704
            Guid financialAssessment2 = new Guid("5362d7c7-a92d-eb11-a2ce-005056926fe4"); //204705

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()

                .ValidateIDCell("204704", financialAssessment1.ToString())
                .ValidateFinancialAssessmentStatusCell("Draft", financialAssessment1.ToString())
                .ValidateCalculationRequiredCell("Yes", financialAssessment1.ToString())
                .ValidateStartDateCell("02/11/2020", financialAssessment1.ToString())
                .ValidateEndDateCell("08/11/2020", financialAssessment1.ToString())
                .ValidateChargingRuleCell("Residential Permanent Stay", financialAssessment1.ToString())
                .ValidateFinancialAssessmentTypeCell("Draft Assessment", financialAssessment1.ToString())
                .ValidateIncomeSupportTypeCell("Appropriate Min Guarantee (Sev Dis [1])", financialAssessment1.ToString())
                .ValidateIncomeSupportValueCell("£129", financialAssessment1.ToString())
                .ValidateFinancialAssessmentCategoryCell("Single", financialAssessment1.ToString())
                .ValidateDaysPropertyDisregardedCell("0", financialAssessment1.ToString())
                .ValidateDeferredPaymentSchemeCell("No", financialAssessment1.ToString())
                .ValidateDeferredPaymentSchemaTypeCell("", financialAssessment1.ToString())
                .ValidatePermitChargeUpdatesViaFinancialAssessmentCell("Yes", financialAssessment1.ToString())
                .ValidatePermitChargeUpdatesViaRecalculationCell("Yes", financialAssessment1.ToString())
                .ValidateServiceProvisionAssociatedCell("No", financialAssessment1.ToString())

                .ValidateIDCell("204705", financialAssessment2.ToString())
                .ValidateFinancialAssessmentStatusCell("Draft", financialAssessment2.ToString())
                .ValidateCalculationRequiredCell("No", financialAssessment2.ToString())
                .ValidateStartDateCell("09/11/2020", financialAssessment2.ToString())
                .ValidateEndDateCell("15/11/2020", financialAssessment2.ToString())
                .ValidateChargingRuleCell("Non Residential", financialAssessment2.ToString())
                .ValidateFinancialAssessmentTypeCell("Draft Assessment", financialAssessment2.ToString())
                .ValidateIncomeSupportTypeCell("IS (Disability + SDP [1] + EDP) 25-PA", financialAssessment2.ToString())
                .ValidateIncomeSupportValueCell("", financialAssessment2.ToString())
                .ValidateFinancialAssessmentCategoryCell("Single", financialAssessment2.ToString())
                .ValidateDaysPropertyDisregardedCell("0", financialAssessment2.ToString())
                .ValidateDeferredPaymentSchemeCell("No", financialAssessment2.ToString())
                .ValidateDeferredPaymentSchemaTypeCell("", financialAssessment2.ToString())
                .ValidatePermitChargeUpdatesViaFinancialAssessmentCell("Yes", financialAssessment2.ToString())
                .ValidatePermitChargeUpdatesViaRecalculationCell("Yes", financialAssessment2.ToString())
                .ValidateServiceProvisionAssociatedCell("No", financialAssessment2.ToString())

                ;
        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-5538

        #region Open Existing Record

        [Test]
        [Property("JiraIssueID", "CDV6-6729")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - Navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record (with editable Income Support Value) - Validate that the record is correctly displayed.")]
        public void PersonFinancialAssessments_OfflineTestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid financialAssessment1 = new Guid("b6493f29-872d-eb11-a2ce-005056926fe4"); //204704

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(financialAssessment1.ToString());

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Pavel MCNamara \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .ValidateIdFieldTitleVisible(true)
                .ValidateFinancialAssessmentStatusFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)

                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)

                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateChargingRuleFieldTitleVisible(true)
                .ValidateIncomeSupportTypeFieldTitleVisible(true)
                .ValidateFinancialAssessmentTypeFieldTitleVisible(true)
                .ValidateDaysPropertyDisregardedFieldTitleVisible(true)
                .ValidateIncomeSupportValueFieldTitleVisible(true)

                .ValidateCommencementDateFieldTitleVisible(true)
                .ValidatePermitChargeUpdatesViaFinancialAssessmentFieldTitleVisible(true)
                .ValidatePermitChargeUpdatesViaRecalculationFieldTitleVisible(true)

                .ValidateNoteTextFieldTitleVisible(true)

                .ValidateIdFieldText("204704")
                .ValidateFinancialAssessmentStatusFieldText("Draft")
                .ValidateResponsibleUserFieldText("CW Forms Test User 1")
                .ValidateResponsibleTeamFieldText("CareDirector QA")

                .ValidateStartDateFieldText("02/11/2020")
                .ValidateEndDateFieldText("08/11/2020")

                .ValidateChargingRuleFieldText("Residential Permanent Stay")
                .ValidateIncomeSupportTypeFieldText("Appropriate Min Guarantee (Sev Dis [1])")
                .ValidateFinancialAssessmentTypeFieldText("Draft Assessment")
                .ValidateDaysPropertyDisregardedFieldText("0")
                .ValidateIncomeSupportValueEditableFieldText("240.7")

                .ValidateCommencementDateFieldText("01/11/2020")
                .ValidatePermitChargeUpdatesViaFinancialAssessmentFieldText("Yes")
                .ValidatePermitChargeUpdatesViaRecalculationFieldText("Yes")

                .ValidateNoteTextEditFieldText("Line 1\nLine 2")
                ;

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6730")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - Navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record (with non-editable Income Support Value) - Validate that the record is correctly displayed.")]
        public void PersonFinancialAssessments_OfflineTestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid financialAssessment1 = new Guid("5362d7c7-a92d-eb11-a2ce-005056926fe4"); //204705

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(financialAssessment1.ToString());

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Pavel MCNamara \\ Non Residential \\ 09/11/2020 \\ 15/11/2020")
                .ValidateIdFieldTitleVisible(true)
                .ValidateFinancialAssessmentStatusFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)

                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)

                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateChargingRuleFieldTitleVisible(true)
                .ValidateIncomeSupportTypeFieldTitleVisible(true)
                .ValidateFinancialAssessmentTypeFieldTitleVisible(true)
                .ValidateDaysPropertyDisregardedFieldTitleVisible(true)
                .ValidateIncomeSupportValueFieldTitleVisible(true)

                .ValidateCommencementDateFieldTitleVisible(true)
                .ValidatePermitChargeUpdatesViaFinancialAssessmentFieldTitleVisible(true)
                .ValidatePermitChargeUpdatesViaRecalculationFieldTitleVisible(true)

                .ValidateNoteTextFieldTitleVisible(true)

                .ValidateIdFieldText("204705")
                .ValidateFinancialAssessmentStatusFieldText("Draft")
                .ValidateResponsibleUserFieldText("CW Forms Test User 1")
                .ValidateResponsibleTeamFieldText("CareDirector QA")

                .ValidateStartDateFieldText("09/11/2020")
                .ValidateEndDateFieldText("15/11/2020")

                .ValidateChargingRuleFieldText("Non Residential")
                .ValidateIncomeSupportTypeFieldText("IS (Disability + SDP [1] + EDP) 25-PA")
                .ValidateFinancialAssessmentTypeFieldText("Draft Assessment")
                .ValidateDaysPropertyDisregardedFieldText("0")
                .ValidateIncomeSupportValueReadOnlyFieldText("190.1")

                .ValidateCommencementDateFieldText("09/11/2020")
                .ValidatePermitChargeUpdatesViaFinancialAssessmentFieldText("Yes")
                .ValidatePermitChargeUpdatesViaRecalculationFieldText("Yes")

                .ValidateNoteTextEditFieldText("Line 1\nLine 2")
                ;
        }

        #endregion

        #region Update Records

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Update the Income Support Type - Validate that the Income Support Value field is automatically updated")]
        [Property("JiraIssueID", "CDV6-6731")]
        public void PersonFinancialAssessments_OfflineTestMethod06()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid ResponsibleUserId = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //mobile test user 1
            Guid FinancialAssessmentStatusId = new Guid("8a6505d2-b286-e911-9afd-d89ef34c460f"); //Draft
            Guid ChargingRuleId = new Guid("ba86fe23-db1d-ea11-a2c8-005056926fe4"); //Residential Permanent Stay
            Guid IncomeSupportTypeId = new Guid("395a5907-d74b-e911-a2c4-0050569231cf"); //ESA - Assessment Phase (Over 25) + EDP + SDP[1] + Carers [1]
            Guid FinancialAssessmentTypeId = new Guid("b9d3f401-6788-e911-9bfe-1803731f3ee3"); //Full Assessment
            DateTime StartDate = new DateTime(2020, 11, 2);
            DateTime EndDate = new DateTime(2020, 11, 8);
            DateTime CommencementDate = new DateTime(2020, 11, 2);
            decimal IncomeSupportValue = 192.6M;
            string Notes = "financial assessment notes...";
            int FinancialAssessmentCategoryId = 1;
            int DaysPropertyDisregarded = 1;
            bool CalculationRequired = false;
            bool DeferredPaymentScheme = false;
            bool OverrideDefaultDeferredAmount = false;
            bool CalculateInterest = true;
            bool RecordedInError = false;
            bool HasServiceProvisionAssociated = false;
            bool PermitChargeUpdatesByFinancialAssessment = true;
            bool PermitChargeUpdatesByRecalculation = true;


            var financialAssessment1 = PlatformServicesHelper.financialAssessment.CreateFinancialAssessment(personID, ownerid,
                ResponsibleUserId, FinancialAssessmentStatusId, ChargingRuleId, IncomeSupportTypeId,
                FinancialAssessmentTypeId, StartDate, EndDate, CommencementDate, IncomeSupportValue, Notes, FinancialAssessmentCategoryId,
                DaysPropertyDisregarded, CalculationRequired, DeferredPaymentScheme, OverrideDefaultDeferredAmount, CalculateInterest,
                RecordedInError, HasServiceProvisionAssociated, PermitChargeUpdatesByFinancialAssessment, PermitChargeUpdatesByRecalculation);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(financialAssessment1.ToString());

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapIncomeSupportTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("ESA - Assessment Phase (Over 25) + EDP + SDP").TapSearchButtonQuery().TapOnRecord("Code: 80");

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .ValidateIncomeSupportTypeFieldText("ESA - Assessment Phase (Over 25) + EDP + SDP[1]")
                .ValidateIncomeSupportValueReadOnlyFieldText("155.75");
        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Update all editable fields - Save the record - Validate that the changes are saved in the database")]
        [Property("JiraIssueID", "CDV6-6732")]
        public void PersonFinancialAssessments_OfflineTestMethod07()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid ResponsibleUserId = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //mobile test user 1
            Guid FinancialAssessmentStatusId = new Guid("8a6505d2-b286-e911-9afd-d89ef34c460f"); //Draft
            Guid ChargingRuleId = new Guid("ba86fe23-db1d-ea11-a2c8-005056926fe4"); //Residential Permanent Stay
            Guid IncomeSupportTypeId = new Guid("395a5907-d74b-e911-a2c4-0050569231cf"); //ESA - Assessment Phase (Over 25) + EDP + SDP[1] + Carers [1]
            Guid FinancialAssessmentTypeId = new Guid("b9d3f401-6788-e911-9bfe-1803731f3ee3"); //Full Assessment
            DateTime StartDate = new DateTime(2020, 11, 2);
            DateTime EndDate = new DateTime(2020, 11, 8);
            DateTime CommencementDate = new DateTime(2020, 11, 2);
            decimal IncomeSupportValue = 192.6M;
            string Notes = "financial assessment notes ...";
            int FinancialAssessmentCategoryId = 1;
            int DaysPropertyDisregarded = 1;
            bool CalculationRequired = false;
            bool DeferredPaymentScheme = false;
            bool OverrideDefaultDeferredAmount = false;
            bool CalculateInterest = true;
            bool RecordedInError = false;
            bool HasServiceProvisionAssociated = false;
            bool PermitChargeUpdatesByFinancialAssessment = true;
            bool PermitChargeUpdatesByRecalculation = true;


            var financialAssessment1 = PlatformServicesHelper.financialAssessment.CreateFinancialAssessment(personID, ownerid,
                ResponsibleUserId, FinancialAssessmentStatusId, ChargingRuleId, IncomeSupportTypeId,
                FinancialAssessmentTypeId, StartDate, EndDate, CommencementDate, IncomeSupportValue, Notes, FinancialAssessmentCategoryId,
                DaysPropertyDisregarded, CalculationRequired, DeferredPaymentScheme, OverrideDefaultDeferredAmount, CalculateInterest,
                RecordedInError, HasServiceProvisionAssociated, PermitChargeUpdatesByFinancialAssessment, PermitChargeUpdatesByRecalculation);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(financialAssessment1.ToString());

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .InsertStartDate("03/11/2020")
                .InsertEndDate("09/11/2020")
                .TapChargingRuleLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Resid").TapSearchButtonQuery().TapOnRecord("Name: Residential");

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapIncomeSupportTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("ESA - Assessment Phase (Over 25) + EDP + SDP").TapSearchButtonQuery().TapOnRecord("Code: 80");

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .InsertDaysPropertyDisregarded("2")
                .TapPermitChargeUpdatesViaFinancialAssessmentField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();


            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapPermitChargeUpdatesViaRecalculationField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();


            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .InsertNotes("financial assessment notes updated ...")
                .TapOnSaveAndCloseButton();


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(financialAssessment1.ToString());

            personFinancialAssessmentRecordPage
               .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential \\ 03/11/2020 \\ 09/11/2020")
               .ValidateFinancialAssessmentStatusFieldText("Draft")
               .ValidateResponsibleUserFieldText("Mobile Test User 1")
               .ValidateResponsibleTeamFieldText("CareDirector QA")

               .ValidateStartDateFieldText("03/11/2020")
               .ValidateEndDateFieldText("09/11/2020")

               .ValidateChargingRuleFieldText("Residential")
               .ValidateIncomeSupportTypeFieldText("ESA - Assessment Phase (Over 25) + EDP + SDP[1]")
               .ValidateFinancialAssessmentTypeFieldText("Full Assessment")
               .ValidateDaysPropertyDisregardedFieldText("2")
               .ValidateIncomeSupportValueReadOnlyFieldText("155.75")

               .ValidateCommencementDateFieldText("02/11/2020")
               .ValidatePermitChargeUpdatesViaFinancialAssessmentFieldText("No")
               .ValidatePermitChargeUpdatesViaRecalculationFieldText("No")

               .ValidateNoteTextEditFieldText("financial assessment notes updated ...");

        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Remove the Start Date field - Tap on the save button - Validate that the user is prevented from saving the record")]
        [Property("JiraIssueID", "CDV6-6733")]
        public void PersonFinancialAssessments_OfflineTestMethod08()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid ResponsibleUserId = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //mobile test user 1
            Guid FinancialAssessmentStatusId = new Guid("8a6505d2-b286-e911-9afd-d89ef34c460f"); //Draft
            Guid ChargingRuleId = new Guid("ba86fe23-db1d-ea11-a2c8-005056926fe4"); //Residential Permanent Stay
            Guid IncomeSupportTypeId = new Guid("395a5907-d74b-e911-a2c4-0050569231cf"); //ESA - Assessment Phase (Over 25) + EDP + SDP[1] + Carers [1]
            Guid FinancialAssessmentTypeId = new Guid("b9d3f401-6788-e911-9bfe-1803731f3ee3"); //Full Assessment
            DateTime StartDate = new DateTime(2020, 11, 2);
            DateTime EndDate = new DateTime(2020, 11, 8);
            DateTime CommencementDate = new DateTime(2020, 11, 2);
            decimal IncomeSupportValue = 192.6M;
            string Notes = "financial assessment notes ...";
            int FinancialAssessmentCategoryId = 1;
            int DaysPropertyDisregarded = 1;
            bool CalculationRequired = false;
            bool DeferredPaymentScheme = false;
            bool OverrideDefaultDeferredAmount = false;
            bool CalculateInterest = true;
            bool RecordedInError = false;
            bool HasServiceProvisionAssociated = false;
            bool PermitChargeUpdatesByFinancialAssessment = true;
            bool PermitChargeUpdatesByRecalculation = true;


            var financialAssessment1 = PlatformServicesHelper.financialAssessment.CreateFinancialAssessment(personID, ownerid,
                ResponsibleUserId, FinancialAssessmentStatusId, ChargingRuleId, IncomeSupportTypeId,
                FinancialAssessmentTypeId, StartDate, EndDate, CommencementDate, IncomeSupportValue, Notes, FinancialAssessmentCategoryId,
                DaysPropertyDisregarded, CalculationRequired, DeferredPaymentScheme, OverrideDefaultDeferredAmount, CalculateInterest,
                RecordedInError, HasServiceProvisionAssociated, PermitChargeUpdatesByFinancialAssessment, PermitChargeUpdatesByRecalculation);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(financialAssessment1.ToString());

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .InsertStartDate("")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Start Date' is required").TapOnOKButton();

        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Set a Start Date greater than the End Date - Tap on the save button - Validate that the user is prevented from saving the record")]
        [Property("JiraIssueID", "CDV6-6734")]
        public void PersonFinancialAssessments_OfflineTestMethod09()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid ResponsibleUserId = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //mobile test user 1
            Guid FinancialAssessmentStatusId = new Guid("8a6505d2-b286-e911-9afd-d89ef34c460f"); //Draft
            Guid ChargingRuleId = new Guid("ba86fe23-db1d-ea11-a2c8-005056926fe4"); //Residential Permanent Stay
            Guid IncomeSupportTypeId = new Guid("395a5907-d74b-e911-a2c4-0050569231cf"); //ESA - Assessment Phase (Over 25) + EDP + SDP[1] + Carers [1]
            Guid FinancialAssessmentTypeId = new Guid("b9d3f401-6788-e911-9bfe-1803731f3ee3"); //Full Assessment
            DateTime StartDate = new DateTime(2020, 11, 2);
            DateTime EndDate = new DateTime(2020, 11, 8);
            DateTime CommencementDate = new DateTime(2020, 11, 2);
            decimal IncomeSupportValue = 192.6M;
            string Notes = "financial assessment notes ...";
            int FinancialAssessmentCategoryId = 1;
            int DaysPropertyDisregarded = 1;
            bool CalculationRequired = false;
            bool DeferredPaymentScheme = false;
            bool OverrideDefaultDeferredAmount = false;
            bool CalculateInterest = true;
            bool RecordedInError = false;
            bool HasServiceProvisionAssociated = false;
            bool PermitChargeUpdatesByFinancialAssessment = true;
            bool PermitChargeUpdatesByRecalculation = true;


            var financialAssessment1 = PlatformServicesHelper.financialAssessment.CreateFinancialAssessment(personID, ownerid,
                ResponsibleUserId, FinancialAssessmentStatusId, ChargingRuleId, IncomeSupportTypeId,
                FinancialAssessmentTypeId, StartDate, EndDate, CommencementDate, IncomeSupportValue, Notes, FinancialAssessmentCategoryId,
                DaysPropertyDisregarded, CalculationRequired, DeferredPaymentScheme, OverrideDefaultDeferredAmount, CalculateInterest,
                RecordedInError, HasServiceProvisionAssociated, PermitChargeUpdatesByFinancialAssessment, PermitChargeUpdatesByRecalculation);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();


            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(financialAssessment1.ToString());

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .InsertStartDate("10/11/2020")
                .TapIncomeSupportTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("ESA - Assessment Phase (Over 25) + EDP + SDP").TapSearchButtonQuery().TapOnRecord("Code: 80");

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "End Date cannot be earlier than the Start Date.").TapOnOKButton();

        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Remove the Charging Rule vale - Tap on the save button - Validate that the user is prevented from saving the record")]
        [Property("JiraIssueID", "CDV6-6735")]
        public void PersonFinancialAssessments_OfflineTestMethod10()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid ResponsibleUserId = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //mobile test user 1
            Guid FinancialAssessmentStatusId = new Guid("8a6505d2-b286-e911-9afd-d89ef34c460f"); //Draft
            Guid ChargingRuleId = new Guid("ba86fe23-db1d-ea11-a2c8-005056926fe4"); //Residential Permanent Stay
            Guid IncomeSupportTypeId = new Guid("395a5907-d74b-e911-a2c4-0050569231cf"); //ESA - Assessment Phase (Over 25) + EDP + SDP[1] + Carers [1]
            Guid FinancialAssessmentTypeId = new Guid("b9d3f401-6788-e911-9bfe-1803731f3ee3"); //Full Assessment
            DateTime StartDate = new DateTime(2020, 11, 2);
            DateTime EndDate = new DateTime(2020, 11, 8);
            DateTime CommencementDate = new DateTime(2020, 11, 2);
            decimal IncomeSupportValue = 192.6M;
            string Notes = "financial assessment notes ...";
            int FinancialAssessmentCategoryId = 1;
            int DaysPropertyDisregarded = 1;
            bool CalculationRequired = false;
            bool DeferredPaymentScheme = false;
            bool OverrideDefaultDeferredAmount = false;
            bool CalculateInterest = true;
            bool RecordedInError = false;
            bool HasServiceProvisionAssociated = false;
            bool PermitChargeUpdatesByFinancialAssessment = true;
            bool PermitChargeUpdatesByRecalculation = true;


            var financialAssessment1 = PlatformServicesHelper.financialAssessment.CreateFinancialAssessment(personID, ownerid,
                ResponsibleUserId, FinancialAssessmentStatusId, ChargingRuleId, IncomeSupportTypeId,
                FinancialAssessmentTypeId, StartDate, EndDate, CommencementDate, IncomeSupportValue, Notes, FinancialAssessmentCategoryId,
                DaysPropertyDisregarded, CalculationRequired, DeferredPaymentScheme, OverrideDefaultDeferredAmount, CalculateInterest,
                RecordedInError, HasServiceProvisionAssociated, PermitChargeUpdatesByFinancialAssessment, PermitChargeUpdatesByRecalculation);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();


            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(financialAssessment1.ToString());

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapChargingRuleRemoveButton()
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Charging Rule' is required").TapOnOKButton();

        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Remove the Income Support Type value - Tap on the save button - Validate that the user is prevented from saving the record")]
        [Property("JiraIssueID", "CDV6-6736")]
        public void PersonFinancialAssessments_OfflineTestMethod11()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid ResponsibleUserId = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //mobile test user 1
            Guid FinancialAssessmentStatusId = new Guid("8a6505d2-b286-e911-9afd-d89ef34c460f"); //Draft
            Guid ChargingRuleId = new Guid("ba86fe23-db1d-ea11-a2c8-005056926fe4"); //Residential Permanent Stay
            Guid IncomeSupportTypeId = new Guid("395a5907-d74b-e911-a2c4-0050569231cf"); //ESA - Assessment Phase (Over 25) + EDP + SDP[1] + Carers [1]
            Guid FinancialAssessmentTypeId = new Guid("b9d3f401-6788-e911-9bfe-1803731f3ee3"); //Full Assessment
            DateTime StartDate = new DateTime(2020, 11, 2);
            DateTime EndDate = new DateTime(2020, 11, 8);
            DateTime CommencementDate = new DateTime(2020, 11, 2);
            decimal IncomeSupportValue = 192.6M;
            string Notes = "financial assessment notes ...";
            int FinancialAssessmentCategoryId = 1;
            int DaysPropertyDisregarded = 1;
            bool CalculationRequired = false;
            bool DeferredPaymentScheme = false;
            bool OverrideDefaultDeferredAmount = false;
            bool CalculateInterest = true;
            bool RecordedInError = false;
            bool HasServiceProvisionAssociated = false;
            bool PermitChargeUpdatesByFinancialAssessment = true;
            bool PermitChargeUpdatesByRecalculation = true;


            var financialAssessment1 = PlatformServicesHelper.financialAssessment.CreateFinancialAssessment(personID, ownerid,
                ResponsibleUserId, FinancialAssessmentStatusId, ChargingRuleId, IncomeSupportTypeId,
                FinancialAssessmentTypeId, StartDate, EndDate, CommencementDate, IncomeSupportValue, Notes, FinancialAssessmentCategoryId,
                DaysPropertyDisregarded, CalculationRequired, DeferredPaymentScheme, OverrideDefaultDeferredAmount, CalculateInterest,
                RecordedInError, HasServiceProvisionAssociated, PermitChargeUpdatesByFinancialAssessment, PermitChargeUpdatesByRecalculation);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();


            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(financialAssessment1.ToString());

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapIncomeSupportTypeRemoveButton()
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Income Support Type' is required").TapOnOKButton();

        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Remove the Permit Charge Updates via Financial Assessment? value - Tap on the save button - " +
            "Validate that the user is prevented from saving the record")]
        [Property("JiraIssueID", "CDV6-6737")]
        public void PersonFinancialAssessments_OfflineTestMethod12()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid ResponsibleUserId = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //mobile test user 1
            Guid FinancialAssessmentStatusId = new Guid("8a6505d2-b286-e911-9afd-d89ef34c460f"); //Draft
            Guid ChargingRuleId = new Guid("ba86fe23-db1d-ea11-a2c8-005056926fe4"); //Residential Permanent Stay
            Guid IncomeSupportTypeId = new Guid("395a5907-d74b-e911-a2c4-0050569231cf"); //ESA - Assessment Phase (Over 25) + EDP + SDP[1] + Carers [1]
            Guid FinancialAssessmentTypeId = new Guid("b9d3f401-6788-e911-9bfe-1803731f3ee3"); //Full Assessment
            DateTime StartDate = new DateTime(2020, 11, 2);
            DateTime EndDate = new DateTime(2020, 11, 8);
            DateTime CommencementDate = new DateTime(2020, 11, 2);
            decimal IncomeSupportValue = 192.6M;
            string Notes = "financial assessment notes ...";
            int FinancialAssessmentCategoryId = 1;
            int DaysPropertyDisregarded = 1;
            bool CalculationRequired = false;
            bool DeferredPaymentScheme = false;
            bool OverrideDefaultDeferredAmount = false;
            bool CalculateInterest = true;
            bool RecordedInError = false;
            bool HasServiceProvisionAssociated = false;
            bool PermitChargeUpdatesByFinancialAssessment = true;
            bool PermitChargeUpdatesByRecalculation = true;


            var financialAssessment1 = PlatformServicesHelper.financialAssessment.CreateFinancialAssessment(personID, ownerid,
                ResponsibleUserId, FinancialAssessmentStatusId, ChargingRuleId, IncomeSupportTypeId,
                FinancialAssessmentTypeId, StartDate, EndDate, CommencementDate, IncomeSupportValue, Notes, FinancialAssessmentCategoryId,
                DaysPropertyDisregarded, CalculationRequired, DeferredPaymentScheme, OverrideDefaultDeferredAmount, CalculateInterest,
                RecordedInError, HasServiceProvisionAssociated, PermitChargeUpdatesByFinancialAssessment, PermitChargeUpdatesByRecalculation);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(financialAssessment1.ToString());

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapPermitChargeUpdatesViaFinancialAssessmentField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Permit Charge Updates via Financial Assessment?' is required").TapOnOKButton();

        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Remove the Permit Charge Updates via Recalculation? value - Tap on the save button - Validate that the user is prevented from saving the record")]
        [Property("JiraIssueID", "CDV6-6738")]
        public void PersonFinancialAssessments_OfflineTestMethod13()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid ResponsibleUserId = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //mobile test user 1
            Guid FinancialAssessmentStatusId = new Guid("8a6505d2-b286-e911-9afd-d89ef34c460f"); //Draft
            Guid ChargingRuleId = new Guid("ba86fe23-db1d-ea11-a2c8-005056926fe4"); //Residential Permanent Stay
            Guid IncomeSupportTypeId = new Guid("395a5907-d74b-e911-a2c4-0050569231cf"); //ESA - Assessment Phase (Over 25) + EDP + SDP[1] + Carers [1]
            Guid FinancialAssessmentTypeId = new Guid("b9d3f401-6788-e911-9bfe-1803731f3ee3"); //Full Assessment
            DateTime StartDate = new DateTime(2020, 11, 2);
            DateTime EndDate = new DateTime(2020, 11, 8);
            DateTime CommencementDate = new DateTime(2020, 11, 2);
            decimal IncomeSupportValue = 192.6M;
            string Notes = "financial assessment notes ...";
            int FinancialAssessmentCategoryId = 1;
            int DaysPropertyDisregarded = 1;
            bool CalculationRequired = false;
            bool DeferredPaymentScheme = false;
            bool OverrideDefaultDeferredAmount = false;
            bool CalculateInterest = true;
            bool RecordedInError = false;
            bool HasServiceProvisionAssociated = false;
            bool PermitChargeUpdatesByFinancialAssessment = true;
            bool PermitChargeUpdatesByRecalculation = true;


            var financialAssessment1 = PlatformServicesHelper.financialAssessment.CreateFinancialAssessment(personID, ownerid,
                ResponsibleUserId, FinancialAssessmentStatusId, ChargingRuleId, IncomeSupportTypeId,
                FinancialAssessmentTypeId, StartDate, EndDate, CommencementDate, IncomeSupportValue, Notes, FinancialAssessmentCategoryId,
                DaysPropertyDisregarded, CalculationRequired, DeferredPaymentScheme, OverrideDefaultDeferredAmount, CalculateInterest,
                RecordedInError, HasServiceProvisionAssociated, PermitChargeUpdatesByFinancialAssessment, PermitChargeUpdatesByRecalculation);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();


            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(financialAssessment1.ToString());

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapPermitChargeUpdatesViaRecalculationField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Permit Charge Updates via Recalculation?' is required").TapOnOKButton();

        }

        #endregion

        #region Creating records

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - Tap on the Add new record button - " +
            "Validate that an error message is displayed to the user preventing him from saving the record.")]
        [Property("JiraIssueID", "CDV6-6739")]
        public void PersonFinancialAssessments_OfflineTestMethod14()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnAddNewRecordButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "This operation requires connectivity").TapOnOKButton();
        }

        #endregion

        #region Delete

        [Test]
        [Property("JiraIssueID", "CDV6-6740")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Person has an Allowable Expense - Open a person record - Navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Tap on the delete button - Confirm the delete operation - Validate that the record was deleted")]
        public void PersonFinancialAssessments_OfflineTestMethod20()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            Guid ResponsibleUserId = new Guid("2b16c2f3-459e-e911-a2c6-005056926fe4"); //mobile test user 1
            Guid FinancialAssessmentStatusId = new Guid("8a6505d2-b286-e911-9afd-d89ef34c460f"); //Draft
            Guid ChargingRuleId = new Guid("ba86fe23-db1d-ea11-a2c8-005056926fe4"); //Residential Permanent Stay
            Guid IncomeSupportTypeId = new Guid("395a5907-d74b-e911-a2c4-0050569231cf"); //ESA - Assessment Phase (Over 25) + EDP + SDP[1] + Carers [1]
            Guid FinancialAssessmentTypeId = new Guid("b9d3f401-6788-e911-9bfe-1803731f3ee3"); //Full Assessment
            DateTime StartDate = new DateTime(2020, 11, 2);
            DateTime EndDate = new DateTime(2020, 11, 8);
            DateTime CommencementDate = new DateTime(2020, 11, 2);
            decimal IncomeSupportValue = 192.6M;
            string Notes = "financial assessment notes ...";
            int FinancialAssessmentCategoryId = 1;
            int DaysPropertyDisregarded = 1;
            bool CalculationRequired = false;
            bool DeferredPaymentScheme = false;
            bool OverrideDefaultDeferredAmount = false;
            bool CalculateInterest = true;
            bool RecordedInError = false;
            bool HasServiceProvisionAssociated = false;
            bool PermitChargeUpdatesByFinancialAssessment = true;
            bool PermitChargeUpdatesByRecalculation = true;


            var financialAssessment1 = PlatformServicesHelper.financialAssessment.CreateFinancialAssessment(personID, ownerid,
                ResponsibleUserId, FinancialAssessmentStatusId, ChargingRuleId, IncomeSupportTypeId,
                FinancialAssessmentTypeId, StartDate, EndDate, CommencementDate, IncomeSupportValue, Notes, FinancialAssessmentCategoryId,
                DaysPropertyDisregarded, CalculationRequired, DeferredPaymentScheme, OverrideDefaultDeferredAmount, CalculateInterest,
                RecordedInError, HasServiceProvisionAssociated, PermitChargeUpdatesByFinancialAssessment, PermitChargeUpdatesByRecalculation);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(financialAssessment1.ToString());

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapOnDeleteButton();

            warningPopup.WaitForWarningPopupToLoad().ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?").TapOnYesButton();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            var records = PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID);
            Assert.AreEqual(0, records.Count);
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
