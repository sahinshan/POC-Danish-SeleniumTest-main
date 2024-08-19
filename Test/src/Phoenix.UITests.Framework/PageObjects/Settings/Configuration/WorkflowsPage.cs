using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WorkflowsPage : CommonMethods
    {
        public WorkflowsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By WorkflowsPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        
        readonly By selectAll_Checkbox = By.Id("cwgridheaderselector");
        readonly By name_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[2]");


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By recordRow_NameCell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");


        public WorkflowsPage WaitForWorkflowsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(WorkflowsPageHeader);

            WaitForElement(selectAll_Checkbox);
            WaitForElement(name_Header);

            return this;
        }

        public WorkflowsPage SearchWorkflowRecord(string SearchQuery, string RecordID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow_NameCell(RecordID));

            return this;
        }

        public WorkflowsPage SearchWorkflowRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public WorkflowsPage OpenWorkflowRecord(string RecordId)
        {
            WaitForElement(recordRow_NameCell(RecordId));
            Click(recordRow_NameCell(RecordId));

            return this;
        }

        public WorkflowsPage SelectWorkflowRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public WorkflowsPage ClickNewRecordButton()
        {
            WaitForElement(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public WorkflowsPage ValidateRecordNameCell(string recordID, string ExpectedText)
        {
            ValidateElementText(recordRow_NameCell(recordID), ExpectedText);
            return this;
        }
        


    }
}
