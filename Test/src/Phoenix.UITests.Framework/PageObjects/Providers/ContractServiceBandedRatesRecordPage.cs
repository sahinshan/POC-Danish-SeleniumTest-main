using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class ContractServiceBandedRatesRecordPage : CommonMethods
	{
        public ContractServiceBandedRatesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careprovidercontractservicebandedrates&')]");

        
        readonly By pageTitle = By.XPath("//*[@id='CWToolbar']/div/h1/span");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By RunOnDemandWorkflow = By.XPath("//*[@id='TI_RunOnDemandWorkflow']");

        readonly By leftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By auditSubMenuButton = By.XPath("//*[@id='CWNavItem_AuditHistory']");


        readonly By CareprovidercontractservicerateperiodidLink = By.XPath("//*[@id='CWField_careprovidercontractservicerateperiodid_Link']");
		readonly By CareprovidercontractservicerateperiodidLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidercontractservicerateperiodid']");
		readonly By Timefrom = By.XPath("//*[@id='CWField_timefrom']");
		readonly By Timefrom_TimePicker = By.XPath("//*[@id='CWField_timefrom_TimePicker']");
		readonly By Bandrate = By.XPath("//*[@id='CWField_bandrate']");
		readonly By CareprovidercontractserviceidLink = By.XPath("//*[@id='CWField_careprovidercontractserviceid_Link']");
		readonly By CareprovidercontractserviceidLookupButton = By.XPath("//*[@id='CWLookupBtn_careprovidercontractserviceid']");
		readonly By Timeto = By.XPath("//*[@id='CWField_timeto']");
		readonly By Timeto_TimePicker = By.XPath("//*[@id='CWField_timeto_TimePicker']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

        public ContractServiceBandedRatesRecordPage WaitForContractServiceBandedRatesRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(BackButton);

            WaitForElementVisible(CareprovidercontractservicerateperiodidLookupButton);
            WaitForElementVisible(CareprovidercontractserviceidLookupButton);
            return this;
        }

        public ContractServiceBandedRatesRecordPage ValidatePageTitleText(string ExpectedText)
        {
            WaitForElement(pageTitle);
            ValidateElementText(pageTitle, ExpectedText);

            return this;
        }


        public ContractServiceBandedRatesRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public ContractServiceBandedRatesRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public ContractServiceBandedRatesRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public ContractServiceBandedRatesRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public ContractServiceBandedRatesRecordPage ClickRunOnDemandWorkflow()
		{
			WaitForElementToBeClickable(RunOnDemandWorkflow);
			Click(RunOnDemandWorkflow);

			return this;
        }



        public ContractServiceBandedRatesRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(leftMenuButton);
            Click(leftMenuButton);

            WaitForElementToBeClickable(auditSubMenuButton);
            Click(auditSubMenuButton);

            return this;
        }



        public ContractServiceBandedRatesRecordPage ClickContractServiceRatePeriodLink()
		{
			WaitForElementToBeClickable(CareprovidercontractservicerateperiodidLink);
			Click(CareprovidercontractservicerateperiodidLink);

			return this;
		}

		public ContractServiceBandedRatesRecordPage ValidateContractServiceRatePeriodLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CareprovidercontractservicerateperiodidLink);
			ValidateElementText(CareprovidercontractservicerateperiodidLink, ExpectedText);

			return this;
		}

		public ContractServiceBandedRatesRecordPage ClickContractServiceRatePeriodLookupButton()
		{
			WaitForElementToBeClickable(CareprovidercontractservicerateperiodidLookupButton);
			Click(CareprovidercontractservicerateperiodidLookupButton);

			return this;
		}

        public ContractServiceBandedRatesRecordPage ValidateContractServiceRatePeriodLookupButtonDisabled(bool ExpectDisabled)
        {
			if(ExpectDisabled)
				ValidateElementDisabled(CareprovidercontractservicerateperiodidLookupButton);
			else
                ValidateElementNotDisabled(CareprovidercontractservicerateperiodidLookupButton);

            return this;
        }

        public ContractServiceBandedRatesRecordPage ValidateTimeFromText(string ExpectedText)
		{
			ValidateElementValue(Timefrom, ExpectedText);

			return this;
		}

		public ContractServiceBandedRatesRecordPage InsertTextOnTimeFrom(string TextToInsert)
		{
			WaitForElementToBeClickable(Timefrom);
			SendKeys(Timefrom, TextToInsert);
			
			return this;
		}

        public ContractServiceBandedRatesRecordPage ValidateTimeFromFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(Timefrom);
            else
                ValidateElementNotDisabled(Timefrom);

            return this;
        }

        public ContractServiceBandedRatesRecordPage ClickTimeFrom_TimePicker()
		{
			WaitForElementToBeClickable(Timefrom_TimePicker);
			Click(Timefrom_TimePicker);

			return this;
		}

		public ContractServiceBandedRatesRecordPage ValidateBandRateText(string ExpectedText)
		{
			ValidateElementValue(Bandrate, ExpectedText);

			return this;
		}

		public ContractServiceBandedRatesRecordPage InsertTextOnBandRate(string TextToInsert)
		{
			WaitForElementToBeClickable(Bandrate);
			SendKeys(Bandrate, TextToInsert);
			
			return this;
		}

        public ContractServiceBandedRatesRecordPage ValidateBandRateFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(Bandrate);
            else
                ValidateElementNotDisabled(Bandrate);

            return this;
        }

        public ContractServiceBandedRatesRecordPage ClickContractServiceLink()
		{
			WaitForElementToBeClickable(CareprovidercontractserviceidLink);
			Click(CareprovidercontractserviceidLink);

			return this;
		}

		public ContractServiceBandedRatesRecordPage ValidateContractServiceLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(CareprovidercontractserviceidLink);
			ValidateElementText(CareprovidercontractserviceidLink, ExpectedText);

			return this;
		}

		public ContractServiceBandedRatesRecordPage ClickContractServiceLookupButton()
		{
			WaitForElementToBeClickable(CareprovidercontractserviceidLookupButton);
			Click(CareprovidercontractserviceidLookupButton);

			return this;
		}

        public ContractServiceBandedRatesRecordPage ValidateContractServiceLookupButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(CareprovidercontractserviceidLookupButton);
            else
                ValidateElementNotDisabled(CareprovidercontractserviceidLookupButton);

            return this;
        }

        public ContractServiceBandedRatesRecordPage ValidateTimeToText(string ExpectedText)
		{
			ValidateElementValue(Timeto, ExpectedText);

			return this;
		}

		public ContractServiceBandedRatesRecordPage InsertTextOnTimeTo(string TextToInsert)
		{
			WaitForElementToBeClickable(Timeto);
			SendKeys(Timeto, TextToInsert);
			
			return this;
		}

        public ContractServiceBandedRatesRecordPage ValidateTimeToFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(Timeto);
            else
                ValidateElementNotDisabled(Timeto);

            return this;
        }

        public ContractServiceBandedRatesRecordPage ClickTimeTo_TimePicker()
		{
			WaitForElementToBeClickable(Timeto_TimePicker);
			Click(Timeto_TimePicker);

			return this;
		}

		public ContractServiceBandedRatesRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public ContractServiceBandedRatesRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public ContractServiceBandedRatesRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

        public ContractServiceBandedRatesRecordPage ValidateResponsibleTeamLookupButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(ResponsibleTeamLookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeamLookupButton);

            return this;
        }
    }
}
