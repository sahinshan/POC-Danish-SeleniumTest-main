using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonRecordPage : CommonMethods
    {
        public PersonRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWSummaryDashboardPanel_IFrame = By.Id("CWUrlPanel_IFrame");
        readonly By CWTimelinePanel_IFrame = By.Id("CWTimelinePanel_IFrame");
        readonly By NewPersonRecord_IFrame = By.Id("iframe_CWNewPerson");

        readonly By iframe_AlertDialog = By.XPath("//iframe[contains(@src, 'Html_PersonSummaryAlertHazardsWidget')");

        #region Iframe hierarchy for opening the person record page as a popup from a case form

        readonly By caseformIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=caseform&')]");
        readonly By iframe_CWAssessmentDialog = By.Id("iframe_CWAssessmentDialog");
        readonly By iframe_CWRecordDialog = By.Id("iframe_CWRecordDialog");

        #endregion

        #region Iframe System User PersonRecord

        readonly By systemUserPersonRecord = By.Id("CWTimelinePanel_IFrame");

        #endregion



        readonly By personPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By DeceasedIcon = By.XPath("//*[@id='CWBannerHolder']/ul/li[2]/div[2]/img[@title ='Deceased']");

        readonly By KnownToChildProtectionIcon = By.XPath("//*[@id='CWBannerHolder']/ul/li[2]/div[2]/img[@title ='Known to Child Protection']");
        readonly By RelatedPerson_KnownToChildProtectionIcon = By.XPath("//*[@id='CWBannerHolder']/ul/li[2]/div[2]/img[@title ='(Related Person) Known to Child Protection']");


        #region Top Menu

        readonly By editButton = By.XPath("//*[@id='TI_EditButton']");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");

        readonly By additionalIItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By PinToMeButton = By.Id("TI_PinToMeButton");
        readonly By UnPinFromMeButton = By.Id("TI_UnpinFromMeButton");

        #endregion

        #region Top Banner

        readonly By toogleButton = By.Id("CWToggleButton");

        readonly By homePhoneInfo = By.XPath("//*[@id='CWDetailItem']/div[2]/ul/li[2]/span");
        readonly By websiteUserApprovedIcon = By.XPath("//*[@id='CWBannerHolder']/ul/li/div/img[@title='Website User Approved'][@alt='Website User Approved']");
        readonly By websiteUserWaitingForApprovalIcon = By.XPath("//*[@id='CWBannerHolder']/ul/li/div/img[@title='Website User Waiting for Approval'][@alt='Website User Waiting for Approval']");
        readonly By AllergiesNotRecordedIcon = By.XPath("//*[@id='CWBannerHolder']/ul/li/div/img[@title='Allergies Not Recorded'][@alt='Allergies Not Recorded']");
        readonly By NoKnownAllergiesIcon = By.XPath("//*[@id='CWBannerHolder']/ul/li/div/img[@title='No Known Allergies'][@alt='No Known Allergies']");
        readonly By KnownAllergiesIcon = By.XPath("//*[@id='CWBannerHolder']/ul/li/div/img[@title='Known Allergies'][@alt='Known Allergies']");


        #endregion

        #region Tabs

        readonly By timelineSection = By.XPath("//li[@id='CWNavGroup_Timeline']/a[@title='Timeline']");
        readonly By summarySection = By.XPath("//li[@id='CWNavGroup_SummaryDashboard']/a[@title='Summary']");
        readonly By aboutMeSection = By.XPath("//li[@id='CWNavGroup_AboutMe']/a[@title='About Me']");
        readonly By profileSection = By.XPath("//li[@id='CWNavGroup_Profile']/a[@title='Profile']");
        readonly By casesSection = By.XPath("//li[@id='CWNavGroup_Cases']/a[@title='Cases']");
        readonly By Cases = By.XPath("//*[@id='CWNavGroup_Cases']/a[text()='Cases']");
        readonly By carePlansSection = By.XPath("//li[@id='CWNavGroup_PersonCarePlanNavigation']/a[@title='Care Plans']");
        readonly By servicesSection = By.XPath("//li[@id='CWNavGroup_ServiceProvisions']/a[@title='Services']");
        readonly By documentViewSection = By.XPath("//li[@id='CWNavGroup_PersonDocumentView']/a[@title='Document View']");
        readonly By allActivitiesSection = By.XPath("//li[@id='CWNavGroup_PersonAllActivities']/a[@title='All Activities']");
        readonly By diarySection = By.XPath("//li[@id='CWNavGroup_Diary']/a[@title='Diary']");

        #endregion

        #region Navigation Area

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        #region Sections

        readonly By activitiesDetailsElementExpanded = By.XPath("//span[text()='Activities']/parent::div/parent::summary/parent::details[@open]");
        readonly By activitiesLeftSubMenu = By.XPath("//details/summary/div/span[text()='Activities']");

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");

        readonly By dailyCareDetailsElementExpanded = By.XPath("//span[text()='Daily Care']/parent::div/parent::summary/parent::details[@open]");
        readonly By dailyCareLeftSubMenu = By.XPath("//details/summary/div/span[text()='Daily Care']");

        readonly By otherInformationDetailsElementExpanded = By.XPath("//span[text()='Other Information']/parent::div/parent::summary/parent::details[@open]");
        readonly By otherInformationLeftSubMenu = By.XPath("//details/summary/div/span[text()='Other Information']");

        readonly By careNetworkDetailsElementExpanded = By.XPath("//span[text()='Care Network']/parent::div/parent::summary/parent::details[@open]");
        readonly By careNetworkSubMenu = By.XPath("//details/summary/div/span[text()='Care Network']");


        /**CDV6 Elements**/
        readonly By mentalHealthActLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_MentalHealthAct']/a");
        readonly By workAndEducationLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_WorkAndEducation']/a");
        readonly By financeLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_Finance']/a");
        /**CDV6 Elements**/

        #endregion

        #region Activities

        readonly By appointmentstLeftSubMenuItem = By.Id("CWNavItem_Appointment");
        readonly By caseNotestLeftSubMenuItem = By.Id("CWNavItem_PersonCaseNote");
        readonly By emailstLeftSubMenuItem = By.Id("CWNavItem_Email");
        readonly By PortalTasksLeftSubMenuItem = By.Id("CWNavItem_PortalTasks");
        readonly By PhoneCallsLeftSubMenuItem = By.Id("CWNavItem_PhoneCall");
        readonly By LettersLeftSubMenuItem = By.Id("CWNavItem_Letter");
        readonly By TasksLeftSubMenuItem = By.Id("CWNavItem_Tasks");
        readonly By recurringAppointmentsLeftSubMenuItem = By.Id("CWNavItem_RecurringAppointment");

        #endregion

        #region Related Items

        readonly By chronologiestLeftSubMenuItem = By.Id("CWNavItem_PersonChronology");
        readonly By audittLeftSubMenuItem = By.Id("CWNavItem_AuditHistory");
        readonly By PersonFormLeftSubMenuItem = By.Id("CWNavItem_PersonForm");
        readonly By MASHEpisodesLeftSubMenuItem = By.Id("CWNavItem_MASHEpisodes");
        readonly By PersonAttachmentsLeftSubMenuItem = By.Id("CWNavItem_PersonAttachment");
        readonly By PrimarySupportReasonsLeftSubMenuItem = By.Id("CWNavItem_PersonPrimarySupportReason");
        readonly By PersonContactsLeftSubMenuItem = By.Id("CWNavItem_Contacts");
        readonly By PersonAlertAndHazardsLeftSubMenuItem = By.Id("CWNavItem_PersonAlertAndHazard");
        readonly By PersonContractsLeftSubMenuItem = By.Id("CWNavItem_ContractsAndFunding");
        readonly By PersonalMoneyAccountsLeftSubMenuItem = By.Id("CWNavItem_PersonalMoneyAccounts");
        readonly By PersonSpecificTrainingLeftSubMenuItem = By.Id("CWNavItem_SpecificTrainings");
        readonly By PersonPostAdoptionLinkLeftSubMenuItem = By.Id("CWNavItem_AdoptionLink");
        readonly By PersonPreAdoptionLinkLeftSubMenuItem = By.Id("CWNavItem_PreAdoptionLink");
        readonly By PersonHealthDetailsPageLeftSubMenuItem = By.Id("CWNavItem_PersonHealthDetail");
        readonly By PersonHealthPersonAbsencesPageLeftSubMenuItem = By.Id("CWNavItem_CPPersonAbsence");
        readonly By PersonGestationPeriodPageLeftSubMenuItem = By.Id("CWNavItem_GestationPeriod");
        readonly By AllegedAbuserPageLeftSubMenuItem = By.Id("CWNavItem_AllegedAbuser");
        readonly By AllegedVictimPageLeftSubMenuItem = By.Id("CWNavItem_AllegedVictim");
        readonly By SytemUserPersonAddressLeftMenuItem = By.Id("CWNavItem_PersonAddress");
        readonly By PersonAboutMePage = By.Id("CWNavItem_PersonAboutMe");
        readonly By ChargeApportionmentsLeftSubMenuItem = By.Id("CWNavItem_ChargeApprotionmentDetails");

        readonly By insideHouseHoldRecord = By.XPath("//*[@id='CWGridHeaderRow']/th[4]/a/span[text()='Inside Household']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        #endregion

        #region Health

        readonly By PhysicalObservationLeftSubMenuItem = By.Id("CWNavItem_PhysicalObservation");
        readonly By ClinicalRiskStatusesLeftSubMenuItem = By.Id("CWNavItem_ClinicalRiskStatuses");
        readonly By ClinicalRiskFactorLeftSubMenuItem = By.Id("CWNavItem_PersonClinicalRiskFactor");
        readonly By PersonHeightAndWeightLeftSubMenuItem = By.Id("CWNavItem_PersonHeightAndWeight");
        readonly By HealthProfessionalsLeftSubMenuItem = By.Id("CWNavItem_HealthProfessionals");
        readonly By PersonDisabilityImpairmentsLeftSubMenuItem = By.Id("CWNavItem_PersonDisabilityImpairments");
        readonly By PersonDNARLeftSubMenuItem = By.Id("CWNavItem_PersonDNAR");
        readonly By PersonImmunisationLeftSubMenuItem = By.Id("CWNavItem_PersonImmunisation");
        readonly By PersonAllergiesLeftSubMenuItem = By.Id("CWNavItem_PersonAllergies");
        readonly By PersonBodyMapsSubMenuItem = By.Id("CWNavItem_BodyMaps");
        readonly By PersonMobilityLeftSubMenuItem = By.Id("CWNavItem_Mobility");
        readonly By KeyworkerNotesLeftSubMenuItem = By.Id("CWNavItem_KeyworkerNotes");
        readonly By DailyPersonalCareLeftSubMenuItem = By.Id("CWNavItem_DailyPersonalCare");
        readonly By WelfareChecksLeftSubMenuItem = By.Id("CWNavItem_DayNightCheck");
        readonly By RepositioningLeftSubMenuItem = By.Id("CWNavItem_Turning");
        readonly By DailyRecordLeftSubMenuItem = By.Id("CWNavItem_DailyRecord");
        readonly By ContinenceCareLeftSubMenuItem = By.Id("CWNavItem_CPPersonToileting");
        readonly By DistressedBehaviourLeftSubMenuItem = By.Id("CWNavItem_BehaviourIncident");
        readonly By ActivitiesLeftSubMenuItem = By.Id("CWNavItem_CPPersonActivities");
        readonly By DiaryEventsLeftSubMenuItem = By.Id("CWNavItem_DiaryEvent");
        readonly By PersonConversationsLeftSubMenuItem = By.Id("CWNavItem_PersonConversations");
        readonly By EmotionalSupportLeftSubMenuItem = By.Id("CWNavItem_CPPersonEmotionalSupport");
        readonly By FoodAndFluidLeftSubMenuItem = By.Id("CWNavItem_FoodAndFluid");
        readonly By PainManagementLeftSubMenuItem = By.Id("CWNavItem_CPPersonPainManagement");
        readonly By PersonalSafetyandEnvironmentLeftSubMenuItem = By.Id("CWNavItem_CPPersonPersonalSafetyandEnvironment");

        #endregion

        #region Mental Health Act

        readonly By PersonMHALegalStatusesLeftSubMenuItem = By.Id("CWNavItem_PersonMHALegalStatus");
        readonly By CWNavItem_MHAAftercareEntitlement = By.Id("CWNavItem_MHAAftercareEntitlement");

        #endregion

        #region Work and Education

        readonly By AttendedEducationEstablishmentLeftSubMenuItem = By.Id("CWNavItem_PersonAttendedEducationEstablishment");

        #endregion

        #region Other Information

        readonly By cpisRecordsLeftSubMenuItem = By.Id("CWNavItem_CPISRecords");

        #endregion

        #region Finance

        readonly By financeTransactionLeftSubMenuItem = By.Id("CWNavItem_FinanceTransaction");
        readonly By financialAssessmentLeftSubMenuItem = By.Id("CWNavItem_FinancialAssessment");
        readonly By personFinancialDetailLeftSubMenuItem = By.Id("CWNavItem_PersonFinancialDetail");
        readonly By personalBudgetLeftSubMenuItem = By.Id("CWNavItem_PersonalBudget");
        readonly By serviceProvisionLeftSubMenuItem = By.Id("CWNavItem_ServiceProvision");
        readonly By servicePackageLeftSubMenuItem = By.Id("CWNavItem_PersonServicePackage");

        #endregion





        #endregion

        #region Summary Tab

        readonly By CWDashboardSelector = By.Id("CWDashboardSelector");


        #endregion

        #region CareNetwork 

        readonly By Relationships = By.Id("CWNavItem_PersonRelationships");
        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By RelatedPersonAlertAndHazards_Icon = By.XPath("//*[@id='CWBannerHolder']/ul/li/div/img[@title='Related Person Alerts/Hazards']");
        readonly By Disability_Icon = By.XPath("//*[@id='CWBannerHolder']/ul/li/div/img[@title='Disability']");
        readonly By Impairment_Icon = By.XPath("//*[@id='CWBannerHolder']/ul/li/div/img[@title='Impairments']");
        readonly By CaseInvolvementCreated_Icon = By.XPath("//*[@id='CWPastTimelinePanel']/li[2]/div[1]/div[@class = 'vertlinemiddle bo-icon']");


        #endregion



        public PersonRecordPage OpenPersonRecordHyperlink(string PersonRecordHyperlink)
        {
            driver.Navigate().GoToUrl(PersonRecordHyperlink);

            return this;
        }


        public PersonRecordPage WaitForPersonRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(MenuButton);
            WaitForElement(timelineSection);
            WaitForElement(summarySection);

            // WaitForElement(profileSection);

            // WaitForElement(casesSection);
            WaitForElement(carePlansSection);
            // WaitForElement(servicesSection);
            WaitForElement(allActivitiesSection);

            return this;
        }

        public PersonRecordPage WaitForPersonRecordPageToLoadAfterSave()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(NewPersonRecord_IFrame);
            SwitchToIframe(NewPersonRecord_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(MenuButton);
            WaitForElement(timelineSection);
            WaitForElement(summarySection);

            WaitForElement(carePlansSection);

            WaitForElement(allActivitiesSection);

            return this;
        }




        public PersonRecordPage WaitForSystemUserPersonRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);


            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public PersonRecordPage WaitForPersonRecordPageToLoadFromHyperlink(string PersonName)
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personPageHeader);
            ValidateElementText(personPageHeader, "Person:\r\n" + PersonName);

            WaitForElement(MenuButton);
            WaitForElement(timelineSection);
            WaitForElement(summarySection);
            WaitForElement(profileSection);
            WaitForElement(casesSection);
            WaitForElement(carePlansSection);
            WaitForElement(allActivitiesSection);

            return this;
        }

        public PersonRecordPage WaitForPersonRecordPageToLoad(bool casesSectionVisible, bool servicesSectionVisible)
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(MenuButton);
            WaitForElement(timelineSection);
            WaitForElement(summarySection);
            //WaitForElement(profileSection);

            if (casesSectionVisible)
                WaitForElement(casesSection);

            WaitForElement(carePlansSection);

            if (servicesSectionVisible)
                WaitForElement(servicesSection);

            WaitForElement(allActivitiesSection);

            return this;
        }

        public PersonRecordPage WaitForPersonRecordPageToLoad(bool casesSectionVisible, bool servicesSectionVisible, bool TimelineVisible, bool carePlansSectionVisible)
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(MenuButton);

            if (TimelineVisible)
                WaitForElement(timelineSection);

            WaitForElement(summarySection);
            //WaitForElement(profileSection);

            if (casesSectionVisible)
                WaitForElement(casesSection);

            if (carePlansSectionVisible)
                WaitForElement(carePlansSection);

            if (servicesSectionVisible)
                WaitForElement(servicesSection);

            WaitForElement(allActivitiesSection);

            return this;
        }

        public PersonRecordPage WaitForSummaryTabToLoad()
        {

            WaitForElement(CWSummaryDashboardPanel_IFrame);
            SwitchToIframe(CWSummaryDashboardPanel_IFrame);


            return this;
        }


        public PersonRecordPage WaitForAlertsSectionToLoad()
        {


            WaitForElement(iframe_AlertDialog);
            SwitchToIframe(iframe_AlertDialog);

            return this;
        }

        /// <summary>
        /// Wait for the person record page to load as a popup after clicking on a CMS Readonly field on a case form
        /// </summary>
        /// <returns></returns>
        public PersonRecordPage WaitForPersonRecordPopupPageToLoadFromCaseForm()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseformIFrame);
            SwitchToIframe(caseformIFrame);

            WaitForElement(iframe_CWAssessmentDialog);
            SwitchToIframe(iframe_CWAssessmentDialog);

            WaitForElement(iframe_CWRecordDialog);
            SwitchToIframe(iframe_CWRecordDialog);

            WaitForElement(MenuButton);
            WaitForElement(timelineSection);
            WaitForElement(summarySection);
            WaitForElement(profileSection);
            WaitForElement(casesSection);
            WaitForElement(carePlansSection);
            WaitForElement(servicesSection);
            WaitForElement(allActivitiesSection);

            return this;
        }

        public PersonRecordPage WaitForPersonRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);
            return this;
        }

        public PersonRecordPage NavigateToFinanceTransactionsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElement(financeLeftSubMenu);
            Click(financeLeftSubMenu);

            WaitForElementToBeClickable(financeTransactionLeftSubMenuItem);
            ScrollToElement(financeTransactionLeftSubMenuItem);
            Click(financeTransactionLeftSubMenuItem);

            return this;
        }


        public PersonFinancialAssessmentsPage NavigateToPersonFinancialAssessmentsPage()
        {
            Click(MenuButton);

            WaitForElement(financeLeftSubMenu);
            Click(financeLeftSubMenu);

            WaitForElement(financialAssessmentLeftSubMenuItem);
            Click(financialAssessmentLeftSubMenuItem);

            return new PersonFinancialAssessmentsPage(this.driver, this.Wait, this.appURL);
        }
        public PersonFinancialAssessmentsPage NavigateToPersonFinancialDetailPage()
        {
            Click(MenuButton);

            WaitForElement(financeLeftSubMenu);
            Click(financeLeftSubMenu);

            WaitForElement(personFinancialDetailLeftSubMenuItem);
            Click(personFinancialDetailLeftSubMenuItem);

            return new PersonFinancialAssessmentsPage(this.driver, this.Wait, this.appURL);
        }
        public PersonFinancialAssessmentsPage NavigateToPersonHealthProfessionalsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElement(HealthProfessionalsLeftSubMenuItem);
            Click(HealthProfessionalsLeftSubMenuItem);

            return new PersonFinancialAssessmentsPage(this.driver, this.Wait, this.appURL);
        }

        public PersonFinancialAssessmentsPage NavigateToPersonDisabilityImpairmentsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElement(PersonDisabilityImpairmentsLeftSubMenuItem);
            Click(PersonDisabilityImpairmentsLeftSubMenuItem);

            return new PersonFinancialAssessmentsPage(this.driver, this.Wait, this.appURL);
        }

        public PersonFinancialAssessmentsPage NavigateToRecordsOfDNARPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElement(PersonDNARLeftSubMenuItem);
            Click(PersonDNARLeftSubMenuItem);

            return new PersonFinancialAssessmentsPage(this.driver, this.Wait, this.appURL);
        }


        public PersonFinancialAssessmentsPage NavigateToPersonAllergiesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElement(PersonAllergiesLeftSubMenuItem);
            Click(PersonAllergiesLeftSubMenuItem);

            return new PersonFinancialAssessmentsPage(this.driver, this.Wait, this.appURL);
        }

        public PersonFinancialAssessmentsPage NavigateToPersonServiceProvisionsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(financeLeftSubMenu);
            Click(financeLeftSubMenu);

            WaitForElementToBeClickable(serviceProvisionLeftSubMenuItem);
            Click(serviceProvisionLeftSubMenuItem);

            return new PersonFinancialAssessmentsPage(this.driver, this.Wait, this.appURL);
        }

        public PersonRecordPage NavigateToPersonCaseNotesPage()
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

        public PersonRecordPage NavigateToAppointmentsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(activitiesDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(activitiesLeftSubMenu);
                Click(activitiesLeftSubMenu);
            }

            WaitForElementToBeClickable(appointmentstLeftSubMenuItem);
            Click(appointmentstLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToRecurringAppointmentsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(activitiesDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(activitiesLeftSubMenu);
                Click(activitiesLeftSubMenu);
            }

            WaitForElementToBeClickable(recurringAppointmentsLeftSubMenuItem);
            Click(recurringAppointmentsLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToEmailsPage()
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

        public PersonRecordPage NavigateToPortalTasksPage()
        {
            Click(MenuButton);

            if (!CheckIfElementExists(activitiesDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(activitiesLeftSubMenu);
                Click(activitiesLeftSubMenu);
            }

            WaitForElementToBeClickable(PortalTasksLeftSubMenuItem);
            Click(PortalTasksLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPhoneCallsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(activitiesDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(activitiesLeftSubMenu);
                Click(activitiesLeftSubMenu);
            }

            WaitForElementToBeClickable(PhoneCallsLeftSubMenuItem);
            Click(PhoneCallsLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToLettersPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(activitiesDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(activitiesLeftSubMenu);
                Click(activitiesLeftSubMenu);
            }

            WaitForElementToBeClickable(LettersLeftSubMenuItem);
            Click(LettersLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToTasksPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(activitiesDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(activitiesLeftSubMenu);
                Click(activitiesLeftSubMenu);
            }

            WaitForElementToBeClickable(TasksLeftSubMenuItem);
            Click(TasksLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToChronologiesPage()
        {
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(chronologiestLeftSubMenuItem);
            Click(chronologiestLeftSubMenuItem);

            return this;
        }


        public PersonRecordPage NavigateToImmunisationPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonImmunisationLeftSubMenuItem);
            Click(PersonImmunisationLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToKeyworkerNotesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(KeyworkerNotesLeftSubMenuItem);
            Click(KeyworkerNotesLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToDailyPersonalCarePage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(DailyPersonalCareLeftSubMenuItem);
            Click(DailyPersonalCareLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToWelfareChecksPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(WelfareChecksLeftSubMenuItem);
            Click(WelfareChecksLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToRepositioningPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(RepositioningLeftSubMenuItem);
            Click(RepositioningLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPersonMobilityPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonMobilityLeftSubMenuItem);
            Click(PersonMobilityLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToDailyRecordPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(DailyRecordLeftSubMenuItem);
            Click(DailyRecordLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToContinenceCarePage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(ContinenceCareLeftSubMenuItem);
            Click(ContinenceCareLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToActivitiesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(ActivitiesLeftSubMenuItem);
            Click(ActivitiesLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToDiaryEventsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(DiaryEventsLeftSubMenuItem);
            Click(DiaryEventsLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToEmotionalSupportPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(EmotionalSupportLeftSubMenuItem);
            Click(EmotionalSupportLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToDistressedBehaviourPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(DistressedBehaviourLeftSubMenuItem);
            Click(DistressedBehaviourLeftSubMenuItem);

            return this;
        }

        //Method to Navigate to Person Conversations Page
        public PersonRecordPage NavigateToPersonConversationsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonConversationsLeftSubMenuItem);
            Click(PersonConversationsLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToFoodAndFluidPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(FoodAndFluidLeftSubMenuItem);
            Click(FoodAndFluidLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPainManagementPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(PainManagementLeftSubMenuItem);
            Click(PainManagementLeftSubMenuItem);

            return this;
        }

        //Navigate to Personal Safety and Environment Page
        public PersonRecordPage NavigateToPersonalSafetyandEnvironmentPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonalSafetyandEnvironmentLeftSubMenuItem);
            Click(PersonalSafetyandEnvironmentLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(audittLeftSubMenuItem);
            Click(audittLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPersonFormsPage()
        {
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonFormLeftSubMenuItem);
            Click(PersonFormLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToMASHEpisodesPage()
        {
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(MASHEpisodesLeftSubMenuItem);
            Click(MASHEpisodesLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPersonAttachmentsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonAttachmentsLeftSubMenuItem);
            Click(PersonAttachmentsLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPrimarySupportReasonsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(PrimarySupportReasonsLeftSubMenuItem);
            Click(PrimarySupportReasonsLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPersonContactsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonContactsLeftSubMenuItem);
            Click(PersonContactsLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPhysicalObservationsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(PhysicalObservationLeftSubMenuItem);
            Click(PhysicalObservationLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToClinicalRiskStatusesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(ClinicalRiskStatusesLeftSubMenuItem);
            Click(ClinicalRiskStatusesLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToHeightWeightObservationsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonHeightAndWeightLeftSubMenuItem);
            Click(PersonHeightAndWeightLeftSubMenuItem);

            return this;
        }
        public PersonRecordPage NavigateToBodyMapsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonBodyMapsSubMenuItem);
            Click(PersonBodyMapsSubMenuItem);

            return this;
        }



        public PersonRecordPage NavigateToPersonMHALegalStatusesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(mentalHealthActLeftSubMenu);
            Click(mentalHealthActLeftSubMenu);

            WaitForElementToBeClickable(PersonMHALegalStatusesLeftSubMenuItem);
            Click(PersonMHALegalStatusesLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToSection117EntitlementsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(mentalHealthActLeftSubMenu);
            Click(mentalHealthActLeftSubMenu);

            WaitForElementToBeClickable(CWNavItem_MHAAftercareEntitlement);
            Click(CWNavItem_MHAAftercareEntitlement);

            return this;
        }

        public PersonFinancialAssessmentsPage NavigateToAttendedEducationEstablishmentPage()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            WaitForElement(workAndEducationLeftSubMenu);
            Click(workAndEducationLeftSubMenu);

            WaitForElement(AttendedEducationEstablishmentLeftSubMenuItem);
            Click(AttendedEducationEstablishmentLeftSubMenuItem);

            return new PersonFinancialAssessmentsPage(this.driver, this.Wait, this.appURL);
        }

        public PersonFinancialAssessmentsPage NavigateToCPISRecordsPage()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(otherInformationDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(otherInformationLeftSubMenu);
                Click(otherInformationLeftSubMenu);
            }

            WaitForElement(cpisRecordsLeftSubMenuItem);
            Click(cpisRecordsLeftSubMenuItem);

            return new PersonFinancialAssessmentsPage(this.driver, this.Wait, this.appURL);
        }

        public PersonRecordPage TapEditButton()
        {
            Click(editButton);


            return this;
        }

        public PersonRecordPage TapBackButton()
        {
            Click(backButton);


            return this;
        }

        public PersonRecordPage TapTopBannerToogleButton()
        {
            WaitForElementToBeClickable(toogleButton);
            Click(toogleButton);


            return this;
        }

        public PersonRecordPage ValidateTopBannerHomePhone(string ExpectedText)
        {
            WaitForElementVisible(homePhoneInfo);
            ValidateElementText(homePhoneInfo, ExpectedText);


            return this;
        }

        public PersonRecordPage TapSummaryTab()
        {
            Click(summarySection);

            return this;
        }

        public PersonRecordPage TapAboutMeTab()
        {
            WaitForElement(aboutMeSection);
            Click(aboutMeSection);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }

        public PersonRecordPage TapDocumentViewTab()
        {
            Click(documentViewSection);

            return this;
        }

        public PersonRecordPage TapTimeLineTab()
        {
            WaitForElementToBeClickable(timelineSection);
            ScrollToElement(timelineSection);
            Click(timelineSection);

            return this;
        }

        public PersonRecordPage TapCarePlansTab()
        {
            WaitForElementToBeClickable(carePlansSection);
            Click(carePlansSection);

            return this;
        }

        public PersonRecordPage TapCasesTab()
        {
            WaitForElementToBeClickable(Cases);
            Click(Cases);

            return this;
        }

        public PersonRecordPage TapAllActivitiesTab()
        {
            Click(allActivitiesSection);

            return this;
        }

        public PersonRecordPage TapDiaryTab()
        {
            Click(diarySection);

            return this;
        }

        public PersonRecordPage SelectDashboard(string DashboardName)
        {
            WaitForElement(CWDashboardSelector);
            WaitForElementToBeClickable(CWDashboardSelector);
            SelectPicklistElementByText(CWDashboardSelector, DashboardName);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonRecordPage ValidateWebsiteUserApprovedIconVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(websiteUserApprovedIcon);
            else
                WaitForElementNotVisible(websiteUserApprovedIcon, 5);

            return this;
        }

        public PersonRecordPage ValidateWebsiteUserWaitingForApprovalIconVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(websiteUserWaitingForApprovalIcon);
            else
                WaitForElementNotVisible(websiteUserWaitingForApprovalIcon, 5);

            return this;
        }

        public PersonRecordPage ValidateNoKnownAllergiesIconVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(NoKnownAllergiesIcon);
            else
                WaitForElementNotVisible(NoKnownAllergiesIcon, 5);

            return this;
        }

        public PersonRecordPage ValidateKnownAllergiesIconVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(KnownAllergiesIcon);
            else
                WaitForElementNotVisible(KnownAllergiesIcon, 5);

            return this;
        }

        public PersonRecordPage ValidateAllergiesNotRecordedIconVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(AllergiesNotRecordedIcon);
            else
                WaitForElementNotVisible(AllergiesNotRecordedIcon, 5);

            return this;
        }

        public PersonRecordPage NavigateToPersonAlertAndHazardsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonAlertAndHazardsLeftSubMenuItem);
            Click(PersonAlertAndHazardsLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPersonContractsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonContractsLeftSubMenuItem);
            Click(PersonContractsLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPersonalMoneyAccountsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonalMoneyAccountsLeftSubMenuItem);
            Click(PersonalMoneyAccountsLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPersonSpecificTrainingPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonSpecificTrainingLeftSubMenuItem);
            Click(PersonSpecificTrainingLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPersonPostAdoptionLinkPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonPostAdoptionLinkLeftSubMenuItem);
            Click(PersonPostAdoptionLinkLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToPersonPreAdoptionLinkPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonPreAdoptionLinkLeftSubMenuItem);
            Click(PersonPreAdoptionLinkLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToRelationshipsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(careNetworkDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(careNetworkSubMenu);
                Click(careNetworkSubMenu);
            }

            WaitForElementToBeClickable(Relationships);
            Click(Relationships);

            return this;
        }

        public PersonRecordPage ValidateNoRecordLabelVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordLabel);
            }
            else
            {
                WaitForElementNotVisible(NoRecordLabel, 5);
            }
            return this;
        }


        public PersonRecordPage ValidatePersonRelatedAlertsAndHazards_Icon(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(RelatedPersonAlertAndHazards_Icon);
            }
            else
            {
                WaitForElementNotVisible(RelatedPersonAlertAndHazards_Icon, 5);
            }
            return this;
        }

        public PersonRecordPage NavigateToClinicalRiskFactorPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(ClinicalRiskFactorLeftSubMenuItem);
            Click(ClinicalRiskFactorLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToHealthDetailsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonHealthDetailsPageLeftSubMenuItem);
            Click(PersonHealthDetailsPageLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToHealthPersonAbsencesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonHealthPersonAbsencesPageLeftSubMenuItem);
            Click(PersonHealthPersonAbsencesPageLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage ValidatePersonDisability_Icon(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(Disability_Icon);
            }
            else
            {
                WaitForElementNotVisible(Disability_Icon, 5);
            }
            return this;
        }

        public PersonRecordPage ValidatePersonImpariment_Icon(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(Impairment_Icon);
            }
            else
            {
                WaitForElementNotVisible(Impairment_Icon, 5);
            }
            return this;
        }

        public PersonRecordPage NavigateToGestationPeriodPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(dailyCareDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(dailyCareLeftSubMenu);
                Click(dailyCareLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonGestationPeriodPageLeftSubMenuItem);
            Click(PersonGestationPeriodPageLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToAllegedAbuserPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(AllegedAbuserPageLeftSubMenuItem);
            Click(AllegedAbuserPageLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToAllegedVictimPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(AllegedVictimPageLeftSubMenuItem);
            Click(AllegedVictimPageLeftSubMenuItem);

            return this;
        }

        public PersonRecordPage ValidateDeceasedPerson_Icon(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(DeceasedIcon);
            }
            else
            {
                WaitForElementNotVisible(DeceasedIcon, 5);
            }
            return this;
        }


        public PersonRecordPage NavigateToPersonAddressPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(SytemUserPersonAddressLeftMenuItem);
            Click(SytemUserPersonAddressLeftMenuItem);

            return this;
        }

        public PersonRecordPage NavigateToAboutMePage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(PersonAboutMePage);
            Click(PersonAboutMePage);

            return this;
        }

        public PersonRecordPage ValidateAboutMe_Icon(bool isPresent)
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            if (isPresent)
            {
                WaitForElementVisible(PersonAboutMePage);
            }
            else
            {
                WaitForElementNotVisible(PersonAboutMePage, 5);
            }

            Click(MenuButton);

            return this;
        }

        public PersonRecordPage ValidateAboutMeTab(bool isPresent)
        {
            WaitForElement(aboutMeSection);

            if (isPresent)
            {
                WaitForElementVisible(aboutMeSection);
            }
            else
            {
                WaitForElementNotVisible(aboutMeSection, 5);
            }
            return this;

        }

        public PersonRecordPage ValidateKnownToChildProtection_Icon(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(KnownToChildProtectionIcon);
            }
            else
            {
                WaitForElementNotVisible(KnownToChildProtectionIcon, 5);
            }
            return this;
        }

        public PersonRecordPage ValidatetitleRelatedPerson_KnownToChildProtection_Icon(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(RelatedPerson_KnownToChildProtectionIcon);
            }
            else
            {
                WaitForElementNotVisible(RelatedPerson_KnownToChildProtectionIcon, 5);
            }
            return this;
        }

        public PersonRecordPage ValidatePersonPageHeaderTitle(string ExpectedText)
        {
            WaitForElementVisible(personPageHeader);
            ScrollToElement(personPageHeader);
            ValidateElementText(personPageHeader, "Person:\r\n" + ExpectedText);
            return this;
        }

        public PersonRecordPage ClickPinToMeLinkButton()
        {
            WaitForElementToBeClickable(additionalIItemsButton);
            Click(additionalIItemsButton);

            WaitForElementToBeClickable(PinToMeButton);
            Click(PinToMeButton);

            return this;
        }

        public PersonRecordPage ClickUnPinFromMeLinkButton()
        {
            WaitForElementToBeClickable(additionalIItemsButton);
            Click(additionalIItemsButton);

            WaitForElementToBeClickable(UnPinFromMeButton);
            Click(UnPinFromMeButton);

            return this;
        }

        public PersonRecordPage NavigateToChargeApportionmentsPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(ChargeApportionmentsLeftSubMenuItem);
            Click(ChargeApportionmentsLeftSubMenuItem);

            return this;
        }

    }

}
