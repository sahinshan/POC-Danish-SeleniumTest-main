﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class Person_Allegations_AllegedAbuser_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _defaultUserId;
        private string _defaultUsername;
        private string _defaultUserFullname;
        private Guid _systemUserId;
        private string _systemUserName;
        private Guid _personId;
        private Guid _personId2;
        private int _personNumber;
        private string _person_fullName;
        private string _person_fullName2;
        private Guid _dataFormId;
        private Guid _caseStatusId;
        private Guid _contactReasonId;
        private Guid _adultsafeguardingcategoryofabuseid;
        private Guid _adultsafeguardingstatusid;
        private Guid _allegationCategoryId;
        private Guid _caseId;
        private string _caseTitle;
        private Guid _safeguardingID;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                _defaultUsername = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                _defaultUserId = dbHelper.systemUser.GetSystemUserByUserName(_defaultUsername).FirstOrDefault();
                _defaultUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultUserId, "fullname")["fullname"];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create SystemUser Record

                _systemUserName = "Person_Allegations_User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Allegations", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Person

                var firstName = "First";
                var lastName = "LN_" + _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _person_fullName = firstName + " " + lastName;

                _personId2 = commonMethodsDB.CreatePersonRecord("Victim", "LN_" + _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
                _person_fullName2 = "Victim LN_" + _currentDateSuffix;

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Inpatient)", _careDirectorQA_TeamId);

                #endregion

                #region Adult Safeguarding Category of Abuse

                _adultsafeguardingcategoryofabuseid = commonMethodsDB.CreateAdultSafeguardingCategoryOfAbuse(_careDirectorQA_TeamId, "Emotional", new DateTime(2019, 4, 1));

                #endregion

                #region Adult Safeguarding Status

                _adultsafeguardingstatusid = commonMethodsDB.CreateAdultSafeguardingStatus(_careDirectorQA_TeamId, "Concern", new DateTime(2019, 4, 1));

                #endregion

                #region Allegation Category

                _allegationCategoryId = commonMethodsDB.CreateAllegationCategory(_careDirectorQA_TeamId, "Physical Abuse", new DateTime(2019, 4, 1));

                #endregion

                #region Case

                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personId2, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2021, 8, 3), new DateTime(2021, 8, 12), 20, "Care Form Record");
                _caseTitle = (string)dbHelper.Case.GetCaseByID(_caseId, "title")["title"];

                #endregion

                #region Adult Safeguarding

                _safeguardingID = dbHelper.AdultSafeguarding.CreateAdultSafeguarding(_careDirectorQA_TeamId, _systemUserId, _caseId, _caseTitle, _personId, _adultsafeguardingcategoryofabuseid, _adultsafeguardingstatusid, new DateTime(2021, 9, 1));

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-12505

        [TestProperty("JiraIssueID", "CDV6-12727")]
        [Description("Pre-Requisite -Person should have atleast 1 case and Adult Safeguarding Record." + "Open existing person Allegation Abuser record" +
            "Navigate to Allegation Investigators and Enter the Date started greater than the Allegation Date- Validate the pop up message")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Allegations_AllegedAbuser_UITestMethod01()

        {
            var allegationDate = DateTime.Now.AddDays(-5);
            var dateStarted = DateTime.Now.AddDays(-10);

            #region Allegation Absuer Record

            Guid AllegationAbsuerRecord = dbHelper.allegation.CreateAllegation(_safeguardingID, _careDirectorQA_TeamId, _personId2, "person", _person_fullName2, _personId, "person", _person_fullName, _personId, _allegationCategoryId, allegationDate);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAllegedAbuserPage();

            allegedAbuserPage
                .WaitForAllegedAbuserPageToLoad()
                .OpenAllegationAbuserRecord(AllegationAbsuerRecord.ToString());

            allegedAbsuserRecordPage
                .WaitForAllegedAbsuserRecordPageToLoad("Allegation for " + _person_fullName + " created by " + _defaultUserFullname + " on")
                .NavigateAllegationInvestigatorsSubpage();

            allegationInvestigatorPage
                .WaitForAllegationInvestigatorPageToLoad()
                .ClickNewRecordButton();

            allegationInvestigatorRecordPage
                .WaitForAllegationInvestigatorRecordPageToLoad("New")
                .InsertDateStarted(dateStarted.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Date Started must be after Allegation Date on the linked Allegation");

        }

        [TestProperty("JiraIssueID", "CDV6-12728")]
        [Description("Pre-Requisite -Person should have atleast 1 case and Adult Safeguarding Record." + "Open existing person Allegation Abuser record" +
            "Navigate to Allegation Investigators and Enter the Date ended greater than the Date started- Validate the pop up message")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Allegations_AllegedAbuser_UITestMethod02()
        {
            var allegationDate = DateTime.Now.AddDays(-15);
            var dateStarted = DateTime.Now.AddDays(-5);
            var dateEnded = DateTime.Now.AddDays(-10);

            #region Allegation Absuer Record

            Guid AllegationAbsuerRecord = dbHelper.allegation.CreateAllegation(_safeguardingID, _careDirectorQA_TeamId, _personId2, "person", _person_fullName2, _personId, "person", _person_fullName, _personId, _allegationCategoryId, allegationDate);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAllegedAbuserPage();

            allegedAbuserPage
                .WaitForAllegedAbuserPageToLoad()
                .OpenAllegationAbuserRecord(AllegationAbsuerRecord.ToString());

            allegedAbsuserRecordPage
                .WaitForAllegedAbsuserRecordPageToLoad("Allegation for " + _person_fullName + " created by " + _defaultUserFullname + " on ")
                .NavigateAllegationInvestigatorsSubpage();

            allegationInvestigatorPage
                .WaitForAllegationInvestigatorPageToLoad()
                .ClickNewRecordButton();

            allegationInvestigatorRecordPage
                .WaitForAllegationInvestigatorRecordPageToLoad("New")
                .InsertDateStarted(dateStarted.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertDateEnded(dateEnded.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Date Ended must be after Date Started");

        }

        [TestProperty("JiraIssueID", "CDV6-12729")]
        [Description("Pre-Requisite -Person should have atleast 1 case and Adult Safeguarding Record." + "Open existing person Allegation Abuser record" +
            "Navigate to Allegation Investigators and Enter all the fields and save the record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Allegations_AllegedAbuser_UITestMethod03()
        {
            var allegationDate = DateTime.Now.AddDays(-15);
            var dateStarted = DateTime.Now.AddDays(-5);
            var dateEnded = DateTime.Now.Date;

            #region Allegation Absuer Record

            Guid AllegationAbsuerRecord = dbHelper.allegation.CreateAllegation(_safeguardingID, _careDirectorQA_TeamId, _personId2, "person", _person_fullName2, _personId, "person", _person_fullName, _personId, _allegationCategoryId, allegationDate);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAllegedAbuserPage();

            allegedAbuserPage
                .WaitForAllegedAbuserPageToLoad()
                .OpenAllegationAbuserRecord(AllegationAbsuerRecord.ToString());

            allegedAbsuserRecordPage
                .WaitForAllegedAbsuserRecordPageToLoad("Allegation for " + _person_fullName + " created by " + _defaultUserFullname + " on ")
                .NavigateAllegationInvestigatorsSubpage();

            allegationInvestigatorPage
                .WaitForAllegationInvestigatorPageToLoad()
                .ClickNewRecordButton();

            allegationInvestigatorRecordPage
                .WaitForAllegationInvestigatorRecordPageToLoad("New")
                .InsertDateStarted(dateStarted.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertDateEnded(dateEnded.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectInvestigator("2")
                .ClickSaveAndCloseButton();

            allegationInvestigatorPage
                .WaitForAllegationInvestigatorPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var investigators = dbHelper.allegationInvestigator.GetAllegationInvestigatorByAllegationID(AllegationAbsuerRecord);
            Assert.AreEqual(1, investigators.Count);
            var allegationInvestigators = investigators.FirstOrDefault();

            var allegationInvestigatorsFields = dbHelper.allegationInvestigator.GetAllegationInvestigatorByID(allegationInvestigators, "startdate", "enddate", "investigatorid", "ownerid", "responsibleuserid");

            Assert.AreEqual(dateStarted.Date, allegationInvestigatorsFields["startdate"]);
            Assert.AreEqual(dateEnded.Date, allegationInvestigatorsFields["enddate"]);
            Assert.AreEqual(dateEnded.Date, allegationInvestigatorsFields["enddate"]);
            Assert.AreEqual(2, allegationInvestigatorsFields["investigatorid"]);
            Assert.AreEqual(_careDirectorQA_TeamId.ToString(), allegationInvestigatorsFields["ownerid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), allegationInvestigatorsFields["responsibleuserid"].ToString());

        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        #endregion
    }
}
