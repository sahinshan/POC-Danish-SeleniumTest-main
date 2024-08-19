using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class HealthIssueTypesPage : CommonMethods
    {
        public HealthIssueTypesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_healthissuetype = By.Id("iframe_healthissuetype");

        readonly By HealthIssueTypesPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        
        readonly By selectAll_Checkbox = By.Id("cwgridheaderselector");
  


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By recordRow_CreatedOnCell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

       



        public HealthIssueTypesPage WaitForHealthIssueTypesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_healthissuetype);
            SwitchToIframe(iframe_healthissuetype);

            WaitForElement(HealthIssueTypesPageHeader);

            WaitForElement(selectAll_Checkbox);

            return this;
        }

        public HealthIssueTypesPage ClickNewRecordButton()
        {
            WaitForElement(newRecordButton);
            
            Click(newRecordButton);

            return this;
        }


        public HealthIssueTypesPage SearchHealthIssueTypesRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public HealthIssueTypesPage SelectHealthIssueTypesRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public HealthIssueTypesPage SelectHealthIssueTypeCheckBox()
        {
            WaitForElement(selectAll_Checkbox);
            Click(selectAll_Checkbox);

            return this;
        }


        public HealthIssueTypesPage ClickDeleteRecordButton()
        {
            WaitForElement(deleteRecordButton);

            Click(deleteRecordButton);

            return this;
        }





    }
}
