using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases.Related_Items

{
    [TestClass]

    public class Case_BrokerageEpisodes_NachaUITestCases : FunctionalTest
    {
        #region Properties
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
        private int _personNumber;
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
        private int contractType = 1; // Spot
        private int status = 1; // new
        private Guid _contactSourceId;
        private Guid insufficientInformation_brokerageEpisodeRejectionReason;
        private Guid brokerageOfferRejectionReasonID;
        private Guid _brokerageOfferCancellationReasonId;
        private Guid rateUnit;
        private Guid rateUnitId2;
        private Guid serviceElement1Id;
        private Guid serviceElement2Id;

        #endregion

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

                //commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

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

                _ethnicityId = commonMethodsDB.CreateEthnicity(teamID, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Person

                _personFirstName = "Brokerage Episode";
                _personLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
                _personFullName = _personFirstName + " " + _personLastName;
                personID = commonMethodsDB.CreatePersonRecord(_personFirstName, _personLastName, _ethnicityId, teamID);
                _personNumber = (int)dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"];


                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", teamID);

                #endregion

                #region Contact Source

                _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Other", teamID);

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

                brokerageEpisodePriorityID = commonMethodsDB.CreateBrokerageEpisodePriority(new Guid("8a5baf95-fc3d-eb11-a2e5-0050569231cf"), "Priority", new DateTime(2021, 1, 1), teamID);

                #endregion                              

                #region Web API auth system user

                commonMethodsDB.CreateSystemUserRecord("webapiauthuser", "webapi", "authuser", "Passw0rd_!", _businessUnitId, teamID, _languageId, _authenticationproviderid);

                #endregion

                #region Brokerage Episode Rejection Reason
                insufficientInformation_brokerageEpisodeRejectionReason = commonMethodsDB.CreateBrokerageEpisodeRejectionReason("Insufficient or incorrect information given", new DateTime(2020, 12, 14), teamID);

                #endregion

                #region Brokerage Offer Rejection Reason

                brokerageOfferRejectionReasonID = commonMethodsDB.CreateBrokerageOfferRejectionReason("Reason_1", new DateTime(2021, 1, 6), teamID);

                #endregion

                #region Brokerage Offer Cancellation Reason

                _brokerageOfferCancellationReasonId = commonMethodsDB.CreateBrokerageOfferCancellationReason("Citizen not content with offer", new DateTime(2021, 1, 6), teamID, false, true);

                #endregion

                #region Service Provision End Reason

                var EndReasons = dbHelper.serviceProvisionEndReason.GetAll();
                foreach (Guid EndReason in EndReasons)
                {
                    dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason, false);
                }

                #endregion

                #region Service Provision Start Reason (Default Brokerage Start Reason = Yes)

                if (!dbHelper.serviceProvisionStartReason.GetDefaultBrokerageStartReason().Any())
                    commonMethodsDB.CreateServiceProvisionStartReason(teamID, _businessUnitId, "Default SPSR", new DateTime(2022, 1, 1), true);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        public Guid UpdateBrokerageEpisodeTrackingStatusScheduledJob { get { return new Guid("6300d270-5562-eb11-a312-0050569231cf"); } }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13133

        [TestProperty("JiraIssueID", "CDV6-13182")]
        [Description("Open active social care case record with Brokerage episode and Brokerage offer record with Under review status" +
            "Change the Brokerage offer record status to Sourced and enter the mandatory fields" +
            "Change the Brokerage offer record status to Accepted and enter the mandatory fields" +
            "Validate the alert message displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod01()
        {

            #region Provider - Supplier
            string providerName = "CDV6-13182" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");
            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13182").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13182", new DateTime(2022, 1, 1), false);

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13182_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
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
            var serviceElement2Name = "CDV6_13182_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Rate Unit            
            var rateUnit = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            #endregion

            #region Services Provided
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1, serviceElement2, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode
            var sourcedDate = DateTime.Now.AddDays(-3);

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress
            #endregion

            #region Brokerage Offer Record
            //To Create Brokerage Offer Record in Under Review Status
            var OfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            #endregion

            loginPage
                  .GoToLoginPage()
                  .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                  .WaitForMainMenuToLoad()
                  .NavigateToCasesSection();

            casesPage
                 .WaitForCasesPageToLoad()
                 .SearchByCaseNumber(caseNumber, caseID.ToString())
                 .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateNumberOfOffersRecieved("1")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(OfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Sourced")
                .InsertSourcedDateTime(sourcedDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "20:30")
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Accepted")
                .ClickRateUnitLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per 1 Hour Unit")
                .TapSearchButton()
                .SelectResultElement(rateUnit.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickServiceProvidedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(serviceProvidedId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Offers cannot be accepted when the Brokerage Episode status is not Approved.");



        }

        [TestProperty("JiraIssueID", "CDV6-13183")]
        [Description("Open active social care case record with Brokerage episode and Brokerage offer record with Under review status" +
            "Change the Brokerage offer record status to Sourced and enter the mandatory fields" +
            "Change the Brokerage episode status to Approved and validate the message displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod02()
        {
            #region Provider - Supplier
            string providerName = "CDV6-13183" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");
            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13183").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13183", new DateTime(2022, 1, 1), true);

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13183_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
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
            var serviceElement2Name = "CDV6_13183_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Rate Unit            
            var rateUnit = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            #endregion

            #region Services Provided
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1, serviceElement2, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode
            var plannedStartDate = DateTime.Now.AddDays(-3);
            var sourcedDate = DateTime.Now.AddDays(-3);

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage episode status to Sourcing in Progress
            #endregion

            #region Brokerage Offer Record
            //To create Brokerage Offer Record with Under review status
            var underReviewId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(underReviewId, 5, sourcedDate);//update Brokerage offer status to Sourced
            #endregion

            loginPage
                 .GoToLoginPage()
                 .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());


            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .SelectStatus("Approved")
                .ClickServiceElement1LookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement1.ToString());

            caseBrokerageEpisodeRecordPage
               .WaitForCaseBrokerageEpisodeRecordPageToLoad()
               .ClickServiceElement2LookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(serviceElement2Name)
                .TapSearchButton()
                .SelectResultElement(serviceElement2.ToString());


            caseBrokerageEpisodeRecordPage
               .WaitForCaseBrokerageEpisodeRecordPageToLoad()
               .InsertPlannedStartDate(plannedStartDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveButton()
               .WaitForCaseBrokerageEpisodeRecordPageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Are you sure this Brokerage Episode should skip the approval process?");

        }

        [TestProperty("JiraIssueID", "CDV6-13190")]
        [Description("Open active social care case form  for record with Brokerage episode and Brokerage offer record with Sourced status" +
            "Create a Brokerage offer communication record by click + button and Save and close the offer communication record" +
            "Change the Brokerage episode status to Accepted and validate the Rate Unit field and Service Provided fields are mandatory")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod03()
        {
            #region Provider - Supplier
            string providerName = "CDV6-13190" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");
            #endregion

            #region Brokerage Communication
            var brokerageCommunicationWith1ID = dbHelper.brokerageOfferAwaitingCommunicationFrom.GetByName("Commissioning").FirstOrDefault();//Commissioning - f2999817-a851-eb11-a2fe-0050569231cf
            var contactMethodId = dbHelper.contactMethod.GetByName("Any").FirstOrDefault();
            var brokerageOfferCommunicationOutcomesId = dbHelper.brokerageOfferCommunicationOutcome.GetByName("Follow-up required").FirstOrDefault();

            #endregion

            #region Brokerage and Brokerage Offer
            var sourcedDate = DateTime.Now.AddDays(-3);
            var communicationDate = DateTime.Now.AddDays(-2);

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            // To create Brokerage Offer Record with Underreview
            var offerId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offerId, 5, sourcedDate);//update Brokerage Offer status to Sourced
            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

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
               .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
               .ValidateNumberOfOffersRecieved("1")
               .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(offerId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad();

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad()
                .ClickNewRecordButton();

            caseBrokerageOfferCommunicationRecordPage
               .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
               .ValidateBrokerageOfferLinkFieldText("Brokerage Offer from " + providerName + " received on ")
               .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
               .InsertCommunicationDateTime(communicationDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "10:15")
               .ClickCommunicationWithLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Commissioning")
                .TapSearchButton()
                .SelectResultElement(brokerageCommunicationWith1ID.ToString());

            caseBrokerageOfferCommunicationRecordPage
                .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
                .ClickContactMethodLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Any")
                .TapSearchButton()
                .SelectResultElement(contactMethodId.ToString());

            caseBrokerageOfferCommunicationRecordPage
                .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
                .InsertSubject("Communication 001")
                .InsertCommunicationDetails("details ...")
                .ClickOutcomeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Follow-up required")
                .TapSearchButton()
                .SelectResultElement(brokerageOfferCommunicationOutcomesId.ToString());

            caseBrokerageOfferCommunicationRecordPage
                .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
                .ClickSaveAndCloseButton();

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad();

            caseBrokerageEpisodeOfferRecordPage
                 .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                 .SelectStatus("Accepted")
                 .ValidateRateUnitMandatoryField(true)
                 .ValidateServiceProvidedMandatoryField(true)
                 .ValidateServiceProvidedLookUp(true);

        }

        [TestProperty("JiraIssueID", "CDV6-13199")]
        [Description("Open active social care case form  for record with Brokerage episode  as Approved status and Brokerage offer record with Sourced status" +
            "Change the Brokage offer status to Accepted and Enter the Mandatory fields and save the record " +
            "Validate the + icon is disabled in Brokerage offer communication sub area" + "Create a new Brokerage Offer record by entering the Mandatory field and save the record" +
            "While saving validate the alert displayed An accepted offer already exists for this brokerage episode. No new offers can be added.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod04()
        {
            #region Provider - Supplier
            string providerName = "CDV6-13199" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");
            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13199").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13199", new DateTime(2022, 1, 1), false);

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13199_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
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
            var serviceElement2Name = "CDV6_13199_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Services Provided
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1, serviceElement2, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode

            var sourcedDate = DateTime.Now.AddDays(-3);

            //To create Brokerage Episode
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

            #endregion

            #region Brokerage Offer
            //To create Brokerage Offer Records with Under review status
            var offerId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offerId, 5, sourcedDate);//update Brokerage Offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1, serviceElement2, DateTime.Now, null); //update  Brokerage Episode status to Approved in Progress
            #endregion

            loginPage
              .GoToLoginPage()
              .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());


            caseBrokerageEpisodeRecordPage
               .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
               .ValidateNumberOfOffersRecieved("1")
               .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(offerId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Accepted")
                .InsertCostPerWeek("20")
                .ClickServiceProvidedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(serviceProvidedId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                 .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                 .ClickSaveButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad();

            var records = dbHelper.brokerageOffer.GetBrokerageOfferByPersonID(personID);
            var OfferRecord = dbHelper.brokerageOffer.GetByID(records[0], "inactive");

            Assert.AreEqual(true, OfferRecord["inactive"]);

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad()
                .ValidateNewRecordButtonVisibility(false);

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
               .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
               .ValidateNumberOfOffersRecieved("1")
               .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .InsertReceivedDateTime("01/10/2021", "10:30")
                .ClickProviderLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(providerID.ToString());


            caseBrokerageEpisodeOfferRecordPage
                 .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                 .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("An accepted offer already exists for this brokerage episode. No new offers can be added.");

        }

        [TestProperty("JiraIssueID", "CDV6-13204")]
        [Description("Open active social care case form  for record with Brokerage episode  as Approved status and Brokerage offer record with Sourced status" +
            "Create a Brokerage offer communication record" + "Change the Brokage offer status to Accepted and Enter the Mandatory fields and save the record " +
          "Update the Brokerage offer communication record" + "Validate the offer communication updated When Brokerage offer Satus is Accepted" +
           "Validate the Brokerage Episode Status is automatically changed to Awaiting Commencement")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod05()
        {

            #region Provider - Supplier
            string providerName = "CDV6-13204" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");
            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13204").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13204", new DateTime(2022, 1, 1), true);

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13204_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
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
            var serviceElement2Name = "CDV6_13204_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Services Provided
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1, serviceElement2, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Rate Unit            
            var rateUnit = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            #endregion

            #region Brokerage Communication
            var brokerageCommunicationWith1ID = dbHelper.brokerageOfferAwaitingCommunicationFrom.GetByName("Commissioning").FirstOrDefault();//Commissioning - f2999817-a851-eb11-a2fe-0050569231cf
            var contactMethodId = dbHelper.contactMethod.GetByName("Any").FirstOrDefault();
            var brokerageOfferCommunicationOutcomesId = dbHelper.brokerageOfferCommunicationOutcome.GetByName("Follow-up required").FirstOrDefault();

            #endregion

            #region Brokerage Episode and Brokerage Offers
            var sourcedDate = DateTime.Now.AddDays(-3);

            //To create Brokerage Episodes
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

            //To create Brokerage Offer record with under review status
            var offerId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offerId, 5, sourcedDate);//update status to Sourced

            //To create Brokerage offer communication record
            var communicationId = dbHelper.brokerageOfferCommunication.CreateBrokerageOfferCommunication(teamID, offerId, caseID, personID, DateTime.Now.AddDays(-4), "Subject Offer communication",
                                                                    brokerageCommunicationWith1ID, "Details...", contactMethodId, brokerageOfferCommunicationOutcomesId);

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


            caseBrokerageEpisodeRecordPage
               .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
               .ValidateNumberOfOffersRecieved("1")
               .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(offerId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusSelectedText("Accepted");


            caseBrokerageOfferCommunicationsSubArea
               .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad()
               .OpenRecord(communicationId.ToString());

            caseBrokerageOfferCommunicationRecordPage
               .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
               .InsertCommunicationDetails("Update")
               .ClickSaveAndCloseButton();

            var communications = dbHelper.brokerageOfferCommunication.GetByBrokerageOfferId(offerId);
            Assert.AreEqual(1, communications.Count);
            var newCommunicationId = communications.FirstOrDefault();

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad()
                .ValidateRecordCellText(newCommunicationId.ToString(), 4, "Update");


            caseBrokerageEpisodeOfferRecordPage
                  .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                  .ValidateStatusSelectedText("Accepted");

            caseBrokerageOfferCommunicationsSubArea
                 .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad()
                 .SelectRecord(communicationId.ToString())
                 .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad();

            communications = dbHelper.brokerageOfferCommunication.GetByBrokerageOfferId(offerId);
            Assert.AreEqual(0, communications.Count);

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
               .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
               .ValidateStatusSelectedText("Awaiting Commencement");


        }


        [TestProperty("JiraIssueID", "CDV6-13205")]
        [Description("Open active social care case form  for record with Brokerage episode  as Approved status and Brokerage offer record with Sourced status" +
         "Update the Brokerage offer to Approved status" + "Open the Brokerage episode Record and Navigate to Service provision and Validate the service provision record generated automatically")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod06()
        {
            #region Provider - Supplier
            string providerName = "CDV6-13205" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");
            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13205").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13205", new DateTime(2022, 1, 1), true);

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13205_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
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
            var serviceElement2Name = "CDV6_13205_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Services Provided
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1, serviceElement2, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Rate Unit            
            var rateUnit = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            #endregion

            #region Brokerage Episode
            //To create Brokerage Episodes
            var sourcedDate = DateTime.Now.AddDays(-3);

            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

            //To create Brokerage Offer in Under review Status
            var offerId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offerId, 5, sourcedDate);//update Brokerage Offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1, serviceElement2, DateTime.Now, null); //update  Brokerage Episode status to Approved in Progress


            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(offerId, 6, rateUnit, serviceProvidedId);//update Brokerage Offer status to Approved
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


            caseBrokerageEpisodeRecordPage
               .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
               .NavigateToServiceProvisionSubPage();

            caseBrokerageEpisodeServiceProvisionsPage
                .WaitForCaseBrokerageEpisodeServiceProvisionsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);


        }


        [TestProperty("JiraIssueID", "CDV6-13216")]
        [Description("Open active social care case form  for record with Brokerage episode record with null planned end date field and update the status to  Approved" + "Brokerage offer record with Sourced status" +
             "Update the Brokerage offer to Approved status" + "Open the Brokerage episode Record and Navigate to Service provision and Open the service provision record and Validate the End Reason field is not visible" +
            "Validate the Brokerage Episode Link field" + "Disable the Brokerage module and Navigate to Service provision" +
            "Open the record and Validate the Brokerage Episode field should not be visible")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod07()
        {

            #region Provider - Supplier
            string providerName = "CDV6-13216" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");
            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13216").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13216", new DateTime(2022, 1, 1), true);

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13216_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, teamID, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2            
            var serviceElement2Name = "CDV6_13216_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Services Provided
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1, serviceElement2, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Rate Unit            
            var rateUnit = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            #endregion

            #region Brokerage Episode
            var sourcedDate = DateTime.Now.AddDays(-3);
            //To create Brokerage Episodes
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

            //To create Brokerage Offer record with Under review Status
            var sourcedOfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(sourcedOfferId, 5, sourcedDate);//update Brokerage Offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1, serviceElement2, DateTime.Now, null); //update  Brokerage Episode status to Approved in Progress

            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(sourcedOfferId, 6, rateUnit, serviceProvidedId);//update Brokerage Offer status to Approved
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


            caseBrokerageEpisodeRecordPage
               .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
               .NavigateToServiceProvisionSubPage();

            caseBrokerageEpisodeServiceProvisionsPage
                .WaitForCaseBrokerageEpisodeServiceProvisionsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            //Service provision record Autogenerated
            var serviceProvisionRecords = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisionRecords.Count);

            caseBrokerageEpisodeServiceProvisionsPage
               .WaitForCaseBrokerageEpisodeServiceProvisionsPageToLoad()
               .OpenRecord(serviceProvisionRecords[0].ToString());


            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .ValidateEndReasonFieldVisibility(false)
                 .ValidateBrokerageEpisodeFieldLinkVisibility(true);

        }

        [TestProperty("JiraIssueID", "CDV6-13217")]
        [Description("Open active social care case form  for record with Brokerage episode record with null planned end date field and update the status to  Approved" + "Brokerage offer record with Accepted status" +
           "Validate the service Provision record created Automatically" + "Create another Episode record with Approved status and created the offer record with the Sourced status" +
            "Change the Default Brokerage start reasons to No " + "Update the Sourced offer record status to Accepted by entering the mandatory fields" + "Click on save button" +
            "Validate the error message:No default Service Provision Start Reason exists. is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod08()
        {
            #region Provider - Supplier
            var partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            string providerName = "CDV6-13217" + partialDateTimeSuffix;
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");
            #endregion

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13217_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, teamID, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2            
            var serviceElement2Name = "CDV6_13217_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Services Provided
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1, serviceElement2, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Rate Unit            
            var rateUnit = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            #endregion

            #region Service provision Start Reason
            //Service provision StartReasons Records
            var ServiceProvisionStartReasonExists = dbHelper.serviceProvisionStartReason.GetByName("Brokered Care Package_13217").Any();
            if (!ServiceProvisionStartReasonExists)
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "Brokered Care Package_13217", new DateTime(2019, 03, 29));
            var BrokeredCarePackage = dbHelper.serviceProvisionStartReason.GetByName("Brokered Care Package_13217")[0];

            ServiceProvisionStartReasonExists = dbHelper.serviceProvisionStartReason.GetByName("New Placement_13217").Any();
            if (!ServiceProvisionStartReasonExists)
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "New Placement_13217", new DateTime(2019, 01, 20));
            var NewPlacement = dbHelper.serviceProvisionStartReason.GetByName("New Placement_13217")[0];

            ServiceProvisionStartReasonExists = dbHelper.serviceProvisionStartReason.GetByName("Service Provision Start Reason Test_13217").Any();

            if (!ServiceProvisionStartReasonExists)
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "Service Provision Start Reason Test_13217", new DateTime(2021, 01, 01));
            var ServiceProvisionStartReasonTest = dbHelper.serviceProvisionStartReason.GetByName("Service Provision Start Reason Test_13217")[0];

            //Change All Default Brokerage start reasons to "Yes"
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(BrokeredCarePackage, true);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(NewPlacement, true);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(ServiceProvisionStartReasonTest, true);

            #endregion

            #region Service Provision Start Reason (Default Brokerage Start Reason = Yes)

            if (!dbHelper.serviceProvisionEndReason.GetDefaultBrokerageEndReason().Any())
            {
                var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();
                commonMethodsDB.CreateServiceProvisionEndReason(teamID, "SPER " + currentDateTime, new DateTime(2022, 1, 1), true);
            }

            #endregion

            #region Brokerage and Brokerage Offer
            //To create Brokerage Episode
            var sourcedDate = DateTime.Now.AddDays(-3);
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

            //To create Brokerage Offer Record in Under review status
            var offerId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offerId, 5, sourcedDate);//update Brokerage Offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1, serviceElement2, DateTime.Now, null); //update Brokerage Episode status to Approved in Progress

            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(offerId, 6, rateUnit, serviceProvidedId);//update Brokerage Offer status to Approved

            //Auto generated Service Provision Record
            var serviceProvisionRecords = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisionRecords.Count);

            //To create Brokerage Episode 1
            var episode1Id = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episode1Id, 3); //Updated Brokerage Episode 1 to Sourcing in progress

            //To create Brokerage offer 1 record with Under review status
            var offer1Id = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episode1Id, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offer1Id, 5, sourcedDate);//update Brokerage offer 1 status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episode1Id, 6, serviceElement1, serviceElement2, DateTime.Now, DateTime.Now); //update Brokerage Episode 1 status to Approved in Progress

            //Change All Default Brokerage start reason to "No"
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(BrokeredCarePackage, false);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(NewPlacement, false);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(ServiceProvisionStartReasonTest, false);

            #endregion

            foreach (var serviceProvisionStartReasonId in dbHelper.serviceProvisionStartReason.GetDefaultBrokerageStartReason())
            {
                dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(serviceProvisionStartReasonId, false);
            }

            loginPage
                 .GoToLoginPage()
                 .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                  .WaitForMainMenuToLoad()
                  .NavigateToCasesSection();

            casesPage
                   .WaitForCasesPageToLoad()
                   .SearchByCaseNumber(caseNumber, caseID.ToString())
                   .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                    .WaitForCaseRecordPageToLoad()
                    .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                    .WaitForCaseBrokerageEpisodesPageToLoad()
                    .OpenRecord(episode1Id.ToString());

            caseBrokerageEpisodeRecordPage
                    .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                    .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                    .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                    .OpenRecord(offer1Id.ToString());

            caseBrokerageEpisodeOfferRecordPage
                    .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                    .SelectStatus("Accepted")
                    .ClickServiceProvidedLookUpButton();

            lookupPopup
                   .WaitForLookupPopupToLoad()
                   .TypeSearchQuery(providerName)
                   .TapSearchButton()
                   .SelectResultElement(serviceProvidedId.ToString());


            caseBrokerageEpisodeOfferRecordPage
                 .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                 .ClickSaveButton();


            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("No default Service Provision Start Reason exists.").TapCloseButton();


            //Change Default Brokerage start reason to "Yes"
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(BrokeredCarePackage, true);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(NewPlacement, true);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(ServiceProvisionStartReasonTest, true);



        }

        [TestProperty("JiraIssueID", "CDV6-13232")]
        [Description("Open active social care case form  for record with Brokerage episode record with null planned end date field and update the status to  Approved" + "Brokerage offer record with Accepted status" +
           "Validate the service Provision record created Automatically" + "Create another Episode record with Approved status and created the offer record with the Sourced status" +
            "Change the Default Brokerage start reasons to Yes(Each time change one of the start reason record status to Yes) " + "Update the Sourced offer record status to Accepted by entering the mandatory fields" + "Click on save button" +
            "Validate the Start reason field is displaying the enabled start reason ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod09()
        {

            #region Provider - Supplier

            var partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            string providerName = "CDV6-13232" + partialDateTimeSuffix;
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13232_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, teamID, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2            
            var serviceElement2Name = "CDV6_13232_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Services Provided
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1, serviceElement2, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Rate Unit            
            var rateUnit = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            #endregion

            #region Service provision Start 

            foreach (var spsrId in dbHelper.serviceProvisionStartReason.GetDefaultBrokerageStartReason())
                dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(spsrId, false);

            var ServiceProvisionStartReasonExists = dbHelper.serviceProvisionStartReason.GetByName("Brokered Care Package_13232").Any();
            if (!ServiceProvisionStartReasonExists)
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "Brokered Care Package_13232", new DateTime(2019, 03, 29));
            var BrokeredCarePackage = dbHelper.serviceProvisionStartReason.GetByName("Brokered Care Package_13232")[0];

            ServiceProvisionStartReasonExists = dbHelper.serviceProvisionStartReason.GetByName("New Placement_13232").Any();
            if (!ServiceProvisionStartReasonExists)
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "New Placement_13232", new DateTime(2019, 01, 20));
            var NewPlacement = dbHelper.serviceProvisionStartReason.GetByName("New Placement_13232")[0];

            ServiceProvisionStartReasonExists = dbHelper.serviceProvisionStartReason.GetByName("Service Provision Start Reason Test_13232").Any();
            if (!ServiceProvisionStartReasonExists)
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "Service Provision Start Reason Test_13232", new DateTime(2021, 01, 01));
            var ServiceProvisionStartReasonTest = dbHelper.serviceProvisionStartReason.GetByName("Service Provision Start Reason Test_13232")[0];

            //Change All Default Brokerage start reason to "Yes"
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(BrokeredCarePackage, false);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(NewPlacement, false);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(ServiceProvisionStartReasonTest, true);

            #endregion

            #region Service Provision End Reason

            var serviceprovisionendreasonid = commonMethodsDB.CreateServiceProvisionEndReason(teamID, "EndReason_13232", new DateTime(2022, 1, 1), true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(serviceprovisionendreasonid, true);

            #endregion

            #region Brokerage Episode and Brokerage Offer

            //To create Brokerage Episode record 
            var sourcedDate = DateTime.Now.AddDays(-3);
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //Updated Brokerage Episode 1 to Sourcing in progress

            //To create Brokerage offer record 
            var offerId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offerId, 5, sourcedDate);//update Brokerage offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1, serviceElement2, DateTime.Now, null); //update Brokerage Episode status to Approved in Progress

            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(offerId, 6, rateUnit, serviceProvidedId);//update Brokerage offer status to Approved

            //Auto generated Service provision Record
            var serviceProvisionRecords = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisionRecords.Count);

            #endregion

            #region Service Provision End Reason (Default Brokerage End Reason = Yes)

            if (!dbHelper.serviceProvisionEndReason.GetDefaultBrokerageEndReason().Any())
            {
                var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();
                commonMethodsDB.CreateServiceProvisionEndReason(teamID, "SPER " + currentDateTime, new DateTime(2022, 1, 1), true);
            }

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                  .WaitForMainMenuToLoad()
                  .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByFullName(_personFirstName, _personLastName)
                 .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionRecords[0].ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .ValidateStartReasonFieldLinkVisibility(true)
                 .ValidateStartReasonFieldLinkText("Service Provision Start Reason Test_13232");


            //To change Brokerage offer Accepted status to Sourced
            dbHelper.brokerageOffer.BrokerageOfferActivateRecord(offerId);

            var Record = dbHelper.brokerageOffer.GetBrokerageOfferByPersonID(personID);
            var SourcedOfferRecord = dbHelper.brokerageOffer.GetByID(Record[0], "inactive", "statusid");

            Assert.AreEqual(false, SourcedOfferRecord["inactive"]);
            Assert.AreEqual(5, SourcedOfferRecord["statusid"]);

            //Changing Default Brokerage start reason by deactivating ServiceProvisionStartReasonTest
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(BrokeredCarePackage, true);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(NewPlacement, false);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(ServiceProvisionStartReasonTest, false);

            //To create Brokerage Episode 1
            var episode1Id = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episode1Id, 3); //Updated Brokerage Episode 1 to Sourcing in progress


            //To create Brokerage Offer 1 
            var offer1Id = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episode1Id, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offer1Id, 5, sourcedDate);//update Brokerage Offer 1  status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episode1Id, 6, serviceElement1, serviceElement2, DateTime.Now, DateTime.Now); //update Brokerage Episode 1 status to Approved in Progress

            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(offer1Id, 6, rateUnit, serviceProvidedId);//update Brokerage Offer 1  status to Approved

            //Auto generated Service Provision for Brokerage Offer 1 Record
            var serviceProvisionRecord1 = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisionRecord1.Count);


            mainMenu
                  .WaitForMainMenuToLoad()
                  .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByFullName(_personFirstName, _personLastName)
                 .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionRecord1[0].ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .ValidateStartReasonFieldLinkVisibility(true)
                 .ValidateStartReasonFieldLinkText("Brokered Care Package_13232");



            //To change Brokerage offer Accepted status to Sourced
            dbHelper.brokerageOffer.BrokerageOfferActivateRecord(offer1Id);

            var Record1 = dbHelper.brokerageOffer.GetBrokerageOfferByPersonID(personID);
            var SourcedOfferRecord1 = dbHelper.brokerageOffer.GetByID(Record1[0], "inactive", "statusid");

            Assert.AreEqual(false, SourcedOfferRecord1["inactive"]);
            Assert.AreEqual(5, SourcedOfferRecord1["statusid"]);


            //Changing Default Brokerage start reason 
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(BrokeredCarePackage, true);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(NewPlacement, false);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(ServiceProvisionStartReasonTest, false);

            var episode2Id = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episode2Id, 3); //Updated to Sourcing in progress


            var offer2Id = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episode2Id, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offer2Id, 5, sourcedDate);//update status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episode2Id, 6, serviceElement1, serviceElement2, DateTime.Now, DateTime.Now); //update status to Approved in Progress

            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(offer2Id, 6, rateUnit, serviceProvidedId);//update status to Approved

            var serviceProvisionRecord2 = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisionRecord2.Count);

            mainMenu
             .WaitForMainMenuToLoad()
             .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByFullName(_personFirstName, _personLastName)
                 .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionRecord2[0].ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .ValidateStartReasonFieldLinkVisibility(true)
                 .ValidateStartReasonFieldLinkText("Brokered Care Package_13232");


            //Change Default Brokerage start reason to "Yes"
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(BrokeredCarePackage, true);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(NewPlacement, true);
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(ServiceProvisionStartReasonTest, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(serviceprovisionendreasonid, false);

        }

        [TestProperty("JiraIssueID", "CDV6-13279")]
        [Description("Open active social care case form  for record with Brokerage episode record with planned end date field and update the status to  Approved" + "Brokerage offer record with Accepted status" +
           "Validate the service Provision record created Automatically" + "Create another Episode record with Approved status and created the offer record with the Sourced status" +
            "Change the Default Brokerage end reasons to No " + "Update the Sourced offer record status to Accepted by entering the mandatory fields" + "Click on save button" +
            "Validate the error message:No default Service Provision End Reason exists. is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod10()
        {

            #region Provider - Supplier
            var partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            string providerName = "CDV6-13279" + partialDateTimeSuffix;
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");

            #endregion            

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13279_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, teamID, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2            
            var serviceElement2Name = "CDV6_13279_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Services Provided
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1, serviceElement2, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Rate Unit            
            var rateUnit = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            #endregion

            #region Service provision End Reason
            //Service provision EndReasons Records
            var ServiceProvisionEndReasonExists = dbHelper.serviceProvisionEndReason.GetByName("Reason1_13279").Any();
            if (!ServiceProvisionEndReasonExists)
                dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(teamID, "Reason1_13279", new DateTime(2019, 01, 01));
            var EndReason_Reason1 = dbHelper.serviceProvisionEndReason.GetByName("Reason1_13279")[0];

            ServiceProvisionEndReasonExists = dbHelper.serviceProvisionEndReason.GetByName("Reason2_13279").Any();
            if (!ServiceProvisionEndReasonExists)
                dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(teamID, "Reason2_13279", new DateTime(2019, 01, 01));
            var EndReason_Reason2 = dbHelper.serviceProvisionEndReason.GetByName("Reason2_13279")[0];

            ServiceProvisionEndReasonExists = dbHelper.serviceProvisionEndReason.GetByName("Services end as planned_13279").Any();
            if (!ServiceProvisionEndReasonExists)
                dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(teamID, "Services end as planned_13279", new DateTime(2019, 01, 01));
            var EndReason_ServicesEndAsPlanned = dbHelper.serviceProvisionEndReason.GetByName("Services end as planned_13279")[0];

            ServiceProvisionEndReasonExists = dbHelper.serviceProvisionEndReason.GetByName("Temporary Brokered Care Package_13279").Any();
            if (!ServiceProvisionEndReasonExists)
                dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(teamID, "Temporary Brokered Care Package_13279", new DateTime(2019, 01, 01));
            var EndReason_TemporaryBrokeredCarePackage = dbHelper.serviceProvisionEndReason.GetByName("Temporary Brokered Care Package_13279")[0];


            //Change All Default Brokerage end reasons to "Yes"
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason1, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason2, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_ServicesEndAsPlanned, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_TemporaryBrokeredCarePackage, true);


            #endregion

            #region Brokerage Episode and Brokerage Episode Offer
            //To create Brokerage Episode
            var sourcedDate = DateTime.Now.AddDays(-3);
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //Updated to Sourcing in progress

            //To create Brokerage Offer Record in Under review status
            var offerId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offerId, 5, sourcedDate);//update Brokerage Offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1, serviceElement2, DateTime.Now.AddDays(-2), DateTime.Now); //update Brokerage Episode status to Approved in Progress

            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(offerId, 6, rateUnit, serviceProvidedId);//update Brokerage Offer status to Approved

            //Auto generated Service Provision Record
            var serviceProvisionRecords = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisionRecords.Count);

            //To create Brokerage Episode 1
            var episode1Id = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episode1Id, 3); //Updated Brokerage Episode 1 to Sourcing in progress

            //To create Brokerage offer 1 record with Under review status
            var offer1Id = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episode1Id, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offer1Id, 5, sourcedDate);//update Brokerage offer 1 status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episode1Id, 6, serviceElement1, serviceElement2, DateTime.Now, DateTime.Now); //update Brokerage Episode 1 status to Approved in Progress

            //Change All Default Brokerage end reason to "No"
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason1, false);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason2, false);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_ServicesEndAsPlanned, false);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_TemporaryBrokeredCarePackage, false);

            #endregion

            loginPage
                 .GoToLoginPage()
                 .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                  .WaitForMainMenuToLoad()
                  .NavigateToCasesSection();

            casesPage
                   .WaitForCasesPageToLoad()
                   .SearchByCaseNumber(caseNumber, caseID.ToString())
                   .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                    .WaitForCaseRecordPageToLoad()
                    .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                    .WaitForCaseBrokerageEpisodesPageToLoad()
                    .OpenRecord(episode1Id.ToString());

            caseBrokerageEpisodeRecordPage
                    .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                    .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                    .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                    .OpenRecord(offer1Id.ToString());

            caseBrokerageEpisodeOfferRecordPage
                    .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                    .SelectStatus("Accepted")
                    .ClickServiceProvidedLookUpButton();

            lookupPopup
                   .WaitForLookupPopupToLoad()
                   .TypeSearchQuery(providerName)
                   .TapSearchButton()
                   .SelectResultElement(serviceProvidedId.ToString());


            caseBrokerageEpisodeOfferRecordPage
                    .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                    .ClickSaveButton();


            dynamicDialogPopup
                    .WaitForDynamicDialogPopupToLoad()
                    .ValidateMessage("No default Service Provision End Reason exists.").TapCloseButton();


            //Change All Default Brokerage end reasons to "Yes"
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason1, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason2, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_ServicesEndAsPlanned, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_TemporaryBrokeredCarePackage, true);
        }

        [TestProperty("JiraIssueID", "CDV6-13280")]
        [Description("Open active social care case form  for record with Brokerage episode record with planned end date field and update the status to  Approved" + "Brokerage offer record with Accepted status" +
           "Validate the service Provision record created Automatically" + "Create another Episode record with Approved status and created the offer record with the Sourced status" +
            "Change the Default Brokerage end reasons to Yes (Each time change one of the end reason record status to Yes) " + "Update the Sourced offer record status to Accepted by entering the mandatory fields" + "Click on save button" +
            "Validate the Default Brokerage end reason which is changed to yes is displayed in the End reason field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod11()
        {

            #region Provider - Supplier
            var partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            string providerName = "CDV6-13280" + partialDateTimeSuffix;
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");

            #endregion            

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13280_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, teamID, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2            
            var serviceElement2Name = "CDV6_13280_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Services Provided
            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1, serviceElement2, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Rate Unit            
            var rateUnit = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            #endregion                      

            #region Service provision End Reason
            //Service provision EndReasons Records
            var ServiceProvisionEndReasonExists = dbHelper.serviceProvisionEndReason.GetByName("Reason1_13280").Any();
            if (!ServiceProvisionEndReasonExists)
                dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(teamID, "Reason1_13280", new DateTime(2019, 01, 01));
            var EndReason_Reason1 = dbHelper.serviceProvisionEndReason.GetByName("Reason1_13280")[0];

            ServiceProvisionEndReasonExists = dbHelper.serviceProvisionEndReason.GetByName("Reason2_13280").Any();
            if (!ServiceProvisionEndReasonExists)
                dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(teamID, "Reason2_13280", new DateTime(2019, 01, 01));
            var EndReason_Reason2 = dbHelper.serviceProvisionEndReason.GetByName("Reason2_13280")[0];

            ServiceProvisionEndReasonExists = dbHelper.serviceProvisionEndReason.GetByName("Services end as planned_13280").Any();
            if (!ServiceProvisionEndReasonExists)
                dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(teamID, "Services end as planned_13280", new DateTime(2019, 01, 01));
            var EndReason_ServicesEndAsPlanned = dbHelper.serviceProvisionEndReason.GetByName("Services end as planned_13280")[0];

            ServiceProvisionEndReasonExists = dbHelper.serviceProvisionEndReason.GetByName("Temporary Brokered Care Package_13280").Any();
            if (!ServiceProvisionEndReasonExists)
                dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(teamID, "Temporary Brokered Care Package_13280", new DateTime(2019, 01, 01));
            var EndReason_TemporaryBrokeredCarePackage = dbHelper.serviceProvisionEndReason.GetByName("Temporary Brokered Care Package_13280")[0];


            //Change All Default Brokerage end reasons to "Yes"
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason1, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason2, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_ServicesEndAsPlanned, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_TemporaryBrokeredCarePackage, true);

            #endregion

            #region Brokerage Episode
            //To create Brokerage Episode record 
            var sourcedDate = DateTime.Now.AddDays(-3);
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //Updated to Sourcing in progress


            //To create Brokerage offer record 
            var offerId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offerId, 5, sourcedDate);//update Brokerage offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1, serviceElement2, DateTime.Now, DateTime.Now); //update Brokerage Episode status to Approved in Progress

            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(offerId, 6, rateUnit, serviceProvidedId);//update Brokerage offer status to Approved

            //Auto generated Service provision Record
            var serviceProvisionRecords = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisionRecords.Count);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("Case_Brokerage_Episodes_User1", "Passw0rd_!");

            mainMenu
                  .WaitForMainMenuToLoad()
                  .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByFullName(_personFirstName, _personLastName)
                 .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionRecords[0].ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .ValidateEndReasonFieldLinkVisibility(true)
                 .ValidateEndReasonFieldLinkText("Reason1_13280");


            //To change Brokerage offer Accepted status to Sourced
            dbHelper.brokerageOffer.BrokerageOfferActivateRecord(offerId);

            var Record = dbHelper.brokerageOffer.GetBrokerageOfferByPersonID(personID);
            var SourcedOfferRecord = dbHelper.brokerageOffer.GetByID(Record[0], "inactive", "statusid");

            Assert.AreEqual(false, SourcedOfferRecord["inactive"]);
            Assert.AreEqual(5, SourcedOfferRecord["statusid"]);

            //Changing Default Brokerage end reason by deactivating ServiceProvisionStartReasonTest
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason1, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason2, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_ServicesEndAsPlanned, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_TemporaryBrokeredCarePackage, false);

            //To create Brokerage Episode 1
            var episode1Id = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episode1Id, 3); //Updated Brokerage Episode 1 to Sourcing in progress


            //To create Brokerage Offer 1 
            var offer1Id = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episode1Id, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offer1Id, 5, sourcedDate);//update Brokerage Offer 1  status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episode1Id, 6, serviceElement1, serviceElement2, DateTime.Now, DateTime.Now); //update Brokerage Episode 1 status to Approved in Progress

            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(offer1Id, 6, rateUnit, serviceProvidedId);//update Brokerage Offer 1  status to Approved

            //Auto generated Service Provision for Brokerage Offer 1 Record
            var serviceProvisionRecord1 = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisionRecord1.Count);


            mainMenu
                  .WaitForMainMenuToLoad()
                  .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByFullName(_personFirstName, _personLastName)
                 .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionRecord1[0].ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .ValidateEndReasonFieldLinkVisibility(true)
                 .ValidateEndReasonFieldLinkText("Reason1_13280");



            //To change Brokerage offer Accepted status to Sourced
            dbHelper.brokerageOffer.BrokerageOfferActivateRecord(offer1Id);

            var Record1 = dbHelper.brokerageOffer.GetBrokerageOfferByPersonID(personID);
            var SourcedOfferRecord1 = dbHelper.brokerageOffer.GetByID(Record1[0], "inactive", "statusid");

            Assert.AreEqual(false, SourcedOfferRecord1["inactive"]);
            Assert.AreEqual(5, SourcedOfferRecord1["statusid"]);


            //Changing Default Brokerage end reason 
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason1, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason2, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_ServicesEndAsPlanned, false);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_TemporaryBrokeredCarePackage, false);

            var episode2Id = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episode2Id, 3); //Updated to Sourcing in progress


            var offer2Id = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episode2Id, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offer2Id, 5, sourcedDate);//update status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episode2Id, 6, serviceElement1, serviceElement2, DateTime.Now, DateTime.Now); //update status to Approved in Progress

            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(offer2Id, 6, rateUnit, serviceProvidedId);//update status to Approved

            var serviceProvisionRecord2 = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisionRecord2.Count);

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByFullName(_personFirstName, _personLastName)
                 .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionRecord2[0].ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .ValidateEndReasonFieldLinkVisibility(true)
                 .ValidateEndReasonFieldLinkText("Reason1_13280");


            //To change Brokerage offer Accepted status to Sourced
            dbHelper.brokerageOffer.BrokerageOfferActivateRecord(offer2Id);

            var Record2 = dbHelper.brokerageOffer.GetBrokerageOfferByPersonID(personID);
            var SourcedOfferRecord2 = dbHelper.brokerageOffer.GetByID(Record2[0], "inactive", "statusid");

            Assert.AreEqual(false, SourcedOfferRecord2["inactive"]);
            Assert.AreEqual(5, SourcedOfferRecord2["statusid"]);

            //Changing Default Brokerage end reason 
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason1, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason2, false);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_ServicesEndAsPlanned, false);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_TemporaryBrokeredCarePackage, false);

            var episode3Id = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episode3Id, 3); //Updated to Sourcing in progress


            var offer3Id = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episode3Id, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(offer3Id, 5, sourcedDate);//update status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episode3Id, 6, serviceElement1, serviceElement2, DateTime.Now, DateTime.Now); //update status to Approved in Progress

            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(offer3Id, 6, rateUnit, serviceProvidedId);//update status to Approved

            var serviceProvisionRecord3 = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisionRecord3.Count);

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            peoplePage.
                 WaitForPeoplePageToLoad()
                 .SearchPersonRecordByFullName(_personFirstName, _personLastName)
                 .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionRecord3[0].ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .ValidateEndReasonFieldLinkVisibility(true)
                 .ValidateEndReasonFieldLinkText("Reason1_13280");


            //Changing Default Brokerage end reason 
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason1, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_Reason2, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_ServicesEndAsPlanned, true);
            dbHelper.serviceProvisionEndReason.UpdateServiceProvisionDefaultBrokerageEndReason(EndReason_TemporaryBrokeredCarePackage, true);


            //To change Brokerage offer Accepted status to Sourced
            dbHelper.brokerageOffer.BrokerageOfferActivateRecord(offer3Id);

            var Record3 = dbHelper.brokerageOffer.GetBrokerageOfferByPersonID(personID);
            var SourcedOfferRecord3 = dbHelper.brokerageOffer.GetByID(Record3[0], "inactive", "statusid");

            Assert.AreEqual(false, SourcedOfferRecord3["inactive"]);
            Assert.AreEqual(5, SourcedOfferRecord3["statusid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-13281")]
        [Description("Open active social care case record with Brokerage episode in New status" +
         "Change the Brokerage episode record status to Request Rejected and enter the mandatory fields and save the Record" +
         "Navigate to Brokerage offer page and create new record by entering all Mandatory field and click save button." +
         "Validate the alert message displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod12()
        {
            #region Provider - Supplier
            var partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            string providerName = "CDV6-13281" + partialDateTimeSuffix;
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");

            #endregion            

            #region Service Element 1            

            var serviceElement1Name = "CDV6_13281_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, teamID, startDate, code, whotopayid, paymentscommenceid, validRateUnits, defaulRateUnitID);

            #endregion

            #region Service Element 2            
            var serviceElement2Name = "CDV6_13281_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var serviceElement2 = commonMethodsDB.CreateServiceElement2(teamID, serviceElement2Name, startDate, code);

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1, serviceElement2);

            #endregion

            #region Brokerage Episode
            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 1); //update Brokerage Episode status to New

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

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .SelectStatus("Request Rejected")
                .InsertRequestRejectionDateTime(DateTime.Now.AddDays(-1).ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "09:00")
                .ClickRequestRejectionReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Insufficient or incorrect information given")
                .TapSearchButton()
                .SelectResultElement(insufficientInformation_brokerageEpisodeRejectionReason.ToString());


            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .InsertReceivedDateTime(DateTime.Now.AddDays(-1).ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "09:00")
                .ClickProviderLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(providerID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Offers cannot be added when the brokerage episode status is New, Request Rejected, Cancelled or Completed.");

        }

        //Bug:CDV6-13340
        [TestProperty("JiraIssueID", "CDV6-13285")]
        [Description("Open active social care case record with Brokerage episode and Brokerage offer record with Under review status" +
         "Change the Brokerage offer record status to Sourced and enter the mandatory fields" +
         "Change the Brokerage offer record status to Accepted and enter the mandatory fields" +
         "Validate the alert message displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod13()
        {
            var cancellationDate = DateTime.Now.AddDays(-1);
            var cancellationTime = DateTime.Now.AddMinutes(05);
            var cancellationReason = dbHelper.brokerageEpisodeCancellationReason.GetByBrokerageEpisodeCancellationReasonName("Family providing care now").FirstOrDefault();

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .SelectStatus("Cancelled")
                .InsertCancellationDateTime(cancellationDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), cancellationTime.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
                .ClickCancellationReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Family providing care now")
                .TapSearchButton()
                .SelectResultElement(cancellationReason.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ValidateNewRecordButtonVisibility(false);

        }


        [TestProperty("JiraIssueID", "CDV6-13316")]
        [Description("Open active social care case record with Brokerage episode and Brokerage offer record with Sourced status" +
           "Validate the  Tracking Status field link should not be displayed for the episode which is having Sourced Date/Time " +
           "After Executing the Scheduled Job 'Update Brokerage Episode Tracking Status'" +
           "Validate the  Tracking Status field link should not be displayed for the episode which is having Sourced Date/Time ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod14()
        {
            #region Provider - Supplier
            var partialDateTimeSuffix = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            string providerName = "CDV6-13316" + partialDateTimeSuffix;
            var providerID = commonMethodsDB.CreateProvider(providerName, teamID, 2, providerName + "@test.com");

            #endregion

            #region Brokerage Episode and Brokerage Episode Offer
            var sourcedDate = DateTime.Now.AddDays(-3);
            var sourcedTime = DateTime.Now;

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            //To Create Brokerage Offer Record in Under Review Status
            var OfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(OfferId, 5, sourcedDate);//update Brokerage offer status to Sourced
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

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ValidateTrackingStatusLinkFieldText("")
                .ValidateSourcedDateTime(sourcedDate.ToUniversalTime().ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), sourcedTime.ToUniversalTime().ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture));


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate(); //execute the "Update Brokerage Episode Tracking Status" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UpdateBrokerageEpisodeTrackingStatusScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UpdateBrokerageEpisodeTrackingStatusScheduledJob);

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
                    .ValidateTrackingStatusLinkFieldText("")
                    .ValidateSourcedDateTime(sourcedDate.ToUniversalTime().ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), sourcedTime.ToUniversalTime().ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture));



        }

        [TestProperty("JiraIssueID", "CDV6-13317")]
        [Description("Open active social care case record with Brokerage episode in New status" +
        "Change the Brokerage episode record status to Request Rejected and enter the mandatory fields and save the Record" +
         "Validate the  Tracking Status field link should not be displayed for the episode which is having Sourced Date/Time " +
           "After Executing the Scheduled Job 'Update Brokerage Episode Tracking Status'" +
           "Validate the  Tracking Status field link should not be displayed for the episode which is having Sourced Date/Time ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod15()
        {

            #region Brokerage Episode
            //To create Brokerage Episode Record            
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 1); //update Brokerage Episode status to New

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

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .SelectStatus("Request Rejected")
                .InsertRequestRejectionDateTime(DateTime.Now.AddDays(-1).ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "09:00")
                .ClickRequestRejectionReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Insufficient or incorrect information given")
                .TapSearchButton()
                .SelectResultElement(insufficientInformation_brokerageEpisodeRejectionReason.ToString());


            caseBrokerageEpisodeRecordPage
              .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
              .ClickSaveButton();

            caseBrokerageEpisodeRecordPage
              .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
              .ValidateTrackingStatusLinkFieldText("");


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate(); //execute the "Update Brokerage Episode Tracking Status" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UpdateBrokerageEpisodeTrackingStatusScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UpdateBrokerageEpisodeTrackingStatusScheduledJob);


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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateTrackingStatusLinkFieldText("");

        }

        //Bug - CDV6-13340
        [TestProperty("JiraIssueID", "CDV6-13318")]
        [Description("Open active social care case record with Brokerage episode in New status" +
        "Change the Brokerage episode record status to Cancelled and enter the mandatory fields and save the Record" +
         "Validate the  Tracking Status field link should not be displayed for the episode which is having Sourced Date/Time " +
           "After Executing the Scheduled Job 'Update Brokerage Episode Tracking Status'" +
           "Validate the  Tracking Status field link should not be displayed for the episode which is having Sourced Date/Time ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod16()
        {
            var cancellationDate = DateTime.Now.AddDays(-1);
            var cancellationTime = DateTime.Now.AddMinutes(05);
            var cancellationReason = dbHelper.brokerageEpisodeCancellationReason.GetByBrokerageEpisodeCancellationReasonName("Family providing care now").FirstOrDefault();

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 1); //update Brokerage Episode status to New


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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .SelectStatus("Cancelled")
                .InsertCancellationDateTime(cancellationDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), cancellationTime.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
                .ClickCancellationReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Family providing care now")
                .TapSearchButton()
                .SelectResultElement(cancellationReason.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateTrackingStatusLinkFieldText("");


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate(); //execute the "Update Brokerage Episode Tracking Status" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UpdateBrokerageEpisodeTrackingStatusScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UpdateBrokerageEpisodeTrackingStatusScheduledJob);


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
               .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
               .ValidateTrackingStatusLinkFieldText("");
        }

        [TestProperty("JiraIssueID", "CDV6-13320")]
        [Description("Open active social care case record with Brokerage episode and Brokerage offer record with Accepted status" +
         "Open the Accepted Offer and click on Undo Acceptance Button" +
         "Validate the Pop up message with Are you sure you would like to undo the acceptance of this Offer? The status will revert to Sourced and the related Service Provision will be deleted.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod17()
        {
            var sourcedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day);
            CreateServiceElement_RateUnit();

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Services Provided

            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1Id, serviceElement2Id, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            #endregion

            #region Brokerage Offer

            //To Create Brokerage Offer Record in Under Review Status
            var OfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(OfferId, 5, sourcedDate);//update Brokerage offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1Id, serviceElement2Id, DateTime.Now, null); //update  Brokerage Episode status to Approved in Progress

            //Update Brokerage Offer record to Accepted status
            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(OfferId, 6, rateUnitId2, serviceProvidedId);

            #endregion

            loginPage
                   .GoToLoginPage()
                   .Login(_systemUserName, "Passw0rd_!");

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
                    .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                    .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                    .OpenRecord(OfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                    .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                    .ClickUndoAccptanceButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure you would like to undo the acceptance of this Offer? The status will revert to Sourced and the related Service Provision will be deleted.");



        }


        [TestProperty("JiraIssueID", "CDV6-13321")]
        [Description("Open active social care case record with Brokerage episode and Brokerage offer record with Accepted status" +
        "Open the Brokerage episode record and Validate the Service Element 1, Service Element 2, Planned Start Date, Planned End Date, Contract Type, Finance Client Category & Temporary Care on the Episode become read only once an Offer is Accepted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod18()
        {
            var sourcedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day);
            CreateServiceElement_RateUnit();

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Services Provided

            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1Id, serviceElement2Id, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            #endregion

            #region Brokerage Offer

            //To Create Brokerage Offer Record in Under Review Status
            var OfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(OfferId, 5, sourcedDate);//update Brokerage offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1Id, serviceElement2Id, DateTime.Now, null); //update  Brokerage Episode status to Approved in Progress

            //Update Brokerage Offer record to Accepted status
            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(OfferId, 6, rateUnitId2, serviceProvidedId);

            #endregion

            loginPage
                   .GoToLoginPage()
                   .Login(_systemUserName, "Passw0rd_!");

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
                .ValidateServiceElement1FieldDisabled(true)
                .ValidateServiceElement2FieldDisabled(true)
                .ValidatePlannedStartDateDisabled(true)
                .ValidatePlannedEndDateDisabled(true)
                .ValidateFinanceClientCategoryDisabled(true)
                .ValidateTemperoryCare_NoRadioButtonDisabled(true)
                .ValidateTemperoryCare_YesRadioButtonDisabled(true)
                .ValidateContractTypeDisabled(true);

        }


        //Bug:CDV6-13355.
        [TestProperty("JiraIssueID", "CDV6-13323")]
        [Description("Open active social care case record with Brokerage episode and Brokerage offer record with Accepted status" +
       "Open the auto generated service provision record and change the status from Draft to ready for authorisation and Enter the mandatory fields" +
       "Atleast one service delivery record should be created for the service provision record to change the status." +
        "Open the Brokerage offer record and click on the undo Acceptance button " + "Click ok on the promt and validate the message in the pop up" +
            "Validate the rate unit field and Service provider field are cleared and the status should be Sourced")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod19()
        {
            CreateServiceElement_RateUnit();

            var status = 1; //new
            var sourcedDate = DateTime.Now.AddDays(-3);

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Services Provided

            var serviceProvider = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1Id, serviceElement2Id, null, null, null, null, 2, false, 1, false);

            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(teamID, serviceProvider, rateUnit, new DateTime(2022, 1, 1), 2);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(teamID, serviceProvidedRatePeriodId, serviceProvider, 10m, 15m);

            #endregion

            #region Service Provision Status

            var serviceProvisionStatus = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Ready for Authorisation")[0];
            var serviceProvisionDraftStatus = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];

            #endregion


            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            //To Create Brokerage Offer Record in Under Review Status
            var OfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(OfferId, 5, sourcedDate);//update Brokerage offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1Id, serviceElement2Id, DateTime.Now, null); //update  Brokerage Episode status to Approved in Progress

            //Update Brokerage Offer record to Accepted status
            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(OfferId, 6, rateUnit, serviceProvider);


            //Service provision record Autogenerated
            var serviceProvisionRecord = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisionRecord.Count);


            //Creating Service delivery for the service provision
            dbHelper.serviceDelivery.CreateServiceDelivery(teamID, personID, serviceProvisionRecord[0], rateUnit, 1, 1, false, true, false, false, false, false, false, false, "Delivery");


            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionRecord[0].ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickStatusLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Ready for Authorisation")
                .TapSearchButton()
                .SelectResultElement(serviceProvisionStatus.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .ClickPurchasingteamLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_teamName)
                .TapSearchButton()
                .SelectResultElement(teamID.ToString());


            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .TapSaveAndCloseButton();


            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(OfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateRateUnitTextVisibility(true)
                .ValidateServiceProvidedTextVisibility(true)
                .ClickUndoAccptanceButton();


            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure you would like to undo the acceptance of this Offer? The status will revert to Sourced and the related Service Provision will be deleted.").TapOKButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Could not undo Offer acceptance. A Service Provision cannot be deleted once it has been submitted for authorisation.").TapCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(serviceProvisionRecord[0].ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ClickStatusLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Draft")
                .TapSearchButton()
                .SelectResultElement(serviceProvisionDraftStatus.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .TapSaveAndCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(OfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickUndoAccptanceButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure you would like to undo the acceptance of this Offer? The status will revert to Sourced and the related Service Provision will be deleted.").TapOKButton();

            System.Threading.Thread.Sleep(3000);

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusSelectedText("Sourced")
                .ValidateRateUnitLinkFieldText("Per 1 Hour Unit \\ Units (Parts)")
                .ValidateServiceProvidedLinkFieldText("");

        }

        [TestProperty("JiraIssueID", "CDV6-13343")]
        [Description("Open active social care case record with Brokerage episode and Brokerage offer record with Accepted status" +
         "Open the Brokerage episode record and Validate the Service Element 1, Service Element 2, Planned Start Date, Planned End Date, Contract Type, Finance Client Category & Temporary Care on the Episode become read only once an Offer is Accepted" +
         "Change the offer status to sourced and validate the Brokerage episode is in Approved status" +
          "Validate the Service Element 1, Service Element 2, Planned Start Date, Planned End Date, Contract Type, Finance Client Category & Temporary Care on the Episode are enabled" +
           "Validate the relavent Service provision record is deleted for the record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod20()
        {
            var sourcedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day);
            CreateServiceElement_RateUnit();

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Services Provided

            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1Id, serviceElement2Id, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            #endregion

            #region Brokerage Offer

            //To Create Brokerage Offer Record in Under Review Status
            var OfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(OfferId, 5, sourcedDate);//update Brokerage offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1Id, serviceElement2Id, DateTime.Now, null); //update  Brokerage Episode status to Approved in Progress

            //Update Brokerage Offer record to Accepted status
            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(OfferId, 6, rateUnitId2, serviceProvidedId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .ValidateServiceElement1FieldDisabled(true)
                .ValidateServiceElement2FieldDisabled(true)
                .ValidatePlannedStartDateDisabled(true)
                .ValidatePlannedEndDateDisabled(true)
                .ValidateFinanceClientCategoryDisabled(true)
                .ValidateTemperoryCare_NoRadioButtonDisabled(true)
                .ValidateTemperoryCare_YesRadioButtonDisabled(true)
                .ValidateContractTypeDisabled(true);

            //To change Brokerage offer Accepted status to Sourced
            dbHelper.brokerageOffer.BrokerageOfferActivateRecord(OfferId);

            var Record = dbHelper.brokerageOffer.GetBrokerageOfferByPersonID(personID);
            var SourcedOfferRecord = dbHelper.brokerageOffer.GetByID(Record[0], "inactive", "statusid");

            Assert.AreEqual(false, SourcedOfferRecord["inactive"]);
            Assert.AreEqual(5, SourcedOfferRecord["statusid"]);

            //To validate the fields are enable once the Accepted offer is reversed
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .ValidateStatusSelectedText("Approved")
                .ValidateServiceElement1FieldDisabled(false)
                .ValidateServiceElement2FieldDisabled(false)
                .ValidatePlannedStartDateDisabled(false)
                .ValidatePlannedEndDateDisabled(false)
                .ValidateFinanceClientCategoryDisabled(false)
                .ValidateTemperoryCare_NoRadioButtonDisabled(false)
                .ValidateTemperoryCare_YesRadioButtonDisabled(false)
                .ValidateContractTypeDisabled(false);

            //Validate the Service Provision record is deleted

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());


            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .NavigateToServiceProvisionSubPage();

            caseBrokerageEpisodeServiceProvisionsPage
                .WaitForCaseBrokerageEpisodeServiceProvisionsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);


            //Service provision record Autogenerated
            var serviceProvisionRecords = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(0, serviceProvisionRecords.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-13344")]
        [Description("Open active social care case record with Brokerage episode and Brokerage offer record with Accepted status" +
            "Undo the Accepted offer and save the record" +
             "Open the Brokerage episode record and change the status to Awaiting Commencement" + "Validate the Message")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod21()
        {
            var sourcedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day);
            CreateServiceElement_RateUnit();

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Services Provided

            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1Id, serviceElement2Id, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            #endregion

            #region Brokerage Offer

            //To Create Brokerage Offer Record in Under Review Status
            var OfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(OfferId, 5, sourcedDate);//update Brokerage offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1Id, serviceElement2Id, DateTime.Now, null); //update  Brokerage Episode status to Approved in Progress

            //Update Brokerage Offer record to Accepted status
            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(OfferId, 6, rateUnitId2, serviceProvidedId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            CreatelocalizeStringValueIfNotAvailable();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(OfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickUndoAccptanceButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure you would like to undo the acceptance of this Offer? The status will revert to Sourced and the related Service Provision will be deleted.").TapOKButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .SelectStatus("Awaiting Commencement")
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("The Episode cannot be set to Completed/Awaiting Commencement because, an Accepted Offer does not exist for this episode.");

        }

        [TestProperty("JiraIssueID", "CDV6-13347")]
        [Description("Open active social care case record with Brokerage episode and Brokerage offer record with Sourced status" +
            "Undo the Accepted offer and save the record" +
            "Open the Brokerage episode record and change the status to Completed" + "Validate the Message")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod22()
        {
            var sourcedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day);
            CreateServiceElement_RateUnit();

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Services Provided

            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1Id, serviceElement2Id, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            #endregion

            #region Brokerage Offer

            //To Create Brokerage Offer Record in Under Review Status
            var OfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(OfferId, 5, sourcedDate);//update Brokerage offer status to Sourced

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1Id, serviceElement2Id, DateTime.Now, null); //update  Brokerage Episode status to Approved in Progress

            //Update Brokerage Offer record to Accepted status
            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(OfferId, 6, rateUnitId2, serviceProvidedId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            CreatelocalizeStringValueIfNotAvailable();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(OfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickUndoAccptanceButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure you would like to undo the acceptance of this Offer? The status will revert to Sourced and the related Service Provision will be deleted.").TapOKButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .SelectStatus("Completed")
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure that you have completed the Brokerage checklist?").TapOKButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("The Episode cannot be set to Completed/Awaiting Commencement because, an Accepted Offer does not exist for this episode.");

        }

        [TestProperty("JiraIssueID", "CDV6-13487")]
        [Description("Open active social care case record with Brokerage episode as Awaiting Commencement and Brokerage offer record with Accepted status" +
            "Change the Brokerage Episode Status to Completed and save the record" + "Undo the Accepted offer and Click ok in the prompt displayed" +
              "Validate the Message :Could not undo Offer acceptance. The parent Episode has been completed." +
            "Validate the Brokerage Episode record is inactive when the status is completed" + "Validate the Brokerage Episode escalation New record button is not displayed" +
            "Validate New record button for Brokerage offer is not displayed" +
            "Click on Activate button in the Brokerage Episode and validate the status displayed as Awaiting Commencement and the Brokerage episode should be active"
            )]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod23()
        {
            CreateServiceElement_RateUnit();

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Services Provided

            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1Id, serviceElement2Id, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, new DateTime(2021, 10, 9), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            #endregion

            #region Brokerage Offer

            var brokerageOfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId, 4); //update status to under review
            System.Threading.Thread.Sleep(2000);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(brokerageOfferId, 5, DateTime.Now.Date); //set the status to Sourced. This will automatically update the brokerage episode status as well

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1Id, serviceElement2Id, DateTime.Now.Date.AddMinutes(05), null); //update  Brokerage Episode status to Approved

            //Update Brokerage Offer record to Accepted status
            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(brokerageOfferId, 6, rateUnitId2, serviceProvidedId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            CreatelocalizeStringValueIfNotAvailable();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .SelectStatus("Completed")
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad();


            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure that you have completed the Brokerage checklist?").TapOKButton();

            System.Threading.Thread.Sleep(3000);

            //Validate When Status is completed Brokerage Episode fields should be inactive.
            var Records = dbHelper.brokerageEpisode.GetBrokerageEpisodeByPersonID(personID);
            var disabledRecords = dbHelper.brokerageEpisode.GetBrokerageEpisodeByID(Records[0], "inactive");

            Assert.AreEqual(true, disabledRecords["inactive"]);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickUndoAcceptanceButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure you would like to undo the acceptance of this Offer? The status will revert to Sourced and the related Service Provision will be deleted.").TapOKButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Could not undo Offer acceptance. The parent Episode has been completed.").TapCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad();

            brokerageEpisodeEscalationsSubArea
                .WaitForBrokerageEpisodeEscalationsSubAreaToLoad()
                .ValidateNewRecordButtonVisibility(false);


            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
               .WaitForCaseBrokerageEpisodeOffersPageToLoad()
               .ValidateNewRecordButtonVisibility(false);


            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .ClickActivateButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.").TapOKButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad();

            System.Threading.Thread.Sleep(3000);

            //Validate When Status is completed Brokerage Episode fields should be inactive.
            var EpisodeRecord = dbHelper.brokerageEpisode.GetBrokerageEpisodeByPersonID(personID);
            var EnabledRecords = dbHelper.brokerageEpisode.GetBrokerageEpisodeByID(EpisodeRecord[0], "inactive");

            Assert.AreEqual(false, EnabledRecords["inactive"]);

            caseBrokerageEpisodeRecordPage
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ValidateStatusSelectedText("Awaiting Commencement");

        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-13473


        [TestProperty("JiraIssueID", "CDV6-13502")]
        [Description("Open active social care case record with Brokerage episode with Approved status and Brokerage offer with all the status" +
           "Change the Brokerage offer record  from sourced status to Accepted and enter the mandatory fields" +
           "Validate the same offers available in the offers record page with different status")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch4_UITestMethod01()
        {
            CreateServiceElement_RateUnit();

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13502").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "StartReason_CDV6_13502", new DateTime(2022, 1, 1), true);

            #endregion

            #region Services Provided

            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1Id, serviceElement2Id, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, new DateTime(2021, 10, 9), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            #endregion

            #region Brokerage Offer

            var brokerageOfferId_New = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);

            var brokerageOfferId_UnderReview = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_UnderReview, 4); //update status to under review


            var brokerageOfferId_Sourced = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_Sourced, 4); //update status to under review
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(brokerageOfferId_Sourced, 5, DateTime.Now.Date); //set the status to Sourced. This will automatically update the brokerage episode status as well


            var brokerageOfferId_Rejected = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.RejectBrokerageOffer(brokerageOfferId_Rejected, 2, brokerageOfferRejectionReasonID);//Rejected offer


            var brokerageOfferId_Cancelled = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.CancelBrokerageOffer(brokerageOfferId_Cancelled, 3, _brokerageOfferCancellationReasonId); //update status to under Cancelled


            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1Id, serviceElement2Id, DateTime.Now.Date.AddMinutes(05), null); //update  Brokerage Episode status to Approved

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad();

            var brokerageOfferRecords = dbHelper.brokerageOffer.GetBrokerageOfferByPersonID(personID);
            Assert.AreEqual(5, brokerageOfferRecords.Count);


            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_Sourced.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Accepted")
                .ClickServiceProvidedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(serviceProvidedId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad();

            var Records = dbHelper.brokerageOffer.GetBrokerageOfferByPersonID(personID);
            Assert.AreEqual(5, Records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-13519")]
        [Description("Open active social care case record with Brokerage episode with Sourcing in Progress and Brokerage offer with all the status" +
         "Change the Brokerage offer record  from sourced status to Accepted and enter the mandatory fields" +
         "Validate the Pop up message")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch4_UITestMethod02()
        {
            CreateServiceElement_RateUnit();

            #region  Rate Type

            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            if (!dbHelper.rateType.GetByName("Hours (One only)").Any())
                dbHelper.rateType.CreateRateType(teamID, teamID, "Hours (One only)", code1, new DateTime(2020, 1, 1), 5, 6, 7);
            var rateType = dbHelper.rateType.GetByName("Hours (One only)")[0];

            #endregion

            #region  Rate Unit

            if (!dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (One only)").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _businessUnitId, "Per 1 Hour", new DateTime(2020, 1, 1), code1, rateType);
            var rateUnitId = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (One only)").FirstOrDefault();

            #endregion

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Services Provided

            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1Id, serviceElement2Id, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, new DateTime(2021, 10, 9), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            #endregion

            #region Brokerage Offer

            var brokerageOfferId_New = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);

            var brokerageOfferId_UnderReview = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_UnderReview, 4); //update status to under review


            var brokerageOfferId_Sourced = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_Sourced, 4); //update status to under review
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(brokerageOfferId_Sourced, 5, DateTime.Now.Date); //set the status to Sourced. This will automatically update the brokerage episode status as well


            var brokerageOfferId_Rejected = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.RejectBrokerageOffer(brokerageOfferId_Rejected, 2, brokerageOfferRejectionReasonID);//Rejected offer


            var brokerageOfferId_Cancelled = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.CancelBrokerageOffer(brokerageOfferId_Cancelled, 3, _brokerageOfferCancellationReasonId); //update status to under Cancelled

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .NavigateToBrokerageOffersSubPage();


            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad();

            var brokerageOfferRecords = dbHelper.brokerageOffer.GetBrokerageOfferByPersonID(personID);
            Assert.AreEqual(5, brokerageOfferRecords.Count);


            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_Sourced.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Accepted")
                .ClickServiceProvidedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(serviceProvidedId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickRateUnitLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per 1 Hour")
                .TapSearchButton()
                .SelectResultElement(rateUnitId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Offers cannot be accepted when the Brokerage Episode status is not Approved.");

        }

        [TestProperty("JiraIssueID", "CDV6-13520")]
        [Description("Open active social care case record with Brokerage episode with Approved status and Brokerage offer with all the status" +
           "Change the Brokerage Episode to completed and validate the existing offers can be updated and it should not be Accepted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch4_UITestMethod03()
        {
            CreateServiceElement_RateUnit();

            var sourcedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day);

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Services Provided

            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1Id, serviceElement2Id, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, new DateTime(2021, 10, 9), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            #endregion

            #region Brokerage Offer

            var brokerageOfferId_New = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);

            var brokerageOfferId_NewToRejected = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);

            var brokerageOfferId_NewToCancelled = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);

            var brokerageOfferId_UnderReview = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_UnderReview, 4); //update status to under review

            var brokerageOfferId_UnderReviewToRejected = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_UnderReviewToRejected, 4); //update status to under review

            var brokerageOfferId_UnderReviewToCancelled = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_UnderReviewToCancelled, 4); //update status to under review


            var brokerageOfferId_Sourced = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_Sourced, 4); //update status to under review
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(brokerageOfferId_Sourced, 5, DateTime.Now.Date); //set the status to Sourced. This will automatically update the brokerage episode status as well


            var brokerageOfferId_SourcedToRejected = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_SourcedToRejected, 4); //update status to under review
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(brokerageOfferId_SourcedToRejected, 5, DateTime.Now.Date); //set the status to Sourced. This will automatically update the brokerage episode status as well

            var brokerageOfferId_SourcedToCancelled = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_SourcedToCancelled, 4); //update status to under review
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(brokerageOfferId_SourcedToCancelled, 5, DateTime.Now.Date); //set the status to Sourced. This will automatically update the brokerage episode status as well

            var brokerageOfferId_Rejected = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.RejectBrokerageOffer(brokerageOfferId_Rejected, 2, brokerageOfferRejectionReasonID);//Rejected offer

            var brokerageOfferId_Cancelled = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.CancelBrokerageOffer(brokerageOfferId_Cancelled, 3, _brokerageOfferCancellationReasonId); //update status to under Cancelled


            var brokerageOfferId_Accepted = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_Accepted, 4); //update status to under review
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(brokerageOfferId_Accepted, 5, DateTime.Now.Date); //set the status to Sourced. This will automatically update the brokerage episode status as well

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1Id, serviceElement2Id, DateTime.Now.Date.AddMinutes(05), null); //update  Brokerage Episode status to Approved

            dbHelper.brokerageOffer.UpdateBrokerageOfferToAcceptedStatus(brokerageOfferId_Accepted, 6, rateUnit, serviceProvidedId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            CreatelocalizeStringValueIfNotAvailable();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .SelectStatus("Completed")
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Are you sure that you have completed the Brokerage checklist?")
                .TapOKButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad();


            //****************************************************

            //Brokerage Offer status Update from New -> Under review -> Sourced -> Accepted Validate the Message displayed

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_New.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Under Review")
                .ClickSaveButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Sourced")
                .InsertSourcedDateTime(sourcedDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture), "20:30")
                .ClickSaveButton();


            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Accepted")
                .ClickRateUnitLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per 1 Hour")
                .TapSearchButton()
                .SelectResultElement(rateUnit.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickServiceProvidedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(serviceProvidedId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Offers cannot be accepted when the Brokerage Episode status is not Approved.").TapCloseButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Sourced")
                .ClickSaveAndCloseButton();

            //****************************************************

            //Brokerage Offer status Update from Under review -> Sourced -> Accepted Validate the Message displayed


            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_UnderReview.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Sourced")
                .InsertSourcedDateTime(sourcedDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture), "20:30")
                .ClickSaveButton();


            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Accepted")
                .ClickRateUnitLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per 1 Hour")
                .TapSearchButton()
                .SelectResultElement(rateUnit.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickServiceProvidedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(serviceProvidedId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Offers cannot be accepted when the Brokerage Episode status is not Approved.").TapCloseButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Sourced")
                .ClickSaveAndCloseButton();

            //Update Brokerage offer Sourced to Accepted

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_Sourced.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Accepted")
                .ClickRateUnitLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per 1 Hour").TapSearchButton()
                .SelectResultElement(rateUnit.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickServiceProvidedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(serviceProvidedId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Offers cannot be accepted when the Brokerage Episode status is not Approved.").TapCloseButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Sourced")
                .ClickSaveAndCloseButton();


            //****************************************************

            //Brokerage offer update from New Status to Rejected 



            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_NewToRejected.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Rejected")
                .ClickRejectionReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Reason")
                .TapSearchButton()
                .SelectResultElement(brokerageOfferRejectionReasonID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            //Brokerage offer update from New Status to Cancelled 



            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_NewToCancelled.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Cancelled")
                .ClickCancellationReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Citizen not content with offer")
                .TapSearchButton()
                .SelectResultElement(_brokerageOfferCancellationReasonId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();


            //****************************************************

            //Brokerage offer update from Under Review Status to Rejected 


            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_UnderReviewToRejected.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Rejected")
                .ClickRejectionReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Reason")
                .TapSearchButton()
                .SelectResultElement(brokerageOfferRejectionReasonID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            //Brokerage offer update from Under Review Status to Cancelled 



            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_UnderReviewToCancelled.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Cancelled")
                .ClickCancellationReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Citizen not content with offer")
                .TapSearchButton()
                .SelectResultElement(_brokerageOfferCancellationReasonId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            //****************************************************


            //Brokerage offer update from Sourced Status to Rejected 


            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_SourcedToRejected.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Rejected")
                .ClickRejectionReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Reason")
                .TapSearchButton()
                .SelectResultElement(brokerageOfferRejectionReasonID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            //Brokerage offer update from Sourced Status to Cancelled 


            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_SourcedToCancelled.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Cancelled")
                .ClickCancellationReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Citizen not content with offer")
                .TapSearchButton()
                .SelectResultElement(_brokerageOfferCancellationReasonId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

        }

        [TestProperty("JiraIssueID", "CDV6-13534")]
        [Description("Open active social care case record with Brokerage episode with Sourced status and Brokerage Offer record with Sourced status" +
            "Navigate to the Brokerage offer record and click new record button" + "Enter the Mandatory fields and save the record" +
            "Change the status to Sourced and enter the sourced date as earlier populated date and save the record" +
            "Navigate to the brokerage Episode and Validate the sourced date is autopopulated with the earliest date")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch4_UITestMethod04()
        {
            var receivedDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-10)).Date;
            var sourcedDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-3)).Date;
            var sourcedDate2 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-8)).Date;

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, new DateTime(2021, 10, 9), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            #endregion

            #region Brokerage Offer

            //To Create Brokerage Offer Record in Under Review Status
            var OfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(OfferId, 5, sourcedDate);//update Brokerage offer status to Sourced

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .InsertReceivedDateTime(receivedDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture), "09:30")
                .ClickProviderLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(providerID.ToString());


            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Sourced")
                .InsertSourcedDateTime(sourcedDate2.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture), "09:30")
                .ClickSaveAndCloseButton();


            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString()).OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ValidateSourcedDateTime(sourcedDate2.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture), "09:30");
        }

        [TestProperty("JiraIssueID", "CDV6-13535")]
        [Description("Open the Referance Date page and navigate to Brokerage Offer Cancellation Reasons Page " +
            "Open any one of the reasons and change the Default status for Episode to Yes and save the record" +
            "Verify the user should be able to do")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch4_UITestMethod05()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Brokerage Offer Cancellation Reasons")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Brokerage")
                .ClickReferenceDataElement("Brokerage Offer Cancellation Reasons");

            brokerageOfferCancellationReasons
                .WaitForBrokerageOfferCancellationReasonsPageToLoad()
                .InsertQuickSearchQuery("Citizen not content with offer")
                .TapSearchButton()
                .OpenBrokerageOfferCancellationReasonsRecord(_brokerageOfferCancellationReasonId.ToString());

            brokerageOfferCancellationReasonsRecordPage
                .WaitForBrokerageOfferCancellationReasonsRecordPageToLoad()
                .ClickDefaultForEpisodeCancellation_YesOption()
                .ClickSaveButton()
                .ClickDefaultForEpisodeCancellation_NoOption()
                .ClickSaveButton();

        }

        [TestProperty("JiraIssueID", "CDV6-13536")]
        [Description("Open active social care case record with Brokerage episode in Sourcing in Progress and Brokerage offer in all the status" +
            "Validate the Rejected and Cancelled Brokerage offer status" + "Enter the Cancellate date and time and Cancellation reason for the Brokerage episode and save" +
            "Validate the Brokerage Episode statud as Cancelled and validate the offers with status as cancelled for New, Underreview and Sourced")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch4_UITestMethod06()
        {
            var cancelledDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day);

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Brokerage Episode Cancellation Reason

            var episodeCancellationReason = dbHelper.brokerageEpisodeCancellationReason.GetByBrokerageEpisodeCancellationReasonName("Care no longer required")[0];

            #endregion

            #region Brokerage Offer Cancellation Reason

            commonMethodsDB.CreateBrokerageOfferCancellationReason("CDV6_13536" + DateTime.Now.ToString("yyyyMMddHHmmss"), new DateTime(2021, 1, 6), teamID, false, true);

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, new DateTime(2021, 10, 9), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            #endregion

            #region Brokerage Offer

            //To Create Brokerage Offer Records
            var brokerageOfferId_New = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);

            var brokerageOfferId_UnderReview = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_UnderReview, 4); //update status to under review

            var brokerageOfferId_Sourced = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_Sourced, 4); //update status to under review
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(brokerageOfferId_Sourced, 5, DateTime.Now.Date); //set the status to Sourced. This will automatically update the brokerage episode status as well

            var brokerageOfferId_Rejected = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.RejectBrokerageOffer(brokerageOfferId_Rejected, 2, brokerageOfferRejectionReasonID);//Rejected offer

            var brokerageOfferId_Cancelled = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.CancelBrokerageOffer(brokerageOfferId_Cancelled, 3, _brokerageOfferCancellationReasonId); //update status to under Cancelled

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
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
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_Rejected.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusSelectedText("Rejected")
                .ClickBackButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_Cancelled.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusSelectedText("Cancelled")
                .ClickBackButton();


            caseBrokerageEpisodeOffersPage

                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .WaitForCaseBrokerageEpisodeOffersDetailsPageToLoad()
                .ClickDetailsButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickRefreshButton()
                .ValidateStatusSelectedText("Sourced")
                .SelectStatus("Cancelled")
                .InsertCancellationDateTime(cancelledDate.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture), "20:30")
                .ClickCancellationReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Care no longer required")
                .TapSearchButton()
                .SelectResultElement(episodeCancellationReason.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ValidateStatusSelectedText("Cancelled")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_New.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusSelectedText("Cancelled")
                .ClickBackButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_UnderReview.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusSelectedText("Cancelled")
                .ClickBackButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_Sourced.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusSelectedText("Cancelled");

        }

        [TestProperty("JiraIssueID", "CDV6-13552")]
        [Description("Open active social care case record with Brokerage episode in Sourcing in Progress and Brokerage offer in Under review status" +
        "Open the brokerage offer and  change the status to Sourced " +
         "Validate the Sourced Date and time field as Mandatory")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch4_UITestMethod07()
        {
            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in progress

            #endregion

            #region Brokerage Offer

            //To Create Brokerage Offer Record in Under Review Status
            var OfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(OfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Sourced")
                .ValidateSourcedDateTimeFieldMandatoryVisible(true);
        }

        [TestProperty("JiraIssueID", "CDV6-13587")]
        [Description("Open active social care case record with Brokerage episode with Approved status and Brokerage offer with Sourced status" +
         "Enter the Rate unit field and Provider field and save the record" + "Validate the offer status is Acccepted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch4_UITestMethod08()
        {
            CreateServiceElement_RateUnit();

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Services Provided

            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, systemUserId, providerID,
            serviceElement1Id, serviceElement2Id, null, null, null, null, 2, false, 1, false);

            #endregion

            #region Brokerage Episode

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, new DateTime(2021, 10, 9), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update Brokerage Episode status to Sourcing in Progress

            #endregion

            #region Brokerage Offer

            var brokerageOfferId_Sourced = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId_Sourced, 4); //update status to under review
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(brokerageOfferId_Sourced, 5, DateTime.Now.Date); //set the status to Sourced. This will automatically update the brokerage episode status as well


            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(episodeId, 6, serviceElement1Id, serviceElement2Id, DateTime.Now.Date.AddMinutes(05), null); //update  Brokerage Episode status to Approved

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToBrokerageEpisodes();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateNumberOfOffersRecieved("1")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId_Sourced.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickRateUnitLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per 1 Hour")
                .TapSearchButton()
                .SelectResultElement(rateUnit.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickServiceProvidedLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(serviceProvidedId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusSelectedText("Accepted");
        }

        [TestProperty("JiraIssueID", "CDV6-13640")]
        [Description("Go to Advannced Search and Select the record type as Brokerage Episodes and Saved views as My Open Episodes ,Team Open Episodes,All Open Episodes.All Completed Episodes and validate the records displayed" +
            "Validate the Sort Request Received date and time")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch4_UITestMethod09()
        {
            var brokerageEpisode_NewStatus = new Guid("0ff01822-cf3d-eb11-a2e5-0050569231cf");// New
            _systemUserName = "CDV6_13640" + DateTime.Now.ToString("yyyyMMddHHmmss");
            systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CDV6_13640", DateTime.Now.ToString("yyyyMMddHHmmss"), "Passw0rd_!", _businessUnitId, teamID, _languageId, _authenticationproviderid);

            //To create Brokerage Episode Record
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisodeWithResposibleUser(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, new DateTime(2021, 10, 9), null, status, 0, 0, contractType, false, false, false, false, true, true, false, systemUserId);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            //Validate My open Eisodes with New Status
            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Brokerage Episodes")
                .SelectSavedView("My Open Episodes")
                .SelectFilter("1", "Status")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            optionSetFormPopUp.WaitForOptionSetFormPopUpToLoad().TypeSearchQuery("New").TapSearchButton().SelectResultElement(brokerageEpisode_NewStatus.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .ValidateSearchResultRecordPresent(episodeId.ToString());

            //Validate search result for  Team Open Episodes        
            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Brokerage Episodes")
                .SelectSavedView("Team Open Episodes")
                .SelectFilter("1", "Status")
                .SelectOperator("1", "Not In")
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            //Validate search result for All Open Episodes    
            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Brokerage Episodes")
                .SelectSavedView("All Open Episodes")
                .SelectFilter("1", "Status")
                .SelectOperator("1", "Not In")
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            //Validate search result for All Completed Episodes 
            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Brokerage Episodes")
                .SelectSavedView("All Completed Episodes")
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            //Validate Sort Request received date
            //To create Brokerage Episode Record
            var episode1Id = dbHelper.brokerageEpisode.CreateBrokerageEpisodeWithResposibleUser(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now, null, status, 0, 0, contractType, false, false, false, false, true, true, false, systemUserId);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Brokerage Episodes")
                .SelectSavedView("My Open Episodes")
                .SelectFilter("1", "Status")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            optionSetFormPopUp.WaitForOptionSetFormPopUpToLoad().TypeSearchQuery("New").TapSearchButton().SelectResultElement(brokerageEpisode_NewStatus.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .ClickRequestReceivedDateSortButton()
                .ValidateSearchResultRecordPresent(episodeId.ToString())
                .WaitForResultsPageToLoad()
                .ClickRequestReceivedDateSortButton()
                .ValidateSearchResultRecordPresent(episode1Id.ToString());

        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        private void CreateServiceElement_RateUnit()
        {
            #region  Rate Type

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            if (!dbHelper.rateType.GetByName("Units (Part)").Any())
                dbHelper.rateType.CreateRateType(teamID, teamID, "Units (Part)", code1, new DateTime(2020, 1, 1), 5, 6, 7);
            var rateType = dbHelper.rateType.GetByName("Units (Part)")[0];

            if (!dbHelper.rateType.GetByName("Hours (One only)").Any())
                dbHelper.rateType.CreateRateType(teamID, teamID, "Hours (One only)", code, new DateTime(2020, 1, 1), 5, 6, 7);
            var rateTypeId = dbHelper.rateType.GetByName("Hours (One only)")[0];

            #endregion

            #region  Rate Unit

            if (!dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _businessUnitId, "Per 1 Hour Unit", new DateTime(2020, 1, 1), code1, rateType);
            rateUnit = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();

            if (!dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (One only)").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _businessUnitId, "Per 1 Hour", new DateTime(2020, 1, 1), code, rateTypeId);
            rateUnitId2 = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (One only)").FirstOrDefault();

            #endregion

            #region Service Elemet 1 & 2

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault());

            var serviceElement1Name = "CDV6_13502_SE1" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            serviceElement1Id = dbHelper.serviceElement1.CreateServiceElement1(teamID, serviceElement1Name, new DateTime(2021, 1, 1), code, 1, 1, validRateUnits, rateUnit);

            var serviceElement2Name = "CDV6_13502_SE2" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            serviceElement2Id = dbHelper.serviceElement2.CreateServiceElement2(teamID, serviceElement2Name, new DateTime(2021, 1, 1), code);

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1Id, serviceElement2Id);

            #region Service Permissions

            dbHelper.servicePermission.CreateServicePermission(teamID, _businessUnitId,
                serviceElement1Id, teamID);

            #endregion

            #endregion

        }

        private void CreatelocalizeStringValueIfNotAvailable()
        {
            #region Localized String

            var LocalizedStringId = dbHelper.localizedString.GetByName("BrokerageEpisode.Message.BrokerageEpisodeCompletionChecklist").FirstOrDefault();

            #endregion

            #region Localized String Value

            var localizedStringValueId = dbHelper.localizedStringValue.GetByPlainText("Are you sure that you have completed the Brokerage checklist?").FirstOrDefault();

            #endregion

            if (localizedStringValueId == Guid.Empty)
            {
                mainMenu
                    .WaitForMainMenuToLoad()
                    .NavigateToCustomizationsSection();

                customizationsPage
                    .WaitForCustomizationsPageToLoad()
                    .ClickLocalizedStringsButton();

                localizedStringsPage
                    .WaitForLocalizedStringsPageToLoad()
                    .InsertQuickSearchText("BrokerageEpisode.Message.BrokerageEpisodeCompletionChecklist")
                    .ClickQuickSearchButton()
                    .OpenRecord(LocalizedStringId.ToString());

                localizedStringsRecordPage
                    .WaitForLocalizedStringsRecordPageToLoad()
                    .NavigateToLocalizedStringValuesPage();

                localizedStringValuesPage
                    .WaitForOptionsetValuesPageToLoad()
                    .ClickCreateNewRecord();

                localizedStringValuesRecordPage
                    .WaitForLocalizedStringValuesRecordPageToLoad()
                    .SelectLanguage(_languageId.ToString())
                    .InsertPlainText("Are you sure that you have completed the Brokerage checklist?")
                    .ClickSaveButton()
                    .WaitForRecordToBeSaved();

            }

        }
    }

}