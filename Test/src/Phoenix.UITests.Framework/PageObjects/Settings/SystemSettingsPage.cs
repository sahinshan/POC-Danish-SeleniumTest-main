using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the Team Members sub page when accessed via a system user record
    /// Settings - Security - System Users - System User record - Team Member tabs
    /// </summary>
    public class SystemSettingsPage : CommonMethods
    {
        public SystemSettingsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region IFrame
        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        

        #endregion IFrame

        #region Fields and labels

        readonly By pageheader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='System Settings']");
        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");
        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By refreshButton = By.Id("CWRefreshButton");


        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        readonly By relatedRecords = By.XPath("//*[@id='SysView']/option[text()='Related Records']");

        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By selectAllRecord = By.Id("cwgridheaderselector");

        
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");


        By recordIdentifier(string recordID) => By.XPath("//tr[@id='" + recordID + "']/td[2]");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By recordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[' + RecordPosition + '][@id='" + RecordID + "']");

        By recordRowPosition(string recordID, int PositionInList) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + PositionInList + "][@id='" + recordID + "']/td[2]");
        By tableHeaderCell(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
        By recordCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");
        By tableHeaderText(int HeaderCellPosition, string ColumnHeader) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/span[text()='" + ColumnHeader + "']");
        By tableHeaderTextPreferredName(int HeaderCellPosition, string ColumnHeader) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/nobr[text()='" + ColumnHeader + "']");

        #endregion Fields and labels

        public SystemSettingsPage WaitForSystemSettingsPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(pageheader);

            WaitForElement(addNewRecordButton);

            WaitForElement(viewsPicklist);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        /// <summary>
        /// use this method to switch the focus to the Iframe that holds the Dynamic Dialog popup.
        /// In the case of System User Team Member sub page the Iframe path is CWContentIFrame -> iframe_CWDialog_
        /// </summary>
        /// <returns></returns>
        public SystemSettingsPage SwitchToDynamicsDialogLevelIframe()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);


            return this;
        }

        public SystemSettingsPage OpenContributionRecord(string RecordID)
        {
            Click(recordIdentifier(RecordID));

            return new SystemSettingsPage(driver, Wait, appURL);
        }

        public SystemSettingsPage SelectRecord(string RecordID)
        {
            this.Click(recordCheckBox(RecordID));

            return this;
        }

        public SystemSettingsPage ClickAddNewButton()
        {
            this.WaitForElement(addNewRecordButton);
            this.Click(addNewRecordButton);

            return new SystemSettingsPage(driver, Wait, appURL);
        }

        public SystemSettingsPage ClickSelectAllCheckBox()
        {
            this.WaitForElement(selectAllRecord);
            this.Click(selectAllRecord);

            return new SystemSettingsPage(driver, Wait, appURL);
        }


        public SystemSettingsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(NoRecordMessage, 5);
            }
            return this;
        }


        public SystemSettingsPage OpenRecord(string RecordID)
        {
            WaitForElement(recordRow(RecordID));
            this.Click(recordRow(RecordID));

            return this;
        }

        public SystemSettingsPage ValidateRelatedRecords_Option(String ExpectedText)
        {
            ValidateElementText(relatedRecords, ExpectedText);


            return this;
        }



        public SystemSettingsPage ValidateRecordPosition(string RecordID, int ElementPosition)
        {
            ScrollToElement(recordRowPosition(RecordID, ElementPosition));
            WaitForElementVisible(recordRowPosition(RecordID, ElementPosition));

            return this;
        }




        public SystemSettingsPage ClickTableHeaderCell(int CellPosition)
        {
            Click(tableHeaderCell(CellPosition));

            return this;
        }

        public SystemSettingsPage ClickExportToExcel()
        {
            Click(exportToExcelButton);

            return this;
        }

        public SystemSettingsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public SystemSettingsPage ValidateColumnHeader(int HeaderCellPosition, string ColumnHeader, string ExpectedText)
        {

            WaitForElement(tableHeaderText(HeaderCellPosition, ColumnHeader));
            ValidateElementText(tableHeaderText(HeaderCellPosition, ColumnHeader), ExpectedText);

            return this;
        }


        public SystemSettingsPage ValidatePreferredNameColumnHeader(int HeaderCellPosition, string ColumnHeader, string ExpectedText)
        {

            WaitForElement(tableHeaderTextPreferredName(HeaderCellPosition, ColumnHeader));
            ValidateElementText(tableHeaderTextPreferredName(HeaderCellPosition, ColumnHeader), ExpectedText);

            return this;
        }



        public SystemSettingsPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public SystemSettingsPage ValidatePageTitle(string ExpectedText)
        {
            WaitForElementToContainText(pageheader, ExpectedText);
            ValidateElementText(pageheader, ExpectedText);

            return this;
        }

        public SystemSettingsPage InsertQuickSearchText(string TextToInsert)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, TextToInsert);

            return this;
        }

        public SystemSettingsPage ClickQuickSearchButton()
        {
            WaitForElement(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

      
        public SystemSettingsPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }
    }

}