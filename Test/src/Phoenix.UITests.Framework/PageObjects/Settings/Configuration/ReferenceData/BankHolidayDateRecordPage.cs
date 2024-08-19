using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class BankHolidayDateRecordPage : CommonMethods
	{
		public BankHolidayDateRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		readonly By cwDialog_BankHolidayChargingCalendarFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpbankholidaydate')]");

		readonly By BankHolidayDateRecordPageHeader = By.XPath("//*[@id='CWToolbar']//h1");
		readonly By PageTitle = By.XPath("//*[@id='CWToolbar']/div/h1/span");

		readonly By BackButton = By.Id("BackButton");
		readonly By SaveButton = By.Id("TI_SaveButton");
		readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
		readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
		readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
		readonly By AdditionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']//div[@id='CWToolbarMenu']/button");
		readonly By ActivateButton = By.Id("TI_ActivateButton");

		readonly By ResponsibleTeam_LabelField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
		readonly By ResponsibleTeam_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span[@class='mandatory']");
		readonly By ResponsibleTeam_LinkText = By.Id("CWField_ownerid_Link");
		readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

		readonly By BankHolidayChargingCalendar_LabelField = By.XPath("//*[@id='CWLabelHolder_cpbankholidaychargingcalendarid']/label[text()='Bank Holiday Charging Calendar']");
		readonly By BankHolidayChargingCalendar_MandatoryField = By.XPath("//*[@id='CWLabelHolder_cpbankholidaychargingcalendarid']/label/span[@class='mandatory']");
		readonly By BankHolidayChargingCalendar_LinkText = By.Id("CWField_cpbankholidaychargingcalendarid_Link");
		readonly By BankHolidayChargingCalendar_LookupButton = By.Id("CWLookupBtn_cpbankholidaychargingcalendarid");

		readonly By PublicHoliday_LabelField = By.XPath("//*[@id='CWLabelHolder_bankholidayid']/label[text()='Public Holiday']");
		readonly By PublicHoliday_MandatoryField = By.XPath("//*[@id='CWLabelHolder_bankholidayid']/label/span[@class='mandatory']");
		readonly By PublicHoliday_LinkText = By.Id("CWField_bankholidayid_Link");
		readonly By PublicHoliday_LookupButton = By.Id("CWLookupBtn_bankholidayid");

		readonly By BankHolidayType_LabelField = By.XPath("//*[@id='CWLabelHolder_careproviderbankholidaytypeid']/label[text()='Bank Holiday Type']");
		readonly By BankHolidayType_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careproviderbankholidaytypeid']/label/span[@class='mandatory']");
		readonly By BankHolidayType_LinkText = By.Id("CWField_careproviderbankholidaytypeid_Link");
		readonly By BankHolidayType_LookupButton = By.Id("CWLookupBtn_careproviderbankholidaytypeid");

		readonly By UsedInFinance_LabelField = By.XPath("//*[@id='CWLabelHolder_usedinfinance']/label[text()='Used In Finance?']");
		readonly By UsedInFinance_YesOption = By.Id("CWField_usedinfinance_1");
		readonly By UsedInFinance_NoOption = By.Id("CWField_usedinfinance_0");

		readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");

		
		public BankHolidayDateRecordPage WaitForBankHolidayDateRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementVisible(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElementVisible(cwDialog_BankHolidayChargingCalendarFrame);
			SwitchToIframe(cwDialog_BankHolidayChargingCalendarFrame);

			WaitForElementNotVisible("CWRefreshPanel", 60);

			WaitForElementVisible(BankHolidayDateRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);

			return this;
		}

		public BankHolidayDateRecordPage ValidateAllFieldsVisible()
		{
			WaitForElementVisible(BankHolidayChargingCalendar_LabelField);
			WaitForElementVisible(BankHolidayChargingCalendar_LinkText);
			WaitForElementVisible(BankHolidayChargingCalendar_LookupButton);

			WaitForElementVisible(PublicHoliday_LabelField);
			WaitForElementVisible(PublicHoliday_LookupButton);

			WaitForElementVisible(BankHolidayType_LabelField);
			WaitForElementVisible(BankHolidayType_LookupButton);

			WaitForElementVisible(ResponsibleTeam_LabelField);
			WaitForElementVisible(ResponsibleTeam_LookupButton);

			WaitForElementVisible(UsedInFinance_LabelField);
			WaitForElementVisible(UsedInFinance_YesOption);
			WaitForElementVisible(UsedInFinance_NoOption);

			return this;
		}

		public BankHolidayDateRecordPage ValidateMandatoryFields()
		{
			WaitForElementVisible(ResponsibleTeam_MandatoryField);

			return this;
		}

		public BankHolidayDateRecordPage ValidateBankHolidayDateRecordPageTitle(string BankHolidayDateRecordTitle)
		{
			WaitForElementVisible(PageTitle);
			ScrollToElement(PageTitle);
			ValidateElementText(PageTitle, BankHolidayDateRecordTitle);

			return this;
		}

		public BankHolidayDateRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			ScrollToElement(BackButton);
			Click(BackButton);

			return this;
		}

		public BankHolidayDateRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			ScrollToElement(SaveButton);
			Click(SaveButton);

			return this;
		}

		public BankHolidayDateRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			ScrollToElement(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public BankHolidayDateRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			ScrollToElement(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public BankHolidayDateRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			ScrollToElement(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public BankHolidayDateRecordPage ClickActivateButton()
		{
			WaitForElementToBeClickable(ActivateButton);
			ScrollToElement(ActivateButton);
			Click(ActivateButton);

			return this;
		}

		public BankHolidayDateRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeam_LinkText);
			ScrollToElement(ResponsibleTeam_LinkText);
			Click(ResponsibleTeam_LinkText);

			return this;
		}

		public BankHolidayDateRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeam_LinkText);
			ValidateElementText(ResponsibleTeam_LinkText, ExpectedText);

			return this;
		}

		public BankHolidayDateRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
			ScrollToElement(ResponsibleTeam_LookupButton);
			Click(ResponsibleTeam_LookupButton);

			return this;
		}

		public BankHolidayDateRecordPage ClickPublicHolidayLookupButton()
		{
			WaitForElementToBeClickable(PublicHoliday_LookupButton);
			ScrollToElement(PublicHoliday_LookupButton);
			Click(PublicHoliday_LookupButton);

			return this;
		}

		public BankHolidayDateRecordPage ValidatePublicHolidayLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PublicHoliday_LinkText);
			ScrollToElement(PublicHoliday_LinkText);
			ValidateElementText(PublicHoliday_LinkText, ExpectedText);

			return this;
		}

		public BankHolidayDateRecordPage ClickBankHolidayTypeLookupButton()
		{
			WaitForElementToBeClickable(BankHolidayType_LookupButton);
			ScrollToElement(BankHolidayType_LookupButton);
			Click(BankHolidayType_LookupButton);

			return this;
		}

		public BankHolidayDateRecordPage ValidateBankHolidayTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(BankHolidayType_LinkText);
			ScrollToElement(BankHolidayType_LinkText);
			ValidateElementText(BankHolidayType_LinkText, ExpectedText);

			return this;
		}

		public BankHolidayDateRecordPage ValidateNotificationAreaText(string ExpectedText)
		{
			ValidateElementText(notificationMessage, ExpectedText);

			return this;
		}

		public BankHolidayDateRecordPage ValidateResponsibleTeamMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(ResponsibleTeam_LabelField);
			ScrollToElement(ResponsibleTeam_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(ResponsibleTeam_MandatoryField);
			else
				WaitForElementNotVisible(ResponsibleTeam_MandatoryField, 3);

			return this;
		}

		public BankHolidayDateRecordPage ValidateBankHolidayChargingCalendarMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(BankHolidayChargingCalendar_LabelField);
			ScrollToElement(BankHolidayChargingCalendar_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(BankHolidayChargingCalendar_MandatoryField);
			else
				WaitForElementNotVisible(BankHolidayChargingCalendar_MandatoryField, 3);

			return this;
		}

		public BankHolidayDateRecordPage ValidatePublicHolidayMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(PublicHoliday_LabelField);
			ScrollToElement(PublicHoliday_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(PublicHoliday_MandatoryField);
			else
				WaitForElementNotVisible(PublicHoliday_MandatoryField, 3);

			return this;
		}

		public BankHolidayDateRecordPage ValidateBankHolidayTypeMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(BankHolidayType_LabelField);
			ScrollToElement(BankHolidayType_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(BankHolidayType_MandatoryField);
			else
				WaitForElementNotVisible(BankHolidayType_MandatoryField, 3);

			return this;
		}

		public BankHolidayDateRecordPage WaitForRecordToBeSaved()
		{
			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);
			WaitForElementVisible(AdditionalToolbarElementsButton);

			return this;
		}

		public BankHolidayDateRecordPage ValidateResponsibleTeamLookupButtondDisabled(bool ExpectDisabled)
		{
			WaitForElementVisible(ResponsibleTeam_LookupButton);
			ScrollToElement(ResponsibleTeam_LookupButton);

			if (ExpectDisabled)
				ValidateElementDisabled(ResponsibleTeam_LookupButton);
			else
				ValidateElementNotDisabled(ResponsibleTeam_LookupButton);

			return this;
		}

		public BankHolidayDateRecordPage ValidateBankHolidayChargingCalendarLookupButtondDisabled(bool ExpectDisabled)
		{
			WaitForElementVisible(BankHolidayChargingCalendar_LookupButton);
			ScrollToElement(BankHolidayChargingCalendar_LookupButton);

			if (ExpectDisabled)
				ValidateElementDisabled(BankHolidayChargingCalendar_LookupButton);
			else
				ValidateElementNotDisabled(BankHolidayChargingCalendar_LookupButton);

			return this;
		}

		public BankHolidayDateRecordPage ValidateBankHolidayChargingCalendarLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(BankHolidayChargingCalendar_LinkText);
			ScrollToElement(BankHolidayChargingCalendar_LinkText);
			ValidateElementText(BankHolidayChargingCalendar_LinkText, ExpectedText);

			return this;
		}

		public BankHolidayDateRecordPage ValidateUsedInFinanceOptionsIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(UsedInFinance_YesOption);
			ScrollToElement(UsedInFinance_YesOption);

			if (ExpectDisabled)
			{
				ValidateElementDisabled(UsedInFinance_YesOption);
				ValidateElementDisabled(UsedInFinance_NoOption);
			}
			else
			{
				ValidateElementNotDisabled(UsedInFinance_YesOption);
				ValidateElementNotDisabled(UsedInFinance_NoOption);
			}

			return this;
		}

		public BankHolidayDateRecordPage ClickUsedInFinance_Option(bool YesOption)
		{
			if (YesOption)
			{
				WaitForElementToBeClickable(UsedInFinance_YesOption);
				ScrollToElement(UsedInFinance_YesOption);
				Click(UsedInFinance_YesOption);
			}
			else
			{
				WaitForElementToBeClickable(UsedInFinance_NoOption);
				ScrollToElement(UsedInFinance_NoOption);
				Click(UsedInFinance_NoOption);
			}

			return this;
		}

		public BankHolidayDateRecordPage ValidateUsedInFinance_OptionIsCheckedOrNot(bool IsYesChecked)
		{
			WaitForElementVisible(UsedInFinance_YesOption);
			WaitForElementVisible(UsedInFinance_NoOption);
			ScrollToElement(UsedInFinance_YesOption);

			if (IsYesChecked)
			{
				ValidateElementChecked(UsedInFinance_YesOption);
				ValidateElementNotChecked(UsedInFinance_NoOption);
			}
			else
			{
				ValidateElementChecked(UsedInFinance_NoOption);
				ValidateElementNotChecked(UsedInFinance_YesOption);
			}

			return this;
		}

	}
}
