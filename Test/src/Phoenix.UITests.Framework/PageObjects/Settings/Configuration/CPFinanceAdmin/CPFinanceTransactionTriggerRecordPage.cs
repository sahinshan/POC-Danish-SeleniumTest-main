using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
	public class CPFinanceTransactionTriggerRecordPage : CommonMethods
	{

        public CPFinanceTransactionTriggerRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CWDialogIframe = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=careproviderfinancetransactiontrigger&')]");
        readonly By CWUrlIframe = By.Id("CWUrlPanel_IFrame");
        readonly By FinanceTransactionTriggerRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By RunOnDemandWorkflow = By.XPath("//*[@id='TI_RunOnDemandWorkflow']");
		readonly By CopyRecordLink = By.XPath("//*[@id='TI_CopyRecordLink']");
		readonly By RecordLink = By.XPath("//*[@id='CWField_recordid_Link']");
		readonly By RecordLookupButton = By.XPath("//*[@id='CWLookupBtn_recordid']");
		readonly By Istoexpand_1 = By.XPath("//*[@id='CWField_istoexpand_1']");
		readonly By Istoexpand_0 = By.XPath("//*[@id='CWField_istoexpand_0']");
		readonly By ErrorTrace = By.XPath("//*[@id='CWField_errortrace']");
		readonly By Reason = By.XPath("//*[@id='CWField_reasonid']");
		readonly By Status = By.XPath("//*[@id='CWField_statusid']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

        public CPFinanceTransactionTriggerRecordPage WaitForPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWDialogIframe);
            SwitchToIframe(CWDialogIframe);

            WaitForElementNotVisible("CWRefreshPanel", 15);

			WaitForElement(FinanceTransactionTriggerRecordPageHeader);

            return this;
        }


        public CPFinanceTransactionTriggerRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ClickRunOnDemandWorkflow()
		{
			WaitForElementToBeClickable(RunOnDemandWorkflow);
			Click(RunOnDemandWorkflow);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ClickCopyRecordLink()
		{
			WaitForElementToBeClickable(CopyRecordLink);
			Click(CopyRecordLink);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ClickRecordLink()
		{
			WaitForElementToBeClickable(RecordLink);
			Click(RecordLink);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ValidateRecordLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(RecordLink);
			ValidateElementText(RecordLink, ExpectedText);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ClickRecordLookupButton()
		{
			WaitForElementToBeClickable(RecordLookupButton);
			Click(RecordLookupButton);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ClickIstoexpand_Yes()
		{
			WaitForElementToBeClickable(Istoexpand_1);
			Click(Istoexpand_1);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ValidateIstoexpand_YesChecked()
		{
			WaitForElement(Istoexpand_1);
			ValidateElementChecked(Istoexpand_1);
			
			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ValidateIstoexpand_YesNotChecked()
		{
			WaitForElement(Istoexpand_1);
			ValidateElementNotChecked(Istoexpand_1);
			
			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ClickIstoexpand_No()
		{
			WaitForElementToBeClickable(Istoexpand_0);
			Click(Istoexpand_0);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ValidateIstoexpand_NoChecked()
		{
			WaitForElement(Istoexpand_0);
			ValidateElementChecked(Istoexpand_0);
			
			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ValidateIstoexpand_NoNotChecked()
		{
			WaitForElement(Istoexpand_0);
			ValidateElementNotChecked(Istoexpand_0);
			
			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ValidateErrorTraceText(string ExpectedText)
		{
			ValidateElementText(ErrorTrace, ExpectedText);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage InsertTextOnErrorTrace(string TextToInsert)
		{
			WaitForElementToBeClickable(ErrorTrace);
			SendKeys(ErrorTrace, TextToInsert);
			
			return this;
		}

		public CPFinanceTransactionTriggerRecordPage SelectReason(string TextToSelect)
		{
			WaitForElementToBeClickable(Reason);
			SelectPicklistElementByText(Reason, TextToSelect);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ValidateReasonSelectedText(string ExpectedText)
		{
			WaitForElementVisible(Reason);
			ScrollToElement(Reason);
			ValidatePicklistSelectedText(Reason, ExpectedText);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage SelectStatus(string TextToSelect)
		{
			WaitForElementToBeClickable(Status);
			SelectPicklistElementByText(Status, TextToSelect);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ValidateStatusSelectedText(string ExpectedText)
		{
			WaitForElementVisible(Status);
			ScrollToElement(Status);
            ValidatePicklistSelectedText(Status, ExpectedText);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public CPFinanceTransactionTriggerRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

	}
}
