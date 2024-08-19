using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
	public class ServiceRecordPage : CommonMethods
	{

		public ServiceRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

        #region IFrames
        readonly By ContentIFrame = By.Id("CWContentIFrame");		
		readonly By CareProviderService_CWDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderservice&')]");
		readonly By ServiceRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
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
		readonly By Isscheduledservice_1 = By.XPath("//*[@id='CWField_isscheduledservice_1']");
		readonly By Isscheduledservice_0 = By.XPath("//*[@id='CWField_isscheduledservice_0']");
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
		readonly By ServiceMappingsTab = By.Id("CWNavGroup_ServiceMappings");
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

        public ServiceRecordPage WaitForServiceRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElement(ContentIFrame);
			SwitchToIframe(ContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElement(CareProviderService_CWDialogIFrame);
			SwitchToIframe(CareProviderService_CWDialogIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(ServiceRecordPageHeader);

			return this;
		}

		public ServiceRecordPage NavigateToServiceMappingsTab()
        {
			ScrollToElement(ServiceMappingsTab);
			WaitForElementToBeClickable(ServiceMappingsTab);
			Click(ServiceMappingsTab);

			return this;
		}

		public ServiceRecordPage NavigateToDetailsTab()
		{
			WaitForElementToBeClickable(DetailsTab);
			ScrollToElement(DetailsTab);
			Click(DetailsTab);

			return this;
		}

		public ServiceRecordPage ClickMenuButton()
        {
			WaitForElementToBeClickable(MenuButton);
			ScrollToElement(MenuButton);
			Click(MenuButton);

			return this;
		}

		public ServiceRecordPage NavigateToAuditPage()
		{
			ClickMenuButton();

			WaitForElementToBeClickable(Audit_MenuItem);
			ScrollToElement(Audit_MenuItem);
			Click(Audit_MenuItem);

			return this;
		}

		public ServiceRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			ScrollToElement(BackButton);
			Click(BackButton);

			return this;
		}

		public ServiceRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			ScrollToElement(SaveButton);
			Click(SaveButton);

			return this;
		}

		public ServiceRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			ScrollToElement(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public ServiceRecordPage ClickDeleteButton()
		{
			ScrollToElement(DeleteRecordButton);
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}


		public ServiceRecordPage WaitForRecordToBeSaved()
		{
			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElement(ServiceRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);
			WaitForElementVisible(ServiceMappingsTab);

			return this;
		}

		public ServiceRecordPage ValidateServiceRecordTitle(string ExpectedText)
        {
			ScrollToElement(ServiceRecordPageHeader);
			WaitForElementVisible(ServiceRecordPageHeader);
			ValidateElementByTitle(ServiceRecordPageHeader, "Service: " + ExpectedText);
			return this;
        }

		public ServiceRecordPage ValidateMandatoryFieldIsDisplayed(string FieldName)
		{
			WaitForElementVisible(MandatoryField_Label(FieldName));			
			bool ActualDisplayed = GetElementVisibility(MandatoryField_Label(FieldName));
			Assert.IsTrue(ActualDisplayed);

			return this;
		}

		public ServiceRecordPage ValidateFormErrorNotificationMessageIsDisplayed(string ExpectedText)
		{
			ScrollToElement(FormNotificationMessage);
			WaitForElementVisible(FormNotificationMessage);			
			ValidateElementText(FormNotificationMessage, ExpectedText);
			return this;
		}

		public ServiceRecordPage ValidateNameMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(NameField_FormErrorMessage);
			ValidateElementByTitle(NameField_FormErrorMessage, ExpectedText);
			return this;
		}

		public ServiceRecordPage ValidateResponsibleTeamMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(ResponsibleTeamField_FormErrorMessage);
			ValidateElementByTitle(ResponsibleTeamField_FormErrorMessage, ExpectedText);
			return this;
		}

		public ServiceRecordPage ValidateStartDateMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(StartDateField_FormErrorMessage);
			ValidateElementByTitle(StartDateField_FormErrorMessage, ExpectedText);
			return this;
		}

		public ServiceRecordPage ValidateNameFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Name);
			else
				WaitForElementNotVisible(Name, 3);
			bool ActualDisplayed = GetElementVisibility(Name);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceRecordPage ValidateNameText(string ExpectedText)
		{
			ScrollToElement(Name);
			ValidateElementValue(Name, ExpectedText);

			return this;
		}

		public ServiceRecordPage InsertTextOnName(string TextToInsert)
		{
			WaitForElementToBeClickable(Name);
			ScrollToElement(Name);
			SendKeys(Name, TextToInsert);
			
			return this;
		}

		public ServiceRecordPage ValidateCodeText(string ExpectedText)
		{
			ScrollToElement(Code);
			ValidateElementValue(Code, ExpectedText);

			return this;
		}

		public ServiceRecordPage ValidateCodeFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Code);
			else
				WaitForElementNotVisible(Code, 3);
			bool ActualDisplayed = GetElementVisibility(Code);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceRecordPage InsertTextOnCode(string TextToInsert)
		{
			WaitForElementToBeClickable(Code);
			ScrollToElement(Code);
			SendKeys(Code, TextToInsert);
			
			return this;
		}

		public ServiceRecordPage ValidateGovCodeText(string ExpectedText)
		{
			ScrollToElement(GovCode);
			ValidateElementValue(GovCode, ExpectedText);

			return this;
		}

		public ServiceRecordPage InsertTextOnGovCode(string TextToInsert)
		{
			ScrollToElement(GovCode);
			WaitForElementToBeClickable(GovCode);
			SendKeys(GovCode, TextToInsert);
			
			return this;
		}

		public ServiceRecordPage ValidateGovCodeFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(GovCode);
			else
				WaitForElementNotVisible(GovCode, 3);
			bool ActualDisplayed = GetElementVisibility(GovCode);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceRecordPage ClickIsscheduledservice_YesOption()
		{
			WaitForElementToBeClickable(Isscheduledservice_1);
			ScrollToElement(Isscheduledservice_1);
			Click(Isscheduledservice_1);

			return this;
		}

		public ServiceRecordPage ValidateIsscheduledservice_YesOptionChecked()
		{
			WaitForElement(Isscheduledservice_1);
			ScrollToElement(Isscheduledservice_1);
			ValidateElementChecked(Isscheduledservice_1);
			
			return this;
		}

		public ServiceRecordPage ValidateIsscheduledservice_YesOptionNotChecked()
		{
			WaitForElement(Isscheduledservice_1);
			ScrollToElement(Isscheduledservice_1);
			ValidateElementNotChecked(Isscheduledservice_1);
			
			return this;
		}

		public ServiceRecordPage ValidateScheduledServiceYesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Isscheduledservice_1);
			else
				WaitForElementNotVisible(Isscheduledservice_1, 3);
			bool ActualDisplayed = GetElementVisibility(Isscheduledservice_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceRecordPage ClickIsscheduledservice_NoOption()
		{
			WaitForElementToBeClickable(Isscheduledservice_0);
			ScrollToElement(Isscheduledservice_0);
			Click(Isscheduledservice_0);

			return this;
		}

		public ServiceRecordPage ValidateIsscheduledservice_NoOptionChecked()
		{
			WaitForElement(Isscheduledservice_0);
			ScrollToElement(Isscheduledservice_0);
			ValidateElementChecked(Isscheduledservice_0);
			
			return this;
		}

		public ServiceRecordPage ValidateIsscheduledservice_NoOptionNotChecked()
		{
			WaitForElement(Isscheduledservice_0);
			ScrollToElement(Isscheduledservice_0);
			ValidateElementNotChecked(Isscheduledservice_0);
			
			return this;
		}

		public ServiceRecordPage ValidateScheduledServiceNoOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Isscheduledservice_0);
			else
				WaitForElementNotVisible(Isscheduledservice_0, 3);
			bool ActualDisplayed = GetElementVisibility(Isscheduledservice_0);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}


		public ServiceRecordPage ClickInactive_YesOption()
		{
			WaitForElementToBeClickable(Inactive_1);
			ScrollToElement(Inactive_1);
			Click(Inactive_1);

			return this;
		}

		public ServiceRecordPage ValidateInactive_YesOptionChecked()
		{
			WaitForElement(Inactive_1);
			ScrollToElement(Inactive_1);
			ValidateElementChecked(Inactive_1);
			
			return this;
		}

		public ServiceRecordPage ValidateInactive_YesOptionNotChecked()
		{
			WaitForElement(Inactive_1);
			ScrollToElement(Inactive_1);
			ValidateElementNotChecked(Inactive_1);
			
			return this;
		}

		public ServiceRecordPage ValidateInactiveYesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Inactive_1);
			else
				WaitForElementNotVisible(Inactive_1, 3);
			bool ActualDisplayed = GetElementVisibility(Inactive_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceRecordPage ClickInactive_NoOption()
		{
			WaitForElementToBeClickable(Inactive_0);
			ScrollToElement(Inactive_0);
			Click(Inactive_0);

			return this;
		}

		public ServiceRecordPage ValidateInactive_NoOptionChecked()
		{
			WaitForElement(Inactive_0);
			ScrollToElement(Inactive_0);
			ValidateElementChecked(Inactive_0);
			
			return this;
		}

		public ServiceRecordPage ValidateInactive_NoOptionNotChecked()
		{
			WaitForElement(Inactive_0);
			ScrollToElement(Inactive_0);
			ValidateElementNotChecked(Inactive_0);
			
			return this;
		}

		public ServiceRecordPage ValidateInactiveNoOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Inactive_0);
			else
				WaitForElementNotVisible(Inactive_0, 3);
			bool ActualDisplayed = GetElementVisibility(Inactive_0);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public ServiceRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			ValidateElementByTitle(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public ServiceRecordPage ValidateResponsibleTeamLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ResponsibleTeamLookupButton);
			else
				WaitForElementNotVisible(ResponsibleTeamLookupButton, 3);
			bool ActualDisplayed = GetElementVisibility(ResponsibleTeamLookupButton);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(ResponsibleTeamLookupButton);
			else
				ValidateElementNotDisabled(ResponsibleTeamLookupButton);

			return this;
		}

		public ServiceRecordPage ClickResponsibleTeamClearButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamClearButton);
			ScrollToElement(ResponsibleTeamClearButton);
			Click(ResponsibleTeamClearButton);

			return this;
		}

		public ServiceRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			ScrollToElement(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public ServiceRecordPage ValidateStartDateText(string ExpectedText)
		{
			ScrollToElement(StartDate);
			ValidateElementValue(StartDate, ExpectedText);

			return this;
		}

		public ServiceRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(StartDate);
			ScrollToElement(StartDate);
			SendKeys(StartDate, TextToInsert);
			
			return this;
		}

		public ServiceRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartDate_DatePicker);
			ScrollToElement(StartDate_DatePicker);
			Click(StartDate_DatePicker);

			return this;
		}

		public ServiceRecordPage ValidateStartDateFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(StartDate);
			else
				WaitForElementNotVisible(StartDate, 3);
			bool ActualDisplayed = GetElementVisibility(StartDate);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceRecordPage ValidateEndDateText(string ExpectedText)
		{
			ScrollToElement(EndDate);
			ValidateElementValue(EndDate, ExpectedText);

			return this;
		}

		public ServiceRecordPage InsertTextOnEnddate(string TextToInsert)
		{
			WaitForElementToBeClickable(EndDate);
			ScrollToElement(EndDate);
			SendKeys(EndDate, TextToInsert);
			
			return this;
		}

		public ServiceRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EndDate_DatePicker);
			ScrollToElement(EndDate_DatePicker);
			Click(EndDate_DatePicker);

			return this;
		}

		public ServiceRecordPage ValidateEndDateFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(EndDate);
			else
				WaitForElementNotVisible(EndDate, 3);
			bool ActualDisplayed = GetElementVisibility(EndDate);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceRecordPage ClickValidforexport_YesOption()
		{
			WaitForElementToBeClickable(Validforexport_1);
			ScrollToElement(Validforexport_1);
			Click(Validforexport_1);

			return this;
		}

		public ServiceRecordPage ValidateValidforexport_YesOptionChecked()
		{
			WaitForElement(Validforexport_1);
			ScrollToElement(Validforexport_1);
			ValidateElementChecked(Validforexport_1);
			
			return this;
		}

		public ServiceRecordPage ValidateValidforexport_YesOptionNotChecked()
		{
			WaitForElement(Validforexport_1);
			ScrollToElement(Validforexport_1);
			ValidateElementNotChecked(Validforexport_1);
			
			return this;
		}

		public ServiceRecordPage ValidateValidForExportYesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Validforexport_1);
			else
				WaitForElementNotVisible(Validforexport_1, 3);
			bool ActualDisplayed = GetElementVisibility(Validforexport_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public ServiceRecordPage ClickValidforexport_NoOption()
		{
			WaitForElementToBeClickable(Validforexport_0);
			ScrollToElement(Validforexport_0);
			Click(Validforexport_0);

			return this;
		}

		public ServiceRecordPage ValidateValidforexport_NoOptionChecked()
		{
			WaitForElement(Validforexport_0);
			ScrollToElement(Validforexport_0);
			ValidateElementChecked(Validforexport_0);
			
			return this;
		}

		public ServiceRecordPage ValidateValidforexport_NoOptionNotChecked()
		{
			WaitForElement(Validforexport_0);
			ScrollToElement(Validforexport_0);
			ValidateElementNotChecked(Validforexport_0);
			
			return this;
		}

		public ServiceRecordPage ValidateValidForExportNoOptionIsDisplayed(bool ExpectedDisplayed)
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
