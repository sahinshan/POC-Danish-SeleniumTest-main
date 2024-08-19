using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the dynamic dialog that is displayed with messages to the user.
    /// </summary>
    public class DrawerDialogPopup : CommonMethods
    {
        public DrawerDialogPopup(IWebDriver driver, WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        By iframe_CWDialog(string type) => By.XPath("//iframe[contains(@id,'mcc-iframe')][contains(@src,'type=" + type + "')]");

        readonly By popupHeader = By.XPath("//*[@class='mcc-drawer__header']//h2");

        readonly By closeIcon = By.XPath("//button[@class='mcc-button mcc-button--sm mcc-button--ghost mcc-button--icon-only btn-close-drawer']");
        readonly By expandIcon = By.XPath("//button[@class='mcc-button mcc-button--sm mcc-button--ghost mcc-button--icon-only btn-full-screen-drawer']");




        public DrawerDialogPopup WaitForDrawerDialogPopupToLoad(string BOType)
        {
            SwitchToDefaultFrame();

            WaitForElementVisible(iframe_CWDialog(BOType));
            SwitchToIframe(iframe_CWDialog(BOType));

            WaitForElementVisible(popupHeader);
            WaitForElementVisible(closeIcon);

            return this;
        }

        public DrawerDialogPopup ValidatePageHeader(string titleName)
        {
            WaitForElementVisible(popupHeader);
            ValidateElementText(popupHeader, titleName);

            return this;
        }

        public DrawerDialogPopup ClickOnCloseIcon()
        {
            WaitForElementToBeClickable(closeIcon);
            ScrollToElement(closeIcon);
            Click(closeIcon);

            return this;
        }

        public DrawerDialogPopup ClickOnExpandIcon()
        {
            WaitForElementToBeClickable(expandIcon);
            ScrollToElement(expandIcon);
            Click(expandIcon);

            return this;
        }


    }
}
