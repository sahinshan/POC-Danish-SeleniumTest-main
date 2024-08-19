using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteUserPasswordResetRecordPage : CommonMethods
    {

        public WebSiteUserPasswordResetRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websiteuserpasswordreset&')]");
        
        
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By WebsiteUser_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteuserid']/label[text()='Website User']");
        readonly By ExpireOn_FieldName = By.XPath("//*[@id='CWLabelHolder_expireon']/label[text()='Expire On']");
        readonly By ResetPasswordLink_FieldName = By.XPath("//*[@id='CWLabelHolder_resetpasswordlink']/label[text()='Reset Password Link']");


        readonly By WebsiteUser_FieldLink = By.XPath("//*[@id='CWField_websiteuserid_Link']");
        readonly By ExpireOn_DateField = By.XPath("//*[@id='CWField_expireon']");
        readonly By ExpireOn_TimeField = By.XPath("//*[@id='CWField_expireon_Time']");
        readonly By ResetPasswordLink_Field = By.XPath("//*[@id='CWField_resetpasswordlink']");
        


        public WebSiteUserPasswordResetRecordPage WaitForWebSiteUserPasswordResetRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(WebsiteUser_FieldName);
            this.WaitForElement(ExpireOn_FieldName);
            this.WaitForElement(ResetPasswordLink_FieldName);
            
            return this;
        }



        public WebSiteUserPasswordResetRecordPage ValidateWebSiteUserFieldLinkText(string ExpectedText)
        {
            ValidateElementText(WebsiteUser_FieldLink, ExpectedText);

            return this;
        }
        public WebSiteUserPasswordResetRecordPage ValidateExpiresOnValueFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(ExpireOn_DateField, ExpectedDate);
            ValidateElementValue(ExpireOn_TimeField, ExpectedTime);

            return this;
        
        }
        public WebSiteUserPasswordResetRecordPage ValidateResetPasswordLinkFieldText(string ExpectedText)
        {
            ValidateElementValue(ResetPasswordLink_Field, ExpectedText);

            return this;
        }
        

        public WebSiteUserPasswordResetRecordPage ClickOnDeleteButton()
        {
            Click(deleteButton);

            return this;
        }
    }
}
