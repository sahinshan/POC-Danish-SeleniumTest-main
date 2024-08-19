using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DataFormFieldPage : CommonMethods
    {
        public DataFormFieldPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_Editor')][contains(@src,'DataFormEditor.aspx?')]");
        readonly By cwDataDialogIFrame = By.Id("iframe_CWDataFormDialog");
        readonly By pageTitle = By.XPath("//h1");        

        readonly By usedInCodeFieldLabel = By.XPath("//li[@id = 'CWLabelHolder_usedincode']");
        readonly By usedInCode_YesOption = By.Id("CWField_usedincode_1");
        readonly By usedInCode_NoOption = By.Id("CWField_usedincode_0");

        #region Toolbar options
        readonly By BackButton = By.XPath("//button[@id='BackButton']");
        #endregion

        public DataFormFieldPage WaitForDataFormFieldPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElement(cwDataDialogIFrame);
            SwitchToIframe(cwDataDialogIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);
            WaitForElement(pageTitle);


            return this;
        }

        public DataFormFieldPage ClickBackButton()
        {
            MoveToElementInPage(BackButton);
            WaitForElementToBeClickable(BackButton);            
            Click(BackButton);

            return this;
        }

        public DataFormFieldPage ValidateUsedInCodeNoOptionSelected(bool ExpectedSelected)
        {
            WaitForElement(usedInCodeFieldLabel);
            WaitForElementVisible(usedInCodeFieldLabel);
            MoveToElementInPage(usedInCodeFieldLabel);

            if (ExpectedSelected)
            {
                ValidateElementChecked(usedInCode_NoOption);
                ValidateElementNotChecked(usedInCode_YesOption);
            }
            else
            {
                ValidateElementChecked(usedInCode_YesOption);
                ValidateElementNotChecked(usedInCode_NoOption);
            }
            return this;
        }
    }
}
