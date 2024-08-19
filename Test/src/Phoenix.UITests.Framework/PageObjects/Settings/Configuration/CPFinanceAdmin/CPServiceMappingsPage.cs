using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
	public class CPServiceMappingsPage : CommonMethods
	{
		public CPServiceMappingsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By CWContentIFrame = By.Id("CWContentIFrame");
		readonly By CareProviderService_CWDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderservice&')]");
		readonly By ServiceMappingsPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
		readonly By UrlPanelIFrame = By.Id("CWUrlPanel_IFrame");

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
		By gridPageHeaderElement(string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']/th//a/span[text() = '" + ColumnName + "']");

		By gridPageHeaderElementSortOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2]");

		By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortasc']");
		By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2][@class = 'sortdesc']");
		By gridPageHeaderElementSortAscendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortasc']");
		By gridPageHeaderElementSortDescendingOrder(string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");

		public CPServiceMappingsPage WaitForCPServiceMappingsPageToLoad()
		{
			SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElement(CWContentIFrame);
			SwitchToIframe(CWContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElement(CareProviderService_CWDialogIFrame);
			SwitchToIframe(CareProviderService_CWDialogIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElement(UrlPanelIFrame);
			SwitchToIframe(UrlPanelIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 100);

			WaitForElementVisible(ServiceMappingsPageHeader);

			return this;
		}

		public CPServiceMappingsPage ClickNewRecordButton()
		{
			WaitForElementToBeClickable(NewRecordButton);
			Click(NewRecordButton);

			return this;
		}

		public CPServiceMappingsPage ClickExportToExcelButton()
		{
			WaitForElementToBeClickable(ExportToExcelButton);
			Click(ExportToExcelButton);

			return this;
		}

		public CPServiceMappingsPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public CPServiceMappingsPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

		public CPServiceMappingsPage SelectSystemView(string TextToSelect)
		{
			WaitForElementToBeClickable(CWViewSelector);
			SelectPicklistElementByText(CWViewSelector, TextToSelect);

			return this;
		}

		public CPServiceMappingsPage ValidateSystemViewSelectedText(string ExpectedText)
		{
			ValidateElementText(CWViewSelector, ExpectedText);

			return this;
		}

		public CPServiceMappingsPage ValidateCWQuickSearchText(string ExpectedText)
		{
			ValidateElementValue(CWQuickSearch, ExpectedText);

			return this;
		}

		public CPServiceMappingsPage InsertTextOnQuickSearch(string TextToInsert)
		{
			WaitForElementToBeClickable(CWQuickSearch);
			SendKeys(CWQuickSearch, TextToInsert);
			
			return this;
		}

		public CPServiceMappingsPage ClickQuickSearchButton()
		{
			WaitForElementToBeClickable(CWQuickSearchButton);
			Click(CWQuickSearchButton);

			return this;
		}

		public CPServiceMappingsPage ClickRefreshButton()
		{
			WaitForElementToBeClickable(CWRefreshButton);
			Click(CWRefreshButton);

			return this;
		}

		public CPServiceMappingsPage ValidateRecordVisible(Guid RecordID)
		{
			WaitForElementVisible(RecordCheckBox(RecordID.ToString()));

			return this;
		}

		public CPServiceMappingsPage ValidateRecordNotVisible(Guid RecordID)
		{
			WaitForElementNotVisible(RecordCheckBox(RecordID.ToString()), 7);

			return this;
		}

		public CPServiceMappingsPage SelectRecord(Guid RecordID)
		{
			WaitForElementToBeClickable(RecordCheckBox(RecordID.ToString()));
			Click(RecordCheckBox(RecordID.ToString()));

			return this;
		}

		public CPServiceMappingsPage OpenRecord(Guid RecordID)
		{
			WaitForElementToBeClickable(RecordIdentifier(RecordID.ToString()));
			Click(RecordIdentifier(RecordID.ToString()));

			return this;
		}

		public CPServiceMappingsPage OpenRecord(int RecordPosition, Guid RecordID)
		{
			WaitForElementToBeClickable(RecordIdentifier(RecordPosition, RecordID.ToString()));
			Click(RecordIdentifier(RecordPosition, RecordID.ToString()));

			return this;
		}

		public CPServiceMappingsPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
		{
			ValidateElementText(RecordCell(RecordID.ToString(), CellPosition), ExpectedText);

			return this;
		}

		public CPServiceMappingsPage RecordsPageValidateHeaderCellText(int CellPosition, string ExpectedText)
		{
			ScrollToElement(gridPageHeaderElement(CellPosition));
			WaitForElementVisible(gridPageHeaderElement(CellPosition));
			ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

			return this;
		}

		public CPServiceMappingsPage RecordsPageValidateHeaderCellText(string ExpectedText)
		{
			ScrollToElement(gridPageHeaderElement(ExpectedText));
			WaitForElementVisible(gridPageHeaderElement(ExpectedText));
			ValidateElementText(gridPageHeaderElement(ExpectedText), ExpectedText);

			return this;
		}

		public CPServiceMappingsPage ClickColumnHeader(int CellPosition)
		{
			ScrollToElement(gridPageHeaderElement(CellPosition));
			WaitForElementVisible(gridPageHeaderElement(CellPosition));
			Click(gridPageHeaderElement(CellPosition));

			return this;
		}

		public CPServiceMappingsPage ClickColumnHeader(string ColumnName)
		{
			ScrollToElement(gridPageHeaderElement(ColumnName));
			WaitForElementVisible(gridPageHeaderElement(ColumnName));
			Click(gridPageHeaderElement(ColumnName));

			return this;
		}

		public CPServiceMappingsPage ClickColumnHeaderSortDescendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			Click(gridPageHeaderElementSortAscendingOrder(CellPosition));

			WaitForElementNotVisible("CWRefreshPanel", 7);

			return this;
		}

		public CPServiceMappingsPage ClickColumnHeaderSortDescendingOrder(string ColumnName)
		{
			WaitForElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
			ScrollToElement(gridPageHeaderElementSortAscendingOrder(ColumnName));
			WaitForElementToBeClickable(gridPageHeaderElementSortAscendingOrder(ColumnName));
			Click(gridPageHeaderElementSortAscendingOrder(ColumnName));

			WaitForElementNotVisible("CWRefreshPanel", 7);

			return this;
		}

		public CPServiceMappingsPage ClickColumnHeaderSortAscendingOrder(string ColumnName)
		{
			WaitForElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
			ScrollToElement(gridPageHeaderElementSortDescendingOrder(ColumnName));
			WaitForElementToBeClickable(gridPageHeaderElementSortDescendingOrder(ColumnName));
			Click(gridPageHeaderElementSortDescendingOrder(ColumnName));

			WaitForElementNotVisible("CWRefreshPanel", 7);

			return this;
		}

		public CPServiceMappingsPage ResultsPageValidateHeaderCellSortAscendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

			return this;
		}

		public CPServiceMappingsPage ClickColumnHeaderSortAscendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			Click(gridPageHeaderElementSortDescendingOrder(CellPosition));

			WaitForElementNotVisible("CWRefreshPanel", 7);

			return this;
		}

		public CPServiceMappingsPage ResultsPageValidateHeaderCellSortDescendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");

			return this;
		}

	}
}
