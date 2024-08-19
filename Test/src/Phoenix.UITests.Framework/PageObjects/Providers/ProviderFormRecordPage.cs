using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ProviderFormRecordPage : CommonMethods
    {

        public ProviderFormRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=providerform&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By editAssessmentButton = By.Id("TI_EditAssessmentButton");


        #region Field Titles

        readonly By ProviderFieldLabel = By.XPath("//*[@id='CWLabelHolder_providerid']/label");

        #endregion

        #region Fields

        readonly By providerLinkField = By.XPath("//*[@id='CWField_providerid_Link']");
        readonly By providerLookupButton = By.XPath("//*[@id='CWLookupBtn_providerid']");
        readonly By providerRemoveButton = By.XPath("//*[@id='CWClearLookup_providerid']");

        #endregion




        public ProviderFormRecordPage WaitForProviderFormRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(ProviderFieldLabel);

            return this;
        }

        public ProviderFormRecordPage ClickProviderLookupButton()
        {
            Click(providerLookupButton);

            return this;
        }
        
        public ProviderFormRecordPage ValidateProviderLinkFieldText(string ExpectedText)
        {
            ValidateElementText(providerLinkField, ExpectedText);

            return this;
        }
        


        public ProviderFormRecordPage ClickBackButton()
        {
            this.Click(backButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public ProviderFormRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public ProviderFormRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            return this;
        }
        public ProviderFormRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }
        public ProviderFormRecordPage ClickEditAssessmentButton()
        {
            this.Click(editAssessmentButton);

            return this;
        }

    }
}
