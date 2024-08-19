using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DocumentSectionQuestionApplicationAccessPage : CommonMethods
    {
        public DocumentSectionQuestionApplicationAccessPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");

        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=documentsectionquestion&')]");

        readonly By CWNavItem_DocumentSectionQuestionApplicationAccessFrame = By.Id("CWUrlPanel_IFrame");
        


        #region Top Menu

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        #endregion


        By recordCheckBox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");

        By recordApplicationCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");
        By recordCanEditCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[3]");
        By recordCreatedByCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[4]");
        By recordCreatedOnCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[5]");




        public DocumentSectionQuestionApplicationAccessPage WaitForDocumentSectionQuestionApplicationAccessPageToLoad()
        {
            SwitchToDefaultFrame();
            
            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);
            
            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(CWNavItem_DocumentSectionQuestionApplicationAccessFrame);
            SwitchToIframe(CWNavItem_DocumentSectionQuestionApplicationAccessFrame);

            WaitForElement(newRecordButton);
            WaitForElement(deleteButton);
            

            return this;

        }


        
        public DocumentSectionQuestionApplicationAccessPage ClickRecordCheckbox(string recordID)
        {
            WaitForElement(recordCheckBox(recordID));

            Click(recordCheckBox(recordID));

            return this;
        }
        public DocumentSectionQuestionApplicationAccessPage ClickOnRecord(string recordID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(recordApplicationCell(recordID));

            Click(recordApplicationCell(recordID));

            return this;
        }
        public DocumentSectionQuestionApplicationAccessPage ClickOnNewRecordButton()
        {
            Click(newRecordButton);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessPage ClickOnDeleteRecordButton()
        {
            Click(deleteButton);

            return this;
        }



        public DocumentSectionQuestionApplicationAccessPage ValidateRecordPresent(string recordID)
        {
            WaitForElement(recordCheckBox(recordID));

            return this;
        }

        public DocumentSectionQuestionApplicationAccessPage ValidateRecordApplicationCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(recordApplicationCell(recordID), ExpectedText);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessPage ValidateRecordCanEditCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(recordCanEditCell(recordID), ExpectedText);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessPage ValidateRecordCreatedByCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(recordCreatedByCell(recordID), ExpectedText);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessPage ValidateRecordCreatedOnCellText(string recordID, string ExpectedText)
        {
            ValidateElementText(recordCreatedOnCell(recordID), ExpectedText);

            return this;
        }



    }
}
