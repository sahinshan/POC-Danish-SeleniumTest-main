using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class MainMenu : CommonMethods
    {
        public MainMenu(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Main Menu

        readonly By homeMenuLink = By.XPath("//*[@id='HomeButton']");
        readonly By peopleMenuLink = By.XPath("//*[@id='mcc-nav-rail']//button[@title='People']");
        readonly By staffMenuLink = By.XPath("//*[@id='mcc-nav-rail']//button[@title='Staff']");
        readonly By rosteringMenuLink = By.XPath("//*[@id='mcc-nav-rail']//button[@title='Rostering']");
        readonly By reportingMenuLink = By.XPath("//*[@id='mcc-nav-rail']//button[@title='Reporting']");
        readonly By adminMenuLink = By.XPath("//*[@id='mcc-nav-rail']//button[@title='Admin']");
        readonly By financeMenuLink = By.XPath("//*[@id='mcc-nav-rail']//button[@title='Finance']");
        readonly By settingsMenuLink = By.XPath("//*[@id='mcc-nav-rail']//button[@title='Settings']");

        readonly By advancedSearchMenuLink = By.XPath("//*[@id='navbar']/*/*/*/a[@title='Advanced Search']");
        readonly By personSearchMenuLink = By.XPath("//*[@id='navbar']/*/*/*/a[@title='Person Search']");
        readonly By pinnedRecordsMenuLink = By.XPath("//*[@id='navbar']/*/*/*/a[@title='Pinned Records']");
        readonly By recentlyViewdMenuLink = By.XPath("//*[@id='navbar']/*/*/*/a[@title='Recently Viewed']");
        readonly By userProfileButton = By.XPath("//ul/li[@class='nav-item dropdown nav-user-profile']/a");
        readonly By userDefaultTeam = By.XPath("//*[@id='CWTeamName']");

        #endregion

        #region Recently Viewed Area

        readonly By RecentlyViewdArea = By.XPath("//*[@id='CWRecentlyViewed']");
        readonly By RecentlyViewdAreaHeader = By.XPath("//*[@id='CWRecentlyViewed']/div/span[text()='RECENTLY VIEWED RECORDS']");
        By RecentlyViewdAreaLinkElement(string ElementText) => By.XPath("//*[@id='CWRecentlyViewed']/div/a[@title='" + ElementText + "']");
        By RecentlyViewdAreaLinkElement(string ElementText, string ElementId) => By.XPath("//*[@id='CWRecentlyViewed']/div/a[@title='" + ElementText + "'][contains(@onclick,'id=" + ElementId + "')]");


        #endregion

        #region People Sub Menu

        readonly By casesSubLink = By.XPath("//details//button//span[text()='Cases']");
        readonly By peopleSubLink = By.XPath("//details//button//span[text()='People']");

        #endregion

        #region Staff Sub Menu

        readonly By AllRoleApplicationsPageLink = By.XPath("//details//button//span[text()='All Role Applications']");
        readonly By ApplicantsPageLink = By.XPath("//details//button//span[text()='Applicants']");
        readonly By professionalsSubLink = By.XPath("//details//button//span[text()='Professionals']");
        readonly By providersSubLink = By.XPath("//details//button//span[text()='Providers']");
        readonly By staffContractsSubLink = By.XPath("//details//button//span[text()='Staff Contracts']");
        readonly By staffReviewSubLink = By.XPath("//details//button//span[text()='Staff Reviews']");

        #endregion

        #region Rostering Sub Menu

        readonly By DiaryBookingsSubLink = By.XPath("//details//button//span[text()='Diary Bookings']");
        readonly By expressBookSubLink = By.XPath("//details//button//span[text()='Express Book']");
        readonly By peopleDiarySubLink = By.XPath("//details//button//span[text()='People Diary']");
        readonly By peoplecheduleSubLink = By.XPath("//details//button//span[text()='People Schedule']");
        readonly By providerDiarySubLink = By.XPath("//details//button//span[text()='Provider Diary']");
        readonly By providerscheduleSubLink = By.XPath("//details//button//span[text()='Provider Schedule']");

        #endregion

        #region Reporting Sub Menu

        readonly By dashboardSubLink = By.XPath("//details//button//span[text()='Dashboard']");

        #endregion

        #region Admin

        readonly By auditListSubLink = By.XPath("//details//button//span[text()='Audit List']");
        readonly By OrganisationalRiskManagementPageLink = By.XPath("//details//button//span[text()='Organisational Risk Management']");
        readonly By ReportableEventPageLink = By.XPath("//details//button//span[text()='Reportable Events']");

        #endregion

        #region Finance Sub Menu

        readonly By financeExtractBatchesSubLink = By.XPath("//details//button//span[text()='Finance Extract Batches']");
        readonly By financeInvoiceBatchesSubLink = By.XPath("//details//button//span[text()='Finance Invoice Batches']");
        readonly By financeInvoicePaymentsSubLink = By.XPath("//details//button//span[text()='Finance Invoice Payments']");
        readonly By financeInvoicesSubLink = By.XPath("//details//button//span[text()='Finance Invoices']");
        readonly By financeTransactionsSubLink = By.XPath("//details//button//span[text()='Finance Transactions']");
        readonly By personContractServicesSubLink = By.XPath("//details//button//span[text()='Person Contract Services']");
        readonly By personContractsSubLink = By.XPath("//details//button//span[text()='Person Contracts']");
        readonly By payrollBatchesSubLink = By.XPath("//details//button//span[text()='Payroll Batches']");

        #endregion

        #region Settings Sub Menu

        #region Sections

        readonly By formsManagementDetailsElementExpanded = By.XPath("//span[text()='Forms Management']/parent::div/parent::summary/parent::details[@open]");
        readonly By formsManagementSection = By.XPath("//details/summary/div/span[text()='Forms Management']/parent::div");

        readonly By careProviderSetupDetailsElementExpanded = By.XPath("//span[text()='Care Provider Setup']/parent::div/parent::summary/parent::details[@open]");
        readonly By careProviderSetupSection = By.XPath("//details/summary/div/span[text()='Care Provider Setup']/parent::div");

        readonly By securityDetailsElementExpanded = By.XPath("//span[text()='Security']/parent::div/parent::summary/parent::details[@open]");
        readonly By securitySection = By.XPath("//details/summary/div/span[text()='Security']/parent::div");

        readonly By configurationDetailsElementExpanded = By.XPath("//span[text()='Configuration']/parent::div/parent::summary/parent::details[@open]");
        readonly By configurationSection = By.XPath("//details/summary/div/span[text()='Configuration']/parent::div");

        readonly By viewsAndDashboardsDetailsElementExpanded = By.XPath("//span[text()='Views & Dashboards']/parent::div/parent::summary/parent::details[@open]");
        readonly By viewsAndDashboardsSection = By.XPath("//details/summary/div/span[text()='Views & Dashboards']/parent::div");

        readonly By portalsDetailsElementExpanded = By.XPath("//span[text()='Portals']/parent::div/parent::summary/parent::details[@open]");
        readonly By portalsSection = By.XPath("//a[@title='Portals'][text()='Portals']/parent::div");

        #endregion

        #region Forms Management Section

        readonly By documentsSubLink = By.XPath("//details//button//span[text()='Documents']");

        #endregion

        #region Care Provider Setup Section

        readonly By AvailabilityTypesSubLink = By.XPath("//details//button//span[text()='Availability Types']");
        readonly By bookingTypesSubLink = By.XPath("//details//button//span[text()='Booking Types']");
        readonly By recruitmentRequirementSetupSubLink = By.XPath("//details//button//span[text()='Recruitment Requirement Setup']");
        readonly By riskCategoriesManagementSubLink = By.XPath("//details//button//span[text()='Risk Categories Management']");
        readonly By schedulingSetupSubLink = By.XPath("//details//button//span[text()='Scheduling Setup']");
        readonly By staffReviewRequirementsSubLink = By.XPath("//details//button//span[text()='Staff Review Requirements']");
        readonly By trainingCourseSetupSubLink = By.XPath("//details//button//span[text()='Training Course Setup']");
        readonly By trainingRequirementSetupSubLink = By.XPath("//details//button//span[text()='Training Requirement Setup']");

        #endregion

        #region Security Section

        readonly By coreUsersSubLink = By.XPath("//details//button//span[text()='Core Users']");
        readonly By providerUsersSubLink = By.XPath("//details//button//span[text()='Provider Users']");
        readonly By rosteredUsersSubLink = By.XPath("//details//button//span[text()='Rostered Users']");
        readonly By securityProfileSubLink = By.XPath("//details//button//span[text()='Security Profiles']");
        readonly By systemUsersSubLink = By.XPath("//details//button//span[text()='System Users']");
        readonly By teamsSubLink = By.XPath("//details//button//span[text()='Teams']");

        #endregion

        #region Configuration Section

        readonly By aboutMeSetupSubLink = By.XPath("//details//button//span[text()='About Me Setup']");
        readonly By customizationsSubLink = By.XPath("//details//button//span[text()='Customizations']");
        readonly By DataManagementSubLink = By.XPath("//details//button//span[text()='Data Management']");
        readonly By FAQsSubLink = By.XPath("//details//button//span[text()='FAQs']");
        readonly By financeDataSubLink = By.XPath("//details//button//span[text()='Finance Admin']");
        readonly By HealthSetUpSubLink = By.XPath("//details//button//span[text()='Health Setup']");
        readonly By referenceDataSubLink = By.XPath("//details//button//span[text()='Reference Data']");
        readonly By SystemManagementSubLink = By.XPath("//details//button//span[text()='System Management']");
        readonly By WorkflowsSubLink = By.XPath("//details//button//span[text()='Workflows']");

        #endregion

        #region Views & Dashboards Section

        readonly By SummaryDashboardsSubLink = By.XPath("//details//button//span[text()='Summary Dashboards']");
        readonly By SystemDashboardsSubLink = By.XPath("//details//button//span[text()='System Dashboards']");
        readonly By UserChartsSubLink = By.XPath("//details//button//span[text()='User Charts']");
        readonly By UserDashboardsSubLink = By.XPath("//details//button//span[text()='User Dashboards']");

        #endregion

        #region Portals

        readonly By WebsiteContactsSubLink = By.XPath("//details//button//span[text()='Website Contacts']");
        readonly By WebsiteUserSubLink = By.XPath("//details//button//span[text()='Website Users']");
        readonly By WebSitesSubLink = By.XPath("//details//button//span[text()='Websites']");

        #endregion

        #endregion

        #region User Profile Dropdown

        readonly By FAQLink = By.XPath("//*[@id='navbar']/*/*/*/*/a[text()='FAQ']");
        readonly By ChangeDefaultTeamLink = By.XPath("//*[@id='navbar']/*/*/*/*/a[text()='Change Default Team']");
        readonly By signOutLink = By.XPath("//*[@id='navbar']/*/*/*/*/a[contains(@onclick, 'Logout')]");

        #endregion


        /**CDV6 LOCATORS**/

        readonly By serviceProvisionSubLink = By.XPath("//details//button//span[text()='Service Provisions']");
        readonly By extractsSubLink = By.XPath("//details//button//span[text()='Extracts']");
        readonly By invoiceBatchesSubLink = By.XPath("//details//button//span[text()='Invoice Batches']");

        readonly By dataRestrictionsSubLink = By.XPath("//a[@title='Data Restrictions']/span[text()='Data Restrictions']");
        readonly By staffReviewsSubLink = By.XPath("//a[@title='Staff Reviews']/span[text()='Staff Reviews']");

        readonly By formsCaseSubLink = By.XPath("//details//button//span[text()='Forms (Case)']");
        readonly By healthDiarySubLink = By.XPath("//details//button//span[text()='Health Diary']");

        readonly By QualityAndComplianceMenuLink = By.XPath("//a[@title = 'Quality and Compliance']");
        readonly By RecruitmentMenuLink = By.XPath("//a[@title = 'Recruitment']");

        /**CDV6 LOCATORS**/

        

        private void ExpandFormsManagementSection()
        {
            if (!CheckIfElementExists(formsManagementDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(formsManagementSection);
                Click(formsManagementSection);
            }
        }

        private void ExpandCareProviderSetupSection()
        {
            if (!CheckIfElementExists(careProviderSetupDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(careProviderSetupSection);
                Click(careProviderSetupSection);
            }
        }

        private void ExpandSecuritySection()
        {
            if (!CheckIfElementExists(securityDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(securitySection);
                Click(securitySection);
            }
        }

        private void ExpandConfigurationSection()
        {
            if (!CheckIfElementExists(configurationDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(configurationSection);
                Click(configurationSection);
            }
        }

        private void ExpandViewsAndDashboardsSection()
        {
            if (!CheckIfElementExists(viewsAndDashboardsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(viewsAndDashboardsSection);
                Click(viewsAndDashboardsSection);
            }
        }

        private void ExpandPortalsSection()
        {
            if (!CheckIfElementExists(portalsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(portalsSection);
                Click(portalsSection);
            }
        }

        public MainMenu WaitForMainMenuToLoad()
        {
            System.Threading.Thread.Sleep(1000);

            WaitForElementNotVisible("CWRefreshPanel", 40);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 40);

            WaitForElement(homeMenuLink);
            WaitForElement(peopleMenuLink);

            WaitForElement(settingsMenuLink);

            WaitForElement(personSearchMenuLink);
            WaitForElement(pinnedRecordsMenuLink);
            WaitForElement(recentlyViewdMenuLink);

            return this;
        }

        public MainMenu WaitForMainMenuToLoad(bool HomeMenuLinkVisible, bool PeopleMenuLinkVisible, bool SettingsMenuLinkVisible, bool PersonSearchMenuLinkVisible, bool PinnedRecordsMenuLinkVisible, bool RecentlyViewdMenuLinkVisible)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            if (HomeMenuLinkVisible)
                WaitForElement(homeMenuLink);

            if (PeopleMenuLinkVisible)
                WaitForElement(peopleMenuLink);

            if (SettingsMenuLinkVisible)
                WaitForElement(settingsMenuLink);

            if (PersonSearchMenuLinkVisible)
                WaitForElement(personSearchMenuLink);

            if (PinnedRecordsMenuLinkVisible)
                WaitForElement(pinnedRecordsMenuLink);

            if (RecentlyViewdMenuLinkVisible)
                WaitForElement(recentlyViewdMenuLink);

            return this;
        }

        public MainMenu WaitForRecentlyViewedAreaToLoad()
        {
            WaitForElementVisible(RecentlyViewdArea);
            WaitForElementVisible(RecentlyViewdAreaHeader);

            return this;
        }

        public MainMenu ClickStaffMenu()
        {
            WaitForElementToBeClickable(staffMenuLink);
            Click(staffMenuLink);

            return this;
        }

        public MainMenu ClickRecentlyViewdButton()
        {
            WaitForElement(recentlyViewdMenuLink);
            Click(recentlyViewdMenuLink);

            return this;
        }

        public MainMenu ClickPersonSearchButton()
        {
            WaitForElement(personSearchMenuLink);
            Click(personSearchMenuLink);

            return this;
        }

        public MainMenu ClickAdvancedSearchButton()
        {
            WaitForElement(advancedSearchMenuLink);
            Click(advancedSearchMenuLink);

            return this;
        }

        /// <summary>
        /// Access the "People -> Cases" section
        /// </summary>
        public MainMenu NavigateToCasesSection()
        {
            WaitForElementToBeClickable(peopleMenuLink);
            Click(peopleMenuLink);

            WaitForElementToBeClickable(casesSubLink);
            Click(casesSubLink);

            return this;
        }

        public MainMenu NavigateToProvidersSection()
        {
            WaitForElementToBeClickable(staffMenuLink);
            Click(staffMenuLink);

            WaitForElementToBeClickable(providersSubLink);
            Click(providersSubLink);

            return this;
        }

        public MainMenu NavigateToProviderScheduleSection()
        {
            WaitForElementToBeClickable(rosteringMenuLink);
            Click(rosteringMenuLink);

            WaitForElementToBeClickable(providerscheduleSubLink);
            Click(providerscheduleSubLink);

            return this;
        }

        public MainMenu NavigateToExpressBookSection()
        {
            WaitForElementToBeClickable(rosteringMenuLink);
            Click(rosteringMenuLink);

            WaitForElementToBeClickable(expressBookSubLink);
            Click(expressBookSubLink);

            return this;
        }

        public MainMenu NavigateToPeopleScheduleSection()
        {
            WaitForElementToBeClickable(rosteringMenuLink);
            Click(rosteringMenuLink);

            WaitForElementToBeClickable(peoplecheduleSubLink);
            Click(peoplecheduleSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Worplace -> Cases" section
        /// </summary>
        public MainMenu NavigateToPeopleSection()
        {
            WaitForElementToBeClickable(peopleMenuLink);
            Click(peopleMenuLink);

            WaitForElementToBeClickable(peopleSubLink);
            Click(peopleSubLink);

            return this;
        }

        public MainMenu NavigateToProviderDiarySection()
        {
            WaitForElementToBeClickable(rosteringMenuLink);
            Click(rosteringMenuLink);

            WaitForElementToBeClickable(providerDiarySubLink);
            Click(providerDiarySubLink);

            return this;
        }

        public MainMenu NavigateToDiaryBookingsSection()
        {
            WaitForElementToBeClickable(rosteringMenuLink);
            Click(rosteringMenuLink);

            WaitForElementToBeClickable(DiaryBookingsSubLink);
            Click(DiaryBookingsSubLink);

            return this;
        }

        public MainMenu NavigateToPeopleDiarySection()
        {
            WaitForElementToBeClickable(rosteringMenuLink);
            Click(rosteringMenuLink);

            WaitForElementToBeClickable(peopleDiarySubLink);
            Click(peopleDiarySubLink);

            return this;
        }

        /// <summary>
        /// Access the "Worplace -> Dashboards" section
        /// </summary>
        public MainMenu NavigateToDashboardsSection()
        {
            WaitForElementToBeClickable(reportingMenuLink);
            Click(reportingMenuLink);

            WaitForElementToBeClickable(dashboardSubLink);
            Click(dashboardSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Worplace -> Professionals" section
        /// </summary>
        public MainMenu NavigateToProfessionalsSection()
        {
            WaitForElementToBeClickable(staffMenuLink);
            Click(staffMenuLink);

            WaitForElementToBeClickable(professionalsSubLink);
            Click(professionalsSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Data Restrictions" section
        /// </summary>
        public MainMenu NavigateToDataRestrictionsSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandSecuritySection();

            WaitForElementToBeClickable(dataRestrictionsSubLink);
            Click(dataRestrictionsSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Views & Dashboards - System Dashboards" section
        /// </summary>
        public MainMenu NavigateToSystemDashboardsSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandViewsAndDashboardsSection();

            WaitForElementToBeClickable(SystemDashboardsSubLink);
            Click(SystemDashboardsSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Views & Dashboards - Summary Dashboards" section
        /// </summary>
        public MainMenu NavigateToSummaryDashboardsSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandViewsAndDashboardsSection();

            WaitForElementToBeClickable(SummaryDashboardsSubLink);
            Click(SummaryDashboardsSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Portals - Websites" section
        /// </summary>
        public MainMenu NavigateToWebSitesSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandPortalsSection();

            WaitForElementToBeClickable(WebSitesSubLink);
            Click(WebSitesSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Portals - Website Contacts" section
        /// </summary>
        public MainMenu NavigateToWebsiteContactsSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandPortalsSection();

            WaitForElementToBeClickable(WebsiteContactsSubLink);
            Click(WebsiteContactsSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Portals - Website Users" Link
        /// </summary>
        public MainMenu NavigateToWebsiteUserSubLink()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandPortalsSection();

            WaitForElementToBeClickable(WebsiteUserSubLink);
            Click(WebsiteUserSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Configuration - System Management
        /// </summary>
        public MainMenu NavigateToSystemManagementSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandConfigurationSection();

            WaitForElementToBeClickable(SystemManagementSubLink);
            Click(SystemManagementSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Configuration - Workflows
        /// </summary>
        public MainMenu NavigateToWorkflowSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandConfigurationSection();

            WaitForElementToBeClickable(WorkflowsSubLink);
            Click(WorkflowsSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Views & Dashboards - User Dashboards" section
        /// </summary>
        public MainMenu NavigateToUserDashboardsSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandViewsAndDashboardsSection();

            WaitForElementToBeClickable(UserDashboardsSubLink);
            Click(UserDashboardsSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Views & Dashboards - User Charts" section
        /// </summary>
        public MainMenu NavigateToUserChartsSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandViewsAndDashboardsSection();

            WaitForElementToBeClickable(UserChartsSubLink);
            Click(UserChartsSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> System User" section
        /// </summary>
        public MainMenu NavigateToSystemUserSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandSecuritySection();

            WaitForElementToBeClickable(systemUsersSubLink);
            Click(systemUsersSubLink);

            return this;
        }

        public MainMenu NavigateToSecuritySubLink()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);
            
            System.Threading.Thread.Sleep(2000);

            ExpandSecuritySection();

            return this;
        }

        /// <summary>
        /// Access the "Setting > Core Users" Section
        /// </summary>
        public MainMenu NavigateToCoreUsersSection()
        {
            if (GetElementVisibility(coreUsersSubLink))
            {
                ScrollToElement(coreUsersSubLink);
                Click(coreUsersSubLink);
            }
            else
            {
                WaitForElementToBeClickable(settingsMenuLink);
                Click(settingsMenuLink);

                ExpandSecuritySection();

                WaitForElementToBeClickable(coreUsersSubLink);
                Click(coreUsersSubLink);
            }
            return this;
        }

        /// <summary>
        /// Access the "Setting > Provider Users" Section
        /// </summary>
        public MainMenu NavigateToProviderUsersSection()
        {
            if (GetElementVisibility(coreUsersSubLink))
            {
                ScrollToElement(providerUsersSubLink);
                Click(providerUsersSubLink);
            }
            else
            {
                WaitForElementToBeClickable(settingsMenuLink);
                Click(settingsMenuLink);

                ExpandSecuritySection();

                WaitForElementToBeClickable(providerUsersSubLink);
                Click(providerUsersSubLink);
            }
            return this;
        }

        /// <summary>
        /// Access the "Settings -> Rostered Users" section
        /// </summary>
        public MainMenu NavigateToRosteredUsersSection()
        {
            if (GetElementVisibility(rosteredUsersSubLink))
            {
                ScrollToElement(rosteredUsersSubLink);
                Click(rosteredUsersSubLink);
            }
            else
            {
                WaitForElementToBeClickable(settingsMenuLink);
                Click(settingsMenuLink);

                ExpandSecuritySection();

                ScrollToElement(rosteredUsersSubLink);
                WaitForElementToBeClickable(rosteredUsersSubLink);
                Click(rosteredUsersSubLink);
            }
            return this;
        }

        public MainMenu NavigateToSecurityProfileSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandSecuritySection();

            WaitForElementToBeClickable(securityProfileSubLink);
            Click(securityProfileSubLink);

            return this;
        }

        public MainMenu ValidateCoreUsersMenuLink(bool expectedStatus)
        {
            bool status;

            if (expectedStatus.Equals(true))
            {
                WaitForElementToBeClickable(coreUsersSubLink);
                ScrollToElement(coreUsersSubLink);
            }

            status = GetElementVisibility(coreUsersSubLink);
            Assert.AreEqual(expectedStatus, status);

            return this;
        }

        public MainMenu ValidateProviderUsersMenuLink(bool expectedStatus)
        {
            bool status;

            if (expectedStatus.Equals(true))
            {
                WaitForElementToBeClickable(providerUsersSubLink);
                ScrollToElement(providerUsersSubLink);
            }

            status = GetElementVisibility(providerUsersSubLink);
            Assert.AreEqual(expectedStatus, status);

            return this;
        }

        public MainMenu ValidateRosteredUsersMenuLink(bool expectedStatus)
        {
            bool status;

            if (expectedStatus.Equals(true))
            {
                WaitForElementToBeClickable(rosteredUsersSubLink);
                ScrollToElement(rosteredUsersSubLink);
            }

            status = GetElementVisibility(rosteredUsersSubLink);
            Assert.AreEqual(expectedStatus, status);

            return this;
        }

        public MainMenu ValidateApplicantsMenuLinkVisible(bool expectedStatus)
        {
            bool status;

            if (expectedStatus.Equals(true))
            {
                WaitForElementToBeClickable(ApplicantsPageLink);
                ScrollToElement(ApplicantsPageLink);
            }

            status = GetElementVisibility(ApplicantsPageLink);
            Assert.AreEqual(expectedStatus, status);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Reference Data" section
        /// </summary>
        public MainMenu NavigateToReferenceDataSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandConfigurationSection();

            WaitForElementToBeClickable(referenceDataSubLink);
            Click(referenceDataSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Configuration -> FAQs" section
        /// </summary>
        public MainMenu NavigateToFAQsSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandConfigurationSection();

            WaitForElementToBeClickable(FAQsSubLink);
            Click(FAQsSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Documents" section
        /// </summary>
        public MainMenu NavigateToDocumentsSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandFormsManagementSection();
            
            WaitForElementToBeClickable(documentsSubLink);
            Click(documentsSubLink);

            return this;
        }

        /// <summary>
        /// Access the Settings -> Configuration -> Finance Admin
        /// </summary>
        public MainMenu NavigateToFinanceAdminSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandConfigurationSection();

            WaitForElementToBeClickable(financeDataSubLink);
            Click(financeDataSubLink);

            return this;
        }

        public MainMenu NavigateToAuditListSection()
        {
            WaitForElementToBeClickable(adminMenuLink);
            Click(adminMenuLink);

            WaitForElementToBeClickable(auditListSubLink);
            Click(auditListSubLink);

            return this;
        }

        /// <summary>
        /// Access the Settings -> Configuration -> Finance Admin
        /// </summary>
        public MainMenu NavigateToCustomizationsSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandConfigurationSection();

            WaitForElementToBeClickable(customizationsSubLink);
            Click(customizationsSubLink);

            return this;
        }

        /// <summary>
        /// Access the Settings -> Configuration -> Data Management
        /// </summary>
        public MainMenu NavigateToDataManagementSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandConfigurationSection();

            WaitForElementToBeClickable(DataManagementSubLink);
            Click(DataManagementSubLink);

            return this;
        }

        /// <summary>
        /// Access the Settings -> Configuration -> About Me Setup
        /// </summary>
        public MainMenu NavigateToAboutMeSetupSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandConfigurationSection();

            WaitForElementToBeClickable(aboutMeSetupSubLink);
            Click(aboutMeSetupSubLink);

            return this;
        }

        public MainMenu NavigateToHealthSetUp()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandConfigurationSection();

            WaitForElementToBeClickable(HealthSetUpSubLink);
            Click(HealthSetUpSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Workplace -> Staffreview" section
        /// </summary>
        public MainMenu NavigateToStaffReviewSection()
        {
            WaitForElementToBeClickable(staffMenuLink);
            Click(staffMenuLink);

            WaitForElementToBeClickable(staffReviewSubLink);
            Click(staffReviewSubLink);

            return this;
        }

        public MainMenu NavigateToStaffReviewRequirementsSubLink()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandCareProviderSetupSection();

            WaitForElementToBeClickable(staffReviewRequirementsSubLink);
            Click(staffReviewRequirementsSubLink);

            return this;
        }

        public MainMenu ValidateToStaffReviewRequirementsSubLink(string ExpectedText)
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandCareProviderSetupSection();

            WaitForElement(staffReviewRequirementsSubLink);
            ValidateElementText(staffReviewRequirementsSubLink, ExpectedText);

            Click(settingsMenuLink);

            return this;
        }

        public MainMenu NavigateToRecruitmentRequirementSetup()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandCareProviderSetupSection();

            WaitForElementToBeClickable(recruitmentRequirementSetupSubLink);
            Click(recruitmentRequirementSetupSubLink);

            return this;
        }

        public MainMenu ValidateRecruitmentRequirementSetupSubLinkVisible()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandCareProviderSetupSection();

            WaitForElementVisible(recruitmentRequirementSetupSubLink);

            return this;
        }

        public MainMenu ClickRecruitmentRequirementSetupSubLink()
        {
            WaitForElementToBeClickable(recruitmentRequirementSetupSubLink);
            ScrollToElement(recruitmentRequirementSetupSubLink);
            Click(recruitmentRequirementSetupSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Settings -> Care Provider Setup -> Risk Categories Management
        /// </summary>
        public MainMenu NavigateToRiskCategoriesManagementPage()
        {
            WaitForElement(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandCareProviderSetupSection();

            WaitForElement(riskCategoriesManagementSubLink);
            Click(riskCategoriesManagementSubLink);

            return this;
        }

        public MainMenu ValidateRiskCategoriesManagementSubLink(string ExpectedText)
        {
            WaitForElement(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandCareProviderSetupSection();

            WaitForElement(riskCategoriesManagementSubLink);
            ValidateElementText(riskCategoriesManagementSubLink, ExpectedText);

            return this;
        }

        public MainMenu NavigateToTrainingCourseSetupPage()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandCareProviderSetupSection();

            WaitForElementToBeClickable(trainingCourseSetupSubLink);
            Click(trainingCourseSetupSubLink);

            return this;
        }

        public MainMenu NavigateToTrainingRequirementSetupPage()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandCareProviderSetupSection();

            WaitForElementToBeClickable(trainingRequirementSetupSubLink);
            Click(trainingRequirementSetupSubLink);

            return this;
        }

        public MainMenu NavigateToAvailabilityTypesPage()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandCareProviderSetupSection();

            WaitForElementToBeClickable(AvailabilityTypesSubLink);
            Click(AvailabilityTypesSubLink);

            return this;
        }

        public MainMenu NavigateToBookingTypesPage()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandCareProviderSetupSection();

            WaitForElementToBeClickable(bookingTypesSubLink);
            Click(bookingTypesSubLink);

            return this;
        }

        /// <summary>
        /// User Profile -> FAQ Questions
        /// </summary>
        /// <returns></returns>
        public MainMenu NavigateToFAQsPage()
        {
            WaitForElement(userProfileButton);
            Click(userProfileButton);

            WaitForElementToBeClickable(FAQLink);
            Click(FAQLink);

            return this;
        }

        /// <summary>
        /// User Profile -> Change Default Team
        /// </summary>
        /// <returns></returns>
        public MainMenu ClickToChangeDefaultTeamButton()
        {
            WaitForElement(userProfileButton);
            Click(userProfileButton);

            WaitForElementToBeClickable(ChangeDefaultTeamLink);
            Click(ChangeDefaultTeamLink);

            return this;
        }

        public MainMenu ValidateDefaultTeamVisible()
        {
            WaitForElement(userDefaultTeam);
            WaitForElementVisible(userDefaultTeam);

            return this;
        }

        public MainMenu ValidateDefaultTeamText(string ExpectText)
        {
            ValidateElementText(userDefaultTeam, ExpectText);

            return this;
        }

        public MainMenu ValidateRecentlyViewdAreaLinkElementVisible(string ExpectText)
        {
            WaitForElementVisible(RecentlyViewdAreaLinkElement(ExpectText));

            return this;
        }

        public MainMenu ValidateRecentlyViewdAreaLinkElementVisible(string ExpectedText, string id)
        {
            WaitForElementVisible(RecentlyViewdAreaLinkElement(ExpectedText, id));

            ValidateElementByTitle(RecentlyViewdAreaLinkElement(ExpectedText, id), ExpectedText);

            string attributeValue = GetElementByAttributeValue(RecentlyViewdAreaLinkElement(ExpectedText, id), "onclick");
            Assert.IsTrue(attributeValue.Contains(id));

            return this;
        }

        public MainMenu ValidateStaffReviewsText(string ExpectText)
        {
            WaitForElementVisible(staffReviewsSubLink);
            ValidateElementText(staffReviewsSubLink, ExpectText);

            return this;
        }

        public MainMenu ClickSignOutButton()
        {
            WaitForElement(userProfileButton);
            Click(userProfileButton);

            WaitForElementToBeClickable(signOutLink);
            Click(signOutLink);

            return this;
        }

        /// <summary>
        /// Access the "Workplace -> Quality And Compliance -> Organisational Risk Management" section
        /// </summary>
        public MainMenu NavigateToOrganisationalRiskManagementPage()
        {
            WaitForElementToBeClickable(adminMenuLink);
            Click(adminMenuLink);

            WaitForElement(OrganisationalRiskManagementPageLink);
            Click(OrganisationalRiskManagementPageLink);

            return this;
        }

        public MainMenu NavigateToReportableEventPage()
        {
            WaitForElementToBeClickable(adminMenuLink);
            Click(adminMenuLink);

            WaitForElement(ReportableEventPageLink);
            Click(ReportableEventPageLink);

            return this;
        }

        public MainMenu NavigateToApplicantsPage()
        {
            WaitForElementToBeClickable(staffMenuLink);
            Click(staffMenuLink);

            WaitForElement(ApplicantsPageLink);
            Click(ApplicantsPageLink);

            return this;
        }

        /// <summary>
        /// Access the "Workplace -> Health Diary" section
        /// </summary>
        public MainMenu NavigateToHealthDiarySection()
        {
            WaitForElementToBeClickable(healthDiarySubLink);
            Click(healthDiarySubLink);

            return this;
        }

        /// <summary>
        /// Access the "Finance -> Service Provisions" section
        /// </summary>
        public MainMenu NavigateToServiceProvisionsSection()
        {
            WaitForElementToBeClickable(financeMenuLink);
            Click(financeMenuLink);

            WaitForElementVisible(serviceProvisionSubLink);
            Click(serviceProvisionSubLink);

            return this;
        }

        /// <summary>
        /// Access the "Setting -> Security -> Teams" section
        /// </summary>
        public MainMenu NavigateToTeamsSection()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandSecuritySection();

            WaitForElementToBeClickable(teamsSubLink);
            Click(teamsSubLink);

            return this;
        }

        public MainMenu NavigateToAllRoleApplicationsPage()
        {
            WaitForElementToBeClickable(staffMenuLink);
            Click(staffMenuLink);

            WaitForElementToBeClickable(AllRoleApplicationsPageLink);
            Click(AllRoleApplicationsPageLink);

            return this;
        }

        public MainMenu refreshPage()
        {
            driver.Navigate().Refresh();

            return this;
        }

        public MainMenu NavigateToInvoiceBatchesSection()
        {
            WaitForElementToBeClickable(financeMenuLink);
            Click(financeMenuLink);

            WaitForElementToBeClickable(invoiceBatchesSubLink);
            Click(invoiceBatchesSubLink);

            return this;
        }

        public MainMenu NavigateToFinanceInvoicesSection()
        {
            WaitForElementToBeClickable(financeMenuLink);
            Click(financeMenuLink);

            WaitForElementToBeClickable(financeInvoicesSubLink);
            Click(financeInvoicesSubLink);

            return this;
        }

        public MainMenu NavigateToFinanceTransactionsSection()
        {
            WaitForElementToBeClickable(financeMenuLink);
            Click(financeMenuLink);

            WaitForElementToBeClickable(financeTransactionsSubLink);
            Click(financeTransactionsSubLink);

            return this;
        }

        public MainMenu NavigateToExtractsSection()
        {
            WaitForElementToBeClickable(financeMenuLink);
            Click(financeMenuLink);

            WaitForElementToBeClickable(extractsSubLink);
            Click(extractsSubLink);

            return this;
        }

        public MainMenu NavigateToPersonContractsSection()
        {
            WaitForElementToBeClickable(financeMenuLink);
            Click(financeMenuLink);

            WaitForElementToBeClickable(personContractsSubLink);
            Click(personContractsSubLink);

            return this;
        }

        public MainMenu NavigateToPayrollBatchesSection()
        {
            WaitForElementToBeClickable(financeMenuLink);
            Click(financeMenuLink);

            WaitForElementToBeClickable(payrollBatchesSubLink);
            Click(payrollBatchesSubLink);

            return this;
        }

        public MainMenu NavigateToFinanceExtractBatchesPage()
        {
            WaitForElementToBeClickable(financeMenuLink);
            Click(financeMenuLink);

            WaitForElement(financeExtractBatchesSubLink);
            Click(financeExtractBatchesSubLink);

            return this;
        }

        public MainMenu NavigateToFinanceInvoiceBatchesPage()
        {
            WaitForElementToBeClickable(financeMenuLink);
            Click(financeMenuLink);

            WaitForElement(financeInvoiceBatchesSubLink);
            Click(financeInvoiceBatchesSubLink);

            return this;
        }

        //Method to navigate to Finance Invoice Payments page
        public MainMenu NavigateToFinanceInvoicePaymentsPage()
        {
            WaitForElement(financeMenuLink);
            ScrollToElement(financeMenuLink);
            Click(financeMenuLink);

            WaitForElement(financeInvoicePaymentsSubLink);
            ScrollToElement(financeInvoicePaymentsSubLink);
            Click(financeInvoicePaymentsSubLink);

            return this;
        }

        public MainMenu ClickPinnedRecordsButton()
        {
            WaitForElementToBeClickable(pinnedRecordsMenuLink);
            Click(pinnedRecordsMenuLink);

            return this;
        }

        public MainMenu NavigateToPersonContractServicesSection()
        {
            WaitForElementToBeClickable(financeMenuLink);
            Click(financeMenuLink);

            WaitForElementToBeClickable(personContractServicesSubLink);
            Click(personContractServicesSubLink);

            return this;
        }

        public MainMenu NavigateToSchedulingSetupPage()
        {
            WaitForElementToBeClickable(settingsMenuLink);
            Click(settingsMenuLink);

            ExpandCareProviderSetupSection();

            WaitForElementToBeClickable(schedulingSetupSubLink);
            Click(schedulingSetupSubLink);

            return this;
        }

        public MainMenu NavigateToStaffContractsPage()
        {
            WaitForElementToBeClickable(staffMenuLink);
            Click(staffMenuLink);

            WaitForElementToBeClickable(staffContractsSubLink);
            Click(staffContractsSubLink);

            return this;
        }

    }
}
