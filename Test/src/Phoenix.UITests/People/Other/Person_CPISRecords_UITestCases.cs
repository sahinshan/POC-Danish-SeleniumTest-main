using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for the Test CPIS Bulk upload tool https://advancedcsg.atlassian.net/browse/CDV6-4091
    /// 
    /// WARNING: before running these tests it is necessary to manually do 3 operations
    ///     1: Remove all CP-IS records (open person record - Other Information - CP-IS Records) for the following people on QA: 504828 c 504831 504832 504838 504839 504840 504841 504936 505043 505044 505045 505046 505047 505048 505049
    ///     2: Update the CPIS record for the person 504834. The scenario covered with this person record is the one that tries to validate if the tool can update an existing CPIS with the "New" status. it is enough to update the start date field value
    ///     3: Manually run the CPIS bulk upload tool
    ///     
    /// After the 3 steps above are completed run all test methods in this class to check the new generated CPIS records
    /// 
    /// </summary>
    [TestClass]
    public class Person_CPISRecords_UITestCases : FunctionalTest
    {

        #region Properties

        private string _tenantName;
        private Guid _defaultBusinessUnitId;
        private Guid _defaultTeamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _languageId;

        #endregion

        [TestInitialize()]
        public void TestClassInitializationMethod()
        {
            #region Default user

            _tenantName = ConfigurationManager.AppSettings["TenantName"];
            string username = ConfigurationManager.AppSettings["Username"];
            string DataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

            if (DataEncoded.Equals("true"))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(username);
                username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

            var userId = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(userId, DateTime.Now.Date);

            #endregion

            #region Authentication Provider

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

            #endregion

            #region Product Language

            if (!dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any())
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion

            #region Business Unit

            if (!dbHelper.businessUnit.GetByName("CareDirector BU 1").Any())
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector BU 1");
            _defaultBusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector BU 1")[0];

            #endregion

            #region Default Team

            if (!dbHelper.team.GetTeamIdByName("CareDirector T 1").Any())
                dbHelper.team.CreateTeam("CareDirector T 1", null, _defaultBusinessUnitId, "907678", "CareDirectorT1@careworkstempmail.com", "CareDirector T 1", "020 123456");
            _defaultTeamId = dbHelper.team.GetTeamIdByName("CareDirector T 1")[0];

            #endregion

            #region Ethnicity

            if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                dbHelper.ethnicity.CreateEthnicity(_defaultTeamId, "Irish", new DateTime(2020, 1, 1));
            _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

            #endregion

            #region System User

            commonMethodsDB.CreateSystemUserRecord("CW_Forms_Test_User_1", "CW_Forms", "Test_User_1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #endregion

        }

        #region Test CPIS Bulk upload tool https://advancedcsg.atlassian.net/browse/CDV6-4091

        #region Child Protection Record

        [TestProperty("JiraIssueID", "CDV6-4144")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Person A has a child protection record (DOB and Age <> Unborn; CP-IS record with Status = New is not associated with the Parent Person record) - " +
            "Bulk update tool should create a CP-IS record setting the following values: Record Type = 'Child Protection'; Record Id = Id of CP record; Start Date = Start Date of Child Protection record ; Status = 'New - " +
            "Open Person Record -> Navigate to The CPIS page - Open the existing CPIS Record - Validate the record content")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod01()
        {
            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId);
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            var childProtectionCategoryOfAbuseId = commonMethodsDB.CreateChildProtectionCategoryOfAbuse("Emotional abuse", _defaultTeamId, "4", "EMO", DateTime.Now.Date);
            var childProtectionStatusTypeId = commonMethodsDB.CreateChildProtectionStatusType("Defaut 1", _defaultTeamId, DateTime.Now.Date, false, false, false, false);
            var ChildProtectionDate = DateTime.Now.AddDays(-2).Date;
            dbHelper.ChildProtection.CreateChildProtection(_defaultTeamId, caseID, caseTitle, personId, childProtectionCategoryOfAbuseId, childProtectionStatusTypeId, ChildProtectionDate, ChildProtectionDate);

            var cpisRecordID = dbHelper.cpis.GetCPISByPersonID(personId)[0];

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecordID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("Child Protection")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Sent")

                .ValidateStarDateFieldText(ChildProtectionDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText("")
                .ValidateMotherOfUnbornChildFieldText("")

                .ValidateMessageFieldText("");
        }

        [TestProperty("JiraIssueID", "CDV6-4145")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Person A has child protection record (DOB and Age = Unborn; Has a Relationship with the reciprocal relationship = Mother; Expected Date of Birth <> Null) - " +
            "System creates a CP-IS record setting the following values: Record Type = 'Child Protection Unborn'; Start Date = Start Date of Child Protection record; End Date = 'Expected Date Of Birth'; Mother of Unborn Child = person identified in Relationship and Status = 'New' - " +
            "Open Person Record -> Navigate to The CPIS page - Open the existing CPIS Record - Validate the record content")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod02()
        {
            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId);
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");
            dbHelper.person.UpdateDOBAndAgeTypeId(personId, 2, null, null, new DateTime(2021, 7, 31)); //unborne

            firstName = "Mary";
            var motherPersonId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId);
            dbHelper.person.UpdateNHSNumber(motherPersonId, "987 654 3210");

            var motherRelationshipTyeId = dbHelper.personRelationshipType.GetByName("Mother").FirstOrDefault();
            var childRelationshipTyeId = dbHelper.personRelationshipType.GetByName("Child").FirstOrDefault();
            dbHelper.personRelationship.CreatePersonRelationship(_defaultTeamId, personId, "Jhon", childRelationshipTyeId, "Child", motherPersonId, "Mary", motherRelationshipTyeId, "Mother", DateTime.Now.Date, "", 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 1, false);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            var childProtectionCategoryOfAbuseId = commonMethodsDB.CreateChildProtectionCategoryOfAbuse("Emotional abuse", _defaultTeamId, "4", "EMO", DateTime.Now.Date);
            var childProtectionStatusTypeId = commonMethodsDB.CreateChildProtectionStatusType("Defaut 1", _defaultTeamId, DateTime.Now.Date, false, false, false, false);
            var ChildProtectionDate = new DateTime(2020, 7, 4);
            dbHelper.ChildProtection.CreateChildProtection(_defaultTeamId, caseID, caseTitle, personId, childProtectionCategoryOfAbuseId, childProtectionStatusTypeId, ChildProtectionDate, ChildProtectionDate);

            var cpisRecordID = dbHelper.cpis.GetCPISByPersonID(personId)[0];

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecordID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("Child Protection (Unborn)")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Sent")

                .ValidateStarDateFieldText("04/07/2020")
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText("31/07/2021")
                .ValidateMotherOfUnbornChildFieldText("Mary " + lastName)

                .ValidateMessageFieldText("");


        }

        [TestProperty("JiraIssueID", "CDV6-4146")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Person A has child protection record (DOB and Age = Unborn; DO NOT HAS Relationship with the reciprocal relationship = Mother; Expected Date of Birth <> Null) - " +
            "System creates a CP-IS record setting the following values: Record Type = 'Child Protection Unborn'; Start Date = Start Date of Child Protection record; End Date = 'Expected Date of Birth'; Mother of Unborn Child = Null; Status = 'Upload Error' - " +
            "Open Person Record -> Navigate to The CPIS page - Open the existing CPIS Record - Validate the record content")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod03()
        {
            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId);
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");
            dbHelper.person.UpdateDOBAndAgeTypeId(personId, 2, null, null, new DateTime(2021, 7, 31)); //unborne

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            var childProtectionCategoryOfAbuseId = commonMethodsDB.CreateChildProtectionCategoryOfAbuse("Emotional abuse", _defaultTeamId, "4", "EMO", DateTime.Now.Date);
            var childProtectionStatusTypeId = commonMethodsDB.CreateChildProtectionStatusType("Defaut 1", _defaultTeamId, DateTime.Now.Date, false, false, false, false);
            var ChildProtectionDate = new DateTime(2020, 4, 1);
            dbHelper.ChildProtection.CreateChildProtection(_defaultTeamId, caseID, caseTitle, personId, childProtectionCategoryOfAbuseId, childProtectionStatusTypeId, ChildProtectionDate, ChildProtectionDate);

            var cpisRecordID = dbHelper.cpis.GetCPISByPersonID(personId)[0];

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecordID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("Child Protection (Unborn)")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Upload Error")

                .ValidateStarDateFieldText("01/04/2020")
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText("31/07/2021")
                .ValidateMotherOfUnbornChildFieldText("")

                .ValidateMessageFieldText("Unborn child must have a mother.");


        }

        [TestProperty("JiraIssueID", "CDV6-4147")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Person A has child protection record (DOB and Age = Unborn; Has a Relationship with the reciprocal relationship = Mother; Expected Date of Birth == Null - " +
            "System creates a CP-IS record setting the following values: Record Type = 'Child Protection Unborn'; Start Date = Start Date of Child Protection record; End Date = Null; Mother of Unborn Child = person identified in step 2a2; Status = 'Upload Error'  - " +
            "Open Person Record -> Navigate to The CPIS page - Open the existing CPIS Record - Validate the record content")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod04()
        {
            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId);
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");
            dbHelper.person.UpdateDOBAndAgeTypeId(personId, 2, null, null, null); //unborne

            firstName = "Mary";
            var motherPersonId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId);
            dbHelper.person.UpdateNHSNumber(motherPersonId, "987 654 3210");

            var motherRelationshipTyeId = dbHelper.personRelationshipType.GetByName("Mother").FirstOrDefault();
            var childRelationshipTyeId = dbHelper.personRelationshipType.GetByName("Child").FirstOrDefault();
            dbHelper.personRelationship.CreatePersonRelationship(_defaultTeamId, personId, "Jhon", childRelationshipTyeId, "Child", motherPersonId, "Mary", motherRelationshipTyeId, "Mother", DateTime.Now.Date, "", 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 1, false);

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            var childProtectionCategoryOfAbuseId = commonMethodsDB.CreateChildProtectionCategoryOfAbuse("Emotional abuse", _defaultTeamId, "4", "EMO", DateTime.Now.Date);
            var childProtectionStatusTypeId = commonMethodsDB.CreateChildProtectionStatusType("Defaut 1", _defaultTeamId, DateTime.Now.Date, false, false, false, false);
            var ChildProtectionDate = new DateTime(2020, 7, 4);
            dbHelper.ChildProtection.CreateChildProtection(_defaultTeamId, caseID, caseTitle, personId, childProtectionCategoryOfAbuseId, childProtectionStatusTypeId, ChildProtectionDate, ChildProtectionDate);

            var cpisRecordID = dbHelper.cpis.GetCPISByPersonID(personId)[0];

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecordID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("Child Protection (Unborn)")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Upload Error")

                .ValidateStarDateFieldText("04/07/2020")
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText("")
                .ValidateMotherOfUnbornChildFieldText("Mary " + lastName)

                .ValidateMessageFieldText("Expected birth date of child must be populated.");

        }

        [TestProperty("JiraIssueID", "CDV6-4148")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "CPIS record with 'New' status already exists before running the upload tool - " +
            "Person A has child protection record (DOB and Age <> Unborn; CP-IS record with Status = New is associated with the Parent Person record) - " +
            "System changes CP-IS record and sets the following values: Record Type = 'Child Protection'; Record Id = Id of CP record; Start Date = Start Date of Child Protection record ; Status = 'New'. - " +
            "Open Person Record -> Navigate to The CPIS page - Open the existing CPIS Record - Validate the record content")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod05()
        {
            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId);
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            var childProtectionCategoryOfAbuseId = commonMethodsDB.CreateChildProtectionCategoryOfAbuse("Emotional abuse", _defaultTeamId, "4", "EMO", DateTime.Now.Date);
            var childProtectionStatusTypeId = commonMethodsDB.CreateChildProtectionStatusType("Defaut 1", _defaultTeamId, DateTime.Now.Date, false, false, false, false);
            var ChildProtectionDate = DateTime.Now.AddDays(-2).Date;
            var childProtectionId = dbHelper.ChildProtection.CreateChildProtection(_defaultTeamId, caseID, caseTitle, personId, childProtectionCategoryOfAbuseId, childProtectionStatusTypeId, ChildProtectionDate, ChildProtectionDate);

            var cpisRecordID = dbHelper.cpis.GetCPISByPersonID(personId)[0];

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(1000);

            ChildProtectionDate = DateTime.Now.Date;
            dbHelper.ChildProtection.UpdateStartDate(childProtectionId, ChildProtectionDate);

            var allCPISRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(2, allCPISRecords.Count);
            var cpisRecord2ID = allCPISRecords.Where(c => c != cpisRecordID).FirstOrDefault();

            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecord2ID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("Child Protection")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Sent")

                .ValidateStarDateFieldText(ChildProtectionDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText("")
                .ValidateMotherOfUnbornChildFieldText("")

                .ValidateMessageFieldText("");

        }

        #endregion

        #region LAC Record

        [TestProperty("JiraIssueID", "CDV6-4149")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Use Case Scenario 1 - (Non-Qualifying > Qualifying > End) - " +
            "Person A has a Valid NHS Number; LAC Episode; LAC Record (Reason with Applicable to started to be looked after = Yes; Legal Status with Status Type = Non-CPIS Qualifying Type) - " +
            "CP-IS record should not be created. - " +
            "Open Person Record -> Navigate to The CPIS page - Validate that no CPIS record is present")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod06()
        {
            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #region Person

            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId);
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            #endregion

            #region Case

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            #endregion

            #region Person LAC Legal Status

            var lacLegalStatusReasonId = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "S - Started to be looked after", "", "S", DateTime.Now.Date, true, false);
            var lacLegalStatusId = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "L1-Invalid-Non Qualifying", "10", "S", DateTime.Now.Date, 2);
            var lacPlacementId = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);
            var lacLocationCodeId = commonMethodsDB.CreateLACLocationCode(_defaultTeamId, "LAC Location Code 1", "", "", DateTime.Now.Date);
            var lacPlacementProviderId = commonMethodsDB.CreateLACPlacementProvider(_defaultTeamId, "Parent(s) or other person(s) with parental responsibility", "", "PR0", DateTime.Now.Date);

            var lacEpisodeId = dbHelper.LACEpisode.CreateLACEpisode(_defaultTeamId, caseID, caseTitle, personId, DateTime.Now.Date);
            var personLACLegalStatusId = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisodeId, lacLegalStatusReasonId, lacLegalStatusId, lacPlacementId, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date);

            #endregion

            #region CPIS

            var cpisRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(0, cpisRecords.Count);

            #endregion

            #region Run the "CPIS - Generate Export File" schedule job

            //var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            //this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            //this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .ValidateNoRecordsMessageVisible();
        }

        [TestProperty("JiraIssueID", "CDV6-4150")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Use Case Scenario 1 - (Non-Qualifying > Qualifying > End) - " +
            "Person A has a Valid NHS Number; LAC Episode; LAC Record (Reason with Applicable to started to be looked after = Yes; Legal Status with Status Type = Non-CPIS Qualifying Type; End Reason with Period of Care continues under subsequent LAC record' = No) - " +
            "CP-IS record should not be created. - " +
            "Open Person Record -> Navigate to The CPIS page - Validate that no CPIS record is present")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod07()
        {
            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #region Person

            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId, DateTime.Now.Date.AddYears(-2));
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            #endregion

            #region Case

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            #endregion

            #region Person LAC Legal Status

            var lacLegalStatusReasonId = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "S - Started to be looked after", "", "S", DateTime.Now.Date, true, false);
            var lacLegalStatusId = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "L1-Invalid-Non Qualifying", "10", "S", DateTime.Now.Date, 2);
            var lacPlacementId = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);
            var lacLocationCodeId = commonMethodsDB.CreateLACLocationCode(_defaultTeamId, "LAC Location Code 1", "", "", DateTime.Now.Date);
            var lacPlacementProviderId = commonMethodsDB.CreateLACPlacementProvider(_defaultTeamId, "Parent(s) or other person(s) with parental responsibility", "", "PR0", DateTime.Now.Date);
            var lacLegalStatusEndReasonId = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Sentenced to custody", "", "E9", DateTime.Now.Date, false, false);

            var lacEpisodeId = dbHelper.LACEpisode.CreateLACEpisode(_defaultTeamId, caseID, caseTitle, personId, DateTime.Now.Date.AddDays(-2));
            var personLACLegalStatusId = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisodeId, lacLegalStatusReasonId, lacLegalStatusId, lacPlacementId, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-2));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatusId, DateTime.Now.Date, lacLegalStatusEndReasonId);

            #endregion

            #region CPIS

            var cpisRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(0, cpisRecords.Count);

            #endregion

            #region Run the "CPIS - Generate Export File" schedule job

            //var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            //this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            //this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .ValidateNoRecordsMessageVisible();
        }

        [TestProperty("JiraIssueID", "CDV6-4151")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Use Case Scenario 1 - (Non-Qualifying > Qualifying > End) - " +
            "Person A has a Valid NHS Number; LAC Episode; LAC Record (Reason with Applicable to started to be looked after = Yes; Legal Status with Status Type = Non-CPIS Qualifying Type; Period of Care continues under subsequent LAC record' = Yes) - " +
            "CP-IS record should not be created. - " +
            "Open Person Record -> Navigate to The CPIS page - Validate that no CPIS record is present")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod08()
        {
            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #region Person

            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId, DateTime.Now.Date.AddYears(-2));
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            #endregion

            #region Case

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            #endregion

            #region Person LAC Legal Status

            var lacLegalStatusReasonId = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "S - Started to be looked after", "", "S", DateTime.Now.Date, true, false);
            var lacLegalStatusId = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "L1-Invalid-Non Qualifying", "10", "S", DateTime.Now.Date, 2);
            var lacPlacementId = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);
            var lacLocationCodeId = commonMethodsDB.CreateLACLocationCode(_defaultTeamId, "LAC Location Code 1", "", "", DateTime.Now.Date);
            var lacPlacementProviderId = commonMethodsDB.CreateLACPlacementProvider(_defaultTeamId, "Parent(s) or other person(s) with parental responsibility", "", "PR0", DateTime.Now.Date);
            var lacLegalStatusEndReasonId = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Sentenced to custody", "", "E9", DateTime.Now.Date, false, false);

            var lacEpisodeId = dbHelper.LACEpisode.CreateLACEpisode(_defaultTeamId, caseID, caseTitle, personId, DateTime.Now.Date.AddDays(-2));
            var personLACLegalStatusId = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisodeId, lacLegalStatusReasonId, lacLegalStatusId, lacPlacementId, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-2));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatusId, DateTime.Now.Date, lacLegalStatusEndReasonId);

            #endregion

            #region CPIS

            var cpisRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(0, cpisRecords.Count);

            #endregion

            #region Run the "CPIS - Generate Export File" schedule job

            //var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            //this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            //this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .ValidateNoRecordsMessageVisible();
        }

        [TestProperty("JiraIssueID", "CDV6-4152")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Use Case Scenario 1 - (Non-Qualifying > Qualifying > End) - " +
            "Person A has a Valid NHS Number; LAC Episode; LAC Record (Reason with Valid for continuing episode of care = Yes; Status Type = CPIS Qualifying Type)  - " +
            "CP-IS record should be created. - " +
            "Open Person Record -> Navigate to The CPIS page - Open the CPIS record - Validate the record fields data")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod09()
        {
            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #region Person

            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId, DateTime.Now.Date.AddYears(-2));
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            #endregion

            #region Case

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            #endregion

            #region Person LAC Legal Status

            var lacLegalStatusReasonId = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "S - Started to be looked after", "", "S", DateTime.Now.Date, true, false);
            var lacLegalStatusId = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "L1-Invalid-Non Qualifying", "10", "S", DateTime.Now.Date, 2);
            var lacPlacementId = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);
            var lacLocationCodeId = commonMethodsDB.CreateLACLocationCode(_defaultTeamId, "LAC Location Code 1", "", "", DateTime.Now.Date);
            var lacPlacementProviderId = commonMethodsDB.CreateLACPlacementProvider(_defaultTeamId, "Parent(s) or other person(s) with parental responsibility", "", "PR0", DateTime.Now.Date);
            var lacLegalStatusEndReasonId = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 1", "", "X1", DateTime.Now.Date, false, true);

            var lacEpisodeId = dbHelper.LACEpisode.CreateLACEpisode(_defaultTeamId, caseID, caseTitle, personId, DateTime.Now.Date.AddDays(-2));
            var personLACLegalStatusId = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisodeId, lacLegalStatusReasonId, lacLegalStatusId, lacPlacementId, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-2));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatusId, DateTime.Now.Date, lacLegalStatusEndReasonId);

            #endregion

            #region CPIS

            var cpisRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(0, cpisRecords.Count);

            #endregion

            #region Run the "CPIS - Generate Export File" schedule job

            //var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            //this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            //this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .ValidateNoRecordsMessageVisible();
        }

        [TestProperty("JiraIssueID", "CDV6-4153")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Use Case Scenario 1 - (Non-Qualifying > Qualifying > End) - " +
            "Person A has a Valid NHS Number; LAC Episode; LAC Record (Reason with Valid for continuing episode of care = Yes; Status Type = CPIS Qualifying Type; End Reason with 'Applicable to Ceased to Be Looked After' = Yes)   - " +
            "CP-IS record should be created (setting End Date = End Date of LAC). - " +
            "Open Person Record -> Navigate to The CPIS page - Open the CPIS record - Validate the record fields data")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod10()
        {
            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #region Person

            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId, DateTime.Now.Date.AddYears(-2));
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            #endregion

            #region Case

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            #endregion

            #region Person LAC Legal Status 1

            var lacLegalStatusReason1Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "S - Started to be looked after", "", "S", DateTime.Now.Date, true, false);
            var lacLegalStatus1Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "L1-Invalid-Non Qualifying", "10", "S", DateTime.Now.Date, 2);
            var lacPlacement1Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);
            var lacLocationCodeId = commonMethodsDB.CreateLACLocationCode(_defaultTeamId, "LAC Location Code 1", "", "", DateTime.Now.Date);
            var lacPlacementProviderId = commonMethodsDB.CreateLACPlacementProvider(_defaultTeamId, "Parent(s) or other person(s) with parental responsibility", "", "PR0", DateTime.Now.Date);
            var lacLegalStatusEndReasonId = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 1", "", "X1", DateTime.Now.Date, false, true);

            var lacEpisode1Id = dbHelper.LACEpisode.CreateLACEpisode(_defaultTeamId, caseID, caseTitle, personId, DateTime.Now.Date.AddDays(-10));
            var personLACLegalStatus1Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason1Id, lacLegalStatus1Id, lacPlacement1Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-10));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus1Id, DateTime.Now.Date.AddDays(-2), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 2

            var lacLegalStatusReason2Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "Status Reason-B-Valid for Continuing Episode of Care", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus2Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C1-Valid-Qualifying", "", "C1", DateTime.Now.Date, 1);
            var lacPlacement2Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Secure Children’s Homes", "", "K1", DateTime.Now.Date);

            var personLACLegalStatusId = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason2Id, lacLegalStatus2Id, lacPlacement2Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-2));

            #endregion

            #region CPIS

            var cpisRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(1, cpisRecords.Count);
            var cpisRecordID = cpisRecords[0];

            #endregion

            #region Run the "CPIS - Generate Export File" schedule job

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecordID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("LAC")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Sent")

                .ValidateStarDateFieldText(DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy"))
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText("")
                .ValidateMotherOfUnbornChildFieldText("")

                .ValidateMessageFieldText("");
        }

        [TestProperty("JiraIssueID", "CDV6-4154")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Use Case Scenario 2 - (Non-Qualifying > Qualifying > Qualifying > End) (LAC records created as defined in the use case and before running the bulk tool) - " +
            "Open Person Record -> Navigate to The CPIS page - Open the CPIS record - Validate the record fields data")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod11()
        {
            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #region Person

            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId, DateTime.Now.Date.AddYears(-2));
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            #endregion

            #region Case

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            #endregion

            #region Person LAC Legal Status 1

            var lacLegalStatusReason1Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "S - Started to be looked after", "", "S", DateTime.Now.Date, true, false);
            var lacLegalStatus1Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "L1-Invalid-Non Qualifying", "10", "S", DateTime.Now.Date, 2);
            var lacPlacement1Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);
            var lacLocationCodeId = commonMethodsDB.CreateLACLocationCode(_defaultTeamId, "LAC Location Code 1", "", "", DateTime.Now.Date);
            var lacPlacementProviderId = commonMethodsDB.CreateLACPlacementProvider(_defaultTeamId, "Parent(s) or other person(s) with parental responsibility", "", "PR0", DateTime.Now.Date);
            var lacLegalStatusEndReasonId = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 1", "", "X1", DateTime.Now.Date, false, true);

            var lacEpisode1Id = dbHelper.LACEpisode.CreateLACEpisode(_defaultTeamId, caseID, caseTitle, personId, DateTime.Now.Date.AddDays(-10));
            var personLACLegalStatus1Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason1Id, lacLegalStatus1Id, lacPlacement1Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-10));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus1Id, DateTime.Now.Date.AddDays(-8), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 2

            var lacLegalStatusReason2Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "Status Reason-B-Valid for Continuing Episode of Care", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus2Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C1-Valid-Qualifying", "", "C1", DateTime.Now.Date, 1);
            var lacPlacement2Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Secure Children’s Homes", "", "K1", DateTime.Now.Date);

            var personLACLegalStatus2Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason2Id, lacLegalStatus2Id, lacPlacement2Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-8));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus2Id, DateTime.Now.Date.AddDays(-6), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 3

            var lacLegalStatusReason3Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "B - Test Reason", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus3Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C2-Valid-Qualifying", "", "C2", DateTime.Now.Date, 1);
            var lacPlacement3Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Foster placement with relative(s) or friend(s) – long term fostering", "", "K1", DateTime.Now.Date);
            var lacLegalStatusEndReason3Id = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 3", "", "X3", DateTime.Now.Date, true, true);

            var personLACLegalStatus3Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason3Id, lacLegalStatus3Id, lacPlacement3Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-6));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus3Id, DateTime.Now.Date.AddDays(-4), lacLegalStatusEndReason3Id);

            #endregion

            #region CPIS

            var cpisRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(1, cpisRecords.Count);
            var cpisRecordID = cpisRecords[0];

            #endregion

            #region Run the "CPIS - Generate Export File" schedule job

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecordID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("LAC")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Sent")

                .ValidateStarDateFieldText(DateTime.Now.Date.AddDays(-6).ToString("dd/MM/yyyy"))
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText(DateTime.Now.Date.AddDays(-4).ToString("dd/MM/yyyy"))
                .ValidateMotherOfUnbornChildFieldText("")

                .ValidateMessageFieldText("");
        }

        [TestProperty("JiraIssueID", "CDV6-4155")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Use Case Scenario 3 - (Non-Qualifying > Qualifying > Non-Qualifying > End) (LAC records created as defined in the use case and before running the bulk tool) - " +
            "Open Person Record -> Navigate to The CPIS page - Open the CPIS record - Validate the record fields data")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod12()
        {
            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #region Person

            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId, DateTime.Now.Date.AddYears(-2));
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            #endregion

            #region Case

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            #endregion

            #region Person LAC Legal Status 1

            var lacLegalStatusReason1Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "S - Started to be looked after", "", "S", DateTime.Now.Date, true, false);
            var lacLegalStatus1Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "L1-Invalid-Non Qualifying", "10", "S", DateTime.Now.Date, 2);
            var lacPlacement1Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);
            var lacLocationCodeId = commonMethodsDB.CreateLACLocationCode(_defaultTeamId, "LAC Location Code 1", "", "", DateTime.Now.Date);
            var lacPlacementProviderId = commonMethodsDB.CreateLACPlacementProvider(_defaultTeamId, "Parent(s) or other person(s) with parental responsibility", "", "PR0", DateTime.Now.Date);
            var lacLegalStatusEndReasonId = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 1", "", "X1", DateTime.Now.Date, false, true);

            var lacEpisode1Id = dbHelper.LACEpisode.CreateLACEpisode(_defaultTeamId, caseID, caseTitle, personId, DateTime.Now.Date.AddDays(-10));
            var personLACLegalStatus1Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason1Id, lacLegalStatus1Id, lacPlacement1Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-10));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus1Id, DateTime.Now.Date.AddDays(-8), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 2

            var lacLegalStatusReason2Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "Status Reason-B-Valid for Continuing Episode of Care", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus2Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C1-Valid-Qualifying", "", "C1", DateTime.Now.Date, 1);
            var lacPlacement2Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Secure Children’s Homes", "", "K1", DateTime.Now.Date);

            var personLACLegalStatus2Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason2Id, lacLegalStatus2Id, lacPlacement2Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-8));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus2Id, DateTime.Now.Date.AddDays(-6), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 3

            var lacLegalStatusReason3Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "B - Test Reason", "", "B", DateTime.Now.Date, true, true);
            //var lacLegalStatus3Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C2-Valid-Qualifying", "", "C2", DateTime.Now.Date, 1);
            var lacPlacement3Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Foster placement with relative(s) or friend(s) – long term fostering", "", "K1", DateTime.Now.Date);
            var lacLegalStatusEndReason3Id = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 3", "", "X3", DateTime.Now.Date, true, true);

            var personLACLegalStatus3Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason3Id, lacLegalStatus1Id, lacPlacement3Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-6));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus3Id, DateTime.Now.Date.AddDays(-4), lacLegalStatusEndReason3Id);

            #endregion

            #region CPIS

            var cpisRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(1, cpisRecords.Count);
            var cpisRecordID = cpisRecords[0];

            #endregion

            #region Run the "CPIS - Generate Export File" schedule job

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecordID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("LAC")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Sent")

                .ValidateStarDateFieldText(DateTime.Now.Date.AddDays(-8).ToString("dd/MM/yyyy"))
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText("")
                .ValidateMotherOfUnbornChildFieldText("")

                .ValidateMessageFieldText("");
        }

        [TestProperty("JiraIssueID", "CDV6-4156")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Use Case Scenario 4 - (Qualifying > Qualifying > Qualifying > End) (LAC records created as defined in the use case and before running the bulk tool) - " +
            "Open Person Record -> Navigate to The CPIS page - Open the CPIS record - Validate the record fields data")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod13()
        {
            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #region Person

            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId, DateTime.Now.Date.AddYears(-2));
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            #endregion

            #region Case

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            #endregion

            #region Person LAC Legal Status 1

            var lacLegalStatusReason1Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "S - Started to be looked after", "", "S", DateTime.Now.Date, true, false);
            var lacLegalStatus1Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C1-Valid-Qualifying", "", "C1", DateTime.Now.Date, 1);
            var lacPlacement1Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);
            var lacLocationCodeId = commonMethodsDB.CreateLACLocationCode(_defaultTeamId, "LAC Location Code 1", "", "", DateTime.Now.Date);
            var lacPlacementProviderId = commonMethodsDB.CreateLACPlacementProvider(_defaultTeamId, "Parent(s) or other person(s) with parental responsibility", "", "PR0", DateTime.Now.Date);
            var lacLegalStatusEndReasonId = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 1", "", "X1", DateTime.Now.Date, false, true);

            var lacEpisode1Id = dbHelper.LACEpisode.CreateLACEpisode(_defaultTeamId, caseID, caseTitle, personId, DateTime.Now.Date.AddDays(-10));
            var personLACLegalStatus1Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason1Id, lacLegalStatus1Id, lacPlacement1Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-10));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus1Id, DateTime.Now.Date.AddDays(-8), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 2

            var lacLegalStatusReason2Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "Status Reason-B-Valid for Continuing Episode of Care", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus2Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C9-Valid-Qualifying", "", "C9", DateTime.Now.Date, 1);
            var lacPlacement2Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Secure Children’s Homes", "", "K1", DateTime.Now.Date);

            var personLACLegalStatus2Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason2Id, lacLegalStatus2Id, lacPlacement2Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-8), "CR0 3RL");
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus2Id, DateTime.Now.Date.AddDays(-6), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 3

            var lacLegalStatusReason3Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "B - Test Reason", "", "B", DateTime.Now.Date, true, true);
            var lacPlacement3Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Foster placement with relative(s) or friend(s) – long term fostering", "", "K1", DateTime.Now.Date);
            var lacLegalStatusEndReason3Id = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 3", "", "X3", DateTime.Now.Date, true, true);

            var personLACLegalStatus3Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason3Id, lacLegalStatus1Id, lacPlacement3Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-6));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus3Id, DateTime.Now.Date.AddDays(-4), lacLegalStatusEndReason3Id);

            #endregion

            #region CPIS

            var cpisRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(1, cpisRecords.Count);
            var cpisRecordID = cpisRecords[0];

            #endregion

            #region Run the "CPIS - Generate Export File" schedule job

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecordID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("LAC")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Sent")

                .ValidateStarDateFieldText(DateTime.Now.Date.AddDays(-6).ToString("dd/MM/yyyy"))
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText(DateTime.Now.Date.AddDays(-4).ToString("dd/MM/yyyy"))
                .ValidateMotherOfUnbornChildFieldText("")

                .ValidateMessageFieldText("");
        }

        [TestProperty("JiraIssueID", "CDV6-4157")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Use Case Scenario 5 - (Qualifying > Qualifying > Qualifying > Non-Qualifying > End) (LAC records created as defined in the use case and before running the bulk tool) - " +
            "Open Person Record -> Navigate to The CPIS page - Open the CPIS record - Validate the record fields data")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod14()
        {
            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #region Person

            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId, DateTime.Now.Date.AddYears(-2));
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            #endregion

            #region Case

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            #endregion

            #region Person LAC Legal Status 1

            var lacLegalStatusReason1Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "S - Started to be looked after", "", "S", DateTime.Now.Date, true, false);
            var lacLegalStatus1Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C1-Valid-Qualifying", "", "C1", DateTime.Now.Date, 1);
            var lacPlacement1Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);
            var lacLocationCodeId = commonMethodsDB.CreateLACLocationCode(_defaultTeamId, "LAC Location Code 1", "", "", DateTime.Now.Date);
            var lacPlacementProviderId = commonMethodsDB.CreateLACPlacementProvider(_defaultTeamId, "Parent(s) or other person(s) with parental responsibility", "", "PR0", DateTime.Now.Date);
            var lacLegalStatusEndReasonId = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 1", "", "X1", DateTime.Now.Date, false, true);

            var lacEpisode1Id = dbHelper.LACEpisode.CreateLACEpisode(_defaultTeamId, caseID, caseTitle, personId, DateTime.Now.Date.AddDays(-10));
            var personLACLegalStatus1Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason1Id, lacLegalStatus1Id, lacPlacement1Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-10));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus1Id, DateTime.Now.Date.AddDays(-8), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 2

            var lacLegalStatusReason2Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "Status Reason-B-Valid for Continuing Episode of Care", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus2Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C9-Valid-Qualifying", "", "C9", DateTime.Now.Date, 1);
            var lacPlacement2Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Secure Children’s Homes", "", "K1", DateTime.Now.Date);

            var personLACLegalStatus2Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason2Id, lacLegalStatus2Id, lacPlacement2Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-8), "CR0 3RL");
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus2Id, DateTime.Now.Date.AddDays(-6), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 3

            var lacLegalStatusReason3Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "B - Test Reason", "", "B", DateTime.Now.Date, true, true);
            var lacPlacement3Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Foster placement with relative(s) or friend(s) – long term fostering", "", "K1", DateTime.Now.Date);
            var lacLegalStatusEndReason3Id = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 3", "", "X3", DateTime.Now.Date, true, true);

            var personLACLegalStatus3Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason3Id, lacLegalStatus1Id, lacPlacement3Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-6));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus3Id, DateTime.Now.Date.AddDays(-4), lacLegalStatusEndReason3Id);

            #endregion

            #region Person LAC Legal Status 4

            var lacLegalStatusReason4Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "C - Test Reason", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus4Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C10-Valid-Qualifying", "", "C10", DateTime.Now.Date, 2);

            var personLACLegalStatus4Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason4Id, lacLegalStatus4Id, lacPlacement3Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-4), "CR1 3RL");

            #endregion

            #region CPIS

            var cpisRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(1, cpisRecords.Count);
            var cpisRecordID = cpisRecords[0];

            #endregion

            #region Run the "CPIS - Generate Export File" schedule job

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecordID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("LAC")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Sent")

                .ValidateStarDateFieldText(DateTime.Now.Date.AddDays(-6).ToString("dd/MM/yyyy"))
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText(DateTime.Now.Date.AddDays(-4).ToString("dd/MM/yyyy"))
                .ValidateMotherOfUnbornChildFieldText("")

                .ValidateMessageFieldText("");
        }

        [TestProperty("JiraIssueID", "CDV6-4158")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Use Case Scenario 6 - (Qualifying > Non-Qualifying > Qualifying > Non-Qualifying > End) (LAC records created as defined in the use case and before running the bulk tool) - " +
            "Open Person Record -> Navigate to The CPIS page - Open the CPIS record - Validate the record fields data")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod15()
        {
            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #region Person

            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId, DateTime.Now.Date.AddYears(-2));
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            #endregion

            #region Case

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            #endregion

            #region Person LAC Legal Status 1

            var lacLegalStatusReason1Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "S - Started to be looked after", "", "S", DateTime.Now.Date, true, false);
            var lacLegalStatus1Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C1-Valid-Qualifying", "", "C1", DateTime.Now.Date, 1);
            var lacPlacement1Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);
            var lacLocationCodeId = commonMethodsDB.CreateLACLocationCode(_defaultTeamId, "LAC Location Code 1", "", "", DateTime.Now.Date);
            var lacPlacementProviderId = commonMethodsDB.CreateLACPlacementProvider(_defaultTeamId, "Parent(s) or other person(s) with parental responsibility", "", "PR0", DateTime.Now.Date);
            var lacLegalStatusEndReasonId = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 1", "", "X1", DateTime.Now.Date, false, true);

            var lacEpisode1Id = dbHelper.LACEpisode.CreateLACEpisode(_defaultTeamId, caseID, caseTitle, personId, DateTime.Now.Date.AddDays(-10));
            var personLACLegalStatus1Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason1Id, lacLegalStatus1Id, lacPlacement1Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-10));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus1Id, DateTime.Now.Date.AddDays(-8), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 2

            var lacLegalStatusReason2Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "Status Reason-B-Valid for Continuing Episode of Care", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus2Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C12-Valid-Qualifying", "", "C12", DateTime.Now.Date, 2);
            var lacPlacement2Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Secure Children’s Homes", "", "K1", DateTime.Now.Date);

            var personLACLegalStatus2Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason2Id, lacLegalStatus2Id, lacPlacement2Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-8), "CR0 3RL");
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus2Id, DateTime.Now.Date.AddDays(-6), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 3

            var lacLegalStatusReason3Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "B - Test Reason", "", "B", DateTime.Now.Date, true, true);
            var lacPlacement3Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Foster placement with relative(s) or friend(s) – long term fostering", "", "K1", DateTime.Now.Date);
            var lacLegalStatusEndReason3Id = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 3", "", "X3", DateTime.Now.Date, true, true);

            var personLACLegalStatus3Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason3Id, lacLegalStatus1Id, lacPlacement3Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-6), "CR1 3RL");
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus3Id, DateTime.Now.Date.AddDays(-4), lacLegalStatusEndReason3Id);

            #endregion

            #region Person LAC Legal Status 4

            var lacLegalStatusReason4Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "C - Test Reason", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus4Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C10-Valid-Qualifying", "", "C10", DateTime.Now.Date, 2);

            var personLACLegalStatus4Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason4Id, lacLegalStatus4Id, lacPlacement3Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-4), "CR2 3RL");

            #endregion

            #region CPIS

            var cpisRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(1, cpisRecords.Count);
            var cpisRecordID = cpisRecords[0];

            #endregion

            #region Run the "CPIS - Generate Export File" schedule job

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecordID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("LAC")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Sent")

                .ValidateStarDateFieldText(DateTime.Now.Date.AddDays(-6).ToString("dd/MM/yyyy"))
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText(DateTime.Now.Date.AddDays(-4).ToString("dd/MM/yyyy"))
                .ValidateMotherOfUnbornChildFieldText("")

                .ValidateMessageFieldText("");
        }

        [TestProperty("JiraIssueID", "CDV6-4159")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Use Case Scenario 7 - (Non-Qualifying > Non-Qualifying > Qualifying > Qualifying > End) (LAC records created as defined in the use case and before running the bulk tool) - " +
            "Open Person Record -> Navigate to The CPIS page - Open the CPIS record - Validate the record fields data")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod16()
        {
            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #region Person

            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId, DateTime.Now.Date.AddYears(-2));
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            #endregion

            #region Case

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            #endregion

            #region Person LAC Legal Status 1

            var lacLegalStatusReason1Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "S - Started to be looked after", "", "S", DateTime.Now.Date, true, false);
            var lacLegalStatus1Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C19-Invalid-Non-Qualifying", "", "C19", DateTime.Now.Date, 2);
            var lacPlacement1Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);
            var lacLocationCodeId = commonMethodsDB.CreateLACLocationCode(_defaultTeamId, "LAC Location Code 1", "", "", DateTime.Now.Date);
            var lacPlacementProviderId = commonMethodsDB.CreateLACPlacementProvider(_defaultTeamId, "Parent(s) or other person(s) with parental responsibility", "", "PR0", DateTime.Now.Date);
            var lacLegalStatusEndReasonId = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 1", "", "X1", DateTime.Now.Date, false, true);

            var lacEpisode1Id = dbHelper.LACEpisode.CreateLACEpisode(_defaultTeamId, caseID, caseTitle, personId, DateTime.Now.Date.AddDays(-10));
            var personLACLegalStatus1Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason1Id, lacLegalStatus1Id, lacPlacement1Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-10));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus1Id, DateTime.Now.Date.AddDays(-8), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 2

            var lacLegalStatusReason2Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "Status Reason-B-Valid for Continuing Episode of Care", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus2Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C12-Valid-Qualifying", "", "C12", DateTime.Now.Date, 2);
            var lacPlacement2Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Secure Children’s Homes", "", "K1", DateTime.Now.Date);

            var personLACLegalStatus2Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason2Id, lacLegalStatus2Id, lacPlacement2Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-8), "CR0 3RL");
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus2Id, DateTime.Now.Date.AddDays(-6), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 3

            var lacLegalStatusReason3Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "B - Test Reason", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus3Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C22-Valid-Qualifying", "", "C22", DateTime.Now.Date, 1);
            var lacPlacement3Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Foster placement with relative(s) or friend(s) – long term fostering", "", "K1", DateTime.Now.Date);
            var lacLegalStatusEndReason3Id = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 3", "", "X3", DateTime.Now.Date, true, true);

            var personLACLegalStatus3Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason3Id, lacLegalStatus3Id, lacPlacement3Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-6), "CR1 3RL");
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus3Id, DateTime.Now.Date.AddDays(-4), lacLegalStatusEndReason3Id);

            #endregion

            #region Person LAC Legal Status 4

            var lacLegalStatusReason4Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "C - Test Reason", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus4Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C20-Valid-Qualifying", "", "C20", DateTime.Now.Date, 1);

            var personLACLegalStatus4Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason4Id, lacLegalStatus4Id, lacPlacement3Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-4), "CR2 3RL");

            #endregion

            #region CPIS

            var cpisRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(1, cpisRecords.Count);
            var cpisRecordID = cpisRecords[0];

            #endregion

            #region Run the "CPIS - Generate Export File" schedule job

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecordID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("LAC")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Sent")

                .ValidateStarDateFieldText(DateTime.Now.Date.AddDays(-4).ToString("dd/MM/yyyy"))
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText("")
                .ValidateMotherOfUnbornChildFieldText("")

                .ValidateMessageFieldText("");
        }

        [TestProperty("JiraIssueID", "CDV6-4160")]
        [Description("Linked Issue https://advancedcsg.atlassian.net/browse/CDV6-4091 - " +
            "Use Case Scenario 8 - (Qualifying > Non-Qualifying > Non-Qualifying > Qualifying > End) (LAC records created as defined in the use case and before running the bulk tool) - " +
            "Open Person Record -> Navigate to The CPIS page - Open the CPIS record - Validate the record fields data")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_CPISRecords_UITestMethod17()
        {
            var userid = commonMethodsDB.CreateSystemUserRecord("cpis.user1", "cpis", "user1", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #region Person

            var firstName = "Jhon";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fullName = firstName + " " + lastName;
            var personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _defaultTeamId, DateTime.Now.Date.AddYears(-2));
            int personNumber = (int)dbHelper.person.GetPersonById(personId, "personnumber")["personnumber"];
            dbHelper.person.UpdateNHSNumber(personId, "987 654 3210");

            #endregion

            #region Case

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            var contactReason = commonMethodsDB.CreateContactReasonIfNeeded("Default", _defaultTeamId);
            var dataformid = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            Guid caseID = dbHelper.Case.CreateSocialCareCaseRecord(_defaultTeamId, personId, userid, userid, _caseStatusId, contactReason, dataformid, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            var caseTitle = (string)dbHelper.Case.GetCaseByID(caseID, "title")["title"];

            #endregion

            #region Person LAC Legal Status 1

            var lacLegalStatusReason1Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "S - Started to be looked after", "", "S", DateTime.Now.Date, true, false);
            var lacLegalStatus1Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C29-Qualifying", "", "C29", DateTime.Now.Date, 1);
            var lacPlacement1Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Family and Friends", "", "POF5", DateTime.Now.Date);
            var lacLocationCodeId = commonMethodsDB.CreateLACLocationCode(_defaultTeamId, "LAC Location Code 1", "", "", DateTime.Now.Date);
            var lacPlacementProviderId = commonMethodsDB.CreateLACPlacementProvider(_defaultTeamId, "Parent(s) or other person(s) with parental responsibility", "", "PR0", DateTime.Now.Date);
            var lacLegalStatusEndReasonId = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 1", "", "X1", DateTime.Now.Date, false, true);

            var lacEpisode1Id = dbHelper.LACEpisode.CreateLACEpisode(_defaultTeamId, caseID, caseTitle, personId, DateTime.Now.Date.AddDays(-10));
            var personLACLegalStatus1Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason1Id, lacLegalStatus1Id, lacPlacement1Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-10));
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus1Id, DateTime.Now.Date.AddDays(-8), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 2

            var lacLegalStatusReason2Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "Status Reason-B-Valid for Continuing Episode of Care", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus2Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C22-Not-Valid-Non-Qualifying", "", "C22", DateTime.Now.Date, 2);
            var lacPlacement2Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Secure Children’s Homes", "", "K1", DateTime.Now.Date);

            var personLACLegalStatus2Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason2Id, lacLegalStatus2Id, lacPlacement2Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-8), "CR0 3RL");
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus2Id, DateTime.Now.Date.AddDays(-6), lacLegalStatusEndReasonId);

            #endregion

            #region Person LAC Legal Status 3

            var lacLegalStatusReason3Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "B - Test Reason", "", "B", DateTime.Now.Date, true, true);
            var lacPlacement3Id = commonMethodsDB.CreateLACPlacement(_defaultTeamId, "Foster placement with relative(s) or friend(s) – long term fostering", "", "K1", DateTime.Now.Date);
            var lacLegalStatus3Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C32-Not-Valid-Non-Qualifying", "", "C32", DateTime.Now.Date, 2);
            var lacLegalStatusEndReason3Id = commonMethodsDB.CreateLACLegalStatusEndReason(_defaultTeamId, "Test LAC Status Reason Ended 3", "", "X3", DateTime.Now.Date, true, true);

            var personLACLegalStatus3Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason3Id, lacLegalStatus3Id, lacPlacement3Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-6), "CR1 3RL");
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus3Id, DateTime.Now.Date.AddDays(-4), lacLegalStatusEndReason3Id);

            #endregion

            #region Person LAC Legal Status 4

            var lacLegalStatusReason4Id = commonMethodsDB.CreateLACLegalStatusReason(_defaultTeamId, "C - Test Reason", "", "B", DateTime.Now.Date, true, true);
            var lacLegalStatus4Id = commonMethodsDB.CreateLACLegalStatus(_defaultTeamId, "C20-Valid-Qualifying", "", "C20", DateTime.Now.Date, 1);

            var personLACLegalStatus4Id = dbHelper.PersonLACLegalStatus.CreatePersonLACLegalStatus(_defaultTeamId, caseID, caseTitle, personId, lacEpisode1Id, lacLegalStatusReason4Id, lacLegalStatus4Id, lacPlacement3Id, lacLocationCodeId, lacPlacementProviderId, DateTime.Now.Date.AddDays(-4), "CR2 3RL");
            dbHelper.PersonLACLegalStatus.UpdateReasonEnded(personLACLegalStatus4Id, DateTime.Now.Date.AddDays(-2), lacLegalStatusEndReason3Id);

            #endregion

            #region CPIS

            var cpisRecords = dbHelper.cpis.GetCPISByPersonID(personId);
            Assert.AreEqual(1, cpisRecords.Count);
            var cpisRecordID = cpisRecords[0];

            #endregion

            #region Run the "CPIS - Generate Export File" schedule job

            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("CPIS - Generate Export File")[0];
            this.WebAPIHelper.Security.Authenticate(_tenantName, "cpis.user1", "Passw0rd_!");
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personId.ToString())
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToCPISRecordsPage();

            personCPISPage
                .WaitForPersonCPISPageToLoad()
                .OpenPersonCPISRecord(cpisRecordID.ToString());

            personCPISRecordPage
                .WaitForPersonCPISRecordPageToLoad()

                .WaitForPersonCPISRecordPageToLoad()

                .ValidateBatchIDFieldText("")

                .ValidateRecordTypeFieldText("LAC")
                .ValidatePersonFieldText(fullName)
                .ValidateResponsibleTeamFieldText("CareDirector T 1")
                .ValidateStatusFieldText("Sent")

                .ValidateStarDateFieldText(DateTime.Now.Date.AddDays(-4).ToString("dd/MM/yyyy"))
                .ValidateDeleteDateFieldText("")
                .ValidateEndDateFieldText(DateTime.Now.Date.AddDays(-2).ToString("dd/MM/yyyy"))
                .ValidateMotherOfUnbornChildFieldText("")

                .ValidateMessageFieldText("");
        }

        #endregion

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
