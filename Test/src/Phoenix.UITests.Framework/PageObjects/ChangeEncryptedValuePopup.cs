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
    public class ChangeEncryptedValuePopup : CommonMethods
    {
        public ChangeEncryptedValuePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By iframe_ChangePassword = By.Id("iframe_ChangePassword");



        readonly By popupHeader = By.XPath("//*[@id='CWHeader']/h1");

        readonly By newPassword_Label = By.XPath("//label[@for='CWPassword']");
        readonly By confirmNewPassword_Label = By.XPath("//label[@for='CWConfirmPassword']");

        readonly By newPassword_Field = By.Id("CWPassword");
        readonly By confirmNewPassword_Field = By.Id("CWConfirmPassword");

        readonly By saveButton = By.Id("CWSaveButton");
        readonly By closeButton = By.Id("CWCloseButton");




        public ChangeEncryptedValuePopup WaitForChangeEncryptedValuePopupToLoad()
        {
            WaitForElement(iframe_ChangePassword);
            SwitchToIframe(iframe_ChangePassword);

            WaitForElement(newPassword_Label);
            WaitForElement(confirmNewPassword_Label);
            
            WaitForElement(newPassword_Field);
            WaitForElement(confirmNewPassword_Field);

            WaitForElement(saveButton);
            WaitForElement(closeButton);

            return this;
        }

        
        public ChangeEncryptedValuePopup InsertNewEncryptedValue(string TextToInsert)
        {
            SendKeys(newPassword_Field, TextToInsert);

            return this;
        }

        public ChangeEncryptedValuePopup InsertConfirmNewEncryptedValue(string TextToInsert)
        {
            SendKeys(confirmNewPassword_Field, TextToInsert);

            return this;
        }


        public ChangeEncryptedValuePopup TapSaveButton()
        {
            Click(saveButton);

            return this;
        }

        public ChangeEncryptedValuePopup TapCloseButton()
        {
            Click(closeButton);

            return this;
        }


    }
}
