using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace Phoenix.UITests.Finance.ServiceProvisions
{
    [TestClass]
    public class ServiceProvisionRateSchedule_UITestCases : FunctionalTest
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
        private Guid _serviceelement1id;
        private Guid _serviceelement2id;
        private Guid _serviceprovisionstartreasonid;
        private Guid _placementRoomTypeId;
        private Guid _serviceProvisionID;
        private string _serviceProvisionName;
        private Guid _rateTypeId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void UITests_SetupTest()
        {

            #region Environment

            environmentName = ConfigurationManager.AppSettings.Get("EnvironmentName");

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

            _systemUsername = "ServiceProvisionRateScheduleUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "Service Provision Rate Schedule", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);


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

            #region Rate Type (Update DecimalPlaces, ServiceDelivery & StartTime)

            _rateTypeId = dbHelper.rateType.GetByName("Days")[0];
            dbHelper.rateType.UpdateServiceDelivery(_rateTypeId, true);
            dbHelper.rateType.UpdateDecimalPlaces(_rateTypeId, true);
            dbHelper.rateType.UpdateStartTime(_rateTypeId, false);

            #endregion

            #region Service Element 1            

            var serviceElement1Name = "SE1_Person_Service_Provision";
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 3; // Person
            var paymentscommenceid = 1; //Actual 
            _defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
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
            var serviceElement2Name = "SE2_Person_Service_Provision";
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

        #region https://advancedcsg.atlassian.net/browse/ACC-1313

        [TestProperty("JiraIssueID", "ACC-1348")]
        [Description("Verify user able to create service provision rate schedule record" +
                    "Verify when the Services Provisions Rate Period = Approved or Cancelled, no existing Service Provision Rate Schedule can be deleted")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvisionRateSchedule_UITestMethod001()
        {
            #region Service Provision Rate Period

            DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime endDate = DateTime.Now.Date.AddDays(3);

            var RatePeriodId = dbHelper.serviceProvisionRatePeriod.CreateServiceProvisionRatePeriod(_teamId, _personID, _serviceProvisionName, _serviceProvisionID, _defaulRateUnitID, currentDate, endDate);

            string serviceProvisionRatePeriodName = dbHelper.serviceProvisionRatePeriod.GetByServiceProvisionRatePeriodId(RatePeriodId, "name")["name"].ToString();
            serviceProvisionRatePeriodName = Regex.Replace(serviceProvisionRatePeriodName, @"\s+", " ");

            #endregion

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
                 .WaitForServiceProvisionRecordPageToLoad()
                 .NavigateToRatePeriodsTab();

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad()
                .SelectAvailableViewByText("Pending Rate Periods")
                .OpenServiceProvisionRatePeriodRecord(RatePeriodId.ToString());

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .NavigateToRateScheduleTab();

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad()
                .ClickNewRecordButton();

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .ValidateServiceProvisionRatePeriodLinkText(serviceProvisionRatePeriodName)
                .ValidateServiceProvisionidLinkText(_serviceProvisionName)
                .ValidateResponsibleTeamLinkText("CareDirector QA")
                .InsertRate("10")
                .InsertRateBankHoliday("11")
                .ClickMonday_YesOption()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            var RateScheduleRecordId = dbHelper.serviceProvisionRateSchedule.GetByServiceProvisionRatePeriodId(RatePeriodId).FirstOrDefault();

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad()
                .ValidateRecordPresent(RateScheduleRecordId.ToString());

            #endregion

            #region Step 3

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesMenuSectionToLoad()
                .ClickBackButton();

            serviceProvisionRatePeriodsPage
                .WaitForServiceProvisionRatePeriodsPageToLoad()
                .OpenServiceProvisionRatePeriodRecord(RatePeriodId.ToString());

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .SelectApprovalStatus("Approved")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .NavigateToRateScheduleTab();

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad()
                .OpenServiceProvisionRateScheduleRecord(RateScheduleRecordId.ToString());

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad();

            System.Threading.Thread.Sleep(1000);

            serviceProvisionRateScheduleRecordPage
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

            System.Threading.Thread.Sleep(3000);

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("A Rate Schedule cannot be deleted after the Rate Period has been approved.")
                .TapCloseButton();

            #endregion

            #region Step 4

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .ClickBackButton();

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesMenuSectionToLoad()
                .ClickDetailsTab();

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .SelectApprovalStatus("Cancelled")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .NavigateToRateScheduleTab();

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad()
                .ClickNewRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("New Rate Schedule records cannot be added to a cancelled Rate Period record.")
                .TapOKButton();

            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1320

        [TestProperty("JiraIssueID", "ACC-1367")]
        [Description("Validate message when Rate Unit has for the Rate Type associated, Service Delivery? = Yes AND Start Time? = No" +
                     "Validate message when Rate Unit has for the Rate Type associated, Service Delivery? = Yes AND Start Time? = Yes" +
                     "Validate message when Rate Unit has for the Rate Type associated, Service Delivery = No" +
                     "Validate messae when when all values set to 'No' in  Rate Applies to Days section.")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvisionRateSchedule_UITestMethod002()
        {
            #region Service Provision Rate Period / Rate Schedule

            DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime endDate = DateTime.Now.Date.AddDays(3);

            var RatePeriodId = dbHelper.serviceProvisionRatePeriod.CreateServiceProvisionRatePeriod(_teamId, _personID, _serviceProvisionName, _serviceProvisionID, _defaulRateUnitID, currentDate, endDate);
            var RateScheduleId = dbHelper.serviceProvisionRateSchedule.CreateServiceProvisionRateSchedule(_teamId, _personID, _serviceProvisionID, RatePeriodId, 11, 12, false, true);

            #endregion

            #region Step 5 & 8

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
                .SelectAvailableViewByText("Pending Rate Periods")
                .OpenServiceProvisionRatePeriodRecord(RatePeriodId.ToString());

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .NavigateToRateScheduleTab();

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad()
                .ClickNewRecordButton();

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .InsertRate("12")
                .InsertRateBankHoliday("13")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("It is mandatory for at least one of the days per week to be set to 'Yes'. Please correct as necessary.")
                .TapCloseButton();

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .ClickMonday_YesOption()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already a Rate Schedule record setup for at least one of the days selected. Please correct as necessary.")
                .TapCloseButton();

            #endregion

            #region Step 6

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            System.Threading.Thread.Sleep(1000);

            dbHelper.serviceProvisionRateSchedule.DeleteServiceProvisionRateScheduleRecord(RateScheduleId);

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad()
                .ClickRefreshButton();

            //Rate Type (Update StartTime)
            dbHelper.rateType.UpdateStartTime(_rateTypeId, true);

            RateScheduleId = dbHelper.serviceProvisionRateSchedule.CreateServiceProvisionRateSchedule(_teamId, _personID, _serviceProvisionID, RatePeriodId, 11, 12, new TimeSpan(0, 5, 0), new TimeSpan(2, 0, 0), false, true);

            serviceProvisionRateSchedulePage
                .ClickRefreshButton()
                .ClickNewRecordButton();

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .InsertRate("12")
                .InsertRateBankHoliday("13")
                .InsertTimeBandStart("01:00")
                .InsertTimeBandEnd("03:00")
                .ClickMonday_YesOption()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already a Rate Schedule record setup for at least one of the days with the same time selected. Please correct as necessary.")
                .TapCloseButton();

            #endregion

            #region Step 7

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            System.Threading.Thread.Sleep(1000);
            dbHelper.serviceProvisionRateSchedule.DeleteServiceProvisionRateScheduleRecord(RateScheduleId);

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad()
                .ClickRefreshButton();

            //Rate Type (Update Service Delivery / StartTime)
            dbHelper.rateType.UpdateServiceDelivery(_rateTypeId, false);
            dbHelper.rateType.UpdateStartTime(_rateTypeId, false);

            dbHelper.serviceProvisionRateSchedule.CreateServiceProvisionRateSchedule(_teamId, _personID, _serviceProvisionID, RatePeriodId, 11, null);

            serviceProvisionRateSchedulePage
                .ClickRefreshButton()
                .ClickNewRecordButton();

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .InsertRate("12")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already a Rate Schedule record setup. You are not permitted to create a second record.")
                .TapCloseButton();

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad();

            #region Change to Default Value

            dbHelper.rateType.UpdateServiceDelivery(_rateTypeId, true);
            dbHelper.rateType.UpdateDecimalPlaces(_rateTypeId, true);
            dbHelper.rateType.UpdateStartTime(_rateTypeId, false);

            #endregion

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1321

        [TestProperty("JiraIssueID", "ACC-1381")]
        [Description("Validate message when Rate Unit has for the Rate Type associated, Start Time? = Yes" +
                     "Validate TimeBand Start and TimeBand End Field should be display" +
                     "Verify Time Band Start and Time Band End fields are defaulted with NULL value" +
                     "Verify Time Band Start and Time Band End allows only time range from 00:00 to 23:59" +
                     "Verify system displays error message as 'Timeband's end time cannot be before Start Time' upon recording end time before start time")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServiceProvisionRateSchedule_UITestMethod003()
        {
            #region Service Provision Rate Period

            DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime endDate = DateTime.Now.Date.AddDays(3);
            var RatePeriodId = dbHelper.serviceProvisionRatePeriod.CreateServiceProvisionRatePeriod(_teamId, _personID, _serviceProvisionName, _serviceProvisionID, _defaulRateUnitID, currentDate, endDate);

            #endregion

            #region Rate Type (Update StartTime = Yes)

            dbHelper.rateType.UpdateStartTime(_rateTypeId, true);

            #endregion

            #region Step 9

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
                .SelectAvailableViewByText("Pending Rate Periods")
                .OpenServiceProvisionRatePeriodRecord(RatePeriodId.ToString());

            serviceProvisionRatePeriodRecordPage
                .WaitForServiceProvisionRatePeriodRecordPageToLoad()
                .ValidateRateScheduleTabVisibility(true);

            #endregion

            #region Step 10

            serviceProvisionRatePeriodRecordPage
                .NavigateToRateScheduleTab();

            serviceProvisionRateSchedulePage
                .WaitForServicesProvisionRateSchedulesPageToLoad()
                .ClickNewRecordButton();

            serviceProvisionRateScheduleRecordPage
                .WaitForServiceProvisionRateScheduleRecordPageToLoad()
                .ValidateTimeBandStartFieldVisibility(true)
                .ValidateTimeBandEndFieldVisibility(true);

            #endregion

            #region Step 11

            serviceProvisionRateScheduleRecordPage
                .ValidateTimeBandStartValue("")
                .ValidateTimeBandEndValue("");

            #endregion

            #region Step 12

            serviceProvisionRateScheduleRecordPage
                .InsertTimeBandStart("23:60")
                .ValidateTimeBandStartErrorLabelText("Please enter a valid time, between 00:00 and 23:59")
                .InsertTimeBandEnd("23:60")
                .ValidateTimeBandEndErrorLabelText("Please enter a valid time, between 00:00 and 23:59");

            #endregion

            #region Step 13

            serviceProvisionRateScheduleRecordPage
                .InsertTimeBandStart("12:00")
                .InsertTimeBandEnd("05:00");

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The timeband's end time cannot be before the start time")
                .TapCloseButton();

            //Rate Type (Update StartTime = No)
            dbHelper.rateType.UpdateStartTime(_rateTypeId, false);

            #endregion

        }

        #endregion

    }
}
