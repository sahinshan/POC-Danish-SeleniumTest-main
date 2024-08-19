using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AllRoleApplicationsPage : CommonMethods
    {

        public AllRoleApplicationsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By readyForInduction = By.XPath("//span[text()='Ready for induction']");
        readonly By applicantsPending = By.XPath("//span[text()='Applicants pending']");

        readonly By contentIFrame = By.Id("CWContentIFrame");
        
        readonly By applicantsPendingFrame = By.XPath("//iframe[contains(@src, '&type=applicant&')]");
        readonly By readyForInductionFrame = By.XPath("//iframe[contains(@src, '&type=systemuser&')]");

        readonly By loadMoreRecords_Link = By.XPath("//button[@id='btn-load-more']");
        By readyForInductionRow(string rowId) => By.XPath("//*[@id='CWListViewContainer']//div[" + rowId + "]//ul/li[1]/p");

        By Applicant_SystemUser_FullName(string FullName) => By.XPath("//*[@id='CWListViewContainer']//div[@class='list-item']//p/span[text()='" + FullName + "']");

        public AllRoleApplicationsPage WaitForAllRoleApplicationsToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementVisible(readyForInduction);
            WaitForElementVisible(applicantsPending);

            return this;
        }

        public AllRoleApplicationsPage SwitchToReadyForInductionFrame()
        {
            WaitForElement(readyForInductionFrame);
            SwitchToIframe(readyForInductionFrame);

            return this;
        }

        public AllRoleApplicationsPage SwitchToApplicantsPendingFrame()
        {
            WaitForElement(applicantsPendingFrame);
            SwitchToIframe(applicantsPendingFrame);

            return this;
        }

        public AllRoleApplicationsPage ValidateSystemUserFullNameInReadyForInductionSection(string systeUserFullName, bool IsPresent)
        {
            if (IsPresent)
            {
                while (GetElementVisibility(loadMoreRecords_Link))
                {
                    if (GetElementVisibility(Applicant_SystemUser_FullName(systeUserFullName)) == false)
                    {
                        MoveToElementInPage(loadMoreRecords_Link);
                        Click(loadMoreRecords_Link);
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                        break;
                }
                WaitForElementVisible(Applicant_SystemUser_FullName(systeUserFullName));
            }
            else
                WaitForElementNotVisible(Applicant_SystemUser_FullName(systeUserFullName), 5);

            return this;
        }

        public AllRoleApplicationsPage ValidateApplicantFullNameInApplicantsPendingSection(string applicantFullName, bool IsPresent)
        {
            if (IsPresent)
            {
                while (GetElementVisibility(loadMoreRecords_Link))
                {
                    if (GetElementVisibility(Applicant_SystemUser_FullName(applicantFullName)) == false)
                    {
                        Click(loadMoreRecords_Link);
                        System.Threading.Thread.Sleep(1000);
                    }
                    else
                        break;
                }
                WaitForElementVisible(Applicant_SystemUser_FullName(applicantFullName));
            }
            else
                WaitForElementNotVisible(Applicant_SystemUser_FullName(applicantFullName), 5);

            return this;
        }

        public AllRoleApplicationsPage VerifyNameForReadyForInductionTable(string rownum, string applicantFullName)
        {
            WaitForElement(readyForInductionRow(rownum));
            ScrollToElement(readyForInductionRow(rownum));
            string name = GetElementText(readyForInductionRow(rownum));
            Assert.AreEqual(name, "Name: " + applicantFullName);

            return this;
        }

        public AllRoleApplicationsPage VerifyNameForApplicantsPendingTable(string rownum, string applicantFullName)
        {
            WaitForElement(readyForInductionRow(rownum));
            ScrollToElement(readyForInductionRow(rownum));
            string name = GetElementText(readyForInductionRow(rownum));
            Assert.AreEqual(name, "Full Name: " + applicantFullName);

            return this;
        }

    }
}