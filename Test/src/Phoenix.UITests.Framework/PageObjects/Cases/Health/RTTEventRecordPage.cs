using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Cases.Health
{
	public class RTTEventRecordPage : CommonMethods
	{
        public RTTEventRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=rttevent&')]");


        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By RunOnDemandWorkflow = By.XPath("//*[@id='TI_RunOnDemandWorkflow']");
		readonly By CopyRecordLink = By.XPath("//*[@id='TI_CopyRecordLink']");

		readonly By RttwaittimeidLink = By.XPath("//*[@id='CWField_rttwaittimeid_Link']");
		readonly By RttwaittimeidLookupButton = By.XPath("//*[@id='CWLookupBtn_rttwaittimeid']");
		readonly By RtttreatmentstatusidLink = By.XPath("//*[@id='CWField_rtttreatmentstatusid_Link']");
		readonly By RtttreatmentstatusidLookupButton = By.XPath("//*[@id='CWLookupBtn_rtttreatmentstatusid']");
		readonly By RTTStatusCode = By.XPath("//*[@id='CWField_RTTStatusCode']");
		readonly By ProvideridLink = By.XPath("//*[@id='CWField_providerid_Link']");
		readonly By ProvideridLookupButton = By.XPath("//*[@id='CWLookupBtn_providerid']");
		readonly By RegardingidLink = By.XPath("//*[@id='CWField_regardingid_Link']");
		readonly By RegardingidLookupButton = By.XPath("//*[@id='CWLookupBtn_regardingid']");
		readonly By Date = By.XPath("//*[@id='CWField_date']");
		readonly By DateDatePicker = By.XPath("//*[@id='CWField_date_DatePicker']");
		readonly By Dayswaited = By.XPath("//*[@id='CWField_dayswaited']");
		readonly By Weekswaited = By.XPath("//*[@id='CWField_weekswaited']");


        public RTTEventRecordPage WaitForRTTEventRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementVisible(BackButton);
            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);

            WaitForElementVisible(RttwaittimeidLookupButton);

            return this;
        }

        public RTTEventRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public RTTEventRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public RTTEventRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public RTTEventRecordPage ClickRunOnDemandWorkflow()
		{
			WaitForElementToBeClickable(RunOnDemandWorkflow);
			Click(RunOnDemandWorkflow);

			return this;
		}

		public RTTEventRecordPage ClickCopyRecordLink()
		{
			WaitForElementToBeClickable(CopyRecordLink);
			Click(CopyRecordLink);

			return this;
		}

		public RTTEventRecordPage ClickRttWaitTimeLink()
		{
			WaitForElementToBeClickable(RttwaittimeidLink);
			MoveToElementInPage(RttwaittimeidLink);
			Click(RttwaittimeidLink);

			return this;
		}

		public RTTEventRecordPage ValidateRttWaitTimeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RttwaittimeidLink);
			ValidateElementText(RttwaittimeidLink, ExpectedText);

			return this;
		}

		public RTTEventRecordPage ClickRttWaitTimeLookupButton()
		{
			WaitForElementToBeClickable(RttwaittimeidLookupButton);
            MoveToElementInPage(RttwaittimeidLookupButton);
            Click(RttwaittimeidLookupButton);

			return this;
		}

		public RTTEventRecordPage ClickRttTreatmentStatusLink()
		{
			WaitForElementToBeClickable(RtttreatmentstatusidLink);
            MoveToElementInPage(RtttreatmentstatusidLink);
            Click(RtttreatmentstatusidLink);

			return this;
		}

		public RTTEventRecordPage ValidateRttTreatmentStatusLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RtttreatmentstatusidLink);
			ValidateElementText(RtttreatmentstatusidLink, ExpectedText);

			return this;
		}

		public RTTEventRecordPage ClickRttTreatmentStatusLookupButton()
		{
			WaitForElementToBeClickable(RtttreatmentstatusidLookupButton);
            MoveToElementInPage(RtttreatmentstatusidLookupButton);
            Click(RtttreatmentstatusidLookupButton);

			return this;
		}

		public RTTEventRecordPage ValidateRTTStatusCodeText(string ExpectedText)
		{
			ValidateElementValue(RTTStatusCode, ExpectedText);

			return this;
		}

		public RTTEventRecordPage InsertTextOnRTTStatusCode(string TextToInsert)
		{
			WaitForElementToBeClickable(RTTStatusCode);
			SendKeys(RTTStatusCode, TextToInsert);
			
			return this;
		}

		public RTTEventRecordPage ClickTransferProviderLink()
		{
			WaitForElementToBeClickable(ProvideridLink);
            MoveToElementInPage(ProvideridLink);
            Click(ProvideridLink);

			return this;
		}

		public RTTEventRecordPage ValidateTransferProviderLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ProvideridLink);
			MoveToElementInPage(ProvideridLink);
			ValidateElementText(ProvideridLink, ExpectedText);

			return this;
		}

		public RTTEventRecordPage ClickTransferProviderLookupButton()
		{
			WaitForElementToBeClickable(ProvideridLookupButton);
            MoveToElementInPage(ProvideridLookupButton);
            Click(ProvideridLookupButton);

			return this;
		}

		public RTTEventRecordPage ClickRegardingLink()
		{
			WaitForElementToBeClickable(RegardingidLink);
            MoveToElementInPage(RegardingidLink);
            Click(RegardingidLink);

			return this;
		}

		public RTTEventRecordPage ValidateRegardingLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RegardingidLink);
			ValidateElementText(RegardingidLink, ExpectedText);

			return this;
		}

		public RTTEventRecordPage ClickRegardingLookupButton()
		{
			WaitForElementToBeClickable(RegardingidLookupButton);
            MoveToElementInPage(RegardingidLookupButton);
            Click(RegardingidLookupButton);

			return this;
		}

		public RTTEventRecordPage ValidateDateText(string ExpectedText)
		{
			ValidateElementValue(Date, ExpectedText);

			return this;
		}

		public RTTEventRecordPage InsertTextOnDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Date);
            SendKeys(Date, TextToInsert);
			
			return this;
		}

		public RTTEventRecordPage ClickDateDatePicker()
		{
			WaitForElementToBeClickable(DateDatePicker);
            MoveToElementInPage(DateDatePicker);
            Click(DateDatePicker);

			return this;
		}

		public RTTEventRecordPage ValidateDaysWaitedText(string ExpectedText)
		{
			ValidateElementValue(Dayswaited, ExpectedText);

			return this;
		}

		public RTTEventRecordPage InsertTextOnDaysWaited(string TextToInsert)
		{
			WaitForElementToBeClickable(Dayswaited);
			SendKeys(Dayswaited, TextToInsert);
			
			return this;
		}

		public RTTEventRecordPage ValidateWeeksWaitedText(string ExpectedText)
		{
			ValidateElementValue(Weekswaited, ExpectedText);

			return this;
		}

		public RTTEventRecordPage InsertTextOnWeeksWaited(string TextToInsert)
		{
			WaitForElementToBeClickable(Weekswaited);
			SendKeys(Weekswaited, TextToInsert);
			
			return this;
		}

	}
}
