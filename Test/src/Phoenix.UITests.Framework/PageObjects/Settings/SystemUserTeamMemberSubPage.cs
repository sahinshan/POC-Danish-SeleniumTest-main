
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the Team Members sub page when accessed via a system user record
    /// Settings - Security - System Users - System User record - Team Member tabs
    /// </summary>
    public class SystemUserTeamMemberSubPage : CommonMethods
    {
        public SystemUserTeamMemberSubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By CWRelatedRecordPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pagehehader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Team Members']");

        readonly By addNewRecordButton = By.Id("TI_NewRecordButton");


        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");


        By recordIdentifier(string recordID) => By.XPath("//tr[@id='" + recordID + "']/td[2]");



        public SystemUserTeamMemberSubPage WaitForSystemUserTeamMemberSubPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWRelatedRecordPanel_IFrame);
            SwitchToIframe(CWRelatedRecordPanel_IFrame);

            WaitForElement(pagehehader);

            WaitForElement(addNewRecordButton);

            WaitForElement(viewsPicklist);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);

            return this;
        }

        /// <summary>
        /// use this method to switch the focus to the Iframe that holds the Dynamic Dialog popup.
        /// In the case of System User Team Member sub page the Iframe path is CWContentIFrame -> iframe_CWDialog_
        /// </summary>
        /// <returns></returns>
        public SystemUserTeamMemberSubPage SwitchToDynamicsDialogLevelIframe()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            return this;
        }

        public SystemUserTeamMemberSubPage OpenContributionRecord(string RecordID)
        {
            Click(recordIdentifier(RecordID));

            return new SystemUserTeamMemberSubPage(driver, Wait, appURL);
        }

        public SystemUserTeamMemberSubPage ClickAddNewButton()
        {
            this.WaitForElement(addNewRecordButton);
            this.Click(addNewRecordButton);

            return new SystemUserTeamMemberSubPage(driver, Wait, appURL);
        }

    }
}
