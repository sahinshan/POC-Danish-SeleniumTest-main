using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]

    public class Case_Seclusions_UITestCases : FunctionalTest


    {
        private Guid _authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _languageId;
        private Guid _AutomationSeclusionsUser1_SystemUserId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _personID;
        private int _personNumber;
        private Guid _caseId;
        private Guid _dataFormListid = new Guid("fde6a5fc-2db7-e811-80dc-0050560502cc");//Inpatient Case
        private Guid _inpatientAdmissionSourceId;
        private Guid _provider_HospitalId;
        private Guid _inpationAdmissionMethodId;
        private Guid _wardSpecialityId = new Guid("08295329-10bc-e811-80dc-0050560502cc");//Adult Acute
        private Guid _inpationWardId;
        private Guid _inpationBayId;
        private Guid _inpationBedId;
        private Guid _inpatientBedTypeId = new Guid("4280BA43-F122-E911-80DC-0050560502CC");//Clinitron
        private Guid _caseStatusID = new Guid("18206816-583B-E911-80DC-0050560502CC");
        private Guid _seclusionReasonid;
        private Guid _documenttypeid;
        private Guid _documentsubtypeid;



        [TestInitialize()]
        public void TestInitializationMethod()
        {
            try
            {
                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

                #endregion

                #region System User

                var automationSeclusionTestUser1Exists = dbHelper.systemUser.GetSystemUserByUserName("Automation_Seclusions_Test_User_1").Any();
                if (!automationSeclusionTestUser1Exists)
                {
                    _AutomationSeclusionsUser1_SystemUserId = dbHelper.systemUser.CreateSystemUser("Automation_Seclusions_Test_User_1", "Automation", " Seclusions Test User 1", "Automation Seclusions Test User 1", "Passw0rd_!", "Automation_Seclusions_Test_User_1@somemail.com", "Automation_Seclusions_Test_User_1@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_AutomationSeclusionsUser1_SystemUserId, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_AutomationSeclusionsUser1_SystemUserId, systemUserSecureFieldsSecurityProfileId);
                }
                if (_AutomationSeclusionsUser1_SystemUserId == Guid.Empty)
                {
                    _AutomationSeclusionsUser1_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName("Automation_Seclusions_Test_User_1").FirstOrDefault();
                }

                #endregion

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Seclusions_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Seclusions_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Seclusions_Ethnicity")[0];

                #endregion

                #region Contact Reason

                var contactReasonExists = dbHelper.contactReason.GetByName("Seclusions_ContactReason").Any();
                if (!contactReasonExists)
                    dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "Seclusions_ContactReason", new DateTime(2020, 1, 1), 140000001, false);
                _contactReasonId = dbHelper.contactReason.GetByName("Seclusions_ContactReason")[0];

                #endregion 

                #region Contact Source

                var contactSourceExists = dbHelper.contactSource.GetByName("Seclusions_ContactSource").Any();
                if (!contactSourceExists)
                    dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "Seclusions_ContactSource", new DateTime(2020, 1, 1));
                _contactSourceId = dbHelper.contactSource.GetByName("Seclusions_ContactSource")[0];

                #endregion 

                #region Contact Inpatient Admission Source

                var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("Seclusions_InpatientAdmissionSource").Any();
                if (!inpatientAdmissionSourceExists)
                    dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_careDirectorQA_TeamId, "Seclusions_InpatientAdmissionSource", new DateTime(2020, 1, 1));
                _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("Seclusions_InpatientAdmissionSource")[0];

                #endregion

                #region Provider_Hospital

                var _provider_Name = "Aut_Prov_LeaveAWOL_" + commonMethodsHelper.GetCurrentDateTimeString();
                _provider_HospitalId = dbHelper.provider.CreateProvider(_provider_Name, _careDirectorQA_TeamId);

                #endregion 

                #region Ward

                var _inpationWardName = "Ward_" + commonMethodsHelper.GetCurrentDateTimeString();
                _inpationWardId = dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provider_HospitalId, _AutomationSeclusionsUser1_SystemUserId, _wardSpecialityId, _inpationWardName, new DateTime(2020, 1, 1));

                #endregion 

                #region Bay/Room

                var _inpationBayName = "Bay_" + commonMethodsHelper.GetCurrentDateTimeString();
                _inpationBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpationWardId, _inpationBayName, 1, "4", "4", "4", 2);

                #endregion

                #region Bed

                var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpationBayId).Any();
                if (!inpatientBedExists)
                    dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12665", "4", "4", _inpationBayId, 1, _inpatientBedTypeId, "4");
                _inpationBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpationBayId)[0];

                #endregion

                #region InpatientAdmissionMethod

                var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionSeclusions").Any();
                if (!inpatientAdmissionMethodExists)
                    dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod("Automation_AdmissionSeclusions", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
                _inpationAdmissionMethodId = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionSeclusions")[0];

                #endregion

                #region Person

                var firstName = commonMethodsHelper.GetCurrentDateTimeString();
                _personID = dbHelper.person.CreatePersonRecord("", firstName, "", "AutomationSeclusionsLastName", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

                #endregion

                #region To Create Inpatient Case record 1

                _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, _personID, DateTime.Now.Date, _AutomationSeclusionsUser1_SystemUserId, "hdsa", _AutomationSeclusionsUser1_SystemUserId, _caseStatusID, _contactReasonId, DateTime.Now.Date, _dataFormListid, _contactSourceId, _inpationWardId, _inpationBayId, _inpationBedId, _inpatientAdmissionSourceId, _inpationAdmissionMethodId, _AutomationSeclusionsUser1_SystemUserId, DateTime.Now.Date, _provider_HospitalId, _inpationWardId, 1, DateTime.Now.Date, false, false, false, false, false, false, false, false, false, false);

                #endregion

                #region Seclusions Reason

                var seclusionsReasonExists = dbHelper.inpatientSeclusionReason.GetByName("Automation_Seclusions").Any();
                if (!seclusionsReasonExists)
                    dbHelper.inpatientSeclusionReason.CreateInpatientSeclusionsReason("Automation_Seclusions", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
                _seclusionReasonid = dbHelper.inpatientSeclusionReason.GetByName("Automation_Seclusions")[0];

                #endregion 

                #region Attach Document Type

                var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Inpatient Case Seclusion Attachment type").Any();
                if (!attachDocumentTypeExists)
                    dbHelper.attachDocumentType.CreateAttachDocumentType(_careDirectorQA_TeamId, "Inpatient Case Seclusion Attachment type", new DateTime(2020, 1, 1));
                _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Inpatient Case Seclusion Attachment type")[0];

                #endregion

                #region Attach Document Sub Type

                var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Inpatient Case Seclusion Attachment sub type").Any();
                if (!attachDocumentSubTypeExists)
                    dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careDirectorQA_TeamId, "Inpatient Case Seclusion Attachment sub type", new DateTime(2020, 1, 1), _documenttypeid);
                _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Inpatient Case Seclusion Attachment sub type")[0];

                #endregion

                #region System User AllActivitiesUser1

                commonMethodsDB.CreateSystemUserRecord("SeclusionsUser1", "Seclusions", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-14907

        [TestProperty("JiraIssueID", "CDV6-15384")]
        [Description("Navigate to WorkPlace -> People -> Open the person record mentioned in pre requisite -> Click on Cases tab -> Open In Patient case " +
            "Set the Seculion flag is set to No in Hospital ward" + "Click on  Menu -> Health -> Seclusions" + "Create a record with entering all the fields" +
            "Validate the error message:Seclusion can only be created against a ward which has Seclusion flag = Yes ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Seclusions_UITestMethod01()
        {

            var seclusionDate = DateTime.Now.Date;
            var initialPlannedSeclusionDate = DateTime.Now.Date;

            //Setting the Seclusion ward flag to No
            dbHelper.inpatientWard.UpdateInpatientWardSeclusions(_inpationWardId, false);

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login("SeclusionsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                 .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToSeclusionsPage();

            seclusionsPage
                .WaitForSeclusionsPageToLoad()
                .ClickNewRecordButton();

            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .InsertSeclusionsDateTime(seclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickSeclusionsReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Seclusions")
                .TapSearchButton()
                .SelectResultElement(_seclusionReasonid.ToString());


            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .ClickApprovedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation")
                .TapSearchButton()
                .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoad()
               .InsertRationaleForSeclusions("Automation Seclusion = No")
               .SelectNOKcarerNotified("No")
               .ClickCommencedByLookUpButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Automation ")
              .TapSearchButton()
              .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoad()
               .InsertIntialPlannedReviewDateTime(initialPlannedSeclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .TapSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("Seclusion can only be created against a ward which has Seclusion flag = Yes");

            #endregion Step 1
        }

        [TestProperty("JiraIssueID", "CDV6-15386")]
        [Description("Navigate to WorkPlace -> People -> Open the person record mentioned in pre requisite -> Click on Cases tab -> Open In Patient case " +
            "Set the Seculion flag is set to Yes in Hospital ward" + "Click on  Menu -> Health -> Seclusions" + "Create a record with entering all the fields" +
            "Enter the Current dates in Seclusion Date field and  Initial Planned Seclusion Date field" + "Validate the created record fields ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Seclusions_UITestMethod02()
        {

            var seclusionDate = DateTime.Now.Date;
            var initialPlannedSeclusionDate = DateTime.Now.Date;

            //Setting the Seclusion ward flag to Yes
            dbHelper.inpatientWard.UpdateInpatientWardSeclusions(_inpationWardId, true);

            #region Step 2 & Step 4

            loginPage
                .GoToLoginPage()
                .Login("SeclusionsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                  .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToSeclusionsPage();


            seclusionsPage
                .WaitForSeclusionsPageToLoad()
                .ClickNewRecordButton();

            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .InsertSeclusionsDateTime(seclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickSeclusionsReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Seclusions")
                .TapSearchButton()
                .SelectResultElement(_seclusionReasonid.ToString());


            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .ClickApprovedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation")
                .TapSearchButton()
                .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoad()
               .InsertRationaleForSeclusions("Automation Seclusion = Yes")
               .SelectNOKcarerNotified("No")
               .ClickCommencedByLookUpButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Automation ")
              .TapSearchButton()
              .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoad()
               .InsertIntialPlannedReviewDateTime(initialPlannedSeclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .TapSaveButton();

            System.Threading.Thread.Sleep(5000);

            var seclusionRecord = dbHelper.inpatientSeclusion.GetByPersonId(_personID);
            Assert.AreEqual(1, seclusionRecord.Count);

            var seclusionRecordFields = dbHelper.inpatientSeclusion.GetByID(seclusionRecord[0], "ownerid", "personid", "caseid", "inpatientseclusionreasonid",
                                                "RationaleForSeclusion", "CommencedById", "CommencedApprovedById", "seclusiondatetime",
                                                "SeclusionReviewPlannedDateTime");


            Assert.AreEqual(_personID.ToString(), seclusionRecordFields["personid"].ToString());
            Assert.AreEqual(_caseId.ToString(), seclusionRecordFields["caseid"].ToString());
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), seclusionRecordFields["ownerid"].ToString());
            Assert.AreEqual(_seclusionReasonid.ToString(), seclusionRecordFields["inpatientseclusionreasonid"].ToString());
            Assert.AreEqual("Automation Seclusion = Yes", seclusionRecordFields["rationaleforseclusion"]);
            Assert.AreEqual(seclusionDate.ToLocalTime(), ((DateTime)seclusionRecordFields["seclusiondatetime"]).ToLocalTime().Date);
            Assert.AreEqual(initialPlannedSeclusionDate.ToLocalTime(), ((DateTime)seclusionRecordFields["seclusionreviewplanneddatetime"]).ToLocalTime().Date);
            Assert.AreEqual(_AutomationSeclusionsUser1_SystemUserId.ToString(), seclusionRecordFields["commencedbyid"].ToString());
            Assert.AreEqual(_AutomationSeclusionsUser1_SystemUserId.ToString(), seclusionRecordFields["commencedapprovedbyid"].ToString());


            #endregion Step 2 & Step 4


        }

        [TestProperty("JiraIssueID", "CDV6-15387")]
        [Description("Navigate to WorkPlace -> People -> Open the person record mentioned in pre requisite -> Click on Cases tab -> Open In Patient case " +
            "Set the Seculion flag is set to Yes in Hospital ward" + "Click on  Menu -> Health -> Seclusions" + "Create a record with entering all the fields" +
            "Enter the Seclusion Date field as earlier and  Initial Planned Seclusion Date field as current date" + "Validate the created record fields ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Seclusions_UITestMethod03()
        {

            var seclusionDate = DateTime.Now.AddDays(-5).Date;
            var initialPlannedSeclusionDate = DateTime.Now.Date;

            //Setting the Seclusion ward flag to Yes
            dbHelper.inpatientWard.UpdateInpatientWardSeclusions(_inpationWardId, true);

            #region Step 3

            loginPage
                .GoToLoginPage()
                .Login("SeclusionsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                  .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToSeclusionsPage();


            seclusionsPage
                .WaitForSeclusionsPageToLoad()
                .ClickNewRecordButton();

            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .InsertSeclusionsDateTime(seclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickSeclusionsReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Seclusions")
                .TapSearchButton()
                .SelectResultElement(_seclusionReasonid.ToString());


            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .ClickApprovedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation")
                .TapSearchButton()
                .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoad()
               .InsertRationaleForSeclusions("Automation Seclusion = Yes")
               .SelectNOKcarerNotified("No")
               .ClickCommencedByLookUpButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Automation ")
              .TapSearchButton()
              .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoad()
               .InsertIntialPlannedReviewDateTime(initialPlannedSeclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .TapSaveButton();

            System.Threading.Thread.Sleep(5000);

            var seclusionRecord = dbHelper.inpatientSeclusion.GetByPersonId(_personID);
            Assert.AreEqual(1, seclusionRecord.Count);

            var seclusionRecordFields = dbHelper.inpatientSeclusion.GetByID(seclusionRecord[0], "ownerid", "personid", "caseid", "inpatientseclusionreasonid",
                                                "RationaleForSeclusion", "CommencedById", "CommencedApprovedById", "seclusiondatetime",
                                                "SeclusionReviewPlannedDateTime");


            Assert.AreEqual(_personID.ToString(), seclusionRecordFields["personid"].ToString());
            Assert.AreEqual(_caseId.ToString(), seclusionRecordFields["caseid"].ToString());
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), seclusionRecordFields["ownerid"].ToString());
            Assert.AreEqual(_seclusionReasonid.ToString(), seclusionRecordFields["inpatientseclusionreasonid"].ToString());
            Assert.AreEqual("Automation Seclusion = Yes", seclusionRecordFields["rationaleforseclusion"]);
            Assert.AreEqual(seclusionDate.ToLocalTime(), ((DateTime)seclusionRecordFields["seclusiondatetime"]).ToLocalTime().Date);
            Assert.AreEqual(initialPlannedSeclusionDate.ToLocalTime(), ((DateTime)seclusionRecordFields["seclusionreviewplanneddatetime"]).ToLocalTime().Date);
            Assert.AreEqual(_AutomationSeclusionsUser1_SystemUserId.ToString(), seclusionRecordFields["commencedbyid"].ToString());
            Assert.AreEqual(_AutomationSeclusionsUser1_SystemUserId.ToString(), seclusionRecordFields["commencedapprovedbyid"].ToString());


            #endregion Step 3


        }

        [TestProperty("JiraIssueID", "CDV6-15388")]
        [Description("Navigate to WorkPlace -> People -> Open the person record mentioned in pre requisite -> Click on Cases tab -> Open In Patient case " +
            "Set the Seculion flag is set to Yes in Hospital ward" + "Click on  Menu -> Health -> Seclusions" + "Create a record with entering all the fields" +
            "Enter the Seclusion Date field as future date and  Initial Planned Seclusion Date field as current date" + "Validate the alert message ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Seclusions_UITestMethod04()
        {

            var seclusionDate = DateTime.Now.AddDays(5).Date;
            var initialPlannedSeclusionDate = DateTime.Now.Date;

            //Setting the Seclusion ward flag to Yes
            dbHelper.inpatientWard.UpdateInpatientWardSeclusions(_inpationWardId, true);

            #region Step 5 

            loginPage
                .GoToLoginPage()
                .Login("SeclusionsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                  .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToSeclusionsPage();


            seclusionsPage
                .WaitForSeclusionsPageToLoad()
                .ClickNewRecordButton();

            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .InsertSeclusionsDateTime(seclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickSeclusionsReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Seclusions")
                .TapSearchButton()
                .SelectResultElement(_seclusionReasonid.ToString());


            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .ClickApprovedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation")
                .TapSearchButton()
                .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoad()
               .InsertRationaleForSeclusions("Automation Seclusion = Yes")
               .SelectNOKcarerNotified("No")
               .ClickCommencedByLookUpButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Automation ")
              .TapSearchButton()
              .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoad()
               .InsertIntialPlannedReviewDateTime(initialPlannedSeclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .TapSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Seclusion Date and Time cannot be in the future");

            #endregion Step 5 

        }

        [TestProperty("JiraIssueID", "CDV6-15389")]
        [Description("Navigate to WorkPlace -> People -> Open the person record mentioned in pre requisite -> Click on Cases tab -> Open In Patient case " +
            "Set the Seculion flag is set to Yes in Hospital ward" + "Create a seclusion record " + "Click on  Menu -> Health -> Seclusions" + "Create 2nd record with entering all the fields" +
            "Validate the error message ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Seclusions_UITestMethod05()
        {

            var seclusionDate = DateTime.Now.AddDays(-5).Date;
            var initialPlannedSeclusionDate = DateTime.Now.Date;

            //Setting the Seclusion ward flag to Yes
            dbHelper.inpatientWard.UpdateInpatientWardSeclusions(_inpationWardId, true);

            var seclusionRecord = dbHelper.inpatientSeclusion.CreateSeclusionRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, _seclusionReasonid,
                                        "Automation Seclusion = Yes", _AutomationSeclusionsUser1_SystemUserId, _AutomationSeclusionsUser1_SystemUserId, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date);

            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login("SeclusionsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                  .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToSeclusionsPage();


            seclusionsPage
                .WaitForSeclusionsPageToLoad()
                .ClickNewRecordButton();

            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .InsertSeclusionsDateTime(seclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickSeclusionsReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Seclusions")
                .TapSearchButton()
                .SelectResultElement(_seclusionReasonid.ToString());


            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .ClickApprovedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation")
                .TapSearchButton()
                .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoad()
               .InsertRationaleForSeclusions("Automation Seclusion = Yes")
               .SelectNOKcarerNotified("No")
               .ClickCommencedByLookUpButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Automation ")
              .TapSearchButton()
              .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoad()
               .InsertIntialPlannedReviewDateTime(initialPlannedSeclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There can only be one active seclusion per case");

            #endregion Step 6

        }

        [TestProperty("JiraIssueID", "CDV6-15390")]
        [Description("Navigate to WorkPlace -> People -> Open the person record mentioned in pre requisite -> Click on Cases tab -> Open In Patient case " +
            "Set the Seculion flag is set to Yes in Hospital ward" + "Open a seclusion record " + "Click on  Menu -> RelatedItems -> SeclusionReviews" + "Enter the Mandatory fields and set Will Person continue in Seclusion = No." +
             "Navigate back to Seclusion Record page  and validate the End Seclusion region is appeared" +
            "Enter the mandatory details and save it " + "Validate the seclusion record is ended")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Seclusions_UITestMethod06()
        {


            var SeclusionDate = DateTime.Now.Date;

            //Setting the Seclusion ward flag to Yes
            dbHelper.inpatientWard.UpdateInpatientWardSeclusions(_inpationWardId, true);

            var seclusionRecord = dbHelper.inpatientSeclusion.CreateSeclusionRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, _seclusionReasonid,
                                        "Automation Seclusion = Yes", _AutomationSeclusionsUser1_SystemUserId, _AutomationSeclusionsUser1_SystemUserId, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date);

            #region Step 7 & Step 8

            loginPage
                .GoToLoginPage()
                .Login("SeclusionsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                  .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToSeclusionsPage();


            seclusionsPage
                .WaitForSeclusionsPageToLoad()
                .OpenSeclusionRecord(seclusionRecord.ToString());

            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .NavigateToSeclusionReviews();

            var seclusionReviewRecord = dbHelper.inpatientSeclusionReview.GetByPersonId(_personID);
            Assert.AreEqual(1, seclusionReviewRecord.Count);

            seclusionReviewsPage
                .WaitForSeclusionReviewsPageToLoad()
                .OpenSeclusionReviewRecord(seclusionReviewRecord[0].ToString());

            seclusionReviewsRecordPage
                .WaitForSeclusionReviewsRecordPageToLoad()
                .InsertActualReviewDateTime(SeclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .SelectWillPersonContinueInSeclusion("No")
                .InsertReviewCommentsAndActionPlan("Review")
                .ClickProfessionalsAtReviewLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Automation")
                 .TapSearchButton()
                 .ClickAddSelectedButton(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionReviewsRecordPage
               .WaitForSeclusionReviewsRecordPageToLoad()
               .TapSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Please complete the “End of Seclusion” section now present on the seclusion record, this will indicate the person is now out of Seclusion.")
                .TapOKButton();

            seclusionReviewsRecordPage
                 .WaitForSeclusionReviewsRecordPageToLoad()
                 .TapBackButton();

            seclusionReviewsPage
                .WaitForSeclusionReviewsPageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                  .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToSeclusionsPage();


            seclusionsPage
                .WaitForSeclusionsPageToLoad()
                .OpenSeclusionRecord(seclusionRecord.ToString());

            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .WaitForSeclusionsRecordPageToLoad()
                .InsertEndDateTime(SeclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickSeclusionsDiscontinuedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation")
                .TapSearchButton()
                .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoad()
               .ClickDebriefProfessionalsLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation")
                .TapSearchButton()
                .ClickAddSelectedButton(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .TapSaveButton();

            System.Threading.Thread.Sleep(5000);

            var seclusionsRecord = dbHelper.inpatientSeclusion.GetByPersonId(_personID);
            Assert.AreEqual(1, seclusionsRecord.Count);

            var seclusionRecordFields = dbHelper.inpatientSeclusion.GetByID(seclusionsRecord[0], "enddatetime", "seclusiondiscontinuedbyid");



            Assert.AreEqual(SeclusionDate.ToLocalTime(), ((DateTime)seclusionRecordFields["enddatetime"]).ToLocalTime().Date);
            Assert.AreEqual(_AutomationSeclusionsUser1_SystemUserId.ToString(), seclusionRecordFields["seclusiondiscontinuedbyid"].ToString());

            #endregion Step 7 & step 8 

        }

        [TestProperty("JiraIssueID", "CDV6-15406")]
        [Description("Go to Advanced Search and search for Seclusions Record and saved view as Active." +
            "Click on Add new record" + "Enter all the mandatory fields and create a seclusion record" +
            "Validate the created record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Seclusions_UITestMethod07()
        {
            var seclusionDate = DateTime.Now.Date;
            var initialPlannedSeclusionDate = DateTime.Now.Date;

            dbHelper.inpatientWard.UpdateInpatientWardSeclusions(_inpationWardId, true);



            loginPage
                .GoToLoginPage()
                .Login("SeclusionsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                  .WaitForMainMenuToLoad()
                  .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Seclusions")
                .WaitForAdvanceSearchPageToLoad();

            advanceSearchPage
              .WaitForAdvanceSearchPageToLoad()
              .SelectSavedView("Active Records")
              .ClickSearchButton()
              .WaitForResultsPageToLoad()
              .ValidateNoRecordMessageVisibile(false)
              .ClickNewRecordButton_ResultsPage();


            seclusionsRecordPage
                 .WaitForSeclusionsRecordPageToLoadFromAdvancedSearch()
                 .ClickInpatientCaseLookUpButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation")
                .TapSearchButton()
                .SelectResultElement(_caseId.ToString());

            seclusionsRecordPage
              .WaitForSeclusionsRecordPageToLoadFromAdvancedSearch()
              .InsertSeclusionsDateTime(seclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
              .ClickSeclusionsReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation_Seclusions")
                .TapSearchButton()
                .SelectResultElement(_seclusionReasonid.ToString());


            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoadFromAdvancedSearch()
                .ClickApprovedByLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation")
                .TapSearchButton()
                .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoadFromAdvancedSearch()
               .InsertRationaleForSeclusions("Automation Seclusion = Yes")
               .SelectNOKcarerNotified("No")
               .ClickCommencedByLookUpButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery("Automation ")
              .TapSearchButton()
              .SelectResultElement(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoadFromAdvancedSearch()
               .InsertIntialPlannedReviewDateTime(initialPlannedSeclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .TapSaveButton();

            System.Threading.Thread.Sleep(5000);

            var seclusionRecord = dbHelper.inpatientSeclusion.GetByPersonId(_personID);
            Assert.AreEqual(1, seclusionRecord.Count);

            var seclusionRecordFields = dbHelper.inpatientSeclusion.GetByID(seclusionRecord[0], "ownerid", "personid", "caseid", "inpatientseclusionreasonid",
                                                "RationaleForSeclusion", "CommencedById", "CommencedApprovedById", "seclusiondatetime",
                                                "SeclusionReviewPlannedDateTime");


            Assert.AreEqual(_personID.ToString(), seclusionRecordFields["personid"].ToString());
            Assert.AreEqual(_caseId.ToString(), seclusionRecordFields["caseid"].ToString());
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), seclusionRecordFields["ownerid"].ToString());
            Assert.AreEqual(_seclusionReasonid.ToString(), seclusionRecordFields["inpatientseclusionreasonid"].ToString());
            Assert.AreEqual("Automation Seclusion = Yes", seclusionRecordFields["rationaleforseclusion"]);
            Assert.AreEqual(seclusionDate.ToLocalTime(), ((DateTime)seclusionRecordFields["seclusiondatetime"]).ToLocalTime().Date);
            Assert.AreEqual(initialPlannedSeclusionDate.ToLocalTime(), ((DateTime)seclusionRecordFields["seclusionreviewplanneddatetime"]).ToLocalTime().Date);
            Assert.AreEqual(_AutomationSeclusionsUser1_SystemUserId.ToString(), seclusionRecordFields["commencedbyid"].ToString());
            Assert.AreEqual(_AutomationSeclusionsUser1_SystemUserId.ToString(), seclusionRecordFields["commencedapprovedbyid"].ToString());



        }

        [TestProperty("JiraIssueID", "CDV6-15410")]
        [Description("Navigate to WorkPlace -> People -> Open the person record mentioned in pre requisite -> Click on Cases tab -> Open In Patient case " +
          "Navigat to Health seclusions and open the record" + "Click on Menu and go to the Attachment" +
            "Create new record and validate the same.")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Seclusions_UITestMethod08()
        {


            var attachmentDate = DateTime.Now.Date;

            //Setting the Seclusion ward flag to Yes
            dbHelper.inpatientWard.UpdateInpatientWardSeclusions(_inpationWardId, true);

            var seclusionRecord = dbHelper.inpatientSeclusion.CreateSeclusionRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, _seclusionReasonid,
                                        "Automation Seclusion = Yes", _AutomationSeclusionsUser1_SystemUserId, _AutomationSeclusionsUser1_SystemUserId, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date);

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login("SeclusionsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                  .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToSeclusionsPage();


            seclusionsPage
                  .WaitForSeclusionsPageToLoad()
                  .OpenSeclusionRecord(seclusionRecord.ToString());

            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .NavigateToAttachmentArea();

            inpatientSeclusionAttachmentPage
                .WaitForInpatientSeclusionAttachmentPageToLoad()
                .ClickNewRecordButton();



            inpatientSeclusionAttachmentRecordPage
               .WaitForInpatientSeclusionAttachmentRecordPageToLoad()
               .InsertTitle("Miss")
               .InsertDate(attachmentDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
               .ClickDocumentTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Inpatient Case Seclusion Attachment type")
                .TapSearchButton()
                .SelectResultElement(_documenttypeid.ToString());


            inpatientSeclusionAttachmentRecordPage
               .WaitForInpatientSeclusionAttachmentRecordPageToLoad()
               .ClickDocumentSubTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Inpatient Case Seclusion Attachment sub type")
                .TapSearchButton()
                .SelectResultElement(_documentsubtypeid.ToString());

            inpatientSeclusionAttachmentRecordPage
               .WaitForInpatientSeclusionAttachmentRecordPageToLoad()
               .UploadFile(TestContext.DeploymentDirectory + "\\Document.txt")
               .ClickSaveAndCloseButton();

            inpatientSeclusionAttachmentRecordPage
               .WaitForInpatientSeclusionAttachmentRecordPageToLoad();

            System.Threading.Thread.Sleep(5000);

            var attachments = dbHelper.inpatientSeclusionAttachment.GetByPersonId(_personID);
            Assert.AreEqual(1, attachments.Count);
            var newAttachment = attachments[0];

            inpatientSeclusionAttachmentPage
              .WaitForInpatientSeclusionAttachmentPageToLoad()
              .ValidateNoRecordMessageVisibile(false)
              .ValidateRecordVisible(newAttachment.ToString());


            #endregion Step 10

        }

        [TestProperty("JiraIssueID", "CDV6-15412")]
        [Description("Navigate to WorkPlace -> People -> Open the person record mentioned in pre requisite -> Click on Cases tab -> Open In Patient case " +
           "Navigate to Health Seclusion and open the seclusion record." + "Navigate to Seclusion review record" +
            "Update the mandatory fields and save the record and validate the same")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Seclusions_UITestMethod09()
        {


            var SeclusionDate = DateTime.Now.Date;

            //Setting the Seclusion ward flag to Yes
            dbHelper.inpatientWard.UpdateInpatientWardSeclusions(_inpationWardId, true);

            var seclusionRecord = dbHelper.inpatientSeclusion.CreateSeclusionRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, _seclusionReasonid,
                                        "Automation Seclusion = Yes", _AutomationSeclusionsUser1_SystemUserId, _AutomationSeclusionsUser1_SystemUserId, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date);

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login("SeclusionsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                  .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToSeclusionsPage();


            seclusionsPage
                .WaitForSeclusionsPageToLoad()
                .OpenSeclusionRecord(seclusionRecord.ToString());

            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .NavigateToSeclusionReviews();

            var seclusionReviewRecord = dbHelper.inpatientSeclusionReview.GetByPersonId(_personID);
            Assert.AreEqual(1, seclusionReviewRecord.Count);

            seclusionReviewsPage
                .WaitForSeclusionReviewsPageToLoad()
                .OpenSeclusionReviewRecord(seclusionReviewRecord[0].ToString());

            seclusionReviewsRecordPage
                .WaitForSeclusionReviewsRecordPageToLoad()
                .InsertActualReviewDateTime(SeclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .SelectWillPersonContinueInSeclusion("No")
                .InsertReviewCommentsAndActionPlan("Review")
                .ClickProfessionalsAtReviewLookUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("Automation")
                 .TapSearchButton()
                 .ClickAddSelectedButton(_AutomationSeclusionsUser1_SystemUserId.ToString());

            seclusionReviewsRecordPage
               .WaitForSeclusionReviewsRecordPageToLoad()
               .TapSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Please complete the “End of Seclusion” section now present on the seclusion record, this will indicate the person is now out of Seclusion.")
                .TapOKButton();

            seclusionReviewsRecordPage
                 .WaitForSeclusionReviewsRecordPageToLoad()
                 .TapBackButton();

            System.Threading.Thread.Sleep(5000);

            var seclusionReviewRecordFields = dbHelper.inpatientSeclusionReview.GetByID(seclusionReviewRecord[0], "ownerid", "owningbusinessunitid", "inpatientseclusionid", "caseid", "personid", "reviewcommentsandactionplan");

            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), seclusionReviewRecordFields["ownerid"].ToString());
            Assert.AreEqual(_careDirectorQA_BusinessUnitId.ToString(), seclusionReviewRecordFields["owningbusinessunitid"].ToString());
            Assert.AreEqual(_caseId.ToString(), seclusionReviewRecordFields["caseid"].ToString());
            Assert.AreEqual("Review", seclusionReviewRecordFields["reviewcommentsandactionplan"]);


            #endregion Step 11
        }

        [TestProperty("JiraIssueID", "CDV6-15425")]
        [Description("Navigate to WorkPlace -> People -> Open the person record mentioned in pre requisite -> Click on Cases tab -> Open In Patient case " +
         "Navigate to Health Seclusion and open the seclusion record." + "Navigate to Seclusion review record" +
          "Update the mandatory fields and save the record and Validate the error message.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Seclusions_UITestMethod10()
        {


            var SeclusionDate = DateTime.Now.Date;

            //Setting the Seclusion ward flag to Yes
            dbHelper.inpatientWard.UpdateInpatientWardSeclusions(_inpationWardId, true);

            var seclusionRecord = dbHelper.inpatientSeclusion.CreateSeclusionRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, _seclusionReasonid,
                                        "Automation Seclusion = Yes", _AutomationSeclusionsUser1_SystemUserId, _AutomationSeclusionsUser1_SystemUserId, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date);

            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login("SeclusionsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                  .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToSeclusionsPage();


            seclusionsPage
                .WaitForSeclusionsPageToLoad()
                .OpenSeclusionRecord(seclusionRecord.ToString());

            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .NavigateToSeclusionReviews();

            var seclusionReviewRecord = dbHelper.inpatientSeclusionReview.GetByPersonId(_personID);
            Assert.AreEqual(1, seclusionReviewRecord.Count);

            seclusionReviewsPage
                .WaitForSeclusionReviewsPageToLoad()
                .ClickNewRecordButton();

            seclusionReviewsRecordPage
                .WaitForSeclusionReviewsRecordPageToLoad()
                .InsertPlannedReviewDateTime(SeclusionDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .TapSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There can only be one active seclusion review per seclusion record");

            #endregion
        }

        [TestProperty("JiraIssueID", "CDV6-15427")]
        [Description("Navigate to WorkPlace -> People -> Open the person record mentioned in pre requisite -> Click on Cases tab -> Open In Patient case " +
        "Navigate to Health Seclusion and open the seclusion record." + "Update the record and Navigate to Audit page" +
            "Validate the Audit Update ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Seclusions_UITestMethod11()
        {

            //Setting the Seclusion ward flag to Yes
            dbHelper.inpatientWard.UpdateInpatientWardSeclusions(_inpationWardId, true);

            var seclusionRecord = dbHelper.inpatientSeclusion.CreateSeclusionRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _caseId, _seclusionReasonid,
                                        "Automation Seclusion = Yes", _AutomationSeclusionsUser1_SystemUserId, _AutomationSeclusionsUser1_SystemUserId, DateTime.Now.AddDays(-5).Date, DateTime.Now.Date);

            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login("SeclusionsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                 .OpenPersonRecord(_personID.ToString());

            personRecordPage
                 .WaitForPersonRecordPageToLoad()
                 .TapCasesTab();

            personCasesPage
                 .WaitForPersonCasesPageToLoad()
                  .OpenCaseRecord(_caseId.ToString());

            personCasesRecordPage
                 .WaitForPersonCasesRecordPageToLoad()
                 .NavigateToSeclusionsPage();


            seclusionsPage
                .WaitForSeclusionsPageToLoad()
                .OpenSeclusionRecord(seclusionRecord.ToString());

            seclusionsRecordPage
                .WaitForSeclusionsRecordPageToLoad()
                .InsertRationaleForSeclusions("Updated")
                .TapSaveButton()
                .WaitForSeclusionsRecordPageToLoad();

            seclusionsRecordPage
               .WaitForSeclusionsRecordPageToLoad()
               .NavigateToAuditPage();

            auditListPage
                .WaitForAuditListPageToLoad("inpatientseclusion");


            System.Threading.Thread.Sleep(5000);

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = seclusionRecord.ToString(),
                ParentTypeName = "InpatientSeclusion",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual("Seclusions User1", auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId = auditResponseData.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoad("inpatientseclusion")
                .ValidateRecordPresent(auditRecordId)
                .ClickOnAuditRecord(auditRecordId);


            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Update")
                .ValidateChangedBy("Seclusions User1");


            #endregion Step 13
        }


        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }



}