
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServiceProvisionRecordPage : CommonMethods
    {
        public ServiceProvisionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By financialAssessmentRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovision&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By timelinePanelFrame = By.Id("CWTimelinePanel_IFrame");
        readonly By cwDataFormDialogFrame = By.Id("iframe_CWDataFormDialog");

        #region Top Menu

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItensButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By calculateCostPerWeekButton = By.Id("TI_CalculateCostPerWeek");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By UpdateGLCodeButton = By.XPath("//*[@id='TI_UpdateGLCode']");

        #endregion

        #region Navigation Area

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/a[@title='Menu']");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");

        readonly By timelineTab = By.XPath("//li[@id='CWNavGroup_Timeline']/a[@title='Timeline']");
        readonly By detailsTab = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");
        readonly By serviceDeliveriesTab = By.XPath("//li[@id='CWNavGroup_ServiceDeliveries']/a[@title='Service Deliveries']");
        readonly By serviceDeliveryVariationTab = By.XPath("//li[@id='CWNavGroup_ServiceDeliveryVariations']/a[@title='Service Delivery Variations']");
        readonly By costsPerWeekTab = By.XPath("//li[@id='CWNavGroup_CostsPerWeek']/a[@title='Costs Per Week']");
        readonly By financeTransactionsTab = By.XPath("//*[@id='CWNavGroup_FinanceTransactions']/a");
        readonly By AllowancesTab = By.XPath("//*[@id='CWNavGroup_Allowances']/a");
        readonly By ratePeriodsTab = By.Id("CWNavGroup_ServiceProvisionRatePeriods");

        #region Left Sub Menu

        readonly By auditButtonLeftSubMenu = By.XPath("//li[@id='CWNavItem_AuditHistory']/a");
        readonly By contributionsButtonLeftSubMenu = By.XPath("//li[@id='CWNavItem_Contributions']/a");

        #endregion

        #endregion

        #region Details Tab

        readonly By plannedEndDateTextBox = By.Id("CWField_plannedenddate");
        readonly By plannedStartDate_Field = By.Id("CWField_plannedstartdate");
        readonly By actualEndDateTextBox = By.Id("CWField_actualenddate");
        readonly By actualEndDateDatePicker = By.Id("CWField_actualenddate_DatePicker");
        readonly By actualStartDate_Field = By.Id("CWField_actualstartdate");
        readonly By endReasonLookupButton = By.Id("CWLookupBtn_serviceprovisionendreasonid");
        readonly By endReason_Field = By.XPath("//*[@id='CWLabelHolder_serviceprovisionendreasonid']/label[text()='End Reason']");
        readonly By brokerageEpisodeField_Link = By.Id("CWField_brokerageepisodeid_Link");
        readonly By brokerageofferField_Link = By.Id("CWField_brokerageofferid_Link");
        readonly By brokerageofferField_Label = By.Id("CWLabelHolder_brokerageofferid");
        readonly By startReasonField_Link = By.Id("CWField_serviceprovisionstartreasonid_Link");
        readonly By endReasonField_Link = By.Id("CWField_serviceprovisionendreasonid_Link");
        readonly By responsibleuser_Lookup = By.Id("CWLookupBtn_responsibleuserid");
        readonly By serviceProvisionStatus_LookUp = By.Id("CWLookupBtn_serviceprovisionstatusid");
        readonly By serviceProvisionStatus_LinkField = By.Id("CWField_serviceprovisionstatusid_Link");
        readonly By purchasingTeam_LookUp = By.Id("CWLookupBtn_purchasingteamid");
        readonly By purchasingTeam_LinkField = By.Id("CWField_purchasingteamid_Link");
        readonly By purchasingTeam_DefaultBlankField = By.Id("CWField_purchasingteamid_cwname");
        readonly By approvedCareType_LookupButton = By.Id("CWLookupBtn_approvedcaretypeid");
        readonly By approvedCareType_LinkField = By.Id("CWField_approvedcaretypeid_Link");
        readonly By approvedCareTypeField_ClearLookup = By.Id("CWClearLookup_approvedcaretypeid");
        readonly By brokerageofferid_FieldLookup = By.Id("CWLookupBtn_brokerageofferid");
        readonly By brokerageepisodeid_FieldLookup = By.Id("CWLookupBtn_brokerageepisodeid");
        readonly By brokerageEpisodeField_ClearLookup = By.Id("CWClearLookup_brokerageepisodeid");
        readonly By serviceelement1id_FieldLookup = By.Id("CWLookupBtn_serviceelement1id");
        readonly By serviceelement1_LinkField = By.Id("CWField_serviceelement1id_Link");
        readonly By startReason_FieldLookup = By.Id("CWLookupBtn_serviceprovisionstartreasonid");
        readonly By serviceelement2id_FieldLookup = By.Id("CWLookupBtn_serviceelement2id");
        readonly By serviceelement2_FieldLabel = By.Id("CWLabelHolder_serviceelement2id");
        readonly By serviceelement2_LinkField = By.Id("CWField_serviceelement2id_Link");
        readonly By glCode_FieldLabel = By.Id("CWLabelHolder_glcode");
        readonly By glCode_Field = By.Id("CWField_glcode");
        readonly By rateUnit_FieldLabel = By.Id("CWLabelHolder_rateunitid");
        readonly By rateunitField_Link = By.Id("CWField_rateunitid_Link");

        readonly By RateUnit_LinkField = By.Id("CWField_rateunitid_Link");
        readonly By RateUnit_LookUpButton = By.Id("CWLookupBtn_rateunitid");
        readonly By RateUnit_RemoveButton = By.Id("CWClearLookup_rateunitid");

        readonly By ServiceProvided_LinkField = By.Id("CWField_serviceprovidedid_Link");
        readonly By ServiceProvided_LookUpButton = By.Id("CWLookupBtn_serviceprovidedid");
        readonly By ServiceProvided_RemoveButton = By.Id("CWClearLookup_serviceprovidedid");

        readonly By ProviderCarer_LinkField = By.Id("CWField_providerid_Link");
        readonly By ProviderCarer_LookUpButton = By.Id("CWLookupBtn_providerid");
        readonly By ProviderCarer_RemoveButton = By.Id("CWClearLookup_providerid");

        readonly By CareTypeLookupButton = By.Id("CWLookupBtn_caretypeid");
        readonly By CareType_LinkField = By.Id("CWField_caretypeid_Link");
        readonly By CareType_RemoveButton = By.Id("CWClearLookup_caretypeid");

        readonly By RateRequired_Yes = By.Id("CWField_raterequired_1");
        readonly By RateRequired_No = By.Id("CWField_raterequired_0");


        #endregion

        public ServiceProvisionRecordPage WaitForServiceProvisionRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(financialAssessmentRecordIFrame);
            SwitchToIframe(financialAssessmentRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 50);

            WaitForElement(backButton);

            WaitForElement(MenuButton);
            WaitForElement(timelineTab);
            WaitForElement(detailsTab);            

            return this;
        }

        public ServiceProvisionRecordPage WaitForNewServiceProvisionRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(financialAssessmentRecordIFrame);
            SwitchToIframe(financialAssessmentRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(backButton);

            return this;
        }


        public ServiceProvisionRecordPage WaitForServiceProvisionRecordPageToLoadFromAdvanceSearch(string ServiceProvisionRecordTitle)
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(cwDataFormDialogFrame);
            SwitchToIframe(cwDataFormDialogFrame);

            //WaitForElement(financialAssessmentRecordIFrame);
            //SwitchToIframe(financialAssessmentRecordIFrame);

            WaitForElement(timelinePanelFrame);
            SwitchToIframe(timelinePanelFrame);

            WaitForElement(pageHeader);


            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);



            if (driver.FindElement(pageHeader).Text != "Service Provision: \r\n" + ServiceProvisionRecordTitle)
                throw new Exception("Page title do not equals:Service Provision:\r\n" + ServiceProvisionRecordTitle);

            return this;
        }

        public ServiceProvisionRecordPage NavigateToDetailsTab()
        {
            MoveToElementInPage(detailsTab);
            WaitForElementToBeClickable(detailsTab);
            Click(detailsTab);

            return this;
        }

        public ServiceProvisionRecordPage NavigateToServiceDeliveriesTab()
        {
            MoveToElementInPage(serviceDeliveriesTab);
            WaitForElementToBeClickable(serviceDeliveriesTab);
            Click(serviceDeliveriesTab);

            return this;
        }

        public ServiceProvisionRecordPage NavigateToCostsPerWeekTab()
        {
            MoveToElementInPage(costsPerWeekTab);
            WaitForElementToBeClickable(costsPerWeekTab);
            Click(costsPerWeekTab);

            return this;
        }

        public ServiceProvisionRecordPage NavigateToAllowancesTab()
        {
            MoveToElementInPage(AllowancesTab);
            WaitForElementToBeClickable(AllowancesTab);
            Click(AllowancesTab);

            return this;
        }

        public ServiceProvisionRecordPage InsertPlannedEndDate(string ValueToInsert)
        {
            WaitForElementToBeClickable(plannedEndDateTextBox);
            MoveToElementInPage(plannedEndDateTextBox);
            SendKeys(plannedEndDateTextBox, ValueToInsert);
            SendKeysWithoutClearing(plannedEndDateTextBox, Keys.Tab);

            return this;
        }

        public ServiceProvisionRecordPage InsertPlannedStartDate(string dateToInsert)
        {
            WaitForElementToBeClickable(plannedStartDate_Field);
            MoveToElementInPage(plannedStartDate_Field);
            SendKeys(plannedStartDate_Field, dateToInsert);

            return this;
        }

        public ServiceProvisionRecordPage ClickPlannedEndDate()
        {
            WaitForElementToBeClickable(plannedEndDateTextBox);
            MoveToElementInPage(plannedEndDateTextBox);
            Click(plannedEndDateTextBox);

            return this;
        }

        public ServiceProvisionRecordPage ClickServiceElement1LookupButton()
        {
            MoveToElementInPage(serviceelement1id_FieldLookup);
            WaitForElementToBeClickable(serviceelement1id_FieldLookup);
            Click(serviceelement1id_FieldLookup);

            return this;
        }

        public ServiceProvisionRecordPage ClickServiceElement1LinkField()
        {
            MoveToElementInPage(serviceelement1_LinkField);
            WaitForElementToBeClickable(serviceelement1_LinkField);
            Click(serviceelement1_LinkField);

            return this;
        }

        public ServiceProvisionRecordPage ClickServiceElement2LookupButton()
        {
            MoveToElementInPage(serviceelement2id_FieldLookup);
            WaitForElementToBeClickable(serviceelement2id_FieldLookup);
            Click(serviceelement2id_FieldLookup);

            return this;
        }

        public ServiceProvisionRecordPage ClickCareTypeLookupButton()
        {
            MoveToElementInPage(CareTypeLookupButton);
            WaitForElementToBeClickable(CareTypeLookupButton);
            Click(CareTypeLookupButton);

            return this;
        }

        public ServiceProvisionRecordPage ClickStartReasonLookupButton()
        {
            WaitForElementToBeClickable(startReason_FieldLookup);
            MoveToElementInPage(startReason_FieldLookup);
            Click(startReason_FieldLookup);

            return this;
        }

        public ServiceProvisionRecordPage ClickStartReasonFieldLink()
        {
            WaitForElementToBeClickable(startReasonField_Link);
            MoveToElementInPage(startReasonField_Link);
            Click(startReasonField_Link);

            return this;
        }

        public ServiceProvisionRecordPage ClickResponsibleUserLookUpButton()
        {
            WaitForElementToBeClickable(responsibleuser_Lookup);
            MoveToElementInPage(responsibleuser_Lookup);
            Click(responsibleuser_Lookup);

            return this;
        }

        public ServiceProvisionRecordPage ClickStatusLookUpButton()
        {
            WaitForElementToBeClickable(serviceProvisionStatus_LookUp);
            MoveToElementInPage(serviceProvisionStatus_LookUp);
            Click(serviceProvisionStatus_LookUp);

            return this;
        }

        public ServiceProvisionRecordPage ClickPurchasingteamLookUpButton()
        {
            WaitForElementToBeClickable(purchasingTeam_LookUp);
            MoveToElementInPage(purchasingTeam_LookUp);
            Click(purchasingTeam_LookUp);

            return this;
        }

        public ServiceProvisionRecordPage ClickServiceProvidedLookupButton()
        {
            WaitForElementToBeClickable(ServiceProvided_LookUpButton);
            MoveToElementInPage(ServiceProvided_LookUpButton);
            Click(ServiceProvided_LookUpButton);

            return this;
        }

        public ServiceProvisionRecordPage ClickApprovedCareTypeLookupButton()
        {
            WaitForElementToBeClickable(approvedCareType_LookupButton);
            MoveToElementInPage(approvedCareType_LookupButton);
            Click(approvedCareType_LookupButton);

            return this;
        }

        public ServiceProvisionRecordPage ClickActualEndDate_DatePicker()
        {
            WaitForElementToBeClickable(actualEndDateDatePicker);
            Click(actualEndDateDatePicker);

            return this;
        }

        public ServiceProvisionRecordPage InsertActualEndDate(string ValueToInsert)
        {
            WaitForElementToBeClickable(actualEndDateTextBox);
            SendKeys(actualEndDateTextBox, ValueToInsert);
            SendKeysWithoutClearing(actualEndDateTextBox, Keys.Tab);

            return this;
        }

        public ServiceProvisionRecordPage ClearActualEndDate()
        {
            MoveToElementInPage(actualEndDateTextBox);
            ClearText(actualEndDateTextBox);
            return this;
        }

        public ServiceProvisionRecordPage InsertActualStartDate(string dateToInsert)
        {
            WaitForElementToBeClickable(actualStartDate_Field);
            MoveToElementInPage(actualStartDate_Field);
            SendKeys(actualStartDate_Field, dateToInsert);

            return this;
        }

        public ServiceProvisionRecordPage TapEndReasonLookupButton()
        {
            WaitForElementToBeClickable(endReasonLookupButton);
            MoveToElementInPage(endReasonLookupButton);
            Click(endReasonLookupButton);

            return this;
        }

        public ServiceProvisionRecordPage ClickSaveButton()
        {
            MoveToElementInPage(saveButton);
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }

        public ServiceProvisionRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);
            return this;
        }

        public ServiceProvisionRecordPage TapSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementNotVisible(financialAssessmentRecordIFrame, 7);

            return this;
        }

        public ServiceProvisionRecordPage ValidateServiceProvisionPageHeader(string ExpectedText)
        {
            WaitForElement(pageHeader);
            string ActualText = GetElementByAttributeValue(pageHeader, "title");
            Assert.AreEqual("Service Provision: " + ExpectedText, ActualText);

            return this;
        }

        public ServiceProvisionRecordPage ClickUpdateGLCodeButton()
        {
            WaitForElementToBeClickable(additionalItensButton);
            Click(additionalItensButton);

            WaitForElementToBeClickable(UpdateGLCodeButton);
            Click(UpdateGLCodeButton);

            return this;
        }

        public ServiceProvisionRecordPage ValidateEndReasonFieldVisibility(bool ExpectVisible)
        {


            if (ExpectVisible)
            {
                WaitForElementVisible(endReason_Field);
            }
            else
            {
                WaitForElementNotVisible(endReason_Field, 3);
            }


            return this;
        }

        public ServiceProvisionRecordPage ValidateStartReasonFieldLinkVisibility(bool ExpectVisible)
        {


            if (ExpectVisible)
            {
                WaitForElementVisible(startReasonField_Link);
            }
            else
            {
                WaitForElementNotVisible(startReasonField_Link, 3);
            }


            return this;
        }

        public ServiceProvisionRecordPage ValidateStartReasonFieldLinkText(string ExpectedText)
        {
            WaitForElementVisible(startReasonField_Link);
            ValidateElementText(startReasonField_Link, ExpectedText);


            return this;
        }

        public ServiceProvisionRecordPage ValidateActualStartDateFieldText(String ExpectedDate)
        {
            WaitForElementVisible(actualStartDate_Field);
            ValidateElementValue(actualStartDate_Field, ExpectedDate);

            return this;
        }

        public ServiceProvisionRecordPage ValidateActualEndDateFieldText(String ExpectedDate)
        {
            WaitForElementVisible(actualEndDateTextBox);
            ValidateElementValue(actualEndDateTextBox, ExpectedDate);

            return this;
        }

        public ServiceProvisionRecordPage ValidateEndReasonFieldLinkVisibility(bool ExpectVisible)
        {


            if (ExpectVisible)
            {
                WaitForElementVisible(endReasonField_Link);
            }
            else
            {
                WaitForElementNotVisible(endReasonField_Link, 3);
            }


            return this;
        }

        public ServiceProvisionRecordPage ValidateEndReasonFieldLinkText(string ExpectedText)
        {
            WaitForElementVisible(endReasonField_Link);
            ValidateElementText(endReasonField_Link, ExpectedText);


            return this;
        }

        public ServiceProvisionRecordPage ValidateBrokerageEpisodeFieldText(string ExpectedText)
        {
            WaitForElementVisible(brokerageEpisodeField_Link);
            ValidateElementTextContainsText(brokerageEpisodeField_Link, ExpectedText);

            return this;
        }

        public ServiceProvisionRecordPage ValidateBrokerageOfferFieldText(string ExpectedText)
        {
            WaitForElementVisible(brokerageofferField_Link);
            ValidateElementTextContainsText(brokerageofferField_Link, ExpectedText);

            return this;
        }

        public ServiceProvisionRecordPage ValidateRateUnitFieldText(string ExpectedText)
        {
            WaitForElementVisible(rateunitField_Link);
            ValidateElementTextContainsText(rateunitField_Link, ExpectedText);

            return this;
        }

        public ServiceProvisionRecordPage ValidateGLCodeFieldText(string ExpectedText)
        {
            WaitForElementVisible(glCode_Field);
            ValidateElementValue(glCode_Field, ExpectedText);

            return this;
        }

        public ServiceProvisionRecordPage ValidateBrokerageEpisodeFieldLinkVisibility(bool ExpectVisible)
        {


            if (ExpectVisible)
            {
                WaitForElementVisible(brokerageEpisodeField_Link);
            }
            else
            {
                WaitForElementNotVisible(brokerageEpisodeField_Link, 3);
            }


            return this;
        }

        public ServiceProvisionRecordPage ValidateBrokerageOfferFieldLabelVisibility(bool ExpectVisible)
        {


            if (ExpectVisible)
            {
                WaitForElementVisible(brokerageofferField_Label);
            }
            else
            {
                WaitForElementNotVisible(brokerageofferField_Label, 3);
            }


            return this;
        }

        public ServiceProvisionRecordPage ValidateBrokerageOfferFieldLookupEnabled(bool expectEnabled)
        {
            WaitForElementVisible(brokerageofferid_FieldLookup);
            if (expectEnabled)
                ValidateElementEnabled(brokerageofferid_FieldLookup);
            else
                ValidateElementDisabled(brokerageofferid_FieldLookup);

            return this;
        }

        public ServiceProvisionRecordPage ValidateBrokerageEpisodeFieldLookupEnabled(bool expectEnabled)
        {
            WaitForElementVisible(brokerageofferid_FieldLookup);
            if (expectEnabled)
                ValidateElementEnabled(brokerageofferid_FieldLookup);
            else
                ValidateElementDisabled(brokerageofferid_FieldLookup);

            return this;
        }

        public ServiceProvisionRecordPage ValidateServiceElement2FieldLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(serviceelement2_FieldLabel);
            }
            else
            {
                WaitForElementNotVisible(serviceelement2_FieldLabel, 3);
            }
            return this;
        }

        public ServiceProvisionRecordPage ValidateServiceProvisionStatusLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(serviceProvisionStatus_LinkField);
            MoveToElementInPage(serviceProvisionStatus_LinkField);
            ValidateElementText(serviceProvisionStatus_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvisionRecordPage ValidateRateUnitFieldLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(RateUnit_LinkField);
            ValidateElementByTitle(RateUnit_LinkField, ExpectedText);

            return this;
        }
        public ServiceProvisionRecordPage ValidateServiceProvidedFieldLinkText(string ExpectedText)
        {
            WaitForElement(ServiceProvided_LinkField);
            ValidateElementText(ServiceProvided_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvisionRecordPage ValidateApprovedCareTypeFieldLinkText(string ExpectedText)
        {
            WaitForElement(approvedCareType_LinkField);
            ValidateElementText(approvedCareType_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvisionRecordPage ValidateProviderCarerFieldLinkText(string ExpectedText)
        {
            WaitForElement(ProviderCarer_LinkField);
            ValidateElementText(ProviderCarer_LinkField, ExpectedText);


            return this;
        }

        public ServiceProvisionRecordPage ValidateCareTypeFieldLinkText(string ExpectedText)
        {
            MoveToElementInPage(CareType_LinkField);
            WaitForElement(CareType_LinkField);
            ValidateElementByTitle(CareType_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvisionRecordPage ValidateServiceElement1FieldLinkText(string ExpectedText)
        {
            MoveToElementInPage(serviceelement1_LinkField);
            WaitForElementToBeClickable(serviceelement1_LinkField);
            ValidateElementByTitle(serviceelement1_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvisionRecordPage ValidateServiceElement2FieldLinkText(string ExpectedText)
        {
            MoveToElementInPage(serviceelement2_LinkField);
            WaitForElement(serviceelement2_LinkField);
            ValidateElementByTitle(serviceelement2_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvisionRecordPage ValidateGlCodeFieldLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(glCode_FieldLabel);
            }
            else
            {
                WaitForElementNotVisible(glCode_FieldLabel, 3);
            }

            return this;
        }

        public ServiceProvisionRecordPage ValidateRateUnitFieldLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(rateUnit_FieldLabel);
            }
            else
            {
                WaitForElementNotVisible(rateUnit_FieldLabel, 3);
            }

            return this;
        }

        public ServiceProvisionRecordPage ClickBackButton()
        {

            WaitForElementVisible(Back_Button);
            Click(Back_Button);

            return this;
        }

        public ServiceProvisionRecordPage ClickBrokerageEpisodeLookupButton()
        {
            WaitForElementToBeClickable(brokerageepisodeid_FieldLookup);
            Click(brokerageepisodeid_FieldLookup);

            return this;
        }

        public ServiceProvisionRecordPage ClickBrokerageEpisodeClearLookupButton()
        {
            WaitForElementToBeClickable(brokerageEpisodeField_ClearLookup);
            MoveToElementInPage(brokerageEpisodeField_ClearLookup);
            Click(brokerageEpisodeField_ClearLookup);

            return this;
        }

        public ServiceProvisionRecordPage ClickCareTypeClearLookupButton()
        {
            MoveToElementInPage(CareType_RemoveButton);
            WaitForElementToBeClickable(CareType_RemoveButton);
            Click(CareType_RemoveButton);

            return this;
        }

        public ServiceProvisionRecordPage NavigateToRatePeriodsTab()
        {
            WaitForElementToBeClickable(ratePeriodsTab);
            MoveToElementInPage(ratePeriodsTab);
            Click(ratePeriodsTab);

            return this;

        }

        public ServiceProvisionRecordPage ValidateRatePeriodsTabVisibility(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
                WaitForElement(ratePeriodsTab);

            Assert.AreEqual(ExpectedVisibility, GetElementVisibility(ratePeriodsTab));

            return this;
        }

        public ServiceProvisionRecordPage ClickRateUnitLookupButton()
        {
            WaitForElementToBeClickable(RateUnit_LookUpButton);
            MoveToElementInPage(RateUnit_LookUpButton);
            Click(RateUnit_LookUpButton);

            return this;
        }

        public ServiceProvisionRecordPage ValidatePlannedStartDateValue(string ExpectedDate)
        {
            WaitForElement(plannedStartDate_Field);
            MoveToElementInPage(plannedStartDate_Field);
            ValidateElementValue(plannedStartDate_Field, ExpectedDate);

            return this;
        }

        public ServiceProvisionRecordPage ValidatePlannedEndDateFieldValue(string ExpectedDate)
        {
            WaitForElement(plannedEndDateTextBox);
            MoveToElementInPage(plannedEndDateTextBox);
            ValidateElementValue(plannedEndDateTextBox, ExpectedDate);

            return this;
        }

        public ServiceProvisionRecordPage ValidatePlannedStartDateFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElement(plannedStartDate_Field);
            MoveToElementInPage(plannedStartDate_Field);
            if (ExpectedDisabled)
                ValidateElementDisabled(plannedStartDate_Field);
            else
                ValidateElementNotDisabled(plannedStartDate_Field);

            return this;
        }

        public ServiceProvisionRecordPage ValidatePlannedEndDateFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(plannedEndDateTextBox);
            MoveToElementInPage(plannedEndDateTextBox);
            if (ExpectedDisabled)
                ValidateElementDisabled(plannedEndDateTextBox);
            else
                ValidateElementNotDisabled(plannedEndDateTextBox);

            return this;
        }

        public ServiceProvisionRecordPage ValidateActualStartDateFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElement(actualStartDate_Field);
            MoveToElementInPage(actualStartDate_Field);
            if (ExpectedDisabled)
                ValidateElementDisabled(actualStartDate_Field);
            else
                ValidateElementNotDisabled(actualStartDate_Field);

            return this;
        }

        public ServiceProvisionRecordPage ValidateActualEndDateFieldDisabled(bool ExpectedDisabled)
        {
            WaitForElementVisible(actualEndDateTextBox);
            MoveToElementInPage(actualEndDateTextBox);
            if (ExpectedDisabled)
                ValidateElementDisabled(actualEndDateTextBox);
            else
                ValidateElementNotDisabled(actualEndDateTextBox);

            return this;
        }

        public ServiceProvisionRecordPage ValidateRateUnitLookupButtonDisabled()
        {
            ValidateElementDisabled(RateUnit_LookUpButton);

            return this;
        }

        public ServiceProvisionRecordPage ValidateRateRequiredYesSelected()
        {
            MoveToElementInPage(RateRequired_Yes);
            ValidateElementChecked(RateRequired_Yes);
            ValidateElementNotChecked(RateRequired_No);

            return this;
        }

        public ServiceProvisionRecordPage ValidateRateRequiredNoSelected()
        {
            MoveToElementInPage(RateRequired_Yes);
            ValidateElementChecked(RateRequired_No);
            ValidateElementNotChecked(RateRequired_Yes);            

            return this;
        }

        public ServiceProvisionRecordPage ValidateServiceDeliveriesTabIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementToBeClickable(serviceDeliveriesTab);
                MoveToElementInPage(serviceDeliveriesTab);
            }
            else
            {
                WaitForElementNotVisible(serviceDeliveriesTab, 3);
            }
            Assert.AreEqual(ExpectedDisplayed, GetElementVisibility(serviceDeliveriesTab));

            return this;
        }

        public ServiceProvisionRecordPage ValidatePurchasingTeamFieldLinkText(string ExpectedText)
        {
            MoveToElementInPage(purchasingTeam_LinkField);
            WaitForElementToBeClickable(purchasingTeam_LinkField);
            ValidateElementByTitle(purchasingTeam_LinkField, ExpectedText);

            return this;
        }

        public ServiceProvisionRecordPage ValidatePurchasingTeamFieldBlankFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(purchasingTeam_DefaultBlankField);
            }
            else
            {
                WaitForElementNotVisible(purchasingTeam_DefaultBlankField, 3);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(purchasingTeam_DefaultBlankField));

            return this;
        }

        public ServiceProvisionRecordPage ValidateAllowancesTabIsVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                MoveToElementInPage(AllowancesTab);
                WaitForElementToBeClickable(AllowancesTab);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(AllowancesTab));

            return this;
        }

        public ServiceProvisionRecordPage ClickFinanceTransactionsTab()
        {
            WaitForElementToBeClickable(financeTransactionsTab);
            MoveToElementInPage(financeTransactionsTab);
            Click(financeTransactionsTab);

            return this;
        }

    }
}
