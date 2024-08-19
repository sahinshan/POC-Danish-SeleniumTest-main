using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_AboutMe_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _ethnicityId;
        private Guid _systemUserId;
        private Guid _nonAdminSystemUserId;
        private Guid _personID1;
        private string _personNumber1;
        private string _person1FullName;
        private Guid _aboutMeSetupId;
        private string _loginUsername;
        private string _nonAdminSystemUsername;
        private string tenantName;

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {

            try
            {
                #region Connection to database / Environmeet

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careProviders_TeamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create Default System User 1

                _loginUsername = "CW_Forms_Test_User_3";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "CW", "Forms Test User 3", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Create Default System User 2

                // Security Profiles

                var userSecProfiles = new List<Guid>
                {
                    dbHelper.securityProfile.GetSecurityProfileByName("Person Module (Edit)")[0],
                    dbHelper.securityProfile.GetSecurityProfileByName("Person (Edit)")[0],
                    dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0],
                    dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0],
                    dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access")[0]
                };

                // Non Admin System User

                _nonAdminSystemUsername = "CW_NonAdmin_Test_User_1";
                _nonAdminSystemUserId = commonMethodsDB.CreateSystemUserRecord(_nonAdminSystemUsername, "CW", "NonAdmin Test User 1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid, userSecProfiles);

                #endregion

                #region Person

                var currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                _person1FullName = "CDV6_16005 " + currentDateTime;
                _personID1 = dbHelper.person.CreatePersonRecord("", "CDV6_16005", "", currentDateTime, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId, 7, 2);
                _personNumber1 = ((int)dbHelper.person.GetPersonById(_personID1, "personnumber")["personnumber"]).ToString();

                #endregion

                #region About Me Setup and Person About Me

                _aboutMeSetupId = dbHelper.aboutMeSetup.CreatePersonAboutMeSetup(_careProviders_TeamId, true, 2, false, "Test", false, "Test", false, "Test", false, "Test", false, "Test", false, "Test", false, "Test", false, "Test", false, "Test");

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-16005

        [TestProperty("JiraIssueID", "ACC-828")]
        [Description("")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [DeploymentItem("Files\\video.mp4"), DeploymentItem("Files\\Video52MB.mp4"), DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod]
        [TestProperty("BusinessModule", "About Me")]
        [TestProperty("Screen", "About Me")]
        public void Person_AboutMe_UITestMethod01()
        {
            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1, _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad();

            #endregion

            #region Step 2

            personRecordPage_AboutMeArea
                .ValidateGeneralAreaVisible(true);

            #endregion

            #region Step 3

            //Collapse/Expand button is not available in the current UI. About Me Sections are visible after opening About Me tab.

            #endregion

            #region Step 4

            personRecordPage_AboutMeArea
                .ValidateSaveButtonDisabled(true)
                .ValidateSaveAndCloseButtonDisabled(true);

            #endregion

            #region Step 5

            personRecordPage_AboutMeArea
                .ValidateDateFieldDisabled(false)
                .ValidateResponsibleTeamFieldDisabled(true)
                .ValidateSupportedToWriteThisByFieldDisabled(true)
                .ValidateCapacityEstablishedFieldDisabled(true)
                .ValidateConsentGrantedForRecordingsFieldDisabled(true);

            #endregion

            #region Step 6

            personRecordPage_AboutMeArea
                .InsertDate(DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"))
                .ValidateDateFieldDisabled(false)
                .ValidateResponsibleTeamFieldDisabled(false)
                .ValidateSupportedToWriteThisByFieldDisabled(false)
                .ValidateCapacityEstablishedFieldDisabled(false)
                .ValidateConsentGrantedForRecordingsFieldDisabled(false);

            #endregion

            #region Step 7

            personRecordPage_AboutMeArea
                .ValidateResponsibleTeamField("CareProviders")
                .ValidateSupportedToWriteThisByField("")
                .ValidateCapacityEstablishedOption(false)
                .ValidateConsentGrantedForRecordingOption(false);

            #endregion

            #region Step 8

            personRecordPage_AboutMeArea
                .ClickSupportedToWriteThisByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateBusinessObjectPresent("People")
                .ValidateBusinessObjectPresent("System Users")
                .ClickCloseButton();

            #endregion

            #region Step 9

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ClickOnDateCalendarIcon();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(DateTime.Now.Date);

            #endregion

            #region Step 10

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ClickSupportedToWriteThisByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("All Active People")
                .TypeSearchQuery(_personNumber1)
                .TapSearchButton()
                .SelectResultElement(_personID1.ToString());

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .SelectConsentGrantedForRecordingOption(true)
                .ClickSaveAndArchiveButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1, _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad();

            #endregion

            #region Step 11

            personRecordPage_AboutMeArea
                .UploadAboutMeMedia(TestContext.DeploymentDirectory + "\\video.mp4")
                .ClickSaveButton(15);

            System.Threading.Thread.Sleep(3000);

            #endregion

            #region Step 12

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ClickAboutMeMediaDeleteButton()
                .ClickSaveButton();

            #endregion

            #region Step 13

            //not possible to automate 

            #endregion

            #region Step 14

            //not possible to automate 

            #endregion

            #region Step 15

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .UploadAboutMeMedia(TestContext.DeploymentDirectory + "\\Document.txt");

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Invalid file type: text/plain. Please only upload webm or mp4 files.")
                .TapCloseButton();

            #endregion

            #region Step 16

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1, _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .UploadAboutMeMedia(TestContext.DeploymentDirectory + "\\Video52MB.mp4");

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Your file is 51.49MB. There is a limit of 50MB per file.")
                .TapCloseButton();

            #endregion

            #region Step 17

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ClickResponsibleTeamRemoveButton()
                .ClickSupportedToWriteThisByRemoveButton()
                .InsertDate("")
                .ClickSaveAndArchiveButton();

            personRecordPage_AboutMeArea
                .ValidateGeneralErrorMessageVisible(true)
                .ValidateGeneralErrorMessageText("Some data is not correct. Please review the data in the form.")
                .ValidateDateErrorLabelVisibility(true)
                .ValidateDateErrorLabelText("Please fill out this field.")
                .ValidateResponsibleTeamErrorLabelVisibility(true)
                .ValidateResponsibleTeamErrorLabelText("Please fill out this field.");

            #endregion

            #region Step 18

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            peoplePage
                .WaitForPeoplePageToLoad();

            #endregion

            #region Step 19

            peoplePage
                .SearchPersonRecordByID(_personNumber1, _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .InsertDate(DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy"))
                .ClickSaveAndArchiveButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAboutMePage();

            personAboutMePage
                .WaitForPerson_AboutMePageToLoad();

            var personAboutMeRecords = dbHelper.personAboutMe.GetByPersonID(_personID1, true);
            Assert.AreEqual(1, personAboutMeRecords.Count());
            var firstAboutMeArchivedRecord = personAboutMeRecords[0];

            personAboutMePage
                .ValidatePersonAboutMeRecordPresent(firstAboutMeArchivedRecord.ToString());

            #endregion

            #region Step 20

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1, _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .UploadWhatIsMostImportantToMeMedia(TestContext.DeploymentDirectory + "\\video.mp4")
                .ClickSaveAndArchiveButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAboutMePage();

            personAboutMePage
                .WaitForPerson_AboutMePageToLoad();

            personAboutMeRecords = dbHelper.personAboutMe.GetByPersonID(_personID1, true);
            Assert.AreEqual(2, personAboutMeRecords.Count());

            #endregion

            #region Step 21

            //this step is repeated. step 11 and 12 achieve the same thing

            #endregion

            #region Step 22

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ValidateWhatIsMostImportantMediaPresent(true);

            #endregion

            #region Step 23

            personRecordPage_AboutMeArea
                .UploadAboutMeMedia(TestContext.DeploymentDirectory + "\\Video52MB.mp4");

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Your file is 51.49MB. There is a limit of 50MB per file.")
                .TapCloseButton();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .UploadAboutMeMedia(TestContext.DeploymentDirectory + "\\video.mp4")
                .ClickSaveButton(15)
                .WaitForPersonRecordPage_AboutMeAreaToLoad();

            #endregion

            #region Step 24

            //not possible to automate this step

            #endregion

            #region Step 25

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ClickAboutMeMediaDeleteButton()
                .ClickWhatIsMostImportantToMeMediaDeleteButton()
                .ClickSaveButton(10);

            #endregion

            #region Step 26

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .SelectConsentGrantedForRecordingOption(false)
                .ClickSaveButton(10);

            #endregion

            #region Step 27

            //not possible to automate

            #endregion

            #region Step 28

            //not possible to automate

            #endregion

            #region Step 29

            //this scenario is already covered by the previous steps. The user CW_Forms_Test_User_1 is linked to the sys admin security profile

            #endregion

            #region Step 30

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickSignOutButton();

            loginPage
                .GoToLoginPage()
                .Login(_nonAdminSystemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1, _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ValidateAboutMeMediaReadOnly(true);

            #endregion

        }


        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }



    }
}
