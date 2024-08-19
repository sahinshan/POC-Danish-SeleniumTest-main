using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebsiteUserAccessAudiRecordPage : CommonMethods
    {

        public WebsiteUserAccessAudiRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuseraccessaudit&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");


        readonly By User_FieldName = By.XPath("//*[@id='CWLabelHolder_systemuserid']/label[text()='User']");
        readonly By Application_FieldName = By.XPath("//*[@id='CWLabelHolder_applicationid']/label[text()='Application']");
        readonly By LoginDateTime_FieldName = By.XPath("//*[@id='CWLabelHolder_createdon']/label[text()='Login Date & Time']");
        readonly By LogoutDateTime_FieldName = By.XPath("//*[@id='CWLabelHolder_logoutdatetime']/label[text()='Logout Date & Time']");
        readonly By BrowserType_FieldName = By.XPath("//*[@id='CWLabelHolder_browsertype']/label[text()='Browser Type']");
        readonly By BrowserVersion_FieldName = By.XPath("//*[@id='CWLabelHolder_browserversion']/label[text()='Browser Version']");
        readonly By UserIPAddress_FieldName = By.XPath("//*[@id='CWLabelHolder_useripaddress']/label[text()='User IP Address']");

        readonly By User_FieldLink = By.Id("CWField_systemuserid_Link");
        readonly By Application_Picklist = By.XPath("//*[@id='CWField_applicationid']");
        readonly By LoginDateTime_DateField = By.XPath("//input[@id='CWField_createdon']");
        readonly By LoginDateTime_TimeField = By.XPath("//input[@id='CWField_createdon_Time']");
        readonly By LogoutDateTime_DateField = By.XPath("//input[@id='CWField_logoutdatetime']");
        readonly By LogoutDateTime_TimeField = By.XPath("//input[@id='CWField_logoutdatetime_Time']");
        readonly By BrowserType_Link = By.XPath("//a[@id='CWField_browsertype']");
        readonly By BrowserVersion_Field = By.XPath("CWControlHolder_browserversion");
        readonly By UserIPAddress_Field = By.XPath("CWField_useripaddress");


        public WebsiteUserAccessAudiRecordPage WaitForWebsiteUserAccessAudiRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(User_FieldName);
            this.WaitForElement(Application_FieldName);
            this.WaitForElement(LoginDateTime_FieldName);
            this.WaitForElement(LogoutDateTime_FieldName);
            this.WaitForElement(BrowserType_FieldName);
            this.WaitForElement(BrowserVersion_FieldName);
            this.WaitForElement(UserIPAddress_FieldName);

            return this;
        }



        public WebsiteUserAccessAudiRecordPage ValidateUserFieldLinkText(string ExpectedText)
        {
            ValidateElementText(User_FieldLink, ExpectedText);

            return this;
        }
        public WebsiteUserAccessAudiRecordPage ValidateApplicationFieldText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Application_Picklist, ExpectedText);

            return this;
        }
        public WebsiteUserAccessAudiRecordPage ValidateLoginDateTimeFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementText(LoginDateTime_DateField, ExpectedDate);
            ValidateElementText(LoginDateTime_TimeField, ExpectedTime);

            return this;
        }
        public WebsiteUserAccessAudiRecordPage ValidateLogoutDateTimeFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementText(LogoutDateTime_DateField, ExpectedDate);
            ValidateElementText(LogoutDateTime_TimeField, ExpectedTime);

            return this;
        }
        public WebsiteUserAccessAudiRecordPage ValidateBrowserTypeFieldText( string ExpectedText)
        {
            ValidatePicklistSelectedText(BrowserType_Link, ExpectedText);

            return this;
        }
        public WebsiteUserAccessAudiRecordPage ValidateBrowserVersionFieldLinkText(string ExpectedText)
        {
            ValidateElementText(BrowserVersion_Field, ExpectedText);

            return this;
        }
        public WebsiteUserAccessAudiRecordPage ValidateUserIPAddressValueFieldText(string ExpectedText)
        {
            ValidateElementText(UserIPAddress_Field, ExpectedText);

            return this;
        }
       



    }
}
