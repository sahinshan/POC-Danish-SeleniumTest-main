using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AutomatedUiTestDocument4PreviewPage : CommonMethods
    {
        public AutomatedUiTestDocument4PreviewPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By documentIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=document&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By iframe_CWAssessmentDialog = By.Id("iframe_CWAssessmentDialog");


        #region Top Menu

        readonly By backButton = By.XPath("//button[@title='Back']");
        readonly By expandAllButton = By.Id("TI_CWAssessmentExpandAll");
        readonly By collapseAllButton = By.Id("TI_CWAssessmentCollapseAll");
        readonly By changeLanguageButton = By.Id("TI_CWChangeLanguage");

        #endregion

        #region Left Navigation Area

        readonly By section1Button = By.XPath("//a[@id='NL_QA-DS-135'][@title='Section 1']");

        #endregion

        #region Fields

        readonly By WFDecimalManageSDEMapsButton = By.XPath("//*[@id='QA-DSQ-406']/li/ul/li[2]");

        #endregion


        public AutomatedUiTestDocument4PreviewPage WaitForPreviewPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(documentIFrame);
            SwitchToIframe(documentIFrame);

            WaitForElement(iframe_CWAssessmentDialog);
            SwitchToIframe(iframe_CWAssessmentDialog);

            WaitForElement(backButton);
            WaitForElement(expandAllButton);
            WaitForElement(collapseAllButton);
            WaitForElement(changeLanguageButton);

            return this;

        }


        /// <summary>
        /// Tap on the "Preview" button
        /// </summary>
        public ManageSDEMapsPopup TapWFDecimalManageSDEMapsButton()
        {
            Click(WFDecimalManageSDEMapsButton);

            return new ManageSDEMapsPopup(this.driver, this.Wait, this.appURL);
        }



    }
}
