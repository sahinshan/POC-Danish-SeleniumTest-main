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
    /// 
    /// </summary>
    public class PersonFormActionOutcomePage : CommonMethods
    {
        public PersonFormActionOutcomePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By newPersonFormOutcomeIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personformoutcome&')]");

        #endregion

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Person Form Action/Outcome: ']/span");
        
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By NotificationMessageArea = By.Id("CWNotificationMessage_DataForm");


        #region Field Labels

        readonly By personFormFieldLabel = By.XPath("//*[@id='CWLabelHolder_personformid']/label");
        readonly By actionsOutcomesFieldLabel = By.XPath("//*[@id='CWLabelHolder_personformoutcometypeid']/label");

        #endregion

        #region Fields

        readonly By PersonFormField = By.XPath("//li[@id='CWControlHolder_personformid']/div/div/a[@id='CWField_personformid_Link']");
        readonly By PersonFormClearButton = By.XPath("//li[@id='CWControlHolder_personformid']/div/div/button[@id='CWClearLookup_personformid']");
        readonly By PersonFormLookupButton = By.XPath("//li[@id='CWControlHolder_personformid']/div/div/button[@id='CWLookupBtn_personformid']");

        readonly By actionsOutcomeField = By.XPath("//li[@id='CWControlHolder_personformoutcometypeid']/div/div/a[@id='CWField_personformoutcometypeid_Link']");
        readonly By actionsOutcomeClearButton = By.XPath("//li[@id='CWControlHolder_personformoutcometypeid']/div/div/button[@id='CWClearLookup_personformoutcometypeid']");
        readonly By actionsOutcomeLookupButton = By.XPath("//li[@id='CWControlHolder_personformoutcometypeid']/div/div/button[@id='CWLookupBtn_personformoutcometypeid']");
        readonly By actionsOutcomeErrorLabel = By.XPath("//*[@id='CWControlHolder_personformoutcometypeid']/label/span");

        readonly By CommentsField = By.XPath("//*[@id='CWField_comments']");

        readonly By ResponsibleTeamField = By.XPath("//li[@id='CWControlHolder_ownerid']/div/div/a[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamClearButton = By.XPath("//li[@id='CWControlHolder_ownerid']/div/div/button[@id='CWClearLookup_ownerid']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//li[@id='CWControlHolder_ownerid']/div/div/button[@id='CWLookupBtn_ownerid']");

        readonly By dateField = By.XPath("//*[@id='CWField_outcomedate']");
        readonly By dateErrorLabel = By.XPath("//*[@id='CWControlHolder_outcomedate']/label/span");

        #endregion

        public PersonFormActionOutcomePage WaitForPersonFormActionOutcomePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(newPersonFormOutcomeIFrame);
            SwitchToIframe(newPersonFormOutcomeIFrame);

            WaitForElement(pageHeader);
            WaitForElement(backButton);
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(personFormFieldLabel);
            WaitForElement(actionsOutcomesFieldLabel);
            
            WaitForElement(PersonFormLookupButton);
            WaitForElement(actionsOutcomeLookupButton);

            return this;
        }

        public PersonFormActionOutcomePage WaitForPersonFormActionOutcomePageToLoad(string ExpectedTitle)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(newPersonFormOutcomeIFrame);
            SwitchToIframe(newPersonFormOutcomeIFrame);

            WaitForElement(pageHeader);
            WaitForElement(backButton);
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(personFormFieldLabel);
            WaitForElement(actionsOutcomesFieldLabel);

            WaitForElement(PersonFormLookupButton);
            WaitForElement(actionsOutcomeLookupButton);

            ValidateElementTextContainsText(pageHeader, ExpectedTitle);

            return this;
        }



        public PersonFormActionOutcomePage ValidateNotificationMessageAreaText(string ExpectedText)
        {
            WaitForElementVisible(NotificationMessageArea);
            ValidateElementText(NotificationMessageArea, ExpectedText);

            return this;
        }
        public PersonFormActionOutcomePage ValidateActionsOutcomeErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(actionsOutcomeErrorLabel);
            ValidateElementText(actionsOutcomeErrorLabel, ExpectedText);

            return this;
        }
        public PersonFormActionOutcomePage ValidateDateErrorLabelText(string ExpectedText)
        {
            WaitForElementVisible(dateErrorLabel);
            ValidateElementText(dateErrorLabel, ExpectedText);

            return this;
        }

        public PersonFormActionOutcomePage ValidatePersonFormField(string ExpectedPersonFormTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string personFormTitle = driver.FindElement(PersonFormField).Text;
            bool clearButtonVisible = GetElementVisibility(PersonFormClearButton);
            bool lookupButtonVisible = GetElementVisibility(PersonFormLookupButton);

            Assert.AreEqual(ExpectedPersonFormTitle, personFormTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);


            return this;
        }
        public PersonFormActionOutcomePage ValidateActionsOutcomesField(string ExpectedFormTypeTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string ActionsOutcomesTitle = driver.FindElement(actionsOutcomeField).Text;
            bool clearButtonVisible = GetElementVisibility(actionsOutcomeClearButton);
            bool lookupButtonVisible = GetElementVisibility(actionsOutcomeLookupButton);

            Assert.AreEqual(ExpectedFormTypeTitle, ActionsOutcomesTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);

            return this;
        }
        public PersonFormActionOutcomePage ValidateResponsibleTeamField(string ExpectedFormTypeTitle, bool ExpectClearButtonVisible, bool ExpectLookupButtonVisible)
        {
            string ResponsibleTeamTitle = driver.FindElement(ResponsibleTeamField).Text;
            bool clearButtonVisible = GetElementVisibility(ResponsibleTeamClearButton);
            bool lookupButtonVisible = GetElementVisibility(ResponsibleTeamLookupButton);

            Assert.AreEqual(ExpectedFormTypeTitle, ResponsibleTeamTitle);
            Assert.AreEqual(ExpectClearButtonVisible, clearButtonVisible);
            Assert.AreEqual(ExpectLookupButtonVisible, lookupButtonVisible);

            return this;
        }
        public PersonFormActionOutcomePage ValidateCommentsField(string ExpectedText)
        {
            ValidateElementText(CommentsField, ExpectedText);

            return this;
        }
        public PersonFormActionOutcomePage ValidateDateField(string ExpectedText)
        {
            ValidateElementValue(dateField, ExpectedText);

            return this;
        }





        public PersonFormActionOutcomePage InsertComments(string TextToInsert)
        {
            SendKeys(CommentsField, TextToInsert);

            return this;
        }
        public PersonFormActionOutcomePage InsertDate(string TextToInsert)
        {
            SendKeys(dateField, TextToInsert);

            return this;
        }



        public PersonFormActionOutcomePage TapPersonFormLookupButton()
        {
            Click(PersonFormLookupButton);

            return this;
        }
        public PersonFormActionOutcomePage TapPersonFormClearButton()
        {
            Click(PersonFormClearButton);

            return this;
        }
        public PersonFormActionOutcomePage TapActionsOutcomesLookupButton()
        {
            Click(actionsOutcomeLookupButton);

            return this;
        }
        public PersonFormActionOutcomePage TapActionsOutcomesClearButton()
        {
            Click(actionsOutcomeClearButton);

            return this;
        }
        public PersonFormActionOutcomePage TapResponsibleTeamLookupButton()
        {
            Click(ResponsibleTeamLookupButton);

            return this;
        }
        public PersonFormActionOutcomePage TapResponsibleTeamClearButton()
        {
            Click(ResponsibleTeamClearButton);

            return this;
        }
        public PersonFormActionOutcomePage TapBackButton()
        {
            Click(backButton);

            return this;
        }
        public PersonFormActionOutcomePage TapSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);
            WaitForElementNotVisible("CWRefreshPanel", 35);
            
            return this;
        }
        public PersonFormActionOutcomePage TapSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }
        public PersonFormActionOutcomePage TapAdditionalToolbarElementsbutton()
        {
            Click(additionalToolbarElementsButton);

            return this;
        }
        public PersonFormActionOutcomePage TapDeleteButton()
        {
            Click(deleteButton);

            return this;
        }

    }
}
