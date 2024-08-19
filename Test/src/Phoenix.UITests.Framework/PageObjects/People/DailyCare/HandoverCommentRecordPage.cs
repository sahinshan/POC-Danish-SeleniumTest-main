using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class HandoverCommentRecordPage : CommonMethods
	{

        public HandoverCommentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersonhandoverdetail&')]");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Handover Comment: ']");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By RecordidLink = By.XPath("//*[@id='CWField_recordid_Link']");
		readonly By RecordidLookupButton = By.XPath("//*[@id='CWLookupBtn_recordid']");
		readonly By Handovercomments = By.XPath("//*[@id='CWField_handovercomments']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By Handoveracknowledged_YesRadioButton = By.XPath("//*[@id='CWField_handoveracknowledged_1']");
		readonly By Handoveracknowledged_NoRadioButton = By.XPath("//*[@id='CWField_handoveracknowledged_0']");
		readonly By AcknowledgedbysystemuseridLookupButton = By.XPath("//*[@id='CWLookupBtn_acknowledgedbysystemuserid']");

        public HandoverCommentRecordPage WaitForPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(pageHeader);

            return this;
        }


        public HandoverCommentRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public HandoverCommentRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public HandoverCommentRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public HandoverCommentRecordPage ClickRecordLink()
		{
			WaitForElementToBeClickable(RecordidLink);
			Click(RecordidLink);

			return this;
		}

		public HandoverCommentRecordPage ValidateRecordLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RecordidLink);
			ValidateElementText(RecordidLink, ExpectedText);

			return this;
		}

		public HandoverCommentRecordPage ClickRecordLookupButton()
		{
			WaitForElementToBeClickable(RecordidLookupButton);
			Click(RecordidLookupButton);

			return this;
		}

		public HandoverCommentRecordPage ValidateHandoverCommentsText(string ExpectedText)
		{
			ValidateElementText(Handovercomments, ExpectedText);

			return this;
		}

		public HandoverCommentRecordPage InsertTextOnHandoverComments(string TextToInsert)
		{
			WaitForElementToBeClickable(Handovercomments);
			SendKeys(Handovercomments, TextToInsert);
			
			return this;
		}

		public HandoverCommentRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public HandoverCommentRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public HandoverCommentRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public HandoverCommentRecordPage ClickHandoveracknowledged_YesRadioButton()
		{
			WaitForElementToBeClickable(Handoveracknowledged_YesRadioButton);
			Click(Handoveracknowledged_YesRadioButton);

			return this;
		}

		public HandoverCommentRecordPage ValidateHandoveracknowledged_YesRadioButtonChecked()
		{
			WaitForElement(Handoveracknowledged_YesRadioButton);
			ValidateElementChecked(Handoveracknowledged_YesRadioButton);
			
			return this;
		}

		public HandoverCommentRecordPage ValidateHandoveracknowledged_YesRadioButtonNotChecked()
		{
			WaitForElement(Handoveracknowledged_YesRadioButton);
			ValidateElementNotChecked(Handoveracknowledged_YesRadioButton);
			
			return this;
		}

		public HandoverCommentRecordPage ClickHandoveracknowledged_NoRadioButton()
		{
			WaitForElementToBeClickable(Handoveracknowledged_NoRadioButton);
			Click(Handoveracknowledged_NoRadioButton);

			return this;
		}

		public HandoverCommentRecordPage ValidateHandoveracknowledged_NoRadioButtonChecked()
		{
			WaitForElement(Handoveracknowledged_NoRadioButton);
			ValidateElementChecked(Handoveracknowledged_NoRadioButton);
			
			return this;
		}

		public HandoverCommentRecordPage ValidateHandoveracknowledged_NoRadioButtonNotChecked()
		{
			WaitForElement(Handoveracknowledged_NoRadioButton);
			ValidateElementNotChecked(Handoveracknowledged_NoRadioButton);
			
			return this;
		}

		public HandoverCommentRecordPage ClickAcknowledgedbysystemuseridLookupButton()
		{
			WaitForElementToBeClickable(AcknowledgedbysystemuseridLookupButton);
			Click(AcknowledgedbysystemuseridLookupButton);

			return this;
		}

	}
}
