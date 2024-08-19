using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServiceProvisionCostPerWeekPage : CommonMethods
    {
        public ServiceProvisionCostPerWeekPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovision&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[contains(text(),'Service Provision Cost Per Week')]");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");

        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        By recordRowCell(int RowPosition, int Cellposition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RowPosition + "]/td[" + Cellposition + "]");

        public ServiceProvisionCostPerWeekPage WaitForServiceProvisionCostsPerWeekPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(ExportToExcelButton);
            WaitForElementVisible(AssignRecordButton);

            return this;
        }

        public ServiceProvisionCostPerWeekPage SearchRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(quickSearchTextBox);
            MoveToElementInPage(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);


            return this;
        }

        public ServiceProvisionCostPerWeekPage OpenRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            MoveToElementInPage(recordRow(RecordId));
            Click(recordRow(RecordId));
            return this;
        }

        public ServiceProvisionCostPerWeekPage OpenRecord(Guid RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId.ToString()));
            MoveToElementInPage(recordRow(RecordId.ToString()));
            Click(recordRow(RecordId.ToString()));
            return this;
        }

        public ServiceProvisionCostPerWeekPage SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            MoveToElementInPage(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public ServiceProvisionCostPerWeekPage SelectRecord(Guid RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId.ToString()));
            MoveToElementInPage(recordRowCheckBox(RecordId.ToString()));
            Click(recordRowCheckBox(RecordId.ToString()));

            return this;
        }

        public ServiceProvisionCostPerWeekPage ValidateRecordCellContent(int RowPosition, int Cellposition, string ExpectedText)
        {
            ScrollToElement(recordRowCell(RowPosition, Cellposition));
            WaitForElementToBeClickable(recordRowCell(RowPosition, Cellposition));
            MoveToElementInPage(recordRowCell(RowPosition, Cellposition));
            ValidateElementText(recordRowCell(RowPosition, Cellposition), ExpectedText);

            return this;
        }

        public ServiceProvisionCostPerWeekPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            MoveToElementInPage(BackButton);
            Click(BackButton);

            return this;
        }

    }
}
