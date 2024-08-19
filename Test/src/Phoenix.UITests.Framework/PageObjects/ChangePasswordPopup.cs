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
    public class ChangePasswordPopup : CommonMethods
    {
        public ChangePasswordPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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

        readonly By newPassword_ErrorLabel = By.XPath("//*[@id='CWForm']/div[4]/div[1]/label[2]/span");
        readonly By confirmNewPassword_ErrorLabel = By.XPath("//*[@id='CWForm']/div[4]/div[2]/label[2]/span");

        readonly By saveButton = By.Id("CWSaveButton");
        readonly By closeButton = By.Id("CWCloseButton");




        public ChangePasswordPopup WaitForChangePasswordPopupToLoad()
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

        
        public ChangePasswordPopup InsertNewPassword(string TextToInsert)
        {
            WaitForElement(newPassword_Field);
            SendKeys(newPassword_Field, TextToInsert);
     

            return this;
        }

     
        public ChangePasswordPopup InsertConfirmNewPassword(string TextToInsert)
        {
            SendKeys(confirmNewPassword_Field, TextToInsert);

            return this;
        }


        public ChangePasswordPopup TapSaveButton()
        {
            Click(saveButton);

            return this;
        }

        public ChangePasswordPopup TapCloseButton()
        {
            Click(closeButton);

            return this;
        }

        public ChangePasswordPopup ValidateNewPasswordField(bool Expected)
        {
            if (Expected)
            {
                WaitForElement(newPassword_Field);
                ValidateElementEnabled(newPassword_Field);
            }
            else
            {
                WaitForElementNotVisible(newPassword_Field, 7);
            }


            return this;
        }

        public ChangePasswordPopup ValidateConfirmNewPasswordField(bool Expected)
        {
            if (Expected)
            {
                WaitForElement(confirmNewPassword_Field);
                ValidateElementEnabled(confirmNewPassword_Field);
            }
            else
            {
                WaitForElementNotVisible(confirmNewPassword_Field, 7);
            }


            return this;
        }


        public ChangePasswordPopup ValidateNewPasswordField_ErrorLabel(string ExpectedText)
        {

            WaitForElementVisible(newPassword_ErrorLabel);
            ValidateElementText(newPassword_ErrorLabel, ExpectedText);

            return this;
        }

        public ChangePasswordPopup ValidateConfirmNewPasswordField_ErrorLabel(string ExpectedText)
        {

            WaitForElementVisible(confirmNewPassword_ErrorLabel);
            ValidateElementText(confirmNewPassword_ErrorLabel, ExpectedText);

            return this;
        }

    }
}
