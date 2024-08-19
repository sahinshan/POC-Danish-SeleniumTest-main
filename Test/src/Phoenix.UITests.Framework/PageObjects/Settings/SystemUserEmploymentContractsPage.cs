using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class SystemUserEmploymentContractsPage : CommonMethods
    {
        public SystemUserEmploymentContractsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region IFrame
        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By CWNavItem_UserEmploymentContractsFrame = By.Id("CWUrlPanel_IFrame");

        #endregion IFrame

        #region Fields and labels

        readonly By pageheader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='System User Employment Contracts']");
        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");
        readonly By viewsPicklist = By.Id("CWViewSelector");
        //readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        //readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By searchButton = By.Id("CWSearchButton");
        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By addBookingTypes = By.Id("TI_AddBookingTypes");

        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        readonly By relatedRecords = By.XPath("//*[@id='SysView']/option[text()='Related Records']");

        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By selectAllRecord = By.Id("cwgridheaderselector");

        readonly By startDate_Header_Sort = By.XPath("//*[@id='CWGridHeaderRow']/th[6]/a/span[@class ='sortdesc']");
        
        readonly By firstName_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/a/span[text()='First Name']");
        readonly By firstName_Header_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[3]/a/span[@class='sortasc']");
        readonly By lastName_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/a/span[text()='Last Name']");
        readonly By lastName_Header_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/a/span[@class='sortasc']");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");

        readonly By staffContractPageheader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Staff Contracts']");
        By Column_HeaderCell(int CellPosition, string ColumnName) => By.XPath("//*[@id='CWGridHeaderRow']/th["+ CellPosition + "]//a/span[text()='"+ ColumnName + "']");

        By recordIdentifier(string recordID) => By.XPath("//tr[@id='" + recordID + "']/td[2]");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");        
        By recordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[' + RecordPosition + '][@id='" + RecordID + "']");

        By recordRowPosition(string recordID, int PositionInList) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + PositionInList + "][@id='" + recordID + "']");
        By tableHeaderCell(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
        By recordCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");
        By GridHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
        By tableHeaderTextPreferredName(int HeaderCellPosition, string ColumnHeader) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a/nobr[text()='" + ColumnHeader + "']");

        By gridPageHeaderElementSortDescendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");

        By SoryByElementTitle(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a[@title = 'Sort by " + FieldName + "']");

        #endregion Fields and labels

        public SystemUserEmploymentContractsPage WaitForSystemUserEmploymentContractsPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(CWNavItem_UserEmploymentContractsFrame);
            SwitchToIframe(CWNavItem_UserEmploymentContractsFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(pageheader);

            WaitForElement(addNewRecordButton);

            WaitForElement(viewsPicklist);

            //WaitForElement(quickSearchTextBox);
            //WaitForElement(quickSearchButton);
            //WaitForElement(refreshButton);

            return this;
        }

        public SystemUserEmploymentContractsPage WaitForSystemUserStaffContractsPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementVisible(staffContractPageheader);

            WaitForElementVisible(addNewRecordButton);
            WaitForElementVisible(exportToExcelButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(addBookingTypes);

            return this;
        }

        /// <summary>
        /// use this method to switch the focus to the Iframe that holds the Dynamic Dialog popup.
        /// In the case of System User Team Member sub page the Iframe path is CWContentIFrame -> iframe_CWDialog_
        /// </summary>
        /// <returns></returns>
        public SystemUserEmploymentContractsPage SwitchToDynamicsDialogLevelIframe()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            return this;
        }

        public SystemUserEmploymentContractsPage SelectRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordCheckBox(RecordID));
            ScrollToElement(recordCheckBox(RecordID));
            Click(recordCheckBox(RecordID));
            
            return this;
        }

        public SystemUserEmploymentContractsPage ClickAddNewButton()
        {
            this.WaitForElement(addNewRecordButton);
            this.Click(addNewRecordButton);

            return new SystemUserEmploymentContractsPage(driver, Wait, appURL);
        }

        public SystemUserEmploymentContractsPage ClickSelectAllCheckBox()
        {
            this.WaitForElement(selectAllRecord);
            this.Click(selectAllRecord);

            return new SystemUserEmploymentContractsPage(driver, Wait, appURL);
        }


        public SystemUserEmploymentContractsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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


        public SystemUserEmploymentContractsPage OpenRecord(string RecordID)
        {
            this.Click(recordRow(RecordID));

            return this;
        }

        public SystemUserEmploymentContractsPage OpenRecord(Guid RecordID)
        {
            return OpenRecord(RecordID.ToString());
        }

        public SystemUserEmploymentContractsPage ValidateRelatedRecords_Option(String ExpectedText)
        {
            ValidateElementText(relatedRecords, ExpectedText);


            return this;
        }



        public SystemUserEmploymentContractsPage ValidateRecordPosition(string RecordID, int ElementPosition)
        {
            ScrollToElement(recordRowPosition(RecordID, ElementPosition));
            WaitForElementVisible(recordRowPosition(RecordID, ElementPosition));

            return this;
        }




        public SystemUserEmploymentContractsPage ClickTableHeaderCell(int CellPosition)
        {
            Click(tableHeaderCell(CellPosition));

            return this;
        }

        public SystemUserEmploymentContractsPage ClickExportToExcel()
        {
            Click(exportToExcelButton);

            return this;
        }

        public SystemUserEmploymentContractsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);
            //ValidateElementByTitle(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsPage ValidateHeaderCellTextAndSortByIsVisible(int CellPosition, string ExpectedText,bool IsPresent = true)
        {
            ScrollToElement(GridHeaderElement(CellPosition));
            WaitForElementVisible(GridHeaderElement(CellPosition));
            ValidateElementText(GridHeaderElement(CellPosition), ExpectedText);

            ValidateSortByColumnIsPresent(CellPosition, ExpectedText, IsPresent);

            return this;
        }


        public SystemUserEmploymentContractsPage ValidatePreferredNameColumnHeader(int HeaderCellPosition, string ColumnHeader, string ExpectedText)
        {

            WaitForElement(tableHeaderTextPreferredName(HeaderCellPosition, ColumnHeader));
            ValidateElementText(tableHeaderTextPreferredName(HeaderCellPosition, ColumnHeader), ExpectedText);

            return this;
        }



        public SystemUserEmploymentContractsPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public SystemUserEmploymentContractsPage ValidatePageTitle(string ExpectedText)
        {
            WaitForElementToContainText(pageheader, ExpectedText);
            ValidateElementText(pageheader, ExpectedText);

            return this;
        }

        //public SystemUserEmploymentContractsPage InsertQuickSearchText(string TextToInsert)
        //{
        //    WaitForElement(quickSearchTextBox);
        //    SendKeys(quickSearchTextBox, TextToInsert);

        //    return this;
        //}

        //public SystemUserEmploymentContractsPage ClickQuickSearchButton()
        //{
        //    WaitForElement(quickSearchButton);
        //    Click(quickSearchButton);

        //    WaitForElementNotVisible("CWRefreshPanel", 7);

        //    return this;
        //}

        public SystemUserEmploymentContractsPage ClickSearchButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementToBeClickable(searchButton);
            Click(searchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SystemUserEmploymentContractsPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            return this;

        }
        public SystemUserEmploymentContractsPage ClickStartDateSort()
        {
         
            WaitForElementVisible(startDate_Header_Sort);
            this.Click(startDate_Header_Sort);

            return this;
        }

        public SystemUserEmploymentContractsPage ClickFirstNameSort()
        {
            this.Click(firstName_Header);
            WaitForElementVisible(firstName_Header_sort);
            this.Click(firstName_Header_sort);

            return this;
        }


        public SystemUserEmploymentContractsPage ClickLastNameSort()
        {
            this.Click(lastName_Header);
            WaitForElementVisible(lastName_Header_sort);
            this.Click(lastName_Header_sort);

            return this;
        }

        public SystemUserEmploymentContractsPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

        public SystemUserEmploymentContractsPage ValidateRecordVisibility(string RecordID, bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                WaitForElementVisible(recordCheckBox(RecordID));
                ScrollToElement(recordCheckBox(RecordID));
            }
            else
                WaitForElementNotVisible(recordCheckBox(RecordID), 3);

            Assert.AreEqual(ExpectedVisibility, GetElementVisibility(recordCheckBox(RecordID)));

            return this;
        }

        public SystemUserEmploymentContractsPage ValidateHeaderCellSortOrdedByDescending(int CellPosition, string headerName)
        {
            ScrollToElement(gridPageHeaderElementSortDescendingOrder(CellPosition, headerName));
            WaitForElementVisible(gridPageHeaderElementSortDescendingOrder(CellPosition, headerName));
            ValidateElementAttribute(gridPageHeaderElementSortDescendingOrder(CellPosition, headerName), "class", "sortdesc");

            return this;
        }

        public SystemUserEmploymentContractsPage SelectSystemView(string ExpectedTextToSelect)
        {
            WaitForElementToBeClickable(viewsPicklist);
            ScrollToElement(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, ExpectedTextToSelect);

            return this;
        }

        public SystemUserEmploymentContractsPage ValidateSelectedSystemView(string ExpectedText)
        {
            WaitForElementToBeClickable(viewsPicklist);
            ScrollToElement(viewsPicklist);
            ValidatePicklistSelectedText(viewsPicklist, ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsPage ValidateSortByColumnIsPresent(int CellPosition, string headerName, bool IsPersent)
        {
            if (IsPersent)
            {
                ScrollToElement(SoryByElementTitle(CellPosition, headerName));
                WaitForElementVisible(SoryByElementTitle(CellPosition, headerName));
            }
            else
                WaitForElementNotVisible(SoryByElementTitle(CellPosition, headerName), 3);

            return this;
        }

        public SystemUserEmploymentContractsPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            ScrollToElement(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }

        public SystemUserEmploymentContractsPage ClickAddBookingTypesButton()
        {
            WaitForElementToBeClickable(addBookingTypes);
            ScrollToElement(addBookingTypes);
            Click(addBookingTypes);

            return this;
        }

        public SystemUserEmploymentContractsPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignButton);
            ScrollToElement(assignButton);
            Click(assignButton);

            return this;
        }

        public SystemUserEmploymentContractsPage ValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            ScrollToElement(GridHeaderElement(CellPosition));
            WaitForElementVisible(GridHeaderElement(CellPosition));
            ValidateElementText(GridHeaderElement(CellPosition), ExpectedText);

            return this;
        }

        public SystemUserEmploymentContractsPage ClickColumnCellToSortRecord(int CellPosition, string ColumnName)
        {
            WaitForElementToBeClickable(Column_HeaderCell(CellPosition, ColumnName));
            ScrollToElement(Column_HeaderCell(CellPosition, ColumnName));
            Click(Column_HeaderCell(CellPosition, ColumnName));
            System.Threading.Thread.Sleep(1000);

            return this;
        }

    }

}