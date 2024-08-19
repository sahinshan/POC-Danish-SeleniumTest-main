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
    public class SpecifyCareWorkerPopup : CommonMethods
    {
        public SpecifyCareWorkerPopup(IWebDriver driver, WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By popupHeader = By.XPath("//*[@id='cd-dialog']//div[@class='mcc-dialog__header']//h2");

        readonly By topMessage = By.XPath("//*[@id='id--fields--staff-info']");

        readonly By picklist = By.XPath("//*[@id='id--fields--staff']/details");

        readonly By searchInput = By.XPath("//*[@id='id--fields--staff--dropdown-filter']");

        By recordInput(string recordDataId) => By.XPath("//button[@data-id='" + recordDataId + "']");

        readonly By closeButton = By.XPath("//*[@id='id--footer--closeButton']");

        readonly By saveButton = By.XPath("//*[@id='id--footer--saveButton']");




        public SpecifyCareWorkerPopup WaitForPopupToLoad()
        {
            SwitchToDefaultFrame();

            System.Threading.Thread.Sleep(4000);

            WaitForElementWithSizeVisible(popupHeader);
            WaitForElementWithSizeVisible(topMessage);
            WaitForElementWithSizeVisible(picklist);
            WaitForElementWithSizeVisible(closeButton);
            WaitForElementWithSizeVisible(saveButton);

            return this;
        }

        public SpecifyCareWorkerPopup ClickCloseButton()
        {
            ClickWithoutWaiting(closeButton);

            return this;
        }

        public SpecifyCareWorkerPopup ClickSaveChangesButton()
        {
            ClickWithoutWaiting(saveButton);

            return this;
        }

        public SpecifyCareWorkerPopup SelectCareWorker(string SearchText, string RecordToSelect)
        {
            ClickWithoutWaiting(picklist);

            var elementExists = CheckIfElementExists(searchInput);
            if (elementExists)
                SendKeys(searchInput, SearchText);

            ScrollToElement(recordInput(RecordToSelect));
            ClickWithoutWaiting(recordInput(RecordToSelect));

            return this;
        }

        public SpecifyCareWorkerPopup SelectCareWorker(string SearchText, Guid RecordToSelect)
        {
            return SelectCareWorker(SearchText, RecordToSelect.ToString());
        }


    }
}
