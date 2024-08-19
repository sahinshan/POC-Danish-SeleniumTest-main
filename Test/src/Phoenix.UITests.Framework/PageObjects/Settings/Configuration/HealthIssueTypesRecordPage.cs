using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class HealthIssueTypesRecordPage : CommonMethods
    {
        public HealthIssueTypesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_healthissuetype = By.Id("iframe_healthissuetype");
        readonly By iframe_healthIssueRecordtype = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=healthissuetype')]");

        readonly By HealthIssueTypesRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By name_Field = By.Id("CWField_name");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
       



        public HealthIssueTypesRecordPage WaitForHealthIssueTypesRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);


            WaitForElement(iframe_healthissuetype);
            SwitchToIframe(iframe_healthissuetype);

            WaitForElement(iframe_healthIssueRecordtype);
            SwitchToIframe(iframe_healthIssueRecordtype);

            WaitForElement(HealthIssueTypesRecordPageHeader);

           

            return this;
        }

        public HealthIssueTypesRecordPage InsertName(String TextToInsert)
        {
            WaitForElement(name_Field);

            SendKeys(name_Field, TextToInsert);

            return this;
        }

        public HealthIssueTypesRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);

            Click(saveAndCloseButton);

            return this;
        }


      


    }
}
