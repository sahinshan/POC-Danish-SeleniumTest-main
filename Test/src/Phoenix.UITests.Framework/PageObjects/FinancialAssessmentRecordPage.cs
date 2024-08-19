
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinancialAssessmentRecordPage : CommonMethods
    {
        public FinancialAssessmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By financialAssessmentRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=financialassessment&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By FinacialAssessentLeftSubMenuItem = By.Id("CWNavItem_CaseNotes");


        #region Top Menu

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItensButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By recalculateChargeButton = By.Id("TI_RecalculateChargeButton");
        readonly By RunOnDemandWorkflowButton = By.Id("TI_RunOnDemandWorkflow");

        #endregion

        #region Navigation Area

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");

        readonly By timelineTab = By.XPath("//li[@id='CWNavGroup_Timeline']/a[@title='Timeline']");
        readonly By detailsTab = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");
        readonly By chargesTab = By.XPath("//li[@id='CWNavGroup_Charges']/a[@title='Charges']");
        readonly By personFinancialDetailsTab = By.XPath("//li[@id='CWNavGroup_PersonFinancialDetails']/a[@title='Person Financial Details']");
        readonly By contributionsTab = By.XPath("//li[@id='CWNavGroup_Contributions']/a[@title='Contributions']");
        readonly By serviceProvisionsTab = By.XPath("//*[@id='CWNavGroup_ServiceProvision']/a");


        #region Left Sub Menu

        readonly By activitiesLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_Activities']/a");
        readonly By relatedItemsLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_RelatedItems']/a");

        #endregion

        #endregion

        #region General Area

        readonly By calculationrequiredYesRadioButton = By.Id("CWField_calculationrequired_1");
        readonly By calculationrequiredNoRadioButton = By.Id("CWField_calculationrequired_0");

        #endregion

        public FinancialAssessmentRecordPage WaitForFinancialAssessmentRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(financialAssessmentRecordIFrame);
            SwitchToIframe(financialAssessmentRecordIFrame);

            WaitForElement(backButton);

            WaitForElement(MenuButton);
            WaitForElement(timelineTab);
            WaitForElement(detailsTab);
            WaitForElement(chargesTab);
            WaitForElement(personFinancialDetailsTab);
            WaitForElement(contributionsTab);
            WaitForElement(serviceProvisionsTab);

            return this;
        }


        public FinancialAssessmentRecordPage NavigateToFinacialAssessentCaseNotesPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(activitiesLeftSubMenu);
            Click(activitiesLeftSubMenu);

            WaitForElementToBeClickable(FinacialAssessentLeftSubMenuItem);
            Click(FinacialAssessentLeftSubMenuItem);

            return this;
        }

        public FinancialAssessmentRecordPage NavigateToDetailsTab()
        {
            WaitForElementToBeClickable(detailsTab);
            Click(detailsTab);
            WaitForElementVisible(saveButton);

            return this;
        }

        public FinancialAssessmentRecordPage NavigateToServiceProvisionTab()
        {
            WaitForElementToBeClickable(serviceProvisionsTab);
            Click(serviceProvisionsTab);
            WaitForElementVisible(saveButton);

            return this;
        }

        public FinancialAssessmentRecordPage TapReCalculateButton()
        {
            WaitForElementToBeClickable(additionalItensButton);
            Click(additionalItensButton);
            WaitForElementVisible(recalculateChargeButton);
            Click(recalculateChargeButton);

            return this;
        }
        public FinancialAssessmentRecordPage TapRunOnDemandWorkflowButton()
        {
            WaitForElementToBeClickable(additionalItensButton);
            Click(additionalItensButton);
            WaitForElementVisible(RunOnDemandWorkflowButton);
            Click(RunOnDemandWorkflowButton);

            return this;
        }



        #region General Area

        public FinancialAssessmentRecordPage TapCalculationrequiredYesRadioButton()
        {
            WaitForElementToBeClickable(calculationrequiredYesRadioButton);
            Click(calculationrequiredYesRadioButton);

            return this;
        }

        public FinancialAssessmentRecordPage ValidateCalculationrequiredYesRadioButtonChecked()
        {
            this.ValidateElementChecked(calculationrequiredYesRadioButton);

            return this;
        }

        public FinancialAssessmentRecordPage ValidateCalculationrequiredNoRadioButtonChecked()
        {
            this.ValidateElementChecked(calculationrequiredNoRadioButton);

            return this;
        }

        #endregion


        public FinancialAssessmentChargesSubPage NavigateToChargesPage()
        {
            WaitForElementToBeClickable(chargesTab);

            Click(chargesTab);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return new FinancialAssessmentChargesSubPage(driver, Wait, appURL);
        }

        public FinancialAssessmentRecordPage NavigateToPersonFinancialDetailsTab()
        {
            WaitForElementToBeClickable(personFinancialDetailsTab);
            Click(personFinancialDetailsTab);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return new FinancialAssessmentRecordPage(driver, Wait, appURL);
        }

        public FinancialAssessmentRecordPage NavigateToContributionsTab()
        {
            WaitForElementToBeClickable(contributionsTab);
            Click(contributionsTab);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return new FinancialAssessmentRecordPage(driver, Wait, appURL);
        }

        public FinancialAssessmentRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            this.Click(saveButton);

            return new FinancialAssessmentRecordPage(driver, Wait, appURL);
        }

        public FinancialAssessmentRecordPage ClickSaveAndCloseButton(bool WaitForRefreshPannelNotVisible)
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            this.Click(saveAndCloseButton);

            if (WaitForRefreshPannelNotVisible)
                this.WaitForElementNotVisible("CWRefreshPanel", 7);

            return new FinancialAssessmentRecordPage(driver, Wait, appURL);
        }


    }
}
