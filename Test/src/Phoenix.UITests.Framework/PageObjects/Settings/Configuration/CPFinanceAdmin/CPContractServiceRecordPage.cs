using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
    public class CPContractServiceRecordPage : CommonMethods
    {

        public CPContractServiceRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careprovidercontractservice')]");
        By CWDialogIFrame(string RecordId) => By.XPath("//*[@id='iframe_CWDialog_" + RecordId + "']");

        readonly By PageHeader = By.XPath("//*[@id='CWToolbar']//h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By CloneRecordButton = By.XPath("//*[@id='TI_CloneRecordButton']");
        readonly By Contract = By.XPath("//*[@id='CWField_contractid']");
        readonly By EstablishmentProviderLookupButton = By.XPath("//*[@id='CWLookupBtn_establishmentproviderid']");
        readonly By EstablishmentProviderLinkField = By.XPath("//*[@id='CWField_establishmentproviderid_Link']");
        readonly By CareproviderContractSchemeLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidercontractschemeid']");
        readonly By CareproviderContractSchemeLinkField = By.XPath("//*[@id='CWField_careprovidercontractschemeid_Link']");
        readonly By ResponsibleUserLookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
        readonly By ResponsibleUserLinkField = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By FunderProviderLink = By.XPath("//*[@id='CWField_funderproviderid_Link']");
        readonly By FunderProviderLookupButton = By.XPath("//*[@id='CWLookupBtn_funderproviderid']");
        readonly By CareproviderServiceLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderserviceid']");
        readonly By CareproviderServiceLinkField = By.XPath("//*[@id='CWField_careproviderserviceid_Link']");
        readonly By CareproviderServiceDetailLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderservicedetailid']");
        readonly By CareproviderServiceDetailLinkField = By.XPath("//a[@id='CWField_careproviderservicedetailid_Link']");
        readonly By CpBookingTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingtypeid']");
        readonly By CareproviderRateUnitLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderrateunitid']");
        readonly By CareproviderBatchGroupingLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderbatchgroupingid']");
        readonly By CareproviderVatCodeLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidervatcodeid']");
        readonly By Isusedinfinanceinvoicebatch_1 = By.XPath("//*[@id='CWField_isusedinfinanceinvoicebatch_1']");
        readonly By Isusedinfinanceinvoicebatch_0 = By.XPath("//*[@id='CWField_isusedinfinanceinvoicebatch_0']");
        readonly By ContractServiceType = By.XPath("//*[@id='CWField_contractservicetypeid']");
        readonly By Isroomsapply_1 = By.XPath("//*[@id='CWField_isroomsapply_1']");
        readonly By Isroomsapply_0 = By.XPath("//*[@id='CWField_isroomsapply_0']");
        readonly By Isnetincome_1 = By.XPath("//*[@id='CWField_isnetincome_1']");
        readonly By Isnetincome_0 = By.XPath("//*[@id='CWField_isnetincome_0']");
        readonly By DateToCommenceBlockCharging = By.XPath("//*[@id='CWField_datetocommenceblockcharging']");
        readonly By DateToCommenceBlockChargingDatePicker = By.XPath("//*[@id='CWField_datetocommenceblockcharging_DatePicker']");
        readonly By Isusedinfinance_1 = By.XPath("//*[@id='CWField_isusedinfinance_1']");
        readonly By Isusedinfinance_0 = By.XPath("//*[@id='CWField_isusedinfinance_0']");
        readonly By ContractServiceAdjustedDays = By.XPath("//*[@id='CWField_contractserviceadjusteddaysid']");
        readonly By Isnegotiatedratescanapply_1 = By.XPath("//*[@id='CWField_isnegotiatedratescanapply_1']");
        readonly By Isnegotiatedratescanapply_0 = By.XPath("//*[@id='CWField_isnegotiatedratescanapply_0']");
        readonly By Ispermitrateoverride_1 = By.XPath("//*[@id='CWField_ispermitrateoverride_1']");
        readonly By Ispermitrateoverride_0 = By.XPath("//*[@id='CWField_ispermitrateoverride_0']");
        readonly By Chargescheduledcareonactuals_1 = By.XPath("//*[@id='CWField_chargescheduledcareonactuals_1']");
        readonly By Chargescheduledcareonactuals_0 = By.XPath("//*[@id='CWField_chargescheduledcareonactuals_0']");
        readonly By IncomeCareproviderContractServiceLookupButton = By.XPath("//*[@id='CWLookupBtn_incomecareprovidercontractserviceid']");
        readonly By Inactive_1 = By.XPath("//*[@id='CWField_inactive_1']");
        readonly By Inactive_0 = By.XPath("//*[@id='CWField_inactive_0']");
        readonly By NoteText = By.XPath("//*[@id='CWField_notetext']");
        readonly By RateUnitLinkField = By.XPath("//*[@id='CWField_careproviderrateunitid_Link']");
        readonly By VATCodeLinkField = By.XPath("//*[@id='CWField_careprovidervatcodeid_Link']");
        readonly By BatchGroupingLinkField = By.XPath("//*[@id='CWField_careproviderbatchgroupingid_Link']");

        readonly By DetailsTab = By.XPath("//*[@id = 'CWNavGroup_EditForm']/a");
        readonly By RatesToRecordTab = By.XPath("//*[@id = 'CWNavGroup_Rates']/a");

        public CPContractServiceRecordPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(PageHeader);

            return this;
        }

        public CPContractServiceRecordPage WaitForPageToLoad(string RecordId)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWDialogIFrame(RecordId));
            SwitchToIframe(CWDialogIFrame(RecordId));

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(PageHeader);

            return this;
        }


        public CPContractServiceRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public CPContractServiceRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public CPContractServiceRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public CPContractServiceRecordPage ClickCloneExistingContractServiceRecordButton()
        {
            WaitForElementToBeClickable(CloneRecordButton);
            ScrollToElement(CloneRecordButton);
            Click(CloneRecordButton);

            return this;
        }

        //CloneRecordButton is visible or not visible
        public CPContractServiceRecordPage ValidateCloneExistingContractServiceRecordButtonVisible(bool ExpectedVisible)
        {
            if(ExpectedVisible)
                WaitForElementVisible(CloneRecordButton);
            else
                WaitForElementNotVisible(CloneRecordButton, 2);

            return this;
        }

        public CPContractServiceRecordPage ValidateContractText(string ExpectedText)
        {
            ValidateElementValue(Contract, ExpectedText);

            return this;
        }

        public CPContractServiceRecordPage InsertTextOnContract(string TextToInsert)
        {
            WaitForElementToBeClickable(Contract);
            SendKeys(Contract, TextToInsert);

            return this;
        }

        public CPContractServiceRecordPage ClickEstablishmentProviderLookupButton()
        {
            WaitForElementToBeClickable(EstablishmentProviderLookupButton);
            Click(EstablishmentProviderLookupButton);

            return this;
        }

        //Verify EstablishmentProviderLinkField text
        public CPContractServiceRecordPage ValidateEstablishmentProviderLinkFieldText(string ExpectedText)
        {
            WaitForElement(EstablishmentProviderLinkField);
            ScrollToElement(EstablishmentProviderLinkField);
            ValidateElementText(EstablishmentProviderLinkField, ExpectedText);

            return this;
        }

        public CPContractServiceRecordPage ClickCareproviderContractSchemeLookupButton()
        {
            WaitForElementToBeClickable(CareproviderContractSchemeLookupButton);
            Click(CareproviderContractSchemeLookupButton);

            return this;
        }

        //Verify CareproviderContractSchemeLinkField text
        public CPContractServiceRecordPage ValidateContractSchemeLinkFieldText(string ExpectedText)
        {
            WaitForElement(CareproviderContractSchemeLinkField);
            ScrollToElement(CareproviderContractSchemeLinkField);
            ValidateElementText(CareproviderContractSchemeLinkField, ExpectedText);

            return this;
        }

        public CPContractServiceRecordPage ClickResponsibleUserLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleUserLookupButton);
            Click(ResponsibleUserLookupButton);

            return this;
        }

        //verify that ResponsibleUserLookupButton is disabled or enabled
        public CPContractServiceRecordPage ValidateResponsibleUserLookupButtonDisabled(bool ExpectedDisabled)
        {
            if(ExpectedDisabled)
                ValidateElementDisabled(ResponsibleUserLookupButton);
            else
                ValidateElementNotDisabled(ResponsibleUserLookupButton);

            return this;
        }

        public CPContractServiceRecordPage ValidateResponsibleUserLinkFieldText(string ExpectedText)
        {
            WaitForElement(ResponsibleUserLinkField);
            ScrollToElement(ResponsibleUserLinkField);
            ValidateElementText(ResponsibleUserLinkField, ExpectedText);

            return this;
        }

        public CPContractServiceRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public CPContractServiceRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElement(ResponsibleTeamLink);
            ScrollToElement(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public CPContractServiceRecordPage ClickResponsibleTeamClearButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamClearButton);
            Click(ResponsibleTeamClearButton);

            return this;
        }

        public CPContractServiceRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public CPContractServiceRecordPage ClickFunderProviderLink()
        {
            WaitForElement(FunderProviderLink);
            ScrollToElement(FunderProviderLink);
            Click(FunderProviderLink);

            return this;
        }

        public CPContractServiceRecordPage ValidateFunderProviderLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(FunderProviderLink);
            ValidateElementText(FunderProviderLink, ExpectedText);

            return this;
        }

        public CPContractServiceRecordPage ClickFunderProviderLookupButton()
        {
            WaitForElementToBeClickable(FunderProviderLookupButton);
            Click(FunderProviderLookupButton);

            return this;
        }

        public CPContractServiceRecordPage ClickCareproviderServiceLookupButton()
        {
            WaitForElementToBeClickable(CareproviderServiceLookupButton);
            Click(CareproviderServiceLookupButton);

            return this;
        }

        public CPContractServiceRecordPage ValidateServiceLinkFieldText(string ExpectedText)
        {
            WaitForElement(CareproviderServiceLinkField);
            ScrollToElement(CareproviderServiceLinkField);
            ValidateElementText(CareproviderServiceLinkField, ExpectedText);

            return this;
        }

        public CPContractServiceRecordPage ClickCareproviderServiceDetailLookupButton()
        {
            WaitForElementToBeClickable(CareproviderServiceDetailLookupButton);
            Click(CareproviderServiceDetailLookupButton);

            return this;
        }

        public CPContractServiceRecordPage ValidateServiceDetailLinkFieldText(string ExpectedText)
        {
            WaitForElement(CareproviderServiceDetailLinkField);
            ScrollToElement(CareproviderServiceDetailLinkField);
            ValidateElementByTitle(CareproviderServiceDetailLinkField, ExpectedText);

            return this;
        }

        public CPContractServiceRecordPage ClickBookingTypeLookupButton()
        {
            WaitForElementToBeClickable(CpBookingTypeLookupButton);
            Click(CpBookingTypeLookupButton);

            return this;
        }

        public CPContractServiceRecordPage ClickCareproviderRateUnitLookupButton()
        {
            WaitForElementToBeClickable(CareproviderRateUnitLookupButton);
            Click(CareproviderRateUnitLookupButton);

            return this;
        }

        public CPContractServiceRecordPage ClickCareproviderBatchGroupingLookupButton()
        {
            WaitForElementToBeClickable(CareproviderBatchGroupingLookupButton);
            Click(CareproviderBatchGroupingLookupButton);

            return this;
        }

        //verify CareproviderBatchGroupingLookupButton is disabled or enabled
        public CPContractServiceRecordPage ValidateCareproviderBatchGroupingLookupButtonDisabled(bool ExpectedDisabled)
        {
            WaitForElement(CareproviderBatchGroupingLookupButton);
            ScrollToElement(CareproviderBatchGroupingLookupButton);
            if(ExpectedDisabled)
                ValidateElementDisabled(CareproviderBatchGroupingLookupButton);
            else
                ValidateElementNotDisabled(CareproviderBatchGroupingLookupButton);

            return this;
        }

        public CPContractServiceRecordPage ClickCareproviderVatCodeLookupButton()
        {
            WaitForElementToBeClickable(CareproviderVatCodeLookupButton);
            Click(CareproviderVatCodeLookupButton);

            return this;
        }

        //Verify CareproviderVatCodeLookupButton is disabled or enabled
        public CPContractServiceRecordPage ValidateCareproviderVatCodeLookupButtonDisabled(bool ExpectedDisabled)
        {
            if(ExpectedDisabled)
                ValidateElementDisabled(CareproviderVatCodeLookupButton);
            else
                ValidateElementNotDisabled(CareproviderVatCodeLookupButton);

            return this;
        }

        public CPContractServiceRecordPage ClickIsUsedInFinanceInvoiceBatch_YesOption()
        {
            WaitForElementToBeClickable(Isusedinfinanceinvoicebatch_1);
            Click(Isusedinfinanceinvoicebatch_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsUsedInFinanceInvoiceBatch_YesOptionChecked()
        {
            WaitForElement(Isusedinfinanceinvoicebatch_1);
            ScrollToElement(Isusedinfinanceinvoicebatch_1);
            ValidateElementChecked(Isusedinfinanceinvoicebatch_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsUsedInFinanceInvoiceBatch_YesOptionNotChecked()
        {
            WaitForElement(Isusedinfinanceinvoicebatch_1);
            ScrollToElement(Isusedinfinanceinvoicebatch_1);
            ValidateElementNotChecked(Isusedinfinanceinvoicebatch_1);

            return this;
        }

        public CPContractServiceRecordPage ClickIsUsedInFinanceInvoiceBatch_NoOption()
        {
            WaitForElementToBeClickable(Isusedinfinanceinvoicebatch_0);
            Click(Isusedinfinanceinvoicebatch_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsUsedInFinanceInvoiceBatch_NoOptionChecked()
        {
            WaitForElement(Isusedinfinanceinvoicebatch_0);
            ScrollToElement(Isusedinfinanceinvoicebatch_0);
            ValidateElementChecked(Isusedinfinanceinvoicebatch_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsUsedInFinanceInvoiceBatch_NoOptionNotChecked()
        {
            WaitForElement(Isusedinfinanceinvoicebatch_0);
            ScrollToElement(Isusedinfinanceinvoicebatch_0);
            ValidateElementNotChecked(Isusedinfinanceinvoicebatch_0);

            return this;
        }

        public CPContractServiceRecordPage SelectContractServiceType(string TextToSelect)
        {
            WaitForElementToBeClickable(ContractServiceType);
            SelectPicklistElementByText(ContractServiceType, TextToSelect);

            return this;
        }

        public CPContractServiceRecordPage ValidateContractServiceTypeSelectedText(string ExpectedText)
        {
            ValidateElementText(ContractServiceType, ExpectedText);

            return this;
        }

        public CPContractServiceRecordPage ClickIsRoomsApply_Yes()
        {
            WaitForElementToBeClickable(Isroomsapply_1);
            Click(Isroomsapply_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsRoomsApply_YesOptionChecked()
        {
            WaitForElement(Isroomsapply_1);
            ValidateElementChecked(Isroomsapply_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsRoomsApply_YesOptionNotChecked()
        {
            WaitForElement(Isroomsapply_1);
            ValidateElementNotChecked(Isroomsapply_1);

            return this;
        }

        public CPContractServiceRecordPage ClickIsRoomsApply_No()
        {
            WaitForElementToBeClickable(Isroomsapply_0);
            Click(Isroomsapply_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsRoomsApply_NoOptionChecked()
        {
            WaitForElement(Isroomsapply_0);
            ValidateElementChecked(Isroomsapply_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsRoomsApply_NoOptionNotChecked()
        {
            WaitForElement(Isroomsapply_0);
            ValidateElementNotChecked(Isroomsapply_0);

            return this;
        }

        //verify Isroomapply options are disabled or enabled
        public CPContractServiceRecordPage ValidateIsRoomsApplyDisabled(bool ExpectedDisabled)
        {
            WaitForElement(Isroomsapply_1);
            WaitForElement(Isroomsapply_0);
            ScrollToElement(Isroomsapply_1);
            if(ExpectedDisabled)
            {
                ValidateElementDisabled(Isroomsapply_1);
                ValidateElementDisabled(Isroomsapply_0);
            }
            else
            {
                ValidateElementNotDisabled(Isroomsapply_1);
                ValidateElementNotDisabled(Isroomsapply_0);
            }

            return this;
        }

        public CPContractServiceRecordPage ClickIsNetIncome_Yes()
        {
            WaitForElementToBeClickable(Isnetincome_1);
            Click(Isnetincome_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsNetIncome_YesOptionChecked()
        {
            WaitForElement(Isnetincome_1);
            ValidateElementChecked(Isnetincome_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsNetIncome_YesOptionNotChecked()
        {
            WaitForElement(Isnetincome_1);
            ValidateElementNotChecked(Isnetincome_1);

            return this;
        }

        public CPContractServiceRecordPage ClickIsNetIncome_No()
        {
            WaitForElementToBeClickable(Isnetincome_0);
            Click(Isnetincome_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsNetIncome_NoOptionChecked()
        {
            WaitForElement(Isnetincome_0);
            ValidateElementChecked(Isnetincome_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsNetIncome_NoOptionNotChecked()
        {
            WaitForElement(Isnetincome_0);
            ValidateElementNotChecked(Isnetincome_0);

            return this;
        }

        //verify Isnetincome options are disabled or enabled
        public CPContractServiceRecordPage ValidateIsNetIncomeDisabled(bool ExpectedDisabled)
        {
            WaitForElement(Isnetincome_1);
            WaitForElement(Isnetincome_0);
            ScrollToElement(Isnetincome_1);
            if(ExpectedDisabled)
            {
                ValidateElementDisabled(Isnetincome_1);
                ValidateElementDisabled(Isnetincome_0);
            }
            else
            {
                ValidateElementNotDisabled(Isnetincome_1);
                ValidateElementNotDisabled(Isnetincome_0);
            }

            return this;
        }

        public CPContractServiceRecordPage ValidateDatetocommenceblockchargingText(string ExpectedText)
        {
            ValidateElementValue(DateToCommenceBlockCharging, ExpectedText);

            return this;
        }

        //Verify DateToCommenceBlockCharging is disabled or not disabled
        public CPContractServiceRecordPage ValidateDateToCommenceBlockChargingDisabled(bool ExpectedDisabled)
        {
            WaitForElement(DateToCommenceBlockCharging);
            ScrollToElement(DateToCommenceBlockCharging);
            if(ExpectedDisabled)
                ValidateElementDisabled(DateToCommenceBlockCharging);
            else
                ValidateElementNotDisabled(DateToCommenceBlockCharging);

            return this;
        }

        public CPContractServiceRecordPage InsertTextOnDatetoCommenceBlockCharging(string TextToInsert)
        {
            WaitForElementToBeClickable(DateToCommenceBlockCharging);
            SendKeys(DateToCommenceBlockCharging, TextToInsert);

            return this;
        }

        public CPContractServiceRecordPage ClickDateToCommenceBlockChargingDatePicker()
        {
            WaitForElementToBeClickable(DateToCommenceBlockChargingDatePicker);
            Click(DateToCommenceBlockChargingDatePicker);

            return this;
        }

        //verify DateToCommenceBlockChargingDatePicker is disabled or not disabled
        public CPContractServiceRecordPage ValidateDateToCommenceBlockChargingDatePickerDisabled(bool ExpectedDisabled)
        {
            WaitForElement(DateToCommenceBlockChargingDatePicker);
            ScrollToElement(DateToCommenceBlockChargingDatePicker);
            if(ExpectedDisabled)
                ValidateElementDisabled(DateToCommenceBlockChargingDatePicker);
            else
                ValidateElementNotDisabled(DateToCommenceBlockChargingDatePicker);

            return this;
        }

        public CPContractServiceRecordPage ClickIsUsedInFinance_Yes()
        {
            WaitForElementToBeClickable(Isusedinfinance_1);
            Click(Isusedinfinance_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsUsedInFinance_YesOptionChecked()
        {
            WaitForElement(Isusedinfinance_1);
            ValidateElementChecked(Isusedinfinance_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsUsedInFinance_YesOptionNotChecked()
        {
            WaitForElement(Isusedinfinance_1);
            ValidateElementNotChecked(Isusedinfinance_1);

            return this;
        }

        public CPContractServiceRecordPage ClickIsUsedInFinance_No()
        {
            WaitForElementToBeClickable(Isusedinfinance_0);
            Click(Isusedinfinance_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsUsedInFinance_NoOptionChecked()
        {
            WaitForElement(Isusedinfinance_0);
            ValidateElementChecked(Isusedinfinance_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsUsedInFinance_NoOptionNotChecked()
        {
            WaitForElement(Isusedinfinance_0);
            ValidateElementNotChecked(Isusedinfinance_0);

            return this;
        }

        public CPContractServiceRecordPage SelectContractServiceAdjustedDays(string TextToSelect)
        {
            WaitForElementToBeClickable(ContractServiceAdjustedDays);
            SelectPicklistElementByText(ContractServiceAdjustedDays, TextToSelect);

            return this;
        }

        public CPContractServiceRecordPage ValidateContractServiceAdjustedDaysSelectedText(string ExpectedText)
        {
            ValidateElementText(ContractServiceAdjustedDays, ExpectedText);

            return this;
        }

        //verify ContractServiceAdjustedDays picklist field is disabled or enabled
        public CPContractServiceRecordPage ValidateContractServiceAdjustedDaysDisabled(bool ExpectedDisabled)
        {
            if(ExpectedDisabled)
                ValidateElementDisabled(ContractServiceAdjustedDays);
            else
                ValidateElementNotDisabled(ContractServiceAdjustedDays);

            return this;
        }


        public CPContractServiceRecordPage ClickIsNegotiatedRatesCanApply_Yes()
        {
            WaitForElementToBeClickable(Isnegotiatedratescanapply_1);
            Click(Isnegotiatedratescanapply_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsNegotiatedRatesCanApply_YesOptionChecked()
        {
            WaitForElement(Isnegotiatedratescanapply_1);
            ValidateElementChecked(Isnegotiatedratescanapply_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsNegotiatedRatesCanApply_YesOptionNotChecked()
        {
            WaitForElement(Isnegotiatedratescanapply_1);
            ValidateElementNotChecked(Isnegotiatedratescanapply_1);

            return this;
        }

        public CPContractServiceRecordPage ClickIsNegotiatedRatesCanApply_No()
        {
            WaitForElementToBeClickable(Isnegotiatedratescanapply_0);
            Click(Isnegotiatedratescanapply_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsNegotiatedRatesCanApply_NoOptionChecked()
        {
            WaitForElement(Isnegotiatedratescanapply_0);
            ValidateElementChecked(Isnegotiatedratescanapply_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateIsNegotiatedRatesCanApply_NoOptionNotChecked()
        {
            WaitForElement(Isnegotiatedratescanapply_0);
            ValidateElementNotChecked(Isnegotiatedratescanapply_0);

            return this;
        }

        public CPContractServiceRecordPage ClickIspermitrateoverride_Yes()
        {
            WaitForElementToBeClickable(Ispermitrateoverride_1);
            Click(Ispermitrateoverride_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateIspermitrateoverride_YesOptionChecked()
        {
            WaitForElement(Ispermitrateoverride_1);
            ValidateElementChecked(Ispermitrateoverride_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateIspermitrateoverride_YesOptionNotChecked()
        {
            WaitForElement(Ispermitrateoverride_1);
            ValidateElementNotChecked(Ispermitrateoverride_1);

            return this;
        }

        public CPContractServiceRecordPage ClickIspermitrateoverride_No()
        {
            WaitForElementToBeClickable(Ispermitrateoverride_0);
            Click(Ispermitrateoverride_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateIspermitrateoverride_NoOptionChecked()
        {
            WaitForElement(Ispermitrateoverride_0);
            ValidateElementChecked(Ispermitrateoverride_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateIspermitrateoverride_NoOptionNotChecked()
        {
            WaitForElement(Ispermitrateoverride_0);
            ValidateElementNotChecked(Ispermitrateoverride_0);

            return this;
        }

        //verify Ispermitrateoverride options are disabled or enabled
        public CPContractServiceRecordPage ValidateIsPermitRateOverrideOptionsAreDisabled(bool ExpectedDisabled)
        {
            WaitForElement(Ispermitrateoverride_1);
            WaitForElement(Ispermitrateoverride_0);
            ScrollToElement(Ispermitrateoverride_1);
            if(ExpectedDisabled)
            {
                ValidateElementDisabled(Ispermitrateoverride_1);
                ValidateElementDisabled(Ispermitrateoverride_0);
            }
            else
            {
                ValidateElementNotDisabled(Ispermitrateoverride_1);
                ValidateElementNotDisabled(Ispermitrateoverride_0);
            }

            return this;
        }

        public CPContractServiceRecordPage ClickChargescheduledcareonactuals_Yes()
        {
            WaitForElementToBeClickable(Chargescheduledcareonactuals_1);
            Click(Chargescheduledcareonactuals_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateChargescheduledcareonactuals_YesOptionChecked()
        {
            WaitForElement(Chargescheduledcareonactuals_1);
            ValidateElementChecked(Chargescheduledcareonactuals_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateChargescheduledcareonactuals_YesOptionNotChecked()
        {
            WaitForElement(Chargescheduledcareonactuals_1);
            ValidateElementNotChecked(Chargescheduledcareonactuals_1);

            return this;
        }

        public CPContractServiceRecordPage ClickChargescheduledcareonactuals_No()
        {
            WaitForElementToBeClickable(Chargescheduledcareonactuals_0);
            Click(Chargescheduledcareonactuals_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateChargescheduledcareonactuals_NoOptionChecked()
        {
            WaitForElement(Chargescheduledcareonactuals_0);
            ValidateElementChecked(Chargescheduledcareonactuals_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateChargescheduledcareonactuals_NoOptionNotChecked()
        {
            WaitForElement(Chargescheduledcareonactuals_0);
            ValidateElementNotChecked(Chargescheduledcareonactuals_0);

            return this;
        }

        public CPContractServiceRecordPage ClickIncomeCareproviderContractServiceLookupButton()
        {
            WaitForElementToBeClickable(IncomeCareproviderContractServiceLookupButton);
            Click(IncomeCareproviderContractServiceLookupButton);

            return this;
        }

        public CPContractServiceRecordPage ClickInactive_Yes()
        {
            WaitForElementToBeClickable(Inactive_1);
            Click(Inactive_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateInactive_YesOptionChecked()
        {
            WaitForElement(Inactive_1);
            ValidateElementChecked(Inactive_1);

            return this;
        }

        public CPContractServiceRecordPage ValidateInactive_YesOptionNotChecked()
        {
            WaitForElement(Inactive_1);
            ValidateElementNotChecked(Inactive_1);

            return this;
        }

        public CPContractServiceRecordPage ClickInactive_No()
        {
            WaitForElementToBeClickable(Inactive_0);
            Click(Inactive_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateInactive_NoOptionChecked()
        {
            WaitForElement(Inactive_0);
            ValidateElementChecked(Inactive_0);

            return this;
        }

        public CPContractServiceRecordPage ValidateInactive_NoOptionNotChecked()
        {
            WaitForElement(Inactive_0);
            ValidateElementNotChecked(Inactive_0);

            return this;
        }

        //verify Inactive options are disabled or enabled
        public CPContractServiceRecordPage ValidateInactiveOptionsDisabled(bool ExpectedDisabled)
        {
            WaitForElement(Inactive_1);
            WaitForElement(Inactive_0);
            ScrollToElement(Inactive_1);
            if(ExpectedDisabled)
            {
                ValidateElementDisabled(Inactive_1);
                ValidateElementDisabled(Inactive_0);
            }
            else
            {
                ValidateElementNotDisabled(Inactive_1);
                ValidateElementNotDisabled(Inactive_0);
            }

            return this;
        }

        public CPContractServiceRecordPage ValidateNoteText(string ExpectedText)
        {
            ValidateElementText(NoteText, ExpectedText);

            return this;
        }

        //verify notetext is disabled or enabled
        public CPContractServiceRecordPage ValidateNoteTextDisabled(bool ExpectedDisabled)
        {
            if(ExpectedDisabled)
                ValidateElementDisabled(NoteText);
            else
                ValidateElementNotDisabled(NoteText);

            return this;
        }

        public CPContractServiceRecordPage InsertTextOnNoteText(string TextToInsert)
        {
            WaitForElementToBeClickable(NoteText);
            SendKeys(NoteText, TextToInsert);

            return this;
        }

        public CPContractServiceRecordPage ValidateRateUnitLinkFieldText(string ExpectedText)
        {
            WaitForElement(RateUnitLinkField);
            ScrollToElement(RateUnitLinkField);
            ValidateElementText(RateUnitLinkField, ExpectedText);

            return this;
        }

        public CPContractServiceRecordPage ValidateVATCodeLinkFieldText(string ExpectedText)
        {
            WaitForElement(VATCodeLinkField);
            ScrollToElement(VATCodeLinkField);
            ValidateElementText(VATCodeLinkField, ExpectedText);

            return this;
        }

        public CPContractServiceRecordPage ValidateBatchGroupingLinkFieldText(string ExpectedText)
        {
            WaitForElement(BatchGroupingLinkField);
            ScrollToElement(BatchGroupingLinkField);
            ValidateElementText(BatchGroupingLinkField, ExpectedText);

            return this;
        }

        public CPContractServiceRecordPage ClickDetailsTab()
        {
            WaitForElementToBeClickable(DetailsTab);

            ScrollToElement(DetailsTab);
            Click(DetailsTab);

            return this;
        }

        public CPContractServiceRecordPage ClickRatesToRecordTab()
        {
            WaitForElementToBeClickable(RatesToRecordTab);

            ScrollToElement(RatesToRecordTab);
            var IsTabCurrent = GetElementByAttributeValue(RatesToRecordTab, "class");

            if(!IsTabCurrent.Equals("tab current"))
            {
                ScrollToElement(RatesToRecordTab);
                Click(RatesToRecordTab);
            }

            return this;
        }

        //verify ResponsibleTeamLookupButton is disabled or enabled
        public CPContractServiceRecordPage ValidateResponsibleTeamLookupButtonDisabled(bool ExpectedDisabled)
        {
            if(ExpectedDisabled)
                ValidateElementDisabled(ResponsibleTeamLookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeamLookupButton);

            return this;
        }

    }
}
