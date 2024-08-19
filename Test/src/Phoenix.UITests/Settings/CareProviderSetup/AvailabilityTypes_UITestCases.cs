using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.CareProviderSetup
{
    /// <summary>
    /// This class contains Automated UI test scripts for Availability Types record creation
    /// </summary>
    [TestClass]
    public class AvailabilityTypes_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _systemUserId;
        private string _systemUserName;
        private Guid _standardAvailabilityTypeId;
        private Guid _regularAvailabilityTypeId;
        private Guid _overtimeAvailabilityTypeId;
        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region System User

                _systemUserName = "AvailabilityTypesUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "AvailabilityTypes", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _systemUserId);

                #endregion

                #region Availability Types                               

                var availabilityTypeIDExists = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").Any();
                if (!availabilityTypeIDExists)
                {
                    _standardAvailabilityTypeId = dbHelper.availabilityTypes.CreateAvailabilityType("Salaried/Contracted", 1, false, _careProviders_TeamId, 1, 1, true);

                }
                if (_standardAvailabilityTypeId == Guid.Empty)
                {
                    _standardAvailabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted")[0];
                }

                availabilityTypeIDExists = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Regular").Any();
                if (!availabilityTypeIDExists)
                {
                    _regularAvailabilityTypeId = dbHelper.availabilityTypes.CreateAvailabilityType("Regular", 5, false, _careProviders_TeamId, 1, 1, true);

                }
                if (_regularAvailabilityTypeId == Guid.Empty)
                {
                    _regularAvailabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Regular")[0];
                }

                availabilityTypeIDExists = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Hourly/Overtime").Any();
                if (!availabilityTypeIDExists)
                {
                    _overtimeAvailabilityTypeId = dbHelper.availabilityTypes.CreateAvailabilityType("Hourly/Overtime", 52, false, _careProviders_TeamId, 1, 1, true);

                }
                if (_overtimeAvailabilityTypeId == Guid.Empty)
                {
                    _overtimeAvailabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Hourly/Overtime")[0];
                }



                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-23657

        [TestProperty("JiraIssueID", "ACC-3579")]
        [Description("Steps 1 to 5 from the original jira test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Availability")]
        [TestProperty("Screen1", "Availability Types")]
        public void AvailabilityTypes_UITestMethod001()
        {

            #region Step 1 - Step 2

            Guid salariedOrContractedAvailabilityType = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted")[0];
            int salariedOrContractedAvailabilityPrecedenceValue = (int)dbHelper.availabilityTypes.GetByAvailabilityTypeID(salariedOrContractedAvailabilityType, "precedence")["precedence"];


            loginPage
              .GoToLoginPage()
              .Login(_systemUserName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAvailabilityTypesPage();

            availabilityTypesPage
                .WaitForAvailabilityTypesPageToLoad()
                .ValidateAvailabilityTypeRecordIsPresent(_standardAvailabilityTypeId.ToString(), true)
                .ValidateAvailabilityTypeRecordIsPresent(_regularAvailabilityTypeId.ToString(), true)
                .ValidateAvailabilityTypeRecordIsPresent(_overtimeAvailabilityTypeId.ToString(), true);

            #endregion

            #region Step 3

            availabilityTypesPage
                .ClickNewRecordButton();

            availabilityTypeRecordPage
                .WaitForAvailabilityTypeRecordPageToLoad()
                .ClickSaveButton()
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateAvailabilityTypeNameFieldErrorLabelVisibility(true)
                .ValidateAvailabilityTypeNameFieldErrorLabelText("Please fill out this field.")
                .ValidatePrecedenceFieldErrorLabelVisibility(true)
                .ValidatePrecedenceFieldErrorLabelText("Please fill out this field.");

            #endregion

            #region Step 4

            // we cannot run this test because the precedence value has a max lenght of 2 digits. so if we have an existing record with precedence id of 99 then this test will allways fail

            //availabilityTypeRecordPage
            //    .ClickBackButton();

            //availabilityTypesPage
            //    .WaitForAvailabilityTypesPageToLoad()
            //    .ClickNewRecordButton();

            //availabilityTypeRecordPage
            //    .WaitForAvailabilityTypeRecordPageToLoad()
            //    .InsertName("Type" + precedenceValue)
            //    .InsertPrecedence(precedenceValue)
            //    .SelectValueForDiaryBookings("Valid")
            //    .SelectValueForScheduleBookings("Valid")
            //    .ClickCountTowardsHoursOrDaysWorkedNoRadioButton()
            //    .ClickSaveButton()
            //    .WaitForAvailabilityTypeRecordPageToLoad()
            //    .WaitForRecordToBeSaved();

            //availabilityTypeRecordPage
            //    .WaitForAvailabilityTypeRecordPageToLoad()
            //    .ValidateAvailabilityNameFieldValue("Type" + precedenceValue)
            //    .ValidatePrecedenceFieldValue(precedenceValue)
            //    .ValidateResponsibleTeamLinkText("CareProviders")
            //    .ValidateIsThisAvailabilityTypeAFixedWorkingPatternNoRadionButtonChecked()
            //    .ValidateValueForDiaryBookingsSelectedText("Valid")
            //    .ValidateValueForScheduleBookingsSelectedText("Valid")
            //    .ValidateCountsTowardsHoursOrDaysWorkedNoRadionButtonChecked();

            #endregion

            #region Step 5

            availabilityTypeRecordPage
                .ClickBackButton();

            availabilityTypesPage
                .WaitForAvailabilityTypesPageToLoad()
                .ClickNewRecordButton();

            availabilityTypeRecordPage
                .WaitForAvailabilityTypeRecordPageToLoad()
                .InsertName("Salaried/Contracted")
                .InsertPrecedence(salariedOrContractedAvailabilityPrecedenceValue.ToString())
                .SelectValueForDiaryBookings("Valid")
                .SelectValueForScheduleBookings("Valid")
                .ClickCountTowardsHoursOrDaysWorkedYesRadioButton()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Name must be unique.\r\n\r\nThe value in Precedence clashes with the 'Salaried/Contracted' Availability Type. Please enter a unique value.");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3580")]
        [Description("Steps 6 to 10 from the original jira test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Availability")]
        [TestProperty("Screen1", "Availability Types")]
        public void AvailabilityTypes_UITestMethod002()
        {
            #region Step 6
            var  precedenceValue1 = dbHelper.availabilityTypes.GetAvailabilityTypeWithHighestPrecedence();
            int IntegerPrecedenceValue = (int) precedenceValue1["precedence"];
            IntegerPrecedenceValue = IntegerPrecedenceValue + 1;

            loginPage
              .GoToLoginPage()
              .Login(_systemUserName, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAvailabilityTypesPage();

            availabilityTypesPage
                .WaitForAvailabilityTypesPageToLoad()
                .ClickNewRecordButton();

            availabilityTypeRecordPage
                .WaitForAvailabilityTypeRecordPageToLoad()
                .InsertName("Work Schedule_" + IntegerPrecedenceValue)
                .InsertPrecedence(IntegerPrecedenceValue.ToString())
                .SelectValueForDiaryBookings("Invalid")
                .SelectValueForScheduleBookings("Invalid")
                .ClickCountTowardsHoursOrDaysWorkedNoRadioButton()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            availabilityTypeRecordPage
                .WaitForAvailabilityTypeRecordPageToLoad()
                .ValidateAvailabilityNameFieldValue("Work Schedule_" + IntegerPrecedenceValue)
                .ValidatePrecedenceFieldValue(IntegerPrecedenceValue.ToString())
                .ValidateResponsibleTeamLinkText("CareProviders")
                .ValidateIsThisAvailabilityTypeAFixedWorkingPatternNoRadionButtonChecked()
                .ValidateValueForDiaryBookingsSelectedText("Invalid")
                .ValidateValueForScheduleBookingsSelectedText("Invalid")
                .ValidateCountsTowardsHoursOrDaysWorkedNoRadionButtonChecked();

            #endregion

            #region Step 7
            availabilityTypeRecordPage
                .ClickBackButton();

            availabilityTypesPage
                .WaitForAvailabilityTypesPageToLoad()
                .ClickNewRecordButton();

            availabilityTypeRecordPage
                .WaitForAvailabilityTypeRecordPageToLoad()
                .InsertName("Work SCHEDULE_" + IntegerPrecedenceValue.ToString());

            var precedenceValue2 = dbHelper.availabilityTypes.GetAvailabilityTypeWithHighestPrecedence();
            int IntegerPrecedenceValue2 = (int)precedenceValue2["precedence"];
            IntegerPrecedenceValue2 = IntegerPrecedenceValue2 + 1;

            availabilityTypeRecordPage
                .InsertPrecedence(IntegerPrecedenceValue2.ToString())
                .SelectValueForDiaryBookings("Invalid")
                .SelectValueForScheduleBookings("Invalid")
                .ClickCountTowardsHoursOrDaysWorkedNoRadioButton()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Name must be unique.")
                .TapCloseButton();

            #endregion

            #region Step 8
            availabilityTypeRecordPage
                .WaitForAvailabilityTypeRecordPageToLoad()
                .InsertPrecedence("0")
                .ClickSaveButton()
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidatePrecedenceFieldErrorLabelText("Please enter a value between 1 and 99.")
                .ValidatePrecedenceFieldMaximumLengthValue("2");

            #endregion

            #region Step 9
            availabilityTypeRecordPage
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            var existingAvailabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Work Schedule_" + IntegerPrecedenceValue.ToString())[0];

            availabilityTypesPage
                .WaitForAvailabilityTypesPageToLoad()
                .InsertQuickSearchText("Work Schedule_" + IntegerPrecedenceValue.ToString())
                .ClickQuickSearchButton()
                .OpenRecord(existingAvailabilityTypeId.ToString());

            var precedence = (int)(dbHelper.availabilityTypes.GetByAvailabilityTypeID(_regularAvailabilityTypeId, "precedence")["precedence"]);

            availabilityTypeRecordPage
                .WaitForAvailabilityTypeRecordPageToLoad()
                .InsertName("Regular")
                .InsertPrecedence(precedence.ToString())
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Name must be unique.\r\n\r\nThe value in Precedence clashes with the 'Regular' Availability Type. Please enter a unique value.")
                .TapCloseButton();

            #endregion

            #region Step 10
            availabilityTypeRecordPage
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            availabilityTypesPage
                .WaitForAvailabilityTypesPageToLoad()
                .ClickNewRecordButton();

            availabilityTypeRecordPage
                .WaitForAvailabilityTypeRecordPageToLoad()
                .ValidateValidForExportNoRadioButtonChecked();

            #endregion
        }

        #endregion

    }
}
