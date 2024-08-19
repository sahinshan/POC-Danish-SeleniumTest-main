using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;


namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class FeedbackDialog : CommonMethods
    {
        public FeedbackDialog(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By dialogTitle = By.XPath("//*[@id='dialog-title'][text()='New Feedback']");

        readonly By dialogIframe = By.Id("dialog-iframe");

        

        readonly By NameFieldLabel = By.XPath("//*[@id='CWField_name']/label[text()='Name']");
        readonly By EmailFieldLabel = By.XPath("//*[@id='CWField_email']/label[text()='Email']");
        readonly By WebsiteFeedbackTypeFieldLabel = By.XPath("//*[@id='CWField_websitefeedbacktypeid']/label[text()='Feedback Type']");
        readonly By MessageFieldLabel = By.XPath("//*[@id='CWField_message']/label[text()='Feedback']");


        readonly By NameField = By.XPath("//*[@id='CWField_name-input']");
        readonly By EmailField = By.XPath("//*[@id='CWField_email-input']");
        readonly By WebsiteFeedbackTypeField = By.XPath("//*[@id='CWField_websitefeedbacktypeid-field']");
        readonly By MessageField = By.XPath("//*[@id='CWField_message-input']");

        readonly By NameFieldError = By.XPath("//*[@id='CWField_name']/div[2]/span");
        readonly By EmailFieldError = By.XPath("//*[@id='CWField_email']/div[3]/span");
        readonly By WebsiteFeedbackTypeFieldError = By.XPath("//*[@id='CWField_websitefeedbacktypeid']/div[3]/span");
        readonly By MessageFieldError = By.XPath("//*[@id='CWField_message']/div[2]/span");

        readonly By SubmitButton = By.XPath("//*[@id='CWDataFormSubmitButton']");

        readonly By ToastMessage = By.XPath("//*[@class='toastify__message']");
        readonly By ToastMessageCloseButton = By.XPath("//*[@class='toast-close']");


        public FeedbackDialog WaitForFeedbackDialogToLoad()
        {
            WaitForElement(dialogTitle);

            WaitForElement(dialogIframe);
            SwitchToIframe(dialogIframe);

            WaitForElement(NameFieldLabel);
            WaitForElement(EmailFieldLabel);
            WaitForElement(WebsiteFeedbackTypeFieldLabel);
            WaitForElement(MessageFieldLabel);

            WaitForElement(NameField);
            WaitForElement(EmailField);
            WaitForElement(WebsiteFeedbackTypeField);
            WaitForElement(MessageField);
            
            WaitForElement(SubmitButton);

            return this;
        }

        public FeedbackDialog WaitForFeedbackDialogElementsNotVisible()
        {

            WaitForElementNotVisible(NameFieldLabel, 7);
            WaitForElementNotVisible(EmailFieldLabel, 7);
            WaitForElementNotVisible(WebsiteFeedbackTypeFieldLabel, 7);
            WaitForElementNotVisible(MessageFieldLabel, 7);

            WaitForElementNotVisible(NameField, 7);
            WaitForElementNotVisible(EmailField, 7);
            WaitForElementNotVisible(WebsiteFeedbackTypeField, 7);
            WaitForElementNotVisible(MessageField, 7);

            WaitForElementNotVisible(SubmitButton, 7);

            return this;
        }


        public FeedbackDialog ValidateNameFieldErrorVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NameFieldError);
            else
                WaitForElementNotVisible(NameFieldError, 7);

            return this;
        }
        public FeedbackDialog ValidateEmailFieldErrorVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EmailFieldError);
            else
                WaitForElementNotVisible(EmailFieldError, 7);

            return this;
        }
        public FeedbackDialog ValidateWebsiteFeedbackTypeFieldErrorVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(WebsiteFeedbackTypeFieldError);
            else
                WaitForElementNotVisible(WebsiteFeedbackTypeFieldError, 7);

            return this;
        }
        public FeedbackDialog ValidateMessageFieldErrorVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MessageFieldError);
            else
                WaitForElementNotVisible(MessageFieldError, 7);

            return this;
        }


        public FeedbackDialog ValidateNameFieldErrorText(string ExpectedText)
        {
            ValidateElementText(NameFieldError, ExpectedText);

            return this;
        }
        public FeedbackDialog ValidateEmailFieldErrorText(string ExpectedText)
        {
            ValidateElementText(EmailFieldError, ExpectedText);
            return this;
        }
        public FeedbackDialog ValidateWebsiteFeedbackTypeFieldErrorText(string ExpectedText)
        {
            ValidateElementText(WebsiteFeedbackTypeFieldError, ExpectedText);

            return this;
        }
        public FeedbackDialog ValidateMessageFieldErrorText(string ExpectedText)
        {
            ValidateElementText(MessageFieldError, ExpectedText);

            return this;
        }


        public FeedbackDialog InsertName(string TextToInsert)
        {
            SendKeys(NameField, TextToInsert);

            return this;
        }
        public FeedbackDialog InsertEmail(string TextToInsert)
        {
            SendKeys(EmailField, TextToInsert);

            return this;
        }
        public FeedbackDialog InsertMessage(string TextToInsert)
        {
            SendKeys(MessageField, TextToInsert);

            return this;
        }

        public FeedbackDialog SelectWebsiteFeedbackType(string TextToSelect)
        {
            SetElementDisplayStyleToInline("CWField_websitefeedbacktypeid", 1);

            SelectPicklistElementByText(WebsiteFeedbackTypeField, TextToSelect);

            return this;
        }


        public FeedbackDialog ClickSubmitButton()
        {
            Click(SubmitButton);

            return this;
        }

        public FeedbackDialog ValidateToastMessageText(string ExpectedText)
        {
            ValidateElementText(ToastMessage, ExpectedText);

            return this;
        }

        public FeedbackDialog ValidateToastMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ToastMessage);
                WaitForElementVisible(ToastMessageCloseButton);
            }
            else
            {
                WaitForElementNotVisible(ToastMessage, 7);
                WaitForElementNotVisible(ToastMessageCloseButton, 7);
            }

            return this;
        }

    }

}
