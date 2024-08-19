using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Settings.Configuration.HealthSetup
{
    [TestClass]
    public class RTTPathwaysSetup_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private string _systemUsername;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("RTT Pathways Setup BU");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("RTT Pathways Setup T1", null, _businessUnitId, "907678", "RTTPathwaysSetupT1@careworkstempmail.com", "RTTPathwaysSetup T3", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User DiaryViewSetupUser1

                _systemUsername = "RTTPathwaysSetupUser1";
                commonMethodsDB.CreateSystemUserRecord(_systemUsername, "RTTPathwaysSetup", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-2550

        [TestProperty("JiraIssueID", "CDV6-21502")]
        [Description("Step(s) 1 to 13 from the original jira test CDV6-21502")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod()]
        public void RTTPathwaysSetup_UITestMethod001()
        {
            string rttPathwaySetupName = "Pathway_" + _currentDateString;
            ;
            #region Step 1 to 3

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToHealthSetUp();

            healthSetupPage
                .WaitForHealthSetupPageToLoad()
                .ClickRTTPathwaysSetupButton();

            rttPathwaysSetupPage
                .WaitForRTTPathwaysSetupPageToLoad()
                .ClickNewRecordButton();

            rttPathwaysSetupRecordPage
                .WaitForRTTPathwaysSetupRecordPageToLoad()
                .ValidateAllFieldsVisible()
                .ValidateNameMandatoryFieldVisibility(true)
                .ValidateNameFieldMaximumLimitText("150")

                .ValidateFirstAppointmentNoLaterThanFieldIsNumeric()
                .ValidateFirstAppointmentNoLaterThanMandatoryFieldVisibility(true)

                .ValidateWarnAfterFieldIsNumeric()
                .ValidateWarnAfterMandatoryFieldVisibility(true)

                .ValidateBreachOccursAfterFieldIsNumeric()
                .ValidateBreachOccursAfterMandatoryFieldVisibility(true)

                .ValidateCodeFieldIsNumeric()
                .ValidateCodeMandatoryFieldVisibility(false)

                .ValidateGovCodeFieldIsNumeric()
                .ValidateGovCodeMandatoryFieldVisibility(false)

                .ValidateStartDateMandatoryFieldVisibility(true)
                .ValidateStartDateDatePickerIsVisibile()

                .ValidateEndDateMandatoryFieldVisibility(false)
                .ValidateEndDateDatePickerIsVisibile();

            #endregion

            #region Step 4

            rttPathwaysSetupRecordPage
                .InsertCode("Text")
                .InsertGovCode("Text")
                .InsertFirstAppointmentNoLaterThan("Text")
                .InsertWarnAfter("Text")
                .InsertBreachOccursAfter("Text")
                .InsertName("")

                .ValidateCodeFieldErrorLabelText("Please enter a value between -2147483648 and 2147483647.")
                .ValidateGovCodeFieldErrorLabelText("Please enter a value between -2147483648 and 2147483647.")

                .ValidateFirstAppointmentNoLaterThanFieldErrorLabelText("Please enter a value between 0 and 2147483647.")
                .ValidateWarnAfterFieldErrorLabelText("Please enter a value between 0 and 2147483647.")
                .ValidateBreachOccursAfterFieldErrorLabelText("Please enter a value between 0 and 2147483647.");

            #endregion

            #region Step 5

            rttPathwaysSetupRecordPage
                .InsertName(rttPathwaySetupName)
                .InsertCode("12")
                .InsertGovCode("12")
                .InsertFirstAppointmentNoLaterThan("5")
                .InsertWarnAfter("5")
                .InsertBreachOccursAfter("5")
                .InsertStartDate(DateTime.Now.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(DateTime.Now.Date.AddDays(-4).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Start Date cannot be after End Date.")
                .TapCloseButton();

            #endregion

            #region Step 6

            rttPathwaysSetupRecordPage
                .InsertEndDate("")
                .InsertFirstAppointmentNoLaterThan("6")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("First Appointment No Later Than cannot be greater than Breach Occurs After.")
                .TapCloseButton();

            #endregion

            #region Step 7

            rttPathwaysSetupRecordPage
                .InsertFirstAppointmentNoLaterThan("4")
                .InsertWarnAfter("6")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Warn After cannot be greater than Breach Occurs After.")
                .TapCloseButton();

            #endregion

            #region Step 8

            rttPathwaysSetupRecordPage
                .InsertWarnAfter("5")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateRTTPathwaySetupRecordTitle(rttPathwaySetupName);

            #endregion

            #region Step 9

            rttPathwaysSetupRecordPage
                .ClickBackButton();

            System.Threading.Thread.Sleep(1000);

            rttPathwaysSetupPage
                .WaitForRTTPathwaysSetupPageToLoad()
                .ValidateHeaderCellText(2, "Name")
                .ValidateHeaderCellText(3, "Code")
                .ValidateHeaderCellText(4, "Gov Code")
                .ValidateHeaderCellText(5, "First Appointment No Later Than")
                .ValidateHeaderCellText(6, "Warn After")
                .ValidateHeaderCellText(7, "Breach Occurs After")
                .ValidateHeaderCellText(8, "Start Date")
                .ValidateHeaderCellText(9, "End Date");

            #endregion

            #region Step 10 to 13

            rttPathwaysSetupPage
                .InsertQuickSearchText(rttPathwaySetupName)
                .ClickQuickSearchButton();

            Guid _rttPathwaySetupId = dbHelper.rttPathwaySetup.GetByName(rttPathwaySetupName)[0];
            var _rttPathwayActiveRecordId = commonMethodsDB.CreateRTTPathwaySetup(_teamId, "Pathway_Active_Record", new DateTime(2020, 1, 1), 5, 5, 5);

            rttPathwaysSetupPage
                .ValidateRecordCellText(_rttPathwaySetupId.ToString(), 2, rttPathwaySetupName)
                .OpenRecord(_rttPathwaySetupId.ToString());

            rttPathwaysSetupRecordPage
                .WaitForRTTPathwaysSetupRecordPageToLoad()
                .InsertEndDate(DateTime.Now.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            rttPathwaysSetupPage
                .WaitForRTTPathwaysSetupPageToLoad()
                .SelectViewByText("Inactive Records")
                .ValidateRecordPresentOrNot(_rttPathwaySetupId.ToString(), true)
                .ValidateRecordPresentOrNot(_rttPathwayActiveRecordId.ToString(), false)

                .SelectViewByText("Active Records")
                .ValidateRecordPresentOrNot(_rttPathwaySetupId.ToString(), false)
                .ValidateRecordPresentOrNot(_rttPathwayActiveRecordId.ToString(), true);

            dbHelper.rttPathwaySetup.DeleteRTTPathwaySetupRecord(_rttPathwaySetupId);

            #endregion

        }

        #endregion

    }
}

