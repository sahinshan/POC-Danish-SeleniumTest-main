using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the dynamic dialog that is displayed with messages to the user.
    /// </summary>
    public class CloneWebsitePopup : CommonMethods
    {
        public CloneWebsitePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By iframe_ChangePassword = By.Id("iframe_CWCloneDialogWindow");



        readonly By popupHeader = By.XPath("//*[@id='CWHeader']/h1");

        readonly By websiteName_Label = By.XPath("//*[@id='CloneNameLabel']");

        readonly By websiteName_Field = By.Id("CloneName");
        readonly By websiteName_ErrorLabel = By.XPath("//*[@id='ErrorLabel']");
        
        By progressBar(string ExpectedPercentage) => By.XPath("//*[@id='progressbar']/div[text()='" + ExpectedPercentage + "']");
        By notifyArea(string ExpectedText) => By.XPath("//*[@id='Notify'][text()='" + ExpectedText + "']");



        readonly By cloneButton = By.Id("CloneButton");
        readonly By closeButton = By.XPath("//*[@id='CWForm']//button[text()='Close']");




        public CloneWebsitePopup WaitForCloneWebsitePopupToLoad()
        {
            WaitForElement(iframe_ChangePassword);
            SwitchToIframe(iframe_ChangePassword);

            WaitForElement(popupHeader);

            WaitForElement(websiteName_Label);
            WaitForElement(websiteName_Field);

            WaitForElement(cloneButton);
            WaitForElement(closeButton);

            return this;
        }

        public CloneWebsitePopup InsertWebsiteName(string TextToInsert)
        {
            SendKeys(websiteName_Field, TextToInsert);

            return this;
        }

        public CloneWebsitePopup ValidateWebsiteNameErrorLabelVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(websiteName_ErrorLabel);
            else
                WaitForElementNotVisible(websiteName_ErrorLabel, 5);

            return this;
        }

        public CloneWebsitePopup ValidateWebsiteNameErrorLabelText(string ExpectedText)
        {
            ValidateElementText(websiteName_ErrorLabel, ExpectedText);

            return this;
        }

        public CloneWebsitePopup WaitForPercentageBar(string ExpectedPercentage)
        {
            WaitForElementVisible(progressBar(ExpectedPercentage));
            
            return this;
        }
        public CloneWebsitePopup WaitForNotifyArea(string ExpectedText)
        {
            WaitForElementVisible(notifyArea(ExpectedText));

            return this;
        }

        public CloneWebsitePopup ClickCloneButton()
        {
            Click(cloneButton);

            return this;
        }

        public CloneWebsitePopup ClickCloseButton()
        {
            Click(closeButton);

            return this;
        }

    }
}
