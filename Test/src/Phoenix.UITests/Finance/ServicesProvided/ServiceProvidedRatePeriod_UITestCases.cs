using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Providers.ServiceProvided
{
    [TestClass]
    public class ServiceProvidedRatePeriod_UITestCases : FunctionalTest
    {
        #region https://advancedcsg.atlassian.net/browse/ACC-669

        private string environmentName;
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private String _systemUsername;
        private Guid _ethnicityId;
        private Guid _personID;
        private int _personNumber;
        private string _personFirstname;
        private string _personLastname;
        private string currentDate = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid serviceelement1id;
        private Guid serviceelement2id;
        private Guid rateunitid;
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

            #region System User ServiceProvidedUser1
            _systemUsername = "ServiceProvidedUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ServiceProvided", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            #region Ethnicity

            _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person


            _personFirstname = "John Doe";
            _personID = dbHelper.person.CreatePersonRecord("", _personFirstname, "", currentDate, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Rate Units

            rateunitid = dbHelper.rateUnit.GetByName("Per Day \\ Days")[0];
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(rateunitid);

            #endregion

            #region Service Element 1
            serviceelement1id = commonMethodsDB.CreateServiceElement1("SE1_669" + currentDate, _careDirectorQA_TeamId, new DateTime(2021, 1, 1), 9787, 1, 2, validRateUnits);

            #endregion

            #region Service Element 2
            serviceelement2id = commonMethodsDB.CreateServiceElement2(_careDirectorQA_TeamId, "SE2_669" + currentDate, new DateTime(2021, 1, 1), 9787);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-669
        [TestProperty("JiraIssueID", "ACC-711")]
        [Description("Test automation for Step 1 to 7 from CDV6-2555: Service Provided Rate Period record creation & Validations")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvided_UITestMethod001()
        {
            #region Step 1 to Step 7

            #region Provider
            string startDate = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("dd'/'MM'/'yyyy");
            string pastEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2).ToString("dd'/'MM'/'yyyy");
            string futureEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(2).ToString("dd'/'MM'/'yyyy");
            providerid = commonMethodsDB.CreateProvider("Provider669_" + currentDate, _careDirectorQA_TeamId, 2);
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, providerid, serviceelement1id, serviceelement2id, null, null, null, 2);
            string serviceProvidedName = (string)dbHelper.serviceProvided.GetByID(serviceProvidedId, "name")["name"];
            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord("Provider669_" + currentDate, providerid.ToString())
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
                .ClickNewRecordButton();

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickRateUnitLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per Day")
                .TapSearchButton()
                .SelectResultElement(rateunitid.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .SelectApprovalStatus("Pending")
                .InsertStartDate(startDate)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceProvidedRatePeriodRecordPageToLoad();

            serviceProvidedRatePeriodRecordPage
                .ValidateServiceProvidedFieldText(serviceProvidedName)
                .ValidateRateUnitFieldText("Per Day \\ Days")
                .ValidateResponsibleTeamFieldText("CareDirector QA")
                .ValidateApprovalStatusFieldSelectedText("Pending")
                .ValidateStartDateFieldText(startDate)
                .ValidateApprovalStatusPendingDisabled(false)
                .ValidateApprovalStatusApprovedDisabled(true)
                .ValidateApprovalStatusCancelledDisabled(true);

            serviceProvidedRatePeriodRecordPage
                .InsertEndDate(startDate)
                .ClickSaveButton();

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .InsertEndDate(pastEndDate);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The end date cannot be before the start date")
                .TapOKButton();

            serviceProvidedRatePeriodRecordPage
                .InsertEndDate(futureEndDate)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateEndDateFieldText(futureEndDate)
                .InsertEndDate("")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .SelectAvailableViewByText("Pending Rate Periods");

            Assert.AreEqual(1, dbHelper.serviceProvidedRatePeriod.GetByServiceProvidedID(serviceProvidedId, 1).Count);

            serviceProvidedRatePeriodsPage
                .ClickNewRecordButton();

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickRateUnitLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per Day")
                .TapSearchButton()
                .SelectResultElement(rateunitid.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .SelectApprovalStatus("Pending")
                .InsertStartDate(futureEndDate)
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already an active record for this Service and Rate Unit. Please correct as necessary.")
                .TapCloseButton();
            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-713")]
        [Description("Test automation for Step 8 to 10 from CDV6-2555: Service Provided Rate Period record creation & Validations")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvided_UITestMethod002()
        {
            #region Step 8 to Step 10

            #region Provider
            string futureStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1).ToString("dd'/'MM'/'yyyy");
            providerid = commonMethodsDB.CreateProvider("Provider713_" + currentDate, _careDirectorQA_TeamId, 2);
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, providerid, serviceelement1id, serviceelement2id, null, null, null, 2);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, rateunitid, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 2);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId, ratePeriodId1, serviceProvidedId, 10m, 15m);
            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord("Provider713_" + currentDate, providerid.ToString())
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
                .ClickNewRecordButton();

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickRateUnitLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per Day")
                .TapSearchButton()
                .SelectResultElement(rateunitid.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .SelectApprovalStatus("Pending")
                .InsertStartDate(futureStartDate)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            Assert.AreEqual(2, dbHelper.serviceProvidedRatePeriod.GetByServiceProvidedID(serviceProvidedId).Count);

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .SelectAvailableViewByText("Approved Rate Periods")
                .OpenServiceProvidedRatePeriodRecord(ratePeriodId1.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .InsertEndDate("")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already an active record for this Service and Rate Unit. Please correct as necessary.")
                .TapCloseButton();

            var ratePeriodId2 = dbHelper.serviceProvidedRatePeriod.GetByServiceProvidedID(serviceProvidedId, 1)[0];
            dbHelper.serviceProvidedRatePeriod.DeleteServiceProvidedRatePeriod(ratePeriodId2);

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .InsertEndDate("")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateEndDateFieldText("")
                .InsertEndDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            Assert.AreEqual(1, dbHelper.serviceProvidedRatePeriod.GetByServiceProvidedID(serviceProvidedId).Count);

            var ratePeriodId3 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, rateunitid, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(2).Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(2).Day), 1);
            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .SelectAvailableViewByText("Pending Rate Periods")
                .OpenServiceProvidedRatePeriodRecord(ratePeriodId3.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad();

            System.Threading.Thread.Sleep(1500);

            serviceProvidedRatePeriodRecordPage
                .ValidateApprovalStatusPendingDisabled(false)
                .ValidateApprovalStatusApprovedDisabled(true)
                .ValidateApprovalStatusCancelledDisabled(true);

            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId, ratePeriodId3, serviceProvidedId, 10m, 15m);

            serviceProvidedRatePeriodRecordPage
                .ClickBackButton();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .ClickRefreshButton()
                .SelectAvailableViewByText("Pending Rate Periods")
                .OpenServiceProvidedRatePeriodRecord(ratePeriodId3.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad();

            System.Threading.Thread.Sleep(500);

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ValidateApprovalStatusPendingDisabled(false)
                .ValidateApprovalStatusApprovedDisabled(false)
                .ValidateApprovalStatusCancelledDisabled(true)
                .SelectApprovalStatus("Approved")
                .ClickSaveAndCloseButton();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .ClickRefreshButton()
                .SelectAvailableViewByText("Approved Rate Periods")
                .OpenServiceProvidedRatePeriodRecord(ratePeriodId3.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad();

            System.Threading.Thread.Sleep(500);
            serviceProvidedRatePeriodRecordPage
                .ValidateApprovalStatusPendingDisabled(true)
                .ValidateApprovalStatusApprovedDisabled(false)
                .ValidateApprovalStatusCancelledDisabled(false)
                .SelectApprovalStatus("Cancelled")
                .ClickSaveAndCloseButton();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .ClickRefreshButton()
                .SelectAvailableViewByText("Approved Rate Periods")
                .ValidateRecordNotPresent(ratePeriodId3.ToString())
                .SelectAvailableViewByText("Cancelled Rate Periods")
                .ValidateRecordPresent(ratePeriodId3.ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-749")]
        [Description("Test automation for Step 11 to 13 from CDV6-2555: Service Provided Rate Period record creation & Validations")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvided_UITestMethod003()
        {
            #region Step 11 to Step 13

            #region Provider
            string futureStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1).ToString("dd'/'MM'/'yyyy");
            providerid = commonMethodsDB.CreateProvider("Provider749_" + currentDate, _careDirectorQA_TeamId, 2);
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, providerid, serviceelement1id, serviceelement2id, null, null, null, 2);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, rateunitid, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 1);
            //dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId, ratePeriodId1, serviceProvidedId, 10m, 15m);
            #endregion

            #region Rate Unit
            var perOneHourWhole_rateunitid = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)")[0];
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(perOneHourWhole_rateunitid);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord("Provider749_" + currentDate, providerid.ToString())
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
                .ValidateRateUnitFieldLookupEnabled()
                .ValidateStartDateFieldDisabled(false)
                .ValidateEndDateFieldDisabled(false)
                .ClickRateUnitLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per 1 Hour")
                .TapSearchButton()
                .SelectResultElement(perOneHourWhole_rateunitid.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .SelectApprovalStatus("Pending")
                .InsertEndDate("")
                .InsertStartDate(futureStartDate)
                .InsertEndDate(futureStartDate)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateRateUnitFieldText("Per 1 Hour \\ Hours (Whole)")
                .ValidateStartDateFieldText(futureStartDate)
                .ValidateEndDateFieldText(futureStartDate)
                .ClickBackButton();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .ClickRefreshButton();

            Assert.AreEqual(1, dbHelper.serviceProvidedRatePeriod.GetByServiceProvidedID(serviceProvidedId).Count);

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .SelectServiceProvidedRatePeriodsRecord(ratePeriodId1.ToString())
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .SelectAvailableViewByText("Pending Rate Periods")
                .ValidateRecordNotPresent(ratePeriodId1.ToString());

            Assert.AreEqual(0, dbHelper.serviceProvidedRatePeriod.GetByServiceProvidedID(serviceProvidedId).Count);

            ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, rateunitid, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day), 2);
            dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId, ratePeriodId1, serviceProvidedId, 10m, 15m);

            serviceProvidedRatePeriodsPage
                .SelectAvailableViewByText("Approved Rate Periods")
                .OpenServiceProvidedRatePeriodRecord(ratePeriodId1.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Rate Period records that have been approved cannot be deleted.")
                .TapCloseButton();

            #endregion
        }


        [TestProperty("JiraIssueID", "ACC-755")]
        [Description("Test automation for Step 14 to 17 from CDV6-2555: Service Provided Rate Period record creation & Validations")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvided_UITestMethod004()
        {
            #region Step 14 to Step 17
            #region Rate Unit
            var perOneHourWhole_rateunitid = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)")[0];
            var validRateUnits = new List<Guid>();
            validRateUnits.Add(perOneHourWhole_rateunitid);

            #endregion

            #region Provider            
            providerid = commonMethodsDB.CreateProvider("Provider755_" + currentDate, _careDirectorQA_TeamId, 2);
            serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, providerid, serviceelement1id, serviceelement2id, null, null, null, 2);
            var ratePeriodId1 = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, perOneHourWhole_rateunitid, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 1);
            var rateScheduleId1 = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId,
                ratePeriodId1, serviceProvidedId, 10m, 15m, new TimeSpan(0, 5, 0), new TimeSpan(23, 55, 0), false, true);
            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord("Provider755_" + currentDate, providerid.ToString())
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
                .ValidateApprovalStatusApprovedDisabled(false)
                .SelectApprovalStatus("Approved")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateApprovalStatusFieldSelectedText("Approved")
                .ClickBackButton();

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .SelectAvailableViewByText("Approved Rate Periods")
                .ClickRefreshButton();

            Assert.AreEqual(1, dbHelper.serviceProvidedRatePeriod.GetByServiceProvidedID(serviceProvidedId).Count);

            serviceProvidedRatePeriodsPage
                .WaitForServicesProvidedPageToLoad()
                .OpenServiceProvidedRatePeriodRecord(ratePeriodId1.ToString());

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad();

            System.Threading.Thread.Sleep(500);

            serviceProvidedRatePeriodRecordPage
                .ValidateApprovalStatusApprovedDisabled(false)
                .ValidateApprovalStatusCancelledDisabled(false)
                .NavigateToServiceProvidedRateScheduleMenuItem();

            serviceProvidedRateSchedulesPage
                .WaitForServicesProvidedRateSchedulesPageToLoad()
                .ValidateRecordPresent(rateScheduleId1.ToString())
                .ClickNewRecordButton();

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .InsertRate("20")
                .InsertRateBankHoliday("30")
                .ClickSelectall_NoOption()
                .ClickTuesday_YesOption()
                .InsertTimeBandStart("10:30")
                .InsertTimeBandEnd("16:30")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ValidateRateText("20.00")
                .ValidateRateBankHolidayText("30.00")
                .ValidateTimeBandStartText("10:30")
                .ValidateTimeBandEndText("16:30")
                .ValidateTuesday_YesOptionChecked()
                .ValidateTuesday_NoOptionNotChecked()
                .ClickBackButton();

            serviceProvidedRateSchedulesPage
                .WaitForServicesProvidedRateSchedulesPageToLoad()
                .OpenServiceProvidedRateScheduleRecord(rateScheduleId1.ToString());

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ValidateServiceProvidedRatePeriodLookupButtonDisabled(true)
                .ValidateResponsibleTeamLookupButtonDisabled(true)
                .ValidateRateFieldDisabled(true)
                .ValidateRateBankHolidayFieldDisabled(true)
                .ValidateServiceProvidedLookupButtonDisabled(true)
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

            var rateScheduleId2 = dbHelper.serviceProvidedRateSchedule.GetByServiceProvidedRatePeriodId(ratePeriodId1)[0];

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ClickBackButton();

            serviceProvidedRatePeriodRecordPage
                .WaitForServiceProvidedRatePeriodRecordPageToLoad()
                .ClickDetailsTab()
                .SelectApprovalStatus("Cancelled")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateApprovalStatusApprovedDisabled(true)
                .ValidateRateUnitFieldLookupDisabled()
                .ValidateStartDateFieldDisabled(true)
                .ValidateEndDateFieldDisabled(true)
                .ClickRateSchedulesTab();

            serviceProvidedRateSchedulesPage
                .WaitForServicesProvidedRateSchedulesPageToLoad()
                .OpenServiceProvidedRateScheduleRecord(rateScheduleId2.ToString());

            serviceProvidedRateScheduleRecordPage
                .WaitForServiceProvidedRateScheduleRecordPageToLoad()
                .ValidateServiceProvidedRatePeriodLookupButtonDisabled(true)
                .ValidateResponsibleTeamLookupButtonDisabled(true)
                .ValidateRateFieldDisabled(true)
                .ValidateRateBankHolidayFieldDisabled(true)
                .ValidateServiceProvidedLookupButtonDisabled(true)
                .ValidateTimeBandStartFieldDisabled(true)
                .ValidateTimeBandEndFieldDisabled(true)
                .ValidateSelectAllOptionsDisabled(true);
            #endregion
        }

        #endregion

        #endregion
    }
}
