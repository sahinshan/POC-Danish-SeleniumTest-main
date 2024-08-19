using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.QualityAndCompliance
{
    public class SystemUserRecruitmentDashboardPage : CommonMethods
    {
        public SystemUserRecruitmentDashboardPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ApplicantRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageTitle = By.XPath("//*[@id='RW-header']/h1");

        readonly By RefreshButton = By.XPath("//button[@class='mcc-button mcc-button--primary']/span[text()='Refresh']");

        By roleApplicationTitle(int position) => By.XPath("//*[@id='RW-content']/div["+ position + "]/div/div/h2");

        By inductionReadinessPercentage(int position) => By.XPath("//*[@id='RW-content']/*["+ position + "]/*/*[7]/*/span[2]");
        
        By fullyAcceptedPercentage(int position) => By.XPath("//*[@id='RW-content']/*[" + position + "]/*/*[8]/*/span[2]");

        By proceedToFullyAcceptedButton(int position) => By.XPath("//*[@id='RW-content']/div[" + position + "]/div/div/button[contains(@onclick, 'ProceedButtonAction')]");

        By processToInductionButton(int position) => By.XPath("//*[@id='RW-content']/div[" + position + "]/div/div/button[contains(@onclick, 'ProceedButtonAction')]");

        By statusField(int position) => By.XPath("//*[@id='RW-content']/div["+ position + "]/div/div[1]/div[2]/span");

        By expandSection(string sectionName) => By.XPath("//span[@class='mcc-accordion__label'][text()='" + sectionName + "']//parent::summary//*[@name='expand_arrow']");

        By UnsatisfiedLink(string RecordId) => By.XPath("//div[@class='cd-accordion-inner']/span/a[contains(@onclick,'" + RecordId + "')]");

        public SystemUserRecruitmentDashboardPage WaitForSystemUserRecruitmentDashboardPagePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ApplicantRecordIFrame);
            SwitchToIframe(ApplicantRecordIFrame);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(pageTitle);

            return this;
        }

        public SystemUserRecruitmentDashboardPage WaitForRecruitmentWidgetPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(pageTitle);

            return this;
        }

        public SystemUserRecruitmentDashboardPage ValidateRoleApplicationTitleText(int Position, string ExpectedText)
        {
            WaitForElementVisible(roleApplicationTitle(Position));
            ValidateElementText(roleApplicationTitle(Position), ExpectedText);

            return this;
        }

        public SystemUserRecruitmentDashboardPage ValidateInductionReadinessPercentageText(int Position, string ExpectedText)
        {
            WaitForElementVisible(inductionReadinessPercentage(Position));
            ValidateElementText(inductionReadinessPercentage(Position), ExpectedText);

            return this;
        }

        public SystemUserRecruitmentDashboardPage ValidateFullyAcceptedPercentageText(int Position, string ExpectedText)
        {
            WaitForElementVisible(fullyAcceptedPercentage(Position));
            ValidateElementText(fullyAcceptedPercentage(Position), ExpectedText);

            return this;
        }

        public SystemUserRecruitmentDashboardPage ClickProceedToFullyAcceptedButton(int ButtonPosition)
        {
            WaitForElementToBeClickable(proceedToFullyAcceptedButton(ButtonPosition));
            Click(proceedToFullyAcceptedButton(ButtonPosition));

            return this;
        }

        public SystemUserRecruitmentDashboardPage ValidateStatusFieldText(int ButtonPosition, string ExpectedText)
        {
            WaitForElementVisible(statusField(ButtonPosition));
            ValidateElementText(statusField(ButtonPosition), ExpectedText);

            return this;
        }

        public SystemUserRecruitmentDashboardPage ClickProcessToInductionButton(int ButtonPosition)
        {
            WaitForElementToBeClickable(processToInductionButton(ButtonPosition));
            Click(processToInductionButton(ButtonPosition));

            return this;
        }

        public SystemUserRecruitmentDashboardPage ExpandUnsatisfiedRequirementsForInductionSection()
        {
            WaitForElementToBeClickable(expandSection("Unsatisfied Requirements For Induction"));
            Click(expandSection("Unsatisfied Requirements For Induction"));

            return this;
        }

        public SystemUserRecruitmentDashboardPage ExpandUnsatisfiedRequirementsForFullyAcceptedSection()
        {
            WaitForElementToBeClickable(expandSection("Unsatisfied Requirements For Fully Accepted"));
            Click(expandSection("Unsatisfied Requirements For Fully Accepted"));

            return this;
        }

        public SystemUserRecruitmentDashboardPage ValidateUnsatisfiedLinkVisible(string recordId, bool IsVisible)
        {
            if (IsVisible)
                WaitForElementVisible(UnsatisfiedLink(recordId));
            else
                WaitForElementNotVisible(UnsatisfiedLink(recordId), 10);

            return this;
        }

        public SystemUserRecruitmentDashboardPage ValidateUnsatisfiedText(string recordId, string ExpectedText)
        {
            WaitForElementVisible(UnsatisfiedLink(recordId));
            ValidateElementText(UnsatisfiedLink(recordId), ExpectedText);

            return this;
        }


        public SystemUserRecruitmentDashboardPage ClickUnsatisfiedLinkText(string recordId)
        {
            WaitForElementToBeClickable(UnsatisfiedLink(recordId));
            MoveToElementInPage(UnsatisfiedLink(recordId));
            Click(UnsatisfiedLink(recordId));

            return this;
        }

        public SystemUserRecruitmentDashboardPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(RefreshButton);
            Click(RefreshButton);

            return this;
        }
    }
}
