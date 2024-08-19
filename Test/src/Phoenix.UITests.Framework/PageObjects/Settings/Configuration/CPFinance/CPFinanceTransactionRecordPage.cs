using OpenQA.Selenium;
using Phoenix.UITests.Framework.WebAppAPI.Classes;
using System.Diagnostics.Contracts;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinance
{
    public class CPFinanceTransactionRecordPage : CommonMethods
    {
        public CPFinanceTransactionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region IFrame Locators

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By cwDialogIFrame_financeTransactions = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=careproviderfinancetransaction&')]");

        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");

        By cwDialog_IFrame(string parentRecordIdSuffix) => By.XPath("//iframe[contains(@id, 'iframe_CWDialog_" + parentRecordIdSuffix + "')]");

        By cwDialog_TypeId(string parentRecordBoName) => By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=" + parentRecordBoName + "')]");

        #endregion

        #region Page Element Locators

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']//h1//span");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By CloneButton = By.XPath("//*[@id='TI_CloneButton']");
        readonly By CancelButton = By.XPath("//*[@id='TI_CancelButton']");
        readonly By ToolbarDropdownButton = By.XPath("//*[@id = 'CWToolbarMenu']");
        readonly By DropDownMenuArea = By.XPath("//*[@class = 'dropdown-menu right-caret show']");
        readonly By CopyRecordLinkButton = By.XPath("//*[@id = 'TI_CopyRecordLink']");

        readonly By confirmed_1 = By.XPath("//*[@id='CWField_confirmed_1']");
        readonly By confirmed_0 = By.XPath("//*[@id='CWField_confirmed_0']");

        readonly By apportionedLabel = By.Id("CWLabelHolder_isapportioned");
        readonly By apportioned_1 = By.XPath("//*[@id='CWField_isapportioned_1']");
        readonly By apportioned_0 = By.XPath("//*[@id='CWField_isapportioned_0']");

        readonly By TransactionClassPicklist = By.XPath("//*[@id='CWField_transactionclassid']");

        readonly By NetAmount = By.XPath("//*[@id='CWField_netamount']");
        readonly By VatAmount = By.XPath("//*[@id='CWField_vatamount']");
        readonly By GrossAmount = By.XPath("//*[@id='CWField_grossamount']");

        readonly By TransactionText = By.XPath("//*[@id='CWField_transactiontext']");

        readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
        readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
        readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
        readonly By Starttime = By.XPath("//*[@id='CWField_starttime']");
        readonly By Starttime_TimePicker = By.XPath("//*[@id='CWField_starttime_TimePicker']");
        readonly By Transactionclassid = By.XPath("//*[@id='CWField_transactionclassid']");
        readonly By Careproviderfinancetransactionnumber = By.XPath("//*[@id='CWField_careproviderfinancetransactionnumber']");
        readonly By Isapportioned_1 = By.XPath("//*[@id='CWField_isapportioned_1']");
        readonly By Isapportioned_0 = By.XPath("//*[@id='CWField_isapportioned_0']");
        readonly By Isforinformationonly_1 = By.XPath("//*[@id='CWField_isforinformationonly_1']");
        readonly By Isforinformationonly_0 = By.XPath("//*[@id='CWField_isforinformationonly_0']");
        readonly By Financecode = By.XPath("//*[@id='CWField_financecode']");
        readonly By CWFieldButton_financecode = By.XPath("//*[@id='CWFieldButton_financecode']");
        readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");
        readonly By EnddateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
        readonly By Endtime = By.XPath("//*[@id='CWField_endtime']");
        readonly By Endtime_TimePicker = By.XPath("//*[@id='CWField_endtime_TimePicker']");
        readonly By Accountcode = By.XPath("//*[@id='CWField_accountcode']");
        readonly By Transactionnocontrad = By.XPath("//*[@id='CWField_transactionnocontrad']");
        readonly By Enddateweeklycharge = By.XPath("//*[@id='CWField_enddateweeklycharge']");
        readonly By CareprovidervatcodeidLink = By.XPath("//*[@id='CWField_careprovidervatcodeid_Link']");
        readonly By CareprovidervatcodeidClearButton = By.XPath("//*[@id='CWClearLookup_careprovidervatcodeid']");
        readonly By CareprovidervatcodeidLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidervatcodeid']");
        readonly By Vatreference = By.XPath("//*[@id='CWField_vatreference']");
        readonly By CareproviderpersoncontractserviceidLink = By.XPath("//*[@id='CWField_careproviderpersoncontractserviceid_Link']");
        readonly By CareproviderpersoncontractserviceidClearButton = By.XPath("//*[@id='CWClearLookup_careproviderpersoncontractserviceid']");
        readonly By CareproviderpersoncontractserviceidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderpersoncontractserviceid']");
        readonly By CpbookingscheduleidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingscheduleid']");
        readonly By CpbookingdiaryidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingdiaryid']");
        readonly By CpbookingdiaryidLink = By.XPath("//*[@id='CWField_cpbookingdiaryid_Link']");
        readonly By EstablishmentprovideridLink = By.XPath("//*[@id='CWField_establishmentproviderid_Link']");
        readonly By EstablishmentprovideridLookupButton = By.XPath("//*[@id='CWLookupBtn_establishmentproviderid']");
        readonly By CareprovidercontractschemeidLink = By.XPath("//*[@id='CWField_careprovidercontractschemeid_Link']");
        readonly By CareprovidercontractschemeidLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidercontractschemeid']");
        readonly By CareproviderrateunitidLink = By.XPath("//*[@id='CWField_careproviderrateunitid_Link']");
        readonly By CareproviderrateunitidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderrateunitid']");
        readonly By Totalunits = By.XPath("//*[@id='CWField_totalunits']");
        readonly By CareprovidercontractserviceidLink = By.XPath("//*[@id='CWField_careprovidercontractserviceid_Link']");
        readonly By CareprovidercontractserviceidLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidercontractserviceid']");
        readonly By ExpensetypeidLink = By.XPath("//*[@id='CWField_expensetypeid_Link']");
        readonly By ExpensetypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_expensetypeid']");
        readonly By PayeridLink = By.XPath("//*[@id='CWField_payerid_Link']");
        readonly By PayeridLookupButton = By.XPath("//*[@id='CWLookupBtn_payerid']");
        readonly By CpbookingtypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingtypeid']");
        readonly By CpbookingtypeidLink = By.XPath("//*[@id='CWField_cpbookingtypeid_Link']");
        readonly By FunderprovideridLink = By.XPath("//*[@id='CWField_funderproviderid_Link']");
        readonly By FunderprovideridLookupButton = By.XPath("//*[@id='CWLookupBtn_funderproviderid']");
        readonly By CareproviderserviceidLink = By.XPath("//*[@id='CWField_careproviderserviceid_Link']");
        readonly By CareproviderserviceidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderserviceid']");
        readonly By CareproviderservicedetailidLink = By.XPath("//*[@id='CWField_careproviderservicedetailid_Link']");
        readonly By CareproviderservicedetailidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderservicedetailid']");
        readonly By Chargeableunits = By.XPath("//*[@id='CWField_chargeableunits']");
        readonly By Contractservicetypeid = By.XPath("//*[@id='CWField_contractservicetypeid']");
        readonly By AdhocexpenseidLink = By.XPath("//*[@id='CWField_adhocexpenseid_Link']");
        readonly By AdhocexpenseidLookupButton = By.XPath("//*[@id='CWLookupBtn_adhocexpenseid']");
        readonly By CpbookingschedulestaffidLink = By.XPath("//*[@id='CWField_cpbookingschedulestaffid_Link']");
        readonly By CpbookingschedulestaffidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingschedulestaffid']");
        readonly By CpbookingdiarystaffidLink = By.XPath("//*[@id='CWField_cpbookingdiarystaffid_Link']");
        readonly By CpbookingdiarystaffidLookupButton = By.XPath("//*[@id='CWLookupBtn_cpbookingdiarystaffid']");
        readonly By CareproviderfinanceinvoiceidLink = By.XPath("//*[@id='CWField_careproviderfinanceinvoiceid_Link']");
        readonly By CareproviderfinanceinvoiceidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderfinanceinvoiceid']");
        readonly By Financeinvoicestatusid = By.XPath("//*[@id='CWField_financeinvoicestatusid']");
        readonly By CareproviderfinanceinvoicebatchsetupidLink = By.XPath("//*[@id='CWField_careproviderfinanceinvoicebatchsetupid_Link']");
        readonly By CareproviderfinanceinvoicebatchsetupidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderfinanceinvoicebatchsetupid']");
        readonly By Invoiceno = By.XPath("//*[@id='CWField_invoicenumber']");
        readonly By CareproviderfinanceinvoicebatchidLink = By.XPath("//*[@id='CWField_careproviderfinanceinvoicebatchid_Link']");
        readonly By CareproviderfinanceinvoicebatchidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderfinanceinvoicebatchid']");
        readonly By CareproviderfinanceextractbatchidLink = By.XPath("//*[@id='CWField_careproviderfinanceextractbatchid_Link']");
        readonly By CareproviderfinanceextractbatchidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderfinanceextractbatchid']");
        readonly By Extractno = By.XPath("//*[@id='CWField_extractno']");
        readonly By CareproviderfinanceextractbatchsetupidLink = By.XPath("//*[@id='CWField_careproviderfinanceextractbatchsetupid_Link']");
        readonly By CareproviderfinanceextractbatchsetupidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderfinanceextractbatchsetupid']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By Notetext = By.XPath("//*[@id='CWField_notetext']");
        readonly By Calculationtrace = By.XPath("//*[@id='CWField_calculationtrace']");

        readonly By RosteredEmployeeLink = By.XPath("//*[@id='CWField_systemuserid_Link']");
        readonly By RosteredEmployeeLookupButton = By.XPath("//*[@id='CWLookupBtn_systemuserid']");


        #endregion


        public CPFinanceTransactionRecordPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(cwDialogIFrame_financeTransactions);
            SwitchToIframe(cwDialogIFrame_financeTransactions);

            WaitForElementNotVisible("CWRefreshPanel", 20);


            return this;
        }

        public CPFinanceTransactionRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);
            
            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickBackButton()
        {
            ScrollToElement(BackButton);
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            ScrollToElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(CloneButton);
            Click(CloneButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickCancelButton()
        {
            WaitForElementToBeClickable(CancelButton);
            Click(CancelButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateSaveButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(SaveButton);
            else
                WaitForElementNotVisible(SaveButton, 3);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            ScrollToElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateSaveAndCloseButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(SaveAndCloseButton);
            else
                WaitForElementNotVisible(SaveAndCloseButton, 3);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateCopyRecordLinkButtonIsDisplayed(bool ExpectedDisplayed)
        {
            bool isToolbarButtonClicked = GetElementVisibility(DropDownMenuArea);

            if (!isToolbarButtonClicked)
            {
                WaitForElementToBeClickable(ToolbarDropdownButton);
                ScrollToElement(ToolbarDropdownButton);
                Click(ToolbarDropdownButton);
            }

            if (ExpectedDisplayed)
            {
                WaitForElementVisible(CopyRecordLinkButton);
            }
            else
            {
                WaitForElementNotVisible(CopyRecordLinkButton, 3);
            }
            return this;
        }

        public CPFinanceTransactionRecordPage ClickCopyRecordLinkButton()
        {
            WaitForElementToBeClickable(ToolbarDropdownButton);
            ScrollToElement(ToolbarDropdownButton);
            Click(ToolbarDropdownButton);

            WaitForElementToBeClickable(CopyRecordLinkButton);
            ScrollToElement(CopyRecordLinkButton);
            Click(CopyRecordLinkButton);

            return this;
        }

        //method to validate confirm_1 field is checked
        public CPFinanceTransactionRecordPage ValidateConfirmed_YesRadioButtonIsChecked()
        {
            ScrollToElement(confirmed_1);
            ValidateElementChecked(confirmed_1);

            return this;
        }

        //method to validate confirm_1 field is not checked
        public CPFinanceTransactionRecordPage ValidateConfirmed_YesRadioButtonIsNotChecked()
        {
            ScrollToElement(confirmed_1);
            ValidateElementNotChecked(confirmed_1);

            return this;
        }

        //method to validate confirm_0 field is checked
        public CPFinanceTransactionRecordPage ValidateConfirmed_NoRadioButtonIsChecked()
        {
            ScrollToElement(confirmed_0);
            ValidateElementChecked(confirmed_0);

            return this;
        }

        //method to validate confirm_0 field is not checked
        public CPFinanceTransactionRecordPage ValidateConfirmed_NoRadioButtonIsNotChecked()
        {
            ScrollToElement(confirmed_0);
            ValidateElementNotChecked(confirmed_0);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidatePageHeader(string ExpectedText)
        {
            WaitForElement(pageHeader);
            ValidateElementText(pageHeader, ExpectedText);

            return this;
        }

        //method to validate net amount field
        public CPFinanceTransactionRecordPage ValidateNetAmount(string ExpectedText)
        {
            ScrollToElement(NetAmount);
            ValidateElementValue(NetAmount, ExpectedText);

            return this;
        }

        //method to validate vat amount field
        public CPFinanceTransactionRecordPage ValidateVatAmount(string ExpectedText)
        {
            ScrollToElement(VatAmount);
            ValidateElementValue(VatAmount, ExpectedText);

            return this;
        }

        //method to validate gross amount field
        public CPFinanceTransactionRecordPage ValidateGrossAmount(string ExpectedText)
        {
            ScrollToElement(GrossAmount);
            ValidateElementValue(GrossAmount, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateTransactionText(string ExpectedText)
        {
            ScrollToElement(TransactionText);
            ValidateElementValue(TransactionText, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnTransactionText(string TextToInsert)
        {
            WaitForElementToBeClickable(TransactionText);
            SendKeys(TransactionText, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateApportioned_YesRadioButtonIsChecked(bool IsApportionedChecked)
        {
            ScrollToElement(apportionedLabel);

            if (IsApportionedChecked)
            {
                ValidateElementChecked(apportioned_1);
                ValidateElementNotChecked(apportioned_0);
            }
            else
            {
                ValidateElementNotChecked(apportioned_1);
                ValidateElementChecked(apportioned_0);
            }            

            return this;
        }        
       

        public CPFinanceTransactionRecordPage ClickPersonLink()
        {
            WaitForElementToBeClickable(PersonidLink);
            Click(PersonidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElement(PersonidLink);
            ValidateElementText(PersonidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateStartDateText(string ExpectedText)
        {
            ValidateElementValue(Startdate, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Startdate);
            SendKeys(Startdate, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickStartDateDatePicker()
        {
            WaitForElementToBeClickable(StartdateDatePicker);
            Click(StartdateDatePicker);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateStartTimeText(string ExpectedText)
        {
            ValidateElementValue(Starttime, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnStartTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Starttime);
            SendKeys(Starttime, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickStartTime_TimePicker()
        {
            WaitForElementToBeClickable(Starttime_TimePicker);
            Click(Starttime_TimePicker);

            return this;
        }

        public CPFinanceTransactionRecordPage SelectTransactionClass(string TextToSelect)
        {
            WaitForElementToBeClickable(Transactionclassid);
            SelectPicklistElementByText(Transactionclassid, TextToSelect);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateTransactionClassSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Transactionclassid, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateTransactionNoText(string ExpectedText)
        {
            ValidateElementValue(Careproviderfinancetransactionnumber, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnTransactionNo(string TextToInsert)
        {
            WaitForElementToBeClickable(Careproviderfinancetransactionnumber);
            SendKeys(Careproviderfinancetransactionnumber, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickApportioned_YesRadioButton()
        {
            WaitForElementToBeClickable(Isapportioned_1);
            Click(Isapportioned_1);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateApportioned_YesRadioButtonChecked()
        {
            WaitForElement(Isapportioned_1);
            ValidateElementChecked(Isapportioned_1);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateApportioned_YesRadioButtonNotChecked()
        {
            WaitForElement(Isapportioned_1);
            ValidateElementNotChecked(Isapportioned_1);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickApportioned_NoRadioButton()
        {
            WaitForElementToBeClickable(Isapportioned_0);
            Click(Isapportioned_0);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateApportioned_NoRadioButtonChecked()
        {
            WaitForElement(Isapportioned_0);
            ValidateElementChecked(Isapportioned_0);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateApportioned_NoRadioButtonNotChecked()
        {
            WaitForElement(Isapportioned_0);
            ValidateElementNotChecked(Isapportioned_0);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickForInformationOnly_YesRadioButton()
        {
            WaitForElementToBeClickable(Isforinformationonly_1);
            Click(Isforinformationonly_1);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateForInformationOnly_YesRadioButtonChecked()
        {
            WaitForElement(Isforinformationonly_1);
            ValidateElementChecked(Isforinformationonly_1);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateForInformationOnly_YesRadioButtonNotChecked()
        {
            WaitForElement(Isforinformationonly_1);
            ValidateElementNotChecked(Isforinformationonly_1);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickForInformationOnly_NoRadioButton()
        {
            WaitForElementToBeClickable(Isforinformationonly_0);
            Click(Isforinformationonly_0);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateForInformationOnly_NoRadioButtonChecked()
        {
            WaitForElement(Isforinformationonly_0);
            ValidateElementChecked(Isforinformationonly_0);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateForInformationOnly_NoRadioButtonNotChecked()
        {
            WaitForElement(Isforinformationonly_0);
            ValidateElementNotChecked(Isforinformationonly_0);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateFinanceCodeText(string ExpectedText)
        {
            ValidateElementValue(Financecode, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnFinanceCode(string TextToInsert)
        {
            WaitForElementToBeClickable(Financecode);
            SendKeys(Financecode, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickUpdateFinanceCodeButton()
        {
            WaitForElementToBeClickable(CWFieldButton_financecode);
            Click(CWFieldButton_financecode);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateEndDateText(string ExpectedText)
        {
            ValidateElementValue(Enddate, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Enddate);
            SendKeys(Enddate, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickEndDateDatePicker()
        {
            WaitForElementToBeClickable(EnddateDatePicker);
            Click(EnddateDatePicker);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateEndTimeText(string ExpectedText)
        {
            ValidateElementValue(Endtime, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnEndTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Endtime);
            SendKeys(Endtime, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickEndTime_TimePicker()
        {
            WaitForElementToBeClickable(Endtime_TimePicker);
            Click(Endtime_TimePicker);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateAccountCodeText(string ExpectedText)
        {
            ValidateElementValue(Accountcode, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateAccountCodeFieldEnabled(bool ExpectEnabled)
        {
            if(ExpectEnabled)
            {
                ValidateElementNotDisabled(Accountcode);
            }else
            {
                ValidateElementDisabled(Accountcode);
            }
            
            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnAccountCode(string TextToInsert)
        {
            WaitForElementToBeClickable(Accountcode);
            SendKeys(Accountcode, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateTransactionNoContradText(string ExpectedText)
        {
            ValidateElementValue(Transactionnocontrad, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnTransactionNoContrad(string TextToInsert)
        {
            WaitForElementToBeClickable(Transactionnocontrad);
            SendKeys(Transactionnocontrad, TextToInsert);
            return this;
        }

        public CPFinanceTransactionRecordPage ValidateEndDateWeeklyChargeText(string ExpectedText)
        {
            ValidateElementValue(Enddateweeklycharge, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnEndDateWeeklyCharge(string TextToInsert)
        {
            WaitForElementToBeClickable(Enddateweeklycharge);
            SendKeys(Enddateweeklycharge, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickVATCodeLink()
        {
            WaitForElementToBeClickable(CareprovidervatcodeidLink);
            Click(CareprovidervatcodeidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateVATCodeText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareprovidervatcodeidLink);
            ValidateElementText(CareprovidervatcodeidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickVATCodeClearButton()
        {
            WaitForElementToBeClickable(CareprovidervatcodeidClearButton);
            Click(CareprovidervatcodeidClearButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickVATCodeLookupButton()
        {
            WaitForElementToBeClickable(CareprovidervatcodeidLookupButton);
            Click(CareprovidervatcodeidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateVatReferenceText(string ExpectedText)
        {
            ValidateElementValue(Vatreference, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnVatReference(string TextToInsert)
        {
            WaitForElementToBeClickable(Vatreference);
            SendKeys(Vatreference, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickPersonContractServiceLink()
        {
            WaitForElementToBeClickable(CareproviderpersoncontractserviceidLink);
            Click(CareproviderpersoncontractserviceidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidatePersonContractServiceText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareproviderpersoncontractserviceidLink);
            ValidateElementText(CareproviderpersoncontractserviceidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickPersonContractServiceClearButton()
        {
            WaitForElementToBeClickable(CareproviderpersoncontractserviceidClearButton);
            Click(CareproviderpersoncontractserviceidClearButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickPersonContractServiceLookupButton()
        {
            WaitForElementToBeClickable(CareproviderpersoncontractserviceidLookupButton);
            Click(CareproviderpersoncontractserviceidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickBookingScheduleLookupButton()
        {
            WaitForElementToBeClickable(CpbookingscheduleidLookupButton);
            Click(CpbookingscheduleidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickBookingDiaryLookupButton()
        {
            WaitForElementToBeClickable(CpbookingdiaryidLookupButton);
            Click(CpbookingdiaryidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateBookingDiaryLinkText(string ExpectedText)
        {
            WaitForElement(CpbookingdiaryidLink);
            ValidateElementText(CpbookingdiaryidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickEstablishmentLink()
        {
            WaitForElementToBeClickable(EstablishmentprovideridLink);
            Click(EstablishmentprovideridLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateEstablishmentLinkText(string ExpectedText)
        {
            WaitForElement(EstablishmentprovideridLink);
            ValidateElementText(EstablishmentprovideridLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickEstablishmentLookupButton()
        {
            WaitForElementToBeClickable(EstablishmentprovideridLookupButton);
            Click(EstablishmentprovideridLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickContractSchemeLink()
        {
            WaitForElementToBeClickable(CareprovidercontractschemeidLink);
            Click(CareprovidercontractschemeidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateContractSchemeLinkText(string ExpectedText)
        {
            WaitForElement(CareprovidercontractschemeidLink);
            ValidateElementText(CareprovidercontractschemeidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickContractSchemeLookupButton()
        {
            WaitForElementToBeClickable(CareprovidercontractschemeidLookupButton);
            Click(CareprovidercontractschemeidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickRateUnitLink()
        {
            WaitForElementToBeClickable(CareproviderrateunitidLink);
            Click(CareproviderrateunitidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateRateUnitLinkText(string ExpectedText)
        {
            WaitForElement(CareproviderrateunitidLink);
            ValidateElementText(CareproviderrateunitidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickRateUnitLookupButton()
        {
            WaitForElementToBeClickable(CareproviderrateunitidLookupButton);
            Click(CareproviderrateunitidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateTotalUnitsText(string ExpectedText)
        {
            ValidateElementValue(Totalunits, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnTotalUnits(string TextToInsert)
        {
            WaitForElementToBeClickable(Totalunits);
            SendKeys(Totalunits, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickContractServiceLink()
        {
            WaitForElementToBeClickable(CareprovidercontractserviceidLink);
            Click(CareprovidercontractserviceidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateContractServiceLinkText(string ExpectedText)
        {
            WaitForElement(CareprovidercontractserviceidLink);
            ValidateElementText(CareprovidercontractserviceidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickContractServiceLookupButton()
        {
            WaitForElementToBeClickable(CareprovidercontractserviceidLookupButton);
            Click(CareprovidercontractserviceidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickExpenseTypeLink()
        {
            WaitForElementToBeClickable(ExpensetypeidLink);
            Click(ExpensetypeidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateExpenseTypeLinkText(string ExpectedText)
        {
            WaitForElement(ExpensetypeidLink);
            ValidateElementText(ExpensetypeidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickExpenseTypeLookupButton()
        {
            WaitForElementToBeClickable(ExpensetypeidLookupButton);
            Click(ExpensetypeidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickPayerLink()
        {
            WaitForElementToBeClickable(PayeridLink);
            Click(PayeridLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidatePayerLinkText(string ExpectedText)
        {
            WaitForElement(PayeridLink);
            ValidateElementText(PayeridLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickPayerLookupButton()
        {
            WaitForElementToBeClickable(PayeridLookupButton);
            Click(PayeridLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickBookingTypeLookupButton()
        {
            WaitForElementToBeClickable(CpbookingtypeidLookupButton);
            Click(CpbookingtypeidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateBookingTypeLinkText(string ExpectedText)
        {
            WaitForElement(CpbookingtypeidLink);
            ValidateElementText(CpbookingtypeidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickFunderLink()
        {
            WaitForElementToBeClickable(FunderprovideridLink);
            Click(FunderprovideridLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateFunderLinkText(string ExpectedText)
        {
            WaitForElement(FunderprovideridLink);
            ValidateElementText(FunderprovideridLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickFunderLookupButton()
        {
            WaitForElementToBeClickable(FunderprovideridLookupButton);
            Click(FunderprovideridLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickServiceLink()
        {
            WaitForElementToBeClickable(CareproviderserviceidLink);
            Click(CareproviderserviceidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateServiceLinkText(string ExpectedText)
        {
            WaitForElement(CareproviderserviceidLink);
            ValidateElementText(CareproviderserviceidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickServiceLookupButton()
        {
            WaitForElementToBeClickable(CareproviderserviceidLookupButton);
            Click(CareproviderserviceidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickServiceDetailLink()
        {
            WaitForElementToBeClickable(CareproviderservicedetailidLink);
            Click(CareproviderservicedetailidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateServiceDetailLinkText(string ExpectedText)
        {
            WaitForElement(CareproviderservicedetailidLink);
            ValidateElementText(CareproviderservicedetailidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickServiceDetailLookupButton()
        {
            WaitForElementToBeClickable(CareproviderservicedetailidLookupButton);
            Click(CareproviderservicedetailidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateChargeableUnitsText(string ExpectedText)
        {
            ValidateElementValue(Chargeableunits, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnChargeableUnits(string TextToInsert)
        {
            WaitForElementToBeClickable(Chargeableunits);
            SendKeys(Chargeableunits, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage SelectContractType(string TextToSelect)
        {
            WaitForElementToBeClickable(Contractservicetypeid);
            SelectPicklistElementByText(Contractservicetypeid, TextToSelect);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateContractTypeSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Contractservicetypeid, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickAdHocExpenseLink()
        {
            WaitForElementToBeClickable(AdhocexpenseidLink);
            Click(AdhocexpenseidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateAdHocExpenseLinkText(string ExpectedText)
        {
            WaitForElement(AdhocexpenseidLink);
            ValidateElementText(AdhocexpenseidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickAdHocExpenseLookupButton()
        {
            WaitForElementToBeClickable(AdhocexpenseidLookupButton);
            Click(AdhocexpenseidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateScheduleStaffMemberFieldVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
            {
                WaitForElementVisible(CpbookingschedulestaffidLookupButton);
            }
            else
            {
                WaitForElementNotVisible(CpbookingschedulestaffidLink, 3);
                WaitForElementNotVisible(CpbookingschedulestaffidLookupButton, 3);
            }

            return this;
        }

        public CPFinanceTransactionRecordPage ClickScheduleStaffMemberLink()
        {
            WaitForElementToBeClickable(CpbookingschedulestaffidLink);
            Click(CpbookingschedulestaffidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateScheduleStaffMemberLinkText(string ExpectedText)
        {
            WaitForElement(CpbookingschedulestaffidLink);
            ValidateElementText(CpbookingschedulestaffidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickScheduleStaffMemberLookupButton()
        {
            WaitForElementToBeClickable(CpbookingschedulestaffidLookupButton);
            Click(CpbookingschedulestaffidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateDiaryStaffMemberFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CpbookingdiarystaffidLookupButton);
            }
            else
            {
                WaitForElementNotVisible(CpbookingdiarystaffidLink, 3);
                WaitForElementNotVisible(CpbookingdiarystaffidLookupButton, 3);
            }

            return this;
        }

        public CPFinanceTransactionRecordPage ClickDiaryStaffMemberLink()
        {
            WaitForElementToBeClickable(CpbookingdiarystaffidLink);
            Click(CpbookingdiarystaffidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateDiaryStaffMemberLinkText(string ExpectedText)
        {
            WaitForElement(CpbookingdiarystaffidLink);
            ValidateElementText(CpbookingdiarystaffidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickDiaryStaffMemberLookupButton()
        {
            WaitForElementToBeClickable(CpbookingdiarystaffidLookupButton);
            Click(CpbookingdiarystaffidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickFinanceInvoiceLink()
        {
            WaitForElementToBeClickable(CareproviderfinanceinvoiceidLink);
            Click(CareproviderfinanceinvoiceidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateFinanceInvoiceLinkText(string ExpectedText)
        {
            WaitForElement(CareproviderfinanceinvoiceidLink);
            ValidateElementText(CareproviderfinanceinvoiceidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickFinanceInvoiceLookupButton()
        {
            WaitForElementToBeClickable(CareproviderfinanceinvoiceidLookupButton);
            Click(CareproviderfinanceinvoiceidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage SelectFinanceInvoiceStatus(string TextToSelect)
        {
            WaitForElementToBeClickable(Financeinvoicestatusid);
            SelectPicklistElementByText(Financeinvoicestatusid, TextToSelect);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateFinanceInvoiceStatusSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Financeinvoicestatusid, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickFinanceInvoiceBatchSetupLink()
        {
            WaitForElementToBeClickable(CareproviderfinanceinvoicebatchsetupidLink);
            Click(CareproviderfinanceinvoicebatchsetupidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateFinanceInvoiceBatchSetupLinkText(string ExpectedText)
        {
            WaitForElement(CareproviderfinanceinvoicebatchsetupidLink);
            ValidateElementText(CareproviderfinanceinvoicebatchsetupidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickFinanceInvoiceBatchSetupLookupButton()
        {
            WaitForElementToBeClickable(CareproviderfinanceinvoicebatchsetupidLookupButton);
            Click(CareproviderfinanceinvoicebatchsetupidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateInvoiceNoText(string ExpectedText)
        {
            ValidateElementValue(Invoiceno, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnInvoiceNo(string TextToInsert)
        {
            WaitForElementToBeClickable(Invoiceno);
            SendKeys(Invoiceno, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickFinanceInvoiceBatchLink()
        {
            WaitForElementToBeClickable(CareproviderfinanceinvoicebatchidLink);
            Click(CareproviderfinanceinvoicebatchidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateFinanceInvoiceBatchLinkText(string ExpectedText)
        {
            WaitForElement(CareproviderfinanceinvoicebatchidLink);
            ValidateElementText(CareproviderfinanceinvoicebatchidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickFinanceInvoiceBatchLookupButton()
        {
            WaitForElementToBeClickable(CareproviderfinanceinvoicebatchidLookupButton);
            Click(CareproviderfinanceinvoicebatchidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickFinanceExtractBatchLink()
        {
            WaitForElementToBeClickable(CareproviderfinanceextractbatchidLink);
            Click(CareproviderfinanceextractbatchidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateFinanceExtractBatchLinkText(string ExpectedText)
        {
            WaitForElement(CareproviderfinanceextractbatchidLink);
            ValidateElementText(CareproviderfinanceextractbatchidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickFinanceExtractBatchLookupButton()
        {
            WaitForElementToBeClickable(CareproviderfinanceextractbatchidLookupButton);
            Click(CareproviderfinanceextractbatchidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateExtractNoText(string ExpectedText)
        {
            ValidateElementValue(Extractno, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnExtractNo(string TextToInsert)
        {
            WaitForElementToBeClickable(Extractno);
            SendKeys(Extractno, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickFinanceExtractBatchSetupLink()
        {
            WaitForElementToBeClickable(CareproviderfinanceextractbatchsetupidLink);
            Click(CareproviderfinanceextractbatchsetupidLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateFinanceExtractBatchSetupLinkText(string ExpectedText)
        {
            WaitForElement(CareproviderfinanceextractbatchsetupidLink);
            ValidateElementText(CareproviderfinanceextractbatchsetupidLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickFinanceExtractBatchSetupLookupButton()
        {
            WaitForElementToBeClickable(CareproviderfinanceextractbatchsetupidLookupButton);
            Click(CareproviderfinanceextractbatchsetupidLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElement(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateNoteText_Text(string ExpectedText)
        {
            ValidateElementText(Notetext, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage InsertTextOnNoteText(string TextToInsert)
        {
            WaitForElementToBeClickable(Notetext);
            SendKeys(Notetext, TextToInsert);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateCalculationTraceText(string ExpectedText)
        {
            ValidateElementText(Calculationtrace, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateCalculationTraceContainsText(string TextSubstring)
        {
            ValidateElementTextContainsText(Calculationtrace, TextSubstring);

            return this;
        }

        //method to validate transaction class picklist selected text
        public CPFinanceTransactionRecordPage ValidateTransactionClass(string ExpectedText)
        {
            WaitForElement(TransactionClassPicklist);
            ValidatePicklistSelectedText(TransactionClassPicklist, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateRosteredEmployeeLinkText(string ExpectedText)
        {
            WaitForElement(RosteredEmployeeLink);
            ScrollToElement(RosteredEmployeeLink);
            ValidateElementText(RosteredEmployeeLink, ExpectedText);

            return this;
        }

        public CPFinanceTransactionRecordPage ValidateRosteredEmployeeLookupFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RosteredEmployeeLookupButton);
            }
            else
            {
                WaitForElementNotVisible(RosteredEmployeeLookupButton, 3);
                WaitForElementNotVisible(RosteredEmployeeLookupButton, 3);
            }

            return this;
        }

    }
}
