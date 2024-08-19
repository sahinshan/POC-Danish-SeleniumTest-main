using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;


namespace Phoenix.UITests.Settings.Configuration
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class StaffRecruitmentItems_UITestCases : FunctionalTest
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

            _businessUnitId = commonMethodsDB.CreateBusinessUnit("Staff Recruitment Item BU");

            #endregion

            #region Team

            _teamId = commonMethodsDB.CreateTeam("Staff Recruitment Item Team", null, _businessUnitId, "907678", "StaffRecruitmentItemTeam@careworkstempmail.com", "Staff Recruitment Item Team", "020 123456");

            #endregion

            #region System User StaffRecruitmentItemUser1

            commonMethodsDB.CreateSystemUserRecord("StaffRecruitmentItemUser1", "StaffRecruitmentItem", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-23595

        [TestProperty("JiraIssueID", "ACC-3371")]
        [Description("Steps 1 to 3 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Staff Recruitment Items")]
        public void StaffRecruitmentItems_UITestMethod001()
        {
            var recordName = "CDV6_23595_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Steps 1 to 3


            loginPage
                .GoToLoginPage()
                .Login("StaffRecruitmentItemUser1", "Passw0rd_!");


            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Staff Recruitment Items")
                .ClickReferenceDataMainHeader("Care Provider Staff Recruitment")
                .ClickReferenceDataElement("Staff Recruitment Items");

            staffRecruitmentItemsPage
                .WaitForStaffRecruitmentItemsPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("staffrecruitmentitem")
                .ClickOnExpandIcon();

            staffRecruitmentItemRecordPage
                .WaitForStaffRecruitmentItemRecordPageToLoad()
                .InsertName(recordName)
                .SelectComplianceRecurrence("Weekly")
                .InsertCode("1")
                .InsertGovCode("1")
                .InsertStartDate("01/01/2023")
                .ClickSaveAndCloseButton();

            staffRecruitmentItemsPage
                .WaitForStaffRecruitmentItemsPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var records = dbHelper.staffRecruitmentItem.GetByName(recordName);
            Assert.AreEqual(1, records.Count);
            var newRecordId = records[0];

            staffRecruitmentItemsPage
                .InsertSearchQuery(recordName)
                .TapSearchButton()
                .OpenRecord(newRecordId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("staffrecruitmentitem")
                .ClickOnExpandIcon();

            staffRecruitmentItemRecordPage
                .WaitForStaffRecruitmentItemRecordPageToLoad()
                .ValidateNameFieldValue(recordName)
                .ValidateComplianceRecurrenceFieldValue("Weekly")
                .ValidateGovCodeFieldValue("1")
                .ValidateCodeFieldValue("1")
                .ValidateInactiveYesOptionChecked(false)
                .ValidateStartDateFieldValue("01/01/2023")
                .ValidateValidForExportYesOptionChecked(false);


            #endregion

        }

        #endregion



    }
}
