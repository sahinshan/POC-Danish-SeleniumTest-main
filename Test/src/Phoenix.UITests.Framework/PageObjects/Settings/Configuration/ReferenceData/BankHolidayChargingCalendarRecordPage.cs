using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class BankHolidayChargingCalendarRecordPage : CommonMethods
	{
		public BankHolidayChargingCalendarRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		readonly By cwDialog_BankHolidayChargingCalendarFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpbankholidaychargingcalendar')]");

		readonly By BankHolidayChargingCalendarRecordPageHeader = By.XPath("//*[@id='CWToolbar']//h1");
		readonly By PageTitle = By.XPath("//*[@id='CWToolbar']/div/h1/span");

		readonly By BackButton = By.Id("BackButton");
		readonly By SaveButton = By.Id("TI_SaveButton");
		readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
		readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
		readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
		readonly By AdditionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']//div[@id='CWToolbarMenu']/button");
		readonly By ActivateButton = By.Id("TI_ActivateButton");

		readonly By BankHolidayDateTab = By.Id("CWNavGroup_CPBankHolidayDate");

		readonly By Name_LabelField = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
		readonly By Name_MandatoryField = By.XPath("//*[@id='CWLabelHolder_name']/label/span[@class='mandatory']");
		readonly By Name_Field = By.Id("CWField_name");
		readonly By Name_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");

		readonly By Code_LabelField = By.XPath("//*[@id='CWLabelHolder_code']/label[text()='Code']");
		readonly By Code_Field = By.Id("CWField_code");

		readonly By GovCode_LabelField = By.XPath("//*[@id='CWLabelHolder_govcode']/label[text()='Gov Code']");
		readonly By GovCode_Field = By.Id("CWField_govcode");

		readonly By ResponsibleTeam_LabelField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
		readonly By ResponsibleTeam_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span[@class='mandatory']");
		readonly By ResponsibleTeamLink = By.Id("CWField_ownerid_Link");
		readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

		readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");

		readonly By leftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
		readonly By auditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");


		public BankHolidayChargingCalendarRecordPage WaitForBankHolidayChargingCalendarRecordPageToLoad()
		{
			SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(contentIFrame);
			SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(cwDialog_BankHolidayChargingCalendarFrame);
			SwitchToIframe(cwDialog_BankHolidayChargingCalendarFrame);

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElementVisible(BankHolidayChargingCalendarRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateAllFieldsVisible()
		{
			WaitForElementVisible(Name_LabelField);
			WaitForElementVisible(Name_Field);

			WaitForElementVisible(Code_LabelField);
			WaitForElementVisible(Code_Field);

			WaitForElementVisible(GovCode_LabelField);
			WaitForElementVisible(GovCode_Field);

			WaitForElementVisible(ResponsibleTeam_LabelField);
			WaitForElementVisible(ResponsibleTeam_LookupButton);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateMandatoryFields()
		{
			WaitForElementVisible(Name_MandatoryField);
			WaitForElementVisible(ResponsibleTeam_MandatoryField);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateBankHolidayChargingCalendarRecordPageTitle(string BankHolidayChargingCalendarRecordTitle)
		{
			WaitForElementVisible(PageTitle);
			MoveToElementInPage(PageTitle);
			ValidateElementText(PageTitle, BankHolidayChargingCalendarRecordTitle);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			MoveToElementInPage(BackButton);
			Click(BackButton);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			MoveToElementInPage(SaveButton);
			Click(SaveButton);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			MoveToElementInPage(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			MoveToElementInPage(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			MoveToElementInPage(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ClickActivateButton()
		{
			WaitForElementToBeClickable(ActivateButton);
			MoveToElementInPage(ActivateButton);
			Click(ActivateButton);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ClickBankHolidayDatesTab()
		{
			WaitForElementToBeClickable(BankHolidayDateTab);
			MoveToElementInPage(BankHolidayDateTab);
			Click(BankHolidayDateTab);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateNameText(string ExpectedText)
		{
			WaitForElementVisible(Name_Field);
			MoveToElementInPage(Name_Field);
			ValidateElementValue(Name_Field, ExpectedText);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage InsertName(string TextToInsert)
		{
			WaitForElementToBeClickable(Name_Field);
			MoveToElementInPage(Name_Field);
			SendKeys(Name_Field, TextToInsert);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateCodeText(string ExpectedText)
		{
			WaitForElementVisible(Code_Field);
			MoveToElementInPage(Code_Field);
			ValidateElementValue(Code_Field, ExpectedText);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage InsertCode(string TextToInsert)
		{
			WaitForElementToBeClickable(Code_Field);
			MoveToElementInPage(Code_Field);
			SendKeys(Code_Field, TextToInsert);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateGovCodeText(string ExpectedText)
		{
			WaitForElementVisible(GovCode_Field);
			MoveToElementInPage(GovCode_Field);
			ValidateElementValue(GovCode_Field, ExpectedText);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage InsertGovCode(string TextToInsert)
		{
			WaitForElementToBeClickable(GovCode_Field);
			MoveToElementInPage(GovCode_Field);
			SendKeys(GovCode_Field, TextToInsert);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			MoveToElementInPage(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
			MoveToElementInPage(ResponsibleTeam_LookupButton);
			Click(ResponsibleTeam_LookupButton);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
		{
			WaitForElement(Name_FieldErrorLabel);
			MoveToElementInPage(Name_FieldErrorLabel);
			ValidateElementText(Name_FieldErrorLabel, ExpectedText);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateNotificationAreaText(string ExpectedText)
		{
			ValidateElementText(notificationMessage, ExpectedText);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateNameMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(Name_LabelField);
			MoveToElementInPage(Name_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(Name_MandatoryField);
			else
				WaitForElementNotVisible(Name_MandatoryField, 3);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateResponsibleTeamMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(ResponsibleTeam_LabelField);
			MoveToElementInPage(ResponsibleTeam_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(ResponsibleTeam_MandatoryField);
			else
				WaitForElementNotVisible(ResponsibleTeam_MandatoryField, 3);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage WaitForRecordToBeSaved()
		{
			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);
			WaitForElementVisible(AdditionalToolbarElementsButton);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateResponsibleTeamLookupButtondDisabled(bool ExpectDisabled)
		{
			WaitForElementVisible(ResponsibleTeam_LookupButton);
			MoveToElementInPage(ResponsibleTeam_LookupButton);

			if (ExpectDisabled)
				ValidateElementDisabled(ResponsibleTeam_LookupButton);
			else
				ValidateElementNotDisabled(ResponsibleTeam_LookupButton);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateNameFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(Name_Field);
			MoveToElementInPage(Name_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(Name_Field);
			else
				ValidateElementNotDisabled(Name_Field);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateCodeFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(Code_Field);
			MoveToElementInPage(Code_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(Code_Field);
			else
				ValidateElementNotDisabled(Code_Field);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage ValidateGovCodeFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(GovCode_Field);
			MoveToElementInPage(GovCode_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(GovCode_Field);
			else
				ValidateElementNotDisabled(GovCode_Field);

			return this;
		}

		public BankHolidayChargingCalendarRecordPage NavigateToAuditSubPage()
		{
			WaitForElementToBeClickable(leftMenuButton);
			Click(leftMenuButton);

			WaitForElementToBeClickable(auditLink_LeftMenu);
			Click(auditLink_LeftMenu);

			return this;
		}

	}
}
