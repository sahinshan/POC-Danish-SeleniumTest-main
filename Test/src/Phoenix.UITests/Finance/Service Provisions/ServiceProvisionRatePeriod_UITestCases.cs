using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceProvisions
{
    [TestClass]
    public class ServiceProvisionRatePeriod_UITestCases : FunctionalTest
    {
        #region Properties

        private string environmentName;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private string _systemUsername;
        private Guid _systemUserId;
        private Guid _personID;
        private int _personNumber;
        private Guid _serviceprovisionstatusid;
        private Guid _defaulRateUnitID;
        private Guid _rateUnitId;
        private Guid _serviceelement1id;
        private Guid _serviceelement2id;
        private Guid _serviceprovisionstartreasonid;
        private Guid _placementRoomTypeId;
        private Guid _serviceProvisionID;
        private string _serviceProvisionName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void UITests_SetupTest()
        {

            #region Environment

            environmentName = ConfigurationManager.AppSettings.Get("EnvironmentName");

            #endregion

            #region Default User

            string username = ConfigurationManager.AppSettings["Username"];
            string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

            username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
            var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

            #endregion

            #region Authentication Provider

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

            #endregion

            #region Language

            _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

            #endregion Language

            #region Business Unit

            _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

            #endregion

            #region Team

            _teamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

            #endregion

            #region System User

            _systemUsername = "ServiceProvisionUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "Service Provision", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);


            #endregion

            #region Ethnicity

            _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Automation";
            var lastName = "LN_" + _currentDateSuffix;
            _personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Service Provision Status

            _serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];

            #endregion

            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1").FirstOrDefault();

            #endregion

            #region GL Code

            var expenditureCode1 = commonMethodsHelper.GetRandomValue(1, 2147483647).ToString();
            var glCodeId1 = commonMethodsDB.CreateGLCode(_teamId, _glCodeLocationId, "GLC_" + expenditureCode1, expenditureCode1, "1B");

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "SE1_" + _currentDateSuffix;
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 3; // Person
            var paymentscommenceid = 1; //Actual 
            _defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            _rateUnitId = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(_rateUnitId);
            validRateUnits.Add(_defaulRateUnitID);

            int AdjustedDays = 0;
            var PaymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
            var ProviderBatchGroupingId = commonMethodsDB.CreateProviderBatchGrouping("Standard", new DateTime(2023, 1, 1), _teamId);
            var VatCodeId = dbHelper.vatCode.GetByName("Exempt").FirstOrDefault();

            if (!dbHelper.serviceElement1.GetByName(serviceElement1Name).Any())
                _serviceelement1id = commonMethodsDB.CreateServiceElement1(_teamId, serviceElement1Name, startDate, code, whotopayid, paymentscommenceid, validRateUnits, _defaulRateUnitID, PaymentTypeCodeId, ProviderBatchGroupingId, AdjustedDays, VatCodeId, false, glCodeId1);
            _serviceelement1id = dbHelper.serviceElement1.GetByName(serviceElement1Name).FirstOrDefault();

            #endregion

            #region Service Element 2            
            var serviceElement2Name = "SE2_" + _currentDateSuffix;
            if (!dbHelper.serviceElement2.GetByName(serviceElement2Name).Any())
                _serviceelement2id = commonMethodsDB.CreateServiceElement2(_teamId, serviceElement2Name, startDate, code);
            _serviceelement2id = dbHelper.serviceElement2.GetByName(serviceElement2Name).FirstOrDefault();

            #endregion

            #region Service Mapping            

            commonMethodsDB.CreateServiceMapping(_teamId, _serviceelement1id, _serviceelement2id);

            #endregion

            #region Service Provision Start Reason

            _serviceprovisionstartreasonid = commonMethodsDB.CreateServiceProvisionStartReason(_teamId, _businessUnitId, "Service Provision Start Reason", new DateTime(2022, 1, 1));

            #endregion

            #region Placement Room Type

            _placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

            #endregion

            #region Service Provision

            _serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_teamId, _systemUserId, _personID,
                _serviceprovisionstatusid, _serviceelement1id, _serviceelement2id,
                _defaulRateUnitID, _serviceprovisionstartreasonid, _placementRoomTypeId, new DateTime(2023, 1, 1), true);

            _serviceProvisionName = (string)dbHelper.serviceProvision.GetServiceProvisionById(_serviceProvisionID, "name")["name"];

            #endregion

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1077

        [TestProperty("JiraIssueID", "ACC-1097")]
        [Description("Create Service Provision Record and Validate Rate Period Tab should be dispaly." +
                     "Validate Default Rate Unit in Rate Priod Record." +
                     "Change Rate Unit from Provision Record and validate Rate Period/Schedule Rercord should be deleted.")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvisionRatePeriod_UITestMethod001()
        {
            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(_serviceProvisionID.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad();

            #endregion

            #region Step 3

            serviceProvisionRecordPage
                 .ValidateRatePeriodsTabVisibility(true)
                 .NavigateToRatePeriodsTab();

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad();

            #endregion

            #region Step 4

            serviceProvisionRatePeriodsPage
                .ClickCreateNewRecordButton();

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad();

            #endregion

            #region Step 5

            serviceProvisionRatePeriodRecordPage
                .ValidateServiceProvisionLinkFieldText(_serviceProvisionName)
                .ValidateRateUnitLinkFieldText("Per Day \\ Days")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateApprovalStatusFieldSelectedText("Pending");

            #endregion

            #region Step 6

            string currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("dd'/'MM'/'yyyy");
            string futureDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(2).ToString("dd'/'MM'/'yyyy");

            serviceProvisionRatePeriodRecordPage
                .InsertStartDate(currentDate)
                .InsertEndDate(futureDate)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ClickBackButton();

            #endregion

            #region Step 7

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad()
                .SelectAvailableViewByText("Pending Rate Periods");

            var RatePeriodRecordCount = dbHelper.serviceProvisionRatePeriod.GetByServiceProvisionId(_serviceProvisionID);
            var RatePeriodRecordId = dbHelper.serviceProvisionRatePeriod.GetByServiceProvisionId(_serviceProvisionID).FirstOrDefault();
            Assert.AreEqual(1, RatePeriodRecordCount.Count);

            serviceProvisionRatePeriodsPage
                .ValidateRecordPresent(RatePeriodRecordId.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToDetailsTab()
                 .WaitForServiceProvisionRecordPageToLoad()
                 .ClickRateUnitLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Per 1 Hour Unit")
                .TapSearchButton()
                .SelectResultElement(_rateUnitId.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Changing the Rate Unit will delete any Rate Period/Schedule records associated with this Service Provision. Do you wish to continue?")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToRatePeriodsTab();

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad()
                .SelectAvailableViewByText("Pending Rate Periods");

            RatePeriodRecordCount = dbHelper.serviceProvisionRatePeriod.GetByServiceProvisionId(_serviceProvisionID);
            Assert.AreEqual(0, RatePeriodRecordCount.Count);

            serviceProvisionRatePeriodsPage
                .ValidateRecordNotPresent(RatePeriodRecordId.ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1102")]
        [Description("Create Service Provision Rate Period and validate Approval Status" +
                    "Verify End date can only be the same or a later date than the Start date." +
                    "Verify user not able to create records with overlapping dates" +
                    "Verify system displays warning message as below when user create records with overlapping dates.")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvisionRatePeriod_UITestMethod002()
        {
            string currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("dd'/'MM'/'yyyy");
            string pastDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3).ToString("dd'/'MM'/'yyyy");
            string futureDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(4).ToString("dd'/'MM'/'yyyy");
            string tomorrowDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(2).ToString("dd'/'MM'/'yyyy");

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(_serviceProvisionID.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToRatePeriodsTab();

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad()
                .ClickCreateNewRecordButton();

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .InsertStartDate(currentDate)
                .InsertEndDate(futureDate)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateApprovalStatusFieldSelectedText("Pending");

            #endregion

            #region Step 9

            var ServiceProvisionRatePeriodId = dbHelper.serviceProvisionRatePeriod.GetByServiceProvisionId(_serviceProvisionID).FirstOrDefault();
            var RateScheduleRecordCount = dbHelper.serviceProvisionRateSchedule.GetByServiceProvisionRatePeriodId(ServiceProvisionRatePeriodId);
            Assert.AreEqual(0, RateScheduleRecordCount.Count);

            serviceProvisionRatePeriodRecordPage
                .ValidateApprovalStatusApprovedDisabled(true)
                .ValidateApprovalStatusCancelledDisabled(true);

            #endregion

            #region Step 10

            serviceProvisionRatePeriodRecordPage
                .InsertEndDate(pastDate);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The end date cannot be before the start date")
                .TapOKButton();

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .InsertEndDate(futureDate)
                .ClickSaveAndCloseButton();

            #endregion

            #region Step 11 & 12

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad()
                .ClickCreateNewRecordButton();

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .InsertStartDate(tomorrowDate)
                .InsertEndDate(futureDate)
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already a record for this Rate Unit with dates that overlap. Please correct as necessary.")
                .TapCloseButton();

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Step 13

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad()
                .SelectAvailableViewByText("Pending Rate Periods")
                .OpenServiceProvisionRatePeriodRecord(ServiceProvisionRatePeriodId.ToString());

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .InsertEndDate("")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateEndDateFieldText("");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1108")]
        [Description("Verify system displays warning message when removing the End Date where another Rate period record exist." +
                    "Approval Status = Pending then verify The Status cannot be changed directly to Cancelled & " +
                    "Approval Status, Start Date and End Date can be edited & Rate Period can be deleted")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvisionRatePeriod_UITestMethod003()
        {
            DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime endDate = DateTime.Now.Date.AddDays(3);
            DateTime startDate = DateTime.Now.Date.AddDays(4);

            #region Service Provision Rate Period / Schedule

            var RatePeriodId1 = dbHelper.serviceProvisionRatePeriod.CreateServiceProvisionRatePeriod(_teamId, _personID, _serviceProvisionName, _serviceProvisionID, _defaulRateUnitID, currentDate, endDate);
            dbHelper.serviceProvisionRateSchedule.CreateServiceProvisionRateSchedule(_teamId, _personID, _serviceProvisionID, RatePeriodId1, 10, 11, false, true);
            dbHelper.serviceProvisionRatePeriod.UpdateStatus(RatePeriodId1, 2);

            var RatePeriodId2 = dbHelper.serviceProvisionRatePeriod.CreateServiceProvisionRatePeriod(_teamId, _personID, _serviceProvisionName, _serviceProvisionID, _defaulRateUnitID, startDate, null);
            dbHelper.serviceProvisionRateSchedule.CreateServiceProvisionRateSchedule(_teamId, _personID, _serviceProvisionID, RatePeriodId2, 11, 12, false, false, true);

            #endregion

            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(_serviceProvisionID.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToRatePeriodsTab();

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad()
                .SelectAvailableViewByText("Related Records");

            var RatePeriodRecordCount = dbHelper.serviceProvisionRatePeriod.GetByServiceProvisionId(_serviceProvisionID);
            Assert.AreEqual(2, RatePeriodRecordCount.Count);

            serviceProvisionRatePeriodsPage
                .OpenServiceProvisionRatePeriodRecord(RatePeriodId1.ToString());

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .InsertEndDate("")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already a record for this Rate Unit with dates that overlap. Please correct as necessary.")
                .TapCloseButton();

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .ClickBackButton();

            #endregion

            #region Step 15

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad()
                .OpenServiceProvisionRatePeriodRecord(RatePeriodId2.ToString());

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .ValidateServiceProvisionLookupButtonDisabled()
                .ValidateResponsibleTeamFieldLookupDisabled()
                .ValidateRateUnitLookupButtonDisabled()
                .ValidateApprovalStatusFieldEnabled()
                .ValidateStartDateFieldDisabled(false)
                .ValidateEndDateFieldDisabled(false)
                .ClickApprovalStatusPicklist()
                .ValidateApprovalStatusApprovedDisabled(false)
                .ValidateApprovalStatusCancelledDisabled(true)
                .ClickApprovalStatusPicklist();

            var RateScheduleRecordCount = dbHelper.serviceProvisionRateSchedule.GetByServiceProvisionRatePeriodId(RatePeriodId2);
            Assert.AreEqual(1, RateScheduleRecordCount.Count);

            serviceProvisionRatePeriodRecordPage
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(3000);

            var ServiceProvisionRatePeriodCount = dbHelper.serviceProvisionRatePeriod.GetByServiceProvisionId(_serviceProvisionID);
            Assert.AreEqual(1, ServiceProvisionRatePeriodCount.Count);

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad()
                .ValidateRecordNotPresent(RatePeriodId2.ToString());

            RateScheduleRecordCount = dbHelper.serviceProvisionRateSchedule.GetByServiceProvisionRatePeriodId(RatePeriodId2);
            Assert.AreEqual(0, RateScheduleRecordCount.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1111")]
        [Description("Verify Record cannot be delete where Rate Period record has Status = Approved." +
                     "Verify End Date can be edited and Approval Status can be changed but only to Cancelled" +
                     "Verify Responsible Team can only be changed using the “Assign” button)" +
                     "Verify the New Service Provision Rate Schedule records can be added at any time BUT no existing Service Provision Rate Schedule records can be deleted")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvisionRatePeriod_UITestMethod004()
        {
            DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime endDate = DateTime.Now.Date.AddDays(3);

            #region Team

            var _teamId2Name = "CareDirector QA 2";
            var _teamId2 = commonMethodsDB.CreateTeam(_teamId2Name, null, _businessUnitId, "", "CareDirectorQA2@careworkstempmail.com", _teamId2Name, "021 654321");

            #endregion

            #region Service Provision Rate Period / Schedule

            var RatePeriodId1 = dbHelper.serviceProvisionRatePeriod.CreateServiceProvisionRatePeriod(_teamId, _personID, _serviceProvisionName, _serviceProvisionID, _defaulRateUnitID, currentDate, endDate);
            var RateScheduleId = dbHelper.serviceProvisionRateSchedule.CreateServiceProvisionRateSchedule(_teamId, _personID, _serviceProvisionID, RatePeriodId1, 10, 11, false, true);
            dbHelper.serviceProvisionRatePeriod.UpdateStatus(RatePeriodId1, 2);

            #endregion

            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(_serviceProvisionID.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToRatePeriodsTab();

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad()
                .ValidateSelectedViewText("Approved Rate Periods")
                .OpenServiceProvisionRatePeriodRecord(RatePeriodId1.ToString());

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("A Service Provision Rate Period cannot be deleted once it has been approved or cancelled.")
                .TapCloseButton();

            #endregion

            #region Step 17

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .ValidateStartDateFieldDisabled(true)
                .ValidateEndDateFieldDisabled(false)
                .ClickApprovalStatusPicklist()
                .ValidateApprovalStatusPendingDisabled(true)
                .ValidateApprovalStatusCancelledDisabled(false)
                .ValidateAssignRecordButtonVisible()
                .ClickAssignRecordButton();

            assignRecordPopup
                .WaitForAssignRecordPopupForPrimarySupportToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery(_teamId2Name.ToString())
                .TapSearchButton()
                .SelectResultElement(_teamId2.ToString())
                .ClickAssignOkButton();

            System.Threading.Thread.Sleep(2000);

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .ValidateResponsibleTeamLinkFieldText(_teamId2Name.ToString());

            #endregion

            #region Step 18

            serviceProvisionRatePeriodRecordPage
                .NavigateToRateScheduleTab();

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad()
                .OpenServiceProvisionRateScheduleRecord(RateScheduleId.ToString());

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .ValidateServiceProvisionRatePeriodLookupButtonDisabled(true)
                .ValidateServiceProvisionLookupButtonDisabled(true)
                .ValidateResponsibleTeamLookupButtonDisabled(true)
                .ValidateRateFieldDisabled(true)
                .ValidateRateBankHolidayFieldDisabled(true)
                .ValidateSelectAllOptionsDisabled(true)
                .ValidateMondayOptionsDisabled(true)
                .ValidateTuesdayOptionsDisabled(true)
                .ValidateWednesdayOptionsDisabled(true)
                .ValidateThursdayOptionsDisabled(true)
                .ValidateFridayOptionsDisabled(true)
                .ValidateSaturdayOptionsDisabled(true)
                .ValidateSundayOptionsDisabled(true)
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("A Rate Schedule cannot be deleted after the Rate Period has been approved.")
                .TapCloseButton();

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .ClickBackButton();

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad()
                .ClickNewRecordButton();

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .InsertRate("11")
                .InsertRateBankHoliday("12")
                .ClickTuesday_YesOption()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            serviceProvisionRateScheduleRecordPage
                .ValidateRateFieldDisabled(true)
                .ValidateRateBankHolidayFieldDisabled(true)
                .ClickBackButton();

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad();

            var RateScheduleRecordCount = dbHelper.serviceProvisionRateSchedule.GetByServiceProvisionRatePeriodId(RatePeriodId1);
            Assert.AreEqual(2, RateScheduleRecordCount.Count);

            #endregion

        }


        [TestProperty("JiraIssueID", "ACC-1120")]
        [Description("Validate When Approval Status = Cancelled" +
                    "1. Record cannot be deleted" +
                    "2. All fields are read only and cannot be changed" +
                    "3. The Approval Status cannot be changed to anything else" +
                    "4. All fields on the associated Services Provisions Rate Schedule record(s) are read only and cannot be changed" +
                    "5. No new Service Provision Rate Schedule record(s) can be created")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvisionRatePeriod_UITestMethod005()
        {
            DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime endDate = DateTime.Now.Date.AddDays(3);

            #region Service Provision Rate Period / Schedule

            var RatePeriodId = dbHelper.serviceProvisionRatePeriod.CreateServiceProvisionRatePeriod(_teamId, _personID, _serviceProvisionName, _serviceProvisionID, _defaulRateUnitID, currentDate, endDate);
            var RateScheduleId = dbHelper.serviceProvisionRateSchedule.CreateServiceProvisionRateSchedule(_teamId, _personID, _serviceProvisionID, RatePeriodId, 10, 11, false, true);
            dbHelper.serviceProvisionRatePeriod.UpdateStatus(RatePeriodId, 3);

            #endregion

            #region Step 19

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonServiceProvisionsPage();

            personServiceProvisionsPage
                .WaitForPersonServiceProvisionsPageToLoad()
                .OpenRecord(_serviceProvisionID.ToString());

            serviceProvisionRecordPage
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToRatePeriodsTab();

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad()
                .SelectAvailableViewByText("Cancelled Rate Periods")
                .OpenServiceProvisionRatePeriodRecord(RatePeriodId.ToString());

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .ValidateApprovalStatusFieldDisabled()
                .ValidateStartDateFieldDisabled(true)
                .ValidateEndDateFieldDisabled(true)
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("A Service Provision Rate Period cannot be deleted once it has been approved or cancelled.")
                .TapCloseButton();

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .NavigateToRateScheduleTab();

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad()
                .OpenServiceProvisionRateScheduleRecord(RateScheduleId.ToString());

            System.Threading.Thread.Sleep(2000);

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .ValidateServiceProvisionRatePeriodLookupButtonDisabled(true)
                .ValidateServiceProvisionLookupButtonDisabled(true)
                .ValidateResponsibleTeamLookupButtonDisabled(true)
                .ValidateRateFieldDisabled(true)
                .ValidateRateBankHolidayFieldDisabled(true)
                .ValidateSelectAllOptionsDisabled(true)
                .ValidateMondayOptionsDisabled(true)
                .ValidateTuesdayOptionsDisabled(true)
                .ValidateWednesdayOptionsDisabled(true)
                .ValidateThursdayOptionsDisabled(true)
                .ValidateFridayOptionsDisabled(true)
                .ValidateSaturdayOptionsDisabled(true)
                .ValidateSundayOptionsDisabled(true)
                .ClickBackButton();

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad()
                .ClickNewRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("New Rate Schedule records cannot be added to a cancelled Rate Period record.")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad();

            #endregion

        }

        #endregion

    }
}
