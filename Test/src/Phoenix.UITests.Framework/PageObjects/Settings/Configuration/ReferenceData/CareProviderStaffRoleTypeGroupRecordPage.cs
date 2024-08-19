using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class CareProviderStaffRoleTypeGroupRecordPage : CommonMethods
	{
		public CareProviderStaffRoleTypeGroupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		//readonly By iframe_CareProviderStaffRoleTypeGroupFrame = By.Id("iframe_careproviderstaffroletypegroup");
		readonly By cwDialog_CareProviderStaffRoleTypeGroupFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderstaffroletypegroup')]");

		readonly By CareProviderStaffRoleTypeGroupRecordPageHeader = By.XPath("//*[@id='CWToolbar']//h1");
		readonly By PageTitle = By.XPath("//*[@id='CWToolbar']/div/h1/span");

		readonly By BackButton = By.Id("BackButton");
		readonly By SaveButton = By.Id("TI_SaveButton");
		readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
		readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
		readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
		readonly By AdditionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']//div[@id='CWToolbarMenu']/button");
		readonly By ActivateButton = By.Id("TI_ActivateButton");

		readonly By Name_LabelField = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
		readonly By Name_MandatoryField = By.XPath("//*[@id='CWLabelHolder_name']/label/span[@class='mandatory']");
		readonly By Name_Field = By.Id("CWField_name");
		readonly By Name_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");

		readonly By Code_LabelField = By.XPath("//*[@id='CWLabelHolder_code']/label[text()='Code']");
		readonly By Code_MandatoryField = By.XPath("//*[@id='CWLabelHolder_code']/label/span[@class='mandatory']");
		readonly By Code_Field = By.Id("CWField_code");
		readonly By Code_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_code']/label/span");

		readonly By GovCode_LabelField = By.XPath("//*[@id='CWLabelHolder_govcode']/label[text()='Gov Code']");
		readonly By GovCode_MandatoryField = By.XPath("//*[@id='CWLabelHolder_govcode']/label/span[@class='mandatory']");
		readonly By GovCode_Field = By.Id("CWField_govcode");

		readonly By Inactive_LabelField = By.XPath("//*[@id='CWLabelHolder_inactive']/label[text()='Inactive']");
		readonly By Inactive_YesOption = By.Id("CWField_inactive_1");
		readonly By Inactive_NoOption = By.Id("CWField_inactive_0");

		readonly By StartDate_LabelField = By.XPath("//*[@id='CWLabelHolder_startdate']/label[text()='Start Date']");
		readonly By StartDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_startdate']/label/span[@class='mandatory']");
		readonly By StartDate_Field = By.Id("CWField_startdate");
		readonly By StartDate_DatePicker = By.Id("CWField_startdate_DatePicker");

		readonly By EndDate_LabelField = By.XPath("//*[@id='CWLabelHolder_enddate']/label[text()='End Date']");
		readonly By EndDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_enddate']/label/span[@class='mandatory']");
		readonly By EndDate_Field = By.Id("CWField_enddate");
		readonly By EndDate_DatePicker = By.Id("CWField_enddate_DatePicker");

		readonly By ValidForExport_LabelField = By.XPath("//*[@id='CWLabelHolder_validforexport']/label[text()='Valid For Export']");
		readonly By ValidForExport_YesOption = By.Id("CWField_validforexport_1");
		readonly By ValidForExport_NoOption = By.Id("CWField_validforexport_0");

		readonly By ResponsibleTeam_LabelField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
		readonly By ResponsibleTeamLink = By.Id("CWField_ownerid_Link");
		readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

		readonly By Description_LabelField = By.XPath("//*[@id='CWLabelHolder_description']/label[text()='Description']");
		readonly By Description_Field = By.Id("CWField_description");

		readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");

		readonly By leftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
		readonly By auditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");


		public CareProviderStaffRoleTypeGroupRecordPage WaitForCareProviderStaffRoleTypeGroupRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementVisible(contentIFrame);
			SwitchToIframe(contentIFrame);

			//WaitForElementVisible(iframe_CareProviderStaffRoleTypeGroupFrame);
			//SwitchToIframe(iframe_CareProviderStaffRoleTypeGroupFrame);

			WaitForElementVisible(cwDialog_CareProviderStaffRoleTypeGroupFrame);
			SwitchToIframe(cwDialog_CareProviderStaffRoleTypeGroupFrame);

			WaitForElementNotVisible("CWRefreshPanel", 60);

			WaitForElementVisible(CareProviderStaffRoleTypeGroupRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateAllFieldsVisible()
		{
			WaitForElementVisible(Name_LabelField);
			WaitForElementVisible(Name_Field);

			WaitForElementVisible(Code_LabelField);
			WaitForElementVisible(Code_Field);

			WaitForElementVisible(GovCode_LabelField);
			WaitForElementVisible(GovCode_Field);

			WaitForElementVisible(Description_LabelField);
			WaitForElementVisible(Description_Field);

			WaitForElementVisible(StartDate_LabelField);
			WaitForElementVisible(StartDate_Field);

			WaitForElementVisible(EndDate_LabelField);
			WaitForElementVisible(EndDate_Field);

			WaitForElementVisible(Inactive_LabelField);
			WaitForElementVisible(ValidForExport_LabelField);

			WaitForElementVisible(ResponsibleTeam_LabelField);
			WaitForElementVisible(ResponsibleTeam_LookupButton);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateCareProviderStaffRoleTypeGroupRecordTitle(string CareProviderStaffRoleTypeGroupRecordTitle)
		{
			WaitForElementVisible(PageTitle);
			ScrollToElement(PageTitle);
			ValidateElementText(PageTitle, CareProviderStaffRoleTypeGroupRecordTitle);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			ScrollToElement(BackButton);
			Click(BackButton);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			ScrollToElement(SaveButton);
			Click(SaveButton);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			ScrollToElement(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			ScrollToElement(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			ScrollToElement(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ClickActivateButton()
		{
			WaitForElementToBeClickable(ActivateButton);
			ScrollToElement(ActivateButton);
			Click(ActivateButton);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateNameText(string ExpectedText)
		{
			WaitForElementVisible(Name_Field);
			ScrollToElement(Name_Field);
			ValidateElementValue(Name_Field, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage InsertName(string TextToInsert)
		{
			WaitForElementToBeClickable(Name_Field);
			ScrollToElement(Name_Field);
			SendKeys(Name_Field, TextToInsert);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateCodeText(string ExpectedText)
		{
			WaitForElementVisible(Code_Field);
			ScrollToElement(Code_Field);
			ValidateElementValue(Code_Field, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage InsertCode(string TextToInsert)
		{
			WaitForElementToBeClickable(Code_Field);
			ScrollToElement(Code_Field);
			SendKeys(Code_Field, TextToInsert);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateGovCodeText(string ExpectedText)
		{
			WaitForElementVisible(GovCode_Field);
			ScrollToElement(GovCode_Field);
			ValidateElementValue(GovCode_Field, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage InsertGovCode(string TextToInsert)
		{
			WaitForElementToBeClickable(GovCode_Field);
			ScrollToElement(GovCode_Field);
			SendKeys(GovCode_Field, TextToInsert);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateDescriptionText(string ExpectedText)
		{
			WaitForElementVisible(Description_Field);
			ScrollToElement(Description_Field);
			ValidateElementValue(Description_Field, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage InsertDescription(string TextToInsert)
		{
			WaitForElementToBeClickable(Description_Field);
			ScrollToElement(Description_Field);
			SendKeys(Description_Field, TextToInsert);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateStartDateText(string ExpectedText)
		{
			WaitForElementVisible(StartDate_Field);
			ScrollToElement(StartDate_Field);
			ValidateElementValue(StartDate_Field, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage InsertStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate_Field);
			ScrollToElement(StartDate_Field);
			SendKeys(StartDate_Field, TextToInsert);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartDate_DatePicker);
			ScrollToElement(StartDate_DatePicker);
			Click(StartDate_DatePicker);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateEndDateText(string ExpectedText)
		{
			WaitForElementVisible(EndDate_Field);
			ScrollToElement(EndDate_Field);
			ValidateElementValue(EndDate_Field, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage InsertEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate_Field);
			ScrollToElement(EndDate_Field);
			SendKeys(EndDate_Field, TextToInsert);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EndDate_DatePicker);
			ScrollToElement(EndDate_DatePicker);
			Click(EndDate_DatePicker);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ClickInactive_Option(bool YesOption)
		{
			if (YesOption)
			{
				WaitForElementToBeClickable(Inactive_YesOption);
				ScrollToElement(Inactive_YesOption);
				Click(Inactive_YesOption);
			}
			else
			{
				WaitForElementToBeClickable(Inactive_NoOption);
				ScrollToElement(Inactive_NoOption);
				Click(Inactive_NoOption);
			}

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateInactive_YesOptionIsCheckedOrNot(bool IsChecked)
		{
			WaitForElementVisible(Inactive_YesOption);
			ScrollToElement(Inactive_YesOption);

			if (IsChecked)
				ValidateElementChecked(Inactive_YesOption);
			else
				ValidateElementNotChecked(Inactive_YesOption);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateInactive_NoOptionIsCheckedOrNot(bool IsChecked)
		{
			WaitForElementVisible(Inactive_NoOption);
			ScrollToElement(Inactive_NoOption);

			if (IsChecked)
				ValidateElementChecked(Inactive_NoOption);
			else
				ValidateElementNotChecked(Inactive_NoOption);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ClickValidForExport_Option(bool YesOption)
		{
			if (YesOption)
			{
				WaitForElementToBeClickable(ValidForExport_YesOption);
				ScrollToElement(ValidForExport_YesOption);
				Click(ValidForExport_YesOption);
			}
			else
			{
				WaitForElementToBeClickable(ValidForExport_NoOption);
				ScrollToElement(ValidForExport_NoOption);
				Click(ValidForExport_NoOption);
			}

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateValidForExport_YesOptionIsCheckedOrNot(bool IsChecked)
		{
			WaitForElementVisible(ValidForExport_YesOption);
			ScrollToElement(ValidForExport_YesOption);

			if (IsChecked)
				ValidateElementChecked(ValidForExport_YesOption);
			else
				ValidateElementNotChecked(ValidForExport_YesOption);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateValidForExport_NoOptionIsCheckedOrNot(bool IsChecked)
		{
			WaitForElementVisible(ValidForExport_NoOption);
			ScrollToElement(ValidForExport_NoOption);

			if (IsChecked)
				ValidateElementChecked(ValidForExport_NoOption);
			else
				ValidateElementNotChecked(ValidForExport_NoOption);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
			ScrollToElement(ResponsibleTeam_LookupButton);
			Click(ResponsibleTeam_LookupButton);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
		{
			WaitForElement(Name_FieldErrorLabel);
			ScrollToElement(Name_FieldErrorLabel);
			ValidateElementText(Name_FieldErrorLabel, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateNotificationAreaText(string ExpectedText)
		{
			ValidateElementText(notificationMessage, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateNameMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(Name_LabelField);
			ScrollToElement(Name_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(Name_MandatoryField);
			else
				WaitForElementNotVisible(Name_MandatoryField, 3);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateCodeMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(Code_LabelField);
			ScrollToElement(Code_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(Code_MandatoryField);
			else
				WaitForElementNotVisible(Code_MandatoryField, 3);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateStartDateMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(StartDate_LabelField);
			ScrollToElement(StartDate_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(StartDate_MandatoryField);
			else
				WaitForElementNotVisible(StartDate_MandatoryField, 3);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateEndDateMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(EndDate_LabelField);
			ScrollToElement(EndDate_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(EndDate_MandatoryField);
			else
				WaitForElementNotVisible(EndDate_MandatoryField, 3);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateNameFieldMaximumLimitText(string expected)
		{
			WaitForElementVisible(Name_Field);
			ValidateElementMaxLength(Name_Field, expected);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateCodeFieldIsNumeric()
		{
			WaitForElementVisible(Code_Field);
			ValidateElementAttribute(Code_Field, "validatenumeric", "ValidateNumeric");

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateStartDateDatePickerIsVisibile()
		{
			WaitForElementVisible(StartDate_DatePicker);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateEndDateDatePickerIsVisibile()
		{
			WaitForElementVisible(EndDate_DatePicker);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage WaitForRecordToBeSaved()
		{
			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);
			WaitForElementVisible(AdditionalToolbarElementsButton);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateResponsibleTeamLookupButtondDisabled(bool ExpectDisabled)
		{
			WaitForElementVisible(ResponsibleTeam_LookupButton);
			ScrollToElement(ResponsibleTeam_LookupButton);

			if (ExpectDisabled)
				ValidateElementDisabled(ResponsibleTeam_LookupButton);
			else
				ValidateElementNotDisabled(ResponsibleTeam_LookupButton);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateNameFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(Name_Field);
			ScrollToElement(Name_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(Name_Field);
			else
				ValidateElementNotDisabled(Name_Field);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateCodeFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(Code_Field);
			ScrollToElement(Code_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(Code_Field);
			else
				ValidateElementNotDisabled(Code_Field);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateGovCodeFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(GovCode_Field);
			ScrollToElement(GovCode_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(GovCode_Field);
			else
				ValidateElementNotDisabled(GovCode_Field);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateDescriptionFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(Description_Field);
			ScrollToElement(Description_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(Description_Field);
			else
				ValidateElementNotDisabled(Description_Field);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateStartDateFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(StartDate_Field);
			ScrollToElement(StartDate_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(StartDate_Field);
			else
				ValidateElementNotDisabled(StartDate_Field);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateEndDateFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(EndDate_Field);
			ScrollToElement(EndDate_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(EndDate_Field);
			else
				ValidateElementNotDisabled(EndDate_Field);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateInactiveOptionsIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(Inactive_YesOption);
			ScrollToElement(Inactive_YesOption);

			if (ExpectDisabled)
			{
				ValidateElementDisabled(Inactive_YesOption);
				ValidateElementDisabled(Inactive_NoOption);
			}
			else
			{
				ValidateElementNotDisabled(Inactive_YesOption);
				ValidateElementNotDisabled(Inactive_NoOption);
			}

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateValidForExportOptionsIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(ValidForExport_YesOption);
			ScrollToElement(ValidForExport_YesOption);

			if (ExpectDisabled)
			{
				ValidateElementDisabled(ValidForExport_YesOption);
				ValidateElementDisabled(ValidForExport_NoOption);
			}
			else
			{
				ValidateElementNotDisabled(ValidForExport_YesOption);
				ValidateElementNotDisabled(ValidForExport_NoOption);
			}

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage ValidateActivateButtonVisible()
		{
			WaitForElementNotVisible("CWRefreshPanel", 10);
			WaitForElementVisible(ActivateButton);

			return this;
		}

		public CareProviderStaffRoleTypeGroupRecordPage NavigateToAuditSubPage()
		{
			WaitForElementToBeClickable(leftMenuButton);
			Click(leftMenuButton);

			WaitForElementToBeClickable(auditLink_LeftMenu);
			Click(auditLink_LeftMenu);

			return this;
		}

	}
}
