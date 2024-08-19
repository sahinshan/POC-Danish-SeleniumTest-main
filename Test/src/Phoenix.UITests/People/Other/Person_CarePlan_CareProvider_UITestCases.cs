//using System;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Phoenix.UITests.Framework.PageObjects;

//namespace Phoenix.UITests.People
//{
//    /// <summary>
//    /// This class contains Automated UI test scripts for 
//    /// </summary>
//    [DeploymentItem("Files\\InitialAssessmentCommunication.zip"), DeploymentItem("Files\\InitialAssessmentCommunicationRules.zip"), DeploymentItem("chromedriver.exe")]
//    [DeploymentItem("Files\\Abbey Pain Scale.zip"), DeploymentItem("Files\\Abbey Pain Scale - Doc Rules.zip")]
//    [TestClass]
//    public class Person_CarePlan_CareProvider_UITestCases : FunctionalTest
//    {

//        private Guid _languageId;
//        private Guid _careDirectorQA_BusinessUnitId;
//        private Guid _careDirectorQA_TeamId;
//        private Guid _authenticationproviderid;
//        private Guid _ethnicityId;
//        private Guid _maritalStatusId;
//        private Guid _AutomationUserId;
//        private string _testUser_PartialName = DateTime.Now.ToString("yyyyMMddHHmmss_FFFFF");
//        private string _loginUsername = "CW_Admin_Test_User_1";
//        private Guid _defaultUserId;
//        private Guid _systemUserId;
//        private Guid _personID;
//        private int _personNumber;
//        private string _personFullName;
//        private Guid _carePlanType;
//        private Guid _personCarePlanID;
//        private Guid _caseStatusId;
//        private Guid _dataFormId;
//        private Guid _caseId;
//        private Guid _carePlanType01;
//        private string _caseNumber;
//        private Guid _contactReasonId;
//        private Guid _contactSourceId;
//        private Guid _personCareNeedDomainID;
//        private Guid _personCarePlanNeedID;
//        private Guid _personCarePlanGoalId;
//        private Guid _carePlanGoalTypeId;
//        private Guid _carePlanInterventionTypeId;
//        private Guid _personCarePlanInterventionId;
//        private Guid _documentCategoryId;
//        private Guid _documentTypeId;
//        private Guid _documentId;
//        private Guid _newdocumentId;
//        private Guid _personCarePlanFormID;
//        private string _documentName = "Initial Assessment - Communication";
//        private string _documentName1 = "Abbey Pain Scale";

//        [TestInitialize()]


//        public void Person_CarePlan_SetupTest()
//        {
//            try
//            {


//                #region Business Unit

//                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
//                if (!businessUnitExists)
//                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
//                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

//                #endregion

//                #region Providers

//                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

//                #endregion

//                #region Team

//                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
//                if (!teamsExist)
//                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
//                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

//                #endregion

//                #region Marital Status

//                var maritalStatusExist = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").Any();
//                if (!maritalStatusExist)
//                {
//                    _maritalStatusId = dbHelper.maritalStatus.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);
//                }
//                if (_maritalStatusId == Guid.Empty)
//                {
//                    _maritalStatusId = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").FirstOrDefault();
//                }
//                #endregion

//                #region Language

//                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
//                if (!language)
//                {
//                    _languageId = dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
//                }
//                if (_languageId == Guid.Empty)
//                {
//                    _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").FirstOrDefault();
//                }
//                #endregion Lanuage

//                #region Ethnicity

//                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("PersonCarePlan_Ethnicity").Any();
//                if (!ethnicitiesExist)
//                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "PersonCarePlan_Ethnicity", new DateTime(2020, 1, 1));
//                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("PersonCarePlan_Ethnicity")[0];

//                #endregion

//                #region Default login user
//                _AutomationUserId = dbHelper.systemUser.CreateSystemUser("AutoUser_" + _testUser_PartialName, "Auto", "User " + _testUser_PartialName, "Auto User " + _testUser_PartialName, "Passw0rd_!", "Auto_User_" + _testUser_PartialName + "@somemail.com", "Auto_User_" + _testUser_PartialName + "@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

//                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_AutomationUserId, "username")["username"];
//                dbHelper.systemUser.UpdateLastPasswordChangedDate(_AutomationUserId, DateTime.Now.Date);
//                #endregion

//                #region Create Login User

//                _loginUsername = "CW_Admin_Test_User_1";

//                if (!dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_1").Any())
//                {
//                    _defaultUserId = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_1", "CW", "Admin_Test_User_1", "CW Admin_Test_User_1", "Passw0rd_!", "CW_Admin_Test_User_1@somemail.com", "CW_Admin_Test_User_1@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

//                    //remove any existing profile from the user
//                    //foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_defaultUserId))
//                    //    dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

//                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
//                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

//                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultUserId, systemAdministratorSecurityProfileId);
//                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultUserId, systemUserSecureFieldsSecurityProfileId);
//                }

//                if (_defaultUserId == Guid.Empty)
//                    _defaultUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_1").FirstOrDefault();

//                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultUserId, DateTime.Now.Date);

//                #endregion

//                #region Create SystemUser Record

//                if (!dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_CDV6_13981").Any())
//                    _systemUserId = dbHelper.systemUser.CreateSystemUser("CW_Forms_Test_User_CDV6_13981", "CW", "Forms_Test_User_CDV6_13981", "CW" + "Kumar", "Passw0rd_!", "CW_Forms_Test_User_CDV6_13981@somemail.com", "CW_Forms_Test_User_CDV6_13981@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

//                if (_systemUserId == Guid.Empty)
//                    _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_CDV6_13981").FirstOrDefault();

//                #endregion

//                #region Person
//                var firstName = "Person_CarePlan1" + DateTime.Now.ToString("yyyyMMddHHmmss");
//                var lastName = "LN_CDV6_17302";
//                var personRecordExists = dbHelper.person.GetByFirstName(firstName).Any();
//                if (!personRecordExists)
//                {
//                    _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
//                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
//                }
//                if (_personID == Guid.Empty)
//                {
//                    _personID = dbHelper.person.GetByFirstName("Person_CarePlan1").FirstOrDefault();
//                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
//                }
//                _personFullName = "Person_CarePlan1 LN_CDV6_17302" + DateTime.Now.ToString("yyyyMMddHHmmss");

//                #endregion

//                #region Question Catalogue - Multi Response Question

//                Guid multiResponseQuestionId;
//                Guid multiOptionAnswer1;
//                Guid multiOptionAnswer2;
//                Guid multiOptionAnswer3;
//                if (!dbHelper.questionCatalogue.GetByQuestionName("CDV6_17721_MR").Any())
//                {
//                    multiResponseQuestionId = dbHelper.questionCatalogue.CreateQuestion("CDV6_17721_MR", "", 4);
//                    dbHelper.multiOptionAnswer.CreateMultiOptionAnswer("Option 1", multiResponseQuestionId, 1, 1, 1);
//                    dbHelper.multiOptionAnswer.CreateMultiOptionAnswer("Option 2", multiResponseQuestionId, 1, 1, 1);
//                    dbHelper.multiOptionAnswer.CreateMultiOptionAnswer("Option 3", multiResponseQuestionId, 1, 1, 1);
//                }

//                multiResponseQuestionId = dbHelper.questionCatalogue.GetByQuestionName("CDV6_17721_MR")[0];
//                multiOptionAnswer1 = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Option 1", multiResponseQuestionId)[0];
//                multiOptionAnswer2 = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Option 2", multiResponseQuestionId)[0];
//                multiOptionAnswer3 = dbHelper.multiOptionAnswer.GetByNameAndQuestionCatalogueID("Option 3", multiResponseQuestionId)[0];

//                #endregion

//                #region Question Catalogue - Table (With Question Per Cell)

//                Guid tableQuestionId;
//                if (!dbHelper.questionCatalogue.GetByQuestionName("CDV6_17721_QPC").Any())
//                {
//                    tableQuestionId = dbHelper.questionCatalogue.CreateQuestion("CDV6_17721_QPC", "", 17);
//                    dbHelper.tableQuestionCell.CreateTableQuestionCell(tableQuestionId, multiResponseQuestionId, 1, 1, 1, 1);
//                }

//                tableQuestionId = dbHelper.questionCatalogue.GetByQuestionName("CDV6_17721_QPC")[0];

//                #endregion

//                #region Care Plan Type

//                var carePlanExist = dbHelper.carePlanType.GetByName("Activities of Daily Living").Any();
//                if (!carePlanExist)
//                {
//                    _carePlanType = dbHelper.carePlanType.CreateCarePlanTypeId("Activities of Daily Living", DateTime.Now, _careDirectorQA_TeamId);
//                }
//                if (_carePlanType == Guid.Empty)
//                {
//                    _carePlanType = dbHelper.carePlanType.GetByName("Activities of Daily Living").FirstOrDefault();
//                }

//                carePlanExist = dbHelper.carePlanType.GetByName("Mental Health Crisis Plan").Any();
//                if (!carePlanExist)
//                {
//                    _carePlanType01 = dbHelper.carePlanType.CreateCarePlanTypeId("Mental Health Crisis Plan", DateTime.Now, _careDirectorQA_TeamId);
//                }
//                if (_carePlanType01 == Guid.Empty)
//                {
//                    _carePlanType01 = dbHelper.carePlanType.GetByName("Mental Health Crisis Plan").FirstOrDefault();
//                }

//                #endregion

//                #region Document Category
//                _documentCategoryId = dbHelper.documentCategory.GetByName("Case Form").FirstOrDefault();

//                #endregion

//                #region Document Type
//                _documentTypeId = dbHelper.documentType.GetByName("initial assessment").FirstOrDefault();

//                #endregion

//                #region Domain of need

//                var careplanneeddomainexists = dbHelper.personCarePlanNeedDomain.GetByName("Communication").Any();
//                if (!careplanneeddomainexists)
//                    dbHelper.personCarePlanNeedDomain.CreateCarePlanNeedDomain(new Guid("d7d27efb-159e-ec11-a334-005056926fe4"), _careDirectorQA_TeamId, "Communication", new DateTime(2020, 1, 1));
//                _personCareNeedDomainID = dbHelper.personCarePlanNeedDomain.GetByName("Communication")[0];

//                #endregion

//                #region Document

//                _documentId = commonMethodsDB.CreateDocumentIfNeeded(_documentName, "InitialAssessmentCommunication.Zip");//Import Document
//                commonMethodsDB.ImportDocumentRules("InitialAssessmentCommunicationRules.Zip");//Import Document Rules

//                #endregion

//                #region Document of the type abbey pain scale

//                _newdocumentId = commonMethodsDB.CreateDocumentIfNeeded(_documentName1, "Abbey Pain Scale.Zip");//Import Document
//                commonMethodsDB.ImportDocumentRules("Abbey Pain Scale - Doc Rules.zip");//Import Document Rules

//                #endregion

//            }


//            catch
//            {
//                if (driver != null)
//                    driver.Quit();

//                throw;
//            }

//        }

//        #region https://advancedcsg.atlassian.net/browse/CDV6-15060

//        [TestProperty("JiraIssueID", "ACC-3081")]
//        [Description("Login CD -> Workplace -> People -> Select any existing person -> Go Care Plan tab -> Go Care Plan sub tab(Care Provider Care Plan should be active ) -> Click + icon to create a new record ->Should not display" +
//         " any field called Care Plan Type -> Fill all mandatory details and try to save the record -> Record should get saved successfully without any warning  fill Care Plan Type" +
//          "Go back and check the summary page of the Care Plan -> Newly created record should be displayed with a care plan type as Care Plan -> Click Settings -> Configurations ->" +
//          " Reference Data -> Care Planning -> Open the Care Plan Types -> New care plan type Care Plan should be created automatically through the steps 2.For that  Valid to export  should be false")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_CareProvider_UITestCases01()
//        {
//            loginPage
//               .GoToLoginPage()
//               .Login(_loginUsername, "Passw0rd_!", "Care Providers");

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickCreateNewRecord();

//            personCarePlanRecordPage
//                .WaitForPersonCarePlanRecordPageToLoad()
//                .ValidateCarePlanTypeVisible(false)
//                .InsertStartDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
//                .InsertReviewDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
//                .ClickSaveAndCloseButton();

//            personCarePlansSubPage_CarePlansTab
//                 .WaitForPersonCarePlansSubPage_CarePlansTabToLoad();

//            System.Threading.Thread.Sleep(1200);

//            var carePlanRecords = dbHelper.personCarePlan.GetByPersonID(_personID);
//            Assert.AreEqual(1, carePlanRecords.Count);

//            mainMenu
//               .WaitForMainMenuToLoad()
//               .NavigateToReferenceDataSection();

//            referenceDataPage
//                .WaitForReferenceDataPageToLoad()
//                .InsertSearchQuery("Care Plan Types")
//                .TapSearchButton()
//                .ClickReferenceDataMainHeader("Care Planning")
//                .ClickReferenceDataElement("Care Plan Types");

//            carePlanTypesPage
//                .WaitForCarePlanTypesPageToLoad()
//                .SearchCarePlanTypesRecord("Care Plan");

//            carePlanTypesRecordPage
//                .WaitForCarePlanTypesRecordPageToLoad()
//                .ValidatevalidforexportShoulBeFalse(true);
//        }

//        //[TestProperty("JiraIssueID", "ACC-3082")]
//        //[Description("Login CD -> Workplace -> People -> Select any existing person -> Go Care Plan tab -> Go Care Plan sub tab( Care Provider Care Plan should be inactive ) -> Click + icon to create a new record ->Should not display" +
//        //  " any field called Care Plan Type -> Fill all mandatory details and try to save the record -> Record should get saved successfully without any warning  fill Care Plan Type" +
//        //   "Go back and check the summary page of the Care Plan -> Newly created record should be displayed with a care plan type as Care Plan -> Click Settings -> Configurations ->" +
//        //   " Reference Data -> Care Planning -> Open the Care Plan Types -> New care plan type Care Plan should be created automatically through the steps 2.For that  Valid to export  should be false")]
//        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), Ignore]
//        //public void Person_CarePlan_CareProvider_UITestCases02()
//        //{
//        //    Assert.Inconclusive("This test requires the activation / deactivation of business modules. This test is deprecated and should be removed");
//        //    //var carePlanType =new Guid("aeb855eb-6e8a-ec11-a350-0050569231cf");

//        //    //dbHelper.businessModule.ActivateModule(carePlanType);
//        //    //dbHelper.businessModule.DeactivateModule(carePlanType);

//        //    loginPage
//        //       .GoToLoginPage()
//        //       .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//        //       .WaitFormHomePageToLoad(true, false, false);

//        //    mainMenu
//        //        .WaitForMainMenuToLoad()
//        //        .NavigateToPeopleSection();

//        //    peoplePage
//        //        .WaitForPeoplePageToLoad()
//        //        .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//        //        .OpenPersonRecord(_personID.ToString());

//        //    personRecordPage
//        //        .WaitForPersonRecordPageToLoad()
//        //        .TapCarePlansTab();

//        //    personCarePlansSubPage
//        //        .WaitForPersonCarePlansSubPageToLoad()
//        //        .ClickCarePlansLink();

//        //    personCarePlansSubPage_CarePlansTab
//        //        .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//        //        .ClickCreateNewRecord();

//        //    personCarePlanRecordPage
//        //        .WaitForPersonCarePlanRecordPageToLoad()
//        //        .ValidateCarePlanTypeVisible(true)
//        //        .ClickCarePlanTypeIdLookupButton();

//        //    lookupPopup
//        //        .WaitForLookupPopupToLoad()
//        //        .TypeSearchQuery("Care Plan")
//        //        .TapSearchButton()
//        //        .SelectResultElement(_carePlanType.ToString());

//        //    personCarePlanRecordPage
//        //        .WaitForPersonCarePlanRecordPageToLoad()
//        //        .InsertStartDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
//        //        .ClickSaveAndCloseButton();

//        //    personCarePlansSubPage_CarePlansTab
//        //         .WaitForPersonCarePlansSubPage_CarePlansTabToLoad();

//        //    var carePlanRecords = dbHelper.personCarePlan.GetByPersonID(_personID);
//        //    Assert.AreEqual(1, carePlanRecords.Count);

//        //    mainMenu
//        //       .WaitForMainMenuToLoad()
//        //       .NavigateToReferenceDataSection();

//        //    referenceDataPage
//        //        .WaitForReferenceDataPageToLoad()
//        //        .InsertSearchQuery("Care Plan Types")
//        //        .TapSearchButton()
//        //        .ClickReferenceDataMainHeader("Care Planning")
//        //        .ClickReferenceDataElement("Care Plan Types");

//        //    carePlanTypesPage
//        //        .WaitForCarePlanTypesPageToLoad()
//        //        .SearchCarePlanTypesRecord("Care Plan");

//        //    carePlanTypesRecordPage
//        //        .WaitForCarePlanTypesRecordPageToLoad();

//        //    var carePlanId = dbHelper.businessModule.GetBusinessModuleByName("Care Provider Care Plan");
//        //    Assert.AreEqual(1, carePlanId.Count);

//        //    //dbHelper.businessModule.ActivateModule(carePlanType);
//        //}

//        [TestProperty("JiraIssueID", "ACC-3083")]
//        [Description("Login CD -> Workplace -> People -> Select any existing person -> Go Care Plan tab(Care Provider Care Plan should be active ) -> Go Care Plan sub tab -> Click + icon to create a new record ->Should not display" +
//        " any field called Care Plan Type -> Fill all mandatory details and try to save the record -> Record should get saved successfully without any warning  fill Care Plan Type" +
//         "Go back and check the summary page of the Care Plan -> Newly created record should be displayed with a care plan type as Care Plan -> Click Settings -> Configurations ->" +
//         " Reference Data -> Care Planning -> Open the Care Plan Types ->Should not display another New care plan type Care Plan as its exist already")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_CareProvider_UITestCases03()
//        {
//            loginPage
//               .GoToLoginPage()
//               .Login(_loginUsername, "Passw0rd_!", "Care Providers");

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickCreateNewRecord();

//            personCarePlanRecordPage
//                .WaitForPersonCarePlanRecordPageToLoad()
//                //.ValidateCarePlanTypeVisible(true)
//                .InsertStartDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
//                .InsertReviewDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
//                .ClickSaveAndCloseButton();

//            personCarePlansSubPage_CarePlansTab
//                 .WaitForPersonCarePlansSubPage_CarePlansTabToLoad();

//            var carePlanRecords = dbHelper.personCarePlan.GetByPersonID(_personID);
//            Assert.AreEqual(1, carePlanRecords.Count);

//            mainMenu
//               .WaitForMainMenuToLoad()
//               .NavigateToReferenceDataSection();

//            referenceDataPage
//                .WaitForReferenceDataPageToLoad()
//                .InsertSearchQuery("Care Plan Types")
//                .TapSearchButton()
//                .ClickReferenceDataMainHeader("Care Planning")
//                .ClickReferenceDataElement("Care Plan Types");

//            System.Threading.Thread.Sleep(3000);

//            carePlanTypesPage
//                .WaitForCarePlanTypesPageToLoad()
//                .SearchCarePlanTypesRecord("Care Plan");

//            carePlanTypesRecordPage
//               .WaitForCarePlanTypesRecordPageToLoad();

//            var carePlanId = dbHelper.businessModule.GetBusinessModuleByName("Care Provider Care Plan");
//            Assert.AreEqual(1, carePlanId.Count);

//        }

//        #endregion

//        #region https://advancedcsg.atlassian.net/browse/CDV6-14647

//        [TestProperty("JiraIssueID", "ACC-3084")]
//        [Description("Login CD -> Work Place -> People -> Select any existing person -> Care Plans -> Click + icon to create new care plan -> Should not show Case and Case Coordinator fields.-> Fill all mandatory fields and click save" +
//            "Record should get saved successfully without any error for Case and Case Coordinator.")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_UITestMethod001()
//        {
//            loginPage
//               .GoToLoginPage()
//               .Login(_loginUsername, "Passw0rd_!", "Care Providers");

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//              .WaitForPersonRecordPageToLoad()
//              .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickCreateNewRecord();

//            personCarePlanRecordPage
//                .WaitForPersonCarePlanRecordPageToLoad()
//                .ValidateCarePlanTypeVisible(false)
//                .InsertStartDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
//                .InsertReviewDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
//                .ClickSaveAndCloseButton();

//            personCarePlansSubPage_CarePlansTab
//                 .WaitForPersonCarePlansSubPage_CarePlansTabToLoad();

//            System.Threading.Thread.Sleep(1500);

//            var carePlanRecords = dbHelper.personCarePlan.GetByPersonID(_personID);
//            Assert.AreEqual(1, carePlanRecords.Count);

//        }

//        [TestProperty("JiraIssueID", "ACC-3085")]
//        [Description("Login CD -> Work Place -> People -> Select any existing person -> Care Plans -> Click Copy Care Plan -> Should not show Case and Case Coordinator fields.-> Fill all mandatory fields and click save" +
//         "Record should get saved successfully without any error for Case and Case Coordinator.")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_UITestMethod002()
//        {

//            var percareplanexists = dbHelper.personCarePlan.GetByPersonID(_personID).Any();
//            if (!percareplanexists)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType01, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            }

//            if (_personCarePlanID == Guid.Empty)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.GetByPersonID(_personID).FirstOrDefault();
//            }

//            loginPage
//               .GoToLoginPage()
//               .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//               .WaitFormHomePageToLoad(true, false, false);

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

//            personCarePlanRecordPage
//              .WaitForPersonCarePlanRecordPageToLoad()
//              .SelectAuthoriseButton();
//            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

//            personCarePlanRecordPage
//             .WaitForPersonCarePlanRecordPageToLoad()
//             .ClickAdditionalItemsMenuButton()
//              .ClickCopyCarePlan();

//            copyCarePlanPopupPage
//                .WaitForCopyCarePlanPageToLoadWhenNocaseNCareCoordinator()
//                .ClickResponsibleTeamLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareDirector QA").TapSearchButton().SelectResultElement(_careDirectorQA_TeamId.ToString());

//            copyCarePlanPopupPage
//                .WaitForCopyCarePlanPageToLoadWhenNocaseNCareCoordinator()
//                .InsertStartDate(DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
//                 .InsertReviewDate(DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));


//            copyCarePlanPopupPage
//                .WaitForCopyCarePlanPageToLoadWhenNocaseNCareCoordinator()
//                .SelectFamilyInvolvedInCarePlan("Yes")
//                .ClickCopyButton();
//            System.Threading.Thread.Sleep(3000);
//            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Person care plan created.").TapOKButton();

//            System.Threading.Thread.Sleep(3000);

//            var personCarePlanId = dbHelper.personCarePlan.GetByPersonID(_personID);
//            Assert.AreEqual(2, personCarePlanId.Count);

//        }

//        [TestProperty("JiraIssueID", "ACC-3086")]
//        [Description("Login CD -> Work Place -> People -> Select any existing person -> Care Plans -> Click + icon to create new care plan -> Along with existing fields ,should display new fields Review Date as mandatory  and Review Frequency as non mandatory dropdown with below options:Daily/Weekly/Fortnightly/Monthly/Quarterly/Bi-annually/Annually.Fill all fields and click save->Record should get saved successfully.")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_UITestMethod003()
//        {
//            loginPage
//               .GoToLoginPage()
//               .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//               .WaitFormHomePageToLoad(true, false, false);

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//              .WaitForPersonRecordPageToLoad()
//              .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickCreateNewRecord();

//            personCarePlanRecordPage
//                .WaitForPersonCarePlanRecordPageToLoad()
//               .ValidateReviewDateField()
//                .ValidateReviewFrequencyField("Daily")
//                .ValidateReviewFrequencyField("Weekly")
//                .ValidateReviewFrequencyField("Fortnightly")
//                 .ValidateReviewFrequencyField("Monthly")
//                  .ValidateReviewFrequencyField("Quarterly")
//                   .ValidateReviewFrequencyField("Bi-annually")
//                     .ValidateReviewFrequencyField("Annually")
//                .InsertStartDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
//                .InsertReviewDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
//                .ClickSaveAndCloseButton();

//            personCarePlansSubPage_CarePlansTab
//                 .WaitForPersonCarePlansSubPage_CarePlansTabToLoad();

//            System.Threading.Thread.Sleep(3000);

//            var carePlanRecords = dbHelper.personCarePlan.GetByPersonID(_personID);
//            Assert.AreEqual(1, carePlanRecords.Count);

//        }


//        [TestProperty("JiraIssueID", "ACC-3087")]
//        [Description("Login CD -> Work Place -> People -> Select any existing person -> Care Plan ->  Verify the column headers:Should not show Case and Case Coordinator fields.Should show newly added fields Review Date and Review Frequency along with existing columns")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_UITestMethod004()
//        {
//            loginPage
//               .GoToLoginPage()
//               .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//               .WaitFormHomePageToLoad(true, false, false);

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//              .WaitForPersonRecordPageToLoad()
//              .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ValidateCarePlanTypeHeader("Care Plan Type")
//                .ValidatePersonHeader("Person")
//                .ValidateResponsibleTeamHeader("Responsible Team")
//                .ValidateReviewDateHeader("Review Date")
//                .ValidateReviewFrequencyHeader("Review Frequency")
//                .ValidateStartDateHeader("Start Date")
//                .ValidateStatusHeader("Status");

//        }

//        [TestProperty("JiraIssueID", "ACC-3088")]
//        [Description("Login CD -> Work Place -> People -> Select any existing person -> Care Plans -> Click Copy Care Plan -> Should not show Case and Case Coordinator fields.-> Fill all mandatory fields and click save" +
//         "Record should get saved successfully without any error for Case and Case Coordinator.")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_UITestMethod005()
//        {

//            var percareplanexists = dbHelper.personCarePlan.GetByPersonID(_personID).Any();
//            if (!percareplanexists)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType01, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            }

//            if (_personCarePlanID == Guid.Empty)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.GetByPersonID(_personID).FirstOrDefault();
//            }

//            loginPage
//               .GoToLoginPage()
//               .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//               .WaitFormHomePageToLoad(true, false, false);

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

//            personCarePlanRecordPage
//              .WaitForPersonCarePlanRecordPageToLoad()
//              .SelectAuthoriseButton();
//            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

//            personCarePlanRecordPage
//             .WaitForPersonCarePlanRecordPageToLoad()
//             .ClickAdditionalItemsMenuButton()
//              .ClickCopyCarePlan();

//            copyCarePlanPopupPage
//                .WaitForCopyCarePlanPageToLoadWhenNocaseNCareCoordinator()
//                .ClickResponsibleTeamLookupButton();

//            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CareDirector QA").TapSearchButton().SelectResultElement(_careDirectorQA_TeamId.ToString());

//            copyCarePlanPopupPage
//                .WaitForCopyCarePlanPageToLoadWhenNocaseNCareCoordinator()
//                .ValidateReviewDateField_Visible()
//                .ValidateReviewFrequencyield_Visible()
//                .InsertStartDate(DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
//                 .InsertReviewDate(DateTime.Now.AddDays(-5).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
//                 .SelectReviewFrequencyield("Daily");


//            copyCarePlanPopupPage
//                .WaitForCopyCarePlanPageToLoadWhenNocaseNCareCoordinator()
//                .SelectFamilyInvolvedInCarePlan("Yes")
//                .ClickCopyButton();
//            System.Threading.Thread.Sleep(3000);
//            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Person care plan created.").TapOKButton();

//            System.Threading.Thread.Sleep(3000);

//            var personCarePlanId = dbHelper.personCarePlan.GetByPersonID(_personID);
//            Assert.AreEqual(2, personCarePlanId.Count);

//        }

//        #endregion

//        #region https://advancedcsg.atlassian.net/browse/CDV6-16570

//        [TestProperty("JiraIssueID", "ACC-3089")]
//        [Description("Login CP -> Work Place -> People -> Select existing person who has active care plan -> Move to Care Plan tab -> Open Active Care Plan -> Move to Needs tab -> Open existing needs record -> Move to Goals / Outcomes tab -> Try to create new record" +
//            "Should display fields in below way Goal/Outcome Type should be non mandatory Domain of Need should be mandatory.")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_GoalROutCome_UITestMethod001()
//        {

//            var percareplanexists = dbHelper.personCarePlan.GetByPersonID(_personID).Any();
//            if (!percareplanexists)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            }

//            if (_personCarePlanID == Guid.Empty)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.GetByPersonID(_personID).FirstOrDefault();
//            }


//            //var careplanneeddomainexists = dbHelper.personCarePlanNeedDomain.GetByName("Communication").Any();
//            //if (!careplanneeddomainexists)
//            //    dbHelper.personCarePlanNeedDomain.CreateCarePlanNeedDomain(_careDirectorQA_TeamId, "Communication", new DateTime(2020, 1, 1));
//            //_personCareNeedDomainID = dbHelper.personCarePlanNeedDomain.GetByName("Communication")[0];

//            var careplangoaltypeexists = dbHelper.personCarePlanGoalType.GetByName("Physical Wellness").Any();
//            if (!careplangoaltypeexists)
//                dbHelper.personCarePlanGoalType.CreateCarePlanGoalType(_careDirectorQA_TeamId, "Physical Wellness", new DateTime(2020, 1, 1));
//            _carePlanGoalTypeId = dbHelper.personCarePlanGoalType.GetByName("Physical Wellness")[0];

//            _personCarePlanNeedID = dbHelper.personCarePlanNeed.CreatePersonCarePlanNeed(_careDirectorQA_TeamId, "Test", _personID, _personCarePlanID, DateTime.Now, _personCareNeedDomainID, "Automation Test Description");

//            loginPage
//              .GoToLoginPage()
//              .Login(_loginUsername, "Passw0rd_!", "Care Providers");

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

//            personCarePlanRecordPage
//               .WaitForPersonCarePlanRecordPageToLoad()
//               .TapNeedsSubLink()
//               .WaitForPersonCarePlanNeedPageToLoad()
//              .ClickOnCarePlanNeedRecord(_personCarePlanNeedID.ToString());

//            personCarePlanNeedRecordPage
//                .WaitForPersonCarePlanNeedRecordPageToLoad()
//                .TapGoalROutcomeLink();

//            personCarePlanNeedGoalsROutcomePage
//                .WaitForPersonCarePlanNeedGoalROutcomePageToLoad()
//                .TapCreateRecordButton();

//            personCarePlanNeedGoalsROutcomeRecordPage
//                .WaitForPersonCarePlanNeedGoalROutcomeRecordPageToLoad()
//                .InsertGoalOutcomeDescription("Test Description")
//                .TapGoalOutcomeTypeLookUpButton();

//            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Physical Wellness").TapSearchButton().SelectResultElement(_carePlanGoalTypeId.ToString());

//            personCarePlanNeedGoalsROutcomeRecordPage
//               .WaitForPersonCarePlanNeedGoalROutcomeRecordPageToLoad()
//               .TapResonsibleUserLookUpButton();

//            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("CW_Forms_Test_User_CDV6_13981").TapSearchButton().SelectResultElement(_systemUserId.ToString());

//            personCarePlanNeedGoalsROutcomeRecordPage
//                .WaitForPersonCarePlanNeedGoalROutcomeRecordPageToLoad()
//                .TapDomainOfNeedLookUpButton();

//            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Communication").TapSearchButton().SelectResultElement(_personCareNeedDomainID.ToString());

//            personCarePlanNeedGoalsROutcomeRecordPage
//                          .WaitForPersonCarePlanNeedGoalROutcomeRecordPageToLoad()
//                         .TapSaveNCloseButton();


//        }

//        [TestProperty("JiraIssueID", "ACC-3090")]
//        [Description("Login CP -> Work Place -> People -> Select existing person who has active care plan -> Move to Care Plan tab -> Open Active Care Plan -> Move to Needs tab -> Open existing needs record -> Move to Goals / Outcomes tab -> Open existing record -> Move to Interventions -> Try to create new record" +
//          "Should display fields in below way Intervention Type should be non mandatory Domain of Need and What should be mandatory.")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_GoalROutCome_UITestMethod002()
//        {
//            var percareplanexists = dbHelper.personCarePlan.GetByPersonID(_personID).Any();
//            if (!percareplanexists)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            }

//            if (_personCarePlanID == Guid.Empty)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.GetByPersonID(_personID).FirstOrDefault();
//            }
//            //var careplanneeddomainexists = dbHelper.personCarePlanNeedDomain.GetByName("Communication").Any();
//            //if (!careplanneeddomainexists)
//            //    dbHelper.personCarePlanNeedDomain.CreateCarePlanNeedDomain(_careDirectorQA_TeamId, "Communication", new DateTime(2020, 1, 1));
//            //_personCareNeedDomainID = dbHelper.personCarePlanNeedDomain.GetByName("Communication")[0];

//            var careplangoaltypeexists = dbHelper.personCarePlanGoalType.GetByName("Physical Wellness").Any();
//            if (!careplangoaltypeexists)
//                dbHelper.personCarePlanGoalType.CreateCarePlanGoalType(_careDirectorQA_TeamId, "Physical Wellness", new DateTime(2020, 1, 1));
//            _carePlanGoalTypeId = dbHelper.personCarePlanGoalType.GetByName("Physical Wellness")[0];

//            var careplaninterventiontypeexists = dbHelper.personCarePlanInterventionType.GetByName("Intervention Type 1").Any();
//            if (!careplaninterventiontypeexists)
//                dbHelper.personCarePlanInterventionType.CreateCarePlanInterventionType(_careDirectorQA_TeamId, "Intervention Type 1", new DateTime(2020, 1, 1));
//            _carePlanInterventionTypeId = dbHelper.personCarePlanInterventionType.GetByName("Intervention Type 1")[0];

//            _personCarePlanNeedID = dbHelper.personCarePlanNeed.CreatePersonCarePlanNeed(_careDirectorQA_TeamId, "Test", _personID, _personCarePlanID, DateTime.Now, _personCareNeedDomainID, "Automation Test Description");

//            _personCarePlanGoalId = dbHelper.personCarePlanGoal.CreatePersonCarePlanGoal(_careDirectorQA_TeamId, "test", _personID, _personCarePlanNeedID, DateTime.Today, "test description", _systemUserId, _carePlanGoalTypeId, _personCareNeedDomainID);

//            loginPage
//              .GoToLoginPage()
//              .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//              .WaitFormHomePageToLoad(true, false, false);


//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

//            personCarePlanRecordPage
//               .WaitForPersonCarePlanRecordPageToLoad()
//               .TapNeedsSubLink()
//               .WaitForPersonCarePlanNeedPageToLoad()
//              .ClickOnCarePlanNeedRecord(_personCarePlanNeedID.ToString());

//            personCarePlanNeedRecordPage
//                .WaitForPersonCarePlanNeedRecordPageToLoad()
//                .TapGoalROutcomeLink();

//            personCarePlanNeedGoalsROutcomePage
//                .WaitForPersonCarePlanNeedGoalROutcomePageToLoad()
//                .ClickOnCarePlanGoalROutcomeRecord(_personCarePlanGoalId.ToString());

//            personCarePlanNeedGoalsROutcomeRecordPage
//                .WaitForPersonCarePlanNeedGoalROutcomeRecordPageToLoad()
//                .TapInterventionsLink();

//            personCarePlanInterventionsRecordPage
//                .WaitForPersonCarePlanInterventionRecordPageToLoad()
//                 .TapInsertRecord();

//            personCarePlanInterventionsRecordAddPage
//                .WaitForPersonCarePlanInterventionRecordAddPageToLoad()
//                .TapInterventionTypeLookUpButton();

//            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Intervention Type 1").TapSearchButton().SelectResultElement(_carePlanInterventionTypeId.ToString());

//            personCarePlanInterventionsRecordAddPage
//               .WaitForPersonCarePlanInterventionRecordAddPageToLoad()
//               .TapDomainOfNeedLookUpButton();

//            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Communication").TapSearchButton().SelectResultElement(_personCareNeedDomainID.ToString());

//            personCarePlanInterventionsRecordAddPage
//               .WaitForPersonCarePlanInterventionRecordAddPageToLoad()
//               .InsertWhatDescription("Test")
//               .TapSaveNCloseButton();
//        }

//        [TestProperty("JiraIssueID", "ACC-3091")]
//        [Description("Login CP -> Work Place -> People -> Select existing person who has active care plan -> Move to Care Plan tab -> Open Active Care Plan -> Move to Needs tab -> Open existing needs record -> Move to Goals / Outcomes tab " +
//         "should display new column Domain of Need  and its relevant values.Open existing Goals / Outcomes record -> Move to Interventions ->Should display new column Domain of Need  and its relevant values.")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_GoalROutCome_UITestMethod003()
//        {
//            var percareplanexists = dbHelper.personCarePlan.GetByPersonID(_personID).Any();
//            if (!percareplanexists)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            }

//            if (_personCarePlanID == Guid.Empty)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.GetByPersonID(_personID).FirstOrDefault();
//            }
//            //var careplanneeddomainexists = dbHelper.personCarePlanNeedDomain.GetByName("Communication").Any();
//            //if (!careplanneeddomainexists)
//            //    dbHelper.personCarePlanNeedDomain.CreateCarePlanNeedDomain(_careDirectorQA_TeamId, "Communication", new DateTime(2020, 1, 1));
//            //_personCareNeedDomainID = dbHelper.personCarePlanNeedDomain.GetByName("Communication")[0];

//            var careplangoaltypeexists = dbHelper.personCarePlanGoalType.GetByName("Physical Wellness").Any();
//            if (!careplangoaltypeexists)
//                dbHelper.personCarePlanGoalType.CreateCarePlanGoalType(_careDirectorQA_TeamId, "Physical Wellness", new DateTime(2020, 1, 1));
//            _carePlanGoalTypeId = dbHelper.personCarePlanGoalType.GetByName("Physical Wellness")[0];

//            var careplaninterventiontypeexists = dbHelper.personCarePlanInterventionType.GetByName("Intervention Type 1").Any();
//            if (!careplaninterventiontypeexists)
//                dbHelper.personCarePlanInterventionType.CreateCarePlanInterventionType(_careDirectorQA_TeamId, "Intervention Type 1", new DateTime(2020, 1, 1));
//            _carePlanInterventionTypeId = dbHelper.personCarePlanInterventionType.GetByName("Intervention Type 1")[0];

//            _personCarePlanNeedID = dbHelper.personCarePlanNeed.CreatePersonCarePlanNeed(_careDirectorQA_TeamId, "Test", _personID, _personCarePlanID, DateTime.Now, _personCareNeedDomainID, "Automation Test Description");

//            _personCarePlanGoalId = dbHelper.personCarePlanGoal.CreatePersonCarePlanGoal(_careDirectorQA_TeamId, "test", _personID, _personCarePlanNeedID, DateTime.Today, "test description", _systemUserId, _carePlanGoalTypeId, _personCareNeedDomainID);

//            _personCarePlanInterventionId = dbHelper.personCarePlanIntervention.CreatePersonCarePlaIntervention(_careDirectorQA_TeamId, "test", _personID, _personCarePlanGoalId, DateTime.Today, "test description", _systemUserId, _carePlanInterventionTypeId, _personCareNeedDomainID);

//            loginPage
//              .GoToLoginPage()
//              .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//              .WaitFormHomePageToLoad(true, false, false);


//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

//            personCarePlanRecordPage
//               .WaitForPersonCarePlanRecordPageToLoad()
//               .TapNeedsSubLink()
//               .WaitForPersonCarePlanNeedPageToLoad()
//                .ClickOnCarePlanNeedRecord(_personCarePlanNeedID.ToString());

//            personCarePlanNeedRecordPage
//                .WaitForPersonCarePlanNeedRecordPageToLoad()
//                .TapGoalROutcomeLink();

//            personCarePlanNeedGoalsROutcomePage
//                .WaitForPersonCarePlanNeedGoalROutcomePageToLoad()
//                .ValidateDomainOfNeedHeaderText("Domain of Need")
//                .ValidateDomainOfNeedCellText(_personCarePlanGoalId.ToString(), "Communication")
//                .ClickOnCarePlanGoalROutcomeRecord(_personCarePlanGoalId.ToString());

//            personCarePlanNeedGoalsROutcomeRecordPage
//                .WaitForPersonCarePlanNeedGoalROutcomeRecordPageToLoad()
//                .TapInterventionsLink();

//            personCarePlanInterventionsRecordPage
//                .WaitForPersonCarePlanInterventionRecordPageToLoad()
//                .ValidateDomainOfNeedHeaderText("Domain of Need")
//                .ValidateDomainOfNeedCellText(_personCarePlanInterventionId.ToString(), "Communication");



//        }
//        #endregion

//        #region https://advancedcsg.atlassian.net/browse/CDV6-16449

//        [TestProperty("JiraIssueID", "ACC-3092")]
//        [Description("Verify the creation of Need from the Form Pre Requisites:Active Care Plan should be present for the personActive Care Plan Record should has Care Plan Form with Initial Assessment document")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_NeedsCreation_UITestMethod001()
//        {

//            var percareplanexists = dbHelper.personCarePlan.GetByPersonID(_personID).Any();
//            if (!percareplanexists)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            }

//            if (_personCarePlanID == Guid.Empty)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.GetByPersonID(_personID).FirstOrDefault();
//            }


//            var personcareplanneedformexists = dbHelper.personCarePlanForm.GetByPersonId(_personID).Any();
//            if (!personcareplanneedformexists)
//                dbHelper.personCarePlanForm.CreatePersonCarePlanForm(_careDirectorQA_TeamId, _documentId, new DateTime(2020, 1, 1), _personID, _personCarePlanID);
//            _personCarePlanFormID = dbHelper.personCarePlanForm.GetByPersonId(_personID)[0];




//            loginPage
//              .GoToLoginPage()
//              .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//              .WaitFormHomePageToLoad(true, false, false);

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

//            personCarePlanRecordPage
//               .WaitForPersonCarePlanRecordPageToLoad()
//               .NavigateToPersonCarePlanFormsCarePlanArea()
//               .WaitForPersonCarePlanFormPageToLoad()
//               .ClickOnCarePlanFormRecord(_personCarePlanFormID.ToString());



//            personCarePlanFormRecordPage
//                 .WaitForPersonCarePlanFormRecordPageToLoad()
//                 .TapEditAssessmentBtn();

//            personCarePlanFormInitialAssessmentRecordPage
//                .WaitForPersonCarePlanInitialAssessmentRecordPageToLoad()
//                .TapYes_EasilyMakeMyselfUnderstoodRadioBtn()
//                .TapNewCareNeedBtn();

//            newCareNeedPopupPage
//                .WaitForNewCareNeedPopupPageToLoad()
//                .TapAddNeed()
//                .ValidateNeedield_Visible()
//                .ValidateDeleteBtn_Visible()
//                .ValidateAddOutcomeBtn_Visible()
//                .InsertNeedield_Txt("Test")
//                .TapSave()
//                .TapDismiss()
//                .TapCancel();

//            personCarePlanFormInitialAssessmentRecordPage
//               .WaitForPersonCarePlanInitialAssessmentRecordPageToLoad();

//            var carePlanNeedRecords = dbHelper.personCarePlanNeed.GetByPersonId(_personID);
//            Assert.AreEqual(1, carePlanNeedRecords.Count);




//        }


//        [TestProperty("JiraIssueID", "ACC-3093")]
//        [Description("Verify the creation of Outcome from the Form Pre Requisites:Active Care Plan should be present for the personActive Care Plan Record should has Care Plan Form with Initial Assessment document")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_OutcomeCreation_UITestMethod002()
//        {

//            var percareplanexists = dbHelper.personCarePlan.GetByPersonID(_personID).Any();
//            if (!percareplanexists)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            }

//            if (_personCarePlanID == Guid.Empty)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.GetByPersonID(_personID).FirstOrDefault();
//            }


//            var personcareplanneedformexists = dbHelper.personCarePlanForm.GetByPersonId(_personID).Any();
//            if (!personcareplanneedformexists)
//                dbHelper.personCarePlanForm.CreatePersonCarePlanForm(_careDirectorQA_TeamId, _documentId, new DateTime(2020, 1, 1), _personID, _personCarePlanID);
//            _personCarePlanFormID = dbHelper.personCarePlanForm.GetByPersonId(_personID)[0];

//            _personCarePlanNeedID = dbHelper.personCarePlanNeed.CreatePersonCarePlanNeed(_careDirectorQA_TeamId, "Test", _personID, _personCarePlanID, DateTime.Now, _personCareNeedDomainID, "Automation Test Description");

//            _personCarePlanGoalId = dbHelper.personCarePlanGoal.CreatePersonCarePlanGoal(_careDirectorQA_TeamId, "test", _personID, _personCarePlanNeedID, DateTime.Today, "test description", _systemUserId, _carePlanGoalTypeId, _personCareNeedDomainID);

//            _personCarePlanInterventionId = dbHelper.personCarePlanIntervention.CreatePersonCarePlaIntervention(_careDirectorQA_TeamId, "test", _personID, _personCarePlanGoalId, DateTime.Today, "test description", _systemUserId, _carePlanInterventionTypeId, _personCareNeedDomainID);

//            loginPage
//              .GoToLoginPage()
//              .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//              .WaitFormHomePageToLoad(true, false, false);

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

//            personCarePlanRecordPage
//               .WaitForPersonCarePlanRecordPageToLoad()
//               .NavigateToPersonCarePlanFormsCarePlanArea()
//               .WaitForPersonCarePlanFormPageToLoad()
//               .ClickOnCarePlanFormRecord(_personCarePlanFormID.ToString());

//            personCarePlanFormRecordPage
//                 .WaitForPersonCarePlanFormRecordPageToLoad()
//                 .TapEditAssessmentBtn();

//            personCarePlanFormInitialAssessmentRecordPage
//                .WaitForPersonCarePlanInitialAssessmentRecordPageToLoad()
//                .TapYes_EasilyMakeMyselfUnderstoodRadioBtn()
//                .TapNewCareNeedBtn();

//            newCareNeedPopupPage
//                .WaitForNewCareNeedPopupPageToLoad()
//                .TapAddNeed()
//                .InsertNeedield_Txt("Need Test")
//                .TapAddOutcome()
//                .InsertOutComeField_Txt(" Outcome Test")
//                .TapSave()
//                .TapDismiss()
//                .TapCancel();

//            personCarePlanFormInitialAssessmentRecordPage
//               .WaitForPersonCarePlanInitialAssessmentRecordPageToLoad();






//        }

//        [TestProperty("JiraIssueID", "ACC-3094")]
//        [Description("Verify the creation of Outcomes from the Form Pre Requisites Active Care Plan should be present for the person Active Care Plan Record should has Care Plan Form with Initial Assessment document At least 1 need, 1 Outcome exist for the Selected question or entered in the Pop up")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]

//        public void Person_CarePlan_OutcomeCreation_UITestMethod003()
//        {

//            var percareplanexists = dbHelper.personCarePlan.GetByPersonID(_personID).Any();
//            if (!percareplanexists)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            }

//            if (_personCarePlanID == Guid.Empty)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.GetByPersonID(_personID).FirstOrDefault();
//            }


//            var personcareplanneedformexists = dbHelper.personCarePlanForm.GetByPersonId(_personID).Any();
//            if (!personcareplanneedformexists)
//                dbHelper.personCarePlanForm.CreatePersonCarePlanForm(_careDirectorQA_TeamId, _documentId, new DateTime(2020, 1, 1), _personID, _personCarePlanID);
//            _personCarePlanFormID = dbHelper.personCarePlanForm.GetByPersonId(_personID)[0];

//            _personCarePlanNeedID = dbHelper.personCarePlanNeed.CreatePersonCarePlanNeed(_careDirectorQA_TeamId, "Test", _personID, _personCarePlanID, DateTime.Now, _personCareNeedDomainID, "Automation Test Description");

//            _personCarePlanGoalId = dbHelper.personCarePlanGoal.CreatePersonCarePlanGoal(_careDirectorQA_TeamId, "test", _personID, _personCarePlanNeedID, DateTime.Today, "test description", _systemUserId, _carePlanGoalTypeId, _personCareNeedDomainID);

//            // _personCarePlanInterventionId = dbHelper.personCarePlanIntervention.CreatePersonCarePlaIntervention(_careDirectorQA_TeamId, "test", _personID, _personCarePlanGoalId, DateTime.Today, "test description", _systemUserId, _carePlanInterventionTypeId, _personCareNeedDomainID);

//            loginPage
//              .GoToLoginPage()
//              .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//              .WaitFormHomePageToLoad(true, false, false);

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

//            personCarePlanRecordPage
//               .WaitForPersonCarePlanRecordPageToLoad()
//               .NavigateToPersonCarePlanFormsCarePlanArea()
//               .WaitForPersonCarePlanFormPageToLoad()
//               .ClickOnCarePlanFormRecord(_personCarePlanFormID.ToString());

//            personCarePlanFormRecordPage
//                 .WaitForPersonCarePlanFormRecordPageToLoad()
//                 .TapEditAssessmentBtn();

//            personCarePlanFormInitialAssessmentRecordPage
//                .WaitForPersonCarePlanInitialAssessmentRecordPageToLoad()
//                .TapYes_EasilyMakeMyselfUnderstoodRadioBtn()
//                .TapNewCareNeedBtn();

//            newCareNeedPopupPage
//                .WaitForNewCareNeedPopupPageToLoad()
//                .TapAddNeed()
//                .InsertNeedield_Txt("Need Test")
//                .TapAddOutcome()
//                .InsertOutComeField_Txt(" Outcome Test")
//                .TapAddAction()
//                .InsertActionField_Txt("Action Test")
//                .TapSave()
//                .TapDismiss()
//                .TapCancel();

//            personCarePlanFormInitialAssessmentRecordPage
//               .WaitForPersonCarePlanInitialAssessmentRecordPageToLoad();

//        }


//        [TestProperty("JiraIssueID", "ACC-3095")]
//        [Description("Verify the Delete icon of Need / Outcome / Action from the Form Pre Requisites Active Care Plan should be present for the person Active Care Plan Record should has Care Plan Form with Initial Assessment document At least 1 Need with 1 Outcome , 1 Action exist for the Selected question or entered in the Pop up")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_OutcomeCreation_UITestMethod004()
//        {

//            var percareplanexists = dbHelper.personCarePlan.GetByPersonID(_personID).Any();
//            if (!percareplanexists)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            }

//            if (_personCarePlanID == Guid.Empty)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.GetByPersonID(_personID).FirstOrDefault();
//            }


//            var personcareplanneedformexists = dbHelper.personCarePlanForm.GetByPersonId(_personID).Any();
//            if (!personcareplanneedformexists)
//                dbHelper.personCarePlanForm.CreatePersonCarePlanForm(_careDirectorQA_TeamId, _documentId, new DateTime(2020, 1, 1), _personID, _personCarePlanID);
//            _personCarePlanFormID = dbHelper.personCarePlanForm.GetByPersonId(_personID)[0];

//            _personCarePlanNeedID = dbHelper.personCarePlanNeed.CreatePersonCarePlanNeed(_careDirectorQA_TeamId, "Test", _personID, _personCarePlanID, DateTime.Now, _personCareNeedDomainID, "Automation Test Description");

//            _personCarePlanGoalId = dbHelper.personCarePlanGoal.CreatePersonCarePlanGoal(_careDirectorQA_TeamId, "test", _personID, _personCarePlanNeedID, DateTime.Today, "test description", _systemUserId, _carePlanGoalTypeId, _personCareNeedDomainID);

//            _personCarePlanInterventionId = dbHelper.personCarePlanIntervention.CreatePersonCarePlaIntervention(_careDirectorQA_TeamId, "test", _personID, _personCarePlanGoalId, DateTime.Today, "test description", _systemUserId, _carePlanInterventionTypeId, _personCareNeedDomainID);

//            loginPage
//              .GoToLoginPage()
//              .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//              .WaitFormHomePageToLoad(true, false, false);

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

//            personCarePlanRecordPage
//               .WaitForPersonCarePlanRecordPageToLoad()
//               .NavigateToPersonCarePlanFormsCarePlanArea()
//               .WaitForPersonCarePlanFormPageToLoad()
//               .ClickOnCarePlanFormRecord(_personCarePlanFormID.ToString());

//            personCarePlanFormRecordPage
//                 .WaitForPersonCarePlanFormRecordPageToLoad()
//                 .TapEditAssessmentBtn();

//            personCarePlanFormInitialAssessmentRecordPage
//                .WaitForPersonCarePlanInitialAssessmentRecordPageToLoad()
//                .TapYes_EasilyMakeMyselfUnderstoodRadioBtn()
//                 .TapNewCareNeedBtn();

//            newCareNeedPopupPage
//                .WaitForNewCareNeedPopupPageToLoad()
//                .TapAddNeed()
//                .InsertNeedield_Txt("Need Test")
//                .TapAddOutcome()
//                .InsertOutComeField_Txt(" Outcome Test")
//                .TapAddAction()
//                .InsertActionField_Txt("Action Test")
//                .TapSave()
//                .TapDismiss()
//                .TapCancel();

//            personCarePlanFormInitialAssessmentRecordPage
//               .WaitForPersonCarePlanInitialAssessmentRecordPageToLoad()
//                 .TapNewCareNeedBtn();

//            newCareNeedPopupPage
//               .WaitForNewCareNeedPopupPageToLoad()
//               .TapDelete()
//               .TapOk()
//                .TapCancel();

//            personCarePlanFormInitialAssessmentRecordPage
//               .WaitForPersonCarePlanInitialAssessmentRecordPageToLoad()
//               .TapSavenCloseBtn();

//            var carePlanNeedRecords = dbHelper.personCarePlanNeed.GetByPersonId(_personID);
//            Assert.AreEqual(1, carePlanNeedRecords.Count);
//        }

//        [TestProperty("JiraIssueID", "ACC-3096")]
//        [Description("Verify the Structure of Add New Need Pop up from the Care Plan Form Pre Requisites Active Care Plan should be present for the person Active Care Plan Record should has Care Plan Form with Initial Assessment document ")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_OutcomeCreation_UITestMethod005()
//        {

//            var percareplanexists = dbHelper.personCarePlan.GetByPersonID(_personID).Any();
//            if (!percareplanexists)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            }

//            if (_personCarePlanID == Guid.Empty)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.GetByPersonID(_personID).FirstOrDefault();
//            }


//            var personcareplanneedformexists = dbHelper.personCarePlanForm.GetByPersonId(_personID).Any();
//            if (!personcareplanneedformexists)
//                dbHelper.personCarePlanForm.CreatePersonCarePlanForm(_careDirectorQA_TeamId, _documentId, new DateTime(2020, 1, 1), _personID, _personCarePlanID);
//            _personCarePlanFormID = dbHelper.personCarePlanForm.GetByPersonId(_personID)[0];

//            _personCarePlanNeedID = dbHelper.personCarePlanNeed.CreatePersonCarePlanNeed(_careDirectorQA_TeamId, "Test", _personID, _personCarePlanID, DateTime.Now, _personCareNeedDomainID, "Automation Test Description");


//            loginPage
//              .GoToLoginPage()
//              .Login(_loginUsername, "Passw0rd_!", "Care Providers");

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

//            personCarePlanRecordPage
//               .WaitForPersonCarePlanRecordPageToLoad()
//               .NavigateToPersonCarePlanFormsCarePlanArea()
//               .WaitForPersonCarePlanFormPageToLoad()
//               .ClickOnCarePlanFormRecord(_personCarePlanFormID.ToString());

//            personCarePlanFormRecordPage
//                 .WaitForPersonCarePlanFormRecordPageToLoad()
//                 .TapEditAssessmentBtn();

//            personCarePlanFormInitialAssessmentRecordPage
//                .WaitForPersonCarePlanInitialAssessmentRecordPageToLoad()
//                .TapYes_EasilyMakeMyselfUnderstoodRadioBtn()
//                 .TapNewCareNeedBtn();

//            System.Threading.Thread.Sleep(1000);

//            newCareNeedPopupPage
//                .WaitForNewCareNeedPopupPageToLoad()
//                .TapAddNeed()
//                .InsertNeedield_Txt("Need Test")
//                .TapAddOutcome()
//                .InsertOutComeField_Txt(" Outcome Test")
//                .TapAddAction()
//                .InsertActionField_Txt("Action Test")
//                 .TapAddNeed()
//                .InsertNeedield2_Txt("Need Test1")
//                 .TapSave()
//                .TapDismiss()
//                .TapCancel();

//            personCarePlanFormInitialAssessmentRecordPage
//               .WaitForPersonCarePlanInitialAssessmentRecordPageToLoad()
//               .TapSavenCloseBtn();

//            var carePlanNeedRecords = dbHelper.personCarePlanNeed.GetByPersonId(_personID);
//            Assert.AreEqual(3, carePlanNeedRecords.Count); // 1 Created from DB & 2 Created from UI


//        }
//        #endregion

//        #region https://advancedcsg.atlassian.net/browse/CDV6-16407

//        [TestProperty("JiraIssueID", "ACC-3097")]
//        [Description("Verify the fields of Abbey Pain Scale care plan form.Pre Requisites :Selected person should have care plan with “Abbey Pain Scale“ form")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_VerifyAbbeyForm_UITestMethod001()
//        {

//            var percareplanexists = dbHelper.personCarePlan.GetByPersonID(_personID).Any();
//            if (!percareplanexists)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            }

//            if (_personCarePlanID == Guid.Empty)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.GetByPersonID(_personID).FirstOrDefault();
//            }


//            var personcareplanneedformexists = dbHelper.personCarePlanForm.GetByPersonId(_personID).Any();
//            if (!personcareplanneedformexists)
//                dbHelper.personCarePlanForm.CreatePersonCarePlanForm(_careDirectorQA_TeamId, _newdocumentId, new DateTime(2020, 1, 1), _personID, _personCarePlanID);
//            _personCarePlanFormID = dbHelper.personCarePlanForm.GetByPersonId(_personID)[0];

//            // _personCarePlanNeedID = dbHelper.personCarePlanNeed.CreatePersonCarePlanNeed(_careDirectorQA_TeamId, "Test", _personID, _personCarePlanID, DateTime.Now, _personCareNeedDomainID, "Automation Test Description");

//            //_personCarePlanGoalId = dbHelper.personCarePlanGoal.CreatePersonCarePlanGoal(_careDirectorQA_TeamId, "test", _personID, _personCarePlanNeedID, DateTime.Today, "test description", _systemUserId, _carePlanGoalTypeId, _personCareNeedDomainID);

//            //  _personCarePlanInterventionId = dbHelper.personCarePlanIntervention.CreatePersonCarePlaIntervention(_careDirectorQA_TeamId, "test", _personID, _personCarePlanGoalId, DateTime.Today, "test description", _systemUserId, _carePlanInterventionTypeId, _personCareNeedDomainID);

//            loginPage
//              .GoToLoginPage()
//              .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//              .WaitFormHomePageToLoad(true, false, false);

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickCarePlansLink();

//            personCarePlansSubPage_CarePlansTab
//                .WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
//                .ClickOnCarePlanRecord(_personCarePlanID.ToString());

//            personCarePlanRecordPage
//               .WaitForPersonCarePlanRecordPageToLoad()
//               .NavigateToPersonCarePlanFormsCarePlanArea()
//               .WaitForPersonCarePlanFormPageToLoad()
//               .ClickOnCarePlanFormRecord(_personCarePlanFormID.ToString());

//            personCarePlanFormRecordPage
//                 .WaitForPersonCarePlanFormRecordPageToLoad()
//                 .TapEditAssessmentBtn();

//            personCarePlanFormInitialAssessmentRecordPage
//                .WaitForPersonCarePlanInitialAssessmentRecordPageToLoad()
//                .ValidateFormLabel("Vocalisation (e.g. whimpering, groaning, crying)", "Vocalisation")
//                .ValidateFormLabel("Facial Expression (e.g. looking tense, frowning, grimace, looking frightened)", "Facial Expression")
//                .ValidateFormLabel("Change in body language (e.g. fidgeting, rocking, guarding part of body, withdrawn)", "Change in body language")
//                .ValidateFormLabel("Behavioural change (e.g. increased confusion, refusing to eat, alteration to usual patterns)", "Behavioural change")
//                .ValidateFormLabel("Physiological change (e.g. temperature, pulse or blood pressure outside of normal limits, perspiring, flushing or pallor)", "Physiological change")
//                .ValidateFormLabel("Physical Changes (e.g. Skin tears, pressure areas, arthritis contractures, previous injuries)", "Physical Changes")
//                .ValidateFormLabel("Type of Pain", "Type of Pain")
//                .ValidateFormLabel("Referrals", "Referrals")
//                .ValidateFormLabel("Total Pain Score", "Total Pain Score");

//        }

//        [TestProperty("JiraIssueID", "ACC-3098")]
//        [Description("Verify the fields of Abbey Pain Scale care plan form.Pre Requisites :Selected person should have care plan with “Abbey Pain Scale“ form")]
//        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        public void Person_CarePlan_VerifyAbbeyForm_UITestMethod002()
//        {

//            var percareplanexists = dbHelper.personCarePlan.GetByPersonID(_personID).Any();
//            if (!percareplanexists)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5), 1, 1, _careDirectorQA_TeamId);
//            }

//            if (_personCarePlanID == Guid.Empty)
//            {
//                _personCarePlanID = dbHelper.personCarePlan.GetByPersonID(_personID).FirstOrDefault();
//            }

//            /*
//                        var personcareplanneedformexists = dbHelper.personCarePlanForm.GetByPersonId(_personID).Any();
//                        if (!personcareplanneedformexists)
//                            dbHelper.personCarePlanForm.CreatePersonCarePlanForm(_careDirectorQA_TeamId, _newdocumentId, new DateTime(2020, 1, 1), _personID, _personCarePlanID);
//                        _personCarePlanFormID = dbHelper.personCarePlanForm.GetByPersonId(_personID)[0];*/

//            var personcareplanforminprogress = dbHelper.personCarePlanForm.CreatePersonCarePlanForm(_careDirectorQA_TeamId, _newdocumentId, new DateTime(2020, 1, 1), _personID, _personCarePlanID);
//            dbHelper.personCarePlanForm.UpdateAssessmentStatusid(personcareplanforminprogress, 2);

//            var personcareplanformcomplete = dbHelper.personCarePlanForm.CreatePersonCarePlanForm(_careDirectorQA_TeamId, _newdocumentId, new DateTime(2020, 1, 1), _personID, _personCarePlanID);
//            dbHelper.personCarePlanForm.UpdateAssessmentStatusid(personcareplanforminprogress, 4);
//            dbHelper.personCarePlanForm.UpdateAssessmentStatusid(personcareplanforminprogress, 2);

//            var personcareplanform1 = dbHelper.personCarePlanForm.CreatePersonCarePlanForm(_careDirectorQA_TeamId, _newdocumentId, new DateTime(2020, 1, 1), _personID, _personCarePlanID);
//            dbHelper.personCarePlanForm.UpdateAssessmentStatusid(personcareplanform1, 4);
//            // _personCarePlanNeedID = dbHelper.personCarePlanNeed.CreatePersonCarePlanNeed(_careDirectorQA_TeamId, "Test", _personID, _personCarePlanID, DateTime.Now, _personCareNeedDomainID, "Automation Test Description");

//            //_personCarePlanGoalId = dbHelper.personCarePlanGoal.CreatePersonCarePlanGoal(_careDirectorQA_TeamId, "test", _personID, _personCarePlanNeedID, DateTime.Today, "test description", _systemUserId, _carePlanGoalTypeId, _personCareNeedDomainID);

//            //  _personCarePlanInterventionId = dbHelper.personCarePlanIntervention.CreatePersonCarePlaIntervention(_careDirectorQA_TeamId, "test", _personID, _personCarePlanGoalId, DateTime.Today, "test description", _systemUserId, _carePlanInterventionTypeId, _personCareNeedDomainID);

//            loginPage
//              .GoToLoginPage()
//              .Login(_loginUsername, "Passw0rd_!", "Care Providers")
//              .WaitFormHomePageToLoad(true, false, false);

//            mainMenu
//                .WaitForMainMenuToLoad()
//                .NavigateToPeopleSection();

//            peoplePage
//                .WaitForPeoplePageToLoad()
//                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
//                .OpenPersonRecord(_personID.ToString());

//            personRecordPage
//                .WaitForPersonRecordPageToLoad()
//                .TapCarePlansTab();

//            personCarePlansSubPage
//                .WaitForPersonCarePlansSubPageToLoad()
//                .ClickAssessmentsLink();

//            PersonCarePlansSubPage_AssessmentsTab
//                .WaitForPersonCarePlansSubPage_AssessmentsTabToLoad();


//            var carePlanAssessmentRecordwithstatus1 = dbHelper.personCarePlanForm.GetByPersonIdandAssessmentStatusId(_personID, 1);
//            Assert.AreEqual(1, carePlanAssessmentRecordwithstatus1.Count);

//            var carePlanAssessmentRecordwithstatus2 = dbHelper.personCarePlanForm.GetByPersonIdandAssessmentStatusId(_personID, 2);
//            Assert.AreEqual(1, carePlanAssessmentRecordwithstatus2.Count);

//            var carePlanAssessmentRecordwithstatus4 = dbHelper.personCarePlanForm.GetByPersonIdandAssessmentStatusId(_personID, 4);
//            Assert.AreEqual(1, carePlanAssessmentRecordwithstatus4.Count);
//        }

//        #endregion




//    }
//}
