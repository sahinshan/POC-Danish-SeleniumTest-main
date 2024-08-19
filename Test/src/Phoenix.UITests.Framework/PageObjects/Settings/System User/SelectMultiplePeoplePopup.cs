using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SelectMultiplePeoplePopup : CommonMethods
    {
        public SelectMultiplePeoplePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By SelectMultiplePeoplePageHeader = By.XPath("//*[@id='id--rostering--people-layer-instance--layer']/div/div/h2[text()='Select Multiple People']");

        readonly By filterPeopleMainDiv = By.XPath("//*[@id='id--rostering--people-layer--body-filter--search']");

        By recordCheckbox(string recordId) => By.XPath("//*[@id='option--"+ recordId + "']");

        readonly By backToBookingButton = By.XPath("//*[@id='id--id--rostering--people-layer-instance--layer--back']/button");
        readonly By confirmSelectionButton = By.XPath("//*[@id='id--id--rostering--people-layer-instance--layer--confirm']/button");


        #region Select Multiple People Area

        public SelectMultiplePeoplePopup WaitForSelectMultiplePeopleAreaToLoad()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(SelectMultiplePeoplePageHeader);

            WaitForElementVisible(filterPeopleMainDiv);

            WaitForElementVisible(backToBookingButton);
            WaitForElementVisible(confirmSelectionButton);

            return this;
        }

        public SelectMultiplePeoplePopup ClickOnRecordCheckbox(string recordId)
        {
            WaitForElementToBeClickable(recordCheckbox(recordId));
            Click(recordCheckbox(recordId));

            return this;
        }

        public SelectMultiplePeoplePopup ClickOnRecordCheckbox(Guid recordId)
        {
            return ClickOnRecordCheckbox(recordId.ToString());
        }

        public SelectMultiplePeoplePopup ClickBackToBookingButton()
        {
            WaitForElementToBeClickable(backToBookingButton);
            Click(backToBookingButton);

            return this;
        }

        public SelectMultiplePeoplePopup ClickConfirmSelectionButton()
        {
            WaitForElementToBeClickable(confirmSelectionButton);
            Click(confirmSelectionButton);

            return this;
        }

        #endregion

    }
}
