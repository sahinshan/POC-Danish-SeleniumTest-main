using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
	public class CPFinanceExtractBatchSetupsPage : CommonMethods
	{
		public CPFinanceExtractBatchSetupsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By CWContentIFrame = By.Id("CWContentIFrame");		
		readonly By FinanceExtractBatchSetupPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

		readonly By NewRecordButton = By.XPath("//*[@id='TI_NewRecordButton']");
		readonly By ExportToExcelButton = By.XPath("//*[@id='TI_ExportToExcelButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
		readonly By CWViewSelector = By.XPath("//*[@id='CWViewSelector']");
		readonly By CWQuickSearch = By.XPath("//*[@id='CWQuickSearch']");
		readonly By CWQuickSearchButton = By.XPath("//*[@id='CWQuickSearchButton']");
		readonly By CWRefreshButton = By.XPath("//*[@id='CWRefreshButton']");

		By RecordCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
		By RecordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
		By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");

		By RecordIdentifier(int RecordPosition, string RecordID) => By.XPath("//tr[" + RecordPosition + "][@id='" + RecordID + "']/td[2]");

		By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
		By gridPageHeaderElement(string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a/span[text() = '" + ColumnName + "']");

		By gridPageHeaderElementSortOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2]");
		By gridPageHeaderElementSortOrder(int HeaderCellPosition, string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[text() = '"+ ColumnName + "']");


		By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortasc']");
		By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortdesc']");
		By gridPageHeaderElementSortAscendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortasc']");
		By gridPageHeaderElementSortDescendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");

		public CPFinanceExtractBatchSetupsPage WaitForCPFinanceExtractBatchSetupsPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElement(CWContentIFrame);
			SwitchToIframe(CWContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(FinanceExtractBatchSetupPageHeader);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ValidateToolbarOptionsDisplayed()
        {
			WaitForElementVisible(NewRecordButton);
			WaitForElementVisible(ExportToExcelButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);

			return this;
        }

		public CPFinanceExtractBatchSetupsPage ClickNewRecordButton()
		{
			WaitForElementToBeClickable(NewRecordButton);
			Click(NewRecordButton);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ClickExportToExcelButton()
		{
			WaitForElementToBeClickable(ExportToExcelButton);
			Click(ExportToExcelButton);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage SelectSystemView(string TextToSelect)
		{
			WaitForElementToBeClickable(CWViewSelector);
			SelectPicklistElementByText(CWViewSelector, TextToSelect);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ValidateSystemViewSelectedText(string ExpectedText)
		{
			ValidateElementText(CWViewSelector, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ValidateSystemViewOptionNotPresent(string optionNotPresent)
		{
			ScrollToElement(CWViewSelector);
			ValidatePicklistDoesNotContainsElementByText(CWViewSelector, optionNotPresent);
			return this;
		}

		public CPFinanceExtractBatchSetupsPage ValidateQuickSearchText(string ExpectedText)
		{
			ValidateElementValue(CWQuickSearch, ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage InsertTextOnQuickSearch(string TextToInsert)
		{
			ScrollToElement(CWQuickSearch);
			WaitForElementToBeClickable(CWQuickSearch);
			SendKeys(CWQuickSearch, TextToInsert);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ClickQuickSearchButton()
		{
			WaitForElementToBeClickable(CWQuickSearchButton);
			Click(CWQuickSearchButton);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ClickRefreshButton()
		{
			WaitForElementToBeClickable(CWRefreshButton);
			Click(CWRefreshButton);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ValidateRecordVisible(Guid RecordID)
		{
			WaitForElementVisible(RecordCheckBox(RecordID.ToString()));

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ValidateRecordVisible(int RecordPosition, string RecordID, bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
				WaitForElementToBeClickable(RecordIdentifier(RecordPosition, RecordID));
				ScrollToElement(RecordIdentifier(RecordPosition, RecordID));
			}
			else
				WaitForElementNotVisible(RecordIdentifier(RecordPosition, RecordID), 3);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ValidateRecordNotVisible(Guid RecordID)
		{
			WaitForElementNotVisible(RecordCheckBox(RecordID.ToString()), 7);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage SelectRecord(Guid RecordID)
		{
			WaitForElementToBeClickable(RecordCheckBox(RecordID.ToString()));
			Click(RecordCheckBox(RecordID.ToString()));

			return this;
		}

		public CPFinanceExtractBatchSetupsPage OpenRecord(Guid RecordID)
		{
			WaitForElementToBeClickable(RecordIdentifier(RecordID.ToString()));
			ScrollToElement(RecordIdentifier(RecordID.ToString()));
			Click(RecordIdentifier(RecordID.ToString()));

			return this;
		}

		public CPFinanceExtractBatchSetupsPage OpenRecord(int RecordPosition, Guid RecordID)
		{
			WaitForElementToBeClickable(RecordIdentifier(RecordPosition, RecordID.ToString()));
			Click(RecordIdentifier(RecordPosition, RecordID.ToString()));

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
		{
			ValidateElementText(RecordCell(RecordID.ToString(), CellPosition), ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage RecordsPageValidateHeaderCellText(int CellPosition, string ExpectedText)
		{
			ScrollToElement(gridPageHeaderElement(CellPosition));
			WaitForElementVisible(gridPageHeaderElement(CellPosition));
			ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage RecordsPageValidateHeaderCellText(string ExpectedText)
		{
			ScrollToElement(gridPageHeaderElement(ExpectedText));
			WaitForElementVisible(gridPageHeaderElement(ExpectedText));
			ValidateElementText(gridPageHeaderElement(ExpectedText), ExpectedText);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ClickColumnHeader(int CellPosition)
		{
			ScrollToElement(gridPageHeaderElement(CellPosition));
			WaitForElementVisible(gridPageHeaderElement(CellPosition));
			Click(gridPageHeaderElement(CellPosition));

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ClickColumnHeader(string ColumnName)
		{
			ScrollToElement(gridPageHeaderElement(ColumnName));
			WaitForElementVisible(gridPageHeaderElement(ColumnName));
			Click(gridPageHeaderElement(ColumnName));

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ClickColumnHeaderSortDescendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			Click(gridPageHeaderElementSortAscendingOrder(CellPosition));

			WaitForElementNotVisible("CWRefreshPanel", 7);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ClickColumnHeaderSortDescendingOrder(string ColumnName)
		{
			WaitForElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
			ScrollToElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
			WaitForElementToBeClickable(gridPageHeaderElementSortAscendingOrder(ColumnName));
			Click(gridPageHeaderElementSortAscendingOrder(ColumnName));

			WaitForElementNotVisible("CWRefreshPanel", 7);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ClickColumnHeaderSortAscendingOrder(string ColumnName)
		{
			WaitForElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
			ScrollToElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
			WaitForElementToBeClickable(gridPageHeaderElementSortDescendingOrder(ColumnName));
			Click(gridPageHeaderElementSortDescendingOrder(ColumnName));

			WaitForElementNotVisible("CWRefreshPanel", 7);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition, string ColumnName)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
			ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ClickColumnHeaderSortAscendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			Click(gridPageHeaderElementSortDescendingOrder(CellPosition));

			WaitForElementNotVisible("CWRefreshPanel", 7);

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");

			return this;
		}

		public CPFinanceExtractBatchSetupsPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition, string ColumnName)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition, ColumnName));
			ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");

			return this;
		}

	}
}
