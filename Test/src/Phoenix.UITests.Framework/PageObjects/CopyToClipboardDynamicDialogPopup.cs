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
    public class CopyToClipboardDynamicDialogPopup : CommonMethods
    {
        public CopyToClipboardDynamicDialogPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupHeader = By.XPath("//*[@id='CWCopyToClipboardDynamicDialog']/div/section/div/h2");

        readonly By textArea = By.XPath("//*[@id='CWCopyToClipboardTextArea']");

        readonly By copyButton = By.Id("CWOKButton");

        


        public CopyToClipboardDynamicDialogPopup WaitForCopyToClipboardDynamicDialogPopupToLoad()
        {
            WaitForElement(popupHeader);
            WaitForElement(textArea);
            WaitForElement(copyButton);

            return this;
        }

        public CopyToClipboardDynamicDialogPopup ValidateCopyToClipboardDynamicDialogPopupNotDisplayed()
        {
            WaitForElementNotVisible(popupHeader, 3);
            WaitForElementNotVisible(textArea, 3);
            WaitForElementNotVisible(copyButton, 3);

            return this;
        }

        public CopyToClipboardDynamicDialogPopup TapCopyButton()
        {
            Click(copyButton);

            return this;
        }

        public CopyToClipboardDynamicDialogPopup ValidateTextAreaLink(string ExpectedLink)
        {
            ValidateElementText(textArea, ExpectedLink);

            return this;
        }

    }
}
