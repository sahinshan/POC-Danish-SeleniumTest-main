using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
    public class ServiceProvidedRatePeriodRecordPage : CommonMethods
    {
        public ServiceProvidedRatePeriodRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");        
        readonly By ServiceProvidedRatePeriodRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovidedrateperiod&')]");

        readonly By pageHeader = By.XPath("//h1");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By deleteButton = By.XPath("//button[@title = 'Delete']");
        readonly By recordHeaderTitle = By.XPath("//div[@id='CWToolbar']/div/h1");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By navGroup_menuButton = By.Id("CWNavGroup_Menu");
        readonly By navSubGroupItem_RelatedItemsButton = By.Id("CWNavSubGroup_RelatedItems");
        readonly By serviceProvidedRateSchedulesButton = By.Id("CWNavItem_ServiceProvidedRateSchedule");
        readonly By serviceProvidedRateSchedulesTab = By.Id("CWNavGroup_ServiceProvisionRateSchedules");
        readonly By detailsTab = By.Id("CWNavGroup_EditForm");

        #region General Fields

        readonly By serviceProvided_Field = By.Id("CWField_serviceprovidedid_Link");
        readonly By serviceProvided_LookupButton = By.Id("CWLookupBtn_serviceprovidedid");
        readonly By responsibleTeam_Field = By.Id("CWField_ownerid_Link");
        readonly By responsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By rateUnitField_Default = By.Id("CWField_rateunitid_cwname");
        readonly By rateUnitField_Field = By.Id("CWField_rateunitid_Link");
        readonly By rateUnit_LookupButton = By.Id("CWLookupBtn_rateunitid");
        readonly By approvalStatus_Picklist = By.Id("CWField_approvalstatusid");
        readonly By approvalStatus_Pending = By.XPath("//select[@id = 'CWField_approvalstatusid']/option[@value = '1']");
        readonly By approvalStatus_Approved = By.XPath("//select[@id = 'CWField_approvalstatusid']/option[@value = '2']");
        readonly By approvalStatus_Cancelled = By.XPath("//select[@id = 'CWField_approvalstatusid']/option[@value = '3']");
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By endDate_Field = By.Id("CWField_enddate");

        #endregion

        public ServiceProvidedRatePeriodRecordPage WaitForServiceProvidedRatePeriodRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ServiceProvidedRatePeriodRecordIFrame);
            SwitchToIframe(ServiceProvidedRatePeriodRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElement(pageHeader);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ClickRateUnitLookupButton()
        {
            WaitForElementToBeClickable(rateUnit_LookupButton);
            MoveToElementInPage(rateUnit_LookupButton);
            Click(rateUnit_LookupButton);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(responsibleTeam_LookupButton);
            MoveToElementInPage(responsibleTeam_LookupButton);
            Click(responsibleTeam_LookupButton);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage SelectApprovalStatus(string OptionToSelect)
        {
            WaitForElementToBeClickable(approvalStatus_Picklist);
            MoveToElementInPage(approvalStatus_Picklist);
            SelectPicklistElementByText(approvalStatus_Picklist, OptionToSelect);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage InsertStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(startDate_Field);
            MoveToElementInPage(startDate_Field);
            SendKeys(startDate_Field, TextToInsert);
            SendKeysWithoutClearing(startDate_Field, Keys.Tab);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(endDate_Field);
            MoveToElementInPage(endDate_Field);
            SendKeys(endDate_Field, TextToInsert);
            SendKeysWithoutClearing(endDate_Field, Keys.Tab);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            MoveToElementInPage(SaveButton);
            Click(SaveButton);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            MoveToElementInPage(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            MoveToElementInPage(deleteButton);
            Click(deleteButton);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);            
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ClickRateSchedulesTab()
        {
            MoveToElementInPage(serviceProvidedRateSchedulesTab);
            WaitForElementToBeClickable(serviceProvidedRateSchedulesTab);
            Click(serviceProvidedRateSchedulesTab);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ClickDetailsTab()
        {
            MoveToElementInPage(detailsTab);
            WaitForElementToBeClickable(detailsTab);
            Click(detailsTab);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage NavigateToServiceProvidedRateScheduleMenuItem()
        {
            WaitForElementToBeClickable(navGroup_menuButton);
            MoveToElementInPage(navGroup_menuButton);            
            Click(navGroup_menuButton);

            WaitForElement(navSubGroupItem_RelatedItemsButton);
            MoveToElementInPage(navSubGroupItem_RelatedItemsButton);
            Click(navSubGroupItem_RelatedItemsButton);

            WaitForElementToBeClickable(serviceProvidedRateSchedulesButton);
            MoveToElementInPage(serviceProvidedRateSchedulesButton);
            Click(serviceProvidedRateSchedulesButton);

            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateServiceProvidedFieldText(string ExpectedText)
        {
            WaitForElementVisible(serviceProvided_Field);
            MoveToElementInPage(serviceProvided_Field);
            ValidateElementByTitle(serviceProvided_Field, ExpectedText);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateServiceProvidedFieldLookupDisabled()
        {
            WaitForElementVisible(serviceProvided_LookupButton);
            MoveToElementInPage(serviceProvided_LookupButton);
            ValidateElementDisabled(serviceProvided_LookupButton);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateRateUnitFieldText(string ExpectedText)
        {
            WaitForElementVisible(rateUnitField_Field);
            MoveToElementInPage(rateUnitField_Field);
            ValidateElementByTitle(rateUnitField_Field, ExpectedText);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateRateUnitFieldLookupDisabled()
        {
            WaitForElementVisible(rateUnit_LookupButton);
            MoveToElementInPage(rateUnit_LookupButton);
            ValidateElementDisabled(rateUnit_LookupButton);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateRateUnitFieldLookupEnabled()
        {
            WaitForElementVisible(rateUnit_LookupButton);
            MoveToElementInPage(rateUnit_LookupButton);
            ValidateElementNotDisabled(rateUnit_LookupButton);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateResponsibleTeamFieldText(string ExpectedText)
        {
            WaitForElementVisible(responsibleTeam_Field);
            MoveToElementInPage(responsibleTeam_Field);
            ValidateElementByTitle(responsibleTeam_Field, ExpectedText);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateResponsibleTeamFieldLookupDisabled()
        {
            WaitForElementVisible(responsibleTeam_LookupButton);
            MoveToElementInPage(responsibleTeam_LookupButton);
            ValidateElementDisabled(responsibleTeam_LookupButton);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateApprovalStatusFieldSelectedText(string ExpectedText)
        {
            WaitForElementVisible(approvalStatus_Picklist);
            MoveToElementInPage(approvalStatus_Picklist);
            ValidatePicklistSelectedText(approvalStatus_Picklist, ExpectedText);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateApprovalStatusFieldDisabled()
        {
            WaitForElementVisible(approvalStatus_Picklist);
            MoveToElementInPage(approvalStatus_Picklist);
            ValidateElementDisabled(approvalStatus_Picklist);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateApprovalStatusFieldEnabled()
        {
            WaitForElementVisible(approvalStatus_Picklist);
            MoveToElementInPage(approvalStatus_Picklist);
            ValidateElementNotDisabled(approvalStatus_Picklist);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateApprovalStatusPendingDisabled(bool ExpectedDisabled)
        {
            WaitForElement(approvalStatus_Pending);
            if (ExpectedDisabled)
                ValidateElementDisabled(approvalStatus_Pending);
            else
                ValidateElementNotDisabled(approvalStatus_Pending);
            
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateApprovalStatusApprovedDisabled(bool ExpectedDisabled)
        {
            WaitForElement(approvalStatus_Approved);
            if (ExpectedDisabled)
                ValidateElementDisabled(approvalStatus_Approved);
            else
                ValidateElementNotDisabled(approvalStatus_Approved);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateApprovalStatusCancelledDisabled(bool ExpectedDisabled)
        {
            WaitForElement(approvalStatus_Cancelled);
            if (ExpectedDisabled)
                ValidateElementDisabled(approvalStatus_Cancelled);
            else
                ValidateElementNotDisabled(approvalStatus_Cancelled);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateStartDateFieldText(string ExpectedText)
        {
            WaitForElementVisible(startDate_Field);
            MoveToElementInPage(startDate_Field);
            Assert.AreEqual(ExpectedText, GetElementValue(startDate_Field));
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateStartDateFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(startDate_Field);
            MoveToElementInPage(startDate_Field);
            if (ExpectedDisabled)
                ValidateElementDisabled(startDate_Field);
            else
                ValidateElementNotDisabled(startDate_Field);
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateEndDateFieldText(string ExpectedText)
        {
            WaitForElementVisible(endDate_Field);
            MoveToElementInPage(endDate_Field);
            Assert.AreEqual(ExpectedText, GetElementValue(endDate_Field));
            return this;
        }

        public ServiceProvidedRatePeriodRecordPage ValidateEndDateFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(endDate_Field);
            MoveToElementInPage(endDate_Field);
            if (ExpectedDisabled)
                ValidateElementDisabled(endDate_Field);
            else
                ValidateElementNotDisabled(endDate_Field);
            return this;
        }
    }
}
