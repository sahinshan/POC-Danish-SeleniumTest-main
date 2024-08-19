using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class RTTTreatmentFunctionRecordPage : CommonMethods
	{
		public RTTTreatmentFunctionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		readonly By iframe_RTTTreatmentFunctionFrame = By.Id("iframe_rtttreatmentfunctioncode");
		readonly By cwDialog_RTTTreatmentFunctionFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=rtttreatmentfunctioncode')]");

		readonly By ContactReasonRecordPageHeader = By.XPath("//*[@id='CWToolbar']//h1");
		readonly By PageTitle = By.XPath("//*[@id='CWToolbar']/div/h1/span");

		readonly By BackButton = By.Id("BackButton");
		readonly By SaveButton = By.Id("TI_SaveButton");
		readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
		readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
		readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
		readonly By AdditionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']//div[@id='CWToolbarMenu']/button");

		readonly By Name_LabelField = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
		readonly By Name_MandatoryField = By.XPath("//*[@id='CWLabelHolder_name']/label/span[@class='mandatory']");
		readonly By Name_Field = By.Id("CWField_name");
		readonly By Name_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");

		readonly By TreatmentFunctionCode_LabelField = By.XPath("//*[@id='CWLabelHolder_govcode']/label[text()='Treatment Function Code']");
		readonly By TreatmentFunctionCode_MandatoryField = By.XPath("//*[@id='CWLabelHolder_govcode']/label/span[@class='mandatory']");
		readonly By TreatmentFunctionCode_Field = By.Id("CWField_govcode");
		readonly By TreatmentFunctionCode_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_govcode']/label/span");

		readonly By RollupTreatmentFunctionCode_LabelField = By.XPath("//*[@id='CWLabelHolder_rolluptreatmentfunctioncode']/label[text()='Rollup Treatment Function Code']");
		readonly By RollupTreatmentFunctionCode_MandatoryField = By.XPath("//*[@id='CWLabelHolder_rolluptreatmentfunctioncode']/label/span[@class='mandatory']");
		readonly By RollupTreatmentFunctionCode_Field = By.Id("CWField_rolluptreatmentfunctioncode");

		readonly By Code_LabelField = By.XPath("//*[@id='CWLabelHolder_code']/label[text()='Code']");
		readonly By Code_MandatoryField = By.XPath("//*[@id='CWLabelHolder_code']/label/span[@class='mandatory']");
		readonly By Code_Field = By.Id("CWField_code");
		readonly By Code_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_code']/label/span");

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

		readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");


		public RTTTreatmentFunctionRecordPage WaitForRTTTreatmentFunctionRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementVisible(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElementVisible(iframe_RTTTreatmentFunctionFrame);
			SwitchToIframe(iframe_RTTTreatmentFunctionFrame);

			WaitForElementVisible(cwDialog_RTTTreatmentFunctionFrame);
			SwitchToIframe(cwDialog_RTTTreatmentFunctionFrame);

			WaitForElementNotVisible("CWRefreshPanel", 60);

			WaitForElementVisible(ContactReasonRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateAllFieldsVisible()
		{
			WaitForElementVisible(Name_LabelField);
			WaitForElementVisible(Name_Field);

			WaitForElementVisible(TreatmentFunctionCode_LabelField);
			WaitForElementVisible(TreatmentFunctionCode_Field);

			WaitForElementVisible(RollupTreatmentFunctionCode_LabelField);
			WaitForElementVisible(RollupTreatmentFunctionCode_Field);

			WaitForElementVisible(Code_LabelField);
			WaitForElementVisible(Code_Field);

			WaitForElementVisible(StartDate_LabelField);
			WaitForElementVisible(StartDate_Field);

			WaitForElementVisible(EndDate_LabelField);
			WaitForElementVisible(EndDate_Field);

			WaitForElementVisible(ResponsibleTeam_LabelField);
			WaitForElementVisible(ResponsibleTeam_LookupButton);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateRTTTreatmentFunctionRecordTitle(string RTTTreatmentFunctionTitle)
		{
			WaitForElementVisible(PageTitle);
			MoveToElementInPage(PageTitle);
			ValidateElementText(PageTitle, RTTTreatmentFunctionTitle);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			MoveToElementInPage(BackButton);
			Click(BackButton);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			MoveToElementInPage(SaveButton);
			Click(SaveButton);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			MoveToElementInPage(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			MoveToElementInPage(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			MoveToElementInPage(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateNameText(string ExpectedText)
		{
			WaitForElementVisible(Name_Field);
			MoveToElementInPage(Name_Field);
			ValidateElementValue(Name_Field, ExpectedText);

			return this;
		}

		public RTTTreatmentFunctionRecordPage InsertName(string TextToInsert)
		{
			WaitForElementToBeClickable(Name_Field);
			MoveToElementInPage(Name_Field);
			SendKeys(Name_Field, TextToInsert);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateTreatmentFunctionCodeText(string ExpectedText)
		{
			WaitForElementVisible(TreatmentFunctionCode_Field);
			MoveToElementInPage(TreatmentFunctionCode_Field);
			ValidateElementValue(TreatmentFunctionCode_Field, ExpectedText);

			return this;
		}

		public RTTTreatmentFunctionRecordPage InsertTreatmentFunctionCode(string TextToInsert)
		{
			WaitForElementToBeClickable(TreatmentFunctionCode_Field);
			MoveToElementInPage(TreatmentFunctionCode_Field);
			SendKeys(TreatmentFunctionCode_Field, TextToInsert);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateRollupTreatmentFunctionCodeText(string ExpectedText)
		{
			WaitForElementVisible(RollupTreatmentFunctionCode_Field);
			MoveToElementInPage(RollupTreatmentFunctionCode_Field);
			ValidateElementValue(RollupTreatmentFunctionCode_Field, ExpectedText);

			return this;
		}

		public RTTTreatmentFunctionRecordPage InsertRollupTreatmentFunctionCode(string TextToInsert)
		{
			WaitForElementToBeClickable(RollupTreatmentFunctionCode_Field);
			MoveToElementInPage(RollupTreatmentFunctionCode_Field);
			SendKeys(RollupTreatmentFunctionCode_Field, TextToInsert);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateCodeText(string ExpectedText)
		{
			WaitForElementVisible(Code_Field);
			MoveToElementInPage(Code_Field);
			ValidateElementValue(Code_Field, ExpectedText);

			return this;
		}

		public RTTTreatmentFunctionRecordPage InsertCode(string TextToInsert)
		{
			WaitForElementToBeClickable(Code_Field);
			MoveToElementInPage(Code_Field);
			SendKeys(Code_Field, TextToInsert);
			SendKeysWithoutClearing(Code_Field, Keys.Tab);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateStartDateText(string ExpectedText)
		{
			WaitForElementVisible(StartDate_Field);
			MoveToElementInPage(StartDate_Field);
			ValidateElementValue(StartDate_Field, ExpectedText);

			return this;
		}

		public RTTTreatmentFunctionRecordPage InsertStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate_Field);
			MoveToElementInPage(StartDate_Field);
			SendKeys(StartDate_Field, TextToInsert);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartDate_DatePicker);
			MoveToElementInPage(StartDate_DatePicker);
			Click(StartDate_DatePicker);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateEndDateText(string ExpectedText)
		{
			WaitForElementVisible(EndDate_Field);
			MoveToElementInPage(EndDate_Field);
			ValidateElementValue(EndDate_Field, ExpectedText);

			return this;
		}

		public RTTTreatmentFunctionRecordPage InsertEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate_Field);
			MoveToElementInPage(EndDate_Field);
			SendKeys(EndDate_Field, TextToInsert);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EndDate_DatePicker);
			MoveToElementInPage(EndDate_DatePicker);
			Click(EndDate_DatePicker);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ClickInactive_Option(bool YesOption)
		{
			if (YesOption)
			{
				WaitForElementToBeClickable(Inactive_YesOption);
				MoveToElementInPage(Inactive_YesOption);
				Click(Inactive_YesOption);
			}
			else
            {
				WaitForElementToBeClickable(Inactive_NoOption);
				MoveToElementInPage(Inactive_NoOption);
				Click(Inactive_NoOption);
			}

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateInactive_YesOptionIsCheckedOrNot(bool IsChecked)
		{
			WaitForElementVisible(Inactive_YesOption);
			MoveToElementInPage(Inactive_YesOption);

			if (IsChecked)
				ValidateElementChecked(Inactive_YesOption);
			else
				ValidateElementNotChecked(Inactive_YesOption);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateInactive_NoOptionIsCheckedOrNot(bool IsChecked)
		{
			WaitForElementVisible(Inactive_NoOption);
			MoveToElementInPage(Inactive_NoOption);

			if (IsChecked)
				ValidateElementChecked(Inactive_NoOption);
			else
				ValidateElementNotChecked(Inactive_NoOption);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ClickValidForExport_Option(bool YesOption)
		{
			if (YesOption)
			{
				WaitForElementToBeClickable(ValidForExport_YesOption);
				MoveToElementInPage(ValidForExport_YesOption);
				Click(ValidForExport_YesOption);
			}
			else
			{
				WaitForElementToBeClickable(ValidForExport_NoOption);
				MoveToElementInPage(ValidForExport_NoOption);
				Click(ValidForExport_NoOption);
			}

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateValidForExport_YesOptionIsCheckedOrNot(bool IsChecked)
		{
			WaitForElementVisible(ValidForExport_YesOption);
			MoveToElementInPage(ValidForExport_YesOption);

			if (IsChecked)
				ValidateElementChecked(ValidForExport_YesOption);
			else
				ValidateElementNotChecked(ValidForExport_YesOption);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateValidForExport_NoOptionIsCheckedOrNot(bool IsChecked)
		{
			WaitForElementVisible(ValidForExport_NoOption);
			MoveToElementInPage(ValidForExport_NoOption);

			if (IsChecked)
				ValidateElementChecked(ValidForExport_NoOption);
			else
				ValidateElementNotChecked(ValidForExport_NoOption);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			MoveToElementInPage(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
			MoveToElementInPage(ResponsibleTeam_LookupButton);
			Click(ResponsibleTeam_LookupButton);

			return this;
		}
				
		public RTTTreatmentFunctionRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
		{
			WaitForElement(Name_FieldErrorLabel);
			MoveToElementInPage(Name_FieldErrorLabel);
			ValidateElementText(Name_FieldErrorLabel, ExpectedText);

			return this;
		}
	
		public RTTTreatmentFunctionRecordPage ValidateNotificationAreaText(string ExpectedText)
		{
			ValidateElementText(notificationMessage, ExpectedText);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateNameMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(Name_LabelField);
			MoveToElementInPage(Name_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(Name_MandatoryField);
			else
				WaitForElementNotVisible(Name_MandatoryField, 3);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateCodeFieldErrorLabelText(string ExpectedText)
		{
			WaitForElement(Code_FieldErrorLabel);
			MoveToElementInPage(Code_FieldErrorLabel);
			ValidateElementText(Code_FieldErrorLabel, ExpectedText);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateTreatmentFunctionCodeMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(TreatmentFunctionCode_LabelField);
			MoveToElementInPage(TreatmentFunctionCode_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(TreatmentFunctionCode_MandatoryField);
			else
				WaitForElementNotVisible(TreatmentFunctionCode_MandatoryField, 3);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateTreatmentFunctionCodeFieldErrorLabelText(string ExpectedText)
		{
			WaitForElement(TreatmentFunctionCode_FieldErrorLabel);
			MoveToElementInPage(TreatmentFunctionCode_FieldErrorLabel);
			ValidateElementText(TreatmentFunctionCode_FieldErrorLabel, ExpectedText);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateRollupTreatmentFunctionCodeMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(RollupTreatmentFunctionCode_LabelField);
			MoveToElementInPage(RollupTreatmentFunctionCode_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(RollupTreatmentFunctionCode_MandatoryField);
			else
				WaitForElementNotVisible(RollupTreatmentFunctionCode_MandatoryField, 3);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateCodeMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(Code_LabelField);
			MoveToElementInPage(Code_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(Code_MandatoryField);
			else
				WaitForElementNotVisible(Code_MandatoryField, 3);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateStartDateMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(StartDate_LabelField);
			MoveToElementInPage(StartDate_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(StartDate_MandatoryField);
			else
				WaitForElementNotVisible(StartDate_MandatoryField, 3);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateEndDateMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(EndDate_LabelField);
			MoveToElementInPage(EndDate_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(EndDate_MandatoryField);
			else
				WaitForElementNotVisible(EndDate_MandatoryField, 3);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateNameFieldMaximumLimitText(string expected)
		{
			WaitForElementVisible(Name_Field);
			ValidateElementMaxLength(Name_Field, expected);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateCodeFieldIsNumeric()
		{
			WaitForElementVisible(Code_Field);
			ValidateElementAttribute(Code_Field, "validatenumeric", "ValidateNumeric");

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateStartDateDatePickerIsVisibile()
		{
			WaitForElementVisible(StartDate_DatePicker);

			return this;
		}

		public RTTTreatmentFunctionRecordPage ValidateEndDateDatePickerIsVisibile()
		{
			WaitForElementVisible(EndDate_DatePicker);

			return this;
		}

		public RTTTreatmentFunctionRecordPage WaitForRecordToBeSaved()
		{
			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);
			WaitForElementVisible(AdditionalToolbarElementsButton);

			return this;
		}

	}
}
