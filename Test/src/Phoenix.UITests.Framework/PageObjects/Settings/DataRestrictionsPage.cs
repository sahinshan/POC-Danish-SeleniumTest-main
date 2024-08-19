using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// 
    /// </summary>
    public class DataRestrictionsPage : CommonMethods
    {
        public DataRestrictionsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1");

        public DataRestrictionsPage WaitForDataRestrictionsPageToLoad()
        {
            Wait.Until(c => c.FindElement(contentIFrame));
            driver.SwitchTo().Frame(driver.FindElement(contentIFrame));

            Wait.Until(c => c.FindElement(pageHeader));

            if (driver.FindElement(pageHeader).Text != "Data Restrictions")
                throw new Exception("Data Restrictions page title do not equals: \"Data Restrictions\" ");
            
            return this;
        }
    }
}
