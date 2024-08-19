using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DocumentPrintTemplatesPage : CommonMethods
    {
        public DocumentPrintTemplatesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=document&')]");
        readonly By CWRelatedRecordPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageTitle = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Document Print Templates']");

        


        #region Top Menu

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        #endregion


        By recordCheckBox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");
        By recordNameCell(string recordID) => By.XPath("//*[@id='" + recordID + "_Primary']");
        By languageCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By createdByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");
        By createdOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[6]");




        public DocumentPrintTemplatesPage WaitForDocumentPrintTemplatesPageToLoad()
        {
            SwitchToDefaultFrame();
            
            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);
            
            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(CWRelatedRecordPanel_IFrame);
            SwitchToIframe(CWRelatedRecordPanel_IFrame);

            WaitForElement(pageTitle);
            
            WaitForElement(newRecordButton);
            WaitForElement(deleteButton);
            

            return this;

        }


        
        public DocumentPrintTemplatesPage ClickRecordCheckbox(string recordID)
        {
            WaitForElement(recordCheckBox(recordID));

            Click(recordCheckBox(recordID));

            return this;
        }
        public DocumentPrintTemplatesPage ClickOnRecord(string recordID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(recordNameCell(recordID));

            Click(recordNameCell(recordID));

            return this;
        }
        public DocumentPrintTemplatesPage ClickOnNewRecordButton()
        {
            Click(newRecordButton);

            return this;
        }
        public DocumentPrintTemplatesPage ClickOnDeleteRecordButton()
        {
            Click(deleteButton);

            return this;
        }


        public DocumentPrintTemplatesPage ValidateRecordPresent(string recordID)
        {
            WaitForElement(recordCheckBox(recordID));

            return this;
        }


        public DocumentPrintTemplatesPage ValidateRecordNameCellText(string recordID, string ExpectedName)
        {
            WaitForElementVisible(recordNameCell(recordID));
            ValidateElementText(recordNameCell(recordID), ExpectedName);

            return this;
        }
        public DocumentPrintTemplatesPage ValidateRecordLanguageCellText(string recordID, string ExpectedName)
        {
            ValidateElementText(languageCell(recordID), ExpectedName);

            return this;
        }
        public DocumentPrintTemplatesPage ValidateRecordCreatedByCellText(string recordID, string ExpectedName)
        {
            ValidateElementText(createdByCell(recordID), ExpectedName);

            return this;
        }
        public DocumentPrintTemplatesPage ValidateRecordCreatedOnCellText(string recordID, string ExpectedName)
        {
            ValidateElementText(createdOnCell(recordID), ExpectedName);

            return this;
        }



    }
}
