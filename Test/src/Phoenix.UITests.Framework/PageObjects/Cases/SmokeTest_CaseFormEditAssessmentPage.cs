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
    /// <summary>
    /// This class represents the Edit Assessment page for the document type "Automation - Person Form 1"
    /// 
    /// </summary>
    public class SmokeTest_CaseFormEditAssessmentPage : CommonMethods
    {
        public SmokeTest_CaseFormEditAssessmentPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        #region Locators

        By notificationArea(string expectedMessage) => By.XPath("//*[@id='CWNotificationMessage_Assessment'][text()='" + expectedMessage + "']");


        #region Iframes

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        By iframe_CWDialog_(string AssessmentID) => By.Id("iframe_CWDialog_" + AssessmentID + "");
        readonly By iframe_CWAssessmentDialog = By.Id("iframe_CWAssessmentDialog");

        #endregion

        #region Top Menu


        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_CWAssessmentSaveAndCloseButton");
        readonly By printButton = By.Id("TI_CWAssessmentPrintButton");
        readonly By printHistoryButton = By.Id("TI_CWAssessmentPrintRecordButton");

        readonly By additionalToolbarItemsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");

        readonly By spellCheckButton = By.Id("TI_CWAssessmentSpellCheckButton");
        readonly By mandatoryQuestionsButton = By.Id("TI_CWDisplayMandatoryButton");
        readonly By expandAllButton = By.Id("TI_CWAssessmentExpandAll");
        readonly By collapseAllButton = By.Id("TI_CWAssessmentCollapseAll");
        readonly By changeLanguageButton = By.Id("TI_CWChangeLanguage");

        readonly By documentTitle = By.XPath("//div[@id='CWToolbar']/div/h1[text()='SmokeTest_CaseForm']");



        #endregion        


        #region Section 1

        By section1TitleLink(string SectionIdentifier) => By.XPath("//a[@id='CWSH_" + SectionIdentifier + "']");
        By section1Title(string SectionIdentifier) => By.XPath("//a[@id='CWSH_" + SectionIdentifier + "']/h2[text()='Section 1']");
        By section1Expanded(string SectionIdentifier) => By.XPath("//a[@id='CWSH_" + SectionIdentifier + "'][@class='section-heading level1 open']/h2[text()='Section 1']");
        By section1MenuButton(string SectionIdentifier) => By.XPath("//div[@id='CWS_" + SectionIdentifier + "']/fieldset/div/span/a[@title='Click to open']");



        #region Question Labels

        By SmokeTest_NumericQuestionHeading(string SectionQuestionIdentifier) => By.XPath("//ul[@id='" + SectionQuestionIdentifier + "']/li/label[text()='SmokeTest_NumericQuestion']");
        By SmokeTest_NumericQuestionSubHeading(string SectionQuestionIdentifier) => By.XPath("//ul[@id='" + SectionQuestionIdentifier + "']/li/label[text()='SmokeTest_NumericQuestion Sub Heading']");

        #endregion

        #region Questions

        By SmokeTest_NumericQuestion(string DocumentQuestionIdentifier) => By.XPath("//input[@id='" + DocumentQuestionIdentifier + "']");


        #endregion

        #endregion


        #endregion


        #region Methods

        public SmokeTest_CaseFormEditAssessmentPage WaitForEditAssessmentPageToLoad(string AssessmentID, string SectionIdentifier)
        {

            #region Iframes

            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog_(AssessmentID));
            SwitchToIframe(iframe_CWDialog_(AssessmentID));

            WaitForElement(iframe_CWAssessmentDialog);
            SwitchToIframe(iframe_CWAssessmentDialog);

            #endregion

            #region Top Menu

            WaitForElement(backButton);
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(printButton);
            WaitForElement(printHistoryButton);
            WaitForElement(additionalToolbarItemsButton);

            WaitForElement(documentTitle);

            #endregion

            WaitForElement(section1TitleLink(SectionIdentifier));
            WaitForElement(section1Title(SectionIdentifier));
            WaitForElement(section1Expanded(SectionIdentifier));
            WaitForElement(section1MenuButton(SectionIdentifier));

            return this;
        }

        public SmokeTest_CaseFormEditAssessmentPage ValidateNotificationAreaVisible(string ExpectedMessage)
        {
            WaitForElementVisible(notificationArea(ExpectedMessage));

            return this;
        }

        #region Top Menu

        public SmokeTest_CaseFormEditAssessmentPage TapBackButton()
        {
            Click(backButton);

            return this;
        }

        public SmokeTest_CaseFormEditAssessmentPage TapSaveButton()
        {
            return TapSaveButton(true);
        }

        public SmokeTest_CaseFormEditAssessmentPage TapSaveButton(bool WaitForRefreshPanelToClose)
        {
            Click(saveButton);

            if(WaitForRefreshPanelToClose)
                WaitForElementNotVisible("CWRefreshPanel", 7);

            System.Threading.Thread.Sleep(1000);

            return this;
        }

        public CaseFormPage TapSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return new CaseFormPage(this.driver, this.Wait, this.appURL);
        }

        public SmokeTest_CaseFormEditAssessmentPage TapAdditionaToolbarItemsButton()
        {
            Click(additionalToolbarItemsButton);

            return this;
        }

        public SmokeTest_CaseFormEditAssessmentPage WaitForAdditionalToolbarItemsDisplayed()
        {

            Wait.Until(c => c.FindElement(spellCheckButton).Displayed);
            Wait.Until(c => c.FindElement(mandatoryQuestionsButton).Displayed);
            Wait.Until(c => c.FindElement(expandAllButton).Displayed);
            Wait.Until(c => c.FindElement(collapseAllButton).Displayed);
            Wait.Until(c => c.FindElement(changeLanguageButton).Displayed);


            return this;
        }

        public PrintAssessmentPopup TapPrintButton()
        {
            WaitForElementToBeClickable(printButton);
            Click(printButton);

            return new PrintAssessmentPopup(driver, Wait, appURL);
        }

        #endregion

        

        #region Section 1

        #region Questions

        public SmokeTest_CaseFormEditAssessmentPage InsertSmokeTest_NumericQuestion(string DocumentQuestionIdentifier, string ValueToInsert)
        {
            SendKeys(SmokeTest_NumericQuestion(DocumentQuestionIdentifier), ValueToInsert);

            return this;
        }
       

        public SmokeTest_CaseFormEditAssessmentPage ValidateSmokeTest_NumericQuestion(string DocumentQuestionIdentifier, string ExpectedValue)
        {
            ValidateElementValue(SmokeTest_NumericQuestion(DocumentQuestionIdentifier), ExpectedValue);

            return this;
        }

        

        #endregion


        #endregion


        #endregion


    }
}
