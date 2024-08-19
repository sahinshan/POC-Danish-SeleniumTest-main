using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class QueryResultsPage : CommonMethods
    {
        public QueryResultsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWQueryResultDialog = By.Id("iframe_CWQueryResultDialog");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, string CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        readonly By BackButton = By.XPath("//button[@title='Back']");
        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public QueryResultsPage WaitForQueryResultsPage(string PageTitle)
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWQueryResultDialog);
            SwitchToIframe(iframe_CWQueryResultDialog);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, PageTitle);

            return this;
        }

        public QueryResultsPage ValidateRecordPresent(string RecordId)
        {
            WaitForElement(recordRow(RecordId));

            return this;
        }

        public QueryResultsPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(recordRow(RecordId), 7);

            return this;
        }

        public QueryResultsPage ValidateRecordCellText(string RecordId, string CellPosition, string ExpectedText)
        {
            ValidateElementText(recordCell(RecordId, CellPosition), ExpectedText);

            return this;
        }




        public QueryResultsPage OpenRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public QueryResultsPage SelectRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public QueryResultsPage TapBackButton()
        {
            Click(BackButton);

            return this;
        }

        public QueryResultsPage TapNewRecordButton()
        {
            Click(NewRecordButton);

            return this;
        }

    }
}
