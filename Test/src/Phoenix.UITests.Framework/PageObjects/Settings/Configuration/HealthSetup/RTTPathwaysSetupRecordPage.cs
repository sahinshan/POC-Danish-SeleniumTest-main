using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class RTTPathwaysSetupRecordPage : CommonMethods
	{
		public RTTPathwaysSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=rttpathwaysetup&')]");

		readonly By pageHeader = By.XPath("//*[@id='CWToolbar']//h1");
		readonly By pageTitle = By.XPath("//*[@id='CWToolbar']/div/h1/span");

		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
		readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
		readonly By AdditionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']//div[@id='CWToolbarMenu']/button");

		readonly By Name_LabelField = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
		readonly By Name_MandatoryField = By.XPath("//*[@id='CWLabelHolder_name']/label/span[@class='mandatory']");
		readonly By Name_InputField = By.Id("CWField_name");

		readonly By Code_LabelField = By.XPath("//*[@id='CWLabelHolder_code']/label[text()='Code']");
		readonly By Code_MandatoryField = By.XPath("//*[@id='CWLabelHolder_code']/label/span[@class='mandatory']");
		readonly By Code_InputField = By.Id("CWField_code");
		readonly By Code_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_code']/label/span");

		readonly By GovCode_LabelField = By.XPath("//*[@id='CWLabelHolder_govcode']/label[text()='Gov Code']");
		readonly By GovCode_MandatoryField = By.XPath("//*[@id='CWLabelHolder_govcode']/label/span[@class='mandatory']");
		readonly By GovCode_InputField = By.Id("CWField_govcode");
		readonly By GovCode_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_govcode']/label/span");

		readonly By StartDate_LabelField = By.XPath("//*[@id='CWLabelHolder_startdate']/label[text()='Start Date']");
		readonly By StartDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_startdate']/label/span[@class='mandatory']");
		readonly By StartDate_InputField = By.Id("CWField_startdate");
		readonly By StartDate_DatePicker = By.Id("CWField_startdate_DatePicker");

		readonly By EndDate_LabelField = By.XPath("//*[@id='CWLabelHolder_enddate']/label[text()='End Date']");
		readonly By EndDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_enddate']/label/span[@class='mandatory']");
		readonly By EndDate_InputField = By.Id("CWField_enddate");
		readonly By EndDate_DatePicker = By.Id("CWField_enddate_DatePicker");

		readonly By ResponsibleTeam_LabelField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
		readonly By ResponsibleTeam_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span[@class='mandatory']");
		readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
		readonly By ResponsibleTeam_LinkField = By.Id("CWField_ownerid_Link");
		readonly By ResponsibleTeam_RemoveButton = By.Id("CWClearLookup_ownerid");

		readonly By FirstAppointmentNoLaterThan_LabelField = By.XPath("//*[@id='CWLabelHolder_firstappointmentnolaterthan']/label[text()='First Appointment No Later Than']");
		readonly By FirstAppointmentNoLaterThan_MandatoryField = By.XPath("//*[@id='CWLabelHolder_firstappointmentnolaterthan']/label/span[@class='mandatory']");
		readonly By FirstAppointmentNoLaterThan_InputField = By.Id("CWField_firstappointmentnolaterthan");
		readonly By FirstAppointmentNoLaterThan_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_firstappointmentnolaterthan']/label/span");

		readonly By WarnAfter_LabelField = By.XPath("//*[@id='CWLabelHolder_warnafter']/label[text()='Warn After']");
		readonly By WarnAfter_MandatoryField = By.XPath("//*[@id='CWLabelHolder_warnafter']/label/span[@class='mandatory']");
		readonly By WarnAfter_InputField = By.Id("CWField_warnafter");
		readonly By WarnAfter_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_warnafter']/label/span");

		readonly By BreachOccursAfter_LabelField = By.XPath("//*[@id='CWLabelHolder_breachoccursafter']/label[text()='Breach Occurs After']");
		readonly By BreachOccursAfter_MandatoryField = By.XPath("//*[@id='CWLabelHolder_breachoccursafter']/label/span[@class='mandatory']");
		readonly By BreachOccursAfter_InputField = By.Id("CWField_breachoccursafter");
		readonly By BreachOccursAfter_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_breachoccursafter']/label/span");

		public RTTPathwaysSetupRecordPage WaitForRTTPathwaysSetupRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElement(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElement(iframe_CWDialog_);
			SwitchToIframe(iframe_CWDialog_);

			WaitForElementVisible(pageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateRTTPathwaySetupRecordTitle(string RTTPathwaySetupTitle)
		{
			WaitForElementVisible(pageTitle);
			MoveToElementInPage(pageTitle);
			ValidateElementText(pageTitle, RTTPathwaySetupTitle);

			return this;
		}

		public RTTPathwaysSetupRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			MoveToElementInPage(BackButton);
			Click(BackButton);

			return this;
		}

		public RTTPathwaysSetupRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			MoveToElementInPage(SaveButton);
			Click(SaveButton);

			return this;
		}

		public RTTPathwaysSetupRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			MoveToElementInPage(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementVisible(ResponsibleTeam_LinkField);
			ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

			return this;
		}

		public RTTPathwaysSetupRecordPage InsertName(string TextToInsert)
		{
			WaitForElementToBeClickable(Name_InputField);
			MoveToElementInPage(Name_InputField);
			SendKeys(Name_InputField, TextToInsert);

			return this;
		}

		public RTTPathwaysSetupRecordPage InsertCode(string ValueToInsert)
		{
			WaitForElementToBeClickable(Code_InputField);
			MoveToElementInPage(Code_InputField);
			SendKeys(Code_InputField, ValueToInsert);

			return this;
		}

		public RTTPathwaysSetupRecordPage InsertGovCode(string ValueToInsert)
		{
			WaitForElementToBeClickable(GovCode_InputField);
			MoveToElementInPage(GovCode_InputField);
			SendKeys(GovCode_InputField, ValueToInsert);

			return this;
		}

		public RTTPathwaysSetupRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
			MoveToElementInPage(ResponsibleTeam_LookupButton);
			Click(ResponsibleTeam_LookupButton);

			return this;
		}

		public RTTPathwaysSetupRecordPage InsertStartDate(string TextToInsert)
		{
			WaitForElementVisible(StartDate_InputField);
			MoveToElementInPage(StartDate_InputField);
			SendKeys(StartDate_InputField, TextToInsert);

			return this;
		}

		public RTTPathwaysSetupRecordPage InsertEndDate(string TextToInsert)
		{
			WaitForElementVisible(EndDate_InputField);
			MoveToElementInPage(EndDate_InputField);
			SendKeys(EndDate_InputField, TextToInsert);

			return this;
		}

		public RTTPathwaysSetupRecordPage InsertFirstAppointmentNoLaterThan(string ValueToInsert)
		{
			WaitForElementVisible(FirstAppointmentNoLaterThan_InputField);
			MoveToElementInPage(FirstAppointmentNoLaterThan_InputField);
			SendKeys(FirstAppointmentNoLaterThan_InputField, ValueToInsert);

			return this;
		}

		public RTTPathwaysSetupRecordPage InsertWarnAfter(string ValueToInsert)
		{
			WaitForElementVisible(WarnAfter_InputField);
			MoveToElementInPage(WarnAfter_InputField);
			SendKeys(WarnAfter_InputField, ValueToInsert);

			return this;
		}

		public RTTPathwaysSetupRecordPage InsertBreachOccursAfter(string ValueToInsert)
		{
			WaitForElementVisible(BreachOccursAfter_InputField);
			MoveToElementInPage(BreachOccursAfter_InputField);
			SendKeys(BreachOccursAfter_InputField, ValueToInsert);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateAllFieldsVisible()
        {
			WaitForElementVisible(Name_LabelField);
			WaitForElementVisible(Name_InputField);

			WaitForElementVisible(Code_LabelField);
			WaitForElementVisible(Code_InputField);

			WaitForElementVisible(GovCode_LabelField);
			WaitForElementVisible(GovCode_InputField);

			WaitForElementVisible(StartDate_LabelField);
			WaitForElementVisible(StartDate_InputField);

			WaitForElementVisible(EndDate_LabelField);
			WaitForElementVisible(EndDate_InputField);

			WaitForElementVisible(ResponsibleTeam_LabelField);
			WaitForElementVisible(ResponsibleTeam_LookupButton);

			WaitForElementVisible(FirstAppointmentNoLaterThan_LabelField);
			WaitForElementVisible(FirstAppointmentNoLaterThan_InputField);

			WaitForElementVisible(WarnAfter_LabelField);
			WaitForElementVisible(WarnAfter_InputField);

			WaitForElementVisible(BreachOccursAfter_LabelField);
			WaitForElementVisible(BreachOccursAfter_InputField);

			return this;
        }

		public RTTPathwaysSetupRecordPage ValidateNameMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(Name_LabelField);
			MoveToElementInPage(Name_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(Name_MandatoryField);
			else
				WaitForElementNotVisible(Name_MandatoryField, 3);
			
			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateCodeMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(Code_LabelField);
			MoveToElementInPage(Code_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(Code_MandatoryField);
			else
				WaitForElementNotVisible(Code_MandatoryField, 3);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateGovCodeMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(GovCode_LabelField);
			MoveToElementInPage(GovCode_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(GovCode_MandatoryField);
			else
				WaitForElementNotVisible(GovCode_MandatoryField, 3);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateStartDateMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(StartDate_LabelField);
			MoveToElementInPage(StartDate_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(StartDate_MandatoryField);
			else
				WaitForElementNotVisible(StartDate_MandatoryField, 3);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateStartDateDatePickerIsVisibile()
		{
			WaitForElementVisible(StartDate_DatePicker);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateEndDateMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(EndDate_LabelField);
			MoveToElementInPage(EndDate_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(EndDate_MandatoryField);
			else
				WaitForElementNotVisible(EndDate_MandatoryField, 3);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateEndDateDatePickerIsVisibile()
		{
			WaitForElementVisible(EndDate_DatePicker);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateResponsibleTeamMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(ResponsibleTeam_LabelField);
			MoveToElementInPage(ResponsibleTeam_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(ResponsibleTeam_MandatoryField);
			else
				WaitForElementNotVisible(ResponsibleTeam_MandatoryField, 3);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateFirstAppointmentNoLaterThanMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(FirstAppointmentNoLaterThan_LabelField);
			MoveToElementInPage(FirstAppointmentNoLaterThan_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(FirstAppointmentNoLaterThan_MandatoryField);
			else
				WaitForElementNotVisible(FirstAppointmentNoLaterThan_MandatoryField, 3);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateWarnAfterMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(WarnAfter_LabelField);
			MoveToElementInPage(WarnAfter_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(WarnAfter_MandatoryField);
			else
				WaitForElementNotVisible(WarnAfter_MandatoryField, 3);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateBreachOccursAfterMandatoryFieldVisibility(bool ExpectVisible)
		{
			WaitForElementVisible(BreachOccursAfter_LabelField);
			MoveToElementInPage(BreachOccursAfter_LabelField);

			if (ExpectVisible)
				WaitForElementVisible(BreachOccursAfter_MandatoryField);
			else
				WaitForElementNotVisible(BreachOccursAfter_MandatoryField, 3);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateNameFieldMaximumLimitText(string expected)
		{
			WaitForElementVisible(Name_InputField);
			ValidateElementMaxLength(Name_InputField, expected);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateCodeFieldIsNumeric()
		{
			WaitForElementVisible(Code_InputField);
			ValidateElementAttribute(Code_InputField, "validatenumeric", "ValidateNumeric");

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateGovCodeFieldIsNumeric()
		{
			WaitForElementVisible(GovCode_InputField);
			ValidateElementAttribute(GovCode_InputField, "validatenumeric", "ValidateNumeric");

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateFirstAppointmentNoLaterThanFieldIsNumeric()
		{
			WaitForElementVisible(FirstAppointmentNoLaterThan_InputField);
			ValidateElementAttribute(FirstAppointmentNoLaterThan_InputField, "validatenumeric", "ValidateNumeric");

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateWarnAfterFieldIsNumeric()
		{
			WaitForElementVisible(WarnAfter_InputField);
			ValidateElementAttribute(WarnAfter_InputField, "validatenumeric", "ValidateNumeric");

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateBreachOccursAfterFieldIsNumeric()
		{
			WaitForElementVisible(BreachOccursAfter_InputField);
			ValidateElementAttribute(BreachOccursAfter_InputField, "validatenumeric", "ValidateNumeric");

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateCodeFieldErrorLabelText(string ExpectedText)
		{
			WaitForElementVisible(Code_FieldErrorLabel);
			MoveToElementInPage(Code_FieldErrorLabel);
			ValidateElementText(Code_FieldErrorLabel, ExpectedText);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateGovCodeFieldErrorLabelText(string ExpectedText)
		{
			WaitForElementVisible(GovCode_FieldErrorLabel);
			MoveToElementInPage(GovCode_FieldErrorLabel);
			ValidateElementText(GovCode_FieldErrorLabel, ExpectedText);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateFirstAppointmentNoLaterThanFieldErrorLabelText(string ExpectedText)
		{
			WaitForElementVisible(FirstAppointmentNoLaterThan_FieldErrorLabel);
			MoveToElementInPage(FirstAppointmentNoLaterThan_FieldErrorLabel);
			ValidateElementText(FirstAppointmentNoLaterThan_FieldErrorLabel, ExpectedText);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateWarnAfterFieldErrorLabelText(string ExpectedText)
		{
			WaitForElementVisible(WarnAfter_FieldErrorLabel);
			MoveToElementInPage(WarnAfter_FieldErrorLabel);
			ValidateElementText(WarnAfter_FieldErrorLabel, ExpectedText);

			return this;
		}

		public RTTPathwaysSetupRecordPage ValidateBreachOccursAfterFieldErrorLabelText(string ExpectedText)
		{
			WaitForElementVisible(BreachOccursAfter_FieldErrorLabel);
			MoveToElementInPage(BreachOccursAfter_FieldErrorLabel);
			ValidateElementText(BreachOccursAfter_FieldErrorLabel, ExpectedText);

			return this;
		}

		public RTTPathwaysSetupRecordPage WaitForRecordToBeSaved()
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
