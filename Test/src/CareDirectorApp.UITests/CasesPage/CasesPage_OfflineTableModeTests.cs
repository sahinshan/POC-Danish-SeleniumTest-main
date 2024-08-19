using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;
using System.Globalization;

namespace CareDirectorApp.UITests.Cases
{
    /// <summary>
    /// All tests in this validate the mobile app when it is NOT displayed in mobile mode
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class CasesPage_OfflineTableModeTests : TestBase
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



        [Test]
        [Property("JiraIssueID", "CDV6-6576")]
        [Description("UI Test for 'Cases' (Offline Mode) Scenario 1 - Open the Cases page")]
        public void CasesPage_OfflineTestMethod01()
        {
            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            //wait for the Cases page to load
            casesPage
                .WaitForCasesPageToLoad();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-6577")]
        [Description("UI Test for 'Cases' (Offline Mode) Scenario 2 - Validate 'Linked As An Involvement' view")]
        public void CasesPage_OfflineTestMethod02()
        {
            Guid caseID = new Guid("bb09bd69-4f9e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5307

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

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
        [Property("JiraIssueID", "CDV6-6578")]
        [Description("UI Test for 'Cases' (Offline Mode) Scenario 3 - Validate 'My Active Records' view")]
        public void CasesPage_OfflineTestMethod03()
        {
            Guid caseID = new Guid("eabd4b33-679e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5309

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

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
        [Property("JiraIssueID", "CDV6-6579")]
        [Description("UI Test for 'Cases' (Offline Mode) Scenario 5 - Validate search button")]
        public void CasesPage_OfflineTestMethod05()
        {

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TypeInSearchTextBox("MCSenna")
                .TapSearchButton()
                .WaitForLoadSymbolToBeRemoved()
                .VerifyThatTextVisible("Mathews MCSenna")
                .VerifyThatTextNotVisible("Pavel MCNamara");
        }






        [Test]
        [Property("JiraIssueID", "CDV6-6580")]
        [Description("UI Test for 'Cases' (Offline Mode) Scenario 9 - Open a 'SocialCareCase' Case record - Validate all fields data")]
        public void CasesPage_OfflineTestMethod09()
        {
            Guid caseID = new Guid("5a081e8c-669e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5308

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();



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
        [Property("JiraIssueID", "CDV6-6581")]
        [Description("UI Test for 'Cases' (Offline Mode) Scenario 10 - Open a 'CommunityHealthCase' Case record - Validate all fields data")]
        public void CasesPage_OfflineTestMethod10()
        {
            Guid caseID = new Guid("bb09bd69-4f9e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5307

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();



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
        [Property("JiraIssueID", "CDV6-6582")]
        [Description("UI Test for 'Cases' (Offline Mode) Scenario 11 - Open a 'InpatientCase' Case record - Validate all fields data")]
        public void CasesPage_OfflineTestMethod11()
        {
            Guid caseID = new Guid("eabd4b33-679e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5309

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();



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
                .ValidateInpatientCase_InpatientStatusField("Admission")

                .ValidateInpatientCase_ContactSourceField("Family")
                .ValidateInpatientCase_ContactMadeByField("Mathews MCSenna")
                .ValidateInpatientCase_AdministrativeCategoryField("Amenity Patient")
                .ValidateInpatientCase_WhoWasNotifiedField("")
                .ValidateInpatientCase_AdmissionSourceField("Sources of Admission 1")
                .ValidateInpatientCase_PatientClassificationField("Patient Classification 1")
                .ValidateInpatientCase_DoesPersonWishNOKOrCarerToBeNotifiedOfAdmissionField("No")
                .ValidateInpatientCase_IntendedManagementField("Intended Management Option 1")

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
        [Property("JiraIssueID", "CDV6-6583")]
        [Description("UI Test for 'Cases' (Offline Mode) Scenario 15 - open Case record and validate top banner")]
        public void CasesPage_OfflineTestMethod15()
        {
            int _personNumber = 500581;
            Guid caseID = new Guid("eabd4b33-679e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5309

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();



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
        [Property("JiraIssueID", "CDV6-6584")]
        [Description("UI Test for 'Cases' (Offline Mode) Scenario 16 - open Case record and expand top banner")]
        public void CasesPage_OfflineTestMethod16()
        {
            int _personNumber = 500581;
            Guid caseID = new Guid("eabd4b33-679e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5309

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();



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
        [Property("JiraIssueID", "CDV6-6585")]
        [Description("UI Test for 'Cases' (Offline Mode) Scenario 18 - Tap Responsible Team Field")]
        public void CasesPage_OfflineTestMethod18()
        {
            Guid caseID = new Guid("eabd4b33-679e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5309

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();



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
        [Property("JiraIssueID", "CDV6-6586")]
        [Description("UI Test for 'Cases' (Offline Mode) Scenario 21 - Tap Responsible Team Field - Tap on the Back button")]
        public void CasesPage_OfflineTestMethod21()
        {
            Guid caseID = new Guid("eabd4b33-679e-e911-a2c6-005056926fe4"); //QA-CAS-000001-5309

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();



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
