using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
	public class ServiceDetailsRecordPage : CommonMethods
	{

		public ServiceDetailsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		#region IFrames
		readonly By ContentIFrame = By.Id("CWContentIFrame");
		readonly By CareProviderServiceDetail_CWDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderservicedetail&')]");
		readonly By ServiceDetailsRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
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
		readonly By Name = By.XPath("//*[@id='CWField_name']");
		readonly By Code = By.XPath("//*[@id='CWField_code']");
		readonly By GovCode = By.XPath("//*[@id='CWField_govcode']");
		readonly By Inactive_1 = By.XPath("//*[@id='CWField_inactive_1']");
		readonly By Inactive_0 = By.XPath("//*[@id='CWField_inactive_0']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By StartDate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartDate_DatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By EndDate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EndDate_DatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
		readonly By Validforexport_1 = By.XPath("//*[@id='CWField_validforexport_1']");
		readonly By Validforexport_0 = By.XPath("//*[@id='CWField_validforexport_0']");
		#endregion

		#region Tabs
		readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

		#endregion

		#region Warning notifications
		readonly By FormNotificationMessage = By.Id("CWNotificationMessage_DataForm");
		readonly By NameField_FormErrorMessage = By.XPath("//*[@id = 'CWField_name']/following-sibling::*[@class = 'formerror']/span");
		readonly By ResponsibleTeamField_FormErrorMessage = By.XPath("//*[@for = 'CWField_ownerid'][@class = 'formerror']/span");
		readonly By StartDateField_FormErrorMessage = By.XPath("//*[@for = 'CWField_startdate'][@class = 'formerror']/span");
		#endregion

		#region Menu Items
		readonly By MenuButton = By.Id("CWNavGroup_Menu");
		readonly By Audit_MenuItem = By.Id("CWNavItem_AuditHistory");

		#endregion

		public ServiceDetailsRecordPage WaitForServiceDetailsRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElement(ContentIFrame);
			SwitchToIframe(ContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElement(CareProviderServiceDetail_CWDialogIFrame);
			SwitchToIframe(CareProviderServiceDetail_CWDialogIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(ServiceDetailsRecordPageHeader);

			return this;
		}

		public ServiceDetailsRecordPage NavigateToDetailsTab()
		{
			WaitForElementToBeClickable(DetailsTab);
			ScrollToElement(DetailsTab);
			Click(DetailsTab);

			return this;
		}

		public ServiceDetailsRecordPage ClickMenuButton()
		{
			WaitForElementToBeClickable(MenuButton);
			ScrollToElement(MenuButton);
			Click(MenuButton);

			return this;
		}

		public ServiceDetailsRecordPage NavigateToAuditPage()
		{
			ClickMenuButton();

			WaitForElementToBeClickable(Audit_MenuItem);
			ScrollToElement(Audit_MenuItem);
			Click(Audit_MenuItem);

			return this;
		}

		public ServiceDetailsRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			ScrollToElement(BackButton);
			Click(BackButton);

			return this;
		}

		public ServiceDetailsRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			ScrollToElement(SaveButton);
			Click(SaveButton);

			return this;
		}

		public ServiceDetailsRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			ScrollToElement(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public ServiceDetailsRecordPage ClickDeleteButton()
		{
			ScrollToElement(DeleteRecordButton);
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}


		public ServiceDetailsRecordPage WaitForRecordToBeSaved()
		{
			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElement(ServiceDetailsRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);

			return this;
		}

		public ServiceDetailsRecordPage ValidateServiceDetailsRecordTitle(string ExpectedText)
		{
			ScrollToElement(ServiceDetailsRecordPageHeader);
			WaitForElementVisible(ServiceDetailsRecordPageHeader);
			ValidateElementByTitle(ServiceDetailsRecordPageHeader, "Service Detail: " + ExpectedText);
			return this;
		}

		public ServiceDetailsRecordPage ValidateMandatoryFieldIsDisplayed(string FieldName)
		{
			WaitForElementVisible(MandatoryField_Label(FieldName));
			bool ActualDisplayed = GetElementVisibility(MandatoryField_Label(FieldName));
			Assert.IsTrue(ActualDisplayed);

			return this;
		}

		public ServiceDetailsRecordPage ValidateFormErrorNotificationMessageIsDisplayed(string ExpectedText)
		{
			ScrollToElement(FormNotificationMessage);
			WaitForElementVisible(FormNotificationMessage);
			ValidateElementText(FormNotificationMessage, ExpectedText);
			return this;
		}

		public ServiceDetailsRecordPage ValidateNameMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(NameField_FormErrorMessage);
			ValidateElementByTitle(NameField_FormErrorMessage, ExpectedText);
			return this;
		}

		public ServiceDetailsRecordPage ValidateResponsibleTeamMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(ResponsibleTeamField_FormErrorMessage);
			ValidateElementByTitle(ResponsibleTeamField_FormErrorMessage, ExpectedText);
			return this;
		}

		public ServiceDetailsRecordPage ValidateStartDateMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(StartDateField_FormErrorMessage);
			ValidateElementByTitle(StartDateField_FormErrorMessage, ExpectedText);
			return this;
		}

		public ServiceDetailsRecordPage ValidateNameFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Name);
			else
				WaitForElementNotVisible(Name, 3);
			bool ActualDisplayed = GetElementVisibility(Name);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceDetailsRecordPage ValidateNameText(string ExpectedText)
		{
			ScrollToElement(Name);
			ValidateElementValue(Name, ExpectedText);

			return this;
		}

		public ServiceDetailsRecordPage InsertTextOnName(string TextToInsert)
		{
			WaitForElementToBeClickable(Name);
			ScrollToElement(Name);
			SendKeys(Name, TextToInsert);

			return this;
		}

		public ServiceDetailsRecordPage ValidateCodeText(string ExpectedText)
		{
			ScrollToElement(Code);
			ValidateElementValue(Code, ExpectedText);

			return this;
		}

		public ServiceDetailsRecordPage ValidateCodeFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Code);
			else
				WaitForElementNotVisible(Code, 3);
			bool ActualDisplayed = GetElementVisibility(Code);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceDetailsRecordPage InsertTextOnCode(string TextToInsert)
		{
			WaitForElementToBeClickable(Code);
			ScrollToElement(Code);
			SendKeys(Code, TextToInsert);

			return this;
		}

		public ServiceDetailsRecordPage ValidateGovCodeText(string ExpectedText)
		{
			ScrollToElement(GovCode);
			ValidateElementValue(GovCode, ExpectedText);

			return this;
		}

		public ServiceDetailsRecordPage InsertTextOnGovCode(string TextToInsert)
		{
			ScrollToElement(GovCode);
			WaitForElementToBeClickable(GovCode);
			SendKeys(GovCode, TextToInsert);

			return this;
		}

		public ServiceDetailsRecordPage ValidateGovCodeFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(GovCode);
			else
				WaitForElementNotVisible(GovCode, 3);
			bool ActualDisplayed = GetElementVisibility(GovCode);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceDetailsRecordPage ClickInactive_YesOption()
		{
			WaitForElementToBeClickable(Inactive_1);
			ScrollToElement(Inactive_1);
			Click(Inactive_1);

			return this;
		}

		public ServiceDetailsRecordPage ValidateInactive_YesOptionChecked()
		{
			WaitForElement(Inactive_1);
			ScrollToElement(Inactive_1);
			ValidateElementChecked(Inactive_1);

			return this;
		}

		public ServiceDetailsRecordPage ValidateInactive_YesOptionNotChecked()
		{
			WaitForElement(Inactive_1);
			ScrollToElement(Inactive_1);
			ValidateElementNotChecked(Inactive_1);

			return this;
		}

		public ServiceDetailsRecordPage ValidateInactiveYesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Inactive_1);
			else
				WaitForElementNotVisible(Inactive_1, 3);
			bool ActualDisplayed = GetElementVisibility(Inactive_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceDetailsRecordPage ClickInactive_NoOption()
		{
			WaitForElementToBeClickable(Inactive_0);
			ScrollToElement(Inactive_0);
			Click(Inactive_0);

			return this;
		}

		public ServiceDetailsRecordPage ValidateInactive_NoOptionChecked()
		{
			WaitForElement(Inactive_0);
			ScrollToElement(Inactive_0);
			ValidateElementChecked(Inactive_0);

			return this;
		}

		public ServiceDetailsRecordPage ValidateInactive_NoOptionNotChecked()
		{
			WaitForElement(Inactive_0);
			ScrollToElement(Inactive_0);
			ValidateElementNotChecked(Inactive_0);

			return this;
		}

		public ServiceDetailsRecordPage ValidateInactiveNoOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Inactive_0);
			else
				WaitForElementNotVisible(Inactive_0, 3);
			bool ActualDisplayed = GetElementVisibility(Inactive_0);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceDetailsRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public ServiceDetailsRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			ValidateElementByTitle(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public ServiceDetailsRecordPage ValidateResponsibleTeamLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ResponsibleTeamLookupButton);
			else
				WaitForElementNotVisible(ResponsibleTeamLookupButton, 3);
			bool ActualDisplayed = GetElementVisibility(ResponsibleTeamLookupButton);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceDetailsRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(ResponsibleTeamLookupButton);
			else
				ValidateElementNotDisabled(ResponsibleTeamLookupButton);

			return this;
		}

		public ServiceDetailsRecordPage ClickResponsibleTeamClearButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamClearButton);
			ScrollToElement(ResponsibleTeamClearButton);
			Click(ResponsibleTeamClearButton);

			return this;
		}

		public ServiceDetailsRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			ScrollToElement(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public ServiceDetailsRecordPage ValidateStartDateText(string ExpectedText)
		{
			ScrollToElement(StartDate);
			ValidateElementValue(StartDate, ExpectedText);

			return this;
		}

		public ServiceDetailsRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate);
			ScrollToElement(StartDate);
			SendKeys(StartDate, TextToInsert);

			return this;
		}

		public ServiceDetailsRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartDate_DatePicker);
			ScrollToElement(StartDate_DatePicker);
			Click(StartDate_DatePicker);

			return this;
		}

		public ServiceDetailsRecordPage ValidateStartDateFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(StartDate);
			else
				WaitForElementNotVisible(StartDate, 3);
			bool ActualDisplayed = GetElementVisibility(StartDate);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceDetailsRecordPage ValidateEndDateText(string ExpectedText)
		{
			ScrollToElement(EndDate);
			ValidateElementValue(EndDate, ExpectedText);

			return this;
		}

		public ServiceDetailsRecordPage InsertTextOnEnddate(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate);
			ScrollToElement(EndDate);
			SendKeys(EndDate, TextToInsert);

			return this;
		}

		public ServiceDetailsRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EndDate_DatePicker);
			ScrollToElement(EndDate_DatePicker);
			Click(EndDate_DatePicker);

			return this;
		}

		public ServiceDetailsRecordPage ValidateEndDateFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(EndDate);
			else
				WaitForElementNotVisible(EndDate, 3);
			bool ActualDisplayed = GetElementVisibility(EndDate);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceDetailsRecordPage ClickValidforexport_YesOption()
		{
			WaitForElementToBeClickable(Validforexport_1);
			ScrollToElement(Validforexport_1);
			Click(Validforexport_1);

			return this;
		}

		public ServiceDetailsRecordPage ValidateValidforexport_YesOptionChecked()
		{
			WaitForElement(Validforexport_1);
			ScrollToElement(Validforexport_1);
			ValidateElementChecked(Validforexport_1);

			return this;
		}

		public ServiceDetailsRecordPage ValidateValidforexport_YesOptionNotChecked()
		{
			WaitForElement(Validforexport_1);
			ScrollToElement(Validforexport_1);
			ValidateElementNotChecked(Validforexport_1);

			return this;
		}

		public ServiceDetailsRecordPage ValidateValidForExportYesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Validforexport_1);
			else
				WaitForElementNotVisible(Validforexport_1, 3);
			bool ActualDisplayed = GetElementVisibility(Validforexport_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceDetailsRecordPage ClickValidforexport_NoOption()
		{
			WaitForElementToBeClickable(Validforexport_0);
			ScrollToElement(Validforexport_0);
			Click(Validforexport_0);

			return this;
		}

		public ServiceDetailsRecordPage ValidateValidforexport_NoOptionChecked()
		{
			WaitForElement(Validforexport_0);
			ScrollToElement(Validforexport_0);
			ValidateElementChecked(Validforexport_0);

			return this;
		}

		public ServiceDetailsRecordPage ValidateValidforexport_NoOptionNotChecked()
		{
			WaitForElement(Validforexport_0);
			ScrollToElement(Validforexport_0);
			ValidateElementNotChecked(Validforexport_0);

			return this;
		}

		public ServiceDetailsRecordPage ValidateValidForExportNoOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Validforexport_0);
			else
				WaitForElementNotVisible(Validforexport_0, 3);
			bool ActualDisplayed = GetElementVisibility(Validforexport_0);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}
	}
}
