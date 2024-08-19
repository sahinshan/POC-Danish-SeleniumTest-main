using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class PdfPopupPage : CommonMethods
    {
        public PdfPopupPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

       
        readonly By pdfElement = By.XPath("/html/body/embed[@type='application/pdf']");
        readonly By errorMessageElement = By.XPath("/html/body/pre");



        public PdfPopupPage WaitForPdfPopupPageToLoad()
        {
            SwitchToDefaultFrame();

            //WaitForElement(pdfElement);

            return this;
        }

        public PdfPopupPage WaitForPdfPopupPageToLoadWithErrorMessage(string ExpectedText)
        {
            SwitchToDefaultFrame();

            WaitForElement(errorMessageElement);
            ValidateElementText(errorMessageElement, ExpectedText);

            return this;
        }



    }
    
}
