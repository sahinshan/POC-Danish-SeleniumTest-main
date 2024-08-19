using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.QualityAndCompliance
{
    public class ApplicantDashboardPage : CommonMethods
    {
        public ApplicantDashboardPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ApplicantRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=applicant&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageTitle = By.XPath("//*[@id='RW-header']/h1");

        readonly By Availability_Tab = By.Id("CWNavGroup_WorkScheduleAdvanced");

        By roleApplicationTitle(int position) => By.XPath("//*[@id='RW-content']/div["+ position + "]/div/div/h2");

        readonly By ApplicationsArea = By.XPath("//*[@id = 'RW-content']");

        readonly By refreshButton = By.XPath("//button/span[text() = 'Refresh']");

        readonly By newApplicationButton = By.XPath("//*[@id='new-application']");

        readonly By unsatisfiedLabel = By.XPath("//div[@class='cd-body-accordion']//span[@class='mcc-accordion__label']");
        By expandSection(string sectionName) => By.XPath("//div[@class='cd-body-accordion']//span[@class='mcc-accordion__label'][text()='" + sectionName + "']//parent::summary//*[@name='expand_arrow']");

        By UnsatisfiedLink(string RecordId) => By.XPath("//div[@class='cd-accordion-inner']/span/a[contains(@onclick,'" + RecordId + "')]");

        By inductionReadinessPercentage(int position) => By.XPath("//*[@id='RW-content']/*["+ position + "]/*/*[7]/*/span[2]");
        
        By fullyAcceptedPercentage(int position) => By.XPath("//*[@id='RW-content']/*[" + position + "]/*/*[8]/*/span[2]");

        By proceedToInductionButton(int position) => By.XPath("//*[@id='RW-content']/div[" + position + "]/div/div/button[contains(@onclick, 'ProceedButtonAction')]");

        By attributeHeader(int position) => By.XPath("//*[@id='RW-content']/div[1]/div[contains(@class,'cd-widget-body-headers')]/div[" + position + "]/div[@class='cd-header-title']/span");
        
        By attributeValue(int position) => By.XPath("//*[@id='RW-content']/div[1]/div[contains(@class,'cd-widget-body-headers')]/div[" + position + "]/div[@class='cd-header-value']/span");


        public ApplicantDashboardPage WaitForApplicantDashboardPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(ApplicantRecordIFrame);
            SwitchToIframe(ApplicantRecordIFrame);
            
            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(pageTitle);

            return this;
        }

        public ApplicantDashboardPage ValidateRoleApplicationTitleText(int Position, string ExpectedText)
        {
            WaitForElementVisible(roleApplicationTitle(Position));
            ValidateElementText(roleApplicationTitle(Position), ExpectedText);

            return this;
        }

        public ApplicantDashboardPage ValidateRoleApplicationNotVisible(int Position)
        {
            WaitForElementNotVisible(roleApplicationTitle(Position), 7);

            return this;
        }

        public ApplicantDashboardPage ValidateInductionReadinessPercentageText(int Position, string ExpectedText)
        {
            WaitForElementVisible(inductionReadinessPercentage(Position));
            ValidateElementText(inductionReadinessPercentage(Position), ExpectedText);

            return this;
        }

        public ApplicantDashboardPage ValidateFullyAcceptedPercentageText(int Position, string ExpectedText)
        {
            WaitForElementVisible(fullyAcceptedPercentage(Position));
            ValidateElementText(fullyAcceptedPercentage(Position), ExpectedText);

            return this;
        }

        public ApplicantDashboardPage ClickProceedToInductionButton(int ButtonPosition)
        {
            WaitForElementToBeClickable(proceedToInductionButton(ButtonPosition));
            Click(proceedToInductionButton(ButtonPosition));

            return this;
        }

        public ApplicantDashboardPage ClickNewApplicationButton()
        {
            WaitForElementToBeClickable(newApplicationButton);
            Click(newApplicationButton);

            return this;
        }

        public ApplicantDashboardPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            ScrollToElement(refreshButton);
            Click(refreshButton);

            return this;
        }

        public ApplicantDashboardPage ValidateProceedToInductionButtonIsDisabled(int ButtonPosition, bool ExpectedDisabled)
        {
            WaitForElement(proceedToInductionButton(ButtonPosition));
            ScrollToElement(proceedToInductionButton(ButtonPosition));
            if (ExpectedDisabled)
                ValidateElementDisabled(proceedToInductionButton(ButtonPosition));
            else
                ValidateElementNotDisabled(proceedToInductionButton(ButtonPosition));
            return this;
        }


        public ApplicantDashboardPage ValidateAttributeTitleAndValueText(int Position, string AttributeTitle, string AttributeValue)
        {
            WaitForElementVisible(attributeHeader(Position));
            ValidateElementText(attributeHeader(Position), AttributeTitle);

            WaitForElementVisible(attributeValue(Position));
            ValidateElementText(attributeValue(Position), AttributeValue);

            return this;
        }

        public ApplicantDashboardPage ValidateUnsatisfiedRequirementsForInductionLabel()
        {
            WaitForElementVisible(unsatisfiedLabel);
            ValidateElementText(unsatisfiedLabel, "Unsatisfied Requirements For Induction");

            return this;
        }

        public ApplicantDashboardPage ValidateUnsatisfiedRequirementsForFullyAcceptedLabel()
        {
            WaitForElementVisible(unsatisfiedLabel);
            ValidateElementText(unsatisfiedLabel, "Unsatisfied Requirements For Fully Accepted");

            return this;
        }

        public ApplicantDashboardPage ExpandUnsatisfiedRequirementsForInductionSection()
        {
            WaitForElementToBeClickable(expandSection("Unsatisfied Requirements For Induction"));
            Click(expandSection("Unsatisfied Requirements For Induction"));

            return this;
        }

        public ApplicantDashboardPage ExpandUnsatisfiedRequirementsForFullyAcceptedSection()
        {
            WaitForElementToBeClickable(expandSection("Unsatisfied Requirements For Fully Accepted"));
            Click(expandSection("Unsatisfied Requirements For Fully Accepted"));

            return this;
        }

        public ApplicantDashboardPage ValidateApplicationsAreaVisible()
        {
            WaitForElementVisible(ApplicationsArea);
            return this;
        }

        public ApplicantDashboardPage ValidateRefreshButtonVisible(bool IsVisible)
        {
            if (IsVisible)
                WaitForElementVisible(refreshButton);
            else
                WaitForElementNotVisible(refreshButton, 3);
            return this;
        }

        public ApplicantDashboardPage ValidateUnsatisfiedLinkVisible(string recordId, bool IsVisible)
        {
            if (IsVisible)
                WaitForElementVisible(UnsatisfiedLink(recordId));
            else
                WaitForElementNotVisible(UnsatisfiedLink(recordId), 10);

            return this;
        }

        public ApplicantDashboardPage ValidateUnsatisfiedText(string recordId, string ExpectedText)
        {
            WaitForElementVisible(UnsatisfiedLink(recordId));
            ValidateElementText(UnsatisfiedLink(recordId), ExpectedText);

            return this;
        }

        public ApplicantDashboardPage ClickUnsatisfiedLinkText(string recordId)
        {
            WaitForElementToBeClickable(UnsatisfiedLink(recordId));
            ScrollToElement(UnsatisfiedLink(recordId));
            Click(UnsatisfiedLink(recordId));

            return this;
        }

    }
}
