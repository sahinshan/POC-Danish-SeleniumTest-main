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

    /// <summary>
    /// this class represents the dynamic dialog that is displayed with messages to the user.
    /// </summary>
    public class WallchartWarningDialogPopup : CommonMethods
    {
        public WallchartWarningDialogPopup(IWebDriver driver, WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By popupHeader = By.XPath("//*[@id='cd-dialog']//div[@class='mcc-dialog__header']//h2");

        readonly By messageArea = By.XPath("//*[@id='cd-dialog']//div[@class='mcc-dialog__body']");
        By MessageLine(int LineNumber) => By.XPath("//*[@id='id--fields--" + LineNumber + "']");
        By BulletlistLine(int IndexNumber) => By.XPath("//*[@id='id--fields--text-" + IndexNumber + "']");

        readonly By cancelButton = By.XPath("//*[@id='id--footer--cancel']");
        readonly By saveButton = By.XPath("//*[@id='id--footer--saveButton']");




        public WallchartWarningDialogPopup WaitForWallchartWarningDialogPopupToLoad(bool WaitForRefreshPanelNotVisible = true)
        {
            SwitchToDefaultFrame();

            System.Threading.Thread.Sleep(5000);

            if(WaitForRefreshPanelNotVisible)
                WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementWithSizeVisible(popupHeader);
            WaitForElementWithSizeVisible(messageArea);
            WaitForElementWithSizeVisible(cancelButton);
            WaitForElementWithSizeVisible(saveButton);

            return this;
        }

        public WallchartWarningDialogPopup WaitForWallchartErrorDialogPopupToLoad()
        {
            SwitchToDefaultFrame();

            System.Threading.Thread.Sleep(5000);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementWithSizeVisible(popupHeader);
            WaitForElementWithSizeVisible(messageArea);
            WaitForElementWithSizeVisible(cancelButton);
            WaitForElementNotVisible(saveButton, 3);

            return this;
        }

        public WallchartWarningDialogPopup ClickCancelButton()
        {
            ClickOnElementWithSize(cancelButton);

            return this;
        }

        public WallchartWarningDialogPopup ClickSaveButton()
        {
            ClickOnElementWithSize(saveButton);

            return this;
        }

        public WallchartWarningDialogPopup ValidateMessageLine(int LineNumber, string ExpectedText)
        {
            var elementText = GetElementTextByJavascript(MessageLine(LineNumber));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public WallchartWarningDialogPopup ValidateBulletlistLine(int IndexNumber, string ExpectedText)
        {
            var elementText = GetElementTextByJavascript(BulletlistLine(IndexNumber));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

    }
}
