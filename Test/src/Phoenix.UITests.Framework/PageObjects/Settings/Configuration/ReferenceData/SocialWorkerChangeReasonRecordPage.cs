using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class SocialWorkerChangeReasonRecordPage : CommonMethods
	{
        public SocialWorkerChangeReasonRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_socialworkerchangereason = By.Id("iframe_socialworkerchangereason");
        readonly By cwDialog_ContactReasonFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=socialworkerchangereason')]");

        readonly By ContactReasonRecordPageHeader = By.XPath("//*[@id='CWToolbar']//h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

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

        public SocialWorkerChangeReasonRecordPage WaitForSocialWorkerChangeReasonRecordPagePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementVisible(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementVisible(iframe_socialworkerchangereason);
            SwitchToIframe(iframe_socialworkerchangereason);

            WaitForElementVisible(cwDialog_ContactReasonFrame);
            SwitchToIframe(cwDialog_ContactReasonFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(ContactReasonRecordPageHeader);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);

            return this;
        }


        public SocialWorkerChangeReasonRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}



		public SocialWorkerChangeReasonRecordPage ValidateNameText(string ExpectedText)
		{
			WaitForElement(Name);
			ValidateElementValue(Name, ExpectedText);

            return this;
		}

		public SocialWorkerChangeReasonRecordPage InsertTextOnName(string TextToInsert)
		{
			WaitForElementToBeClickable(Name);
			SendKeys(Name, TextToInsert);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateCodeText(string ExpectedText)
		{
			ValidateElementValue(Code, ExpectedText);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage InsertTextOnCode(string TextToInsert)
		{
			WaitForElementToBeClickable(Code);
			SendKeys(Code, TextToInsert);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateGovCodeText(string ExpectedText)
		{
			ValidateElementValue(Govcode, ExpectedText);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage InsertTextOnGovcode(string TextToInsert)
		{
			WaitForElementToBeClickable(Govcode);
			SendKeys(Govcode, TextToInsert);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ClickInactive_YesRadioButton()
		{
			WaitForElementToBeClickable(Inactive_1);
			Click(Inactive_1);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateInactive_YesRadioButtonChecked()
		{
			WaitForElement(Inactive_1);
			ValidateElementChecked(Inactive_1);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateInactive_YesRadioButtonNotChecked()
		{
			WaitForElement(Inactive_1);
			ValidateElementNotChecked(Inactive_1);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ClickInactive_NoRadioButton()
		{
			WaitForElementToBeClickable(Inactive_0);
			Click(Inactive_0);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateInactive_NoRadioButtonChecked()
		{
			WaitForElement(Inactive_0);
			ValidateElementChecked(Inactive_0);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateInactive_NoRadioButtonNotChecked()
		{
			WaitForElement(Inactive_0);
			ValidateElementNotChecked(Inactive_0);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateStartDateText(string ExpectedText)
		{
			ValidateElementValue(Startdate, ExpectedText);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Startdate);
			SendKeys(Startdate, TextToInsert);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartdateDatePicker);
			Click(StartdateDatePicker);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateEndDateText(string ExpectedText)
		{
			ValidateElementValue(Enddate, ExpectedText);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage InsertTextOnEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Enddate);
			SendKeys(Enddate, TextToInsert);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EnddateDatePicker);
			Click(EnddateDatePicker);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage ClickValidforexport_YesRadioButton()
		{
			WaitForElementToBeClickable(Validforexport_1);
			Click(Validforexport_1);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateValidforexport_YesRadioButtonChecked()
		{
			WaitForElement(Validforexport_1);
			ValidateElementChecked(Validforexport_1);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateValidforexport_YesRadioButtonNotChecked()
		{
			WaitForElement(Validforexport_1);
			ValidateElementNotChecked(Validforexport_1);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ClickValidforexport_NoRadioButton()
		{
			WaitForElementToBeClickable(Validforexport_0);
			Click(Validforexport_0);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateValidforexport_NoRadioButtonChecked()
		{
			WaitForElement(Validforexport_0);
			ValidateElementChecked(Validforexport_0);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateValidforexport_NoRadioButtonNotChecked()
		{
			WaitForElement(Validforexport_0);
			ValidateElementNotChecked(Validforexport_0);
			
			return this;
		}

		public SocialWorkerChangeReasonRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public SocialWorkerChangeReasonRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

	}
}
