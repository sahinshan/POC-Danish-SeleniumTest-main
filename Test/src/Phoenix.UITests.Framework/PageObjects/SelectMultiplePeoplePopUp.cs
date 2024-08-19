using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SelectMultiplePeoplePopUp : CommonMethods
    {
        public SelectMultiplePeoplePopUp(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By dialogHeader = By.XPath("//div[@class='mcc-drawer__header']/div[2]");
        readonly By filterInput = By.XPath("//*[@id='id--rostering--people-layer--body-filter--search']");
        readonly By confirmSelectionButton = By.XPath("//div[@id='id--id--rostering--people-layer-instance--layer--confirm']");
        readonly By backToBookingButton = By.XPath("//div[@id='id--id--rostering--people-layer-instance--layer--back']");
        By peopleRecordSelection(string RecordID) => By.XPath("//input[@id='option--" + RecordID + "']");



        public SelectMultiplePeoplePopUp WaitForSelectMultiplePeoplePopUpPageToLoad()
        {
            SwitchToDefaultFrame();
            WaitForElementNotVisible("CWRefreshPanel", 15);
            WaitForElement(confirmSelectionButton);
            WaitForElement(backToBookingButton);


            return this;
        }

        public SelectMultiplePeoplePopUp SearchRecords(string SearchQuery)
        {
            WaitForElementToBeClickable(filterInput);
            SendKeys(filterInput, SearchQuery + Keys.Tab);

            return this;
        }

        public SelectMultiplePeoplePopUp ClickPeopleRecordCellText(string RecordID)
        {
            WaitForElementToBeClickable(peopleRecordSelection(RecordID));
            Click(peopleRecordSelection(RecordID));

            return this;
        }

        public SelectMultiplePeoplePopUp ClickConfirmSelection()
        {
            WaitForElementToBeClickable(confirmSelectionButton);
            Click(confirmSelectionButton);

            return this;
        }


    }
}
