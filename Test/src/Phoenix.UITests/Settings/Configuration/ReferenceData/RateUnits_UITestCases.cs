using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Configuration
{
    [TestClass]
    public class RateUnits_UITestCases : FunctionalTest
    {
        #region properties

        private Guid _businessUnitId;
        private Guid _teamId;
        private string _teamName;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private string _systemUserName;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

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

            _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

            #endregion

            #region Team

            _teamName = "CareDirector QA";
            _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

            #endregion

            #region System User

            _systemUserName = "RateUnitsUser1";
            commonMethodsDB.CreateSystemUserRecord(_systemUserName, "RateUnits", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-2788

        [TestProperty("JiraIssueID", "ACC-2907")]
        [Description("Step(s) 1 to 10 from the original test method ACC-905")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Rate Units")]
        public void RateUnits_UITestMethod01()
        {
            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Rate Units")
                .ClickReferenceDataMainHeader("Person Contracts")
                .ClickReferenceDataElement("Rate Units");

            rateUnitsPage
                .WaitForRateUnitsPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderrateunit")
                .ClickOnExpandIcon();

            rateUnitRecordPage
                .WaitForRateUnitRecordPageToLoad()
                .ValidateAllFieldsVisible()
                .ValidateNameMandatoryFieldVisibility(true)
                .ValidateCodeMandatoryFieldVisibility(true)
                .ValidateResponsibleTeamMandatoryFieldVisibility(true)
                .ValidateStartDateMandatoryFieldVisibility(true)
                .ValidateTimeAndDaysMandatoryFieldVisibility(true)
                .ValidateOneOffMandatoryFieldVisibility(true);

            #endregion

            #region Step 2 to 5

            rateUnitRecordPage
                .ValidateNameFieldMaximumTextLimit("100")
                .ValidateGovCodeFieldMaximumTextLimit("8")
                .ValidateTimeAndDays_OptionIsCheckedOrNot(false)
                .ValidateOneOff_OptionIsCheckedOrNot(false)

                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateStartDateText(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))

                .ValidateBandedRatesIsVisible(false)
                .ValidateDecimalPlacesIsVisible(false)
                .ValidateMinimumAllowedFieldIsVisible(false)
                .ValidateMaximumAllowedFieldIsVisible(false)
                .ValidateUnitDivisorFieldIsVisible(false);

            rateUnitRecordPage
                .ClickTimeAndDays_Option(true)

                .ValidateBandedRatesIsVisible(true)
                .ValidateBandedRatesMandatoryFieldVisibility(true)
                .ValidateBandedRates_OptionIsCheckedOrNot(false)

                .ValidateDecimalPlacesIsVisible(true)
                .ValidateDecimalPlacesMandatoryFieldVisibility(true)
                .ValidateDecimalPlaces_OptionIsCheckedOrNot(false)

                .ValidateMinimumAllowedFieldIsVisible(true)
                .ValidateMinimumAllowedMandatoryFieldVisibility(true)

                .ValidateMaximumAllowedFieldIsVisible(true)
                .ValidateMaximumAllowedMandatoryFieldVisibility(true)

                .ValidateUnitDivisorFieldIsVisible(true)
                .ValidateUnitDivisorMandatoryFieldVisibility(true)

                .ValidatePrecisionLimitOfUnitDivisorField("3")
                .ClickDecimalPlaces_Option(true)
                .ValidatePrecisionLimitOfMinimumAllowedField("2")
                .ValidatePrecisionLimitOfMaximumAllowedField("2");

            #endregion

            #region Step 6

            var RateUnitName = "Rate_Unit_" + _currentDateString;
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);

            rateUnitRecordPage
                .InsertName(RateUnitName)
                .InsertCode(code.ToString())
                .InsertMinimumAllowedText("2")
                .InsertMaximumAllowedText("1")
                .InsertUnitDivisorText("1")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Minimum Allowed must be less than or equal to Maximum Allowed.")
                .TapCloseButton();

            #endregion

            #region Step 7 to 9

            rateUnitRecordPage
                .ValidateMinimumAllowedText("2")
                .ValidateMaximumAllowedText("1");

            rateUnitRecordPage
                .ClickDecimalPlaces_Option(false)
                .ValidateMinimumAllowedText("")
                .ValidateMaximumAllowedText("")
                .InsertMinimumAllowedText("-0.1")
                .ValidateMinimumAllowedFieldErrorLabelText("Please enter a value between 0 and 7.922816251426434e+28.")

                .InsertMaximumAllowedText("-0.1")
                .ValidateMaximumAllowedFieldErrorLabelText("Please enter a value between 0 and 7.922816251426434e+28.")

                .InsertUnitDivisorText("10")
                .ValidateUnitDivisorFieldErrorLabelText("Please enter a value between 0 and 9.99.");

            #endregion

            #region Step 10

            rateUnitRecordPage
                .InsertMinimumAllowedText("2")
                .InsertMaximumAllowedText("4")
                .InsertUnitDivisorText("3")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()

                .ValidateRateUnitRecordPageTitle(RateUnitName)
                .ValidateResponsibleTeamLookupButtondDisabled(true)
                .ValidateNameFieldIsDisabledOrNot(false)
                .ValidateCodeFieldIsDisabledOrNot(false)
                .ValidateGovCodeFieldIsDisabledOrNot(false)
                .ValidateStartDateFieldIsDisabledOrNot(false)
                .ValidateEndDateFieldIsDisabledOrNot(false)
                .ValidateInactiveOptionsIsDisabledOrNot(false)
                .ValidateValidForExportOptionsIsDisabledOrNot(false)

                .ValidateTimeAndDaysOptionsIsDisabledOrNot(false)
                .ValidateOneOffOptionsIsDisabledOrNot(false)
                .ValidateBandedRatesOptionsIsDisabledOrNot(false)
                .ValidateDecimalPlacesOptionsIsDisabledOrNot(false)
                .ValidateMinimumAllowedFieldIsDisabledOrNot(false)
                .ValidateMaximumAllowedFieldIsDisabledOrNot(false)
                .ValidateUnitDivisorFieldIsDisabledOrNot(false)
                .ValidateNoteTextFieldIsDisabledOrNot(false);

            Guid careProviderRateUnitId = dbHelper.careProviderRateUnit.GetByName(RateUnitName).FirstOrDefault();
            dbHelper.careProviderRateUnit.DeleteCareProviderRateUnitRecord(careProviderRateUnitId);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2908")]
        [Description("Step(s) 11 to 15 to from the original test method ACC-905")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Rate Units")]
        public void RateUnits_UITestMethod02()
        {
            var RateUnitName1 = "Rate_Unit_1_" + _currentDateString;
            var RateUnitName2 = "Rate_Unit_2_" + _currentDateString;
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, RateUnitName1, new DateTime(2020, 1, 1), code);

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Rate Units")
                .ClickReferenceDataMainHeader("Person Contracts")
                .ClickReferenceDataElement("Rate Units");

            rateUnitsPage
                .WaitForRateUnitsPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderrateunit")
                .ClickOnExpandIcon();

            rateUnitRecordPage
                .WaitForRateUnitRecordPageToLoad()
                .InsertName(RateUnitName2)
                .InsertCode(code.ToString())
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Rate Unit with same combination already exist: Inactive = No AND Code = " + code.ToString() + ".")
                .TapCloseButton();

            #endregion

            #region Step 12

            rateUnitRecordPage
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            rateUnitsPage
                .WaitForRateUnitsPageToLoad()
                .ValidateHeaderCellText(2, "Name")
                .ValidateGridHeaderCellSortAscendingOrder(2, "Name")
                .ValidateHeaderCellText(3, "Code")
                .ValidateHeaderCellText(4, "One-Off?")
                .ValidateHeaderCellText(5, "Time and Days?")
                .ValidateHeaderCellText(6, "Banded Rates?")
                .ValidateHeaderCellText(7, "Single Rate?")
                .ValidateHeaderCellText(8, "Decimal Places?")
                .ValidateHeaderCellText(9, "Minimum Allowed")
                .ValidateHeaderCellText(10, "Maximum Allowed")
                .ValidateHeaderCellText(11, "Unit Divisor")
                .ValidateHeaderCellText(16, "Modified On")
                .ValidateHeaderCellText(17, "Modified By");

            #endregion

            #region Step 13

            rateUnitsPage
                .InsertSearchQuery(RateUnitName1)
                .TapSearchButton()
                .ValidateRecordPresentOrNot(careProviderRateUnitId.ToString(), true)

                .InsertSearchQuery(code.ToString())
                .TapSearchButton()
                .ValidateRecordPresentOrNot(careProviderRateUnitId.ToString(), true);

            #endregion

            #region Step 15

            rateUnitsPage
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderrateunit")
                .ClickOnExpandIcon();

            rateUnitRecordPage
                .WaitForRateUnitRecordPageToLoad()

                .ClickTimeAndDays_Option(true)
                .ClickBandedRates_Option(true)
                .ValidateOneOffOptionsIsDisabledOrNot(true)
                .ValidateOneOff_OptionIsCheckedOrNot(false);

            rateUnitRecordPage
                .ClickTimeAndDays_Option(false)
                .ValidateOneOff_OptionIsCheckedOrNot(false)
                .ValidateOneOffOptionsIsDisabledOrNot(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2925")]
        [Description("Step(s) 16 to 18 to from the original test method ACC-905")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Bank Holiday Charging Calendars")]
        public void RateUnits_UITestMethod03()
        {
            var BankHolidayChargingCalendarId = commonMethodsDB.CreateCPBankHolidayChargingCalendar(_teamId, "ACC_2909", "1234");

            foreach (var BankHoildayDates in dbHelper.cpBankHolidayDate.GetByCPBankHolidayChargingCalendarId(BankHolidayChargingCalendarId))
                dbHelper.cpBankHolidayDate.DeleteCPBankHolidayDateRecord(BankHoildayDates);

            #region Bank Holiday 

            var BankHolidayId = commonMethodsDB.CreateBankHoliday("Republic day 2023", new DateTime(2021, 1, 26), "Republic day 2023");
            var BankHolidayTypeId = dbHelper.careProviderBankHolidayType.GetByName("Manual").FirstOrDefault();

            dbHelper.cpBankHolidayDate.CreateCPBankHolidayDate(_teamId, BankHolidayChargingCalendarId, "ACC_2909", BankHolidayId, BankHolidayTypeId);

            #endregion

            #region Step 16 to 18

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Bank Holiday Charging Calendars")
                .ClickReferenceDataMainHeader("Care Provider Invoicing")
                .ClickReferenceDataElement("Bank Holiday Charging Calendars");

            bankHolidayChargingCalendarsPage
                .WaitForBankHolidayChargingCalendarsPageToLoad()
                .OpenRecord(BankHolidayChargingCalendarId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("cpbankholidaychargingcalendar")
                .ClickOnExpandIcon();

            bankHolidayChargingCalendarRecordPage
                .WaitForBankHolidayChargingCalendarRecordPageToLoad()
                .ValidateBankHolidayChargingCalendarRecordPageTitle("ACC_2909")
                .ClickBankHolidayDatesTab();

            bankHolidayDatesPage
                .WaitForBankHolidayDatesPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("cpbankholidaydate")
                .ClickOnExpandIcon();

            bankHolidayDateRecordPage
                .WaitForBankHolidayDateRecordPageToLoad()
                .ValidateAllFieldsVisible()
                .ValidateBankHolidayChargingCalendarMandatoryFieldVisibility(true)
                .ValidatePublicHolidayMandatoryFieldVisibility(true)
                .ValidateBankHolidayTypeMandatoryFieldVisibility(true);

            bankHolidayDateRecordPage
                .ClickPublicHolidayLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Republic day 2023")
                .TapSearchButton()
                .SelectResultElement(BankHolidayId);

            bankHolidayDateRecordPage
                .ClickBankHolidayTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Manual")
                .TapSearchButton()
                .SelectResultElement(BankHolidayTypeId);

            bankHolidayDateRecordPage
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("'Holiday' value has already been linked to this Bank Holiday Charging Calendar.")
                .TapCloseButton();

            bankHolidayDateRecordPage
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            bankHolidayDatesPage
                .WaitForBankHolidayDatesPageToLoad()
                .ValidateHeaderCellText(2, "Bank Holiday Charging Calendar")

                .ValidateHeaderCellText(3, "Public Holiday")
                .ValidateHeaderCellText(4, "Date [Public Holiday]")
                .ValidateHeaderCellText(5, "Bank Holiday Type")
                .ValidateHeaderCellText(6, "Weighting [Bank Holiday Type]")
                .ValidateHeaderCellText(7, "Used In Finance?");

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
