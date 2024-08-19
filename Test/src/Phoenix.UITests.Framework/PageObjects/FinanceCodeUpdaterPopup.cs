using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinanceCodeUpdaterPopup : CommonMethods
    {
        public FinanceCodeUpdaterPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_FinanceCodeUpdater");
        readonly By popupHeader = By.XPath("//*[@id='CWHeaderTitle'][text()='Update Finance Code']");

        #region Fields

        readonly By position1Field = By.Id("CWField_P1");
        readonly By position2Field = By.Id("CWField_P2");
        readonly By position3Field = By.Id("CWField_P3");
        readonly By position4Field = By.Id("CWField_P4");
        readonly By position5Field = By.Id("CWField_P5");
        readonly By position6Field = By.Id("CWField_P6");
        readonly By position7Field = By.Id("CWField_P7");
        readonly By position8Field = By.Id("CWField_P8");
        readonly By position9Field = By.Id("CWField_P9");

        readonly By saveButton = By.Id("CWButtonOk");
        readonly By cancelButton = By.Id("CWButtonCancel");


        #endregion



        public FinanceCodeUpdaterPopup WaitForPopupToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);

            WaitForElement(position1Field);
            WaitForElement(position2Field);
            WaitForElement(position3Field);
            WaitForElement(position4Field);
            WaitForElement(position5Field);
            WaitForElement(position6Field);
            WaitForElement(position7Field);
            WaitForElement(position8Field);
            WaitForElement(position9Field);


            return this;
        }

        public FinanceCodeUpdaterPopup TapSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            return this;
        }

        public FinanceCodeUpdaterPopup TapCancelButton()
        {
            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);

            return this;
        }

        public FinanceCodeUpdaterPopup InsertTextInPosition1(string Text)
        {
            SendKeys(position1Field, Text + Keys.Tab);

            return this;
        }

        public FinanceCodeUpdaterPopup InsertTextInPosition2(string Text)
        {
            SendKeys(position2Field, Text + Keys.Tab);

            return this;
        }

        public FinanceCodeUpdaterPopup InsertTextInPosition3(string Text)
        {
            SendKeys(position3Field, Text + Keys.Tab);

            return this;
        }

        public FinanceCodeUpdaterPopup InsertTextInPosition4(string Text)
        {
            SendKeys(position4Field, Text + Keys.Tab);

            return this;
        }

        public FinanceCodeUpdaterPopup InsertTextInPosition5(string Text)
        {
            SendKeys(position5Field, Text + Keys.Tab);

            return this;
        }

        public FinanceCodeUpdaterPopup InsertTextInPosition6(string Text)
        {
            SendKeys(position6Field, Text + Keys.Tab);

            return this;
        }

        public FinanceCodeUpdaterPopup InsertTextInPosition7(string Text)
        {
            SendKeys(position7Field, Text + Keys.Tab);

            return this;
        }

        public FinanceCodeUpdaterPopup InsertTextInPosition8(string Text)
        {
            SendKeys(position8Field, Text + Keys.Tab);

            return this;
        }

        public FinanceCodeUpdaterPopup InsertTextInPosition9(string Text)
        {
            SendKeys(position9Field, Text + Keys.Tab);

            return this;
        }

    }
}
