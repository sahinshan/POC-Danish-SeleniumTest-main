using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceProvisions
{
    [TestClass]
    public class ServiceDelivery_UITestCases_JB : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;


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

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("ServiceDelivery BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("ServiceDelivery T1", null, _businessUnitId, "907678", "ServiceDeliveryT1@careworkstempmail.com", "ServiceDelivery T1", "020 123456");

                #endregion

                #region Authorization Level

                //Allowances
                commonMethodsDB.CreateAuthorisationLevel(_teamId, defaultSystemUserId, new DateTime(2020, 1, 1), 1, 999999m, true, true);

                //Fees
                commonMethodsDB.CreateAuthorisationLevel(_teamId, defaultSystemUserId, new DateTime(2020, 1, 1), 2, 999999m, true, true);

                //Financial Assessments
                commonMethodsDB.CreateAuthorisationLevel(_teamId, defaultSystemUserId, new DateTime(2020, 1, 1), 3, 999999m, true, true);

                //Service Provisions
                commonMethodsDB.CreateAuthorisationLevel(_teamId, defaultSystemUserId, new DateTime(2020, 1, 1), 4, 999999m, true, true);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User ServiceDeliveryUser3

                _systemUsername = "ServiceDeliveryUser3";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ServiceDelivery", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22814

        [TestProperty("JiraIssueID", "CDV6-25295")]
        [Description("Step(s) 1 to 8 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceDelivery_UITestMethod001()
        {
            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, null);

            #endregion

            #region Service Element 2

            var _serviceElement2Name = "SE2_" + partialDateTimeSuffix;
            code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, serviceElement2Id);

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 2); //creating a "Supplier" provider
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, providerid, serviceElement1Id, serviceElement2Id, null, null, null, 2);
            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, serviceProvidedId, validRateUnits[0], new DateTime(2022, 1, 1), 2);
            var serviceProvidedRateScheduleId = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Person

            var _firstName = "Paul";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Service Provision Status

            var draftServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var authorisedServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

            #endregion

            #region Service Provision Start Reason

            var serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Service Provision End Reason

            var serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_teamId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            var actualStartDate = new DateTime(2022, 6, 20);
            var actualEndDate = new DateTime(2022, 6, 26);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(
                _teamId, _systemUserId, _personID,
                draftServiceprovisionstatusid, serviceElement1Id, serviceElement2Id,
                validRateUnits[0],
                serviceprovisionstartreasonid, serviceprovisionendreasonid,
                _teamId, serviceProvidedId, providerid, _systemUserId,
                placementRoomTypeId, actualStartDate, actualEndDate, null);

            var serviceProvisionName = (string)(dbHelper.serviceProvision.GetByID(serviceProvisionID, "name")["name"]);

            #endregion


            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
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
                .WaitForServiceDeliveriesPageToLoad();

            #endregion

            #region Step 3

            serviceDeliveriesPage
                .ClickNewRecordButton();

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad();

            #endregion

            #region Step 4

            serviceDeliveryRecordPage
                .InsertTextOnPlannedStartTime("09:00")
                .InsertTextOnUnits("1")
                .InsertTextOnNumberOfCarers("2")
                .ClickSelectAll_YesRadioButton()
                .InsertTextOnNoteText("Note text ...")
                .ClickSaveAndCloseButton();

            serviceDeliveriesPage
                .WaitForServiceDeliveriesPageToLoad()
                .ClickRefreshButton();

            var serviceDeliveries = dbHelper.serviceDelivery.GetByServiceProvisionID(serviceProvisionID);
            Assert.AreEqual(1, serviceDeliveries.Count);
            var serviceDeliveryId = serviceDeliveries[0];

            serviceDeliveriesPage
                .OpenRecord(serviceDeliveryId);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ValidateserviceProvisionidLinkText(serviceProvisionName.Replace("  ", " "))
                .ValidatePlannedStartTimeText("09:00")
                .ValidateTotalVisitsText("7")
                .ValidateNumberOfCarersText("2")
                .ValidateResponsibleTeamLinkText("ServiceDelivery T1")
                .ValidateRateUnitIdLinkText("Per 1 Hour \\ Hours (Whole)")
                .ValidateUnitsText("1.0000")
                .ValidateTotalUnitsText("14.0000")
                .ValidateSelectAll_YesRadioButtonChecked()
                .ValidateMonday_YesRadioButtonChecked()
                .ValidateTuesday_YesRadioButtonChecked()
                .ValidateWednesday_YesRadioButtonChecked()
                .ValidateThursday_YesRadioButtonChecked()
                .ValidateFriday_YesRadioButtonChecked()
                .ValidateSaturday_YesRadioButtonChecked()
                .ValidateSunday_YesRadioButtonChecked()
                .ValidateNoteText("Note text ...");

            #endregion

            #region Step 5

            serviceDeliveryRecordPage
                .ClickCloneButton();

            cloneServiceDeliveryPopup
                .WaitForCloneServiceDeliveryPopupToLoad();

            #endregion

            #region Step 6

            cloneServiceDeliveryPopup
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Are you sure you want to clone this Service Delivery record - values will be copied identically?")
                .ValidateSuccessMessageVisibility(false);

            #endregion

            #region Step 7

            cloneServiceDeliveryPopup
                .ClickCloneButton()
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Are you sure you want to clone this Service Delivery record - values will be copied identically?")
                .ValidateSuccessMessageVisibility(true)
                .ValidateSuccessMessageText("Service Delivery record cloned successfully.")
                .ClickCancelButton();

            #endregion

            #region Step 8

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ClickBackButton();

            serviceDeliveriesPage
                .WaitForServiceDeliveriesPageToLoad();

            serviceDeliveries = dbHelper.serviceDelivery.GetByServiceProvisionID(serviceProvisionID);
            Assert.AreEqual(2, serviceDeliveries.Count);
            var newServiceDeviceryId = serviceDeliveries.Where(c => c != serviceDeliveryId).FirstOrDefault();

            serviceDeliveriesPage
                .OpenRecord(newServiceDeviceryId);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ValidateserviceProvisionidLinkText(serviceProvisionName.Replace("  ", " "))
                .ValidatePlannedStartTimeText("09:00")
                .ValidateTotalVisitsText("7")
                .ValidateNumberOfCarersText("2")
                .ValidateResponsibleTeamLinkText("ServiceDelivery T1")
                .ValidateRateUnitIdLinkText("Per 1 Hour \\ Hours (Whole)")
                .ValidateUnitsText("1.0000")
                .ValidateTotalUnitsText("14.0000")
                .ValidateSelectAll_YesRadioButtonChecked()
                .ValidateMonday_YesRadioButtonChecked()
                .ValidateTuesday_YesRadioButtonChecked()
                .ValidateWednesday_YesRadioButtonChecked()
                .ValidateThursday_YesRadioButtonChecked()
                .ValidateFriday_YesRadioButtonChecked()
                .ValidateSaturday_YesRadioButtonChecked()
                .ValidateSunday_YesRadioButtonChecked()
                .ValidateNoteText("Note text ...");

            #endregion
        }

        [TestProperty("JiraIssueID", "CDV6-25296")]
        [Description("Step(s) 10 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceDelivery_UITestMethod002()
        {
            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per 1/2 Hour Unit \\ Units (Half)").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID);
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, null);

            #endregion

            #region Service Element 2

            var _serviceElement2Name = "SE2_" + partialDateTimeSuffix;
            code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, serviceElement2Id);

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 2); //creating a "Supplier" provider
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, providerid, serviceElement1Id, serviceElement2Id, null, null, null, 2);
            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, serviceProvidedId, validRateUnits[0], new DateTime(2022, 1, 1), 2);
            var serviceProvidedRateScheduleId = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Person

            var _firstName = "Paul";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Service Provision Status

            var draftServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var authorisedServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

            #endregion

            #region Service Provision Start Reason

            var serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Service Provision End Reason

            var serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_teamId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            var actualStartDate = new DateTime(2022, 6, 20);
            var actualEndDate = new DateTime(2022, 6, 26);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(
                _teamId, _systemUserId, _personID,
                draftServiceprovisionstatusid, serviceElement1Id, serviceElement2Id,
                validRateUnits[0],
                serviceprovisionstartreasonid, serviceprovisionendreasonid,
                _teamId, serviceProvidedId, providerid, _systemUserId,
                placementRoomTypeId, actualStartDate, actualEndDate, null);

            #endregion

            #region Service Delivery

            var serviceDeliveryID = dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personID, serviceProvisionID, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion


            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
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
                .WaitForServiceDeliveriesPageToLoad();

            serviceDeliveriesPage
                .OpenRecord(serviceDeliveryID);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ValidateCloneButtonHidden();

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25297")]
        [Description("Step(s) 11 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceDelivery_UITestMethod003()
        {
            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, null);

            #endregion

            #region Service Element 2

            var _serviceElement2Name = "SE2_" + partialDateTimeSuffix;
            code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, serviceElement2Id);

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 2); //creating a "Supplier" provider
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, providerid, serviceElement1Id, serviceElement2Id, null, null, null, 2);
            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, serviceProvidedId, validRateUnits[0], new DateTime(2022, 1, 1), 2);
            var serviceProvidedRateScheduleId = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Person

            var _firstName = "Paul";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Service Provision Status

            var draftServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var authorisedServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];
            var cancelledServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Cancelled")[0];

            #endregion

            #region Service Provision Start Reason

            var serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Service Provision End Reason

            var serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_teamId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            var actualStartDate = new DateTime(2022, 6, 20);
            var actualEndDate = new DateTime(2022, 6, 26);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(
                _teamId, _systemUserId, _personID,
                draftServiceprovisionstatusid, serviceElement1Id, serviceElement2Id,
                validRateUnits[0],
                serviceprovisionstartreasonid, serviceprovisionendreasonid,
                _teamId, serviceProvidedId, providerid, _systemUserId,
                placementRoomTypeId, actualStartDate, actualEndDate, null);

            #endregion

            #region Service Delivery

            var serviceDeliveryID = dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personID, serviceProvisionID, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Authorise Service Provision

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, authorisedServiceprovisionstatusid);

            #endregion


            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
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
                .OpenRecord(serviceDeliveryID);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ValidateCloneButtonHidden()
                .ClickBackButton();

            serviceDeliveriesPage
                .WaitForServiceDeliveriesPageToLoad();

            //cancel the service provision
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, cancelledServiceprovisionstatusid);

            serviceDeliveriesPage
                .ClickRefreshButton()
                .OpenRecord(serviceDeliveryID);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ValidateCloneButtonHidden();

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25298")]
        [Description("Step(s) 12 from the original jira test ")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceDelivery_UITestMethod004()
        {
            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, null);

            #endregion

            #region Service Element 2

            var _serviceElement2Name = "SE2_" + partialDateTimeSuffix;
            code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, serviceElement2Id);

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 2); //creating a "Supplier" provider
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, providerid, serviceElement1Id, serviceElement2Id, null, null, null, 2);
            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, serviceProvidedId, validRateUnits[0], new DateTime(2022, 1, 1), 2);
            var serviceProvidedRateScheduleId = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Person

            var _firstName = "Paul";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Service Provision Status

            var draftServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var authorisedServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];
            var readyForAuthorisationServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];

            #endregion

            #region Service Provision Start Reason

            var serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Service Provision End Reason

            var serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_teamId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            var actualStartDate = new DateTime(2022, 6, 20);
            var actualEndDate = new DateTime(2022, 6, 26);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(
                _teamId, _systemUserId, _personID,
                draftServiceprovisionstatusid, serviceElement1Id, serviceElement2Id,
                validRateUnits[0],
                serviceprovisionstartreasonid, serviceprovisionendreasonid,
                _teamId, serviceProvidedId, providerid, _systemUserId,
                placementRoomTypeId, actualStartDate, actualEndDate, null);

            #endregion


            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
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
                 .ClickStatusLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Ready for Authorisation").TapSearchButton().SelectResultElement(readyForAuthorisationServiceprovisionstatusid);

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("At least one Service Delivery record must be recorded before the Service Provision's status can be set to Ready for Authorisation.")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25299")]
        [Description("Step(s) 13 from the original jira test (creating a service delivery)")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceDelivery_UITestMethod005()
        {
            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, null);

            #endregion

            #region Service Element 2

            var _serviceElement2Name = "SE2_" + partialDateTimeSuffix;
            code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, serviceElement2Id);

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 2); //creating a "Supplier" provider
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, providerid, serviceElement1Id, serviceElement2Id, null, null, null, 2);
            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, serviceProvidedId, validRateUnits[0], new DateTime(2022, 1, 1), 2);
            var serviceProvidedRateScheduleId = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Person

            var _firstName = "Paul";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Service Provision Status

            var draftServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var authorisedServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];
            var readyForAuthorisationServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];
            var cancelledServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Cancelled")[0];

            #endregion

            #region Service Provision Start Reason

            var serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Service Provision End Reason

            var serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_teamId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            var actualStartDate = new DateTime(2022, 6, 20);
            var actualEndDate = new DateTime(2022, 6, 26);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(
                _teamId, _systemUserId, _personID,
                draftServiceprovisionstatusid, serviceElement1Id, serviceElement2Id,
                validRateUnits[0],
                serviceprovisionstartreasonid, serviceprovisionendreasonid,
                _teamId, serviceProvidedId, providerid, _systemUserId,
                placementRoomTypeId, actualStartDate, actualEndDate, null);

            #endregion

            #region Service Delivery

            var serviceDeliveryID = dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personID, serviceProvisionID, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Change Service Provision Status

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, readyForAuthorisationServiceprovisionstatusid);

            #endregion

            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
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
                .InsertTextOnUnits("1")
                .InsertTextOnNumberOfCarers("1")
                .ClickSelectAll_YesRadioButton()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Parent Service Provision record is in an invalid status")
                .TapCloseButton();

            //update the Service Provision status to authorised
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, authorisedServiceprovisionstatusid);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Parent Service Provision record is in an invalid status")
                .TapCloseButton();

            //update the Service Provision status to cancelled
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, cancelledServiceprovisionstatusid);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Parent Service Provision record is in an invalid status")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25300")]
        [Description("Step(s) 13 from the original jira test (updating a service delivery)")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceDelivery_UITestMethod006()
        {
            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, null);

            #endregion

            #region Service Element 2

            var _serviceElement2Name = "SE2_" + partialDateTimeSuffix;
            code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, serviceElement2Id);

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 2); //creating a "Supplier" provider
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, providerid, serviceElement1Id, serviceElement2Id, null, null, null, 2);
            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, serviceProvidedId, validRateUnits[0], new DateTime(2022, 1, 1), 2);
            var serviceProvidedRateScheduleId = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Person

            var _firstName = "Paul";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Service Provision Status

            var draftServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var authorisedServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];
            var readyForAuthorisationServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];
            var cancelledServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Cancelled")[0];

            #endregion

            #region Service Provision Start Reason

            var serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Service Provision End Reason

            var serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_teamId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            var actualStartDate = new DateTime(2022, 6, 20);
            var actualEndDate = new DateTime(2022, 6, 26);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(
                _teamId, _systemUserId, _personID,
                draftServiceprovisionstatusid, serviceElement1Id, serviceElement2Id,
                validRateUnits[0],
                serviceprovisionstartreasonid, serviceprovisionendreasonid,
                _teamId, serviceProvidedId, providerid, _systemUserId,
                placementRoomTypeId, actualStartDate, actualEndDate, null);

            #endregion

            #region Service Delivery

            var serviceDeliveryID = dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personID, serviceProvisionID, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Change Service Provision Status

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, readyForAuthorisationServiceprovisionstatusid);

            #endregion

            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
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
                .OpenRecord(serviceDeliveryID);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .InsertTextOnPlannedStartTime("09:00")
                .InsertTextOnNumberOfCarers("2")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Parent Service Provision record is in an invalid status")
                .TapCloseButton();

            //update the Service Provision status to Authorised
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, authorisedServiceprovisionstatusid);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Parent Service Provision record is in an invalid status")
                .TapCloseButton();

            //update the Service Provision status to Cancelled
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, cancelledServiceprovisionstatusid);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Parent Service Provision record is in an invalid status")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25301")]
        [Description("Step(s) 13 from the original jira test (deleting a service delivery)")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceDelivery_UITestMethod007()
        {
            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, null);

            #endregion

            #region Service Element 2

            var _serviceElement2Name = "SE2_" + partialDateTimeSuffix;
            code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, serviceElement2Id);

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 2); //creating a "Supplier" provider
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, providerid, serviceElement1Id, serviceElement2Id, null, null, null, 2);
            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, serviceProvidedId, validRateUnits[0], new DateTime(2022, 1, 1), 2);
            var serviceProvidedRateScheduleId = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Person

            var _firstName = "Paul";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Service Provision Status

            var draftServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var authorisedServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];
            var readyForAuthorisationServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];
            var cancelledServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Cancelled")[0];

            #endregion

            #region Service Provision Start Reason

            var serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Service Provision End Reason

            var serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_teamId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            var actualStartDate = new DateTime(2022, 6, 20);
            var actualEndDate = new DateTime(2022, 6, 26);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(
                _teamId, _systemUserId, _personID,
                draftServiceprovisionstatusid, serviceElement1Id, serviceElement2Id,
                validRateUnits[0],
                serviceprovisionstartreasonid, serviceprovisionendreasonid,
                _teamId, serviceProvidedId, providerid, _systemUserId,
                placementRoomTypeId, actualStartDate, actualEndDate, null);

            #endregion

            #region Service Delivery

            var serviceDeliveryID = dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personID, serviceProvisionID, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Change Service Provision Status

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, readyForAuthorisationServiceprovisionstatusid);

            #endregion

            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
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
                .OpenRecord(serviceDeliveryID);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Parent Service Provision record is in an invalid status")
                .TapCloseButton();

            //update the Service Provision status to Authorised
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, authorisedServiceprovisionstatusid);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Parent Service Provision record is in an invalid status")
                .TapCloseButton();

            //update the Service Provision status to Cancelled
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, cancelledServiceprovisionstatusid);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Parent Service Provision record is in an invalid status")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25302")]
        [Description("Step(s) 15 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceDelivery_UITestMethod008()
        {
            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, null);

            #endregion

            #region Service Element 2

            var _serviceElement2Name = "SE2_" + partialDateTimeSuffix;
            code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, serviceElement2Id);

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 2); //creating a "Supplier" provider
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, providerid, serviceElement1Id, serviceElement2Id, null, null, null, 2);
            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, serviceProvidedId, validRateUnits[0], new DateTime(2022, 1, 1), 2);
            var serviceProvidedRateScheduleId = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Person

            var _firstName = "Paul";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Service Provision Status

            var draftServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var authorisedServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

            #endregion

            #region Service Provision Start Reason

            var serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Service Provision End Reason

            var serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_teamId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            var actualStartDate = new DateTime(2022, 6, 20);
            var actualEndDate = new DateTime(2022, 6, 26);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(
                _teamId, _systemUserId, _personID,
                draftServiceprovisionstatusid, serviceElement1Id, serviceElement2Id,
                validRateUnits[0],
                serviceprovisionstartreasonid, serviceprovisionendreasonid,
                _teamId, serviceProvidedId, providerid, _systemUserId,
                placementRoomTypeId, actualStartDate, actualEndDate, null);

            var serviceProvisionName = (string)(dbHelper.serviceProvision.GetByID(serviceProvisionID, "name")["name"]);

            #endregion

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
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
                .InsertTextOnPlannedStartTime("09:00")
                .InsertTextOnUnits("1")
                .InsertTextOnNumberOfCarers("2")
                .InsertTextOnNoteText("Note text ...")
                .ClickSaveAndCloseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("It is mandatory to have at least one day of the week to be selected").TapOKButton();

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad();

            var serviceDeliveries = dbHelper.serviceDelivery.GetByServiceProvisionID(serviceProvisionID);
            Assert.AreEqual(0, serviceDeliveries.Count);

            #endregion
        }

        [TestProperty("JiraIssueID", "CDV6-25303")]
        [Description("Step(s) 16 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceDelivery_UITestMethod009()
        {
            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, null);

            #endregion

            #region Service Element 2

            var _serviceElement2Name = "SE2_" + partialDateTimeSuffix;
            code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, serviceElement2Id);

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 2); //creating a "Supplier" provider
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, providerid, serviceElement1Id, serviceElement2Id, null, null, null, 2);
            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, serviceProvidedId, validRateUnits[0], new DateTime(2022, 1, 1), 2);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);
            var serviceProvidedRatePeriod2Id = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, serviceProvidedId, defaulRateUnitID, new DateTime(2022, 1, 1), 2);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, serviceProvidedRatePeriod2Id, serviceProvidedId, 10m, 15m);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Person

            var _firstName = "Paul";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Service Provision Status

            var draftServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var readyForAuthorisationServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];
            var authorisedServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

            #endregion

            #region Service Provision Start Reason

            var serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Service Provision End Reason

            var serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_teamId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            var actualStartDate = new DateTime(2022, 6, 20);
            var actualEndDate = new DateTime(2022, 6, 26);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(
                _teamId, _systemUserId, _personID,
                draftServiceprovisionstatusid, serviceElement1Id, serviceElement2Id,
                validRateUnits[0],
                serviceprovisionstartreasonid, serviceprovisionendreasonid,
                _teamId, serviceProvidedId, providerid, _systemUserId,
                placementRoomTypeId, actualStartDate, actualEndDate, null);

            var serviceProvisionName = (string)(dbHelper.serviceProvision.GetByID(serviceProvisionID, "name")["name"]);

            #endregion

            #region Service Delivery

            var serviceDeliveryID = dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personID, serviceProvisionID, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");

            #endregion

            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
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
                 .ClickRateUnitLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Per Day").TapSearchButton().SelectResultElement(defaulRateUnitID);

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .ClickSaveAndCloseButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapRefreshButton()
                .OpenRecord(serviceProvisionID.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .ValidateRateUnitFieldLinkText("Per Day \\ Days");

            //set the status to "Ready for Authorization"
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, readyForAuthorisationServiceprovisionstatusid);

            serviceProvisionRecordPage
                .ClickRateUnitLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Per 1 Hour").TapSearchButton().SelectResultElement(validRateUnits[0]);

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .ClickSaveAndCloseButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapRefreshButton()
                .OpenRecord(serviceProvisionID.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .ValidateRateUnitFieldLinkText("Per 1 Hour \\ Hours (Whole)")
                 .ClickBackButton();

            //set the status to "Authorised"
            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, authorisedServiceprovisionstatusid);

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .TapRefreshButton()
                .OpenRecord(serviceProvisionID.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .ValidateRateUnitLookupButtonDisabled();

            #endregion
        }

        [TestProperty("JiraIssueID", "CDV6-25304")]
        [Description("Step(s) 18 and 19 from the original jira test (updating a service delivery)")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceDelivery_UITestMethod010()
        {
            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, null);

            #endregion

            #region Service Element 2

            var _serviceElement2Name = "SE2_" + partialDateTimeSuffix;
            code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, serviceElement2Id);

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 2); //creating a "Supplier" provider
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, providerid, serviceElement1Id, serviceElement2Id, null, null, null, 2);
            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, serviceProvidedId, validRateUnits[0], new DateTime(2022, 1, 1), 2);
            var serviceProvidedRateScheduleId = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Person

            var _firstName = "Paul";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Service Provision Status

            var draftServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var authorisedServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];
            var readyForAuthorisationServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];
            var cancelledServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Cancelled")[0];

            #endregion

            #region Service Provision Start Reason

            var serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Service Provision End Reason

            var serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_teamId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            var actualStartDate = new DateTime(2022, 6, 20);
            var actualEndDate = new DateTime(2022, 6, 26);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(
                _teamId, _systemUserId, _personID,
                draftServiceprovisionstatusid, serviceElement1Id, serviceElement2Id,
                validRateUnits[0],
                serviceprovisionstartreasonid, serviceprovisionendreasonid,
                _teamId, serviceProvidedId, providerid, _systemUserId,
                placementRoomTypeId, actualStartDate, actualEndDate, null);

            var serviceProvisionName = (string)(dbHelper.serviceProvision.GetByID(serviceProvisionID, "name")["name"]);

            #endregion

            #region Service Delivery

            var serviceDeliveryID = dbHelper.serviceDelivery.CreateServiceDelivery(_teamId, _personID, serviceProvisionID, validRateUnits[0], 1, 1, true, true, true, true, true, true, true, true, "");
            var serviceDeliveryNumber = (int)(dbHelper.serviceDelivery.GetByID(serviceDeliveryID, "servicedeliverynumber")["servicedeliverynumber"]);

            #endregion

            #region Step 18

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
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
                .InsertTextOnPlannedStartTime("15:00")
                .InsertTextOnUnits("1")
                .InsertTextOnNumberOfCarers("2")
                .ClickSelectAll_YesRadioButton()
                .ClickSaveAndCloseButton();

            serviceDeliveriesPage
                .WaitForServiceDeliveriesPageToLoad()
                .ClickRefreshButton();

            var serviceDeliveries = dbHelper.serviceDelivery.GetByServiceProvisionID(serviceProvisionID);
            Assert.AreEqual(2, serviceDeliveries.Count);

            #endregion

            #region Step 19

            serviceDeliveriesPage
                .ValidateRecordCellContent(serviceDeliveryID, 2, _personFullName)
                .ValidateRecordCellContent(serviceDeliveryID, 3, serviceDeliveryNumber.ToString())
                .ValidateRecordCellContent(serviceDeliveryID, 4, "09:00")
                .ValidateRecordCellContent(serviceDeliveryID, 5, "1.0000")
                .ValidateRecordCellContent(serviceDeliveryID, 6, "7.0000")
                .ValidateRecordCellContent(serviceDeliveryID, 7, "7")
                .ValidateRecordCellContent(serviceDeliveryID, 8, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 9, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 10, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 11, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 12, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 13, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 14, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 15, "Per 1 Hour \\ Hours (Whole)")
                .ValidateRecordCellContent(serviceDeliveryID, 16, serviceProvisionName.Replace("  ", " "));

            serviceDeliveriesPage
                .InsertSearchQuery(serviceDeliveryNumber.ToString())
                .ClickSearchButton()
                .ValidateRecordCellContent(serviceDeliveryID, 2, _personFullName)
                .ValidateRecordCellContent(serviceDeliveryID, 3, serviceDeliveryNumber.ToString())
                .ValidateRecordCellContent(serviceDeliveryID, 4, "09:00")
                .ValidateRecordCellContent(serviceDeliveryID, 5, "1.0000")
                .ValidateRecordCellContent(serviceDeliveryID, 6, "7.0000")
                .ValidateRecordCellContent(serviceDeliveryID, 7, "7")
                .ValidateRecordCellContent(serviceDeliveryID, 8, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 9, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 10, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 11, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 12, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 13, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 14, "Yes")
                .ValidateRecordCellContent(serviceDeliveryID, 15, "Per 1 Hour \\ Hours (Whole)")
                .ValidateRecordCellContent(serviceDeliveryID, 16, serviceProvisionName.Replace("  ", " "));

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-25305")]
        [Description("Step(s) 20 to 24 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceDelivery_UITestMethod011()
        {
            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 1; // Provider
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, null);

            #endregion

            #region Service Element 2

            var _serviceElement2Name = "SE2_" + partialDateTimeSuffix;
            code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var serviceElement2Id = commonMethodsDB.CreateServiceElement2(_teamId, _serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, serviceElement2Id);

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 2); //creating a "Supplier" provider
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_teamId, _systemUserId, providerid, serviceElement1Id, serviceElement2Id, null, null, null, 2);
            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_teamId, serviceProvidedId, validRateUnits[0], new DateTime(2022, 1, 1), 2);
            var serviceProvidedRateScheduleId = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_teamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_teamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            #region Person

            var _firstName = "Paul";
            var _lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Service Provision Status

            var draftServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];
            var authorisedServiceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];

            #endregion

            #region Service Provision Start Reason

            var serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Service Provision End Reason

            var serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(_teamId, "Default", new DateTime(2022, 1, 1));

            #endregion

            #region Placement Room Type

            var placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            var actualStartDate = new DateTime(2022, 6, 20);
            var actualEndDate = new DateTime(2022, 6, 26);
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(
                _teamId, _systemUserId, _personID,
                draftServiceprovisionstatusid, serviceElement1Id, serviceElement2Id,
                validRateUnits[0],
                serviceprovisionstartreasonid, serviceprovisionendreasonid,
                _teamId, serviceProvidedId, providerid, _systemUserId,
                placementRoomTypeId, actualStartDate, actualEndDate, null);

            var serviceProvisionName = (string)(dbHelper.serviceProvision.GetByID(serviceProvisionID, "name")["name"]);

            #endregion



            #region Step 20

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
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
                .WaitForServiceDeliveriesPageToLoad();

            serviceDeliveriesPage
                .ClickNewRecordButton();

            #endregion

            #region Step 21

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad();

            serviceDeliveryRecordPage
                .InsertTextOnPlannedStartTime("09:00")
                .InsertTextOnUnits("1")
                .InsertTextOnNumberOfCarers("3")
                .ClickSelectAll_YesRadioButton()
                .InsertTextOnNoteText("Note text ...")
                .ClickSaveAndCloseButton();

            serviceDeliveriesPage
                .WaitForServiceDeliveriesPageToLoad()
                .ClickRefreshButton();

            var serviceDeliveries = dbHelper.serviceDelivery.GetByServiceProvisionID(serviceProvisionID);
            Assert.AreEqual(1, serviceDeliveries.Count);
            var serviceDeliveryId = serviceDeliveries[0];

            serviceDeliveriesPage
                .OpenRecord(serviceDeliveryId);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ValidateserviceProvisionidLinkText(serviceProvisionName.Replace("  ", " "))
                .ValidatePlannedStartTimeText("09:00")
                .ValidateTotalVisitsText("7")
                .ValidateNumberOfCarersText("3")
                .ValidateResponsibleTeamLinkText("ServiceDelivery T1")
                .ValidateRateUnitIdLinkText("Per 1 Hour \\ Hours (Whole)")
                .ValidateUnitsText("1.0000")
                .ValidateTotalUnitsText("21.0000")
                .ValidateSelectAll_YesRadioButtonChecked()
                .ValidateMonday_YesRadioButtonChecked()
                .ValidateTuesday_YesRadioButtonChecked()
                .ValidateWednesday_YesRadioButtonChecked()
                .ValidateThursday_YesRadioButtonChecked()
                .ValidateFriday_YesRadioButtonChecked()
                .ValidateSaturday_YesRadioButtonChecked()
                .ValidateSunday_YesRadioButtonChecked()
                .ValidateNoteText("Note text ...");

            #endregion

            #region Step 22 and 23

            serviceDeliveryRecordPage
                .InsertTextOnUnits("0")
                .ClickSaveAndCloseButton();

            serviceDeliveriesPage
                .WaitForServiceDeliveriesPageToLoad()
                .OpenRecord(serviceDeliveryId);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ValidateserviceProvisionidLinkText(serviceProvisionName.Replace("  ", " "))
                .ValidatePlannedStartTimeText("09:00")
                .ValidateTotalVisitsText("7")
                .ValidateNumberOfCarersText("3")
                .ValidateResponsibleTeamLinkText("ServiceDelivery T1")
                .ValidateRateUnitIdLinkText("Per 1 Hour \\ Hours (Whole)")
                .ValidateUnitsText("0.0000")
                .ValidateTotalUnitsText("0.0000")
                .ValidateSelectAll_YesRadioButtonChecked()
                .ValidateMonday_YesRadioButtonChecked()
                .ValidateTuesday_YesRadioButtonChecked()
                .ValidateWednesday_YesRadioButtonChecked()
                .ValidateThursday_YesRadioButtonChecked()
                .ValidateFriday_YesRadioButtonChecked()
                .ValidateSaturday_YesRadioButtonChecked()
                .ValidateSunday_YesRadioButtonChecked()
                .ValidateNoteText("Note text ...");

            #endregion

            #region Step 24

            serviceDeliveryRecordPage
                .InsertTextOnUnits("1")
                .ClickSelectAll_NoRadioButton()
                .ClickMonday_YesRadioButton()
                .ClickTuesday_YesRadioButton()
                .InsertTextOnNumberOfCarers("0")
                .ClickSaveAndCloseButton();

            serviceDeliveriesPage
                .WaitForServiceDeliveriesPageToLoad()
                .OpenRecord(serviceDeliveryId);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ValidateserviceProvisionidLinkText(serviceProvisionName.Replace("  ", " "))
                .ValidatePlannedStartTimeText("09:00")
                .ValidateTotalVisitsText("2")
                .ValidateNumberOfCarersText("0")
                .ValidateResponsibleTeamLinkText("ServiceDelivery T1")
                .ValidateRateUnitIdLinkText("Per 1 Hour \\ Hours (Whole)")
                .ValidateUnitsText("1.0000")
                .ValidateTotalUnitsText("0.0000")
                .ValidateSelectAll_NoRadioButtonChecked()
                .ValidateMonday_YesRadioButtonChecked()
                .ValidateTuesday_YesRadioButtonChecked()
                .ValidateWednesday_NoRadioButtonChecked()
                .ValidateThursday_NoRadioButtonChecked()
                .ValidateFriday_NoRadioButtonChecked()
                .ValidateSaturday_NoRadioButtonChecked()
                .ValidateSunday_NoRadioButtonChecked()
                .ValidateNoteText("Note text ...");

            #endregion

            #region Step 25

            serviceDeliveryRecordPage
                .InsertTextOnNumberOfCarers("5")
                .ClickSaveAndCloseButton();

            serviceDeliveriesPage
                .WaitForServiceDeliveriesPageToLoad()
                .OpenRecord(serviceDeliveryId);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ValidateserviceProvisionidLinkText(serviceProvisionName.Replace("  ", " "))
                .ValidatePlannedStartTimeText("09:00")
                .ValidateTotalVisitsText("2")
                .ValidateNumberOfCarersText("5")
                .ValidateResponsibleTeamLinkText("ServiceDelivery T1")
                .ValidateRateUnitIdLinkText("Per 1 Hour \\ Hours (Whole)")
                .ValidateUnitsText("1.0000")
                .ValidateTotalUnitsText("10.0000")
                .ValidateSelectAll_NoRadioButtonChecked()
                .ValidateMonday_YesRadioButtonChecked()
                .ValidateTuesday_YesRadioButtonChecked()
                .ValidateWednesday_NoRadioButtonChecked()
                .ValidateThursday_NoRadioButtonChecked()
                .ValidateFriday_NoRadioButtonChecked()
                .ValidateSaturday_NoRadioButtonChecked()
                .ValidateSunday_NoRadioButtonChecked()
                .ValidateNoteText("Note text ...");

            #endregion

            #region Step 26

            serviceDeliveryRecordPage
                .ClickCloneButton();

            cloneServiceDeliveryPopup
                .WaitForCloneServiceDeliveryPopupToLoad()
                .ClickCloneButton()
                .ValidateSuccessMessageVisibility(true)
                .ClickCancelButton();

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ClickBackButton();

            serviceDeliveriesPage
                .WaitForServiceDeliveriesPageToLoad()
                .ClickRefreshButton();

            serviceDeliveries = dbHelper.serviceDelivery.GetByServiceProvisionID(serviceProvisionID);
            Assert.AreEqual(2, serviceDeliveries.Count);
            var clonnedServiceDeliveryId = serviceDeliveries.Where(c => c != serviceDeliveryId).FirstOrDefault();

            serviceDeliveriesPage
                .OpenRecord(clonnedServiceDeliveryId);

            serviceDeliveryRecordPage
                .WaitForServiceDeliveryRecordPageToLoad()
                .ValidateserviceProvisionidLinkText(serviceProvisionName.Replace("  ", " "))
                .ValidatePlannedStartTimeText("09:00")
                .ValidateTotalVisitsText("2")
                .ValidateNumberOfCarersText("5")
                .ValidateResponsibleTeamLinkText("ServiceDelivery T1")
                .ValidateRateUnitIdLinkText("Per 1 Hour \\ Hours (Whole)")
                .ValidateUnitsText("1.0000")
                .ValidateTotalUnitsText("10.0000")
                .ValidateSelectAll_NoRadioButtonChecked()
                .ValidateMonday_YesRadioButtonChecked()
                .ValidateTuesday_YesRadioButtonChecked()
                .ValidateWednesday_NoRadioButtonChecked()
                .ValidateThursday_NoRadioButtonChecked()
                .ValidateFriday_NoRadioButtonChecked()
                .ValidateSaturday_NoRadioButtonChecked()
                .ValidateSunday_NoRadioButtonChecked()
                .ValidateNoteText("Note text ...");

            #endregion

        }

        #endregion


    }
}

