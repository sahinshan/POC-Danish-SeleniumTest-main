using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class FAQSPage : CommonMethods
    {
        public FAQSPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        
        readonly By pageHeader = By.XPath("//mosaic-page-header/div/div/div/div/h1[text()='Frequently Asked Questions']");
        
        readonly By TopFrequentlyAskedQuestionsTitle = By.XPath("//mosaic-card/mosaic-card-header/div/div[text()='Top Frequently Asked Questions']");
        readonly By FrequentlyAskedQuestionCategoriesTitle = By.XPath("//mosaic-card/mosaic-card-header/div/div[text()='Frequently Asked Question Categories']");

        By faq(string titleName) => By.XPath("//mosaic-list-group/ul/li/mosaic-link/a[text()='" + titleName + "']");
        By faqCategory(string categoryName) => By.XPath("//mosaic-card/mosaic-card-body/div/mosaic-list-group/ul/li/mosaic-link/a[text()='" + categoryName + "']");




        public FAQSPage WaitForFAQSPageLoad()
        {
            WaitForElement(pageHeader);
            WaitForElement(TopFrequentlyAskedQuestionsTitle);
            WaitForElement(FrequentlyAskedQuestionCategoriesTitle);

            return this;
        }

        public FAQSPage ValidateFaqVisible(string FaqTitle)
        {
            WaitForElement(faq(FaqTitle));

            return this;
        }

        public FAQSPage ValidateFaqNotVisible(string FaqTitle)
        {
            WaitForElementNotVisible(faq(FaqTitle), 7);

            return this;
        }

        public FAQSPage ValidateFaqCategoryVisible(string FaqCategory)
        {
            WaitForElement(faqCategory(FaqCategory));

            return this;
        }

        public FAQSPage ClickFAQ(string FaqTitle)
        {
            WaitForElementToBeClickable(faq(FaqTitle));
            Click(faq(FaqTitle));

            return this;
        }

        public FAQSPage ClickFAQCategory(string FaqCategory)
        {
            Click(faqCategory(FaqCategory));

            return this;
        }


    }
    
}
