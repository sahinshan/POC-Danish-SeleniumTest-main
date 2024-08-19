using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    [DeploymentItem("chromedriver.exe"), DeploymentItem("Files\\Care Plan.Zip")]
    public class Person_CarePlan_UITestCases : FunctionalTest
    {

        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _maritalStatusId;
        private Guid _systemUserId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _carePlanType;
        private Guid _caseId;
        private string _caseNumber;
        private Guid _caseStatusId;
        private Guid _personCarePlanID;
        private Guid _carePlanType01;
        private Guid _carePlanReviewOutcomeId;
        private Guid _dataFormId;
        private Guid _newCaseId;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void Person_CarePlan_SetupTest()
        {
            try
            {
                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

                #endregion

                #region Marital Status

                var maritalStatusExist = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").Any();
                if (!maritalStatusExist)
                {
                    _maritalStatusId = dbHelper.maritalStatus.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);
                }
                if (_maritalStatusId == Guid.Empty)
                {
                    _maritalStatusId = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").FirstOrDefault();
                }
                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                {
                    _languageId = dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                }
                if (_languageId == Guid.Empty)
                {
                    _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").FirstOrDefault();
                }
                #endregion Lanuage

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("PersonCarePlan_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "PersonCarePlan_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("PersonCarePlan_Ethnicity")[0];

                #endregion

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

                #region Create SystemUser Record

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_CDV6_13981").Any();
                if (!newSystemUser)
                {
                    _systemUserId = dbHelper.systemUser.CreateSystemUser("CW_Forms_Test_User_CDV6_13981", "CW", "Forms_Test_User_CDV6_13981", "CW" + "Kumar", "Passw0rd_!", "CW_Forms_Test_User_CDV6_13981@somemail.com", "CW_Forms_Test_User_CDV6_13981@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }
                if (_systemUserId == Guid.Empty)
                {
                    _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_CDV6_13981").FirstOrDefault();
                }
                #endregion

                #region Person

                _personID = dbHelper.person.CreatePersonRecord("", "CDV6_13981", "", _currentDateString, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                _personFullName = "CDV6_13981 " + _currentDateString;

                #endregion

                #region Care Plan Type

                var carePlanExist = dbHelper.carePlanType.GetByName("Activities of Daily Living").Any();
                if (!carePlanExist)
                {
                    _carePlanType = dbHelper.carePlanType.CreateCarePlanTypeId("Activities of Daily Living", DateTime.Now, _careDirectorQA_TeamId);
                }
                if (_carePlanType == Guid.Empty)
                {
                    _carePlanType = dbHelper.carePlanType.GetByName("Activities of Daily Living").FirstOrDefault();
                }

                carePlanExist = dbHelper.carePlanType.GetByName("Mental Health Crisis Plan").Any();
                if (!carePlanExist)
                {
                    _carePlanType01 = dbHelper.carePlanType.CreateCarePlanTypeId("Mental Health Crisis Plan", DateTime.Now, _careDirectorQA_TeamId);
                }
                if (_carePlanType01 == Guid.Empty)
                {
                    _carePlanType01 = dbHelper.carePlanType.GetByName("Mental Health Crisis Plan").FirstOrDefault();
                }

                #endregion

                #region Care Plan Review Outcome
                _carePlanReviewOutcomeId = commonMethodsDB.CreateCarePlanReviewOutcome("CPReviewOutcome", new DateTime(2019, 1, 1), null, _careDirectorQA_TeamId);

                #endregion

                #region System User PersonCarePlanUser1

                commonMethodsDB.CreateSystemUserRecord("PersonCarePlanUser1", "PersonCarePlan", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region Template Care Plan https://advancedcsg.atlassian.net/browse/CDV6-5024
        [TestProperty("JiraIssueID", "CDV6-20391")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5024 - " +
            "User has access to a person record with a Care Plan (with no Reviews or Needs) - " +
            "Person has no First Name, Proferred Name, Date of Birth, Telephone Number, Home Aress, GP Details (involvements) - " +
            "Login in the web app - Navigate to the People page - Open the Person record - Click on the Care Plans Tab - " +
            "Click on the Care Plans Sub Tab - Select the Care Plan record - " +
            "Click on the mail Merge Button - Select Care Plan as the Mail Merge Template -  Select No for Create Activity - Tap on the OK button - " +
            "Validate that the generated word file contains the 2 static tables of the Care Plan Template")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Person_CarePlan_UITestMethod01()
        {
            #region Person

            var personFirstName = "CDV6_14299_";
            var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _personID = dbHelper.person.CreatePersonRecord("", personFirstName, "", personLastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            _personFullName = personFirstName + " " + personLastName;

            #endregion

            #region Case 

            _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();
            _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);
            _caseNumber = (string)(dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"]);

            #endregion

            commonMethodsDB.ImportMailMergeTemplate("Care Plan.Zip");
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("MailMerge.PrintFormat", "Word", "...", false, null);
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");
            var carePlanRecordID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _systemUserId, _personID, _caseId, _systemUserId, DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login("PersonCarePlanUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCarePlansLink();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .SelectCarePlanRecord(carePlanRecordID.ToString())
                .ClickMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Care Plan")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            System.Threading.Thread.Sleep(2000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "CarePlanTemplate.docx");
            Assert.IsTrue(fileExists);

            string[] wordsToFind = new string[]
            {
                /*CARE PLAN*/

                "Name: "+_personFullName, "Preferred Name:",
                "Date of Birth", "NHS Number:",
                "Telephone Number:",
                "Home Address:",
                "GP Details:",
                "Care Co-ordinator: CW Forms_Test_User_CDV6_13981",
                "Care Plan Level/Type: Activities of Daily Living",

            };

            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CarePlanTemplate.docx", wordsToFind);


            string[] wordsNotToFind = new string[]
            {
                /*REVIEW INFORMATION*/

                "Review Date:",
                "Carers/Advocates in Attendance:",
                "Co-Worker’s in Attendance:",
                "Other Professionals in Attendance:",
                "Person’s View:",
                "Carers/Advocates View:",
                "Other Professionals View:",
                "Review Summary:",
                "Review Outcome:", "Next Review Date:",

                /*CARE PLAN*/

                "Responsible Team:",
                "Domain of Need:",
                "Need Description:",
                "Outcome Description",
                "Intervention",
                "What:",
                "Where:",
                "Who:"
            };

            msWordHelper.ValidateWordsNotPresent(this.DownloadsDirectory + "\\CarePlanTemplate.docx", wordsNotToFind);
        }

        [TestProperty("JiraIssueID", "CDV6-20392")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5024 - " +
            "User has access to a person record with a Care Plan (with no Reviews or Needs) - " +
            "Person has all info to set in the first table: First Name, Proferred Name, Date of Birth, Telephone Number, Home Aress, GP Details (involvements) - " +
            "Login in the web app - Navigate to the People page - Open the Person record - Click on the Care Plans Tab - " +
            "Click on the Care Plans Sub Tab - Select the Care Plan record - " +
            "Click on the mail Merge Button - Select Care Plan as the Mail Merge Template -  Select No for Create Activity - Tap on the OK button - " +
            "Validate that the generated word file contains the 2 static tables of the Care Plan Template")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Person_CarePlan_UITestMethod02()
        {
            #region Person

            var personFirstName = "CDV6_14299_";
            var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _personID = dbHelper.person.CreatePersonRecord("", personFirstName, "", personLastName, "Mr. Couts", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 4, "987 654 3210", personLastName, "SJK8DS99H8FGFD", "3746SAS32HJH3G43SS",
                "Prop Name", "Prop No", "Str", "Vlg", "Tow", "Cou", "PC 3RL");
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            _personFullName = personFirstName + " " + personLastName;

            #endregion

            #region Case 

            _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();
            _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);
            _caseNumber = (string)(dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"]);

            #endregion

            commonMethodsDB.ImportMailMergeTemplate("Care Plan.Zip");
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("MailMerge.PrintFormat", "Word", "...", false, null);
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");
            var carePlanRecordID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _systemUserId, _personID, _caseId, _systemUserId, DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login("PersonCarePlanUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCarePlansLink();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .SelectCarePlanRecord(carePlanRecordID.ToString())
                .ClickMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Care Plan")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            System.Threading.Thread.Sleep(2000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "CarePlanTemplate.docx");
            Assert.IsTrue(fileExists);

            string[] wordsToFind = new string[]
            {
                /*CARE PLAN*/

                "Name: "+_personFullName, "Preferred Name: Mr. Couts",
                "Date of Birth: 02/01/2000", "NHS Number: 987 654 3210",
                "Home Address:", "Prop Name", "Prop No", "Str", "Vlg", "Tow", "Cou", "PC 3RL",
                "Care Co-ordinator: CW Forms_Test_User_CDV6_13981",
                "Care Plan Level/Type: Activities of Daily Living",

            };

            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CarePlanTemplate.docx", wordsToFind);


            string[] wordsNotToFind = new string[]
            {
                /*REVIEW INFORMATION*/

                "Review Date:",
                "Carers/Advocates in Attendance:",
                "Co-Worker’s in Attendance:",
                "Other Professionals in Attendance:",
                "Person’s View:",
                "Carers/Advocates View:",
                "Other Professionals View:",
                "Review Summary:",
                "Review Outcome:", "Next Review Date:",

                /*CARE PLAN*/

                "Responsible Team:",
                "Domain of Need:",
                "Need Description:",
                "Outcome Description",
                "Intervention",
                "What:",
                "Where:",
                "Who:"
            };

            msWordHelper.ValidateWordsNotPresent(this.DownloadsDirectory + "\\CarePlanTemplate.docx", wordsNotToFind);
        }

        [TestProperty("JiraIssueID", "CDV6-20393")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5024 - " +
            "User has access to a person record with a Care Plan (with one Review and no Needs) - " +
            "Person has all info to set in the Second Static Table - " +
            "Login in the web app - Navigate to the People page - Open the Person record - Click on the Care Plans Tab - " +
            "Click on the Care Plans Sub Tab - Select the Care Plan record - " +
            "Click on the mail Merge Button - Select Care Plan as the Mail Merge Template -  Select No for Create Activity - Tap on the OK button - " +
            "Validate that the generated word file contains the 2 static tables of the Care Plan Template - Validate the contant of the static table")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Person_CarePlan_UITestMethod03()
        {
            #region Person

            var personFirstName = "CDV6_14299_";
            var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _personID = dbHelper.person.CreatePersonRecord("", personFirstName, "", personLastName, "", new DateTime(2000, 07, 01), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            _personFullName = personFirstName + " " + personLastName;

            var _person2ID = dbHelper.person.CreatePersonRecord("", "Marisol", "", "Acevedo", "", new DateTime(2000, 07, 01), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            var _person2Number = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            var _person3ID = dbHelper.person.CreatePersonRecord("", "Marjorie", "", "Acevedo", "", new DateTime(2000, 07, 01), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            var _person3Number = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            List<Guid> _carersInAttendance = new List<Guid>()
            { _person2ID, _person3ID };


            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord("AarronKirrk", "Aarron", "Kirrk!", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            var _systemUser3Id = commonMethodsDB.CreateSystemUserRecord("AbbieCotterell", "Abbie", "Cotterell!", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            List<Guid> _coworkersInAttendance = new List<Guid>()
            { _systemUser2Id, _systemUser3Id };

            var professionTypeId1 = commonMethodsDB.CreateProfessionType(_careDirectorQA_TeamId, "District Nurse", new DateTime(2019, 1, 15));
            var professionTypeId2 = commonMethodsDB.CreateProfessionType(_careDirectorQA_TeamId, "Doctor", new DateTime(2019, 2, 27));

            var professionalId1 = commonMethodsDB.CreateProfessional(_careDirectorQA_TeamId, professionTypeId1, "Ms", "Annabel", "Watson");
            var professionalId2 = commonMethodsDB.CreateProfessional(_careDirectorQA_TeamId, professionTypeId2, "Dr", "Beatrice", "Oneill");

            List<Guid> _otherProfessionalsInAttendance = new List<Guid>()
            { professionalId1, professionalId2 };

            #endregion

            #region Case 

            _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();
            _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);
            _caseNumber = (string)(dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"]);

            #endregion

            commonMethodsDB.ImportMailMergeTemplate("Care Plan.Zip");
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("MailMerge.PrintFormat", "Word", "...", false, null);
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");
            var carePlanRecordID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _systemUserId, _personID, _caseId, _systemUserId, new DateTime(2020, 10, 16), 1, 1, _careDirectorQA_TeamId);

            var personCarePlanReviewId = dbHelper.personCarePlanReview.CreatePersonCarePlanReview(_careDirectorQA_TeamId, _systemUserId,
                _personID, carePlanRecordID, new DateTime(2020, 10, 16), new DateTime(2020, 10, 16), null,
                2, "summary and action ...", _carePlanReviewOutcomeId, "person view 1\r\nperson view 2",
                "advocate view 1\r\nadvocate view 2", "professional view 1\r\nprofessional view 2", true, new DateTime(2020, 10, 17), _systemUserId,
                _carersInAttendance, _coworkersInAttendance, _otherProfessionalsInAttendance);

            loginPage
                .GoToLoginPage()
                .Login("PersonCarePlanUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCarePlansLink();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .SelectCarePlanRecord(carePlanRecordID.ToString())
                .ClickMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Care Plan")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            System.Threading.Thread.Sleep(2000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "CarePlanTemplate.docx");
            Assert.IsTrue(fileExists);

            string[] wordsToFind = new string[]
            {
                /*CARE PLAN*/

                "Name: "+_personFullName, "Preferred Name:",
                "Date of Birth: 01/07/2000", "NHS Number:",
                "Telephone Number:",
                "Home Address:",
                "GP Details:",
                "Care Co-ordinator: CW Forms_Test_User_CDV6_13981",
                "Care Plan Level/Type: Activities of Daily Living",
                
                /*REVIEW INFORMATION*/

                "Review Date: 16/10/2020",
                "Carers/Advocates in Attendance:", "Marisol Acevedo", "Marjorie Acevedo",
                "Co-worker's in Attendance:", "Aarron Kirrk", "Abbie Cotterell",
                "Other Professionals in Attendance:", "Ms Annabel Watson", "Beatrice Oneill",
                "Person's View: person view 1", "person view 2",
                "Carers/Advocates View: advocate view 1", "advocate view 2",
                "Other Professionals View: professional view 1", "professional view 2",
                "Review Summary: summary and action ...",
                "Review Outcome:", "CPReviewOutcome", "Next Review Date:", "17/10/2020"
            };

            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CarePlanTemplate.docx", wordsToFind);


            string[] wordsNotToFind = new string[]
            {
                /*CARE PLAN*/

                "Responsible Team:",
                "Domain of Need:",
                "Need Description:",
                "Outcome Description",
                "Intervention",
                "What:",
                "Where:",
                "Who:"
            };

            msWordHelper.ValidateWordsNotPresent(this.DownloadsDirectory + "\\CarePlanTemplate.docx", wordsNotToFind);
        }

        [TestProperty("JiraIssueID", "CDV6-20394")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-5024 - " +
            "User has access to a person that has all info to set in the dynamic tables: 1 person care plan, 2 plan needs, 4 outcomes, 8 interventions - " +
            "Person has all info to set in the Second Static Table - " +
            "Login in the web app - Navigate to the People page - Open the Person record - Click on the Care Plans Tab - " +
            "Click on the Care Plans Sub Tab - Select the Care Plan record - " +
            "Click on the mail Merge Button - Select Care Plan as the Mail Merge Template -  Select No for Create Activity - Tap on the OK button - " +
            "Validate that the generated word file contains the all information in the dynamic tables for the needs, outcomes and interventions")]
        [TestCategory("UITest")]
        [TestMethod]
        public void Person_CarePlan_UITestMethod04()
        {
            Guid systemSettingID1 = dbHelper.systemSetting.GetSystemSettingIdByName("MailMerge.PrintFormat").FirstOrDefault();
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            var personID = new Guid("e2557ec4-da0f-eb11-a2cd-005056926fe4"); //Carlos Coutinho
            var personNumber = "506064";
            var carePlanRecordID = new Guid("2c5d95e5-da0f-eb11-a2cd-005056926fe4");

            loginPage
                .GoToLoginPage()
                .Login("PersonCarePlanUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCarePlansLink();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .SelectCarePlanRecord(carePlanRecordID.ToString())
                .ClickMailMergeButton();

            mailMergePopup
                .WaitForMailMergePopupToLoad()
                .SelectMailMergeTemplateByText("Care Plan")
                .TapCreateActivityNoRadioButton()
                .ClickOKButton();

            System.Threading.Thread.Sleep(2000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "CarePlanTemplate.docx");
            Assert.IsTrue(fileExists);

            string[] wordsToFind = new string[]
            {
                /*DYNAMIC TABLE 1*/

                

                /*Need 1*/
                "Responsible Team: Acute & PICU",
                "Domain of Need: Behaviour",
                "Need Description: care plan need 1 description ...",

                /*Need 1 - Outcome 1*/
                "Goal/Outcome Description (1):", "Outcome Description 1 ...",

                /*Need 1 - Outcome 1 - Intervention 1*/ 
                "Intervention (1a):",
                "What: Intervention 1 What ....",
                "Where: Intervention 1 Where ....",
                "Who: Intervention 1 Who ....",
                /*Need 1 - Outcome 1 - Intervention 2*/  
                "What: Intervention 2 What ...",
                "Where: Intervention 2 Where ...",
                "Who: Intervention 2 Who ...",

                /*Need 1 - Outcome 2*/
                "Outcome Description (2):", "Outcome 2 Description",

                /*Need 1 - Outcome 2 - Intervention 1*/ 
                "What: Intervention 3 What ...",
                "Where: Intervention 3 Where ...",
                "Who: Intervention 3 Who ...",
                /*Need 1 - Outcome 2 - Intervention 2*/  
                "What: Intervention 4 What ...",
                "Where: Intervention 4 Where ...",
                "Who: Intervention 4 Who ...",


                /*Need 2*/
                "Responsible Team: Admission Ward",
                "Domain of Need: Self Care",
                "Need Description: Care Plan Need 2 Description",

                /*Need 2 - Outcome 1*/
                "Outcome Description (1):", "Outcome 3 Description ....",

                /*Need 2 - Outcome 1 - Intervention 1*/ 
                "What: Intervention 5 What ...",
                "Where: Intervention 5 Where ...",
                "Who: Intervention 5 Who ...",
                /*Need 2 - Outcome 1 - Intervention 2*/  
                "What: Intervention 6 What ...",
                "Where: Intervention 6 Where ...",
                "Who: Intervention 6 Who ...",

                /*Need 2 - Outcome 2*/
                "Outcome Description (2):", "Outcome 4 Description",

                /*Need 2 - Outcome 2 - Intervention 1*/ 
                "What: Intervention 7 What ...",
                "Where: Intervention 7 Where ...",
                "Who: Intervention 7 Who ...",
                /*Need 2 - Outcome 2 - Intervention 2*/  
                "What: Intervention 8 What ...",
                "Where: Intervention 8 Where ...",
                "Who: Intervention 8 Who ...",
            };

            msWordHelper.ValidateWordsPresent(this.DownloadsDirectory + "\\CarePlanTemplate.docx", wordsToFind);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8418

        [TestProperty("JiraIssueID", "CDV6-20395")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-2446 - " +
            "Open a person care plan case note record (with data in all fields) - Click on the clone button - Wait for the clone popup to be displayed - " +
            "Confirm the clone operation - Validate that the case note record is properly cloned")]
        [TestCategory("UITest")]
        [TestMethod]
        public void PersonCarePlanCaseNote_Cloning_UITestMethod01()
        {
            var personID = new Guid("f4f051f0-de2f-4721-974b-0da92f5fedbc"); //Selma Ellis
            var personNumber = "109858";
            var caseID = new Guid("6de3f3dd-3540-e911-a2c5-005056926fe4");//CAS-3-297734
            var controlCaseID = new Guid("af2f7da3-e93a-e911-a2c5-005056926fe4"); //CAS-3-212576
            var personCarePlanID = new Guid("4e718ac9-07df-eb11-a325-005056926fe4"); //Activities of Daily Living 
            var PersonCarePlanCaseNoteID = new Guid("3e0102f4-07df-eb11-a325-005056926fe4"); //Person Care Plan Case Note 001 

            //remove all cloned case notes for the case record
            foreach (var recordid in dbHelper.caseCaseNote.GetByCaseID(caseID))
                dbHelper.caseCaseNote.DeleteCaseCaseNote(recordid);

            //remove all cloned case notes for the control case record
            foreach (var recordid in dbHelper.caseCaseNote.GetByCaseID(controlCaseID))
                dbHelper.caseCaseNote.DeleteCaseCaseNote(recordid);


            loginPage
                .GoToLoginPage()
                .Login("PersonCarePlanUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCarePlansLink();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .ClickOnCarePlanRecord(personCarePlanID.ToString());

            personCarePlanRecordPage
                .WaitForPersonCarePlanRecordPageToLoad()
                .NavigateToPersonCarePlanCaseNotesArea();

            personCarePlanCaseNotesPage
                .WaitForPersonCarePlanCaseNotesPageToLoad()
                .OpenPersonCarePlanCaseNoteRecord(PersonCarePlanCaseNoteID.ToString());

            personCarePlanCaseNoteRecordPage
                .WaitForPersonCarePlanCaseNoteRecordPageToLoad("Person Care Plan Case Note 001")
                .ClickCloneButton();

            cloneActivityPopup
                .WaitForCloneActivityPopupToLoad()
                .SelectBusinessObjectTypeText("Case")
                .SelectRetainStatus("Yes")
                .SelectRecord(caseID.ToString())
                .ClickCloneButton()

                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("1 of 1 Activities Cloned Successfully")
                .ClickCloseButton();

            dbHelper = new DBHelper.DatabaseHelper();
            var records = dbHelper.caseCaseNote.GetByCaseID(caseID);
            Assert.AreEqual(1, records.Count);

            var fields = dbHelper.caseCaseNote.GetByID(records[0],
                "subject", "notes", "personid", "ownerid", "activityreasonid", "responsibleuserid",
                "activitypriorityid", "activitycategoryid", "casenotedate", "activitysubcategoryid", "statusid", "activityoutcomeid",
                "informationbythirdparty", "issignificantevent", "significanteventcategoryid", "significanteventdate", "significanteventsubcategoryid",
                "iscloned", "clonedfromid");

            var teamId = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA
            var activityreasonid = new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"); //Assessment
            var responsibleuserid = new Guid("32972024-0839-e911-a2c5-005056926fe4"); //José Brazeta    
            var activitypriorityid = new Guid("5246a13f-9d45-e911-a2c5-005056926fe4"); //Normal
            var activitycategoryid = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"); //Assessment
            var casenotedate = new DateTime(2021, 7, 5, 8, 25, 0, DateTimeKind.Utc);
            var activitysubcategoryid = new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"); //Health Assessment
            var statusid = 1; //Open
            var activityoutcomeid = new Guid("4c2bec1c-9e45-e911-a2c5-005056926fe4"); //Completed
            var significanteventcategoryid = new Guid("85bf13ef-1a52-e911-a2c5-005056926fe4"); //Category 1
            var significanteventdate = new DateTime(2021, 7, 4, 0, 0, 0, DateTimeKind.Utc);
            var significanteventsubcategoryid = new Guid("641f471b-1b52-e911-a2c5-005056926fe4"); //Sub Cat 1_2


            Assert.AreEqual("Person Care Plan Case Note 001", fields["subject"]);
            Assert.AreEqual("<p>Person Care Plan Case Note Description</p>", fields["notes"]);
            Assert.AreEqual(personID, fields["personid"]);
            Assert.AreEqual(teamId, fields["ownerid"]);
            Assert.AreEqual(activityreasonid, fields["activityreasonid"]);
            Assert.AreEqual(responsibleuserid, fields["responsibleuserid"]);
            Assert.AreEqual(activitypriorityid, fields["activitypriorityid"]);
            Assert.AreEqual(activitycategoryid, fields["activitycategoryid"]);
            Assert.AreEqual(casenotedate.ToLocalTime(), ((DateTime)fields["casenotedate"]).ToLocalTime());
            Assert.AreEqual(activitysubcategoryid, fields["activitysubcategoryid"]);
            Assert.AreEqual(statusid, fields["statusid"]);
            Assert.AreEqual(activityoutcomeid, fields["activityoutcomeid"]);
            Assert.AreEqual(true, fields["informationbythirdparty"]);
            Assert.AreEqual(true, fields["issignificantevent"]);
            Assert.AreEqual(significanteventcategoryid, fields["significanteventcategoryid"]);
            Assert.AreEqual(significanteventdate.ToLocalTime(), ((DateTime)fields["significanteventdate"]).ToLocalTime());
            Assert.AreEqual(significanteventsubcategoryid, fields["significanteventsubcategoryid"]);
            Assert.AreEqual(true, fields["iscloned"]);
            Assert.AreEqual(PersonCarePlanCaseNoteID, fields["clonedfromid"]);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-13981

        [TestProperty("JiraIssueID", "CDV6-14266")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-14266- " +
            "Login CD -> Work Place -> People -> Select any existing person -> Care Plans -> Click + to create new care plan -> " +
            "Select yes for Agreed with Person or Legitimate Representative -> Look for the field Plan Also Agreed By->" +
            "Label of the existing field Agreed By should be changed to Plan Also Agreed By")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_CarePlan_UITestMethod05()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _systemUserId, _personID, _caseId, _systemUserId, DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login("PersonCarePlanUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCarePlansLink();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .ClickCreateNewRecord();

            personCarePlanRecordPage
                .WaitForPersonCarePlanRecordPageToLoad()
                .SelectYesAgreedWithPersonRadioButton()
                .ValidateAgreedWithPersonLabel("Agreed with Person or Legitimate Representative");

        }

        [TestProperty("JiraIssueID", "CDV6-14268")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-14268- " +
           "Login CD -> Work Place -> People -> Select any existing person -> Care Plans -> Click + to create new care plan -> " +
            "Select yes for Agreed with Person or Legitimate Representative ->  Check the look up values of  Plan Also Agreed By->" +
            "Should not display “Patient or Patient Proxy” as  one of the look up value.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_CarePlan_UITestMethod06()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _systemUserId, _personID, _caseId, _systemUserId, DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            loginPage
                 .GoToLoginPage()
                 .Login("PersonCarePlanUser1", "Passw0rd_!")
                 .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCarePlansLink();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .ClickCreateNewRecord();

            personCarePlanRecordPage
                .WaitForPersonCarePlanRecordPageToLoad()
                .SelectYesAgreedWithPersonRadioButton()
                .ValidatePlanAlsoAgreedByLabel("Plan Also Agreed By");

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-14107
        [TestProperty("JiraIssueID", "CDV6-14299")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-14299- " +
        "Login CD -> Workplace -> People -> Select existing person -> Move to Care Plans Tab -> Select the existing authorized Care Plans record and try to edit below fields " +
        " Date or Agreement & Time , Plan Also Agreed By  , Family Involved In Care Plan , Family Not Involved Reason, Specify the Reason,Start Time->All the below fields should be in non editable mode" +
        "Date or Agreement & Time , Plan Also Agreed By  , Family Involved In Care Plan , Family Not Involved Reason , Specify the Reason,Start Time")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_CarePlan_UITestMethod07()
        {
            var personFirstName = "Person_CarePlan";
            var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _newPersonID = dbHelper.person.CreatePersonRecord("", personFirstName, "", personLastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            var _newPersonNumber = (int)dbHelper.person.GetPersonById(_newPersonID, "personnumber")["personnumber"];
            var _newCaseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _newPersonID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);

            var _newPersonCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _systemUserId, _newPersonID, _newCaseId, _systemUserId, DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
            var carePlanStatusId = 2;

            dbHelper.personCarePlan.UpdateStatus(_newPersonCarePlanID, carePlanStatusId);

            loginPage
                  .GoToLoginPage()
                  .Login("PersonCarePlanUser1", "Passw0rd_!")
                  .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_newPersonNumber.ToString(), _newPersonID.ToString())
                .OpenPersonRecord(_newPersonID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCarePlansLink();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .ClickOnCarePlanRecord(_newPersonCarePlanID.ToString());

            personCarePlanRecordPage
                 .WaitForPersonCarePlanRecordPageToLoad()
                 .ValidateAgreedDateNonEditable()
                 .ValidateAgreedTimeNonEditable()
                 .ValidateCarePlanAgreedByNonEditable()
                 .ValidateFamilyNotInvolvedReasonNonEditable()
                 .ValidateCarePlanFamilyInvolvedNonEditable()
                 .ValidateReasonTextBoxNonEditable()
                 .ValidateStartDateNonEditable();
        }

        [TestProperty("JiraIssueID", "CDV6-14300")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-14300- " +
         "Login CD -> Workplace -> People -> Select existing person -> Move to Care Plans Tab -> Select the existing non authorized Care Plans record and try to edit below fields " +
        " Date or Agreement & Time , Plan Also Agreed By  , Family Involved In Care Plan , Family Not Involved Reason, Specify the Reason,Start Time->All the below fields should be in editable mode" +
        "Date or Agreement & Time , Plan Also Agreed By  , Family Involved In Care Plan , Family Not Involved Reason , Specify the Reason,Start Time")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_CarePlan_UITestMethod08()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _systemUserId, _personID, _caseId, _systemUserId, DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            loginPage
                  .GoToLoginPage()
                  .Login("PersonCarePlanUser1", "Passw0rd_!")
                  .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCarePlansLink();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

            personCarePlanRecordPage
                 .WaitForPersonCarePlanRecordPageToLoad()
                 .ValidateAgreedDateEditable()
                .ValidateAgreedTimeEditable()
                .ValidateCarePlanAgreedByEditable()
                .ValidateFamilyNotInvolvedReasonEditable()
                .ValidateCarePlanFamilyInvolvedEditable()
                .ValidateReasonTextBoxEditable()
                .ValidateStartDateEditable();

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-14109

        // [Description("https://advancedcsg.atlassian.net/browse/CDV6-14363- " +
        //"Login CD -> Work Place -> People -> Select any existing person -> Care Plans -> Open any existing non authorized care plan record which was created before " +
        //"Family Involved In Care Plan introduced in Care Plan -> Click on Authorize icon and confirm authorization-> System should authorize the record without asking Family Involved In Care Plan to be filled")]
        // [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        // public void Person_CarePlan_UITestMethod09()
        // {
        //     var _newPersonID = dbHelper.person.CreatePersonRecord("", "Person_CarePlan", "", "CDV6_14299", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
        //     var _newPersonNumber = (int)dbHelper.person.GetPersonById(_newPersonID, "personnumber")["personnumber"];

        //     var _newCaseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _newPersonID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);

        //     var _newPersonCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _systemUserId, _newPersonID, _newCaseId, _systemUserId, DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

        //     dbHelper.personCarePlan.UpdateCreatedOn(_newPersonCarePlanID, new DateTime(2019, 1, 1));

        //     loginPage
        //         .GoToLoginPage()
        //         .Login("PersonCarePlanUser1", "Passw0rd_!")
        //         .WaitFormHomePageToLoad();

        //     mainMenu
        //         .WaitForMainMenuToLoad()
        //         .NavigateToPeopleSection();

        //     peoplePage
        //         .WaitForPeoplePageToLoad()
        //         .SearchPersonRecord(_newPersonNumber.ToString(), _newPersonID.ToString())
        //         .OpenPersonRecord(_newPersonID.ToString());

        //     personRecordPage
        //         .WaitForPersonRecordPageToLoad()
        //         .TapCarePlansTab();

        //     personCarePlansSubPage
        //         .WaitForPersonCarePlansSubPageToLoad()
        //         .ClickCarePlansLink();

        //     personCarePlansSubPage_CarePlansTab
        //         .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
        //         .ClickOnCarePlanRecord(_newPersonCarePlanID.ToString());

        //     personCarePlanRecordPage
        //          .WaitForPersonCarePlanRecordPageToLoad()
        //          .SelectAuthoriseButton();

        //     alertPopup
        //         .WaitForAlertPopupToLoad()
        //         .ValidateAlertText("Are you sure you want to authorise this plan?")
        //         .TapOKButton();

        //     personCarePlanRecordPage
        //          .WaitForPersonCarePlanRecordPageToLoad();

        // }   

        //CDV6-15543 Based up on the user story automation script need to be update in future.

        [TestProperty("JiraIssueID", "CDV6-14364")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-14364- " +
            "Login CD -> Work Place -> People -> Select any existing person -> Care Plans -> Open any existing non authorized care plan record which was created after " +
            "Family Involved In Care Plan introduced in Care Plan -> Click on Authorize icon and confirm authorization->" +
            "Should not authorize the record , System should  give appropriate error if Family Involved In Care Plan is blank.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS"), Ignore]
        public void Person_CarePlan_UITestMethod010()
        {
            var personFirstName = "Person_CarePlan";
            var personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _newPersonID = dbHelper.person.CreatePersonRecord("", personFirstName, "", personLastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            var _newPersonNumber = (int)dbHelper.person.GetPersonById(_newPersonID, "personnumber")["personnumber"];

            _newCaseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _newPersonID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);

            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _systemUserId, _newPersonID, _newCaseId, _systemUserId, DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login("PersonCarePlanUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_newPersonNumber.ToString(), _newPersonID.ToString())
                .OpenPersonRecord(_newPersonID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCarePlansLink();

            personCarePlansSubPage_CarePlansTab
               .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
               .ClickOnCarePlanRecord(_personCarePlanID.ToString());

            personCarePlanRecordPage
                .WaitForPersonCarePlanRecordPageToLoad()
                .SelectCWCarePlanFamilyInvolvedDropDown("")
                .ValidateFamilyInvolvedInCarePlanErrormessage("Please fill out this field.")
                .SelectAuthoriseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to authorise this plan?")
                .TapOKButton();

            personCarePlanRecordPage
               .WaitForPersonCarePlanRecordPageToLoad()
               .ValidateNodificationErrormessage("Some data is not correct. Please review the data in the Form.");
        }

        // [TestProperty("JiraIssueID", "CDV6-14408")]
        // [Description("https://advancedcsg.atlassian.net/browse/CDV6-14408- " +
        //"Login CD -> Work Place -> People -> Select any existing person -> Care Plans -> Open any existing authorized care plan record which was created before" +
        //     " Family Involved In Care Plan introduced in Care Plan -> Give End Date and End Reason and Save the record->" +
        //     "System should save the record without asking Family Involved In Care Plan to be filled.")]
        // [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        // public void Person_CarePlan_UITestMethod011()
        // {
        //     var personID = new Guid("68ada6df-503e-e911-a2c5-005056926fe4"); //Test last name(existing authorized care plan record which was created before Family Involved In Care Plan introduced in Care Plan)
        //     var personNumber = "500028";
        //     var carePlanRecordID = new Guid("5b5b6198-513e-e911-a2c5-005056926fe4");
        //     var endReasonId = new Guid("de05cda9-8ee6-e811-80dc-0050560502cc");

        //     loginPage
        //          .GoToLoginPage()
        //         .Login("PersonCarePlanUser1", "Passw0rd_!")
        //         .WaitFormHomePageToLoad();

        //     mainMenu
        //         .WaitForMainMenuToLoad()
        //         .NavigateToPeopleSection();

        //     peoplePage
        //         .WaitForPeoplePageToLoad()
        //         .SearchPersonRecord(personNumber.ToString(), personID.ToString())
        //         .OpenPersonRecord(personID.ToString());

        //     personRecordPage
        //         .WaitForPersonRecordPageToLoad()
        //         .TapCarePlansTab();

        //     personCarePlansSubPage
        //         .WaitForPersonCarePlansSubPageToLoad()
        //         .ClickCarePlansLink();

        //     personCarePlansSubPage_CarePlansTab
        //         .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
        //         .ClickOnCarePlanRecord(carePlanRecordID.ToString());

        //     personCarePlanRecordPage
        //          .WaitForPersonCarePlanRecordPageToLoad()
        //          .InsertEndDate("06/01/2022")
        //          .ClickEndReasonLookUp();

        //     lookupPopup
        //         .WaitForLookupPopupToLoad()
        //         .TypeSearchQuery("Ended")
        //         .TapSearchButton()
        //         .SelectResultElement(endReasonId.ToString());

        //     personCarePlanRecordPage
        //        .WaitForPersonCarePlanRecordPageToLoad()
        //        .ClickSaveAndCloseButton();
        // }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-14543

        [TestProperty("JiraIssueID", "CDV6-14666")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-14666- " +
            "Login CD -> Work Place -> People -> Select any existing person -> Care Plans -> Open any existing Care Plan -> Click Copy Care Plan -> Verify the fields displayed in the Pop up window" +
            "Below new fields should be displayed in pop up along with the existing fields-> Mandatory field called Family involved in Care Plan ->If no, please show the mandatory field Family Not involved Reason field" +
            "and a Non mandatory field Agreed with Person or Legal Representative")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_CarePlan_UITestMethod012()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _systemUserId, _personID, _caseId, _systemUserId, DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login("PersonCarePlanUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCarePlansLink();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

            personCarePlanRecordPage
               .WaitForPersonCarePlanRecordPageToLoad()
               .ClickAdditionalItemsMenuButton()
               .ClickCopyCarePlan()
               .WaitForCopyCarePlanPageToLoad()
               .ValidateFamilyInvolvedInCarePlanLabel("Family involved in Care Plan")
               .ValidateAgreeWithPersonLabel("Agreed with Person or Legal Representative");
        }

        [TestProperty("JiraIssueID", "CDV6-14667")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-14667- " +
         "Login CD -> Work Place -> People -> Select any existing person -> Care Plans -> Open any existing Care Plan -> Click Copy Care Plan -> Fill all the mandatory and Non mandatory " +
            "fields in the pop up window and click Copy -> Open the newly created record and check below fields-> 1. Family involved in Care PlanFamily Not involved Reason field" +
            " 2.Agreedwith Person or Legal Representative 3.Plan also agreed by 4.Reason for new updated plan 5.Date of agreement 6.Time of agreement   7.Start Time  ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_CarePlan_UITestMethod013()
        {
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _systemUserId, _personID, _caseId, _systemUserId, DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);

            #region Case 

            _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();
            _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 20);
            _caseNumber = (string)(dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("PersonCarePlanUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCarePlansLink();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

            personCarePlanRecordPage
               .WaitForPersonCarePlanRecordPageToLoad()
               .ClickAdditionalItemsMenuButton()
               .ClickCopyCarePlan();

            copyCarePlanPopupPage
                .WaitForCopyCarePlanPageToLoad()
                .ClickCarePlanTypeIdLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Mental Health Crisis Plan").TapSearchButton().SelectResultElement(_carePlanType01.ToString());

            copyCarePlanPopupPage
                .WaitForCopyCarePlanPageToLoad()
                .ClickCareCoordinatorLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CW_Forms_Test_User_CDV6_13981").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            copyCarePlanPopupPage
                .WaitForCopyCarePlanPageToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareDirector QA").TapSearchButton().SelectResultElement(_careDirectorQA_TeamId.ToString());

            copyCarePlanPopupPage
                .WaitForCopyCarePlanPageToLoad()
                .InsertStartDate(DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickCaseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_caseNumber).TapSearchButton().SelectResultElement(_caseId.ToString());

            copyCarePlanPopupPage
                .WaitForCopyCarePlanPageToLoad()
                .SelectFamilyInvolvedInCarePlan("Yes")
                .ClickCopyButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Person care plan created.").TapOKButton();

            System.Threading.Thread.Sleep(3000);

            var personCarePlanId = dbHelper.personCarePlan.GetByCaseID(_caseId);
            Assert.AreEqual(1, personCarePlanId.Count);

            personCarePlansSubPage_CarePlansTab
               .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
               .ClickOnCarePlanRecord(personCarePlanId[0].ToString());

            personCarePlanRecordPage
              .WaitForPersonCarePlanRecordPageToLoad()
              .SelectYesAgreedWithPersonRadioButton()
              .WaitForPersonCarePlanRecordPageToLoad()
              .ValidateAgreedDateEditable()
              .ValidateAgreedTimeEditable()
              .ValidateCarePlanAgreedByEditable()
              .ValidateFamilyNotInvolvedReasonEditable()
              .ValidateCarePlanFamilyInvolvedEditable()
              .ValidateReasonTextBoxEditable()
              .ValidateStartDateEditable();

        }

        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        //[TestCleanup]
        //public void ClassCleanUp()
        //{
        //    string jiraIssueID = (string)this.TestContext.Properties["JiraIssueID"];

        //    //if we have a jira id for the test then we will update its status in jira
        //    if (jiraIssueID != null)
        //    {
        //        bool testPassed = this.TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;


        //        var zapi = new AtlassianServiceAPI.Models.Zapi()
        //        {
        //            AccessKey = ConfigurationManager.AppSettings["AccessKey"],
        //            SecretKey = ConfigurationManager.AppSettings["SecretKey"],
        //            User = ConfigurationManager.AppSettings["User"],
        //        };

        //        var jiraAPI = new AtlassianServiceAPI.Models.JiraApi()
        //        {
        //            Authentication = ConfigurationManager.AppSettings["Authentication"],
        //            JiraCloudUrl = ConfigurationManager.AppSettings["JiraCloudUrl"],
        //            ProjectKey = ConfigurationManager.AppSettings["ProjectKey"]
        //        };

        //        AtlassianServicesAPI.AtlassianService atlassianService = new AtlassianServicesAPI.AtlassianService(zapi, jiraAPI);

        //        string versionName = ConfigurationManager.AppSettings["CurrentVersionName"];

        //        if(testPassed)
        //            atlassianService.UpdateTestStatus(jiraIssueID, versionName, AtlassianServiceAPI.Models.JiraTestOutcome.Passed);
        //        else
        //            atlassianService.UpdateTestStatus(jiraIssueID, versionName, AtlassianServiceAPI.Models.JiraTestOutcome.Failed);


        //    }

        //}

    }
}

