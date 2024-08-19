using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class BandRateScheduleRecordPage : CommonMethods
	{
        public BandRateScheduleRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderbandrateschedule')]");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

		readonly By Name = By.XPath("//*[@id='CWField_name']");
		readonly By Timefrom = By.XPath("//*[@id='CWField_timefrom']");
		readonly By Timefrom_TimePicker = By.XPath("//*[@id='CWField_timefrom_TimePicker']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By CareproviderbandratetypeidLink = By.XPath("//*[@id='CWField_careproviderbandratetypeid_Link']");
		readonly By CareproviderbandratetypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_careproviderbandratetypeid']");
		readonly By Timeto = By.XPath("//*[@id='CWField_timeto']");
		readonly By Timeto_TimePicker = By.XPath("//*[@id='CWField_timeto_TimePicker']");



        public BandRateScheduleRecordPage WaitForPageToLoad()
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


        public BandRateScheduleRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public BandRateScheduleRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public BandRateScheduleRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public BandRateScheduleRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public BandRateScheduleRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}



		public BandRateScheduleRecordPage ValidateNameText(string ExpectedText)
		{
			ValidateElementValue(Name, ExpectedText);

			return this;
		}

        public BandRateScheduleRecordPage ValidateNameFieldVisible()
        {
            WaitForElementVisible(Name);

            return this;
        }

        public BandRateScheduleRecordPage InsertTextOnName(string TextToInsert)
		{
			WaitForElementToBeClickable(Name);
			SendKeys(Name, TextToInsert);
			
			return this;
		}

		public BandRateScheduleRecordPage ValidateVisitLengthFromText(string ExpectedText)
		{
			ValidateElementValueByJavascript("CWField_timefrom", ExpectedText);

			return this;
		}

        public BandRateScheduleRecordPage ValidateVisitLengthFromFieldTooltip(string ExpectedTooltipText)
        {
            this.ValidateElementAttribute(Timefrom, "title", ExpectedTooltipText);

            return this;
        }

        public BandRateScheduleRecordPage InsertTextOnVisitLengthFrom(string TextToInsert)
		{
			WaitForElementToBeClickable(Timefrom);
			SendKeys(Timefrom, TextToInsert);
			
			return this;
		}

		public BandRateScheduleRecordPage ClickVisitLengthFrom_TimePicker()
		{
			WaitForElementToBeClickable(Timefrom_TimePicker);
			Click(Timefrom_TimePicker);

			return this;
		}

		public BandRateScheduleRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public BandRateScheduleRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public BandRateScheduleRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public BandRateScheduleRecordPage ClickBandRateTypeLink()
		{
			WaitForElementToBeClickable(CareproviderbandratetypeidLink);
			Click(CareproviderbandratetypeidLink);

			return this;
		}

		public BandRateScheduleRecordPage ValidateBandRateTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CareproviderbandratetypeidLink);
			ValidateElementText(CareproviderbandratetypeidLink, ExpectedText);

			return this;
		}

		public BandRateScheduleRecordPage ClickBandRateTypeLookupButton()
		{
			WaitForElementToBeClickable(CareproviderbandratetypeidLookupButton);
			Click(CareproviderbandratetypeidLookupButton);

			return this;
		}

		public BandRateScheduleRecordPage ValidateVisitLengthToText(string ExpectedText)
		{
            ValidateElementValueByJavascript("CWField_timeto", ExpectedText);

            return this;
		}

        public BandRateScheduleRecordPage ValidateVisitLengthToFieldTooltip(string ExpectedTooltipText)
        {
            this.ValidateElementAttribute(Timeto, "title", ExpectedTooltipText);

            return this;
        }

        public BandRateScheduleRecordPage InsertTextOnVisitLengthTo(string TextToInsert)
		{
			WaitForElementToBeClickable(Timeto);
			SendKeys(Timeto, TextToInsert);
			
			return this;
		}

		public BandRateScheduleRecordPage ClickVisitLengthTo_TimePicker()
		{
			WaitForElementToBeClickable(Timeto_TimePicker);
			Click(Timeto_TimePicker);

			return this;
		}

	}
}
