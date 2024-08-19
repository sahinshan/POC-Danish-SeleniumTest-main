using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WorkflowJobsPage : CommonMethods
    {
        public WorkflowJobsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=workflow&')]");
        readonly By CWNavItem_WokflowJobsFrame = By.Id("CWUrlPanel_IFrame");

        readonly By WorkflowJobsPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        
        readonly By selectAll_Checkbox = By.Id("cwgridheaderselector");


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By recordRow_CreatedOnCell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");




        public WorkflowJobsPage WaitForWorkflowJobsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWNavItem_WokflowJobsFrame);
            SwitchToIframe(CWNavItem_WokflowJobsFrame);

            WaitForElement(WorkflowJobsPageHeader);

            WaitForElement(selectAll_Checkbox);

            return this;
        }

        public WorkflowJobsPage SearchWorkflowJobRecord(string SearchQuery, string RecordID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow_CreatedOnCell(RecordID));

            return this;
        }

        public WorkflowJobsPage SearchWorkflowJobRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public WorkflowJobsPage OpenWorkflowJobRecord(string RecordId)
        {
            WaitForElement(recordRow_CreatedOnCell(RecordId));
            Click(recordRow_CreatedOnCell(RecordId));

            return this;
        }

        public WorkflowJobsPage SelectWorkflowJobRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

       

        public WorkflowJobsPage ValidateRecordCreatedOnCell(string recordID, string ExpectedText)
        {
            ValidateElementText(recordRow_CreatedOnCell(recordID), ExpectedText);
            return this;
        }
        


    }
}
