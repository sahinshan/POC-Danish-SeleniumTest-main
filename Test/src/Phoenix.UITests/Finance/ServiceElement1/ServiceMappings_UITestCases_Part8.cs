using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceMappings
{
    [TestClass]
    public class ServiceMappings_UITestCases_Part8 : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _startReasonId;


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

                #region System User ServiceMappingsUser1

                commonMethodsDB.CreateSystemUserRecord("ServiceMappingsUser1", "ServiceMappings", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                #endregion                
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22792

        [TestProperty("JiraIssueID", "CDV6-22982")]
        [Description("Steps 23 to 26 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceMappings_UITestMethod008()
        {

            #region Service Element 1
            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "CDV6_22982_Provider_" + partialDateTimeSuffix;
            var serviceElement1Name_Carer = "CDV6_22982_Carer_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var whotopayid_Carer = 2; // Carer
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _careDirectorQA_TeamId, startDate, code1, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID);
            var serviceElement1Id_Carer = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Carer, _careDirectorQA_TeamId, startDate, code2, whotopayid_Carer, paymentscommenceid_Actual, null);
            #endregion

            #region Service Element 2

            var serviceElement2NameA = "CDV6_22982_SE2_A_" + partialDateTimeSuffix;
            var serviceElement2Id_A = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, serviceElement2NameA, startDate, code1);

            var serviceElement2NameB = "CDV6_22982_SE2_B_" + partialDateTimeSuffix;
            code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id_B = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, serviceElement2NameB, startDate, code1);

            #endregion

            #region Care Type

            var careTypeNameA = "CDV6_22982_CareTypeA" + partialDateTimeSuffix;
            var careTypeNameB = "CDV6_22982_CareTypeB" + partialDateTimeSuffix;
            var _careTypeIdA = dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, careTypeNameA, code1, startDate);
            var _careTypeIdB = dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, careTypeNameB, code2, startDate);
            #endregion

            #region Service Mapping

            var serviceMappingA = commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id, serviceElement2Id_A);
            var serviceMappingB = commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id, serviceElement2Id_B);
            var serviceMappingA_Carer = commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id_Carer, _careTypeIdA, false);
            var serviceMappingB_Carer = commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, serviceElement1Id_Carer, _careTypeIdB, false);

            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_22982").Any())
                _startReasonId = dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "StartReason_22982", new DateTime(2022, 1, 1));
            _startReasonId = dbHelper.serviceProvisionStartReason.GetByName("StartReason_22982")[0];

            #endregion

            #region Provider - Supplier
            string providerName = "CDV6-22982" + partialDateTimeSuffix;
            Guid provider_Supplier = commonMethodsDB.CreateProvider(providerName, _careDirectorQA_TeamId, 2, providerName + "@test.com");
            #endregion

            #region Provider - Carer
            string providerName_Carer = "CDV6-22982-Carer" + partialDateTimeSuffix;
            Guid provider_Carer = commonMethodsDB.CreateProvider(providerName_Carer, _careDirectorQA_TeamId, 7, providerName_Carer + "@test.com");

            #endregion

            #region Person
            var personId = commonMethodsDB.CreatePersonRecord("John", "Doe" + partialDateTimeSuffix, _ethnicityId, _careDirectorQA_TeamId);

            #endregion

            #region Step 23

            loginPage
                .GoToLoginPage()
                .Login("ServiceMappingsUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerName)
                .OpenProviderRecord(provider_Supplier.ToString());

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
                .TypeSearchQuery(serviceElement1Name_Provider)
                .TapSearchButton()
                .SelectResultElement(serviceElement1Id.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad(true)
                .SelectServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement2NameA)
                .TapSearchButton()
                .ValidateResultElementPresent(serviceElement2Id_A.ToString());

            lookupPopup
                .TypeSearchQuery(serviceElement2NameB)
                .TapSearchButton()
                .ValidateResultElementPresent(serviceElement2Id_B.ToString());

            dbHelper.serviceMapping.DeactivateRecord(serviceMappingB);

            lookupPopup
                .TypeSearchQuery(serviceElement2NameB)
                .TapSearchButton()
                .ValidateResultElementNotPresent(serviceElement2Id_B.ToString());

            lookupPopup
                .TypeSearchQuery(serviceElement2NameA)
                .TapSearchButton()
                .ValidateResultElementPresent(serviceElement2Id_A.ToString());

            #endregion

            #region Step 24
            lookupPopup
                .ClickCloseButton();

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerName_Carer)
                .OpenProviderRecord(provider_Carer.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapApprovedCareTypesTab();

            approvedCareTypesPage
                .WaitForApprovedCareTypesPageToLoad()
                .ClickNewRecordButton();

            approvedCareTypeRecordPage
                .WaitForApprovedCareTypeRecordPageToLoad(true)
                .SelectServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement1Name_Carer)
                .TapSearchButton()
                .SelectResultElement(serviceElement1Id_Carer.ToString());

            approvedCareTypeRecordPage
                .WaitForApprovedCareTypeRecordPageToLoad(true)
                .SelectCareTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careTypeNameA)
                .TapSearchButton()
                .ValidateResultElementPresent(_careTypeIdA.ToString());

            lookupPopup
                .TypeSearchQuery(careTypeNameB)
                .TapSearchButton()
                .ValidateResultElementPresent(_careTypeIdB.ToString());

            dbHelper.serviceMapping.DeactivateRecord(serviceMappingA_Carer);

            lookupPopup
                .TypeSearchQuery(careTypeNameA)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_careTypeIdA.ToString());

            lookupPopup
                .TypeSearchQuery(careTypeNameB)
                .TapSearchButton()
                .ValidateResultElementPresent(_careTypeIdB.ToString());

            #endregion

            #region Step 25
            //For SE1 in which Who to Pay is Provider            
            lookupPopup
                .ClickCloseButton();

            approvedCareTypeRecordPage
                .WaitForApprovedCareTypeRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName("John", "Doe" + partialDateTimeSuffix)
                .OpenPersonRecord(personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapNewButton();

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement1Name_Provider)
                .TapSearchButton()
                .SelectResultElement(serviceElement1Id.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement2NameB)
                .TapSearchButton()
                .ValidateResultElementNotPresent(serviceElement2Id_B.ToString());

            lookupPopup
                .TypeSearchQuery(serviceElement2NameA)
                .TapSearchButton()
                .ValidateResultElementPresent(serviceElement2Id_A.ToString())
                .SelectResultElement(serviceElement2Id_A.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickStartReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("StartReason_22982")
                .TapSearchButton()
                .SelectResultElement(_startReasonId.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .InsertPlannedStartDate(commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ValidateServiceElement1FieldLinkText(serviceElement1Name_Provider)
                .ValidateServiceElement2FieldLinkText(serviceElement2NameA)
                .ClickBackButton();

            //For SE1 in which Who to Pay is Carer
            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapNewButton();

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement1Name_Carer)
                .TapSearchButton()
                .SelectResultElement(serviceElement1Id_Carer.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickCareTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careTypeNameA)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_careTypeIdA.ToString());

            lookupPopup
                .TypeSearchQuery(careTypeNameB)
                .TapSearchButton()
                .ValidateResultElementPresent(_careTypeIdB.ToString())
                .SelectResultElement(_careTypeIdB.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickStartReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("StartReason_22982")
                .TapSearchButton()
                .SelectResultElement(_startReasonId.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .InsertPlannedStartDate(commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ValidateServiceElement1FieldLinkText(serviceElement1Name_Carer)
                .ValidateCareTypeFieldLinkText(careTypeNameB)
                .ClickBackButton();

            var serviceProvision_Provider = dbHelper.serviceProvision.GetServiceProvisionByServiceElement1AndServiceElement2(personId, serviceElement1Id, serviceElement2Id_A).FirstOrDefault();
            var serviceProvision_Carer = dbHelper.serviceProvision.GetServiceProvisionByServiceElement1AndCareType(personId, serviceElement1Id_Carer, _careTypeIdB).FirstOrDefault();

            #endregion

            #region Step 26
            dbHelper.serviceMapping.DeactivateRecord(serviceMappingA);
            dbHelper.serviceMapping.DeactivateRecord(serviceMappingB_Carer);
            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvision_Provider.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ValidateServiceElement1FieldLinkText(serviceElement1Name_Provider)
                .ValidateServiceElement2FieldLinkText(serviceElement2NameA)
                .ClickBackButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvision_Carer.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ValidateServiceElement1FieldLinkText(serviceElement1Name_Carer)
                .ValidateCareTypeFieldLinkText(careTypeNameB);

            #endregion
        }

        #endregion
    }
}
