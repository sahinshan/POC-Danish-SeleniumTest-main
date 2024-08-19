using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DocumentPage : CommonMethods
    {
        public DocumentPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By documentIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=document&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'


        #region Top Menu

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By previewButton = By.Id("TI_EditDocument");
        readonly By viewMappingsButton = By.Id("TI_ViewMappingsButton");

        #endregion

        #region Navigation Area

        readonly By detailsTab = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");
        readonly By sectionsTab = By.XPath("//li[@id='CWNavGroup_Sections']/a[@title='Sections']");
        readonly By printTemplatesTab = By.XPath("//*[@id='CWNavGroup_PrintTemplates']/a");

        readonly By menuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");

        #region Left Sub Menu

        readonly By DocumentApplicationAccessLeftSubMenu = By.Id("CWNavItem_DocumentApplicationAccess");
        readonly By auditLeftSubMenu = By.Id("CWNavItem_AuditHistory");

        #endregion

        #endregion

        #region Field Labels

        readonly By nameFieldLabel = By.XPath("//*[@id='CWLabelHolder_name']/label");
        readonly By availabilityFieldLabel = By.XPath("//*[@id='CWLabelHolder_availabilityid']/label");

        #endregion

        #region Fields

        readonly By nameField = By.Id("CWField_name");
        readonly By availabilityField = By.Id("CWField_availabilityid");

        #endregion


        public DocumentPage WaitForDocumentPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(documentIFrame);
            SwitchToIframe(documentIFrame);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(previewButton);
            WaitForElement(viewMappingsButton);
            WaitForElement(detailsTab);
            WaitForElement(menuButton);
            
            WaitForElement(nameFieldLabel);
            WaitForElement(availabilityFieldLabel);

            return this;

        }



        public DocumentPage TapPreviewButton()
        {
            driver.FindElement(previewButton).Click();

            return this;
        }
        public DocumentPage TapViewMappingsButton()
        {
            Click(viewMappingsButton);

            return this;
        }
        public DocumentPage ClickMenuButton()
        {
            WaitForElement(menuButton);
            Click(menuButton);

            return this;
        }
        public DocumentPage ClickDocumentApplicationAccessLink()
        {
            WaitForElement(DocumentApplicationAccessLeftSubMenu);
            Click(DocumentApplicationAccessLeftSubMenu);

            return this;
        }
        public DocumentPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        public DocumentPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public DocumentPage ClickSectionsTab()
        {
            WaitForElement(sectionsTab);
            Click(sectionsTab);

            return this;
        }
        public DocumentPage ClickPrintTemplatesTab()
        {
            WaitForElement(printTemplatesTab);
            Click(printTemplatesTab);

            return this;
        }




        public DocumentPage ValidateDocumentApplicationAccessLinkVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DocumentApplicationAccessLeftSubMenu);
            else
                WaitForElementNotVisible(DocumentApplicationAccessLeftSubMenu, 5);

            return this;
        }
        public DocumentPage ValidateAvailabilityFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(availabilityField);
            else
                WaitForElementNotVisible(availabilityField, 5);

            return this;
        }




        public DocumentPage ValidateAvailabilityFieldSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(availabilityField, ExpectedText);

            return this;
        }



        public DocumentPage SelectAvailability(string TextToSelect)
        {
            SelectPicklistElementByText(availabilityField, TextToSelect);

            return this;
        }



    }
}
