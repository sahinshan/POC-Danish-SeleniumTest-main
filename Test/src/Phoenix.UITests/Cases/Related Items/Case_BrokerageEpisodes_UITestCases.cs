using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases.Related_Items
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Automated Brokerage Episode From New to Sourcing - Pre Sync.Zip")]
    [DeploymentItem("Files\\SCA - Brokerage.Zip")]
    [DeploymentItem("Files\\Social Care Assessment Demo.Zip")]
    [DeploymentItem("Files\\Social Care Assessment Demo - BrokerageEpisode.Zip")]
    [DeploymentItem("Files\\Brokerage Assessment Creation - Schedule Time.Zip")]
    [DeploymentItem("Files\\Brokerage Assessment Creation - Schedule Time - Brokerage Episode.Zip")]
    [DeploymentItem("Files\\Brokerage episode creation with Scheduling.Zip")]
    public class Case_BrokerageEpisodes_UITestCases : FunctionalTest
    {
        public Guid UpdateBrokerageEpisodeTrackingStatusScheduledJob { get { return new Guid("6300d270-5562-eb11-a312-0050569231cf"); } }

        private Guid _ethnicityId;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private Guid systemUserId;
        private Guid _contactReasonId;
        private Guid _contactSourceId;
        private Guid personID;
        private int personnumber;
        private Guid _caseStatusId;
        private Guid _caseStatusId2;
        private Guid dataFormId;
        private Guid caseID;
        private string caseNumber;
        private Guid personRelationshipType;
        private Guid sourceOfBrokerageRequestsID_CommunityHospital;
        private Guid sourceOfBrokerageRequestsID_AcuteHospital;
        private Guid sourceOfBrokerageRequestsID_CommunityExistingCarePackage;
        private Guid sourceOfBrokerageRequestsID_AutomationCDV6_10338;
        private Guid brokerageEpisodePriorityID_Priority;
        private Guid brokerageEpisodePriorityID_Standard;
        private Guid brokerageEpisodePriorityID_Urgent;

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid teamID;
        private string _loginUserName;
        private Guid _loginUserId;
        private string _personName;
        private int contractType = 1; // Spot
        private int status = 1; // new
        private Guid _caseClosureReasonId;
        private Guid brokerageCommunicationWith1ID;
        private Guid brokerageCommunicationWith2ID;
        private Guid brokerageOfferCommunicationOutcomesId;
        private Guid contactMethodId;
        private Guid BrokerageExistingCarePackageId;
        private Guid brokerageCarePackageTypeID;
        private Guid brokerageOfferRejectionReasonID;

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

                teamID = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region System User AllActivitiesUser1

                _loginUserName = "BrokerageEpisodeUser1";
                _loginUserId = commonMethodsDB.CreateSystemUserRecord(_loginUserName, "BrokerageEpisode", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, teamID, _languageId, _authenticationproviderid);

                #endregion

                #region Person

                var DOB = new DateTime(DateTime.Now.AddYears(-18).Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                _personName = "Brokerage Episode" + DateTime.Now.ToString("yyyyMMddHHmmss");
                personID = commonMethodsDB.CreatePersonRecord(_personName, "", _ethnicityId, teamID, DOB);
                personnumber = (int)dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"];

                #endregion

                #region Case Status

                _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();
                _caseStatusId2 = dbHelper.caseStatus.GetByName("Closed & Logged As Enquiry").FirstOrDefault();

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

                caseID = dbHelper.Case.CreateSocialCareCaseRecord(teamID, personID, _loginUserId, _loginUserId, _caseStatusId, _contactReasonId, dataFormId, _contactSourceId, DateTime.Now.Date, DateTime.Now.Date, 18);
                caseNumber = (string)dbHelper.Case.GetCaseByID(caseID, "casenumber")["casenumber"];

                #endregion

                #region Source of Brokerage Requests

                sourceOfBrokerageRequestsID_CommunityHospital = dbHelper.brokerageRequestSource.GetByName("Community Hospital")[0];

                sourceOfBrokerageRequestsID_AcuteHospital = dbHelper.brokerageRequestSource.GetByName("Acute Hospital")[0];

                if (!dbHelper.brokerageRequestSource.GetByName("Community - Existing Care Package").Any())
                    dbHelper.brokerageRequestSource.CreateBrokerageRequestSource("Community - Existing Care Package", new DateTime(2023, 1, 1), teamID);
                sourceOfBrokerageRequestsID_CommunityExistingCarePackage = dbHelper.brokerageRequestSource.GetByName("Community - Existing Care Package").FirstOrDefault();

                if (!dbHelper.brokerageRequestSource.GetByName("Automation CDV6-10338").Any())
                    dbHelper.brokerageRequestSource.CreateBrokerageRequestSource("Automation CDV6-10338", new DateTime(2023, 1, 1), teamID);
                sourceOfBrokerageRequestsID_AutomationCDV6_10338 = dbHelper.brokerageRequestSource.GetByName("Automation CDV6-10338").FirstOrDefault();

                #endregion

                #region Brokerage Episode Priority

                if (!dbHelper.brokerageEpisodePriority.GetByName("Priority").Any())
                    dbHelper.brokerageEpisodePriority.CreateBrokerageEpisodePriority(new Guid("8a5baf95-fc3d-eb11-a2e5-0050569231cf"), "Priority", new DateTime(2021, 1, 1), teamID);
                brokerageEpisodePriorityID_Priority = dbHelper.brokerageEpisodePriority.GetByName("Priority")[0];

                if (!dbHelper.brokerageEpisodePriority.GetByName("Standard").Any())
                    dbHelper.brokerageEpisodePriority.CreateBrokerageEpisodePriority(new Guid("6569c2c9-fb3d-eb11-a2e5-0050569231cf"), "Standard", new DateTime(2021, 1, 1), teamID);
                brokerageEpisodePriorityID_Standard = dbHelper.brokerageEpisodePriority.GetByName("Standard")[0];

                if (!dbHelper.brokerageEpisodePriority.GetByName("Urgent").Any())
                    dbHelper.brokerageEpisodePriority.CreateBrokerageEpisodePriority(new Guid("865baf95-fc3d-eb11-a2e5-0050569231cf"), "Urgent", new DateTime(2021, 1, 1), teamID);
                brokerageEpisodePriorityID_Urgent = dbHelper.brokerageEpisodePriority.GetByName("Urgent")[0];

                #endregion

                #region Workflow 

                commonMethodsDB.CreateWorkflowIfNeeded("Automated Brokerage Episode From New to Sourcing - Pre Sync", "Automated Brokerage Episode From New to Sourcing - Pre Sync.Zip");

                #endregion

                #region Case Closure Reason

                if (!dbHelper.caseClosureReason.GetByName("CDV6-12966_Reason").Any())
                    dbHelper.caseClosureReason.CreateCaseClosureReason(teamID, "CDV6-12966_Reason", "", "", new DateTime(2021, 1, 1), 110000000, true);
                _caseClosureReasonId = dbHelper.caseClosureReason.GetByName("CDV6-12966_Reason").FirstOrDefault();

                #endregion

                #region Brokerage Care Type 

                if (!dbHelper.brokerageCarePackageType.GetByName("Day care").Any())
                    dbHelper.brokerageCarePackageType.CreateBrokerageCarePackageType(new Guid("7afa1e7d-006a-eb11-a312-0050569231cf"), "Day care", new DateTime(2021, 1, 1), teamID);
                brokerageCarePackageTypeID = dbHelper.brokerageCarePackageType.GetByName("Day care").FirstOrDefault();

                #endregion

                #region Brokerage Offer Awaiting Communication From (Brokerage Communication With)

                brokerageCommunicationWith1ID = dbHelper.brokerageOfferAwaitingCommunicationFrom.GetByName("Commissioning")[0];
                brokerageCommunicationWith2ID = dbHelper.brokerageOfferAwaitingCommunicationFrom.GetByName("Brokerage")[0];

                #endregion

                #region Brokerage Offer Communication Outcome

                brokerageOfferCommunicationOutcomesId = dbHelper.brokerageOfferCommunicationOutcome.GetByName("Follow-up required")[0];

                #endregion

                #region Contact Method

                contactMethodId = dbHelper.contactMethod.GetByName("Any")[0];

                #endregion

                #region Brokerage Existing Care Package

                BrokerageExistingCarePackageId = dbHelper.brokerageExistingCarePackage.GetByName("No existing care package")[0];

                #endregion

                #region Bank Holiday

                if (!dbHelper.bankHoliday.GetByHolidayDate(new DateTime(2021, 1, 26)).Any())
                    dbHelper.bankHoliday.CreateBankHoliday("Republic day 2022", new DateTime(2021, 1, 26), "Republic day 2022");

                #endregion

                #region Brokerage Episode Tracking Status

                if (!dbHelper.brokerageEpisodeTrackingStatus.GetByName("Green_Good").Any())
                    dbHelper.brokerageEpisodeTrackingStatus.CreateBrokerageEpisodeTrackingStatus("Green_Good", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), teamID);
                var brokerageEpisodeTrackingStatusId = dbHelper.brokerageEpisodeTrackingStatus.GetByName("Green_Good")[0];

                if (!dbHelper.brokerageEpisodeTrackingStatus.GetByName("Oragne_Still OK").Any())
                    dbHelper.brokerageEpisodeTrackingStatus.CreateBrokerageEpisodeTrackingStatus("Oragne_Still OK", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), teamID);
                var brokerageEpisodeTrackingStatusId2 = dbHelper.brokerageEpisodeTrackingStatus.GetByName("Oragne_Still OK")[0];

                if (!dbHelper.brokerageEpisodeTrackingStatus.GetByName("Red_Negative").Any())
                    dbHelper.brokerageEpisodeTrackingStatus.CreateBrokerageEpisodeTrackingStatus("Red_Negative", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), teamID);
                var brokerageEpisodeTrackingStatusId3 = dbHelper.brokerageEpisodeTrackingStatus.GetByName("Red_Negative")[0];

                #endregion

                #region Brokerage Target Setup

                if (!dbHelper.brokerageTargetSetup.GetByName("Community - Existing Care Package / Standard").Any())
                    dbHelper.brokerageTargetSetup.CreateBrokerageEpisodeTargetSetup(sourceOfBrokerageRequestsID_CommunityExistingCarePackage, brokerageEpisodePriorityID_Standard, 2, new DateTime(2021, 1, 1), teamID, 4);
                var brokerageTargetSetupId = dbHelper.brokerageTargetSetup.GetByName("Community - Existing Care Package / Standard")[0];

                if (!dbHelper.brokerageTargetSetup.GetByName("Automation CDV6-10338 / Standard").Any())
                    dbHelper.brokerageTargetSetup.CreateBrokerageEpisodeTargetSetup(sourceOfBrokerageRequestsID_AutomationCDV6_10338, brokerageEpisodePriorityID_Standard, 2, new DateTime(2021, 1, 1), teamID, 7);
                var brokerageTargetSetupId2 = dbHelper.brokerageTargetSetup.GetByName("Automation CDV6-10338 / Standard")[0];

                if (!dbHelper.brokerageTargetSetup.GetByName("Acute Hospital / Standard").Any())
                    dbHelper.brokerageTargetSetup.CreateBrokerageEpisodeTargetSetup(sourceOfBrokerageRequestsID_AcuteHospital, brokerageEpisodePriorityID_Standard, 1, new DateTime(2021, 1, 1), teamID, 7);
                var brokerageTargetSetupId3 = dbHelper.brokerageTargetSetup.GetByName("Acute Hospital / Standard")[0];

                if (!dbHelper.brokerageTargetSetup.GetByName("Acute Hospital / Priority").Any())
                    dbHelper.brokerageTargetSetup.CreateBrokerageEpisodeTargetSetup(sourceOfBrokerageRequestsID_AcuteHospital, brokerageEpisodePriorityID_Priority, 2, new DateTime(2021, 3, 1), teamID, 7);
                var brokerageTargetSetupId4 = dbHelper.brokerageTargetSetup.GetByName("Acute Hospital / Priority")[0];

                #endregion

                #region Brokerage Target Tracking Status Setup

                if (!dbHelper.brokerageTargetTrackingStatusSetup.GetByName("Green_Good / 6").Any())
                    dbHelper.brokerageTargetTrackingStatusSetup.CreateBrokerageTargetTrackingStatusSetup(brokerageTargetSetupId4, brokerageEpisodeTrackingStatusId, 6, teamID);

                if (!dbHelper.brokerageTargetTrackingStatusSetup.GetByName("Oragne_Still OK / 3").Any())
                    dbHelper.brokerageTargetTrackingStatusSetup.CreateBrokerageTargetTrackingStatusSetup(brokerageTargetSetupId4, brokerageEpisodeTrackingStatusId2, 3, teamID);

                if (!dbHelper.brokerageTargetTrackingStatusSetup.GetByName("Red_Negative / -4").Any())
                    dbHelper.brokerageTargetTrackingStatusSetup.CreateBrokerageTargetTrackingStatusSetup(brokerageTargetSetupId4, brokerageEpisodeTrackingStatusId3, -4, teamID);

                #endregion

                #region Brokerage Offer Rejection Reason

                if (!dbHelper.brokerageOfferRejectionReason.GetByName("Reason_1").Any())
                    dbHelper.brokerageOfferRejectionReason.CreateBrokerageOfferRejectionReason("Reason_1", new DateTime(2021, 1, 6), teamID);
                brokerageOfferRejectionReasonID = dbHelper.brokerageOfferRejectionReason.GetByName("Reason_1")[0];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-12832

        [TestProperty("JiraIssueID", "CDV6-12966")]
        [Description("New button not visible in closed Cases")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod001()
        {
            #region Person

            var DOB = new DateTime(DateTime.Now.AddYears(-18).Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _personName = "Brokerage Episode" + DateTime.Now.ToString("yyyyMMddHHmmss");
            personID = commonMethodsDB.CreatePersonRecord(_personName, "", _ethnicityId, teamID, DOB);

            #endregion

            #region Create Case

            caseID = dbHelper.Case.CreateSocialCareCaseRecord(teamID, personID, systemUserId, systemUserId, _caseStatusId, _contactReasonId, dataFormId, _contactSourceId, DateTime.Now.Date, DateTime.Now.Date, 18);
            caseNumber = (string)dbHelper.Case.GetCaseByID(caseID, "casenumber")["casenumber"];
            dbHelper.Case.UpdateCaseRecordToClosed(caseID, _caseStatusId2, DateTime.Now.Date, _caseClosureReasonId, systemUserId, DateTime.Now.Date);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .ValidateNewRecordButtonVisibility(false);

        }

        [TestProperty("JiraIssueID", "CDV6-12967")]
        [Description("Try to create new record with future Request Received Date/Time - " +
            "Create new record and validate fields after the creation - " +
            "Validate Offers menu not visible for records with status equal to New - " +
            "Updating responsible user and type of care package should automatically update the record status to Sourcing in Progress - " +
            "if Status is Sourcing in Progress then Offers should be available from the left menu " +
            "")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod002()
        {
            DateTime requestedDate1 = DateTime.Now.AddDays(2);
            DateTime requestedDate2 = DateTime.Now.AddDays(-2);

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .ClickNewRecordButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .InsertRequestReceivedDateTime(requestedDate1.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "10:00")
                .ClickSourceOfRequestLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Community Hospital").TapSearchButton().SelectResultElement(sourceOfBrokerageRequestsID_CommunityHospital.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .ClickPriorityLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Priority").TapSearchButton().SelectResultElement(brokerageEpisodePriorityID_Priority.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Request Received Date/Time cannot be in future.").TapCloseButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .InsertRequestReceivedDateTime(requestedDate2.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture), "10:00")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad();

            System.Threading.Thread.Sleep(1000);

            var episodes = dbHelper.brokerageEpisode.GetBrokerageEpisodeByCaseID(caseID);
            Assert.AreEqual(1, episodes.Count);
            var newEpisodeid = episodes[0];

            caseBrokerageEpisodesPage
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateCaseLinkFieldText(", " + _personName + " - (" + DateTime.Now.AddYears(-18).ToString("dd'/'MM'/'yyyy") + ") [" + caseNumber + "]")
                .ValidatePersonLinkFieldText(_personName)
                .ValidateStatusSelectedText("New")
                .ValidateRequestReceivedDateTime(requestedDate2.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "10:00")
                .ValidateSourceOfRequestLinkFieldText("Community Hospital")
                .ValidatePriorityLinkFieldText("Priority")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")

                .ValidateBrokerageOffersLeftMenuVisibility(false)

                .ClickResponsibleUserLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_loginUserName).TapSearchButton().SelectResultElement(_loginUserId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickTypeOFCarePackageLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Day care").TapSearchButton().SelectResultElement(brokerageCarePackageTypeID.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateStatusSelectedText("Sourcing in Progress")
                .ValidateResponsibleUserLinkFieldText("BrokerageEpisode User1")
                .ValidateTypeOFCarePackageLinkFieldText("Day care")

                .ValidateStatusOptionDisabled("Ready for Approval", true)
                .ValidateStatusOptionDisabled("Approved", true)

                .ValidateBrokerageOffersLeftMenuVisibility(true)

                .ValidateNumberOfOffersRecievedFieldDisabled(true)
                .ValidateNumberOfOffersRecieved("0");

        }

        [TestProperty("JiraIssueID", "CDV6-12975")]
        [Description("Validate default fields for new record - " +
            "try to save without mandatory fields - " +
            "try to save with future received date - " +
            "Set received date for today; add Communication Log and save the record - " +
            "Update status to rejected;save the record; Validate that record is disabled; validate that Brokerage Offer Communications cannot be added - " +
            "Create a new Canceled Offer; Validate that the record gets disabled after saving it")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod003()
        {
            #region Brokerage Offer Cancellation Reason

            if (!dbHelper.brokerageOfferCancellationReason.GetByName("Citizen not content with offer").Any())
                dbHelper.brokerageOfferCancellationReason.CreateBrokerageOfferCancellationReason("Citizen not content with offer", new DateTime(2021, 1, 6), teamID);
            var brokerageOfferCancellationReasons = dbHelper.brokerageOfferCancellationReason.GetByName("Citizen not content with offer")[0];

            #endregion

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID_CommunityHospital, brokerageEpisodePriorityID_Priority, DateTime.Now.Date, null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress


            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateBrokerageEpisodeLinkFieldText("Brokerage episode for " + _personName + " received on ")
                .ValidateStatusSelectedText("Under Review")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateSourcedDateTimeFieldVisibility(false)
                .ValidateRateUnitFieldVisibility(false)
                .ValidateServiceProvidedFieldVisibility(false)

                .ClickSaveAndCloseButton()
                .ValidateMessageAreaVisible(true)
                .ValidateReceivedDateTimeFieldErrorLabelVisibility(true)
                .ValidateProviderFieldErrorLabelVisibility(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateReceivedDateTimeFieldErrorLabelText("Please fill out this field.")
                .ValidateProviderFieldErrorLabelText("Please fill out this field.")

                .InsertReceivedDateTime(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), "09:00")
                .ClickProviderLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(providerName).TapSearchButton().SelectResultElement(providerID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Received Date/Time cannot be in the future.").TapCloseButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("New")
                .InsertReceivedDateTime(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:19")
                .ClickAwaitingCommunicationFromLookUpButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Commissioning").TapSearchButton().AddElementToList(brokerageCommunicationWith1ID.ToString())
                .TypeSearchQuery("Brokerage").TapSearchButton().AddElementToList(brokerageCommunicationWith2ID.ToString())
                .TapOKButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad();

            var offers = dbHelper.brokerageOffer.GetByBrokerageEpisodeId(episodeId);
            Assert.AreEqual(1, offers.Count);
            var newOfferId = offers.FirstOrDefault();

            caseBrokerageEpisodeOffersPage
                .OpenRecord(newOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad("Brokerage Offer from " + providerName + " received on")
                .ValidateBrokerageEpisodeLinkFieldText("Brokerage episode for " + _personName + " received on ")
                .ValidateStatusSelectedText("New")
                .ValidateReceivedDateTime(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:19")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateProviderRegisteredInCareDirector(true)
                .ValidateProviderLinkFieldText(providerName)
                .ValidateCostPerWeekValue("")
                .ValidateRejectionReasonLinkFieldText("")
                .ValidateCancellationReasonLinkFieldText("")
                .ValidateAwaitingCommunicationFromOptionText(brokerageCommunicationWith1ID.ToString(), "Commissioning\r\nRemove")
                .ValidateAwaitingCommunicationFromOptionText(brokerageCommunicationWith2ID.ToString(), "Brokerage\r\nRemove")

                .SelectStatus("Rejected")
                .ClickRejectionReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Reason_1").TapSearchButton().SelectResultElement(brokerageOfferRejectionReasonID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad("Brokerage Offer from " + providerName + " received on")
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoadDisabled("Brokerage Offer from " + providerName + " received on");

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad()
                .ValidateNewRecordButtonVisibility(false);

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad("Brokerage Offer from " + providerName + " received on")
                .ClickBackButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Cancelled")
                .InsertReceivedDateTime("01/09/2021", "10:00")
                .ClickProviderLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(providerName).TapSearchButton().SelectResultElement(providerID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickCancellationReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Citizen not content with offer").TapSearchButton().SelectResultElement(brokerageOfferCancellationReasons.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoadDisabled("Brokerage Offer from " + providerName + " received on ");

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad()
                .ValidateNewRecordButtonVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-12997")]
        [Description("Verify that is possible to create Offer records with Status of Under Review - " +
            "validate that is possible to add a communication record to the offer - " +
            "Validate that is possible to delete the communication record from the offer - " +
            "On the episode record the number of offers field should be updated to 1")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod004()
        {
            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID_CommunityHospital, brokerageEpisodePriorityID_Priority, DateTime.Now.Date, null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress


            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Under Review")
                .InsertReceivedDateTime("01/09/2021", "09:00")
                .ClickProviderLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(providerName).TapSearchButton().SelectResultElement(providerID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickAwaitingCommunicationFromLookUpButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Commissioning").TapSearchButton().AddElementToList(brokerageCommunicationWith1ID.ToString())
                .TypeSearchQuery("Brokerage").TapSearchButton().AddElementToList(brokerageCommunicationWith2ID.ToString())
                .TapOKButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad();

            var offers = dbHelper.brokerageOffer.GetByBrokerageEpisodeId(episodeId);
            Assert.AreEqual(1, offers.Count);
            var newOfferId = offers.FirstOrDefault();

            caseBrokerageEpisodeOffersPage
                .OpenRecord(newOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad("Brokerage Offer from " + providerName + " received on");

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad()
                .ClickNewRecordButton();

            caseBrokerageOfferCommunicationRecordPage
                .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
                .ValidateBrokerageOfferLinkFieldText("Brokerage Offer from " + providerName + " received on")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .InsertCommunicationDateTime("02/09/2021", "10:15")
                .ClickCommunicationWithLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Commissioning").TapSearchButton().SelectResultElement(brokerageCommunicationWith1ID.ToString());

            caseBrokerageOfferCommunicationRecordPage
                .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
                .ClickContactMethodLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Any").TapSearchButton().SelectResultElement(contactMethodId.ToString());

            caseBrokerageOfferCommunicationRecordPage
                .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
                .InsertSubject("Communication 001")
                .InsertCommunicationDetails("details ...")
                .ClickOutcomeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Follow-up required").TapSearchButton().SelectResultElement(brokerageOfferCommunicationOutcomesId.ToString());

            caseBrokerageOfferCommunicationRecordPage
                .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad();

            var communications = dbHelper.brokerageOfferCommunication.GetByBrokerageOfferId(newOfferId);
            Assert.AreEqual(1, communications.Count);
            var newCommunicationId = communications.FirstOrDefault();

            caseBrokerageOfferCommunicationsSubArea
                .ValidateRecordVisible(newCommunicationId.ToString())
                .ValidateRecordCellText(newCommunicationId.ToString(), 2, "Commissioning")
                .ValidateRecordCellText(newCommunicationId.ToString(), 3, "02/09/2021 10:15:00")
                .ValidateRecordCellText(newCommunicationId.ToString(), 4, "details ...")
                .ValidateRecordCellText(newCommunicationId.ToString(), 5, "Any")
                .ValidateRecordCellText(newCommunicationId.ToString(), 6, "Follow-up required")

                .SelectRecord(newCommunicationId.ToString())
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad();

            communications = dbHelper.brokerageOfferCommunication.GetByBrokerageOfferId(newOfferId);
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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateNumberOfOffersRecieved("1");

        }

        [TestProperty("JiraIssueID", "CDV6-13029")]
        [Description("Deleting an Offer should automatically update the 'Number of Offers Received' field on the Episode record - ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod005()
        {
            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Related Person

            var currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            var relatedPersonID = dbHelper.person.CreatePersonRecord("", "Henrietta", currentDateTime, "Walls", "", DateTime.Now.Date.AddYears(-18), _ethnicityId, teamID, 1, 1);

            dbHelper.personRelationship.CreatePersonRelationship(teamID,
                personID, "van Episode" + DateTime.Now.Date,
                personRelationshipType, "Friend",
                relatedPersonID, "Henrietta Walls" + DateTime.Now.Date,
                personRelationshipType, "Friend",
                DateTime.Now.Date, "desc ...", 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);

            #endregion


            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID_CommunityHospital, brokerageEpisodePriorityID_Priority, DateTime.Now.Date, null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

            var cancelledOfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.Date, 3, true);
            var newOfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.Date, 1, true);
            var underReviewOfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, DateTime.Now.Date, 4, true);


            /************************* Step 2 *************************/
            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateNumberOfOffersRecieved("3")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .SelectRecord(cancelledOfferId.ToString())
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad();


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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateNumberOfOffersRecieved("2")
                .NavigateToBrokerageOffersSubPage();

            /************************* Step 2 *************************/



            /************************* Step 3 *************************/

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(newOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusOptionDisabled("New", false)
                .ValidateStatusOptionDisabled("Rejected", false)
                .ValidateStatusOptionDisabled("Cancelled", false)
                .ValidateStatusOptionDisabled("Under Review", false)
                .ValidateStatusOptionDisabled("Sourced", true)
                .ValidateStatusOptionDisabled("Accepted", true)
                .ClickBackButton();

            /************************* Step 3 *************************/



            /************************* Step 4 *************************/

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(underReviewOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusOptionDisabled("New", false)
                .ValidateStatusOptionDisabled("Rejected", false)
                .ValidateStatusOptionDisabled("Cancelled", false)
                .ValidateStatusOptionDisabled("Under Review", false)
                .ValidateStatusOptionDisabled("Sourced", false)
                .ValidateStatusOptionDisabled("Accepted", true)
                .ClickBackButton();

            /************************* Step 4 *************************/


            /************************* Step 5 *************************/

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickContactPersonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TapSearchButton().SelectResultElement(relatedPersonID.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateContactPersonLinkFieldText("Henrietta Walls");

            /************************* Step 5 *************************/


            /************************* Step 6 *************************/

            caseBrokerageEpisodeRecordPage
                .ClickContactRegisteredInCareDirectorNoRadioButton()
                .ValidateContactPersonFieldVisibility(false)
                .InsertContactName("Contact Name ...")
                .InsertRelationshipToCitizen("Relationship to Citizen ...")
                .InsertContactAddress("Contact Address ...")
                .InsertContactPhoneNumber("98767890")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateContactPersonFieldVisibility(false)
                .ValidateContactRegisteredInCareDirector(false)
                .ValidateContactName("Contact Name ...")
                .ValidateRelationshipToCitizen("Relationship to Citizen ...")
                .ValidateContactAddress("Contact Address ...")
                .ValidateContactPhoneNumber("98767890");

            /************************* Step 6 *************************/


            /************************* Step 7 *************************/

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .InsertRequestedLocalAuthorityFunding("9999999999999999")
                .InsertAgreedLocalAuthorityFunding("9999999999999999")
                .InsertThirdPartyTopUpFunding("9999999999999999")
                .InsertContinuingHealthcareFunding("9999999999999999")
                .InsertFundedNursingCare("9999999999999999")
                .InsertOtherFunding("9999999999999999")
                .ClickSaveButton()


                .ValidateMessageAreaVisible(true)
                .ValidateRequestedLocalAuthorityFundingFieldErrorLabelVisibility(true)
                .ValidateAgreedLocalAuthorityFundingFieldErrorLabelVisibility(true)
                .ValidateThirdPartyTopUpFundingFieldErrorLabelVisibility(true)
                .ValidateContinuingHealthcareFundingFieldErrorLabelVisibility(true)
                .ValidateFundedNursingCareFieldErrorLabelVisibility(true)
                .ValidateOtherFundingFieldErrorLabelVisibility(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateRequestedLocalAuthorityFundingFieldErrorLabelText("Please enter a value between 0 and 999999999.")
                .ValidateAgreedLocalAuthorityFundingFieldErrorLabelText("Please enter a value between 0 and 999999999.")
                .ValidateThirdPartyTopUpFundingFieldErrorLabelText("Please enter a value between 0 and 999999999.")
                .ValidateContinuingHealthcareFundingFieldErrorLabelText("Please enter a value between 0 and 999999999.")
                .ValidateFundedNursingCareFieldErrorLabelText("Please enter a value between 0 and 999999999.")
                .ValidateOtherFundingFieldErrorLabelText("Please enter a value between 0 and 999999999.")
                ;


            /************************* Step 7 *************************/




            /************************* Step 8 *************************/

            caseBrokerageEpisodeRecordPage
                .InsertRequestedLocalAuthorityFunding("123")
                .InsertAgreedLocalAuthorityFunding("123")
                .InsertThirdPartyTopUpFunding("123")
                .InsertContinuingHealthcareFunding("123")
                .InsertFundedNursingCare("123")
                .InsertOtherFunding("123")
                .ValidateTotalFunding("615");

            /************************* Step 8 *************************/



            /************************* Step 9 *************************/

            caseBrokerageEpisodeRecordPage
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateRequestedLocalAuthorityFunding("123.00")
                .ValidateAgreedLocalAuthorityFunding("123.00")
                .ValidateThirdPartyTopUpFunding("123.00")
                .ValidateContinuingHealthcareFunding("123.00")
                .ValidateFundedNursingCare("123.00")
                .ValidateOtherFunding("123.00")
                .ValidateTotalFunding("615.00");


            /************************* Step 9 *************************/

        }

        [TestProperty("JiraIssueID", "CDV6-13063")]
        [Description("Set theReferral type to internal and select a system user as an Internal Referrer - ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod006()
        {
            #region Document / Document Business Object Mapping

            var documentID = commonMethodsDB.CreateDocumentIfNeeded("Social Care Assessment Demo", "Social Care Assessment Demo.Zip");
            commonMethodsDB.CreateDocumentBusinessObjectMapping("Social Care Assessment Demo - BrokerageEpisode", "Social Care Assessment Demo - BrokerageEpisode.Zip");

            #endregion

            #region

            var workflowID = commonMethodsDB.CreateWorkflowIfNeeded("SCA - Brokerage", "SCA - Brokerage.Zip");

            #endregion

            #region Episode

            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID_CommunityHospital, brokerageEpisodePriorityID_Priority, DateTime.Now.Date, null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

            #endregion

            /************************* Step 2 *************************/
            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .SelectReferralType("Internal")
                .ClickInternalReferrerLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_loginUserName).TapSearchButton().SelectResultElement(_loginUserId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateInternalReferrerLinkFieldText("BrokerageEpisode User1");


            /**********************************************************/



            /************************* Step 3 *************************/


            caseBrokerageEpisodeRecordPage
                .ClickInternalReferrerLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectBusinessObjectByText("Teams").SelectViewByText("Lookup View").TypeSearchQuery("CareDirector QA").TapSearchButton().SelectResultElement(teamID.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateInternalReferrerLinkFieldText("CareDirector QA");


            /**********************************************************/



            /************************* Step 4 *************************/


            caseBrokerageEpisodeRecordPage
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad();

            var caseFormId = dbHelper.caseForm.CreateCaseFormRecord(teamID, documentID, personID, caseID, new DateTime(2021, 9, 21));
            var caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(caseFormId, "title")["title"]);

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
                .OpenRecord(caseFormId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("Complete")
                .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(1000);

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .ClickRefreshButton();


            var newWorkflowJobs = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowID, 1);

            //authenticate against the v6 Web API
            WebAPIHelper.Security.Authenticate();

            //execute the Workflow Jobs 
            foreach (var jobid in newWorkflowJobs)
                WebAPIHelper.WorkflowJob.Execute(jobid.ToString(), WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();

            //wait for the Idle status
            foreach (var jobid in newWorkflowJobs)
                dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(jobid);


            //at this point we should have 2 episodes
            System.Threading.Thread.Sleep(2000);
            var episodes = dbHelper.brokerageEpisode.GetBrokerageEpisodeByCaseID(caseID);
            Assert.AreEqual(2, episodes.Count);


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
                .OpenRecord(episodes[0].ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateRelatedAssessmentLinkFieldText(caseFormTitle);



            /**********************************************************/


            /************************* Step 5 *************************/

            caseBrokerageEpisodeRecordPage
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodes[1].ToString());


            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .NavigateToBrokerageEscalationsSubPage();

            brokerageEpisodeEscalationsPage
                .WaitForBrokerageEpisodeEscalationsPageToLoad()
                .ClickNewRecordButton();

            brokerageEpisodeEscalationRecordPage
                .WaitForBrokerageEpisodeEscalationRecordPageToLoad()
                .InsertEscalationDateTime(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), "09:00")
                .InsertEscalationDetails("Escalated by Abby")
                .ClickEscalatedToLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_loginUserName).TapSearchButton().SelectResultElement(_loginUserId.ToString());

            brokerageEpisodeEscalationRecordPage
                .WaitForBrokerageEpisodeEscalationRecordPageToLoad()
                .ClickSaveButton();


            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Escalation date cannot be in future")
                .TapOKButton();


            /**********************************************************/


            /************************* Step 6 *************************/

            brokerageEpisodeEscalationRecordPage
                .WaitForBrokerageEpisodeEscalationRecordPageToLoad()
                .InsertEscalationDateTime("01/09/2021", "09:00")
                .InsertResolutionDateTime(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), "09:00")

                .ClickSaveButton()

                .ValidateMessageAreaVisible(true)
                .ValidateResolutionDetailsFieldErrorLabelVisibility(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateResolutionDetailsFieldErrorLabelText("Please fill out this field.");





            /**********************************************************/


            /************************* Step 7 *************************/

            brokerageEpisodeEscalationRecordPage
                .InsertResolutionDetails("resolved by tom")
                .ClickSaveButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Resolution date cannot be in future").TapOKButton();


            /**********************************************************/



            /************************* Step 8 *************************/

            brokerageEpisodeEscalationRecordPage
                .WaitForBrokerageEpisodeEscalationRecordPageToLoad()
                .InsertEscalationDateTime("10/09/2021", "09:00")
                .InsertResolutionDateTime("05/09/2021", "08:00")
                .ClickSaveButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Resolution date time cannot be before Escalation date time").TapOKButton();


            /**********************************************************/



            /************************* Step 9 *************************/

            brokerageEpisodeEscalationRecordPage
                .WaitForBrokerageEpisodeEscalationRecordPageToLoad()
                .InsertEscalationDateTime("10/09/2021", "09:00")
                .InsertResolutionDateTime(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:05")
                .ClickSaveAndCloseButton();

            brokerageEpisodeEscalationsPage
                .WaitForBrokerageEpisodeEscalationsPageToLoad()
                .ClickRefreshButton();

            var escalations = dbHelper.brokerageEpisodeEscalation.GetByBrokerageEpisodeId(episodes[1]);
            Assert.AreEqual(1, escalations.Count);

            brokerageEpisodeEscalationsPage
                .ValidateRecordCellText(escalations[0].ToString(), 2, "10/09/2021 09:00:00")
                .ValidateRecordCellText(escalations[0].ToString(), 3, "BrokerageEpisode User1")
                .ValidateRecordCellText(escalations[0].ToString(), 4, DateTime.Now.ToString("dd'/'MM'/'yyyy") + " 00:05:00")
                .ValidateRecordCellText(escalations[0].ToString(), 5, "BrokerageEpisode User1");


            /**********************************************************/

        }

        [TestProperty("JiraIssueID", "CDV6-13088")]
        [Description("")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod007()
        {
            #region Service Elemet 1 & 2

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code1 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault());

            if (!dbHelper.serviceElement1.GetByName("01Jun21-SE1").Any())
                dbHelper.serviceElement1.CreateServiceElement1(teamID, "01Jun21-SE1", new DateTime(2021, 1, 1), code, 1, 1, validRateUnits);
            var ServiceElement1Id = dbHelper.serviceElement1.GetByName("01Jun21-SE1")[0];

            if (!dbHelper.serviceElement2.GetByName("01Jun21-SE2").Any())
                dbHelper.serviceElement2.CreateServiceElement2(teamID, "01Jun21-SE2", new DateTime(2021, 1, 1), code);
            var newServiceElement2Id = dbHelper.serviceElement2.GetByName("01Jun21-SE2")[0];

            commonMethodsDB.CreateServiceMapping(teamID, ServiceElement1Id, newServiceElement2Id);

            if (!dbHelper.serviceElement2.GetByName("Test Debtors 2").Any())
                dbHelper.serviceElement2.CreateServiceElement2(teamID, "Test Debtors 2", new DateTime(2021, 4, 15), code1);
            var ServiceElement2Id = dbHelper.serviceElement2.GetByName("Test Debtors 2")[0];

            #endregion

            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID_CommunityHospital, brokerageEpisodePriorityID_Priority, DateTime.Now.Date, null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

            dbHelper.brokerageEpisodeEscalation.CreateBrokerageEpisodeEscalation(teamID, caseID, personID, episodeId, _loginUserId, "systemuser", "BrokerageEpisode User1", new DateTime(2121, 9, 20), "desc ...");


            /************************* Step 2 *************************/
            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
               .NavigateToBrokerageEscalationsSubPage();

            brokerageEpisodeEscalationsPage
                .WaitForBrokerageEpisodeEscalationsPageToLoad()
                .ClickNewRecordButton();

            brokerageEpisodeEscalationRecordPage
                .WaitForBrokerageEpisodeEscalationRecordPageToLoad()
                .InsertEscalationDateTime("29/09/2021", "07:00")
                .InsertEscalationDetails("Escalated by Abby")
                .ClickEscalatedToLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_loginUserName).TapSearchButton().SelectResultElement(_loginUserId.ToString());

            brokerageEpisodeEscalationRecordPage
                .WaitForBrokerageEpisodeEscalationRecordPageToLoad()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            brokerageEpisodeEscalationsPage
                .WaitForBrokerageEpisodeEscalationsPageToLoad();

            var escalations = dbHelper.brokerageEpisodeEscalation.GetByBrokerageEpisodeId(episodeId);
            Assert.AreEqual(2, escalations.Count);

            brokerageEpisodeEscalationsPage
                .ValidateRecordPositionInList(escalations[0].ToString(), 1)
                .ValidateRecordPositionInList(escalations[1].ToString(), 2);

            /**********************************************************/


            /************************* Step 3 *************************/

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                //.ClickBackButton()//because we are in a subpage we need to load the parent to go back
                .ClickBackButton();//the 2nd click will close the record

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ");

            brokerageEpisodeEscalationsSubArea
                .WaitForBrokerageEpisodeEscalationsSubAreaToLoad()
                .ValidateRecordPositionInList(escalations[0].ToString(), 1)
                .ValidateRecordPositionInList(escalations[1].ToString(), 2)
                ;


            /**********************************************************/


            /************************* Step 4 *************************/

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateContractTypeSelectedText("Spot");

            /**********************************************************/


            /************************* Step 5 *************************/

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickServiceElement2LookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("test debtors 2").TapSearchButton().SelectResultElement(ServiceElement2Id.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateServiceElement2LinkFieldText("Test Debtors 2")
                .ClickServiceElement1LookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("01Jun21").TapSearchButton().SelectResultElement(ServiceElement1Id.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateServiceElement1LinkFieldText("01Jun21-SE1")
                .ValidateServiceElement2LinkFieldText("");

            /**********************************************************/


            /************************* Step 6 *************************/

            caseBrokerageEpisodeRecordPage
                .ClickServiceElement2LookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("test debtors 2").TapSearchButton().ValidateResultElementNotPresent(ServiceElement2Id.ToString())
                .TypeSearchQuery("01Jun21-SE2").TapSearchButton().SelectResultElement(newServiceElement2Id.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ");

            /**********************************************************/


            /************************* Step 7 *************************/

            caseBrokerageEpisodeRecordPage
                .ClickExistingCarePackageLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("No existing care package").TapSearchButton().SelectResultElement(BrokerageExistingCarePackageId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickScheduleRequiredYesRadioButton()
                .InsertNumberOfCarersPerVisit("2")
                .InsertMondayHealthMinutes("1")
                .InsertTuesdayHealthMinutes("2")
                .InsertWednesdayHealthMinutes("3")
                .InsertThursdayHealthMinutes("4")
                .InsertFridayHealthMinutes("5")
                .InsertSaturdayHealthMinutes("6")
                .InsertSundayHealthMinutes("7")

                .ValidateTotalHealthMinutesPerWeekFieldText("28")
                .ValidateTotalHealthMinutesPerWeekFieldDisabled(true)
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateScheduleRequired(true)
                .ValidateNumberOfCarersPerVisitFieldText("2")
                .ValidateMondayHealthMinutesFieldText("1")
                .ValidateTuesdayHealthMinutesFieldText("2")
                .ValidateWednesdayHealthMinutesFieldText("3")
                .ValidateThursdayHealthMinutesFieldText("4")
                .ValidateFridayHealthMinutesFieldText("5")
                .ValidateSaturdayHealthMinutesFieldText("6")
                .ValidateSundayHealthMinutesFieldText("7")

                .ValidateTotalHealthMinutesPerWeekFieldText("28")
                .ValidateTotalHealthMinutesPerWeekFieldDisabled(true);


            /**********************************************************/


            /************************* Step 8 *************************/

            caseBrokerageEpisodeRecordPage
                .ClickDeferredToCommissioningYesRadioButton()
                .InsertDateTimeDeferred("30/09/2021", "09:20")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateDeferredToCommissioning(true)
                .ValidateDateTimeDeferred("30/09/2021", "09:20");


            /**********************************************************/


            /************************* Step 9 *************************/

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .InsertPlannedStartDate("30/09/2021")
                .InsertPlannedEndDate("29/09/2021")
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Planned End Date cannot be before Planned Start Date").TapCloseButton();



            /**********************************************************/



            /************************* Step 9 *************************/

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .InsertPlannedStartDate("")
                .InsertPlannedEndDate("")
                .InsertNumberOfProvidersContacted("2")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .InsertPlannedStartDate("")
                .InsertPlannedEndDate("")
                .ValidateNumberOfProvidersContacted("2");

            /**********************************************************/
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10338

        [TestProperty("JiraIssueID", "CDV6-13200")]
        [Description("See story CDV6-13200 steps 1 to 10")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch2_UITestMethod001()
        {
            #region Step 1

            /************************* Step 1 *************************/

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .ClickNewRecordButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .InsertRequestReceivedDateTime("01/10/2021", "10:25")
                .ClickSourceOfRequestLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Community - Existing Care Package").TapSearchButton().SelectResultElement(sourceOfBrokerageRequestsID_CommunityExistingCarePackage.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .ClickPriorityLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Priority").TapSearchButton().SelectResultElement(brokerageEpisodePriorityID_Priority.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var episodes = dbHelper.brokerageEpisode.GetBrokerageEpisodeByCaseID(caseID);
            Assert.AreEqual(1, episodes.Count);
            var newEpisodeid = episodes[0];

            caseBrokerageEpisodesPage
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateTargetDateTime("", "")
                .ValidateTrackingStatusLinkFieldText("")
                .ValidateResponsibleUserLinkFieldText("");

            /**********************************************************/

            #endregion

            #region Step 2

            /************************* Step 2 *************************/

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Update Brokerage Episode Tracking Status" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UpdateBrokerageEpisodeTrackingStatusScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UpdateBrokerageEpisodeTrackingStatusScheduledJob);


            /**********************************************************/

            #endregion

            #region Step 3

            /************************* Step 3 *************************/

            caseBrokerageEpisodeRecordPage
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateTargetDateTime("", "")
                .ValidateTrackingStatusLinkFieldText("")
                .ValidateResponsibleUserLinkFieldText("");


            /**********************************************************/

            #endregion

            #region Step 4

            /************************* Step 4 *************************/

            caseBrokerageEpisodeRecordPage
                .ClickPriorityLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Standard").TapSearchButton().SelectResultElement(brokerageEpisodePriorityID_Standard.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateTargetDateTime("05/10/2021", "10:25")
                .ValidateTrackingStatusLinkFieldText("")
                .ClickBackButton();

            /**********************************************************/

            #endregion

            #region Step 5

            /************************* Step 5 *************************/

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Update Brokerage Episode Tracking Status" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UpdateBrokerageEpisodeTrackingStatusScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UpdateBrokerageEpisodeTrackingStatusScheduledJob);

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateTargetDateTime("05/10/2021", "10:25")
                .ValidateTrackingStatusLinkFieldText("");


            /**********************************************************/

            #endregion

            #region Step 6

            /************************* Step 6 *************************/

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .InsertRequestReceivedDateTime(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:05")
                .ClickSourceOfRequestLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Automation CDV6-10338").TapSearchButton().SelectResultElement(sourceOfBrokerageRequestsID_AutomationCDV6_10338.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ClickSaveAndCloseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Request Received Date/Time has changed. Saving this record will delete Pause Records that are before Request Received Date/Time").TapOKButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateTargetDateTime(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "00:05")
                .ValidateTrackingStatusLinkFieldText("")
                .ClickBackButton();

            /**********************************************************/

            #endregion

            #region Step 7

            /************************* Step 7 *************************/

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Update Brokerage Episode Tracking Status" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UpdateBrokerageEpisodeTrackingStatusScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UpdateBrokerageEpisodeTrackingStatusScheduledJob);

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateTargetDateTime(DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy"), "00:05")
                .ValidateTrackingStatusLinkFieldText("");

            /**********************************************************/

            #endregion

            #region Step 8

            /************************* Step 8 *************************/

            caseBrokerageEpisodeRecordPage
                .ClickSourceOfRequestLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Acute Hospital").TapSearchButton().SelectResultElement(sourceOfBrokerageRequestsID_AcuteHospital.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ClickPriorityLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Standard").TapSearchButton().SelectResultElement(brokerageEpisodePriorityID_Standard.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .InsertRequestReceivedDateTime("21/01/2021", "11:58")
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateTargetDateTime("02/02/2021", "11:58")
                .ValidateTrackingStatusLinkFieldText("");

            /**********************************************************/

            #endregion

            #region Step 9

            /************************* Step 9 *************************/


            caseBrokerageEpisodeRecordPage
                .InsertRequestReceivedDateTime(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"), "00:05")
                .ClickPriorityLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Priority").TapSearchButton().SelectResultElement(brokerageEpisodePriorityID_Priority.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ClickSaveAndCloseButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Request Received Date/Time has changed. Saving this record will delete Pause Records that are before Request Received Date/Time").TapOKButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateTargetDateTime(DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy"), "00:05")
                .ValidateTrackingStatusLinkFieldText("Green_Good")
                .ClickBackButton();

            /**********************************************************/

            #endregion

            #region Step 10

            /************************* Step 10 *************************/

            dbHelper.brokerageEpisode.UpdateTrackingStatus(newEpisodeid, null);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Update Brokerage Episode Tracking Status" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UpdateBrokerageEpisodeTrackingStatusScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UpdateBrokerageEpisodeTrackingStatusScheduledJob);

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateTargetDateTime(DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy"), "00:05")
                .ValidateTrackingStatusLinkFieldText("Green_Good");

            /**********************************************************/

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-13218")]
        [Description("See story CDV6-13200 steps 1 to ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch2_UITestMethod002()
        {
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID_AcuteHospital, brokerageEpisodePriorityID_Priority, DateTime.Now.Date, null, status, 0, 0, contractType, false, false, false, false, true, true, false);

            #region Step 2

            /************************* Step 2 *************************/

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .InsertRequestReceivedDateTime(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"), "10:25")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateTargetDateTime(DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy"), "10:25")
                .ValidateTrackingStatusLinkFieldText("Green_Good")
                .ClickBackButton();

            /**********************************************************/

            #endregion

            #region Step 3

            /************************* Step 3 *************************/

            dbHelper.brokerageEpisode.UpdateTrackingStatus(episodeId, null);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Update Brokerage Episode Tracking Status" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UpdateBrokerageEpisodeTrackingStatusScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UpdateBrokerageEpisodeTrackingStatusScheduledJob);

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateTrackingStatusLinkFieldText("Green_Good")
                ;

            /**********************************************************/

            #endregion

            #region Step 4

            /************************* Step 4 *************************/

            caseBrokerageEpisodeRecordPage
                .InsertRequestReceivedDateTime(DateTime.Now.AddDays(-5).ToString("dd'/'MM'/'yyyy"), "10:25")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateTargetDateTime(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), "10:25")
                .ValidateTrackingStatusLinkFieldText("Oragne_Still OK")
                .InsertRequestReceivedDateTime(DateTime.Now.AddDays(-8).ToString("dd'/'MM'/'yyyy"), "10:25")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateTargetDateTime(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"), "10:25")
                .ValidateTrackingStatusLinkFieldText("Oragne_Still OK")
                .InsertRequestReceivedDateTime(DateTime.Now.AddDays(-12).ToString("dd'/'MM'/'yyyy"), "10:25")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateTargetDateTime(DateTime.Now.AddDays(-5).ToString("dd'/'MM'/'yyyy"), "10:25")
                .ValidateTrackingStatusLinkFieldText("Red_Negative")
                .ClickBackButton();

            /**********************************************************/

            #endregion

            #region Step 5

            /************************* Step 5 *************************/

            dbHelper.brokerageEpisode.UpdateTrackingStatus(episodeId, null);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Update Brokerage Episode Tracking Status" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UpdateBrokerageEpisodeTrackingStatusScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UpdateBrokerageEpisodeTrackingStatusScheduledJob);

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateTargetDateTime(DateTime.Now.AddDays(-5).ToString("dd'/'MM'/'yyyy"), "10:25")
                .ValidateTrackingStatusLinkFieldText("Red_Negative")
                ;

            /**********************************************************/

            #endregion

            #region Step 6

            /************************* Step 6 *************************/

            caseBrokerageEpisodeRecordPage
                .ClickPriorityLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Urgent").TapSearchButton().SelectResultElement(brokerageEpisodePriorityID_Urgent.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateTrackingStatusLinkFieldText("");
            //.ValidateTargetDateTime("", "");

            /**********************************************************/

            #endregion

            #region Step 7

            /************************* Step 7 *************************/

            caseBrokerageEpisodeRecordPage
                .ClickAdditionalItemsButton()
                .ValidateCopyButtonVisibility(true)
                .ClickAdditionalItemsButton();

            /**********************************************************/

            #endregion

            #region Step 8

            /************************* Step 8 *************************/

            var EscalationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var escalation1Id = dbHelper.brokerageEpisodeEscalation.CreateBrokerageEpisodeEscalation(teamID, caseID, personID, episodeId, _loginUserId, "systemuser", "BrokerageEpisode User1", EscalationDate, "desc ...");

            caseBrokerageEpisodeRecordPage
                .ClickCopyButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure you would like to copy this Brokerage Episode?");

            /**********************************************************/

            #endregion

            #region Step 9

            /************************* Step 9 *************************/

            confirmDynamicDialogPopup.TapOKButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Brokerage Episode copied successfully.");


            /**********************************************************/

            #endregion

            #region Step 10

            /************************* Step 10 *************************/

            dynamicDialogPopup.TapCloseButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad();

            var allEpisodes = dbHelper.brokerageEpisode.GetBrokerageEpisodeByCaseID(caseID);
            Assert.AreEqual(2, allEpisodes.Count);

            caseBrokerageEpisodesPage
                .ValidateRecordVisible(allEpisodes[0].ToString())
                .ValidateRecordVisible(allEpisodes[1].ToString());

            /**********************************************************/

            #endregion

            #region Step 11

            /************************* Step 11 *************************/

            var copiedEpisodeId = allEpisodes.Where(c => c != episodeId).FirstOrDefault();

            caseBrokerageEpisodesPage
                .OpenRecord(copiedEpisodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateCaseLinkFieldText(", " + _personName + " - (" + DateTime.Now.AddYears(-18).ToString("dd'/'MM'/'yyyy") + ") [" + caseNumber + "]")
                .ValidatePersonLinkFieldText(_personName)
                .ValidateStatusSelectedText("New")
                .ValidateRequestReceivedDateTime(DateTime.Now.AddDays(-12).ToString("dd'/'MM'/'yyyy"), "10:25")
                .ValidateSourceOfRequestLinkFieldText("Acute Hospital")
                .ValidatePriorityLinkFieldText("Urgent")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText("")
                //.ValidateTargetDateTime(DateTime.Now.AddDays(-4).ToString("dd/MM/yyyy"), "10:25")
                .ValidateTrackingStatusLinkFieldText("")

                .NavigateToBrokerageEscalationsSubPage();

            brokerageEpisodeEscalationsPage
                .WaitForBrokerageEpisodeEscalationsPageToLoad();

            var copiedEpisodeEscalations = dbHelper.brokerageEpisodeEscalation.GetByBrokerageEpisodeId(copiedEpisodeId);
            Assert.AreEqual(1, copiedEpisodeEscalations.Count);
            var copiedEscalationId = copiedEpisodeEscalations[0];

            brokerageEpisodeEscalationsPage
                .OpenRecord(copiedEscalationId.ToString());

            brokerageEpisodeEscalationRecordPage
                .WaitForBrokerageEpisodeEscalationRecordPageToLoad()
                .ValidateBrokerageEpisodeLinkFieldText("Brokerage episode for " + _personName + " received on " + DateTime.Now.AddDays(-12).ToString("dd'/'MM'/'yyyy") + " 10:25:00")
                .ValidateEscalatedToLinkFieldText("BrokerageEpisode User1")
                .ValidateEscalationDateTime(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:00")
                .ValidateEscalationDetails("desc ...")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResolutionDateTime("", "")
                .ValidateResolutionDetails("");

            /**********************************************************/

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-13271")]
        [Description("See story CDV6-13271 steps 1 to ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS"), DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        public void Case_BrokerageEpisodes_Batch2_UITestMethod003()
        {
            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Attach Document Type

            var documentTypeID = commonMethodsDB.CreateAttachDocumentType(teamID, "All Attached Documents", new DateTime(2021, 2, 3));

            #endregion

            #region Attach Document Sub Type

            var documentSubTypeID = dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(teamID, "Independent Living Grant", new DateTime(2021, 2, 3), documentTypeID);

            #endregion

            var BrokerageEpisodeDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID_AcuteHospital, brokerageEpisodePriorityID_Priority, BrokerageEpisodeDate, null, status, 0, 0, contractType, false, false, false, false, true, true, false);

            #region Step 2

            /************************* Step 2 *************************/

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .NavigateToAttachmentsSubPage();

            caseBrokerageEpisodeAttachmentsPage
                .WaitForCaseBrokerageEpisodeAttachmentsPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeAttachmentRecordPage
                .WaitForCaseBrokerageEpisodeAttachmentRecordPageToLoad("New")
                .InsertTitle("Attachment 01")
                .InsertDate("01/01/2021", "10:50")
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("All Attached Documents").TapSearchButton().SelectResultElement(documentTypeID.ToString());

            caseBrokerageEpisodeAttachmentRecordPage
                .WaitForCaseBrokerageEpisodeAttachmentRecordPageToLoad("New")
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Independent Living Grant").TapSearchButton().SelectResultElement(documentSubTypeID.ToString());

            caseBrokerageEpisodeAttachmentRecordPage
                .WaitForCaseBrokerageEpisodeAttachmentRecordPageToLoad("New")
                .UploadFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodeAttachmentsPage
                .WaitForCaseBrokerageEpisodeAttachmentsPageToLoad();

            dbHelper = new DBHelper.DatabaseHelper();
            var attachments = dbHelper.brokerageEpisodeAttachment.GetByBrokerageEpisodeId(episodeId);
            Assert.AreEqual(1, attachments.Count);
            var newAttachment = attachments[0];

            caseBrokerageEpisodeAttachmentsPage
                .ValidateRecordVisible(newAttachment.ToString());


            /**********************************************************/



            #endregion

            #region Step 3

            /************************* Step 3 *************************/

            caseBrokerageEpisodeAttachmentsPage
                .OpenRecord(newAttachment.ToString());

            caseBrokerageEpisodeAttachmentRecordPage
                .WaitForCaseBrokerageEpisodeAttachmentRecordPageToLoad("Attachment 01")
                .ClickCloneButton();

            cloneAttachmentsPopup
                .WaitForCloneAttachmentsPopupToLoad()
                .SelectBusinessObjectTypeText("Brokerage Episode")
                .InsertStartDate("09/01/2021")
                .SelectRecord(episodeId.ToString())
                .ClickCloneButton()
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Attachment(s) cloned successfully.")
                .ClickCloseButton();

            /**********************************************************/

            #endregion

            #region Step 4

            /************************* Step 4 *************************/


            caseBrokerageEpisodeAttachmentRecordPage
                .WaitForCaseBrokerageEpisodeAttachmentRecordPageToLoad("Attachment 01")
                .ClickBackButton();

            caseBrokerageEpisodeAttachmentsPage
                .WaitForCaseBrokerageEpisodeAttachmentsPageToLoad()
                .ClickSearchButton();

            dbHelper = new DBHelper.DatabaseHelper();
            attachments = dbHelper.brokerageEpisodeAttachment.GetByBrokerageEpisodeId(episodeId);
            Assert.AreEqual(2, attachments.Count);
            var clonedAttachment = attachments.Where(c => c != newAttachment).FirstOrDefault();

            caseBrokerageEpisodeAttachmentsPage
                .OpenRecord(clonedAttachment.ToString());

            caseBrokerageEpisodeAttachmentRecordPage
                .WaitForCaseBrokerageEpisodeAttachmentRecordPageToLoad("Attachment 01")
                .ValidateTitle("Attachment 01")
                .ValidateBrokerageEpisodeLinkFieldText("Brokerage episode for " + _personName + " received on " + DateTime.Now.ToString("dd'/'MM'/'yyyy") + " 00:00:00")
                .ValidateDate("09/01/2021", "00:00")
                .ValidateDocumentTypeLinkFieldText("All Attached Documents")
                .ValidateDocumentSubTypeLinkFieldText("Independent Living Grant")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateIsClonedChecked(true)
                .ValidateClonedFromLinkFieldText("Attachment 01");

            /**********************************************************/

            #endregion

            #region Step 5

            /************************* Step 5 *************************/

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personnumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateAttachmentLinkText(newAttachment.ToString(), "Title: Attachment 01, Date: 01/01/2021 10:50:00")
                .ValidateAttachmentLinkText(clonedAttachment.ToString(), "Title: Attachment 01, Date: 09/01/2021 00:00:00")
                ;

            /**********************************************************/

            #endregion

            #region Step 6

            /************************* Step 6 *************************/

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ClickResponsibleUserLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_loginUserName).TapSearchButton().SelectResultElement(_loginUserId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ClickTypeOFCarePackageLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Day care").TapSearchButton().SelectResultElement(brokerageCarePackageTypeID.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateStatusSelectedText("Sourcing in Progress")
                .ValidateResponsibleUserLinkFieldText("BrokerageEpisode User1")
                .ValidateTypeOFCarePackageLinkFieldText("Day care")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Under Review")
                .InsertReceivedDateTime("01/09/2021", "09:00")
                .ClickProviderLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(providerName).TapSearchButton().SelectResultElement(providerID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad();

            System.Threading.Thread.Sleep(2000);
            dbHelper = new DBHelper.DatabaseHelper();
            var offers = dbHelper.brokerageOffer.GetByBrokerageEpisodeId(episodeId);
            Assert.AreEqual(1, offers.Count);
            var newOfferId = offers.FirstOrDefault();

            caseBrokerageEpisodeOffersPage
                .OpenRecord(newOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad("Brokerage Offer from " + providerName + " received on")
                .NavigateToAttachmentsSubPage();

            caseBrokerageOfferAttachmentsPage
                .WaitForCaseBrokerageOfferAttachmentsPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageOfferAttachmentRecordPage
                .WaitForCaseBrokerageOfferAttachmentRecordPageToLoad("New")
                .InsertTitle("Offer Record - Attachment 01")
                .InsertDate("01/01/2021", "10:50")
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("All Attached Documents").TapSearchButton().SelectResultElement(documentTypeID.ToString());

            caseBrokerageOfferAttachmentRecordPage
                .WaitForCaseBrokerageOfferAttachmentRecordPageToLoad("New")
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Independent Living Grant").TapSearchButton().SelectResultElement(documentSubTypeID.ToString());

            caseBrokerageOfferAttachmentRecordPage
                .WaitForCaseBrokerageOfferAttachmentRecordPageToLoad("New")
                .UploadFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveAndCloseButton();

            caseBrokerageOfferAttachmentsPage
                .WaitForCaseBrokerageOfferAttachmentsPageToLoad();

            dbHelper = new DBHelper.DatabaseHelper();
            var newOfferAttachments = dbHelper.brokerageOfferAttachment.GetByBrokerageOfferId(newOfferId);
            Assert.AreEqual(1, newOfferAttachments.Count);
            var newOfferAttachmentId = newOfferAttachments[0];

            caseBrokerageOfferAttachmentsPage
                .ValidateRecordVisible(newOfferAttachmentId.ToString());

            /**********************************************************/

            #endregion

            #region Step 7

            /************************* Step 7 *************************/

            caseBrokerageOfferAttachmentsPage
                .OpenRecord(newOfferAttachmentId.ToString());

            caseBrokerageOfferAttachmentRecordPage
                .WaitForCaseBrokerageOfferAttachmentRecordPageToLoad("Offer record - Attachment 01")
                .InsertDate("15/01/2021", "09:35")
                .ClickSaveAndCloseButton();

            caseBrokerageOfferAttachmentsPage
                .WaitForCaseBrokerageOfferAttachmentsPageToLoad()
                .ValidateRecordCellText(newOfferAttachmentId.ToString(), 5, "15/01/2021 09:35:00");

            /**********************************************************/

            #endregion

            #region Step 8

            /************************* Step 7 *************************/

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personnumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapDocumentViewTab();

            personDocumentViewSubPage
                .WaitForPersonDocumentViewSubPageToLoad()
                .ClickClearFilterButton()
                .ClickSearchButton()

                .ValidateAttachmentLinkText(newOfferAttachmentId.ToString(), "Title: Offer Record - Attachment 01, Date: 15/01/2021 09:35:00");

            /**********************************************************/

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-13310")]
        [Description("See story CDV6-13271 steps 1 to ")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Case_BrokerageEpisodes_Batch2_UITestMethod004()
        {
            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Attach Document Type

            if (!dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("All Attached Documents").Any())
                dbHelper.attachDocumentType.CreateAttachDocumentType(teamID, "All Attached Documents", new DateTime(2021, 2, 3));
            var documentTypeID = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("All Attached Documents")[0];

            #endregion

            #region Attach Document Sub Type

            if (!dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Independent Living Grant").Any())
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(teamID, "Independent Living Grant", new DateTime(2021, 2, 3), documentTypeID);
            var documentSubTypeID = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Independent Living Grant")[0];

            #endregion

            #region Episode / Brokerage Offer

            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID_AcuteHospital, brokerageEpisodePriorityID_Priority, DateTime.Now.Date, null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

            var brokerageOfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);

            #endregion

            #region Step 2

            /************************* Step 2 *************************/

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Under Review")
                .ClickSaveButton()

                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .InsertSourcedDateTime(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), "12:30")
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Sourced Date/Time cannot be in the future.").TapCloseButton();

            /**********************************************************/

            #endregion

            #region Step 3

            /************************* Step 3 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .InsertSourcedDateTime("07/10/2021", "12:30")
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Sourced Date/Time cannot be before Received Date/Time.").TapCloseButton();


            /**********************************************************/

            #endregion

            #region Step 4

            /************************* Step 4 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .InsertSourcedDateTime(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:05")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickRefreshButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ValidateRecordCellText(brokerageOfferId.ToString(), 6, "Sourced");

            /**********************************************************/

            #endregion

            #region Step 5

            /************************* Step 5 *************************/

            caseBrokerageEpisodeOffersPage
                .OpenRecord(brokerageOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusOptionDisabled("Rejected", false)
                .ValidateStatusOptionDisabled("Cancelled", false)
                .ValidateStatusOptionDisabled("Accepted", false);



            /**********************************************************/

            #endregion

            #region Step 6

            /************************* Step 6 *************************/

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad()
                .ClickNewRecordButton();

            caseBrokerageOfferCommunicationRecordPage
                .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
                .InsertCommunicationDateTime(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:05")
                .ClickCommunicationWithLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Commissioning").TapSearchButton().SelectResultElement(brokerageCommunicationWith1ID.ToString());

            caseBrokerageOfferCommunicationRecordPage
                .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
                .ClickContactMethodLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Any").TapSearchButton().SelectResultElement(contactMethodId.ToString());

            caseBrokerageOfferCommunicationRecordPage
                .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
                .InsertSubject("Communication 001")
                .InsertCommunicationDetails("details ...")
                .ClickOutcomeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Follow-up required").TapSearchButton().SelectResultElement(brokerageOfferCommunicationOutcomesId.ToString());

            caseBrokerageOfferCommunicationRecordPage
                .WaitForCaseBrokerageOfferCommunicationRecordPageToLoad()
                .ClickSaveAndCloseButton();

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad();

            System.Threading.Thread.Sleep(3000);

            var brokerageOfferCommunications = dbHelper.brokerageOfferCommunication.GetByBrokerageOfferId(brokerageOfferId);
            Assert.AreEqual(1, brokerageOfferCommunications.Count);
            var newBrokerageOfferCommunication = brokerageOfferCommunications.FirstOrDefault();

            caseBrokerageOfferCommunicationsSubArea
                .ValidateRecordVisible(newBrokerageOfferCommunication.ToString());

            /**********************************************************/

            #endregion

            #region Step 7

            /************************* Step 7 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .NavigateToAttachmentsSubPage();

            caseBrokerageOfferAttachmentsPage
                .WaitForCaseBrokerageOfferAttachmentsPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageOfferAttachmentRecordPage
                .WaitForCaseBrokerageOfferAttachmentRecordPageToLoad("New")
                .InsertTitle("Offer Record - Attachment 01")
                .InsertDate("01/01/2021", "10:50")
                .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("All Attached Documents").TapSearchButton().SelectResultElement(documentTypeID.ToString());

            caseBrokerageOfferAttachmentRecordPage
                .WaitForCaseBrokerageOfferAttachmentRecordPageToLoad("New")
                .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Independent Living Grant").TapSearchButton().SelectResultElement(documentSubTypeID.ToString());

            caseBrokerageOfferAttachmentRecordPage
                .WaitForCaseBrokerageOfferAttachmentRecordPageToLoad("New")
                .UploadFile(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            caseBrokerageOfferAttachmentsPage
                .WaitForCaseBrokerageOfferAttachmentsPageToLoad();

            var offerAttachmentRecords = dbHelper.brokerageOfferAttachment.GetByBrokerageOfferId(brokerageOfferId);
            Assert.AreEqual(1, offerAttachmentRecords.Count);
            var newOfferAttachmentRecord = offerAttachmentRecords.FirstOrDefault();

            /**********************************************************/

            #endregion

            #region Step 8

            /************************* Step 8 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            System.Threading.Thread.Sleep(2000);

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Related record exists in Attachment (Brokerage Offers). Please delete related records before deleting record in Brokerage Offer.").TapCloseButton();


            /**********************************************************/

            #endregion

            #region Step 9

            /************************* Step 9 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .NavigateToAttachmentsSubPage();

            caseBrokerageOfferAttachmentsPage
                .WaitForCaseBrokerageOfferAttachmentsPageToLoad()
                .SelectRecord(newOfferAttachmentRecord.ToString())
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(brokerageOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad();

            caseBrokerageOfferCommunicationsSubArea
                .WaitForCaseBrokerageOfferCommunicationsSubAreaToLoad()
                .SelectRecord(newBrokerageOfferCommunication.ToString())
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("A sourced offer cannot be deleted. It can be rejected or cancelled.").TapCloseButton();

            /**********************************************************/

            #endregion

            #region Step 10

            /************************* Step 10 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ValidateStatusSelectedText("Sourced")
                .ValidateSourcedDateTime(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:05");

            /**********************************************************/

            #endregion
        }

        [TestProperty("JiraIssueID", "CDV6-13314")]
        [Description("See story CDV6-13271 steps 1 to ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch2_UITestMethod005()
        {
            var CurrentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

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

            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID_AcuteHospital, brokerageEpisodePriorityID_Priority, CurrentDate, null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeId, 3); //update status to Sourcing in Progress

            var brokerageOfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(brokerageOfferId, 4); //update status to under review
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(brokerageOfferId, 5, CurrentDate); //set the status to Sourced. This will automatically update the brokerage episode status as well

            #region Step 2

            /************************* Step 2 *************************/

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .SelectStatus("Ready for Approval")
                .ClickSaveButton()

                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateServiceElement1FieldErrorLabelVisibility(true)
                .ValidateServiceElement1FieldErrorLabelText("Please fill out this field.")
                .ValidateServiceElement2FieldErrorLabelVisibility(true)
                .ValidateServiceElement2FieldErrorLabelText("Please fill out this field.")
                .ValidatePlannedStartDateFieldErrorLabelVisibility(true)
                .ValidatePlannedStartDateFieldErrorLabelText("Please fill out this field.");

            /**********************************************************/

            #endregion

            #region Step 3

            /************************* Step 3 *************************/

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ClickServiceElement1LookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("01Jun21-SE1").TapSearchButton().SelectResultElement(ServiceElement1Id.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickServiceElement2LookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("01Jun21-SE2").TapSearchButton().SelectResultElement(ServiceElement2Id.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .InsertPlannedStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertPlannedEndDate(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Planned End Date cannot be before Planned Start Date").TapCloseButton();

            /**********************************************************/

            #endregion

            #region Step 4

            /************************* Step 4 *************************/

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .InsertPlannedEndDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateStatusSelectedText("Ready for Approval")
                .ValidateServiceElement1LinkFieldText("01Jun21-SE1")
                .ValidateServiceElement2LinkFieldText("01Jun21-SE2")
                .ValidatePlannedStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidatePlannedEndDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickBackButton();

            /**********************************************************/

            #endregion

            #region Step 5

            /************************* Step 5 *************************/

            var newEpisodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID_AcuteHospital, brokerageEpisodePriorityID_Priority, DateTime.Now.Date, null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(newEpisodeId, 3); //update status to Sourcing in Progress

            var newBrokerageOfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, newEpisodeId, caseID, personID, providerID, new DateTime(2021, 10, 9), 1, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(newBrokerageOfferId, 4); //update status to under review
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(newBrokerageOfferId, 5, DateTime.Now.Date); //set the status to Sourced. This will automatically update the brokerage episode status as well

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .ClickSearchButton()
                .OpenRecord(newEpisodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .SelectStatus("Approved")
                .ClickServiceElement1LookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("01Jun21-SE1").TapSearchButton().SelectResultElement(ServiceElement1Id.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickServiceElement2LookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("01Jun21-SE2").TapSearchButton().SelectResultElement(ServiceElement2Id.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .InsertPlannedStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure this Brokerage Episode should skip the approval process?").TapOKButton();

            /**********************************************************/

            #endregion

            #region Step 6

            /************************* Step 6 *************************/

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateStatusSelectedText("Approved")
                .ValidateDateTimeApprovedVisible(false)
                .ValidateApproverVisible(false)
                .ValidateApproverCommentsVisible(false)
                .ValidateDateTimeApprovalRejectedVisible(false)
                .ClickBackButton();

            /**********************************************************/

            #endregion

            #region Step 7

            /************************* Step 7 *************************/

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateStatusOptionDisabled("Approval Rejected", false)
                .ValidateStatusOptionDisabled("Approved", false)
                ;

            /**********************************************************/

            #endregion

            #region Step 8

            /************************* Step 8 *************************/

            caseBrokerageEpisodeRecordPage
                .SelectStatus("Approved")
                .ClickSaveButton()

                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")

                .ValidateDateTimeApprovedFieldErrorLabelVisibility(true)
                .ValidateDateTimeApprovedFieldErrorLabelText("Please fill out this field.")
                .ValidateApproverFieldErrorLabelVisibility(true)
                .ValidateApproverFieldErrorLabelText("Please fill out this field.")
                .ValidateApproverCommentsFieldErrorLabelVisibility(true)
                .ValidateApproverCommentsFieldErrorLabelText("Please fill out this field.")
                .ValidateDateTimeApprovalRejectedFieldErrorLabelVisibility(false)
                ;

            /**********************************************************/

            #endregion

            #region Step 9

            /************************* Step 9 *************************/

            caseBrokerageEpisodeRecordPage
                .SelectStatus("Approval Rejected")
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")

                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")

                .ValidateDateTimeApprovedFieldErrorLabelVisibility(false)
                .ValidateApproverFieldErrorLabelVisibility(true)
                .ValidateApproverFieldErrorLabelText("Please fill out this field.")
                .ValidateApproverCommentsFieldErrorLabelVisibility(true)
                .ValidateApproverCommentsFieldErrorLabelText("Please fill out this field.")
                .ValidateDateTimeApprovalRejectedFieldErrorLabelVisibility(true)
                .ValidateDateTimeApprovalRejectedFieldErrorLabelText("Please fill out this field.")
                ;

            /**********************************************************/

            #endregion

            #region Step 10

            /************************* Step 10 *************************/

            caseBrokerageEpisodeRecordPage
                .SelectStatus("Approved")
                .InsertDateTimeApprovedDateTime(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:05")
                .InsertApproverComments("approver comments ...")
                .ClickApproverLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("BrokerageEpisode User1").TapSearchButton().SelectResultElement(_loginUserId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on")
                .ValidateStatusSelectedText("Approved")
                .ValidateDateTimeApproved(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "00:05")
                .ValidateApproverLinkFieldText("BrokerageEpisode User1")
                .ValidateApproverComments("approver comments ...");

            /**********************************************************/

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-13134

        [TestProperty("JiraIssueID", "CDV6-13440")]
        [Description("")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch3_UITestMethod001()
        {
            #region Random Codes

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code3 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code4 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code5 = commonMethodsHelper.GetRandomValue(1, 2147483647);

            #endregion

            #region  RateType

            if (!dbHelper.rateType.GetByName("Hours (One only)").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Hours (One only)", code, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId1 = dbHelper.rateType.GetByName("Hours (One only)")[0];

            if (!dbHelper.rateType.GetByName("Days").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Days", code2, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId2 = dbHelper.rateType.GetByName("Days")[0];

            if (!dbHelper.rateType.GetByName("Weekly").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Weekly", code3, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId3 = dbHelper.rateType.GetByName("Weekly")[0];

            if (!dbHelper.rateType.GetByName("Hours (Part)").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Hours (Part)", code4, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId4 = dbHelper.rateType.GetByName("Hours (Part)")[0];

            if (!dbHelper.rateType.GetByName("Units (Whole)").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Units (Whole)", code5, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId5 = dbHelper.rateType.GetByName("Units (Whole)")[0];


            #endregion

            #region  RateUnit

            if (!dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (One only)").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "Per 1 Hour", new DateTime(2020, 1, 1), code, _rateTypeId1);
            var rateUnit1ID = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (One only)").FirstOrDefault();

            if (!dbHelper.rateUnit.GetByName("Per Day \\ Days").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "Per Day", new DateTime(2020, 1, 1), code2, _rateTypeId2);
            var rateUnit2ID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            if (!dbHelper.rateUnit.GetByName("ML_Weekly_17 \\ Weekly").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "ML_Weekly_17", new DateTime(2020, 1, 1), code3, _rateTypeId3);
            var rateUnit3ID = dbHelper.rateUnit.GetByName("ML_Weekly_17 \\ Weekly").FirstOrDefault();

            if (!dbHelper.rateUnit.GetByName("ML_Whole_10 \\ Units (Whole)").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "ML_Whole_10", new DateTime(2020, 1, 1), code4, _rateTypeId5);
            var rateUnit4ID = dbHelper.rateUnit.GetByName("ML_Whole_10 \\ Units (Whole)").FirstOrDefault();

            if (!dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Part)").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "Per 1 Hour", new DateTime(2020, 1, 1), code5, _rateTypeId4);
            var rateUnit5ID = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Part)").FirstOrDefault();

            #endregion

            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13440").Any())
                dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(teamID, _careDirectorQA_BusinessUnitId, "StartReason_CDV6_13440", new DateTime(2022, 1, 1), false);
            var serviceProvisionStartReasonId = dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_13440").FirstOrDefault();
            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(serviceProvisionStartReasonId, true);
            #endregion

            #region Service Element 1 & 2

            var validRateUnits1 = new List<Guid>();
            validRateUnits1.Add(rateUnit1ID);
            validRateUnits1.Add(rateUnit3ID);
            validRateUnits1.Add(rateUnit5ID);

            var validRateUnits2 = new List<Guid>();
            validRateUnits2.Add(rateUnit1ID);
            validRateUnits2.Add(rateUnit3ID);
            validRateUnits2.Add(rateUnit4ID);
            validRateUnits2.Add(rateUnit5ID);

            if (!dbHelper.serviceElement1.GetByName("08julsd").Any())
                dbHelper.serviceElement1.CreateServiceElement1(teamID, "08julsd", new DateTime(2021, 1, 1), code, 1, 1, validRateUnits1);
            var serviceElement1ID1 = dbHelper.serviceElement1.GetByName("08julsd")[0];

            if (!dbHelper.serviceElement1.GetByName("23jun20").Any())
                dbHelper.serviceElement1.CreateServiceElement1(teamID, "23jun20", new DateTime(2021, 1, 1), code2, 1, 1, validRateUnits2, rateUnit3ID);
            var serviceElement1ID2 = dbHelper.serviceElement1.GetByName("23jun20")[0];

            if (!dbHelper.serviceElement2.GetByName("AD_SE2").Any())
                dbHelper.serviceElement2.CreateServiceElement2(teamID, "AD_SE2", new DateTime(2021, 1, 1), code);
            var serviceElement2ID = dbHelper.serviceElement2.GetByName("AD_SE2")[0];

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1ID2, serviceElement2ID);

            #endregion

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Service Provider

            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, _loginUserId, providerID, serviceElement1ID2, serviceElement2ID, null, null, null, null, 2, false);

            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(teamID, serviceProvidedId, rateUnit3ID, new DateTime(2022, 1, 1), 2);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(teamID, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

            #endregion

            #region Step 1

            /************************* Step 1 *************************/

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .ClickNewRecordButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .InsertRequestReceivedDateTime("21/01/2021", "11:58")
                .ClickSourceOfRequestLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Acute Hospital").TapSearchButton().SelectResultElement(sourceOfBrokerageRequestsID_AcuteHospital.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .ClickPriorityLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Standard").TapSearchButton().SelectResultElement(brokerageEpisodePriorityID_Standard.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var episodes = dbHelper.brokerageEpisode.GetBrokerageEpisodeByCaseID(caseID);
            Assert.AreEqual(1, episodes.Count);
            var newEpisodeid = episodes[0];

            caseBrokerageEpisodesPage
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ValidateTargetDateTime("02/02/2021", "11:58")
                .ValidateTrackingStatusLinkFieldText("")
                .ValidateResponsibleUserLinkFieldText("");

            /**********************************************************/

            #endregion

            #region Step 2

            /************************* Step 2 *************************/

            caseBrokerageEpisodeRecordPage
                .SelectStatus("Sourcing in Progress")
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Under Review")
                .InsertReceivedDateTime("22/09/2021", "09:00")
                .ClickProviderLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(providerName).TapSearchButton().SelectResultElement(providerID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad();

            System.Threading.Thread.Sleep(2000);
            dbHelper = new DBHelper.DatabaseHelper();
            var offers = dbHelper.brokerageOffer.GetByBrokerageEpisodeId(newEpisodeid);
            Assert.AreEqual(1, offers.Count);
            var newOfferId = offers.FirstOrDefault();

            caseBrokerageEpisodeOffersPage
                .OpenRecord(newOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateCostPerWeekValue("");


            /**********************************************************/

            #endregion

            #region Step 3

            /************************* Step 3 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .ClickBackButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickServiceElement1LookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("08julsd").TapSearchButton().SelectResultElement(serviceElement1ID1.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickSaveButton()
                .ClickServiceElement1LinkFieldText();

            lookupFormPage.WaitForLookupFormPageToLoad().ClickViewButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad(false)
                .ValidateValidRateUnitOptionVisible(rateUnit1ID.ToString(), "Per 1 Hour \\ Hours (One only)\r\nRemove")
                .ValidateDefaultRateUnitLinkFieldText("");

            /**********************************************************/

            #endregion

            #region Step 4

            /************************* Step 4 *************************/

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
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(newOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Sourced")
                .InsertSourcedDateTime("22/09/2021", "10:00")

                .ClickSaveButton()

                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Accepted")
                .ValidateRateUnitLinkFieldText("");


            /**********************************************************/

            #endregion

            #region Step 5

            /************************* Step 5 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickServiceElement1LookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("23jun20").TapSearchButton().SelectResultElement(serviceElement1ID2.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickSaveButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickServiceElement1LinkFieldText();

            lookupFormPage.WaitForLookupFormPageToLoad().ClickViewButton();

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad(false)
                .ValidateValidRateUnitOptionVisible(rateUnit1ID.ToString(), "Per 1 Hour \\ Hours (One only)\r\nRemove")
                .ValidateDefaultRateUnitLinkFieldText("ML_Weekly_17 \\ Weekly");


            /**********************************************************/

            #endregion

            #region Step 6

            /************************* Step 6 *************************/

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
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(newOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Accepted")
                .ValidateRateUnitLinkFieldText("ML_Weekly_17 \\ Weekly");


            /**********************************************************/

            #endregion

            #region Step 7

            /************************* Step 7 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .ValidateRateUnitLinkFieldText("ML_Weekly_17 \\ Weekly")
                .ClickRateUnitLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per Day").TapSearchButton().ValidateResultElementNotPresent(rateUnit2ID.ToString());



            /**********************************************************/

            #endregion

            #region Step 8

            /************************* Step 8 *************************/

            lookupPopup.TypeSearchQuery("Per 1 Hour").TapSearchButton().SelectResultElement(rateUnit1ID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateRateUnitLinkFieldText("Per 1 Hour \\ Hours (One only)");

            /**********************************************************/

            #endregion

            #region Step 9

            /************************* Step 9 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .INsertCostPerWeek("-1")
                .ValidateCostPerWeekFieldErrorLabelVisibility(true)
                .ValidateCostPerWeekFieldErrorLabelText("Please enter a value between 0 and 7.922816251426434e+28.");

            /**********************************************************/

            #endregion

            #region Step 10

            /************************* Step 10 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .SelectStatus("Approved")
                .ClickServiceElement2LookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(serviceElement2ID.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .InsertPlannedStartDate("21/01/2021")
                .ClickSaveButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure this Brokerage Episode should skip the approval process?").TapOKButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(newOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Accepted")
                .INsertCostPerWeek("1200")
                .ClickRateUnitLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("ML_Weekly_17").TapSearchButton().SelectResultElement(rateUnit3ID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickServiceProvidedLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(serviceProvidedId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(newOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusSelectedText("Accepted")
                .ValidateCostPerWeekValue("1200.00")
                .ValidateRateUnitLinkFieldText("ML_Weekly_17 \\ Weekly")
                .ValidateServiceProvidedLinkFieldText(providerName + " \\ 23jun20 \\ AD_SE2 \\ \\ \\ Spot");

            /**********************************************************/

            #endregion

            #region Step 11

            /************************* Step 11 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToServiceProvisionSubPage();

            caseBrokerageEpisodeServiceProvisionsPage
                .WaitForCaseBrokerageEpisodeServiceProvisionsPageToLoad();

            var serviceProvisions = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisions.Count);
            var newServiceProvisionId = serviceProvisions[0];

            caseBrokerageEpisodeServiceProvisionsPage
                .OpenRecord(newServiceProvisionId.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ValidateRateUnitFieldLinkText("ML_Weekly_17 \\ Weekly");

            dbHelper.serviceProvisionStartReason.UpdateServiceProvisionDefaultBrokerageStartReason(serviceProvisionStartReasonId, false);

            /**********************************************************/

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-13478")]
        [Description("")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch3_UITestMethod002()
        {
            #region Random Codes

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code3 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code4 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code5 = commonMethodsHelper.GetRandomValue(1, 2147483647);

            #endregion

            #region  RateType

            if (!dbHelper.rateType.GetByName("Hours (One only)").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Hours (One only)", code, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId1 = dbHelper.rateType.GetByName("Hours (One only)")[0];

            if (!dbHelper.rateType.GetByName("Weekly").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Weekly", code3, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId3 = dbHelper.rateType.GetByName("Weekly")[0];

            if (!dbHelper.rateType.GetByName("Hours (Part)").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Hours (Part)", code4, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId4 = dbHelper.rateType.GetByName("Hours (Part)")[0];

            if (!dbHelper.rateType.GetByName("Units (Whole)").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Units (Whole)", code5, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId5 = dbHelper.rateType.GetByName("Units (Whole)")[0];

            #endregion

            #region  RateUnit

            if (!dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (One only)").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "Per 1 Hour", new DateTime(2020, 1, 1), code, _rateTypeId1);
            var rateUnit1ID = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (One only)").FirstOrDefault();

            if (!dbHelper.rateUnit.GetByName("ML_Weekly_17 \\ Weekly").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "ML_Weekly_17", new DateTime(2020, 1, 1), code3, _rateTypeId3);
            var rateUnit3ID = dbHelper.rateUnit.GetByName("ML_Weekly_17 \\ Weekly").FirstOrDefault();

            if (!dbHelper.rateUnit.GetByName("ML_Whole_10 \\ Units (Whole)").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "ML_Whole_10", new DateTime(2020, 1, 1), code4, _rateTypeId5);
            var rateUnit4ID = dbHelper.rateUnit.GetByName("ML_Whole_10 \\ Units (Whole)").FirstOrDefault();

            if (!dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Part)").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "Per 1 Hour", new DateTime(2020, 1, 1), code5, _rateTypeId4);
            var rateUnit5ID = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Part)").FirstOrDefault();

            #endregion

            #region Service Elemet 1 & 2

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(rateUnit1ID);
            validRateUnits.Add(rateUnit3ID);
            validRateUnits.Add(rateUnit4ID);
            validRateUnits.Add(rateUnit5ID);

            if (!dbHelper.serviceElement1.GetByName("23jun20").Any())
                dbHelper.serviceElement1.CreateServiceElement1(teamID, "23jun20", new DateTime(2021, 1, 1), code, 1, 1, validRateUnits, rateUnit3ID);
            var serviceElement1ID2 = dbHelper.serviceElement1.GetByName("23jun20")[0];

            if (!dbHelper.serviceElement2.GetByName("AD_SE2").Any())
                dbHelper.serviceElement2.CreateServiceElement2(teamID, "AD_SE2", new DateTime(2021, 1, 1), code);
            var serviceElement2ID = dbHelper.serviceElement2.GetByName("AD_SE2")[0];

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1ID2, serviceElement2ID);

            #endregion

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            var newEpisodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID_AcuteHospital, brokerageEpisodePriorityID_Standard, DateTime.Now.AddDays(-5).Date, null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(newEpisodeId, 3); //update status to Sourcing in Progress

            #region Step 2

            /************************* Step 2 *************************/

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .OpenRecord(newEpisodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("New")
                .InsertReceivedDateTime(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"), "09:00")
                .ClickProviderLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(providerName).TapSearchButton().SelectResultElement(providerID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(1500);

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad();

            var offers = dbHelper.brokerageOffer.GetByBrokerageEpisodeId(newEpisodeId);
            Assert.AreEqual(1, offers.Count);
            var newOfferId = offers.FirstOrDefault();

            caseBrokerageEpisodeOffersPage
                .OpenRecord(newOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateProviderRegisteredInCareDirectorOptionsDisabled(true)
                .ValidateProviderLookupButtondDisabled(true);

            /**********************************************************/

            #endregion

            #region Step 3

            /************************* Step 3 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .SelectStatus("Rejected")
                .ClickRejectionReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Reason_1").TapSearchButton().SelectResultElement(brokerageOfferRejectionReasonID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(newOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoadDisabled("Brokerage Offer from " + providerName + " received on ")
                .ValidateProviderRegisteredInCareDirectorOptionsDisabled(true)
                .ValidateProviderLookupButtondDisabled(true);

            /**********************************************************/

            #endregion

            #region Step 4

            /************************* Step 4 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .ClickBackButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("New")
                .InsertReceivedDateTime(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"), "09:00")
                .ClickProviderRegisteredInCareDirectorNoRadioButton()

                .ValidateProviderFieldVisible(false)
                .ValidateExternalProviderFieldVisible(true)

                .ClickSaveButton()

                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateExternalProviderFieldErrorLabelVisibility(true)
                .ValidateExternalProviderFieldErrorLabelText("Please fill out this field.");


            /**********************************************************/

            #endregion

            #region Step 5

            /************************* Step 5 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .InsertExternalProvider("Some External Provider")
                .ClickSaveButton();
            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Under Review")
                .ClickSaveButton();
            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Sourced")
                .InsertSourcedDateTime(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"), "10:00")
                .ClickSaveButton();
            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .InsertCostPerWeek("100"); // if the record is editable then we should be able to set a value in one of the fieds

            /**********************************************************/

            #endregion

            #region Step 6

            /************************* Step 6 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .InsertCostPerWeek("")
                .SelectStatus("Rejected")
                .ClickRejectionReasonLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Reason_1").TapSearchButton().SelectResultElement(brokerageOfferRejectionReasonID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveButton()

                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoadDisabled("Brokerage Offer from received on")
                .ValidateExternalProviderFieldDisabled(true);

            /**********************************************************/

            #endregion

            #region Step 7

            /************************* Step 7 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .ClickBackButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .ClickNewRecordButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Under Review")
                .InsertReceivedDateTime(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"), "09:00")
                .ClickProviderRegisteredInCareDirectorNoRadioButton()
                .InsertExternalProvider("Another External Provider")
                .ClickSaveButton()

                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Sourced")
                .InsertSourcedDateTime(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"), "10:00")
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(newEpisodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .SelectStatus("Approved")
                .ClickServiceElement1LookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("23jun20").TapSearchButton().SelectResultElement(serviceElement1ID2.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .ClickServiceElement2LookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("AD_SE2").TapSearchButton().SelectResultElement(serviceElement2ID.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .InsertPlannedStartDate(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure this Brokerage Episode should skip the approval process?").TapOKButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ");

            dbHelper = new DBHelper.DatabaseHelper();
            var lastOfferId = dbHelper.brokerageOffer.GetByBrokerageEpisodeIdAndStatus(newEpisodeId, 5).FirstOrDefault(); //get the sourced offer

            caseBrokerageEpisodeRecordPage
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(lastOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Accepted")
                .ValidateServiceProvidedFieldVisibility(false);

            /**********************************************************/

            #endregion

            #region Step 8

            /************************* Step 8 *************************/

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(5000);

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToServiceProvisionSubPage();

            caseBrokerageEpisodeServiceProvisionsPage
                .WaitForCaseBrokerageEpisodeServiceProvisionsPageToLoad();

            dbHelper = new DBHelper.DatabaseHelper();
            var serviceProvisions = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisions.Count);
            var newServiceProvisionId = serviceProvisions[0];

            caseBrokerageEpisodeServiceProvisionsPage
                .OpenRecord(newServiceProvisionId.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ValidateServiceProvidedFieldLinkText("")
                .ValidateProviderCarerFieldLinkText("");

            /**********************************************************/

            #endregion

            #region Step 9

            /************************* Step 9 *************************/

            serviceProvisionRecordPage
                .ClickBackButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(lastOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickUndoAcceptanceButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure you would like to undo the acceptance of this Offer? The status will revert to Sourced and the related Service Provision will be deleted.").TapOKButton();

            System.Threading.Thread.Sleep(3000);

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ValidateStatusSelectedText("Sourced")
                .ValidateExternalProviderFieldDisabled(false);

            /**********************************************************/

            #endregion

            #region Step 10

            /************************* Step 10 *************************/

            dbHelper = new DBHelper.DatabaseHelper();
            serviceProvisions = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(0, serviceProvisions.Count);

            /**********************************************************/

            #endregion
        }

        [TestProperty("JiraIssueID", "CDV6-13496")]
        [Description("")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch3_UITestMethod003()
        {
            #region Random Codes

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code2 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code3 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code4 = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var code5 = commonMethodsHelper.GetRandomValue(1, 2147483647);

            #endregion

            #region  RateType

            if (!dbHelper.rateType.GetByName("Hours (One only)").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Hours (One only)", code, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId1 = dbHelper.rateType.GetByName("Hours (One only)")[0];

            if (!dbHelper.rateType.GetByName("Days").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Days", code2, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId2 = dbHelper.rateType.GetByName("Days")[0];

            if (!dbHelper.rateType.GetByName("Weekly").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Weekly", code3, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId3 = dbHelper.rateType.GetByName("Weekly")[0];

            if (!dbHelper.rateType.GetByName("Hours (Part)").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Hours (Part)", code4, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId4 = dbHelper.rateType.GetByName("Hours (Part)")[0];

            if (!dbHelper.rateType.GetByName("Units (Whole)").Any())
                dbHelper.rateType.CreateRateType(teamID, _careDirectorQA_BusinessUnitId, "Units (Whole)", code5, new DateTime(2020, 1, 1), 5, 6, 7);
            var _rateTypeId5 = dbHelper.rateType.GetByName("Units (Whole)")[0];

            #endregion

            #region  RateUnit

            if (!dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (One only)").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "Per 1 Hour", new DateTime(2020, 1, 1), code, _rateTypeId1);
            var rateUnit1ID = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (One only)").FirstOrDefault();

            if (!dbHelper.rateUnit.GetByName("Per Day \\ Days").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "Per Day", new DateTime(2020, 1, 1), code2, _rateTypeId2);
            var rateUnit2ID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            if (!dbHelper.rateUnit.GetByName("ML_Weekly_17 \\ Weekly").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "ML_Weekly_17", new DateTime(2020, 1, 1), code3, _rateTypeId3);
            var rateUnit3ID = dbHelper.rateUnit.GetByName("ML_Weekly_17 \\ Weekly").FirstOrDefault();

            if (!dbHelper.rateUnit.GetByName("ML_Whole_10 \\ Units (Whole)").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "ML_Whole_10", new DateTime(2020, 1, 1), code4, _rateTypeId5);
            var rateUnit4ID = dbHelper.rateUnit.GetByName("ML_Whole_10 \\ Units (Whole)").FirstOrDefault();

            if (!dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Part)").Any())
                dbHelper.rateUnit.CreateRateUnit(teamID, _careDirectorQA_BusinessUnitId, "Per 1 Hour", new DateTime(2020, 1, 1), code5, _rateTypeId4);
            var rateUnit5ID = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Part)").FirstOrDefault();

            #endregion

            #region Service Elemet 1 & 2

            var validRateUnits1 = new List<Guid>();
            validRateUnits1.Add(rateUnit1ID);
            validRateUnits1.Add(rateUnit3ID);
            validRateUnits1.Add(rateUnit5ID);

            var validRateUnits2 = new List<Guid>();
            validRateUnits2.Add(rateUnit1ID);
            validRateUnits2.Add(rateUnit3ID);
            validRateUnits2.Add(rateUnit4ID);
            validRateUnits2.Add(rateUnit5ID);

            if (!dbHelper.serviceElement1.GetByName("08julsd").Any())
                dbHelper.serviceElement1.CreateServiceElement1(teamID, "08julsd", new DateTime(2021, 1, 1), code, 1, 1, validRateUnits1);
            var serviceElement1ID1 = dbHelper.serviceElement1.GetByName("08julsd")[0];

            if (!dbHelper.serviceElement1.GetByName("23jun20").Any())
                dbHelper.serviceElement1.CreateServiceElement1(teamID, "23jun20", new DateTime(2021, 1, 1), code2, 1, 1, validRateUnits2, rateUnit3ID);
            var serviceElement1ID2 = dbHelper.serviceElement1.GetByName("23jun20")[0];

            if (!dbHelper.serviceElement2.GetByName("AD_SE2").Any())
                dbHelper.serviceElement2.CreateServiceElement2(teamID, "AD_SE2", new DateTime(2021, 1, 1), code);
            var serviceElement2ID = dbHelper.serviceElement2.GetByName("AD_SE2")[0];

            commonMethodsDB.CreateServiceMapping(teamID, serviceElement1ID2, serviceElement2ID);

            #endregion

            #region Provider

            var providerName = "Work Test" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerID = dbHelper.provider.CreateProvider(providerName, teamID, 2); //creating a "Supplier" provider

            #endregion

            #region Serivce Provider

            var serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(teamID, _loginUserId, providerID, serviceElement1ID2, serviceElement2ID, null, null, null, null, 2, false);

            var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(teamID, serviceProvidedId, rateUnit3ID, new DateTime(2022, 1, 1), 2);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(teamID, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

            #endregion

            var newEpisodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID_AcuteHospital, brokerageEpisodePriorityID_Standard, DateTime.Now.AddDays(-5).Date, null, status, 0, 0, contractType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(newEpisodeId, 3); //update status to Sourcing in Progress

            var newBrokerageOfferId = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, newEpisodeId, caseID, personID, "Some External Provider", DateTime.Now.AddDays(-5).Date, 1);
            dbHelper.brokerageOffer.UpdateBrokerageOfferStatus(newBrokerageOfferId, 4); //update status to under review
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(newBrokerageOfferId, 5, DateTime.Now.Date); //set the status to Sourced. This will automatically update the brokerage episode status as well

            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeForApprovedStatus(newEpisodeId, 6, serviceElement1ID2, serviceElement2ID, DateTime.Now.AddDays(-5).Date); //approve the episode record



            #region Step 2

            /************************* Step 2 *************************/

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .OpenRecord(newEpisodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personName + " received on ")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(newBrokerageOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .SelectStatus("Accepted")
                .ClickProviderRegisteredInCareDirectorYesRadioButton()
                .ClickProviderLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(providerName).TapSearchButton().SelectResultElement(providerID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickRateUnitLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("ML_Weekly_17").TapSearchButton().SelectResultElement(rateUnit3ID.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .ClickServiceProvidedLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(serviceProvidedId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .ClickSaveAndCloseButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToServiceProvisionSubPage();

            caseBrokerageEpisodeServiceProvisionsPage
                .WaitForCaseBrokerageEpisodeServiceProvisionsPageToLoad();

            var serviceProvisions = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(1, serviceProvisions.Count());
            var newServiceProvisionId = serviceProvisions[0];

            caseBrokerageEpisodeServiceProvisionsPage
                .OpenRecord(newServiceProvisionId.ToString());

            serviceProvisionRecordPage
                .WaitForServiceProvisionRecordPageToLoad()
                .NavigateToDetailsTab()
                .ValidateServiceProvidedFieldLinkText(providerName + " \\ 23jun20 \\ AD_SE2 \\ \\ \\ Spot")
                .ValidateProviderCarerFieldLinkText(providerName);

            /**********************************************************/

            #endregion

            #region Step 3

            /************************* Step 3 *************************/

            serviceProvisionRecordPage
                .ClickBackButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(newBrokerageOfferId.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoadDisabled("Brokerage Offer from " + providerName + " received on")
                .ClickUndoAcceptanceButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure you would like to undo the acceptance of this Offer? The status will revert to Sourced and the related Service Provision will be deleted.").TapOKButton();

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad();

            System.Threading.Thread.Sleep(1200);

            caseBrokerageEpisodeOfferRecordPage
                .ValidateStatusSelectedText("Sourced");

            serviceProvisions = dbHelper.serviceProvision.GetServiceProvisionByPersonID(personID);
            Assert.AreEqual(0, serviceProvisions.Count());

            /**********************************************************/

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-13500")]
        [Description("")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_Batch3_UITestMethod004()
        {
            var documentid = commonMethodsDB.CreateDocumentIfNeeded("Brokerage Assessment Creation - Schedule Time", "Brokerage Assessment Creation - Schedule Time.Zip");
            commonMethodsDB.CreateDocumentBusinessObjectMapping("Brokerage Assessment Creation - Schedule Time - Brokerage Episode", "Brokerage Assessment Creation - Schedule Time - Brokerage Episode.Zip");

            var workflowId = commonMethodsDB.CreateWorkflowIfNeeded("Brokerage episode creation with Scheduling", "Brokerage episode creation with Scheduling.Zip");

            #region Step 1

            /************************* Step 1 *************************/

            loginPage
                .GoToLoginPage()
                .Login(_loginUserName, "Passw0rd_!");

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
                .ClickNewRecordButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .InsertRequestReceivedDateTime("21/01/2021", "11:58")
                .ClickSourceOfRequestLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Acute Hospital").TapSearchButton().SelectResultElement(sourceOfBrokerageRequestsID_AcuteHospital.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .ClickPriorityLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Standard").TapSearchButton().SelectResultElement(brokerageEpisodePriorityID_Standard.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("New")
                .ClickScheduleRequiredYesRadioButton()
                .InsertMondayMinutes("20")
                .ValidateTotalMinutesPerWeekFieldText("20");


            /**********************************************************/

            #endregion

            #region Step 2

            /************************* Step 2 *************************/

            caseBrokerageEpisodeRecordPage
                .InsertTuesdayMinutes("20")
                .InsertFridayMinutes("10")
                .ValidateTotalMinutesPerWeekFieldText("50");

            /**********************************************************/

            #endregion

            #region Step 3

            /************************* Step 3 *************************/

            caseBrokerageEpisodeRecordPage
                .InsertMondayMinutes("")
                .InsertTuesdayMinutes("20")
                .InsertFridayMinutes("10")
                .ValidateTotalMinutesPerWeekFieldText("30");

            /**********************************************************/

            #endregion

            #region Step 4

            /************************* Step 4 *************************/

            caseBrokerageEpisodeRecordPage
                .InsertThursdayHealthMinutes("20")

                .ValidateTotalHealthMinutesPerWeekFieldText("20")

                .ValidateMondayFieldText("")
                .ValidateTuesdayFieldText("")
                .ValidateWednesdayFieldText("")
                .ValidateThursdayFieldText("20")
                .ValidateFridayFieldText("")
                .ValidateSaturdayFieldText("")
                .ValidateSundayFieldText("")
                .ValidateTotalMinutesPerWeekFieldText("20");

            /**********************************************************/

            #endregion

            #region Step 5

            /************************* Step 5 *************************/

            caseBrokerageEpisodeRecordPage
                .InsertWednesdaySocialCareMinutes("50")

                .ValidateTotalSocialCareMinutesPerWeekFieldText("50")

                .ValidateMondayFieldText("")
                .ValidateTuesdayFieldText("")
                .ValidateWednesdayFieldText("50")
                .ValidateThursdayFieldText("20")
                .ValidateFridayFieldText("")
                .ValidateSaturdayFieldText("")
                .ValidateSundayFieldText("")
                .ValidateTotalMinutesPerWeekFieldText("70");

            /**********************************************************/

            #endregion

            #region Step 6

            /************************* Step 6 *************************/

            caseBrokerageEpisodeRecordPage
                .InsertWednesdayHealthMinutes("10")
                .InsertThursdaySocialCareMinutes("10")

                .ValidateTotalHealthMinutesPerWeekFieldText("30")
                .ValidateTotalSocialCareMinutesPerWeekFieldText("60")

                .ValidateMondayFieldText("")
                .ValidateTuesdayFieldText("")
                .ValidateWednesdayFieldText("60")
                .ValidateThursdayFieldText("30")
                .ValidateFridayFieldText("")
                .ValidateSaturdayFieldText("")
                .ValidateSundayFieldText("")
                .ValidateTotalMinutesPerWeekFieldText("90");

            /**********************************************************/

            #endregion

            #region Step 7

            /************************* Step 7 *************************/

            caseBrokerageEpisodeRecordPage
                .InsertWednesdayHealthMinutes("")
                .InsertThursdaySocialCareMinutes("")

                .ValidateTotalHealthMinutesPerWeekFieldText("20")
                .ValidateTotalSocialCareMinutesPerWeekFieldText("50")

                .ValidateMondayFieldText("")
                .ValidateTuesdayFieldText("")
                .ValidateWednesdayFieldText("50")
                .ValidateThursdayFieldText("20")
                .ValidateFridayFieldText("")
                .ValidateSaturdayFieldText("")
                .ValidateSundayFieldText("")
                .ValidateTotalMinutesPerWeekFieldText("70");

            /**********************************************************/

            #endregion

            #region Step 8

            /************************* Step 8 *************************/

            caseBrokerageEpisodeRecordPage
                .InsertWednesdayHealthMinutes("")
                .InsertThursdayHealthMinutes("")
                .InsertWednesdaySocialCareMinutes("")
                .InsertThursdaySocialCareMinutes("")

                .InsertTotalHealthMinutesPerWeek("70")
                .InsertTotalSocialCareMinutesPerWeek("80")

                .ValidateMondayFieldText("")
                .ValidateTuesdayFieldText("")
                .ValidateWednesdayFieldText("")
                .ValidateThursdayFieldText("")
                .ValidateFridayFieldText("")
                .ValidateSaturdayFieldText("")
                .ValidateSundayFieldText("")
                .ValidateTotalMinutesPerWeekFieldText("150");

            /**********************************************************/

            #endregion

            #region Step 9

            /************************* Step 9 *************************/

            caseBrokerageEpisodeRecordPage
                .InsertTotalHealthMinutesPerWeek("70")
                .InsertTotalSocialCareMinutesPerWeek("")

                .ValidateMondayFieldText("")
                .ValidateTuesdayFieldText("")
                .ValidateWednesdayFieldText("")
                .ValidateThursdayFieldText("")
                .ValidateFridayFieldText("")
                .ValidateSaturdayFieldText("")
                .ValidateSundayFieldText("")
                .ValidateTotalMinutesPerWeekFieldText("70");

            /**********************************************************/

            #endregion

            #region Step 10

            /************************* Step 10 *************************/

            caseBrokerageEpisodeRecordPage
                .InsertTuesdayHealthMinutes("20")
                .InsertWednesdayHealthMinutes("")

                .ValidateTotalHealthMinutesPerWeekFieldText("20")
                .ValidateTotalSocialCareMinutesPerWeekFieldText("")

                .ValidateMondayFieldText("")
                .ValidateTuesdayFieldText("20")
                .ValidateWednesdayFieldText("")
                .ValidateThursdayFieldText("")
                .ValidateFridayFieldText("")
                .ValidateSaturdayFieldText("")
                .ValidateSundayFieldText("")
                .ValidateTotalMinutesPerWeekFieldText("20");

            /**********************************************************/

            #endregion

            #region Step 11

            /************************* Step 11 *************************/

            caseBrokerageEpisodeRecordPage
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            var assessmentid = dbHelper.caseForm.CreateCaseForm(teamID, personID, _personName, _loginUserId, caseID, caseNumber, documentid, "Brokerage Assessment Creation - Schedule Time", 4, DateTime.Now.Date, null, null);

            //get the Document Question Identifier for 'Monday ( Social Care Minutes)'
            var documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1265")[0];

            //set the answer for the Monday ( Social Care Minutes) question
            var documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(assessmentid, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateNumericAnswer(documentAnswerID, 15);


            //get the Document Question Identifier for 'Monday ( Social Care Minutes)'
            documentQuestionIdentifierId = dbHelper.documentQuestionIdentifier.GetByIdentifier("QA-DQ-1266")[0];

            //set the answer for the Monday ( Social Care Minutes) question
            documentAnswerID = dbHelper.documentAnswer.GetDocumentAnswer(assessmentid, documentQuestionIdentifierId)[0];
            dbHelper.documentAnswer.UpdateNumericAnswer(documentAnswerID, 10);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(caseNumber, caseID.ToString())
                .OpenCaseRecord(caseID.ToString(), caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(assessmentid.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .SelectStatus("In Progress")
                .TapSaveAndCloseButton();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad();


            //get all "Not Started" workflow jobs
            var newWorkflowJobId = dbHelper.workflowJob.GetWorkflowJobByWorkflowId(workflowId, 1).FirstOrDefault();

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the Workflow Job and wait for the Idle status
            this.WebAPIHelper.WorkflowJob.Execute(newWorkflowJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.workflowJob.WaitForWorkflowJobFinishedExecuting(newWorkflowJobId);


            var brokerageEpisodes = dbHelper.brokerageEpisode.GetBrokerageEpisodeByCaseID(caseID);
            Assert.AreEqual(1, brokerageEpisodes.Count());
            var newEpisodeId = brokerageEpisodes[0];


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
                .OpenRecord(newEpisodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad()

                .ValidateMondayHealthMinutesFieldText("10")
                .ValidateMondaySocialCareMinutesFieldText("15")

                .ValidateTotalHealthMinutesPerWeekFieldText("10")
                .ValidateTotalSocialCareMinutesPerWeekFieldText("15")

                .ValidateMondayFieldText("25")
                .ValidateTuesdayFieldText("0")
                .ValidateWednesdayFieldText("0")
                .ValidateThursdayFieldText("0")
                .ValidateFridayFieldText("0")
                .ValidateSaturdayFieldText("0")
                .ValidateSundayFieldText("0")
                .ValidateTotalMinutesPerWeekFieldText("25");

            /**********************************************************/

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
