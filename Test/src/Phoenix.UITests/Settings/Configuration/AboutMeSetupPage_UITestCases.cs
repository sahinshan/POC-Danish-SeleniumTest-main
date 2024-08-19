using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Phoenix.UITests.Settings.Configuration
{
    [TestClass]
    public class AboutMeSetupPage_UITestCases : FunctionalTest
    {
        #region https://advancedcsg.atlassian.net/browse/CDV6-16007

        #region Properties
        readonly string errorMessageText = "Please fill out this field.";
        readonly string guidelinesText = "Guidelines Text Area Text";
        private Guid _authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _languageId;

        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _ethnicityId;
        private Guid _systemUserId;
        private Guid _aboutMeSetupId;
        private Guid _personAboutMeId;

        private Guid adminUserId;
        private Guid _personID1;
        private int _personNumber1;
        private string _person1FullName;
        private string personFirstName;
        private string personLastName;
        private string _adminUser_Name;

        private string EnvironmentName;

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {

                #region Connection to database

                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new DBHelper.DatabaseHelper(tenantName);

                #endregion

                #region Environment Name
                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Business Unit
                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];


                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region System User

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_User").Any();
                if (!newSystemUser)
                    dbHelper.systemUser.CreateSystemUser("Testing_CDV6_User", "Testing", "CDV6_User", "Testing CDV6_User", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
                _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_User")[0];



                #endregion

                #region Admin user

                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_2").Any();
                if (!adminUserExists)
                {
                    adminUserId = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_2", "CW", "Admin Test User 2", "CW Admin Test User 2", "Passw0rd_!", "CW_Admin_Test_User_2@somemail.com", "CW_Admin_Test_User_2@othermail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId, systemUserSecureFieldsSecurityProfileId);
                }





                adminUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_2").FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);

                _adminUser_Name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(adminUserId, "fullname")["fullname"];

                #endregion

                #region Person

                personFirstName = "AutoUser_Fname" + DateTime.Now.ToString("yyyyMMddHHmmss");
                personLastName = "AutoUser_Lname";

                var personRecord1Exists = dbHelper.person.GetByFirstName(personFirstName).Any();
                if (!personRecord1Exists)
                {
                    _personID1 = dbHelper.person.CreatePersonRecord("", personFirstName, "", personLastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId, 7, 2);
                    _personNumber1 = (int)dbHelper.person.GetPersonById(_personID1, "personnumber")["personnumber"];
                }
                if (_personID1 == Guid.Empty)
                {
                    _personID1 = dbHelper.person.GetByFirstName(personFirstName).FirstOrDefault();
                    _personNumber1 = (int)dbHelper.person.GetPersonById(_personID1, "personnumber")["personnumber"];
                }
                _person1FullName = personFirstName + personLastName;

                #endregion

                #region About Me Setup and Person About Me

                _aboutMeSetupId = dbHelper.aboutMeSetup.CreatePersonAboutMeSetup(_careProviders_TeamId, true, 2, false, "Test", false, "Test", false, "Test",
        false, "Test", false, "Test", false, "Test", false, "Test", false, "Test", false, "Test");
                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }

        [TestProperty("JiraIssueID", "ACC-3352")]
        [Description("Login to CD. Navigate to Settings > Configuration > Customizations > Business Objects > PersonAboutMe Record" +
                    "Verify that About me link is present in the Person record > Menu | Related Items | About Me" +
                    "Verify that when user enters all the details / modifies any field & clicks on  “Save and Archive” in the Person > About Me tab, then a copy of the About Me Record prior to the changes is created & appears on the Menu > Related Items > About Me" +
                    "Verify that the snapshot of the archived record is non-editable" +
                    "Verify that snapshots are not taken for each click on Save icon after any changes" +
                    "Verify that when user opens the record from related items, the snapshot of the archived record appears" +
                    "Verify that the snapshot has the same data Prior to changes made and Save and Archive is clicked" +
                    "Verify that user does not have option to create a About Me record via Related items" +
                    "Verify that Default view is Related records view and below columns are present:" +
                    "Date, Supported to write this by, Capacity Established, Consent Granted, Responsible User, Created On." +
                    "Verify that user is able to search via 'Responsible User' field & the following columns appear in Quick Search view:" +
                    "Date, Supported to write this by, Capacity Established, Consent Granted, Responsible User, Modified On." +
                    "Verified that Person About Me is not available in Advanced Search")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "About Me")]
        [TestProperty("Screen1", "About Me")]
        public void AboutMeTests_UITestMethod001()
        {
            _personAboutMeId = dbHelper.personAboutMe.CreatePersonAboutMe(DateTime.Now,
                _systemUserId, _careProviders_BusinessUnitId, _careProviders_TeamId, _personID1, _personID1, false, false, "person", _person1FullName, _aboutMeSetupId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString(), _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            #region Step 1

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .ValidateAboutMe_Icon(true)
                .TapAboutMeTab();

            #endregion

            #region Step 2, 3, 4, 5, 6, 10, 11

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .InsertDate(commonMethodsHelper.GetFutureDate())
                .ClickSaveButton();

            personRecordPage_AboutMeArea
                .ValidateDate(commonMethodsHelper.GetFutureDate())
                .ValidateCapacityEstablishedOption(false)
                .ValidateConsentGrantedForRecordingOption(false)
                .ValidateSupportedToWriteThisByField(_person1FullName)
                .ValidateAboutMeMediaPresent(false)
                .ValidateWhatIsMostImportantMediaPresent(false)
                .ValidatePeopleWhoAreImportantMediaPresent(false)
                .ValidateHowICommunicateMediaPresent(false)
                .ValidatePleaseDoAndPleaseDoNotMediaPresent(false)
                .ValidateMyWellnessMediaPresent(false)
                .ValidateHowAndWhenToSupportMeMediaPresent(false)
                .ValidateAlsoWorthKnowingAboutMeMediaPresent(false)
                .ValidatePhysicalCharacteristicsMediaPresent(false);

            string valueBeforeArchival = personRecordPage_AboutMeArea.GetDate();
            string supportedToWriteValueBeforeArchival = personRecordPage_AboutMeArea.GetSupportedToWriteThisBy();
            //string responsibleUser = personRecordPage_AboutMeArea.GetResponsibleUser();
            string responsibleTeam = personRecordPage_AboutMeArea.GetResponsibleTeam();
            string capacity = personRecordPage_AboutMeArea.GetCapacityEstablishedSelectedOption();
            string consent = personRecordPage_AboutMeArea.GetConsentGrantedSelectedOption();

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .NavigateToAboutMePage();

            #region Step 4

            personAboutMePage
                .WaitForPerson_AboutMePageToLoad()
                .ValidateNoRecordMessageVisible(true);

            #endregion

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .InsertDate(commonMethodsHelper.GetFutureDate());
            //.ClickResponsibleUserLookupButton();

            //lookupPopup
            //    .WaitForLookupPopupToLoad()
            //    .SelectLookIn("Lookup View")
            //    .TypeSearchQuery("Testing_CDV6_User")
            //    .TapSearchButton()
            //    .SelectResultElement(_systemUserId.ToString());

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .SelectCapacityEstablisedOption(true)
                .InsertAboutMe_MyStory("About Me My Story Text")
                .InsertWhatIsMostImportant_MyStory("What Is Most Important Story Text")
                .InsertPeopleWhoAreImportant_MyStory("People Who Are Important Story Text")
                .InsertHowICommunicate_MyStory("How I Communicate Story Text")
                .InsertPleaseDoAndPleaseDoNot_MyStory("Please Do And Do Not Story Text")
                .InsertMyWellness_MyStory("Wellness Story Text")
                .InsertHowAndWhenToSupportMe_MyStory("How And When To Support Story Text")
                .InsertAlsoWorthKnowingAboutMe_MyStory("Worth Knowing About Me Story Text")
                .InsertPhysicalCharacteristics_MyStory("Physical Characteristics Story Text")
                .ClickSaveAndArchiveButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .NavigateToAboutMePage();

            #region Step 10, 11
            personAboutMePage
                .WaitForPerson_AboutMePageToLoad()
                .ValidateCreateNewRecordButtonAvailable(false)
                .ValidateSelectedPicklistOption("Related Records")
                .ValidatePersonAboutMeGridColumns();

            personAboutMePage
                //.SearchPersonAboutMeRecord(responsibleUser)
                .ValidatePersonAboutMeGridColumns();
            #endregion

            #region Step 2, 3
            personAboutMePage
                .SelectAvailableViewByText("Related Records")
                .OpenPersonAboutMeRecord();


            personAboutMeRecordPage
                .WaitForPerson_AboutMeRecordPageToLoad()
                .ValidatePersonAboutMeRecordPageFieldsAreDisabled()
                .ValidatePersonAboutMeRecordPageMediaFieldsAreDisabled()
                .ValidateAboutMeMediaPresent(false)
                .ValidateWhatIsMostImportantMediaPresent(false)
                .ValidatePeopleWhoAreImportantMediaPresent(false)
                .ValidateHowICommunicateMediaPresent(false)
                .ValidatePleaseDoAndPleaseDoNotMediaPresent(false)
                .ValidateMyWellnessMediaPresent(false)
                .ValidateHowAndWhenToSupportMeMediaPresent(false)
                .ValidateAlsoWorthKnowingAboutMesMediaPresent(false)
                .ValidatePhysicalCharacteristicsMediaPresent(false);
            #endregion

            #region Step 5, 6
            personAboutMeRecordPage
                .ValidateDate(valueBeforeArchival)
                //.ValidateResponsibleUserField(responsibleUser)
                .ValidateResponsibleTeamField(responsibleTeam)
                .ValidateSupportedToWriteThisByField(supportedToWriteValueBeforeArchival)
                .ValidateCapacityEstablishedOption(false)
                .ValidateConsentGrantedForRecordingOption(false);

            string capacityArchivedValue = personAboutMeRecordPage.GetCapacityEstablishedSelectedOption();
            string consentArchivedValue = personAboutMeRecordPage.GetConsentGrantedSelectedOption();
            Assert.AreEqual(capacity, capacityArchivedValue);
            Assert.AreEqual(consent, consentArchivedValue);

            #endregion

            #endregion

            #region Step 12


            homePage
                .WaitFormHomePageToLoad(false, false, false)
                .ClickAdvanceSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ValidateSelectFieldOptionNotPresent("Person About Me");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3353")]
        [Description("Login to CD. Navigate to People > Person Record > Menu > Related Items > About Me Page" +
            "Verify that multiple records are created in the Person > Related items > About me")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "About Me")]
        [TestProperty("Screen1", "About Me")]
        public void AboutMeTests_UITestMethod002()
        {

            #region Step 8
            dbHelper.personAboutMe.CreatePersonAboutMe(DateTime.Now.AddDays(2),
                _systemUserId, _careProviders_BusinessUnitId, _careProviders_TeamId, _personID1, _personID1, true, true, "person", _person1FullName, _aboutMeSetupId);

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString(), _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());


            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .InsertDate(DateTime.UtcNow.AddDays(5).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndArchiveButton();

            personRecordPage_AboutMeArea
                .InsertDate(DateTime.UtcNow.AddDays(10).ToString("dd'/'MM'/'yyyy"))
                .SelectCapacityEstablisedOption(false)
                .SelectConsentGrantedForRecordingOption(false)
                .ClickSaveAndArchiveButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .NavigateToAboutMePage();

            personAboutMePage
                .WaitForPerson_AboutMePageToLoad()
                .ClickRefreshButton()
                .ValidateSelectedPicklistOption("Related Records")
                .ValidateRelatedRecordsCount(2);

            #endregion


        }


        [TestProperty("JiraIssueID", "ACC-3354")]
        [DeploymentItem("Files\\video.mp4")]
        [DeploymentItem("chromedriver.exe")]
        [Description("Login to CD. Verify that the video files are retained in the snapshot screen after a page reload.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "About Me")]
        [TestProperty("Screen1", "About Me")]
        public void AboutMeTests_UITestMethod003()
        {
            _personAboutMeId = dbHelper.personAboutMe.CreatePersonAboutMe(DateTime.Now,
                _systemUserId, _careProviders_BusinessUnitId, _careProviders_TeamId, _personID1, _personID1, true, true, "person", _person1FullName, _aboutMeSetupId);


            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString(), _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());


            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAboutMeTab();


            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .InsertDate(commonMethodsHelper.GetFutureDate())

                .InsertAboutMe_MyStory("About Me My Story Text")
                .UploadAboutMeMedia(TestContext.DeploymentDirectory + "\\video.mp4")
                .InsertWhatIsMostImportant_MyStory("What Is Most Important Story Text")
                .InsertPeopleWhoAreImportant_MyStory("People Who Are Important Story Text")
                .InsertHowICommunicate_MyStory("How I Communicate Story Text")
                .InsertPleaseDoAndPleaseDoNot_MyStory("Please Do And Do Not Story Text")
                .InsertMyWellness_MyStory("Wellness Story Text")
                .InsertHowAndWhenToSupportMe_MyStory("How And When To Support Story Text")
                .InsertAlsoWorthKnowingAboutMe_MyStory("Worth Knowing About Me Story Text")
                .InsertPhysicalCharacteristics_MyStory("Physical Characteristics Story Text");

            personRecordPage_AboutMeArea.ClickSaveButton(60);
            Thread.Sleep(60000);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString(), _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());


            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAboutMeTab();


            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ValidateDate(commonMethodsHelper.GetFutureDate())
                .ValidateAboutMeMediaPresent(true)
                .ValidateCapacityEstablishedOption(true)
                .ValidateConsentGrantedForRecordingOption(true)
                .ValidateSupportedToWriteThisByField(_person1FullName)
                .ValidateWhatIsMostImportantMediaPresent(false)
                .ValidatePeopleWhoAreImportantMediaPresent(false)
                .ValidateHowICommunicateMediaPresent(false)
                .ValidatePleaseDoAndPleaseDoNotMediaPresent(false)
                .ValidateMyWellnessMediaPresent(false)
                .ValidateHowAndWhenToSupportMeMediaPresent(false)
                .ValidateAlsoWorthKnowingAboutMeMediaPresent(false)
                .ValidatePhysicalCharacteristicsMediaPresent(false);


            personRecordPage_AboutMeArea
                .SelectCapacityEstablisedOption(false)
                .InsertAboutMe_MyStory("About Me My Story Text Updated")
                .InsertWhatIsMostImportant_MyStory("What Is Most Important Story Text Updated")
                .InsertPeopleWhoAreImportant_MyStory("People Who Are Important Story Text Updated")
                .InsertHowICommunicate_MyStory("How I Communicate Story Text Updated")
                .InsertPleaseDoAndPleaseDoNot_MyStory("Please Do And Do Not Story Text Updated")
                .InsertMyWellness_MyStory("Wellness Story Text Updated")
                .InsertHowAndWhenToSupportMe_MyStory("How And When To Support Story Text Updated")
                .InsertAlsoWorthKnowingAboutMe_MyStory("Worth Knowing About Me Story Text Updated")
                .InsertPhysicalCharacteristics_MyStory("Physical Characteristics Story Text Updated")
                .ClickSaveAndArchiveButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .NavigateToAboutMePage();


            personAboutMePage
                .WaitForPerson_AboutMePageToLoad()
                .ClickRefreshButton()
                .OpenPersonAboutMeRecord();


            personAboutMeRecordPage
                .WaitForPerson_AboutMeRecordPageToLoad()
                .ValidateAboutMeMediaPresent(true)
                .ValidateWhatIsMostImportantMediaPresent(false)
                .ValidatePeopleWhoAreImportantMediaPresent(false)
                .ValidateHowICommunicateMediaPresent(false)
                .ValidatePleaseDoAndPleaseDoNotMediaPresent(false)
                .ValidateMyWellnessMediaPresent(false)
                .ValidateHowAndWhenToSupportMeMediaPresent(false)
                .ValidateAlsoWorthKnowingAboutMesMediaPresent(false)
                .ValidatePhysicalCharacteristicsMediaPresent(false);



        }


        #endregion


        #region https://advancedcsg.atlassian.net/browse/CDV6-16006

        [TestProperty("JiraIssueID", "ACC-3355")]
        [Description("Login to CD. Verify that About Me Tab and Related Items > About Me module present when About Me Business module is active." +
            "Verify that When there is no About Me set up record, then error message appears in the Person > About Me tab" +
            "Verify that when user clicks on Create icon '+', user is navigated to the 'About Me Setup : New' screen and mandatory fields are displayed." +
            "Verify that when user sets the 'Hide fields' as 'Yes', the related Guidelines fields should be hidden" +
            "Verify that when the mandatory fields are left blank and user tries to save the record, the error message appears to notify user to enter the mandatory fields" +
            "Verify that once the user enters the mandatory fields and saves the record, the record is saved successfully and appears on the About me Setup list view" +
            "Verify that the title of the About Me Setup record will be as follows: 'About Me Setup Record Created By System_User_Name on Date'" +
            "Verify that when user selects the Status as Draft & saves the record, all the fields are editable except the 'Responsible Team' field." +
            "Verify that once the About me setup record is in Draft status, then when user clicks on Create New About Me setup button, Creation form does not pre-populate the values of the current setup record (Draft record)" +
            "Verify that when user selects the Status as Published & saves the record, then all the fields are non-editable.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "About Me")]
        [TestProperty("Screen1", "About Me")]
        public void AboutMeTests_UITestMethod004()
        {
            #region Step 1

            Guid aboutMeBusinessModuleId = new Guid("52d45ead-3620-ec11-a343-0050569231cf");

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickModulesButton();

            businessModulesChecklistPage
                .WaitForBusinessModulesChecklistPageToLoad()
                .ValidateModuleCheckBox(aboutMeBusinessModuleId.ToString(), true)
                .ValidateModuleLabel(aboutMeBusinessModuleId.ToString(), "About Me");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString(), _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .ValidateAboutMeTab(true)
                .ValidateAboutMe_Icon(true);
            #endregion

            #region Step 9

            var aboutMeSetupIDs = dbHelper.aboutMeSetup.GetAll(false);

            foreach (var ID in aboutMeSetupIDs)
            {
                dbHelper.aboutMeSetup.Delete(ID);
            }

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAboutMeSetupSection();

            aboutMeSetupPage
                .WaitForAboutMeSetupPageToLoad()
                .ValidateNoRecordMessageVisible(true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString(), _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ValidateNoAboutMeSetupRecordErrorMessage("No active About Me Setup record found.");
            #endregion

            #region Step 10
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAboutMeSetupSection();

            aboutMeSetupPage
                .WaitForAboutMeSetupPageToLoad()
                .ClickNewRecordButton();

            aboutMeSetupRecordPage
                .WaitForAboutMeSetupRecordPageToLoad()
                .ValidatePageHeader("About Me Setup: New")
                .ValidateResponsibleTeamMandatoryField()
                .ValidateEnableMediaContentMandatoryField()
                .ValidateStatusMandatoryField()
                .ValidateSelectedStatusField("Draft")
                .ValidateHideAboutMeSectionMandatoryField()
                .ValidateAboutMeGuidelinesMandatoryField(true)
                .ValidateHideWhatIsMostImportantToMeSectionMandatoryField()
                .ValidateWhatIsMostImportantGuidelinesMandatoryField(true)
                .ValidateHidePeopleWhoAreImportantToMeSectionMandatoryField()
                .ValidatePeopleWhoAreImportantToMeGuidelinesMandatoryField(true)
                .ValidateHideHowICommunicateAndHowToCommunicateWithMeSectionMandatoryField()
                .ValidateHowICommunicateAndHowToCommunicateWithMeGuidelinesMandatoryField(true)
                .ValidateHidePleaseDoAndPleaseDoNotSectionMandatoryField()
                .ValidatePleaseDoAndPleaseDoNotSectionGuidelinesMandatoryField(true)
                .ValidateHideMyWellnessSectionMandatoryField()
                .ValidateMyWellnessSectionGuidelinesMandatoryField(true)
                .ValidateHideHowAndWhenToSupportMeSectionMandatoryField()
                .ValidateHowAndWhenToSupportMeSectionGuidelinesMandatoryField(true)
                .ValidateHideAlsoWorthKnowingAboutSectionMandatoryField()
                .ValidateAlsoWorthKnowingAboutSectionGuidelinesMandatoryField(true)
                .ValidateHidePhysicalCharacteristicsSectionMandatoryField()
                .ValidatePhysicalCharacteristicsSectionGuidelinesMandatoryField(true);
            #endregion

            #region Step 11
            aboutMeSetupRecordPage
                .SelectHideAboutMeSectionOption(true)
                .SelectHideWhatIsMostImportantToMeSectionOption(true)
                .SelectHidePeopleWhoAreImportantToMeSectionOption(true)
                .SelectHideHowICommunicateAndHowToCommunicateSectionOption(true)
                .SelectHidePleaseDoAndPleaseDoNotSectionOption(true)
                .SelectHideMyWellnessSectionOption(true)
                .SelectHideHowAndWhenToSupportMeOption(true)
                .SelectHideAlsoWorthKnowingAboutMeSectionOption(true)
                .SelectHidePhysicalCharacteristicsSectionOption(true);

            aboutMeSetupRecordPage
                .ValidateAboutMeGuidelinesMandatoryField(false)
                .ValidateWhatIsMostImportantGuidelinesMandatoryField(false)
                .ValidatePeopleWhoAreImportantToMeGuidelinesMandatoryField(false)
                .ValidateHowICommunicateAndHowToCommunicateWithMeGuidelinesMandatoryField(false)
                .ValidatePleaseDoAndPleaseDoNotSectionGuidelinesMandatoryField(false)
                .ValidateMyWellnessSectionGuidelinesMandatoryField(false)
                .ValidateHowAndWhenToSupportMeSectionGuidelinesMandatoryField(false)
                .ValidateAlsoWorthKnowingAboutSectionGuidelinesMandatoryField(false)
                .ValidatePhysicalCharacteristicsSectionGuidelinesMandatoryField(false);

            #endregion

            #region Step 12
            aboutMeSetupRecordPage
                .SelectHideAboutMeSectionOption(false)
                .SelectHideWhatIsMostImportantToMeSectionOption(false)
                .SelectHidePeopleWhoAreImportantToMeSectionOption(false)
                .SelectHideHowICommunicateAndHowToCommunicateSectionOption(false)
                .SelectHidePleaseDoAndPleaseDoNotSectionOption(false)
                .SelectHideMyWellnessSectionOption(false)
                .SelectHideHowAndWhenToSupportMeOption(false)
                .SelectHideAlsoWorthKnowingAboutMeSectionOption(false)
                .SelectHidePhysicalCharacteristicsSectionOption(false)
                .ClickSaveButton();

            aboutMeSetupRecordPage
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateAboutMeGuidelinesFieldErrorLabelText(errorMessageText)
                .ValidateWhatIsMostImportantToMeGuidelinesFieldErrorLabelText(errorMessageText)
                .ValidatePeopleWhoAreImportantToMeGuidelinesFieldErrorLabelText(errorMessageText)
                .ValidateHowICommunicateGuidelinesFieldErrorLabelText(errorMessageText)
                .ValidatePleaseDoAndPleaseDoNotGuidelinesFieldErrorLabelText(errorMessageText)
                .ValidateMyWellnessGuidelinesFieldErrorLabelText(errorMessageText)
                .ValidateHowAndWhenToSupportGuidelinesFieldErrorLabelText(errorMessageText)
                .ValidateAlsoWorthKnowingAboutMeGuidelinesFieldErrorLabelText(errorMessageText)
                .ValidatePhysicalCharacteristicsGuidelinesFieldErrorLabelText(errorMessageText);
            #endregion

            #region Step 13 and Step 14
            aboutMeSetupRecordPage
                .InsertAboutMeGuidelines(guidelinesText)
                .InsertWhatIsMostImportantToMeGuidelines(guidelinesText)
                .InsertPeopleWhoAreImportantToMeGuidelines(guidelinesText)
                .InsertHowICommunicateAndHowToCommunicateWithMeGuidelines(guidelinesText)
                .InsertPleaseDoAndPleaseDoNotGuidelines(guidelinesText)
                .InsertMyWellnessGuidelines(guidelinesText)
                .InsertHowAndWhenToSupportMeGuidelines(guidelinesText)
                .InsertAlsoWorthKnowingAboutMeGuidelines(guidelinesText)
                .InsertPhysicalCharacteristicsGuidelines(guidelinesText)
                .ClickSaveButton()
                .WaitForAboutMeSetupRecordPageToLoad();


            string recordDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            string recordTitle = "About Me Setup: About Me Setup Record created By " + _adminUser_Name + " on " + recordDate;

            aboutMeSetupRecordPage
                .WaitForAboutMeSetupRecordPageToLoad()
                .ValidateAboutMeGuidelinesField(guidelinesText)
                .ValidateWhatIsMostImportantToMeGuidelinesField(guidelinesText)
                .ValidatePeopleWhoAreImportantToMeGuidelinesField(guidelinesText)
                .ValidateHowICommunicateGuidelinesField(guidelinesText)
                .ValidatePleaseDoAndPleaseDoNotGuidelinesField(guidelinesText)
                .ValidateMyWellnessGuidelinesField(guidelinesText)
                .ValidateHowAndWhenToSupportGuidelinesField(guidelinesText)
                .ValidateAlsoWorthKnowingAboutMeGuidelinesField(guidelinesText)
                .ValidatePhysicalCharacteristicsGuidelinesField(guidelinesText);


            aboutMeSetupRecordPage
                .ValidatePageHeader(recordTitle)
                .ClickBackButton();

            Guid record = dbHelper.aboutMeSetup.GetAll(false).FirstOrDefault();
            Console.WriteLine("Record ID:" + record.ToString());

            aboutMeSetupPage
                .WaitForAboutMeSetupPageToLoad()
                .ValidateAboutMeSetupRecordAvailable(record.ToString(), "About Me Setup Record created By " + _adminUser_Name + " on " + recordDate);
            #endregion

            #region Step 15
            aboutMeSetupPage
                .SearchAboutMeSetupRecord("About Me Setup Record created By " + _adminUser_Name)
                .OpenAboutMeSetupRecord(record.ToString());

            aboutMeSetupRecordPage
                .WaitForAboutMeSetupRecordPageToLoad()
                .ValidateResponsibleTeamFieldLookup();
            #endregion

            #region Step 16
            aboutMeSetupRecordPage
                .ClickBackButton();

            aboutMeSetupPage
                .WaitForAboutMeSetupPageToLoad()
                .ClickNewRecordButton();

            aboutMeSetupRecordPage
                .WaitForAboutMeSetupRecordPageToLoad()
                .ValidateAboutMeGuidelinesField("")
                .ValidateWhatIsMostImportantToMeGuidelinesField("")
                .ValidatePeopleWhoAreImportantToMeGuidelinesField("")
                .ValidateHowICommunicateGuidelinesField("")
                .ValidatePleaseDoAndPleaseDoNotGuidelinesField("")
                .ValidateMyWellnessGuidelinesField("")
                .ValidateHowAndWhenToSupportGuidelinesField("")
                .ValidateAlsoWorthKnowingAboutMeGuidelinesField("")
                .ValidatePhysicalCharacteristicsGuidelinesField("")
                .ValidateHideAboutMeSectionSelectedOption(false)
                .ValidateHideWhatIsMostImportantToMeSectionSelectedOption(false)
                .ValidateHidePeopleWhoAreImportantToMeSectionSelectedOption(false)
                .ValidateHideHowICommunicateAndHowToCommunicateSectionSelectedOption(false)
                .ValidateHidePleaseDoAndPleaseDoNotSectionSelectedOption(false)
                .ValidateHideMyWellnessSectionSelectedOption(false)
                .ValidateHideHowAndWhenToSupportMeSectionSelectedOption(false)
                .ValidateHidePhysicalCharacteristicsSectionSelectedOption(false);

            #endregion

            #region Step 17
            aboutMeSetupRecordPage
                .WaitForAboutMeSetupRecordPageToLoad()
                .InsertAboutMeGuidelines(guidelinesText)
                .InsertWhatIsMostImportantToMeGuidelines(guidelinesText)
                .InsertPeopleWhoAreImportantToMeGuidelines(guidelinesText)
                .InsertHowICommunicateAndHowToCommunicateWithMeGuidelines(guidelinesText)
                .InsertPleaseDoAndPleaseDoNotGuidelines(guidelinesText)
                .InsertMyWellnessGuidelines(guidelinesText)
                .InsertHowAndWhenToSupportMeGuidelines(guidelinesText)
                .InsertAlsoWorthKnowingAboutMeGuidelines(guidelinesText)
                .InsertPhysicalCharacteristicsGuidelines(guidelinesText)
                .SelectAboutMeSetupStatus("Published")
                .ClickSaveButton();

            aboutMeSetupRecordPage
                .WaitForAboutMeSetupRecordPageToLoad()
                .ValidateFieldsAreDisabled();
            #endregion



        }


        [TestProperty("JiraIssueID", "ACC-3356")]
        [Description("Person > About Me Tab > General Section." +
            "Verify that when About Me Setup > Enable Media Content = Yes, Only then in Person > About Me tab, Consent Granted for recordings field is displayed" +
            "Verify that when About Me Setup > Enable Media Content = No, then in Person > About Me tab, the Consent Granted for recordings field is not displayed" +
            "Verify that when Consent Granted for Recordings is set to Yes - then the Media Column is displayed and when it is set to No, the Media Column is hidden" +
            "When Consent Granted for Recordings is set to yes, Verify that the column name for the Each question section are as follows:" +
            "First Column - Information, Second Column - My Story, Third Column - My Recorded Story" +
            "When Consent Granted for Recordings is set to No, Verify that the column name for the Each question section are as follows:" +
            "First Column - Information, Second Column - My Story")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "About Me")]
        [TestProperty("Screen1", "About Me")]
        public void AboutMeTests_UITestMethod005()
        {
            #region Step 18

            loginPage
                    .GoToLoginPage()
                    .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString(), _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ValidateConsentGrantedForRecordingFieldPresent(true);

            #endregion

            #region Step 19

            _aboutMeSetupId = dbHelper.aboutMeSetup.CreatePersonAboutMeSetup(_careProviders_TeamId, false, 2, false, "Test", false, "Test", false, "Test",
    false, "Test", false, "Test", false, "Test", false, "Test", false, "Test", false, "Test");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString(), _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ValidateConsentGrantedForRecordingFieldPresent(false);

            #endregion

            #region Step 20
            _aboutMeSetupId = dbHelper.aboutMeSetup.CreatePersonAboutMeSetup(_careProviders_TeamId, true, 2, false, "Test", false, "Test", false, "Test",
           false, "Test", false, "Test", false, "Test", false, "Test", false, "Test", false, "Test");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString(), _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .InsertDate(commonMethodsHelper.GetFutureDate())
                .ClickSupportedToWriteThisByLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .TypeSearchQuery(_personNumber1.ToString())
                .TapSearchButton()
                .SelectResultElement(_personID1.ToString());

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ValidateConsentGrantedForRecordingFieldPresent(true)
                .SelectConsentGrantedForRecordingOption(true)
                .ClickSaveButton(10)
                .ValidateConsentGrantedForRecordingOption(true)
                .ValidateAboutMeMediaUploadField(true)
                .ValidateWhatIsMostImportantMediaUploadField(true)
                .ValidatePeopleWhoAreImportantMediaUploadField(true)
                .ValidateHowICommunicateMediaUploadField(true)
                .ValidatePleaseDoAndPleaseDoNotUploadField(true)
                .ValidateMyWellnessUploadField(true)
                .ValidateHowAndWhenToSupportMeUploadField(true)
                .ValidateAlsoWorthKnowingAboutMeUploadField(true)
                .ValidatePhysicalCharacteristicsUploadField(true);



            personRecordPage_AboutMeArea
                .SelectConsentGrantedForRecordingOption(false)
                .ClickSaveButton(10)
                .ValidateConsentGrantedForRecordingOption(false)
                .ValidateAboutMeMediaUploadField(false)
                .ValidateWhatIsMostImportantMediaUploadField(false)
                .ValidatePeopleWhoAreImportantMediaUploadField(false)
                .ValidateHowICommunicateMediaUploadField(false)
                .ValidatePleaseDoAndPleaseDoNotUploadField(false)
                .ValidateMyWellnessUploadField(false)
                .ValidateHowAndWhenToSupportMeUploadField(false)
                .ValidateAlsoWorthKnowingAboutMeUploadField(false)
                .ValidatePhysicalCharacteristicsUploadField(false);


            #endregion

            #region Step 21
            personRecordPage_AboutMeArea
                .SelectConsentGrantedForRecordingOption(true)
                .ClickSaveButton(10)
                .ValidateAboutMeSectionColumns("Information", "My Story", "My Recorded Story")
                .ValidateWhatIsMostImportantToMeSectionColumns("Information", "My Story", "My Recorded Story")
                .ValidatePeopleWhoAreImportantToMeSectionColumns("Information", "My Story", "My Recorded Story")
                .ValidateHowICommunicateAndHowToCommunicateWithMeSectionColumns("Information", "My Story", "My Recorded Story")
                .ValidatePleaseDoAndPleaseDoNoSectionColumns("Information", "My Story", "My Recorded Story")
                .ValidateMyWellnessSectionColumns("Information", "My Story", "My Recorded Story")
                .ValidateHowAndWhenToSupportMeSectionColumns("Information", "My Story", "My Recorded Story")
                .ValidateAlsoWorthKnowingAboutMeSectionColumns("Information", "My Story", "My Recorded Story")
                .ValidatePhysicalCharacteristicsSectionColumns("Information", "My Story", "My Recorded Story");
            #endregion

            #region Step 22
            personRecordPage_AboutMeArea
                .SelectConsentGrantedForRecordingOption(false)
                .ClickSaveButton(10)
                .ValidateAboutMeSectionColumns("Information", "My Story")
                .ValidateWhatIsMostImportantToMeSectionColumns("Information", "My Story")
                .ValidatePeopleWhoAreImportantToMeSectionColumns("Information", "My Story")
                .ValidateHowICommunicateAndHowToCommunicateWithMeSectionColumns("Information", "My Story")
                .ValidatePleaseDoAndPleaseDoNoSectionColumns("Information", "My Story")
                .ValidateMyWellnessSectionColumns("Information", "My Story")
                .ValidateHowAndWhenToSupportMeSectionColumns("Information", "My Story")
                .ValidateAlsoWorthKnowingAboutMeSectionColumns("Information", "My Story")
                .ValidatePhysicalCharacteristicsSectionColumns("Information", "My Story");
            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3357")]
        [Description("Tests for Person > About Me Tab" +
            "Verify that in Published About Me set up, when the Hide section is set to Yes, then in Person > About Me Tab, the Guidelines field is hidden and the respective questions are hidden in the About Me Page" +
            "Verify that in Published About Me set up, when the Hide section is set to No, then the Guidelines field  (with existing values if any) appears and the respective questions & guidelines appears in the Person > About Me Page")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "About Me")]
        [TestProperty("Screen1", "About Me")]
        public void AboutMeTests_UITestMethod006()
        {
            #region Step 23

            _aboutMeSetupId = dbHelper.aboutMeSetup.CreatePersonAboutMeSetup(_careProviders_TeamId, true, 2, true, true, true,
                true, true, true, true, true, true);

            loginPage
                    .GoToLoginPage()
                    .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName)
                    .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString(), _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ValidateAboutMeGuidelinesFieldDisplayed(false)
                .ValidateWhatIsMostImportantToMeGuidelinesFieldDisplayed(false)
                .ValidatePeopleWhoAreImportantToMeGuidelinesFieldDisplayed(false)
                .ValidateHowICommunicateAndHowToCommunicateWithMeGuidelinesFieldDisplayed(false)
                .ValidatePleaseDoAndPleaseDoNotGuidelinesFieldDisplayed(false)
                .ValidateMyWellnessGuidelinesFieldDisplayed(false)
                .ValidateHowAndWhenToSupportMeGuidelinesFieldDisplayed(false)
                .ValidateAlsoWorthKnowingAboutMeGuidelinesFieldDisplayed(false)
                .ValidatePhysicalCharacteristicsGuidelinesFieldDisplayed(false)
                .ValidateAboutMeResponseFieldDisplayed(false)
                .ValidateWhatIsMostImportantToMeResponseFieldDisplayed(false)
                .ValidatePeopleWhoAreImportantToMeResponseFieldDisplayed(false)
                .ValidateHowICommunicateAndHowToCommunicateWithMeResponseFieldDisplayed(false)
                .ValidatePleaseDoAndPleaseDoNotResponseFieldDisplayed(false)
                .ValidateMyWellnessResponseFieldDisplayed(false)
                .ValidateHowAndWhenToSupportMeResponseFieldDisplayed(false)
                .ValidateAlsoWorthKnowingAboutMeResponseFieldDisplayed(false)
                .ValidatePhysicalCharacteristicsResponseFieldDisplayed(false);


            #endregion

            #region Step 24


            _aboutMeSetupId = dbHelper.aboutMeSetup.CreatePersonAboutMeSetup(_careProviders_TeamId, false, 2, false, "Test", false, "Test", false, "Test",
    false, "Test", false, "Test", false, "Test", false, "Test", false, "Test", false, "Test");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString(), _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ValidateAboutMeGuidelinesFieldDisplayed(true)
                .ValidateWhatIsMostImportantToMeGuidelinesFieldDisplayed(true)
                .ValidatePeopleWhoAreImportantToMeGuidelinesFieldDisplayed(true)
                .ValidateHowICommunicateAndHowToCommunicateWithMeGuidelinesFieldDisplayed(true)
                .ValidatePleaseDoAndPleaseDoNotGuidelinesFieldDisplayed(true)
                .ValidateMyWellnessGuidelinesFieldDisplayed(true)
                .ValidateHowAndWhenToSupportMeGuidelinesFieldDisplayed(true)
                .ValidateAlsoWorthKnowingAboutMeGuidelinesFieldDisplayed(true)
                .ValidatePhysicalCharacteristicsGuidelinesFieldDisplayed(true)
                .ValidateAboutMeResponseFieldDisplayed(true)
                .ValidateWhatIsMostImportantToMeResponseFieldDisplayed(true)
                .ValidatePeopleWhoAreImportantToMeResponseFieldDisplayed(true)
                .ValidateHowICommunicateAndHowToCommunicateWithMeResponseFieldDisplayed(true)
                .ValidatePleaseDoAndPleaseDoNotResponseFieldDisplayed(true)
                .ValidateMyWellnessResponseFieldDisplayed(true)
                .ValidateHowAndWhenToSupportMeResponseFieldDisplayed(true)
                .ValidateAlsoWorthKnowingAboutMeResponseFieldDisplayed(true)
                .ValidatePhysicalCharacteristicsResponseFieldDisplayed(true)
                .ValidateAboutMe_Information("Test")
                .ValidateWhatIsMostImportantToMe_Information("Test")
                .ValidatePeopleWhoAreImportantToMe_Information("Test")
                .ValidateHowICommunicateAndHowToCommunicateWithMe_Information("Test")
                .ValidatePleaseDoAndPleaseDoNot_Information("Test")
                .ValidateMyWellness_Information("Test")
                .ValidateHowAndWhenToSupportMe_Information("Test")
                .ValidateAlsoWorthKnowingAboutMe_Information("Test")
                .ValidatePhysicalCharacteristics_Information("Test");
            #endregion

        }


        [TestProperty("JiraIssueID", "ACC-3358")]
        [Description("Tests for About Me Setup" +
            "Verify that Upon saving About Me Setup record with Status = Published the previous About Me setup record becomes inactive and the Status becomes Archived" +
            "Verify that 'Archived' status does not appear in the 'Status' drop-down in Create New About me setup/in Draft status record" +
            "Verify that once the About me setup record is in Published status, then when user clicks on 'Create New About Me setup' button, Creation form pre - populates the values of the current setup record(Active + Published)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "About Me")]
        [TestProperty("Screen1", "About Me")]
        public void AboutMeTests_UITestMethod007()
        {

            #region Step 26
            _aboutMeSetupId = dbHelper.aboutMeSetup.CreatePersonAboutMeSetup(_careProviders_TeamId, false, 2, false, "Test 1", false, "Test 1", false, "Test 1",
    false, "Test 1", false, "Test 1", false, "Test 1", false, "Test 1", false, "Test 1", false, "Test 1");

            loginPage
                    .GoToLoginPage()
                    .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName)
                    .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAboutMeSetupSection();

            aboutMeSetupPage
                .WaitForAboutMeSetupPageToLoad()
                .ClickNewRecordButton();

            aboutMeSetupRecordPage
                .WaitForAboutMeSetupRecordPageToLoad()
                .SelectAboutMeSetupStatus("Published")
                .InsertAboutMeGuidelines("Test 2")
                .InsertWhatIsMostImportantToMeGuidelines("Test 2")
                .InsertPeopleWhoAreImportantToMeGuidelines("Test 2")
                .InsertHowICommunicateAndHowToCommunicateWithMeGuidelines("Test 2")
                .InsertPleaseDoAndPleaseDoNotGuidelines("Test 2")
                .InsertMyWellnessGuidelines("Test 2")
                .InsertHowAndWhenToSupportMeGuidelines("Test 2")
                .InsertAlsoWorthKnowingAboutMeGuidelines("Test 2")
                .InsertPhysicalCharacteristicsGuidelines("Test 2")
                .ClickSaveButton()
                .ClickBackButton();

            aboutMeSetupPage
                .WaitForAboutMeSetupPageToLoad()
                .SelectAvailableViewByText("Inactive Records")
                .OpenAboutMeSetupRecord(_aboutMeSetupId.ToString());

            aboutMeSetupRecordPage
                .WaitForAboutMeSetupRecordPageToLoad()
                .ValidateRecordActiveStatus("No")
                .ValidateSelectedStatusField("Archived");

            #endregion

            #region Step 27
            aboutMeSetupRecordPage
                .ClickBackButton();

            aboutMeSetupPage
                .WaitForAboutMeSetupPageToLoad()
                .SelectAvailableViewByText("Active Records")
                .ClickNewRecordButton();

            aboutMeSetupRecordPage
                .WaitForAboutMeSetupRecordPageToLoad()
                .ValidateSelectedStatusField("Draft")
                .ValidateStatusFieldOptionNotPresent("Archived");

            #endregion

            #region Step 28            
            aboutMeSetupRecordPage
                .ValidateHideAboutMeSectionSelectedOption(false)
                .ValidateHideWhatIsMostImportantToMeSectionSelectedOption(false)
                .ValidateHidePeopleWhoAreImportantToMeSectionSelectedOption(false)
                .ValidateHideHowICommunicateAndHowToCommunicateSectionSelectedOption(false)
                .ValidateHidePleaseDoAndPleaseDoNotSectionSelectedOption(false)
                .ValidateHideMyWellnessSectionSelectedOption(false)
                .ValidateHideHowAndWhenToSupportMeSectionSelectedOption(false)
                .ValidateHideAlsoWorthKnowingAboutMeSectionSelectedOption(false)
                .ValidateHidePhysicalCharacteristicsSectionSelectedOption(false)
                .ValidateAboutMeGuidelinesField("Test 2")
                .ValidateWhatIsMostImportantToMeGuidelinesField("Test 2")
                .ValidatePeopleWhoAreImportantToMeGuidelinesField("Test 2")
                .ValidateHowICommunicateGuidelinesField("Test 2")
                .ValidatePleaseDoAndPleaseDoNotGuidelinesField("Test 2")
                .ValidateMyWellnessGuidelinesField("Test 2")
                .ValidateHowAndWhenToSupportGuidelinesField("Test 2")
                .ValidateAlsoWorthKnowingAboutMeGuidelinesField("Test 2")
                .ValidatePhysicalCharacteristicsGuidelinesField("Test 2");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-3359")]
        [Description("Tests for About Me Setup and Person > About Me Tab" +
            "Verify that input fields on the About Me setup page is text area field types. (i.e) when user enters the following basic syntax in the input field in the About Me Setup record, the same format has to be reflected in the Person > About Me Tab")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "About Me")]
        [TestProperty("Screen1", "About Me")]
        public void AboutMeTests_UITestMethod008()
        {
            #region Step 25
            _aboutMeSetupId = dbHelper.aboutMeSetup.CreatePersonAboutMeSetup(_careProviders_TeamId, false, 2, false, "# Test", false, "## Test", false, "Test",
    false, "**Test**", false, "*Test*", false, "1. Test1", false, "- Test1", false, "Test1<br> Test2", false, "Test");

            loginPage
                    .GoToLoginPage()
                    .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName)
                    .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString(), _personID1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAboutMeTab();

            personRecordPage_AboutMeArea
                .WaitForPersonRecordPage_AboutMeAreaToLoad()
                .ValidateHeadingLevel1("Test")
                .ValidateHeadingLevel2("Test")
                .ValidateParagraphText("Test")
                .ValidateBoldText("Test")
                .ValidateItalicizedText("Test")
                .ValidateOrderedListElements(1)
                .ValidateUnorderedListElements(1)
                .ValidateLinebreakText("Test1" + System.Environment.NewLine + "Test2");
            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3360")]
        [Description("Tests for About Me Setup Page > Menu > Related Items > Audit" +
            "Verify that the audit is captured for the creation/updation of the About Me Setup record (About Me Setup record > Related items > Audit)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "About Me")]
        [TestProperty("Screen1", "About Me")]
        public void AboutMeTests_UITestMethod009()
        {
            #region Step 29

            var aboutMeSetupIDs = dbHelper.aboutMeSetup.GetAll(false);

            foreach (var ID in aboutMeSetupIDs)
            {
                dbHelper.aboutMeSetup.Delete(ID);
            }

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_2", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAboutMeSetupSection();

            aboutMeSetupPage
                .WaitForAboutMeSetupPageToLoad()
                .ClickNewRecordButton();

            aboutMeSetupRecordPage
                .WaitForAboutMeSetupRecordPageToLoad()
                .SelectAboutMeSetupStatus("Draft")
                .InsertAboutMeGuidelines("Audit Test")
                .InsertWhatIsMostImportantToMeGuidelines("Audit Test")
                .InsertPeopleWhoAreImportantToMeGuidelines("Audit Test")
                .InsertHowICommunicateAndHowToCommunicateWithMeGuidelines("Audit Test")
                .InsertPleaseDoAndPleaseDoNotGuidelines("Audit Test")
                .InsertMyWellnessGuidelines("Audit Test")
                .InsertHowAndWhenToSupportMeGuidelines("Audit Test")
                .InsertAlsoWorthKnowingAboutMeGuidelines("Audit Test")
                .InsertPhysicalCharacteristicsGuidelines("Audit Test")
                .ClickSaveButton()
                .NavigateToAuditPage();

            auditListPage
                .WaitForAuditListPageToLoad("aboutmesetup")
                .ClickOnAuditRecordText("Created");

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Created")
                .ValidateChangedBy(_adminUser_Name)
                .TapCloseButton();

            aboutMeSetupRecordPage
                .WaitForAboutMeSetupRecordPageToLoad()
                .ClickDetailsTab()
                .SelectAboutMeSetupStatus("Published")
                .ClickSaveButton()
                .NavigateToAuditPage();

            auditListPage
                .WaitForAuditListPageToLoad("aboutmesetup")
                .ClickOnAuditRecordText("Updated");

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy(_adminUser_Name)
                .TapCloseButton();

            #endregion

        }

        #endregion

    }


}

