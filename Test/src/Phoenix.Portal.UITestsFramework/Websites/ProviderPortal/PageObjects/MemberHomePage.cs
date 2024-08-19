using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.ProviderPortal.PageObjects
{
    public class MemberHomePage : CommonMethods
    {
        public MemberHomePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By PageTitle = By.XPath("//mosaic-page-header/div/div/div/div/h1[text()='Welcome to the Provider Portal']");

        readonly By messagesArea = By.XPath("//*[@id='CWMessages']");

        



        #region Messages

        By messageTopElement(int messagePosition) => By.XPath("//*[@id='CWMessages']/mosaic-container/mosaic-card[" + messagePosition + "]");
        By messageMosaicCardBody(int messagePosition) => By.XPath("//*[@id='CWMessages']/mosaic-container/mosaic-card[" + messagePosition + "]/mosaic-card-body");
        By messageFromInformation(int messagePosition) => By.XPath("//*[@id='CWMessages']/mosaic-container/mosaic-card[" + messagePosition + "]/mosaic-card-body/div/h2");
        By messageSentDate(int messagePosition) => By.XPath("//*[@id='CWMessages']/mosaic-container/mosaic-card[" + messagePosition + "]/mosaic-card-body/div/span/small");
        By messageText(int messagePosition) => By.XPath("//*[@id='CWMessages']/mosaic-container/mosaic-card[" + messagePosition + "]/mosaic-card-body/div/p");

        readonly By messageTextBox = By.XPath("//*[@id='CWField_message-input']");
        readonly By messagelabel = By.XPath("//*[@id='CWField_message']/div[text()='Message']");
        readonly By messageSubmitButton = By.XPath("//*[@id='CWSubmitMessageButton']");
        

        #endregion


        public MemberHomePage WaitForMemberHomePageToLoad()
        {
            this.WaitForBrowserWindowTitle("Home");

            WaitForElement(PageTitle, 15);

            WaitForElementVisible(messagesArea);
            WaitForElementVisible(messageTextBox);
            WaitForElementVisible(messagelabel);
            WaitForElementVisible(messageSubmitButton);

            return this;
        }


        #region Messages

        public MemberHomePage ValidateMessageVisibility(int MessagePosition, bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(messageTopElement(MessagePosition));
            else
                WaitForElementNotVisible(messageTopElement(MessagePosition), 3);

            return this;
        }

        public MemberHomePage ValidateMessageBackgroundColor(int MessagePosition, string ExpectedColor)
        {
            ValidateElementAttributeValue(messageTopElement(MessagePosition), "color", ExpectedColor);

            return this;
        }

        public MemberHomePage ValidateMessageReadFlag(int MessagePosition, bool ExpectedRead)
        {
            if(ExpectedRead)
                ValidateElementAttributeValue(messageMosaicCardBody(MessagePosition), "class", "mc-card__body");
            else
                ValidateElementAttributeValue(messageMosaicCardBody(MessagePosition), "class", "notread mc-card__body");

            return this;
        }

        public MemberHomePage ValidateMessageFromText(int MessagePosition, string ExpectedText)
        {
            ValidateElementText(messageFromInformation(MessagePosition), ExpectedText);

            return this;
        }

        public MemberHomePage ValidateMessageSentDate(int MessagePosition, string ExpectedText)
        {
            ValidateElementText(messageSentDate(MessagePosition), ExpectedText);

            return this;
        }

        public MemberHomePage ValidateMessageText(int MessagePosition, string ExpectedText)
        {
            ValidateElementText(messageText(MessagePosition), ExpectedText);

            return this;
        }

        public MemberHomePage InsertMessageText(string TextToInsert)
        {
            SendKeys(messageTextBox, TextToInsert);

            return this;
        }

        public MemberHomePage ClickSubmitButton()
        {
            Click(messageSubmitButton);

            System.Threading.Thread.Sleep(500);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        #endregion
    }

}
