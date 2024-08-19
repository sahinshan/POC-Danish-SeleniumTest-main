using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceProvisions
{
    [TestClass]
    public class ServiceProvision_UITestCases_JB : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private String _systemUsername;


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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("ServiceProvisions BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("ServiceProvisions T1", null, _businessUnitId, "907678", "ServiceProvisionsT1@careworkstempmail.com", "ServiceProvisions T1", "020 123456");

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

                #region System User ServiceProvisionsUser3

                _systemUsername = "ServiceProvisionsUser3";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ServiceProvisions", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

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

        [TestProperty("JiraIssueID", "CDV6-23968")]
        [Description("Step(s) 38 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void SP_UpdateGLCodeManually_UITestMethod001()
        {
            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code

            var expenditureCode1 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var expenditureCode2 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var glCodeId1 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode1, expenditureCode1, "1B");
            var glCodeId2 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode2, expenditureCode2, "2B");

            #endregion

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

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, glCodeId1);

            #endregion

            #region GL COde Mapping

            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, serviceElement1Id, _glCodeLocationId, false);

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

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, authorisedServiceprovisionstatusid);

            #endregion

            #region Update Service Element 1 GL Code field

            dbHelper.serviceElement1.UpdateGLCode(serviceElement1Id, glCodeId2);

            #endregion


            #region Step 38

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
                 .ValidateGLCodeFieldText(expenditureCode1) //GL Code should not be updated at this point
                 .ClickUpdateGLCodeButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("GL Code updated.").TapCloseButton();

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .ValidateGLCodeFieldText(expenditureCode2);  //After using the "GL Code updated" button the GL Code should be updated

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22815

        [TestProperty("JiraIssueID", "CDV6-23969")]
        [Description("Step(s) 39 from the original jira test ")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void AL_UpdateGLCodeManually_UITestMethod002()
        {
            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code

            var expenditureCode1 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var expenditureCode2 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var glCodeId1 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode1, expenditureCode1, "1B");
            var glCodeId2 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode2, expenditureCode2, "2B");

            #endregion

            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 2; // Carer
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, glCodeId1);

            #endregion

            #region GL Code Mapping

            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, serviceElement1Id, _glCodeLocationId, false);

            #endregion

            #region Care Type

            var careTypeID = commonMethodsDB.CreateCareType(_teamId, _businessUnitId, "Default Care Type", 99, new DateTime(2020, 1, 1));

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, careTypeID, false);

            #endregion

            #region Decision

            var decisionId = dbHelper.decision.GetByName("Approved")[0];

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 7); //creating a "Carer" provider
            dbHelper.carerApprovalDecision.CreateCarerApprovalDecision(providerid, _teamId, new DateTime(2020, 1, 1), decisionId);
            var approvedCareTypeId = dbHelper.approvedCareType.CreateApprovedCareType(providerid, 2, serviceElement1Id, careTypeID, true, true, 999, new DateTime(2020, 1, 1), 1, 100, 1, 100, false, _teamId);



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
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personID, draftServiceprovisionstatusid,
                serviceElement1Id, careTypeID,
                serviceprovisionstartreasonid, actualStartDate, actualEndDate, serviceprovisionendreasonid,
                _teamId, providerid, approvedCareTypeId, placementRoomTypeId);

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, authorisedServiceprovisionstatusid);

            #endregion

            #region Allowance Type

            var allowanceTypeId = commonMethodsDB.CreateAllowanceType("Default", 99, 99, new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Carer Rate Unit

            var carerRateUnitId = dbHelper.cpCarerRateUnit.GetByName("Per Week Per Units")[0];

            #endregion

            #region carer Batch Grouping

            var carerBatchGroupingId = dbHelper.carerBatchGrouping.GetByName("Batching Not Applicable")[0];

            #endregion

            #region Allowance Setup

            int payeeType = 1; //Carer
            int rateType = 1; //Standard
            int ruleType = 1; //Standard
            var allowglcodeupdating = false;
            commonMethodsDB.CreateAllowanceSetup(allowanceTypeId, payeeType, allowglcodeupdating, new DateTime(2020, 1, 1), false, false, rateType, 10m, carerRateUnitId, true, ruleType, 1, 99, 0, 0, true, true, _teamId, carerBatchGroupingId);

            #endregion

            #region Allowance

            var startTime = new TimeSpan(0, 5, 0);
            var endTime = new TimeSpan(23, 55, 0);
            int status = 1; //Draft
            var allowanceId = dbHelper.cpAllowance.CreateCPAllowance(providerid, _personID,
                serviceProvisionID, allowanceTypeId, _teamId,
                actualStartDate, actualEndDate, actualStartDate, startTime, actualEndDate, endTime,
                payeeType, 1, 10m, true,
                rateType, carerRateUnitId, ruleType,
                status);

            dbHelper.cpAllowance.UpdateStatus(allowanceId, 3); //Authorised

            #endregion

            #region Update Service Element 1 GL Code field

            dbHelper.serviceElement1.UpdateGLCode(serviceElement1Id, glCodeId2);

            #endregion


            #region Step 39

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
                 .NavigateToAllowancesTab();

            serviceProvisionAllowancesPage
                .WaitForServiceProvisionAllowancesPageToLoad()
                .OpenRecord(allowanceId);

            serviceProvisionAllowanceRecordPage
                .WaitForServiceProvisionAllowanceRecordPageToLoad()
                .ValidateGLCodeFieldValue(expenditureCode1)
                .ClickUpdateGLCodeButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("GL Code updated.").TapCloseButton();

            serviceProvisionAllowanceRecordPage
                .WaitForServiceProvisionAllowanceRecordPageToLoad()
                .ValidateGLCodeFieldValue(expenditureCode2);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22816

        [TestProperty("JiraIssueID", "CDV6-23997")]
        [Description("Step(s) 36 from the original jira test (here we are just validating the fee record)")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void Fees_UpdateGLCodeManually_UITestMethod001()
        {
            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code

            var expenditureCode1 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var expenditureCode2 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var glCodeId1 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode1, expenditureCode1, "1B");
            var glCodeId2 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode2, expenditureCode2, "2B");

            #endregion

            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 2; // Carer
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, glCodeId1);

            #endregion

            #region GL Code Mapping

            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, serviceElement1Id, _glCodeLocationId, false);

            #endregion

            #region Care Type

            var careTypeID = commonMethodsDB.CreateCareType(_teamId, _businessUnitId, "Default Care Type", 99, new DateTime(2020, 1, 1));

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, careTypeID, false);

            #endregion

            #region Decision

            var decisionId = dbHelper.decision.GetByName("Approved")[0];

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 7); //creating a "Carer" provider
            dbHelper.carerApprovalDecision.CreateCarerApprovalDecision(providerid, _teamId, new DateTime(2020, 1, 1), decisionId);
            var approvedCareTypeId = dbHelper.approvedCareType.CreateApprovedCareType(providerid, 2, serviceElement1Id, careTypeID, true, true, 999, new DateTime(2020, 1, 1), 1, 100, 1, 100, false, _teamId);

            #endregion

            #region Fee Type

            var cpFeeTypeID = commonMethodsDB.CreateCPFeeType("Default Fee T2", new DateTime(2020, 1, 1), 99, _teamId);

            #endregion

            #region Carer Rate Unit

            var carerRateUnitId = dbHelper.cpCarerRateUnit.GetByName("Per Week Per Units")[0];

            #endregion

            #region carer Batch Grouping

            var carerBatchGroupingId = dbHelper.carerBatchGrouping.GetByName("Batching Not Applicable")[0];

            #endregion

            #region Fee Setup

            int ratetypeid = 1; //Standard
            int ruletypeid = 1; //Standard
            commonMethodsDB.CreateCPFeeSetup(cpFeeTypeID, _teamId, new DateTime(2020, 1, 1), false, false, false,
                ratetypeid, carerRateUnitId, 10m, true,
                ruletypeid, 0, 0,
                true, true, carerBatchGroupingId, false);

            #endregion

            #region Fee

            int draftStatusId = 1;//Draft
            var authorisedStatusId = 3;
            var feeId = dbHelper.cpFee.CreateCPFee(new DateTime(2020, 1, 1),
                providerid, cpFeeTypeID, serviceElement1Id,
                ratetypeid, 10m, ruletypeid, true, carerRateUnitId, 1,
                draftStatusId, _teamId);

            dbHelper.cpFee.UpdateStatus(feeId, authorisedStatusId);

            #endregion

            #region Update Service Element 1 GL Code field

            dbHelper.serviceElement1.UpdateGLCode(serviceElement1Id, glCodeId2);

            #endregion

            #region step 40

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(providerName)
                .OpenProviderRecord(providerid.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .NavigateToFeesPage();

            feesPage
                .WaitForFeesPageToLoad()
                .OpenRecord(feeId);

            feeRecordPage
                .WaitForFeeRecordPageToLoad()
                .ValidateGLCodeFieldValue(expenditureCode1)
                .ClickUpdateGLCodeButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("GL Code updated.").TapCloseButton();

            feeRecordPage
                .WaitForFeeRecordPageToLoad()
                .ValidateGLCodeFieldValue(expenditureCode2);


            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22817

        [TestProperty("JiraIssueID", "CDV6-24001")]
        [Description("Step(s) 41 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void CONT_UpdateGLCodeManually_UITestMethod001()
        {
            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code

            var expenditureCode1 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var expenditureCode2 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var glCodeId1 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode1, expenditureCode1, "1B");
            var glCodeId2 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode2, expenditureCode2, "2B");

            #endregion

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

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, glCodeId1);

            #endregion

            #region GL COde Mapping

            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, serviceElement1Id, _glCodeLocationId, false);

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

            var _firstName = "Oliver";
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

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, authorisedServiceprovisionstatusid);

            #endregion

            #region Charging Rule Type

            var ChargingRuleTypeID = commonMethodsDB.CreateChargingRuleType("Default CR1", _teamId, new DateTime(2000, 1, 1));

            #endregion

            #region Charging Rule Setup

            var chargingRuleSetupId = commonMethodsDB.CreateChargingRuleSetup(_teamId, ChargingRuleTypeID, 3, new DateTime(2020, 1, 1),
                1, 99,
                100m, 200m, 60m, 120m,
                50, 2);

            #endregion

            #region Schedule Types

            var scheduleTypeId = dbHelper.financeScheduleType.GetByName("Residential")[0];

            #endregion

            #region Schedule Setup

            var scheduleSetupId = commonMethodsDB.CreateScheduleSetup(_teamId, ChargingRuleTypeID, scheduleTypeId, new DateTime(2020, 1, 1), null, 1, false, false, 0, 100, true, true, false, false);

            #endregion

            #region Charge for Services Setup

            var chargeforServicesSetupId = commonMethodsDB.CreateChargeforServicesSetup(_teamId, ChargingRuleTypeID, serviceElement1Id, serviceElement2Id, null, validRateUnits[0], new DateTime(2020, 1, 1));

            #endregion

            #region Income Support Type

            var incomeSupportTypeId = dbHelper.incomeSupportType.GetByName("Appropriate Min Guarantee (Sev Dis [1])")[0];

            #endregion

            #region Income Support Setup

            var incomeSupportSetupId = commonMethodsDB.CreateIncomeSupportSetup(_teamId, incomeSupportTypeId, new DateTime(2020, 1, 1), null,
                1, 99, 100m, 200m, 60m, 120m);

            #endregion

            #region Income Support Type -- Charging Rule Type

            commonMethodsDB.CreateIncomeSupportTypeChargingRuleType(incomeSupportTypeId, ChargingRuleTypeID);

            #endregion

            #region Financial Assessment Type

            var financialAssessmentTypeID = dbHelper.financialAssessmentType.GetByName("Full Assessment")[0];

            #endregion

            #region Financial Assessment Status

            var financialAssessmentStatusId = dbHelper.financialAssessmentStatus.GetFinancialAssessmentStatusByName("Draft")[0];

            #endregion

            #region Financial Assessment

            var financialAssessmentId = dbHelper.financialAssessment.CreateFinancialAssessment(
                _personID, financialAssessmentStatusId, _systemUserId, _teamId,
                actualStartDate, actualEndDate,
                ChargingRuleTypeID, incomeSupportTypeId, financialAssessmentTypeID, 0, 150m);

            #endregion

            #region Contribution Type

            var contributionTypeId = dbHelper.contributionType.GetByName("Person Charge")[0];

            #endregion

            #region Recovery Method

            var recoveryMethodId = dbHelper.recoveryMethod.GetByName("Client Monies")[0];

            #endregion

            #region Debtor Batch Groupings

            var debtorBatchGroupingId = dbHelper.debtorBatchGrouping.GetByName("Batching Not Applicable")[0];

            #endregion

            #region Contribution

            var faContributionId = dbHelper.faContribution.CreateFAContribution(
                financialAssessmentId, serviceProvisionID, _personID, _teamId, contributionTypeId, recoveryMethodId,
                debtorBatchGroupingId, _personID, "person", _firstName + " " + _lastName, actualStartDate, actualEndDate);

            #endregion

            #region Update Service Element 1 GL Code field

            dbHelper.serviceElement1.UpdateGLCode(serviceElement1Id, glCodeId2);

            #endregion

            #region Validate GL Code for Contribution

            var faContributionGLCode = (string)(dbHelper.faContribution.GetByID(faContributionId, "glcode")["glcode"]);
            Assert.AreEqual("1B", faContributionGLCode);

            #endregion

            #region Step 41

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
                 .ValidateGLCodeFieldText(expenditureCode1)
                 .ClickUpdateGLCodeButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("GL Code updated.").TapCloseButton();

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .ValidateGLCodeFieldText(expenditureCode2);

            #region Validate GL Code for Contribution

            faContributionGLCode = (string)(dbHelper.faContribution.GetByID(faContributionId, "glcode")["glcode"]);
            Assert.AreEqual("2B", faContributionGLCode);

            #endregion

            #endregion





        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-23215

        [TestProperty("JiraIssueID", "CDV6-24004")]
        [Description("Step(s) 43 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void SP_UpdateGLCodeManually_UITestMethod002()
        {
            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code

            var expenditureCode1 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var expenditureCode2 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var glCodeId1 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode1, expenditureCode1, "1B");
            var glCodeId2 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode2, expenditureCode2, "2B");

            #endregion

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

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, glCodeId1);

            #endregion

            #region GL COde Mapping

            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, serviceElement1Id, _glCodeLocationId, false);

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

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, authorisedServiceprovisionstatusid);

            #endregion

            #region Update Service Element 1 GL Code field

            dbHelper.serviceElement1.UpdateGLCode(serviceElement1Id, glCodeId2);

            #endregion


            #region Step 43

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
                .SelectRecord(serviceProvisionID.ToString())
                .ClickUpdateGLCodeButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Total selected records: 1\r\nUpdated records: 1").TapCloseButton();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionID.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .ValidateGLCodeFieldText(expenditureCode2);  //After using the "GL Code updated" button the GL Code should be updated

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22818

        [TestProperty("JiraIssueID", "CDV6-24005")]
        [Description("Step(s) 44 from the original jira test ")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void AL_UpdateGLCodeManually_UITestMethod003()
        {
            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code

            var expenditureCode1 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var expenditureCode2 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var glCodeId1 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode1, expenditureCode1, "1B");
            var glCodeId2 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode2, expenditureCode2, "2B");

            #endregion

            #region Service Element 1

            string partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement1Name_Provider = "SE1_" + partialDateTimeSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid_Provider = 2; // Carer
            var paymentscommenceid_Actual = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, glCodeId1);

            #endregion

            #region GL Code Mapping

            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, serviceElement1Id, _glCodeLocationId, false);

            #endregion

            #region Care Type

            var careTypeID = commonMethodsDB.CreateCareType(_teamId, _businessUnitId, "Default Care Type", 99, new DateTime(2020, 1, 1));

            #endregion

            #region Service Mapping

            dbHelper.serviceMapping.CreateServiceMapping(_teamId, serviceElement1Id, careTypeID, false);

            #endregion

            #region Decision

            var decisionId = dbHelper.decision.GetByName("Approved")[0];

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 7); //creating a "Carer" provider
            dbHelper.carerApprovalDecision.CreateCarerApprovalDecision(providerid, _teamId, new DateTime(2020, 1, 1), decisionId);
            var approvedCareTypeId = dbHelper.approvedCareType.CreateApprovedCareType(providerid, 2, serviceElement1Id, careTypeID, true, true, 999, new DateTime(2020, 1, 1), 1, 100, 1, 100, false, _teamId);



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
            var serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personID, draftServiceprovisionstatusid,
                serviceElement1Id, careTypeID,
                serviceprovisionstartreasonid, actualStartDate, actualEndDate, serviceprovisionendreasonid,
                _teamId, providerid, approvedCareTypeId, placementRoomTypeId);

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, authorisedServiceprovisionstatusid);

            #endregion

            #region Allowance Type

            var allowanceTypeId = commonMethodsDB.CreateAllowanceType("Default", 99, 99, new DateTime(2020, 1, 1), _teamId);

            #endregion

            #region Carer Rate Unit

            var carerRateUnitId = dbHelper.cpCarerRateUnit.GetByName("Per Week Per Units")[0];

            #endregion

            #region carer Batch Grouping

            var carerBatchGroupingId = dbHelper.carerBatchGrouping.GetByName("Batching Not Applicable")[0];

            #endregion

            #region Allowance Setup

            int payeeType = 1; //Carer
            int rateType = 1; //Standard
            int ruleType = 1; //Standard
            var allowglcodeupdating = false;
            commonMethodsDB.CreateAllowanceSetup(allowanceTypeId, payeeType, allowglcodeupdating, new DateTime(2020, 1, 1), false, false, rateType, 10m, carerRateUnitId, true, ruleType, 1, 99, 0, 0, true, true, _teamId, carerBatchGroupingId);

            #endregion

            #region Allowance

            var startTime = new TimeSpan(0, 5, 0);
            var endTime = new TimeSpan(23, 55, 0);
            int status = 1; //Draft
            var allowanceId = dbHelper.cpAllowance.CreateCPAllowance(providerid, _personID,
                serviceProvisionID, allowanceTypeId, _teamId,
                actualStartDate, actualEndDate, actualStartDate, startTime, actualEndDate, endTime,
                payeeType, 1, 10m, true,
                rateType, carerRateUnitId, ruleType,
                status);

            dbHelper.cpAllowance.UpdateStatus(allowanceId, 3); //Authorised

            #endregion

            #region Update Service Element 1 GL Code field

            dbHelper.serviceElement1.UpdateGLCode(serviceElement1Id, glCodeId2);

            #endregion


            #region Step 44

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
                 .NavigateToAllowancesTab();

            serviceProvisionAllowancesPage
                .WaitForServiceProvisionAllowancesPageToLoad()
                .SelectRecord(allowanceId)
                .ClickUpdateGLCodeButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Total selected records: 1\r\nUpdated records: 1").TapCloseButton();

            serviceProvisionAllowancesPage
                .WaitForServiceProvisionAllowancesPageToLoad()
                .OpenRecord(allowanceId);

            serviceProvisionAllowanceRecordPage
                .WaitForServiceProvisionAllowanceRecordPageToLoad()
                .ValidateGLCodeFieldValue(expenditureCode2);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-23214

        [TestProperty("JiraIssueID", "CDV6-24008")]
        [Description("Step(s) 47 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void SP_CostsPerWeek_UITestMethod001()
        {
            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code

            var expenditureCode1 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var expenditureCode2 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var glCodeId1 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode1, expenditureCode1, "1B");
            var glCodeId2 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode2, expenditureCode2, "2B");

            #endregion

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

            var serviceElement1Id = commonMethodsDB.CreateServiceElement1(serviceElement1Name_Provider, _teamId, startDate, code, whotopayid_Provider, paymentscommenceid_Actual, validRateUnits, defaulRateUnitID, glCodeId1);

            #endregion

            #region GL COde Mapping

            dbHelper.glCodeMapping.CreateGLCodeMapping(_teamId, serviceElement1Id, _glCodeLocationId, false);

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

            dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, authorisedServiceprovisionstatusid);

            #endregion


            #region Step 47

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
                 .NavigateToCostsPerWeekTab();

            serviceProvisionCostPerWeekPage
                .WaitForServiceProvisionCostsPerWeekPageToLoad();

            #endregion

        }

        #endregion
    }
}

