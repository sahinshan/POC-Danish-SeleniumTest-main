using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class Person_PrimarySupportReasons_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _advancedTeamId;
        private string _systemUserName;
        private string _systemUserFullName;
        private Guid _personId;
        private int _personNumber;
        private string _person_fullName;
        private Guid _primarySupportReasonTypeId;
        private string _primarySupportReasonTypeName;
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

                #region Create System User Record

                _systemUserName = "PrimarySupportReasons_User1";
                commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Primary Support Reasons", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                _systemUserFullName = "Primary Support Reasons User1";

                #endregion

                #region Person Record

                var firstName = "First";
                var lastName = "LN_" + _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _person_fullName = firstName + " " + lastName;

                #endregion

                #region Primary Support Reason Type

                _primarySupportReasonTypeName = "child in care";
                _primarySupportReasonTypeId = commonMethodsDB.CreatePrimarySupportReasonType(_careDirectorQA_TeamId, _primarySupportReasonTypeName, new DateTime(2021, 1, 1));

                #endregion

                #region Team Manager (System User)

                var _teamManagerUserName = "alex.smith";
                var _teamManagerId = commonMethodsDB.CreateSystemUserRecord(_teamManagerUserName, "Alex", "Smith (Advanced)", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Team (Advanced)

                _advancedTeamId = commonMethodsDB.CreateTeam("Advanced", _teamManagerId, _careDirectorQA_BusinessUnitId, "", "oneadvanced@randommail.com", "Advanced", "");

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-11949

        [TestProperty("JiraIssueID", "CDV6-12000")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason without mandatory fields" +
                     "-validating Error popup is displayed ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod01()
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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickSaveButton()
                .ValidateNotificationMessage("Some data is not correct. Please review the data in the Form.")
                .ValidatePrimarySupportFieldErrorMessage("Please fill out this field.")
                .ValidatestartDateFieldErrorMessage("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-12001")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with Futhur date in both start and end start " +
                     "-validating Error popup is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod02()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);

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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_primarySupportReasonTypeName.ToString())
                .TapSearchButton()
                .SelectResultElement(_primarySupportReasonTypeId.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate("12/08/2030")
                .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Start Date cannot be in the future")
                .TapOKButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(currentdate)
                .InsertEndDate("13/08/2030")
                .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("End Date cannot be in the future")
                .TapOKButton();

        }

        [TestProperty("JiraIssueID", "CDV6-12005")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with previous date in  start daye and end start is lesser than the start date " +
                     "- validating error message is displayed ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod03()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);

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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_primarySupportReasonTypeName.ToString())
                .TapSearchButton()
                .SelectResultElement(_primarySupportReasonTypeId.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(currentdate)
                .InsertEndDate("11/08/2021")
                .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Start Date cannot be later than End Date")
                .TapOKButton();

        }

        [TestProperty("JiraIssueID", "CDV6-12009")]
        [Description("Testing persons primary support reasons - Saving Primary support reason with all valid details -validating person primary support reason is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod04()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);

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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_primarySupportReasonTypeName.ToString())
                .TapSearchButton()
                .SelectResultElement(_primarySupportReasonTypeId.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(currentdate)
                .ClickSaveAndCloseButton();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

        }

        [TestProperty("JiraIssueID", "CDV6-12012")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with all valid details " +
                     "-validating person primary support reason is created is visible in person Time line page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod05()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//for date formate

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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_primarySupportReasonTypeName.ToString())
                .TapSearchButton()
                .SelectResultElement(_primarySupportReasonTypeId.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(currentdate)
                .ClickSaveAndCloseButton();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var primarySupportReasonRecords = dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(_personId);
            Assert.AreEqual(1, primarySupportReasonRecords.Count);

            personPrimarySupportReasonPage
                .ValidateNoRecordMessageVisibile(false);

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
                .ValidateRecordPresent(primarySupportReasonRecords.FirstOrDefault().ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-12019")]
        [Description("Testing persons primary support reasons" +
                     " - Saving Primary support reason with past date in  start date and end date null  " +
                     "- validating primary support reason is created ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod06()
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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_primarySupportReasonTypeName.ToString())
                .TapSearchButton()
                .SelectResultElement(_primarySupportReasonTypeId.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate("11/08/2021")
                .ClickSaveAndCloseButton();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var primarySupportReasonRecords = dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(_personId);
            Assert.AreEqual(1, primarySupportReasonRecords.Count);

            personPrimarySupportReasonPage
                .ValidateNoRecordMessageVisibile(false);

        }

        [TestProperty("JiraIssueID", "CDV6-12023")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with same date for both start date and end date   " +
                     "- validating primary support reason record is created ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod07()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);

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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_primarySupportReasonTypeName.ToString())
                .TapSearchButton()
                .SelectResultElement(_primarySupportReasonTypeId.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .ClickSaveAndCloseButton();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var primarySupportReasonRecords = dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(_personId);
            Assert.AreEqual(1, primarySupportReasonRecords.Count);

            personPrimarySupportReasonPage
                .ValidateNoRecordMessageVisibile(false);

        }

        [TestProperty("JiraIssueID", "CDV6-12026")]
        [Description("Testing persons primary support reasons - " +
                     "Saving Primary support reason with same date for both start date and end date  " +
                     "- validating primary support reason record is created is visible in audit page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod08()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);

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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_primarySupportReasonTypeName.ToString())
                .TapSearchButton()
                .SelectResultElement(_primarySupportReasonTypeId.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .ClickSaveAndCloseButton();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var primarySupportReasonRecords = dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(_personId);
            Assert.AreEqual(1, primarySupportReasonRecords.Count);

            personPrimarySupportReasonPage
                .OpenPersonPrimarySupportReasonRecord(primarySupportReasonRecords[0].ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad("Primary Support Reason for " + _person_fullName + " created by " + _systemUserFullName + " on ")
                .ClickMenuButton()
                .ClickAuditButton();

            System.Threading.Thread.Sleep(3000);

            auditListPage
                .WaitForAuditListPageToLoad("personprimarysupportreason");

            var auditSearch = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                AllowMultiSelect = "false",
                CurrentPage = "1", //Create operation
                IsGeneralAuditSearch = false,
                Operation = 1,
                PageNumber = 1,
                RecordsPerPage = "100",
                TypeName = "audit",
                UsePaging = true,
                ParentId = primarySupportReasonRecords[0].ToString(),
                ParentTypeName = "personprimarysupportreason",
                ViewGroup = "1",
                ViewType = "0",
                Year = "Last 90 Days",
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual(1, auditResponseData.GridData.Count);
            Assert.AreEqual("Create", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual(_systemUserFullName.ToString(), auditResponseData.GridData[0].cols[1].Text);

        }

        [TestProperty("JiraIssueID", "CDV6-12027")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with past date in  start date and end date null  " +
                     "- validating primary support reason is created ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod09()
        {
            var pastdate = DateTime.Now.AddDays(-05);

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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickNewRecordButton();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_primarySupportReasonTypeName.ToString())
                .TapSearchButton()
                .SelectResultElement(_primarySupportReasonTypeId.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoad()
                .InsertStartDate(pastdate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ClickActivePrimarySupportReasonRelatedViewOption()
                .ValidateNoRecordLabelVisibile(false);

            personPrimarySupportReasonPage
                .ClickInactivePrimarySupportReasonsRelatedViewOtion()
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12028")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with current datefor both start date and end date " +
                     "- validating primary support reason is created can be assigned to other team ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod10()
        {
            DateTime date = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());

            #region Person Primary Support Reason

            Guid Record = dbHelper.personPrimarySupportReason.CreatePersonPrimarySupportReason(_personId, _advancedTeamId, _primarySupportReasonTypeId, date, date);

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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(false)
                .SelectPersonPrimarySupportReasonRecord(Record.ToString())
                .ClickAssignButton();

            assignRecordPopup
                .WaitForAssignRecordPopupForPrimarySupportToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad().SelectViewByText("Lookup View")
                .TypeSearchQuery("CareDirector QA")
                .TapSearchButton()
                .SelectResultElement(_careDirectorQA_TeamId.ToString());

            assignRecordPopup
                .TapOkButton();

            System.Threading.Thread.Sleep(3000);

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateRecordCellText(Record.ToString(), 5, "CareDirector QA");

        }

        [TestProperty("JiraIssueID", "CDV6-12031")]
        [Description("Testing persons primary support reasons " +
                     "- Creating Primary support reason with Past date for both start date and end date " +
                     "- validating primary support reason created is modified with current date ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod11()
        {
            var currentdate = DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date                                                                                                                    
            DateTime pastdate = DateTime.Now.AddDays(-05);

            #region Person Primary Support Reason

            Guid Record = dbHelper.personPrimarySupportReason.CreatePersonPrimarySupportReason(_personId, _advancedTeamId, _primarySupportReasonTypeId, pastdate, pastdate);

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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(false)
                .SelectPersonPrimarySupportReasonRecord(Record.ToString())
                .ClickRecord();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordModificationPageToLoad()
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateRecordCellText(Record.ToString(), 3, currentdate)
                .ValidateRecordCellText(Record.ToString(), 4, currentdate);

        }

        [TestProperty("JiraIssueID", "CDV6-12032")]
        [Description("Testing persons primary support reasons - " +
                     "Creating Primary support reason with current date for both start date and end date " +
                     "- validating primary support reason created is Shared to other team ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod12()
        {
            DateTime date = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());

            #region System User Record (Share To)

            var _sharedSystemUserName = "Share_CDV6_12032_User1";
            var _sharedSystemUserId = commonMethodsDB.CreateSystemUserRecord(_sharedSystemUserName, "Share CDV6-12032", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            var _sharedSystemUserFullName = "Share CDV6-12032 User1";

            #endregion

            #region Person Primary Support Reason

            Guid Record = dbHelper.personPrimarySupportReason.CreatePersonPrimarySupportReason(_personId, _advancedTeamId, _primarySupportReasonTypeId, date, date);

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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(false)
                .SelectPersonPrimarySupportReasonRecord(Record.ToString())
                .ClickRecord();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordModificationPageToLoad()
                .ClickShareRecordButton();

            shareRecordPopup
                .WaitForShareRecordPopupToLoad()
                .SearchForUserRecord(_sharedSystemUserFullName);

            shareRecordResultsPopup
                .WaitForShareRecordResultsPopupToLoad()
                .TapAddUserButton(_sharedSystemUserId.ToString());

            shareRecordPopup
                .WaitForResultsPopupToClose();

        }

        [TestProperty("JiraIssueID", "CDV6-12033")]
        [Description("Testing persons primary support reasons -" +
                     " Creating Primary support reason  via advance search option" +
                     " - validating primary support reason is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod13()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date                                                                                

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            homePage
                .ClickAdvanceSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Person Primary Support Reasons")
                .WaitForAdvanceSearchPageToLoad()
                .SelectSavedView("Inactive Primary Support Reasons View")
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoadFromAdvanceSearch()
                .ClickPersonID_LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_person_fullName.ToString())
                .TapSearchButton()
                .SelectResultElement(_personId.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoadFromAdvanceSearch()
                .ClickOwnerIDLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("caredirector QA")
                .TapSearchButton()
                .SelectResultElement(_careDirectorQA_TeamId.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoadFromAdvanceSearch()
                .ClickPrimarySupportReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_primarySupportReasonTypeName.ToString())
                .TapSearchButton()
                .SelectResultElement(_primarySupportReasonTypeId.ToString());

            personPrimarySupportReasonRecordPage
                .WaitForPersonPrimarySupportReasonRecordPageToLoadFromAdvanceSearch()
                .InsertStartDate(currentdate)
                .ClickSaveAndCloseButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(false);

        }

        [TestProperty("JiraIssueID", "CDV6-12034")]
        [Description("Testing persons primary support reasons" +
                     " - Saving Primary support reason with current date for both start date and end date" +
                     " - validating primary support reason created is deleted ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod14()
        {
            DateTime date = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());

            #region Person Primary Support Reason Record

            Guid Record = dbHelper.personPrimarySupportReason.CreatePersonPrimarySupportReason(_personId, _advancedTeamId, _primarySupportReasonTypeId, date, date);

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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(false)
                .SelectPersonPrimarySupportReasonRecord(Record.ToString())
                .ClickDeletedButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad()
                .ValidateNoRecordLabelVisibile(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12157")]
        [Description("Testing persons primary support reasons " +
                     "- Saving Primary support reason with current date for both start date and end date " +
                     "- validating primary support reason created is Export to excel ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PrimarySupportReasons_UITestMethod15()
        {
            DateTime date = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());

            #region Person Primary Support Reason Record

            Guid Record = dbHelper.personPrimarySupportReason.CreatePersonPrimarySupportReason(_personId, _advancedTeamId, _primarySupportReasonTypeId, date, date);

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
                .NavigateToPrimarySupportReasonsPage();

            personPrimarySupportReasonPage
                .WaitForPersonPrimarySupportReasonPageToLoad();

            var primarySupportReasonRecords = dbHelper.personPrimarySupportReason.GetPersonPrimarySupportReasonByPersonID(_personId);
            Assert.AreEqual(1, primarySupportReasonRecords.Count);

            personPrimarySupportReasonPage
                .SelectPersonPrimarySupportReasonRecord(primarySupportReasonRecords[0].ToString())
                .ClickExportToExcel();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Selected Records")
                .SelectExportFormat("Csv (comma separated with quotes)")
                .ClickExportButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "PersonPrimarySupportReasons.csv");
            Assert.IsTrue(fileExists);

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
