using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ProfessionalsPage : CommonMethods
    {
        public ProfessionalsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By ProfessionalRow(string ProfessionalID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ProfessionalID + "']/td[2]");
        By ProfessionalRowCheckBox(string ProfessionalID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + ProfessionalID + "']/td[1]/input");

        public ProfessionalsPage WaitForProfessionalsPageToLoad()
        {

            Wait.Until(c => c.FindElement(contentIFrame));
            driver.SwitchTo().Frame(driver.FindElement(contentIFrame));

            Wait.Until(c => c.FindElement(pageHeader));

            if (driver.FindElement(pageHeader).Text != "Professionals")
                throw new Exception("Professionals page title do not equals: \"Professionals\" ");
            
            return this;
        }

        public ProfessionalsPage SearchProfessionalRecord(string SearchQuery, string ProfessionalID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(ProfessionalRow(ProfessionalID));

            return this;
        }

        public ProfessionalsPage SearchProfessionalRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ProfessionalsPage OpenProfessionalRecord(string ProfessionalId)
        {
            WaitForElement(ProfessionalRow(ProfessionalId));
            driver.FindElement(ProfessionalRow(ProfessionalId)).Click();

            return this;
        }

        public ProfessionalsPage SelectProfessionalRecord(string ProfessionalId)
        {
            WaitForElement(ProfessionalRowCheckBox(ProfessionalId));
            Click(ProfessionalRowCheckBox(ProfessionalId));

            return this;
        }
    }
}
