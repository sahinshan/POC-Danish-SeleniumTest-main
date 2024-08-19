using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DocumentApplicationAccessRecordPage : CommonMethods
    {
        public DocumentApplicationAccessRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=documentapplicationaccess&')]");




        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");
        readonly By detailsSection = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");

        readonly By DocumentFieldLabel = By.XPath("//*[@id='CWLabelHolder_documentid']/label");
        readonly By ApplicationFieldLabel = By.XPath("//*[@id='CWLabelHolder_applicationid']/label");
        readonly By CanEditFieldLabel = By.XPath("//*[@id='CWLabelHolder_canedit']/label");


        readonly By documentLinkField = By.XPath("//*[@id='CWField_documentid_Link']");
        readonly By documentClearLookupButton = By.XPath("//*[@id='CWClearLookup_documentid']");
        readonly By documentLookupButton = By.XPath("//*[@id='CWLookupBtn_documentid']");

        readonly By applicationLinkField = By.XPath("//*[@id='CWField_applicationid_Link']");
        readonly By applicationClearLookupButton = By.XPath("//*[@id='CWClearLookup_applicationid']");
        readonly By applicationLookupButton = By.XPath("//*[@id='CWLookupBtn_applicationid']");

        readonly By canEdit_YesRadioButton = By.XPath("//*[@id='CWField_canedit_1']");
        readonly By canEdit_NoRadioButton = By.XPath("//*[@id='CWField_canedit_0']");



        public DocumentApplicationAccessRecordPage WaitForDocumentApplicationAccessRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(detailsSection);
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(DocumentFieldLabel);
            WaitForElement(ApplicationFieldLabel);
            WaitForElement(CanEditFieldLabel);


            return this;
        }


        public DocumentApplicationAccessRecordPage ValidiateDocumentFieldLinkText(string ExpectedText)
        {
            ValidateElementText(documentLinkField, ExpectedText);

            return this;
        }
        public DocumentApplicationAccessRecordPage ValidiateApplicationFieldLinkText(string ExpectedText)
        {
            ValidateElementText(applicationLinkField, ExpectedText);

            return this;
        }


        public DocumentApplicationAccessRecordPage ValidiateCanEditYesOptionChecked(bool ExpectedChecked)
        {
            if (ExpectedChecked)
                ValidateElementChecked(canEdit_YesRadioButton);
            else
                ValidateElementNotChecked(canEdit_YesRadioButton);

            return this;
        }
        public DocumentApplicationAccessRecordPage ValidiateCanEditNoOptionChecked(bool ExpectedChecked)
        {
            if (ExpectedChecked)
                ValidateElementChecked(canEdit_NoRadioButton);
            else
                ValidateElementNotChecked(canEdit_NoRadioButton);

            return this;
        }



        public DocumentApplicationAccessRecordPage ClickDocumentClearLookupButton()
        {
            Click(documentClearLookupButton);

            return this;
        }
        public DocumentApplicationAccessRecordPage ClickDocumentLookupButton()
        {
            Click(documentLookupButton);

            return this;
        }
        public DocumentApplicationAccessRecordPage ClickApplicationClearLookupButton()
        {
            Click(applicationClearLookupButton);

            return this;
        }
        public DocumentApplicationAccessRecordPage ClickApplicationLookupButton()
        {
            Click(applicationLookupButton);

            return this;
        }
        public DocumentApplicationAccessRecordPage ClickCanEditYesRadioButton()
        {
            Click(canEdit_YesRadioButton);

            return this;
        }
        public DocumentApplicationAccessRecordPage ClickCanEditNoRadioButton()
        {
            Click(canEdit_NoRadioButton);

            return this;
        }
        public DocumentApplicationAccessRecordPage ClickSaveButton()
        {
            Click(saveButton);

            return this;
        }
        public DocumentApplicationAccessRecordPage ClickSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            return this;
        }
        public DocumentApplicationAccessRecordPage ClickDeleteButton()
        {
            Click(deleteButton);

            return this;
        }



    }
}
