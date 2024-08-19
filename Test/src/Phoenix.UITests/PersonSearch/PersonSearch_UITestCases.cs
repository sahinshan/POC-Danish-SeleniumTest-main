using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.PersonSearch
{
    /// <summary>
    /// This class contains Automated UI test scripts for Person Search 
    /// </summary>
    [TestClass]
    public class PersonSearch_UITestCases : FunctionalTest
    {
        #region Properties

        private string _tenantName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _AutomationTestUser1_SystemUserId;
        private Guid _AuditSearchUserAdmin_SystemUserId;
        private string _AuditSearchUserAdmin_UserName;
        private string _AuditSearchUserAdmin_FullName;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _ethnicityId;
        private Guid _personID;
        private int _personNumber;
        private Guid _caseStatusId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _dataFormId;
        private Guid _childProtectionCategoryofAbuseId;
        private Guid _childProtectionStatusTypeId;
        private Guid _childProtectionEndReasonTypeId;
        private Guid _personRelationshipType;
        private string _AutomationTestUser1_SystemUserName;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void UIPersonSearch_SetupTest()
        {
            #region Tenant

            _tenantName = ConfigurationManager.AppSettings["TenantName"];
            dbHelper = new DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

            #endregion

            #region Authentication Provider

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

            #endregion

            #region Default User

            string username = ConfigurationManager.AppSettings["Username"];
            string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];
            if (dataEncoded.Equals("true"))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(username);
                username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            }

            var userid = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);

            #endregion

            #region Language

            _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

            #endregion Language

            #region Business Unit
            _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

            #endregion

            #region Team

            _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careDirectorQA_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

            #endregion

            #region System User - Automation_Test_User_1
            dbHelper = new DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);
            _AutomationTestUser1_SystemUserName = "Automation_Test_User_1";
            _AutomationTestUser1_SystemUserId = commonMethodsDB.CreateSystemUserRecord("Automation_Test_User_1", "Automation", "Test User 1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            #region Ethnicity

            _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-19134 & https://advancedcsg.atlassian.net/browse/CDV6-19136

        [TestProperty("JiraIssueID", "ACC-3588")]
        [Description("Login to CD as a user and click on Person search button." +
            "Person Record should be DOD value - Serach Person Record and Validate 'Date of Death' Column with proper value" +
            "Open Person from Workplage and Search Person Record - Record should be display with 'Date of Death' Column with proper value.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("IntegrationTestLevel3_SocialCare_AWS"), TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Person Search")]
        public void PersonSearchToCheckDOD_UITestMethod001()
        {
            #region Create Person Record

            var firstName = "Andre";
            var lastName = currentTimeString;
            var addresstypeid = 6; //Home

            _personID = commonMethodsDB.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, new DateTime(2020, 10, 20), addresstypeid, 1, "9876543210", "", "1234567890", "",
            "pna", "pno", "st", "dist", "tow", "cou", "CR0 3RL");
            dbHelper.person.UpdateDeceased(_personID, true, new DateTime(2020, 1, 2));

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_AutomationTestUser1_SystemUserName, "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            #endregion

            #region Step 3 & 4

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertLastName(lastName)
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_personID.ToString())

                .ValidateRecordCellText(_personID.ToString(), "Date of Death", "02/01/2020");

            #endregion

            #region Step 5

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertLastName(lastName)
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_personID.ToString())

            .ValidateRecordCellText(_personID.ToString(), "Date of Death", "02/01/2020");

            #endregion
        }


        [TestProperty("JiraIssueID", "CDV6-8829")]
        [Description("Login to CD as a user and click on Person search button." +
            "When Person don't have CP or Related CP Record - Search Person Record and Validate 'Has Child Protection' & 'Has Related Child Protection' column field should not be display any icon and on mouse over it should display as 'No' tooltip message." +
            "When Person have CP or Related CP Record - Search Person Record and Validate 'Has Child Protection' & 'Has Related Child Protection' column field should display with icon and on mouse over it should display as 'Yes' tooltip message." +
            "Open Person Record - Validate 'Has Child Protection' & 'Has Related Child Protection' Banner Icon should be display.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonSearchToCheckChildProtection_TestMethod001()
        {

            #region Contact Reason

            var contactReasonExists = dbHelper.contactReason.GetByName("PersonCareTest_ContactReason").Any();
            if (!contactReasonExists)
                dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "PersonCareTest_ContactReason", new DateTime(2020, 1, 1), 110000000, false);
            _contactReasonId = dbHelper.contactReason.GetByName("PersonCareTest_ContactReason")[0];

            #endregion

            #region Contact Source

            var contactSourceExists = dbHelper.contactSource.GetByName("PersonCareTest_ContactSource").Any();
            if (!contactSourceExists)
                dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "PersonCareTest_ContactSource", new DateTime(2020, 1, 1));
            _contactSourceId = dbHelper.contactSource.GetByName("PersonCareTest_ContactSource")[0];

            #endregion

            #region Case Status

            _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

            #endregion

            #region DataForm

            _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            #endregion

            #region Create Person Record

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            // First Person Record
            _personID = dbHelper.person.CreatePersonRecord("", "Testing_CDV6_19136_User-1", "", currentDate, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2, true, new DateTime(2020, 1, 2));
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            // Related Person Record
            var _personRelated = dbHelper.person.CreatePersonRecord("", "Testing_CDV6_19136_Related_User", "", currentDate, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2, true, new DateTime(2020, 1, 2));

            // Person Record 3
            var _personRecordWithoutCP = dbHelper.person.CreatePersonRecord("", "Testing_CDV6_19136_Without_CP", "", currentDate, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2, true, new DateTime(2020, 1, 2));
            var _withoutCPPersonNumber = (int)dbHelper.person.GetPersonById(_personRecordWithoutCP, "personnumber")["personnumber"];

            #endregion

            #region Create Case Record

            // Case for First Person Record
            var _newCaseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _AutomationTestUser1_SystemUserId, _AutomationTestUser1_SystemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);

            // Case for Related Person Record
            var _newCaseId2 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personRelated, _AutomationTestUser1_SystemUserId, _AutomationTestUser1_SystemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);

            #endregion

            #region create Child Protection Category of Abuse

            var abuseCategory = dbHelper.childProtectionCategoryOfAbuse.GetByName("Test_CP_Abuse_Category").Any();
            if (!abuseCategory)
                dbHelper.childProtectionCategoryOfAbuse.CreateChildProtectionCategoryOfAbuse(_careDirectorQA_TeamId, "Test_CP_Abuse_Category", DateTime.Now);
            _childProtectionCategoryofAbuseId = dbHelper.childProtectionCategoryOfAbuse.GetByName("Test_CP_Abuse_Category")[0];

            #endregion

            #region create Child Protection Status Type	

            var childProtectionStatusType = dbHelper.childProtectionStatusType.GetByName("Test_CP_Status").Any();
            if (!childProtectionStatusType)
                dbHelper.childProtectionStatusType.CreateChildProtectionStatusType(_careDirectorQA_TeamId, "Test_CP_Status", DateTime.Now);
            _childProtectionStatusTypeId = dbHelper.childProtectionStatusType.GetByName("Test_CP_Status")[0];

            #endregion

            #region create Child Protection End Reason Type

            var childProtectionEndReasonType = dbHelper.childProtectionEndReasonType.GetByName("Test_End_Reason_1").Any();
            if (!childProtectionEndReasonType)
                dbHelper.childProtectionEndReasonType.CreateChildProtectionEndReasonType(_careDirectorQA_TeamId, "Test_End_Reason_1", DateTime.Now);
            _childProtectionEndReasonTypeId = dbHelper.childProtectionEndReasonType.GetByName("Test_End_Reason_1")[0];

            #endregion

            #region Create Child Protection

            // Child Protection for First Person Record
            Guid childProtectionId = dbHelper.ChildProtection.CreateChildProtection(_careDirectorQA_TeamId, _newCaseId, _personID, _childProtectionCategoryofAbuseId, _childProtectionStatusTypeId, new DateTime(2021, 10, 10), new DateTime(2021, 10, 10), new DateTime(2021, 10, 10), _childProtectionEndReasonTypeId);

            // Child Protection for Related Person Record
            Guid childProtectionId2 = dbHelper.ChildProtection.CreateChildProtection(_careDirectorQA_TeamId, _newCaseId2, _personRelated, _childProtectionCategoryofAbuseId, _childProtectionStatusTypeId, new DateTime(2021, 10, 10), new DateTime(2021, 10, 10), new DateTime(2021, 10, 10), _childProtectionEndReasonTypeId);


            #endregion

            #region Create Person Relationship Type

            var personRelationshipType = dbHelper.personRelationshipType.GetByName("Friend").Any();
            if (!personRelationshipType)
                dbHelper.personRelationshipType.CreatePersonRelationshipType(_careDirectorQA_TeamId, "Friend", DateTime.Now);
            _personRelationshipType = dbHelper.personRelationshipType.GetByName("Friend")[0];

            #endregion

            #region Create Relationship with Person

            DateTime startDate = new DateTime(2021, 10, 10);
            Guid relationship1 = dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId,
                _personID, "Testing_CDV6_19136_User-1" + currentDate,
                _personRelationshipType, "Friend",
                _personRelated, "Testing_CDV6_19136_Related_User" + currentDate,
                _personRelationshipType, "Friend",
                startDate, "desc ...", 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_AutomationTestUser1_SystemUserName, "Passw0rd_!");

            #endregion

            #region Step 3 & 6

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertId(_withoutCPPersonNumber.ToString())
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_personRecordWithoutCP.ToString())

                .VerifyColumnCellText_Icon_Tooltip(_personRecordWithoutCP.ToString(), "Has Child Protection", "", "No")
                .VerifyColumnCellText_Icon_Tooltip(_personRecordWithoutCP.ToString(), "Has Related Child Protection", "", "No");

            #endregion

            #region Step 4,5 & 7

            personSearchPage
                .InsertId(_personNumber.ToString())
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_personID.ToString())

                .VerifyColumnCellText_Icon_Tooltip(_personID.ToString(), "Has Child Protection", "person-cp-icon.png", "Yes")
                .VerifyColumnCellText_Icon_Tooltip(_personID.ToString(), "Has Related Child Protection", "person-related-cp-icon.png", "Yes");

            #endregion

            #region Step 2 & 8

            personSearchPage
            .ClickOnRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .ValidateKnownToChildProtection_Icon(true)
                .ValidatetitleRelatedPerson_KnownToChildProtection_Icon(true);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10463

        [TestProperty("JiraIssueID", "CDV6-24950")]
        [Description("Access the person search screen - Insert a first name, middle name, last name, stated gender, nhs no, dob - " +
            "Click on the search button - " +
            "Validate that a new audit record is created with the search parameters - " +
            "Validate that the audit states that inactive records are not included in the results")]
        [TestMethod(), TestCategory("UITest")]
        public void PersonSearchAudit_UITestMethod001()
        {
            CreateAuditAdminTestUser();

            #region Create Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            var personFirstName = "Rosalyn";
            var personMiddleName = currentDate;
            var personLastName = "Meyers";
            var preferredName = "";

            var _personID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(1966, 08, 24), _ethnicityId, _careDirectorQA_TeamId, 7, 4, "987 654 3210");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_AuditSearchUserAdmin_UserName, "Passw0rd_!")
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Rosalyn")
                .InsertMiddleName(currentDate)
                .InsertLastName("Meyers")
                .SelectStatedGender("Indeterminate")
                .InsertNHSNo("987 654 3210")
                .InsertDOB("24/08/1966")
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_personID.ToString());

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "0",
                IsGeneralAuditSearch = true,
                Operation = 29,
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                UsePaging = true,
                UserId = _AuditSearchUserAdmin_SystemUserId.ToString(),
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Person", auditResponseData.GridData[0].cols[1].Text);
            Assert.AreEqual("Person search. Not Includes inactive records.", auditResponseData.GridData[0].cols[2].Text);
            Assert.AreEqual("NHS No.: 987 654 3210,DOB: 24/08/1966,Gender: Indeterminate,First Name: Rosalyn,Last Name: Meyers,Middle Name: " + currentDate + ",", auditResponseData.GridData[0].cols[6].Text);
        }

        [TestProperty("JiraIssueID", "CDV6-24951")]
        [Description("Access the person search screen - Insert a first name, middle name, last name, Date of Birth From, Date of Birth To - " +
            "Click on the search button - " +
            "Validate that a new audit record is created with the search parameters - " +
            "Validate that the audit states that inactive records are not included in the results")]
        [TestMethod(), TestCategory("UITest")]
        public void PersonSearchAudit_UITestMethod002()
        {
            CreateAuditAdminTestUser();

            #region Create Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            var personFirstName = "Rosalyn";
            var personMiddleName = currentDate;
            var personLastName = "Meyers";
            var preferredName = "";

            var _personID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(1966, 08, 24), _ethnicityId, _careDirectorQA_TeamId, 7, 4, "987 654 3210");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_AuditSearchUserAdmin_UserName, "Passw0rd_!")
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Rosalyn")
                .InsertMiddleName(currentDate)
                .InsertLastName("Meyers")
                .ClickUseDateOfBirthRangeCheckbox()
                .InsertDateOfBirthFrom("24/07/1966")
                .InsertDateOfBirthTo("24/09/1966")
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_personID.ToString());

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "0",
                IsGeneralAuditSearch = true,
                Operation = 29,
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                UsePaging = true,
                UserId = _AuditSearchUserAdmin_SystemUserId.ToString(),
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Person", auditResponseData.GridData[0].cols[1].Text);
            Assert.AreEqual("Person search. Not Includes inactive records.", auditResponseData.GridData[0].cols[2].Text);
            Assert.AreEqual("DOB From: 24/07/1966,DOB To: 24/09/1966,First Name: Rosalyn,Last Name: Meyers,Middle Name: " + currentDate + ",", auditResponseData.GridData[0].cols[6].Text);
        }

        [TestProperty("JiraIssueID", "CDV6-24952")]
        [Description("Access the person search screen - Insert an Id - " +
            "Click on the search button - " +
            "Validate that a new audit record is created with the search parameter - " +
            "Validate that the audit states that inactive records are not included in the results")]
        [TestMethod(), TestCategory("UITest")]
        public void PersonSearchAudit_UITestMethod003()
        {
            CreateAuditAdminTestUser();

            #region Create Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            var personFirstName = "Rosalyn";
            var personMiddleName = currentDate;
            var personLastName = "Meyers";
            var preferredName = "";

            var _personID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(1966, 08, 24), _ethnicityId, _careDirectorQA_TeamId, 7, 4, "987 654 3210");
            var personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_AuditSearchUserAdmin_UserName, "Passw0rd_!")
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertId(personNumber.ToString())
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_personID.ToString());

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "0",
                IsGeneralAuditSearch = true,
                Operation = 29,
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                UsePaging = true,
                UserId = _AuditSearchUserAdmin_SystemUserId.ToString(),
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Person", auditResponseData.GridData[0].cols[1].Text);
            Assert.AreEqual("Person search. Not Includes inactive records.", auditResponseData.GridData[0].cols[2].Text);
            Assert.AreEqual("Id: " + personNumber + ",", auditResponseData.GridData[0].cols[6].Text);
        }

        [TestProperty("JiraIssueID", "CDV6-24953")]
        [Description("Access the person search screen - Insert an Legacy Id, Unique Pupil No, National Insurance Number - " +
            "Click on the search button - " +
            "Validate that a new audit record is created with the search parameter - " +
            "Validate that the audit states that inactive records are not included in the results")]
        [TestMethod(), TestCategory("UITest")]
        public void PersonSearchAudit_UITestMethod004()
        {
            CreateAuditAdminTestUser();

            #region Create Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            var personFirstName = "Rosalyn";
            var personMiddleName = "";
            var personLastName = "Meyers";
            var preferredName = "";

            var _personID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(1966, 08, 24), _ethnicityId, _careDirectorQA_TeamId, 7, 4, "987 654 3210", currentDate, "SJK8DS99H8FGFD", "3746SAS32HJH3G43SS");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_AuditSearchUserAdmin_UserName, "Passw0rd_!")
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertLegacyId(currentDate)
                .InsertNationalInsuranceNumber("SJK8DS99H8FGFD")
                .InsertUniquePupilNo("3746SAS32HJH3G43SS")
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_personID.ToString());

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "0",
                IsGeneralAuditSearch = true,
                Operation = 29,
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                UsePaging = true,
                UserId = _AuditSearchUserAdmin_SystemUserId.ToString(),
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Person", auditResponseData.GridData[0].cols[1].Text);
            Assert.AreEqual("Person search. Not Includes inactive records.", auditResponseData.GridData[0].cols[2].Text);
            Assert.AreEqual("Unique Pupil Number: 3746SAS32HJH3G43SS,Legacy Id: " + currentDate + ",National Insurance Number: SJK8DS99H8FGFD,", auditResponseData.GridData[0].cols[6].Text);
        }

        [TestProperty("JiraIssueID", "CDV6-24954")]
        [Description("Access the person search screen - Insert an Legacy Id, Property Name, Property No, Street, Village/District, Town/City, County, Postcode - " +
            "Click on the search button - " +
            "Validate that a new audit record is created with the search parameter - " +
            "Validate that the audit states that inactive records are not included in the results")]
        [TestMethod(), TestCategory("UITest")]
        public void PersonSearchAudit_UITestMethod005()
        {
            CreateAuditAdminTestUser();

            #region Create Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            var personFirstName = "Rosalyn";
            var personMiddleName = "";
            var personLastName = "Meyers";
            var preferredName = "";

            var _personID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName,
                new DateTime(1966, 08, 24), _ethnicityId, _careDirectorQA_TeamId, 7, 4, "987 654 3210", currentDate, null, null,
                "Bromley", "168469", "Moray Mews", "London", "London", "Greater London", "BR2 0NF");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_AuditSearchUserAdmin_UserName, "Passw0rd_!")
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertLegacyId(currentDate)
                .InsertPropertyName("Bromley")
                .InsertPropertyNo("168469")
                .InsertStreet("Moray Mews,")
                .InsertVillageDistrict("London")
                .InsertTownCity("London")
                .InsertCounty("Greater London")
                .InsertPostCode("BR2 0NF")
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_personID.ToString());

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "0",
                IsGeneralAuditSearch = true,
                Operation = 29,
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                UsePaging = true,
                UserId = _AuditSearchUserAdmin_SystemUserId.ToString(),
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Person", auditResponseData.GridData[0].cols[1].Text);
            Assert.AreEqual("Person search. Not Includes inactive records.", auditResponseData.GridData[0].cols[2].Text);
            Assert.AreEqual("Legacy Id: " + currentDate + ",Property Name: Bromley,Property No: 168469,Street: Moray Mews,,Vlg/District: London,Town/City: London,County: Greater London,Post Code: BR2 0NF,", auditResponseData.GridData[0].cols[6].Text);
        }

        [TestProperty("JiraIssueID", "CDV6-24955")]
        [Description("Access the person search screen - Insert a first name, last name - Click on the Sounds Like checkbox - " +
            "Click on the search button - " +
            "Validate that a new audit record is created with the search parameters - " +
            "Validate that the audit states that inactive records are not included in the results")]
        [TestMethod(), TestCategory("UITest")]
        public void PersonSearchAudit_UITestMethod006()
        {
            CreateAuditAdminTestUser();

            #region Create Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            var personFirstName = "Rosalyn";
            var personMiddleName = "";
            var personLastName = currentDate;
            var preferredName = "";

            var _personID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(1966, 08, 24), _ethnicityId, _careDirectorQA_TeamId, 7, 4, "987 654 3210");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_AuditSearchUserAdmin_UserName, "Passw0rd_!")
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Rosalyn")
                .InsertLastName(currentDate)
                .ClickUseSoundsLikeCheckbox()
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_personID.ToString());


            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "0",
                IsGeneralAuditSearch = true,
                Operation = 29,
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                UsePaging = true,
                UserId = _AuditSearchUserAdmin_SystemUserId.ToString(),
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Person", auditResponseData.GridData[0].cols[1].Text);
            Assert.AreEqual("Person search. Not Includes inactive records.", auditResponseData.GridData[0].cols[2].Text);
            Assert.AreEqual("Sounds: Yes,First Name: Rosalyn,Last Name: " + currentDate + ",", auditResponseData.GridData[0].cols[6].Text);
        }

        [TestProperty("JiraIssueID", "CDV6-24956")]
        [Description("Access the person search screen - Insert a first name, last name - Click on the Include Inactive? checkbox - " +
            "Click on the search button - " +
            "Validate that a new audit record is created with the search parameters - " +
            "Validate that the audit states that inactive records are not included in the results")]
        [TestMethod(), TestCategory("UITest")]
        public void PersonSearchAudit_UITestMethod007()
        {
            CreateAuditAdminTestUser();

            #region Create Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            var personFirstName = "Rosalyn";
            var personMiddleName = "";
            var personLastName = currentDate;
            var preferredName = "";

            var _personID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(1966, 08, 24), _ethnicityId, _careDirectorQA_TeamId, 7, 4, "987 654 3210");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_AuditSearchUserAdmin_UserName, "Passw0rd_!")
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Rosalyn")
                .InsertLastName(currentDate)
                .ClickIncludeInactiveCheckbox()
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_personID.ToString());


            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "0",
                IsGeneralAuditSearch = true,
                Operation = 29,
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                UsePaging = true,
                UserId = _AuditSearchUserAdmin_SystemUserId.ToString(),
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Person", auditResponseData.GridData[0].cols[1].Text);
            Assert.AreEqual("Person search. Includes inactive records.", auditResponseData.GridData[0].cols[2].Text);
            Assert.AreEqual("First Name: Rosalyn,Last Name: " + currentDate + ",", auditResponseData.GridData[0].cols[6].Text);
        }

        [TestProperty("JiraIssueID", "CDV6-24957")]
        [Description("Access the person search screen - Insert a first name, last name - Click on the Include Inactive? checkbox - " +
            "Click on the search button - " +
            "Validate that a new audit record is created with the search parameters - " +
            "Validate that the audit states that inactive records are not included in the results")]
        [TestMethod(), TestCategory("UITest")]
        public void PersonSearchAudit_UITestMethod008()
        {
            CreateAuditAdminTestUser();

            #region Create Person

            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            var personFirstName = "Rosalyn";
            var personMiddleName = "";
            var personLastName = currentDate;
            var preferredName = "";

            var _personID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(1966, 08, 24), _ethnicityId, _careDirectorQA_TeamId, 7, 4, "987 654 3210");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_AuditSearchUserAdmin_UserName, "Passw0rd_!")
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Rosalyn")
                .InsertLastName(currentDate)
                .ClickIncludeInactiveCheckbox()
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_personID.ToString());

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "0",
                IsGeneralAuditSearch = true,
                Operation = 29,
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                UsePaging = true,
                UserId = _AuditSearchUserAdmin_SystemUserId.ToString(),
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);

            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Person", auditResponseData.GridData[0].cols[1].Text);
            var auditRecordId = auditResponseData.GridData[0].Id;

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAuditListSection();

            auditListPage
                .WaitForAdminAuditListPageToLoad()
                .TapUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery(_AuditSearchUserAdmin_UserName).TapSearchButton().SelectResultElement(_AuditSearchUserAdmin_SystemUserId.ToString());

            auditListPage
                .WaitForAdminAuditListPageToLoad()
                .ClickSearchButton()

                .ValidateAdminAuditListRecordTypeCellText(auditRecordId, "Person")
                .ValidateAdminAuditListTitleCellText(auditRecordId, "Person search. Includes inactive records.")
                .ValidateAdminAuditListOperationCellText(auditRecordId, "Person Search")
                .ValidateAdminAuditListUserCellText(auditRecordId, _AuditSearchUserAdmin_FullName)
                .ValidateAdminAuditCommentsCellText(auditRecordId, "First Name: Rosalyn,Last Name: " + currentDate + ",")
                .ValidateAdminAuditApplicationCellText(auditRecordId, "CareDirector")
                .ValidateAdminAuditReasonCellText(auditRecordId, "")

                .ClickOnAdminAuditRecord(auditRecordId);

            auditChangeSetDialogPopup
                .WaitForAdminAuditChangeSetDialogPopupToLoad()

                .ValidateAdminAuditDetailOperation("Person Search")
                .ValidateAdminAuditDetailUser(_AuditSearchUserAdmin_FullName)
                .ValidateAdminAuditDetailComments("First Name: Rosalyn,Last Name: " + currentDate + ",");
            ;
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-19137

        [TestProperty("JiraIssueID", "ACC-3572")]
        [Description("Test case for CDV6-10471 - Enhance Person Search to include Preferred Names.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("IntegrationTestLevel3_SocialCare_AWS"), TestCategory("IntegrationTestLevel3_MentalHealth_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Person Search")]
        public void PersonSearch_UITestMethod001()
        {
            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            #region Person 1

            var personFirstName = "FN_" + currentDate;
            var personMiddleName = "MD_" + currentDate;
            var personLastName = "LN_" + currentDate;
            var preferredName = "PN_" + currentDate;
            var _person1ID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(2000, 1, 1), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion

            #region Person 2

            personFirstName = "FN_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            personMiddleName = "";
            personLastName = "Testing_CDV6_19137";
            preferredName = "FN_" + currentDate;
            var _person2ID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion

            #region Person 3

            personFirstName = "FN_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            personMiddleName = "";
            personLastName = "Testing_CDV6_19137";
            preferredName = "";
            var _person3ID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(2000, 1, 3), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion

            #region Person 4

            personFirstName = "FN_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            personMiddleName = "";
            personLastName = "Testing_CDV6_19137";
            preferredName = "MD_" + currentDate;
            var _person4ID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(2000, 1, 4), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion

            #region Person 5

            personFirstName = "FN_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            personMiddleName = "";
            personLastName = "Testing_CDV6_19137";
            preferredName = "LN_" + currentDate;
            var _person5ID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(2000, 1, 5), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion


            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_AutomationTestUser1_SystemUserName, "Passw0rd_!")
                .WaitFormHomePageToLoad(false, false, false);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            #endregion

            #region Step 3

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("FN_" + currentDate)
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()

                .WaitForRecordVisible(_person1ID.ToString())
                .WaitForRecordVisible(_person2ID.ToString())
                .ValidateRecordNotVisible(_person3ID.ToString());

            #endregion

            #region Step 4

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickClearFiltersButton()
                .InsertMiddleName("MD_" + currentDate)
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()

                .WaitForRecordVisible(_person1ID.ToString())
                .WaitForRecordVisible(_person4ID.ToString())
                .ValidateRecordNotVisible(_person3ID.ToString());

            #endregion

            #region Step 5 & 6

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickClearFiltersButton()
                .InsertLastName("LN_" + currentDate)
                .ClickSearchButton()

                .WaitForResultsAreaToLoad()

                .WaitForRecordVisible(_person1ID.ToString())
                .WaitForRecordVisible(_person5ID.ToString())
                .ValidateRecordNotVisible(_person3ID.ToString());

            #endregion

            #region Step 7 & 8

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickClearFiltersButton()
                .InsertFirstName("FN_" + currentDate)
                .InsertMiddleName("MD_" + currentDate)
                .InsertLastName("LN_" + currentDate)
                .ClickSearchButton();

            personSearchPage
                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_person1ID.ToString())
                .ValidateRecordNotVisible(_person2ID.ToString())
                .ValidateRecordNotVisible(_person3ID.ToString())
                .ValidateRecordNotVisible(_person4ID.ToString())
                .ValidateRecordNotVisible(_person5ID.ToString())
                .ValidateRecordIsUnique(_person1ID.ToString());

            #endregion

            #region Step 9

            personSearchPage
                .ValidateRecordCellText(_person1ID.ToString(), "Last Name", "LN_" + currentDate)
                .ValidateRecordCellText(_person1ID.ToString(), "Preferred Name", "PN_" + currentDate)
                .ValidateRecordCellText(_person1ID.ToString(), "DOB", "01/01/2000");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-19138

        [TestProperty("JiraIssueID", "ACC-3573")]
        [Description("Test case for CDV6-18043- Performance - Enforce min 3 characters per person search")]
        [TestMethod(), TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("IntegrationTestLevel3_SocialCare_AWS"), TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Person Search")]
        public void PersonSearch_UITestMethod002()
        {
            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var partialDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Person 1

            var personFirstName = "FN_";
            var personMiddleName = "MD_";
            var personLastName = "LN_";
            var preferredName = "PN_";
            var _person1ID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(2000, 1, 1), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion

            #region Person 2

            personFirstName = "FN_" + currentDate;
            personMiddleName = "MD_" + currentDate;
            personLastName = "LN_" + currentDate;
            preferredName = "FN_";
            var _person2ID = dbHelper.person.CreatePersonRecord("", personFirstName, personMiddleName, personLastName, preferredName, new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion



            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_AutomationTestUser1_SystemUserName, "Passw0rd_!")
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            #endregion

            #region Step 2

            /*This step is no longer valid. The application requirements changed.
             Now the minimum number of characters for the search is 1*/

            //personSearchPage
            //    .WaitForPersonSearchPageToLoad()
            //    .InsertFirstName("FN")
            //    .ClickSearchButton()

            //    .ValidateNotificationMessageVisibility(true)
            //    .ValidateNotificationHideLinkVisibility(true)
            //    .ValidateNotificationMessageText("Please supply at least 3 characters in one of your search values.")
            //    .ClickNotificationHideLink()
            //    .ValidateNotificationMessageVisibility(false)
            //    .ValidateNotificationHideLinkVisibility(false);

            #endregion

            #region Step 3

            /*This step is no longer valid. The application requirements changed.
             Now the minimum number of characters for the search is 1*/

            //personSearchPage
            //    .WaitForPersonSearchPageToLoad()
            //    .ClickClearFiltersButton()
            //    .InsertMiddleName("MD")
            //    .ClickSearchButton()

            //    .ValidateNotificationMessageVisibility(true)
            //    .ValidateNotificationHideLinkVisibility(true)
            //    .ValidateNotificationMessageText("Please supply at least 3 characters in one of your search values.")
            //    .ClickNotificationHideLink()
            //    .ValidateNotificationMessageVisibility(false)
            //    .ValidateNotificationHideLinkVisibility(false);

            //personSearchPage
            //    .WaitForPersonSearchPageToLoad()
            //    .ClickClearFiltersButton()
            //    .InsertLastName("LN")
            //    .ClickSearchButton()

            //    .ValidateNotificationMessageVisibility(true)
            //    .ValidateNotificationHideLinkVisibility(true)
            //    .ValidateNotificationMessageText("Please supply at least 3 characters in one of your search values.")
            //    .ClickNotificationHideLink()
            //    .ValidateNotificationMessageVisibility(false)
            //    .ValidateNotificationHideLinkVisibility(false);

            #endregion

            #region Step 4

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickClearFiltersButton()
                .InsertFirstName("FN_")
                .ClickSearchButton()
                .ValidateNotificationMessageVisibility(false)
                .ValidateNotificationHideLinkVisibility(false);

            personSearchPage
                .WaitForResultsAreaToLoad()
                .ClickTableHeaderCellLink(3) //order the results by person id ascending
                .ClickTableHeaderCellLink(3) //2nd click to order the results by person id descending
                .WaitForResultsAreaToLoad();

            personSearchPage
                .WaitForRecordVisible(_person1ID.ToString())
                .WaitForRecordVisible(_person2ID.ToString());

            #endregion

            #region Step 6

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickClearFiltersButton()
                .InsertMiddleName("MD_")
                .ClickSearchButton()
                .ValidateNotificationMessageVisibility(false)
                .ValidateNotificationHideLinkVisibility(false);

            personSearchPage
                .WaitForResultsAreaToLoad()
                .ClickTableHeaderCellLink(3) //order the results by person id ascending
                .ClickTableHeaderCellLink(3) //2nd click to order the results by person id descending
                .WaitForResultsAreaToLoad();

            personSearchPage
                .WaitForRecordVisible(_person1ID.ToString());

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickClearFiltersButton()
                .InsertLastName("LN_")
                .ClickSearchButton()
                .ValidateNotificationMessageVisibility(false)
                .ValidateNotificationHideLinkVisibility(false);

            personSearchPage
                .WaitForResultsAreaToLoad()
                .ClickTableHeaderCellLink(3) //order the results by person id ascending
                .ClickTableHeaderCellLink(3) //2nd click to order the results by person id descending
                .WaitForResultsAreaToLoad();

            personSearchPage
                .WaitForRecordVisible(_person1ID.ToString());

            #endregion

            #region Step 7

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickClearFiltersButton()
                .InsertMiddleName("MD_")
                .ClickSearchButton()
                .ValidateNotificationMessageVisibility(false)
                .ValidateNotificationHideLinkVisibility(false);

            personSearchPage
                .WaitForResultsAreaToLoad()
                .ClickTableHeaderCellLink(3) //order the results by person id ascending
                .ClickTableHeaderCellLink(3) //2nd click to order the results by person id descending
                .WaitForResultsAreaToLoad();

            personSearchPage
                .WaitForRecordVisible(_person1ID.ToString());

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickClearFiltersButton()
                .InsertLastName("LN_")
                .ClickSearchButton()
                .ValidateNotificationMessageVisibility(false)
                .ValidateNotificationHideLinkVisibility(false);

            personSearchPage
                .WaitForResultsAreaToLoad()
                .ClickTableHeaderCellLink(3) //order the results by person id ascending
                .ClickTableHeaderCellLink(3) //2nd click to order the results by person id descending
                .WaitForResultsAreaToLoad();

            personSearchPage
                .WaitForRecordVisible(_person1ID.ToString());

            #endregion

            #region Step 7

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            /*This step is no longer valid. The application requirements changed.
             Now the minimum number of characters for the search is 1*/

            //personSearchPage
            //    .WaitForPersonSearchPageToLoad()
            //    .InsertFirstName("FN")
            //    .ClickSearchButton()

            //    .ValidateNotificationMessageVisibility(true)
            //    .ValidateNotificationHideLinkVisibility(true)
            //    .ValidateNotificationMessageText("Please supply at least 3 characters in one of your search values.")
            //    .ClickNotificationHideLink()
            //    .ValidateNotificationMessageVisibility(false)
            //    .ValidateNotificationHideLinkVisibility(false);

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickClearFiltersButton()
                .InsertMiddleName("MD_")
                .ClickSearchButton()
                .ValidateNotificationMessageVisibility(false)
                .ValidateNotificationHideLinkVisibility(false);

            personSearchPage
                .WaitForResultsAreaToLoad()
                .ClickTableHeaderCellLink(3) //order the results by person id ascending
                .ClickTableHeaderCellLink(3) //2nd click to order the results by person id descending
                .WaitForResultsAreaToLoad();

            personSearchPage
                .WaitForRecordVisible(_person1ID.ToString());

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickClearFiltersButton()
                .InsertLastName("LN_")
                .ClickSearchButton()
                .ValidateNotificationMessageVisibility(false)
                .ValidateNotificationHideLinkVisibility(false);

            personSearchPage
                .WaitForResultsAreaToLoad()
                .ClickTableHeaderCellLink(3) //order the results by person id ascending
                .ClickTableHeaderCellLink(3) //2nd click to order the results by person id descending
                .WaitForResultsAreaToLoad();

            personSearchPage
                .WaitForRecordVisible(_person1ID.ToString());

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-19135

        [TestProperty("JiraIssueID", "ACC-3574")]
        [Description("1. Login to CD as a user who has permission for viewing and searching person and click on Person search button." +
            "Validate that there is a new filter in person search - 'Include Inactive ?' and it is not checked by default." +
            "2. Select the Include Inactive checkbox and search." +
            "Validate that the search results will display the inactive records." +
            "3. Uncheck the checkbox and search." +
            "Validate that the search results does not display inactive records.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("IntegrationTestLevel3_SocialCare_AWS"), TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Person Search")]
        public void PersonSearch_UITestMethod003()
        {

            #region Person 1
            string currentDate1 = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string partialDate = DateTime.Now.ToString("yyyyMMddHH");

            string personFirstName1 = "FN_" + currentDate1;
            string personMiddleName1 = "MD_" + currentDate1;
            string personLastName1 = "LN_" + currentDate1;
            string preferredName1 = "PN_" + currentDate1;
            Guid _person1ID = dbHelper.person.CreatePersonRecord("", personFirstName1, personMiddleName1, personLastName1, preferredName1, new DateTime(2000, 1, 1), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion

            #region Person 2
            string currentDate2 = DateTime.Now.ToString("yyyyMMddHHmmssffff");

            string personFirstName2 = "FN_" + currentDate2;
            string personMiddleName2 = "";
            string personLastName2 = "LN_" + currentDate2;
            string preferredName2 = "FN_" + currentDate2;
            Guid _person2ID = dbHelper.person.CreatePersonRecord("", personFirstName2, personMiddleName2, personLastName2, preferredName2, new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_AutomationTestUser1_SystemUserName, "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPersonSearchButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .ClickSearchButton()
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationHideLinkVisibility(true)
                .ValidateNotificationMessageText("At least one search value must be specified")
                .ClickNotificationHideLink();

            #endregion

            #region Step 3

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("FN_" + partialDate + "*")
                .ClickSearchButton()
                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_person1ID.ToString())
                .WaitForRecordVisible(_person2ID.ToString())
                .ValidateRecordCellText(_person1ID.ToString(), "Inactive", "No")
                .ValidateRecordCellText(_person2ID.ToString(), "Inactive", "No");

            #endregion

            #region Step 4

            personSearchPage
                .ValidateIncludeInactiveFilterVisible(true);

            #endregion

            #region Step 5
            dbHelper.person.UpdateInactiveStatus(_person2ID, true);
            personSearchPage
                .ClickIncludeInactiveCheckbox()
                .InsertFirstName("")
                .InsertLastName("LN_" + partialDate + "*")
                .ClickSearchButton()
                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_person1ID.ToString())
                .WaitForRecordVisible(_person2ID.ToString())
                .ValidateRecordCellText(_person1ID.ToString(), "Inactive", "No")
                .ValidateRecordCellText(_person2ID.ToString(), "Inactive", "Yes");

            personSearchPage
                .InsertFirstName("")
                .InsertLastName(personLastName2)
                .ClickSearchButton()
                .WaitForResultsAreaToLoad()
                .WaitForRecordVisible(_person2ID.ToString())
                .ValidateRecordCellText(_person2ID.ToString(), "Inactive", "Yes")
                .ValidateRecordNotVisible(_person1ID.ToString());

            #endregion

            #region Step 6

            personSearchPage
                .InsertFirstName("")
                .ClickIncludeInactiveCheckbox()
                .InsertLastName(personLastName2)
                .ClickSearchButton()
                .ValidateRecordNotVisible(_person2ID.ToString());

            #endregion         

        }

        #endregion


        private void CreateAuditAdminTestUser()
        {
            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            _AuditSearchUserAdmin_UserName = "AuditSearchUserAdmin_" + currentDate;
            _AuditSearchUserAdmin_FullName = "Audit Search User Admin " + currentDate;

            _AuditSearchUserAdmin_SystemUserId = dbHelper.systemUser.CreateSystemUser(_AuditSearchUserAdmin_UserName, "Audit Search User Admin", currentDate, _AuditSearchUserAdmin_FullName, "Passw0rd_!", _AuditSearchUserAdmin_UserName + "@somemail.com", _AuditSearchUserAdmin_UserName + "@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date);

            var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
            var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_AuditSearchUserAdmin_SystemUserId, systemAdministratorSecurityProfileId);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_AuditSearchUserAdmin_SystemUserId, systemUserSecureFieldsSecurityProfileId);

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_AuditSearchUserAdmin_SystemUserId, DateTime.Now.Date);
        }

    }
}
