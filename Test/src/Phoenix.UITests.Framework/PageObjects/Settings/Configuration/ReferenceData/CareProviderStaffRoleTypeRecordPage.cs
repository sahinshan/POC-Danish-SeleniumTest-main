using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class CareProviderStaffRoleTypeRecordPage : CommonMethods
	{
		public CareProviderStaffRoleTypeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		readonly By cwDialog_CareProviderStaffRoleTypeFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderstaffroletype')]");


		readonly By ContactReasonRecordPageHeader = By.XPath("//*[@id='CWToolbar']//h1");
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

		readonly By ValidForExport_YesOption = By.Id("CWField_validforexport_1");
		readonly By ValidForExport_NoOption = By.Id("CWField_validforexport_0");

		readonly By ResponsibleTeam_LabelField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
		readonly By ResponsibleTeamLink = By.Id("CWField_ownerid_Link");
		readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

		readonly By DeliversCare_LabelField = By.XPath("//*[@id='CWLabelHolder_deliverscare']/label[text()='Delivers Care?']");
		readonly By DeliversCare_YesOption = By.Id("CWField_deliverscare_1");
		readonly By DeliversCare_NoOption = By.Id("CWField_deliverscare_0");

		readonly By RoleTypeGroups_LabelField = By.XPath("//*[@id='CWLabelHolder_careproviderstaffroletypegroups']/label[text()='Role Type Groups']");
		readonly By RoleTypeGroupsList = By.Id("CWField_careproviderstaffroletypegroups_List");
		readonly By RoleTypeGroups_LookupButton = By.Id("CWLookupBtn_careproviderstaffroletypegroups");

		readonly By Description_LabelField = By.XPath("//*[@id='CWLabelHolder_description']/label[text()='Description']");
		readonly By Description_Field = By.Id("CWField_description");

		readonly By DefaultBookingType_LabelField = By.XPath("//*[@id='CWLabelHolder_defaultbookingtypeid']/label[text()='Default Booking Type']");
		readonly By DefaultBookingType_LinkText = By.Id("CWField_defaultbookingtypeid_Link");
		readonly By DefaultBookingType_LookupButton = By.Id("CWLookupBtn_defaultbookingtypeid");

		readonly By leftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
		readonly By auditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");

		By roleTypeGroups_SelectedRecordLinkText(string RecordId) => By.XPath("//ul[@id='CWField_careproviderstaffroletypegroups_List']/li[@lkpid='" + RecordId + "']");

		readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");


		public CareProviderStaffRoleTypeRecordPage WaitForCareProviderStaffRoleTypeRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementVisible(contentIFrame);
			SwitchToIframe(contentIFrame);

			//WaitForElementVisible(iframe_CareProviderStaffRoleTypeFrame);
			//SwitchToIframe(iframe_CareProviderStaffRoleTypeFrame);

			WaitForElementVisible(cwDialog_CareProviderStaffRoleTypeFrame);
			SwitchToIframe(cwDialog_CareProviderStaffRoleTypeFrame);

			WaitForElementNotVisible("CWRefreshPanel", 60);

			WaitForElementVisible(ContactReasonRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage WaitForCareProviderStaffRoleTypeRecordPageToLoadFromAdvancedSearch()
		{
			SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElementVisible(cwDialog_CareProviderStaffRoleTypeFrame);
			SwitchToIframe(cwDialog_CareProviderStaffRoleTypeFrame);

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElementVisible(ContactReasonRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateAllFieldsVisible()
		{
			WaitForElementVisible(Name_LabelField);
			WaitForElementVisible(Name_Field);

			WaitForElementVisible(Code_LabelField);
			WaitForElementVisible(Code_Field);

			WaitForElementVisible(GovCode_LabelField);
			WaitForElementVisible(GovCode_Field);

			WaitForElementVisible(StartDate_LabelField);
			WaitForElementVisible(StartDate_Field);

			WaitForElementVisible(EndDate_LabelField);
			WaitForElementVisible(EndDate_Field);

			WaitForElementVisible(Inactive_YesOption);
			WaitForElementVisible(Inactive_NoOption);

			WaitForElementVisible(ValidForExport_YesOption);
			WaitForElementVisible(ValidForExport_NoOption);

			WaitForElementVisible(ResponsibleTeam_LabelField);
			WaitForElementVisible(ResponsibleTeam_LookupButton);

			WaitForElementVisible(DeliversCare_LabelField);
			WaitForElementVisible(DeliversCare_YesOption);
			WaitForElementVisible(DeliversCare_NoOption);

			WaitForElementVisible(RoleTypeGroups_LabelField);
			WaitForElementVisible(RoleTypeGroups_LookupButton);

			WaitForElementVisible(DefaultBookingType_LabelField);
			WaitForElementVisible(DefaultBookingType_LookupButton);

			WaitForElementVisible(Description_LabelField);
			WaitForElementVisible(Description_Field);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateCareProviderStaffRoleTypeRecordTitle(string CareProviderStaffRoleTypeRecordTitle)
		{
			WaitForElementVisible(PageTitle);
			ScrollToElement(PageTitle);
			ValidateElementText(PageTitle, CareProviderStaffRoleTypeRecordTitle);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			ScrollToElement(BackButton);
			Click(BackButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			ScrollToElement(SaveButton);
			Click(SaveButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			ScrollToElement(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			ScrollToElement(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			ScrollToElement(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickActivateButton()
		{
			WaitForElementToBeClickable(ActivateButton);
			ScrollToElement(ActivateButton);
			Click(ActivateButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateNameText(string ExpectedText)
		{
			WaitForElementVisible(Name_Field);
			ScrollToElement(Name_Field);
			ValidateElementValue(Name_Field, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage InsertName(string TextToInsert)
		{
			WaitForElementToBeClickable(Name_Field);
			ScrollToElement(Name_Field);
			SendKeys(Name_Field, TextToInsert);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateCodeText(string ExpectedText)
		{
			WaitForElementVisible(Code_Field);
			ScrollToElement(Code_Field);
			ValidateElementValue(Code_Field, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage InsertCode(string TextToInsert)
		{
			WaitForElementToBeClickable(Code_Field);
			ScrollToElement(Code_Field);
			SendKeys(Code_Field, TextToInsert);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateGovCodeText(string ExpectedText)
		{
			WaitForElementVisible(GovCode_Field);
			ScrollToElement(GovCode_Field);
			ValidateElementValue(GovCode_Field, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage InsertGovCode(string TextToInsert)
		{
			WaitForElementToBeClickable(GovCode_Field);
			ScrollToElement(GovCode_Field);
			SendKeys(GovCode_Field, TextToInsert);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateStartDateText(string ExpectedText)
		{
			WaitForElementVisible(StartDate_Field);
			ScrollToElement(StartDate_Field);
			ValidateElementValue(StartDate_Field, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage InsertStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate_Field);
			ScrollToElement(StartDate_Field);
			SendKeys(StartDate_Field, TextToInsert);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartDate_DatePicker);
			ScrollToElement(StartDate_DatePicker);
			Click(StartDate_DatePicker);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateEndDateText(string ExpectedText)
		{
			WaitForElementVisible(EndDate_Field);
			ScrollToElement(EndDate_Field);
			ValidateElementValue(EndDate_Field, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage InsertEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate_Field);
			ScrollToElement(EndDate_Field);
			SendKeys(EndDate_Field, TextToInsert);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EndDate_DatePicker);
			ScrollToElement(EndDate_DatePicker);
			Click(EndDate_DatePicker);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateDescriptionText(string ExpectedText)
		{
			WaitForElementVisible(Description_Field);
			ScrollToElement(Description_Field);
			ValidateElementValue(Description_Field, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage InsertDescription(string TextToInsert)
		{
			WaitForElementToBeClickable(Description_Field);
			ScrollToElement(Description_Field);
			SendKeys(Description_Field, TextToInsert);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickInactive_Option(bool YesOption)
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

		public CareProviderStaffRoleTypeRecordPage ValidateInactive_OptionIsCheckedOrNot(bool YesOptionIsChecked)
		{
			WaitForElementVisible(Inactive_YesOption);
			WaitForElementVisible(Inactive_NoOption);

			if (YesOptionIsChecked)
			{
				ScrollToElement(Inactive_YesOption);
				ValidateElementChecked(Inactive_YesOption);
				ValidateElementNotChecked(Inactive_NoOption);
			}
			else
			{
				ScrollToElement(Inactive_NoOption);
				ValidateElementChecked(Inactive_NoOption);
				ValidateElementNotChecked(Inactive_YesOption);
			}

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickValidForExport_Option(bool YesOption)
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

		public CareProviderStaffRoleTypeRecordPage ValidateValidForExport_OptionIsCheckedOrNot(bool YesOptionIsChecked)
		{
			WaitForElementVisible(ValidForExport_YesOption);
			WaitForElementVisible(ValidForExport_NoOption);

			if (YesOptionIsChecked)
			{
				ScrollToElement(ValidForExport_YesOption);
				ValidateElementChecked(ValidForExport_YesOption);
				ValidateElementNotChecked(ValidForExport_NoOption);
			}
			else
			{
				ScrollToElement(ValidForExport_NoOption);
				ValidateElementChecked(ValidForExport_NoOption);
				ValidateElementNotChecked(ValidForExport_YesOption);
			}

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
			ScrollToElement(ResponsibleTeam_LookupButton);
			Click(ResponsibleTeam_LookupButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool ExpectDisabled)
		{
			WaitForElementVisible(ResponsibleTeam_LookupButton);
			ScrollToElement(ResponsibleTeam_LookupButton);

			if (ExpectDisabled)
				ValidateElementDisabled(ResponsibleTeam_LookupButton);
			else
				ValidateElementNotDisabled(ResponsibleTeam_LookupButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
		{
			WaitForElement(Name_FieldErrorLabel);
			ScrollToElement(Name_FieldErrorLabel);
			ValidateElementText(Name_FieldErrorLabel, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateNotificationAreaText(string ExpectedText)
		{
			ValidateElementText(notificationMessage, ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateNameMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(Name_LabelField);
			ScrollToElement(Name_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(Name_MandatoryField);
			else
				WaitForElementNotVisible(Name_MandatoryField, 3);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateCodeMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(Code_LabelField);
			ScrollToElement(Code_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(Code_MandatoryField);
			else
				WaitForElementNotVisible(Code_MandatoryField, 3);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateStartDateMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(StartDate_LabelField);
			ScrollToElement(StartDate_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(StartDate_MandatoryField);
			else
				WaitForElementNotVisible(StartDate_MandatoryField, 3);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateEndDateMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(EndDate_LabelField);
			ScrollToElement(EndDate_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(EndDate_MandatoryField);
			else
				WaitForElementNotVisible(EndDate_MandatoryField, 3);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateNameFieldMaximumLimitText(string expected)
		{
			WaitForElementVisible(Name_Field);
			ValidateElementMaxLength(Name_Field, expected);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateCodeFieldIsNumeric()
		{
			WaitForElementVisible(Code_Field);
			ValidateElementAttribute(Code_Field, "validatenumeric", "ValidateNumeric");

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateStartDateDatePickerIsVisibile()
		{
			WaitForElementVisible(StartDate_DatePicker);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateEndDateDatePickerIsVisibile()
		{
			WaitForElementVisible(EndDate_DatePicker);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage WaitForRecordToBeSaved()
		{
			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);
			WaitForElementVisible(AdditionalToolbarElementsButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateDeliversCare_OptionIsCheckedOrNot(bool YesOptionIsChecked)
		{
			WaitForElementVisible(DeliversCare_YesOption);
			WaitForElementVisible(DeliversCare_NoOption);

			if (YesOptionIsChecked)
			{
				ScrollToElement(DeliversCare_YesOption);
				ValidateElementChecked(DeliversCare_YesOption);
				ValidateElementNotChecked(DeliversCare_NoOption);
			}
			else
			{
				ScrollToElement(DeliversCare_NoOption);
				ValidateElementChecked(DeliversCare_NoOption);
				ValidateElementNotChecked(DeliversCare_YesOption);
			}

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickRoleTypeGroupsLookupButton()
		{
			WaitForElementToBeClickable(RoleTypeGroups_LookupButton);
			ScrollToElement(RoleTypeGroups_LookupButton);
			Click(RoleTypeGroups_LookupButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ClickDefaultBookingTypeLookupButton()
		{
			WaitForElementToBeClickable(DefaultBookingType_LookupButton);
			ScrollToElement(DefaultBookingType_LookupButton);
			Click(DefaultBookingType_LookupButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateRoleTypeGroupsMultiSelectElementText(Guid RecordId, string ExpectedText)
		{
			WaitForElement(roleTypeGroups_SelectedRecordLinkText(RecordId.ToString()));
			ValidateElementTextContainsText(roleTypeGroups_SelectedRecordLinkText(RecordId.ToString()), ExpectedText);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateActivateButtonVisible()
		{
			WaitForElementNotVisible("CWRefreshPanel", 10);
			WaitForElementVisible(ActivateButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateNameFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(Name_Field);
			ScrollToElement(Name_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(Name_Field);
			else
				ValidateElementNotDisabled(Name_Field);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateCodeFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(Code_Field);
			ScrollToElement(Code_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(Code_Field);
			else
				ValidateElementNotDisabled(Code_Field);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateGovCodeFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(GovCode_Field);
			ScrollToElement(GovCode_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(GovCode_Field);
			else
				ValidateElementNotDisabled(GovCode_Field);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateStartDateFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(StartDate_Field);
			ScrollToElement(StartDate_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(StartDate_Field);
			else
				ValidateElementNotDisabled(StartDate_Field);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateEndDateFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(EndDate_Field);
			ScrollToElement(EndDate_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(EndDate_Field);
			else
				ValidateElementNotDisabled(EndDate_Field);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateInactiveOptionsIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(Inactive_YesOption);
			WaitForElementVisible(Inactive_NoOption);
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

		public CareProviderStaffRoleTypeRecordPage ValidateValidForExportOptionsIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(ValidForExport_YesOption);
			WaitForElementVisible(ValidForExport_NoOption);
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

		public CareProviderStaffRoleTypeRecordPage ValidateDeliversCareOptionsIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(DeliversCare_YesOption);
			WaitForElementVisible(DeliversCare_NoOption);
			ScrollToElement(DeliversCare_YesOption);

			if (ExpectDisabled)
			{
				ValidateElementDisabled(DeliversCare_YesOption);
				ValidateElementDisabled(DeliversCare_NoOption);
			}
			else
			{
				ValidateElementNotDisabled(DeliversCare_YesOption);
				ValidateElementNotDisabled(DeliversCare_NoOption);
			}

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateDescriptionFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(Description_Field);
			ScrollToElement(Description_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(Description_Field);
			else
				ValidateElementNotDisabled(Description_Field);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateRoleTypeGroupsLookupButtonIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(RoleTypeGroups_LookupButton);
			ScrollToElement(RoleTypeGroups_LookupButton);

			if (ExpectDisabled)
				ValidateElementDisabled(RoleTypeGroups_LookupButton);
			else
				ValidateElementNotDisabled(RoleTypeGroups_LookupButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage ValidateDefaultBookingTypeLookupButtonIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(DefaultBookingType_LookupButton);
			ScrollToElement(DefaultBookingType_LookupButton);

			if (ExpectDisabled)
				ValidateElementDisabled(DefaultBookingType_LookupButton);
			else
				ValidateElementNotDisabled(DefaultBookingType_LookupButton);

			return this;
		}

		public CareProviderStaffRoleTypeRecordPage NavigateToAuditSubPage()
		{
			WaitForElementToBeClickable(leftMenuButton);
			Click(leftMenuButton);

			WaitForElementToBeClickable(auditLink_LeftMenu);
			Click(auditLink_LeftMenu);

			return this;
		}
	}
}
