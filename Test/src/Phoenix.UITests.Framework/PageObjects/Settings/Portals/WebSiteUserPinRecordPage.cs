using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteUserPinRecordPage : CommonMethods
    {

        public WebSiteUserPinRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websiteuserpin&')]");
        
        
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By deleteButton = By.Id("TI_DeleteRecordButton");


        readonly By PIN_FieldName = By.XPath("//*[@id='CWLabelHolder_pin']/label[text()='PIN']");
        readonly By ExpireOn_FieldName = By.XPath("//*[@id='CWLabelHolder_expireon']/label[text()='Expire On']");
        readonly By WebsiteUser_FieldName = By.XPath("//*[@id='CWLabelHolder_websiteuserid']/label[text()='Website User']");


        readonly By PIN_Field = By.XPath("//*[@id='CWField_pin']");
        readonly By ExpireOn_DateField = By.XPath("//*[@id='CWField_expireon']");
        readonly By ExpireOn_TimeField = By.XPath("//*[@id='CWField_expireon_Time']");
        readonly By WebsiteUser_FieldLink = By.XPath("//*[@id='CWField_websiteuserid_Link']");




        public WebSiteUserPinRecordPage WaitForWebSiteUserPinRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(PIN_FieldName);
            this.WaitForElement(ExpireOn_FieldName);
            this.WaitForElement(WebsiteUser_FieldName);

            return this;
        }

      

        public WebSiteUserPinRecordPage ValidatePINFieldText(string ExpectedText)
        {
            ValidateElementValue(PIN_Field, ExpectedText);

            return this;
        }
        public WebSiteUserPinRecordPage ValidateExpiresOnValueFieldText(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(ExpireOn_DateField, ExpectedDate);
            ValidateElementValue(ExpireOn_TimeField, ExpectedTime);

            return this;
        
        }

        public WebSiteUserPinRecordPage ValidateWebSiteUserFieldLinkText(string ExpectedText)
        {
            ValidateElementText(WebsiteUser_FieldLink, ExpectedText);

            return this;
        }

        public WebSiteUserPinRecordPage ClickOnDeleteButton()
        {
            Click(deleteButton);

            return this;
        }
    }
}
