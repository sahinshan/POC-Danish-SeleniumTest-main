using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DocumentSectionQuestionsSubPage : CommonMethods
    {
        public DocumentSectionQuestionsSubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=documentsection&')]");
        readonly By CWRelatedRecordPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageTitle = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Document Section Questions']");

        


        #region Top Menu

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        #endregion


        By recordCheckBox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");

        By recordNameCell(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");




        public DocumentSectionQuestionsSubPage WaitForDocumentSectionQuestionsSubPageToLoad()
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


        
        public DocumentSectionQuestionsSubPage ClickRecordCheckbox(string recordID)
        {
            WaitForElement(recordCheckBox(recordID));

            Click(recordCheckBox(recordID));

            return this;
        }
        public DocumentSectionQuestionsSubPage ClickOnRecord(string recordID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(recordNameCell(recordID));

            Click(recordNameCell(recordID));

            return this;
        }
        public DocumentSectionQuestionsSubPage ClickOnNewRecordButton()
        {
            Click(newRecordButton);

            return this;
        }
        public DocumentSectionQuestionsSubPage ClickOnDeleteRecordButton()
        {
            Click(deleteButton);

            return this;
        }



        public DocumentSectionQuestionsSubPage ValidateRecordPresent(string recordID)
        {
            WaitForElement(recordCheckBox(recordID));

            return this;
        }




    }
}
