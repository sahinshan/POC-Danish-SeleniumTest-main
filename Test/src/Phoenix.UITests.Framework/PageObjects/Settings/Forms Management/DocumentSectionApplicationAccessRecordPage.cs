using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DocumentSectionApplicationAccessRecordPage : CommonMethods
    {
        public DocumentSectionApplicationAccessRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=documentsectionapplicationaccess&')]");




        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");
        readonly By detailsSection = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");

        readonly By SectionFieldLabel = By.XPath("//*[@id='CWLabelHolder_documentsectionid']/label");
        readonly By ApplicationFieldLabel = By.XPath("//*[@id='CWLabelHolder_applicationid']/label");
        readonly By CanEditFieldLabel = By.XPath("//*[@id='CWLabelHolder_canedit']/label");


        readonly By sectionLinkField = By.XPath("//*[@id='CWField_documentsectionid_Link']");
        readonly By sectionClearLookupButton = By.XPath("//*[@id='CWClearLookup_documentsectionid']");
        readonly By sectionLookupButton = By.XPath("//*[@id='CWLookupBtn_documentsectionid']");

        readonly By applicationLinkField = By.XPath("//*[@id='CWField_applicationid_Link']");
        readonly By applicationClearLookupButton = By.XPath("//*[@id='CWClearLookup_applicationid']");
        readonly By applicationLookupButton = By.XPath("//*[@id='CWLookupBtn_applicationid']");

        readonly By canEdit_YesRadioButton = By.XPath("//*[@id='CWField_canedit_1']");
        readonly By canEdit_NoRadioButton = By.XPath("//*[@id='CWField_canedit_0']");



        public DocumentSectionApplicationAccessRecordPage WaitForDocumentSectionApplicationAccessRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(detailsSection);
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(SectionFieldLabel);
            WaitForElement(ApplicationFieldLabel);
            WaitForElement(CanEditFieldLabel);


            return this;
        }


        public DocumentSectionApplicationAccessRecordPage ValidiateSectionFieldLinkText(string ExpectedText)
        {
            ValidateElementText(sectionLinkField, ExpectedText);

            return this;
        }
        public DocumentSectionApplicationAccessRecordPage ValidiateApplicationFieldLinkText(string ExpectedText)
        {
            ValidateElementText(applicationLinkField, ExpectedText);

            return this;
        }


        public DocumentSectionApplicationAccessRecordPage ValidiateCanEditYesOptionChecked(bool ExpectedChecked)
        {
            if (ExpectedChecked)
                ValidateElementChecked(canEdit_YesRadioButton);
            else
                ValidateElementNotChecked(canEdit_YesRadioButton);

            return this;
        }
        public DocumentSectionApplicationAccessRecordPage ValidiateCanEditNoOptionChecked(bool ExpectedChecked)
        {
            if (ExpectedChecked)
                ValidateElementChecked(canEdit_NoRadioButton);
            else
                ValidateElementNotChecked(canEdit_NoRadioButton);

            return this;
        }



        public DocumentSectionApplicationAccessRecordPage ClickSectionClearLookupButton()
        {
            Click(sectionClearLookupButton);

            return this;
        }
        public DocumentSectionApplicationAccessRecordPage ClickSectionLookupButton()
        {
            Click(sectionLookupButton);

            return this;
        }
        public DocumentSectionApplicationAccessRecordPage ClickApplicationClearLookupButton()
        {
            Click(applicationClearLookupButton);

            return this;
        }
        public DocumentSectionApplicationAccessRecordPage ClickApplicationLookupButton()
        {
            Click(applicationLookupButton);

            return this;
        }
        public DocumentSectionApplicationAccessRecordPage ClickCanEditYesRadioButton()
        {
            Click(canEdit_YesRadioButton);

            return this;
        }
        public DocumentSectionApplicationAccessRecordPage ClickCanEditNoRadioButton()
        {
            Click(canEdit_NoRadioButton);

            return this;
        }
        public DocumentSectionApplicationAccessRecordPage ClickSaveButton()
        {
            Click(saveButton);

            return this;
        }
        public DocumentSectionApplicationAccessRecordPage ClickSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            return this;
        }
        public DocumentSectionApplicationAccessRecordPage ClickDeleteButton()
        {
            Click(deleteButton);

            return this;
        }



    }
}
