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
    public class ChangeBookingStaffStatusDialogPopup : CommonMethods
    {
        public ChangeBookingStaffStatusDialogPopup(IWebDriver driver, WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By popupHeader = By.XPath("//*[@id='cd-dialog']//div[@class='mcc-dialog__header']//h2");
        readonly By topMessage = By.XPath("//*[@id='id--fields--info']");

        readonly By staffCostsCheckmark = By.XPath("//*[@id='id--fields--loading-costs']//mcc-icon[@name='checkmark']");
        readonly By staffCostsMessage = By.XPath("//*[@id='id--fields--messages']");

        readonly By closeButton = By.XPath("//section//*[@id='id--footer--closeButton']");
        readonly By saveChangesButton = By.XPath("//section//*[@id='id--footer--saveButton']");




        public ChangeBookingStaffStatusDialogPopup WaitForPopupToLoad()
        {
            System.Threading.Thread.Sleep(4000);

            SwitchToDefaultFrame();

            WaitForElementWithSizeVisible(popupHeader);
            WaitForElementWithSizeVisible(topMessage);

            WaitForElementWithSizeVisible(closeButton);
            WaitForElementWithSizeVisible(saveChangesButton);

            return this;
        }

        public ChangeBookingStaffStatusDialogPopup ClickCloseButton()
        {
            ClickWithoutWaiting(closeButton);

            return this;
        }

        public ChangeBookingStaffStatusDialogPopup ClickSaveChangesButton()
        {
            ClickWithoutWaiting(saveChangesButton);

            return this;
        }

        public ChangeBookingStaffStatusDialogPopup ValidateStaffCostsMessage(string ExpectedText)
        {
            var elementText = GetElementTextByJavascript(staffCostsMessage);
            Assert.AreEqual(elementText, ExpectedText);

            return this;
        }

        public ChangeBookingStaffStatusDialogPopup ValidateStaffCostsCheckmarkDisplayed()
        {
            WaitForElement(staffCostsCheckmark);

            return this;
        }


    }
}
