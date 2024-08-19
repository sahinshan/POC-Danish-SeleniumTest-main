using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class Person_Chronologies_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _systemUserId;
        private string _systemUserName;
        private Guid _personId;
        private int _personNumber;
        private string _person_fullName;
        private Guid _significantEventCategoryId1;
        private Guid _significantEventCategoryId2;
        private string _significantEventCategoryName1;
        private string _significantEventCategoryName2;
        private Guid _significantEventSubCategoryId1;
        private Guid _significantEventSubCategoryId2;
        private string _significantEventSubCategoryName1;
        private string _significantEventSubCategoryName2;
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create System User Record

                _systemUserName = "Person_Chronologies_User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Chronologies", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Person

                var firstName = "First";
                var lastName = "LN_" + _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _person_fullName = firstName + " " + lastName;

                #endregion

                #region Significant Event Category

                _significantEventCategoryName1 = "Category 1";
                _significantEventCategoryId1 = commonMethodsDB.CreateSignificantEventCategory(_significantEventCategoryName1, new DateTime(2020, 1, 1), _teamId, "", null, null, true);

                _significantEventCategoryName2 = "Child in Need";
                _significantEventCategoryId2 = commonMethodsDB.CreateSignificantEventCategory(_significantEventCategoryName2, new DateTime(2020, 1, 1), _teamId, "", null, null, true);

                #endregion

                #region Significant Event Category

                commonMethodsDB.CreateSignificantEventSubCategory(_teamId, "Category 1_Sub", _significantEventCategoryId1, new DateTime(2022, 1, 1), null, null);
                commonMethodsDB.CreateSignificantEventSubCategory(_teamId, "Sub Cat 1_1", _significantEventCategoryId1, new DateTime(2022, 1, 1), null, null);

                _significantEventSubCategoryName1 = "Sub Cat 1_2";
                _significantEventSubCategoryId1 = commonMethodsDB.CreateSignificantEventSubCategory(_teamId, _significantEventSubCategoryName1, _significantEventCategoryId1, new DateTime(2022, 1, 1), null, null);

                _significantEventSubCategoryName2 = "Sub Cat 2_1";
                _significantEventSubCategoryId2 = commonMethodsDB.CreateSignificantEventSubCategory(_teamId, _significantEventSubCategoryName2, _significantEventCategoryId2, new DateTime(2022, 1, 1), null, null);
                commonMethodsDB.CreateSignificantEventSubCategory(_teamId, "Sub Cat 2_2", _significantEventCategoryId2, new DateTime(2022, 1, 1), null, null);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-11947

        [TestProperty("JiraIssueID", "CDV6-12273")]
        [Description("Testing Chronology Record " +
                     "- Saving Chronology without mandatory fields" +
                     "-validating (Please fill out this field.) Field Error Message displayed ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod01()
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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .ClickSaveButton()
                .ValidateTitleFieldErrorMessage("Please fill out this field.")
                .ValidateDateFromFieldErrorMessage("Please fill out this field.")
                .ValidateDateToFieldErrorMessage("Please fill out this field.");

        }


        [TestProperty("JiraIssueID", "CDV6-12295")]
        [Description("Testing person Chronology " +
                     "- Saving chronology record without selecting category fields" +
                     "-validating (Please select at least one category) Error message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod02()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .ClickSaveButton()
                .ValidateNotificationErrorMessage("Please select at least one category.")
                .TapClickHereToHideMessage()
                .ValidateNoNotificationErrorMessageVisibile(true);

        }


        [TestProperty("JiraIssueID", "CDV6-12298")]
        [Description("Testing persons Chronology Record Creation " +
                     "- Saving ChronologyRecord with mandatory fields Without Significant Event Record" +
                     "-validating Error message (No events found!) is displayed ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod03()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .TapCategorie()
                .ClickSaveButton()
                .WaitForPersonChronologyNewRecordPageToLoad()
                .WaitForPersonChronologyNewRecordPageToLoad()
                .ValidateNotificationErrorMessage("No events found!");

        }


        [TestProperty("JiraIssueID", "CDV6-12318")]
        [Description(" persons Chronology Record Creation " +
                     "- Saving Chronology Record with Date Frome and Date To in Past" +
                     "-validating Chronology Record Creation ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod04()
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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate("21/08/2021")
                .InsertEndDate("21/08/2021")
                .TapCategorie()
                .ClickSaveButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickViewSaveRecordButton();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

        }


        [TestProperty("JiraIssueID", "CDV6-12319")]
        [Description(" persons Chronology Record Creation " +
                     "- Saving Chronology Record with Date Frome and Date To in future date" +
                     "-validating Chronology Record Creation ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod05()
        {
            DateTime eventdate = Convert.ToDateTime(commonMethodsHelper.GetDateWithoutCulture(new DateTime(2030, 8, 21)));

            #region Person Significant Event Record

            dbHelper.personSignificantEvent.CreatePersonSignificantEvent(_personId, _significantEventCategoryId2, eventdate, _teamId, _significantEventSubCategoryId2);

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate("21/08/2030")
                .InsertEndDate("21/08/2030")
                .TapCategorie()
                .ClickSaveButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickViewSaveRecordButton();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

        }


        [TestProperty("JiraIssueID", "CDV6-12334")]
        [Description(" persons Chronology Record Creation " +
                     "- Saving Chronology Record with Date Frome and Date To in present date" +
                     "-validating Chronology Record Creation in TimeLine Tab ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod06()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date          
            DateTime eventdate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            #region Person Significant Event Record

            dbHelper.personSignificantEvent.CreatePersonSignificantEvent(_personId, _significantEventCategoryId2, eventdate, _teamId, _significantEventSubCategoryId2);

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .TapCategorie()
                .ClickSaveButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickViewSaveRecordButton();

            var chronologyRecords = dbHelper.personChronology.GetPersonChronologyByPersonID(_personId);
            Assert.AreEqual(1, chronologyRecords.Count);

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
                .ValidateRecordPresent(chronologyRecords.FirstOrDefault().ToString());

        }


        [TestProperty("JiraIssueID", "CDV6-12335")]
        [Description(" persons Chronology Record Creation " +
                     "- Saving Chronology Record with Date Frome and Date To in present date" +
                      "-modifying significant event record" +
                     "-validating Chronology Record is existing")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod07()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date          
            DateTime eventdate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person Significant Event Record

            dbHelper.personSignificantEvent.CreatePersonSignificantEvent(_personId, _significantEventCategoryId2, eventdate, _teamId, _significantEventSubCategoryId2);

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .TapCategorie()
                .ClickSaveButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad();

            var significantEvenetRecords = dbHelper.personSignificantEvent.GetPersonSignificantEventByPersonID(_personId);
            Assert.AreEqual(1, significantEvenetRecords.Count);


            personChronologyRecordPage
                .WaitForIncludeEventRecordPageToLoad()
                .OpenSignificantEventRecord(significantEvenetRecords[0].ToString());

            personChronologyRecordPage
                .WaitForSignificantEventRecordPageToLoad()
                .InsertEventDate("23/08/2024")
                .ClickSaveAndCloseButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickViewSaveRecordButton();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
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
                .ValidateRecordNotPresent(significantEvenetRecords.FirstOrDefault().ToString());

        }


        [TestProperty("JiraIssueID", "CDV6-12373")]
        [Description(" persons Chronology Record Creation " +
                     "- Saving Chronology Record with Date Frome and Date To in present date" +
                      "-select view saved records option" +
                     "-validating Chronology Record is existing saved records page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod08()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date          
            DateTime eventdate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person Significant Event Record

            dbHelper.personSignificantEvent.CreatePersonSignificantEvent(_personId, _significantEventCategoryId2, eventdate, _teamId, _significantEventSubCategoryId2);

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .TapCategorie()
                .ClickSaveButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickViewSaveRecordButton();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            var chronologyRecords = dbHelper.personChronology.GetPersonChronologyByPersonID(_personId);
            Assert.AreEqual(1, chronologyRecords.Count);

        }


        [TestProperty("JiraIssueID", "CDV6-12377")]
        [Description(" persons Chronology Record Creation " +
                     "- Saving Chronology Record with Date Frome and Date To in present date" +
                      "-creat new chronology record with existing record" +
                     "-validating new Chronology Record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod9()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date          
            DateTime eventdate = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());

            #region Significant Event Record

            dbHelper.personSignificantEvent.CreatePersonSignificantEvent(_personId, _significantEventCategoryId1, eventdate, _teamId, _significantEventSubCategoryId1);

            #endregion

            #region  Person Chronology Record

            Guid ChronologyId = dbHelper.personChronology.CreatePersonChronology("chrono", eventdate, eventdate, _teamId, _personId);

            #endregion

            #region Significant Event Category  & Sub Category Person Chronology

            dbHelper.significantEventCategoryPersonChronology.CreateSignificantEventCategoryPersonChronology(_significantEventCategoryId2, ChronologyId);
            dbHelper.significantEventSubCategoryPersonChronology.CreateSignificantEventSubCategoryPersonChronology(_significantEventSubCategoryId2, ChronologyId);

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .TapCategorie()
                .ClickSaveButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickViewSaveRecordButton();

            var chronologyRecords = dbHelper.personChronology.GetPersonChronologyByPersonID(_personId);
            Assert.AreEqual(2, chronologyRecords.Count);

        }


        [TestProperty("JiraIssueID", "CDV6-12376")]
        [Description(" persons Chronology Record Creation " +
                     "- Saving Chronology Record with Date Frome and Date To in present date" +
                      "-click on additional event icon" +
                     "-validating (select atleast one record) validation message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod10()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date          
            DateTime eventdate = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());

            #region Significant Event Record

            dbHelper.personSignificantEvent.CreatePersonSignificantEvent(_personId, _significantEventCategoryId1, eventdate, _teamId, _significantEventSubCategoryId1);

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .TapCategorie()
                .ClickSaveButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickAdditionalEventButton()
                .ValidateNotificationErrorMessage("Please select at least one record.")
                .TapClickHereToHideMessage();

            var significantEvenetRecords = dbHelper.personSignificantEvent.GetPersonSignificantEventByPersonID(_personId);
            Assert.AreEqual(1, significantEvenetRecords.Count);

            personChronologyRecordPage
                .WaitForIncludeEventRecordPageToLoad()
                .SelectPersonChronologyRecord(significantEvenetRecords[0].ToString())
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickAdditionalEventButton();

            System.Threading.Thread.Sleep(2000);

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .WaitForPersonChronologyRecordPageToLoad()
                .WaitForIncludeEventRecordPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12439")]
        [Description(" persons Chronology Record Creation " +
                     "- Saving Chronology Record with Date Frome and Date To in present date" +
                      "-click on additional event icon" +
                      "-click on include chronology icon" +
                     "-validating (select atleast one record) validation message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod11()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date          
            DateTime eventdate = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());

            #region Significant Event Record

            dbHelper.personSignificantEvent.CreatePersonSignificantEvent(_personId, _significantEventCategoryId1, eventdate, _teamId, _significantEventSubCategoryId1);

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .TapCategorie()
                .ClickSaveButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad();

            var significantEvenetRecords = dbHelper.personSignificantEvent.GetPersonSignificantEventByPersonID(_personId);
            Assert.AreEqual(1, significantEvenetRecords.Count);

            personChronologyRecordPage
                .WaitForIncludeEventRecordPageToLoad()
                .SelectPersonChronologyRecord(significantEvenetRecords[0].ToString())
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickAdditionalEventButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapBackButton();

            peoplePage
                .WaitForPeoplePageToLoad()
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToChronologiesPage();

            var chronologyId = dbHelper.personChronology.GetPersonChronologyByPersonID(_personId).First();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .OpenPersonChronologyRecord(chronologyId.ToString());

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickExcludeEventButton()
                .ValidateNotificationErrorMessage("Please select at least one record.")
                .TapClickHereToHideMessage();

            personChronologyRecordPage
                .WaitForExcludeChronologyRecordsToLoad()
                .SelectPersonChronologyRecord(significantEvenetRecords[0].ToString())
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickExcludeEventButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .WaitForExcludeChronologyRecordsToLoad()
                .ValidateNoRecordMessageVisibile(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12450")]
        [Description(" persons Chronology Record Creation " +
                     "- Saving Chronology Record with Date Frome and Date To in present date" +
                      "-click on  save snapshot Button" +
                     "-validating (Snapshot created successfully) validation message is displayed-" +
                     "click on view saved record snapshot Button" +
                     "-validated saved snapshot record  is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod12()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date          
            DateTime eventdate = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());

            #region System Setting

            var systemSettingId = commonMethodsDB.CreateSystemSetting("Chronology.PrintFormat", "Word", "Describe print format for chronology. Valid values PDF or Word", false, "false");

            #endregion

            #region Significant Event Record

            dbHelper.personSignificantEvent.CreatePersonSignificantEvent(_personId, _significantEventCategoryId1, eventdate, _teamId, _significantEventSubCategoryId1);

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .TapCategorie()
                .ClickSaveButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad();

            var significantEvenetRecords = dbHelper.personSignificantEvent.GetPersonSignificantEventByPersonID(_personId);
            Assert.AreEqual(1, significantEvenetRecords.Count);

            personChronologyRecordPage
                .WaitForIncludeEventRecordPageToLoad()
                .SelectPersonChronologyRecord(significantEvenetRecords[0].ToString())
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickSaveSnapshotButton()
                .ValidateNotificationHolderMessage("Snapshot created successfully")
                .ClickToolBarMenuButton()
                .ClickViewSavedSnapShotButton()
                .WaitForPersonChronologyRecordPageToLoad()
                .TapViewSnapShotPrintButton();

            System.Threading.Thread.Sleep(3000);

            List<string> filesPath = fileIOHelper.GetFilesPath(this.DownloadsDirectory, "*.docx");
            Assert.AreEqual(1, filesPath.Count); //only 1 file should exist

        }

        [TestProperty("JiraIssueID", "CDV6-12455")]
        [Description(" persons Chronology Record Creation " +
                    "- Saving Chronology Record with Date Frome and Date To in present date" +
                     "-click on  Print Button" +
                    "-validated record is downloaded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod13()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date          
            DateTime eventdate = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());

            #region System Setting

            var systemSettingId = commonMethodsDB.CreateSystemSetting("Chronology.PrintFormat", "Word", "Describe print format for chronology. Valid values PDF or Word", false, "false");

            #endregion

            #region Significant Event Record

            dbHelper.personSignificantEvent.CreatePersonSignificantEvent(_personId, _significantEventCategoryId1, eventdate, _teamId, _significantEventSubCategoryId1);

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .TapCategorie()
                .ClickSaveButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad();

            var significantEvenetRecords = dbHelper.personSignificantEvent.GetPersonSignificantEventByPersonID(_personId);
            Assert.AreEqual(1, significantEvenetRecords.Count);

            personChronologyRecordPage
                .WaitForIncludeEventRecordPageToLoad()
                .SelectPersonChronologyRecord(significantEvenetRecords[0].ToString())
                .WaitForPersonChronologyRecordPageToLoad()
                .TapPrintButton();

            personChronologyRecordPrintPopup
                .WaitForPersonChronologyRecordPrintPopupToLoad()
                .SelectGroupByByText("Event date")
                .TapPrintButton();

            System.Threading.Thread.Sleep(3000);

            List<string> filesPath = fileIOHelper.GetFilesPath(this.DownloadsDirectory, "*.docx");
            Assert.AreEqual(1, filesPath.Count); //only 1 file should exist

        }

        [TestProperty("JiraIssueID", "CDV6-12458")]
        [Description(" persons Chronology Record Creation " +
                   "- Saving Chronology Record with Date Frome and Date To in present date" +
                    "-click on  add new significant event option" +
                   "-validated record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod14()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date          
            DateTime eventdate = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());

            #region Significant Event Record

            dbHelper.personSignificantEvent.CreatePersonSignificantEvent(_personId, _significantEventCategoryId1, eventdate, _teamId, _significantEventSubCategoryId1);

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .TapCategorie()
                .ClickSaveButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad();

            var significantEvenetRecords = dbHelper.personSignificantEvent.GetPersonSignificantEventByPersonID(_personId);
            Assert.AreEqual(1, significantEvenetRecords.Count);

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .ClickToolBarMenuButton()
                .ClickAddANewSignificantEventButton();

            personSignificantEvent
                .WaitForPersonNewSignificantEventPageToLoad()
                .InsertEventCatogery("Child in Need")
                .InsertEventDate(currentdate)
                .ClickResponsibleTeamLookup();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CareDirector QA")
                .TapSearchButton()
                .SelectResultElement(_teamId.ToString());

            personSignificantEvent
                .WaitForPersonNewSignificantEventPageToLoad()
                .ClickSaveAndCloseButton();

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad();

            var significantEvenetRecordsAfterRecordCreation = dbHelper.personSignificantEvent.GetPersonSignificantEventByPersonID(_personId);
            Assert.AreEqual(2, significantEvenetRecordsAfterRecordCreation.Count);

        }


        [TestProperty("JiraIssueID", "CDV6-12461")]
        [Description(" persons Chronology Record Creation " +
                    "- Saving Chronology Record with Date Frome and Date To in present date" +
                     "-delete the chronology record" +
                    "-validating chronology Record is deleted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod15()
        {
            DateTime eventdate = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());

            #region Significant Event Record

            dbHelper.personSignificantEvent.CreatePersonSignificantEvent(_personId, _significantEventCategoryId1, eventdate, _teamId, _significantEventSubCategoryId1);

            #endregion

            #region Person Chronology Record

            Guid ChronologyId = dbHelper.personChronology.CreatePersonChronology("chrono", eventdate, eventdate, _teamId, _personId);

            #endregion

            #region Significant Event Category & Sub Category Person Chronology

            dbHelper.significantEventCategoryPersonChronology.CreateSignificantEventCategoryPersonChronology(_significantEventCategoryId2, ChronologyId);
            dbHelper.significantEventSubCategoryPersonChronology.CreateSignificantEventSubCategoryPersonChronology(_significantEventSubCategoryId2, ChronologyId);

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad();

            var chronologyRecords = dbHelper.personChronology.GetPersonChronologyByPersonID(_personId);
            Assert.AreEqual(1, chronologyRecords.Count);

            personChronologiesPage
                .SelectPersonChronologyRecord(chronologyRecords[0].ToString())
                .SelectDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12462")]
        [Description(" persons Chronology Record Creation " +
                    "-create person Letter with significant event option enabled" +
                   "- create Chronology Record with Date Frome and Date To in present date" +
                   "-validated chronology record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronologies_UITestMethod16()
        {
            var currentdate = DateTime.Now.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture);//current date          
            DateTime eventdate = Convert.ToDateTime(commonMethodsHelper.GetCurrentDateWithoutCulture());
            bool IsSignificantEvent = true;

            #region Sender Person

            var _sender_Id = commonMethodsDB.CreatePersonRecord("Ralph", "Abbott", _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            #endregion

            #region Recipient Person

            var _recipient_Id = commonMethodsDB.CreatePersonRecord("John", "Abbott", _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            #endregion

            #region Activity Categories                

            var _activityCategoryId_Advice = commonMethodsDB.CreateActivityCategory("Advice", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Sub Categories

            var _activitySubCategoryId_HomeSupport = commonMethodsDB.CreateActivitySubCategory("Home Support", new DateTime(2020, 1, 1), _activityCategoryId_Advice, _teamId);

            #endregion

            #region Activity Outcome

            var _activityOutcomeId_MoreInformationNeeded = commonMethodsDB.CreateActivityOutcome("More information needed", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Reason

            var _activityReasonId_Assessment = commonMethodsDB.CreateActivityReason("Assessment", new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Activity Priority

            var _activityPriorityId_Normal = commonMethodsDB.CreateActivityPriority("Normal", new DateTime(2020, 1, 1), _teamId);

            #endregion


            #region Letter

            dbHelper.letter.CreateLetter(_sender_Id.ToString(), "Ralph Abbott", "person", "Address", _recipient_Id.ToString(), "John Abbott", "person", 1, "In Progress", "Letter 001", "Person Activities Letter 001", null, _teamId, _systemUserId, _activityCategoryId_Advice,
                                          _activitySubCategoryId_HomeSupport, _activityOutcomeId_MoreInformationNeeded, _activityReasonId_Assessment, _activityPriorityId_Normal, _personId, eventdate, _personId, _person_fullName, "person", IsSignificantEvent,
                                          eventdate, _significantEventCategoryId2, _significantEventSubCategoryId2);

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
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .SelectNewRecordButton();

            personChronologyRecordPage
                .WaitForPersonChronologyNewRecordPageToLoad()
                .InsertTitle("chrno")
                .InsertStartDate(currentdate)
                .InsertEndDate(currentdate)
                .TapCategorie()
                .ClickSaveButton();

            personChronologyRecordPage
               .WaitForPersonChronologyRecordPageToLoad()
               .ClickViewSaveRecordButton();

            var chronologyRecords = dbHelper.personChronology.GetPersonChronologyByPersonID(_personId);
            Assert.AreEqual(1, chronologyRecords.Count);

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
