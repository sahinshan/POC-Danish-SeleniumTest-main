using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class UserTransportationSchedule : CommonMethods
    {
        public UserTransportationSchedule(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By contentIFrame = By.Id("CWContentIFrame");
        By iframe_CWDialog_SubFrame(string frameId) => By.Id("iframe_CWDialog_" + frameId);

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Advanced Search']");

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");

        readonly By auditHistory = By.Id("CWNavItem_AuditHistory");

        readonly By titleLabel = By.Id("CWLabelHolder_title");
        readonly By resultContainer = By.Id("CWResultsContainer");
        readonly By iframe_AuditHistory = By.Id("CWUrlPanel_IFrame");

        By AuditListOperationStatus(string status) => By.XPath("//*[@id='CWResultsContainer']//*[@id='CWGrid']//tr/td[@title='" + status + "']");

        By recordRow(string recordID) => By.XPath("//*[@id='" + recordID + "']/td[2]");

        #region

        public UserTransportationSchedule WaitForUserTransportationSchedulePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(pageHeader);          

            return this;
        }

        public UserTransportationSchedule WaitForResultsPageToLoad(string frameId)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog_SubFrame(frameId));
            SwitchToIframe(iframe_CWDialog_SubFrame(frameId));

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(titleLabel);

            return this;
        }

        public UserTransportationSchedule NavigateToRelatedItemsSubPage_Audit()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            WaitForElement(auditHistory);
            Click(auditHistory);

            return this;
        }

        #endregion

        #region Audit History

        public UserTransportationSchedule WaitForAuditListPageToLoad(string frameId)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_CWDialog_SubFrame(frameId));
            SwitchToIframe(iframe_CWDialog_SubFrame(frameId));

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_AuditHistory);
            SwitchToIframe(iframe_AuditHistory);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(resultContainer);

            return this;
        }

        public UserTransportationSchedule ValidateAuditRecordOnAuditList(string status)
        {
            WaitForElement(AuditListOperationStatus(status));
            MoveToElementInPage(AuditListOperationStatus(status));
            bool availabilityCardDisplayed = GetElementVisibility(AuditListOperationStatus(status));
            Assert.IsTrue(availabilityCardDisplayed);

            return this;
        }

        #endregion Audit History Region End

    }
}
