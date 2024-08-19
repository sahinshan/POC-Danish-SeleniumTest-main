using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ContractServiceRatePeriodRecordPage : CommonMethods
    {
        public ContractServiceRatePeriodRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        //readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careprovidercontractservicerateperiod&')]");
        By CWDialogIFrame(string RecordId) => By.XPath("//*[@id='iframe_CWDialog_" + RecordId + "']");

        readonly By titleSpan = By.XPath("//*[@id='CWToolbar']/div/h1/span");

        readonly By notificationArea = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By deleteButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        readonly By ContractServiceBandedRatesTab = By.XPath("//*[@id='CWNavGroup_ContractServiceBandedRates']/a");

        readonly By CareprovidercontractserviceidLink = By.XPath("//*[@id='CWField_careprovidercontractserviceid_Link']");
        readonly By CareprovidercontractserviceidClearButton = By.XPath("//*[@id='CWClearLookup_careprovidercontractserviceid']");
        readonly By CareprovidercontractserviceidLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidercontractserviceid']");
        readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
        readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
        readonly By CareproviderrateunitidLink = By.XPath("//*[@id='CWField_careproviderrateunitid_Link']");
        readonly By CareproviderrateunitidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderrateunitid']");
        readonly By CptimebandsetidLookupButton = By.XPath("//*[@id='CWLookupBtn_cptimebandsetid']");
        readonly By CptimebandsetidLink = By.XPath("//*[@id='CWField_cptimebandsetid_Link']");
        readonly By Rate = By.XPath("//*[@id='CWField_rate']");
        readonly By CppersoncontractidLookupButton = By.XPath("//*[@id='CWLookupBtn_cppersoncontractid']");
        By cpPersoncontractOptionRemoveButton(string RecordId) => By.XPath("//*[@id='MS_cppersoncontractid_" + RecordId + "']/a[text()='Remove']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");
        readonly By EnddateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
        readonly By CpbankholidaychargingcalendaridLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbankholidaychargingcalendarid']");
        readonly By CpbankholidaychargingcalendaridLink = By.XPath("//*[@id='CWField_cpbankholidaychargingcalendarid_Link']");
        readonly By Bankholidayrate = By.XPath("//*[@id='CWField_bankholidayrate']");
        readonly By Bankholidayrate_ErrorLabel = By.XPath("//*[@id='CWControlHolder_bankholidayrate']/label/span");
        readonly By CareproviderbandratetypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderbandratetypeid']");
        readonly By CareproviderbandratetypeidLink = By.XPath("//*[@id='CWField_careproviderbandratetypeid_Link']");
        readonly By CpstaffroletypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpstaffroletypeid']");
        By cpJobRoleOptionRemoveButton(string RecordId) => By.XPath("//*[@id='MS_cpstaffroletypeid_" + RecordId + "']/a[text()='Remove']");
        readonly By rateperunit = By.XPath("//*[@id='CWField_rateperunit']");
        readonly By financecode = By.XPath("//*[@id='CWField_financecode']");
        readonly By financecodeEditButton = By.XPath("//*[@id='CWFieldButton_financecode']");
        readonly By capacity = By.XPath("//*[@id='CWField_capacity']");
        readonly By capacitycanbeexceededid = By.XPath("//*[@id='CWField_capacitycanbeexceededid']");
        readonly By Notetext = By.XPath("//*[@id='CWField_notetext']");



        public ContractServiceRatePeriodRecordPage WaitForContractServiceRatePeriodRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(BackButton);

            WaitForElementVisible(CareprovidercontractserviceidLookupButton);
            return this;
        }

        public ContractServiceRatePeriodRecordPage WaitForContractServiceRatePeriodRecordPageToLoad(string RecordId)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWDialogIFrame(RecordId));
            SwitchToIframe(CWDialogIFrame(RecordId));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ContractServiceRatePeriodRecordPage WaitForContractServiceRatePeriodRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            //WaitForElement(iframe_CWDataFormDialog);
            //SwitchToIframe(iframe_CWDataFormDialog);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(BackButton);

            WaitForElementVisible(CareprovidercontractserviceidLookupButton);
            return this;
        }


        public ContractServiceRatePeriodRecordPage ValidateTitleText(string ExpectedText)
        {
            ValidateElementText(titleSpan, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateNotificationAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(notificationArea);
            }
            else
            {
                WaitForElementNotVisible(notificationArea, 3);
            }

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateNotificationAreaText(string ExpectedText)
        {
            ValidateElementText(notificationArea, ExpectedText);

            return this;
        }


        public ContractServiceRatePeriodRecordPage ValidateGeneralSectionFieldsVisible(bool TimebandSetFieldVisible = false, bool RateFieldVisible = false, bool BankHolidayChargingCalendarFieldVisible = false, bool BandRateTypeFieldVisible = false)
        {
            WaitForElementVisible(CareprovidercontractserviceidLookupButton);
            WaitForElementVisible(Startdate);
            WaitForElementVisible(CareproviderrateunitidLookupButton);
            WaitForElementVisible(ResponsibleTeamLookupButton);
            WaitForElementVisible(Enddate);

            if (TimebandSetFieldVisible)
                WaitForElementVisible(CptimebandsetidLookupButton);
            else
                WaitForElementNotVisible(CptimebandsetidLookupButton, 3);

            if (RateFieldVisible)
                WaitForElementVisible(Rate);
            else
                WaitForElementNotVisible(Rate, 3);

            if (BankHolidayChargingCalendarFieldVisible)
                WaitForElementVisible(CpbankholidaychargingcalendaridLookupButton);
            else
                WaitForElementNotVisible(CpbankholidaychargingcalendaridLookupButton, 3);

            if (BandRateTypeFieldVisible)
                WaitForElementVisible(CareproviderbandratetypeidLookupButton);
            else
                WaitForElementNotVisible(CareproviderbandratetypeidLookupButton, 3);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateBlockContractDetailsSectionFieldsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(rateperunit);
                WaitForElementVisible(financecode);
                WaitForElementVisible(capacity);
                WaitForElementVisible(capacitycanbeexceededid);
            }
            else
            {
                WaitForElementNotVisible(rateperunit, 3);
                WaitForElementNotVisible(financecode, 3);
                WaitForElementNotVisible(capacity, 3);
                WaitForElementNotVisible(capacitycanbeexceededid, 3);
            }

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateNotesSectionFieldsVisible()
        {
            WaitForElementVisible(Notetext);

            return this;
        }



        public ContractServiceRatePeriodRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickContractServiceBandedRatesTab()
        {
            WaitForElementToBeClickable(ContractServiceBandedRatesTab);
            Click(ContractServiceBandedRatesTab);

            return this;
        }


        public ContractServiceRatePeriodRecordPage ClickContractServiceLink()
        {
            WaitForElementToBeClickable(CareprovidercontractserviceidLink);
            Click(CareprovidercontractserviceidLink);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateContractServiceLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareprovidercontractserviceidLink);
            ValidateElementText(CareprovidercontractserviceidLink, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickContractServiceClearButton()
        {
            WaitForElementToBeClickable(CareprovidercontractserviceidClearButton);
            Click(CareprovidercontractserviceidClearButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickContractServiceLookupButton()
        {
            WaitForElementToBeClickable(CareprovidercontractserviceidLookupButton);
            Click(CareprovidercontractserviceidLookupButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateStartDateText(string ExpectedText)
        {
            ValidateElementValue(Startdate, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage InsertTextOnStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Startdate);
            SendKeys(Startdate, TextToInsert + Keys.Tab);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickStartDateDatePicker()
        {
            WaitForElementToBeClickable(StartdateDatePicker);
            Click(StartdateDatePicker);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickRateUnitLink()
        {
            WaitForElementToBeClickable(CareproviderrateunitidLink);
            Click(CareproviderrateunitidLink);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateRateUnitLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareproviderrateunitidLink);
            ValidateElementText(CareproviderrateunitidLink, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickRateUnitLookupButton()
        {
            WaitForElementToBeClickable(CareproviderrateunitidLookupButton);
            Click(CareproviderrateunitidLookupButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateRateUnitFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(CareproviderrateunitidLookupButton);
            else
                ValidateElementNotDisabled(CareproviderrateunitidLookupButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickTimebandSetLookupButton()
        {
            WaitForElementToBeClickable(CptimebandsetidLookupButton);
            Click(CptimebandsetidLookupButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateTimebandSetLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CptimebandsetidLink);
            ValidateElementText(CptimebandsetidLink, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateRateText(string ExpectedText)
        {
            ValidateElementValue(Rate, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage InsertTextOnRate(string TextToInsert)
        {
            WaitForElementToBeClickable(Rate);
            SendKeys(Rate, TextToInsert + Keys.Tab);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickPersonContractLookupButton()
        {
            WaitForElementToBeClickable(CppersoncontractidLookupButton);
            Click(CppersoncontractidLookupButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickPersonContractOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(cpPersoncontractOptionRemoveButton(RecordId));
            Click(cpPersoncontractOptionRemoveButton(RecordId));

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickPersonContractOptionRemoveButton(Guid RecordId)
        {
            return ClickPersonContractOptionRemoveButton(RecordId.ToString());
        }

        public ContractServiceRatePeriodRecordPage ValidatePersonContractLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CppersoncontractidLookupButton);
            }
            else
            {
                WaitForElementNotVisible(CppersoncontractidLookupButton, 3);
            }

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickResponsibleTeamClearButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamClearButton);
            Click(ResponsibleTeamClearButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateEndDateText(string ExpectedText)
        {
            ValidateElementValue(Enddate, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage InsertTextOnEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Enddate);
            SendKeys(Enddate, TextToInsert + Keys.Tab);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickEndDateDatePicker()
        {
            WaitForElementToBeClickable(EnddateDatePicker);
            Click(EnddateDatePicker);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickBankHolidayChargingCalendarLookupButton()
        {
            WaitForElementToBeClickable(CpbankholidaychargingcalendaridLookupButton);
            Click(CpbankholidaychargingcalendaridLookupButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateBankHolidayChargingCalendarLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CpbankholidaychargingcalendaridLink);
            ValidateElementText(CpbankholidaychargingcalendaridLink, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateBankHolidayRateText(string ExpectedText)
        {
            ValidateElementValue(Bankholidayrate, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage InsertTextOnBankHolidayRate(string TextToInsert)
        {
            WaitForElementToBeClickable(Bankholidayrate);
            SendKeys(Bankholidayrate, TextToInsert + Keys.Tab);

            return this;
        }

        public ContractServiceRatePeriodRecordPage InsertTextOnBankHolidayRateFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Bankholidayrate);
            }
            else
            {
                WaitForElementNotVisible(Bankholidayrate, 3);
                WaitForElementNotVisible(Bankholidayrate_ErrorLabel, 3);
            }

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateBankHolidayRateErrorLabelText(string ExpectedText)
        {
            WaitForElement(Bankholidayrate_ErrorLabel);
            ValidateElementText(Bankholidayrate_ErrorLabel, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickBandRateTypeLookupButton()
        {
            WaitForElementToBeClickable(CareproviderbandratetypeidLookupButton);
            Click(CareproviderbandratetypeidLookupButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateBandRateTypeLookupLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareproviderbandratetypeidLink);
            ValidateElementText(CareproviderbandratetypeidLink, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickJobRoleLookupButton()
        {
            WaitForElementToBeClickable(CpstaffroletypeidLookupButton);
            Click(CpstaffroletypeidLookupButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickJobRoleOptionRemoveButton(string RecordId)
        {
            WaitForElementToBeClickable(cpJobRoleOptionRemoveButton(RecordId));
            Click(cpJobRoleOptionRemoveButton(RecordId));

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickJobRoleOptionRemoveButton(Guid RecordId)
        {
            return ClickJobRoleOptionRemoveButton(RecordId.ToString());
        }

        public ContractServiceRatePeriodRecordPage ValidateRatePerUnitBlockText(string ExpectedText)
        {
            ValidateElementValue(rateperunit, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage InsertTextOnRatePerUnitBlock(string TextToInsert)
        {
            WaitForElementToBeClickable(rateperunit);
            SendKeys(rateperunit, TextToInsert + Keys.Tab);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateFinanceCodeText(string ExpectedText)
        {
            ValidateElementValue(financecode, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ClickFinanceCodeEditButton()
        {
            WaitForElementToBeClickable(financecodeEditButton);
            Click(financecodeEditButton);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateCapacityText(string ExpectedText)
        {
            ValidateElementValue(capacity, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage InsertTextOnCapacity(string TextToInsert)
        {
            WaitForElementToBeClickable(capacity);
            SendKeys(capacity, TextToInsert + Keys.Tab);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateNoteTextText(string ExpectedText)
        {
            ValidateElementText(Notetext, ExpectedText);

            return this;
        }

        public ContractServiceRatePeriodRecordPage InsertTextOnNoteText(string TextToInsert)
        {
            WaitForElementToBeClickable(Notetext);
            SendKeys(Notetext, TextToInsert + Keys.Tab);

            return this;
        }

        public ContractServiceRatePeriodRecordPage SelectCapacityCanBeExceeded(string TextToSelect)
        {
            WaitForElementToBeClickable(capacitycanbeexceededid);
            SelectPicklistElementByText(capacitycanbeexceededid, TextToSelect);

            return this;
        }

        public ContractServiceRatePeriodRecordPage ValidateCapacityCanBeExceededSelectedText(string ExpectedText)
        {
            WaitForElement(capacitycanbeexceededid);
            ValidatePicklistSelectedText(capacitycanbeexceededid, ExpectedText);

            return this;
        }
    }
}
