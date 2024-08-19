using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonEmailAttachmentsPage : CommonMethods
    {
        public PersonEmailAttachmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=email&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWRelatedRecordPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Email Attachments']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");

        By recordRowCheckBoxCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordRowFileCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCreatedByCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[3]");
        By recordRowCreatedOnCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[4]");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");






        public PersonEmailAttachmentsPage WaitForPersonEmailAttachmentsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWRelatedRecordPanel_IFrame);
            SwitchToIframe(CWRelatedRecordPanel_IFrame);

            WaitForElementVisible(pageHeader);

            ValidateElementText(pageHeader, "Email Attachments");

            return this;
        }

        public PersonEmailAttachmentsPage TapOnAddButton()
        {            
            Click(NewRecordButton);

            return this;
        }

        public PersonEmailAttachmentsPage SearchEmailAttachmentRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(PersonID));

            return this;
        }

        public PersonEmailAttachmentsPage ClickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            Click(quickSearchButton);

            return this;
        }

        public PersonEmailAttachmentsPage SearchEmailAttachmentRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonEmailAttachmentsPage OpenEmailAttachmentRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public PersonEmailAttachmentsPage SelectEmailAttachmentRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBoxCell(RecordId));
            Click(recordRowCheckBoxCell(RecordId));

            return this;
        }

        public PersonEmailAttachmentsPage ValidateFileCellText(string RecordId, string ExpectedText)
        {
            WaitForElement(recordRowFileCell(RecordId));
            ValidateElementText(recordRowFileCell(RecordId), ExpectedText);

            return this;
        }

        public PersonEmailAttachmentsPage ValidateCreatedByCellText(string RecordId, string ExpectedText)
        {
            WaitForElement(recordRowCreatedByCell(RecordId));
            ValidateElementText(recordRowCreatedByCell(RecordId), ExpectedText);

            return this;
        }

        public PersonEmailAttachmentsPage ValidateCreatedOnCellText(string RecordId, string ExpectedText)
        {
            WaitForElement(recordRowCreatedOnCell(RecordId));
            ValidateElementText(recordRowCreatedOnCell(RecordId), ExpectedText);

            return this;
        }

    }
}
