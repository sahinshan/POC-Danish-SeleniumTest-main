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
    public class SystemUserEmergencyContactsPage : CommonMethods
    {
        public SystemUserEmergencyContactsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region IFrame
        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By CWNavItem_EmergencyContactsFrame = By.Id("CWUrlPanel_IFrame");

        #endregion IFrame

        #region Fields and labels

        readonly By pageheader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='System User Emergency Contacts']");
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

        readonly By startDate_Header_Sort = By.XPath("//*[@id='CWGridHeaderRow']/th[6]//a/span[@class ='sortdesc']");
        
        readonly By firstName_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[3]//a/span[text()='First Name']");
        readonly By firstName_Header_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[3]//a/span[@class='sortasc']");
        readonly By lastName_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[4]//a/span[text()='Last Name']");
        readonly By lastName_Header_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[4]//a/span[@class='sortasc']");


        By recordIdentifier(string recordID) => By.XPath("//tr[@id='" + recordID + "']/td[2]");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By recordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[' + RecordPosition + '][@id='" + RecordID + "']");

        By recordRowPosition(string recordID, int PositionInList) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + PositionInList + "][@id='" + recordID + "']/td[2]");
        By tableHeaderCell(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
        By recordCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");
        By tableHeaderText(int HeaderCellPosition, string ColumnHeader) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[text()='" + ColumnHeader + "']");
        By tableHeaderTextPreferredName(int HeaderCellPosition, string ColumnHeader) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/nobr[text()='" + ColumnHeader + "']");

        #endregion Fields and labels

        public SystemUserEmergencyContactsPage WaitForSystemUserEmergencyContactsPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWNavItem_EmergencyContactsFrame);
            SwitchToIframe(CWNavItem_EmergencyContactsFrame);

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
        public SystemUserEmergencyContactsPage SwitchToDynamicsDialogLevelIframe()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            return this;
        }

        public SystemUserEmergencyContactsPage OpenContributionRecord(string RecordID)
        {
            Click(recordIdentifier(RecordID));

            return new SystemUserEmergencyContactsPage(driver, Wait, appURL);
        }

        public SystemUserEmergencyContactsPage SelectRecord(string RecordID)
        {
            this.Click(recordCheckBox(RecordID));

            return this;
        }


        public SystemUserEmergencyContactsPage ClickExportToExcelButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }
        public SystemUserEmergencyContactsPage ClickAddNewButton()
        {
            this.WaitForElement(addNewRecordButton);
            this.Click(addNewRecordButton);

            return new SystemUserEmergencyContactsPage(driver, Wait, appURL);
        }

        public SystemUserEmergencyContactsPage ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            return this;
        }

        public SystemUserEmergencyContactsPage ClickSelectAllCheckBox()
        {
            this.WaitForElement(selectAllRecord);
            this.Click(selectAllRecord);

            return new SystemUserEmergencyContactsPage(driver, Wait, appURL);
        }


        public SystemUserEmergencyContactsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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


        public SystemUserEmergencyContactsPage OpenRecord(string RecordID)
        {
            this.Click(recordRow(RecordID));

            return this;
        }

        public SystemUserEmergencyContactsPage ValidateRelatedRecords_Option(String ExpectedText)
        {
            ValidateElementText(relatedRecords, ExpectedText);


            return this;
        }



        public SystemUserEmergencyContactsPage ValidateRecordPosition(string RecordID, int ElementPosition)
        {
            ScrollToElement(recordRowPosition(RecordID, ElementPosition));
            WaitForElementVisible(recordRowPosition(RecordID, ElementPosition));

            return this;
        }




        public SystemUserEmergencyContactsPage ClickTableHeaderCell(int CellPosition)
        {
            Click(tableHeaderCell(CellPosition));

            return this;
        }

        public SystemUserEmergencyContactsPage ClickExportToExcel()
        {
            Click(exportToExcelButton);

            return this;
        }

        public SystemUserEmergencyContactsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsPage ValidateColumnHeader(int HeaderCellPosition, string ColumnHeader, string ExpectedText)
        {

            WaitForElement(tableHeaderText(HeaderCellPosition, ColumnHeader));
            ValidateElementText(tableHeaderText(HeaderCellPosition, ColumnHeader), ExpectedText);

            return this;
        }


        public SystemUserEmergencyContactsPage ValidatePreferredNameColumnHeader(int HeaderCellPosition, string ColumnHeader, string ExpectedText)
        {

            WaitForElement(tableHeaderTextPreferredName(HeaderCellPosition, ColumnHeader));
            ValidateElementText(tableHeaderTextPreferredName(HeaderCellPosition, ColumnHeader), ExpectedText);

            return this;
        }



        public SystemUserEmergencyContactsPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public SystemUserEmergencyContactsPage ValidatePageTitle(string ExpectedText)
        {
            WaitForElementToContainText(pageheader, ExpectedText);
            ValidateElementText(pageheader, ExpectedText);

            return this;
        }

        public SystemUserEmergencyContactsPage InsertQuickSearchText(string TextToInsert)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, TextToInsert);

            return this;
        }

        public SystemUserEmergencyContactsPage ClickQuickSearchButton()
        {
            WaitForElement(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SystemUserEmergencyContactsPage ClickStartDateSort()
        {
         
            WaitForElementVisible(startDate_Header_Sort);
            this.Click(startDate_Header_Sort);

            return this;
        }

        public SystemUserEmergencyContactsPage ClickFirstNameSort()
        {
            this.Click(firstName_Header);
            WaitForElementVisible(firstName_Header_sort);
            this.Click(firstName_Header_sort);

            return this;
        }


        public SystemUserEmergencyContactsPage ClickLastNameSort()
        {
            this.Click(lastName_Header);
            WaitForElementVisible(lastName_Header_sort);
            this.Click(lastName_Header_sort);

            return this;
        }
    }

}