using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Finance
{
	public class FinanceExtractRecordPage : CommonMethods
	{
		public FinanceExtractRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		readonly By cwDialogIFrame_financeExtract = By.XPath("//iframe[contains(@id, 'iframe_CWDialog')][contains(@src, 'type=financeextract&')]");
		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
		readonly By RunRemittanceAdviceButton = By.Id("TI_RunRemittanceAdvice");
		readonly By BatchId = By.XPath("//*[@id='CWField_batchid']");
		readonly By Runontime = By.XPath("//*[@id='CWField_runontime']");
		readonly By RunontimeDatePicker = By.XPath("//*[@id='CWField_runontime_DatePicker']");
		readonly By Runontime_Time = By.XPath("//*[@id='CWField_runontime_Time']");
		readonly By Runontime_Time_TimePicker = By.XPath("//*[@id='CWField_runontime_Time_TimePicker']");
		readonly By FinanceExtractBatchSetupLink = By.XPath("//*[@id='CWField_financeextractsetupid_Link']");
		readonly By FinanceExtractBatchSetupLookupButton = By.XPath("//*[@id='CWLookupBtn_financeextractsetupid']");
		readonly By BatchStatusId = By.XPath("//*[@id='CWField_batchstatusid']");
		readonly By FinanceModuleId = By.XPath("//*[@id='CWField_financemoduleid']");
		readonly By Isadhoc_1 = By.XPath("//*[@id='CWField_adhoc_1']");
		readonly By Isadhoc_0 = By.XPath("//*[@id='CWField_adhoc_0']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By FinanceExtractBatchRecordPageHeader = By.XPath("//h1");
		readonly By FinanceInvoicesTab = By.XPath("//li[@id = 'CWNavGroup_FinanceInvoice']");

		public FinanceExtractRecordPage WaitForFinanceExtractRecordPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElement(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElement(cwDialogIFrame_financeExtract);
			SwitchToIframe(cwDialogIFrame_financeExtract);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(FinanceExtractBatchRecordPageHeader);

			return this;
		}


		public FinanceExtractRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			MoveToElementInPage(BackButton);
			Click(BackButton);

			return this;
		}

		public FinanceExtractRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			MoveToElementInPage(SaveButton);
			Click(SaveButton);

			return this;
		}

		public FinanceExtractRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			MoveToElementInPage(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public FinanceExtractRecordPage ClickRunRemittanceAdviceButton()
		{
			WaitForElementToBeClickable(RunRemittanceAdviceButton);
			MoveToElementInPage(RunRemittanceAdviceButton);
			Click(RunRemittanceAdviceButton);

			return this;
		}

		public FinanceExtractRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public FinanceExtractRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public FinanceExtractRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public FinanceExtractRecordPage ValidateResponsibleTeamLinkFieldDisabled(bool ExpectedDisabled)
		{
			MoveToElementInPage(ResponsibleTeamLink);
			if (ExpectedDisabled)
			{
				WaitForElementVisible(ResponsibleTeamLink);
				ValidateElementDisabled(ResponsibleTeamLink);
			}
			else
			{
				WaitForElementVisible(ResponsibleTeamLink);
				ValidateElementNotDisabled(ResponsibleTeamLink);
			}
			return this;
		}

		public FinanceExtractRecordPage SelectFinanceModule(string TextToSelect)
		{
			WaitForElementToBeClickable(FinanceModuleId);
			SelectPicklistElementByText(FinanceModuleId, TextToSelect);

			return this;
		}

		public FinanceExtractRecordPage ValidateFinanceModuleSelectedText(string ExpectedText)
		{
			ValidateElementText(FinanceModuleId, ExpectedText);

			return this;
		}

		public FinanceExtractRecordPage ValidateFinanceModulePicklistFieldDisabled(bool ExpectedDisabled)
		{
			MoveToElementInPage(FinanceModuleId);
			if (ExpectedDisabled)
			{
				WaitForElementVisible(FinanceModuleId);
				ValidateElementDisabled(FinanceModuleId);
			}
			else
			{
				WaitForElementVisible(FinanceModuleId);
				ValidateElementNotDisabled(FinanceModuleId);
			}
			return this;
		}

		public FinanceExtractRecordPage ValidateRunOnTimeText(string ExpectedText)
		{
			MoveToElementInPage(Runontime);
			ValidateElementValue(Runontime, ExpectedText);

			return this;
		}

		public FinanceExtractRecordPage InsertTextOnRunOnTime(string TextToInsert)
		{
			WaitForElementToBeClickable(Runontime);
			SendKeys(Runontime, TextToInsert);

			return this;
		}

		public FinanceExtractRecordPage ClickRunOnTimeDatePicker()
		{
			WaitForElementToBeClickable(RunontimeDatePicker);
			Click(RunontimeDatePicker);

			return this;
		}

		public FinanceExtractRecordPage ValidateRunOnTime_TimeText(string ExpectedText)
		{
			MoveToElementInPage(Runontime_Time);
			ValidateElementValue(Runontime_Time, ExpectedText);

			return this;
		}

		public FinanceExtractRecordPage InsertTextOnRunonTime_Time(string TextToInsert)
		{
			WaitForElementToBeClickable(Runontime_Time);
			SendKeys(Runontime_Time, TextToInsert);

			return this;
		}

		public FinanceExtractRecordPage ClickRunOnTime_Time_TimePicker()
		{
			WaitForElementToBeClickable(Runontime_Time_TimePicker);
			Click(Runontime_Time_TimePicker);

			return this;
		}

		public FinanceExtractRecordPage SelectBatchStatus(string TextToSelect)
		{
			WaitForElementToBeClickable(BatchStatusId);
			SelectPicklistElementByText(BatchStatusId, TextToSelect);

			return this;
		}

		public FinanceExtractRecordPage ValidateBatchStatusSelectedText(string ExpectedText)
		{
			WaitForElementVisible(BatchStatusId);
			ValidatePicklistSelectedText(BatchStatusId, ExpectedText);

			return this;
		}

		public FinanceExtractRecordPage ValidateBatchNumberText(string ExpectedText)
		{
			MoveToElementInPage(BatchId);
			WaitForElementVisible(BatchId);
			ValidateElementValue(BatchId, ExpectedText);

			return this;
		}

		public FinanceExtractRecordPage ClickFinanceExtractBatchSetupLink()
		{
			WaitForElementToBeClickable(FinanceExtractBatchSetupLink);
			Click(FinanceExtractBatchSetupLink);

			return this;
		}

		public FinanceExtractRecordPage ValidateFinanceExtractBatchSetupLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(FinanceExtractBatchSetupLink);
			ValidateElementText(FinanceExtractBatchSetupLink, ExpectedText);

			return this;
		}

		public FinanceExtractRecordPage ClickFinanceExtractBatchSetupLookupButton()
		{
			WaitForElementToBeClickable(FinanceExtractBatchSetupLookupButton);
			Click(FinanceExtractBatchSetupLookupButton);

			return this;
		}

		public FinanceExtractRecordPage ClickIsAdHoc_YesButton()
		{
			WaitForElementToBeClickable(Isadhoc_1);
			Click(Isadhoc_1);

			return this;
		}

		public FinanceExtractRecordPage ValidateIsAdHoc_YesChecked()
		{
			WaitForElement(Isadhoc_1);
			ValidateElementChecked(Isadhoc_1);
			
			return this;
		}

		public FinanceExtractRecordPage ValidateIsAdHoc_YesNotChecked()
		{
			WaitForElement(Isadhoc_1);
			ValidateElementNotChecked(Isadhoc_1);
			
			return this;
		}

		public FinanceExtractRecordPage ClickIsAdHoc_NoButton()
		{
			WaitForElementToBeClickable(Isadhoc_0);
			Click(Isadhoc_0);

			return this;
		}

		public FinanceExtractRecordPage ValidateIsAdHoc_NoChecked()
		{
			WaitForElement(Isadhoc_0);
			ValidateElementChecked(Isadhoc_0);
			
			return this;
		}

		public FinanceExtractRecordPage ValidateIsAdHoc_NoNotChecked()
		{
			WaitForElement(Isadhoc_0);
			ValidateElementNotChecked(Isadhoc_0);
			
			return this;
		}

		public FinanceExtractRecordPage ClickFinanceInvoicesTab()
        {
			WaitForElementToBeClickable(FinanceInvoicesTab);
			Click(FinanceInvoicesTab);

			return this;
        }

	}
}
