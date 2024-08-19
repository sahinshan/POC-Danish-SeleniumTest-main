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
    public class CopyBookingToDialogPopup : CommonMethods
    {
        public CopyBookingToDialogPopup(IWebDriver driver, WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By popupHeader = By.XPath("//*[@id='cd-dialog']//div[@class='mcc-dialog__header']//h2");

        readonly By topMessage = By.XPath("//*[@id='id--fields--message']");

        readonly By picklist = By.XPath("//*[@id='id--fields--copyToRow']/details");

        By dayOfWeekInput(string DayOfWeek) => By.XPath("//*[@name='" + DayOfWeek + "']");

        By employmentContractInput(string EmploymentContractId) => By.XPath("//input[@data-id='" + EmploymentContractId + "']");

        readonly By closeButton = By.XPath("//*[@id='id--footer--closeButton']");

        readonly By copyButton = By.XPath("//*[@id='id--footer--saveButton']");




        public CopyBookingToDialogPopup WaitForPopupToLoad()
        {
            SwitchToDefaultFrame();

            System.Threading.Thread.Sleep(4000);

            WaitForElementWithSizeVisible(popupHeader);
            WaitForElementWithSizeVisible(topMessage);
            WaitForElementWithSizeVisible(picklist);
            WaitForElementWithSizeVisible(closeButton);
            WaitForElementWithSizeVisible(copyButton);

            return this;
        }

        public CopyBookingToDialogPopup ClickCloseButton()
        {
            ClickWithoutWaiting(closeButton);

            return this;
        }

        public CopyBookingToDialogPopup ClickCopyButton()
        {
            ClickWithoutWaiting(copyButton);

            return this;
        }

        public CopyBookingToDialogPopup SelectDaysOfWeek(params string[] DayOfWeekToSelect)
        {
            ClickWithoutWaiting(picklist);

            foreach (string dow in DayOfWeekToSelect)
            {
                ScrollToElement(dayOfWeekInput(dow));
                ClickWithoutWaiting(dayOfWeekInput(dow));
            }

            ClickWithoutWaiting(picklist);

            return this;
        }

        public CopyBookingToDialogPopup SelectEmploymentContracts(params string[] EmploymentContracts)
        {
            ClickWithoutWaiting(picklist);

            foreach (string EmploymentContractId in EmploymentContracts)
            {
                ScrollToElement(employmentContractInput(EmploymentContractId));
                ClickWithoutWaiting(employmentContractInput(EmploymentContractId));
            }

            ClickWithoutWaiting(picklist);

            return this;
        }
    }
}
