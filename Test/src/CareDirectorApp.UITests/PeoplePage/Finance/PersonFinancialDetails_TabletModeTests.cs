using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;
using System.Collections.Generic;

namespace CareDirectorApp.UITests.People.Finance
{
    /// <summary>
    /// https://advancedcsg.atlassian.net/browse/CDV6-4875
    /// 
    /// Tests for the activation and deactivation of the finance business module 
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PersonFinancialDetails_TabletModeTests : TestBase
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

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6820")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Validate that the page is displayed")]
        public void PersonFinancialDetails_TestMethod01()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6821")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record (person has no person Financial details records) - navigate to the person financial details page - " +
            "Validate that the no records message is present")]
        public void PersonFinancialDetails_TestMethod02()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6822")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record (person has a person Financial details record) - navigate to the person financial details page - " +
            "Validate that the record is displayed")]
        public void PersonFinancialDetails_TestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid personFinancialDetailID = new Guid("df30bc95-c804-eb11-a2cd-005056926fe4"); //AE (Multiple Rates)

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .ValidateNameCellText("Pavel MCNamara, AE (Multiple Rates), 01/10/2020, 31/10/2020", personFinancialDetailID.ToString())
                .ValidateCreatedByCellText("José Brazeta", personFinancialDetailID.ToString())
                .ValidateCreatedOnCellText("02/10/2020 17:01", personFinancialDetailID.ToString())
                .ValidateModifiedByText("José Brazeta", personFinancialDetailID.ToString())
                .ValidateModifiedOnCellText("02/10/2020 17:01", personFinancialDetailID.ToString());
        }


        #region Open Existing Record


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6823")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Allowable Expense (multiple rates) person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Validate that the record is correctly displayed.")]
        public void PersonFinancialDetails_TestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid personFinancialDetailID = new Guid("df30bc95-c804-eb11-a2cd-005056926fe4"); //AE (Multiple Rates)

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Pavel MCNamara, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .ValidateIdFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(true)
                .ValidateFinancialDetailTypeFieldTitleVisible(true)
                .ValidateFinancialDetailFieldTitleVisible(true)
                .ValidateAmountFieldTitleVisible(true)
                .ValidateJointAmountFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateFrequencyOfReceiptFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateVerificationFieldTitleVisible(true)

                .ValidateReferenceFieldTitleVisible(true)
                .ValidateInactiveFieldTitleVisible(true)
                .ValidateShowReferenceInScheduleFieldTitleVisible(true)

                .ValidateIdFieldText("719326")
                .ValidatePersonFieldText("Pavel MCNamara")
                .ValidateFinancialDetailTypeFieldText("Allowable Expense")
                .ValidateFinancialDetailFieldText("AE (Multiple Rates)")
                .ValidateAmountFieldText("33.80")
                .ValidateJointAmountFieldText("50.00")
                .ValidateResponsibleTeamFieldText("CareDirector QA")
                .ValidateFrequencyOfReceiptFieldText("Per Fortnight")
                .ValidateStartDateFieldText("01/10/2020")
                .ValidateEndDateFieldText("31/10/2020")
                .ValidateVerificationFieldText("")

                .ValidateReferenceFieldText("1")
                .ValidateInactiveFieldText("No")
                .ValidateShowReferenceInScheduleFieldText("No");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6824")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Allowable Expense (Variable Rates) person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Validate that the record is correctly displayed.")]
        public void PersonFinancialDetails_TestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid personFinancialDetailID = new Guid("4c2bb7b8-c804-eb11-a2cd-005056926fe4"); //Additional Personal Allowance

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Pavel MCNamara, Additional Personal Allowance - Variable, 01/10/2020,")
                .ValidateIdFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(true)
                .ValidateFinancialDetailTypeFieldTitleVisible(true)
                .ValidateFinancialDetailFieldTitleVisible(true)
                .ValidateAmountFieldTitleVisible(true)
                .ValidateJointAmountFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateFrequencyOfReceiptFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateVerificationFieldTitleVisible(true)

                .ValidateReferenceFieldTitleVisible(true)
                .ValidateInactiveFieldTitleVisible(true)
                .ValidateShowReferenceInScheduleFieldTitleVisible(true)

                .ValidateIdFieldText("719327")
                .ValidatePersonFieldText("Pavel MCNamara")
                .ValidateFinancialDetailTypeFieldText("Allowable Expense")
                .ValidateFinancialDetailFieldText("Additional Personal Allowance - Variable")
                .ValidateAmountFieldText("321.98")
                .ValidateJointAmountFieldText("432.76")
                .ValidateResponsibleTeamFieldText("CareDirector QA")
                .ValidateFrequencyOfReceiptFieldText("Per Calendar Month")
                .ValidateStartDateFieldText("01/10/2020")
                .ValidateEndDateFieldText("")
                .ValidateVerificationFieldText("")

                .ValidateReferenceFieldText("2")
                .ValidateInactiveFieldText("No")
                .ValidateShowReferenceInScheduleFieldText("No");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6825")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Asset person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Validate that the record is correctly displayed.")]
        public void PersonFinancialDetails_TestMethod07()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid personFinancialDetailID = new Guid("e5184dcc-c804-eb11-a2cd-005056926fe4"); //Current Amount

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Pavel MCNamara, Current Account, 01/10/2020, 31/10/2020")
                .ValidateIdFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(true)
                .ValidateFinancialDetailTypeFieldTitleVisible(true)
                .ValidateFinancialDetailFieldTitleVisible(true)
                .ValidateAmountFieldTitleVisible(true)
                .ValidateJointAmountFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateFrequencyOfReceiptFieldTitleVisible(false)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateVerificationFieldTitleVisible(true)

                .ValidateReferenceFieldTitleVisible(true)
                .ValidateInactiveFieldTitleVisible(true)
                .ValidateShowReferenceInScheduleFieldTitleVisible(true)

                .ValidateIdFieldText("719328")
                .ValidatePersonFieldText("Pavel MCNamara")
                .ValidateFinancialDetailTypeFieldText("Asset")
                .ValidateFinancialDetailFieldText("Current Account")
                .ValidateAmountFieldText("1500.99")
                .ValidateJointAmountFieldText("1600.51")
                .ValidateResponsibleTeamFieldText("CareDirector QA")
                //.ValidateFrequencyOfReceiptFieldText("Per Calendar Month")
                .ValidateStartDateFieldText("01/10/2020")
                .ValidateEndDateFieldText("31/10/2020")
                .ValidateVerificationFieldText("")

                .ValidateReferenceFieldText("3")
                .ValidateInactiveFieldText("No")
                .ValidateShowReferenceInScheduleFieldText("No");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6826")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Benefit & Income (Multiple Rates) person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Validate that the record is correctly displayed.")]
        public void PersonFinancialDetails_TestMethod08()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid personFinancialDetailID = new Guid("912599f7-c804-eb11-a2cd-005056926fe4"); //Current Amount

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Pavel MCNamara, Attendance Allowance - High, 01/10/2020, 31/10/2020")
                .ValidateIdFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(true)
                .ValidateFinancialDetailTypeFieldTitleVisible(true)
                .ValidateFinancialDetailFieldTitleVisible(true)
                .ValidateAmountFieldTitleVisible(true)
                .ValidateJointAmountFieldTitleVisible(false)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateFrequencyOfReceiptFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateVerificationFieldTitleVisible(true)
                .ValidateBeingReceivedFieldTitleVisible(true)

                .ValidateReferenceFieldTitleVisible(true)
                .ValidateApplicationDateFieldTitleVisible(true)
                .ValidateInactiveFieldTitleVisible(true)
                .ValidateShowReferenceInScheduleFieldTitleVisible(true)
                .ValidateArrearsFieldTitleVisible(true)

                .ValidateIdFieldText("719329")
                .ValidatePersonFieldText("Pavel MCNamara")
                .ValidateFinancialDetailTypeFieldText("Benefit & Income")
                .ValidateFinancialDetailFieldText("Attendance Allowance - High")
                .ValidateAmountFieldText("55.21")
                .ValidateResponsibleTeamFieldText("CareDirector QA")
                .ValidateFrequencyOfReceiptFieldText("Per Week")
                .ValidateStartDateFieldText("01/10/2020")
                .ValidateEndDateFieldText("31/10/2020")
                .ValidateVerificationFieldText("")
                .ValidateBeingReceivedFieldText("Yes")

                .ValidateReferenceFieldText("4")
                .ValidateApplicationDateFieldText("02/10/2020")
                .ValidateInactiveFieldText("No")
                .ValidateShowReferenceInScheduleFieldText("No")
                .ValidateArrearsFieldText("54.13")
                ;
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6827")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Benefit & Income (Variable Rate) person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Validate that the record is correctly displayed.")]
        public void PersonFinancialDetails_TestMethod09()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid personFinancialDetailID = new Guid("98756f13-c904-eb11-a2cd-005056926fe4"); //Bereavement Allowance - Variable

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Pavel MCNamara, Bereavement Allowance - Variable, 01/10/2020, 31/10/2020")
                .ValidateIdFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(true)
                .ValidateFinancialDetailTypeFieldTitleVisible(true)
                .ValidateFinancialDetailFieldTitleVisible(true)
                .ValidateAmountFieldTitleVisible(true)
                .ValidateJointAmountFieldTitleVisible(false)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateFrequencyOfReceiptFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateVerificationFieldTitleVisible(true)
                .ValidateBeingReceivedFieldTitleVisible(true)

                .ValidateReferenceFieldTitleVisible(true)
                .ValidateApplicationDateFieldTitleVisible(true)
                .ValidateInactiveFieldTitleVisible(true)
                .ValidateShowReferenceInScheduleFieldTitleVisible(true)
                .ValidateArrearsFieldTitleVisible(true)

                .ValidateIdFieldText("719330")
                .ValidatePersonFieldText("Pavel MCNamara")
                .ValidateFinancialDetailTypeFieldText("Benefit & Income")
                .ValidateFinancialDetailFieldText("Bereavement Allowance - Variable")
                .ValidateAmountFieldText("250.73")
                .ValidateResponsibleTeamFieldText("CareDirector QA")
                .ValidateFrequencyOfReceiptFieldText("Per Week")
                .ValidateStartDateFieldText("01/10/2020")
                .ValidateEndDateFieldText("31/10/2020")
                .ValidateVerificationFieldText("")
                .ValidateBeingReceivedFieldText("Yes")

                .ValidateReferenceFieldText("5")
                .ValidateApplicationDateFieldText("03/10/2020")
                .ValidateInactiveFieldText("No")
                .ValidateShowReferenceInScheduleFieldText("No")
                .ValidateArrearsFieldText("-0.98")
                ;
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6828")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Property person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Validate that the record is correctly displayed.")]
        public void PersonFinancialDetails_TestMethod10()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara
            Guid personFinancialDetailID = new Guid("54d40445-c904-eb11-a2cd-005056926fe4"); //Land

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Pavel MCNamara, Land, 01/10/2020, 31/10/2020")
                .ValidateIdFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(true)
                .ValidateFinancialDetailTypeFieldTitleVisible(true)
                .ValidateFinancialDetailFieldTitleVisible(true)
                .ValidateAmountFieldTitleVisible(false)
                .ValidateJointAmountFieldTitleVisible(false)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateFrequencyOfReceiptFieldTitleVisible(false)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateVerificationFieldTitleVisible(true)
                .ValidateBeingReceivedFieldTitleVisible(false)

                .ValidateReferenceFieldTitleVisible(true)
                .ValidateApplicationDateFieldTitleVisible(false)
                .ValidateInactiveFieldTitleVisible(true)
                .ValidateShowReferenceInScheduleFieldTitleVisible(true)
                .ValidateArrearsFieldTitleVisible(false)

                .ValidateIdFieldText("719331")
                .ValidatePersonFieldText("Pavel MCNamara")
                .ValidateFinancialDetailTypeFieldText("Property")
                .ValidateFinancialDetailFieldText("Land")
                .ValidateResponsibleTeamFieldText("CareDirector QA")
                .ValidateStartDateFieldText("01/10/2020")
                .ValidateEndDateFieldText("31/10/2020")
                .ValidateVerificationFieldText("")

                .ValidateAddressFieldText("address line 1\naddress line 2")
                .ValidatePropertyDisregardTypeFieldText("Property Disregard Type 1")
                .ValidateExcludeFromDWPCalculationFieldText("Yes")
                .ValidateGrossValueFieldText("5678.98")
                .ValidateOutstandingLoanFieldText("23.97")
                .ValidateEquityFieldText("5655.01")
                .ValidateOwnershipFieldText("97")

                .ValidateReferenceFieldText("6")
                .ValidateInactiveFieldText("No")
                .ValidateShowReferenceInScheduleFieldText("No")
                ;
        }


        #endregion

        #region Update Records

        [Test]
        [Property("JiraIssueID", "CDV6-6829")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Allowable Expense (multiple rates) person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Change the Frequency of receipt value - Validate that the Amount field is automatically updated")]
        public void PersonFinancialDetails_TestMethod11()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .TapFrequencyOfReceiptLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Per W").TapSearchButtonQuery().TapOnRecord("Per Week");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .ValidateAmountFieldText("19.01");
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6830")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Allowable Expense (multiple rates) person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Update all editable fields - Save the record - Validate that the changes are saved in the database")]
        public void PersonFinancialDetails_TestMethod12()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("Additional Personal Allowance - Variable")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Additional Personal Allowance - Variable, 01/10/2020, 31/10/2020")
                .InsertAmount("19.01")
                .InsertJointAmount("98.76")
                .InsertStartDate("02/10/2020")
                .InsertEndDate("30/10/2020")
                .InsertReference("2")
                .TapFrequencyOfReceiptLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Per W").TapSearchButtonQuery().TapOnRecord("Per Week");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Additional Personal Allowance - Variable, 01/10/2020, 31/10/2020")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Additional Personal Allowance - Variable, 01/10/2020, 31/10/2020")
                .TapOnSaveAndCloseButton();

            warningPopup.WaitForWarningPopupToLoad().ValidateErrorMessageTitleAndMessage("Warning", "Any changes to this record will lead to a recalculation of this Person’s Financial Assessments that date overlap. Do you wish to continue?").TapOnYesButton();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad();


            var newFrequencyOfReceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Week")[0];
            var newStartDate = new DateTime(2020, 10, 2);
            var newEndDate = new DateTime(2020, 10, 30);
            var newReference = "2";
            var newAmount = 19.01;
            var newJointAmount = 98.76M;
            var newShowReferenceInSchedule = true;
            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(personFinancialDetailID, "ownerid", "personid", "financialdetailid", "frequencyofreceiptid", "startdate", "enddate", "financialdetailtypeid", "reference", "amount", "jointamount", "beingreceived", "excludefromdwpcalculation", "showreferenceinschedule", "deferredpaymentschemesecurity");

            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(newFrequencyOfReceiptid, fields["frequencyofreceiptid"]); //updated
            Assert.AreEqual(newStartDate, fields["startdate"]);//updated
            Assert.AreEqual(newEndDate, fields["enddate"]);//updated
            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(newReference, fields["reference"]); //updated
            Assert.AreEqual(newAmount, fields["amount"]); //updated
            Assert.AreEqual(newJointAmount, fields["jointamount"]); //update
            Assert.AreEqual(beingreceived, fields["beingreceived"]);
            Assert.AreEqual(excludefromdwpcalculation, fields["excludefromdwpcalculation"]);
            Assert.AreEqual(newShowReferenceInSchedule, fields["showreferenceinschedule"]);//updated
            Assert.AreEqual(deferredpaymentschemesecurity, fields["deferredpaymentschemesecurity"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6831")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Allowable Expense (Variable Amount) person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Update all editable fields - Save the record - Validate that the changes are saved in the database")]
        public void PersonFinancialDetails_TestMethod13()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("Additional Personal Allowance - Variable")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Calendar Month")[0];
            var startdate = new DateTime(2020, 10, 1);
            DateTime? enddate = null;
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 321.98M;
            var jointamount = 432.76M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Additional Personal Allowance - Variable, 01/10/2020,")
                .InsertAmount("50.35")
                .InsertJointAmount("60.99")
                .InsertStartDate("02/10/2020")
                .InsertEndDate("30/10/2020")
                .InsertReference("2")
                .TapFrequencyOfReceiptLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Per W").TapSearchButtonQuery().TapOnRecord("Per Week");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Additional Personal Allowance - Variable, 01/10/2020,")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Additional Personal Allowance - Variable, 01/10/2020,")
                .TapOnSaveAndCloseButton();

            warningPopup.WaitForWarningPopupToLoad().ValidateErrorMessageTitleAndMessage("Warning", "Any changes to this record will lead to a recalculation of this Person’s Financial Assessments that date overlap. Do you wish to continue?").TapOnYesButton();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad();


            var newFrequencyOfReceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Week")[0];
            var newStartDate = new DateTime(2020, 10, 2);
            var newEndDate = new DateTime(2020, 10, 30);
            var newReference = "2";
            var newAmount = 50.35;
            var newJointAmount = 60.99;
            var newShowReferenceInSchedule = true;
            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(personFinancialDetailID, "ownerid", "personid", "financialdetailid", "frequencyofreceiptid", "startdate", "enddate", "financialdetailtypeid", "reference", "amount", "jointamount", "beingreceived", "excludefromdwpcalculation", "showreferenceinschedule", "deferredpaymentschemesecurity");

            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(newFrequencyOfReceiptid, fields["frequencyofreceiptid"]); //updated
            Assert.AreEqual(newStartDate, fields["startdate"]);//updated
            Assert.AreEqual(newEndDate, fields["enddate"]);//updated
            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(newReference, fields["reference"]); //updated
            Assert.AreEqual(newAmount, fields["amount"]); //updated
            Assert.AreEqual(newJointAmount, fields["jointamount"]); //update
            Assert.AreEqual(beingreceived, fields["beingreceived"]);
            Assert.AreEqual(excludefromdwpcalculation, fields["excludefromdwpcalculation"]);
            Assert.AreEqual(newShowReferenceInSchedule, fields["showreferenceinschedule"]);//updated
            Assert.AreEqual(deferredpaymentschemesecurity, fields["deferredpaymentschemesecurity"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6832")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Asset person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Update all editable fields - Save the record - Validate that the changes are saved in the database")]
        public void PersonFinancialDetails_TestMethod14()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("Current Account")[0];
            var frequencyofreceiptid = (Guid?)null;
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 2;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Current Account, 01/10/2020, 31/10/2020")
                .InsertAmount("19.01")
                .InsertJointAmount("98.76")
                .InsertStartDate("02/10/2020")
                .InsertEndDate("30/10/2020")
                .InsertReference("2")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Current Account, 01/10/2020, 31/10/2020")
                .TapOnSaveAndCloseButton();

            warningPopup.WaitForWarningPopupToLoad().ValidateErrorMessageTitleAndMessage("Warning", "Any changes to this record will lead to a recalculation of this Person’s Financial Assessments that date overlap. Do you wish to continue?").TapOnYesButton();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad();


            var newStartDate = new DateTime(2020, 10, 2);
            var newEndDate = new DateTime(2020, 10, 30);
            var newReference = "2";
            var newAmount = 19.01M;
            var newJointAmount = 98.76M;
            var newShowReferenceInSchedule = true;
            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(personFinancialDetailID, "ownerid", "personid", "financialdetailid", "frequencyofreceiptid", "startdate", "enddate", "financialdetailtypeid", "reference", "amount", "jointamount", "beingreceived", "excludefromdwpcalculation", "showreferenceinschedule", "deferredpaymentschemesecurity");

            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(false, fields.ContainsKey("frequencyofreceiptid")); //updated
            Assert.AreEqual(newStartDate, fields["startdate"]);//updated
            Assert.AreEqual(newEndDate, fields["enddate"]);//updated
            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(newReference, fields["reference"]); //updated
            Assert.AreEqual(newAmount, fields["amount"]); //updated
            Assert.AreEqual(newJointAmount, fields["jointamount"]); //update
            Assert.AreEqual(beingreceived, fields["beingreceived"]);
            Assert.AreEqual(excludefromdwpcalculation, fields["excludefromdwpcalculation"]);
            Assert.AreEqual(newShowReferenceInSchedule, fields["showreferenceinschedule"]);//updated
            Assert.AreEqual(deferredpaymentschemesecurity, fields["deferredpaymentschemesecurity"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6833")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Benefit & Income (multiple rates) person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Update all editable fields - Save the record - Validate that the changes are saved in the database")]
        public void PersonFinancialDetails_TestMethod15()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("BI (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per 4 Weeks")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var applicationdate = new DateTime(2020, 10, 15);
            var financialdetailtypeid = 3;
            var reference = "1";
            var amount = 100M;
            var jointamount = (decimal?)null;
            var arrears = -1.80M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, applicationdate, financialdetailtypeid, reference, amount, jointamount, arrears, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, BI (Multiple Rates), 01/10/2020, 31/10/2020")
                .InsertStartDate("02/10/2020")
                .InsertEndDate("30/10/2020")
                .InsertApplicationDate("09/10/2020")
                .InsertReference("2")
                .InsertArrears("-9.12")
                .TapFrequencyOfReceiptLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Per Fort").TapSearchButtonQuery().TapOnRecord("Per Fortnight");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, BI (Multiple Rates), 01/10/2020, 31/10/2020")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, BI (Multiple Rates), 01/10/2020, 31/10/2020")
                .TapBeingReceivedField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, BI (Multiple Rates), 01/10/2020, 31/10/2020")
                .TapOnSaveAndCloseButton();

            warningPopup.WaitForWarningPopupToLoad().ValidateErrorMessageTitleAndMessage("Warning", "Any changes to this record will lead to a recalculation of this Person’s Financial Assessments that date overlap. Do you wish to continue?").TapOnYesButton();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad();


            var newFrequencyOfReceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var newAmount = 137M;
            var newStartDate = new DateTime(2020, 10, 2);
            var newEndDate = new DateTime(2020, 10, 30);
            var newbeingrecieved = false;
            var newReference = "2";
            var newapplicationdate = new DateTime(2020, 10, 09);
            var newShowReferenceInSchedule = true;
            var newarrears = -9.12M;
            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(personFinancialDetailID, "ownerid", "personid", "financialdetailid", "frequencyofreceiptid", "startdate", "enddate", "applicationdate", "financialdetailtypeid", "reference", "amount", "jointamount", "arrears", "beingreceived", "excludefromdwpcalculation", "showreferenceinschedule", "deferredpaymentschemesecurity");



            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(newFrequencyOfReceiptid, fields["frequencyofreceiptid"]); //updated
            Assert.AreEqual(newStartDate, fields["startdate"]);//updated
            Assert.AreEqual(newEndDate, fields["enddate"]);//updated
            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(newReference, fields["reference"]); //updated
            Assert.AreEqual(newAmount, fields["amount"]); //updated
            Assert.AreEqual(newapplicationdate, fields["applicationdate"]); //updated
            Assert.AreEqual(newarrears, fields["arrears"]); //updated
            Assert.AreEqual(newbeingrecieved, fields["beingreceived"]);
            Assert.AreEqual(excludefromdwpcalculation, fields["excludefromdwpcalculation"]);
            Assert.AreEqual(newShowReferenceInSchedule, fields["showreferenceinschedule"]);//updated
            Assert.AreEqual(deferredpaymentschemesecurity, fields["deferredpaymentschemesecurity"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6834")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Benefit & Income (variable amount) person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Update all editable fields - Save the record - Validate that the changes are saved in the database")]
        public void PersonFinancialDetails_TestMethod16()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("Benefit (Disregarded) - Variable [SF]")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per 4 Weeks")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var applicationdate = new DateTime(2020, 10, 15);
            var financialdetailtypeid = 3;
            var reference = "1";
            var amount = 112.21M;
            var jointamount = (decimal?)null;
            var arrears = -1.80M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, applicationdate, financialdetailtypeid, reference, amount, jointamount, arrears, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Benefit (Disregarded) - Variable [SF], 01/10/2020, 31/10/2020")
                .InsertAmount("99.62")
                .InsertStartDate("02/10/2020")
                .InsertEndDate("30/10/2020")
                .InsertApplicationDate("09/10/2020")
                .InsertReference("2")
                .InsertArrears("-9.12")
                .TapFrequencyOfReceiptLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Per Fort").TapSearchButtonQuery().TapOnRecord("Per Fortnight");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Benefit (Disregarded) - Variable [SF], 01/10/2020, 31/10/2020")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Benefit (Disregarded) - Variable [SF], 01/10/2020, 31/10/2020")
                .TapBeingReceivedField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Benefit (Disregarded) - Variable [SF], 01/10/2020, 31/10/2020")
                .TapOnSaveAndCloseButton();

            warningPopup.WaitForWarningPopupToLoad().ValidateErrorMessageTitleAndMessage("Warning", "Any changes to this record will lead to a recalculation of this Person’s Financial Assessments that date overlap. Do you wish to continue?").TapOnYesButton();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad();


            var newFrequencyOfReceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var newAmount = 99.62M;
            var newStartDate = new DateTime(2020, 10, 2);
            var newEndDate = new DateTime(2020, 10, 30);
            var newbeingrecieved = false;
            var newReference = "2";
            var newapplicationdate = new DateTime(2020, 10, 09);
            var newShowReferenceInSchedule = true;
            var newarrears = -9.12M;
            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(personFinancialDetailID, "ownerid", "personid", "financialdetailid", "frequencyofreceiptid", "startdate", "enddate", "applicationdate", "financialdetailtypeid", "reference", "amount", "jointamount", "arrears", "beingreceived", "excludefromdwpcalculation", "showreferenceinschedule", "deferredpaymentschemesecurity");



            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(newFrequencyOfReceiptid, fields["frequencyofreceiptid"]); //updated
            Assert.AreEqual(newStartDate, fields["startdate"]);//updated
            Assert.AreEqual(newEndDate, fields["enddate"]);//updated
            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(newReference, fields["reference"]); //updated
            Assert.AreEqual(newAmount, fields["amount"]); //updated
            Assert.AreEqual(newapplicationdate, fields["applicationdate"]); //updated
            Assert.AreEqual(newarrears, fields["arrears"]); //updated
            Assert.AreEqual(newbeingrecieved, fields["beingreceived"]);
            Assert.AreEqual(excludefromdwpcalculation, fields["excludefromdwpcalculation"]);
            Assert.AreEqual(newShowReferenceInSchedule, fields["showreferenceinschedule"]);//updated
            Assert.AreEqual(deferredpaymentschemesecurity, fields["deferredpaymentschemesecurity"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6835")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Property person financial detail - Open a person record - navigate to the person financial details page - " +
            "Open the person financial detail record - Update all editable fields - Save the record - Validate that the changes are saved in the database")]
        public void PersonFinancialDetails_TestMethod17()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailtypeid = 4;
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("Land")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var address = "address value";
            var propertydisregardtypeid = PlatformServicesHelper.propertyDisregardType.GetPropertyDisregardTypeByName("Property Disregard Type 1")[0];
            var excludefromdwpcalculation = false;
            var grossvalue = 5123.54M;
            var outstandingloan = 1002.84M;
            var equity = 4120.70M;
            var percentageownership = 90;
            var reference = "1";
            var showreferenceinschedule = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetailForProperty(
                ownerid, personID, financialdetailtypeid, financialdetailid,
                startdate, enddate,
                address, propertydisregardtypeid, excludefromdwpcalculation, grossvalue, outstandingloan, equity, percentageownership,
                reference, showreferenceinschedule);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Land, 01/10/2020, 31/10/2020")
                .InsertStartDate("02/10/2020")
                .InsertEndDate("30/10/2020")
                .InsertAddress("address changed")
                .InsertGrossValue("5000.00")
                .InsertOutstandingLoan("1000.00")
                .InsertOwnershipField("80")
                .InsertReference("2")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Land, 01/10/2020, 31/10/2020")
                .TapPropertyDisregardTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Resident P").TapSearchButtonQuery().TapOnRecord("Resident Partner");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Land, 01/10/2020, 31/10/2020")
                .TapExcludeFromDWPCalculationField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Land, 01/10/2020, 31/10/2020")
                .TapOnSaveAndCloseButton();

            warningPopup.WaitForWarningPopupToLoad().ValidateErrorMessageTitleAndMessage("Warning", "Any changes to this record will lead to a recalculation of this Person’s Financial Assessments that date overlap. Do you wish to continue?").TapOnYesButton();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad();


            var newstartdate = new DateTime(2020, 10, 2);
            var newenddate = new DateTime(2020, 10, 30);
            var newaddress = "address changed";
            var newpropertydisregardtypeid = PlatformServicesHelper.propertyDisregardType.GetPropertyDisregardTypeByName("Resident Partner")[0];
            var newexcludefromdwpcalculation = true;
            var newgrossvalue = 5000M;
            var newoutstandingloan = 1000M;
            var newequity = 4000M;
            var newpercentageownership = 80;
            var newreference = "2";
            var newshowreferenceinschedule = true;
            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(personFinancialDetailID, "ownerid", "personid", "financialdetailtypeid", "financialdetailid", "startdate", "enddate", "address", "propertydisregardtypeid", "excludefromdwpcalculation", "grossvalue", "outstandingloan", "equity", "percentageownership", "reference", "showreferenceinschedule");

            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(newstartdate, fields["startdate"]);
            Assert.AreEqual(newenddate, fields["enddate"]);
            Assert.AreEqual(newaddress, fields["address"]);
            Assert.AreEqual(newpropertydisregardtypeid, fields["propertydisregardtypeid"]);
            Assert.AreEqual(newexcludefromdwpcalculation, fields["excludefromdwpcalculation"]);
            Assert.AreEqual(newgrossvalue, fields["grossvalue"]);
            Assert.AreEqual(newoutstandingloan, fields["outstandingloan"]);
            Assert.AreEqual(newequity, fields["equity"]);
            Assert.AreEqual(newpercentageownership, fields["percentageownership"]);
            Assert.AreEqual(newreference, fields["reference"]);
            Assert.AreEqual(newshowreferenceinschedule, fields["showreferenceinschedule"]);
        }

        #endregion

        #region Creating records

        [Test]
        [Property("JiraIssueID", "CDV6-6836")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Allowable Expense as the detail type - select 'Additional Personal Allowance - Variable' as the financial detail - Set values for Amount and remaining fields - " +
            "Tap on the save button - Validate that the record is correctly saved")]
        public void PersonFinancialDetails_TestMethod18()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Additional Personal Allowance - V").TapSearchButtonQuery().TapOnRecord("Additional Personal Allowance - Variable");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertAmount("37.76")
                .InsertJointAmount("98.76")
                .InsertStartDate("01/10/2020")
                .InsertEndDate("31/10/2020")
                .TapFrequencyOfReceiptLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Per F").TapSearchButtonQuery().TapOnRecord("Per Fortnight");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertReference("1")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapOnSaveButton()
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Additional Personal Allowance - Variable, 01/10/2020, 31/10/2020");


            var records = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID);
            Assert.AreEqual(1, records.Count);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("Additional Personal Allowance - Variable")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 37.76M;
            var jointamount = 98.76M;
            var showreferenceinschedule = true;
            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(records[0], "ownerid", "personid", "financialdetailid", "frequencyofreceiptid", "startdate", "enddate", "financialdetailtypeid", "reference", "amount", "jointamount", "showreferenceinschedule");

            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(frequencyofreceiptid, fields["frequencyofreceiptid"]); 
            Assert.AreEqual(startdate, fields["startdate"]);
            Assert.AreEqual(enddate, fields["enddate"]);
            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(reference, fields["reference"]); 
            Assert.AreEqual(amount, fields["amount"]); 
            Assert.AreEqual(jointamount, fields["jointamount"]);
            Assert.AreEqual(showreferenceinschedule, fields["showreferenceinschedule"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6837")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Allowable Expense as the detail type - select 'AE (Multiple Rates)' as the financial detail - Set values for Amount and remaining fields - " +
            "Validate that the amount field is set automatically")]
        public void PersonFinancialDetails_TestMethod19()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("AE (M").TapSearchButtonQuery().TapOnRecord("AE (Multiple Rates)");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertJointAmount("98.76")
                .InsertStartDate("01/10/2020")
                .InsertEndDate("31/10/2020")
                .InsertReference("1")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .ValidateAmountFieldText("19.01");
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6838")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Allowable Expense as the detail type - select 'AE (Multiple Rates)' as the financial detail - Set values for Amount and remaining fields - " +
            "Tap on the save button - Validate that the record is correctly saved")]
        public void PersonFinancialDetails_TestMethod20()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("AE (M").TapSearchButtonQuery().TapOnRecord("AE (Multiple Rates)");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertJointAmount("98.76")
                .InsertStartDate("01/10/2020")
                .InsertEndDate("31/10/2020")
                .InsertReference("1")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapOnSaveButton()
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020");


            var records = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID);
            Assert.AreEqual(1, records.Count);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Week")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 19.01M;
            var jointamount = 98.76M;
            var showreferenceinschedule = true;
            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(records[0], "ownerid", "personid", "financialdetailid", "frequencyofreceiptid", "startdate", "enddate", "financialdetailtypeid", "reference", "amount", "jointamount", "showreferenceinschedule");

            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(frequencyofreceiptid, fields["frequencyofreceiptid"]);
            Assert.AreEqual(startdate, fields["startdate"]);
            Assert.AreEqual(enddate, fields["enddate"]);
            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(reference, fields["reference"]);
            Assert.AreEqual(amount, fields["amount"]);
            Assert.AreEqual(jointamount, fields["jointamount"]);
            Assert.AreEqual(showreferenceinschedule, fields["showreferenceinschedule"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6839")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Asset as the detail type - select 'Current Account' as the financial detail - Set values for Amount and remaining fields - " +
            "Tap on the save button - Validate that the record is correctly saved")]
        public void PersonFinancialDetails_TestMethod21()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Current A").TapSearchButtonQuery().TapOnRecord("Current Account");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertAmount("88.76")
                .InsertJointAmount("98.76")
                .InsertStartDate("01/10/2020")
                .InsertEndDate("31/10/2020")
                .InsertReference("1")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapOnSaveButton()
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Current Account, 01/10/2020, 31/10/2020");


            var records = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID);
            Assert.AreEqual(1, records.Count);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Week")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("Current Account")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 2;
            var reference = "1";
            var amount = 88.76M;
            var jointamount = 98.76M;
            var showreferenceinschedule = true;
            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(records[0], "ownerid", "personid", "financialdetailid", "startdate", "enddate", "financialdetailtypeid", "reference", "amount", "jointamount", "showreferenceinschedule");

            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(startdate, fields["startdate"]);
            Assert.AreEqual(enddate, fields["enddate"]);
            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(reference, fields["reference"]);
            Assert.AreEqual(amount, fields["amount"]);
            Assert.AreEqual(jointamount, fields["jointamount"]);
            Assert.AreEqual(showreferenceinschedule, fields["showreferenceinschedule"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6840")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Benefit & Income as the detail type - select 'Attendance Allowance - High' as the financial detail - Set values for the remaining fields - " +
            "Tap on the save button - Validate that the record is correctly saved")]
        public void PersonFinancialDetails_TestMethod22()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(3).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Attendance Allowance").TapSearchButtonQuery().TapOnRecord("Attendance Allowance - High");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertStartDate("01/10/2020")
                .InsertEndDate("31/10/2020")
                .InsertAmount("55.21")
                .InsertReference("1")
                .InsertApplicationDate("09/10/2020")
                .InsertArrears("12.95")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapOnSaveButton()
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Attendance Allowance - High, 01/10/2020, 31/10/2020");


            var records = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID);
            Assert.AreEqual(1, records.Count);

            
            var financialdetailtypeid = 3;
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("Attendance Allowance - High")[0];
            var amount = 55.21M;
            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Week")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var beingreceived = true;
            var reference = "1";
            var applicationdate = new DateTime(2020, 10, 9);
            var showreferenceinschedule = true;
            var arrears = 12.95M;

            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(records[0], "ownerid", "personid", "financialdetailtypeid", "financialdetailid", "amount", "frequencyofreceiptid", "startdate", "enddate", "beingreceived", "reference", "applicationdate", "showreferenceinschedule", "arrears");

            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(amount, fields["amount"]);
            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(frequencyofreceiptid, fields["frequencyofreceiptid"]);
            Assert.AreEqual(startdate, fields["startdate"]);
            Assert.AreEqual(enddate, fields["enddate"]);
            Assert.AreEqual(beingreceived, fields["beingreceived"]);
            Assert.AreEqual(reference, fields["reference"]);
            Assert.AreEqual(applicationdate, fields["applicationdate"]);
            Assert.AreEqual(showreferenceinschedule, fields["showreferenceinschedule"]);
            Assert.AreEqual(arrears, fields["arrears"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6841")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Benefit & Income as the detail type - select 'Bereavement Allowance - Variable' as the financial detail - Set values for the remaining fields - " +
            "Tap on the save button - Validate that the record is correctly saved")]
        public void PersonFinancialDetails_TestMethod23()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(3).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Bereavement Allowance").TapSearchButtonQuery().TapOnRecord("Bereavement Allowance - Variable");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertAmount("19.18")
                .InsertStartDate("01/10/2020")
                .InsertEndDate("31/10/2020")
                .InsertReference("1")
                .InsertApplicationDate("09/10/2020")
                .InsertArrears("12.95")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapOnSaveButton()
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Bereavement Allowance - Variable, 01/10/2020, 31/10/2020");


            var records = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID);
            Assert.AreEqual(1, records.Count);


            var financialdetailtypeid = 3;
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("Bereavement Allowance - Variable")[0];
            var amount = 19.18M;
            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Week")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var beingreceived = true;
            var reference = "1";
            var applicationdate = new DateTime(2020, 10, 9);
            var showreferenceinschedule = true;
            var arrears = 12.95M;

            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(records[0], "ownerid", "personid", "financialdetailtypeid", "financialdetailid", "amount", "frequencyofreceiptid", "startdate", "enddate", "beingreceived", "reference", "applicationdate", "showreferenceinschedule", "arrears");

            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(amount, fields["amount"]);
            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(frequencyofreceiptid, fields["frequencyofreceiptid"]);
            Assert.AreEqual(startdate, fields["startdate"]);
            Assert.AreEqual(enddate, fields["enddate"]);
            Assert.AreEqual(beingreceived, fields["beingreceived"]);
            Assert.AreEqual(reference, fields["reference"]);
            Assert.AreEqual(applicationdate, fields["applicationdate"]);
            Assert.AreEqual(showreferenceinschedule, fields["showreferenceinschedule"]);
            Assert.AreEqual(arrears, fields["arrears"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6842")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Property as the detail type - Select 'Land' as the financial detail - Set values for the remaining fields - " +
            "Tap on the save button - Validate that the record is correctly saved")]
        public void PersonFinancialDetails_TestMethod24()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(4).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Land");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertStartDate("01/10/2020")
                .InsertEndDate("31/10/2020")
                .InsertAddress("address value")
                .InsertGrossValue("5000")
                .InsertOutstandingLoan("1000")
                .InsertOwnershipField("90")
                .InsertReference("1")
                .TapShowReferenceInScheduleField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapPropertyDisregardTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Property Disregard Type 1");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapExcludeFromDWPCalculationField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .ValidateEquityFieldText("4000")
                .TapOnSaveButton()
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Land, 01/10/2020, 31/10/2020");


            var records = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID);
            Assert.AreEqual(1, records.Count);


            var financialdetailtypeid = 4;
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("Land")[0];
            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);

            var address = "address value";
            var propertydisregardtypeid = PlatformServicesHelper.propertyDisregardType.GetPropertyDisregardTypeByName("Property Disregard Type 1")[0];
            var excludefromdwpcalculation = true;
            var grossvalue = 5000M;
            var outstandingloan = 1000M;
            var equity = 4000M;
            var percentageownership = 90;

            var reference = "1";
            var showreferenceinschedule = true;

            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(records[0], "ownerid", "personid", "financialdetailtypeid", "financialdetailid", "amount", "frequencyofreceiptid", "startdate", "enddate", "address", "propertydisregardtypeid", "excludefromdwpcalculation", "grossvalue", "outstandingloan", "equity", "percentageownership", "beingreceived", "reference", "applicationdate", "showreferenceinschedule", "arrears");

            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(startdate, fields["startdate"]);
            Assert.AreEqual(enddate, fields["enddate"]);

            Assert.AreEqual(address, fields["address"]);
            Assert.AreEqual(propertydisregardtypeid, fields["propertydisregardtypeid"]);
            Assert.AreEqual(excludefromdwpcalculation, fields["excludefromdwpcalculation"]);
            Assert.AreEqual(grossvalue, fields["grossvalue"]);
            Assert.AreEqual(outstandingloan, fields["outstandingloan"]);
            Assert.AreEqual(equity, fields["equity"]);
            Assert.AreEqual(percentageownership, fields["percentageownership"]);

            Assert.AreEqual(reference, fields["reference"]);
            Assert.AreEqual(showreferenceinschedule, fields["showreferenceinschedule"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6843")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Property as the detail type - Set all data except for Financial Detail Field - tap on the save button - validate that the user is prevented from saving the record")]
        public void PersonFinancialDetails_TestMethod25()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(4).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertStartDate("01/10/2020")
                .InsertAddress("address value")
                .InsertGrossValue("5000")
                .InsertOwnershipField("90")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Financial Detail' is required").TapOnOKButton();
            
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6844")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Property as the detail type - Set all data except for Start Date Field - Tap on the save button - Validate that the user is prevented from saving the record")]
        public void PersonFinancialDetails_TestMethod26()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(4).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Land");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertAddress("address value")
                .InsertGrossValue("5000")
                .InsertOwnershipField("90")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Start Date' is required").TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6845")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Property as the detail type - Set all data except for Address Field - Tap on the save button - Validate that the user is prevented from saving the record")]
        public void PersonFinancialDetails_TestMethod28()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(4).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Land");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertStartDate("01/10/2020")
                .InsertGrossValue("5000")
                .InsertOwnershipField("90")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Address' is required").TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6846")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Property as the detail type - Set all data except for Gross value Field - Tap on the save button - Validate that the user is prevented from saving the record")]
        public void PersonFinancialDetails_TestMethod29()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(4).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Land");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertStartDate("01/10/2020")
                .InsertAddress("address value")
                .InsertOwnershipField("90")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field 'Gross Value' is required").TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6847")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Property as the detail type - Set all data except for % Ownership Field - Tap on the save button - Validate that the user is prevented from saving the record")]
        public void PersonFinancialDetails_TestMethod30()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(4).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Land");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertStartDate("01/10/2020")
                .InsertAddress("address value")
                .InsertGrossValue("5000")
                .TapOnSaveButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The field '% Ownership' is required").TapOnOKButton();

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6848")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Open a person record - navigate to the person financial details page - Tap on the Add new record button - " +
            "Select Property as the detail type - Select 'Land' as the financial detail - Set values for Address, gross Value and Ownership - " +
            "Change the financial detail type to Allowable Expense - Set all mandatory fields - tap on the save button" +
            "Validate that the record is correctly saved - validate that the Address, Gross Value and Ownership fields are not saved")]
        public void PersonFinancialDetails_TestMethod32()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnAddNewRecordButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(4).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Land");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertStartDate("01/10/2020")
                .InsertEndDate("31/10/2020")
                .InsertAddress("address value")
                .InsertGrossValue("5000")
                .InsertOutstandingLoan("1000")
                .InsertOwnershipField("90")
                .InsertReference("1")
                ////////////////////////////////////////////////////////////////////////////////// CHANGE THE financial detail type from Property to Allowable Expense
                .TapFinancialDetailTypeField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(3).TapOKButton();

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .TapFinancialDetailLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Additional Personal Allowance - V").TapSearchButtonQuery().TapOnRecord("Additional Personal Allowance - Variable");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertAmount("37.76")
                .InsertJointAmount("98.76")
                .TapFrequencyOfReceiptLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Per F").TapSearchButtonQuery().TapOnRecord("Per Fortnight");

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAILS")
                .InsertReference("1")
                .TapOnSaveButton()
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, Additional Personal Allowance - Variable, 01/10/2020, 31/10/2020");


            var records = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID);
            Assert.AreEqual(1, records.Count);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("Additional Personal Allowance - Variable")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 37.76M;
            var jointamount = 98.76M;
            var showreferenceinschedule = false;
            var fields = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByID(records[0], 
                "ownerid", "personid", "financialdetailid", "frequencyofreceiptid", "startdate", "enddate", "financialdetailtypeid", "reference", "amount", "jointamount", "showreferenceinschedule", 
                "address", "grossvalue", "percentageownership");

            Assert.AreEqual(ownerid, fields["ownerid"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(financialdetailid, fields["financialdetailid"]);
            Assert.AreEqual(frequencyofreceiptid, fields["frequencyofreceiptid"]);
            Assert.AreEqual(startdate, fields["startdate"]);
            Assert.AreEqual(enddate, fields["enddate"]);
            Assert.AreEqual(financialdetailtypeid, fields["financialdetailtypeid"]);
            Assert.AreEqual(reference, fields["reference"]);
            Assert.AreEqual(amount, fields["amount"]);
            Assert.AreEqual(jointamount, fields["jointamount"]);
            Assert.AreEqual(showreferenceinschedule, fields["showreferenceinschedule"]);
            
            Assert.AreEqual(false, fields.ContainsKey("address"));
            Assert.AreEqual(false, fields.ContainsKey("grossvalue"));
            Assert.AreEqual(false, fields.ContainsKey("percentageownership"));
        }

        #endregion

        #region Delete

        [Test]
        [Property("JiraIssueID", "CDV6-6849")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-4875" +
            "Person has an Allowable Expense - Open a person record - Navigate to the person financial details page - " +
            "Open the person financial detail record - Tap on the delete button - Confirm the delete operation - Validate that the record was deleted")]
        public void PersonFinancialDetails_TestMethod31()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline

            foreach (var pfdID in PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID))
                PlatformServicesHelper.personFinancialDetail.DeletePersonFinancialDetail(pfdID);

            var ownerid = PlatformServicesHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var financialdetailid = PlatformServicesHelper.financialDetail.GetFinancialDetailByName("AE (Multiple Rates)")[0];
            var frequencyofreceiptid = PlatformServicesHelper.frequencyOfReceipt.GetFrequencyOfReceiptByName("Per Fortnight")[0];
            var startdate = new DateTime(2020, 10, 1);
            var enddate = new DateTime(2020, 10, 31);
            var financialdetailtypeid = 1;
            var reference = "1";
            var amount = 33.80M;
            var jointamount = 65.15M;
            var beingreceived = true;
            var excludefromdwpcalculation = false;
            var showreferenceinschedule = false;
            var deferredpaymentschemesecurity = false;

            var personFinancialDetailID = PlatformServicesHelper.personFinancialDetail.CreatePersonFinancialDetail(ownerid, personID, financialdetailid, frequencyofreceiptid, startdate, enddate, financialdetailtypeid, reference, amount, jointamount, beingreceived, excludefromdwpcalculation, showreferenceinschedule, deferredpaymentschemesecurity);


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapFinanceArea_RelatedItems()
                .TapFinanceDetailsIcon_RelatedItems();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad()
                .TapOnRecord(personFinancialDetailID.ToString());

            personFinancialDetailRecordPage
                .WaitForPersonFinancialDetailRecordPageToLoad("PERSON FINANCIAL DETAIL: Maria Tsatsouline, AE (Multiple Rates), 01/10/2020, 31/10/2020")
                .TapOnDeleteButton();

            warningPopup.WaitForWarningPopupToLoad().ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?").TapOnYesButton();

            personFinancialDetailsPage
                .WaitForPersonFinancialDetailsPageToLoad();

            var records = PlatformServicesHelper.personFinancialDetail.GetPersonFinancialDetailByPersonID(personID);
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
