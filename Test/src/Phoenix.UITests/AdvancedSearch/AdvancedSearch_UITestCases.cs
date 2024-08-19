using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
//[assembly: Parallelize(Workers = 100, Scope = ExecutionScope.MethodLevel)]

namespace Phoenix.UITests.AdvancedSearch.AdvancedSearch
{
    /// <summary>
    ///  
    /// </summary>
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Automation - Person Form 1.Zip")]
    [DeploymentItem("Files\\Mobile - Person Form.Zip")]
    [DeploymentItem("Files\\Gender Change.Zip")]
    [DeploymentItem("Files\\CP Review.Zip")]
    [DeploymentItem("Files\\Automated UI Test Document - CMS Questions.Zip")]

    public class AdvancedSearch_UITestCases : FunctionalTest
    {

        #region Properties

        private string _tenantName;
        private Guid _languageId;
        private Guid _personLanguageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUserName;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid _maritalStatusId;
        private Guid _systemUserId;
        private Guid _personID;
        private Guid _personID2;
        private Guid _caseId;
        private string _caseNumber;
        private string _caseNumberTitle;
        private Guid _caseStatusId;
        private Guid _closedCaseStatusId;
        private Guid _caseFormId;
        private Guid _dataFormId;
        private Guid _cloneCaseId;
        private Guid _personFormDocumentId1;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _documentId1, _documentId2, _documentId3;
        private Guid _serviceElement1Id, _serviceElement2Id;
        private Guid _providerId;
        private Guid _serviceProvidedId;

        #endregion

        [TestInitialize()]
        public void AdvancedSearch_SetupTest()
        {

            try
            {
                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

                #endregion

                #region Marital Status

                var maritalStatusExist = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married").Any();
                if (!maritalStatusExist)
                {
                    _maritalStatusId = dbHelper.maritalStatus.CreateMaritalStatus("Married", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);
                }
                if (_maritalStatusId == Guid.Empty)
                {
                    _maritalStatusId = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married").FirstOrDefault();
                }
                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                {
                    _languageId = dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                }
                if (_languageId == Guid.Empty)
                {
                    _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").FirstOrDefault();
                }
                #endregion

                #region Person Language

                _personLanguageId = dbHelper.language.CreateLanguage("English (UK)" + _currentDateSuffix, _careDirectorQA_TeamId, "2345", "001", new DateTime(2015, 1, 1), null);

                #endregion

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Ethnicity" + _currentDateSuffix).Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Ethnicity" + _currentDateSuffix, new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Ethnicity" + _currentDateSuffix)[0];

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Contact Reason

                var contactReasonExists = dbHelper.contactReason.GetByName("ContactReason" + _currentDateSuffix).Any();
                if (!contactReasonExists)
                    dbHelper.contactReason.CreateContactReason(_careDirectorQA_TeamId, "ContactReason" + _currentDateSuffix, new DateTime(2020, 1, 1), 110000000, false);
                _contactReasonId = dbHelper.contactReason.GetByName("ContactReason" + _currentDateSuffix)[0];

                #endregion

                #region Contact Source

                var contactSourceExists = dbHelper.contactSource.GetByName("ContactSource" + _currentDateSuffix).Any();
                if (!contactSourceExists)
                    dbHelper.contactSource.CreateContactSource(_careDirectorQA_TeamId, "ContactSource" + _currentDateSuffix, new DateTime(2020, 1, 1));
                _contactSourceId = dbHelper.contactSource.GetByName("ContactSource" + _currentDateSuffix)[0];

                #endregion

                #region Case Status

                _closedCaseStatusId = dbHelper.caseStatus.GetByName("Closed")[0];

                #endregion

                #region Create SystemUser Record

                _systemUserName = "AdvancedSearchUser1" + _currentDateSuffix;
                commonMethodsDB.CreateSystemUserRecord(_systemUserName, "AdvancedSearch", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-8420

        [TestProperty("JiraIssueID", "CDV6-9909")]
        [Description("Open the advanced search window - Set Record Type to 'Form (Person)' - Set filter to 'Cancelled Reason' - " +
            "Set Operator to 'Equals' - Select inactive Form cancellation Reason reference data (e.g Duplicated) - Tap on the Search button - " +
            "Validate that only matching person records with the selected Cancelled Reason are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void InactiveReferenceDataAvailableToSelectInAdvancedSearch_UITestMethod001()
        {
            #region Form Cancellation Reason

            var FormCancellationReason = commonMethodsDB.CreateFormCancellationReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Duplicated", new DateTime(2022, 1, 1), null, true);
            var FormCancellationReason2 = commonMethodsDB.CreateFormCancellationReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Created in error", new DateTime(2022, 1, 1), null, false);

            #endregion

            #region Person

            if (!dbHelper.person.GetByFirstName("CDV6_9909").Any())
                _personID = dbHelper.person.CreatePersonRecord("", "CDV6_9909", "", "Person", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            _personID = dbHelper.person.GetByFirstName("CDV6_9909").FirstOrDefault();

            #endregion

            #region Document

            string document1Name = "Automation - Person Form 1";
            _documentId1 = commonMethodsDB.CreateDocumentIfNeeded(document1Name, "Automation - Person Form 1.Zip");

            string document2Name = "Mobile - Person Form";
            _documentId2 = commonMethodsDB.CreateDocumentIfNeeded(document2Name, "Mobile - Person Form.Zip");

            string document3Name = "Gender Change";
            _documentId3 = commonMethodsDB.CreateDocumentIfNeeded(document3Name, "Gender Change.Zip");

            #endregion

            #region Delete Person Form

            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            #endregion

            #region Person Form

            Guid personForm1 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId1, DateTime.Now, true);
            Guid personForm2 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId2, DateTime.Now, true);
            Guid personForm3 = dbHelper.personForm.CreatePersonForm(_careDirectorQA_TeamId, _personID, _documentId3, DateTime.Now, true);

            dbHelper.personForm.UpdatePersonFormStatus(personForm1, 5, FormCancellationReason);
            dbHelper.personForm.UpdatePersonFormStatus(personForm2, 5, FormCancellationReason);
            dbHelper.personForm.UpdatePersonFormStatus(personForm3, 5, FormCancellationReason2);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Forms (Person)")
                .SelectFilter("1", "Cancelled Reason")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Inactive Records").SelectResultElement(FormCancellationReason.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(personForm1.ToString())
                .ValidateSearchResultRecordPresent(personForm2.ToString());

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8525

        [TestProperty("JiraIssueID", "CDV6-9910")]
        [Description("")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CustomisedColumnWidthSettingsBeingLost_UITestMethod001()
        {
            var BusinessObjectRelationshipId = new Guid("ad0fde87-f08c-e911-a2c5-0050569231cf"); //FK_Provider_AdoptionInfoAdopter_ProviderId
            var BusinessObjectFieldId = new Guid("eb300d32-6499-e811-9be8-989096c9be3d"); //accountnumber

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Adoption Info – Adopters")
                .ClickChooseColumnsButton()
                .WaitForChooseColumnsPopupToLoad()
                .ChooseColumnsPopup_SelectParentRecordType("Provider [Provider]")
                .ChooseColumnsPopup_SelectField("Account Number")
                .ChooseColumnsPopup_ClickAddButton()
                .ChooseColumnsPopup_InsertColumnWith(BusinessObjectRelationshipId.ToString(), BusinessObjectFieldId.ToString(), "321")
                .ChooseColumnsPopup_ClickOKButton()
                .WaitForAdvanceSearchPageToLoad()
                .ClickChooseColumnsButton()
                .WaitForChooseColumnsPopupToLoad()
                .ChooseColumnsPopup_ValidateColumnWith(BusinessObjectRelationshipId.ToString(), BusinessObjectFieldId.ToString(), "321");
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-8522

        [TestProperty("JiraIssueID", "CDV6-9911")]
        [Description("Open the advanced search window - Validate that it is possible to search Document records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExtraFieldsInAdvancedSearchFilters_UITestMethod001()
        {
            string document1Name = "Automation - Person Form 1";
            _personFormDocumentId1 = commonMethodsDB.CreateDocumentIfNeeded(document1Name, "Automation - Person Form 1.Zip");

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Documents")
                .SelectFilter("1", "Name")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", document1Name)
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_personFormDocumentId1.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-9912")]
        [Description("Open the advanced search window - Validate that it is possible to search Person Address records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExtraFieldsInAdvancedSearchFilters_UITestMethod002()
        {
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeByName("Other")[0];
            int AddressTypeId_Primary = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            #region Person

            _personID = dbHelper.person.CreatePersonRecord("", "CDV6_9912", "", _currentDateSuffix, "", new DateTime(2000, 1, 2), _ethnicityId, _maritalStatusId, _personLanguageId, AddressPropertyType, _careDirectorQA_TeamId, "PROPERTY NAME" + _currentDateSuffix, "PROPERTY NO" + _currentDateSuffix, "COUNTRY" + _currentDateSuffix, "STREET" + _currentDateSuffix, "VLG" + _currentDateSuffix, "UPRN" + _currentDateSuffix, "TOWN" + _currentDateSuffix, "COUNTY" + _currentDateSuffix, "PC" + _currentDateSuffix, AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);

            #endregion

            #region Person Address

            var PersonAddressId = dbHelper.personAddress.GetByPersonId(_personID)[0];

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Person Addresses")
                .SelectFilter("1", "Postcode")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", "PC" + _currentDateSuffix)
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(PersonAddressId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-9913")]
        [Description("Open the advanced search window - Validate that it is possible to search Team Restricted Data Accesses records by the Data Restriction ID")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExtraFieldsInAdvancedSearchFilters_UITestMethod003()
        {
            #region Data Restriction

            var dataRestrictionID = commonMethodsDB.CreateDataRestrictionRecord(new Guid("e2985963-19a7-e911-a2c6-005056926fe4"), "Security Data Restriction - Allow Team", 1, _careDirectorQA_TeamId);

            #endregion

            #region Team Restricted Data Access

            if (!dbHelper.teamRestrictedDataAccess.GetTeamRestrictedDataAccessByTeamID(_careDirectorQA_TeamId, dataRestrictionID).Any())
                dbHelper.teamRestrictedDataAccess.CreateTeamRestrictedDataAccess(dataRestrictionID, _careDirectorQA_TeamId, new DateTime(2019, 7, 19), null, _careDirectorQA_TeamId);

            Guid teamRestrictedDataAccessId = dbHelper.teamRestrictedDataAccess.GetTeamRestrictedDataAccessByTeamID(_careDirectorQA_TeamId, dataRestrictionID).FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Team Restricted Data Accesses")
                .SelectFilter("1", "Data Restriction")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Security Data Restriction - Allow Team").TapSearchButton().SelectResultElement(dataRestrictionID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(teamRestrictedDataAccessId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-9914")]
        [Description("Open the advanced search window - Validate that it is possible to search Team Restricted Data Accesses records by the End Date")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExtraFieldsInAdvancedSearchFilters_UITestMethod004()
        {

            #region Data Restriction

            var dataRestrictionID = commonMethodsDB.CreateDataRestrictionRecord(new Guid("e236c8f1-09a7-e911-a2c6-005056926fe4"), "Security Test - Control Data Restriction", 1, _careDirectorQA_TeamId);

            #endregion

            #region Team Restricted Data Access

            if (!dbHelper.teamRestrictedDataAccess.GetTeamRestrictedDataAccessByTeamID(_careDirectorQA_TeamId, dataRestrictionID).Any())
                dbHelper.teamRestrictedDataAccess.CreateTeamRestrictedDataAccess(dataRestrictionID, _careDirectorQA_TeamId, new DateTime(2019, 7, 19), new DateTime(2019, 7, 21), _careDirectorQA_TeamId);

            Guid teamRestrictedDataAccessId = dbHelper.teamRestrictedDataAccess.GetTeamRestrictedDataAccessByTeamID(_careDirectorQA_TeamId, dataRestrictionID).FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Team Restricted Data Accesses")
                .SelectFilter("1", "End Date")
                .SelectOperator("1", "Contains Data")
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(teamRestrictedDataAccessId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-9915")]
        [Description("Open the advanced search window - Validate that it is possible to search User Restricted Data Accesses records by the Data Restriction ID")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExtraFieldsInAdvancedSearchFilters_UITestMethod005()
        {

            #region System User

            Guid _systemUserId2 = commonMethodsDB.CreateSystemUserRecord("AdvancedSearch9915" + _currentDateSuffix, "AdvancedSearch", "9915" + _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            #region Data Restriction

            var mobileDataRestrictionID = commonMethodsDB.CreateDataRestrictionRecord(new Guid("85fa2adb-78d2-ea11-a2cd-005056926fe4"), "Mobile Data Restriction - Allow User (1)", 1, _careDirectorQA_TeamId);

            #endregion

            #region User Restricted Data Access

            if (!dbHelper.userRestrictedDataAccess.GetUserRestrictedDataAccessByUserID(_systemUserId2, mobileDataRestrictionID).Any())
                dbHelper.userRestrictedDataAccess.CreateUserRestrictedDataAccess(mobileDataRestrictionID, _systemUserId2, new DateTime(2019, 7, 19), new DateTime(2019, 7, 21), _careDirectorQA_TeamId);

            Guid userRestrictedDataAccessId = dbHelper.userRestrictedDataAccess.GetUserRestrictedDataAccessByUserID(_systemUserId2, mobileDataRestrictionID).FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("User Restricted Data Accesses")
                .SelectFilter("1", "Data Restriction")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Mobile Data Restriction - Allow User (1)").TapSearchButton().SelectResultElement(mobileDataRestrictionID.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(userRestrictedDataAccessId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-9916")]
        [Description("Open the advanced search window - Validate that it is possible to search User Restricted Data Accesses records by the End Date")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExtraFieldsInAdvancedSearchFilters_UITestMethod006()
        {

            #region System User

            Guid _systemUserId2 = commonMethodsDB.CreateSystemUserRecord("AdvancedSearch9916" + _currentDateSuffix, "AdvancedSearch", "9916" + _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            #region Data Restriction

            var dataRestrictionID = commonMethodsDB.CreateDataRestrictionRecord(new Guid("e236c8f1-09a7-e911-a2c6-005056926fe4"), "Security Test - Control Data Restriction", 1, _careDirectorQA_TeamId);

            #endregion

            #region User Restricted Data Access

            if (!dbHelper.userRestrictedDataAccess.GetUserRestrictedDataAccessByUserID(_systemUserId2, dataRestrictionID).Any())
                dbHelper.userRestrictedDataAccess.CreateUserRestrictedDataAccess(dataRestrictionID, _systemUserId2, new DateTime(2019, 7, 19), new DateTime(2019, 7, 21), _careDirectorQA_TeamId);

            Guid userRestrictedDataAccessId = dbHelper.userRestrictedDataAccess.GetUserRestrictedDataAccessByUserID(_systemUserId2, dataRestrictionID).FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("User Restricted Data Accesses")
                .SelectFilter("1", "End Date")
                .SelectOperator("1", "Contains Data")
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(userRestrictedDataAccessId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-9917")]
        [Description("Open the advanced search window - Validate that it is possible to search Case Form records by the Joint Carer field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExtraFieldsInAdvancedSearchFilters_UITestMethod007()
        {

            #region Document

            string documentName = "Automated UI Test Document - CMS Questions";
            _personFormDocumentId1 = commonMethodsDB.CreateDocumentIfNeeded(documentName, "Automated UI Test Document - CMS Questions.Zip");

            #endregion

            #region Person

            _personID = dbHelper.person.CreatePersonRecord("", "CDV6_9917", "", "Person", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion

            #region Case
            _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _closedCaseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);

            #endregion

            #region Forms Case

            _caseFormId = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _personFormDocumentId1, _personID, _caseId, new DateTime(2020, 02, 02), 1, _personID, false, true, true, false, false, false);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Forms (Case)")
                .SelectFilter("1", "Joint Carer")
                .SelectOperator("1", "Contains Data")
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_caseFormId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-9918")]
        [Description("Open the advanced search window - Validate that it is possible to search Case Form records by the Joint Carer Assessment field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExtraFieldsInAdvancedSearchFilters_UITestMethod008()
        {
            #region Document

            _documentId1 = commonMethodsDB.CreateDocumentIfNeeded("CP Review", "CP Review.Zip");

            #endregion

            #region Person

            _personID = commonMethodsDB.CreatePersonRecord("CDV6_9918", _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2001, 1, 2));

            #endregion

            #region Case / Case Form

            _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Worker").FirstOrDefault();
            _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Inpatient)", _careDirectorQA_TeamId);

            _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            _caseFormId = dbHelper.caseForm.CreateCaseFormRecord(_careDirectorQA_TeamId, _documentId1, _personID, _caseId, new DateTime(2017, 1, 1), true);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Forms (Case)")
                .SelectFilter("1", "Joint Carer Assessment")
                .SelectOperator("1", "Equals")
                .SelectPicklistRuleValue("1", "Yes")
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_caseFormId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-9919")]
        [Description("Open the advanced search window - Validate that it is possible to search Service Provision records by the Authorization Date field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExtraFieldsInAdvancedSearchFilters_UITestMethod009()
        {
            #region Person

            _personID = commonMethodsDB.CreatePersonRecord("CDV6_9919", _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId);

            #endregion

            #region Service Provision Status

            var serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var AuthoriseStatus = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

            #endregion

            #region Service Element 1

            if (!dbHelper.serviceElement1.GetByName("SE_1_CDV6_9919").Any())
                dbHelper.serviceElement1.CreateServiceElement1(_careDirectorQA_TeamId, "SE_1_CDV6_9919", DateTime.Now.Date, 9988, 1, 2);
            _serviceElement1Id = dbHelper.serviceElement1.GetByName("SE_1_CDV6_9919")[0];

            #endregion

            #region Service Element 2

            if (!dbHelper.serviceElement2.GetByName("SE_2_CDV6_9919").Any())
            {
                _serviceElement2Id = dbHelper.serviceElement2.CreateServiceElement2(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "SE_2_CDV6_9919", DateTime.Now.Date, 9988);
                dbHelper.serviceMapping.CreateServiceMapping(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _serviceElement1Id, _serviceElement2Id);
            }
            _serviceElement2Id = dbHelper.serviceElement2.GetByName("SE_2_CDV6_9919")[0];

            #endregion

            #region Rate Units

            var rateunitid = dbHelper.rateUnit.GetByName("Per Week Pro Rata \\ Weekly")[0];

            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_9919").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "StartReason_CDV6_9919", new DateTime(2022, 1, 1));
            var serviceprovisionstartreasonid = dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_9919")[0];

            #endregion

            #region Service Provision End Reason

            if (!dbHelper.serviceProvisionEndReason.GetByName("EndReason_CDV6_9919").Any())
                dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(_careDirectorQA_TeamId, "EndReason_CDV6_9919", new DateTime(2022, 1, 1));
            var serviceprovisionendreasonid = dbHelper.serviceProvisionEndReason.GetByName("EndReason_CDV6_9919")[0];

            #endregion

            #region Provider

            if (!dbHelper.provider.GetProviderByName("Provider_Testing_CDV6_9919").Any())
            {
                _providerId = dbHelper.provider.CreateProvider("Provider_Testing_CDV6_9919", _careDirectorQA_TeamId, 2); //creating a "Supplier" provider
                dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);
            }
            _providerId = dbHelper.provider.GetProviderByName("Provider_Testing_CDV6_9919")[0];
            _serviceProvidedId = dbHelper.serviceProvided.GetByProviderId(_providerId)[0];

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID,
               serviceprovisionstatusid, _serviceElement1Id, _serviceElement2Id,
               rateunitid,
               serviceprovisionstartreasonid, serviceprovisionendreasonid,
               _careDirectorQA_TeamId, _serviceProvidedId,
               _providerId, _systemUserId, placementRoomTypeId, new DateTime(2019, 7, 1), new DateTime(2019, 7, 28), null);

            #endregion

            #region Update Service Provision Status / Authorisation Date

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, AuthoriseStatus);
            dbHelper.serviceProvision.UpdateServiceProvisionAuthorisationdateDate(serviceProvisionID, new DateTime(2019, 10, 22));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service Provisions")
                .SelectFilter("1", "Authorisation Date")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", "22/10/2019")
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(serviceProvisionID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-9920")]
        [Description("Open the advanced search window - Validate that it is possible to search Person records by the Target Group")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExtraFieldsInAdvancedSearchFilters_UITestMethod010()
        {
            #region Target Group

            var targetGroupName = "Target_Group_CDV6_9920";
            if (!dbHelper.personTargetGroup.GetByName(targetGroupName).Any())
                dbHelper.personTargetGroup.CreatePersonTargetGroup(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, targetGroupName, 9920, DateTime.Now.Date, null);
            var _targetGroupId = dbHelper.personTargetGroup.GetByName(targetGroupName)[0];

            #endregion

            #region Person

            _personID = dbHelper.person.CreatePersonRecord("", "CDV6_9920", "", _currentDateSuffix, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2, _targetGroupId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("People")
                .SelectFilter("1", "Target Group")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(targetGroupName).TapSearchButton().SelectResultElement(_targetGroupId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_personID.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-9921")]
        [Description("Open the advanced search window - Validate that it is possible to search Case records by the Cloned From field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ExtraFieldsInAdvancedSearchFilters_UITestMethod011()
        {
            #region Create Person

            _personID = commonMethodsDB.CreatePersonRecord("CDV6_9921_Person_1", _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId);
            _personID2 = commonMethodsDB.CreatePersonRecord("CDV6_9921_Person_2", _currentDateSuffix, _ethnicityId, _careDirectorQA_TeamId);

            #endregion

            #region Case 1

            _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
            _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Advice/Consultation", _careDirectorQA_TeamId);

            _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18);
            _caseNumber = (string)dbHelper.Case.GetCaseById(_caseId, "casenumber")["casenumber"];
            _caseNumberTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Case 2 (Clone Case)

            _cloneCaseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID2, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2021, 11, 10), new DateTime(2021, 11, 11), 18, _caseId, _caseId, _caseNumberTitle);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Cases")
                .SelectFilter("1", "Cloned From")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_caseNumber).TapSearchButton().SelectResultElement(_caseId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_cloneCaseId.ToString());
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10876

        [TestProperty("JiraIssueID", "CDV6-11402")]
        [Description("Validate the fix for https://advancedcsg.atlassian.net/browse/CDV6-10876 - " +
            "Open the advance search window - Set the record type to 'Finance Extracts' - " +
            "Set filter to 'Finance invoices | Extract' - Set Operator to 'Contains Linked Records' - Click on the add new Rule button - " +
            "Set filter to 'Finance Transactions | Extract' - Set Operator to 'Contains Linked Records' - " +
            "Click on the search button - Validate that the results page is displayed - Validate that Finance Extracts results are displayed")]
        [TestMethod]
        [TestCategory("UITest")]
        public void AdvancedSearchFinanceExtracts_UITestMethod001()
        {
            var financeExtractRecordId = new Guid("7c50dc80-0a8a-ea11-a2cd-005056926fe4"); //Batchid 19

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Extracts")

                .SelectFilter("1", "Finance Invoices | Extract")
                .SelectOperator("1", "Contains Linked Records")

                .ClickAddRuleButton(1)

                .SelectFilter("2", "Finance Transactions | Extract")
                .SelectOperator("2", "Contains Linked Records")

                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(financeExtractRecordId.ToString());
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
