using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
	public class CPServiceMappingRecordPage : CommonMethods
	{

		public CPServiceMappingRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		#region IFrames

		readonly By ContentIFrame = By.Id("CWContentIFrame");
		readonly By CareProviderServiceMapping_CWDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderservicemapping&')]");
		readonly By CPServiceMappingRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

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

		readonly By ServiceFieldLink = By.XPath("//*[@id='CWField_serviceid_Link']");
		readonly By ServiceFieldClearButton = By.XPath("//*[@id='CWClearLookup_serviceid']");
		readonly By ServiceFieldLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceid']");

		readonly By ServiceDetailFieldLink = By.XPath("//*[@id='CWField_servicedetailid_Link']");
		readonly By ServiceDetailFieldClearButton = By.XPath("//*[@id='CWClearLookup_servicedetailid']");
		readonly By ServiceDetailFieldLookupButton = By.XPath("//*[@id='CWLookupBtn_servicedetailid']");

		readonly By BookingTypeFieldLink = By.XPath("//*[@id='CWField_bookingtypeid_Link']");
		readonly By BookingTypeFieldClearButton = By.XPath("//*[@id='CWClearLookup_bookingtypeid']");
		readonly By BookingTypeFieldLookupButton = By.XPath("//*[@id='CWLookupBtn_bookingtypeid']");

		readonly By FinanceCodeFieldLink = By.XPath("//*[@id='CWField_financecodeid_Link']");
		readonly By FinanceCodeFieldClearButton = By.XPath("//*[@id='CWClearLookup_financecodeid']");
		readonly By FinanceCodeFieldLookupButton = By.XPath("//*[@id='CWLookupBtn_financecodeid']");

		readonly By Inactive_1 = By.XPath("//*[@id='CWField_inactive_1']");
		readonly By Inactive_0 = By.XPath("//*[@id='CWField_inactive_0']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By NoteText_TextField = By.XPath("//*[@id='CWField_notetext']");

		#endregion

		#region Tabs

		readonly By DetailsTab = By.Id("CWNavGroup_EditForm");

		#endregion

		#region Warning notifications

		readonly By FormNotificationMessage = By.Id("CWNotificationMessage_DataForm");
		readonly By ServiceField_FormErrorMessage = By.XPath("//*[@for = 'CWField_serviceid'][@class = 'formerror']/span");
		readonly By ServiceDetailField_FormErrorMessage = By.XPath("//*[@for = 'CWField_servicedetailid'][@class = 'formerror']/span");
		readonly By BookingTypeField_FormErrorMessage = By.XPath("//*[@for = 'CWField_bookingtypeid'][@class = 'formerror']/span");
		readonly By ResponsibleTeamField_FormErrorMessage = By.XPath("//*[@for = 'CWField_ownerid'][@class = 'formerror']/span");
		
		#endregion

		#region Menu Items

		readonly By MenuButton = By.Id("CWNavGroup_Menu");
		readonly By Audit_MenuItem = By.Id("CWNavItem_AuditHistory");

		#endregion

		By cwDialog_IFrame(string parentRecordIdSuffix) => By.XPath("//iframe[contains(@id, 'iframe_CWDialog_" + parentRecordIdSuffix + "')]");
		readonly By careProviderServiceMappingFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderservicemapping&')]");
		readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");

		public CPServiceMappingRecordPage WaitForCPServiceMappingRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElement(ContentIFrame);
			SwitchToIframe(ContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElement(CareProviderServiceMapping_CWDialogIFrame);
			SwitchToIframe(CareProviderServiceMapping_CWDialogIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElementVisible(CPServiceMappingRecordPageHeader);

			return this;
		}

		public CPServiceMappingRecordPage WaitForCPServiceMappingRecordPageToLoadFromAdvancedSearch()
		{
			SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElement(ContentIFrame);
			SwitchToIframe(ContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElement(iframe_CWDataFormDialog);
			SwitchToIframe(iframe_CWDataFormDialog);

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElement(careProviderServiceMappingFrame);
			SwitchToIframe(careProviderServiceMappingFrame);

			WaitForElementNotVisible("CWRefreshPanel", 20);
			return this;
		}

		public CPServiceMappingRecordPage WaitForCPServiceMappingRecordPageToLoadFromAdvancedSearch(string frameId)
		{
			SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElement(ContentIFrame);
			SwitchToIframe(ContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElement(iframe_CWDataFormDialog);
			SwitchToIframe(iframe_CWDataFormDialog);

			WaitForElementNotVisible("CWRefreshPanel", 20);

			WaitForElement(cwDialog_IFrame(frameId));
			SwitchToIframe(cwDialog_IFrame(frameId));

			WaitForElementNotVisible("CWRefreshPanel", 20);
			return this;
		}

		public CPServiceMappingRecordPage NavigateToDetailsTab()
		{
			WaitForElementToBeClickable(DetailsTab);
			ScrollToElement(DetailsTab);
			Click(DetailsTab);

			return this;
		}

		public CPServiceMappingRecordPage ClickMenuButton()
		{
			WaitForElementToBeClickable(MenuButton);
			ScrollToElement(MenuButton);
			Click(MenuButton);

			return this;
		}

		public CPServiceMappingRecordPage NavigateToAuditPage()
		{
			ClickMenuButton();

			WaitForElementToBeClickable(Audit_MenuItem);
			ScrollToElement(Audit_MenuItem);
			Click(Audit_MenuItem);

			return this;
		}

		public CPServiceMappingRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			ScrollToElement(BackButton);
			Click(BackButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			ScrollToElement(SaveButton);
			Click(SaveButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			ScrollToElement(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickDeleteButton()
		{
			ScrollToElement(DeleteRecordButton);
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}


		public CPServiceMappingRecordPage WaitForRecordToBeSaved()
		{
			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElement(CPServiceMappingRecordPageHeader);
			WaitForElementVisible(SaveButton);
			WaitForElementVisible(SaveAndCloseButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);

			return this;
		}

		public CPServiceMappingRecordPage ValidateServiceMappingRecordTitle(string ExpectedText)
		{
			ScrollToElement(CPServiceMappingRecordPageHeader);
			WaitForElementVisible(CPServiceMappingRecordPageHeader);
			ValidateElementByTitle(CPServiceMappingRecordPageHeader, "Service Mapping: " + ExpectedText);
			return this;
		}

		public CPServiceMappingRecordPage ValidateMandatoryFieldIsDisplayed(string FieldName)
		{
			WaitForElementVisible(MandatoryField_Label(FieldName));
			bool ActualDisplayed = GetElementVisibility(MandatoryField_Label(FieldName));
			Assert.IsTrue(ActualDisplayed);

			return this;
		}

		public CPServiceMappingRecordPage ValidateFormErrorNotificationMessageIsDisplayed(string ExpectedText)
		{
			ScrollToElement(FormNotificationMessage);
			WaitForElementVisible(FormNotificationMessage);
			ValidateElementText(FormNotificationMessage, ExpectedText);
			return this;
		}

		public CPServiceMappingRecordPage ValidateServiceMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(ServiceField_FormErrorMessage);
			ValidateElementByTitle(ServiceField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPServiceMappingRecordPage ValidateResponsibleTeamMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(ResponsibleTeamField_FormErrorMessage);
			ValidateElementByTitle(ResponsibleTeamField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPServiceMappingRecordPage ValidateServiceDetailsMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(ServiceDetailField_FormErrorMessage);
			ValidateElementByTitle(ServiceDetailField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPServiceMappingRecordPage ValidateBookingTypeMandatoryFieldNotificationMessageIsDisplayed(string ExpectedText)
		{
			WaitForElementVisible(BookingTypeField_FormErrorMessage);
			ValidateElementByTitle(BookingTypeField_FormErrorMessage, ExpectedText);
			return this;
		}

		public CPServiceMappingRecordPage ValidateServiceFieldLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ServiceFieldLookupButton);
			else
				WaitForElementNotVisible(ServiceFieldLookupButton, 3);
			bool ActualDisplayed = GetElementVisibility(ServiceFieldLookupButton);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPServiceMappingRecordPage ClickServiceFieldLink()
		{
			WaitForElementToBeClickable(ServiceFieldLink);
			ScrollToElement(ServiceFieldLink);
			Click(ServiceFieldLink);

			return this;
		}

		public CPServiceMappingRecordPage ValidateServiceLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ServiceFieldLink);
			ScrollToElement(ServiceFieldLink);
			ValidateElementByTitle(ServiceFieldLink, ExpectedText);

			return this;
		}

		public CPServiceMappingRecordPage ValidateServiceLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ServiceFieldLookupButton);
			else
				WaitForElementNotVisible(ServiceFieldLookupButton, 3);
			bool ActualDisplayed = GetElementVisibility(ServiceFieldLookupButton);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPServiceMappingRecordPage ValidateServiceLookupButtonIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(ServiceFieldLookupButton);
			else
				ValidateElementNotDisabled(ServiceFieldLookupButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickServiceClearButton()
		{
			WaitForElementToBeClickable(ServiceFieldClearButton);
			ScrollToElement(ServiceFieldClearButton);
			Click(ServiceFieldClearButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickServiceLookupButton()
		{
			WaitForElementToBeClickable(ServiceFieldLookupButton);
			ScrollToElement(ServiceFieldLookupButton);
			Click(ServiceFieldLookupButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickInactive_YesOption()
		{
			WaitForElementToBeClickable(Inactive_1);
			ScrollToElement(Inactive_1);
			Click(Inactive_1);

			return this;
		}

		public CPServiceMappingRecordPage ValidateInactive_YesOptionChecked()
		{
			WaitForElement(Inactive_1);
			ScrollToElement(Inactive_1);
			ValidateElementChecked(Inactive_1);

			return this;
		}

		public CPServiceMappingRecordPage ValidateInactive_YesOptionNotChecked()
		{
			WaitForElement(Inactive_1);
			ScrollToElement(Inactive_1);
			ValidateElementNotChecked(Inactive_1);

			return this;
		}

		public CPServiceMappingRecordPage ValidateInactiveYesOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Inactive_1);
			else
				WaitForElementNotVisible(Inactive_1, 3);
			bool ActualDisplayed = GetElementVisibility(Inactive_1);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPServiceMappingRecordPage ClickInactive_NoOption()
		{
			WaitForElementToBeClickable(Inactive_0);
			ScrollToElement(Inactive_0);
			Click(Inactive_0);

			return this;
		}

		public CPServiceMappingRecordPage ValidateInactive_NoOptionChecked()
		{
			WaitForElement(Inactive_0);
			ScrollToElement(Inactive_0);
			ValidateElementChecked(Inactive_0);

			return this;
		}

		public CPServiceMappingRecordPage ValidateInactive_NoOptionNotChecked()
		{
			WaitForElement(Inactive_0);
			ScrollToElement(Inactive_0);
			ValidateElementNotChecked(Inactive_0);

			return this;
		}

		public CPServiceMappingRecordPage ValidateInactiveNoOptionIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(Inactive_0);
			else
				WaitForElementNotVisible(Inactive_0, 3);
			bool ActualDisplayed = GetElementVisibility(Inactive_0);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPServiceMappingRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public CPServiceMappingRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ScrollToElement(ResponsibleTeamLink);
			ValidateElementByTitle(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public CPServiceMappingRecordPage ValidateResponsibleTeamLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ResponsibleTeamLookupButton);
			else
				WaitForElementNotVisible(ResponsibleTeamLookupButton, 3);
			bool ActualDisplayed = GetElementVisibility(ResponsibleTeamLookupButton);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPServiceMappingRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(ResponsibleTeamLookupButton);
			else
				ValidateElementNotDisabled(ResponsibleTeamLookupButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickResponsibleTeamClearButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamClearButton);
			ScrollToElement(ResponsibleTeamClearButton);
			Click(ResponsibleTeamClearButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			ScrollToElement(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickServiceDetailLink()
		{
			WaitForElementToBeClickable(ServiceDetailFieldLink);
			ScrollToElement(ServiceDetailFieldLink);
			Click(ServiceDetailFieldLink);

			return this;
		}

		public CPServiceMappingRecordPage ValidateServiceDetailLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ServiceDetailFieldLink);
			ScrollToElement(ServiceDetailFieldLink);
			ValidateElementByTitle(ServiceDetailFieldLink, ExpectedText);

			return this;
		}

		public CPServiceMappingRecordPage ValidateServiceDetailLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(ServiceDetailFieldLookupButton);
			else
				WaitForElementNotVisible(ServiceDetailFieldLookupButton, 3);
			bool ActualDisplayed = GetElementVisibility(ServiceDetailFieldLookupButton);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPServiceMappingRecordPage ValidateServiceDetailLookupButtonIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(ServiceDetailFieldLookupButton);
			else
				ValidateElementNotDisabled(ServiceDetailFieldLookupButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickServiceDetailClearButton()
		{
			WaitForElementToBeClickable(ServiceDetailFieldClearButton);
			ScrollToElement(ServiceDetailFieldClearButton);
			Click(ServiceDetailFieldClearButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickServiceDetailLookupButton()
		{
			WaitForElementToBeClickable(ServiceDetailFieldLookupButton);
			ScrollToElement(ServiceDetailFieldLookupButton);
			Click(ServiceDetailFieldLookupButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickBookingTypeLink()
		{
			WaitForElementToBeClickable(BookingTypeFieldLink);
			ScrollToElement(BookingTypeFieldLink);
			Click(BookingTypeFieldLink);

			return this;
		}

		public CPServiceMappingRecordPage ValidateBookingTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(BookingTypeFieldLink);
			ScrollToElement(BookingTypeFieldLink);
			ValidateElementByTitle(BookingTypeFieldLink, ExpectedText);

			return this;
		}

		public CPServiceMappingRecordPage ValidateBookingTypeLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(BookingTypeFieldLookupButton);
			else
				WaitForElementNotVisible(BookingTypeFieldLookupButton, 3);
			bool ActualDisplayed = GetElementVisibility(BookingTypeFieldLookupButton);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPServiceMappingRecordPage ValidateBookingTypeLookupButtonIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(BookingTypeFieldLookupButton);
			else
				ValidateElementNotDisabled(BookingTypeFieldLookupButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickBookingTypeClearButton()
		{
			WaitForElementToBeClickable(BookingTypeFieldClearButton);
			ScrollToElement(BookingTypeFieldClearButton);
			Click(BookingTypeFieldClearButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickBookingTypeLookupButton()
		{
			WaitForElementToBeClickable(BookingTypeFieldLookupButton);
			ScrollToElement(BookingTypeFieldLookupButton);
			Click(BookingTypeFieldLookupButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickFinanceCodeLink()
		{
			WaitForElementToBeClickable(FinanceCodeFieldLink);
			ScrollToElement(FinanceCodeFieldLink);
			Click(FinanceCodeFieldLink);

			return this;
		}

		public CPServiceMappingRecordPage ValidateFinanceCodeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(FinanceCodeFieldLink);
			ScrollToElement(FinanceCodeFieldLink);
			ValidateElementByTitle(FinanceCodeFieldLink, ExpectedText);

			return this;
		}

		public CPServiceMappingRecordPage ValidateFinanceCodeLookupButtonIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(FinanceCodeFieldLookupButton);
			else
				WaitForElementNotVisible(FinanceCodeFieldLookupButton, 3);
			bool ActualDisplayed = GetElementVisibility(FinanceCodeFieldLookupButton);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}

		public CPServiceMappingRecordPage ValidateFinanceCodeLookupButtonIsDisabled(bool ExpectedDisabled)
		{
			if (ExpectedDisabled)
				ValidateElementDisabled(FinanceCodeFieldLookupButton);
			else
				ValidateElementNotDisabled(FinanceCodeFieldLookupButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickFinanceCodeClearButton()
		{
			WaitForElementToBeClickable(FinanceCodeFieldClearButton);
			ScrollToElement(FinanceCodeFieldClearButton);
			Click(FinanceCodeFieldClearButton);

			return this;
		}

		public CPServiceMappingRecordPage ClickFinanceCodeLookupButton()
		{
			WaitForElementToBeClickable(FinanceCodeFieldLookupButton);
			ScrollToElement(FinanceCodeFieldLookupButton);
			Click(FinanceCodeFieldLookupButton);

			return this;
		}

		public CPServiceMappingRecordPage ValidateNoteText_Text(string ExpectedText)
		{
			ScrollToElement(NoteText_TextField);
			ValidateElementValue(NoteText_TextField, ExpectedText);

			return this;
		}

		public CPServiceMappingRecordPage InsertTextOnNoteTextField(string TextToInsert)
		{
			WaitForElementToBeClickable(NoteText_TextField);
			ScrollToElement(NoteText_TextField);
			SendKeys(NoteText_TextField, TextToInsert);

			return this;
		}

		public CPServiceMappingRecordPage ValidateNoteText_TextFieldIsDisplayed(bool ExpectedDisplayed)
		{
			if (ExpectedDisplayed)
				WaitForElementVisible(NoteText_TextField);
			else
				WaitForElementNotVisible(NoteText_TextField, 3);
			bool ActualDisplayed = GetElementVisibility(NoteText_TextField);
			Assert.AreEqual(ExpectedDisplayed, ActualDisplayed);

			return this;
		}
	}
}
