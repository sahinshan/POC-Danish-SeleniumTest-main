using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class ContactUsPage : CommonMethods
    {
        public ContactUsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

       
        readonly By pageHeader = By.XPath("//mosaic-container/mosaic-page-header/div/div/div/div/h1[text()='Contact']");


        readonly By topMessage = By.XPath("//*[@id='CWDataForm']/form/mosaic-alert/div/p[text()='* denotes mandatory field']");

        readonly By ContactFieldTitle = By.XPath("//*[@id='CWField_websitepointofcontactid']/label[text()='Contact']");
        readonly By YourNameFieldTitle = By.XPath("//*[@id='CWField_name']/label[text()='Your name']");
        readonly By YourEmailAddressFieldTitle = By.XPath("//*[@id='CWField_emailaddress']/label[text()='Your email address']");
        readonly By SubjectFieldTitle = By.XPath("//*[@id='CWField_subject']/label[text()='Subject']");
        readonly By MessageFieldTitle = By.XPath("//*[@id='CWField_message']/label[text()='Message']");

        readonly By ContactField = By.XPath("//*[@id='CWField_websitepointofcontactid-field']");
        readonly By YourNameField = By.XPath("//*[@id='CWField_name-input']");
        readonly By YourEmailAddressField = By.XPath("//*[@id='CWField_emailaddress-input']");
        readonly By SubjectField = By.XPath("//*[@id='CWField_subject-input']");
        readonly By MessageField = By.XPath("//*[@id='CWField_message-input']");


        readonly By ContactFieldErrorLabel = By.XPath("//*[@id='CWField_websitepointofcontactid']/div[3]/span");
        readonly By YourNameFieldErrorLabel = By.XPath("//*[@id='CWField_name']/div[2]/span");
        readonly By YourEmailAddressFieldErrorLabel = By.XPath("//*[@id='CWField_emailaddress']/div[3]/span");
        readonly By SubjectFieldErrorLabel = By.XPath("//*[@id='CWField_subject']/div[2]/span");
        readonly By MessageFieldErrorLabel = By.XPath("//*[@id='CWField_message']/div[2]/span");


        readonly By SubmitButton = By.XPath("//*[@id='CWDataFormSubmitButton']");

        readonly By ToastMessage = By.XPath("//*[@class='toastify__message']");
        readonly By ToastMessageCloseButton = By.XPath("//*[@class='toast-close']");

        #region Right hand side panel

        By pointOfContactMainElement(string PointOfContactID) => By.XPath("//ul[@id='" + PointOfContactID + "_MainUl']");
        By pointOfContactNameElement(string PointOfContactID) => By.XPath("//li[@id='" + PointOfContactID + "_Name']/h3");
        By pointOfContactEmailLabelElement(string PointOfContactID) => By.XPath("//li[@id='" + PointOfContactID + "_Email']/strong[text()='Email : ']");
        By pointOfContactEmailLinkElement(string PointOfContactID) => By.XPath("//li[@id='" + PointOfContactID + "_Email']/a");
        By pointOfContactPhoneLabelElement(string PointOfContactID) => By.XPath("//li[@id='" + PointOfContactID + "_Phone']/strong[text()='Telephone : ']");
        By pointOfContactPhoneNumberElement(string PointOfContactID) => By.XPath("//li[@id='" + PointOfContactID + "_Phone']/h8");
        By pointOfContactAddressLabelElement(string PointOfContactID) => By.XPath("//li[@id='" + PointOfContactID + "_Address']/strong[text()='Address : ']");
        By pointOfContactAddressLineElement(string PointOfContactID) => By.XPath("//li[@id='" + PointOfContactID + "_Address']/h8");

        #endregion


        public ContactUsPage WaitForContactUsPageToLoad()
        {
            WaitForElement(pageHeader);

            WaitForElement(topMessage);
            
            WaitForElement(ContactFieldTitle);
            WaitForElement(YourNameFieldTitle);
            WaitForElement(YourEmailAddressFieldTitle);
            WaitForElement(SubjectFieldTitle);
            WaitForElement(MessageFieldTitle);
            
            WaitForElement(ContactField);
            WaitForElement(YourNameField);
            WaitForElement(YourEmailAddressField);
            WaitForElement(SubjectField);
            WaitForElement(MessageField);
            
            WaitForElement(SubmitButton);

            return this;
        }


        public ContactUsPage ValidateYourNameText(string ExpectedText)
        {
            ValidateElementValueByJavascript("CWField_name-input", ExpectedText);

            return this;
        }

        public ContactUsPage ValidateYourEmailAddressText(string ExpectedText)
        {
            ValidateElementValueByJavascript("CWField_emailaddress-input", ExpectedText);

            return this;
        }


        public ContactUsPage SelectContact(string ContactToSelect)
        {
            SetElementDisplayStyleToInline("CWField_websitepointofcontactid", 1);

            SelectPicklistElementByText(ContactField, ContactToSelect);

            return this;
        }


        public ContactUsPage InsertYourName(string TextToInsert)
        {
            SendKeys(YourNameField, TextToInsert);

            return this;
        }
        public ContactUsPage InsertYourEmailAddress(string TextToInsert)
        {
            SendKeys(YourEmailAddressField, TextToInsert);

            return this;
        }
        public ContactUsPage InsertSubject(string TextToInsert)
        {
            SendKeys(SubjectField, TextToInsert);

            return this;
        }
        public ContactUsPage InsertMessage(string TextToInsert)
        {
            SendKeys(MessageField, TextToInsert);

            return this;
        }


        public ContactUsPage ClickSubmitButton()
        {
            Click(SubmitButton);

            return this;
        }


        public ContactUsPage ValidateRightPanelContactPresent(string PointOfContactId)
        {
            WaitForElementVisible(pointOfContactMainElement(PointOfContactId));

            WaitForElementVisible(pointOfContactNameElement(PointOfContactId));

            WaitForElementVisible(pointOfContactEmailLabelElement(PointOfContactId));
            WaitForElementVisible(pointOfContactEmailLinkElement(PointOfContactId));

            WaitForElementVisible(pointOfContactPhoneLabelElement(PointOfContactId));
            WaitForElementVisible(pointOfContactPhoneNumberElement(PointOfContactId));

            WaitForElementVisible(pointOfContactAddressLabelElement(PointOfContactId));
            WaitForElementVisible(pointOfContactAddressLineElement(PointOfContactId));

            return this;
        }
        public ContactUsPage ValidatePointOfContactName(string PointOfContactId, string ExpectedName)
        {
            ValidateElementText(pointOfContactNameElement(PointOfContactId), ExpectedName);
            
            return this;
        }
        public ContactUsPage ValidatePointOfContactEmailLink(string PointOfContactId, string ExpectedName)
        {
            ValidateElementText(pointOfContactEmailLinkElement(PointOfContactId), ExpectedName);

            return this;
        }
        public ContactUsPage ValidatePointOfContactPhoneNumber(string PointOfContactId, string ExpectedName)
        {
            ValidateElementText(pointOfContactPhoneNumberElement(PointOfContactId), ExpectedName);

            return this;
        }
        public ContactUsPage ValidatePointOfContactAddressLine(string PointOfContactId, string ExpectedName)
        {
            ValidateElementText(pointOfContactAddressLineElement(PointOfContactId), ExpectedName);

            return this;
        }


        public ContactUsPage ValidateRightPanelContactNotPresent(string PointOfContactId)
        {
            WaitForElementNotVisible(pointOfContactMainElement(PointOfContactId), 3);

            WaitForElementNotVisible(pointOfContactNameElement(PointOfContactId), 3);

            WaitForElementNotVisible(pointOfContactEmailLabelElement(PointOfContactId), 3);
            WaitForElementNotVisible(pointOfContactEmailLinkElement(PointOfContactId), 3);

            WaitForElementNotVisible(pointOfContactPhoneLabelElement(PointOfContactId), 3);
            WaitForElementNotVisible(pointOfContactPhoneNumberElement(PointOfContactId), 3);

            WaitForElementNotVisible(pointOfContactAddressLabelElement(PointOfContactId), 3);
            WaitForElementNotVisible(pointOfContactAddressLineElement(PointOfContactId), 3);

            return this;
        }


        public ContactUsPage ValidateContactErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ContactFieldErrorLabel);
            else
                WaitForElementNotVisible(ContactFieldErrorLabel, 3);

            return this;
        }
        public ContactUsPage ValidateYourNameErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(YourNameFieldErrorLabel);
            else
                WaitForElementNotVisible(YourNameFieldErrorLabel, 3);

            return this;
        }
        public ContactUsPage ValidateYourEmailAddressErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(YourEmailAddressFieldErrorLabel);
            else
                WaitForElementNotVisible(YourEmailAddressFieldErrorLabel, 3);

            return this;
        }
        public ContactUsPage ValidateSubjectErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(SubjectFieldErrorLabel);
            else
                WaitForElementNotVisible(SubjectFieldErrorLabel, 3);

            return this;
        }
        public ContactUsPage ValidateMessageErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MessageFieldErrorLabel);
            else
                WaitForElementNotVisible(MessageFieldErrorLabel, 3);

            return this;
        }


        public ContactUsPage ValidateContactErrorLabelText(string ExpectetdText)
        {
            ValidateElementText(ContactFieldErrorLabel, ExpectetdText);

            return this;
        }
        public ContactUsPage ValidateYourNameFieldErrorLabelText(string ExpectetdText)
        {
            ValidateElementText(YourNameFieldErrorLabel, ExpectetdText);

            return this;
        }
        public ContactUsPage ValidateYourEmailAddressFieldErrorLabelText(string ExpectetdText)
        {
            ValidateElementText(YourEmailAddressFieldErrorLabel, ExpectetdText);

            return this;
        }
        public ContactUsPage ValidateSubjectFieldErrorLabelText(string ExpectetdText)
        {
            ValidateElementText(SubjectFieldErrorLabel, ExpectetdText);

            return this;
        }
        public ContactUsPage ValidateMessageFieldErrorLabelText(string ExpectetdText)
        {
            ValidateElementText(MessageFieldErrorLabel, ExpectetdText);

            return this;
        }

        public ContactUsPage ValidateToastMessageText(string ExpectedText)
        {
            ValidateElementText(ToastMessage, ExpectedText);

            return this;
        }

        public ContactUsPage ValidateToastMessageVisibility(bool ExpectVisible)
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
