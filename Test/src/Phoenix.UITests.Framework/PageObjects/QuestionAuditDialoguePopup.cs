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
    /// This class represents the Section information popup.
    /// this popup is displayed when a user open an assessment in edit mode, taps on a section (or sub section) menu button and tap on the "Section Information" link
    /// </summary>
    public class QuestionAuditDialoguePopup : CommonMethods
    {
        public QuestionAuditDialoguePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By iframe_CWAuditDialogWindow = By.Id("iframe_CWAuditDialogWindow");

        readonly By popupTitle = By.XPath("//*[@id='CWHeader']/h1");

        By auditRowEditedBy(string UserFullName, string DateTimeOfEdit) => By.XPath("//*[@id='CWAssessmentAuditContent']/div/div/div/div/div/p[text()='Edited by " + UserFullName + " on " + DateTimeOfEdit + "']");
        By auditRowNewValue(string NewValue) => By.XPath("//*[@id='CWAssessmentAuditContent']/div/div/div/div/div/p[text()='New Value: " + NewValue + "']");
        By auditRowPreviousValue(string PreviousValue) => By.XPath("//*[@id='CWAssessmentAuditContent']/div/div/div/div/div/p[text()='Previous Value: " + PreviousValue + "']");

        By notificationMessage(string ExpectedMessage) => By.XPath("//*[@id='CWNotificationMessage_Audit'][text()='" + ExpectedMessage + "']");



        #region Labels

        readonly By userLabel= By.XPath("//*[@id='CWAssessmentAuditUserNameLabel'][text()='User']");
        readonly By dateFromLabel= By.XPath("//*[@id='CWAssessmentAuditDateFromLabel'][text()='Date From']");
        readonly By dateToLabel= By.XPath("//*[@id='CWAssessmentAuditDateToLabel'][text()='Date To']");


        #endregion

        
        #region Fields

        readonly By userField = By.XPath("//*[@id='CWAssessmentAuditUserName']");
        readonly By dateFromField = By.XPath("//*[@id='CWAssessmentAuditDateFrom']");
        readonly By dateToField = By.XPath("//*[@id='CWAssessmentAuditDateTo']");


        #endregion


        #region Buttons

        readonly By SearchButton = By.XPath("//*[@id='CWSearchButton']");
        readonly By ClearButton = By.XPath("//*[@id='CWClearButton']");
        readonly By CloseButton = By.XPath("//*[@id='CWAuditCloseButton']");
        

        #endregion




        public QuestionAuditDialoguePopup WaitForQuestionAuditDialoguePopupToLoad(string ExpectedPopupTitle)
        {
            WaitForElement(iframe_CWAuditDialogWindow);
            SwitchToIframe(iframe_CWAuditDialogWindow);

            WaitForElement(popupTitle);
            ValidateElementText(popupTitle, ExpectedPopupTitle);

            WaitForElement(CloseButton);
            

            return this;
        }

        public QuestionAuditDialoguePopup WaitForAuditRecordVisible(string UserFullName, string DateTimeOfEdit, string NewValue, string PreviousValue)
        {
            WaitForElementVisible(auditRowEditedBy(UserFullName, DateTimeOfEdit));
            WaitForElementVisible(auditRowNewValue(NewValue));
            WaitForElementVisible(auditRowPreviousValue(PreviousValue));
            
            return this;
        }

        public QuestionAuditDialoguePopup WaitForAuditRecordNotVisible(string UserFullName, string DateTimeOfEdit, string NewValue, string PreviousValue)
        {
            WaitForElementNotVisible(auditRowEditedBy(UserFullName, DateTimeOfEdit), 3);
            WaitForElementNotVisible(auditRowNewValue(NewValue), 3);
            WaitForElementNotVisible(auditRowPreviousValue(PreviousValue), 3);

            return this;
        }

        public QuestionAuditDialoguePopup WaitForNotificationMessageVisible(string ExpectedMessage)
        {
            WaitForElementVisible(notificationMessage(ExpectedMessage));

            return this;
        }

        public QuestionAuditDialoguePopup WaitForNotificationMessageNotVisible(string ExpectedMessage)
        {
            WaitForElementNotVisible(notificationMessage(ExpectedMessage), 3);

            return this;
        }



        public QuestionAuditDialoguePopup InsertUser(string ValueToInsert)
        {
            WaitForElementToBeClickable(userField);
            SendKeys(userField, ValueToInsert);

            return this;
        }

        public QuestionAuditDialoguePopup InsertDateFrom(string ValueToInsert)
        {
            WaitForElementToBeClickable(dateFromField);
            SendKeysWithoutClearing(dateFromField, ValueToInsert);

            return this;
        }

        public QuestionAuditDialoguePopup InsertDateTo(string ValueToInsert)
        {
            WaitForElementToBeClickable(dateToField);
            SendKeysWithoutClearing(dateToField, ValueToInsert);

            return this;
        }



        public QuestionAuditDialoguePopup TapSearchButton()
        {
            Click(SearchButton);



            return this;
        }

        public QuestionAuditDialoguePopup TapClearButton()
        {
            Click(ClearButton);

            return this;
        }

        public QuestionAuditDialoguePopup TapCloseButton()
        {
            Click(CloseButton);

            return this;
        }
        


    }
}
