using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.Providers
{
    /// <summary>
    /// This class contains Automated UI test scripts for Service Provided Record Duplicate Check
    /// </summary>
    [TestClass]
    public class Provider_ServiceProvided_DuplicateCheck_UITestCases : FunctionalTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-17924

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _systemUserId;
        private Guid _languageId;
        private Guid _serviceElement1Id;
        private Guid _serviceElement2Id;
        private Guid _serviceElement3Id;
        private string _serviceElement1IdName;
        private string _serviceElement2IdName;
        private string _serviceElement3IdName;
        private Guid _serviceMappingId;
        private Guid _clientCategory1Id;
        private string _clientCategory1IdName;
        private Guid _clientCategory2Id;
        private string _clientCategory2IdName;
        private Guid _providerId;
        private int _providerNumber;
        private string _systemUsername;
        private string _provider1Name = "CDV6-17924_Provider1" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss FFFFF");
        private string _provider2Name = "CDV6-17924_Provider2" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss FFFFF");

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {


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

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Create Default System User 

                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_1").Any();
                if (!adminUserExists)
                {
                    _systemUserId = dbHelper.systemUser.CreateSystemUser("CW_Forms_Test_User_1", "CW", "Forms Test User 1", "CW Forms Test User 1", "Passw0rd_!", "CW_Forms_Test_User_1@somemail.com", "CW_Forms_Test_User_1@othermail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                    //foreach (var userSecProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_systemUserId))
                    //    dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecProfileId);

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserId, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserId, systemUserSecureFieldsSecurityProfileId);
                }

                if (Guid.Empty == _systemUserId)
                    _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_1").FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemUserId, DateTime.Now.Date);
                _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId, "username")["username"];

                #endregion

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("CDV6-17924_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "CDV6-17924_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("CDV6-17924_Ethnicity")[0];

                #endregion

                #region Person

                var personRecordExists = dbHelper.person.GetByFirstName("CDV6-17924_TestUser").Any();
                if (!personRecordExists)
                {
                    _personID = dbHelper.person.CreatePersonRecord("", "CDV6-17924_TestUser", "", "1", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                if (_personID == Guid.Empty)
                {
                    _personID = dbHelper.person.GetByFirstName("CDV6-17924_TestUser").FirstOrDefault();
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                _personFullName = "CDV6-17924_TestUser 1";

                #endregion

                #region Provider
                _providerId = dbHelper.provider.CreateProvider(_provider1Name, _careDirectorQA_TeamId, 2);
                _providerId = dbHelper.provider.GetProviderByName(_provider1Name).FirstOrDefault();
                _providerNumber = (int)dbHelper.provider.GetProviderByID(_providerId, "providernumber")["providernumber"];

                #endregion

                #region  ServiceElement1

                var serviceElement1Exist = dbHelper.serviceElement1.GetByName("CDV6-17924_ServiceElement1").Any();
                if (!serviceElement1Exist)
                    dbHelper.serviceElement1.CreateServiceElement1(_careDirectorQA_TeamId, "CDV6-17924_ServiceElement1", new DateTime(2020, 1, 1), 17924, 1, 1);
                _serviceElement1Id = dbHelper.serviceElement1.GetByName("CDV6-17924_ServiceElement1")[0];
                _serviceElement1IdName = (string)dbHelper.serviceElement1.GetByID(_serviceElement1Id, "name")["name"];


                #endregion

                #region  ServiceElement2

                var serviceElement2Exist = dbHelper.serviceElement2.GetByName("CDV6-17924_ServiceElement2").Any();
                if (!serviceElement2Exist)
                    dbHelper.serviceElement2.CreateServiceElement2(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "CDV6-17924_ServiceElement2", new DateTime(2020, 1, 1), 17925);
                _serviceElement2Id = dbHelper.serviceElement2.GetByName("CDV6-17924_ServiceElement2")[0];
                _serviceElement2IdName = (string)dbHelper.serviceElement2.GetByID(_serviceElement2Id, "name")["name"];

                #endregion

                #region  ServiceElement3

                var serviceElement3Exist = dbHelper.serviceElement3.GetByName("CDV6-17924_ServiceElement3").Any();
                if (!serviceElement3Exist)
                    dbHelper.serviceElement3.CreateServiceElement3(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "CDV6-17924_ServiceElement3", new DateTime(2020, 1, 1), 17926);
                _serviceElement3Id = dbHelper.serviceElement3.GetByName("CDV6-17924_ServiceElement3")[0];
                _serviceElement3IdName = (string)dbHelper.serviceElement3.GetByID(_serviceElement3Id, "name")["name"];

                #endregion

                #region Client Category

                var clientCategory1Exist = dbHelper.financeClientCategory.GetByName("CDV6-17924_ClientCategory1").Any();
                if (!clientCategory1Exist)
                    dbHelper.financeClientCategory.CreateFinanceClientCategory(_careDirectorQA_TeamId, "CDV6-17924_ClientCategory1", new DateTime(2020, 1, 1), "17924");
                _clientCategory1Id = dbHelper.financeClientCategory.GetByName("CDV6-17924_ClientCategory1").FirstOrDefault();
                _clientCategory1IdName = (string)dbHelper.financeClientCategory.GetByID(_clientCategory1Id, "name")["name"];

                #endregion

                #region Client Category 2

                var clientCategory2Exist = dbHelper.financeClientCategory.GetByName("CDV6-17924_ClientCategory2").Any();
                if (!clientCategory2Exist)
                    dbHelper.financeClientCategory.CreateFinanceClientCategory(_careDirectorQA_TeamId, "CDV6-17924_ClientCategory2", new DateTime(2020, 1, 1), "17924");
                _clientCategory2Id = dbHelper.financeClientCategory.GetByName("CDV6-17924_ClientCategory2").FirstOrDefault();
                _clientCategory2IdName = (string)dbHelper.financeClientCategory.GetByID(_clientCategory2Id, "name")["name"];

                #endregion

                #region Service Mapping 

                var serviceMappingExist = dbHelper.serviceMapping.GetByName("CDV6-17924_ServiceElement1 \\ CDV6-17924_ServiceElement2").Any();
                if (!serviceMappingExist)
                    dbHelper.serviceMapping.CreateServiceMapping(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _serviceElement1Id, _serviceElement2Id);
                _serviceMappingId = dbHelper.serviceMapping.GetByName("CDV6-17924_ServiceElement1 \\ CDV6-17924_ServiceElement2").FirstOrDefault();

                #endregion                

            }
            catch
            {
                if (driver != null)
                    driver.Close();

                throw;
            }

        }

        [TestProperty("JiraIssueID", "CDV6-18914")]
        [Description("Automated UI Test - Duplicate check for Provider > Service Provided record. - " +
            "Create a Service Provided record 1 for a Provider where - Approval status is Pending.- " +
            "Create a second Service Provided record for the Provider where -" +
            "1. Service Element 1, Service Element 2, Service Element 3 and Client Category values are same as that of Service Provided record 1" +
            "2. Approval status is Pending. Save the record." +
            "Validate Duplicate detected error message." +
            "Change Approval status of record 2 to 'Approved'." +
            "Validate Duplicate detected error message." +
            "Change Approval status of record 1 to 'Approved'. Create a record 2 where - Approval status is Pending." +
            "Validate Duplicate detected error message." +
            "Create a record 2 where - Approval status is 'Approved'." +
            "Validate Duplicate detected error message." +
            "Create a Service Provided record SP2 with different Client Category value where - Approval status is Pending." +
            "Validate that record is created." +
            "Create a Service Provided record SP2 with blank Client Category value where - Approval status is Pending." +
            "Validate that record is created.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Provider_ServiceProvided_DuplicateCheck_UITestMethod01()
        {

            #region Create a Service Provided record - SP1 with status "Pending"

            Guid _serviceProvided1Id = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, _serviceElement3Id, _clientCategory1Id, null, null, 1, false);

            #endregion

            #region 1. Create a Service Provided record with same values - SP2. Validate Duplicate record detected error message.

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!")
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerNumber.ToString(), _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ClickNewRecordButton();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement1IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement1Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement2IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement2Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement3LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement3IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement3Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectClientCategoryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_clientCategory1IdName)
                .TapSearchButton()
                .SelectResultElement(_clientCategory1Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("You are trying to create a record that is a duplicate i.e. where Provider, Service Element 1, Service Element 2, Service Element 3, Client Category, Negotiated Rates Apply and Contract Type are identical. Please correct as necessary.")
                 .TapCloseButton();

            #endregion

            #region 2. Change Approval Status of record SP2 to "Approved". Validate Duplicate record error message.
            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectApprovalStatus("Approved")
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("You are trying to create a record that is a duplicate i.e. where Provider, Service Element 1, Service Element 2, Service Element 3, Client Category, Negotiated Rates Apply and Contract Type are identical. Please correct as necessary.")
                 .TapCloseButton();

            #endregion

            #region 3. Change Approval Status of record SP1 to Approved. Create a record SP2 with same values and status "Pending". Validate Duplicate record error messge.
            dbHelper.serviceProvided.UpdateStatus(_serviceProvided1Id, 2);


            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectApprovalStatus("Pending")
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("You are trying to create a record that is a duplicate i.e. where Provider, Service Element 1, Service Element 2, Service Element 3, Client Category, Negotiated Rates Apply and Contract Type are identical. Please correct as necessary.")
                 .TapCloseButton();
            #endregion

            #region 4. Create a record SP2 with same values and status "Approved". Validate Duplicate record error messge.

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectApprovalStatus("Approved")
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("You are trying to create a record that is a duplicate i.e. where Provider, Service Element 1, Service Element 2, Service Element 3, Client Category, Negotiated Rates Apply and Contract Type are identical. Please correct as necessary.")
                 .TapCloseButton();

            #endregion

            #region 5. Create a Service Provided record SP2 with a different Client Category value and status "Pending". Validate that record is created.

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectClientCategoryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_clientCategory2IdName)
                .TapSearchButton()
                .SelectResultElement(_clientCategory2Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ClickSaveButton()
                .WaitForRecordPageTitleToLoad(_provider1Name + " \\ " + _serviceElement1IdName + " \\ " + _serviceElement2IdName + " \\ " + _serviceElement3IdName
                                                + " \\ " + _clientCategory2IdName + " \\ Spot");

            int recordCount = dbHelper.serviceProvided.GetByProviderId(_providerId).Count();
            Assert.AreEqual(2, recordCount);

            #endregion

            #region 6. Create a Service Provided record SP2 with a blank Client Category value and status "Pending". Validate that record is created.

            serviceProvidedRecordPage
                .ClickBackButton();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ClickNewRecordButton();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement1IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement1Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement2IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement2Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement3LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement3IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement3Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ClickSaveButton();

            recordCount = dbHelper.serviceProvided.GetByProviderId(_providerId).Count();
            Assert.AreEqual(3, recordCount);



            #endregion

        }


        [TestProperty("JiraIssueID", "CDV6-18915")]
        [Description("Automated UI Test - Provider > Service Provided record creation." +
            "Create a Service Provided record 1 for a Provider where - Approval status is Approved." +
            "Create a second Service Provided record for the Provider where - Approval status is Unpproved." +
            "1. Validate Duplicate detected error message." +
            "Create a Service Provided record SP1 and SP2 with same values for Service Element 1, Service Element 2, Service Element 3, Client Category and status 'Unapproved'" +
            "2. Validate that the records SP1 and SP2 are created." +
            "Create a Service Provided record SP1 and SP2 with different values for Service Element 1, Service Element 2, Service Element 3 and Client Category as blank and status 'Unapproved'" +
            "Validate that the record SP 3is created.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Provider_ServiceProvided_DuplicateCheck_UITestMethod02()
        {

            #region 7. Create a Service Provided record - SP1 with status "Approved". Create a Service Provided record - SP2 with status "Unapproved". Validate error message.

            Guid _serviceProvided1Id = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, _serviceElement3Id, _clientCategory1Id, null, null, 2, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerNumber.ToString(), _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ClickNewRecordButton();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement1IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement1Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement2IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement2Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement3LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement3IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement3Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectClientCategoryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_clientCategory1IdName)
                .TapSearchButton()
                .SelectResultElement(_clientCategory1Id.ToString());


            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectApprovalStatus("Unapproved")
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("You are trying to create a record that is a duplicate i.e. where Provider, Service Element 1, Service Element 2, Service Element 3, Client Category, Negotiated Rates Apply and Contract Type are identical. Please correct as necessary.")
                 .TapCloseButton();

            int recordCount = dbHelper.serviceProvided.GetByProviderId(_providerId).Count();
            Assert.AreEqual(1, recordCount);

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ClickBackButton();


            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            #endregion

            #region 8. Create a Service Provided record SP1 and SP2 with same values for SE1, SE2, SE3, Client Category and status "Unapproved". Validate that the record SP1 and SP2 are created.            
            Guid _providerId2 = dbHelper.provider.CreateProvider(_provider2Name, _careDirectorQA_TeamId, 2);
            _providerId2 = dbHelper.provider.GetProviderByName(_provider2Name).FirstOrDefault();
            int _providerNumber2 = (int)dbHelper.provider.GetProviderByID(_providerId2, "providernumber")["providernumber"];

            Guid _serviceProvided2Id = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId2, _serviceElement1Id, _serviceElement2Id, _serviceElement3Id, _clientCategory1Id, null, null, 3, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerNumber2.ToString(), _providerId2.ToString())
                .OpenProviderRecord(_providerId2.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();


            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ClickNewRecordButton();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement1IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement1Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement2IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement2Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement3LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement3IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement3Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectClientCategoryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_clientCategory1IdName)
                .TapSearchButton()
                .SelectResultElement(_clientCategory1Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectApprovalStatus("Unapproved")
                .ClickSaveButton()
                .WaitForRecordPageTitleToLoad(_provider2Name + " \\ " + _serviceElement1IdName + " \\ " + _serviceElement2IdName + " \\ " + _serviceElement3IdName
                                                + " \\ " + _clientCategory1IdName + " \\ Spot");

            recordCount = dbHelper.serviceProvided.GetByProviderId(_providerId2).Count();
            Assert.AreEqual(2, recordCount);


            #endregion

            #region 9. Create a Service Provided record SP3 with different values for SE1, SE2, SE3, Client Category and status "Unapproved". Validate that the record SP3 is created.
            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ClickBackButton();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ClickNewRecordButton();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement1IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement1Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement2IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement2Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectApprovalStatus("Unapproved")
                .ClickSaveButton()
                .WaitForRecordPageTitleToLoad(_provider2Name + " \\ " + _serviceElement1IdName + " \\ " + _serviceElement2IdName + " \\ \\ \\ Spot");

            recordCount = dbHelper.serviceProvided.GetByProviderId(_providerId2).Count();
            Assert.AreEqual(3, recordCount);

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-18916")]
        [Description("Automated UI Test - Provider > Service Provided record deletion." +
            "Create a Service Provided record for a Provider, set Approval status is Pending. Select and Delete the record." +
            "1. Validate that record is deleted." +
            "Create a Service Provided record for a Provider, set Approval status is Approved. Select and Delete the record." +
            "2. Validate that record is not deleted.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Provider_ServiceProvided_DuplicateCheck_UITestMethod03()
        {

            #region 10. Create a Service Provided record with status Pending. Verify record is deleted.

            Guid _serviceProvided1Id = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, null, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerNumber.ToString(), _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ValidateRecordPresent(_serviceProvided1Id.ToString());

            int recordCount = dbHelper.serviceProvided.GetByProviderId(_providerId).Count();
            Assert.AreEqual(1, recordCount);

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .SelectServiceProvidedRecord(_serviceProvided1Id.ToString())
                .DeleteServiceProvidedRecord();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordNotPresent(_serviceProvided1Id.ToString());

            recordCount = dbHelper.serviceProvided.GetByProviderId(_providerId).Count();
            Assert.AreEqual(0, recordCount);

            #endregion

            #region 11. Create a Service Provided record with status Unapproved. Verify record is not deleted.

            Guid _serviceProvided2Id = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, null, 3, false);

            servicesProvidedPage
                .ClickRefreshButton();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ValidateRecordPresent(_serviceProvided2Id.ToString());

            recordCount = dbHelper.serviceProvided.GetByProviderId(_providerId).Count();
            Assert.AreEqual(1, recordCount);

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .SelectServiceProvidedRecord(_serviceProvided2Id.ToString())
                .OpenServiceProvidedRecord(_serviceProvided2Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("The record could not be deleted. Only Service Provided records with a status of Pending can be deleted.")
                 .TapCloseButton();


            #endregion

            #region 12. Create a Service Provided record with status Approved. Verify record is not deleted.

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ClickBackButton();

            Guid _serviceProvided3Id = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, null, 2, false);

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordPresent(_serviceProvided3Id.ToString());

            recordCount = dbHelper.serviceProvided.GetByProviderId(_providerId).Count();
            Assert.AreEqual(2, recordCount);

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .SelectServiceProvidedRecord(_serviceProvided3Id.ToString())
                .OpenServiceProvidedRecord(_serviceProvided3Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("The record could not be deleted. Only Service Provided records with a status of Pending can be deleted.")
                 .TapCloseButton();


            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-18918")]
        [Description("Automated UI Test - Provider > Service Provided record deactivate and Duplicate check." +
            "Create a Service Provided record for a Provider, set Approval status is Pending. Select and Delete the record." +
            "1. Validate that record is deleted." +
            "Create a Service Provided record for a Provider, set Approval status is Approved. Select and Delete the record." +
            "2. Validate that record is not deleted.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Provider_ServiceProvided_DuplicateCheck_UITestMethod04()
        {

            #region 13. Create a Service Provided record with status Pending. Verify record is made inactive.

            Guid _serviceProvided1Id = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, null, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerNumber.ToString(), _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ValidateRecordPresent(_serviceProvided1Id.ToString())
                .SelectServiceProvidedRecord(_serviceProvided1Id.ToString())
                .OpenServiceProvidedRecord(_serviceProvided1Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .InactiveStatus("Yes")
                .ClickSaveButton()
                .ValidateRecordIsInactive()
                .ValidateInactiveStatus("Yes")
                .ClickBackButton();


            #endregion

            #region 14. Create a Service Provided record with same values as Service Provided record 1 and same status Pending. Verify record is created.

            Guid _serviceProvided2Id = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, null, 1, false);

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordPresent(_serviceProvided2Id.ToString());

            int recordCount = dbHelper.serviceProvided.GetByProviderId(_providerId).Count();
            Assert.AreEqual(2, recordCount);

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .OpenServiceProvidedRecord(_serviceProvided1Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(false)
                .ClickActivateButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.")
                .TapOKButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("You are trying to create a record that is a duplicate i.e. where Provider, Service Element 1, Service Element 2, Service Element 3, Client Category, Negotiated Rates Apply and Contract Type are identical. Please correct as necessary.")
                 .TapCloseButton();

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
