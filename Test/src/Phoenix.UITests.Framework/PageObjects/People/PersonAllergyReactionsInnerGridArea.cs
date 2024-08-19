using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonAllergyReactionsInnerGridArea : CommonMethods
    {
        public PersonAllergyReactionsInnerGridArea(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personallergy&')]");
        readonly By CWIFrame_PersonAllergicReactions = By.Id("CWIFrame_PersonAllergicReactions");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");

        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRow(string RecordID, int RecordPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        public PersonAllergyReactionsInnerGridArea WaitForPersonAllergyReactionsInnerGridAreaToLoad()
        {
            SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(CWIFrame_PersonAllergicReactions);
            this.SwitchToIframe(CWIFrame_PersonAllergicReactions);

            this.WaitForElement(pageHeader);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

      

        public PersonAllergyReactionsInnerGridArea ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }
        public PersonAllergyReactionsInnerGridArea OpenRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }
        public PersonAllergyReactionsInnerGridArea SelectRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }
        public PersonAllergyReactionsInnerGridArea ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;

        }
        public PersonAllergyReactionsInnerGridArea ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }
        public PersonAllergyReactionsInnerGridArea ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }

       
        public PersonAllergyReactionsInnerGridArea ValidateNoRecordsMessageLabelVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(NoRecordLabel);
            }
            else
            {
                WaitForElementNotVisible(NoRecordLabel, 5);
            }
            return this;
        }

        public PersonAllergyReactionsInnerGridArea ValidateNoRecordMessageVisibile(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(NoRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(NoRecordMessage, 5);
            }
            return this;
        }

        public PersonAllergyReactionsInnerGridArea ValidateRecordPositionInList(string RecordId, int RecordPosition)
        {
            WaitForElement(recordRow(RecordId, RecordPosition));

            return this;
        }

        public PersonAllergyReactionsInnerGridArea ValidateRecordVisible(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }

        public PersonAllergyReactionsInnerGridArea ValidateRecordNotVisible(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 5);

            return this;
        }

        public PersonAllergyReactionsInnerGridArea ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElement(recordCell(RecordID, CellPosition));
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

    }
}
