using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Phoenix.UITests.Providers
{
    [TestClass]
    public class ServiceProvision_UITestCases3 : FunctionalTest
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
        private Guid _serviceElement1Id2;
        private Guid _serviceElement2Id2;
        private string _serviceElement1IdName;
        private string _serviceElement2IdName;
        private Guid _providerId;
        private int _providerNumber;
        private string _providerName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _clientCategoryId;
        private string _clientCategoryName;
        private Guid _currentRannkingId;
        private string _currentRannkingName;
        private Guid Draft_Serviceprovisionstatusid;
        private Guid ReadyForAuthorisation_Serviceprovisionstatusid;
        private Guid Authorised_Serviceprovisionstatusid;
        private Guid _serviceprovisionstartreasonid;
        private Guid _serviceprovisionendreasonid;
        private Guid _placementRoomTypeId;
        private string _firstName;
        private string _lastName;
        private string _personNumber;
        private Guid serviceProvidedId;
        private Guid _perWeekProRata_RateUnit;


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

                _systemUserName = "SP3User1_" + _currentDateSuffix;
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "SP3", "User1_" + _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Person

                _firstName = "John";
                _lastName = "LN_" + _currentDateSuffix;
                _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2003, 1, 2));
                _personNumber = dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"].ToString();

                #endregion                

                #region Client Category
                var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
                _clientCategoryName = "Default Category";
                _clientCategoryId = commonMethodsDB.CreateFinanceClientCategory(_careDirectorQA_TeamId, _clientCategoryName, new DateTime(2022, 1, 1), code.ToString());

                #endregion

                #region Current Ranking

                _currentRannkingName = "Outstanding";
                _currentRannkingId = dbHelper.currentRanking.GetCurrentRankingByName(_currentRannkingName).FirstOrDefault();

                #endregion

                #region Service Provision Status

                Draft_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
                ReadyForAuthorisation_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];
                Authorised_Serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

                #endregion

                #region Service Provision Start Reason

                _serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Default", new DateTime(2020, 1, 1));

                #endregion

                #region Service Provision End Reason

                _serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_careDirectorQA_TeamId, "Default", new DateTime(2020, 1, 1));

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

        #region https://advancedcsg.atlassian.net/browse/ACC-1314

        [TestProperty("JiraIssueID", "ACC-1351")]
        [Description("Test automation for Step 1 to 4 from CDV6-3123: To verify creation of Service Provision record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_CDV6_3123_UITestMethod01()
        {

            #region Provider - Supplier

            _providerName = "Provider_" + _currentDateSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 2, _providerName + "@test.com");
            _providerNumber = (int)dbHelper.provider.GetProviderByID(_providerId, "providernumber")["providernumber"];

            #endregion

            #region Service Element 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE1_3123" + _currentDateSuffix;
            _serviceElement2IdName = "SE2_3123" + _currentDateSuffix;

            var defaulRateUnitID1 = dbHelper.rateUnit.GetByName("Per Week Pro Rata \\ Weekly").FirstOrDefault();
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID1);

            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, 1, 1, validRateUnits, defaulRateUnitID1);
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2022, 1, 1), code);

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region Service Provided
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);
            string serviceProvidedName = dbHelper.serviceProvided.GetByID(serviceProvidedId, "name")["name"].ToString();
            serviceProvidedName = Regex.Replace(serviceProvidedName, @"\s+", " ");
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, defaulRateUnitID1, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 1);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId1, serviceProvidedId, 10m, null, null, null);
            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(serviceProvidedId, 2);


            #endregion

            #region Step 1 to 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapNewButton();

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ValidateServiceProvisionStatusLinkText("Draft")
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup Records")
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
                .InsertActualStartDate(commonMethodsHelper.GetCurrentDate())
                .ClickStartReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default")
                .TapSearchButton()
                .SelectResultElement(_serviceprovisionstartreasonid.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceProvidedLookupButton();


            providerCarerSearchPopup
                .WaitForProviderCarerSearchPopupToLoad()
                .ClickSearchButton()
                .SelectResultElement(serviceProvidedId.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForServiceProvisionRecordPageToLoad();

            System.Threading.Thread.Sleep(1500);

            var _serviceProvisionId = dbHelper.serviceProvision.GetServiceProvisionByPersonID(_personID)[0];
            string _serviceProvisionTitle = (string)dbHelper.serviceProvision.GetServiceProvisionById(_serviceProvisionId, "name")["name"];

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ValidateServiceElement1FieldLinkText(_serviceElement1IdName)
                .ValidateServiceElement2FieldLinkText(_serviceElement2IdName)
                .ValidateActualStartDateFieldText(commonMethodsHelper.GetCurrentDate())
                .ValidateStartReasonFieldLinkText("Default")
                .ValidateServiceProvidedFieldLinkText(serviceProvidedName)
                .ValidateServiceProvisionPageHeader(_serviceProvisionTitle);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1315

        [TestProperty("JiraIssueID", "ACC-1360")]
        [Description("Test automation for Step 5 to 7 from CDV6-3123: To verify creation of Service Provision record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_CDV6_3123_UITestMethod02()
        {

            #region Provider - Supplier

            _providerName = "Provider_" + _currentDateSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 2, _providerName + "@test.com");
            _providerNumber = (int)dbHelper.provider.GetProviderByID(_providerId, "providernumber")["providernumber"];

            #endregion

            #region Service Element 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE1_3123" + _currentDateSuffix;
            _serviceElement2IdName = "SE2_3123" + _currentDateSuffix;

            var defaulRateUnitID1 = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID1);

            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, 1, 1, validRateUnits, defaulRateUnitID1);
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2022, 1, 1), code);

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region Service Provided
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, defaulRateUnitID1, new DateTime(2023, 1, 1));

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId1, serviceProvidedId, 10m, 15m);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(serviceProvidedId, 2);

            var plannedStartDate = commonMethodsHelper.GetThisWeekFirstMonday();
            var plannedEndDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(1);
            var actualStartDate = commonMethodsHelper.GetThisWeekFirstMonday();
            var actualEndDate = actualStartDate.AddDays(2);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[0], _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate, null, plannedEndDate, null, null);

            #endregion

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirectorQA_TeamId, _systemUserId, new DateTime(2020, 1, 1), 4, 9999m, true, true);

            #endregion            

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Step 5 to 7

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ValidateServiceProvisionStatusLinkText("Draft")
                .ValidatePlannedEndDateFieldValue(plannedEndDate.ToString("dd'/'MM'/'yyyy"));

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .InsertPlannedEndDate(actualStartDate.ToString("dd'/'MM'/'yyyy"))
                .TapEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default")
                .TapSearchButton()
                .SelectResultElement(_serviceprovisionendreasonid.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidatePlannedEndDateFieldValue(actualStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePlannedEndDateFieldDisabled(false)
                .ValidatePlannedStartDateFieldDisabled(false)
                .InsertPlannedEndDate("")
                .ClickSaveButton()
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidatePlannedEndDateFieldValue("")
                .InsertActualStartDate(actualStartDate.ToString("dd'/'MM'/'yyyy"))
                .InsertActualEndDate(actualStartDate.ToString("dd'/'MM'/'yyyy"))
                .TapEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default")
                .TapSearchButton()
                .SelectResultElement(_serviceprovisionendreasonid.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickSaveButton();

            System.Threading.Thread.Sleep(2000);

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidateActualStartDateFieldText(actualStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateActualEndDateFieldText(actualStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePlannedEndDateFieldDisabled(true)
                .ValidatePlannedStartDateFieldDisabled(true)
                .ClickStatusLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Ready for Authorisation")
                .TapSearchButton()
                .SelectResultElement(ReadyForAuthorisation_Serviceprovisionstatusid.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForServiceProvisionRecordPageToLoad();

            serviceProvisionRecordPage
                .ValidateServiceProvisionStatusLinkText("Ready for Authorisation")
                .ValidatePlannedEndDateFieldDisabled(true)
                .ValidatePlannedStartDateFieldDisabled(true)
                .ValidateActualEndDateFieldDisabled(true)
                .ValidateActualStartDateFieldDisabled(true);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1316

        [TestProperty("JiraIssueID", "ACC-1380")]
        [Description("Test automation for Step 8 to 11 from CDV6-3123: To verify creation of Service Provision record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_CDV6_3123_UITestMethod03()
        {

            #region Provider - Supplier

            _providerName = "Provider_" + _currentDateSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 2, _providerName + "@test.com");
            _providerNumber = (int)dbHelper.provider.GetProviderByID(_providerId, "providernumber")["providernumber"];

            #endregion

            #region Service Element 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE1_3123" + _currentDateSuffix;
            _serviceElement2IdName = "SE2_3123" + _currentDateSuffix;

            var defaulRateUnitID1 = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID1);

            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, 1, 1, validRateUnits, defaulRateUnitID1);
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2022, 1, 1), code);

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region Service Provided
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, defaulRateUnitID1, new DateTime(2023, 1, 1));

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId1, serviceProvidedId, 10m, 15m);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(serviceProvidedId, 2);

            var plannedStartDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6).Date;
            var plannedEndDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(1);
            var actualStartDate = commonMethodsHelper.GetThisWeekFirstMonday();
            DateTime mondayOfLastWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6).Date;
            var actualEndDate = actualStartDate.AddDays(2);

            #endregion

            #region Authorisation Level

            dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirectorQA_TeamId, _systemUserId, new DateTime(2020, 1, 1), 4, 999999m, true, true);

            #endregion

            #region Service Provision

            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID,
                Draft_Serviceprovisionstatusid, _serviceElement1Id, _serviceElement2Id,
                null, null, validRateUnits[0],
                _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId, _providerId, _systemUserId,
                _placementRoomTypeId, plannedStartDate, null, plannedEndDate, null, null);

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Service Provision - Ready for Authorisation
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, ReadyForAuthorisation_Serviceprovisionstatusid);

            #endregion

            #region Step 8 to 11

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .SelectRecord(serviceProvisionID.ToString())
                .TapAuthoriseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 1\r\nUpdated records: 1\r\n")
                .TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ValidateServiceProvisionStatusLinkText("Authorised")
                .ValidatePlannedStartDateFieldDisabled(true)
                .ValidatePlannedEndDateFieldValue(plannedEndDate.ToString("dd'/'MM'/'yyyy"));

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .InsertPlannedEndDate(mondayOfLastWeek.AddDays(5).ToString("dd'/'MM'/'yyyy"))
                .TapEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default")
                .TapSearchButton()
                .SelectResultElement(_serviceprovisionendreasonid.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidatePlannedEndDateFieldValue(mondayOfLastWeek.AddDays(5).ToString("dd'/'MM'/'yyyy"))
                .ValidatePlannedEndDateFieldDisabled(false)
                .ValidatePlannedStartDateFieldDisabled(true)
                .InsertPlannedEndDate("")
                .ClickSaveButton()
                .WaitForServiceProvisionRecordPageToLoad();

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidatePlannedEndDateFieldValue("")
                .InsertActualStartDate(mondayOfLastWeek.ToString("dd'/'MM'/'yyyy"))
                .InsertActualEndDate(actualStartDate.ToString("dd'/'MM'/'yyyy"))
                .TapEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default")
                .TapSearchButton()
                .SelectResultElement(_serviceprovisionendreasonid.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickSaveButton();

            System.Threading.Thread.Sleep(2000);

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidateActualStartDateFieldText(mondayOfLastWeek.ToString("dd'/'MM'/'yyyy"))
                .ValidateActualEndDateFieldText(actualStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePlannedStartDateFieldDisabled(true)
                .ValidatePlannedEndDateFieldDisabled(true)
                .ValidateActualStartDateFieldDisabled(true)
                .ValidateActualEndDateFieldDisabled(false);

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClearActualEndDate();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The actual end date cannot be empty.")
                .TapOKButton();

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickActualEndDate_DatePicker();

            calendarDatePicker
                .WaitForCalendarPickerPopupToLoad()
                .SelectCalendarDate(actualStartDate.AddDays(-1));

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .TapEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default")
                .TapSearchButton()
                .SelectResultElement(_serviceprovisionendreasonid.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForServiceProvisionRecordPageToLoad();

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidateActualEndDateFieldText(actualStartDate.AddDays(-1).ToString("dd'/'MM'/'yyyy"));

            serviceProvisionRecordPage
                .ClickActualEndDate_DatePicker();

            calendarDatePicker
                .WaitForCalendarPickerPopupToLoad()
                .SelectCalendarDate(actualEndDate.AddDays(2));

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The actual end date cannot be a future date.")
                .TapOKButton();

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .SelectRecord(serviceProvisionID.ToString())
                .TapCancelButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Are you sure you wish to cancel the Selected Service Provisions?")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 1\r\nUpdated records: 1\r\n")
                .TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidateServiceProvisionStatusLinkText("Cancelled");
            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1317

        [TestProperty("JiraIssueID", "ACC-1402")]
        [Description("Test automation for Step 12 to 19 from CDV6-3123: To verify creation of Service Provision record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_CDV6_3123_UITestMethod04()
        {
            var _systemUserName2 = "SP3User2_" + _currentDateSuffix;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_systemUserName2, "SP3", "User2_" + _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #region Service Element 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE1_3123_" + _currentDateSuffix;
            _serviceElement2IdName = "SE2_3123_" + _currentDateSuffix;

            var defaulRateUnitID1 = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            _perWeekProRata_RateUnit = dbHelper.rateUnit.GetByName("Per Week Pro Rata \\ Weekly")[0];
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID1);
            validRateUnits.Add(_perWeekProRata_RateUnit);

            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, 3, 1, validRateUnits, defaulRateUnitID1);
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2022, 1, 1), code);

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirectorQA_TeamId, _systemUserId, new DateTime(2020, 1, 1), 4, 9999m, true, true);

            authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId2, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirectorQA_TeamId, _systemUserId2, new DateTime(2020, 1, 1), 4, 1m, true, true);

            #endregion

            #region Service Provision
            var actualStartDate = commonMethodsHelper.GetThisWeekFirstMonday();
            var actualEndDate = actualStartDate.AddDays(3);

            var withoutServiceDelivery_serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvisionWithoutRateRequired(_careDirectorQA_TeamId, _systemUserId, _personID,
                Draft_Serviceprovisionstatusid, _serviceElement1Id, _serviceElement2Id,
                null, null, validRateUnits[0],
                _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, null, actualStartDate, null, null, serviceProvidedId, _providerId, _systemUserId,
                _placementRoomTypeId, null);

            var serviceDeliveryIsNo_serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvisionWithoutRateRequired(_careDirectorQA_TeamId, _systemUserId, _personID,
                Draft_Serviceprovisionstatusid, _serviceElement1Id, _serviceElement2Id,
                null, null, validRateUnits[1],
                _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, null, actualStartDate, null, actualEndDate, serviceProvidedId, _providerId, _systemUserId,
                _placementRoomTypeId, null);

            var _serviceProvision1Name = (string)dbHelper.serviceProvision.GetServiceProvisionById(withoutServiceDelivery_serviceProvisionID, "name")["name"];
            var _serviceProvision2Name = (string)dbHelper.serviceProvision.GetServiceProvisionById(serviceDeliveryIsNo_serviceProvisionID, "name")["name"];

            #endregion

            #region Service Provision Rate Period and Rate Schedule
            var serviceProvisionRatePeriodId1 = dbHelper.serviceProvisionRatePeriod.CreateServiceProvisionRatePeriod(_careDirectorQA_TeamId, _personID, _serviceProvision1Name, withoutServiceDelivery_serviceProvisionID, defaulRateUnitID1,
                new DateTime(2023, 1, 1), null);
            dbHelper.serviceProvisionRateSchedule.CreateServiceProvisionRateSchedule(_careDirectorQA_TeamId, _personID, withoutServiceDelivery_serviceProvisionID, serviceProvisionRatePeriodId1,
                10m, 10m);

            dbHelper.serviceProvisionRatePeriod.UpdateStatus(serviceProvisionRatePeriodId1, 2);

            var serviceProvisionRatePeriodId2 = dbHelper.serviceProvisionRatePeriod.CreateServiceProvisionRatePeriod(_careDirectorQA_TeamId, _personID, _serviceProvision2Name, serviceDeliveryIsNo_serviceProvisionID, validRateUnits[1],
                new DateTime(2023, 1, 1), null);
            dbHelper.serviceProvisionRateSchedule.CreateServiceProvisionRateSchedule(_careDirectorQA_TeamId, _personID, serviceDeliveryIsNo_serviceProvisionID, serviceProvisionRatePeriodId2,
                10m, null);

            dbHelper.serviceProvisionRatePeriod.UpdateStatus(serviceProvisionRatePeriodId2, 2);
            #endregion

            #region Step 12, 14 to 17

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .ValidateRecordIsPresent(serviceDeliveryIsNo_serviceProvisionID.ToString(), true)
                .OpenRecord(serviceDeliveryIsNo_serviceProvisionID.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidateServiceDeliveriesTabIsDisplayed(false)
                .ClickBackButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .ValidateRecordIsPresent(withoutServiceDelivery_serviceProvisionID.ToString(), true)
                .OpenRecord(withoutServiceDelivery_serviceProvisionID.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidateServiceDeliveriesTabIsDisplayed(true)
                .ClickStatusLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Ready for Authorisation")
                .TapSearchButton()
                .SelectResultElement(ReadyForAuthorisation_Serviceprovisionstatusid.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("At least one Service Delivery record must be recorded before the Service Provision's status can be set to Ready for Authorisation.")
                .TapCloseButton();

            #region Service Delivery - Add a Service Delivery record to Service Provision which does not have any associated Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, withoutServiceDelivery_serviceProvisionID, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForServiceProvisionRecordPageToLoad();

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidateServiceProvisionStatusLinkText("Ready for Authorisation")
                .ClickBackButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .SelectRecord(withoutServiceDelivery_serviceProvisionID.ToString())
                .TapAuthoriseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 1\r\nUpdated records: 1\r\n")
                .TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(withoutServiceDelivery_serviceProvisionID.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ValidateServiceProvisionStatusLinkText("Authorised");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickSignOutButton();

            #endregion

            #region  Step 19

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName2, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            #region Service Provision - Ready for Authorisation
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceDeliveryIsNo_serviceProvisionID, ReadyForAuthorisation_Serviceprovisionstatusid);

            #endregion

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .ValidateRecordIsPresent(serviceDeliveryIsNo_serviceProvisionID.ToString(), true)
                .SelectRecord(serviceDeliveryIsNo_serviceProvisionID.ToString())
                .TapAuthoriseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 1\r\nUpdated records: 0\r\n")
                .TapOKButton();
            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1437")]
        [Description("Test automation for Step 13, 18 from CDV6-3123: To verify creation of Service Provision record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_CDV6_3123_UITestMethod05()
        {
            #region Service Element 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE1_3123_" + _currentDateSuffix;
            _serviceElement2IdName = "SE2_3123_" + _currentDateSuffix;

            var defaulRateUnitID1 = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            _perWeekProRata_RateUnit = dbHelper.rateUnit.GetByName("Per Week Pro Rata \\ Weekly")[0];
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID1);
            validRateUnits.Add(_perWeekProRata_RateUnit);

            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, 3, 1, validRateUnits, defaulRateUnitID1);
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2022, 1, 1), code);

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirectorQA_TeamId, _systemUserId, new DateTime(2020, 1, 1), 4, 9999m, true, true);

            #endregion

            var actualStartDate = commonMethodsHelper.GetThisWeekFirstMonday();

            #region Step 13, Step 18

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

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
                .InsertActualStartDate(actualStartDate.ToString("dd'/'MM'/'yyyy"))
                .ClickStartReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default")
                .TapSearchButton()
                .SelectResultElement(_serviceprovisionstartreasonid.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForServiceProvisionRecordPageToLoad();

            System.Threading.Thread.Sleep(1500);

            var _serviceProvisionId = dbHelper.serviceProvision.GetServiceProvisionByPersonID(_personID)[0];
            string _serviceProvisionTitle = (string)dbHelper.serviceProvision.GetServiceProvisionById(_serviceProvisionId, "name")["name"];

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidateServiceElement1FieldLinkText(_serviceElement1IdName)
                .ValidateServiceElement2FieldLinkText(_serviceElement2IdName)
                .ValidateRateRequiredYesSelected()
                .ValidateActualStartDateFieldText(actualStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartReasonFieldLinkText("Default")
                .ValidateServiceProvisionPageHeader(_serviceProvisionTitle);
            #endregion

            #region Service Provision Rate Period and Rate Schedule

            var serviceProvisionRatePeriodId1 = dbHelper.serviceProvisionRatePeriod.CreateServiceProvisionRatePeriod(_careDirectorQA_TeamId, _personID, _serviceProvisionTitle, _serviceProvisionId, defaulRateUnitID1,
                new DateTime(2023, 1, 1), null);
            dbHelper.serviceProvisionRateSchedule.CreateServiceProvisionRateSchedule(_careDirectorQA_TeamId, _personID, _serviceProvisionId, serviceProvisionRatePeriodId1,
                10m, 10m);

            dbHelper.serviceProvisionRatePeriod.UpdateStatus(serviceProvisionRatePeriodId1, 2);

            #endregion

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickBackButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapRefreshButton()
                .SelectRecord(_serviceProvisionId.ToString())
                .TapCancelButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Are you sure you wish to cancel the Selected Service Provisions?")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 1\r\nUpdated records: 0\r\n")
                .TapOKButton();

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, _serviceProvisionId, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(1, dbHelper.serviceDelivery.GetByServiceProvisionID(_serviceProvisionId).Count);

            #endregion

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapRefreshButton()
                .SelectRecord(_serviceProvisionId.ToString())
                .TapCancelButton();
            System.Threading.Thread.Sleep(1000);
            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Are you sure you wish to cancel the Selected Service Provisions?")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 1\r\nUpdated records: 0\r\n")
                .TapOKButton();
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1319

        [TestProperty("JiraIssueID", "ACC-1599")]
        [Description("Test automation for Step 20 - 26 from CDV6-3123: To verify creation of Service Provision record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_CDV6_3123_UITestMethod06()
        {
            #region Provider - Carer

            _providerName = "Provider_" + _currentDateSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 7, _providerName + "@test.com");
            _providerNumber = (int)dbHelper.provider.GetProviderByID(_providerId, "providernumber")["providernumber"];

            #endregion

            #region Service Element 1 & Care Type

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Carer = 2; // Carer            
            var paymentscommenceid_Actual = 1; //Actual 
            _serviceElement1IdName = "SE1_3123_" + _currentDateSuffix; //Who to Pay = Carer            
            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, whotopayid_Carer, paymentscommenceid_Actual, null, false);

            var careTypeNameA = "CT1_3123" + _currentDateSuffix;
            var _careTypeIdA = dbHelper.careType.CreateCareType(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, careTypeNameA, code, new DateTime(2022, 1, 1));

            #endregion

            #region Service Mapping

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _careTypeIdA, false);

            #endregion            

            #region Decision

            var decisionId = dbHelper.decision.GetByName("Approved")[0];

            #endregion

            dbHelper.carerApprovalDecision.CreateCarerApprovalDecision(_providerId, _careDirectorQA_TeamId, new DateTime(2020, 1, 1), decisionId);
            var approvedCareTypeId = dbHelper.approvedCareType.CreateApprovedCareType(_providerId, 2, _serviceElement1Id, _careTypeIdA, true, true, 999, new DateTime(2020, 1, 1), 1, 100, 1, 100, false, _careDirectorQA_TeamId);
            string approvedCareTypeTitle = (string)dbHelper.approvedCareType.GetApprovedCareTypeByID(approvedCareTypeId, "name")["name"];

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirectorQA_TeamId, _systemUserId, new DateTime(2020, 1, 1), 4, 999999m, true, true);

            #endregion

            DateTime actualStartDate2 = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6).Date;

            #region Step 20 - Step 26

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

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
                .TypeSearchQuery(_serviceElement1IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement1Id.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickCareTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careTypeNameA)
                .TapSearchButton()
                .SelectResultElement(_careTypeIdA.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .InsertPlannedStartDate(actualStartDate2.ToString("dd'/'MM'/'yyyy"))
                .ClickStartReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default")
                .TapSearchButton()
                .SelectResultElement(_serviceprovisionstartreasonid.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .InsertPlannedEndDate(actualStartDate2.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .TapEndReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default")
                .TapSearchButton()
                .SelectResultElement(_serviceprovisionendreasonid.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickPurchasingteamLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CareDirector QA")
                .TapSearchButton()
                .SelectResultElement(_careDirectorQA_TeamId.ToString());

            serviceProvisionRecordPage
                .ClickApprovedCareTypeLookupButton();

            providerCarerSearchPopup
                .WaitForProviderCarerSearchPopupToLoad()
                .ClickSearchButton()
                .SelectResultElement(approvedCareTypeId.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForServiceProvisionRecordPageToLoad();

            System.Threading.Thread.Sleep(1500);

            var _serviceProvisionId = dbHelper.serviceProvision.GetServiceProvisionByPersonID(_personID);
            Assert.AreEqual(1, _serviceProvisionId.Count);
            string _serviceProvisionTitle = (string)dbHelper.serviceProvision.GetServiceProvisionById(_serviceProvisionId[0], "name")["name"];

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidateServiceProvisionStatusLinkText("Draft")
                .ValidateServiceElement1FieldLinkText(_serviceElement1IdName)
                .ValidateCareTypeFieldLinkText(careTypeNameA)
                .ValidateApprovedCareTypeFieldLinkText(approvedCareTypeTitle)
                .ValidateProviderCarerFieldLinkText(_providerName)
                .ValidatePlannedStartDateValue(actualStartDate2.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartReasonFieldLinkText("Default")
                .ValidateEndReasonFieldLinkText("Default")
                .ValidatePurchasingTeamFieldLinkText("CareDirector QA")
                .ValidateServiceProvisionPageHeader(_serviceProvisionTitle);

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidateAllowancesTabIsVisible(true);

            bool usedInFinanceFieldValue = (bool)dbHelper.serviceElement1.GetByID(_serviceElement1Id, "usedinfinance")["usedinfinance"];
            Assert.IsFalse(usedInFinanceFieldValue);

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId[0], ReadyForAuthorisation_Serviceprovisionstatusid);
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(_serviceProvisionId[0], Authorised_Serviceprovisionstatusid);

            usedInFinanceFieldValue = (bool)dbHelper.serviceElement1.GetByID(_serviceElement1Id, "usedinfinance")["usedinfinance"];
            Assert.IsTrue(usedInFinanceFieldValue);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1318

        [TestProperty("JiraIssueID", "ACC-1642")]
        [Description("Test automation for Step 27 to 35 from CDV6-3123: To verify creation of Service Provision record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_CDV6_3123_UITestMethod07()
        {

            #region Provider - Supplier

            _providerName = "Provider_" + _currentDateSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 2, _providerName + "@test.com");
            _providerNumber = (int)dbHelper.provider.GetProviderByID(_providerId, "providernumber")["providernumber"];

            #endregion

            #region Service Element 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE1_3123" + _currentDateSuffix;
            _serviceElement2IdName = "SE2_3123" + _currentDateSuffix;

            var defaulRateUnitID1 = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID1);
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());

            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, 1, 1, validRateUnits, validRateUnits[1], null, false);
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2022, 1, 1), code);

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region Service Provided
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2, false);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, validRateUnits[1], new DateTime(2023, 1, 1));

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId1, serviceProvidedId, 10m, 10m, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), true, true, true, true, true, true, true, true);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId1, serviceProvidedId, 20m, 20m, new TimeSpan(8, 0, 0), new TimeSpan(20, 0, 0), true, true, true, true, true, true, true, true);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId1, serviceProvidedId, 30m, 30m, new TimeSpan(20, 0, 0), new TimeSpan(21, 30, 0), true, true, true, true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(serviceProvidedId, 2);

            var plannedStartDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6).Date;
            var plannedEndDate = plannedStartDate.AddDays(1);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[1], _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate, null, plannedEndDate, null, null);

            var serviceProvisionID2 = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[1], _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate, null, plannedEndDate, null, null);

            var serviceProvisionID3 = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[1], _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate, null, plannedEndDate, null, null);

            #endregion

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirectorQA_TeamId, _systemUserId, new DateTime(2020, 1, 1), 4, 999999m, true, true);

            #endregion            

            #region Step 27 to 35

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToServiceDeliveriesTab();


            serviceDeliveriesPage
                .WaitForServiceDeliveriesPageToLoad()
                .ClickNewRecordButton();

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .InsertTextOnPlannedStartTime("12:00")
                .InsertTextOnUnits("1")
                .InsertTextOnNumberOfCarers("1")
                .ClickSelectAll_YesRadioButton()
                .ClickSaveButton()
                .WaitForServiceDeliveryRecordPageToLoad()
                .ClickBackButton();

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickBackButton();

            var serviceDelivery = dbHelper.serviceDelivery.GetByServiceProvisionID(serviceProvisionID);
            Assert.AreEqual(1, serviceDelivery.Count);

            bool usedInFinanceFieldValue = (bool)dbHelper.serviceProvided.GetByID(serviceProvidedId, "usedinfinance")["usedinfinance"];
            Assert.IsFalse(usedInFinanceFieldValue);


            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .SelectRecord(serviceProvisionID.ToString())
                .TapReadyToAuthoriseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 1\r\nUpdated records: 1\r\n")
                .TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapRefreshButton()
                .SelectRecord(serviceProvisionID.ToString())
                .TapAuthoriseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 1\r\nUpdated records: 1\r\n")
                .TapOKButton();

            usedInFinanceFieldValue = (bool)dbHelper.serviceProvided.GetByID(serviceProvidedId, "usedinfinance")["usedinfinance"];
            Assert.IsTrue(usedInFinanceFieldValue);

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToCostsPerWeekTab();

            serviceProvisionCostPerWeekPage
                .WaitForServiceProvisionCostsPerWeekPageToLoad()
                .ValidateRecordCellContent(1, 4, "£140.00");

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID2, validRateUnits[1], 1, 1, true, true, true, true, true, true, true, true, "", new TimeSpan(7, 0, 0));
            dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID3, validRateUnits[1], 1, 1, true, true, true, true, true, true, true, true, "", new TimeSpan(21, 30, 0));

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickBackButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .SelectRecord(serviceProvisionID2.ToString())
                .SelectRecord(serviceProvisionID3.ToString())
                .TapReadyToAuthoriseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 2\r\nUpdated records: 2\r\n")
                .TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapRefreshButton()
                .SelectRecord(serviceProvisionID2.ToString())
                .SelectRecord(serviceProvisionID3.ToString())
                .TapAuthoriseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 2\r\nUpdated records: 2\r\n")
                .TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID2.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToCostsPerWeekTab();

            serviceProvisionCostPerWeekPage
                .WaitForServiceProvisionCostsPerWeekPageToLoad()
                .ValidateRecordCellContent(1, 4, "£70.00");

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickBackButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID3.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToCostsPerWeekTab();

            serviceProvisionCostPerWeekPage
                .WaitForServiceProvisionCostsPerWeekPageToLoad()
                .ValidateRecordCellContent(1, 4, "£210.00");
            #endregion

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1648")]
        [Description("Test automation for Step 36 to 37 from CDV6-3123: To verify creation of Service Provision record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_CDV6_3123_UITestMethod08()
        {

            #region Provider - Supplier

            _providerName = "Provider_" + _currentDateSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 2, _providerName + "@test.com");
            _providerNumber = (int)dbHelper.provider.GetProviderByID(_providerId, "providernumber")["providernumber"];

            #endregion

            #region Service Element 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE1_3123" + _currentDateSuffix;
            _serviceElement2IdName = "SE2_3123" + _currentDateSuffix;

            var defaulRateUnitID1 = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID1);
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());

            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, 1, 1, validRateUnits, validRateUnits[1], null, false);
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2022, 1, 1), code);

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region Service Package Type
            var servicePackageType = dbHelper.servicePackageType.CreateServicePackageType(_careDirectorQA_TeamId, "P3123_" + _currentDateSuffix, new DateTime(2020, 1, 1));

            var servicePackageId = dbHelper.servicePackage.CreateServicePackage(_careDirectorQA_TeamId, _systemUserId, _personID, servicePackageType, new DateTime(2020, 1, 1));
            #endregion

            #region Service Provided
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2, false);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, validRateUnits[1], new DateTime(2023, 1, 1));

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId1, serviceProvidedId, 10m, 10m, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), true, true, true, true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(serviceProvidedId, 2);

            var plannedStartDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6).Date;
            var plannedEndDate = plannedStartDate.AddDays(1);

            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[1], _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate, null, plannedEndDate, null, null);

            var serviceProvisionID2 = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[1], _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate, null, plannedEndDate, null, null, servicePackageId);


            #endregion

            #region Service Delivery
            dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID, validRateUnits[1], 1, 1, true, true, true, true, true, true, true, true, "", new TimeSpan(7, 0, 0));
            dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID2, validRateUnits[1], 1, 1, true, true, true, true, true, true, true, true, "", new TimeSpan(8, 0, 0));

            #endregion

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirectorQA_TeamId, _systemUserId, new DateTime(2020, 1, 1), 4, 999999m, true, true);

            #endregion            

            #region Step 36 to 37

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .SelectRecord(serviceProvisionID.ToString())
                .SelectRecord(serviceProvisionID2.ToString())
                .TapReadyToAuthoriseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 2\r\nUpdated records: 2\r\n")
                .TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapRefreshButton()
                .SelectRecord(serviceProvisionID.ToString())
                .SelectRecord(serviceProvisionID2.ToString())
                .TapAuthoriseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 2\r\nUpdated records: 2\r\n")
                .TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToCostsPerWeekTab();

            var costPerWeekId = dbHelper.serviceProvisionCostPerWeek.GetServiceProvisionCostPerWeekByServiceProvisionID(serviceProvisionID)[0];

            serviceProvisionCostPerWeekPage
                .WaitForServiceProvisionCostsPerWeekPageToLoad()
                .OpenRecord(costPerWeekId.ToString());

            serviceProvisionCostPerWeekRecordPage
                .WaitForServiceProvisionCostPerWeekRecordPageToLoad()
                .ValidateCostPerWeekText("70.00")
                .ClickBackButton();

            serviceProvisionCostPerWeekPage
                .WaitForServiceProvisionCostsPerWeekPageToLoad();

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickBackButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID2.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToCostsPerWeekTab();

            var costPerWeekId2 = dbHelper.serviceProvisionCostPerWeek.GetServiceProvisionCostPerWeekByServiceProvisionID(serviceProvisionID2)[0];

            serviceProvisionCostPerWeekPage
                .WaitForServiceProvisionCostsPerWeekPageToLoad()
                .OpenRecord(costPerWeekId2.ToString());

            serviceProvisionCostPerWeekRecordPage
                .WaitForServiceProvisionCostPerWeekRecordPageToLoad()
                .ValidateCostPerWeekText("70.00")
                .ClickBackButton();

            serviceProvisionCostPerWeekPage
                .WaitForServiceProvisionCostsPerWeekPageToLoad();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1588

        [TestProperty("JiraIssueID", "ACC-1656")]
        [Description("Test automation for Step 38 to 41 from CDV6-3123: To verify creation of Service Provision record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_CDV6_3123_UITestMethod09()
        {
            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Data Form

            var dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Brokerage Episode Priority

            var brokerageEpisodePriorityID = commonMethodsDB.CreateBrokerageEpisodePriority(new Guid("8a5baf95-fc3d-eb11-a2e5-0050569231cf"), "Priority", new DateTime(2021, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Provider - Supplier

            _providerName = "Provider_" + _currentDateSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 2, _providerName + "@test.com");
            _providerNumber = (int)dbHelper.provider.GetProviderByID(_providerId, "providernumber")["providernumber"];

            #endregion

            #region GL Code            
            var glCodeLocation_SpecialSchemeType = dbHelper.glCodeLocation.GetByName("Special Scheme Type").FirstOrDefault();

            var glCodeId = commonMethodsDB.CreateGLCode(_careDirectorQA_TeamId, glCodeLocation_SpecialSchemeType, "Special Scheme", "123", "123", false);
            #endregion

            #region Service Element 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE1_3123" + _currentDateSuffix;
            _serviceElement2IdName = "SE2_3123" + _currentDateSuffix;

            var defaulRateUnitID1 = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID1);
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());

            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, 1, 1, validRateUnits, validRateUnits[1], null, false);
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2022, 1, 1), code);

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region Case

            var caseID = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, _personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, dataFormId, null, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);

            #endregion

            #region Source of Brokerage Requests

            var sourceOfBrokerageRequestsID = dbHelper.brokerageRequestSource.GetByName("Community Hospital")[0];

            #endregion

            #region Case and Brokerage Episode
            int contactType = 1; // Spot            
            var sourceDate = new DateTime(2020, 1, 1, 1, 0, 0);

            //Creating New Brokerage Episode
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(_careDirectorQA_TeamId, caseID, _personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, sourceDate, sourceDate, 1, 0, 0, contactType, false, false, false, false, true, true, false);

            #endregion

            #region Service Provided and Service Provision
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2, false);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, validRateUnits[1], new DateTime(2023, 1, 1));

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId1, serviceProvidedId, 10m, 10m, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), true, true, true, true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(serviceProvidedId, 2);

            var plannedStartDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek - 6).Date;
            var plannedEndDate = plannedStartDate.AddDays(1);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[1], _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate, null, plannedEndDate, null, null);

            //Creating New Service Provision for Brokerage Episode
            var serviceProvisionID2 = dbHelper.serviceProvision.CreateServiceProvisionForBrokerageEpisode(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid, _serviceElement1Id, _serviceElement2Id,
                    null, glCodeId, validRateUnits[1], _serviceprovisionstartreasonid, null, _careDirectorQA_TeamId, null,
                    null, null, _placementRoomTypeId, sourceDate, null, null, episodeId);
            #endregion

            #region Service Delivery

            var serviceDeliveryId1 = dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID, validRateUnits[1], 22, 1, true, true, true, true, true, true, true, true, "", new TimeSpan(6, 0, 0));
            var serviceDeliveryId2 = dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID2, validRateUnits[1], 22, 1, true, true, true, true, true, true, true, true, "", new TimeSpan(7, 0, 0));

            #endregion

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                commonMethodsDB.CreateAuthorisationLevel(_careDirectorQA_TeamId, _systemUserId, new DateTime(2020, 1, 1), 4, 999999m, true, true);

            #endregion            

            #region Step 38 to 41

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID2.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToServiceDeliveriesTab();


            serviceDeliveriesPage
                .WaitForServiceDeliveriesPageToLoad()
                .OpenRecord(serviceDeliveryId2.ToString());

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ValidateRateUnitIdLinkText("Per 1 Hour \\ Hours (Whole)")
                .ClickBackButton();

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickRateUnitLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per Day")
                .TapSearchButton()
                .SelectResultElement(validRateUnits[0].ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("This Service Provision has Service Delivery record with different Rate Unit having greater Units value than current Rate Unit therefore the update to Rate Unit is not possible. Please correct as necessary.")
                .TapCloseButton();

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToServiceDeliveriesTab();

            serviceDeliveriesPage
                .WaitForServiceDeliveriesPageToLoad()
                .OpenRecord(serviceDeliveryId1.ToString());

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ValidateRateUnitIdLinkText("Per 1 Hour \\ Hours (Whole)")
                .ClickBackButton();

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickRateUnitLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per Day")
                .TapSearchButton()
                .SelectResultElement(validRateUnits[0].ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("This Service Provision has Service Delivery record with different Rate Unit having greater Units value than current Rate Unit therefore the update to Rate Unit is not possible. Please correct as necessary.")
                .TapCloseButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1589

        [TestProperty("JiraIssueID", "ACC-1677")]
        [Description("Test automation for Step 43 from CDV6-3123: To verify creation of Service Provision record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_CDV6_3123_UITestMethod10()
        {
            #region Provider - Supplier

            _providerName = "Provider_" + _currentDateSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 2, _providerName + "@test.com");
            _providerNumber = (int)dbHelper.provider.GetProviderByID(_providerId, "providernumber")["providernumber"];

            #endregion           

            #region Service Element 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE1_3123" + _currentDateSuffix;
            _serviceElement2IdName = "SE2_3123" + _currentDateSuffix;

            var defaulRateUnitID1 = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID1);
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());

            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, 1, 1, validRateUnits, validRateUnits[1], null, false);
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2022, 1, 1), code);

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region Service Provided and Service Provision
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2, false);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, validRateUnits[1], new DateTime(2023, 1, 1));

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId1, serviceProvidedId, 10m, 10m, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), true, true, true, true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(serviceProvidedId, 2);

            var plannedStartDate1 = new DateTime(2023, 4, 24);
            var plannedEndDate1 = new DateTime(2023, 4, 27);

            var plannedStartDate2 = new DateTime(2023, 4, 28);
            var plannedEndDate2 = new DateTime(2023, 4, 30);

            var plannedStartDate3 = new DateTime(2023, 5, 1);
            var plannedEndDate3 = new DateTime(2023, 5, 4);

            var serviceProvisionID1 = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[1], _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, plannedEndDate1, null, null);

            var serviceProvisionID2 = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[1], _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate3, null, plannedEndDate3, null, null);

            var serviceProvisionID3 = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[1], _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate2, null, plannedEndDate2, null, null);


            #endregion

            #region Service Delivery
            dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID1, validRateUnits[1], 1, 1, true, true, true, true, true, true, true, true, "", new TimeSpan(7, 0, 0));
            dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID2, validRateUnits[1], 1, 1, true, true, true, true, true, true, true, true, "", new TimeSpan(8, 0, 0));
            dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID3, validRateUnits[1], 1, 1, true, true, true, true, true, true, true, true, "", new TimeSpan(6, 0, 0));

            #endregion

            #region Authorisation Level

            var authorisationLevelExist = dbHelper.authorisationLevel.GetBySystemUserId(_systemUserId, 4).Any();
            if (!authorisationLevelExist)
                dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirectorQA_TeamId, _systemUserId, new DateTime(2020, 1, 1), 4, 999999m, true, true);

            #endregion            

            #region Step 43

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .SelectRecord(serviceProvisionID1.ToString())
                .SelectRecord(serviceProvisionID2.ToString())
                .SelectRecord(serviceProvisionID3.ToString())
                .TapReadyToAuthoriseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 3\r\nUpdated records: 3\r\n")
                .TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapRefreshButton()
                .SelectRecord(serviceProvisionID1.ToString())
                .SelectRecord(serviceProvisionID2.ToString())
                .SelectRecord(serviceProvisionID3.ToString())
                .TapAuthoriseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Total selected records: 3\r\nUpdated records: 3\r\n")
                .TapOKButton();

            Assert.AreEqual(Authorised_Serviceprovisionstatusid.ToString(), dbHelper.serviceProvision.GetByID(serviceProvisionID1, "serviceprovisionstatusid")["serviceprovisionstatusid"].ToString());
            Assert.AreEqual(Authorised_Serviceprovisionstatusid.ToString(), dbHelper.serviceProvision.GetByID(serviceProvisionID2, "serviceprovisionstatusid")["serviceprovisionstatusid"].ToString());
            Assert.AreEqual(Authorised_Serviceprovisionstatusid.ToString(), dbHelper.serviceProvision.GetByID(serviceProvisionID3, "serviceprovisionstatusid")["serviceprovisionstatusid"].ToString());
            #endregion
        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1590

        [TestProperty("JiraIssueID", "ACC-1680")]
        [Description("Test automation for Step 44 to 47 from CDV6-3123: To verify creation of Service Provision record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_CDV6_3123_UITestMethod11()
        {
            #region System Settings - EnablePurchasingTeamFilter
            Guid enablePurchasingTeamFilterId = commonMethodsDB.CreateSystemSetting("EnablePurchasingTeamFilter", "true",
                "Should the Purchasing Team lookup on Service Provision form only list Teams that have a Service Permission record for the selected Service Element 1? Valid values are \"true\" or \"false\".",
                false, "");

            dbHelper.systemSetting.UpdateSystemSettingValue(enablePurchasingTeamFilterId, "true");

            #endregion

            #region Team - Team not mapped in service permission
            var _purchasingTeam2Id = commonMethodsDB.CreateTeam("PurchasingTeam1", null, _careDirectorQA_BusinessUnitId, "997678", "PurchasingTeam1@careworkstempmail.com", "PurchasingTeam1", "020 123456");


            #endregion

            #region Provider - Supplier

            _providerName = "Provider_" + _currentDateSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 2, _providerName + "@test.com");
            _providerNumber = (int)dbHelper.provider.GetProviderByID(_providerId, "providernumber")["providernumber"];

            #endregion           

            #region Service Element 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE1_3123" + _currentDateSuffix;
            _serviceElement2IdName = "SE2_3123" + _currentDateSuffix;

            var defaulRateUnitID1 = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID1);
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());

            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code, 1, 1, validRateUnits, validRateUnits[1], null, false);
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2022, 1, 1), code);

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region Service Permissions
            dbHelper.servicePermission.CreateServicePermission(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId,
                _serviceElement1Id, _careDirectorQA_TeamId);

            #endregion

            #region Service Provided and Service Provision
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2, false);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, validRateUnits[1], new DateTime(2023, 1, 1));

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2019, 1, 1), null, 0);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId1, serviceProvidedId, 10m, 10m, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), true, true, true, true, true, true, true, true);

            dbHelper.serviceProvidedRatePeriod.UpdateApprovalStatus(ratePeriodId1, 2);
            dbHelper.serviceProvided.UpdateStatus(serviceProvidedId, 2);

            var plannedStartDate1 = new DateTime(2023, 4, 24);
            var plannedEndDate1 = new DateTime(2023, 4, 27);

            var serviceProvisionID1 = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[1], _serviceprovisionstartreasonid, _serviceprovisionendreasonid,
                _careDirectorQA_TeamId, serviceProvidedId, _providerId, _systemUserId, _placementRoomTypeId, plannedStartDate1, null, plannedEndDate1, null, null);

            #endregion

            #region Step 44 to 47

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID1.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickPurchasingteamLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("CareDirector QA")
                .TapSearchButton()
                .ValidateResultElementPresent(_careDirectorQA_TeamId.ToString())
                .TypeSearchQuery("PurchasingTeam1")
                .TapSearchButton()
                .ValidateResultElementNotPresent(_purchasingTeam2Id.ToString())
                .ClickCloseButton();

            dbHelper.systemSetting.UpdateSystemSettingValue(enablePurchasingTeamFilterId, "false");

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickBackButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapRefreshButton()
                .OpenRecord(serviceProvisionID1.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickPurchasingteamLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("CareDirector QA")
                .TapSearchButton()
                .ValidateResultElementPresent(_careDirectorQA_TeamId.ToString())
                .TypeSearchQuery("PurchasingTeam1")
                .TapSearchButton()
                .ValidateResultElementPresent(_purchasingTeam2Id.ToString())
                .ClickCloseButton();

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickBackButton();

            dbHelper.systemSetting.UpdateSystemSettingValue(enablePurchasingTeamFilterId, "true");

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapRefreshButton()
                .OpenRecord(serviceProvisionID1.ToString());

            #region Service Element 1 & 2

            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var _serviceElement1IdName2 = "SE1b_3123" + _currentDateSuffix;
            var _serviceElement2IdName2 = "SE2b_3123" + _currentDateSuffix;

            defaulRateUnitID1 = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID1);
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());

            _serviceElement1Id2 = commonMethodsDB.CreateServiceElement1(_serviceElement1IdName2, _careDirectorQA_TeamId, new DateTime(2022, 1, 1), code2, 1, 1, validRateUnits, validRateUnits[1], null, false);
            _serviceElement2Id2 = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName2, new DateTime(2022, 1, 1), code2);

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id2, _serviceElement2Id2);

            #endregion

            #region Service Permissions
            dbHelper.servicePermission.CreateServicePermission(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId,
                _serviceElement1Id2, _purchasingTeam2Id);

            #endregion

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_serviceElement1IdName2)
                .TapSearchButton()
                .SelectResultElement(_serviceElement1Id2.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ValidatePurchasingTeamFieldBlankFieldVisible(true);

            serviceProvisionRecordPage
                .ClickPurchasingteamLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("PurchasingTeam1")
                .TapSearchButton()
                .ValidateResultElementPresent(_purchasingTeam2Id.ToString())
                .TypeSearchQuery("CareDirector QA")
                .TapSearchButton()
                .ValidateResultElementNotPresent(_careDirectorQA_TeamId.ToString())
                .ClickCloseButton();

            #endregion
        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1593

        [TestProperty("JiraIssueID", "ACC-1689")]
        [Description("Test automation for Step 48 to 49 from CDV6-3123: To verify creation of Service Provision record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ServiceProvision_CDV6_3123_UITestMethod12()
        {
            #region Service Element 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            _serviceElement1IdName = "SE1_3123" + _currentDateSuffix;
            _serviceElement2IdName = "SE2_3123" + _currentDateSuffix;

            var defaulRateUnitID1 = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID1);
            var rateUnitID2 = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault();
            validRateUnits.Add(rateUnitID2);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];

            _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_careDirectorQA_TeamId, _serviceElement1IdName, new DateTime(2023, 5, 1), code, 3, 1, validRateUnits, validRateUnits[1], paymentTypeCodeId, providerBatchGroupingId, 0, vatCodeId);
            _serviceElement2Id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, _serviceElement2IdName, new DateTime(2023, 5, 1), code);
            var serviceelement2ids = new List<Guid>();
            serviceelement2ids.Add(_serviceElement2Id);

            commonMethodsDB.CreateServiceMapping(_careDirectorQA_TeamId, _serviceElement1Id, _serviceElement2Id);

            #endregion

            #region Service Permissions
            dbHelper.servicePermission.CreateServicePermission(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId,
                _serviceElement1Id, _careDirectorQA_TeamId);

            #endregion

            #region GL Code Mappings
            var glCode_Level1Id = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
            dbHelper.glCodeMapping.CreateGLCodeMapping(_careDirectorQA_TeamId, _serviceElement1Id, glCode_Level1Id);

            #endregion

            #region Authorisation Level

            dbHelper.authorisationLevel.CreateAuthorisationLevel(_careDirectorQA_TeamId, _systemUserId, new DateTime(2020, 1, 1), 10, null, true, null);

            #endregion

            #region Service Provision
            var plannedStartDate1 = new DateTime(2023, 5, 1);

            var serviceProvisionID1 = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID, Draft_Serviceprovisionstatusid,
                _serviceElement1Id, _serviceElement2Id, null, null, validRateUnits[1], _serviceprovisionstartreasonid, null,
                null, serviceProvidedId, null, null, _placementRoomTypeId, plannedStartDate1, null, null, null, null, null, true);

            var _serviceProvision1Name = (string)dbHelper.serviceProvision.GetServiceProvisionById(serviceProvisionID1, "name")["name"];

            var serviceProvisionRatePeriodId1 = dbHelper.serviceProvisionRatePeriod.CreateServiceProvisionRatePeriod(_careDirectorQA_TeamId, _personID, _serviceProvision1Name, serviceProvisionID1, validRateUnits[1],
                new DateTime(2023, 5, 1), null);
            dbHelper.serviceProvisionRateSchedule.CreateServiceProvisionRateSchedule(_careDirectorQA_TeamId, _personID, serviceProvisionID1, serviceProvisionRatePeriodId1,
                10m, 10m, new TimeSpan(0, 0, 0), new TimeSpan(8, 0, 0), true, true, true, true, true, true, true, true);

            dbHelper.serviceProvisionRatePeriod.UpdateStatus(serviceProvisionRatePeriodId1, 2);

            #endregion

            #region Service Delivery

            dbHelper.serviceDelivery.CreateServiceDelivery(_careDirectorQA_TeamId, _personID, serviceProvisionID1, validRateUnits[1], 1, 1, true, true, true, true, true, true, true, true, "", new TimeSpan(0, 0, 0));

            #endregion

            #region Service Uprate
            var serviceUprateId = dbHelper.serviceUprate.CreateServiceUprate(_careDirectorQA_TeamId, null, new DateTime(2023, 5, 1), 1, 1, 1, 20m, 30m, 1,
                3, validRateUnits[1], 1, _serviceElement1Id, serviceelement2ids, true);

            #endregion

            #region Step 48 to 49

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickServiceProvisionAreaButton()
                .ClickServiceUpratesButton();

            serviceUpratesPage
                .WaitForServiceUpratePageToLoad()
                .ValidateRecordIsPresent(serviceUprateId.ToString(), true)
                .OpenRecord(serviceUprateId.ToString());

            serviceUprateRecordPage
                .WaitForServiceUprateRecordPageToLoad()
                .ValidateStatusSelectedText("New");

            var serviceUprateDetailsList = dbHelper.serviceUprateDetail.GetByServiceUprateID(serviceUprateId);
            Assert.AreEqual(0, serviceUprateDetailsList.Count);

            serviceUprateRecordPage
                .ClickGenerateButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Service Uprate Details are being generated in the background.")
                .TapCloseButton();

            serviceUprateRecordPage
                .WaitForServiceUprateRecordPageToLoad()
                .ClickBackButton();

            serviceUpratesPage
                .WaitForServiceUpratePageToLoad()
                .ClickRefreshButton()
                .OpenRecord(serviceUprateId.ToString());

            serviceUprateRecordPage
                .WaitForServiceUprateRecordPageToLoad()
                .ValidateStatusSelectedText("Generated");

            serviceUprateDetailsList = dbHelper.serviceUprateDetail.GetByServiceUprateID(serviceUprateId);
            Assert.AreEqual(1, serviceUprateDetailsList.Count);

            serviceUprateRecordPage
                .ClickProcessButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Service Uprate is being processed in the background.")
                .TapCloseButton();

            serviceUprateRecordPage
                .WaitForServiceUprateRecordPageToLoad()
                .ClickBackButton();

            serviceUpratesPage
                .WaitForServiceUpratePageToLoad()
                .ClickRefreshButton()
                .OpenRecord(serviceUprateId.ToString());

            serviceUprateRecordPage
                .WaitForServiceUprateRecordPageToLoad()
                .ValidateStatusSelectedText("Processed")
                .ClickBackButton();

            serviceUprateDetailsList = dbHelper.serviceUprateDetail.GetByServiceUprateID(serviceUprateId);
            Assert.AreEqual(1, serviceUprateDetailsList.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

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
                .TypeSearchQuery(_serviceElement1IdName)
                .TapSearchButton()
                .SelectResultElement(_serviceElement1Id.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickRateUnitLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(rateUnitID2.ToString())
                .ValidateResultElementPresent(defaulRateUnitID1.ToString())
                .ClickCloseButton();

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .ValidateRecordIsPresent(serviceProvisionID1.ToString(), true)
                .SelectRecord(serviceProvisionID1.ToString())
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            serviceUprateDetailsList = dbHelper.serviceUprateDetail.GetByServiceUprateID(serviceUprateId);
            Assert.AreEqual(0, serviceUprateDetailsList.Count);


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
