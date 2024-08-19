
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
    public class SystemUserAddressPage : CommonMethods
    {
        public SystemUserAddressPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By CWNavItem_AddressesFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='System User Addresses']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        readonly By startDate_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[2]//a/span[2]");
        readonly By propertyNo_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[6]//a/span[text()='Property No']");
        readonly By propertyNo_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[6]//a/span[@class = 'sortasc']");
        readonly By propertyName_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[5]//a/span[text()='Property Name']");
        readonly By propertyName_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[5]//a/span[text()='Property Name']/following-sibling::span[@class='sortasc']");
        readonly By street_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[7]//a/span[contains(text(),'Street')]");
        readonly By street_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[7]//a/span[contains(text(),'Street')]/following-sibling::span[@class='sortasc']");
        readonly By villageDistrict_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[8]//a/span[text()='Village / District']");
        readonly By villageDistrict_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[8]//a/span[@class = 'sortasc']");
        readonly By townCity_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[9]//a/span[text()='Town / City']");
        readonly By townCity_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[9]//a/span[@class = 'sortasc']");
        readonly By county_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[10]//a/span[text()='County']");
        readonly By county_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[10]//a/span[@class = 'sortasc']");
   
        readonly By postCode_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[11]//a/span[text()='Postcode']");
        readonly By postCode_sort = By.XPath("//*[@id='CWGridHeaderRow']/th[11]//a/span[text()='Postcode']/following-sibling::span[@class = 'sortasc']");

        readonly By relatedRecords = By.XPath("//*[@id='SysView']/option[text()='Related Records']");
        readonly By startDate_ColumnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]//a/span[text()='Start Date']");
        readonly By endDate_ColumnHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]//a/span[text()='End Date']");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By selectAllRecord = By.Id("cwgridheaderselector");




        By recordIdentifier(string recordID) => By.XPath("//tr[@id='" + recordID + "']/td[2]");

        By recordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By recordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[' + RecordPosition + '][@id='" + RecordID + "']");
        By tableHeaderCell(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");
        By recordCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");
        By tableHeaderText(int HeaderCellPosition, string ColumnHeader) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]//a/span[text()='" + ColumnHeader + "']");

      
        public SystemUserAddressPage WaitForSystemUserAddressPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWNavItem_AddressesFrame);
            SwitchToIframe(CWNavItem_AddressesFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(pagehehader);

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
        public SystemUserAddressPage SwitchToDynamicsDialogLevelIframe()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            return this;
        }

        public SystemUserAddressPage OpenContributionRecord(string RecordID)
        {
            Click(recordIdentifier(RecordID));

            return new SystemUserAddressPage(driver, Wait, appURL);
        }

        public SystemUserAddressPage SelectRecord(string RecordID)
        {
            this.Click(recordCheckBox(RecordID));

            return this;
        }

        public SystemUserAddressPage ClickAddNewButton()
        {
            this.WaitForElement(addNewRecordButton);
            this.Click(addNewRecordButton);

            return new SystemUserAddressPage(driver, Wait, appURL);
        }

        public SystemUserAddressPage ClickSelectAllCheckBox()
        {
            this.WaitForElement(selectAllRecord);
            this.Click(selectAllRecord);

            return new SystemUserAddressPage(driver, Wait, appURL);
        }


        public SystemUserAddressPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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


        public SystemUserAddressPage OpenRecord(string RecordID)
        {
            this.Click(recordRow(RecordID));

            return this;
        }

        public SystemUserAddressPage ValidateRelatedRecords_Option(String ExpectedText)
        {
            WaitForElement(relatedRecords);
            MoveToElementInPage(relatedRecords);
            ValidateElementText(relatedRecords, ExpectedText);


            return this;
        }


        public SystemUserAddressPage ClickStartDateSort()
        {
            this.Click(startDate_sort);

            return this;
        }

        public SystemUserAddressPage ClickPropertyNoSort()
        {
            this.Click(propertyNo_Header);
            WaitForElementVisible(propertyNo_sort);
            this.Click(propertyNo_sort);

            return this;
        }

        public SystemUserAddressPage ClickPropertyNameSort()
        {
            this.Click(propertyName_Header);
            WaitForElementVisible(propertyName_sort);
            this.Click(propertyName_sort);

            return this;
        }


        public SystemUserAddressPage ClickStreetSort()
        {
            System.Threading.Thread.Sleep(3000);
            WaitForElementVisible(street_Header);
            MoveToElementInPage(street_Header);
            Click(street_Header);
            System.Threading.Thread.Sleep(3000);
            WaitForElementVisible(street_sort);
            MoveToElementInPage(street_sort);
            Click(street_sort);

            return this;
        }

        public SystemUserAddressPage ClickVillageDistrictSort()
        {
            this.Click(villageDistrict_Header);
            WaitForElementVisible(villageDistrict_sort);
            this.Click(villageDistrict_sort);

            return this;
        }

        public SystemUserAddressPage ClickTownCitySort()
        {
            WaitForElement(townCity_Header);
            MoveToElementInPage(townCity_Header);
            Click(townCity_Header);
            WaitForElement(townCity_sort);
            MoveToElementInPage(townCity_sort);
            Click(townCity_sort);

            return this;
        }


        public SystemUserAddressPage ClickCountySort()
        {
            WaitForElement(county_Header);
            MoveToElementInPage(county_Header);
            Click(county_Header);
            WaitForElement(county_sort);
            MoveToElementInPage(county_sort);
            Click(county_sort);

            return this;
        }

        public SystemUserAddressPage ClickPostCodeSort()
        {
            System.Threading.Thread.Sleep(3000);
            WaitForElement(postCode_Header);
            MoveToElementInPage(postCode_Header);
            Click(postCode_Header);
            System.Threading.Thread.Sleep(3000);
            WaitForElement(postCode_sort);
            MoveToElementInPage(postCode_sort);
            Click(postCode_sort);



            return this;
        }


        public SystemUserAddressPage ValidateRecordPosition(int ElementPosition, string RecordID)
        {
            WaitForElement(recordPosition(ElementPosition, RecordID), 5);
            MoveToElementInPage(recordPosition(ElementPosition, RecordID));
            WaitForElementVisible(recordPosition(ElementPosition, RecordID));

            return this;
        }


        public SystemUserAddressPage ValidateStartDateColumnHeader(string ExpectedText)
        {


            WaitForElementVisible(startDate_ColumnHeader);
            ValidateElementText(startDate_ColumnHeader, ExpectedText);

            return this;
        }
        public SystemUserAddressPage ValidateEndDateColumnHeader(string ExpectedText)
        {


            WaitForElementVisible(endDate_ColumnHeader);
            ValidateElementText(endDate_ColumnHeader, ExpectedText);

            return this;
        }


        
        public SystemUserAddressPage ClickTableHeaderCell(int CellPosition)
        {
            Click(tableHeaderCell(CellPosition));

            return this;
        }


        public SystemUserAddressPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public SystemUserAddressPage ValidateColumnHeader(int HeaderCellPosition,string ColumnHeader,string ExpectedText)
        {

            WaitForElement(tableHeaderText(HeaderCellPosition, ColumnHeader));
            ScrollToElement(tableHeaderText(HeaderCellPosition, ColumnHeader));
            ValidateElementText(tableHeaderText(HeaderCellPosition, ColumnHeader), ExpectedText);

            return this;
        }

        public SystemUserAddressPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public SystemUserAddressPage InsertQuickSearchText(string Text)
        {
            this.SendKeys(quickSearchTextBox, Text);

            return this;
        }

        public SystemUserAddressPage ClickQuickSearchButton()
        {
            this.Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SystemUserAddressPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);
            return this;
        }

    }
}
