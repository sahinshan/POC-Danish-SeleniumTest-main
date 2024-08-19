using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// This class represents the "Edit Audit" record for Print History of Assessment Records
    /// Open Case Record - Navigate to Case Forms - Open Case Form - Open Print History window - Find Audit record with attached document - Tap the Edit Audit record button
    /// </summary>
    public class EditPrintAuditRecordPopup : CommonMethods
    {
        readonly By TitleFieldLabel = By.XPath("//div[@id='CWPrintRecordEditDialog']//label[text()='Title']");
        readonly By commentsFieldLabel = By.XPath("//div[@id='CWPrintRecordEditDialog']//label[text()='Title']");

        readonly By TitleField = By.Id("CWEditPrintRecordTitle");
        readonly By commentsField = By.Id("CWEditPrintRecordComments");

        readonly By saveButton = By.XPath("//div[@id='CWPrintRecordEditDialog']//button[contains(@onclick, 'CW.AssessmentPrintRecords.Save')]");
        readonly By closeButton = By.XPath("//div[@id='CWPrintRecordEditDialog']//button[contains(@onclick, 'CW.AssessmentPrintRecords.CloseEdit')]");

        public EditPrintAuditRecordPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        
        public EditPrintAuditRecordPopup WaitForEditPrintAuditRecordPopupToLoad()
        {
            

            Wait.Until(c => c.FindElement(TitleFieldLabel));
            Wait.Until(c => c.FindElement(commentsFieldLabel));

            Wait.Until(ExpectedConditions.ElementToBeClickable(TitleField));
            Wait.Until(ExpectedConditions.ElementToBeClickable(commentsField));

            Wait.Until(c => c.FindElement(saveButton));
            Wait.Until(c => c.FindElement(closeButton));

            return this;
        }

        public EditPrintAuditRecordPopup InsertTitle(string Title)
        {
            driver.FindElement(TitleField).Clear();
            driver.FindElement(TitleField).SendKeys(Title);

            return this;
        }

        public EditPrintAuditRecordPopup InsertComments(string Comments)
        {
            driver.FindElement(commentsField).Clear();
            driver.FindElement(commentsField).SendKeys(Comments);

            return this;
        }

        public EditPrintAuditRecordPopup TapSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            return this;
        }

        public EditPrintAuditRecordPopup TapCloseButton()
        {
            WaitForElementToBeClickable(closeButton);
            MoveToElementInPage(closeButton);
            Click(closeButton);

            return this;
        }


    }
}
