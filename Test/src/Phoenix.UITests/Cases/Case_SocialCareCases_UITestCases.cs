using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{

    [TestClass]
    public class Case_SocialCareCases_UITestCases : FunctionalTest
    {
        #region Properties

        private string _tenantName;
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUserName;
        private string _contactReceivedByUserName;
        private Guid _contactReceivedByUserId;
        private string _contactReasonName;
        private string _contactReasonName_AdoptionEnquiry;
        private Guid _contactReasonId;
        private Guid _contactReasonId_AdoptionEnquiry;
        private string _contactSourceName;
        private Guid _contactSourceId;
        private Guid _systemUserId;
        private Guid _personID;
        private string _personFirstName;
        private string _personLastName;
        private string _personFullName;
        private Guid _caseStatusId;
        private Guid _dataFormId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _otherPresentingPriority;
        private Guid _fosteringExperience;
        private Guid CINcode;
        private string CINCodeName;
        private Guid _contactTypeId;
        private Guid _contactStatusId;
        private string _casePriorityName;
        private Guid _casePriorityId;
        private Guid _contactid;
        private DateTime _caseDate = DateTime.Now.AddDays(-15);
        private DateTime _dateContactReceived = DateTime.Now.Date.AddDays(-10);
        private DateTime _reviewDate = DateTime.Now.Date.AddDays(-5);
        private DateTime _dateAndTimeOfContactWithTrainedStaff_Date = DateTime.Now.Date.AddDays(-4);
        private int personage = 21;

        #endregion

        [TestInitialize()]
        public void AdvancedSearch_SetupTest()
        {

            try
            {
                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Ethnicity" + _currentDateSuffix, new DateTime(2020, 1, 1));

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Contact Reason

                _contactReasonName = "Advice/Consultation";
                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded(_contactReasonName, _careDirectorQA_TeamId);

                #endregion

                #region Contact Source

                _contactSourceName = "Family";
                _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded(_contactSourceName, _careDirectorQA_TeamId);

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

                #endregion

                #region Create SystemUser Record

                _systemUserName = "Case_Social_Care_User_1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Case Social Care", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Presenting Priority

                _otherPresentingPriority = commonMethodsDB.CreateContactPresentingPriority(_careDirectorQA_TeamId, "Other");

                #endregion

                #region Fostering Experience 

                _fosteringExperience = commonMethodsDB.CreateFosteringExperience("Future start date", new DateTime(2022, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Child In Need Code 

                CINCodeName = "Absent parenting";
                CINcode = commonMethodsDB.CreateChildInNeedCode(CINCodeName, new DateTime(2022, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Contact Type

                _contactTypeId = commonMethodsDB.CreateContactType(_careDirectorQA_TeamId, "Adult Safeguarding", DateTime.Now.Date.AddYears(-1), true);

                #endregion

                #region Contact Status

                _contactStatusId = dbHelper.contactStatus.CreateContactStatus(_careDirectorQA_TeamId, "New Contact", "2", new DateTime(2020, 1, 1), 1, true);

                #endregion

                #region Contact Reason

                _contactReasonName_AdoptionEnquiry = "Adoption Enquiry";
                _contactReasonId_AdoptionEnquiry = commonMethodsDB.CreateContactReasonIfNeeded(_contactReasonName_AdoptionEnquiry, _careDirectorQA_TeamId);

                #endregion

                #region Case Priorities

                _casePriorityName = "Green";
                _casePriorityId = commonMethodsDB.CreateCasePriority(_casePriorityName, new DateTime(2022, 1, 1), 3, _careDirectorQA_TeamId);

                #endregion

                #region Person

                _personFirstName = "Care_Case";
                _personLastName = "Person_" + _currentDateSuffix;
                _personFullName = _personFirstName + " " + _personLastName;
                _personID = commonMethodsDB.CreatePersonRecord(_personFirstName, _personLastName, _ethnicityId, _careDirectorQA_TeamId);

                #endregion

                #region Contact Received By User (System User)

                _contactReceivedByUserName = "Contact_Received_User1";
                _contactReceivedByUserId = commonMethodsDB.CreateSystemUserRecord(_contactReceivedByUserName, "Contact Received", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-12831

        [TestProperty("JiraIssueID", "CDV6-12957")]
        [Description("Open active person record-Click cases Tab - Click on add new record- Select Social care case- Click on Save button" +
            "Validate the error messages.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            casesPage
                .WaitForPersonCasesPageToLoad()
                .ClickNewRecordButton();

            if (appURL.Contains("phoenixqa.careworks.ie"))
                selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Social Care Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickSaveButton()
                .ValidateNotificationMessage(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateContactRecievedByNotificationMessage(true)
                .ValidateContactRecievedByNotificationMessageText("Please fill out this field.")
                .ValidateContactReasonNotificationMessage(true)
                .ValidateContactReasonNotificationMessageText("Please fill out this field.")
                .ValidateCaseDateTime_DateNotificationMessage(true)
                .ValidateCaseDateAndTime_DateNotificationMessageText("Please fill out this field.")
                .ValidateCaseDateTime_TimeNotificationMessage(true)
                .ValidateCaseDateAndTime_TimeNotificationMessageText("Please fill out this field.")
                .ValidateDateTimeContactReceived_DateNotificationMessage(true)
                .ValidateDateTimeContactReceived_DateNotificationMessageText("Please fill out this field.")
                .ValidateDateTimeContactReceived_TimeNotificationMessage(true)
                .ValidateDateTimeContactReceived_TimeNotificationMessageText("Please fill out this field.")
                .ValidateIsThePersonAwareOfTheContactNotificationMessage(true)
                .ValidateIsThePersonAwareOfTheContactTimeNotificationMessageText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-12960")]
        [Description("Open active person record-Click cases Tab - Click on add new record- Select Social care case- Click on Save button" +
            "Enter all the optional and Mandatory field and save the record." + "Pre-Requisite Person should have a contact record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod02()
        {
            #region create contact Made By Person

            var contactMadeByPersonName = "John Contact Person";
            var contactMadeBy = commonMethodsDB.CreatePersonRecord("John", "Contact Person", _ethnicityId, _careDirectorQA_TeamId);

            #endregion

            #region Contact

            _contactid = dbHelper.contact.CreateContact(_careDirectorQA_TeamId, _personID, _contactTypeId, _contactReasonId_AdoptionEnquiry, _otherPresentingPriority, _contactStatusId, _systemUserId, _personID, "person", _personFirstName + " " + _personLastName, DateTime.Now.Date, "some value ...", 2, 2);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            casesPage
                .WaitForPersonCasesPageToLoad()
                .ClickNewRecordButton();

            if (appURL.Contains("phoenixqa.careworks.ie"))
                selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Social Care Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReceivedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_contactReceivedByUserName.ToString())
                .TapSearchButton()
                .SelectResultElement(_contactReceivedByUserId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReasonLookUpButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_contactReasonName.ToString())
                .TapSearchButton()
                .SelectResultElement(_contactReasonId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .InsertCaseDate(_caseDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertCaseTime("00:00")
                .ClickPresentingPriorityLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(_otherPresentingPriority.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickInitialContactLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_contactReasonName_AdoptionEnquiry.ToString())
                .TapSearchButton()
                .SelectResultElement(_contactid.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickCINcodeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(CINCodeName)
                .TapSearchButton()
                .SelectResultElement(CINcode.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .InsertDateContactReceived(_dateContactReceived.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertTimeContactReceived("00:00")
                .InsertAdditionalInformation("New Social care Case")
                .ClickContactMadeByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(contactMadeByPersonName.ToString())
                .TapSearchButton()
                .SelectResultElement(contactMadeBy.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .SelectCaseOrigin("Phone")
                .InsertContactMadeByFreeText("Social care case")
                .ClickContactSourceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_contactSourceName.ToString())
                .TapSearchButton()
                .SelectResultElement(_contactSourceId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .SelectIsThePersonAwareOfTheContact("Yes")
                .SelectDoesPersonAgreeSupportThisContact("Yes")
                .SelectNOKCarerAwareOfThisContact_Field("Yes")
                .ClickCasePriorityLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_casePriorityName)
                .TapSearchButton()
                .SelectResultElement(_casePriorityId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .InsertReviewDate(_reviewDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertReferrringAgencyCaseId("1234")
                .InsertDateAndTimeOfContactWithTrainedStaff_Date(_dateAndTimeOfContactWithTrainedStaff_Date.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertDateAndTimeOfContactWithTrainedStaff_Time("00:00")
                .ClickfosteringExperienceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Future start date")
                .TapSearchButton()
                .SelectResultElement(_fosteringExperience.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var cases = dbHelper.Case.GetCasesByPersonID(_personID);
            Assert.AreEqual(1, cases.Count);

            //Validate the created case record fields
            var CaseRecordFields = dbHelper.Case.GetCaseByID(cases[0], "personid", "contactreceivedbyid", "ownerid", "contactreasonid", "startdatetime", "presentingpriorityid", "initialcontactid", "childinneedcodeid",
                                                            "contactreceiveddatetime", "additionalinformation", "contactmadebyid", "caseoriginid", "contactmadebyname", "contactsourceid", "personawareofcontactid",
                                                            "nextofkinawareofcontactid", "personsupportcontactid", "casepriorityid", "reviewdate", "referringagencycaseid", "contactwithtrainedstaffdate", "fosteringexperienceid");


            Assert.AreEqual(_personID.ToString(), CaseRecordFields["personid"]);
            Assert.AreEqual(_contactReceivedByUserId.ToString(), CaseRecordFields["contactreceivedbyid"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), CaseRecordFields["ownerid"]);
            Assert.AreEqual(_contactReasonId.ToString(), CaseRecordFields["contactreasonid"]);
            Assert.AreEqual(_caseDate.Date.ToLocalTime(), ((DateTime)CaseRecordFields["startdatetime"]).ToLocalTime().Date);
            Assert.AreEqual(_otherPresentingPriority.ToString(), CaseRecordFields["presentingpriorityid"]);
            Assert.AreEqual(_contactid.ToString(), CaseRecordFields["initialcontactid"]);
            Assert.AreEqual(CINcode.ToString(), CaseRecordFields["childinneedcodeid"]);
            Assert.AreEqual(_dateContactReceived.Date.ToLocalTime(), ((DateTime)CaseRecordFields["contactreceiveddatetime"]).ToLocalTime().Date);
            Assert.AreEqual("New Social care Case", CaseRecordFields["additionalinformation"]);
            Assert.AreEqual(contactMadeBy.ToString(), CaseRecordFields["contactmadebyid"]);
            Assert.AreEqual(1, CaseRecordFields["caseoriginid"]);
            Assert.AreEqual("Social care case", CaseRecordFields["contactmadebyname"]);
            Assert.AreEqual(_contactSourceId.ToString(), CaseRecordFields["contactsourceid"]);
            Assert.AreEqual(1, CaseRecordFields["personawareofcontactid"]);
            Assert.AreEqual(1, CaseRecordFields["nextofkinawareofcontactid"]);
            Assert.AreEqual(1, CaseRecordFields["personsupportcontactid"]);
            Assert.AreEqual(_casePriorityId.ToString(), CaseRecordFields["casepriorityid"]);
            Assert.AreEqual(_reviewDate.Date, CaseRecordFields["reviewdate"]);
            Assert.AreEqual("1234", CaseRecordFields["referringagencycaseid"]);
            Assert.AreEqual(_dateAndTimeOfContactWithTrainedStaff_Date.Date, ((DateTime)CaseRecordFields["contactwithtrainedstaffdate"]).ToLocalTime().Date);
            Assert.AreEqual(_fosteringExperience.ToString(), CaseRecordFields["fosteringexperienceid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12988")]
        [Description("Open active person record-Click cases Tab - Click on add new record- Verify the person name is Auto populated ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod03()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            casesPage
                .WaitForPersonCasesPageToLoad()
                .ClickNewRecordButton();

            if (appURL.Contains("phoenixqa.careworks.ie"))
                selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Social Care Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ValidatePersonFieldValue(_personID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-12989")]
        [Description("Open active person record-Click cases Tab - Create a case for the person - verify the Case number is auto populated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod04()
        {
            #region Case

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _contactReceivedByUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, _dateContactReceived, _caseDate, personage);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad();


            //get the personid and title fields for the Case record and assert they are correctly updated
            var fields = dbHelper.Case.GetCaseByID(caseID, "personid", "title", "casenumber");
            Assert.AreEqual(_personID.ToString(), fields["personid"].ToString());
            string caseNumber = (string)fields["casenumber"];

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .SearchCaseRecord(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickDetailsLink()
                .ValidateCaseNumberAutoPopulated(true);


            var cases = dbHelper.Case.GetCasesByPersonID(_personID);
            Assert.AreEqual(1, cases.Count);

            //Validating the Auto populated Case Number
            var CaseRecordFields = dbHelper.Case.GetCaseByID(cases[0], "casenumber");

            Assert.AreEqual(caseNumber, CaseRecordFields["casenumber"]);


        }

        [TestProperty("JiraIssueID", "CDV6-13001")]
        [Description("Open active person record-Click cases Tab - Create a case for the person - verify the Case Date and Time field are editable")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod05()
        {
            #region Case

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _contactReceivedByUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, _dateContactReceived, _caseDate, personage);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad();


            //get the personid and title fields for the Case record and assert they are correctly updated
            var fields = dbHelper.Case.GetCaseByID(caseID, "personid", "title", "casenumber");
            Assert.AreEqual(_personID.ToString(), fields["personid"]);
            string caseNumber = (string)fields["casenumber"];

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .SearchCaseRecord(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickDetailsLink()
                .InsertCaseDate("16/09/2021")
                .InsertCaseTime("09:30:00")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            var cases = dbHelper.Case.GetCasesByPersonID(_personID);
            Assert.AreEqual(1, cases.Count);

            //Validate the created case record fields
            var CaseRecordFields = dbHelper.Case.GetCaseByID(cases[0], "startdatetime");

            //Validate the Case start date and time
            var expectedDate = new DateTime(2021, 9, 16, 9, 30, 0);
            Assert.AreEqual(expectedDate, ((DateTime)CaseRecordFields["startdatetime"]).ToLocalTime());

        }

        [TestProperty("JiraIssueID", "CDV6-13002")]
        [Description("Open active person record-Click cases Tab - Create a case for the person - verify the Date/Time Contact received are not editable")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod06()
        {
            #region Case

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _contactReceivedByUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, _dateContactReceived, _caseDate, personage);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad();


            //get the personid and title fields for the Case record and assert they are correctly updated
            var fields = dbHelper.Case.GetCaseByID(caseID, "personid", "title", "casenumber");
            Assert.AreEqual(_personID.ToString(), fields["personid"]);
            string caseNumber = (string)fields["casenumber"];

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .SearchCaseRecord(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickDetailsLink()
                .ValidateDateContactReceivedFieldDisabled(true)
                .ValidateTimeContactReceivedFieldDisabled(true);

        }

        [TestProperty("JiraIssueID", "CDV6-13004")]
        [Description("Open active person record-Click cases Tab - Create a case for the person - *Contact Awareness* -> Click on “Is the Person aware of the contact ?” -> Select *Yes* from the drop down list -> Verify if system displays the particular mandatory field “Does Person agree/support this contact?” text field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod07()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickNewRecordButton();

            if (appURL.Contains("phoenixqa.careworks.ie"))
                selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Social Care Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReceivedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_contactReceivedByUserName.ToString())
                .TapSearchButton()
                .SelectResultElement(_contactReceivedByUserId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_contactReasonName)
                .TapSearchButton()
                .SelectResultElement(_contactReasonId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .InsertCaseDate(_caseDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertCaseTime("00:00")
                .SelectIsThePersonAwareOfTheContact("Yes")
                .ValidateDoesPersonAgreeSupportThisContact_MandatoryField(true);

        }

        [TestProperty("JiraIssueID", "CDV6-13011")]
        [Description("Open active person record-Click cases Tab - Create a case for the person - *Contact Awareness* -> Click on “Is the Person aware of the contact ?” -> Select *No* from the drop down list -> Verify if system displays the particular mandatory field “Is N.O.K/Carer aware of this  contact?” text field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod08()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickNewRecordButton();

            if (appURL.Contains("phoenixqa.careworks.ie"))
                selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Social Care Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReceivedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_contactReceivedByUserName.ToString())
                .TapSearchButton()
                .SelectResultElement(_contactReceivedByUserId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_contactReasonName)
                .TapSearchButton()
                .SelectResultElement(_contactReasonId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .InsertCaseDate(_caseDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertCaseTime("00:00")
                .SelectIsThePersonAwareOfTheContact("No")
                .ValidateNokCarerAwareOfThisContact_MandatoryField(true);

        }

        [TestProperty("JiraIssueID", "CDV6-13018")]
        [Description("Open active person record-Click cases Tab - Create a case for the person - *Contact Awareness* -> Click on “Is the Person aware of the contact ?” -> Select *Not Known* from the drop down list -> Verify if system displays any mandatory field if the user selected *Not Known* in the “Is the Person aware of the contact?” in the Contact Awareness Section.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod09()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickNewRecordButton();

            if (appURL.Contains("phoenixqa.careworks.ie"))
                selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Social Care Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReceivedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_contactReceivedByUserName.ToString())
                .TapSearchButton()
                .SelectResultElement(_contactReceivedByUserId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_contactReasonName)
                .TapSearchButton()
                .SelectResultElement(_contactReasonId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .InsertCaseDate(_caseDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertCaseTime("00:00")
                .SelectIsThePersonAwareOfTheContact("Not Known")
                .ValidateNokCarerAwareOfThisContact_MandatoryField(false);

        }

        [TestProperty("JiraIssueID", "CDV6-13020")]
        [Description("Open active person record-Click cases Tab - Create a case for the person -> Verify Case Status ,Responsible User and Responsible Team are auto populated in the Assignment Information section.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod10()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickNewRecordButton();

            if (appURL.Contains("phoenixqa.careworks.ie"))
                selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Social Care Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReceivedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_contactReceivedByUserName.ToString())
                .TapSearchButton()
                .SelectResultElement(_contactReceivedByUserId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_contactReasonName)
                .TapSearchButton()
                .SelectResultElement(_contactReasonId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .InsertCaseDate(_caseDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertCaseTime("00:00")
                .ValidateCaseStatusLinkAutopopulated(true)
                .ValidateResponsibleUserAutopopulated(true)
                .ValidateResponsibleTeamAutopopulated(true);

        }

        [TestProperty("JiraIssueID", "CDV6-13021")]
        [Description("Open active person record-Click cases Tab - Create a case for the person ->*Police Notified* -> Verify Police Notified Flag is set to Yes then it will display additional text fields like Police Notified Date and Police Notes or not in the Police Notified Section.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod11()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .ClickNewRecordButton();

            if (appURL.Contains("phoenixqa.careworks.ie"))
                selectCaseTypePopUp.WaitForSelectCaseTypePopUpToLoad().SelectViewByText("Social Care Case").TapNextButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReceivedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_contactReceivedByUserName.ToString())
                .TapSearchButton()
                .SelectResultElement(_contactReceivedByUserId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickContactReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_contactReasonName)
                .TapSearchButton()
                .SelectResultElement(_contactReasonId.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .InsertCaseDate(_caseDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertCaseTime("00:00")
                .ClickPoliceNotified_YesOption()
                .ValidatePoliceNotifiedDateField_Displayed(true)
                .ValidatePoliceNotesField_Displayed(true);

        }

        //NOTE: This test can pnly run against QA as the "Can Correct Errors?" feature is not enabled in AWS
        [TestProperty("JiraIssueID", "CDV6-13022")]
        [Description("Open active person record-Click cases Tab - Create a case for the person - Open the case and click Correct Errors under the additional items - Update the  Contact Reason , Contact updated ,Contact source and Presenting Priority" +
            "Save the case")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Case_SocialCareCases_UITestMethod12()
        {
            #region Contact Received By Update User (System User)

            var contactReceivedByUpdateUserName = "Contact_Received_Update_User1";
            var contactReceivedByUpdate = commonMethodsDB.CreateSystemUserRecord(contactReceivedByUpdateUserName, "Contact Received Update", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            #region Contact Reason

            var contactReasonUpdateName = "Cardiac Arrest / related issues";
            var contactReasonUpdateId = commonMethodsDB.CreateContactReasonIfNeeded(contactReasonUpdateName, _careDirectorQA_TeamId);

            #endregion

            #region Contact Source

            var contactSourceUpdateName = "Friend";
            var contactSourceUpdateId = commonMethodsDB.CreateContactSourceIfNeeded(contactSourceUpdateName, _careDirectorQA_TeamId);

            #endregion

            #region Presenting Priority

            var PresentingPriorityUpdate = commonMethodsDB.CreateContactPresentingPriority(_careDirectorQA_TeamId, "Update_Priority");

            #endregion

            #region Audit Reason (Error Management Reason)

            var auditReasonName = "Changes required";
            var auditReasonId = commonMethodsDB.CreateErrorManagementReason(auditReasonName, DateTime.Now.Date.AddYears(-1), 4, _careDirectorQA_TeamId);

            #endregion

            #region Case

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _contactReceivedByUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, _dateContactReceived, _caseDate, personage);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad();

            //get the personid and title fields for the Case record and assert they are correctly updated
            var fields = dbHelper.Case.GetCaseByID(caseID, "personid", "title", "casenumber");
            Assert.AreEqual(_personID.ToString(), fields["personid"].ToString());
            string caseNumber = (string)fields["casenumber"];

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .SearchCaseRecord(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString());

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickDetailsLink()
                .ClickAdditionalIItemsButton()
                .ClickCorrectErrors();

            correctErrorsPopUp.WaitForCorrectErrorsPopupToLoad().ClickUpdateContactReason().ClickUpdateContactReasonLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(contactReasonUpdateName.ToString())
                .TapSearchButton()
                .SelectResultElement(contactReasonUpdateId.ToString());

            correctErrorsPopUp.ClickUpdateContactReceivedBy().ClickContactReceivedByLookUButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(contactReceivedByUpdateUserName.ToString())
                .TapSearchButton()
                .SelectResultElement(contactReceivedByUpdate.ToString());

            correctErrorsPopUp.ClickUpdateContactSource().ClickContactSourceLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(contactSourceUpdateName.ToString())
                .TapSearchButton()
                .SelectResultElement(contactSourceUpdateId.ToString());

            correctErrorsPopUp.ClickUpdatePresentingPriority().ClickPresentingPriorityLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Update_Priority")
                .TapSearchButton()
                .SelectResultElement(PresentingPriorityUpdate.ToString());

            correctErrorsPopUp.ClickReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(auditReasonName.ToString()).TapSearchButton().SelectResultElement(auditReasonId.ToString());

            correctErrorsPopUp.ClickUpdateButton();

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad();

            var cases = dbHelper.Case.GetCasesByPersonID(_personID);
            Assert.AreEqual(1, cases.Count);

            var CaseRecordFields = dbHelper.Case.GetCaseByID(cases[0], "personid", "contactreceivedbyid", "ownerid", "contactreasonid", "startdatetime", "presentingpriorityid", "initialcontactid", "childinneedcodeid",
                                                            "contactreceiveddatetime", "additionalinformation", "contactmadebyid", "caseoriginid", "contactmadebyname", "contactsourceid", "personawareofcontactid",
                                                            "nextofkinawareofcontactid", "personsupportcontactid", "casepriorityid", "reviewdate", "referringagencycaseid", "contactwithtrainedstaffdate", "fosteringexperienceid");


            Assert.AreEqual(_personID.ToString(), CaseRecordFields["personid"].ToString());
            Assert.AreEqual(contactReceivedByUpdate.ToString(), CaseRecordFields["contactreceivedbyid"].ToString());
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), CaseRecordFields["ownerid"].ToString());
            Assert.AreEqual(contactReasonUpdateId.ToString(), CaseRecordFields["contactreasonid"].ToString());
            Assert.AreEqual(PresentingPriorityUpdate.ToString(), CaseRecordFields["presentingpriorityid"].ToString());
            Assert.AreEqual(contactSourceUpdateId.ToString(), CaseRecordFields["contactsourceid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-13035")]
        [Description("Open active person record-Click cases Tab - Create a case for the person - Click Additional Items Button and select Delete- Check the case is deleted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod13()
        {
            #region Case

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _contactReceivedByUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, _dateContactReceived, _caseDate, personage);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad();


            //get the personid and title fields for the Case record and assert they are correctly updated
            var fields = dbHelper.Case.GetCaseByID(caseID, "personid", "title", "casenumber");
            Assert.AreEqual(_personID.ToString(), fields["personid"].ToString());
            string caseNumber = (string)fields["casenumber"];

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .SearchCaseRecord(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString());

            //Deleting the child records for the created case
            foreach (var caseinvolvementid in dbHelper.CaseInvolvement.GetByCasePersonId(_personID))
                dbHelper.CaseInvolvement.DeleteCaseInvolvement(caseinvolvementid);

            //Deleting the child records for the created case
            foreach (var caseinvolvementid in dbHelper.CaseStatusHistory.GetByPersonID(_personID))
                dbHelper.CaseStatusHistory.DeleteCaseStatusHistory(caseinvolvementid);

            caseRecordPage
                .WaitForPersonCaseRecordPageToLoad()
                .ClickDetailsLink()
                .ClickAdditionalIItemsButton()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            System.Threading.Thread.Sleep(5000);

            var cases = dbHelper.Case.GetCaseByID(caseID);
            Assert.AreEqual(0, cases.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-13045")]
        [Description("Open active person record-Click cases Tab - Create a case for the person - Validate the case status in Timeline")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod14()
        {
            #region Case

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _contactReceivedByUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, _dateContactReceived, _caseDate, personage);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ValidateRecordPresent(caseID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-13046")]
        [Description("Open active person record-Click cases Tab - Create a case for the person - Click export to excel")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_SocialCareCases_UITestMethod15()
        {
            #region Case

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _contactReceivedByUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, _dateContactReceived, _caseDate, personage);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad();

            //get the personid and title fields for the Case record and assert they are correctly updated
            var fields = dbHelper.Case.GetCaseByID(caseID, "personid", "title", "casenumber");
            Assert.AreEqual(_personID.ToString(), fields["personid"].ToString());
            string caseNumber = (string)fields["casenumber"];

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .SearchCaseRecord(caseNumber, caseID.ToString())
                .SelectCaseRecord(caseID.ToString())
                .ClickExportToExcel();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Selected Records")
                .SelectExportFormat("Csv (comma separated with quotes)")
                .ClickExportButton();

            System.Threading.Thread.Sleep(5000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "Cases.csv");
            Assert.IsTrue(fileExists);

        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        #endregion
    }
}
