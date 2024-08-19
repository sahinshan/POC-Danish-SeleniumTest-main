using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AssessmentPrintHistoryPopup : CommonMethods
    {
        public AssessmentPrintHistoryPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_CWAssessmentPrintRecordsDialog");
        By popupHeader(string PopupHeader) => By.XPath("//header[@id='CWHeader']/h1/Span[text()='" + PopupHeader + "']");

        #region Fields

        readonly By sectionPicklist = By.Id("CWPrintRecordsSectionSelect");
        readonly By excludePrintAuditRecordsCheckBox = By.Id("CWExcludePrintAuditCheckbox");
        readonly By filterButton = By.XPath("//div[@id='CWAssessmentPrintRecordsFilterBar']/div/div/button[text()='Filter']");
        readonly By closeButton = By.XPath("//div[@id='CWAssessmentPrintRecordsDialog']/div/button[text()='Close']");

        readonly By noPrintRecordsMessage = By.XPath("//div[@id='CWNotificationMessage_RenderRecords'][text()='No print record available for this assessment.']");
        readonly By noPrintRecordsForSectionMessage = By.XPath("//div[@id='CWNotificationMessage_RenderRecords'][text()='No print record available for this section.']");


        #endregion

        #region Audit Record

        //

        By recordIconWord(string recordID) => By.XPath("//*[@id='CWPrintRecord_" + recordID + "']/div/div/div/div/span[@class='attachmenttype filedoc']");
        By recordIconPDF(string recordID) => By.XPath("//*[@id='CWPrintRecord_" + recordID + "']/div/div/div/div/span[@class='attachmenttype filepdf']");


        By printHistoryRecordParentElement(string recordID) => By.XPath("//div[@id='CWPrintRecord_" + recordID + "']");


        By recordTitleLink(string recordID, string Title) => By.XPath("//div[@id='CWPrintRecord_" + recordID + "']/div/div/div/div/h4/a[@title='Download file']/span[@id='CWPrintRecordTitle_" + recordID + "'][text()='" + Title + "']");

        By recordTitle(string recordID, string Title) => By.XPath("//div[@id='CWPrintRecord_" + recordID + "']/div/div/div/div/h4/span[@id='CWPrintRecordTitle_" + recordID + "'][text()='" + Title + "']");
        By recordCommentsLine1 (string recordID, string Line1Text) => By.XPath("//div[@id='CWPrintRecord_" + recordID + "']/div/div/div/div/p[@id='CWPrintRecordComments_" + recordID + "'][text()='" + Line1Text + "']");
        By recordCommentsLine2(string recordID, string CreationDate, string UserName) => By.XPath("//div[@id='CWPrintRecord_" + recordID + "']/div/div/div/div/p[text()='Created on " + CreationDate + " by " + UserName + "']");
        By recordCommentsLine3(string recordID, string TemplateName) => By.XPath("//div[@id='CWPrintRecord_" + recordID + "']/div/div/div/div/p[text()='Printed with template " + TemplateName + "']");

        By recordAdditionalLineComment(string recordID, string Comment) => By.XPath("//div[@id='CWPrintRecord_" + recordID + "']/div/div/div/div/p[text()='" + Comment + "']");


        By deleteButton(string recordID) => By.XPath("//div[@id='CWPrintRecord_" + recordID + "']/div/div/div/div/div/button[@title='Delete Record']");
        By editButton(string recordID) => By.XPath("//div[@id='CWPrintRecord_" + recordID + "']/div/div/div/div/div/button[@title='Edit Record']");



        #endregion



        public AssessmentPrintHistoryPopup WaitForAssessmentPrintHistoryPopupToLoad()
        {
            return WaitForAssessmentPrintHistoryPopupToLoad("All print records");
        }

        public AssessmentPrintHistoryPopup WaitForAssessmentPrintHistoryPopupToLoad(string PopupTitle)
        {
            Wait.Until(c => c.FindElement(popupIframe));
            driver.SwitchTo().Frame(driver.FindElement(popupIframe));

            Wait.Until(c => c.FindElement(popupHeader(PopupTitle)));

            Wait.Until(c => c.FindElement(sectionPicklist));
            Wait.Until(c => c.FindElement(excludePrintAuditRecordsCheckBox));
            Wait.Until(c => c.FindElement(filterButton));

            return this;
        }

        /// <summary>
        /// This method should be used to guarantee that the popup remains open after the user interacts with an js alert window (after either clicking O on Cancel)
        /// </summary>
        /// <returns></returns>
        public AssessmentPrintHistoryPopup WaitForAssessmentPrintHistoryPopupToLoadAfterAlert()
        {
            Wait.Until(c => c.FindElement(popupHeader("All print records")));

            Wait.Until(c => c.FindElement(sectionPicklist));
            Wait.Until(c => c.FindElement(excludePrintAuditRecordsCheckBox));
            Wait.Until(c => c.FindElement(filterButton));

            return this;
        }

        public AssessmentPrintHistoryPopup ValidatePrintHistoryRecordWordIcon(string PrintHistoryRecordID)
        {
            WaitForElement(recordIconWord(PrintHistoryRecordID));

            return this;
        }

        public AssessmentPrintHistoryPopup ValidatePrintHistoryRecordPDFIcon(string PrintHistoryRecordID)
        {
            WaitForElement(recordIconPDF(PrintHistoryRecordID));

            return this;
        }

        public AssessmentPrintHistoryPopup ValidateHistoryRecordPresent(string PrintHistoryRecordID, string Title, string Line1Text, string CreationDate, string UserName, string TemplateName)
        {
            Wait.Until(c => c.FindElement(recordTitle(PrintHistoryRecordID, Title)));

            Wait.Until(c => c.FindElement(recordCommentsLine1(PrintHistoryRecordID, Line1Text)));

            Wait.Until(c => c.FindElement(recordCommentsLine2(PrintHistoryRecordID, CreationDate, UserName)));

            Wait.Until(c => c.FindElement(recordCommentsLine3(PrintHistoryRecordID, TemplateName)));

            bool deleteButtonVisible = GetElementVisibility(deleteButton(PrintHistoryRecordID));
            bool editButtonVisible = GetElementVisibility(editButton(PrintHistoryRecordID));

            Assert.IsFalse(deleteButtonVisible);
            Assert.IsFalse(editButtonVisible);

            return this;
        }

        public AssessmentPrintHistoryPopup ValidateHistoryRecordPresent(string PrintHistoryRecordID, string Title, string Line1Text, string CreationDate, string UserName, string TemplateName, string AdditionalCommentLine)
        {
            Wait.Until(c => c.FindElement(recordTitle(PrintHistoryRecordID, Title)));

            Wait.Until(c => c.FindElement(recordCommentsLine1(PrintHistoryRecordID, Line1Text)));

            Wait.Until(c => c.FindElement(recordCommentsLine2(PrintHistoryRecordID, CreationDate, UserName)));

            Wait.Until(c => c.FindElement(recordCommentsLine3(PrintHistoryRecordID, TemplateName)));

            Wait.Until(c => c.FindElement(recordAdditionalLineComment(PrintHistoryRecordID, AdditionalCommentLine)));
            
            bool deleteButtonVisible = GetElementVisibility(deleteButton(PrintHistoryRecordID));
            bool editButtonVisible = GetElementVisibility(editButton(PrintHistoryRecordID));

            Assert.IsFalse(deleteButtonVisible);
            Assert.IsFalse(editButtonVisible);

            return this;
        }

        public AssessmentPrintHistoryPopup ValidateHistoryRecordNotPresent(string PrintHistoryRecordID)
        {
            bool elementVisible = GetElementVisibility(printHistoryRecordParentElement(PrintHistoryRecordID));
            Assert.IsFalse(elementVisible);
            
            return this;
        }

        public AssessmentPrintHistoryPopup ValidateHistoryWithSavedRecordPresent(string PrintHistoryRecordID, string Title, string Line1Text, string CreationDate, string UserName, string TemplateName)
        {
            Wait.Until(c => c.FindElement(recordTitleLink(PrintHistoryRecordID, Title)));
            
            Wait.Until(c => c.FindElement(recordCommentsLine1(PrintHistoryRecordID, Line1Text)));

            Wait.Until(c => c.FindElement(recordCommentsLine2(PrintHistoryRecordID, CreationDate, UserName)));

            Wait.Until(c => c.FindElement(recordCommentsLine3(PrintHistoryRecordID, TemplateName)));

            bool deleteButtonVisible = GetElementVisibility(deleteButton(PrintHistoryRecordID));
            bool editButtonVisible = GetElementVisibility(editButton(PrintHistoryRecordID));

            Assert.IsTrue(deleteButtonVisible);
            Assert.IsTrue(editButtonVisible);

            return this;
        }

        public AssessmentPrintHistoryPopup TapAuditLink(string PrintHistoryRecordID, string Title)
        {
            driver.FindElement(recordTitleLink(PrintHistoryRecordID, Title)).Click();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public AssessmentPrintHistoryPopup TapDeletebutton(string PrintHistoryRecordID)
        {
            WaitForElementToBeClickable(deleteButton(PrintHistoryRecordID));
            Click(deleteButton(PrintHistoryRecordID));

            return this;
        }

        public AssessmentPrintHistoryPopup TapEditbutton(string PrintHistoryRecordID)
        {
            WaitForElementToBeClickable(editButton(PrintHistoryRecordID));
            Click(editButton(PrintHistoryRecordID));

            return this;
        }

        public AssessmentPrintHistoryPopup CheckNoPrintRecordsMessageVisibility(bool ExpectVisible)
        {
            bool elementVisible = GetElementVisibility(noPrintRecordsMessage);
            Assert.AreEqual(ExpectVisible, elementVisible);

            return this;
        }

        public AssessmentPrintHistoryPopup CheckNoPrintRecordsForSectionMessageVisibility(bool ExpectVisible)
        {
            bool elementVisible = GetElementVisibility(noPrintRecordsForSectionMessage);
            Assert.AreEqual(ExpectVisible, elementVisible);

            return this;
        }

        public AssessmentPrintHistoryPopup SelectSection(string SectionName)
        {
            IWebElement element = driver.FindElement(sectionPicklist);
            SelectElement select = new SelectElement(element);
            select.SelectByText(SectionName);

            return this;
        }

        public AssessmentPrintHistoryPopup ValidateSectionSelectedElement(string ExpectedSelectedText)
        {
            ValidatePicklistSelectedText(sectionPicklist, ExpectedSelectedText);

            return this;
        }

        public AssessmentPrintHistoryPopup TapExcludePrintAuditRecordsCheckbox()
        {
            driver.FindElement(excludePrintAuditRecordsCheckBox).Click();

            return this;
        }

        public AssessmentPrintHistoryPopup TapFilterButton()
        {
            driver.FindElement(filterButton).Click();
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
    }
}
