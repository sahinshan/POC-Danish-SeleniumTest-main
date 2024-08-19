using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DocumentSectionQuestionApplicationAccessRecordPage : CommonMethods
    {
        public DocumentSectionQuestionApplicationAccessRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=documentsectionquestionapplicationaccess&')]");




        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");
        readonly By detailsSection = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");

        readonly By QuestionFieldLabel = By.XPath("//*[@id='CWLabelHolder_documentsectionquestionid']/label");
        readonly By ApplicationFieldLabel = By.XPath("//*[@id='CWLabelHolder_applicationid']/label");
        readonly By CanEditFieldLabel = By.XPath("//*[@id='CWLabelHolder_canedit']/label");


        readonly By questionLinkField = By.XPath("//*[@id='CWField_documentsectionquestionid_Link']");
        readonly By questionClearLookupButton = By.XPath("//*[@id='CWClearLookup_documentsectionquestionid']");
        readonly By questionLookupButton = By.XPath("//*[@id='CWLookupBtn_documentsectionquestionid']");

        readonly By applicationLinkField = By.XPath("//*[@id='CWField_applicationid_Link']");
        readonly By applicationClearLookupButton = By.XPath("//*[@id='CWClearLookup_applicationid']");
        readonly By applicationLookupButton = By.XPath("//*[@id='CWLookupBtn_applicationid']");

        readonly By canEdit_YesRadioButton = By.XPath("//*[@id='CWField_canedit_1']");
        readonly By canEdit_NoRadioButton = By.XPath("//*[@id='CWField_canedit_0']");



        public DocumentSectionQuestionApplicationAccessRecordPage WaitForDocumentSectionQuestionApplicationAccessRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(detailsSection);
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(QuestionFieldLabel);
            WaitForElement(ApplicationFieldLabel);
            WaitForElement(CanEditFieldLabel);


            return this;
        }


        public DocumentSectionQuestionApplicationAccessRecordPage ValidiateQuestionFieldLinkText(string ExpectedText)
        {
            ValidateElementText(questionLinkField, ExpectedText);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessRecordPage ValidiateApplicationFieldLinkText(string ExpectedText)
        {
            ValidateElementText(applicationLinkField, ExpectedText);

            return this;
        }


        public DocumentSectionQuestionApplicationAccessRecordPage ValidiateCanEditYesOptionChecked(bool ExpectedChecked)
        {
            if (ExpectedChecked)
                ValidateElementChecked(canEdit_YesRadioButton);
            else
                ValidateElementNotChecked(canEdit_YesRadioButton);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessRecordPage ValidiateCanEditNoOptionChecked(bool ExpectedChecked)
        {
            if (ExpectedChecked)
                ValidateElementChecked(canEdit_NoRadioButton);
            else
                ValidateElementNotChecked(canEdit_NoRadioButton);

            return this;
        }



        public DocumentSectionQuestionApplicationAccessRecordPage ClickQuestionClearLookupButton()
        {
            Click(questionClearLookupButton);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessRecordPage ClickQuestionLookupButton()
        {
            Click(questionLookupButton);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessRecordPage ClickApplicationClearLookupButton()
        {
            Click(applicationClearLookupButton);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessRecordPage ClickApplicationLookupButton()
        {
            Click(applicationLookupButton);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessRecordPage ClickCanEditYesRadioButton()
        {
            Click(canEdit_YesRadioButton);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessRecordPage ClickCanEditNoRadioButton()
        {
            Click(canEdit_NoRadioButton);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessRecordPage ClickSaveButton()
        {
            Click(saveButton);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessRecordPage ClickSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            return this;
        }
        public DocumentSectionQuestionApplicationAccessRecordPage ClickDeleteButton()
        {
            Click(deleteButton);

            return this;
        }



    }
}
