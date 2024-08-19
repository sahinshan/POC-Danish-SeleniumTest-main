using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteUserPasswordHistoryRecordPage : CommonMethods
    {

        public WebSiteUserPasswordHistoryRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websiteuserpasswordhistory&')]");
        
        
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By WebsiteUser_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteuserid']/label[text()='Website User']");
        readonly By Password_FieldName = By.XPath("//*[@id='CWLabelHolder_password']/label[text()='Password']");


        readonly By WebsiteUser_FieldLink = By.XPath("//*[@id='CWField_websiteuserid_Link']");
        readonly By Password_DateField = By.XPath("//*[@id='CWField_password']");

        


        public WebSiteUserPasswordHistoryRecordPage WaitForWebSiteUserPasswordHistoryRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(WebsiteUser_FieldName);
            this.WaitForElement(Password_FieldName);
            
            return this;
        }



        public WebSiteUserPasswordHistoryRecordPage ValidateWebSiteUserFieldLinkText(string ExpectedText)
        {
            ValidateElementText(WebsiteUser_FieldLink, ExpectedText);

            return this;
        }
        public WebSiteUserPasswordHistoryRecordPage ValidatePasswordValueFieldText(string ExpectedText)
        {
            ValidateElementValue(Password_DateField, ExpectedText);

            return this;
        }
       
        

        public WebSiteUserPasswordHistoryRecordPage ClickOnDeleteButton()
        {
            Click(deleteButton);

            return this;
        }
    }
}
