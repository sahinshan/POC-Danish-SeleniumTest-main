using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class UserChartsPage : CommonMethods
    {
        public UserChartsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By UserChartsPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='User Charts']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By UserChartRowCheckBox(string UserChartID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + UserChartID + "']/td[1]/input");
        By UserChartRow(string UserChartID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + UserChartID + "']/td[2]");



        




        public UserChartsPage WaitForUserChartsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(UserChartsPageHeader);
            WaitForElement(viewsPicklist);

            return this;
        }

        public UserChartsPage SearchUserChartRecord(string SearchQuery, string UserChartID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(UserChartRow(UserChartID));

            return this;
        }

        public UserChartsPage SearchUserChartRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public UserChartsPage OpenUserChartRecord(string UserChartId)
        {
            WaitForElement(UserChartRow(UserChartId));
            Click(UserChartRow(UserChartId));

            return this;
        }

        public UserChartsPage SelectUserChartRecord(string UserChartId)
        {
            WaitForElement(UserChartRowCheckBox(UserChartId));
            Click(UserChartRowCheckBox(UserChartId));

            return this;
        }

        public UserChartsPage TapAddNewRecordButton()
        {
            WaitForElement(addNewRecordButton);
            Click(addNewRecordButton);

            return this;
        }

        public UserChartsPage TapDeleteButton()
        {
            WaitForElement(additionalItemsButton);
            SwitchToIframe(additionalItemsButton);

            WaitForElement(deleteButton);
            SwitchToIframe(deleteButton);

            return this;
        }

    }
}
