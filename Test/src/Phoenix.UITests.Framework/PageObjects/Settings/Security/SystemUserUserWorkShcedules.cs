using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserUserWorkShcedules : CommonMethods
    {
        public SystemUserUserWorkShcedules(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContent_Iframe = By.XPath("//iframe[@id='CWContentIFrame']");
        readonly By systemUserstaffReview_iframe = By.XPath("//iframe[@id='iframe_CWDialog_22424407-8760-ec11-a32d-f90a4322a942']");
        readonly By workSchedule_Link = By.XPath("//li[@id='CWNavGroup_WorkSchedule']/a");
    
           
        public SystemUserUserWorkShcedules WaitForSystemUserUserWorkShcedulesToLoad()
        {
            SwitchToDefaultFrame();
            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);
            WaitForElement(systemUserstaffReview_iframe);
            SwitchToIframe(systemUserstaffReview_iframe);
            WaitForElement(workSchedule_Link);

            return this;
        }
        public SystemUserUserWorkShcedules ClickCreateRecordButton()
        {
           
            WaitForElement(workSchedule_Link);
            Click(workSchedule_Link);
          
            return this;
        }
    }
}
