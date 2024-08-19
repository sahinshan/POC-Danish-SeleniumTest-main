using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases.Related_Items
{
    [TestClass]
    public class Case_BrokerageEpisodes_RubanUITestCases2 : FunctionalTest
    {
        public Guid UpdateBrokerageEpisodeTrackingStatusScheduledJob { get { return new Guid("6300d270-5562-eb11-a312-0050569231cf"); } }

        private Guid _ethnicityId;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private Guid _businessUnitId;
        private Guid teamID;
        private string _teamName;
        private string _systemUserName;
        private Guid _systemUserId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid personID;
        private string _personFullName;
        private Guid _caseStatusId;
        private Guid dataFormId;
        private Guid caseID;
        private string caseNumber;
        private Guid sourceOfBrokerageRequestsID;
        private Guid sourceOfBrokerageRequestsID2;
        private Guid brokerageEpisodePriorityID;
        private int contractType = 1; // Spot
        private int status = 1; // new

        [TestInitialize]
        public void TestInitializationMethod()
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _teamName = "CareDirector QA";
                teamID = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region System User

                _systemUserName = "Case_Brokerage_Episodes_User2";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseBrokerageEpisodes", "User2", "Passw0rd_!", _businessUnitId, teamID, _languageId, _authenticationproviderid);

                #endregion

                #region Ethnicity

                if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                    dbHelper.ethnicity.CreateEthnicity(teamID, "Irish", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                #endregion

                #region Person

                var firstName = "Brokerage Episode";
                var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
                _personFullName = firstName + " " + lastName;
                personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, teamID);

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Adoption Enquiry", teamID);

                #endregion

                #region Contact Source

                if (!dbHelper.contactSource.GetByName("Other").Any())
                    dbHelper.contactSource.CreateContactSource(teamID, "Other", new DateTime(2020, 1, 1));
                _contactSourceId = dbHelper.contactSource.GetByName("Other")[0];

                #endregion

                #region Data Form

                dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Case

                caseID = dbHelper.Case.CreateSocialCareCaseRecord(teamID, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, dataFormId, _contactSourceId, DateTime.Now.Date, DateTime.Now.Date, 18);
                caseNumber = (string)dbHelper.Case.GetCaseByID(caseID, "casenumber")["casenumber"];

                #endregion

                #region Source of Brokerage Requests

                sourceOfBrokerageRequestsID = dbHelper.brokerageRequestSource.GetByName("Community Hospital")[0];

                sourceOfBrokerageRequestsID2 = dbHelper.brokerageRequestSource.GetByName("Acute Hospital")[0];

                #endregion

                #region Brokerage Episode Priority

                if (!dbHelper.brokerageEpisodePriority.GetByName("Priority").Any())
                    dbHelper.brokerageEpisodePriority.CreateBrokerageEpisodePriority(new Guid("8a5baf95-fc3d-eb11-a2e5-0050569231cf"), "Priority", new DateTime(2021, 1, 1), teamID);
                brokerageEpisodePriorityID = dbHelper.brokerageEpisodePriority.GetByName("Priority")[0];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13135

        //Script For Test Batch 5
        [TestProperty("JiraIssueID", "CDV6-13436")]
        [Description("Open active social care case record with Brokerage episode('Sourcing in Progress')" +
                     "Validate Refresh Page Option Is Visible ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod0001()
        {
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, 1, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

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
                .ValidateRefeshPageButtonVisbility(true);

        }

        [TestProperty("JiraIssueID", "CDV6-13437")]
        [Description("Open active social care case record with Brokerage episode('Sourcing in Progress')" +
                     "Navigate to Related Items Brokerage Offers and Create New Brokerage Offer With All Madatory Fields" +
                     "Validate Brokerage Offer is Created By Clicking on Save And Return To Previous Page" +
                     "Click On Details Tab in Brokerage Offer Page " +
                     "Enter any Field In Brokerage Episode And Click Save Option" +
                     "Validate Alert Message is displayed('Episode has been updated by System, please refresh to ensure latest changes are loaded.')" +
                     "Click Refresh Page Option and then Fill any Field In Brokerage Episode Record" +
                     "Click Save Option and then Validate Entered Field Is Updated in Brokerage Episode Record" +
                     "Navigate to Brokerage Offers and Create New Record and Validate Record is Created" +
                     "Navigate Back to Brokerage Episode Record Page And Validate Number Of offers Received Field value is updated as 2 ")]

        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod0002()
        {
            #region Service Elemet 1

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault());

            if (!dbHelper.serviceElement1.GetByName("01Jun21-SE1").Any())
                dbHelper.serviceElement1.CreateServiceElement1(teamID, "01Jun21-SE1", new DateTime(2021, 1, 1), code, 1, 1, validRateUnits);
            var serviceElement1 = dbHelper.serviceElement1.GetByName("01Jun21-SE1")[0];

            #endregion

            #region Provider

            var providerName = "Work Test";
            var providerid = dbHelper.provider.CreateProvider(providerName + DateTime.Now.ToString("yyyyMMddHHmmss"), teamID, 2); //creating a "Supplier" provider

            #endregion

            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, 1, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

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
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Under Review")
                .InsertReceivedDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickProviderLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(providerid.ToString());

            caseBrokerageEpisodeOfferRecordPage
               .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
               .ClickSaveAndCloseButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .WaitForCaseBrokerageEpisodeOffersDetailsPageToLoad()
                .ClickDetailsButton();
            /*---------------------------step3-------------------------*/

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickServiceElement1LookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("01Jun21-SE1")
                .TapSearchButton()
                .SelectResultElement(serviceElement1.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Episode has been updated by System, please refresh to ensure latest changes are loaded.")
                .TapCloseButton();
            /*---------------------------step4-------------------------*/

            caseBrokerageEpisodeRecordPage
                 .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                 .ClickRefreshButton();

            alertPopup
               .WaitForAlertPopupToLoad()
               .TapOKButton();

            caseBrokerageEpisodeRecordPage
                 .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                 .ValidateNumberOfOffersRecieved("1")
                 .ValidateServiceElement1LinkFieldText("")
                 .ClickServiceElement1LookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("01Jun21-SE1")
                .TapSearchButton()
                .SelectResultElement(serviceElement1.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickSaveButton()
                .ValidateServiceElement1LinkFieldText("01Jun21-SE1");

            /*---------------------------step5-------------------------*/

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Under Review")
                .InsertReceivedDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickProviderLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(providerName)
                .TapSearchButton()
                .SelectResultElement(providerid.ToString());

            caseBrokerageEpisodeOfferRecordPage
               .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
               .ClickSaveAndCloseButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad();

            var OfferRecords = dbHelper.brokerageOffer.GetBrokerageOfferByPersonID(personID);
            Assert.AreEqual(2, OfferRecords.Count);

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersDetailsPageToLoad()
                .ClickDetailsButton();

            /*---------------------------step6-------------------------*/

            caseBrokerageEpisodeRecordPage
                 .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                 .ClickRefreshButton()
                 .ValidateNumberOfOffersRecieved("2");

        }

        [TestProperty("JiraIssueID", "CDV6-13438")]
        [Description("Open active social care case record with Brokerage episode('Sourcing in Progress')" +
                     "Open Brokerage Episode Offer Record And Update the Status To Sourced" +
                     "Navigate Back To Brokerage Episode Record Page And Click On Refresh Page Option" +
                     "Validate Status Field is Updated To Sourced")]

        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod0003()
        {
            #region Provider

            var providerid = dbHelper.provider.CreateProvider("Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss"), teamID, 2); //creating a "Supplier" provider

            #endregion

            //Creating Brokerage Episode
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, 1, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

            //To create Brokerage Offer Records with Under review status
            var offerId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerid, DateTime.Now.AddDays(-4), 4, true);

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
                .OpenRecord(offerId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Sourced")
                .InsertSourcedDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:00")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersDetailsPageToLoad()
                .ClickDetailsButton();

            caseBrokerageEpisodeRecordPage
                 .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                 .ClickRefreshButton()
                 .ValidateStatusSelectedText("Sourced")
                 .ValidateNumberOfOffersRecieved("1");

        }

        [TestProperty("JiraIssueID", "CDV6-13439")]
        [Description("Open active social care case record with Brokerage episode Status as Completed" +
                     "Click Activate Button  " +
                     "Validate Refresh Page Button is Visbile")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod0004()
        {
            var sourcedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            #region Service Elemet 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault());

            if (!dbHelper.serviceElement1.GetByName("01Jun21-SE1").Any())
                dbHelper.serviceElement1.CreateServiceElement1(teamID, "01Jun21-SE1", new DateTime(2021, 1, 1), code, 1, 1, validRateUnits);
            var ServiceElement1Id = dbHelper.serviceElement1.GetByName("01Jun21-SE1")[0];

            if (!dbHelper.serviceElement2.GetByName("01Jun21-SE2").Any())
                dbHelper.serviceElement2.CreateServiceElement2(teamID, "01Jun21-SE2", new DateTime(2021, 1, 1), code);
            var ServiceElement2Id = dbHelper.serviceElement2.GetByName("01Jun21-SE2")[0];

            commonMethodsDB.CreateServiceMapping(teamID, ServiceElement1Id, ServiceElement2Id);

            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("Service Provision Start Reason Test").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _businessUnitId, "Service Provision Start Reason Test", new DateTime(2022, 1, 1), true);

            #endregion

            #region Provider

            var providerID = dbHelper.provider.CreateProvider("Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss"), teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Serivce Provider

            var rateunitid = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var serviceproviderId = dbHelper.serviceProvided.CreateServiceProvided(teamID, _systemUserId, providerID, ServiceElement1Id, ServiceElement2Id, null, null, null, null, 2, false);

            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(teamID, serviceproviderId, rateunitid, new DateTime(2022, 1, 1), 2);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(teamID, serviceProvidedRatePeriodId, serviceproviderId, 10m, 15m);

            var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var vatCodeId = dbHelper.vatCode.GetByName("Exempt")[0];

            if (!dbHelper.providerBatchGrouping.GetByName("Batching").Any())
                dbHelper.providerBatchGrouping.CreateProviderBatchGrouping("Batching", new DateTime(2023, 1, 1), teamID, false, false);
            var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching")[0];

            dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(teamID, serviceproviderId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);

            #endregion

            //Creating Completed Record For Brokerage Episode
            var episodeIdWithStatusCompleted = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusCompleted, 3); //update status to Sourcing in Progress
            var underReviewId1 = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeIdWithStatusCompleted, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(underReviewId1, 5, sourcedDate);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeStatusApproved(episodeIdWithStatusCompleted, 6, ServiceElement1Id, ServiceElement2Id, DateTime.Now); //Approved
            dbHelper.brokerageOffer.UpdateBrokerageOfferApprovedStatus(underReviewId1, 6, providerID, rateunitid, serviceproviderId);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusCompleted, 8); //Completed


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
                .OpenRecord(episodeIdWithStatusCompleted.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateStatusSelectedText("Completed")
                .ClickActivateButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.")
                .TapOKButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateRefeshPageButtonVisbility(true)
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(underReviewId1.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickUndoAccptanceButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .TapOKButton();
        }

        #endregion
    }

}