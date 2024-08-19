using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonContractRecordPage : CommonMethods
    {
        public PersonContractRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersoncontract')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By PinToMeButton = By.XPath("//*[@id='TI_PinToMeButton']");


        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By auditSubMenuLink = By.XPath("//*[@id='CWNavItem_AuditHistory']");


        readonly By DetailsTab = By.XPath("//*[@id='CWNavGroup_EditForm']/a");
        readonly By PersonAbsenceTab = By.XPath("//*[@id='CWNavGroup_PersonAbsence']/a");
        readonly By FinanceTransactionTab = By.XPath("//*[@id='CWNavGroup_CareProviderFinanceTransaction']/a");
        readonly By PersonContractServiceTab = By.XPath("//*[@id='CWNavGroup_PersonContractService']/a");
        readonly By CWUrlPanelIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By iframe_CWDialogService = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersoncontractservice')]");
        readonly By PersonContractServiceRateTab = By.XPath("//*[@id='CWNavGroup_Rates']/a");
        readonly By RateUnit = By.XPath("//*[@id='CWField_careproviderrateunitid_Link']");
        readonly By ResponsibleTeam = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By PersonContractService = By.XPath("//*[@id='CWField_careproviderpersoncontractserviceid_Link']");
        readonly By iframe_CWDialogServiceRatePeriod = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersoncontractservicerateperiod')]");
        readonly By EndDate = By.XPath("//*[@id='CWField_enddate']");
        readonly By Rate = By.XPath("//*[@id='CWField_rate']");
        readonly By ChargeApportionmentTab = By.XPath("//*[@id='CWNavGroup_CareProviderChargeApportionment']/a");

        readonly By NotificationArea = By.XPath("//*[@id='CWNotificationHolder_DataForm']");


        readonly By GeneralSectionTitle = By.XPath("//*[@id='CWSection_General']//span[text()='General']");
        readonly By Careproviderpersoncontractnumber = By.XPath("//*[@id='CWField_careproviderpersoncontractnumber']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
        readonly By SystemuseridLink = By.XPath("//*[@id='CWField_systemuserid_Link']");
        readonly By SystemuseridClearButton = By.XPath("//*[@id='CWClearLookup_systemuserid']");
        readonly By SystemuseridLookupButton = By.XPath("//*[@id='CWLookupBtn_systemuserid']");

        readonly By SchemeSectionTitle = By.XPath("//*[@id='CWSection_Scheme']//span[text()='Scheme']");
        readonly By ProviderestablishmentidLink = By.XPath("//*[@id='CWField_providerestablishmentid_Link']");
        readonly By ProviderestablishmentidClearButton = By.XPath("//*[@id='CWClearLookup_providerestablishmentid']");
        readonly By ProviderestablishmentidLookupButton = By.XPath("//*[@id='CWLookupBtn_providerestablishmentid']");
        readonly By CareprovidercontractschemeidLink = By.XPath("//*[@id='CWField_careprovidercontractschemeid_Link']");
        readonly By CareprovidercontractschemeidClearButton = By.XPath("//*[@id='CWClearLookup_careprovidercontractschemeid']");
        readonly By CareprovidercontractschemeidLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidercontractschemeid']");
        readonly By ProviderfunderidLink = By.XPath("//*[@id='CWField_providerfunderid_Link']");
        readonly By ProviderfunderidLookupButton = By.XPath("//*[@id='CWLookupBtn_providerfunderid']");
        readonly By Pcisenabledforscheduledbookings_FieldLabel = By.XPath("//*[@id='CWLabelHolder_pcisenabledforscheduledbookings']/label");
        readonly By Pcisenabledforscheduledbookings_1 = By.XPath("//*[@id='CWField_pcisenabledforscheduledbookings_1']");
        readonly By Pcisenabledforscheduledbookings_0 = By.XPath("//*[@id='CWField_pcisenabledforscheduledbookings_0']");

        readonly By DatesSectionTitle = By.XPath("//*[@id='CWSection_Dates']//span[text()='Dates']");
        readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
        readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
        readonly By Enddatetime = By.XPath("//*[@id='CWField_enddatetime']");
        readonly By EnddatetimeDatePicker = By.XPath("//*[@id='CWField_enddatetime_DatePicker']");
        readonly By Enddatetime_Time = By.XPath("//*[@id='CWField_enddatetime_Time']");
        readonly By Enddatetime_Time_TimePicker = By.XPath("//*[@id='CWField_enddatetime_Time_TimePicker']");
        readonly By CareproviderpersoncontractendreasonidLink = By.XPath("//*[@id='CWField_careproviderpersoncontractendreasonid_Link']");
        readonly By CareproviderpersoncontractendreasonidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderpersoncontractendreasonid']");
        readonly By Notifiedofendingdatetime = By.XPath("//*[@id='CWField_notifiedofendingdatetime']");
        readonly By NotifiedofendingdatetimeDatePicker = By.XPath("//*[@id='CWField_notifiedofendingdatetime_DatePicker']");
        readonly By Notifiedofendingdatetime_Time = By.XPath("//*[@id='CWField_notifiedofendingdatetime_Time']");
        readonly By Notifiedofendingdatetime_Time_TimePicker = By.XPath("//*[@id='CWField_notifiedofendingdatetime_Time_TimePicker']");

        readonly By NotesSectionTitle = By.XPath("//*[@id='CWSection_Notes']//span[text()='Notes']");
        readonly By Notetextid = By.XPath("//*[@id='CWField_notetextid']");
        readonly By ServiceLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderserviceid']");
        readonly By StartdateService = By.XPath("//*[@id='CWField_startdatetime']");
        readonly By OverrideRate_YesOption = By.Id("CWField_isoverriderate_1");
        readonly By OverrideRate_NoOption = By.Id("CWField_isoverriderate_0");
        readonly By PersonContractServiceRatesTab = By.XPath("//*[@id='CWNavGroup_Rates']/a");
       
        readonly By BankHolidayChargingCalendarId = By.XPath("//*[@id='CWLookupBtn_cpbankholidaychargingcalendarid']");
        readonly By BankHolidayRate = By.XPath("//*[@id='CWField_bankholidayrate']");
        readonly By BankHolidayChargingCalendarLink = By.XPath("//*[@id='CWField_cpbankholidaychargingcalendarid_Link']");
        readonly By StartDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/span[1]");
        readonly By EndDateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/a/span");
        readonly By RateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/a/span");
        readonly By BankHolidayRateHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]/a/span");
        readonly By RateUnitHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]/a/span");
        readonly By TimeBandStartHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[7]/a/span");
        readonly By TimeBandEndHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[8]/a/span");
        readonly By SelectAllDaysHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[9]/a/span");
        readonly By PersonContractServiceRatePeriodHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By RateUnitLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderrateunitid']");
        readonly By PersonContractServiceLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderpersoncontractserviceid']");
        readonly By RateValidationMeesage = By.XPath("//*[@id='CWControlHolder_rate']/label/span");
        readonly By PersonContractServiceLookup = By.XPath("//*[@id='CWLookupBtn_careproviderpersoncontractserviceid']");

        readonly By iframe_CWDataFormDialogServiceRatePeriod = By.XPath("//iframe[contains(@id,'iframe_CWDataFormDialog')][contains(@src,'type=careproviderpersoncontractservicerateperiod')]");
        readonly By ChargePerWeekTab = By.XPath("//*[@id='CWNavGroup_ChargePerWeek']/a");
        readonly By Status_Field = By.Id("CWField_personcontractservicestatusid");
        readonly By IsRateVerified_1 = By.XPath("//*[@id='CWField_israteverified_1']");
        readonly By IsOverrideRate_1 = By.XPath("//*[@id='CWField_isoverriderate_1']");
        readonly By iframe_CWDialogPerson = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person')]");
        readonly By PersonContractServiceLookUp = By.XPath("//*[@id='CWLookupBtn_careproviderpersoncontractserviceid']");



        public PersonContractRecordPage WaitForPersonContractRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElement(pageHeader);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);

            WaitForElement(GeneralSectionTitle);
            WaitForElement(SchemeSectionTitle);
            WaitForElement(DatesSectionTitle);
            WaitForElement(NotesSectionTitle);

            WaitForElement(Careproviderpersoncontractnumber);

            return this;
        }

        public PersonContractRecordPage ValidatePageHeaderText(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            ValidateElementText(pageHeader, ExpectedText);

            return this;
        }

        public PersonContractRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(auditSubMenuLink);
            Click(auditSubMenuLink);

            return this;
        }

        public PersonContractRecordPage ClickPersonAbsenceTab()
        {
            WaitForElementToBeClickable(PersonAbsenceTab);
            Click(PersonAbsenceTab);

            return this;
        }

        public PersonContractRecordPage ClickFinanceTransactionTab()
        {
            WaitForElementToBeClickable(FinanceTransactionTab);
            Click(FinanceTransactionTab);

            return this;
        }

        public PersonContractRecordPage ClickDetailsTab()
        {
            WaitForElementToBeClickable(DetailsTab);
            Click(DetailsTab);

            return this;
        }


        public PersonContractRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public PersonContractRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonContractRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonContractRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public PersonContractRecordPage ClickPinToMeButton()
        {
            WaitForElementToBeClickable(PinToMeButton);
            Click(PinToMeButton);

            return this;
        }


        public PersonContractRecordPage ValidateNotificationAreaVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NotificationArea);
            else
                WaitForElementNotVisible(NotificationArea, 3);

            return this;
        }

        public PersonContractRecordPage ValidateNotificationAreaText(string ExpectedText)
        {
            ValidateElementText(NotificationArea, ExpectedText);

            return this;
        }



        public PersonContractRecordPage ValidateIdText(string ExpectedText)
        {
            ValidateElementValue(Careproviderpersoncontractnumber, ExpectedText);

            return this;
        }

        public PersonContractRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public PersonContractRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            if (!string.IsNullOrEmpty(ExpectedText))
            {
                WaitForElementToBeClickable(ResponsibleTeamLink);
                ValidateElementText(ResponsibleTeamLink, ExpectedText);
            }
            else
            {
                WaitForElement(ResponsibleTeamLink);
                ValidateElementText(ResponsibleTeamLink, ExpectedText);
            }

            return this;
        }

        public PersonContractRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public PersonContractRecordPage ClickPersonLink()
        {
            WaitForElementToBeClickable(PersonidLink);
            Click(PersonidLink);

            return this;
        }

        public PersonContractRecordPage ValidatePersonLinkFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(PersonidLink);
            else
                WaitForElementNotVisible(PersonidLink, 3);

            return this;
        }

        public PersonContractRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonidLink);
            ValidateElementText(PersonidLink, ExpectedText);

            return this;
        }

        public PersonContractRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        public PersonContractRecordPage ClickResponsibleUserLink()
        {
            WaitForElementToBeClickable(SystemuseridLink);
            Click(SystemuseridLink);

            return this;
        }

        public PersonContractRecordPage ValidateResponsibleUserLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(SystemuseridLink);
            ValidateElementText(SystemuseridLink, ExpectedText);

            return this;
        }

        public PersonContractRecordPage ClickResponsibleUserClearButton()
        {
            WaitForElementToBeClickable(SystemuseridClearButton);
            Click(SystemuseridClearButton);

            return this;
        }

        public PersonContractRecordPage ClickResponsibleUserLookupButton()
        {
            WaitForElementToBeClickable(SystemuseridLookupButton);
            Click(SystemuseridLookupButton);

            return this;
        }



        public PersonContractRecordPage ClickEstablishmentLink()
        {
            WaitForElementToBeClickable(ProviderestablishmentidLink);
            Click(ProviderestablishmentidLink);

            return this;
        }

        public PersonContractRecordPage ValidateEstablishmentLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ProviderestablishmentidLink);
            ValidateElementText(ProviderestablishmentidLink, ExpectedText);

            return this;
        }

        public PersonContractRecordPage ValidateEstablishmentLinkTextVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ProviderestablishmentidLink);
            else
                WaitForElementNotVisible(ProviderestablishmentidLink, 3);

            return this;
        }

        public PersonContractRecordPage ClickEstablishmentClearButton()
        {
            WaitForElementToBeClickable(ProviderestablishmentidClearButton);
            Click(ProviderestablishmentidClearButton);

            return this;
        }

        public PersonContractRecordPage ClickEstablishmentLookupButton()
        {
            WaitForElementToBeClickable(ProviderestablishmentidLookupButton);
            Click(ProviderestablishmentidLookupButton);

            return this;
        }

        public PersonContractRecordPage ValidateEstablishmentLookupButtonDisabled(bool ExpectDisabled)
        {
            WaitForElement(ProviderestablishmentidLookupButton);

            if (ExpectDisabled)
                ValidateElementDisabled(ProviderestablishmentidLookupButton);
            else
                ValidateElementNotDisabled(ProviderestablishmentidLookupButton);

            return this;
        }

        public PersonContractRecordPage ClickContractSchemeLink()
        {
            WaitForElementToBeClickable(CareprovidercontractschemeidLink);
            Click(CareprovidercontractschemeidLink);

            return this;
        }

        public PersonContractRecordPage ValidateContractSchemeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareprovidercontractschemeidLink);
            ValidateElementText(CareprovidercontractschemeidLink, ExpectedText);

            return this;
        }

        public PersonContractRecordPage ValidateContractSchemeLinkTextVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(CareprovidercontractschemeidLink);
            else
                WaitForElementNotVisible(CareprovidercontractschemeidLink, 3);

            return this;
        }

        public PersonContractRecordPage ClickContractSchemeClearButton()
        {
            WaitForElementToBeClickable(CareprovidercontractschemeidClearButton);
            Click(CareprovidercontractschemeidClearButton);

            return this;
        }

        public PersonContractRecordPage ClickContractSchemeLookupButton()
        {
            WaitForElementToBeClickable(CareprovidercontractschemeidLookupButton);
            Click(CareprovidercontractschemeidLookupButton);

            return this;
        }

        public PersonContractRecordPage ValidateContractSchemeLookupButtonDisabled(bool ExpectDisabled)
        {
            WaitForElement(CareprovidercontractschemeidLookupButton);

            if (ExpectDisabled)
                ValidateElementDisabled(CareprovidercontractschemeidLookupButton);
            else
                ValidateElementNotDisabled(CareprovidercontractschemeidLookupButton);

            return this;
        }

        public PersonContractRecordPage ClickFunderLink()
        {
            WaitForElementToBeClickable(ProviderfunderidLink);
            Click(ProviderfunderidLink);

            return this;
        }

        public PersonContractRecordPage ValidateFunderLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ProviderfunderidLink);
            ValidateElementText(ProviderfunderidLink, ExpectedText);

            return this;
        }

        public PersonContractRecordPage ValidateFunderLinkTextVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ProviderfunderidLink);
            else
                WaitForElementNotVisible(ProviderfunderidLink, 3);

            return this;
        }

        public PersonContractRecordPage ClickFunderLookupButton()
        {
            WaitForElementToBeClickable(ProviderfunderidLookupButton);
            Click(ProviderfunderidLookupButton);

            return this;
        }

        public PersonContractRecordPage ValidateFunderLookupButtonDisabled(bool ExpectDisabled)
        {
            WaitForElement(ProviderfunderidLookupButton);

            if (ExpectDisabled)
                ValidateElementDisabled(ProviderfunderidLookupButton);
            else
                ValidateElementNotDisabled(ProviderfunderidLookupButton);

            return this;
        }

        public PersonContractRecordPage ValidatePersonContractIsEnabledForScheduledBookings_FieldLabelTooltip(string ExpectedTooltip)
        {
            WaitForElement(Pcisenabledforscheduledbookings_FieldLabel);
            ValidateElementAttribute(Pcisenabledforscheduledbookings_FieldLabel, "title", ExpectedTooltip);

            return this;
        }

        public PersonContractRecordPage ClickPersonContractIsEnabledForScheduledBookings_YesRadioButton()
        {
            WaitForElementToBeClickable(Pcisenabledforscheduledbookings_1);
            Click(Pcisenabledforscheduledbookings_1);

            return this;
        }

        public PersonContractRecordPage ValidatePersonContractIsEnabledForScheduledBookings_YesRadioButtonChecked()
        {
            WaitForElementVisible(Pcisenabledforscheduledbookings_1);
            ScrollToElement(Pcisenabledforscheduledbookings_1);
            ValidateElementCheckedByJavascript("CWField_pcisenabledforscheduledbookings_1", true);

            return this;
        }

        public PersonContractRecordPage ValidatePersonContractIsEnabledForScheduledBookings_YesRadioButtonNotChecked()
        {
            WaitForElement(Pcisenabledforscheduledbookings_1);
            ValidateElementNotChecked(Pcisenabledforscheduledbookings_1);

            return this;
        }

        public PersonContractRecordPage ClickPersonContractIsEnabledForScheduledBookings_NoRadioButton()
        {
            WaitForElementToBeClickable(Pcisenabledforscheduledbookings_0);
            Click(Pcisenabledforscheduledbookings_0);

            return this;
        }

        public PersonContractRecordPage ValidatePersonContractIsEnabledForScheduledBookings_NoRadioButtonChecked()
        {
            WaitForElement(Pcisenabledforscheduledbookings_0);
            ValidateElementChecked(Pcisenabledforscheduledbookings_0);

            return this;
        }

        public PersonContractRecordPage ValidatePersonContractIsEnabledForScheduledBookings_NoRadioButtonNotChecked()
        {
            WaitForElement(Pcisenabledforscheduledbookings_0);
            ValidateElementNotChecked(Pcisenabledforscheduledbookings_0);

            return this;
        }

        public PersonContractRecordPage ValidatePersonContractIsEnabledForScheduledBookingsDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Pcisenabledforscheduledbookings_1);
                ValidateElementDisabled(Pcisenabledforscheduledbookings_0);
            }
            else
            {
                ValidateElementNotDisabled(Pcisenabledforscheduledbookings_1);
                ValidateElementNotDisabled(Pcisenabledforscheduledbookings_0);
            }

            return this;
        }



        public PersonContractRecordPage ValidateStartDateText(string ExpectedText)
        {
            ValidateElementValue(Startdate, ExpectedText);

            return this;
        }

        public PersonContractRecordPage InsertTextOnStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Startdate);
            SendKeys(Startdate, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractRecordPage ClickStartDateDatePicker()
        {
            WaitForElementToBeClickable(StartdateDatePicker);
            Click(StartdateDatePicker);

            return this;
        }

        public PersonContractRecordPage ValidateEndDateFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Enddatetime);
                WaitForElementVisible(Enddatetime_Time);
                WaitForElementVisible(EnddatetimeDatePicker);
                WaitForElementVisible(Enddatetime_Time_TimePicker);
            }
            else
            {
                WaitForElementNotVisible(Enddatetime, 4);
                WaitForElementNotVisible(Enddatetime_Time, 4);
                WaitForElementNotVisible(EnddatetimeDatePicker, 4);
                WaitForElementNotVisible(Enddatetime_Time_TimePicker, 4);
            }

            return this;
        }

        public PersonContractRecordPage ValidateEndDateTimeFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Enddatetime);
                ValidateElementDisabled(EnddatetimeDatePicker);
                ValidateElementDisabled(Enddatetime_Time);
                ValidateElementDisabled(Enddatetime_Time_TimePicker);
            }
            else
            {
                ValidateElementNotDisabled(Enddatetime);
                ValidateElementNotDisabled(EnddatetimeDatePicker);
                ValidateElementNotDisabled(Enddatetime_Time);
                ValidateElementNotDisabled(Enddatetime_Time_TimePicker);
            }

            return this;
        }

        public PersonContractRecordPage ValidateEndDateTimeText(string ExpectedText)
        {
            ValidateElementValue(Enddatetime, ExpectedText);

            return this;
        }

        public PersonContractRecordPage InsertTextOnEndDateTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Enddatetime);
            SendKeys(Enddatetime, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractRecordPage ClickEndDateTimeDatePicker()
        {
            WaitForElementToBeClickable(EnddatetimeDatePicker);
            Click(EnddatetimeDatePicker);

            return this;
        }

        public PersonContractRecordPage ValidateEndDateTime_TimeText(string ExpectedText)
        {
            ValidateElementValue(Enddatetime_Time, ExpectedText);

            return this;
        }

        public PersonContractRecordPage InsertTextOnEndDateTime_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Enddatetime_Time);
            SendKeys(Enddatetime_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractRecordPage ClickEndDateTime_TimePicker()
        {
            WaitForElementToBeClickable(Enddatetime_Time_TimePicker);
            Click(Enddatetime_Time_TimePicker);

            return this;
        }

        public PersonContractRecordPage ValidateEndReasonFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CareproviderpersoncontractendreasonidLink);
                WaitForElementVisible(CareproviderpersoncontractendreasonidLookupButton);
            }
            else
            {
                WaitForElementNotVisible(CareproviderpersoncontractendreasonidLink, 4);
                WaitForElementNotVisible(CareproviderpersoncontractendreasonidLookupButton, 4);
            }

            return this;
        }

        public PersonContractRecordPage ClickEndReasonLink()
        {
            WaitForElementToBeClickable(CareproviderpersoncontractendreasonidLink);
            Click(CareproviderpersoncontractendreasonidLink);

            return this;
        }

        public PersonContractRecordPage ValidateEndReasonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareproviderpersoncontractendreasonidLink);
            ValidateElementText(CareproviderpersoncontractendreasonidLink, ExpectedText);

            return this;
        }

        public PersonContractRecordPage ClickEndReasonLookupButton()
        {
            WaitForElementToBeClickable(CareproviderpersoncontractendreasonidLookupButton);
            Click(CareproviderpersoncontractendreasonidLookupButton);

            return this;
        }

        public PersonContractRecordPage ValidateEndReasonLookupButtonDisabled(bool ExpectDisable)
        {
            if (ExpectDisable)
                ValidateElementDisabled(CareproviderpersoncontractendreasonidLookupButton);
            else
                ValidateElementNotDisabled(CareproviderpersoncontractendreasonidLookupButton);

            return this;
        }

        public PersonContractRecordPage ValidateNotifiedOfEndingDateTimeText(string ExpectedText)
        {
            ValidateElementValue(Notifiedofendingdatetime, ExpectedText);

            return this;
        }

        public PersonContractRecordPage InsertTextOnNotifiedOfEndingDateTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Notifiedofendingdatetime);
            SendKeys(Notifiedofendingdatetime, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractRecordPage ClickNotifiedOfEndingDateTimeDatePicker()
        {
            WaitForElementToBeClickable(NotifiedofendingdatetimeDatePicker);
            Click(NotifiedofendingdatetimeDatePicker);

            return this;
        }

        public PersonContractRecordPage ValidateNotifiedOfEndingDateTime_TimeText(string ExpectedText)
        {
            ValidateElementValue(Notifiedofendingdatetime_Time, ExpectedText);

            return this;
        }

        public PersonContractRecordPage InsertTextOnNotifiedOfEndingDateTime_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Notifiedofendingdatetime_Time);
            SendKeys(Notifiedofendingdatetime_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractRecordPage ClickNotifiedOfEndingDateTime_TimePicker()
        {
            WaitForElementToBeClickable(Notifiedofendingdatetime_Time_TimePicker);
            Click(Notifiedofendingdatetime_Time_TimePicker);

            return this;
        }



        public PersonContractRecordPage ValidateNoteTextText(string ExpectedText)
        {
            ValidateElementText(Notetextid, ExpectedText);

            return this;
        }

        public PersonContractRecordPage InsertTextOnNoteText(string TextToInsert)
        {
            WaitForElementToBeClickable(Notetextid);
            SendKeys(Notetextid, TextToInsert + Keys.Tab);

            return this;
        }
        public PersonContractRecordPage ClickPersonContractServicesTab()
        {
            WaitForElementToBeClickable(PersonContractServiceTab);
            Click(PersonContractServiceTab);

            return this;

        }

        public PersonContractRecordPage WaitForPersonContractServicePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWUrlPanelIFrame);
            SwitchToIframe(CWUrlPanelIFrame);

            return this;
        }

        public PersonContractRecordPage ClickServiceLookupButton()
        {
            WaitForElementToBeClickable(ServiceLookupButton);
            Click(ServiceLookupButton);

            return this;
        }

        public PersonContractRecordPage WaitForPersonContractServiceRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialogService);
            SwitchToIframe(iframe_CWDialogService);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(SaveAndCloseButton);
            return this;
        }

        public PersonContractRecordPage InsertTextOnStartDateForService(string TextToInsert)
        {
            WaitForElementToBeClickable(StartdateService);
            SendKeys(StartdateService, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractRecordPage ClickOverrideRate_Option(bool YesOption)
        {
            if (YesOption)
            {
                WaitForElementToBeClickable(OverrideRate_YesOption);
                ScrollToElement(OverrideRate_YesOption);
                Click(OverrideRate_YesOption);
            }
            else
            {
                WaitForElementToBeClickable(OverrideRate_NoOption);
                ScrollToElement(OverrideRate_NoOption);
                Click(OverrideRate_NoOption);
            }



            return this;
        }

        public PersonContractRecordPage ClickPersonContractServiceRatesTab()
        {
            WaitForElementToBeClickable(PersonContractServiceRateTab);
            Click(PersonContractServiceRateTab);

            return this;
        }

        public PersonContractRecordPage VerifyPersonContractServiceRatesTabNotVisible()
        {
            WaitForElementNotVisible(PersonContractServiceTab, 60);

            return this;
        }

        public PersonContractRecordPage WaitForPersonContractServiceRatesTabToLoad()
        {

            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialogService);
            SwitchToIframe(iframe_CWDialogService);

            WaitForElement(CWUrlPanelIFrame);
            SwitchToIframe(CWUrlPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);
            WaitForElement(pageHeader);

            WaitForElementVisible(pageHeader);

            return this;
        }
        public PersonContractRecordPage WaitForPersonContractServiceRatePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialogServiceRatePeriod);
            SwitchToIframe(iframe_CWDialogServiceRatePeriod);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(SaveAndCloseButton);
            return this;
        }
        public PersonContractRecordPage ValidateRateUnitText(string ExpectedText)
        {
            WaitForElementVisible(RateUnit);
            ValidateElementText(RateUnit, ExpectedText);
            return this;
        }

        public PersonContractRecordPage ValidateResponsibleTeamText(string ExpectedText)
        {
            WaitForElementVisible(ResponsibleTeam);
            ValidateElementText(ResponsibleTeam, ExpectedText);
            return this;
        }

        public PersonContractRecordPage ValidatePersonContractServiceText(string ExpectedText)
        {
            WaitForElementVisible(PersonContractService);
            ValidateElementText(PersonContractService, ExpectedText);
            return this;
        }

        public PersonContractRecordPage ValidateEndDateText(string ExpectedText)
        {
            WaitForElementVisible(EndDate);
            ValidateElementValue(EndDate, ExpectedText);
            return this;
        }
        public PersonContractRecordPage ValidateRateText(string ExpectedText)
        {
            WaitForElementVisible(Rate);
            ValidateElementText(Rate, ExpectedText);
            return this;
        }

       
        public PersonContractRecordPage InsertRateFielValue(string TextToInsert)
        {
            WaitForElementToBeClickable(Rate);
            SendKeys(Rate, TextToInsert + Keys.Tab);

            return this;
        }
        public PersonContractRecordPage ValidateRateValue(string ExpectedText)
        {
            WaitForElementVisible(Rate);
            WaitForElementToBeClickable(Rate);
            ValidateElementValue(Rate, ExpectedText);
            return this;
        }

        public PersonContractRecordPage ClickBankHolidayChargingCalendarLookupButton()
        {
            WaitForElementToBeClickable(BankHolidayChargingCalendarId);
            Click(BankHolidayChargingCalendarId);

            return this;
        }
        public PersonContractRecordPage InsertBankHolidayRateFieldValue(string TextToInsert)
        {
            WaitForElementVisible(BankHolidayRate);
            WaitForElementToBeClickable(BankHolidayRate);
            SendKeys(BankHolidayRate, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractRecordPage ValidateBankHolidayRateFieldValue(string ExpectedText)
        {
            WaitForElementVisible(BankHolidayRate);
            ValidateElementValue(BankHolidayRate, ExpectedText);
            return this;
        }

        public PersonContractRecordPage ValidateBankHolidayChargingCalendarFieldValue(string ExpectedText)
        {
            WaitForElementVisible(BankHolidayChargingCalendarLink);
            ValidateElementText(BankHolidayChargingCalendarLink, ExpectedText);
            return this;
        }

        public PersonContractRecordPage ValidateFieldOrderAfterRateRecordCreation(string ExpectedText1, string ExpectedText2, string ExpectedText3, string ExpectedText4, string ExpectedText5, string ExpectedText6, string ExpectedText7, string ExpectedText8)
        {
            WaitForElementVisible(StartDateHeader);
            ValidateElementText(StartDateHeader, ExpectedText1);
            WaitForElementVisible(EndDateHeader);
            ValidateElementText(EndDateHeader, ExpectedText2);
            WaitForElementVisible(RateHeader);
            ValidateElementText(RateHeader, ExpectedText3);
            WaitForElementVisible(BankHolidayRateHeader);
            ValidateElementText(BankHolidayRateHeader, ExpectedText4);
            WaitForElementVisible(RateUnitHeader);
            ValidateElementText(RateUnitHeader, ExpectedText5);
            WaitForElementVisible(TimeBandStartHeader);
            ValidateElementText(TimeBandStartHeader, ExpectedText6);
            WaitForElementVisible(TimeBandEndHeader);
            ValidateElementText(TimeBandEndHeader, ExpectedText7);
            WaitForElementVisible(SelectAllDaysHeader);
            ValidateElementText(SelectAllDaysHeader, ExpectedText8);

            return this;
        }

        public PersonContractRecordPage InsertTextOnEndtDate(string TextToInsert)
        {
            WaitForElementToBeClickable(EndDate);
            SendKeys(EndDate, TextToInsert + Keys.Tab);

            return this;
        }

        public PersonContractRecordPage ValidateHeaderForPersonContractServiceRatePeriod(string ExpectedText)
        {

            ValidateElementText(PersonContractServiceRatePeriodHeader, "Person Contract Service Rate Period:\r\n" + ExpectedText);
            return this;
        }

        public PersonContractRecordPage ValidateResponsibleTeamLookupIsDisabled()
        {
            WaitForElement(ResponsibleTeamLookupButton);
            ValidateElementDisabled(ResponsibleTeamLookupButton);
            return this;
        }

        public PersonContractRecordPage ValidateRateUnitLookupIsDisabled()
        {
            WaitForElement(RateUnitLookupButton);
            ValidateElementDisabled(RateUnitLookupButton);
            return this;
        }

        public PersonContractRecordPage ValidatePersonContractServiceLookupIsDisabled()
        {
            WaitForElement(PersonContractServiceLookupButton);
            ValidateElementDisabled(PersonContractServiceLookupButton);
            return this;
        }

        public PersonContractRecordPage ValidateRateValidationMessage(string ExpectedText)
        {
            WaitForElementVisible(RateValidationMeesage);
            WaitForElementToBeClickable(RateValidationMeesage);
            ValidateElementText(RateValidationMeesage, ExpectedText);
            return this;
        }

        public PersonContractRecordPage ClickPersonContractServiceLookupButton()
        {
            WaitForElementToBeClickable(PersonContractServiceLookup);
            Click(PersonContractServiceLookup);

            return this;
        }

        public PersonContractRecordPage WaitForPersonContractServiceRatePageToLoadAfterAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialogServiceRatePeriod);
            SwitchToIframe(iframe_CWDialogServiceRatePeriod);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(SaveAndCloseButton);
            return this;
        }

        public PersonContractRecordPage ValidateRateUnitTextFromAdvancedSearch(string ExpectedText)
        {
            WaitForElementVisible(RateUnit);
            WaitForElement(RateUnit);
            ScrollToElement(RateUnit);
            string ActualText = GetElementTextByJavascript(RateUnit);
            Assert.AreEqual(ExpectedText, ActualText);

            return this;
        }

        public PersonContractRecordPage ClickPersonContractServiceChargesPerWeekTab()
        {
            WaitForElementToBeClickable(ChargePerWeekTab);
            Click(ChargePerWeekTab);

            return this;
        }

        public PersonContractRecordPage WaitForPersonContractServiceChargesPerWeekTabToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialogService);
            SwitchToIframe(iframe_CWDialogService);

            WaitForElement(CWUrlPanelIFrame);
            SwitchToIframe(CWUrlPanelIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);
            WaitForElement(pageHeader);

            WaitForElementVisible(pageHeader);

            return this;
        }

        public PersonContractRecordPage SelectStatus(string TextToSelect)
        {
            WaitForElement(Status_Field);
            SelectPicklistElementByText(Status_Field, TextToSelect);

            return this;
        }

        public PersonContractRecordPage ClickYesRadioButtonForRateVerified()
        {
            WaitForElementToBeClickable(IsRateVerified_1);
            ScrollToElement(IsRateVerified_1);
            Click(IsRateVerified_1);

            return this;
        }

        public PersonContractRecordPage ClickYesRadioButtonForOverrideRate()
        {
            WaitForElementToBeClickable(IsOverrideRate_1);
            ScrollToElement(IsOverrideRate_1);
            Click(IsOverrideRate_1);

            return this;
        }

        public PersonContractRecordPage WaitForPersonContractServiceRatesTabToLoadAfterRateCreation()
        {

            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialogService);
            SwitchToIframe(iframe_CWDialogService);

            WaitForElementNotVisible("CWRefreshPanel", 30);
            WaitForElement(pageHeader);

            WaitForElementVisible(pageHeader);

            return this;
        }

        public PersonContractRecordPage ClickChargeApportionmentsTab()
        {
            WaitForElementToBeClickable(ChargeApportionmentTab);
            Click(ChargeApportionmentTab);

            return this;
        }

        public PersonContractRecordPage ValidateChargeApportionmentsTabIsVisible(bool IsVisible)
        {
            if (IsVisible)
                WaitForElementVisible(ChargeApportionmentTab);
            else
                WaitForElementNotVisible(ChargeApportionmentTab, 3);

            return this;
        }

    }
}
