using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class RateUnitRecordPage : CommonMethods
	{
		public RateUnitRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		//readonly By iframe_RateUnitFrame = By.Id("iframe_careproviderrateunit");
		readonly By cwDialog_RateUnitFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderrateunit')]");

		readonly By RateUnitRecordPageHeader = By.XPath("//*[@id='CWToolbar']//h1");
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
		readonly By ResponsibleTeam_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span[@class='mandatory']");
		readonly By ResponsibleTeamLink = By.Id("CWField_ownerid_Link");
		readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

		readonly By TimeAndDays_LabelField = By.XPath("//*[@id='CWLabelHolder_timeanddays']/label[text()='Time and Days?']");
		readonly By TimeAndDays_MandatoryField = By.XPath("//*[@id='CWLabelHolder_timeanddays']/label/span[@class='mandatory']");
		readonly By TimeAndDays_YesOption = By.Id("CWField_timeanddays_1");
		readonly By TimeAndDays_NoOption = By.Id("CWField_timeanddays_0");

		readonly By OneOff_LabelField = By.XPath("//*[@id='CWLabelHolder_oneoff']/label[text()='One-Off?']");
		readonly By OneOff_MandatoryField = By.XPath("//*[@id='CWLabelHolder_oneoff']/label/span[@class='mandatory']");
		readonly By OneOff_YesOption = By.Id("CWField_oneoff_1");
		readonly By OneOff_NoOption = By.Id("CWField_oneoff_0");

		readonly By BandedRates_LabelField = By.XPath("//*[@id='CWLabelHolder_bandedrates']/label[text()='Banded Rates?']");
		readonly By BandedRates_MandatoryField = By.XPath("//*[@id='CWLabelHolder_bandedrates']/label/span[@class='mandatory']");
		readonly By BandedRates_YesOption = By.Id("CWField_bandedrates_1");
		readonly By BandedRates_NoOption = By.Id("CWField_bandedrates_0");

		readonly By DecimalPlaces_LabelField = By.XPath("//*[@id='CWLabelHolder_decimalplaces']/label[text()='Decimal Places?']");
		readonly By DecimalPlaces_MandatoryField = By.XPath("//*[@id='CWLabelHolder_decimalplaces']/label/span[@class='mandatory']");
		readonly By DecimalPlaces_YesOption = By.Id("CWField_decimalplaces_1");
		readonly By DecimalPlaces_NoOption = By.Id("CWField_decimalplaces_0");

		readonly By MinimumAllowed_LabelField = By.XPath("//*[@id='CWLabelHolder_minimumallowed']/label[text()='Minimum Allowed']");
		readonly By MinimumAllowed_MandatoryField = By.XPath("//*[@id='CWLabelHolder_minimumallowed']/label/span[@class='mandatory']");
		readonly By MinimumAllowed_Field = By.Id("CWField_minimumallowed");
		readonly By MinimumAllowed_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_minimumallowed']/label/span");

		readonly By MaximumAllowed_LabelField = By.XPath("//*[@id='CWLabelHolder_maximumallowed']/label[text()='Maximum Allowed']");
		readonly By MaximumAllowed_MandatoryField = By.XPath("//*[@id='CWLabelHolder_maximumallowed']/label/span[@class='mandatory']");
		readonly By MaximumAllowed_Field = By.Id("CWField_maximumallowed");
		readonly By MaximumAllowed_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_maximumallowed']/label/span");

		readonly By UnitDivisor_LabelField = By.XPath("//*[@id='CWLabelHolder_unitdivisor']/label[text()='Unit Divisor']");
		readonly By UnitDivisor_MandatoryField = By.XPath("//*[@id='CWLabelHolder_unitdivisor']/label/span[@class='mandatory']");
		readonly By UnitDivisor_Field = By.Id("CWField_unitdivisor");
		readonly By UnitDivisor_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_unitdivisor']/label/span");

		readonly By NoteText_LabelField = By.XPath("//*[@id='CWLabelHolder_notetext']/label[text()='Note Text']");
		readonly By NoteText_Field = By.Id("CWField_notetext");

		readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");

		readonly By leftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
		readonly By auditLink_LeftMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");


		public RateUnitRecordPage WaitForRateUnitRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementVisible(contentIFrame);
			SwitchToIframe(contentIFrame);

			//WaitForElementVisible(iframe_RateUnitFrame);
			//SwitchToIframe(iframe_RateUnitFrame);

			WaitForElementVisible(cwDialog_RateUnitFrame);
			SwitchToIframe(cwDialog_RateUnitFrame);

			WaitForElementNotVisible("CWRefreshPanel", 60);

			WaitForElementVisible(RateUnitRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);

			return this;
		}

		public RateUnitRecordPage ValidateAllFieldsVisible()
		{
			WaitForElementVisible(Name_LabelField);
			WaitForElementVisible(Name_Field);

			WaitForElementVisible(Code_LabelField);
			WaitForElementVisible(Code_Field);

			WaitForElementVisible(GovCode_LabelField);
			WaitForElementVisible(GovCode_Field);

			WaitForElementVisible(ResponsibleTeam_LabelField);
			WaitForElementVisible(ResponsibleTeam_LookupButton);

			WaitForElementVisible(StartDate_LabelField);
			WaitForElementVisible(StartDate_Field);

			WaitForElementVisible(EndDate_LabelField);
			WaitForElementVisible(EndDate_Field);

			WaitForElementVisible(Inactive_LabelField);
			WaitForElementVisible(Inactive_YesOption);
			WaitForElementVisible(Inactive_NoOption);

			WaitForElementVisible(ValidForExport_LabelField);
			WaitForElementVisible(ValidForExport_YesOption);
			WaitForElementVisible(ValidForExport_NoOption);

			WaitForElementVisible(TimeAndDays_LabelField);
			WaitForElementVisible(TimeAndDays_YesOption);
			WaitForElementVisible(TimeAndDays_NoOption);

			WaitForElementVisible(OneOff_LabelField);
			WaitForElementVisible(OneOff_YesOption);
			WaitForElementVisible(OneOff_NoOption);

			WaitForElementVisible(NoteText_LabelField);
			WaitForElementVisible(NoteText_Field);

			return this;
		}

		public RateUnitRecordPage ValidateMandatoryFields()
		{
			WaitForElementVisible(Name_MandatoryField);
			WaitForElementVisible(Code_MandatoryField);

			WaitForElementVisible(ResponsibleTeam_MandatoryField);
			WaitForElementVisible(StartDate_MandatoryField);

			WaitForElementVisible(TimeAndDays_MandatoryField);
			WaitForElementVisible(OneOff_MandatoryField);

			return this;
		}

		public RateUnitRecordPage ValidateRateUnitRecordPageTitle(string RateUnitRecordTitle)
		{
			WaitForElementVisible(PageTitle);
			ScrollToElement(PageTitle);
			ValidateElementText(PageTitle, RateUnitRecordTitle);

			return this;
		}

		public RateUnitRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			ScrollToElement(BackButton);
			Click(BackButton);

			return this;
		}

		public RateUnitRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			ScrollToElement(SaveButton);
			Click(SaveButton);

			return this;
		}

		public RateUnitRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			ScrollToElement(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public RateUnitRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			ScrollToElement(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public RateUnitRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			ScrollToElement(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public RateUnitRecordPage ClickActivateButton()
		{
			WaitForElementToBeClickable(ActivateButton);
			ScrollToElement(ActivateButton);
			Click(ActivateButton);

			return this;
		}

		public RateUnitRecordPage ValidateNameText(string ExpectedText)
		{
			WaitForElementVisible(Name_Field);
			ScrollToElement(Name_Field);
			ValidateElementValue(Name_Field, ExpectedText);

			return this;
		}

		public RateUnitRecordPage InsertName(string TextToInsert)
		{
			WaitForElementToBeClickable(Name_Field);
			ScrollToElement(Name_Field);
			SendKeys(Name_Field, TextToInsert);

			return this;
		}

		public RateUnitRecordPage ValidateCodeText(string ExpectedText)
		{
			WaitForElementVisible(Code_Field);
			ScrollToElement(Code_Field);
			ValidateElementValue(Code_Field, ExpectedText);

			return this;
		}

		public RateUnitRecordPage InsertCode(string TextToInsert)
		{
			WaitForElementToBeClickable(Code_Field);
			ScrollToElement(Code_Field);
			SendKeys(Code_Field, TextToInsert);

			return this;
		}

		public RateUnitRecordPage ValidateGovCodeText(string ExpectedText)
		{
			WaitForElementVisible(GovCode_Field);
			ScrollToElement(GovCode_Field);
			ValidateElementValue(GovCode_Field, ExpectedText);

			return this;
		}

		public RateUnitRecordPage InsertGovCode(string TextToInsert)
		{
			WaitForElementToBeClickable(GovCode_Field);
			ScrollToElement(GovCode_Field);
			SendKeys(GovCode_Field, TextToInsert);

			return this;
		}

		public RateUnitRecordPage ValidateNoteText(string ExpectedText)
		{
			WaitForElementVisible(NoteText_Field);
			ScrollToElement(NoteText_Field);
			ValidateElementValue(NoteText_Field, ExpectedText);

			return this;
		}

		public RateUnitRecordPage InsertNoteText(string TextToInsert)
		{
			WaitForElementToBeClickable(NoteText_Field);
			ScrollToElement(NoteText_Field);
			SendKeys(NoteText_Field, TextToInsert);

			return this;
		}

		public RateUnitRecordPage ValidateStartDateText(string ExpectedText)
		{
			WaitForElementVisible(StartDate_Field);
			ScrollToElement(StartDate_Field);
			ValidateElementValue(StartDate_Field, ExpectedText);

			return this;
		}

		public RateUnitRecordPage InsertStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate_Field);
			ScrollToElement(StartDate_Field);
			SendKeys(StartDate_Field, TextToInsert);

			return this;
		}

		public RateUnitRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartDate_DatePicker);
			ScrollToElement(StartDate_DatePicker);
			Click(StartDate_DatePicker);

			return this;
		}

		public RateUnitRecordPage ValidateEndDateText(string ExpectedText)
		{
			WaitForElementVisible(EndDate_Field);
			ScrollToElement(EndDate_Field);
			ValidateElementValue(EndDate_Field, ExpectedText);

			return this;
		}

		public RateUnitRecordPage InsertEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate_Field);
			ScrollToElement(EndDate_Field);
			SendKeys(EndDate_Field, TextToInsert);

			return this;
		}

		public RateUnitRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EndDate_DatePicker);
			ScrollToElement(EndDate_DatePicker);
			Click(EndDate_DatePicker);

			return this;
		}

		public RateUnitRecordPage ClickInactive_Option(bool YesOption)
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

		public RateUnitRecordPage ValidateInactive_OptionIsCheckedOrNot(bool IsYesChecked)
		{
			WaitForElementVisible(Inactive_YesOption);
			WaitForElementVisible(Inactive_NoOption);
			ScrollToElement(Inactive_YesOption);

			if (IsYesChecked)
			{
				ValidateElementChecked(Inactive_YesOption);
				ValidateElementNotChecked(Inactive_NoOption);
			}
			else
			{
				ValidateElementChecked(Inactive_NoOption);
				ValidateElementNotChecked(Inactive_YesOption);
			}

			return this;
		}

		public RateUnitRecordPage ClickValidForExport_Option(bool YesOption)
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

		public RateUnitRecordPage ValidateValidForExport_OptionIsCheckedOrNot(bool IsYesChecked)
		{
			WaitForElementVisible(ValidForExport_YesOption);
			WaitForElementVisible(ValidForExport_NoOption);
			ScrollToElement(ValidForExport_YesOption);

			if (IsYesChecked)
			{
				ValidateElementChecked(ValidForExport_YesOption);
				ValidateElementNotChecked(ValidForExport_NoOption);
			}
			else
			{
				ValidateElementChecked(ValidForExport_NoOption);
				ValidateElementNotChecked(ValidForExport_YesOption);
			}

			return this;
		}

		public RateUnitRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public RateUnitRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public RateUnitRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
			ScrollToElement(ResponsibleTeam_LookupButton);
			Click(ResponsibleTeam_LookupButton);

			return this;
		}

		public RateUnitRecordPage ValidateMinimumAllowedText(string ExpectedText)
		{
			WaitForElementVisible(MinimumAllowed_Field);
			ScrollToElement(MinimumAllowed_Field);
			ValidateElementValue(MinimumAllowed_Field, ExpectedText);

			return this;
		}

		public RateUnitRecordPage InsertMinimumAllowedText(string TextToInsert)
		{
			WaitForElementToBeClickable(MinimumAllowed_Field);
			ScrollToElement(MinimumAllowed_Field);
			SendKeys(MinimumAllowed_Field, TextToInsert);
			SendKeysWithoutClearing(MinimumAllowed_Field, Keys.Tab);

			return this;
		}

		public RateUnitRecordPage ValidateMinimumAllowedFieldErrorLabelText(string ExpectedText)
		{
			WaitForElement(MinimumAllowed_FieldErrorLabel);
			ScrollToElement(MinimumAllowed_FieldErrorLabel);
			ValidateElementText(MinimumAllowed_FieldErrorLabel, ExpectedText);

			return this;
		}

		public RateUnitRecordPage ValidateMaximumAllowedText(string ExpectedText)
		{
			WaitForElementVisible(MaximumAllowed_Field);
			ScrollToElement(MaximumAllowed_Field);
			ValidateElementValue(MaximumAllowed_Field, ExpectedText);

			return this;
		}

		public RateUnitRecordPage InsertMaximumAllowedText(string TextToInsert)
		{
			WaitForElementToBeClickable(MaximumAllowed_Field);
			ScrollToElement(MaximumAllowed_Field);
			SendKeys(MaximumAllowed_Field, TextToInsert);
			SendKeysWithoutClearing(MaximumAllowed_Field, Keys.Tab);

			return this;
		}

		public RateUnitRecordPage ValidateMaximumAllowedFieldErrorLabelText(string ExpectedText)
		{
			WaitForElement(MaximumAllowed_FieldErrorLabel);
			ScrollToElement(MaximumAllowed_FieldErrorLabel);
			ValidateElementText(MaximumAllowed_FieldErrorLabel, ExpectedText);

			return this;
		}

		public RateUnitRecordPage ValidateUnitDivisorText(string ExpectedText)
		{
			WaitForElementVisible(UnitDivisor_Field);
			ScrollToElement(UnitDivisor_Field);
			ValidateElementValue(UnitDivisor_Field, ExpectedText);

			return this;
		}

		public RateUnitRecordPage InsertUnitDivisorText(string TextToInsert)
		{
			WaitForElementToBeClickable(UnitDivisor_Field);
			ScrollToElement(UnitDivisor_Field);
			SendKeys(UnitDivisor_Field, TextToInsert);
			SendKeysWithoutClearing(UnitDivisor_Field, Keys.Tab);

			return this;
		}

		public RateUnitRecordPage ValidateUnitDivisorFieldErrorLabelText(string ExpectedText)
		{
			WaitForElement(UnitDivisor_FieldErrorLabel);
			ScrollToElement(UnitDivisor_FieldErrorLabel);
			ValidateElementText(UnitDivisor_FieldErrorLabel, ExpectedText);

			return this;
		}

		public RateUnitRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
		{
			WaitForElement(Name_FieldErrorLabel);
			ScrollToElement(Name_FieldErrorLabel);
			ValidateElementText(Name_FieldErrorLabel, ExpectedText);

			return this;
		}

		public RateUnitRecordPage ValidateNotificationAreaText(string ExpectedText)
		{
			ValidateElementText(notificationMessage, ExpectedText);

			return this;
		}

		public RateUnitRecordPage ValidateNameMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(Name_LabelField);
			ScrollToElement(Name_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(Name_MandatoryField);
			else
				WaitForElementNotVisible(Name_MandatoryField, 3);

			return this;
		}

		public RateUnitRecordPage ValidateCodeMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(Code_LabelField);
			ScrollToElement(Code_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(Code_MandatoryField);
			else
				WaitForElementNotVisible(Code_MandatoryField, 3);

			return this;
		}

		public RateUnitRecordPage ValidateStartDateMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(StartDate_LabelField);
			ScrollToElement(StartDate_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(StartDate_MandatoryField);
			else
				WaitForElementNotVisible(StartDate_MandatoryField, 3);

			return this;
		}

		public RateUnitRecordPage ValidateEndDateMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(EndDate_LabelField);
			ScrollToElement(EndDate_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(EndDate_MandatoryField);
			else
				WaitForElementNotVisible(EndDate_MandatoryField, 3);

			return this;
		}

		public RateUnitRecordPage ValidateResponsibleTeamMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(ResponsibleTeam_LabelField);
			ScrollToElement(ResponsibleTeam_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(ResponsibleTeam_MandatoryField);
			else
				WaitForElementNotVisible(ResponsibleTeam_MandatoryField, 3);

			return this;
		}

		public RateUnitRecordPage ValidateTimeAndDaysMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(TimeAndDays_LabelField);
			ScrollToElement(TimeAndDays_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(TimeAndDays_MandatoryField);
			else
				WaitForElementNotVisible(TimeAndDays_MandatoryField, 3);

			return this;
		}

		public RateUnitRecordPage ValidateOneOffMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(OneOff_LabelField);
			ScrollToElement(OneOff_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(OneOff_MandatoryField);
			else
				WaitForElementNotVisible(OneOff_MandatoryField, 3);

			return this;
		}

		public RateUnitRecordPage ValidateBandedRatesMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(BandedRates_LabelField);
			ScrollToElement(BandedRates_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(BandedRates_MandatoryField);
			else
				WaitForElementNotVisible(BandedRates_MandatoryField, 3);

			return this;
		}

		public RateUnitRecordPage ValidateDecimalPlacesMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(DecimalPlaces_LabelField);
			ScrollToElement(DecimalPlaces_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(DecimalPlaces_MandatoryField);
			else
				WaitForElementNotVisible(DecimalPlaces_MandatoryField, 3);

			return this;
		}

		public RateUnitRecordPage ValidateMinimumAllowedMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(MinimumAllowed_LabelField);
			ScrollToElement(MinimumAllowed_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(MinimumAllowed_MandatoryField);
			else
				WaitForElementNotVisible(MinimumAllowed_MandatoryField, 3);

			return this;
		}

		public RateUnitRecordPage ValidateMaximumAllowedMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(MaximumAllowed_LabelField);
			ScrollToElement(MaximumAllowed_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(MaximumAllowed_MandatoryField);
			else
				WaitForElementNotVisible(MaximumAllowed_MandatoryField, 3);

			return this;
		}

		public RateUnitRecordPage ValidateUnitDivisorMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(UnitDivisor_LabelField);
			ScrollToElement(UnitDivisor_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(UnitDivisor_MandatoryField);
			else
				WaitForElementNotVisible(UnitDivisor_MandatoryField, 3);

			return this;
		}

		public RateUnitRecordPage ValidateNameFieldMaximumTextLimit(string expected)
		{
			WaitForElementVisible(Name_Field);
			ValidateElementMaxLength(Name_Field, expected);

			return this;
		}

		public RateUnitRecordPage ValidateCodeFieldMaximumTextLimit(string expected)
		{
			WaitForElementVisible(Code_Field);
			ValidateElementMaxLength(Code_Field, expected);

			return this;
		}

		public RateUnitRecordPage ValidateGovCodeFieldMaximumTextLimit(string expected)
		{
			WaitForElementVisible(GovCode_Field);
			ValidateElementMaxLength(GovCode_Field, expected);

			return this;
		}

		public RateUnitRecordPage ValidateCodeFieldIsNumeric()
		{
			WaitForElementVisible(Code_Field);
			ValidateElementAttribute(Code_Field, "validatenumeric", "ValidateNumeric");

			return this;
		}

		public RateUnitRecordPage WaitForRecordToBeSaved()
		{
			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);
			WaitForElementVisible(AdditionalToolbarElementsButton);

			return this;
		}

		public RateUnitRecordPage ValidateResponsibleTeamLookupButtondDisabled(bool ExpectDisabled)
		{
			WaitForElementVisible(ResponsibleTeam_LookupButton);
			ScrollToElement(ResponsibleTeam_LookupButton);

			if (ExpectDisabled)
				ValidateElementDisabled(ResponsibleTeam_LookupButton);
			else
				ValidateElementNotDisabled(ResponsibleTeam_LookupButton);

			return this;
		}

		public RateUnitRecordPage ValidateNameFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(Name_Field);
			ScrollToElement(Name_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(Name_Field);
			else
				ValidateElementNotDisabled(Name_Field);

			return this;
		}

		public RateUnitRecordPage ValidateCodeFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(Code_Field);
			ScrollToElement(Code_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(Code_Field);
			else
				ValidateElementNotDisabled(Code_Field);

			return this;
		}

		public RateUnitRecordPage ValidateGovCodeFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(GovCode_Field);
			ScrollToElement(GovCode_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(GovCode_Field);
			else
				ValidateElementNotDisabled(GovCode_Field);

			return this;
		}

		public RateUnitRecordPage ValidateNoteTextFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(NoteText_Field);
			ScrollToElement(NoteText_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(NoteText_Field);
			else
				ValidateElementNotDisabled(NoteText_Field);

			return this;
		}

		public RateUnitRecordPage ValidateStartDateFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(StartDate_Field);
			ScrollToElement(StartDate_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(StartDate_Field);
			else
				ValidateElementNotDisabled(StartDate_Field);

			return this;
		}

		public RateUnitRecordPage ValidateEndDateFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(EndDate_Field);
			ScrollToElement(EndDate_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(EndDate_Field);
			else
				ValidateElementNotDisabled(EndDate_Field);

			return this;
		}

		public RateUnitRecordPage ValidateInactiveOptionsIsDisabledOrNot(bool ExpectDisabled)
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

		public RateUnitRecordPage ValidateValidForExportOptionsIsDisabledOrNot(bool ExpectDisabled)
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

		public RateUnitRecordPage ValidateTimeAndDaysOptionsIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(TimeAndDays_YesOption);
			ScrollToElement(TimeAndDays_YesOption);

			if (ExpectDisabled)
			{
				ValidateElementDisabled(TimeAndDays_YesOption);
				ValidateElementDisabled(TimeAndDays_NoOption);
			}
			else
			{
				ValidateElementNotDisabled(TimeAndDays_YesOption);
				ValidateElementNotDisabled(TimeAndDays_NoOption);
			}

			return this;
		}

		public RateUnitRecordPage ValidateOneOffOptionsIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(OneOff_YesOption);
			ScrollToElement(OneOff_YesOption);

			if (ExpectDisabled)
			{
				ValidateElementDisabled(OneOff_YesOption);
				ValidateElementDisabled(OneOff_NoOption);
			}
			else
			{
				ValidateElementNotDisabled(OneOff_YesOption);
				ValidateElementNotDisabled(OneOff_NoOption);
			}

			return this;
		}

		public RateUnitRecordPage ValidateBandedRatesOptionsIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(BandedRates_YesOption);
			ScrollToElement(BandedRates_YesOption);

			if (ExpectDisabled)
			{
				ValidateElementDisabled(BandedRates_YesOption);
				ValidateElementDisabled(BandedRates_NoOption);
			}
			else
			{
				ValidateElementNotDisabled(BandedRates_YesOption);
				ValidateElementNotDisabled(BandedRates_NoOption);
			}

			return this;
		}

		public RateUnitRecordPage ValidateDecimalPlacesOptionsIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(DecimalPlaces_YesOption);
			ScrollToElement(DecimalPlaces_YesOption);

			if (ExpectDisabled)
			{
				ValidateElementDisabled(DecimalPlaces_YesOption);
				ValidateElementDisabled(DecimalPlaces_NoOption);
			}
			else
			{
				ValidateElementNotDisabled(DecimalPlaces_YesOption);
				ValidateElementNotDisabled(DecimalPlaces_NoOption);
			}

			return this;
		}

		public RateUnitRecordPage ValidateMinimumAllowedFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(MinimumAllowed_Field);
			ScrollToElement(MinimumAllowed_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(MinimumAllowed_Field);
			else
				ValidateElementNotDisabled(MinimumAllowed_Field);

			return this;
		}

		public RateUnitRecordPage ValidateMaximumAllowedFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(MaximumAllowed_Field);
			ScrollToElement(MaximumAllowed_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(MaximumAllowed_Field);
			else
				ValidateElementNotDisabled(MaximumAllowed_Field);

			return this;
		}

		public RateUnitRecordPage ValidateUnitDivisorFieldIsDisabledOrNot(bool ExpectDisabled)
		{
			WaitForElementVisible(UnitDivisor_Field);
			ScrollToElement(UnitDivisor_Field);

			if (ExpectDisabled)
				ValidateElementDisabled(UnitDivisor_Field);
			else
				ValidateElementNotDisabled(UnitDivisor_Field);

			return this;
		}

		public RateUnitRecordPage ValidateActivateButtonVisible()
		{
			WaitForElementNotVisible("CWRefreshPanel", 10);
			WaitForElementVisible(ActivateButton);

			return this;
		}

		public RateUnitRecordPage NavigateToAuditSubPage()
		{
			WaitForElementToBeClickable(leftMenuButton);
			Click(leftMenuButton);

			WaitForElementToBeClickable(auditLink_LeftMenu);
			Click(auditLink_LeftMenu);

			return this;
		}

		public RateUnitRecordPage ValidateBandedRatesIsVisible(bool IsVisible)
		{
			if (IsVisible)
			{
				WaitForElementVisible(BandedRates_LabelField);
				WaitForElementVisible(BandedRates_YesOption);
				WaitForElementVisible(BandedRates_NoOption);
			}
			else
            {
				WaitForElementNotVisible(BandedRates_LabelField, 3);
				WaitForElementNotVisible(BandedRates_YesOption, 3);
				WaitForElementNotVisible(BandedRates_NoOption, 3);
			}

			return this;
		}

		public RateUnitRecordPage ValidateDecimalPlacesIsVisible(bool IsVisible)
		{
			if (IsVisible)
			{
				WaitForElementVisible(DecimalPlaces_LabelField);
				WaitForElementVisible(DecimalPlaces_YesOption);
				WaitForElementVisible(DecimalPlaces_NoOption);
			}
			else
            {
				WaitForElementNotVisible(DecimalPlaces_LabelField, 3);
				WaitForElementNotVisible(DecimalPlaces_YesOption, 3);
				WaitForElementNotVisible(DecimalPlaces_NoOption, 3);
			}

			return this;
		}

		public RateUnitRecordPage ValidateMinimumAllowedFieldIsVisible(bool IsVisible)
		{
			if (IsVisible)
			{
				WaitForElementVisible(MinimumAllowed_LabelField);
				WaitForElementVisible(MinimumAllowed_Field);
			}
			else
			{
				WaitForElementNotVisible(MinimumAllowed_LabelField, 3);
				WaitForElementNotVisible(MinimumAllowed_Field, 3);
			}

			return this;
		}

		public RateUnitRecordPage ValidateMaximumAllowedFieldIsVisible(bool IsVisible)
		{
			if (IsVisible)
			{
				WaitForElementVisible(MaximumAllowed_LabelField);
				WaitForElementVisible(MaximumAllowed_Field);
			}
			else
			{
				WaitForElementNotVisible(MaximumAllowed_LabelField, 3);
				WaitForElementNotVisible(MaximumAllowed_Field, 3);
			}

			return this;
		}

		public RateUnitRecordPage ValidateUnitDivisorFieldIsVisible(bool IsVisible)
		{
			if (IsVisible)
			{
				WaitForElementVisible(UnitDivisor_LabelField);
				WaitForElementVisible(UnitDivisor_Field);
			}
			else
			{
				WaitForElementNotVisible(UnitDivisor_LabelField, 3);
				WaitForElementNotVisible(UnitDivisor_Field, 3);
			}

			return this;
		}

		public RateUnitRecordPage ValidatePrecisionLimitOfMinimumAllowedField(string expected)
		{
			WaitForElementVisible(MinimumAllowed_Field);
			ValidateElementAttribute(MinimumAllowed_Field, "validateprecision", expected);

			return this;
		}

		public RateUnitRecordPage ValidatePrecisionLimitOfMaximumAllowedField(string expected)
		{
			WaitForElementVisible(MaximumAllowed_Field);
			ValidateElementAttribute(MaximumAllowed_Field, "validateprecision", expected);

			return this;
		}

		public RateUnitRecordPage ValidatePrecisionLimitOfUnitDivisorField(string expected)
		{
			WaitForElementVisible(UnitDivisor_Field);
			ValidateElementAttribute(UnitDivisor_Field, "validateprecision", expected);

			return this;
		}

		public RateUnitRecordPage ClickTimeAndDays_Option(bool YesOption)
		{
			if (YesOption)
			{
				WaitForElementToBeClickable(TimeAndDays_YesOption);
				ScrollToElement(TimeAndDays_YesOption);
				Click(TimeAndDays_YesOption);
			}
			else
			{
				WaitForElementToBeClickable(TimeAndDays_NoOption);
				ScrollToElement(TimeAndDays_NoOption);
				Click(TimeAndDays_NoOption);
			}

			return this;
		}

		public RateUnitRecordPage ClickOneOff_Option(bool YesOption)
		{
			if (YesOption)
			{
				WaitForElementToBeClickable(OneOff_YesOption);
				ScrollToElement(OneOff_YesOption);
				Click(OneOff_YesOption);
			}
			else
			{
				WaitForElementToBeClickable(OneOff_NoOption);
				ScrollToElement(OneOff_NoOption);
				Click(OneOff_NoOption);
			}

			return this;
		}

		public RateUnitRecordPage ClickBandedRates_Option(bool YesOption)
		{
			if (YesOption)
			{
				WaitForElementToBeClickable(BandedRates_YesOption);
				ScrollToElement(BandedRates_YesOption);
				Click(BandedRates_YesOption);
			}
			else
			{
				WaitForElementToBeClickable(BandedRates_NoOption);
				ScrollToElement(BandedRates_NoOption);
				Click(BandedRates_NoOption);
			}

			return this;
		}

		public RateUnitRecordPage ClickDecimalPlaces_Option(bool YesOption)
		{
			if (YesOption)
			{
				WaitForElementToBeClickable(DecimalPlaces_YesOption);
				ScrollToElement(DecimalPlaces_YesOption);
				Click(DecimalPlaces_YesOption);
			}
			else
			{
				WaitForElementToBeClickable(DecimalPlaces_NoOption);
				ScrollToElement(DecimalPlaces_NoOption);
				Click(DecimalPlaces_NoOption);
			}

			return this;
		}

		public RateUnitRecordPage ValidateTimeAndDays_OptionIsCheckedOrNot(bool IsYesChecked)
		{
			WaitForElementVisible(TimeAndDays_YesOption);
			WaitForElementVisible(TimeAndDays_NoOption);
			ScrollToElement(TimeAndDays_YesOption);

			if (IsYesChecked)
			{
				ValidateElementChecked(TimeAndDays_YesOption);
				ValidateElementNotChecked(TimeAndDays_NoOption);
			}
			else
			{
				ValidateElementChecked(TimeAndDays_NoOption);
				ValidateElementNotChecked(TimeAndDays_YesOption);
			}

			return this;
		}

		public RateUnitRecordPage ValidateOneOff_OptionIsCheckedOrNot(bool IsYesChecked)
		{
			WaitForElementVisible(OneOff_YesOption);
			WaitForElementVisible(OneOff_NoOption);
			ScrollToElement(OneOff_YesOption);

			if (IsYesChecked)
			{
				ValidateElementChecked(OneOff_YesOption);
				ValidateElementNotChecked(OneOff_NoOption);
			}
			else
			{
				ValidateElementChecked(OneOff_NoOption);
				ValidateElementNotChecked(OneOff_YesOption);
			}

			return this;
		}

		public RateUnitRecordPage ValidateBandedRates_OptionIsCheckedOrNot(bool IsYesChecked)
		{
			WaitForElementVisible(BandedRates_YesOption);
			WaitForElementVisible(BandedRates_NoOption);
			ScrollToElement(BandedRates_YesOption);

			if (IsYesChecked)
			{
				ValidateElementChecked(BandedRates_YesOption);
				ValidateElementNotChecked(BandedRates_NoOption);
			}
			else
			{
				ValidateElementChecked(BandedRates_NoOption);
				ValidateElementNotChecked(BandedRates_YesOption);
			}

			return this;
		}

		public RateUnitRecordPage ValidateDecimalPlaces_OptionIsCheckedOrNot(bool IsYesChecked)
		{
			WaitForElementVisible(DecimalPlaces_YesOption);
			WaitForElementVisible(DecimalPlaces_NoOption);
			ScrollToElement(DecimalPlaces_YesOption);

			if (IsYesChecked)
			{
				ValidateElementChecked(DecimalPlaces_YesOption);
				ValidateElementNotChecked(DecimalPlaces_NoOption);
			}
			else
			{
				ValidateElementChecked(DecimalPlaces_NoOption);
				ValidateElementNotChecked(DecimalPlaces_YesOption);
			}

			return this;
		}
	}
}
