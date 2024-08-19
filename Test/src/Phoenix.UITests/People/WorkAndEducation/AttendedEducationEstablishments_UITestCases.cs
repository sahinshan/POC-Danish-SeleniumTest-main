using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.People.WorkAndEducation
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    /// 
    [TestClass]
    public class AttendedEducationEstablishments_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private String _systemUsername;


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

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("EducationYears BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("EducationYears T1", null, _businessUnitId, "907678", "EducationYearsT3@careworkstempmail.com", "EducationYears T3", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User EducationYearsUser1

                _systemUsername = "EducationYearsUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EducationYears", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-672

        [TestProperty("JiraIssueID", "CDV6-2354")]
        [Description("To verify 'All Attended Education Establishment Records' view is displayed in the Business Object Reference view of Attended Education Establishments record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void EducationYears_UITestMethod01()
        {
            var currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Person

            var _firstName = "Paul";
            var _lastName = "LN_" + currentDate;
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Provider

            var _providerName = "School " + currentDate;
            var _providerTypeId = 9; //Education Establishment
            var _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, _providerTypeId);

            #endregion

            #region Attended Education Establishment

            var personAttendedEducationEstablishmentId = dbHelper.personAttendedEducationEstablishment.CreatePersonAttendedEducationEstablishment(_personID, _providerId, new DateTime(2023, 1, 1), 1, _teamId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("EducationYearsUser1", "Passw0rd_!")
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
                .NavigateToAttendedEducationEstablishmentPage();

            attendedEducationEstablishmentsPage
                .WaitForPageToLoad()
                .OpenRecord(personAttendedEducationEstablishmentId);

            attendedEducationEstablishmentRecordPage
                .WaitForPageToLoad()
                .NavigateToPersonEducationYear();

            educationYearsPage
                .WaitForEducationYearsPageToLoad()
                .ClickNewRecordButton();

            educationYearRecordPage
                .WaitForEducationYearRecordPageToLoad()
                .ClickAttendedEducationEstablishmentLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateGridHeaderCellText(2, "Person")
                .ValidateGridHeaderCellText(3, "Education Establishment")
                .ValidateGridHeaderCellText(4, "Establishment Type")
                .ValidateGridHeaderCellText(5, "Current Year")
                .ValidateGridHeaderCellText(6, "Start Date")
                .ValidateGridHeaderCellText(7, "Status")

                .ValidateGridRecordCellText(personAttendedEducationEstablishmentId, 2, _personFullName)
                .ValidateGridRecordCellText(personAttendedEducationEstablishmentId, 3, _providerName)
                .ValidateGridRecordCellText(personAttendedEducationEstablishmentId, 4, "")
                .ValidateGridRecordCellText(personAttendedEducationEstablishmentId, 5, "")
                .ValidateGridRecordCellText(personAttendedEducationEstablishmentId, 6, "01/01/2023")
                .ValidateGridRecordCellText(personAttendedEducationEstablishmentId, 7, "New")
                ;

        }


        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }


        #endregion
    }

}






