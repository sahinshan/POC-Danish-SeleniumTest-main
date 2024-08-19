using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DocumentSectionRecordPage : CommonMethods
    {
        public DocumentSectionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By documentIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=documentsection&')]"); 


        #region Top Menu

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By previewButton = By.Id("TI_PreviewDocument");

        #endregion

        #region Navigation Area

        readonly By menuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");
        readonly By detailsTab = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");
        readonly By questionsTab = By.XPath("//li[@id='CWNavGroup_SectionQuestions']/a[@title='Questions']");
        

        #region Left Sub Menu

        readonly By DocumentSectionApplicationAccessLeftSubMenu = By.Id("CWNavItem_DocumentSectionApplicationAccess");
        readonly By auditLeftSubMenu = By.Id("CWNavItem_AuditHistory");

        #endregion

        #endregion

        #region Field Labels

        readonly By nameFieldLabel = By.XPath("//*[@id='CWLabelHolder_sectionname']/label");
        readonly By availabilityFieldLabel = By.XPath("//*[@id='CWLabelHolder_availabilityid']/label");

        #endregion

        #region Fields

        readonly By nameField = By.Id("CWField_name");
        readonly By availabilityField = By.Id("CWField_availabilityid");

        #endregion


        public DocumentSectionRecordPage WaitForDocumentSectionRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(documentIFrame);
            SwitchToIframe(documentIFrame);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(previewButton);
            WaitForElement(detailsTab);
            WaitForElement(menuButton);
            
            WaitForElement(nameFieldLabel);
            WaitForElement(availabilityFieldLabel);

            return this;

        }



        public DocumentSectionRecordPage TapPreviewButton()
        {
            driver.FindElement(previewButton).Click();

            return this;
        }
        public DocumentSectionRecordPage ClickMenuButton()
        {
            WaitForElement(menuButton);
            Click(menuButton);

            return this;
        }
        public DocumentSectionRecordPage ClickDocumentSectionApplicationAccessLink()
        {
            WaitForElement(DocumentSectionApplicationAccessLeftSubMenu);
            Click(DocumentSectionApplicationAccessLeftSubMenu);

            return this;
        }
        public DocumentSectionRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        public DocumentSectionRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public DocumentSectionRecordPage ClickQuestionsTab()
        {
            WaitForElement(questionsTab);
            Click(questionsTab);

            return this;
        }




        public DocumentSectionRecordPage ValidateDocumentSectionApplicationAccessLinkVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DocumentSectionApplicationAccessLeftSubMenu);
            else
                WaitForElementNotVisible(DocumentSectionApplicationAccessLeftSubMenu, 5);

            return this;
        }
        public DocumentSectionRecordPage ValidateAvailabilityFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(availabilityField);
            else
                WaitForElementNotVisible(availabilityField, 5);

            return this;
        }




        public DocumentSectionRecordPage ValidateAvailabilityFieldSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(availabilityField, ExpectedText);

            return this;
        }



        public DocumentSectionRecordPage SelectAvailability(string TextToSelect)
        {
            SelectPicklistElementByText(availabilityField, TextToSelect);

            return this;
        }



    }
}
