using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class FAQRecordPage : CommonMethods
    {
        public FAQRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        
        readonly By pageHeader = By.XPath("//mosaic-page-header/div/div/div/div/h1[text()='Frequently Asked Questions']");
        
        readonly By questionTitle = By.XPath("//*[@id='CWMainRow1']/mosaic-col/mosaic-card/mosaic-card-header/div/h2");
        By questionContents(int line) => By.XPath("//*[@id='CWMainRow1']/mosaic-col/mosaic-card/mosaic-card-body/div/p[" + line + "]");

        readonly By wasThisHelpfullMessage = By.XPath("//*[@id='votingcontainer']/span[text()='Was this helpful?']");

        readonly By thankYouForYourFeedbackMessage = By.XPath("//*[@id='votedcontainer']/mosaic-alert/div/p[text()='Thank you for your feedback.']");


        readonly By helpfulButton = By.XPath("//*[@id='CWUpVoteButton']");
        readonly By notHelpfulButton = By.XPath("//*[@id='CWDownVoteButton']");

        



        public FAQRecordPage WaitForFAQRecordPageToLoad()
        {
            WaitForElement(pageHeader);
            WaitForElement(questionTitle);

            WaitForElement(wasThisHelpfullMessage);
            WaitForElement(helpfulButton);
            WaitForElement(notHelpfulButton);

            return this;
        }

        public FAQRecordPage ValidateQuestionTitleText(string ExpectedText)
        {
            WaitForElementVisible(questionTitle);

            ValidateElementText(questionTitle, ExpectedText);

            return this;
        }

        public FAQRecordPage ValidateQuestionContentTitleText(int Line, string ExpectedText)
        {
            ValidateElementText(questionContents(Line), ExpectedText);

            return this;
        }

        public FAQRecordPage WaitForThankYouForYourFeedbackMessageVisible()
        {
            WaitForElementVisible(thankYouForYourFeedbackMessage);

            return this;
        }

        public FAQRecordPage ClickHelpfulButton()
        {
            Click(helpfulButton);

            return this;
        }

        public FAQRecordPage ClickNotHelpfulButton()
        {
            Click(notHelpfulButton);

            return this;
        }


    }
    
}
