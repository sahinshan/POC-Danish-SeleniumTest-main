using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Providers.QualityAssurance
{
    [TestClass]
    public class ComplaintsFeedback_UITestCases : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("ComplaintsAndFeedback BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("ComplaintsAndFeedback T1", null, _businessUnitId, "907678", "ComplaintsAndFeedbackT1@careworkstempmail.com", "ComplaintsAndFeedback T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User ComplaintsAndFeedbackUser1

                _systemUsername = "ComplaintsAndFeedbackUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ComplaintsAndFeedback", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-673

        [TestProperty("JiraIssueID", "CDV6-2538")]
        [Description("To verify user able to create the Complaints & Feedback record for the provider record.")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ComplaintsFeedback_UITestMethod001()
        {
            #region Person

            var _firstName = "Marya";
            var _lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Provider

            var providerName = "Prvd_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var providerid = dbHelper.provider.CreateProvider(providerName, _teamId, 7); //creating a "Carer" provider

            #endregion

            #region Provider Complaint FeedBack Type

            var providerComplaintFeedBackTypeId = commonMethodsDB.CreateProviderComplaintFeedBackType(_teamId, "Default FeedBack Type", new DateTime(2023, 1, 1));

            #endregion

            #region Provider Complaint Stage

            var providerComplaintStageId = commonMethodsDB.CreateProviderComplaintStage(_teamId, "Default Stage", new DateTime(2023, 1, 1));

            #endregion

            #region Provider Complaint Outcome

            var providerComplaintOutcomeId = commonMethodsDB.CreateProviderComplaintOutcome(_teamId, "Default Outcome", new DateTime(2023, 1, 1));

            #endregion

            #region Provider Complaint Nature

            var providerComplaintNatureId = commonMethodsDB.CreateProviderComplaintNature(_teamId, "Default Complaint Nature", new DateTime(2023, 1, 1), providerComplaintFeedBackTypeId);

            #endregion




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
                .NavigateToComplaintsAndFeedbackPage();

            complaintsAndFeedbackPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            complaintAndFeedbackRecordPage
                .WaitForPageToLoad()
                .InsertTextOnComplaintFeedbackDate("01/03/2023")
                .ClickMadeByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").TypeSearchQuery(_personNumber.ToString()).TapSearchButton().SelectResultElement(_personID);

            complaintAndFeedbackRecordPage
                .WaitForPageToLoad()
                .ClickProviderComplaintFeedbackTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default FeedBack Type").TapSearchButton().SelectResultElement(providerComplaintFeedBackTypeId);

            complaintAndFeedbackRecordPage
                .WaitForPageToLoad()
                .ClickStageLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default Stage").TapSearchButton().SelectResultElement(providerComplaintStageId);

            complaintAndFeedbackRecordPage
                .WaitForPageToLoad()
                .ClickComplaintOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default Outcome").TapSearchButton().SelectResultElement(providerComplaintOutcomeId);

            complaintAndFeedbackRecordPage
                .WaitForPageToLoad()
                .InsertTextOnComplaintFeedbackDetails("Details ...")
                .InsertTextOnFreeTextMadeBy("Free Text ...")
                .ClickNatureLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default Complaint Nature").TapSearchButton().SelectResultElement(providerComplaintNatureId);

            complaintAndFeedbackRecordPage
                .WaitForPageToLoad()
                .InsertTextOnResolutionDueDate("02/03/2023")
                .InsertTextOnOutcomeDate("03/03/2023")
                .InsertTextOnInvestigationDetails("investigation details ...")
                .ClickSaveAndCloseButton();

            complaintsAndFeedbackPage
                .WaitForPageToLoad();

            var records = dbHelper.providerComplaintFeedback.GetByProviderId(providerid);
            Assert.AreEqual(1, records.Count);
            var recordID = records[0];

            complaintsAndFeedbackPage
                .OpenRecord(recordID);

            complaintAndFeedbackRecordPage
                .WaitForPageToLoad()
                .ValidateComplaintFeedbackDateText("01/03/2023")
                .ValidateMadeByLinkText(_personFullName)
                .ValidateProviderComplaintFeedbackTypeLinkText("Default FeedBack Type")
                .ValidateStageLinkText("Default Stage")
                .ValidateComplaintOutcomeLinkText("Default Outcome")
                .ValidateComplaintFeedbackDetailsText("Details ...")
                .ValidateResponsibleUserLinkText("ComplaintsAndFeedback User1")
                .ValidateProviderLinkText(providerName)
                .ValidateFreeTextMadeByText("Free Text ...")
                .ValidateNatureLinkText("Default Complaint Nature")
                .ValidateResolutionDueDateText("02/03/2023")
                .ValidateOutcomeDateText("03/03/2023")
                .ValidateInvestigationDetailsText("investigation details ...")
                .ValidateResponsibleTeamLinkText("ComplaintsAndFeedback T1");
        }

        #endregion

    }
}

