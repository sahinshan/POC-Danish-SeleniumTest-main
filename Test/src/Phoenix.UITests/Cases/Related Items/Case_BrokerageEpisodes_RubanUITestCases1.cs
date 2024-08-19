using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases.Related_Items
{
    [TestClass]
    public class Case_BrokerageEpisodes_RubanUITestCases1 : FunctionalTest
    {
        public Guid UpdateBrokerageEpisodeTrackingStatusScheduledJob
        {
            get
            {
                return new Guid("6300d270-5562-eb11-a312-0050569231cf");
            }
        }

        private Guid _ethnicityId;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private Guid _businessUnitId;
        private Guid teamID;
        private string _teamName;
        private Guid systemUserId;
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
        private Guid pauseReasonid;
        private int contactType = 1; // Spot
        private int status = 1; // new
        private Guid brokerageEpisodeTrackingStatusId;
        private Guid brokerageTargetSetupId;
        private Guid brokerageTargetTrackingStatusSetupId;
        private Guid brokerageOfferRejectionReasonId;

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

                _systemUserName = "Case_Brokerage_Episodes_User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CaseBrokerageEpisodes", "User1", "Passw0rd_!", _businessUnitId, teamID, _languageId, _authenticationproviderid);

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

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", teamID);

                #endregion

                #region Data Form

                dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Case

                caseID = dbHelper.Case.CreateSocialCareCaseRecord(teamID, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, dataFormId, null, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
                caseNumber = (string)dbHelper.Case.GetCaseByID(caseID, "casenumber")["casenumber"];

                #endregion

                #region Source of Brokerage Requests

                sourceOfBrokerageRequestsID = dbHelper.brokerageRequestSource.GetByName("Community Hospital")[0];

                sourceOfBrokerageRequestsID2 = dbHelper.brokerageRequestSource.GetByName("Acute Hospital")[0];

                #endregion

                #region Source of Brokerage Requests 2

                sourceOfBrokerageRequestsID2 = dbHelper.brokerageRequestSource.GetByName("Acute Hospital")[0];

                #endregion

                #region Brokerage Episode Priority

                if (!dbHelper.brokerageEpisodePriority.GetByName("Priority").Any())
                    dbHelper.brokerageEpisodePriority.CreateBrokerageEpisodePriority(new Guid("8a5baf95-fc3d-eb11-a2e5-0050569231cf"), "Priority", new DateTime(2021, 1, 1), teamID);
                brokerageEpisodePriorityID = dbHelper.brokerageEpisodePriority.GetByName("Priority")[0];

                #endregion

                #region Brokerage Episode Pause Reasons

                if (!dbHelper.brokerageEpisodePauseReason.GetByName("Pause Reason 1").Any())
                    dbHelper.brokerageEpisodePauseReason.CreateBrokerageEpisodePauseReason(new Guid("a8e586a7-0750-eb11-a2f6-005056926fe4"), "Pause Reason 1", new DateTime(2021, 1, 1), teamID, false, true);
                pauseReasonid = dbHelper.brokerageEpisodePauseReason.GetByName("Pause Reason 1")[0];

                #endregion

                #region Brokerage Episode Tracking Status

                if (!dbHelper.brokerageEpisodeTrackingStatus.GetByName("Green_Good").Any())
                    dbHelper.brokerageEpisodeTrackingStatus.CreateBrokerageEpisodeTrackingStatus("Green_Good", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), teamID);
                brokerageEpisodeTrackingStatusId = dbHelper.brokerageEpisodeTrackingStatus.GetByName("Green_Good")[0];

                #endregion

                #region Brokerage Target Setup

                if (!dbHelper.brokerageTargetSetup.GetByName("Acute Hospital / Priority").Any())
                    dbHelper.brokerageTargetSetup.CreateBrokerageEpisodeTargetSetup(sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, 2, new DateTime(DateTime.Now.AddYears(-2).Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day), teamID, 7);
                brokerageTargetSetupId = dbHelper.brokerageTargetSetup.GetByName("Acute Hospital / Priority")[0];

                #endregion

                #region Brokerage Target Tracking Status Setup

                if (!dbHelper.brokerageTargetTrackingStatusSetup.GetByName("Green_Good / 6").Any())
                    dbHelper.brokerageTargetTrackingStatusSetup.CreateBrokerageTargetTrackingStatusSetup(brokerageTargetSetupId, brokerageEpisodeTrackingStatusId, 6, teamID);
                brokerageTargetTrackingStatusSetupId = dbHelper.brokerageTargetTrackingStatusSetup.GetByName("Green_Good / 6")[0];

                #endregion

                #region Brokerage Offer Rejection Reason
                if (!dbHelper.brokerageOfferRejectionReason.GetByName("Reason_1").Any())
                    dbHelper.brokerageOfferRejectionReason.CreateBrokerageOfferRejectionReason("Reason_1", new DateTime(2021, 1, 6), teamID);
                brokerageOfferRejectionReasonId = dbHelper.brokerageOfferRejectionReason.GetByName("Reason_1")[0];


                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        private void CreateTestData(Guid businessUnitId, Guid teamID, string PersonMiddleName)
        {
            #region Ethnicity

            if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                dbHelper.ethnicity.CreateEthnicity(teamID, "Irish", new DateTime(2020, 1, 1));
            _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

            #endregion

            #region Language

            if (!dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any())
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language

            #region Authentication Provider

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

            #endregion

            #region System User

            if (!dbHelper.systemUser.GetSystemUserByUserName("Testing_cdv6_13473").Any())
            {
                systemUserId = dbHelper.systemUser.CreateSystemUser("Testing_cdv6_13473", "Testing", "cdv6_13473", "Testing cdv6_13473", "Passw0rd_!", "Testing_cdv6_13473@somemail.com", "Testing_cdv6_13473@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, businessUnitId, teamID, DateTime.Now.Date);

                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                dbHelper.userSecurityProfile.CreateUserSecurityProfile(systemUserId, systemAdministratorSecurityProfileId);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(systemUserId, systemUserSecureFieldsSecurityProfileId);
            }
            if (systemUserId == Guid.Empty)
            {
                systemUserId = dbHelper.systemUser.GetSystemUserByUserName("Testing_cdv6_13473").FirstOrDefault();
            }

            dbHelper.systemUser.UpdateLastPasswordChangedDate(systemUserId, DateTime.Now.Date);

            #endregion

            #region Contact Reason

            if (!dbHelper.contactReason.GetByName("Adoption Enquiry").Any())
                dbHelper.contactReason.CreateContactReason(teamID, "Adoption Enquiry", new DateTime(2020, 1, 1), 110000000, false);
            _contactReasonId = dbHelper.contactReason.GetByName("Adoption Enquiry")[0];

            #endregion

            #region Contact Source

            if (!dbHelper.contactSource.GetByName("Other").Any())
                dbHelper.contactSource.CreateContactSource(teamID, "Other", new DateTime(2020, 1, 1));
            _contactSourceId = dbHelper.contactSource.GetByName("Other")[0];

            #endregion

            #region Person

            personID = dbHelper.person.CreatePersonRecord("", "van", PersonMiddleName, "Episode", "", DateTime.Now.Date.AddYears(-18), _ethnicityId, teamID, 1, 1);
            #endregion

            #region Case Status

            _caseStatusId = dbHelper.caseStatus.GetByName("Allocate to Team").FirstOrDefault();

            #endregion

            #region Data Form

            dataFormId = dbHelper.dataForm.GetByName("SocialCareCase").FirstOrDefault();

            #endregion

            #region Case

            caseID = dbHelper.Case.CreateSocialCareCaseRecord(teamID, personID, systemUserId, systemUserId, _caseStatusId, _contactReasonId, dataFormId, _contactSourceId, DateTime.Now.Date, DateTime.Now.Date, 18);
            caseNumber = (string)dbHelper.Case.GetCaseByID(caseID, "casenumber")["casenumber"];

            #endregion

        }

        #region https: //advancedcsg.atlassian.net/browse/CDV6-13162

        [TestProperty("JiraIssueID", "CDV6-13184")]
        [Description("Open active social care case record with Brokerage episode " +
            "Click on More option Button Select Pause Record Button" +
            "In Pause Record Popup enter all the mandatory fields" +
            "Hit Save Option and Validate Pause Record is saved" +
            "Validate Target Date and Time Fields in Brokerage Episode is 'Null'" +
            "Again Click on More Option Button Select Restart Episode Enter Restart Date and Time and Click Save" +
            "Validate Restart Date and Time Updated In the Pause Episode Record" +
            "Validate Target Date And Time Fields In Brokerage Episode is Automatically Filled" +
            "Again Click on More Option Button And Select Pause Episode Option" +
            "In Pause Record Popup enter all the mandatory fields with Same Date/Time  and Click Save Option" +
            "Validate the  Dynamic alert popup is Displayed with message (The selected Pause Date/Time overlaps with existing pause periods)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Case_BrokerageEpisodes_UITestMethod001()
        {
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, 1, 0, 0, contactType, false, false, false, false, true, true, false);
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
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Pause Reason 1")
                .TapSearchButton()
                .SelectResultElement(pauseReasonid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRefreshButton()
                .ValidateStatusSelectedText("Paused")
                .ValidateTargetDateTime("", "");

            //Validating Pause Record is created
            dbHelper = new DBHelper.DatabaseHelper();
            var BrokerageEpisodePausedRecords = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(1, BrokerageEpisodePausedRecords.Count);

            //Restarting Episode
            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRestartEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertRestartDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateStatusSelectedText("Sourcing in Progress")
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Pause Reason 1")
                .TapSearchButton()
                .SelectResultElement(pauseReasonid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The selected Pause Date/Time overlaps with existing pause periods")
                .TapCloseButton();
        }

        [TestProperty("JiraIssueID", "CDV6-13186")]
        [Description("Open active social care case record with Brokerage episode " +
            "Click on More option Button Select Pause Record Button" +
            "In Pause Record Popup enter all the mandatory fields" +
            "Hit Save Option and Validate Pause Record is saved" +
            "Again Click on More Option Button Select Restart Episode Enter Restart Date and Time and Click Save" +
            "Validate Restart Date and Time Updated In the Pause Episode Record" +
            "Again Click on More Option Button And Select Pause Episode Option" +
            "In Pause Record Popup enter all the mandatory fields with Different Time and Click Save Option" +
            "Validate the  Second Pause Record is created")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Case_BrokerageEpisodes_UITestMethod002()
        {
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
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
                .ValidateStatusSelectedText("Sourcing in Progress")
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Pause Reason 1")
                .TapSearchButton()
                .SelectResultElement(pauseReasonid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateStatusSelectedText("Paused");

            //validating pasue Record is created
            dbHelper = new DBHelper.DatabaseHelper();
            var BrokerageEpisodePausedRecords = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(1, BrokerageEpisodePausedRecords.Count);

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRestartEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertRestartDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateStatusSelectedText("Sourcing in Progress")
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertPauseTime("01:00")
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Pause Reason 1")
                .TapSearchButton()
                .SelectResultElement(pauseReasonid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ");

            dbHelper = new DBHelper.DatabaseHelper();
            var BrokerageEpisodePausedRecords1 = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(2, BrokerageEpisodePausedRecords1.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-13220")]
        [Description("Open active social care case record with Brokerage episode " +
            "Click on More option Button Select Pause Record Button" +
            "In Pause Record Popup enter all the mandatory fields" +
            "Hit Save Option and Validate Pause Record is saved" +
            "In Brokerage Episode Change the Request Received Time and then Save The Record" +
            "Validate Alert Message is Displayed('Request recieved date is changed. Saving this record will delete pause records that are before the request recieved date')" +
            "Tap Okay Button in the Alert Popup" +
            "Validate Brokerage Pause Record Is Deleted")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Case_BrokerageEpisodes_UITestMethod003()
        {
            //Creating Brokerage Episode
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-5), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
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
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Pause Reason 1")
                .TapSearchButton()
                .SelectResultElement(pauseReasonid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ");

            //validating pasue Record is created
            dbHelper = new DBHelper.DatabaseHelper();
            var BrokerageEpisodePausedRecords = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(1, BrokerageEpisodePausedRecords.Count);

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .InsertRequestReceivedDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:10")
                .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Request Received Date/Time has changed. Saving this record will delete Pause Records that are before Request Received Date/Time")
                .TapOKButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ");

            //Validating Brokerage Episode Pause Record is Deleted
            dbHelper = new DBHelper.DatabaseHelper();
            var BrokerageEpisodePausedRecords1 = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(0, BrokerageEpisodePausedRecords1.Count);
        }

        [TestProperty("JiraIssueID", "CDV6-13221")]
        [Description("Open active social care case record with Brokerage episode " +
            "Click on More option Button Select Pause Record Button" +
            "In Pause Record Popup enter all the mandatory fields" +
            "Hit Save Option and Validate Pause Record is saved" +
            "Click on Menu Pause Episode option and then select the pause period Record and hit delete option" +
            "Validate Pause Period record is deleted and then click Details Button" +
            "Click on Refresh Button in the Brokerage Episode Record Page" +
            "Validate Target Date and Time and Tracking Status Field is auto populated")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Case_BrokerageEpisodes_UITestMethod004()
        {
            var sourceDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-2).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, sourceDate, sourceDate, status, 0, 0, contactType, false, false, false, false, true, true, false);

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
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Pause Reason 1")
                .TapSearchButton()
                .SelectResultElement(pauseReasonid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ");

            //validating pasue Record is created
            dbHelper = new DBHelper.DatabaseHelper();
            var BrokerageEpisodePausedRecords = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(1, BrokerageEpisodePausedRecords.Count);

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateTargetDateTime("", "")
                .NavigateToBrokeragePauseEpisodeSubPage();

            caseBrokerageEisodePausePeriodPage
                .WaitForCaseBrokerageEpisodePausePeriodPageToLoad()
                .SelectRecord(BrokerageEpisodePausedRecords[0].ToString())
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton()
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            caseBrokerageEisodePausePeriodPage
                .WaitForNoCaseBrokerageEpisodePausePeriodPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad();

            //Validating Brokerage Episode Pause Record is Deleted
            dbHelper = new DBHelper.DatabaseHelper();
            var BrokerageEpisodePausedRecords1 = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(0, BrokerageEpisodePausedRecords1.Count);

            caseBrokerageEpisodesPage
                .OpenRecord(episodeId.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRefreshButton()
                .ValidateTrackingStatusLinkFieldText("Green_Good")
                .ValidateTargetDateTime(sourceDate.AddDays(07).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), sourceDate.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture));
        }

        [TestProperty("JiraIssueID", "CDV6-13222")]
        [Description("Open active social care case record with Brokerage episode Status as  Sourced Ready for Approval, Approval Rejected,Approved,Awaiting Commencement, Completed and Cancelled " +
            "Validate Pause Episode Option is not visible in more options for all the Status as Mention above")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Case_BrokerageEpisodes_UITestMethod005()
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
            var serviceprovisionstartreasonid = dbHelper.serviceProvisionStartReason.GetByName("Service Provision Start Reason Test")[0];

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

            #region Brokerage Episode

            //Creating Sourced Record For Brokerage Episode
            var episodeIdWithStatusSourced = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusSourced, 10); //update status to Sourced

            //Creating Ready For Approval Record For Brokerage Episode
            var episodeIdWithStatusReadyForApproval = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusReadyForApproval, 4); //Ready for Approval

            //Creating Approval Rejected Record For Brokerage Episode
            var episodeIdWithStatusApprovalRejected = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusApprovalRejected, 5); //Approval Rejected

            //Creating Approved Record For Brokerage Episode
            var episodeIdWithStatusApproved = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusApproved, 6); //Approved

            //Creating Completed Record For Brokerage Episode
            var episodeIdWithStatusCompleted = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusCompleted, 3); //update status to Sourcing in Progress
            var underReviewId1 = dbHelper.brokerageOffer.CreateBrokerageOffer(teamID, episodeIdWithStatusCompleted, caseID, personID, providerID, DateTime.Now.AddDays(-4), 4, true);
            dbHelper.brokerageOffer.UpdateBrokerageOfferSourcedStatus(underReviewId1, 5, sourcedDate);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisodeStatusApproved(episodeIdWithStatusCompleted, 6, ServiceElement1Id, ServiceElement2Id, DateTime.Now); //Approved
            dbHelper.brokerageOffer.UpdateBrokerageOfferApprovedStatus(underReviewId1, 6, providerID, rateunitid, serviceproviderId);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusCompleted, 8); //Completed

            //Creating Cancelled Record For Brokerage Episode
            var episodeIdWithStatusCancelled = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusCancelled, 7); //Cancelled

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
                .OpenRecord(episodeIdWithStatusSourced.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidatePasueEpisodeButtonVisbility(false)
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusReadyForApproval.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidatePasueEpisodeButtonVisbility(false)
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusApprovalRejected.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidatePasueEpisodeButtonVisbility(false)
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusApproved.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidatePasueEpisodeButtonVisbility(false)
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusCompleted.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidatePasueEpisodeButtonVisbility(false)
                .ClickActivateButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            //Validating Awaiting Commencement Status Pause Episode option 
            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on")
                .ValidatePasueEpisodeButtonVisbility(false)
                .ValidateStatusSelectedText("Awaiting Commencement")
                .NavigateToBrokerageOffersSubPage();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersPageToLoad()
                .OpenRecord(underReviewId1.ToString());

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickUndoAcceptanceButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            caseBrokerageEpisodeOfferRecordPage
                .WaitForCaseBrokerageEpisodeOfferRecordPageToLoad()
                .ClickBackButton();

            caseBrokerageEpisodeOffersPage
                .WaitForCaseBrokerageEpisodeOffersDetailsPageToLoad()
                .ClickDetailsButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on")
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusCancelled.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on")
                .ValidatePasueEpisodeButtonVisbility(false)
                .ClickBackButton();
        }

        [TestProperty("JiraIssueID", "CDV6-13274")]
        [Description("Open active social care case record with Brokerage episode Status as New , Request Rejected,Sourcing in Progress" +
            "Validate Pause Episode Option is visible in more options for all the Status Mention as above")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Case_BrokerageEpisodes_UITestMethod006()
        {
            //Creating New Status Brokerage Episode
            var episodeIdWithStatusNew = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);

            //Creating Request Rejected Status Brokerage Episode
            var episodeIdWithStatusRequestRejected = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusRequestRejected, 2); //Request Rejected

            //Creating Sourceing In Progress Status Brokerage Episode
            var episodeIdWithStatusSourcingInProgress = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusSourcingInProgress, 3); //Sourcing in Progress

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
                .OpenRecord(episodeIdWithStatusNew.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidatePasueEpisodeButtonVisbility(true)
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusRequestRejected.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidatePasueEpisodeButtonVisbility(true)
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusSourcingInProgress.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidatePasueEpisodeButtonVisbility(true)
                .ClickBackButton();
        }

        [TestProperty("JiraIssueID", "CDV6-13275")]
        [Description("Open active social care case record with Brokerage episode Status as New , Request Rejected,Approved, Sourcing in Progress" +
            "Click on Pause Episode Button and Enter Pause Date Field with Date Before Request Received Date" +
            "Click Save Button in Pause Episode Popup" +
            "Validate Error Message Popup is Displayed with('Pause Date/Time cannot be before Request Received Date/Time')")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Case_BrokerageEpisodes_UITestMethod007()
        {

            var episodeIdWithStatusNew = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);

            var episodeIdWithStatusRequestRejected = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusRequestRejected, 2); //Request Rejected

            var episodeIdWithStatusSourcingInProgress = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusSourcingInProgress, 3); //Sourcing in Progress

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
                .OpenRecord(episodeIdWithStatusNew.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.AddDays(-02).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Pause Reason 1")
                .TapSearchButton()
                .SelectResultElement(pauseReasonid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Pause Date/Time cannot be before Request Received Date/Time")
                .TapCloseButton();

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .ClickPasueEpisodeCancelButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusRequestRejected.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.AddDays(-02).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Pause Reason 1")
                .TapSearchButton()
                .SelectResultElement(pauseReasonid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Pause Date/Time cannot be before Request Received Date/Time")
                .TapCloseButton();

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .ClickPasueEpisodeCancelButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusSourcingInProgress.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.AddDays(-02).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Pause Reason 1")
                .TapSearchButton()
                .SelectResultElement(pauseReasonid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Pause Date/Time cannot be before Request Received Date/Time")
                .TapCloseButton();

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .ClickPasueEpisodeCancelButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickBackButton();
        }

        [TestProperty("JiraIssueID", "CDV6-13276")]
        [Description("Open active social care case record with Brokerage episode Status as New , Request Rejected,Approved, Sourcing in Progress" +
            "Click on Pause Episode Button and Enter Pause Date Field with Future Date" +
            "Click Save Button in Pause Episode Popup" +
            "Validate Error Message Popup is Displayed with('Pause Date/Time cannot be in future')")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Case_BrokerageEpisodes_UITestMethod008()
        {
            //Creating New Brokerage Episode
            var episodeIdWithStatusNew = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);

            var episodeIdWithStatusRequestRejected = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusRequestRejected, 2); //Request Rejected

            var episodeIdWithStatusSourcingInProgress = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);
            dbHelper.brokerageEpisode.UpdateBrokerageEpisode(episodeIdWithStatusSourcingInProgress, 3); //Sourcing in Progress

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
                .OpenRecord(episodeIdWithStatusNew.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.AddDays(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Pause Reason 1")
                .TapSearchButton()
                .SelectResultElement(pauseReasonid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Pause Date/Time cannot be in future")
                .TapCloseButton();

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .ClickPasueEpisodeCancelButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusRequestRejected.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.AddDays(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Pause Reason 1")
                .TapSearchButton()
                .SelectResultElement(pauseReasonid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Pause Date/Time cannot be in future")
                .TapCloseButton();

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .ClickPasueEpisodeCancelButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickBackButton();

            caseBrokerageEpisodesPage
                .WaitForCaseBrokerageEpisodesPageToLoad()
                .OpenRecord(episodeIdWithStatusSourcingInProgress.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.AddDays(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Pause Reason 1")
                .TapSearchButton()
                .SelectResultElement(pauseReasonid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Pause Date/Time cannot be in future")
                .TapCloseButton();

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .ClickPasueEpisodeCancelButton()
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickBackButton();
        }

        [TestProperty("JiraIssueID", "CDV6-13312")]
        [Description("Open active social care case record with Brokerage episode Status as New " +
            "Click on Pause Episode Button and Enter Pause Date Field with Date After Request Received Date" +
            "Click Save Button in Pause Episode Popup" +
            "Validating Status Field is auto Populated with 'Paused'" +
            "Validate Pause Record Is Created with Following Fields  Brokerage Episode, Responsible Team,Pause Date/Time, Restart Date/Time, Status,Pause Reason,Other Pause Reason")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Case_BrokerageEpisodes_UITestMethod009()
        {
            var pauseReasonOtherid = dbHelper.brokerageEpisodePauseReason.GetByName("Other").FirstOrDefault();

            //CreateTestData(businessUnitId, teamID, currentDateTime);

            var episodeIdWithStatusNew = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, DateTime.Now.AddDays(-01), null, status, 0, 0, contactType, false, false, false, false, true, true, false);

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
                .OpenRecord(episodeIdWithStatusNew.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(pauseReasonOtherid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateStatusSelectedText("Paused")
                .NavigateToBrokeragePauseEpisodeSubPage();

            //validating pasue Record is created
            var BrokerageEpisodePausedRecords = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(1, BrokerageEpisodePausedRecords.Count);

            caseBrokerageEisodePausePeriodPage
                .WaitForCaseBrokerageEpisodePausePeriodPageToLoad()
                .OpenRecord(BrokerageEpisodePausedRecords[0].ToString());

            caseBrokerageEpisodePausePeriodsRecordPage
                .WaitForCaseBrokerageEpisodePausePeriodsRecordPageToLoad("Brokerage episode paused from");
        }

        [TestProperty("JiraIssueID", "CDV6-13313")]
        [Description("Open active social care case record with Brokerage episode Status as New " +
            "Click on Pause Episode Button and Enter Pause Date Field with Date After Request Received Date" +
            "Click Save Button in Pause Episode Popup" +
            "Validating Status Field is auto Populated with 'Paused'" +
            "Validate Target Date And Time and Tracking Status Field is Cleared Automatically" +
            "Run the Scheduled Job Record('Update Brokerage Episode Tracking Status') in Setting " +
            "Again Validate the Brokerage Episode Tracking Status and Target Date and Time Fields is Empty")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Case_BrokerageEpisodes_UITestMethod0010()
        {
            var pauseReasonOtherid = dbHelper.brokerageEpisodePauseReason.GetByName("Other").FirstOrDefault();

            var episodeIdWithStatusNew = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, DateTime.Now.AddDays(-1), null, status, 0, 0, contactType, false, false, false, false, true, true, false);

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
                .OpenRecord(episodeIdWithStatusNew.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateTrackingStatusLinkFieldText("Green_Good")
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(pauseReasonOtherid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateStatusSelectedText("Paused")
                .ValidateStatusOptionDisabled("Cancelled", false)
                .ValidateStatusOptionDisabled("New", true)
                .ValidateStatusOptionDisabled("Request Rejected", true)
                .ValidateStatusOptionDisabled("Sourcing in Progress", true)
                .ValidateStatusOptionDisabled("Sourced", true)
                .ValidateStatusOptionDisabled("Ready for Approval", true)
                .ValidateStatusOptionDisabled("Approval Rejected", true)
                .ValidateStatusOptionDisabled("Approved", true)
                .ValidateStatusOptionDisabled("Awaiting Commencement", true)
                .ValidateStatusOptionDisabled("Completed", true)
                .ValidateStatusOptionDisabled("Paused", false)
                .ValidateTargetDateTime("", "")
                .ValidateTrackingStatusLinkFieldText("");

            //validating pasue Record is created
            var BrokerageEpisodePausedRecords = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(1, BrokerageEpisodePausedRecords.Count);

            // authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate(); //execute the "Update Brokerage Episode Tracking Status" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(UpdateBrokerageEpisodeTrackingStatusScheduledJob.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(UpdateBrokerageEpisodeTrackingStatusScheduledJob);

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRefreshButton()
                .ValidateStatusSelectedText("Paused")
                .ValidateTargetDateTime("", "")
                .ValidateTrackingStatusLinkFieldText("");
        }

        [TestProperty("JiraIssueID", "CDV6-13349")]
        [Description("Open active social care case record with Brokerage episode Status as New " +
            "Click on Pause Episode Button and Enter Pause Date Field with Date After Request Received Date" +
            "Click Save Button in Pause Episode Popup" +
            "Validating Status Field is auto Populated with 'Paused'" +
            "Validate Target Date And Time and Tracking Status Field is Cleared Automatically" +
            "Click Restart Episode Button and insert Restart Date and time Before Request received Date(or)Future Date(or)Before pause Date " +
            "Validate Alert Popup is Displayed with Error Message")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Case_BrokerageEpisodes_UITestMethod0011()
        {
            var pauseReasonOtherid = dbHelper.brokerageEpisodePauseReason.GetByName("Other").FirstOrDefault();

            var episodeIdWithStatusNew = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, DateTime.Now.AddDays(-2), null, status, 0, 0, contactType, false, false, false, false, true, true, false);

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
                .OpenRecord(episodeIdWithStatusNew.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateTrackingStatusLinkFieldText("Green_Good")
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(pauseReasonOtherid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRefreshButton()
                .ValidateStatusSelectedText("Paused")
                .ValidateStatusOptionDisabled("Cancelled", false)
                .ValidateTargetDateTime("", "")
                .ValidateTrackingStatusLinkFieldText("");

            //validating pasue Record is created
            var BrokerageEpisodePausedRecords = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(1, BrokerageEpisodePausedRecords.Count);

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRestartEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertRestartDateTime(DateTime.Now.AddDays(01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueEpisodeSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Restart Date/Time cannot be in future")
                .TapCloseButton();

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertRestartDateTime(DateTime.Now.AddDays(-03).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueEpisodeSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Restart Date/Time cannot be before Request Received Date/Time")
                .TapCloseButton();

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertRestartDateTime(DateTime.Now.AddDays(-01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueEpisodeSaveButton();

            System.Threading.Thread.Sleep(3000);

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The selected Restart Date/Time cannot be before Pause Date/Time")
                .TapCloseButton();
        }

        [TestProperty("JiraIssueID", "CDV6-13350")]
        [Description("Open active social care case record with Brokerage episode Status as New " +
            "Click on Pause Episode Button and Enter Pause Date Field with Date After Request Received Date" +
            "Click Save Button in Pause Episode Popup" +
            "Validating Status Field is auto Populated with 'Paused'" +
            "Validate Target Date And Time and Tracking Status Field is Cleared Automatically" +
            "Click Restart Episode Button and insert Restart Date and time After Request received Date(or)Present Date(or)Requiest Received Date " +
            "Validate Brokerage Episode Is Restarted " +
            "Validate Target Date And Time And Tracking Status is autopopulated")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void Case_BrokerageEpisodes_UITestMethod0012()
        {
            var sourceDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-3).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            var pauseReasonOtherid = dbHelper.brokerageEpisodePauseReason.GetByName("Other").FirstOrDefault();
            var episodeId = dbHelper.brokerageEpisode.CreateBrokerageEpisode(teamID, caseID, personID, sourceOfBrokerageRequestsID2, brokerageEpisodePriorityID, sourceDate, sourceDate, 1, 0, 0, contactType, false, false, false, false, true, true, false);

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

            //pausing the Record with Present Date
            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateTrackingStatusLinkFieldText("Green_Good")
                .ValidateTargetDateTime(sourceDate.AddDays(07).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), sourceDate.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(pauseReasonOtherid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRefreshButton()
                .ValidateStatusSelectedText("Paused")
                .ValidateTargetDateTime("", "")
                .ValidateTrackingStatusLinkFieldText("");

            //validating pasue Record is created
            var BrokerageEpisodePausedRecords = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(1, BrokerageEpisodePausedRecords.Count);

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRestartEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertRestartDateTime(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueEpisodeSaveButton();

            System.Threading.Thread.Sleep(1500);

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateTargetDateTime(sourceDate.AddDays(07).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), sourceDate.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateTrackingStatusLinkFieldText("Green_Good");

            //Deleting the Record in Paused Period
            dbHelper.brokerageEpisodePausePeriod.DeleteBrokerageEpisodePausePeriod(BrokerageEpisodePausedRecords[0]);

            //Pausing the Record at the Request Received Date
            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateTrackingStatusLinkFieldText("Green_Good")
                .ValidateTargetDateTime(sourceDate.AddDays(07).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), sourceDate.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.AddDays(-02).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(pauseReasonOtherid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRefreshButton()
                .ValidateStatusSelectedText("Paused")
                .ValidateTargetDateTime("", "")
                .ValidateTrackingStatusLinkFieldText("");

            //validating pasue Record is created
            var BrokerageEpisodePausedRecords1 = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(1, BrokerageEpisodePausedRecords.Count);

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRestartEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertRestartDateTime(DateTime.Now.AddDays(-02).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueEpisodeSaveButton();

            System.Threading.Thread.Sleep(1000);

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateTargetDateTime(sourceDate.AddDays(07).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), sourceDate.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateTrackingStatusLinkFieldText("Green_Good");

            //Deleting the Paused Period Record
            dbHelper.brokerageEpisodePausePeriod.DeleteBrokerageEpisodePausePeriod(BrokerageEpisodePausedRecords1[0]);

            //Pausing the Record After the Requeist Received Date
            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateTrackingStatusLinkFieldText("Green_Good")
                .ValidateTargetDateTime(sourceDate.AddDays(07).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), sourceDate.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertPauseDateTime(DateTime.Now.AddDays(-01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueReasonLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Other")
                .TapSearchButton()
                .SelectResultElement(pauseReasonOtherid.ToString());

            caseBrokerageEpisodeRecordPage
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertOtherReason("hbfc")
                .ClickPasueEpisodeSaveButton();

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRefreshButton()
                .ValidateStatusSelectedText("Paused")
                .ValidateTargetDateTime("", "")
                .ValidateTrackingStatusLinkFieldText("");

            //validating pasue Record is created
            var BrokerageEpisodePausedRecords2 = dbHelper.brokerageEpisodePausePeriod.GetBrokerageEpisodePausePeriodByCaseID(caseID);
            Assert.AreEqual(1, BrokerageEpisodePausedRecords.Count);

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ClickRestartEpisodeButton()
                .WaitForPasueBrokerageEpisodeRecordLookupToLoad()
                .InsertRestartDateTime(DateTime.Now.AddDays(-01).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickPasueEpisodeSaveButton();

            System.Threading.Thread.Sleep(1000);

            caseBrokerageEpisodeRecordPage
                .WaitForCaseBrokerageEpisodeRecordPageToLoad("Brokerage episode for " + _personFullName + " received on ")
                .ValidateTargetDateTime(sourceDate.AddDays(07).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), sourceDate.ToString("HH:mm", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateTrackingStatusLinkFieldText("Green_Good");

            dbHelper.brokerageEpisodePausePeriod.DeleteBrokerageEpisodePausePeriod(BrokerageEpisodePausedRecords2[0]);

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