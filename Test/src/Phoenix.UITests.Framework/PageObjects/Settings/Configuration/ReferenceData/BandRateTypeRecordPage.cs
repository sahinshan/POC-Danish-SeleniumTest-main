using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class BandRateTypeRecordPage : CommonMethods
	{
        public BandRateTypeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderbandratetype')]");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        readonly By BandRateSchedulesTab = By.XPath("//*[@id='CWNavGroup_CareProviderBandRateSchedule']/a");

        readonly By Name = By.XPath("//*[@id='CWField_name']");
		readonly By Code = By.XPath("//*[@id='CWField_code']");
		readonly By Govcode = By.XPath("//*[@id='CWField_govcode']");
		readonly By Inactive_1 = By.XPath("//*[@id='CWField_inactive_1']");
		readonly By Inactive_0 = By.XPath("//*[@id='CWField_inactive_0']");
		readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EnddateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");
		readonly By Validforexport_1 = By.XPath("//*[@id='CWField_validforexport_1']");
		readonly By Validforexport_0 = By.XPath("//*[@id='CWField_validforexport_0']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");


        public BandRateTypeRecordPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(Name);
            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);

            return this;
        }



        public BandRateTypeRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public BandRateTypeRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public BandRateTypeRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public BandRateTypeRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public BandRateTypeRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}



        public BandRateTypeRecordPage ClickBandRateSchedulesTab()
        {
            WaitForElementToBeClickable(BandRateSchedulesTab);
            Click(BandRateSchedulesTab);

            return this;
        }



        public BandRateTypeRecordPage ValidateNameText(string ExpectedText)
		{
			ValidateElementValue(Name, ExpectedText);

			return this;
		}

		public BandRateTypeRecordPage InsertTextOnName(string TextToInsert)
		{
			WaitForElementToBeClickable(Name);
			SendKeys(Name, TextToInsert);
			
			return this;
		}

		public BandRateTypeRecordPage ValidateCodeText(string ExpectedText)
		{
			ValidateElementValue(Code, ExpectedText);

			return this;
		}

		public BandRateTypeRecordPage InsertTextOnCode(string TextToInsert)
		{
			WaitForElementToBeClickable(Code);
			SendKeys(Code, TextToInsert);
			
			return this;
		}

		public BandRateTypeRecordPage ValidateGovCodeText(string ExpectedText)
		{
			ValidateElementValue(Govcode, ExpectedText);

			return this;
		}

		public BandRateTypeRecordPage InsertTextOnGovCode(string TextToInsert)
		{
			WaitForElementToBeClickable(Govcode);
			SendKeys(Govcode, TextToInsert);
			
			return this;
		}

		public BandRateTypeRecordPage ClickInactive_YesRadioButton()
		{
			WaitForElementToBeClickable(Inactive_1);
			Click(Inactive_1);

			return this;
		}

		public BandRateTypeRecordPage ValidateInactive_YesRadioButtonChecked()
		{
			WaitForElement(Inactive_1);
			ValidateElementChecked(Inactive_1);
			
			return this;
		}

		public BandRateTypeRecordPage ValidateInactive_YesRadioButtonNotChecked()
		{
			WaitForElement(Inactive_1);
			ValidateElementNotChecked(Inactive_1);
			
			return this;
		}

		public BandRateTypeRecordPage ClickInactive_NoRadioButton()
		{
			WaitForElementToBeClickable(Inactive_0);
			Click(Inactive_0);

			return this;
		}

		public BandRateTypeRecordPage ValidateInactive_NoRadioButtonChecked()
		{
			WaitForElement(Inactive_0);
			ValidateElementChecked(Inactive_0);
			
			return this;
		}

		public BandRateTypeRecordPage ValidateInactive_NoRadioButtonNotChecked()
		{
			WaitForElement(Inactive_0);
			ValidateElementNotChecked(Inactive_0);
			
			return this;
		}

		public BandRateTypeRecordPage ValidateStartDateText(string ExpectedText)
		{
			ValidateElementValue(Startdate, ExpectedText);

			return this;
		}

		public BandRateTypeRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Startdate);
			SendKeys(Startdate, TextToInsert);
			
			return this;
		}

		public BandRateTypeRecordPage ClickStartDate_DatePicker()
		{
			WaitForElementToBeClickable(StartdateDatePicker);
			Click(StartdateDatePicker);

			return this;
		}

		public BandRateTypeRecordPage ValidateEndDateText(string ExpectedText)
		{
			ValidateElementValue(Enddate, ExpectedText);

			return this;
		}

		public BandRateTypeRecordPage InsertTextOnEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Enddate);
			SendKeys(Enddate, TextToInsert);
			
			return this;
		}

		public BandRateTypeRecordPage ClickEndDate_DatePicker()
		{
			WaitForElementToBeClickable(EnddateDatePicker);
			Click(EnddateDatePicker);

			return this;
		}

		public BandRateTypeRecordPage ClickValidForExport_YesRadioButton()
		{
			WaitForElementToBeClickable(Validforexport_1);
			Click(Validforexport_1);

			return this;
		}

		public BandRateTypeRecordPage ValidateValidForExport_YesRadioButtonChecked()
		{
			WaitForElement(Validforexport_1);
			ValidateElementChecked(Validforexport_1);
			
			return this;
		}

		public BandRateTypeRecordPage ValidateValidForExport_YesRadioButtonNotChecked()
		{
			WaitForElement(Validforexport_1);
			ValidateElementNotChecked(Validforexport_1);
			
			return this;
		}

		public BandRateTypeRecordPage ClickValidForExport_NoRadioButton()
		{
			WaitForElementToBeClickable(Validforexport_0);
			Click(Validforexport_0);

			return this;
		}

		public BandRateTypeRecordPage ValidateValidForExport_NoRadioButtonChecked()
		{
			WaitForElement(Validforexport_0);
			ValidateElementChecked(Validforexport_0);
			
			return this;
		}

		public BandRateTypeRecordPage ValidateValidForExport_NoRadioButtonNotChecked()
		{
			WaitForElement(Validforexport_0);
			ValidateElementNotChecked(Validforexport_0);
			
			return this;
		}

		public BandRateTypeRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public BandRateTypeRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public BandRateTypeRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

	}
}
