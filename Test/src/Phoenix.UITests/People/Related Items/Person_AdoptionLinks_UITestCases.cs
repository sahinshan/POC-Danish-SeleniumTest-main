using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.People.Related_Items
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    /// 
    [TestClass]
    public class Person_AdoptionLinks_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUserName;
        private string _systemUserFullName;
        private Guid _personId;
        private Guid _personId2;
        private int _personNumber;
        private int _personNumber2;
        private string _person_fullName;
        private string _person_fullName2;
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

                _systemUserName = "Person_Adoption_User1";
                commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Adoption", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                _systemUserFullName = "Person Adoption User1";

                #endregion

                #region Person

                var firstName = "First";
                var lastName = "LN_" + _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _person_fullName = firstName + " " + lastName;


                var firstName2 = "Second";
                var lastName2 = "LN_" + _currentDateSuffix;
                _personId2 = commonMethodsDB.CreatePersonRecord(firstName2, lastName2, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
                _personNumber2 = (int)dbHelper.person.GetPersonById(_personId2, "personnumber")["personnumber"];
                _person_fullName2 = firstName2 + " " + lastName2;

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-11950

        [TestProperty("JiraIssueID", "CDV6-2720")]
        [Description("Create a new Post Adoption Link record - Validate that the record is accessible after saving it")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PostAdoptionLinks_UITestMethod01()
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
                .NavigateToPersonPostAdoptionLinkPage();

            personPostAdoptionLinksPage
                .WaitForPersonPostAdoptionLinksPageToLoad()
                .ClickNewRecordButton();

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ClickPreAdoptionPersonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery(_personNumber2.ToString()).TapSearchButton().SelectResultElement(_personId2.ToString());

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personPostAdoptionLinksPage
                .WaitForPersonPostAdoptionLinksPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var adoptionRecords = dbHelper.adoptionLink.GetByPersonID(_personId);
            Assert.AreEqual(1, adoptionRecords.Count);

            personPostAdoptionLinksPage
                .OpenPersonAdoptionLinksRecord(adoptionRecords[0].ToString());

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ValidatePreAdoptionPersonLinkText(_person_fullName2)
                .ValidatePostAdoptionPersonLinkText(_person_fullName)
                .ValidateResponsibleTeamLinkText("CareDirector QA")
                .ValidateResponsibleUserLinkText(_systemUserFullName);

        }

        [TestProperty("JiraIssueID", "CDV6-8788")]
        [Description("Create a new Pre Adoption Link record - Validate that the record is accessible after saving it - Navigate to the Pre Adoption Person - " +
            "Validate that the same record is displayed in the Pre Adoption records area")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PreAdoptionLinks_UITestMethod01()
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
                .NavigateToPersonPreAdoptionLinkPage();

            personPreAdoptionLinksPage
                .WaitForPersonPreAdoptionLinksPageToLoad()
                .ClickNewRecordButton();

            personPreAdoptionLinkRecordPage
                .WaitForPersonPreAdoptionLinkRecordPageToLoad()

                .ValidateMessageAreaVisible(false)
                .ValidatePostAdoptionPersonErrorAreaVisibility(false)

                .ClickSaveAndCloseButton()

                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")

                .ValidatePostAdoptionPersonErrorAreaVisibility(true)
                .ValidatePostAdoptionPersonErrorAreaText("Please fill out this field.")

                .ClickPostAdoptionPersonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery(_personNumber2.ToString()).TapSearchButton().SelectResultElement(_personId2.ToString());

            personPreAdoptionLinkRecordPage
                .WaitForPersonPreAdoptionLinkRecordPageToLoad()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            personPreAdoptionLinksPage
                .WaitForPersonPreAdoptionLinksPageToLoad();

            var adoptionRecords = dbHelper.adoptionLink.GetByPreAdoptionPersonId(_personId);
            Assert.AreEqual(1, adoptionRecords.Count);

            personPreAdoptionLinksPage
                .OpenPersonAdoptionLinksRecord(adoptionRecords[0].ToString());

            personPreAdoptionLinkRecordPage
                .WaitForPersonPreAdoptionLinkRecordPageToLoad()
                .ValidatePreAdoptionPersonLinkText(_person_fullName)
                .ValidatePostAdoptionPersonLinkText(_person_fullName2)
                .ValidateResponsibleTeamLinkText("CareDirector QA")
                .ValidateResponsibleUserLinkText(_systemUserFullName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber2.ToString(), _personId2.ToString())
                .OpenPersonRecord(_personId2.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonPostAdoptionLinkPage();

            personPostAdoptionLinksPage
                .WaitForPersonPostAdoptionLinksPageToLoad()
                .ValidateRecordCellText(adoptionRecords[0].ToString(), 2, _person_fullName)
                .ValidateRecordCellText(adoptionRecords[0].ToString(), 3, _person_fullName2)
                .ValidateRecordCellText(adoptionRecords[0].ToString(), 4, "CareDirector QA")
                .ValidateRecordCellText(adoptionRecords[0].ToString(), 5, _systemUserFullName);

        }

        [TestProperty("JiraIssueID", "CDV6-2724")]
        [Description("Create a new Post Adoption Link record - Validate that the record is visible in the list page after saving")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PostAdoptionLinks_UITestMethod02()
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
                .NavigateToPersonPostAdoptionLinkPage();

            personPostAdoptionLinksPage
                .WaitForPersonPostAdoptionLinksPageToLoad()
                .ClickNewRecordButton();

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ClickPreAdoptionPersonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery(_personNumber2.ToString()).TapSearchButton().SelectResultElement(_personId2.ToString());

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personPostAdoptionLinksPage
                .WaitForPersonPostAdoptionLinksPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var adoptionRecords = dbHelper.adoptionLink.GetByPersonID(_personId);
            Assert.AreEqual(1, adoptionRecords.Count);

            personPostAdoptionLinksPage
                .ValidateRecordCellText(adoptionRecords[0].ToString(), 2, _person_fullName2)
                .ValidateRecordCellText(adoptionRecords[0].ToString(), 3, _person_fullName)
                .ValidateRecordCellText(adoptionRecords[0].ToString(), 4, "CareDirector QA")
                .ValidateRecordCellText(adoptionRecords[0].ToString(), 5, _systemUserFullName)
                .ValidateRecordCellText(adoptionRecords[0].ToString(), 6, _systemUserFullName)
                .ValidateRecordCellText(adoptionRecords[0].ToString(), 8, _systemUserFullName);

        }

        [TestProperty("JiraIssueID", "CDV6-2722")]
        [Description("Create a new Post Adoption Link record - Reopen it and update the Pre Adoption person and Responsible User fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PostAdoptionLinks_UITestMethod03()
        {
            #region System User Record

            var _systemUserName2 = "Person_Adoption_User2";
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_systemUserName2, "Person Adoption", "User2", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            var _systemUserFullName2 = "Person Adoption User2";

            #endregion

            #region Person 3

            var firstName3 = "Third";
            var lastName3 = "LN_" + _currentDateSuffix;
            var _personId3 = commonMethodsDB.CreatePersonRecord(firstName3, lastName3, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
            var _personNumber3 = (int)dbHelper.person.GetPersonById(_personId3, "personnumber")["personnumber"];
            var _person_fullName3 = firstName3 + " " + lastName3;

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
                .NavigateToPersonPostAdoptionLinkPage();

            personPostAdoptionLinksPage
                .WaitForPersonPostAdoptionLinksPageToLoad()
                .ClickNewRecordButton();

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ClickPreAdoptionPersonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery(_personNumber2.ToString()).TapSearchButton().SelectResultElement(_personId2.ToString());

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            personPostAdoptionLinksPage
                .WaitForPersonPostAdoptionLinksPageToLoad();

            var adoptionRecords = dbHelper.adoptionLink.GetByPersonID(_personId);
            Assert.AreEqual(1, adoptionRecords.Count);

            personPostAdoptionLinksPage
                .OpenPersonAdoptionLinksRecord(adoptionRecords[0].ToString());

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ClickPreAdoptionPersonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery(_personNumber3.ToString()).TapSearchButton().SelectResultElement(_personId3.ToString());

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ClickResponsibleUserLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad(20).TypeSearchQuery(_systemUserName2).TapSearchButton().SelectResultElement(_systemUserId2.ToString());

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personPostAdoptionLinksPage
                .WaitForPersonPostAdoptionLinksPageToLoad()
                .OpenPersonAdoptionLinksRecord(adoptionRecords[0].ToString());

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ValidatePreAdoptionPersonLinkText(_person_fullName3)
                .ValidatePostAdoptionPersonLinkText(_person_fullName)
                .ValidateResponsibleTeamLinkText("CareDirector QA")
                .ValidateResponsibleUserLinkText(_systemUserFullName2);

        }

        [TestProperty("JiraIssueID", "CDV6-2721")]
        [Description("Create a new record using the Advanced Search window")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PostAdoptionLinks_UITestMethod04()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            homePage
                .ClickAdvanceSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Adoption Links")
                .WaitForAdvanceSearchPageToLoad()
                .SelectFilter("1", "Post-Adoption Person")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoadFromAdvancedSearch()
                .ClickPreAdoptionPersonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery(_personNumber2.ToString()).TapSearchButton().SelectResultElement(_personId2.ToString());

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoadFromAdvancedSearch()
                .ClickPostAdoptionPersonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoadFromAdvancedSearch()
                .ClickSaveAndCloseButton();

            advanceSearchPage
                .WaitForResultsPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var adoptionRecords = dbHelper.adoptionLink.GetByPersonID(_personId);
            Assert.AreEqual(1, adoptionRecords.Count);

            advanceSearchPage
                .ValidateSearchResultRecordPresent(adoptionRecords[0].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-2723")]
        [Description("Create a new Post Adoption Link record using the same person record in the pre and post adoption person fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PostAdoptionLinks_UITestMethod05()
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
                .NavigateToPersonPostAdoptionLinkPage();

            personPostAdoptionLinksPage
                .WaitForPersonPostAdoptionLinksPageToLoad()
                .ClickNewRecordButton();

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ClickPreAdoptionPersonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad(20).SelectViewByText("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personId.ToString());

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personPostAdoptionLinksPage
                .WaitForPersonPostAdoptionLinksPageToLoad();

            System.Threading.Thread.Sleep(2000);
            var adoptionRecords = dbHelper.adoptionLink.GetByPersonID(_personId);
            Assert.AreEqual(1, adoptionRecords.Count);

            personPostAdoptionLinksPage
                .OpenPersonAdoptionLinksRecord(adoptionRecords[0].ToString());

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()
                .ValidatePreAdoptionPersonLinkText(_person_fullName)
                .ValidatePostAdoptionPersonLinkText(_person_fullName)
                .ValidateResponsibleTeamLinkText("CareDirector QA")
                .ValidateResponsibleUserLinkText(_systemUserFullName);

        }

        [TestProperty("JiraIssueID", "CDV6-2719")]
        [Description("Try to create a new record without the mandatory fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_PostAdoptionLinks_UITestMethod06()
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
                .NavigateToPersonPostAdoptionLinkPage();

            personPostAdoptionLinksPage
                .WaitForPersonPostAdoptionLinksPageToLoad()
                .ClickNewRecordButton();

            personPostAdoptionLinkRecordPage
                .WaitForPersonPostAdoptionLinkRecordPageToLoad()

                .ValidateMessageAreaVisible(false)
                .ValidatePreAdoptionPersonErrorAreaVisibility(false)
                .ValidatePostAdoptionPersonErrorAreaVisibility(false)
                .ValidateResponsibleTeamErrorAreaVisibility(false)
                .ValidateResponsibleUserErrorAreaVisibility(false)

                .ClickPostAdoptionPersonRemoveButton()
                .ClickResponsibleTeamRemoveButton()
                .ClickResponsibleUserRemoveButton()
                .ClickSaveAndCloseButton()

                .ValidateMessageAreaVisible(true)
                .ValidatePreAdoptionPersonErrorAreaVisibility(true)
                .ValidatePostAdoptionPersonErrorAreaVisibility(true)
                .ValidateResponsibleTeamErrorAreaVisibility(true)
                .ValidateResponsibleUserErrorAreaVisibility(true)

                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidatePreAdoptionPersonErrorAreaText("Please fill out this field.")
                .ValidatePostAdoptionPersonErrorAreaText("Please fill out this field.")
                .ValidateResponsibleTeamErrorAreaText("Please fill out this field.")
                .ValidateResponsibleUserErrorAreaText("Please fill out this field.");

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






