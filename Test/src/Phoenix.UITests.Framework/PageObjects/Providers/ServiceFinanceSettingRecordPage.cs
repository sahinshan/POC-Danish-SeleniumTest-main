using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
	public class ServiceFinanceSettingRecordPage : CommonMethods
	{

		public ServiceFinanceSettingRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}
		readonly By CWContentIFrame = By.Id("CWContentIFrame");
		readonly By ServiceFinanceSettingRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=servicefinancesettings&')]");

		readonly By pageHeader = By.XPath("//h1");

		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
		readonly By RestrictAccessButton = By.Id("TI_RestrictAccessButton");
		readonly By AdditionalToolbarButton = By.XPath("//div[@id='CWToolbarMenu']/button");
		readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

		readonly By ServiceProvidedLink = By.XPath("//*[@id='CWField_serviceprovidedid_Link']");
		readonly By ServiceProvidedLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceprovidedid']");
		readonly By StartDate_Field = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartDateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By PaymentTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_paymenttypecodeid']");
		readonly By VATCodeLookupButton = By.XPath("//*[@id='CWLookupBtn_vatcodeid']");
		readonly By ChargeUsingNumberOfCarers_Yes = By.XPath("//*[@id='CWField_chargeusingnumberofcarers_1']");
		readonly By ChargeUsingNumberOfCarers_No = By.XPath("//*[@id='CWField_chargeusingnumberofcarers_0']");
		readonly By UsedInBatchSetup_Yes = By.XPath("//*[@id='CWField_usedinbatchsetup_1']");
		readonly By UsedInBatchSetup_No = By.XPath("//*[@id='CWField_usedinbatchsetup_0']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By EndDate_Field = By.XPath("//*[@id='CWField_enddate']");
		readonly By EndDateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
		readonly By ProviderBatchGroupingLookupButton = By.XPath("//*[@id='CWLookupBtn_providerbatchgroupingid']");
		readonly By AdjustedDays_Field = By.XPath("//*[@id='CWField_adjusteddays']");
		readonly By EndReasonRulesApply_Yes = By.XPath("//*[@id='CWField_endreasonrulesapply_1']");
		readonly By EndReasonRulesApply_No = By.XPath("//*[@id='CWField_endreasonrulesapply_0']");
		readonly By VATApplyToCharging_Yes = By.XPath("//*[@id='CWField_vatapplycharging_1']");
		readonly By VATApplyToCharging_No = By.XPath("//*[@id='CWField_vatapplycharging_0']");
		readonly By Notes_Field = By.XPath("//*[@id='CWField_notes']");

		readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");
		readonly By paymentType_NotificationMessage = By.XPath("//*[@id='CWControlHolder_paymenttypecodeid']/label/span");
		readonly By ProviderBatchGrouping_NotificationMessage = By.XPath("//*[@id='CWControlHolder_providerbatchgroupingid']/label/span");
		readonly By VATCode_NotificationMessage = By.XPath("//*[@id='CWControlHolder_vatcodeid']/label/span");
		readonly By AdjustedDays_NotificationMessage = By.XPath("//*[@id='CWControlHolder_adjusteddays']/label/span");


		public ServiceFinanceSettingRecordPage WaitForServiceFinanceSettingRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElement(CWContentIFrame);
			SwitchToIframe(CWContentIFrame);

			WaitForElement(ServiceFinanceSettingRecordIFrame);
			SwitchToIframe(ServiceFinanceSettingRecordIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 60);

			WaitForElement(pageHeader);

			WaitForElement(SaveButton);
			WaitForElement(SaveAndCloseButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage WaitForRecordToBeSaved()
		{
			WaitForElementNotVisible("CWRefreshPanel", 15);

			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(RestrictAccessButton);
			WaitForElementVisible(AdditionalToolbarButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickDeleteButton()
		{
			WaitForElementToBeClickable(AdditionalToolbarButton);
			Click(AdditionalToolbarButton);

			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickServiceProvidedLink()
		{
			WaitForElementToBeClickable(ServiceProvidedLink);
			Click(ServiceProvidedLink);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateServiceprovidedidLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ServiceProvidedLink);
			ValidateElementText(ServiceProvidedLink, ExpectedText);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickServiceProvidedLookupButton()
		{
			WaitForElementToBeClickable(ServiceProvidedLookupButton);
			Click(ServiceProvidedLookupButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateStartdateText(string ExpectedText)
		{
			ValidateElementValue(StartDate_Field, ExpectedText);

			return this;
		}

		public ServiceFinanceSettingRecordPage InsertTextOnStartdate(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate_Field);
			MoveToElementInPage(StartDate_Field);
			SendKeys(StartDate_Field, TextToInsert);
			SendKeysWithoutClearing(StartDate_Field, Keys.Tab);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickStartdateDatePicker()
		{
			WaitForElementToBeClickable(StartDateDatePicker);
			Click(StartDateDatePicker);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickPaymentTypeLookupButton()
		{
			WaitForElementToBeClickable(PaymentTypeLookupButton);
			Click(PaymentTypeLookupButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickVATCodeLookupButton()
		{
			WaitForElementToBeClickable(VATCodeLookupButton);
			Click(VATCodeLookupButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickChargeUsingNumberOfCarers_Yes()
		{
			WaitForElementToBeClickable(ChargeUsingNumberOfCarers_Yes);
			Click(ChargeUsingNumberOfCarers_Yes);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateChargeUsingNumberOfCarers_OptionChecked(bool YesOptionChecked)
		{
			WaitForElement(ChargeUsingNumberOfCarers_Yes);
			WaitForElement(ChargeUsingNumberOfCarers_No);
			MoveToElementInPage(ChargeUsingNumberOfCarers_Yes);

			if (YesOptionChecked)
			{
				ValidateElementChecked(ChargeUsingNumberOfCarers_Yes);
				ValidateElementNotChecked(ChargeUsingNumberOfCarers_No);
			}
			else
			{
				ValidateElementChecked(ChargeUsingNumberOfCarers_No);
				ValidateElementNotChecked(ChargeUsingNumberOfCarers_Yes);
			}

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickChargeUsingNumberOfCarers_No()
		{
			WaitForElementToBeClickable(ChargeUsingNumberOfCarers_No);
			Click(ChargeUsingNumberOfCarers_No);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickUsedInBatchSetup_Yes()
		{
			WaitForElementToBeClickable(UsedInBatchSetup_Yes);
			Click(UsedInBatchSetup_Yes);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateUsedInBatchSetup_OptionChecked(bool YesOptionChecked)
		{
			WaitForElement(UsedInBatchSetup_Yes);
			WaitForElement(UsedInBatchSetup_No);
			MoveToElementInPage(UsedInBatchSetup_Yes);

			if (YesOptionChecked)
			{
				ValidateElementChecked(UsedInBatchSetup_Yes);
				ValidateElementNotChecked(UsedInBatchSetup_No);
			}
			else
            {
				ValidateElementChecked(UsedInBatchSetup_No);
				ValidateElementNotChecked(UsedInBatchSetup_Yes);
			}

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickUsedInBatchSetup_No()
		{
			WaitForElementToBeClickable(UsedInBatchSetup_No);
			Click(UsedInBatchSetup_No);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickResponsibleTeamClearButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamClearButton);
			Click(ResponsibleTeamClearButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateEnddateText(string ExpectedText)
		{
			ValidateElementValue(EndDate_Field, ExpectedText);

			return this;
		}

		public ServiceFinanceSettingRecordPage InsertTextOnEnddate(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate_Field);
			SendKeys(EndDate_Field, TextToInsert);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EndDateDatePicker);
			Click(EndDateDatePicker);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickProviderBatchGroupingLookupButton()
		{
			WaitForElementToBeClickable(ProviderBatchGroupingLookupButton);
			Click(ProviderBatchGroupingLookupButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateAdjustedDaysText(string ExpectedText)
		{
			ValidateElementValue(AdjustedDays_Field, ExpectedText);

			return this;
		}

		public ServiceFinanceSettingRecordPage InsertTextOnAdjustedDays(string TextToInsert)
		{
			WaitForElementToBeClickable(AdjustedDays_Field);
			SendKeys(AdjustedDays_Field, TextToInsert);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickEndReasonRulesApply_YesOption()
		{
			WaitForElementToBeClickable(EndReasonRulesApply_Yes);
			Click(EndReasonRulesApply_Yes);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateEndreasonrulesapply_OptionChecked(bool YesOptionChecked)
		{
			WaitForElementVisible(EndReasonRulesApply_Yes);
			WaitForElementVisible(EndReasonRulesApply_No);
			MoveToElementInPage(EndReasonRulesApply_Yes);

			if (YesOptionChecked)
			{
				ValidateElementChecked(EndReasonRulesApply_Yes);
				ValidateElementNotChecked(EndReasonRulesApply_No);
			}
			else
			{
				ValidateElementChecked(EndReasonRulesApply_No);
				ValidateElementNotChecked(EndReasonRulesApply_Yes);
			}

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickEndReasonRulesApply_No()
		{
			WaitForElementToBeClickable(EndReasonRulesApply_No);
			Click(EndReasonRulesApply_No);

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickVATApplyCharging_Yes()
		{
			WaitForElementToBeClickable(VATApplyToCharging_Yes);
			Click(VATApplyToCharging_Yes);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateVATApplyCharging_OptionChecked(bool YesOptionChecked)
		{
			WaitForElementVisible(VATApplyToCharging_Yes);
			WaitForElementVisible(VATApplyToCharging_No);
			MoveToElementInPage(VATApplyToCharging_Yes);

			if (YesOptionChecked)
			{
				ValidateElementChecked(VATApplyToCharging_Yes);
				ValidateElementNotChecked(VATApplyToCharging_No);
			}
			else
			{
				ValidateElementChecked(VATApplyToCharging_No);
				ValidateElementNotChecked(VATApplyToCharging_Yes);
			}

			return this;
		}

		public ServiceFinanceSettingRecordPage ClickVATApplyCharging_No()
		{
			WaitForElementToBeClickable(VATApplyToCharging_No);
			Click(VATApplyToCharging_No);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateNotesText(string ExpectedText)
		{
			WaitForElementVisible(Notes_Field);
			MoveToElementInPage(Notes_Field);
			ValidateElementText(Notes_Field, ExpectedText);

			return this;
		}

		public ServiceFinanceSettingRecordPage InsertTextOnNotes(string TextToInsert)
		{
			WaitForElementToBeClickable(Notes_Field);
			SendKeys(Notes_Field, TextToInsert);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateNotificationMessageText(String ExpectedText)
		{
			WaitForElementVisible(NotificationMessage);
			MoveToElementInPage(NotificationMessage);
			ValidateElementTextContainsText(NotificationMessage, ExpectedText);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidatePaymentTypeNotificationMessageText(string ExpectedText)
		{
			WaitForElementVisible(paymentType_NotificationMessage);
			MoveToElementInPage(paymentType_NotificationMessage);
			ValidateElementText(paymentType_NotificationMessage, ExpectedText);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateProviderBatchGroupingNotificationMessageText(string ExpectedText)
		{
			WaitForElementVisible(ProviderBatchGrouping_NotificationMessage);
			MoveToElementInPage(ProviderBatchGrouping_NotificationMessage);
			ValidateElementText(ProviderBatchGrouping_NotificationMessage, ExpectedText);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateVATCodeNotificationMessageText(string ExpectedText)
		{
			WaitForElementVisible(VATCode_NotificationMessage);
			MoveToElementInPage(VATCode_NotificationMessage);
			ValidateElementText(VATCode_NotificationMessage, ExpectedText);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateAdjustedDaysNotificationMessageText(string ExpectedText)
		{
			WaitForElementVisible(AdjustedDays_NotificationMessage);
			MoveToElementInPage(AdjustedDays_NotificationMessage);
			ValidateElementText(AdjustedDays_NotificationMessage, ExpectedText);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateResponsibleTeamLookupDisabled(bool ElementDisabled)
		{
			WaitForElementVisible(ResponsibleTeamLookupButton);
			MoveToElementInPage(ResponsibleTeamLookupButton);

			if (ElementDisabled)
				ValidateElementDisabled(ResponsibleTeamLookupButton);
			else
				ValidateElementNotDisabled(ResponsibleTeamLookupButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidatePaymentTypeLookupDisabled(bool ElementDisabled)
		{
			WaitForElementVisible(PaymentTypeLookupButton);
			MoveToElementInPage(PaymentTypeLookupButton);

			if (ElementDisabled)
				ValidateElementDisabled(PaymentTypeLookupButton);
			else
				ValidateElementNotDisabled(PaymentTypeLookupButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateProviderBatchGroupingLookupDisabled(bool ElementDisabled)
		{
			WaitForElementVisible(ProviderBatchGroupingLookupButton);
			MoveToElementInPage(ProviderBatchGroupingLookupButton);

			if (ElementDisabled)
				ValidateElementDisabled(ProviderBatchGroupingLookupButton);
			else
				ValidateElementNotDisabled(ProviderBatchGroupingLookupButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateVATCodeLookupDisabled(bool ElementDisabled)
		{
			WaitForElementVisible(VATCodeLookupButton);
			MoveToElementInPage(VATCodeLookupButton);

			if (ElementDisabled)
				ValidateElementDisabled(VATCodeLookupButton);
			else
				ValidateElementNotDisabled(VATCodeLookupButton);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateStartDateFieldDisabled(bool ElementDisabled)
		{
			WaitForElementVisible(StartDate_Field);
			MoveToElementInPage(StartDate_Field);

			if (ElementDisabled)
				ValidateElementDisabled(StartDate_Field);
			else
				ValidateElementNotDisabled(StartDate_Field);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateEndDateFieldDisabled(bool ElementDisabled)
		{
			WaitForElementVisible(EndDate_Field);
			MoveToElementInPage(EndDate_Field);

			if (ElementDisabled)
				ValidateElementDisabled(EndDate_Field);
			else
				ValidateElementNotDisabled(EndDate_Field);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateAdjustedDaysFieldDisabled(bool ElementDisabled)
		{
			WaitForElementVisible(AdjustedDays_Field);
			MoveToElementInPage(AdjustedDays_Field);

			if (ElementDisabled)
				ValidateElementDisabled(AdjustedDays_Field);
			else
				ValidateElementNotDisabled(AdjustedDays_Field);

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateChargeUsingNumberOfCarersOptionsDisabled(bool OtpionDisabled)
		{
			WaitForElementVisible(ChargeUsingNumberOfCarers_Yes);
			WaitForElementVisible(ChargeUsingNumberOfCarers_No);
			MoveToElementInPage(ChargeUsingNumberOfCarers_No);

			if (OtpionDisabled)
			{
				ValidateElementDisabled(ChargeUsingNumberOfCarers_Yes);
				ValidateElementDisabled(ChargeUsingNumberOfCarers_No);
			}
			else
			{
				ValidateElementNotDisabled(ChargeUsingNumberOfCarers_Yes);
				ValidateElementNotDisabled(ChargeUsingNumberOfCarers_No);
			}

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateEndReasonRulesApplyOptionsDisabled(bool OtpionDisabled)
		{
			WaitForElement(EndReasonRulesApply_Yes);
			WaitForElement(EndReasonRulesApply_No);

			if (OtpionDisabled)
			{
				ValidateElementDisabled(EndReasonRulesApply_Yes);
				ValidateElementDisabled(EndReasonRulesApply_No);
			}
			else
			{
				ValidateElementNotDisabled(EndReasonRulesApply_Yes);
				ValidateElementNotDisabled(EndReasonRulesApply_No);
			}

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateUsedInBatchSetupOptionsDisabled(bool OtpionDisabled)
		{
			WaitForElement(UsedInBatchSetup_Yes);
			WaitForElement(UsedInBatchSetup_No);

			if (OtpionDisabled)
			{
				ValidateElementDisabled(UsedInBatchSetup_Yes);
				ValidateElementDisabled(UsedInBatchSetup_No);
			}
			else
			{
				ValidateElementNotDisabled(UsedInBatchSetup_Yes);
				ValidateElementNotDisabled(UsedInBatchSetup_No);
			}

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateVATApplyToChargingOptionsDisabled(bool OtpionDisabled)
		{
			WaitForElement(VATApplyToCharging_Yes);
			WaitForElement(VATApplyToCharging_No);

			if (OtpionDisabled)
			{
				ValidateElementDisabled(VATApplyToCharging_Yes);
				ValidateElementDisabled(VATApplyToCharging_No);
			}
			else
			{
				ValidateElementNotDisabled(VATApplyToCharging_Yes);
				ValidateElementNotDisabled(VATApplyToCharging_No);
			}

			return this;
		}

		public ServiceFinanceSettingRecordPage ValidateNotesFieldDisabled(bool ElementDisabled)
		{
			WaitForElement(Notes_Field);

			if (ElementDisabled)
				ValidateElementDisabled(Notes_Field);
			else
				ValidateElementNotDisabled(Notes_Field);

			return this;
		}

	}
}