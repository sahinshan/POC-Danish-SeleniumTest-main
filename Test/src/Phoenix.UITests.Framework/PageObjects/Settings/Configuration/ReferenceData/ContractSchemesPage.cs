using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ContractSchemesPage : CommonMethods
    {
        public ContractSchemesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CareProviderContractScheme = By.Id("iframe_careprovidercontractscheme");

        readonly By PageHeader = By.XPath("//*[@id='CWToolbar']//h1[text()='Contract Schemes']");

		readonly By BackButton = By.Id("BackButton");
		readonly By NewRecordButton = By.Id("TI_NewRecordButton");
		readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
		readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
		readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
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


		public ContractSchemesPage WaitForContractSchemesPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElement(contentIFrame);
			SwitchToIframe(contentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 30);

			WaitForElementVisible(PageHeader);

			WaitForElementVisible(NewRecordButton);
			WaitForElementVisible(ExportToExcelButton);
			WaitForElementVisible(AssignRecordButton);

			return this;
		}

		public ContractSchemesPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			MoveToElementInPage(BackButton);
			Click(BackButton);

			return this;
		}

		public ContractSchemesPage ClickCreateNewRecordButton()
		{
			WaitForElementToBeClickable(NewRecordButton);
			MoveToElementInPage(NewRecordButton);
			Click(NewRecordButton);

			return this;
		}

		public ContractSchemesPage ClickExportToExcelButton()
		{
			WaitForElementToBeClickable(ExportToExcelButton);
			MoveToElementInPage(ExportToExcelButton);
			Click(ExportToExcelButton);

			return this;
		}

		public ContractSchemesPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			MoveToElementInPage(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public ContractSchemesPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			MoveToElementInPage(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public ContractSchemesPage SelectSystemView(string TextToSelect)
		{
			WaitForElementToBeClickable(ViewSelector);
			MoveToElementInPage(ViewSelector);
			SelectPicklistElementByText(ViewSelector, TextToSelect);

			return this;
		}

		public ContractSchemesPage ValidateSelectedSystemView(string ExpectedText)
		{
			WaitForElementToBeClickable(ViewSelector);
			MoveToElementInPage(ViewSelector);
			ValidatePicklistSelectedText(ViewSelector, ExpectedText);

			return this;
		}

		public ContractSchemesPage ValidateQuickSearchText(string ExpectedText)
		{
			WaitForElementVisible(QuickSearchTextArea);
			MoveToElementInPage(QuickSearchTextArea);
			ValidateElementValue(QuickSearchTextArea, ExpectedText);

			return this;
		}

		public ContractSchemesPage InsertTextOnQuickSearch(string TextToInsert)
		{
			WaitForElementToBeClickable(QuickSearchTextArea);
			MoveToElementInPage(QuickSearchTextArea);
			SendKeys(QuickSearchTextArea, TextToInsert);

			return this;
		}

		public ContractSchemesPage ClickQuickSearchButton()
		{
			WaitForElementToBeClickable(QuickSearchButton);
			MoveToElementInPage(QuickSearchButton);
			Click(QuickSearchButton);

			return this;
		}

		public ContractSchemesPage ClickRefreshButton()
		{
			WaitForElementToBeClickable(RefreshButton);
			MoveToElementInPage(RefreshButton);
			Click(RefreshButton);

			return this;
		}

		public ContractSchemesPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
		{
			WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
			ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

			return this;
		}

		public ContractSchemesPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
		{
			ScrollToElement(headerCell(CellPosition));
			WaitForElementVisible(headerCell(CellPosition));
			ValidateElementText(headerCell(CellPosition), ExpectedText);

			return this;
		}

		public ContractSchemesPage ValidateRecordPresent(string RecordID, bool ExpectedVisibility)
		{
			if (ExpectedVisibility)
				WaitForElementVisible(RecordIdentifier(RecordID));
			else
				WaitForElementNotVisible(RecordIdentifier(RecordID), 7);


			return this;
		}

		public ContractSchemesPage OpenContractSchemeRecord(string RecordID)
		{
			WaitForElementToBeClickable(RecordIdentifier(RecordID));
			MoveToElementInPage(RecordIdentifier(RecordID));
			Click(RecordIdentifier(RecordID));

			return this;
		}

        public ContractSchemesPage OpenContractSchemeRecord(Guid RecordID)
        {
            return OpenContractSchemeRecord(RecordID.ToString());
        }

        public ContractSchemesPage ValidateNoRecordsMessageVisible()
		{
			WaitForElementVisible(noRecordsMessage);
			WaitForElementVisible(noResultsMessage);

			return this;
		}

		public ContractSchemesPage SelectContractSchemeRecord(string RecordId)
		{
			WaitForElementToBeClickable(recordRowCheckBox(RecordId));
			MoveToElementInPage(recordRowCheckBox(RecordId));
			Click(recordRowCheckBox(RecordId));

			return this;
		}

		public ContractSchemesPage ValidateHeaderCellSortOrdedAscending(int CellPosition, string headerName)
		{
			ScrollToElement(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
			WaitForElementVisible(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName));
			ValidateElementAttribute(gridPageHeaderElementSortAscendingOrder(CellPosition, headerName), "class", "sortasc");

			return this;
		}

	}
}
