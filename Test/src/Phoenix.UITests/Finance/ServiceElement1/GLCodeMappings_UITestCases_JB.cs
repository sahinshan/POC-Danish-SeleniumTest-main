using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceMappings
{
    [TestClass]
    public class GLCodeMappings_UITestCases_JB : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("GLCodeMappings BU3");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("GLCodeMappings T3", null, _businessUnitId, "907678", "GLCodeMappingsT3@careworkstempmail.com", "GLCodeMappings T3", "020 123456");

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

                #region System User GLCodeMappingsUser3

                _systemUsername = "GLCodeMappingsUser3";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "GLCodeMappings", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22808

        [TestProperty("JiraIssueID", "CDV6-23804")]
        [Description("Steps 29 to 31 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void GLCodeMappings_JB_UITestMethod001_A()
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


            #region Step 29

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Provider)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickGLCodeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(expenditureCode2).TapSearchButton().SelectResultElement(glCodeId2.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickSaveAndCloseButton();

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .TapSearchButton();

            #endregion

            #region Step 30

            //Get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Expand and Process GL Code Update Triggers")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            #endregion

            #region Step 31

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
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
                 .ValidateGLCodeFieldText(expenditureCode2);


            #endregion


        }

        [TestProperty("JiraIssueID", "CDV6-23805")]
        [Description("Steps 29, 30 and 32 from the original jira test (here we are just validating the allowance record)")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void GLCodeMappings_JB_UITestMethod001_B()
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


            #region Step 29

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Provider)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickGLCodeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(expenditureCode2).TapSearchButton().SelectResultElement(glCodeId2.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickSaveAndCloseButton();

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .TapSearchButton();

            #endregion

            #region Step 30

            //Get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Expand and Process GL Code Update Triggers")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            #endregion

            #region Step 32

            System.Threading.Thread.Sleep(2000);
            var glCode = (string)(dbHelper.cpAllowance.GetCPAllowanceByID(allowanceId, "glcode")["glcode"]);
            Assert.AreEqual(expenditureCode2, glCode);

            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22810

        [TestProperty("JiraIssueID", "CDV6-23806")]
        [Description("Steps 33 from the original jira test (here we are just validating the Fee records linked with a Provider)")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void GLCodeMappings_JB_UITestMethod002()
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
            dbHelper.approvedCareType.CreateApprovedCareType(providerid, 2, serviceElement1Id, careTypeID, true, true, 999, new DateTime(2020, 1, 1), 1, 100, 1, 100, false, _teamId);

            #endregion

            #region Carer Rate Unit

            var carerRateUnitId = dbHelper.cpCarerRateUnit.GetByName("Per Week Per Units")[0];

            #endregion

            #region carer Batch Grouping

            var carerBatchGroupingId = dbHelper.carerBatchGrouping.GetByName("Batching Not Applicable")[0];

            #endregion

            #region Fee Type

            var cpFeeTypeId = commonMethodsDB.CreateCPFeeType("Default", new DateTime(2020, 1, 1), 99, _teamId);

            #endregion

            #region Fee Setup

            int rateType = 1; //Standard
            int ruleType = 1; //Standard
            var cpFeeSetupId = commonMethodsDB.CreateCPFeeSetup(cpFeeTypeId, _teamId, new DateTime(2020, 1, 1), false, false, false,
                rateType, carerRateUnitId, 10m, true,
                ruleType, 0, 0,
                true, true, carerBatchGroupingId, false);

            #endregion

            #region Fee

            var draftStatusId = 1;
            var authorisedStatusId = 3;
            var feeId = dbHelper.cpFee.CreateCPFee(new DateTime(2023, 1, 1),
                providerid, cpFeeTypeId, serviceElement1Id,
                rateType, 10m, ruleType, true, carerRateUnitId, 1,
                draftStatusId, _teamId);

            dbHelper.cpFee.UpdateStatus(feeId, authorisedStatusId);

            #endregion



            #region Step 29

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name_Provider)
                .TapSearchButton()
                .OpenRecord(serviceElement1Id.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickGLCodeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(expenditureCode2).TapSearchButton().SelectResultElement(glCodeId2.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .ClickSaveAndCloseButton();

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .TapSearchButton();

            #endregion

            #region Step 30

            //Get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Expand and Process GL Code Update Triggers")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            #endregion

            #region Step 33

            System.Threading.Thread.Sleep(2000);
            var glCode = (string)(dbHelper.cpFee.GetByID(feeId, "glcode")["glcode"]);
            Assert.AreEqual(expenditureCode2, glCode);

            #endregion


        }

        #endregion
    }
}

