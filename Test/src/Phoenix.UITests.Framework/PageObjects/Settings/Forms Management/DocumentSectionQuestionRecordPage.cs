using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DocumentSectionQuestionRecordPage : CommonMethods
    {
        public DocumentSectionQuestionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By documentIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=documentsectionquestion&')]"); 


        #region Top Menu

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        #endregion

        #region Navigation Area

        readonly By menuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");
        readonly By detailsTab = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");
        

        #region Left Sub Menu

        readonly By DocumentSectionQuestionApplicationAccessLeftSubMenu = By.Id("CWNavItem_DocumentSectionQuestionApplicationAccess");
        readonly By auditLeftSubMenu = By.Id("CWNavItem_AuditHistory");

        #endregion

        #endregion

        #region Field Labels

        readonly By displayPositionFieldLabel = By.XPath("//*[@id='CWLabelHolder_displayposition']/label");
        readonly By availabilityFieldLabel = By.XPath("//*[@id='CWLabelHolder_availabilityid']/label");

        #endregion

        #region Fields

        readonly By displayPositionField = By.Id("CWField_displayposition");
        readonly By availabilityField = By.Id("CWField_availabilityid");

        #endregion


        public DocumentSectionQuestionRecordPage WaitForDocumentSectionQuestionRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(documentIFrame);
            SwitchToIframe(documentIFrame);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(deleteButton);
            WaitForElement(detailsTab);
            WaitForElement(menuButton);
            
            WaitForElement(displayPositionFieldLabel);
            WaitForElement(availabilityFieldLabel);

            return this;

        }



        public DocumentSectionQuestionRecordPage TapDeleteButton()
        {
            Click(deleteButton);

            return this;
        }
        public DocumentSectionQuestionRecordPage ClickMenuButton()
        {
            WaitForElement(menuButton);
            Click(menuButton);

            return this;
        }
        public DocumentSectionQuestionRecordPage ClickDocumentSectionQuestionApplicationAccessLink()
        {
            WaitForElement(DocumentSectionQuestionApplicationAccessLeftSubMenu);
            Click(DocumentSectionQuestionApplicationAccessLeftSubMenu);

            return this;
        }
        public DocumentSectionQuestionRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        public DocumentSectionQuestionRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }




        public DocumentSectionQuestionRecordPage ValidateDocumentSectionQuestionApplicationAccessLinkVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DocumentSectionQuestionApplicationAccessLeftSubMenu);
            else
                WaitForElementNotVisible(DocumentSectionQuestionApplicationAccessLeftSubMenu, 5);

            return this;
        }
        public DocumentSectionQuestionRecordPage ValidateAvailabilityFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(availabilityField);
            else
                WaitForElementNotVisible(availabilityField, 5);

            return this;
        }




        public DocumentSectionQuestionRecordPage ValidateAvailabilityFieldSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(availabilityField, ExpectedText);

            return this;
        }



        public DocumentSectionQuestionRecordPage SelectAvailability(string TextToSelect)
        {
            SelectPicklistElementByText(availabilityField, TextToSelect);

            return this;
        }



    }
}
