
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class BusinessObjectRecordPage : CommonMethods
    {
        public BusinessObjectRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=businessobject&')]");

        #region Business Object Fields and Field Values
        readonly By IsBulkEditEnabledLabel = By.XPath("//*[@id='CWLabelHolder_isbulkeditenabled']/label[text()='Is Bulk Edit Enabled?']");

        readonly By IsBulkEditEnabledField_YesOption = By.XPath("//*[@id='CWField_isbulkeditenabled_1']");
        readonly By IsBulkEditEnabledField_NoOption = By.XPath("//*[@id='CWField_isbulkeditenabled_0']");

        By IsBulkEditEnabledFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_isbulkeditenabled_" + value + "'][@checked = 'checked']");

        By Is_FieldEnabled_Label(string labelTitleText) => By.XPath("//li[starts-with(@id,'CWLabelHolder_')]/label[text()='" + labelTitleText+"']");
        By Is_FieldEnabled_Label2(string labelId) => By.XPath("//*[contains(@id,'_"+ labelId + "')]/label");
        By Is_FieldRadioButtonEnabled_Label2(string labelId) => By.XPath("//input[contains(@id,'_"+ labelId+"')][@checked = 'checked']");
        By FieldOptionYesOrNoCheck(string value) => By.XPath("//*[@id='CWField_enableresponsibleuser_"+value+ "'][@checked = 'checked']");
        
        readonly By TitleRequiredFieldLabel = By.XPath("//*[@id='CWLabelHolder_titlefieldrequired']/label[text()='Title Field Required']");
        readonly By TitleFieldRequiredField_YesOption = By.XPath("//*[@id='CWField_titlefieldrequired_1']");
        readonly By TitleFieldRequiredField_NoOption = By.XPath("//*[@id='CWField_titlefieldrequired_0']");
        By TitleFieldRequiredFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_titlefieldrequired_" + value + "'][@checked = 'checked']");

        readonly By OwnershipRequiredFieldLabel = By.XPath("//*[@id='CWLabelHolder_ownershiptypeid']/label[text()='Ownership']");
        readonly By OwnershipField_Option = By.XPath("//*[@id='CWField_ownershiptypeid']");

        readonly By EnableResponsibleUserFieldLabel = By.XPath("//*[@id='CWLabelHolder_enableresponsibleuser']/label[text()='Enable Responsible User?']");
        readonly By EnableResponsibleUserField_YesOption = By.XPath("//*[@id='CWField_enableresponsibleuser_1']");
        readonly By EnableResponsibleUserField_NoOption = By.XPath("//*[@id='CWField_enableresponsibleuser_1']");
        By EnableResponsibleUserFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_enableresponsibleuser_" + value + "'][@checked = 'checked']");

        readonly By CanBeCustomizedFieldLabel = By.XPath("//*[@id='CWLabelHolder_canbecustomized']/label[text()='Can Be Customized?']");
        readonly By CanBeCustomizedField_YesOption = By.XPath("//*[@id='CWField_canbecustomized_1']");
        readonly By CanBeCustomizedField_NoOption = By.XPath("//*[@id='CWField_canbecustomized_0']");
        By CanBeCustomizedFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_canbecustomized_" + value + "'][@checked = 'checked']");

        readonly By SharingFieldLabel = By.XPath("//*[@id='CWLabelHolder_enablesharing']/label[text()='Sharing?']");
        readonly By SharingField_YesOption = By.XPath("//*[@id='CWField_enablesharing_1']");
        readonly By SharingField_NoOption = By.XPath("//*[@id='CWField_enablesharing_0']");
        By SharingFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_enablesharing_" + value + "'][@checked = 'checked']");

        readonly By AuditFieldLabel = By.XPath("//*[@id='CWLabelHolder_enableauditing']/label[text()='Audit?']");
        readonly By AuditField_YesOption = By.XPath("//*[@id='CWField_enableauditing_1']");
        readonly By AuditField_NoOption = By.XPath("//*[@id='CWField_enableauditing_0']");
        By AuditFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_enableauditing_" + value + "'][@checked = 'checked']");

        readonly By ViewAuditFieldLabel = By.XPath("//*[@id='CWLabelHolder_enableviewaudit']/label[text()='View Audit?']");
        readonly By ViewAuditField_YesOption = By.XPath("//*[@id='CWField_enableviewaudit_1']");
        readonly By ViewAuditField_NoOption = By.XPath("//*[@id='CWField_enableviewaudit_0']");
        By ViewAuditFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_enableviewaudit_" + value + "'][@checked = 'checked']");

        readonly By ValidForUpdateFieldLabel = By.XPath("//*[@id='CWLabelHolder_validforupdate']/label[text()='Valid For Update']");
        readonly By ValidForUpdateField_YesOption = By.XPath("//*[@id='CWField_validforupdate_1']");
        readonly By ValidForUpdateField_NoOption = By.XPath("//*[@id='CWField_validforupdate_0']");
        By ValidForUpdateFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_validforupdate_" + value + "'][@checked = 'checked']");

        readonly By RecentRecordsFieldLabel = By.XPath("//*[@id='CWLabelHolder_recentrecordenabled']/label[text()='Recent Records?']");
        readonly By RecentRecordsField_YesOption = By.XPath("//*[@id='CWField_recentrecordenabled_1']");
        readonly By RecentRecordsField_NoOption = By.XPath("//*[@id='CWField_recentrecordenabled_0']");
        By RecentRecordsFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_recentrecordenabled_" + value + "'][@checked = 'checked']");

        readonly By EnablePinToSelfFieldLabel = By.XPath("//*[@id='CWLabelHolder_pintoselfenabled']/label[text()='Enable Pin to Self?']");
        readonly By EnablePinToSelfField_YesOption = By.XPath("//*[@id='CWField_pintoselfenabled_1']");
        readonly By EnablePinToSelfField_NoOption = By.XPath("//*[@id='CWField_pintoselfenabled_0']");
        By EnablePinToSelfFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_pintoselfenabled_" + value + "'][@checked = 'checked']");

        readonly By EnablePinToAnotherFieldLabel = By.XPath("//*[@id='CWLabelHolder_pintoanotherenabled']/label[text()='Enable Pin to Another?']");
        readonly By EnablePinToAnotherField_YesOption = By.XPath("//*[@id='CWField_pintoanotherenabled_1']");
        readonly By EnablePinToAnotherField_NoOption = By.XPath("//*[@id='CWField_pintoanotherenabled_0']");
        By EnablePinToAnotherFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_pintoanotherenabled_" + value + "'][@checked = 'checked']");

        readonly By SupportsMultipleDataFormsFieldLabel = By.XPath("//*[@id='CWLabelHolder_supportsmultipledataforms']/label[text()='Supports Multiple Data Forms']");
        readonly By SupportsMultipleDataFormsField_YesOption = By.XPath("//*[@id='CWField_supportsmultipledataforms_1']");
        readonly By SupportsMultipleDataFormsField_NoOption = By.XPath("//*[@id='CWField_supportsmultipledataforms_0']");
        By SupportsMultipleDataFormsOptionCheck(string value) => By.XPath("//*[@id='CWField_supportsmultipledataforms_" + value + "'][@checked = 'checked']");

        readonly By AvailableInAdvancedSearchFieldLabel = By.XPath("//*[@id='CWLabelHolder_isadvancedsearchenabled']/label[text()='Available in Advanced Search?']");
        readonly By AvailableInAdvancedSearchField_YesOption = By.XPath("//*[@id='CWField_isadvancedsearchenabled_1']");
        readonly By AvailableInAdvancedSearchField_NoOption = By.XPath("//*[@id='CWField_isadvancedsearchenabled_0']");
        By AvailableInAdvancedSearchFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_isadvancedsearchenabled_" + value + "'][@checked = 'checked']");

        readonly By AvailableinGlobalSearchFieldLabel = By.XPath("//*[@id='CWLabelHolder_isglobalsearchenabled']/label[text()='Available in Global Search?']");//
        readonly By AvailableinGlobalSearchField_YesOption = By.XPath("//*[@id='CWField_isglobalsearchenabled_1']");
        readonly By AvailableinGlobalSearchField_NoOption = By.XPath("//*[@id='CWField_isglobalsearchenabled_0']");
        By AvailableInGlobalSearchFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_isglobalsearchenabled_" + value + "'][@checked = 'checked']");

        readonly By DataRestrictionEnabledFieldLabel = By.XPath("//*[@id='CWLabelHolder_isdatarestrictionenabled']/label[text()='Data Restriction Enabled?']");
        readonly By DataRestrictionEnabledField_YesOption = By.XPath("//*[@id='CWField_isdatarestrictionenabled_1']");
        readonly By DataRestrictionEnabledField_NoOption = By.XPath("//*[@id='CWField_isdatarestrictionenabled_0']");
        By DataRestrictionEnabledFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_isdatarestrictionenabled_" + value + "'][@checked = 'checked']");

        readonly By RestrictforLegitimateRelationshipsFieldLabel = By.XPath("//*[@id='CWLabelHolder_islegitimaterelationshipenabled']/label[text()='Restrict for Legitimate Relationships']");
        readonly By RestrictforLegitimateRelationshipsField_YesOption = By.XPath("//*[@id='CWField_islegitimaterelationshipenabled_1']");
        readonly By RestrictforLegitimateRelationshipsField_NoOption = By.XPath("//*[@id='CWField_islegitimaterelationshipenabled_0']");
        By RestrictforLegitimateRelationshipsOptionCheck(string value) => By.XPath("//*[@id='CWField_islegitimaterelationshipenabled_" + value + "'][@checked = 'checked']");


        readonly By IsMergeEnabledFieldLabel = By.XPath("//*[@id='CWLabelHolder_ismergeenabled']/label[text()='Is Merge Enabled?']");
        readonly By IsMergeEnabledField_YesOption = By.XPath("//*[@id='CWField_ismergeenabled_1']");
        readonly By IsMergeEnabledField_NoOption = By.XPath("//*[@id='CWField_ismergeenabled_0']");
        By IsMergeFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_ismergeenabled_" + value + "'][@checked = 'checked']");

        readonly By AvailableforFieldLevelSecurityFieldLabel = By.XPath("//*[@id='CWLabelHolder_availableforfls']/label[text()='Available For Field Level Security']");
        readonly By AvailableforFieldLevelSecurityField_YesOption = By.XPath("//*[@id='CWField_availableforfls_1']");
        readonly By AvailableforFieldLevelSecurityField_NoOption = By.XPath("//*[@id='CWField_availableforfls_0']");
        By AvailableforFieldLevelSecurityFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_availableforfls_" + value + "'][@checked = 'checked']");

        readonly By AvailableinMobileFieldLabel = By.XPath("//*[@id='CWLabelHolder_availableoffline']/label[text()='Available in Mobile']");
        readonly By AvailableinMobileField_YesOption = By.XPath("//*[@id='CWField_availableoffline_1']");
        readonly By AvailableinMobileField_NoOption = By.XPath("//*[@id='CWField_availableoffline_0']");
        By AvailableinMobileFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_availableoffline_" + value + "'][@checked = 'checked']");
        
        readonly By EncryptMobileOfflineDataFieldLabel = By.XPath("//*[@id='CWLabelHolder_encryptdata']/label[text()='Encrypt Mobile Offline Data']");
        readonly By EncryptMobileOfflineDataField_YesOption = By.XPath("//*[@id='CWField_encryptdata_1']");
        readonly By EncryptMobileOfflineDataField_NoOption = By.XPath("//*[@id='CWField_encryptdata_0']");
        By EncryptMobileOfflineDataFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_encryptdata_" + value + "'][@checked = 'checked']");
        
        readonly By EnableMailMergeFieldLabel = By.XPath("//*[@id='CWLabelHolder_enablemailmerge']/label[text()='Enable Mail Merge']");
        readonly By EnableMailMergeField_YesOption = By.XPath("//*[@id='CWField_enablemailmerge_1']");
        readonly By EnableMailMergeField_NoOption = By.XPath("//*[@id='CWField_enablemailmerge_0']");
        By EnableMailMergeFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_enablemailmerge_" + value + "'][@checked = 'checked']");

        readonly By ExcludefromDataDictionaryFieldLabel = By.XPath("//*[@id='CWLabelHolder_excludefromdatadictionary']/label[text()='Exclude From DataDictionary']");
        readonly By ExcludefromDataDictionaryField_YesOption = By.XPath("//*[@id='CWField_excludefromdatadictionary_1']");
        readonly By ExcludefromDataDictionaryField_NoOption = By.XPath("//*[@id='CWField_excludefromdatadictionary_0']");
        By ExcludefromDataDictionaryFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_excludefromdatadictionary_" + value + "'][@checked = 'checked']");

        readonly By ExcludefromDataMapsFieldLabel = By.XPath("//*[@id='CWLabelHolder_excludefromdatamaps']/label[text()='Exclude From DataMaps']");
        readonly By ExcludefromDataMapsField_YesOption = By.XPath("//*[@id='CWField_excludefromdatamaps_1']");
        readonly By ExcludefromDataMapsField_NoOption = By.XPath("//*[@id='CWField_excludefromdatamaps_0']");
        By ExcludefromDataMapsFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_excludefromdatamaps_" + value + "'][@checked = 'checked']");

        readonly By ExcludefromWorkflowFieldLabel = By.XPath("//*[@id='CWLabelHolder_excludefromworkflow']/label[text()='Exclude From Workflow']");
        readonly By ExcludefromWorkflowField_YesOption = By.XPath("//*[@id='CWField_excludefromworkflow_1']");
        readonly By ExcludefromWorkflowField_NoOption = By.XPath("//*[@id='CWField_excludefromworkflow_0']");
        By ExcludefromWorkflowFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_excludefromworkflow_" + value + "'][@checked = 'checked']");

        readonly By ExcludefromSARFieldLabel = By.XPath("//*[@id='CWLabelHolder_excludefromsar']/label[text()='Exclude From SAR']");
        readonly By ExcludefromSARField_YesOption = By.XPath("//*[@id='CWField_excludefromsar_1']");
        readonly By ExcludefromSARField_NoOption = By.XPath("//*[@id='CWField_excludefromsar_0']");
        By ExcludefromSARFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_excludefromsar_" + value + "'][@checked = 'checked']");

        readonly By TrackChangesFieldLabel = By.XPath("//*[@id='CWLabelHolder_istrackchangesenabled']/label[text()='Track Changes']");
        readonly By TrackChangesField_YesOption = By.XPath("//*[@id='CWField_istrackchangesenabled_1']");
        readonly By TrackChangesField_NoOption = By.XPath("//*[@id='CWField_istrackchangesenabled_0']");
        By TrackChangesFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_istrackchangesenabled_" + value + "'][@checked = 'checked']");
        
        readonly By EnableErrorManagementFieldLabel = By.XPath("//*[@id='CWLabelHolder_iserrormanagementenabled']/label[text()='Enable Error Management']");
        readonly By EnableErrorManagementField_YesOption = By.XPath("//*[@id='CWField_iserrormanagementenabled_1']");
        readonly By EnableErrorManagementField_NoOption = By.XPath("//*[@id='CWField_iserrormanagementenabled_0']");
        By EnableErrorManagementFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_istrackchangesenabled_" + value + "'][@checked = 'checked']");

        readonly By ValidforExportFieldLabel = By.XPath("//*[@id='CWLabelHolder_validforexport']/label[text()='Valid for Export']");
        readonly By ValidforExportField_YesOption = By.XPath("//*[@id='CWField_validforexport_1']");
        readonly By ValidforExportField_NoOption = By.XPath("//*[@id='CWField_validforexport_0']");
        By ValidforExportFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_validforexport_" + value + "'][@checked = 'checked']");
        
        readonly By DisplayOnTimelineFieldLabel = By.XPath("//*[@id='CWLabelHolder_displayontimeline']/label[text()='Display On Timeline']");
        readonly By DisplayOnTimelineField_YesOption = By.XPath("//*[@id='CWField_displayontimeline_1']");
        readonly By DisplayOnTimelineField_NoOption = By.XPath("//*[@id='CWField_displayontimeline_0']");
        By DisplayOnTimelineFieldOptionCheck(string value) => By.XPath("//*[@id='CWField_displayontimeline_" + value + "'][@checked = 'checked']");

        readonly By NameFieldLabel = By.XPath("//*[@id='CWLabelHolder_name']/label[text()='Name']");
        readonly By NameField = By.Id("CWField_name");

        readonly By TypeFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_businessobjecttype']/label[@title = 'Type']");
        readonly By TypeField = By.XPath("//*[@id = 'CWField_businessobjecttype']");

        readonly By SingularNameFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_singularnameid']/label[@title = 'Singular Name']");
        readonly By SingularNameField = By.Id("CWField_singularnameid_cwname");

        readonly By PluralNameFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_pluralnameid']/label[@title = 'Plural Name']");
        readonly By PluralNameField = By.Id("CWField_pluralnameid_cwname");

        readonly By IconFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_icon']/label[@title = 'Icon (50x50)']");
        readonly By IconField = By.Id("CWField_icon");

        readonly By BusinessModuleFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_businessmoduleid']/label[@title = 'Business Module']");
        readonly By BusinessModuleField = By.Id("CWField_businessmoduleid");

        readonly By DescriptionFieldLabel = By.XPath("//*[@id = 'CWLabelHolder_descriptionid']/label[@title = 'Description']");
        readonly By DescriptionField = By.Id("CWField_descriptionid_cwname");
        #endregion

        #region Navigation Area

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_grpRelated']/button[@title = 'Menu']");
        


        #region Left Sub Menu

        readonly By relatedItemsLeftSubMenu = By.XPath("//li[@id='CWNavSubGroup_RelatedItems']/a");

        #region Related Items

        readonly By businessObjectFieldsLeftSubMenuItem = By.XPath("//*[@id='CWNavItem_businessobjectfield']");
        readonly By dataFormsLeftSubMenuItem = By.XPath("//*[@id='CWNavItem_dataform']");

        #endregion

        #endregion

        #endregion



        public BusinessObjectRecordPage WaitForBusinessObjectRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(MenuButton);

            return this;
        }

        #region Business Object Field Visibility and Field Value Validation
        public BusinessObjectRecordPage ValidateIsBulkEditEnabledFieldVisible()
        {
            WaitForElement(IsBulkEditEnabledLabel);
            WaitForElement(IsBulkEditEnabledField_YesOption);
            WaitForElement(IsBulkEditEnabledField_NoOption);

            return this;
        }

        public BusinessObjectRecordPage ValidateIsBulkEditEnabledFieldVisible(string optionValue)
        {

            WaitForElement(IsBulkEditEnabledLabel);
            WaitForElement(IsBulkEditEnabledField_YesOption);
            WaitForElement(IsBulkEditEnabledField_NoOption);

            Assert.IsTrue(GetElementVisibility(IsBulkEditEnabledLabel));
            ValidateElementChecked(IsBulkEditEnabledFieldOptionCheck(optionValue)); //0

            return this;
        }

        //Validate if a field is visible and its options are visible. Validate the value which is selected.
        public BusinessObjectRecordPage ValidateIsBusinessObjecFieldVisible(string fieldName, string fieldValue)
        {

            WaitForElement(Is_FieldEnabled_Label(fieldName));            
            WaitForElement(Is_FieldRadioButtonEnabled_Label2(fieldValue));
            Assert.AreEqual(fieldName, GetElementText(Is_FieldEnabled_Label(fieldName)));            
            ValidateElementChecked(Is_FieldRadioButtonEnabled_Label2(fieldValue));

            return this;
        }

        public BusinessObjectRecordPage ValidateFieldValue(string Value)
        {
            WaitForElement(Is_FieldRadioButtonEnabled_Label2(Value));
            ValidateElementChecked(Is_FieldRadioButtonEnabled_Label2(Value));
            return this;
        }

        public BusinessObjectRecordPage ValidateTitleFieldRequiredFieldVisible(string optionValue)
        {

            WaitForElement(TitleRequiredFieldLabel);
            WaitForElement(TitleFieldRequiredField_YesOption);
            WaitForElement(TitleFieldRequiredField_NoOption);
            ScrollToElement(TitleRequiredFieldLabel);
            Assert.IsTrue(GetElementVisibility(TitleRequiredFieldLabel));            
            ValidateElementChecked(TitleFieldRequiredFieldOptionCheck(optionValue)); //1


            return this;
        }

        public BusinessObjectRecordPage ValidateOwnershipFieldVisible(string optionValue)
        {
            WaitForElement(OwnershipRequiredFieldLabel);
            WaitForElement(OwnershipField_Option);

            Assert.IsTrue(GetElementVisibility(OwnershipRequiredFieldLabel));
            ValidatePicklistSelectedText(OwnershipField_Option, optionValue); //Team Based

            return this;
        }

        public BusinessObjectRecordPage ValidateEnableResponsibleUserFieldVisible(string optionValue)
        {

            WaitForElement(EnableResponsibleUserFieldLabel);
            WaitForElement(EnableResponsibleUserField_YesOption);
            WaitForElement(EnableResponsibleUserField_NoOption);

            Assert.IsTrue(GetElementVisibility(EnableResponsibleUserFieldLabel));
            ValidateElementChecked(EnableResponsibleUserFieldOptionCheck(optionValue)); //1


            return this;
        }

        public BusinessObjectRecordPage ValidateCanBeCustomizedFieldVisible(string optionValue)
        {

            WaitForElement(CanBeCustomizedFieldLabel);
            WaitForElement(CanBeCustomizedField_YesOption);
            WaitForElement(CanBeCustomizedField_NoOption);

            Assert.IsTrue(GetElementVisibility(CanBeCustomizedFieldLabel));
            ValidateElementChecked(CanBeCustomizedFieldOptionCheck(optionValue)); //1


            return this;
        }

        public BusinessObjectRecordPage ValidateSharingFieldVisible(string optionValue)
        {

            WaitForElement(SharingFieldLabel);
            WaitForElement(SharingField_YesOption);
            WaitForElement(SharingField_NoOption);

            Assert.IsTrue(GetElementVisibility(SharingFieldLabel));
            ValidateElementChecked(SharingFieldOptionCheck(optionValue)); //0


            return this;
        }

        public BusinessObjectRecordPage ValidateAuditFieldVisible(string optionValue)
        {

            WaitForElement(AuditFieldLabel);
            WaitForElement(AuditField_YesOption);
            WaitForElement(AuditField_NoOption);

            Assert.IsTrue(GetElementVisibility(AuditFieldLabel));
            ValidateElementChecked(AuditFieldOptionCheck(optionValue)); //1


            return this;
        }

        public BusinessObjectRecordPage ValidateViewAuditFieldVisible(string optionValue)
        {

            WaitForElement(ViewAuditFieldLabel);
            WaitForElement(ViewAuditField_YesOption);
            WaitForElement(ViewAuditField_NoOption);

            Assert.IsTrue(GetElementVisibility(ViewAuditFieldLabel));
            ValidateElementChecked(ViewAuditFieldOptionCheck(optionValue));//1


            return this;
        }

        public BusinessObjectRecordPage ValidateValidForUpdateFieldVisible(string optionValue)
        {

            WaitForElement(ValidForUpdateFieldLabel);
            WaitForElement(ValidForUpdateField_YesOption);
            WaitForElement(ValidForUpdateField_NoOption);

            Assert.IsTrue(GetElementVisibility(ValidForUpdateFieldLabel));
            ValidateElementChecked(ValidForUpdateFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateRecentRecordsFieldVisible(string optionValue)
        {
            WaitForElement(RecentRecordsFieldLabel);
            WaitForElement(RecentRecordsField_YesOption);
            WaitForElement(RecentRecordsField_NoOption);
            ScrollToElement(RecentRecordsFieldLabel);
            ScrollToElement(RecentRecordsFieldLabel);

            Assert.IsTrue(GetElementVisibility(RecentRecordsFieldLabel));
            ValidateElementChecked(RecentRecordsFieldOptionCheck(optionValue));

            return this;
        }

        public BusinessObjectRecordPage ValidateEnablePinToSelfFieldVisible(string optionValue)
        {
            WaitForElement(EnablePinToSelfFieldLabel);
            WaitForElement(EnablePinToSelfField_YesOption);
            WaitForElement(EnablePinToSelfField_NoOption);

            Assert.IsTrue(GetElementVisibility(EnablePinToSelfFieldLabel));
            ValidateElementChecked(EnablePinToSelfFieldOptionCheck(optionValue));

            return this;
        }

        public BusinessObjectRecordPage ValidateEnablePinToAnotherFieldVisible(string optionValue)
        {
            WaitForElement(EnablePinToAnotherFieldLabel);
            WaitForElement(EnablePinToAnotherField_YesOption);
            WaitForElement(EnablePinToAnotherField_NoOption);

            Assert.IsTrue(GetElementVisibility(EnablePinToAnotherFieldLabel));
            ValidateElementChecked(EnablePinToAnotherFieldOptionCheck(optionValue));

            return this;
        }

        public BusinessObjectRecordPage ValidateSupportsMultipleDataFormsFieldVisible(string optionValue)
        {
            WaitForElement(SupportsMultipleDataFormsFieldLabel);
            WaitForElement(SupportsMultipleDataFormsField_YesOption);
            WaitForElement(SupportsMultipleDataFormsField_NoOption);

            Assert.IsTrue(GetElementVisibility(SupportsMultipleDataFormsFieldLabel));
            ValidateElementChecked(SupportsMultipleDataFormsOptionCheck(optionValue));

            return this;
        }


        public BusinessObjectRecordPage ValidateAvailableInAdvancedSearchFieldVisible(string optionValue)
        {

            WaitForElement(AvailableInAdvancedSearchFieldLabel);
            WaitForElement(AvailableInAdvancedSearchField_YesOption);
            WaitForElement(AvailableInAdvancedSearchField_NoOption);
            ScrollToElement(AvailableInAdvancedSearchFieldLabel);

            Assert.IsTrue(GetElementVisibility(AvailableInAdvancedSearchFieldLabel));
            ValidateElementChecked(AvailableInAdvancedSearchFieldOptionCheck(optionValue));


            return this;
        }


        public BusinessObjectRecordPage ValidateAvailableInGlobalSearchFieldVisible(string optionValue)
        {

            WaitForElement(AvailableinGlobalSearchFieldLabel);
            WaitForElement(AvailableinGlobalSearchField_YesOption);
            WaitForElement(AvailableinGlobalSearchField_NoOption);

            Assert.IsTrue(GetElementVisibility(AvailableinGlobalSearchFieldLabel));
            ValidateElementChecked(AvailableInGlobalSearchFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateDataRestrictionEnabledFieldVisible(string optionValue)
        {

            WaitForElement(DataRestrictionEnabledFieldLabel);
            WaitForElement(DataRestrictionEnabledField_YesOption);
            WaitForElement(DataRestrictionEnabledField_NoOption);
            ScrollToElement(DataRestrictionEnabledFieldLabel);

            Assert.IsTrue(GetElementVisibility(DataRestrictionEnabledFieldLabel));
            ValidateElementChecked(DataRestrictionEnabledFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateRestrictforLegitimateRelationshipsFieldVisible(string optionValue)
        {

            WaitForElement(RestrictforLegitimateRelationshipsFieldLabel);
            WaitForElement(RestrictforLegitimateRelationshipsField_YesOption);
            WaitForElement(RestrictforLegitimateRelationshipsField_NoOption);

            Assert.IsTrue(GetElementVisibility(RestrictforLegitimateRelationshipsFieldLabel));
            ValidateElementChecked(RestrictforLegitimateRelationshipsOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateIsMergeEnabledFieldVisible(string optionValue)
        {

            WaitForElement(IsMergeEnabledFieldLabel);
            WaitForElement(IsMergeEnabledField_YesOption);
            WaitForElement(IsMergeEnabledField_NoOption);

            Assert.IsTrue(GetElementVisibility(IsMergeEnabledFieldLabel));
            ValidateElementChecked(IsMergeFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateAvailableforFieldLevelSecurityVisible(string optionValue)
        {
            WaitForElement(AvailableforFieldLevelSecurityFieldLabel);
            WaitForElement(AvailableforFieldLevelSecurityField_YesOption);
            WaitForElement(AvailableforFieldLevelSecurityField_NoOption);

            Assert.IsTrue(GetElementVisibility(AvailableforFieldLevelSecurityFieldLabel));
            ValidateElementChecked(AvailableforFieldLevelSecurityFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateAvailableinMobileFieldVisible(string optionValue)
        {
            WaitForElement(AvailableinMobileFieldLabel);
            WaitForElement(AvailableinMobileField_YesOption);
            WaitForElement(AvailableinMobileField_NoOption);

            Assert.IsTrue(GetElementVisibility(AvailableinMobileFieldLabel));
            ValidateElementChecked(AvailableinMobileFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateEncryptMobileOfflineDataFieldVisible(string optionValue)
        {
            WaitForElement(EncryptMobileOfflineDataFieldLabel);
            WaitForElement(EncryptMobileOfflineDataField_YesOption);
            WaitForElement(EncryptMobileOfflineDataField_NoOption);

            Assert.IsTrue(GetElementVisibility(EncryptMobileOfflineDataFieldLabel));
            ValidateElementChecked(EncryptMobileOfflineDataFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateEnableMailMergeFieldVisible(string optionValue)
        {
            WaitForElement(EnableMailMergeFieldLabel);
            WaitForElement(EnableMailMergeField_YesOption);
            WaitForElement(EnableMailMergeField_NoOption);

            Assert.IsTrue(GetElementVisibility(EnableMailMergeFieldLabel));
            ValidateElementChecked(EnableMailMergeFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateExcludefromDataDictionaryFieldVisible(string optionValue)
        {

            WaitForElement(ExcludefromDataDictionaryFieldLabel);
            WaitForElement(ExcludefromDataDictionaryField_YesOption);
            WaitForElement(ExcludefromDataDictionaryField_NoOption);

            Assert.IsTrue(GetElementVisibility(ExcludefromDataDictionaryFieldLabel));
            ValidateElementChecked(ExcludefromDataDictionaryFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateExcludefromDataMapsFieldVisible(string optionValue)
        {
            WaitForElement(ExcludefromDataMapsFieldLabel);
            WaitForElement(ExcludefromDataMapsField_YesOption);
            WaitForElement(ExcludefromDataMapsField_NoOption);

            Assert.IsTrue(GetElementVisibility(ExcludefromDataMapsFieldLabel));
            ValidateElementChecked(ExcludefromDataMapsFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateExcludefromWorkflowFieldVisible(string optionValue)
        {
            WaitForElement(ExcludefromWorkflowFieldLabel);
            WaitForElement(ExcludefromWorkflowField_YesOption);
            WaitForElement(ExcludefromWorkflowField_NoOption);

            Assert.IsTrue(GetElementVisibility(ExcludefromWorkflowFieldLabel));
            ValidateElementChecked(ExcludefromWorkflowFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateExcludeFromSARFieldVisible(string optionValue)
        {
            WaitForElement(ExcludefromSARFieldLabel);
            WaitForElement(ExcludefromSARField_YesOption);
            WaitForElement(ExcludefromSARField_NoOption);

            Assert.IsTrue(GetElementVisibility(ExcludefromSARFieldLabel));
            ValidateElementChecked(ExcludefromSARFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateTrackChangesFieldVisible(string optionValue)
        {
            WaitForElement(TrackChangesFieldLabel);
            WaitForElement(TrackChangesField_YesOption);
            WaitForElement(TrackChangesField_NoOption);

            Assert.IsTrue(GetElementVisibility(TrackChangesFieldLabel));
            ValidateElementChecked(TrackChangesFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateEnableErrorManagementFieldVisible(string optionValue)
        {
            WaitForElement(EnableErrorManagementFieldLabel);
            WaitForElement(EnableErrorManagementField_YesOption);
            WaitForElement(EnableErrorManagementField_NoOption);

            Assert.IsTrue(GetElementVisibility(EnableErrorManagementFieldLabel));
            ValidateElementChecked(EnableErrorManagementFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateValidForExportFieldVisible(string optionValue)
        {
            WaitForElement(ValidforExportFieldLabel);
            WaitForElement(ValidforExportField_YesOption);
            WaitForElement(ValidforExportField_NoOption);

            Assert.IsTrue(GetElementVisibility(ValidforExportFieldLabel));
            ValidateElementChecked(ValidforExportFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateDisplayOnTimelineFieldVisible(string optionValue)
        {
            WaitForElement(DisplayOnTimelineFieldLabel);
            WaitForElement(DisplayOnTimelineField_YesOption);
            WaitForElement(DisplayOnTimelineField_NoOption);

            Assert.IsTrue(GetElementVisibility(DisplayOnTimelineFieldLabel));
            ValidateElementChecked(DisplayOnTimelineFieldOptionCheck(optionValue));


            return this;
        }

        public BusinessObjectRecordPage ValidateNameFieldValue(string ExpectedText)
        {
            WaitForElementVisible(NameFieldLabel);
            ScrollToElement(NameFieldLabel);

            WaitForElementVisible(NameField);
            ScrollToElement(NameField);
            ValidateElementValue(NameField, ExpectedText);

            return this;
        }

        public BusinessObjectRecordPage ValidateTypeFieldValue(string ExpectedText)
        {
            WaitForElementVisible(TypeFieldLabel);
            ScrollToElement(TypeFieldLabel);

            WaitForElementVisible(TypeField);
            ScrollToElement(TypeField);
            ValidatePicklistSelectedText(TypeField, ExpectedText);

            return this;
        }

        public BusinessObjectRecordPage ValidateSingularNameFieldValue(string ExpectedText)
        {
            WaitForElementVisible(SingularNameFieldLabel);
            ScrollToElement(SingularNameFieldLabel);

            WaitForElementVisible(SingularNameField);
            ScrollToElement(SingularNameField);
            ValidateElementValue(SingularNameField, ExpectedText);

            return this;
        }

        public BusinessObjectRecordPage ValidatePluralNameFieldValue(string ExpectedText)
        {
            WaitForElementVisible(PluralNameFieldLabel);
            ScrollToElement(PluralNameFieldLabel);

            WaitForElementVisible(PluralNameField);
            ScrollToElement(PluralNameField);
            ValidateElementValue(PluralNameField, ExpectedText);

            return this;
        }

        public BusinessObjectRecordPage ValidateIconFieldValue(string ExpectedText)
        {
            WaitForElementVisible(IconFieldLabel);
            ScrollToElement(IconFieldLabel);

            WaitForElementVisible(IconField);
            ScrollToElement(IconField);
            ValidateElementValue(IconField, ExpectedText);

            return this;
        }

        public BusinessObjectRecordPage ValidateBusinessModuleFieldValue(string ExpectedText)
        {
            WaitForElementVisible(BusinessModuleFieldLabel);
            ScrollToElement(BusinessModuleFieldLabel);

            WaitForElementVisible(BusinessModuleField);
            ScrollToElement(BusinessModuleField);
            ValidatePicklistSelectedText(BusinessModuleField, ExpectedText);

            return this;
        }

        public BusinessObjectRecordPage ValidateDescriptionFieldValue(string ExpectedText)
        {
            WaitForElementVisible(DescriptionFieldLabel);
            ScrollToElement(DescriptionFieldLabel);

            ScrollToElement(DescriptionField);
            WaitForElementVisible(DescriptionField);
            ValidateElementValue(DescriptionField, ExpectedText);

            return this;
        
        }

        public BusinessObjectRecordPage NavigateToBusinessObjectFieldsSubArea()
        {
            WaitForElement(MenuButton);
            Click(MenuButton);

            WaitForElement(relatedItemsLeftSubMenu);
            Click(relatedItemsLeftSubMenu);

            WaitForElement(businessObjectFieldsLeftSubMenuItem);
            Click(businessObjectFieldsLeftSubMenuItem);

            return this;
        }

        public BusinessObjectRecordPage NavigateToDataFormsSubArea()
        {
            WaitForElementToBeClickable(MenuButton);
            ScrollToElement(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(dataFormsLeftSubMenuItem);
            ScrollToElement(dataFormsLeftSubMenuItem);
            Click(dataFormsLeftSubMenuItem);

            return this;
        }
        #endregion

    }
}
