using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Configuration.CPFinanceAdmin
{
	public class CPMasterPayArrangementsPage : CommonMethods
	{
		public CPMasterPayArrangementsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
		{
			this.driver = driver;
			this.Wait = Wait;
			this.appURL = appURL;
		}

		readonly By CWContentIFrame = By.Id("CWContentIFrame");

		readonly By pageHeader = By.XPath("//*[@id='CWToolbar']//h1");

		readonly By NewRecordButton = By.XPath("//*[@id='TI_NewRecordButton']");
		readonly By ExportToExcelButton = By.XPath("//*[@id='TI_ExportToExcelButton']");
		readonly By CopyRecordButton = By.XPath("//*[@id='TI_CopyRecordButton']");
		readonly By UpliftRatesButton = By.XPath("//*[@id='TI_UpliftRatesButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
		readonly By AdditionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");

        readonly By CWViewSelector = By.XPath("//*[@id='CWViewSelector']");

		readonly By NameField_SearchArea = By.XPath("//*[@id='CWField_careprovidermasterpayarrangement_name']");
		readonly By RateField_SearchArea = By.XPath("//*[@id='CWField_careprovidermasterpayarrangement_defaultrate']");
        readonly By ClearFiltersButton_SearchArea = By.XPath("//*[@id='CWClearFiltersButton']");
		readonly By SearchButton_SearchArea = By.XPath("//*[@id='CWSearchButton']");



		By RecordCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
		By RecordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
		By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBoxSelection(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']");
        By RecordIdentifier(int RecordPosition, string RecordID) => By.XPath("//tr[" + RecordPosition + "][@id='" + RecordID + "']/td[2]");

		By gridPageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
		By gridPageHeaderElement(string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']//a/span[text() = '" + ColumnName + "']");
		By gridPageHeaderElementSortOrder(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[2]");



		public CPMasterPayArrangementsPage WaitForPageToLoad()
        {
            System.Threading.Thread.Sleep(1000);

            SwitchToDefaultFrame();

			WaitForElementNotVisible("CWRefreshPanel", 15);

			WaitForElement(CWContentIFrame);
			SwitchToIframe(CWContentIFrame);

			WaitForElementNotVisible("CWRefreshPanel", 15);

			WaitForElementVisible(pageHeader);

			return this;
		}



        public CPMasterPayArrangementsPage InsertTextOnNameField_SearchArea(string TextToInsert)
        {
            WaitForElementToBeClickable(NameField_SearchArea);
            SendKeys(NameField_SearchArea, TextToInsert);

            return this;
        }

        public CPMasterPayArrangementsPage InsertTextOnRateField_SearchArea(string TextToInsert)
        {
            WaitForElementToBeClickable(RateField_SearchArea);
            SendKeys(RateField_SearchArea, TextToInsert);

            return this;
        }

        public CPMasterPayArrangementsPage ClickSearchButton_SearchArea()
        {
            WaitForElementToBeClickable(SearchButton_SearchArea);
            Click(SearchButton_SearchArea);

            return this;
        }

        public CPMasterPayArrangementsPage ClickClearFiltersButton_SearchArea()
        {
            WaitForElementToBeClickable(ClearFiltersButton_SearchArea);
            Click(ClearFiltersButton_SearchArea);

            return this;
        }




        public CPMasterPayArrangementsPage ValidateToolbarOptionsDisplayed()
        {
			WaitForElementVisible(NewRecordButton);
			WaitForElementVisible(ExportToExcelButton);
			WaitForElementVisible(CopyRecordButton);
			WaitForElementVisible(UpliftRatesButton);
			WaitForElementVisible(DeleteRecordButton);

            return this;
        }
		
		public CPMasterPayArrangementsPage ValidateUpliftRatesIconIsDisplayed()
		{
			WaitForElementVisible(UpliftRatesButton);
			Assert.IsTrue(GetElementByAttributeValue(UpliftRatesButton, "style").Contains("ToolbarIcon_RateUplift.png"));

			return this;

		}

        public CPMasterPayArrangementsPage ValidateToolTipForUpliftRatesButton(String ExpectedText)
        {
            ValidateElementToolTip(UpliftRatesButton, ExpectedText);

            return this;
        }

        public CPMasterPayArrangementsPage ClickNewRecordButton()
		{
			WaitForElementToBeClickable(NewRecordButton);
			Click(NewRecordButton);

			return this;
		}

		public CPMasterPayArrangementsPage ClickExportToExcelButton()
		{
			WaitForElementToBeClickable(ExportToExcelButton);
			Click(ExportToExcelButton);

			return this;
		}

		public CPMasterPayArrangementsPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

        public CPMasterPayArrangementsPage ClickAdditionalItemsButton()
        {
            WaitForElementToBeClickable(AdditionalItemsButton);
            Click(AdditionalItemsButton);

            return this;
        }

        //click UpliftRatesButton
		public CPMasterPayArrangementsPage ClickUpliftRatesButton()
		{
			ClickWithoutWaiting(UpliftRatesButton);

			return this;
		}

        public CPMasterPayArrangementsPage SelectSystemView(string TextToSelect)
		{
			WaitForElementToBeClickable(CWViewSelector);
			SelectPicklistElementByText(CWViewSelector, TextToSelect);

			return this;
		}

		public CPMasterPayArrangementsPage ValidateSystemViewSelectedText(string ExpectedText)
		{
			ValidatePicklistSelectedText(CWViewSelector, ExpectedText);

			return this;
		}

		

		public CPMasterPayArrangementsPage ValidateRecordVisible(Guid RecordID)
		{
			WaitForElementVisible(RecordCheckBox(RecordID.ToString()));

			return this;
		}

		public CPMasterPayArrangementsPage ValidateRecordVisible(int RecordPosition, string RecordID, bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
				WaitForElementToBeClickable(RecordIdentifier(RecordPosition, RecordID));
			}
			else
				WaitForElementNotVisible(RecordIdentifier(RecordPosition, RecordID), 3);

			return this;
		}

		public CPMasterPayArrangementsPage ValidateRecordNotVisible(Guid RecordID)
		{
			WaitForElementNotVisible(RecordCheckBox(RecordID.ToString()), 7);

			return this;
		}

		public CPMasterPayArrangementsPage SelectRecord(Guid RecordID)
		{
			WaitForElementToBeClickable(RecordCheckBox(RecordID.ToString()));
            if(!GetElementByAttributeValue(recordRowCheckBoxSelection(RecordID.ToString()), "class").Contains("selrow"))
                    Click(RecordCheckBox(RecordID.ToString()));

			return this;
		}

		public CPMasterPayArrangementsPage OpenRecord(Guid RecordID)
		{
			WaitForElementToBeClickable(RecordIdentifier(RecordID.ToString()));
			ScrollToElement(RecordIdentifier(RecordID.ToString()));
			Click(RecordIdentifier(RecordID.ToString()));

			return this;
		}

		public CPMasterPayArrangementsPage OpenRecord(int RecordPosition, Guid RecordID)
		{
			WaitForElementToBeClickable(RecordIdentifier(RecordPosition, RecordID.ToString()));
			Click(RecordIdentifier(RecordPosition, RecordID.ToString()));

			return this;
		}

		public CPMasterPayArrangementsPage ValidateRecordCellText(Guid RecordID, int CellPosition, string ExpectedText)
		{
			ValidateElementText(RecordCell(RecordID.ToString(), CellPosition), ExpectedText);

			return this;
		}

		public CPMasterPayArrangementsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
		{
			ScrollToElement(gridPageHeaderElement(CellPosition));
			WaitForElementVisible(gridPageHeaderElement(CellPosition));
			ValidateElementText(gridPageHeaderElement(CellPosition), ExpectedText);

			return this;
		}

		public CPMasterPayArrangementsPage ValidateHeaderCellText(string ExpectedText)
		{
			ScrollToElement(gridPageHeaderElement(ExpectedText));
			WaitForElementVisible(gridPageHeaderElement(ExpectedText));
			ValidateElementText(gridPageHeaderElement(ExpectedText), ExpectedText);

			return this;
		}

		public CPMasterPayArrangementsPage ClickColumnHeader(int CellPosition)
		{
			ScrollToElement(gridPageHeaderElement(CellPosition));
			WaitForElementVisible(gridPageHeaderElement(CellPosition));
			Click(gridPageHeaderElement(CellPosition));

			return this;
		}

		public CPMasterPayArrangementsPage ClickColumnHeader(string ColumnName)
		{
			ScrollToElement(gridPageHeaderElement(ColumnName));
			WaitForElementVisible(gridPageHeaderElement(ColumnName));
			Click(gridPageHeaderElement(ColumnName));

			return this;
		}

		public CPMasterPayArrangementsPage ValidateHeaderCellSortAscendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortasc");

			return this;
		}

		public CPMasterPayArrangementsPage ValidateHeaderCellSortDescendingOrder(int CellPosition)
		{
			WaitForElement(gridPageHeaderElementSortOrder(CellPosition));
			ScrollToElement(gridPageHeaderElementSortOrder(CellPosition));
			WaitForElementVisible(gridPageHeaderElementSortOrder(CellPosition));
			ValidateElementAttribute(gridPageHeaderElementSortOrder(CellPosition), "class", "sortdesc");

			return this;
		}

	}
}
