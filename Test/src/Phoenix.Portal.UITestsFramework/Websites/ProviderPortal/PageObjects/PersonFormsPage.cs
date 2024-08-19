using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;



namespace Phoenix.Portal.UITestsFramework.Websites.ProviderPortal.PageObjects
{
    public class PersonFormsPage : CommonMethods
    {
        public PersonFormsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By PageTitle = By.XPath("//mosaic-page-header/div/div/div/div/h1[text()='Person Forms']");

        readonly By messagesArea = By.XPath("//*[@id='CWMessages']");
        readonly By ViewOptionSelected = By.XPath("//select[@id='viewInput-field']/following-sibling::div");
        readonly By SearchField = By.XPath("//input[@id='searchBy-input']");
        readonly By ResetButton = By.XPath("//mosaic-button/button[@aria-label='Reset']");
        readonly By ExportToExcelButton = By.XPath("//mosaic-button/button[@aria-label='Export']");
        



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


        public PersonFormsPage WaitForPersonFormsPageToLoad()
        {
            this.WaitForBrowserWindowTitle("Person Forms");

            WaitForElement(PageTitle, 15);

          
            return this;
        }


        #region Grid

        public PersonFormsPage validateViewDropDown(String ExpectedText)
        {

            WaitForElementVisible(ViewOptionSelected);
            ValidateElementText(ViewOptionSelected, ExpectedText);

            return this;

        }

        public PersonFormsPage validateSearchText()
        {

            WaitForElementVisible(SearchField);
           

            return this;

        }

        public PersonFormsPage ValidateRefreshButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResetButton);
            else
                WaitForElementNotVisible(ResetButton, 7);

            return this;
        }

        public PersonFormsPage ValidateExportToExcelButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ExportToExcelButton);
            else
                WaitForElementNotVisible(ExportToExcelButton, 7);

            return this;
        }
        #endregion

    }

}
