using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class FeedbackPage : CommonMethods
    {
        public FeedbackPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By NameFieldLabel = By.XPath("//*[@id='CWField_name']/label[text()='Name']");
        readonly By EmailFieldLabel = By.XPath("//*[@id='CWField_email']/label[text()='Email']");
        readonly By WebsiteFeedbackTypeFieldLabel = By.XPath("//*[@id='CWField_websitefeedbacktypeid']/label[text()='Website Feedback Type']");
        readonly By MessageFieldLabel = By.XPath("//*[@id='CWField_message']/label[text()='Message']");


        readonly By NameField = By.XPath("//*[@id='CWField_name-input']");
        readonly By EmailField = By.XPath("//*[@id='CWField_email-input']");
        readonly By WebsiteFeedbackTypeField = By.XPath("//*[@id='CWField_websitefeedbacktypeid-field']");
        readonly By MessageField = By.XPath("//*[@id='CWField_message-input']");

        readonly By NameFieldError = By.XPath("//*[@id='CWField_name']/div[2]/span");
        readonly By EmailFieldError = By.XPath("//*[@id='CWField_email']/div[3]/span");
        readonly By WebsiteFeedbackTypeFieldError = By.XPath("//*[@id='CWField_websitefeedbacktypeid']/div[3]/span");
        readonly By MessageFieldError = By.XPath("//*[@id='CWField_message']/div[2]/span");

        readonly By SubmitButton = By.XPath("//*[@id='CWDataFormSubmitButton']");


        readonly By ThankYou_ToastMessage = By.XPath("//*[@class='toastify__message']");
        readonly By ThankYou_ToastMessageCloseButton = By.XPath("//*[@class='toast-close']");

        readonly By SaveMessage = By.XPath("//*[@id='CWSaveMessageHolder']/div[2]");




        public FeedbackPage WaitForFeedbackPageToLoad()
        {
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
        public FeedbackPage WaitForFeedbackPageElementsNotVisible()
        {
            WaitForElementNotVisible(NameFieldLabel, 5);
            WaitForElementNotVisible(EmailFieldLabel, 5);
            WaitForElementNotVisible(WebsiteFeedbackTypeFieldLabel, 5);
            WaitForElementNotVisible(MessageFieldLabel, 5);

            WaitForElementNotVisible(NameField, 5);
            WaitForElementNotVisible(EmailField, 5);
            WaitForElementNotVisible(WebsiteFeedbackTypeField, 5);
            WaitForElementNotVisible(MessageField, 5);

            WaitForElementNotVisible(SubmitButton, 5);

            return this;
        }


        public FeedbackPage ValidateNameFieldErrorVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(NameFieldError);
            else
                WaitForElementNotVisible(NameFieldError, 7);

            return this;
        }
        public FeedbackPage ValidateEmailFieldErrorVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EmailFieldError);
            else
                WaitForElementNotVisible(EmailFieldError, 7);

            return this;
        }
        public FeedbackPage ValidateWebsiteFeedbackTypeFieldErrorVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(WebsiteFeedbackTypeFieldError);
            else
                WaitForElementNotVisible(WebsiteFeedbackTypeFieldError, 7);

            return this;
        }
        public FeedbackPage ValidateMessageFieldErrorVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MessageFieldError);
            else
                WaitForElementNotVisible(MessageFieldError, 7);

            return this;
        }
        public FeedbackPage ValidateThankYouToastMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible) 
            { 
                WaitForElementVisible(ThankYou_ToastMessage);
                WaitForElementVisible(ThankYou_ToastMessageCloseButton);
            }
            else
            { 
                WaitForElementNotVisible(ThankYou_ToastMessage, 7);
                WaitForElementNotVisible(ThankYou_ToastMessageCloseButton, 7);
            }

            return this;
        }
        public FeedbackPage ValidateSaveMessageVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(SaveMessage);
            }
            else
            {
                WaitForElementNotVisible(SaveMessage, 7);
            }

            return this;
        }



        public FeedbackPage ValidateNameFieldErrorText(string ExpectedText)
        {
            ValidateElementText(NameFieldError, ExpectedText);

            return this;
        }
        public FeedbackPage ValidateEmailFieldErrorText(string ExpectedText)
        {
            ValidateElementText(EmailFieldError, ExpectedText);
            return this;
        }
        public FeedbackPage ValidateWebsiteFeedbackTypeFieldErrorText(string ExpectedText)
        {
            ValidateElementText(WebsiteFeedbackTypeFieldError, ExpectedText);

            return this;
        }
        public FeedbackPage ValidateMessageFieldErrorText(string ExpectedText)
        {
            ValidateElementText(MessageFieldError, ExpectedText);

            return this;
        }
        public FeedbackPage ValidateThankYouToastMessageText(string ExpectedText)
        {
            ValidateElementText(ThankYou_ToastMessage, ExpectedText);

            return this;
        }
        public FeedbackPage ValidateSaveMessageText(string ExpectedText)
        {
            ValidateElementText(SaveMessage, ExpectedText);

            return this;
        }


        public FeedbackPage InsertName(string TextToInsert)
        {
            SendKeys(NameField, TextToInsert);

            return this;
        }
        public FeedbackPage InsertEmail(string TextToInsert)
        {
            SendKeys(EmailField, TextToInsert);

            return this;
        }
        public FeedbackPage InsertMessage(string TextToInsert)
        {
            SendKeys(MessageField, TextToInsert);

            return this;
        }
        public FeedbackPage SelectWebsiteFeedbackType(string TextToSelect)
        {
            SetElementDisplayStyleToInline("CWField_websitefeedbacktypeid-field", 1);

            SelectPicklistElementByText(WebsiteFeedbackTypeField, TextToSelect);

            return this;
        }



        public FeedbackPage ClickSubmitButton()
        {
            Click(SubmitButton);

            return this;
        }
        public FeedbackPage ClickToastMessageCloseButton()
        {
            Click(ThankYou_ToastMessageCloseButton);

            return this;
        }

    }

}
