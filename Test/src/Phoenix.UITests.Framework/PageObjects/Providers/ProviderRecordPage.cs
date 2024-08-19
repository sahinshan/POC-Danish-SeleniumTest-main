
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ProviderRecordPage : CommonMethods
    {
        public ProviderRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ProviderRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=provider&')]");

        readonly By ProviderPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        #region Navigation Area

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");

        readonly By timelineSection = By.XPath("//li[@id='CWNavGroup_Timeline']/a[@title='Timeline']");
        readonly By summarySection = By.XPath("//li[@id='CWNavGroup_SummaryDashboard']/a[@title='Summary']");
        readonly By detailsSection = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");
        readonly By servicesProvidedSection = By.XPath("//li[@id='CWNavGroup_ServicesProvided']/a[@title='Services Provided']");
        readonly By approvedCareTypesSection = By.XPath("//li[@id = 'CWNavGroup_ApprovedCareTypes']/a[@title = 'Approved Care Types']");
        readonly By financeTransactionsSection = By.XPath("//li/a[@title='Finance Transactions']");
        readonly By serviceProvisionsSection = By.XPath("//li[@id='CWNavGroup_ServiceProvisions']/a[@title='Service Provisions']");


        #region Left Sub Menu


        readonly By activitiesDetailsElementExpanded = By.XPath("//span[text()='Activities']/parent::div/parent::summary/parent::details[@open]");
        readonly By activitiesLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_Activities']/a");

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_RelatedItems']/a");

        readonly By otherInformationDetailsElementExpanded = By.XPath("//span[text()='Other Information']/parent::div/parent::summary/parent::details[@open]");
        readonly By otherInformationLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_OtherInformation']/a");

        readonly By qualityAssuranceDetailsElementExpanded = By.XPath("//span[text()='Quality Assurance']/parent::div/parent::summary/parent::details[@open]");
        readonly By qualityAssuranceLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_QualityAssurance']/a");

        readonly By commissioningDetailsElementExpanded = By.XPath("//span[text()='Commissioning']/parent::div/parent::summary/parent::details[@open]");
        readonly By commissioningLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_Commissioning']/a");

        #region Activities

        readonly By caseNotestLeftSubMenuItem = By.Id("CWNavItem_ProviderCaseNote");
        readonly By emailstLeftSubMenuItem = By.Id("CWNavItem_Email");
        readonly By WebsiteMessagesLeftSubMenuItem = By.Id("CWNavItem_WebsiteMessage");
        readonly By ProviderFormLeftSubMenuItem = By.Id("CWNavItem_ProviderForm");

        #endregion

        #region Quality Assurance

        readonly By ComplaintFeedbackLeftSubMenuItem = By.Id("CWNavItem_ProviderComplaintFeedback");

        #endregion

        #region Commissioning

        readonly By feestLeftSubMenuItem = By.Id("CWNavItem_Fees");
        readonly By financeTransactionSubMenuItem = By.Id("CWNavItem_FinanceTransaction");

        #endregion

        #endregion

        #endregion


        public ProviderRecordPage WaitForProviderRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ProviderRecordIFrame);
            SwitchToIframe(ProviderRecordIFrame);

            WaitForElement(MenuButton);
            WaitForElement(timelineSection);
            WaitForElement(summarySection);

            return this;
        }

        public ProviderRecordPage WaitForProviderRecordPageToLoadFromHyperlink(string ProviderName)
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ProviderPageHeader);
            ValidateElementText(ProviderPageHeader, "Provider: " + ProviderName);

            WaitForElement(MenuButton);
            WaitForElement(timelineSection);
            WaitForElement(summarySection);

            return this;
        }

        public ProviderRecordPage NavigateToProviderCaseNotesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(activitiesDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(activitiesLeftSubMenu);
                Click(activitiesLeftSubMenu);
            }
            
            WaitForElementToBeClickable(caseNotestLeftSubMenuItem);
            Click(caseNotestLeftSubMenuItem);

            return this;
        }

        public ProviderRecordPage NavigateToEmailsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(activitiesDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(activitiesLeftSubMenu);
                Click(activitiesLeftSubMenu);
            }

            WaitForElementToBeClickable(emailstLeftSubMenuItem);
            Click(emailstLeftSubMenuItem);

            return this;
        }

        public ProviderRecordPage NavigateToComplaintsAndFeedbackPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(qualityAssuranceDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(qualityAssuranceLeftSubMenu);
                Click(qualityAssuranceLeftSubMenu);
            }
            
            WaitForElementToBeClickable(ComplaintFeedbackLeftSubMenuItem);
            Click(ComplaintFeedbackLeftSubMenuItem);

            return this;
        }

        public ProviderRecordPage NavigateToFeesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(commissioningDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(commissioningLeftSubMenu);
                Click(commissioningLeftSubMenu);
            }

            WaitForElementToBeClickable(feestLeftSubMenuItem);
            Click(feestLeftSubMenuItem);

            return this;
        }

        public ProviderRecordPage NavigateToFinanceTransactionsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(commissioningDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(commissioningLeftSubMenu);
                Click(commissioningLeftSubMenu);
            }

            WaitForElementToBeClickable(financeTransactionSubMenuItem);
            Click(financeTransactionSubMenuItem);

            return this;
        }

        public ProviderRecordPage NavigateToWebsiteMessagesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(activitiesDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(activitiesLeftSubMenu);
                Click(activitiesLeftSubMenu);
            }

            WaitForElementToBeClickable(WebsiteMessagesLeftSubMenuItem);
            Click(WebsiteMessagesLeftSubMenuItem);

            return this;
        }

        public ProviderRecordPage NavigateToProviderFormsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(ProviderFormLeftSubMenuItem);
            Click(ProviderFormLeftSubMenuItem);

            return this;
        }

        public ProviderRecordPage TapSummaryTab()
        {
            WaitForElementToBeClickable(summarySection);
            ScrollToElement(financeTransactionsSection);
            Click(summarySection);

            return this;
        }

        public ProviderRecordPage TapTimeLineTab()
        {
            WaitForElementToBeClickable(timelineSection);
            ScrollToElement(financeTransactionsSection);
            Click(timelineSection);

            return this;
        }

        public ProviderRecordPage TapServicesProvidedTab()
        {
            ScrollToElement(servicesProvidedSection);
            WaitForElementToBeClickable(servicesProvidedSection);
            Click(servicesProvidedSection);

            return this;
        }

        public ProviderRecordPage TapDetailsTab()
        {
            WaitForElementToBeClickable(detailsSection);
            ScrollToElement(detailsSection);
            Click(detailsSection);

            return this;
        }

        public ProviderRecordPage TapApprovedCareTypesTab()
        {
            ScrollToElement(approvedCareTypesSection);
            WaitForElementToBeClickable(approvedCareTypesSection);
            Click(approvedCareTypesSection);

            return this;
        }

        public ProviderRecordPage TapFinanceTransactionsTab()
        {
            WaitForElementToBeClickable(financeTransactionsSection);
            ScrollToElement(financeTransactionsSection);
            Click(financeTransactionsSection);

            return this;
        }

        public ProviderRecordPage TapServiceProvisionsTab()
        {
            WaitForElementToBeClickable(serviceProvisionsSection);
            ScrollToElement(serviceProvisionsSection);
            Click(serviceProvisionsSection);

            return this;
        }

    }
}
