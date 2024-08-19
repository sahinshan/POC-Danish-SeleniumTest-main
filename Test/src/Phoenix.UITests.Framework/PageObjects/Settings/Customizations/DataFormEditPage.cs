using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DataFormEditPage : CommonMethods
    {
        public DataFormEditPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_Editor')][contains(@src,'DataFormEditor.aspx?')]");
        readonly By pageTitle = By.XPath("//*[@id = 'CWFormName']");        

        readonly By pronounsField_EditButton = By.XPath("//div[text() = 'Pronouns']/following-sibling::*/button[@title = 'Edit this field']");

        #region Toolbar options
        readonly By BackButton = By.XPath("//button[@title='Back']");
        #endregion

        public DataFormEditPage WaitForDataFormEditPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);


            return this;
        }

        public DataFormEditPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            MoveToElementInPage(BackButton);
            Click(BackButton);

            return this;
        }

        public DataFormEditPage ClickPronounFieldEditButton()
        {
            WaitForElementToBeClickable(pronounsField_EditButton);
            MoveToElementInPage(pronounsField_EditButton);
            Click(pronounsField_EditButton);

            return this;
        }
    }
}
