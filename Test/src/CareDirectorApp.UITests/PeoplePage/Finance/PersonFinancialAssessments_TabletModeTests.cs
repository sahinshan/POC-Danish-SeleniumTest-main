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
    [Category("Mobile_TabletMode_Online")]
    public class PersonFinancialAssessments_TabletModeTests : TestBase
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

            //if the cases Form injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the cases Form review pop-up is open then close it 
            personBodyMapReviewPopup.ClosePopupIfOpen();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

            //if the lookup pop-up is open close it
            lookupPopup.ClosePopupIfOpen();


            //navigate to the Settings page
            mainMenu.NavigateToPeoplePage();



            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-5537

        [Test]
        [Property("JiraIssueID", "CDV6-6741")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5537" +
            "Open a person record - navigate to the person Financial Assessments page - Validate that the page is displayed")]
        public void PersonFinancialAssessments_TestMethod01()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara

            peoplePage
                .WaitForPeoplePageToLoad()
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
        [Property("JiraIssueID", "CDV6-6742")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5537" +
            "Open a person record (person has no person Financial Assessments records) - Navigate to the person Financial Assessments page - " +
            "Validate that the no records message is present")]
        public void PersonFinancialAssessments_TestMethod02()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(pfdID);

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
                .ValidateNoRecordsMessageVisibility(true);
        }


        [Test]
        [Property("JiraIssueID", "CDV6-6743")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5537" +
            "Open a person record (person has Person Financial Assessments record) - Navigate to the person Financial Assessments page - " +
            "Validate that the record is displayed")]
        public void PersonFinancialAssessments_TestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid financialAssessment1 = new Guid("b6493f29-872d-eb11-a2ce-005056926fe4"); //204704
            Guid financialAssessment2 = new Guid("5362d7c7-a92d-eb11-a2ce-005056926fe4"); //204705

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .ValidateCalculationRequiredCell("Yes", financialAssessment2.ToString())
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
        [Property("JiraIssueID", "CDV6-6744")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - Navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record (with editable Income Support Value) - Validate that the record is correctly displayed.")]
        public void PersonFinancialAssessments_TestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid financialAssessment1 = new Guid("b6493f29-872d-eb11-a2ce-005056926fe4"); //204704

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .ValidateIncomeSupportValueEditableFieldText("129.00")

                .ValidateCommencementDateFieldText("01/11/2020")
                .ValidatePermitChargeUpdatesViaFinancialAssessmentFieldText("Yes")
                .ValidatePermitChargeUpdatesViaRecalculationFieldText("Yes")

                .ValidateNoteTextEditFieldText("Line 1\nLine 2")
                ;

        }


        [Test]
        [Property("JiraIssueID", "CDV6-6745")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - Navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record (with non-editable Income Support Value) - Validate that the record is correctly displayed.")]
        public void PersonFinancialAssessments_TestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid financialAssessment1 = new Guid("5362d7c7-a92d-eb11-a2ce-005056926fe4"); //204705

            peoplePage
                .WaitForPeoplePageToLoad()
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
        [Property("JiraIssueID", "CDV6-6746")]
        public void PersonFinancialAssessments_TestMethod06()
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
        [Property("JiraIssueID", "CDV6-6747")]
        public void PersonFinancialAssessments_TestMethod07()
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
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .InsertStartDate("03/11/2020")
                .InsertEndDate("09/11/2020")
                .TapChargingRuleLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Resid").TapSearchButtonQuery().TapOnRecordWithExactText("Name: Residential");
            System.Threading.Thread.Sleep(2000);

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapIncomeSupportTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("ESA - Assessment Phase (Over 25) + EDP + SDP").TapSearchButtonQuery().TapOnRecord("ESA - Assessment Phase (Over 25) + EDP + SDP[1]");

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
        [Property("JiraIssueID", "CDV6-6748")]
        public void PersonFinancialAssessments_TestMethod08()
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
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .InsertStartDate("")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Start Date' is required").TapOnOKButton();

        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Set a Start Date greater than the End Date - Tap on the save button - Validate that the user is prevented from saving the record")]
        [Property("JiraIssueID", "CDV6-6749")]
        public void PersonFinancialAssessments_TestMethod09()
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
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .InsertStartDate("10/11/2020")
                .TapIncomeSupportTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("ESA - Assessment Phase (Over 25) + EDP + SDP").TapSearchButtonQuery().TapOnRecord("ESA - Assessment Phase (Over 25) + EDP + SDP[1] + Carers [1]");

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "End Date cannot be earlier than the Start Date.").TapOnOKButton();

        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Remove the Charging Rule vale - Tap on the save button - Validate that the user is prevented from saving the record")]
        [Property("JiraIssueID", "CDV6-6750")]
        public void PersonFinancialAssessments_TestMethod10()
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
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapChargingRuleRemoveButton()
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Charging Rule' is required").TapOnOKButton();

        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Remove the Income Support Type value - Tap on the save button - Validate that the user is prevented from saving the record")]
        [Property("JiraIssueID", "CDV6-6751")]
        public void PersonFinancialAssessments_TestMethod11()
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
        [Property("JiraIssueID", "CDV6-6752")]
        public void PersonFinancialAssessments_TestMethod12()
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
        [Property("JiraIssueID", "CDV6-6753")]
        public void PersonFinancialAssessments_TestMethod13()
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
            "Set data in all fields - " +
            "Tap on the save button - Validate that the record is correctly saved")]
        [Property("JiraIssueID", "CDV6-6754")]
        public void PersonFinancialAssessments_TestMethod14()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);


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
                .TapOnAddNewRecordButton();

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .InsertStartDate("02/11/2020")
                .InsertEndDate("08/11/2020")
                .TapChargingRuleLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Residential").TapSearchButtonQuery().TapOnRecord("Residential Permanent Stay");

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .TapIncomeSupportTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("ESA - Assessment Phase (Over 25) + EDP + SDP").TapSearchButtonQuery().TapOnRecord("ESA - Assessment Phase (Over 25) + EDP + SDP[1] + Carers [1]");

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .InsertDaysPropertyDisregarded("1")
                .InsertNotes("financial assessment notes ...")
                .TapOnSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);
            var records = PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID);
            Assert.AreEqual(1, records.Count);

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnRecord(records[0].ToString());

            personFinancialAssessmentRecordPage
               .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
               .ValidateFinancialAssessmentStatusFieldText("Draft")
               .ValidateResponsibleUserFieldText("Mobile Test User 1")
               .ValidateResponsibleTeamFieldText("Mobile Team 1")

               .ValidateStartDateFieldText("02/11/2020")
               .ValidateEndDateFieldText("08/11/2020")

               .ValidateChargingRuleFieldText("Residential Permanent Stay")
               .ValidateIncomeSupportTypeFieldText("ESA - Assessment Phase (Over 25) + EDP + SDP[1] + Carers [1]")
               .ValidateFinancialAssessmentTypeFieldText("Full Assessment")
               .ValidateDaysPropertyDisregardedFieldText("1")
               .ValidateIncomeSupportValueReadOnlyFieldText("192.6")

               .ValidateCommencementDateFieldText(DateTime.Now.ToString("dd/MM/yyyy"))
               .ValidatePermitChargeUpdatesViaFinancialAssessmentFieldText("Yes")
               .ValidatePermitChargeUpdatesViaRecalculationFieldText("Yes")

               .ValidateNoteTextEditFieldText("financial assessment notes ...");
        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record (person has no DOB recorded) - navigate to the Financial Assessments page - Tap on the Add new record button - " +
            "Validate that the app displays an error message preventing the user from adding the record")]
        [Property("JiraIssueID", "CDV6-6755")]
        public void PersonFinancialAssessments_TestMethod15()
        {
            var personID = new Guid("48b8a6cd-f732-eb11-a2d4-005056926fe4"); //Eduarda Nunes

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Eduarda Nunes", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Eduarda Nunes")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinancialAssessmentsIcon_RelatedItems();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad()
                .TapOnAddNewRecordButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "Person's date of birth MUST be recorded before allowing an FA to be created").TapOnOKButton();
        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - Tap on the Add new record button - " +
            "Remove the value from the start date field - Select a charging rule - Validate that the Income Support Type lookup button is not visible - " +
            "Re-insert the start date - Validate that the Income Support Type lookup button is visible ")]
        [Property("JiraIssueID", "CDV6-6756")]
        public void PersonFinancialAssessments_TestMethod16()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);


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
                .TapOnAddNewRecordButton();

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .InsertStartDate("")
                .TapChargingRuleLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Residential").TapSearchButtonQuery().TapOnRecord("Residential Permanent Stay");

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .ValidateIncomeSupportTypeLookupButtonVisible(false)
                .InsertStartDate("02/11/2020")
                .ValidateIncomeSupportTypeLookupButtonVisible(true);
        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - Tap on the Add new record button - " +
            "Set Start Date to '01/11/2020' - Set End Date to '30/11/2020' - Set Charging Rule to 'Non Residential' - Tap on the Income Support Type lookup -  " +
            "Search for 'Appropriate Minimum Amount (Complex)' - Validate that one result is displayed - Close the lookup - " +
            "Change the Charging Rule to 'Residential' - Tap on the Income Support Type lookup -  " +
            "Search for 'Appropriate Minimum Amount (Complex)' - Validate that NO results are displayed")]
        [Property("JiraIssueID", "CDV6-6757")]
        public void PersonFinancialAssessments_TestMethod17()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);


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
                .TapOnAddNewRecordButton();

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .InsertStartDate("01/11/2020")
                .InsertEndDate("30/11/2020")
                .TapChargingRuleLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Non").TapSearchButtonQuery().TapOnRecord("Non Residential");

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .TapIncomeSupportTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Appropriate Minimum Amount").TapSearchButtonQuery().ValidateElementPresent("Appropriate Minimum Amount (Complex)").ClosePopupIfOpen();

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .TapChargingRuleLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Res").TapSearchButtonQuery().TapOnRecord("Residential");

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .TapIncomeSupportTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Appropriate Minimum Amount").TapSearchButtonQuery().ValidateElementNotPresent("Appropriate Minimum Amount (Complex)").ClosePopupIfOpen();
        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record - navigate to the Financial Assessments page - Tap on the Add new record button - " +
            "Set Start Date to '01/11/2020' - Set End Date to '30/11/2020' - Set Charging Rule to 'Non Residential' - Tap on the Income Support Type lookup -  " +
            "Search for 'Appropriate Minimum Amount (Complex)' - Validate that one result is displayed - Close the lookup - " +
            "Chage the Start Date to '01/12/2019' and End Date to '31/12/2019' - Tap on the Income Support Type lookup -  " +
            "Search for 'Appropriate Minimum Amount (Complex)' - Validate that NO results are displayed")]
        [Property("JiraIssueID", "CDV6-6758")]
        public void PersonFinancialAssessments_TestMethod18()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);


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
                .TapOnAddNewRecordButton();

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .InsertStartDate("01/11/2020")
                .InsertEndDate("30/11/2020")
                .TapChargingRuleLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Non").TapSearchButtonQuery().TapOnRecord("Non Residential");

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .TapIncomeSupportTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Appropriate Minimum Amount").TapSearchButtonQuery().ValidateElementPresent("Appropriate Minimum Amount (Complex)").ClosePopupIfOpen();

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .InsertStartDate("01/12/2019")
                .InsertEndDate("31/12/2019")
                .TapIncomeSupportTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Appropriate Minimum Amount").TapSearchButtonQuery().ValidateElementNotPresent("Appropriate Minimum Amount (Complex)").ClosePopupIfOpen();
        }

        [Test]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Open a person record (born in 01/05/1960) - navigate to the Financial Assessments page - Tap on the Add new record button - " +
            "Set Start Date to '01/11/2020' - Set End Date to '30/11/2020' - Set Charging Rule to 'Non Residential' - Tap on the Income Support Type lookup -  " +
            "Search for 'Appropriate Minimum Amount (Basic)' - Validate that one result is displayed - Close the lookup - " +
            "Change the Start Date to '01/12/2019' and End Date to '31/12/2019' - Tap on the Income Support Type lookup -  " +
            "Search for 'Appropriate Minimum Amount (Basic)' - Validate that NO results are displayed (at the start date the person is 59 years old and no Income Support Setup will match)")]
        [Property("JiraIssueID", "CDV6-6759")]
        public void PersonFinancialAssessments_TestMethod19()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var faID in PlatformServicesHelper.financialAssessment.GetFinancialAssessmentByPersonID(personID))
                PlatformServicesHelper.financialAssessment.DeleteFinancialAssessment(faID);


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
                .TapOnAddNewRecordButton();

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .InsertStartDate("01/11/2020")
                .InsertEndDate("30/11/2020")
                .TapChargingRuleLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Non").TapSearchButtonQuery().TapOnRecord("Non Residential");

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .TapIncomeSupportTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Appropriate Minimum Amount").TapSearchButtonQuery().ValidateElementPresent("Appropriate Minimum Amount (Basic)").ClosePopupIfOpen();

            personFinancialAssessmentRecordPage
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENTS")
                .InsertStartDate("01/12/2019")
                .InsertEndDate("31/12/2019")
                .TapIncomeSupportTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Appropriate Minimum Amount").TapSearchButtonQuery().ValidateElementNotPresent("Appropriate Minimum Amount (Basic)").ClosePopupIfOpen();
        }




        #endregion

        #region Delete

        [Test]
        [Property("JiraIssueID", "CDV6-6760")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5538" +
            "Person has an Allowable Expense - Open a person record - Navigate to the Financial Assessments page - " +
            "Open the Financial Assessment record - Tap on the delete button - Confirm the delete operation - Validate that the record was deleted")]
        public void PersonFinancialAssessments_TestMethod20()
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
                .WaitForPersonFinancialAssessmentRecordPageToLoad("FINANCIAL ASSESSMENT: Maria Tsatsouline \\ Residential Permanent Stay \\ 02/11/2020 \\ 08/11/2020")
                .TapOnDeleteButton();

            warningPopup.WaitForWarningPopupToLoad().ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?").TapOnYesButton();

            personFinancialAssessmentsPage
                .WaitForPersonFinancialAssessmentsPageToLoad();

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
