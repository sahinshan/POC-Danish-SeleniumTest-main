using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
	public class ProviderCarerSearchPopup : CommonMethods
	{
		public ProviderCarerSearchPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}
		
		readonly By iframe_ProviderCarer = By.Id("iframe_ProviderCarerSearch");
		readonly By popupHeader = By.XPath("//header//h1");		

		readonly By BackButton = By.XPath("//*[@id='CWClose']"); //Back Button
		readonly By ProviderNameField = By.XPath("//*[@id='CWProviderName']");
		readonly By PostcodeField = By.XPath("//*[@id='CWPostcode']");
		readonly By ClearFiltersButton = By.XPath("//*[@id='CWClearFiltersButton']");
		readonly By ProviderCarerSearchButton = By.XPath("//*[@id='TI_ProviderCarerSearchButton']");
		readonly By OKButton = By.XPath("//*[@id='CWSubmit']");
		readonly By CloseButton = By.XPath("//button[text() = 'Close']");

		By checkboxElement(string ElementID) => By.XPath("//tr[@id='" + ElementID + "']/td[1]/input[@id='CHK_" + ElementID + "']");

		public ProviderCarerSearchPopup WaitForProviderCarerSearchPopupToLoad()
		{
			WaitForElement(iframe_ProviderCarer);
			SwitchToIframe(iframe_ProviderCarer);

			WaitForElementNotVisible("CWRefreshPanel", 80);

			WaitForElement(popupHeader);

			WaitForElement(BackButton);
			WaitForElement(ProviderNameField);
			WaitForElement(PostcodeField);
			WaitForElement(ClearFiltersButton);
			WaitForElement(ProviderCarerSearchButton);
			WaitForElementVisible(OKButton);			

			return this;
		}

		public ProviderCarerSearchPopup ClickCloseButton()
		{
			WaitForElementToBeClickable(CloseButton);
			Click(CloseButton);

			return this;
		}

		public ProviderCarerSearchPopup ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public ProviderCarerSearchPopup ValidateProviderNameText(string ExpectedText)
		{
			WaitForElementToBeClickable(ProviderNameField);
			ValidateElementValue(ProviderNameField, ExpectedText);

			return this;
		}

		public ProviderCarerSearchPopup InsertProviderNameText(string TextToInsert)
		{
			WaitForElementToBeClickable(ProviderNameField);
			MoveToElementInPage(ProviderNameField);
			SendKeys(ProviderNameField, TextToInsert);

			return this;
		}

		public ProviderCarerSearchPopup ValidatePostcodeText(string ExpectedText)
		{
			WaitForElementToBeClickable(PostcodeField);
			ValidateElementValue(PostcodeField, ExpectedText);

			return this;
		}

		public ProviderCarerSearchPopup InsertTextOnCWPostcode(string TextToInsert)
		{
			WaitForElementToBeClickable(PostcodeField);
			SendKeys(PostcodeField, TextToInsert);

			return this;
		}

		public ProviderCarerSearchPopup ClickClearFiltersButton()
		{
			WaitForElementToBeClickable(ClearFiltersButton);
			MoveToElementInPage(ClearFiltersButton);
			Click(ClearFiltersButton);

			return this;
		}

		public ProviderCarerSearchPopup ClickSearchButton()
		{
			WaitForElementToBeClickable(ProviderCarerSearchButton);
			Click(ProviderCarerSearchButton);

			return this;
		}

		public ProviderCarerSearchPopup ClickOKButton()
		{
			WaitForElementToBeClickable(OKButton);
			Click(OKButton);

			return this;
		}

		public ProviderCarerSearchPopup SelectResultElement(string ElementID)
		{
			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementToBeClickable(checkboxElement(ElementID));
			Click(checkboxElement(ElementID));

			Click(OKButton);

			SwitchToDefaultFrame();

			return this;
		}

		public ProviderCarerSearchPopup ValidateServiceProviderPresentOrNot(string RecordID, bool IsPresent)
		{
			WaitForElementNotVisible("CWRefreshPanel", 20);

			if (IsPresent)
				WaitForElementVisible(checkboxElement(RecordID));
			else
				WaitForElementNotVisible(checkboxElement(RecordID), 10);

			Click(CloseButton);

			SwitchToDefaultFrame();

			return this;
		}

	}
}
