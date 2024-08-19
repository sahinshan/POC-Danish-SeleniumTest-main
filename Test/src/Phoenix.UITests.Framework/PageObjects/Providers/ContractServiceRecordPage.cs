using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.Providers;
using System;
using System.Diagnostics.Contracts;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ContractServiceRecordPage : CommonMethods
    {
        public ContractServiceRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By careprovidercontractserviceIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careprovidercontractservice&')]");




        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By AuditLink = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By CloneRecordButton = By.XPath("//*[@id='TI_CloneRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
        readonly By ActivateButton = By.XPath("//*[@id='TI_ActivateButton']");

        readonly By details_Tab = By.XPath("//*[@id='CWNavGroup_EditForm']");
        readonly By rates_Tab = By.XPath("//*[@id='CWNavGroup_Rates']");
        readonly By financeTransactions_Tab = By.XPath("//li/a[@title='Finance Transactions']");
        readonly By bandedRates_Tab = By.XPath("//*[@id='CWNavGroup_BandedRates']");

        readonly By topErrorLabel = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        readonly By Contractid = By.XPath("//*[@id='CWField_contractid']");
        readonly By EstablishmentprovideridLink = By.XPath("//*[@id='CWField_establishmentproviderid_Link']");
        readonly By EstablishmentprovideridLookupButton = By.XPath("//*[@id='CWLookupBtn_establishmentproviderid']");
        readonly By CareprovidercontractschemeidLink = By.XPath("//*[@id='CWField_careprovidercontractschemeid_Link']");
        readonly By CareprovidercontractschemeidLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidercontractschemeid']");
        readonly By ResponsibleuseridLink = By.XPath("//*[@id='CWField_responsibleuserid_Link']");
        readonly By ResponsibleuseridClearButton = By.XPath("//*[@id='CWClearLookup_responsibleuserid']");
        readonly By ResponsibleuseridLookupButton = By.XPath("//*[@id='CWLookupBtn_responsibleuserid']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By FunderprovideridLink = By.XPath("//*[@id='CWField_funderproviderid_Link']");
        readonly By FunderprovideridLookupButton = By.XPath("//*[@id='CWLookupBtn_funderproviderid']");
        readonly By CareproviderserviceidLink = By.XPath("//*[@id='CWField_careproviderserviceid_Link']");
        readonly By CareproviderserviceidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderserviceid']");
        readonly By CareproviderservicedetailidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderservicedetailid']");
        readonly By CpbookingtypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingtypeid']");
        readonly By CareproviderrateunitidLink = By.XPath("//*[@id='CWField_careproviderrateunitid_Link']");
        readonly By CareproviderrateunitidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderrateunitid']");
        readonly By CareproviderbatchgroupingidLink = By.XPath("//*[@id='CWField_careproviderbatchgroupingid_Link']");
        readonly By CareproviderbatchgroupingidClearButton = By.XPath("//*[@id='CWClearLookup_careproviderbatchgroupingid']");
        readonly By CareproviderbatchgroupingidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderbatchgroupingid']");
        readonly By CareprovidervatcodeidLink = By.XPath("//*[@id='CWField_careprovidervatcodeid_Link']");
        readonly By CareprovidervatcodeidClearButton = By.XPath("//*[@id='CWClearLookup_careprovidervatcodeid']");
        readonly By CareprovidervatcodeidLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidervatcodeid']");
        readonly By Isusedinfinanceinvoicebatch_1 = By.XPath("//*[@id='CWField_isusedinfinanceinvoicebatch_1']");
        readonly By Isusedinfinanceinvoicebatch_0 = By.XPath("//*[@id='CWField_isusedinfinanceinvoicebatch_0']");
        readonly By Contractservicetypeid = By.XPath("//*[@id='CWField_contractservicetypeid']");
        readonly By Isroomsapply_1 = By.XPath("//*[@id='CWField_isroomsapply_1']");
        readonly By Isroomsapply_0 = By.XPath("//*[@id='CWField_isroomsapply_0']");
        readonly By Isnetincome_1 = By.XPath("//*[@id='CWField_isnetincome_1']");
        readonly By Isnetincome_0 = By.XPath("//*[@id='CWField_isnetincome_0']");
        readonly By Datetocommenceblockcharging = By.XPath("//*[@id='CWField_datetocommenceblockcharging']");
        readonly By DatetocommenceblockchargingDatePicker = By.XPath("//*[@id='CWField_datetocommenceblockcharging_DatePicker']");
        readonly By Isusedinfinance_1 = By.XPath("//*[@id='CWField_isusedinfinance_1']");
        readonly By Isusedinfinance_0 = By.XPath("//*[@id='CWField_isusedinfinance_0']");
        readonly By Contractserviceadjusteddaysid = By.XPath("//*[@id='CWField_contractserviceadjusteddaysid']");
        readonly By Isnegotiatedratescanapply_1 = By.XPath("//*[@id='CWField_isnegotiatedratescanapply_1']");
        readonly By Isnegotiatedratescanapply_0 = By.XPath("//*[@id='CWField_isnegotiatedratescanapply_0']");
        readonly By Ispermitrateoverride_1 = By.XPath("//*[@id='CWField_ispermitrateoverride_1']");
        readonly By Ispermitrateoverride_0 = By.XPath("//*[@id='CWField_ispermitrateoverride_0']");
        readonly By Chargescheduledcareonactuals_1 = By.XPath("//*[@id='CWField_chargescheduledcareonactuals_1']");
        readonly By Chargescheduledcareonactuals_0 = By.XPath("//*[@id='CWField_chargescheduledcareonactuals_0']");
        readonly By IncomecareprovidercontractserviceidLookupButton = By.XPath("//*[@id='CWLookupBtn_incomecareprovidercontractserviceid']");
        readonly By Inactive_1 = By.XPath("//*[@id='CWField_inactive_1']");
        readonly By Inactive_0 = By.XPath("//*[@id='CWField_inactive_0']");
        readonly By Notetext = By.XPath("//*[@id='CWField_notetext']");


        public ContractServiceRecordPage WaitForContractServiceRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(careprovidercontractserviceIFrame);
            SwitchToIframe(careprovidercontractserviceIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);
            WaitForElementVisible(details_Tab);

            return this;
        }

        public ContractServiceRecordPage WaitForContractServicesPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(careprovidercontractserviceIFrame);
            SwitchToIframe(careprovidercontractserviceIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);
            WaitForElementVisible(Contractid);

            return this;
        }


        public ContractServiceRecordPage ValidatePageHeaderText(string ExpectedText)
        {
            ValidateElementText(pageHeader, ExpectedText);

            return this;
        }



        public ContractServiceRecordPage ValidateRatesTabVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(rates_Tab);
            else
                WaitForElementNotVisible(rates_Tab, 3);

            return this;
        }

        public ContractServiceRecordPage ClickRatesTab()
        {
            WaitForElementToBeClickable(rates_Tab);
            Click(rates_Tab);

            return this;
        }

        public ContractServiceRecordPage ClickFinanceTransactionsTab()
        {
            WaitForElementToBeClickable(financeTransactions_Tab);
            Click(financeTransactions_Tab);

            return this;
        }

        public ContractServiceRecordPage ValidateBandedRatesTabVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(bandedRates_Tab);
            else
                WaitForElementNotVisible(bandedRates_Tab, 3);

            return this;
        }

        public ContractServiceRecordPage ClickBandedRatesTab()
        {
            WaitForElementToBeClickable(bandedRates_Tab);
            Click(bandedRates_Tab);

            return this;
        }



        public ContractServiceRecordPage ValidateGeneralSectionFieldsVisibleForNewRecord()
        {
            WaitForElementVisible(Contractid);
            WaitForElementVisible(EstablishmentprovideridLookupButton);
            WaitForElementVisible(CareprovidercontractschemeidLookupButton);

            WaitForElementVisible(ResponsibleuseridLookupButton);
            WaitForElementVisible(ResponsibleTeamLookupButton);
            WaitForElementVisible(FunderprovideridLookupButton);

            return this;
        }

        public ContractServiceRecordPage ValidateServicesSectionFieldsVisibleForNewRecord()
        {
            WaitForElementVisible(CareproviderserviceidLookupButton);
            WaitForElementVisible(CareproviderservicedetailidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ValidateFinanceSectionFieldsVisibleForNewRecord()
        {
            WaitForElementVisible(CareproviderrateunitidLookupButton);
            WaitForElementVisible(CareproviderbatchgroupingidLookupButton);

            WaitForElementVisible(CareprovidervatcodeidLookupButton);
            WaitForElementVisible(Isusedinfinanceinvoicebatch_1);
            WaitForElementVisible(Isusedinfinanceinvoicebatch_0);

            return this;
        }

        public ContractServiceRecordPage ValidateOtherSettingsSectionFieldsVisibleForNewRecord()
        {
            WaitForElementVisible(Contractservicetypeid);
            WaitForElementVisible(Isroomsapply_1);
            WaitForElementVisible(Isroomsapply_0);
            WaitForElementVisible(Isusedinfinance_1);
            WaitForElementVisible(Isusedinfinance_0);

            WaitForElementVisible(Contractserviceadjusteddaysid);
            WaitForElementVisible(Isnegotiatedratescanapply_1);
            WaitForElementVisible(Isnegotiatedratescanapply_0);
            WaitForElementVisible(Ispermitrateoverride_1);
            WaitForElementVisible(Ispermitrateoverride_0);
            WaitForElementVisible(Inactive_1);
            WaitForElementVisible(Inactive_0);

            return this;
        }

        public ContractServiceRecordPage ValidateNotesSectionFieldsVisibleForNewRecord()
        {
            WaitForElementVisible(Notetext);

            return this;
        }



        public ContractServiceRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(AuditLink);
            Click(AuditLink);

            return this;
        }



        public ContractServiceRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public ContractServiceRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public ContractServiceRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ContractServiceRecordPage ClickCloneRecordButton()
        {
            WaitForElementToBeClickable(CloneRecordButton);
            Click(CloneRecordButton);

            return this;
        }

        public ContractServiceRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public ContractServiceRecordPage ClickActivateButton()
        {
            WaitForElementToBeClickable(ActivateButton);
            Click(ActivateButton);

            return this;
        }



        public ContractServiceRecordPage ValidateTopErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(topErrorLabel);
            else
                WaitForElementNotVisible(topErrorLabel, 3);

            return this;
        }

        public ContractServiceRecordPage ValidateTopErrorLabelText(string ExpectedText)
        {
            ValidateElementText(topErrorLabel, ExpectedText);

            return this;
        }



        public ContractServiceRecordPage ValidateIdText(string ExpectedText)
        {
            ValidateElementValue(Contractid, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage ValidateIdFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(Contractid);
            else
                ValidateElementNotDisabled(Contractid);

            return this;
        }

        public ContractServiceRecordPage InsertTextOnId(string TextToInsert)
        {
            WaitForElementToBeClickable(Contractid);
            SendKeys(Contractid, TextToInsert);

            return this;
        }

        public ContractServiceRecordPage ClickEstablishmentLink()
        {
            WaitForElementToBeClickable(EstablishmentprovideridLink);
            Click(EstablishmentprovideridLink);

            return this;
        }

        public ContractServiceRecordPage ValidateEstablishmentLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(EstablishmentprovideridLink);
            ValidateElementText(EstablishmentprovideridLink, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage ClickEstablishmentLookupButton()
        {
            WaitForElementToBeClickable(EstablishmentprovideridLookupButton);
            Click(EstablishmentprovideridLookupButton);

            return this;
        }

        public ContractServiceRecordPage ValidateEstablishmentFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(EstablishmentprovideridLookupButton);
            else
                ValidateElementNotDisabled(EstablishmentprovideridLookupButton);

            return this;
        }

        public ContractServiceRecordPage ClickContractSchemeLink()
        {
            WaitForElementToBeClickable(CareprovidercontractschemeidLink);
            Click(CareprovidercontractschemeidLink);

            return this;
        }

        public ContractServiceRecordPage ValidateContractSchemeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareprovidercontractschemeidLink);
            ValidateElementText(CareprovidercontractschemeidLink, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage ClickContractSchemeLookupButton()
        {
            WaitForElementToBeClickable(CareprovidercontractschemeidLookupButton);
            Click(CareprovidercontractschemeidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ValidateContractSchemeFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(CareprovidercontractschemeidLookupButton);
            else
                ValidateElementNotDisabled(CareprovidercontractschemeidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ClickResponsibleUserLink()
        {
            WaitForElementToBeClickable(ResponsibleuseridLink);
            Click(ResponsibleuseridLink);

            return this;
        }

        public ContractServiceRecordPage ValidateResponsibleUserLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleuseridLink);
            ValidateElementText(ResponsibleuseridLink, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage ClickResponsibleUserClearButton()
        {
            WaitForElementToBeClickable(ResponsibleuseridClearButton);
            Click(ResponsibleuseridClearButton);

            return this;
        }

        public ContractServiceRecordPage ClickResponsibleUserLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleuseridLookupButton);
            Click(ResponsibleuseridLookupButton);

            return this;
        }

        public ContractServiceRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public ContractServiceRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public ContractServiceRecordPage ValidateResponsibleTeamFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(ResponsibleTeamLookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeamLookupButton);

            return this;
        }

        public ContractServiceRecordPage ClickFunderLink()
        {
            WaitForElementToBeClickable(FunderprovideridLink);
            Click(FunderprovideridLink);

            return this;
        }

        public ContractServiceRecordPage ValidateFunderLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(FunderprovideridLink);
            ValidateElementText(FunderprovideridLink, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage ClickFunderLookupButton()
        {
            WaitForElementToBeClickable(FunderprovideridLookupButton);
            Click(FunderprovideridLookupButton);

            return this;
        }

        public ContractServiceRecordPage ValidateFunderFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(FunderprovideridLookupButton);
            else
                ValidateElementNotDisabled(FunderprovideridLookupButton);

            return this;
        }

        public ContractServiceRecordPage ClickServiceLink()
        {
            WaitForElementToBeClickable(CareproviderserviceidLink);
            Click(CareproviderserviceidLink);

            return this;
        }

        public ContractServiceRecordPage ValidateServiceLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareproviderserviceidLink);
            ValidateElementText(CareproviderserviceidLink, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage ClickServiceLookupButton()
        {
            WaitForElementToBeClickable(CareproviderserviceidLookupButton);
            Click(CareproviderserviceidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ValidateServiceFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(CareproviderserviceidLookupButton);
            else
                ValidateElementNotDisabled(CareproviderserviceidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ClickServiceDetailLookupButton()
        {
            WaitForElementToBeClickable(CareproviderservicedetailidLookupButton);
            Click(CareproviderservicedetailidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ValidateServiceDetailLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(CareproviderservicedetailidLookupButton);
            else
                WaitForElementNotVisible(CareproviderservicedetailidLookupButton, 3);

            return this;
        }

        public ContractServiceRecordPage ValidateServiceDetailFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(CareproviderservicedetailidLookupButton);
            else
                ValidateElementNotDisabled(CareproviderservicedetailidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ClickBookingTypeLookupButton()
        {
            WaitForElementToBeClickable(CpbookingtypeidLookupButton);
            Click(CpbookingtypeidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ValidateBookingTypeLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CpbookingtypeidLookupButton);
            }
            else
            {
                WaitForElementNotVisible(CpbookingtypeidLookupButton, 3);
            }

            return this;
        }

        public ContractServiceRecordPage ClickRateUnitLink()
        {
            WaitForElementToBeClickable(CareproviderrateunitidLink);
            Click(CareproviderrateunitidLink);

            return this;
        }

        public ContractServiceRecordPage ValidateRateUnitLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareproviderrateunitidLink);
            ValidateElementText(CareproviderrateunitidLink, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage ClickRateUnitLookupButton()
        {
            WaitForElementToBeClickable(CareproviderrateunitidLookupButton);
            Click(CareproviderrateunitidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ValidateRateUnitFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(CareproviderrateunitidLookupButton);
            else
                ValidateElementNotDisabled(CareproviderrateunitidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ClickBatchGroupingLink()
        {
            WaitForElementToBeClickable(CareproviderbatchgroupingidLink);
            Click(CareproviderbatchgroupingidLink);

            return this;
        }

        public ContractServiceRecordPage ValidateBatchGroupingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareproviderbatchgroupingidLink);
            ValidateElementText(CareproviderbatchgroupingidLink, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage ClickBatchGroupingClearButton()
        {
            WaitForElementToBeClickable(CareproviderbatchgroupingidClearButton);
            Click(CareproviderbatchgroupingidClearButton);

            return this;
        }

        public ContractServiceRecordPage ClickBatchGroupingLookupButton()
        {
            WaitForElementToBeClickable(CareproviderbatchgroupingidLookupButton);
            Click(CareproviderbatchgroupingidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ClickVATCodeLink()
        {
            WaitForElementToBeClickable(CareprovidervatcodeidLink);
            Click(CareprovidervatcodeidLink);

            return this;
        }

        public ContractServiceRecordPage ValidateVATCodeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareprovidervatcodeidLink);
            ValidateElementText(CareprovidervatcodeidLink, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage ClickVATCodeClearButton()
        {
            WaitForElementToBeClickable(CareprovidervatcodeidClearButton);
            Click(CareprovidervatcodeidClearButton);

            return this;
        }

        public ContractServiceRecordPage ClickVATCodeLookupButton()
        {
            WaitForElementToBeClickable(CareprovidervatcodeidLookupButton);
            Click(CareprovidervatcodeidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ClickUsedInFinanceInvoiceBatch_YesRadioButton()
        {
            WaitForElementToBeClickable(Isusedinfinanceinvoicebatch_1);
            Click(Isusedinfinanceinvoicebatch_1);

            return this;
        }

        public ContractServiceRecordPage ValidateUsedInFinanceInvoiceBatch_YesRadioButtonChecked()
        {
            WaitForElement(Isusedinfinanceinvoicebatch_1);
            ValidateElementChecked(Isusedinfinanceinvoicebatch_1);

            return this;
        }

        public ContractServiceRecordPage ValidateUsedInFinanceInvoiceBatch_YesRadioButtonNotChecked()
        {
            WaitForElement(Isusedinfinanceinvoicebatch_1);
            ValidateElementNotChecked(Isusedinfinanceinvoicebatch_1);

            return this;
        }

        public ContractServiceRecordPage ClickUsedInFinanceInvoiceBatch_NoRadioButton()
        {
            WaitForElementToBeClickable(Isusedinfinanceinvoicebatch_0);
            Click(Isusedinfinanceinvoicebatch_0);

            return this;
        }

        public ContractServiceRecordPage ValidateUsedInFinanceInvoiceBatch_NoRadioButtonChecked()
        {
            WaitForElement(Isusedinfinanceinvoicebatch_0);
            ValidateElementChecked(Isusedinfinanceinvoicebatch_0);

            return this;
        }

        public ContractServiceRecordPage ValidateUsedInFinanceInvoiceBatch_NoRadioButtonNotChecked()
        {
            WaitForElement(Isusedinfinanceinvoicebatch_0);
            ValidateElementNotChecked(Isusedinfinanceinvoicebatch_0);

            return this;
        }

        public ContractServiceRecordPage ValidateUsedInFinanceInvoiceBatchFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Isusedinfinanceinvoicebatch_1);
                ValidateElementDisabled(Isusedinfinanceinvoicebatch_0);
            }
            else
            {
                ValidateElementNotDisabled(Isusedinfinanceinvoicebatch_1);
                ValidateElementNotDisabled(Isusedinfinanceinvoicebatch_0);
            }

            return this;
        }

        public ContractServiceRecordPage SelectContractType(string TextToSelect)
        {
            WaitForElementToBeClickable(Contractservicetypeid);
            SelectPicklistElementByText(Contractservicetypeid, TextToSelect);

            return this;
        }

        public ContractServiceRecordPage ValidateContractTypeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Contractservicetypeid, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage ValidateContractTypeFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(Contractservicetypeid);
            else
                ValidateElementNotDisabled(Contractservicetypeid);

            return this;
        }

        public ContractServiceRecordPage ClickRoomsApply_YesRadioButton()
        {
            WaitForElementToBeClickable(Isroomsapply_1);
            Click(Isroomsapply_1);

            return this;
        }

        public ContractServiceRecordPage ValidateRoomsApply_YesRadioButtonChecked()
        {
            WaitForElement(Isroomsapply_1);
            ValidateElementChecked(Isroomsapply_1);

            return this;
        }

        public ContractServiceRecordPage ValidateRoomsApply_YesRadioButtonNotChecked()
        {
            WaitForElement(Isroomsapply_1);
            ValidateElementNotChecked(Isroomsapply_1);

            return this;
        }

        public ContractServiceRecordPage ClickRoomsApply_NoRadioButton()
        {
            WaitForElementToBeClickable(Isroomsapply_0);
            Click(Isroomsapply_0);

            return this;
        }

        public ContractServiceRecordPage ValidateRoomsApply_NoRadioButtonChecked()
        {
            WaitForElement(Isroomsapply_0);
            ValidateElementChecked(Isroomsapply_0);

            return this;
        }

        public ContractServiceRecordPage ValidateRoomsApply_NoRadioButtonNotChecked()
        {
            WaitForElement(Isroomsapply_0);
            ValidateElementNotChecked(Isroomsapply_0);

            return this;
        }

        public ContractServiceRecordPage ClickNetIncome_YesRadioButton()
        {
            WaitForElementToBeClickable(Isnetincome_1);
            Click(Isnetincome_1);

            return this;
        }

        public ContractServiceRecordPage ValidateNetIncome_YesRadioButtonChecked()
        {
            WaitForElement(Isnetincome_1);
            ValidateElementChecked(Isnetincome_1);

            return this;
        }

        public ContractServiceRecordPage ValidateNetIncome_YesRadioButtonNotChecked()
        {
            WaitForElement(Isnetincome_1);
            ValidateElementNotChecked(Isnetincome_1);

            return this;
        }

        public ContractServiceRecordPage ClickNetIncome_NoRadioButton()
        {
            WaitForElementToBeClickable(Isnetincome_0);
            Click(Isnetincome_0);

            return this;
        }

        public ContractServiceRecordPage ValidateNetIncome_NoRadioButtonChecked()
        {
            WaitForElement(Isnetincome_0);
            ValidateElementChecked(Isnetincome_0);

            return this;
        }

        public ContractServiceRecordPage ValidateNetIncome_NoRadioButtonNotChecked()
        {
            WaitForElement(Isnetincome_0);
            ValidateElementNotChecked(Isnetincome_0);

            return this;
        }

        public ContractServiceRecordPage ValidateNetIncomeFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Isnetincome_1);
                WaitForElementVisible(Isnetincome_0);
            }
            else
            {
                WaitForElementNotVisible(Isnetincome_1, 3);
                WaitForElementNotVisible(Isnetincome_0, 3);
            }

            return this;
        }

        public ContractServiceRecordPage ValidateNetIncomeFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
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

        public ContractServiceRecordPage ValidateDateToCommenceBlockChargingText(string ExpectedText)
        {
            ValidateElementValue(Datetocommenceblockcharging, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage InsertTextOnDateToCommenceBlockCharging(string TextToInsert)
        {
            WaitForElementToBeClickable(Datetocommenceblockcharging);
            SendKeys(Datetocommenceblockcharging, TextToInsert);

            return this;
        }

        public ContractServiceRecordPage ClickDateToCommenceBlockChargingDatePicker()
        {
            WaitForElementToBeClickable(DatetocommenceblockchargingDatePicker);
            Click(DatetocommenceblockchargingDatePicker);

            return this;
        }

        public ContractServiceRecordPage ClickUsedInFinance_YesRadioButton()
        {
            WaitForElementToBeClickable(Isusedinfinance_1);
            Click(Isusedinfinance_1);

            return this;
        }

        public ContractServiceRecordPage ValidateUsedInFinance_YesRadioButtonChecked()
        {
            WaitForElement(Isusedinfinance_1);
            ValidateElementChecked(Isusedinfinance_1);

            return this;
        }

        public ContractServiceRecordPage ValidateUsedInFinance_YesRadioButtonNotChecked()
        {
            WaitForElement(Isusedinfinance_1);
            ValidateElementNotChecked(Isusedinfinance_1);

            return this;
        }

        public ContractServiceRecordPage ClickUsedInFinance_NoRadioButton()
        {
            WaitForElementToBeClickable(Isusedinfinance_0);
            Click(Isusedinfinance_0);

            return this;
        }

        public ContractServiceRecordPage ValidateUsedInFinance_NoRadioButtonChecked()
        {
            WaitForElement(Isusedinfinance_0);
            ValidateElementChecked(Isusedinfinance_0);

            return this;
        }

        public ContractServiceRecordPage ValidateUsedInFinance_NoRadioButtonNotChecked()
        {
            WaitForElement(Isusedinfinance_0);
            ValidateElementNotChecked(Isusedinfinance_0);

            return this;
        }

        public ContractServiceRecordPage ValidateUsedInFinanceFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Isusedinfinance_1);
                ValidateElementDisabled(Isusedinfinance_0);
            }
            else
            {
                ValidateElementNotDisabled(Isusedinfinance_1);
                ValidateElementNotDisabled(Isusedinfinance_0);
            }

            return this;
        }

        public ContractServiceRecordPage SelectAdjustedDays(string TextToSelect)
        {
            WaitForElementToBeClickable(Contractserviceadjusteddaysid);
            SelectPicklistElementByText(Contractserviceadjusteddaysid, TextToSelect);

            return this;
        }

        public ContractServiceRecordPage ValidateAdjustedDaysSelectedText(string ExpectedText)
        {
            ValidateElementText(Contractserviceadjusteddaysid, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage ClickNegotiatedRatesApply_YesRadioButton()
        {
            WaitForElementToBeClickable(Isnegotiatedratescanapply_1);
            Click(Isnegotiatedratescanapply_1);

            return this;
        }

        public ContractServiceRecordPage ValidateNegotiatedRatesApply_YesRadioButtonChecked()
        {
            WaitForElement(Isnegotiatedratescanapply_1);
            ValidateElementChecked(Isnegotiatedratescanapply_1);

            return this;
        }

        public ContractServiceRecordPage ValidateNegotiatedRatesApply_YesRadioButtonNotChecked()
        {
            WaitForElement(Isnegotiatedratescanapply_1);
            ValidateElementNotChecked(Isnegotiatedratescanapply_1);

            return this;
        }

        public ContractServiceRecordPage ClickNegotiatedRatesApply_NoRadioButton()
        {
            WaitForElementToBeClickable(Isnegotiatedratescanapply_0);
            Click(Isnegotiatedratescanapply_0);

            return this;
        }

        public ContractServiceRecordPage ValidateNegotiatedRatesApply_NoRadioButtonChecked()
        {
            WaitForElement(Isnegotiatedratescanapply_0);
            ValidateElementChecked(Isnegotiatedratescanapply_0);

            return this;
        }

        public ContractServiceRecordPage ValidateNegotiatedRatesApply_NoRadioButtonNotChecked()
        {
            WaitForElement(Isnegotiatedratescanapply_0);
            ValidateElementNotChecked(Isnegotiatedratescanapply_0);

            return this;
        }

        public ContractServiceRecordPage ValidateNegotiatedRatesApplyFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(Isnegotiatedratescanapply_1);
                ValidateElementDisabled(Isnegotiatedratescanapply_0);
            }
            else
            {
                ValidateElementNotDisabled(Isnegotiatedratescanapply_1);
                ValidateElementNotDisabled(Isnegotiatedratescanapply_0);
            }

            return this;
        }

        public ContractServiceRecordPage ValidatePermitRateOverrideFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
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

        public ContractServiceRecordPage ClickPermitRateOverride_YesRadioButton()
        {
            WaitForElementToBeClickable(Ispermitrateoverride_1);
            Click(Ispermitrateoverride_1);

            return this;
        }

        public ContractServiceRecordPage ValidatePermitRateOverride_YesRadioButtonChecked()
        {
            WaitForElement(Ispermitrateoverride_1);
            ValidateElementChecked(Ispermitrateoverride_1);

            return this;
        }

        public ContractServiceRecordPage ValidatePermitRateOverride_YesRadioButtonNotChecked()
        {
            WaitForElement(Ispermitrateoverride_1);
            ValidateElementNotChecked(Ispermitrateoverride_1);

            return this;
        }

        public ContractServiceRecordPage ClickPermitRateOverride_NoRadioButton()
        {
            WaitForElementToBeClickable(Ispermitrateoverride_0);
            Click(Ispermitrateoverride_0);

            return this;
        }

        public ContractServiceRecordPage ValidatePermitRateOverride_NoRadioButtonChecked()
        {
            WaitForElement(Ispermitrateoverride_0);
            ValidateElementChecked(Ispermitrateoverride_0);

            return this;
        }

        public ContractServiceRecordPage ValidatePermitRateOverride_NoRadioButtonNotChecked()
        {
            WaitForElement(Ispermitrateoverride_0);
            ValidateElementNotChecked(Ispermitrateoverride_0);

            return this;
        }

        public ContractServiceRecordPage ClickChargeScheduledCareOnActuals_YesRadioButton()
        {
            WaitForElementToBeClickable(Chargescheduledcareonactuals_1);
            Click(Chargescheduledcareonactuals_1);

            return this;
        }

        public ContractServiceRecordPage ValidateChargeScheduledCareOnActuals_YesRadioButtonChecked()
        {
            WaitForElement(Chargescheduledcareonactuals_1);
            ValidateElementChecked(Chargescheduledcareonactuals_1);

            return this;
        }

        public ContractServiceRecordPage ValidateChargeScheduledCareOnActuals_YesRadioButtonNotChecked()
        {
            WaitForElement(Chargescheduledcareonactuals_1);
            ValidateElementNotChecked(Chargescheduledcareonactuals_1);

            return this;
        }

        public ContractServiceRecordPage ClickChargeScheduledCareOnActuals_NoRadioButton()
        {
            WaitForElementToBeClickable(Chargescheduledcareonactuals_0);
            Click(Chargescheduledcareonactuals_0);

            return this;
        }

        public ContractServiceRecordPage ValidateChargeScheduledCareOnActuals_NoRadioButtonChecked()
        {
            WaitForElement(Chargescheduledcareonactuals_0);
            ValidateElementChecked(Chargescheduledcareonactuals_0);

            return this;
        }

        public ContractServiceRecordPage ValidateChargeScheduledCareOnActuals_NoRadioButtonNotChecked()
        {
            WaitForElement(Chargescheduledcareonactuals_0);
            ValidateElementNotChecked(Chargescheduledcareonactuals_0);

            return this;
        }

        public ContractServiceRecordPage ValidateChargeScheduledCareOnActualsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Chargescheduledcareonactuals_1);
                WaitForElementVisible(Chargescheduledcareonactuals_0);
            }
            else
            {
                WaitForElementNotVisible(Chargescheduledcareonactuals_1, 3);
                WaitForElementNotVisible(Chargescheduledcareonactuals_0, 3);
            }

            return this;
        }

        public ContractServiceRecordPage ValidateChargeScheduledCareOnActualsDisabled(bool ExpectDisabled)
        {
            WaitForElementVisible(Chargescheduledcareonactuals_0);
            WaitForElementVisible(Chargescheduledcareonactuals_1);
            MoveToElementInPage(Chargescheduledcareonactuals_1);

            if (ExpectDisabled)
            {
                ValidateElementDisabled(Chargescheduledcareonactuals_1);
                ValidateElementDisabled(Chargescheduledcareonactuals_0);
            }
            else
            {
                ValidateElementNotDisabled(Chargescheduledcareonactuals_1);
                ValidateElementNotDisabled(Chargescheduledcareonactuals_0);
            }

            return this;
        }

        public ContractServiceRecordPage ClickIncomeContractServiceLookupButton()
        {
            WaitForElementToBeClickable(IncomecareprovidercontractserviceidLookupButton);
            Click(IncomecareprovidercontractserviceidLookupButton);

            return this;
        }

        public ContractServiceRecordPage ClickInactive_YesRadioButton()
        {
            WaitForElementToBeClickable(Inactive_1);
            Click(Inactive_1);

            return this;
        }

        public ContractServiceRecordPage ValidateInactive_YesRadioButtonChecked()
        {
            WaitForElement(Inactive_1);
            ValidateElementChecked(Inactive_1);

            return this;
        }

        public ContractServiceRecordPage ValidateInactive_YesRadioButtonNotChecked()
        {
            WaitForElement(Inactive_1);
            ValidateElementNotChecked(Inactive_1);

            return this;
        }

        public ContractServiceRecordPage ClickInactive_NoRadioButton()
        {
            WaitForElementToBeClickable(Inactive_0);
            Click(Inactive_0);

            return this;
        }

        public ContractServiceRecordPage ValidateInactive_NoRadioButtonChecked()
        {
            WaitForElement(Inactive_0);
            ValidateElementChecked(Inactive_0);

            return this;
        }

        public ContractServiceRecordPage ValidateInactive_NoRadioButtonNotChecked()
        {
            WaitForElement(Inactive_0);
            ValidateElementNotChecked(Inactive_0);

            return this;
        }

        public ContractServiceRecordPage ValidateNoteTextText(string ExpectedText)
        {
            ValidateElementText(Notetext, ExpectedText);

            return this;
        }

        public ContractServiceRecordPage InsertTextOnNoteText(string TextToInsert)
        {
            WaitForElementToBeClickable(Notetext);
            SendKeys(Notetext, TextToInsert);

            return this;
        }

    }
}
