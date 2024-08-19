using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
	public class UpliftRatesforMasterPayArrangementsPopup : CommonMethods
	{

        public UpliftRatesforMasterPayArrangementsPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By ContentIFrame = By.Id("CWContentIFrame");
		readonly By UpliftRatesPayArrangementIFrame = By.Id("iframe_UpliftRatesPayArrangement");

        readonly By PageHeader = By.XPath("//h1[@id='CWHeaderTitle']");
		readonly By SetToSpecificValue = By.XPath("//*[@id='CWSetValue']");
		readonly By ChangeBySpecificValue = By.XPath("//*[@id='CWChangeValue']");
		readonly By IncreaseByPercentage = By.XPath("//*[@id='CWIncreaseValue']");
		readonly By Close = By.XPath("//*[@id='CWClose']");
		readonly By Uplift = By.XPath("//*[@id='CWUplift']");

		readonly By UpliftValueField = By.XPath("//*[@id='CWField_upliftvalue']");

        //readonly By NewRateFieldLabel = By.XPath("//*[@id='CWLabelHolder_upliftvalue']/label[text() = 'New Rate']");
        By UpliftValueFieldLabel(string FieldLabelText) => By.XPath("//*[@id='CWLabelHolder_upliftvalue']/label[text() = '"+FieldLabelText+"']");
		readonly By UpliftValueFieldError = By.XPath("//*[@id = 'CWField_upliftvalue']/following-sibling::*/span");

        public UpliftRatesforMasterPayArrangementsPopup WaitForPageToLoad()
        {
            System.Threading.Thread.Sleep(1000);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(ContentIFrame);
            SwitchToIframe(ContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

			WaitForElement(UpliftRatesPayArrangementIFrame);
			SwitchToIframe(UpliftRatesPayArrangementIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(PageHeader);

            return this;
        }

		public UpliftRatesforMasterPayArrangementsPopup ValidateUpliftOptionsAreDisplayed(bool IsDisplayed = true)
		{
			if(IsDisplayed)
			{
				WaitForElementVisible(SetToSpecificValue);
				WaitForElementVisible(ChangeBySpecificValue);
				WaitForElementVisible(IncreaseByPercentage);
				WaitForElementVisible(UpliftValueField);
				WaitForElementVisible(Close);
				WaitForElementVisible(Uplift);
			}
			else
			{
				WaitForElementNotVisible(SetToSpecificValue, 3);
                WaitForElementNotVisible(ChangeBySpecificValue, 3);
                WaitForElementNotVisible(IncreaseByPercentage, 3);
                WaitForElementNotVisible(UpliftValueField, 3);
                WaitForElementNotVisible(Close, 3);
                WaitForElementNotVisible(Uplift, 3);
			}

			return this;
		}


        public UpliftRatesforMasterPayArrangementsPopup ClickSetToSpecificValueOption()
		{
			ClickWithoutWaiting(SetToSpecificValue);

			return this;
		}

		public UpliftRatesforMasterPayArrangementsPopup ValidateSetToSpecificValueChecked(bool IsChecked = true)
		{
			WaitForElement(SetToSpecificValue);

			if(IsChecked)
				ValidateElementChecked(SetToSpecificValue);
			else
				ValidateElementNotChecked(SetToSpecificValue);
			
			return this;
		}

		public UpliftRatesforMasterPayArrangementsPopup ClickChangeBySpecificValueOption()
		{
			ScrollToElement(ChangeBySpecificValue);
            ClickWithoutWaiting(ChangeBySpecificValue);

			return this;
		}

		public UpliftRatesforMasterPayArrangementsPopup ValidateChangeBySpecificValueChecked(bool IsChecked = true)
		{
			WaitForElement(ChangeBySpecificValue);
			if(IsChecked)
                ValidateElementChecked(ChangeBySpecificValue);
            else
                ValidateElementNotChecked(ChangeBySpecificValue);
			
			return this;
		}

		public UpliftRatesforMasterPayArrangementsPopup ClickIncreaseByPercentageOption()
		{
			ScrollToElement(IncreaseByPercentage);
			ClickWithoutWaiting(IncreaseByPercentage);

			return this;
		}

		public UpliftRatesforMasterPayArrangementsPopup ValidateIncreaseByPercentageChecked(bool IsChecked = true)
		{
			WaitForElement(IncreaseByPercentage);
			if(IsChecked)
                ValidateElementChecked(IncreaseByPercentage);
            else
                ValidateElementNotChecked(IncreaseByPercentage);
			
			return this;
		}

        //Verify UpliftValueFieldLabel
		public UpliftRatesforMasterPayArrangementsPopup ValidateUpliftValueFieldLabel(string FieldLabelText)
		{
			WaitForElement(UpliftValueFieldLabel(FieldLabelText));

            return this;
		}

        //Insert text in UpliftField 
        public UpliftRatesforMasterPayArrangementsPopup InsertUpliftValue(string UpliftValue)
		{
			WaitForElement(UpliftValueField);
			ScrollToElement(UpliftValueField);
			SendKeys(UpliftValueField, UpliftValue + Keys.Tab);

			return this;
		}

		public UpliftRatesforMasterPayArrangementsPopup ValidateUpliftValueFieldRange(string ExpectedText)
		{
			WaitForElement(UpliftValueField);
			ScrollToElement(UpliftValueField);
			ValidateElementAttribute(UpliftValueField, "range", ExpectedText);

			return this;
		}

		public UpliftRatesforMasterPayArrangementsPopup ValidateUpliftValueFieldError(string ExpectedText)
		{
			WaitForElement(UpliftValueFieldError);
			ScrollToElement(UpliftValueFieldError);
			ValidateElementByTitle(UpliftValueFieldError, ExpectedText);

			return this;
		}

        public UpliftRatesforMasterPayArrangementsPopup ClickClose()
		{
			ScrollToElement(Close);
			ClickWithoutWaiting(Close);

			return this;
		}

		public UpliftRatesforMasterPayArrangementsPopup ClickUplift()
		{
			ScrollToElement(Uplift);
			ClickWithoutWaiting(Uplift);

			return this;
		}

	}
}
