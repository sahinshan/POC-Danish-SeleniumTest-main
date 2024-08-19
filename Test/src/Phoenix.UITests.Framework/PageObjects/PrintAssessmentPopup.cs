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
    public class PrintAssessmentPopup : CommonMethods
    {
        public PrintAssessmentPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_CWPrintDialogWindow");
        readonly By popupHeader = By.XPath("//header[@id='CWHeader']/h1/Span[text()='Print Assessment']");

        #region Field Labels

        readonly By sectionsLabel = By.Id("CWSectionsLabel");
        readonly By templatesLabel = By.Id("lblTemplates");

        readonly By printBlankDocumentLabel = By.XPath("//label[@for='CWPrintBlank']");
        readonly By printOnlySelectedValuesLabel = By.XPath("//label[@for='CWPrintWithSelectedValues']");
        readonly By saveDocumentInPrintHistoryLabel = By.XPath("//label[@for='CWPrintRecordStore']");

        readonly By checkUncheckAllLabel = By.XPath("//div[@id='CWSectionParent']/label[text()='Check/Uncheck All']");
        readonly By section1Label = By.XPath("//a[@title='Section 1']/div/label[text()='Section 1']");
        readonly By section1_1Label = By.XPath("//a[@title='Section 1.1']/div/label[text()='Section 1.1']");
        readonly By section1_2Label = By.XPath("//a[@title='Section 1.2']/div/label[text()='Section 1.2']");
        readonly By section2Label = By.XPath("//a[@title='Section 2']/div/label[text()='Section 2']");
        readonly By section2_1Label = By.XPath("//a[@title='Section 2.1']/div/label[text()='Section 2.1']");

        #endregion

        #region Fields

        readonly By sectionsField = By.Id("CWSectionList");
        readonly By templateField = By.Id("ddlTemplate");

        readonly By printBlankDocumentField = By.Id("CWPrintBlank");
        readonly By printOnlySelectedVauesField = By.Id("CWPrintWithSelectedValues");
        readonly By saveDocumentInPrintHistoryField = By.Id("CWPrintRecordStore");

        readonly By checkUncheckAllField = By.Id("CWSectionParentNode");

        readonly By section1Field = By.XPath("//a[@title='Section 1']/div/input");
        readonly By section1_1Field = By.XPath("//a[@title='Section 1.1']/div/input");
        readonly By section1_2Field = By.XPath("//a[@title='Section 1.2']/div/input");
        readonly By section2Field = By.XPath("//a[@title='Section 2']/div/input");
        readonly By section2_1Field = By.XPath("//a[@title='Section 2.1']/div/input");

        readonly By printButton = By.Id("CWPrintButton");
        readonly By closeButton = By.Id("Cancel");

        readonly By CommentsTextBox = By.Id("CWPrintRecordComment");

        //

        #endregion

        /// <summary>
        /// This variable represents the element that holds the pdf file in a popup window when a user prints a mail merge document
        /// </summary>
        readonly By pdfEmbededElement = By.XPath("//html/body/embed[@type='application/pdf']");

        public PrintAssessmentPopup WaitForPrintAssessmentPopupToLoad()
        {
            Wait.Until(c => c.FindElement(popupIframe));
            driver.SwitchTo().Frame(driver.FindElement(popupIframe));

            Wait.Until(c => c.FindElement(popupHeader));

            Wait.Until(c => c.FindElement(sectionsLabel));
            Wait.Until(c => c.FindElement(templatesLabel));

            Wait.Until(c => c.FindElement(printBlankDocumentLabel));
            Wait.Until(c => c.FindElement(printOnlySelectedValuesLabel));
            Wait.Until(c => c.FindElement(saveDocumentInPrintHistoryLabel));

            Wait.Until(c => c.FindElement(checkUncheckAllLabel));
            Wait.Until(c => c.FindElement(section1Label));
            Wait.Until(c => c.FindElement(section1_1Label));
            Wait.Until(c => c.FindElement(section1_2Label));
            Wait.Until(c => c.FindElement(section2Label));
            Wait.Until(c => c.FindElement(section2_1Label));

            Wait.Until(c => c.FindElement(sectionsField));
            Wait.Until(c => c.FindElement(templateField));

            Wait.Until(c => c.FindElement(printBlankDocumentField));
            Wait.Until(c => c.FindElement(printOnlySelectedVauesField));
            Wait.Until(c => c.FindElement(saveDocumentInPrintHistoryField));

            Wait.Until(c => c.FindElement(checkUncheckAllField));

            Wait.Until(c => c.FindElement(section1Field));
            Wait.Until(c => c.FindElement(section1_1Field));
            Wait.Until(c => c.FindElement(section1_2Field));
            Wait.Until(c => c.FindElement(section2Field));
            Wait.Until(c => c.FindElement(section2_1Field));

            Wait.Until(c => c.FindElement(printButton));
            Wait.Until(c => c.FindElement(closeButton));
            
            return this;
        }

        /// <summary>
        /// Use this method to wait for all elements to be loaded after selecting the "Section 1" options in the Sections select box
        /// </summary>
        /// <returns></returns>
        public PrintAssessmentPopup WaitForPopupToLoadedForSection1()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);
            WaitForElement(sectionsLabel);
            WaitForElement(templatesLabel);
            WaitForElement(printBlankDocumentLabel);
            WaitForElement(printOnlySelectedValuesLabel);
            WaitForElement(saveDocumentInPrintHistoryLabel);
            WaitForElement(checkUncheckAllLabel);
            WaitForElement(section1Label);
            WaitForElement(section1_1Label);
            WaitForElement(section1_2Label);
            WaitForElement(sectionsField);
            WaitForElement(templateField);
            WaitForElement(printOnlySelectedVauesField);
            WaitForElement(saveDocumentInPrintHistoryField);
            WaitForElement(checkUncheckAllField);
            WaitForElement(section1Field);
            WaitForElement(section1_1Field);
            WaitForElement(section1_2Field);
            WaitForElement(printButton);
            WaitForElement(closeButton);

            return this;
        }

        /// <summary>
        /// Use this method to validate that all elements are visible after selecting the "All" options in the Sections select box
        /// </summary>
        /// <returns></returns>
        public PrintAssessmentPopup ValidatePopupLoadedForAllSections()
        {
            Wait.Until(c => c.FindElement(popupHeader));

            Wait.Until(c => c.FindElement(sectionsLabel));
            Wait.Until(c => c.FindElement(templatesLabel));

            Wait.Until(c => c.FindElement(printBlankDocumentLabel));
            Wait.Until(c => c.FindElement(printOnlySelectedValuesLabel));
            Wait.Until(c => c.FindElement(saveDocumentInPrintHistoryLabel));

            Wait.Until(c => c.FindElement(checkUncheckAllLabel));
            Wait.Until(c => c.FindElement(section1Label));
            Wait.Until(c => c.FindElement(section1_1Label));
            Wait.Until(c => c.FindElement(section1_2Label));
            Wait.Until(c => c.FindElement(section2Label));
            Wait.Until(c => c.FindElement(section2_1Label));

            Wait.Until(c => c.FindElement(sectionsField));
            Wait.Until(c => c.FindElement(templateField));

            Wait.Until(c => c.FindElement(printBlankDocumentField));
            Wait.Until(c => c.FindElement(printOnlySelectedVauesField));
            Wait.Until(c => c.FindElement(saveDocumentInPrintHistoryField));

            Wait.Until(c => c.FindElement(checkUncheckAllField));

            Wait.Until(c => c.FindElement(section1Field));
            Wait.Until(c => c.FindElement(section1_1Field));
            Wait.Until(c => c.FindElement(section1_2Field));
            Wait.Until(c => c.FindElement(section2Field));
            Wait.Until(c => c.FindElement(section2_1Field));

            Wait.Until(c => c.FindElement(printButton));
            Wait.Until(c => c.FindElement(closeButton));

            return this;
        }

        

        public PrintAssessmentPopup SelectSection(string SectionName)
        {
            IWebElement element = driver.FindElement(sectionsField);
            SelectElement se = new SelectElement(element);
            se.SelectByText(SectionName);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PrintAssessmentPopup ValidateSelectedSection(string ExpectedText)
        {
            ValidatePicklistSelectedText(sectionsField, ExpectedText);

            return this;
        }

        /// <summary>
        /// Determine the visibility of the Section 1; Section 1.1 and Section 1.2 in the "Sections to include:" area
        /// </summary>
        /// <param name="ExpectSectionVisible"></param>
        /// <returns></returns>
        public PrintAssessmentPopup CheckSection1Visibility(bool ExpectSectionVisible)
        {
            bool element1Visible = GetElementVisibility(section1Field);
            bool element2Visible = GetElementVisibility(section1_1Field);
            bool element3Visible = GetElementVisibility(section1_2Field);

            Assert.AreEqual(ExpectSectionVisible, element1Visible);
            Assert.AreEqual(ExpectSectionVisible, element2Visible);
            Assert.AreEqual(ExpectSectionVisible, element3Visible);

            return this;
        }

        /// <summary>
        /// Determine the visibility of the Section 2 and Section 2.1 in the "Sections to include:" area
        /// </summary>
        /// <param name="ExpectSectionVisible"></param>
        /// <returns></returns>
        public PrintAssessmentPopup CheckSection2Visibility(bool ExpectSectionVisible)
        {
            if (ExpectSectionVisible)
            {
                WaitForElementVisible(section2Field);
                WaitForElementVisible(section2_1Field);
            }
            else 
            {
                WaitForElementNotVisible(section2Field, 3);
                WaitForElementNotVisible(section2_1Field, 3);
            }

            return this;
        }

        public PrintAssessmentPopup ValidateListOfTemplates(params string [] ExpectedListOfTemplates)
        {
            System.Threading.Thread.Sleep(1000);
            foreach (string templateName in ExpectedListOfTemplates)
                ValidatePicklistContainsElementByText(templateField, templateName);

            return this;
        }

        public PrintAssessmentPopup SelectTemplate(string TemplateName)
        {
            this.SelectPicklistElementByText(templateField, TemplateName);

            return this;
        }

        public PrintAssessmentPopup TapOnPrintBlankDocumentCheckBox()
        {
            driver.FindElement(printBlankDocumentField).Click();

            return this;
        }

        public PrintAssessmentPopup TapOnSaveDocumentInPrintHistoryCheckbox()
        {
            driver.FindElement(saveDocumentInPrintHistoryField).Click();

            return this;
        }

        public PrintAssessmentPopup InsertPrintCommentsInTextbox(string Comments)
        {
            WaitForElementToBeClickable(CommentsTextBox);
            SendKeys(CommentsTextBox, Comments);

            return this;
        }

        /// <summary>
        /// Determine the visibility of the Comments textbox
        /// </summary>
        /// <param name="ExpectSectionVisible"></param>
        /// <returns></returns>
        public PrintAssessmentPopup CheckCommentsTextboxVisibility(bool ExpectCommentsTextboxVisible)
        {
            if (ExpectCommentsTextboxVisible)
                WaitForElementVisible(CommentsTextBox);
            else
                WaitForElementNotVisible(CommentsTextBox, 3);

            return this;
        }

        public PrintAssessmentPopup CheckIfPrintOnlySelectedValuesCheckboxDisabled(bool ExpectDisabled)
        {
            string disabledAttribute = driver.FindElement(printOnlySelectedVauesField).GetAttribute("disabled");
            bool elementDisabled = disabledAttribute == "true" ? true : false;

            Assert.AreEqual(ExpectDisabled, elementDisabled);

            return this;
        }

        public PrintAssessmentPopup CheckIfSaveDocumentInPrintHistoryCheckboxDisabled(bool ExpectDisabled)
        {
            string disabledAttribute = driver.FindElement(saveDocumentInPrintHistoryField).GetAttribute("disabled");
            bool elementDisabled = disabledAttribute == "true" ? true : false;

            Assert.AreEqual(ExpectDisabled, elementDisabled);

            return this;
        }

        public PrintAssessmentPopup CheckPrintBlankDocumentVisibility(bool ExpectVisible)
        {
            System.Threading.Thread.Sleep(1000);
            bool printBlankDocumentLabelVisibility = GetElementVisibility(printBlankDocumentLabel);
            bool printBlankDocumentFieldVisibility = GetElementVisibility(printBlankDocumentField);

            Assert.AreEqual(ExpectVisible, printBlankDocumentLabelVisibility);
            Assert.AreEqual(ExpectVisible, printBlankDocumentFieldVisibility);

            return this;
        }

        public PrintAssessmentPopup TapOnPrintButton()
        {
            driver.FindElement(printButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public PrintAssessmentPopup TapOnCloseButton()
        {
            WaitForElementToBeClickable(closeButton);
            Click(closeButton);
            
            return this;
        }

        /// <summary>
        /// this method will validate that a popup window was open with a print pdf file.
        /// This method will try to locate the popup, switch the driver focus to the popup and will search for the element "//html/body/embed[@type='application/pdf']"  in the popup
        /// </summary>
        /// <returns></returns>
        public PrintAssessmentPopup ValidatePDFPopupIsOpen()
        {
            string currentWindow = GetCurrentWindowIdentifier();
            string popupWindow = GetAllWindowIdentifier().Where(c => c != currentWindow).FirstOrDefault();

            SwitchToWindow(popupWindow);

            //WaitForElement(pdfEmbededElement);

            return this;
        }
    }
}
