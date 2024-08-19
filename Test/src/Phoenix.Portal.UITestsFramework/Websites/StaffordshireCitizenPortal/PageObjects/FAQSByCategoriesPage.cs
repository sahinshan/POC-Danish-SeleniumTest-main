using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class FAQSByCategoriesPage : CommonMethods
    {
        public FAQSByCategoriesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        
        readonly By pageHeader = By.XPath("//mosaic-page-header/div/div/div/div/h1[text()='Frequently Asked Questions']");
        
        By generalInformationTitle(string FaqCategoryName) => By.XPath("//mosaic-card/mosaic-card-header/div/div[text()='" + FaqCategoryName + "']");

        By faq(string titleName) => By.XPath("//mosaic-list-group/ul/li/mosaic-link/a[text()='" + titleName + "']");




        public FAQSByCategoriesPage WaitForFAQSByCategoriesPageToLoad(string FaqCategoryName)
        {
            WaitForElement(pageHeader);
            
            WaitForElement(generalInformationTitle(FaqCategoryName));

            return this;
        }

        public FAQSByCategoriesPage ValidateFaqVisible(string FaqTitle)
        {
            WaitForElement(faq(FaqTitle));

            return this;
        }

        public FAQSByCategoriesPage ValidateFaqNotVisible(string FaqTitle)
        {
            WaitForElementNotVisible(faq(FaqTitle), 7);

            return this;
        }


        public FAQSByCategoriesPage ClickFAQ(string FaqTitle)
        {
            Click(faq(FaqTitle));

            return this;
        }



    }
    
}
