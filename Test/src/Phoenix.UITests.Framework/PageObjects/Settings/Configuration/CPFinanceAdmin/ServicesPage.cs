using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
	public class ServicesPage : CommonMethods
	{

		public ServicesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By CWContentIFrame = By.Id("CWContentIFrame");
		readonly By ServicesPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

		readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By NewRecordButton = By.XPath("//*[@id='TI_NewRecordButton']");
		readonly By ExportToExcelButton = By.XPath("//*[@id='TI_ExportToExcelButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
		readonly By ViewSelector = By.XPath("//*[@id='CWViewSelector']");
		readonly By QuickSearch = By.XPath("//*[@id='CWQuickSearch']");
		readonly By QuickSearchButton = By.XPath("//*[@id='CWQuickSearchButton']");
		readonly By RefreshButton = By.XPath("//*[@id='CWRefreshButton']");

		By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
		By recordRowCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
		By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");
		By RecordIdentifier(int RecordPosition, string RecordID) => By.XPath("//tr["+ RecordPosition +"][@id='" + RecordID + "']/td[2]");

		By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
		By gridPageHeaderElement(string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']/th//a/span[text() = '" + ColumnName + "']");

		By gridPageHeaderElementSortOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2]");

		By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortasc']");
		By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortdesc']");
		By gridPageHeaderElementSortAscendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th//a[@title = 'Sort by "+ FieldName + "']//span[2][@class = 'sortasc']");
		By gridPageHeaderElementSortDescendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th//a[@title = 'Sort by " + FieldName + "']//span[2][@class = 'sortdesc']");

		public ServicesPage WaitForServicesPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElement(CWContentIFrame);
			SwitchToIframe(CWContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(ServicesPageHeader);
			WaitForElementVisible(NewRecordButton);
			WaitForElementVisible(ExportToExcelButton);
			WaitForElementVisible(AssignRecordButton);
			WaitForElementVisible(DeleteRecordButton);

			return this;
		}

		public ServicesPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public ServicesPage ClickNewRecordButton()
		{
			WaitForElementToBeClickable(NewRecordButton);
			ScrollToElement(NewRecordButton);
			Click(NewRecordButton);

			return this;
		}

		public ServicesPage ClickExportToExcelButton()
		{
			WaitForElementToBeClickable(ExportToExcelButton);
			ScrollToElement(ExportToExcelButton);
			Click(ExportToExcelButton);

			return this;
		}

		public ServicesPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			ScrollToElement(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public ServicesPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			ScrollToElement(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public ServicesPage SelectSystemView(string TextToSelect)
		{
			WaitForElementToBeClickable(ViewSelector);
			SelectPicklistElementByText(ViewSelector, TextToSelect);

			return this;
		}

		public ServicesPage ValidateSelectedSystemView(string ExpectedText)
		{

			ValidatePicklistSelectedText(ViewSelector, ExpectedText);

			return this;
		}

		public ServicesPage InsertTextOnQuickSearch(string TextToInsert)
		{
			WaitForElementToBeClickable(QuickSearch);
			ScrollToElement(QuickSearch);
			SendKeys(QuickSearch, TextToInsert);
			
			return this;
		}

		public ServicesPage ClickQuickSearchButton()
		{
			WaitForElementToBeClickable(QuickSearchButton);
			ScrollToElement(QuickSearchButton);
			Click(QuickSearchButton);

			return this;
		}

		public ServicesPage ClickRefreshButton()
		{
			WaitForElementToBeClickable(RefreshButton);
			ScrollToElement(RefreshButton);
			Click(RefreshButton);

			return this;
		}

		public ServicesPage SelectServicesRecord(string RecordId)
		{
			WaitForElementToBeClickable(recordRowCheckBox(RecordId));
			ScrollToElement(recordRowCheckBox(RecordId));
			Click(recordRowCheckBox(RecordId));

			return this;
		}

		public ServicesPage ValidateRecordCellContent(string RecordId, int CellPosition, string ExpectedText)
		{
			WaitForElementToBeClickable(recordRowCell(RecordId, CellPosition));
			ValidateElementText(recordRowCell(RecordId, CellPosition), ExpectedText);

			return this;
		}

		public ServicesPage OpenRecord(string RecordID)
		{
			WaitForElementToBeClickable(RecordIdentifier(RecordID));
			ScrollToElement(RecordIdentifier(RecordID));
			Click(RecordIdentifier(RecordID));

			return this;
		}

		public ServicesPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
				WaitForElementToBeClickable(RecordIdentifier(RecordID));
				ScrollToElement(RecordIdentifier(RecordID));
			}
			else
				WaitForElementNotVisible(RecordIdentifier(RecordID), 3);

			Assert.AreEqual(ExpectedVisible, GetElementVisibility(RecordIdentifier(RecordID)));

			return this;
		}

		public ServicesPage ValidateRecordIsPresent(int RecordPosition, string RecordID, bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
				WaitForElementToBeClickable(RecordIdentifier(RecordPosition, RecordID));
				ScrollToElement(RecordIdentifier(RecordPosition, RecordID));
			}
			else
				WaitForElementNotVisible(RecordIdentifier(RecordPosition, RecordID), 3);

			Assert.AreEqual(ExpectedVisible, GetElementVisibility(RecordIdentifier(RecordPosition, RecordID)));

			return this;
		}

		public ServicesPage RecordsPageValidateHeaderCellText(int CellPosition, string ExpectedText)
		{
			ScrollToElement(gridPageHeaderElement(CellPosition));
			WaitForElementVisible(gridPageHeaderElement(CellPosition));
			ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

			return this;
		}

		public ServicesPage RecordsPageValidateHeaderCellText(string ExpectedText)
		{
			ScrollToElement(gridPageHeaderElement(ExpectedText));
			WaitForElementVisible(gridPageHeaderElement(ExpectedText));
			ValidateElementText(gridPageHeaderElement(ExpectedText), ExpectedText);

			return this;
		}

		public ServicesPage ClickColumnHeader(int CellPosition)
		{
			ScrollToElement(gridPageHeaderElement(CellPosition));
			WaitForElementVisible(gridPageHeaderElement(CellPosition));
			Click(gridPageHeaderElement(CellPosition));

			return this;
		}

		public ServicesPage ClickColumnHeader(string ColumnName)
		{
			ScrollToElement(gridPageHeaderElement(ColumnName));
			WaitForElementVisible(gridPageHeaderElement(ColumnName));
			Click(gridPageHeaderElement(ColumnName));

			return this;
		}

		public ServicesPage ClickColumnHeaderSortDescendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			Click(gridPageHeaderElementSortAscendingOrder(CellPosition));

			WaitForElementNotVisible("CWRefreshPanel", 7);

			return this;
		}

		public ServicesPage ClickColumnHeaderSortDescendingOrder(string ColumnName)
		{
			WaitForElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
			ScrollToElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
			WaitForElementToBeClickable(gridPageHeaderElementSortAscendingOrder(ColumnName));
			Click(gridPageHeaderElementSortAscendingOrder(ColumnName));

			WaitForElementNotVisible("CWRefreshPanel", 7);

			return this;
		}

		public ServicesPage ClickColumnHeaderSortAscendingOrder(string ColumnName)
		{
			WaitForElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
			ScrollToElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
			WaitForElementToBeClickable(gridPageHeaderElementSortDescendingOrder(ColumnName));
			Click(gridPageHeaderElementSortDescendingOrder(ColumnName));

			WaitForElementNotVisible("CWRefreshPanel", 7);

			return this;
		}

		public ServicesPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

			return this;
		}

		public ServicesPage ClickColumnHeaderSortAscendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			Click(gridPageHeaderElementSortDescendingOrder(CellPosition));

			WaitForElementNotVisible("CWRefreshPanel", 7);

			return this;
		}

		public ServicesPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");

			return this;
		}
	}
}
