using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class Person_AlertAndHazards_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUserName;
        private string _systemUserFullName;
        private Guid _personId;
        private int _personNumber;
        private string _person_fullName;
        private Guid _alertAndHazardsType;
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

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

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

                _systemUserName = "Person_AlertAndHazards_User1";
                commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person AlertAndHazards", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                _systemUserFullName = "Person AlertAndHazards User1";

                #endregion

                #region Person

                var firstName = "First";
                var lastName = "LN_" + _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _person_fullName = firstName + " " + lastName;

                #endregion

                #region Alert And Hazard Type

                _alertAndHazardsType = commonMethodsDB.CreateAlertAndHazardType(_careDirectorQA_TeamId, "Dangerous Dog", new DateTime(2019, 2, 26));

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-11948

        [TestProperty("JiraIssueID", "CDV6-11955")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Click on Add - Save and close the Alert and Hazards- Validate the message displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod01()
        {
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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ClickSaveButton()
                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateRoleMessageArea(true)
                .ValidateRoleMessageAreaText("Please fill out this field.")
                .ValidateAlertHazardTypeMessageArea(true)
                .ValidateAlertHazardTypeMessageAreaText("Please fill out this field.")
                .ValidateStartDateMessageArea(true)
                .ValidateStartDateMessageAreaText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-11956")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Click on Add- Enter the future date -Validate the message displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod02()
        {
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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .InsertFutureEndDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("End Date cannot be a future date.");

        }

        [TestProperty("JiraIssueID", "CDV6-11957")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Click on Add- Enter the future date lesser than the start date -Validate the message displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod03()
        {
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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .InsertStartDate("02/08/2021")
                .InsertFutureEndDate("01/08/2021")
                .ClickSaveButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("End Date cannot be prior to Start Date.");

        }

        [TestProperty("JiraIssueID", "CDV6-11958")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Click on Add- Enter all the mandatory details and save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod04()
        {
            var startdate = DateTime.Now.Date;

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .SelectRole("1")
                .InsertStartDate(startdate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickAlertAndHazardsLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Dangerous Dog").TapSearchButton().SelectResultElement(_alertAndHazardsType.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad();

            var alertAndHazard = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(_personId);
            Assert.AreEqual(1, alertAndHazard.Count);


            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            var alertAndHazardFields = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(alertAndHazard[0], "personid", "ownerid", "roleid", "reviewfrequencytypeid",
                "alertandhazardtypeid", "alertandhazardendreasonid", "startdate", "enddate", "reviewdate", "details");

            Assert.AreEqual(1, alertAndHazardFields["roleid"]);
            Assert.AreEqual(_alertAndHazardsType.ToString(), alertAndHazardFields["alertandhazardtypeid"].ToString());
            Assert.AreEqual(startdate, alertAndHazardFields["startdate"]);

        }

        [TestProperty("JiraIssueID", "CDV6-11959")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Open the Record and update any field- Validate the update")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod05()
        {
            var startDate = DateTime.Now.Date;
            var reviewDate = DateTime.Now.Date;

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .SelectRole("1")
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickAlertAndHazardsLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Dangerous Dog").TapSearchButton().SelectResultElement(_alertAndHazardsType.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .InsertDetails("Alerts and Hazards Details:")
                .SelectReviewFrequency("5")
                .InsertReviewDate(reviewDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad();

            var alertAndHazard = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(_personId);
            Assert.AreEqual(1, alertAndHazard.Count);

            var alertAndHazardFields = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(alertAndHazard[0], "personid", "ownerid", "roleid", "reviewfrequencytypeid",
                "alertandhazardtypeid", "alertandhazardendreasonid", "startdate", "enddate", "reviewdate", "details");

            Assert.AreEqual(1, alertAndHazardFields["roleid"]);
            Assert.AreEqual(_alertAndHazardsType.ToString(), alertAndHazardFields["alertandhazardtypeid"].ToString());
            Assert.AreEqual(startDate, alertAndHazardFields["startdate"]);
            Assert.AreEqual("Alerts and Hazards Details:", alertAndHazardFields["details"]);
            Assert.AreEqual(reviewDate, alertAndHazardFields["reviewdate"]);
            Assert.AreEqual("5", alertAndHazardFields["reviewfrequencytypeid"].ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .InsertDetails("Alerts and Hazards Details: Updated")
                .ClickSaveButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad();

            var alertAndHazardUpdated = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(_personId);
            Assert.AreEqual(1, alertAndHazardUpdated.Count);

            var alertAndHazardFieldsUpdated = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(alertAndHazardUpdated[0], "personid", "ownerid", "roleid", "reviewfrequencytypeid",
                "alertandhazardtypeid", "alertandhazardendreasonid", "startdate", "enddate", "reviewdate", "details");

            Assert.AreEqual(1, alertAndHazardFieldsUpdated["roleid"]);
            Assert.AreEqual(_alertAndHazardsType.ToString(), alertAndHazardFieldsUpdated["alertandhazardtypeid"].ToString());
            Assert.AreEqual(startDate, alertAndHazardFieldsUpdated["startdate"]);
            Assert.AreEqual("Alerts and Hazards Details: Updated", alertAndHazardFieldsUpdated["details"]);
            Assert.AreEqual(reviewDate, alertAndHazardFieldsUpdated["reviewdate"]);
            Assert.AreEqual("5", alertAndHazardFields["reviewfrequencytypeid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-12059")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Open the Record and click restrict to access button and validate")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod06()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Data Deny Restriction

            var dataDenyRestriction = commonMethodsDB.CreateDataRestrictionRecord(new Guid("06e46468-7846-ea11-a2c8-005056926fe4"), "DT Deny data restriction", 2, _careDirectorQA_TeamId);

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ClickRestrictAccessRecord();

            restrictPersonAlertAndHazardPopup.WaitForRestrictPersonAlertAndHazardPopupToLoad().RestrictionTypeTapSearchButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Data Restrictions Deny User access to this data")
                .TypeSearchQuery("DT Deny data restriction")
                .TapSearchButton()
                .SelectResultElement(dataDenyRestriction.ToString());

            restrictPersonAlertAndHazardPopup.TapOkButton();
            restrictPersonAlertAndHazardPopup.validateSuccessfullMessage("Record/s restricted successfully").TapOkButton();

            var alertAndHazard = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(_personId);
            Assert.AreEqual(1, alertAndHazard.Count);

            var alertAndHazardFields = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(alertAndHazard[0], "personid", "ownerid", "roleid", "reviewfrequencytypeid",
                "alertandhazardtypeid", "alertandhazardendreasonid", "startdate", "enddate", "reviewdate", "details", "datarestrictionid");

            Assert.AreEqual(dataDenyRestriction.ToString(), alertAndHazardFields["datarestrictionid"].ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11958")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Click on Add- Enter all the mandatory details, with End date as null and save the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod07()
        {
            var startDate = DateTime.Now.Date;

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .SelectRole("1")
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickAlertAndHazardsLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Dangerous Dog")
                .TapSearchButton()
                .SelectResultElement(_alertAndHazardsType.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ClickSaveButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad();

            var alertAndHazard = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(_personId);
            Assert.AreEqual(1, alertAndHazard.Count);

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ValidateEndDate("");

            var alertAndHazardFields = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByID(alertAndHazard[0], "personid", "ownerid", "roleid", "reviewfrequencytypeid",
                "alertandhazardtypeid", "alertandhazardendreasonid", "startdate", "enddate", "reviewdate", "details");

            Assert.AreEqual(1, alertAndHazardFields["roleid"]);
            Assert.AreEqual(_alertAndHazardsType.ToString(), alertAndHazardFields["alertandhazardtypeid"].ToString());
            Assert.AreEqual(startDate, alertAndHazardFields["startdate"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12062")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Delete the record and Validate")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod08()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .SelectPersonAlertAndHazardRecord(personAlertAndHazards.ToString())
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup.WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad();

            var alertAndHazard = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(_personId);
            Assert.AreEqual(0, alertAndHazard.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12063")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Open the Record and update any field- Validate the update in Audit")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod09()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .SelectRole("2")
                .ClickSaveButton()
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .NavigateToAuditSubPage();

            auditListPage
                .WaitForAuditListPageToLoad("personalertandhazard");

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "1",
                IsGeneralAuditSearch = false,
                Operation = 2,
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                UsePaging = true,
                ParentId = personAlertAndHazards.ToString(),
                ParentTypeName = "personalertandhazard",
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Update", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual(_systemUserFullName, auditResponseData.GridData[0].cols[1].Text);

        }

        [TestProperty("JiraIssueID", "CDV6-12067")]
        [Description("Open a person Alert and Hazard - validate Export to Excel ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod10()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .SelectPersonAlertAndHazardRecord(personAlertAndHazards.ToString())
                .ClickExportToExcelButton();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Selected Records")
                .SelectExportFormat("Csv (comma separated with quotes)")
                .ClickExportButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "PersonAlertsAndHazards.csv");
            Assert.IsTrue(fileExists);

        }

        [TestProperty("JiraIssueID", "CDV6-12068")]
        [Description("Open a person Alert and Hazard - Assign ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod11()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Team Manager (System User)

            var _teamManagerUserName = "Person.AlertHazard";
            var _teamManagerId = commonMethodsDB.CreateSystemUserRecord(_teamManagerUserName, "Person", "AlertHazard (Advanced)", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            #region Team

            var advancedId = commonMethodsDB.CreateTeam("Advanced", _teamManagerId, _careDirectorQA_BusinessUnitId, "", "oneadvanced@randommail.com", "Advanced", "");

            #endregion

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .SelectPersonAlertAndHazardRecord(personAlertAndHazards.ToString())
                .ClickAssignButton();

            assignRecordPopup.WaitForAssignRecordAlertAndHazardPopupToLoad().ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("Advanced")
                .TapSearchButton().SelectResultElement(advancedId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAlertAndHazardsPage();

        }

        [TestProperty("JiraIssueID", "CDV6-12064")]
        [Description("Open a person Alert and Hazard - Update the Review date-Open the Alert and Hazard reviews record -" +
            "Open Alert and Hazard Review " +
            " Validate the Review date is null in Alert and Hazard reviews record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod12()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .InsertReviewDate("20/5/2021")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .NavigateToAlertAndHazardReviewSubpage();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .ValidateReviewDateFieldValueVisible("");

        }

        [TestProperty("JiraIssueID", "CDV6-12065")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Open the Record and update End date and validate the Mandatory End Reason field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod13()
        {
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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .SelectRole("1")
                .InsertStartDate("02/08/2021")
                .ClickAlertAndHazardsLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Dangerous Dog").TapSearchButton().SelectResultElement(_alertAndHazardsType.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .InsertEndDate("12/08/2021")
                .ClickSaveButton()
                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateAlertHazardEndReasonArea(true)
                .ValidateAlertHazardEndReasonAreaText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-12066")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Open the Record and click restrict to access button and validate")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod14()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad();

            var alertAndHazard = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(_personId);
            Assert.AreEqual(1, alertAndHazard.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12123")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Open the Record and Delete the record and validate")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod15()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var alertAndHazard = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(_personId);
            Assert.AreEqual(0, alertAndHazard.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12061")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Open the Record - Create a Alerts and Hazards review record save and close - Close the Alerts and Hazards " +
            "Review -Delete the record - User should recieve a pop up message ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod16()
        {
            var startDate = new DateTime(2021, 8, 20);

            #region Create System User Record 2

            var reviewCompletedBy = commonMethodsDB.CreateSystemUserRecord("Person_AlertAndHazards_User2", "Person AlertAndHazards", "User2", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            var reviewCompletedByUserFullName = "Person AlertAndHazards User2";

            #endregion

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazardId = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazardId.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .NavigateToAlertAndHazardReviewSubpage();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .InsertPlannedReviewDate("21/8/2021")
                .ClickReviewCompletedByLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery(reviewCompletedByUserFullName.ToString())
                .TapSearchButton()
                .SelectResultElement(reviewCompletedBy.ToString());

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);
            var reviewIds = dbHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByAlertAndHazardID(personAlertAndHazardId);
            Assert.AreEqual(1, reviewIds.Count);

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ClickDetailsTab()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Related record exists in Person Alert And Hazard Review. Please delete related records before deleting record in Person Alert And Hazard.");

        }

        [TestProperty("JiraIssueID", "CDV6-12160")]
        [Description("Check PPRC Inactive status is Yes(settings-Configuration-Module)- Create Alert and Hazard record with Start date as previous date, End date as equal or Future date and PPRC type as Yes-Save the Record and validate the Record in Summary Alert Section")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_AlertAndHazards_UITestMethod17()
        {
            var personID = new Guid("42341e8a-0c03-435d-a302-af94db202ca8"); //Kristine Pollard
            Guid pprcInformationId = new Guid("fe089cdf-93cd-ea11-a2cc-0050569231cf");//PPRC Information
            var personNumber = "300932";
            var alertAndHazardsType = new Guid("d35a41d9-51d5-ea11-a2cd-005056926fe4");//PPRC alert


            Guid summaryDashBoardID = new Guid("1a884fe3-2170-eb11-a309-005056926fe4");//Alerts

            //remove Alert and Hazards related to the person
            foreach (var recordid in dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
            {
                foreach (var reviewRecordId in dbHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByAlertAndHazardID(recordid))
                {
                    dbHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewRecordId);
                }
                dbHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordid);
            }


            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickModulesButton();

            businessModulesChecklistPage
                .WaitForBusinessModulesChecklistPageToLoad()
                .ValidateModuleCheckBox(pprcInformationId.ToString(), true)
                .ValidateModuleLabel(pprcInformationId.ToString(), "PPRC Information");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .SelectRole("1")
                .InsertStartDate("02/08/2021")
                .InsertDetails("PPRC Alert")
                .ClickAlertAndHazardsLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("PPRC alert").TapSearchButton().SelectResultElement(alertAndHazardsType.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad();

            var alertAndHazard = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID);
            Assert.AreEqual(1, alertAndHazard.Count);

            dbHelper.businessObjectDashboard.UpdateBusinessObjectDashboard(summaryDashBoardID, true, 120);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapBackButton();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickSearchButton()
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapSummaryTab()
                .WaitForSummaryTabToLoad()
                .SelectDashboard("Alerts");

            personRecordPage_SummaryArea
                .WaitForPersonRecordPage_SummaryAreaToLoad()
                .ValidatePPRCMessage("PPRC Alert");

        }

        [TestProperty("JiraIssueID", "CDV6-12161")]
        [Description("Verify the Person Record is Associated with the another person Record in Relationships and Ensure the Inside Household is Yes")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod18()
        {
            var startDate = new DateTime(2021, 5, 20);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Related Person

            var _relatedPersonId = commonMethodsDB.CreatePersonRecord("Related", "LN_" + _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
            var _relatedPerson_fullName = "Related LN_" + _currentDateSuffix;

            #endregion

            #region Relationship Type

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            #endregion

            #region Person Alert And Hazards Record

            dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);
            dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 002", _careDirectorQA_TeamId, "CareDirector QA", _relatedPersonId, _relatedPerson_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);

            #endregion

            #region Person Relationship

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, _person_fullName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                _relatedPerson_fullName, relationshipTypeId, relationshipTypeName,
                new DateTime(2023, 1, 1), "Person Relationship Created", 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);

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
                .NavigateToRelationshipsPage()
                .ValidateNoRecordLabelVisibile(false)
                .ValidatePersonRelatedAlertsAndHazards_Icon(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12162")]
        [Description("Verify that recent details are displayed in the Person timeline upon creating or updating the Person alerts and Hazards record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod19()
        {
            var startDate = new DateTime(2021, 5, 20);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);

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
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ValidatePersonAlertRecord(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12165")]
        [Description("Create Active person record-Select Alert and Hazards under Related Items- Click on Add- Enter all the mandatory details and save the record- Validate the Alerts/Hazards recorded button")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod20()
        {
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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .SelectRole("1")
                .InsertStartDate("02/08/2021")
                .ClickAlertAndHazardsLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Dangerous Dog").TapSearchButton().SelectResultElement(_alertAndHazardsType.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ClickSaveButton()
                .ValidateAlertHazardRecordedButton(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12166")]
        [Description("Verify the Alert and Hazards recorded button for the inactive Record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod21()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ValidateAlertHazardRecordedButton(false);

        }

        [TestProperty("JiraIssueID", "CDV6-12167")]
        [Description("Verify the Alert and Hazards relationship button for the person record when Inside household-Yes, and End date is Null ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod22()
        {
            var startDate = new DateTime(2021, 5, 20);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Related Person

            var _relatedPersonId = commonMethodsDB.CreatePersonRecord("Related", "LN_" + _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
            var _relatedPerson_fullName = "Related LN_" + _currentDateSuffix;

            #endregion

            #region Relationship Type

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            #endregion

            #region Person Alert And Hazards Record

            dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);
            dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 002", _careDirectorQA_TeamId, "CareDirector QA", _relatedPersonId, _relatedPerson_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);

            #endregion

            #region Person Relationship

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, _person_fullName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                _relatedPerson_fullName, relationshipTypeId, relationshipTypeName,
                new DateTime(2023, 1, 1), "Person Relationship Created", 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);

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
                .ValidatePersonRelatedAlertsAndHazards_Icon(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12169")]
        [Description("Open  person Alert and Hazards - Click Menu and Alerts and Hazards Review - Click new record button and click save button- Validate the " +
            "Mandatory fields error message")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod23()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .NavigateToAlertAndHazardReviewSubpage();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .ClickSaveButton()
                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidatePlannedReviewDateMessageArea(true)
                .ValidatePlannedReviewDateMessageAreaText("Please fill out this field.")
                .ValidateReviewCompletedByMessageArea(true)
                .ValidateReviewCompletedByMessageAreaText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-12171")]
        [Description("Open  person Alert and Hazards - Click Menu and Alerts and Hazards Review - Click new record button - Enter the Review Date Prior to Planned Review Date")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod24()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Create System User Record 2

            var reviewCompletedBy = commonMethodsDB.CreateSystemUserRecord("Person_AlertAndHazards_User2", "Person AlertAndHazards", "User2", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            var reviewCompletedByUserFullName = "Person AlertAndHazards User2";

            #endregion

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .NavigateToAlertAndHazardReviewSubpage();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .InsertPlannedReviewDate("21/8/2021")
                .ClickReviewCompletedByLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(reviewCompletedByUserFullName.ToString())
                .TapSearchButton().SelectResultElement(reviewCompletedBy.ToString());

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .InsertReviewDate("02/8/2021")
                .ClickReviewOutcomeFieldLookUp();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Review Date cannot be prior to Planned Review Date");

        }

        [TestProperty("JiraIssueID", "CDV6-12172")]
        [Description("Open  person Alert and Hazards - Click Menu and Alerts and Hazards Review - Click new record button - Enter the Future Review Date to Planned Review Date")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod25()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Create System User Record 2

            var reviewCompletedBy = commonMethodsDB.CreateSystemUserRecord("Person_AlertAndHazards_User2", "Person AlertAndHazards", "User2", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            var reviewCompletedByUserFullName = "Person AlertAndHazards User2";

            #endregion

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .NavigateToAlertAndHazardReviewSubpage();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .InsertPlannedReviewDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ClickReviewCompletedByLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(reviewCompletedByUserFullName.ToString())
                .TapSearchButton().SelectResultElement(reviewCompletedBy.ToString());

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .InsertReviewDate(DateTime.Now.Date.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickReviewOutcomeFieldLookUp();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Review Date cannot be in the future");

        }

        [TestProperty("JiraIssueID", "CDV6-12173")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Open the Record - Create a Alerts and Hazards review record save and close")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod26()
        {
            var startDate = new DateTime(2021, 8, 20);
            var plannedReviewDate = new DateTime(2021, 8, 21);
            var reviewDate = new DateTime(2021, 8, 22);

            #region Create System User Record 2

            var reviewCompletedBy = commonMethodsDB.CreateSystemUserRecord("Person_AlertAndHazards_User2", "Person AlertAndHazards", "User2", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            var reviewCompletedByUserFullName = "Person AlertAndHazards User2";

            #endregion

            #region Alert And Hazard Review Outcome

            var reviewOutcome = commonMethodsDB.CreateAlertAndHazardReviewOutcome(_careDirectorQA_TeamId, "Outcome_1", new DateTime(2019, 6, 1));

            #endregion

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .NavigateToAlertAndHazardReviewSubpage();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .InsertPlannedReviewDate(plannedReviewDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertReviewDate(reviewDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertSummary("Alert and Hazard Review")
                .InsertPersonView("Alert and Hazard person view")
                .InsertProfessionalView("Alert and Hazard Professional view")
                .ClickReviewCompletedByLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(reviewCompletedByUserFullName.ToString())
                .TapSearchButton().SelectResultElement(reviewCompletedBy.ToString());

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .ClickReviewOutcomeFieldLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records")
                .TypeSearchQuery("Outcome_1").TapSearchButton().SelectResultElement(reviewOutcome.ToString());

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .ClickSaveAndCloseButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad();

            var reviewIds = dbHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByAlertAndHazardID(personAlertAndHazards);
            Assert.AreEqual(1, reviewIds.Count);

            var alertAndHazardReviewFields = dbHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByID(reviewIds[0], "personid", "ownerid", "plannedreviewdate", "alertandhazardreviewoutcomeid",
              "reviewdate", "reviewcompletedbyid", "personview", "professionalview", "summary");

            Assert.AreEqual(plannedReviewDate, alertAndHazardReviewFields["plannedreviewdate"]);
            Assert.AreEqual(reviewOutcome.ToString(), alertAndHazardReviewFields["alertandhazardreviewoutcomeid"].ToString());
            Assert.AreEqual(reviewDate, alertAndHazardReviewFields["reviewdate"]);
            Assert.AreEqual(reviewCompletedBy.ToString(), alertAndHazardReviewFields["reviewcompletedbyid"].ToString());
            Assert.AreEqual("Alert and Hazard person view", alertAndHazardReviewFields["personview"]);
            Assert.AreEqual("Alert and Hazard Professional view", alertAndHazardReviewFields["professionalview"]);
            Assert.AreEqual("Alert and Hazard Review", alertAndHazardReviewFields["summary"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12174")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Open the Record - Create a Alerts and Hazards review record save and close and update the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod27()
        {
            var startDate = new DateTime(2021, 8, 20);

            #region Create System User Record 2

            var reviewCompletedBy = commonMethodsDB.CreateSystemUserRecord("Person_AlertAndHazards_User2", "Person AlertAndHazards", "User2", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            var reviewCompletedByUserFullName = "Person AlertAndHazards User2";

            #endregion

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .NavigateToAlertAndHazardReviewSubpage();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .InsertPlannedReviewDate("21/8/2021")
                .ClickReviewCompletedByLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(reviewCompletedByUserFullName.ToString())
                .TapSearchButton().SelectResultElement(reviewCompletedBy.ToString());

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .ClickSaveButton()
                .InsertSummary("Person Alert And Hazard Testing")
                .ClickSaveAndCloseButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad();

            var reviewIds = dbHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByAlertAndHazardID(personAlertAndHazards);
            Assert.AreEqual(1, reviewIds.Count);
            var alertAndHazardReview = reviewIds.FirstOrDefault();

            var alertAndHazardsFields = dbHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByID(alertAndHazardReview, "summary");
            Assert.AreEqual("Person Alert And Hazard Testing", alertAndHazardsFields["summary"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12296")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Open the Record - Create a Alerts and Hazards review record save and close and Delete the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod28()
        {
            var startDate = new DateTime(2021, 8, 20);

            #region Create System User Record 2

            var reviewCompletedBy = commonMethodsDB.CreateSystemUserRecord("Person_AlertAndHazards_User2", "Person AlertAndHazards", "User2", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            var reviewCompletedByUserFullName = "Person AlertAndHazards User2";

            #endregion

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .NavigateToAlertAndHazardReviewSubpage();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .InsertPlannedReviewDate("21/8/2021")
                .ClickReviewCompletedByLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(reviewCompletedByUserFullName.ToString())
                .TapSearchButton().SelectResultElement(reviewCompletedBy.ToString());

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .ClickSaveAndCloseButton();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad();

            var reviewIds = dbHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByAlertAndHazardID(personAlertAndHazards);
            Assert.AreEqual(1, reviewIds.Count);
            var alertAndHazardReview = reviewIds.FirstOrDefault();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad()
                .SelectPersonAlertAndHazardReviewRecord(alertAndHazardReview.ToString())
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad();

            var reviewIDs = dbHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByAlertAndHazardID(personAlertAndHazards);
            Assert.AreEqual(0, reviewIDs.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12297")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Open the Record - Create a Alerts and Hazards review record save and close and update the record- Validate the Timeline")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod29()
        {
            var startDate = new DateTime(2021, 8, 20);

            #region Create System User Record 2

            var reviewCompletedBy = commonMethodsDB.CreateSystemUserRecord("Person_AlertAndHazards_User2", "Person AlertAndHazards", "User2", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            var reviewCompletedByUserFullName = "Person AlertAndHazards User2";

            #endregion

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .NavigateToAlertAndHazardReviewSubpage();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .InsertPlannedReviewDate("21/8/2021")
                .ClickReviewCompletedByLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(reviewCompletedByUserFullName.ToString())
                .TapSearchButton().SelectResultElement(reviewCompletedBy.ToString());

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .ClickSaveButton()
                .InsertSummary("Person Alert And Hazard Testing")
                .ClickSaveAndCloseButton();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ClickBackButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ValidatePersonAlertReviewRecord(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12314")]
        [Description("Create Record through Advanced Search")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod30()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Person Alerts And Hazards")
                .SelectFilter("1", "Person")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertAndHazardRecordPageToLoadFromAdvanceSearch("New")
                .ClickPersonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertAndHazardRecordPageToLoadFromAdvanceSearch("New")
                .SelectRole("1")
                .InsertStartDate("02/08/2021")
                .ClickAlertAndHazardsLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Dangerous Dog").TapSearchButton().SelectResultElement(_alertAndHazardsType.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertAndHazardRecordPageToLoadFromAdvanceSearch("New")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            var records = dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(_personId);
            Assert.AreEqual(1, records.Count);

        }

        ////Sub bug in Alert and Hazards Review record creation through Advanced Search , Task Created and the Id is  CDV6-12474
        //[TestProperty("JiraIssueID", "CDV6-12317")]
        //[Description("Create Record through Advanced Search")]
        //[TestMethod]
        //[TestCategory("UITest")]
        //public void Person_AlertAndHazards_UITestMethod31()
        //{
        //    var systemUserID = new Guid("c074c7a5-74a9-e911-a2c6-005056926fe4"); //CW_Forms_Test_User_1
        //    var personID = new Guid("42341e8a-0c03-435d-a302-af94db202ca8"); //Kristine Pollard
        //    var personNumber = "300932";
        //    var alertAndHazardsType = new Guid("95499ddb-d139-e911-a2c5-005056926fe4");//Dangerous Dog
        //    Guid ownerID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5");//CareDirector QA
        //    var alertAndHazardsEndReason = new Guid("2f979679-5097-e911-a2c6-005056926fe4");//End Reason_1
        //    var startDate = new DateTime(2021, 8, 20);

        //    //remove Alert and Hazards related to the person
        //    foreach (var recordid in dbHelper.personAlertAndHazard.GetPersonAlertAndHazardByPersonID(personID))
        //    {
        //        foreach (var reviewRecordId in dbHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByAlertAndHazardID(recordid))
        //        {
        //            dbHelper.personAlertAndHazardReview.DeletePersonAlertAndHazardReview(reviewRecordId);
        //        }
        //        dbHelper.personAlertAndHazard.DeletePersonAlertAndHazard(recordid);
        //    }


        //    var personAlertAndHazardId = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", ownerID, "CareDirector QA", personID, "Kristine Pollard", alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);

        //    loginPage
        //      .GoToLoginPage()
        //      .Login("CW_Forms_Test_User_1", "Passw0rd_!")
        //      .WaitFormHomePageToLoad();

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .ClickAdvancedSearchButton();

        //    advanceSearchPage
        //        .WaitForAdvanceSearchPageToLoad()
        //        .SelectRecordType("Person Alert And Hazard Reviews")
        //        .SelectFilter("1", "Person")
        //        .SelectOperator("1", "Equals")
        //        .ClickRuleValueLookupButton("1");

        //    lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("All Active People").TypeSearchQuery(personNumber).TapSearchButton().SelectResultElement(personID.ToString());

        //    advanceSearchPage
        //        .WaitForAdvanceSearchPageToLoad()
        //        .ClickSearchButton()
        //        .WaitForResultsPageToLoad()
        //        .ClickNewRecordButton_ResultsPage();

        //    personAlertAndHazardReviewRecordPage
        //        .WaitForPersonAlertAndHazardReviewsFromAdvancedSearchPageToLoad()
        //        .ClickPersonAlertAndHazardIDLookUp();

        //    lookupPopup
        //        .WaitForLookupPopupToLoad()
        //        .SelectLookIn("Lookup Records")
        //        .TypeSearchQuery(startDate.ToString("dd/MM/yyyy"))
        //        .TapSearchButton(60)
        //        .SelectResultElement(personAlertAndHazardId.ToString());

        //    personAlertAndHazardReviewRecordPage
        //         .WaitForPersonAlertAndHazardReviewsFromAdvancedSearchPageToLoad()
        //         .ClickReviewCompletedByLookUp();

        //    lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("CW_Forms_Test_User_1").TapSearchButton().SelectResultElement(systemUserID.ToString());

        //    personAlertAndHazardReviewRecordPage
        //        .WaitForPersonAlertAndHazardReviewsFromAdvancedSearchPageToLoad()
        //        .InsertPlannedReviewDate(DateTime.Now.ToString("dd/MM/yyyy"))
        //        .ClickSaveAndCloseButton();

        //    advanceSearchPage
        //       .WaitForResultsPageToLoad();

        //    System.Threading.Thread.Sleep(1000);

        //    var records = dbHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByAlertAndHazardID(personAlertAndHazardId);
        //    Assert.AreEqual(1, records.Count);
        //    var personAlertAndHazardReview = records.FirstOrDefault();

        //    advanceSearchPage
        //        .WaitForResultsPageToLoad()
        //        .ValidateSearchResultRecordPresent(personAlertAndHazardReview.ToString());
        //}

        [TestProperty("JiraIssueID", "CDV6-12168")]
        [Description("Verify the Alert and Hazards relationship button for the active Person relationship record must exist and associated to the person record" +
          "Inside household-Yes, and End date is not Null ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod32()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Related Person

            var _relatedPersonId = commonMethodsDB.CreatePersonRecord("Related", "LN_" + _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
            var _relatedPerson_fullName = "Related LN_" + _currentDateSuffix;

            #endregion

            #region Relationship Type

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            #endregion

            #region Person Alert And Hazards Record

            dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);
            dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 002", _careDirectorQA_TeamId, "CareDirector QA", _relatedPersonId, _relatedPerson_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

            #endregion

            #region Person Relationship

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, _person_fullName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                _relatedPerson_fullName, relationshipTypeId, relationshipTypeName,
                new DateTime(2023, 1, 1), "Person Relationship Created", 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);

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
                .ValidatePersonRelatedAlertsAndHazards_Icon(false);

        }

        [TestProperty("JiraIssueID", "CDV6-12448")]
        [Description("Verify the Alert and Hazards relationship button for the active Person relationship record must exist and associated to the person record" +
         "Inside household-No, and End date is  Null ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod33()
        {
            var startDate = new DateTime(2021, 5, 20);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Related Person

            var _relatedPersonId = commonMethodsDB.CreatePersonRecord("Related", "LN_" + _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
            var _relatedPerson_fullName = "Related LN_" + _currentDateSuffix;

            #endregion

            #region Relationship Type

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);
            dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 002", _careDirectorQA_TeamId, "CareDirector QA", _relatedPersonId, _relatedPerson_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);

            #endregion

            #region Person Relationship

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, _person_fullName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                _relatedPerson_fullName, relationshipTypeId, relationshipTypeName,
                new DateTime(2023, 1, 1), "Person Relationship Created", 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);

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
                .ValidatePersonRelatedAlertsAndHazards_Icon(false)
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ValidateRelatedPersonAlertHazardButton(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12449")]
        [Description("Verify the Alert and Hazards relationship button for the active Person relationship record must exist and associated to the person record" +
        "Inside household-No, and End date is not  Null ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod34()
        {
            var startDate = new DateTime(2021, 5, 20);
            var endDate = new DateTime(2021, 6, 25);

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Related Person

            var _relatedPersonId = commonMethodsDB.CreatePersonRecord("Related", "LN_" + _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
            var _relatedPerson_fullName = "Related LN_" + _currentDateSuffix;

            #endregion

            #region Relationship Type

            string relationshipTypeName = "Friend";
            var relationshipTypeId = dbHelper.personRelationshipType.GetByName(relationshipTypeName).FirstOrDefault();

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);
            dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 002", _careDirectorQA_TeamId, "CareDirector QA", _relatedPersonId, _relatedPerson_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, endDate, 4);

            #endregion

            #region Person Relationship

            dbHelper.personRelationship.CreatePersonRelationship(_careDirectorQA_TeamId, _personId, _person_fullName, relationshipTypeId, relationshipTypeName, _relatedPersonId,
                _relatedPerson_fullName, relationshipTypeId, relationshipTypeName,
                new DateTime(2023, 1, 1), "Person Relationship Created", 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);

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
                .ValidatePersonRelatedAlertsAndHazards_Icon(false)
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .ValidateRelatedPersonAlertHazardButton(false);

        }

        [TestProperty("JiraIssueID", "CDV6-12492")]
        [Description("Open Active person record-Select Alert and Hazards under Related Items- Open the Record - Create a Alerts and Hazards review record save - Verify the Audit")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AlertAndHazards_UITestMethod35()
        {
            var startDate = new DateTime(2021, 8, 20);
            var plannedReviewDate = new DateTime(2021, 8, 21);
            var reviewDate = new DateTime(2021, 8, 22);

            #region Create System User Record 2

            var reviewCompletedBy = commonMethodsDB.CreateSystemUserRecord("Person_AlertAndHazards_User2", "Person AlertAndHazards", "User2", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            var reviewCompletedByUserFullName = "Person AlertAndHazards User2";

            #endregion

            #region Alert And Hazard Review Outcome

            var reviewOutcome = commonMethodsDB.CreateAlertAndHazardReviewOutcome(_careDirectorQA_TeamId, "Outcome_1", new DateTime(2019, 6, 1));

            #endregion

            #region Alert And Hazards End Reason

            var alertAndHazardsEndReason = commonMethodsDB.CreateAlertAndHazardEndReason(_careDirectorQA_TeamId, "End Reason_1", new DateTime(2019, 6, 1));

            #endregion

            #region Person Alert And Hazards Record

            var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personId, _person_fullName, _alertAndHazardsType, "Dangerous Dog", alertAndHazardsEndReason, "End Reason_1", startDate, null, 4);

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
                .NavigateToPersonAlertAndHazardsPage();

            personAlertAndHazardsPage
                .WaitForPersonAlertAndHazardsPageToLoad()
                .OpenPersonAlertAndHazardsRecord(personAlertAndHazards.ToString());

            personAlertAndHazardsRecordPage
                .WaitForPersonAlertandHazardsRecordPageToLoad()
                .NavigateToAlertAndHazardReviewSubpage();

            personAlertAndHazardReviewPage
                .WaitForPersonAlertAndHazardReviewPageToLoad()
                .ClickNewRecordButton();

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .InsertPlannedReviewDate(plannedReviewDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertReviewDate(reviewDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertSummary("Alert and Hazard Review")
                .InsertPersonView("Alert and Hazard person view")
                .InsertProfessionalView("Alert and Hazard Professional view")
                .ClickReviewCompletedByLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(reviewCompletedByUserFullName.ToString())
                .TapSearchButton().SelectResultElement(reviewCompletedBy.ToString());

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .ClickReviewOutcomeFieldLookUp();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records")
                .TypeSearchQuery("Outcome_1").TapSearchButton().SelectResultElement(reviewOutcome.ToString());

            personAlertAndHazardReviewRecordPage
                .WaitForPersonAlertAndHazardReviewsPageToLoad("New")
                .ClickSaveButton()
                .WaitForPersonAlertAndHazardReviewsPageToLoad("Person Alerts And Hazards Review for " + _person_fullName + " created by " + _systemUserFullName + " on ")
                .InsertSummary("Updated")
                .ClickSaveButton()
                .WaitForPersonAlertAndHazardReviewsPageToLoad("Person Alerts And Hazards Review for " + _person_fullName + " created by " + _systemUserFullName + " on ")
                .NavigateToAuditSubPage();

            auditListPage
                .WaitForAuditListPageToLoad("personalertandhazardreview");

            var reviewIds = dbHelper.personAlertAndHazardReview.GetPersonAlertAndHazardReviewByAlertAndHazardID(personAlertAndHazards);
            Assert.AreEqual(1, reviewIds.Count);

            System.Threading.Thread.Sleep(2000);

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "1",
                IsGeneralAuditSearch = false,
                Operation = 2,
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                UsePaging = true,
                ParentId = reviewIds[0].ToString(),
                ParentTypeName = "personalertandhazardreview",
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual(auditResponseData.GridData[0].cols[0].Text, "Update");
            Assert.AreEqual(auditResponseData.GridData[0].cols[1].Text, _systemUserFullName);
            Assert.AreEqual(auditResponseData.GridData[0].cols[4].Text, "CareDirector");
        }


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }

}








#endregion




