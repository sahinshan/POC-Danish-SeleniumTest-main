using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WorkflowJobRecordPage : CommonMethods
    {
        public WorkflowJobRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=workflowjob&')]");

        readonly By WorkflowJobRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By executeJob_Button = By.Id("TI_ExecuteButton");

        readonly By Workflow_FieldLabel = By.XPath("//*[@id='CWLabelHolder_workflowid']/label");

        




        public WorkflowJobRecordPage WaitForWorkflowJobRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(WorkflowJobRecordPageHeader);

            WaitForElement(back_Button);
            WaitForElement(executeJob_Button);

            WaitForElement(Workflow_FieldLabel);

            return this;
        }

        
        public WorkflowJobRecordPage ClickExecuteButton()
        {
            WaitForElementToBeClickable(executeJob_Button);
            Click(executeJob_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public WorkflowJobRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(back_Button);
            Click(back_Button);

            return this;
        }



    }
}
