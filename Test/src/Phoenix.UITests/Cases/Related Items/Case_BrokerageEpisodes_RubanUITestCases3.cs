using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases.Related_Items
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Brokerage_ObjectMapping_4.Zip"), DeploymentItem("Files\\Brokerage Episode Request date new.Zip")]
    [DeploymentItem("Files\\Brokerage_ObjectMapping_4 - BrokerageEpisode.Zip")]
    public class Case_BrokerageEpisodes_RubanUITestCases3 : FunctionalTest
    {
        private string _tenantName;
        private Guid _ethnicityId;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private Guid _businessUnitId;
        private Guid teamID;
        private string _teamName;
        private Guid systemUserId;
        private Guid _documentId;
        private string _systemUserName;
        private Guid _contactReasonId;
        private Guid personID;
        private string _personFirstName;
        private string _personLastName;
        private string _personFullName;
        private Guid _caseStatusId;
        private Guid dataFormId;
        private Guid caseID;
        private string caseNumber;
        private Guid episodeId1;
        private Guid episodeId2;
        private Guid sourceOfBrokerageRequestsID;
        private Guid sourceOfBrokerageRequestsID2;
        private Guid brokerageEpisodePriorityID;
        private int contactType = 1; // Spot
        private int status = 1; // new


        [TestInitialize]
        public void TestInitializationMethod()
        {
            try
            {

                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _teamName = "CareDirector QA";
                teamID = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region System User

                _systemUserName = "Case_Brokerage_Episodes_User1";
                systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseBrokerageEpisodes", "User1", "Passw0rd_!", _businessUnitId, teamID, _languageId, _authenticationproviderid);

                #endregion

                #region Ethnicity

                if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                    dbHelper.ethnicity.CreateEthnicity(teamID, "Irish", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                #endregion

                #region Person

                _personFirstName = "Brokerage Episode";
                _personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
                _personFullName = _personFirstName + " " + _personLastName;
                personID = commonMethodsDB.CreatePersonRecord(_personFirstName, _personLastName, _ethnicityId, teamID);

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", teamID);

                #endregion

                #region Data Form

                dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Case

                caseID = dbHelper.Case.CreateSocialCareCaseRecord(teamID, personID, systemUserId, systemUserId, _caseStatusId, _contactReasonId, dataFormId, null, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
                caseNumber = (string)dbHelper.Case.GetCaseByID(caseID, "casenumber")["casenumber"];

                #endregion

                #region Source of Brokerage Requests

                sourceOfBrokerageRequestsID = dbHelper.brokerageRequestSource.GetByName("Community Hospital")[0];

                #endregion

                #region Source of Brokerage Requests 2

                sourceOfBrokerageRequestsID2 = dbHelper.brokerageRequestSource.GetByName("Acute Hospital")[0];

                #endregion

                #region Brokerage Episode Priority

                if (!dbHelper.brokerageEpisodePriority.GetByName("Priority").Any())
                    dbHelper.brokerageEpisodePriority.CreateBrokerageEpisodePriority(new Guid("8a5baf95-fc3d-eb11-a2e5-0050569231cf"), "Priority", new DateTime(2021, 1, 1), teamID);
                brokerageEpisodePriorityID = dbHelper.brokerageEpisodePriority.GetByName("Priority")[0];

                #endregion                              

                #region Web API auth system user

                commonMethodsDB.CreateSystemUserRecord("webapiauthuser", "webapi", "authuser", "Passw0rd_!", _businessUnitId, teamID, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13473

        //This Script contains Test Batch 4  (from step 38-to step 54)

        [TestProperty("JiraIssueID", "CDV6-13510")]
        [Description("Open active social care case and then Navigate to case form Create New Record With Status In progress" +
                     "Update the Case Form Status To completed" +
                     "Navigate to Brokerage Episode " +
                     "Validate Brokerage Episode Record is Created with Status New And Request Received Date Field with Completion Date of Case Form Record ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod1()
        {
            #region Document
            string documentName = "Brokerage_ObjectMapping_4";
            _documentId = commonMethodsDB.CreateDocumentIfNeeded(documentName, "Brokerage_ObjectMapping_4.Zip");
            commonMethodsDB.CreateDocumentBusinessObjectMapping("Brokerage_ObjectMapping_4 - BrokerageEpisode", "Brokerage_ObjectMapping_4 - BrokerageEpisode.Zip");

            #endregion

            #region Workflow Brokerage Episode Request date new
            var workFlowId = commonMethodsDB.CreateWorkflowIfNeeded("Brokerage Episode Request date new", "Brokerage Episode Request date new.Zip");
            #endregion

            loginPage
              .GoToLoginPage()
              .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Brokerage_ObjectMapping_4")
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .TapSaveAndCloseButton(); System.Threading.Thread.Sleep(3000);

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            //validating Brokerage Episode Record is created
            var caseFormRecordId = dbHelper.caseForm.GetCaseFormByCaseID(caseID).First();

            caseCasesFormPage
                .OpenRecord(caseFormRecordId.ToString());


            caseFormPage
                .WaitForCaseFormPageToLoad()
                .ValidateStatusField("In Progress")
                .SelectStatus("Complete")
                .TapSaveAndCloseButton(); System.Threading.Thread.Sleep(3000);

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            //get all "Not Started" workflow jobs
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workFlowId, 1).FirstOrDefault();

            //authenticate against the v6 Web API
            //this.WebAPIHelper.Security.Authenticate();
            WebAPIHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");

            //execute the Workflow Job and wait for the Idle status
            WebAPIHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            //validating Brokerage Episode Record is created
            var BrokerageEpisodeRecords = dbHelper.brokerageEpisode.GetBrokerageEpisodeByCaseID(caseID);
            Assert.AreEqual(1, BrokerageEpisodeRecords.Count);

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .ClickSearchButton()
                .OpenRecord(BrokerageEpisodeRecords[0].ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ValidateRequestReceivedDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00");



        }


        [TestProperty("JiraIssueID", "CDV6-13511")]
        [Description("Open active social care case and then Navigate to case form Create New Recorrd With Status Inprogress" +
                     "Update the Case Form Status To completed" +
                     "Navigate to Brokerage Episode " +
                     "Validate Brokerage Episode Record is Created with Status New And Request Recieved Date Field is Modified on Date of Case Form Record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod2()
        {

            #region Document
            string documentName = "Brokerage_ObjectMapping_4";
            _documentId = commonMethodsDB.CreateDocumentIfNeeded(documentName, "Brokerage_ObjectMapping_4.Zip");
            commonMethodsDB.CreateDocumentBusinessObjectMapping("Brokerage_ObjectMapping_4 - BrokerageEpisode", "Brokerage_ObjectMapping_4 - BrokerageEpisode.Zip");

            #endregion

            #region Workflow Brokerage Episode Request date new
            var workFlowId = commonMethodsDB.CreateWorkflowIfNeeded("Brokerage Episode Request date new", "Brokerage Episode Request date new.Zip");
            #endregion

            loginPage
              .GoToLoginPage()
              .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .TapNewButton();

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .TapFormTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Brokerage_ObjectMapping_4")
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .InsertStartDate("16/09/2021")
                .TapSaveAndCloseButton(); System.Threading.Thread.Sleep(3000);

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            //validating Brokerage Episode Record is created
            var caseFormRecordId = dbHelper.caseForm.GetCaseFormByCaseID(caseID).FirstOrDefault();

            caseCasesFormPage
                .OpenRecord(caseFormRecordId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .ValidateStatusField("In Progress")
                .SelectStatus("Complete")
                .InsertCompletionDate("24/10/2021")
                .TapSaveAndCloseButton(); System.Threading.Thread.Sleep(3000);

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            //get all "Not Started" workflow jobs
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workFlowId, 1).FirstOrDefault();

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate(_tenantName, "webapiauthuser", "Passw0rd_!");

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);

            //validating Brokerage Episode Record is created
            var BrokerageEpisodeRecords = dbHelper.brokerageEpisode.GetBrokerageEpisodeByCaseID(caseID);
            Assert.AreEqual(1, BrokerageEpisodeRecords.Count);

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .ClickSearchButton()
                .OpenRecord(BrokerageEpisodeRecords[0].ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ValidateRequestReceivedDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00");



        }


        [TestProperty("JiraIssueID", "CDV6-13512")]
        [Description("Open active social care case and Brokerage Episode Record" +
                    "Navigate to Service Provisions page" +
                    "Tap New Record Button" + "Validate Brokerage Episode Field And Brokerage Offer Fields are Visible under Related Infermation" +
                    "Validate Brokeage Episode is auto populated with Episode name and Brokerage Offer Lookup is Disabled" +
                    " By Insert Service Element 1 as(23jun20)" + "Validate Service Element2 ,GL Code and Rate Unit Fields Displayed" + "Validate the Rate Unit Value IS auto Populated" +
                    "Insert Service Element 2 and Start Reason Field and tap Save Button" +
                    "Validate Error Message is displayed('A planned or actual start date must be specified.')" +
                    "Insert Planned Start Date and Then Tap Save Button" +
                    "Validate Service Provision Record is Created and Then Navigate to Case Timeline Tab" +
                    "Validate the Service Provision Record Creation Status is Displayed" +
                    "Navigate to Service Provision Update the Actual Start Date Field And then Tap Save Button" +
                    "Validate that Record is Updated and Navigate Case Timline Tab" +
                    "Validate Service Provision Update Status is Displayed")]

        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod3()
        {
            var sourceDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            #region Service Element 1

            var serviceElement1Name = "CDV6_13512_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, teamID, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2

            var serviceElement2Name = "CDV6_13512_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            var serviceMappingA = commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13512").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13512", new DateTime(2022, 1, 1));
            var startReason = dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13512")[0];

            #endregion

            //Creating New Brokerage Episode
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, sourceDate, sourceDate, 1, 0, 0, contactType, false, false, false, false, true, true, false);

            loginPage
              .GoToLoginPage()
              .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToServiceProvisionSubPage();

            personServiceProvisionsPage
                .WaitForBrokerageServiceProvisionsPageToLoad()
                .TapNewButton();


            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ValidateBrokerageEpisodeFieldLinkVisibility(true)
                .ValidateBrokerageEpisodeFieldText("Brokerage episode for " + _personFullName + " received on ")
                .ValidateBrokerageOfferFieldLabelVisibility(true)
                .ValidateBrokerageOfferFieldLookupEnabled(false);

            /************************Step2**************/
            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ValidateServiceElement2FieldLabelVisibility(false)
                .ValidateGlCodeFieldLabelVisibility(false)
                .ValidateRateUnitFieldLabelVisibility(false)
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement1.ToString());


            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ValidateServiceElement2FieldLabelVisibility(true)
                .ValidateGlCodeFieldLabelVisibility(true)
                .ValidateRateUnitFieldLabelVisibility(true)
                .ValidateRateUnitFieldText(@"Per Day \ Days");
            /*******************************************/

            /************************Step3**************/
            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement2Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement2.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickStartReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("StartReason_CDV6_13512")
                .TapSearchButton()
                .SelectResultElement(startReason.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickSaveAndCloseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("A planned or actual start date must be specified.")
                .TapOKButton();

            /*******************************************/

            /************************Step4**************/
            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .InsertPlannedStartDate(sourceDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personServiceProvisionsPage
               .WaitForBrokerageServiceProvisionsPageToLoad();

            //validating Service Provision Record is created
            System.Threading.Thread.Sleep(2000);
            var ServiceProvisioRecords = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, ServiceProvisioRecords.Count);

            /********************************************/

            /************************Step5**************/

            mainMenu
              .WaitForMainMenuToLoad()
              .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForTimelinePageToLoad()
                .ValidateRecordPresent(ServiceProvisioRecords[0].ToString());

            /*******************************************/
            /************************Step6**************/
            mainMenu
             .WaitForMainMenuToLoad()
             .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToServiceProvisionSubPage();

            personServiceProvisionsPage
                .WaitForBrokerageServiceProvisionsPageToLoad()
                .OpenRecord(ServiceProvisioRecords[0].ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .InsertActualStartDate(sourceDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personServiceProvisionsPage
               .WaitForBrokerageServiceProvisionsPageToLoad();

            //validating Service Provision Record is created
            var ServiceProvisionUpdatedRecords = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, ServiceProvisionUpdatedRecords.Count);

            personServiceProvisionsPage
                .WaitForBrokerageServiceProvisionsPageToLoad()
                .OpenRecord(ServiceProvisionUpdatedRecords[0].ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ValidateActualStartDateFieldText(sourceDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            /********************************************/

            /************************Step7**************/

            mainMenu
              .WaitForMainMenuToLoad()
              .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForTimelinePageToLoad()
                .ValidateRecordPresent(ServiceProvisionUpdatedRecords[0].ToString());



        }


        [TestProperty("JiraIssueID", "CDV6-13513")]
        [Description("Open active social care case and Brokerage Episode Record" +
                   "Navigate to Service Provisions page" +
                   "Created First Record with DB Helper" + "Trying to Create Second Record " +
                   "Validate User is able to Create the Second Record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod4()
        {
            var sourceDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            #region Service Element 1

            var serviceElement1Name = "CDV6_13513_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, teamID, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2

            var serviceElement2Name = "CDV6_13513_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13513").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13513", new DateTime(2022, 1, 1));
            var startReason = dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13513")[0];

            #endregion

            #region Placement Room Type
            var placementRoomTypeExist = dbHelper.placementRoomType.GetPlacementRoomTypeByName("CDV6-13513_RoomType").Any();
            if (!placementRoomTypeExist)
                dbHelper.placementRoomType.CreatePlacementRoomType(teamID, _businessUnitId, "CDV6-13513_RoomType", new DateTime(2020, 1, 1));
            var placementroomtypeid = dbHelper.placementRoomType.GetPlacementRoomTypeByName("CDV6-13513_RoomType")[0];

            #endregion

            #region GL Code

            Guid serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft").FirstOrDefault();
            var glCodeLocation_SpecialSchemeType = dbHelper.glCodeLocation.GetByName("Special Scheme Type").FirstOrDefault();

            var glCodeExist = dbHelper.glCode.GetByDescription("Special Scheme").Any();
            if (!glCodeExist)
                dbHelper.glCode.CreateGLCode(teamID, glCodeLocation_SpecialSchemeType, "Special Scheme", "123", "123", false);
            var glCodeId = dbHelper.glCode.GetByDescription("Special Scheme").FirstOrDefault();
            #endregion

            //Creating New Brokerage Episode
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, sourceDate, sourceDate, 1, 0, 0, contactType, false, false, false, false, true, true, false);

            //Creating New Service Provision
            dbHelper.serviceProvision.CreateServiceProvisionForBrokerageEpisode(teamID, systemUserId, personID, serviceprovisionstatusid, serviceElement1, serviceElement2,
                null, glCodeId, defaulRateUnitID, startReason, null, teamID, null,
                null, null, placementroomtypeid, sourceDate, null, null, episodeId);

            loginPage
              .GoToLoginPage()
              .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToServiceProvisionSubPage();

            personServiceProvisionsPage
                .WaitForBrokerageServiceProvisionsPageToLoad()
                .TapNewButton();


            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement1.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement2Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement2.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickStartReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("StartReason_CDV6_13513")
                .TapSearchButton()
                .SelectResultElement(startReason.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .InsertPlannedStartDate(sourceDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personServiceProvisionsPage
               .WaitForBrokerageServiceProvisionsPageToLoad();

            //validating Service Provision Record is created
            System.Threading.Thread.Sleep(2000);
            var ServiceProvisioRecords = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(2, ServiceProvisioRecords.Count);



        }


        [TestProperty("JiraIssueID", "CDV6-13606")]
        [Description("Open active social care case and Brokerage Episode Record With Status Completed and Cancelled" +
                   "Navigate to Service Provisions page" +
                   "Validate New Record Button is Not Displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod5()
        {

            #region Provider
            //var providerID = new Guid("2883cac1-93a4-e911-a2c6-005056926fe4");//Work test
            string providerName = "CDV6-13606" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");
            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13606").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13606", new DateTime(2022, 1, 1), true);

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13606_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, teamID, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2            
            var serviceElement2Name = "CDV6_13606_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Rate Unit            
            var rateunitid = dbHelper.rateUnit.GetByName("Per Day").FirstOrDefault();
            #endregion

            #region Service Provided            
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
                serviceElement1, serviceElement2, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode
            var sourcedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            //Creating Completed Record For Brokerage Episode
            var episodeIdWithStatusCompleted = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusCompleted, 3); //update status to Sourcing in Progress
            var underReviewId1 = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeIdWithStatusCompleted, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(underReviewId1, 5, sourcedDate);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeStatusApproved(episodeIdWithStatusCompleted, 6, serviceElement1, serviceElement2, DateTime.Now); //Approved
            dbHelper.brokerageOffer.UpdateBrokerageOfferApprovedStatus(underReviewId1, 6, providerID, rateunitid, serviceProvidedId);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusCompleted, 8); //Completed

            //Creating Cancelled Record For Brokerage Episode
            var episodeIdWithStatusCancelled = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusCancelled, 7); //Cancelled
            #endregion
            loginPage
              .GoToLoginPage()
              .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusCompleted.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToServiceProvisionSubPage();

            personServiceProvisionsPage
                .WaitForBrokerageServiceProvisionsPageToLoad()
                .ValidateNewRecordButtonVisibility(false);

            //Validate When Status is completed Brokerage Episode fields should be inactive.
            var Records = dbHelper.brokerageEpisode.GetBrokerageEpisodeByPersonID(personID);
            var disabledRecords = dbHelper.brokerageEpisode.GetBrokerageEpisodeByID(Records[0], "inactive");
            Assert.AreEqual(true, disabledRecords["inactive"]);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusCancelled.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToServiceProvisionSubPage();

            personServiceProvisionsPage
                .WaitForBrokerageServiceProvisionsPageToLoad()
                .ValidateNewRecordButtonVisibility(false);
        }


        [TestProperty("JiraIssueID", "CDV6-13607")]
        [Description("Open active social care case and Brokerage Episode Record With Status Awaiting Commencement" +
                  "Navigate to Service Provisions page" +
                  "Validate Service Provision Record is Auto Created" +
                  "Validating Brokerage Episode and Brokerage Offer Field is auto filled and lookup is disabled")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod6()
        {

            #region Provider            
            string providerName = "CDV6-13606" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");
            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13607").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13607", new DateTime(2022, 1, 1), true);

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13607_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, teamID, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2            
            var serviceElement2Name = "CDV6_13607_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Rate Unit            
            var rateUnit = dbHelper.rateUnit.GetByName("Per 1 Hour Unit").FirstOrDefault();
            #endregion

            #region Service Provided            
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
                serviceElement1, serviceElement2, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode

            var sourcedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            //To create Brokerage Episodes
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

            //To create Brokerage Offer record with under review status
            var offerId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offerId, 5, sourcedDate);//update status to Sourced            

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1, serviceElement2, DateTime.Now, null); //update  Brokerage Episode status to Approved in Progress

            //Update Brokerage Offer record to Accepted status
            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(offerId, 6, rateUnit, serviceProvidedId);
            #endregion

            loginPage
              .GoToLoginPage()
              .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            //validating Service Provision Record is created
            var ServiceProvisionRecords = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, ServiceProvisionRecords.Count);

            caseBrokerageEpisodeRecordPage
               .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
               .ClickRefreshButton()
               .ValidateStatusSelectedText("Awaiting Commencement")
               .NavigateToServiceProvisionSubPage();

            personServiceProvisionsPage
                .WaitForBrokerageServiceProvisionsPageToLoad()
                .OpenRecord(ServiceProvisionRecords[0].ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ValidateBrokerageEpisodeFieldLinkVisibility(true)
                .ValidateBrokerageEpisodeFieldText("Brokerage episode for " + _personFullName + " received on ")
                .ValidateBrokerageOfferFieldText("Brokerage Offer from " + providerName + " received on ")
                .ValidateBrokerageEpisodeFieldLookupEnabled(false)
                .ValidateBrokerageOfferFieldLookupEnabled(false);

        }


        [TestProperty("JiraIssueID", "CDV6-13608")]
        [Description("Open active social care case and Brokerage Episode Record" +
                  "Navigate to Service Provisions page" +
                  "Created service provision record" + "update Brokerage Episode Field with other Brokerage Episode and hit save option" +
                  "Validate No Service Provision Record is Displayed + Navigate to Brokerage Episode which is used in Service Provision Record" +
                  "Validate Service Provision Record Displayed ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod7()
        {

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13608").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13608", new DateTime(2022, 1, 1), true);
            var startReason = dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13608")[0];

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13608_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, teamID, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2            
            var serviceElement2Name = "CDV6_13608_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Brokerage Episode
            var sourceDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //Creating New Brokerage Episode
            episodeId1 = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, sourceDate, sourceDate, 1, 0, 0, contactType, false, false, false, false, true, true, false);

            //Creating New Brokerage Episode
            episodeId2 = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, sourceDate, sourceDate, 1, 0, 0, contactType, false, false, false, false, true, true, false);
            #endregion

            loginPage
              .GoToLoginPage()
              .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId1.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToServiceProvisionSubPage();

            personServiceProvisionsPage
                .WaitForBrokerageServiceProvisionsPageToLoad()
                .TapNewButton();


            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement1.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement2Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement2.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickStartReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("StartReason_CDV6_13608")
                .TapSearchButton()
                .SelectResultElement(startReason.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickBrokerageEpisodeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Brokerage episode for " + _personFullName + " received on")
                .TapSearchButton()
                .SelectResultElement(episodeId2.ToString());


            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .InsertPlannedStartDate(sourceDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            personServiceProvisionsPage
               .WaitForBrokerageServiceProvisionsPageToLoad()
               .ValidateNoRecordMessageVisibile(true);

            //validating Service Provision Record is not stored under episode1 when Brokerage Episode Field value changed
            System.Threading.Thread.Sleep(2000);
            var ServiceProvisioRecord1 = dbHelper.serviceProvision.GetServiceProvisionByEpisodeID(episodeId1);
            Assert.AreEqual(0, ServiceProvisioRecord1.Count);

            //Validating Service Provision Record is stored under Episode2 when Brokerage Episode Field value Changed to Episode2
            var ServiceProvisioRecord2 = dbHelper.serviceProvision.GetServiceProvisionByEpisodeID(episodeId2);
            Assert.AreEqual(1, ServiceProvisioRecord2.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId2.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToServiceProvisionSubPage();

            personServiceProvisionsPage
                .WaitForBrokerageServiceProvisionsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

        }


        [TestProperty("JiraIssueID", "CDV6-13609")]
        [Description("Open active social care case and Brokerage Episode Record" +
                  "Navigate to Service Provisions page" +
                  "Created Record without Brokerage Episode Field" +
                  "Validate No Record is displayed under Service Provision Grid" +
                  "Navigate to Finace -Service Provision " +
                  "Validate Created Service provision Record is Displayed" +
                  "open the record and update the service provision field" +
                  "Validate that Service Provision Record is Displayed under Respective Brokerage Episode")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod8()
        {
            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13609").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13609", new DateTime(2022, 1, 1), true);
            var startReason = dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13609")[0];

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13609_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault());
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, teamID, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2            
            var serviceElement2Name = "CDV6_13609_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Brokerage Episode
            var sourceDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //Creating New Brokerage Episode
            var episodeId1 = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, sourceDate, sourceDate, 1, 0, 0, contactType, false, false, false, false, true, true, false);
            #endregion

            loginPage
              .GoToLoginPage()
              .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage.
                WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId1.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToServiceProvisionSubPage();

            personServiceProvisionsPage
                .WaitForBrokerageServiceProvisionsPageToLoad()
                .TapNewButton();


            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement1.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickServiceElement2LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement2Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement2.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickStartReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("StartReason_CDV6_13609")
                .TapSearchButton()
                .SelectResultElement(startReason.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .InsertPlannedStartDate(sourceDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickBrokerageEpisodeClearLookupButton()
                .ClickSaveAndCloseButton();

            personServiceProvisionsPage
               .WaitForBrokerageServiceProvisionsPageToLoad()
               .ValidateNoRecordMessageVisibile(true);

            //validating Service Provision Record is not displayed under episode1 when Brokerage Episode Field value cleared
            System.Threading.Thread.Sleep(2000);
            var ServiceProvisioRecord1 = dbHelper.serviceProvision.GetServiceProvisionByEpisodeID(episodeId1);
            Assert.AreEqual(0, ServiceProvisioRecord1.Count);

            //Validating Service Provision Record is stored under person when Brokerage Episode Field value Cleared
            var ServiceProvisioRecord2 = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, ServiceProvisioRecord2.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(_personFirstName, _personLastName, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
               .WaitForPersonServiceProvisionsPageToLoad()
               .OpenRecord(ServiceProvisioRecord2[0].ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickBrokerageEpisodeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Brokerage episode for " + _personFullName + " received on")
               .TapSearchButton()
               .SelectResultElement(episodeId1.ToString());

            serviceProvisionRecordPage
                .WaitForNewServiceProvisionRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personServiceProvisionsPage
               .WaitForPersonServiceProvisionsPageToLoad()
               .ValidateNoRecordMessageVisibile(false);

            //validating Service Provision Record is displayed under episode1 when Brokerage Episode Field value updated with episode1
            System.Threading.Thread.Sleep(2000);
            var UpdatedServiceProvisioRecord1 = dbHelper.serviceProvision.GetServiceProvisionByEpisodeID(episodeId1);
            Assert.AreEqual(1, UpdatedServiceProvisioRecord1.Count);

            //Validating Service Provision Record is displayed under person even after Brokerage Episode Field value updated with episode1
            var UpdatedServiceProvisioRecord2 = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, UpdatedServiceProvisioRecord2.Count);


        }


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        #endregion
    }

}