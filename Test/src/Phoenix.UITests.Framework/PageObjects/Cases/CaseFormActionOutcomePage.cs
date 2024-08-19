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
    /// This class represents a "Case Form" record when accessed from a Case record.
    /// The "Case Form" page is the page displayed when a User:
    /// --Open a Case Record
    /// --Navigate to the Case Forms sub section
    /// --Tap on the "New" button to create a new Case Form or Open an existing record
    /// </summary>
    public class FormActionOutcomePage : CommonMethods
    {
        public FormActionOutcomePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By newCaseFormOutcomeIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=caseformoutcome&')]");

        #endregion

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Form Action/Outcome: ']/span");
        
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By menuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By auditButton = By.Id("CWNavItem_AuditHistory");


        #region Field Labels

        readonly By formFieldLabel = By.XPath("//*[@id='CWLabelHolder_caseformid']/label");
        readonly By actionsOutcomesFieldLabel = By.XPath("//*[@id='CWLabelHolder_caseformoutcometypeid']/label");

        #endregion

        #region Fields

        readonly By formField = By.XPath("//li[@id='CWControlHolder_caseformid']/div/div/a[@id='CWField_caseformid_Link']");
        readonly By formClearButton = By.XPath("//li[@id='CWControlHolder_caseformid']/div/div/button[@id='CWClearLookup_caseformid']");
        readonly By formLookupButton = By.XPath("//li[@id='CWControlHolder_caseformid']/div/div/button[@id='CWLookupBtn_caseformid']");
        readonly By outcomedateField = By.Id("CWField_outcomedate");

        readonly By actionsOutcomesField = By.XPath("//li[@id='CWControlHolder_caseformoutcometypeid']/div/div/a[@id='CWField_caseformoutcometypeid_Link']");
        readonly By actionsOutcomesClearButton = By.XPath("//li[@id='CWControlHolder_caseformoutcometypeid']/div/div/button[@id='CWClearLookup_caseformoutcometypeid']");
        readonly By actionsOutcomesLookupButton = By.XPath("//li[@id='CWControlHolder_caseformoutcometypeid']/div/div/button[@id='CWLookupBtn_caseformoutcometypeid']");



        #endregion

        public FormActionOutcomePage WaitForFormActionOutcomePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(newCaseFormOutcomeIFrame);
            SwitchToIframe(newCaseFormOutcomeIFrame);

            WaitForElement(pageHeader);
            WaitForElement(backButton);
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(formFieldLabel);
            WaitForElement(actionsOutcomesFieldLabel);
            
            WaitForElement(formLookupButton);
            WaitForElement(actionsOutcomesLookupButton);

            return this;
        }

        


        #region Validation methods



        public FormActionOutcomePage ValidateFormFieldLinkText(string ExpectedText)
        {
            ValidateElementText(formField, ExpectedText);

            return this;
        }

        public FormActionOutcomePage ValidateFormField(string ExpectedCaseTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string caseTitle = driver.FindElement(formField).Text;
            bool clearButtonVisible = GetElementVisibility(formClearButton);
            bool lookupButtonVisible = GetElementVisibility(formLookupButton);

            Assert.AreEqual(ExpectedCaseTitle, caseTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);


            return this;
        }

        public FormActionOutcomePage ValidateActionsOutcomesField(string ExpectedFormTypeTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string ActionsOutcomesTitle = driver.FindElement(actionsOutcomesField).Text;
            bool clearButtonVisible = GetElementVisibility(actionsOutcomesClearButton);
            bool lookupButtonVisible = GetElementVisibility(actionsOutcomesLookupButton);

            Assert.AreEqual(ExpectedFormTypeTitle, ActionsOutcomesTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);

            return this;
        }


        #endregion



        public FormActionOutcomePage InsertOutcomeDate(string outcomeDate)
        {
            SendKeys(outcomedateField, outcomeDate);

            return this;
        }

        public FormActionOutcomePage NavigateToAuditPage()
        {
            Click(menuButton);
            WaitForElement(auditButton);
            Click(auditButton);

            return this;
        }

        public FormActionOutcomePage TapClearFormButton()
        {
            Click(formClearButton);

            return this;
        }

        public FormActionOutcomePage TapActionsOutcomesLookupButton()
        {
            Click(actionsOutcomesLookupButton);

            return this;
        }

       


        public FormActionOutcomePage TapBackButton()
        {
            Click(backButton);

            return this;
        }

        public FormActionOutcomePage TapSaveButton()
        {
            Click(saveButton);
            
            return this;
        }

        public FormActionOutcomePage TapSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }
        
        public FormActionOutcomePage TapAdditionalToolbarElementsbutton()
        {
            Click(additionalToolbarElementsButton);

            return this;
        }

        public FormActionOutcomePage TapDeleteButton()
        {
            Click(deleteButton);

            return this;
        }

    }
}
