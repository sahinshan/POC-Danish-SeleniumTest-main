using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Finance
{
    public class CPFinanceInvoiceBatchRecordPage : CommonMethods
    {
        public CPFinanceInvoiceBatchRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame_financeInvoice = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=careproviderfinanceinvoicebatch&')]");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By RunInvoiceBatch = By.XPath("//*[@id='TI_RunInvoiceBatch']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
        readonly By ToolbarDropdownButton = By.XPath("//*[@id = 'CWToolbarMenu']");
        readonly By DropDownMenuArea = By.XPath("//*[@class = 'dropdown-menu right-caret show']");
        readonly By CopyRecordLinkButton = By.XPath("//*[@id = 'TI_CopyRecordLink']");

        readonly By BatchId = By.XPath("//*[@id='CWField_careproviderfinanceinvoicebatchnumber']");
        readonly By Runon = By.XPath("//*[@id='CWField_runon']");
        readonly By RunonDatePicker = By.XPath("//*[@id='CWField_runon_DatePicker']");
        readonly By Runon_Time = By.XPath("//*[@id='CWField_runon_Time']");
        readonly By Runon_Time_TimePicker = By.XPath("//*[@id='CWField_runon_Time_TimePicker']");
        readonly By FinanceInvoiceBatchSetupLink = By.XPath("//*[@id='CWField_careproviderfinanceinvoicebatchsetupid_Link']");
        readonly By FinanceInvoiceBatchSetupLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderfinanceinvoicebatchsetupid']");
        readonly By BatchStatusId = By.XPath("//*[@id='CWField_batchstatusid']");

        readonly By Isadhoc_1 = By.XPath("//*[@id='CWField_isadhocbatch_1']");
        readonly By Isadhoc_0 = By.XPath("//*[@id='CWField_isadhocbatch_0']");

        readonly By Netbatchtotal = By.XPath("//*[@id='CWField_netbatchtotal']");
        readonly By Vattotal = By.XPath("//*[@id='CWField_vattotal']");
        readonly By Grossbatchtotal = By.XPath("//*[@id='CWField_grossbatchtotal']");        
        readonly By Numberofinvoicescreated = By.XPath("//*[@id='CWField_numberofinvoicescreated']");
        readonly By Numberofinvoicescancelled = By.XPath("//*[@id='CWField_numberofinvoicescancelled']");

        readonly By PeriodStartDate = By.XPath("//*[@id='CWField_periodstartdate']");
        readonly By PeriodEndDate = By.XPath("//*[@id='CWField_periodenddate']");        
        readonly By PeriodStartDate_DatePicker = By.XPath("//*[@id='CWField_periodstartdate_DatePicker']");
        readonly By PeriodEndDate_DatePicker = By.XPath("//*[@id='CWField_periodenddate_DatePicker']");

        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By FinanceInvoiceBatchRecordPageHeader = By.XPath("//h1");

        readonly By FinanceInvoicesTab = By.Id("CWNavGroup_FinanceInvoices");
        readonly By FinanceTransactionsTab = By.Id("CWNavGroup_FinanceTransactions");

        readonly By WhenToBatchFinanceTransactions_Picklist = By.Id("CWField_whentobatchfinancetransactionsid");
        readonly By UserFieldLinkText = By.Id("CWField_systemuserid_Link");
        readonly By UserFieldLookupButton = By.Id("CWLookupBtn_systemuserid");
        readonly By PersonFieldLookupButton = By.Id("CWLookupBtn_personid");
        readonly By PersonFieldLinkText = By.Id("CWField_personid_Link");

        readonly By ContractSchemeLookupButton = By.Id("CWLookupBtn_careprovidercontractschemeid");
        readonly By ContractSchemeLink = By.Id("CWField_careprovidercontractschemeid_Link");
        readonly By BatchGroupingLookupButton = By.Id("CWLookupBtn_careproviderbatchgroupingid");
        readonly By BatchGroupingLink = By.Id("CWField_careproviderbatchgroupingid_Link");


        #region Labels
        By MandatoryField_Label(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']/span[@class = 'mandatory']");

        #endregion


        public CPFinanceInvoiceBatchRecordPage WaitForCPFinanceInvoiceBatchRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(cwDialogIFrame_financeInvoice);
            SwitchToIframe(cwDialogIFrame_financeInvoice);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(FinanceInvoiceBatchRecordPageHeader);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(FinanceInvoiceBatchRecordPageHeader);
            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);            

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateFinanceInvoiceBatchRecordTitle(string ExpectedText)
        {
            ScrollToElement(FinanceInvoiceBatchRecordPageHeader);
            WaitForElementVisible(FinanceInvoiceBatchRecordPageHeader);
            ValidateElementByTitle(FinanceInvoiceBatchRecordPageHeader, "Finance Invoice Batch: " + ExpectedText);
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickBackButton()
        {
            ScrollToElement(BackButton);
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            ScrollToElement(SaveButton);
            Click(SaveButton);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickFinanceInvoicesTab()
        {
            WaitForElementToBeClickable(FinanceInvoicesTab);
            ScrollToElement(FinanceInvoicesTab);
            Click(FinanceInvoicesTab);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickFinanceTransactionsTab()
        {
            WaitForElementToBeClickable(FinanceTransactionsTab);
            ScrollToElement(FinanceTransactionsTab);
            Click(FinanceTransactionsTab);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateSaveButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(SaveButton);
            else
                WaitForElementNotVisible(SaveButton, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            ScrollToElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateSaveAndCloseButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(SaveAndCloseButton);
            else
                WaitForElementNotVisible(SaveAndCloseButton, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            ScrollToElement(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateAssignRecordButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(AssignRecordButton);
            else
                WaitForElementNotVisible(AssignRecordButton, 3);

            return this;
        }



        public CPFinanceInvoiceBatchRecordPage ClickRunInvoiceBatchButton()
        {
            WaitForElementToBeClickable(RunInvoiceBatch);
            ScrollToElement(RunInvoiceBatch);
            Click(RunInvoiceBatch);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateRunInvoiceBatchButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(RunInvoiceBatch);
            else
                WaitForElementNotVisible(RunInvoiceBatch, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateCopyRecordLinkButtonIsDisplayed(bool ExpectedDisplayed)
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

        public CPFinanceInvoiceBatchRecordPage ClickCopyRecordLinkButton()
        {
            WaitForElementToBeClickable(ToolbarDropdownButton);
            ScrollToElement(ToolbarDropdownButton);
            Click(ToolbarDropdownButton);

            WaitForElementToBeClickable(CopyRecordLinkButton);
            ScrollToElement(CopyRecordLinkButton);
            Click(CopyRecordLinkButton);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateFinanceInvoicesTabIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(FinanceInvoicesTab);
            else
                WaitForElementNotVisible(FinanceInvoicesTab, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateFinanceTransactionsTabIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(FinanceTransactionsTab);
            else
                WaitForElementNotVisible(FinanceTransactionsTab, 3);

            return this;
        }
        public CPFinanceInvoiceBatchRecordPage ValidateMandatoryFieldIsDisplayed(string FieldName, bool ExpectedMandatory = true)
        {
            if (ExpectedMandatory)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 2);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ScrollToElement(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ScrollToElement(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            ScrollToElement(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateResponsibleTeamLookupButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(ResponsibleTeamLookupButton);
            else
                WaitForElementNotVisible(ResponsibleTeamLookupButton, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(ResponsibleTeamLookupButton);
            ScrollToElement(ResponsibleTeamLookupButton);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(ResponsibleTeamLookupButton);
            }
            else
            {
                ValidateElementEnabled(ResponsibleTeamLookupButton);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateResponsibleTeamLinkFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(ResponsibleTeamLink);
            ScrollToElement(ResponsibleTeamLink);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(ResponsibleTeamLink);
            }
            else
            {
                ValidateElementEnabled(ResponsibleTeamLink);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateRunOnText(string ExpectedText)
        {
            WaitForElementVisible(Runon);
            ScrollToElement(Runon);
            ValidateElementValue(Runon, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage InsertTextOnRunOn(string TextToInsert)
        {
            WaitForElementToBeClickable(Runon);
            ScrollToElement(Runon);
            SendKeys(Runon, TextToInsert + Keys.Tab);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateRunOnFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(Runon);
            else
                WaitForElementNotVisible(Runon, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateRunOnFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(Runon);
            ScrollToElement(Runon);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Runon);
            }
            else
            {
                ValidateElementEnabled(Runon);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickRunOnDatePicker()
        {
            WaitForElementToBeClickable(RunonDatePicker);
            ScrollToElement(RunonDatePicker);
            Click(RunonDatePicker);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateRunOnDatePickerIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(RunonDatePicker);
            else
                WaitForElementNotVisible(RunonDatePicker, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateRunOnDatePickerDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(RunonDatePicker);
            ScrollToElement(RunonDatePicker);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(RunonDatePicker);
            }
            else
            {
                ValidateElementEnabled(RunonDatePicker);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateRunOn_TimeText(string ExpectedText)
        {
            ScrollToElement(Runon_Time);
            WaitForElementVisible(Runon_Time);
            ValidateElementValue(Runon_Time, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage InsertTextOnRunon_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Runon_Time);
            ScrollToElement(Runon_Time);
            SendKeys(Runon_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateRunOn_TimeFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(Runon_Time);
            ScrollToElement(Runon_Time);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Runon_Time);
            }
            else
            {
                ValidateElementEnabled(Runon_Time);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateRunOn_TimeFieldIsDisplayed(bool ExpectedDisplayed)
        {
            WaitForElementVisible(Runon_Time);
            ScrollToElement(Runon_Time);
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(Runon_Time);
            }
            else
            {
                WaitForElementNotVisible(Runon_Time, 3);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickRunOn_TimePicker()
        {
            WaitForElementToBeClickable(Runon_Time_TimePicker);
            ScrollToElement(Runon_Time_TimePicker);
            Click(Runon_Time_TimePicker);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateRunOn_TimePickerIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(Runon_Time_TimePicker);
            else
                WaitForElementNotVisible(Runon_Time_TimePicker, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateRunon_TimePickerFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(Runon_Time_TimePicker);
            ScrollToElement(Runon_Time_TimePicker);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Runon_Time_TimePicker);
            }
            else
            {
                ValidateElementEnabled(Runon_Time_TimePicker);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage SelectWhenToBatchFinanceTransactionsPicklistValue(string TextToSelect)
        {
            WaitForElementToBeClickable(WhenToBatchFinanceTransactions_Picklist);
            ScrollToElement(WhenToBatchFinanceTransactions_Picklist);
            SelectPicklistElementByText(WhenToBatchFinanceTransactions_Picklist, TextToSelect);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateWhenToBatchFinanceTransactionsSelectedText(string ExpectedText)
        {
            WaitForElementVisible(WhenToBatchFinanceTransactions_Picklist);
            ScrollToElement(WhenToBatchFinanceTransactions_Picklist);
            ValidatePicklistSelectedText(WhenToBatchFinanceTransactions_Picklist, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateWhenToBatchFinanceTransactionsPicklistFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(WhenToBatchFinanceTransactions_Picklist);
            else
                WaitForElementNotVisible(WhenToBatchFinanceTransactions_Picklist, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateWhenToBatchFinanceTransactionsPicklistFieldDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(WhenToBatchFinanceTransactions_Picklist);
            WaitForElementVisible(WhenToBatchFinanceTransactions_Picklist);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(WhenToBatchFinanceTransactions_Picklist);
            }
            else
            {
                ValidateElementEnabled(WhenToBatchFinanceTransactions_Picklist);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateBatchIDText(string ExpectedText)
        {
            ScrollToElement(BatchId);
            WaitForElementVisible(BatchId);
            ValidateElementValue(BatchId, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateBatchIDFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(BatchId);
            else
                WaitForElementNotVisible(BatchId, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateBatchIDFieldDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(BatchId);
            WaitForElementVisible(BatchId);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(BatchId);
            }
            else
            {
                ValidateElementEnabled(BatchId);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickFinanceInvoiceBatchSetupLink()
        {
            WaitForElementToBeClickable(FinanceInvoiceBatchSetupLink);
            ScrollToElement(FinanceInvoiceBatchSetupLink);
            Click(FinanceInvoiceBatchSetupLink);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateFinanceInvoiceBatchSetupLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(FinanceInvoiceBatchSetupLink);
            ScrollToElement(FinanceInvoiceBatchSetupLink);
            ValidateElementText(FinanceInvoiceBatchSetupLink, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickFinanceInvoiceBatchSetupLookupButton()
        {
            WaitForElementToBeClickable(FinanceInvoiceBatchSetupLookupButton);
            ScrollToElement(FinanceInvoiceBatchSetupLookupButton);
            Click(FinanceInvoiceBatchSetupLookupButton);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateFinanceInvoiceBatchSetupLookupButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(FinanceInvoiceBatchSetupLookupButton);
            else
                WaitForElementNotVisible(FinanceInvoiceBatchSetupLookupButton, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateFinanceInvoiceBatchSetupLookupButtonDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(FinanceInvoiceBatchSetupLookupButton);
            WaitForElementVisible(FinanceInvoiceBatchSetupLookupButton);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(FinanceInvoiceBatchSetupLookupButton);
            }
            else
            {
                ValidateElementEnabled(FinanceInvoiceBatchSetupLookupButton);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickIsAdHoc_YesButton()
        {
            WaitForElementToBeClickable(Isadhoc_1);
            ScrollToElement(Isadhoc_1);
            Click(Isadhoc_1);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateIsAdHoc_YesChecked()
        {
            WaitForElement(Isadhoc_1);
            ScrollToElement(Isadhoc_1);
            ValidateElementChecked(Isadhoc_1);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateIsAdHoc_YesNotChecked()
        {
            WaitForElement(Isadhoc_1);
            ScrollToElement(Isadhoc_1);
            ValidateElementNotChecked(Isadhoc_1);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickIsAdHoc_NoButton()
        {
            WaitForElementToBeClickable(Isadhoc_0);
            ScrollToElement(Isadhoc_0);
            Click(Isadhoc_0);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateIsAdHoc_NoChecked()
        {
            WaitForElement(Isadhoc_0);
            ScrollToElement(Isadhoc_0);
            ValidateElementChecked(Isadhoc_0);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateIsAdHoc_NoNotChecked()
        {
            WaitForElement(Isadhoc_0);
            ScrollToElement(Isadhoc_0);
            ValidateElementNotChecked(Isadhoc_0);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateIsAdHocOptionsAreDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(Isadhoc_1);
                WaitForElementVisible(Isadhoc_0);
            }
            else
            {
                WaitForElementNotVisible(Isadhoc_1, 3);
                WaitForElementNotVisible(Isadhoc_0, 3);

            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateIsAdHocOptionsDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(Isadhoc_1);
            WaitForElementVisible(Isadhoc_1);
            WaitForElementVisible(Isadhoc_0);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Isadhoc_1);
                ValidateElementDisabled(Isadhoc_0);
            }
            else
            {
                ValidateElementEnabled(Isadhoc_1);
                ValidateElementEnabled(Isadhoc_0);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateNetbatchtotalText(string ExpectedText)
        {
            ScrollToElement(Netbatchtotal);
            ValidateElementValue(Netbatchtotal, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage InsertTextOnNetbatchtotal(string TextToInsert)
        {
            WaitForElementToBeClickable(Netbatchtotal);
            ScrollToElement(Netbatchtotal);
            SendKeys(Netbatchtotal, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateNetBatchTotalFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(Netbatchtotal);
            else
                WaitForElementNotVisible(Netbatchtotal, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateNetBatchTotalDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(Netbatchtotal);
            WaitForElementVisible(Netbatchtotal);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Netbatchtotal);
            }
            else
            {
                ValidateElementEnabled(Netbatchtotal);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateVattotalText(string ExpectedText)
        {
            ScrollToElement(Vattotal);
            ValidateElementValue(Vattotal, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage InsertTextOnVattotal(string TextToInsert)
        {
            WaitForElementToBeClickable(Vattotal);
            ScrollToElement(Vattotal);
            SendKeys(Vattotal, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateVatTotalFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(Vattotal);
            else
                WaitForElementNotVisible(Vattotal, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateVatTotalDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(Vattotal);
            WaitForElementVisible(Vattotal);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Vattotal);
            }
            else
            {
                ValidateElementEnabled(Vattotal);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateGrossbatchtotalText(string ExpectedText)
        {
            ScrollToElement(Grossbatchtotal);
            ValidateElementValue(Grossbatchtotal, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage InsertTextOnGrossbatchtotal(string TextToInsert)
        {
            WaitForElementToBeClickable(Grossbatchtotal);
            ScrollToElement(Grossbatchtotal);
            SendKeys(Grossbatchtotal, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateGrossBatchTotalFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(Grossbatchtotal);
            else
                WaitForElementNotVisible(Grossbatchtotal, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateGrossBatchTotalDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(Grossbatchtotal);
            WaitForElementVisible(Grossbatchtotal);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Grossbatchtotal);
            }
            else
            {
                ValidateElementEnabled(Grossbatchtotal);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateNumberofInvoicesCreatedText(string ExpectedText)
        {
            ScrollToElement(Numberofinvoicescreated);
            ValidateElementValue(Numberofinvoicescreated, ExpectedText);

            return this;
        }


        public CPFinanceInvoiceBatchRecordPage ValidateNumberOfInvoicesCreatedFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(Numberofinvoicescreated);
            else
                WaitForElementNotVisible(Numberofinvoicescreated, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateNumberOfInvoicesCreatedDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(Numberofinvoicescreated);
            WaitForElementVisible(Numberofinvoicescreated);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Numberofinvoicescreated);
            }
            else
            {
                ValidateElementEnabled(Numberofinvoicescreated);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateNumberOfInvoicesCancelledText(string ExpectedText)
        {
            ScrollToElement(Numberofinvoicescancelled);
            ValidateElementValue(Numberofinvoicescancelled, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateNumberOfInvoicesCancelledFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(Numberofinvoicescancelled);
            else
                WaitForElementNotVisible(Numberofinvoicescancelled, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateNumberOfInvoicesCancelledDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(Numberofinvoicescancelled);
            WaitForElementVisible(Numberofinvoicescancelled);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Numberofinvoicescancelled);
            }
            else
            {
                ValidateElementEnabled(Numberofinvoicescancelled);
            }
            return this;
        }
        public CPFinanceInvoiceBatchRecordPage ValidatePeriodStartDateText(string ExpectedText)
        {
            ScrollToElement(PeriodStartDate);
            ValidateElementValue(PeriodStartDate, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage InsertTextOnPeriodStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(PeriodStartDate);
            ScrollToElement(PeriodStartDate);
            SendKeys(PeriodStartDate, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidatePeriodStartDateFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(PeriodStartDate);
            else
                WaitForElementNotVisible(PeriodStartDate, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidatePeriodStartDateFieldDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(PeriodStartDate);
            WaitForElementVisible(PeriodStartDate);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(PeriodStartDate);
            }
            else
            {
                ValidateElementEnabled(PeriodStartDate);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickPeriodStartDate_DatePicker()
        {
            WaitForElementToBeClickable(PeriodStartDate_DatePicker);
            ScrollToElement(PeriodStartDate_DatePicker);
            Click(PeriodStartDate_DatePicker);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidatePeriodStartDate_DatePickerIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(PeriodStartDate_DatePicker);
            else
                WaitForElementNotVisible(PeriodStartDate_DatePicker, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidatePeriodStartDate_PickerDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(PeriodStartDate_DatePicker);
            WaitForElementVisible(PeriodStartDate_DatePicker);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(PeriodStartDate_DatePicker);
            }
            else
            {
                ValidateElementEnabled(PeriodStartDate_DatePicker);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidatePeriodEndDateText(string ExpectedText)
        {
            ScrollToElement(PeriodEndDate);
            ValidateElementValue(PeriodEndDate, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage InsertTextOnPeriodEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(PeriodEndDate);
            ScrollToElement(PeriodEndDate);
            SendKeys(PeriodEndDate, TextToInsert);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidatePeriodEndDateFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(PeriodEndDate);
            else
                WaitForElementNotVisible(PeriodEndDate, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidatePeriodEndDateFieldDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(PeriodEndDate);
            WaitForElementVisible(PeriodEndDate);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(PeriodEndDate);
            }
            else
            {
                ValidateElementEnabled(PeriodEndDate);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickPeriodEndDate_DatePicker()
        {
            WaitForElementToBeClickable(PeriodEndDate_DatePicker);
            ScrollToElement(PeriodEndDate_DatePicker);
            Click(PeriodEndDate_DatePicker);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidatePeriodEndDate_DatePickerIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(PeriodEndDate_DatePicker);
            else
                WaitForElementNotVisible(PeriodEndDate_DatePicker, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidatePeriodEndDate_DatePickerDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(PeriodEndDate_DatePicker);
            WaitForElementVisible(PeriodEndDate_DatePicker);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(PeriodEndDate_DatePicker);
            }
            else
            {
                ValidateElementEnabled(PeriodEndDate_DatePicker);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateBatchStatusText(string ExpectedText)
        {
            WaitForElementVisible(BatchStatusId);
            ScrollToElement(BatchStatusId);
            ValidatePicklistSelectedText(BatchStatusId, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateBatchStatusFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(BatchStatusId);
            else
                WaitForElementNotVisible(BatchStatusId, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateBatchStatusFieldDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(BatchStatusId);
            WaitForElementVisible(BatchStatusId);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(BatchStatusId);
            }
            else
            {
                ValidateElementEnabled(BatchStatusId);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickUserFieldLinkText()
        {
            WaitForElementToBeClickable(UserFieldLinkText);
            ScrollToElement(UserFieldLinkText);
            Click(UserFieldLinkText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateUserFieldLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(UserFieldLinkText);
            ScrollToElement(UserFieldLinkText);
            ValidateElementText(UserFieldLinkText, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickUserFieldLookupButton()
        {
            WaitForElementToBeClickable(UserFieldLookupButton);
            ScrollToElement(UserFieldLookupButton);
            Click(UserFieldLookupButton);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateUserFieldLookupButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(UserFieldLookupButton);
            else
                WaitForElementNotVisible(UserFieldLookupButton, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateUserFieldLookupButtonDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(UserFieldLookupButton);
            WaitForElementVisible(UserFieldLookupButton);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(UserFieldLookupButton);
            }
            else
            {
                ValidateElementEnabled(UserFieldLookupButton);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickContractSchemeLookupButton()
        {
            WaitForElementToBeClickable(ContractSchemeLookupButton);
            ScrollToElement(ContractSchemeLookupButton);
            Click(ContractSchemeLookupButton);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateContractSchemeLookupButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(ContractSchemeLookupButton);
            else
                WaitForElementNotVisible(ContractSchemeLookupButton, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateContractSchemeLookupButtonDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(ContractSchemeLookupButton);
            WaitForElementVisible(ContractSchemeLookupButton);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(ContractSchemeLookupButton);
            }
            else
            {
                ValidateElementEnabled(ContractSchemeLookupButton);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickContractSchemeLink()
        {
            WaitForElementToBeClickable(ContractSchemeLink);
            ScrollToElement(ContractSchemeLink);
            Click(ContractSchemeLink);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateContractSchemeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ContractSchemeLink);
            ScrollToElement(ContractSchemeLink);
            ValidateElementText(ContractSchemeLink, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickBatchGroupingLookupButton()
        {
            WaitForElementToBeClickable(BatchGroupingLookupButton);
            ScrollToElement(BatchGroupingLookupButton);
            Click(BatchGroupingLookupButton);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateBatchGroupingLookupButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(BatchGroupingLookupButton);
            else
                WaitForElementNotVisible(BatchGroupingLookupButton, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateBatchGroupingLookupButtonDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(BatchGroupingLookupButton);
            WaitForElementVisible(BatchGroupingLookupButton);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(BatchGroupingLookupButton);
            }
            else
            {
                ValidateElementEnabled(BatchGroupingLookupButton);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickBatchGroupingLink()
        {
            WaitForElementToBeClickable(BatchGroupingLink);
            ScrollToElement(BatchGroupingLink);
            Click(BatchGroupingLink);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidateBatchGroupingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(BatchGroupingLink);
            ScrollToElement(BatchGroupingLink);
            ValidateElementText(BatchGroupingLink, ExpectedText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickPersonFieldLookupButton()
        {
            WaitForElementToBeClickable(PersonFieldLookupButton);
            ScrollToElement(PersonFieldLookupButton);
            Click(PersonFieldLookupButton);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidatePersonFieldLookupButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(PersonFieldLookupButton);
            else
                WaitForElementNotVisible(PersonFieldLookupButton, 3);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidatePersonFieldLookupButtonDisabled(bool ExpectedDisabled)
        {
            ScrollToElement(PersonFieldLookupButton);
            WaitForElementVisible(PersonFieldLookupButton);
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(PersonFieldLookupButton);
            }
            else
            {
                ValidateElementEnabled(PersonFieldLookupButton);
            }
            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ClickPersonFieldLinkText()
        {
            WaitForElementToBeClickable(PersonFieldLinkText);
            ScrollToElement(PersonFieldLinkText);
            Click(PersonFieldLinkText);

            return this;
        }

        public CPFinanceInvoiceBatchRecordPage ValidatePersonFieldLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonFieldLinkText);
            ScrollToElement(PersonFieldLinkText);
            ValidateElementText(PersonFieldLinkText, ExpectedText);

            return this;
        }

        //Method to Click Delete Record Button
        public CPFinanceInvoiceBatchRecordPage ClickDeleteRecordButton()
        {
            if (GetElementVisibility(DeleteRecordButton).Equals(false))
            {
                if (GetElementVisibility(ToolbarDropdownButton).Equals(true) && !GetElementByAttributeValue(ToolbarDropdownButton, "class").Contains("show"))
                {
                    WaitForElementToBeClickable(ToolbarDropdownButton);
                    Click(ToolbarDropdownButton);
                }
            }
            
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);



            return this;
        }

        //validate DeleteRecordButton is displayed outside and also inside ToolbarDropdownButton
        public CPFinanceInvoiceBatchRecordPage ValidateDeleteRecordButtonIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                if (GetElementVisibility(DeleteRecordButton).Equals(false))
                {
                    if (GetElementVisibility(ToolbarDropdownButton).Equals(true) && !GetElementByAttributeValue(ToolbarDropdownButton, "class").Contains("show"))
                    {
                        WaitForElementToBeClickable(ToolbarDropdownButton);
                        Click(ToolbarDropdownButton);
                    }
                }
                WaitForElementVisible(DeleteRecordButton);
            }
            else
            {
                WaitForElementNotVisible(DeleteRecordButton, 3);
                Click(ToolbarDropdownButton);
                WaitForElementNotVisible(DeleteRecordButton, 3);
            }

            return this;
        }

    }
}
