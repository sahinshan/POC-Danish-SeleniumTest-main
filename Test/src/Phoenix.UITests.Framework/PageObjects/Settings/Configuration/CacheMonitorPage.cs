using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CacheMonitorPage : CommonMethods
    {
        public CacheMonitorPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }




        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By CacheMonitorPageHeader = By.XPath("//*[@id='CWHeader']/h1");


        By recordRow_NameCell(string CacheElementName) => By.XPath("//*[@id='CWGrid']/tbody/tr/td[2][text()='" + CacheElementName + "']");
        By recordRow_RecycleButton(string CacheElementName) => By.XPath("//*[@id='CWGrid']/tbody/tr/td[2][text()='" + CacheElementName + "']/parent::tr/td/a");






        public CacheMonitorPage WaitForCacheMonitorPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(CacheMonitorPageHeader);

            return this;
        }


        public CacheMonitorPage ValidateCacheElementVisible(string CacheElementName)
        {
            WaitForElementVisible(recordRow_NameCell(CacheElementName));
            WaitForElementVisible(recordRow_RecycleButton(CacheElementName));

            return this;
        }

    }
}
