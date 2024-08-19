using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.Cases
{
    /// <summary>
    /// All tests in this validate the mobile app when it is NOT displayed in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class CasesPage_TableModeTests : TestBase
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

            //if the person body map injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the person body map review pop-up is open then close it 
            personBodyMapReviewPopup.ClosePopupIfOpen();

            //if the error popup is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning popup is open close it
            warningPopup.CloseWarningPopupIfOpen();

            //navigate to the Cases page
            mainMenu.NavigateToCasesPage();
        }



        [Test]
        [Property("JiraIssueID", "CDV6-6587")]
        [Description("UI Test for 'Cases' Scenario 1 - Open the Cases page")]
        public void CasesPage_TestMethod01()
        {
            //wait for the Cases page to load
            casesPage
                .WaitForCasesPageToLoad();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-6588")]
        [Description("UI Test for 'Cases' Scenario 2 - Validate 'Linked As An Involvement' view")]
        public void CasesPage_TestMethod02()
        {
            Guid caseID = new Guid("bb09bd69-4f9e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5307

            //
            casesPage
                .WaitForCasesPageToLoad()
                .ValidatePersonCellText("Mathews MCSenna", caseID.ToString())
                .ValidateContactReaonCellText("Ongoing diabetics", caseID.ToString())
                .ValidateNHSNOCellText("987 654 3210", caseID.ToString())
                .ValidateLastNameCellText("MCSenna", caseID.ToString())
                .ValidateFirstNameCellText("Mathews", caseID.ToString())
                .ValidateDOBCellText("01/01/2000", caseID.ToString())
                .ValidateDateTimeRequestRecievedCellText("02/07/2019 11:00", caseID.ToString())
                .ValidatePresentingPriorityCellText("Other", caseID.ToString())
                .ValidateCaseStatusCellText("First Appointment Booked", caseID.ToString())
                .ValidateSecundaryCaseReasonCellText("Medication Review", caseID.ToString())
                .ValidateCaseNumberCellText("QA-CAS-000001-5307", caseID.ToString())
                .ValidateResponsibleTeamCellText("Mobile Team 1", caseID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", caseID.ToString())
                .ValidateCreatedOnCellText("04/07/2019 12:32", caseID.ToString())
                .ValidateModifiedByCellText("Mobile Test User 1", caseID.ToString())
                .ValidateModifiedOnCellText("05/07/2019 10:36", caseID.ToString());

        }


        [Test]
        [Property("JiraIssueID", "CDV6-6589")]
        [Description("UI Test for 'Cases' Scenario 3 - Validate 'My Active Records' view")]
        public void CasesPage_TestMethod03()
        {
            Guid caseID = new Guid("eabd4b33-679e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5309


            casesPage
                .WaitForCasesPageToLoad()
                .TapViewPicker();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(1)
                .TapOKButton();

            casesPage
                .WaitForCasesPageToLoad("My Active Records")
                .ValidatePersonCellText("Mathews MCSenna", caseID.ToString())
                .ValidateContactReaonCellText("Depression", caseID.ToString())
                .ValidateNHSNOCellText("987 654 3210", caseID.ToString())
                .ValidateLastNameCellText("MCSenna", caseID.ToString())
                .ValidateFirstNameCellText("Mathews", caseID.ToString())
                .ValidateDOBCellText("01/01/2000", caseID.ToString())
                .ValidateDateTimeRequestRecievedCellText("", caseID.ToString())
                .ValidatePresentingPriorityCellText("", caseID.ToString())
                .ValidateCaseStatusCellText("Admission", caseID.ToString())
                .ValidateSecundaryCaseReasonCellText("Inpatient Reason_1", caseID.ToString())
                .ValidateCaseNumberCellText("QA-CAS-000001-5309", caseID.ToString())
                .ValidateResponsibleTeamCellText("Mobile Team 1", caseID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", caseID.ToString())
                .ValidateCreatedOnCellText("04/07/2019 15:22", caseID.ToString())
                .ValidateModifiedByCellText("Mobile Test User 1", caseID.ToString());

        }


        [Test]
        [Property("JiraIssueID", "CDV6-6590")]
        [Description("UI Test for 'Cases' Scenario 4 - Validate refresh button")]
        public void CasesPage_TestMethod04()
        {
            Guid personID = new Guid("5e6cc2ab-479e-e911-a2c6-005056926fe4"); //Mr Mathews MCSenna
            Guid caseID = new Guid("5a081e8c-669e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5308
            Guid mobile_test_user_1ID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1
            Guid mobileTeam1ID = new Guid("0bffb4b6-429e-e911-a2c6-005056926fe4"); //Mobile Team 1
            Guid invovementRoleID = new Guid("b33fd920-fd90-e911-a2c6-005056926fe4"); //Care Coordinator


            //Delete involvement
            foreach (Guid recordID in this.PlatformServicesHelper.caseInvolvement.GetCaseInvolvementsByCaseAndUserID(caseID, mobile_test_user_1ID))
                this.PlatformServicesHelper.caseInvolvement.DeleteCaseInvolvement(recordID);

            //wait for the page to load
            casesPage
                .WaitForCasesPageToLoad()
                .TapRefreshButton()
                .VerifyThatTextNotVisible("QA-CAS-000001-5308");


            //Create involvement for the user

            PlatformServicesHelper.caseInvolvement.CreateCaseInvolvement(mobileTeam1ID, mobile_test_user_1ID, "systemuser", "Mobile Test User 1", invovementRoleID, DateTime.Now.AddDays(-1).Date, caseID, personID, false);


            //refresh the page and validate that the record is present
            casesPage
                .WaitForCasesPageToLoad()
                .TapRefreshButton()
                .ValidatePersonCellText("Mathews MCSenna", caseID.ToString())
                .ValidateContactReaonCellText("Advice/Consultation", caseID.ToString())
                .ValidateNHSNOCellText("987 654 3210", caseID.ToString())
                .ValidateLastNameCellText("MCSenna", caseID.ToString())
                .ValidateFirstNameCellText("Mathews", caseID.ToString())
                .ValidateDOBCellText("01/01/2000", caseID.ToString())
                .ValidateDateTimeRequestRecievedCellText("", caseID.ToString())
                .ValidatePresentingPriorityCellText("Other", caseID.ToString())
                .ValidateCaseStatusCellText("Allocate to Team", caseID.ToString())
                .ValidateSecundaryCaseReasonCellText("", caseID.ToString())
                .ValidateCaseNumberCellText("QA-CAS-000001-5308", caseID.ToString());

        }


        [Test]
        [Property("JiraIssueID", "CDV6-6591")]
        [Description("UI Test for 'Cases' Scenario 5 - Validate search button")]
        public void CasesPage_TestMethod05()
        {
            casesPage
                .WaitForCasesPageToLoad()
                .TypeInSearchTextBox("*MCSenna*")
                .TapSearchButton()
                .WaitForLoadSymbolToBeRemoved()
                .VerifyThatTextVisible("Mathews MCSenna")
                .VerifyThatTextNotVisible("Pavel MCNamara");
        }






        [Test]
        [Property("JiraIssueID", "CDV6-6592")]
        [Description("UI Test for 'Cases' Scenario 9 - Open a 'SocialCareCase' Case record - Validate all fields data")]
        public void CasesPage_TestMethod09()
        {
            Guid caseID = new Guid("5a081e8c-669e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5308


            //refresh the page and validate that the record is present
            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Mathews MCSenna", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCSenna, Mathews - (01/01/2000) [QA-CAS-000001-5308]")

                .ValidateSocialCareCase_CaseNoField("QA-CAS-000001-5308")
                .ValidateSocialCareCase_PersonField("Mathews MCSenna")
                .ValidateSocialCareCase_PersonAgeField("19")
                .ValidateSocialCareCase_CaseDateTimeField("04/07/2019 08:00")
                .ValidateSocialCareCase_InitialContactField("")
                .ValidateSocialCareCase_DateTimeContactReceivedField("04/07/2019 08:30")
                .ValidateSocialCareCase_ContactReceivedByField("Mobile Test User 1")
                .ValidateSocialCareCase_ContactReasonField("Advice/Consultation")
                .ValidateSocialCareCase_PresentingPriorityField("Other")
                .ValidateSocialCareCase_CINCodeField("Absent parenting")
                .ValidateSocialCareCase_AdditionalInformationField("ai...")

                .ValidateSocialCareCase_ContactMadeByField("Mathews MCSenna")
                .ValidateSocialCareCase_ContactMadeByFreeTextField("cmbft")
                .ValidateSocialCareCase_CaseOriginField("In Person")
                .ValidateSocialCareCase_ContactSourceField("Family")

                .ValidateSocialCareCase_IsThePersonAwareOfTheContactField("Yes")
                .ValidateSocialCareCase_DoesPersonAgreeSupportThisContactField("Yes")
                .ValidateSocialCareCase_IsParentCarerAwareOfThisContactField("")
                .ValidateSocialCareCase_DoesParentCarerAgreeSupportThisContactField("")
                .ValidateSocialCareCase_IsNOKCarerAwareOfThisContactField("Yes")

                .ValidateSocialCareCase_CaseStatusField("Allocate to Team")
                .ValidateSocialCareCase_CasePriorityField("Routine")
                .ValidateSocialCareCase_ResponsibleUserField("Mobile Test User 2")
                .ValidateSocialCareCase_ResponsibleTeamField("Mobile Team 1")
                .ValidateSocialCareCase_ReviewDateField("05/07/2019")
                .ValidateSocialCareCase_CloseDateField("")
                .ValidateSocialCareCase_ClosureReasonField("")
                .ValidateSocialCareCase_ClosureAcceptedByField("")
                .ValidateSocialCareCase_ArchieDateField("")

                .ValidateSocialCareCase_ReReferralField("No")
                .ValidateSocialCareCase_ReferringAgencyCaseIDField("")
                .ValidateSocialCareCase_ResponseMadeToContactField("No")
                .ValidateSocialCareCase_NonMigratedWorkerNameField("")
                .ValidateSocialCareCase_DateAndTimeOfContactWithTraineStaffField("")
                
                .ValidateSocialCareCase_PoliceNotifiedField("No")
                .ValidateSocialCareCase_PoliceNotifiedDateField("")
                .ValidateSocialCareCase_PoliceNotesField("")

                .ValidateCreatedByFooterField("Mobile Test User 1")
                .ValidateCreatedOnFooterField("04/07/2019 15:18");

        }


        [Test]
        [Property("JiraIssueID", "CDV6-6593")]
        [Description("UI Test for 'Cases' Scenario 10 - Open a 'CommunityHealthCase' Case record - Validate all fields data")]
        public void CasesPage_TestMethod10()
        {
            Guid caseID = new Guid("bb09bd69-4f9e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5307


            //refresh the page and validate that the record is present
            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Mathews MCSenna", caseID.ToString());


            casePage
                .WaitForCasePageToLoad("MCSenna, Mathews - (01/01/2000) [QA-CAS-000001-5307]")

                .ValidateCommunityHealtCase_CaseNoField("QA-CAS-000001-5307")
                .ValidateCommunityHealtCase_PersonField("Mathews MCSenna")
                .ValidateCommunityHealtCase_PersonAgeField("19")
                .ValidateCommunityHealtCase_InitialContactField("Single Point of Access (SPOA) for Mr Mathews MCSenna received 04/07/2019 referred by Family")
                .ValidateCommunityHealtCase_OtherRelatedContactField("Test Contact Type_2 for Mr Mathews MCSenna received 01/07/2019 referred by Friend")
                .ValidateCommunityHealtCase_DateTimeContactReceivedField("01/07/2019 09:00")
                .ValidateCommunityHealtCase_ContactReceivedByField("Mobile Test User 1")
                .ValidateCommunityHealtCase_DateTimeRequestReceivedField("02/07/2019 11:00")
                .ValidateCommunityHealtCase_ResponsibleTeamField("Mobile Team 1")
                .ValidateCommunityHealtCase_ResponsibleUserField("Mobile Test User 1")
                .ValidateCommunityHealtCase_ContactReasonField("Ongoing diabetics")
                .ValidateCommunityHealtCase_PresentingPriorityField("Other")
                .ValidateCommunityHealtCase_SecondaryCaseReasonField("Medication Review")
                .ValidateCommunityHealtCase_PresentingPriorityField("Other")
                .ValidateCommunityHealtCase_PresentingNeedField("pn...")

                .ValidateCommunityHealtCase_ContactSourceField("Family")
                .ValidateCommunityHealtCase_AdministrativeCategoryField("NHS Patient")
                .ValidateCommunityHealtCase_CaseTransferredFromField("")
                .ValidateCommunityHealtCase_ContactMadeByField("Mathews MCSenna")
                .ValidateCommunityHealtCase_ContactMadeByFreeTextField("cmb...")                

                .ValidateCommunityHealtCase_IsThePersonAwareOfTheContactField("Yes")
                .ValidateCommunityHealtCase_IsNOKCarerAwareOfThisContactField("Yes")

                .ValidateCommunityHealtCase_CommunityClinicTeamRequiredField("Mobile Test Clinic Team")
                .ValidateCommunityHealtCase_CaseAcceptedField("Yes")
                .ValidateCommunityHealtCase_CasePriorityField("")

                .ValidateCommunityHealtCase_PathwayKeySourceField("New Pathway Key")

                .ValidateCommunityHealtCase_ServiceTypeRequestedField("Advice and Consultation")
                .ValidateCommunityHealtCase_CNACountField("0")
                .ValidateCommunityHealtCase_CaseStatusField("First Appointment Booked")
                .ValidateCommunityHealtCase_DNACountField("0")
                
                .ValidateCommunityHealtCase_DischargePersonField("No")
                
                .ValidateCreatedByFooterField("Mobile Test User 1")
                .ValidateCreatedOnFooterField("04/07/2019 12:32");

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6594")]
        [Description("UI Test for 'Cases' Scenario 11 - Open a 'InpatientCase' Case record - Validate all fields data")]
        public void CasesPage_TestMethod11()
        {
            Guid caseID = new Guid("eabd4b33-679e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5309


            //refresh the page and validate that the record is present
            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Mathews MCSenna", caseID.ToString());


            casePage
                .WaitForCasePageToLoad("MCSenna, Mathews - (01/01/2000) [QA-CAS-000001-5309]")

                .ValidateInpatientCase_CaseNoField("QA-CAS-000001-5309")
                .ValidateInpatientCase_PersonField("Mathews MCSenna")
                .ValidateInpatientCase_InitialContactField("Single Point of Access (SPOA) for Mr Mathews MCSenna received 04/07/2019 referred by Family")
                .ValidateInpatientCase_OtherRelatedContactField("Single Point of Access (SPOA) for Mr Mathews MCSenna received 04/07/2019 referred by Family")
                .ValidateInpatientCase_DateTimeContactReceivedField("04/07/2019 09:00")
                .ValidateInpatientCase_ContactReceivedByField("Mobile Test User 1")
                .ValidateInpatientCase_ResponsibleUserField("Mobile Test User 1")
                .ValidateInpatientCase_ResponsibleTeamField("Mobile Team 1")
                .ValidateInpatientCase_ReasonForAdmissionField("Depression")
                .ValidateInpatientCase_SecondaryAdmissionReasonField("Inpatient Reason_1")
                .ValidateInpatientCase_InpatientStatusField("Admission");

            casePage
                .ValidateInpatientCase_ContactSourceField("Family")
                .ValidateInpatientCase_ContactMadeByField("Mathews MCSenna")
                .ValidateInpatientCase_AdministrativeCategoryField("Amenity Patient")
                .ValidateInpatientCase_WhoWasNotifiedField("")
                .ValidateInpatientCase_AdmissionSourceField("Sources of Admission 1")
                .ValidateInpatientCase_PatientClassificationField("Patient Classification 1")
                .ValidateInpatientCase_DoesPersonWishNOKOrCarerToBeNotifiedOfAdmissionField("No")
                .ValidateInpatientCase_IntendedManagementField("Intended Management Option 1");

            casePage
                .ValidateInpatientCase_OutlineNeedForAdmissionField("onoa")
                .ValidateInpatientCase_CriteriaForDischargeField("CFD...")
                .ValidateInpatientCase_AdmissionMethodField("Admission Method 1")

                .ValidateInpatientCase_ServiceTypeRequestedField("Advice/consultation")
                .ValidateInpatientCase_CurrentConsultatField("Mobile Test User 1")
                .ValidateInpatientCase_DoLSConcernField("Admitted under D.O.L.s")
                .ValidateInpatientCase_NamedProfessionalField("Mobile Test User 1")
                .ValidateInpatientCase_AdmissionDateTimeField("04/07/2019 10:00")
                .ValidateInpatientCase_LegalStatusOnAdmissionField("Adhoc Section")

                .ValidateInpatientCase_DecisionToAdmitAgreedDateTimeField("")
                .ValidateInpatientCase_IntendedAdmissionDateField("")
                .ValidateInpatientCase_ReasonForChangeIntendedAdmissionField("")
                .ValidateInpatientCase_DateIntendedAdmissionChangedField("")
                
                .ValidateInpatientCase_HospitalField("Blaenau Gwent - Hospital")
                .ValidateInpatientCase_WardField("Ward 1")
                .ValidateInpatientCase_BayRoomField("Bay 1")
                .ValidateInpatientCase_BedField("Bed 1 created by Mobile Test User 1 on 04/07/2019 14:22:31")
                .ValidateInpatientCase_ResponsibleWardField("Ward 1")
                .ValidateInpatientCase_TransferOfCareField("")
                .ValidateInpatientCase_ActualDateTimeOfTransferField("")

                .ValidateInpatientCase_PathwayKeySourceField("New Pathway Key")

                .ValidateInpatientCase_EstimatedDateOfDischargeField("")
                .ValidateInpatientCase_PlannedDateOfDischargeField("")
                .ValidateInpatientCase_PlannedDischargeDestinationField("")
                .ValidateInpatientCase_FitForDischargeDateField("")

                .ValidateInpatientCase_ActualDischargeDateTimeField("")
                .ValidateInpatientCase_DischargeMethodField("")
                .ValidateInpatientCase_ReasonNotAcceptedField("")
                .ValidateInpatientCase_DischargeInformationField("")
                .ValidateInpatientCase_Section117AftercareEntitlementField("No")
                .ValidateInpatientCase_CarerNoKNotifiedField("No")
                .ValidateInpatientCase_WishesContactFromAnIMHAField("")
                .ValidateInpatientCase_DischargeCloseDateField("")
                .ValidateInpatientCase_DischargedToHomAddressField("")

                .ValidateInpatientCase_PropertyNameield("")
                .ValidateInpatientCase_PropertyNoField("")
                .ValidateInpatientCase_StreetField("")
                .ValidateInpatientCase_VlgField("")
                .ValidateInpatientCase_PropertyTypeField("")
                .ValidateInpatientCase_TownField("")
                .ValidateInpatientCase_CountryField("")
                .ValidateInpatientCase_UPRNField("")
                .ValidateInpatientCase_BoroughField("")
                .ValidateInpatientCase_CountyField("")
                .ValidateInpatientCase_TempAddressWardField("")
                .ValidateInpatientCase_PostCodeField("")

                .ValidateCreatedByFooterField("Mobile Test User 1")
                .ValidateCreatedOnFooterField("04/07/2019 15:22");

        }




        [Test]
        [Property("JiraIssueID", "CDV6-6595")]
        [Description("UI Test for 'Cases' Scenario 15 - open Case record and validate top banner")]
        public void CasesPage_TestMethod15()
        {
            int _personNumber = 500581;
            Guid caseID = new Guid("eabd4b33-679e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5309


            //refresh the page and validate that the record is present
            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Mathews MCSenna", caseID.ToString());


            casePage
                .WaitForCasePageToLoad("MCSenna, Mathews - (01/01/2000) [QA-CAS-000001-5309]")
                .ValidateMainTopBannerLabelsVisible()
                .ValidateSecondayTopBannerLabelsNotVisible()
                .ValidatePersonNameAndId_TopBanner("MCSENNA, Mathews (Id " + _personNumber.ToString() + ")")
                .ValidateBornText_TopBanner(new DateTime(2000, 1, 1))
                .ValidateGenderText_TopBanner("Male")
                .ValidateNHSNoText_TopBanner("987 654 3210")
                .ValidatePreferredNameText_TopBanner("Alcordy");


        }


        [Test]
        [Property("JiraIssueID", "CDV6-6596")]
        [Description("UI Test for 'Cases' Scenario 16 - open Case record and expand top banner")]
        public void CasesPage_TestMethod16()
        {
            int _personNumber = 500581;
            Guid caseID = new Guid("eabd4b33-679e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5309


            //refresh the page and validate that the record is present
            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Mathews MCSenna", caseID.ToString());


            casePage
                .WaitForCasePageToLoad("MCSenna, Mathews - (01/01/2000) [QA-CAS-000001-5309]")
                .ExpandTopBanner()
                .ValidateMainTopBannerLabelsVisible()
                .ValidateSecondayTopBannerLabelsVisible()
                .ValidatePersonNameAndId_TopBanner("MCSENNA, Mathews (Id " + _personNumber.ToString() + ")")
                .ValidateBornText_TopBanner(new DateTime(2000, 1, 1))
                .ValidateGenderText_TopBanner("Male")
                .ValidateNHSNoText_TopBanner("987 654 3210")
                .ValidatePreferredNameText_TopBanner("Alcordy")
                .ValidateAddressText_TopBanner("PNA\nPNO ST \nVlg\nTO CO \nPC")
                .ValidateHomePhoneText_TopBanner("12342")
                .ValidateWorkPhoneText_TopBanner("12341")
                .ValidateMobilePhoneText_TopBanner("12343")
                .ValidateEmailText_TopBanner("MCSenna@mail.com");

        }


        [Test]
        [Property("JiraIssueID", "CDV6-6597")]
        [Description("UI Test for 'Cases' Scenario 18 - Tap Responsible Team Field")]
        public void CasesPage_TestMethod18()
        {
            Guid caseID = new Guid("eabd4b33-679e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5309


            //refresh the page and validate that the record is present
            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Mathews MCSenna", caseID.ToString());


            casePage
                .WaitForCasePageToLoad("MCSenna, Mathews - (01/01/2000) [QA-CAS-000001-5309]")
                .InpatientCase_TapResponsibleTeamField();

            teamPage
                .WaitForTeamPageToLoad("Mobile Team 1");


        }


        [Test]
        [Property("JiraIssueID", "CDV6-6598")]
        [Description("UI Test for 'Cases' Scenario 21 - Tap Responsible Team Field - Tap on the Back button")]
        public void CasesPage_TestMethod21()
        {
            Guid caseID = new Guid("eabd4b33-679e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5309


            //refresh the page and validate that the record is present
            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Mathews MCSenna", caseID.ToString());


            casePage
                .WaitForCasePageToLoad("MCSenna, Mathews - (01/01/2000) [QA-CAS-000001-5309]")
                .InpatientCase_TapResponsibleTeamField();

            teamPage
                .WaitForTeamPageToLoad("Mobile Team 1")
                .TapBackButton();

            casePage
                .WaitForCasePageToLoad("MCSenna, Mathews - (01/01/2000) [QA-CAS-000001-5309]");
        }



        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }



    }
}
