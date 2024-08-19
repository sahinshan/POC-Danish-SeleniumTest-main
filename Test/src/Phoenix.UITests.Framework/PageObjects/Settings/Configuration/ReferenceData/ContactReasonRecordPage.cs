using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class ContactReasonRecordPage : CommonMethods
	{
		public ContactReasonRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		readonly By iframe_ContactReasonFrame = By.Id("iframe_contactreason");
		readonly By cwDialog_ContactReasonFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=contactreason')]");

		readonly By ContactReasonRecordPageHeader = By.XPath("//*[@id='CWToolbar']//h1");
		readonly By BackButton = By.Id("BackButton");
		readonly By SaveButton = By.Id("TI_SaveButton");
		readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
		readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
		readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
		readonly By Name_Field = By.Id("CWField_name");
		readonly By Name_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");
		readonly By Code_Field = By.Id("CWField_code");
		readonly By GovCode_Field = By.Id("CWField_govcode");
		readonly By BusinessType_Picklist = By.Id("CWField_businesstypeid");
		readonly By BusinessType_MandatoryField = By.XPath("//*[@id='CWLabelHolder_businesstypeid']//span[@class='mandatory']");
		readonly By AdmissionType_Picklist = By.Id("CWField_rttadmissiontypeid");
		readonly By AdmissionType_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_rttadmissiontypeid']/label/span");
		readonly By AdmissionType_MandatoryField = By.XPath("//*[@id='CWLabelHolder_rttadmissiontypeid']//span[@class='mandatory']");
		readonly By BusinessArea_Picklist = By.Id("CWField_businessareaid");
		readonly By Inactive_YesOption = By.Id("CWField_inactive_1");
		readonly By Inactive_NoOption = By.Id("CWField_inactive_0");
		readonly By StartDate_Field = By.Id("CWField_startdate");
		readonly By StartDateDatePicker = By.Id("CWField_startdate_DatePicker");
		readonly By EndDate_Field = By.Id("CWField_enddate");
		readonly By EndDateDatePicker = By.Id("CWField_enddate_DatePicker");
		readonly By ValidForExport_YesOption = By.Id("CWField_validforexport_1");
		readonly By ValidForExport_NoOption = By.Id("CWField_validforexport_0");
		readonly By ResponsibleTeamLink = By.Id("CWField_ownerid_Link");
		readonly By ResponsibleTeamLookupButton = By.Id("CWLookupBtn_ownerid");
		readonly By SupportMultipleCases_YesOption = By.Id("CWField_supportmultiplecases_1");
		readonly By SupportMultipleCases_NoOption = By.Id("CWField_supportmultiplecases_0");
		
		readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");


		public ContactReasonRecordPage WaitForContactReasonRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementVisible(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElementVisible(iframe_ContactReasonFrame);
			SwitchToIframe(iframe_ContactReasonFrame);

			WaitForElementVisible(cwDialog_ContactReasonFrame);
			SwitchToIframe(cwDialog_ContactReasonFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(ContactReasonRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);

			return this;
		}

		public ContactReasonRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			MoveToElementInPage(BackButton);
			Click(BackButton);

			return this;
		}

		public ContactReasonRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			MoveToElementInPage(SaveButton);
			Click(SaveButton);

			return this;
		}

		public ContactReasonRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			MoveToElementInPage(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public ContactReasonRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			MoveToElementInPage(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public ContactReasonRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			MoveToElementInPage(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public ContactReasonRecordPage ValidateNameText(string ExpectedText)
		{
			ValidateElementValue(Name_Field, ExpectedText);

			return this;
		}

		public ContactReasonRecordPage InsertTextOnName(string TextToInsert)
		{
			WaitForElementToBeClickable(Name_Field);
			SendKeys(Name_Field, TextToInsert);

			return this;
		}

		public ContactReasonRecordPage ValidateCodeText(string ExpectedText)
		{
			ValidateElementValue(Code_Field, ExpectedText);

			return this;
		}

		public ContactReasonRecordPage InsertTextOnCode(string TextToInsert)
		{
			WaitForElementToBeClickable(Code_Field);
			SendKeys(Code_Field, TextToInsert);

			return this;
		}

		public ContactReasonRecordPage ValidateGovcodeText(string ExpectedText)
		{
			ValidateElementValue(GovCode_Field, ExpectedText);

			return this;
		}

		public ContactReasonRecordPage InsertTextOnGovcode(string TextToInsert)
		{
			WaitForElementToBeClickable(GovCode_Field);
			SendKeys(GovCode_Field, TextToInsert);

			return this;
		}

		public ContactReasonRecordPage SelectBusinessType(string TextToSelect)
		{
			WaitForElementToBeClickable(BusinessType_Picklist);
			SelectPicklistElementByText(BusinessType_Picklist, TextToSelect);

			return this;
		}

		public ContactReasonRecordPage ValidateBusinessTypeSelectedText(string ExpectedText)
		{
			ValidateElementText(BusinessType_Picklist, ExpectedText);

			return this;
		}

		public ContactReasonRecordPage SelectAdmissionType(string TextToSelect)
		{
			WaitForElementToBeClickable(AdmissionType_Picklist);
			SelectPicklistElementByText(AdmissionType_Picklist, TextToSelect);

			return this;
		}

		public ContactReasonRecordPage ValidateAdmissionTypeSelectedText(string ExpectedText)
		{
			ValidateElementText(AdmissionType_Picklist, ExpectedText);

			return this;
		}

		public ContactReasonRecordPage SelectBusinessArea(string TextToSelect)
		{
			WaitForElementToBeClickable(BusinessArea_Picklist);
			SelectPicklistElementByText(BusinessArea_Picklist, TextToSelect);

			return this;
		}

		public ContactReasonRecordPage ValidateBusinessAreaSelectedText(string ExpectedText)
		{
			ValidateElementText(BusinessArea_Picklist, ExpectedText);

			return this;
		}

		public ContactReasonRecordPage ClickInactive_YesOption()
		{
			WaitForElementToBeClickable(Inactive_YesOption);
			MoveToElementInPage(Inactive_YesOption);
			Click(Inactive_YesOption);

			return this;
		}

		public ContactReasonRecordPage ValidateInactive_YesOptionChecked()
		{
			WaitForElement(Inactive_YesOption);
			ValidateElementChecked(Inactive_YesOption);

			return this;
		}

		public ContactReasonRecordPage ValidateInactive_YesOptionNotChecked()
		{
			WaitForElement(Inactive_YesOption);
			ValidateElementNotChecked(Inactive_YesOption);

			return this;
		}

		public ContactReasonRecordPage ClickInactive_NoOption()
		{
			WaitForElementToBeClickable(Inactive_NoOption);
			MoveToElementInPage(Inactive_NoOption);
			Click(Inactive_NoOption);

			return this;
		}

		public ContactReasonRecordPage ValidateInactive_NoOptionChecked()
		{
			WaitForElement(Inactive_NoOption);
			ValidateElementChecked(Inactive_NoOption);

			return this;
		}

		public ContactReasonRecordPage ValidateInactive_NoOptionNotChecked()
		{
			WaitForElement(Inactive_NoOption);
			ValidateElementNotChecked(Inactive_NoOption);

			return this;
		}

		public ContactReasonRecordPage ValidateStartDateText(string ExpectedText)
		{
			ValidateElementValue(StartDate_Field, ExpectedText);

			return this;
		}

		public ContactReasonRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate_Field);
			SendKeys(StartDate_Field, TextToInsert);

			return this;
		}

		public ContactReasonRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartDateDatePicker);
			MoveToElementInPage(StartDateDatePicker);
			Click(StartDateDatePicker);

			return this;
		}

		public ContactReasonRecordPage ValidateEndDateText(string ExpectedText)
		{
			ValidateElementValue(EndDate_Field, ExpectedText);

			return this;
		}

		public ContactReasonRecordPage InsertTextOnEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate_Field);
			SendKeys(EndDate_Field, TextToInsert);

			return this;
		}

		public ContactReasonRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EndDateDatePicker);
			MoveToElementInPage(EndDateDatePicker);
			Click(EndDateDatePicker);

			return this;
		}

		public ContactReasonRecordPage ClickValidForExport_YesOption()
		{
			WaitForElementToBeClickable(ValidForExport_YesOption);
			MoveToElementInPage(ValidForExport_YesOption);
			Click(ValidForExport_YesOption);

			return this;
		}

		public ContactReasonRecordPage ValidateValidForExport_YesOptionChecked()
		{
			WaitForElement(ValidForExport_YesOption);
			ValidateElementChecked(ValidForExport_YesOption);

			return this;
		}

		public ContactReasonRecordPage ValidateValidForExport_YesOptionNotChecked()
		{
			WaitForElement(ValidForExport_YesOption);
			ValidateElementNotChecked(ValidForExport_YesOption);

			return this;
		}

		public ContactReasonRecordPage ClickValidForExport_NoOption()
		{
			WaitForElementToBeClickable(ValidForExport_NoOption);
			MoveToElementInPage(ValidForExport_NoOption);
			Click(ValidForExport_NoOption);

			return this;
		}

		public ContactReasonRecordPage ValidateValidForExport_NoOptionChecked()
		{
			WaitForElement(ValidForExport_NoOption);
			ValidateElementChecked(ValidForExport_NoOption);

			return this;
		}

		public ContactReasonRecordPage ValidateValidForExport_NoOptionNotChecked()
		{
			WaitForElement(ValidForExport_NoOption);
			ValidateElementNotChecked(ValidForExport_NoOption);

			return this;
		}

		public ContactReasonRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			MoveToElementInPage(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public ContactReasonRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public ContactReasonRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public ContactReasonRecordPage ClickSupportMultipleCases_YesOption()
		{
			WaitForElementToBeClickable(SupportMultipleCases_YesOption);
			MoveToElementInPage(SupportMultipleCases_YesOption);
			Click(SupportMultipleCases_YesOption);

			return this;
		}

		public ContactReasonRecordPage ValidateSupportMultipleCases_YesOptionChecked()
		{
			WaitForElement(SupportMultipleCases_YesOption);
			ValidateElementChecked(SupportMultipleCases_YesOption);

			return this;
		}

		public ContactReasonRecordPage ValidateSupportMultipleCases_YesOptionNotChecked()
		{
			WaitForElement(SupportMultipleCases_YesOption);
			ValidateElementNotChecked(SupportMultipleCases_YesOption);

			return this;
		}

		public ContactReasonRecordPage ClickSupportMultipleCases_NoOption()
		{
			WaitForElementToBeClickable(SupportMultipleCases_NoOption);
			MoveToElementInPage(SupportMultipleCases_NoOption);
			Click(SupportMultipleCases_NoOption);

			return this;
		}

		public ContactReasonRecordPage ValidateSupportMultipleCases_NoOptionChecked()
		{
			WaitForElement(SupportMultipleCases_NoOption);
			ValidateElementChecked(SupportMultipleCases_NoOption);

			return this;
		}

		public ContactReasonRecordPage ValidateSupportMultipleCases_NoOptionNotChecked()
		{
			WaitForElement(SupportMultipleCases_NoOption);
			ValidateElementNotChecked(SupportMultipleCases_NoOption);

			return this;
		}

		public ContactReasonRecordPage ValidateBusinessTypePicklistIsDisabled(bool IsDisabled)
		{
			if (IsDisabled)
				ValidateElementDisabled(BusinessType_Picklist);
			else
				ValidateElementNotDisabled(BusinessType_Picklist);

			return this;
		}

		public ContactReasonRecordPage ValidateAdmissionTypePicklistIsDisabled(bool IsDisabled)
		{
			if (IsDisabled)
				ValidateElementDisabled(AdmissionType_Picklist);
			else
				ValidateElementNotDisabled(AdmissionType_Picklist);

			return this;
		}

		public ContactReasonRecordPage ValidateAdmissionTypeFieldVisibility(bool IsVisible)
		{
			if (IsVisible)
			{
				WaitForElementVisible(AdmissionType_Picklist);
				MoveToElementInPage(AdmissionType_Picklist);
			}
			else
            {
				WaitForElementNotVisible(AdmissionType_Picklist, 3);
				WaitForElementNotVisible(AdmissionType_MandatoryField, 3);
			}

			return this;
		}

		public ContactReasonRecordPage ValidateAdmissionType_MandatoryFieldVisibility()
		{
			WaitForElementVisible(AdmissionType_MandatoryField);
			MoveToElementInPage(AdmissionType_MandatoryField);

			return this;
		}

		public ContactReasonRecordPage ValidateAdmissionTypePicklist_FieldOptionIsPresent(string Text)
		{
			WaitForElementVisible(AdmissionType_Picklist);
			ValidatePicklistContainsElementByText(AdmissionType_Picklist, Text);

			return this;
		}

		public ContactReasonRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
		{
			WaitForElement(Name_FieldErrorLabel);
			MoveToElementInPage(Name_FieldErrorLabel);
			ValidateElementText(Name_FieldErrorLabel, ExpectedText);

			return this;
		}
		
		public ContactReasonRecordPage ValidateAdmissionTypeFieldErrorLabelText(string ExpectedText)
		{
			WaitForElement(AdmissionType_FieldErrorLabel);
			MoveToElementInPage(AdmissionType_FieldErrorLabel);
			ValidateElementText(AdmissionType_FieldErrorLabel, ExpectedText);

			return this;
		}

		public ContactReasonRecordPage ValidateNotificationAreaText(string ExpectedText)
		{
			ValidateElementText(notificationMessage, ExpectedText);

			return this;
		}

	}
}
