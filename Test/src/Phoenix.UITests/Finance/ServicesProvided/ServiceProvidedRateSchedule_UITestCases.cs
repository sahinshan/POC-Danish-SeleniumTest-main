using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace Phoenix.UITests.Finance.ServicesProvided
{
    [TestClass]
    public class ServiceProvidedRateSchedule_UITestCases : FunctionalTest
    {
        private string environmentName;
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private String _systemUsername;
        private Guid _ethnicityId;
        private Guid _personID;
        private string _personFirstname;
        private string currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid serviceelement1id;
        private Guid serviceelement2id;
        private Guid rateunitid;
        private Guid per1HourWhole_rateunitid;
        private Guid oneOff_rateunitid;
        private Guid providerid;
        private Guid serviceProvidedId;

        [TestInitialize()]
        public void UITests_SetupTest()
        {

            #region Environment

            environmentName = ConfigurationManager.AppSettings.Get("EnvironmentName");

            #endregion

            #region Provider

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

            #endregion

            #region Default User

            string username = ConfigurationManager.AppSettings["Username"];
            string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];
            if (dataEncoded.Equals("true"))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(username);
                username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            }

            var userid = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);

            #endregion

            #region Language
            _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

            #endregion Language

            #region Business Unit
            _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

            #endregion

            #region Team
            _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

            #endregion

            #region System User ServiceProvidedRateScheduleUser1
            _systemUsername = "ServiceProvidedRateScheduleUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ServiceProvidedRateSchedule", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            #region Ethnicity

            _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person


            _personFirstname = "John Doe";
            _personID = dbHelper.person.CreatePersonRecord("", _personFirstname, "", currentDate, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);

            #endregion

            #region Rate Units

            rateunitid = dbHelper.rateUnit.GetByName("Per Day \\ Days")[0];
            per1HourWhole_rateunitid = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)")[0];
            oneOff_rateunitid = dbHelper.rateUnit.GetByName("One-Off \\ One-Off")[0];
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(rateunitid);
            validRateUnits.Add(per1HourWhole_rateunitid);
            validRateUnits.Add(oneOff_rateunitid);

            #endregion

            #region Service Element 1
            serviceelement1id = commonMethodsDB.CreateServiceElement1("SE1_1080" + currentDate, _careDirectorQA_TeamId, new DateTime(2021, 1, 1), 9788, 1, 2, validRateUnits);

            #endregion

            #region Service Element 2
            serviceelement2id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, "SE2_1080" + currentDate, new DateTime(2021, 1, 1), 9789);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1080

        [TestProperty("JiraIssueID", "ACC-1098")]
        [Description("Test automation for Step 1 to 7 from CDV6-2564: Service Provided Rate Schedule record creation & Validations")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvidedRateSchedule_UITestMethod001()
        {
            #region Step 1 to Step 7

            #region Provider
            providerid = commonMethodsDB.CreateProvider("Provider1080_" + currentDate, _careDirectorQA_TeamId, 2);
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, providerid, serviceelement1id, serviceelement2id, null, null, null, 2);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, per1HourWhole_rateunitid, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 1);
            string serviceProvidedRatePeriodName = dbHelper.serviceProvidedRatePeriod.GetByID(ratePeriodId1, "name")["name"].ToString();
            serviceProvidedRatePeriodName = Regex.Replace(serviceProvidedRatePeriodName, @"\s+", " ");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord("Provider1080_" + currentDate, providerid.ToString())
                .OpenProviderRecord(providerid.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .OpenServiceProvidedRecord(serviceProvidedId.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad()
                .NavigateToRatePeriodsTab();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .SelectAvailableViewByText("Pending Rate Periods")
                .OpenServiceProvidedRatePeriodRecord(ratePeriodId1.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickRateSchedulesTab();

            serviceProvidedRateSchedulesPage
                .WaitForServicesProvidedRateSchedulesPageToLoad()
                .ClickNewRecordButton();

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ValidateServiceProvidedRatePeriodLinkText(serviceProvidedRatePeriodName)
                .InsertRate("10")
                .InsertRateBankHoliday("20")
                .InsertTimeBandStart("10:00")
                .InsertTimeBandEnd("16:00")
                .ClickSelectall_NoOption()
                .ClickMonday_YesOption()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ValidateServiceProvidedRatePeriodLinkText(serviceProvidedRatePeriodName)
                .ValidateRateText("10.00")
                .ValidateRateBankHolidayText("20.00")
                .ValidateTimeBandStartText("10:00")
                .ValidateTimeBandEndText("16:00")
                .ValidateSelectall_NoOptionChecked()
                .ValidateMonday_YesOptionChecked()
                .ValidateTuesday_NoOptionChecked()
                .ValidateTuesday_NoOptionChecked()
                .ValidateWednesday_NoOptionChecked()
                .ValidateThursday_NoOptionChecked()
                .ValidateFriday_NoOptionChecked()
                .ValidateSaturday_NoOptionChecked()
                .ValidateSunday_NoOptionChecked();

            serviceProvidedRateScheduleRecordPage
                .ClickSelectall_NoOption()
                .ClickMonday_NoOption()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("It is mandatory for at least one of the days per week to be set to 'Yes'. Please correct as necessary.")
                .TapCloseButton();

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ClickMonday_NoOption()
                .InsertTimeBandStart("17:00");

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The timeband's end time cannot be before the start time")
                .TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1105")]
        [Description("Test automation for Step 8 to 11 from CDV6-2564: Service Provided Rate Schedule record creation & Validations")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvidedRateSchedule_UITestMethod002()
        {
            #region Step 8 to Step 11

            #region Provider
            providerid = commonMethodsDB.CreateProvider("Provider1080_" + currentDate, _careDirectorQA_TeamId, 2);
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, providerid, serviceelement1id, serviceelement2id, null, null, null, 2);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, per1HourWhole_rateunitid, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 1);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord("Provider1080_" + currentDate, providerid.ToString())
                .OpenProviderRecord(providerid.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .OpenServiceProvidedRecord(serviceProvidedId.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad()
                .NavigateToRatePeriodsTab();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .SelectAvailableViewByText("Pending Rate Periods")
                .OpenServiceProvidedRatePeriodRecord(ratePeriodId1.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickRateSchedulesTab();

            serviceProvidedRateSchedulesPage
                .WaitForServicesProvidedRateSchedulesPageToLoad()
                .ClickNewRecordButton();

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .InsertRate("10")
                .InsertRateBankHoliday("20")
                .InsertTimeBandStart("10:00")
                .InsertTimeBandEnd("16:00")
                .ClickSelectall_YesOption()
                .ValidateSelectall_YesOptionChecked()
                .ValidateMonday_YesOptionChecked()
                .ValidateTuesday_YesOptionChecked()
                .ValidateTuesday_YesOptionChecked()
                .ValidateWednesday_YesOptionChecked()
                .ValidateThursday_YesOptionChecked()
                .ValidateFriday_YesOptionChecked()
                .ValidateSaturday_YesOptionChecked()
                .ValidateSunday_YesOptionChecked()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ValidateSelectall_YesOptionChecked()
                .ValidateMonday_YesOptionChecked()
                .ValidateTuesday_YesOptionChecked()
                .ValidateTuesday_YesOptionChecked()
                .ValidateWednesday_YesOptionChecked()
                .ValidateThursday_YesOptionChecked()
                .ValidateFriday_YesOptionChecked()
                .ValidateSaturday_YesOptionChecked()
                .ValidateSunday_YesOptionChecked();

            var rateSchedule1Id = dbHelper.serviceProvidedRateSchedule.GetByServiceProvidedRatePeriodId(ratePeriodId1)[0];

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ClickSelectall_NoOption();

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ValidateSelectall_NoOptionChecked()
                .ValidateMonday_NoOptionChecked()
                .ValidateTuesday_NoOptionChecked()
                .ValidateTuesday_NoOptionChecked()
                .ValidateWednesday_NoOptionChecked()
                .ValidateThursday_NoOptionChecked()
                .ValidateFriday_NoOptionChecked()
                .ValidateSaturday_NoOptionChecked()
                .ValidateSunday_NoOptionChecked()
                .ClickMonday_YesOption()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ClickBackButton();


            serviceProvidedRateSchedulesPage
                .WaitForServicesProvidedRateSchedulesPageToLoad()
                .SelectServiceProvidedRateScheduleRecord(rateSchedule1Id.ToString())
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton()
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            rateSchedule1Id = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
    ratePeriodId1, serviceProvidedId, 10m, 15m, new TimeSpan(0, 5, 0), new TimeSpan(23, 55, 0), false, true);

            serviceProvidedRateSchedulesPage
                .WaitForServicesProvidedRateSchedulesPageToLoad()
                .ClickRefreshButton();

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickBackButton();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .ClickRefreshButton()
                .OpenServiceProvidedRatePeriodRecord(ratePeriodId1.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .SelectApprovalStatus("Approved")
                .ClickSaveButton()
                .SelectApprovalStatus("Cancelled")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickRateSchedulesTab();

            serviceProvidedRateSchedulesPage
                .WaitForServicesProvidedRateSchedulesPageToLoad()
                .ClickNewRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("New Rate Schedule records cannot be added to a cancelled Rate Period record.")
                .TapOKButton();

            serviceProvidedRateSchedulesPage
                .WaitForServicesProvidedRateSchedulesPageToLoad()
                .OpenServiceProvidedRateScheduleRecord(rateSchedule1Id.ToString());

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ValidateServiceProvidedLookupButtonDisabled(true)
                .ValidateServiceProvidedRatePeriodLookupButtonDisabled(true)
                .ValidateResponsibleTeamLookupButtonDisabled(true)
                .ValidateRateFieldDisabled(true)
                .ValidateRateBankHolidayFieldDisabled(true)
                .ValidateTimeBandStartFieldDisabled(true)
                .ValidateTimeBandEndFieldDisabled(true)
                .ValidateSelectAllOptionsDisabled(true)
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("A Rate Schedule cannot be deleted after the Rate Period has been approved.")
                .TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1107")]
        [Description("Test automation for Step 12 to 17 from CDV6-2564: Service Provided Rate Schedule record creation & Validations")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvidedRateSchedule_UITestMethod003()
        {
            #region Step 12 to Step 17

            #region Provider            
            providerid = commonMethodsDB.CreateProvider("Provider1080_" + currentDate, _careDirectorQA_TeamId, 2);
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, providerid, serviceelement1id, serviceelement2id, null, null, null, 2);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, rateunitid, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 1);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId1, serviceProvidedId, 10m, 15m, null, null, false, true);
            var ratePeriodId2 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, per1HourWhole_rateunitid, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 1);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId2, serviceProvidedId, 10m, 15m, new TimeSpan(0, 5, 0), new TimeSpan(23, 55, 0), false, true);
            var ratePeriodId3 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, oneOff_rateunitid, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 1);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId3, serviceProvidedId, 10m, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord("Provider1080_" + currentDate, providerid.ToString())
                .OpenProviderRecord(providerid.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapServicesProvidedTab();

            servicesProvidedPage
                .WaitForServicesProvidedPageToLoad()
                .OpenServiceProvidedRecord(serviceProvidedId.ToString());

            serviceProvidedRecordPage
                .WaitForServiceProvidedRecordPageToLoad()
                .NavigateToRatePeriodsTab();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .SelectAvailableViewByText("Pending Rate Periods")
                .OpenServiceProvidedRatePeriodRecord(ratePeriodId1.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickRateSchedulesTab();

            serviceProvidedRateSchedulesPage
                .WaitForServicesProvidedRateSchedulesPageToLoad()
                .ClickNewRecordButton();

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ValidateTimeBandStartFieldVisible(false)
                .ValidateTimeBandEndFieldVisible(false)
                .InsertRate("10")
                .InsertRateBankHoliday("20")
                .ClickSelectall_NoOption()
                .ClickMonday_YesOption()
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("There is already a Rate Schedule record setup for at least one of the days selected. Please correct as necessary.")
                 .TapCloseButton();

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickBackButton();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .ClickRefreshButton()
                .OpenServiceProvidedRatePeriodRecord(ratePeriodId2.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickRateSchedulesTab();

            serviceProvidedRateSchedulesPage
                .WaitForServicesProvidedRateSchedulesPageToLoad()
                .ClickNewRecordButton();

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .InsertRate("10")
                .InsertRateBankHoliday("20")
                .ValidateTimeBandStartFieldVisible(true)
                .ValidateTimeBandEndFieldVisible(true)
                .ValidateTimeBandStartText("")
                .ValidateTimeBandEndText("")
                .InsertTimeBandStart("-00:00")
                .InsertTimeBandEnd("24:00")
                .ValidateTimebandStartFieldErrorMessage("Please enter a valid time, between 00:00 and 23:59")
                .ValidateTimebandEndFieldErrorMessage("Please enter a valid time, between 00:00 and 23:59")
                .InsertTimeBandStart("00:05")
                .InsertTimeBandEnd("23:55")
                .ClickSelectall_NoOption()
                .ClickMonday_YesOption()
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("There is already a Rate Schedule record setup for at least one of the days with the same time selected. Please correct as necessary.")
                 .TapCloseButton();

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickBackButton();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .ClickRefreshButton()
                .OpenServiceProvidedRatePeriodRecord(ratePeriodId3.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickRateSchedulesTab();

            serviceProvidedRateSchedulesPage
                .WaitForServicesProvidedRateSchedulesPageToLoad()
                .ClickNewRecordButton();

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .InsertRate("50")
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("There is already a Rate Schedule record setup. You are not permitted to create a second record.")
                 .TapCloseButton();

            #endregion
        }

        #endregion

    }
}
