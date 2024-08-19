using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonContributionRecordPage : CommonMethods
    {

        public PersonContributionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=facontribution&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By cancelButton = By.Id("TI_Cancel");

        #region Menu Area

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a[@title='Menu']");

        readonly By activitiesMenuArea = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By relatedItemsMenuArea = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']/a");

        readonly By contributionExceptionsButton = By.Id("CWNavItem_ContributionException");


        #endregion






        public PersonContributionRecordPage WaitForPersonContributionRecordPageToLoad(string ContributionName)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);
            this.WaitForElement(deleteButton);
            this.WaitForElement(cancelButton);

            if (driver.FindElement(pageHeader).Text != "Contribution: " + ContributionName)
                throw new Exception("Page title do not equals: Contribution: " + ContributionName);

            return this;
        }


        public PersonContributionRecordPage NavigateToContributionsExceptionsPage()
        {
            this.WaitForElement(MenuButton);
            this.Click(MenuButton);

            this.WaitForElement(relatedItemsMenuArea);
            this.Click(relatedItemsMenuArea);

            this.WaitForElement(contributionExceptionsButton);
            this.Click(contributionExceptionsButton);

            return this;
        }


        public PersonContributionRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonContributionRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);


            return this;
        }

        public PersonContributionRecordPage ClickDeleteButton()
        {
            this.Click(deleteButton);


            return this;
        }

        public PersonContributionRecordPage ClickCancelButton()
        {
            this.Click(cancelButton);


            return this;
        }

        public PersonContributionRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }




    }
}
