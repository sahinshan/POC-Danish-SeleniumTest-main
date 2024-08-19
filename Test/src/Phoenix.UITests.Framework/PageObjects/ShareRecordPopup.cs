using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ShareRecordPopup : CommonMethods
    {
        public ShareRecordPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_ShareRecord");
        readonly By popupHeader = By.XPath("//div[@id='CWRecordShareDialog']/div/h1[contains(text(), 'Share Record')]");

        #region Elements

        readonly By shareWithTypeCombobox = By.Id("CWShareWithType");
        readonly By searchTextBox = By.Id("CWShareWithKeyword");
        readonly By searchbutton = By.Id("CWQuickSearchButton");

        readonly By resultHeaderUserAndTeams = By.XPath("//tr[@id='CWGridHeaderRow']/th[text()='User/Team']");
        readonly By resultHeaderView = By.XPath("//th[@id='ThCanView']/a");
        readonly By resultHeaderEdit = By.XPath("//th[@id='ThCanEdit']/a");
        readonly By resultHeaderDelete = By.XPath("//th[@id='ThCanDelete']/a");
        readonly By resultHeaderShare = By.XPath("//th[@id='ThCanShare']/a");

        readonly By savebutton = By.Id("CWSaveButton");
        readonly By closebutton = By.Id("CWCloseButton");

        #endregion

        #region Labels

        readonly By NoRecordsLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordsMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");


        #endregion

        #region Share elements

        By objectName(string RecordLevelAccessID, string UserOrTeamName) => By.XPath("//a[@id='Name_" + RecordLevelAccessID + "'][text()='" + UserOrTeamName + "']");

        By viewCheckbox(string RecordLevelAccessID) => By.XPath("//input[@id='CWView_" + RecordLevelAccessID + "']");
        By editCheckbox(string RecordLevelAccessID) => By.XPath("//input[@id='CWEdit_" + RecordLevelAccessID + "']");
        By deleteCheckbox(string RecordLevelAccessID) => By.XPath("//input[@id='CWDelete_" + RecordLevelAccessID + "']");
        By shareCheckbox(string RecordLevelAccessID) => By.XPath("//input[@id='CWShare_" + RecordLevelAccessID + "']");
        By deleteButton(string RecordLevelAccessID) => By.XPath("//a[contains(@onclick, 'CW.RecordShare.Delete')][contains(@onclick, '" + RecordLevelAccessID + "')]");


        #endregion

        public ShareRecordPopup WaitForShareRecordPopupToLoad()
        {
            Wait.Until(c => c.FindElement(popupIframe));
            driver.SwitchTo().Frame(driver.FindElement(popupIframe));

            Wait.Until(c => c.FindElement(popupHeader));

            Wait.Until(c => c.FindElement(shareWithTypeCombobox));
            Wait.Until(c => c.FindElement(searchTextBox));
            Wait.Until(c => c.FindElement(searchbutton));

            Wait.Until(c => c.FindElement(savebutton));
            Wait.Until(c => c.FindElement(closebutton));



            return this;
        }

        public ShareRecordPopup WaitForResultsPopupToClose()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(resultHeaderUserAndTeams));
            Wait.Until(ExpectedConditions.ElementToBeClickable(resultHeaderView));
            Wait.Until(ExpectedConditions.ElementToBeClickable(resultHeaderEdit));
            Wait.Until(ExpectedConditions.ElementToBeClickable(resultHeaderDelete));
            Wait.Until(ExpectedConditions.ElementToBeClickable(resultHeaderShare));
            
            return this;
        }

        public ShareRecordPopup ValidateNoRecordsMessages()
        {
            Wait.Until(c => c.FindElement(NoRecordsLabel));
            Wait.Until(c => c.FindElement(NoRecordsMessage));

            return this;
        }

        public ShareRecordPopup ValidateNoRecordsMessagesNotDisplayed()
        {
            bool element1Visible = GetElementVisibility(NoRecordsLabel);
            bool element2Visible = GetElementVisibility(NoRecordsMessage);

            Assert.IsFalse(element1Visible);
            Assert.IsFalse(element2Visible);

            return this;
        }

        public ShareRecordPopup ValidateUserOrTeamInformationPresent(string RecordLevelAccessID, string UserOrTeamName)
        {
            Wait.Until(c => c.FindElement(objectName(RecordLevelAccessID, UserOrTeamName)));

            Wait.Until(c => c.FindElement(viewCheckbox(RecordLevelAccessID)));
            Wait.Until(c => c.FindElement(editCheckbox(RecordLevelAccessID)));
            Wait.Until(c => c.FindElement(deleteCheckbox(RecordLevelAccessID)));
            Wait.Until(c => c.FindElement(shareCheckbox(RecordLevelAccessID)));
            Wait.Until(c => c.FindElement(deleteButton(RecordLevelAccessID)));
            
            
            return this;
        }

        public ShareRecordPopup ValidateViewAccessToUserOrTeam(string RecordLevelAccessID, bool ExpectViewAccess)
        {
            IWebElement element = driver.FindElement(viewCheckbox(RecordLevelAccessID));
            string attributeValue = element.GetAttribute("checked");
            bool viewAccess = attributeValue == "true" ? true : false;
            Assert.AreEqual(ExpectViewAccess, viewAccess);
            
            return this;
        }

        public ShareRecordPopup ValidateEditAccessToUserOrTeam(string RecordLevelAccessID, bool ExpectEditAccess)
        {
            IWebElement element = driver.FindElement(editCheckbox(RecordLevelAccessID));
            string attributeValue = element.GetAttribute("checked");
            bool editAccess = attributeValue == "true" ? true : false;
            Assert.AreEqual(ExpectEditAccess, editAccess);

            return this;
        }

        public ShareRecordPopup ValidateDeleteAccessToUserOrTeam(string RecordLevelAccessID, bool ExpectDeleteAccess)
        {
            IWebElement element = driver.FindElement(deleteCheckbox(RecordLevelAccessID));
            string attributeValue = element.GetAttribute("checked");
            bool deleteAccess = attributeValue == "true" ? true : false;
            Assert.AreEqual(ExpectDeleteAccess, deleteAccess);

            return this;
        }

        public ShareRecordPopup ValidateShareAccessToUserOrTeam(string RecordLevelAccessID, bool ExpectShareAccess)
        {
            IWebElement element = driver.FindElement(shareCheckbox(RecordLevelAccessID));
            string attributeValue = element.GetAttribute("checked");
            bool shareAccess = attributeValue == "true" ? true : false;
            Assert.AreEqual(ExpectShareAccess, shareAccess);

            return this;
        }

        public ShareRecordResultsPopup SearchForUserRecord(string SearchQuery)
        {
            var element = driver.FindElement(shareWithTypeCombobox);
            OpenQA.Selenium.Support.UI.SelectElement se = new OpenQA.Selenium.Support.UI.SelectElement(element);
            se.SelectByValue("systemuser");

            driver.FindElement(searchTextBox).Clear();
            driver.FindElement(searchTextBox).Click();
            driver.FindElement(searchTextBox).SendKeys(SearchQuery);

            driver.FindElement(searchbutton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return new ShareRecordResultsPopup(driver, Wait, appURL);
        }

        public ShareRecordResultsPopup SearchForTeamRecord(string SearchQuery)
        {
            var element = driver.FindElement(shareWithTypeCombobox);
            OpenQA.Selenium.Support.UI.SelectElement se = new OpenQA.Selenium.Support.UI.SelectElement(element);
            se.SelectByValue("team");

            
            driver.FindElement(searchTextBox).Clear();
            driver.FindElement(searchTextBox).Click();
            driver.FindElement(searchTextBox).SendKeys(SearchQuery);

            driver.FindElement(searchbutton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return new ShareRecordResultsPopup(driver, Wait, appURL);
        }

        public ShareRecordPopup RemoveShare(string RecordLevelAccessID)
        {
            driver.FindElement(deleteButton(RecordLevelAccessID)).Click();

            WaitForElementNotVisible(viewCheckbox(RecordLevelAccessID), 7);
            WaitForElementNotVisible(editCheckbox(RecordLevelAccessID), 7);
            WaitForElementNotVisible(deleteCheckbox(RecordLevelAccessID), 7);
            WaitForElementNotVisible(shareCheckbox(RecordLevelAccessID), 7);
            WaitForElementNotVisible(deleteButton(RecordLevelAccessID), 7);

            return this;
        }

        public ShareRecordPopup TapViewCheckbox(string RecordLevelAccessID)
        {
            driver.FindElement(viewCheckbox(RecordLevelAccessID)).Click();

            return this;
        }

        public ShareRecordPopup TapEditCheckbox(string RecordLevelAccessID)
        {
            WaitForElementToBeClickable(editCheckbox(RecordLevelAccessID));
            Click(editCheckbox(RecordLevelAccessID));

            return this;
        }

        public ShareRecordPopup TapDeleteCheckbox(string RecordLevelAccessID)
        {
            driver.FindElement(deleteCheckbox(RecordLevelAccessID)).Click();

            return this;
        }

        public ShareRecordPopup TapShareCheckbox(string RecordLevelAccessID)
        {
            driver.FindElement(shareCheckbox(RecordLevelAccessID)).Click();

            return this;
        }

        /// <summary>
        /// Tap on the "Edit" header link (to select activate the Edit permission in all shares)
        /// </summary>
        /// <returns></returns>
        public ShareRecordPopup TapEditHeaderLink()
        {
            driver.FindElement(resultHeaderEdit).Click();

            return this;
        }

        public ShareRecordPopup TapSaveButton()
        {
            driver.FindElement(savebutton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

    }
}
