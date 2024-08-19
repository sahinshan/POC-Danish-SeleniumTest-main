using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.UITests.Providers
{
    [TestClass]
    public class ServiceProvided_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _personID;
        private string _systemUserName;
        private Guid _systemUserId;
        private Guid _languageId;
        private Guid _serviceElement1Id;
        private Guid _serviceElement2Id;
        private Guid _serviceElement2Id_2;
        private Guid _serviceElement3Id;
        private string _serviceElement1IdName;
        private string _serviceElement2IdName;
        private string _serviceElement2IdName_2;
        private string _serviceElement3IdName;
        private Guid _providerId;
        private int _providerNumber;
        private string _providerName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _clientCategoryId;
        private string _clientCategoryName;
        private Guid _currentRannkingId;
        private string _currentRannkingName;
        private Guid Draft_Serviceprovisionstatusid;
        private Guid Authorised_Serviceprovisionstatusid;
        private Guid _serviceprovisionstartreasonid;
        private Guid _serviceprovisionendreasonid;
        private Guid _placementRoomTypeId;
        private Guid defaulRateUnitID;

        #endregion

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Default System User 

                _systemUserName = "Service_Provided_User_1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Service Provider", "User 1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "CDV6-17924_Ethnicity", new DateTime(2020, 1, 1));

                #endregion

                #region Person

                var firstName = "Provide_Person";
                var lastName = "LN_" + _currentDateSuffix;
                _personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2003, 1, 2));

                #endregion

                #region Provider - Supplier

                _providerName = "Provider_" + _currentDateSuffix;
                _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 2, _providerName + "@test.com");
                _providerNumber = (int)dbHelper.provider.GetProviderByID(_providerId, "providernumber")["providernumber"];

                #endregion

                #region Service Element 1 & 2

                var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
                var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
                _serviceElement1IdName = "Service_Provided_SE1";
                _serviceElement2IdName = "Service_Provided_SE2";
                _serviceElement2IdName_2 = "Service_Provided_SE2_2";

                defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
                var validRateUnits = new List<Guid>();
                validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());

                validRateUnits.Add(defaulRateUnitID);
                if (!dbHelper.serviceElement1.GetByName(_serviceElement1IdName).Any())
                    _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, 1, 1, validRateUnits, defaulRateUnitID);
                _serviceElement1Id = dbHelper.serviceElement1.GetByName(_serviceElement1IdName)[0];

                if (!dbHelper.serviceElement2.GetByName(_serviceElement2IdName).Any())
                    dbHelper.serviceElement2.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2022, 1, 1), code);
                _serviceElement2Id = dbHelper.serviceElement2.GetByName(_serviceElement2IdName)[0];

                if (!dbHelper.serviceElement2.GetByName(_serviceElement2IdName_2).Any())
                    dbHelper.serviceElement2.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName_2, new DateTime(2022, 1, 1), code2);
                _serviceElement2Id_2 = dbHelper.serviceElement2.GetByName(_serviceElement2IdName_2)[0];

                commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);
                commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id_2);

                #endregion

                #region  Service Element 3

                _serviceElement3IdName = "Service_Provided_SE3";
                if (!dbHelper.serviceElement3.GetByName(_serviceElement3IdName).Any())
                    dbHelper.serviceElement3.CreateServiceElement3(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _serviceElement3IdName, new DateTime(2022, 1, 1), code);
                _serviceElement3Id = dbHelper.serviceElement3.GetByName(_serviceElement3IdName)[0];

                #endregion

                #region Client Category

                _clientCategoryName = "Default Category";
                _clientCategoryId = commonMethodsDB.CreateFinanceClientCategory(_careDirectorQA_TeamId, _clientCategoryName, new DateTime(2022, 1, 1), code.ToString());

                #endregion

                #region Current Ranking

                _currentRannkingName = "Outstanding";
                _currentRannkingId = dbHelper.currentRanking.GetCurrentRankingByName(_currentRannkingName).FirstOrDefault();

                #endregion

                #region Service Provision Status

                Draft_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
                Authorised_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

                #endregion

                #region Service Provision Start Reason

                _serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Default", new DateTime(2022, 1, 1));

                #endregion

                #region Service Provision End Reason

                _serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_careDirectorQA_TeamId, "Default", new DateTime(2022, 1, 1));

                #endregion

                #region Placement Room Type

                _placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Close();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-352

        [TestProperty("JiraIssueID", "ACC-354")]
        [Description("Create Service Provided record for Provider type Supplier" +
                    "Validate Service Element 1 and Service Element 2 Mappling Records" +
                    "Save record and validate fields is disable or not" +
                    "Change Approval status to Approval and Validate fields is disable or not and also" +
                    "Validate Pending arrpoval status option is disable")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServicesProvided_UITestMethod01()
        {
            #region Step 1 to 3

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerNumber.ToString(), _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            #endregion

            #region Step 4, 5 & 6

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ClickNewRecordButton();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement1LookupButton();

            #region Step 5

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement1IdName)
                .TapSearchButton()
                .ValidateResultElementPresent(_serviceElement1Id.ToString())
                .SelectResultElement(_serviceElement1Id.ToString());

            #endregion

            #region Step 6

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement2IdName_2)
                .TapSearchButton()
                .ValidateResultElementPresent(_serviceElement2Id_2.ToString())
                .TypeSearchQuery(_serviceElement2IdName)
                .TapSearchButton()
                .ValidateResultElementPresent(_serviceElement2Id.ToString())
                .SelectResultElement(_serviceElement2Id.ToString());

            #endregion

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
                .TypeSearchQuery(_clientCategoryName)
                .TapSearchButton()
                .SelectResultElement(_clientCategoryId.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectCurrentRankingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_currentRannkingName)
                .TapSearchButton()
                .SelectResultElement(_currentRannkingId.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .InsertNotes("Automation Script to test Provided test.")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            #endregion

            #region Step 7

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ValidateApprovalStatusSelectedText("Pending")
                .ValidateProviderLookupButtondDisabled(true)
                .ValidateServiceElement1LookupButtondDisabled(false)
                .ValidateServiceElement2LookupButtondDisabled(false)
                .ValidateServiceElement3LookupButtondDisabled(false)
                .ValidateContractTypePickListDisabled(false)
                .ValidateResponsibleTeamLookupButtondDisabled(true)
                .ValidateResponsibleUserLookupButtondDisabled(false)
                .ValidateApprovalStatusPickListDisabled(false)
                .ValidateServiceProvidedNumberFieldDisabled(true)
                .ValidateClientCategoryLookupButtondDisabled(false)
                .ValidateCurrentRankingLookupButtondDisabled(false)
                .ValidateGLCodeLookupButtondDisabled(false)
                .ValidateNegotiatedRatesApplyOptionsDisabled(false)
                .ValidateUsedInFinanceOptionsDisabled(true)
                .ValidateInactiveOptionsDisabled(false);

            serviceProvidedRecordPage
                .SelectApprovalStatus("Approved")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            #endregion

            #region Step 8

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ValidateApprovalStatusOptionDisabled("Pending", true)
                .ValidateApprovalStatusOptionDisabled("Unapproved", false);

            #endregion

            #region Step 9

            serviceProvidedRecordPage
                .ValidateProviderLookupButtondDisabled(true)
                .ValidateServiceElement1LookupButtondDisabled(true)
                .ValidateServiceElement2LookupButtondDisabled(true)
                .ValidateServiceElement3LookupButtondDisabled(true)
                .ValidateContractTypePickListDisabled(true)
                .ValidateResponsibleTeamLookupButtondDisabled(true)
                .ValidateResponsibleUserLookupButtondDisabled(false)
                .ValidateApprovalStatusPickListDisabled(false)
                .ValidateServiceProvidedNumberFieldDisabled(true)
                .ValidateClientCategoryLookupButtondDisabled(true)
                .ValidateCurrentRankingLookupButtondDisabled(false)
                .ValidateGLCodeLookupButtondDisabled(false)
                .ValidateNegotiatedRatesApplyOptionsDisabled(true)
                .ValidateUsedInFinanceOptionsDisabled(true)
                .ValidateInactiveOptionsDisabled(false)
                .ValidateNotesFieldDisabled(false);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-368

        [TestProperty("JiraIssueID", "ACC-361")]
        [Description("Validate Multiple Service Provided records." +
                    "Validate Related Itesm after Approved Service Provided Status" +
                    "Validate Used In Finance Yes Option checked after Service Provisions status has Authorised.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServicesProvided_UITestMethod02()
        {
            #region Step 10

            Guid _serviceProvided1Id = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, _serviceElement3Id, _clientCategoryId, _currentRannkingId, null, 1, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

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
                .TypeSearchQuery(_serviceElement2IdName_2)
                .TapSearchButton()
                .SelectResultElement(_serviceElement2Id_2.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .InsertNotes("Automation Script to test Provided test.")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            var _serviceProvided2Id = dbHelper.serviceProvided.GetByProviderId_SeviceElement1_ServiceElement2(_providerId, _serviceElement1Id, _serviceElement2Id_2).FirstOrDefault();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ValidateRecordPresent(_serviceProvided1Id.ToString())
                .ValidateRecordPresent(_serviceProvided2Id.ToString());

            #endregion

            #region Step 11

            servicesProvidedPage
                .OpenServiceProvidedRecord(_serviceProvided2Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ValidateServiceFinanceSettingsTabVisibility(true)
                .ValidateRatePeriodsTabVisibility(true)
                .ValidateServiceDeliveryVariationsTabVisibility(true)
                .SelectApprovalStatus("Approved")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            #endregion

            #region Step 12

            #region Service Provision

            var rateUnit = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid, _serviceElement1Id, _serviceElement2Id, rateUnit,
                                    _serviceprovisionstartreasonid, _serviceprovisionendreasonid, _careDirectorQA_TeamId, _serviceProvided2Id, _providerId, _systemUserId, _placementRoomTypeId, new DateTime(2022, 6, 20), new DateTime(2022, 6, 26), null);

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, Authorised_Serviceprovisionstatusid);

            #endregion

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .OpenServiceProvidedRecord(_serviceProvided2Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ValidateUsedInFinanceOptionsDisabled(true)
                .ValidateUsedInFinanceOptionChecked(true)
                .ClickBackButton();

            #endregion

            #region Step 13

            #region Create Service Finance Settings for Service Provided First

            var paymentTypeCodeId = commonMethodsDB.CreatePaymentTypeCode("Default Payment", new DateTime(2023, 1, 1), _careDirectorQA_TeamId);
            var vatCodeId = commonMethodsDB.CreateVATCode("Default VAT", new DateTime(2023, 1, 1), _careDirectorQA_TeamId);
            var providerBatchGroupingId = commonMethodsDB.CreateProviderBatchGrouping("Default Batching", new DateTime(2023, 1, 1), _careDirectorQA_TeamId);

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, _serviceProvided1Id, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .OpenServiceProvidedRecord(_serviceProvided1Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(3000);

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .ValidateRecordNotPresent(_serviceProvided1Id.ToString());

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-402

        [TestProperty("JiraIssueID", "ACC-403")]
        [Description("Validate Rate Period tab should not be display." +
                    "Validate Service Delivery Variations tab should not be display after changing Contract Type to Block." +
                    "Validate error message while createing duplicate records.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServicesProvided_UITestMethod03()
        {
            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

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
                .TypeSearchQuery(_serviceElement2IdName_2)
                .TapSearchButton()
                .SelectResultElement(_serviceElement2Id_2.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectNegotiatedRatesApplyOption("Yes")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ValidateNegotiatedRatesApplyOptionChecked(true)
                .ValidateRatePeriodsTabVisibility(false)
                .ClickBackButton();

            #endregion

            #region Step 15 & 16

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
                .TypeSearchQuery(_serviceElement2IdName_2)
                .TapSearchButton()
                .SelectResultElement(_serviceElement2Id_2.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectContractType("Block")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ValidateServiceDeliveryVariationsTabVisibility(false)
                .ValidateNegotiatedRatesApplyOptionChecked(false)
                .ValidateNegotiatedRatesApplyOptionsDisabled(true)
                .ClickBackButton();

            #endregion

            #region Step 17 & 18

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
                .TypeSearchQuery(_serviceElement2IdName_2)
                .TapSearchButton()
                .SelectResultElement(_serviceElement2Id_2.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectNegotiatedRatesApplyOption("Yes")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("You are trying to create a record that is a duplicate i.e. where Provider, Service Element 1, Service Element 2, Service Element 3, Client Category, Negotiated Rates Apply and Contract Type are identical. Please correct as necessary.")
                .TapCloseButton();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-410

        [TestProperty("JiraIssueID", "ACC-411")]
        [Description("Verify create the new record if the existing record with Status = Unapproved." +
                    "Verify the Service provided record has Toggle inactive field where its default to No value" +
                    "Verify user able to inactivate the record by selecting 'Yes' value and save" +
                    "Verify user is able to reactivate the record using the toolbar Activate button.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServicesProvided_UITestMethod04()
        {
            #region Step 19 & 21

            dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, null, 3, false);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

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
                .ValidateInactiveStatus("No")
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
                .TypeSearchQuery(_serviceElement2IdName_2)
                .TapSearchButton()
                .SelectResultElement(_serviceElement2Id_2.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad();

            int recordCount = dbHelper.serviceProvided.GetByProviderId(_providerId).Count();
            Assert.AreEqual(2, recordCount);

            var _serviceProvidedId2 = dbHelper.serviceProvided.GetByProviderId(_providerId, 1).FirstOrDefault();

            #endregion

            #region Step 22

            servicesProvidedPage
                .ValidateRecordPresent(_serviceProvidedId2.ToString())
                .OpenServiceProvidedRecord(_serviceProvidedId2.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ValidateActiveButtonVisibility(false)
                .InactiveStatus("Yes")
                .ClickSaveButton()
                .WaitForRecordToBeSavedAsInactive()
                .ValidateInactiveStatus("Yes")
                .ValidateActiveButtonVisibility(true);

            #endregion

            #region Step 23

            serviceProvidedRecordPage
                .ClickActivateButton();

            alertPopup
               .WaitForAlertPopupToLoad()
               .ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.")
               .TapOKButton();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .ValidateActiveButtonVisibility(false)
                .ValidateInactiveStatus("No");

            #endregion

            // Step 24 & 26 already Covered in another script

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2673

        [TestProperty("JiraIssueID", "ACC-2678")]
        [Description("Deactivate the Service provided record and navigate to the service provision , " +
                     "Enter SE1 and SE2 record used in Service provided. Select the Service provided lookup " +
                     "Verify the inactive Service provided record is not displayed in the lookup")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServicesProvided_UITestMethod05()
        {
            #region Service Provided

            var _serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, null, 3, false);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, _serviceProvidedId, defaulRateUnitID, DateTime.Now.Date, DateTime.Now.Date, 1);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, _serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, DateTime.Now.Date, null, 0);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId, ratePeriodId1, _serviceProvidedId, 10m, 15m, null, null);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(_serviceProvidedId, 2);

            #endregion

            #region Step 25

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToServiceProvisionsSection();

            serviceProvisionsPage
                .WaitForServiceProvisionsPageToLoad()
                .ClickNewRecordButton();

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement1IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement1Id.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement2IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement2Id.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceProvidedLookupButton();

            providerCarerSearchPopup
                .WaitForProviderCarerSearchPopupToLoad()
                .ClickSearchButton()
                .ValidateServiceProviderPresentOrNot(_serviceProvidedId.ToString(), true);

            dbHelper.serviceProvided.UpdateInactiveStatus(_serviceProvidedId, true);

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceProvidedLookupButton();

            providerCarerSearchPopup
                .WaitForProviderCarerSearchPopupToLoad()
                .ClickSearchButton()
                .ValidateServiceProviderPresentOrNot(_serviceProvidedId.ToString(), false);

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
