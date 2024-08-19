
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
    public class SystemUserTrainingPage : CommonMethods
    {
        public SystemUserTrainingPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By CWNavItem_AddressesFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='System User Training']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        //readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        //readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By searchButton = By.Id("CWSearchButton");

        
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        
        readonly By selectAllRecord = By.Id("cwgridheaderselector");




        By recordIdentifier(string recordID) => By.XPath("//tr[@id='" + recordID + "']/td[2]");

        By recordCell(string recordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[" + CellPosition + "]");
        
        By recordCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");



        By tableHeaderCell(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By expandCollapseFilterButton = By.XPath("//a[@id = 'CWSplitter_Link']");               

      
        public SystemUserTrainingPage WaitForSystemUserTrainingPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWNavItem_AddressesFrame);
            SwitchToIframe(CWNavItem_AddressesFrame);

            WaitForElement(pagehehader);

            WaitForElement(addNewRecordButton);

            WaitForElement(viewsPicklist);
            //WaitForElement(quickSearchTextBox);
            //WaitForElementToBeClickable(quickSearchButton);
            //WaitForElementToBeClickable(refreshButton);

            return this;
        }

        public SystemUserTrainingPage CollapseFilterPanel()
        {
            string classAttrubuteValue = GetElementByAttributeValue(expandCollapseFilterButton, "class");
            if(classAttrubuteValue.Equals("splitter-link open"))
            {
                WaitForElementToBeClickable(expandCollapseFilterButton);
                Click(expandCollapseFilterButton);
            }

            return this;
        }

        public SystemUserTrainingPage ClickAddNewButton()
        {
            this.WaitForElementToBeClickable(addNewRecordButton);
            this.Click(addNewRecordButton);

            return this;
        }


        public SystemUserTrainingPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public SystemUserTrainingPage ClickSelectAllCheckBox()
        {
            this.WaitForElementToBeClickable(selectAllRecord);
            this.Click(selectAllRecord);

            return new SystemUserTrainingPage(driver, Wait, appURL);
        }

        public SystemUserTrainingPage SelectRecord(string RecordID)
        {
            this.WaitForElementToBeClickable(recordCheckBox(RecordID));
            this.Click(recordCheckBox(RecordID));

            return this;
        }

        public SystemUserTrainingPage ValidateRecordVisible(string RecordID)
        {
            WaitForElementVisible(recordCheckBox(RecordID));

            return this;
        }

        public SystemUserTrainingPage ValidateRecordNotVisible(string RecordID)
        {
            WaitForElementNotVisible(recordCheckBox(RecordID), 7);

            return this;
        }

        public SystemUserTrainingPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(recordIdentifier(RecordID));
            Click(recordIdentifier(RecordID));

            return this;
        }

        public SystemUserTrainingPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(RecordID, CellPosition));
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }


        public SystemUserTrainingPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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


        //public SystemUserTrainingPage InsertQuickSearchText(string Text)
        //{
        //    this.SendKeys(quickSearchTextBox, Text);

        //    return this;
        //}

        //public SystemUserTrainingPage ClickQuickSearchButton()
        //{
        //    this.Click(quickSearchButton);

        //    WaitForElementNotVisible("CWRefreshPanel", 14);

        //    return this;
        //}

        public SystemUserTrainingPage ClickSearchButton()
        {
            WaitForElementToBeClickable(searchButton);
            Click(searchButton);
            return this;
        }

        public SystemUserTrainingPage ValidateSelectedView(string ExpectedText)
        {
            ValidatePicklistSelectedText(viewsPicklist, ExpectedText);
            return this;
        }

        public SystemUserTrainingPage SelectView(string TextToSelect)
        {
            WaitForElementToBeClickable(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }

        public SystemUserTrainingPage ValidateTableHeaderCellText(int HeaderCellPosition, string ExpectedText)
        {
            WaitForElement(tableHeaderCell(HeaderCellPosition));
            ScrollToElement(tableHeaderCell(HeaderCellPosition));
            ValidateElementText(tableHeaderCell(HeaderCellPosition), ExpectedText);

            return this;
        }


    }
}
