using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Finance
{
	public class ServiceUpratesPage : CommonMethods
	{
		public ServiceUpratesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By CWContentIFrame = By.Id("CWContentIFrame");
		readonly By pageHeader = By.XPath("//h1");
		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By NewRecordButton = By.XPath("//*[@id='TI_NewRecordButton']");
		readonly By ExportToExcelButton = By.XPath("//*[@id='TI_ExportToExcelButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By GenerateButton = By.XPath("//*[@id='TI_GenerateButton']");
		readonly By ToolbarMenuButton = By.Id("CWToolbarMenu");
		readonly By ProcessButton = By.Id("TI_ProcessButton");
		readonly By ViewSelector = By.XPath("//*[@id='CWViewSelector']");
		readonly By QuickSearch = By.XPath("//*[@id='CWQuickSearch']");
		readonly By QuickSearchButton = By.XPath("//*[@id='CWQuickSearchButton']");
		readonly By RefreshButton = By.XPath("//*[@id='CWRefreshButton']");

		By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");
		By RecordIdentifierCheckbox(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[1]");

		By recordRowCell(int RowPosition, int Cellposition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RowPosition + "]/td[" + Cellposition + "]");

		public ServiceUpratesPage WaitForServiceUpratePageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElement(CWContentIFrame);
			SwitchToIframe(CWContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 60);

			WaitForElement(pageHeader);
			return this;
		}

		public ServiceUpratesPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public ServiceUpratesPage ClickNewRecordButton()
		{
			WaitForElementToBeClickable(NewRecordButton);
			Click(NewRecordButton);

			return this;
		}

		public ServiceUpratesPage ClickExportToExcelButton()
		{
			WaitForElementToBeClickable(ExportToExcelButton);
			Click(ExportToExcelButton);

			return this;
		}

		public ServiceUpratesPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public ServiceUpratesPage ClickGenerateButton()
		{
			WaitForElementToBeClickable(GenerateButton);
			Click(GenerateButton);

			return this;
		}

		public ServiceUpratesPage ClickProcessButton()
        {
			WaitForElementToBeClickable(ToolbarMenuButton);
			MoveToElementInPage(ToolbarMenuButton);
			Click(ToolbarMenuButton);

			WaitForElementToBeClickable(ProcessButton);
			MoveToElementInPage(ProcessButton);
			Click(ProcessButton);

			return this;
        }

		public ServiceUpratesPage SelectViewSelector(string TextToSelect)
		{
			WaitForElementToBeClickable(ViewSelector);
			SelectPicklistElementByText(ViewSelector, TextToSelect);

			return this;
		}

		public ServiceUpratesPage ValidateViewSelectorSelectedText(string ExpectedText)
		{
			WaitForElementVisible(ViewSelector);
			ValidateElementText(ViewSelector, ExpectedText);

			return this;
		}

		public ServiceUpratesPage ValidateQuickSearchText(string ExpectedText)
		{
			WaitForElementVisible(QuickSearch);
			ValidateElementValue(QuickSearch, ExpectedText);

			return this;
		}

		public ServiceUpratesPage InsertTextOnQuickSearch(string TextToInsert)
		{
			WaitForElementToBeClickable(QuickSearch);
			SendKeys(QuickSearch, TextToInsert);
			
			return this;
		}

		public ServiceUpratesPage ClickQuickSearchButton()
		{
			WaitForElementToBeClickable(QuickSearchButton);
			Click(QuickSearchButton);

			return this;
		}

		public ServiceUpratesPage ClickRefreshButton()
		{
			WaitForElementToBeClickable(RefreshButton);
			Click(RefreshButton);

			return this;
		}

		public ServiceUpratesPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
				WaitForElementToBeClickable(RecordIdentifier(RecordID));
				MoveToElementInPage(RecordIdentifier(RecordID));
			}
			else
			{
				WaitForElementNotVisible(RecordIdentifier(RecordID), 3);
			}
			Assert.AreEqual(ExpectedVisible, GetElementVisibility(RecordIdentifier(RecordID)));
			return this;
		}

		public ServiceUpratesPage OpenRecord(string RecordID)
		{
			MoveToElementInPage(RecordIdentifier(RecordID));
			WaitForElementToBeClickable(RecordIdentifier(RecordID));

			Click(RecordIdentifier(RecordID));

			return this;
		}

		public ServiceUpratesPage SelectRecord(string RecordID)
		{
			WaitForElementToBeClickable(RecordIdentifierCheckbox(RecordID));
			MoveToElementInPage(RecordIdentifierCheckbox(RecordID));
			Click(RecordIdentifierCheckbox(RecordID));

			return this;
		}

		public ServiceUpratesPage ValidateRecordCellContent(int RowPosition, int Cellposition, string ExpectedText)
		{
			ScrollToElement(recordRowCell(RowPosition, Cellposition));
			WaitForElementToBeClickable(recordRowCell(RowPosition, Cellposition));
			MoveToElementInPage(recordRowCell(RowPosition, Cellposition));
			ValidateElementText(recordRowCell(RowPosition, Cellposition), ExpectedText);

			return this;
		}
	}
}
