using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class ServiceProvisionRatePeriodRecordPage : CommonMethods
    {
        public ServiceProvisionRatePeriodRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ServiceProvisionRatePeriodRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovisionrateperiod&')]");

        readonly By pageHeader = By.XPath("//h1");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By deleteButton = By.XPath("//button[@title = 'Delete']");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");

        readonly By rateSchedules_Tab = By.Id("CWNavGroup_ServiceProvisionRateSchedules");

        #region General Fields

        readonly By serviceprovision_LinkField = By.Id("CWField_serviceprovisionid_Link");
        readonly By serviceProvision_LookupButton = By.Id("CWLookupBtn_serviceprovisionid");

        readonly By responsibleTeam_LinkField = By.Id("CWField_ownerid_Link");
        readonly By responsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

        readonly By rateUnit_Field = By.Id("CWField_rateunitid_cwname");
        readonly By rateUnit_LinkField = By.Id("CWField_rateunitid_Link");
        readonly By rateUnit_LookupButton = By.Id("CWLookupBtn_rateunitid");

        readonly By approvalStatus_Picklist = By.Id("CWField_approvalstatusid");
        readonly By approvalStatus_Pending = By.XPath("//select[@id = 'CWField_approvalstatusid']/option[@value = '1']");
        readonly By approvalStatus_Approved = By.XPath("//select[@id = 'CWField_approvalstatusid']/option[@value = '2']");
        readonly By approvalStatus_Cancelled = By.XPath("//select[@id = 'CWField_approvalstatusid']/option[@value = '3']");

        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By endDate_Field = By.Id("CWField_enddate");

        #endregion

        public ServiceProvisionRatePeriodRecordPage WaitForServiceProvisionRatePeriodRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ServiceProvisionRatePeriodRecordIFrame);
            SwitchToIframe(ServiceProvisionRatePeriodRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(pageHeader);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ClickRateUnitLookupButton()
        {
            WaitForElementToBeClickable(rateUnit_LookupButton);
            MoveToElementInPage(rateUnit_LookupButton);
            Click(rateUnit_LookupButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(responsibleTeam_LookupButton);
            MoveToElementInPage(responsibleTeam_LookupButton);
            Click(responsibleTeam_LookupButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage SelectApprovalStatus(string OptionToSelect)
        {
            WaitForElementToBeClickable(approvalStatus_Picklist);
            MoveToElementInPage(approvalStatus_Picklist);
            SelectPicklistElementByText(approvalStatus_Picklist, OptionToSelect);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage InsertStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(startDate_Field);
            MoveToElementInPage(startDate_Field);
            SendKeys(startDate_Field, TextToInsert);
            SendKeysWithoutClearing(startDate_Field, Keys.Tab);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(endDate_Field);
            MoveToElementInPage(endDate_Field);
            SendKeys(endDate_Field, TextToInsert);
            SendKeysWithoutClearing(endDate_Field, Keys.Tab);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            MoveToElementInPage(SaveButton);
            Click(SaveButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            MoveToElementInPage(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            MoveToElementInPage(deleteButton);
            Click(deleteButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateServiceProvisionLinkFieldText(string ExpectedText)
        {
            WaitForElementVisible(serviceprovision_LinkField);
            MoveToElementInPage(serviceprovision_LinkField);
            ValidateElementByTitle(serviceprovision_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateServiceProvisionLookupButtonDisabled()
        {
            WaitForElementVisible(serviceProvision_LookupButton);
            MoveToElementInPage(serviceProvision_LookupButton);
            ValidateElementDisabled(serviceProvision_LookupButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateRateUnitLinkFieldText(string ExpectedText)
        {
            WaitForElementVisible(rateUnit_LinkField);
            MoveToElementInPage(rateUnit_LinkField);
            ValidateElementByTitle(rateUnit_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateRateUnitLookupButtonDisabled()
        {
            WaitForElementVisible(rateUnit_LookupButton);
            MoveToElementInPage(rateUnit_LookupButton);
            ValidateElementDisabled(rateUnit_LookupButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateRateUnitFieldLookupEnabled()
        {
            WaitForElementVisible(rateUnit_LookupButton);
            MoveToElementInPage(rateUnit_LookupButton);
            ValidateElementNotDisabled(rateUnit_LookupButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            WaitForElementVisible(responsibleTeam_LinkField);
            MoveToElementInPage(responsibleTeam_LinkField);
            ValidateElementByTitle(responsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateResponsibleTeamFieldLookupDisabled()
        {
            WaitForElementVisible(responsibleTeam_LookupButton);
            MoveToElementInPage(responsibleTeam_LookupButton);
            ValidateElementDisabled(responsibleTeam_LookupButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateApprovalStatusFieldSelectedText(string ExpectedText)
        {
            WaitForElementVisible(approvalStatus_Picklist);
            MoveToElementInPage(approvalStatus_Picklist);
            ValidatePicklistSelectedText(approvalStatus_Picklist, ExpectedText);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateApprovalStatusFieldDisabled()
        {
            WaitForElementVisible(approvalStatus_Picklist);
            MoveToElementInPage(approvalStatus_Picklist);
            ValidateElementDisabled(approvalStatus_Picklist);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateApprovalStatusFieldEnabled()
        {
            WaitForElementVisible(approvalStatus_Picklist);
            MoveToElementInPage(approvalStatus_Picklist);
            ValidateElementNotDisabled(approvalStatus_Picklist);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateApprovalStatusPendingDisabled(bool ExpectedDisabled)
        {
            WaitForElement(approvalStatus_Pending);
            if (ExpectedDisabled)
                ValidateElementDisabled(approvalStatus_Pending);
            else
                ValidateElementNotDisabled(approvalStatus_Pending);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateApprovalStatusApprovedDisabled(bool ExpectedDisabled)
        {
            WaitForElement(approvalStatus_Approved);
            if (ExpectedDisabled)
                ValidateElementDisabled(approvalStatus_Approved);
            else
                ValidateElementNotDisabled(approvalStatus_Approved);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateApprovalStatusCancelledDisabled(bool ExpectedDisabled)
        {
            WaitForElement(approvalStatus_Cancelled);
            if (ExpectedDisabled)
                ValidateElementDisabled(approvalStatus_Cancelled);
            else
                ValidateElementNotDisabled(approvalStatus_Cancelled);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateStartDateFieldText(string ExpectedText)
        {
            WaitForElementVisible(startDate_Field);
            MoveToElementInPage(startDate_Field);
            Assert.AreEqual(ExpectedText, GetElementValue(startDate_Field));

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateStartDateFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(startDate_Field);
            MoveToElementInPage(startDate_Field);
            if (ExpectedDisabled)
                ValidateElementDisabled(startDate_Field);
            else
                ValidateElementNotDisabled(startDate_Field);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateEndDateFieldText(string ExpectedText)
        {
            WaitForElementVisible(endDate_Field);
            MoveToElementInPage(endDate_Field);
            Assert.AreEqual(ExpectedText, GetElementValue(endDate_Field));

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateEndDateFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(endDate_Field);
            MoveToElementInPage(endDate_Field);
            if (ExpectedDisabled)
                ValidateElementDisabled(endDate_Field);
            else
                ValidateElementNotDisabled(endDate_Field);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateRateScheduleTabVisibility(bool Visibility)
        {
            if (Visibility)
                WaitForElementVisible(rateSchedules_Tab);
            else
                Assert.IsFalse(GetElementVisibility(rateSchedules_Tab));

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ClickApprovalStatusPicklist()
        {
            WaitForElementToBeClickable(approvalStatus_Picklist);
            MoveToElementInPage(approvalStatus_Picklist);
            Click(approvalStatus_Picklist);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            MoveToElementInPage(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage ValidateAssignRecordButtonVisible()
        {
            WaitForElementVisible(assignRecordButton);

            return this;
        }

        public ServiceProvisionRatePeriodRecordPage NavigateToRateScheduleTab()
        {
            WaitForElementToBeClickable(rateSchedules_Tab);
            MoveToElementInPage(rateSchedules_Tab);
            Click(rateSchedules_Tab);

            return this;
        }

    }
}
