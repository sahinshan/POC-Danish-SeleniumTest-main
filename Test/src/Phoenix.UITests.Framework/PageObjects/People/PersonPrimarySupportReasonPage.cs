using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonPrimarySupportReasonPage : CommonMethods
    {
        public PersonPrimarySupportReasonPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWNavItem_PersonPrimarySupportReasonFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Primary Support Reasons']");
        
        

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By ExportOption = By.Id("CWSaveButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By ViewSelectorButton = By.Id("CWViewSelector");
        readonly By AllPrimarySupporReasonsRelatedView = By.XPath("//*[@id='SysView']/option[1]");
        readonly By InactivePrimarySupportReasonsRelatedView = By.XPath("//*[@id='SysView']/option[2]");
        


        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        readonly By TableGrid = By.Id("CWGrid");
        


        By recordsAreaHeaderCell(int CellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + CellPosition + "]/a");

        By recordFullRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By recordRowCheckbox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

       

        public PersonPrimarySupportReasonPage WaitForPersonPrimarySupportReasonPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonPrimarySupportReasonFrame);
            SwitchToIframe(CWNavItem_PersonPrimarySupportReasonFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Person Primary Support Reasons");

            return this;
        }

        public PersonPrimarySupportReasonPage SearchPersonPrimarySupportReasonRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public PersonPrimarySupportReasonPage SearchPersonPrimarySupportReasonRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonPrimarySupportReasonPage OpenPersonPrimarySupportReasonRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public PersonPrimarySupportReasonPage SelectPersonPrimarySupportReasonRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public PersonPrimarySupportReasonPage ClickExportToExcel()
        {
            Click(ExportToExcelButton);         
           
            return this;

        }
        public PersonPrimarySupportReasonPage ClickRecord()
        {
            Click(TableGrid);

            return this;

        }
        public PersonPrimarySupportReasonPage ClickAssignButton()
        {
            Click(AssignRecordButton);

            return this;

        }

        public PersonPrimarySupportReasonPage ClickViewSelectorButton()
        {
            Click(ViewSelectorButton);

            return this;

        }

        public PersonPrimarySupportReasonPage ClickActivePrimarySupportReasonRelatedViewOption()
        {
            WaitForElementToBeClickable(AllPrimarySupporReasonsRelatedView);
            Click(AllPrimarySupporReasonsRelatedView);

            return this;

        }

        public PersonPrimarySupportReasonPage ClickInactivePrimarySupportReasonsRelatedViewOtion()
        {
            WaitForElementToBeClickable(InactivePrimarySupportReasonsRelatedView);
            Click(InactivePrimarySupportReasonsRelatedView);

            return this;

        }
        public PersonPrimarySupportReasonPage ClickNewRecordButton()
        {
            WaitForElementVisible(NewRecordButton);
            Click(NewRecordButton);

            return this;

        }
        

        public PersonPrimarySupportReasonPage ToggleButton()
        {
            Click(NewRecordButton);

            return this;

        }

        public PersonPrimarySupportReasonPage ValidateNoRecordLabelVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordLabel);
            }
            else
            {
                WaitForElementNotVisible(NoRecordLabel, 5);
            }
            return this;
        }

        public PersonPrimarySupportReasonPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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

        public PersonPrimarySupportReasonPage ValidateRecordVisible(string RecordID)
        {
            WaitForElementVisible(recordRow(RecordID));

            return this;
        }
        public PersonPrimarySupportReasonPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }
        public PersonPrimarySupportReasonPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public PersonPrimarySupportReasonPage ValidateRecordNotVisible(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 3);

            return this;
        }

       
        






    }
}
