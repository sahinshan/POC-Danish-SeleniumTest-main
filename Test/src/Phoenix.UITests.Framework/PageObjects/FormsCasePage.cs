using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FormsCasePage : CommonMethods
    {
        public FormsCasePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Forms (Case)']");

        public FormsCasePage WaitForFormsCasePageToLoad()
        {
            Wait.Until(c => c.FindElement(contentIFrame));
            driver.SwitchTo().Frame(driver.FindElement(contentIFrame));

            Wait.Until(c => c.FindElement(pageHeader));

            if (driver.FindElement(pageHeader).Text != "CasesForms (Case))") 
                throw new Exception("Forms (Case) page title do not equals: \"Forms (Case)\" ");

            return this;
        }
    }
}
