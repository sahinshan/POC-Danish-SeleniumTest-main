using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class BrokerageOfferCancellationReasonsPage : CommonMethods
    {
        public BrokerageOfferCancellationReasonsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By CWNavItem_BrokerageOfferCancellationReasons = By.Id("iframe_brokerageoffercancellationreason");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");

        readonly By back_Button = By.XPath("/html/body/form/div[3]/div/div/button[@onclick='CW.DataForm.Close(); return false;']");
        readonly By details_Button = By.XPath("/html/body/form/div[5]/nav/div/ul/li[3]/a");


        readonly By quickSearchTextBox = By.XPath("//*[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//*[@id='CWQuickSearchButton']");
        
        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By selectAll_Checkbox = By.Id("cwgridheaderselector");



        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRow(string RecordID, int RecordPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");





        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");



        public BrokerageOfferCancellationReasonsPage WaitForBrokerageOfferCancellationReasonsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(CWNavItem_BrokerageOfferCancellationReasons);
            SwitchToIframe(CWNavItem_BrokerageOfferCancellationReasons);

           

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Brokerage Offer Cancellation Reasons");

            WaitForElement(exportToExcelButton);
            WaitForElement(deleteRecordButton);

            return this;
        }


      

      


        public BrokerageOfferCancellationReasonsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }


        public BrokerageOfferCancellationReasonsPage ClickDetailsTab()
        {
            WaitForElementToBeClickable(details_Button);
            Click(details_Button);

            return this;

        }


        public BrokerageOfferCancellationReasonsPage OpenRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }
        public BrokerageOfferCancellationReasonsPage SelectRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }


        public BrokerageOfferCancellationReasonsPage OpenBrokerageOfferCancellationReasonsRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }




        public BrokerageOfferCancellationReasonsPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;

        }
        public BrokerageOfferCancellationReasonsPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }

        public BrokerageOfferCancellationReasonsPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }


        public BrokerageOfferCancellationReasonsPage ValidateNewRecordButtonVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(newRecordButton);
            }
            else
            {
                WaitForElementNotVisible(newRecordButton, 5);
            }
            return this;
        }

        public BrokerageOfferCancellationReasonsPage ValidateNoErrorMessageLabelVisibile(bool ExpectedText)
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

        public BrokerageOfferCancellationReasonsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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

        public BrokerageOfferCancellationReasonsPage ValidateRecordPositionInList(string RecordId, int RecordPosition)
        {
            WaitForElement(recordRow(RecordId, RecordPosition));

            return this;
        }

        public BrokerageOfferCancellationReasonsPage ValidateRecordVisible(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public BrokerageOfferCancellationReasonsPage ValidateRecordNotVisible(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 5);

            return this;
        }

        public BrokerageOfferCancellationReasonsPage InsertQuickSearchQuery(string TextToInsert)
        {
            SendKeys(quickSearchTextBox, TextToInsert);

            return this;
        }

        public BrokerageOfferCancellationReasonsPage ClickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;

        }

        public BrokerageOfferCancellationReasonsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public BrokerageOfferCancellationReasonsPage ClickBackButton()
        {

            WaitForElementVisible(back_Button);
            Click(back_Button);

            return this;
        }

        public BrokerageOfferCancellationReasonsPage TapSearchButton()
        {
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public BrokerageOfferCancellationReasonsPage SelectBrokerageOfferCancellationReasonsCheckBox()
        {
            WaitForElement(selectAll_Checkbox);
            Click(selectAll_Checkbox);

            return this;
        }



    }
}
