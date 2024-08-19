using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class FinanceCodeLocationsPage : CommonMethods
	{
		public FinanceCodeLocationsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By contentIFrame = By.Id("CWContentIFrame");
		readonly By iframe_CareProviderFinanceCodeLocations = By.Id("iframe_careproviderfinancecodelocation");

		readonly By PageHeader = By.XPath("//*[@id='CWToolbar']//h1[text()='Finance Code Locations']");

		readonly By BackButton = By.Id("BackButton");
		readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
		readonly By AddRemoveBookmarkButton = By.Id("TI_OpenBookmarkDialog");
		readonly By ViewSelector = By.Id("CWViewSelector");
		readonly By QuickSearchTextArea = By.Id("CWQuickSearch");
		readonly By QuickSearchButton = By.Id("CWQuickSearchButton");
		readonly By RefreshButton = By.Id("CWRefreshButton");

		By headerCell(int CellPosition) => By.XPath("//table[@id='CWGridHeader']//tr/th[" + CellPosition + "]/a");

		By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");

		By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

		By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");

		By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortasc']");


		readonly By noRecordsMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
		readonly By noResultsMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


		public FinanceCodeLocationsPage WaitForFinanceCodeLocationsPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElement(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElement(iframe_CareProviderFinanceCodeLocations);
			SwitchToIframe(iframe_CareProviderFinanceCodeLocations);

			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElementVisible(PageHeader);

			WaitForElementVisible(ExportToExcelButton);
			WaitForElementVisible(AddRemoveBookmarkButton);

			return this;
		}

		public FinanceCodeLocationsPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			MoveToElementInPage(BackButton);
			Click(BackButton);

			return this;
		}

		public FinanceCodeLocationsPage ClickExportToExcelButton()
		{
			WaitForElementToBeClickable(ExportToExcelButton);
			MoveToElementInPage(ExportToExcelButton);
			Click(ExportToExcelButton);

			return this;
		}

		public FinanceCodeLocationsPage SelectSystemView(string TextToSelect)
		{
			WaitForElementToBeClickable(ViewSelector);
			MoveToElementInPage(ViewSelector);
			SelectPicklistElementByText(ViewSelector, TextToSelect);

			return this;
		}

		public FinanceCodeLocationsPage ValidateSelectedSystemView(string ExpectedText)
		{
			WaitForElementToBeClickable(ViewSelector);
			MoveToElementInPage(ViewSelector);
			ValidatePicklistSelectedText(ViewSelector, ExpectedText);

			return this;
		}

		public FinanceCodeLocationsPage InsertTextOnQuickSearch(string TextToInsert)
		{
			WaitForElementToBeClickable(QuickSearchTextArea);
			MoveToElementInPage(QuickSearchTextArea);
			SendKeys(QuickSearchTextArea, TextToInsert);

			return this;
		}

		public FinanceCodeLocationsPage ClickQuickSearchButton()
		{
			WaitForElementToBeClickable(QuickSearchButton);
			MoveToElementInPage(QuickSearchButton);
			Click(QuickSearchButton);

			return this;
		}

		public FinanceCodeLocationsPage ClickRefreshButton()
		{
			WaitForElementToBeClickable(RefreshButton);
			MoveToElementInPage(RefreshButton);
			Click(RefreshButton);

			return this;
		}

		public FinanceCodeLocationsPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
		{
			WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
			ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

			return this;
		}

		public FinanceCodeLocationsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
		{
			ScrollToElement(headerCell(CellPosition));
			WaitForElementVisible(headerCell(CellPosition));
			ValidateElementText(headerCell(CellPosition), ExpectedText);

			return this;
		}

		public FinanceCodeLocationsPage ValidateRecordIsPresentOrNot(string RecordID, bool ExpectedVisibility)
		{
			if (ExpectedVisibility)
				WaitForElementVisible(RecordIdentifier(RecordID));
			else
				WaitForElementNotVisible(RecordIdentifier(RecordID), 7);


			return this;
		}

		public FinanceCodeLocationsPage OpenFinanceCodeLocationRecord(string RecordID)
		{
			WaitForElementToBeClickable(RecordIdentifier(RecordID));
			MoveToElementInPage(RecordIdentifier(RecordID));
			Click(RecordIdentifier(RecordID));

			return this;
		}

		public FinanceCodeLocationsPage ValidateNoRecordsMessageVisible()
		{
			WaitForElementVisible(noRecordsMessage);
			WaitForElementVisible(noResultsMessage);

			return this;
		}

		public FinanceCodeLocationsPage SelectFinanceCodeLocationRecord(string RecordId)
		{
			WaitForElementToBeClickable(recordRowCheckBox(RecordId));
			MoveToElementInPage(recordRowCheckBox(RecordId));
			Click(recordRowCheckBox(RecordId));

			return this;
		}

		public FinanceCodeLocationsPage ValidateHeaderCellSortOrdedAscending(int CellPosition, string headerName)
		{
			ScrollToElement(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
			WaitForElementVisible(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
			ValidateElementAttribute(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName), "class", "sortasc");

			return this;
		}

	}
}
