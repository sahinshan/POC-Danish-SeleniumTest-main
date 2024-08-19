using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Finance
{
	public class CPFinanceExtractBatchRecordPage : CommonMethods
	{
		public CPFinanceExtractBatchRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		readonly By cwDialogIFrame_financeExtract = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=careproviderfinanceextractbatch&')]");
		readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By RunExtractBatch = By.XPath("//*[@id='TI_RunExtractBatch']");
		readonly By ToolbarDropdownButton = By.XPath("//*[@id = 'CWToolbarMenu']");
		readonly By DropDownMenuArea = By.XPath("//*[@class = 'dropdown-menu right-caret show']");
		readonly By DeleteRecordButton = By.XPath("//*[@id = 'TI_DeleteRecordButton']");
		readonly By CopyRecordLinkButton = By.XPath("//*[@id = 'TI_CopyRecordLink']");

		readonly By BatchId = By.XPath("//*[@id='CWField_batchid']");
		readonly By Runon = By.XPath("//*[@id='CWField_runon']");
		readonly By RunonDatePicker = By.XPath("//*[@id='CWField_runon_DatePicker']");
		readonly By Runon_Time = By.XPath("//*[@id='CWField_runon_Time']");
		readonly By Runon_Time_TimePicker = By.XPath("//*[@id='CWField_runon_Time_TimePicker']");
		readonly By ExtractnameLink = By.XPath("//*[@id='CWField_extractnameid_Link']");
		readonly By ExtractnameLookupButton = By.XPath("//*[@id='CWLookupBtn_extractnameid']");
		readonly By InvoiceFiles_FieldLabel = By.XPath("//*[@id='CWLabelHolder_invoicefiles']");
		readonly By Invoicefiles_FileLink = By.XPath("//*[@id='CWField_invoicefiles_FileLink']");
		readonly By FinanceExtractBatchSetupLink = By.XPath("//*[@id='CWField_careproviderfinanceextractbatchsetupid_Link']");
		readonly By FinanceExtractBatchSetupLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderfinanceextractbatchsetupid']");
		readonly By BatchStatusId = By.XPath("//*[@id='CWField_batchstatusid']");

		readonly By Isadhoc_1 = By.XPath("//*[@id='CWField_isadhocbatch_1']");
		readonly By Isadhoc_0 = By.XPath("//*[@id='CWField_isadhocbatch_0']");

		readonly By Netbatchtotal = By.XPath("//*[@id='CWField_netbatchtotal']");
		readonly By Vattotal = By.XPath("//*[@id='CWField_vattotal']");
		readonly By Totalcredits = By.XPath("//*[@id='CWField_totalcredits']");
		readonly By Numberofuniquepayers = By.XPath("//*[@id='CWField_numberofuniquepayers']");
		readonly By Extractyear = By.XPath("//*[@id='CWField_extractyear']");
		readonly By Extractweek = By.XPath("//*[@id='CWField_extractweek']");
		readonly By Isdownloaded_1 = By.XPath("//*[@id='CWField_isdownloaded_1']");
		readonly By Isdownloaded_0 = By.XPath("//*[@id='CWField_isdownloaded_0']");
		readonly By Grossbatchtotal = By.XPath("//*[@id='CWField_grossbatchtotal']");
		readonly By Totaldebits = By.XPath("//*[@id='CWField_totaldebits']");
		readonly By Numberofinvoicesextracted = By.XPath("//*[@id='CWField_numberofinvoicesextracted']");
		readonly By Numberofinvoicescancelled = By.XPath("//*[@id='CWField_numberofinvoicescancelled']");
		readonly By Extractmonth = By.XPath("//*[@id='CWField_extractmonth']");
		readonly By Extractcontent_FieldLabel = By.XPath("//*[@id='CWLabelHolder_extractcontent']");
		readonly By Extractcontent_FileLink = By.XPath("//*[@id='CWField_extractcontent_FileLink']");
		readonly By Completedon = By.XPath("//*[@id='CWField_completedon']");
		readonly By CompletedonDatePicker = By.XPath("//*[@id='CWField_completedon_DatePicker']");
		readonly By Completedon_Time = By.XPath("//*[@id='CWField_completedon_Time']");
		readonly By Completedon_Time_TimePicker = By.XPath("//*[@id='CWField_completedon_Time_TimePicker']");


		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By FinanceExtractBatchRecordPageHeader = By.XPath("//h1");

		readonly By FinanceInvoiceTab = By.Id("CWNavGroup_FinanceInvoice");
        readonly By FinanceTransactionTab = By.Id("CWNavGroup_FinanceTransaction");

        #region Labels
        By MandatoryField_Label(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']/span[@class = 'mandatory']");

		#endregion


		public CPFinanceExtractBatchRecordPage WaitForCPFinanceExtractBatchRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElement(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElement(cwDialogIFrame_financeExtract);
			SwitchToIframe(cwDialogIFrame_financeExtract);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(FinanceExtractBatchRecordPageHeader);

			return this;
		}

		public CPFinanceExtractBatchRecordPage WaitForRecordToBeSaved()
		{
			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElement(FinanceExtractBatchRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateFinanceExtractBatchRecordTitle(string ExpectedText)
		{
			ScrollToElement(FinanceExtractBatchRecordPageHeader);
			WaitForElementVisible(FinanceExtractBatchRecordPageHeader);
			ValidateElementByTitle(FinanceExtractBatchRecordPageHeader, "Finance Extract Batch: " + ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);			
			Click(BackButton);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			ScrollToElement(SaveButton);
			Click(SaveButton);

			return this;
		}

        public CPFinanceExtractBatchRecordPage ClickFinanceInvoiceTab()
        {
            WaitForElementToBeClickable(FinanceInvoiceTab);
            ScrollToElement(FinanceInvoiceTab);
            Click(FinanceInvoiceTab);

            return this;
        }

        public CPFinanceExtractBatchRecordPage ClickFinanceTransactionTab()
        {
            WaitForElementToBeClickable(FinanceTransactionTab);
            ScrollToElement(FinanceTransactionTab);
            Click(FinanceTransactionTab);

            return this;
        }

        public CPFinanceExtractBatchRecordPage ValidateSaveButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(SaveButton);
			else
				WaitForElementNotVisible(SaveButton, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			ScrollToElement(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateSaveAndCloseButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(SaveAndCloseButton);
			else
				WaitForElementNotVisible(SaveAndCloseButton, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			ScrollToElement(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateAssignRecordButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(AssignRecordButton);
			else
				WaitForElementNotVisible(AssignRecordButton, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickRunExtractBatchButton()
		{
			WaitForElementToBeClickable(RunExtractBatch);
			ScrollToElement(RunExtractBatch);
			Click(RunExtractBatch);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateRunExtractBatchButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(RunExtractBatch);
			else
				WaitForElementNotVisible(RunExtractBatch, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickDeleteButton()
		{
			WaitForElementToBeClickable(ToolbarDropdownButton);
			ScrollToElement(ToolbarDropdownButton);
			Click(ToolbarDropdownButton);

			WaitForElementToBeClickable(DeleteRecordButton);
			ScrollToElement(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}


		public CPFinanceExtractBatchRecordPage ValidateDeleteButtonIsDisplayed(bool ExpectedDisplayed)
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
				WaitForElementVisible(DeleteRecordButton);
            }
            else
            {
				WaitForElementNotVisible(DeleteRecordButton, 3);
			}

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateCopyRecordLinkButtonIsDisplayed(bool ExpectedDisplayed)
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

		public CPFinanceExtractBatchRecordPage ClickCopyRecordLinkButton()
		{
			WaitForElementToBeClickable(ToolbarDropdownButton);
			ScrollToElement(ToolbarDropdownButton);
			Click(ToolbarDropdownButton);

			WaitForElementToBeClickable(CopyRecordLinkButton);
			ScrollToElement(CopyRecordLinkButton);
			Click(CopyRecordLinkButton);

			return this;
		}

        public CPFinanceExtractBatchRecordPage ValidateFinanceInvoiceTabIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(FinanceInvoiceTab);
            else
                WaitForElementNotVisible(FinanceInvoiceTab, 3);

            return this;
        }

        public CPFinanceExtractBatchRecordPage ValidateFinanceTransactionTabIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(FinanceTransactionTab);
            else
                WaitForElementNotVisible(FinanceTransactionTab, 3);

            return this;
        }
        public CPFinanceExtractBatchRecordPage ValidateMandatoryFieldIsDisplayed(string FieldName, bool ExpectedMandatory = true)
		{
			if (ExpectedMandatory)
				WaitForElementVisible(MandatoryField_Label(FieldName));
			else
				WaitForElementNotVisible(MandatoryField_Label(FieldName), 2);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			ScrollToElement(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateResponsibleTeamLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ResponsibleTeamLookupButton);
			else
				WaitForElementNotVisible(ResponsibleTeamLookupButton, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool ExpectedDisabled)
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

		public CPFinanceExtractBatchRecordPage ValidateResponsibleTeamLinkFieldDisabled(bool ExpectedDisabled)
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

		public CPFinanceExtractBatchRecordPage ValidateRunOnText(string ExpectedText)
		{
			ScrollToElement(Runon);
			ScrollToElement(Runon);
			ValidateElementValue(Runon, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnRunOn(string TextToInsert)
		{
			WaitForElementToBeClickable(Runon);
            ScrollToElement(Runon);
			SendKeys(Runon, TextToInsert + Keys.Tab);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateRunOnFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Runon);
			else
				WaitForElementNotVisible(Runon, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateRunOnFieldDisabled(bool ExpectedDisabled)
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

		public CPFinanceExtractBatchRecordPage ClickRunOnDatePicker()
		{
			WaitForElementToBeClickable(RunonDatePicker);
			ScrollToElement(RunonDatePicker);
			Click(RunonDatePicker);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateRunOnDatePickerIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(RunonDatePicker);
			else
				WaitForElementNotVisible(RunonDatePicker, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateRunOnDatePickerDisabled(bool ExpectedDisabled)
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

		public CPFinanceExtractBatchRecordPage ValidateRunOn_TimeText(string ExpectedText)
		{
			ScrollToElement(Runon_Time);
			WaitForElementVisible(Runon_Time);
			ValidateElementValue(Runon_Time, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnRunon_Time(string TextToInsert)
		{
			WaitForElementToBeClickable(Runon_Time);
			ScrollToElement(Runon_Time);			
			SendKeys(Runon_Time, TextToInsert);			

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateRunOn_TimeFieldDisabled(bool ExpectedDisabled)
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

		public CPFinanceExtractBatchRecordPage ClickRunOn_TimePicker()
		{
			WaitForElementToBeClickable(Runon_Time_TimePicker);
            ScrollToElement(Runon_Time_TimePicker);
			Click(Runon_Time_TimePicker);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateRunOn_TimePickerIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Runon_Time_TimePicker);
			else
				WaitForElementNotVisible(Runon_Time_TimePicker, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateRunon_TimePickerFieldDisabled(bool ExpectedDisabled)
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

		public CPFinanceExtractBatchRecordPage ClickExtractNameLink()
		{
			WaitForElementToBeClickable(ExtractnameLink);
			ScrollToElement(ExtractnameLink);
			Click(ExtractnameLink);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractNameLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ExtractnameLink);
			ScrollToElement(ExtractnameLink);
			ValidateElementText(ExtractnameLink, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickExtractNameLookupButton()
		{
			WaitForElementToBeClickable(ExtractnameLookupButton);
			ScrollToElement(ExtractnameLookupButton);
			Click(ExtractnameLookupButton);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractNameLookupButtonDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(ExtractnameLookupButton);
			ScrollToElement(ExtractnameLookupButton);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(ExtractnameLookupButton);
			}
			else
			{
				ValidateElementEnabled(ExtractnameLookupButton);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractNameLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractnameLookupButton);
			else
				WaitForElementNotVisible(ExtractnameLookupButton, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractNameLinkFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(ExtractnameLink);
			ScrollToElement(ExtractnameLink);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(ExtractnameLink);
			}
			else
			{
				ValidateElementEnabled(ExtractnameLink);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickInvoiceFiles_FileLink()
		{
			WaitForElementToBeClickable(Invoicefiles_FileLink);
			ScrollToElement(Invoicefiles_FileLink);
			Click(Invoicefiles_FileLink);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateInvoiceFiles_FileLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(Invoicefiles_FileLink);
			ScrollToElement(Invoicefiles_FileLink);
			ValidateElementByTitle(Invoicefiles_FileLink, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateInvoiceFilesFieldLabelIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(InvoiceFiles_FieldLabel);
			else
				WaitForElementNotVisible(InvoiceFiles_FieldLabel, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateInvoiceFilesFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Invoicefiles_FileLink);
			else
				WaitForElementNotVisible(Invoicefiles_FileLink, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateInvoicefiles_FileLinkFieldDisabled(bool ExpectedDisabled)
		{
			WaitForElementVisible(Invoicefiles_FileLink);
			ScrollToElement(Invoicefiles_FileLink);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Invoicefiles_FileLink);
			}
			else
			{
				ValidateElementEnabled(Invoicefiles_FileLink);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage SelectBatchStatus(string TextToSelect)
		{
			WaitForElementToBeClickable(BatchStatusId);
			ScrollToElement(BatchStatusId);
			SelectPicklistElementByText(BatchStatusId, TextToSelect);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateBatchStatusSelectedText(string ExpectedText)
		{
			WaitForElementVisible(BatchStatusId);
            ScrollToElement(BatchStatusId);
			ValidatePicklistSelectedText(BatchStatusId, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateBatchStatusFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(BatchStatusId);
			else
				WaitForElementNotVisible(BatchStatusId, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateBatchStatusFieldDisabled(bool ExpectedDisabled)
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

		public CPFinanceExtractBatchRecordPage ValidateBatchIDText(string ExpectedText)
		{
			ScrollToElement(BatchId);
			WaitForElementVisible(BatchId);
			ValidateElementValue(BatchId, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateBatchIDFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(BatchId);
			else
				WaitForElementNotVisible(BatchId, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateBatchIDFieldDisabled(bool ExpectedDisabled)
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

		public CPFinanceExtractBatchRecordPage ClickFinanceExtractBatchSetupLink()
		{
			WaitForElementToBeClickable(FinanceExtractBatchSetupLink);
            ScrollToElement(FinanceExtractBatchSetupLink);
			Click(FinanceExtractBatchSetupLink);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateFinanceExtractBatchSetupLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(FinanceExtractBatchSetupLink);
            ScrollToElement(FinanceExtractBatchSetupLink);
			ValidateElementText(FinanceExtractBatchSetupLink, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickFinanceExtractBatchSetupLookupButton()
		{
			WaitForElementToBeClickable(FinanceExtractBatchSetupLookupButton);
            ScrollToElement(FinanceExtractBatchSetupLookupButton);
			Click(FinanceExtractBatchSetupLookupButton);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateFinanceExtractBatchSetupLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(FinanceExtractBatchSetupLookupButton);
			else
				WaitForElementNotVisible(FinanceExtractBatchSetupLookupButton, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateFinanceExtractBatchSetupLookupButtonDisabled(bool ExpectedDisabled)
		{
			ScrollToElement(FinanceExtractBatchSetupLookupButton);
			WaitForElementVisible(FinanceExtractBatchSetupLookupButton);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(FinanceExtractBatchSetupLookupButton);
			}
			else
			{
				ValidateElementEnabled(FinanceExtractBatchSetupLookupButton);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickIsAdHoc_YesButton()
		{
			WaitForElementToBeClickable(Isadhoc_1);
            ScrollToElement(Isadhoc_1);
			Click(Isadhoc_1);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateIsAdHoc_YesChecked()
		{
			WaitForElement(Isadhoc_1);
            ScrollToElement(Isadhoc_1);
			ValidateElementChecked(Isadhoc_1);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateIsAdHoc_YesNotChecked()
		{
			WaitForElement(Isadhoc_1);
			ScrollToElement(Isadhoc_1);
			ValidateElementNotChecked(Isadhoc_1);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickIsAdHoc_NoButton()
		{
			WaitForElementToBeClickable(Isadhoc_0);
			ScrollToElement(Isadhoc_0);
			Click(Isadhoc_0);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateIsAdHoc_NoChecked()
		{
			WaitForElement(Isadhoc_0);
			ScrollToElement(Isadhoc_0);
			ValidateElementChecked(Isadhoc_0);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateIsAdHoc_NoNotChecked()
		{
			WaitForElement(Isadhoc_0);
            ScrollToElement(Isadhoc_0);
			ValidateElementNotChecked(Isadhoc_0);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateIsAdHocOptionsAreDisplayed(bool ExpectedDisplayed)
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

		public CPFinanceExtractBatchRecordPage ValidateIsAdHocOptionsDisabled(bool ExpectedDisabled)
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

		public CPFinanceExtractBatchRecordPage ValidateNetbatchtotalText(string ExpectedText)
		{
			ScrollToElement(Netbatchtotal);
			ValidateElementValue(Netbatchtotal, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnNetbatchtotal(string TextToInsert)
		{
			WaitForElementToBeClickable(Netbatchtotal);
            ScrollToElement(Netbatchtotal);
			SendKeys(Netbatchtotal, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateNetBatchTotalFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Netbatchtotal);
			else
				WaitForElementNotVisible(Netbatchtotal, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateNetBatchTotalDisabled(bool ExpectedDisabled)
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

		public CPFinanceExtractBatchRecordPage ValidateVattotalText(string ExpectedText)
		{
            ScrollToElement(Vattotal);
			ValidateElementValue(Vattotal, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnVattotal(string TextToInsert)
		{
			WaitForElementToBeClickable(Vattotal);
            ScrollToElement(Vattotal);
			SendKeys(Vattotal, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateVatTotalFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Vattotal);
			else
				WaitForElementNotVisible(Vattotal, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateVatTotalDisabled(bool ExpectedDisabled)
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

		public CPFinanceExtractBatchRecordPage ValidateTotalcreditsText(string ExpectedText)
		{
            ScrollToElement(Totalcredits);
			ValidateElementValue(Totalcredits, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnTotalcredits(string TextToInsert)
		{
			WaitForElementToBeClickable(Totalcredits);
            ScrollToElement(Totalcredits);
			SendKeys(Totalcredits, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateTotalCreditsFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Totalcredits);
			else
				WaitForElementNotVisible(Totalcredits, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateTotalCreditsDisabled(bool ExpectedDisabled)
		{
            ScrollToElement(Totalcredits);
			WaitForElementVisible(Totalcredits);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Totalcredits);
			}
			else
			{
				ValidateElementEnabled(Totalcredits);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateNumberofuniquepayersText(string ExpectedText)
		{
            ScrollToElement(Numberofuniquepayers);
			ValidateElementValue(Numberofuniquepayers, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnNumberofuniquepayers(string TextToInsert)
		{
			WaitForElementToBeClickable(Numberofuniquepayers);
            ScrollToElement(Numberofuniquepayers);
			SendKeys(Numberofuniquepayers, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateNumberOfUniquePayersFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Numberofuniquepayers);
			else
				WaitForElementNotVisible(Numberofuniquepayers, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateNumberOfUniquePayersDisabled(bool ExpectedDisabled)
		{
            ScrollToElement(Numberofuniquepayers);
			WaitForElementVisible(Numberofuniquepayers);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Numberofuniquepayers);
			}
			else
			{
				ValidateElementEnabled(Numberofuniquepayers);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractyearText(string ExpectedText)
		{
            ScrollToElement(Extractyear);
			ValidateElementValue(Extractyear, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnExtractyear(string TextToInsert)
		{
			WaitForElementToBeClickable(Extractyear);
            ScrollToElement(Extractyear);
			SendKeys(Extractyear, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractYearFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Extractyear);
			else
				WaitForElementNotVisible(Extractyear, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractYearDisabled(bool ExpectedDisabled)
		{
            ScrollToElement(Extractyear);
			WaitForElementVisible(Extractyear);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Extractyear);
			}
			else
			{
				ValidateElementEnabled(Extractyear);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractweekText(string ExpectedText)
		{
            ScrollToElement(Extractweek);
			ValidateElementValue(Extractweek, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnExtractweek(string TextToInsert)
		{
			WaitForElementToBeClickable(Extractweek);
			ScrollToElement(Extractweek);
			SendKeys(Extractweek, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractWeekFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Extractweek);
			else
				WaitForElementNotVisible(Extractweek, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractWeekDisabled(bool ExpectedDisabled)
		{
            ScrollToElement(Extractweek);
			WaitForElementVisible(Extractweek);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Extractweek);
			}
			else
			{
				ValidateElementEnabled(Extractweek);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickIsdownloaded_YesOption()
		{
			WaitForElementToBeClickable(Isdownloaded_1);
            ScrollToElement(Isdownloaded_1);
			Click(Isdownloaded_1);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateIsdownloaded_YesOptionChecked()
		{
			WaitForElement(Isdownloaded_1);
            ScrollToElement(Isdownloaded_1);
			ValidateElementChecked(Isdownloaded_1);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateIsdownloaded_YesOptionNotChecked()
		{
			WaitForElement(Isdownloaded_1);
            ScrollToElement(Isdownloaded_1);
			ValidateElementNotChecked(Isdownloaded_1);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickIsdownloaded_NoOption()
		{
			WaitForElementToBeClickable(Isdownloaded_0);
            ScrollToElement(Isdownloaded_0);
			Click(Isdownloaded_0);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateIsdownloaded_NoOptionChecked()
		{
			WaitForElement(Isdownloaded_0);
            ScrollToElement(Isdownloaded_0);
			ValidateElementChecked(Isdownloaded_0);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateIsdownloaded_NoOptionNotChecked()
		{
			WaitForElement(Isdownloaded_0);
            ScrollToElement(Isdownloaded_0);
			ValidateElementNotChecked(Isdownloaded_0);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateIsDownloadedOptionsAreDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(Isdownloaded_1);
				WaitForElementVisible(Isdownloaded_0);
			}
			else
			{
				WaitForElementNotVisible(Isdownloaded_1, 3);
				WaitForElementNotVisible(Isdownloaded_0, 3);

			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateIsDownloadedOptionsDisabled(bool ExpectedDisabled)
		{
            ScrollToElement(Isdownloaded_0);
			WaitForElementVisible(Isdownloaded_0);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Isdownloaded_0);
				ValidateElementDisabled(Isdownloaded_1);
			}
			else
			{
				ValidateElementEnabled(Isdownloaded_0);
				ValidateElementEnabled(Isdownloaded_1);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateGrossbatchtotalText(string ExpectedText)
		{
            ScrollToElement(Grossbatchtotal);
			ValidateElementValue(Grossbatchtotal, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnGrossbatchtotal(string TextToInsert)
		{
			WaitForElementToBeClickable(Grossbatchtotal);
            ScrollToElement(Grossbatchtotal);
			SendKeys(Grossbatchtotal, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateGrossBatchTotalFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Grossbatchtotal);
			else
				WaitForElementNotVisible(Grossbatchtotal, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateGrossBatchTotalDisabled(bool ExpectedDisabled)
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

		public CPFinanceExtractBatchRecordPage ValidateTotaldebitsText(string ExpectedText)
		{
            ScrollToElement(Totaldebits);
			ValidateElementValue(Totaldebits, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnTotaldebits(string TextToInsert)
		{
			WaitForElementToBeClickable(Totaldebits);
            ScrollToElement(Totaldebits);
			SendKeys(Totaldebits, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateTotalDebitsFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Totaldebits);
			else
				WaitForElementNotVisible(Totaldebits, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateTotalDebitsDisabled(bool ExpectedDisabled)
		{
			ScrollToElement(Totaldebits);
			WaitForElementVisible(Totaldebits);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Totaldebits);
			}
			else
			{
				ValidateElementEnabled(Totaldebits);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateNumberofinvoicesextractedText(string ExpectedText)
		{
			ScrollToElement(Numberofinvoicesextracted);
			ValidateElementValue(Numberofinvoicesextracted, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnNumberofinvoicesextracted(string TextToInsert)
		{
			WaitForElementToBeClickable(Numberofinvoicesextracted);
			ScrollToElement(Numberofinvoicesextracted);
			SendKeys(Numberofinvoicesextracted, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateNumberOfInvoicesExtractedFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Numberofinvoicesextracted);
			else
				WaitForElementNotVisible(Numberofinvoicesextracted, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateNumberOfInvoicesExtractedDisabled(bool ExpectedDisabled)
		{
			ScrollToElement(Numberofinvoicesextracted);
			WaitForElementVisible(Numberofinvoicesextracted);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Numberofinvoicesextracted);
			}
			else
			{
				ValidateElementEnabled(Numberofinvoicesextracted);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateNumberofinvoicescancelledText(string ExpectedText)
		{
			ScrollToElement(Numberofinvoicescancelled);
			ValidateElementValue(Numberofinvoicescancelled, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnNumberofinvoicescancelled(string TextToInsert)
		{
			WaitForElementToBeClickable(Numberofinvoicescancelled);
			ScrollToElement(Numberofinvoicescancelled);
			SendKeys(Numberofinvoicescancelled, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateNumberOfInvoicesCancelledFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Numberofinvoicescancelled);
			else
				WaitForElementNotVisible(Numberofinvoicescancelled, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateNumberOfInvoicesCancelledDisabled(bool ExpectedDisabled)
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
		public CPFinanceExtractBatchRecordPage ValidateExtractmonthText(string ExpectedText)
		{
			ScrollToElement(Extractmonth);
			ValidateElementValue(Extractmonth, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnExtractmonth(string TextToInsert)
		{
			WaitForElementToBeClickable(Extractmonth);
			ScrollToElement(Extractmonth);
			SendKeys(Extractmonth, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractMonthFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Extractmonth);
			else
				WaitForElementNotVisible(Extractmonth, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractMonthDisabled(bool ExpectedDisabled)
		{
			ScrollToElement(Extractmonth);
			WaitForElementVisible(Extractmonth);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Extractmonth);
			}
			else
			{
				ValidateElementEnabled(Extractmonth);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickExtractcontent_FileLink()
		{
			WaitForElementToBeClickable(Extractcontent_FileLink);
			ScrollToElement(Extractcontent_FileLink);
			Click(Extractcontent_FileLink);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractcontent_FileLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(Extractcontent_FileLink);
			ScrollToElement(Extractcontent_FileLink);
			ValidateElementByTitle(Extractcontent_FileLink, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractContent_FileLinkIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Extractcontent_FileLink);
			else
				WaitForElementNotVisible(Extractcontent_FileLink, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractContent_FileLinkDisabled(bool ExpectedDisabled)
		{
			ScrollToElement(Extractcontent_FileLink);
			WaitForElementVisible(Extractcontent_FileLink);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Extractcontent_FileLink);
			}
			else
			{
				ValidateElementEnabled(Extractcontent_FileLink);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateExtractContentFieldLabelIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Extractcontent_FieldLabel);
			else
				WaitForElementNotVisible(Extractcontent_FieldLabel, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateCompletedonText(string ExpectedText)
		{
			ScrollToElement(Completedon);
			ValidateElementValue(Completedon, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnCompletedon(string TextToInsert)
		{
			WaitForElementToBeClickable(Completedon);
			ScrollToElement(Completedon);
			SendKeys(Completedon, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateCompletedOnFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Completedon);
			else
				WaitForElementNotVisible(Completedon, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateCompletedOnFieldDisabled(bool ExpectedDisabled)
		{
			ScrollToElement(Completedon);
			WaitForElementVisible(Completedon);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Completedon);
			}
			else
			{
				ValidateElementEnabled(Completedon);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ClickCompletedonDatePicker()
		{
			WaitForElementToBeClickable(CompletedonDatePicker);
			ScrollToElement(CompletedonDatePicker);
			Click(CompletedonDatePicker);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateCompletedOnDatePickerIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(CompletedonDatePicker);
			else
				WaitForElementNotVisible(CompletedonDatePicker, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateCompletedOnDatePickerDisabled(bool ExpectedDisabled)
		{
			ScrollToElement(CompletedonDatePicker);
			WaitForElementVisible(CompletedonDatePicker);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(CompletedonDatePicker);
			}
			else
			{
				ValidateElementEnabled(CompletedonDatePicker);
			}
			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateCompletedon_TimeText(string ExpectedText)
		{
			ScrollToElement(Completedon_Time);
			ValidateElementValue(Completedon_Time, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchRecordPage InsertTextOnCompletedon_Time(string TextToInsert)
		{
			WaitForElementToBeClickable(Completedon_Time);
			ScrollToElement(Completedon_Time);
			SendKeys(Completedon_Time, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateCompletedOn_TimeFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Completedon_Time);
			else
				WaitForElementNotVisible(Completedon_Time, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateCompletedOn_TimeDisabled(bool ExpectedDisabled)
		{
			ScrollToElement(Completedon_Time);
			WaitForElementVisible(Completedon_Time);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Completedon_Time);
			}
			else
			{
				ValidateElementEnabled(Completedon_Time);
			}
			return this;
		}
		public CPFinanceExtractBatchRecordPage ClickCompletedon_Time_TimePicker()
		{
			WaitForElementToBeClickable(Completedon_Time_TimePicker);
			ScrollToElement(Completedon_Time_TimePicker);
			Click(Completedon_Time_TimePicker);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateCompletedOn_TimePickerIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Completedon_Time_TimePicker);
			else
				WaitForElementNotVisible(Completedon_Time_TimePicker, 3);

			return this;
		}

		public CPFinanceExtractBatchRecordPage ValidateCompletedOn_TimePickerDisabled(bool ExpectedDisabled)
		{
			ScrollToElement(Completedon_Time_TimePicker);
			WaitForElementVisible(Completedon_Time_TimePicker);
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(Completedon_Time_TimePicker);
			}
			else
			{
				ValidateElementEnabled(Completedon_Time_TimePicker);
			}
			return this;
		}

	}
}
