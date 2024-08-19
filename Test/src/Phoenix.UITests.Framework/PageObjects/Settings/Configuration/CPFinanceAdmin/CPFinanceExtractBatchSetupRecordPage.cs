using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
	public class CPFinanceExtractBatchSetupRecordPage : CommonMethods
	{

		public CPFinanceExtractBatchSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		#region IFrames

		readonly By ContentIFrame = By.Id("CWContentIFrame");
		readonly By CPFinanceExtractBatchSetupPage_CWDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderfinanceextractbatchsetup&')]");
		readonly By CPFinanceExtractBatchSetupRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

		By cwDialog_IFrame(string parentRecordIdSuffix) => By.XPath("//iframe[contains(@id, 'iframe_CWDialog_" + parentRecordIdSuffix + "')]");		
		readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
		#endregion


		#region Labels

		By MandatoryField_Label(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']/span[@class = 'mandatory']");

		#endregion

		#region Fields

		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id = 'TI_AssignRecordButton']");
		readonly By DeleteRecordButton = By.XPath("//*[@id = 'TI_DeleteRecordButton']");

		readonly By StartDate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartDateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By EndDate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EndDateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
		readonly By StartTime = By.XPath("//*[@id='CWField_starttime']");
		readonly By StartTime_TimePicker = By.XPath("//*[@id='CWField_starttime_TimePicker']");
		readonly By CareProviderExtractNameLink = By.XPath("//*[@id='CWField_careproviderextractnameid_Link']");
		readonly By CareProviderExtractNameClearButton = By.XPath("//*[@id='CWClearLookup_careproviderextractnameid']");
		readonly By CareProviderExtractNameLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderextractnameid']");
		readonly By ExtractOnMonday_1 = By.XPath("//*[@id='CWField_extractonmonday_1']");
		readonly By ExtractOnMonday_0 = By.XPath("//*[@id='CWField_extractonmonday_0']");
		readonly By ExtractOnWednesday_1 = By.XPath("//*[@id='CWField_extractonwednesday_1']");
		readonly By ExtractOnWednesday_0 = By.XPath("//*[@id='CWField_extractonwednesday_0']");
		readonly By ExtractOnFriday_1 = By.XPath("//*[@id='CWField_extractonfriday_1']");
		readonly By ExtractOnFriday_0 = By.XPath("//*[@id='CWField_extractonfriday_0']");
		readonly By ExtractOnSunday_1 = By.XPath("//*[@id='CWField_extractonsunday_1']");
		readonly By ExtractOnSunday_0 = By.XPath("//*[@id='CWField_extractonsunday_0']");
		readonly By ExtractFrequency = By.XPath("//*[@id='CWField_extractfrequencyid']");
		readonly By ExtractOnTuesday_1 = By.XPath("//*[@id='CWField_extractontuesday_1']");
		readonly By ExtractOnTuesday_0 = By.XPath("//*[@id='CWField_extractontuesday_0']");
		readonly By ExtractOnThursday_1 = By.XPath("//*[@id='CWField_extractonthursday_1']");
		readonly By ExtractOnThursday_0 = By.XPath("//*[@id='CWField_extractonthursday_0']");
		readonly By ExtractOnSaturday_1 = By.XPath("//*[@id='CWField_extractonsaturday_1']");
		readonly By ExtractOnSaturday_0 = By.XPath("//*[@id='CWField_extractonsaturday_0']");
		readonly By CareProviderExtractTypeLink = By.XPath("//*[@id='CWField_careproviderextracttypeid_Link']");
		readonly By CareProviderExtractTypeClearButton = By.XPath("//*[@id='CWClearLookup_careproviderextracttypeid']");
		readonly By CareProviderExtractTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderextracttypeid']");
		readonly By ExcludeZeroTransactionsFromExtract_1 = By.XPath("//*[@id='CWField_excludezerotransactions_1']");
		readonly By ExcludeZeroTransactionsFromExtract_0 = By.XPath("//*[@id='CWField_excludezerotransactions_0']");
		readonly By ExtractReference = By.XPath("//*[@id='CWField_extractreference']");
		readonly By VatGlCode = By.XPath("//*[@id='CWField_vatglcode']");
		readonly By InvoiceTemplateLink = By.XPath("//*[@id='CWField_mailmergeid_Link']");
		readonly By InvoiceTemplateClearButton = By.XPath("//*[@id='CWClearLookup_mailmergeid']");
		readonly By InvoiceTemplateLookupButton = By.XPath("//*[@id='CWLookupBtn_mailmergeid']");
		readonly By ExcludeZeroTransactionsFromInvoice_1 = By.XPath("//*[@id='CWField_excludezerotransactionsfrominvoice_1']");
		readonly By ExcludeZeroTransactionsFromInvoice_0 = By.XPath("//*[@id='CWField_excludezerotransactionsfrominvoice_0']");
		readonly By PaymentTerms = By.XPath("//*[@id='CWField_cpfinanceextractbatchpaymenttermsid']");
		readonly By ShowRoomNumber_1 = By.XPath("//*[@id='CWField_showroomnumber_1']");
		readonly By ShowRoomNumber_0 = By.XPath("//*[@id='CWField_showroomnumber_0']");
		readonly By PaymentDetailToShow = By.XPath("//*[@id='CWField_cpfinanceextractbatchpaymentdetailid']");
		readonly By InvoiceTransactionsGrouping = By.XPath("//*[@id='CWField_invoicetransactionsgroupingid']");
		readonly By GenerateAndSendInvoicesAutomatically_1 = By.XPath("//*[@id='CWField_generateandsendinvoicesautomatically_1']");
		readonly By GenerateAndSendInvoicesAutomatically_0 = By.XPath("//*[@id='CWField_generateandsendinvoicesautomatically_0']");
		readonly By PaymentTermUnits = By.XPath("//*[@id='CWField_paymenttermunits']");
		readonly By ShowInvoiceText_1 = By.XPath("//*[@id='CWField_showinvoicetext_1']");
		readonly By ShowInvoiceText_0 = By.XPath("//*[@id='CWField_showinvoicetext_0']");
		readonly By ShowWeeklyBreakdown_1 = By.XPath("//*[@id='CWField_showweeklybreakdown_1']");
		readonly By ShowWeeklyBreakdown_0 = By.XPath("//*[@id='CWField_showweeklybreakdown_0']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By EmailSenderClearButton = By.XPath("//*[@id = 'CWClearLookup_emailsenderid']");
		readonly By EmailSenderLookupButton = By.XPath("//*[@id = 'CWLookupBtn_emailsenderid']");
		readonly By EmailSenderLinkText = By.XPath("//*[@id = 'CWField_emailsenderid_Link']");

		#endregion

		#region Tabs

		readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

		#endregion

		#region Warning notifications

		readonly By FormNotificationMessage = By.Id("CWNotificationMessage_DataForm");
		readonly By StartDateField_FormErrorMessage = By.XPath(" //*[@for = 'CWField_startdate'][@class = 'formerror']/span");
		readonly By StartTimeField_FormErrorMessage = By.XPath(" //*[@for = 'CWField_starttime'][@class = 'formerror']/span");
		readonly By ExtractNameField_FormErrorMessage = By.XPath("//*[@id = 'CWField_careproviderextractnameid']/following-sibling::*[@class = 'formerror']/span");
		readonly By ExtractFrequencyField_FormErrorMessage = By.XPath("//*[@for = 'CWField_extractfrequencyid'][@class = 'formerror']/span");
		readonly By ExtractTypeField_FormErrorMessage = By.XPath("//*[@for = 'CWField_careproviderextracttypeid'][@class = 'formerror']/span");
		readonly By InvoiceTemplateField_FormErrorMessage = By.XPath("//*[@for = 'CWField_mailmergeid'][@class = 'formerror']/span");
		readonly By InvoiceTransactionsGroupingField_FormErrorMessage = By.XPath("//*[@for = 'CWField_invoicetransactionsgroupingid'][@class = 'formerror']/span");
		readonly By PaymentTermsField_FormErrorMessage = By.XPath("//*[@for = 'CWField_cpfinanceextractbatchpaymenttermsid'][@class = 'formerror']/span");
		readonly By PaymentDetailToShowField_FormErrorMessage = By.XPath("//*[@for = 'CWField_cpfinanceextractbatchpaymentdetailid'][@class = 'formerror']/span");
		readonly By ResponsibleTeamField_FormErrorMessage = By.XPath("//*[@for = 'CWField_ownerid'][@class = 'formerror']/span");

		#endregion

		#region Menu Items

		readonly By MenuButton = By.Id("CWNavGroup_Menu");
		readonly By Audit_MenuItem = By.Id("CWNavItem_AuditHistory");

		#endregion

		public CPFinanceExtractBatchSetupRecordPage WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElement(ContentIFrame);
			SwitchToIframe(ContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElement(CPFinanceExtractBatchSetupPage_CWDialogIFrame);
			SwitchToIframe(CPFinanceExtractBatchSetupPage_CWDialogIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(CPFinanceExtractBatchSetupRecordPageHeader);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage WaitForCPFinanceExtractBatchSetupRecordPageToLoadFromAdvancedSearch()
		{
			SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElement(ContentIFrame);
			SwitchToIframe(ContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElement(CPFinanceExtractBatchSetupPage_CWDialogIFrame);
			SwitchToIframe(CPFinanceExtractBatchSetupPage_CWDialogIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 30);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage WaitForCPFinanceExtractBatchSetupRecordPageToLoadFromAdvancedSearch(string frameId)
		{
			SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElement(ContentIFrame);
			SwitchToIframe(ContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElement(iframe_CWDataFormDialog);
			SwitchToIframe(iframe_CWDataFormDialog);

			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElement(cwDialog_IFrame(frameId));
			SwitchToIframe(cwDialog_IFrame(frameId));

			WaitForElementNotVisible("CWRefreshPanel", 30);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateToolbarOptionsDisplayed()
		{
			ScrollToElement(SaveButton);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage NavigateToDetailsTab()
		{
			WaitForElementToBeClickable(DetailsTab);
			ScrollToElement(DetailsTab);
			Click(DetailsTab);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickMenuButton()
		{
			WaitForElementToBeClickable(MenuButton);
			ScrollToElement(MenuButton);
			Click(MenuButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage NavigateToAuditPage()
		{
			ClickMenuButton();

			WaitForElementToBeClickable(Audit_MenuItem);
			ScrollToElement(Audit_MenuItem);
			Click(Audit_MenuItem);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			ScrollToElement(BackButton);
			Click(BackButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			ScrollToElement(SaveButton);
			Click(SaveButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			ScrollToElement(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickDeleteButton()
		{
			ScrollToElement(DeleteRecordButton);
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}


		public CPFinanceExtractBatchSetupRecordPage WaitForRecordToBeSaved()
		{
			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElement(CPFinanceExtractBatchSetupRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateFinanceExtractBatchSetupRecordTitle(string ExpectedText)
		{
			ScrollToElement(CPFinanceExtractBatchSetupRecordPageHeader);
			WaitForElementVisible(CPFinanceExtractBatchSetupRecordPageHeader);
			ValidateElementByTitle(CPFinanceExtractBatchSetupRecordPageHeader, "Finance Extract Batch Setup: " + ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateMandatoryFieldIsDisplayed(string FieldName, bool ExpectedMandatory = true)
		{
			if(ExpectedMandatory)
				WaitForElementVisible(MandatoryField_Label(FieldName));
			else
				WaitForElementNotVisible(MandatoryField_Label(FieldName), 2);
			bool ActualDisplayed = GetElementVisibility(MandatoryField_Label(FieldName));
			Assert.AreEqual(ExpectedMandatory, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateFormErrorNotificationMessageIsDisplayed(string ExpectedText)
		{
			ScrollToElement(FormNotificationMessage);
			WaitForElementVisible(FormNotificationMessage);
			ValidateElementText(FormNotificationMessage, ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateStartDateMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(StartDateField_FormErrorMessage);
			ValidateElementByTitle(StartDateField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateStartTimeMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(StartTimeField_FormErrorMessage);
			ValidateElementByTitle(StartTimeField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractNameMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(ExtractNameField_FormErrorMessage);
			ValidateElementByTitle(ExtractNameField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractFrequencyMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(ExtractFrequencyField_FormErrorMessage);
			ValidateElementByTitle(ExtractFrequencyField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractTypeMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(ExtractTypeField_FormErrorMessage);
			ValidateElementByTitle(ExtractTypeField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateInvoiceTemplateMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(InvoiceTemplateField_FormErrorMessage);
			ValidateElementByTitle(InvoiceTemplateField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateInvoiceTransactionsGroupingMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(InvoiceTransactionsGroupingField_FormErrorMessage);
			ValidateElementByTitle(InvoiceTransactionsGroupingField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentTermsMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(PaymentTermsField_FormErrorMessage);
			ValidateElementByTitle(PaymentTermsField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentDetailToShowMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(PaymentDetailToShowField_FormErrorMessage);
			ValidateElementByTitle(PaymentDetailToShowField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateResponsibleTeamMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(ResponsibleTeamField_FormErrorMessage);
			ValidateElementByTitle(ResponsibleTeamField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateStartDateFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(StartDate);
			else
				WaitForElementNotVisible(StartDate, 3);
			bool ActualDisplayed = GetElementVisibility(StartDate);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateStartDateText(string ExpectedText)
		{
			ScrollToElement(StartDate);
			ValidateElementValue(StartDate, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate);
			SendKeys(StartDate, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateStartDateFieldIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(StartDate);
			else
				ValidateElementNotDisabled(StartDate);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickStartDate_DatePicker()
		{
			WaitForElementToBeClickable(StartDateDatePicker);
			Click(StartDateDatePicker);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateStartDatePickerIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(StartDateDatePicker);
			else
				ValidateElementNotDisabled(StartDateDatePicker);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateEndDateFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(EndDate);
			else
				WaitForElementNotVisible(EndDate, 3);
			bool ActualDisplayed = GetElementVisibility(EndDate);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateEndDateText(string ExpectedText)
		{
			ScrollToElement(EndDate);
			ValidateElementValue(EndDate, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage InsertTextOnEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate);
			SendKeys(EndDate, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateEndDateFieldIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(EndDate);
			else
				ValidateElementNotDisabled(EndDate);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickEndDate_DatePicker()
		{
			WaitForElementToBeClickable(EndDateDatePicker);
			Click(EndDateDatePicker);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateEndDatePickerIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(EndDateDatePicker);
			else
				ValidateElementNotDisabled(EndDateDatePicker);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateStartTimeFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(StartTime);
			else
				WaitForElementNotVisible(StartTime, 3);
			bool ActualDisplayed = GetElementVisibility(StartTime);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateStartTimeText(string ExpectedText)
		{
			ScrollToElement(StartTime);
			ValidateElementValue(StartTime, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage InsertTextOnStartTime(string TextToInsert)
		{
			WaitForElementToBeClickable(StartTime);
			SendKeys(StartTime, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateStartTimeFieldIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(StartTime);
			else
				ValidateElementNotDisabled(StartTime);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickStartTime_TimePicker()
		{
			WaitForElementToBeClickable(StartTime_TimePicker);
			Click(StartTime_TimePicker);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateStartTimePickerIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(StartTime_TimePicker);
			else
				ValidateElementNotDisabled(StartTime_TimePicker);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickCareProviderExtractNameLookupButton()
		{
			WaitForElementToBeClickable(CareProviderExtractNameLookupButton);
			ScrollToElement(CareProviderExtractNameLookupButton);
			Click(CareProviderExtractNameLookupButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickCareProviderExtractNameLink()
		{
			WaitForElementToBeClickable(CareProviderExtractNameLink);
			ScrollToElement(CareProviderExtractNameLink);
			Click(CareProviderExtractNameLink);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateCareProviderExtractNameLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CareProviderExtractNameLink);
			ScrollToElement(CareProviderExtractNameLink);
			ValidateElementByTitle(CareProviderExtractNameLink, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateCareProviderExtractNameLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(CareProviderExtractNameLookupButton);
			else
				WaitForElementNotVisible(CareProviderExtractNameLookupButton, 3);
			bool ActualDisplayed = GetElementVisibility(CareProviderExtractNameLookupButton);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateCareProviderExtractNameLookupButtonIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(CareProviderExtractNameLookupButton);
			else
				ValidateElementNotDisabled(CareProviderExtractNameLookupButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickCareProviderExtractNameClearButton()
		{
			WaitForElementToBeClickable(CareProviderExtractNameClearButton);
			ScrollToElement(CareProviderExtractNameClearButton);
			Click(CareProviderExtractNameClearButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnMonday_YesOption()
		{
			WaitForElementToBeClickable(ExtractOnMonday_1);
			ScrollToElement(ExtractOnMonday_1);
			Click(ExtractOnMonday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnMonday_YesOptionChecked()
		{
			WaitForElement(ExtractOnMonday_1);
			ScrollToElement(ExtractOnMonday_1);
			ValidateElementChecked(ExtractOnMonday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnMonday_YesOptionNotChecked()
		{
			WaitForElement(ExtractOnMonday_1);
			ScrollToElement(ExtractOnMonday_1);
			ValidateElementNotChecked(ExtractOnMonday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnMonday_YesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnMonday_1);
			else
				WaitForElementNotVisible(ExtractOnMonday_1, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnMonday_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnMonday_NoOption()
		{
			WaitForElementToBeClickable(ExtractOnMonday_0);
			ScrollToElement(ExtractOnMonday_0);
			Click(ExtractOnMonday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnMonday_NoOptionChecked()
		{
			WaitForElement(ExtractOnMonday_0);
			ScrollToElement(ExtractOnMonday_0);
			ValidateElementChecked(ExtractOnMonday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnMonday_NoOptionNotChecked()
		{
			WaitForElement(ExtractOnMonday_0);
			ScrollToElement(ExtractOnMonday_0);
			ValidateElementNotChecked(ExtractOnMonday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnMonday_NoOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnMonday_0);
			else
				WaitForElementNotVisible(ExtractOnMonday_0, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnMonday_0);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		//Tues
		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnTuesday_YesOption()
		{
			WaitForElementToBeClickable(ExtractOnTuesday_1);
			ScrollToElement(ExtractOnTuesday_1);
			Click(ExtractOnTuesday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnTuesday_YesOptionChecked()
		{
			WaitForElement(ExtractOnTuesday_1);
			ScrollToElement(ExtractOnTuesday_1);
			ValidateElementChecked(ExtractOnTuesday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnTuesday_YesOptionNotChecked()
		{
			WaitForElement(ExtractOnTuesday_1);
			ScrollToElement(ExtractOnTuesday_1);
			ValidateElementNotChecked(ExtractOnTuesday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnTuesday_YesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnTuesday_1);
			else
				WaitForElementNotVisible(ExtractOnTuesday_1, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnTuesday_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnTuesday_NoOption()
		{
			WaitForElementToBeClickable(ExtractOnTuesday_0);
			ScrollToElement(ExtractOnTuesday_0);
			Click(ExtractOnTuesday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnTuesday_NoOptionChecked()
		{
			WaitForElement(ExtractOnTuesday_0);
			ScrollToElement(ExtractOnTuesday_0);
			ValidateElementChecked(ExtractOnTuesday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnTuesday_NoOptionNotChecked()
		{
			WaitForElement(ExtractOnTuesday_0);
			ScrollToElement(ExtractOnTuesday_0);
			ValidateElementNotChecked(ExtractOnTuesday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnTuesday_NoOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnTuesday_0);
			else
				WaitForElementNotVisible(ExtractOnTuesday_0, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnTuesday_0);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnWednesday_YesOption()
		{
			WaitForElementToBeClickable(ExtractOnWednesday_1);
			ScrollToElement(ExtractOnWednesday_1);
			Click(ExtractOnWednesday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnWednesday_YesOptionChecked()
		{
			WaitForElement(ExtractOnWednesday_1);
			ScrollToElement(ExtractOnWednesday_1);
			ValidateElementChecked(ExtractOnWednesday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnWednesday_YesOptionNotChecked()
		{
			WaitForElement(ExtractOnWednesday_1);
			ScrollToElement(ExtractOnWednesday_1);
			ValidateElementNotChecked(ExtractOnWednesday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnWednesday_YesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnWednesday_1);
			else
				WaitForElementNotVisible(ExtractOnWednesday_1, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnWednesday_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnWednesday_NoOption()
		{
			WaitForElementToBeClickable(ExtractOnWednesday_0);
			ScrollToElement(ExtractOnWednesday_0);
			Click(ExtractOnWednesday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnWednesday_NoOptionChecked()
		{
			WaitForElement(ExtractOnWednesday_0);
			ScrollToElement(ExtractOnWednesday_0);
			ValidateElementChecked(ExtractOnWednesday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnWednesday_NoOptionNotChecked()
		{
			WaitForElement(ExtractOnWednesday_0);
			ScrollToElement(ExtractOnWednesday_0);
			ValidateElementNotChecked(ExtractOnWednesday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnWednesday_NoOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnWednesday_0);
			else
				WaitForElementNotVisible(ExtractOnWednesday_0, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnWednesday_0);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnThursday_YesOption()
		{
			WaitForElementToBeClickable(ExtractOnThursday_1);
			ScrollToElement(ExtractOnThursday_1);
			Click(ExtractOnThursday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnThursday_YesOptionChecked()
		{
			WaitForElement(ExtractOnThursday_1);
			ScrollToElement(ExtractOnThursday_1);
			ValidateElementChecked(ExtractOnThursday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnThursday_YesOptionNotChecked()
		{
			WaitForElement(ExtractOnThursday_1);
			ScrollToElement(ExtractOnThursday_1);
			ValidateElementNotChecked(ExtractOnThursday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnThursday_YesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnThursday_1);
			else
				WaitForElementNotVisible(ExtractOnThursday_1, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnThursday_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnThursday_NoOption()
		{
			WaitForElementToBeClickable(ExtractOnThursday_0);
			ScrollToElement(ExtractOnThursday_0);
			Click(ExtractOnThursday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnThursday_NoOptionChecked()
		{
			WaitForElement(ExtractOnThursday_0);
			ScrollToElement(ExtractOnThursday_0);
			ValidateElementChecked(ExtractOnThursday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnThursday_NoOptionNotChecked()
		{
			WaitForElement(ExtractOnThursday_0);
			ScrollToElement(ExtractOnThursday_0);
			ValidateElementNotChecked(ExtractOnThursday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnThursday_NoOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnThursday_0);
			else
				WaitForElementNotVisible(ExtractOnThursday_0, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnThursday_0);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnFriday_YesOption()
		{
			WaitForElementToBeClickable(ExtractOnFriday_1);
			ScrollToElement(ExtractOnFriday_1);
			Click(ExtractOnFriday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnFriday_YesOptionChecked()
		{
			WaitForElement(ExtractOnFriday_1);
			ScrollToElement(ExtractOnFriday_1);
			ValidateElementChecked(ExtractOnFriday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnFriday_YesOptionNotChecked()
		{
			WaitForElement(ExtractOnFriday_1);
			ScrollToElement(ExtractOnFriday_1);
			ValidateElementNotChecked(ExtractOnFriday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnFriday_YesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnFriday_1);
			else
				WaitForElementNotVisible(ExtractOnFriday_1, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnFriday_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnFriday_NoOption()
		{
			WaitForElementToBeClickable(ExtractOnFriday_0);
			ScrollToElement(ExtractOnFriday_0);
			Click(ExtractOnFriday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnFriday_NoOptionChecked()
		{
			WaitForElement(ExtractOnFriday_0);
			ScrollToElement(ExtractOnFriday_0);
			ValidateElementChecked(ExtractOnFriday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnFriday_NoOptionNotChecked()
		{
			WaitForElement(ExtractOnFriday_0);
			ScrollToElement(ExtractOnFriday_0);
			ValidateElementNotChecked(ExtractOnFriday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnFriday_NoOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnFriday_0);
			else
				WaitForElementNotVisible(ExtractOnFriday_0, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnFriday_0);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnSaturday_YesOption()
		{
			WaitForElementToBeClickable(ExtractOnSaturday_1);
			ScrollToElement(ExtractOnSaturday_1);
			Click(ExtractOnSaturday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnSaturday_YesOptionChecked()
		{
			WaitForElement(ExtractOnSaturday_1);
			ScrollToElement(ExtractOnSaturday_1);
			ValidateElementChecked(ExtractOnSaturday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnSaturday_YesOptionNotChecked()
		{
			WaitForElement(ExtractOnSaturday_1);
			ScrollToElement(ExtractOnSaturday_1);
			ValidateElementNotChecked(ExtractOnSaturday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnSaturday_YesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnSaturday_1);
			else
				WaitForElementNotVisible(ExtractOnSaturday_1, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnSaturday_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnSaturday_NoOption()
		{
			WaitForElementToBeClickable(ExtractOnSaturday_0);
			ScrollToElement(ExtractOnSaturday_0);
			Click(ExtractOnSaturday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnSaturday_NoOptionChecked()
		{
			WaitForElement(ExtractOnSaturday_0);
			ScrollToElement(ExtractOnSaturday_0);
			ValidateElementChecked(ExtractOnSaturday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnSaturday_NoOptionNotChecked()
		{
			WaitForElement(ExtractOnSaturday_0);
			ScrollToElement(ExtractOnSaturday_0);
			ValidateElementNotChecked(ExtractOnSaturday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnSaturday_NoOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnSaturday_0);
			else
				WaitForElementNotVisible(ExtractOnSaturday_0, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnSaturday_0);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnSunday_YesOption()
		{
			WaitForElementToBeClickable(ExtractOnSunday_1);
			ScrollToElement(ExtractOnSunday_1);
			Click(ExtractOnSunday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnSunday_YesOptionChecked()
		{
			WaitForElement(ExtractOnSunday_1);
			ScrollToElement(ExtractOnSunday_1);
			ValidateElementChecked(ExtractOnSunday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnSunday_YesOptionNotChecked()
		{
			WaitForElement(ExtractOnSunday_1);
			ScrollToElement(ExtractOnSunday_1);
			ValidateElementNotChecked(ExtractOnSunday_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnSunday_YesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnSunday_1);
			else
				WaitForElementNotVisible(ExtractOnSunday_1, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnSunday_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExtractOnSunday_NoOption()
		{
			WaitForElementToBeClickable(ExtractOnSunday_0);
			ScrollToElement(ExtractOnSunday_0);
			Click(ExtractOnSunday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnSunday_NoOptionChecked()
		{
			WaitForElement(ExtractOnSunday_0);
			ScrollToElement(ExtractOnSunday_0);
			ValidateElementChecked(ExtractOnSunday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnSunday_NoOptionNotChecked()
		{
			WaitForElement(ExtractOnSunday_0);
			ScrollToElement(ExtractOnSunday_0);
			ValidateElementNotChecked(ExtractOnSunday_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractOnSunday_NoOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractOnSunday_0);
			else
				WaitForElementNotVisible(ExtractOnSunday_0, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractOnSunday_0);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}


		public CPFinanceExtractBatchSetupRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			ValidateElementByTitle(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateResponsibleTeamLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ResponsibleTeamLookupButton);
			else
				WaitForElementNotVisible(ResponsibleTeamLookupButton, 3);
			bool ActualDisplayed = GetElementVisibility(ResponsibleTeamLookupButton);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(ResponsibleTeamLookupButton);
			else
				ValidateElementNotDisabled(ResponsibleTeamLookupButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickResponsibleTeamClearButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamClearButton);
			ScrollToElement(ResponsibleTeamClearButton);
			Click(ResponsibleTeamClearButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			ScrollToElement(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage SelectExtractFrequency(string TextToSelect)
		{
			WaitForElementToBeClickable(ExtractFrequency);
			SelectPicklistElementByText(ExtractFrequency, TextToSelect);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractFrequencySelectedText(string ExpectedText)
		{
			ScrollToElement(ExtractFrequency);
			ValidatePicklistSelectedText(ExtractFrequency, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractFrequencyIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractFrequency);
			else
				WaitForElementNotVisible(ExtractFrequency, 3);
			bool ActualDisplayed = GetElementVisibility(ExtractFrequency);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractFrequencyOptions(string OptionText)
		{
			WaitForElementVisible(ExtractFrequency);
			ValidatePicklistContainsElementByText(ExtractFrequency, OptionText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractFrequencyOptionSetId(Guid OptionSetId)
		{
			WaitForElementVisible(ExtractFrequency);
			ValidateElementAttribute(ExtractFrequency, "opsetid", OptionSetId.ToString());
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickCareProviderExtractTypeLookupButton()
		{
			WaitForElementToBeClickable(CareProviderExtractTypeLookupButton);
			ScrollToElement(CareProviderExtractTypeLookupButton);
			Click(CareProviderExtractTypeLookupButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickCareProviderExtractTypeLink()
		{
			WaitForElementToBeClickable(CareProviderExtractTypeLink);
			ScrollToElement(CareProviderExtractTypeLink);
			Click(CareProviderExtractTypeLink);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateCareProviderExtractTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CareProviderExtractTypeLink);
			ScrollToElement(CareProviderExtractTypeLink);
			ValidateElementByTitle(CareProviderExtractTypeLink, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateCareProviderExtractTypeLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(CareProviderExtractTypeLookupButton);
			else
				WaitForElementNotVisible(CareProviderExtractTypeLookupButton, 3);
			bool ActualDisplayed = GetElementVisibility(CareProviderExtractTypeLookupButton);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateCareProviderExtractTypeLookupButtonIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(CareProviderExtractTypeLookupButton);
			else
				ValidateElementNotDisabled(CareProviderExtractTypeLookupButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickCareProviderExtractTypeClearButton()
		{
			WaitForElementToBeClickable(CareProviderExtractTypeClearButton);
			ScrollToElement(CareProviderExtractTypeClearButton);
			Click(CareProviderExtractTypeClearButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExcludeZeroTransactionsFromExtract_YesOption()
		{
			WaitForElementToBeClickable(ExcludeZeroTransactionsFromExtract_1);
			Click(ExcludeZeroTransactionsFromExtract_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExcludeZeroTransactionsFromExtract_YesOptionChecked()
		{
			WaitForElement(ExcludeZeroTransactionsFromExtract_1);
			ValidateElementChecked(ExcludeZeroTransactionsFromExtract_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExcludeZeroTransactionsFromExtract_YesOptionNotChecked()
		{
			WaitForElement(ExcludeZeroTransactionsFromExtract_1);
			ValidateElementNotChecked(ExcludeZeroTransactionsFromExtract_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExcludeZeroTransactionsFromExtract_NoOption()
		{
			WaitForElementToBeClickable(ExcludeZeroTransactionsFromExtract_0);
			Click(ExcludeZeroTransactionsFromExtract_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExcludeZeroTransactionsFromExtract_NoOptionChecked()
		{
			WaitForElement(ExcludeZeroTransactionsFromExtract_0);
			ValidateElementChecked(ExcludeZeroTransactionsFromExtract_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExcludeZeroTransactionsFromExtract_NoOptionNotChecked()
		{
			WaitForElement(ExcludeZeroTransactionsFromExtract_0);
			ValidateElementNotChecked(ExcludeZeroTransactionsFromExtract_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExcludeZeroTransactionsFromExtractOptionsAreDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(ExcludeZeroTransactionsFromExtract_1);
				WaitForElementVisible(ExcludeZeroTransactionsFromExtract_0);
			}
			else
			{
				WaitForElementNotVisible(ExcludeZeroTransactionsFromExtract_1, 3);
				WaitForElementNotVisible(ExcludeZeroTransactionsFromExtract_0, 3);
			}

			return this;
		}


		public CPFinanceExtractBatchSetupRecordPage ValidateExtractReferenceText(string ExpectedText)
		{
			ScrollToElement(ExtractReference);
			ValidateElementValue(ExtractReference, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage InsertTextOnExtractReference(string TextToInsert)
		{
			WaitForElementToBeClickable(ExtractReference);
			SendKeys(ExtractReference, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExtractReferenceFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ExtractReference);
			else
				WaitForElementNotVisible(ExtractReference, 3);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateVatGlCodeText(string ExpectedText)
		{
			ScrollToElement(VatGlCode);
			ValidateElementValue(VatGlCode, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage InsertTextOnVatGlCode(string TextToInsert)
		{
			WaitForElementToBeClickable(VatGlCode);
			SendKeys(VatGlCode, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateVatGlCodeFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(VatGlCode);
			else
				WaitForElementNotVisible(VatGlCode, 3);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickInvoiceTemplateLink()
		{
			WaitForElementToBeClickable(InvoiceTemplateLink);
			Click(InvoiceTemplateLink);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateInvoiceTemplateLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(InvoiceTemplateLink);
			ValidateElementText(InvoiceTemplateLink, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickInvoiceTemplateLinkClearButton()
		{
			WaitForElementToBeClickable(InvoiceTemplateClearButton);
			Click(InvoiceTemplateClearButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickInvoiceTemplateLookupButton()
		{
			WaitForElementToBeClickable(InvoiceTemplateLookupButton);
			Click(InvoiceTemplateLookupButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateInvoiceTemplateLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(InvoiceTemplateLookupButton);
			else
				WaitForElementNotVisible(InvoiceTemplateLookupButton, 3);
			bool ActualDisplayed = GetElementVisibility(InvoiceTemplateLookupButton);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateInvoiceTemplateLookupButtonIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(InvoiceTemplateLookupButton);
			else
				ValidateElementNotDisabled(InvoiceTemplateLookupButton);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExcludeZeroTransactionsFromInvoice_YesOption()
		{
			WaitForElementToBeClickable(ExcludeZeroTransactionsFromInvoice_1);
			Click(ExcludeZeroTransactionsFromInvoice_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExcludeZeroTransactionsFromInvoice_YesOptionChecked()
		{
			WaitForElement(ExcludeZeroTransactionsFromInvoice_1);
			ValidateElementChecked(ExcludeZeroTransactionsFromInvoice_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExcludeZeroTransactionsFromInvoice_YesOptionNotChecked()
		{
			WaitForElement(ExcludeZeroTransactionsFromInvoice_1);
			ValidateElementNotChecked(ExcludeZeroTransactionsFromInvoice_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickExcludeZeroTransactionsFromInvoice_NoOption()
		{
			WaitForElementToBeClickable(ExcludeZeroTransactionsFromInvoice_0);
			Click(ExcludeZeroTransactionsFromInvoice_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExcludeZeroTransactionsFromInvoice_NoOptionChecked()
		{
			WaitForElement(ExcludeZeroTransactionsFromInvoice_0);
			ValidateElementChecked(ExcludeZeroTransactionsFromInvoice_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExcludeZeroTransactionsFromInvoice_NoOptionNotChecked()
		{
			WaitForElement(ExcludeZeroTransactionsFromInvoice_0);
			ValidateElementNotChecked(ExcludeZeroTransactionsFromInvoice_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateExcludeZeroTransactionsFromInvoiceOptionsAreDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(ExcludeZeroTransactionsFromInvoice_1);
				WaitForElementVisible(ExcludeZeroTransactionsFromInvoice_0);
			}
			else
			{
				WaitForElementNotVisible(ExcludeZeroTransactionsFromInvoice_1, 3);
				WaitForElementNotVisible(ExcludeZeroTransactionsFromInvoice_0, 3);
			}

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage SelectPaymentTerms(string TextToSelect)
		{
			WaitForElementToBeClickable(PaymentTerms);
			SelectPicklistElementByText(PaymentTerms, TextToSelect);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentTermsSelectedText(string ExpectedText)
		{
			ScrollToElement(PaymentTerms);
			ValidatePicklistSelectedText(PaymentTerms, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentTermsFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(PaymentTerms);
			}
			else
			{
				WaitForElementNotVisible(PaymentTerms, 3);
			}

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentTermsFieldIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(PaymentTerms);
			}
			else
			{
				ValidateElementEnabled(PaymentTerms);
			}

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentTermsOptions(string OptionText)
		{
			WaitForElementVisible(PaymentTerms);
			ValidatePicklistContainsElementByText(PaymentTerms, OptionText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickShowRoomNumber_YesOption()
		{
			WaitForElementToBeClickable(ShowRoomNumber_1);
			Click(ShowRoomNumber_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowRoomNumber_YesOptionChecked()
		{
			WaitForElement(ShowRoomNumber_1);
			ValidateElementChecked(ShowRoomNumber_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowRoomNumber_YesOptionNotChecked()
		{
			WaitForElement(ShowRoomNumber_1);
			ValidateElementNotChecked(ShowRoomNumber_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickShowRoomNumber_NoOption()
		{
			WaitForElementToBeClickable(ShowRoomNumber_0);
			Click(ShowRoomNumber_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowRoomNumber_NoOptionChecked()
		{
			WaitForElement(ShowRoomNumber_0);
			ValidateElementChecked(ShowRoomNumber_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowRoomNumber_NoOptionNotChecked()
		{
			WaitForElement(ShowRoomNumber_0);
			ValidateElementNotChecked(ShowRoomNumber_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowRoomNumberOptionsAreDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(ShowRoomNumber_1);
				WaitForElementVisible(ShowRoomNumber_0);
			}
			else
			{
				WaitForElementNotVisible(ShowRoomNumber_1, 3);
				WaitForElementNotVisible(ShowRoomNumber_0, 3);
			}

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage SelectPaymentDetail(string TextToSelect)
		{
			WaitForElementToBeClickable(PaymentDetailToShow);
			SelectPicklistElementByText(PaymentDetailToShow, TextToSelect);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentDetailSelectedText(string ExpectedText)
		{
			ScrollToElement(PaymentDetailToShow);
			ValidatePicklistSelectedText(PaymentDetailToShow, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentDetailToShowFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(PaymentDetailToShow);
			else
				WaitForElementNotVisible(PaymentDetailToShow, 3);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentDetailToShowFieldIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(PaymentDetailToShow);
			else
				ValidateElementEnabled(PaymentDetailToShow);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentDetailToShowOptions(string OptionText)
		{
			WaitForElementVisible(PaymentDetailToShow);
			ValidatePicklistContainsElementByText(PaymentDetailToShow, OptionText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentDetailToShowOptionSetId(Guid OptionSetId)
		{
			WaitForElementVisible(PaymentDetailToShow);
			ValidateElementAttribute(PaymentDetailToShow, "opsetid", OptionSetId.ToString());
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage SelectInvoiceTransactionsGrouping(string TextToSelect)
		{
			WaitForElementToBeClickable(InvoiceTransactionsGrouping);
			SelectPicklistElementByText(InvoiceTransactionsGrouping, TextToSelect);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateInvoiceTransactionsGroupingSelectedText(string ExpectedText)
		{
			ScrollToElement(InvoiceTransactionsGrouping);
			ValidatePicklistSelectedText(InvoiceTransactionsGrouping, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateInvoiceTransactionsGroupingIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(InvoiceTransactionsGrouping);
			else
				WaitForElementNotVisible(InvoiceTransactionsGrouping, 3);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateInvoiceTransactionsGroupingOptions(string OptionText)
		{
			WaitForElementVisible(InvoiceTransactionsGrouping);
			ValidatePicklistContainsElementByText(InvoiceTransactionsGrouping, OptionText);
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateInvoiceTransactionsGroupingOptionSetId(Guid OptionSetId)
		{
			WaitForElementVisible(InvoiceTransactionsGrouping);
			ValidateElementAttribute(InvoiceTransactionsGrouping, "opsetid", OptionSetId.ToString());
			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickGenerateAndSendInvoicesAutomatically_YesOption()
		{
			WaitForElementToBeClickable(GenerateAndSendInvoicesAutomatically_1);
			Click(GenerateAndSendInvoicesAutomatically_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateGenerateAndSendInvoicesAutomatically_YesOptionChecked()
		{
			WaitForElement(GenerateAndSendInvoicesAutomatically_1);
			ValidateElementChecked(GenerateAndSendInvoicesAutomatically_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateGenerateAndSendInvoicesAutomatically_YesOptionNotChecked()
		{
			WaitForElement(GenerateAndSendInvoicesAutomatically_1);
			ValidateElementNotChecked(GenerateAndSendInvoicesAutomatically_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickGenerateAndSendInvoicesAutomatically_NoOption()
		{
			WaitForElementToBeClickable(GenerateAndSendInvoicesAutomatically_0);
			Click(GenerateAndSendInvoicesAutomatically_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateGenerateAndSendInvoicesAutomatically_NoOptionChecked()
		{
			WaitForElement(GenerateAndSendInvoicesAutomatically_0);
			ValidateElementChecked(GenerateAndSendInvoicesAutomatically_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateGenerateAndSendInvoicesAutomatically_NoOptionNotChecked()
		{
			WaitForElement(GenerateAndSendInvoicesAutomatically_0);
			ValidateElementNotChecked(GenerateAndSendInvoicesAutomatically_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateGenerateAndSendInvoicesAutomaticallyOptionsAreDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(GenerateAndSendInvoicesAutomatically_1);
				WaitForElementVisible(GenerateAndSendInvoicesAutomatically_0);
			}
			else
			{
				WaitForElementNotVisible(GenerateAndSendInvoicesAutomatically_1, 3);
				WaitForElementNotVisible(GenerateAndSendInvoicesAutomatically_0, 3);
			}

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentTermUnitsText(string ExpectedText)
		{
			ValidateElementValue(PaymentTermUnits, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage InsertTextOnPaymentTermUnits(string TextToInsert)
		{
			WaitForElementToBeClickable(PaymentTermUnits);
			SendKeys(PaymentTermUnits, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentTermUnitsIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(PaymentTermUnits);
			else
				WaitForElementNotVisible(PaymentTermUnits, 3);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentTermUnitsFieldIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
			{
				ValidateElementDisabled(PaymentTermUnits);
			}
			else
			{
				ValidateElementEnabled(PaymentTermUnits);
			}

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickShowInvoiceText_YesOption()
		{
			WaitForElementToBeClickable(ShowInvoiceText_1);
			Click(ShowInvoiceText_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowInvoiceText_YesOptionChecked()
		{
			WaitForElement(ShowInvoiceText_1);
			ValidateElementChecked(ShowInvoiceText_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowInvoiceText_YesOptionNotChecked()
		{
			WaitForElement(ShowInvoiceText_1);
			ValidateElementNotChecked(ShowInvoiceText_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickShowInvoiceText_NoOption()
		{
			WaitForElementToBeClickable(ShowInvoiceText_0);
			Click(ShowInvoiceText_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowInvoiceText_NoOptionChecked()
		{
			WaitForElement(ShowInvoiceText_0);
			ValidateElementChecked(ShowInvoiceText_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowInvoiceText_NoOptionNotChecked()
		{
			WaitForElement(ShowInvoiceText_0);
			ValidateElementNotChecked(ShowInvoiceText_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowInvoiceTextOptionsAreDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(ShowInvoiceText_1);
				WaitForElementVisible(ShowInvoiceText_0);
			}
			else
			{
				WaitForElementNotVisible(ShowInvoiceText_1, 3);
				WaitForElementNotVisible(ShowInvoiceText_0, 3);
			}

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickShowWeeklyBreakdown_YesOption()
		{
			WaitForElementToBeClickable(ShowWeeklyBreakdown_1);
			Click(ShowWeeklyBreakdown_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowWeeklyBreakdown_YesOptionChecked()
		{
			WaitForElement(ShowWeeklyBreakdown_1);
			ValidateElementChecked(ShowWeeklyBreakdown_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowWeeklyBreakdown_YesOptionNotChecked()
		{
			WaitForElement(ShowWeeklyBreakdown_1);
			ValidateElementNotChecked(ShowWeeklyBreakdown_1);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ClickShowWeeklyBreakdown_NoOption()
		{
			WaitForElementToBeClickable(ShowWeeklyBreakdown_0);
			Click(ShowWeeklyBreakdown_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowWeeklyBreakdown_NoOptionChecked()
		{
			WaitForElement(ShowWeeklyBreakdown_0);
			ValidateElementChecked(ShowWeeklyBreakdown_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowWeeklyBreakdown_NoOptionNotChecked()
		{
			WaitForElement(ShowWeeklyBreakdown_0);
			ValidateElementNotChecked(ShowWeeklyBreakdown_0);

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidateShowWeeklyBreakdownOptionsAreDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
			{
				WaitForElementVisible(ShowWeeklyBreakdown_1);
				WaitForElementVisible(ShowWeeklyBreakdown_0);
			}
			else
			{
				WaitForElementNotVisible(ShowWeeklyBreakdown_1, 3);
				WaitForElementNotVisible(ShowWeeklyBreakdown_0, 3);
			}

			return this;
		}

		public CPFinanceExtractBatchSetupRecordPage ValidatePaymentTermsUnitsFieldRangeValue(string ExpectedRange)
		{
			ScrollToElement(PaymentTermUnits);
			WaitForElementVisible(PaymentTermUnits);
			ValidateElementAttribute(PaymentTermUnits, "range", ExpectedRange);
			return this;
		}

		//method to click email sender lookup button
		public CPFinanceExtractBatchSetupRecordPage ClickEmailSenderLookupButton()
		{
            WaitForElementToBeClickable(EmailSenderLookupButton);
            ScrollToElement(EmailSenderLookupButton);
            Click(EmailSenderLookupButton);

            return this;
        }

		//validate email sender lookup button is disabled
		public CPFinanceExtractBatchSetupRecordPage ValidateEmailSenderLookupButtonIsDisabled(bool ExpectedDisabled)
		{
            if (ExpectedDisabled)
                ValidateElementDisabled(EmailSenderLookupButton);
            else
                ValidateElementNotDisabled(EmailSenderLookupButton);

            return this;
        }

		//method to validate email sender link text
		public CPFinanceExtractBatchSetupRecordPage ValidateEmailSenderLinkText(string ExpectedText)
		{
            WaitForElementToBeClickable(EmailSenderLinkText);
            ScrollToElement(EmailSenderLinkText);
            ValidateElementByTitle(EmailSenderLinkText, ExpectedText);

            return this;
        }

		//method to click email sender clear button
		public CPFinanceExtractBatchSetupRecordPage ClickEmailSenderClearButton()
		{
            WaitForElementToBeClickable(EmailSenderClearButton);
            ScrollToElement(EmailSenderClearButton);
            Click(EmailSenderClearButton);

            return this;
        }
	}
}
