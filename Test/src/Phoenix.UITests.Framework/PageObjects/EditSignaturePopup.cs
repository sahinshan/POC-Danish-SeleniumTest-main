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
    public class EditSignaturePopup : CommonMethods
    {
        public EditSignaturePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By iframe_CWEditSignatureWindow = By.Id("iframe_CWEditSignatureWindow");



        readonly By popupHeader = By.XPath("//*[@id='CWHeader']/h1");


        readonly By CanvasArea = By.Id("CWSignatureCanvas");


        readonly By SaveButton = By.Id("CWSaveButton");
        readonly By RevertButton = By.Id("CWUndoButton");
        readonly By ClearButton = By.Id("CWClearButton");
        readonly By CloseButton = By.Id("CWCloseButton");




        public EditSignaturePopup WaitForEditSignaturePopupToLoad()
        {
            WaitForElement(iframe_CWEditSignatureWindow);
            SwitchToIframe(iframe_CWEditSignatureWindow);

            WaitForElement(popupHeader);

            WaitForElement(ClearButton);
            WaitForElement(CloseButton);

            return this;
        }

        public EditSignaturePopup ClicAndDragonOnCanvas(int offsetX, int offsetY)
        {
            WaitForElementToBeClickable(CanvasArea);
            ClickAndDrag(CanvasArea, offsetX, offsetY);

            return this;
        }


        public EditSignaturePopup ClickSaveButton()
        {
            Click(SaveButton);

            return this;
        }

        public EditSignaturePopup ClickRevertButton()
        {
            Click(RevertButton);

            return this;
        }

        public EditSignaturePopup ClickClearButton()
        {
            Click(ClearButton);

            return this;
        }

        public EditSignaturePopup ClickCloseButton()
        {
            Click(CloseButton);

            return this;
        }

    }
}
