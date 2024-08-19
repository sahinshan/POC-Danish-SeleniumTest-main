using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Finance
{
	public class ServiceDeliveryRecordPage : CommonMethods
	{

        public ServiceDeliveryRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By serviceProvisionFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=servicedelivery&')]");


        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By ShareRecordButton = By.XPath("//*[@id='TI_ShareRecordButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
		readonly By CloneButton = By.XPath("//*[@id='TI_CloneServiceDelivery']");
		readonly By DeleteButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        readonly By serviceProvisionidLink = By.XPath("//*[@id='CWField_serviceprovisionid_Link']");
		readonly By serviceProvisionidLookupButton = By.XPath("//*[@id='CWLookupBtn_serviceprovisionid']");
		readonly By ServiceDeliveryNumber = By.XPath("//*[@id='CWField_servicedeliverynumber']");
		readonly By PlannedStartTime = By.XPath("//*[@id='CWField_plannedstarttime']");
		readonly By PlannedStartTime_TimePicker = By.XPath("//*[@id='CWField_plannedstarttime_TimePicker']");
		readonly By TotalVisits = By.XPath("//*[@id='CWField_totalvisits']");
		readonly By NumberOfCarers = By.XPath("//*[@id='CWField_numberofcarers']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By RateUnitIdLink = By.XPath("//*[@id='CWField_rateunitid_Link']");
		readonly By RateUnitIdLookupButton = By.XPath("//*[@id='CWLookupBtn_rateunitid']");
		readonly By Units = By.XPath("//*[@id='CWField_units']");
		readonly By TotalUnits = By.XPath("//*[@id='CWField_totalunits']");
		readonly By SelectAll_1 = By.XPath("//*[@id='CWField_selectall_1']");
		readonly By SelectAll_0 = By.XPath("//*[@id='CWField_selectall_0']");
		readonly By Monday_1 = By.XPath("//*[@id='CWField_monday_1']");
		readonly By Monday_0 = By.XPath("//*[@id='CWField_monday_0']");
		readonly By Tuesday_1 = By.XPath("//*[@id='CWField_tuesday_1']");
		readonly By Tuesday_0 = By.XPath("//*[@id='CWField_tuesday_0']");
		readonly By Wednesday_1 = By.XPath("//*[@id='CWField_wednesday_1']");
		readonly By Wednesday_0 = By.XPath("//*[@id='CWField_wednesday_0']");
		readonly By Thursday_1 = By.XPath("//*[@id='CWField_thursday_1']");
		readonly By Thursday_0 = By.XPath("//*[@id='CWField_thursday_0']");
		readonly By Friday_1 = By.XPath("//*[@id='CWField_friday_1']");
		readonly By Friday_0 = By.XPath("//*[@id='CWField_friday_0']");
		readonly By Saturday_1 = By.XPath("//*[@id='CWField_saturday_1']");
		readonly By Saturday_0 = By.XPath("//*[@id='CWField_saturday_0']");
		readonly By Sunday_1 = By.XPath("//*[@id='CWField_sunday_1']");
		readonly By Sunday_0 = By.XPath("//*[@id='CWField_sunday_0']");
		readonly By Notetext = By.XPath("//*[@id='CWField_notetext']");


        public ServiceDeliveryRecordPage WaitForServiceDeliveryRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(serviceProvisionFrame);
            SwitchToIframe(serviceProvisionFrame);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElementVisible(BackButton);

            WaitForElementVisible(serviceProvisionidLookupButton);
            WaitForElementVisible(ServiceDeliveryNumber);
            WaitForElementVisible(PlannedStartTime);
            WaitForElementVisible(TotalVisits);
            WaitForElementVisible(NumberOfCarers);
            WaitForElementVisible(ResponsibleTeamLookupButton);
            WaitForElementVisible(RateUnitIdLookupButton);
            WaitForElementVisible(Units);
            WaitForElementVisible(TotalUnits);

            WaitForElementVisible(SelectAll_1);
            WaitForElementVisible(Monday_1);
            WaitForElementVisible(Tuesday_1);
            WaitForElementVisible(Wednesday_1);
            WaitForElementVisible(Thursday_1);
            WaitForElementVisible(Friday_1);
            WaitForElementVisible(Saturday_1);
            WaitForElementVisible(Sunday_1);

            WaitForElementVisible(Notetext);

            return this;
        }



        public ServiceDeliveryRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public ServiceDeliveryRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public ServiceDeliveryRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public ServiceDeliveryRecordPage ClickShareRecordButton()
		{
			WaitForElementToBeClickable(ShareRecordButton);
			Click(ShareRecordButton);

			return this;
		}

		public ServiceDeliveryRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

        public ServiceDeliveryRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(AdditionalItemsButton);
            Click(AdditionalItemsButton);

            WaitForElementToBeClickable(DeleteButton);
            Click(DeleteButton);

            return this;
        }

        public ServiceDeliveryRecordPage ClickCloneButton()
        {
            WaitForElementToBeClickable(AdditionalItemsButton);
            Click(AdditionalItemsButton);

            WaitForElementToBeClickable(CloneButton);
            Click(CloneButton);

            return this;
        }

        public ServiceDeliveryRecordPage ValidateCloneButtonHidden()
        {
            WaitForElementToBeClickable(AdditionalItemsButton);
            Click(AdditionalItemsButton);

            WaitForElementNotVisible(CloneButton, 3);

            return this;
        }

        public ServiceDeliveryRecordPage ClickServiceProvisionIdLink()
		{
			WaitForElementToBeClickable(serviceProvisionidLink);
			Click(serviceProvisionidLink);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateserviceProvisionidLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(serviceProvisionidLink);
			ValidateElementText(serviceProvisionidLink, ExpectedText);

			return this;
		}

		public ServiceDeliveryRecordPage ClickserviceProvisionidLookupButton()
		{
			WaitForElementToBeClickable(serviceProvisionidLookupButton);
			Click(serviceProvisionidLookupButton);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateServiceDeliveryNumberText(string ExpectedText)
		{
			ValidateElementValue(ServiceDeliveryNumber, ExpectedText);

			return this;
		}

		public ServiceDeliveryRecordPage InsertTextOnServiceDeliveryNumber(string TextToInsert)
		{
			WaitForElementToBeClickable(ServiceDeliveryNumber);
			SendKeys(ServiceDeliveryNumber, TextToInsert);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidatePlannedStartTimeText(string ExpectedText)
		{
			ValidateElementValue(PlannedStartTime, ExpectedText);

			return this;
		}

		public ServiceDeliveryRecordPage InsertTextOnPlannedStartTime(string TextToInsert)
		{
			WaitForElementToBeClickable(PlannedStartTime);
			SendKeys(PlannedStartTime, TextToInsert);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickPlannedStartTime_TimePicker()
		{
			WaitForElementToBeClickable(PlannedStartTime_TimePicker);
			Click(PlannedStartTime_TimePicker);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateTotalVisitsText(string ExpectedText)
		{
			ValidateElementValue(TotalVisits, ExpectedText);

			return this;
		}

		public ServiceDeliveryRecordPage InsertTextOnTotalVisits(string TextToInsert)
		{
			WaitForElementToBeClickable(TotalVisits);
			SendKeys(TotalVisits, TextToInsert);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateNumberOfCarersText(string ExpectedText)
		{
			ValidateElementValue(NumberOfCarers, ExpectedText);

			return this;
		}

		public ServiceDeliveryRecordPage InsertTextOnNumberOfCarers(string TextToInsert)
		{
			WaitForElementToBeClickable(NumberOfCarers);
			SendKeys(NumberOfCarers, TextToInsert);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public ServiceDeliveryRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public ServiceDeliveryRecordPage ClickRateUnitIdLink()
		{
			WaitForElementToBeClickable(RateUnitIdLink);
			Click(RateUnitIdLink);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateRateUnitIdLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RateUnitIdLink);
			ValidateElementText(RateUnitIdLink, ExpectedText);

			return this;
		}

		public ServiceDeliveryRecordPage ClickRateUnitIdLookupButton()
		{
			WaitForElementToBeClickable(RateUnitIdLookupButton);
			Click(RateUnitIdLookupButton);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateUnitsText(string ExpectedText)
		{
			ValidateElementValue(Units, ExpectedText);

			return this;
		}

		public ServiceDeliveryRecordPage InsertTextOnUnits(string TextToInsert)
		{
			WaitForElementToBeClickable(Units);
			SendKeys(Units, TextToInsert);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateTotalUnitsText(string ExpectedText)
		{
			ValidateElementValue(TotalUnits, ExpectedText);

			return this;
		}

		public ServiceDeliveryRecordPage InsertTextOnTotalUnits(string TextToInsert)
		{
			WaitForElementToBeClickable(TotalUnits);
			SendKeys(TotalUnits, TextToInsert);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickSelectAll_YesRadioButton()
		{
			WaitForElementToBeClickable(SelectAll_1);
			Click(SelectAll_1);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateSelectAll_YesRadioButtonChecked()
		{
			WaitForElement(SelectAll_1);
			ValidateElementChecked(SelectAll_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateSelectAll_YesRadioButtonNotChecked()
		{
			WaitForElement(SelectAll_1);
			ValidateElementNotChecked(SelectAll_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickSelectAll_NoRadioButton()
		{
			WaitForElementToBeClickable(SelectAll_0);
			Click(SelectAll_0);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateSelectAll_NoRadioButtonChecked()
		{
			WaitForElement(SelectAll_0);
			ValidateElementChecked(SelectAll_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateSelectAll_NoRadioButtonNotChecked()
		{
			WaitForElement(SelectAll_0);
			ValidateElementNotChecked(SelectAll_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickMonday_YesRadioButton()
		{
			WaitForElementToBeClickable(Monday_1);
			Click(Monday_1);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateMonday_YesRadioButtonChecked()
		{
			WaitForElement(Monday_1);
			ValidateElementChecked(Monday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateMonday_YesRadioButtonNotChecked()
		{
			WaitForElement(Monday_1);
			ValidateElementNotChecked(Monday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickMonday_NoRadioButton()
		{
			WaitForElementToBeClickable(Monday_0);
			Click(Monday_0);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateMonday_NoRadioButtonChecked()
		{
			WaitForElement(Monday_0);
			ValidateElementChecked(Monday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateMonday_NoRadioButtonNotChecked()
		{
			WaitForElement(Monday_0);
			ValidateElementNotChecked(Monday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickTuesday_YesRadioButton()
		{
			WaitForElementToBeClickable(Tuesday_1);
			Click(Tuesday_1);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateTuesday_YesRadioButtonChecked()
		{
			WaitForElement(Tuesday_1);
			ValidateElementChecked(Tuesday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateTuesday_YesRadioButtonNotChecked()
		{
			WaitForElement(Tuesday_1);
			ValidateElementNotChecked(Tuesday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickTuesday_NoRadioButton()
		{
			WaitForElementToBeClickable(Tuesday_0);
			Click(Tuesday_0);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateTuesday_NoRadioButtonChecked()
		{
			WaitForElement(Tuesday_0);
			ValidateElementChecked(Tuesday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateTuesday_NoRadioButtonNotChecked()
		{
			WaitForElement(Tuesday_0);
			ValidateElementNotChecked(Tuesday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickWednesday_YesRadioButton()
		{
			WaitForElementToBeClickable(Wednesday_1);
			Click(Wednesday_1);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateWednesday_YesRadioButtonChecked()
		{
			WaitForElement(Wednesday_1);
			ValidateElementChecked(Wednesday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateWednesday_YesRadioButtonNotChecked()
		{
			WaitForElement(Wednesday_1);
			ValidateElementNotChecked(Wednesday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickWednesday_NoRadioButton()
		{
			WaitForElementToBeClickable(Wednesday_0);
			Click(Wednesday_0);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateWednesday_NoRadioButtonChecked()
		{
			WaitForElement(Wednesday_0);
			ValidateElementChecked(Wednesday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateWednesday_NoRadioButtonNotChecked()
		{
			WaitForElement(Wednesday_0);
			ValidateElementNotChecked(Wednesday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickThursday_YesRadioButton()
		{
			WaitForElementToBeClickable(Thursday_1);
			Click(Thursday_1);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateThursday_YesRadioButtonChecked()
		{
			WaitForElement(Thursday_1);
			ValidateElementChecked(Thursday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateThursday_YesRadioButtonNotChecked()
		{
			WaitForElement(Thursday_1);
			ValidateElementNotChecked(Thursday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickThursday_NoRadioButton()
		{
			WaitForElementToBeClickable(Thursday_0);
			Click(Thursday_0);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateThursday_NoRadioButtonChecked()
		{
			WaitForElement(Thursday_0);
			ValidateElementChecked(Thursday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateThursday_NoRadioButtonNotChecked()
		{
			WaitForElement(Thursday_0);
			ValidateElementNotChecked(Thursday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickFriday_YesRadioButton()
		{
			WaitForElementToBeClickable(Friday_1);
			Click(Friday_1);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateFriday_YesRadioButtonChecked()
		{
			WaitForElement(Friday_1);
			ValidateElementChecked(Friday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateFriday_YesRadioButtonNotChecked()
		{
			WaitForElement(Friday_1);
			ValidateElementNotChecked(Friday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickFriday_NoRadioButton()
		{
			WaitForElementToBeClickable(Friday_0);
			Click(Friday_0);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateFriday_NoRadioButtonChecked()
		{
			WaitForElement(Friday_0);
			ValidateElementChecked(Friday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateFriday_NoRadioButtonNotChecked()
		{
			WaitForElement(Friday_0);
			ValidateElementNotChecked(Friday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickSaturday_YesRadioButton()
		{
			WaitForElementToBeClickable(Saturday_1);
			Click(Saturday_1);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateSaturday_YesRadioButtonChecked()
		{
			WaitForElement(Saturday_1);
			ValidateElementChecked(Saturday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateSaturday_YesRadioButtonNotChecked()
		{
			WaitForElement(Saturday_1);
			ValidateElementNotChecked(Saturday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickSaturday_NoRadioButton()
		{
			WaitForElementToBeClickable(Saturday_0);
			Click(Saturday_0);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateSaturday_NoRadioButtonChecked()
		{
			WaitForElement(Saturday_0);
			ValidateElementChecked(Saturday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateSaturday_NoRadioButtonNotChecked()
		{
			WaitForElement(Saturday_0);
			ValidateElementNotChecked(Saturday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickSunday_YesRadioButton()
		{
			WaitForElementToBeClickable(Sunday_1);
			Click(Sunday_1);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateSunday_YesRadioButtonChecked()
		{
			WaitForElement(Sunday_1);
			ValidateElementChecked(Sunday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateSunday_YesRadioButtonNotChecked()
		{
			WaitForElement(Sunday_1);
			ValidateElementNotChecked(Sunday_1);
			
			return this;
		}

		public ServiceDeliveryRecordPage ClickSunday_NoRadioButton()
		{
			WaitForElementToBeClickable(Sunday_0);
			Click(Sunday_0);

			return this;
		}

		public ServiceDeliveryRecordPage ValidateSunday_NoRadioButtonChecked()
		{
			WaitForElement(Sunday_0);
			ValidateElementChecked(Sunday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateSunday_NoRadioButtonNotChecked()
		{
			WaitForElement(Sunday_0);
			ValidateElementNotChecked(Sunday_0);
			
			return this;
		}

		public ServiceDeliveryRecordPage ValidateNoteText(string ExpectedText)
		{
			ValidateElementText(Notetext, ExpectedText);

			return this;
		}

		public ServiceDeliveryRecordPage InsertTextOnNoteText(string TextToInsert)
		{
			WaitForElementToBeClickable(Notetext);
			SendKeys(Notetext, TextToInsert);
			
			return this;
		}

	}
}
