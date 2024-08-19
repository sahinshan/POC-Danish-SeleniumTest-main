using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.WebAppAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServiceElement1RecordPage : CommonMethods
    {
        public ServiceElement1RecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_ServiceElement1Frame = By.Id("iframe_serviceelement1");
        readonly By cwDialog_ServiceElement1Frame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceelement1')]");

        #region option toolbar
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        #endregion

        readonly By ServiceElement1RecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By serviceGLCodings_Tab = By.Id("CWNavGroup_ServiceGLCodings");
        readonly By servicePermission_Tab = By.Id("CWNavGroup_ServicePermissions");
        readonly By serviceMappings_Tab = By.Id("CWNavGroup_ServiceMappings");
        readonly By GLCodeMappings_Tab = By.Id("CWNavGroup_GLCodeMappings");

        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        #region Top Menu

        readonly By TopLeftMenu = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");

        readonly By AuditSubMenuLink = By.XPath("//*[@id='CWNavItem_AuditHistory']");
        readonly By GLCodeMappingSubMenuLink = By.XPath("//*[@id='CWNavItem_GLCodeMapping']");
        readonly By ServiceGLCodingSubMenuLink = By.XPath("//*[@id='CWNavItem_ServiceGLCoding']");
        readonly By ServiceMappingSubMenuLink = By.XPath("//*[@id='CWNavItem_ServiceMapping']");
        readonly By ServicePermissionSubMenuLink = By.XPath("//*[@id='CWNavItem_ServicePermission']");

        #endregion

        #region General area

        readonly By Name_FieldLabel = By.XPath("//*[@id='CWLabelHolder_name']/label");
        readonly By Name_Field = By.XPath("//*[@id='CWField_name']");

        readonly By Code_FieldLabel = By.XPath("//*[@id='CWLabelHolder_code']/label");
        readonly By Code_Field = By.XPath("//*[@id='CWField_code']");

        readonly By GovCode_FieldLabel = By.XPath("//*[@id='CWLabelHolder_govcode']/label");
        readonly By GovCode_Field = By.XPath("//*[@id='CWField_govcode']");

        readonly By Inactive_FieldLabel = By.XPath("//*[@id='CWLabelHolder_inactive']/label");
        readonly By Inactive_YesRadioButton = By.XPath("//*[@id='CWField_inactive_1']");
        readonly By Inactive_NoRadioButton = By.XPath("//*[@id='CWField_inactive_0']");

        readonly By WhoToPay_FieldLabel = By.XPath("//*[@id='CWLabelHolder_whotopayid']/label");
        readonly By WhoToPay_Field = By.XPath("//*[@id='CWField_whotopayid']");

        readonly By PaymentsCommence_FieldLabel = By.XPath("//*[@id='CWLabelHolder_paymentscommenceid']/label");
        readonly By PaymentsCommence_Field = By.XPath("//*[@id='CWField_paymentscommenceid']");

        readonly By DefaultStartReason_FieldLabel = By.XPath("//*[@id='CWLabelHolder_serviceprovisionstartreasonid']/label");
        readonly By DefaultStartReason_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_serviceprovisionstartreasonid']");
        readonly By DefaultStartReason_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_serviceprovisionstartreasonid']");
        readonly By DefaultStartReason_LinkField = By.XPath("//*[@id='CWField_serviceprovisionstartreasonid_Link']");

        readonly By DefaultEndReason_FieldLabel = By.XPath("//*[@id='CWLabelHolder_serviceprovisionendreasonid']/label");
        readonly By DefaultEndReason_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_serviceprovisionendreasonid']");
        readonly By DefaultEndReason_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_serviceprovisionendreasonid']");
        readonly By DefaultEndReason_LinkField = By.XPath("//*[@id='CWField_serviceprovisionendreasonid_Link']");

        readonly By UsedInFinance_FieldLabel = By.XPath("//*[@id='CWLabelHolder_usedinfinance']/label");
        readonly By UsedInFinance_YesRadioButton = By.XPath("//*[@id='CWField_usedinfinance_1']");
        readonly By UsedInFinance_NoRadioButton = By.XPath("//*[@id='CWField_usedinfinance_0']");

        readonly By StartDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By StartDate_Field = By.XPath("//*[@id='CWField_startdate']");

        readonly By EndDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_enddate']/label");
        readonly By EndDate_Field = By.XPath("//*[@id='CWField_enddate']");

        readonly By ValidForExport_FieldLabel = By.XPath("//*[@id='CWLabelHolder_validforexport']/label");
        readonly By ValidForExport_YesRadioButton = By.XPath("//*[@id='CWField_validforexport_1']");
        readonly By ValidForExport_NoRadioButton = By.XPath("//*[@id='CWField_validforexport_0']");

        readonly By ResponsibleTeam_FieldLabel = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ResponsibleTeam_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");

        readonly By EndDateRequired_FieldLabel = By.XPath("//*[@id='CWLabelHolder_enddaterequired']/label");
        readonly By EndDateRequired_YesRadioButton = By.XPath("//*[@id='CWField_enddaterequired_1']");
        readonly By EndDateRequired_NoRadioButton = By.XPath("//*[@id='CWField_enddaterequired_0']");

        readonly By LACLegalStatusApplies_FieldLabel = By.XPath("//*[@id='CWLabelHolder_legalstatusapplies']/label");
        readonly By LACLegalStatusApplies_YesRadioButton = By.XPath("//*[@id='CWField_legalstatusapplies_1']");
        readonly By LACLegalStatusApplies_NoRadioButton = By.XPath("//*[@id='CWField_legalstatusapplies_0']");

        readonly By MaximumCapacityApplies_FieldLabel = By.XPath("//*[@id='CWLabelHolder_checkmaximumcapacity']/label");
        readonly By MaximumCapacityApplies_YesRadioButton = By.XPath("//*[@id='CWField_checkmaximumcapacity_1']");
        readonly By MaximumCapacityApplies_NoRadioButton = By.XPath("//*[@id='CWField_checkmaximumcapacity_0']");

        readonly By ExemptionOrExtensionRulesApply_FieldLabel = By.XPath("//*[@id='CWLabelHolder_checkrules']/label");
        readonly By ExemptionOrExtensionRulesApply_YesRadioButton = By.XPath("//*[@id='CWField_checkrules_1']");
        readonly By ExemptionOrExtensionRulesApply_NoRadioButton = By.XPath("//*[@id='CWField_checkrules_0']");

        readonly By ValidRateUnits_FieldLabel = By.XPath("//*[@id='CWLabelHolder_validrateunits']/label");
        readonly By ValidRateUnits_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_validrateunits']");
        readonly By ValidRateUnits_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_validrateunits']");
        readonly By ValidRateUnits_LinkField = By.XPath("//*[@id='CWField_validrateunits_List']");
        By ValidRateUnitOption_Field(string Identifier) => By.XPath("//*[@id='MS_validrateunits_" + Identifier + "']");
        By ValidRateUnitOption_RemoveButton(string Identifier) => By.XPath("//*[@id='MS_validrateunits_" + Identifier + "']/a[text()='Remove']");



        readonly By DefaultRateUnit_FieldLabel = By.XPath("//*[@id='CWLabelHolder_rateunitid']/label");
        readonly By DefaultRateUnit_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_rateunitid']");
        readonly By DefaultRateUnit_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_rateunitid']");
        readonly By DefaultRateUnit_LinkField = By.XPath("//*[@id='CWField_rateunitid_Link']");

        readonly By GLCode_FieldLabel = By.XPath("//*[@id='CWLabelHolder_glcodeid']/label");
        readonly By GLCode_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_glcodeid']");
        readonly By GLCode_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_glcodeid']");
        readonly By GLCode_LinkField = By.XPath("//*[@id='CWField_glcodeid_Link']");

        readonly By Notes_FieldLabel = By.XPath("//*[@id='CWLabelHolder_notes']/label");
        readonly By Notes_Field = By.XPath("//*[@id='CWField_notes']");

        readonly By Capacity_FieldLabel = By.XPath("//li[@id = 'CWLabelHolder_capacity']/label");
        readonly By Capacity_Field = By.Id("CWField_capacity");

        #endregion


        #region Batching Information Area
        readonly By PaymentType_FieldLabel = By.XPath("//*[@id='CWLabelHolder_paymenttypecodeid']/label");
        readonly By PaymentType_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_paymenttypecodeid']");
        readonly By PaymentType_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_paymenttypecodeid']");
        readonly By PaymentType_LinkField = By.XPath("//*[@id='CWField_paymenttypecodeid_Link']");

        readonly By ProviderBatchGrouping_FieldLabel = By.XPath("//*[@id='CWLabelHolder_providerbatchgroupingid']/label");
        readonly By ProviderBatchGrouping_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_providerbatchgroupingid']");
        readonly By ProviderBatchGrouping_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_providerbatchgroupingid']");
        readonly By ProviderBatchGrouping_LinkField = By.XPath("//*[@id='CWField_providerbatchgroupingid_Link']");

        readonly By AdjustedDays_FieldLabel = By.XPath("//*[@id='CWLabelHolder_adjusteddays']/label");
        readonly By AdjustedDays_InputField = By.Id("CWField_adjusteddays");

        readonly By VATCode_FieldLabel = By.XPath("//*[@id='CWLabelHolder_vatcodeid']/label");
        readonly By VATCode_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_vatcodeid']");
        readonly By VATCode_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_vatcodeid']");
        readonly By VATCode_LinkField = By.XPath("//*[@id='CWField_vatcodeid_Link']");

        readonly By UsedInBatchSetup_YesRadioButton = By.Id("CWField_usedinbatchsetup_1");
        readonly By UsedInBatchSetup_NoRadioButton = By.Id("CWField_usedinbatchsetup_0");

        #endregion


        public ServiceElement1RecordPage WaitForServiceElement1RecordPageToLoad(bool serviceElementIFrame = true)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            if (serviceElementIFrame)
            {
                WaitForElement(iframe_ServiceElement1Frame);
                SwitchToIframe(iframe_ServiceElement1Frame);
            }

            WaitForElement(cwDialog_ServiceElement1Frame);
            SwitchToIframe(cwDialog_ServiceElement1Frame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(ServiceElement1RecordPageHeader);

            return this;
        }

        public ServiceElement1RecordPage ClickMenu()
        {
            WaitForElementToBeClickable(TopLeftMenu);
            MoveToElementInPage(TopLeftMenu);
            Click(TopLeftMenu);

            return this;

        }

        public ServiceElement1RecordPage NavigateToServicePermissionsPage()
        {
            WaitForElementToBeClickable(TopLeftMenu);
            Click(TopLeftMenu);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            MoveToElementInPage(ServicePermissionSubMenuLink);
            Click(ServicePermissionSubMenuLink);
            return this;
        }

        public ServiceElement1RecordPage NavigateToServiceMappingsPage()
        {
            WaitForElementToBeClickable(TopLeftMenu);
            Click(TopLeftMenu);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            MoveToElementInPage(ServiceMappingSubMenuLink);
            Click(ServiceMappingSubMenuLink);
            return this;
        }

        public ServiceElement1RecordPage NavigateToGLCodeMappingsPage()
        {
            WaitForElementToBeClickable(TopLeftMenu);
            Click(TopLeftMenu);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            MoveToElementInPage(GLCodeMappingSubMenuLink);
            Click(GLCodeMappingSubMenuLink);
            return this;
        }

        public ServiceElement1RecordPage ValidateNewRecordFieldsVisible()
        {
            WaitForElementVisible(Name_FieldLabel);
            WaitForElementVisible(Name_Field);
            WaitForElementVisible(Code_FieldLabel);
            WaitForElementVisible(Code_Field);
            WaitForElementVisible(GovCode_FieldLabel);
            WaitForElementVisible(GovCode_Field);
            WaitForElementVisible(Inactive_FieldLabel);
            WaitForElementVisible(Inactive_YesRadioButton);
            WaitForElementVisible(Inactive_NoRadioButton);
            WaitForElementVisible(WhoToPay_FieldLabel);
            WaitForElementVisible(WhoToPay_Field);
            WaitForElementVisible(PaymentsCommence_FieldLabel);
            WaitForElementVisible(PaymentsCommence_Field);
            WaitForElementVisible(DefaultStartReason_FieldLabel);
            WaitForElementVisible(DefaultStartReason_LookupButtonField);
            WaitForElementVisible(DefaultEndReason_FieldLabel);
            WaitForElementVisible(DefaultEndReason_LookupButtonField);
            WaitForElementVisible(UsedInFinance_FieldLabel);
            WaitForElementVisible(UsedInFinance_YesRadioButton);
            WaitForElementVisible(UsedInFinance_NoRadioButton);

            WaitForElementVisible(StartDate_FieldLabel);
            WaitForElementVisible(StartDate_Field);
            WaitForElementVisible(EndDate_FieldLabel);
            WaitForElementVisible(EndDate_Field);
            WaitForElementVisible(ValidForExport_FieldLabel);
            WaitForElementVisible(ValidForExport_YesRadioButton);
            WaitForElementVisible(ValidForExport_NoRadioButton);
            WaitForElementVisible(ResponsibleTeam_FieldLabel);
            WaitForElementVisible(ResponsibleTeam_LookupButtonField);
            WaitForElementVisible(EndDateRequired_FieldLabel);
            WaitForElementVisible(EndDateRequired_YesRadioButton);
            WaitForElementVisible(EndDateRequired_NoRadioButton);
            WaitForElementVisible(LACLegalStatusApplies_FieldLabel);
            WaitForElementVisible(LACLegalStatusApplies_YesRadioButton);
            WaitForElementVisible(LACLegalStatusApplies_NoRadioButton);
            WaitForElementVisible(ValidRateUnits_FieldLabel);
            WaitForElementVisible(ValidRateUnits_LookupButtonField);
            WaitForElementVisible(DefaultRateUnit_FieldLabel);
            WaitForElementVisible(DefaultRateUnit_LookupButtonField);
            WaitForElementVisible(GLCode_FieldLabel);
            WaitForElementVisible(GLCode_LookupButtonField);

            WaitForElementVisible(Notes_FieldLabel);
            WaitForElementVisible(Notes_Field);

            return this;
        }

        public ServiceElement1RecordPage ValidateTopMenuLinksVisible()
        {
            WaitForElementToBeClickable(TopLeftMenu);
            Click(TopLeftMenu);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementVisible(AuditSubMenuLink);
            WaitForElementVisible(GLCodeMappingSubMenuLink);
            WaitForElementVisible(ServiceGLCodingSubMenuLink);
            WaitForElementVisible(ServiceMappingSubMenuLink);
            WaitForElementVisible(ServicePermissionSubMenuLink);

            Click(TopLeftMenu);

            return this;
        }

        public ServiceElement1RecordPage InsertName(string TextToInsert)
        {
            WaitForElementToBeClickable(Name_Field);
            SendKeys(Name_Field, TextToInsert);

            return this;
        }
        public ServiceElement1RecordPage InsertCode(string TextToInsert)
        {
            WaitForElementToBeClickable(Code_Field);
            SendKeys(Code_Field, TextToInsert);

            return this;
        }
        public ServiceElement1RecordPage InsertGovCode(string TextToInsert)
        {
            WaitForElementToBeClickable(GovCode_Field);
            SendKeys(GovCode_Field, TextToInsert);

            return this;
        }
        public ServiceElement1RecordPage InsertStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(StartDate_Field);
            SendKeys(StartDate_Field, TextToInsert);

            return this;
        }
        public ServiceElement1RecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(EndDate_Field);
            SendKeys(EndDate_Field, TextToInsert);

            return this;
        }
        public ServiceElement1RecordPage InsertNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Notes_Field);
            SendKeys(Notes_Field, TextToInsert);

            return this;
        }

        public ServiceElement1RecordPage InsertCapacity(string TextToInsert)
        {
            WaitForElementToBeClickable(Capacity_Field);
            SendKeys(Capacity_Field, TextToInsert);

            return this;
        }

        public ServiceElement1RecordPage InsertAdjustedDays(string TextToInsert)
        {
            WaitForElementToBeClickable(AdjustedDays_InputField);
            SendKeys(AdjustedDays_InputField, TextToInsert);

            return this;
        }


        public ServiceElement1RecordPage ClickInactiveYesOption()
        {
            WaitForElementToBeClickable(Inactive_YesRadioButton);
            Click(Inactive_YesRadioButton);

            return this;
        }
        public ServiceElement1RecordPage ClickInactiveNoOption()
        {
            WaitForElementToBeClickable(Inactive_NoRadioButton);
            Click(Inactive_NoRadioButton);

            return this;
        }
        public ServiceElement1RecordPage ClickUsedInFinanceYesOption()
        {
            WaitForElementToBeClickable(UsedInFinance_YesRadioButton);
            Click(UsedInFinance_YesRadioButton);

            return this;
        }
        public ServiceElement1RecordPage ClickUsedInFinanceNoOption()
        {
            WaitForElementToBeClickable(UsedInFinance_NoRadioButton);
            Click(UsedInFinance_NoRadioButton);

            return this;
        }
        public ServiceElement1RecordPage ClickValidForExportYesOption()
        {
            WaitForElementToBeClickable(ValidForExport_YesRadioButton);
            Click(ValidForExport_YesRadioButton);

            return this;
        }
        public ServiceElement1RecordPage ClickValidForExportNoOption()
        {
            WaitForElementToBeClickable(ValidForExport_NoRadioButton);
            Click(ValidForExport_NoRadioButton);

            return this;
        }
        public ServiceElement1RecordPage ClickEndDateRequiredYesOption()
        {
            WaitForElementToBeClickable(EndDateRequired_YesRadioButton);
            Click(EndDateRequired_YesRadioButton);

            return this;
        }
        public ServiceElement1RecordPage ClickEndDateRequiredNoOption()
        {
            WaitForElementToBeClickable(EndDateRequired_NoRadioButton);
            Click(EndDateRequired_NoRadioButton);

            return this;
        }
        public ServiceElement1RecordPage ClickLACLegalStatusAppliesYesOption()
        {
            WaitForElementToBeClickable(LACLegalStatusApplies_YesRadioButton);
            Click(LACLegalStatusApplies_YesRadioButton);

            return this;
        }
        public ServiceElement1RecordPage ClickLACLegalStatusAppliesNoOption()
        {
            WaitForElementToBeClickable(LACLegalStatusApplies_NoRadioButton);
            Click(LACLegalStatusApplies_NoRadioButton);

            return this;
        }

        public ServiceElement1RecordPage ClickMaximumCapacityAppliesYesOption()
        {
            MoveToElementInPage(MaximumCapacityApplies_YesRadioButton);
            WaitForElementToBeClickable(MaximumCapacityApplies_YesRadioButton);
            Click(MaximumCapacityApplies_YesRadioButton);

            return this;
        }

        public ServiceElement1RecordPage ClickMaximumCapacityAppliesNoOption()
        {
            MoveToElementInPage(MaximumCapacityApplies_NoRadioButton);
            WaitForElementToBeClickable(MaximumCapacityApplies_NoRadioButton);
            Click(MaximumCapacityApplies_NoRadioButton);

            return this;
        }

        public ServiceElement1RecordPage ClickDefaultStartReasonLookupButton()
        {
            WaitForElementToBeClickable(DefaultStartReason_LookupButtonField);
            Click(DefaultStartReason_LookupButtonField);

            return this;
        }
        public ServiceElement1RecordPage ClickDefaultStartReasonRemoveButton()
        {
            WaitForElementToBeClickable(DefaultStartReason_RemoveButtonField);
            Click(DefaultStartReason_RemoveButtonField);

            return this;
        }
        public ServiceElement1RecordPage ClickDefaultEndReasonLookupButton()
        {
            WaitForElementToBeClickable(DefaultEndReason_LookupButtonField);
            Click(DefaultEndReason_LookupButtonField);

            return this;
        }
        public ServiceElement1RecordPage ClickDefaultEndReasonRemoveButton()
        {
            WaitForElementToBeClickable(DefaultEndReason_RemoveButtonField);
            Click(DefaultEndReason_RemoveButtonField);

            return this;
        }
        public ServiceElement1RecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButtonField);
            Click(ResponsibleTeam_LookupButtonField);

            return this;
        }
        public ServiceElement1RecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButtonField);
            Click(ResponsibleTeam_RemoveButtonField);

            return this;
        }
        public ServiceElement1RecordPage ClickValidRateUnitsLookupButton()
        {
            WaitForElementToBeClickable(ValidRateUnits_LookupButtonField);
            Click(ValidRateUnits_LookupButtonField);

            return this;
        }
        public ServiceElement1RecordPage ClickValidRateUnitsRemoveButton()
        {
            WaitForElementToBeClickable(ValidRateUnits_RemoveButtonField);
            Click(ValidRateUnits_RemoveButtonField);

            return this;
        }
        public ServiceElement1RecordPage ClickDefaultRateUnitLookupButton()
        {
            WaitForElementToBeClickable(DefaultRateUnit_LookupButtonField);
            Click(DefaultRateUnit_LookupButtonField);

            return this;
        }
        public ServiceElement1RecordPage ClickDefaultRateUnitRemoveButton()
        {
            WaitForElementToBeClickable(DefaultRateUnit_RemoveButtonField);
            Click(DefaultRateUnit_RemoveButtonField);

            return this;
        }
        public ServiceElement1RecordPage ClickGLCodeLookupButton()
        {
            WaitForElementToBeClickable(GLCode_LookupButtonField);
            Click(GLCode_LookupButtonField);

            return this;
        }
        public ServiceElement1RecordPage ClickGLCodeRemoveButton()
        {
            WaitForElementToBeClickable(GLCode_RemoveButtonField);
            Click(GLCode_RemoveButtonField);

            return this;
        }



        public ServiceElement1RecordPage SelectWhoToPay(string TextToSelect)
        {
            WaitForElementToBeClickable(WhoToPay_Field);
            SelectPicklistElementByText(WhoToPay_Field, TextToSelect);

            return this;
        }
        public ServiceElement1RecordPage SelectPaymentsCommence(string TextToSelect)
        {
            WaitForElementToBeClickable(PaymentsCommence_Field);
            SelectPicklistElementByText(PaymentsCommence_Field, TextToSelect);

            return this;
        }

        public ServiceElement1RecordPage ClickPaymentTypeLookupButton()
        {
            WaitForElementToBeClickable(PaymentType_LookupButtonField);
            Click(PaymentType_LookupButtonField);

            return this;
        }

        public ServiceElement1RecordPage ClickProviderBatchGroupingLookupButton()
        {
            WaitForElementToBeClickable(ProviderBatchGrouping_LookupButtonField);
            Click(ProviderBatchGrouping_LookupButtonField);

            return this;
        }

        public ServiceElement1RecordPage ClickVATCodeLookupButton()
        {
            WaitForElementToBeClickable(VATCode_LookupButtonField);
            Click(VATCode_LookupButtonField);

            return this;
        }

        public ServiceElement1RecordPage ClickPaymentTypeRemoveButton()
        {
            WaitForElementToBeClickable(PaymentType_RemoveButtonField);
            Click(PaymentType_RemoveButtonField);

            return this;
        }

        public ServiceElement1RecordPage ClickProviderBatchGroupingRemoveButton()
        {
            WaitForElementToBeClickable(ProviderBatchGrouping_RemoveButtonField);
            Click(ProviderBatchGrouping_RemoveButtonField);

            return this;
        }

        public ServiceElement1RecordPage ClickVATCodeRemoveButton()
        {
            WaitForElementToBeClickable(VATCode_RemoveButtonField);
            Click(VATCode_RemoveButtonField);

            return this;
        }

        public ServiceElement1RecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public ServiceElement1RecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            return this;
        }

        public ServiceElement1RecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(deleteButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public ServiceElement1RecordPage ValidateDefaultStartReasonLinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DefaultStartReason_LinkField);
            else
                WaitForElementNotVisible(DefaultStartReason_LinkField, 3);

            return this;
        }
        public ServiceElement1RecordPage ValidateDefaultEndReasonLinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DefaultEndReason_LinkField);
            else
                WaitForElementNotVisible(DefaultEndReason_LinkField, 3);

            return this;
        }
        public ServiceElement1RecordPage ValidateResponsibleTeamLinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_LinkField);
            else
                WaitForElementNotVisible(ResponsibleTeam_LinkField, 3);

            return this;
        }
        public ServiceElement1RecordPage ValidateValidRateUnitsLinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ValidRateUnits_LinkField);
            else
                WaitForElementNotVisible(ValidRateUnits_LinkField, 3);

            return this;
        }
        public ServiceElement1RecordPage ValidateDefaultRateUnitLinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(DefaultRateUnit_LinkField);
            else
                WaitForElementNotVisible(DefaultRateUnit_LinkField, 3);

            return this;
        }
        public ServiceElement1RecordPage ValidateGLCodeLinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(GLCode_LinkField);
            else
                WaitForElementNotVisible(GLCode_LinkField, 3);

            return this;
        }


        public ServiceElement1RecordPage ValidateDefaultStartReasonLinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(DefaultStartReason_LinkField);
            WaitForElementToBeClickable(DefaultStartReason_LinkField);
            ValidateElementText(DefaultStartReason_LinkField, ExpectedText);

            return this;
        }
        public ServiceElement1RecordPage ValidateDefaultEndReasonLinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(DefaultEndReason_LinkField);
            WaitForElementToBeClickable(DefaultEndReason_LinkField);
            ValidateElementText(DefaultEndReason_LinkField, ExpectedText);

            return this;
        }
        public ServiceElement1RecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(ResponsibleTeam_LinkField);
            WaitForElementToBeClickable(ResponsibleTeam_LinkField);
            ValidateElementTextContainsText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public ServiceElement1RecordPage ValidateValidRateUnitsLinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(ValidRateUnits_LinkField);
            WaitForElementToBeClickable(ValidRateUnits_LinkField);
            ValidateElementTextContainsText(ValidRateUnits_LinkField, ExpectedText);

            return this;
        }
        public ServiceElement1RecordPage ValidateDefaultRateUnitLinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(DefaultRateUnit_LinkField);
            if (ExpectedText != "")
                WaitForElementToBeClickable(DefaultRateUnit_LinkField);
            ValidateElementText(DefaultRateUnit_LinkField, ExpectedText);

            return this;
        }
        public ServiceElement1RecordPage ValidateGLCodeLinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(GLCode_LinkField);
            WaitForElementToBeClickable(GLCode_LinkField);
            ValidateElementText(GLCode_LinkField, ExpectedText);

            return this;
        }



        public ServiceElement1RecordPage ValidateNameFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(Name_Field);
            WaitForElementToBeClickable(Name_Field);
            ValidateElementValue(Name_Field, ExpectedValue);

            return this;
        }
        public ServiceElement1RecordPage ValidateCodeFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(Code_Field);
            WaitForElementToBeClickable(Code_Field);
            ValidateElementValue(Code_Field, ExpectedValue);

            return this;
        }
        public ServiceElement1RecordPage ValidateGovCodeFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(GovCode_Field);
            WaitForElementToBeClickable(GovCode_Field);
            ValidateElementValue(GovCode_Field, ExpectedValue);

            return this;
        }
        public ServiceElement1RecordPage ValidateStartDateFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(StartDate_Field);
            WaitForElementToBeClickable(StartDate_Field);
            ValidateElementValue(StartDate_Field, ExpectedValue);

            return this;
        }
        public ServiceElement1RecordPage ValidateEndDateFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(EndDate_Field);
            WaitForElementToBeClickable(EndDate_Field);
            ValidateElementValue(EndDate_Field, ExpectedValue);

            return this;
        }
        public ServiceElement1RecordPage ValidateNotesFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(Notes_Field);
            WaitForElementToBeClickable(Notes_Field);
            ValidateElementValue(Notes_Field, ExpectedValue);

            return this;
        }

        public ServiceElement1RecordPage ValidateCapacityFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(Capacity_Field);
            WaitForElementToBeClickable(Capacity_Field);
            ValidateElementValue(Capacity_Field, ExpectedValue);

            return this;
        }

        public ServiceElement1RecordPage ValidateAdjustedDaysFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(AdjustedDays_InputField);
            WaitForElementToBeClickable(AdjustedDays_InputField);
            ValidateElementValue(AdjustedDays_InputField, ExpectedValue);

            return this;
        }


        public ServiceElement1RecordPage ValidateInactiveYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(Inactive_YesRadioButton);
                ValidateElementNotChecked(Inactive_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(Inactive_YesRadioButton);
                ValidateElementChecked(Inactive_NoRadioButton);
            }

            return this;
        }
        public ServiceElement1RecordPage ValidateUsedInFinanceYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(UsedInFinance_YesRadioButton);
                ValidateElementNotChecked(UsedInFinance_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(UsedInFinance_YesRadioButton);
                ValidateElementChecked(UsedInFinance_NoRadioButton);
            }

            return this;
        }
        public ServiceElement1RecordPage ValidateValidForExportYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(ValidForExport_YesRadioButton);
                ValidateElementNotChecked(ValidForExport_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ValidForExport_YesRadioButton);
                ValidateElementChecked(ValidForExport_NoRadioButton);
            }

            return this;
        }
        public ServiceElement1RecordPage ValidateEndDateRequiredYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(EndDateRequired_YesRadioButton);
                ValidateElementNotChecked(EndDateRequired_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(EndDateRequired_YesRadioButton);
                ValidateElementChecked(EndDateRequired_NoRadioButton);
            }

            return this;
        }
        public ServiceElement1RecordPage ValidateLACLegalStatusAppliesYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(LACLegalStatusApplies_YesRadioButton);
                ValidateElementNotChecked(LACLegalStatusApplies_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(LACLegalStatusApplies_YesRadioButton);
                ValidateElementChecked(LACLegalStatusApplies_NoRadioButton);
            }

            return this;
        }




        public ServiceElement1RecordPage NavigateToServiceGLCodingsTab()
        {
            WaitForElementVisible(serviceGLCodings_Tab);
            Click(serviceGLCodings_Tab);

            return this;
        }

        public ServiceElement1RecordPage NavigateToServicePermissionsTab()
        {            
            MoveToElementInPage(servicePermission_Tab);
            WaitForElementToBeClickable(servicePermission_Tab);
            Click(servicePermission_Tab);

            return this;
        }

        public ServiceElement1RecordPage NavigateToServiceMappingsTab()
        {

            MoveToElementInPage(serviceMappings_Tab);
            WaitForElementToBeClickable(serviceMappings_Tab);
            Click(serviceMappings_Tab);

            return this;

        }

        public ServiceElement1RecordPage NavigateToGLCodeMappingsTab()
        {

            MoveToElementInPage(GLCodeMappings_Tab);
            WaitForElementToBeClickable(GLCodeMappings_Tab);
            Click(GLCodeMappings_Tab);

            return this;

        }

        public ServiceElement1RecordPage ValidateValidRateUnitOptionVisible(string OptionIdentifier, string ExpectedText)
        {
            WaitForElementVisible(ValidRateUnitOption_Field(OptionIdentifier));

            WaitForElementVisible(ValidRateUnitOption_RemoveButton(OptionIdentifier));

            ValidateElementText(ValidRateUnitOption_Field(OptionIdentifier), ExpectedText);


            return this;
        }

        public ServiceElement1RecordPage ClickValidRateUnitOptionRemoveButton(string OptionIdentifier)
        {
            WaitForElementVisible(ValidRateUnitOption_RemoveButton(OptionIdentifier));
            Click(ValidRateUnitOption_RemoveButton(OptionIdentifier));

            return this;
        }

        public ServiceElement1RecordPage ValidateWhoToPayFieldValue(string ExpectedText)
        {
            MoveToElementInPage(WhoToPay_Field);
            ValidatePicklistSelectedText(WhoToPay_Field, ExpectedText);
            return this;
        }

        public ServiceElement1RecordPage ValidatePaymentsCommenceFieldValue(string ExpectedText)
        {
            MoveToElementInPage(PaymentsCommence_Field);
            ValidatePicklistSelectedText(PaymentsCommence_Field, ExpectedText);
            return this;
        }

        public ServiceElement1RecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public ServiceElement1RecordPage ValidateWhoToPayOptionIsDisabled()
        {
            ValidateElementDisabled(WhoToPay_Field);

            return this;
        }

        public ServiceElement1RecordPage ValidateUsedInFinanceOptionsIsDisabled()
        {
            ValidateElementDisabled(UsedInFinance_YesRadioButton);
            ValidateElementDisabled(UsedInFinance_NoRadioButton);

            return this;
        }

        public ServiceElement1RecordPage ValidateUsedInFinanceOptionSelection(bool ExpectYesOptionSelected)
        {
            if (ExpectYesOptionSelected)
            {
                ValidateElementChecked(UsedInFinance_YesRadioButton);
                ValidateElementNotChecked(UsedInFinance_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(UsedInFinance_YesRadioButton);
                ValidateElementChecked(UsedInFinance_NoRadioButton);
            }

            return this;
        }

        public ServiceElement1RecordPage ValidateNameFieldIsDisabled()
        {
            WaitForElementVisible(Name_Field);
            MoveToElementInPage(Name_Field);
            ValidateElementDisabled(Name_Field);

            return this;
        }

        public ServiceElement1RecordPage ValidateCodeFieldIsDisabled()
        {
            WaitForElementVisible(Code_Field);
            MoveToElementInPage(Code_Field);
            ValidateElementDisabled(Code_Field);

            return this;
        }

        public ServiceElement1RecordPage ValidateGovCodeFieldIsDisabled()
        {
            WaitForElementVisible(GovCode_Field);
            MoveToElementInPage(GovCode_Field);
            ValidateElementDisabled(GovCode_Field);

            return this;
        }

        public ServiceElement1RecordPage ValidateStartDateFieldIsDisabled()
        {
            WaitForElementVisible(StartDate_Field);
            MoveToElementInPage(StartDate_Field);
            ValidateElementDisabled(StartDate_Field);

            return this;
        }

        public ServiceElement1RecordPage ValidateEndDateFieldIsDisabled()
        {
            WaitForElementVisible(EndDate_Field);
            MoveToElementInPage(EndDate_Field);
            ValidateElementDisabled(EndDate_Field);

            return this;
        }

        public ServiceElement1RecordPage ValidateInactiveFieldOptionsDisabled()
        {
            WaitForElementVisible(Inactive_YesRadioButton);
            MoveToElementInPage(Inactive_YesRadioButton);
            ValidateElementDisabled(Inactive_YesRadioButton);
            ValidateElementDisabled(Inactive_NoRadioButton);

            return this;
        }

        public ServiceElement1RecordPage ValidatePaymentsCommenceFieldIsDisabled()
        {
            MoveToElementInPage(PaymentsCommence_Field);
            ValidateElementDisabled(PaymentsCommence_Field);

            return this;
        }

        public ServiceElement1RecordPage ValidateDefaultStartReasonFieldIsDisabled()
        {
            MoveToElementInPage(DefaultStartReason_LookupButtonField);
            ValidateElementDisabled(DefaultStartReason_LookupButtonField);

            return this;
        }

        public ServiceElement1RecordPage ValidateDefaultEndReasonFieldIsDisabled()
        {
            MoveToElementInPage(DefaultEndReason_LookupButtonField);
            ValidateElementDisabled(DefaultEndReason_LookupButtonField);

            return this;
        }

        public ServiceElement1RecordPage ValidateResponsibleTeamFieldIsDisabled()
        {
            WaitForElementVisible(ResponsibleTeam_LookupButtonField);
            MoveToElementInPage(ResponsibleTeam_LookupButtonField);
            ValidateElementDisabled(ResponsibleTeam_LookupButtonField);

            return this;
        }

        public ServiceElement1RecordPage ValidateEndDateRequiredOptionsDisabled()
        {
            MoveToElementInPage(EndDateRequired_YesRadioButton);
            ValidateElementDisabled(EndDateRequired_YesRadioButton);
            ValidateElementDisabled(EndDateRequired_NoRadioButton);

            return this;
        }

        public ServiceElement1RecordPage ValidateLACLegalStatusAppliesFieldOptionsDisabled()
        {
            MoveToElementInPage(LACLegalStatusApplies_YesRadioButton);
            ValidateElementDisabled(LACLegalStatusApplies_YesRadioButton);
            ValidateElementDisabled(LACLegalStatusApplies_NoRadioButton);

            return this;
        }

        public ServiceElement1RecordPage ValidateMaximumCapacityAppliesFieldOptionsDisabled()
        {
            MoveToElementInPage(MaximumCapacityApplies_YesRadioButton);
            ValidateElementDisabled(MaximumCapacityApplies_YesRadioButton);
            ValidateElementDisabled(MaximumCapacityApplies_NoRadioButton);

            return this;
        }

        public ServiceElement1RecordPage ValidateExemptionOrExtensionRulesApplyFieldOptionsDisabled()
        {
            MoveToElementInPage(ExemptionOrExtensionRulesApply_YesRadioButton);
            ValidateElementDisabled(ExemptionOrExtensionRulesApply_YesRadioButton);
            ValidateElementDisabled(ExemptionOrExtensionRulesApply_NoRadioButton);

            return this;
        }

        public ServiceElement1RecordPage ValidateValidRateUnitsLookupFieldButtonIsDisabled()
        {
            MoveToElementInPage(ValidRateUnits_LookupButtonField);
            WaitForElementVisible(ValidRateUnits_LookupButtonField);
            ValidateElementDisabled(ValidRateUnits_LookupButtonField);

            return this;
        }

        public ServiceElement1RecordPage ValidateDefaultRateUnitLookupFieldButtonIsDisabled()
        {
            MoveToElementInPage(DefaultRateUnit_LookupButtonField);
            WaitForElementVisible(DefaultRateUnit_LookupButtonField);
            ValidateElementDisabled(DefaultRateUnit_LookupButtonField);

            return this;
        }

        public ServiceElement1RecordPage ValidateGLCodeLookupFieldButtonIsDisabled()
        {
            MoveToElementInPage(GLCode_LookupButtonField);
            WaitForElementVisible(GLCode_LookupButtonField);
            ValidateElementDisabled(GLCode_LookupButtonField);

            return this;
        }

        public ServiceElement1RecordPage ValidateNotesFieldIsDisabled()
        {
            MoveToElementInPage(Notes_Field);
            WaitForElementVisible(Notes_Field);
            ValidateElementDisabled(Notes_Field);

            return this;
        }

        public ServiceElement1RecordPage ValidateWhoToPayFieldIsDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                MoveToElementInPage(WhoToPay_Field);
                Assert.IsTrue(GetElementVisibility(WhoToPay_Field));
            }
            else
            {
                WaitForElementNotVisible(WhoToPay_Field, 5);
                bool ActualVisibility = GetElementVisibility(WhoToPay_Field);
                Assert.IsFalse(ActualVisibility);
            }
            return this;
        }

        public ServiceElement1RecordPage ValidateValidRateUnitFieldIsDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                MoveToElementInPage(ValidRateUnits_LookupButtonField);
                Assert.IsTrue(GetElementVisibility(ValidRateUnits_LookupButtonField));
            }
            else
            {
                WaitForElementNotVisible(ValidRateUnits_LookupButtonField, 5);
                bool ActualVisibility = GetElementVisibility(ValidRateUnits_LookupButtonField);
                Assert.IsFalse(ActualVisibility);
            }
            return this;
        }

        public ServiceElement1RecordPage ValidateDefaultRateUnitFieldIsDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                MoveToElementInPage(DefaultRateUnit_LookupButtonField);
                Assert.IsTrue(GetElementVisibility(DefaultRateUnit_LookupButtonField));
            }
            else
            {
                WaitForElementNotVisible(DefaultRateUnit_LookupButtonField, 5);
                bool ActualVisibility = GetElementVisibility(DefaultRateUnit_LookupButtonField);
                Assert.IsFalse(ActualVisibility);
            }
            return this;
        }

        public ServiceElement1RecordPage ValidateExemptionOrExtensionRulesApplyOptionsIsDisplayed()
        {
            MoveToElementInPage(ExemptionOrExtensionRulesApply_YesRadioButton);
            Assert.IsTrue(GetElementVisibility(ExemptionOrExtensionRulesApply_YesRadioButton));
            Assert.IsTrue(GetElementVisibility(ExemptionOrExtensionRulesApply_NoRadioButton));
            return this;
        }

        public ServiceElement1RecordPage ValidateExemptionOrExtensionRulesApplyOptionsIsNotDisplayed()
        {
            MoveToElementInPage(ExemptionOrExtensionRulesApply_YesRadioButton);
            Assert.IsFalse(GetElementVisibility(ExemptionOrExtensionRulesApply_YesRadioButton));
            Assert.IsFalse(GetElementVisibility(ExemptionOrExtensionRulesApply_NoRadioButton));
            return this;
        }

        public ServiceElement1RecordPage ValidateMaximumCapacityAppliesOptionsIsDisplayed()
        {
            MoveToElementInPage(MaximumCapacityApplies_YesRadioButton);
            Assert.IsTrue(GetElementVisibility(MaximumCapacityApplies_YesRadioButton));
            Assert.IsTrue(GetElementVisibility(MaximumCapacityApplies_YesRadioButton));
            return this;
        }

        public ServiceElement1RecordPage ValidateMaximumCapacityAppliesOptionsIsNotDisplayed()
        {
            MoveToElementInPage(MaximumCapacityApplies_YesRadioButton);
            Assert.IsFalse(GetElementVisibility(MaximumCapacityApplies_YesRadioButton));
            Assert.IsFalse(GetElementVisibility(MaximumCapacityApplies_YesRadioButton));
            return this;
        }

        public ServiceElement1RecordPage ValidateCapacityFieldIsDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                MoveToElementInPage(Capacity_Field);
                WaitForElementToBeClickable(Capacity_Field);
                Assert.IsTrue(GetElementVisibility(Capacity_Field));
            }
            else
            {
                WaitForElementNotVisible(Capacity_Field, 5);
                bool ActualVisibility = GetElementVisibility(Capacity_Field);
                Assert.IsFalse(ActualVisibility);
            }
            return this;
        }

        public ServiceElement1RecordPage ValidateUsedInBatchSetupYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(UsedInBatchSetup_YesRadioButton);
                ValidateElementNotChecked(UsedInBatchSetup_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(UsedInBatchSetup_YesRadioButton);
                ValidateElementChecked(UsedInBatchSetup_NoRadioButton);
            }

            return this;
        }

        public ServiceElement1RecordPage ValidateUsedInBatchSetupFieldOptionsDisplayed()
        {
            MoveToElementInPage(UsedInBatchSetup_YesRadioButton);
            Assert.IsTrue(GetElementVisibility(UsedInBatchSetup_YesRadioButton));
            Assert.IsTrue(GetElementVisibility(UsedInBatchSetup_NoRadioButton));
            return this;
        }

        public ServiceElement1RecordPage ValidatePaymentTypeLookupFieldButtonIsDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                MoveToElementInPage(PaymentType_LookupButtonField);
                WaitForElementToBeClickable(PaymentType_LookupButtonField);
                Assert.IsTrue(GetElementVisibility(PaymentType_LookupButtonField));
            }
            else
            {
                WaitForElementNotVisible(PaymentType_LookupButtonField, 5);
                bool ActualVisibility = GetElementVisibility(PaymentType_LookupButtonField);
                Assert.IsFalse(ActualVisibility);
            }
            return this;
        }

        public ServiceElement1RecordPage ValidateProviderBatchGroupingLookupFieldButtonIsDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                MoveToElementInPage(ProviderBatchGrouping_LookupButtonField);
                WaitForElementToBeClickable(ProviderBatchGrouping_LookupButtonField);
                Assert.IsTrue(GetElementVisibility(ProviderBatchGrouping_LookupButtonField));
            }
            else
            {
                WaitForElementNotVisible(ProviderBatchGrouping_LookupButtonField, 5);
                bool ActualVisibility = GetElementVisibility(ProviderBatchGrouping_LookupButtonField);
                Assert.IsFalse(ActualVisibility);
            }
            return this;
        }

        public ServiceElement1RecordPage ValidateVATCodeLookupFieldButtonIsDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                MoveToElementInPage(VATCode_LookupButtonField);
                WaitForElementToBeClickable(VATCode_LookupButtonField);
                Assert.IsTrue(GetElementVisibility(VATCode_LookupButtonField));
            }
            else
            {
                WaitForElementNotVisible(VATCode_LookupButtonField, 5);
                bool ActualVisibility = GetElementVisibility(VATCode_LookupButtonField);
                Assert.IsFalse(ActualVisibility);
            }
            return this;
        }

        public ServiceElement1RecordPage ValidateAdjustedDaysFieldIsDisplayed(bool ExpectedVisibility)
        {
            if (ExpectedVisibility)
            {
                MoveToElementInPage(AdjustedDays_InputField);
                WaitForElementToBeClickable(AdjustedDays_InputField);
                Assert.IsTrue(GetElementVisibility(AdjustedDays_InputField));
            }
            else
            {
                WaitForElementNotVisible(AdjustedDays_InputField, 5);
                bool ActualVisibility = GetElementVisibility(AdjustedDays_InputField);
                Assert.IsFalse(ActualVisibility);
            }
            return this;
        }

        public ServiceElement1RecordPage ValidatePaymentTypeLookupIsDisabled()
        {
            MoveToElementInPage(PaymentType_LookupButtonField);
            ValidateElementDisabled(PaymentType_LookupButtonField);

            return this;
        }

        public ServiceElement1RecordPage ValidateProviderBatchGroupingLookupIsDisabled()
        {
            MoveToElementInPage(ProviderBatchGrouping_LookupButtonField);
            ValidateElementDisabled(ProviderBatchGrouping_LookupButtonField);

            return this;
        }
        public ServiceElement1RecordPage ValidateAdjustedDaysFieldIsDisabled()
        {
            MoveToElementInPage(AdjustedDays_InputField);
            ValidateElementDisabled(AdjustedDays_InputField);

            return this;
        }

        public ServiceElement1RecordPage ValidateVATCodeLookupIsDisabled()
        {
            MoveToElementInPage(VATCode_LookupButtonField);
            ValidateElementDisabled(VATCode_LookupButtonField);

            return this;
        }

        public ServiceElement1RecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            MoveToElementInPage(deleteButton);
            Click(deleteButton);

            return this;
        }
    }
}