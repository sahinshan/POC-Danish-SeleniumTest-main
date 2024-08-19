using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Phoenix.UITests.Framework.PageObjects.People;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class SelectPersonPhysicalObservationTypePopup : CommonMethods
	{

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CWIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personphysicalobservation&')]");

        readonly By DataFormList = By.XPath("//*[@id='dataFormList']");
		readonly By CWCloseButton = By.XPath("//*[@id='CWCloseButton']");
		readonly By BtnChooseDataForm = By.XPath("//*[@id='btnChooseDataForm']");

		readonly By PageHeader = By.XPath("//*[@id = 'CWDataFormListHeader']");

        public SelectPersonPhysicalObservationTypePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        public SelectPersonPhysicalObservationTypePopup WaitForPopupToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWIFrame);
            SwitchToIframe(CWIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(DataFormList);
            WaitForElementVisible(BtnChooseDataForm);
            WaitForElementVisible(CWCloseButton);

            WaitForElementVisible(PageHeader);

            return this;
        }


        public SelectPersonPhysicalObservationTypePopup SelectPersonPhysicalObservationType(string TextToSelect)
		{
			WaitForElementToBeClickable(DataFormList);
			SelectPicklistElementByText(DataFormList, TextToSelect);

			return this;
		}

		public SelectPersonPhysicalObservationTypePopup ValidatePersonPhysicalObservationTypeSelectedText(string ExpectedText)
		{
			ValidatePicklistSelectedText(DataFormList, ExpectedText);

			return this;
		}

		public SelectPersonPhysicalObservationTypePopup ClickCloseButton()
		{
			WaitForElementToBeClickable(CWCloseButton);
			Click(CWCloseButton);

			return this;
		}

		public SelectPersonPhysicalObservationTypePopup ClickNextButton()
		{
			WaitForElementToBeClickable(BtnChooseDataForm);
			Click(BtnChooseDataForm);

			return this;
		}

		//verify options available in the data form list
		public SelectPersonPhysicalObservationTypePopup VerifyPersonPhysicalObservationTypeOptions(string OptionText, bool ExpectedAvailable = true)
		{
			WaitForElement(DataFormList);
			if(ExpectedAvailable)
				ValidatePicklistContainsElementByText(DataFormList, OptionText);
			else
				ValidatePicklistDoesNotContainsElementByText(DataFormList, OptionText);

            return this;
        }

	}
}
