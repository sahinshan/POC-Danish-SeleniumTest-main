using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Configuration.ReferenceData
{
    [TestClass]
    public class Pronouns_UITestCases : FunctionalTest
    {
        #region Private Properties
        private string EnvironmentName;
        private string _tenantName;
        private string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _businessUnitId;
        private Guid _productLanguageId;
        private Guid _teamId;
        private Guid _systemUserId;
        private Guid _authenticationproviderid;
        private string _username;
        private Guid _pronoun_HeHim;
        private Guid _pronoun_SheHer;
        private Guid _pronoun_TheyThem;
        private Guid _pronoun_WeUs;

        #endregion

        [TestInitialize()]
        public void PronounsTestSetup()
        {
            try
            {
                #region Tenant

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareProviders", null, _businessUnitId, null, "CareProviderQA@careworkstempmail.com", "Default team for business unit", null);

                #endregion

                #region Product Language

                _productLanguageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion

                #region Create SystemUser Record
                _username = "ACC3634User";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_username, "ACC", "3634User", "Passw0rd_!", _businessUnitId, _teamId, _productLanguageId, _authenticationproviderid);

                #endregion

                #region Pronouns
                _pronoun_HeHim = dbHelper.pronouns.GetByName("He/Him").First();
                _pronoun_SheHer = dbHelper.pronouns.GetByName("She/Her").First();
                _pronoun_TheyThem = dbHelper.pronouns.GetByName("They/Them").First();

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-3636

        [TestProperty("JiraIssueID", "ACC-3651")]
        [Description("Testcase for CDV6-17250 - Capture Pronouns on Person, Applicant and System User")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen1", "Pronouns")]
        [TestProperty("Screen2", "System Users")]
        public void Pronouns_UITestMethod001()
        {
            #region Step 1          

            loginPage
              .GoToLoginPage()
              .Login(_username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Pronouns")
                .TapSearchButton()
                .ValidateReferenceDataMainHeaderVisibility("Person", true)
                .ClickReferenceDataMainHeader("Person");

            referenceDataPage
                .ValidateReferenceDataElementVisibility("Pronouns", true);

            #endregion

            #region Step 2

            referenceDataPage
                .ClickReferenceDataElement("Pronouns");

            pronounsPage
                .WaitForPronounsPageToLoad()
                .ValidateRecordIsDisplayed(_pronoun_HeHim.ToString())
                .ValidateRecordIsDisplayed(_pronoun_SheHer.ToString())
                .ValidateRecordIsDisplayed(_pronoun_TheyThem.ToString())
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("pronouns")
                .ClickOnExpandIcon();

            pronounsRecordPage
                .WaitForPronounsRecordPageToLoad()
                .InsertName("We/Us_" + currentDateTime)
                .InsertStartDate("01/01/2023")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            pronounsRecordPage
                .WaitForPronounsRecordPageToLoad()
                .ValidateNameFieldValue("We/Us_" + currentDateTime)
                .ValidateStartDateFieldValue("01/01/2023");

            #endregion

            #region Step 3

            Guid _pronoun_WeUs = dbHelper.pronouns.GetByName("We/Us_" + currentDateTime)[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_username)
                .ClickSearchButton()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidatePronounsLookupButtonVisible(true)
                .ClickPronounsLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndValidateRecordPresentOrNot("He/Him", _pronoun_HeHim, true)
                .SearchAndValidateRecordPresentOrNot("She/Her", _pronoun_SheHer, true)
                .SearchAndValidateRecordPresentOrNot("They/Them", _pronoun_TheyThem, true)
                .SearchAndValidateRecordPresentOrNot("We/Us_" + currentDateTime, _pronoun_WeUs, true)
                .SelectResultElement(_pronoun_WeUs.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .InsertAvailableFromDateField("01/01/2023")
                .ClickSaveButton()
                .ValidatePronounsLinkFieldText("We/Us_" + currentDateTime);

            #endregion

            #region Step 4

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectEmployeeType("System Administrator")
                .InsertFirstName("FN")
                .InsertLastName("LN_" + currentDateTime)
                .ClickRequiresLogin_NoOption()
                .SelectBusinessUnitByText("CareProviders")
                .ClickDefaultTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CareProviders")
                .TapSearchButton()
                .SelectResultElement(_teamId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertAvailableFromDateField("01/01/2022")
                .ClickPronounsLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndValidateRecordPresentOrNot("He/Him", _pronoun_HeHim, true)
                .SearchAndValidateRecordPresentOrNot("She/Her", _pronoun_SheHer, true)
                .SearchAndValidateRecordPresentOrNot("They/Them", _pronoun_TheyThem, true)
                .SearchAndValidateRecordPresentOrNot("We/Us_" + currentDateTime, _pronoun_WeUs, true)
                .SelectResultElement(_pronoun_WeUs.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .ClickSaveButton()
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidatePronounsLinkFieldText("We/Us_" + currentDateTime);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3652")]
        [Description("Testcase for CDV6-17250 - Capture Pronouns on Person, Applicant and System User")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "People")]
        public void Pronouns_UITestMethod002()
        {
            #region Step 5

            var _pronoun_IMe = commonMethodsDB.CreatePronouns(_teamId, _businessUnitId, "I/Me_" + currentDateTime, null, new DateTime(2023, 1, 1));
            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));
            var _personID = dbHelper.person.CreatePersonRecord("", "FN", "", "LN_" + currentDateTime, "", new DateTime(1990, 1, 1), _ethnicityId, _teamId, 7, 2, 4);
            dbHelper.person.UpdateDOBAndAgeTypeId(_personID, 5);
            dbHelper.person.UpdatePreferredInvoiceDeliveryMethod(_personID, 2);

            loginPage
              .GoToLoginPage()
              .Login(_username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName("LN_" + currentDateTime)
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_personID.ToString(), "FN LN_" + currentDateTime)
                .ValidatePronounsLookupButtonVisible(true)
                .ClickPronounsLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndValidateRecordPresentOrNot("He/Him", _pronoun_HeHim, true)
                .SearchAndValidateRecordPresentOrNot("She/Her", _pronoun_SheHer, true)
                .SearchAndValidateRecordPresentOrNot("They/Them", _pronoun_TheyThem, true)
                .SearchAndValidateRecordPresentOrNot("I/Me_" + currentDateTime, _pronoun_IMe, true)
                .SelectResultElement(_pronoun_IMe.ToString());

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(_personID.ToString(), "FN LN_" + currentDateTime)
                .ClickSaveButton()
                .WaitForEditPersonRecordPopupToLoad(_personID.ToString(), "FN LN_" + currentDateTime)
                .ValidatePronounsLinkFieldText("I/Me_" + currentDateTime)
                .ClickCloseButton();

            #endregion

            #region Step 6

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("LN2_" + currentDateTime)
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectPersonType("Person We Support")
                .InsertTitle("Mr")
                .InsertFirstName("FN2")
                .InsertMiddleName("MN")
                .InsertLastName("LN2_" + currentDateTime)
                .SelectStatedGender("Male")
                .SelectDOBAndAge("DOB")
                .InsertDOB("01/01/1980")
                .ClickEthnicityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Irish")
                .TapSearchButton()
                .SelectResultElement(_ethnicityId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertStartDateOfAddress("01/01/2023")
                .SelectAddressType("Home")
                .ClickPronounsLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .SearchAndValidateRecordPresentOrNot("He/Him", _pronoun_HeHim, true)
                .SearchAndValidateRecordPresentOrNot("She/Her", _pronoun_SheHer, true)
                .SearchAndValidateRecordPresentOrNot("They/Them", _pronoun_TheyThem, true)
                .SearchAndValidateRecordPresentOrNot("I/Me_" + currentDateTime, _pronoun_IMe, true)
                .SelectResultElement(_pronoun_IMe.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("CareProviders")
                .TapSearchButton()
                .SelectResultElement(_teamId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectLivesInSmokinHouseholdFieldValue("No")
                .SelectPetsFieldValue("No Pets")
                .TapSaveButton();

            System.Threading.Thread.Sleep(3000);

            personRecordPage
                .WaitForPersonRecordPageToLoadAfterSave()
                .TapEditButton();

            var newPersonId = dbHelper.person.GetByFirstAndLastName("FN2", "LN2_" + currentDateTime)[0];

            personRecordEditPage
                .WaitForEditPersonRecordPopupToLoad(newPersonId.ToString(), "FN2 LN2_" + currentDateTime)
                .ValidatePronounsLinkFieldText("I/Me_" + currentDateTime);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3655")]
        [Description("Testcase for CDV6-17250 - Capture Pronouns on Person, Applicant and System User")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Applicants")]
        public void Pronouns_UITestMethod003()
        {
            #region Step 7

            _pronoun_WeUs = commonMethodsDB.CreatePronouns(_teamId, _businessUnitId, "We/Us_" + currentDateTime, null, new DateTime(2023, 1, 1));

            #region Create Applicant

            var applicantFirstName = "Test_3655";
            var applicantLastName = "User_" + currentDateTime;
            var _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _teamId);

            #endregion

            loginPage
              .GoToLoginPage()
              .Login(_username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ClickPronounsLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndValidateRecordPresentOrNot("He/Him", _pronoun_HeHim, true)
                .SearchAndValidateRecordPresentOrNot("She/Her", _pronoun_SheHer, true)
                .SearchAndValidateRecordPresentOrNot("They/Them", _pronoun_TheyThem, true)
                .SearchAndValidateRecordPresentOrNot("We/Us_" + currentDateTime, _pronoun_WeUs, true)
                .SelectResultElement(_pronoun_WeUs.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertAvailableFromDateField("01/01/2023")
                .ClickSaveButton()
                .WaitForApplicantRecordPagePageToLoad()
                .ValidatePronounFieldLinkText("We/Us_" + currentDateTime);
            #endregion

            #region Step 8

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName("FName3")
                .InsertLastName("LN3_" + currentDateTime)
                .InsertAvailableFromDateField("01/01/2020")
                .ClickPronounsLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndValidateRecordPresentOrNot("He/Him", _pronoun_HeHim, true)
                .SearchAndValidateRecordPresentOrNot("She/Her", _pronoun_SheHer, true)
                .SearchAndValidateRecordPresentOrNot("They/Them", _pronoun_TheyThem, true)
                .SearchAndValidateRecordPresentOrNot("We/Us_" + currentDateTime, _pronoun_WeUs, true)
                .SelectResultElement(_pronoun_WeUs.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ClickSaveButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateFirstName("FName3")
                .ValidateLastName("LN3_" + currentDateTime)
                .ValidateAvailableFrom("01/01/2020")
                .ValidatePronounFieldLinkText("We/Us_" + currentDateTime);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3658")]
        [Description("Testcase for CDV6-17250 - Capture Pronouns on Person, Applicant and System User")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen", "Advanced Search")]
        public void Pronouns_UITestMethod004()
        {
            #region Step 9

            var _pronoun_You = commonMethodsDB.CreatePronouns(_teamId, _businessUnitId, "You/You" + currentDateTime, null, new DateTime(2023, 1, 1));

            #region System User
            string _username2 = "ACC3658User" + currentDateTime;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_username2, "ACC3658", "User" + currentDateTime, "Passw0rd_!", _businessUnitId, _teamId, _productLanguageId, _authenticationproviderid);
            dbHelper.systemUser.UpdatePronoun(_systemUserId2, _pronoun_TheyThem);

            #endregion

            #region Person
            var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];
            var _personID = dbHelper.person.CreatePersonRecord("", "FN", "", "LN_" + currentDateTime, "", new DateTime(1990, 1, 1), _ethnicityId, _teamId, 7, 2, 3);
            dbHelper.person.UpdateDOBAndAgeTypeId(_personID, 5);
            dbHelper.person.UpdatePreferredInvoiceDeliveryMethod(_personID, 2);
            dbHelper.person.UpdatePronoun(_personID, _pronoun_You);

            #endregion

            #region Applicant
            var applicantFirstName = "Test_3658";
            var applicantLastName = "ZUser_" + currentDateTime;
            var _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _teamId);
            dbHelper.applicant.UpdatePronoun(_applicantId, _pronoun_HeHim);

            #endregion

            loginPage
              .GoToLoginPage()
              .Login(_username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Applicants")
                .SelectFilter("1", "Pronouns")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("He/Him")
                .TapSearchButton()
                .SelectResultElement(_pronoun_HeHim.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_applicantId.ToString());

            advanceSearchPage
               .WaitForResultsPageToLoad()
               .OpenRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPageToLoadFromAdvancedSearch()
                .ValidatePageHeaderTitle(applicantFirstName + " " + applicantLastName);

            #endregion

            #region Step 10

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("People")
                .SelectFilter("1", "Pronouns")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("You/You" + currentDateTime)
                .TapSearchButton()
                .SelectResultElement(_pronoun_You.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_personID.ToString());

            advanceSearchPage
               .WaitForResultsPageToLoad()
               .OpenRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoadFromAdvancedSearch()
                .ValidatePersonPageHeaderTitle("FN LN_" + currentDateTime);

            #endregion

            #region Step 11

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System Users")
                .SelectFilter("1", "System User")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("ACC3658 User" + currentDateTime)
                .TapSearchButton()
                .SelectResultElement(_systemUserId2.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickAddRuleButton(1)
                .SelectFilter("2", "Pronouns")
                .SelectOperator("2", "Equals")
                .ClickRuleValueLookupButton("2");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("They/Them")
                .TapSearchButton()
                .SelectResultElement(_pronoun_TheyThem.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_systemUserId2.ToString());

            advanceSearchPage
               .WaitForResultsPageToLoad()
               .OpenRecord(_systemUserId2.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoadFromAdvancedSearch()
                .ValidateSystemUserRecordTitle("ACC3658 User" + currentDateTime);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3669")]
        [Description("Testcase for CDV6-17250 - Capture Pronouns on Person, Applicant and System User")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen1", "Business Objects")]
        [TestProperty("Screen2", "Data Forms")]
        public void Pronouns_UITestMethod005()
        {
            #region Step 14

            loginPage
              .GoToLoginPage()
              .Login(_username, "Passw0rd_!", EnvironmentName);

            var _applicantBOId = dbHelper.businessObject.GetBusinessObjectByName("Applicant")[0];
            var _applicantDataFormId = dbHelper.dataForm.GetByName("Applicant: Main")[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickBusinessObjectsButton();

            businessObjectsPage
                .WaitForBusinessObjectsPageToLoad()
                .InsertQuickSearchText("*Applicant*")
                .ClickQuickSearchButton()
                .WaitForBusinessObjectsPageToLoad()
                .OpenRecord(_applicantBOId.ToString());

            businessObjectRecordPage
                .WaitForBusinessObjectRecordPageToLoad()
                .NavigateToDataFormsSubArea();

            dataFormsPage
                .WaitForDataFormsPageToLoad()
                .ValidateRecordIsPresent(_applicantDataFormId.ToString())
                .OpenRecord(_applicantDataFormId.ToString());

            dataFormRecordPage
                .WaitForDataFormRecordPageToLoad()
                .ClickEditButton();

            dataFormEditPage
                .WaitForDataFormEditPageToLoad()
                .ClickPronounFieldEditButton();

            dataFormFieldPage
                .WaitForDataFormFieldPageToLoad()
                .ValidateUsedInCodeNoOptionSelected(true)
                .ClickBackButton();

            #endregion

            #region Step 13

            var _systemUserBOId = dbHelper.businessObject.GetBusinessObjectByName("SystemUser")[0];
            var _systemUserDataFormId = dbHelper.dataForm.GetByName("SystemUser: Main")[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickBusinessObjectsButton();

            businessObjectsPage
                .WaitForBusinessObjectsPageToLoad()
                .InsertQuickSearchText("SystemUser")
                .ClickQuickSearchButton()
                .WaitForBusinessObjectsPageToLoad()
                .OpenRecord(_systemUserBOId.ToString());

            businessObjectRecordPage
                .WaitForBusinessObjectRecordPageToLoad()
                .NavigateToDataFormsSubArea();

            dataFormsPage
                .WaitForDataFormsPageToLoad()
                .ValidateRecordIsPresent(_systemUserDataFormId.ToString())
                .OpenRecord(_systemUserDataFormId.ToString());

            dataFormRecordPage
                .WaitForDataFormRecordPageToLoad()
                .ClickEditButton();

            dataFormEditPage
                .WaitForDataFormEditPageToLoad()
                .ClickPronounFieldEditButton();

            dataFormFieldPage
                .WaitForDataFormFieldPageToLoad()
                .ValidateUsedInCodeNoOptionSelected(true)
                .ClickBackButton();

            #endregion

            #region Step 12

            var _personBOId = dbHelper.businessObject.GetBusinessObjectByName("Person")[0];
            var _personDataFormId = dbHelper.dataForm.GetByName("Person: Main")[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickBusinessObjectsButton();

            businessObjectsPage
                .WaitForBusinessObjectsPageToLoad()
                .InsertQuickSearchText("Person")
                .ClickQuickSearchButton()
                .WaitForBusinessObjectsPageToLoad()
                .OpenRecord(_personBOId.ToString());

            businessObjectRecordPage
                .WaitForBusinessObjectRecordPageToLoad()
                .NavigateToDataFormsSubArea();

            dataFormsPage
                .WaitForDataFormsPageToLoad()
                .ValidateRecordIsPresent(_personDataFormId.ToString())
                .OpenRecord(_personDataFormId.ToString());

            dataFormRecordPage
                .WaitForDataFormRecordPageToLoad()
                .ClickEditButton();

            dataFormEditPage
                .WaitForDataFormEditPageToLoad()
                .ClickPronounFieldEditButton();

            dataFormFieldPage
                .WaitForDataFormFieldPageToLoad()
                .ValidateUsedInCodeNoOptionSelected(true)
                .ClickBackButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3686

        [TestProperty("JiraIssueID", "ACC-3685")]
        [Description("Test Case for CDV6-17491 - Transfer pronouns from Applicant to System User (when is promoted)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Role Applications")]
        [TestProperty("Screen3", "System Users")]
        public void Pronouns_UITestMethod006()
        {
            #region Step 1           

            #region Care provider staff role type

            string _careProviderStaffRoleName = "Associate";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Associate ...");

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Staff Recruitment Item
            var _staffRecruitmentItemName = "SRI3685";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            var _recruitmentRequirementName = "RecReq_3685";
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant
            var applicantFirstName = "Test3685";
            var applicantLastName = "0User_" + currentDateTime;
            var _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _teamId);
            dbHelper.applicant.UpdateAvailableFrom(_applicantId, new DateTime(2023, 7, 1));
            dbHelper.applicant.UpdatePronoun(_applicantId, _pronoun_TheyThem);

            #endregion

            #region Role Application
            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _teamId, DateTime.Now, _teamId, 1, "applicant", applicantFirstName + " " + applicantLastName, _employmentContractTypeid);

            #endregion

            loginPage
              .GoToLoginPage()
              .Login(_username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2, 3, 4

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            #endregion

            #region Step 5

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidatePronounFieldLinkText("They/Them")
                .NavigateToRoleApplicationsPage();

            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            var roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.AreEqual(1, roleApplications.Count);

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateRoleApplicationRecordIsPresent(_roleApplicationID.ToString())
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            #region Recruitment Documents

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var recruitmentDocuments = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);

            foreach (Guid recruitmentDocumentId in recruitmentDocuments)
            {
                dbHelper.compliance.UpdateCompliance(recruitmentDocumentId, 3694, "RefNameTest", currentDate, currentDate);
            }

            #endregion

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicantName(applicantFirstName + " " + applicantLastName)
                .SelectRecruitmentStatus("Induction")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            #endregion

            #region Step 6

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            var _promotedSystemUserId = dbHelper.systemUser.GetSystemUserByUserNameContains(applicantLastName).First();
            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("*" + applicantLastName + "*")
                .ClickSearchButton()
                .OpenRecord(_promotedSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidatePronounsLinkFieldText("They/Them");

            #endregion
        }

        #endregion
    }
}
