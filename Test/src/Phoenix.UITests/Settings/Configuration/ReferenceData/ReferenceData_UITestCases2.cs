using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;


namespace Phoenix.UITests.Settings.Configuration
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class ReferenceData_UITestCases2 : FunctionalTest
    {
        #region properties

        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _languageId;
        private Guid _authenticationproviderid;

        #endregion

        [TestInitialize()]
        public void TestMethod_Setup()
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

            _businessUnitId = commonMethodsDB.CreateBusinessUnit("Reference Data BU");

            #endregion

            #region Team

            _teamId = commonMethodsDB.CreateTeam("Reference Data T1", null, _businessUnitId, "907678", "ReferenceDataT1@careworkstempmail.com", "Reference Data", "020 123456");

            #endregion

            #region System User

            commonMethodsDB.CreateSystemUserRecord("ReferenceDataUser1", "ReferenceData", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-2341

        [TestProperty("JiraIssueID", "CDV6-25756")]
        [Description("Step(s) 3 to 4 from the original test method CDV6-23357")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        public void SocialWorkerChangeReasons_UITestMethod001()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Steps 3 & 4

            loginPage
                .GoToLoginPage()
                .Login("ReferenceDataUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Social Worker Change Reasons")
                .ClickReferenceDataMainHeader("LAC")
                .ClickReferenceDataElement("Social Worker Change Reasons");

            socialWorkerChangeReasonsPage
                .WaitForSocialWorkerChangeReasonsPageToLoad()
                .ClickNewRecordButton();

            var socialWorkerChangeReasonName = "SWCR_" + currentDateTimeString;

            socialWorkerChangeReasonRecordPage
                .WaitForSocialWorkerChangeReasonRecordPagePageToLoad()
                .InsertTextOnName(socialWorkerChangeReasonName)
                .InsertTextOnStartDate("01/01/2000")
                .ClickSaveAndCloseButton();

            socialWorkerChangeReasonsPage
                .WaitForSocialWorkerChangeReasonsPageToLoad()
                .ClickRefreshButton();

            var socialWorkerChangeReasons = dbHelper.socialWorkerChangeReason.GetByName(socialWorkerChangeReasonName);
            Assert.AreEqual(1, socialWorkerChangeReasons.Count);
            var newSocialWorkerChangeReasonId = socialWorkerChangeReasons[0];

            socialWorkerChangeReasonsPage
                .OpenRecord(newSocialWorkerChangeReasonId.ToString());

            socialWorkerChangeReasonRecordPage
                .WaitForSocialWorkerChangeReasonRecordPagePageToLoad()
                .ValidateNameText(socialWorkerChangeReasonName)
                .ValidateCodeText("")
                .ValidateGovCodeText("")
                .ValidateInactive_NoRadioButtonChecked()
                .ValidateStartDateText("01/01/2000")
                .ValidateEndDateText("")
                .ValidateValidforexport_NoRadioButtonChecked()
                .ValidateResponsibleTeamLinkText("Reference Data T1");

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
