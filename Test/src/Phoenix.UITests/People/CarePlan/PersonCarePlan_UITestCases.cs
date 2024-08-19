using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.CarePlan
{
    /// <summary>
    /// This class contains Automated UI test scripts for Care Plan
    /// </summary>
    [TestClass]
    public class PersonCarePlan_UITestCases : FunctionalTest
    {
        private string _tenantName;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _systemUserId;
        private Guid _personID;
        private string person_fullName;
        private int _personNumber;
        private string _systemUsername;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        private Guid _carePlanNeedDomainId;
        private Guid _carePlanAgreedById;

        [TestInitialize()]
        public void CarePlan_SetupTest()
        {

            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Environment Name

                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                string user = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(user)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CarePlanBU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CarePlanT1", null, _businessUnitId, "907678", "careplant1@careworkstempmail.com", "CarePlanT1", "020 123456");

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Create SystemUser 

                _systemUsername = "CarePlanUser5947";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "CarePlan", "User5947", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Person

                var firstName = "Phil";
                var lastName = _currentDateSuffix;
                person_fullName = firstName + " " + lastName;
                _personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-8897

        [TestProperty("JiraIssueID", "ACC-5947")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5947 - " +
            "Verify that 'care plan review date' in person BO should be displayed the nearest future date from its list of care plans.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Planning")]
        [TestProperty("Screen1", "Person")]
        [TestProperty("Screen2", "Personalised Care and Support Plans")]
        public void PersonCarePlan_UITestMethod01()
        {
            #region careplanneeddomain

            _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Acute").FirstOrDefault();

            #endregion

            #region careplannagreedby

            _carePlanAgreedById = dbHelper.carePlanAgreedBy.GetByName("Clinical Service or Team").FirstOrDefault();

            #endregion

            #region Care Plan
            var todayDate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            var _carePlanAgreed = dbHelper.carePlanAgreedBy.GetById(_carePlanAgreedById, "name")["name"];
            var _carePlanAgreedByIds = new Dictionary<Guid, String>();
            _carePlanAgreedByIds.Add(_carePlanAgreedById, _carePlanAgreed.ToString());

            var _personCarePlanId1 =
                dbHelper
                .personCarePlan
                .CreatePersonCarePlan(_teamId, _businessUnitId, _personID, todayDate, _carePlanAgreedByIds,
                        _carePlanNeedDomainId, todayDate, false, todayDate.AddDays(1),
                       1, 1, "Situation Test 1", "Outcome 1", "Action 1", 1);

            var _personCarePlanId2 =
                dbHelper
                .personCarePlan
                .CreatePersonCarePlan(_teamId, _businessUnitId, _personID, todayDate, _carePlanAgreedByIds,
                        _carePlanNeedDomainId, todayDate, false, todayDate.AddDays(2),
                       1, 1, "Situation Test 2", "Outcome 2", "Action 2", 1);

            dbHelper.personCarePlan.UpdateStatus(_personCarePlanId2, 2);
            dbHelper.personCarePlan.UpdateAuthorisationInformation(_personCarePlanId2, _systemUserId, todayDate);

            #endregion


            #region Step 1

            loginPage
                   .GoToLoginPage()
                   .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false);

            #endregion

            #region Step 2

            mainMenu
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
                .VerifyRecordIsPresent(_personCarePlanId1.ToString())
                .VerifyRecordIsPresent(_personCarePlanId2.ToString());

            #endregion

            #region Step 3

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_personID.ToString(), person_fullName);

            #endregion

            #region Step 4

            personRecordEditPage
                .ValidateNextCarePlanReviewDate(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ClickCloseButton();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .DeleteCarePlanRecord(_personCarePlanId1.ToString());

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .VerifyRecordIsPresent(_personCarePlanId1.ToString(), false)
                .VerifyRecordIsPresent(_personCarePlanId2.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_personID.ToString(), person_fullName)
                .ValidateNextCarePlanReviewDate(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickCloseButton();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8926

        [TestProperty("JiraIssueID", "ACC-5948")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5948 - " +
            "Verify that 'care plan review date' in person BO should be displayed as null when person has historic care plans or person has no care plans at all.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Planning")]
        [TestProperty("Screen1", "Person")]
        [TestProperty("Screen2", "Personalised Care and Support Plans")]
        public void PersonCarePlan_UITestMethod02()
        {
            #region careplanneeddomain

            _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Acute").FirstOrDefault();

            #endregion

            #region careplannagreedby

            _carePlanAgreedById = dbHelper.carePlanAgreedBy.GetByName("Clinical Service or Team").FirstOrDefault();

            #endregion

            #region Care Plan
            var todayDate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            var _carePlanAgreed = dbHelper.carePlanAgreedBy.GetById(_carePlanAgreedById, "name")["name"];
            var _carePlanAgreedByIds = new Dictionary<Guid, String>();
            _carePlanAgreedByIds.Add(_carePlanAgreedById, _carePlanAgreed.ToString());

            var _personCarePlanId1 =
                dbHelper
                .personCarePlan
                .CreatePersonCarePlan(_teamId, _businessUnitId, _personID, todayDate, _carePlanAgreedByIds,
                        _carePlanNeedDomainId, todayDate, false, todayDate.AddDays(1),
                       1, 1, "Situation Test 1", "Outcome 1", "Action 1", 1);

            var _personCarePlanId2 =
                dbHelper
                .personCarePlan
                .CreatePersonCarePlan(_teamId, _businessUnitId, _personID, todayDate, _carePlanAgreedByIds,
                        _carePlanNeedDomainId, todayDate, false, todayDate.AddDays(2),
                       1, 1, "Situation Test 2", "Outcome 2", "Action 2", 1);

            dbHelper.personCarePlan.UpdateStatus(_personCarePlanId1, 2);
            dbHelper.personCarePlan.UpdateAuthorisationInformation(_personCarePlanId1, _systemUserId, todayDate);

            #endregion

            #region Step 1

            loginPage
                   .GoToLoginPage()
                   .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, false, false, false);

            #endregion

            #region Step 2

            mainMenu
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
                .VerifyRecordIsPresent(_personCarePlanId1.ToString())
                .VerifyRecordIsPresent(_personCarePlanId2.ToString());

            #endregion

            #region Step 3

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_personID.ToString(), person_fullName);

            #endregion

            #region Step 4

            personRecordEditPage
                .ValidateNextCarePlanReviewDate(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ClickCloseButton();

            #endregion

            #region Step 5

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .OpenRecord(_personCarePlanId1.ToString());

            personalisedCareAndSupportPlanRecordPage
                .WaitForPageToLoad()
                .ClickEndCarePlanButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("This action will update the Care Plan to read-only and cannot be undone. Do you wish to continue?")
                .TapOKButton();

            personalisedCareAndSupportPlanRecordPage
                .WaitForPageToLoad()
                .ValidateStatusSelectedText("Historic")
                .ValidateEndDateText(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndReasonField_LinkText("Ended")
                .ClickBackButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_personID.ToString(), person_fullName)
                .ValidateNextCarePlanReviewDate(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickCloseButton();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .DeleteCarePlanRecord(_personCarePlanId2.ToString());

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad();

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_personID.ToString(), person_fullName)
                .ValidateNextCarePlanReviewDate("")
                .ClickCloseButton();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
                .DeleteCarePlanRecord(_personCarePlanId1.ToString());

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personCarePlansSubPage_CarePlansTab
                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad();

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_personID.ToString(), person_fullName)
                .ValidateNextCarePlanReviewDate("")
                .ClickCloseButton();

            #endregion


        }

        #endregion
    }

}

