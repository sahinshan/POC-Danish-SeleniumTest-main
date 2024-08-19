using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.Activities
{
    /// <summary>
    /// This class contains all test methods for Person Appointments validations while the APP is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PersonAppointments_TabletModeTests : TestBase
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

            //if the cases PersonAppointment injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the cases PersonAppointment review pop-up is open then close it 
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

        #region Person Appointment page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6657")]
        [Description("UI Test for Person Appointments - 0001 - " +
            "Navigate to the Person Appointment area (do not contains Appointment records) - Validate the page content")]
        public void PersonAppointments_TestMethod001()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any PersonAppointment for the person
            foreach (Guid recordID in this.PlatformServicesHelper.appointment.GetAppointmentByPersonID(personID))
                this.PlatformServicesHelper.appointment.DeleteAppointment(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6658")]
        [Description("UI Test for Person Appointments - 0002 - " +
            "Navigate to the Person Appointment area (person contains Appointment records) - Validate the page content")]
        public void PersonAppointments_TestMethod002()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid appointmentID = new Guid("a3412ab1-32a0-ea11-a2cd-005056926fe4"); //Appointment 001

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .ValidateSubjectCellText("Appointment 001", appointmentID.ToString())
                .ValidateStartTimeCellText("17:00", appointmentID.ToString())
                .ValidateEndTimeCellText("17:05", appointmentID.ToString())
                .ValidateStartDateCellText("27/05/2020", appointmentID.ToString())
                .ValidateActivityCategoryCellText("Advice", appointmentID.ToString())
                .ValidateActivityReasonCellText("Other", appointmentID.ToString())
                .ValidateLocationCellText("Location information", appointmentID.ToString())
                .ValidateOwnerCellText("Mobile Team 1", appointmentID.ToString())
                .ValidateResponsibleUserCellText("Mobile Test User 1", appointmentID.ToString())
                
                ;
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6659")]
        [Description("UI Test for Person Appointments - 0003 - " +
            "Navigate to the Person Appointment area - Open a person Appointment record - Validate that the Person Appointment record page is displayed")]
        public void PersonAppointments_TestMethod003()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string appointmentSubject = "Appointment 001";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord(appointmentSubject);

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6660")]
        [Description("UI Test for Person Appointments - 0004 - " +
            "Navigate to the Person Appointment area - Open a person Appointment record - Validate that the Person Appointment record page field titles are displayed")]
        public void PersonAppointments_TestMethod004()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string appointmentSubject = "Appointment 001";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord(appointmentSubject);

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .ValidateSubjectFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateStartTimeFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateEndTimeFieldTitleVisible(true)
                .ValidateAppointmentTypeFieldTitleVisible(true)
                .ValidateCaseFieldTitleVisible(true)
                .ValidateStatusFieldTitleVisible(true)
                .ValidateOutcomeFieldTitleVisible(true)

                .ValidatePersonFieldTitleVisible(true)
                .ValidateLocationFieldTitleVisible(true)
                .ValidateRequiredFieldTitleVisible(true)
                .ValidateOptionalFieldTitleVisible(true)
                .ValidateMeetingNotesFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6661")]
        [Description("UI Test for Person Appointments - 0005 - " +
            "Navigate to the Person Appointment area - Open a person Appointment record - Validate that the Person Appointment record page fields are correctly displayed")]
        public void PersonAppointments_TestMethod005()
        {
            string appointmentSubject = "Appointment 001";
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid professionalID1 = new Guid("148AABA7-443F-E911-A2C5-005056926FE4"); //Brendan Morrison 
            Guid providerID1 = new Guid("6A34768A-3B9F-EA11-A2CD-005056926FE4"); //Advanced
            Guid systemUserID1 = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile Test User 1

            Guid personID2 = new Guid("5E6CC2AB-479E-E911-A2C6-005056926FE4"); //Mr Mathews MCsenna 
            Guid professionalID2 = new Guid("258AABA7-443F-E911-A2C5-005056926FE4"); //Yvonne Cash 
            Guid providerID2 = new Guid("CBC34B69-71E0-E911-A2C7-005056926FE4"); //Carer
            Guid systemUserID2 = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4"); //Mobile Test User 2


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord(appointmentSubject);

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .ValidateSubjectFieldText("Appointment 001")
                .ValidateStartDateFieldText("27/05/2020")
                .ValidateStartTimeRichFieldText("17:00")
                .ValidateEndDateFieldText("27/05/2020")
                .ValidateEndTimeFieldText("17:05")
                .ValidateAppointmentTypeFieldText("Conference")
                .ValidateCaseFieldText("")
                .ValidateStatusFieldText("Scheduled")
                .ValidateOutcomeFieldText("More information needed")

                .ValidatePersonFieldText("Pavel MCNamara")
                .ValidateLocationFieldText("Location information")
                .ValidateRequiredFieldEntryText(personID.ToString("N"), "Pavel MCNamara")
                .ValidateRequiredFieldEntryText(professionalID1.ToString("N"), "Brendan Morrison")
                .ValidateRequiredFieldEntryText(providerID1.ToString("N"), "Advanced")
                .ValidateRequiredFieldEntryText(systemUserID1.ToString("N"), "Mobile Test User 1")
                .ValidateOptionalFieldEntryText(personID2.ToString("N"), "Mathews MCSenna")
                .ValidateOptionalFieldEntryText(professionalID2.ToString("N"), "Yvonne Cash")
                .ValidateOptionalFieldEntryText(providerID2.ToString("N"), "Carer")
                .ValidateOptionalFieldEntryText(systemUserID2.ToString("N"), "Mobile Test User 2")
                .ValidateMeetingNotesRichTeaxtFieldText("Appointment 001 notes");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6662")]
        [Description("UI Test for Person Appointments - 0006 - " +
            "Navigate to the Person Appointment area - Open a person Appointment record (with only the mandatory information set) - Validate that the Person Appointment record page fields are correctly displayed")]
        public void PersonAppointments_TestMethod006()
        {
            string appointmentSubject = "Appointment 002";

            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid systemUserID1 = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile Test User 1

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord(appointmentSubject);

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 002")
                 .ValidateSubjectFieldText("Appointment 002")
                .ValidateStartDateFieldText("27/05/2020")
                .ValidateStartTimeRichFieldText("17:05")
                .ValidateEndDateFieldText("27/05/2020")
                .ValidateEndTimeFieldText("17:10")
                .ValidateAppointmentTypeFieldText("")
                .ValidateCaseFieldText("")
                .ValidateStatusFieldText("Scheduled")
                .ValidateOutcomeFieldText("")

                .ValidatePersonFieldText("Pavel MCNamara")
                .ValidateLocationFieldText("")
                .ValidateRequiredFieldEntryText(systemUserID1.ToString("N"), "Mobile Test User 1")
                .ValidateMeetingNotesFieldText("");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6663")]
        [Description("UI Test for Person Appointments - 0007 - " +
            "Navigate to the Person Appointment area - Open a person Appointment record (Person record is set in the Required field) - Tap on the Required field entry - Validate that the user is redirected to the person page")]
        public void PersonAppointments_TestMethod007()
        {
            string appointmentSubject = "Appointment 001";

            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid professionalID1 = new Guid("148AABA7-443F-E911-A2C5-005056926FE4"); //Brendan Morrison 
            Guid providerID1 = new Guid("6A34768A-3B9F-EA11-A2CD-005056926FE4"); //Advanced
            Guid systemUserID1 = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile Test User 1

            Guid personID2 = new Guid("5E6CC2AB-479E-E911-A2C6-005056926FE4"); //Mr Mathews MCsenna 
            Guid professionalID2 = new Guid("258AABA7-443F-E911-A2C5-005056926FE4"); //Yvonne Cash 
            Guid providerID2 = new Guid("CBC34B69-71E0-E911-A2C7-005056926FE4"); //Carer
            Guid systemUserID2 = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4"); //Mobile Test User 2

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord(appointmentSubject);

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .TapRequiredEntryField(personID.ToString("N"));

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapbackButton();

            personAppointmentRecordPage
               .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6664")]
        [Description("UI Test for Person Appointments - 0008 - " +
            "Navigate to the Person Appointment area - Open a person Appointment record (Professional record is set in the Required field) - Tap on the Required field entry - Validate that the user is redirected to the Professional page")]
        public void PersonAppointments_TestMethod008()
        {
            string appointmentSubject = "Appointment 001";

            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid professionalID1 = new Guid("148AABA7-443F-E911-A2C5-005056926FE4"); //Brendan Morrison 
            Guid providerID1 = new Guid("6A34768A-3B9F-EA11-A2CD-005056926FE4"); //Advanced
            Guid systemUserID1 = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile Test User 1

            Guid personID2 = new Guid("5E6CC2AB-479E-E911-A2C6-005056926FE4"); //Mr Mathews MCsenna 
            Guid professionalID2 = new Guid("258AABA7-443F-E911-A2C5-005056926FE4"); //Yvonne Cash 
            Guid providerID2 = new Guid("CBC34B69-71E0-E911-A2C7-005056926FE4"); //Carer
            Guid systemUserID2 = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4"); //Mobile Test User 2

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord(appointmentSubject);

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .TapRequiredEntryField(professionalID1.ToString("N"));

            professionalPage
                .WaitForProfessionalPageToLoad("Brendan Morrison")
                .ValidateProfessionalIdFieldTitleVisible()
                .ValidateTitleFieldTitleVisible()
                .ValidateFirstNameFieldTitleVisible()
                .ValidateLastNameFieldTitleVisible()
                .ValidateProfessionFieldTitleVisible()
                .ValidateBusinessPhoneFieldTitleVisible()
                .ValidateMobilePhoneTitleVisible()

                .ValidateProfessionalIdField("7194")
                .ValidateTitleField("Dr")
                .ValidateFirstNameField("Brendan")
                .ValidateLastNameField("Morrison")
                .ValidateProfessionField("Doctor")
                .ValidateBusinessPhoneField("340339026")
                .ValidateMobilePhoneField("315938297")

                .ValidateBusinessPhoneSMSButtonEnabled(true)
                .ValidateMobilePhoneSMSButtonEnabled(true)
                .ValidateBusinessPhoneCallButtonEnabled(true)
                .ValidateMobilePhoneCallButtonEnabled(true)

                .TapbackButton();

            personAppointmentRecordPage
               .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6665")]
        [Description("UI Test for Person Appointments - 0009 - " +
            "Navigate to the Person Appointment area - Open a person Appointment record (Provider record is set in the Required field) - Tap on the Required field entry - Validate that the user is redirected to the Provider page")]
        public void PersonAppointments_TestMethod009()
        {
            string appointmentSubject = "Appointment 001";

            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid professionalID1 = new Guid("148AABA7-443F-E911-A2C5-005056926FE4"); //Brendan Morrison 
            Guid providerID1 = new Guid("6A34768A-3B9F-EA11-A2CD-005056926FE4"); //Advanced
            Guid systemUserID1 = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile Test User 1

            Guid personID2 = new Guid("5E6CC2AB-479E-E911-A2C6-005056926FE4"); //Mr Mathews MCsenna 
            Guid professionalID2 = new Guid("258AABA7-443F-E911-A2C5-005056926FE4"); //Yvonne Cash 
            Guid providerID2 = new Guid("CBC34B69-71E0-E911-A2C7-005056926FE4"); //Carer
            Guid systemUserID2 = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4"); //Mobile Test User 2

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord(appointmentSubject);

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .TapRequiredEntryField(providerID1.ToString("N"));

            providerPage
                .WaitForProviderPageToLoad("Advanced")
                .ValidateIdFieldTitleVisible()
                .ValidateNameFieldTitleVisible()
                .ValidateProviderTypeTitleVisible()
              
                .ValidateIdField("1748")
                .ValidateNameField("Advanced")
                .ValidateProviderTypeField("Supplier")

                .TapbackButton();

            personAppointmentRecordPage
               .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6666")]
        [Description("UI Test for Person Appointments - 0010 - " +
            "Navigate to the Person Appointment area - Open a person Appointment record (System User record is set in the Required field) - Tap on the Required field entry - Validate that the user is redirected to the System User page")]
        public void PersonAppointments_TestMethod010()
        {
            string appointmentSubject = "Appointment 001";

            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid professionalID1 = new Guid("148AABA7-443F-E911-A2C5-005056926FE4"); //Brendan Morrison 
            Guid providerID1 = new Guid("6A34768A-3B9F-EA11-A2CD-005056926FE4"); //Advanced
            Guid systemUserID1 = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile Test User 1

            Guid personID2 = new Guid("5E6CC2AB-479E-E911-A2C6-005056926FE4"); //Mr Mathews MCsenna 
            Guid professionalID2 = new Guid("258AABA7-443F-E911-A2C5-005056926FE4"); //Yvonne Cash 
            Guid providerID2 = new Guid("CBC34B69-71E0-E911-A2C7-005056926FE4"); //Carer
            Guid systemUserID2 = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4"); //Mobile Test User 2

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord(appointmentSubject);

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .TapRequiredEntryField(systemUserID1.ToString("N"));

            SystemUserPage
                .WaitForSystemUserPageToLoad("Mobile Test User 1")
                .ValidateNameFieldTitleVisible()
                .ValidateUsernameTitleVisible()
                .ValidateBusinessUnitTitleVisible()

                .ValidateNameField("Mobile Test User 1")
                .ValidateProviderTypeField("mobile_test_user_1")
                .ValidateBusinessUnitField("Mobile Business Unit")

                .TapbackButton();

            personAppointmentRecordPage
               .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6667")]
        [Description("UI Test for Person Appointments - 0014 - " +
            "Navigate to the Person Appointment area - Open a person Appointment record - Tap on the Person field entry - Validate that the user is redirected to the Person page")]
        public void PersonAppointments_TestMethod014()
        {
            string appointmentSubject = "Appointment 001";

            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord(appointmentSubject);

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .TapOnPersonField();

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapbackButton();

            personAppointmentRecordPage
               .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6668")]
        [Description("UI Test for Person Appointments - 0015 - " +
            "Navigate to the Person Appointment area - Open a person Appointment record - Tap on the Outcome field entry - Validate that the user is redirected to the Activity Outcome page")]
        public void PersonAppointments_TestMethod015()
        {
            string appointmentSubject = "Appointment 001";

            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 


            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord(appointmentSubject);

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .TapOnOutcomeField();

            activityOutcome
                .WaitForActivityOutcomeToLoad("More information needed")

                .ValidateNameFieldTitleVisible()
                .ValidateCodeTitleVisible()
                .ValidateGovCodeTitleVisible()
                .ValidateNameField("More information needed")
                .ValidateCodeField("2")
                .ValidateGovCodeField("2")
                .TapbackButton();

            personAppointmentRecordPage
               .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001");
        }

        #endregion

        #region Edit Appointment

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6669")]
        [Description("UI Test for Person Appointments - 011 - Create a new person Appointment using the main APP web services" +
            "Navigate to the Person Appointment area - Open the Person Appointment record - Update the Outcome and Meeting Notes fields - Tap on the save button - Validate that the record is updated and sync to the web APP")]
        public void PersonAppointments_TestMethod011()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid activityOutcomeID = new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"); //More information needed
            Guid appointmentTypeID = new Guid("08759c8a-a4cc-e811-80dc-0050560502cc"); //Conference
            DateTime appointmentDate = new DateTime(2020, 6, 1);
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(9, 5, 0);


            //remove any PersonAppointment for the person
            foreach (Guid recordID in this.PlatformServicesHelper.appointment.GetAppointmentByPersonID(personID))
            {
                foreach (var requiredID in PlatformServicesHelper.appointmentRequiredAttendee.GetAppointmentRequiredAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentRequiredAttendee.DeleteAppointmentRequiredAttendee(requiredID);

                foreach (var optional in PlatformServicesHelper.appointmentOptionalAttendee.GetAppointmentOptionalAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentOptionalAttendee.DeleteAppointmentOptionalAttendee(optional);

                PlatformServicesHelper.appointment.DeleteAppointment(recordID);
            }

            Guid appointmentID = PlatformServicesHelper.appointment.CreateAppointment("Appointment 001", mobileTeam1, null, null, null, activityOutcomeID, null, null, personID, null, appointmentTypeID, personID, "Maria Tsatsouline", "person", "Notes ...", "location info ...", appointmentDate, startTime, appointmentDate, endTime);
            PlatformServicesHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(appointmentID, personID, "person", "Maria Tsatsouline");

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord("Appointment 001");

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .TapOutcomeLookupButtonField();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TapOnRecord("Recorded in Error");

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .InsertMeetingNotes("meeting notes!!!")
                .TapOnSaveAndCloseButton();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord("Appointment 001");

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .ValidateOutcomeFieldText("Recorded in Error")
                .ValidateMeetingNotesFieldText("meeting notes!!!");


            Guid activityOutcomeID2 = new Guid("AE000A29-9E45-E911-A2C5-005056926FE4"); //Recorded in Error

            var fields = PlatformServicesHelper.appointment.GetAppointmentByID(appointmentID, "activityoutcomeid", "notes");
            Assert.AreEqual(activityOutcomeID2, (Guid)fields["activityoutcomeid"]);
            Assert.AreEqual("<div>meeting notes!!!</div>", (string)fields["notes"]);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6670")]
        [Description("UI Test for Person Appointments - 012 - Create a new person Appointment using the main APP web services" +
            "Navigate to the Person Appointment area - Open the Person Appointment record - Update the Status field to Completed - Tap on the save button - Validate that the record is updated and deactivated")]
        public void PersonAppointments_TestMethod012()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid activityOutcomeID = new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"); //More information needed
            Guid appointmentTypeID = new Guid("08759c8a-a4cc-e811-80dc-0050560502cc"); //Conference
            DateTime appointmentDate = DateTime.Now.Date;
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(9, 5, 0);


            //remove any PersonAppointment for the person
            foreach (Guid recordID in this.PlatformServicesHelper.appointment.GetAppointmentByPersonID(personID))
            {
                foreach (var requiredID in PlatformServicesHelper.appointmentRequiredAttendee.GetAppointmentRequiredAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentRequiredAttendee.DeleteAppointmentRequiredAttendee(requiredID);

                foreach (var optional in PlatformServicesHelper.appointmentOptionalAttendee.GetAppointmentOptionalAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentOptionalAttendee.DeleteAppointmentOptionalAttendee(optional);

                PlatformServicesHelper.appointment.DeleteAppointment(recordID);
            }

            Guid appointmentID = PlatformServicesHelper.appointment.CreateAppointment("Appointment 001", mobileTeam1, null, null, null, activityOutcomeID, null, null, personID, null, appointmentTypeID, personID, "Maria Tsatsouline", "person", "Notes ...", "location info ...", appointmentDate, startTime, appointmentDate, endTime);
            PlatformServicesHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(appointmentID, personID, "person", "Maria Tsatsouline");

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord("Appointment 001");

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .TapStatusField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .TapOnSaveAndCloseButton();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);


            int statusid = 2; //completed
            var fields = PlatformServicesHelper.appointment.GetAppointmentByID(appointmentID, "statusid", "inactive");
            Assert.AreEqual(statusid, (int)fields["statusid"]);
            Assert.AreEqual(true, (bool)fields["inactive"]);
        }

        #endregion

        #region Sync Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6671")]
        [Description("UI Test for Person Appointments - 013 - Create a new person Appointment using the main APP web services" +
            "Navigate to the Person Appointment area - open the Person Appointment record - validate that all fields are correctly synced")]
        public void PersonAppointments_TestMethod013()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid activityOutcomeID = new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"); //More information needed
            Guid appointmentTypeID = new Guid("08759c8a-a4cc-e811-80dc-0050560502cc"); //Conference
            DateTime appointmentDate = new DateTime(2020, 6, 1);
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(9, 5, 0);


            //remove any PersonAppointment for the person
            foreach (Guid recordID in this.PlatformServicesHelper.appointment.GetAppointmentByPersonID(personID))
            {
                foreach (var requiredID in PlatformServicesHelper.appointmentRequiredAttendee.GetAppointmentRequiredAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentRequiredAttendee.DeleteAppointmentRequiredAttendee(requiredID);

                foreach (var optional in PlatformServicesHelper.appointmentOptionalAttendee.GetAppointmentOptionalAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentOptionalAttendee.DeleteAppointmentOptionalAttendee(optional);

                PlatformServicesHelper.appointment.DeleteAppointment(recordID);
            }

            Guid appointmentID = PlatformServicesHelper.appointment.CreateAppointment( "Appointment 001", mobileTeam1, null, null, null, activityOutcomeID, null, null, personID, null, appointmentTypeID, personID, "Maria Tsatsouline", "person", "Notes ...", "location info ...", appointmentDate, startTime, appointmentDate, endTime);
            PlatformServicesHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(appointmentID, personID, "person", "Maria Tsatsouline");

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TaAppointmentsIcon_RelatedItems();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .TapOnRecord("Appointment 001");

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001")
                .ValidateSubjectFieldText("Appointment 001")
                .ValidateStartDateFieldText("01/06/2020")
                .ValidateStartTimeRichFieldText("09:00")
                .ValidateEndDateFieldText("01/06/2020")
                .ValidateEndTimeFieldText("09:05")
                .ValidateAppointmentTypeFieldText("Conference")
                .ValidateCaseFieldText("")
                .ValidateStatusFieldText("Scheduled")
                .ValidateOutcomeFieldText("More information needed")

                .ValidatePersonFieldText("Maria Tsatsouline")
                .ValidateLocationFieldText("location info ...")
                .ValidateMeetingNotesFieldText("Notes ...");

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
