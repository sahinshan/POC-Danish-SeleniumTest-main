
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ProfessionalRecordPage : CommonMethods
    {
        public ProfessionalRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=professional&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'


        #region Top Menu

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By shareButton = By.Id("TI_ShareRecordButton");
        readonly By additionalIItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By runOnDemandWorkflowButton = By.Id("TI_RunOnDemandWorkflow");

        #endregion



        public ProfessionalRecordPage WaitForProfessionalRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(shareButton);
            WaitForElement(additionalIItemsButton);

            return this;
        }

        public ProfessionalRecordPage ClickRunOnDemandWorkflowButton()
        {
            WaitForElement(additionalIItemsButton);
            Click(additionalIItemsButton);

            WaitForElement(runOnDemandWorkflowButton);
            Click(runOnDemandWorkflowButton);

            return this;
        }

    }
}
