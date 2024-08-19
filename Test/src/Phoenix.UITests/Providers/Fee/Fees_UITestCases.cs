using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance
{
    [TestClass]
    public class Fees_UITestCases : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Fees BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Fees T1", null, _businessUnitId, "907678", "FeesT1@careworkstempmail.com", "Fees T1", "020 123456");

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

                #region System User FinancialAssessmentUser1

                _systemUsername = "FeesUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "Fees", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22813

        [TestProperty("JiraIssueID", "CDV6-23965")]
        [Description("Step(s) 36 from the original jira test (here we are just validating the fee record)")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void Fees_UITestMethod001()
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

            var cpFeeTypeID = commonMethodsDB.CreateCPFeeType("Default Fee T1", new DateTime(2020, 1, 1), 99, _teamId);

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


            #region step 36

            #region Update the SE1 record

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

            System.Threading.Thread.Sleep(3000);

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .TapSearchButton();

            #endregion

            #region Trigger the schedule Job

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

            #region Validate that the Fee record GL Code field is updated

            System.Threading.Thread.Sleep(2000);
            var glCode = (string)(dbHelper.cpFee.GetByID(feeId, "glcode")["glcode"]);
            Assert.AreEqual(expenditureCode2, glCode);

            #endregion

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22819

        [TestProperty("JiraIssueID", "CDV6-24006")]
        [Description("Step(s) 45 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void Fees_UpdateGLCodeManually_UITestMethod002()
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

            #region step 45

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
                .SelectRecord(feeId)
                .ClickUpdateGLCodeButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Total selected records: 1\r\nUpdated records: 1").TapCloseButton();

            feesPage
                .WaitForFeesPageToLoad()
                .OpenRecord(feeId);

            feeRecordPage
                .WaitForFeeRecordPageToLoad()
                .ValidateGLCodeFieldValue(expenditureCode2);

            #endregion

        }

        #endregion


    }
}

