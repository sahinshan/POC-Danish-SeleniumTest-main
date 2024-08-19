using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class CareProviderSchedulingSetupRecordPage : CommonMethods
    {
        public CareProviderSchedulingSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CPSchedulingSetupFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpschedulingsetup&')]");

        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By ActivateButton = By.Id("TI_ActivateButton");
        readonly By BackButton = By.XPath("//button[@title='Back']");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");

        #endregion

        #region Bookings

        readonly By DefaultPublicHolidayForRegularBookings_LabelField = By.XPath("//*[@id='CWLabelHolder_expressbookonpublicholidaydefaultid']/label[text()='Default Public Holiday for Regular Bookings']");
        readonly By DefaultPublicHolidayForRegularBookings_MandatoryField = By.XPath("//*[@id='CWLabelHolder_expressbookonpublicholidaydefaultid']/label/span[@class='mandatory']");
        readonly By DefaultPublicHolidayForRegularBookings_PickList = By.Id("CWField_expressbookonpublicholidaydefaultid");

        readonly By DefaultBookingTypeForPersonAbsence_LabelField = By.XPath("//*[@id='CWLabelHolder_defaultbookingtypeforpersonabsenceid']/label[text()='Default Booking Type for Person Absence']");
        readonly By DefaultBookingTypeForPersonAbsence_MandatoryField = By.XPath("//*[@id='CWLabelHolder_defaultbookingtypeforpersonabsenceid']/label/span[@class='mandatory']");
        readonly By DefaultBookingTypeForPersonAbsence_LinkText = By.Id("CWField_defaultbookingtypeforpersonabsenceid_Link");
        readonly By DefaultBookingTypeForPersonAbsence_LookupButton = By.Id("CWLookupBtn_defaultbookingtypeforpersonabsenceid");

        readonly By IgnoreInductionStatusForScheduling_LabelField = By.XPath("//*[@id='CWLabelHolder_ignoreinductionstatusforscheduling']/label[text()='Ignore Induction Status for Scheduling']");
        readonly By IgnoreInductionStatusForScheduling_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ignoreinductionstatusforscheduling']/label/span[@class='mandatory']");
        readonly By IgnoreInductionStatusForScheduling_YesOption = By.Id("CWField_ignoreinductionstatusforscheduling_1");
        readonly By IgnoreInductionStatusForScheduling_NoOption = By.Id("CWField_ignoreinductionstatusforscheduling_0");

        readonly By ValidForExport_LabelField = By.XPath("//*[@id='CWLabelHolder_validforexport']/label[text()='Valid For Export']");
        readonly By ValidForExport_MandatoryField = By.XPath("//*[@id='CWLabelHolder_validforexport']/label/span[@class='mandatory']");
        readonly By ValidForExport_YesOption = By.Id("CWField_validforexport_1");
        readonly By ValidForExport_NoOption = By.Id("CWField_validforexport_0");

        readonly By UpdateBookingEnd_Day_Date_Time_LabelField = By.XPath("//*[@id='CWLabelHolder_bookingstartchangeaction']/label[text()='Update Booking End Day/Date/Time']");
        readonly By UpdateBookingEnd_Day_Date_Time_MandatoryField = By.XPath("//*[@id='CWLabelHolder_bookingstartchangeaction']/label/span[@class='mandatory']");
        readonly By UpdateBookingEnd_Day_Date_Time_YesOption = By.Id("CWField_bookingstartchangeaction_1");
        readonly By UpdateBookingEnd_Day_Date_Time_NoOption = By.Id("CWField_bookingstartchangeaction_0");

        readonly By DefaultBookingTypePerStaffRoleType_LabelField = By.XPath("//*[@id='CWLabelHolder_defaultbookingtypeperstaffroletype']/label[text()='Default Booking Type Per Staff Role Type']");
        readonly By DefaultBookingTypePerStaffRoleType_MandatoryField = By.XPath("//*[@id='CWLabelHolder_defaultbookingtypeperstaffroletype']/label/span[@class='mandatory']");
        readonly By DefaultBookingTypePerStaffRoleType_YesOption = By.Id("CWField_defaultbookingtypeperstaffroletype_1");
        readonly By DefaultBookingTypePerStaffRoleType_NoOption = By.Id("CWField_defaultbookingtypeperstaffroletype_0");

        readonly By DeleteReasonRequiredSchedule_LabelField = By.XPath("//*[@id='CWLabelHolder_deletereasonrequiredschedule']/label[text()='Delete Reason Required - Schedule']");
        readonly By DeleteReasonRequiredSchedule_MandatoryField = By.XPath("//*[@id='CWLabelHolder_deletereasonrequiredschedule']/label/span[@class='mandatory']");
        readonly By DeleteReasonRequiredSchedule_YesOption = By.Id("CWField_deletereasonrequiredschedule_1");
        readonly By DeleteReasonRequiredSchedule_NoOption = By.Id("CWField_deletereasonrequiredschedule_0");

        readonly By DeleteReasonRequiredDiary_LabelField = By.XPath("//*[@id='CWLabelHolder_deletereasonrequireddiary']/label[text()='Delete Reason Required - Diary']");
        readonly By DeleteReasonRequiredDiary_MandatoryField = By.XPath("//*[@id='CWLabelHolder_deletereasonrequireddiary']/label/span[@class='mandatory']");
        readonly By DeleteReasonRequiredDiary_YesOption = By.Id("CWField_deletereasonrequireddiary_1");
        readonly By DeleteReasonRequiredDiary_NoOption = By.Id("CWField_deletereasonrequireddiary_0");

        #endregion

        #region Validation

        readonly By BookingLengthUnit_mins_LabelField = By.XPath("//*[@id='CWLabelHolder_bookinglengthunit']/label[text()='Booking Length Unit (mins)']");
        readonly By BookingLengthUnit_mins_MandatoryField = By.XPath("//*[@id='CWLabelHolder_bookinglengthunit']/label/span[@class='mandatory']");
        readonly By BookingLengthUnit_mins_Field = By.Id("CWField_bookinglengthunit");

        readonly By CheckStaffAvailability_LabelField = By.XPath("//*[@id='CWLabelHolder_checkstaffavailabilityid']/label[text()='Check Staff Availability']");
        readonly By CheckStaffAvailability_MandatoryField = By.XPath("//*[@id='CWLabelHolder_checkstaffavailabilityid']/label/span[@class='mandatory']");
        readonly By CheckStaffAvailability_PickList = By.Id("CWField_checkstaffavailabilityid");

        readonly By PlannedBookingPrecision_LabelField = By.XPath("//*[@id='CWLabelHolder_plannedbookingprecision']/label[text()='Planned Booking Precision']");
        readonly By PlannedBookingPrecision_MandatoryField = By.XPath("//*[@id='CWLabelHolder_plannedbookingprecision']/label/span[@class='mandatory']");
        internal static string PlannedBookingPrecisionFieldInputId = "CWField_plannedbookingprecision";
        readonly By PlannedBookingPrecision_Field = By.Id(PlannedBookingPrecisionFieldInputId);

        #endregion

        #region Diary Bookings Validation

        readonly By UseBookingTypeClashActions_LabelField = By.XPath("//*[@id='CWLabelHolder_usebookingtypeclashactions']/label");
        readonly By UseBookingTypeClashActions_MandatoryField = By.XPath("//*[@id='CWLabelHolder_usebookingtypeclashactions']/label/span[@class='mandatory']");
        readonly By UseBookingTypeClashActions_YesOption = By.Id("CWField_usebookingtypeclashactions_1");
        readonly By UseBookingTypeClashActions_NoOption = By.Id("CWField_usebookingtypeclashactions_0");

        readonly By UseBookingTypeClashActions_YesOptionToolTip = By.XPath("//*[@id='CWSection_DiaryBookingsValidation']//input[@id='CWField_usebookingtypeclashactions_1']/parent::span");
        readonly By UseBookingTypeClashActions_NoOptionToolTip = By.XPath("//*[@id='CWSection_DiaryBookingsValidation']//input[@id='CWField_usebookingtypeclashactions_0']/parent::span");

        readonly By DoubleBookingAction_LabelField = By.XPath("//*[@id='CWLabelHolder_doublebookingactionid']/label");
        readonly By DoubleBookingAction_MandatoryField = By.XPath("//*[@id='CWLabelHolder_doublebookingactionid']/label/span[@class='mandatory']");
        readonly By DoubleBookingAction_PickList = By.Id("CWField_doublebookingactionid");
        readonly By DoubleBookingAction_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_doublebookingactionid']/label/span");

        #endregion

        #region Contract Hours Validation

        readonly By Salaried_LabelField = By.XPath("//*[@id='CWLabelHolder_contracthoursvalidationsalariedid']/label[text()='Salaried']");
        readonly By Salaried_MandatoryField = By.XPath("//*[@id='CWLabelHolder_contracthoursvalidationsalariedid']/label/span[@class='mandatory']");
        readonly By Salaried_PickList = By.Id("CWField_contracthoursvalidationsalariedid");

        readonly By Hourly_LabelField = By.XPath("//*[@id='CWLabelHolder_contracthoursvalidationhourlyid']/label[text()='Hourly']");
        readonly By Hourly_MandatoryField = By.XPath("//*[@id='CWLabelHolder_contracthoursvalidationhourlyid']/label/span[@class='mandatory']");
        readonly By Hourly_PickList = By.Id("CWField_contracthoursvalidationhourlyid");

        readonly By ScheduledBookingWeeksToCheck_LabelField = By.XPath("//*[@id='CWLabelHolder_contractedhoursschedulecheckweeks']/label[text()='Scheduled Booking Weeks to Check']");
        readonly By ScheduledBookingWeeksToCheck_MandatoryField = By.XPath("//*[@id='CWLabelHolder_contractedhoursschedulecheckweeks']/label/span[@class='mandatory']");
        readonly By ScheduledBookingWeeksToCheck_Field = By.Id("CWField_contractedhoursschedulecheckweeks");

        readonly By Contracted_LabelField = By.XPath("//*[@id='CWLabelHolder_contracthoursvalidationcontractedid']/label[text()='Contracted (Fixed and Variable)']");
        readonly By Contracted_MandatoryField = By.XPath("//*[@id='CWLabelHolder_contracthoursvalidationcontractedid']/label/span[@class='mandatory']");
        readonly By Contracted_PickList = By.Id("CWField_contracthoursvalidationcontractedid");

        readonly By Volunteer_LabelField = By.XPath("//*[@id='CWLabelHolder_contracthoursvalidationvolunteerid']/label[text()='Volunteer']");
        readonly By Volunteer_MandatoryField = By.XPath("//*[@id='CWLabelHolder_contracthoursvalidationvolunteerid']/label/span[@class='mandatory']");
        readonly By Volunteer_PickList = By.Id("CWField_contracthoursvalidationvolunteerid");

        #endregion

        #region Auto-Refresh

        readonly By AutoRefresh_LabelField = By.XPath("//*[@id='CWLabelHolder_autorefresh']/label[text()='Auto-Refresh']");
        readonly By AutoRefresh_MandatoryField = By.XPath("//*[@id='CWLabelHolder_autorefresh']/label/span[@class='mandatory']");
        readonly By AutoRefresh_YesOption = By.Id("CWField_autorefresh_1");
        readonly By AutoRefresh_NoOption = By.Id("CWField_autorefresh_0");

        readonly By AutoRefreshInterval_Seconds_LabelField = By.XPath("//*[@id='CWLabelHolder_autorefreshinterval']/label[text()='Auto-Refresh Interval (Seconds)']");
        readonly By AutoRefreshInterval_Seconds_MandatoryField = By.XPath("//*[@id='CWLabelHolder_autorefreshinterval']/label/span[@class='mandatory']");
        readonly By AutoRefreshInterval_Seconds_Field = By.Id("CWField_autorefreshinterval");

        #endregion

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");

        #region Drawer Popup

        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'mcc-iframe')][contains(@src,'type=cpschedulingsetup')]");

        readonly By EditpopupHeader = By.XPath("//*[@class='mcc-drawer__header']//h2");
        readonly By closeIcon = By.XPath("//button[@class='mcc-button mcc-button--sm mcc-button--ghost mcc-button--icon-only btn-close-drawer']");

        readonly By DrwaerCloseButton = By.Id("CWCloseDrawerButton");

        #endregion


        public CareProviderSchedulingSetupRecordPage WaitForCareProviderSchedulingSetupRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(CPSchedulingSetupFrame);
            SwitchToIframe(CPSchedulingSetupFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage WaitForCareProviderSchedulingSetupRecordPopupToLoad(string CPSName = "New")
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(EditpopupHeader);
            ValidateElementText(EditpopupHeader, "Care Provider Scheduling Setup: " + CPSName);

            WaitForElementVisible(closeIcon);
            WaitForElementVisible(DrwaerCloseButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(SaveButton);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            MoveToElementInPage(SaveButton);
            Click(SaveButton);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            MoveToElementInPage(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            MoveToElementInPage(BackButton);
            Click(BackButton);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateMessageAreaText()
        {
            WaitForElement(notificationMessage);
            Assert.AreEqual("Some data is not correct. Please review the data in the Form.", GetElementText(notificationMessage));

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateAllFieldsOfBookingsSection()
        {
            WaitForElementVisible(DefaultPublicHolidayForRegularBookings_LabelField);
            WaitForElementVisible(DefaultPublicHolidayForRegularBookings_PickList);
            MoveToElementInPage(DefaultPublicHolidayForRegularBookings_PickList);

            WaitForElementVisible(DefaultBookingTypeForPersonAbsence_LabelField);
            WaitForElementVisible(DefaultBookingTypeForPersonAbsence_LookupButton);
            MoveToElementInPage(DefaultBookingTypeForPersonAbsence_LookupButton);

            WaitForElementVisible(IgnoreInductionStatusForScheduling_LabelField);
            MoveToElementInPage(IgnoreInductionStatusForScheduling_LabelField);
            WaitForElementVisible(IgnoreInductionStatusForScheduling_YesOption);
            WaitForElementVisible(IgnoreInductionStatusForScheduling_NoOption);

            WaitForElementVisible(ValidForExport_LabelField);
            MoveToElementInPage(ValidForExport_LabelField);
            WaitForElementVisible(ValidForExport_YesOption);
            WaitForElementVisible(ValidForExport_NoOption);

            WaitForElementVisible(UpdateBookingEnd_Day_Date_Time_LabelField);
            MoveToElementInPage(UpdateBookingEnd_Day_Date_Time_LabelField);
            WaitForElementVisible(UpdateBookingEnd_Day_Date_Time_YesOption);
            WaitForElementVisible(UpdateBookingEnd_Day_Date_Time_NoOption);

            WaitForElementVisible(DefaultBookingTypePerStaffRoleType_LabelField);
            MoveToElementInPage(DefaultBookingTypePerStaffRoleType_LabelField);
            WaitForElementVisible(DefaultBookingTypePerStaffRoleType_YesOption);
            WaitForElementVisible(DefaultBookingTypePerStaffRoleType_NoOption);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateAllFieldsOfValidationSection()
        {
            WaitForElementVisible(BookingLengthUnit_mins_LabelField);
            MoveToElementInPage(BookingLengthUnit_mins_LabelField);
            WaitForElementVisible(BookingLengthUnit_mins_Field);

            WaitForElementVisible(CheckStaffAvailability_LabelField);
            MoveToElementInPage(CheckStaffAvailability_LabelField);
            WaitForElementVisible(CheckStaffAvailability_PickList);
            
            WaitForElementVisible(PlannedBookingPrecision_LabelField);
            MoveToElementInPage(PlannedBookingPrecision_LabelField);
            WaitForElementVisible(PlannedBookingPrecision_Field);
            WaitForElementVisible(PlannedBookingPrecision_Field);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateAllFieldsOfDiaryBookingsValidationSection()
        {
            WaitForElementVisible(UseBookingTypeClashActions_LabelField);
            MoveToElementInPage(UseBookingTypeClashActions_LabelField);
            ValidateElementText(UseBookingTypeClashActions_LabelField, "Use 'Booking Type: Clash Actions' Setting for Clashes with Schedule Bookings");
            WaitForElementVisible(UseBookingTypeClashActions_YesOption);
            WaitForElementVisible(UseBookingTypeClashActions_NoOption);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateAllFieldsOfContractHoursValidationSection()
        {
            WaitForElementVisible(Salaried_LabelField);
            MoveToElementInPage(Salaried_LabelField);
            WaitForElementVisible(Salaried_PickList);

            WaitForElementVisible(Hourly_LabelField);
            MoveToElementInPage(Hourly_LabelField);
            WaitForElementVisible(Hourly_PickList);

            WaitForElementVisible(ScheduledBookingWeeksToCheck_LabelField);
            MoveToElementInPage(ScheduledBookingWeeksToCheck_LabelField);
            WaitForElementVisible(ScheduledBookingWeeksToCheck_Field);

            WaitForElementVisible(Contracted_LabelField);
            MoveToElementInPage(Contracted_LabelField);
            WaitForElementVisible(Contracted_PickList);

            WaitForElementVisible(Volunteer_LabelField);
            MoveToElementInPage(Volunteer_LabelField);
            WaitForElementVisible(Volunteer_PickList);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateAllFieldsOfAutoRefreshSection()
        {
            WaitForElementVisible(AutoRefresh_LabelField);
            MoveToElementInPage(AutoRefresh_LabelField);
            WaitForElementVisible(AutoRefresh_YesOption);
            WaitForElementVisible(AutoRefresh_NoOption);

            WaitForElementVisible(AutoRefreshInterval_Seconds_LabelField);
            MoveToElementInPage(AutoRefreshInterval_Seconds_LabelField);
            WaitForElementVisible(AutoRefreshInterval_Seconds_Field);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ClickDefaultBookingTypeForPersonAbsenceLookupButton()
        {
            WaitForElementToBeClickable(DefaultBookingTypeForPersonAbsence_LookupButton);
            MoveToElementInPage(DefaultBookingTypeForPersonAbsence_LookupButton);
            Click(DefaultBookingTypeForPersonAbsence_LookupButton);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage SelectDefaultPublicHolidayForRegularBookings(string TextToSelect)
        {
            WaitForElementToBeClickable(DefaultPublicHolidayForRegularBookings_PickList) ;
            MoveToElementInPage(DefaultPublicHolidayForRegularBookings_PickList);
            SelectPicklistElementByText(DefaultPublicHolidayForRegularBookings_PickList, TextToSelect);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateSelectedDefaultPublicHolidayForRegularBookingsPickListValue(string expectedText)
        {
            WaitForElementVisible(DefaultPublicHolidayForRegularBookings_PickList);
            MoveToElementInPage(DefaultPublicHolidayForRegularBookings_PickList);
            ValidatePicklistSelectedText(DefaultPublicHolidayForRegularBookings_PickList, expectedText);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage SelectCheckStaffAvailability(string TextToSelect)
        {
            WaitForElementToBeClickable(CheckStaffAvailability_PickList);
            MoveToElementInPage(CheckStaffAvailability_PickList);
            SelectPicklistElementByText(CheckStaffAvailability_PickList, TextToSelect);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateSelectedCheckStaffAvailabilityPickListValue(string expectedText)
        {
            WaitForElementVisible(CheckStaffAvailability_PickList);
            MoveToElementInPage(CheckStaffAvailability_PickList);
            ValidatePicklistSelectedText(CheckStaffAvailability_PickList, expectedText);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage SelectSalaried(string TextToSelect)
        {
            WaitForElementToBeClickable(Salaried_PickList);
            MoveToElementInPage(Salaried_PickList);
            SelectPicklistElementByText(Salaried_PickList, TextToSelect);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateSelectedSalariedPickListValue(string expectedText)
        {
            WaitForElementVisible(Salaried_PickList);
            MoveToElementInPage(Salaried_PickList);
            ValidatePicklistSelectedText(Salaried_PickList, expectedText);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage SelectHourly(string TextToSelect)
        {
            WaitForElementToBeClickable(Hourly_PickList);
            MoveToElementInPage(Hourly_PickList);
            SelectPicklistElementByText(Hourly_PickList, TextToSelect);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateSelectedHourlyPickListValue(string expectedText)
        {
            WaitForElementVisible(Hourly_PickList);
            MoveToElementInPage(Hourly_PickList);
            ValidatePicklistSelectedText(Hourly_PickList, expectedText);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage SelectContracted(string TextToSelect)
        {
            WaitForElementToBeClickable(Contracted_PickList);
            MoveToElementInPage(Contracted_PickList);
            SelectPicklistElementByText(Contracted_PickList, TextToSelect);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateSelectedContractedPickListValue(string expectedText)
        {
            WaitForElementVisible(Contracted_PickList);
            MoveToElementInPage(Contracted_PickList);
            ValidatePicklistSelectedText(Contracted_PickList, expectedText);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage SelectVolunteer(string TextToSelect)
        {
            WaitForElementToBeClickable(Volunteer_PickList);
            MoveToElementInPage(Volunteer_PickList);
            SelectPicklistElementByText(Volunteer_PickList, TextToSelect);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateSelectedVolunteerPickListValue(string expectedText)
        {
            WaitForElementVisible(Volunteer_PickList);
            MoveToElementInPage(Volunteer_PickList);
            ValidatePicklistSelectedText(Volunteer_PickList, expectedText);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage InsertTextOnBookingLengthUnit_mins(string TextToInsert)
        {
            WaitForElementToBeClickable(BookingLengthUnit_mins_Field);
            MoveToElementInPage(BookingLengthUnit_mins_Field);
            SendKeys(BookingLengthUnit_mins_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage InsertTextOnScheduledBookingWeeksToCheck(string TextToInsert)
        {
            WaitForElementToBeClickable(ScheduledBookingWeeksToCheck_Field);
            MoveToElementInPage(ScheduledBookingWeeksToCheck_Field);
            SendKeys(ScheduledBookingWeeksToCheck_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage InsertTextOnAutoRefreshInterval_Seconds(string TextToInsert)
        {
            WaitForElementToBeClickable(AutoRefreshInterval_Seconds_Field);
            MoveToElementInPage(AutoRefreshInterval_Seconds_Field);
            SendKeys(AutoRefreshInterval_Seconds_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ClickIgnoreInductionStatusForScheduling_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(IgnoreInductionStatusForScheduling_YesOption) ;
                MoveToElementInPage(IgnoreInductionStatusForScheduling_YesOption);
                Click(IgnoreInductionStatusForScheduling_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(IgnoreInductionStatusForScheduling_NoOption);
                MoveToElementInPage(IgnoreInductionStatusForScheduling_NoOption);
                Click(IgnoreInductionStatusForScheduling_NoOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateIgnoreInductionStatusForScheduling_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(IgnoreInductionStatusForScheduling_YesOption);
            WaitForElementVisible(IgnoreInductionStatusForScheduling_NoOption);
            MoveToElementInPage(IgnoreInductionStatusForScheduling_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(IgnoreInductionStatusForScheduling_YesOption);
                ValidateElementNotChecked(IgnoreInductionStatusForScheduling_NoOption);
            }
            else
            {
                ValidateElementChecked(IgnoreInductionStatusForScheduling_NoOption);
                ValidateElementNotChecked(IgnoreInductionStatusForScheduling_YesOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ClickValidForExport_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(ValidForExport_YesOption);
                MoveToElementInPage(ValidForExport_YesOption);
                Click(ValidForExport_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(ValidForExport_NoOption);
                MoveToElementInPage(ValidForExport_NoOption);
                Click(ValidForExport_NoOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateValidForExport_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(ValidForExport_YesOption);
            WaitForElementVisible(ValidForExport_NoOption);
            MoveToElementInPage(ValidForExport_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(ValidForExport_YesOption);
                ValidateElementNotChecked(ValidForExport_NoOption);
            }
            else
            {
                ValidateElementChecked(ValidForExport_NoOption);
                ValidateElementNotChecked(ValidForExport_YesOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ClickUpdateBookingEnd_Day_Date_Time_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(UpdateBookingEnd_Day_Date_Time_YesOption);
                MoveToElementInPage(UpdateBookingEnd_Day_Date_Time_YesOption);
                Click(UpdateBookingEnd_Day_Date_Time_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(UpdateBookingEnd_Day_Date_Time_NoOption);
                MoveToElementInPage(UpdateBookingEnd_Day_Date_Time_NoOption);
                Click(UpdateBookingEnd_Day_Date_Time_NoOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateUpdateBookingEnd_Day_Date_Time_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(UpdateBookingEnd_Day_Date_Time_YesOption);
            WaitForElementVisible(UpdateBookingEnd_Day_Date_Time_NoOption);
            MoveToElementInPage(UpdateBookingEnd_Day_Date_Time_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(UpdateBookingEnd_Day_Date_Time_YesOption);
                ValidateElementNotChecked(UpdateBookingEnd_Day_Date_Time_NoOption);
            }
            else
            {
                ValidateElementChecked(UpdateBookingEnd_Day_Date_Time_NoOption);
                ValidateElementNotChecked(UpdateBookingEnd_Day_Date_Time_YesOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ClickDefaultBookingTypePerStaffRoleType_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(DefaultBookingTypePerStaffRoleType_YesOption);
                MoveToElementInPage(DefaultBookingTypePerStaffRoleType_YesOption);
                Click(DefaultBookingTypePerStaffRoleType_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(DefaultBookingTypePerStaffRoleType_NoOption);
                MoveToElementInPage(DefaultBookingTypePerStaffRoleType_NoOption);
                Click(DefaultBookingTypePerStaffRoleType_NoOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateDefaultBookingTypePerStaffRoleType_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(DefaultBookingTypePerStaffRoleType_YesOption);
            WaitForElementVisible(DefaultBookingTypePerStaffRoleType_NoOption);
            MoveToElementInPage(DefaultBookingTypePerStaffRoleType_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(DefaultBookingTypePerStaffRoleType_YesOption);
                ValidateElementNotChecked(DefaultBookingTypePerStaffRoleType_NoOption);
            }
            else
            {
                ValidateElementChecked(DefaultBookingTypePerStaffRoleType_NoOption);
                ValidateElementNotChecked(DefaultBookingTypePerStaffRoleType_YesOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ClickUseBookingTypeClashActionsSettingForClashesWithScheduleBookings_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(UseBookingTypeClashActions_YesOption);
                MoveToElementInPage(UseBookingTypeClashActions_YesOption);
                Click(UseBookingTypeClashActions_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(UseBookingTypeClashActions_NoOption);
                MoveToElementInPage(UseBookingTypeClashActions_NoOption);
                Click(UseBookingTypeClashActions_NoOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateUseBookingTypeClashActionsSettingForClashesWithScheduleBookings_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(UseBookingTypeClashActions_YesOption);
            WaitForElementVisible(UseBookingTypeClashActions_NoOption);
            MoveToElementInPage(UseBookingTypeClashActions_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(UseBookingTypeClashActions_YesOption);
                ValidateElementNotChecked(UseBookingTypeClashActions_NoOption);
            }
            else
            {
                ValidateElementChecked(UseBookingTypeClashActions_NoOption);
                ValidateElementNotChecked(UseBookingTypeClashActions_YesOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ClickAutoRefresh_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(AutoRefresh_YesOption);
                MoveToElementInPage(AutoRefresh_YesOption);
                Click(AutoRefresh_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(AutoRefresh_NoOption);
                MoveToElementInPage(AutoRefresh_NoOption);
                Click(AutoRefresh_NoOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateAutoRefresh_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(AutoRefresh_YesOption);
            WaitForElementVisible(AutoRefresh_NoOption);
            MoveToElementInPage(AutoRefresh_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(AutoRefresh_YesOption);
                ValidateElementNotChecked(AutoRefresh_NoOption);
            }
            else
            {
                ValidateElementChecked(AutoRefresh_NoOption);
                ValidateElementNotChecked(AutoRefresh_YesOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateDefaultPublicHolidayForRegularBookingsMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(DefaultPublicHolidayForRegularBookings_LabelField) ;
            MoveToElementInPage(DefaultPublicHolidayForRegularBookings_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(DefaultPublicHolidayForRegularBookings_MandatoryField);
            else
                WaitForElementNotVisible(DefaultPublicHolidayForRegularBookings_MandatoryField, 3);

            return this;
        }

        //Validate Planned Booking Precision Mandatory Field Visibility
        public CareProviderSchedulingSetupRecordPage ValidatePlannedBookingPrecisionMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(PlannedBookingPrecision_LabelField);
            MoveToElementInPage(PlannedBookingPrecision_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(PlannedBookingPrecision_MandatoryField);
            else
                WaitForElementNotVisible(PlannedBookingPrecision_MandatoryField, 3);

            return this;
        }

        //validate Planned Booking Precision Text Field Value
        public CareProviderSchedulingSetupRecordPage ValidatePlannedBookingPrecisionTextFieldValue(string expectedText)
        {
            WaitForElementVisible(PlannedBookingPrecision_Field);
            MoveToElementInPage(PlannedBookingPrecision_Field);
            ValidateElementValueByJavascript(PlannedBookingPrecisionFieldInputId, expectedText);

            return this;
        }

        //validate Planned Booking Precision field is disabled
        public CareProviderSchedulingSetupRecordPage ValidatePlannedBookingPrecisionFieldIsDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(PlannedBookingPrecision_Field);
            MoveToElementInPage(PlannedBookingPrecision_Field);

            if(ExpectedDisabled)
                ValidateElementDisabled(PlannedBookingPrecision_Field);
            else
                ValidateElementNotDisabled(PlannedBookingPrecision_Field);


            return this;
        }

        public CareProviderSchedulingSetupRecordPage ClickDrawerCloseButton()
        {
            WaitForElementToBeClickable(DrwaerCloseButton);
            MoveToElementInPage(DrwaerCloseButton);
            Click(DrwaerCloseButton);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateDoubleBookingActionFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(DoubleBookingAction_LabelField);
                MoveToElementInPage(DoubleBookingAction_LabelField);
                ValidateElementText(DoubleBookingAction_LabelField, "Double-Booking Action*");
                WaitForElementVisible(DoubleBookingAction_PickList);
            }
            else
            {
                WaitForElementNotVisible(DoubleBookingAction_LabelField, 3);
                WaitForElementNotVisible(DoubleBookingAction_PickList, 3);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateDoubleBookingActionMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(DoubleBookingAction_LabelField);
            MoveToElementInPage(DoubleBookingAction_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(DoubleBookingAction_MandatoryField);
            else
                WaitForElementNotVisible(DoubleBookingAction_MandatoryField, 3);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage SelectDoubleBookingAction(string TextToSelect)
        {
            WaitForElementToBeClickable(DoubleBookingAction_PickList);
            MoveToElementInPage(DoubleBookingAction_PickList);
            SelectPicklistElementByText(DoubleBookingAction_PickList, TextToSelect);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateSelectedDoubleBookingActionPickListText(string expectedText)
        {
            WaitForElementVisible(DoubleBookingAction_PickList);
            MoveToElementInPage(DoubleBookingAction_PickList);
            ValidatePicklistSelectedText(DoubleBookingAction_PickList, expectedText);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ClickDoubleBookingAction()
        {
            WaitForElementToBeClickable(DoubleBookingAction_PickList);
            MoveToElementInPage(DoubleBookingAction_PickList);
            Click(DoubleBookingAction_PickList);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateDoubleBookingActionFieldOptionIsPresent(string text)
        {
            WaitForElementVisible(DoubleBookingAction_PickList);
            ValidatePicklistContainsElementByText(DoubleBookingAction_PickList, text);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateDoubleBookingActionErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(DoubleBookingAction_FieldErrorLabel);
            MoveToElementInPage(DoubleBookingAction_FieldErrorLabel);
            ValidateElementText(DoubleBookingAction_FieldErrorLabel, ExpectedText);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage MouseHoverOnYesOptionOfUseBookingTypeClashActionsSettingForClashesWithScheduleBookings()
        {
            WaitForElementToBeClickable(UseBookingTypeClashActions_YesOption);
            MoveToElementInPage(UseBookingTypeClashActions_YesOption);
            MouseHover(UseBookingTypeClashActions_YesOption);
            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage MouseHoverOnNoOptionOfUseBookingTypeClashActionsSettingForClashesWithScheduleBookings()
        {
            WaitForElementToBeClickable(UseBookingTypeClashActions_NoOption);
            MoveToElementInPage(UseBookingTypeClashActions_NoOption);
            MouseHover(UseBookingTypeClashActions_NoOption);
            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateYesOptionToolTipText(string ExpectedText)
        {
            MoveToElementInPage(UseBookingTypeClashActions_YesOptionToolTip);
            System.Threading.Thread.Sleep(1000);
            ValidateElementByTitle(UseBookingTypeClashActions_YesOptionToolTip, ExpectedText);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateNoOptionToolTipText(string ExpectedText)
        {
            MoveToElementInPage(UseBookingTypeClashActions_NoOptionToolTip);
            System.Threading.Thread.Sleep(1000);
            ValidateElementByTitle(UseBookingTypeClashActions_NoOptionToolTip, ExpectedText);

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateDeleteReasonRequiredSchedule_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(DeleteReasonRequiredSchedule_YesOption);
            WaitForElementVisible(DeleteReasonRequiredSchedule_NoOption);
            MoveToElementInPage(DeleteReasonRequiredSchedule_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(DeleteReasonRequiredSchedule_YesOption);
                ValidateElementNotChecked(DeleteReasonRequiredSchedule_NoOption);
            }
            else
            {
                ValidateElementChecked(DeleteReasonRequiredSchedule_NoOption);
                ValidateElementNotChecked(DeleteReasonRequiredSchedule_YesOption);
            }

            return this;
        }

        public CareProviderSchedulingSetupRecordPage ValidateDeleteReasonRequiredDiary_OptionIsCheckedOrNot(bool YesOptionIsChecked)
        {
            WaitForElementVisible(DeleteReasonRequiredDiary_YesOption);
            WaitForElementVisible(DeleteReasonRequiredDiary_NoOption);
            MoveToElementInPage(DeleteReasonRequiredDiary_NoOption);

            if (YesOptionIsChecked)
            {
                ValidateElementChecked(DeleteReasonRequiredDiary_YesOption);
                ValidateElementNotChecked(DeleteReasonRequiredDiary_NoOption);
            }
            else
            {
                ValidateElementChecked(DeleteReasonRequiredDiary_NoOption);
                ValidateElementNotChecked(DeleteReasonRequiredDiary_YesOption);
            }

            return this;
        }

    }
}
