using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.StaffordshireCitizenPortal.PageObjects
{
    public class WarningDialog : CommonMethods
    {
        public WarningDialog(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By dialogTitle = By.XPath("//*[@id='CWWarningHtmlDialog']/div/div/div/header/div/h5");
        readonly By dialogMessage = By.XPath("//*[@id='HtmlWarningDialogContent']");
        
        readonly By okButton = By.XPath("//*[@color='warning']/button/div");

        

        public WarningDialog WaitForWarningDialogToLoad()
        {
            WaitForElement(dialogTitle);
            WaitForElement(dialogMessage);
            WaitForElement(okButton);

            return this;
        }

        public WarningDialog ValidateDialogTitleVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(dialogTitle);
            else
                WaitForElementNotVisible(dialogTitle, 7);

            return this;
        }
        public WarningDialog ValidateDialogMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(dialogMessage);
            else
                WaitForElementNotVisible(dialogMessage, 7);

            return this;
        }



        public WarningDialog ValidateDialogTitleText(string ExpectedText)
        {
            ValidateElementText(dialogTitle, ExpectedText);

            return this;
        }
        public WarningDialog ValidateDialogMessageText(string ExpectedText)
        {
            ValidateElementText(dialogMessage, ExpectedText);

            return this;
        }




        public WarningDialog ClickOkButton()
        {
            Click(okButton);

            return this;
        }

    }

}
